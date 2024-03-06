using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 은행코드관리 등록 팝업 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.04.02 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2433E348 : 은행코드 SEQ
    ///  TY_P_AC_243BN333 : 은행코드관리 등록
    ///  TY_P_AC_243BO334 : 은행코드관리 삭제
    ///  TY_P_AC_243BP335 : 은행코드관리 수정
    ///  TY_P_AC_243BQ336 : 은행코드관리 확인
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23S40973 : 동일한 코드가 존재합니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  CDCODE1 : 코드1
    ///  CDBIGO : 비고
    ///  CDCODE : CODE
    ///  CDCODE2 : 코드2
    ///  CDDESC1 : 내용1
    ///  CDDESC2 : 내용2
    /// </summary>
    public partial class TYACAB007I : TYBase
    {
        private string _CDCODE;
        private TYData DAT01_CDHISAB;

        public string fsCODE;

        #region Description : 페이지 로드
        public TYACAB007I(string sCDCODE)
        {
            InitializeComponent();

            // 팝업 스타일 맞추는것임
            this.SetPopupStyle();

            // 파라미터값 가져오기
            this._CDCODE = sCDCODE;

            // 로그인 사번 가져오기
            this.DAT01_CDHISAB = new TYData("DAT01_CDHISAB", TYUserInfo.EmpNo);
        }

        private void TYACAB007I_Load(object sender, System.EventArgs e)
        {
            //this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.ControlFactory.Add(this.DAT01_CDHISAB);

            if (string.IsNullOrEmpty(this._CDCODE))
            {
                this.CBH01_CDCODE1.SetReadOnly(false);

                SetStartingFocus(this.CBH01_CDCODE1.CodeText);
            }
            else
            {
                this.CBH01_CDCODE1.SetReadOnly(true);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_243BQ336", this._CDCODE);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                    this.CurrentDataTableRowMapping(dt, "01");
            }
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            if (string.IsNullOrEmpty(this._CDCODE))
            {
                // PadLeft(3, '0') <--- 왼쪽에서 3자리까지 빈 공백을 0으로 채움.

                // INDEX 순번 가져오는 SP
                this.DbConnector.Attach("TY_P_AC_2433E348", CBH01_CDCODE1.GetValue(), "");
                // SP의 OUTPUT 값 가져오는 부분
                this.TXT01_CDCODE2.SetValue(Convert.ToString(this.DbConnector.ExecuteScalar()).PadLeft(3, '0'));

                // 등록
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_243BN333", this.ControlFactory, "01");

            }
            else
                // 수정
                this.DbConnector.Attach("TY_P_AC_243BP335", this.ControlFactory, "01");

            this.DbConnector.ExecuteNonQuery();

            this.ShowMessage("TY_M_GB_23NAD873");

            fsCODE = this.CBH01_CDCODE1.GetValue().ToString() + this.TXT01_CDCODE2.GetValue().ToString();

            //this.DialogResult = System.Windows.Forms.DialogResult.OK;            
            //this.Close();
        }
        #endregion

        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            fsCODE = this.CBH01_CDCODE1.GetValue().ToString() + this.TXT01_CDCODE2.GetValue().ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
