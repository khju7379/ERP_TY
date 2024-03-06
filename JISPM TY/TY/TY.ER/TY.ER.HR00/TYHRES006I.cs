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
    /// EIS 직급별 예상인건비 등록 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.11.20 15:23
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_2BJ30469 : EIS 직급별 예상인건비 조회
    ///  TY_P_HR_2BK3T510 : EIS 직급별 예상인건비 삭제
    ///  TY_P_HR_2BK3U512 : EIS 직급별 예상인건비 등록
    ///  TY_P_HR_2BK3V514 : EIS 직급별 예상인건비 수정
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_2BK3T509 : EIS 직급별 예상인건비 조회
    ///  TY_S_HR_2BK3Y517 : EIS 직급별 예상인건비 등록
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
    ///  ELFJKCD : 직급
    ///  ELFSAUP : 사업부
    ///  ELFYYMM : 년월
    /// </summary>
    public partial class TYHRES006I : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRES006I()
        {
            InitializeComponent();

            this.SetPopupStyle();
 
        }

        private void TYHRES006I_Load(object sender, System.EventArgs e)
        {

            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);


            this.MTB01_ELFYYMM.SetValue(DateTime.Now.ToString("yyyyMMdd"));

            this.CBH01_ELFSAUP.DummyValue = this.MTB01_ELFYYMM.GetValue().ToString() + "0101";  

            this.BTN61_INQ_Click(null, null);

            this.UP_SetReadOnly(true); 

            this.SetStartingFocus(this.MTB01_ELFYYMM);
        }
        #endregion       

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_2BK3T509.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_HR_2BL1P529", this.MTB01_ELFYYMM.GetValue().ToString(), this.CBH01_ELFSAUP.GetValue(), this.CBH01_ELFJKCD.GetValue());
            this.FPS91_TY_S_HR_2BK3T509.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_HR_2BK3T509.CurrentRowCount > 0)
            {
                this.SetFocus(this.FPS91_TY_S_HR_2BK3T509);
            }
        }
        #endregion

        #region  Description :  FPS91_TY_S_HR_2BK3T509_CellDoubleClick 이벤트
        private void FPS91_TY_S_HR_2BK3T509_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {           

            UP_SetSreadMaster(this.FPS91_TY_S_HR_2BK3T509.GetValue("ELFYYMM").ToString().Substring(0, 4),
                              this.FPS91_TY_S_HR_2BK3T509.GetValue("ELFSAUP").ToString(),
                              this.FPS91_TY_S_HR_2BK3T509.GetValue("ELFJKCD").ToString());
            
            this.MTB02_ELFYYMM.SetValue(this.FPS91_TY_S_HR_2BK3T509.GetValue("ELFYYMM").ToString().Substring(0, 4));
            this.CBH02_ELFSAUP.DummyValue = MTB02_ELFYYMM.GetValue() + "0101";
            this.CBH02_ELFSAUP.SetValue(this.FPS91_TY_S_HR_2BK3T509.GetValue("ELFSAUP").ToString());
            this.CBH02_ELFJKCD.SetValue(this.FPS91_TY_S_HR_2BK3T509.GetValue("ELFJKCD").ToString());

            this.UP_SetReadOnly(false); 
        }
        #endregion

        #region  Description :  신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_2BK3Y517.Initialize();
 
            this.UP_SetReadOnly(false);
            
            this.Initialize_Controls("02");

            this.MTB02_ELFYYMM.SetValue(DateTime.Now.ToString("yyyy"));
            this.CBH02_ELFSAUP.DummyValue = this.MTB02_ELFYYMM.GetValue().ToString() + "0101";  

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_2BJ30469", "9999", this.CBH02_ELFSAUP.GetValue(), this.CBH02_ELFJKCD.GetValue());
            DataTable dt = this.DbConnector.ExecuteDataTable();
                        
            if (dt.Rows.Count <= 0)
            {
                this.FPS91_TY_S_HR_2BK3Y517.SetValue(UP_NewRowAdd(dt));
            }

            UP_SumRowAdd();

            for (int i = 0; i < this.FPS91_TY_S_HR_2BK3Y517.CurrentRowCount - 1; i++)
            {
                this.FPS91_TY_S_HR_2BK3Y517.ActiveSheet.RowHeader.Cells[i, 0].Text = "N";
            }           

            this.FPS91_TY_S_HR_2BK3Y517.ActiveSheet.Rows[this.FPS91_TY_S_HR_2BK3Y517.ActiveSheet.Rows.Count - 1].Locked = true;

            this.SetFocus(this.MTB02_ELFYYMM);            
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_2BK3T510", ds.Tables[0]);            
            this.DbConnector.ExecuteTranQueryList();
            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);

            UP_SetSreadMaster(MTB02_ELFYYMM.GetValue().ToString(), CBH02_ELFSAUP.GetValue().ToString(), CBH02_ELFJKCD.GetValue().ToString());

            UP_SetReadOnly(true);
            
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sELFJKGN = "";

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            if (CBH02_ELFJKCD.GetValue().ToString() == "01")
            {
                sELFJKGN = "1";
            }
            else if (CBH02_ELFJKCD.GetValue().ToString() == "3C" || CBH02_ELFJKCD.GetValue().ToString() == "3D" || CBH02_ELFJKCD.GetValue().ToString() == "6C")
            {
                sELFJKGN = "3";
            }
            else
            {
                sELFJKGN = "2";
            }

            this.DbConnector.CommandClear();
            //등록
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {                
                this.DbConnector.Attach("TY_P_HR_2BK3U512", MTB02_ELFYYMM.GetValue().ToString() + ds.Tables[0].Rows[i]["ELMM"].ToString(),
                                                               CBH02_ELFSAUP.GetValue(),
                                                               CBH02_ELFJKCD.GetValue(),
                                                               CBH02_ELFJKCD.GetText(),
                                                               sELFJKGN,
                                                               ds.Tables[0].Rows[i]["ELFPAYTOTAL"].ToString(),
                                                               ds.Tables[0].Rows[i]["ELFPEOPLE"].ToString(),
                                                               TYUserInfo.EmpNo);               
            }
            //수정
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_HR_2BK3V514", ds.Tables[1].Rows[i]["ELFPAYTOTAL"].ToString(),
                                                            ds.Tables[1].Rows[i]["ELFPEOPLE"].ToString(), 
                                                            TYUserInfo.EmpNo,
                                                            ds.Tables[1].Rows[i]["ELFYYMM"].ToString(),
                                                            ds.Tables[1].Rows[i]["ELFSAUP"].ToString(),
                                                            ds.Tables[1].Rows[i]["ELFJKCD"].ToString()
                                                               );
            }           

            this.DbConnector.ExecuteTranQueryList();
            this.ShowMessage("TY_M_GB_23NAD873");

            this.BTN61_INQ_Click(null, null);

            UP_SetSreadMaster(MTB02_ELFYYMM.GetValue().ToString(), CBH02_ELFSAUP.GetValue().ToString(), CBH02_ELFJKCD.GetValue().ToString());

            UP_SetReadOnly(true);
        }
        #endregion

        #region  Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion  
    
        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {         
            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_HR_2BK3Y517.GetDataSourceInclude(TSpread.TActionType.New, "ELMM", "ELFPAYTOTAL", "ELFPEOPLE"));
            ds.Tables.Add(this.FPS91_TY_S_HR_2BK3Y517.GetDataSourceInclude(TSpread.TActionType.Update,"ELFYYMM","ELFSAUP","ELFJKCD", "ELMM", "ELFPAYTOTAL", "ELFPEOPLE"));
            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
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
            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_HR_2BK3T509.GetDataSourceInclude(TSpread.TActionType.Remove, "ELFYYMM", "ELFSAUP", "ELFJKCD"));

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

        #region  Description : MTB02_ELFYYMM_TextChanged 이벤트
        private void MTB02_ELFYYMM_TextChanged(object sender, EventArgs e)
        {
            this.CBH02_ELFSAUP.DummyValue = this.MTB02_ELFYYMM.GetValue().ToString() + "0101";  
        }
        #endregion

        #region  Description : 사용자 정의 함수
        private void UP_SetReadOnly(bool bTrueAndFalse)
        {
            this.MTB02_ELFYYMM.SetReadOnly(bTrueAndFalse);
            this.CBH02_ELFSAUP.SetReadOnly(bTrueAndFalse);
            this.CBH02_ELFJKCD.SetReadOnly(bTrueAndFalse);
        }       

        private DataTable UP_NewRowAdd(DataTable dt)
        {
            DataTable Rowdt = new DataTable();
            DataRow rw;
            Rowdt = dt.Clone();
            for (int i = 1; i < 13; i++)
            {
                rw = Rowdt.NewRow();
                rw["ELMM"] = i.ToString("00");
                rw["ELFPAYTOTAL"] = 0;
                rw["ELFPEOPLE"] = 0;
                Rowdt.Rows.Add(rw);
            }
            return Rowdt;
        }

        private void UP_SumRowAdd()
        {
            this.SpreadSumRowAdd(this.FPS91_TY_S_HR_2BK3Y517, "ELMM", "합 계", Color.Yellow);
            this.FPS91_TY_S_HR_2BK3Y517_Sheet1.SetFormula(
                 FPS91_TY_S_HR_2BK3Y517_Sheet1.RowCount - 1,
                FPS91_TY_S_HR_2BK3Y517_Sheet1.ColumnCount - 1,
                "R[0]C[-4] - R[0]C[-3] - R[0]C[-2]"); //잔액 구하기        
        }

        private void UP_SetSreadMaster(string sELFYYMM, string sELFSAUP, string sELFJKCD)
        {
            FPS91_TY_S_HR_2BK3Y517.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_2BJ30469", sELFYYMM,sELFSAUP,sELFJKCD);
            this.FPS91_TY_S_HR_2BK3Y517.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_HR_2BK3Y517.CurrentRowCount > 0)
            {
                UP_SumRowAdd();
            }
        }
        #endregion

        #region  Description : MTB02_ELFYYMM_TextChanged 이벤트
        private void FPS91_TY_S_HR_2BK3T509_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.FPS91_TY_S_HR_2BK3T509_CellDoubleClick(null, null);
            }
        }
        #endregion
    }

}
