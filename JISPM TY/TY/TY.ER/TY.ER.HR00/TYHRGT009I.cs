using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.HR00
{
    /// <summary>
    /// 개인별 월연장수당 관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2020.11.12 14:40
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_ABCEY153 : 개인별OT시간관리 확인
    ///  TY_P_HR_ABCEZ154 : 개인별OT시간관리 수정
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  POPYGUBN : 급여구분
    ///  POSABUN : 사번
    ///  PODATE : 근태일자
    ///  POJIDATE : 지급일자
    ///  POYYMM : 급여년월
    ///  POAMOUNT : 총연장수당금액
    ///  POGJTIME : 교대인정시간
    ///  POHTTIME : 하프인정시간
    ///  POHUTIME : 특근인정시간
    ///  POINTIME : 총인정시간
    ///  POJOTIME : 조출인정시간
    ///  PONTTIME : 철야인정시간
    ///  PONUTIME : 심야특근인정시간
    ///  POORDPAY : 통상임금
    ///  POOTTIME : 연장인정시간
    /// </summary>
    public partial class TYHRGT009I : TYBase
    {
	    private string fsPOSABUN;
        private string fsPODATE;

        #region  Description : 폼 로드 이벤트
        public TYHRGT009I(string sPODATE, string sPOSABUN)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsPOSABUN = sPOSABUN;
            fsPODATE = sPODATE;
        }

        private void TYHRGT009I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            DTP01_POYYMM.SetReadOnly(true);
            DTP01_POJIDATE.SetReadOnly(true);
            DTP01_PODATE.SetReadOnly(true);
            
            UP_GetDataBinding();
        }
        #endregion       

        #region  Description : UP_GetDataBinding 이벤트
        private void UP_GetDataBinding()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_ABCEY153",  fsPODATE, fsPOSABUN);
            this.CurrentDataTableRowMapping(this.DbConnector.ExecuteDataTable(), "01");           

        }
        #endregion
        
        
        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_ABCEZ154", TXT01_POORDPAY.GetValue().ToString(), TXT01_POAMOUNT.GetValue().ToString(),
                                                        TYUserInfo.EmpNo,
                                                        "P",
                                                        CBH01_POPYGUBN.GetValue(),
                                                        DTP01_POYYMM.GetString().Substring(0,6),
                                                        DTP01_POJIDATE.GetString(),
                                                        CBH01_POSABUN.GetValue(),
                                                        DTP01_PODATE.GetString());
            this.DbConnector.ExecuteTranQuery();

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_ABCFW156", DTP01_POYYMM.GetString().Substring(0,6), "1", CBH01_POPYGUBN.GetValue(), DTP01_POJIDATE.GetString(), "1");
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["APMJPNO"].ToString() != "")
                {
                    this.ShowCustomMessage("급여전표가 발행되어 수정할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion
    }
}
