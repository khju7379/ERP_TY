using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TY.Service.Library;
using TY.ER.GB00;
using GrapeCity.ActiveReports;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using FarPoint.Win.Spread.CellType;

namespace TY.ER.AC00
{
    /// <summary>
    /// 접대비 CHECK LIST 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.04.25 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    ///  # 프로시저 정보 ####
    ///  TY_P_AC_24U5Y945 : 접대비 CHECK LIST 조회
    ///  TY_P_AC_24P5T857 : 접대비 CHECK LIST 출력
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_24U1L921 : 접대비 CHECK LIST 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  GDEID : 접대비구분
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACCE004S : TYBase
    {
        #region Description : 페이지 로드
        public TYACCE004S()
        {
            InitializeComponent();
        }

        private void TYACCE004S_Load(object sender, System.EventArgs e)
        {
            UP_Spread_Load();

            SetStartingFocus(this.DTP01_GSTYYMM);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_24U5Y945",
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GEDYYMM.GetValue(),
                this.CBO01_GDEID.GetValue()
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

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // 1행(작성년월, 
                dz.Rows.Add(dt.Rows[i]["YYMM"].ToString(), dt.Rows[i]["B7NOSQ"].ToString(), dt.Rows[i]["B7DTOC"].ToString(),
                            dt.Rows[i]["BKDESC"].ToString(), dt.Rows[i]["B7NOCL"].ToString(), dt.Rows[i]["B7NMCP"].ToString(),
                            dt.Rows[i]["B7ADCL"].ToString(), dt.Rows[i]["B7NMRP"].ToString(), "", "");
                // 2행
                dz.Rows.Add("", "", dt.Rows[i]["A6NOSA"].ToString(),
                            dt.Rows[i]["A6NMSA"].ToString(), "", dt.Rows[i]["B7NOCC"].ToString(),
                            dt.Rows[i]["A6NMPD"].ToString(), "", "", "");

                // 3행
                dz.Rows.Add("", "", dt.Rows[i]["B7REMK"].ToString(),
                            "", "", dt.Rows[i]["B7NOMK"].ToString(),
                            dt.Rows[i]["KBHANGL"].ToString(), dt.Rows[i]["B7AMSE"].ToString(),
                            dt.Rows[i]["B7CGSE"].ToString(), dt.Rows[i]["TOTAMT"].ToString());
            }

            this.FPS91_TY_S_AC_24U1L921.SetValue(dz);

            UP_Spread_Load();
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_24P5T857",
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GEDYYMM.GetValue(),
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GEDYYMM.GetValue(),
                this.CBO01_GDEID.GetValue()
                );

            SectionReport rpt = new TYACCE004R();

            rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

