using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TY.Service.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.AC00
{
    /// <summary>
    /// 세무구분별 매입명세서 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.05.14 16:13
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_25EAH372 : 세무구분별 매입명세서 조회
    ///  TY_P_AC_25G19489 : 세무구분별 매입명세서 출력
    ///  TY_P_AC_25H3V532 : 세무구분별 매입명세서 집계표
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_25E4Y431 : 세무구분별 매입명세서
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_25GAZ484 : 세무 구분을 선택하세요.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  VNCODE : 거래처코드
    ///  B4VLMI1 : 관리항목값１
    ///  B4VLMI2 : 관리항목값２
    ///  B4VLMI4 : 관리항목값４
    ///  GDATEGUBUN : 일자구분
    ///  CBO01_GPRTGN : 출력구분
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACTX003S : TYBase
    {
        string fsAMIYEAR    = string.Empty;
        string fsAMIBRANCH  = string.Empty;
        string fsAMIREPGB   = string.Empty;
        string fsAMIRPTGUBN = string.Empty;

        string fsPOPUP = string.Empty;

        #region Description : 페이지 로드
        public TYACTX003S()
        {
            InitializeComponent();
        }

        public TYACTX003S(string sAMIYEAR, string sAMIBRANCH, string sAMIREPGB, string sAMIRPTGUBN, string sPOPUP)
        {
            InitializeComponent();

            fsAMIYEAR    = sAMIYEAR.ToString();
            fsAMIBRANCH  = sAMIBRANCH.ToString();
            fsAMIREPGB   = sAMIREPGB.ToString();
            fsAMIRPTGUBN = sAMIRPTGUBN.ToString();

            fsPOPUP     = sPOPUP.ToString();

            // 폼사이즈 조정
            this.ClientSize = new System.Drawing.Size(1184, 750);
        }

        private void TYACTX003S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            if (fsPOPUP.ToString() == "")
            {
                this.TXT01_AMIYEAR.SetValue(DateTime.Now.ToString("yyyyMMdd").Substring(0, 4));
            }
            else
            {
                this.TXT01_AMIYEAR.SetValue(fsAMIYEAR.ToString());
                this.CBO01_AMIBRANCH.SetValue(fsAMIBRANCH.ToString());
                this.CBO01_AMIREPGB.SetValue(fsAMIREPGB.ToString());
                this.CBO01_AMIRPTGUBN.SetValue(fsAMIRPTGUBN.ToString());

                this.BTN61_INQ_Click(null, null);
            }

            SetStartingFocus(this.TXT01_AMIYEAR);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3BM4V420",
                this.TXT01_AMIYEAR.GetValue().ToString(),
                this.CBO01_AMIREPGB.GetValue().ToString(),
                this.CBO01_AMIRPTGUBN.GetValue().ToString(),
                this.CBO01_AMIBRANCH.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_3BM4Z422.SetValue(dt);
            }
            else
            {
                this.FPS91_TY_S_AC_3BM4Z422.SetValue(dt);

                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYACTX003I
                (
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                ""
                )) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_3BM4Y421", dt);
            this.DbConnector.ExecuteNonQueryList();

            for (i = 0; i < dt.Rows.Count; i++)
            {
                if ((new TYACTX019S(dt.Rows[i]["AMIYEAR"].ToString(),        dt.Rows[i]["AMIBRANCH"].ToString(),
                                    dt.Rows[i]["AMIREPGB"].ToString() + "2", "POPUP")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.TXT01_AMIYEAR.Focus();

                    this.BTN61_INQ_Click(null, null);
                    this.ShowMessage("TY_M_GB_23NAD874");
                }
            }
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_3BM4Z422.GetDataSourceInclude(TSpread.TActionType.Remove, "AMIYEAR", "AMIREPGB", "AMIRPTGUBN", "AMIBRANCH", "AMITAXGUBN", "AMIDEALDT", "AMIVNEDCD", "AMISAUPNO");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            DataTable dt1 = new DataTable();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_42B8L317",
                    dt.Rows[i]["AMIYEAR"].ToString(),
                    dt.Rows[i]["AMIBRANCH"].ToString(),
                    dt.Rows[i]["AMIREPGB"].ToString(),
                    "2"
                    );

                dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_42B8N318");
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

        #region Description : 스프레드 더블클릭 이벤트
        private void FPS91_TY_S_AC_3BM4Z422_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.OpenModalPopup(new TYACTX003I
                (
                this.FPS91_TY_S_AC_3BM4Z422.GetValue("AMIYEAR").ToString(),
                this.FPS91_TY_S_AC_3BM4Z422.GetValue("AMIREPGB").ToString(),
                this.FPS91_TY_S_AC_3BM4Z422.GetValue("AMIRPTGUBN").ToString(),
                this.FPS91_TY_S_AC_3BM4Z422.GetValue("AMIBRANCH").ToString(),
                this.FPS91_TY_S_AC_3BM4Z422.GetValue("AMITAXGUBN").ToString(),
                this.FPS91_TY_S_AC_3BM4Z422.GetValue("AMIDEALDT").ToString(),
                this.FPS91_TY_S_AC_3BM4Z422.GetValue("AMIVNEDCD").ToString(),
                this.FPS91_TY_S_AC_3BM4Z422.GetValue("AMISAUPNO").ToString(),
                "UPT"
                )) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}