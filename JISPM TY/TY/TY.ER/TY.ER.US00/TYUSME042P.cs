using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.US00
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
    ///  TY_S_US_92CE5728 : 선급자재 DETAIL 하위 조회
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
    public partial class TYUSME042P : TYBase
    {
        private string fsFXDNUM = string.Empty;

        private string fsJASANNUM = string.Empty;
        private string fsPONUM = string.Empty;
        private string fsRRNUM = string.Empty;
        private string fsVEND = string.Empty;
        private string fsITEMCODE = string.Empty;
        private string fsCGVEND = string.Empty;
        private string fsCHGUBUN = string.Empty;
        private string fsGUBUN = string.Empty;

        private string fsIJWKTYPE = string.Empty;
        private string fsIJTMGUBN = string.Empty;
        private string fsIJIPDATE = string.Empty;

        private string fsIJHWAJU = string.Empty;
        private string fsIJHWAMUL = string.Empty;
        private string fsIJTANKNO = string.Empty;
        private string fsIJIPQTY = string.Empty;

        private string fsIJCARNO = string.Empty;
        private string fsIJCONTAIN = string.Empty;
        private string fsIJSEALNUM = string.Empty;

        private string fsIJIPTIME1 = string.Empty;
        private string fsIJIPTIME2 = string.Empty;

        private string fsIJDESC = string.Empty;

        private string fsHJDESC1 = string.Empty;
        private string fsHMDESC1 = string.Empty;

        #region Description : 페이지 로드
        public TYUSME042P()
        {
            InitializeComponent();
        }

        private void TYUSME042P_Load(object sender, System.EventArgs e)
        {
            //this.DTP01_STDATE.SetValue(DateTime.Now.AddYears(-1).ToString("yyyy-MM"));
            this.DTP01_STDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));
            this.DTP01_EDDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.SetFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sDATE = string.Empty;
            string sProcedure = string.Empty;

            sDATE = "(" + Get_Date(this.DTP01_STDATE.GetValue().ToString()) + " ~ " + Get_Date(this.DTP01_EDDATE.GetValue().ToString()) + ")";

            if (this.CBO01_GGUBUN.GetValue().ToString().ToString() == "ALL")
            {
                sProcedure = "TY_P_US_94CGN365";
            }
            else
            {
                sProcedure = "TY_P_US_94CGO366";
            }

            DataTable dt = new DataTable();

            this.DbConnector.Attach
                (
                sProcedure.ToString(),
                sDATE.ToString(),
                Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                Get_Date(this.DTP01_EDDATE.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (this.CBO01_GGUBUN.GetValue().ToString().ToString() == "ALL")
                {
                    SectionReport rpt = new TYUSME042R1();

                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait;

                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
                else
                {
                    SectionReport rpt = new TYUSME042R2();

                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait;

                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
            }
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}