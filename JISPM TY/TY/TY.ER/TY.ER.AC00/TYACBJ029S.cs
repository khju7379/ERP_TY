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
    /// 영업비용명세서 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.08.17 14:18
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_28H2C424 : 영업비용명세서
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_28M65508 : 영업비용명세서
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  GPRTGN : 출력구분
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACBJ029S : TYBase
    {
        #region Description : 페이지 로드
        public TYACBJ029S()
        {
            InitializeComponent();
        }

        private void TYACBJ029S_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.DTP01_GSTYYMM);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_28H2C424",
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GEDYYMM.GetValue(),
                TYUserInfo.EmpNo,
                this.CBO01_E6PRGN.GetValue().ToString()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_28M65508.SetValue(dt);

                // 특정 COLUMN 색깔 입히기
                this.FPS91_TY_S_AC_28M65508.ActiveSheet.Columns["TMAMHAP1"].BackColor = Color.FromArgb(218, 239, 244);
                this.FPS91_TY_S_AC_28M65508.ActiveSheet.Columns["TMAMHAP3"].BackColor = Color.FromArgb(218, 239, 244);
                this.FPS91_TY_S_AC_28M65508.ActiveSheet.Columns["TMAMHAP6"].BackColor = Color.FromArgb(218, 239, 244);
                this.FPS91_TY_S_AC_28M65508.ActiveSheet.Columns["TMAMHAP7"].BackColor = Color.FromArgb(218, 239, 244);
                this.FPS91_TY_S_AC_28M65508.ActiveSheet.Columns["TMAMHAP9"].BackColor = Color.FromArgb(218, 239, 244);

                // 특정 ROW 색깔 입히기
                for (int i = 0; i < this.FPS91_TY_S_AC_28M65508.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_28M65508.GetValue(i, "TMCDACNM").ToString() == "총     계")
                    {
                        // 특정 칼럼 색깔 입히기
                        this.FPS91_TY_S_AC_28M65508.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 194);
                    }
                }
            }
            else
            {
                this.FPS91_TY_S_AC_28M65508.SetValue(dt);

                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_28H2C424",
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GEDYYMM.GetValue(),
                TYUserInfo.EmpNo,
                this.CBO01_E6PRGN.GetValue().ToString()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (this.CBO01_GPRTGN.GetValue().ToString() == "A")
                {
                    // 운영비
                    SectionReport rpt1 = new TYACBJ029R1();
                    rpt1.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait;
                    (new TYERGB001P(rpt1, dt)).ShowDialog();

                    // 관리비
                    SectionReport rpt2 = new TYACBJ029R2();
                    rpt2.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait;
                    (new TYERGB001P(rpt2, dt)).ShowDialog();

                    // 판매비
                    SectionReport rpt3 = new TYACBJ029R3();
                    rpt3.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait;
                    (new TYERGB001P(rpt3, dt)).ShowDialog();

                    // 무역판매비
                    SectionReport rpt5 = new TYACBJ029R5();
                    rpt5.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait;
                    (new TYERGB001P(rpt5, dt)).ShowDialog();
                }
                else if (this.CBO01_GPRTGN.GetValue().ToString() == "1")
                {
                    // 운영비
                    SectionReport rpt1 = new TYACBJ029R1();
                    rpt1.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait;
                    (new TYERGB001P(rpt1, dt)).ShowDialog();
                }
                else if (this.CBO01_GPRTGN.GetValue().ToString() == "2")
                {
                    // 관리비
                    SectionReport rpt2 = new TYACBJ029R2();
                    rpt2.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait;
                    (new TYERGB001P(rpt2, dt)).ShowDialog();
                }
                else if (this.CBO01_GPRTGN.GetValue().ToString() == "3")
                {
                    // 판매비
                    SectionReport rpt3 = new TYACBJ029R3();
                    rpt3.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait;
                    (new TYERGB001P(rpt3, dt)).ShowDialog();
                }
                else if (this.CBO01_GPRTGN.GetValue().ToString() == "5")
                {
                    // 무역판매비
                    SectionReport rpt5 = new TYACBJ029R5();
                    rpt5.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait;
                    (new TYERGB001P(rpt5, dt)).ShowDialog();
                }
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