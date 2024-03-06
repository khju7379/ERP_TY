using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 대분류 조회[팝업] 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2012.12.12 11:23
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_37G6B125 : 대분류 자산분류 조회 [팝업]
    ///  TY_P_AC_37G6B126 : 대분류 자산분류 확인 [단일]
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_37G6C127 : 대분류 자산분류 조회 [팝업]
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  LMCODE : 대분류 코드
    ///  LMDESC : 대분류명
    /// </summary>
    public partial class TYAZHF05C4 : TYBase, IPopupHelper
    {
        bool _Isloaded = false;
        private TCodeBox _TComboHelper;
        private DataRow _SelectedDataRow;

        private string fsSessionId;  // ssid
        private string fsSabun;      // 사번
        private string fsNumber;     // 코드
        private string fsQru;        // 조회 구분
        private string fsVALUE;      // 값

        public TYAZHF05C4()
        {
            InitializeComponent();
            this.SetPopupStyle();
        }

        #region Description : Page_Load
        private void TYAZHF05C4_Load(object sender, System.EventArgs e)
        {
            

            if (this.DesignMode)
                return;

            if (this._TComboHelper != null && this._TComboHelper.DummyValue != null)
            {
                string[] value = this._TComboHelper.DummyValue as string[];
                if (value != null && value.Length > 1)
                {
                    this.fsSessionId = value[0];  //ssid
                    this.fsSabun = value[1];  //사번
                }
            }

            this.CBH01_FXC1SAUP.SetValue("");
            this.TXT01_LMDESC.SetValue("");

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 닫기
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 조회
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();  // ACFIXUMDMF (like)
            //this.DbConnector.Attach("TY_P_AC_37G6B125", this.TXT01_LMCODE.GetValue().ToString(), this.TXT01_LMDESC.GetValue().ToString());            
            this.DbConnector.Attach("TY_P_AC_2C42V851", this.CBH01_FXC1SAUP.GetValue().ToString(), this.TXT01_LMDESC.GetValue().ToString());
            this.FPS91_TY_S_AC_37G6C127.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        ////필수..시작
        #region Description : 필수 선택 조회
        public void ConfirmEventInterface()
        {
            int row = 0;
            string sNum = "";
            string sName = "";
            string code = "";
            string name = "";

            string code1 = "";

            row = FPS91_TY_S_AC_37G6C127.ActiveSheet.ActiveRowIndex;
            //자산 코드,코드명

            code1 = this.FPS91_TY_S_AC_37G6C127.GetValue(row, "FXC1SAUP").ToString().Trim(); // 자산

            sNum = this.FPS91_TY_S_AC_37G6C127.GetValue(row, "FXC1CODE").ToString().Trim(); // 대
            sName = this.FPS91_TY_S_AC_37G6C127.GetValue(row, "FXC1DESC").ToString().Trim();

            code = code1+sNum;
            name = sName;

            this._SelectedDataRow = this.FPS91_TY_S_AC_37G6C127.GetDataRow(row);

            if (this._TComboHelper != null)
            {
                this._TComboHelper.SetValue(code, name);
                this._TComboHelper.BindedDataRow = _SelectedDataRow;
            }

            this.Close();
        }

        public DataTable GetDataSource(params string[] parameters)
        {
            if (this._TComboHelper != null && this._TComboHelper.DummyValue != null)
            {
                string[] value = this._TComboHelper.DummyValue as string[];
                if (value != null && value.Length > 1)
                {
                    this.fsSessionId = value[0];  // ssid
                    this.fsSabun = value[1];      // 사번
                    this.fsQru = value[2];        // 조회 구분
                    this.fsVALUE = value[3];      // 분류코드
                }
            }

            this.fsNumber = parameters[1]; // 분류코드

            if (this._TComboHelper.DummyValue == null)
            {
                this.fsQru = "1";
            }

            //if (this.fsQru == "1")
            //{
            //    this.DbConnector.CommandClear(); // ACFIXUSDMF (like)
            //    //this.DbConnector.Attach("TY_P_AC_37G6B126", this.fsNumber);
            //    this.DbConnector.Attach("TY_P_AC_2C42V851", this.fsNumber, "");
            //    this.FPS91_TY_S_AC_37G6C127.SetValue(this.DbConnector.ExecuteDataTable());

            //}
            //else if (this.fsQru == "3")
            //{
            //    this.fsNumber = fsVALUE; // 분류코드
            //    this.DbConnector.CommandClear();  // ACFIXUSDMF (단일)
            //    this.DbConnector.Attach("TY_P_AC_65UAJ993", this.fsNumber.Substring(0, 1), this.fsNumber.Substring(1, 2));
            //    this.FPS91_TY_S_AC_37G6C127.SetValue(this.DbConnector.ExecuteDataTable());
            //}

            
                this.DbConnector.CommandClear();  // ACFIXUSDMF (단일)
                this.DbConnector.Attach("TY_P_AC_65UAJ993", this.fsNumber.Substring(0, 1), this.fsNumber.Substring(1, 2));
                this.FPS91_TY_S_AC_37G6C127.SetValue(this.DbConnector.ExecuteDataTable());


            return this.DbConnector.ExecuteDataTable();

        }

        public void Initialize_Helper(TCodeBox sender)
        {
            this._TComboHelper = sender;
            this._SelectedDataRow = null;

            this.Initialize_DbConnector();

            this.CBH01_FXC1SAUP.Initialize();
            this.TXT01_LMDESC.Initialize();
        }

        public DataRow SelectedRow
        {
            get { return this._SelectedDataRow; }
        }

        public void ShowPopupHelper()
        {
            if (this.IsHandleCreated)
            {
                this.TYAZHF05C4_Load(null, null);
                this._TComboHelper.CodeBoxPopupHelper_Init();
            }

            base.Show();
        }

        private void FPS91_TY_S_AC_37G6C127_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader)
                return;
            this.ConfirmEventInterface();
        }
        #endregion

        //필수...끝
    }
}
