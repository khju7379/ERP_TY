using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using GrapeCity.ActiveReports;
using TY.ER.AC00;
using TY.ER.GB00;

namespace TY.ER.AC00
{
    /// <summary>
    /// 재무상태표 부속서류 출력 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2014.07.14 13:30
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_479DT990 : 결산 재무상태표 부속서류 현금성자산 출력
    ///  TY_P_AC_47BF9013 : 결산 재무상태표 부속명세 출력_01
    ///  TY_P_AC_47F9Z025 : 결산 재무상태표 부속명세 출력_02
    ///  TY_P_AC_47FB6026 : 결산 재무상태표 부속명세 출력_03
    ///  TY_P_AC_47FGW030 : 결산 재무상태표 부속명세 출력_04
    ///  TY_P_AC_47G8Z032 : 결산 재무상태표 부속명세 출력_05
    ///  TY_P_AC_47GA0034 : 결산 재무상태표 부속명세 출력_채무
    ///  TY_P_AC_47GA8033 : 결산 재무상태표 부속명세 출력_채권
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  PRT : 출력
    ///  PRTCHK01 : 현금성자산
    ///  PRTCHK02 : 선수금
    ///  PRTCHK03 : 미수금
    ///  PRTCHK04 : 예수금
    ///  PRTCHK05 : 선급금
    ///  PRTCHK06 : 미지급비용
    ///  PRTCHK07 : 선급비용
    ///  PRTCHK08 : 예수보증금
    ///  PRTCHK09 : 장기미수금
    ///  PRTCHK10 : 임대보증금
    ///  PRTCHK11 : 보증금
    ///  PRTCHK12 : 매입채무
    ///  PRTCHK13 : 매출채권
    ///  PRTCHK14 : 기부금
    ///  PRTCHK15 : 이자수익
    ///  PRTCHK16 : 이자비용
    ///  S1CHK12 : 전체
    ///  ELXYYMM : 기준년도
    /// </summary>
    public partial class TYACSE010P : TYBase
    {
        public TYACSE010P()
        {
            InitializeComponent();
        }

        private void TYACSE010P_Load(object sender, System.EventArgs e)
        {
            this.BTN61_PRT.ProcessCheck += new TButton.CheckHandler(BTN61_PRT_ProcessCheck);

            this.SetStartingFocus(this.DTP01_ELXYYMM);
        }

