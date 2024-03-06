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
    /// 출장문서번호 조회(미승인 등록) 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2013.05.13 18:14
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_35D61658 : 출장문서번호 확인 (미승인 코드헬프 단일)
    ///  TY_P_AC_35D62659 : 출장문서번호 확인(미승인 코드헬프)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_35D6B660 : 출장문서번호음 등록 조회(미승인등록)
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  GXGWDOCID : 출장문서 번호
    /// </summary>
    public partial class TYAZBJ01C7 : TYBase, IPopupHelper
    {
        bool _Isloaded = false;
        private TCodeBox _TComboHelper;
        private DataRow _SelectedDataRow;

        private string fsSessionId;  //ssid
        private string fsSabun;  //사번
        private string fsNumber;  //출장번호
        private string fsQru;  //조회 구분

        public TYAZBJ01C7()
        {
            InitializeComponent();
            this.SetPopupStyle();
        }

        #region Description : Page_Load
        private void TYAZBJ01C7_Load(object sender, System.EventArgs e)
        {
            //this.CBH01_TWBANK.CodeBoxDataBinded += new TCodeBox.TCodeBoxEventHandler(CBH01_TWBANK_CodeBoxDataBinded); // 외화관리 

            if (this.DesignMode)
                return;

            if (this._TComboHelper != null && this._TComboHelper.DummyValue != null)
            {
                string[] value = this._TComboHelper.DummyValue as string[];
                if (value != null && value.Length > 1)
                {
                    this.fsSessionId = value[0];  //ssid
                    this.fsSabun = value[1];  //사번

                    this.CBH01_KBSABUN.SetValue(fsSabun);
                }
            }
            this.TXT01_GXGWDOCID.SetValue("");
            //this.CBH01_TWGUJA.SetValue(fsGuja);

            this.TXT01_SDATE.SetValue(DateTime.Now.ToString("yyyy"));
            
            this.BTN61_INQ_Click(null, null);
        } 
        #endregion

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();  // GTHUEXPENSEF
            //ORA
            //this.DbConnector.Attach("TY_P_AC_35D62659",this.fsSabun,  this.TXT01_GXGWDOCID.GetValue());
            //DB2 신인사용 (2016년 1월 사용 )
            this.DbConnector.Attach("TY_P_AC_5BCEW154", this.TXT01_SDATE.GetValue().ToString(),  this.CBH01_KBSABUN.GetValue().ToString(),   this.TXT01_GXGWDOCID.GetValue());
            this.FPS91_TY_S_AC_35D6B660.SetValue(this.DbConnector.ExecuteDataTable());
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

            row = FPS91_TY_S_AC_35D6B660.ActiveSheet.ActiveRowIndex;
            //출장문서 번호
            sNum = this.FPS91_TY_S_AC_35D6B660.GetValue(row, "GXGWDOCID").ToString().Trim();//.Replace("-", "");

            code = sNum;
            name = sNum;

            this._SelectedDataRow = this.FPS91_TY_S_AC_35D6B660.GetDataRow(row);


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
                    this.fsSabun = value[1];  //사번
                    this.fsQru = value[2];  //조회 구분
                }
            }

            this.fsNumber = parameters[1]; // 출장번호

            if (this.fsQru == "1")
            {
                this.DbConnector.CommandClear(); // GTHUEXPENSEF
                //ORA
                //this.DbConnector.Attach("TY_P_AC_35D61658", this.fsSabun , this.fsNumber);
                //DB2 신인사용(2015년 1월 사용)
                this.DbConnector.Attach("TY_P_AC_5BCEU153", this.fsSabun, this.fsNumber);
                this.FPS91_TY_S_AC_35D6B660.SetValue(this.DbConnector.ExecuteDataTable());
            }
            //else if (this.fsQru == "2")
            //    {
            //        this.DbConnector.CommandClear();  // GTHUEXPENSEF
            //         this.DbConnector.Attach("TY_P_AC_35D62659",this.fsSabun,  this.TXT01_GXGWDOCID.GetValue());
            //        this.FPS91_TY_S_AC_35D6B660.SetValue(this.DbConnector.ExecuteDataTable());
            //    }

            return this.DbConnector.ExecuteDataTable();
        }

        public void Initialize_Helper(TCodeBox sender)
        {
            this._TComboHelper = sender;
            this._SelectedDataRow = null;

            this.Initialize_DbConnector();

            this.TXT01_GXGWDOCID.Initialize();
            //this.CBH01_TWGUJA.Initialize();
        }

        public DataRow SelectedRow
        {
            get { return this._SelectedDataRow; }
        }

        public void ShowPopupHelper()
        {
            if (this.IsHandleCreated)
            {
                this.TYAZBJ01C7_Load(null, null);
                this._TComboHelper.CodeBoxPopupHelper_Init();
            }

            base.Show();
        }

        private void FPS91_TY_S_AC_35D6B660_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader)
                return;
            this.ConfirmEventInterface();

        }

        #endregion

        //필수...끝
    }
}
