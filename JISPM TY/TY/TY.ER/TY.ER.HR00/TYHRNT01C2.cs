using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 연말정산 종전근무지 관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.07.11 15:38
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_77BFV113 : 연말정산 종전근무지 등록
    ///  TY_P_HR_77BFW114 : 연말정산 종전근무지 삭제
    ///  TY_P_HR_77BFW115 : 연말정산 종전근무지 수정
    ///  TY_P_HR_77BFX116 : 연말정산 종전근무지 확인
    ///  TY_P_HR_77BFY117 : 연말정산 종전근무지 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_77BFY118 : 연말정산 종전근무지 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  NEW : 신규
    ///  REM : 삭제
    ///  SAV : 저장
    ///  WKSABUN : 사번
    ///  WKEDATE : 근무종료일자
    ///  WKSDATE : 근무시작일자
    ///  WKEMPLOYAMT : 고용보험
    ///  WKHEALTHAMT : 의료보험료
    ///  WKINCOMETAX : 소득세
    ///  WKISPYAMOUNT : 인정상여
    ///  WKMPYAMOUNT : 급여
    ///  WKNATIONAMT : 국민연금
    ///  WKNOJOAMT : 노조회비
    ///  WKRESTAX : 주민세
    ///  WKSANGHO : 상호
    ///  WKSAUPNO : 사업자번호
    ///  WKSPYAMOUNT : 상여
    ///  WKTAXAMT : 근로소득세
    ///  WKYEAR : 년도
    /// </summary>
    public partial class TYHRNT01C2 : TYBase
    {
        private string fsWKCOMPANY;
        private string fsWKYEAR;
        private string fsWKSABUN;
        private string fsFixGubn;

        #region  Description : 폼 로드 이벤트
        public TYHRNT01C2(string sWKCOMPANY, string sWKYEAR, string sWKSABUN, string sFixGubn)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsWKCOMPANY = sWKCOMPANY;
            fsWKYEAR = sWKYEAR;
            fsWKSABUN = sWKSABUN;
            fsFixGubn = sFixGubn;
        }

        private void TYHRNT01C2_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            TXT01_WKYEAR.SetValue(fsWKYEAR);
            CBH01_WKSABUN.SetValue(fsWKSABUN);

            DTP01_WKSDATE.SetValue(DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd"));
            DTP01_WKEDATE.SetValue(DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd"));

            if (fsFixGubn == "Y")
            {
                UP_Set_ButtonStatus(false, false, false);
            }
            else
            {
                UP_Set_ButtonStatus(true, true, false);
            }

            this.UP_DataBinding();

            if (fsFixGubn != "Y")
            {
                this.BTN61_NEW_Click(null, null);
            }
        }
        #endregion

        #region  Description : 전근무지 자료 조회
        private void UP_DataBinding()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77BFY117", TYUserInfo.SecureKey, "Y",  fsWKCOMPANY, TXT01_WKYEAR.GetValue(), CBH01_WKSABUN.GetValue());
            this.FPS91_TY_S_HR_77BFY118.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 전근무지 자료 확인
        private void UP_Run(string sWKCOMPANY, string sWKYEAR, string sWKSABUN, string sWKSAUPNO)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77BFX116", fsWKCOMPANY, sWKYEAR, sWKSABUN, sWKSAUPNO);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");

                UP_Set_ButtonStatus(true, true, true);
            }
        }
        #endregion

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            MTB01_WKSAUPNO.SetValue("");
            TXT01_WKSANGHO.SetValue("");
            DTP01_WKSDATE.SetValue(DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd"));
            DTP01_WKEDATE.SetValue(DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd"));
            TXT01_WKMPYAMOUNT.SetValue(0);
            TXT01_WKSPYAMOUNT.SetValue(0);
            TXT01_WKISPYAMOUNT.SetValue(0);
            TXT01_WKTAXAMT.SetValue(0);
            TXT01_WKHEALTHAMT.SetValue(0);
            TXT01_WKNATIONAMT.SetValue(0);
            TXT01_WKEMPLOYAMT.SetValue(0);
            TXT01_WKNOJOAMT.SetValue(0);
            TXT01_WKINCOMETAX.SetValue(0);
            TXT01_WKRESTAX.SetValue(0);

            UP_Set_ButtonStatus(true, true, false);

            this.SetFocus(MTB01_WKSAUPNO);
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {           

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77BFW114", fsWKCOMPANY, TXT01_WKYEAR.GetValue(), CBH01_WKSABUN.GetValue(), MTB01_WKSAUPNO.GetValue().ToString().Replace("-", ""));
            this.DbConnector.ExecuteNonQueryList();

            this.UP_DataBinding();

            this.BTN61_NEW_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {   
            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }            
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77BFX116", fsWKCOMPANY, TXT01_WKYEAR.GetValue(), CBH01_WKSABUN.GetValue(), MTB01_WKSAUPNO.GetValue().ToString().Replace("-","") );
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_77BFW115", TXT01_WKSANGHO.GetValue(),
                                                            DTP01_WKSDATE.GetString().ToString().Replace("19000101",""),
                                                            DTP01_WKEDATE.GetString().ToString().Replace("19000101",""),
                                                            TXT01_WKMPYAMOUNT.GetValue().ToString(),
                                                            TXT01_WKSPYAMOUNT.GetValue().ToString(),
                                                            TXT01_WKISPYAMOUNT.GetValue().ToString(),
                                                            TXT01_WKTAXAMT.GetValue().ToString(),
                                                            TXT01_WKHEALTHAMT.GetValue().ToString(),
                                                            TXT01_WKNATIONAMT.GetValue().ToString(),
                                                            TXT01_WKEMPLOYAMT.GetValue().ToString(),
                                                            TXT01_WKNOJOAMT.GetValue().ToString(),
                                                            TXT01_WKINCOMETAX.GetValue().ToString(),
                                                            TXT01_WKRESTAX.GetValue().ToString(),
                                                            TYUserInfo.EmpNo,  
                                                           fsWKCOMPANY, TXT01_WKYEAR.GetValue(), CBH01_WKSABUN.GetValue(), MTB01_WKSAUPNO.GetValue().ToString().Replace("-", "")                                                            
                                              );
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_77BFV113", fsWKCOMPANY, TXT01_WKYEAR.GetValue(), CBH01_WKSABUN.GetValue(), MTB01_WKSAUPNO.GetValue().ToString().Replace("-", ""),
                                                            TXT01_WKSANGHO.GetValue() ,
                                                            DTP01_WKSDATE.GetString().ToString().Replace("19000101",""),
                                                            DTP01_WKEDATE.GetString().ToString().Replace("19000101",""),
                                                            TXT01_WKMPYAMOUNT.GetValue().ToString(),
                                                            TXT01_WKSPYAMOUNT.GetValue().ToString(),
                                                            TXT01_WKISPYAMOUNT.GetValue().ToString(),
                                                            TXT01_WKTAXAMT.GetValue().ToString(),
                                                            TXT01_WKHEALTHAMT.GetValue().ToString(),
                                                            TXT01_WKNATIONAMT.GetValue().ToString(),
                                                            TXT01_WKEMPLOYAMT.GetValue().ToString(),
                                                            TXT01_WKNOJOAMT.GetValue().ToString(),
                                                            TXT01_WKINCOMETAX.GetValue().ToString(),
                                                            TXT01_WKRESTAX.GetValue().ToString(),
                                                            TYUserInfo.EmpNo
                    );
            }
            this.DbConnector.ExecuteTranQuery();

            this.UP_DataBinding();

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sWKSDATE = DTP01_WKSDATE.GetString().ToString();
            string sWKEDATE = DTP01_WKEDATE.GetString().ToString();

            if (sWKSDATE.Substring(0, 4) != TXT01_WKYEAR.GetValue().ToString())
            {
                this.ShowCustomMessage("귀속년도와 근무년도가 같아야 합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (sWKEDATE.Substring(0, 4) != TXT01_WKYEAR.GetValue().ToString())
            {
                this.ShowCustomMessage("귀속년도와 근무년도가 같아야 합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            DateTime dts = Convert.ToDateTime(sWKSDATE.Substring(0, 4) + "-" + sWKSDATE.Substring(4, 2) + "-" + sWKSDATE.Substring(6, 2));
            DateTime dte = Convert.ToDateTime(sWKEDATE.Substring(0, 4) + "-" + sWKEDATE.Substring(4, 2) + "-" + sWKEDATE.Substring(6, 2));

            if (dts > dte)
            {
                this.ShowCustomMessage("근무시작일자가 근무종료일보다 늦을수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : FPS91_TY_S_HR_77BFY118_CellDoubleClick 이벤트
        private void FPS91_TY_S_HR_77BFY118_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            UP_Run(this.FPS91_TY_S_HR_77BFY118.GetValue("WKCOMPANY").ToString(),
                   this.FPS91_TY_S_HR_77BFY118.GetValue("WKYEAR").ToString(),
                   this.FPS91_TY_S_HR_77BFY118.GetValue("WKSABUN").ToString(),
                   this.FPS91_TY_S_HR_77BFY118.GetValue("WKSAUPNO").ToString().Replace("-",""));

            this.SetFocus(this.MTB01_WKSAUPNO);
        }
        #endregion

        #region  Description : 버튼 Visible 타입 이벤트
        private void UP_Set_ButtonStatus(bool bNew, bool bSav, bool bRem)
        {
            this.BTN61_NEW.Visible = bNew;
            this.BTN61_SAV.Visible = bSav;
            this.BTN61_REM.Visible = bRem;
        }
        #endregion

        #region  Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

       

    }
}
