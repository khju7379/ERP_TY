using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.BS00
{
    /// <summary>
    /// 구분손익 계정과목 등록 팝업 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2012.05.14 13:20
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_25EBF378 : 구분손익 계정과목 코드 확인
    ///  TY_P_AC_25EBS389 : 구분손익 계정과목 코드 저장
    ///  TY_P_AC_25EBT392 : 구분손익 계정과목 코드 수정
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
    ///  APCACHL1 : 상위계정코드1
    ///  APCACHL2 : 상위계정코드2
    ///  APCACHL3 : 상위계정코드3
    ///  APCACHL4 : 상위계정코드4
    ///  APCACHL5 : 상위계정코드5
    ///  APCIDAC : 계정구분
    ///  APCTAG01 : 차/대(D/C)
    ///  APCTAG03 : 관리대장KEY
    ///  APCTAG04 : 기간비용정리
    ///  APCTAG10 : 충당금
    ///  APCTAG11 : 반제연결
    ///  APCTAG02 : 전표계정
    ///  APCTAG06 : 예산통제여부
    ///  APCTAG07 : 반제관리
    ///  APCTAG09 : 접대비
    ///  APCYNBS : B/S계정
    ///  APCYNCM : 제조원가여부
    ///  APCYNIS : I/S계정여부
    ///  APCYNTBD : 일계표계정여부
    ///  APCABAC : 계정과목약명
    ///  APCCDAC : 계정코드
    ///  APCNMAC : 계정과목명
    ///  APCNMENAC : 계정과목영문명
    ///  APCNMHNAC : 계정과목한문명
    ///  APCTAG05 : 자금관리DB
    ///  APCYNTB : T/B계정여부
    /// </summary>
    public partial class TYBSKB004I : TYBase
    {

        private string _APCCDAC;
        private TYData DAT01_APCHISAB;


        public TYBSKB004I(string APCCDAC)
        {
            InitializeComponent();

            this.SetPopupStyle();
            this._APCCDAC = APCCDAC;
            //this.DAT01_APCHISAB = new TYData("DAT01_APCHISAB", Employer.UserID);
            this.DAT01_APCHISAB = new TYData("DAT01_APCHISAB", (object)"0310-M");
        }

        #region Description : Page_Load
        private void TYBSKB004I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.ControlFactory.Add(this.DAT01_APCHISAB);

            if (string.IsNullOrEmpty(_APCCDAC))
            {
                this.TXT01_APCCDAC.SetReadOnly(false);
                this.SetStartingFocus(TXT01_APCCDAC);
            }
            else
            {
                this.TXT01_APCCDAC.SetReadOnly(true);
                this.SetStartingFocus(TXT01_APCNMAC);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_77HIH195", _APCCDAC);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.CurrentDataTableRowMapping(dt, "01");
                }
            }
        } 
        #endregion

        #region Description : 닫기
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        } 
        #endregion

        #region Description : 저장
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            if (string.IsNullOrEmpty(this._APCCDAC))
            {
                this.DbConnector.Attach("TY_P_AC_77HII196", this.ControlFactory, "01"); // 저장
            }
            else
            {
                this.DbConnector.Attach("TY_P_AC_77HIJ197", this.ControlFactory, "01"); // 수정
            }
            this.DbConnector.ExecuteNonQuery();            

            this.ShowMessage("TY_M_GB_23NAD873");

            BTN61_CLO_Click(null, null);
            
        } 
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        } 
        #endregion

     }
}
