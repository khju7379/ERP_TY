using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.US00
{
    /// <summary>
    /// 후생복지비 지출명세서 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.04.03 13:47
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_943DO229 : 후생복지비 지불명세서 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_94ABO331 : 후생복지비 지불명세서 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  CHHANGCHA : 항 차
    ///  INQOPTION : 조회구분
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYUSME065S : TYBase
    {
        #region Description : 페이지 로드
        public TYUSME065S()
        {
            InitializeComponent();
        }

        private void TYUSME065S_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_US_94ABO331.Visible = true;

            this.DTP01_GDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_GDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sSTHANGCHA = string.Empty;
            string sEDHANGCHA = string.Empty;

            string sProcedure = string.Empty;

            DataTable dt = new DataTable();

            if (this.CBH01_STHANGCHA.GetValue().ToString() != "" && this.CBH01_EDHANGCHA.GetValue().ToString() != "")
            {
                sSTHANGCHA = this.CBH01_STHANGCHA.GetValue().ToString();
                sEDHANGCHA = this.CBH01_EDHANGCHA.GetValue().ToString();
            }
            else
            {
                // 소급분 항차 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_97CAL044", Get_Date(this.DTP01_GDATE.GetValue().ToString()),
                                                            this.CBO01_GMEGUBUN.GetValue().ToString(),
                                                            this.CBH01_GHWAJU.GetValue().ToString()
                                                            );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sSTHANGCHA = dt.Rows[0]["STHANGCHA"].ToString();
                    sEDHANGCHA = dt.Rows[0]["EDHANGCHA"].ToString();
                }
            }

            this.UP_Set_TitleSetting();

            this.FPS91_TY_S_US_94ABO331.Initialize();

            if (this.CBO01_GGUBUN.GetText().ToString() == "모선별")
            {
                sProcedure = "TY_P_US_94ABK330";
            }
            else
            {
                sProcedure = "TY_P_US_94ADK332";
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach(sProcedure, sSTHANGCHA.ToString(),
                                                sEDHANGCHA.ToString(),
                                                this.CBO01_GGUBUN.GetValue().ToString(),
                                                Get_Date(this.DTP01_GDATE.GetValue().ToString()),
                                                sSTHANGCHA.ToString(),
                                                sEDHANGCHA.ToString(),
                                                this.CBO01_GMEGUBUN.GetValue().ToString(),
                                                this.CBH01_GHWAJU.GetValue().ToString()
                                                );

            this.FPS91_TY_S_US_94ABO331.SetValue(UP_InsertRowTotal(UP_Get_Convert(this.DbConnector.ExecuteDataTable())));

            if (this.FPS91_TY_S_US_94ABO331.CurrentRowCount > 0)
            {
                this.SetSpreadSumRow(this.FPS91_TY_S_US_94ABO331, "GUBN3", "[소  계]", SumRowType.SubTotal);
                this.SetSpreadSumRow(this.FPS91_TY_S_US_94ABO331, "GUBN3", "[합  계]", SumRowType.Total);
            }
        }
        #endregion

        #region Description :  조회(귀속부서별) 그리드 타이트 조정 함수
        private void UP_Set_TitleSetting()
        {
            if (this.CBO01_GGUBUN.GetText().ToString() == "모선별")
            {
                this.FPS91_TY_S_US_94ABO331_Sheet1.ColumnHeader.Cells[0, 0].Value = "항    차";
                this.FPS91_TY_S_US_94ABO331_Sheet1.ColumnHeader.Cells[0, 1].Value = "모 선 명";
                this.FPS91_TY_S_US_94ABO331_Sheet1.ColumnHeader.Cells[0, 2].Value = "입 항 일";
                this.FPS91_TY_S_US_94ABO331_Sheet1.ColumnHeader.Cells[0, 3].Value = "화 주 명";
            }
            else
            {
                this.FPS91_TY_S_US_94ABO331_Sheet1.ColumnHeader.Cells[0, 0].Value = "화 주 명";
                this.FPS91_TY_S_US_94ABO331_Sheet1.ColumnHeader.Cells[0, 1].Value = "항    차";
                this.FPS91_TY_S_US_94ABO331_Sheet1.ColumnHeader.Cells[0, 2].Value = "모 선 명";
                this.FPS91_TY_S_US_94ABO331_Sheet1.ColumnHeader.Cells[0, 3].Value = "입 항 일";
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sSTHANGCHA = string.Empty;
            string sEDHANGCHA = string.Empty;

            string sProcedure = string.Empty;

            DataTable dt = new DataTable();

            if (this.CBH01_STHANGCHA.GetValue().ToString() != "" && this.CBH01_EDHANGCHA.GetValue().ToString() != "")
            {
                sSTHANGCHA = this.CBH01_STHANGCHA.GetValue().ToString();
                sEDHANGCHA = this.CBH01_EDHANGCHA.GetValue().ToString();
            }
            else
            {
                // 소급분 항차 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_97CAL044", Get_Date(this.DTP01_GDATE.GetValue().ToString()),
                                                            this.CBO01_GMEGUBUN.GetValue().ToString(),
                                                            this.CBH01_GHWAJU.GetValue().ToString()
                                                            );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sSTHANGCHA = dt.Rows[0]["STHANGCHA"].ToString();
                    sEDHANGCHA = dt.Rows[0]["EDHANGCHA"].ToString();
                }
            }

            if (this.CBO01_GGUBUN.GetText().ToString() == "모선별")
            {
                sProcedure = "TY_P_US_94ABK330";
            }
            else
            {
                sProcedure = "TY_P_US_94ADK332";
            }


            this.DbConnector.CommandClear();
            this.DbConnector.Attach(sProcedure, sSTHANGCHA.ToString(),
                                                sEDHANGCHA.ToString(),
                                                this.CBO01_GGUBUN.GetValue().ToString(),
                                                Get_Date(this.DTP01_GDATE.GetValue().ToString()),
                                                sSTHANGCHA.ToString(),
                                                sEDHANGCHA.ToString(),
                                                this.CBO01_GMEGUBUN.GetValue().ToString(),
                                                this.CBH01_GHWAJU.GetValue().ToString()
                                                );

            dt = UP_Get_Convert(this.DbConnector.ExecuteDataTable());

            if (this.CBO01_GGUBUN.GetText().ToString() == "모선별")
            {
                SectionReport rpt = new TYUSME065R1();

                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                SectionReport rpt = new TYUSME065R2();

                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
        }
        #endregion

        #region Description : 데이터 컨버젼
        private DataTable UP_Get_Convert(DataTable Orgdt)
        {
            string sTITLE1 = string.Empty;
            string sTITLE2 = string.Empty;

            string sProcedure = string.Empty;
            string sUTMAECH = string.Empty;

            string sSTHANGCHA = string.Empty;
            string sEDHANGCHA = string.Empty;

            int i = 0;
            int j = 0;

            DataTable dt = new DataTable();

            DataTable retDt = new DataTable();

            DataRow dtRow;

            retDt.Columns.Add("GUBUN1", typeof(System.String));
            retDt.Columns.Add("GUBN1", typeof(System.String));
            retDt.Columns.Add("GUBN2", typeof(System.String));
            retDt.Columns.Add("GUBN3", typeof(System.String));
            retDt.Columns.Add("GUBN4", typeof(System.String));
            retDt.Columns.Add("GUBN5", typeof(System.String));
            retDt.Columns.Add("BLQTY", typeof(System.Decimal));
            retDt.Columns.Add("CHQTY", typeof(System.Decimal));
            retDt.Columns.Add("SGDANGA", typeof(System.Decimal));
            retDt.Columns.Add("SGAMT", typeof(System.Decimal));
            retDt.Columns.Add("SGVAT", typeof(System.Decimal));
            retDt.Columns.Add("HAP", typeof(System.Decimal));
            retDt.Columns.Add("SGMCDATE", typeof(System.String));
            retDt.Columns.Add("TITLE1", typeof(System.String));
            retDt.Columns.Add("TITLE2", typeof(System.String));

            if (this.CBO01_GMEGUBUN.GetText().ToString() == "하역료")
            {
                if (this.CBO01_GGUBUN.GetText().ToString() == "모선별")
                {
                    sTITLE1 = "모선별 하역료 소급분 정산내역서";
                }
                else
                {
                    sTITLE1 = "화주별 하역료 소급분 정산내역서";
                }
            }
            else
            {
                if (this.CBO01_GGUBUN.GetText().ToString() == "모선별")
                {
                    sTITLE1 = "모선별 시설사용료 소급분 정산내역서";
                }
                else
                {
                    sTITLE1 = "화주별 시설사용료 소급분 정산내역서";
                }
            }

            if (this.CBH01_STHANGCHA.GetValue().ToString() != "" && this.CBH01_EDHANGCHA.GetValue().ToString() != "")
            {
                sSTHANGCHA = this.CBH01_STHANGCHA.GetValue().ToString();
                sEDHANGCHA = this.CBH01_EDHANGCHA.GetValue().ToString();
            }
            else
            {
                // 소급분 항차 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_97CAL044", Get_Date(this.DTP01_GDATE.GetValue().ToString()),
                                                            this.CBO01_GMEGUBUN.GetValue().ToString(),
                                                            this.CBH01_GHWAJU.GetValue().ToString()
                                                            );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sSTHANGCHA = dt.Rows[0]["STHANGCHA"].ToString();
                    sEDHANGCHA = dt.Rows[0]["EDHANGCHA"].ToString();
                }
            }

            sTITLE2 = "(" + sSTHANGCHA.ToString() + "~" + sEDHANGCHA.ToString() + ")";


            for (i = 0; i < Orgdt.Rows.Count; i++)
            {
                dtRow = retDt.NewRow();

                dtRow["GUBUN1"] = Orgdt.Rows[i]["GUBUN1"].ToString();
                dtRow["GUBN1"] = Orgdt.Rows[i]["GUBN1"].ToString();
                dtRow["GUBN2"] = Orgdt.Rows[i]["GUBN2"].ToString();
                dtRow["GUBN3"] = Orgdt.Rows[i]["GUBN3"].ToString();
                dtRow["GUBN4"] = Orgdt.Rows[i]["GUBN4"].ToString();
                dtRow["GUBN5"] = Orgdt.Rows[i]["GUBN5"].ToString();
                dtRow["BLQTY"] = Convert.ToDecimal(Orgdt.Rows[i]["BLQTY"].ToString());
                dtRow["CHQTY"] = Convert.ToDecimal(Orgdt.Rows[i]["CHQTY"].ToString());
                dtRow["SGDANGA"] = Convert.ToDecimal(Orgdt.Rows[i]["SGDANGA"].ToString());
                dtRow["SGAMT"] = Convert.ToDecimal(Orgdt.Rows[i]["SGAMT"].ToString());
                dtRow["SGVAT"] = Convert.ToDecimal(Orgdt.Rows[i]["SGVAT"].ToString());
                dtRow["HAP"] = Convert.ToDecimal(Orgdt.Rows[i]["HAP"].ToString());
                dtRow["SGMCDATE"] = Orgdt.Rows[i]["SGMCDATE"].ToString();
                dtRow["TITLE1"] = sTITLE1.ToString();
                dtRow["TITLE2"] = sTITLE2.ToString();

                retDt.Rows.Add(dtRow);

                if (this.CBO01_GGUBUN.GetText().ToString() == "모선별")
                {
                    sProcedure = "TY_P_US_94BFW354";
                }
                else
                {
                    sProcedure = "TY_P_US_94BFV353";
                }

                if (this.CBO01_GMEGUBUN.GetText().ToString() == "하역료")
                {
                    sUTMAECH = "62";
                }
                else
                {
                    sUTMAECH = "63";
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach(sProcedure, sUTMAECH.ToString(),
                                                    Orgdt.Rows[i]["SGJPNO"].ToString()
                                                    );

                dt = this.DbConnector.ExecuteDataTable();

                for (j = 0; j < dt.Rows.Count; j++)
                {
                    dtRow = retDt.NewRow();

                    dtRow["GUBUN1"] = "SOGUB";

                    dtRow["GUBN1"] = dt.Rows[j]["GUBN1"].ToString();
                    dtRow["GUBN2"] = dt.Rows[j]["GUBN2"].ToString();
                    dtRow["GUBN3"] = dt.Rows[j]["GUBN3"].ToString();
                    dtRow["GUBN4"] = dt.Rows[j]["GUBN4"].ToString();
                    dtRow["GUBN5"] = dt.Rows[j]["GUBN5"].ToString();
                    dtRow["BLQTY"] = Convert.ToDecimal(dt.Rows[j]["BLQTY"].ToString());
                    dtRow["CHQTY"] = Convert.ToDecimal(dt.Rows[j]["CHQTY"].ToString());
                    dtRow["SGDANGA"] = Convert.ToDecimal(dt.Rows[j]["SGDANGA"].ToString());
                    dtRow["SGAMT"] = Convert.ToDecimal(dt.Rows[j]["SGAMT"].ToString());
                    dtRow["SGVAT"] = Convert.ToDecimal(dt.Rows[j]["SGVAT"].ToString());
                    dtRow["HAP"] = Convert.ToDecimal(dt.Rows[j]["HAP"].ToString());
                    dtRow["SGMCDATE"] = dt.Rows[j]["SGMCDATE"].ToString();
                    dtRow["TITLE1"] = sTITLE1.ToString();
                    dtRow["TITLE2"] = sTITLE2.ToString();

                    retDt.Rows.Add(dtRow);
                }
            }

            //if (this.CBO01_GGUBUN.GetText().ToString() == "모선별")
            //{
            //    retDt.DefaultView.Sort = "GUBN1, GUBN4";
            //}
            //else
            //{
            //    retDt.DefaultView.Sort = "GUBN1, GUBN2";
            //}

            return retDt;
        }
        #endregion

        #region Description : 소계, 합계 넣기
        private DataTable UP_InsertRowTotal(DataTable dt)
        {
            double dBLQTY = 0;
            double dCHQTY = 0;
            double dSGAMT = 0;
            double dSGVAT = 0;
            double dHAP = 0;

            int i = 0;

            string sFilter = string.Empty;

            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;


            for (i = 1; i < nNum; i++)
            {
                if (table.Rows[i - 1]["GUBN1"].ToString() != table.Rows[i]["GUBN1"].ToString())
                {
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    // 합 계 이름 넣기
                    table.Rows[i]["GUBN3"] = "[소  계]";

                    sFilter = " GUBN1 = '" + table.Rows[i - 1]["GUBN1"].ToString() + "' ";

                    // 확정량
                    table.Rows[i]["BLQTY"] = Convert.ToDouble(table.Compute("SUM(BLQTY)", sFilter).ToString());
                    // 양도/출고량
                    table.Rows[i]["CHQTY"] = Convert.ToDouble(table.Compute("SUM(CHQTY)", sFilter).ToString());

                    // 공급가
                    table.Rows[i]["SGAMT"] = Convert.ToDouble(table.Compute("SUM(SGAMT)", sFilter).ToString());
                    // 부가세
                    table.Rows[i]["SGVAT"] = Convert.ToDouble(table.Compute("SUM(SGVAT)", sFilter).ToString());
                    // 합계
                    table.Rows[i]["HAP"] = Convert.ToDouble(table.Compute("SUM(HAP)", sFilter).ToString());

                    dBLQTY = dBLQTY + Convert.ToDouble(table.Compute("SUM(BLQTY)", sFilter).ToString());
                    dCHQTY = dCHQTY + Convert.ToDouble(table.Compute("SUM(CHQTY)", sFilter).ToString());
                    dSGAMT = dSGAMT + Convert.ToDouble(table.Compute("SUM(SGAMT)", sFilter).ToString());
                    dSGVAT = dSGVAT + Convert.ToDouble(table.Compute("SUM(SGVAT)", sFilter).ToString());
                    dHAP = dHAP + Convert.ToDouble(table.Compute("SUM(HAP)", sFilter).ToString());

                    nNum = nNum + 1;

                    i = i + 1;
                }

            }

            if (nNum > 0)
            {
                row = table.NewRow();
                table.Rows.InsertAt(row, i);

                // 합 계 이름 넣기
                table.Rows[i]["GUBN3"] = "[소  계]";

                sFilter = " GUBN1 = '" + table.Rows[i - 1]["GUBN1"].ToString() + "' ";

                // 확정량
                table.Rows[i]["BLQTY"] = Convert.ToDouble(table.Compute("SUM(BLQTY)", sFilter).ToString());
                // 양도/출고량
                table.Rows[i]["CHQTY"] = Convert.ToDouble(table.Compute("SUM(CHQTY)", sFilter).ToString());

                // 공급가
                table.Rows[i]["SGAMT"] = Convert.ToDouble(table.Compute("SUM(SGAMT)", sFilter).ToString());
                // 부가세
                table.Rows[i]["SGVAT"] = Convert.ToDouble(table.Compute("SUM(SGVAT)", sFilter).ToString());
                // 합계
                table.Rows[i]["HAP"] = Convert.ToDouble(table.Compute("SUM(HAP)", sFilter).ToString());

                dBLQTY = dBLQTY + Convert.ToDouble(table.Compute("SUM(BLQTY)", sFilter).ToString());
                dCHQTY = dCHQTY + Convert.ToDouble(table.Compute("SUM(CHQTY)", sFilter).ToString());
                dSGAMT = dSGAMT + Convert.ToDouble(table.Compute("SUM(SGAMT)", sFilter).ToString());
                dSGVAT = dSGVAT + Convert.ToDouble(table.Compute("SUM(SGVAT)", sFilter).ToString());
                dHAP = dHAP + Convert.ToDouble(table.Compute("SUM(HAP)", sFilter).ToString());

                nNum = nNum + 1;

                i = i + 1;

                /******* 마지막 거래처의 대한 로우 생성*****/
                row = table.NewRow();
                table.Rows.InsertAt(row, i);

                // 합 계 이름 넣기
                table.Rows[i]["GUBN3"] = "[합  계]";

                // 확정량
                table.Rows[i]["BLQTY"] = dBLQTY;
                // 양도/출고량
                table.Rows[i]["CHQTY"] = dCHQTY;

                // 공급가
                table.Rows[i]["SGAMT"] = dSGAMT;
                // 부가세
                table.Rows[i]["SGVAT"] = dSGVAT;
                // 합계
                table.Rows[i]["HAP"] = dHAP;
            }

            return table;
        }
        #endregion   

        #region Description : 소계 넣기
        private DataTable UP_InsertRowSubTotal(DataTable dt)
        {
            int i = 0;

            string sFilter = string.Empty;

            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;

            for (i = 1; i < nNum; i++)
            {
                if (table.Rows[i - 1]["HWYYMM"].ToString() != table.Rows[i]["HWYYMM"].ToString() ||
                    table.Rows[i - 1]["GUBN"].ToString() != table.Rows[i]["GUBN"].ToString()
                    )
                {
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    // 합 계 이름 넣기
                    table.Rows[i]["HWHANGCHANM"] = "[소  계]";

                    sFilter = "  HWYYMM  = " + table.Rows[i - 1]["HWYYMM"].ToString() + " ";
                    sFilter = sFilter + " AND  GUBN  = '" + table.Rows[i - 1]["GUBN"].ToString() + "'";

                    table.Rows[i]["HWYYMM"] = table.Rows[i - 1]["HWYYMM"].ToString();

                    //취급톤수
                    table.Rows[i]["HWWKQTY"] = Convert.ToDouble(table.Compute("SUM(HWWKQTY)", sFilter).ToString());
                    //금액
                    table.Rows[i]["HWHSBJAMT"] = Convert.ToDouble(table.Compute("SUM(HWHSBJAMT)", sFilter).ToString());

                    nNum = nNum + 1;

                    i = i + 1;


                }

            }

            if (nNum > 0)
            {
                row = table.NewRow();
                table.Rows.InsertAt(row, i);

                // 합 계 이름 넣기
                table.Rows[i]["HWHANGCHANM"] = "[소  계]";

                sFilter = "  HWYYMM  = " + table.Rows[i - 1]["HWYYMM"].ToString() + " ";
                sFilter = sFilter + " AND  GUBN  = '" + table.Rows[i - 1]["GUBN"].ToString() + "'";

                table.Rows[i]["HWYYMM"] = table.Rows[i - 1]["HWYYMM"].ToString();

                //취급톤수
                table.Rows[i]["HWWKQTY"] = Convert.ToDouble(table.Compute("SUM(HWWKQTY)", sFilter).ToString());
                //금액
                table.Rows[i]["HWHSBJAMT"] = Convert.ToDouble(table.Compute("SUM(HWHSBJAMT)", sFilter).ToString());

                nNum = nNum + 1;

                i = i + 1;

            }

            return table;
        }
        #endregion



    }
}