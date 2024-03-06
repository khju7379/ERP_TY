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
using GrapeCity.ActiveReports;

using TY.ER.GB00;
namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 유형자산현황 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.11.08 15:48
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_9B8GB490 : EIS 유현자산 현황 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_9B8GB492 : EIS 유현자산 현황 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  INQOPTION : 조회구분
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYACPO029S : TYBase
    {
        
        public TYACPO029S()
        {
            InitializeComponent();
        }

        #region Description : Page_Load
        private void TYACPO029S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.UP_Spread_Title(this.DTP01_SDATE.GetString().ToString().Substring(0,6));

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.UP_Spread_Title(this.DTP01_SDATE.GetString().ToString().Substring(0, 6));

            this.FPS91_TY_S_AC_9B8GB492.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_9B8GB490",            
                this.DTP01_SDATE.GetString().ToString().Substring(0, 6),
                this.CBO01_INQOPTION.GetValue().ToString()
                );           

            this.FPS91_TY_S_AC_9B8GB492.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_9B8GB492.CurrentRowCount > 0)
            {
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_9B8GB492, "EDACDACNM", "[합 계]", SumRowType.Sum);
            }
          
        }
        #endregion

        #region Description : 스프레드 타이틀
        private void UP_Spread_Title(string sDATE)
        {
          
            this.FPS91_TY_S_AC_9B8GB492_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_9B8GB492_Sheet1.RowHeaderColumnCount = 1;

            // 세로 컬럼 합치기
            this.FPS91_TY_S_AC_9B8GB492_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_AC_9B8GB492_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_AC_9B8GB492_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);
            this.FPS91_TY_S_AC_9B8GB492_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);

            this.FPS91_TY_S_AC_9B8GB492_Sheet1.ColumnHeader.Cells[0, 0].Value = "년월";
            this.FPS91_TY_S_AC_9B8GB492_Sheet1.ColumnHeader.Cells[0, 1].Value = "사업부";
            this.FPS91_TY_S_AC_9B8GB492_Sheet1.ColumnHeader.Cells[0, 2].Value = "항  목";
            this.FPS91_TY_S_AC_9B8GB492_Sheet1.ColumnHeader.Cells[0, 3].Value = "유형자산";

            this.FPS91_TY_S_AC_9B8GB492_Sheet1.AddColumnHeaderSpanCell(0, 4, 1, 2);
            this.FPS91_TY_S_AC_9B8GB492_Sheet1.AddColumnHeaderSpanCell(0, 6, 1, 2);
            this.FPS91_TY_S_AC_9B8GB492_Sheet1.AddColumnHeaderSpanCell(0, 8, 1, 2);

            this.FPS91_TY_S_AC_9B8GB492_Sheet1.ColumnHeader.Cells[0, 4].Value = Convert.ToString(Convert.ToInt16(sDATE.Substring(0, 4)) - 1) + "년(연말)";
            this.FPS91_TY_S_AC_9B8GB492_Sheet1.ColumnHeader.Cells[0, 6].Value = sDATE.Substring(0, 4) + "년 " + sDATE.Substring(4, 2) + "월 현재";
            this.FPS91_TY_S_AC_9B8GB492_Sheet1.ColumnHeader.Cells[0, 8].Value = "전년대비 증(감)액";            

            this.FPS91_TY_S_AC_9B8GB492_Sheet1.ColumnHeader.Cells[1, 4].Value = "취득금액";
            this.FPS91_TY_S_AC_9B8GB492_Sheet1.ColumnHeader.Cells[1, 5].Value = "미상각잔액";
            this.FPS91_TY_S_AC_9B8GB492_Sheet1.ColumnHeader.Cells[1, 6].Value = "취득금액";
            this.FPS91_TY_S_AC_9B8GB492_Sheet1.ColumnHeader.Cells[1, 7].Value = "미상각잔액";
            this.FPS91_TY_S_AC_9B8GB492_Sheet1.ColumnHeader.Cells[1, 8].Value = "취득금액";
            this.FPS91_TY_S_AC_9B8GB492_Sheet1.ColumnHeader.Cells[1, 9].Value = "미상각잔액";

            this.FPS91_TY_S_AC_9B8GB492_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_9B8GB492_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_9B8GB492_Sheet1.ColumnHeader.Cells[0, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;           

        }
        #endregion

        #region Description : 조회
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            //전년도말
            DateTime dtm = Convert.ToDateTime(Set_Date(this.DTP01_SDATE.GetString().ToString().Substring(0, 6) + "01"));
            string sPreYYMM = dtm.AddYears(-1).Year.ToString() + "12";

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_9BBAQ498", this.DTP01_SDATE.GetString().ToString().Substring(0, 6));
            this.DbConnector.ExecuteTranQuery();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_9B8EV489", this.DTP01_SDATE.GetString().ToString().Substring(0, 6), sPreYYMM);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                
                    this.DbConnector.Attach("TY_P_AC_9B8ET488", this.DTP01_SDATE.GetString().ToString().Substring(0, 6),
                                                                dt.Rows[i]["YD_SAUP"].ToString() + "00000",
                                                                "1",
                                                                dt.Rows[i]["YD_CODE"].ToString(),
                                                                dt.Rows[i]["YP_AMMALAMOUNT"].ToString(),
                                                                dt.Rows[i]["YP_REPJANAMOUNT"].ToString(),
                                                                dt.Rows[i]["YD_AMMALAMOUNT"].ToString(),
                                                                dt.Rows[i]["YD_REPJANAMOUNT"].ToString(),
                                                                dt.Rows[i]["AMMALAMOUNT_GAP"].ToString(),
                                                                dt.Rows[i]["REPJANAMOUNT_GAP"].ToString()
                                                                    );
                    
                }
                if (this.DbConnector.CommandCount > 0)
                {
                    this.DbConnector.ExecuteTranQueryList();
                }
            }

            this.ShowMessage("TY_M_GB_26E30875");
        }
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_GB_26E2Z874"))
            {
                e.Successed = false;
                return;
            }

        }
        #endregion



    }
}
