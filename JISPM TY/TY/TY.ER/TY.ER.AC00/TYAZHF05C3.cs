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
    /// 중분류 조회[팝업] 프로그램입니다.
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
    ///  TY_P_AC_37G5S122 : 중분류 자산분류 조회 [팝업]
    ///  TY_P_AC_37G5T124 : 중분류 자산분류 확인 [단일]
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_37G5S123 : 중분류 자산분류 조회 [팝업]
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  MMCODE : 중분류 코드
    ///  MMDESC : 중분류명
    /// </summary>
    public partial class TYAZHF05C3 : TYBase, IPopupHelper
    {
        bool _Isloaded = false;
        private TCodeBox _TComboHelper;
        private DataRow _SelectedDataRow;

        private string fsSessionId;  // ssid
        private string fsSabun;      // 사번
        private string fsNumber;     // 코드
        private string fsQru;        // 조회 구분
        private string fsVALUE;      // 값

        public TYAZHF05C3()
        {
            InitializeComponent();
            this.SetPopupStyle();
        }

        #region Description : Page_Load
        private void TYAZHF05C3_Load(object sender, System.EventArgs e)
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

            //this.TXT01_MMCODE.SetValue("");
            this.TXT01_MMDESC.SetValue("");

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
            this.DbConnector.Attach("TY_P_AC_2C44G874", CBH01_FXC2SAUP.GetValue().ToString(), CBH01_FXC2BCODE.GetValue().ToString(), this.TXT01_MMDESC.GetValue().ToString());
            this.FPS91_TY_S_AC_37G5S123.SetValue(this.DbConnector.ExecuteDataTable());
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
            string code2 = "";

            row = FPS91_TY_S_AC_37G5S123.ActiveSheet.ActiveRowIndex;
            //중분류 코드,코드명

            code1 = this.FPS91_TY_S_AC_37G5S123.GetValue(row, "FXMLASCODE").ToString().Trim(); // 자산
            code2 = this.FPS91_TY_S_AC_37G5S123.GetValue(row, "FXMLMCODE").ToString().Trim(); // 대

            sNum = this.FPS91_TY_S_AC_37G5S123.GetValue(row, "FXMMCODE").ToString().Trim();//중
            sName = this.FPS91_TY_S_AC_37G5S123.GetValue(row, "FXMMDESC").ToString().Trim();

            code = code1 + "-" + code2 + "-" + sNum; // 자산+대+중
            name = sName;

            this._SelectedDataRow = this.FPS91_TY_S_AC_37G5S123.GetDataRow(row);

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

            if (this.fsQru == "1")
            {
                //this.DbConnector.CommandClear(); // ACFIXUSDMF (단일)
                //this.DbConnector.Attach("TY_P_AC_37G5T124", this.fsNumber);
                //this.FPS91_TY_S_AC_37G5S123.SetValue(this.DbConnector.ExecuteDataTable());

                //this.TXT01_MMCODE.SetValue(this.fsNumber);

                this.DbConnector.CommandClear();  // ACFIXUMDMF (like)
                this.DbConnector.Attach("TY_P_AC_37G5S122", this.fsNumber , "");
                this.FPS91_TY_S_AC_37G5S123.SetValue(this.DbConnector.ExecuteDataTable());

            }
            else if (this.fsQru == "3")
            {
                this.fsNumber = fsVALUE; // 분류코드
                this.DbConnector.CommandClear();  // ACFIXUSDMF (단일)
                this.DbConnector.Attach("TY_P_AC_37G5T124", this.fsNumber);
                this.FPS91_TY_S_AC_37G5S123.SetValue(this.DbConnector.ExecuteDataTable());

            }

            return this.DbConnector.ExecuteDataTable();
        }

        public void Initialize_Helper(TCodeBox sender)
        {
            this._TComboHelper = sender;
            this._SelectedDataRow = null;

            this.Initialize_DbConnector();

            //this.TXT01_MMCODE.Initialize();
            this.TXT01_MMDESC.Initialize();
        }

        public DataRow SelectedRow
        {
            get { return this._SelectedDataRow; }
        }

        public void ShowPopupHelper()
        {
            if (this.IsHandleCreated)
            {
                this.TYAZHF05C3_Load(null, null);
                this._TComboHelper.CodeBoxPopupHelper_Init();
            }

            base.Show();
        }

        private void FPS91_TY_S_AC_37G5S123_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader)
                return;
            this.ConfirmEventInterface();
        }

        #endregion

        private void CBH01_FXC2SAUP_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH01_FXC2SAUP.GetValue().ToString();
            this.CBH01_FXC2BCODE.DummyValue = groupCode;
            this.CBH01_FXC2BCODE.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) this.CBH01_FXC2BCODE.Initialize();
        }

        //필수...끝
    }
}
