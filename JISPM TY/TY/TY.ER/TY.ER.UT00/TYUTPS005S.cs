using System;
using System.Data;
using System.Drawing;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using TY.ER.GB00;

namespace TY.ER.UT00
{
    /// <summary>
    /// 질소사용료 등록 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.07.06 15:00
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_676EI582 : 질소사용료 조회
    ///  TY_P_UT_676EL583 : 질소사용료 등록
    ///  TY_P_UT_676EN584 : 질소사용료 수정
    ///  TY_P_UT_676EP585 : 질소사용료 삭제
    ///  TY_P_UT_676EQ587 : 질소사용료 확인
    ///  TY_P_UT_676ER589 : 단가등록 마스타 조회(질소사용료 관리)
    ///  TY_P_UT_676ES591 : 질소 금액 조회(질소사용료 관리)
    ///  TY_P_UT_676EU593 : 년월 총금액 조회(질소사용료 관리)
    ///  TY_P_UT_676EV594 : TYC 화주 존재 확인(질소사용료 관리)
    ///  TY_P_UT_676EV595 : 탱크요율 조회(질소사용료 관리)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_676G2596 : 질소사용료 등록
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    ///  TY_M_GB_43C9G671 : 삭제 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  JLHWAJU : 화주
    ///  JLHWAMUL : 화물
    ///  JLYYMM : 작업년월
    ///  JLAMT : 금액
    ///  JLAMTTOT : 총 금액
    ///  JLQTYTOT : 총 사용량
    ///  JLTANK : 탱크번호
    /// </summary>
    public partial class TYUTPS005S : TYBase
    {
        #region Description : 페이지 로드
        public TYUTPS005S()
        {
            InitializeComponent();
        }

        private void TYUTPS005S_Load(object sender, System.EventArgs e)
        {
            (this.FPS91_TY_S_UT_B3AFC871.Sheets[0].Columns[26].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.application_view_detail;
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_UT_B3AFC871, "BTN");

            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            UP_Spread_Load();

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.CBH01_DICODE.CodeText);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sUSE    = string.Empty;
            string sMODIFY = string.Empty;

            if (this.CBO01_GGUBUN.GetValue().ToString() == "A")
            {
                sUSE    = "";
                sMODIFY = "";
            }
            else if (this.CBO01_GGUBUN.GetValue().ToString() == "U")
            {
                sUSE    = "Y";
                sMODIFY = "";
            }
            else if (this.CBO01_GGUBUN.GetValue().ToString() == "C")
            {
                sUSE    = "";
                sMODIFY = "C";
            }

            this.FPS91_TY_S_UT_B3AFC871.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_B3AEN864", this.CBH01_DICODE.GetValue().ToString(), sUSE.ToString(), sMODIFY.ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_B3AFC871.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_UT_B3AFC871.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_UT_B3AFC871.GetValue(i, "DIUSEGUBN").ToString() == "Y")
                {
                    this.FPS91_TY_S_UT_B3AFC871.ActiveSheet.Rows[i].ForeColor = Color.Blue;
                }

