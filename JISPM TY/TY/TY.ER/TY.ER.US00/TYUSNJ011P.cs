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
    /// 항운노조 노임명세서 출력 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.06.11 13:13
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_96BDE771 : 항운노조 노임명세서 출력(항차)
    ///  TY_P_US_96BG1777 : 항운노조 노임명세서 출력(개인)
    ///  TY_P_US_96BG4778 : 항운노조 연안노임명세서 출력(항차)
    ///  TY_P_US_96BGC780 : 항운노조 연안노임명세서 출력(개인)
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  HIHANGCHA : 항　　차
    ///  INQOPTION : 조회구분
    ///  PAYSOKP : 소급포함유무
    ///  HIJYYYMM : 적용년월
    /// </summary>
    public partial class TYUSNJ011P : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYUSNJ011P()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }

        private void TYUSNJ011P_Load(object sender, System.EventArgs e)
        {
            this.BTN61_PRT.ProcessCheck += new TButton.CheckHandler(BTN61_PRT_ProcessCheck);

            RDB01_CHK01.Checked = true;
            RDB01_CHK02.Checked = false;

            this.DTP01_HIJYYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            this.DTP02_HIJYYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.SetStartingFocus(CBH01_HIHANGCHA.CodeText);
        }
        #endregion

        #region  Description : 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sProcId = string.Empty;

            this.DbConnector.CommandClear();
            sProcId = RDB01_CHK01.Checked == true ? "TY_P_US_96HDK866" : "TY_P_US_96HDV868";
            this.DbConnector.Attach
                (
                    sProcId,
                    this.CBH01_HIHANGCHA.GetValue().ToString(),
                    this.CBH02_HIHANGCHA.GetValue().ToString(),
                    this.DTP01_HIJYYYMM.GetString().ToString().Substring(0, 6),
                    this.DTP02_HIJYYYMM.GetString().ToString().Substring(0, 6),
                    CKB01_PAYSOKP.Checked == true ? "1" : ""
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYUSNJ011R1(RDB01_CHK01.Checked == true ? "" : "1");
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
        }

        private void BTN61_PRT_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_GB_2BN4U622"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : CBO01_INQOPTION_SelectedIndexChanged 이벤트
        private void CBO01_INQOPTION_SelectedIndexChanged(object sender, EventArgs e)
        {

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
