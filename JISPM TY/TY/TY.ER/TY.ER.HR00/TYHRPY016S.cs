using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using Shoveling2010.SmartClient.SystemUtility.Internal.Repository;
using TY.Service.Library;
using TY.Service.Library.Controls;
using FarPoint.Win.Spread.CellType;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.Model;
using TY.Service.Library.Controls.TYSpreadCellType;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Text;

namespace TY.ER.HR00
{
    /// <summary>
    /// 급여자료 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.09.16 11:09
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_59GDQ838 : 급여 자료 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_59GDS842 : 급여 자료 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  EDATE : 종료일자
    ///  INQOPTION : 조회구분
    ///  KBBUSEO : 부서
    ///  PMGUBN : 급여구분
    ///  PMSABUN : 사번
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRPY016S : TYBase
    {

        #region  Description : 폼 로드 이벤트
        public TYHRPY016S()
        {
            InitializeComponent();

        }

        private void TYHRPY016S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_INQ.ProcessCheck += new TButton.CheckHandler(BTN61_INQ_ProcessCheck);

            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.CBH01_KBBUSEO.DummyValue = this.DTP01_SDATE.GetString().ToString();

            this.SetStartingFocus(this.DTP01_SDATE);
 
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {           

            this.FPS91_TY_S_HR_59GDS842.Initialize();
            this.FPS91_TY_S_HR_633AN586.Initialize();
            this.DbConnector.CommandClear();

            if (CBO01_INQOPTION.GetValue().ToString() == "P")
            {
                this.FPS91_TY_S_HR_59GDS842.Visible = false;
                this.FPS91_TY_S_HR_633AN586.Visible = true;

                UP_SpreadDetail_Title();
                ////상세
                //this.DbConnector.Attach("TY_P_HR_59GDQ838", this.DTP01_SDATE.GetString().ToString().Substring(0, 6),
                //                                            this.DTP01_EDATE.GetString().ToString().Substring(0, 6),
                //                                            this.CBH01_PMGUBN.GetValue(),
                //                                            this.CBH01_PMSABUN.GetValue(),
                //                                            this.CBH01_PMJKCD.GetValue(),
                //                                            this.CBH01_KBBUSEO.GetValue()
                //                                            );
                //this.FPS91_TY_S_HR_59GDS842.SetValue(UP_DataTableConvert(this.DbConnector.ExecuteDataTable()));

                //if (this.FPS91_TY_S_HR_59GDS842.CurrentRowCount > 0)
                //{
                //    this.SetSpreadSumRow(this.FPS91_TY_S_HR_59GDS842, "PMBUSEONM", "소   계", SumRowType.SubTotal);

                //    this.SetSpreadSumRow(this.FPS91_TY_S_HR_59GDS842, "KBHANGL", "합   계", SumRowType.Total);
                //}

                ////FPS91_TY_S_HR_59GDS842_Sheet1.RemoveColumns(15, FPS91_TY_S_HR_59GDS842_Sheet1.ColumnCount - 15);

                ////int iColumnCount = FPS91_TY_S_HR_59GDS842_Sheet1.ColumnCount;

                //// 스프레드 타이틀 생성
                ////FPS91_TY_S_HR_59GDS842_Sheet1.AddColumns(iColumnCount, 1);


                this.DbConnector.Attach("TY_P_HR_59GDQ838", this.DTP01_SDATE.GetString().ToString().Substring(0, 6),
                                                            this.DTP01_EDATE.GetString().ToString().Substring(0, 6),
                                                            this.CBH01_PMGUBN.GetValue(),
                                                            this.CBH01_PMSABUN.GetValue(),
                                                            this.CBH01_PMJKCD.GetValue(),
                                                            this.CBH01_KBBUSEO.GetValue()
                                                            );
                DataTable dt = this.DbConnector.ExecuteDataTable();

                //급여코드 자료
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_61QAY485");
                DataTable dtpay = this.DbConnector.ExecuteDataTable();

                //급여결과내역 자료
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_61QFM486", this.CBH01_PMGUBN.GetValue(),
                                                            this.DTP01_SDATE.GetString().ToString().Substring(0, 6),
                                                            this.DTP01_EDATE.GetString().ToString().Substring(0, 6),
                                                            this.CBH01_PMSABUN.GetValue(),
                                                            this.CBH01_KBBUSEO.GetValue());
                DataTable dtpayResult = this.DbConnector.ExecuteDataTable();

                UP_Set_DetailDataBinding(dt, dtpay, dtpayResult);
            }
            else
            {
                //this.FPS91_TY_S_HR_59GDS842.Visible = true;
                //this.FPS91_TY_S_HR_633AN586.Visible = false;

                //UP_TotalSpread_Title();

                ////집계
                //this.DbConnector.Attach("TY_P_HR_59MDH884", this.DTP01_SDATE.GetString().ToString().Substring(0, 6),
                //                                            this.DTP01_EDATE.GetString().ToString().Substring(0, 6),
                //                                            this.CBH01_PMGUBN.GetValue(),
                //                                            this.CBH01_PMSABUN.GetValue(),
                //                                            this.CBH01_PMJKCD.GetValue(),
                //                                            this.CBH01_KBBUSEO.GetValue()
                //                                            );
                //this.FPS91_TY_S_HR_59GDS842.SetValue(this.DbConnector.ExecuteDataTable());

                //if (this.FPS91_TY_S_HR_59GDS842.CurrentRowCount > 0)
                //{
                //    this.SpreadSumRowAdd(this.FPS91_TY_S_HR_59GDS842, "KBHANGL", "합  계", SumRowType.Total, "PMPAYTOTAL", "PMTAXTOTAL", "PMAFTERTOTAL", "PMINCOMETAX", "PMRESTAX", "PMNATIONAMT", "PMHEALTHAMT", "PMEMPLOYAMT", "PMLTERMAMT", "PMHFOTTIME", "PMWKOTTIME", "PMNTOTTIME", "PMHTOTTIME", "PMGJOTTIME", "PMHFAMOUNT", "PMOTAMOUNT", "PMNTAMOUNT", "PMHTAMOUNT", "PMGJAMOUNT");
                //}

                this.FPS91_TY_S_HR_59GDS842.Visible = false;
                this.FPS91_TY_S_HR_633AN586.Visible = true;

                UP_SpreadDetail_Title();

                this.DbConnector.Attach("TY_P_HR_87OHC474", this.DTP01_SDATE.GetString().ToString().Substring(0, 6),
                                                            this.DTP01_EDATE.GetString().ToString().Substring(0, 6),
                                                            this.CBH01_PMGUBN.GetValue(),
                                                            this.CBH01_PMSABUN.GetValue(),
                                                            this.CBH01_PMJKCD.GetValue(),
                                                            this.CBH01_KBBUSEO.GetValue()
                                                            );
                DataTable dt = this.DbConnector.ExecuteDataTable();

                //급여코드 자료
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_61QAY485");
                DataTable dtpay = this.DbConnector.ExecuteDataTable();

                //급여결과내역 자료
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_87OHA473", this.CBH01_PMGUBN.GetValue(),
                                                            this.DTP01_SDATE.GetString().ToString().Substring(0, 6),
                                                            this.DTP01_EDATE.GetString().ToString().Substring(0, 6),
                                                            this.CBH01_PMSABUN.GetValue(),
                                                            this.CBH01_KBBUSEO.GetValue());
                DataTable dtpayResult = this.DbConnector.ExecuteDataTable();

                UP_Set_TotalDataBinding(dt, dtpay, dtpayResult);

            }


        }
        
        private void BTN61_INQ_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if ( Convert.ToInt32(this.DTP01_SDATE.GetString().ToString().Substring(0, 6)) > Convert.ToInt32((this.DTP01_EDATE.GetString().ToString().Substring(0, 6))) )
            {
                this.ShowCustomMessage("시작일자가 종료일자보다 클수 없습니다", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                this.DTP01_SDATE.Focus(); 
                e.Successed = false;
                return;
            }
        }        
        #endregion

        #region  Description : DataTable Convert
        private DataTable UP_DataTableConvert(DataTable dt)
        {
            int i = 0;
            string sKBSABUN = string.Empty;

            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;

            for (i = 1; i < nNum; i++)
            {
                if (table.Rows[i - 1]["PMSABUN"].ToString() != table.Rows[i]["PMSABUN"].ToString())
                {
                    // 소계
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    // 소 계 이름 넣기
                    table.Rows[i]["PMBUSEONM"] = "소   계";

                    sKBSABUN = "PMSABUN = '" + table.Rows[i - 1]["PMSABUN"].ToString() + "' ";

                    table.Rows[i]["PMPAYTOTAL"] = table.Compute("SUM(PMPAYTOTAL)", sKBSABUN).ToString();
                    table.Rows[i]["PMTAXTOTAL"] = table.Compute("SUM(PMTAXTOTAL)", sKBSABUN).ToString();
                    table.Rows[i]["PMAFTERTOTAL"] = table.Compute("SUM(PMAFTERTOTAL)", sKBSABUN).ToString();

                    table.Rows[i]["PMINCOMETAX"] = table.Compute("SUM(PMINCOMETAX)", sKBSABUN).ToString();
                    table.Rows[i]["PMRESTAX"] = table.Compute("SUM(PMRESTAX)", sKBSABUN).ToString();
                    table.Rows[i]["PMNATIONAMT"] = table.Compute("SUM(PMNATIONAMT)", sKBSABUN).ToString();
                    table.Rows[i]["PMHEALTHAMT"] = table.Compute("SUM(PMHEALTHAMT)", sKBSABUN).ToString();
                    table.Rows[i]["PMEMPLOYAMT"] = table.Compute("SUM(PMEMPLOYAMT)", sKBSABUN).ToString();
                    table.Rows[i]["PMLTERMAMT"] = table.Compute("SUM(PMLTERMAMT)", sKBSABUN).ToString();

                    table.Rows[i]["PMHFAMOUNT"] = table.Compute("SUM(PMHFAMOUNT)", sKBSABUN).ToString();
                    table.Rows[i]["PMOTAMOUNT"] = table.Compute("SUM(PMOTAMOUNT)", sKBSABUN).ToString();
                    table.Rows[i]["PMNTAMOUNT"] = table.Compute("SUM(PMNTAMOUNT)", sKBSABUN).ToString();
                    table.Rows[i]["PMHTAMOUNT"] = table.Compute("SUM(PMHTAMOUNT)", sKBSABUN).ToString();
                    
                    
                    nNum = nNum + 1;

                    i = i + 1;
                }
            }

            if (nNum > 0)
            {
                // 소계
                row = table.NewRow();
                table.Rows.InsertAt(row, i);

                table.Rows[i]["PMBUSEONM"] = "소   계";

                sKBSABUN = "PMSABUN = '" + table.Rows[i - 1]["PMSABUN"].ToString() + "' ";

                table.Rows[i]["PMPAYTOTAL"] = table.Compute("SUM(PMPAYTOTAL)", sKBSABUN).ToString();
                table.Rows[i]["PMTAXTOTAL"] = table.Compute("SUM(PMTAXTOTAL)", sKBSABUN).ToString();
                table.Rows[i]["PMAFTERTOTAL"] = table.Compute("SUM(PMAFTERTOTAL)", sKBSABUN).ToString();

                table.Rows[i]["PMINCOMETAX"] = table.Compute("SUM(PMINCOMETAX)", sKBSABUN).ToString();
                table.Rows[i]["PMRESTAX"] = table.Compute("SUM(PMRESTAX)", sKBSABUN).ToString();
                table.Rows[i]["PMNATIONAMT"] = table.Compute("SUM(PMNATIONAMT)", sKBSABUN).ToString();
                table.Rows[i]["PMHEALTHAMT"] = table.Compute("SUM(PMHEALTHAMT)", sKBSABUN).ToString();
                table.Rows[i]["PMEMPLOYAMT"] = table.Compute("SUM(PMEMPLOYAMT)", sKBSABUN).ToString();
                table.Rows[i]["PMLTERMAMT"] = table.Compute("SUM(PMLTERMAMT)", sKBSABUN).ToString();

                table.Rows[i]["PMHFAMOUNT"] = table.Compute("SUM(PMHFAMOUNT)", sKBSABUN).ToString();
                table.Rows[i]["PMOTAMOUNT"] = table.Compute("SUM(PMOTAMOUNT)", sKBSABUN).ToString();
                table.Rows[i]["PMNTAMOUNT"] = table.Compute("SUM(PMNTAMOUNT)", sKBSABUN).ToString();
                table.Rows[i]["PMHTAMOUNT"] = table.Compute("SUM(PMHTAMOUNT)", sKBSABUN).ToString();


                //총합계
                i = i + 1;

                row = table.NewRow();
                table.Rows.InsertAt(row, i);

                table.Rows[i]["KBHANGL"] = "합   계";

                sKBSABUN = "PMSABUN <> '' ";

                table.Rows[i]["PMPAYTOTAL"] = dt.Compute("SUM(PMPAYTOTAL)", sKBSABUN).ToString();
                table.Rows[i]["PMTAXTOTAL"] = dt.Compute("SUM(PMTAXTOTAL)", sKBSABUN).ToString();
                table.Rows[i]["PMAFTERTOTAL"] = dt.Compute("SUM(PMAFTERTOTAL)", sKBSABUN).ToString();

                table.Rows[i]["PMINCOMETAX"] = dt.Compute("SUM(PMINCOMETAX)", sKBSABUN).ToString();
                table.Rows[i]["PMRESTAX"] = dt.Compute("SUM(PMRESTAX)", sKBSABUN).ToString();
                table.Rows[i]["PMNATIONAMT"] = dt.Compute("SUM(PMNATIONAMT)", sKBSABUN).ToString();
                table.Rows[i]["PMHEALTHAMT"] = dt.Compute("SUM(PMHEALTHAMT)", sKBSABUN).ToString();
                table.Rows[i]["PMEMPLOYAMT"] = dt.Compute("SUM(PMEMPLOYAMT)", sKBSABUN).ToString();
                table.Rows[i]["PMLTERMAMT"] = dt.Compute("SUM(PMLTERMAMT)", sKBSABUN).ToString();

                table.Rows[i]["PMHFAMOUNT"] = dt.Compute("SUM(PMHFAMOUNT)", sKBSABUN).ToString();
                table.Rows[i]["PMOTAMOUNT"] = dt.Compute("SUM(PMOTAMOUNT)", sKBSABUN).ToString();
                table.Rows[i]["PMNTAMOUNT"] = dt.Compute("SUM(PMNTAMOUNT)", sKBSABUN).ToString();
                table.Rows[i]["PMHTAMOUNT"] = dt.Compute("SUM(PMHTAMOUNT)", sKBSABUN).ToString();
            }

            return table;
        }
        #endregion

        #region  Description : DTP01_SDATE_ValueChanged 이벤트
        private void DTP01_SDATE_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_KBBUSEO.DummyValue = this.DTP01_SDATE.GetString().ToString();
        }
        #endregion

        #region Description : 스프레드 타이틀 변경(집계)
        private void UP_TotalSpread_Title()
        {
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_HR_59GDS842_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_HR_59GDS842_Sheet1.AddColumnHeaderSpanCell(0, 0, 1, 12);
            this.FPS91_TY_S_HR_59GDS842_Sheet1.AddColumnHeaderSpanCell(0, 12, 1, 3);
            this.FPS91_TY_S_HR_59GDS842_Sheet1.AddColumnHeaderSpanCell(0, 15, 1, 9);
            this.FPS91_TY_S_HR_59GDS842_Sheet1.AddColumnHeaderSpanCell(0, 24, 1, 12);
            //this.FPS91_TY_S_HR_59GDS842_Sheet1.AddColumnHeaderSpanCell(0, 35, 1, 7);

            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[0, 0].Value = "기  본  사  항";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[0, 12].Value = "지  급";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[0, 15].Value = "공  제";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[0, 24].Value = "연  장";
            //this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[0, 34].Value = "근로 및 예외사항";

            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 0].Value = "급여구분";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 1].Value = "급여년월";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 2].Value = "지급일자";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 3].Value = "사번";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 4].Value = "이름";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 5].Value = "부서";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 6].Value = "부서명";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 7].Value = "직위";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 8].Value = "직위명";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 9].Value = "직급";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 10].Value = "직급명";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 11].Value = "호봉";

            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 12].Value = "지급합계";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 13].Value = "공제합계";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 14].Value = "차인지급액";

            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 15].Value = "소득세";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 16].Value = "주민세";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 17].Value = "국민연금";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 18].Value = "건강보험";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 19].Value = "고용보험";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 20].Value = "장기요양보험";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 21].Value = "과표액";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 22].Value = "산출세액";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 23].Value = "근로공제";

            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 24].Value = "통상임금";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 25].Value = "OT용상여금";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 26].Value = "심야시간";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 27].Value = "연장시간";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 28].Value = "야간시간";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 29].Value = "휴일시간";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 30].Value = "고정시간";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 31].Value = "심야금액";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 32].Value = "연장금액";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 33].Value = "야간금액";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 34].Value = "휴일금액";
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 35].Value = "고정금액";

            //this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 34].Value = "지급율";
            //this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 35].Value = "근무일자";
            //this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 36].Value = "근무일수";
            //this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 37].Value = "예외코드";
            //this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 38].Value = "예외사유";
            //this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 39].Value = "예외기간";
            //this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[1, 40].Value = "예외지급율";

            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[0, 12].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[0, 15].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[0, 24].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_59GDS842_Sheet1.ColumnHeader.Cells[0, 34].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }

        private void UP_SpreadDetail_Title()
        {
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_HR_633AN586_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_HR_633AN586_Sheet1.AddColumnHeaderSpanCell(0, 0, 1, 12);
            this.FPS91_TY_S_HR_633AN586_Sheet1.AddColumnHeaderSpanCell(0, 12, 1, 3);

            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[0, 0].Value = "기  본  사  항";
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[0, 12].Value = "지  급";

            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[0, 12].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

        }
        #endregion

        #region Description : FPS91_TY_S_HR_59GDS842_CellDoubleClick 이벤트
        private void FPS91_TY_S_HR_59GDS842_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.FPS91_TY_S_HR_59GDS842.GetValue("PMGUBN").ToString() == "")
            {
                ShowMessage("TY_M_MR_2BF8A365");
            }
            else
            {
                if ((new TYHRPY008I(this.FPS91_TY_S_HR_59GDS842.GetValue("PMGUBN").ToString(),
                                    this.FPS91_TY_S_HR_59GDS842.GetValue("PMYYMM").ToString(), 
                                    this.FPS91_TY_S_HR_59GDS842.GetValue("PMJIDATE").ToString(), 
                                    this.FPS91_TY_S_HR_59GDS842.GetValue("PMSABUN").ToString(),
                                    "READ"
                                    )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region Description : UP_Set_DetailDataBinding 이벤트
        private void UP_Set_DetailDataBinding(DataTable dt, DataTable dtPayTitle, DataTable dtDetail)
        {
            int iStartJ = 0;  //지급항목 시작 인덱스
            int iStartG = 0;  //공제항목 시작 인덱스
            int iMinusIndex  = 0; 
            int iColumsTotal = 0;

            string sPayCode = string.Empty;
            string sPayCodeName = string.Empty;

            string sColName = string.Empty;


            //급여타이틀 만들기
            DataTable dtSpTitle = new DataTable();
            dtSpTitle.Columns.Add("PMGUBN", typeof(System.String));
            //dtSpTitle.Columns[0].ColumnName = "급여구분";
            dtSpTitle.Columns.Add("PMYYMM", typeof(System.String));
            //dtSpTitle.Columns[1].ColumnName = "급여년월";
            dtSpTitle.Columns.Add("PMJIDATE", typeof(System.String));
            //dtSpTitle.Columns[2].ColumnName = "지급일자";
            dtSpTitle.Columns.Add("PMSABUN", typeof(System.String));
            //dtSpTitle.Columns[3].ColumnName = "사 번";
            dtSpTitle.Columns.Add("KBHANGL", typeof(System.String));
            //dtSpTitle.Columns[4].ColumnName = "성 명";
            dtSpTitle.Columns.Add("PMBUSEO", typeof(System.String));
            //dtSpTitle.Columns[5].ColumnName = "부 서";
            dtSpTitle.Columns.Add("PMBUSEONM", typeof(System.String));
            //dtSpTitle.Columns[6].ColumnName = "부서명";
            dtSpTitle.Columns.Add("PMJJCD", typeof(System.String));
            //dtSpTitle.Columns[7].ColumnName = "직위";
            dtSpTitle.Columns.Add("PMJJCDNM", typeof(System.String));
            //dtSpTitle.Columns[8].ColumnName = "직위명";
            dtSpTitle.Columns.Add("PMJKCD", typeof(System.String));
            //dtSpTitle.Columns[9].ColumnName = "직급";
            dtSpTitle.Columns.Add("PMJKCDNM", typeof(System.String));
            //dtSpTitle.Columns[10].ColumnName = "직급명";
            dtSpTitle.Columns.Add("PMHOBN", typeof(System.String));
            //dtSpTitle.Columns[11].ColumnName = "호봉";
            dtSpTitle.Columns.Add("PMPAYTOTAL", typeof(System.Double));
            //dtSpTitle.Columns[12].ColumnName = "지급총액";
            dtSpTitle.Columns.Add("PMTAXTOTAL", typeof(System.Double));
            //dtSpTitle.Columns[13].ColumnName = "공제총액";
            dtSpTitle.Columns.Add("PMAFTERTOTAL", typeof(System.Double));
            //dtSpTitle.Columns[14].ColumnName = "차인지급액";

            if (dtPayTitle.Rows.Count > 0)
            {
                for (int i = 0; i < dtPayTitle.Rows.Count; i++)
                {
                    dtSpTitle.Columns.Add("PMPAYCODE" + i.ToString(), typeof(System.Double));

                    sColName =  dtPayTitle.Rows[i]["PSDNAME"].ToString();
                    sColName = sColName.Replace("(", "");
                    sColName = sColName.Replace(")", "");
                    sColName = sColName.Replace(".", "");
                    sColName = sColName.Replace(" ", "");

                    dtSpTitle.Columns[i + 15].ColumnName = sColName;
                    
                    //지급항목 시작 cell index
                    if (dtPayTitle.Rows[i]["PSDCODE"].ToString().Substring(0, 1) == "1" && iStartJ == 0)
                    {
                        iStartJ = (i + 15);
                    }
                    //공제항목 시작 cell index
                    if (dtPayTitle.Rows[i]["PSDCODE"].ToString().Substring(0, 1) == "2" && iStartG == 0)
                    {
                        iStartG = (i + 15);
                    }

                    iColumsTotal += 1;
                }
            }
            dtSpTitle.TableName = "TableNames";

            DataRow row;

            //급여세부항목 추가
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row = dtSpTitle.NewRow();
                row[0] = dt.Rows[i]["PMGUBN"].ToString();
                row[1] = dt.Rows[i]["PMYYMM"].ToString();
                row[2] = dt.Rows[i]["PMJIDATE"].ToString();
                row[3] = dt.Rows[i]["PMSABUN"].ToString();
                row[4] = dt.Rows[i]["KBHANGL"].ToString();
                row[5] = dt.Rows[i]["PMBUSEO"].ToString();
                row[6] = dt.Rows[i]["PMBUSEONM"].ToString();
                row[7] = dt.Rows[i]["PMJJCD"].ToString();
                row[8] = dt.Rows[i]["PMJJCDNM"].ToString();
                row[9] = dt.Rows[i]["PMJKCD"].ToString();
                row[10] = dt.Rows[i]["PMJKCDNM"].ToString();
                row[11] = dt.Rows[i]["PMHOBN"].ToString();
                row[12] = dt.Rows[i]["PMPAYTOTAL"].ToString();
                row[13] = dt.Rows[i]["PMTAXTOTAL"].ToString();
                row[14] = dt.Rows[i]["PMAFTERTOTAL"].ToString();


                for (int j = 0; j < dtPayTitle.Rows.Count; j++)
                {
                    sPayCode = dtPayTitle.Rows[j]["PSDCODE"].ToString();
                    sPayCodeName = dtPayTitle.Rows[j]["PSDNAME"].ToString();
                    row[j + 15] = 0;

                    foreach (DataRow rw in dtDetail.Select("PSGUBN = '" + dt.Rows[i]["PMGUBN"].ToString() + "' AND PSYYMM = '" + dt.Rows[i]["PMYYMM"].ToString() + "' AND PSJIDATE = '" + dt.Rows[i]["PMJIDATE"].ToString() + "' AND PSSABUN = '" + dt.Rows[i]["PMSABUN"].ToString() + "' AND PSPAYCODE = '" + sPayCode + "'"))
                    {
                        if (rw.ItemArray.Length > 0)
                        {
                            row[j + 15] = Convert.ToDouble(rw.ItemArray[8].ToString());
                        }
                        else
                        {
                            row[j + 15] = 0;
                        }
                    }

                }
                
                dtSpTitle.Rows.Add(row);
            }

            if (dtSpTitle.Rows.Count > 0)
            {
                int nNum = dtSpTitle.Rows.Count;
                int i = 0;
                int index = 0;
                string sPMSABUN = string.Empty;
                
                int nColNum = dtSpTitle.Columns.Count;

                int[] iColRemove;

                iColRemove = new int[nColNum];

                //금액이 없는 컬럼 찾기
                for (i = 15; i < nColNum; i++)
                {
                    sColName = dtSpTitle.Columns[i].ColumnName;

                    double dHap = Convert.ToDouble(dtSpTitle.Compute("Sum("+sColName+")", "PMSABUN <> ''").ToString());

                    if (dHap == 0)
                    {
                        iColRemove[index] = i;

                        index = index + 1;

                        if (iStartG > i)
                        {
                            iMinusIndex = iMinusIndex + 1;
                        }
                    }
                }

                //금액이 없는 컬럼 삭제
                index = 0;
                for (i = 0; i < nColNum; i++)
                {
                    if (iColRemove[i] > 0)
                    {
                        dtSpTitle.Columns.RemoveAt(iColRemove[i] - index);
                        index = index + 1;
                    }
                }

                //소계,합계 처리
                                
                //nColNum = dtSpTitle.Columns.Count;

                //for (i = 1; i < nNum; i++)
                //{
                //    if (dtSpTitle.Rows[i - 1]["PMSABUN"].ToString() != dtSpTitle.Rows[i]["PMSABUN"].ToString())
                //    {
                //        row = dtSpTitle.NewRow();
                //        dtSpTitle.Rows.InsertAt(row, i);

                //        dtSpTitle.Rows[i]["PMBUSEONM"] = "[소     계]";

                //        sPMSABUN = "PMSABUN = '" + dtSpTitle.Rows[i - 1]["PMSABUN"].ToString() + "' ";

                //        dtSpTitle.Rows[i]["PMPAYTOTAL"] = Convert.ToDouble(dtSpTitle.Compute("Sum(PMPAYTOTAL)", sPMSABUN).ToString());
                //        dtSpTitle.Rows[i]["PMTAXTOTAL"] = Convert.ToDouble(dtSpTitle.Compute("Sum(PMTAXTOTAL)", sPMSABUN).ToString());
                //        dtSpTitle.Rows[i]["PMAFTERTOTAL"] = Convert.ToDouble(dtSpTitle.Compute("Sum(PMAFTERTOTAL)", sPMSABUN).ToString());

                //        for (int j = 15; j < nColNum; j++)
                //        {
                //            sColName = dtSpTitle.Columns[j].ColumnName;
                //            dtSpTitle.Rows[i][j] = Convert.ToDouble(dtSpTitle.Compute("Sum(" + sColName + ")", sPMSABUN).ToString());
                //        }

                //        nNum = nNum + 1;

                //        i = i + 1;
                //    }
                //}

                //row = dtSpTitle.NewRow();
                //dtSpTitle.Rows.InsertAt(row, i);

                //dtSpTitle.Rows[i]["PMBUSEONM"] = "[소     계]";

                //sPMSABUN = "PMSABUN = '" + dtSpTitle.Rows[i - 1]["PMSABUN"].ToString() + "' ";

                //dtSpTitle.Rows[i]["PMPAYTOTAL"] = Convert.ToDouble(dtSpTitle.Compute("Sum(PMPAYTOTAL)", sPMSABUN).ToString());
                //dtSpTitle.Rows[i]["PMTAXTOTAL"] = Convert.ToDouble(dtSpTitle.Compute("Sum(PMTAXTOTAL)", sPMSABUN).ToString());
                //dtSpTitle.Rows[i]["PMAFTERTOTAL"] = Convert.ToDouble(dtSpTitle.Compute("Sum(PMAFTERTOTAL)", sPMSABUN).ToString());


                //for (int j = 15; j < nColNum; j++)
                //{
                //    sColName = dtSpTitle.Columns[j].ColumnName;
                //    dtSpTitle.Rows[i][j] = Convert.ToDouble(dtSpTitle.Compute("Sum(" + sColName + ")", sPMSABUN).ToString());
                //}

                //row = dtSpTitle.NewRow();
                //dtSpTitle.Rows.InsertAt(row, i + 1);

                //sPMSABUN = "PMSABUN <> '' ";

                //dtSpTitle.Rows[i + 1]["PMSABUN"] = "[합     계]";

                //dtSpTitle.Rows[i + 1]["PMPAYTOTAL"] = Convert.ToDouble(dtSpTitle.Compute("Sum(PMPAYTOTAL)", sPMSABUN).ToString());
                //dtSpTitle.Rows[i + 1]["PMTAXTOTAL"] = Convert.ToDouble(dtSpTitle.Compute("Sum(PMTAXTOTAL)", sPMSABUN).ToString());
                //dtSpTitle.Rows[i + 1]["PMAFTERTOTAL"] = Convert.ToDouble(dtSpTitle.Compute("Sum(PMAFTERTOTAL)", sPMSABUN).ToString());

                //for (int j = 15; j < nColNum; j++)
                //{
                //    sColName = dtSpTitle.Columns[j].ColumnName;
                //    dtSpTitle.Rows[i+1][j] = Convert.ToDouble(dtSpTitle.Compute("Sum(" + sColName + ")", sPMSABUN).ToString());
                //}
                 

            }

            int iColumnCount = dtSpTitle.Columns.Count;

            FPS91_TY_S_HR_633AN586.Initialize();

            if (FPS91_TY_S_HR_633AN586_Sheet1.ColumnCount > 15)
            {
                FPS91_TY_S_HR_633AN586_Sheet1.RemoveColumns(15, FPS91_TY_S_HR_633AN586_Sheet1.ColumnCount - 15);
            }

            // 스프레드 타이틀 생성
            FPS91_TY_S_HR_633AN586_Sheet1.AddColumns(15, iColumnCount - 15);

            for (int i = 0; i < iColumnCount; i++)
            {
                FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, i].Value = dtSpTitle.Columns[i].ColumnName;
                FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, i].Column.Width = 110;
                if (i < 12)
                {
                    FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                }
                else
                {
                    FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                }
            }

            iStartG = iStartG - iMinusIndex;

            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, 0].Value = "급여구분";
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, 1].Value = "급여년월";
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, 2].Value = "지급일자";
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, 3].Value = "사 번";
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, 4].Value = "성 명";
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, 5].Value = "부 서";
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, 6].Value = "부서명";
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, 7].Value = "직 위";
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, 8].Value = "직위명";
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, 9].Value = "직급";
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, 10].Value = "직급명";
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, 11].Value = "호봉";
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, 12].Value = "지급총액";
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, 13].Value = "공제총액";
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, 14].Value = "차인지급액";


            this.FPS91_TY_S_HR_633AN586_Sheet1.AddColumnHeaderSpanCell(0, iStartJ, 1, iStartG - iStartJ);
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[0, iStartJ].Value = "지급항목";

            if (iColumnCount - iStartG != 0)
            {
                this.FPS91_TY_S_HR_633AN586_Sheet1.AddColumnHeaderSpanCell(0, iStartG, 1, iColumnCount - iStartG);
                this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[0, iStartG].Value = "공제항목";
            }
          
            int iRowIndex = 0;
            
            string sValue = string.Empty;
            GeneralCellType tmpCellType = new GeneralCellType();
            tmpCellType.FormatString = "#,###";                                   
            
            for (int i = 0; i < dtSpTitle.Rows.Count; i++)
            {
                iRowIndex = iRowIndex + 1;
                this.FPS91_TY_S_HR_633AN586.ActiveSheet.AddRows(iRowIndex - 1, 1);
                this.FPS91_TY_S_HR_633AN586.ActiveSheet.RowHeader.Cells[iRowIndex - 1, 0].Text = iRowIndex.ToString();
                for (int j = 0; j < iColumnCount; j++)
                {
                    //sValue = dtSpTitle.Rows[i][j].ToString();
                    this.FPS91_TY_S_HR_633AN586.ActiveSheet.Cells[iRowIndex - 1, j].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    if (j < 12)
                    {
                        this.FPS91_TY_S_HR_633AN586.ActiveSheet.Cells[iRowIndex - 1, j].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        this.FPS91_TY_S_HR_633AN586_Sheet1.Cells[iRowIndex - 1, j].Value = dtSpTitle.Rows[i][j].ToString();
                    }
                    else
                    {
                        this.FPS91_TY_S_HR_633AN586.ActiveSheet.Cells[iRowIndex - 1, j].CellType = tmpCellType;
                        this.FPS91_TY_S_HR_633AN586.ActiveSheet.Cells[iRowIndex - 1, j].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                        if (dtSpTitle.Rows[i][j].ToString() != "")
                        {
                            this.FPS91_TY_S_HR_633AN586_Sheet1.Cells[iRowIndex - 1, j].Value = string.Format("{0:#,###}", Convert.ToDouble(dtSpTitle.Rows[i][j].ToString()));                            
                        }
                        else
                        {
                            this.FPS91_TY_S_HR_633AN586_Sheet1.Cells[iRowIndex - 1, j].Value = "";
                        }
                    }

                    if (dtSpTitle.Rows[i][6].ToString() == "[소     계]")
                    {
                        this.FPS91_TY_S_HR_633AN586.ActiveSheet.Rows[i].BackColor = Color.FromArgb(228, 242, 194);
                    }
                    if (dtSpTitle.Rows[i][3].ToString() == "[합     계]")
                    {
                        this.FPS91_TY_S_HR_633AN586.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    }                   
                }
            }


            //상세, 특정사번일 경우 합계라인 추가
            if (CBO01_INQOPTION.GetValue().ToString() == "P" && CBH01_PMSABUN.GetValue().ToString() != "")
            {
                this.FPS91_TY_S_HR_633AN586_Sheet1.AddRows(this.FPS91_TY_S_HR_633AN586.ActiveSheet.RowCount, 1);
                this.FPS91_TY_S_HR_633AN586_Sheet1.Cells[this.FPS91_TY_S_HR_633AN586.ActiveSheet.RowCount - 1, 0].Value = "[합 계]";
                this.FPS91_TY_S_HR_633AN586_Sheet1.Cells[this.FPS91_TY_S_HR_633AN586.ActiveSheet.RowCount - 1, 12].Value = dt.Compute("SUM(PMPAYTOTAL)", "").ToString();
                this.FPS91_TY_S_HR_633AN586_Sheet1.Cells[this.FPS91_TY_S_HR_633AN586.ActiveSheet.RowCount - 1, 13].Value = dt.Compute("SUM(PMTAXTOTAL)", "").ToString();
                this.FPS91_TY_S_HR_633AN586_Sheet1.Cells[this.FPS91_TY_S_HR_633AN586.ActiveSheet.RowCount - 1, 14].Value = dt.Compute("SUM(PMAFTERTOTAL)", "").ToString();


                string sColumnName = string.Empty;

                for (int j = 0; j < iColumnCount; j++)
                {
                    if (j > 14)
                    {
                        sColumnName = dtSpTitle.Columns[j].ColumnName;
                        this.FPS91_TY_S_HR_633AN586.ActiveSheet.Cells[this.FPS91_TY_S_HR_633AN586.ActiveSheet.RowCount - 1, j].CellType = tmpCellType;
                        this.FPS91_TY_S_HR_633AN586.ActiveSheet.Cells[this.FPS91_TY_S_HR_633AN586.ActiveSheet.RowCount - 1, j].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                        this.FPS91_TY_S_HR_633AN586.ActiveSheet.Cells[this.FPS91_TY_S_HR_633AN586.ActiveSheet.RowCount - 1, j].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

                        if (dtSpTitle.Rows.Count > 0)
                        {
                            this.FPS91_TY_S_HR_633AN586_Sheet1.Cells[this.FPS91_TY_S_HR_633AN586.ActiveSheet.RowCount - 1, j].Value = string.Format("{0:#,###}", Convert.ToDouble(dtSpTitle.Compute("SUM(" + sColumnName + ")", "").ToString()));
                        }
                        else
                        {
                            this.FPS91_TY_S_HR_633AN586_Sheet1.Cells[this.FPS91_TY_S_HR_633AN586.ActiveSheet.RowCount - 1, j].Value = "0";
                        }

                    }
                }

                this.FPS91_TY_S_HR_633AN586.ActiveSheet.Rows[this.FPS91_TY_S_HR_633AN586.ActiveSheet.RowCount - 1].BackColor = Color.FromArgb(218, 239, 244);              
            }

            //this.FPS91_TY_S_HR_633AN586.TSh

            //this.FPS91_TY_S_HR_633AN586.ControlSetting();

            //this.FPS91_TY_S_HR_633AN586.ControlEventReset();

        }
        #endregion

        #region Description : UP_Set_TotalDataBinding 이벤트
        private void UP_Set_TotalDataBinding(DataTable dt, DataTable dtPayTitle, DataTable dtDetail)
        {
            int iStartJ = 0;  //지급항목 시작 인덱스
            int iStartG = 0;  //공제항목 시작 인덱스
            int iMinusIndex = 0;
            int iColumsTotal = 0;

            string sPayCode = string.Empty;
            string sPayCodeName = string.Empty;

            string sColName = string.Empty;


            //급여타이틀 만들기
            DataTable dtSpTitle = new DataTable();
            dtSpTitle.Columns.Add("PMGUBN", typeof(System.String));
            //dtSpTitle.Columns[0].ColumnName = "급여구분";
            dtSpTitle.Columns.Add("PMYYMM", typeof(System.String));
            //dtSpTitle.Columns[1].ColumnName = "급여년월";
            dtSpTitle.Columns.Add("PMJIDATE", typeof(System.String));
            //dtSpTitle.Columns[2].ColumnName = "지급일자";
            dtSpTitle.Columns.Add("PMSABUN", typeof(System.String));
            //dtSpTitle.Columns[3].ColumnName = "사 번";
            dtSpTitle.Columns.Add("KBHANGL", typeof(System.String));
            //dtSpTitle.Columns[4].ColumnName = "성 명";
            dtSpTitle.Columns.Add("PMBUSEO", typeof(System.String));
            //dtSpTitle.Columns[5].ColumnName = "부 서";
            dtSpTitle.Columns.Add("PMBUSEONM", typeof(System.String));
            //dtSpTitle.Columns[6].ColumnName = "부서명";
            dtSpTitle.Columns.Add("PMJJCD", typeof(System.String));
            //dtSpTitle.Columns[7].ColumnName = "직위";
            dtSpTitle.Columns.Add("PMJJCDNM", typeof(System.String));
            //dtSpTitle.Columns[8].ColumnName = "직위명";
            dtSpTitle.Columns.Add("PMJKCD", typeof(System.String));
            //dtSpTitle.Columns[9].ColumnName = "직급";
            dtSpTitle.Columns.Add("PMJKCDNM", typeof(System.String));
            //dtSpTitle.Columns[10].ColumnName = "직급명";
            dtSpTitle.Columns.Add("PMHOBN", typeof(System.String));
            //dtSpTitle.Columns[11].ColumnName = "호봉";
            dtSpTitle.Columns.Add("PMPAYTOTAL", typeof(System.Double));
            //dtSpTitle.Columns[12].ColumnName = "지급총액";
            dtSpTitle.Columns.Add("PMTAXTOTAL", typeof(System.Double));
            //dtSpTitle.Columns[13].ColumnName = "공제총액";
            dtSpTitle.Columns.Add("PMAFTERTOTAL", typeof(System.Double));
            //dtSpTitle.Columns[14].ColumnName = "차인지급액";

            if (dtPayTitle.Rows.Count > 0)
            {
                for (int i = 0; i < dtPayTitle.Rows.Count; i++)
                {
                    dtSpTitle.Columns.Add("PMPAYCODE" + i.ToString(), typeof(System.Double));

                    sColName = dtPayTitle.Rows[i]["PSDNAME"].ToString();
                    sColName = sColName.Replace("(", "");
                    sColName = sColName.Replace(")", "");
                    sColName = sColName.Replace(".", "");
                    sColName = sColName.Replace(" ", "");

                    dtSpTitle.Columns[i + 15].ColumnName = sColName;

                    //지급항목 시작 cell index
                    if (dtPayTitle.Rows[i]["PSDCODE"].ToString().Substring(0, 1) == "1" && iStartJ == 0)
                    {
                        iStartJ = (i + 15);
                    }
                    //공제항목 시작 cell index
                    if (dtPayTitle.Rows[i]["PSDCODE"].ToString().Substring(0, 1) == "2" && iStartG == 0)
                    {
                        iStartG = (i + 15);
                    }

                    iColumsTotal += 1;
                }
            }
            dtSpTitle.TableName = "TableNames";

            DataRow row;

            //급여세부항목 추가
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row = dtSpTitle.NewRow();
                row[0] = dt.Rows[i]["PMGUBN"].ToString();
                row[1] = dt.Rows[i]["PMYYMM"].ToString();
                row[2] = dt.Rows[i]["PMJIDATE"].ToString();
                row[3] = dt.Rows[i]["PMSABUN"].ToString();
                row[4] = dt.Rows[i]["KBHANGL"].ToString();
                row[5] = dt.Rows[i]["PMBUSEO"].ToString();
                row[6] = dt.Rows[i]["PMBUSEONM"].ToString();
                row[7] = dt.Rows[i]["PMJJCD"].ToString();
                row[8] = dt.Rows[i]["PMJJCDNM"].ToString();
                row[9] = dt.Rows[i]["PMJKCD"].ToString();
                row[10] = dt.Rows[i]["PMJKCDNM"].ToString();
                row[11] = dt.Rows[i]["PMHOBN"].ToString();
                row[12] = dt.Rows[i]["PMPAYTOTAL"].ToString();
                row[13] = dt.Rows[i]["PMTAXTOTAL"].ToString();
                row[14] = dt.Rows[i]["PMAFTERTOTAL"].ToString();


                for (int j = 0; j < dtPayTitle.Rows.Count; j++)
                {
                    sPayCode = dtPayTitle.Rows[j]["PSDCODE"].ToString();
                    sPayCodeName = dtPayTitle.Rows[j]["PSDNAME"].ToString();
                    row[j + 15] = 0;

                    foreach (DataRow rw in dtDetail.Select("PSSABUN = '" + dt.Rows[i]["PMSABUN"].ToString() + "' AND PSPAYCODE = '" + sPayCode + "'"))
                    {
                        if (rw.ItemArray.Length > 0)
                        {
                            row[j + 15] = Convert.ToDouble(rw.ItemArray[8].ToString());
                        }
                        else
                        {
                            row[j + 15] = 0;
                        }
                    }

                }

                dtSpTitle.Rows.Add(row);
            }

            if (dtSpTitle.Rows.Count > 0)
            {
                int nNum = dtSpTitle.Rows.Count;
                int i = 0;
                int index = 0;
                string sPMSABUN = string.Empty;

                int nColNum = dtSpTitle.Columns.Count;

                int[] iColRemove;

                iColRemove = new int[nColNum];

                //금액이 없는 컬럼 찾기
                for (i = 15; i < nColNum; i++)
                {
                    sColName = dtSpTitle.Columns[i].ColumnName;

                    double dHap = Convert.ToDouble(dtSpTitle.Compute("Sum(" + sColName + ")", "PMSABUN <> ''").ToString());

                    if (dHap == 0)
                    {
                        iColRemove[index] = i;

                        index = index + 1;

                        if (iStartG > i)
                        {
                            iMinusIndex = iMinusIndex + 1;
                        }
                    }
                }

                //금액이 없는 컬럼 삭제
                index = 0;
                for (i = 0; i < nColNum; i++)
                {
                    if (iColRemove[i] > 0)
                    {
                        dtSpTitle.Columns.RemoveAt(iColRemove[i] - index);
                        index = index + 1;
                    }
                }

                //소계,합계 처리

                //nColNum = dtSpTitle.Columns.Count;

                //for (i = 1; i < nNum; i++)
                //{
                //    if (dtSpTitle.Rows[i - 1]["PMSABUN"].ToString() != dtSpTitle.Rows[i]["PMSABUN"].ToString())
                //    {
                //        row = dtSpTitle.NewRow();
                //        dtSpTitle.Rows.InsertAt(row, i);

                //        dtSpTitle.Rows[i]["PMBUSEONM"] = "[소     계]";

                //        sPMSABUN = "PMSABUN = '" + dtSpTitle.Rows[i - 1]["PMSABUN"].ToString() + "' ";

                //        dtSpTitle.Rows[i]["PMPAYTOTAL"] = Convert.ToDouble(dtSpTitle.Compute("Sum(PMPAYTOTAL)", sPMSABUN).ToString());
                //        dtSpTitle.Rows[i]["PMTAXTOTAL"] = Convert.ToDouble(dtSpTitle.Compute("Sum(PMTAXTOTAL)", sPMSABUN).ToString());
                //        dtSpTitle.Rows[i]["PMAFTERTOTAL"] = Convert.ToDouble(dtSpTitle.Compute("Sum(PMAFTERTOTAL)", sPMSABUN).ToString());

                //        for (int j = 15; j < nColNum; j++)
                //        {
                //            sColName = dtSpTitle.Columns[j].ColumnName;
                //            dtSpTitle.Rows[i][j] = Convert.ToDouble(dtSpTitle.Compute("Sum(" + sColName + ")", sPMSABUN).ToString());
                //        }

                //        nNum = nNum + 1;

                //        i = i + 1;
                //    }
                //}

                //row = dtSpTitle.NewRow();
                //dtSpTitle.Rows.InsertAt(row, i);

                //dtSpTitle.Rows[i]["PMBUSEONM"] = "[소     계]";

                //sPMSABUN = "PMSABUN = '" + dtSpTitle.Rows[i - 1]["PMSABUN"].ToString() + "' ";

                //dtSpTitle.Rows[i]["PMPAYTOTAL"] = Convert.ToDouble(dtSpTitle.Compute("Sum(PMPAYTOTAL)", sPMSABUN).ToString());
                //dtSpTitle.Rows[i]["PMTAXTOTAL"] = Convert.ToDouble(dtSpTitle.Compute("Sum(PMTAXTOTAL)", sPMSABUN).ToString());
                //dtSpTitle.Rows[i]["PMAFTERTOTAL"] = Convert.ToDouble(dtSpTitle.Compute("Sum(PMAFTERTOTAL)", sPMSABUN).ToString());


                //for (int j = 15; j < nColNum; j++)
                //{
                //    sColName = dtSpTitle.Columns[j].ColumnName;
                //    dtSpTitle.Rows[i][j] = Convert.ToDouble(dtSpTitle.Compute("Sum(" + sColName + ")", sPMSABUN).ToString());
                //}

                //row = dtSpTitle.NewRow();
                //dtSpTitle.Rows.InsertAt(row, i + 1);

                //sPMSABUN = "PMSABUN <> '' ";

                //dtSpTitle.Rows[i + 1]["PMSABUN"] = "[합     계]";

                //dtSpTitle.Rows[i + 1]["PMPAYTOTAL"] = Convert.ToDouble(dtSpTitle.Compute("Sum(PMPAYTOTAL)", sPMSABUN).ToString());
                //dtSpTitle.Rows[i + 1]["PMTAXTOTAL"] = Convert.ToDouble(dtSpTitle.Compute("Sum(PMTAXTOTAL)", sPMSABUN).ToString());
                //dtSpTitle.Rows[i + 1]["PMAFTERTOTAL"] = Convert.ToDouble(dtSpTitle.Compute("Sum(PMAFTERTOTAL)", sPMSABUN).ToString());

                //for (int j = 15; j < nColNum; j++)
                //{
                //    sColName = dtSpTitle.Columns[j].ColumnName;
                //    dtSpTitle.Rows[i+1][j] = Convert.ToDouble(dtSpTitle.Compute("Sum(" + sColName + ")", sPMSABUN).ToString());
                //}


            }

            int iColumnCount = dtSpTitle.Columns.Count;

            FPS91_TY_S_HR_633AN586.Initialize();

            if (FPS91_TY_S_HR_633AN586_Sheet1.ColumnCount > 15)
            {
                FPS91_TY_S_HR_633AN586_Sheet1.RemoveColumns(15, FPS91_TY_S_HR_633AN586_Sheet1.ColumnCount - 15);
            }

            // 스프레드 타이틀 생성
            FPS91_TY_S_HR_633AN586_Sheet1.AddColumns(15, iColumnCount - 15);

            for (int i = 0; i < iColumnCount; i++)
            {
                FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, i].Value = dtSpTitle.Columns[i].ColumnName;
                FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, i].Column.Width = 110;
                if (i < 12)
                {
                    FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                }
                else
                {
                    FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                }
            }

            iStartG = iStartG - iMinusIndex;

            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, 0].Value = "급여구분";
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, 1].Value = "급여년월";
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, 2].Value = "지급일자";
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, 3].Value = "사 번";
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, 4].Value = "성 명";
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, 5].Value = "부 서";
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, 6].Value = "부서명";
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, 7].Value = "직 위";
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, 8].Value = "직위명";
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, 9].Value = "직급";
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, 10].Value = "직급명";
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, 11].Value = "호봉";
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, 12].Value = "지급총액";
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, 13].Value = "공제총액";
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[1, 14].Value = "차인지급액";


            this.FPS91_TY_S_HR_633AN586_Sheet1.AddColumnHeaderSpanCell(0, iStartJ, 1, iStartG - iStartJ);
            this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[0, iStartJ].Value = "지급항목";

            if (iColumnCount - iStartG != 0)
            {
                this.FPS91_TY_S_HR_633AN586_Sheet1.AddColumnHeaderSpanCell(0, iStartG, 1, iColumnCount - iStartG);
                this.FPS91_TY_S_HR_633AN586_Sheet1.ColumnHeader.Cells[0, iStartG].Value = "공제항목";
            }

            int iRowIndex = 0;

            string sValue = string.Empty;
            GeneralCellType tmpCellType = new GeneralCellType();
            tmpCellType.FormatString = "#,###";

            for (int i = 0; i < dtSpTitle.Rows.Count; i++)
            {
                iRowIndex = iRowIndex + 1;
                this.FPS91_TY_S_HR_633AN586.ActiveSheet.AddRows(iRowIndex - 1, 1);
                this.FPS91_TY_S_HR_633AN586.ActiveSheet.RowHeader.Cells[iRowIndex - 1, 0].Text = iRowIndex.ToString();
                for (int j = 0; j < iColumnCount; j++)
                {
                    //sValue = dtSpTitle.Rows[i][j].ToString();
                    this.FPS91_TY_S_HR_633AN586.ActiveSheet.Cells[iRowIndex - 1, j].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    if (j < 12)
                    {
                        this.FPS91_TY_S_HR_633AN586.ActiveSheet.Cells[iRowIndex - 1, j].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        this.FPS91_TY_S_HR_633AN586_Sheet1.Cells[iRowIndex - 1, j].Value = dtSpTitle.Rows[i][j].ToString();
                    }
                    else
                    {
                        this.FPS91_TY_S_HR_633AN586.ActiveSheet.Cells[iRowIndex - 1, j].CellType = tmpCellType;
                        this.FPS91_TY_S_HR_633AN586.ActiveSheet.Cells[iRowIndex - 1, j].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                        if (dtSpTitle.Rows[i][j].ToString() != "")
                        {
                            this.FPS91_TY_S_HR_633AN586_Sheet1.Cells[iRowIndex - 1, j].Value = string.Format("{0:#,###}", Convert.ToDouble(dtSpTitle.Rows[i][j].ToString()));
                        }
                        else
                        {
                            this.FPS91_TY_S_HR_633AN586_Sheet1.Cells[iRowIndex - 1, j].Value = "";
                        }
                    }

                    if (dtSpTitle.Rows[i][6].ToString() == "[소     계]")
                    {
                        this.FPS91_TY_S_HR_633AN586.ActiveSheet.Rows[i].BackColor = Color.FromArgb(228, 242, 194);
                    }
                    if (dtSpTitle.Rows[i][3].ToString() == "[합     계]")
                    {
                        this.FPS91_TY_S_HR_633AN586.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    }

                }
            }

            //this.FPS91_TY_S_HR_633AN586.TSh

            //this.FPS91_TY_S_HR_633AN586.ControlSetting();

            //this.FPS91_TY_S_HR_633AN586.ControlEventReset();

        }
        #endregion

    }
}
