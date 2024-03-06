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
    /// 미지급금 청구서 조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.05.10 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_25A6E258 : 미지급금 청구서 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_25A6J259 : 미지급금 청구서
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  M1GUBN : 지급형태
    ///  M1SAGB : 지역구분
    ///  M1DTED : 지급일자
    /// </summary>
    public partial class TYACFP003S : TYBase
    {
        #region Description : 페이지 로드
        public TYACFP003S()
        {
            InitializeComponent();
        }

        private void TYACFP003S_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.CBO01_M1SAGB);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sM1SAGB = string.Empty;

            if (this.CBO01_M1SAGB.GetValue().ToString() != "")
            {
                sM1SAGB = this.CBO01_M1SAGB.GetValue().ToString() == "1" ? "1" : "6";
            }

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_25A6E258",
                this.DTP01_M1DTED.GetValue(),
                this.CBO01_M1GUBN.GetValue(),
                sM1SAGB
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_25A6J259.SetValue(dt);

                // 특정 ROW 잠금
                for (int i = 0; i < this.FPS91_TY_S_AC_25A6J259.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_25A6J259.GetValue(i, "M1RKAC").ToString() == "소계")
                    {
                        // 특정 칼럼 색깔 입히기
                        this.FPS91_TY_S_AC_25A6J259.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    }
                    else if (this.FPS91_TY_S_AC_25A6J259.GetValue(i, "M1RKAC").ToString() == "총계")
                    {
                        // 특정 칼럼 색깔 입히기
                        this.FPS91_TY_S_AC_25A6J259.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
                    }
                }
            }
            else
            {
                this.FPS91_TY_S_AC_25A6J259.SetValue(dt);
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sM1SAGB = string.Empty;

            if (this.CBO01_M1SAGB.GetValue().ToString() != "")
            {
                sM1SAGB = this.CBO01_M1SAGB.GetValue().ToString() == "1" ? "1" : "6";
            }

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_25A6E258",
                this.DTP01_M1DTED.GetValue(),
                this.CBO01_M1GUBN.GetValue(),
                sM1SAGB
                );

            SectionReport rpt = new TYACFP003R();

            rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

            (new TYERGB001P(rpt, this.DbConnector.ExecuteDataTable())).ShowDialog();
        }
        #endregion
    }
}
