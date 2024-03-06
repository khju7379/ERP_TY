using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AF00
{
    /// <summary>
    /// 자회사 관리카드 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2013.09.04 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_3944B514 : EIS 자회사 관리카드 관리
    ///  TY_P_AC_3944E515 : EIS 자회사 관리카드 등록
    ///  TY_P_AC_3944E516 : EIS 자회사 관리카드 수정
    ///  TY_P_AC_3944G517 : EIS 자회사 관리카드 삭제
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CONFIRM : 확인
    ///  REM : 삭제
    ///  SAV : 저장
    ///  ESCUSTGB : 계열사구분
    ///  ESCSDATE : 설립년월일
    ///  ESCADDRES : 소재지
    ///  ESCCMPNM : 회사명
    ///  ESCCORPNO : 법인등록번호
    ///  ESCCUSTN1 : 사업내용1
    ///  ESCCUSTN2 : 사업내용2
    ///  ESCCUSTN3 : 사업내용3
    ///  ESCCUSTN4 : 사업내용4
    ///  ESCCUSTN5 : 사업내용5
    ///  ESCEMPCNT : 종업원수
    ///  ESCFAXNO : 팩스번호
    ///  ESCFIRNM : 대표자명
    ///  ESCPRONM1 : 수입구조1
    ///  ESCPRONM2 : 수입구조2
    ///  ESCPRONM3 : 수입구조3
    ///  ESCPRONM4 : 수입구조4
    ///  ESCPRONM5 : 수입구조5
    ///  ESCSAUPNO : 사업자번호
    ///  ESCTELNO : 전화번호
    ///  ESCUPJONG : 업종
    /// </summary>
    public partial class TYAFMA001I : TYBase
    {
        private string sGUBUN = string.Empty;
        private string fsCompanyCode;

        #region Description : 페이지 로드
        public TYAFMA001I()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }

        private void TYAFMA001I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            switch (TYUserInfo.EmpNo.Substring(0, 2))
            {
                case "HT":
                    fsCompanyCode = "TH";
                    break;
                case "TG":
                    fsCompanyCode = "TG";
                    break;
                case "TS":
                    fsCompanyCode = "TS";
                    break;
                case "TL":
                    fsCompanyCode = "TL";
                    break;
                default:
                    fsCompanyCode = "";
                    break;
            }

            if (fsCompanyCode != "")
            {
                this.CBH01_ESCUSTGB.SetValue(fsCompanyCode);
                this.CBH01_ESCUSTGB.SetReadOnly(true);
            }
                        
            if (TYUserInfo.EmpNo.Substring(0, 2).ToString() != "TY")
            {
                this.BTN61_CONFIRM_Click(null, null);
            }
        }
        #endregion

        #region Description : 확인 버튼
        private void BTN61_CONFIRM_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_3944B514", this.CBH01_ESCUSTGB.GetValue());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sGUBUN = "UPT";
                if (this.CBH01_ESCUSTGB.GetValue().ToString() != "")
                {
                    this.CurrentDataTableRowMapping(dt, "01");

                    this.SetStartingFocus(this.TXT01_ESCCMPNM);
                }
                else
                {
                    SetStartingFocus(this.CBH01_ESCUSTGB.CodeText);
                }
            }
            else
            {
                sGUBUN = "ADD";

                this.TXT01_ESCCMPNM.SetValue("");

                this.TXT01_ESCFIRNM.SetValue("");
                this.TXT01_ESCSAUPNO.SetValue("");
                this.TXT01_ESCUPJONG.SetValue("");
                this.TXT01_ESCCORPNO.SetValue("");
                this.TXT01_ESCEMPCNT.SetValue("");
                this.TXT01_ESCTELNO.SetValue("");
                this.TXT01_ESCFAXNO.SetValue("");
                this.TXT01_ESCADDRES.SetValue("");
                this.TXT01_ESCCUSTN1.SetValue("");
                this.TXT01_ESCCUSTN2.SetValue("");
                this.TXT01_ESCCUSTN3.SetValue("");
                this.TXT01_ESCCUSTN4.SetValue("");
                this.TXT01_ESCCUSTN5.SetValue("");
                this.TXT01_ESCPRONM1.SetValue("");
                this.TXT01_ESCPRONM2.SetValue("");
                this.TXT01_ESCPRONM3.SetValue("");
                this.TXT01_ESCPRONM4.SetValue("");
                this.TXT01_ESCPRONM5.SetValue("");
            }
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sProcedure = string.Empty;

            if (sGUBUN == "ADD")
            {
                sProcedure = "TY_P_AC_3944E515"; // 등록
            }
            else
            {
                sProcedure = "TY_P_AC_3944E516"; // 수정
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach(sProcedure, this.ControlFactory, "01");

            this.DbConnector.ExecuteNonQuery();

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_3944G517", this.CBH01_ESCUSTGB.GetValue());

            this.DbConnector.ExecuteNonQuery();

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
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

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 코드박스
        private void CBH01_ESCUSTGB_CodeBoxDataBinded(object sender, EventArgs e)
        {
            if (TYUserInfo.EmpNo.Substring(0, 2).ToString() != "TY")
            {
                this.BTN61_CONFIRM_Click(null, null);
            }
        }
        #endregion
    }
}