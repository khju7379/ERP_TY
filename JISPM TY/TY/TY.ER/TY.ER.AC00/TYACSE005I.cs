using System;
using System.Data;
using System.Drawing;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.AC00
{
    public partial class TYACSE005I : TYBase
    {
        private string fsTabIndex = string.Empty;

        #region Description : 페이지 로드
        public TYACSE005I()
        {
            InitializeComponent();

            // 스프레드에서 코드헬프 사용
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_43I52775, "ASCUSTCD",  "VNSANGHO",    "ASCUSTCD");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_43I52775, "ASCONCDDP", "ASCONCDDPNM", "ASCONCDDP");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_43J5P827, "AHUSECDDP", "AHUSECDDPNM", "AHUSECDDP");
        }

        private void TYACSE005I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_43I52775, "ASMEMGUBN");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_43I52775, "ASMEMYEAR");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_43I52775, "ASMEMSEQ");

            this.BTN61_INQ.ProcessCheck += new TButton.CheckHandler(BTN61_INQ_ProcessCheck);

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.BTN62_SAV.ProcessCheck += new TButton.CheckHandler(BTN62_SAV_ProcessCheck);
            this.BTN62_REM.ProcessCheck += new TButton.CheckHandler(BTN62_REM_ProcessCheck);

            this.BTN63_INQ.ProcessCheck += new TButton.CheckHandler(BTN63_INQ_ProcessCheck);

            this.BTN63_SAV.ProcessCheck += new TButton.CheckHandler(BTN63_SAV_ProcessCheck);
            this.BTN63_REM.ProcessCheck += new TButton.CheckHandler(BTN63_REM_ProcessCheck);

            UP_Spread_Title();

            UP_SetReadOnly(false);

            // Blank Row 생성
            //UP_Row_Ins();

            fsTabIndex = "1";

            UP_Items_Visible("1");

            SetStartingFocus(this.CBO01_GGUBUN);
        }
        #endregion

        #region Description : 빈 ROW 생성
        private void UP_Row_Ins()
        {
            DataTable dt = new DataTable();
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_43I58774",
                "99999999",
                "99999999",
                this.CBO01_GGUBUN.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();
            this.FPS91_TY_S_AC_43I52775.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                int i = 0;

                this.SpreadSumRowAdd(this.FPS91_TY_S_AC_43I52775, "ASCUSTCD", "합 계", Color.Yellow);
                for (i = 0; i < this.FPS91_TY_S_AC_43I52775.ActiveSheet.RowCount; i++)
                {
                    this.FPS91_TY_S_AC_43I52775_Sheet1.SetFormula(
                        i,  // row
                        11, // column
                        "R[0]C[-2] - R[0]C[-1]"); // 잔액
                }

                this.FPS91_TY_S_AC_43I52775.ActiveSheet.Rows[i - 1].Visible = false;
            }
        }
        #endregion

        #region Description : 버튼 Visible 이벤트
        private void UP_Items_Visible(string sGUBUN)
        {
            if (sGUBUN == "1")
            {
                this.BTN61_INQ.Visible = true;
                this.BTN61_SAV.Visible = true;
                this.BTN61_REM.Visible = true;

                this.BTN63_INQ.Visible = false;
                this.BTN63_SAV.Visible = false;
                this.BTN63_REM.Visible = false;
            }
            else
            {
                this.BTN61_INQ.Visible = false;
                this.BTN61_SAV.Visible = false;
                this.BTN61_REM.Visible = false;

                this.BTN63_INQ.Visible = true;
                this.BTN63_SAV.Visible = true;
                this.BTN63_REM.Visible = true;
            }
        }
        #endregion

        #region Description : 스프레드 타이틀
        private void UP_Spread_Title()
        {
            #region Description : 탭1 - 회원권 MAST

            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_43I52775_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_43I52775_Sheet1.AddColumnHeaderSpanCell(0, 0,  1, 3);
            this.FPS91_TY_S_AC_43I52775_Sheet1.AddColumnHeaderSpanCell(0, 9,  1, 3);
            this.FPS91_TY_S_AC_43I52775_Sheet1.AddColumnHeaderSpanCell(0, 12, 1, 2);

            this.FPS91_TY_S_AC_43I52775_Sheet1.AddColumnHeaderSpanCell(0, 3,  2, 1);
            this.FPS91_TY_S_AC_43I52775_Sheet1.AddColumnHeaderSpanCell(0, 4,  2, 1);
            this.FPS91_TY_S_AC_43I52775_Sheet1.AddColumnHeaderSpanCell(0, 5,  2, 1);
            this.FPS91_TY_S_AC_43I52775_Sheet1.AddColumnHeaderSpanCell(0, 6,  2, 1);
            this.FPS91_TY_S_AC_43I52775_Sheet1.AddColumnHeaderSpanCell(0, 7,  2, 1);
            this.FPS91_TY_S_AC_43I52775_Sheet1.AddColumnHeaderSpanCell(0, 8,  2, 1);
            this.FPS91_TY_S_AC_43I52775_Sheet1.AddColumnHeaderSpanCell(0, 14, 2, 1);
            this.FPS91_TY_S_AC_43I52775_Sheet1.AddColumnHeaderSpanCell(0, 15, 2, 1);

            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[0, 0].Value  = "관리번호";
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[0, 3].Value  = "거 래 처";
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[0, 4].Value  = "거래처명";
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[0, 5].Value  = "취득일자";
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[0, 6].Value  = "취득내용";
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[0, 7].Value  = "귀속부서";
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[0, 8].Value  = "귀속부서명";
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[0, 9].Value  = "금    액";
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[0, 12].Value = "구좌당 회원수";            
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[0, 14].Value = "보유구좌";
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[0, 15].Value = "비    고";

            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[1, 0].Value  = "회원권";
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[1, 1].Value  = "년  도";
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[1, 2].Value  = "순  번";
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[1, 3].Value  = "";
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[1, 4].Value  = "";
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[1, 5].Value  = "";
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[1, 6].Value  = "";
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[1, 7].Value  = "";
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[1, 8].Value  = "";
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[1, 9].Value  = "취득금액";
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[1, 10].Value = "매각금액";
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[1, 11].Value = "잔    액";
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[1, 12].Value = "정 회 원";
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[1, 13].Value = "준 회 원";
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[1, 14].Value = "";
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[1, 15].Value = "";

            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[0, 3].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[0, 8].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[0, 9].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[0, 12].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[0, 14].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[0, 15].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[1, 0].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[1, 1].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[1, 2].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[1, 9].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[1, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[1, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[1, 12].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43I52775_Sheet1.ColumnHeader.Cells[1, 13].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            #endregion

            #region Description : 탭2 - 회원권 MAST

            this.FPS91_TY_S_AC_43J3X819_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_43J3X819_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_43J3X819_Sheet1.AddColumnHeaderSpanCell(0, 0, 1, 3);
            this.FPS91_TY_S_AC_43J3X819_Sheet1.AddColumnHeaderSpanCell(0, 5, 1, 2);

            this.FPS91_TY_S_AC_43J3X819_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);
            this.FPS91_TY_S_AC_43J3X819_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1);
            this.FPS91_TY_S_AC_43J3X819_Sheet1.AddColumnHeaderSpanCell(0, 7, 2, 1);
            this.FPS91_TY_S_AC_43J3X819_Sheet1.AddColumnHeaderSpanCell(0, 8, 2, 1);

            this.FPS91_TY_S_AC_43J3X819_Sheet1.ColumnHeader.Cells[0, 0].Value = "관리번호";
            this.FPS91_TY_S_AC_43J3X819_Sheet1.ColumnHeader.Cells[0, 3].Value = "거래처";
            this.FPS91_TY_S_AC_43J3X819_Sheet1.ColumnHeader.Cells[0, 4].Value = "거래처명";
            this.FPS91_TY_S_AC_43J3X819_Sheet1.ColumnHeader.Cells[0, 5].Value = "구좌당 회원수";
            this.FPS91_TY_S_AC_43J3X819_Sheet1.ColumnHeader.Cells[0, 7].Value = "보유구좌";
            this.FPS91_TY_S_AC_43J3X819_Sheet1.ColumnHeader.Cells[0, 8].Value = "취득금액";

            this.FPS91_TY_S_AC_43J3X819_Sheet1.ColumnHeader.Cells[1, 0].Value = "회원권";
            this.FPS91_TY_S_AC_43J3X819_Sheet1.ColumnHeader.Cells[1, 1].Value = "년  도";
            this.FPS91_TY_S_AC_43J3X819_Sheet1.ColumnHeader.Cells[1, 2].Value = "순  번";
            this.FPS91_TY_S_AC_43J3X819_Sheet1.ColumnHeader.Cells[1, 3].Value = "";
            this.FPS91_TY_S_AC_43J3X819_Sheet1.ColumnHeader.Cells[1, 4].Value = "";
            this.FPS91_TY_S_AC_43J3X819_Sheet1.ColumnHeader.Cells[1, 5].Value = "정회원";
            this.FPS91_TY_S_AC_43J3X819_Sheet1.ColumnHeader.Cells[1, 6].Value = "준회수";
            this.FPS91_TY_S_AC_43J3X819_Sheet1.ColumnHeader.Cells[1, 7].Value = "";
            this.FPS91_TY_S_AC_43J3X819_Sheet1.ColumnHeader.Cells[1, 8].Value = "";

            this.FPS91_TY_S_AC_43J3X819_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43J3X819_Sheet1.ColumnHeader.Cells[0, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43J3X819_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43J3X819_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43J3X819_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43J3X819_Sheet1.ColumnHeader.Cells[0, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_43J3X819_Sheet1.ColumnHeader.Cells[1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43J3X819_Sheet1.ColumnHeader.Cells[1, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43J3X819_Sheet1.ColumnHeader.Cells[1, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43J3X819_Sheet1.ColumnHeader.Cells[1, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43J3X819_Sheet1.ColumnHeader.Cells[1, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            #endregion
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            if (this.TXT01_STDATE.GetValue().ToString() != "" &&
                this.TXT01_EDDATE.GetValue().ToString() != "") // 취득일자 기준
            {
                this.DbConnector.Attach
                    (
                    "TY_P_AC_43I58774",
                    this.TXT01_STDATE.GetValue().ToString(),
                    this.TXT01_EDDATE.GetValue().ToString(),
                    this.CBO01_GGUBUN.GetValue().ToString()
                    );
            }
            else // 기준일자 기준(금액있는것만 나옴)
            {
                this.DbConnector.Attach
                    (
                    "TY_P_AC_43J3H818",
                    this.TXT01_GDATE.GetValue().ToString(),
                    this.TXT01_GDATE.GetValue().ToString(),
                    this.CBO01_GGUBUN.GetValue().ToString(),
                    this.TXT01_GDATE.GetValue().ToString(),
                    this.CBO01_GGUBUN.GetValue().ToString()
                    );
            }

            dt = this.DbConnector.ExecuteDataTable();

            // 탭1
            this.FPS91_TY_S_AC_43I52775.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                int i = 0;

                this.SpreadSumRowAdd(this.FPS91_TY_S_AC_43I52775, "ASCUSTCD", "합 계", Color.Yellow);
                for (i = 0; i < this.FPS91_TY_S_AC_43I52775.ActiveSheet.RowCount; i++)
                {
                    this.FPS91_TY_S_AC_43I52775_Sheet1.SetFormula(
                        i,  // row
                        11, // column
                        "R[0]C[-2] - R[0]C[-1]"); // 잔액
                }

                this.FPS91_TY_S_AC_43I52775.ActiveSheet.Rows[i - 1].Visible = false;
            }

            this.FPS91_TY_S_AC_43J9T784.Initialize();

            this.CBO02_ANMEMGUBN.SetValue("");
            this.TXT02_ANMEMYEAR.SetValue("");
            this.TXT02_ANMEMSEQ.SetValue("");
        }
        #endregion

        #region Description : 회원권 MAST 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            int i = 0;
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // 순번 부여
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_43JAT790", ds.Tables[0].Rows[i]["ASMEMGUBN"].ToString(),
                                                            ds.Tables[0].Rows[i]["ASMEMYEAR"].ToString());

                ds.Tables[0].Rows[i]["ASMEMSEQ"] = this.DbConnector.ExecuteScalar();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_43JB0795", ds.Tables[0].Rows[i]["ASMEMGUBN"].ToString(),
                                                            ds.Tables[0].Rows[i]["ASMEMYEAR"].ToString(),
                                                            ds.Tables[0].Rows[i]["ASMEMSEQ"].ToString(),
                                                            ds.Tables[0].Rows[i]["ASCUSTCD"].ToString(),
                                                            ds.Tables[0].Rows[i]["ASACQCOST"].ToString(),
                                                            ds.Tables[0].Rows[i]["ASDURYEAR"].ToString(),
                                                            ds.Tables[0].Rows[i]["ASCONTENT"].ToString(),
                                                            ds.Tables[0].Rows[i]["ASCONCDDP"].ToString(),
                                                            ds.Tables[0].Rows[i]["ASBIGO"].ToString(),
                                                            ds.Tables[0].Rows[i]["ASREGULAR"].ToString(),
                                                            ds.Tables[0].Rows[i]["ASASSOCIAT"].ToString(),
                                                            ds.Tables[0].Rows[i]["ASPOSSCNT"].ToString(),
                                                            ds.Tables[0].Rows[i]["ASDISTAMT"].ToString(),
                                                            ds.Tables[0].Rows[i]["ASACQCOST"].ToString()
                                                            );

                this.DbConnector.ExecuteTranQueryList();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();

                for (i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_43JB1796", ds.Tables[1].Rows[i]["ASCUSTCD"].ToString(),
                                                                ds.Tables[1].Rows[i]["ASACQCOST"].ToString(),
                                                                ds.Tables[1].Rows[i]["ASDURYEAR"].ToString(),
                                                                ds.Tables[1].Rows[i]["ASCONTENT"].ToString(),
                                                                ds.Tables[1].Rows[i]["ASCONCDDP"].ToString(),
                                                                ds.Tables[1].Rows[i]["ASBIGO"].ToString(),
                                                                ds.Tables[1].Rows[i]["ASREGULAR"].ToString(),
                                                                ds.Tables[1].Rows[i]["ASASSOCIAT"].ToString(),
                                                                ds.Tables[1].Rows[i]["ASPOSSCNT"].ToString(),
                                                                ds.Tables[1].Rows[i]["ASDISTAMT"].ToString(),
                                                                ds.Tables[1].Rows[i]["ASSTCOKAMT"].ToString(),
                                                                ds.Tables[1].Rows[i]["ASMEMGUBN"].ToString(),
                                                                ds.Tables[1].Rows[i]["ASMEMYEAR"].ToString(),
                                                                ds.Tables[1].Rows[i]["ASMEMSEQ"].ToString()
                                                                );
                }

                this.DbConnector.ExecuteTranQueryList();
            }

            this.ShowMessage("TY_M_GB_23NAD873");
            this.BTN61_INQ_Click(null, null);

            this.FPS91_TY_S_AC_43J9T784.Initialize();

            this.CBO02_ANMEMGUBN.SetValue("");
            this.TXT02_ANMEMYEAR.SetValue("");
            this.TXT02_ANMEMSEQ.SetValue("");
        }
        #endregion

        #region Description : 저장품 MAST 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();

            for (i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_43JB1797", dt.Rows[i]["ASMEMGUBN"].ToString(),
                                                            dt.Rows[i]["ASMEMYEAR"].ToString(),
                                                            dt.Rows[i]["ASMEMSEQ"].ToString());
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지
            this.BTN61_INQ_Click(null, null);

            this.FPS91_TY_S_AC_43J9T784.Initialize();

            this.CBO02_ANMEMGUBN.SetValue("");
            this.TXT02_ANMEMYEAR.SetValue("");
            this.TXT02_ANMEMSEQ.SetValue("");
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();

            // 취득일자
            if (this.TXT01_STDATE.GetValue().ToString() != "" && this.TXT01_EDDATE.GetValue().ToString() != "")
            {
                //회계팀 명세서
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_43LBR914",
                    this.TXT01_STDATE.GetValue().ToString(),
                    this.TXT01_EDDATE.GetValue().ToString(),
                    this.CBO01_GGUBUN.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    SectionReport rpt = new TYACSE005R1(this.TXT01_EDDATE.GetValue().ToString());

                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }

                //총무팀 명세서
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_43KHK902",
                    this.TXT01_STDATE.GetValue().ToString(),
                    this.TXT01_EDDATE.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_43LFN919",
                    this.TXT01_STDATE.GetValue().ToString(),
                    this.TXT01_EDDATE.GetValue().ToString()
                    );

                dt2 = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    SectionReport rpt = new TYACSE005R2(dt2, this.TXT01_EDDATE.GetValue().ToString());

                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
            }
            // 기준일자
            else
            {
                //회계팀 명세서
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_43OGK960",
                    this.TXT01_GDATE.GetValue().ToString(),
                    this.CBO01_GGUBUN.GetValue().ToString(),
                    this.TXT01_GDATE.GetValue().ToString(),
                    this.CBO01_GGUBUN.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    SectionReport rpt = new TYACSE005R1(this.TXT01_GDATE.GetValue().ToString());

                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }

                //총무팀 명세서
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_441HT112",
                    this.TXT01_GDATE.GetValue().ToString(),
                    this.TXT01_GDATE.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_441HT114",
                    this.TXT01_GDATE.GetValue().ToString(),
                    this.TXT01_GDATE.GetValue().ToString()
                    );

                dt2 = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    SectionReport rpt = new TYACSE005R2(dt2, this.TXT01_GDATE.GetValue().ToString());

                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
            }
        }
        #endregion

        #region Description : 회원권 회원번호 저장 버튼
        private void BTN62_SAV_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // 등록
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_43JBU798", this.CBO02_ANMEMGUBN.GetValue().ToString(),
                                                                this.TXT02_ANMEMYEAR.GetValue().ToString(),
                                                                this.TXT02_ANMEMSEQ.GetValue().ToString(),
                                                                ds.Tables[0].Rows[i]["ANSHIPNUM"].ToString(),
                                                                ds.Tables[0].Rows[i]["ANGUBNUSE"].ToString(),
                                                                ds.Tables[0].Rows[i]["ANDISTDAY"].ToString(),
                                                                ds.Tables[0].Rows[i]["ANDISTAMT"].ToString(),
                                                                ds.Tables[0].Rows[i]["ANSALEAMT"].ToString(),
                                                                ds.Tables[0].Rows[i]["ANSALEREA"].ToString()
                                                                );
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            // 수정
            if (ds.Tables[1].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_43JBV799", ds.Tables[1].Rows[i]["ANGUBNUSE"].ToString(),
                                                                ds.Tables[1].Rows[i]["ANDISTDAY"].ToString(),
                                                                ds.Tables[1].Rows[i]["ANDISTAMT"].ToString(),
                                                                ds.Tables[1].Rows[i]["ANSALEAMT"].ToString(),
                                                                ds.Tables[1].Rows[i]["ANSALEREA"].ToString(),
                                                                this.CBO02_ANMEMGUBN.GetValue().ToString(),
                                                                this.TXT02_ANMEMYEAR.GetValue().ToString(),
                                                                this.TXT02_ANMEMSEQ.GetValue().ToString(),
                                                                ds.Tables[1].Rows[i]["ANSHIPNUM"].ToString()
                                                                );
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            // 회원권 MAST 매각장부금액 업데이트
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_43J1X809",
                this.CBO02_ANMEMGUBN.GetValue().ToString(),
                this.TXT02_ANMEMYEAR.GetValue().ToString(),
                this.TXT02_ANMEMSEQ.GetValue().ToString(),
                this.CBO02_ANMEMGUBN.GetValue().ToString(),
                this.TXT02_ANMEMYEAR.GetValue().ToString(),
                this.TXT02_ANMEMSEQ.GetValue().ToString(),
                this.CBO02_ANMEMGUBN.GetValue().ToString(),
                this.TXT02_ANMEMYEAR.GetValue().ToString(),
                this.TXT02_ANMEMSEQ.GetValue().ToString()
                );
            this.DbConnector.ExecuteNonQuery();

            this.ShowMessage("TY_M_GB_23NAD873");
            this.BTN61_INQ_Click(null, null);

            this.CBO02_ANMEMGUBN.SetValue("");
            this.TXT02_ANMEMYEAR.SetValue("");
            this.TXT02_ANMEMSEQ.SetValue("");
        }
        #endregion

        #region Description : 회원권 회원번호 삭제 버튼
        private void BTN62_REM_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            for (i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_43JBV800", this.CBO02_ANMEMGUBN.GetValue().ToString(),
                                                            this.TXT02_ANMEMYEAR.GetValue().ToString(),
                                                            this.TXT02_ANMEMSEQ.GetValue().ToString(),
                                                            dt.Rows[i]["ANSHIPNUM"].ToString());
            }
            this.DbConnector.ExecuteTranQueryList();

            // 회원권 MAST 매각장부금액 업데이트
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_43J1X809",
                this.CBO02_ANMEMGUBN.GetValue().ToString(),
                this.TXT02_ANMEMYEAR.GetValue().ToString(),
                this.TXT02_ANMEMSEQ.GetValue().ToString(),
                this.CBO02_ANMEMGUBN.GetValue().ToString(),
                this.TXT02_ANMEMYEAR.GetValue().ToString(),
                this.TXT02_ANMEMSEQ.GetValue().ToString(),
                this.CBO02_ANMEMGUBN.GetValue().ToString(),
                this.TXT02_ANMEMYEAR.GetValue().ToString(),
                this.TXT02_ANMEMSEQ.GetValue().ToString()
                );
            this.DbConnector.ExecuteNonQuery();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지
            this.BTN61_INQ_Click(null, null);

            this.CBO02_ANMEMGUBN.SetValue("");
            this.TXT02_ANMEMYEAR.SetValue("");
            this.TXT02_ANMEMSEQ.SetValue("");
        }
        #endregion

        #region Description : 탭2 - 버튼 이벤트
        private void BTN63_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            if (this.TXT01_STDATE.GetValue().ToString() != "" &&
                this.TXT01_EDDATE.GetValue().ToString() != "") // 취득일자 기준
            {
                this.DbConnector.Attach
                    (
                    "TY_P_AC_43I58774",
                    this.TXT01_STDATE.GetValue().ToString(),
                    this.TXT01_EDDATE.GetValue().ToString(),
                    this.CBO01_GGUBUN.GetValue().ToString()
                    );
            }
            else // 기준일자 기준(금액있는것만 나옴)
            {
                this.DbConnector.Attach
                    (
                    "TY_P_AC_43J3H818",
                    this.TXT01_GDATE.GetValue().ToString(),
                    this.TXT01_GDATE.GetValue().ToString(),
                    this.CBO01_GGUBUN.GetValue().ToString(),
                    this.TXT01_GDATE.GetValue().ToString(),
                    this.CBO01_GGUBUN.GetValue().ToString()
                    );
            }

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_43J3X819.SetValue(dt);


            this.CBO03_ANMEMGUBN.SetValue("");
            this.TXT03_ANMEMYEAR.SetValue("");
            this.TXT03_ANMEMSEQ.SetValue("");

            this.FPS91_TY_S_AC_43J4R820.Initialize();
            this.FPS91_TY_S_AC_43J5P827.Initialize();
        }

        private void BTN63_SAV_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // 등록
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_43J65831", this.CBO03_ANMEMGUBN.GetValue().ToString(),
                                                                this.TXT03_ANMEMYEAR.GetValue().ToString(),
                                                                this.TXT03_ANMEMSEQ.GetValue().ToString(),
                                                                this.TXT03_AHSHIPNUM.GetValue().ToString(),
                                                                ds.Tables[0].Rows[i]["AHCHADATE"].ToString(),
                                                                ds.Tables[0].Rows[i]["AHGUBNCHA"].ToString(),
                                                                ds.Tables[0].Rows[i]["AHUSENAME"].ToString(),
                                                                ds.Tables[0].Rows[i]["AHUSECDDP"].ToString(),
                                                                ds.Tables[0].Rows[i]["AHHISTREA"].ToString(),
                                                                ds.Tables[0].Rows[i]["AHCOMMAMT"].ToString()
                                                                );
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            // 수정
            if (ds.Tables[1].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_43J6A832", ds.Tables[1].Rows[i]["AHGUBNCHA"].ToString(),
                                                                ds.Tables[1].Rows[i]["AHUSENAME"].ToString(),
                                                                ds.Tables[1].Rows[i]["AHUSECDDP"].ToString(),
                                                                ds.Tables[1].Rows[i]["AHHISTREA"].ToString(),
                                                                ds.Tables[1].Rows[i]["AHCOMMAMT"].ToString(),
                                                                this.CBO03_ANMEMGUBN.GetValue().ToString(),
                                                                this.TXT03_ANMEMYEAR.GetValue().ToString(),
                                                                this.TXT03_ANMEMSEQ.GetValue().ToString(),
                                                                this.TXT03_AHSHIPNUM.GetValue().ToString(),
                                                                ds.Tables[1].Rows[i]["AHCHADATE"].ToString()
                                                                );
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            string sOUTMSG = string.Empty;

            // 회원권 회원번호 사용자 업데이트
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_43K9E836",
                this.CBO03_ANMEMGUBN.GetValue().ToString(),
                this.TXT03_ANMEMYEAR.GetValue().ToString(),
                this.TXT03_ANMEMSEQ.GetValue().ToString(),
                this.TXT03_AHSHIPNUM.GetValue().ToString(),
                sOUTMSG.ToString()
                );

            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUTMSG.ToString() == "OK")
            {
                this.ShowMessage("TY_M_GB_23NAD873");
                this.BTN63_INQ_Click(null, null);
            }
        }

        private void BTN63_REM_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            for (i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_43J6A833", this.CBO03_ANMEMGUBN.GetValue().ToString(),
                                                            this.TXT03_ANMEMYEAR.GetValue().ToString(),
                                                            this.TXT03_ANMEMSEQ.GetValue().ToString(),
                                                            this.TXT03_AHSHIPNUM.GetValue().ToString(),
                                                            dt.Rows[i]["AHCHADATE"].ToString());
            }
            this.DbConnector.ExecuteTranQueryList();

            string sOUTMSG = string.Empty;

            // 회원권 회원번호 사용자 업데이트
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_43K9E836",
                this.CBO03_ANMEMGUBN.GetValue().ToString(),
                this.TXT03_ANMEMYEAR.GetValue().ToString(),
                this.TXT03_ANMEMSEQ.GetValue().ToString(),
                this.TXT03_AHSHIPNUM.GetValue().ToString(),
                sOUTMSG.ToString()
                );

            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUTMSG.ToString() == "OK")
            {
                this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지
                this.BTN63_INQ_Click(null, null);
            }
        }
        #endregion

        #region Description : 회원권 MAST 조회 ProcessCheck 이벤트
        private void BTN61_INQ_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.TXT01_STDATE.GetValue().ToString() == "" && this.TXT01_EDDATE.GetValue().ToString() == "" && this.TXT01_GDATE.GetValue().ToString() == "")
            {
                SetFocus(TXT01_STDATE);
                this.ShowMessage("TY_M_AC_4372A599");
                e.Successed = false;
                return;
            }
        }

        private void BTN63_INQ_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.TXT01_STDATE.GetValue().ToString() == "" && this.TXT01_EDDATE.GetValue().ToString() == "" && this.TXT01_GDATE.GetValue().ToString() == "")
            {
                SetFocus(TXT01_STDATE);
                this.ShowMessage("TY_M_AC_4372A599");
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 회원권 MAST 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataSet ds = new DataSet();

            // 등록 및 수정을 동시에 할 경우
            // 등록이 먼저이면 Tables[0]
            // 수정이 나중이면 Tables[1]임
            // 저장
            ds.Tables.Add(this.FPS91_TY_S_AC_43I52775.GetDataSourceInclude(TSpread.TActionType.New,    "ASMEMGUBN", "ASMEMYEAR", "ASMEMSEQ", "ASCUSTCD", "ASDURYEAR", "ASCONTENT", "ASCONCDDP", "ASACQCOST", "ASDISTAMT", "ASSTCOKAMT", "ASREGULAR", "ASASSOCIAT", "ASPOSSCNT", "ASBIGO"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_43I52775.GetDataSourceInclude(TSpread.TActionType.Update, "ASMEMGUBN", "ASMEMYEAR", "ASMEMSEQ", "ASCUSTCD", "ASDURYEAR", "ASCONTENT", "ASCONCDDP", "ASACQCOST", "ASDISTAMT", "ASSTCOKAMT", "ASREGULAR", "ASASSOCIAT", "ASPOSSCNT", "ASBIGO"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
                e.Successed = false;
                return;
            }

            for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                // 거래처명
                if (double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["ASDISTAMT"].ToString())) != 0)
                {
                    this.ShowMessage("TY_M_AC_43JAW793");
                    e.Successed = false;
                    return;
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

        #region Description : 회원권 MAST 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataTable dt = this.FPS91_TY_S_AC_43I52775.GetDataSourceInclude(TSpread.TActionType.Remove, "ASMEMGUBN", "ASMEMYEAR", "ASMEMSEQ", "ASDISTAMT");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            DataTable dt1 = new DataTable();

            for (i = 0; i < dt.Rows.Count; i++)
            {
                // 회원권 회원 이력관리 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_43J11801",
                    dt.Rows[i]["ASMEMGUBN"].ToString(),
                    dt.Rows[i]["ASMEMYEAR"].ToString(),
                    dt.Rows[i]["ASMEMSEQ"].ToString(),
                    ""
                    );

                dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_43J14802");
                    e.Successed = false;
                    return;
                }

                // 거래처명
                if (double.Parse(Get_Numeric(dt.Rows[i]["ASDISTAMT"].ToString())) != 0)
                {
                    this.ShowMessage("TY_M_AC_43JAW793");
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

        #region Description : 회원권 회원번호 저장 ProcessCheck 이벤트
        private void BTN62_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_43J9T784.GetDataSourceInclude(TSpread.TActionType.New,    "ANSHIPNUM", "ANGUBNUSE", "ANDISTDAY", "ANDISTAMT", "ANSALEAMT", "ANSALEREA"));
            ds.Tables.Add(this.FPS91_TY_S_AC_43J9T784.GetDataSourceInclude(TSpread.TActionType.Update, "ANSHIPNUM", "ANGUBNUSE", "ANDISTDAY", "ANDISTAMT", "ANSALEAMT", "ANSALEREA", "ANGUBNUSE1"));
            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }
            else
            {
                double dCNT1 = 0;
                double dCNT2 = 0;

                double dDetail_CNT1 = 0;
                double dDetail_CNT2 = 0;

                DataTable dt = new DataTable();

                // 마스터에 저장된 정회원, 준회원 자료
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_43J2K812",
                    this.CBO02_ANMEMGUBN.GetValue().ToString(),
                    this.TXT02_ANMEMYEAR.GetValue().ToString(),
                    this.TXT02_ANMEMSEQ.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    dCNT1 = double.Parse(dt.Rows[0]["CNT1"].ToString());
                    dCNT2 = double.Parse(dt.Rows[0]["CNT2"].ToString());
                }


                // 회원내역에 저장된 정회원수 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_43J2Q813",
                    this.CBO02_ANMEMGUBN.GetValue().ToString(),
                    this.TXT02_ANMEMYEAR.GetValue().ToString(),
                    this.TXT02_ANMEMSEQ.GetValue().ToString(),
                    "1"
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    dDetail_CNT1 = double.Parse(dt.Rows[0]["CNT"].ToString());
                }

                // 회원내역에 저장된 준회원수 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_43J2Q813",
                    this.CBO02_ANMEMGUBN.GetValue().ToString(),
                    this.TXT02_ANMEMYEAR.GetValue().ToString(),
                    this.TXT02_ANMEMSEQ.GetValue().ToString(),
                    "2"
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    dDetail_CNT2 = double.Parse(dt.Rows[0]["CNT"].ToString());
                }

                // 저장
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        // 매각일자
                        if (ds.Tables[0].Rows[i]["ANDISTDAY"].ToString() != "")
                        {
                            //// 매각장부금액
                            //if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["ANDISTAMT"].ToString())) == 0)
                            //{
                            //    this.ShowMessage("TY_M_AC_43J1H806");
                            //    e.Successed = false;
                            //    return;
                            //}

                            //// 매각실금액
                            //if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["ANSALEAMT"].ToString())) == 0)
                            //{
                            //    this.ShowMessage("TY_M_AC_43J1H807");
                            //    e.Successed = false;
                            //    return;
                            //}

                            // 매각사유
                            if (ds.Tables[0].Rows[i]["ANSALEREA"].ToString() == "")
                            {
                                this.ShowMessage("TY_M_AC_43J1H808");
                                e.Successed = false;
                                return;
                            }
                        }

                        //// 매각장부금액
                        //if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["ANDISTAMT"].ToString())) != 0)
                        //{
                        //    // 매각일자
                        //    if (ds.Tables[0].Rows[i]["ANDISTDAY"].ToString() == "")
                        //    {
                        //        this.ShowMessage("TY_M_AC_43J1G805");
                        //        e.Successed = false;
                        //        return;
                        //    }

                        //    // 매각실금액
                        //    if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["ANSALEAMT"].ToString())) == 0)
                        //    {
                        //        this.ShowMessage("TY_M_AC_43J1H807");
                        //        e.Successed = false;
                        //        return;
                        //    }

                        //    // 매각사유
                        //    if (ds.Tables[0].Rows[i]["ANSALEREA"].ToString() == "")
                        //    {
                        //        this.ShowMessage("TY_M_AC_43J1H808");
                        //        e.Successed = false;
                        //        return;
                        //    }
                        //}

                        //// 매각실금액
                        //if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["ANSALEAMT"].ToString())) != 0)
                        //{
                        //    // 매각일자
                        //    if (ds.Tables[0].Rows[i]["ANDISTDAY"].ToString() == "")
                        //    {
                        //        this.ShowMessage("TY_M_AC_43J1G805");
                        //        e.Successed = false;
                        //        return;
                        //    }

                        //    // 매각장부금액
                        //    if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["ANDISTAMT"].ToString())) == 0)
                        //    {
                        //        this.ShowMessage("TY_M_AC_43J1H806");
                        //        e.Successed = false;
                        //        return;
                        //    }

                        //    // 매각사유
                        //    if (ds.Tables[0].Rows[i]["ANSALEREA"].ToString() == "")
                        //    {
                        //        this.ShowMessage("TY_M_AC_43J1H808");
                        //        e.Successed = false;
                        //        return;
                        //    }
                        //}

                        // 매각사유
                        if (ds.Tables[0].Rows[i]["ANSALEREA"].ToString() != "")
                        {
                            // 매각일자
                            if (ds.Tables[0].Rows[i]["ANDISTDAY"].ToString() == "")
                            {
                                this.ShowMessage("TY_M_AC_43J1G805");
                                e.Successed = false;
                                return;
                            }

                            //// 매각장부금액
                            //if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["ANDISTAMT"].ToString())) == 0)
                            //{
                            //    this.ShowMessage("TY_M_AC_43J1H806");
                            //    e.Successed = false;
                            //    return;
                            //}

                            //// 매각실금액
                            //if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["ANSALEAMT"].ToString())) == 0)
                            //{
                            //    this.ShowMessage("TY_M_AC_43J1H807");
                            //    e.Successed = false;
                            //    return;
                            //}
                        }

                        // 회원구분에 따른 카운트
                        if (ds.Tables[0].Rows[i]["ANGUBNUSE"].ToString() == "1") // 정회원
                        {
                            dDetail_CNT1++;
                        }
                        else // 준회원
                        {
                            dDetail_CNT2++;
                        }
                    }

                    // 정회원수 확인
                    if (dCNT1 < dDetail_CNT1)
                    {
                        this.ShowMessage("TY_M_AC_43J2T814");
                        e.Successed = false;
                        return;
                    }

                    // 준회원수 확인
                    if (dCNT2 < dDetail_CNT2)
                    {
                        this.ShowMessage("TY_M_AC_43J2U817");
                        e.Successed = false;
                        return;
                    }
                }


                dDetail_CNT1 = 0;
                dDetail_CNT2 = 0;

                // 회원내역에 저장된 정회원수 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_43J2Q813",
                    this.CBO02_ANMEMGUBN.GetValue().ToString(),
                    this.TXT02_ANMEMYEAR.GetValue().ToString(),
                    this.TXT02_ANMEMSEQ.GetValue().ToString(),
                    "1"
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    dDetail_CNT1 = double.Parse(dt.Rows[0]["CNT"].ToString());
                }

                // 회원내역에 저장된 준회원수 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_43J2Q813",
                    this.CBO02_ANMEMGUBN.GetValue().ToString(),
                    this.TXT02_ANMEMYEAR.GetValue().ToString(),
                    this.TXT02_ANMEMSEQ.GetValue().ToString(),
                    "2"
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    dDetail_CNT2 = double.Parse(dt.Rows[0]["CNT"].ToString());
                }

                // 수정
                if (ds.Tables[1].Rows.Count > 0)
                {
                    for (i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        // 매각일자
                        if (ds.Tables[1].Rows[i]["ANDISTDAY"].ToString() != "")
                        {
                            //// 매각장부금액
                            //if (double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["ANDISTAMT"].ToString())) == 0)
                            //{
                            //    this.ShowMessage("TY_M_AC_43J1H806");
                            //    e.Successed = false;
                            //    return;
                            //}

                            //// 매각실금액
                            //if (double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["ANSALEAMT"].ToString())) == 0)
                            //{
                            //    this.ShowMessage("TY_M_AC_43J1H807");
                            //    e.Successed = false;
                            //    return;
                            //}

                            // 매각사유
                            if (ds.Tables[1].Rows[i]["ANSALEREA"].ToString() == "")
                            {
                                this.ShowMessage("TY_M_AC_43J1H808");
                                e.Successed = false;
                                return;
                            }
                        }

                        //// 매각장부금액
                        //if (double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["ANDISTAMT"].ToString())) != 0)
                        //{
                        //    // 매각일자
                        //    if (ds.Tables[1].Rows[i]["ANDISTDAY"].ToString() == "")
                        //    {
                        //        this.ShowMessage("TY_M_AC_43J1G805");
                        //        e.Successed = false;
                        //        return;
                        //    }

                        //    // 매각실금액
                        //    if (double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["ANSALEAMT"].ToString())) == 0)
                        //    {
                        //        this.ShowMessage("TY_M_AC_43J1H807");
                        //        e.Successed = false;
                        //        return;
                        //    }

                        //    // 매각사유
                        //    if (ds.Tables[1].Rows[i]["ANSALEREA"].ToString() == "")
                        //    {
                        //        this.ShowMessage("TY_M_AC_43J1H808");
                        //        e.Successed = false;
                        //        return;
                        //    }
                        //}

                        //// 매각실금액
                        //if (double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["ANSALEAMT"].ToString())) != 0)
                        //{
                        //    // 매각일자
                        //    if (ds.Tables[1].Rows[i]["ANDISTDAY"].ToString() == "")
                        //    {
                        //        this.ShowMessage("TY_M_AC_43J1G805");
                        //        e.Successed = false;
                        //        return;
                        //    }

                        //    // 매각장부금액
                        //    if (double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["ANDISTAMT"].ToString())) == 0)
                        //    {
                        //        this.ShowMessage("TY_M_AC_43J1H806");
                        //        e.Successed = false;
                        //        return;
                        //    }

                        //    // 매각사유
                        //    if (ds.Tables[1].Rows[i]["ANSALEREA"].ToString() == "")
                        //    {
                        //        this.ShowMessage("TY_M_AC_43J1H808");
                        //        e.Successed = false;
                        //        return;
                        //    }
                        //}

                        // 매각사유
                        if (ds.Tables[1].Rows[i]["ANSALEREA"].ToString() != "")
                        {
                            // 매각일자
                            if (ds.Tables[1].Rows[i]["ANDISTDAY"].ToString() == "")
                            {
                                this.ShowMessage("TY_M_AC_43J1G805");
                                e.Successed = false;
                                return;
                            }

                            //// 매각장부금액
                            //if (double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["ANDISTAMT"].ToString())) == 0)
                            //{
                            //    this.ShowMessage("TY_M_AC_43J1H806");
                            //    e.Successed = false;
                            //    return;
                            //}

                            //// 매각실금액
                            //if (double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["ANSALEAMT"].ToString())) == 0)
                            //{
                            //    this.ShowMessage("TY_M_AC_43J1H807");
                            //    e.Successed = false;
                            //    return;
                            //}
                        }

                        // 회원구분에 따른 카운트
                        if (ds.Tables[1].Rows[i]["ANGUBNUSE"].ToString() == "1") // 정회원
                        {
                            dDetail_CNT1++;

                            if (ds.Tables[1].Rows[i]["ANGUBNUSE1"].ToString() == "2")
                            {
                                dDetail_CNT2--;
                            }
                        }
                        else // 준회원
                        {
                            dDetail_CNT2++;

                            if (ds.Tables[1].Rows[i]["ANGUBNUSE1"].ToString() == "1")
                            {
                                dDetail_CNT1--;
                            }
                        }
                    }

                    // 정회원수 확인
                    if (dCNT1 < dDetail_CNT1)
                    {
                        this.ShowMessage("TY_M_AC_43J2T814");
                        e.Successed = false;
                        return;
                    }

                    // 준회원수 확인
                    if (dCNT2 < dDetail_CNT2)
                    {
                        this.ShowMessage("TY_M_AC_43J2U817");
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

        #region Description : 회원권 회원번호 삭제 ProcessCheck 이벤트
        private void BTN62_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataTable dt = this.FPS91_TY_S_AC_43J9T784.GetDataSourceInclude(TSpread.TActionType.Remove, "ANSHIPNUM", "ANGUBNUSE", "ANDISTDAY", "ANDISTAMT", "ANSALEAMT", "ANSALEREA");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            DataTable dt1 = new DataTable();

            for (i = 0; i < dt.Rows.Count; i++)
            {
                // 회원권 회원 이력관리 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_43J11801",
                    this.CBO02_ANMEMGUBN.GetValue().ToString(),
                    this.TXT02_ANMEMYEAR.GetValue().ToString(),
                    this.TXT02_ANMEMSEQ.GetValue().ToString(),
                    dt.Rows[i]["ANSHIPNUM"].ToString()
                    );

                dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_43J14802");
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

        #region Description : 회원권 사용자 이력 저장 ProcessCheck 이벤트
        private void BTN63_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_43J5P827.GetDataSourceInclude(TSpread.TActionType.New,    "AHCHADATE", "AHGUBNCHA", "AHUSENAME", "AHUSECDDP", "AHHISTREA", "AHCOMMAMT"));
            ds.Tables.Add(this.FPS91_TY_S_AC_43J5P827.GetDataSourceInclude(TSpread.TActionType.Update, "AHCHADATE", "AHGUBNCHA", "AHUSENAME", "AHUSECDDP", "AHHISTREA", "AHCOMMAMT"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }
            else
            {
                DataTable dt1 = new DataTable();
                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                    (
                    "TY_P_AC_43K92834",
                    this.CBO03_ANMEMGUBN.GetValue().ToString(),
                    this.TXT03_ANMEMYEAR.GetValue().ToString(),
                    this.TXT03_ANMEMSEQ.GetValue().ToString(),
                    this.TXT03_AHSHIPNUM.GetValue().ToString()
                    );

                dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_43K95835");
                    e.Successed = false;
                    return;
                }

                if (!this.ShowMessage("TY_M_GB_23NAD871"))
                {
                    e.Successed = false;
                    return;
                }

                e.ArgData = ds;
            }
        }
        #endregion

        #region Description : 회원권 사용자 삭제 ProcessCheck 이벤트
        private void BTN63_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_43J5P827.GetDataSourceInclude(TSpread.TActionType.Remove, "AHCHADATE");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            DataTable dt1 = new DataTable();
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_43K92834",
                this.CBO03_ANMEMGUBN.GetValue().ToString(),
                this.TXT03_ANMEMYEAR.GetValue().ToString(),
                this.TXT03_ANMEMSEQ.GetValue().ToString(),
                this.TXT03_AHSHIPNUM.GetValue().ToString()
                );

            dt1 = this.DbConnector.ExecuteDataTable();

            if (dt1.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_AC_43K95835");
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

        #region Description : 사용자 정의 함수
        private void UP_SetReadOnly(bool bTrueAndFalse)
        {
            this.CBO02_ANMEMGUBN.SetReadOnly(bTrueAndFalse);
            this.TXT02_ANMEMYEAR.SetReadOnly(bTrueAndFalse);
            this.TXT02_ANMEMSEQ.SetReadOnly(bTrueAndFalse);

            this.CBO03_ANMEMGUBN.SetReadOnly(bTrueAndFalse);
            this.TXT03_ANMEMYEAR.SetReadOnly(bTrueAndFalse);
            this.TXT03_ANMEMSEQ.SetReadOnly(bTrueAndFalse);
            this.TXT03_AHSHIPNUM.SetReadOnly(bTrueAndFalse);
        }
        #endregion

        #region Description : 회원권 MAST 스프레드 클릭 이벤트
        private void FPS91_TY_S_AC_43I52775_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.CBO02_ANMEMGUBN.SetValue(this.FPS91_TY_S_AC_43I52775.GetValue("ASMEMGUBN").ToString());
            this.TXT02_ANMEMYEAR.SetValue(this.FPS91_TY_S_AC_43I52775.GetValue("ASMEMYEAR").ToString());
            this.TXT02_ANMEMSEQ.SetValue(this.FPS91_TY_S_AC_43I52775.GetValue("ASMEMSEQ").ToString());
            
            UP_SetReadOnly(true);

            UP_Set_Detail();
        }
        #endregion

        #region Description : 회원권 회원번호 내역 조회
        private void UP_Set_Detail()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_43J9T783",
                this.CBO02_ANMEMGUBN.GetValue().ToString(),
                this.TXT02_ANMEMYEAR.GetValue().ToString(),
                this.TXT02_ANMEMSEQ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_43J9T784.SetValue(dt);
        }
        #endregion

        #region Description : 탭2-회원권 회원번호 내역 조회
        private void UP_Set_Detail_Tab2()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_43J9T783",
                this.CBO03_ANMEMGUBN.GetValue().ToString(),
                this.TXT03_ANMEMYEAR.GetValue().ToString(),
                this.TXT03_ANMEMSEQ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_43J4R820.SetValue(dt);
        }
        #endregion

        #region Description : 탭2-회원권 사용자 이력 내역 조회
        private void UP_Set_Detail2_Tab2()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_43J5P826",
                this.CBO03_ANMEMGUBN.GetValue().ToString(),
                this.TXT03_ANMEMYEAR.GetValue().ToString(),
                this.TXT03_ANMEMSEQ.GetValue().ToString(),
                this.TXT03_AHSHIPNUM.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_43J5P827.SetValue(dt);
        }
        #endregion

        #region Description : 포커스
        private void TXT01_AESQDATE_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (fsTabIndex == "1")
                {
                    SetFocus(this.BTN61_INQ);
                }
                else
                {
                    SetFocus(this.BTN63_INQ);
                }
            }
        }
        #endregion

        #region Description : 회원권 MAST 스프레드 이벤트
        private void FPS91_TY_S_AC_43I52775_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            if (e.Column == 0)
            {
                if (this.FPS91_TY_S_AC_43I52775.GetValue(e.Row, "AAMANGUBN").ToString() == "1")
                {
                    this.FPS91_TY_S_AC_43I52775.SetValue(e.Row, "AACNCDAC", "12100500");
                }
                else if (this.FPS91_TY_S_AC_43I52775.GetValue(e.Row, "AAMANGUBN").ToString() == "2")
                {
                    this.FPS91_TY_S_AC_43I52775.SetValue(e.Row, "AACNCDAC", "12100400");
                }
                //else if (this.FPS91_TY_S_AC_43I52775.GetValue(e.Row, "AAMANGUBN").ToString() == "3")
                //{
                //    this.FPS91_TY_S_AC_43I52775.SetValue(e.Row, "AACNCDAC", "12100200");
                //}
                else if (this.FPS91_TY_S_AC_43I52775.GetValue(e.Row, "AAMANGUBN").ToString() == "4")
                {
                    this.FPS91_TY_S_AC_43I52775.SetValue(e.Row, "AACNCDAC", "12100203");
                }
            }

            //if (e.Column == 4)
            //{
            //    this.FPS91_TY_S_AC_43I52775_Sheet1.Cells[e.Row, 6].Value = this.FPS91_TY_S_AC_43I52775_Sheet1.Cells[e.Row, 5].Text.ToString();
            //}
        }

        private void FPS91_TY_S_AC_43I52775_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            if (e.Column != 5)
                return;

            string year = FPS91_TY_S_AC_43I52775.GetValue(e.Row, "ASDURYEAR").ToString();
            TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_43I52775, "ASCONCDDP");
            if (tyCodeBox != null)
                tyCodeBox.DummyValue = year;
        }
        #endregion

        #region Description : 회원권 MAST 취득일자 이벤트(관리부서)
        private void FPS91_TY_S_AC_43I52775_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            if (e.Column != 5)
                return;

            string year = FPS91_TY_S_AC_43I52775.GetValue(e.Row, "ASDURYEAR").ToString();
            TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_43I52775, "ASCONCDDP");
            if (tyCodeBox != null)
                tyCodeBox.DummyValue = year;
        }
        #endregion

        #region Description : 탭 이벤트
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                fsTabIndex = "1";

                this.BTN61_INQ_Click(null, null);
            }
            else
            {
                fsTabIndex = "2";

                this.BTN63_INQ_Click(null, null);
            }

            UP_Items_Visible(fsTabIndex);
        }
        #endregion

        #region Description : 탭2 - 회원권 MAST 스프레드 이벤트
        private void FPS91_TY_S_AC_43J3X819_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.CBO03_ANMEMGUBN.SetValue(this.FPS91_TY_S_AC_43J3X819.GetValue("ASMEMGUBN").ToString());
            this.TXT03_ANMEMYEAR.SetValue(this.FPS91_TY_S_AC_43J3X819.GetValue("ASMEMYEAR").ToString());
            this.TXT03_ANMEMSEQ.SetValue(this.FPS91_TY_S_AC_43J3X819.GetValue("ASMEMSEQ").ToString());

            UP_SetReadOnly(true);

            UP_Set_Detail_Tab2();
        }
        #endregion

        #region Description : 탭2 - 회원권 회원번호 스프레드 이벤트
        private void FPS91_TY_S_AC_43J4R820_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.TXT03_AHSHIPNUM.SetValue(this.FPS91_TY_S_AC_43J4R820.GetValue("ANSHIPNUM").ToString());

            UP_Set_Detail2_Tab2();
        }
        #endregion

        #region Description : 회원권 사용자 이력 스프레드 이벤트
        private void FPS91_TY_S_AC_43J5P827_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            if (e.Column != 0)
                return;

            string year = FPS91_TY_S_AC_43J5P827.GetValue(e.Row, "AHCHADATE").ToString();
            TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_43J5P827, "AHUSECDDP");
            if (tyCodeBox != null)
                tyCodeBox.DummyValue = year;
        }

        private void FPS91_TY_S_AC_43J5P827_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            if (e.Column != 0)
                return;

            string year = FPS91_TY_S_AC_43J5P827.GetValue(e.Row, "AHCHADATE").ToString();
            TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_43J5P827, "AHUSECDDP");
            if (tyCodeBox != null)
                tyCodeBox.DummyValue = year;
        }
        #endregion
    }
}