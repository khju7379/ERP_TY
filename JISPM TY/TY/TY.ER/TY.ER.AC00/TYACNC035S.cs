using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.AC00
{
    /// <summary>
    /// 투하자금출력(자금용) 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2013.03.19 15:15
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_33J3E342 : 투하자금 출력(자금용)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_33J3E343 : 투하자금 출력(자금용)
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACNC035S : TYBase
    {
        public TYACNC035S()
        {
            InitializeComponent();
        }

        #region Description : Page_Load
        private void TYACNC035S_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.DTP01_GSTYYMM);
        } 
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sDATE = string.Empty;

            sDATE = "(" + this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4) + "." + this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2) + ")";
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_33J3E342",
                sDATE.ToString(),
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GSTYYMM.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_33J3E343.SetValue(dt);

                // 특정 ROW 색깔 입히기
                for (int i = 0; i < this.FPS91_TY_S_AC_33J3E343.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_33J3E343.GetValue(i, "A1NMAC").ToString() == "소  계")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_33J3E343.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    }
                    else if (this.FPS91_TY_S_AC_33J3E343.GetValue(i, "THNM").ToString() == "투하자금 합계")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_33J3E343.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
                    }
                }
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        } 
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {

            string sDATE = string.Empty;

            sDATE =this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 6) ;
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_33P33366",
                sDATE.ToString(),
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GSTYYMM.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYACNC035R();

                // 가로 출력
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        } 
        #endregion
    }
}
