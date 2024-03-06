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
    public partial class TYACSE001I : TYBase
    {
        private string fsAESQDATE = string.Empty;

        #region Description : 페이지 로드
        public TYACSE001I()
        {
            InitializeComponent();

            // 스프레드에서 코드헬프 사용
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_43653585, "AEMANCDDP", "AEMANCDDPNM", "AEMANCDDP");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_43653585, "AESPURC",   "VNSANGHO",    "AESPURC");

            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_4375A603, "AISCOCDAC", "AISCOCDACNM", "AISCOCDAC");
        }

        private void TYACSE001I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_43653585, "AEMANYEAR");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_43653585, "AEMANCDDP");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_43653585, "AEMANSEQ");

            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_4375A603, "AISQDATE");

            this.BTN61_INQ.ProcessCheck += new TButton.CheckHandler(BTN61_INQ_ProcessCheck);

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.BTN62_SAV.ProcessCheck += new TButton.CheckHandler(BTN62_SAV_ProcessCheck);
            this.BTN62_REM.ProcessCheck += new TButton.CheckHandler(BTN62_REM_ProcessCheck);

            this.CBH02_AIMANCDDP.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            UP_SetReadOnly(false);

            // Blank Row 생성
            //UP_Row_Ins();

            SetStartingFocus(this.CBO01_GCDAC);

            this.FPS91_TY_S_AC_43653585.Initialize();
        }
        #endregion

        #region Description : 빈 ROW 생성
        private void UP_Row_Ins()
        {
            DataTable dt = new DataTable();
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_4365D586",
                this.CBO01_GCDAC.GetValue().ToString(),
                "1",
                "99999999",
                "99999999",
                ""
                );

            dt = this.DbConnector.ExecuteDataTable();
            this.FPS91_TY_S_AC_43653585.SetValue(dt);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_4365D586",
                this.CBO01_GCDAC.GetValue().ToString(),
                this.CBO01_GGUBUN.GetValue().ToString(),
                this.TXT01_STDATE.GetValue().ToString(),
                this.TXT01_EDDATE.GetValue().ToString(),
                this.TXT01_AESQDATE.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_43653585.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                int i = 0;

                this.SpreadSumRowAdd(this.FPS91_TY_S_AC_43653585, "AEMANCDDP", "합 계", Color.Yellow);
                for (i = 0; i < this.FPS91_TY_S_AC_43653585.ActiveSheet.RowCount; i++)
                {
                    this.FPS91_TY_S_AC_43653585_Sheet1.SetFormula(
                        i, // row
                        12, // column
                        "R[0]C[-2] - R[0]C[-1]"); // 합계
                }

                this.FPS91_TY_S_AC_43653585.ActiveSheet.Rows[i - 1].Visible = false;
            }

            this.FPS91_TY_S_AC_4375A603.Initialize();

            this.TXT02_AIMANYEAR.SetValue("");
            this.CBH02_AIMANCDDP.SetValue("");
            this.TXT02_AIMANSEQ.SetValue("");
        }
        #endregion

        #region Description : 저장품 MAST 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            int i = 0;
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_4379T594", ds.Tables[0].Rows[i]["AEMANYEAR"].ToString(),
                                                            ds.Tables[0].Rows[i]["AEMANCDDP"].ToString());

                ds.Tables[0].Rows[i]["AEMANSEQ"] = this.DbConnector.ExecuteScalar();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_43714595", ds.Tables[0].Rows[i]["AEMANYEAR"].ToString(),
                                                            ds.Tables[0].Rows[i]["AEMANCDDP"].ToString(),
                                                            ds.Tables[0].Rows[i]["AEMANSEQ"].ToString(),
                                                            ds.Tables[0].Rows[i]["AESCDAC"].ToString(),
                                                            ds.Tables[0].Rows[i]["AESNAME"].ToString(),
                                                            ds.Tables[0].Rows[i]["AESQDATE"].ToString(),
                                                            ds.Tables[0].Rows[i]["AESPURC"].ToString(),
                                                            ds.Tables[0].Rows[i]["AESUNIT"].ToString(),
                                                            ds.Tables[0].Rows[i]["AESPURAMT"].ToString(),
                                                            ds.Tables[0].Rows[i]["AESISSAMT"].ToString(),
                                                            (
                                                            Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i]["AESPURAMT"].ToString())) - 
                                                            Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i]["AESISSAMT"].ToString()))
                                                            )
                                                            );
                this.DbConnector.ExecuteTranQueryList();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();

                for (i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_4371B597", ds.Tables[1].Rows[i]["AESCDAC"].ToString(),
                                                                ds.Tables[1].Rows[i]["AESNAME"].ToString(),
                                                                ds.Tables[1].Rows[i]["AESQDATE"].ToString(),
                                                                ds.Tables[1].Rows[i]["AESPURC"].ToString(),
                                                                ds.Tables[1].Rows[i]["AESUNIT"].ToString(),
                                                                ds.Tables[1].Rows[i]["AESPURAMT"].ToString(),
                                                                ds.Tables[1].Rows[i]["AESISSAMT"].ToString(),
                                                                ds.Tables[1].Rows[i]["AESBALAMT"].ToString(),
                                                                ds.Tables[1].Rows[i]["AEMANYEAR"].ToString(),
                                                                ds.Tables[1].Rows[i]["AEMANCDDP"].ToString(),
                                                                ds.Tables[1].Rows[i]["AEMANSEQ"].ToString()
                                                                );
                }

                this.DbConnector.ExecuteTranQueryList();
            }

            this.ShowMessage("TY_M_GB_23NAD873");
            this.BTN61_INQ_Click(null, null);

            this.FPS91_TY_S_AC_4375A603.Initialize();

            this.TXT02_AIMANYEAR.SetValue("");
            this.CBH02_AIMANCDDP.SetValue("");
            this.TXT02_AIMANSEQ.SetValue("");
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
                this.DbConnector.Attach("TY_P_AC_4371A596", dt.Rows[i]["AEMANYEAR"].ToString(),
                                                            dt.Rows[i]["AEMANCDDP"].ToString(),
                                                            dt.Rows[i]["AEMANSEQ"].ToString());
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지
            this.BTN61_INQ_Click(null, null);

            this.FPS91_TY_S_AC_4375A603.Initialize();

            this.TXT02_AIMANYEAR.SetValue("");
            this.CBH02_AIMANCDDP.SetValue("");
            this.TXT02_AIMANSEQ.SetValue("");
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sDATE = string.Empty;

            sDATE = this.TXT01_AESQDATE.GetValue().ToString().Substring(0,4) + "년  " + this.TXT01_AESQDATE.GetValue().ToString().Substring(4,2) + "월  " + this.TXT01_AESQDATE.GetValue().ToString().Substring(6,2) + "일  현재";

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_43A1E629",
                this.TXT01_AESQDATE.GetValue().ToString(),
                this.TXT01_AESQDATE.GetValue().ToString(),
                this.CBO01_GCDAC.GetValue().ToString(),
                this.TXT01_AESQDATE.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYACSE001R();

                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
        }
        #endregion

        #region Description : 저장품 불출내역 저장 버튼
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
                    this.DbConnector.Attach("TY_P_AC_4375M604", this.TXT02_AIMANYEAR.GetValue().ToString(),
                                                                this.CBH02_AIMANCDDP.GetValue().ToString(),
                                                                this.TXT02_AIMANSEQ.GetValue().ToString(),
                                                                ds.Tables[0].Rows[i]["AISQDATE"].ToString(),
                                                                ds.Tables[0].Rows[i]["AISISSAMT"].ToString(),
                                                                ds.Tables[0].Rows[i]["AISCOCDAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["AISUNIT"].ToString()
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
                    this.DbConnector.Attach("TY_P_AC_43A94621", ds.Tables[1].Rows[i]["AISISSAMT"].ToString(),
                                                                ds.Tables[1].Rows[i]["AISCOCDAC"].ToString(),
                                                                ds.Tables[1].Rows[i]["AISUNIT"].ToString(),
                                                                this.TXT02_AIMANYEAR.GetValue().ToString(),
                                                                this.CBH02_AIMANCDDP.GetValue().ToString(),
                                                                this.TXT02_AIMANSEQ.GetValue().ToString(),
                                                                ds.Tables[1].Rows[i]["AISQDATE"].ToString()
                                                                );
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            // 저장품 MAST 불출금액 업데이트
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_43A97618", this.TXT02_AIMANYEAR.GetValue().ToString(),
                                                        this.CBH02_AIMANCDDP.GetValue().ToString(),
                                                        this.TXT02_AIMANSEQ.GetValue().ToString(),
                                                        this.TXT02_AIMANYEAR.GetValue().ToString(),
                                                        this.CBH02_AIMANCDDP.GetValue().ToString(),
                                                        this.TXT02_AIMANSEQ.GetValue().ToString(),
                                                        this.TXT02_AIMANYEAR.GetValue().ToString(),
                                                        this.CBH02_AIMANCDDP.GetValue().ToString(),
                                                        this.TXT02_AIMANSEQ.GetValue().ToString()
                                                        );
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");
            this.BTN61_INQ_Click(null, null);

            this.TXT02_AIMANYEAR.SetValue("");
            this.CBH02_AIMANCDDP.SetValue("");
            this.TXT02_AIMANSEQ.SetValue("");
        }
        #endregion

        #region Description : 저장품 불출내역 삭제 버튼
        private void BTN62_REM_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            for (i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_4375M605", this.TXT02_AIMANYEAR.GetValue().ToString(),
                                                            this.CBH02_AIMANCDDP.GetValue().ToString(),
                                                            this.TXT02_AIMANSEQ.GetValue().ToString(),
                                                            dt.Rows[i]["AISQDATE"].ToString());
            }
            this.DbConnector.ExecuteTranQueryList();

            // 저장품 MAST 불출금액 업데이트
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_43A97618", this.TXT02_AIMANYEAR.GetValue().ToString(),
                                                        this.CBH02_AIMANCDDP.GetValue().ToString(),
                                                        this.TXT02_AIMANSEQ.GetValue().ToString(),
                                                        this.TXT02_AIMANYEAR.GetValue().ToString(),
                                                        this.CBH02_AIMANCDDP.GetValue().ToString(),
                                                        this.TXT02_AIMANSEQ.GetValue().ToString(),
                                                        this.TXT02_AIMANYEAR.GetValue().ToString(),
                                                        this.CBH02_AIMANCDDP.GetValue().ToString(),
                                                        this.TXT02_AIMANSEQ.GetValue().ToString()
                                                        );
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지
            this.BTN61_INQ_Click(null, null);

            this.TXT02_AIMANYEAR.SetValue("");
            this.CBH02_AIMANCDDP.SetValue("");
            this.TXT02_AIMANSEQ.SetValue("");
        }
        #endregion

        #region Description : 저장품 MAST 조회 ProcessCheck 이벤트
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
                if (this.TXT01_AESQDATE.GetValue().ToString() == "")
                {
                    SetFocus(TXT01_AESQDATE);
                    this.ShowMessage("TY_M_AC_4372A600");
                    e.Successed = false;
                    return;
                }
            }
        }
        #endregion

        #region Description : 저장품 MAST 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataSet ds = new DataSet();

            // 등록 및 수정을 동시에 할 경우
            // 등록이 먼저이면 Tables[0]
            // 수정이 나중이면 Tables[1]임
            // 저장
            ds.Tables.Add(this.FPS91_TY_S_AC_43653585.GetDataSourceInclude(TSpread.TActionType.New,    "AEMANYEAR", "AEMANCDDP", "AEMANSEQ", "AESCDAC", "AESNAME", "AESQDATE", "AESPURC", "AESUNIT", "AESPURAMT", "AESISSAMT", "AESBALAMT"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_43653585.GetDataSourceInclude(TSpread.TActionType.Update, "AEMANYEAR", "AEMANCDDP", "AEMANSEQ", "AESCDAC", "AESNAME", "AESQDATE", "AESPURC", "AESUNIT", "AESPURAMT", "AESISSAMT", "AESBALAMT"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            //// 신규순번 부여
            //for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //    // 순번 부여
            //    this.DbConnector.CommandClear();
            //    this.DbConnector.Attach("TY_P_AC_4379T594", ds.Tables[0].Rows[i]["AEMANYEAR"].ToString(),
            //                                                ds.Tables[0].Rows[i]["AEMANCDDP"].ToString());

            //    ds.Tables[0].Rows[i]["AEMANSEQ"] = this.DbConnector.ExecuteScalar();
            //}

            e.ArgData = ds;
        }
        #endregion

        #region Description : 저장품 MAST 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataTable dt = this.FPS91_TY_S_AC_43653585.GetDataSourceInclude(TSpread.TActionType.Remove, "AEMANYEAR", "AEMANCDDP", "AEMANSEQ", "AESISSAMT");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            for (i = 0; i < dt.Rows.Count; i++)
            {
                if (double.Parse(Get_Numeric(dt.Rows[i]["AESISSAMT"].ToString())) != 0)
                {
                    this.ShowMessage("TY_M_AC_43759602");
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

        #region Description : 저장품 불출내역 저장 ProcessCheck 이벤트
        private void BTN62_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_4375A603.GetDataSourceInclude(TSpread.TActionType.New, "AISQDATE", "AISISSAMT", "AISCOCDAC", "AISUNIT"));
            ds.Tables.Add(this.FPS91_TY_S_AC_4375A603.GetDataSourceInclude(TSpread.TActionType.Update, "AISQDATE", "AISISSAMT", "AISCOCDAC", "AISUNIT"));

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
                    "TY_P_AC_43A8V614",
                    this.TXT02_AIMANYEAR.GetValue().ToString(),
                    this.CBH02_AIMANCDDP.GetValue().ToString(),
                    this.TXT02_AIMANSEQ.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_AC_43A8Y615");
                    e.Successed = false;
                    return;
                }

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (Get_Numeric(ds.Tables[0].Rows[i]["AISISSAMT"].ToString()) == "0")
                    {
                        this.ShowMessage("TY_M_AC_382BD291");
                        e.Successed = false;
                        return;
                    }

                    if (int.Parse(Get_Numeric(fsAESQDATE)) > int.Parse(Get_Numeric(ds.Tables[0].Rows[i]["AISQDATE"].ToString())))
                    {
                        this.ShowMessage("TY_M_AC_4375V607");
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

        #region Description : 저장품 불출내역 삭제 ProcessCheck 이벤트
        private void BTN62_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataTable dt = this.FPS91_TY_S_AC_4375A603.GetDataSourceInclude(TSpread.TActionType.Remove, "AISQDATE", "AISISSAMT");

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
                "TY_P_AC_43A8V614",
                this.TXT02_AIMANYEAR.GetValue().ToString(),
                this.CBH02_AIMANCDDP.GetValue().ToString(),
                this.TXT02_AIMANSEQ.GetValue().ToString()
                );

            dt1 = this.DbConnector.ExecuteDataTable();

            if (dt1.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_AC_43A8Y615");
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
            this.TXT02_AIMANYEAR.SetReadOnly(bTrueAndFalse);
            this.CBH02_AIMANCDDP.SetReadOnly(bTrueAndFalse);
            this.TXT02_AIMANSEQ.SetReadOnly(bTrueAndFalse);
        }
        #endregion

        #region Description : 저장품 MAST 스프레드 이벤트
        private void FPS91_TY_S_AC_43653585_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            if (e.Column != 1)
                return;

            // 부서명을 가져오기 위해서 스프레드의 예산년도에 파라미터 날짜를 넣음.
            string year = FPS91_TY_S_AC_43653585.GetValue(e.Row, "AEMANYEAR").ToString() + "0101";
            //((TCodeBoxCellType)FPS91_TY_S_AC_24917510.ActiveSheet.Columns["P1CDDP"].CellType).DummyValue = year;
            TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_43653585, "AEMANCDDP");
            if (tyCodeBox != null)
                tyCodeBox.DummyValue = year;
        }
        #endregion

        #region Description : 저장품 MAST 스프레드 클릭 이벤트
        private void FPS91_TY_S_AC_43653585_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.TXT02_AIMANYEAR.SetValue(this.FPS91_TY_S_AC_43653585.GetValue("AEMANYEAR").ToString());
            this.CBH02_AIMANCDDP.SetValue(this.FPS91_TY_S_AC_43653585.GetValue("AEMANCDDP").ToString());
            this.TXT02_AIMANSEQ.SetValue(this.FPS91_TY_S_AC_43653585.GetValue("AEMANSEQ").ToString());

            fsAESQDATE = this.FPS91_TY_S_AC_43653585.GetValue("AESQDATE").ToString();
            
            UP_SetReadOnly(true);

            UP_Set_Detail();
        }
        #endregion

        #region Description : 저장품 불출내역 조회
        private void UP_Set_Detail()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_43755601",
                this.TXT02_AIMANYEAR.GetValue().ToString(),
                this.CBH02_AIMANCDDP.GetValue().ToString(),
                this.TXT02_AIMANSEQ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_4375A603.SetValue(dt);

            if (this.FPS91_TY_S_AC_4375A603.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_AC_4375A603, "AISQDATE", "합 계", SumRowType.Total, "AISISSAMT");
            }
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
    }
}