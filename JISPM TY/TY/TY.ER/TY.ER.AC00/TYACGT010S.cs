using System;
using System.Data;
using System.Drawing;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

using System.Data.OleDb;

namespace TY.ER.AC00
{
    /// <summary>
    /// e-세로 세금계산서 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2013.01.10 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_31A4W606 : 부가세 - 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_31A4X609 : 부가세 - 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  SAV : 저장
    ///  EV1AREA : 지역구분
    ///  EV1GUBN : 매입，매출구분
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    /// </summary>
    public partial class TYACGT010S : TYBase
    {
        #region Description : 페이지 로드
        public TYACGT010S()
        {
            InitializeComponent();
        }

        private void TYACGT010S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_EXCEL.ProcessCheck += new TButton.CheckHandler(BTN61_EXCEL_ProcessCheck);

            this.FPS91_TY_S_AC_31A4X609.Visible = true;
            this.FPS91_TY_S_AC_31B9W612.Visible = false;

            UP_Spread_Title();

            SetStartingFocus(this.DTP01_GSTDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sEV1GUBN = string.Empty;

            if (this.CBO01_EV1GUBN.GetValue().ToString() == "1" && this.CBO01_INQOPTION.GetValue().ToString() == "1")
            {
                sEV1GUBN = this.CBO01_EV1GUBN.GetValue().ToString();  //매입전자계산서
            }
            else if (this.CBO01_EV1GUBN.GetValue().ToString() == "1" && this.CBO01_INQOPTION.GetValue().ToString() == "2")
            {
                sEV1GUBN = "3"; //매입계산서
            }
            else
            {
                sEV1GUBN = this.CBO01_EV1GUBN.GetValue().ToString();
            }

            this.FPS91_TY_S_AC_31A4X609.Visible = true;
            this.FPS91_TY_S_AC_31B9W612.Visible = false;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_31A4W606",
                this.DTP01_GSTDATE.GetValue().ToString(),
                this.DTP01_GEDDATE.GetValue().ToString(),
                this.CBO01_EV1AREA.GetValue().ToString(),
                //this.CBO01_EV1GUBN.GetValue().ToString()
                sEV1GUBN
                );


            this.FPS91_TY_S_AC_31A4X609.SetValue(this.DbConnector.ExecuteDataTable());

            this.SpreadSumRowAdd(this.FPS91_TY_S_AC_31A4X609, "EV1SAUP", "합  계", SumRowType.Sum, "EV1CNT", "EV1AMT", "EV1VAT", "EV1TOT", "EV2CNT", "EV2AMT", "EV2VAT", "EV2TOT");

            for (int i = 0; i < this.FPS91_TY_S_AC_31A4X609.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_31A4X609.GetValue(i, "EV1CNT").ToString() != this.FPS91_TY_S_AC_31A4X609.GetValue(i, "EV2CNT").ToString())
                {
                    this.FPS91_TY_S_AC_31A4X609.ActiveSheet.Rows[i].BackColor = Color.FromName("#ffff66");

                    this.FPS91_TY_S_AC_31A4X609_Sheet1.Cells[i, 6].ForeColor  = Color.Red;
                    this.FPS91_TY_S_AC_31A4X609_Sheet1.Cells[i, 10].ForeColor = Color.Red;
                }

                if (this.FPS91_TY_S_AC_31A4X609.GetValue(i, "EV1AMT").ToString() != this.FPS91_TY_S_AC_31A4X609.GetValue(i, "EV2AMT").ToString())
                {
                    this.FPS91_TY_S_AC_31A4X609.ActiveSheet.Rows[i].BackColor = Color.FromName("#ffff66");

                    this.FPS91_TY_S_AC_31A4X609_Sheet1.Cells[i, 7].ForeColor  = Color.Red;
                    this.FPS91_TY_S_AC_31A4X609_Sheet1.Cells[i, 11].ForeColor = Color.Red;
                }

                if (this.FPS91_TY_S_AC_31A4X609.GetValue(i, "EV1VAT").ToString() != this.FPS91_TY_S_AC_31A4X609.GetValue(i, "EV2VAT").ToString())
                {
                    this.FPS91_TY_S_AC_31A4X609.ActiveSheet.Rows[i].BackColor = Color.FromName("#ffff66");

                    this.FPS91_TY_S_AC_31A4X609_Sheet1.Cells[i, 8].ForeColor  = Color.Red;
                    this.FPS91_TY_S_AC_31A4X609_Sheet1.Cells[i, 12].ForeColor = Color.Red;
                }

