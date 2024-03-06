using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.Drawing; 

namespace TY.ER.AC00
{
    /// <summary>
    /// 원천세 환급금 관리 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2014.09.16 11:12
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_49GGW889 : 원천세 환급금자료 조회
    ///  TY_P_AC_49GGY891 : 원천세 환급금자료 수정
    ///  TY_P_AC_4B3GF298 : 원천세 생성 체크
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_49GH1894 : 원천세 환급금 관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  SAV : 저장
    ///  WASABUN : 사번
    ///  WJBRANCH : 지점구분
    ///  WJREVYYMM : 귀속년월
    /// </summary>
    public partial class TYACTP017I : TYBase
    {
        private string fsCompanyCode = string.Empty;

        #region  Description : 폼 로드 이벤트
        public TYACTP017I()
        {
            InitializeComponent();
        }

        private void TYACTP017I_Load(object sender, System.EventArgs e)
        {
            //this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.DTP01_WJREVYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.SetStartingFocus(this.DTP01_WJREVYYMM);

            //UP_Spread_Title();

            this.FPS91_TY_S_AC_49GH1894.Initialize();
            this.BTN61_INQ_Click(null, null);

        }
        #endregion

        
        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_AC_49GGW889", "1", this.DTP01_WJREVYYMM.GetString().Substring(0, 6));
            this.DbConnector.Attach("TY_P_AC_BCHAV914", "1", this.DTP01_WJREVYYMM.GetString().Substring(0, 6));
            this.FPS91_TY_S_AC_49GH1894.SetValue(this.DbConnector.ExecuteDataTable());

            //if (this.FPS91_TY_S_AC_49GH1894.CurrentRowCount > 0)
            //{
            //    this.SpreadSumRowAdd(this.FPS91_TY_S_AC_49GH1894, "TAXGBNM", "[합    계]", SumRowType.Total, "WJMISTAX", "WJGIRTAX", "WJDEDTAX", "WDGENTAX", "WDTRUTAX", "WDBANKTAX", "WDMERGTAX", "WNMEDTAX", "WNNOWTAX", "WNNEXTTAX", "WNREFTAX");
            //}

            this.SetSpreadSumRow(this.FPS91_TY_S_AC_49GH1894, "TAXGBNM", "계", Color.YellowGreen);

        }
        #endregion


        

        //#region Description : 저장 버튼
        //private void BTN61_SAV_Click(object sender, EventArgs e)
        //{
        //    int i = 0;

        //    DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

        //    if (ds.Tables[1].Rows.Count > 0)
        //    {
        //        this.DbConnector.CommandClear();

        //        for (i = 0; i < ds.Tables[1].Rows.Count; i++)
        //        {
        //            this.DbConnector.Attach("TY_P_AC_49GGY891", 
        //                                                        Get_Numeric(ds.Tables[1].Rows[i]["WNNOWTAX"].ToString()),
        //                                                        Get_Numeric(ds.Tables[1].Rows[i]["WNNEXTTAX"].ToString()),
        //                                                        Get_Numeric(ds.Tables[1].Rows[i]["WNREFTAX"].ToString()) ,
        //                                                        ds.Tables[1].Rows[i]["WJBRANCH"].ToString(),
        //                                                        ds.Tables[1].Rows[i]["WJREVYYMM"].ToString(),
        //                                                        ds.Tables[1].Rows[i]["WJTAXGUBN"].ToString(),
        //                                                        ds.Tables[1].Rows[i]["WJGUNMU"].ToString()
        //                                                        );
        //        }

        //        this.DbConnector.ExecuteTranQueryList();
        //    }

        //    this.ShowMessage("TY_M_GB_23NAD873");
        //    this.BTN61_INQ_Click(null, null);
        //}
        //#endregion

        //#region Description : 자료 저장 ProcessCheck 이벤트
        //private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        //{
        //    DataSet ds = new DataSet();

