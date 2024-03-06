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
    /// 세무구분별 매입명세서 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.05.14 16:13
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_25EAH372 : 세무구분별 매입명세서 조회
    ///  TY_P_AC_25G19489 : 세무구분별 매입명세서 출력
    ///  TY_P_AC_25H3V532 : 세무구분별 매입명세서 집계표
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_25E4Y431 : 세무구분별 매입명세서
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
    ///  CBO01_GPRTGN : 출력구분
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACTX001S : TYBase
    {
        #region Description : 페이지 로드
        public TYACTX001S()
        {
            InitializeComponent();
        }

        private void TYACTX001S_Load(object sender, System.EventArgs e)
        {
            UP_COMBOBOX(this.CBO01_GGUBUN.GetValue().ToString());

            SetStartingFocus(this.CBO01_GGUBUN);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sB4VLMI2 = this.CBO01_B4VLMI2.GetValue().ToString();
            string sSTYYMM  = this.DTP01_GSTYYMM.GetValue().ToString() + "01";
            string sEDYYMM  = this.DTP01_GEDYYMM.GetValue().ToString();

            int iDD = 0;

            // 해당월 마지막 일자 가져오기
            iDD = DateTime.DaysInMonth(int.Parse(sEDYYMM.ToString().Substring(0, 4)), int.Parse(sEDYYMM.ToString().Substring(4, 2)));

            sEDYYMM = sEDYYMM + Convert.ToString(iDD);

            string sB4VLMI1 = string.Empty;

            DataTable dt = new DataTable();

            if (this.CBO01_B4VLMI1.GetValue().ToString() != "" && this.CBO01_B4VLMI1.GetValue().ToString() != "''")
            {
                sB4VLMI1 = this.CBO01_B4VLMI1.GetValue().ToString();
            }
            else
            {
                sB4VLMI1 = "";

                // 부가세 코드 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_32C2M048",
                    this.CBO01_GGUBUN.GetValue().ToString()
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

            if (this.CBO01_GGUBUN.GetValue().ToString() == "1")
            {
                if (sB4VLMI2 == "1")
                {
                    sB4VLMI2 = "11103101";
                }
                else
                {
                    sB4VLMI2 = "11103102";
                }
            }
            else
            {
                if (sB4VLMI2 == "1")
                {
                    sB4VLMI2 = "21103101";
                }
                else
                {
                    sB4VLMI2 = "21103102";
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
                "TY_P_AC_3BJ3S363",
                this.CBO01_GGUBUN.GetValue().ToString(),
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
                this.FPS91_TY_S_AC_3BKA2373.SetValue(dt);

                // 특정 ROW 색깔 입히기
                for (int i = 0; i < this.FPS91_TY_S_AC_3BKA2373.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_3BKA2373.GetValue(i, "VNSANGHO").ToString() == "소계")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_3BKA2373.ActiveSheet.Rows[i].BackColor = Color.FromArgb(228, 242, 194);
                    }

                    if (this.FPS91_TY_S_AC_3BKA2373.GetValue(i, "VNSANGHO").ToString() == "총계")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_3BKA2373.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    }
                }
            }
            else
            {
                this.FPS91_TY_S_AC_3BKA2373.SetValue(dt);

                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sFirstB4VLMI1 = string.Empty;
            string sLastB4VLMI1  = string.Empty;
            string sB4VLMI1      = string.Empty;
            string sB4VLMI2      = this.CBO01_B4VLMI2.GetValue().ToString();
            string sSTYYMM       = this.DTP01_GSTYYMM.GetValue().ToString() + "01";
            string sEDYYMM       = this.DTP01_GEDYYMM.GetValue().ToString();

            int iDD = 0;

            // 해당월 마지막 일자 가져오기
            iDD = DateTime.DaysInMonth(int.Parse(sEDYYMM.ToString().Substring(0, 4)), int.Parse(sEDYYMM.ToString().Substring(4, 2)));

            sEDYYMM = sEDYYMM + Convert.ToString(iDD);

            DataTable dt = new DataTable();

            if (this.CBO01_B4VLMI1.GetValue().ToString() != "" && this.CBO01_B4VLMI1.GetValue().ToString() != "''")
            {
                sB4VLMI1 = this.CBO01_B4VLMI1.GetValue().ToString();

                // 세무구분 첫번째 값 가져오기
                sFirstB4VLMI1 = this.CBO01_B4VLMI1.GetFirstValue().ToString();

                // 세무구분 마지막 값 가져오기
                sLastB4VLMI1  = this.CBO01_B4VLMI1.GetLastValue().ToString();
            }
            else
            {
                sB4VLMI1 = "";

                // 부가세 코드 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_32C2M048",
                    this.CBO01_GGUBUN.GetValue().ToString()
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

            if (this.CBO01_GGUBUN.GetValue().ToString() == "1")
            {
                if (sB4VLMI2 == "1")
                {
                    sB4VLMI2 = "11103101";
                }
                else
                {
                    sB4VLMI2 = "11103102";
                }
            }
            else
            {
                if (sB4VLMI2 == "1")
                {
                    sB4VLMI2 = "21103101";
                }
                else
                {
                    sB4VLMI2 = "21103102";
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
                "TY_P_AC_42R54537",
                this.CBO01_GGUBUN.GetValue().ToString(),
                sB4VLMI2.ToString(),
                this.CBO01_GDATEGUBUN.GetValue(),
                sSTYYMM.ToString(),
                sEDYYMM.ToString(),
                sB4VLMI1.ToString(),
                this.CBH01_VNCODE.GetValue(),
                this.CBO01_B4VLMI2.GetText(),
                sFirstB4VLMI1.ToString().Replace("'", ""),
                sLastB4VLMI1.ToString().Replace("'", "")
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt1 = new TYACTX001R1();

                rpt1.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt1, this.DbConnector.ExecuteDataTable())).ShowDialog();


                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_42R63539",
                    this.CBO01_GGUBUN.GetValue().ToString(),
                    sB4VLMI2.ToString(),
                    this.CBO01_GDATEGUBUN.GetValue(),
                    sSTYYMM.ToString(),
                    sEDYYMM.ToString(),
                    sB4VLMI1.ToString(),
                    this.CBH01_VNCODE.GetValue(),
                    this.CBO01_B4VLMI2.GetText(),
                    sFirstB4VLMI1.ToString().Replace("'", ""),
                    sLastB4VLMI1.ToString().Replace("'", "")
                    );

                dt = this.DbConnector.ExecuteDataTable();

                SectionReport rpt2 = new TYACTX001R2();

                rpt2.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt2, this.DbConnector.ExecuteDataTable())).ShowDialog();
            }
            else
            {
                this.FPS91_TY_S_AC_3BKA2373.SetValue(dt);

                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        }
        #endregion

        #region Description : 구분 콤보박스
        private void CBO01_GGUBUN_SelectedIndexChanged(object sender, EventArgs e)
        {
            UP_COMBOBOX(this.CBO01_GGUBUN.GetValue().ToString());
        }
        #endregion

        #region Description : 부가세에 따른 세무구분값 가져오기
        private void UP_COMBOBOX(string sCODE)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3BK1T383",
                sCODE.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.CBO01_B4VLMI1.DataBind(dt, false);
        }
        #endregion

        #region Description : 미승인 전표 화면 띄우기
        private void FPS91_TY_S_AC_3BKA2373_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column.ToString() == "8")
            {
                if (this.FPS91_TY_S_AC_3BKA2373.GetValue("VNSANGHO").ToString() != "소계" &&
                    this.FPS91_TY_S_AC_3BKA2373.GetValue("VNSANGHO").ToString() != "총계")
                {
                    string sB2DPMK = this.FPS91_TY_S_AC_3BKA2373.GetValue("B4NOJP").ToString().Substring(0, 6);
                    string sB2DTMK = this.FPS91_TY_S_AC_3BKA2373.GetValue("B4NOJP").ToString().Substring(6, 8);
                    string sB2NOSQ = this.FPS91_TY_S_AC_3BKA2373.GetValue("B4NOJP").ToString().Substring(14, 3);

                    if (this.OpenModalPopup(new TYACBJ001I(sB2DPMK, sB2DTMK, sB2NOSQ)) == System.Windows.Forms.DialogResult.OK)
                        this.BTN61_INQ_Click(null, null);
                }
            }
        }
        #endregion
    }
}