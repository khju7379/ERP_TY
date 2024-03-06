using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 반제설정 조회(신용카드결재관리용) 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.09.12 08:55
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_29C92938 : 신용카드결재 반제설정 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_29C96939 : 신용카드결재 반제설정 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  A6CDBK : 은행코드
    ///  A6NOAC : 계좌번호
    ///  B7CDAC : 계정코드
    /// </summary>
    public partial class TYAZCE03C1 : TYBase
    {
        private bool _Isloaded = false;

        private string fsB7CDAC;
        private string fsA6CDBK;
        private string fsA6NOAC;

        public string fsSJJPNO;  
        public string fsB7AMAT;
        public string fsB7CRDT;
        public string fsB7DTAC;
        public string fsB7VEND;

        public DataTable ftDataTable;


        #region Description : 폼 로드 이벤트
        public TYAZCE03C1(string sB7CDAC,string sA6CDBK,string sA6NOAC )
        {
            InitializeComponent();

            this.fsB7CDAC = sB7CDAC;
            this.fsA6CDBK = sA6CDBK;
            this.fsA6NOAC = sA6NOAC;
        }

        private void TYAZCE03C1_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SEL.ProcessCheck += new TButton.CheckHandler(BTN61_SEL_ProcessCheck);

            this.CBH01_A6CDBK.CodeBoxDataBinded += new TCodeBox.TCodeBoxEventHandler(CBH01_A6CDBK_CodeBoxDataBinded);

            this.CBH01_B7CDAC.SetValue(this.fsB7CDAC);
            this.CBH01_A6CDBK.SetValue(this.fsA6CDBK);
            this.CBH01_A6NOAC.SetValue(this.fsA6NOAC);

            this.CBH01_B7CDAC.SetReadOnly(true);
            this.CBH01_A6CDBK.SetReadOnly(true);
            this.CBH01_A6NOAC.SetReadOnly(true);

            this.BTN61_INQ_Click(null, null);

            this.SetStartingFocus(CBH01_B7VEND.CodeText);  
        }
        #endregion

        #region Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_29C96939.Initialize(); 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C92938", this.fsA6CDBK, this.fsA6NOAC, this.fsB7CDAC, this.CBH01_B7VEND.GetValue(), this.TXT01_B8WNJP.GetValue());
            this.FPS91_TY_S_AC_29C96939.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion      

        #region Description : 계좌번호 코드 헬프 처리
        private void CBH01_A6CDBK_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH01_A6CDBK.GetValue().ToString();
            this.CBH01_A6NOAC.DummyValue = groupCode;
            this.CBH01_A6NOAC.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) this.CBH01_A6NOAC.Initialize();
        }
        #endregion

        #region Description : FPS91_TY_S_AC_29C96939_CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_29C96939_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //int row = (e == null ? 0 : e.Row);

            //fsSJJPNO = this.FPS91_TY_S_AC_29C96939.GetValue(row, "SJJPNO").ToString();
            //fsB7AMAT = this.FPS91_TY_S_AC_29C96939.GetValue(row, "B7AMAT").ToString();
            //fsB7CRDT = this.FPS91_TY_S_AC_29C96939.GetValue(row, "B2VLMI1").ToString();
            //fsB7DTAC = this.FPS91_TY_S_AC_29C96939.GetValue(row, "B7DTAC").ToString();
            //fsB7VEND = this.FPS91_TY_S_AC_29C96939.GetValue(row, "B7VEND").ToString();

            //this.DialogResult = System.Windows.Forms.DialogResult.OK;

            //this.Close();
        }
        #endregion

        #region Description : FPS91_TY_S_AC_29C96939_KeyPress 이벤트
        private void FPS91_TY_S_AC_29C96939_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //int row = (e == null ? 0 : FPS91_TY_S_AC_29C96939.ActiveSheet.ActiveRowIndex);

            //fsSJJPNO = this.FPS91_TY_S_AC_29C96939.GetValue(row, "SJJPNO").ToString();
            //fsB7AMAT = this.FPS91_TY_S_AC_29C96939.GetValue(row, "B7AMAT").ToString();
            //fsB7CRDT = this.FPS91_TY_S_AC_29C96939.GetValue(row, "B2VLMI1").ToString();
            //fsB7DTAC = this.FPS91_TY_S_AC_29C96939.GetValue(row, "B7DTAC").ToString();
            //fsB7VEND = this.FPS91_TY_S_AC_29C96939.GetValue(row, "B7VEND").ToString();

            //this.DialogResult = System.Windows.Forms.DialogResult.OK;

            //this.Close();
        }
        #endregion

        #region Description : 선택 버튼 이벤트
        private void BTN61_SEL_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            if (dt.Rows.Count != 0)
            {
                ftDataTable = dt;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }

        }

        private void BTN61_SEL_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = FPS91_TY_S_AC_29C96939.GetDataSourceInclude(TSpread.TActionType.Select, "SJJPNO", "B7AMAT", "B2VLMI1", "B7DTAC", "B7VEND");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_25F59464");
                e.Successed = false;
                return;  
            }

            e.ArgData = dt;
        }
        #endregion
    }
}