        //    // 저장
        //    ds.Tables.Add(this.FPS91_TY_S_AC_49GH1894.GetDataSourceInclude(TSpread.TActionType.New,    "WJBRANCH", "WJREVYYMM", "WJTAXGUBN", "WJGUNMU", "WNNOWTAX", "WNNEXTTAX", "WNREFTAX"));
        //    // 스프레드에서 수정 할 항목들
        //    ds.Tables.Add(this.FPS91_TY_S_AC_49GH1894.GetDataSourceInclude(TSpread.TActionType.Update, "WJBRANCH", "WJREVYYMM", "WJTAXGUBN", "WJGUNMU", "WNNOWTAX", "WNNEXTTAX", "WNREFTAX"));

        //    if (ds.Tables[1].Rows.Count == 0)
        //    {
        //        this.ShowMessage("TY_M_AC_2422N250");
        //        e.Successed = false;
        //        return;
        //    }

        //    // 생성 체크 WSUMMARYTF
        //    this.DbConnector.CommandClear();
        //    this.DbConnector.Attach("TY_P_AC_BBG92747", "1", "", this.DTP01_WJREVYYMM.GetValue().ToString());

        //    if (this.DbConnector.ExecuteDataTable().Rows.Count != 0)
        //    {
        //        this.ShowCustomMessage("신고서 생성이 완료 되었습니다(취소후 작업)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        //        e.Successed = false;
        //        this.SetFocus(this.DTP01_WJREVYYMM);
        //        return;
        //    }

        //    if (!this.ShowMessage("TY_M_GB_23NAD871"))
        //    {
        //        e.Successed = false;
        //        return;
        //    }

        //    e.ArgData = ds;
        //}
        //#endregion

