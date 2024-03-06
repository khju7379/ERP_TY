using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;
using TY.Service.Library.Controls.TYSpreadCellType;
using DataDynamics.ActiveReports;
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
    public partial class TYUSME039S : TYBase
    {
        #region Description : 페이지 로드
        public TYUSME039S()
        {
            InitializeComponent();
        }

        private void TYUSME039S_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_US_964DL709.Initialize();

            this.DTP01_STDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sSTDATE = string.Empty;
            string sEDDATE = string.Empty;
            string sDATE   = string.Empty;

            sSTDATE = Get_Date(this.DTP01_STDATE.GetValue().ToString());
            sEDDATE = Get_Date(this.DTP01_EDDATE.GetValue().ToString());

            sSTDATE = sSTDATE.ToString().Substring(0, 4) + "/" + sSTDATE.ToString().Substring(4, 2) + "/" + sSTDATE.ToString().Substring(6, 2);
            sEDDATE = sEDDATE.ToString().Substring(0, 4) + "/" + sEDDATE.ToString().Substring(4, 2) + "/" + sEDDATE.ToString().Substring(6, 2);

            sDATE = "(" + sSTDATE.ToString() + "~" + sEDDATE.ToString() + ")";
            

            this.FPS91_TY_S_US_964DL709.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.Attach
                (
                "TY_P_US_964DH708",
                Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                this.CBH01_GHWAJU.GetValue().ToString().Trim(),
                sDATE.ToString(),
                this.CBO01_GGUBUN.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_964DL709.SetValue(dt);

            if (this.FPS91_TY_S_US_964DL709.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_US_964DL709, "TMMCILJA", "합 계", SumRowType.Total, "TMAMT", "TMVAT", "TMAMOUNT");
            }

            for (int i = 0; i < this.FPS91_TY_S_US_964DL709.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_US_964DL709.GetValue(i, "TMMCILJA").ToString() == "합 계")
                {
                    // 특정 ROW 글자 크기 변경
                    this.FPS91_TY_S_US_964DL709.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);
                    this.FPS91_TY_S_US_964DL709.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 194);
                }
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sSTDATE = string.Empty;
            string sEDDATE = string.Empty;
            string sDATE = string.Empty;

            sSTDATE = Get_Date(this.DTP01_STDATE.GetValue().ToString());
            sEDDATE = Get_Date(this.DTP01_EDDATE.GetValue().ToString());

            sSTDATE = sSTDATE.ToString().Substring(0, 4) + "/" + sSTDATE.ToString().Substring(4, 2) + "/" + sSTDATE.ToString().Substring(6, 2);
            sEDDATE = sEDDATE.ToString().Substring(0, 4) + "/" + sEDDATE.ToString().Substring(4, 2) + "/" + sEDDATE.ToString().Substring(6, 2);

            sDATE = "(" + sSTDATE.ToString() + "~" + sEDDATE.ToString() + ")";

            DataTable dt = new DataTable();

            this.DbConnector.Attach
                (
                "TY_P_US_964DH708",
                Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                this.CBH01_GHWAJU.GetValue().ToString().Trim(),
                sDATE.ToString(),
                this.CBO01_GGUBUN.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                ActiveReport rpt = new TYUSME039R();

                rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
        }
        #endregion

        #region Description : 합 계
        private DataTable UP_DataTableSumRow(DataTable dt)
        {
            DataTable table = new DataTable();

            table = dt;

            string sVNSANGHO = "";

            double dTMAMT12  = 0;
            double dTMAMT13  = 0;
            double dTMAMT14  = 0;
            double dTMAMT15  = 0;
            double dTMAMT20  = 0;
            double dTMAMT21  = 0;
            double dTMAMOUNT = 0;

            DataRow row;
            int nNum = table.Rows.Count;
            int i = 0;

            for (i = 1; i < nNum; i++)
            {
                if (table.Rows[i - 1]["VNSANGHO"].ToString() != table.Rows[i]["VNSANGHO"].ToString())
                {
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);
                    
                    // 소 계 이름 넣기
                    table.Rows[i]["VNSANGHO"] = table.Rows[i - 1]["VNSANGHO"].ToString();
                    table.Rows[i]["DATE"]     = table.Rows[i - 1]["DATE"].ToString();
                    table.Rows[i]["TMDATE"]   = "소 계";

                    table.Rows[i]["TMHWAJU"]  = "";

                    
                    sVNSANGHO = " VNSANGHO    = '" + table.Rows[i - 1]["VNSANGHO"].ToString() + "'";


                    dTMAMT12  = dTMAMT12 + double.Parse(Get_Numeric(table.Compute("SUM(TMAMT12)", sVNSANGHO).ToString()));
                    dTMAMT13  = dTMAMT13 + double.Parse(Get_Numeric(table.Compute("SUM(TMAMT13)", sVNSANGHO).ToString()));
                    dTMAMT14  = dTMAMT14 + double.Parse(Get_Numeric(table.Compute("SUM(TMAMT14)", sVNSANGHO).ToString()));
                    dTMAMT15  = dTMAMT15 + double.Parse(Get_Numeric(table.Compute("SUM(TMAMT15)", sVNSANGHO).ToString()));
                    dTMAMT20  = dTMAMT20 + double.Parse(Get_Numeric(table.Compute("SUM(TMAMT20)", sVNSANGHO).ToString()));
                    dTMAMT21  = dTMAMT21 + double.Parse(Get_Numeric(table.Compute("SUM(TMAMT21)", sVNSANGHO).ToString()));
                    dTMAMOUNT = dTMAMOUNT + double.Parse(Get_Numeric(table.Compute("SUM(TMAMOUNT)", sVNSANGHO).ToString()));

                    table.Rows[i]["TMAMT12"]  = Get_Numeric(table.Compute("SUM(TMAMT12)", sVNSANGHO).ToString());
                    table.Rows[i]["TMAMT13"]  = Get_Numeric(table.Compute("SUM(TMAMT13)", sVNSANGHO).ToString());
                    table.Rows[i]["TMAMT14"]  = Get_Numeric(table.Compute("SUM(TMAMT14)", sVNSANGHO).ToString());
                    table.Rows[i]["TMAMT15"]  = Get_Numeric(table.Compute("SUM(TMAMT15)", sVNSANGHO).ToString());
                    table.Rows[i]["TMAMT20"]  = Get_Numeric(table.Compute("SUM(TMAMT20)", sVNSANGHO).ToString());
                    table.Rows[i]["TMAMT21"]  = Get_Numeric(table.Compute("SUM(TMAMT21)", sVNSANGHO).ToString());
                    table.Rows[i]["TMAMOUNT"] = Get_Numeric(table.Compute("SUM(TMAMOUNT)", sVNSANGHO).ToString());

                    nNum = nNum + 1;
                    i = i + 1;

                }
            }

            row = table.NewRow();
            table.Rows.InsertAt(row, i);

            // 소 계 이름 넣기
            table.Rows[i]["VNSANGHO"] = table.Rows[i - 1]["VNSANGHO"].ToString();
            table.Rows[i]["DATE"]     = table.Rows[i - 1]["DATE"].ToString();
            table.Rows[i]["TMDATE"]   = "소 계";

            table.Rows[i]["TMHWAJU"]  = "";


            sVNSANGHO = " VNSANGHO    = '" + table.Rows[i - 1]["VNSANGHO"].ToString() + "'";

            dTMAMT12 = dTMAMT12 + double.Parse(Get_Numeric(table.Compute("SUM(TMAMT12)", sVNSANGHO).ToString()));
            dTMAMT13 = dTMAMT13 + double.Parse(Get_Numeric(table.Compute("SUM(TMAMT13)", sVNSANGHO).ToString()));
            dTMAMT14 = dTMAMT14 + double.Parse(Get_Numeric(table.Compute("SUM(TMAMT14)", sVNSANGHO).ToString()));
            dTMAMT15 = dTMAMT15 + double.Parse(Get_Numeric(table.Compute("SUM(TMAMT15)", sVNSANGHO).ToString()));
            dTMAMT20 = dTMAMT20 + double.Parse(Get_Numeric(table.Compute("SUM(TMAMT20)", sVNSANGHO).ToString()));
            dTMAMT21 = dTMAMT21 + double.Parse(Get_Numeric(table.Compute("SUM(TMAMT21)", sVNSANGHO).ToString()));
            dTMAMOUNT = dTMAMOUNT + double.Parse(Get_Numeric(table.Compute("SUM(TMAMOUNT)", sVNSANGHO).ToString()));

            table.Rows[i]["TMAMT12"]  = Get_Numeric(table.Compute("SUM(TMAMT12)", sVNSANGHO).ToString());
            table.Rows[i]["TMAMT13"]  = Get_Numeric(table.Compute("SUM(TMAMT13)", sVNSANGHO).ToString());

            table.Rows[i]["TMAMT14"]  = Get_Numeric(table.Compute("SUM(TMAMT14)", sVNSANGHO).ToString());
            table.Rows[i]["TMAMT15"]  = Get_Numeric(table.Compute("SUM(TMAMT15)", sVNSANGHO).ToString());
            table.Rows[i]["TMAMT20"]  = Get_Numeric(table.Compute("SUM(TMAMT20)", sVNSANGHO).ToString());
            table.Rows[i]["TMAMT21"]  = Get_Numeric(table.Compute("SUM(TMAMT21)", sVNSANGHO).ToString());
            table.Rows[i]["TMAMOUNT"] = Get_Numeric(table.Compute("SUM(TMAMOUNT)", sVNSANGHO).ToString());

            nNum = nNum + 1;
            i = i + 1;

            row = table.NewRow();
            table.Rows.InsertAt(row, i);

            // 총 계 이름 넣기
            table.Rows[i]["DATE"]     = table.Rows[i - 1]["DATE"].ToString();
            table.Rows[i]["TMDATE"]   = "총 계";

            table.Rows[i]["TMHWAJU"]  = "";
            table.Rows[i]["VNSANGHO"] = "";


            table.Rows[i]["TMAMT12"]  = Convert.ToString(dTMAMT12);
            table.Rows[i]["TMAMT13"]  = Convert.ToString(dTMAMT13);

            table.Rows[i]["TMAMT14"]  = Convert.ToString(dTMAMT14);
            table.Rows[i]["TMAMT15"]  = Convert.ToString(dTMAMT15);
            table.Rows[i]["TMAMT20"]  = Convert.ToString(dTMAMT20);
            table.Rows[i]["TMAMT21"]  = Convert.ToString(dTMAMT21);
            table.Rows[i]["TMAMOUNT"] = Convert.ToString(dTMAMOUNT);

            return table;
        }
        #endregion
    }
}