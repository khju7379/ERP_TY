using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.UT00
{
    /// <summary>
    /// 코드관리 등록 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.03.29 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_668DK092 : 코드관리 INDEX 조회
    ///  TY_P_UT_668DU094 : 코드관리 CODE 조회
    ///  TY_P_MR_2B21D026 : 코드관리 체크
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_72MA2763 : 코드관리 INDEX 조회
    ///  TY_S_UT_72MA8764 : 코드관리 CODE  조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_AC_243AY315 : 작업이 불가합니다.
    ///  TY_M_AC_26B9D824 : 인덱스를 확인하세요.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    /// </summary>
    public partial class TYUTIN035I : TYBase
    {
        private string fsCDINDEX;

        #region Description : Page Load()
        public TYUTIN035I()
        {
            InitializeComponent();

            // 화주
            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_72MA8764, "CUHWAJU",  "HJDESC1", "CUHWAJU");
            // 화물
            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_72MA8764, "CUHWAMUL", "HMDESC1", "CUHWAMUL");
            // 화물
            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_72MA8764, "CUJISI",   "KBHANGL", "CUJISI");
        }

        private void TYUTIN035I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_72MA8764, "CUDATE");

            // 등록 체크
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            // 삭제 체크
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.FPS91_TY_S_UT_72MA2763.Initialize();
            this.FPS91_TY_S_UT_72MA8764.Initialize();

            this.DTP01_STDATE.SetValue("");
            this.DTP01_EDDATE.SetValue("");

            this.SetStartingFocus(this.CBH01_CUHWAJU.CodeText);
            
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            if (Get_Date(this.DTP01_STDATE.GetValue().ToString()) != "" && Get_Date(this.DTP01_EDDATE.GetValue().ToString()) != "")
            {
                this.DbConnector.Attach("TY_P_UT_72LF5755", Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                                                            Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                                                            this.CBH01_CUHWAJU.GetValue().ToString(),
                                                            this.CBH01_CUHWAMUL.GetValue().ToString(),
                                                            this.TXT01_CUTANKNO.GetValue().ToString().Trim());
            }
            else
            {
                this.DbConnector.Attach("TY_P_UT_72LFA756", this.CBH01_CUHWAJU.GetValue().ToString(),
                                                            this.CBH01_CUHWAMUL.GetValue().ToString(),
                                                            this.TXT01_CUTANKNO.GetValue().ToString().Trim());
            }
            
            this.FPS91_TY_S_UT_72MA2763.SetValue(this.DbConnector.ExecuteDataTable());

            // 초기화
            this.FPS91_TY_S_UT_72MA8764.Initialize();
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_72M9Z760", Get_Date(ds.Tables[0].Rows[i]["CUDATE"].ToString()),
                                                                Get_Date(ds.Tables[0].Rows[i]["CUDATE"].ToString()),
                                                                ds.Tables[0].Rows[i]["CUHWAJU"].ToString(),
                                                                ds.Tables[0].Rows[i]["CUHWAMUL"].ToString(),
                                                                ds.Tables[0].Rows[i]["CUTANKNO"].ToString(),
                                                                ds.Tables[0].Rows[i]["CUJISI"].ToString(),
                                                                ds.Tables[0].Rows[i]["CUNUM"].ToString(),
                                                                ds.Tables[0].Rows[i]["CUDESC"].ToString(),
                                                                TYUserInfo.EmpNo
                                                                ); //저장

                    this.DbConnector.ExecuteNonQueryList();
                }
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_UT_72M9Z761", ds.Tables[1].Rows[i]["CUHWAJU"].ToString(),
                                                                ds.Tables[1].Rows[i]["CUHWAMUL"].ToString(),
                                                                ds.Tables[1].Rows[i]["CUTANKNO"].ToString(),
                                                                ds.Tables[1].Rows[i]["CUJISI"].ToString(),
                                                                ds.Tables[1].Rows[i]["CUNUM"].ToString(),
                                                                ds.Tables[1].Rows[i]["CUDESC"].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["CUDATE"].ToString(),
                                                                ds.Tables[1].Rows[i]["CUSEQ"].ToString()
                                                                ); //수정
                }
                this.DbConnector.ExecuteNonQueryList();
            }

            // CODE 조회
            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD873"); // 저장 메세지
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_72M9Z762", dt);
            this.DbConnector.ExecuteNonQueryList();

            // CODE 조회
            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_UT_72MA8764.GetDataSourceInclude(TSpread.TActionType.New,    "CUDATE", "CUHWAJU", "CUHWAMUL", "CUTANKNO", "CUJISI", "CUNUM", "CUDESC"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_UT_72MA8764.GetDataSourceInclude(TSpread.TActionType.Update, "CUDATE", "CUSEQ", "CUHWAJU", "CUHWAMUL", "CUTANKNO", "CUJISI", "CUNUM", "CUDESC"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            // 신규
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // 화주 확인
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_66FAH184",
                                       "HJ",
                                       ds.Tables[0].Rows[i]["CUHWAJU"].ToString(),
                                       ""
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_UT_71NBQ558");
                    e.Successed = false;
                    return;
                }

                // 화물 확인
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_66FAH184",
                                       "HM",
                                       ds.Tables[0].Rows[i]["CUHWAMUL"].ToString(),
                                       ""
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_UT_71NBR559");
                    e.Successed = false;
                    return;
                }

                // 탱크 확인
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_6AKBS435",
                                       ds.Tables[0].Rows[i]["CUTANKNO"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_UT_676GD601");
                    e.Successed = false;
                    return;
                }

                // 지시자 확인
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_GB_72LEX754",
                                       ds.Tables[0].Rows[i]["CUJISI"].ToString(),
                                       ""
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_UT_72MH0767");
                    e.Successed = false;
                    return;
                }

                if (int.Parse(Get_Numeric(ds.Tables[0].Rows[i]["CUNUM"].ToString())) == 0)
                {
                    this.ShowMessage("TY_M_UT_777F6039");
                    e.Successed = false;
                    return;
                }
            }

            // 수정
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                // 화주 확인
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_66FAH184",
                                       "HJ",
                                       ds.Tables[1].Rows[i]["CUHWAJU"].ToString(),
                                       ""
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_UT_71NBQ558");
                    e.Successed = false;
                    return;
                }

                // 화물 확인
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_66FAH184",
                                       "HM",
                                       ds.Tables[1].Rows[i]["CUHWAMUL"].ToString(),
                                       ""
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_UT_71NBR559");
                    e.Successed = false;
                    return;
                }

                // 탱크 확인
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_6AKBS435",
                                       ds.Tables[1].Rows[i]["CUTANKNO"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_UT_676GD601");
                    e.Successed = false;
                    return;
                }

                // 지시자 확인
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_GB_72LEX754",
                                       ds.Tables[1].Rows[i]["CUJISI"].ToString(),
                                       ""
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_UT_72MH0767");
                    e.Successed = false;
                    return;
                }

                if (int.Parse(Get_Numeric(ds.Tables[1].Rows[i]["CUNUM"].ToString())) == 0)
                {
                    this.ShowMessage("TY_M_UT_777F6039");
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt1 = new DataTable();

            DataTable dt = this.FPS91_TY_S_UT_72MA8764.GetDataSourceInclude(TSpread.TActionType.Remove, "CUDATE", "CUSEQ");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_UT_72MA2763_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_72MF5765", this.FPS91_TY_S_UT_72MA2763.GetValue("CUHWAJU").ToString(),
                                                        this.FPS91_TY_S_UT_72MA2763.GetValue("CUHWAMUL").ToString(),
                                                        this.FPS91_TY_S_UT_72MA2763.GetValue("CUTANKNO").ToString());

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_72MA8764.SetValue(dt);
        }

        private void FPS91_TY_S_UT_72MA8764_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            string sCUDATE = string.Empty;

            sCUDATE = DateTime.Now.ToString("yyyyMMdd");

            this.FPS91_TY_S_UT_72MA8764.SetValue(e.RowIndex, "CUDATE", sCUDATE.ToString());
        }
        #endregion
    }
}