                if (this.FPS91_TY_S_AC_31A4X609.GetValue(i, "EV1TOT").ToString() != this.FPS91_TY_S_AC_31A4X609.GetValue(i, "EV2TOT").ToString())
                {
                    this.FPS91_TY_S_AC_31A4X609.ActiveSheet.Rows[i].BackColor = Color.FromName("#ffff66");

                    this.FPS91_TY_S_AC_31A4X609_Sheet1.Cells[i, 9].ForeColor  = Color.Red;
                    this.FPS91_TY_S_AC_31A4X609_Sheet1.Cells[i, 13].ForeColor = Color.Red;
                }

                if (this.FPS91_TY_S_AC_31A4X609.GetValue(i, "EV1SAUP").ToString() == "합  계")
                {
                    // 특정 칼럼 색깔 입히기
                    this.FPS91_TY_S_AC_31A4X609.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    this.FPS91_TY_S_AC_31A4X609.ActiveSheet.Rows[i].ForeColor = Color.Red;
                }
            }
        }
        #endregion

        //#region Description : 저장 버튼
        //private void BTN61_SAV_Click(object sender, EventArgs e)
        //{
        //    string sOUTMSG = string.Empty;

        //    this.DbConnector.CommandClear();
        //    this.DbConnector.Attach
        //        (
        //        "TY_P_AC_31BAT618",
        //        this.DTP01_GSTDATE.GetValue().ToString(),
        //        this.DTP01_GEDDATE.GetValue().ToString(),
        //        this.CBO01_EV1AREA.GetValue().ToString(),
        //        this.CBO01_EV1GUBN.GetValue().ToString(),
        //        "P",
        //        Employer.EmpNo,
        //        sOUTMSG.ToString()
        //        );

        //    sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

        //    if (sOUTMSG.Substring(0, 2) != "OK")
        //    {
        //        return;
        //    }

        //    this.BTN61_INQ_Click(null, null);
        //    this.ShowMessage("TY_M_GB_23NAD873");
        //}
        //#endregion

        #region Description : 찾아보기
        private void BTN61_SEARCH_Click(object sender, EventArgs e)
        {
            OpenFile.Filter = "Excel 97-2003통합 문서(*.xls)|*.xls|Excel 통합 문서 (.xlsx)|*.xlsx|All Files (*.*)|*.*";

            if (this.OpenFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.txtFile.Text = this.OpenFile.FileName;
        }
        #endregion

        #region Description : 엑셀 업데이트
        private void BTN61_EXCEL_Click(object sender, EventArgs e)
        {
            string sEV1GUBN = string.Empty;

            if (this.CBO01_EV1GUBN.GetValue().ToString() == "1" && this.CBO01_INQOPTION.GetValue().ToString() == "1")
            {
                sEV1GUBN = this.CBO01_EV1GUBN.GetValue().ToString();  //매입전자계산서
            }
            else if (this.CBO01_EV1GUBN.GetValue().ToString() == "1" && this.CBO01_INQOPTION.GetValue().ToString() == "2")
            {
                sEV1GUBN = "3"; //매입계산서
            }
            else
            {
                sEV1GUBN = this.CBO01_EV1GUBN.GetValue().ToString();
            }

            if (this.txtFile.Text.Trim() != "")
            {
                string strQuery = string.Empty;

                this.FPS91_TY_S_AC_31A4X609.Visible = false;
                this.FPS91_TY_S_AC_31B9W612.Visible = true;

                // TEMP 삭제
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_31A62610");

                this.DbConnector.ExecuteNonQueryList();

                string strProvider = string.Empty;
                strProvider = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + this.txtFile.Text.Trim() + "; Extended Properties=Excel 12.0";

                if (this.CBO01_EV1GUBN.GetValue().ToString().Trim() == "1")
                {
                    if (sEV1GUBN != "1")
                    {
                        //strQuery = "SELECT * FROM [매입 합계표 명세서$A09:H10000]"; //  , Sheet1$
                        strQuery = "SELECT * FROM [계산서$A09:H10000]"; //  , Sheet1$
                    }
                    else
                    {
                        strQuery = "SELECT * FROM [세금계산서$A09:H10000]"; //  , Sheet1$
                    }
                }
                else
                {
                    //strQuery = "SELECT * FROM [매출 합계표 명세서$A09:H10000]"; //  , Sheet1$
                    strQuery = "SELECT * FROM [세금계산서$A09:H10000]"; //  , Sheet1$
                }

                OleDbConnection ExcelCon = new OleDbConnection(strProvider);
                ExcelCon.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(strQuery, strProvider);

                DataSet ds = new DataSet();
                adapter.Fill(ds, "EXCEL");

                // 데이터테이블로 가져옴
                DataSet ExcelDs = new DataSet();

                ExcelDs = Convert_DataSet(ds);

                this.FPS91_TY_S_AC_31B9W612.SetValue(ExcelDs);


                // 20181205 원본 소스 시작
                for (int i = 0; i < this.FPS91_TY_S_AC_31B9W612.ActiveSheet.RowCount; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_31BAM615",
                                            this.FPS91_TY_S_AC_31B9W612.GetValue(i, "TM1SYMD").ToString(),
                                            this.FPS91_TY_S_AC_31B9W612.GetValue(i, "TM1EYMD").ToString(),
                                            this.FPS91_TY_S_AC_31B9W612.GetValue(i, "TM1AREA").ToString(),
                                            this.FPS91_TY_S_AC_31B9W612.GetValue(i, "TM1GUBN").ToString(),
                                            this.FPS91_TY_S_AC_31B9W612.GetValue(i, "TM1SAUP").ToString(),
                                            this.FPS91_TY_S_AC_31B9W612.GetValue(i, "TM1CNT").ToString(),
                                            this.FPS91_TY_S_AC_31B9W612.GetValue(i, "TM1AMT").ToString(),
                                            this.FPS91_TY_S_AC_31B9W612.GetValue(i, "TM1VAT").ToString(),
                                            this.FPS91_TY_S_AC_31B9W612.GetValue(i, "TM1TOT").ToString()
                                            );
                    this.DbConnector.ExecuteNonQueryList();
                }
                // 20181205 원본 소스 종료
                

                string sOUTMSG = string.Empty;

                if (this.CBO01_INQOPTION2.GetValue().ToString() == "N")
                {
                    //매입,매출 부가세 신고자료 생성
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_5C8BH279",
                        this.DTP01_GSTDATE.GetValue().ToString(),
                        this.DTP01_GEDDATE.GetValue().ToString(),
                        this.CBO01_EV1AREA.GetValue().ToString(),
                        //this.CBO01_EV1GUBN.GetValue().ToString(),
                        sEV1GUBN,
                        "P",
                        Employer.EmpNo,
                        sOUTMSG.ToString()
                        );
                }
                else
                {
                    //매입,매출 부가세 신고자료 생성
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_31BAT618",
                        this.DTP01_GSTDATE.GetValue().ToString(),
                        this.DTP01_GEDDATE.GetValue().ToString(),
                        this.CBO01_EV1AREA.GetValue().ToString(),
                        //this.CBO01_EV1GUBN.GetValue().ToString(),
                        sEV1GUBN,
                        "P",
                        Employer.EmpNo,
                        sOUTMSG.ToString()
                        );
                }

                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                if (sOUTMSG.Substring(0, 2) != "OK")
                {
                    return;
                }

                this.ShowMessage("TY_M_AC_31BAP617");
            }
            else
            {
                this.ShowMessage("TY_M_AC_31B1C623");
            }

            //this.FPS91_TY_S_AC_31A4X609.Visible = true;
            //this.FPS91_TY_S_AC_31B9W612.Visible = false;
        }
        #endregion

        #region Description : 데이터셋 변경
        private DataSet Convert_DataSet(DataSet ds)
        {
            string sSYYMMDD = this.DTP01_GSTDATE.GetValue().ToString();
            string sEYYMMDD = this.DTP01_GEDDATE.GetValue().ToString();
            string sAREA    = this.CBO01_EV1AREA.GetValue().ToString();
            string sMBGU    = this.CBO01_EV1GUBN.GetValue().ToString();

            string sEV1GUBN = string.Empty;

            if (this.CBO01_EV1GUBN.GetValue().ToString() == "1" && this.CBO01_INQOPTION.GetValue().ToString() == "1")
            {
                sMBGU = this.CBO01_EV1GUBN.GetValue().ToString();  //매입전자계산서
            }
            else if (this.CBO01_EV1GUBN.GetValue().ToString() == "1" && this.CBO01_INQOPTION.GetValue().ToString() == "2")
            {
                sMBGU = "3"; //매입계산서
            }
            else
            {
                sMBGU = this.CBO01_EV1GUBN.GetValue().ToString();
            }


            DataSet RetDs = new DataSet();

            // 마스터테이블 생성
            DataTable ExcelTable = new DataTable();

            // 빈 ROW 생성
            DataRow ExcelRow;

            string sTM1SYMD = string.Empty;
            string sTM1EYMD = string.Empty;
            string sTM1AREA = string.Empty;
            string sTM1GUBN = string.Empty;
            string sTM1SAUP = string.Empty;
            string sTM1CNT = string.Empty;
            string sTM1AMT = string.Empty;
            string sTM1VAT = string.Empty;
            string sTM1TOT = string.Empty;

            ExcelTable.Columns.Add("TM1SYMD", typeof(System.String));
            ExcelTable.Columns.Add("TM1EYMD", typeof(System.String));
            ExcelTable.Columns.Add("TM1AREA", typeof(System.String));
            ExcelTable.Columns.Add("TM1GUBN", typeof(System.String));
            ExcelTable.Columns.Add("TM1SAUP", typeof(System.String));
            ExcelTable.Columns.Add("TM1CNT", typeof(System.Double));
            ExcelTable.Columns.Add("TM1AMT", typeof(System.Double));
            ExcelTable.Columns.Add("TM1VAT", typeof(System.Double));
            ExcelTable.Columns.Add("TM1TOT", typeof(System.Double));

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (Get_Numeric(SetDefaultValue(ds.Tables[0].Rows[i][0].ToString().Trim())) != "0")
                {
                    ExcelRow = ExcelTable.NewRow();

                    sTM1SYMD = sSYYMMDD;
                    sTM1EYMD = sEYYMMDD;
                    sTM1AREA = sAREA;
                    sTM1GUBN = sMBGU;

                    sTM1SAUP = SetDefaultValue(ds.Tables[0].Rows[i][1].ToString().Trim().Replace("-", ""));
                    sTM1CNT = Get_Numeric(SetDefaultValue(ds.Tables[0].Rows[i][3].ToString().Trim()));
                    sTM1AMT = Get_Numeric(SetDefaultValue(ds.Tables[0].Rows[i][4].ToString().Trim()));
                    sTM1VAT = Get_Numeric(SetDefaultValue(ds.Tables[0].Rows[i][5].ToString().Trim()));
                    sTM1TOT = Get_Numeric(SetDefaultValue(ds.Tables[0].Rows[i][6].ToString().Trim()));

                    ExcelRow["TM1SYMD"] = sTM1SYMD;
                    ExcelRow["TM1EYMD"] = sTM1EYMD;
                    ExcelRow["TM1AREA"] = sTM1AREA;
                    ExcelRow["TM1GUBN"] = sTM1GUBN;
                    ExcelRow["TM1SAUP"] = sTM1SAUP;
                    ExcelRow["TM1CNT"] = double.Parse(sTM1CNT);
                    ExcelRow["TM1AMT"] = double.Parse(sTM1AMT);
                    if (sMBGU == "3")
                    {
                        ExcelRow["TM1VAT"] = double.Parse("0");
                        ExcelRow["TM1TOT"] = double.Parse(sTM1AMT);
                    }
                    else
                    {
                        ExcelRow["TM1VAT"] = double.Parse(sTM1VAT);
                        ExcelRow["TM1TOT"] = double.Parse(sTM1TOT);
                    }

                    ExcelTable.Rows.Add(ExcelRow);
                }
            }

            ExcelTable.TableName = "EXCEL1";

            RetDs.Tables.Add(ExcelTable);

            return RetDs;
        }
        #endregion


        #region Description : 엑셀 ProcessCheck 이벤트
        private void BTN61_EXCEL_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sProcedid = string.Empty;
            string sJpno = string.Empty;
            string sMessage = string.Empty;
            string sCDAC = string.Empty;

            if (this.CBO01_EV1GUBN.GetValue().ToString().Trim() == "1")
            {
                sProcedid = "TY_P_AC_532GG368"; // (매입)

                if (this.CBO01_EV1AREA.GetValue() == "1")
                {
                    sCDAC = "11103101";
                }
                else{
                    sCDAC = "11103102";
                }
            }
            else
            {
                sProcedid = "TY_P_AC_532GH369"; // (매출)

                if (this.CBO01_EV1AREA.GetValue() == "1")
                {
                    sCDAC = "21103101";
                }
                else
                {
                    sCDAC = "21103102";
                }
            };

            this.DbConnector.CommandClear();
            this.DbConnector.Attach(sProcedid, this.DTP01_GSTDATE.GetValue().ToString(), this.DTP01_GEDDATE.GetValue().ToString(), sCDAC);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                sJpno = dt.Rows[0]["WCJPNO"].ToString();
                sMessage = sJpno + " 전표의 거래처 코드를 등록 하세요";

                this.ShowCustomMessage(sMessage, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.CBO01_EV1GUBN);
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 스프레드 타이틀 변경
        private void UP_Spread_Title()
        {
            this.FPS91_TY_S_AC_31A4X609_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_31A4X609_Sheet1.RowHeaderColumnCount = 1;

            //(현재ROW, 현재COLUMN, 묶을ROW수, 묶을 COLUMN수)
            this.FPS91_TY_S_AC_31A4X609_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1); // 사업자번호
            this.FPS91_TY_S_AC_31A4X609_Sheet1.AddColumnHeaderSpanCell(0, 5, 2, 1); // 거래처명
            this.FPS91_TY_S_AC_31A4X609_Sheet1.AddColumnHeaderSpanCell(0, 6, 1, 4); // e-세로
            this.FPS91_TY_S_AC_31A4X609_Sheet1.AddColumnHeaderSpanCell(0, 10, 1, 4); // 당사

            this.FPS91_TY_S_AC_31A4X609_Sheet1.ColumnHeader.Cells[0, 4].Value = "사업자번호";
            this.FPS91_TY_S_AC_31A4X609_Sheet1.ColumnHeader.Cells[0, 5].Value = "거래처명";
            this.FPS91_TY_S_AC_31A4X609_Sheet1.ColumnHeader.Cells[0, 6].Value = "e-세로";
            this.FPS91_TY_S_AC_31A4X609_Sheet1.ColumnHeader.Cells[0, 10].Value = "당사";

            this.FPS91_TY_S_AC_31A4X609_Sheet1.ColumnHeader.Cells[1, 4].Value = "";
            this.FPS91_TY_S_AC_31A4X609_Sheet1.ColumnHeader.Cells[1, 5].Value = "";
            this.FPS91_TY_S_AC_31A4X609_Sheet1.ColumnHeader.Cells[1, 6].Value = "건수";
            this.FPS91_TY_S_AC_31A4X609_Sheet1.ColumnHeader.Cells[1, 7].Value = "공급가액";
            this.FPS91_TY_S_AC_31A4X609_Sheet1.ColumnHeader.Cells[1, 8].Value = "부가세액";
            this.FPS91_TY_S_AC_31A4X609_Sheet1.ColumnHeader.Cells[1, 9].Value = "합    계";
            this.FPS91_TY_S_AC_31A4X609_Sheet1.ColumnHeader.Cells[1, 10].Value = "건수";
            this.FPS91_TY_S_AC_31A4X609_Sheet1.ColumnHeader.Cells[1, 11].Value = "공급가액";
            this.FPS91_TY_S_AC_31A4X609_Sheet1.ColumnHeader.Cells[1, 12].Value = "부가세액";
            this.FPS91_TY_S_AC_31A4X609_Sheet1.ColumnHeader.Cells[1, 13].Value = "합    계";

            if (this.FPS91_TY_S_AC_31A4X609_Sheet1.AlternatingRows.Count > 0)
                this.FPS91_TY_S_AC_31A4X609_Sheet1.AlternatingRows[0].BackColor = Color.White;


            this.FPS91_TY_S_AC_31A4X609_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_31A4X609_Sheet1.ColumnHeader.Cells[0, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion

    }
}