using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// (구) 계정과목 등록 팝업 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2012.05.15 13:20
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_25F71466 : (구) 계정과목 확인
    ///  TY_P_AC_25F7J471 : (구) 계정과목 저장
    ///  TY_P_AC_25F7O472 : (구) 계정과목 수정
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
    ///  AO1ACHL1 : 상위계정코드1
    ///  AO1ACHL2 : 상위계정코드2
    ///  AO1ACHL3 : 상위계정코드3
    ///  AO1ACHL4 : 상위계정코드4
    ///  AO1ACHL5 : 상위계정코드5
    ///  AO1CDMI1 : 관리항목코드1
    ///  AO1CDMI2 : 관리항목코드2
    ///  AO1CDMI3 : 관리항목코드3
    ///  AO1CDMI4 : 관리항목코드4
    ///  AO1CDMI5 : 관리항목코드5
    ///  AO1CDMI6 : 관리항목코드6
    ///  AO1CRFD : 자금항목코드-대변
    ///  AO1DRFD : 자금항목-차변
    ///  AO1NCDAC : 신계정과목
    ///  AO1IDAC : 계정구분
    ///  AO1LVAC : LEVEL
    ///  AO1OTMI1 : OPTION1
    ///  AO1OTMI2 : OPTION2
    ///  AO1OTMI3 : OPTION3
    ///  AO1OTMI4 : OPTION4
    ///  AO1OTMI5 : OPTION5
    ///  AO1OTMI6 : OPTION6
    ///  AO1TAG01 : 차/대(D/C)
    ///  AO1TAG03 : 관리대장KEY
    ///  AO1TAG04 : 기간비용정리여부
    ///  AO1TAG10 : 충당금
    ///  AO1TAG11 : 반제연결
    ///  AO1TAG02 : 전표계정
    ///  AO1TAG06 : 예산통제여부
    ///  AO1TAG07 : 반제관리
    ///  AO1TAG09 : 접대비
    ///  AO1YNBS : B/S계정
    ///  AO1YNCM : 제조원가계정여부
    ///  AO1YNIS : I/S계정여부
    ///  AO1YNTBD : 일계표계정여부
    ///  AO1YNTB_ : T/B계정여부
    ///  AO1ABAC : 계정과목약명
    ///  AO1CDAC : 계정코드
    ///  AO1NMAC : 계정과목명
    ///  AO1NMENAC : 계정과목영문명
    ///  AO1NMHNAC : 계정과목한문명
    ///  AO1TAG05 : 자금관리DB
    /// </summary>
    public partial class TYACAB003I : TYBase
    {
        private string _AO1CDAC;
        private TYData DAT01_AO1HISAB;

        public TYACAB003I(string AO1CDAC)
        {
            InitializeComponent();
            this.SetPopupStyle();
            this._AO1CDAC = AO1CDAC;
            //this.DAT01_AO1HISAB = new TYData("DAT01_AO1HISAB", Employer.UserID);
            this.DAT01_AO1HISAB = new TYData("DAT01_AO1HISAB", (object)"0310-M");
        }

        #region Description : Page_Load
        private void TYACAB003I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.ControlFactory.Add(this.DAT01_AO1HISAB);

            if (string.IsNullOrEmpty(_AO1CDAC))
            {
                this.TXT01_AO1CDAC.SetReadOnly(false);
                this.SetStartingFocus(TXT01_AO1CDAC);
            }
            else
            {
                this.TXT01_AO1CDAC.SetReadOnly(true);
                this.SetStartingFocus(TXT01_AO1NMAC);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_25F71466", _AO1CDAC);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.CurrentDataTableRowMapping(dt, "01");
                }
            }
        }
        #endregion

        #region Description : 저장
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            if (string.IsNullOrEmpty(this._AO1CDAC))
            {
                this.DbConnector.Attach("TY_P_AC_25F7J471", this.ControlFactory, "01"); // 저장
            }
            else
            {
                this.DbConnector.Attach("TY_P_AC_25F7O472", this.ControlFactory, "01"); // 수정
            }

            this.DbConnector.ExecuteNonQuery();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ShowMessage("TY_M_GB_23NAD873");
            this.Close();
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
    }
}
