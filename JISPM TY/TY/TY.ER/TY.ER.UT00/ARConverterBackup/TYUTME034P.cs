using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using System.IO;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Export.Html;
using DataDynamics.ActiveReports.Export.Pdf;

namespace TY.ER.UT00
{
    /// <summary>
    /// 화물료 거래명세서 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.09.01 13:27
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_791DS515 : 화물료 거래명세서 출력
    ///  TY_P_UT_791DS516 : 화물료 거래명세서 출력(화주x)
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
    ///  CHHWAMUL : 화물
    ///  SVIPHANG : 입항일자
    ///  UTDATE : 매출일자
    /// </summary>
    public partial class TYUTME034P : TYBase
    {
        private string fsFileDownPath = string.Empty;

        #region Description : 폼 로드
        public TYUTME034P()
        {
            InitializeComponent();
        }

        private void TYUTME034P_Load(object sender, System.EventArgs e)
        {
            this.DTP01_UTDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_UTDATE);
        }
        #endregion

        #region Description : 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sDATE  = string.Empty;
            string sSDATE = string.Empty;
            string sEDATE = string.Empty;

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_7BHH6048", this.DTP01_UTDATE.GetString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sSDATE = dt.Rows[0]["MINDATE"].ToString();
                sEDATE = dt.Rows[0]["MAXDATE"].ToString();
            }

            if (sSDATE.Length == 8)
            {
                sSDATE = sSDATE.Substring(0, 4).ToString() + "-" + sSDATE.Substring(4, 2).ToString() + "-" + sSDATE.Substring(6, 2).ToString();
            }

            if (sEDATE.Length == 8)
            {
                sEDATE = sEDATE.Substring(0, 4).ToString() + "-" + sEDATE.Substring(4, 2).ToString() + "-" + sEDATE.Substring(6, 2).ToString();
            }


            sDATE = "(" + sSDATE.ToString() + " ~ " + sEDATE.ToString() + ")";



            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_7BHH1047", sDATE.ToString(),
                                                        Get_Date(this.DTP01_UTDATE.GetString()).Substring(4,2).ToString(),
                                                        this.DTP01_UTDATE.GetString(),
                                                        this.CBH01_CHHWAJU.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                ActiveReport rpt = new TYUTME034R();
                
                rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Default;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion
    }
}
