using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using DataDynamics.ActiveReports;

namespace TY.ER.UT00
{
    /// <summary>
    /// 출고일,일련번호(협회용) 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.09.19 16:22
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_66FD4200 : 대표 거래처 코드 조회
    ///  TY_P_UT_79JGJ620 : 출고일,일련번호(협회용) 조회
    ///  TY_P_UT_79JGK621 : 출고일,일련번호(협회용) 조회 (화주X)
    ///  TY_P_UT_79JGM622 : 출고일,일련번호(협회용) 출력
    ///  TY_P_UT_79JGM623 : 출고일,일련번호(협회용) 출력 (화주X)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_79JGN624 : 출고일,일련번호(협회용) 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  CHHWAJU : 화주
    ///  CHHWAMUL : 화물
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYUTME006S : TYBase
    {
        #region Description : 폼 로드
        public TYUTME006S()
        {
            InitializeComponent();
        }

        private void TYUTME006S_Load(object sender, System.EventArgs e)
        {
            string sSTDATE = string.Empty;
            string sEDDATE = string.Empty;

            sSTDATE = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            sEDDATE = DateTime.Now.ToString("yyyy-MM-dd");

            this.DTP01_STDATE.SetValue(sSTDATE.Substring(0, 8) + "26");
            this.DTP01_EDDATE.SetValue(sEDDATE.Substring(0, 8) + "25");

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sHWAJU = string.Empty;

            // 대표거래처 코드 가져오기
            sHWAJU = Get_VNCODE(this.CBH01_CHHWAJU.GetValue().ToString());

            if (Convert.ToDouble(this.DTP01_STDATE.GetString()) > Convert.ToDouble(this.DTP01_EDDATE.GetString()))
            {
                this.ShowCustomMessage("시작일자가 종료일자보다 클수 없습니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            else
            {
                DataTable dt = new DataTable();

                this.FPS91_TY_S_UT_79JGN624.Initialize();

                #region Description : 반출 보고서 조회

                this.DbConnector.CommandClear();

                if (sHWAJU != "")
                {   
                    this.DbConnector.Attach(
                        "TY_P_UT_79JGJ620",
                        this.DTP01_STDATE.GetString(),
                        this.DTP01_EDDATE.GetString(),
                        sHWAJU,
                        this.CBH01_CHHWAMUL.GetValue().ToString()
                        );
                }
                else
                {
                    this.DbConnector.Attach(
                        "TY_P_UT_79JGK621",
                        this.DTP01_STDATE.GetString(),
                        this.DTP01_EDDATE.GetString(),
                        this.CBH01_CHHWAMUL.GetValue().ToString()
                        );
                }
                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_UT_79JGN624.SetValue(dt);

                #endregion




                #region Description : 반입 보고서 조회

                this.DbConnector.CommandClear();

                if (sHWAJU != "")
                {
                    this.DbConnector.Attach(
                        "TY_P_UT_79KG4636",
                        this.DTP01_STDATE.GetString(),
                        this.DTP01_EDDATE.GetString(),
                        this.DTP01_STDATE.GetString(),
                        this.DTP01_EDDATE.GetString(),
                        sHWAJU,
                        this.CBH01_CHHWAMUL.GetValue().ToString()
                        );
                }
                else
                {
                    this.DbConnector.Attach(
                        "TY_P_UT_79KG4637",
                        this.DTP01_STDATE.GetString(),
                        this.DTP01_EDDATE.GetString(),
                        this.DTP01_STDATE.GetString(),
                        this.DTP01_EDDATE.GetString(),
                        this.CBH01_CHHWAMUL.GetValue().ToString()
                        );
                }
                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_UT_79KH9638.SetValue(dt);

                #endregion

            }
        }
        #endregion

        #region Description : 반출 보고서 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sHWAJU = string.Empty;

            // 대표거래처 코드 가져오기
            sHWAJU = Get_VNCODE(this.CBH01_CHHWAJU.GetValue().ToString());

            if (Convert.ToDouble(this.DTP01_STDATE.GetString()) > Convert.ToDouble(this.DTP01_EDDATE.GetString()))
            {
                this.ShowCustomMessage("시작일자가 종료일자보다 클수 없습니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            else
            {
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();

                if (sHWAJU != "")
                {
                    this.DbConnector.Attach(
                        "TY_P_UT_79JGM622",
                        this.DTP01_STDATE.GetString(),
                        this.DTP01_EDDATE.GetString(),
                        sHWAJU,
                        this.CBH01_CHHWAMUL.GetValue().ToString()
                        );
                }
                else
                {
                    this.DbConnector.Attach(
                        "TY_P_UT_79JGM623",
                        this.DTP01_STDATE.GetString(),
                        this.DTP01_EDDATE.GetString(),
                        this.CBH01_CHHWAMUL.GetValue().ToString()
                        );
                }

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    ActiveReport rpt = new TYUTME006R();

                    rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
                else
                {
                    this.ShowMessage("TY_M_AC_2422N250");
                }
            }
        }
        #endregion

        #region Description : 반입 보고서 출력 버튼 이벤트
        private void BTN62_PRT_Click(object sender, EventArgs e)
        {
            string sHWAJU = string.Empty;

            // 대표거래처 코드 가져오기
            sHWAJU = Get_VNCODE(this.CBH01_CHHWAJU.GetValue().ToString());

            if (Convert.ToDouble(this.DTP01_STDATE.GetString()) > Convert.ToDouble(this.DTP01_EDDATE.GetString()))
            {
                this.ShowCustomMessage("시작일자가 종료일자보다 클수 없습니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            else
            {
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();

                if (sHWAJU != "")
                {
                    this.DbConnector.Attach(
                        "TY_P_UT_79KG4636",
                        this.DTP01_STDATE.GetString(),
                        this.DTP01_EDDATE.GetString(),
                        this.DTP01_STDATE.GetString(),
                        this.DTP01_EDDATE.GetString(),
                        sHWAJU,
                        this.CBH01_CHHWAMUL.GetValue().ToString()
                        );
                }
                else
                {
                    this.DbConnector.Attach(
                        "TY_P_UT_79KG4637",
                        this.DTP01_STDATE.GetString(),
                        this.DTP01_EDDATE.GetString(),
                        this.DTP01_STDATE.GetString(),
                        this.DTP01_EDDATE.GetString(),
                        this.CBH01_CHHWAMUL.GetValue().ToString()
                        );
                }

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    ActiveReport rpt = new TYUTME007R();

                    rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
                else
                {
                    this.ShowMessage("TY_M_AC_2422N250");
                }
            }
        }
        #endregion
    }
}
