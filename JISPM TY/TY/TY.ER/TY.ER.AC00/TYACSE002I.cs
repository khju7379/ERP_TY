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
    public partial class TYACSE002I : TYBase
    {
        #region Description : 페이지 로드
        public TYACSE002I()
        {
            InitializeComponent();

            // 스프레드에서 코드헬프 사용
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_43B9G643, "AACUSTCD", "AACUSTCDNM", "AACUSTCD");
        }

        private void TYACSE002I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_43B9G643, "AAMANGUBN");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_43B9G643, "AAMANYEAR");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_43B9G643, "AAMANSEQ");

            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_43B30659, "ADDATEJUST");

            this.BTN61_INQ.ProcessCheck += new TButton.CheckHandler(BTN61_INQ_ProcessCheck);

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.BTN62_SAV.ProcessCheck += new TButton.CheckHandler(BTN62_SAV_ProcessCheck);
            this.BTN62_REM.ProcessCheck += new TButton.CheckHandler(BTN62_REM_ProcessCheck);

            UP_Spread_Title();

            UP_SetReadOnly(false);

            // Blank Row 생성
            //UP_Row_Ins();

            SetStartingFocus(this.CBO01_GCDAC);
        }
        #endregion

        #region Description : 빈 ROW 생성
        private void UP_Row_Ins()
        {
            DataTable dt = new DataTable();
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_43B9F642",
                "99999999",
                "99999999",
                "",
                "9999",
                "9999",
                ""
                );

            dt = this.DbConnector.ExecuteDataTable();
            this.FPS91_TY_S_AC_43B9G643.SetValue(dt);
        }
        #endregion

        #region Description : 스프레드 타이틀
        private void UP_Spread_Title()
        {
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_43B9G643_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_43B9G643_Sheet1.AddColumnHeaderSpanCell(0, 0,  1, 3);
            this.FPS91_TY_S_AC_43B9G643_Sheet1.AddColumnHeaderSpanCell(0, 7,  1, 2);
            this.FPS91_TY_S_AC_43B9G643_Sheet1.AddColumnHeaderSpanCell(0, 9,  1, 2);
            this.FPS91_TY_S_AC_43B9G643_Sheet1.AddColumnHeaderSpanCell(0, 11, 1, 2);

            this.FPS91_TY_S_AC_43B9G643_Sheet1.AddColumnHeaderSpanCell(0, 3,  2, 1);
            this.FPS91_TY_S_AC_43B9G643_Sheet1.AddColumnHeaderSpanCell(0, 4,  2, 1);
            this.FPS91_TY_S_AC_43B9G643_Sheet1.AddColumnHeaderSpanCell(0, 6,  2, 1);
            this.FPS91_TY_S_AC_43B9G643_Sheet1.AddColumnHeaderSpanCell(0, 13, 2, 1);
            this.FPS91_TY_S_AC_43B9G643_Sheet1.AddColumnHeaderSpanCell(0, 14, 2, 1);
            this.FPS91_TY_S_AC_43B9G643_Sheet1.AddColumnHeaderSpanCell(0, 15, 2, 1);

            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[0, 0].Value  = "관리번호";
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[0, 3].Value  = "계정과목";

            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[0, 4].Value  = "거 래 처";
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[0, 6].Value  = "거래처명";
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[0, 7].Value  = "취    득";
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[0, 9].Value  = "감    소";
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[0, 11].Value = "장    부";
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[0, 13].Value = "총발행주식";
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[0, 14].Value = "지 분 율";
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[0, 15].Value = "적    요";

            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[1, 0].Value  = "자산구분";
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[1, 1].Value  = "년  도";
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[1, 2].Value  = "순  번";
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[1, 3].Value  = "";
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[1, 4].Value  = "";
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[1, 5].Value  = "";
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[1, 6].Value  = "";
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[1, 7].Value  = "주 식 수";
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[1, 8].Value  = "금  액";
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[1, 9].Value  = "주 식 수";
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[1, 10].Value = "금  액";
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[1, 11].Value = "주 식 수";
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[1, 12].Value = "금  액";
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[1, 13].Value = "";
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[1, 14].Value = "";
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[1, 15].Value = "";

            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[0, 3].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[0, 9].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[0, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[0, 13].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[0, 14].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[0, 15].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[1, 0].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[1, 1].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[1, 2].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[1, 7].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[1, 8].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[1, 9].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[1, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[1, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43B9G643_Sheet1.ColumnHeader.Cells[1, 12].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            if (this.CBO01_GGUBUN.GetValue().ToString() == "1")
            {
                this.DbConnector.Attach
                    (
                    "TY_P_AC_43B9F642",
                    this.TXT01_STDATE.GetValue().ToString(),
                    this.TXT01_EDDATE.GetValue().ToString(),
                    this.CBO01_GCDAC.GetValue().ToString(),
                    this.TXT01_STDATE.GetValue().ToString().Substring(0, 4),
                    this.TXT01_EDDATE.GetValue().ToString().Substring(0, 4),
                    this.CBO01_GCDAC.GetValue().ToString()
                    );
            }
            else
            {
                this.DbConnector.Attach
                    (
                    "TY_P_AC_43D2H717",
                    this.TXT01_GDATE.GetValue().ToString(),
                    this.CBO01_GCDAC.GetValue().ToString(),
                    this.TXT01_GDATE.GetValue().ToString().Substring(0, 4),
                    this.CBO01_GCDAC.GetValue().ToString()
                    );
            }

            dt = this.DbConnector.ExecuteDataTable();
            this.FPS91_TY_S_AC_43B9G643.SetValue(dt);

            this.FPS91_TY_S_AC_43B30659.Initialize();

            this.CBO02_ADMANGUBN.SetValue("");
            this.TXT02_ADMANYEAR.SetValue("");
            this.TXT02_ADMANSEQ.SetValue("");
        }
        #endregion

        #region Description : 투자주식 MAST 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            int i = 0;
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // 순번 부여
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_43BAP649", ds.Tables[0].Rows[i]["AAMANGUBN"].ToString(),
                                                            ds.Tables[0].Rows[i]["AAMANYEAR"].ToString());

                ds.Tables[0].Rows[i]["AAMANSEQ"] = this.DbConnector.ExecuteScalar();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_43BAS650", ds.Tables[0].Rows[i]["AAMANGUBN"].ToString(),
                                                            ds.Tables[0].Rows[i]["AAMANYEAR"].ToString(),
                                                            ds.Tables[0].Rows[i]["AAMANSEQ"].ToString(),
                                                            ds.Tables[0].Rows[i]["AACNCDAC"].ToString(),
                                                            ds.Tables[0].Rows[i]["AACUSTCD"].ToString(),
                                                            ds.Tables[0].Rows[i]["AACUSTNM"].ToString(),
                                                            ds.Tables[0].Rows[i]["AACNRKAC"].ToString()
                                                            );

                this.DbConnector.ExecuteTranQueryList();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();

                for (i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_43BAT651", ds.Tables[1].Rows[i]["AACNCDAC"].ToString(),
                                                                ds.Tables[1].Rows[i]["AACUSTCD"].ToString(),
                                                                ds.Tables[1].Rows[i]["AACUSTNM"].ToString(),
                                                                ds.Tables[1].Rows[i]["AACNRKAC"].ToString(),
                                                                ds.Tables[1].Rows[i]["AAMANGUBN"].ToString(),
                                                                ds.Tables[1].Rows[i]["AAMANYEAR"].ToString(),
                                                                ds.Tables[1].Rows[i]["AAMANSEQ"].ToString()
                                                                );
                }

                this.DbConnector.ExecuteTranQueryList();
            }

            this.ShowMessage("TY_M_GB_23NAD873");
            this.BTN61_INQ_Click(null, null);

            this.FPS91_TY_S_AC_43B30659.Initialize();

            this.CBO02_ADMANGUBN.SetValue("");
            this.TXT02_ADMANYEAR.SetValue("");
            this.TXT02_ADMANSEQ.SetValue("");
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
                this.DbConnector.Attach("TY_P_AC_43BAT652", dt.Rows[i]["AAMANGUBN"].ToString(),
                                                            dt.Rows[i]["AAMANYEAR"].ToString(),
                                                            dt.Rows[i]["AAMANSEQ"].ToString());
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지
            this.BTN61_INQ_Click(null, null);

            this.FPS91_TY_S_AC_43B30659.Initialize();

            this.CBO02_ADMANGUBN.SetValue("");
            this.TXT02_ADMANYEAR.SetValue("");
            this.TXT02_ADMANSEQ.SetValue("");
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_43DA9702",
                this.TXT01_GDATE.GetValue().ToString(),
                this.TXT01_GDATE.GetValue().ToString(),
                this.TXT01_GDATE.GetValue().ToString(),
                this.TXT01_GDATE.GetValue().ToString(),
                this.TXT01_GDATE.GetValue().ToString(),
                this.TXT01_GDATE.GetValue().ToString().Substring(0,4),
                this.TXT01_GDATE.GetValue().ToString().Substring(0,4),
                this.TXT01_GDATE.GetValue().ToString().Substring(0,4),
                this.TXT01_GDATE.GetValue().ToString().Substring(0,4),
                this.CBO01_GCDAC.GetValue().ToString(),
                this.TXT01_GDATE.GetValue().ToString(),
                this.TXT01_GDATE.GetValue().ToString(),
                this.TXT01_GDATE.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYACSE002R1();

                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_43DBD708",
                this.TXT01_GDATE.GetValue().ToString(),
                this.TXT01_GDATE.GetValue().ToString(),
                this.TXT01_GDATE.GetValue().ToString(),
                this.TXT01_GDATE.GetValue().ToString(),
                this.TXT01_GDATE.GetValue().ToString(),
                this.CBO01_GCDAC.GetValue().ToString(),
                this.TXT01_GDATE.GetValue().ToString(),
                this.TXT01_GDATE.GetValue().ToString(),
                this.TXT01_GDATE.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYACSE002R2();

                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
        }
        #endregion

        #region Description : 투자주식 가감내역 저장 버튼
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
                    this.DbConnector.Attach("TY_P_AC_43B34661", this.CBO02_ADMANGUBN.GetValue().ToString(),
                                                                this.TXT02_ADMANYEAR.GetValue().ToString(),
                                                                this.TXT02_ADMANSEQ.GetValue().ToString(),
                                                                ds.Tables[0].Rows[i]["ADDATEJUST"].ToString(),
                                                                ds.Tables[0].Rows[i]["ADNUMBJUST"].ToString(),
                                                                ds.Tables[0].Rows[i]["ADINCGUBN"].ToString(),
                                                                ds.Tables[0].Rows[i]["ADJUSTSTOK"].ToString(),
                                                                ds.Tables[0].Rows[i]["ADJUSTPRIC"].ToString(),
                                                                ds.Tables[0].Rows[i]["ADLEDJUAMT"].ToString(),
                                                                ds.Tables[0].Rows[i]["ADSTOKCNT"].ToString(),
                                                                ds.Tables[0].Rows[i]["ADSELAMT"].ToString(),
                                                                ds.Tables[0].Rows[i]["ADINCSAYU"].ToString()
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
                    this.DbConnector.Attach("TY_P_AC_43B35662", ds.Tables[1].Rows[i]["ADINCGUBN"].ToString(),
                                                                ds.Tables[1].Rows[i]["ADJUSTSTOK"].ToString(),
                                                                ds.Tables[1].Rows[i]["ADJUSTPRIC"].ToString(),
                                                                ds.Tables[1].Rows[i]["ADLEDJUAMT"].ToString(),
                                                                ds.Tables[1].Rows[i]["ADSTOKCNT"].ToString(),
                                                                ds.Tables[1].Rows[i]["ADSELAMT"].ToString(),
                                                                ds.Tables[1].Rows[i]["ADINCSAYU"].ToString(),
                                                                this.CBO02_ADMANGUBN.GetValue().ToString(),
                                                                this.TXT02_ADMANYEAR.GetValue().ToString(),
                                                                this.TXT02_ADMANSEQ.GetValue().ToString(),
                                                                ds.Tables[1].Rows[i]["ADDATEJUST"].ToString(),
                                                                ds.Tables[1].Rows[i]["ADNUMBJUST"].ToString()
                                                                );
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            string sOUTMSG = string.Empty;

            // 투자주식 MAST 취득, 감소 업데이트
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_43C8Q669",
                this.CBO02_ADMANGUBN.GetValue().ToString(),
                this.TXT02_ADMANYEAR.GetValue().ToString(),
                this.TXT02_ADMANSEQ.GetValue().ToString(),
                sOUTMSG.ToString()
                );

            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUTMSG.ToString() == "OK")
            {
                this.ShowMessage("TY_M_GB_23NAD873");
                this.BTN61_INQ_Click(null, null);

                this.CBO02_ADMANGUBN.SetValue("");
                this.TXT02_ADMANYEAR.SetValue("");
                this.TXT02_ADMANSEQ.SetValue("");
            }
            else
            {
                this.ShowMessage("TY_M_AC_246A2488");
            }
        }
        #endregion

        #region Description : 투자주식 가감내역 삭제 버튼
        private void BTN62_REM_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            for (i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_43B35663", this.CBO02_ADMANGUBN.GetValue().ToString(),
                                                            this.TXT02_ADMANYEAR.GetValue().ToString(),
                                                            this.TXT02_ADMANSEQ.GetValue().ToString(),
                                                            dt.Rows[i]["ADDATEJUST"].ToString(),
                                                            dt.Rows[i]["ADNUMBJUST"].ToString());
            }
            this.DbConnector.ExecuteTranQueryList();

            string sOUTMSG = string.Empty;

            // 투자주식 MAST 취득, 감소 업데이트
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_43C8Q669",
                this.CBO02_ADMANGUBN.GetValue().ToString(),
                this.TXT02_ADMANYEAR.GetValue().ToString(),
                this.TXT02_ADMANSEQ.GetValue().ToString(),
                sOUTMSG.ToString()
                );

            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUTMSG.ToString() == "OK")
            {
                this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지
                this.BTN61_INQ_Click(null, null);

                this.CBO02_ADMANGUBN.SetValue("");
                this.TXT02_ADMANYEAR.SetValue("");
                this.TXT02_ADMANSEQ.SetValue("");
            }
            else
            {
                this.ShowMessage("TY_M_GB_43C9G671"); // 삭제 메세지
            }
        }
        #endregion

        #region Description : 투자주식 MAST 조회 ProcessCheck 이벤트
        private void BTN61_INQ_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.CBO01_GGUBUN.GetValue().ToString() == "1")
            {
                if (this.TXT01_STDATE.GetValue().ToString() == "")
                {
                    SetFocus(TXT01_STDATE);
                    this.ShowMessage("TY_M_AC_4372A599");
                    e.Successed = false;
                    return;
                }

                if (this.TXT01_EDDATE.GetValue().ToString() == "")
                {
                    SetFocus(TXT01_EDDATE);
                    this.ShowMessage("TY_M_AC_4372A599");
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (this.TXT01_GDATE.GetValue().ToString() == "")
                {
                    SetFocus(TXT01_GDATE);
                    this.ShowMessage("TY_M_AC_4372A600");
                    e.Successed = false;
                    return;
                }
            }
        }
        #endregion

        #region Description : 투자주식 MAST 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataSet ds = new DataSet();

            // 등록 및 수정을 동시에 할 경우
            // 등록이 먼저이면 Tables[0]
            // 수정이 나중이면 Tables[1]임
            // 저장
            ds.Tables.Add(this.FPS91_TY_S_AC_43B9G643.GetDataSourceInclude(TSpread.TActionType.New,    "AAMANGUBN", "AAMANYEAR", "AAMANSEQ", "AACNCDAC", "AACUSTCD", "AACUSTNM", "AACNRKAC"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_43B9G643.GetDataSourceInclude(TSpread.TActionType.Update, "AAMANGUBN", "AAMANYEAR", "AAMANSEQ", "AACNCDAC", "AACUSTCD", "AACUSTNM", "AACNRKAC"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
                e.Successed = false;
                return;
            }

            DataTable dt = new DataTable();

            // 신규순번 부여
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //// 순번 부여
                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach("TY_P_AC_43BAP649", ds.Tables[0].Rows[i]["AAMANGUBN"].ToString(),
                //                                            ds.Tables[0].Rows[i]["AAMANYEAR"].ToString());

                //ds.Tables[0].Rows[i]["AAMANSEQ"] = this.DbConnector.ExecuteScalar();

                // 거래처명
                if (ds.Tables[0].Rows[i]["AACUSTNM"].ToString() == "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_2445D438", ds.Tables[0].Rows[i]["AACUSTCD"].ToString());

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        ds.Tables[0].Rows[i]["AACUSTNM"] = dt.Rows[0]["VNSANGHO"].ToString();
                    }
                }
            }

            for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                // 거래처명
                if (ds.Tables[1].Rows[i]["AACUSTNM"].ToString() == "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_2445D438", ds.Tables[1].Rows[i]["AACUSTCD"].ToString());

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        ds.Tables[1].Rows[i]["AACUSTNM"] = dt.Rows[0]["VNSANGHO"].ToString();
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

        #region Description : 투자주식 MAST 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataTable dt = this.FPS91_TY_S_AC_43B9G643.GetDataSourceInclude(TSpread.TActionType.Remove, "AAMANGUBN", "AAMANYEAR", "AAMANSEQ");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            DataTable dt1 = new DataTable();

            for (i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_43BAZ653",
                    dt.Rows[i]["AAMANGUBN"].ToString(),
                    dt.Rows[i]["AAMANYEAR"].ToString(),
                    dt.Rows[i]["AAMANSEQ"].ToString()
                    );

                dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_43BB4654");
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

        #region Description : 투자주식 가감내역 저장 ProcessCheck 이벤트
        private void BTN62_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_43B30659.GetDataSourceInclude(TSpread.TActionType.New,    "ADDATEJUST", "ADNUMBJUST", "ADINCGUBN", "ADJUSTSTOK", "ADJUSTPRIC", "ADLEDJUAMT", "ADSTOKCNT", "ADSELAMT", "ADINCSAYU"));
            ds.Tables.Add(this.FPS91_TY_S_AC_43B30659.GetDataSourceInclude(TSpread.TActionType.Update, "ADDATEJUST", "ADNUMBJUST", "ADINCGUBN", "ADJUSTSTOK", "ADJUSTPRIC", "ADLEDJUAMT", "ADSTOKCNT", "ADSELAMT", "ADINCSAYU"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }
            else
            {
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_43B3E664",
                    this.CBO02_ADMANGUBN.GetValue().ToString(),
                    this.TXT02_ADMANYEAR.GetValue().ToString(),
                    this.TXT02_ADMANSEQ.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_AC_43B3H665");
                    e.Successed = false;
                    return;
                }

                // 등록 체크
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    // 순번 부여
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_43B54667", this.CBO02_ADMANGUBN.GetValue().ToString(),
                                                                this.TXT02_ADMANYEAR.GetValue().ToString(),
                                                                this.TXT02_ADMANSEQ.GetValue().ToString(),
                                                                ds.Tables[0].Rows[i]["ADDATEJUST"].ToString());

                    ds.Tables[0].Rows[i]["ADNUMBJUST"] = this.DbConnector.ExecuteScalar();

                    if(double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["ADJUSTSTOK"].ToString())) == 0 && 
                       double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["ADJUSTPRIC"].ToString())) == 0 &&
                       double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["ADLEDJUAMT"].ToString())) == 0 &&
                       double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["ADSTOKCNT"].ToString())) == 0)
                    {
                        this.ShowMessage("TY_M_AC_43CA2673");
                        e.Successed = false;
                        return;
                    }

                    // 주식수
                    if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["ADJUSTSTOK"].ToString())) != 0)
                    {
                        // 단가
                        if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["ADJUSTPRIC"].ToString())) == 0)
                        {
                            this.ShowMessage("TY_M_AC_43CAA674");
                            e.Successed = false;
                            return;
                        }

                        // 장부증감금액
                        if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["ADLEDJUAMT"].ToString())) == 0)
                        {
                            this.ShowMessage("TY_M_AC_43CAB675");
                            e.Successed = false;
                            return;
                        }

                        // 총발행주식수
                        if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["ADSTOKCNT"].ToString())) == 0)
                        {
                            this.ShowMessage("TY_M_AC_43CAB676");
                            e.Successed = false;
                            return;
                        }

                        if (ds.Tables[0].Rows[i]["ADINCGUBN"].ToString() == "2")
                        {
                            // 처분금액
                            if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["ADSELAMT"].ToString())) == 0)
                            {
                                this.ShowMessage("TY_M_AC_43CAD677");
                                e.Successed = false;
                                return;
                            }
                        }
                    }

                    // 처분금액
                    if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["ADSELAMT"].ToString())) != 0)
                    {
                        if (ds.Tables[0].Rows[i]["ADINCGUBN"].ToString() == "1")
                        {
                            this.ShowMessage("TY_M_AC_43CAD678");
                            e.Successed = false;
                            return;
                        }
                    }
                }

                // 수정 체크
                for (i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    if (double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["ADJUSTSTOK"].ToString())) == 0 &&
                        double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["ADJUSTPRIC"].ToString())) == 0 &&
                        double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["ADLEDJUAMT"].ToString())) == 0 &&
                        double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["ADSTOKCNT"].ToString())) == 0)
                    {
                        this.ShowMessage("TY_M_AC_43CA2673");
                        e.Successed = false;
                        return;
                    }

                    // 주식수
                    if (double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["ADJUSTSTOK"].ToString())) != 0)
                    {
                        // 단가
                        if (double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["ADJUSTPRIC"].ToString())) == 0)
                        {
                            this.ShowMessage("TY_M_AC_43CAA674");
                            e.Successed = false;
                            return;
                        }

                        // 장부증감금액
                        if (double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["ADLEDJUAMT"].ToString())) == 0)
                        {
                            this.ShowMessage("TY_M_AC_43CAB675");
                            e.Successed = false;
                            return;
                        }

                        // 총발행주식수
                        if (double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["ADSTOKCNT"].ToString())) == 0)
                        {
                            this.ShowMessage("TY_M_AC_43CAB676");
                            e.Successed = false;
                            return;
                        }

                        if (ds.Tables[1].Rows[i]["ADINCGUBN"].ToString() == "2")
                        {
                            // 처분금액
                            if (double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["ADSELAMT"].ToString())) == 0)
                            {
                                this.ShowMessage("TY_M_AC_43CAD677");
                                e.Successed = false;
                                return;
                            }
                        }
                    }

                    // 처분금액
                    if (double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["ADSELAMT"].ToString())) != 0)
                    {
                        if (ds.Tables[1].Rows[i]["ADINCGUBN"].ToString() == "1")
                        {
                            this.ShowMessage("TY_M_AC_43CAD678");
                            e.Successed = false;
                            return;
                        }
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

        #region Description : 투자주식 가감내역 삭제 ProcessCheck 이벤트
        private void BTN62_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataTable dt = this.FPS91_TY_S_AC_43B30659.GetDataSourceInclude(TSpread.TActionType.Remove, "ADDATEJUST", "ADNUMBJUST");

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
                "TY_P_AC_43B3E664",
                this.CBO02_ADMANGUBN.GetValue().ToString(),
                this.TXT02_ADMANYEAR.GetValue().ToString(),
                this.TXT02_ADMANSEQ.GetValue().ToString()
                );

            dt1 = this.DbConnector.ExecuteDataTable();

            if (dt1.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_AC_43B3H665");
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
            this.CBO02_ADMANGUBN.SetReadOnly(bTrueAndFalse);
            this.TXT02_ADMANYEAR.SetReadOnly(bTrueAndFalse);
            this.TXT02_ADMANSEQ.SetReadOnly(bTrueAndFalse);
        }
        #endregion

        #region Description : 저장품 MAST 스프레드 클릭 이벤트
        private void FPS91_TY_S_AC_43B9G643_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.CBO02_ADMANGUBN.SetValue(this.FPS91_TY_S_AC_43B9G643.GetValue("AAMANGUBN").ToString());
            this.TXT02_ADMANYEAR.SetValue(this.FPS91_TY_S_AC_43B9G643.GetValue("AAMANYEAR").ToString());
            this.TXT02_ADMANSEQ.SetValue(this.FPS91_TY_S_AC_43B9G643.GetValue("AAMANSEQ").ToString());
            
            UP_SetReadOnly(true);

            UP_Set_Detail();
        }
        #endregion

        #region Description : 투자주식 가감내역 조회
        private void UP_Set_Detail()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_43BAZ653",
                this.CBO02_ADMANGUBN.GetValue().ToString(),
                this.TXT02_ADMANYEAR.GetValue().ToString(),
                this.TXT02_ADMANSEQ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_43B30659.SetValue(dt);

            //if (this.FPS91_TY_S_AC_43B30659.CurrentRowCount > 0)
            //{
            //    this.SpreadSumRowAdd(this.FPS91_TY_S_AC_43B30659, "AISQDATE", "합 계", SumRowType.Total, "AISISSAMT");
            //}
        }
        #endregion

        #region Description : 포커스
        private void TXT01_AESQDATE_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.BTN61_INQ);
            }
        }
        #endregion

        #region Description : 투자주식 스프레드 이벤트
        private void FPS91_TY_S_AC_43B9G643_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            if (e.Column == 0)
            {
                if (this.FPS91_TY_S_AC_43B9G643.GetValue(e.Row, "AAMANGUBN").ToString() == "1")
                {
                    this.FPS91_TY_S_AC_43B9G643.SetValue(e.Row, "AACNCDAC", "12100500");
                }
                else if (this.FPS91_TY_S_AC_43B9G643.GetValue(e.Row, "AAMANGUBN").ToString() == "2")
                {
                    this.FPS91_TY_S_AC_43B9G643.SetValue(e.Row, "AACNCDAC", "12100400");
                }
                //else if (this.FPS91_TY_S_AC_43B9G643.GetValue(e.Row, "AAMANGUBN").ToString() == "3")
                //{
                //    this.FPS91_TY_S_AC_43B9G643.SetValue(e.Row, "AACNCDAC", "12100200");
                //}
                else if (this.FPS91_TY_S_AC_43B9G643.GetValue(e.Row, "AAMANGUBN").ToString() == "4")
                {
                    this.FPS91_TY_S_AC_43B9G643.SetValue(e.Row, "AACNCDAC", "12100203");
                }
            }

            //if (e.Column == 4)
            //{
            //    this.FPS91_TY_S_AC_43B9G643_Sheet1.Cells[e.Row, 6].Value = this.FPS91_TY_S_AC_43B9G643_Sheet1.Cells[e.Row, 5].Text.ToString();
            //}
        }

        private void FPS91_TY_S_AC_43B9G643_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            if (e.Column == 4)
            {
                if (this.FPS91_TY_S_AC_43B9G643_Sheet1.Cells[e.Row, 5].Text.ToString() != "")
                {
                    this.FPS91_TY_S_AC_43B9G643_Sheet1.Cells[e.Row, 6].Value = this.FPS91_TY_S_AC_43B9G643_Sheet1.Cells[e.Row, 5].Text.ToString();
                }
            }
        }
        #endregion

        //private void FPS91_TY_S_AC_43B9G643_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        //{
        //    if (e.KeyCode == System.Windows.Forms.Keys.F1 && FPS91_TY_S_AC_43B9G643.ActiveSheet.ActiveColumnIndex == 4)
        //    {
        //        this.FPS91_TY_S_AC_43B9G643_Sheet1.Cells[FPS91_TY_S_AC_43B9G643.ActiveSheet.ActiveRowIndex, 6].Value = this.FPS91_TY_S_AC_43B9G643_Sheet1.Cells[FPS91_TY_S_AC_43B9G643.ActiveSheet.ActiveRowIndex, 5].Text.ToString();
        //    }
        //}
    }
}