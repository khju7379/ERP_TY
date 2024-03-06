using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;
using TY.Service.Library.Controls.TYSpreadCellType;
using GrapeCity.ActiveReports;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using TY.ER.GB00;
using TY.ER.AC00;

namespace TY.ER.US00
{
    /// <summary>
    /// 선급자재 관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2013.02.19 09:59
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// </summary>
    public partial class TYUSME033S : TYBase
    {
        #region Description : 페이지 로드
        public TYUSME033S()
        {
            InitializeComponent();
        }

        private void TYUSME033S_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_US_95KE2588.Initialize();

            this.DTP01_GDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));

            SetStartingFocus(this.DTP01_GDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sDATE = string.Empty;

            sDATE = "(" + Get_Date(this.DTP01_GDATE.GetValue().ToString()).Substring(0, 4) + "/" + Get_Date(this.DTP01_GDATE.GetValue().ToString()).Substring(4, 2) + ")";

            this.FPS91_TY_S_US_95KE2588.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.Attach
                (
                "TY_P_US_95KE1587",
                sDATE.ToString(),
                Get_Date(this.DTP01_GDATE.GetValue().ToString()),
                this.CBH01_GHWAJU.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_95KE2588.SetValue(dt);
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sDATE = string.Empty;

            sDATE = "(" + Get_Date(this.DTP01_GDATE.GetValue().ToString()).Substring(0, 4) + "/" + Get_Date(this.DTP01_GDATE.GetValue().ToString()).Substring(4, 2) + ")";

            DataTable dt = new DataTable();

            this.DbConnector.Attach
                (
                "TY_P_US_95KE1587",
                sDATE.ToString(),
                Get_Date(this.DTP01_GDATE.GetValue().ToString()),
                this.CBH01_GHWAJU.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYUSME033R();

                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
        }
        #endregion
    }
}