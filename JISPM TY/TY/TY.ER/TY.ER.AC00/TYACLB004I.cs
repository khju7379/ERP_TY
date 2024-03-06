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
    /// 세목예산관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.04.09 13:05
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_24915508 : 세목예산관리 조회
    ///  TY_P_AC_2491G520 : 세목코드 등록
    ///  TY_P_AC_2491H522 : 세목코드 수정
    ///  TY_P_AC_2491H523 : 세목코드 삭제
    ///  TY_P_AC_2491K528 : 세목 월예산 등록
    ///  TY_P_AC_2491M529 : 세목 월예산 수정
    ///  TY_P_AC_2491M530 : 세목 월예산 삭제
    ///  TY_P_AC_2491N531 : 세목순번 등록
    ///  TY_P_AC_2491O532 : 세목순번 수정
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_24917510 : 세목예산관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  P2CDAC : 계정과목
    ///  P2CDDP : 예산부서
    ///  P2YEAR : 예산년도
    /// </summary>
    public partial class TYACLB004I : TYBase
    {

        #region Description : 폼 Load
        public TYACLB004I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_24917510, "P1CDDP", "P1CDDPNM", "P1CDDP");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_24917510, "P1CDAC", "P1CDACNM", "P1CDAC");
        }

        private void TYACLB004I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN62_SAV.ProcessCheck += new TButton.CheckHandler(BTN62_SAV_ProcessCheck);

            this.TXT01_P1YEAR_TextChanged(null, null);

            UP_SetReadOnly(true);

            this.BTN61_INQ_Click(null, null);

            this.SetStartingFocus(this.CBH01_P1CDAC.CodeText);
            //TXT01_P1YEAR.Focus();

            this.SetStartingFocus(this.TXT01_P1YEAR);
        }
        #endregion

        #region Description : 조회 ProcessCheck 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_24915508", this.ControlFactory, "01");
            this.FPS91_TY_S_AC_24917510.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;  

            this.DbConnector.CommandClear();
            //세목코드 삭제
            this.DbConnector.Attach("TY_P_AC_2491H523", dt);
            //세목월예산 삭제
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 1; j < 13; j++)
                {
                    this.DbConnector.Attach("TY_P_AC_2491M530", dt.Rows[i]["P1YEAR"].ToString(),
                                                                dt.Rows[i]["P1CDDP"].ToString(),
                                                                dt.Rows[i]["P1CDAC"].ToString(),
                                                                dt.Rows[i]["P1SEQ"].ToString(),
                                                                j.ToString("00"));
                    //본예산 삭제
                    //this.DbConnector.Attach("TY_P_AC_24C2E601", dt.Rows[i]["P1YEAR"].ToString(),
                   //                                             j.ToString("00"),
                    //                                            dt.Rows[i]["P1CDDP"].ToString(),
                     //                                           dt.Rows[i]["P1CDAC"].ToString()
                    //                                            );

                    this.DbConnector.Attach("TY_P_AC_32C4G049", "U",
                                                                          "C",
                                                                          TYUserInfo.EmpNo,
                                                                          dt.Rows[i]["P1YEAR"].ToString(),
                                                                          dt.Rows[i]["P1CDDP"].ToString(),
                                                                          dt.Rows[i]["P1CDAC"].ToString());
                }
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");
            this.BTN61_INQ_Click(null, null);

            this.Initialize_Controls("02");
            FPS91_TY_S_AC_2493K556.Initialize(); 
        }
        #endregion

        #region Description : 저장 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;
                                                
            //세목코드(APPSCDF) 저장
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2491G520", ds.Tables[0]);
            //세목월예산(APPSLSF) 저장
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                for (int j = 1; j < 13; j++)
                {
                    this.DbConnector.Attach("TY_P_AC_2491K528", ds.Tables[0].Rows[i]["P1YEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["P1CDDP"].ToString(),
                                                                ds.Tables[0].Rows[i]["P1CDAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["P1SEQ"].ToString(),
                                                                j.ToString("00"),
                                                                "0", "0", "N");                 
                    //본예산 등록
                    this.DbConnector.Attach("TY_P_AC_24C9K597", ds.Tables[0].Rows[i]["P1YEAR"].ToString(),
                                                                j.ToString("00"),
                                                                ds.Tables[0].Rows[i]["P1CDDP"].ToString(),
                                                                ds.Tables[0].Rows[i]["P1CDAC"].ToString(),
                                                                "0", "0", "0", "0", "I", TYUserInfo.EmpNo,
                                                                ds.Tables[0].Rows[i]["P1YEAR"].ToString(),
                                                                j.ToString("00"),
                                                                ds.Tables[0].Rows[i]["P1CDDP"].ToString(),
                                                                ds.Tables[0].Rows[i]["P1CDAC"].ToString());                 
                }
            }           

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");
            this.BTN61_INQ_Click(null, null);

        }
        #endregion

        #region Description : 저장(세목예산 월) 이벤트
        private void BTN62_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["P2FLAG"].ToString().Trim() == "A")
                {                    
                    this.DbConnector.Attach("TY_P_AC_2491K528", ds.Tables[0]); //저장(월예산관리)
                }
                else
                {
                    this.DbConnector.Attach("TY_P_AC_2491M529", ds.Tables[0]); //수정(월예산관리)
                }
                //본예산 등록
                this.DbConnector.Attach("TY_P_AC_24C9K597", ds.Tables[0].Rows[i]["P2YEAR"].ToString(),
                                                               ds.Tables[0].Rows[i]["P2MONTH"].ToString(),
                                                               ds.Tables[0].Rows[i]["P2CDDP"].ToString(),
                                                               ds.Tables[0].Rows[i]["P2CDAC"].ToString(),
                                                               ds.Tables[0].Rows[i]["P2CRAMT"].ToString(),
                                                               ds.Tables[0].Rows[i]["P2PLAMT"].ToString(),
                                                               "0", "0", "I", TYUserInfo.EmpNo,
                                                               ds.Tables[0].Rows[i]["P2YEAR"].ToString(),
                                                               ds.Tables[0].Rows[i]["P2MONTH"].ToString(),
                                                               ds.Tables[0].Rows[i]["P2CDDP"].ToString(),
                                                               ds.Tables[0].Rows[i]["P2CDAC"].ToString());
                ////본예산 수정
                //this.DbConnector.Attach("TY_P_AC_24C9L598", ds.Tables[0].Rows[i]["P2CRAMT"].ToString(),
                //                                            ds.Tables[0].Rows[i]["P2PLAMT"].ToString(),
                //                                            "U",
                //                                            "C",
                //                                            TYUserInfo.EmpNo,
                //                                            ds.Tables[0].Rows[i]["P2YEAR"].ToString(),
                //                                            ds.Tables[0].Rows[i]["P2MONTH"].ToString(),
                //                                            ds.Tables[0].Rows[i]["P2CDDP"].ToString(),
                //                                            ds.Tables[0].Rows[i]["P2CDAC"].ToString());

            }
            this.DbConnector.ExecuteTranQueryList();

            this.DbConnector.CommandClear();
            //본예산 수정
            this.DbConnector.Attach("TY_P_AC_32C4G049", "U",
                                                        "C",
                                                        TYUserInfo.EmpNo,
                                                        ds.Tables[0].Rows[0]["P2YEAR"].ToString(),
                                                        ds.Tables[0].Rows[0]["P2CDDP"].ToString(),
                                                        ds.Tables[0].Rows[0]["P2CDAC"].ToString());
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");
            this.BTN61_INQ_Click(null, null);

            this.UP_SetGridMaster();


        }
        #endregion

        #region Description : Spread  CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_24917510_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.TXT02_P2YEAR.SetValue(this.FPS91_TY_S_AC_24917510.GetValue("P1YEAR").ToString());
            this.CBH02_P2CDDP.DummyValue = this.TXT02_P2YEAR.GetValue().ToString() + "0101";  
            this.CBH02_P2CDDP.SetValue(this.FPS91_TY_S_AC_24917510.GetValue("P1CDDP").ToString());
            this.CBH02_P2CDAC.SetValue(this.FPS91_TY_S_AC_24917510.GetValue("P1CDAC").ToString());
            this.TXT02_P2SEQ.SetValue(Set_Fill3(this.FPS91_TY_S_AC_24917510.GetValue("P1SEQ").ToString()));

            UP_SetReadOnly(true);

            UP_SetGridMaster();            
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_AC_24917510.GetDataSourceInclude(TSpread.TActionType.New, "P1YEAR", "P1CDDP", "P1CDAC", "P1SEQ", "P1RKAC"));

            if (ds.Tables[0].Rows.Count == 0 )
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

            //신규 계정체크
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (//----  국내 출장  ----//
                    ds.Tables[0].Rows[i]["P1CDAC"].ToString() == "42411201" ||
                    ds.Tables[0].Rows[i]["P1CDAC"].ToString() == "44121201" ||
                    ds.Tables[0].Rows[i]["P1CDAC"].ToString() == "44111201" ||
                    ds.Tables[0].Rows[i]["P1CDAC"].ToString() == "44211201" ||
                    //----  국외 출장  ----//
                    ds.Tables[0].Rows[i]["P1CDAC"].ToString() == "42411202" ||
                    ds.Tables[0].Rows[i]["P1CDAC"].ToString() == "44121202" ||
                    ds.Tables[0].Rows[i]["P1CDAC"].ToString() == "44111202" ||
                    ds.Tables[0].Rows[i]["P1CDAC"].ToString() == "44211202" ||
                    //----  소모성 비품  ----//
                    ds.Tables[0].Rows[i]["P1CDAC"].ToString() == "42413301" ||
                    ds.Tables[0].Rows[i]["P1CDAC"].ToString() == "44123301" ||
                    ds.Tables[0].Rows[i]["P1CDAC"].ToString() == "44113301" ||
                    ds.Tables[0].Rows[i]["P1CDAC"].ToString() == "44213301"
                   )
                {
                    this.ShowMessage("TY_M_AC_2733V948");
                    e.Successed = false;
                    return;
                }
            }

            //신규
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //자산수번 부여                
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_24A1M575", ds.Tables[0].Rows[i]["P1YEAR"].ToString(),
                                                            ds.Tables[0].Rows[i]["P1CDDP"].ToString(),
                                                            ds.Tables[0].Rows[i]["P1CDAC"].ToString());
                ds.Tables[0].Rows[i]["P1SEQ"] = this.DbConnector.ExecuteScalar();

                if (Convert.ToInt16(ds.Tables[0].Rows[i]["P1SEQ"].ToString()) <= 1)
                {
                    //순번 입력
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_2491N531", ds.Tables[0].Rows[i]["P1YEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["P1CDDP"].ToString(),
                                                                ds.Tables[0].Rows[i]["P1CDAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["P1SEQ"].ToString());
                    this.DbConnector.ExecuteTranQuery();
                }
                else
                {
                    //순번 수정
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_2491O532", ds.Tables[0].Rows[i]["P1SEQ"].ToString(),
                                                                ds.Tables[0].Rows[i]["P1YEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["P1CDDP"].ToString(),
                                                                ds.Tables[0].Rows[i]["P1CDAC"].ToString());
                    this.DbConnector.ExecuteTranQuery();
                }
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN62_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            if (
                //----  국내 출장  ----//
                (this.CBH02_P2CDAC.GetValue().ToString() == "42411201") ||
                (this.CBH02_P2CDAC.GetValue().ToString() == "44121201") ||
                (this.CBH02_P2CDAC.GetValue().ToString() == "44111201") ||
                (this.CBH02_P2CDAC.GetValue().ToString() == "44211201") ||
                //----  국외 출장  ----//
                (this.CBH02_P2CDAC.GetValue().ToString() == "42411202") ||
                (this.CBH02_P2CDAC.GetValue().ToString() == "44121202") ||
                (this.CBH02_P2CDAC.GetValue().ToString() == "44111202") ||
                (this.CBH02_P2CDAC.GetValue().ToString() == "44211202") ||
                //----  소모성 비품  ----//
                (this.CBH02_P2CDAC.GetValue().ToString() == "42413301") ||
                (this.CBH02_P2CDAC.GetValue().ToString() == "44123301") ||
                (this.CBH02_P2CDAC.GetValue().ToString() == "44113301") ||
                (this.CBH02_P2CDAC.GetValue().ToString() == "44213301") 
                 )
            {
                this.ShowMessage("TY_M_AC_2733V948");
                e.Successed = false;
                return;
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_AC_2493K556.GetDataSourceInclude(TSpread.TActionType.Update, "P2YEAR", "P2CDDP", "P2CDAC", "P2SEQ","P2MONTH","P2CRAMT", "P2PLAMT", "P2FLAG"));

            if (ds.Tables[0].Rows.Count == 0)
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

            DataTable dt = this.FPS91_TY_S_AC_24917510.GetDataSourceInclude(TSpread.TActionType.Remove, "P1YEAR", "P1CDDP", "P1CDAC", "P1SEQ");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            //집행금액 체크
            //집행금액이 있으면 삭제안됨
            this.DbConnector.CommandClear();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                this.DbConnector.Attach("TY_P_AC_24C3A609", dt.Rows[j].ItemArray);
            }
            DataSet dsChk = this.DbConnector.ExecuteDataSet();

            for (int i = 0; i < dsChk.Tables.Count; i++)
            {
                dWkAMT = Convert.ToDouble(dsChk.Tables[i].Rows[0]["P1USAMT"].ToString());
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

        #region Description : 사용자 정의 함수
        private void UP_SetReadOnly(bool bTrueAndFalse)
        {
            this.TXT02_P2YEAR.SetReadOnly(bTrueAndFalse);
            this.CBH02_P2CDDP.SetReadOnly(bTrueAndFalse);
            this.CBH02_P2CDAC.SetReadOnly(bTrueAndFalse);
            this.TXT02_P2SEQ.SetReadOnly(bTrueAndFalse);
            this.TXT02_P2SEQNM.SetReadOnly(bTrueAndFalse);
        }

        private void UP_SetGridMaster()
        {          
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2493J555", this.TXT02_P2YEAR.GetValue(), this.CBH02_P2CDDP.GetValue(), this.CBH02_P2CDAC.GetValue(), this.TXT02_P2SEQ.GetValue());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "02");
                if (dt.Rows.Count > 0)
                {
                    this.FPS91_TY_S_AC_2493K556.SetValue(UP_MonRowAdd(dt));
                }
                else
                {
                    this.FPS91_TY_S_AC_2493K556.SetValue(UP_NewRowAdd(dt));                   
                }

                UP_SumRowAdd();
            }
        }

        private void UP_SumRowAdd()
        {
            this.SpreadSumRowAdd(this.FPS91_TY_S_AC_2493K556, "P2MONTH", "합 계", Color.Yellow);
            this.FPS91_TY_S_AC_2493K556_Sheet1.SetFormula(
                FPS91_TY_S_AC_2493K556_Sheet1.RowCount - 1,
                FPS91_TY_S_AC_2493K556_Sheet1.ColumnCount - 2,
                "R[0]C[-3] + R[0]C[-2] - R[0]C[-1]"); //잔액 구하기
        }

        private DataTable UP_NewRowAdd(DataTable dt)
        {
            DataTable Rowdt = new DataTable();
            DataRow rw;
            Rowdt = dt.Clone();
            for (int i = 1; i < 13; i++)
            {
                rw = Rowdt.NewRow();
                rw["P2MONTH"] = i.ToString("00");
                rw["P2CRAMT"] = DBNull.Value;
                rw["P2PLAMT"] = 0;                
                rw["P2USAMT"] = 0;
                rw["P2JANAMT"] = 0;
                rw["P2FLAG"] = "A";
                Rowdt.Rows.Add(rw);
            }
            return Rowdt;
        }       

        private DataTable UP_MonRowAdd(DataTable dt)
        {
            DataTable Rowdt = new DataTable();
            DataRow rw;

            Rowdt = dt.Clone();

            for (int i = 1; i < 13; i++)
            {
                rw = Rowdt.NewRow();
                rw["P2YEAR"] = dt.Rows[0]["P2YEAR"].ToString();
                rw["P2MONTH"] = i.ToString("00");
                rw["P2CDDP"] = dt.Rows[0]["P2CDDP"].ToString();
                rw["P2CDDPNM"] = dt.Rows[0]["P2CDDPNM"].ToString();
                rw["P2CDAC"] = dt.Rows[0]["P2CDAC"].ToString();
                rw["P2CDACNM"] = dt.Rows[0]["P2CDACNM"].ToString();
                rw["P2SEQ"] = dt.Rows[0]["P2SEQ"].ToString();
                rw["P2SEQNM"] = dt.Rows[0]["P2SEQNM"].ToString();
                rw["P2CRAMT"] = Get_Numeric(dt.Compute("Sum(P2CRAMT)", "P2MONTH = " + i.ToString("00")).ToString());
                rw["P2PLAMT"] = Get_Numeric(dt.Compute("Sum(P2PLAMT)", "P2MONTH = " + i.ToString("00")).ToString());
                rw["P2USAMT"] = Get_Numeric(dt.Compute("Sum(P2USAMT)", "P2MONTH = " + i.ToString("00")).ToString());
                rw["P2JANAMT"] = Convert.ToDouble(Get_Numeric(dt.Compute("Sum(P2CRAMT)", "P2MONTH <= " + i.ToString("00")).ToString())) +
                                 Convert.ToDouble(Get_Numeric(dt.Compute("Sum(P2PLAMT)", "P2MONTH <= " + i.ToString("00")).ToString())) -
                                 Convert.ToDouble(Get_Numeric(dt.Compute("Sum(P2USAMT)", "P2MONTH <= " + i.ToString("00")).ToString()));
                rw["P2FLAG"] = dt.Rows[0]["P2FLAG"].ToString();
                Rowdt.Rows.Add(rw);
            }

            return Rowdt;
        }        
        #endregion

        #region Description : TXT01_P1YEAR_TextChanged 이벤트
        private void TXT01_P1YEAR_TextChanged(object sender, EventArgs e)
        {
            if( TXT01_P1YEAR.GetValue().ToString() != "" )
            {
               this.CBH01_P1CDDP.DummyValue = TXT01_P1YEAR.GetValue()+"0101";
            }
            else
            {
                this.CBH01_P1CDDP.DummyValue = DateTime.Now.ToString("yyyyMMdd");
            }
        }
        #endregion

        #region Description : FPS91_TY_S_AC_24917510_EnterCell 이벤트
        private void FPS91_TY_S_AC_24917510_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            if (e.Column != 1)
                return;

            string year = FPS91_TY_S_AC_24917510.GetValue(e.Row, "P1YEAR").ToString() + "0101";
            //((TCodeBoxCellType)FPS91_TY_S_AC_24917510.ActiveSheet.Columns["P1CDDP"].CellType).DummyValue = year;
            TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_24917510, "P1CDDP");
            if (tyCodeBox != null)
                tyCodeBox.DummyValue = year;
        }
        #endregion
    }
}
