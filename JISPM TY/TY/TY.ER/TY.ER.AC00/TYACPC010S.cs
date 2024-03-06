using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using FarPoint.Win.Spread.CellType;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 차입금 세부내역 관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.07.23 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_27H64059 : EIS 마감 CHECK  확인
    ///  TY_P_AC_27N84241 : EIS 차입금 세부내역 등록
    ///  TY_P_AC_27N84242 : EIS 차입금 세부내역 수정
    ///  TY_P_AC_27N85243 : EIS 차입금 세부내역 삭제
    ///  TY_P_AC_27N8A244 : EIS 차입금 세부내역 순번 가져오기
    ///  TY_P_AC_27N87240 : EIS 차입금 세부내역 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_27N8B245 : EIS 차입금 세부내역 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_AC_243AY315 : 작업이 불가합니다.
    ///  TY_M_AC_26D6A858 : 데이터가 존재합니다.
    ///  TY_M_AC_27H6I062 : EIS 마감 년월이 존재 하지 않습니다.
    ///  TY_M_AC_27H6I063 : EIS 적용 완료상태 입니다. (처리 불가)
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  ELSBKCD : 금융기관
    ///  ELSCDAC : 대출과목
    ///  ELSYYMM : 년월
    /// </summary>
    public partial class TYACPC010S : TYBase
    {
        #region Description : 페이지 로드
        public TYACPC010S()
        {
            InitializeComponent();
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_27N8B245, "ELSCDAC", "CDACNM", "ELSCDAC"); // 스프레드 CODE HELP (대출과목)
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_27N8B245, "ELSBKCD", "BKNM", "ELSBKCD");   // 스프레드 CODE HELP (금융기관)
        }

        private void TYACPC010S_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27N8B245, "ELSYYMM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27N8B245, "ELSCDAC");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27N8B245, "CDACNM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27N8B245, "ELSBKCD");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27N8B245, "BKNM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27N8B245, "ELSSTAT");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27N8B245, "ELSSEQNO");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            UP_Spread_Load();

            SetStartingFocus(this.DTP01_ELSYYMM);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_27N87240",
                this.DTP01_ELSYYMM.GetValue().ToString(),
                this.CBH01_ELSCDAC.GetValue().ToString(),
                this.CBH01_ELSBKCD.GetValue().ToString()
                );

            this.FPS91_TY_S_AC_27N8B245.SetValue(this.DbConnector.ExecuteDataTable());

            UP_SumRowAdd();

            // 마지막 ROW 잠금
            this.FPS91_TY_S_AC_27N8B245.ActiveSheet.Rows[this.FPS91_TY_S_AC_27N8B245.ActiveSheet.Rows.Count - 1].Locked = true;

            this.FPS91_TY_S_AC_27N8B245.Focus();
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.Attach
                    (
                    "TY_P_AC_27N84241",
                    ds.Tables[0].Rows[i]["ELSYYMM"].ToString(),
                    ds.Tables[0].Rows[i]["ELSCDAC"].ToString(),
                    ds.Tables[0].Rows[i]["ELSBKCD"].ToString(),
                    ds.Tables[0].Rows[i]["ELSSTAT"].ToString(),
                    ds.Tables[0].Rows[i]["ELSLNAMT"].ToString(),
                    ds.Tables[0].Rows[i]["ELSSDATE"].ToString(),
                    ds.Tables[0].Rows[i]["ELSEDATE"].ToString(),
                    ds.Tables[0].Rows[i]["ELSIEYUL"].ToString(),
                    ds.Tables[0].Rows[i]["ELSDSTAT"].ToString(),
                    ds.Tables[0].Rows[i]["ELSJJAMT"].ToString(),
                    ds.Tables[0].Rows[i]["ELSDMAMT"].ToString(),
                    ds.Tables[0].Rows[i]["ELSDJAMT"].ToString(),
                    ds.Tables[0].Rows[i]["ELSBIGO"].ToString()
                    );
            }

            this.DbConnector.Attach("TY_P_AC_27N84242", ds.Tables[1]); // 수정

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_27N85243", dt);

            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataSet ds = new DataSet();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_27N8B245.GetDataSourceInclude(TSpread.TActionType.New, "ELSYYMM", "ELSCDAC", "ELSBKCD", "ELSSTAT", "ELSSEQNO", "ELSLNAMT", "ELSSDATE", "ELSEDATE", "ELSIEYUL", "ELSDSTAT", "ELSJJAMT", "ELSDMAMT", "ELSDJAMT", "ELSBIGO"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_27N8B245.GetDataSourceInclude(TSpread.TActionType.Update, "ELSYYMM", "ELSCDAC", "ELSBKCD", "ELSSTAT", "ELSSEQNO", "ELSLNAMT", "ELSSDATE", "ELSEDATE", "ELSIEYUL", "ELSDSTAT", "ELSJJAMT", "ELSDMAMT", "ELSDJAMT", "ELSBIGO"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
                e.Successed = false;
                return;
            }

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // 마감 완료 CHECK 
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_27H64059",
                    ds.Tables[0].Rows[i]["ELSYYMM"].ToString().Substring(0, 4),
                    ds.Tables[0].Rows[i]["ELSYYMM"].ToString().Substring(4, 2)
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count == 0)
                {
                    this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                    e.Successed = false;
                    return;
                }
                else
                {
                    if (dt.Rows[0]["ECGUBUN"].ToString() == "Y")
                    {
                        this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                        e.Successed = false;
                        return;
                    }
                }
            }

            for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                // 마감 완료 CHECK 
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_27H64059",
                    ds.Tables[1].Rows[i]["ELSYYMM"].ToString().Substring(0, 4),
                    ds.Tables[1].Rows[i]["ELSYYMM"].ToString().Substring(4, 2)
                    );

                DataTable dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count == 0)
                {
                    this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                    e.Successed = false;
                    return;
                }
                else
                {
                    if (dt1.Rows[0]["ECGUBUN"].ToString() == "Y")
                    {
                        this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_27N8B245.GetDataSourceInclude(TSpread.TActionType.Remove, "ELSYYMM", "ELSCDAC", "ELSBKCD", "ELSSTAT", "ELSSEQNO");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // 마감 완료 CHECK 
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_27H64059",
                    dt.Rows[i]["ELSYYMM"].ToString().Substring(0, 4),
                    dt.Rows[i]["ELSYYMM"].ToString().Substring(4, 2)
                    );

                DataTable dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count == 0)
                {
                    this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                    e.Successed = false;
                    return;
                }
                else
                {
                    if (dt1.Rows[0]["ECGUBUN"].ToString() == "Y")
                    {
                        this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                        e.Successed = false;
                        return;
                    }
                }
            };


            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region Description : 특정 Row와 Column 값 변경
        private void UP_SumRowAdd()
        {
            int i = 0;

            this.SpreadSumRowAdd(this.FPS91_TY_S_AC_27N8B245, "CDACNM", "합 계", Color.Yellow, "ELSLNAMT", "ELSIEYUL", "ELSJJAMT", "ELSDMAMT", "ELSDJAMT");

            for (i = 0; i < this.FPS91_TY_S_AC_27N8B245.ActiveSheet.RowCount; i++)
            {
                // 당월 잔액
                this.FPS91_TY_S_AC_27N8B245_Sheet1.SetFormula(
                    i,   // row
                    14, // column
                    "R[0]C[-2] - R[0]C[-1]");
            }

            //this.FPS91_TY_S_AC_27N8B245_Sheet1.SetFormula(
            //    FPS91_TY_S_AC_27N8B245_Sheet1.RowCount - 1,
            //    FPS91_TY_S_AC_27N8B245_Sheet1.ColumnCount - 1,
            //    "R[0]C[-4] - R[0]C[-3] - R[0]C[-2] - R[0]C[-1]"); //잔액 구하기

            //this.FPS91_TY_S_AC_27N8B245.ActiveSheet.Rows[this.FPS91_TY_S_AC_27N8B245.ActiveSheet.Rows.Count - 1].Visible = false;
        }
        #endregion

        #region Description : 스프레드 로드
        private void UP_Spread_Load()
        {
            this.FPS91_TY_S_AC_27N8B245_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_27N8B245_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_27N8B245_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);  // 년월
            
            this.FPS91_TY_S_AC_27N8B245_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);  // 대출과목
            this.FPS91_TY_S_AC_27N8B245_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);  // 대출과목명

            this.FPS91_TY_S_AC_27N8B245_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);  // 금융기관
            this.FPS91_TY_S_AC_27N8B245_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1);  // 금융기관명

            this.FPS91_TY_S_AC_27N8B245_Sheet1.AddColumnHeaderSpanCell(0, 5, 2, 1);  // 상태

            this.FPS91_TY_S_AC_27N8B245_Sheet1.AddColumnHeaderSpanCell(0, 6, 2, 1);  // 순번

            this.FPS91_TY_S_AC_27N8B245_Sheet1.AddColumnHeaderSpanCell(0, 7, 1, 5);  // 최초차입내역
            this.FPS91_TY_S_AC_27N8B245_Sheet1.AddColumnHeaderSpanCell(0, 13, 1, 2); // 당월잔액
            this.FPS91_TY_S_AC_27N8B245_Sheet1.AddColumnHeaderSpanCell(0, 15, 2, 1); // 비고

            this.FPS91_TY_S_AC_27N8B245_Sheet1.ColumnHeader.Cells[0, 0].Value  = "년월";

            this.FPS91_TY_S_AC_27N8B245_Sheet1.ColumnHeader.Cells[0, 1].Value  = "대출과목";
            this.FPS91_TY_S_AC_27N8B245_Sheet1.ColumnHeader.Cells[0, 2].Value  = "대출과목명";

            this.FPS91_TY_S_AC_27N8B245_Sheet1.ColumnHeader.Cells[0, 3].Value  = "금융기관";
            this.FPS91_TY_S_AC_27N8B245_Sheet1.ColumnHeader.Cells[0, 4].Value  = "금융기관명";

            this.FPS91_TY_S_AC_27N8B245_Sheet1.ColumnHeader.Cells[0, 5].Value  = "상태";

            this.FPS91_TY_S_AC_27N8B245_Sheet1.ColumnHeader.Cells[0, 6].Value  = "순번";

            this.FPS91_TY_S_AC_27N8B245_Sheet1.ColumnHeader.Cells[0, 7].Value  = "최초차입내역";
            this.FPS91_TY_S_AC_27N8B245_Sheet1.ColumnHeader.Cells[0, 13].Value = "당월";
            this.FPS91_TY_S_AC_27N8B245_Sheet1.ColumnHeader.Cells[0, 15].Value = "비고";
            
            this.FPS91_TY_S_AC_27N8B245_Sheet1.ColumnHeader.Cells[1, 0].Value  = "";

            this.FPS91_TY_S_AC_27N8B245_Sheet1.ColumnHeader.Cells[1, 1].Value  = "";
            this.FPS91_TY_S_AC_27N8B245_Sheet1.ColumnHeader.Cells[1, 2].Value  = "";

            this.FPS91_TY_S_AC_27N8B245_Sheet1.ColumnHeader.Cells[1, 3].Value  = "";
            this.FPS91_TY_S_AC_27N8B245_Sheet1.ColumnHeader.Cells[1, 4].Value  = "";

            this.FPS91_TY_S_AC_27N8B245_Sheet1.ColumnHeader.Cells[1, 5].Value  = "";
            this.FPS91_TY_S_AC_27N8B245_Sheet1.ColumnHeader.Cells[1, 6].Value  = "";

            this.FPS91_TY_S_AC_27N8B245_Sheet1.ColumnHeader.Cells[1, 7].Value  = "원화";
            this.FPS91_TY_S_AC_27N8B245_Sheet1.ColumnHeader.Cells[1, 8].Value  = "차입일";
            this.FPS91_TY_S_AC_27N8B245_Sheet1.ColumnHeader.Cells[1, 9].Value  = "만기일";
            this.FPS91_TY_S_AC_27N8B245_Sheet1.ColumnHeader.Cells[1, 10].Value = "이율";
            this.FPS91_TY_S_AC_27N8B245_Sheet1.ColumnHeader.Cells[1, 11].Value = "상태";
            this.FPS91_TY_S_AC_27N8B245_Sheet1.ColumnHeader.Cells[1, 12].Value = "잔액";
            this.FPS91_TY_S_AC_27N8B245_Sheet1.ColumnHeader.Cells[1, 13].Value = "당기증(감)";
            this.FPS91_TY_S_AC_27N8B245_Sheet1.ColumnHeader.Cells[1, 14].Value = "잔액";

            if (this.FPS91_TY_S_AC_27N8B245_Sheet1.AlternatingRows.Count > 0)
                this.FPS91_TY_S_AC_27N8B245_Sheet1.AlternatingRows[0].BackColor = Color.White;

        }
        #endregion

        private void SpreadSumRowAdd(TYSpread spread, string sumStringColumnName, string sumString, System.Drawing.Color sumRowColor, params string[] sumColumns)
        {
            List<string> listSumColumns = new List<string>(sumColumns);
            spread.Sheets[0].AddRows(spread.Sheets[0].Rows.Count, 1);
            spread.Sheets[0].ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            spread.Sheets[0].AutoCalculation = true;
            spread.Sheets[0].Rows[spread.Sheets[0].Rows.Count - 1].BackColor = sumRowColor;
            for (int i = 0; i < spread.Sheets[0].Columns.Count; i++)
            {
                if (spread.Sheets[0].Columns[i].DataField == sumStringColumnName)
                    spread.Sheets[0].SetValue(spread.Sheets[0].Rows.Count - 1, i, sumString);
                else if (listSumColumns.IndexOf(spread.Sheets[0].Columns[i].DataField) > -1)
                {
                    if (spread.Sheets[0].Rows.Count == 1)
                        spread.Sheets[0].Cells[0, i].Value = 0;
                    else
                        if (spread.Sheets[0].Columns[i].DataField != "ELSIEYUL")
                        {
                            spread.Sheets[0].SetFormula(spread.Sheets[0].Rows.Count - 1, i, string.Format("SUM(R1C[0]:R{0}C[0])", spread.Sheets[0].Rows.Count - 1));
                        }
                        else
                        {
                            spread.Sheets[0].SetFormula(spread.Sheets[0].Rows.Count - 1, i, string.Format("AVERAGE(R1C[0]:R{0}C[0])", spread.Sheets[0].Rows.Count - 1));
                        }
                }
            }
        }
    }
}
