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
    public partial class TYHRKB011I : TYBase
    {
        string fsKYYEAR = string.Empty;
        string fsKYSEQ = string.Empty;

        #region Description : 폼 로드
        public TYHRKB011I(string sKYYEAR, string sKYSEQ)
        {
            InitializeComponent();

            fsKYYEAR = sKYYEAR;
            fsKYSEQ = sKYSEQ;
        }

        private void TYHRKB011I_Load(object sender, System.EventArgs e)
        {
            TXT01_KYYEAR.SetReadOnly(true);
            TXT01_KYSEQ.SetReadOnly(true);

            UP_Run(fsKYYEAR, fsKYSEQ);
            SetStartingFocus(this.TXT01_KBHANGL);
        }
        #endregion

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            if (string.IsNullOrEmpty(this.fsKYYEAR))
            {
                string sYEAR = System.DateTime.Now.ToString("yyyy");

                this.DbConnector.Attach("TY_P_HR_51JHW190",
                                        sYEAR,
                                        UP_getSEQ(sYEAR),
                                        TXT01_KBHANGL.GetValue().ToString(),
                                        TXT01_KBHANJA.GetValue().ToString(),
                                        CBO01_KBSEXGB.GetValue().ToString(),
                                        CBH01_KYJJCD.GetValue().ToString(),
                                        DTP01_KBIDATE.GetString(),
                                        DTP01_KBGDATE.GetString(),
                                        TXT01_VNJUSO.GetValue().ToString(),
                                        TXT01_KBUPCD.GetValue().ToString(),                                        
                                        TXT01_KBTELNO.GetValue().ToString(),
                                        MTB01_KBBIRTH.GetValue().ToString().Replace("-", ""),
                                        CBO01_KYBIRGB.GetValue().ToString(),
                                        TXT01_KBRFID.GetValue().ToString(),
                                        TYUserInfo.EmpNo);
            }
            else
            {
                this.DbConnector.Attach("TY_P_HR_51KDN197",
                                        TXT01_KBHANGL.GetValue().ToString(),
                                        TXT01_KBHANJA.GetValue().ToString(),
                                        CBO01_KBSEXGB.GetValue().ToString(),
                                        CBH01_KYJJCD.GetValue().ToString(),
                                        DTP01_KBIDATE.GetString(),
                                        DTP01_KBGDATE.GetString(),
                                        TXT01_VNJUSO.GetValue().ToString(),
                                        TXT01_KBUPCD.GetValue().ToString(),                                        
                                        TXT01_KBTELNO.GetValue().ToString(),
                                        MTB01_KBBIRTH.GetValue().ToString().Replace("-", ""),
                                        CBO01_KYBIRGB.GetValue().ToString(),
                                        TXT01_KBRFID.GetValue().ToString(),
                                        TYUserInfo.EmpNo,
                                        TXT01_KYYEAR.GetValue().ToString(),
                                        TXT01_KYSEQ.GetValue().ToString());
            }
            this.DbConnector.ExecuteTranQuery();

            this.ShowMessage("TY_M_GB_23NAD873");
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 데이터 조회
        private void UP_Run(string sKYYEAR, string sKYSEQ)
        {       
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_51GAN168", sKYYEAR, sKYSEQ);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                TXT01_KYYEAR.SetValue(dt.Rows[0]["KYYEAR"].ToString());
                TXT01_KYSEQ.SetValue(dt.Rows[0]["KYSEQ"].ToString());
                TXT01_KBHANGL.SetValue(dt.Rows[0]["KYHANGL"].ToString());
                TXT01_KBHANJA.SetValue(dt.Rows[0]["KYHANJA"].ToString());
                CBO01_KBSEXGB.SetValue(dt.Rows[0]["KYSEXGB"].ToString());
                CBH01_KYJJCD.SetValue(dt.Rows[0]["KYJJCD"].ToString());
                DTP01_KBIDATE.SetValue(dt.Rows[0]["KYIDATE"].ToString());
                DTP01_KBGDATE.SetValue(dt.Rows[0]["KYGDATE"].ToString());                
                MTB01_KBBIRTH.SetValue(dt.Rows[0]["KYBIRTH"].ToString());
                CBO01_KYBIRGB.SetValue(dt.Rows[0]["KYBIRGB"].ToString());
                TXT01_KBUPCD.SetValue(dt.Rows[0]["KYUPCD"].ToString());
                TXT01_KBTELNO.SetValue(dt.Rows[0]["KYTELNO"].ToString());
                TXT01_VNJUSO.SetValue(dt.Rows[0]["KYJUSO"].ToString());
                TXT01_KBRFID.SetValue(dt.Rows[0]["KYRFID"].ToString());
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
