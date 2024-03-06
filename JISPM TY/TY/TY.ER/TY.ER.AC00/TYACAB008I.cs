using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// K-GAAP 계정과목 등록 팝업 프로그램입니다.
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
    ///  TY_P_AC_25A7A272 : K-GAAP 계정과목 수정
    ///  TY_P_AC_25A7C276 : K-GAAP 계정과목 저장
    ///  TY_P_AC_25A7D277 : K-GAAP 계정과목 확인
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
    ///  AG1ACHL1 : 상위계정코드1
    ///  AG1ACHL2 : 상위계정코드2
    ///  AG1ACHL3 : 상위계정코드3
    ///  AG1ACHL4 : 상위계정코드4
    ///  AG1ACHL5 : 상위계정코드5
    ///  AG1NCDAC : 신계정과목
    ///  AG1IDAC : 계정구분
    ///  AG1LVAC : LEVEL
    ///  AG1TAG01 : 차/대(D/C)
    ///  AG1TAG10 : 충당금
    ///  AG1YNBS : B/S계정
    ///  AG1YNBSDT : B/S계정세목
    ///  AG1YNCM : 제조원가
    ///  AG1YNIS : I/S계정
    ///  AG1YNISDT : I/S계정세목
    ///  AG1ABAC : 계정과목약명
    ///  AG1CDAC : 계정코드
    ///  AG1NMAC : 계정과목명
    ///  AG1NMENAC : 계정과목영문명
    ///  AG1NMHNAC : 계정과목한문명
    /// </summary>
    public partial class TYACAB008I : TYBase
    {
        private string _AG1CDAC;
        private TYData DAT01_AG1HISAB;

        
        public TYACAB008I(string AG1CDAC)
        {
            InitializeComponent();

            this.SetPopupStyle();
            this._AG1CDAC = AG1CDAC;
            //this.DAT01_AG1HISAB = new TYData("DAT01_AG1HISAB", Employer.UserID);
            this.DAT01_AG1HISAB = new TYData("DAT01_AG1HISAB", (object)"0310-M");
        }

        #region Description : Page_Load
        private void TYACAB008I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.ControlFactory.Add(this.DAT01_AG1HISAB);

            if (string.IsNullOrEmpty(_AG1CDAC))
            {
                this.TXT01_AG1CDAC.SetReadOnly(false);
                this.SetStartingFocus(TXT01_AG1CDAC);
            }
            else
            {
                this.TXT01_AG1CDAC.SetReadOnly(true);
                this.SetStartingFocus(TXT01_AG1NMAC);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_25A7D277", _AG1CDAC);
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
            
            if (string.IsNullOrEmpty(this._AG1CDAC))
            {
                this.DbConnector.Attach("TY_P_AC_25A7C276", this.ControlFactory, "01"); // 저장
            }
            else
            {
                this.DbConnector.Attach("TY_P_AC_25A7A272", this.ControlFactory, "01"); // 수정
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