                if (this.FPS91_TY_S_UT_B3AFC871.GetValue(i, "DIHIGB").ToString() == "C")
                {
                    this.FPS91_TY_S_UT_B3AFC871.ActiveSheet.Rows[i].ForeColor = Color.Red;
                }
            }
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYUTPS005I(string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

                if (dt.Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.DbConnector.Attach("TY_P_UT_B3B9L882", dt.Rows[i]["DICODE"].ToString());
                    }
                    this.DbConnector.ExecuteTranQueryList();

                    this.BTN61_INQ_Click(null, null);

                    this.ShowMessage("TY_M_GB_23NAD874");
                }
            }
            catch
            {
                this.ShowMessage("TY_M_GB_43C9G671");
            }
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_UT_B3AFC871.GetDataSourceInclude(TSpread.TActionType.Remove, "DICODE");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            DataTable dt1 = new DataTable();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_B3B9J880", dt.Rows[i]["DICODE"].ToString());

                dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_B3B9K881");
                    e.Successed = false;
                    return;
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_B3PG4060", dt.Rows[i]["DICODE"].ToString());

                dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count > 0)
                {
                    this.ShowCustomMessage("안전밸브 및 안전판 사양에 보호기기 자료가 존재하므로 작업이 불가합니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
            e.ArgData = dt;
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_UT_B3AFC871_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYUTPS005I(this.FPS91_TY_S_UT_B3AFC871.GetValue("DICODE").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BTN61_INQ_Click(null, null);
            }
        }

        private void FPS91_TY_S_UT_B3AFC871_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "26")
            {
                if (this.FPS91_TY_S_UT_B3AFC871.GetValue("DITANKGUBN").ToString() == "Y" && this.FPS91_TY_S_UT_B3AFC871.GetValue("DIUSEGUBN").ToString() == "Y")
                {
                    if ((new TYUTPS006I(this.FPS91_TY_S_UT_B3AFC871.GetValue("DICODE").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        this.BTN61_INQ_Click(null, null);
                    }
                }
            }
        }
        #endregion

        #region Description : 스프레드 타이틀 변경
        private void UP_Spread_Load()
        {
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_UT_B3AFC871_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);

            this.FPS91_TY_S_UT_B3AFC871_Sheet1.AddColumnHeaderSpanCell(0, 3, 1, 3);

            this.FPS91_TY_S_UT_B3AFC871_Sheet1.AddColumnHeaderSpanCell(0, 6, 1, 2);

            this.FPS91_TY_S_UT_B3AFC871_Sheet1.AddColumnHeaderSpanCell(0, 8, 1, 2);

            this.FPS91_TY_S_UT_B3AFC871_Sheet1.AddColumnHeaderSpanCell(0, 10, 1, 3);

            this.FPS91_TY_S_UT_B3AFC871_Sheet1.AddColumnHeaderSpanCell(0, 13, 2, 1);

            this.FPS91_TY_S_UT_B3AFC871_Sheet1.AddColumnHeaderSpanCell(0, 15, 1, 4);

            this.FPS91_TY_S_UT_B3AFC871_Sheet1.AddColumnHeaderSpanCell(0, 19, 1, 4);

            this.FPS91_TY_S_UT_B3AFC871_Sheet1.AddColumnHeaderSpanCell(0, 23, 2, 1);
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.AddColumnHeaderSpanCell(0, 24, 2, 1);
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.AddColumnHeaderSpanCell(0, 25, 2, 1);
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.AddColumnHeaderSpanCell(0, 26, 2, 1);

            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 0].Value = "장치번호";
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 1].Value = "장치번호";

            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 2].Value = "내용물";

            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 3].Value = "용량";

            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 6].Value = "압력[MPa]";

            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 8].Value = "온도[℃]";

            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 10].Value = "사용재질";
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 13].Value = "용접효율%";
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 14].Value = "계산두께[mm]";
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 15].Value = "부식여유[mm]";
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 19].Value = "사용두께[mm]";

            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 23].Value = "후열처리여부";
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 24].Value = "비파괴검사[%]";
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 25].Value = "비고";


            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 3].Value = "용량KL";
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 4].Value = "직경M";
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 5].Value = "높이M";
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 6].Value = "운전";
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 7].Value = "설계";
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 8].Value = "운전";
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 9].Value = "설계";

            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 10].Value = "본체";
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 11].Value = "부속품";
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 12].Value = "가스켓";

            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 14].Value = "S";

            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 15].Value = "A";
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 16].Value = "B";
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 17].Value = "S";
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 18].Value = "R";
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 19].Value = "A";
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 20].Value = "B";
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 21].Value = "S";
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 22].Value = "R";


            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 2].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 3].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 8].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 9].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 12].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 13].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 14].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 15].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 16].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 17].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 18].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 19].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 20].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 21].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 22].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 23].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 24].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[0, 25].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 3].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 4].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 5].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 6].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 7].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 8].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 9].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 12].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 13].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 14].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 15].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 16].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 17].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 18].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 19].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 20].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 21].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3AFC871_Sheet1.ColumnHeader.Cells[1, 22].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion
    }
}
