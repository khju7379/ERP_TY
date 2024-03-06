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
    public partial class TYUSME024P : TYBase
    {
        #region Description : 페이지 로드
        public TYUSME024P()
        {
            InitializeComponent();
        }

        private void TYUSME024P_Load(object sender, System.EventArgs e)
        {
            this.SetFocus(this.CBH01_STHANGCHA.CodeText);
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_95EB0545",
                this.CBH01_STHANGCHA.GetValue().ToString(),
                this.CBH01_EDHANGCHA.GetValue().ToString(),
                this.CBH01_GGOKJONG.GetValue().ToString(),
                this.CBH01_IHSOSOK.GetValue().ToString()
                );

            dt = UP_DataTableSumRow(this.DbConnector.ExecuteDataTable());

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYUSME024R();

                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
        }
        #endregion

        #region Description : 합 계
        private DataTable UP_DataTableSumRow(DataTable dt)
        {
            double dChoBLhap = 0;
            double dChoHwakhap = 0;
            double dCheBLhap = 0;
            double dCheHwakhap = 0;

            int iDTime = 0;
            int iHTime = 0;
            int iMTime = 0;

            int iWorkTime = 0;

            DataTable table = new DataTable();

            table = dt;

            string sLTGUBUN = "";

            DataRow row;
            int nNum = table.Rows.Count;
            int i = 0;

            for (i = 1; i < nNum; i++)
            {
                if (table.Rows[i - 1]["TMSOSOK"].ToString() != table.Rows[i]["TMSOSOK"].ToString())
                {
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);
                    // 합 계 이름 넣기
                    table.Rows[i]["TMHANGCHA"] = " 소   계 ";
                    table.Rows[i]["TMGOKJONG"] = "";
                    table.Rows[i]["TMSOSOK"] = table.Rows[i - 1]["TMSOSOK"].ToString();

                    sLTGUBUN = " TMSOSOK = '" + table.Rows[i - 1]["TMSOSOK"].ToString() + "'";

                    table.Rows[i]["TMBLQTY"] = Get_Numeric(table.Compute("SUM(TMBLQTY)", sLTGUBUN).ToString());
                    table.Rows[i]["TMHWAKQTY"] = Get_Numeric(table.Compute("SUM(TMHWAKQTY)", sLTGUBUN).ToString());


                    if (Convert.ToDouble(table.Rows[i]["TMBLQTY"].ToString()) > 0 &&
                        Convert.ToDouble(table.Rows[i]["TMHWAKQTY"].ToString()) > 0)
                    {
                        table.Rows[i]["TMGAMQTY"] = String.Format("{0,9:N3}", Convert.ToDouble(100 - (Convert.ToDouble(Get_Numeric(table.Rows[i]["TMHWAKQTY"].ToString())) * 100) /
                            Convert.ToDouble(Get_Numeric(table.Rows[i]["TMBLQTY"].ToString()))));
                    }
                    else
                    {
                        table.Rows[i]["TMGAMQTY"] = "0";
                    }
                    table.Rows[i]["TMLTSAVEGN"] = "(조  출)";

                    sLTGUBUN = " LTGUBUN = '1' AND ";
                    sLTGUBUN = sLTGUBUN + " TMSOSOK = '" + table.Rows[i - 1]["TMSOSOK"].ToString() + "'";
                    iDTime = Convert.ToInt16(Get_Numeric(table.Compute("SUM(TMLTSAVED)", sLTGUBUN).ToString()));
                    iHTime = Convert.ToInt16(Get_Numeric(table.Compute("SUM(TMLTSAVEH)", sLTGUBUN).ToString()));
                    iMTime = Convert.ToInt16(Get_Numeric(table.Compute("SUM(TMLTSAVEM)", sLTGUBUN).ToString()));

                    if (iMTime > 59)
                    {
                        iWorkTime = Convert.ToInt16(UP_DotDelete(Convert.ToString(iMTime / 60)));

                        iMTime = iMTime - (iWorkTime * 60);

                        iHTime = iHTime + iWorkTime;
                    }

                    if (iHTime > 23)
                    {
                        iWorkTime = Convert.ToInt16(UP_DotDelete(Convert.ToString(iHTime / 24)));

                        iHTime = iHTime - (iWorkTime * 24);

                        iDTime = iDTime + iWorkTime;
                    }

                    table.Rows[i]["TMLTSAVE"] = Convert.ToString(iDTime) + "D " + Convert.ToString(iHTime) + "H " + Convert.ToString(iMTime) + "M";


                    table.Rows[i]["TMLTSAVED"] = Convert.ToString(iDTime);
                    table.Rows[i]["TMLTSAVEH"] = Convert.ToString(iHTime);
                    table.Rows[i]["TMLTSAVEM"] = Convert.ToString(iMTime);

                    sLTGUBUN = "LTGUBUN = '1' AND ";
                    sLTGUBUN = sLTGUBUN + " TMSOSOK = '" + table.Rows[i - 1]["TMSOSOK"].ToString() + "'";

                    table.Rows[i]["TMLTTOTSAVE"] = Get_Numeric(table.Compute("SUM(TMLTTOTSAVE)", sLTGUBUN).ToString());

                    dChoBLhap = dChoBLhap + Convert.ToDouble(table.Rows[i]["TMBLQTY"].ToString());
                    dChoHwakhap = dChoHwakhap + Convert.ToDouble(table.Rows[i]["TMHWAKQTY"].ToString());

                    nNum = nNum + 1;
                    i = i + 1;

                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);
                    // 합 계 이름 넣기
                    table.Rows[i]["TMHANGCHA"] = " ";
                    table.Rows[i]["TMGOKJONG"] = "";
                    table.Rows[i]["TMSOSOK"] = table.Rows[i - 1]["TMSOSOK"].ToString();

                    table.Rows[i]["TMBLQTY"] = "0";
                    table.Rows[i]["TMHWAKQTY"] = "0";

                    table.Rows[i]["TMGAMQTY"] = "0";

                    table.Rows[i]["TMLTSAVEGN"] = "(체  선)";

                    sLTGUBUN = "LTGUBUN = '2' AND ";
                    sLTGUBUN = sLTGUBUN + " TMSOSOK = '" + table.Rows[i - 1]["TMSOSOK"].ToString() + "'";
                    iDTime = Convert.ToInt16(Get_Numeric(table.Compute("SUM(TMLTSAVED)", sLTGUBUN).ToString()));
                    iHTime = Convert.ToInt16(Get_Numeric(table.Compute("SUM(TMLTSAVEH)", sLTGUBUN).ToString()));
                    iMTime = Convert.ToInt16(Get_Numeric(table.Compute("SUM(TMLTSAVEM)", sLTGUBUN).ToString()));

                    if (iMTime > 59)
                    {
                        iWorkTime = Convert.ToInt16(UP_DotDelete(Convert.ToString(iMTime / 60)));

                        iMTime = iMTime - (iWorkTime * 60);

                        iHTime = iHTime + iWorkTime;
                    }

                    if (iHTime > 23)
                    {
                        iWorkTime = Convert.ToInt16(UP_DotDelete(Convert.ToString(iHTime / 24)));

                        iHTime = iHTime - (iWorkTime * 24);

                        iDTime = iDTime + iWorkTime;
                    }

                    table.Rows[i]["TMLTSAVE"] = Convert.ToString(iDTime) + "D " + Convert.ToString(iHTime) + "H " + Convert.ToString(iMTime) + "M";

                    table.Rows[i]["TMLTSAVED"] = Convert.ToString(iDTime);
                    table.Rows[i]["TMLTSAVEH"] = Convert.ToString(iHTime);
                    table.Rows[i]["TMLTSAVEM"] = Convert.ToString(iMTime);

                    sLTGUBUN = "LTGUBUN = '2' AND ";
                    sLTGUBUN = sLTGUBUN + " TMSOSOK = '" + table.Rows[i - 1]["TMSOSOK"].ToString() + "'";

                    table.Rows[i]["TMLTTOTSAVE"] = Get_Numeric(table.Compute("SUM(TMLTTOTSAVE)", sLTGUBUN).ToString());

                    dCheBLhap = dCheBLhap + Convert.ToDouble(table.Rows[i]["TMBLQTY"].ToString());
                    dCheHwakhap = dCheHwakhap + Convert.ToDouble(table.Rows[i]["TMHWAKQTY"].ToString());

                    nNum = nNum + 1;
                    i = i + 1;

                }
            }

            row = table.NewRow();
            table.Rows.InsertAt(row, i);
            // 합 계 이름 넣기
            table.Rows[i]["TMHANGCHA"] = " 소   계 ";
            table.Rows[i]["TMGOKJONG"] = "";
            table.Rows[i]["TMSOSOK"] = table.Rows[i - 1]["TMSOSOK"].ToString();

            sLTGUBUN = " TMSOSOK = '" + table.Rows[i - 1]["TMSOSOK"].ToString() + "'";

            table.Rows[i]["TMBLQTY"] = Get_Numeric(table.Compute("SUM(TMBLQTY)", sLTGUBUN).ToString());
            table.Rows[i]["TMHWAKQTY"] = Get_Numeric(table.Compute("SUM(TMHWAKQTY)", sLTGUBUN).ToString());

            if (Convert.ToDouble(table.Rows[i]["TMBLQTY"].ToString()) > 0 &&
                Convert.ToDouble(table.Rows[i]["TMHWAKQTY"].ToString()) > 0)
            {
                table.Rows[i]["TMGAMQTY"] = String.Format("{0,9:N3}", Convert.ToDouble(100 - (Convert.ToDouble(Get_Numeric(table.Rows[i]["TMHWAKQTY"].ToString())) * 100) /
                    Convert.ToDouble(Get_Numeric(table.Rows[i]["TMBLQTY"].ToString()))));
            }
            else
            {
                table.Rows[i]["TMGAMQTY"] = "0";
            }
            table.Rows[i]["TMLTSAVEGN"] = "(조  출)";

            sLTGUBUN = "LTGUBUN = '1' AND ";
            sLTGUBUN = sLTGUBUN + " TMSOSOK = '" + table.Rows[i - 1]["TMSOSOK"].ToString() + "'";
            iDTime = Convert.ToInt16(Get_Numeric(table.Compute("SUM(TMLTSAVED)", sLTGUBUN).ToString()));
            iHTime = Convert.ToInt16(Get_Numeric(table.Compute("SUM(TMLTSAVEH)", sLTGUBUN).ToString()));
            iMTime = Convert.ToInt16(Get_Numeric(table.Compute("SUM(TMLTSAVEM)", sLTGUBUN).ToString()));

            if (iMTime > 59)
            {
                iWorkTime = Convert.ToInt16(UP_DotDelete(Convert.ToString(iMTime / 60)));

                iMTime = iMTime - (iWorkTime * 60);

                iHTime = iHTime + iWorkTime;
            }

            if (iHTime > 23)
            {
                iWorkTime = Convert.ToInt16(UP_DotDelete(Convert.ToString(iHTime / 24)));

                iHTime = iHTime - (iWorkTime * 24);

                iDTime = iDTime + iWorkTime;
            }

            table.Rows[i]["TMLTSAVE"] = Convert.ToString(iDTime) + "D " + Convert.ToString(iHTime) + "H " + Convert.ToString(iMTime) + "M";

            table.Rows[i]["TMLTSAVED"] = Convert.ToString(iDTime);
            table.Rows[i]["TMLTSAVEH"] = Convert.ToString(iHTime);
            table.Rows[i]["TMLTSAVEM"] = Convert.ToString(iMTime);

            sLTGUBUN = "LTGUBUN = '1' AND ";
            sLTGUBUN = sLTGUBUN + " TMSOSOK = '" + table.Rows[i - 1]["TMSOSOK"].ToString() + "'";

            table.Rows[i]["TMLTTOTSAVE"] = Get_Numeric(table.Compute("SUM(TMLTTOTSAVE)", sLTGUBUN).ToString());

            dChoBLhap = dChoBLhap + Convert.ToDouble(table.Rows[i]["TMBLQTY"].ToString());
            dChoHwakhap = dChoHwakhap + Convert.ToDouble(table.Rows[i]["TMHWAKQTY"].ToString());

            nNum = nNum + 1;
            i = i + 1;

            row = table.NewRow();
            table.Rows.InsertAt(row, i);
            // 합 계 이름 넣기
            table.Rows[i]["TMHANGCHA"] = " ";
            table.Rows[i]["TMGOKJONG"] = "";
            table.Rows[i]["TMSOSOK"] = table.Rows[i - 1]["TMSOSOK"].ToString();


            table.Rows[i]["TMBLQTY"] = "0";
            table.Rows[i]["TMHWAKQTY"] = "0";

            table.Rows[i]["TMGAMQTY"] = "0";

            table.Rows[i]["TMLTSAVEGN"] = "(체  선)";

            sLTGUBUN = "LTGUBUN = '2' AND ";
            sLTGUBUN = sLTGUBUN + " TMSOSOK = '" + table.Rows[i - 1]["TMSOSOK"].ToString() + "'";

            iDTime = Convert.ToInt16(Get_Numeric(table.Compute("SUM(TMLTSAVED)", sLTGUBUN).ToString()));
            iHTime = Convert.ToInt16(Get_Numeric(table.Compute("SUM(TMLTSAVEH)", sLTGUBUN).ToString()));
            iMTime = Convert.ToInt16(Get_Numeric(table.Compute("SUM(TMLTSAVEM)", sLTGUBUN).ToString()));

            if (iMTime > 59)
            {
                iWorkTime = Convert.ToInt16(UP_DotDelete(Convert.ToString(iMTime / 60)));

                iMTime = iMTime - (iWorkTime * 60);

                iHTime = iHTime + iWorkTime;
            }

            if (iHTime > 23)
            {
                iWorkTime = Convert.ToInt16(UP_DotDelete(Convert.ToString(iHTime / 24)));

                iHTime = iHTime - (iWorkTime * 24);

                iDTime = iDTime + iWorkTime;
            }

            table.Rows[i]["TMLTSAVE"] = Convert.ToString(iDTime) + "D " + Convert.ToString(iHTime) + "H " + Convert.ToString(iMTime) + "M";

            table.Rows[i]["TMLTSAVED"] = Convert.ToString(iDTime);
            table.Rows[i]["TMLTSAVEH"] = Convert.ToString(iHTime);
            table.Rows[i]["TMLTSAVEM"] = Convert.ToString(iMTime);

            sLTGUBUN = "LTGUBUN = '2' AND ";
            sLTGUBUN = sLTGUBUN + " TMSOSOK = '" + table.Rows[i - 1]["TMSOSOK"].ToString() + "'";

            table.Rows[i]["TMLTTOTSAVE"] = Get_Numeric(table.Compute("SUM(TMLTTOTSAVE)", sLTGUBUN).ToString());

            dCheBLhap = dCheBLhap + Convert.ToDouble(table.Rows[i]["TMBLQTY"].ToString());
            dCheHwakhap = dCheHwakhap + Convert.ToDouble(table.Rows[i]["TMHWAKQTY"].ToString());

            nNum = nNum + 1;
            i = i + 1;


            /******* 마지막 거래처의 대한 로우 생성*****/
            row = table.NewRow();
            table.Rows.InsertAt(row, i);
            // 합 계 이름 넣기
            table.Rows[i]["TMHANGCHA"] = " 합   계 ";
            table.Rows[i]["TMGOKJONG"] = "";
            table.Rows[i]["TMSOSOK"] = table.Rows[i - 1]["TMSOSOK"].ToString();

            sLTGUBUN = "TMGOKJONG <> '' ";

            table.Rows[i]["TMBLQTY"] = table.Compute("SUM(TMBLQTY)", sLTGUBUN).ToString();
            table.Rows[i]["TMHWAKQTY"] = table.Compute("SUM(TMHWAKQTY)", sLTGUBUN).ToString();

            sLTGUBUN = "LTGUBUN = '1' ";

            if (dChoHwakhap > 0 && dChoBLhap > 0)
            {
                table.Rows[i]["TMGAMQTY"] = String.Format("{0,9:N3}", Convert.ToDouble(100 - ((dChoHwakhap * 100) / dChoBLhap)));
            }
            else
            {
                table.Rows[i]["TMGAMQTY"] = "0";
            }

            table.Rows[i]["TMLTSAVEGN"] = "(조  출)";

            iDTime = Convert.ToInt16(Get_Numeric(table.Compute("SUM(TMLTSAVED)", sLTGUBUN).ToString()));
            iHTime = Convert.ToInt16(Get_Numeric(table.Compute("SUM(TMLTSAVEH)", sLTGUBUN).ToString()));
            iMTime = Convert.ToInt16(Get_Numeric(table.Compute("SUM(TMLTSAVEM)", sLTGUBUN).ToString()));

            if (iMTime > 59)
            {
                iWorkTime = Convert.ToInt16(Convert.ToString(UP_DotDelete(Convert.ToString(iMTime / 60))));

                iMTime = iMTime - (iWorkTime * 60);

                iHTime = iHTime + iWorkTime;
            }

            if (iHTime > 23)
            {
                iWorkTime = Convert.ToInt16(UP_DotDelete(Convert.ToString(iHTime / 24)));

                iHTime = iHTime - (iWorkTime * 24);

                iDTime = iDTime + iWorkTime;
            }

            table.Rows[i]["TMLTSAVE"] = Convert.ToString(iDTime) + "D " + Convert.ToString(iHTime) + "H " + Convert.ToString(iMTime) + "M";

            table.Rows[i]["TMLTSAVED"] = Convert.ToString(iDTime);
            table.Rows[i]["TMLTSAVEH"] = Convert.ToString(iHTime);
            table.Rows[i]["TMLTSAVEM"] = Convert.ToString(iMTime);

            table.Rows[i]["TMLTTOTSAVE"] = Get_Numeric(table.Compute("SUM(TMLTTOTSAVE)", sLTGUBUN).ToString());


            row = table.NewRow();
            table.Rows.InsertAt(row, i + 1);
            // 합 계 이름 넣기
            table.Rows[i + 1]["TMHANGCHA"] = " ";
            table.Rows[i + 1]["TMGOKJONG"] = "";
            table.Rows[i + 1]["TMSOSOK"] = table.Rows[i - 1]["TMSOSOK"].ToString();

            sLTGUBUN = "LTGUBUN = '2' ";

            table.Rows[i + 1]["TMBLQTY"] = "0";
            table.Rows[i + 1]["TMHWAKQTY"] = "0";
            table.Rows[i + 1]["TMGAMQTY"] = "0";

            table.Rows[i + 1]["TMLTSAVEGN"] = "(체  선)";

            iDTime = Convert.ToInt16(Get_Numeric(table.Compute("SUM(TMLTSAVED)", sLTGUBUN).ToString()));
            iHTime = Convert.ToInt16(Get_Numeric(table.Compute("SUM(TMLTSAVEH)", sLTGUBUN).ToString()));
            iMTime = Convert.ToInt16(Get_Numeric(table.Compute("SUM(TMLTSAVEM)", sLTGUBUN).ToString()));

            if (iMTime > 59)
            {
                iWorkTime = Convert.ToInt16(UP_DotDelete(Convert.ToString(iMTime / 60)));

                iMTime = iMTime - (iWorkTime * 60);

                iHTime = iHTime + iWorkTime;
            }

            if (iHTime > 23)
            {
                iWorkTime = Convert.ToInt16(UP_DotDelete(Convert.ToString(iHTime / 24)));

                iHTime = iHTime - (iWorkTime * 24);

                iDTime = iDTime + iWorkTime;
            }

            table.Rows[i + 1]["TMLTSAVE"] = Convert.ToString(iDTime) + "D " + Convert.ToString(iHTime) + "H " + Convert.ToString(iMTime) + "M";

            table.Rows[i + 1]["TMLTSAVED"] = Convert.ToString(iDTime);
            table.Rows[i + 1]["TMLTSAVEH"] = Convert.ToString(iHTime);
            table.Rows[i + 1]["TMLTSAVEM"] = Convert.ToString(iMTime);

            table.Rows[i + 1]["TMLTTOTSAVE"] = Get_Numeric(table.Compute("SUM(TMLTTOTSAVE)", sLTGUBUN).ToString());

            return table;
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