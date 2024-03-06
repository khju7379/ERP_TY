using System;
using System.Data;
using System.Drawing;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// EIS 사업부별 인건비 예산 등록 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.11.22 11:44
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_2BMBW567 : EIS 사업부별 예상인건비 집계 조회
    ///  TY_P_HR_2BMC0569 : EIS 사업부별 예산인건비 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_2BMBX568 : EIS 사업부별 예상인건비 집계 조회
    ///  TY_S_HR_2BMC3570 : EIS 사업부별 예상인건비 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  SAV : 저장
    ///  ELXCDAC : 계정과목
    ///  ELXSAUP : 사업부
    ///  ELXYYMM : 기준년도
    ///  ELXGUBN : 구분(임원,직원)
    ///  INQOPTION : 조회구분
    /// </summary>
    public partial class TYHRES007I : TYBase
    {
        public TYHRES007I()
        {
            InitializeComponent();
            this.SetPopupStyle();
        }

        private void TYHRES007I_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_2BMC3570, "ELXMM");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_2BMC3570, "ELXCRAMT");

            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);


            this.MTB01_ELXYYMM.SetValue(DateTime.Now.ToString("yyyy"));

            this.CBH01_ELXSAUP.DummyValue = this.MTB01_ELXYYMM.GetValue().ToString() + "0101";

            this.BTN61_INQ_Click(null, null);

            this.UP_SetReadOnly(true);

            this.BTN61_SAV.Visible = false;

            this.SetStartingFocus(this.MTB01_ELXYYMM);
        }

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_2BMBX568.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_HR_2BMBW567", this.MTB01_ELXYYMM.GetValue().ToString().Substring(0, 4), this.CBH01_ELXSAUP.GetValue(), this.CBH01_ELXCDAC.GetValue(), CBO01_INQOPTION.GetValue());
            this.FPS91_TY_S_HR_2BMBX568.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_HR_2BMBX568.CurrentRowCount > 0)
            {
                this.SetFocus(this.FPS91_TY_S_HR_2BMBX568);
            }

            this.UP_SetReadOnly(true);
            this.BTN61_SAV.Visible = false;
        }
        #endregion

        #region  Description :  신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_2BMC3570.Initialize();

            this.UP_SetReadOnly(false);

            this.Initialize_Controls("02");

            this.MTB02_ELXYYMM.SetValue(DateTime.Now.ToString("yyyy"));
            this.CBH02_ELXSAUP.DummyValue = this.MTB02_ELXYYMM.GetValue().ToString() + "0101";

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_2BMC0569", "9999", this.CBH02_ELXSAUP.GetValue(), this.CBH02_ELXCDAC.GetValue(), this.CBO02_ELXGUBN.GetValue());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.FPS91_TY_S_HR_2BMC3570.SetValue(UP_NewRowAdd(dt));
            }

            UP_SumRowAdd();

            for (int i = 0; i < this.FPS91_TY_S_HR_2BMC3570.CurrentRowCount - 1; i++)
            {
                this.FPS91_TY_S_HR_2BMC3570.ActiveSheet.RowHeader.Cells[i, 0].Text = "N";
            }

            this.FPS91_TY_S_HR_2BMC3570.ActiveSheet.Rows[this.FPS91_TY_S_HR_2BMC3570.ActiveSheet.Rows.Count - 1].Locked = true;

            this.BTN61_SAV.Visible = true;

            this.SetFocus(this.MTB02_ELXYYMM);
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_2BM31574", ds.Tables[0]);
            this.DbConnector.ExecuteTranQueryList();
            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);

            this.BTN61_SAV.Visible = false;

            UP_SetReadOnly(true);
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            //등록
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_HR_2BM30573", MTB02_ELXYYMM.GetValue().ToString().Substring(0, 4) + ds.Tables[0].Rows[i]["ELXMM"].ToString(),
                                                            CBH02_ELXSAUP.GetValue(),
                                                            CBH02_ELXCDAC.GetValue(),
                                                            CBO02_ELXGUBN.GetValue(),
                                                            ds.Tables[0].Rows[i]["ELXCRAMT"].ToString(),
                                                            TYUserInfo.EmpNo);
            }
            //수정
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_HR_2BM33575", ds.Tables[1].Rows[i]["ELXCRAMT"].ToString(),
                                                            TYUserInfo.EmpNo,
                                                            ds.Tables[1].Rows[i]["ELXYYMM"].ToString(),
                                                            ds.Tables[1].Rows[i]["ELXSAUP"].ToString(),
                                                            ds.Tables[1].Rows[i]["ELXCDAC"].ToString(),
                                                            ds.Tables[1].Rows[i]["ELXGUBN"].ToString()
                                                               );
            }

            this.DbConnector.ExecuteTranQueryList();
            this.ShowMessage("TY_M_GB_23NAD873");

            this.BTN61_INQ_Click(null, null);

            UP_SetSreadMaster(MTB02_ELXYYMM.GetValue().ToString().Substring(0, 4), CBH02_ELXSAUP.GetValue().ToString(), CBH02_ELXCDAC.GetValue().ToString(), CBO02_ELXGUBN.GetValue().ToString());

            this.BTN61_SAV.Visible = false;

            UP_SetReadOnly(true);
        }
        #endregion


        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_HR_2BMC3570.GetDataSourceInclude(TSpread.TActionType.New, "ELXMM", "ELXCRAMT"));
            ds.Tables.Add(this.FPS91_TY_S_HR_2BMC3570.GetDataSourceInclude(TSpread.TActionType.Update, "ELXYYMM", "ELXSAUP", "ELXCDAC", "ELXGUBN", "ELXCRAMT"));
            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            //중복 체크
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_2BMC0569", MTB02_ELXYYMM.GetValue().ToString().Substring(0, 4),
                                                                CBH02_ELXSAUP.GetValue(),
                                                                CBH02_ELXCDAC.GetValue(),
                                                                CBO02_ELXGUBN.GetValue());
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_GB_23S40973");
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

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_HR_2BMBX568.GetDataSourceInclude(TSpread.TActionType.Remove, "ELXYYMM", "ELXSAUP", "ELXCDAC", "ELXGUBN"));

            if (ds.Tables[0].Rows.Count == 0)
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

            e.ArgData = ds;
        }
        #endregion

        #region  Description : 사용자 정의 함수
        private void UP_SetReadOnly(bool bTrueAndFalse)
        {
            this.MTB02_ELXYYMM.SetReadOnly(bTrueAndFalse);
            this.CBH02_ELXSAUP.SetReadOnly(bTrueAndFalse);
            this.CBH02_ELXCDAC.SetReadOnly(bTrueAndFalse);
            this.CBO02_ELXGUBN.SetReadOnly(bTrueAndFalse);            
        }

        private DataTable UP_NewRowAdd(DataTable dt)
        {
            DataTable Rowdt = new DataTable();
            DataRow rw;
            Rowdt = dt.Clone();
            for (int i = 1; i < 13; i++)
            {
                rw = Rowdt.NewRow();
                rw["ELXMM"] = i.ToString("00");
                rw["ELXCRAMT"] = 0;
                Rowdt.Rows.Add(rw);
            }
            return Rowdt;
        }

        private void UP_SumRowAdd()
        {
            this.SpreadSumRowAdd(this.FPS91_TY_S_HR_2BMC3570, "ELXMM", "합 계", Color.Yellow);
            this.FPS91_TY_S_HR_2BMC3570_Sheet1.SetFormula(
                 FPS91_TY_S_HR_2BMC3570_Sheet1.RowCount - 1,
                FPS91_TY_S_HR_2BMC3570_Sheet1.ColumnCount - 1,
                "R[-12]C[0] + R[-11]C[0] + R[-10]C[0]+ R[-9]C[0]+ R[-8]C[0]+ R[-7]C[0]+ R[-6]C[0]+ R[-5]C[0]+ R[-4]C[0]+ R[-3]C[0]+ R[-2]C[0]+ R[-1]C[0]"); //잔액 구하기        
        }

        private void UP_SetSreadMaster(string sELXYYMM, string sELXSAUP, string sELXCDAC, string sELXGUBN)
        {
            FPS91_TY_S_HR_2BMC3570.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_2BMC0569", sELXYYMM, sELXSAUP, sELXCDAC, sELXGUBN);
            this.FPS91_TY_S_HR_2BMC3570.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_HR_2BMC3570.CurrentRowCount > 0)
            {
                UP_SumRowAdd();
            }
        }
        #endregion

        #region  Description : FPS91_TY_S_HR_2BMBX568_CellDoubleClick 이벤트
        private void FPS91_TY_S_HR_2BMBX568_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            UP_SetSreadMaster(this.FPS91_TY_S_HR_2BMBX568.GetValue("ELXYYMM").ToString().Substring(0, 4),
                              this.FPS91_TY_S_HR_2BMBX568.GetValue("ELXSAUP").ToString(),
                              this.FPS91_TY_S_HR_2BMBX568.GetValue("ELXCDAC").ToString(),
                              this.FPS91_TY_S_HR_2BMBX568.GetValue("ELXGUBN").ToString());

            this.MTB02_ELXYYMM.SetValue(this.FPS91_TY_S_HR_2BMBX568.GetValue("ELXYYMM").ToString().Substring(0, 4));
            this.CBH02_ELXSAUP.DummyValue = MTB02_ELXYYMM.GetValue().ToString().Substring(0, 4) + "0101";
            this.CBH02_ELXSAUP.SetValue(this.FPS91_TY_S_HR_2BMBX568.GetValue("ELXSAUP").ToString());
            this.CBH02_ELXCDAC.SetValue(this.FPS91_TY_S_HR_2BMBX568.GetValue("ELXCDAC").ToString());
            this.CBO02_ELXGUBN.SetValue(this.FPS91_TY_S_HR_2BMBX568.GetValue("ELXGUBN").ToString());

            this.BTN61_SAV.Visible = true;

            this.UP_SetReadOnly(false);
        }
        #endregion

        #region  Description : FPS91_TY_S_HR_2BMBX568_KeyPress 이벤트
        private void FPS91_TY_S_HR_2BMBX568_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.FPS91_TY_S_HR_2BMBX568_CellDoubleClick(null, null);
            }
        }
        #endregion
    }
}
