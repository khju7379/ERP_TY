using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 용역직 인사기본사항 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2015.01.19 17:46
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_51GAN168 : 용역직 인사기본사항 조회(팝업)
    ///  TY_P_HR_51JHW190 : 용역직 인사기본사항 등록
    ///  TY_P_HR_51KDN197 : 용역직 인사기본사항 수정
    ///  TY_P_HR_51KDO198 : 용역직 인사기본사항 순번 가져오기
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  KYJJCD : 직종
    ///  KBSEXGB : 성별
    ///  KYBIRGB : 음력구분
    ///  KBGDATE : 그룹입사일
    ///  KBIDATE : 입사일자
    ///  KBBIRTH : 생년월일
    ///  KBHANGL : 한글이름
    ///  KBHANJA : 한자이름
    ///  KBJUMIN : 주민번호
    ///  KBRFID : RF카드번호
    ///  KBTELNO : 전화번호
    ///  KBUPCD : 우편번호
    ///  KYSEQ : 순번
    ///  KYYEAR : 년도
    ///  VNJUSO : 주소
    /// </summary>
    public partial class TYHRGB002I : TYBase
    {
        string fsBASEQ   = string.Empty;
        string fsBANAME  = string.Empty;
        string fsBAJUMIN = string.Empty;

        string fsEDITGN = string.Empty;

        #region Description : 폼 로드
        public TYHRGB002I(string sBASEQ, string sBANAME, string sBAJUMIN)
        {
            InitializeComponent();

            fsBASEQ = sBASEQ;
            fsBANAME = sBANAME;
            fsBAJUMIN = sBAJUMIN;
        }

        private void TYHRGB002I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            // 확인
            UP_Run(fsBASEQ, fsBANAME, fsBAJUMIN);

            if (string.IsNullOrEmpty(this.fsBANAME) && string.IsNullOrEmpty(this.fsBAJUMIN))
            {
                this.TXT01_BANAME.SetReadOnly(false);
                this.TXT01_BAJUMIN.SetReadOnly(false);

                fsEDITGN = "ADD";

                SetStartingFocus(this.TXT01_BANAME);
            }
            else
            {
                this.TXT01_BANAME.SetReadOnly(true);
                this.TXT01_BAJUMIN.SetReadOnly(true);

                fsEDITGN = "EDIT";

                SetStartingFocus(this.CBH01_BACODE.CodeText);
            }
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            if (string.IsNullOrEmpty(this.fsBANAME) && string.IsNullOrEmpty(this.fsBAJUMIN))
            {
                this.DbConnector.Attach("TY_P_HR_853BO924",
                                        this.TXT01_BANAME.GetValue().ToString(),
                                        this.TXT01_BAJUMIN.GetValue().ToString(),
                                        this.CBH01_BACODE.GetValue().ToString(),
                                        this.CBH01_BACODE.GetText().ToString(),
                                        this.TXT01_BAJUSO.GetValue().ToString(),
                                        this.TXT01_BACARNO.GetValue().ToString(),
                                        this.TXT01_BATEL.GetValue().ToString(),
                                        this.CBO01_BASTOPGN.GetValue().ToString(),
                                        this.TXT01_BASTOPSU.GetValue().ToString(),
                                        this.CBO01_BASAFEGN.GetValue().ToString(),
                                        DateTime.Now.ToString("yyyyMMdd"),
                                        DateTime.Now.ToString("HHmmss").ToString(),
                                        DateTime.Now.ToString("yyyyMMdd"),
                                        DateTime.Now.ToString("yyyyMMdd"),
                                        DateTime.Now.ToString("HHmmss").ToString()
                                        );
            }
            else
            {
                this.DbConnector.Attach("TY_P_HR_853BP925",
                                        this.CBH01_BACODE.GetValue().ToString(),
                                        this.CBH01_BACODE.GetText().ToString(),
                                        this.TXT01_BAJUSO.GetValue().ToString(),
                                        this.TXT01_BACARNO.GetValue().ToString(),
                                        this.TXT01_BATEL.GetValue().ToString(),
                                        this.CBO01_BASTOPGN.GetValue().ToString(),
                                        this.TXT01_BASTOPSU.GetValue().ToString(),
                                        this.CBO01_BASAFEGN.GetValue().ToString(),
                                        DateTime.Now.ToString("yyyyMMdd"),
                                        DateTime.Now.ToString("HHmmss").ToString(),
                                        this.TXT01_BASEQ.GetValue().ToString(),
                                        this.TXT01_BANAME.GetValue().ToString(),
                                        this.TXT01_BAJUMIN.GetValue().ToString()
                                        );
            }
            this.DbConnector.ExecuteTranQuery();

            this.ShowMessage("TY_M_GB_23NAD873");
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sJUMIN = string.Empty;

            if (this.TXT01_BAJUMIN.GetValue().ToString().Replace("-", "") != "")
            {
                if (this.TXT01_BAJUMIN.GetValue().ToString().Replace("-", "").Length < 7)
                {
                    this.ShowCustomMessage("주민번호는 최소 7자리이상은 입력해야합니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }

                sJUMIN = this.TXT01_BAJUMIN.GetValue().ToString().Replace("-", "").Substring(0, 7);
            }

            //이름 주민번호 동일인 체크
            if (fsEDITGN != "EDIT")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_D3TBZ809", this.TXT01_BANAME.GetValue().ToString(),
                                                            sJUMIN
                                                            );
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    this.ShowCustomMessage("이름, 주민번호가 동일한 사람이 존재합니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            // 저장하시겠습니까?
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 데이터 확인
        private void UP_Run(string sBASEQ, string sBANAME, string sBAJUMIN)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_853BA923", sBASEQ, sBANAME, sBAJUMIN);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");
            }
        }
        #endregion

        #region Description : 순번 가져오기
        private string UP_getSEQ(string sYEAR)
        {
            string sSEQ = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_51KDO198", sYEAR);

            sSEQ = this.DbConnector.ExecuteScalar().ToString();

            return sSEQ;
        }
        #endregion
    }
}
