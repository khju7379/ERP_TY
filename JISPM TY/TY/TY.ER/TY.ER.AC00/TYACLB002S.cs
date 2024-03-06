using System;
using System.Data;
using System.Drawing;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;


namespace TY.ER.AC00
{
    /// <summary>
    /// 월예산관리 등록 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.04.03 13:05
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2423A253 : 년예산 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_243AT314 : 월예산 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  Y1CDAC : 예산계정과목
    ///  Y1CDDP : 예산부서
    ///  Y1CDSB : 예산사번
    ///  Y1YEAR : 예산년도
    /// </summary>
    public partial class TYACLB002S : TYBase
    {
        //년예산 변수
        private TYData DAT03_Y1YEAR;
        private TYData DAT03_Y1CDDP;
        private TYData DAT03_Y1CDSB;
        private TYData DAT03_Y1CDAC;
        private TYData DAT03_Y1AMT;
        private TYData DAT03_Y1PLAMT;
        private TYData DAT03_Y1CSAMT;
        private TYData DAT03_Y1CDAMT;
        private TYData DAT03_Y1HIGB;
        private TYData DAT03_Y1HISAB;

       


        #region Description : 폼 Load
        public TYACLB002S()
        {
            InitializeComponent();

            this.DAT03_Y1YEAR = new TYData("DAT03_Y1YEAR", null);
            this.DAT03_Y1CDDP = new TYData("DAT03_Y1CDDP", null);
            this.DAT03_Y1CDSB = new TYData("DAT03_Y1CDSB", null);
            this.DAT03_Y1CDAC = new TYData("DAT03_Y1CDAC", null);
            this.DAT03_Y1AMT = new TYData("DAT03_Y1AMT", null);
            this.DAT03_Y1PLAMT = new TYData("DAT03_Y1PLAMT", null);
            this.DAT03_Y1CSAMT = new TYData("DAT03_Y1CSAMT", null);
            this.DAT03_Y1CDAMT = new TYData("DAT03_Y1CDAMT", null);
            this.DAT03_Y1HIGB = new TYData("DAT03_Y1HIGB", null);
            this.DAT03_Y1HISAB = new TYData("DAT03_Y1HISAB", null);
        }

        private void TYACLB002S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.TXT02_Y2YEAR.SetReadOnly(true);
            this.CBH02_Y2CDDP.SetReadOnly(true);
            this.CBH02_Y2CDSB.SetReadOnly(true);
            this.CBH02_Y2CDAC.SetReadOnly(true);

