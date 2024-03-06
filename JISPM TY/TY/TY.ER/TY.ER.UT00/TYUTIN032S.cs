using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.UT00
{
    /// <summary>
    /// 선급자재 관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2013.02.19 09:59
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_32J79125 : 선급자재 미생성 조회
    ///  TY_P_MR_32J7A126 : 선급자재 생성 조회
    ///  TY_P_MR_32J7A127 : 선급자재 DETAIL 조회
    ///  TY_P_MR_32J7A128 : 선급자재 DETAIL 하위 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_32J7C129 : 선급자재 생성 조회
    ///  TY_S_MR_32J7M130 : 선급자재 DETAIL 조회
    ///  TY_S_UT_71NIB578 : 선급자재 DETAIL 하위 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CANCEL : 취소
    ///  CREATE : 생성
    ///  INQ : 조회
    ///  JASAN_CRE : 자산생성
    ///  JASAN_DEL : 자산삭제
    ///  JPNO_CRE : 전표생성
    ///  JPNO_DEL : 전표삭제
    ///  FXDDPMK : 귀속부서
    ///  FXDSAUP : 선급사업부
    ///  FXDGETDATE : 취득일
    ///  GCDACGHAP : 계정총액
    ///  GDAESANGHAP : 대상총액
    ///  GJANGHAP : 잔액
    /// </summary>
    public partial class TYUTIN032S : TYBase
    {
        private string fsFXDNUM = string.Empty;

        private string fsJASANNUM = string.Empty;
        private string fsPONUM    = string.Empty;
        private string fsRRNUM    = string.Empty;
        private string fsVEND     = string.Empty;
        private string fsITEMCODE = string.Empty;
        private string fsCGVEND   = string.Empty;
        private string fsCHGUBUN  = string.Empty;
        private string fsGUBUN    = string.Empty;

        private string fsIJWKTYPE = string.Empty;
        private string fsIJTMGUBN = string.Empty;
        private string fsIJIPDATE = string.Empty;

        #region Description : 페이지 로드
        public TYUTIN032S()
        {
            InitializeComponent();
        }

        private void TYUTIN032S_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_UT_71NIB578.Initialize();
            this.FPS91_TY_S_UT_71NI2577.Initialize();

            this.DTP01_EDDATE.SetValue(DateTime.Now.AddDays(+1).ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.AddDays(+1).ToString("yyyy-MM-dd"));

            this.BTN61_INQ_Click(null, null);

            this.BTN62_INQ_Click(null, null);

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 출고 특허신청서 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            // 출고 특허신청서 기존
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_71NHZ571",
                Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                this.CBO01_JIWKTYPE.GetValue().ToString()
                );

            //TY_P_UT_91VFS663 수정

            //this.DbConnector.Attach
            //    (
            //    "TY_P_UT_91VFS663",
            //    Get_Date(this.DTP01_STDATE.GetValue().ToString()),
            //    Get_Date(Convert.ToDateTime(this.DTP01_EDDATE.GetValue().ToString()).AddDays(1).ToString("yyyyMMdd")),
            //    this.CBO01_JIWKTYPE.GetValue().ToString()
            //    );
            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_71NIB578.SetValue(dt);
            
        }
        #endregion

        #region Description : 입고 특허신청서 조회 버튼
        private void BTN62_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            // 입고 특허신청서
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_71NI0576",
                Get_Date(this.DTP01_SDATE.GetValue().ToString()),
                Get_Date(this.DTP01_EDATE.GetValue().ToString()),
                this.CBO01_GGUBUN.GetValue().ToString()
                );

            //this.DbConnector.Attach
            //    (
            //    "TY_P_UT_91VGZ665",
            //    Get_Date(this.DTP01_SDATE.GetValue().ToString()),
            //    Get_Date(Convert.ToDateTime(this.DTP01_EDDATE.GetValue().ToString()).AddDays(1).ToString("yyyyMMdd")),
            //    this.CBO01_GGUBUN.GetValue().ToString()
            //    );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_71NI2577.SetValue(dt);
        }
        #endregion
    }
}