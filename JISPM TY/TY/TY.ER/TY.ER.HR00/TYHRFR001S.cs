using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;

namespace TY.ER.HR00
{
    /// <summary>
    /// 연장관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2014.11.25 16:53
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4COI3981 : 급여징수관리 조회
    ///  TY_P_HR_519HW082 : 급여징수관리 삭제
    ///  TY_P_HR_51C94086 : 급여징수관리 등록
    ///  TY_P_HR_51C95087 : 급여징수관리 수정
    ///  TY_P_HR_51CD1092 : 급여이체자료 존재 체크
    ///  TY_P_HR_53I90693 : 급여징수관리 존재 확인
    ///  TY_P_HR_53I9P694 : 급여징수관리 내역자료 존재확인
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_4COHZ979 : 급여징수관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_23S40973 : 동일한 코드가 존재합니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    ///  TY_M_HR_4COHY978 : 이체자료가 존재하므로 삭제를 할 수가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  SAV_BATCH : 일괄등록
    ///  COGUBN : 이체구분
    ///  COPYGUBN : 급여구분
    ///  COSABUN : 사번
    /// </summary>
    public partial class TYHRFR001S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRFR001S()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_4COHZ979, "COSABUN", "COSABUNNM", "COSABUN");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_4COHZ979, "COPYGUBN", "COPYGUBNNM", "COPYGUBN");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_4COHZ979, "COGUBN", "COGUBNNM", "COGUBN");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_4COHZ979, "COINSABUN", "COINSABUNNM", "COINSABUN");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_4COHZ979, "COINCDBK", "COINCDBKNM", "COINCDBK"); 
        }

        private void TYHRFR001S_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4COHZ979, "COSABUN");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4COHZ979, "COGUBN");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4COHZ979, "COPYGUBN");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4COHZ979, "COSDATE");

            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4COHZ979, "COSABUN");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4COHZ979, "COGUBN");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4COHZ979, "COPYGUBN");

            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4COHZ979, "BTNDETAIL");

            (this.FPS91_TY_S_HR_4COHZ979.Sheets[0].Columns[15].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.magnifier;

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            
            this.SetStartingFocus(this.CBH01_COGUBN);

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_4COHZ979.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4COI3981", this.CBH01_COSABUN.GetValue(),                                                                                                                
                                                        this.CBH01_COGUBN.GetValue(),
                                                        this.CBH01_COPYGUBN.GetValue(),
                                                        this.CBO01_INQOPTION.GetValue()
                                                        );
            this.FPS91_TY_S_HR_4COHZ979.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DataTableColumnAdd(ds.Tables[0], "COHISAB", TYUserInfo.EmpNo);
            this.DataTableColumnAdd(ds.Tables[1], "COHISAB", TYUserInfo.EmpNo);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_51C94086", ds.Tables[0].Rows[i]["COSABUN"].ToString(),
                                                                ds.Tables[0].Rows[i]["COGUBN"].ToString(),
                                                                ds.Tables[0].Rows[i]["COPYGUBN"].ToString(),
                                                                ds.Tables[0].Rows[i]["COSDATE"].ToString(),
                                                                ds.Tables[0].Rows[i]["COEDATE"].ToString().Replace("19000101", "").ToString(),
                                                                ds.Tables[0].Rows[i]["COFLAG"].ToString(),
                                                                (ds.Tables[0].Rows[i]["COFLAG"].ToString() == "1" ? ds.Tables[0].Rows[i]["COAMT"].ToString() : "0"),
                                                                ds.Tables[0].Rows[i]["COINSABUN"].ToString(),
                                                                ds.Tables[0].Rows[i]["COINCDBK"].ToString(),
                                                                ds.Tables[0].Rows[i]["COINNOAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["COHISAB"].ToString()
                                                                );
                }
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {                   

                    this.DbConnector.Attach("TY_P_HR_51C95087", ds.Tables[1].Rows[i]["COEDATE"].ToString().Replace("19000101", "").ToString(),
                                                                ds.Tables[1].Rows[i]["COFLAG"].ToString(),
                                                                (ds.Tables[1].Rows[i]["COFLAG"].ToString() == "1" ? ds.Tables[1].Rows[i]["COAMT"].ToString() : "0"),
                                                                ds.Tables[1].Rows[i]["COINSABUN"].ToString(),
                                                                ds.Tables[1].Rows[i]["COINCDBK"].ToString(),
                                                                ds.Tables[1].Rows[i]["COINNOAC"].ToString(),
                                                                ds.Tables[1].Rows[i]["COHISAB"].ToString(),
                                                                ds.Tables[1].Rows[i]["COSABUN"].ToString(),
                                                                ds.Tables[1].Rows[i]["COGUBN"].ToString(),
                                                                ds.Tables[1].Rows[i]["COPYGUBN"].ToString(),
                                                                ds.Tables[1].Rows[i]["COSDATE"].ToString()
                                                                );
                }
            }


            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD873");
        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            Int16 iCnt = 0;
            
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_HR_4COHZ979.GetDataSourceInclude(TSpread.TActionType.New, "COSABUN", "COGUBN", "COPYGUBN", "COSDATE", "COEDATE", "COFLAG", "COAMT", "COINSABUN", "COINCDBK", "COINNOAC"));
            ds.Tables.Add(this.FPS91_TY_S_HR_4COHZ979.GetDataSourceInclude(TSpread.TActionType.Update, "COSABUN", "COGUBN", "COPYGUBN", "COSDATE", "COEDATE", "COFLAG", "COAMT", "COINSABUN", "COINCDBK", "COINNOAC"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                // 동일코드 체크 
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_53I90693", ds.Tables[0].Rows[i]["COSABUN"].ToString(),
                                                                ds.Tables[0].Rows[i]["COGUBN"].ToString(),
                                                                ds.Tables[0].Rows[i]["COPYGUBN"].ToString(),
                                                                ds.Tables[0].Rows[i]["COSDATE"].ToString()
                                                                 );
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                    if (iCnt > 0)
                    {
                        this.ShowMessage("TY_M_GB_23S40973");
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

        #region Description : 그리드 더블 클릭 이벤트
        private void FPS91_TY_S_HR_4COHZ979_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //if ((new TYHRFR001I(this.FPS91_TY_S_HR_4COHZ979.GetValue("COYYMM").ToString(),
            //                    this.FPS91_TY_S_HR_4COHZ979.GetValue("COGUBN").ToString(),
            //                    this.FPS91_TY_S_HR_4COHZ979.GetValue("COJIDATE").ToString(),
            //                    this.FPS91_TY_S_HR_4COHZ979.GetValue("COSABUN").ToString(),
            //                    this.FPS91_TY_S_HR_4COHZ979.GetValue("COINGUBN").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)

            //    this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_519HW082", dt);
            this.DbConnector.ExecuteTranQueryList();
                       
            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);
        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            Int16 iCnt = 0;
            
            DataTable dt = this.FPS91_TY_S_HR_4COHZ979.GetDataSourceInclude(TSpread.TActionType.Remove, "COSABUN", "COGUBN", "COPYGUBN", "COSDATE");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (dt.Rows.Count > 0)
            {
                //이체내역이 있는지 체크
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_53I9P694", dt.Rows[i]["COSABUN"].ToString(),
                                                                dt.Rows[i]["COGUBN"].ToString(),
                                                                dt.Rows[i]["COPYGUBN"].ToString(),
                                                                dt.Rows[i]["COSDATE"].ToString()
                                                                 );
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                    if (iCnt > 0)
                    {
                        this.ShowMessage("TY_M_HR_4COHY978");
                        e.Successed = false;
                        return;
                    }
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

        #region Description : 이체계좌관리 팝업
        private void BTN61_SAV_BATCH_Click(object sender, EventArgs e)
        {
            if ((new TYHRFR001I()).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : FPS91_TY_S_HR_4COHZ979_KeyDown 이벤트
        private void FPS91_TY_S_HR_4COHZ979_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {     
          //if (e.KeyCode == System.Windows.Forms.Keys.F1 && FPS91_TY_S_HR_4COHZ979.ActiveSheet.ActiveColumnIndex == 2)
          //{
          //      TYHRFR01C1 popup = new TYHRFR01C1();
          //      if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
          //      {
          //          FPS91_TY_S_HR_4COHZ979.SetValue(FPS91_TY_S_HR_4COHZ979.ActiveSheet.ActiveRowIndex, "COGUBN", popup.fsCTGUBN);
          //          FPS91_TY_S_HR_4COHZ979.SetValue(FPS91_TY_S_HR_4COHZ979.ActiveSheet.ActiveRowIndex, "COGUBNNM", popup.fsCTGUBNNM);
          //          FPS91_TY_S_HR_4COHZ979.SetValue(FPS91_TY_S_HR_4COHZ979.ActiveSheet.ActiveRowIndex, "CTSABUN", popup.fsCTSABUN);
          //          FPS91_TY_S_HR_4COHZ979.SetValue(FPS91_TY_S_HR_4COHZ979.ActiveSheet.ActiveRowIndex, "CTSABUNNM", popup.fsCTSABUNNM);
          //          FPS91_TY_S_HR_4COHZ979.SetValue(FPS91_TY_S_HR_4COHZ979.ActiveSheet.ActiveRowIndex, "CTBKCODE", popup.fsCTBKCODE);
          //          FPS91_TY_S_HR_4COHZ979.SetValue(FPS91_TY_S_HR_4COHZ979.ActiveSheet.ActiveRowIndex, "CTBKCODENM", popup.fsCTBKCODENM);
          //          FPS91_TY_S_HR_4COHZ979.SetValue(FPS91_TY_S_HR_4COHZ979.ActiveSheet.ActiveRowIndex, "CTBKACCOUNT", popup.fsCTBKACCOUNT);
          //      }

          //  }     

        }
        #endregion

        #region Description : FPS91_TY_S_HR_4COHZ979_ButtonClicked 이벤트
        private void FPS91_TY_S_HR_4COHZ979_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "15")
            {
                TYHRFR01C1 popup = new TYHRFR01C1(FPS91_TY_S_HR_4COHZ979.GetValue(e.Row, "COSABUN").ToString(), FPS91_TY_S_HR_4COHZ979.GetValue(e.Row, "COGUBN").ToString());
                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }
            }
        }
        #endregion


    }
}