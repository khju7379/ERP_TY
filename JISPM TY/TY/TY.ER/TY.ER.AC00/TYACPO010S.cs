using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 장기채권현황 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.07.17 20:03
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_37H85144 : EIS 장기채권현황 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_37H8D146 : EIS 장기채권현황 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  INQ : 조회
    ///  EROCDDP : 사업부
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACPO010S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYACPO010S()
        {
            InitializeComponent();
        }

        private void TYACPO010S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_GSTYYMM.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.CBH01_EROCDDP.DummyValue = this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6) + "01";

            this.SetStartingFocus(this.DTP01_GSTYYMM);

            UP_Set_SpreadTitle(DateTime.Now.AddYears(-1).ToString("yyyy") + "12", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
        }
        #endregion

        #region  Description : 생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYACPO010B()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_37H8D146.Initialize(); 
            this.DbConnector.CommandClear();
            
            DateTime dt = Convert.ToDateTime(this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 4) + "-" + this.DTP01_GSTYYMM.GetString().ToString().Substring(4, 2) + "-" + "01");

            dt = dt.AddYears(-1);

            string sJWDATE = Convert.ToString(dt.Year) + "12";

            UP_Set_SpreadTitle(sJWDATE, this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));

            this.DbConnector.Attach("TY_P_AC_37H85144", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), 
                                                        sJWDATE,
                                                        this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 4)+"01",
                                                        this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6),
                                                        this.CBH01_EROCDDP.GetValue());


            this.FPS91_TY_S_AC_37H8D146.SetValue(UP_SumRowAdd(this.DbConnector.ExecuteDataTable()));

            if (this.FPS91_TY_S_AC_37H8D146.CurrentRowCount > 0)
            {
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_37H8D146, "ERBCDDP", "사업부계", SumRowType.SubTotal);
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_37H8D146, "ERBCDDP", "총 계", SumRowType.Total);
            }

        }
        #endregion

        #region  Description : DTP01_GSTYYMM_ValueChanged 이벤트
        private void DTP01_GSTYYMM_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_EROCDDP.DummyValue = this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6) + "01";
        }
        #endregion

        #region  Description : UP_Set_SpreadTitle() 이벤트
        private void UP_Set_SpreadTitle(string sJunYearDate, string sDangWolDate)
        {
            this.FPS91_TY_S_AC_37H8D146_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_37H8D146_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_37H8D146_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_AC_37H8D146_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_AC_37H8D146_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);
            this.FPS91_TY_S_AC_37H8D146_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);
            this.FPS91_TY_S_AC_37H8D146_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1);
            this.FPS91_TY_S_AC_37H8D146_Sheet1.AddColumnHeaderSpanCell(0, 5, 2, 1);
            this.FPS91_TY_S_AC_37H8D146_Sheet1.AddColumnHeaderSpanCell(0, 6, 2, 1);
            this.FPS91_TY_S_AC_37H8D146_Sheet1.AddColumnHeaderSpanCell(0, 7, 2, 1);

            this.FPS91_TY_S_AC_37H8D146_Sheet1.AddColumnHeaderSpanCell(0, 8, 1, 2);
            this.FPS91_TY_S_AC_37H8D146_Sheet1.AddColumnHeaderSpanCell(0, 9, 1, 2);

            this.FPS91_TY_S_AC_37H8D146_Sheet1.AddColumnHeaderSpanCell(0, 10, 2, 1);

            this.FPS91_TY_S_AC_37H8D146_Sheet1.ColumnHeader.Cells[0, 0].Value = "년  월";
            this.FPS91_TY_S_AC_37H8D146_Sheet1.ColumnHeader.Cells[0, 1].Value = "사업부";
            this.FPS91_TY_S_AC_37H8D146_Sheet1.ColumnHeader.Cells[0, 2].Value = "사업부명";
            this.FPS91_TY_S_AC_37H8D146_Sheet1.ColumnHeader.Cells[0, 3].Value = "부실코드";
            this.FPS91_TY_S_AC_37H8D146_Sheet1.ColumnHeader.Cells[0, 4].Value = "부실명";
            this.FPS91_TY_S_AC_37H8D146_Sheet1.ColumnHeader.Cells[0, 5].Value = "거래처";
            this.FPS91_TY_S_AC_37H8D146_Sheet1.ColumnHeader.Cells[0, 6].Value = "거래처명";
            this.FPS91_TY_S_AC_37H8D146_Sheet1.ColumnHeader.Cells[0, 7].Value = sJunYearDate.Substring(0, 4) + "년" + sJunYearDate.Substring(4, 2) + "월 장기채권";

            this.FPS91_TY_S_AC_37H8D146_Sheet1.ColumnHeader.Cells[0, 8].Value = sDangWolDate.Substring(0, 4) + "년 증(감)내역 ";
            this.FPS91_TY_S_AC_37H8D146_Sheet1.ColumnHeader.Cells[1, 8].Value = "채권증가액";
            this.FPS91_TY_S_AC_37H8D146_Sheet1.ColumnHeader.Cells[1, 9].Value = "채권감소액";

            this.FPS91_TY_S_AC_37H8D146_Sheet1.ColumnHeader.Cells[0, 10].Value = "장기채권 잔액";

            this.FPS91_TY_S_AC_37H8D146_Sheet1.ColumnHeader.Cells[0, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;            
        }
        #endregion


        #region  Description : UP_SumRowAdd 소계, 합계 넣기
        private DataTable UP_SumRowAdd(DataTable dt)
        {
            int i = 0;

            string sFilter = "";

            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;

            if (nNum > 0)
            {

                for (i = 1; i < nNum; i++)
                {
                    //사업부 합계
                    if (table.Rows[i - 1]["ERBCDDP"].ToString() != table.Rows[i]["ERBCDDP"].ToString())
                    {                        
                     
                            row = table.NewRow();
                            table.Rows.InsertAt(row, i);

                            // 합 계 이름 넣기                           
                            sFilter = "ERBCDDP = '" + table.Rows[i - 1]["ERBCDDP"].ToString() + "' ";

                            table.Rows[i]["ERBCDDP"] = "사업부계";

                            table.Rows[i]["ERBBALANCEAMT"] = table.Compute("SUM(ERBBALANCEAMT)", sFilter).ToString();
                            table.Rows[i]["ERBOCCURAMT"] = table.Compute("SUM(ERBOCCURAMT)", sFilter).ToString();
                            table.Rows[i]["ERBCOLLECTAMT"] = table.Compute("SUM(ERBCOLLECTAMT)", sFilter).ToString();
                            table.Rows[i]["LONGBONDAMT"] = table.Compute("SUM(LONGBONDAMT)", sFilter).ToString();

                            nNum = nNum + 1;

                            i = i + 1;                        
                    }                   
                   
                }

                /******* 마지막 거래처의 대한 로우 생성*****/
                
                row = table.NewRow();
                table.Rows.InsertAt(row, i);

                //사업부 소 계 이름 넣기
                table.Rows[i]["ERBCDDP"] = "사업부계";

                sFilter = "ERBCDDP = '" + dt.Rows[i - 1]["ERBCDDP"].ToString() + "' ";

                table.Rows[i]["ERBBALANCEAMT"] = dt.Compute("SUM(ERBBALANCEAMT)", sFilter).ToString();
                table.Rows[i]["ERBOCCURAMT"] = dt.Compute("SUM(ERBOCCURAMT)", sFilter).ToString();
                table.Rows[i]["ERBCOLLECTAMT"] = dt.Compute("SUM(ERBCOLLECTAMT)", sFilter).ToString();
                table.Rows[i]["LONGBONDAMT"] = dt.Compute("SUM(LONGBONDAMT)", sFilter).ToString();  

                ///******** 총계를 위한 Row 생성 **************/                

                row = table.NewRow();
                table.Rows.InsertAt(row, i + 1);

                sFilter = " ERBYYMM <> '' ";

                // 합 계 이름 넣기
                table.Rows[i + 1]["ERBCDDP"] = "총 계";

                table.Rows[i + 1]["ERBBALANCEAMT"] = dt.Compute("SUM(ERBBALANCEAMT)", sFilter).ToString();
                table.Rows[i + 1]["ERBOCCURAMT"] = dt.Compute("SUM(ERBOCCURAMT)", sFilter).ToString();
                table.Rows[i + 1]["ERBCOLLECTAMT"] = dt.Compute("SUM(ERBCOLLECTAMT)", sFilter).ToString();
                table.Rows[i + 1]["LONGBONDAMT"] = dt.Compute("SUM(LONGBONDAMT)", sFilter).ToString();
            }

            return table;

        }
        #endregion
    }
}
