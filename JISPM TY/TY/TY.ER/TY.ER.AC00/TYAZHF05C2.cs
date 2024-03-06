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
    /// 소분류 조회[팝업] 프로그램입니다.
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
    ///  TY_P_AC_37G4M116 : 소분류 자산분류 조회 [팝업]
    ///  TY_P_AC_37G51119 : 소분류 자산분류 확인 [단일]
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_37G4N118 : 소분류 자산분류 조회 [팝업]
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  SMCODE : 소분류 코드
    ///  SMDESC : 소분류명
    /// </summary>
    public partial class TYAZHF05C2 : TYBase, IPopupHelper
    {
        bool _Isloaded = false;
        private TCodeBox _TComboHelper;
        private DataRow _SelectedDataRow;

        private string fsSessionId;  // ssid
        private string fsSabun;      // 사번
        private string fsNumber;     // 코드
        private string fsQru;        // 조회 구분
        private string fsVALUE;      // 값

        public TYAZHF05C2()
        {
            InitializeComponent();
            this.SetPopupStyle();
        }

        #region Description : Page_Load
        private void TYAZHF05C2_Load(object sender, System.EventArgs e)
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

            this.TXT01_SMCODE.SetValue("");
            this.TXT01_SMDESC.SetValue("");

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
            this.DbConnector.CommandClear();  // ACFIXUSDMF (like)
            this.DbConnector.Attach("TY_P_AC_37G4M116", this.TXT01_SMCODE.GetValue().ToString(), this.TXT01_SMDESC.GetValue().ToString());
            this.FPS91_TY_S_AC_37G4N118.SetValue(this.DbConnector.ExecuteDataTable());
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
            string code3 = "";

            row = FPS91_TY_S_AC_37G4N118.ActiveSheet.ActiveRowIndex;
            //소분류 코드,코드명

            code1 = this.FPS91_TY_S_AC_37G4N118.GetValue(row, "FXSLASCODE").ToString().Trim(); // 자산
            code2 = this.FPS91_TY_S_AC_37G4N118.GetValue(row, "FXSLMCODE").ToString().Trim(); // 대
            code3 = this.FPS91_TY_S_AC_37G4N118.GetValue(row, "FXMMCODE").ToString().Trim(); //중

            sNum = this.FPS91_TY_S_AC_37G4N118.GetValue(row, "FXSMCODE").ToString().Trim(); // 소
            sName = this.FPS91_TY_S_AC_37G4N118.GetValue(row, "FXSMDESC").ToString().Trim();

            code = code1 + "-" + code2 + "-" + code3 + "-" + sNum;
            name = sName;

            this._SelectedDataRow = this.FPS91_TY_S_AC_37G4N118.GetDataRow(row);

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
                //this.DbConnector.Attach("TY_P_AC_37G51119", this.fsNumber);
                //this.FPS91_TY_S_AC_37G4N118.SetValue(this.DbConnector.ExecuteDataTable());

                //this.TXT01_SMCODE.SetValue(this.fsNumber);

                this.DbConnector.CommandClear();  // ACFIXUSDMF (like)
                this.DbConnector.Attach("TY_P_AC_37G4M116", this.fsNumber , "");
                this.FPS91_TY_S_AC_37G4N118.SetValue(this.DbConnector.ExecuteDataTable());

            }
            else if (this.fsQru == "3")
            {
                this.fsNumber = fsVALUE; // 분류코드
                this.DbConnector.CommandClear();  // ACFIXUSDMF (단일)
                this.DbConnector.Attach("TY_P_AC_37G51119", this.fsNumber);
                this.FPS91_TY_S_AC_37G4N118.SetValue(this.DbConnector.ExecuteDataTable());
            }

            return this.DbConnector.ExecuteDataTable();
        }

        public void Initialize_Helper(TCodeBox sender)
        {
            this._TComboHelper = sender;
            this._SelectedDataRow = null;

            this.Initialize_DbConnector();

            this.TXT01_SMCODE.Initialize();
            this.TXT01_SMDESC.Initialize();
        }

        public DataRow SelectedRow
        {
            get { return this._SelectedDataRow; }
        }

        public void ShowPopupHelper()
        {
            if (this.IsHandleCreated)
            {
                this.TYAZHF05C2_Load(null, null);
                this._TComboHelper.CodeBoxPopupHelper_Init();
            }

            base.Show();
        }

        private void FPS91_TY_S_AC_37G4N118_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader)
                return;
            this.ConfirmEventInterface();
        }

        #endregion

        //필수...끝
    }
}
