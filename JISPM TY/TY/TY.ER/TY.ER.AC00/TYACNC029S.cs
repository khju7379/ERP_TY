using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.AC00
{
    /// <summary>
    /// 영업외손익명세서 출력 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.09.14 09:48
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_29H6N149 : 영업외손익명세서 출력(12년 이전)
    ///  TY_P_AC_29H6N150 : 영업외손익명세서 출력(12년 이후)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_29H77151 : 영업외손익명세서-2012이전
    ///  TY_S_AC_29H78152 : 영업외손익명세서-2012이후
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    ///  GPRTGN : 출력구분
    /// </summary>
    public partial class TYACNC029S : TYBase
    {
        #region Description : 페이지 로드
        public TYACNC029S()
        {
            InitializeComponent();
        }

        private void TYACNC029S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_INQ.ProcessCheck += new TButton.CheckHandler(this.BTN61_INQ_ProcessCheck);
            this.BTN61_PRT.ProcessCheck += new TButton.CheckHandler(this.BTN61_PRT_ProcessCheck);

            this.FPS91_TY_S_AC_29H78152.Visible = true;
            this.FPS91_TY_S_AC_29H77151.Visible = false;

            SetStartingFocus(this.DTP01_GSTYYMM);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sProcedure = string.Empty;

            if (this.CBO01_GPRTGN.GetValue().ToString() == "3" || this.CBO01_GPRTGN.GetValue().ToString() == "4")
            {
                this.FPS91_TY_S_AC_29H78152.Visible = false;

                //if (Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(4, 2)) <= 03)
                //{
                if (Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 4)) <= 2012 ||
                    (Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 6)) == 201401 ||
                     Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 6)) == 201402 ||
                     Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 6)) == 201403)
                   )
                {
                    this.FPS91_TY_S_AC_29H77151.Visible = true;
                    this.FPS91_TY_S_AC_44UGK334.Visible = false;

                    UP_Spread_Title();  // 2014년 01월~ 2014년 03월 // 기존 타이들을 조정함

                    sProcedure = "TY_P_AC_29H6N149"; // 영업외손익계산서 출력(2012년 이전 및 2014년 01월~ 2014년 03월)
                }
                else // 2014년중 기준종료 월이 03월 이상인경우
                {
                    this.FPS91_TY_S_AC_29H77151.Visible = false;
                    this.FPS91_TY_S_AC_44UGK334.Visible = true;

                    UP_Spread_Title_2014_03();  // 기존 타이들을 조정함  (2014년중 기준종료 월이 03월 이상인경우)

                    sProcedure = "TY_P_AC_44UGM335"; // 영업외손익계산서 출력(2014년 04월이후 조건: 시작이 2014년 1월이라도 종료 월이 3월이상이면 기타사업으로 처리함)
                }
            }
            else
            {
                this.FPS91_TY_S_AC_29H78152.Visible = true;
                this.FPS91_TY_S_AC_29H77151.Visible = false;
                this.FPS91_TY_S_AC_44UGK334.Visible = false;

                sProcedure = "TY_P_AC_29H6N150"; // 영업외손익계산서 출력(12년 이후)
            }


            // ---  조회 처리 (SP)  --
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                sProcedure.ToString(),
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GEDYYMM.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (this.CBO01_GPRTGN.GetValue().ToString() == "3" || this.CBO01_GPRTGN.GetValue().ToString() == "4")
                {
                    if (this.CBO01_GPRTGN.GetValue().ToString() == "4")
                    {
                        //if (Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(4, 2)) <= 03)
                        //{
                        if (Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 4)) <= 2012 ||
                            (Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 6)) == 201401 ||
                             Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 6)) == 201402 ||
                             Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 6)) == 201403)
                           )
                        {
                            this.FPS91_TY_S_AC_29H77151.SetValue(UP_ConvertDt(dt)); // 영업외손익계산서 출력(12년 이전 및 2014년 01월~ 2014년 03월)
                        }
                        else
                        {
                            this.FPS91_TY_S_AC_44UGK334.SetValue(UP_ConvertDt_2014_03(dt)); // 2014년중 기준종료 월이 03월 이상인경우
                        }
                    }
                }
                else
                {
                    this.FPS91_TY_S_AC_29H78152.SetValue(UP_ConvertDt(dt)); // 영업외손익계산서 출력(12년 이후)
                }
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        }
        #endregion

        #region Description : 스프레드 타이틀 변경
        private void UP_Spread_Title()
        {
            this.FPS91_TY_S_AC_29H77151_Sheet1.ColumnHeaderRowCount = 1;
            this.FPS91_TY_S_AC_29H77151_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_29H77151_Sheet1.AddColumnHeaderSpanCell(0, 0, 1, 2);

            this.FPS91_TY_S_AC_29H77151_Sheet1.ColumnHeader.Cells[0, 6].Value = "Latex 외-계획";
            this.FPS91_TY_S_AC_29H77151_Sheet1.ColumnHeader.Cells[0, 7].Value = "Latex 외-실적";
            this.FPS91_TY_S_AC_29H77151_Sheet1.ColumnHeader.Cells[0, 8].Value = "Methanol 외-계획";
            this.FPS91_TY_S_AC_29H77151_Sheet1.ColumnHeader.Cells[0, 9].Value = "Methanol 외-실적";
            this.FPS91_TY_S_AC_29H77151_Sheet1.ColumnHeader.Cells[0, 10].Value = "TFT 팀-계획합계";
            this.FPS91_TY_S_AC_29H77151_Sheet1.ColumnHeader.Cells[0, 11].Value = "TFT 팀-실적합계";

            this.FPS91_TY_S_AC_29H77151_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion

        #region Description : 스프레드 타이틀 변경 (2014년중 기준종료 월이 03월 이상인경우)
        private void UP_Spread_Title_2014_03()
        {
            this.FPS91_TY_S_AC_44UGK334_Sheet1.ColumnHeaderRowCount = 1;
            this.FPS91_TY_S_AC_44UGK334_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_44UGK334_Sheet1.AddColumnHeaderSpanCell(0, 0, 1, 2);

            this.FPS91_TY_S_AC_44UGK334_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;


            /////////////////////////////////////////////////////////////////
            //this.FPS91_TY_S_AC_44UGK334_Sheet1.ColumnHeaderRowCount = 2;
            //this.FPS91_TY_S_AC_44UGK334_Sheet1.RowHeaderColumnCount = 1;

            ////(현재ROW, 현재COLUMN, 묶을ROW수, 묶을 COLUMN수)
            //this.FPS91_TY_S_AC_44UGK334_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1); // 사업자번호
            //this.FPS91_TY_S_AC_44UGK334_Sheet1.AddColumnHeaderSpanCell(0, 2, 1, 2); // UTT
            //this.FPS91_TY_S_AC_44UGK334_Sheet1.AddColumnHeaderSpanCell(0, 4, 1, 2); // SILO
            //this.FPS91_TY_S_AC_44UGK334_Sheet1.AddColumnHeaderSpanCell(0, 6, 1, 2); // 기타
            //this.FPS91_TY_S_AC_44UGK334_Sheet1.AddColumnHeaderSpanCell(0, 8, 1, 2); // 합계

            //this.FPS91_TY_S_AC_44UGK334_Sheet1.ColumnHeader.Cells[0, 1].Value = "계정과목";
            //this.FPS91_TY_S_AC_44UGK334_Sheet1.ColumnHeader.Cells[0, 2].Value = "UTT";
            //this.FPS91_TY_S_AC_44UGK334_Sheet1.ColumnHeader.Cells[0, 4].Value = "SILO";
            //this.FPS91_TY_S_AC_44UGK334_Sheet1.ColumnHeader.Cells[0, 6].Value = "기타사업";
            //this.FPS91_TY_S_AC_44UGK334_Sheet1.ColumnHeader.Cells[0, 8].Value = "합계";


            //this.FPS91_TY_S_AC_44UGK334_Sheet1.ColumnHeader.Cells[1, 1].Value = "";
            //this.FPS91_TY_S_AC_44UGK334_Sheet1.ColumnHeader.Cells[1, 2].Value = "계획";
            //this.FPS91_TY_S_AC_44UGK334_Sheet1.ColumnHeader.Cells[1, 3].Value = "실적";
            //this.FPS91_TY_S_AC_44UGK334_Sheet1.ColumnHeader.Cells[1, 4].Value = "계획";
            //this.FPS91_TY_S_AC_44UGK334_Sheet1.ColumnHeader.Cells[1, 5].Value = "실적";
            //this.FPS91_TY_S_AC_44UGK334_Sheet1.ColumnHeader.Cells[1, 6].Value = "계획";
            //this.FPS91_TY_S_AC_44UGK334_Sheet1.ColumnHeader.Cells[1, 7].Value = "실적";
            //this.FPS91_TY_S_AC_44UGK334_Sheet1.ColumnHeader.Cells[1, 8].Value = "계획";
            //this.FPS91_TY_S_AC_44UGK334_Sheet1.ColumnHeader.Cells[1, 9].Value = "실적";

            //this.FPS91_TY_S_AC_44UGK334_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sProcedure = string.Empty;

            if (this.CBO01_GPRTGN.GetValue().ToString() == "3" || this.CBO01_GPRTGN.GetValue().ToString() == "4")
            {
                //if (Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(4, 2)) <= 03)
                //{
                if (Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 4)) <= 2012 ||
                    (Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 6)) == 201401 ||
                     Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 6)) == 201402 ||
                     Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 6)) == 201403)
                   )
                {
                    sProcedure = "TY_P_AC_29H6N149"; // 영업외손익계산서 출력(12년 이전)
                }
                else
                {
                    sProcedure = "TY_P_AC_44UGM335"; // 영업외손익계산서 출력(2014년 04월이후 조건: 시작이 2014년 1월이라도 종료 월이 3월이상이면 기타사업으로 처리함)
                }
            }
            else
            {
                sProcedure = "TY_P_AC_29H6N150"; // 영업외손익계산서 출력(12년 이후)
            }

            // 조회 (SP)
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                sProcedure.ToString(),
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GEDYYMM.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (this.CBO01_GPRTGN.GetValue().ToString() == "1")
                {
                    SectionReport rpt1 = new TYACNC029R1();

                    // 가로 출력
                    rpt1.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                    (new TYERGB001P(rpt1, UP_ConvertDt(dt))).ShowDialog();
                }
                else if (this.CBO01_GPRTGN.GetValue().ToString() == "2")
                {
                    SectionReport rpt2 = new TYACNC029R2();

                    // 가로 출력
                    rpt2.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                    (new TYERGB001P(rpt2, UP_ConvertDt(dt))).ShowDialog();
                }
                else if (this.CBO01_GPRTGN.GetValue().ToString() == "3")
                {
                    SectionReport rpt3 = new TYACNC029R3();

                    // 가로 출력
                    rpt3.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                    (new TYERGB001P(rpt3, UP_ConvertDt(dt))).ShowDialog();
                }
                else if (this.CBO01_GPRTGN.GetValue().ToString() == "4")
                {
                    // 영업비용명세서 출력(12년 이전 및 2014년 01월~ 2014년 03월)
                    //if (Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(4, 2)) <= 03)
                    //{
                    if (Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 4)) <= 2012 ||
                        (Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 6)) == 201401 ||
                         Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 6)) == 201402 ||
                         Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 6)) == 201403)
                       )
                    {
                        SectionReport rpt4 = new TYACNC029R4();

                        // 가로 출력
                        rpt4.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                        (new TYERGB001P(rpt4, UP_ConvertDt(dt))).ShowDialog();
                    }
                    else // 영업비용명세서 출력(2014년 04월이후 조건: 시작이 2014년 1월이라도 종료 월이 3월이상이면 기타사업으로 처리함)
                    {
                        if (Convert.ToDouble(this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4)) >= 2016 && 
                            Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 4)) >= 2016)
                        {
                            SectionReport rpt6 = new TYACNC029R6();

                            // 가로 출력
                            rpt6.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                            (new TYERGB001P(rpt6, UP_ConvertDt_2014_03(dt))).ShowDialog();
                        }
                        else{
                            SectionReport rpt5 = new TYACNC029R5();

                            // 가로 출력
                            rpt5.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                            (new TYERGB001P(rpt5, UP_ConvertDt_2014_03(dt))).ShowDialog();
                        }
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

        #region Description : 데이터테이블 컨버젼
        private DataTable UP_ConvertDt(DataTable dt)
        {
            DataTable table = new DataTable();

            table = dt;

            if (this.CBO01_GPRTGN.GetValue().ToString() == "3" || this.CBO01_GPRTGN.GetValue().ToString() == "4")
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {

                    table.Rows[i]["T0CRHAP"]  = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["T0CRHAP"].ToString())));
                    table.Rows[i]["T0DRHAP"]  = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["T0DRHAP"].ToString())));
                    table.Rows[i]["S0CRHAP"]  = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["S0CRHAP"].ToString())));
                    table.Rows[i]["S0DRHAP"]  = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["S0DRHAP"].ToString())));
                    table.Rows[i]["B82CRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["B82CRHAP"].ToString())));
                    table.Rows[i]["B82DRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["B82DRHAP"].ToString())));
                    table.Rows[i]["B81CRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["B81CRHAP"].ToString())));
                    table.Rows[i]["B81DRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["B81DRHAP"].ToString())));
                    table.Rows[i]["B8CRHAP"]  = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["B8CRHAP"].ToString())));
                    table.Rows[i]["B8DRHAP"]  = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["B8DRHAP"].ToString())));
                    table.Rows[i]["CRHAP"]    = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["CRHAP"].ToString())));
                    table.Rows[i]["DRHAP"]    = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["DRHAP"].ToString())));
                }
            }
            else
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    table.Rows[i]["T0CRHAP"]  = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["T0CRHAP"].ToString())));
                    table.Rows[i]["T0DRHAP"]  = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["T0DRHAP"].ToString())));
                    table.Rows[i]["S0CRHAP"]  = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["S0CRHAP"].ToString())));
                    table.Rows[i]["S0DRHAP"]  = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["S0DRHAP"].ToString())));
                    table.Rows[i]["B82CRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["B82CRHAP"].ToString())));
                    table.Rows[i]["B82DRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["B82DRHAP"].ToString())));
                    table.Rows[i]["B81CRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["B81CRHAP"].ToString())));
                    table.Rows[i]["B81DRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["B81DRHAP"].ToString())));
                    table.Rows[i]["B8CRHAP"]  = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["B8CRHAP"].ToString())));
                    table.Rows[i]["B8DRHAP"]  = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["B8DRHAP"].ToString())));
                    table.Rows[i]["A5CRHAP"]  = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["A5CRHAP"].ToString())));
                    table.Rows[i]["A5DRHAP"]  = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["A5DRHAP"].ToString())));
                    table.Rows[i]["CRHAP"]    = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["CRHAP"].ToString())));
                    table.Rows[i]["DRHAP"]    = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["DRHAP"].ToString())));
                }
            }

            return table;
        }
        #endregion

        #region Description : 데이터 테이블 컨버젼(2014년중 기준종료 월이 03월 이상인경우)
        private DataTable UP_ConvertDt_2014_03(DataTable dt)
        {
            DataTable table = new DataTable();

            table = dt;

            if (this.CBO01_GPRTGN.GetValue().ToString() == "3" || this.CBO01_GPRTGN.GetValue().ToString() == "4")
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {

                    table.Rows[i]["T0CRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["T0CRHAP"].ToString())));
                    table.Rows[i]["T0DRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["T0DRHAP"].ToString())));
                    table.Rows[i]["S0CRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["S0CRHAP"].ToString())));
                    table.Rows[i]["S0DRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["S0DRHAP"].ToString())));
                    table.Rows[i]["B8CRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["B8CRHAP"].ToString())));
                    table.Rows[i]["B8DRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["B8DRHAP"].ToString())));
                    table.Rows[i]["CRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["CRHAP"].ToString())));
                    table.Rows[i]["DRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["DRHAP"].ToString())));
                }
            }
            else
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    table.Rows[i]["T0CRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["T0CRHAP"].ToString())));
                    table.Rows[i]["T0DRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["T0DRHAP"].ToString())));
                    table.Rows[i]["S0CRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["S0CRHAP"].ToString())));
                    table.Rows[i]["S0DRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["S0DRHAP"].ToString())));
                    table.Rows[i]["B8CRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["B8CRHAP"].ToString())));
                    table.Rows[i]["B8DRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["B8DRHAP"].ToString())));
                    table.Rows[i]["A5CRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["A5CRHAP"].ToString())));
                    table.Rows[i]["A5DRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["A5DRHAP"].ToString())));
                    table.Rows[i]["CRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["CRHAP"].ToString())));
                    table.Rows[i]["DRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["DRHAP"].ToString())));
                }
            }

            return table;

        } 
        #endregion

        #region Description : 조회처리 전 처리
        private void BTN61_INQ_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.DTP01_GSTYYMM.GetString().Substring(0, 4) != this.DTP01_GEDYYMM.GetString().Substring(0, 4))
            {
                this.ShowCustomMessage("조회기간을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.DTP01_GSTYYMM);
                e.Successed = false;
                return;
            }
        }
        #endregion
        #region Description : 출력 전 처리
        private void BTN61_PRT_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.DTP01_GSTYYMM.GetString().Substring(0, 4) != this.DTP01_GEDYYMM.GetString().Substring(0, 4))
            {
                this.ShowCustomMessage("출력기간을 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.DTP01_GSTYYMM);
                e.Successed = false;
                return;
            }
        }
        #endregion
    }
}