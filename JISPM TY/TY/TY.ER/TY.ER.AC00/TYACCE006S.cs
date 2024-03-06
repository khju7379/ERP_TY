using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TY.Service.Library;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.AC00
{
    /// <summary>
    /// 신용카드 지출명세조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.05.03 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2531W984 : 신용카드 지출명세서 조회 - 신용카드 지출명세서
    ///  TY_P_AC_2532X990 : 신용카드 지출명세서 조회 - 신용카드 지출집계표
    ///  TY_P_AC_2532Y991 : 신용카드 지출명세서 조회 - 신용카드 지출집계표(전체)
    ///  TY_P_AC_2535E999 : 신용카드 지출명세서 출력 - 신용카드 지출명세서
    ///  TY_P_AC_2535E001 : 신용카드 지출명세서 출력 - 신용카드 지출집계표
    ///  TY_P_AC_2535G002 : 신용카드 지출명세서 출력 - 신용카드 지출집계표(전체)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_25324985 : 신용카드 지출명세서 조회 - 신용카드 지출명세서
    ///  TY_S_AC_2532Z992 : 신용카드 지출명세서 조회 - 신용카드 지출집계표
    ///  TY_S_AC_2533B997 : 신용카드 지출명세서 조회 - 신용카드 지출집계표(전체)
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  GDEID : 접대비구분
    ///  INQOPTION : 조회구분
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACCE006S : TYBase
    {
        #region Description : 페이지 로드
        public TYACCE006S()
        {
            InitializeComponent();
        }

        private void TYACCE006S_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.DTP01_GSTYYMM);

            this.FPS91_TY_S_AC_25324985.Visible = true;
            this.FPS91_TY_S_AC_2532Z992.Visible = false;
            this.FPS91_TY_S_AC_2533B997.Visible = false;
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sGDEID = string.Empty;
            sGDEID = this.CBO01_GDEID.GetValue().ToString();

            if (this.CBO01_GDEID.GetValue().ToString() == "")
            {
                sGDEID = "'11,12,21,22'";
            }

            this.DbConnector.CommandClear();

            this.FPS91_TY_S_AC_25324985.Initialize();
            this.FPS91_TY_S_AC_2532Z992.Initialize();
            this.FPS91_TY_S_AC_2533B997.Initialize();

            if (this.CBO01_INQOPTION.GetValue().ToString() == "1")
            {
                this.FPS91_TY_S_AC_25324985.Visible = true;
                this.FPS91_TY_S_AC_2532Z992.Visible = false;
                this.FPS91_TY_S_AC_2533B997.Visible = false;

                this.DbConnector.Attach
                    (
                    "TY_P_AC_2531W984",
                    this.DTP01_GSTYYMM.GetValue(),
                    this.DTP01_GEDYYMM.GetValue(),
                    sGDEID.ToString()
                    );

                this.FPS91_TY_S_AC_25324985.SetValue(this.DbConnector.ExecuteDataTable());
            }
            else if (this.CBO01_INQOPTION.GetValue().ToString() == "2")
            {
                this.FPS91_TY_S_AC_25324985.Visible = false;
                this.FPS91_TY_S_AC_2532Z992.Visible = true;
                this.FPS91_TY_S_AC_2533B997.Visible = false;

                this.DbConnector.Attach
                    (
                    "TY_P_AC_2532X990",
                    this.DTP01_GSTYYMM.GetValue(),
                    this.DTP01_GEDYYMM.GetValue(),
                    sGDEID.ToString()
                    );

                this.FPS91_TY_S_AC_2532Z992.SetValue(this.DbConnector.ExecuteDataTable());
            }
            else if (this.CBO01_INQOPTION.GetValue().ToString() == "3")
            {
                this.FPS91_TY_S_AC_25324985.Visible = false;
                this.FPS91_TY_S_AC_2532Z992.Visible = false;
                this.FPS91_TY_S_AC_2533B997.Visible = true;

                this.DbConnector.Attach
                    (
                    "TY_P_AC_2532Y991",
                    this.DTP01_GSTYYMM.GetValue(),
                    this.DTP01_GEDYYMM.GetValue()
                    );

                this.FPS91_TY_S_AC_2533B997.SetValue(this.DbConnector.ExecuteDataTable());
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sGDEID = string.Empty;
            sGDEID = this.CBO01_GDEID.GetValue().ToString();

            if (this.CBO01_GDEID.GetValue().ToString() == "")
            {
                sGDEID = "'11,12,21,22'";
            }

            this.DbConnector.CommandClear();

            if (this.CBO01_INQOPTION.GetValue().ToString() == "1")
            {
                this.DbConnector.Attach
                    (
                    "TY_P_AC_2535E999",
                    this.DTP01_GSTYYMM.GetValue(),
                    this.DTP01_GEDYYMM.GetValue(),
                    this.DTP01_GSTYYMM.GetValue(),
                    this.DTP01_GEDYYMM.GetValue(),
                    sGDEID.ToString()
                    );

                SectionReport rpt = new TYACCE0061R();

                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, this.DbConnector.ExecuteDataTable())).ShowDialog();
            }
            else if (this.CBO01_INQOPTION.GetValue().ToString() == "2")
            {
                this.DbConnector.Attach
                   (
                   "TY_P_AC_2535E001",
                   this.DTP01_GSTYYMM.GetValue(),
                   this.DTP01_GEDYYMM.GetValue(),
                   this.DTP01_GSTYYMM.GetValue(),
                   this.DTP01_GEDYYMM.GetValue(),
                   sGDEID.ToString()
                   );

                SectionReport rpt = new TYACCE0062R();

                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, this.DbConnector.ExecuteDataTable())).ShowDialog();
            }
            else if (this.CBO01_INQOPTION.GetValue().ToString() == "3")
            {
                this.DbConnector.Attach
                   (
                   "TY_P_AC_2535G002",
                   this.DTP01_GSTYYMM.GetValue(),
                   this.DTP01_GEDYYMM.GetValue(),
                   this.DTP01_GSTYYMM.GetValue(),
                   this.DTP01_GEDYYMM.GetValue()
                   );

                SectionReport rpt = new TYACCE0063R();

                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, this.DbConnector.ExecuteDataTable())).ShowDialog();
            }
        }
        #endregion
    }
}