            this.ControlFactory.Add(this.DAT03_Y1YEAR);
            this.ControlFactory.Add(this.DAT03_Y1CDDP);
            this.ControlFactory.Add(this.DAT03_Y1CDSB);
            this.ControlFactory.Add(this.DAT03_Y1CDAC);
            this.ControlFactory.Add(this.DAT03_Y1AMT);
            this.ControlFactory.Add(this.DAT03_Y1PLAMT);
            this.ControlFactory.Add(this.DAT03_Y1CSAMT);
            this.ControlFactory.Add(this.DAT03_Y1CDAMT);
            this.ControlFactory.Add(this.DAT03_Y1HIGB);
            this.ControlFactory.Add(this.DAT03_Y1HISAB);

            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_243AT314, "Y1YEAR");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_243AT314, "Y1CDDP");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_243AT314, "Y1CDSB");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_243AT314, "Y1CDAC");

            this.TXT01_Y1YEAR_TextChanged(null, null);

            this.SetStartingFocus(this.TXT01_Y1YEAR); 

        }
        #endregion

        #region Description : 조회 ProcessCheck 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2423A253", this.ControlFactory, "01");
            this.FPS91_TY_S_AC_243AT314.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 신규 ProcessCheck 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            //if ((new TYACLB002I(string.Empty, string.Empty, string.Empty, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //    this.BTN61_INQ_Click(null, null);

                this.Initialize_Controls("02");

                UP_SetReadOnly(false);               

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_243AL308", this.TXT02_Y2YEAR.GetValue(), this.CBH02_Y2CDDP.GetValue(), this.CBH02_Y2CDSB.GetValue(), this.CBH02_Y2CDAC.GetValue());
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count != 0)
                {
                    dt = UP_NewRowAdd(dt);

                    //this.FPS91_TY_S_AC_243BR337.SetValue(UP_SumRowAdd(dt));
                    this.FPS91_TY_S_AC_243BR337.SetValue(dt);
                }
                else
                {
                    this.FPS91_TY_S_AC_243BR337.SetValue(UP_NewRowAdd_NEW(dt));

                    // 강제 세팅 (신규 월 강제 셋팅)
                    for (int i = 0; i < this.FPS91_TY_S_AC_243BR337.CurrentRowCount; i++)
                    {
                        this.FPS91_TY_S_AC_243BR337.ActiveSheet.RowHeader.Cells[i, 0].Text = "N";
                    }
                }
                UP_SumRowAdd();

        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            if (TXT02_Y2YEAR.ReadOnly == false)
            {
                // 기존 DATASET에 신규필드(사번 필드) 추가
                if (ds.Tables[1].Rows.Count != 0)
                {
                    this.DataTableColumnAdd(ds.Tables[1], "Y2YEAR", TXT02_Y2YEAR.GetValue());
                    this.DataTableColumnAdd(ds.Tables[1], "Y2CDDP", CBH02_Y2CDDP.GetValue());
                    this.DataTableColumnAdd(ds.Tables[1], "Y2CDSB", CBH02_Y2CDSB.GetValue());
                    this.DataTableColumnAdd(ds.Tables[1], "Y2CDAC", CBH02_Y2CDAC.GetValue());
                    this.DataTableColumnAdd(ds.Tables[1], "Y2HIGB", "A");
                    this.DataTableColumnAdd(ds.Tables[1], "Y2HISAB", TYUserInfo.EmpNo);
                }
            }
            else
            {
                // 기존 DATASET에 신규필드(사번 필드) 추가
                this.DataTableColumnAdd(ds.Tables[0], "Y2YEAR", TXT02_Y2YEAR.GetValue());
                this.DataTableColumnAdd(ds.Tables[0], "Y2CDDP", CBH02_Y2CDDP.GetValue());
                this.DataTableColumnAdd(ds.Tables[0], "Y2CDSB", CBH02_Y2CDSB.GetValue());
                this.DataTableColumnAdd(ds.Tables[0], "Y2CDAC", CBH02_Y2CDAC.GetValue());
                this.DataTableColumnAdd(ds.Tables[0], "Y2HIGB", "C");
                this.DataTableColumnAdd(ds.Tables[0], "Y2HISAB", TYUserInfo.EmpNo);
            }

            //년예산관리 등록
            this.DAT03_Y1YEAR.SetValue(TXT02_Y2YEAR.GetValue());
            this.DAT03_Y1CDDP.SetValue(CBH02_Y2CDDP.GetValue());
            this.DAT03_Y1CDSB.SetValue(CBH02_Y2CDSB.GetValue());
            this.DAT03_Y1CDAC.SetValue(CBH02_Y2CDAC.GetValue());
            this.DAT03_Y1AMT.SetValue(this.FPS91_TY_S_AC_243BR337.GetValue(12, "Y2AMT"));
            this.DAT03_Y1PLAMT.SetValue(this.FPS91_TY_S_AC_243BR337.GetValue(12, "Y2PLAMT"));
            this.DAT03_Y1CSAMT.SetValue(this.FPS91_TY_S_AC_243BR337.GetValue(12, "Y2CSAMT"));
            this.DAT03_Y1CDAMT.SetValue(this.FPS91_TY_S_AC_243BR337.GetValue(12, "Y2CDAMT"));

            this.DAT03_Y1HISAB.SetValue(TYUserInfo.EmpNo);

            if (TXT02_Y2YEAR.ReadOnly == false)
            {
                this.DAT03_Y1HIGB.SetValue("A");

                this.DbConnector.CommandClear();
                //this.DbConnector.Attach("TY_P_AC_243AO309", ds.Tables[1]); //저장(월예산관리)

                // 신규등록 (월예산관리)
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_243AO309", ds.Tables[1].Rows[i]["Y2YEAR"].ToString(),
                                                                ds.Tables[1].Rows[i]["Y2MONTH"].ToString(),
                                                                ds.Tables[1].Rows[i]["Y2CDDP"].ToString(),
                                                                ds.Tables[1].Rows[i]["Y2CDSB"].ToString(),
                                                                ds.Tables[1].Rows[i]["Y2CDAC"].ToString(),
                                                                Get_Numeric(ds.Tables[1].Rows[i]["Y2AMT"].ToString()),
                                                                Get_Numeric(ds.Tables[1].Rows[i]["Y2PLAMT"].ToString()),
                                                                Get_Numeric(ds.Tables[1].Rows[i]["Y2CSAMT"].ToString()),
                                                                Get_Numeric(ds.Tables[1].Rows[i]["Y2CDAMT"].ToString()),
                                                                ds.Tables[1].Rows[i]["Y2HIGB"].ToString(),
                                                                ds.Tables[1].Rows[i]["Y2HISAB"].ToString());
                }

                this.DbConnector.Attach("TY_P_AC_24355367", this.ControlFactory, "03"); //저장(년예산관리)                 
                this.DbConnector.ExecuteTranQueryList();
            }
            else
            {
                this.DAT03_Y1HIGB.SetValue("C");

                this.DbConnector.CommandClear();
                //this.DbConnector.Attach("TY_P_AC_243AQ310", ds.Tables[0]); //수정(월예산관리)

                //수정(월예산관리)
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_243AQ310",
                                            Get_Numeric(ds.Tables[0].Rows[i]["Y2AMT"].ToString()),
                                            Get_Numeric(ds.Tables[0].Rows[i]["Y2PLAMT"].ToString()),
                                            Get_Numeric(ds.Tables[0].Rows[i]["Y2CSAMT"].ToString()),
                                            Get_Numeric(ds.Tables[0].Rows[i]["Y2CDAMT"].ToString()),
                                            ds.Tables[0].Rows[i]["Y2HIGB"].ToString(),
                                            ds.Tables[0].Rows[i]["Y2HISAB"].ToString(),
                                            ds.Tables[0].Rows[i]["Y2YEAR"].ToString(),
                                            ds.Tables[0].Rows[i]["Y2MONTH"].ToString(),
                                            ds.Tables[0].Rows[i]["Y2CDDP"].ToString(),
                                            ds.Tables[0].Rows[i]["Y2CDSB"].ToString(),
                                            ds.Tables[0].Rows[i]["Y2CDAC"].ToString()  
                                            );
                }

                this.DbConnector.Attach("TY_P_AC_2449P391", this.DAT03_Y1AMT.GetValue(),
                                                            this.DAT03_Y1PLAMT.GetValue(),   
                                                            this.DAT03_Y1HIGB.GetValue(),
                                                            this.DAT03_Y1HISAB.GetValue(),
                                                            this.DAT03_Y1YEAR.GetValue(),
                                                            this.DAT03_Y1CDDP.GetValue(),
                                                            this.DAT03_Y1CDSB.GetValue(),
                                                            this.DAT03_Y1CDAC.GetValue()
                                                            ); //수정(년예산관리)
                this.DbConnector.ExecuteTranQueryList();
            }

            this.ShowMessage("TY_M_GB_23NAD873");
            this.BTN61_INQ_Click(null, null);

            UP_SetReadOnly(true);
            UP_SetGridMaster();


        }
        #endregion

        #region Description : 삭제 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;
            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_244AF393", dt); //년파일 
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 1; j < 13; j++)
                {
                    this.DbConnector.Attach("TY_P_AC_243AR311", dt.Rows[i]["Y1YEAR"].ToString(),
                                                                j.ToString("00"),
                                                                dt.Rows[i]["Y1CDDP"].ToString(),
                                                                dt.Rows[i]["Y1CDSB"].ToString(),
                                                                dt.Rows[i]["Y1CDAC"].ToString()); //월파일  
                }
            }
            this.DbConnector.ExecuteTranQueryList(); 

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            // 신규 버턴 처리시에만 체크
            if (TXT02_Y2YEAR.ReadOnly == false)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_243AL308", this.TXT02_Y2YEAR.GetValue(), this.CBH02_Y2CDDP.GetValue(), this.CBH02_Y2CDSB.GetValue(), this.CBH02_Y2CDAC.GetValue());
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count != 0)
                {
                    this.ShowMessage("TY_M_AC_387AG357");
                    e.Successed = false;
                    return;
                }
            }


            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_243BR337.GetDataSourceInclude(TSpread.TActionType.Update, "Y2MONTH", "Y2AMT", "Y2PLAMT", "Y2CSAMT", "Y2CDAMT")); // 수정
            ds.Tables.Add(this.FPS91_TY_S_AC_243BR337.GetDataSourceInclude(TSpread.TActionType.New, "Y2MONTH", "Y2AMT", "Y2PLAMT", "Y2CSAMT", "Y2CDAMT"));   // 신규

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)  // 수정 , 저장 체크
            {
                this.ShowMessage("TY_M_GB_2452W459");
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
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            double dWkAMT = 0;

            DataTable dt = this.FPS91_TY_S_AC_243AT314.GetDataSourceInclude(TSpread.TActionType.Remove, "Y1YEAR", "Y1CDDP", "Y1CDSB", "Y1CDAC");

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

            //년파일 예산사용액 체크            
            this.DbConnector.CommandClear();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                this.DbConnector.Attach("TY_P_AC_24415405", dt.Rows[j].ItemArray);
            }            
            DataSet dtChk = this.DbConnector.ExecuteDataSet();
            for (int i = dtChk.Tables.Count - 1; i >= 0; i--)
            {
                dWkAMT = Convert.ToDouble(dtChk.Tables[i].Rows[0]["Y1PLAMT"].ToString());
                dWkAMT = dWkAMT + Convert.ToDouble(dtChk.Tables[i].Rows[0]["Y1CSAMT"].ToString());
                dWkAMT = dWkAMT + Convert.ToDouble(dtChk.Tables[i].Rows[0]["Y1CDAMT"].ToString());
                if (dWkAMT > 0)
                {
                    this.ShowMessage("TY_M_AC_244AZ394");
                    dt.Rows.RemoveAt(i);
                }
            }

            e.ArgData = dt;
        }
        #endregion

        #region Description : Spread  CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_243AT314_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //if ((new TYACLB002I(this.FPS91_TY_S_AC_243AT314.GetValue("Y1YEAR").ToString(),
            //                    this.FPS91_TY_S_AC_243AT314.GetValue("Y1CDDP").ToString(),
            //                    this.FPS91_TY_S_AC_243AT314.GetValue("Y1CDSB").ToString(),
            //                    this.FPS91_TY_S_AC_243AT314.GetValue("Y1CDAC").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //    this.BTN61_INQ_Click(null, null);

            this.TXT02_Y2YEAR.SetValue(this.FPS91_TY_S_AC_243AT314.GetValue("Y1YEAR").ToString());
            this.CBH02_Y2CDDP.SetValue(this.FPS91_TY_S_AC_243AT314.GetValue("Y1CDDP").ToString());
            this.CBH02_Y2CDSB.SetValue(this.FPS91_TY_S_AC_243AT314.GetValue("Y1CDSB").ToString());
            this.CBH02_Y2CDAC.SetValue(this.FPS91_TY_S_AC_243AT314.GetValue("Y1CDAC").ToString());

            this.CBH02_Y2CDDP.DummyValue = TXT02_Y2YEAR.GetValue() + "0101";
            UP_SetReadOnly(true);
            UP_SetGridMaster();

        }
        #endregion

        #region Description : 사용자 정의 함수
        private void UP_SetReadOnly(bool bTrueAndFalse)
        {
            this.TXT02_Y2YEAR.SetReadOnly(bTrueAndFalse);
            this.CBH02_Y2CDDP.SetReadOnly(bTrueAndFalse);
            this.CBH02_Y2CDSB.SetReadOnly(bTrueAndFalse);
            this.CBH02_Y2CDAC.SetReadOnly(bTrueAndFalse);   
        }

        private void UP_SetGridMaster()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_243AL308", this.TXT02_Y2YEAR.GetValue(), this.CBH02_Y2CDDP.GetValue(), this.CBH02_Y2CDSB.GetValue(), this.CBH02_Y2CDAC.GetValue());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "02");
                this.FPS91_TY_S_AC_243BR337.SetValue(UP_MonRowAdd(dt));
                //this.FPS91_TY_S_AC_243BR337.SetValue(dt);
                UP_SumRowAdd();
            }
        }


        private DataTable UP_NewRowAdd_NEW(DataTable dt)
        {
            DataTable Rowdt = new DataTable();
            DataRow rw;
            Rowdt = dt.Clone();
            for (int i = 1; i < 13; i++)
            {
                rw = Rowdt.NewRow();
                rw["Y2MONTH"] = i.ToString("00");
                rw["Y2AMT"] = 0;
                rw["Y2PLAMT"] = 0;
                rw["Y2CSAMT"] = 0;
                rw["Y2CDAMT"] = 0;
                rw["Y2JANAMT"] = 0;
                Rowdt.Rows.Add(rw);
            }
            return Rowdt;
        }

        private DataTable UP_NewRowAdd(DataTable dt)
        {
            DataTable Rowdt = new DataTable();
            DataRow rw;
            Rowdt = dt.Clone();
            for (int i = 1; i < 13; i++)
            {
                rw = Rowdt.NewRow();
                rw["Y2MONTH"] = i.ToString("00");
                rw["Y2AMT"] = DBNull.Value;
                rw["Y2PLAMT"] = 0;
                rw["Y2CSAMT"] = 0;
                rw["Y2CDAMT"] = 0;
                rw["Y2JANAMT"] = 0;
                Rowdt.Rows.Add(rw);
            }
            return Rowdt;
        }

        private void UP_SumRowAdd()
        {
            this.SpreadSumRowAdd(this.FPS91_TY_S_AC_243BR337, "Y2MONTH", "합 계", Color.Yellow);
            this.FPS91_TY_S_AC_243BR337_Sheet1.SetFormula(
                FPS91_TY_S_AC_243BR337_Sheet1.RowCount - 1,
                FPS91_TY_S_AC_243BR337_Sheet1.ColumnCount - 1,
                "R[0]C[-4] + R[0]C[-3] - R[0]C[-2] - R[0]C[-1]"); //잔액 구하기        
        }

        private DataTable UP_MonRowAdd(DataTable dt)
        {
            DataTable Rowdt = new DataTable();
            DataRow rw;

            Rowdt = dt.Clone();

            for (int i = 1; i < 13; i++)
            {
                rw = Rowdt.NewRow();
                rw["Y2YEAR"] = dt.Rows[0]["Y2YEAR"].ToString();
                rw["Y2MONTH"] = i.ToString("00");
                rw["Y2CDDP"] = dt.Rows[0]["Y2CDDP"].ToString();
                rw["Y2CDDPNM"] = dt.Rows[0]["Y2CDDPNM"].ToString();
                rw["Y2CDSB"] = dt.Rows[0]["Y2CDSB"].ToString();
                rw["Y2CDSBNM"] = dt.Rows[0]["Y2CDSBNM"].ToString();
                rw["Y2CDAC"] = dt.Rows[0]["Y2CDAC"].ToString();
                rw["Y2CDACNM"] = dt.Rows[0]["Y2CDACNM"].ToString();
                rw["Y2AMT"] = Get_Numeric(dt.Compute("Sum(Y2AMT)", "Y2MONTH = " + i.ToString("00")).ToString());
                rw["Y2PLAMT"] = Get_Numeric(dt.Compute("Sum(Y2PLAMT)", "Y2MONTH = " + i.ToString("00")).ToString());
                rw["Y2CSAMT"] = Get_Numeric(dt.Compute("Sum(Y2CSAMT)", "Y2MONTH = " + i.ToString("00")).ToString());
                rw["Y2CDAMT"] = Get_Numeric(dt.Compute("Sum(Y2CDAMT)", "Y2MONTH = " + i.ToString("00")).ToString());
                rw["Y2JANAMT"] = Convert.ToDouble(Get_Numeric(dt.Compute("Sum(Y2AMT)", "Y2MONTH <= " + i.ToString("00")).ToString())) +
                                 Convert.ToDouble(Get_Numeric(dt.Compute("Sum(Y2PLAMT)", "Y2MONTH <= " + i.ToString("00")).ToString())) -
                                 Convert.ToDouble(Get_Numeric(dt.Compute("Sum(Y2CSAMT)", "Y2MONTH <= " + i.ToString("00")).ToString())) -
                                 Convert.ToDouble(Get_Numeric(dt.Compute("Sum(Y2CDAMT)", "Y2MONTH <= " + i.ToString("00")).ToString()));
                Rowdt.Rows.Add(rw);
            }

            return Rowdt;
        }
        
        #endregion

        #region Description : TXT01_Y1YEAR_TextChanged 이벤트
        private void TXT01_Y1YEAR_TextChanged(object sender, EventArgs e)
        {
            if (this.TXT01_Y1YEAR.GetValue().ToString() != "")
            {
                this.CBH01_Y1CDDP.DummyValue = TXT01_Y1YEAR.GetValue() + "0101";
                this.CBH02_Y2CDDP.DummyValue = TXT01_Y1YEAR.GetValue() + "0101";
            }
            else
            {
                this.CBH01_Y1CDDP.DummyValue = DateTime.Now.ToString("yyyyMMdd");
                this.CBH02_Y2CDDP.DummyValue = DateTime.Now.ToString("yyyyMMdd");
            }
        }
        #endregion
    }
}
