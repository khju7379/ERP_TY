using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using DataDynamics.ActiveReports;

namespace TY.ER.HR00
{
    /// <summary>
    /// 년차 지급명세서 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2015.10.02 15:37
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  YYSABUN : 사　　번
    ///  YYDATE : 지급년월
    /// </summary>
    public partial class TYHRYB001P : TYBase
    {
        private String fsYYDATE = string.Empty;
        private String fsYYSABUN = string.Empty;

        #region Description : 폼 로드 이벤트
        public TYHRYB001P(string sYYDATE, string sYYSABUN)
        {
            InitializeComponent();

            fsYYDATE = sYYDATE;
            fsYYSABUN = sYYSABUN;
        }

        private void TYHRYB001P_Load(object sender, System.EventArgs e)
        {
            this.DTP01_YYDATE.SetValue(fsYYDATE);
            this.CBH01_YYSABUN.SetValue(fsYYSABUN);
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.Attach
                (
                "TY_P_HR_5A2EH958",
                this.CBO01_INQOPTION.GetValue().ToString(),
                this.DTP01_YYDATE.GetValue().ToString(),
                this.CBH01_YYSABUN.GetValue().ToString()                
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 1)
            {
                ActiveReport rpt = new TYHRYB001R();

                // 가로 출력
                rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }           

        }
        #endregion

    }
}
