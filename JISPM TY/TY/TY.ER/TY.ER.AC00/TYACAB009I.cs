using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// IFRS 계정과목 등록 팝업 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2012.05.11 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_25B4P333 : IFRS 계정과목 수정
    ///  TY_P_AC_25B4P334 : IFRS 계정과목 저장
    ///  TY_P_AC_25B4R335 : IFRS 계정과목 확인
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
    ///  AI1ACHL1 : 상위계정코드1
    ///  AI1ACHL2 : 상위계정코드2
    ///  AI1ACHL3 : 상위계정코드3
    ///  AI1ACHL4 : 상위계정코드4
    ///  AI1ACHL5 : 상위계정코드5
    ///  AI1NCDAC : 신계정과목
    ///  AI1TAG01 : 차/대(D/C)
    ///  AI1IDAC : 계정구분
    ///  AI1LVAC : LEVEL
    ///  AI1TAG10 : 충당금
    ///  AI1YNBSDT : B/S계정세목
    ///  AI1YNBS_ : B/S계정
    ///  AI1YNISDT : I/S계정세목
    ///  AI1YNIS_ : I/S계정
    ///  AI1ABAC : 계정과목약명
    ///  AI1CDAC : 계정코드
    ///  AI1NMAC : 계정과목명
    ///  AI1NMENAC : 계정과목영문명
    ///  AI1NMHNAC : 계정과목한문명
    ///  AI1TAG05 : 자금관리DB
    ///  AI1YNCM : 제조원가
    /// </summary>
    public partial class TYACAB009I : TYBase
    {
        private string _AI1CDAC;
        private TYData DAT01_AI1HISAB;


        public TYACAB009I(string AI1CDAC)
        {
            InitializeComponent();

            this.SetPopupStyle();
            this._AI1CDAC = AI1CDAC;
            //this.DAT01_AI1HISAB = new TYData("DAT01_AI1HISAB", Employer.UserID);
            this.DAT01_AI1HISAB = new TYData("DAT01_AI1HISAB", (object)"0310-M");
        }

        #region Description : Page_Load
        private void TYACAB009I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.ControlFactory.Add(this.DAT01_AI1HISAB);

            if (string.IsNullOrEmpty(_AI1CDAC))
            {
                this.TXT01_AI1CDAC.SetReadOnly(false);
                this.SetStartingFocus(TXT01_AI1CDAC);
            }
            else
            {
                this.TXT01_AI1CDAC.SetReadOnly(true);
                this.SetStartingFocus(TXT01_AI1NMAC);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_25B4R335", _AI1CDAC);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.CurrentDataTableRowMapping(dt, "01");
                }
            }
        } 
        #endregion

        #region Description : 저장 처리시 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            //if (!this.ShowMessage("TY_M_GB_23NAD871"))
            //{
            //    e.Successed = false;
            //    return;
            //}
        }
        #endregion

        #region Description : 닫기
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
        #endregion

        #region Description : 저장
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            if (string.IsNullOrEmpty(this._AI1CDAC))
            {
                this.DbConnector.Attach("TY_P_AC_25B4P334", this.ControlFactory, "01"); // 저장
            }
            else
            {
                this.DbConnector.Attach("TY_P_AC_25B4P333", this.ControlFactory, "01"); // 수정
            }

            this.DbConnector.ExecuteNonQuery();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ShowMessage("TY_M_GB_23NAD873");
            this.Close();
        } 
        #endregion


    }
}
