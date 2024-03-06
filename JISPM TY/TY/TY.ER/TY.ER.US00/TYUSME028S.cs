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
    public partial class TYUSME028S : TYBase
    {
        #region Description : 페이지 로드
        public TYUSME028S()
        {
            InitializeComponent();
        }

        private void TYUSME028S_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_US_95SFW642.Initialize();

            this.DTP01_STDATE.SetValue(DateTime.Now.ToString("yyyy"));
            this.DTP01_EDDATE.SetValue(DateTime.Now.ToString("yyyy"));

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sDATE = string.Empty;

            sDATE = "(" + Get_Date(this.DTP01_STDATE.GetValue().ToString()) + "~" + Get_Date(this.DTP01_EDDATE.GetValue().ToString()) + ")";

            this.FPS91_TY_S_US_95SFW642.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.Attach
                (
                "TY_P_US_9BK9P526",
                Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                this.CBH01_IHSOSOK.GetValue().ToString(),
                this.CBH01_IHGOKJONG1.GetValue().ToString(),
                sDATE.ToString()
                );

            dt = UP_DataTableSumRow(this.DbConnector.ExecuteDataTable());

            this.FPS91_TY_S_US_95SFW642.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_US_95SFW642.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_US_95SFW642.GetValue(i, "CDDESC1").ToString() == "거래처별 소계")
                {
                    // 특정 ROW 글자 크기 변경
                    this.FPS91_TY_S_US_95SFW642.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);
                    this.FPS91_TY_S_US_95SFW642.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 194);
                }

                if (this.FPS91_TY_S_US_95SFW642.GetValue(i, "CDDESC1").ToString() == "총 계")
                {
                    // 특정 ROW 글자 크기 변경
                    this.FPS91_TY_S_US_95SFW642.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);
                    this.FPS91_TY_S_US_95SFW642.ActiveSheet.Rows[i].BackColor = Color.SkyBlue;
                }
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sDATE = string.Empty;

            sDATE = "(" + Get_Date(this.DTP01_STDATE.GetValue().ToString()) + "~" + Get_Date(this.DTP01_EDDATE.GetValue().ToString()) + ")";

            DataTable dt = new DataTable();

            this.DbConnector.Attach
                (
                "TY_P_US_9BK9P526",
                Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                this.CBH01_IHSOSOK.GetValue().ToString(),
                this.CBH01_IHGOKJONG1.GetValue().ToString(),
                sDATE.ToString()
                );

            dt = UP_DataTableSumRow(this.DbConnector.ExecuteDataTable());

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYUSME028R();

                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

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
            string sSOSOK = "";

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
                    table.Rows[i]["TMSOSOK"] = table.Rows[i - 1]["TMSOSOK"].ToString();
                    table.Rows[i]["CDDESC1"] = "거래처별 소계";

                    table.Rows[i]["TMHANGCHA"] = "";
                    table.Rows[i]["IHJAKENDAT"] = "";
                    table.Rows[i]["VNSANGHO"] = "";
                    table.Rows[i]["TMSTARTIL"] = "";
                    table.Rows[i]["TMENDIL"] = "";
                    table.Rows[i]["TMILSU"] = 0;
                    table.Rows[i]["TITLE_DATE"] = "";

                    sVNSANGHO = " VNSANGHO = '" + table.Rows[i - 1]["VNSANGHO"].ToString() + "'";

                    table.Rows[i]["TMBOKAMT"] = Get_Numeric(table.Compute("SUM(TMBOKAMT)", sVNSANGHO).ToString());
                    table.Rows[i]["TMHWAKQTY"] = Get_Numeric(table.Compute("SUM(TMHWAKQTY)", sVNSANGHO).ToString());

                    nNum = nNum + 1;
                    i = i + 1;

                }
            }

            row = table.NewRow();
            table.Rows.InsertAt(row, i);

            // 소 계 이름 넣기
            table.Rows[i]["TMSOSOK"] = table.Rows[i - 1]["TMSOSOK"].ToString();
            table.Rows[i]["CDDESC1"] = "거래처별 소계";

            table.Rows[i]["TMHANGCHA"] = "";
            table.Rows[i]["IHJAKENDAT"] = "";
            table.Rows[i]["VNSANGHO"] = "";
            table.Rows[i]["TMSTARTIL"] = "";
            table.Rows[i]["TMENDIL"] = "";
            table.Rows[i]["TMILSU"] = 0;
            table.Rows[i]["TITLE_DATE"] = "";

            sVNSANGHO = " VNSANGHO = '" + table.Rows[i - 1]["VNSANGHO"].ToString() + "'";

            table.Rows[i]["TMBOKAMT"] = Get_Numeric(table.Compute("SUM(TMBOKAMT)", sVNSANGHO).ToString());
            table.Rows[i]["TMHWAKQTY"] = Get_Numeric(table.Compute("SUM(TMHWAKQTY)", sVNSANGHO).ToString());


            i = i + 1;

            row = table.NewRow();
            table.Rows.InsertAt(row, i);

            // 소 계 이름 넣기
            table.Rows[i]["TMSOSOK"] = table.Rows[i - 1]["TMSOSOK"].ToString();
            table.Rows[i]["CDDESC1"] = "총 계";

            table.Rows[i]["TMHANGCHA"] = "";
            table.Rows[i]["IHJAKENDAT"] = "";
            table.Rows[i]["VNSANGHO"] = "";
            table.Rows[i]["TMSTARTIL"] = "";
            table.Rows[i]["TMENDIL"] = "";
            table.Rows[i]["TMILSU"] = 0;
            table.Rows[i]["TITLE_DATE"] = "";

            sSOSOK = " TMSOSOK = '" + table.Rows[i - 1]["TMSOSOK"].ToString() + "'";
            sSOSOK = sSOSOK + "AND CDDESC1 = '거래처별 소계'";

            table.Rows[i]["TMBOKAMT"] = Get_Numeric(table.Compute("SUM(TMBOKAMT)", sSOSOK).ToString());
            table.Rows[i]["TMHWAKQTY"] = Get_Numeric(table.Compute("SUM(TMHWAKQTY)", sSOSOK).ToString());

            return table;
        }
        #endregion
    }
}