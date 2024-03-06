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
    /// 개인별 접대비 명세 조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.05.04 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2545P027 : 개인별 접대비 명세조회
    ///  TY_P_AC_25794032 : 개인별 접대비 명세출력
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2545Q028 : 개인별 접대비 명세조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  GCDDP : 사업장코드
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACCE008S : TYBase
    {
        #region Description : 페이지 로드
        public TYACCE008S()
        {
            InitializeComponent();
        }

        private void TYACCE008S_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.DTP01_GSTYYMM);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sGCDDP = string.Empty;

            if (this.CBO01_GCDDP.GetValue().ToString() != "")
            {
                sGCDDP = this.CBO01_GCDDP.GetValue().ToString().Substring(0,1);
            }

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_2545P027",
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GEDYYMM.GetValue(),
                sGCDDP.ToString()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_2545Q028.SetValue(dt);

                // 특정 ROW 색깔 입히기
                for (int i = 0; i < this.FPS91_TY_S_AC_2545Q028.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_2545Q028.GetValue(i, "B7NOMK").ToString() == "소계")
                    {
                        // 특정 칼럼 색깔 입히기
                        this.FPS91_TY_S_AC_2545Q028.ActiveSheet.Rows[i].BackColor = Color.FromArgb(228, 242, 194);
                    }

                    if (this.FPS91_TY_S_AC_2545Q028.GetValue(i, "B7NOMK").ToString() == "총계")
                    {
                        // 특정 칼럼 색깔 입히기
                        this.FPS91_TY_S_AC_2545Q028.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 194);
                    }
                }
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sGCDDP = string.Empty;

            if (this.CBO01_GCDDP.GetValue().ToString() != "")
            {
                sGCDDP = this.CBO01_GCDDP.GetValue().ToString().Substring(0, 1);
            }

            this.DbConnector.Attach
                (
                "TY_P_AC_25794032",
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GEDYYMM.GetValue(),
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GEDYYMM.GetValue(),
                sGCDDP.ToString()
                );

            SectionReport rpt = new TYACCE008R();

            rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

            (new TYERGB001P(rpt, this.DbConnector.ExecuteDataTable())).ShowDialog();
        }
        #endregion
    }
}