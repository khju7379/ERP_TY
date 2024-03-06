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
    public partial class TYHRNT01C5 : TYBase
    {
        private string fsWKCOMPANY;
        private string fsWKYEAR;
        private string fsWKSABUN;
        private string fsFixGubn;

        #region  Description : 폼 로드 이벤트
        public TYHRNT01C5(string sWKCOMPANY, string sWKYEAR, string sWKSABUN, string sFixGubn)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsWKCOMPANY = sWKCOMPANY;
            fsWKYEAR = sWKYEAR;
            fsWKSABUN = sWKSABUN;
            //fsFixGubn = "Y";
            fsFixGubn = sFixGubn;
        }

        private void TYHRNT01C5_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            TXT01_WFYEAR.SetValue(fsWKYEAR);
            CBH01_WFSABUN.SetValue(fsWKSABUN);

            TXT01_ANINVYEAR.SetReadOnly(true);
            CBO01_ANINVGUBN.SetReadOnly(true);

            UP_Set_ButtonStatus(true, false, false);

            this.UP_RightGrid_DataBinding();

            this.BTN61_NEW_Click(null, null);

            if (fsFixGubn == "Y")
            {
                BTN61_NEW.Visible = false;
                BTN61_SAV.Visible = false;
                BTN61_REM.Visible = false;
            }

            this.SetStartingFocus(CBH01_ANTYPECODE.CodeText);

        }
        #endregion

        #region  Description : 그리드 데이타 바인딩 이벤트
        private void UP_RightGrid_DataBinding()
        {
            this.FPS91_TY_S_HR_7AQD5896.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_7AQD4895", fsWKCOMPANY, fsWKYEAR, fsWKSABUN
                                                         );
            this.FPS91_TY_S_HR_7AQD5896.SetValue(this.DbConnector.ExecuteDataTable());
            
        }
        #endregion


        #region  Description :  FPS91_TY_S_HR_7AQD5896_CellDoubleClick 이벤트
        private void FPS91_TY_S_HR_7AQD5896_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_7AQDL897", this.FPS91_TY_S_HR_7AQD5896.GetValue("ANCOMPANY").ToString(),
                                                        this.FPS91_TY_S_HR_7AQD5896.GetValue("ANYEAR").ToString(),
                                                        this.FPS91_TY_S_HR_7AQD5896.GetValue("ANSABUN").ToString(),
                                                        this.FPS91_TY_S_HR_7AQD5896.GetValue("ANTYPECODE").ToString(),
                                                        this.FPS91_TY_S_HR_7AQD5896.GetValue("ANBANKCODE").ToString(),
                                                        this.FPS91_TY_S_HR_7AQD5896.GetValue("ANBANKACNUM").ToString()
                                                         );
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                TXT01_WFYEAR.SetValue(dt.Rows[0]["ANYEAR"].ToString());
                CBH01_WFSABUN.SetValue(dt.Rows[0]["ANSABUN"].ToString());
                CBH01_ANTYPECODE.SetValue(dt.Rows[0]["ANTYPECODE"].ToString());
                CBH01_ANBANKCODE.SetValue(dt.Rows[0]["ANBANKCODE"].ToString());
                TXT01_ANBANKACNUM.SetValue(dt.Rows[0]["ANBANKACNUM"].ToString());
                TXT01_ANYEARCHA.SetValue(dt.Rows[0]["ANYEARCHA"].ToString());
                TXT01_ANJOYEAR.SetValue(dt.Rows[0]["ANJOYEAR"].ToString());
                TXT01_ANPAYAMOUNT.SetValue(dt.Rows[0]["ANPAYAMOUNT"].ToString());
                TXT01_ANDEDAMOUNT.SetValue(dt.Rows[0]["ANDEDAMOUNT"].ToString());

                TXT01_ANINVYEAR.SetValue(dt.Rows[0]["ANINVYEAR"].ToString());
                CBO01_ANINVGUBN.SetValue(dt.Rows[0]["ANINVGUBN"].ToString());

                CBH01_ANTYPECODE.SetReadOnly(true);
                CBH01_ANBANKCODE.SetReadOnly(true);
                TXT01_ANBANKACNUM.SetReadOnly(true);                

                this.SetFocus(TXT01_ANPAYAMOUNT);

                if (dt.Rows[0]["ANNTSGN"].ToString() == "Y")
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
            CBH01_ANTYPECODE.SetValue("");
            CBH01_ANBANKCODE.SetValue("");
            TXT01_ANBANKACNUM.SetValue("");
            TXT01_ANYEARCHA.SetValue("");
            TXT01_ANJOYEAR.SetValue("");
            TXT01_ANPAYAMOUNT.SetValue("0");
            TXT01_ANDEDAMOUNT.SetValue("0");

            TXT01_ANINVYEAR.SetValue("");
            CBO01_ANINVGUBN.SetValue("");

            CBH01_ANTYPECODE.SetReadOnly(false);
            CBH01_ANBANKCODE.SetReadOnly(false);
            TXT01_ANBANKACNUM.SetReadOnly(false);

            UP_Set_ButtonStatus(true, true, false);

            this.SetFocus(CBH01_ANTYPECODE.CodeText);
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_7AQDL897", fsWKCOMPANY,
                                                        TXT01_WFYEAR.GetValue(),
                                                        CBH01_WFSABUN.GetValue(),
                                                        CBH01_ANTYPECODE.GetValue(),
                                                        CBH01_ANBANKCODE.GetValue(),
                                                        TXT01_ANBANKACNUM.GetValue().ToString().Replace("-","")
                                                         );
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                //수정
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_7AQDN899", TXT01_ANJOYEAR.GetValue().ToString(),
                                                            TXT01_ANYEARCHA.GetValue().ToString(),
                                                            TXT01_ANPAYAMOUNT.GetValue().ToString(),
                                                            TXT01_ANDEDAMOUNT.GetValue().ToString(),
                                                            CBH01_ANTYPECODE.GetValue().ToString() == "61" ?  TXT01_ANINVYEAR.GetValue().ToString() : "0000",
                                                            CBH01_ANTYPECODE.GetValue().ToString() == "61" ?  CBO01_ANINVGUBN.GetValue().ToString() : "",
                                                            TYUserInfo.EmpNo,
                                                            fsWKCOMPANY,
                                                            TXT01_WFYEAR.GetValue(),
                                                            CBH01_WFSABUN.GetValue(),
                                                            CBH01_ANTYPECODE.GetValue(),
                                                            CBH01_ANBANKCODE.GetValue(),
                                                            TXT01_ANBANKACNUM.GetValue().ToString().Replace("-", "")
                                                             );

            }
            else
            {
                //등록
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_7AQDN898", fsWKCOMPANY,
                                                            TXT01_WFYEAR.GetValue(),
                                                            CBH01_WFSABUN.GetValue(),
                                                            CBH01_ANTYPECODE.GetValue(),
                                                            CBH01_ANBANKCODE.GetValue(),
                                                            TXT01_ANBANKACNUM.GetValue().ToString().Replace("-", ""),
                                                            TXT01_ANJOYEAR.GetValue().ToString(),
                                                            TXT01_ANYEARCHA.GetValue().ToString(),
                                                            TXT01_ANPAYAMOUNT.GetValue().ToString(),
                                                            TXT01_ANDEDAMOUNT.GetValue().ToString(),
                                                            CBH01_ANTYPECODE.GetValue().ToString() == "61" ? TXT01_ANINVYEAR.GetValue().ToString() : "0000",
                                                            CBH01_ANTYPECODE.GetValue().ToString() == "61" ? CBO01_ANINVGUBN.GetValue().ToString() : "",
                                                            "N",
                                                            TYUserInfo.EmpNo
                                                             );
            }
            this.DbConnector.ExecuteTranQuery();

            UP_ProCedure_FixCall();

            this.UP_RightGrid_DataBinding();

            UP_Set_ButtonStatus(true, true, false);

            this.ShowMessage("TY_M_GB_23NAD873");
        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            double dANPAYAMOUNTTotal = 0;
            bool bCheck = false;

            this.TXT01_ANBANKACNUM.SetValue(this.TXT01_ANBANKACNUM.GetValue().ToString().Replace("-",""));

            //주택종합저축은 가입년도가 필수
            if (CBH01_ANTYPECODE.GetValue().ToString() == "32")
            {
                if (TXT01_ANJOYEAR.GetValue().ToString() == "")
                {
                    this.SetFocus(TXT01_ANJOYEAR);
                    this.ShowCustomMessage("가입년도을 입력하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            //중소기업창업투자조합
            if (CBH01_ANTYPECODE.GetValue().ToString() == "61")
            {
                if (TXT01_ANINVYEAR.GetValue().ToString() == "")
                {
                    this.SetFocus(TXT01_ANINVYEAR);
                    this.ShowCustomMessage("투자년도 입력하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
                if (CBO01_ANINVGUBN.GetValue().ToString() == "")
                {
                    this.SetFocus(CBO01_ANINVGUBN);
                    this.ShowCustomMessage("투자구분을 선택하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            //연금계좌 세액공제 대상금액 : 연 700만원 한도(전체)
            //연금저축계좌 납입액 : 연 400만원 한도 (총급여 1억2천만원 또는 종합소득금액 1억원 초과자 300만원)
            if (CBH01_ANTYPECODE.GetValue().ToString() == "11" || CBH01_ANTYPECODE.GetValue().ToString() == "12" || CBH01_ANTYPECODE.GetValue().ToString() == "22")
            {
                if (Convert.ToDouble(Get_Numeric(TXT01_ANPAYAMOUNT.GetValue().ToString())) > 4000000)
                {
                    this.SetFocus(TXT01_ANPAYAMOUNT);
                    this.ShowCustomMessage("연금저축계좌 납입액은 400만원을 초과 할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }

                //연 700만원 한도(전체)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_7CKIX348", fsWKCOMPANY, TXT01_WFYEAR.GetValue(), CBH01_WFSABUN.GetValue());
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["ANTYPECODE"].ToString() == CBH01_ANTYPECODE.GetValue().ToString() &&
                            dt.Rows[i]["ANBANKCODE"].ToString() == CBH01_ANBANKCODE.GetValue().ToString() &&
                            dt.Rows[i]["ANBANKACNUM"].ToString() == TXT01_ANBANKACNUM.GetValue().ToString())
                        {
                            bCheck = true;
                            dANPAYAMOUNTTotal = dANPAYAMOUNTTotal + Convert.ToDouble(Get_Numeric(TXT01_ANPAYAMOUNT.GetValue().ToString()));
                        }
                        else
                        {
                            dANPAYAMOUNTTotal = dANPAYAMOUNTTotal + Convert.ToDouble(dt.Rows[i]["ANPAYAMOUNT"].ToString());
                        }
                    }                  
                }

                if (bCheck != true)
                {
                    dANPAYAMOUNTTotal = dANPAYAMOUNTTotal + Convert.ToDouble(Get_Numeric(TXT01_ANPAYAMOUNT.GetValue().ToString()));
                }

                if (dANPAYAMOUNTTotal > 7000000)
                {
                    this.SetFocus(TXT01_ANPAYAMOUNT);
                    this.ShowCustomMessage("연금계좌(과학기술+퇴직연금+연금저축) 공제대상 금액의 합은 700만원을 초과 할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
            this.DbConnector.Attach("TY_P_HR_7AQDP900",
                                                        fsWKCOMPANY,
                                                            TXT01_WFYEAR.GetValue(),
                                                            CBH01_WFSABUN.GetValue(),
                                                            CBH01_ANTYPECODE.GetValue(),
                                                            CBH01_ANBANKCODE.GetValue(),
                                                            TXT01_ANBANKACNUM.GetValue().ToString()
                                                        );
            this.DbConnector.ExecuteTranQuery();

            UP_ProCedure_FixCall();

            this.UP_RightGrid_DataBinding();

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
            this.DbConnector.Attach("TY_P_HR_77JDB223", fsWKCOMPANY, TXT01_WFYEAR.GetValue() , CBH01_WFSABUN.GetValue(), TYUserInfo.EmpNo ,TYUserInfo.SecureKey, "Y",  "");
            this.DbConnector.ExecuteScalar();
        }
        #endregion          

        #region  Description : CBH01_ANTYPECODE_CodeBoxDataBinded 이벤트
        private void CBH01_ANTYPECODE_CodeBoxDataBinded(object sender, EventArgs e)
        {
            if (CBH01_ANTYPECODE.GetValue().ToString() != "61")
            {
                TXT01_ANINVYEAR.SetReadOnly(true);
                CBO01_ANINVGUBN.SetReadOnly(true);
            }
            else
            {
                TXT01_ANINVYEAR.SetReadOnly(false);
                CBO01_ANINVGUBN.SetReadOnly(false);
                CBO01_ANINVGUBN.SetValue("1");
            }
        }
        #endregion

    }
}
