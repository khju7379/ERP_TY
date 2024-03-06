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
    /// 세무구분별 매출명세서 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.05.21 16:13
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_25H3V532 : 세무구분별 매입명세서 집계표
    ///  TY_P_AC_25L9Y567 : 세무구분별 매출명세서 조회
    ///  TY_P_AC_25LAP569 : 세무구분별 매출명세서 출력
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_25LA2568 : 세무구분별 매출명세서 조회
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
    ///  GDATEGUBUN : 일자구분
    ///  GPRTGN : 출력구분
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACGT005S : TYBase
    {
        #region Description : 페이지 로드
        public TYACGT005S()
        {
            InitializeComponent();
        }

        private void TYACGT005S_Load(object sender, System.EventArgs e)
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

            string sEDYYMM = this.DTP01_GEDYYMM.GetValue().ToString();

            int iDD = 0;

            // 해당월 마지막 일자 가져오기
            iDD = DateTime.DaysInMonth(int.Parse(sEDYYMM.ToString().Substring(0, 4)), int.Parse(sEDYYMM.ToString().Substring(4, 2)));

            sEDYYMM = sEDYYMM + Convert.ToString(iDD);

            DataTable dt = new DataTable();

            if (this.CBO01_B4VLMI1.GetValue().ToString() != "" && this.CBO01_B4VLMI1.GetValue().ToString() != "''")
            {
                sB4VLMI1 = this.CBO01_B4VLMI1.GetValue().ToString();
            }
            else
            {
                sB4VLMI1 = "";

                this.DbConnector.CommandClear();
                // 매출부가세 코드 가져오기
                this.DbConnector.Attach
                    (
                    "TY_P_AC_32C2M048",
                    "2"
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

            if (sB4VLMI2 == "1")
            {
                sB4VLMI2 = "21103101";
            }
            else
            {
                sB4VLMI2 = "21103102";
            }

            if (this.CBO01_B4VLMI1.GetValue().ToString() == "")
            {
                this.ShowMessage("TY_M_AC_25GAZ484");

                return;
            }

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_25L9Y567",
                sB4VLMI2.ToString(),
                this.CBO01_GDATEGUBUN.GetValue(),
                sSTYYMM.ToString(),
                sEDYYMM.ToString(),
                sB4VLMI1.ToString(),
                this.CBH01_VNCODE.GetValue()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_25LA2568.SetValue(dt);

                // 특정 ROW 색깔 입히기
                for (int i = 0; i < this.FPS91_TY_S_AC_25LA2568.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_25LA2568.GetValue(i, "B4DTAC").ToString() == "")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_25LA2568.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    }
                }
            }
            else
            {
                this.FPS91_TY_S_AC_25LA2568.SetValue(dt);

                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sB4VLMI1 = string.Empty;
            string sB4VLMI2 = this.CBO01_B4VLMI2.GetValue().ToString();
            string sSTYYMM  = this.DTP01_GSTYYMM.GetValue().ToString() + "01";

            string sEDYYMM = this.DTP01_GEDYYMM.GetValue().ToString();

            int iDD = 0;

            // 해당월 마지막 일자 가져오기
            iDD = DateTime.DaysInMonth(int.Parse(sEDYYMM.ToString().Substring(0, 4)), int.Parse(sEDYYMM.ToString().Substring(4, 2)));

            sEDYYMM = sEDYYMM + Convert.ToString(iDD);

            string sB4VLMI1NM    = string.Empty;
            string sFirstB4VLMI1 = string.Empty;
            string sLastB4VLMI1  = string.Empty;

            DataTable dt = new DataTable();

            if (this.CBO01_B4VLMI1.GetValue().ToString() != "" && this.CBO01_B4VLMI1.GetValue().ToString() != "''")
            {
                sB4VLMI1 = this.CBO01_B4VLMI1.GetValue().ToString();

                sB4VLMI1NM    = this.CBO01_B4VLMI1.GetFirstText();
                sFirstB4VLMI1 = this.CBO01_B4VLMI1.GetFirstText();
                sLastB4VLMI1  = this.CBO01_B4VLMI1.GetLastText();
            }
            else
            {
                sB4VLMI1 = "";

                this.DbConnector.CommandClear();
                // 매출부가세 코드 가져오기
                this.DbConnector.Attach
                    (
                    "TY_P_AC_32C2M048",
                    "2"
                    );

                dt = this.DbConnector.ExecuteDataTable();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i != 0)
                    {
                        sB4VLMI1 = sB4VLMI1.ToString() + "," + dt.Rows[i][0].ToString();

                        sLastB4VLMI1 = dt.Rows[i][1].ToString();
                    }
                    else
                    {
                        sB4VLMI1 = dt.Rows[i][0].ToString();

                        sB4VLMI1NM    = dt.Rows[i][1].ToString();
                        sFirstB4VLMI1 = dt.Rows[i][1].ToString();
                        sLastB4VLMI1  = dt.Rows[i][1].ToString();
                    }
                }
            }

            if (sFirstB4VLMI1.ToString() != sLastB4VLMI1.ToString())
            {
                sB4VLMI1NM = sB4VLMI1NM + "~" + sLastB4VLMI1.ToString();
            }

            if (sB4VLMI2 == "1")
            {
                sB4VLMI2 = "21103101";
            }
            else
            {
                sB4VLMI2 = "21103102";
            }

            if (this.CBO01_B4VLMI1.GetValue().ToString() == "")
            {
                this.ShowMessage("TY_M_AC_25GAZ484");

                return;
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_25LAP569",
                sB4VLMI2.ToString(),
                this.CBO01_GDATEGUBUN.GetValue(),
                sSTYYMM.ToString(),
                sEDYYMM.ToString(),
                sB4VLMI1.ToString(),
                this.CBH01_VNCODE.GetValue(),
                this.CBO01_GDATEGUBUN.GetText(),
                this.CBO01_B4VLMI2.GetText(),
                sB4VLMI1NM.ToString()
                );

            SectionReport rpt = new TYACGT005R();

            rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

            (new TYERGB001P(rpt, this.DbConnector.ExecuteDataTable())).ShowDialog();



            
            this.DbConnector.Attach
                (
                "TY_P_AC_25H3V532",
                sB4VLMI2.ToString(),
                this.CBO01_GDATEGUBUN.GetValue(),
                sSTYYMM.ToString(),
                sEDYYMM.ToString(),
                sB4VLMI1.ToString(),
                this.CBH01_VNCODE.GetValue(),
                this.CBO01_GDATEGUBUN.GetText(),
                this.CBO01_B4VLMI2.GetText(),
                sB4VLMI1NM.ToString()
                );

            SectionReport rpt1 = new TYACGT003R1();

            rpt1.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

            (new TYERGB001P(rpt1, this.DbConnector.ExecuteDataTable())).ShowDialog();
        }
        #endregion
    }
}