        #region Description : 출력 ProcessCheck 이벤트
        private void BTN61_PRT_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (CKB01_PRTCHK01.Checked != true && CKB01_PRTCHK02.Checked != true && CKB01_PRTCHK03.Checked != true && CKB01_PRTCHK04.Checked != true &&
                CKB01_PRTCHK05.Checked != true && CKB01_PRTCHK06.Checked != true && CKB01_PRTCHK07.Checked != true && CKB01_PRTCHK08.Checked != true &&
                CKB01_PRTCHK09.Checked != true && CKB01_PRTCHK10.Checked != true && CKB01_PRTCHK11.Checked != true && CKB01_PRTCHK12.Checked != true &&
                CKB01_PRTCHK13.Checked != true && CKB01_PRTCHK14.Checked != true && CKB01_PRTCHK15.Checked != true && CKB01_PRTCHK16.Checked != true)
            {
                this.ShowCustomMessage("선택한 출력문서가 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sPRYYMM = this.DTP01_ELXYYMM.GetValue().ToString().Substring(0, 6);

            string sPRCDAC = string.Empty;
            string sPRTITNO = string.Empty;

            // PRTCHK01 : 현금성자산 
            if (CKB01_PRTCHK01.Checked == true)
            {
                PRT_Group01(sPRYYMM);
            }

            // PRTCHK02 : 선수금     ,  PRTCHK03 : 미수금     ,  PRTCHK05 : 선급금, PRTCHK07 : 선급비용
            // PRTCHK08 : 예수보증금 ,  PRTCHK10 : 임대보증금 ,  PRTCHK11 : 보증금
            if (CKB01_PRTCHK02.Checked == true || CKB01_PRTCHK03.Checked == true || CKB01_PRTCHK05.Checked == true ||
                CKB01_PRTCHK07.Checked == true || CKB01_PRTCHK08.Checked == true || CKB01_PRTCHK10.Checked == true || CKB01_PRTCHK11.Checked == true )
            {

                if (CKB01_PRTCHK02.Checked == true)
                {
                    sPRCDAC = "21100800";
                    sPRTITNO = "02"; // "선 수 금";

                    PRT_Group02(sPRYYMM, sPRCDAC, sPRTITNO);
                }
                if (CKB01_PRTCHK03.Checked == true)
                {
                    sPRCDAC = "11100800";
                    sPRTITNO = "03"; // "미 수 금";

                    PRT_Group02(sPRYYMM, sPRCDAC, sPRTITNO);
                }
                if (CKB01_PRTCHK05.Checked == true)
                {
                    sPRCDAC = "11101000";
                    sPRTITNO = "05"; //  "선 급 금";

                    PRT_Group02(sPRYYMM, sPRCDAC, sPRTITNO);
                }
                if (CKB01_PRTCHK07.Checked == true)
                {
                    sPRCDAC = "11101100";
                    sPRTITNO = "07"; // "선 급 비 용";

                    PRT_Group02(sPRYYMM, sPRCDAC, sPRTITNO);
                }

                if (CKB01_PRTCHK08.Checked == true)
                {
                    sPRCDAC = "21101300";
                    sPRTITNO = "08"; // "예수 보증금";

                    PRT_Group02(sPRYYMM, sPRCDAC, sPRTITNO);
                }

                if (CKB01_PRTCHK10.Checked == true)
                {
                    sPRCDAC = "22100400";
                    sPRTITNO = "10"; // "임대 보증금";

                    PRT_Group02(sPRYYMM, sPRCDAC, sPRTITNO);
                }

                if (CKB01_PRTCHK11.Checked == true)
                {
                    sPRCDAC = "12400400";
                    sPRTITNO = "11"; // "보 증 금";

                    PRT_Group02(sPRYYMM, sPRCDAC, sPRTITNO);
                }
            }

            // PRTCHK04 : 예수금 , PRTCHK06 : 미지급 비용
            if (CKB01_PRTCHK04.Checked == true || CKB01_PRTCHK06.Checked == true)
            {
                if (CKB01_PRTCHK04.Checked == true)
                {
                    sPRCDAC = "21101300";
                    sPRTITNO = "04"; // "임대 보증금";

                    PRT_Group03(sPRYYMM, sPRCDAC, sPRTITNO);
                }

                if (CKB01_PRTCHK06.Checked == true)
                {
                    sPRCDAC = "21101000";
                    sPRTITNO = "06"; // "보 증 금";

                    PRT_Group03(sPRYYMM, sPRCDAC, sPRTITNO);
                }
            }

            // PRTCHK15 : 이자수익,  PRTCHK16 : 이자비용
            if (CKB01_PRTCHK15.Checked == true || CKB01_PRTCHK16.Checked == true)
            {
                if (CKB01_PRTCHK15.Checked == true)
                {
                    sPRCDAC = "51000100";
                    sPRTITNO = "15"; // "이 자 수 익";

                    PRT_Group04(sPRYYMM, sPRCDAC, sPRTITNO);
                }

                if (CKB01_PRTCHK16.Checked == true)
                {
                    sPRCDAC = "52000100";
                    sPRTITNO = "16"; // "이 자 비 용";

                    PRT_Group04(sPRYYMM, sPRCDAC, sPRTITNO);
                }
            }

            // PRTCHK14 : 기부금
            if (CKB01_PRTCHK14.Checked == true)
            {
                sPRCDAC = "52001500";
                sPRTITNO = "14"; // "기 부 금";

                PRT_Group05(sPRYYMM, sPRCDAC, sPRTITNO);
            }

            // PRTCHK09 : 장기미수금
            if (CKB01_PRTCHK09.Checked == true)
            {
                sPRCDAC = "12400300";
                sPRTITNO = "09"; // "장기미수금";

                PRT_Group06(sPRYYMM, sPRCDAC, sPRTITNO);
            }

            // PRTCHK12 : 매입채무 , PRTCHK13 : 매입채권
            if (CKB01_PRTCHK12.Checked == true || CKB01_PRTCHK13.Checked == true)
            {
                string sPROCID = string.Empty;

                if (CKB01_PRTCHK12.Checked == true)
                {
                    sPROCID = "TY_P_AC_47GA0034";  // "매입채무";
                    PRT_Group07(sPRYYMM, sPROCID);
                }

                if (CKB01_PRTCHK13.Checked == true)
                {
                    sPROCID = "TY_P_AC_47GA8033";  // "매출채권";
                    PRT_Group07(sPRYYMM, sPROCID);
                }
            }

        }
        #endregion


        #region Description : 출력  이벤트 01 (현금성자산)
        private void PRT_Group01(string sPRYYMM)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_479DT990", sPRYYMM);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYACSE010R1();
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
        }
        #endregion

