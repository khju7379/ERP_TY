using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.UT00
{
    /// <summary>
    /// 내국물품 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.05.16 19:22
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_75GJA527 : 내국 화물 조회
    ///  TY_P_UT_75GJB528 : 내국 화물 출력
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_75GJC530 : 내국 화물 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYUTPR018S : TYBase
    {
        #region Description : 폼 로드
        public TYUTPR018S()
        {
            InitializeComponent();
        }

        private void TYUTPR018S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_75GJC530.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75GJA527", this.DTP01_STDATE.GetString(),
                                                        this.DTP01_EDDATE.GetString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_75GJC530.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                this.TXT01_IPMTQTY.Text = string.Format("{0:#,##0.000}", double.Parse(dt.Compute("Sum(IPMTQTY)", null).ToString()));
            }
            else
            {
                this.TXT01_IPMTQTY.Text = "0.000";
            }

        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sSTDATE = this.DTP01_STDATE.GetString();
            string sEDDATE = this.DTP01_EDDATE.GetString();
            string sDATE = "(" + sSTDATE.Substring(0, 4) + "/" + sSTDATE.Substring(4, 2) + "/" + sSTDATE.Substring(6, 2) + "-" + sEDDATE.Substring(0, 4) + "/" + sEDDATE.Substring(4, 2) + "/" + sEDDATE.Substring(6, 2) + ")";

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_UT_75GJB528", sDATE,
                                                        sSTDATE,
                                                        sEDDATE
                                                        );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYUTPR018R();

                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Default;

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
