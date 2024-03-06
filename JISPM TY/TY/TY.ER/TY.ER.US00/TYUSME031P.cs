using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.US00
{
    public partial class TYUSME031P : TYBase
    {
        #region Description : 페이지 로드
        public TYUSME031P()
        {
            InitializeComponent();
        }

        private void TYUSME031P_Load(object sender, System.EventArgs e)
        {
            this.DTP01_GDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.SetFocus(this.DTP01_GDATE);
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sDATE = string.Empty;

            sDATE = Get_Date(this.DTP01_GDATE.GetValue().ToString());

            sDATE = "(" + sDATE.Substring(0, 4) + "/" + sDATE.Substring(4, 2) + "/" + sDATE.Substring(6, 2) + "일 기준)";

            // TEMP 삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_957FD514");

            this.DbConnector.ExecuteNonQuery();

            // TEMP 생성
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_957FD515", Get_Date(this.DTP01_GDATE.GetValue().ToString()),
                                                        Get_Date(this.DTP01_GDATE.GetValue().ToString()),
                                                        this.CBH01_GHWAJU.GetValue().ToString()
                                                        );

            this.DbConnector.ExecuteNonQuery();

            DataTable dt = new DataTable();

            // 출력
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_957FG516", Get_Date(this.DTP01_GDATE.GetValue().ToString()));

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYUSME031R();

                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}