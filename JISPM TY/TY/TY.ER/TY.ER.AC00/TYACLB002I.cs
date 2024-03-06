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
    /// 월예산관리 등록 팝업 프로그램입니다.
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
    ///  TY_P_AC_243AL308 : 월예산관리 조회
    ///  TY_P_AC_243AO309 : 월예산관리 등록
    ///  TY_P_AC_243AQ310 : 월예산관리 수정
    ///  TY_P_GB_2423M259 : 코드박스-공통코드
    ///  TY_P_GB_24242261 : 코드박스-사원번호
    ///  TY_P_GB_24391302 : 계정 과목 조회(코드박스용)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_243BR337 : 월예산 상세 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  Y2CDAC : 예산계정과목
    ///  Y2CDDP : 예산부서
    ///  Y2CDSB : 사 번
    ///  Y2YEAR : 예산년도
    /// </summary>
    public partial class TYACLB002I : TYBase
    {
        private string fsY2YEAR;
        private string fsY2CDDP;
        private string fsY2CDSB;
        private string fsY2CDAC;

        //년예산 변수
        private TYData DAT02_Y1YEAR;
        private TYData DAT02_Y1CDDP;
        private TYData DAT02_Y1CDSB;
        private TYData DAT02_Y1CDAC;
        private TYData DAT02_Y1AMT;
        private TYData DAT02_Y1PLAMT;
        private TYData DAT02_Y1CSAMT;
        private TYData DAT02_Y1CDAMT;
        private TYData DAT02_Y1HIGB;
        private TYData DAT02_Y1HISAB;

        public TYACLB002I(string sY2YEAR, string sY2CDDP, string sY2CDSB, string sY2CDAC)
        {
            InitializeComponent();

            this.SetPopupStyle();

            this.fsY2YEAR = sY2YEAR;
            this.fsY2CDDP = sY2CDDP;
            this.fsY2CDSB = sY2CDSB;
            this.fsY2CDAC = sY2CDAC;

            this.DAT02_Y1YEAR = new TYData("DAT02_Y1YEAR", null);
            this.DAT02_Y1CDDP = new TYData("DAT02_Y1CDDP", null);
            this.DAT02_Y1CDSB = new TYData("DAT02_Y1CDSB", null);
            this.DAT02_Y1CDAC = new TYData("DAT02_Y1CDAC", null);
            this.DAT02_Y1AMT = new TYData("DAT02_Y1AMT", null);
            this.DAT02_Y1PLAMT = new TYData("DAT02_Y1PLAMT", null);
            this.DAT02_Y1CSAMT = new TYData("DAT02_Y1CSAMT", null);
            this.DAT02_Y1CDAMT = new TYData("DAT02_Y1CDAMT", null);
            this.DAT02_Y1HIGB = new TYData("DAT02_Y1HIGB", null);
            this.DAT02_Y1HISAB = new TYData("DAT02_Y1HISAB", null);

        }

        private void TYACLB002I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.ControlFactory.Add(this.DAT02_Y1YEAR);
            this.ControlFactory.Add(this.DAT02_Y1CDDP);
            this.ControlFactory.Add(this.DAT02_Y1CDSB);
            this.ControlFactory.Add(this.DAT02_Y1CDAC);
            this.ControlFactory.Add(this.DAT02_Y1AMT);
            this.ControlFactory.Add(this.DAT02_Y1PLAMT);
            this.ControlFactory.Add(this.DAT02_Y1CSAMT);
            this.ControlFactory.Add(this.DAT02_Y1CDAMT);
            this.ControlFactory.Add(this.DAT02_Y1HIGB);
            this.ControlFactory.Add(this.DAT02_Y1HISAB);

            if (string.IsNullOrEmpty(this.fsY2YEAR))
            {
                this.TXT01_Y2YEAR.SetReadOnly(false);
                this.CBH01_Y2CDDP.SetReadOnly(false);
                this.CBH01_Y2CDSB.SetReadOnly(false);
                this.CBH01_Y2CDAC.SetReadOnly(false);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_243AL308", this.fsY2YEAR, this.fsY2CDDP, this.fsY2CDSB, this.fsY2CDAC);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                dt = UP_NewRowAdd(dt);

                //this.FPS91_TY_S_AC_243BR337.SetValue(UP_SumRowAdd(dt));
                this.FPS91_TY_S_AC_243BR337.SetValue(dt);
                UP_SumRowAdd();

            }
            else
            {
                this.TXT01_Y2YEAR_TextChanged(null, null);

                this.TXT01_Y2YEAR.SetReadOnly(true);
                this.CBH01_Y2CDDP.SetReadOnly(true);
                this.CBH01_Y2CDSB.SetReadOnly(true);
                this.CBH01_Y2CDAC.SetReadOnly(true);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_243AL308", this.fsY2YEAR, this.fsY2CDDP, this.fsY2CDSB, this.fsY2CDAC);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.CurrentDataTableRowMapping(dt, "01");
                    this.FPS91_TY_S_AC_243BR337.SetValue(UP_MonRowAdd(dt));
                    //this.FPS91_TY_S_AC_243BR337.SetValue(dt);
                    UP_SumRowAdd();
                }
            }
        }

        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // 기존 DATASET에 신규필드(사번 필드) 추가
            this.DataTableColumnAdd(ds.Tables[0], "Y2YEAR", TXT01_Y2YEAR.GetValue());
            this.DataTableColumnAdd(ds.Tables[0], "Y2CDDP", CBH01_Y2CDDP.GetValue());
            this.DataTableColumnAdd(ds.Tables[0], "Y2CDSB", CBH01_Y2CDSB.GetValue());
            this.DataTableColumnAdd(ds.Tables[0], "Y2CDAC", CBH01_Y2CDAC.GetValue());
            this.DataTableColumnAdd(ds.Tables[0], "Y2HIGB", "A");
            this.DataTableColumnAdd(ds.Tables[0], "Y2HISAB", TYUserInfo.EmpNo);

            //년예산관리 등록
            this.DAT02_Y1YEAR.SetValue(TXT01_Y2YEAR.GetValue());
            this.DAT02_Y1CDDP.SetValue(CBH01_Y2CDDP.GetValue());
            this.DAT02_Y1CDSB.SetValue(CBH01_Y2CDSB.GetValue());
            this.DAT02_Y1CDAC.SetValue(CBH01_Y2CDAC.GetValue());
            this.DAT02_Y1AMT.SetValue(this.FPS91_TY_S_AC_243BR337.GetValue(12, "Y2AMT"));
            this.DAT02_Y1PLAMT.SetValue(this.FPS91_TY_S_AC_243BR337.GetValue(12, "Y2PLAMT"));
            this.DAT02_Y1CSAMT.SetValue(this.FPS91_TY_S_AC_243BR337.GetValue(12, "Y2CSAMT"));
            this.DAT02_Y1CDAMT.SetValue(this.FPS91_TY_S_AC_243BR337.GetValue(12, "Y2CDAMT"));
            
            this.DAT02_Y1HISAB.SetValue(TYUserInfo.EmpNo);

            if (string.IsNullOrEmpty(this.fsY2YEAR))
            {
                this.DAT02_Y1HIGB.SetValue("A");

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_243AO309", ds.Tables[0]); //저장(월예산관리)
                this.DbConnector.Attach("TY_P_AC_24355367", this.ControlFactory, "02"); //저장(년예산관리)                 
                this.DbConnector.ExecuteTranQueryList();                  
            }
            else
            {
                this.DAT02_Y1HIGB.SetValue("C");

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_243AQ310", ds.Tables[0]); //수정(월예산관리)
                this.DbConnector.Attach("TY_P_AC_2449P391", this.DAT02_Y1AMT.GetValue(),
                                                            this.DAT02_Y1HIGB.GetValue(),
                                                            this.DAT02_Y1HISAB.GetValue(),
                                                            this.DAT02_Y1YEAR.GetValue(), 
                                                            this.DAT02_Y1CDDP.GetValue(), 
                                                            this.DAT02_Y1CDSB.GetValue(), 
                                                            this.DAT02_Y1CDAC.GetValue()
                                                            ); //수정(년예산관리)
                this.DbConnector.ExecuteTranQueryList();
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ShowMessage("TY_M_GB_23NAD873");
            this.Close();
        }

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_243BR337.GetDataSourceInclude(TSpread.TActionType.Update, "Y2MONTH", "Y2AMT", "Y2PLAMT", "Y2CSAMT", "Y2CDAMT"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
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
                "R[0]C[-4] - R[0]C[-3] - R[0]C[-2] - R[0]C[-1]"); //잔액 구하기        
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
                rw["Y2JANAMT"] = Convert.ToDouble(Get_Numeric(dt.Compute("Sum(Y2AMT)", "Y2MONTH <= " + i.ToString("00")).ToString())) -
                                 Convert.ToDouble(Get_Numeric(dt.Compute("Sum(Y2PLAMT)", "Y2MONTH <= " + i.ToString("00")).ToString())) -
                                 Convert.ToDouble(Get_Numeric(dt.Compute("Sum(Y2CSAMT)", "Y2MONTH <= " + i.ToString("00")).ToString())) -
                                 Convert.ToDouble(Get_Numeric(dt.Compute("Sum(Y2CDAMT)", "Y2MONTH <= " + i.ToString("00")).ToString()));
                Rowdt.Rows.Add(rw);
            }

            return Rowdt;
        }

        private string Get_Numeric(string sStr)
        {
            if (sStr == "") return "0";
            else return sStr.Replace(",", "");
        }

        #region Description : TXT01_Y2YEAR_TextChanged 이벤트
        private void TXT01_Y2YEAR_TextChanged(object sender, EventArgs e)
        {
            if (this.TXT01_Y2YEAR.GetValue().ToString() != "")
            {
                this.CBH01_Y2CDDP.DummyValue = TXT01_Y2YEAR.GetValue() + "0101";
            }
            else
            {
                this.CBH01_Y2CDDP.DummyValue = DateTime.Now.ToString("yyyyMMdd");
            }
        }
        #endregion
    }
}
