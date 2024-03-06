using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using GrapeCity.ActiveReports;
using TY.ER.GB00;

namespace TY.ER.HR00
{
    /// <summary>
    /// 원천번호 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2015.03.24 15:37
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_53OFO856 : 원천번호 조회(인사)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_53OFQ858 : 원전번호 조회(인사)
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  B7CDAC : 계정코드
    /// </summary>
    public partial class TYHRKB24C2 : TYBase
    {


        public string fsPSSABUN;
        public string fsPSYDATE;
        public string fsPSGUBN;
        public string fsPSTYPE;
        public string fsPSJKCD;

        #region Description : 폼 로드 이벤트
        public TYHRKB24C2(string sPSSABUN, string sPSYDATE, string sPSGUBN, string sPSTYPE, string sPSJKCD)
        {
            InitializeComponent();

            this.SetPopupStyle();

            this.fsPSSABUN = sPSSABUN;
            this.fsPSYDATE = sPSYDATE;
            this.fsPSGUBN = sPSGUBN;
            this.fsPSTYPE = sPSTYPE;
            this.fsPSJKCD = sPSJKCD;
        }

        private void TYHRKB24C2_Load(object sender, System.EventArgs e)
        {
            this.RDBPRTCHECK1.Checked = true;
            this.RDBPRTCHECK2.Checked = false;

        }
        #endregion

        #region Description :  출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            //퇴직금내역서
            if (RDBPRTCHECK1.Checked)
            {
                this.UP_RETIREDetail_PRT();
            }

            //퇴직소득영수증
            if (RDBPRTCHECK2.Checked)
            {
                this.UP_RETIREReceipt_PRT();
            }


        }
        #endregion

        #region Description : 퇴직금내역서 출력 이벤트
        private void UP_RETIREDetail_PRT()
        {

            // 퇴직금 산출 마스타 조회
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_HR_BBUHJ839",
                fsPSSABUN,
                fsPSYDATE,
                fsPSGUBN
                );

            dt = this.DbConnector.ExecuteDataTable();

            // 퇴직금 급여 내역 조회
            DataTable dtM = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_HR_BC1B3847",
                fsPSSABUN,
                fsPSYDATE,
                fsPSGUBN,
                "M", "", "", ""
                );

            dtM = this.DbConnector.ExecuteDataTable();

            // 퇴직금 상여 내역 조회
            DataTable dtS = new DataTable();

            this.DbConnector.CommandClear();

            if (fsPSTYPE != "DC")
            {
                this.DbConnector.Attach
                    (
                    "TY_P_HR_BC1B3847",
                    fsPSSABUN,
                    fsPSYDATE,
                    fsPSGUBN,
                    "S", "H", "", ""
                    );
            }
            else
            {
                this.DbConnector.Attach
                (
                "TY_P_HR_BC1B3847",
                fsPSSABUN,
                fsPSYDATE,
                fsPSGUBN,
                "S", "H", "M", "Y"
                );
            }

            dtS = this.DbConnector.ExecuteDataTable();

            // 퇴직금 연차 내역 조회
            DataTable dtY = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_HR_BC1B3847",
                fsPSSABUN,
                fsPSYDATE,
                fsPSGUBN,
                "Y", "", "", ""
                );

            dtY = this.DbConnector.ExecuteDataTable();

            //배수별 퇴직금 산출내역 
            DataTable dtBeSu = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_HR_BC192844",
                fsPSSABUN,
                fsPSYDATE,
                fsPSGUBN
                );

            dtBeSu = this.DbConnector.ExecuteDataTable();

            if (fsPSJKCD != "01")
            {
                if (fsPSTYPE != "DC")
                {
                    SectionReport rpt = new TYHRKB017R(dtM, dtS, dtY, 1);
                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
                else
                {
                    SectionReport rpt = new TYHRKB017R1(dtM, dtS, dtY, 1);
                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
            }
            else
            {
                if (fsPSTYPE != "DC")
                {
                    SectionReport rpt = new TYHRKB017R2(dtM, dtS, dtY, dtBeSu);
                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
            }
        }
        #endregion

        #region Description : 퇴직금영수증 출력 이벤트
        private void UP_RETIREReceipt_PRT()
        {
            // 퇴직금 산출 마스타 조회
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_HR_BC7GD888",
                fsPSSABUN,
                fsPSYDATE,
                fsPSGUBN,
                TYUserInfo.SecureKey,
                "N"
                );

            dt = this.DbConnector.ExecuteDataTable();

            SectionReport rpt = new TYHRKB017R3(dt);
            (new TYERGB001P(rpt, dt)).ShowDialog();

        }
        #endregion

        #region Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion






    }
}
