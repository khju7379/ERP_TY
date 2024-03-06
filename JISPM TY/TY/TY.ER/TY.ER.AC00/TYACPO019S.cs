using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using FarPoint.Win.Spread.CellType;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 계열사 관리카드 경영실적 조회 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2013.09.30 13:05
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_39UB1914 : EIS 계열사 관리카드 경영실적 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_39UB2916 : EIS 계열사 관리카드 경영실적 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_27P47279 : 년도를 입력하세요.
    ///  TY_M_AC_3992B618 : 작업 할 권한이 없습니다.
    ///  TY_M_MR_2BF50354 : 처리하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  ESBMCUST : 계열사구분
    ///  ESBMYYHD : 처리년
    /// </summary>
    public partial class TYACPO019S : TYBase
    {
        private string fsCompanyCode = string.Empty;

        public TYACPO019S()
        {
            InitializeComponent();
        }

        #region Description : Page_Load
        private void TYACPO019S_Load(object sender, System.EventArgs e)
        {

            switch (TYUserInfo.EmpNo.Substring(0, 2))
            {
                case "HT":
                    fsCompanyCode = "TH";
                    break;
                case "TG":
                    fsCompanyCode = "TG";
                    break;
                case "TS":
                    fsCompanyCode = "TS";
                    break;
                case "TL":
                    fsCompanyCode = "TL";
                    break;
                default:
                    fsCompanyCode = "";
                    break;
            }

            if (fsCompanyCode != "")
            {
                this.CBH01_ESBMCUST.SetValue(fsCompanyCode);
                this.CBH01_ESBMCUST.SetReadOnly(true);
            }

            if (fsCompanyCode != "")
            {
                this.SetStartingFocus(this.TXT01_ESBMYYHD);
            }
            else
            {
                this.SetStartingFocus(this.CBH01_ESBMCUST.CodeText);
            }

            this.TXT01_ESBMYYHD.SetValue(DateTime.Now.ToString("yyyy"));

            UP_Spread_Title(this.TXT01_ESBMYYHD.GetValue().ToString());

            this.BTN61_INQ_Click(null, null);
        } 
        #endregion

        #region Description : 조회
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            UP_Spread_Title(this.TXT01_ESBMYYHD.GetValue().ToString());

            this.DbConnector.CommandClear();

            string sEDCYY = Convert.ToString(int.Parse(this.TXT01_ESBMYYHD.GetValue().ToString().Substring(0, 4)) - 1);

            if (this.CBH01_ESBMCUST.GetValue().ToString() == "TG") // 그레인터미널
            {
                this.DbConnector.Attach("TY_P_AC_3AF5V059",
                                       this.CBH01_ESBMCUST.GetValue(),
                                       this.TXT01_ESBMYYHD.GetValue(),
                                       this.CBH01_ESBMCUST.GetValue(),
                                       this.TXT01_ESBMYYHD.GetValue(),
                                       this.CBH01_ESBMCUST.GetValue(),
                                       sEDCYY
                                       );
            }
            else
            {
                this.DbConnector.Attach("TY_P_AC_39UB1914",
                       this.CBH01_ESBMCUST.GetValue(),
                       this.TXT01_ESBMYYHD.GetValue(),
                       this.CBH01_ESBMCUST.GetValue(),
                       this.TXT01_ESBMYYHD.GetValue(),
                       this.CBH01_ESBMCUST.GetValue(),
                       sEDCYY
                       );
            } 

            this.FPS91_TY_S_AC_39UB2916.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_39UB2916.ActiveSheet.RowCount > 0)
            {
                // 특정 ROW 색깔 입히기
                for (int i = 0; i < this.FPS91_TY_S_AC_39UB2916.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_39UB2916.GetValue(i, "ESMCDAC").ToString() == "01000000" || // 매출
                        this.FPS91_TY_S_AC_39UB2916.GetValue(i, "ESMCDAC").ToString() == "02000000" || // 원가
                        this.FPS91_TY_S_AC_39UB2916.GetValue(i, "ESMCDAC").ToString() == "03000000" || // 매출이익
                        this.FPS91_TY_S_AC_39UB2916.GetValue(i, "ESMCDAC").ToString() == "05000000" || // 영업이익
                        this.FPS91_TY_S_AC_39UB2916.GetValue(i, "ESMCDAC").ToString() == "10000000" || // 영업외손익
                        this.FPS91_TY_S_AC_39UB2916.GetValue(i, "ESMCDAC").ToString() == "11000000"    // 세전이익
                        ) 
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_39UB2916.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    }
                    else if (this.FPS91_TY_S_AC_39UB2916.GetValue(i, "ESMCDAC").ToString() == "13000000")  // 당기순이익
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_39UB2916.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
                    }
                }
            }

        } 
        #endregion

        #region Description : 스프레드 타이틀
        private void UP_Spread_Title(string sDATE)
        {

            string sTwo_Years_Ago = string.Empty;
            string sYears_Ago = string.Empty;

            if (sDATE != "")
            {
                sTwo_Years_Ago = Convert.ToString(int.Parse(sDATE.ToString().Substring(0, 4)) - 2) + "년";
                sYears_Ago = Convert.ToString(int.Parse(sDATE.ToString().Substring(0, 4)) - 1) + "년 실적";
            }


            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeaderRowCount = 3;
            this.FPS91_TY_S_AC_39UB2916_Sheet1.RowHeaderColumnCount = 1;


            this.FPS91_TY_S_AC_39UB2916_Sheet1.AddColumnHeaderSpanCell(0, 00, 3, 1);
            this.FPS91_TY_S_AC_39UB2916_Sheet1.AddColumnHeaderSpanCell(0, 01, 3, 1);
            this.FPS91_TY_S_AC_39UB2916_Sheet1.AddColumnHeaderSpanCell(0, 02, 3, 1);
            this.FPS91_TY_S_AC_39UB2916_Sheet1.AddColumnHeaderSpanCell(0, 03, 3, 1);
            this.FPS91_TY_S_AC_39UB2916_Sheet1.AddColumnHeaderSpanCell(0, 04, 3, 1);
            this.FPS91_TY_S_AC_39UB2916_Sheet1.AddColumnHeaderSpanCell(0, 05, 1, 8);
            this.FPS91_TY_S_AC_39UB2916_Sheet1.AddColumnHeaderSpanCell(0, 13, 1, 8);
            this.FPS91_TY_S_AC_39UB2916_Sheet1.AddColumnHeaderSpanCell(0, 21, 1, 8);
            this.FPS91_TY_S_AC_39UB2916_Sheet1.AddColumnHeaderSpanCell(0, 29, 1, 8);
            this.FPS91_TY_S_AC_39UB2916_Sheet1.AddColumnHeaderSpanCell(0, 37, 2, 2);

            this.FPS91_TY_S_AC_39UB2916_Sheet1.AddColumnHeaderSpanCell(1, 05, 1, 2); //1분기
            this.FPS91_TY_S_AC_39UB2916_Sheet1.AddColumnHeaderSpanCell(1, 07, 1, 2);
            this.FPS91_TY_S_AC_39UB2916_Sheet1.AddColumnHeaderSpanCell(1, 09, 1, 2);
            this.FPS91_TY_S_AC_39UB2916_Sheet1.AddColumnHeaderSpanCell(1, 11, 1, 2);
            this.FPS91_TY_S_AC_39UB2916_Sheet1.AddColumnHeaderSpanCell(1, 13, 1, 2); //2분기
            this.FPS91_TY_S_AC_39UB2916_Sheet1.AddColumnHeaderSpanCell(1, 15, 1, 2);
            this.FPS91_TY_S_AC_39UB2916_Sheet1.AddColumnHeaderSpanCell(1, 17, 1, 2);
            this.FPS91_TY_S_AC_39UB2916_Sheet1.AddColumnHeaderSpanCell(1, 19, 1, 2);
            this.FPS91_TY_S_AC_39UB2916_Sheet1.AddColumnHeaderSpanCell(1, 21, 1, 2); //3분기
            this.FPS91_TY_S_AC_39UB2916_Sheet1.AddColumnHeaderSpanCell(1, 23, 1, 2);
            this.FPS91_TY_S_AC_39UB2916_Sheet1.AddColumnHeaderSpanCell(1, 25, 1, 2);
            this.FPS91_TY_S_AC_39UB2916_Sheet1.AddColumnHeaderSpanCell(1, 27, 1, 2);
            this.FPS91_TY_S_AC_39UB2916_Sheet1.AddColumnHeaderSpanCell(1, 29, 1, 2); //4분기
            this.FPS91_TY_S_AC_39UB2916_Sheet1.AddColumnHeaderSpanCell(1, 31, 1, 2);
            this.FPS91_TY_S_AC_39UB2916_Sheet1.AddColumnHeaderSpanCell(1, 33, 1, 2);
            this.FPS91_TY_S_AC_39UB2916_Sheet1.AddColumnHeaderSpanCell(1, 35, 1, 2);

            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[0, 00].Value = "계열사";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[0, 01].Value = "년도";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[0, 02].Value = "계정코드";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[0, 03].Value = "계정명";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[0, 04].Value = sYears_Ago; // 전년실적
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[0, 05].Value = sDATE+"년 1분기";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[0, 13].Value = sDATE + "년 2분기";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[0, 21].Value = sDATE + "년 3분기";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[0, 29].Value = sDATE + "년 4분기";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[0, 37].Value = sDATE + "년 예상실적";

            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[1, 05].Value = "1월";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[1, 07].Value = "2월";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[1, 09].Value = "3월";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[1, 11].Value = "1분기계";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[1, 13].Value = "4월";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[1, 15].Value = "5월";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[1, 17].Value = "6월";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[1, 19].Value = "2분기계";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[1, 21].Value = "7월";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[1, 23].Value = "8월";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[1, 25].Value = "9월";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[1, 27].Value = "3분기계";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[1, 29].Value = "10월";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[1, 31].Value = "11월";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[1, 33].Value = "12월";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[1, 35].Value = "4분기계";

            
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 05].Value = "계획";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 06].Value = "실적";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 07].Value = "계획";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 08].Value = "실적";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 09].Value = "계획";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 10].Value = "실적";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 11].Value = "계획";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 12].Value = "실적";

            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 13].Value = "계획";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 14].Value = "실적";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 15].Value = "계획";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 16].Value = "실적";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 17].Value = "계획";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 18].Value = "실적";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 19].Value = "계획";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 20].Value = "실적";

            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 21].Value = "계획";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 22].Value = "실적";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 23].Value = "계획";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 24].Value = "실적";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 25].Value = "계획";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 26].Value = "실적";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 27].Value = "계획";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 28].Value = "실적";

            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 29].Value = "계획";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 30].Value = "실적";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 31].Value = "계획";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 32].Value = "실적";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 33].Value = "계획";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 34].Value = "실적";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 35].Value = "계획";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 36].Value = "실적";

            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 37].Value = "계획";
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[2, 38].Value = "실적";

            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[0, 05].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[0, 13].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[0, 21].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[0, 29].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_39UB2916_Sheet1.ColumnHeader.Cells[0, 37].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            //for (int i = 0; i < this.FPS91_TY_S_AC_39UB2916_Sheet1.RowCount; i++)
            //{
            //    // 스프레드 칼럼 ZERO인 경우 안나오게 함.
            //    GeneralCellType tmpCellType = new GeneralCellType();
            //    tmpCellType.FormatString = "#,###";

            //    this.FPS91_TY_S_AC_39UB2916_Sheet1.Cells[i, 2].CellType = tmpCellType;
            //    this.FPS91_TY_S_AC_39UB2916_Sheet1.Cells[i, 4].CellType = tmpCellType;
            //    this.FPS91_TY_S_AC_39UB2916_Sheet1.Cells[i, 6].CellType = tmpCellType;

            //    this.FPS91_TY_S_AC_39UB2916_Sheet1.Cells[i, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //    this.FPS91_TY_S_AC_39UB2916_Sheet1.Cells[i, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //    this.FPS91_TY_S_AC_39UB2916_Sheet1.Cells[i, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //}


        }
        #endregion
    }
}
