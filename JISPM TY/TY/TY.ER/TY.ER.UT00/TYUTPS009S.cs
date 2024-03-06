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
    public partial class TYUTPS009S : TYBase
    {
        #region Description : 페이지 로드
        public TYUTPS009S()
        {
            InitializeComponent();
        }

        private void TYUTPS009S_Load(object sender, System.EventArgs e)
        {
            (this.FPS91_TY_S_UT_B3P9Y054.Sheets[0].Columns[1].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.create;
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_UT_B3P9Y054, "BTN");

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
            this.FPS91_TY_S_UT_B3P9Y054.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_B3P9X053", Get_Date(this.DTP01_STYYMM.GetValue().ToString()).Substring(0, 6),
                                                        Get_Date(this.DTP01_EDYYMM.GetValue().ToString()).Substring(0, 6));

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_B3P9Y054.SetValue(dt);
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
            if ((new TYUTPS009I(string.Empty, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
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
                        this.DbConnector.Attach("TY_P_UT_B3PG2059", dt.Rows[i]["SVREDATE"].ToString(),
                                                                    dt.Rows[i]["SVCODE"].ToString());
                    }
                    this.DbConnector.ExecuteTranQueryList();

                    // 내용이 없으면 수정사항관리 내용도 삭제 함
                    DataTable dt1 = new DataTable();

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_B4KEP240", Get_Date(this.DTP01_SVREDATE.GetValue().ToString()));

                    dt1 = this.DbConnector.ExecuteDataTable();

                    if (dt1.Rows.Count <= 0)
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_B4KEP241", Get_Date(this.DTP01_SVTREDATE.GetValue().ToString()));

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
            DataTable dt = this.FPS91_TY_S_UT_B3PAS057.GetDataSourceInclude(TSpread.TActionType.Remove, "SVREDATE", "SVCODE");

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
                    string sSVTNUM = string.Empty;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        // 순번 가져오기
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_B4KEM236", Get_Date(this.DTP01_SVTREDATE.GetValue().ToString().Trim()));

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            sSVTNUM = dt.Rows[0]["SVTNUM"].ToString();
                        }

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_B4KEN237", Get_Date(this.DTP01_SVTREDATE.GetValue().ToString().Trim()),
                                                                    sSVTNUM.ToString(),
                                                                    ds.Tables[0].Rows[i]["SVTDESC"].ToString(),
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
                        this.DbConnector.Attach("TY_P_UT_B4KEN238", ds.Tables[1].Rows[i]["SVTDESC"].ToString(),
                                                                    TYUserInfo.EmpNo,
                                                                    Get_Date(this.DTP01_SVTREDATE.GetValue().ToString().Trim()),
                                                                    ds.Tables[1].Rows[i]["SVTNUM"].ToString()
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
                        this.DbConnector.Attach("TY_P_UT_B4KEO239", Get_Date(this.DTP01_SVTREDATE.GetValue().ToString().Trim()),
                                                                    ds.Tables[2].Rows[i]["SVTNUM"].ToString()
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

            ds.Tables.Add(this.FPS91_TY_S_UT_B4KEQ243.GetDataSourceInclude(TSpread.TActionType.New,    "SVTNUM", "SVTDESC"));
            ds.Tables.Add(this.FPS91_TY_S_UT_B4KEQ243.GetDataSourceInclude(TSpread.TActionType.Update, "SVTNUM", "SVTDESC"));
            ds.Tables.Add(this.FPS91_TY_S_UT_B4KEQ243.GetDataSourceInclude(TSpread.TActionType.Remove, "SVTNUM"));

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
                sProcedure = "TY_P_UT_B3PAM055";

                if (this.CBO01_GGUBUN.GetValue().ToString() == "C")
                {
                    sGUBUN = "C";
                }
            }
            else
            {
                sProcedure = "TY_P_UT_B3PAN056";

                sGUBUN = "C";
            }
            

            this.FPS91_TY_S_UT_B3PAS057.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach(sProcedure, sGUBUN.ToString(),
                                                Get_Date(this.DTP01_SVREDATE.GetValue().ToString()),
                                                this.CBH01_SVCODE.GetValue().ToString().Trim());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_B3PAS057.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_UT_B3PAS057.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_UT_B3PAS057.GetValue(i, "SVHIGB").ToString() == "C")
                {
                    this.FPS91_TY_S_UT_B3PAS057.ActiveSheet.Rows[i].ForeColor = Color.Red;
                }
            }
        }
        #endregion

        #region Description : 수정사항 관리 조회 메소드
        private void UP_Master_Select()
        {
            this.FPS91_TY_S_UT_B4KEQ243.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_B4KEQ242", Get_Date(this.DTP01_SVTREDATE.GetValue().ToString()));

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_B4KEQ243.SetValue(dt);
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_UT_B3P9Y054_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DTP01_SVREDATE.SetValue(Set_Date(this.FPS91_TY_S_UT_B3P9Y054.GetValue("SVREDATE").ToString()));

            this.DTP01_SVTREDATE.SetValue(Set_Date(this.FPS91_TY_S_UT_B3P9Y054.GetValue("SVREDATE").ToString()));

            UP_Spread_Select();

            UP_Master_Select();
        }

        private void FPS91_TY_S_UT_B3P9Y054_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "1")
            {
                if ((new TYUTPS009B(this.FPS91_TY_S_UT_B3P9Y054.GetValue("SVREDATE").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.BTN61_INQ_Click(null, null);
                }
            }
        }
        #endregion

        #region Description : 상세 스프레드 이벤트
        private void FPS91_TY_S_UT_B3PAS057_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYUTPS009I(this.FPS91_TY_S_UT_B3PAS057.GetValue("SVREDATE").ToString(),
                                this.FPS91_TY_S_UT_B3PAS057.GetValue("SVCODE").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BTN61_INQ_FXM_Click(null, null);

                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region Description : 스프레드 타이틀 변경
        private void UP_Spread_Load()
        {
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_UT_B3PAS057_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1);
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.AddColumnHeaderSpanCell(0, 5, 2, 1);
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.AddColumnHeaderSpanCell(0, 6, 2, 1);



            this.FPS91_TY_S_UT_B3PAS057_Sheet1.AddColumnHeaderSpanCell(0, 7, 1, 2);

            this.FPS91_TY_S_UT_B3PAS057_Sheet1.AddColumnHeaderSpanCell(0, 9, 1, 4);

            this.FPS91_TY_S_UT_B3PAS057_Sheet1.AddColumnHeaderSpanCell(0, 13, 1, 5);

            this.FPS91_TY_S_UT_B3PAS057_Sheet1.AddColumnHeaderSpanCell(0, 18, 2, 1);
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.AddColumnHeaderSpanCell(0, 19, 2, 1);
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.AddColumnHeaderSpanCell(0, 20, 2, 1);
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.AddColumnHeaderSpanCell(0, 21, 2, 1);

            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[0, 7].Value  = "노줄크기";
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[0, 9].Value  = "보호기기압력";
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[0, 13].Value = "안전밸브";

            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[1, 7].Value  = "입구";
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[1, 8].Value  = "출구";

            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[1, 10].Value = "기기번호";

            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[1, 11].Value = "운전";
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[1, 12].Value = "설계";

            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[1, 13].Value = "설정";
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[1, 15].Value = "몸체";
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[1, 17].Value = "TRIM";

            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[0, 2].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[0, 3].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[0, 8].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[0, 9].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[0, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[0, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[0, 12].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[0, 13].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[0, 14].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[0, 15].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[0, 16].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[0, 17].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[0, 18].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[0, 19].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[0, 20].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[0, 21].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[1, 7].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[1, 8].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[1, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[1, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[1, 12].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[1, 13].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[1, 15].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_B3PAS057_Sheet1.ColumnHeader.Cells[1, 17].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion
    }
}
