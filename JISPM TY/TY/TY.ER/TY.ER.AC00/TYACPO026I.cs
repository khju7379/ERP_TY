using System;
using System.Data;
using System.Drawing;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 계열사 마감 관리 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2013.11.06 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_27O6X262 : 경영정보 마감 체크
    ///  TY_P_AC_3B79S213 : 계열사 EIS 마감관리 등록
    ///  TY_P_AC_3B79T215 : 계열사 EIS 마감관리 삭제
    ///  TY_P_AC_3B79T216 : 계열사 EIS 마감관리 수정
    ///  TY_P_AC_3B79U217 : 계열사 EIS 마감관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_3B79V218 : 계열사 EIS 마감관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_AC_243AY315 : 작업이 불가합니다.
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_AC_26B9D824 : 인덱스를 확인하세요.
    ///  TY_M_AC_26D6A858 : 데이터가 존재합니다.
    ///  TY_M_AC_27O70264 : 이후 월에 마감된 자료가 존재합니다. 마감 구분을 확인하세요.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_23S40973 : 동일한 코드가 존재합니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  ECSCUST : 계열사구분
    ///  ECSYYMM : 년월
    ///  ECYEAR : 년도
    /// </summary>
    public partial class TYACPO026I : TYBase
    {
        private string fsCompanyCode;

        #region Description : 페이지 로드
        public TYACPO026I()
        {
            InitializeComponent();
        }

        private void TYACPO026I_Load(object sender, System.EventArgs e)
        {

            this.BTN61_SAV.Visible = false;
            this.BTN61_REM.Visible = false;

            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_3B79V218, "ECSCUST");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_3B79V218, "ECSYYMM");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            switch (TYUserInfo.EmpNo.Substring(0, 2))
            {
                case "HT":
                    fsCompanyCode = "TH";
                    break;
                case "TG":
                    fsCompanyCode = "TG";
                    break;
                case "TS":
                    fsCompanyCode = "TS";
                    break;
                case "TL":
                    fsCompanyCode = "TL";
                    break;
                default:
                    fsCompanyCode = "";
                    break;
            }

            if (fsCompanyCode != "")
            {
                this.CBH01_ECSCUST.SetValue(fsCompanyCode);
                this.CBH01_ECSCUST.SetReadOnly(true);
            }

            this.TXT01_ECYEAR.SetValue(DateTime.Now.ToString("yyyy"));

            if (fsCompanyCode != "")
            {
                this.SetStartingFocus(this.TXT01_ECYEAR);
            }
            else
            {
                this.SetStartingFocus(this.CBH01_ECSCUST.CodeText);
            }

        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_3B79U217",
                this.CBH01_ECSCUST.GetValue().ToString(),
                this.TXT01_ECYEAR.GetValue().ToString()
                );

            this.FPS91_TY_S_AC_3B79V218.SetValue(this.DbConnector.ExecuteDataTable());

            this.FPS91_TY_S_AC_3B79V218.Focus();
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sECGUBUN = string.Empty;

            int i = 0;
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            // 등록
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["ECGUBUN"].ToString() == "Y")
                {
                    sECGUBUN = "N";
                }
                else
                {
                    sECGUBUN = "Y";
                }

                this.DbConnector.Attach("TY_P_AC_3B79S213", ds.Tables[0].Rows[i]["ECSCUST"].ToString(),
                                                            ds.Tables[0].Rows[i]["ECSYYMM"].ToString(),
                                                            ds.Tables[0].Rows[i]["ECSISSU"].ToString(),
                                                            ds.Tables[0].Rows[i]["ECSFACI"].ToString(),
                                                            ds.Tables[0].Rows[i]["ECSINSA"].ToString(),
                                                            ds.Tables[0].Rows[i]["ECSSALE"].ToString(),
                                                            ds.Tables[0].Rows[i]["ECSPLAN"].ToString(),
                                                            ds.Tables[0].Rows[i]["ECSRESU"].ToString(),
                                                            ds.Tables[0].Rows[i]["ECSLOAN"].ToString(),
                                                            ds.Tables[0].Rows[i]["ECSCASH"].ToString(),
                                                            TYUserInfo.EmpNo.ToString(),
                                                            ds.Tables[0].Rows[i]["ECSCUST"].ToString(),
                                                            ds.Tables[0].Rows[i]["ECSYYMM"].ToString()
                                                            );
            }

            // 수정
            for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                if (ds.Tables[1].Rows[i]["ECGUBUN"].ToString() == "Y")
                {
                    sECGUBUN = "N";
                }
                else
                {
                    sECGUBUN = "Y";
                }


                this.DbConnector.Attach("TY_P_AC_3B79T216", ds.Tables[0].Rows[i]["ECSISSU"].ToString(),
                                                            ds.Tables[0].Rows[i]["ECSFACI"].ToString(),
                                                            ds.Tables[0].Rows[i]["ECSINSA"].ToString(),
                                                            ds.Tables[0].Rows[i]["ECSSALE"].ToString(),
                                                            ds.Tables[0].Rows[i]["ECSPLAN"].ToString(),
                                                            ds.Tables[0].Rows[i]["ECSRESU"].ToString(),
                                                            ds.Tables[0].Rows[i]["ECSLOAN"].ToString(),
                                                            ds.Tables[0].Rows[i]["ECSCASH"].ToString(),
                                                            TYUserInfo.EmpNo.ToString(),
                                                            ds.Tables[0].Rows[i]["ECSCUST"].ToString(),
                                                            ds.Tables[0].Rows[i]["ECSYYMM"].ToString()
                                                            );
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {

            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_3B79T215", dt);

            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_3B79V218.GetDataSourceInclude(TSpread.TActionType.New, "ECSCUST", "ECSYYMM", "ECSISSU", "ECSFACI", "ECSINSA", "ECSSALE", "ECSPLAN", "ECSRESU", "ECSLOAN", "ECSCASH"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_3B79V218.GetDataSourceInclude(TSpread.TActionType.Update, "ECSCUST", "ECSYYMM", "ECSISSU", "ECSFACI", "ECSINSA", "ECSSALE", "ECSPLAN", "ECSRESU", "ECSLOAN", "ECSCASH"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
                e.Successed = false;
                return;
            }

            //for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            //{
            //    this.DbConnector.CommandClear();

            //    this.DbConnector.Attach
            //        (
            //        "TY_P_AC_27O6X262",
            //        ds.Tables[1].Rows[i]["ECYEAR"].ToString()
            //        );

            //    DataTable dt = this.DbConnector.ExecuteDataTable();

            //    if (dt.Rows.Count > 0)
            //    {
            //        if (int.Parse(ds.Tables[1].Rows[i]["ECMONTH"].ToString()) < int.Parse(dt.Rows[0]["ECMONTH"].ToString()))
            //        {
            //            this.ShowMessage("TY_M_AC_27O70264");
            //            e.Successed = false;
            //            return;
            //        }
            //    }
            //}

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
            int i = 0;

            DataTable dt = this.FPS91_TY_S_AC_3B79V218.GetDataSourceInclude(TSpread.TActionType.Remove, "ECSCUST", "ECSYYMM");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            //for (i = 0; i < dt.Rows.Count; i++)
            //{
            //    this.DbConnector.CommandClear();

            //    this.DbConnector.Attach
            //        (
            //        "TY_P_AC_27O6X262",
            //        dt.Rows[i]["ECYEAR"].ToString()
            //        );

            //    DataTable dtchk = this.DbConnector.ExecuteDataTable();

            //    if (dtchk.Rows.Count > 0)
            //    {
            //        if (int.Parse(dt.Rows[i]["ECMONTH"].ToString()) < int.Parse(dtchk.Rows[0]["ECMONTH"].ToString()))
            //        {
            //            this.ShowMessage("TY_M_AC_27O70264");
            //            e.Successed = false;
            //            return;
            //        }
            //    }
            //}

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion
    }
}