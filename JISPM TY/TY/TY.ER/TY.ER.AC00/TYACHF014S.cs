using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.AC00
{
    /// <summary>
    /// 충당금 명세서 조회 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2014.06.11 17:38
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_463DW669 : 충당금 명세서 조회
    ///  TY_P_AC_46BD8724 : 충당금 명세서 집계 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_46BDB726 : 충당금 명세서 조회
    ///  TY_S_AC_46BD1725 : 충당금 명세서 집계 조회
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  FXMYYMM : 상각년월
    /// </summary>
    public partial class TYACHF014S: TYBase
    {
        #region Description : 페이지 로드
        public TYACHF014S()
        {
            InitializeComponent();
        }

        private void TYACHF014S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_INQ.ProcessCheck += new TButton.CheckHandler(BTN61_INQ_ProcessCheck);

            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            UP_Spread_Title();

            this.SetStartingFocus(this.DTP01_SDATE);            
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {            
           //유형자산기준
            this.FPS91_TY_S_AC_87GBA394.Initialize();
            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_87GB0392", DTP01_SDATE.GetString().ToString(), DTP01_EDATE.GetString().ToString(), CBH01_FXLMASCODE.GetValue());
            this.FPS91_TY_S_AC_87GBA394.SetValue(UP_DataSetRowHap1(this.DbConnector.ExecuteDataTable()));

            if (this.FPS91_TY_S_AC_87GBA394.CurrentRowCount > 0)
            {
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_87GBA394, "FXSCLASSNM", "[자산 소계]", SumRowType.SubTotal);
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_87GBA394, "FXSCLASSNM", "[자산 총합계]", SumRowType.Total);
            }

            //회계자산기준
            this.FPS91_TY_S_AC_87GEP399.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_87GEO398", DTP01_SDATE.GetString().ToString(), DTP01_EDATE.GetString().ToString(), CBH01_FXLMASCODE.GetValue());
            this.FPS91_TY_S_AC_87GEP399.SetValue(UP_DataSetRowHap2(this.DbConnector.ExecuteDataTable()));

            if (this.FPS91_TY_S_AC_87GEP399.CurrentRowCount > 0)
            {
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_87GEP399, "B2CDACNM", "[자산 소계]", SumRowType.SubTotal);
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_87GEP399, "B2CDACNM", "[자산 총합계]", SumRowType.Total);
            }          
            
            this.SetFocus(this.DTP01_SDATE);
        }
        #endregion

        #region Description : 조회번턴 처리
        private void BTN61_INQ_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            

        } 
        #endregion

        #region Description : 스프레드 타이틀 변경
        private void UP_Spread_Title()
        {
            //유형자산기준
            this.FPS91_TY_S_AC_87GBA394_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_87GBA394_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_87GBA394_Sheet1.AddColumnHeaderSpanCell(0, 0, 1, 10);

            this.FPS91_TY_S_AC_87GBA394_Sheet1.ColumnHeader.Cells[0, 0].Value = "유형자산 내역";
            
            this.FPS91_TY_S_AC_87GBA394_Sheet1.ColumnHeader.Cells[1, 0].Value = "자산년도";
            this.FPS91_TY_S_AC_87GBA394_Sheet1.ColumnHeader.Cells[1, 1].Value = "자산순번";
            this.FPS91_TY_S_AC_87GBA394_Sheet1.ColumnHeader.Cells[1, 2].Value = "가족코드";
            this.FPS91_TY_S_AC_87GBA394_Sheet1.ColumnHeader.Cells[1, 3].Value = "자산번호";
            this.FPS91_TY_S_AC_87GBA394_Sheet1.ColumnHeader.Cells[1, 4].Value = "자 산 명";
            this.FPS91_TY_S_AC_87GBA394_Sheet1.ColumnHeader.Cells[1, 5].Value = "자산분류";
            this.FPS91_TY_S_AC_87GBA394_Sheet1.ColumnHeader.Cells[1, 6].Value = "자산분류명";
            this.FPS91_TY_S_AC_87GBA394_Sheet1.ColumnHeader.Cells[1, 7].Value = "취득일자";
            this.FPS91_TY_S_AC_87GBA394_Sheet1.ColumnHeader.Cells[1, 8].Value = "취득금액";
            this.FPS91_TY_S_AC_87GBA394_Sheet1.ColumnHeader.Cells[1, 9].Value = "전표번호";

            this.FPS91_TY_S_AC_87GBA394_Sheet1.AddColumnHeaderSpanCell(0, 10, 1, 7);
            this.FPS91_TY_S_AC_87GBA394_Sheet1.ColumnHeader.Cells[0, 10].Value = "회계자산 내역";

            this.FPS91_TY_S_AC_87GBA394_Sheet1.ColumnHeader.Cells[1, 10].Value = "자산번호";
            this.FPS91_TY_S_AC_87GBA394_Sheet1.ColumnHeader.Cells[1, 11].Value = "자 산 명";
            this.FPS91_TY_S_AC_87GBA394_Sheet1.ColumnHeader.Cells[1, 12].Value = "계정코드";
            this.FPS91_TY_S_AC_87GBA394_Sheet1.ColumnHeader.Cells[1, 13].Value = "계 정 명";
            this.FPS91_TY_S_AC_87GBA394_Sheet1.ColumnHeader.Cells[1, 14].Value = "전표일자";
            this.FPS91_TY_S_AC_87GBA394_Sheet1.ColumnHeader.Cells[1, 15].Value = "금    액";
            this.FPS91_TY_S_AC_87GBA394_Sheet1.ColumnHeader.Cells[1, 16].Value = "전표번호";

            this.FPS91_TY_S_AC_87GBA394_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_87GBA394_Sheet1.ColumnHeader.Cells[0, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            //회계자산기준

            this.FPS91_TY_S_AC_87GEP399_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_87GEP399_Sheet1.RowHeaderColumnCount = 1;


            this.FPS91_TY_S_AC_87GEP399_Sheet1.AddColumnHeaderSpanCell(0, 0, 1, 7);
            this.FPS91_TY_S_AC_87GEP399_Sheet1.ColumnHeader.Cells[0, 0].Value = "회계자산 내역";

            this.FPS91_TY_S_AC_87GEP399_Sheet1.ColumnHeader.Cells[1, 0].Value = "자산번호";
            this.FPS91_TY_S_AC_87GEP399_Sheet1.ColumnHeader.Cells[1, 1].Value = "자 산 명";
            this.FPS91_TY_S_AC_87GEP399_Sheet1.ColumnHeader.Cells[1, 2].Value = "계정코드";
            this.FPS91_TY_S_AC_87GEP399_Sheet1.ColumnHeader.Cells[1, 3].Value = "계 정 명";
            this.FPS91_TY_S_AC_87GEP399_Sheet1.ColumnHeader.Cells[1, 4].Value = "전표일자";
            this.FPS91_TY_S_AC_87GEP399_Sheet1.ColumnHeader.Cells[1, 5].Value = "금    액";
            this.FPS91_TY_S_AC_87GEP399_Sheet1.ColumnHeader.Cells[1, 6].Value = "전표번호";

            this.FPS91_TY_S_AC_87GEP399_Sheet1.AddColumnHeaderSpanCell(0, 7, 1, 10);

            this.FPS91_TY_S_AC_87GEP399_Sheet1.ColumnHeader.Cells[0, 7].Value = "유형자산 내역";

            this.FPS91_TY_S_AC_87GEP399_Sheet1.ColumnHeader.Cells[1, 7].Value = "자산년도";
            this.FPS91_TY_S_AC_87GEP399_Sheet1.ColumnHeader.Cells[1, 8].Value = "자산순번";
            this.FPS91_TY_S_AC_87GEP399_Sheet1.ColumnHeader.Cells[1, 9].Value = "가족코드";
            this.FPS91_TY_S_AC_87GEP399_Sheet1.ColumnHeader.Cells[1, 10].Value = "자산번호";
            this.FPS91_TY_S_AC_87GEP399_Sheet1.ColumnHeader.Cells[1, 11].Value = "자 산 명";
            this.FPS91_TY_S_AC_87GEP399_Sheet1.ColumnHeader.Cells[1, 12].Value = "자산분류";
            this.FPS91_TY_S_AC_87GEP399_Sheet1.ColumnHeader.Cells[1, 13].Value = "자산분류명";
            this.FPS91_TY_S_AC_87GEP399_Sheet1.ColumnHeader.Cells[1, 14].Value = "취득일자";
            this.FPS91_TY_S_AC_87GEP399_Sheet1.ColumnHeader.Cells[1, 15].Value = "취득금액";
            this.FPS91_TY_S_AC_87GEP399_Sheet1.ColumnHeader.Cells[1, 16].Value = "전표번호";           

            this.FPS91_TY_S_AC_87GEP399_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_87GEP399_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;


        }
        #endregion

        #region Description : 유형자산 기준 합계 처리 함수
        private DataTable UP_DataSetRowHap1(DataTable dt)
        {
            string sFilter = string.Empty;

            double dHapFXSGETAMOUNT = 0;
            double dHapB2AMDR = 0;

            DataTable table = new DataTable();
            table = dt;
            DataRow row;
            int i = 0;
            int nNum = table.Rows.Count;

            if (nNum > 0)
            {
                dHapFXSGETAMOUNT = Convert.ToDouble(Get_Numeric(table.Compute("Sum(FXSGETAMOUNT)", "").ToString()));
                dHapB2AMDR = Convert.ToDouble(Get_Numeric(table.Compute("Sum(FXSGETAMOUNT)", "").ToString()));

                for (i = 1; i < nNum; i++)
                {
                    if (table.Rows[i - 1]["FXSCLASS"].ToString() != table.Rows[i]["FXSCLASS"].ToString())
                    {
                        row = table.NewRow();
                        table.Rows.InsertAt(row, i);

                        sFilter = "FXSCLASS = '" + table.Rows[i - 1]["FXSCLASS"].ToString() + "' ";

                        table.Rows[i]["FXSCLASSNM"] = "[자산 소계]";
                        //취득가액
                        table.Rows[i]["FXSGETAMOUNT"] = Convert.ToDouble(Get_Numeric(table.Compute("Sum(FXSGETAMOUNT)", sFilter).ToString()));
                        //전표금액
                        table.Rows[i]["B2AMDR"] = Convert.ToDouble(Get_Numeric(table.Compute("Sum(B2AMDR)", sFilter).ToString()));

                        nNum = nNum + 1;
                        i = i + 1;
                    }
                }

                row = table.NewRow();
                table.Rows.InsertAt(row, i);

                sFilter = "FXSCLASS = '" + table.Rows[i - 1]["FXSCLASS"].ToString() + "' ";

                table.Rows[i]["FXSCLASSNM"] = "[자산 소계]";
                //취득가액
                table.Rows[i]["FXSGETAMOUNT"] = Convert.ToDouble(Get_Numeric(table.Compute("Sum(FXSGETAMOUNT)", sFilter).ToString()));
                //전표금액
                table.Rows[i]["B2AMDR"] = Convert.ToDouble(Get_Numeric(table.Compute("Sum(B2AMDR)", sFilter).ToString()));

                /******** 총계를 위한 Row 생성 **************/
                row = table.NewRow();
                table.Rows.InsertAt(row, i + 1);

                // 합 계 이름 넣기
                table.Rows[i + 1]["FXSCLASSNM"] = "[자산 총합계]";

                table.Rows[i + 1]["FXSGETAMOUNT"] = dHapFXSGETAMOUNT;
                table.Rows[i + 1]["B2AMDR"] = dHapB2AMDR;
            }            

            return table;
        }
        #endregion

        #region Description : 회계자산 기준 합계 처리 함수
        private DataTable UP_DataSetRowHap2(DataTable dt)
        {
            string sFilter = string.Empty;

            double dHapFXSGETAMOUNT = 0;
            double dHapB2AMDR = 0;

            DataTable table = new DataTable();
            table = dt;
            DataRow row;
            int i = 0;
            int nNum = table.Rows.Count;

            if (nNum > 0)
            {
                dHapFXSGETAMOUNT = Convert.ToDouble(Get_Numeric(table.Compute("Sum(FXSGETAMOUNT)", "").ToString()));
                dHapB2AMDR = Convert.ToDouble(Get_Numeric(table.Compute("Sum(FXSGETAMOUNT)", "").ToString()));

                for (i = 1; i < nNum; i++)
                {
                    if (table.Rows[i - 1]["B2CDAC"].ToString() != table.Rows[i]["B2CDAC"].ToString())
                    {
                        row = table.NewRow();
                        table.Rows.InsertAt(row, i);

                        sFilter = "B2CDAC = '" + table.Rows[i - 1]["B2CDAC"].ToString() + "' ";

                        table.Rows[i]["B2CDACNM"] = "[자산 소계]";
                        //취득가액
                        table.Rows[i]["FXSGETAMOUNT"] = Convert.ToDouble(Get_Numeric(table.Compute("Sum(FXSGETAMOUNT)", sFilter).ToString()));
                        //전표금액
                        table.Rows[i]["B2AMDR"] = Convert.ToDouble(Get_Numeric(table.Compute("Sum(B2AMDR)", sFilter).ToString()));

                        nNum = nNum + 1;
                        i = i + 1;
                    }
                }

                row = table.NewRow();
                table.Rows.InsertAt(row, i);

                sFilter = "B2CDAC = '" + table.Rows[i - 1]["B2CDAC"].ToString() + "' ";

                table.Rows[i]["B2CDACNM"] = "[자산 소계]";
                //취득가액
                table.Rows[i]["FXSGETAMOUNT"] = Convert.ToDouble(Get_Numeric(table.Compute("Sum(FXSGETAMOUNT)", sFilter).ToString()));
                //전표금액
                table.Rows[i]["B2AMDR"] = Convert.ToDouble(Get_Numeric(table.Compute("Sum(B2AMDR)", sFilter).ToString()));

                /******** 총계를 위한 Row 생성 **************/
                row = table.NewRow();
                table.Rows.InsertAt(row, i + 1);

                // 합 계 이름 넣기
                table.Rows[i + 1]["B2CDACNM"] = "[자산 총합계]";

                table.Rows[i + 1]["FXSGETAMOUNT"] = dHapFXSGETAMOUNT;
                table.Rows[i + 1]["B2AMDR"] = dHapB2AMDR;

            }
            return table;
        }
        #endregion
       

      
    }
}