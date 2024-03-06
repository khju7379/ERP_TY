using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 부가세 옵션 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2013.11.26 09:30
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_3BQ9L453 : 부가세 옵션 등록
    ///  TY_P_AC_3BQ9M454 : 부가세 옵션 수정
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_MR_2BD3Y285 : 수정하시겠습니까?
    ///  TY_M_MR_2BD3Z286 : 수정하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  EDIT : 수정
    ///  SAV : 저장
    ///  BADVEND : 거래처
    ///  INQOPTION2 : 조회구분
    ///  VNGUBUN : 구분
    ///  AOCNELEAMT : 전자발급공제액(건당)
    ///  AODEPRAMT : 상각 제외금액
    ///  AOLMELEAMT : 전자발급공제액(한도)
    ///  AOZEROTAX : 영세율제출사유
    ///  INQOPTION : 조회구분
    ///  PRYEAR : 년도
    /// </summary>
    public partial class TYACTX005I : TYBase
    {
        private TYData DAT01_MAHISAB;
        public string fsAOYEAR = string.Empty;
        public string fsAOBRANCH = string.Empty;
        public string fsAOBNEDCD = string.Empty;
        public string fsAOPERGB = string.Empty;
        public string fsAOCONFGB = string.Empty;
        public string fsAOCNELEAMT = string.Empty;
        public string fsAOLMELEAMT = string.Empty;
        public string fsAODEPRAMT = string.Empty;
        public string fsAOZEROTAX = string.Empty;
        public string fsAOINEMDEL = string.Empty;
        public string fsAOIDENDEL = string.Empty;
        public string fsAOINEMPEN = string.Empty;
        public string fsAOIDENPEN = string.Empty;
        public string fsAOINEMPAY = string.Empty;
        public string fsAOIDENPAY = string.Empty;
        public string fsAOINEMZEO = string.Empty;
        public string fsAOIDENZEO = string.Empty;
        public string fsAOBANKCD = string.Empty;
        public string fsAOGUJANUM = string.Empty;
        public string fsAOGENCHK = string.Empty;
        public string fsAOGENNUM = string.Empty;

        #region Description : 페이지 로드
        public TYACTX005I(string sAOYEAR,    string sAOBRANCH,   string sAOBNEDCD,   string sAOPERGB, 
                          string sAOCONFGB,  string sAOCNELEAMT, string sAOLMELEAMT, string sAODEPRAMT, 
                          string sAOZEROTAX, string sAOINEMDEL,  string sAOIDENDEL,  string sAOINEMPEN,
                          string sAOIDENPEN, string sAOINEMPAY,  string sAOIDENPAY,  string sAOINEMZEO, 
                          string sAOIDENZEO, string sAOBANKCD,   string sAOGUJANUM,  string sAOGENCHK,
                          string sAOGENNUM)
        {
            InitializeComponent();

            this.fsAOYEAR = sAOYEAR;
            this.fsAOBRANCH = sAOBRANCH;
            this.fsAOBNEDCD = sAOBNEDCD;
            this.fsAOPERGB = sAOPERGB;
            this.fsAOCONFGB = sAOCONFGB;
            this.fsAOCNELEAMT = sAOCNELEAMT;
            this.fsAOLMELEAMT = sAOLMELEAMT;
            this.fsAODEPRAMT = sAODEPRAMT;
            this.fsAOZEROTAX = sAOZEROTAX;
            this.fsAOINEMDEL = sAOINEMDEL;
            this.fsAOIDENDEL = sAOIDENDEL;
            this.fsAOINEMPEN = sAOINEMPEN;
            this.fsAOIDENPEN = sAOIDENPEN;
            this.fsAOINEMPAY = sAOINEMPAY;
            this.fsAOIDENPAY = sAOIDENPAY;
            this.fsAOINEMZEO = sAOINEMZEO;
            this.fsAOIDENZEO = sAOIDENZEO;
            this.fsAOBANKCD = sAOBANKCD;
            this.fsAOGUJANUM = sAOGUJANUM;
            this.fsAOGENCHK = sAOGENCHK;
            this.fsAOGENNUM = sAOGENNUM;

            this.DAT01_MAHISAB = new TYData("DAT01_MAHISAB", TYUserInfo.EmpNo);
        }

        private void TYACTX005I_Load(object sender, System.EventArgs e)
        {

            this.ControlFactory.Add(this.DAT01_MAHISAB);
            UP_AOBANKCD();
            UP_AOGENNUM();
            if (string.IsNullOrEmpty(this.fsAOYEAR))
            {
                UP_BADVEND("1");
                this.TXT01_PRYEAR.SetValue(DateTime.Now.ToString("yyyy"));
                this.SetStartingFocus(this.TXT01_PRYEAR);
                this.TXT01_AOGENNUM.SetValue("1286");
            }
            else
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                    (
                    "TY_P_AC_42BBW322",
                    fsAOYEAR,
                    fsAOBRANCH,
                    fsAOPERGB,
                    fsAOCONFGB
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    //마감 구분 (Y)
                    if (dt.Rows[0]["E1FINISH"].ToString() == "Y")
                    {
                        this.TXT01_PRYEAR.SetReadOnly(true);
                        this.CBO01_VNGUBUN.SetReadOnly(true);
                        this.CBO01_BADVEND.SetReadOnly(true);
                        this.CBO01_INQOPTION.SetReadOnly(true);
                        this.TXT01_AODEPRAMT.SetReadOnly(true);
                        this.CBO01_AOZEROTAX.SetReadOnly(true);
                        this.TXT01_AOINEMDEL.SetReadOnly(true);
                        this.TXT01_AOIDENDEL.SetReadOnly(true);
                        this.TXT01_AOINEMPEN.SetReadOnly(true);
                        this.TXT01_AOIDENPEN.SetReadOnly(true);
                        this.TXT01_AOINEMPAY.SetReadOnly(true);
                        this.TXT01_AOIDENPAY.SetReadOnly(true);
                        this.TXT01_AOINEMZEO.SetReadOnly(true);
                        this.TXT01_AOIDENZEO.SetReadOnly(true);
                        this.CBO01_AOBANKCD.SetReadOnly(true);
                        this.TXT01_AOGUJANUM.SetReadOnly(true);
                        this.CBO01_AOGENCHK.SetReadOnly(true);
                        this.TXT01_AOGENNUM.SetReadOnly(true);
                        this.BTN61_SAV.Visible = false;
                    }
                }

                UP_BADVEND(fsAOBRANCH);
                this.TXT01_PRYEAR.SetValue(fsAOYEAR);
                this.CBO01_VNGUBUN.SetValue(fsAOBRANCH);
                this.CBO01_BADVEND.SetValue(fsAOBNEDCD);
                this.CBO01_INQOPTION.SetValue(fsAOPERGB + fsAOCONFGB);
                //this.CBO01_INQOPTION2.SetValue(fsAOCONFGB);
                //this.TXT01_AOCNELEAMT.SetValue(fsAOCNELEAMT);
                //this.TXT01_AOLMELEAMT.SetValue(fsAOLMELEAMT);
                this.TXT01_AODEPRAMT.SetValue(fsAODEPRAMT);
                this.CBO01_AOZEROTAX.SetValue(fsAOZEROTAX);
                this.TXT01_AOINEMDEL.SetValue(fsAOINEMDEL);
                this.TXT01_AOIDENDEL.SetValue(fsAOIDENDEL);
                this.TXT01_AOINEMPEN.SetValue(fsAOINEMPEN);
                this.TXT01_AOIDENPEN.SetValue(fsAOIDENPEN);
                this.TXT01_AOINEMPAY.SetValue(fsAOINEMPAY);
                this.TXT01_AOIDENPAY.SetValue(fsAOIDENPAY);
                this.TXT01_AOINEMZEO.SetValue(fsAOINEMZEO);
                this.TXT01_AOIDENZEO.SetValue(fsAOIDENZEO);
                this.CBO01_AOBANKCD.SetValue(fsAOBANKCD);
                this.TXT01_AOGUJANUM.SetValue(fsAOGUJANUM);
                this.CBO01_AOGENCHK.SetValue(fsAOGENCHK);
                this.TXT01_AOGENNUM.SetValue(fsAOGENNUM);

                this.TXT01_PRYEAR.SetReadOnly(true);
                this.CBO01_VNGUBUN.SetReadOnly(true);
                this.CBO01_BADVEND.SetReadOnly(true);
                this.CBO01_INQOPTION.SetReadOnly(true);
                //this.CBO01_INQOPTION2.SetReadOnly(true);

                this.BTN61_PRE.Visible = false;
                //this.SetStartingFocus(this.TXT01_AOCNELEAMT);
            }
        }
        #endregion

        #region Description : 닫기버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 저장버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ShowMessage("TY_M_GB_23NAD871"))
                {
                    if (UP_AOBANK_CHECK(this.CBO01_AOBANKCD.GetValue().ToString(),
                                        this.TXT01_AOGUJANUM.GetValue().ToString()))
                    {
                        if (string.IsNullOrEmpty(this.fsAOBRANCH))  // 저장
                        {
                            this.DbConnector.CommandClear();

                            this.DbConnector.Attach
                                (
                                "TY_P_AC_3BQ9L453",
                                this.TXT01_PRYEAR.GetValue(),
                                this.CBO01_VNGUBUN.GetValue(),
                                this.CBO01_BADVEND.GetValue(),
                                getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                                getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                                "",
                                "",
                                this.TXT01_AODEPRAMT.GetValue(),
                                this.CBO01_AOZEROTAX.GetValue(),
                                this.TXT01_AOINEMDEL.GetValue(),
                                this.TXT01_AOIDENDEL.GetValue(),
                                this.TXT01_AOINEMPEN.GetValue(),
                                this.TXT01_AOIDENPEN.GetValue(),
                                this.TXT01_AOINEMPAY.GetValue(),
                                this.TXT01_AOIDENPAY.GetValue(),
                                this.TXT01_AOINEMZEO.GetValue(),
                                this.TXT01_AOIDENZEO.GetValue(),
                                this.CBO01_AOBANKCD.GetValue(),
                                this.TXT01_AOGUJANUM.GetValue(),
                                this.CBO01_AOGENCHK.GetValue(),
                                this.TXT01_AOGENNUM.GetValue()
                                );
                            this.DbConnector.ExecuteTranQueryList();

                            //마감정보 등록
                            this.DbConnector.CommandClear();

                            this.DbConnector.Attach
                                (
                                "TY_P_AC_42A4S311",
                                this.TXT01_PRYEAR.GetValue(),
                                this.CBO01_VNGUBUN.GetValue(),
                                this.CBO01_BADVEND.GetValue(),
                                getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                                getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                                "N"
                                );
                            this.DbConnector.ExecuteTranQueryList();
                        }
                        else // 수정
                        {
                            this.DbConnector.CommandClear();

                            this.DbConnector.Attach
                                (
                                "TY_P_AC_3BQ9M454",
                                "",
                                "",
                                this.TXT01_AODEPRAMT.GetValue(),
                                this.CBO01_AOZEROTAX.GetValue(),
                                this.TXT01_AOINEMDEL.GetValue(),
                                this.TXT01_AOIDENDEL.GetValue(),
                                this.TXT01_AOINEMPEN.GetValue(),
                                this.TXT01_AOIDENPEN.GetValue(),
                                this.TXT01_AOINEMPAY.GetValue(),
                                this.TXT01_AOIDENPAY.GetValue(),
                                this.TXT01_AOINEMZEO.GetValue(),
                                this.TXT01_AOIDENZEO.GetValue(),
                                this.CBO01_AOBANKCD.GetValue(),
                                this.TXT01_AOGUJANUM.GetValue(),
                                this.CBO01_AOGENCHK.GetValue(),
                                this.TXT01_AOGENNUM.GetValue(),
                                this.TXT01_PRYEAR.GetValue(),
                                this.CBO01_VNGUBUN.GetValue(),
                                this.CBO01_BADVEND.GetValue(),
                                getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                                getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)
                                );
                            this.DbConnector.ExecuteTranQueryList();
                        }
                        this.ShowMessage("TY_M_GB_23NAD873");
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch
            {
                this.ShowMessage("TY_M_AC_3219C986");
            }
        }
        #endregion

        #region Description : 사업장 구분
        private void CBO01_VNGUBUN_SelectedIndexChanged(object sender, EventArgs e)
        {
            UP_BADVEND(this.CBO01_VNGUBUN.GetValue().ToString());
        }
        #endregion

        #region Description : 사업장 구분에 따른 거래처코드 가져오기
        private void UP_BADVEND(string sCODE)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3C3BY520",
                sCODE.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.CBO01_BADVEND.DataBind(dt, false);
        }
        #endregion

        #region Description : 국세환급은행 가져오기
        private void UP_AOBANKCD()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_416B1936"
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.CBO01_AOBANKCD.DataBind(dt, true);
        }
        #endregion

        #region Description : 은행, 계좌 유효성 체크
        private bool UP_AOBANK_CHECK(string sAOBANKCD, string sAOGUJANUM)
        {
            bool rst = true;
            if (sAOBANKCD != "" && sAOGUJANUM == "")
            {
                this.ShowMessage("TY_M_AC_2445M441");
                rst = false;
            }
            if (sAOBANKCD == "" && sAOGUJANUM != "")
            {
                this.ShowMessage("TY_M_AC_2445M440");
                rst = false;
            }
            if (sAOBANKCD != "" && sAOGUJANUM != "")
            {
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_4163B943",
                    sAOGUJANUM.Trim());

                dt = this.DbConnector.ExecuteDataTable();
                if (dt == null || dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_AC_4163H945");
                    rst = false;
                }
            }

            return rst; 
        }
        #endregion

        #region Description : 총괄승인여부
        private void CBO01_AOGENCHK_SelectedIndexChanged(object sender, EventArgs e)
        {
            UP_AOGENNUM();
        }

        private void UP_AOGENNUM()
        {
            if (this.CBO01_AOGENCHK.GetValue().ToString() == "Y")
            {
                this.TXT01_AOGENNUM.SetReadOnly(false);
            }
            else
            {
                this.TXT01_AOGENNUM.SetReadOnly(true);
            }
            this.TXT01_AOGENNUM.SetReadOnly(false);
        }
        #endregion

        #region Description : 이전 버튼 이벤트
        private void BTN61_PRE_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_427BN252",
                    this.CBO01_VNGUBUN.GetValue(),
                    this.CBO01_BADVEND.GetValue()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                this.TXT01_AODEPRAMT.SetValue(dt.Rows[0]["AODEPRAMT"].ToString());
                this.CBO01_AOZEROTAX.SetValue(dt.Rows[0]["AOZEROTAX"].ToString());
                this.TXT01_AOINEMDEL.SetValue(dt.Rows[0]["AOINEMDEL"].ToString());
                this.TXT01_AOIDENDEL.SetValue(dt.Rows[0]["AOIDENDEL"].ToString());
                this.TXT01_AOINEMPEN.SetValue(dt.Rows[0]["AOINEMPEN"].ToString());
                this.TXT01_AOIDENPEN.SetValue(dt.Rows[0]["AOIDENPEN"].ToString());
                this.TXT01_AOINEMPAY.SetValue(dt.Rows[0]["AOINEMPAY"].ToString());
                this.TXT01_AOIDENPAY.SetValue(dt.Rows[0]["AOIDENPAY"].ToString());
                this.TXT01_AOINEMZEO.SetValue(dt.Rows[0]["AOINEMZEO"].ToString());
                this.TXT01_AOIDENZEO.SetValue(dt.Rows[0]["AOIDENZEO"].ToString());
                this.CBO01_AOBANKCD.SetValue(dt.Rows[0]["AOBANKCD"].ToString());
                this.TXT01_AOGUJANUM.SetValue(dt.Rows[0]["AOGUJANUM"].ToString());
                this.CBO01_AOGENCHK.SetValue(dt.Rows[0]["AOGENCHK"].ToString());
                this.TXT01_AOGENNUM.SetValue(dt.Rows[0]["AOGENNUM"].ToString());
            }
            catch{
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion
    }
}
