using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.ER.GB00;
using GrapeCity.ActiveReports;


namespace TY.ER.US00
{
    /// <summary>
    /// 항운노조 각종 출력양식 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.04.15 13:33
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_94FEP372 : 항운노조 교육훈련비 납입내역서
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_2BN4U622 : 출력하시겠습니까?
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  IHHANGCHA : 항차
    ///  INQOPTION : 조회구분
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYUSNJ018P : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYUSNJ018P()
        {
            InitializeComponent();
        }

        private void TYUSNJ018S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_PRT.ProcessCheck += new TButton.CheckHandler(BTN61_PRT_ProcessCheck);

            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.CBH01_IHHANGCHA.Visible = false;
            this.CBH02_IHHANGCHA.Visible = false;

            this.SetStartingFocus(CBO01_INQOPTION);
        }
        #endregion

        #region  Description : 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            //교육훈련비납입내역서
            if (CBO01_INQOPTION.GetValue().ToString() == "1")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                        "TY_P_US_94FEP372",
                        this.DTP01_SDATE.GetString().ToString().Substring(0, 6),
                        this.DTP01_EDATE.GetString().ToString().Substring(0, 6)
                    );
                DataTable dt = this.DbConnector.ExecuteDataTable();
                SectionReport rpt = new TYUSNJ018R1();
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                (new TYERGB001P(rpt, dt)).ShowDialog();
            }

            //항만하역실태보고서
            if (CBO01_INQOPTION.GetValue().ToString() == "2")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                        "TY_P_US_94GH7379",
                        this.DTP01_SDATE.GetString().ToString().Substring(0, 6),
                        this.DTP01_EDATE.GetString().ToString().Substring(0, 6),
                        this.CBH01_IHHANGCHA.GetValue().ToString(),
                        this.CBH02_IHHANGCHA.GetValue().ToString()
                    );
                DataTable dt = this.DbConnector.ExecuteDataTable();
                SectionReport rpt = new TYUSNJ018R2();
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                (new TYERGB001P(rpt, dt)).ShowDialog();
            }

            //항만현대화기금납부실적서
            if (CBO01_INQOPTION.GetValue().ToString() == "3")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                        "TY_P_US_94IF4394",
                        this.DTP01_SDATE.GetString().ToString().Substring(0, 4) + "01",
                        Get_Date(this.DTP01_SDATE.GetValue().ToString())
                    );
                DataTable dt = this.DbConnector.ExecuteDataTable();
                SectionReport rpt = new TYUSNJ018R3();
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                (new TYERGB001P(rpt, dt)).ShowDialog();
            }

            //업체별/품목별 항만하역실적
            if (CBO01_INQOPTION.GetValue().ToString() == "4")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                        "TY_P_US_94HDW382",
                        this.DTP01_SDATE.GetString().ToString().Substring(0, 6)
                    );
                DataTable dt = this.DbConnector.ExecuteDataTable();
                SectionReport rpt = new TYUSNJ018R4();
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                (new TYERGB001P(rpt, dt)).ShowDialog();
            }

            //육운분야 퇴직충당금납부실적
            if (CBO01_INQOPTION.GetValue().ToString() == "5")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                        "TY_P_US_94HHA388",
                        this.DTP01_SDATE.GetString().ToString().Substring(0, 6),
                        this.DTP01_EDATE.GetString().ToString().Substring(0, 6)
                    );
                DataTable dt = this.DbConnector.ExecuteDataTable();
                SectionReport rpt = new TYUSNJ018R5();
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                (new TYERGB001P(rpt, dt)).ShowDialog();
            }

            //육운분야 퇴직충당금납부실적(소급)
            if (CBO01_INQOPTION.GetValue().ToString() == "6")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                        "TY_P_US_94IDL392",
                        this.DTP01_SDATE.GetString().ToString().Substring(0, 6),
                        this.DTP01_EDATE.GetString().ToString().Substring(0, 6),
                        this.CBH01_IHHANGCHA.GetValue().ToString(),
                        this.CBH02_IHHANGCHA.GetValue().ToString()
                    );
                DataTable dt = this.DbConnector.ExecuteDataTable();
                SectionReport rpt = new TYUSNJ018R5();
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                (new TYERGB001P(rpt, dt)).ShowDialog();
            }


        }

        private void BTN61_PRT_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (CBO01_INQOPTION.GetValue().ToString() == "2" || CBO01_INQOPTION.GetValue().ToString() == "6")
            {
                if (this.CBH01_IHHANGCHA.GetValue().ToString() == "")
                {
                    this.SetFocus(this.CBH01_IHHANGCHA.CodeText);
                    this.ShowCustomMessage("항차를 입력하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
                if (this.CBH02_IHHANGCHA.GetValue().ToString() == "")
                {
                    this.SetFocus(this.CBH02_IHHANGCHA.CodeText);
                    this.ShowCustomMessage("항차를 입력하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }
        }
        #endregion

        #region  Description : CBO01_INQOPTION_SelectedIndexChanged 이벤트
        private void CBO01_INQOPTION_SelectedIndexChanged(object sender, EventArgs e)
        {
            //교육훈련비납입내역서
            if (CBO01_INQOPTION.GetValue().ToString() == "1")
            {
                this.CBH01_IHHANGCHA.Visible = false;
                this.CBH02_IHHANGCHA.Visible = false;

                this.DTP01_SDATE.Visible = true;
                this.DTP01_EDATE.Visible = true;

            }

            //항만하역실태보고서
            if (CBO01_INQOPTION.GetValue().ToString() == "2")
            {
                this.CBH01_IHHANGCHA.Visible = true;
                this.CBH02_IHHANGCHA.Visible = true;

                this.DTP01_SDATE.Visible = true;
                this.DTP01_EDATE.Visible = true;
            }

            //항만현대화기금납부실적서
            if (CBO01_INQOPTION.GetValue().ToString() == "3")
            {
                this.CBH01_IHHANGCHA.Visible = false;
                this.CBH02_IHHANGCHA.Visible = false;

                this.DTP01_SDATE.Visible = true;
                this.DTP01_EDATE.Visible = false;
            }

            //업체별/품목별 항만하역실적
            if (CBO01_INQOPTION.GetValue().ToString() == "4")
            {
                this.CBH01_IHHANGCHA.Visible = false;
                this.CBH02_IHHANGCHA.Visible = false;

                this.DTP01_SDATE.Visible = true;
                this.DTP01_EDATE.Visible = false;
            }

            //육운분야 퇴직충당금납부실적
            if (CBO01_INQOPTION.GetValue().ToString() == "5")
            {
                this.CBH01_IHHANGCHA.Visible = false;
                this.CBH02_IHHANGCHA.Visible = false;

                this.DTP01_SDATE.Visible = true;
                this.DTP01_EDATE.Visible = true;
            }

            //육운분야 퇴직충당금납부실적(소급)
            if (CBO01_INQOPTION.GetValue().ToString() == "6")
            {
                this.CBH01_IHHANGCHA.Visible = true;
                this.CBH02_IHHANGCHA.Visible = true;

                this.DTP01_SDATE.Visible = true;
                this.DTP01_EDATE.Visible = true;
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
