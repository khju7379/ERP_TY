using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using System.IO;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Export.Html;
using GrapeCity.ActiveReports.Export.Pdf;

namespace TY.ER.UT00
{
    /// <summary>
    /// 접안료 거래명세서 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.06.20 09:25
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_76K9J886 : 접안료 거래명세서 출력
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  CHHWAJU : 화주
    ///  IPHANG : 접안일자
    ///  MAECHIL : 매출일자
    ///  TOSEQ : 순번
    /// </summary>
    public partial class TYUTME041P : TYBase
    {
        private string fsFileDownPath = string.Empty;

        #region Description : 폼 로드
        public TYUTME041P()
        {
            InitializeComponent();
        }

        private void TYUTME041P_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM"));

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_B9SGH583", Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                                                            Get_Date(this.DTP01_EDDATE.GetValue().ToString())
                                                            );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    SectionReport rpt = new TYUTME013R();
                    // 가로 출력
                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait;
                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
                else
                {
                    this.ShowMessage("TY_M_AC_2422N250");
                }

            }
            catch (Exception ex)
            {
                string str = ex.Message;
                string aa = str;
            }
        }
        #endregion
    }
}
