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
    /// 구분손익계산서 출력 프로그램입니다.
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
    ///  TY_P_AC_29E5P068 : 구분손익계산서 출력(12년 이전)
    ///  TY_P_AC_29H9W129 : 구분손익계산서 출력(12년 이후)-전체
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_29HBN138 : 구분손익계산서-2012이전
    ///  TY_S_AC_29HBU139 : 구분손익계산서-2012이후
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACNC028S : TYBase
    {
        #region Description : 페이지 로드
        public TYACNC028S()
        {
            InitializeComponent();
        }

        private void TYACNC028S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_INQ.ProcessCheck += new TButton.CheckHandler(this.BTN61_INQ_ProcessCheck);
            this.BTN61_PRT.ProcessCheck += new TButton.CheckHandler(this.BTN61_PRT_ProcessCheck);

            this.FPS91_TY_S_AC_29HBU139.Visible = true;
            this.FPS91_TY_S_AC_29HBN138.Visible = false;

            SetStartingFocus(this.DTP01_GSTYYMM);
        }
        #endregion

 

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sProcedure = string.Empty;

            if (this.CBO01_GPRTGN.GetValue().ToString() == "3" || this.CBO01_GPRTGN.GetValue().ToString() == "4")
            {
                this.FPS91_TY_S_AC_29HBU139.Visible = false;

                if  (Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 4)) <= 2012 ||
                    (Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 6)) == 201401 ||
                     Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 6)) == 201402 ||
                     Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 6)) == 201403 )
                   )
                {
                    this.FPS91_TY_S_AC_29HBN138.Visible = true;
                    this.FPS91_TY_S_AC_44UDI331.Visible = false;

                    UP_Spread_Title();  // 2014년 01월~ 2014년 03월 // 기존 타이들을 조정함

                    sProcedure = "TY_P_AC_29E5P068"; // 구분손익계산서 출력(12년 이전 및 2014년 01월~ 2014년 03월)
                }
                else // 2014년중 기준종료 월이 03월 이상인경우
                {
                    this.FPS91_TY_S_AC_29HBN138.Visible = false;
                    this.FPS91_TY_S_AC_44UDI331.Visible = true;

                    UP_Spread_Title_2014_03();  // 기존 타이들을 조정함  (2014년중 기준종료 월이 03월 이상인경우)

                    sProcedure = "TY_P_AC_44UE5332"; // 구분손익계산서 출력(2014년 04월이후 조건: 시작이 2014년 1월이라도 종료 월이 3월이상이면 기타사업으로 처리함)
                }
            }
            else
            {
                this.FPS91_TY_S_AC_29HBU139.Visible = true;
                this.FPS91_TY_S_AC_29HBN138.Visible = false;
                this.FPS91_TY_S_AC_44UDI331.Visible = false;

                sProcedure = "TY_P_AC_29H9W129";
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
                        if (Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 4)) <= 2012 ||
                            (Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 6)) == 201401 ||
                             Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 6)) == 201402 ||
                             Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 6)) == 201403)
                           )
                        {
                            this.FPS91_TY_S_AC_29HBN138.SetValue(UP_ConvertDt(dt)); // 구분손익계산서 출력(12년 이전 및 2014년 01월~ 2014년 03월)
                        }
                        else
                        {
                            this.FPS91_TY_S_AC_44UDI331.SetValue(UP_ConvertDt_2014_03(dt)); // 2014년중 기준종료 월이 03월 이상인경우
                        }
                    }
                }
                else
                {
                    this.FPS91_TY_S_AC_29HBU139.SetValue(UP_ConvertDt(dt));
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
                    sProcedure = "TY_P_AC_29E5P068"; // 구분손익계산서 출력(12년 이전 및 2014년 01월~ 2014년 03월)
                }
                else
                {
                    sProcedure = "TY_P_AC_44UE5332"; // 구분손익계산서 출력(2014년 04월이후 조건: 시작이 2014년 1월이라도 종료 월이 3월이상이면 기타사업으로 처리함)
                }
            }
            else
            {
                sProcedure = "TY_P_AC_29H9W129";
            }

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
                    SectionReport rpt1 = new TYACNC028R1();

                    // 가로 출력
                    rpt1.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                    (new TYERGB001P(rpt1, UP_ConvertDt(dt))).ShowDialog();
                }
                else if (this.CBO01_GPRTGN.GetValue().ToString() == "2")
                {
                    SectionReport rpt2 = new TYACNC028R2();

                    // 가로 출력
                    rpt2.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                    (new TYERGB001P(rpt2, UP_ConvertDt(dt))).ShowDialog();
                }
                else if (this.CBO01_GPRTGN.GetValue().ToString() == "3")
                {
                    SectionReport rpt3 = new TYACNC028R3();

                    // 가로 출력
                    rpt3.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                    (new TYERGB001P(rpt3, UP_ConvertDt(dt))).ShowDialog();
                }
                else if (this.CBO01_GPRTGN.GetValue().ToString() == "4")
                {
                    // 구분손익계산서 출력(12년 이전 및 2014년 01월~ 2014년 03월)
                    //if (Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(4, 2)) <= 03)
                    //{
                    if (Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 4)) <= 2012 ||
                        (Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 6)) == 201401 ||
                         Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 6)) == 201402 ||
                         Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 6)) == 201403)
                       )
                    {
                        SectionReport rpt4 = new TYACNC028R4();

                        // 가로 출력
                        rpt4.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                        (new TYERGB001P(rpt4, UP_ConvertDt(dt))).ShowDialog();
                    }
                    else  // 구분손익계산서 출력(2014년 04월이후 조건: 시작이 2014년 1월이라도 종료 월이 3월이상이면 기타사업으로 처리함)
                    {
                        if (Convert.ToDouble(this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4)) >= 2016 &&
                            Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 4)) >= 2016)
                        {
                            SectionReport rpt6 = new TYACNC028R6();

                            // 가로 출력
                            rpt6.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                            (new TYERGB001P(rpt6, UP_ConvertDt_2014_03(dt))).ShowDialog();
                        }
                        else
                        {
                            SectionReport rpt5 = new TYACNC028R5();

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


        #region Description : 스프레드 타이틀 변경
        private void UP_Spread_Title()
        {
            this.FPS91_TY_S_AC_29HBN138_Sheet1.ColumnHeaderRowCount = 1;
            this.FPS91_TY_S_AC_29HBN138_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_29HBN138_Sheet1.AddColumnHeaderSpanCell(0, 0, 1, 2);

            this.FPS91_TY_S_AC_29HBN138_Sheet1.ColumnHeader.Cells[0, 6].Value = "Latex 외-계획";
            this.FPS91_TY_S_AC_29HBN138_Sheet1.ColumnHeader.Cells[0, 7].Value = "Latex 외-실적";
            this.FPS91_TY_S_AC_29HBN138_Sheet1.ColumnHeader.Cells[0, 8].Value = "Methanol 외-계획";
            this.FPS91_TY_S_AC_29HBN138_Sheet1.ColumnHeader.Cells[0, 9].Value = "Methanol 외-실적";
            this.FPS91_TY_S_AC_29HBN138_Sheet1.ColumnHeader.Cells[0, 10].Value = "TFT 팀-계획합계";
            this.FPS91_TY_S_AC_29HBN138_Sheet1.ColumnHeader.Cells[0, 11].Value = "TFT 팀-실적합계";

            this.FPS91_TY_S_AC_29HBN138_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion

        #region Description : 스프레드 타이틀 변경 (2014년중 기준종료 월이 03월 이상인경우)
        private void UP_Spread_Title_2014_03()
        {
            this.FPS91_TY_S_AC_44UDI331_Sheet1.ColumnHeaderRowCount = 1;
            this.FPS91_TY_S_AC_44UDI331_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_44UDI331_Sheet1.AddColumnHeaderSpanCell(0, 0, 1, 2);

            this.FPS91_TY_S_AC_44UDI331_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion


        #region Description : 데이터테이블 컨버젼 
        private DataTable UP_ConvertDt(DataTable dt)
        {
            DataTable table = new DataTable();

            table = dt;

            if (this.CBO01_GPRTGN.GetValue().ToString() == "3" || this.CBO01_GPRTGN.GetValue().ToString() == "4")
            {
                #region Description : 2014년 1월
                double Sum07_T0CRAHP = 0;
                double Sum07_T0DRHAP = 0;
                double Sum07_S0CRHAP = 0;
                double Sum07_S0DRHAP = 0;
                double Sum07_B82CRHAP = 0;
                double Sum07_B82DRHAP = 0;
                double Sum07_B81CRHAP = 0;
                double Sum07_B81DRHAP = 0;
                double Sum07_B8CRHAP = 0;
                double Sum07_B8DRHAP = 0;
                double Sum07_CRHAP = 0;
                double Sum07_DRHAP = 0;

                double Sum08_T0CRAHP = 0;
                double Sum08_T0DRHAP = 0;
                double Sum08_S0CRHAP = 0;
                double Sum08_S0DRHAP = 0;
                double Sum08_B82CRHAP = 0;
                double Sum08_B82DRHAP = 0;
                double Sum08_B81CRHAP = 0;
                double Sum08_B81DRHAP = 0;
                double Sum08_B8CRHAP = 0;
                double Sum08_B8DRHAP = 0;
                double Sum08_CRHAP = 0;
                double Sum08_DRHAP = 0;

                double Sum09_T0CRAHP = 0;
                double Sum09_T0DRHAP = 0;
                double Sum09_S0CRHAP = 0;
                double Sum09_S0DRHAP = 0;
                double Sum09_B82CRHAP = 0;
                double Sum09_B82DRHAP = 0;
                double Sum09_B81CRHAP = 0;
                double Sum09_B81DRHAP = 0;
                double Sum09_B8CRHAP = 0;
                double Sum09_B8DRHAP = 0;
                double Sum09_CRHAP = 0;
                double Sum09_DRHAP = 0;

                double Sum10_T0CRAHP = 0;
                double Sum10_T0DRHAP = 0;
                double Sum10_S0CRHAP = 0;
                double Sum10_S0DRHAP = 0;
                double Sum10_B82CRHAP = 0;
                double Sum10_B82DRHAP = 0;
                double Sum10_B81CRHAP = 0;
                double Sum10_B81DRHAP = 0;
                double Sum10_B8CRHAP = 0;
                double Sum10_B8DRHAP = 0;
                double Sum10_CRHAP = 0;
                double Sum10_DRHAP = 0;

                double Sum11_T0CRAHP = 0;
                double Sum11_T0DRHAP = 0;
                double Sum11_S0CRHAP = 0;
                double Sum11_S0DRHAP = 0;
                double Sum11_B82CRHAP = 0;
                double Sum11_B82DRHAP = 0;
                double Sum11_B81CRHAP = 0;
                double Sum11_B81DRHAP = 0;
                double Sum11_B8CRHAP = 0;
                double Sum11_B8DRHAP = 0;
                double Sum11_CRHAP = 0;
                double Sum11_DRHAP = 0;

                double Sum12_T0CRAHP = 0;
                double Sum12_T0DRHAP = 0;
                double Sum12_S0CRHAP = 0;
                double Sum12_S0DRHAP = 0;
                double Sum12_B82CRHAP = 0;
                double Sum12_B82DRHAP = 0;
                double Sum12_B81CRHAP = 0;
                double Sum12_B81DRHAP = 0;
                double Sum12_B8CRHAP = 0;
                double Sum12_B8DRHAP = 0;
                double Sum12_CRHAP = 0;
                double Sum12_DRHAP = 0;

                double Sum13_T0CRAHP = 0;
                double Sum13_T0DRHAP = 0;
                double Sum13_S0CRHAP = 0;
                double Sum13_S0DRHAP = 0;
                double Sum13_B82CRHAP = 0;
                double Sum13_B82DRHAP = 0;
                double Sum13_B81CRHAP = 0;
                double Sum13_B81DRHAP = 0;
                double Sum13_B8CRHAP = 0;
                double Sum13_B8DRHAP = 0;
                double Sum13_CRHAP = 0;
                double Sum13_DRHAP = 0;

                double Sum20_T0CRAHP = 0;
                double Sum20_T0DRHAP = 0;
                double Sum20_S0CRHAP = 0;
                double Sum20_S0DRHAP = 0;
                double Sum20_B82CRHAP = 0;
                double Sum20_B82DRHAP = 0;
                double Sum20_B81CRHAP = 0;
                double Sum20_B81DRHAP = 0;
                double Sum20_B8CRHAP = 0;
                double Sum20_B8DRHAP = 0;
                double Sum20_CRHAP = 0;
                double Sum20_DRHAP = 0;


                double Div07_T0DRHAP = 0;
                double Div07_S0DRHAP = 0;
                double Div07_B82DRHAP = 0;
                double Div07_B81DRHAP = 0;
                double Div07_B8DRHAP = 0;
                double Div07_DRHAP = 0;


                double Div10_T0DRHAP = 0;
                double Div10_S0DRHAP = 0;
                double Div10_B82DRHAP = 0;
                double Div10_B81DRHAP = 0;
                double Div10_B8DRHAP = 0;
                double Div10_DRHAP = 0;

                double Div20_T0DRHAP = 0;
                double Div20_S0DRHAP = 0;
                double Div20_B82DRHAP = 0;
                double Div20_B81DRHAP = 0;
                double Div20_B8DRHAP = 0;
                double Div20_DRHAP = 0;

                for (int i = 0; i < table.Rows.Count; i++)
                {

                    if (table.Rows[i]["APACDAC"].ToString() == "08000000" ||
                        table.Rows[i]["APACDAC"].ToString() == "09000000" ||
                        table.Rows[i]["APACDAC"].ToString() == "20000000")
                    {
                        table.Rows[i]["T0CRHAP"] = 0;
                        table.Rows[i]["T0DRHAP"] = 0;
                        table.Rows[i]["S0CRHAP"] = 0;
                        table.Rows[i]["S0DRHAP"] = 0;
                        table.Rows[i]["B82CRHAP"] = 0;
                        table.Rows[i]["B82DRHAP"] = 0;
                        table.Rows[i]["B81CRHAP"] = 0;
                        table.Rows[i]["B81DRHAP"] = 0;
                        table.Rows[i]["B8CRHAP"] = 0;
                        table.Rows[i]["B8DRHAP"] = 0;
                        table.Rows[i]["CRHAP"] = 0;
                        table.Rows[i]["DRHAP"] = 0;
                    }
                    else
                    {
                        if (table.Rows[i]["APACDAC"].ToString().Substring(7, 1) == "1")
                        {
                            table.Rows[i]["T0CRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(table.Rows[i]["T0CRHAP"].ToString()) / 10));
                            table.Rows[i]["T0DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(table.Rows[i]["T0DRHAP"].ToString()) / 10));
                            table.Rows[i]["S0CRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(table.Rows[i]["S0CRHAP"].ToString()) / 10));
                            table.Rows[i]["S0DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(table.Rows[i]["S0DRHAP"].ToString()) / 10));
                            table.Rows[i]["B82CRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(table.Rows[i]["B82CRHAP"].ToString()) / 10));
                            table.Rows[i]["B82DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(table.Rows[i]["B82DRHAP"].ToString()) / 10));
                            table.Rows[i]["B81CRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(table.Rows[i]["B81CRHAP"].ToString()) / 10));
                            table.Rows[i]["B81DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(table.Rows[i]["B81DRHAP"].ToString()) / 10));
                            table.Rows[i]["B8CRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(table.Rows[i]["B8CRHAP"].ToString()) / 10));
                            table.Rows[i]["B8DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(table.Rows[i]["B8DRHAP"].ToString()) / 10));
                            table.Rows[i]["CRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(table.Rows[i]["CRHAP"].ToString()) / 10));
                            table.Rows[i]["DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(table.Rows[i]["DRHAP"].ToString()) / 10));
                        }
                        else
                        {
                            table.Rows[i]["T0CRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["T0CRHAP"].ToString())));
                            table.Rows[i]["T0DRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["T0DRHAP"].ToString())));
                            table.Rows[i]["S0CRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["S0CRHAP"].ToString())));
                            table.Rows[i]["S0DRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["S0DRHAP"].ToString())));
                            table.Rows[i]["B82CRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["B82CRHAP"].ToString())));
                            table.Rows[i]["B82DRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["B82DRHAP"].ToString())));
                            table.Rows[i]["B81CRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["B81CRHAP"].ToString())));
                            table.Rows[i]["B81DRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["B81DRHAP"].ToString())));
                            table.Rows[i]["B8CRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["B8CRHAP"].ToString())));
                            table.Rows[i]["B8DRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["B8DRHAP"].ToString())));
                            table.Rows[i]["CRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["CRHAP"].ToString())));
                            table.Rows[i]["DRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(table.Rows[i]["DRHAP"].ToString())));
                        }
                    }
                }

                for (int i = 0; i < table.Rows.Count; i++)
                {

                    // 07000000	영업이익2 
                    if (table.Rows[i]["APACDAC"].ToString() == "07000000")
                    {
                        Sum07_T0CRAHP = Sum07_T0CRAHP + Convert.ToDouble(Get_Numeric(table.Rows[i]["T0CRHAP"].ToString()));
                        Sum07_T0DRHAP = Sum07_T0DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["T0DRHAP"].ToString()));
                        Sum07_S0CRHAP = Sum07_S0CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["S0CRHAP"].ToString()));
                        Sum07_S0DRHAP = Sum07_S0DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["S0DRHAP"].ToString()));
                        Sum07_B82CRHAP = Sum07_B82CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B82CRHAP"].ToString()));
                        Sum07_B82DRHAP = Sum07_B82DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B82DRHAP"].ToString()));
                        Sum07_B81CRHAP = Sum07_B81CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B81CRHAP"].ToString()));
                        Sum07_B81DRHAP = Sum07_B81DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B81DRHAP"].ToString()));
                        Sum07_B8CRHAP = Sum07_B8CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B8CRHAP"].ToString()));
                        Sum07_B8DRHAP = Sum07_B8DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B8DRHAP"].ToString()));
                        Sum07_CRHAP = Sum07_CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["CRHAP"].ToString()));
                        Sum07_DRHAP = Sum07_DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["DRHAP"].ToString()));
                    }

                    // 08000000	영업외수익
                    if (table.Rows[i]["APACDAC"].ToString().Substring(0, 2) == "08")
                    {
                        Sum08_T0CRAHP = Sum08_T0CRAHP + Convert.ToDouble(Get_Numeric(table.Rows[i]["T0CRHAP"].ToString()));
                        Sum08_T0DRHAP = Sum08_T0DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["T0DRHAP"].ToString()));
                        Sum08_S0CRHAP = Sum08_S0CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["S0CRHAP"].ToString()));
                        Sum08_S0DRHAP = Sum08_S0DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["S0DRHAP"].ToString()));
                        Sum08_B82CRHAP = Sum08_B82CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B82CRHAP"].ToString()));
                        Sum08_B82DRHAP = Sum08_B82DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B82DRHAP"].ToString()));
                        Sum08_B81CRHAP = Sum08_B81CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B81CRHAP"].ToString()));
                        Sum08_B81DRHAP = Sum08_B81DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B81DRHAP"].ToString()));
                        Sum08_B8CRHAP = Sum08_B8CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B8CRHAP"].ToString()));
                        Sum08_B8DRHAP = Sum08_B8DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B8DRHAP"].ToString()));
                        Sum08_CRHAP = Sum08_CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["CRHAP"].ToString()));
                        Sum08_DRHAP = Sum08_DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["DRHAP"].ToString()));
                    }

                    // 09000000	영업외비용
                    if (table.Rows[i]["APACDAC"].ToString().Substring(0, 2) == "09")
                    {
                        Sum09_T0CRAHP = Sum09_T0CRAHP + Convert.ToDouble(Get_Numeric(table.Rows[i]["T0CRHAP"].ToString()));
                        Sum09_T0DRHAP = Sum09_T0DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["T0DRHAP"].ToString()));
                        Sum09_S0CRHAP = Sum09_S0CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["S0CRHAP"].ToString()));
                        Sum09_S0DRHAP = Sum09_S0DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["S0DRHAP"].ToString()));
                        Sum09_B82CRHAP = Sum09_B82CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B82CRHAP"].ToString()));
                        Sum09_B82DRHAP = Sum09_B82DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B82DRHAP"].ToString()));
                        Sum09_B81CRHAP = Sum09_B81CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B81CRHAP"].ToString()));
                        Sum09_B81DRHAP = Sum09_B81DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B81DRHAP"].ToString()));
                        Sum09_B8CRHAP = Sum09_B8CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B8CRHAP"].ToString()));
                        Sum09_B8DRHAP = Sum09_B8DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B8DRHAP"].ToString()));
                        Sum09_CRHAP = Sum09_CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["CRHAP"].ToString()));
                        Sum09_DRHAP = Sum09_DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["DRHAP"].ToString()));
                    }

                    // 11000000	외환손익(재무)
                    if (table.Rows[i]["APACDAC"].ToString() == "11000000")
                    {
                        Sum11_T0CRAHP = 0;
                        Sum11_T0DRHAP = 0;
                        Sum11_S0CRHAP = 0;
                        Sum11_S0DRHAP = 0;
                        Sum11_B82CRHAP = 0;
                        Sum11_B82DRHAP = 0;
                        Sum11_B81CRHAP = 0;
                        Sum11_B81DRHAP = 0;
                        Sum11_B8CRHAP = 0;
                        Sum11_B8DRHAP = 0;
                        Sum11_CRHAP = Sum11_CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["CRHAP"].ToString()));
                        Sum11_DRHAP = Sum11_DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["DRHAP"].ToString()));
                    }

                    // 12000000	투자이자(P.J)
                    if (table.Rows[i]["APACDAC"].ToString() == "12000000")
                    {
                        Sum12_T0CRAHP = 0;
                        Sum12_T0DRHAP = 0;
                        Sum12_S0CRHAP = 0;
                        Sum12_S0DRHAP = 0;
                        Sum12_B82CRHAP = 0;
                        Sum12_B82DRHAP = 0;
                        Sum12_B81CRHAP = 0;
                        Sum12_B81DRHAP = 0;
                        Sum12_B8CRHAP = 0;
                        Sum12_B8DRHAP = 0;
                        Sum12_CRHAP = Sum12_CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["CRHAP"].ToString()));
                        Sum12_DRHAP = Sum12_DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["DRHAP"].ToString()));
                    }

                    // 13000000	지분법손익
                    if (table.Rows[i]["APACDAC"].ToString() == "13000000")
                    {
                        Sum13_T0CRAHP = Sum13_T0CRAHP + Convert.ToDouble(Get_Numeric(table.Rows[i]["T0CRHAP"].ToString()));
                        Sum13_T0DRHAP = Sum13_T0DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["T0DRHAP"].ToString()));
                        Sum13_S0CRHAP = Sum13_S0CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["S0CRHAP"].ToString()));
                        Sum13_S0DRHAP = Sum13_S0DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["S0DRHAP"].ToString()));
                        Sum13_B82CRHAP = Sum13_B82CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B82CRHAP"].ToString()));
                        Sum13_B82DRHAP = Sum13_B82DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B82DRHAP"].ToString()));
                        Sum13_B81CRHAP = Sum13_B81CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B81CRHAP"].ToString()));
                        Sum13_B81DRHAP = Sum13_B81DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B81DRHAP"].ToString()));
                        Sum13_B8CRHAP = Sum13_B8CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B8CRHAP"].ToString()));
                        Sum13_B8DRHAP = Sum13_B8DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B8DRHAP"].ToString()));
                        Sum13_CRHAP = Sum13_CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["CRHAP"].ToString()));
                        Sum13_DRHAP = Sum13_DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["DRHAP"].ToString()));
                    }

                }

                // 영업외수익 (08000000)
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i]["APACDAC"].ToString() == "08000000")
                    {
                        table.Rows[i]["T0CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum08_T0CRAHP));
                        table.Rows[i]["T0DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum08_T0DRHAP));
                        table.Rows[i]["S0CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum08_S0CRHAP));
                        table.Rows[i]["S0DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum08_S0DRHAP));
                        table.Rows[i]["B82CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum08_B82CRHAP));
                        table.Rows[i]["B82DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum08_B82DRHAP));
                        table.Rows[i]["B81CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum08_B81CRHAP));
                        table.Rows[i]["B81DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum08_B81DRHAP));
                        table.Rows[i]["B8CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum08_B8CRHAP));
                        table.Rows[i]["B8DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum08_B8DRHAP));
                        table.Rows[i]["CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum08_CRHAP));
                        table.Rows[i]["DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum08_DRHAP));
                    }
                }

                // 영업외비용 (09000000)
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i]["APACDAC"].ToString() == "09000000")
                    {
                        table.Rows[i]["T0CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum09_T0CRAHP));
                        table.Rows[i]["T0DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum09_T0DRHAP));
                        table.Rows[i]["S0CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum09_S0CRHAP));
                        table.Rows[i]["S0DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum09_S0DRHAP));
                        table.Rows[i]["B82CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum09_B82CRHAP));
                        table.Rows[i]["B82DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum09_B82DRHAP));
                        table.Rows[i]["B81CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum09_B81CRHAP));
                        table.Rows[i]["B81DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum09_B81DRHAP));
                        table.Rows[i]["B8CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum09_B8CRHAP));
                        table.Rows[i]["B8DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum09_B8DRHAP));
                        table.Rows[i]["CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum09_CRHAP));
                        table.Rows[i]["DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum09_DRHAP));
                    }
                }

                // 프로젝트이자 (12000000) -- 사업부별 화면정리(0)
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i]["APACDAC"].ToString() == "12000000")
                    {
                        table.Rows[i]["T0CRHAP"] = "";
                        table.Rows[i]["T0DRHAP"] = "";
                        table.Rows[i]["S0CRHAP"] = "";
                        table.Rows[i]["S0DRHAP"] = "";
                        table.Rows[i]["B82CRHAP"] = "";
                        table.Rows[i]["B82DRHAP"] = "";
                        table.Rows[i]["B81CRHAP"] = "";
                        table.Rows[i]["B81DRHAP"] = "";
                        table.Rows[i]["B8CRHAP"] = "";
                        table.Rows[i]["B8DRHAP"] = "";
                    }
                }

                // 10000000  경상이익
                // 경상이익 =  영업이익 +  영업외수익 -  영업외비용
                Sum10_T0CRAHP = Sum07_T0CRAHP + Sum08_T0CRAHP - Sum09_T0CRAHP;
                Sum10_T0DRHAP = Sum07_T0DRHAP + Sum08_T0DRHAP - Sum09_T0DRHAP;
                Sum10_S0CRHAP = Sum07_S0CRHAP + Sum08_S0CRHAP - Sum09_S0CRHAP;
                Sum10_S0DRHAP = Sum07_S0DRHAP + Sum08_S0DRHAP - Sum09_S0DRHAP;
                Sum10_B82CRHAP = Sum07_B82CRHAP + Sum08_B82CRHAP - Sum09_B82CRHAP;
                Sum10_B82DRHAP = Sum07_B82DRHAP + Sum08_B82DRHAP - Sum09_B82DRHAP;
                Sum10_B81CRHAP = Sum07_B81CRHAP + Sum08_B81CRHAP - Sum09_B81CRHAP;
                Sum10_B81DRHAP = Sum07_B81DRHAP + Sum08_B81DRHAP - Sum09_B81DRHAP;
                Sum10_B8CRHAP = Sum07_B8CRHAP + Sum08_B8CRHAP - Sum09_B8CRHAP;
                Sum10_B8DRHAP = Sum07_B8DRHAP + Sum08_B8DRHAP - Sum09_B8DRHAP;
                Sum10_CRHAP = Sum07_CRHAP + Sum08_CRHAP - Sum09_CRHAP;
                Sum10_DRHAP = Sum07_DRHAP + Sum08_DRHAP - Sum09_DRHAP;


                //Div10_T0DRHAP  = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum10_T0DRHAP / Sum10_T0CRAHP), 3) * 1000))) / 10;
                //Div10_S0DRHAP  = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum10_S0DRHAP / Sum10_S0CRHAP), 3) * 1000))) / 10;
                //Div10_B82DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum10_B82DRHAP / Sum10_B82CRHAP), 3) * 1000))) / 10;
                //Div10_B81DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum10_B81DRHAP / Sum10_B81CRHAP), 3) * 1000))) / 10;
                //Div10_B8DRHAP  = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum10_B8DRHAP / Sum10_B8CRHAP), 3) * 1000))) / 10;
                //Div10_DRHAP    = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum10_DRHAP / Sum10_CRHAP), 3) * 1000))) / 10;

                // 영업이익2 달성률 재계산(-)인 경우 표현 안함
                // 영업이익 --> 07000001
                if (Convert.ToDouble(Sum07_T0CRAHP) > 0 && Sum07_T0DRHAP > 0)
                {
                    Div07_T0DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum07_T0DRHAP / Sum07_T0CRAHP), 3) * 1000))) / 10;
                }
                else
                {
                    Div07_T0DRHAP = 0;
                }
                if (Convert.ToDouble(Sum07_S0CRHAP) > 0 && Sum07_S0DRHAP > 0)
                {
                    Div07_S0DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum07_S0DRHAP / Sum07_S0CRHAP), 3) * 1000))) / 10;
                }
                else
                {
                    Div07_S0DRHAP = 0;
                }
                if (Convert.ToDouble(Sum07_B82CRHAP) > 0 && Sum07_B82DRHAP > 0)
                {
                    Div07_B82DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum07_B82DRHAP / Sum07_B82CRHAP), 3) * 1000))) / 10;
                }
                else
                {
                    Div07_B82DRHAP = 0;
                }

                if (Convert.ToDouble(Sum07_B81CRHAP) > 0 && Sum07_B81DRHAP > 0)
                {
                    Div07_B81DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum07_B81DRHAP / Sum07_B81CRHAP), 3) * 1000))) / 10;
                }
                else
                {
                    Div07_B81DRHAP = 0;
                }

                if (Convert.ToDouble(Sum07_B8CRHAP) > 0 && Sum07_B8DRHAP > 0)
                {
                    Div07_B8DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum07_B8DRHAP / Sum07_B8CRHAP), 3) * 1000))) / 10;
                }
                else
                {
                    Div07_B8DRHAP = 0;
                }
                if (Convert.ToDouble(Sum07_CRHAP) > 0 && Sum07_DRHAP > 0)
                {
                    Div07_DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum07_DRHAP / Sum07_CRHAP), 3) * 1000))) / 10;
                }
                else
                {
                    Div07_DRHAP = 0;
                }
                // 영업이익율2 (07000001)
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i]["APACDAC"].ToString() == "07000001")
                    {
                        table.Rows[i]["T0DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div07_T0DRHAP));
                        table.Rows[i]["S0DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div07_S0DRHAP));
                        table.Rows[i]["B82DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div07_B82DRHAP));
                        table.Rows[i]["B81DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div07_B81DRHAP));
                        table.Rows[i]["B8DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div07_B8DRHAP));
                        table.Rows[i]["DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div07_DRHAP));
                    }
                } // End .. For 경상이익 (10000000)
                // -------------------------------------   END ----------------------------------------------------
                // ------------------------------------------------------------------------------------------------


                if (Convert.ToDouble(Sum10_T0CRAHP) > 0 && Sum10_T0DRHAP > 0)
                {
                    Div10_T0DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum10_T0DRHAP / Sum10_T0CRAHP), 3) * 1000))) / 10;
                }
                else
                {
                    Div10_T0DRHAP = 0;
                }
                if (Convert.ToDouble(Sum10_S0CRHAP) > 0 && Sum10_S0DRHAP > 0)
                {
                    Div10_S0DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum10_S0DRHAP / Sum10_S0CRHAP), 3) * 1000))) / 10;
                }
                else
                {
                    Div10_S0DRHAP = 0;
                }
                if (Convert.ToDouble(Sum10_B82CRHAP) > 0 && Sum10_B82DRHAP > 0)
                {
                    Div10_B82DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum10_B82DRHAP / Sum10_B82CRHAP), 3) * 1000))) / 10;
                }
                else
                {
                    Div10_B82DRHAP = 0;
                }

                if (Convert.ToDouble(Sum10_B81CRHAP) > 0 && Sum10_B81DRHAP > 0)
                {
                    Div10_B81DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum10_B81DRHAP / Sum10_B81CRHAP), 3) * 1000))) / 10;
                }
                else
                {
                    Div10_B81DRHAP = 0;
                }

                if (Convert.ToDouble(Sum10_B8CRHAP) > 0 && Sum10_B8DRHAP > 0)
                {
                    Div10_B8DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum10_B8DRHAP / Sum10_B8CRHAP), 3) * 1000))) / 10;
                }
                else
                {
                    Div10_B8DRHAP = 0;
                }
                if (Convert.ToDouble(Sum10_CRHAP) > 0 && Sum10_DRHAP > 0)
                {
                    Div10_DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum10_DRHAP / Sum10_CRHAP), 3) * 1000))) / 10;
                }
                else
                {
                    Div10_DRHAP = 0;
                }

                // 경상이익 (10000000)
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i]["APACDAC"].ToString() == "10000000")
                    {
                        table.Rows[i]["T0CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum10_T0CRAHP));
                        table.Rows[i]["T0DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum10_T0DRHAP));
                        table.Rows[i]["S0CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum10_S0CRHAP));
                        table.Rows[i]["S0DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum10_S0DRHAP));
                        table.Rows[i]["B82CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum10_B82CRHAP));
                        table.Rows[i]["B82DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum10_B82DRHAP));
                        table.Rows[i]["B81CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum10_B81CRHAP));
                        table.Rows[i]["B81DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum10_B81DRHAP));
                        table.Rows[i]["B8CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum10_B8CRHAP));
                        table.Rows[i]["B8DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum10_B8DRHAP));
                        table.Rows[i]["CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum10_CRHAP));
                        table.Rows[i]["DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum10_DRHAP));
                    }

                    if (table.Rows[i]["APACDAC"].ToString() == "10000001")
                    {

                        //if (Div10_T0DRHAP > 0)
                        //{ table.Rows[i]["T0DRHAP"] = Div10_T0DRHAP; }
                        //else { table.Rows[i]["T0DRHAP"] = 0; }

                        //if (Div10_S0DRHAP > 0)
                        //{ table.Rows[i]["S0DRHAP"] = Div10_S0DRHAP; }
                        //else { table.Rows[i]["S0DRHAP"] = 0; }

                        //if (Div10_B82DRHAP > 0)
                        //{ table.Rows[i]["B82DRHAP"] = Div10_B82DRHAP; }
                        //else { table.Rows[i]["B82DRHAP"] = 0; }

                        //if (Div10_B81DRHAP > 0)
                        //{ table.Rows[i]["B81DRHAP"] = Div10_B81DRHAP; }
                        //else { table.Rows[i]["B81DRHAP"] = 0; }

                        //if (Div10_B8DRHAP > 0)
                        //{ table.Rows[i]["B8DRHAP"] = Div10_B8DRHAP; }
                        //else { table.Rows[i]["B8DRHAP"] = 0; }

                        //if (Div10_DRHAP > 0)
                        //{ table.Rows[i]["DRHAP"] = Div10_DRHAP; }
                        //else { table.Rows[i]["DRHAP"] = 0; }

                        table.Rows[i]["T0DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div10_T0DRHAP));
                        table.Rows[i]["S0DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div10_S0DRHAP));
                        table.Rows[i]["B82DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div10_B82DRHAP));
                        table.Rows[i]["B81DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div10_B81DRHAP));
                        table.Rows[i]["B8DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div10_B8DRHAP));
                        table.Rows[i]["DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div10_DRHAP));
                    }
                } // End .. For 경상이익 (10000000)

                // 20000000	세전순이익
                // 세전순이익 = 경상이익 + 외환손익(재무) -  투자이자(P.J) +  지분법손익
                Sum20_T0CRAHP = Sum10_T0CRAHP + Sum11_T0CRAHP - Sum12_T0CRAHP + Sum13_T0CRAHP;
                Sum20_T0DRHAP = Sum10_T0DRHAP + Sum11_T0DRHAP - Sum12_T0DRHAP + Sum13_T0DRHAP;
                Sum20_S0CRHAP = Sum10_S0CRHAP + Sum11_S0CRHAP - Sum12_S0CRHAP + Sum13_S0CRHAP;
                Sum20_S0DRHAP = Sum10_S0DRHAP + Sum11_S0DRHAP - Sum12_S0DRHAP + Sum13_S0DRHAP;
                Sum20_B82CRHAP = Sum10_B82CRHAP + Sum11_B82CRHAP - Sum12_B82CRHAP + Sum13_B82CRHAP;
                Sum20_B82DRHAP = Sum10_B82DRHAP + Sum11_B82DRHAP - Sum12_B82DRHAP + Sum13_B82DRHAP;
                Sum20_B81CRHAP = Sum10_B81CRHAP + Sum11_B81CRHAP - Sum12_B81CRHAP + Sum13_B81CRHAP;
                Sum20_B81DRHAP = Sum10_B81DRHAP + Sum11_B81DRHAP - Sum12_B81DRHAP + Sum13_B81DRHAP;
                Sum20_B8CRHAP = Sum10_B8CRHAP + Sum11_B8CRHAP - Sum12_B8CRHAP + Sum13_B8CRHAP;
                Sum20_B8DRHAP = Sum10_B8DRHAP + Sum11_B8DRHAP - Sum12_B8DRHAP + Sum13_B8DRHAP;
                Sum20_CRHAP = Sum10_CRHAP + Sum11_CRHAP - Sum12_CRHAP + Sum13_CRHAP;
                Sum20_DRHAP = Sum10_DRHAP + Sum11_DRHAP - Sum12_DRHAP + Sum13_DRHAP;


                //Div20_T0DRHAP  = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum20_T0DRHAP / Sum20_T0CRAHP), 3) * 1000))) / 10;
                //Div20_S0DRHAP  = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum20_S0DRHAP / Sum20_S0CRHAP), 3) * 1000))) / 10;
                //Div20_B82DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum20_B82DRHAP / Sum20_B82CRHAP), 3) * 1000))) / 10;
                //Div20_B81DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum20_B81DRHAP / Sum20_B81CRHAP), 3) * 1000))) / 10;
                //Div20_B8DRHAP  = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum20_B8DRHAP / Sum20_B8CRHAP), 3) * 1000))) / 10;
                //Div20_DRHAP    = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum20_DRHAP / Sum20_CRHAP), 3) * 1000))) / 10;

                if (Convert.ToDouble(Sum20_T0CRAHP) > 0 && Sum20_T0DRHAP > 0)
                {
                    Div20_T0DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum20_T0DRHAP / Sum20_T0CRAHP), 3) * 1000))) / 10;
                }
                else
                {
                    Div20_T0DRHAP = 0;
                }
                if (Convert.ToDouble(Sum20_S0CRHAP) > 0 && Sum20_S0DRHAP > 0)
                {
                    Div20_S0DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum20_S0DRHAP / Sum20_S0CRHAP), 3) * 1000))) / 10;
                }
                else
                {
                    Div20_S0DRHAP = 0;
                }
                if (Convert.ToDouble(Sum20_B82CRHAP) > 0 && Sum20_B82DRHAP > 0)
                {
                    Div20_B82DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum20_B82DRHAP / Sum20_B82CRHAP), 3) * 1000))) / 10;
                }
                else
                {
                    Div20_B82DRHAP = 0;
                }
                if (Convert.ToDouble(Sum20_B81CRHAP) > 0 && Sum20_B81DRHAP > 0)
                {
                    Div20_B81DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum20_B81DRHAP / Sum20_B81CRHAP), 3) * 1000))) / 10;
                }
                else
                {
                    Div20_B81DRHAP = 0;
                }
                if (Convert.ToDouble(Sum20_B8CRHAP) > 0 && Sum20_B8DRHAP > 0)
                {
                    Div20_B8DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum20_B8DRHAP / Sum20_B8CRHAP), 3) * 1000))) / 10;
                }
                else
                {
                    Div20_B8DRHAP = 0;
                }
                if (Convert.ToDouble(Sum20_CRHAP) > 0 && Sum20_DRHAP > 0)
                {
                    Div20_DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum20_DRHAP / Sum20_CRHAP), 3) * 1000))) / 10;
                }
                else
                {
                    Div20_DRHAP = 0;
                }


                // 세전순이익 (20000000)
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i]["APACDAC"].ToString() == "20000000")
                    {
                        table.Rows[i]["T0CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum20_T0CRAHP));
                        table.Rows[i]["T0DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum20_T0DRHAP));
                        table.Rows[i]["S0CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum20_S0CRHAP));
                        table.Rows[i]["S0DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum20_S0DRHAP));
                        table.Rows[i]["B82CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum20_B82CRHAP));
                        table.Rows[i]["B82DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum20_B82DRHAP));
                        table.Rows[i]["B81CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum20_B81CRHAP));
                        table.Rows[i]["B81DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum20_B81DRHAP));
                        table.Rows[i]["B8CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum20_B8CRHAP));
                        table.Rows[i]["B8DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum20_B8DRHAP));
                        table.Rows[i]["CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum20_CRHAP));
                        table.Rows[i]["DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum20_DRHAP));
                    }

                    if (table.Rows[i]["APACDAC"].ToString() == "20000001")
                    {
                        //if (Div20_T0DRHAP > 0)
                        //{ table.Rows[i]["T0DRHAP"] = Div20_T0DRHAP; }
                        //else { table.Rows[i]["T0DRHAP"] = 0; }

                        //if (Div20_S0DRHAP > 0)
                        //{ table.Rows[i]["S0DRHAP"] = Div20_S0DRHAP; }
                        //else { table.Rows[i]["S0DRHAP"] = 0; }

                        //if (Div20_B82DRHAP > 0)
                        //{ table.Rows[i]["B82DRHAP"] = Div20_B82DRHAP; }
                        //else { table.Rows[i]["B82DRHAP"] = 0; }

                        //if (Div20_B81DRHAP > 0)
                        //{ table.Rows[i]["B81DRHAP"] = Div20_B81DRHAP; }
                        //else { table.Rows[i]["B81DRHAP"] = 0; }

                        //if (Div20_B8DRHAP > 0)
                        //{ table.Rows[i]["B8DRHAP"] = Div20_B8DRHAP; }
                        //else { table.Rows[i]["B8DRHAP"] = 0; }

                        //if (Div20_DRHAP > 0)
                        //{ table.Rows[i]["DRHAP"] = Div20_DRHAP; }
                        //else { table.Rows[i]["DRHAP"] = 0; }


                        table.Rows[i]["T0DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div20_T0DRHAP));
                        table.Rows[i]["S0DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div20_S0DRHAP));
                        table.Rows[i]["B82DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div20_B82DRHAP));
                        table.Rows[i]["B81DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div20_B81DRHAP));
                        table.Rows[i]["B8DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div20_B8DRHAP));
                        table.Rows[i]["DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div20_DRHAP));
                    }
                } // End..For 세전순이익 (20000000)

                #endregion 

            }
            else // 2012년 01월  ~ 2013년 12월
            {
                #region Description :   2012년 01월  ~ 2013년 12월
		
                double   Sum07_T0CRAHP  = 0 ;
			    double   Sum07_T0DRHAP  = 0 ;
			    double   Sum07_S0CRHAP  = 0 ;
			    double   Sum07_S0DRHAP  = 0 ;
			    double   Sum07_B82CRHAP = 0 ;
			    double   Sum07_B82DRHAP = 0 ;
			    double   Sum07_B81CRHAP = 0 ;
			    double   Sum07_B81DRHAP = 0 ;
			    double   Sum07_B8CRHAP  = 0 ;
			    double   Sum07_B8DRHAP  = 0 ;
			    double   Sum07_A5CRHAP  = 0 ;
			    double   Sum07_A5DRHAP  = 0 ;
			    double   Sum07_CRHAP    = 0 ;
			    double   Sum07_DRHAP    = 0 ;

			    double   Sum08_T0CRAHP  = 0 ;
			    double   Sum08_T0DRHAP  = 0 ;
			    double   Sum08_S0CRHAP  = 0 ;
			    double   Sum08_S0DRHAP  = 0 ;
			    double   Sum08_B82CRHAP = 0 ;
			    double   Sum08_B82DRHAP = 0 ;
			    double   Sum08_B81CRHAP = 0 ;
			    double   Sum08_B81DRHAP = 0 ;
			    double   Sum08_B8CRHAP  = 0 ;
			    double   Sum08_B8DRHAP  = 0 ;
			    double   Sum08_A5CRHAP  = 0 ;
			    double   Sum08_A5DRHAP  = 0 ;
			    double   Sum08_CRHAP    = 0 ;
			    double   Sum08_DRHAP    = 0 ;

			    double   Sum09_T0CRAHP  = 0 ;
			    double   Sum09_T0DRHAP  = 0 ;
			    double   Sum09_S0CRHAP  = 0 ;
			    double   Sum09_S0DRHAP  = 0 ;
			    double   Sum09_B82CRHAP = 0 ;
			    double   Sum09_B82DRHAP = 0 ;
			    double   Sum09_B81CRHAP = 0 ;
			    double   Sum09_B81DRHAP = 0 ;
			    double   Sum09_B8CRHAP  = 0 ;
			    double   Sum09_B8DRHAP  = 0 ;
			    double   Sum09_A5CRHAP  = 0 ;
			    double   Sum09_A5DRHAP  = 0 ;
			    double   Sum09_CRHAP    = 0 ;
			    double   Sum09_DRHAP    = 0 ;

			    double   Sum10_T0CRAHP  = 0 ;
			    double   Sum10_T0DRHAP  = 0 ;
			    double   Sum10_S0CRHAP  = 0 ;
			    double   Sum10_S0DRHAP  = 0 ;
			    double   Sum10_B82CRHAP = 0 ;
			    double   Sum10_B82DRHAP = 0 ;
			    double   Sum10_B81CRHAP = 0 ;
			    double   Sum10_B81DRHAP = 0 ;
			    double   Sum10_B8CRHAP  = 0 ;
			    double   Sum10_B8DRHAP  = 0 ;
			    double   Sum10_A5CRHAP  = 0 ;
			    double   Sum10_A5DRHAP  = 0 ;
			    double   Sum10_CRHAP    = 0 ;
			    double   Sum10_DRHAP    = 0 ;

			    double   Sum11_T0CRAHP  = 0 ;
			    double   Sum11_T0DRHAP  = 0 ;
			    double   Sum11_S0CRHAP  = 0 ;
			    double   Sum11_S0DRHAP  = 0 ;
			    double   Sum11_B82CRHAP = 0 ;
			    double   Sum11_B82DRHAP = 0 ;
			    double   Sum11_B81CRHAP = 0 ;
			    double   Sum11_B81DRHAP = 0 ;
			    double   Sum11_B8CRHAP  = 0 ;
			    double   Sum11_B8DRHAP  = 0 ;
			    double   Sum11_A5CRHAP  = 0 ;
			    double   Sum11_A5DRHAP  = 0 ;
			    double   Sum11_CRHAP    = 0 ;
			    double   Sum11_DRHAP    = 0 ;

			    double   Sum12_T0CRAHP  = 0 ;
			    double   Sum12_T0DRHAP  = 0 ;
			    double   Sum12_S0CRHAP  = 0 ;
			    double   Sum12_S0DRHAP  = 0 ;
			    double   Sum12_B82CRHAP = 0 ;
			    double   Sum12_B82DRHAP = 0 ;
			    double   Sum12_B81CRHAP = 0 ;
			    double   Sum12_B81DRHAP = 0 ;
			    double   Sum12_B8CRHAP  = 0 ;
			    double   Sum12_B8DRHAP  = 0 ;
			    double   Sum12_A5CRHAP  = 0 ;
			    double   Sum12_A5DRHAP  = 0 ;
			    double   Sum12_CRHAP    = 0 ;
			    double   Sum12_DRHAP    = 0 ;

			    double   Sum13_T0CRAHP  = 0 ;
			    double   Sum13_T0DRHAP  = 0 ;
			    double   Sum13_S0CRHAP  = 0 ;
			    double   Sum13_S0DRHAP  = 0 ;
			    double   Sum13_B82CRHAP = 0 ;
			    double   Sum13_B82DRHAP = 0 ;
			    double   Sum13_B81CRHAP = 0 ;
			    double   Sum13_B81DRHAP = 0 ;
			    double   Sum13_B8CRHAP  = 0 ;
			    double   Sum13_B8DRHAP  = 0 ;
			    double   Sum13_A5CRHAP  = 0 ;
			    double   Sum13_A5DRHAP  = 0 ;
			    double   Sum13_CRHAP    = 0 ;
			    double   Sum13_DRHAP    = 0 ;

			    double   Sum20_T0CRAHP  = 0 ;
			    double   Sum20_T0DRHAP  = 0 ;
			    double   Sum20_S0CRHAP  = 0 ;
			    double   Sum20_S0DRHAP  = 0 ;
			    double   Sum20_B82CRHAP = 0 ;
			    double   Sum20_B82DRHAP = 0 ;
			    double   Sum20_B81CRHAP = 0 ;
			    double   Sum20_B81DRHAP = 0 ;
			    double   Sum20_B8CRHAP  = 0 ;
			    double   Sum20_B8DRHAP  = 0 ;
			    double   Sum20_A5CRHAP  = 0 ;
			    double   Sum20_A5DRHAP  = 0 ;
			    double   Sum20_CRHAP    = 0 ;
			    double   Sum20_DRHAP    = 0 ;

			    double   Div10_T0DRHAP  = 0 ;
			    double   Div10_S0DRHAP  = 0 ;
			    double   Div10_B82DRHAP = 0 ;
			    double   Div10_B81DRHAP = 0 ;
			    double   Div10_B8DRHAP  = 0 ;
			    double   Div10_A5DRHAP  = 0 ;
			    double   Div10_DRHAP    = 0 ;

			    double   Div20_T0DRHAP  = 0 ;
			    double   Div20_S0DRHAP  = 0 ;
			    double   Div20_B82DRHAP = 0 ;
			    double   Div20_B81DRHAP = 0 ;
			    double   Div20_B8DRHAP  = 0 ;
			    double   Div20_A5DRHAP  = 0 ;
			    double   Div20_DRHAP    = 0 ;

                for (int i = 0; i < table.Rows.Count; i++)
                {

                    if (table.Rows[i]["APACDAC"].ToString() == "08000000" ||
                        table.Rows[i]["APACDAC"].ToString() == "09000000" ||
                        table.Rows[i]["APACDAC"].ToString() == "20000000")
                    {
                        table.Rows[i]["T0CRHAP"]  = 0;
                        table.Rows[i]["T0DRHAP"]  = 0;
                        table.Rows[i]["S0CRHAP"]  = 0;
                        table.Rows[i]["S0DRHAP"]  = 0;
                        table.Rows[i]["B82CRHAP"] = 0;
                        table.Rows[i]["B82DRHAP"] = 0;
                        table.Rows[i]["B81CRHAP"] = 0;
                        table.Rows[i]["B81DRHAP"] = 0;
                        table.Rows[i]["B8CRHAP"]  = 0;
                        table.Rows[i]["B8DRHAP"]  = 0;
                        table.Rows[i]["A5CRHAP"]  = 0;
                        table.Rows[i]["A5DRHAP"]  = 0;
                        table.Rows[i]["CRHAP"]    = 0;
                        table.Rows[i]["DRHAP"]    = 0;
                    }
                    else
                    {
                        if (table.Rows[i]["APACDAC"].ToString().Substring(7, 1) == "1")
                        {
                            table.Rows[i]["T0CRHAP"]  = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(Get_Numeric(table.Rows[i]["T0CRHAP"].ToString())) / 10));
                            table.Rows[i]["T0DRHAP"]  = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(Get_Numeric(table.Rows[i]["T0DRHAP"].ToString())) / 10));
                            table.Rows[i]["S0CRHAP"]  = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(Get_Numeric(table.Rows[i]["S0CRHAP"].ToString())) / 10));
                            table.Rows[i]["S0DRHAP"]  = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(Get_Numeric(table.Rows[i]["S0DRHAP"].ToString())) / 10));
                            table.Rows[i]["B82CRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(Get_Numeric(table.Rows[i]["B82CRHAP"].ToString())) / 10));
                            table.Rows[i]["B82DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(Get_Numeric(table.Rows[i]["B82DRHAP"].ToString())) / 10));
                            table.Rows[i]["B81CRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(Get_Numeric(table.Rows[i]["B81CRHAP"].ToString())) / 10));
                            table.Rows[i]["B81DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(Get_Numeric(table.Rows[i]["B81DRHAP"].ToString())) / 10));
                            table.Rows[i]["B8CRHAP"]  = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(Get_Numeric(table.Rows[i]["B8CRHAP"].ToString())) / 10));
                            table.Rows[i]["B8DRHAP"]  = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(Get_Numeric(table.Rows[i]["B8DRHAP"].ToString())) / 10));
                            table.Rows[i]["A5CRHAP"]  = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(Get_Numeric(table.Rows[i]["A5CRHAP"].ToString())) /10));
                            table.Rows[i]["A5DRHAP"]  = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(Get_Numeric(table.Rows[i]["A5DRHAP"].ToString())) / 10));
                            table.Rows[i]["CRHAP"]    = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(Get_Numeric(table.Rows[i]["CRHAP"].ToString())) / 10));
                            table.Rows[i]["DRHAP"]    = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(Get_Numeric(table.Rows[i]["DRHAP"].ToString())) / 10));
                        }
                        else
                        {
                            table.Rows[i]["T0CRHAP"]  = Convert.ToString(string.Format("{0:#,###}", double.Parse(Get_Numeric(table.Rows[i]["T0CRHAP"].ToString()))));
                            table.Rows[i]["T0DRHAP"]  = Convert.ToString(string.Format("{0:#,###}", double.Parse(Get_Numeric(table.Rows[i]["T0DRHAP"].ToString()))));
                            table.Rows[i]["S0CRHAP"]  = Convert.ToString(string.Format("{0:#,###}", double.Parse(Get_Numeric(table.Rows[i]["S0CRHAP"].ToString()))));
                            table.Rows[i]["S0DRHAP"]  = Convert.ToString(string.Format("{0:#,###}", double.Parse(Get_Numeric(table.Rows[i]["S0DRHAP"].ToString()))));
                            table.Rows[i]["B82CRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(Get_Numeric(table.Rows[i]["B82CRHAP"].ToString()))));
                            table.Rows[i]["B82DRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(Get_Numeric(table.Rows[i]["B82DRHAP"].ToString()))));
                            table.Rows[i]["B81CRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(Get_Numeric(table.Rows[i]["B81CRHAP"].ToString()))));
                            table.Rows[i]["B81DRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(Get_Numeric(table.Rows[i]["B81DRHAP"].ToString()))));
                            table.Rows[i]["B8CRHAP"]  = Convert.ToString(string.Format("{0:#,###}", double.Parse(Get_Numeric(table.Rows[i]["B8CRHAP"].ToString()))));
                            table.Rows[i]["B8DRHAP"]  = Convert.ToString(string.Format("{0:#,###}", double.Parse(Get_Numeric(table.Rows[i]["B8DRHAP"].ToString()))));
                            table.Rows[i]["A5CRHAP"]  = Convert.ToString(string.Format("{0:#,###}", double.Parse(Get_Numeric(table.Rows[i]["A5CRHAP"].ToString()))));
                            table.Rows[i]["A5DRHAP"]  = Convert.ToString(string.Format("{0:#,###}", double.Parse(Get_Numeric(table.Rows[i]["A5DRHAP"].ToString()))));
                            table.Rows[i]["CRHAP"]    = Convert.ToString(string.Format("{0:#,###}", double.Parse(Get_Numeric(table.Rows[i]["CRHAP"].ToString()))));
                            table.Rows[i]["DRHAP"]    = Convert.ToString(string.Format("{0:#,###}", double.Parse(Get_Numeric(table.Rows[i]["DRHAP"].ToString()))));
                        }
                    }
                }

                for (int i = 0; i < table.Rows.Count; i++)
                {

                    // 07000000	영업이익2 
                    if (table.Rows[i]["APACDAC"].ToString() == "07000000")
                    {
                        Sum07_T0CRAHP  = Sum07_T0CRAHP + Convert.ToDouble(Get_Numeric(table.Rows[i]["T0CRHAP"].ToString()));
                        Sum07_T0DRHAP  = Sum07_T0DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["T0DRHAP"].ToString()));
                        Sum07_S0CRHAP  = Sum07_S0CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["S0CRHAP"].ToString()));
                        Sum07_S0DRHAP  = Sum07_S0DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["S0DRHAP"].ToString()));
                        Sum07_B82CRHAP = Sum07_B82CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B82CRHAP"].ToString()));
                        Sum07_B82DRHAP = Sum07_B82DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B82DRHAP"].ToString()));
                        Sum07_B81CRHAP = Sum07_B81CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B81CRHAP"].ToString()));
                        Sum07_B81DRHAP = Sum07_B81DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B81DRHAP"].ToString()));
                        Sum07_B8CRHAP  = Sum07_B8CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B8CRHAP"].ToString()));
                        Sum07_B8DRHAP  = Sum07_B8DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B8DRHAP"].ToString()));
                        Sum07_A5CRHAP  = Sum07_A5CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["A5CRHAP"].ToString()));
                        Sum07_A5DRHAP  = Sum07_A5DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["A5DRHAP"].ToString()));
                        Sum07_CRHAP    = Sum07_CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["CRHAP"].ToString()));
                        Sum07_DRHAP    = Sum07_DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["DRHAP"].ToString()));
                    }

                    // 08000000	영업외수익
                    if (table.Rows[i]["APACDAC"].ToString().Substring(0, 2) == "08")
                    {
                        Sum08_T0CRAHP  = Sum08_T0CRAHP + Convert.ToDouble(Get_Numeric(table.Rows[i]["T0CRHAP"].ToString()));
                        Sum08_T0DRHAP  = Sum08_T0DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["T0DRHAP"].ToString()));
                        Sum08_S0CRHAP  = Sum08_S0CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["S0CRHAP"].ToString()));
                        Sum08_S0DRHAP  = Sum08_S0DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["S0DRHAP"].ToString()));
                        Sum08_B82CRHAP = Sum08_B82CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B82CRHAP"].ToString()));
                        Sum08_B82DRHAP = Sum08_B82DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B82DRHAP"].ToString()));
                        Sum08_B81CRHAP = Sum08_B81CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B81CRHAP"].ToString()));
                        Sum08_B81DRHAP = Sum08_B81DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B81DRHAP"].ToString()));
                        Sum08_B8CRHAP  = Sum08_B8CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B8CRHAP"].ToString()));
                        Sum08_B8DRHAP  = Sum08_B8DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B8DRHAP"].ToString()));
                        Sum08_A5CRHAP  = Sum08_A5CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["A5CRHAP"].ToString()));
                        Sum08_A5DRHAP  = Sum08_A5DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["A5DRHAP"].ToString()));
                        Sum08_CRHAP    = Sum08_CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["CRHAP"].ToString()));
                        Sum08_DRHAP    = Sum08_DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["DRHAP"].ToString()));
                    }

                    // 09000000	영업외비용
                    if (table.Rows[i]["APACDAC"].ToString().Substring(0, 2) == "09")
                    {
                        Sum09_T0CRAHP  = Sum09_T0CRAHP + Convert.ToDouble(Get_Numeric(table.Rows[i]["T0CRHAP"].ToString()));
                        Sum09_T0DRHAP  = Sum09_T0DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["T0DRHAP"].ToString()));
                        Sum09_S0CRHAP  = Sum09_S0CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["S0CRHAP"].ToString()));
                        Sum09_S0DRHAP  = Sum09_S0DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["S0DRHAP"].ToString()));
                        Sum09_B82CRHAP = Sum09_B82CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B82CRHAP"].ToString()));
                        Sum09_B82DRHAP = Sum09_B82DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B82DRHAP"].ToString()));
                        Sum09_B81CRHAP = Sum09_B81CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B81CRHAP"].ToString()));
                        Sum09_B81DRHAP = Sum09_B81DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B81DRHAP"].ToString()));
                        Sum09_B8CRHAP  = Sum09_B8CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B8CRHAP"].ToString()));
                        Sum09_B8DRHAP  = Sum09_B8DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B8DRHAP"].ToString()));
                        Sum09_A5CRHAP  = Sum09_A5CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["A5CRHAP"].ToString()));
                        Sum09_A5DRHAP  = Sum09_A5DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["A5DRHAP"].ToString()));
                        Sum09_CRHAP    = Sum09_CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["CRHAP"].ToString()));
                        Sum09_DRHAP    = Sum09_DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["DRHAP"].ToString()));
                    }

                    // 11000000	외환손익(재무)
                    if (table.Rows[i]["APACDAC"].ToString() == "11000000")
                    {
                        Sum11_T0CRAHP  = 0;
                        Sum11_T0DRHAP  = 0;
                        Sum11_S0CRHAP  = 0;
                        Sum11_S0DRHAP  = 0;
                        Sum11_B82CRHAP = 0;
                        Sum11_B82DRHAP = 0;
                        Sum11_B81CRHAP = 0;
                        Sum11_B81DRHAP = 0;
                        Sum11_B8CRHAP  = 0;
                        Sum11_B8DRHAP  = 0;
                        Sum11_A5CRHAP  = 0;
                        Sum11_A5DRHAP  = 0;
                        Sum11_CRHAP    = Sum11_CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["CRHAP"].ToString()));
                        Sum11_DRHAP    = Sum11_DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["DRHAP"].ToString()));
                    }

                    // 12000000	투자이자(P.J)
                    if (table.Rows[i]["APACDAC"].ToString() == "12000000")
                    {
                        Sum12_T0CRAHP  = 0;
                        Sum12_T0DRHAP  = 0;
                        Sum12_S0CRHAP  = 0;
                        Sum12_S0DRHAP  = 0;
                        Sum12_B82CRHAP = 0;
                        Sum12_B82DRHAP = 0;
                        Sum12_B81CRHAP = 0;
                        Sum12_B81DRHAP = 0;
                        Sum12_B8CRHAP  = 0;
                        Sum12_B8DRHAP  = 0;
                        Sum12_A5CRHAP  = 0;
                        Sum12_A5DRHAP  = 0;
                        Sum12_CRHAP    = Sum12_CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["CRHAP"].ToString()));
                        Sum12_DRHAP    = Sum12_DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["DRHAP"].ToString()));
                    }

                    // 13000000	지분법손익
                    if (table.Rows[i]["APACDAC"].ToString() == "13000000")
                    {
                        Sum13_T0CRAHP  = Sum13_T0CRAHP + Convert.ToDouble(Get_Numeric(table.Rows[i]["T0CRHAP"].ToString()));
                        Sum13_T0DRHAP  = Sum13_T0DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["T0DRHAP"].ToString()));
                        Sum13_S0CRHAP  = Sum13_S0CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["S0CRHAP"].ToString()));
                        Sum13_S0DRHAP  = Sum13_S0DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["S0DRHAP"].ToString()));
                        Sum13_B82CRHAP = Sum13_B82CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B82CRHAP"].ToString()));
                        Sum13_B82DRHAP = Sum13_B82DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B82DRHAP"].ToString()));
                        Sum13_B81CRHAP = Sum13_B81CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B81CRHAP"].ToString()));
                        Sum13_B81DRHAP = Sum13_B81DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B81DRHAP"].ToString()));
                        Sum13_B8CRHAP  = Sum13_B8CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B8CRHAP"].ToString()));
                        Sum13_B8DRHAP  = Sum13_B8DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B8DRHAP"].ToString()));
                        Sum13_A5CRHAP  = Sum13_A5CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["A5CRHAP"].ToString()));
                        Sum13_A5DRHAP  = Sum13_A5DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["A5DRHAP"].ToString()));
                        Sum13_CRHAP    = Sum13_CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["CRHAP"].ToString()));
                        Sum13_DRHAP    = Sum13_DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["DRHAP"].ToString()));
                    }

                }

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i]["APACDAC"].ToString() == "08000000")
                    {
                        table.Rows[i]["T0CRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum08_T0CRAHP));
                        table.Rows[i]["T0DRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum08_T0DRHAP));
                        table.Rows[i]["S0CRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum08_S0CRHAP));
                        table.Rows[i]["S0DRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum08_S0DRHAP));
                        table.Rows[i]["B82CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum08_B82CRHAP));
                        table.Rows[i]["B82DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum08_B82DRHAP));
                        table.Rows[i]["B81CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum08_B81CRHAP));
                        table.Rows[i]["B81DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum08_B81DRHAP));
                        table.Rows[i]["B8CRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum08_B8CRHAP));
                        table.Rows[i]["B8DRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum08_B8DRHAP));
                        table.Rows[i]["A5CRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum08_A5CRHAP));
                        table.Rows[i]["A5DRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum08_A5DRHAP));
                        table.Rows[i]["CRHAP"]    = Convert.ToString(string.Format("{0:#,###}", Sum08_CRHAP));
                        table.Rows[i]["DRHAP"]    = Convert.ToString(string.Format("{0:#,###}", Sum08_DRHAP));
                    }
                }

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i]["APACDAC"].ToString() == "09000000")
                    {
                        table.Rows[i]["T0CRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum09_T0CRAHP));
                        table.Rows[i]["T0DRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum09_T0DRHAP));
                        table.Rows[i]["S0CRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum09_S0CRHAP));
                        table.Rows[i]["S0DRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum09_S0DRHAP));
                        table.Rows[i]["B82CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum09_B82CRHAP));
                        table.Rows[i]["B82DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum09_B82DRHAP));
                        table.Rows[i]["B81CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum09_B81CRHAP));
                        table.Rows[i]["B81DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum09_B81DRHAP));
                        table.Rows[i]["B8CRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum09_B8CRHAP));
                        table.Rows[i]["B8DRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum09_B8DRHAP));
                        table.Rows[i]["A5CRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum09_A5CRHAP));
                        table.Rows[i]["A5DRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum09_A5DRHAP));
                        table.Rows[i]["CRHAP"]    = Convert.ToString(string.Format("{0:#,###}", Sum09_CRHAP));
                        table.Rows[i]["DRHAP"]    = Convert.ToString(string.Format("{0:#,###}", Sum09_DRHAP));
                    }
                }

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i]["APACDAC"].ToString() == "09000000")
                    {
                        table.Rows[i]["T0CRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum09_T0CRAHP));
                        table.Rows[i]["T0DRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum09_T0DRHAP));
                        table.Rows[i]["S0CRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum09_S0CRHAP));
                        table.Rows[i]["S0DRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum09_S0DRHAP));
                        table.Rows[i]["B82CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum09_B82CRHAP));
                        table.Rows[i]["B82DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum09_B82DRHAP));
                        table.Rows[i]["B81CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum09_B81CRHAP));
                        table.Rows[i]["B81DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum09_B81DRHAP));
                        table.Rows[i]["B8CRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum09_B8CRHAP));
                        table.Rows[i]["B8DRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum09_B8DRHAP));
                        table.Rows[i]["A5CRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum09_A5CRHAP));
                        table.Rows[i]["A5DRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum09_A5DRHAP));
                        table.Rows[i]["CRHAP"]    = Convert.ToString(string.Format("{0:#,###}", Sum09_CRHAP));
                        table.Rows[i]["DRHAP"]    = Convert.ToString(string.Format("{0:#,###}", Sum09_DRHAP));
                    }
                }

                // 10000000  경상이익
                // 경상이익 =  영업이익 +  영업외수익 -  영업외비용
                Sum10_T0CRAHP  = Sum07_T0CRAHP + Sum08_T0CRAHP - Sum09_T0CRAHP;
                Sum10_T0DRHAP  = Sum07_T0DRHAP + Sum08_T0DRHAP - Sum09_T0DRHAP;
                Sum10_S0CRHAP  = Sum07_S0CRHAP + Sum08_S0CRHAP - Sum09_S0CRHAP;
                Sum10_S0DRHAP  = Sum07_S0DRHAP + Sum08_S0DRHAP - Sum09_S0DRHAP;
                Sum10_B82CRHAP = Sum07_B82CRHAP + Sum08_B82CRHAP - Sum09_B82CRHAP;
                Sum10_B82DRHAP = Sum07_B82DRHAP + Sum08_B82DRHAP - Sum09_B82DRHAP;
                Sum10_B81CRHAP = Sum07_B81CRHAP + Sum08_B81CRHAP - Sum09_B81CRHAP;
                Sum10_B81DRHAP = Sum07_B81DRHAP + Sum08_B81DRHAP - Sum09_B81DRHAP;
                Sum10_B8CRHAP  = Sum07_B8CRHAP + Sum08_B8CRHAP - Sum09_B8CRHAP;
                Sum10_B8DRHAP  = Sum07_B8DRHAP + Sum08_B8DRHAP - Sum09_B8DRHAP;
                Sum10_A5CRHAP  = Sum07_A5CRHAP + Sum08_A5CRHAP - Sum09_A5CRHAP;
                Sum10_A5DRHAP  = Sum07_A5DRHAP + Sum08_A5DRHAP - Sum09_A5DRHAP;
                Sum10_CRHAP    = Sum07_CRHAP + Sum08_CRHAP - Sum09_CRHAP;
                Sum10_DRHAP    = Sum07_DRHAP + Sum08_DRHAP - Sum09_DRHAP;

                if (Convert.ToDouble(Sum10_T0CRAHP) > 0 && Sum10_T0DRHAP > 0)
                {
                    Div10_T0DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum10_T0DRHAP / Sum10_T0CRAHP), 3) * 1000))) / 10;
                }
                else
                {
                    Div10_T0DRHAP = 0;
                }
                if (Convert.ToDouble(Sum10_S0CRHAP) > 0 && Sum10_S0DRHAP > 0)
                {
                    Div10_S0DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum10_S0DRHAP / Sum10_S0CRHAP), 3) * 1000))) / 10;
                }
                else
                {
                    Div10_S0DRHAP = 0;
                }
                if (Convert.ToDouble(Sum10_B82CRHAP) > 0 && Sum10_B82DRHAP > 0)
                {
                    Div10_B82DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum10_B82DRHAP / Sum10_B82CRHAP), 3) * 1000))) / 10;
                }
                else
                {
                    Div10_B82DRHAP = 0;
                }

                if (Convert.ToDouble(Sum10_B81CRHAP) > 0 && Sum10_B81DRHAP > 0)
                {
                    Div10_B81DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum10_B81DRHAP / Sum10_B81CRHAP), 3) * 1000))) / 10;
                }
                else
                {
                    Div10_B81DRHAP = 0;
                }

                if (Convert.ToDouble(Sum10_B8CRHAP) > 0 && Sum10_B8DRHAP > 0)
                {
                    Div10_B8DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum10_B8DRHAP / Sum10_B8CRHAP), 3) * 1000))) / 10;
                }
                else
                {
                    Div10_B8DRHAP = 0;
                }

                if (Convert.ToDouble(Sum10_A5CRHAP) > 0 && Sum10_A5DRHAP > 0)
                {
                    Div10_A5DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum10_A5DRHAP / Sum10_A5CRHAP), 3) * 1000))) / 10;
                }
                else
                {
                    Div10_A5DRHAP = 0;
                }
                if (Convert.ToDouble(Sum10_CRHAP) > 0 && Sum10_DRHAP > 0)
                {
                    Div10_DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum10_DRHAP / Sum10_CRHAP), 3) * 1000))) / 10;
                }
                else
                {
                    Div10_DRHAP = 0;
                }

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i]["APACDAC"].ToString() == "10000000")
                    {
                        table.Rows[i]["T0CRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum10_T0CRAHP));
                        table.Rows[i]["T0DRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum10_T0DRHAP));
                        table.Rows[i]["S0CRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum10_S0CRHAP));
                        table.Rows[i]["S0DRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum10_S0DRHAP));
                        table.Rows[i]["B82CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum10_B82CRHAP));
                        table.Rows[i]["B82DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum10_B82DRHAP));
                        table.Rows[i]["B81CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum10_B81CRHAP));
                        table.Rows[i]["B81DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum10_B81DRHAP));
                        table.Rows[i]["B8CRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum10_B8CRHAP));
                        table.Rows[i]["B8DRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum10_B8DRHAP));
                        table.Rows[i]["A5CRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum10_A5CRHAP));
                        table.Rows[i]["A5DRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum10_A5DRHAP));
                        table.Rows[i]["CRHAP"]    = Convert.ToString(string.Format("{0:#,###}", Sum10_CRHAP));
                        table.Rows[i]["DRHAP"]    = Convert.ToString(string.Format("{0:#,###}", Sum10_DRHAP));
                    }

                    if (table.Rows[i]["APACDAC"].ToString() == "10000001")
                    {
                        //if (Div10_T0DRHAP > 0)
                        //{ table.Rows[i]["T0DRHAP"] = Div10_T0DRHAP; }
                        //else { table.Rows[i]["T0DRHAP"] = 0; }

                        //if (Div10_S0DRHAP > 0)
                        //{ table.Rows[i]["S0DRHAP"] = Div10_S0DRHAP; }
                        //else { table.Rows[i]["S0DRHAP"] = 0; }

                        //if (Div10_B82DRHAP > 0)
                        //{ table.Rows[i]["B82DRHAP"] = Div10_B82DRHAP; }
                        //else { table.Rows[i]["B82DRHAP"] = 0; }

                        //if (Div10_B81DRHAP > 0)
                        //{ table.Rows[i]["B81DRHAP"] = Div10_B81DRHAP; }
                        //else { table.Rows[i]["B81DRHAP"] = 0; }

                        //if (Div10_B8DRHAP > 0)
                        //{ table.Rows[i]["B8DRHAP"] = Div10_B8DRHAP; }
                        //else { table.Rows[i]["B8DRHAP"] = 0; }

                        //if (Div10_A5DRHAP > 0)
                        //{ table.Rows[i]["A5DRHAP"] = Div10_A5DRHAP; }
                        //else { table.Rows[i]["A5DRHAP"] = 0; }

                        //if (Div10_DRHAP > 0)
                        //{ table.Rows[i]["DRHAP"] = Div10_DRHAP; }
                        //else { table.Rows[i]["DRHAP"] = 0; }

                        table.Rows[i]["T0DRHAP"]  = Convert.ToString(string.Format("{0:#,###.#}", Div10_T0DRHAP));
                        table.Rows[i]["S0DRHAP"]  = Convert.ToString(string.Format("{0:#,###.#}", Div10_S0DRHAP));
                        table.Rows[i]["B82DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div10_B82DRHAP));
                        table.Rows[i]["B81DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div10_B81DRHAP));
                        table.Rows[i]["B8DRHAP"]  = Convert.ToString(string.Format("{0:#,###.#}", Div10_B8DRHAP));
                        table.Rows[i]["A5DRHAP"]  = Convert.ToString(string.Format("{0:#,###.#}", Div10_A5DRHAP));
                        table.Rows[i]["DRHAP"]    = Convert.ToString(string.Format("{0:#,###.#}", Div10_DRHAP));
                    }
                }

                // 20000000	세전순이익
                // 세전순이익 = 경상이익 + 외환손익(재무) -  투자이자(P.J) +  지분법손익
                Sum20_T0CRAHP  = Sum10_T0CRAHP + Sum11_T0CRAHP - Sum12_T0CRAHP + Sum13_T0CRAHP;
                Sum20_T0DRHAP  = Sum10_T0DRHAP + Sum11_T0DRHAP - Sum12_T0DRHAP + Sum13_T0DRHAP;
                Sum20_S0CRHAP  = Sum10_S0CRHAP + Sum11_S0CRHAP - Sum12_S0CRHAP + Sum13_S0CRHAP;
                Sum20_S0DRHAP  = Sum10_S0DRHAP + Sum11_S0DRHAP - Sum12_S0DRHAP + Sum13_S0DRHAP;
                Sum20_B82CRHAP = Sum10_B82CRHAP + Sum11_B82CRHAP - Sum12_B82CRHAP + Sum13_B82CRHAP;
                Sum20_B82DRHAP = Sum10_B82DRHAP + Sum11_B82DRHAP - Sum12_B82DRHAP + Sum13_B82DRHAP;
                Sum20_B81CRHAP = Sum10_B81CRHAP + Sum11_B81CRHAP - Sum12_B81CRHAP + Sum13_B81CRHAP;
                Sum20_B81DRHAP = Sum10_B81DRHAP + Sum11_B81DRHAP - Sum12_B81DRHAP + Sum13_B81DRHAP;
                Sum20_B8CRHAP  = Sum10_B8CRHAP + Sum11_B8CRHAP - Sum12_B8CRHAP + Sum13_B8CRHAP;
                Sum20_B8DRHAP  = Sum10_B8DRHAP + Sum11_B8DRHAP - Sum12_B8DRHAP + Sum13_B8DRHAP;
                Sum20_A5CRHAP  = Sum10_A5CRHAP + Sum11_A5CRHAP - Sum12_A5CRHAP + Sum13_A5CRHAP;
                Sum20_A5DRHAP  = Sum10_A5DRHAP + Sum11_A5DRHAP - Sum12_A5DRHAP + Sum13_A5DRHAP;
                Sum20_CRHAP    = Sum10_CRHAP + Sum11_CRHAP - Sum12_CRHAP + Sum13_CRHAP;
                Sum20_DRHAP    = Sum10_DRHAP + Sum11_DRHAP - Sum12_DRHAP + Sum13_DRHAP;


                if (Convert.ToDouble(Sum20_T0CRAHP) > 0 && Sum20_T0DRHAP > 0)
                {
                    Div20_T0DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum20_T0DRHAP / Sum20_T0CRAHP), 3) * 1000))) / 10;
                }
                else
                {
                    Div20_T0DRHAP = 0;
                }

                if (Convert.ToDouble(Sum20_S0CRHAP) > 0 && Sum20_S0DRHAP > 0)
                {
                    Div20_S0DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum20_S0DRHAP / Sum20_S0CRHAP), 3) * 1000))) / 10;
                }
                else
                {
                    Div20_S0DRHAP = 0;
                }

                if (Convert.ToDouble(Sum20_B82CRHAP) > 0 && Sum20_B82DRHAP > 0)
                {
                    Div20_B82DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum20_B82DRHAP / Sum20_B82CRHAP), 3) * 1000))) / 10;
                }
                else
                {
                    Div20_B82DRHAP = 0;
                }

                if (Convert.ToDouble(Sum20_B81CRHAP) > 0 && Sum20_B81DRHAP > 0)
                {
                    Div20_B81DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum20_B81DRHAP / Sum20_B81CRHAP), 3) * 1000))) / 10;
                }
                else
                {
                    Div20_B81DRHAP = 0;
                }

                if (Convert.ToDouble(Sum20_B8CRHAP) > 0 && Sum20_B8DRHAP > 0)
                {
                    Div20_B8DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum20_B8DRHAP / Sum20_B8CRHAP), 3) * 1000))) / 10;
                }
                else
                {
                    Div20_B8DRHAP = 0;
                }

                if (Convert.ToDouble(Sum20_A5CRHAP) > 0 && Sum20_A5DRHAP > 0)
                {
                    Div20_A5DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum20_A5DRHAP / Sum20_A5CRHAP), 3) * 1000))) / 10;
                }
                else
                {
                    Div20_A5DRHAP = 0;
                }

                if (Convert.ToDouble(Sum20_CRHAP) > 0 && Sum20_DRHAP > 0)
                {
                    Div20_DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum20_DRHAP / Sum20_CRHAP), 3) * 1000))) / 10;
                }
                else
                {
                    Div20_DRHAP = 0;
                }


                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i]["APACDAC"].ToString() == "20000000")
                    {
                        table.Rows[i]["T0CRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum20_T0CRAHP));
                        table.Rows[i]["T0DRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum20_T0DRHAP));
                        table.Rows[i]["S0CRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum20_S0CRHAP));
                        table.Rows[i]["S0DRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum20_S0DRHAP));
                        table.Rows[i]["B82CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum20_B82CRHAP));
                        table.Rows[i]["B82DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum20_B82DRHAP));
                        table.Rows[i]["B81CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum20_B81CRHAP));
                        table.Rows[i]["B81DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum20_B81DRHAP));
                        table.Rows[i]["B8CRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum20_B8CRHAP));
                        table.Rows[i]["B8DRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum20_B8DRHAP));
                        table.Rows[i]["A5CRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum20_A5CRHAP));
                        table.Rows[i]["A5DRHAP"]  = Convert.ToString(string.Format("{0:#,###}", Sum20_A5DRHAP));
                        table.Rows[i]["CRHAP"]    = Convert.ToString(string.Format("{0:#,###}", Sum20_CRHAP));
                        table.Rows[i]["DRHAP"]    = Convert.ToString(string.Format("{0:#,###}", Sum20_DRHAP));
                    }

                    if (table.Rows[i]["APACDAC"].ToString() == "20000001")
                    {

                        //if (Div20_T0DRHAP > 0)
                        //{ table.Rows[i]["T0DRHAP"] = Div20_T0DRHAP; }
                        //else { table.Rows[i]["T0DRHAP"] = 0; }

                        //if (Div20_S0DRHAP > 0)
                        //{ table.Rows[i]["S0DRHAP"] = Div20_S0DRHAP; }
                        //else { table.Rows[i]["S0DRHAP"] = 0; }

                        //if (Div20_B82DRHAP > 0)
                        //{ table.Rows[i]["B82DRHAP"] = Div20_B82DRHAP; }
                        //else { table.Rows[i]["B82DRHAP"] = 0; }

                        //if (Div20_B81DRHAP > 0)
                        //{ table.Rows[i]["B81DRHAP"] = Div20_B81DRHAP; }
                        //else { table.Rows[i]["B81DRHAP"] = 0; }

                        //if (Div20_B8DRHAP > 0)
                        //{ table.Rows[i]["B8DRHAP"] = Div20_B8DRHAP; }
                        //else { table.Rows[i]["B8DRHAP"] = 0; }

                        //if (Div20_A5DRHAP > 0)
                        //{ table.Rows[i]["A5DRHAP"] = Div20_A5DRHAP; }
                        //else { table.Rows[i]["A5DRHAP"] = 0; }

                        //if (Div20_DRHAP > 0)
                        //{ table.Rows[i]["DRHAP"] = Div20_DRHAP; }
                        //else { table.Rows[i]["DRHAP"] = 0; }

                        table.Rows[i]["T0DRHAP"]  = Convert.ToString(string.Format("{0:#,###.#}", Div20_T0DRHAP));
                        table.Rows[i]["S0DRHAP"]  = Convert.ToString(string.Format("{0:#,###.#}", Div20_S0DRHAP));
                        table.Rows[i]["B82DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div20_B82DRHAP));
                        table.Rows[i]["B81DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div20_B81DRHAP));
                        table.Rows[i]["B8DRHAP"]  = Convert.ToString(string.Format("{0:#,###.#}", Div20_B8DRHAP));
                        table.Rows[i]["A5DRHAP"]  = Convert.ToString(string.Format("{0:#,###.#}", Div20_A5DRHAP));
                        table.Rows[i]["DRHAP"]    = Convert.ToString(string.Format("{0:#,###.#}", Div20_DRHAP));
                    }
                }
 
	#endregion
            }

            return table;
        }
        #endregion

        #region Description : 데이터 테이블 컨버젼(2014년중 기준종료 월이 03월 이상인경우)
        private DataTable UP_ConvertDt_2014_03(DataTable dt)
        {
            DataTable table = new DataTable();

            table = dt;

            double Sum07_T0CRAHP = 0;
            double Sum07_T0DRHAP = 0;
            double Sum07_S0CRHAP = 0;
            double Sum07_S0DRHAP = 0;
            double Sum07_B8CRHAP = 0;
            double Sum07_B8DRHAP = 0;
            double Sum07_CRHAP = 0;
            double Sum07_DRHAP = 0;

            double Sum08_T0CRAHP = 0;
            double Sum08_T0DRHAP = 0;
            double Sum08_S0CRHAP = 0;
            double Sum08_S0DRHAP = 0;
            double Sum08_B8CRHAP = 0;
            double Sum08_B8DRHAP = 0;
            double Sum08_CRHAP = 0;
            double Sum08_DRHAP = 0;

            double Sum09_T0CRAHP = 0;
            double Sum09_T0DRHAP = 0;
            double Sum09_S0CRHAP = 0;
            double Sum09_S0DRHAP = 0;
            double Sum09_B8CRHAP = 0;
            double Sum09_B8DRHAP = 0;
            double Sum09_CRHAP = 0;
            double Sum09_DRHAP = 0;

            double Sum10_T0CRAHP = 0;
            double Sum10_T0DRHAP = 0;
            double Sum10_S0CRHAP = 0;
            double Sum10_S0DRHAP = 0;
            double Sum10_B8CRHAP = 0;
            double Sum10_B8DRHAP = 0;
            double Sum10_CRHAP = 0;
            double Sum10_DRHAP = 0;

            double Sum11_T0CRAHP = 0;
            double Sum11_T0DRHAP = 0;
            double Sum11_S0CRHAP = 0;
            double Sum11_S0DRHAP = 0;
            double Sum11_B8CRHAP = 0;
            double Sum11_B8DRHAP = 0;
            double Sum11_CRHAP = 0;
            double Sum11_DRHAP = 0;

            double Sum12_T0CRAHP = 0;
            double Sum12_T0DRHAP = 0;
            double Sum12_S0CRHAP = 0;
            double Sum12_S0DRHAP = 0;
            double Sum12_B8CRHAP = 0;
            double Sum12_B8DRHAP = 0;
            double Sum12_CRHAP = 0;
            double Sum12_DRHAP = 0;

            double Sum13_T0CRAHP = 0;
            double Sum13_T0DRHAP = 0;
            double Sum13_S0CRHAP = 0;
            double Sum13_S0DRHAP = 0;
            double Sum13_B8CRHAP = 0;
            double Sum13_B8DRHAP = 0;
            double Sum13_CRHAP = 0;
            double Sum13_DRHAP = 0;

            // 2014.07.25 (사내이자 손익) 추가
            double Sum15_T0CRAHP = 0;
            double Sum15_T0DRHAP = 0;
            double Sum15_S0CRHAP = 0;
            double Sum15_S0DRHAP = 0;
            double Sum15_B8CRHAP = 0;
            double Sum15_B8DRHAP = 0;
            double Sum15_CRHAP = 0;
            double Sum15_DRHAP = 0;

            // 2014.12.31 (결산손익 ) 추가
            double Sum16_T0CRAHP = 0;
            double Sum16_T0DRHAP = 0;
            double Sum16_S0CRHAP = 0;
            double Sum16_S0DRHAP = 0;
            double Sum16_B8CRHAP = 0;
            double Sum16_B8DRHAP = 0;
            double Sum16_CRHAP = 0;
            double Sum16_DRHAP = 0;

            double Sum20_T0CRAHP = 0;
            double Sum20_T0DRHAP = 0;
            double Sum20_S0CRHAP = 0;
            double Sum20_S0DRHAP = 0;
            double Sum20_B8CRHAP = 0;
            double Sum20_B8DRHAP = 0;
            double Sum20_CRHAP = 0;
            double Sum20_DRHAP = 0;

            // 율 계산
            double Div07_T0DRHAP = 0;
            double Div07_S0DRHAP = 0;
            double Div07_B8DRHAP = 0;
            double Div07_DRHAP = 0;

            double Div10_T0DRHAP = 0;
            double Div10_S0DRHAP = 0;
            double Div10_B8DRHAP = 0;
            double Div10_DRHAP = 0;

            double Div20_T0DRHAP = 0;
            double Div20_S0DRHAP = 0;
            double Div20_B8DRHAP = 0;
            double Div20_DRHAP = 0;

            for (int i = 0; i < table.Rows.Count; i++)
            {

                if (table.Rows[i]["APACDAC"].ToString() == "08000000" ||
                    table.Rows[i]["APACDAC"].ToString() == "09000000" ||
                    table.Rows[i]["APACDAC"].ToString() == "20000000")
                {
                    table.Rows[i]["T0CRHAP"] = 0;
                    table.Rows[i]["T0DRHAP"] = 0;
                    table.Rows[i]["S0CRHAP"] = 0;
                    table.Rows[i]["S0DRHAP"] = 0;
                    table.Rows[i]["B8CRHAP"] = 0;
                    table.Rows[i]["B8DRHAP"] = 0;
                    table.Rows[i]["CRHAP"] = 0;
                    table.Rows[i]["DRHAP"] = 0;
                }
                else
                {
                    if (table.Rows[i]["APACDAC"].ToString().Substring(7, 1) == "1")
                    {
                        if (table.Rows[i]["B8DRHAP"].ToString() == "" || table.Rows[i]["B8DRHAP"].ToString() == null)
                        { table.Rows[i]["B8DRHAP"] = "0"; };
                        table.Rows[i]["T0CRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(Get_Numeric(table.Rows[i]["T0CRHAP"].ToString())) / 10));
                        table.Rows[i]["T0DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(Get_Numeric(table.Rows[i]["T0DRHAP"].ToString())) / 10));
                        table.Rows[i]["S0CRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(Get_Numeric(table.Rows[i]["S0CRHAP"].ToString())) / 10));
                        table.Rows[i]["S0DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(Get_Numeric(table.Rows[i]["S0DRHAP"].ToString())) / 10));
                        table.Rows[i]["B8CRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(Get_Numeric(table.Rows[i]["B8CRHAP"].ToString())) / 10));
                        table.Rows[i]["B8DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(Get_Numeric(table.Rows[i]["B8DRHAP"].ToString())) / 10));
                        table.Rows[i]["CRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(Get_Numeric(table.Rows[i]["CRHAP"].ToString())) / 10));
                        table.Rows[i]["DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", double.Parse(Get_Numeric(table.Rows[i]["DRHAP"].ToString())) / 10));
                    }
                    else
                    {
                        table.Rows[i]["T0CRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(Get_Numeric(table.Rows[i]["T0CRHAP"].ToString()))));
                        table.Rows[i]["T0DRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(Get_Numeric(table.Rows[i]["T0DRHAP"].ToString()))));
                        table.Rows[i]["S0CRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(Get_Numeric(table.Rows[i]["S0CRHAP"].ToString()))));
                        table.Rows[i]["S0DRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(Get_Numeric(table.Rows[i]["S0DRHAP"].ToString()))));
                        table.Rows[i]["B8CRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(Get_Numeric(table.Rows[i]["B8CRHAP"].ToString()))));
                        table.Rows[i]["B8DRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(Get_Numeric(table.Rows[i]["B8DRHAP"].ToString()))));
                        table.Rows[i]["CRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(Get_Numeric(table.Rows[i]["CRHAP"].ToString()))));
                        table.Rows[i]["DRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(Get_Numeric(table.Rows[i]["DRHAP"].ToString()))));
                    }
                }

            }

            for (int i = 0; i < table.Rows.Count; i++)
            {

                // 07000000	영업이익2 
                if (table.Rows[i]["APACDAC"].ToString() == "07000000")
                {
                    Sum07_T0CRAHP = Sum07_T0CRAHP + Convert.ToDouble(Get_Numeric(table.Rows[i]["T0CRHAP"].ToString()));
                    Sum07_T0DRHAP = Sum07_T0DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["T0DRHAP"].ToString()));
                    Sum07_S0CRHAP = Sum07_S0CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["S0CRHAP"].ToString()));
                    Sum07_S0DRHAP = Sum07_S0DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["S0DRHAP"].ToString()));
                    Sum07_B8CRHAP = Sum07_B8CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B8CRHAP"].ToString()));
                    Sum07_B8DRHAP = Sum07_B8DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B8DRHAP"].ToString()));
                    Sum07_CRHAP = Sum07_CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["CRHAP"].ToString()));
                    Sum07_DRHAP = Sum07_DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["DRHAP"].ToString()));
                }

                // 08000000	영업외수익
                if (table.Rows[i]["APACDAC"].ToString().Substring(0, 2) == "08")
                {
                    Sum08_T0CRAHP = Sum08_T0CRAHP + Convert.ToDouble(Get_Numeric(table.Rows[i]["T0CRHAP"].ToString()));
                    Sum08_T0DRHAP = Sum08_T0DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["T0DRHAP"].ToString()));
                    Sum08_S0CRHAP = Sum08_S0CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["S0CRHAP"].ToString()));
                    Sum08_S0DRHAP = Sum08_S0DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["S0DRHAP"].ToString()));
                    Sum08_B8CRHAP = Sum08_B8CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B8CRHAP"].ToString()));
                    Sum08_B8DRHAP = Sum08_B8DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B8DRHAP"].ToString()));
                    Sum08_CRHAP = Sum08_CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["CRHAP"].ToString()));
                    Sum08_DRHAP = Sum08_DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["DRHAP"].ToString()));
                }

                // 09000000	영업외비용
                if (table.Rows[i]["APACDAC"].ToString().Substring(0, 2) == "09")
                {
                    Sum09_T0CRAHP = Sum09_T0CRAHP + Convert.ToDouble(Get_Numeric(table.Rows[i]["T0CRHAP"].ToString()));
                    Sum09_T0DRHAP = Sum09_T0DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["T0DRHAP"].ToString()));
                    Sum09_S0CRHAP = Sum09_S0CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["S0CRHAP"].ToString()));
                    Sum09_S0DRHAP = Sum09_S0DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["S0DRHAP"].ToString()));
                    Sum09_B8CRHAP = Sum09_B8CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B8CRHAP"].ToString()));
                    Sum09_B8DRHAP = Sum09_B8DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B8DRHAP"].ToString()));
                    Sum09_CRHAP = Sum09_CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["CRHAP"].ToString()));
                    Sum09_DRHAP = Sum09_DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["DRHAP"].ToString()));
                }

                // 11000000	외환손익(재무)
                if (table.Rows[i]["APACDAC"].ToString() == "11000000")
                {
                    Sum11_T0CRAHP = 0;
                    Sum11_T0DRHAP = 0;
                    Sum11_S0CRHAP = 0;
                    Sum11_S0DRHAP = 0;
                    Sum11_B8CRHAP = 0;
                    Sum11_B8DRHAP = 0;
                    Sum11_CRHAP = Sum11_CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["CRHAP"].ToString()));
                    Sum11_DRHAP = Sum11_DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["DRHAP"].ToString()));
                }

                // 12000000	투자이자(P.J)
                if (table.Rows[i]["APACDAC"].ToString() == "12000000")
                {
                    Sum12_T0CRAHP = 0;
                    Sum12_T0DRHAP = 0;
                    Sum12_S0CRHAP = 0;
                    Sum12_S0DRHAP = 0;
                    Sum12_B8CRHAP = 0;
                    Sum12_B8DRHAP = 0;
                    Sum12_CRHAP = Sum12_CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["CRHAP"].ToString()));
                    Sum12_DRHAP = Sum12_DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["DRHAP"].ToString()));
                }

                // 13000000	지분법손익
                if (table.Rows[i]["APACDAC"].ToString() == "13000000")
                {
                    Sum13_T0CRAHP = Sum13_T0CRAHP + Convert.ToDouble(Get_Numeric(table.Rows[i]["T0CRHAP"].ToString()));
                    Sum13_T0DRHAP = Sum13_T0DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["T0DRHAP"].ToString()));
                    Sum13_S0CRHAP = Sum13_S0CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["S0CRHAP"].ToString()));
                    Sum13_S0DRHAP = Sum13_S0DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["S0DRHAP"].ToString()));
                    Sum13_B8CRHAP = Sum13_B8CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B8CRHAP"].ToString()));
                    Sum13_B8DRHAP = Sum13_B8DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["B8DRHAP"].ToString()));
                    Sum13_CRHAP = Sum13_CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["CRHAP"].ToString()));
                    Sum13_DRHAP = Sum13_DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["DRHAP"].ToString()));
                }

                // 15000000	사내이자 손익 (2014.07.25 추가)
                if (table.Rows[i]["APACDAC"].ToString() == "15000000")
                {
                    Sum15_T0CRAHP = 0;
                    Sum15_T0DRHAP = 0;
                    Sum15_S0CRHAP = 0;
                    Sum15_S0DRHAP = 0;
                    Sum15_B8CRHAP = 0;
                    Sum15_B8DRHAP = 0;
                    Sum15_CRHAP = Sum15_CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["CRHAP"].ToString()));
                    Sum15_DRHAP = Sum15_DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["DRHAP"].ToString()));
                }

                // 16000000	결산1 손익 (2014.12.31 추가)
                if (table.Rows[i]["APACDAC"].ToString() == "16000000")
                {
                    Sum16_T0CRAHP = 0;
                    Sum16_T0DRHAP = 0;
                    Sum16_S0CRHAP = 0;
                    Sum16_S0DRHAP = 0;
                    Sum16_B8CRHAP = 0;
                    Sum16_B8DRHAP = 0;
                    Sum16_CRHAP = Sum16_CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["CRHAP"].ToString()));
                    Sum16_DRHAP = Sum16_DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["DRHAP"].ToString()));
                }

                // 17000000	결산1 손익 (2018.12.31 추가)
                if (table.Rows[i]["APACDAC"].ToString() == "17000000")
                {
                    Sum16_T0CRAHP = 0;
                    Sum16_T0DRHAP = 0;
                    Sum16_S0CRHAP = 0;
                    Sum16_S0DRHAP = 0;
                    Sum16_B8CRHAP = 0;
                    Sum16_B8DRHAP = 0;
                    Sum16_CRHAP = Sum16_CRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["CRHAP"].ToString()));
                    Sum16_DRHAP = Sum16_DRHAP + Convert.ToDouble(Get_Numeric(table.Rows[i]["DRHAP"].ToString()));
                }
            }

            // 영업외수익 (08000000)
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i]["APACDAC"].ToString() == "08000000")
                {
                    table.Rows[i]["T0CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum08_T0CRAHP));
                    table.Rows[i]["T0DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum08_T0DRHAP));
                    table.Rows[i]["S0CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum08_S0CRHAP));
                    table.Rows[i]["S0DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum08_S0DRHAP));
                    table.Rows[i]["B8CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum08_B8CRHAP));
                    table.Rows[i]["B8DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum08_B8DRHAP));
                    table.Rows[i]["CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum08_CRHAP));
                    table.Rows[i]["DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum08_DRHAP));
                }
            }

            // 영업외비용 (09000000)
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i]["APACDAC"].ToString() == "09000000")
                {
                    table.Rows[i]["T0CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum09_T0CRAHP));
                    table.Rows[i]["T0DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum09_T0DRHAP));
                    table.Rows[i]["S0CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum09_S0CRHAP));
                    table.Rows[i]["S0DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum09_S0DRHAP));
                    table.Rows[i]["B8CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum09_B8CRHAP));
                    table.Rows[i]["B8DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum09_B8DRHAP));
                    table.Rows[i]["CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum09_CRHAP));
                    table.Rows[i]["DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum09_DRHAP));
                }
            }

            // 프로젝트이자 (12000000) , 사내이자 손익  (15000000) , 결산손익  (16000000) , 결산손익  (17000000) -- 사업부별 화면정리(0) (합계 부분에서만 조회됨)
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i]["APACDAC"].ToString() == "12000000" || table.Rows[i]["APACDAC"].ToString() == "15000000" || table.Rows[i]["APACDAC"].ToString() == "16000000" || table.Rows[i]["APACDAC"].ToString() == "17000000")
                {
                    table.Rows[i]["T0CRHAP"] = "";
                    table.Rows[i]["T0DRHAP"] = "";
                    table.Rows[i]["S0CRHAP"] = "";
                    table.Rows[i]["S0DRHAP"] = "";
                    table.Rows[i]["B8CRHAP"] = "";
                    table.Rows[i]["B8DRHAP"] = "";
                }
            }

            // 10000000  경상이익
            // 경상이익 =  영업이익2 +  영업외수익 -  영업외비용
            // 10000000 =  07000000  +  08000000   -  09000000
            Sum10_T0CRAHP = Sum07_T0CRAHP + Sum08_T0CRAHP - Sum09_T0CRAHP;
            Sum10_T0DRHAP = Sum07_T0DRHAP + Sum08_T0DRHAP - Sum09_T0DRHAP;
            Sum10_S0CRHAP = Sum07_S0CRHAP + Sum08_S0CRHAP - Sum09_S0CRHAP;
            Sum10_S0DRHAP = Sum07_S0DRHAP + Sum08_S0DRHAP - Sum09_S0DRHAP;
            Sum10_B8CRHAP = Sum07_B8CRHAP + Sum08_B8CRHAP - Sum09_B8CRHAP;
            Sum10_B8DRHAP = Sum07_B8DRHAP + Sum08_B8DRHAP - Sum09_B8DRHAP;
            Sum10_CRHAP = Sum07_CRHAP + Sum08_CRHAP - Sum09_CRHAP;
            Sum10_DRHAP = Sum07_DRHAP + Sum08_DRHAP - Sum09_DRHAP;


            // 영업이익2 달성률 재계산(-)인 경우 표현 안함
            // 영업이익 --> 07000001
            if (Convert.ToDouble(Sum07_T0CRAHP) > 0 && Sum07_T0DRHAP > 0)
            {
                Div07_T0DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum07_T0DRHAP / Sum07_T0CRAHP), 3) * 1000))) / 10;
            }
            else
            {
                Div07_T0DRHAP = 0;
            }
            if (Convert.ToDouble(Sum07_S0CRHAP) > 0 && Sum07_S0DRHAP > 0)
            {
                Div07_S0DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum07_S0DRHAP / Sum07_S0CRHAP), 3) * 1000))) / 10;
            }
            else
            {
                Div07_S0DRHAP = 0;
            }

            if (Convert.ToDouble(Sum07_B8CRHAP) > 0 && Sum07_B8DRHAP > 0)
            {
                Div07_B8DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum07_B8DRHAP / Sum07_B8CRHAP), 3) * 1000))) / 10;
            }
            else
            {
                Div07_B8DRHAP = 0;
            }
            if (Convert.ToDouble(Sum07_CRHAP) > 0 && Sum07_DRHAP > 0)
            {
                Div07_DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum07_DRHAP / Sum07_CRHAP), 3) * 1000))) / 10;
            }
            else
            {
                Div07_DRHAP = 0;
            }
            // 영업이익율2 (07000001)
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i]["APACDAC"].ToString() == "07000001")
                {
                    table.Rows[i]["T0DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div07_T0DRHAP));
                    table.Rows[i]["S0DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div07_S0DRHAP));
                    table.Rows[i]["B8DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div07_B8DRHAP));
                    table.Rows[i]["DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div07_DRHAP));
                }
            } // End .. For 경상이익 (10000000)
            // -------------------------------------   END ----------------------------------------------------
            // ------------------------------------------------------------------------------------------------


            if (Convert.ToDouble(Sum10_T0CRAHP) > 0 && Sum10_T0DRHAP > 0)
            {
                Div10_T0DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum10_T0DRHAP / Sum10_T0CRAHP), 3) * 1000))) / 10;
            }
            else
            {
                Div10_T0DRHAP = 0;
            }
            if (Convert.ToDouble(Sum10_S0CRHAP) > 0 && Sum10_S0DRHAP > 0)
            {
                Div10_S0DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum10_S0DRHAP / Sum10_S0CRHAP), 3) * 1000))) / 10;
            }
            else
            {
                Div10_S0DRHAP = 0;
            }
            if (Convert.ToDouble(Sum10_B8CRHAP) > 0 && Sum10_B8DRHAP > 0)
            {
                Div10_B8DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum10_B8DRHAP / Sum10_B8CRHAP), 3) * 1000))) / 10;
            }
            else
            {
                Div10_B8DRHAP = 0;
            }
            if (Convert.ToDouble(Sum10_CRHAP) > 0 && Sum10_DRHAP > 0)
            {
                Div10_DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum10_DRHAP / Sum10_CRHAP), 3) * 1000))) / 10;
            }
            else
            {
                Div10_DRHAP = 0;
            }

            // 경상이익 (10000000)
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i]["APACDAC"].ToString() == "10000000")
                {
                    table.Rows[i]["T0CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum10_T0CRAHP));
                    table.Rows[i]["T0DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum10_T0DRHAP));
                    table.Rows[i]["S0CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum10_S0CRHAP));
                    table.Rows[i]["S0DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum10_S0DRHAP));
                    table.Rows[i]["B8CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum10_B8CRHAP));
                    table.Rows[i]["B8DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum10_B8DRHAP));
                    table.Rows[i]["CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum10_CRHAP));
                    table.Rows[i]["DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum10_DRHAP));
                }

                if (table.Rows[i]["APACDAC"].ToString() == "10000001")
                {

                    table.Rows[i]["T0DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div10_T0DRHAP));
                    table.Rows[i]["S0DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div10_S0DRHAP));
                    table.Rows[i]["B8DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div10_B8DRHAP));
                    table.Rows[i]["DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div10_DRHAP));
                }
            } // End .. For 경상이익 (10000000)



            // 20000000	세전순이익
            // 세전순이익 = 경상이익 + 외환손익(재무) -  투자이자(P.J) +  지분법손익 - 사내이자 손익 + 결산손익(배당) (2014.12.31 추가)
            // 20000000   = 10000000 +   11000000     -   12000000     +  13000000   - 15000000  +  16000000 
            Sum20_T0CRAHP = ((((Sum10_T0CRAHP + Sum11_T0CRAHP) - Sum12_T0CRAHP) + Sum13_T0CRAHP) - Sum15_T0CRAHP) + Sum16_T0CRAHP;
            Sum20_T0DRHAP = ((((Sum10_T0DRHAP + Sum11_T0DRHAP) - Sum12_T0DRHAP) + Sum13_T0DRHAP) - Sum15_T0DRHAP) + Sum16_T0DRHAP;
            Sum20_S0CRHAP = ((((Sum10_S0CRHAP + Sum11_S0CRHAP) - Sum12_S0CRHAP) + Sum13_S0CRHAP) - Sum15_S0CRHAP) + Sum16_S0CRHAP;
            Sum20_S0DRHAP = ((((Sum10_S0DRHAP + Sum11_S0DRHAP) - Sum12_S0DRHAP) + Sum13_S0DRHAP) - Sum15_S0DRHAP) + Sum16_S0DRHAP;
            Sum20_B8CRHAP = ((((Sum10_B8CRHAP + Sum11_B8CRHAP) - Sum12_B8CRHAP) + Sum13_B8CRHAP) - Sum15_B8CRHAP) + Sum16_B8CRHAP;
            Sum20_B8DRHAP = ((((Sum10_B8DRHAP + Sum11_B8DRHAP) - Sum12_B8DRHAP) + Sum13_B8DRHAP) - Sum15_B8DRHAP) + Sum16_B8DRHAP;

            Sum20_CRHAP = ((((Sum10_CRHAP + Sum11_CRHAP) - Sum12_CRHAP) + Sum13_CRHAP) - Sum15_CRHAP) + Sum16_CRHAP;
            Sum20_DRHAP = ((((Sum10_DRHAP + Sum11_DRHAP) - Sum12_DRHAP) + Sum13_DRHAP) - Sum15_DRHAP) + Sum16_DRHAP;


            if (Convert.ToDouble(Sum20_T0CRAHP) > 0 && Sum20_T0DRHAP > 0)
            {
                Div20_T0DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum20_T0DRHAP / Sum20_T0CRAHP), 3) * 1000))) / 10;
            }
            else
            {
                Div20_T0DRHAP = 0;
            }
            if (Convert.ToDouble(Sum20_S0CRHAP) > 0 && Sum20_S0DRHAP > 0)
            {
                Div20_S0DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum20_S0DRHAP / Sum20_S0CRHAP), 3) * 1000))) / 10;
            }
            else
            {
                Div20_S0DRHAP = 0;
            }
            if (Convert.ToDouble(Sum20_B8CRHAP) > 0 && Sum20_B8DRHAP > 0)
            {
                Div20_B8DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum20_B8DRHAP / Sum20_B8CRHAP), 3) * 1000))) / 10;
            }
            else
            {
                Div20_B8DRHAP = 0;
            }
            if (Convert.ToDouble(Sum20_CRHAP) > 0 && Sum20_DRHAP > 0)
            {
                Div20_DRHAP = Convert.ToDouble(UP_DotDelete(Convert.ToString(Math.Round((Sum20_DRHAP / Sum20_CRHAP), 3) * 1000))) / 10;
            }
            else
            {
                Div20_DRHAP = 0;
            }


            // 세전순이익 (20000000)
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i]["APACDAC"].ToString() == "20000000")
                {
                    table.Rows[i]["T0CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum20_T0CRAHP));
                    table.Rows[i]["T0DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum20_T0DRHAP));
                    table.Rows[i]["S0CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum20_S0CRHAP));
                    table.Rows[i]["S0DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum20_S0DRHAP));
                    table.Rows[i]["B8CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum20_B8CRHAP));
                    table.Rows[i]["B8DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum20_B8DRHAP));
                    table.Rows[i]["CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum20_CRHAP));
                    table.Rows[i]["DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Sum20_DRHAP));
                }

                if (table.Rows[i]["APACDAC"].ToString() == "20000001")
                {
                    table.Rows[i]["T0DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div20_T0DRHAP));
                    table.Rows[i]["S0DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div20_S0DRHAP));
                    table.Rows[i]["B8DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div20_B8DRHAP));
                    table.Rows[i]["DRHAP"] = Convert.ToString(string.Format("{0:#,###.#}", Div20_DRHAP));
                }

            } // End..For 세전순이익 (20000000)

            // 결산손익 - 처리하여 보이기
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i]["APACDAC"].ToString() == "15000000")
                {
                    table.Rows[i]["CRHAP"] = Convert.ToString(string.Format("{0:#,###}", Convert.ToDouble(Get_Numeric(table.Rows[i]["CRHAP"].ToString())) * -1));
                    table.Rows[i]["DRHAP"] = Convert.ToString(string.Format("{0:#,###}", Convert.ToDouble(Get_Numeric(table.Rows[i]["DRHAP"].ToString())) * -1));
                }

                if (table.Rows[i]["APACDAC"].ToString() == "12000000")
                {
                    if (Convert.ToDouble(Get_Numeric(table.Rows[i]["DRHAP"].ToString())) > 0)
                    {
                        table.Rows[i]["DRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(Get_Numeric(table.Rows[i]["DRHAP"].ToString())) * -1));
                    }
                    if (Convert.ToDouble(Get_Numeric(table.Rows[i]["CRHAP"].ToString())) > 0)
                    {
                        table.Rows[i]["CRHAP"] = Convert.ToString(string.Format("{0:#,###}", double.Parse(Get_Numeric(table.Rows[i]["CRHAP"].ToString())) * -1));
                    }
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