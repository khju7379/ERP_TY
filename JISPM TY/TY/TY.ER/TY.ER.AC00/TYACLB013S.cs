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
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using FarPoint.Win.Spread.CellType;

namespace TY.ER.AC00
{
    /// <summary>
    /// 분기별예산운영집계표 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.08.30 11:30
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_28UBP656 : 분기별예산운영집계표
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_28UBV661 : 분기별예산운영집계표
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  GCDDP : 사업장코드
    ///  GBUNGI : 분기
    ///  GYESAN : 예산구분
    ///  GDATE : 일자
    /// </summary>
    public partial class TYACLB013S : TYBase
    {
        #region Description : 페이지 로드
        public TYACLB013S()
        {
            InitializeComponent();
        }

        private void TYACLB013S_Load(object sender, System.EventArgs e)
        {
            UP_Spread_Load();

            this.CBH01_GCDDP.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            SetStartingFocus(this.TXT01_GDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sSTDATE = string.Empty;
            string sEDDATE = string.Empty;

            sSTDATE = this.TXT01_GDATE.GetValue().ToString() + "01";

            // 1분기
            if (this.CBO01_GBUNGI.GetValue().ToString() == "1")
            {
                //sSTDATE = this.TXT01_GDATE.GetValue().ToString() + "01";
                sEDDATE = this.TXT01_GDATE.GetValue().ToString() + "03";
            }
            else if (this.CBO01_GBUNGI.GetValue().ToString() == "2") // 2분기
            {
                //sSTDATE = this.TXT01_GDATE.GetValue().ToString() + "04";
                sEDDATE = this.TXT01_GDATE.GetValue().ToString() + "06";
            }
            else if (this.CBO01_GBUNGI.GetValue().ToString() == "3") // 3분기
            {
                //sSTDATE = this.TXT01_GDATE.GetValue().ToString() + "07";
                sEDDATE = this.TXT01_GDATE.GetValue().ToString() + "09";
            }
            else if (this.CBO01_GBUNGI.GetValue().ToString() == "4") // 4분기
            {
                //sSTDATE = this.TXT01_GDATE.GetValue().ToString() + "10";
                sEDDATE = this.TXT01_GDATE.GetValue().ToString() + "12";
            }

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_28UBP656",
                sSTDATE.ToString(),
                sEDDATE.ToString(),
                this.CBO01_GYESAN.GetText(),
                this.CBO01_GYESAN.GetValue(),
                this.CBO01_GYESAN.GetValue(),
                this.CBO01_GYESAN.GetValue(),
                this.CBO01_GYESAN.GetValue(),
                this.CBO01_GYESAN.GetValue(),
                this.CBO01_GYESAN.GetValue(),
                this.CBO01_GYESAN.GetValue(),
                this.CBO01_GYESAN.GetValue(),
                sSTDATE.ToString(),
                sEDDATE.ToString(),
                this.CBH01_GCDDP.GetValue().ToString()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            DataTable dz = new DataTable();

            dz.Columns.Add("GUBUN1");
            dz.Columns.Add("GUBUN2");
            dz.Columns.Add("GUBUN3");
            dz.Columns.Add("GUBUN4");
            dz.Columns.Add("GUBUN5");
            dz.Columns.Add("GUBUN6");
            dz.Columns.Add("GUBUN7");
            dz.Columns.Add("GUBUN8");
            dz.Columns.Add("GUBUN9");
            dz.Columns.Add("GUBUN10");
            dz.Columns.Add("GUBUN11");
            dz.Columns.Add("GUBUN12");
            dz.Columns.Add("GUBUN13");
            dz.Columns.Add("GUBUN14");
            dz.Columns.Add("GUBUN15");
            dz.Columns.Add("GUBUN16");

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_28UBV661.SetValue(UP_ConvertDt(dt));

                // 특정 ROW 색깔 입히기
                for (int i = 0; i < this.FPS91_TY_S_AC_28UBV661.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_28UBV661.GetValue(i, "NMAC").ToString() == "총   계")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_28UBV661.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
                    }
                }
            }
            else
            {
                this.FPS91_TY_S_AC_28UBV661.SetValue(dt);

                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }

            UP_Spread_Load();
        }
        #endregion

        #region Description : 스프레드 로드
        private void UP_Spread_Load()
        {
            this.FPS91_TY_S_AC_28UBV661_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_28UBV661_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_28UBV661_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_AC_28UBV661_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_AC_28UBV661_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);
            this.FPS91_TY_S_AC_28UBV661_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);
            this.FPS91_TY_S_AC_28UBV661_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1);
            this.FPS91_TY_S_AC_28UBV661_Sheet1.AddColumnHeaderSpanCell(0, 5, 1, 2);
            this.FPS91_TY_S_AC_28UBV661_Sheet1.AddColumnHeaderSpanCell(0, 7, 1, 2);
            this.FPS91_TY_S_AC_28UBV661_Sheet1.AddColumnHeaderSpanCell(0, 9, 1, 2);
            this.FPS91_TY_S_AC_28UBV661_Sheet1.AddColumnHeaderSpanCell(0, 11, 1, 2);
            this.FPS91_TY_S_AC_28UBV661_Sheet1.AddColumnHeaderSpanCell(0, 13, 1, 2);
            this.FPS91_TY_S_AC_28UBV661_Sheet1.AddColumnHeaderSpanCell(0, 15, 2, 1);

            this.FPS91_TY_S_AC_28UBV661_Sheet1.ColumnHeader.Cells[0, 0].Value = "예산구분";
            this.FPS91_TY_S_AC_28UBV661_Sheet1.ColumnHeader.Cells[0, 1].Value = "사업장";
            this.FPS91_TY_S_AC_28UBV661_Sheet1.ColumnHeader.Cells[0, 2].Value = "집행부서";
            this.FPS91_TY_S_AC_28UBV661_Sheet1.ColumnHeader.Cells[0, 3].Value = "상위계정";
            this.FPS91_TY_S_AC_28UBV661_Sheet1.ColumnHeader.Cells[0, 4].Value = "계정명";
            this.FPS91_TY_S_AC_28UBV661_Sheet1.ColumnHeader.Cells[0, 5].Value = "1분기";

            this.FPS91_TY_S_AC_28UBV661_Sheet1.ColumnHeader.Cells[0, 7].Value = "2분기";

            this.FPS91_TY_S_AC_28UBV661_Sheet1.ColumnHeader.Cells[0, 9].Value = "3분기";

            this.FPS91_TY_S_AC_28UBV661_Sheet1.ColumnHeader.Cells[0, 11].Value = "4분기";

            this.FPS91_TY_S_AC_28UBV661_Sheet1.ColumnHeader.Cells[0, 13].Value = "합계";
            this.FPS91_TY_S_AC_28UBV661_Sheet1.ColumnHeader.Cells[0, 15].Value = "잔액";

            this.FPS91_TY_S_AC_28UBV661_Sheet1.ColumnHeader.Cells[1, 0].Value = "";
            this.FPS91_TY_S_AC_28UBV661_Sheet1.ColumnHeader.Cells[1, 1].Value = "";
            this.FPS91_TY_S_AC_28UBV661_Sheet1.ColumnHeader.Cells[1, 2].Value = "";
            this.FPS91_TY_S_AC_28UBV661_Sheet1.ColumnHeader.Cells[1, 3].Value = "";
            this.FPS91_TY_S_AC_28UBV661_Sheet1.ColumnHeader.Cells[1, 4].Value = "";
            this.FPS91_TY_S_AC_28UBV661_Sheet1.ColumnHeader.Cells[1, 5].Value = "계획";
            this.FPS91_TY_S_AC_28UBV661_Sheet1.ColumnHeader.Cells[1, 6].Value = "집행";

            this.FPS91_TY_S_AC_28UBV661_Sheet1.ColumnHeader.Cells[1, 7].Value = "계획";
            this.FPS91_TY_S_AC_28UBV661_Sheet1.ColumnHeader.Cells[1, 8].Value = "집행";

            this.FPS91_TY_S_AC_28UBV661_Sheet1.ColumnHeader.Cells[1, 9].Value = "계획";
            this.FPS91_TY_S_AC_28UBV661_Sheet1.ColumnHeader.Cells[1, 10].Value = "집행";

            this.FPS91_TY_S_AC_28UBV661_Sheet1.ColumnHeader.Cells[1, 11].Value = "계획";
            this.FPS91_TY_S_AC_28UBV661_Sheet1.ColumnHeader.Cells[1, 12].Value = "집행";

            this.FPS91_TY_S_AC_28UBV661_Sheet1.ColumnHeader.Cells[1, 13].Value = "계획";
            this.FPS91_TY_S_AC_28UBV661_Sheet1.ColumnHeader.Cells[1, 14].Value = "집행";

            this.FPS91_TY_S_AC_28UBV661_Sheet1.ColumnHeader.Cells[1, 15].Value = "";            

            for (int i = 0; i < this.FPS91_TY_S_AC_28UBV661_Sheet1.RowCount; i++)
            {
                //if (i % 2 == 0)
                //{
                    this.FPS91_TY_S_AC_28UBV661_Sheet1.AddSpanCell(i, 5, 1, 1);
                    this.FPS91_TY_S_AC_28UBV661_Sheet1.AddSpanCell(i, 6, 1, 1);

                    this.FPS91_TY_S_AC_28UBV661_Sheet1.AddSpanCell(i, 7, 1, 1);
                    this.FPS91_TY_S_AC_28UBV661_Sheet1.AddSpanCell(i, 8, 1, 1);

                    this.FPS91_TY_S_AC_28UBV661_Sheet1.AddSpanCell(i, 9, 1, 1);
                    this.FPS91_TY_S_AC_28UBV661_Sheet1.AddSpanCell(i, 10, 1, 1);

                    this.FPS91_TY_S_AC_28UBV661_Sheet1.AddSpanCell(i, 11, 1, 1);
                    this.FPS91_TY_S_AC_28UBV661_Sheet1.AddSpanCell(i, 12, 1, 1);
                    this.FPS91_TY_S_AC_28UBV661_Sheet1.AddSpanCell(i, 13, 1, 1);
                    this.FPS91_TY_S_AC_28UBV661_Sheet1.AddSpanCell(i, 14, 1, 1);
                //}
                //else if (i % 2 == 1)
                //{
                    this.FPS91_TY_S_AC_28UBV661_Sheet1.AddSpanCell(i, 5, 1, 1);
                    this.FPS91_TY_S_AC_28UBV661_Sheet1.AddSpanCell(i, 6, 1, 1);

                    this.FPS91_TY_S_AC_28UBV661_Sheet1.AddSpanCell(i, 7, 1, 1);
                    this.FPS91_TY_S_AC_28UBV661_Sheet1.AddSpanCell(i, 8, 1, 1);

                    this.FPS91_TY_S_AC_28UBV661_Sheet1.AddSpanCell(i, 9, 1, 1);
                    this.FPS91_TY_S_AC_28UBV661_Sheet1.AddSpanCell(i, 10, 1, 1);

                    this.FPS91_TY_S_AC_28UBV661_Sheet1.AddSpanCell(i, 11, 1, 1);
                    this.FPS91_TY_S_AC_28UBV661_Sheet1.AddSpanCell(i, 12, 1, 1);
                    this.FPS91_TY_S_AC_28UBV661_Sheet1.AddSpanCell(i, 13, 1, 1);
                    this.FPS91_TY_S_AC_28UBV661_Sheet1.AddSpanCell(i, 14, 1, 1);
                
                    // 스프레드 칼럼 숫자형
                    //TNumberCellType tmpCellType = new TNumberCellType(0, 999999999, -999999999);
                    //tmpCellType.ShowSeparator = true;
                    //tmpCellType.LeadingZero = FarPoint.Win.Spread.CellType.LeadingZero.No; // Zero일 경우 표시 안함.

                    // 스프레드 칼럼 텍스트형
                    //TTextCellType tmpCellType1 = new TTextCellType();
                    //tmpCellType1.CharacterSet = FarPoint.Win.Spread.CellType.CharacterSet.Numeric;

                    // 스프레드 칼럼 ZERO인 경우 안나오게 함.
                    GeneralCellType tmpCellType = new GeneralCellType();
                    tmpCellType.FormatString = "#,###";

                    this.FPS91_TY_S_AC_28UBV661_Sheet1.Cells[i, 5].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_28UBV661_Sheet1.Cells[i, 6].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_28UBV661_Sheet1.Cells[i, 7].CellType = tmpCellType;

                    this.FPS91_TY_S_AC_28UBV661_Sheet1.Cells[i, 8].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_28UBV661_Sheet1.Cells[i, 9].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_28UBV661_Sheet1.Cells[i, 10].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_28UBV661_Sheet1.Cells[i, 11].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_28UBV661_Sheet1.Cells[i, 12].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_28UBV661_Sheet1.Cells[i, 13].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_28UBV661_Sheet1.Cells[i, 14].CellType = tmpCellType;

                    this.FPS91_TY_S_AC_28UBV661_Sheet1.Cells[i, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_28UBV661_Sheet1.Cells[i, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

                    this.FPS91_TY_S_AC_28UBV661_Sheet1.Cells[i, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_28UBV661_Sheet1.Cells[i, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_28UBV661_Sheet1.Cells[i, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

                    this.FPS91_TY_S_AC_28UBV661_Sheet1.Cells[i, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_28UBV661_Sheet1.Cells[i, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_28UBV661_Sheet1.Cells[i, 12].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_28UBV661_Sheet1.Cells[i, 13].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_28UBV661_Sheet1.Cells[i, 14].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                //}
            }

            if (this.FPS91_TY_S_AC_28UBV661_Sheet1.AlternatingRows.Count > 0)
                this.FPS91_TY_S_AC_28UBV661_Sheet1.AlternatingRows[0].BackColor = Color.White;
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sSTDATE = string.Empty;
            string sEDDATE = string.Empty;

            sSTDATE = this.TXT01_GDATE.GetValue().ToString() + "01";

            // 1분기
            if (this.CBO01_GBUNGI.GetValue().ToString() == "1")
            {
                //sSTDATE = this.TXT01_GDATE.GetValue().ToString() + "01";
                sEDDATE = this.TXT01_GDATE.GetValue().ToString() + "03";
            }
            else if (this.CBO01_GBUNGI.GetValue().ToString() == "2") // 2분기
            {
                //sSTDATE = this.TXT01_GDATE.GetValue().ToString() + "04";
                sEDDATE = this.TXT01_GDATE.GetValue().ToString() + "06";
            }
            else if (this.CBO01_GBUNGI.GetValue().ToString() == "3") // 3분기
            {
                //sSTDATE = this.TXT01_GDATE.GetValue().ToString() + "07";
                sEDDATE = this.TXT01_GDATE.GetValue().ToString() + "09";
            }
            else if (this.CBO01_GBUNGI.GetValue().ToString() == "4") // 4분기
            {
                //sSTDATE = this.TXT01_GDATE.GetValue().ToString() + "10";
                sEDDATE = this.TXT01_GDATE.GetValue().ToString() + "12";
            }

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_28UBP656",
                sSTDATE.ToString(),
                sEDDATE.ToString(),
                this.CBO01_GYESAN.GetText(),
                this.CBO01_GYESAN.GetValue(),
                this.CBO01_GYESAN.GetValue(),
                this.CBO01_GYESAN.GetValue(),
                this.CBO01_GYESAN.GetValue(),
                this.CBO01_GYESAN.GetValue(),
                this.CBO01_GYESAN.GetValue(),
                this.CBO01_GYESAN.GetValue(),
                this.CBO01_GYESAN.GetValue(),
                sSTDATE.ToString(),
                sEDDATE.ToString(),
                this.CBH01_GCDDP.GetValue().ToString()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYACLB013R();

                // 가로 출력
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, UP_ConvertDt(dt))).ShowDialog();
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
            int i = 0;

            string sNEWMMCDDP = string.Empty;
            string sOLDMMCDDP = string.Empty;
            string sNEWMMNMDP = string.Empty;
            string sOldMMNMDP = string.Empty;
            string sNEWNMAC   = string.Empty;
            string sOldNMAC   = string.Empty;

            DataTable Condt = new DataTable();

            DataRow row;

            Condt.Columns.Add("MMYYMM1",   typeof(System.String));
            Condt.Columns.Add("MMYYMM2",   typeof(System.String));
            Condt.Columns.Add("MMNMGB",    typeof(System.String));
            Condt.Columns.Add("MMCDDP",    typeof(System.String));
            Condt.Columns.Add("MMCDDP1",   typeof(System.String));
            Condt.Columns.Add("MMNMDP",    typeof(System.String));
            Condt.Columns.Add("MMNMDP1",   typeof(System.String));
            Condt.Columns.Add("NMAC",      typeof(System.String));
            Condt.Columns.Add("MMNMAC",    typeof(System.String));
            Condt.Columns.Add("CPBUNKI01", typeof(System.String));
            Condt.Columns.Add("USBUNKI01", typeof(System.String));
            Condt.Columns.Add("CPBUNKI02", typeof(System.String));
            Condt.Columns.Add("USBUNKI02", typeof(System.String));
            Condt.Columns.Add("CPBUNKI03", typeof(System.String));
            Condt.Columns.Add("USBUNKI03", typeof(System.String));
            Condt.Columns.Add("CPBUNKI04", typeof(System.String));
            Condt.Columns.Add("USBUNKI04", typeof(System.String));
            Condt.Columns.Add("CPHAP",     typeof(System.String));
            Condt.Columns.Add("USHAP",     typeof(System.String));
            Condt.Columns.Add("JAN",       typeof(System.String));
            Condt.Columns.Add("DATE",      typeof(System.String));

            for (i = 0; i < dt.Rows.Count; i++)
            {
                sNEWMMCDDP = dt.Rows[i]["MMCDDP"].ToString();
                sNEWMMNMDP = dt.Rows[i]["MMNMDP"].ToString();
                sNEWNMAC   = dt.Rows[i]["NMAC"].ToString();

                row = Condt.NewRow();

                row["MMYYMM1"] = dt.Rows[i]["MMYYMM1"].ToString();
                row["MMYYMM2"] = dt.Rows[i]["MMYYMM2"].ToString();

                row["MMNMGB"]  = dt.Rows[i]["MMNMGB"].ToString();
                row["MMCDDP"]  = dt.Rows[i]["MMCDDP"].ToString();

                row["MMNMDP"] = dt.Rows[i]["MMNMDP"].ToString();

                if (sNEWMMCDDP != sOLDMMCDDP)
                {
                    row["MMCDDP1"] = dt.Rows[i]["MMCDDP"].ToString();

                    sOLDMMCDDP = sNEWMMCDDP;
                }
                else
                {
                    row["MMCDDP1"] = "";
                }

                if (sNEWMMNMDP != sOldMMNMDP)
                {
                    row["MMNMDP1"] = dt.Rows[i]["MMNMDP"].ToString();

                    sOldMMNMDP = sNEWMMNMDP;
                }
                else
                {
                    row["MMNMDP1"] = "";
                }

                if (sNEWNMAC != sOldNMAC)
                {
                    row["NMAC"] = dt.Rows[i]["NMAC"].ToString();

                    sOldNMAC = sNEWNMAC;
                }
                else
                {
                    row["NMAC"] = "";
                }

                row["MMNMAC"]    = dt.Rows[i]["MMNMAC"].ToString();
                row["CPBUNKI01"] = dt.Rows[i]["CPBUNKI01"].ToString();
                row["USBUNKI01"] = dt.Rows[i]["USBUNKI01"].ToString();
                row["CPBUNKI02"] = dt.Rows[i]["CPBUNKI02"].ToString();
                row["USBUNKI02"] = dt.Rows[i]["USBUNKI02"].ToString();
                row["CPBUNKI03"] = dt.Rows[i]["CPBUNKI03"].ToString();
                row["USBUNKI03"] = dt.Rows[i]["USBUNKI03"].ToString();
                row["CPBUNKI04"] = dt.Rows[i]["CPBUNKI04"].ToString();
                row["USBUNKI04"] = dt.Rows[i]["USBUNKI04"].ToString();
                row["CPHAP"]     = dt.Rows[i]["CPHAP"].ToString();
                row["USHAP"]     = dt.Rows[i]["USHAP"].ToString();
                row["JAN"]       = dt.Rows[i]["JAN"].ToString();
                row["DATE"]      = dt.Rows[i]["DATE"].ToString();

                Condt.Rows.Add(row);
            }

            return Condt;
        }
        #endregion

        #region Description : 예산년도 이벤트
        private void TXT01_GDATE_TextChanged(object sender, EventArgs e)
        {
            this.CBH01_GCDDP.DummyValue = TXT01_GDATE.GetValue() + "0101";
        }
        #endregion
    }
}