        #region Description : 스프레드 타이틀
        private void UP_Spread_Title()
        {
            this.FPS91_TY_S_AC_49GH1894_Sheet1.ColumnHeaderRowCount = 3;
            this.FPS91_TY_S_AC_49GH1894_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_49GH1894_Sheet1.AddColumnHeaderSpanCell(0, 1 + 3, 3, 1);
            this.FPS91_TY_S_AC_49GH1894_Sheet1.AddColumnHeaderSpanCell(0, 2 + 3, 1, 3);
            this.FPS91_TY_S_AC_49GH1894_Sheet1.AddColumnHeaderSpanCell(0, 5 + 3, 1, 4);
            this.FPS91_TY_S_AC_49GH1894_Sheet1.AddColumnHeaderSpanCell(0, 9 + 3, 3, 1); // 18
            this.FPS91_TY_S_AC_49GH1894_Sheet1.AddColumnHeaderSpanCell(0, 10 + 3, 3, 1); // 19
            this.FPS91_TY_S_AC_49GH1894_Sheet1.AddColumnHeaderSpanCell(0, 11 + 3, 3, 1); // 20
            this.FPS91_TY_S_AC_49GH1894_Sheet1.AddColumnHeaderSpanCell(0, 12 + 3, 3, 1); // 21

            this.FPS91_TY_S_AC_49GH1894_Sheet1.AddColumnHeaderSpanCell(1, 2 + 3, 2, 1); //12
            this.FPS91_TY_S_AC_49GH1894_Sheet1.AddColumnHeaderSpanCell(1, 3 + 3, 2, 1); //13
            this.FPS91_TY_S_AC_49GH1894_Sheet1.AddColumnHeaderSpanCell(1, 4 + 3, 2, 1); //14
            this.FPS91_TY_S_AC_49GH1894_Sheet1.AddColumnHeaderSpanCell(1, 5+ 3, 2, 1); //15
            this.FPS91_TY_S_AC_49GH1894_Sheet1.AddColumnHeaderSpanCell(1, 6 + 3, 2, 1); //16

            this.FPS91_TY_S_AC_49GH1894_Sheet1.AddColumnHeaderSpanCell(1, 7 + 3, 1, 2);


            this.FPS91_TY_S_AC_49GH1894_Sheet1.ColumnHeader.Cells[0, 1 + 3].Value = "구  분";
            this.FPS91_TY_S_AC_49GH1894_Sheet1.ColumnHeader.Cells[0, 2 + 3].Value = "전월 미환급 세액의 계산";
            this.FPS91_TY_S_AC_49GH1894_Sheet1.ColumnHeader.Cells[0, 5 + 3].Value = "당월발생 환급세액";
            this.FPS91_TY_S_AC_49GH1894_Sheet1.ColumnHeader.Cells[0, 9 + 3].Value = "18.조정대상환급 세액";
            this.FPS91_TY_S_AC_49GH1894_Sheet1.ColumnHeader.Cells[0, 10 + 3].Value = "19.당월조정 환급세액계";
            this.FPS91_TY_S_AC_49GH1894_Sheet1.ColumnHeader.Cells[0, 11 + 3].Value = "20.차월이월환급세액";
            this.FPS91_TY_S_AC_49GH1894_Sheet1.ColumnHeader.Cells[0, 12 + 3].Value = "21.환급신청액";

            this.FPS91_TY_S_AC_49GH1894_Sheet1.ColumnHeader.Cells[0, 1 + 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_49GH1894_Sheet1.ColumnHeader.Cells[0, 2 + 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_49GH1894_Sheet1.ColumnHeader.Cells[0, 5 + 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_49GH1894_Sheet1.ColumnHeader.Cells[0, 9 + 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_49GH1894_Sheet1.ColumnHeader.Cells[0, 10 + 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_49GH1894_Sheet1.ColumnHeader.Cells[0, 11 + 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_49GH1894_Sheet1.ColumnHeader.Cells[0, 12 + 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;


            this.FPS91_TY_S_AC_49GH1894_Sheet1.ColumnHeader.Cells[1, 2 + 3].Value = "12.전월미환급세액";
            this.FPS91_TY_S_AC_49GH1894_Sheet1.ColumnHeader.Cells[1, 3 + 3].Value = "13.기환급신청세액";
            this.FPS91_TY_S_AC_49GH1894_Sheet1.ColumnHeader.Cells[1, 4 + 3].Value = "14.차감잔액(12-13)";
            this.FPS91_TY_S_AC_49GH1894_Sheet1.ColumnHeader.Cells[1, 5 + 3].Value = "15.일반환급";
            this.FPS91_TY_S_AC_49GH1894_Sheet1.ColumnHeader.Cells[1, 6 + 3].Value = "16.신탁재산(금융기관)";
            this.FPS91_TY_S_AC_49GH1894_Sheet1.ColumnHeader.Cells[1, 7 + 3].Value = "17.그 밖의 환급세액";

            this.FPS91_TY_S_AC_49GH1894_Sheet1.ColumnHeader.Cells[1, 2 + 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_49GH1894_Sheet1.ColumnHeader.Cells[1, 3 + 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_49GH1894_Sheet1.ColumnHeader.Cells[1, 4 + 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_49GH1894_Sheet1.ColumnHeader.Cells[1, 5 + 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_49GH1894_Sheet1.ColumnHeader.Cells[1, 6 + 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_49GH1894_Sheet1.ColumnHeader.Cells[1, 7 + 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;


            this.FPS91_TY_S_AC_49GH1894_Sheet1.ColumnHeader.Cells[2, 7 + 3].Value = "금융회사등";
            this.FPS91_TY_S_AC_49GH1894_Sheet1.ColumnHeader.Cells[2, 8 + 3].Value = "합병등";
            this.FPS91_TY_S_AC_49GH1894_Sheet1.ColumnHeader.Cells[2, 7 + 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_49GH1894_Sheet1.ColumnHeader.Cells[2, 8 + 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

        }
        #endregion

    }
}
