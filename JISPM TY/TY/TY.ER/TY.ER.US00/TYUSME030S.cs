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
    public partial class TYUSME030S : TYBase
    {
        #region Description : 페이지 로드
        public TYUSME030S()
        {
            InitializeComponent();
        }

        private void TYUSME030S_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_US_957BB512.Initialize();

            this.MTB01_GDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));

            SetStartingFocus(this.MTB01_GDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            double dMIJUNAMT = 0;
            double dMIDANGAMT = 0;
            double dMIIPAMT = 0;
            double dMIMISUAMT = 0;

            this.FPS91_TY_S_US_957BB512.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.Attach
                (
                "TY_P_US_957BA511",
                Get_Date(this.MTB01_GDATE.GetValue().ToString()),
                this.CBH01_GHWAJU.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_957BB512.SetValue(dt);

            dMIJUNAMT = 0;
            dMIDANGAMT = 0;
            dMIIPAMT = 0;
            dMIMISUAMT = 0;

            for (int i = 0; i < this.FPS91_TY_S_US_957BB512.ActiveSheet.RowCount; i++)
            {
                dMIJUNAMT = dMIJUNAMT + double.Parse(String.Format("{0,9:N0}", this.FPS91_TY_S_US_957BB512.GetValue(i, "MIJUNAMT").ToString()));
                dMIDANGAMT = dMIDANGAMT + double.Parse(String.Format("{0,9:N0}", this.FPS91_TY_S_US_957BB512.GetValue(i, "MIDANGAMT").ToString()));
                dMIIPAMT = dMIIPAMT + double.Parse(String.Format("{0,9:N0}", this.FPS91_TY_S_US_957BB512.GetValue(i, "MIIPAMT").ToString()));
                dMIMISUAMT = dMIMISUAMT + double.Parse(String.Format("{0,9:N0}", this.FPS91_TY_S_US_957BB512.GetValue(i, "MIMISUAMT").ToString()));
            }

            this.TXT01_MIJUNAMT.SetValue((dMIJUNAMT).ToString("#,##0"));
            this.TXT01_MIDANGAMT.SetValue((dMIDANGAMT).ToString("#,##0"));
            this.TXT01_MIIPAMT.SetValue((dMIIPAMT).ToString("#,##0"));
            this.TXT01_MIMISUAMT.SetValue((dMIMISUAMT).ToString("#,##0"));

        }
        #endregion
    }
}