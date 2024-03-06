using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.BS00
{
    /// <summary>
    /// 당기실적 손익계산서 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.10.11 13:42
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_7ABEQ743 : 당기실적 손익계산서 조회(집계)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_7ABEQ745 : 당기실적 손익계산서 조회(집계)
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  BSJYYMM : 실적생성년월
    ///  INQOPTION : 조회구분
    ///  INQ_GROUPDPAC : 귀속부서그룹
    /// </summary>
    public partial class TYBSSJ011S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYBSSJ011S()
        {
            InitializeComponent();
        }

        private void TYBSSJ011S_Load(object sender, System.EventArgs e)
        {
            CBH01_BSJYYMM.SetValue(UP_Get_LastSJYYMM());

            CBO01_INQOPTION.SetValue("T");

            this.SetStartingFocus(this.CBH01_BSJYYMM);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            if (CBO01_INQOPTION.GetValue().ToString() == "T") //집계
            {
                this.FPS91_TY_S_AC_7ABEQ745.Visible = true;
                this.FPS91_TY_S_AC_7ABGN754.Visible = false;

                this.UP_Set_HTitleSetting();
                this.FPS91_TY_S_AC_7ABEQ745.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_7ABEQ743", this.CBH01_BSJYYMM.GetValue().ToString());
                this.FPS91_TY_S_AC_7ABEQ745.SetValue(UP_InsertToRow(this.DbConnector.ExecuteDataTable()));
            }
            else  //상세
            {
                this.FPS91_TY_S_AC_7ABEQ745.Visible = false;
                this.FPS91_TY_S_AC_7ABGN754.Visible = true;

                this.UP_Set_DTitleSetting();

                this.FPS91_TY_S_AC_7ABGN754.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_7ABGG750", this.CBH01_BSJYYMM.GetValue().ToString(), this.CBO01_INQ_GROUPDPAC.GetValue().ToString() );
                this.FPS91_TY_S_AC_7ABGN754.SetValue(this.DbConnector.ExecuteDataTable());
            }
        }
        #endregion

        #region  Description : 내부거래 ROW 삽입 
        private DataTable UP_InsertToRow(DataTable dt)
        {
            string sFilter = string.Empty;

            double dMAPLAMT = 0;
            double dMASJAMT = 0;            

            //내부거래
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_7ABJ0761", this.CBH01_BSJYYMM.GetValue().ToString().Substring(0,4));
            DataTable dtlc = this.DbConnector.ExecuteDataTable();

            int i = 0;
            DataTable table = new DataTable();

            table = dt;
            DataRow row;
            int nNum = table.Rows.Count;

            for (i = 1; i < nNum; i++)
            {
                dMAPLAMT = 0;
                dMASJAMT = 0;      
                //매출액
                if (table.Rows[i - 1]["APCCDAC"].ToString() == "01000000")
                {
                    //내부거래
                    if (dtlc.Rows.Count > 0)
                    {
                        row = table.NewRow();
                        table.Rows.InsertAt(row, i);

                        table.Rows[i]["APCCDAC"] = table.Rows[i - 1]["APCCDAC"].ToString();
                        table.Rows[i]["APCNMAC"] = "    (내부거래)";

                        sFilter = "PBCDPAC = 'T10000'";
                        dMAPLAMT = Convert.ToDouble(Get_Numeric(dtlc.Compute("SUM(MAPLAMT)", sFilter).ToString()));
                        table.Rows[i]["UTTPLAMT"] = Get_Numeric(dtlc.Compute("SUM(MAPLAMT)", sFilter).ToString());                        
                        sFilter = "PBCDPAC = 'T10000'";
                        dMASJAMT = Convert.ToDouble(Get_Numeric(dtlc.Compute("SUM(MASJAMT)", sFilter).ToString()));
                        table.Rows[i]["UTTSJAMT"] = Get_Numeric(dtlc.Compute("SUM(MASJAMT)", sFilter).ToString());

                        table.Rows[i]["UTTRATE"] = 0;
                        sFilter = "PBCDPAC = 'S10000'";
                        dMAPLAMT = dMAPLAMT + Convert.ToDouble(Get_Numeric(dtlc.Compute("SUM(MAPLAMT)", sFilter).ToString()));
                        table.Rows[i]["SILOPLAMT"] = Get_Numeric(dtlc.Compute("SUM(MAPLAMT)", sFilter).ToString());
                        sFilter = "PBCDPAC = 'S10000'";
                        dMASJAMT = dMASJAMT + Convert.ToDouble(Get_Numeric(dtlc.Compute("SUM(MASJAMT)", sFilter).ToString()));
                        table.Rows[i]["SILOSJAMT"] = Get_Numeric(dtlc.Compute("SUM(MASJAMT)", sFilter).ToString());
                        table.Rows[i]["SILORATE"] = 0;

                        table.Rows[i]["ALLPLAMT"] = dMAPLAMT;
                        table.Rows[i]["ALLSJAMT"] = dMASJAMT;
                        table.Rows[i]["ALLRATE"] = 0;

                        nNum = nNum + 1;

                        i = i + 1;
                    }
                }

                //매출원가
                if (table.Rows[i - 1]["APCCDAC"].ToString() == "02000000")
                {
                    //내부거래
                    if (dtlc.Rows.Count > 0)
                    {
                        row = table.NewRow();
                        table.Rows.InsertAt(row, i);

                        table.Rows[i]["APCCDAC"] = table.Rows[i - 1]["APCCDAC"].ToString();
                        table.Rows[i]["APCNMAC"] = "    (내부거래)";

                        sFilter = "PBCDPAC = 'T10000'";
                        dMAPLAMT = Convert.ToDouble(Get_Numeric(dtlc.Compute("SUM(WONPLAMT)", sFilter).ToString()));
                        table.Rows[i]["UTTPLAMT"] = Get_Numeric(dtlc.Compute("SUM(WONPLAMT)", sFilter).ToString());
                        sFilter = "PBCDPAC = 'T10000'";
                        dMASJAMT = Convert.ToDouble(Get_Numeric(dtlc.Compute("SUM(WONSJAMT)", sFilter).ToString()));
                        table.Rows[i]["UTTSJAMT"] = Get_Numeric(dtlc.Compute("SUM(WONSJAMT)", sFilter).ToString());
                        table.Rows[i]["UTTRATE"] = 0;

                        sFilter = "PBCDPAC = 'S10000'";
                        dMAPLAMT = dMAPLAMT + Convert.ToDouble(Get_Numeric(dtlc.Compute("SUM(WONPLAMT)", sFilter).ToString()));
                        table.Rows[i]["SILOPLAMT"] = Get_Numeric(dtlc.Compute("SUM(WONPLAMT)", sFilter).ToString());
                        sFilter = "PBCDPAC = 'S10000'";
                        dMASJAMT = dMASJAMT + Convert.ToDouble(Get_Numeric(dtlc.Compute("SUM(WONSJAMT)", sFilter).ToString()));
                        table.Rows[i]["SILOSJAMT"] = Get_Numeric(dtlc.Compute("SUM(WONSJAMT)", sFilter).ToString());
                        table.Rows[i]["SILORATE"] = 0;

                        table.Rows[i]["ALLPLAMT"] = dMAPLAMT;
                        table.Rows[i]["ALLSJAMT"] = dMASJAMT;
                        table.Rows[i]["ALLRATE"] = 0;

                        nNum = nNum + 1;

                        i = i + 1;
                    }
                }

                //매출총이익
                if (table.Rows[i - 1]["APCCDAC"].ToString() == "03000000")
                {
                    //내부거래
                    if (dtlc.Rows.Count > 0)
                    {
                        row = table.NewRow();
                        table.Rows.InsertAt(row, i);

                        table.Rows[i]["APCCDAC"] = table.Rows[i - 1]["APCCDAC"].ToString();
                        table.Rows[i]["APCNMAC"] = "    (내부거래)";

                        sFilter = "PBCDPAC = 'T10000'";
                        dMAPLAMT = Convert.ToDouble(Get_Numeric(dtlc.Compute("SUM(PLJANAMT)", sFilter).ToString()));
                        table.Rows[i]["UTTPLAMT"] = Get_Numeric(dtlc.Compute("SUM(PLJANAMT)", sFilter).ToString());
                        sFilter = "PBCDPAC = 'T10000'";
                        dMASJAMT = Convert.ToDouble(Get_Numeric(dtlc.Compute("SUM(SJJANAMT)", sFilter).ToString()));
                        table.Rows[i]["UTTSJAMT"] = Get_Numeric(dtlc.Compute("SUM(SJJANAMT)", sFilter).ToString());

                        table.Rows[i]["UTTRATE"] = 0;
                        sFilter = "PBCDPAC = 'S10000'";
                        dMAPLAMT = dMAPLAMT + Convert.ToDouble(Get_Numeric(dtlc.Compute("SUM(PLJANAMT)", sFilter).ToString()));
                        table.Rows[i]["SILOPLAMT"] = Get_Numeric(dtlc.Compute("SUM(PLJANAMT)", sFilter).ToString());
                        sFilter = "PBCDPAC = 'S10000'";
                        dMASJAMT = dMASJAMT + Convert.ToDouble(Get_Numeric(dtlc.Compute("SUM(SJJANAMT)", sFilter).ToString()));
                        table.Rows[i]["SILOSJAMT"] = Get_Numeric(dtlc.Compute("SUM(SJJANAMT)", sFilter).ToString());
                        table.Rows[i]["SILORATE"] = 0;

                        table.Rows[i]["ALLPLAMT"] = dMAPLAMT;
                        table.Rows[i]["ALLSJAMT"] = dMASJAMT;
                        table.Rows[i]["ALLRATE"] = 0;

                        nNum = nNum + 1;

                        i = i + 1;
                    }
                }
            }

            return table;
        }
        #endregion

        #region Description :  그리드 타이트 셋팅 함수
        private void UP_Set_HTitleSetting()
        {
            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);
            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);
            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1);
            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.AddColumnHeaderSpanCell(0, 5, 2, 1);
            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.AddColumnHeaderSpanCell(0, 6, 2, 1);
            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.AddColumnHeaderSpanCell(0, 7, 2, 1);
            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.AddColumnHeaderSpanCell(0, 8, 2, 1);
            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.AddColumnHeaderSpanCell(0, 9, 2, 1);
            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.AddColumnHeaderSpanCell(0, 10, 2, 1);

            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.AddColumnHeaderSpanCell(0, 2, 1, 3); 
            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.AddColumnHeaderSpanCell(0, 5, 1, 3);
            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.AddColumnHeaderSpanCell(0, 8, 1, 3); 

            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.ColumnHeader.Cells[1, 0].Value = "계정코드";
            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.ColumnHeader.Cells[1, 1].Value = "계정과목";
            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.ColumnHeader.Cells[1, 2].Value = "계  획";
            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.ColumnHeader.Cells[1, 3].Value = "실  적";
            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.ColumnHeader.Cells[1, 4].Value = "달성율(%)";
            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.ColumnHeader.Cells[1, 5].Value = "계  획";
            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.ColumnHeader.Cells[1, 6].Value = "실  적";
            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.ColumnHeader.Cells[1, 7].Value = "달성율(%)";
            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.ColumnHeader.Cells[1, 8].Value = "계  획";
            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.ColumnHeader.Cells[1, 9].Value = "실  적";
            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.ColumnHeader.Cells[1, 10].Value = "달성율(%)";

            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.ColumnHeader.Cells[0, 2].Value = "UTT";
            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.ColumnHeader.Cells[0, 5].Value = "SILO";
            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.ColumnHeader.Cells[0, 8].Value = "전 사";

            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.ColumnHeader.Cells[0, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_7ABEQ745_Sheet1.ColumnHeader.Cells[0, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

        }

        private void UP_Set_DTitleSetting()
        {
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_7ABGN754_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1);
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.AddColumnHeaderSpanCell(0, 5, 2, 1);
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.AddColumnHeaderSpanCell(0, 6, 2, 1);
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.AddColumnHeaderSpanCell(0, 7, 2, 1);
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.AddColumnHeaderSpanCell(0, 8, 2, 1);
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.AddColumnHeaderSpanCell(0, 9, 2, 1);
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.AddColumnHeaderSpanCell(0, 10, 2, 1);
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.AddColumnHeaderSpanCell(0, 11, 2, 1);
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.AddColumnHeaderSpanCell(0, 12, 2, 1);
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.AddColumnHeaderSpanCell(0, 13, 2, 1);
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.AddColumnHeaderSpanCell(0, 14, 2, 1);
            
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.AddColumnHeaderSpanCell(0, 2, 1, 2);
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.AddColumnHeaderSpanCell(0, 4, 1, 2);
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.AddColumnHeaderSpanCell(0, 6, 1, 2);
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.AddColumnHeaderSpanCell(0, 8, 1, 2);
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.AddColumnHeaderSpanCell(0, 10, 1, 2);
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.AddColumnHeaderSpanCell(0, 12, 1, 3);

            this.FPS91_TY_S_AC_7ABGN754_Sheet1.ColumnHeader.Cells[1, 0].Value = "계정코드";
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.ColumnHeader.Cells[1, 1].Value = "계정과목";
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.ColumnHeader.Cells[1, 2].Value = "계  획";
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.ColumnHeader.Cells[1, 3].Value = "실  적";            
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.ColumnHeader.Cells[1, 4].Value = "계  획";
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.ColumnHeader.Cells[1, 5].Value = "실  적";
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.ColumnHeader.Cells[1, 6].Value = "계  획";
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.ColumnHeader.Cells[1, 7].Value = "실  적";
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.ColumnHeader.Cells[1, 8].Value = "계  획";
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.ColumnHeader.Cells[1, 9].Value = "실  적";
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.ColumnHeader.Cells[1, 10].Value = "계  획";
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.ColumnHeader.Cells[1, 11].Value = "실  적";
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.ColumnHeader.Cells[1, 12].Value = "계  획";
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.ColumnHeader.Cells[1, 13].Value = "실  적";
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.ColumnHeader.Cells[1, 14].Value = "달성율(%)";
            
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.ColumnHeader.Cells[0, 2].Value = "3Q 누계";
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.ColumnHeader.Cells[0, 4].Value = "10월";
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.ColumnHeader.Cells[0, 6].Value = "11월";
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.ColumnHeader.Cells[0, 8].Value = "12월";
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.ColumnHeader.Cells[0, 10].Value = "4Q 소계";
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.ColumnHeader.Cells[0, 12].Value = "년 간";


            this.FPS91_TY_S_AC_7ABGN754_Sheet1.ColumnHeader.Cells[0, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.ColumnHeader.Cells[0, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.ColumnHeader.Cells[0, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_7ABGN754_Sheet1.ColumnHeader.Cells[0, 12].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion

        #region Description :  CBO01_INQOPTION_SelectedIndexChanged 함수
        private void CBO01_INQOPTION_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBO01_INQOPTION.GetValue().ToString() == "T")
            {
                CBO01_INQ_GROUPDPAC.Enabled = false;
            }
            else
            {
                CBO01_INQ_GROUPDPAC.Enabled = true;
            }
        }
        #endregion

        #region Description : 최종 실적년월 가져오기
        private string UP_Get_LastSJYYMM()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_7AKAW859");
            string sYYMM = this.DbConnector.ExecuteScalar().ToString();

            return sYYMM;
        }
        #endregion
    }
}
