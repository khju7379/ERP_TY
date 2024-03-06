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
    public partial class TYUSME035S : TYBase
    {
        #region Description : 페이지 로드
        public TYUSME035S()
        {
            InitializeComponent();
        }

        private void TYUSME035S_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_US_9638Q682.Initialize();

            this.DTP01_STDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_9638Q682.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.Attach
                (
                "TY_P_US_95MD5602",
                Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                this.CBH01_GHWAJU.GetValue().ToString()
                );

            dt = UP_DataTableSumRow(this.DbConnector.ExecuteDataTable());

            this.FPS91_TY_S_US_9638Q682.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_US_9638Q682.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_US_9638Q682.GetValue(i, "VNSANGHO").ToString() == "매출별 소계")
                {
                    // 특정 ROW 글자 크기 변경
                    this.FPS91_TY_S_US_9638Q682.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);
                    this.FPS91_TY_S_US_9638Q682.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 194);
                }

                if (this.FPS91_TY_S_US_9638Q682.GetValue(i, "MEDESC1").ToString() == "총 계")
                {
                    // 특정 ROW 글자 크기 변경
                    this.FPS91_TY_S_US_9638Q682.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);
                    this.FPS91_TY_S_US_9638Q682.ActiveSheet.Rows[i].BackColor = Color.SkyBlue;
                }
            }
        }
        #endregion

        #region Description : 합 계
        private DataTable UP_DataTableSumRow(DataTable dt)
        {
            DataTable table = new DataTable();

            table = dt;


            string sMEDESC1 = "";

            double dMIDANGAMT = 0;
            double dMIDANGVAT = 0;
            double dHAP = 0;

            DataRow row;
            int nNum = table.Rows.Count;
            int i = 0;

            for (i = 1; i < nNum; i++)
            {
                if (table.Rows[i - 1]["MIYYMM"].ToString() != table.Rows[i]["MIYYMM"].ToString())
                {
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    // 매출별 소계 이름 넣기
                    table.Rows[i]["MIYYMM"] = table.Rows[i - 1]["MIYYMM"].ToString();
                    table.Rows[i]["MEDESC1"] = table.Rows[i - 1]["MEDESC1"].ToString();
                    table.Rows[i]["VNSANGHO"] = "매출별 소계";

                    table.Rows[i]["MIMAECH"] = "";
                    table.Rows[i]["MIHWAJU"] = "";

                    sMEDESC1 = " MIYYMM        = '" + table.Rows[i - 1]["MIYYMM"].ToString() + "'";
                    sMEDESC1 = sMEDESC1 + " AND MEDESC1   = '" + table.Rows[i - 1]["MEDESC1"].ToString() + "'";

                    dMIDANGAMT = dMIDANGAMT + double.Parse(Get_Numeric(table.Compute("SUM(MIDANGAMT)", sMEDESC1).ToString()));
                    dMIDANGVAT = dMIDANGVAT + double.Parse(Get_Numeric(table.Compute("SUM(MIDANGVAT)", sMEDESC1).ToString()));
                    dHAP = dHAP + double.Parse(Get_Numeric(table.Compute("SUM(HAP)", sMEDESC1).ToString()));

                    table.Rows[i]["MIDANGAMT"] = Get_Numeric(table.Compute("SUM(MIDANGAMT)", sMEDESC1).ToString());
                    table.Rows[i]["MIDANGVAT"] = Get_Numeric(table.Compute("SUM(MIDANGVAT)", sMEDESC1).ToString());
                    table.Rows[i]["HAP"] = Get_Numeric(table.Compute("SUM(HAP)", sMEDESC1).ToString());
                    table.Rows[i]["MIMISUAMT"] = Get_Numeric(table.Compute("SUM(MIMISUAMT)", sMEDESC1).ToString());

                    nNum = nNum + 1;
                    i = i + 1;

                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    // 총 계 이름 넣기
                    table.Rows[i]["MIYYMM"] = table.Rows[i - 1]["MIYYMM"].ToString();
                    table.Rows[i]["MEDESC1"] = "총 계";

                    table.Rows[i]["MIMAECH"] = "";
                    table.Rows[i]["MIHWAJU"] = "";
                    table.Rows[i]["VNSANGHO"] = "";

                    table.Rows[i]["MIDANGAMT"] = Convert.ToString(dMIDANGAMT);
                    table.Rows[i]["MIDANGVAT"] = Convert.ToString(dMIDANGVAT);
                    table.Rows[i]["HAP"] = Convert.ToString(dHAP);

                    nNum = nNum + 1;
                    i = i + 1;

                    dMIDANGAMT = 0;
                    dMIDANGVAT = 0;
                    dHAP = 0;

                }
                else
                {
                    if (table.Rows[i - 1]["MEDESC1"].ToString() != table.Rows[i]["MEDESC1"].ToString())
                    {
                        row = table.NewRow();
                        table.Rows.InsertAt(row, i);

                        // 매출별 소계 이름 넣기
                        table.Rows[i]["MIYYMM"] = table.Rows[i - 1]["MIYYMM"].ToString();
                        table.Rows[i]["MEDESC1"] = table.Rows[i - 1]["MEDESC1"].ToString();
                        table.Rows[i]["VNSANGHO"] = "매출별 소계";

                        table.Rows[i]["MIMAECH"] = "";
                        table.Rows[i]["MIHWAJU"] = "";

                        sMEDESC1 = " MIYYMM        = '" + table.Rows[i - 1]["MIYYMM"].ToString() + "'";
                        sMEDESC1 = sMEDESC1 + " AND MEDESC1   = '" + table.Rows[i - 1]["MEDESC1"].ToString() + "'";

                        dMIDANGAMT = dMIDANGAMT + double.Parse(Get_Numeric(table.Compute("SUM(MIDANGAMT)", sMEDESC1).ToString()));
                        dMIDANGVAT = dMIDANGVAT + double.Parse(Get_Numeric(table.Compute("SUM(MIDANGVAT)", sMEDESC1).ToString()));
                        dHAP = dHAP + double.Parse(Get_Numeric(table.Compute("SUM(HAP)", sMEDESC1).ToString()));

                        table.Rows[i]["MIDANGAMT"] = Get_Numeric(table.Compute("SUM(MIDANGAMT)", sMEDESC1).ToString());
                        table.Rows[i]["MIDANGVAT"] = Get_Numeric(table.Compute("SUM(MIDANGVAT)", sMEDESC1).ToString());
                        table.Rows[i]["HAP"] = Get_Numeric(table.Compute("SUM(HAP)", sMEDESC1).ToString());

                        nNum = nNum + 1;
                        i = i + 1;
                    }
                }
            }

            row = table.NewRow();
            table.Rows.InsertAt(row, i);

            // 매출별 소계 이름 넣기
            table.Rows[i]["MIYYMM"] = table.Rows[i - 1]["MIYYMM"].ToString();
            table.Rows[i]["MEDESC1"] = table.Rows[i - 1]["MEDESC1"].ToString();
            table.Rows[i]["VNSANGHO"] = "매출별 소계";

            table.Rows[i]["MIMAECH"] = "";
            table.Rows[i]["MIHWAJU"] = "";

            sMEDESC1 = " MIYYMM        = '" + table.Rows[i - 1]["MIYYMM"].ToString() + "'";
            sMEDESC1 = sMEDESC1 + " AND MEDESC1   = '" + table.Rows[i - 1]["MEDESC1"].ToString() + "'";

            dMIDANGAMT = dMIDANGAMT + double.Parse(Get_Numeric(table.Compute("SUM(MIDANGAMT)", sMEDESC1).ToString()));
            dMIDANGVAT = dMIDANGVAT + double.Parse(Get_Numeric(table.Compute("SUM(MIDANGVAT)", sMEDESC1).ToString()));
            dHAP = dHAP + double.Parse(Get_Numeric(table.Compute("SUM(HAP)", sMEDESC1).ToString()));

            table.Rows[i]["MIDANGAMT"] = Get_Numeric(table.Compute("SUM(MIDANGAMT)", sMEDESC1).ToString());
            table.Rows[i]["MIDANGVAT"] = Get_Numeric(table.Compute("SUM(MIDANGVAT)", sMEDESC1).ToString());
            table.Rows[i]["HAP"] = Get_Numeric(table.Compute("SUM(HAP)", sMEDESC1).ToString());
            table.Rows[i]["MIMISUAMT"] = Get_Numeric(table.Compute("SUM(MIMISUAMT)", sMEDESC1).ToString());

            nNum = nNum + 1;
            i = i + 1;

            row = table.NewRow();
            table.Rows.InsertAt(row, i);

            // 총 계 이름 넣기
            table.Rows[i]["MIYYMM"] = table.Rows[i - 1]["MIYYMM"].ToString();
            table.Rows[i]["MEDESC1"] = "총 계";

            table.Rows[i]["MIMAECH"] = "";
            table.Rows[i]["MIHWAJU"] = "";
            table.Rows[i]["VNSANGHO"] = "";

            table.Rows[i]["MIDANGAMT"] = Convert.ToString(dMIDANGAMT);
            table.Rows[i]["MIDANGVAT"] = Convert.ToString(dMIDANGVAT);
            table.Rows[i]["HAP"] = Convert.ToString(dHAP);

            return table;
        }
        #endregion
    }
}