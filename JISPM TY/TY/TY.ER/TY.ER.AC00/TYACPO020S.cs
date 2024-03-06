using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 계열사 계정과목 조회 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2013.09.03 10:41
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_393AZ457 : EIS 계정과목 코드 조회
    ///  TY_P_AC_393B0460 : EIS 계정과목 코드 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_393B2462 : EIS 계정과목 코드 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  EPCCDAC : 계정코드
    ///  EPCNMAC : 계정과목명
    /// </summary>
    public partial class TYACPO020S : TYBase
    {
        private string fsCompanyCode = string.Empty;

        public TYACPO020S()
        {
            InitializeComponent();
        }

        #region Description : Page_Load
        private void TYACPO020S_Load(object sender, System.EventArgs e)
        {
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
                this.BTN61_NEW.Visible = false;
                this.BTN61_REM.Visible = false;

                this.BTN61_INQ.Location = new System.Drawing.Point(1094, 11);

            }

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_393AZ457", this.ControlFactory, "01");
            this.FPS91_TY_S_AC_393B2462.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 신규
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYACPO020I(string.Empty)) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_AC_393B0460", dt);
            //this.DbConnector.Attach("TY_P_AC_3C45G550", dt); // 태영그레인 터미널 삭제
            //this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int iCnt = 0;
            string sMESSAGE = string.Empty;

            DataTable dt = this.FPS91_TY_S_AC_393B2462.GetDataSourceInclude(TSpread.TActionType.Remove, "EPCCDAC");

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

            //계정과목 사용 체크
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // GLS
                    iCnt = 0;
                    sMESSAGE = "";
                    this.DbConnector.CommandClear(); //ACDACMF (EIS 계정사용 사용체크(GLS))
                    this.DbConnector.Attach("TY_P_AC_3C5AW557", dt.Rows[i]["EPCCDAC"].ToString());
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                    if (iCnt != 0)
                    {
                        sMESSAGE = "계정과목 : (" + dt.Rows[i]["EPCCDAC"].ToString() + ") 태영 GLS 사용중입니다 (삭제 불가)";
                        this.ShowCustomMessage(sMESSAGE, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }

                    // 태영호라이즌
                    iCnt = 0;
                    sMESSAGE = "";
                    this.DbConnector.CommandClear(); //ACDACMF (EIS 계정사용 사용체크(태영호라이즌))
                    this.DbConnector.Attach("TY_P_AC_3C5AX558", dt.Rows[i]["EPCCDAC"].ToString());
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                    if (iCnt != 0)
                    {
                        sMESSAGE = "계정과목 : (" + dt.Rows[i]["EPCCDAC"].ToString() + ") 태영 호라이즌 사용중입니다 (삭제 불가)";
                        this.ShowCustomMessage(sMESSAGE, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }

                    // 태영그레인터미널
                    iCnt = 0;
                    sMESSAGE = "";
                    this.DbConnector.CommandClear(); //ACDACMF (EIS 계정사용 사용체크(태영그레인터미널))
                    this.DbConnector.Attach("TY_P_AC_3C5AX559", dt.Rows[i]["EPCCDAC"].ToString());
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                    if (iCnt != 0)
                    {
                        sMESSAGE = "계정과목 : (" + dt.Rows[i]["EPCCDAC"].ToString() + ") 태영 그레인터미널 사용중입니다 (삭제 불가)";
                        this.ShowCustomMessage(sMESSAGE, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }

                }
            }

            e.ArgData = dt;
        }
        #endregion

        #region Description : 그리드 선택 처리
        private void FPS91_TY_S_AC_393B2462_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (fsCompanyCode == "")
            {
                if (this.OpenModalPopup(new TYACPO020I(this.FPS91_TY_S_AC_393B2462.GetValue("EPCCDAC").ToString())) == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion
    }
}
