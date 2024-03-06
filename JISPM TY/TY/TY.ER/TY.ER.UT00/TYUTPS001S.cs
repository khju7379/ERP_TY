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
    public partial class TYUTPS001S : TYBase
    {
        #region Description : 페이지 로드
        public TYUTPS001S()
        {
            InitializeComponent();
        }

        private void TYUTPS001S_Load(object sender, System.EventArgs e)
        {
            (this.FPS91_TY_S_UT_B2HDA600.Sheets[0].Columns[1].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.create;
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_UT_B2HDA600, "BTN");

            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_STYYMM.SetValue(System.DateTime.Now.ToString("yyyy-MM"));
            this.DTP01_EDYYMM.SetValue(System.DateTime.Now.ToString("yyyy-MM"));

            UP_Spread_Load();

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.DTP01_STYYMM);
        }
        #endregion

        #region Description : 개정일자 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_B2HDA600.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_B2HD3599", Get_Date(this.DTP01_STYYMM.GetValue().ToString()).Substring(0, 6), Get_Date(this.DTP01_EDYYMM.GetValue().ToString()).Substring(0, 6));

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_B2HDA600.SetValue(dt);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_FXM_Click(object sender, EventArgs e)
        {
            UP_Spread_Select();
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYUTPS001I(string.Empty, string.Empty, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BTN61_INQ_FXM_Click(null, null);

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
                        this.DbConnector.Attach("TY_P_UT_B2I9C652", dt.Rows[i]["RMREDATE"].ToString(),
                                                                    dt.Rows[i]["RMHMCODE"].ToString(),
                                                                    dt.Rows[i]["RMHWAJU"].ToString());
                    }
                    this.DbConnector.ExecuteTranQueryList();

                    // 내용이 없으면 수정사항관리 내용도 삭제 함
                    DataTable dt1 = new DataTable();

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_B4KE4234", Get_Date(this.DTP01_RMREDATE.GetValue().ToString()));

                    dt1 = this.DbConnector.ExecuteDataTable();

                    if (dt1.Rows.Count <= 0)
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_B4KE5235", Get_Date(this.DTP01_RMTREDATE.GetValue().ToString()));

                        this.DbConnector.ExecuteTranQueryList();
                    }

                    this.BTN61_INQ_FXM_Click(null, null);

                    UP_Master_Select();

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
            DataTable dt = this.FPS91_TY_S_UT_B2HEE613.GetDataSourceInclude(TSpread.TActionType.Remove, "RMREDATE", "RMHMCODE", "RMHWAJU");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
            e.ArgData = dt;
        }
        #endregion

        #region Description : 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            try
            {
                DataTable dt = new DataTable();

                // 등록
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string sRMTNUM = string.Empty;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        // 순번 가져오기
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_B4KBS214", Get_Date(this.DTP01_RMTREDATE.GetValue().ToString().Trim()));

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            sRMTNUM = dt.Rows[0]["RMTNUM"].ToString();
                        }

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_B4KBT215", Get_Date(this.DTP01_RMTREDATE.GetValue().ToString().Trim()),
                                                                    sRMTNUM.ToString(),
                                                                    ds.Tables[0].Rows[i]["RMTDESC"].ToString(),
                                                                    TYUserInfo.EmpNo
                                                                    );
                        this.DbConnector.ExecuteTranQuery();
                    }
                }

                // 수정
                if (ds.Tables[1].Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();

                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        this.DbConnector.Attach("TY_P_UT_B4KBV216", ds.Tables[1].Rows[i]["RMTDESC"].ToString(),
                                                                    TYUserInfo.EmpNo,
                                                                    Get_Date(this.DTP01_RMTREDATE.GetValue().ToString().Trim()),
                                                                    ds.Tables[1].Rows[i]["RMTNUM"].ToString()
                                                                    );
                    }

                    this.DbConnector.ExecuteTranQueryList();
                }

                // 삭제
                if (ds.Tables[2].Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();

                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                    {
                        this.DbConnector.Attach("TY_P_UT_B4KBW217", Get_Date(this.DTP01_RMTREDATE.GetValue().ToString().Trim()),
                                                                    ds.Tables[2].Rows[i]["RMTNUM"].ToString()
                                                                    );
                    }

                    this.DbConnector.ExecuteTranQueryList();
                }

                this.ShowMessage("TY_M_MR_2BF50354");

                // 수정사항 관리 조회 메소드
                UP_Master_Select();
            }
            catch (Exception ex)
            {
                this.ShowMessage("TY_M_UT_71BDP399");
            }
        }
        #endregion

        #region Description : 처리 체크
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_UT_B4KBM213.GetDataSourceInclude(TSpread.TActionType.New, "RMTNUM", "RMTDESC"));
            ds.Tables.Add(this.FPS91_TY_S_UT_B4KBM213.GetDataSourceInclude(TSpread.TActionType.Update, "RMTNUM", "RMTDESC"));
            ds.Tables.Add(this.FPS91_TY_S_UT_B4KBM213.GetDataSourceInclude(TSpread.TActionType.Remove, "RMTNUM"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2CV43442");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_MR_2BF50353"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 조회 메소드
        private void UP_Spread_Select()
        {
            UP_Spread_Load();

            string sProcedure = string.Empty;
            string sGUBUN = string.Empty;

            if (this.CBO01_GGUBUN.GetValue().ToString() == "A" || this.CBO01_GGUBUN.GetValue().ToString() == "C")
            {
                sProcedure = "TY_P_UT_B2HDT611";

                if (this.CBO01_GGUBUN.GetValue().ToString() == "C")
                {
                    sGUBUN = "C";
                }
            }
            else
            {
                sProcedure = "TY_P_UT_B2HDW612";

                sGUBUN = "C";
            }
            

            this.FPS91_TY_S_UT_B2HEE613.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach(sProcedure, sGUBUN.ToString(),
                                                Get_Date(this.DTP01_RMREDATE.GetValue().ToString()),
                                                this.CBH01_RMHMCODE.GetValue().ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_B2HEE613.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_UT_B2HEE613.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_UT_B2HEE613.GetValue(i, "RMHIGB").ToString() == "C")
                {
                    this.FPS91_TY_S_UT_B2HEE613.ActiveSheet.Rows[i].ForeColor = Color.Red;
                }
            }
        }
        #endregion

        #region Description : 수정사항 관리 조회 메소드
        private void UP_Master_Select()
        {
            this.FPS91_TY_S_UT_B4KBM213.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_B4KBM212", Get_Date(this.DTP01_RMREDATE.GetValue().ToString()));

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_B4KBM213.SetValue(dt);
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_UT_B2HDA600_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DTP01_RMREDATE.SetValue(Set_Date(this.FPS91_TY_S_UT_B2HDA600.GetValue("RMREDATE").ToString()));

            this.DTP01_RMTREDATE.SetValue(Set_Date(this.FPS91_TY_S_UT_B2HDA600.GetValue("RMREDATE").ToString()));

            UP_Spread_Select();

            UP_Master_Select();
        }

        private void FPS91_TY_S_UT_B2HDA600_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "1")
            {
                if ((new TYUTPS001B(this.FPS91_TY_S_UT_B2HDA600.GetValue("RMREDATE").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.BTN61_INQ_Click(null, null);
                }
            }
        }
        #endregion

        #region Description : 상세 스프레드 이벤트
        private void FPS91_TY_S_UT_B2HEE613_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYUTPS001I(this.FPS91_TY_S_UT_B2HEE613.GetValue("RMREDATE").ToString(), this.FPS91_TY_S_UT_B2HEE613.GetValue("RMHMCODE").ToString(), this.FPS91_TY_S_UT_B2HEE613.GetValue("RMHWAJU").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BTN61_INQ_FXM_Click(null, null);

                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region Description : 스프레드 타이틀 변경
        private void UP_Spread_Load()
        {
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_UT_B2HEE613_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1);
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.AddColumnHeaderSpanCell(0, 5, 1, 2);

            this.FPS91_TY_S_UT_B2HEE613_Sheet1.AddColumnHeaderSpanCell(0, 7, 2, 1);
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.AddColumnHeaderSpanCell(0, 8, 1, 3);

            this.FPS91_TY_S_UT_B2HEE613_Sheet1.AddColumnHeaderSpanCell(0, 9, 2, 1);
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.AddColumnHeaderSpanCell(0, 10, 2, 1);
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.AddColumnHeaderSpanCell(0, 11, 2, 1);
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.AddColumnHeaderSpanCell(0, 12, 2, 1);
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.AddColumnHeaderSpanCell(0, 13, 2, 1);
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.AddColumnHeaderSpanCell(0, 14, 2, 1);
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.AddColumnHeaderSpanCell(0, 15, 2, 1);
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.AddColumnHeaderSpanCell(0, 16, 2, 1);
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.AddColumnHeaderSpanCell(0, 17, 2, 1);
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.AddColumnHeaderSpanCell(0, 18, 2, 1);
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.AddColumnHeaderSpanCell(0, 19, 2, 1);
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.AddColumnHeaderSpanCell(0, 20, 2, 1);
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.AddColumnHeaderSpanCell(0, 21, 2, 1);
            

            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[0, 5].Value = "폭발한계(%)";
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[0, 8].Value = "독성치";

            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[1, 5].Value = "하한";
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[1, 6].Value = "상한";

            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[1, 8].Value  = "경구";
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[1, 9].Value  = "경피";
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[1, 10].Value = "흡입";

            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[0, 2].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[0, 3].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[0, 8].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[0, 9].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[0, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[0, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[0, 12].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[0, 13].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[0, 14].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[0, 15].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[0, 16].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[0, 17].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[0, 18].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[0, 19].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[0, 20].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[0, 21].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[1, 5].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[1, 6].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[1, 8].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[1, 9].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B2HEE613_Sheet1.ColumnHeader.Cells[1, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion
    }
}
