using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 결산 전년대비 생성 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2014.06.16 15:11
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_45UFE621 : 전년대비분석_손익계산서 생성 SP
    ///  TY_P_AC_45UHG622 : 전년대비분석_재무상태표 생성 SP
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_26E2Z874 : 생성하시겠습니까?
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  GOKCR : 생성구분
    ///  BSCHK : 재무상태표
    ///  PLCHK : 손익계산서
    ///  S1CHK12 : 전체
    ///  GBEDDATE : 비교종료일자
    ///  GBSTDATE : 비교시작일자
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    /// </summary>
    public partial class TYACSE012B : TYBase
    {

        public TYACSE012B()
        {
            InitializeComponent();
        }

        #region Description : 페이지 로드
        private void TYACSE012B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_GBEDDATE.SetReadOnly(true);
            this.DTP01_GEDDATE.SetReadOnly(true);

            this.CBO01_GOKCR.Focus();
        } 
        #endregion

        #region Description : 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sOUTMSG = string.Empty;

            int iCCNT = 0;

            string s재무상태표 = this.CKB01_BSCHK.GetValue().ToString();
            string s손익계산서 = this.CKB01_PLCHK.GetValue().ToString();

            string s전체처리구분 = this.CBO01_GOKCR.GetValue().ToString();

            if (s재무상태표 == "Y") { iCCNT = iCCNT + 1; };
            if (s손익계산서 == "Y") { iCCNT = iCCNT + 1; };

            #region Description : 재무상태표 계정 생성 처리
            if (s재무상태표 == "Y")
            {
                string sBS_NOWDATE = string.Empty;
                string sBS_PREDATE = string.Empty;

                int iDD = 0;
                string sYear = string.Empty;
                string sMonth = string.Empty;

                // 재무상태표 기준년 시작일자 및 종료 일자 구하기
                sYear = this.DTP01_GBSTDATE.GetValue().ToString().Substring(0, 4);
                sMonth = this.DTP01_GBSTDATE.GetValue().ToString().Substring(4, 2);
                // 해당월 마지막 일자 가져오기
                iDD = DateTime.DaysInMonth(int.Parse(sYear.ToString()), int.Parse(sMonth.ToString()));
                sBS_NOWDATE = this.DTP01_GBSTDATE.GetValue().ToString() + Set_Fill2(Convert.ToString(iDD));

                sBS_PREDATE = Convert.ToString(int.Parse(this.DTP01_GBSTDATE.GetValue().ToString().Substring(0, 4)) - 1) + "1231";

                this.DTP01_GBEDDATE.SetValue(sBS_PREDATE.ToString()); // 화면에 강제 세팅

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_45UHG622",
                                        s전체처리구분, sBS_NOWDATE , sBS_PREDATE ,
                                        sOUTMSG.ToString()
                                        );

                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            }
            #endregion

            #region Description : 손익계정 생성 처리
            if (s손익계산서 == "Y")
            {
                string sPL_NOWSTDATE = string.Empty;
                string sPL_NOWEDDATE = string.Empty;

                string sPL_PRYYMM = string.Empty;
                string sPL_PRESTDATE = string.Empty;
                string sPL_PREEDDATE = string.Empty;
                string sPLDDP = string.Empty;

                int iDD = 0;
                string sYear = string.Empty;
                string sMonth = string.Empty;

                sPLDDP = "00    ";
                // 손익 기준년 시작일자 및 종료 일자 구하기
                sPL_NOWSTDATE = this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 4) + "0101";
                sYear = this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 4);
                sMonth = this.DTP01_GSTDATE.GetValue().ToString().Substring(4, 2);
                // 해당월 마지막 일자 가져오기
                iDD = DateTime.DaysInMonth(int.Parse(sYear.ToString()), int.Parse(sMonth.ToString()));
                sPL_NOWEDDATE = this.DTP01_GSTDATE.GetValue().ToString() + Set_Fill2(Convert.ToString(iDD));

                // 손익 대비년 시작일자 및 종료 일자 구하기
                iDD = 0;
                sYear = string.Empty;
                sMonth = string.Empty;

                sPL_PRESTDATE = Convert.ToString(int.Parse(this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 4)) - 1) + "0101";
                sPL_PRYYMM = Convert.ToString(int.Parse(this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 4)) - 1) + this.DTP01_GSTDATE.GetValue().ToString().Substring(4, 2);
                sYear = sPL_PRYYMM.Substring(0, 4);
                sMonth = sPL_PRYYMM.Substring(4, 2);

                // 해당월 마지막 일자 가져오기
                iDD = DateTime.DaysInMonth(int.Parse(sYear.ToString()), int.Parse(sMonth.ToString()));
                sPL_PREEDDATE = sPL_PRYYMM + Set_Fill2(Convert.ToString(iDD));

                this.DTP01_GEDDATE.SetValue(sPL_PREEDDATE.ToString()); // 화면에 강제 세팅

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_45UFE621",
                                        s전체처리구분, sPLDDP, sPL_NOWSTDATE, sPL_NOWEDDATE, sPL_PRESTDATE, sPL_PREEDDATE,
                                        sOUTMSG.ToString()
                                        );
                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            }
            #endregion

            if (iCCNT != 0)
            {
                this.ShowCustomMessage(sOUTMSG, "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            
        } 
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
        #endregion


        #region Description : 처리 CHECK
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int iCCNT = 0;

            string s재무상태표 = this.CKB01_BSCHK.GetValue().ToString();
            string s손익계산서 = this.CKB01_PLCHK.GetValue().ToString();

            if (s재무상태표 == "Y") { iCCNT = iCCNT + 1; };
            if (s손익계산서 == "Y") { iCCNT = iCCNT + 1; };

            if (iCCNT == 0)
            {
                this.ShowCustomMessage("선택 자료가 없습니다.", "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                e.Successed = false;
                return;
            }

        }
        #endregion

        private void DTP01_GBSTDATE_ValueChanged(object sender, EventArgs e)
        {
            string sBS_NOWDATE = string.Empty;
            string sBS_PREDATE = string.Empty;

            int iDD = 0;
            string sYear = string.Empty;
            string sMonth = string.Empty;

            // 재무상태표 기준년 시작일자 및 종료 일자 구하기
            sYear = this.DTP01_GBSTDATE.GetValue().ToString().Substring(0, 4);
            sMonth = this.DTP01_GBSTDATE.GetValue().ToString().Substring(4, 2);
            // 해당월 마지막 일자 가져오기
            iDD = DateTime.DaysInMonth(int.Parse(sYear.ToString()), int.Parse(sMonth.ToString()));
            sBS_NOWDATE = this.DTP01_GBSTDATE.GetValue().ToString() + Set_Fill2(Convert.ToString(iDD));

            sBS_PREDATE = Convert.ToString(int.Parse(this.DTP01_GBSTDATE.GetValue().ToString().Substring(0, 4)) - 1) + "1231";

            this.DTP01_GBEDDATE.SetValue(sBS_PREDATE.ToString()); // 화면에 강제 세팅
        }


        private void DTP01_GSTDATE_ValueChanged(object sender, EventArgs e)
        {
            string sPL_NOWSTDATE = string.Empty;
            string sPL_NOWEDDATE = string.Empty;

            string sPL_PRYYMM = string.Empty;
            string sPL_PRESTDATE = string.Empty;
            string sPL_PREEDDATE = string.Empty;
            string sPLDDP = string.Empty;

            int iDD = 0;
            string sYear = string.Empty;
            string sMonth = string.Empty;

            sPLDDP = "00    ";
            // 손익 기준년 시작일자 및 종료 일자 구하기
            sPL_NOWSTDATE = this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 4) + "0101";
            sYear = this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 4);
            sMonth = this.DTP01_GSTDATE.GetValue().ToString().Substring(4, 2);
            // 해당월 마지막 일자 가져오기
            iDD = DateTime.DaysInMonth(int.Parse(sYear.ToString()), int.Parse(sMonth.ToString()));
            sPL_NOWEDDATE = this.DTP01_GSTDATE.GetValue().ToString() + Set_Fill2(Convert.ToString(iDD));

            // 손익 대비년 시작일자 및 종료 일자 구하기
            iDD = 0;
            sYear = string.Empty;
            sMonth = string.Empty;

            sPL_PRESTDATE = Convert.ToString(int.Parse(this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 4)) - 1) + "0101";
            sPL_PRYYMM = Convert.ToString(int.Parse(this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 4)) - 1) + this.DTP01_GSTDATE.GetValue().ToString().Substring(4, 2);
            sYear = sPL_PRYYMM.Substring(0, 4);
            sMonth = sPL_PRYYMM.Substring(4, 2);

            // 해당월 마지막 일자 가져오기
            iDD = DateTime.DaysInMonth(int.Parse(sYear.ToString()), int.Parse(sMonth.ToString()));
            sPL_PREEDDATE = sPL_PRYYMM + Set_Fill2(Convert.ToString(iDD));

            this.DTP01_GEDDATE.SetValue(sPL_PREEDDATE.ToString()); // 화면에 강제 세팅

        }
    }
}
