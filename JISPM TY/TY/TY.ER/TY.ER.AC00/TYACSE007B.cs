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
    /// 결산 재무상태표 부속명세 생성 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2014.06.12 15:11
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_45EDD399 : 결산관리_재무상태표 현금성자산생성(SP)
    ///  TY_P_AC_45FF6407 : 결산관리_재무상태표 비 현금성자산생성(SP)
    ///  TY_P_AC_47AHH012 : 결산환율 CHECK  확인
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
    ///  GBPRYYMM : 처리년월
    /// </summary>
    public partial class TYACSE007B : TYBase
    {
        public TYACSE007B()
        {
            InitializeComponent();
        }

        #region Description : 페이지 로드
        private void TYACSE007B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_GBPRYYMM.Focus();
        } 
        #endregion

        #region Description : 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sOUTMSG = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_45EDD399", this.DTP01_GBPRYYMM.GetValue(), "0", sOUTMSG.ToString());

            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUTMSG.Substring(0, 2) == "OK")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_45FF6407", this.DTP01_GBPRYYMM.GetValue(), "0", sOUTMSG.ToString());

                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
            }

            this.ShowCustomMessage(sOUTMSG, "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

            
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
            string sYYMMDD = string.Empty;
            string sOUTMSG = string.Empty;

            int iDD = 0;
            // 해당월 마지막 일자 가져오기
            iDD = DateTime.DaysInMonth(int.Parse(this.DTP01_GBPRYYMM.GetValue().ToString().Substring(0, 4)), int.Parse(this.DTP01_GBPRYYMM.GetValue().ToString().Substring(4, 2)));
            // 기준년월
            sYYMMDD = this.DTP01_GBPRYYMM.GetValue().ToString() + Set_Fill2(Convert.ToString(iDD));

            // 환율등록 CHECK (해당월의 마지막 날짜 등록 체크)
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_47AHH012", sYYMMDD);
            DataTable dt1 = this.DbConnector.ExecuteDataTable();
            if (dt1.Rows.Count == 0)
            {
                sOUTMSG = "[ " + sYYMMDD.Substring(0, 4) + " 년" + sYYMMDD.Substring(4, 2) + " 월" + sYYMMDD.Substring(6, 2) + " 일 ] 환율 미등록됨! 해당년월의 마지막 일로 등록";
                this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }


        }
        #endregion
    }
}
