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
    /// 매입처별 세금계산서명세서 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.05.17 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_25H58535 : 매입처별 세금계산서 명세서 조회
    ///  TY_P_AC_25IAG543 : 매입처별 세금계산서 명세서 출력
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_25H5A537 : 매입처별 세금계산서 명세서 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_25GAZ484 : 세무 구분을 선택하세요.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  VNCODE : 거래처코드
    ///  B4VLMI1 : 관리항목값１
    ///  B4VLMI2 : 관리항목값２
    ///  B4VLMI4 : 관리항목값４
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACGT004S : TYBase
    {
        #region Description : 페이지 로드
        public TYACGT004S()
        {
            InitializeComponent();
        }

        private void TYACGT004S_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.CBO01_B4VLMI2);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sB4VLMI1 = string.Empty;
            string sB4VLMI2 = this.CBO01_B4VLMI2.GetValue().ToString();
            string sSTYYMM  = this.DTP01_GSTYYMM.GetValue().ToString() + "01";
            string sEDYYMM  = this.DTP01_GEDYYMM.GetValue().ToString();
            string sYear    = this.DTP01_GEDYYMM.GetValue().ToString().Substring(0,4);
            string sMonth   = this.DTP01_GEDYYMM.GetValue().ToString().Substring(4,2);

            // 해당월 마지막 일자 가져오기
            int iDD = DateTime.DaysInMonth(int.Parse(sYear.ToString()), int.Parse(sMonth.ToString()));

            sEDYYMM = sEDYYMM + Convert.ToString(iDD);

            if (sB4VLMI2 == "1")
            {
                sB4VLMI2 = "11103101";
            }
            else
            {
                sB4VLMI2 = "11103102";
            }

            DataTable dt = new DataTable();

            if (this.CBO01_B4VLMI1.GetValue().ToString() != "" && this.CBO01_B4VLMI1.GetValue().ToString() != "''")
            {
                sB4VLMI1 = this.CBO01_B4VLMI1.GetValue().ToString();
            }
            else
            {
                sB4VLMI1 = "";

                this.DbConnector.CommandClear();
                // 매입부가세 코드 가져오기
                this.DbConnector.Attach
                    (
                    "TY_P_AC_32C2M048",
                    "1"
                    );

                dt = this.DbConnector.ExecuteDataTable();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i != 0)
                    {
                        sB4VLMI1 = sB4VLMI1.ToString() + "," + dt.Rows[i][0].ToString();
                    }
                    else
                    {
                        sB4VLMI1 = dt.Rows[i][0].ToString();
                    }
                }
            }

            if (this.CBO01_B4VLMI1.GetValue().ToString() == "")
            {
                this.ShowMessage("TY_M_AC_25GAZ484");

                return;
            }

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_32316006",
                sSTYYMM.ToString(),
                sEDYYMM.ToString(),
                sB4VLMI2.ToString(),
                sB4VLMI1.ToString(),
                this.CBH01_VNCODE.GetValue()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_25H5A537.SetValue(dt);

                // 특정 ROW 색깔 입히기
                for (int i = 0; i < this.FPS91_TY_S_AC_25H5A537.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_25H5A537.GetValue(i, "VNCODE").ToString() == "소계")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_25H5A537.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    }
                }
            }
            else
            {
                this.FPS91_TY_S_AC_25H5A537.SetValue(dt);

                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sB4VLMI1 = string.Empty;
            string sFirstB4VLMI1 = string.Empty;
            string sLastB4VLMI1  = string.Empty;
            string sB4VLMI2 = this.CBO01_B4VLMI2.GetValue().ToString();
            string sSTYYMM  = this.DTP01_GSTYYMM.GetValue().ToString() + "01";
            string sEDYYMM  = this.DTP01_GEDYYMM.GetValue().ToString();
            string sYear    = this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 4);
            string sMonth   = this.DTP01_GEDYYMM.GetValue().ToString().Substring(4, 2);

            // 해당월 마지막 일자 가져오기
            int iDD = DateTime.DaysInMonth(int.Parse(sYear.ToString()), int.Parse(sMonth.ToString()));

            sEDYYMM = sEDYYMM + Convert.ToString(iDD);

            // 사업장
            if (sB4VLMI2 == "1")
            {
                sB4VLMI2 = "11103101";
            }
            else
            {
                sB4VLMI2 = "11103102";
            }

            DataTable dt = new DataTable();

            if (this.CBO01_B4VLMI1.GetValue().ToString() != "" && this.CBO01_B4VLMI1.GetValue().ToString() != "''")
            {
                sB4VLMI1 = this.CBO01_B4VLMI1.GetValue().ToString();

                // 세무구분 첫번째 값 가져오기
                sFirstB4VLMI1 = this.CBO01_B4VLMI1.GetFirstValue().ToString();

                // 세무구분 마지막 값 가져오기
                sLastB4VLMI1 = this.CBO01_B4VLMI1.GetLastValue().ToString();
            }
            else
            {
                sB4VLMI1 = "";

                this.DbConnector.CommandClear();
                // 매입부가세 코드 가져오기
                this.DbConnector.Attach
                    (
                    "TY_P_AC_32C2M048",
                    "1"
                    );

                dt = this.DbConnector.ExecuteDataTable();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i != 0)
                    {
                        sB4VLMI1 = sB4VLMI1.ToString() + "," + dt.Rows[i][0].ToString();

                        // 세무구분 마지막 값 가져오기
                        sLastB4VLMI1 = dt.Rows[i][0].ToString();
                    }
                    else
                    {
                        sB4VLMI1 = dt.Rows[i][0].ToString();

                        // 세무구분 첫번째 값 가져오기
                        sFirstB4VLMI1 = dt.Rows[i][0].ToString();

                        // 세무구분 마지막 값 가져오기
                        sLastB4VLMI1 = dt.Rows[i][0].ToString();
                    }
                }
            }

            if (this.CBO01_B4VLMI1.GetValue().ToString() == "")
            {
                this.ShowMessage("TY_M_AC_25GAZ484");

                return;
            }

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_25IAG543",
                sSTYYMM.ToString(),
                sEDYYMM.ToString(),
                this.CBO01_B4VLMI2.GetText(),
                sFirstB4VLMI1.ToString().Replace("'", ""),
                sLastB4VLMI1.ToString().Replace("'", ""),
                sB4VLMI2.ToString(),
                sB4VLMI1.ToString(),
                this.CBH01_VNCODE.GetValue()
                );

            SectionReport rpt = new TYACGT004R();

            rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

            (new TYERGB001P(rpt, this.DbConnector.ExecuteDataTable())).ShowDialog();
        }
        #endregion
    }
}