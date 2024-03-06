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
    /// 외화관리 등록 팝업(미승인 등록) 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2012.11.14 09:03
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2BE5E315 : 외화관리 등록 조회(미승인등록)
    ///  TY_P_AC_2BE5F316 : 외화 조회 존재 유무 확인 외화등록시
    ///  TY_P_AC_2BE5G317 : 외화 조회 존재 유무 확인 조회시
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2BE5H319 : 외화 등록 팝업 조회 (미승인 등록)
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  TWBANK : 발생은행
    ///  TWGUJA : 발생계좌
    /// </summary>
    public partial class TYAZBJ01C5 : TYBase, IPopupHelper
    {
        bool _Isloaded = false;
        private TCodeBox _TComboHelper;
        private DataRow _SelectedDataRow;

        private string fsSessionId;  //ssid
        private string fsBank;  //은행
        private string fsGuja;  //계좌번호
        private string fsQru;  //조회 구분
        private string fsIHNO; // 외화번호

        private string fsDpmk, fsDtmk, fsNosq, fsNoln; // 전표번호


        public TYAZBJ01C5()
        {
            InitializeComponent();
            this.SetPopupStyle();
        }

        private void TYAZBJ01C5_Load(object sender, System.EventArgs e)
        {
            this.CBH01_TWBANK.CodeBoxDataBinded += new TCodeBox.TCodeBoxEventHandler(CBH01_TWBANK_CodeBoxDataBinded); // 외화관리 

            if (this.DesignMode)
                return;

            if (this._TComboHelper != null && this._TComboHelper.DummyValue != null)
            {
                string[] value = this._TComboHelper.DummyValue as string[];
                if (value != null && value.Length > 1)
                {
                    //this.fsSessionId = value[0];  //ssid
                    this.fsBank = value[0];  //은행
                    this.fsGuja = value[1];  //계좌번호
                    //this.fsDpmk = value[3]; // 부서
                    //this.fsDtmk = value[4]; // 일자
                    //this.fsNosq = value[5]; // 번호
                    //this.fsNoln = value[6]; // 순번
                    //this.fsQru = value[7];  //조회 구분
                }
            }

            this.CBH01_TWBANK.SetValue(fsBank);
            this.CBH01_TWGUJA.SetValue(fsGuja);

            this.BTN61_INQ_Click(null, null);

        }

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();  // AIHWANMF , AIHWANSF
            this.DbConnector.Attach("TY_P_AC_2BE5E315", this.CBH01_TWBANK.GetValue().ToString().Trim(), this.CBH01_TWGUJA.GetValue().ToString().Trim());
            this.FPS91_TY_S_AC_2BE5H319.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion


        #region Description : 계좌번호 코드 헬프 처리
        private void CBH01_TWBANK_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH01_TWBANK.GetValue().ToString();
            this.CBH01_TWGUJA.DummyValue = groupCode;
            this.CBH01_TWGUJA.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) this.CBH01_TWGUJA.Initialize();
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

            row = FPS91_TY_S_AC_2BE5H319.ActiveSheet.ActiveRowIndex;
            //외화번호
            sNum =  this.FPS91_TY_S_AC_2BE5H319.GetValue(row, "IHNO").ToString().Trim().Replace("-","");

            code = sNum;
            name = sNum;

            this._SelectedDataRow = this.FPS91_TY_S_AC_2BE5H319.GetDataRow(row);


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
                    this.fsBank = value[1];  //은행
                    this.fsGuja = value[2];  //계좌번호
                    this.fsDpmk = value[3]; // 부서
                    this.fsDtmk = value[4]; // 일자
                    this.fsNosq = value[5]; // 번호
                    this.fsNoln = value[6]; // 순번
                    this.fsQru = value[7];  //조회 구분
                }
            }

            this.fsIHNO = parameters[1]; // 외화번호

            if (this.fsQru == "1")
            {
                this.DbConnector.CommandClear(); // TMAC1102WF ,AIHWANMF
                this.DbConnector.Attach("TY_P_AC_2BE5F316", fsSessionId, fsDpmk, fsDtmk, fsNosq, fsNoln, fsBank, fsGuja, this.fsIHNO, fsBank, fsGuja, this.fsIHNO);
                this.FPS91_TY_S_AC_2BE5H319.SetValue(this.DbConnector.ExecuteDataTable());
            }
            //else if (this.fsQru == "2")
            //    {
            //        this.DbConnector.CommandClear();  // AIHWANMF
            //        this.DbConnector.Attach("TY_P_AC_2BE5G317",fsBank,fsGuja, this.fsIHNO);
            //        this.FPS91_TY_S_AC_2BE5H319.SetValue(this.DbConnector.ExecuteDataTable());
            //    }

            return this.DbConnector.ExecuteDataTable();
        }

        public void Initialize_Helper(TCodeBox sender)
        {
            this._TComboHelper = sender;
            this._SelectedDataRow = null;

            this.Initialize_DbConnector();

            this.CBH01_TWBANK.Initialize();
            this.CBH01_TWGUJA.Initialize();
        }

        public DataRow SelectedRow
        {
            get { return this._SelectedDataRow; }
        }

        public void ShowPopupHelper()
        {
            if (this.IsHandleCreated)
            {
                this.TYAZBJ01C5_Load(null, null);
                this._TComboHelper.CodeBoxPopupHelper_Init();
            }

            base.Show();
        }

        private void FPS91_TY_S_AC_2BE5H319_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader)
                return;
            this.ConfirmEventInterface();
        }

        #endregion

        //필수...끝
    }
}
