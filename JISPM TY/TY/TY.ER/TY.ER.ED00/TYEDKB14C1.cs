using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.ED00
{
    /// <summary>
    /// 내국화물 반입재고 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2020.04.16 16:24
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_A4HAR281 : 내국반입보고서 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_A4HAS283 : 내국화물반입보고서 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  EDHHWAJU : 화주
    ///  EDHHWAMUL : 화물
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYEDKB14C1 : TYBase
    {
        public string fsEDIGJ;
        public string fsCSIPHANG;
        public string fsCSBONSUN;
        public string fsCSHWAJU;
        public string fsCSHWAMUL;
        public string fsCSHWAMULNM;
        public string fsVSJUKHA;        
        public string fsCSMSNSEQ;
        public string fsCSHSNSEQ;
        public string fsCSSINNO;
        public string fsCSIPQTY;
        public string fsCSBLNO;

        public string fsIPSINOYY;
        public string fsIPSINO;

        #region  Description : 폼 로드 이벤트
        public TYEDKB14C1()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }

        private void TYEDKB14C1_Load(object sender, System.EventArgs e)
        {
            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.UP_SetLockCheck();

            this.SetStartingFocus(DTP01_SDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_A4HAS283.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_A4HAR281", this.CBO01_EDIGJ.GetValue(), DTP01_SDATE.GetString(), DTP01_EDATE.GetString(), CBH01_EDHHWAJU.GetValue(), CBH01_EDHHWAMUL.GetValue() );
            this.FPS91_TY_S_UT_A4HAS283.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_UT_A4HAS283.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_UT_A4HAS283.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_UT_A4HAS283.GetValue(i, "EDIRCVGB").ToString() == "Y")
                    {
                        this.FPS91_TY_S_UT_A4HAS283_Sheet1.Rows[i].ForeColor = Color.Blue;
                    }
                    else if (this.FPS91_TY_S_UT_A4HAS283.GetValue(i, "EDIRCVGB").ToString() == "E")
                    {
                        this.FPS91_TY_S_UT_A4HAS283_Sheet1.Rows[i].ForeColor = Color.Red;
                    }
                }
            }
        }
        #endregion        

        #region  Description : FPS91_TY_S_UT_A4HAS283_CellDoubleClick 버튼 이벤트
        private void FPS91_TY_S_UT_A4HAS283_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            fsEDIGJ = this.FPS91_TY_S_UT_A4HAS283.GetValue("EDIGJ").ToString();
            fsCSIPHANG = this.FPS91_TY_S_UT_A4HAS283.GetValue("EDIIPHANG").ToString();
            fsCSBONSUN = this.FPS91_TY_S_UT_A4HAS283.GetValue("EDIBONSUN").ToString();
            fsCSHWAJU = this.FPS91_TY_S_UT_A4HAS283.GetValue("EDIHWAJU").ToString();
            fsCSHWAMUL = this.FPS91_TY_S_UT_A4HAS283.GetValue("EDIHWAMUL").ToString();
            fsCSHWAMULNM = this.FPS91_TY_S_UT_A4HAS283.GetValue("EDIHWAMULNM").ToString();
            fsVSJUKHA = this.FPS91_TY_S_UT_A4HAS283.GetValue("EDIJUKHA").ToString();            
            fsCSMSNSEQ = this.FPS91_TY_S_UT_A4HAS283.GetValue("EDIBLMSN").ToString();
            fsCSHSNSEQ = this.FPS91_TY_S_UT_A4HAS283.GetValue("EDIBLHSN").ToString();
            fsCSSINNO = this.FPS91_TY_S_UT_A4HAS283.GetValue("EDISINGONUM").ToString();
            fsCSBLNO = this.FPS91_TY_S_UT_A4HAS283.GetValue("EDIBLNO").ToString();

            fsCSIPQTY = this.FPS91_TY_S_UT_A4HAS283.GetValue("JEGOQTY").ToString();

            fsIPSINOYY = this.FPS91_TY_S_UT_A4HAS283.GetValue("EDIHMNO1").ToString();
            fsIPSINO = this.FPS91_TY_S_UT_A4HAS283.GetValue("EDIHMNO2").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region  Description : Lock Check
        private void UP_SetLockCheck()
        {
            if (TYUserInfo.DeptCode.Substring(0, 1) == "S")
            {
                CBO01_EDIGJ.SetValue("S");
            }
            else
            {
                CBO01_EDIGJ.SetValue("T");
            }

            if (TYUserInfo.DeptCode.Substring(0, 6) != "A10300")
            {
                CBO01_EDIGJ.SetReadOnly(true);
            }
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

    }
}
