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
    /// 연말정산 의료비 등록 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.07.19 17:16
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_77JC6219 : 연말정산 소득자공제명세서 조회
    ///  TY_P_HR_77JHK227 : 연말정산 영수증 의료비 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_77JHH226 : 연말정산 부양가족명세 조회
    ///  TY_S_HR_77JHL228 : 연말정산 영수증 의료비 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  NEW : 신규
    ///  SAV : 저장
    ///  NSBUSNID : 사업자번호
    ///  NSDAT_CD : 자료코드
    ///  NSFORM_CD : 서식코드
    ///  NSSUM : 납입금액계
    ///  NSTRADE_NM : 상    호
    ///  WFCODE : 가족코드
    ///  WFJUMIN : 주민번호
    ///  WFNAME : 가족이름
    ///  WFSABUN : 사　　번
    ///  WFYEAR : 년    도
    /// </summary>
    public partial class TYHRNT01C4 : TYBase
    {
        private string fsWKCOMPANY;
        private string fsWKYEAR;
        private string fsWKSABUN;
        private string fsFixGubn;

        #region  Description : 폼 로드 이벤트
        public TYHRNT01C4(string sWKCOMPANY, string sWKYEAR, string sWKSABUN, string sFixGubn)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsWKCOMPANY = sWKCOMPANY;
            fsWKYEAR = sWKYEAR;
            fsWKSABUN = sWKSABUN;
            fsFixGubn = sFixGubn;
        }

        private void TYHRNT01C4_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            TXT01_WFYEAR.SetValue(fsWKYEAR);
            CBH01_WFSABUN.SetValue(fsWKSABUN);

            this.MTB02_DOBUSNID.Visible = false;
            this.MTB01_DOBUSNID.Visible = true;

            UP_Set_ButtonStatus(false, false, false);

            FPS91_TY_S_HR_77KDT253.Initialize();

            this.UP_LeftGrid_DataBinding();

            if (fsFixGubn == "Y")
            {
                BTN61_NEW.Visible = false;
                BTN61_SAV.Visible = false;
                BTN61_REM.Visible = false;
            }


        }
        #endregion

        #region  Description : 좌측 그리드 데이타 바인딩 이벤트
        private void UP_LeftGrid_DataBinding()
        {
            this.FPS91_TY_S_HR_77KDO252.Initialize();            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77JC6219", fsWKCOMPANY, TXT01_WFYEAR.GetValue(), CBH01_WFSABUN.GetValue(), TYUserInfo.SecureKey, "Y");
            this.FPS91_TY_S_HR_77KDO252.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 우측 그리드 데이타 바인딩 이벤트
        private void UP_RightGrid_DataBinding(string sNSCOMPANY, string sNSYEAR, string sNSSABUN,  string sNSresid)
        {
            this.FPS91_TY_S_HR_77KDT253.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77KF2254", sNSCOMPANY, sNSYEAR, sNSSABUN, sNSresid, TYUserInfo.SecureKey, "Y"
                                                         );
            this.FPS91_TY_S_HR_77KDT253.SetValue(this.DbConnector.ExecuteDataTable());
            this.SpreadSumRowAdd(this.FPS91_TY_S_HR_77KDT253, "DOTRADE_NM", "합  계", SumRowType.Sum, "DOCONB_SUM");
        }
        #endregion


        #region  Description :  FPS91_TY_S_HR_77KDO252_CellDoubleClick 이벤트
        private void FPS91_TY_S_HR_77KDO252_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.UP_RightGrid_DataBinding(this.FPS91_TY_S_HR_77KDO252.GetValue("WFCOMPANY").ToString(),
                                          this.FPS91_TY_S_HR_77KDO252.GetValue("WFYEAR").ToString(),
                                          this.FPS91_TY_S_HR_77KDO252.GetValue("WFSABUN").ToString(),                                        
                                          this.FPS91_TY_S_HR_77KDO252.GetValue("WFJUMIN").ToString()
                                        );

            MTB01_WFJUMIN.SetValue(this.FPS91_TY_S_HR_77KDO252.GetValue("WFJUMIN").ToString());
            TXT01_WFNAME.SetValue(this.FPS91_TY_S_HR_77KDO252.GetValue("WFNAME").ToString());
            TXT01_WFCODE.SetValue(this.FPS91_TY_S_HR_77KDO252.GetValue("WFCODE").ToString());
            CBO01_DORELATION.SetValue(this.FPS91_TY_S_HR_77KDO252.GetValue("RELATIONCD").ToString());

            this.BTN61_NEW_Click(null, null);
            
        }
        #endregion

        #region  Description :  FPS91_TY_S_HR_77KDT253_CellDoubleClick 이벤트
        private void FPS91_TY_S_HR_77KDT253_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77KFE258", TYUserInfo.SecureKey, "Y", this.FPS91_TY_S_HR_77KDT253.GetValue("DOCOMPANY").ToString(),
                                                        this.FPS91_TY_S_HR_77KDT253.GetValue("DOYEAR").ToString(),
                                                        this.FPS91_TY_S_HR_77KDT253.GetValue("DOSABUN").ToString(),
                                                        this.FPS91_TY_S_HR_77KDT253.GetValue("DOSEQ").ToString()
                                                         );
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                    this.CBH01_DODONATION_CD.SetReadOnly(true);                    

                    //사업자번호인지 주민번호 인지 판단
                    if (dt.Rows[0]["DObusnid"].ToString().Trim().Length > 10)
                    {
                        this.MTB02_DOBUSNID.Visible = true;
                        this.MTB01_DOBUSNID.Visible = false;
                        this.MTB02_DOBUSNID.SetReadOnly(true);

                        this.MTB02_DOBUSNID.SetValue(dt.Rows[0]["DObusnid"].ToString().Trim());
                    }
                    else
                    {
                        this.MTB01_DOBUSNID.Visible = true;
                        this.MTB02_DOBUSNID.Visible = false;
                        this.MTB01_DOBUSNID.SetReadOnly(true);                    
                    }

                    this.CurrentDataTableRowMapping(dt, "01");                  

                    this.SetFocus(TXT01_DOTRADE_NM);

                    if (dt.Rows[0]["DONTSGN"].ToString() == "Y")
                    {
                        this.ShowCustomMessage("국세청자료는 수정,삭제가 불가합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        if (fsFixGubn != "Y")
                        {
                            UP_Set_ButtonStatus(true, false, false);
                        }
                    }
                    else
                    {
                        if (fsFixGubn != "Y")
                        {
                            UP_Set_ButtonStatus(true, true, true);
                        }
                    }
            }
        }
        #endregion

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            //순번생성
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_7BHER039", fsWKCOMPANY,
                                                        TXT01_WFYEAR.GetValue(),
                                                        CBH01_WFSABUN.GetValue()
                                                         );
            string sSeq = this.DbConnector.ExecuteScalar().ToString();

            TXT01_DOSEQ.SetValue(sSeq);

            CBH01_DODONATION_CD.SetValue("");
            MTB01_DOBUSNID.SetValue("");
            MTB02_DOBUSNID.SetValue("");
            TXT01_DOTRADE_NM.SetValue("");
            TXT01_DOCOUNT.SetValue("0");
            TXT01_DOAMOUNT.SetValue("0");
            TXT01_DOENCAMOUNT.SetValue("0");
            TXT01_DOCONB_SUM.SetValue("0");

            CBO01_DOCONTENT.SetValue("1");

            this.CBH01_DODONATION_CD.SetReadOnly(false);
            this.MTB01_DOBUSNID.SetReadOnly(false);
            this.MTB02_DOBUSNID.SetReadOnly(false);

            this.SetFocus(CBH01_DODONATION_CD.CodeText);

            if (fsFixGubn != "Y")
            {
                UP_Set_ButtonStatus(true, true, false);
            }
            
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77KFE258", TYUserInfo.SecureKey, "Y", fsWKCOMPANY,
                                                        TXT01_WFYEAR.GetValue(),
                                                        CBH01_WFSABUN.GetValue(),
                                                        TXT01_DOSEQ.GetValue().ToString()
                                                         );
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                //수정
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_7BHF0041", CBH01_DODONATION_CD.GetValue().ToString(),
                                                            MTB01_DOBUSNID.GetValue().ToString().Replace("-", ""),
                                                            TXT01_DOTRADE_NM.GetValue(),
                                                            CBO01_DORELATION.GetValue().ToString(),
                                                            TXT01_WFNAME.GetValue(),
                                                            MTB01_WFJUMIN.GetValue().ToString().Replace("-", ""),
                                                            TYUserInfo.SecureKey,
                                                            TXT01_DOCOUNT.GetValue(),
                                                            TXT01_DOAMOUNT.GetValue(),
                                                            TXT01_DOENCAMOUNT.GetValue(),
                                                            TXT01_DOCONB_SUM.GetValue(),
                                                            CBO01_DOCONTENT.GetValue(),
                                                            //TXT01_DONTSGN.GetValue().ToString().Trim(),
                                                            TYUserInfo.EmpNo,
                                                            fsWKCOMPANY,
                                                            TXT01_WFYEAR.GetValue(),
                                                            CBH01_WFSABUN.GetValue(),
                                                            TXT01_DOSEQ.GetValue().ToString()
                                                             );

            }
            else
            {
                //등록
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_7BHEU040", fsWKCOMPANY,
                                                            TXT01_WFYEAR.GetValue(),
                                                            CBH01_WFSABUN.GetValue(),
                                                            TXT01_DOSEQ.GetValue().ToString(),
                                                            CBH01_DODONATION_CD.GetValue().ToString(),
                                                            MTB01_DOBUSNID.GetValue().ToString().Replace("-", ""),
                                                            TXT01_DOTRADE_NM.GetValue(),
                                                            CBO01_DORELATION.GetValue().ToString(),
                                                            TXT01_WFNAME.GetValue(),
                                                            MTB01_WFJUMIN.GetValue().ToString().Replace("-", ""),
                                                            TYUserInfo.SecureKey,
                                                            TXT01_DOCOUNT.GetValue().ToString(),
                                                            TXT01_DOAMOUNT.GetValue().ToString(),
                                                            TXT01_DOENCAMOUNT.GetValue().ToString(),
                                                            TXT01_DOCONB_SUM.GetValue().ToString(),
                                                            CBO01_DOCONTENT.GetValue(),
                                                            "N",
                                                            TYUserInfo.EmpNo
                                                             );
            }
            this.DbConnector.ExecuteTranQuery();

            UP_ProCedure_FixCall();

            this.CBH01_DODONATION_CD.SetReadOnly(true);
            this.MTB01_DOBUSNID.SetReadOnly(true);

            this.UP_LeftGrid_DataBinding();
            this.UP_RightGrid_DataBinding(fsWKCOMPANY, TXT01_WFYEAR.GetValue().ToString(), CBH01_WFSABUN.GetValue().ToString(), MTB01_WFJUMIN.GetValue().ToString().Replace("-","")  );

            UP_Set_ButtonStatus(true, true, false);

            this.ShowMessage("TY_M_GB_23NAD873");
        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            double dDOAMOUNT = Convert.ToDouble(Get_Numeric(TXT01_DOAMOUNT.GetValue().ToString()));
            double dDOENCAMOUNT = Convert.ToDouble(Get_Numeric(TXT01_DOENCAMOUNT.GetValue().ToString()));

            TXT01_DOCONB_SUM.SetValue((dDOAMOUNT + dDOENCAMOUNT).ToString());          

            //정치자금 기부금이외 자료는 사업자번호, 상호가 필수 
            if (CBH01_DODONATION_CD.GetValue().ToString().Trim() != "20")
            {
                if (this.MTB01_DOBUSNID.GetValue().ToString().Replace("-", "").Trim().Length < 10)
                {
                    this.SetFocus(MTB01_DOBUSNID);
                    this.ShowCustomMessage("사업자번호를 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return; 
                }

                if (TXT01_DOTRADE_NM.GetValue().ToString().Trim() == "")
                {
                    this.SetFocus(TXT01_DOTRADE_NM);
                    this.ShowCustomMessage("상 호를 입력하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            //정치자금(20), 우리사주기부금(42)는 본인만 등록가능하다.
            if (CBH01_DODONATION_CD.GetValue().ToString().Trim() == "20" || CBH01_DODONATION_CD.GetValue().ToString().Trim() == "42")
            {
                if (CBO01_DORELATION.GetValue().ToString() != "1")
                {
                    this.ShowCustomMessage("정치자금(20), 우리사주기부금(42)는 본인만 공제가 가능함!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            if (dDOAMOUNT > 0 && Convert.ToInt32(Get_Numeric(TXT01_DOCOUNT.GetValue().ToString())) <= 0)
            {
                this.SetFocus(TXT01_DOCOUNT);
                this.ShowCustomMessage("기부건수는 0보다 커야 합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if ((dDOAMOUNT + dDOENCAMOUNT) <= 0)
            {
                this.SetFocus(TXT01_DOAMOUNT);
                this.ShowCustomMessage("기부금액 입력하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            //정치자금 기부금은 합산 10만원이하만 가능하다
            if (CBH01_DODONATION_CD.GetValue().ToString().Trim() == "20")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_D29HJ583", fsWKCOMPANY,
                                                            TXT01_WFYEAR.GetValue(),
                                                            CBH01_WFSABUN.GetValue(),
                                                            CBH01_DODONATION_CD.GetValue().ToString().Trim(),
                                                            TXT01_DOSEQ.GetValue()
                                                             );
                double dSumDOAMOUNT = Convert.ToDouble(this.DbConnector.ExecuteScalar().ToString());

                dSumDOAMOUNT = dSumDOAMOUNT + Convert.ToDouble(TXT01_DOAMOUNT.GetValue().ToString());

                if (dSumDOAMOUNT > 100000)
                {
                    this.SetFocus(TXT01_DOAMOUNT);
                    this.ShowCustomMessage("정치자금 기부금은 10만원을 초과 할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77KFC257", fsWKCOMPANY,
                                                        TXT01_WFYEAR.GetValue(),
                                                        CBH01_WFSABUN.GetValue(),
                                                        TXT01_DOSEQ.GetValue().ToString()
                                                        );
            this.DbConnector.ExecuteTranQuery();

            UP_ProCedure_FixCall();

            this.UP_LeftGrid_DataBinding();
            this.UP_RightGrid_DataBinding(fsWKCOMPANY, TXT01_WFYEAR.GetValue().ToString(), CBH01_WFSABUN.GetValue().ToString(), MTB01_WFJUMIN.GetValue().ToString().Replace("-",""));

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

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region  Description : 버튼 Visible 타입 이벤트
        private void UP_Set_ButtonStatus(bool bNew, bool bSav, bool bRem )
        {
            this.BTN61_NEW.Visible = bNew;
            this.BTN61_SAV.Visible = bSav;
            this.BTN61_REM.Visible = bRem;
        }
        #endregion
        
        #region  Description : 연말정산 국세청 확정 프로시저 호출 함수
        private void UP_ProCedure_FixCall()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77JDB223", fsWKCOMPANY, TXT01_WFYEAR.GetValue() , CBH01_WFSABUN.GetValue(), TYUserInfo.EmpNo , TYUserInfo.SecureKey, "Y",  "");
            this.DbConnector.ExecuteScalar();
        }
        #endregion       

        #region  Description :  TEXT박스 Leave 이벤트  함수
        private void TXT01_NSSUM_Leave(object sender, EventArgs e)
        {
            double dNSSUM = Convert.ToDouble(Get_Numeric(TXT01_DOAMOUNT.GetValue().ToString()));
            double dNSSBDY_APLN_SUM = Convert.ToDouble(Get_Numeric(TXT01_DOENCAMOUNT.GetValue().ToString()));

            TXT01_DOCONB_SUM.SetValue( (dNSSUM + dNSSBDY_APLN_SUM).ToString());

        }

        private void TXT01_NSSBDY_APLN_SUM_Leave(object sender, EventArgs e)
        {
            double dNSSUM = Convert.ToDouble(Get_Numeric(TXT01_DOAMOUNT.GetValue().ToString()));
            double dNSSBDY_APLN_SUM = Convert.ToDouble(Get_Numeric(TXT01_DOENCAMOUNT.GetValue().ToString()));

            TXT01_DOCONB_SUM.SetValue((dNSSUM + dNSSBDY_APLN_SUM).ToString());
        }
        #endregion





    }
}
