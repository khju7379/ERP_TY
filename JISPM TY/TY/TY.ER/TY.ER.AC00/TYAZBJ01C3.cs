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
    /// 지급어음 등록 팝업(미승인 등록) 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2012.11.06 09:03
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2B63U143 : 지급어음 확인 (미승인 코드헬프 단일)
    ///  TY_P_AC_2B63V144 : 지급어음 확인(미승인 코드헬프)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2B64S145 : 지급어음 등록 조회(미승인등록)
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  F3NONY : 어음번호
    /// </summary>
    public partial class TYAZBJ01C3 : TYBase, IPopupHelper
    {
        private string fsSessionId = string.Empty;

        private TCodeBox _TComboHelper;
        private DataRow _SelectedDataRow;

        private string fsNONR = string.Empty;

        public TYAZBJ01C3()
        {
            this.SetPopupStyle();
            InitializeComponent();
        }

        #region Descriontion : Page_Load
        private void TYAZBJ01C3_Load(object sender, System.EventArgs e)
        {
            if (this.DesignMode)
                return;

            if (this._TComboHelper != null && this._TComboHelper.DummyValue != null)
            {
                string[] value = this._TComboHelper.DummyValue as string[];
                if (value != null && value.Length > 2)
                {
                    this.fsSessionId = value[0];  //ssid
                    this.fsNONR = value[1]; // 어음번호
                }
            }

            this.TXT01_F3NONY.SetValue(fsNONR);

            this.BTN61_INQ_Click(null, null);
        } 
        #endregion

        #region Descriontion : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();

        } 
        #endregion

        #region Descriontion : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2B63V144", this.TXT01_F3NONY.GetValue().ToString());
            this.FPS91_TY_S_AC_2B64S145.SetValue(this.DbConnector.ExecuteDataTable());
        } 
        #endregion

        ////필수..시작
        #region Description : 필수 선택 조회
        public void ConfirmEventInterface()
        {
            int row = 0;
            string sNum = "";
            string code = "";
            string name = "";

            row = FPS91_TY_S_AC_2B64S145.ActiveSheet.ActiveRowIndex;
            //어음번호
            sNum = this.FPS91_TY_S_AC_2B64S145.GetValue(row, "F5NONC").ToString();

            code = sNum;
            name = sNum;

            this._SelectedDataRow = this.FPS91_TY_S_AC_2B64S145.GetDataRow(row);


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
                if (value != null && value.Length > 2)
                {
                    this.fsSessionId = value[0];  //ssid
                }
            }

            this.fsNONR = parameters[1]; // 어음번호

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2B63U143", this.fsNONR);
            this.FPS91_TY_S_AC_2B64S145.SetValue(this.DbConnector.ExecuteDataTable());

            return this.DbConnector.ExecuteDataTable();
        }

        public void Initialize_Helper(TCodeBox sender)
        {
            this._TComboHelper = sender;
            this._SelectedDataRow = null;

            this.Initialize_DbConnector();

            this.TXT01_F3NONY.Initialize();

        }

        public DataRow SelectedRow
        {
            get { return this._SelectedDataRow; }
        }

        public void ShowPopupHelper()
        {
            if (this.IsHandleCreated)
            {
                this.TYAZBJ01C3_Load(null, null);
                this._TComboHelper.CodeBoxPopupHelper_Init();
            }

            base.Show();
        }

        private void FPS91_TY_S_AC_2B64S145_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader)
                return;
            this.ConfirmEventInterface();
        }
        #endregion
        //필수...끝
    }
}
