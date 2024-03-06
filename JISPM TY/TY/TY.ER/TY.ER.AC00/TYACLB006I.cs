using System;
using System.Data;
using System.Drawing;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;

namespace TY.ER.AC00
{
    /// <summary>
    /// 소모품비세목관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.07.05 13:05
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2756L971 : 소모품비 세목예산 조회
    ///  TY_S_AC_2756M972 : 소모품비 월예산 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  J1CDDP : 팀코드
    ///  J2CDAC : 계정
    ///  J2CDDP : 팀코드
    ///  J2CDJJ : 비품코드
    ///  J1RKAC : 내용
    ///  J1YEAR : 예산년도
    ///  J2SEQ : 순번
    ///  J2YEAR : 예산년도
    /// </summary>
    public partial class TYACLB006I : TYBase
    {
        #region Description : 페이지 로드
        public TYACLB006I()
        {
            InitializeComponent();

            // 스프레드에서 코드헬프 사용
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_2756L971, "J1CDDP", "DTDESC1", "J1CDDP");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_2756L971, "J1CDJJ", "BPDESC1", "J1CDJJ");
        }

        private void TYACLB006I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_2756L971, "J1YEAR");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_2756L971, "J1CDDP");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_2756L971, "DTDESC1");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_2756L971, "J1GUBUN");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_2756L971, "J1CDAC");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_2756L971, "J1CDJJ");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_2756L971, "BPDESC1");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_2756L971, "J1SEQ");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.BTN62_SAV.ProcessCheck += new TButton.CheckHandler(BTN62_SAV_ProcessCheck);

            this.CBH01_J1CDDP.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            UP_SetReadOnly(false);

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.TXT01_J1YEAR);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_2757N973",
                this.TXT01_J1YEAR.GetValue().ToString(),
                this.CBH01_J1CDDP.GetValue().ToString()
                );

            this.FPS91_TY_S_AC_2756L971.SetValue(this.DbConnector.ExecuteDataTable());

            this.FPS91_TY_S_AC_2756M972.Initialize();

            this.TXT02_J2YEAR.SetValue("");
            this.CBH02_J2CDDP.SetValue("");
            this.CBH02_J2CDAC.SetValue("");
            this.CBH02_J2CDJJ.SetValue("");
            this.TXT02_J2SEQ.SetValue("");
            this.TXT02_J1RKAC.SetValue("");
        }
        #endregion

        #region Description : 소모품비 세목예산 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            int i = 0;
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // 소모품비 세목코드(APPJCDF) 저장 및 수정
            this.DbConnector.CommandClear();

            //this.DbConnector.Attach("TY_P_AC_2763D984", ds.Tables[0]);
            //this.DbConnector.Attach("TY_P_AC_2763G985", ds.Tables[1]);

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_2763D984", ds.Tables[0].Rows[i]["J1YEAR"].ToString(),
                                                            ds.Tables[0].Rows[i]["J1CDDP"].ToString(),
                                                            ds.Tables[0].Rows[i]["J1CDAC"].ToString().Substring(0, 8),
                                                            ds.Tables[0].Rows[i]["J1CDJJ"].ToString(),
                                                            ds.Tables[0].Rows[i]["J1SEQ"].ToString(),
                                                            ds.Tables[0].Rows[i]["J1RKAC"].ToString()
                                                            );
            }

            for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_2763G985", ds.Tables[0].Rows[i]["J1RKAC"].ToString(),
                                                            ds.Tables[0].Rows[i]["J1YEAR"].ToString(),
                                                            ds.Tables[0].Rows[i]["J1CDDP"].ToString(),
                                                            ds.Tables[0].Rows[i]["J1CDAC"].ToString().Substring(0, 8),
                                                            ds.Tables[0].Rows[i]["J1CDJJ"].ToString(),
                                                            ds.Tables[0].Rows[i]["J1SEQ"].ToString()
                                                            );
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");
            this.BTN61_INQ_Click(null, null);

            this.FPS91_TY_S_AC_2756M972.Initialize();
        }
        #endregion

        #region Description : 소모품비 세목예산 및 월예산 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();

            // 본예산 업데이트
            for (i = 0; i < dt.Rows.Count; i++)
            {
                // SP 호출
                this.DbConnector.Attach
                (
                "TY_P_AC_2799E989",
                dt.Rows[i]["J1YEAR"].ToString(),
                dt.Rows[i]["J1CDDP"].ToString(),
                dt.Rows[i]["J1CDAC"].ToString().Substring(0,8),
                dt.Rows[i]["J1CDJJ"].ToString(),
                dt.Rows[i]["J1SEQ"].ToString()
                );

                // 소모품비 세목예산
                this.DbConnector.Attach("TY_P_AC_279B2990", dt.Rows[i]["J1YEAR"].ToString(),
                                                            dt.Rows[i]["J1CDDP"].ToString(),
                                                            dt.Rows[i]["J1CDAC"].ToString().Substring(0, 8),
                                                            dt.Rows[i]["J1CDJJ"].ToString(),
                                                            dt.Rows[i]["J1SEQ"].ToString());

                // 소모품비 세목 월예산
                this.DbConnector.Attach("TY_P_AC_279B3991", dt.Rows[i]["J1YEAR"].ToString(),
                                                            dt.Rows[i]["J1CDDP"].ToString(),
                                                            dt.Rows[i]["J1CDAC"].ToString().Substring(0, 8),
                                                            dt.Rows[i]["J1CDJJ"].ToString(),
                                                            dt.Rows[i]["J1SEQ"].ToString());
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지
            this.BTN61_INQ_Click(null, null);

            this.FPS91_TY_S_AC_2756M972.Initialize();

            this.TXT02_J2YEAR.SetValue("");
            this.CBH02_J2CDDP.SetValue("");
            this.CBH02_J2CDAC.SetValue("");
            this.CBH02_J2CDJJ.SetValue("");
            this.TXT02_J2SEQ.SetValue("");
            this.TXT02_J1RKAC.SetValue("");
        }
        #endregion

        #region Description : 소모품비 세목 월예산 저장 버튼
        private void BTN62_SAV_Click(object sender, EventArgs e)
        {
            double dJ2CRAMT = 0;
            double dJ2PLAMT = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            for (int i = 0; i < 12; i++)
            {
                dJ2CRAMT = double.Parse(Get_Numeric(this.FPS91_TY_S_AC_2756M972.GetValue(i, "J2CRAMT").ToString()));
                dJ2PLAMT = double.Parse(Get_Numeric(this.FPS91_TY_S_AC_2756M972.GetValue(i, "J2PLAMT").ToString()));

                this.DbConnector.Attach
                    (
                    "TY_P_AC_279BP992",
                    this.TXT02_J2YEAR.GetValue().ToString(),
                    this.CBH02_J2CDDP.GetValue().ToString(),
                    this.CBH02_J2CDAC.GetValue().ToString(),
                    this.CBH02_J2CDJJ.GetValue().ToString(),
                    this.TXT02_J2SEQ.GetValue().ToString(),
                    (i + 1).ToString("00"),
                    dJ2CRAMT,
                    dJ2PLAMT
                    );
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");
            this.BTN61_INQ_Click(null, null);

            this.TXT02_J2YEAR.SetValue("");
            this.CBH02_J2CDDP.SetValue("");
            this.CBH02_J2CDAC.SetValue("");
            this.CBH02_J2CDJJ.SetValue("");
            this.TXT02_J2SEQ.SetValue("");
            this.TXT02_J1RKAC.SetValue("");
        }
        #endregion

        #region Description : 소모품비 세목예산 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;
            int j = 0;

            DataSet ds = new DataSet();

            // 등록 및 수정을 동시에 할 경우
            // 등록이 먼저이면 Tables[0]
            // 수정이 나중이면 Tables[1]임
            // 저장
            ds.Tables.Add(this.FPS91_TY_S_AC_2756L971.GetDataSourceInclude(TSpread.TActionType.New, "J1YEAR", "J1CDDP", "J1CDAC", "J1CDJJ", "J1SEQ", "J1RKAC"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_2756L971.GetDataSourceInclude(TSpread.TActionType.Update, "J1YEAR", "J1CDDP", "J1CDAC", "J1CDJJ", "J1SEQ", "J1RKAC"));

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

            // 저장 체크
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["J1CDDP"].ToString() != "A10000" &&
                    ds.Tables[0].Rows[i]["J1CDDP"].ToString() != "A50000" &&
                    ds.Tables[0].Rows[i]["J1CDDP"].ToString() != "A90000" &&
                    ds.Tables[0].Rows[i]["J1CDDP"].ToString() != "C10000" &&
                    ds.Tables[0].Rows[i]["J1CDDP"].ToString() != "G10000" &&
                    ds.Tables[0].Rows[i]["J1CDDP"].ToString() != "T10000" &&
                    ds.Tables[0].Rows[i]["J1CDDP"].ToString() != "T40000" &&
                    ds.Tables[0].Rows[i]["J1CDDP"].ToString() != "S10000" &&
                    ds.Tables[0].Rows[i]["J1CDDP"].ToString() != "S30200" &&
                    ds.Tables[0].Rows[i]["J1CDDP"].ToString() != "S40000" &&
                    ds.Tables[0].Rows[i]["J1CDDP"].ToString() != "O30000" &&
                    ds.Tables[0].Rows[i]["J1CDDP"].ToString() != "O40000" &&
                    ds.Tables[0].Rows[i]["J1CDDP"].ToString() != "O50000" &&
                    ds.Tables[0].Rows[i]["J1CDDP"].ToString() != "O60000" &&
                    ds.Tables[0].Rows[i]["J1CDDP"].ToString() != "B10000" &&
                    ds.Tables[0].Rows[i]["J1CDDP"].ToString() != "B20000" &&
                    ds.Tables[0].Rows[i]["J1CDDP"].ToString() != "B30000" &&
                    ds.Tables[0].Rows[i]["J1CDDP"].ToString() != "B40000" &&
                    ds.Tables[0].Rows[i]["J1CDDP"].ToString() != "B50000" &&
                    ds.Tables[0].Rows[i]["J1CDDP"].ToString() != "B60000" &&
                    ds.Tables[0].Rows[i]["J1CDDP"].ToString() != "B60100" &&
                    ds.Tables[0].Rows[i]["J1CDDP"].ToString() != "B70000" &&
                    ds.Tables[0].Rows[i]["J1CDDP"].ToString() != "B70100" &&
                    ds.Tables[0].Rows[i]["J1CDDP"].ToString() != "B10100" &&
                    ds.Tables[0].Rows[i]["J1CDDP"].ToString() != "B10200" &&
                    ds.Tables[0].Rows[i]["J1CDDP"].ToString() != "B20100" &&
                    ds.Tables[0].Rows[i]["J1CDDP"].ToString() != "B50100" &&
                    ds.Tables[0].Rows[i]["J1CDDP"].ToString() != "B80100" &&
                    ds.Tables[0].Rows[i]["J1CDDP"].ToString() != "B80200" &&
                    ds.Tables[0].Rows[i]["J1CDDP"].ToString() != "B80300"
                    )
                {
                    this.ShowMessage("TY_M_AC_2733W949");
                    e.Successed = false;
                    return;
                }

                if (ds.Tables[0].Rows[i]["J1RKAC"].ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_27636980");
                    e.Successed = false;
                    return;
                }

                // 순번 부여
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_27633981", ds.Tables[0].Rows[i]["J1YEAR"].ToString(),
                                                            ds.Tables[0].Rows[i]["J1CDDP"].ToString(),
                                                            ds.Tables[0].Rows[i]["J1CDAC"].ToString().Substring(0,8),
                                                            ds.Tables[0].Rows[i]["J1CDJJ"].ToString());

                ds.Tables[0].Rows[i]["J1SEQ"] = this.DbConnector.ExecuteScalar();

                if (Convert.ToInt16(ds.Tables[0].Rows[i]["J1SEQ"].ToString()) <= 1)
                {
                    //순번 입력
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_2763A982", ds.Tables[0].Rows[i]["J1YEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["J1CDDP"].ToString(),
                                                                ds.Tables[0].Rows[i]["J1CDAC"].ToString().Substring(0, 8),
                                                                ds.Tables[0].Rows[i]["J1CDJJ"].ToString(),
                                                                ds.Tables[0].Rows[i]["J1SEQ"].ToString()
                                                                );
                    this.DbConnector.ExecuteTranQuery();
                }
                else
                {
                    //순번 수정
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_2763B983", ds.Tables[0].Rows[i]["J1SEQ"].ToString(),
                                                                ds.Tables[0].Rows[i]["J1YEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["J1CDDP"].ToString(),
                                                                ds.Tables[0].Rows[i]["J1CDAC"].ToString().Substring(0, 8),
                                                                ds.Tables[0].Rows[i]["J1CDJJ"].ToString());
                    this.DbConnector.ExecuteTranQuery();
                }
            }

            // 수정 체크
            for (j = 0; j < ds.Tables[1].Rows.Count; j++)
            {
                if (ds.Tables[1].Rows[j]["J1RKAC"].ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_27636980");
                    e.Successed = false;
                    return;
                }
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 소모품비 세목예산 및 월예산 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            double dWkAMT = 0;

            DataTable dt = this.FPS91_TY_S_AC_2756L971.GetDataSourceInclude(TSpread.TActionType.Remove, "J1YEAR", "J1CDDP", "J1CDAC", "J1CDJJ", "J1SEQ", "J2USAMT");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            //집행금액 체크
            //집행금액이 있으면 삭제안됨
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                dWkAMT = Convert.ToDouble(dt.Rows[j]["J2USAMT"].ToString());

                if (dWkAMT > 0)
                {
                    this.ShowMessage("TY_M_AC_24C3F612");
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

        #region Description : 소모품비 세목 월예산 저장 ProcessCheck 이벤트
        private void BTN62_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_2756M972.GetDataSourceInclude(TSpread.TActionType.Update, "J2YEAR", "J2CDDP", "J2CDAC", "J2CDJJ", "J2SEQ", "T2MONTH", "J2CRAMT", "J2PLAMT", "J2FLAG"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }
            else
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["J2CRAMT"].ToString() == "" && ds.Tables[0].Rows[i]["J2PLAMT"].ToString() == "")
                    {
                        this.ShowMessage("TY_M_AC_27477965");
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

        #region Description : 소모품비 세목예산 스프레드 클릭 이벤트
        private void FPS91_TY_S_AC_2756L971_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.TXT02_J2YEAR.SetValue(this.FPS91_TY_S_AC_2756L971.GetValue("J1YEAR").ToString());
            this.CBH02_J2CDDP.DummyValue = this.TXT02_J2YEAR.GetValue().ToString() + "0101";
            this.CBH02_J2CDDP.SetValue(this.FPS91_TY_S_AC_2756L971.GetValue("J1CDDP").ToString());
            this.CBH02_J2CDAC.SetValue(this.FPS91_TY_S_AC_2756L971.GetValue("J1CDAC").ToString().Substring(0,8));
            this.CBH02_J2CDJJ.SetValue(this.FPS91_TY_S_AC_2756L971.GetValue("J1CDJJ").ToString());
            this.TXT02_J2SEQ.SetValue(this.FPS91_TY_S_AC_2756L971.GetValue("J1SEQ").ToString());
            this.TXT02_J1RKAC.SetValue(this.FPS91_TY_S_AC_2756L971.GetValue("J1RKAC").ToString());

            UP_SetReadOnly(true);

            UP_SetWoldMaster();
        }
        #endregion

        #region Description : 소모품비 세목여비교통비 세목 월예산 조회
        private void UP_SetWoldMaster()
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_2762E978",
                this.TXT02_J2YEAR.GetValue(),
                this.CBH02_J2CDDP.GetValue(),
                this.CBH02_J2CDAC.GetValue(),
                this.CBH02_J2CDJJ.GetValue(),
                this.TXT02_J2SEQ.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "02");

                if (dt.Rows.Count > 0)
                {
                    this.FPS91_TY_S_AC_2756M972.SetValue(UP_MonRowAdd(dt));                    
                }
                else
                {
                    this.FPS91_TY_S_AC_2756M972.SetValue(UP_NewRowAdd(dt));

                    for (int i = 0; i < this.FPS91_TY_S_AC_2756M972.CurrentRowCount; i++)
                    {
                        this.FPS91_TY_S_AC_2756M972.ActiveSheet.RowHeader.Cells[i, 0].Text = "U";
                    }
                }
            }
            else
            {
                this.FPS91_TY_S_AC_2756M972.SetValue(UP_NewRowAdd(dt));

                for (int i = 0; i < this.FPS91_TY_S_AC_2756M972.CurrentRowCount; i++)
                {
                    this.FPS91_TY_S_AC_2756M972.ActiveSheet.RowHeader.Cells[i, 0].Text = "U";
                }
            }

            UP_SumRowAdd();

            // 마지막 ROW 잠금
            this.FPS91_TY_S_AC_2756M972.ActiveSheet.Rows[this.FPS91_TY_S_AC_2756M972.ActiveSheet.Rows.Count - 1].Locked = true;
        }
        #endregion

        #region Description : 소모품비 세목여비교통비 세목 월예산 합계
        private DataTable UP_MonRowAdd(DataTable dt)
        {
            string sFilter = "";

            DataTable Rowdt = new DataTable();
            DataRow rw;

            Rowdt = dt.Clone();

            for (int i = 0; i < 12; i++)
            {
                rw = Rowdt.NewRow();

                rw["J2YEAR"]  = this.TXT02_J2YEAR.GetValue();
                rw["J2CDDP"]  = this.CBH02_J2CDDP.GetValue();
                rw["J2CDAC"]  = this.CBH02_J2CDAC.GetValue();
                rw["J2CDJJ"]  = this.CBH02_J2CDJJ.GetValue();
                rw["J2SEQ"]   = this.TXT02_J2SEQ.GetValue();
                rw["J2MONTH"] = (i + 1).ToString("00");
                
                sFilter = "J2MONTH = '"+(i + 1).ToString("00")+"'";
                rw["J2CRAMT"] =  Get_Numeric(dt.Compute("SUM(J2CRAMT)", sFilter).ToString());

                rw["J2PLAMT"] = Get_Numeric(dt.Compute("SUM(J2PLAMT)", sFilter).ToString());
                rw["J2USAMT"] = Get_Numeric(dt.Compute("SUM(J2USAMT)", sFilter).ToString());
                rw["J2JAMT"] = Get_Numeric(dt.Compute("SUM(J2JAMT)", sFilter).ToString());
                rw["J2FLAG"]  = "C";

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

                rw["J2YEAR"]  = this.TXT02_J2YEAR.GetValue();
                rw["J2CDDP"]  = this.CBH02_J2CDDP.GetValue();
                rw["J2CDAC"]  = this.CBH02_J2CDAC.GetValue();
                rw["J2CDJJ"]  = this.CBH02_J2CDJJ.GetValue();
                rw["J2SEQ"]   = Get_Numeric(this.TXT02_J2SEQ.GetValue().ToString());
                rw["J2MONTH"] = i.ToString("00");
                rw["J2CRAMT"] = 0;
                rw["J2PLAMT"] = 0;
                rw["J2USAMT"] = 0;
                rw["J2JAMT"]  = 0;
                rw["J2FLAG"]  = "A";

                Rowdt.Rows.Add(rw);
            }
            return Rowdt;
        }

        private void UP_SumRowAdd()
        {
            this.SpreadSumRowAdd(this.FPS91_TY_S_AC_2756M972, "J2MONTH", "합 계", Color.Yellow);
            this.FPS91_TY_S_AC_2756M972_Sheet1.SetFormula(
                FPS91_TY_S_AC_2756M972_Sheet1.RowCount - 1,
                FPS91_TY_S_AC_2756M972_Sheet1.ColumnCount - 2,
                "R[0]C[-3] + R[0]C[-2] - R[0]C[-1]"); //잔액 구하기        
        }
        #endregion

        #region Description : 소모품비 세목예산 스프레드 이벤트
        private void FPS91_TY_S_AC_2756L971_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            string sJ1GUBUN = string.Empty;

            if (e.Column == 1)
            {
                // 부서명을 가져오기 위해서 스프레드의 예산년도에 파라미터 날짜를 넣음.
                string year = FPS91_TY_S_AC_2756L971.GetValue(e.Row, "J1YEAR").ToString() + "0101";
                //((TCodeBoxCellType)FPS91_TY_S_AC_24917510.ActiveSheet.Columns["P1CDDP"].CellType).DummyValue = year;
                TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_2756L971, "J1CDDP");
                if (tyCodeBox != null)
                    tyCodeBox.DummyValue = year;
            }
            else if (e.Column == 4)
            {
                if (this.FPS91_TY_S_AC_2756L971.GetValue(e.Row, "WKGUBUN").ToString() != "C")
                {
                    sJ1GUBUN = this.FPS91_TY_S_AC_2756L971.GetValue(e.Row, "J1GUBUN").ToString();

                    DataTable dt = new DataTable();

                    if (sJ1GUBUN.ToString() == "1")
                    {
                        dt.Columns.Add("42413301");
                        dt.Columns.Add("44123301");
                        dt.Columns.Add("44113301");
                        dt.Columns.Add("44213301");

                        dt.Rows.Add(42413301, "42413301-소모성비품운영");
                        dt.Rows.Add(44123301, "44123301-소모성비품무역");
                        dt.Rows.Add(44113301, "44113301-소모성비품판매");
                        dt.Rows.Add(44213301, "44213301-소모성비품일반");
                    }
                    else if (sJ1GUBUN.ToString() == "2")
                    {
                        dt.Columns.Add("42413388");
                        dt.Columns.Add("44123388");
                        dt.Columns.Add("44113388");
                        dt.Columns.Add("44213388");

                        dt.Rows.Add(42413388, "42413388-기타소모품비운영");
                        dt.Rows.Add(44123388, "44123388-기타소모품비무역");
                        dt.Rows.Add(44113388, "44113388-기타소모품비판매");
                        dt.Rows.Add(44213388, "44213388-기타소모품비일반");
                    }
                    else if (sJ1GUBUN.ToString() == "3")  //연료대
                    {
                        dt.Columns.Add("42413306");
                        dt.Columns.Add("44123306");
                        dt.Columns.Add("44113306");
                        dt.Columns.Add("44213306");

                        dt.Rows.Add(42413306, "42413306-연료대(질소,LPG)운영");
                        dt.Rows.Add(44123306, "44123306-연료대(질소,LPG)무역");
                        dt.Rows.Add(44113306, "44113306-연료대(질소,LPG)판매");
                        dt.Rows.Add(44213306, "44213306-연료대(질소,LPG)일반");
                    }


                    this.FPS91_TY_S_AC_2756L971_Sheet1.Cells[e.Row, 4].CellType = new TComboBoxCellType(dt);
                }
            }
            else
            {
                return;
            }
        }
        #endregion

        #region Description : 사용자 정의 함수
        private void UP_SetReadOnly(bool bTrueAndFalse)
        {
            this.TXT02_J2YEAR.SetReadOnly(bTrueAndFalse);
            this.CBH02_J2CDDP.SetReadOnly(bTrueAndFalse);
            this.CBH02_J2CDAC.SetReadOnly(bTrueAndFalse);
            this.CBH02_J2CDJJ.SetReadOnly(bTrueAndFalse);
            this.TXT02_J2SEQ.SetReadOnly(bTrueAndFalse);
            this.TXT02_J1RKAC.SetReadOnly(bTrueAndFalse);
        }
        #endregion

        #region Description : 예산년도 이벤트
        private void TXT01_J1YEAR_TextChanged(object sender, EventArgs e)
        {
            // DummyValue = 파라미터를 넘길때 사용 됨.
            if (TXT01_J1YEAR.GetValue().ToString() != "")
            {
                this.CBH01_J1CDDP.DummyValue = TXT01_J1YEAR.GetValue() + "0101";
            }
            else
            {
                this.CBH01_J1CDDP.DummyValue = DateTime.Now.ToString("yyyyMMdd");
            }
        }
        #endregion
    }
}