        #region Description : 출력  이벤트 02 (선수금, 미수금, 선급금, 선급비용, 예수보증금, 임대보증금, 보증금)
        private void PRT_Group02(string sPRYYMM, string sPRCDAC, string sPRTITNO)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_47BF9013", sPRYYMM, sPRCDAC, sPRTITNO);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYACSE010R2();
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
        }
        #endregion

        #region Description : 출력  이벤트 03 (예수금 , 미지급 비용)
        private void PRT_Group03(string sPRYYMM, string sPRCDAC, string sPRTITNO)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_47F9Z025", sPRYYMM, sPRCDAC, sPRTITNO);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYACSE010R3();
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
        }
        #endregion

        #region Description : 출력  이벤트 04 (이자수익, 이자비용)
        private void PRT_Group04(string sPRYYMM, string sPRCDAC, string sPRTITNO)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_47FB6026", sPRYYMM, sPRCDAC, sPRTITNO);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYACSE010R4();
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
        }
        #endregion

        #region Description : 출력  이벤트 05 (기 부 금)
        private void PRT_Group05(string sPRYYMM, string sPRCDAC, string sPRTITNO)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_47FGW030", sPRYYMM, sPRCDAC, sPRTITNO);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYACSE010R5();
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
        }
        #endregion

        #region Description : 출력  이벤트 06 (장기미수금)
        private void PRT_Group06(string sPRYYMM, string sPRCDAC, string sPRTITNO)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_47G8Z032", sPRYYMM, sPRCDAC, sPRTITNO);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYACSE010R6();
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
        }
        #endregion

        #region Description : 출력  이벤트 07 (매입채무- TY_P_AC_47GA0034 , 매입채권 - TY_P_AC_47GA8033)
        private void PRT_Group07(string sPRYYMM, string sPROCID)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach(sPROCID, sPRYYMM);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYACSE010R7();
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
        }
        #endregion

        #region Description : 체크박스 이벤트
        private void CKB01_PRTCHK01_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_PRTCHK01.Checked == false)
            {
                this.CKB01_S1CHK12.Checked = false;
            }

        }

        private void CKB01_PRTCHK02_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_PRTCHK02.Checked == false)
            {
                this.CKB01_S1CHK12.Checked = false;
            }
        }

        private void CKB01_PRTCHK03_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_PRTCHK03.Checked == false)
            {
                this.CKB01_S1CHK12.Checked = false;
            }
        }

        private void CKB01_PRTCHK04_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_PRTCHK04.Checked == false)
            {
                this.CKB01_S1CHK12.Checked = false;
            }
        }

        private void CKB01_PRTCHK05_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_PRTCHK05.Checked == false)
            {
                this.CKB01_S1CHK12.Checked = false;
            }
        }

        private void CKB01_PRTCHK06_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_PRTCHK06.Checked == false)
            {
                this.CKB01_S1CHK12.Checked = false;
            }
        }

        private void CKB01_PRTCHK07_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_PRTCHK07.Checked == false)
            {
                this.CKB01_S1CHK12.Checked = false;
            }
        }

        private void CKB01_PRTCHK08_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_PRTCHK08.Checked == false)
            {
                this.CKB01_S1CHK12.Checked = false;
            }
        }

        private void CKB01_PRTCHK09_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_PRTCHK09.Checked == false)
            {
                this.CKB01_S1CHK12.Checked = false;
            }
        }

        private void CKB01_PRTCHK10_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_PRTCHK10.Checked == false)
            {
                this.CKB01_S1CHK12.Checked = false;
            }
        }

        private void CKB01_PRTCHK11_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_PRTCHK11.Checked == false)
            {
                this.CKB01_S1CHK12.Checked = false;
            }
        }

        private void CKB01_PRTCHK12_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_PRTCHK12.Checked == false)
            {
                this.CKB01_S1CHK12.Checked = false;
            }
        }

        private void CKB01_PRTCHK13_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_PRTCHK13.Checked == false)
            {
                this.CKB01_S1CHK12.Checked = false;
            }
        }

        private void CKB01_PRTCHK14_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_PRTCHK14.Checked == false)
            {
                this.CKB01_S1CHK12.Checked = false;
            }
        }

        private void CKB01_PRTCHK15_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_PRTCHK15.Checked == false)
            {
                this.CKB01_S1CHK12.Checked = false;
            }
        }

        private void CKB01_PRTCHK16_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_PRTCHK16.Checked == false)
            {
                this.CKB01_S1CHK12.Checked = false;
            }
        }


        // 전체
        private void CKB01_S1CHK12_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_S1CHK12.Checked == true)
            {
                CKB01_PRTCHK01.Checked = true;
                CKB01_PRTCHK02.Checked = true;
                CKB01_PRTCHK03.Checked = true;
                CKB01_PRTCHK04.Checked = true;
                CKB01_PRTCHK05.Checked = true;
                CKB01_PRTCHK06.Checked = true;
                CKB01_PRTCHK07.Checked = true;
                CKB01_PRTCHK08.Checked = true;
                CKB01_PRTCHK09.Checked = true;
                CKB01_PRTCHK10.Checked = true;
                CKB01_PRTCHK11.Checked = true;
                CKB01_PRTCHK12.Checked = true;
                CKB01_PRTCHK13.Checked = true;
                CKB01_PRTCHK14.Checked = true;
                CKB01_PRTCHK15.Checked = true;
                CKB01_PRTCHK16.Checked = true;

            }
            else if (CKB01_PRTCHK01.Checked == true && CKB01_PRTCHK02.Checked == true && CKB01_PRTCHK03.Checked == true && CKB01_PRTCHK04.Checked == true &&
                     CKB01_PRTCHK05.Checked == true && CKB01_PRTCHK06.Checked == true && CKB01_PRTCHK07.Checked == true && CKB01_PRTCHK08.Checked == true &&
                     CKB01_PRTCHK09.Checked == true && CKB01_PRTCHK10.Checked == true && CKB01_PRTCHK11.Checked == true && CKB01_PRTCHK12.Checked == true &&
                     CKB01_PRTCHK13.Checked == true && CKB01_PRTCHK14.Checked == true && CKB01_PRTCHK15.Checked == true && CKB01_PRTCHK16.Checked == true)
            {

                CKB01_PRTCHK01.Checked = false;
                CKB01_PRTCHK02.Checked = false;
                CKB01_PRTCHK03.Checked = false;
                CKB01_PRTCHK04.Checked = false;
                CKB01_PRTCHK05.Checked = false;
                CKB01_PRTCHK06.Checked = false;
                CKB01_PRTCHK07.Checked = false;
                CKB01_PRTCHK08.Checked = false;
                CKB01_PRTCHK09.Checked = false;
                CKB01_PRTCHK10.Checked = false;
                CKB01_PRTCHK11.Checked = false;
                CKB01_PRTCHK12.Checked = false;
                CKB01_PRTCHK13.Checked = false;
                CKB01_PRTCHK14.Checked = false;
                CKB01_PRTCHK15.Checked = false;
                CKB01_PRTCHK16.Checked = false;
            }
        }
        #endregion

    }
}