            (new TYERGB001P(rpt, this.DbConnector.ExecuteDataTable())).ShowDialog();
        }
        #endregion

        #region Description : 스프레드 로드
        private void UP_Spread_Load()
        {
            this.FPS91_TY_S_AC_24U1L921_Sheet1.ColumnHeaderRowCount = 3;
            this.FPS91_TY_S_AC_24U1L921_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_24U1L921_Sheet1.AddColumnHeaderSpanCell(0, 0, 3, 1);
            this.FPS91_TY_S_AC_24U1L921_Sheet1.AddColumnHeaderSpanCell(0, 1, 3, 1);
            this.FPS91_TY_S_AC_24U1L921_Sheet1.AddColumnHeaderSpanCell(0, 7, 1, 3);
            this.FPS91_TY_S_AC_24U1L921_Sheet1.AddColumnHeaderSpanCell(1, 3, 1, 2);
            this.FPS91_TY_S_AC_24U1L921_Sheet1.AddColumnHeaderSpanCell(1, 7, 1, 3);

            this.FPS91_TY_S_AC_24U1L921_Sheet1.AddColumnHeaderSpanCell(2, 2, 1, 3);

            this.FPS91_TY_S_AC_24U1L921_Sheet1.ColumnHeader.Cells[0, 0].Value = "작성년월";
            this.FPS91_TY_S_AC_24U1L921_Sheet1.ColumnHeader.Cells[0, 1].Value = "일련번호";
            this.FPS91_TY_S_AC_24U1L921_Sheet1.ColumnHeader.Cells[0, 2].Value = "발행일";
            this.FPS91_TY_S_AC_24U1L921_Sheet1.ColumnHeader.Cells[0, 3].Value = "거래구분<사용처>";
            this.FPS91_TY_S_AC_24U1L921_Sheet1.ColumnHeader.Cells[0, 4].Value = "사업자번호";
            this.FPS91_TY_S_AC_24U1L921_Sheet1.ColumnHeader.Cells[0, 5].Value = "상호";
            this.FPS91_TY_S_AC_24U1L921_Sheet1.ColumnHeader.Cells[0, 6].Value = "주소";
            this.FPS91_TY_S_AC_24U1L921_Sheet1.ColumnHeader.Cells[0, 7].Value = "대표자명";

            this.FPS91_TY_S_AC_24U1L921_Sheet1.ColumnHeader.Cells[1, 0].Value = "";
            this.FPS91_TY_S_AC_24U1L921_Sheet1.ColumnHeader.Cells[1, 1].Value = "<카드발급처>";
            this.FPS91_TY_S_AC_24U1L921_Sheet1.ColumnHeader.Cells[1, 2].Value = "사용자번호";
            this.FPS91_TY_S_AC_24U1L921_Sheet1.ColumnHeader.Cells[1, 3].Value = "카드명";
            this.FPS91_TY_S_AC_24U1L921_Sheet1.ColumnHeader.Cells[1, 4].Value = "";
            this.FPS91_TY_S_AC_24U1L921_Sheet1.ColumnHeader.Cells[1, 5].Value = "카드번호";
            this.FPS91_TY_S_AC_24U1L921_Sheet1.ColumnHeader.Cells[1, 6].Value = "회원성명";
            this.FPS91_TY_S_AC_24U1L921_Sheet1.ColumnHeader.Cells[1, 7].Value = "비고";

            this.FPS91_TY_S_AC_24U1L921_Sheet1.ColumnHeader.Cells[2, 0].Value = "";
            this.FPS91_TY_S_AC_24U1L921_Sheet1.ColumnHeader.Cells[2, 1].Value = "<사용자>";
            this.FPS91_TY_S_AC_24U1L921_Sheet1.ColumnHeader.Cells[2, 2].Value = "사용자내역";
            this.FPS91_TY_S_AC_24U1L921_Sheet1.ColumnHeader.Cells[2, 3].Value = "";
            this.FPS91_TY_S_AC_24U1L921_Sheet1.ColumnHeader.Cells[2, 4].Value = "";
            this.FPS91_TY_S_AC_24U1L921_Sheet1.ColumnHeader.Cells[2, 5].Value = "사원번호";
            this.FPS91_TY_S_AC_24U1L921_Sheet1.ColumnHeader.Cells[2, 6].Value = "사원명";
            this.FPS91_TY_S_AC_24U1L921_Sheet1.ColumnHeader.Cells[2, 7].Value = "접대비";
            this.FPS91_TY_S_AC_24U1L921_Sheet1.ColumnHeader.Cells[2, 8].Value = "봉사료";
            this.FPS91_TY_S_AC_24U1L921_Sheet1.ColumnHeader.Cells[2, 9].Value = "합계";

            for (int i = 0; i < this.FPS91_TY_S_AC_24U1L921_Sheet1.RowCount; i++)
            {
                if (i % 3 == 0)
                {
                    this.FPS91_TY_S_AC_24U1L921_Sheet1.AddSpanCell(i, 0, 3, 1);
                    this.FPS91_TY_S_AC_24U1L921_Sheet1.AddSpanCell(i, 1, 3, 1);
                    this.FPS91_TY_S_AC_24U1L921_Sheet1.AddSpanCell(i, 7, 1, 3);
                }
                else if (i % 3 == 1)
                {
                    this.FPS91_TY_S_AC_24U1L921_Sheet1.AddSpanCell(i, 3, 1, 2);
                    this.FPS91_TY_S_AC_24U1L921_Sheet1.AddSpanCell(i, 7, 1, 3);
                }
                else if (i % 3 == 2)
                {
                    this.FPS91_TY_S_AC_24U1L921_Sheet1.AddSpanCell(i, 2, 1, 3);

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

                    this.FPS91_TY_S_AC_24U1L921_Sheet1.Cells[i, 7].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_24U1L921_Sheet1.Cells[i, 8].CellType = tmpCellType;
                    this.FPS91_TY_S_AC_24U1L921_Sheet1.Cells[i, 9].CellType = tmpCellType;

                    this.FPS91_TY_S_AC_24U1L921_Sheet1.Cells[i, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_24U1L921_Sheet1.Cells[i, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_AC_24U1L921_Sheet1.Cells[i, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                }
            }

            if (this.FPS91_TY_S_AC_24U1L921_Sheet1.AlternatingRows.Count > 0)
                this.FPS91_TY_S_AC_24U1L921_Sheet1.AlternatingRows[0].BackColor = Color.White;
        }
        #endregion
    }
}