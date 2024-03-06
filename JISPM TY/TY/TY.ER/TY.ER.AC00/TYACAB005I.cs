using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 자금항목 등록 팝업 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.03.30 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_23U3Z210 : 자금항목코드 등록
    ///  TY_P_AC_23U40211 : 자금항목코드 수정
    ///  TY_P_AC_23U43214 : 자금항목코드 확인
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  A3FDHL1 : 상위항목코드１
    ///  A3FDHL2 : 상위항목코드２
    ///  A3FDHL3 : 상위항목코드３
    ///  A3IDPL  : LEVEL
    ///  A3YNSL  : 전표발생단위Y/N
    ///  A3ABFD  : 자금항목약명
    ///  A3CDFD  : 자금항목코드
    ///  A3HISAB : 작성사번
    ///  A3NMFD  : 자금항목명
    /// </summary>
    public partial class TYACAB005I : TYBase
    {
        private string _A3CDFD;
        private TYData DAT01_A3HISAB;

        #region Description : 페이지 로드
        public TYACAB005I(string A3CDFD)
        {
            InitializeComponent();

            this.SetPopupStyle();

            // 파라미터값 가져오기
            this._A3CDFD = A3CDFD;

            // 로그인 사번 가져오기
            this.DAT01_A3HISAB = new TYData("DAT01_A3HISAB", TYUserInfo.EmpNo);
        }

        private void TYACAB005I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.ControlFactory.Add(this.DAT01_A3HISAB);

            if (string.IsNullOrEmpty(this._A3CDFD))
            {
                this.TXT01_A3CDFD.SetReadOnly(false);

                SetStartingFocus(this.TXT01_A3CDFD);
            }
            else
            {
                this.TXT01_A3CDFD.SetReadOnly(true);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_23U43214", this._A3CDFD);
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

            if (string.IsNullOrEmpty(this._A3CDFD))
                // 등록
                this.DbConnector.Attach("TY_P_AC_23U3Z210", this.ControlFactory, "01");
            else
                // 수정
                this.DbConnector.Attach("TY_P_AC_23U40211", this.ControlFactory, "01");

            this.DbConnector.ExecuteNonQuery();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ShowMessage("TY_M_GB_23NAD873");
            this.Close();
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (string.IsNullOrEmpty(this._A3CDFD))
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_2CE3J195",
                    this.TXT01_A3CDFD.GetValue().ToString()
                    );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_GB_23S40973");
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}