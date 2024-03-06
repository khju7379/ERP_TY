using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 출장비정산관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.04.03 09:06
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_54394023 : 출장비정산관리 등록
    ///  TY_P_HR_54395024 : 출장비정산관리 수정
    ///  TY_P_HR_5439A025 : 출장비정산관리 삭제
    ///  TY_P_HR_5439D029 : 출장비정산관리 확인
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_5439A027 : 출장비정산관리용 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  REM : 삭제
    ///  SAV : 저장
    ///  GXCODE : 휴무코드
    ///  GXSABUN : 사원번호
    ///  GHHAENG : 행선지
    ///  GHSAYU : 휴무사유
    ///  GHTRWAY : 교통편
    ///  GXBSDATE1 : 출장시작일자
    ///  GXBSDATE2 : 출장종료일자
    ///  GXDATE : 휴무일자
    ///  GXGWDOCID : 출장문서 번호
    ///  GXGWURL : 그룹웨어문서URL
    /// </summary>
    public partial class TYHRGT010I : TYBase
    {
        private string fsGXGWDOCID;
        private string fsGXCODE;
        private string fsGXDATE;


        #region  Description : 폼 로드 이벤트
        public TYHRGT010I(string sGXGWDOCID, string sGXCODE, string sGXDATE)
        {
            InitializeComponent();

            fsGXGWDOCID = sGXGWDOCID;
            fsGXCODE = sGXCODE;
            fsGXDATE = sGXDATE;
        }

        private void TYHRGT010I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            if (string.IsNullOrEmpty(this.fsGXGWDOCID))
            {
                //등록
                this.BTN61_SEL.SetReadOnly(false);

                this.Initialize_Controls("01");

                this.SetStartingFocus(this.TXT01_GXGWDOCID);
            }
            else
            {
                this.BTN61_SEL.SetReadOnly(true);
                //수정
                this.UP_DataBinding();
            }
        }
        #endregion

        #region  Description :  UP_DataBinding 이벤트
        private void UP_DataBinding()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_5439D029", fsGXCODE, fsGXDATE, fsGXGWDOCID);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.TXT01_GXGWDOCID.SetValue(dt.Rows[0]["GXGWDOCID"].ToString());
            this.TXT01_GXGWDOCNAME.SetValue(dt.Rows[0]["GXGWDOCNAME"].ToString());
            this.CBH01_GXCODE.SetValue(dt.Rows[0]["GXCODE"].ToString());
            this.DTP01_GXDATE.SetValue(dt.Rows[0]["GXDATE"].ToString());
            this.TXT01_GHHAENG.SetValue(dt.Rows[0]["GHHAENG"].ToString());
            this.TXT01_GHSAYU.SetValue(dt.Rows[0]["GHSAYU"].ToString());
            this.TXT01_GHTRWAY.SetValue(dt.Rows[0]["GHTRWAY"].ToString());
            this.DTP01_GXBSDATE1.SetValue(dt.Rows[0]["GXBSDATE1"].ToString());
            this.DTP01_GXBSDATE2.SetValue(dt.Rows[0]["GXBSDATE2"].ToString());
            this.TXT01_GXGWURL.SetValue(dt.Rows[0]["GXGWURL"].ToString());

            this.FPS91_TY_S_HR_5439A027.SetValue(dt);

            this.TXT01_GXGWDOCID.SetReadOnly(true);

            for (int i = 0; i < this.FPS91_TY_S_HR_5439A027.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_HR_5439A027.GetValue(i, "GXJUNNO1").ToString() != "")
                {
                    this.FPS91_TY_S_HR_5439A027_Sheet1.Rows[i].Locked = true;
                }
            }
        }
        #endregion


        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_5439A025", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.UP_DataBinding();

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                double dGXJUNTOTAL = Convert.ToDouble(ds.Tables[0].Rows[i]["GXTRCOST"].ToString()) +
                                     Convert.ToDouble(ds.Tables[0].Rows[i]["GXROCOST"].ToString()) +
                                     Convert.ToDouble(ds.Tables[0].Rows[i]["GXFOCOST"].ToString()) +
                                     Convert.ToDouble(ds.Tables[0].Rows[i]["GXDYCOST"].ToString()) +
                                     Convert.ToDouble(ds.Tables[0].Rows[i]["GXETCCOST"].ToString()); 

                this.DbConnector.Attach("TY_P_HR_54394023", ds.Tables[0].Rows[i]["GXSABUN"].ToString(),
                                                            this.DTP01_GXDATE.GetString(),
                                                            this.CBH01_GXCODE.GetValue().ToString(),
                                                            this.TXT01_GXGWDOCID.GetValue().ToString(),
                                                            this.TXT01_GXGWDOCNAME.GetValue().ToString(),
                                                            "N",
                                                            ds.Tables[0].Rows[i]["GXTRCOST"].ToString(),
                                                            "N",
                                                            ds.Tables[0].Rows[i]["GXROCOST"].ToString(),
                                                            "N",
                                                            ds.Tables[0].Rows[i]["GXFOCOST"].ToString(),
                                                            "N",
                                                            ds.Tables[0].Rows[i]["GXDYCOST"].ToString(),
                                                            "N",
                                                            ds.Tables[0].Rows[i]["GXETCCOST"].ToString(),
                                                            dGXJUNTOTAL.ToString(),
                                                            this.DTP01_GXBSDATE1.GetString().ToString(),
                                                            this.DTP01_GXBSDATE2.GetString().ToString(),
                                                            DateTime.Now.ToString("yyyyMMdd"),
                                                            "",
                                                            "",
                                                            "",
                                                            "",
                                                            DateTime.Now.ToString("yyyyMMdd"),
                                                            TYUserInfo.EmpNo
                                                            );
            }

            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                double dGXJUNTOTAL = Convert.ToDouble(ds.Tables[1].Rows[i]["GXTRCOST"].ToString()) +
                                     Convert.ToDouble(ds.Tables[1].Rows[i]["GXROCOST"].ToString()) +
                                     Convert.ToDouble(ds.Tables[1].Rows[i]["GXFOCOST"].ToString()) +
                                     Convert.ToDouble(ds.Tables[1].Rows[i]["GXDYCOST"].ToString()) +
                                     Convert.ToDouble(ds.Tables[1].Rows[i]["GXETCCOST"].ToString());

                this.DbConnector.Attach("TY_P_HR_543HW073", ds.Tables[1].Rows[i]["GXTRCOST"].ToString(),                                                            
                                                            ds.Tables[1].Rows[i]["GXROCOST"].ToString(),                                                            
                                                            ds.Tables[1].Rows[i]["GXFOCOST"].ToString(),                                                            
                                                            ds.Tables[1].Rows[i]["GXDYCOST"].ToString(),                                                            
                                                            ds.Tables[1].Rows[i]["GXETCCOST"].ToString(),
                                                            dGXJUNTOTAL.ToString(),
                                                            DateTime.Now.ToString("yyyyMMdd"),
                                                            TYUserInfo.EmpNo,
                                                            ds.Tables[1].Rows[i]["GXSABUN"].ToString(),
                                                            this.DTP01_GXDATE.GetString(),
                                                            this.CBH01_GXCODE.GetValue().ToString(),
                                                            this.TXT01_GXGWDOCID.GetValue().ToString()
                                                            );
            }
            this.DbConnector.ExecuteTranQueryList();

            this.UP_DataBinding();

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_HR_5439A027.GetDataSourceInclude(TSpread.TActionType.New, "GXSABUN", "GXTRCOST", "GXROCOST", "GXFOCOST", "GXDYCOST", "GXETCCOST"));
            ds.Tables.Add(this.FPS91_TY_S_HR_5439A027.GetDataSourceInclude(TSpread.TActionType.Update, "GXSABUN", "GXTRCOST", "GXROCOST", "GXFOCOST", "GXDYCOST", "GXETCCOST", "GXJUNNO1", "GXJUNNO2"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    if (ds.Tables[1].Rows[i]["GXJUNNO1"].ToString() != "" || ds.Tables[1].Rows[i]["GXJUNNO2"].ToString() != "")
                    {
                        this.ShowMessage("TY_M_GB_25F8V482");
                        e.Successed = false;
                        return;
                    }
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
            DataTable dt = this.FPS91_TY_S_HR_5439A027.GetDataSourceInclude(TSpread.TActionType.Remove, "GXSABUN","GXDATE","GXCODE","GXGWDOCID", "GXJUNNO1", "GXJUNNO2");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["GXJUNNO1"].ToString() != "" || dt.Rows[i]["GXJUNNO2"].ToString() != "")
                {
                    this.ShowMessage("TY_M_GB_25F8V482");
                    e.Successed = false;
                    return;
                }
            }


            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region Description : 선택 버튼 이벤트
        private void BTN61_SEL_Click(object sender, EventArgs e)
        {
            TYHRGT10C1 popup = new TYHRGT10C1();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (popup.fsPopGHCODE != "" && popup.fsPopGHDATE != "" && popup.fsPopGHGWID != "")
                {
                    this.FPS91_TY_S_HR_5439A027.Initialize();
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_543F4063", popup.fsPopGHDATE, popup.fsPopGHCODE, popup.fsPopGHGWID);
                    DataTable dt = this.DbConnector.ExecuteDataTable();

                    this.TXT01_GXGWDOCID.SetValue(dt.Rows[0]["GHGWID"].ToString());
                    this.TXT01_GXGWDOCNAME.SetValue("");
                    this.CBH01_GXCODE.SetValue(dt.Rows[0]["GHCODE"].ToString());
                    this.DTP01_GXDATE.SetValue(dt.Rows[0]["GHDATE"].ToString());

                    this.TXT01_GHHAENG.SetValue(dt.Rows[0]["GHHAENG"].ToString());
                    this.TXT01_GHSAYU.SetValue(dt.Rows[0]["GHSAYU"].ToString());
                    this.TXT01_GHTRWAY.SetValue(dt.Rows[0]["GHTRWAY"].ToString());
                    this.DTP01_GXBSDATE1.SetValue(dt.Rows[0]["GHSTDATE"].ToString());
                    this.DTP01_GXBSDATE2.SetValue(dt.Rows[0]["GHEDDATE"].ToString());
                    this.TXT01_GXGWURL.SetValue(dt.Rows[0]["GHGWURL"].ToString());

                    this.FPS91_TY_S_HR_5439A027.SetValue(dt);

                    for (int i = 0; i < this.FPS91_TY_S_HR_5439A027.CurrentRowCount; i++)
                    {
                        this.FPS91_TY_S_HR_5439A027.ActiveSheet.RowHeader.Cells[i, 0].Text = "N";
                    }
                }
            }
        }
        #endregion

        #region Description : FPS91_TY_S_HR_5439A027_LeaveCell 이벤트
        private void FPS91_TY_S_HR_5439A027_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            if (e.Column != 5 && e.Column != 6 && e.Column != 7 && e.Column != 8 && e.Column != 9)
                return;

            double dTotal = Convert.ToDouble(this.FPS91_TY_S_HR_5439A027.GetValue(e.Row, "GXTRCOST").ToString().Trim()) +
                            Convert.ToDouble(this.FPS91_TY_S_HR_5439A027.GetValue(e.Row, "GXROCOST").ToString().Trim()) +
                            Convert.ToDouble(this.FPS91_TY_S_HR_5439A027.GetValue(e.Row, "GXFOCOST").ToString().Trim()) +
                            Convert.ToDouble(this.FPS91_TY_S_HR_5439A027.GetValue(e.Row, "GXDYCOST").ToString().Trim()) +
                            Convert.ToDouble(this.FPS91_TY_S_HR_5439A027.GetValue(e.Row, "GXETCCOST").ToString().Trim());

            this.FPS91_TY_S_HR_5439A027.SetValue(e.Row, "TOTCOST", dTotal.ToString());
        }
        #endregion


    }
}
