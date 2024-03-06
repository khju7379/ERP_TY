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
    public partial class TYUTME039P : TYBase
    {
        private string fsFileDownPath = string.Empty;

        #region Description : 폼 로드
        public TYUTME039P()
        {
            InitializeComponent();
        }

        private void TYUTME039P_Load(object sender, System.EventArgs e)
        {
            // 화물료 생성일자 가져오기
            UP_GET_DATE();

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sDATE  = string.Empty;
            string sSDATE = string.Empty;
            string sEDATE = string.Empty;

            DataTable dt = new DataTable();

            sSDATE = this.DTP01_STDATE.GetValue().ToString();
            sEDATE = this.DTP01_EDDATE.GetValue().ToString();

            sDATE = "(" + sSDATE.ToString() + " ~ " + sEDATE.ToString() + ")";

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_839FD683", sDATE.ToString(),
                                                        Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                                                        Get_Date(this.DTP01_EDDATE.GetValue().ToString())
                                                        );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                ActiveReport rpt = new TYUTME039R();
                
                rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Description : 화물료 생성일자 가져오기
        private void UP_GET_DATE()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_839FG684", Get_Date(System.DateTime.Now.ToString("yyyy-MM-dd").ToString()).Substring(0,6));

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["MINDATE"].ToString() != "")
                {
                    this.DTP01_STDATE.SetValue(dt.Rows[0]["MINDATE"].ToString());
                }
                else
                {
                    this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd").ToString());
                }

                if (dt.Rows[0]["MAXDATE"].ToString() != "")
                {
                    this.DTP01_EDDATE.SetValue(dt.Rows[0]["MAXDATE"].ToString());
                }
                else
                {
                    this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd").ToString());
                }
            }
            else
            {
                this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd").ToString());
                this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd").ToString());
            }
        }
        #endregion
    }
}
