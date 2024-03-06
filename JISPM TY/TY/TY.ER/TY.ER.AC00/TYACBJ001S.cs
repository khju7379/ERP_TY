using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using GrapeCity.ActiveReports;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using TY.ER.GB00;


namespace TY.ER.AC00
{
    /// <summary>
    /// 미승인전표 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.11.15 14:37
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2BF50356 : 미승인 전표 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2BF55357 : 미승인 전표 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  B2CDAC : 계정코드
    ///  B2DPMK : 작성부서
    ///  B2HISAB : 작성사번
    ///  INQOPTION : 조회구분
    ///  INQOPTION2 : 조회구분
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    /// </summary>
    public partial class TYACBJ001S : TYBase
    {

        #region  Description : 폼 로드 이벤트
        public TYACBJ001S()
        {
            InitializeComponent();
        }

        private void TYACBJ001S_Load(object sender, System.EventArgs e)
        {

            (this.FPS91_TY_S_AC_2BF55357.Sheets[0].Columns[32].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.printer;
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_2BF55357, "BTN");

            this.DTP01_GSTDATE.SetValue(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"));
            this.DTP01_GEDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.CBH01_B2DPMK.DummyValue = this.DTP01_GSTDATE.GetString();

            this.SetStartingFocus(this.DTP01_GSTDATE);

        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sProcedure = string.Empty;

            if (this.TXT01_ARJUNNO.GetValue().ToString().Trim().Length > 0)
            {
                if (this.TXT01_ARJUNNO.GetValue().ToString().Replace("-", "").Trim().Substring(0, 1) == "(")
                {
                    this.TXT01_ARJUNNO.SetValue("");
                }
            }

            this.FPS91_TY_S_AC_2BF55357.Initialize();

            this.DbConnector.CommandClear();

            // 23.02.20 수정 전 소스
            sProcedure = "TY_P_AC_D2KF6613";

            // 23.02.20 수정 전 소스
            //sProcedure = "TY_P_AC_B9TBB589";

            this.DbConnector.Attach(sProcedure.ToString(), this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString(), this.CBH01_B2CDAC.GetValue(), this.CBH02_B2CDAC.GetValue(),
                                                           this.CBO01_INQOPTION.GetValue(), this.CBH01_B2DPMK.GetValue(), this.CBH01_B2HISAB.GetValue(), this.CBO01_INQOPTION2.GetValue(),
                                                           this.TXT01_ARJUNNO.GetValue().ToString().Replace("-", "").Trim(), this.TXT01_B2RKAC.GetValue()
                                                           );

            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_2BF55357.SetValue(dt);

                for (int i = 0; i < this.FPS91_TY_S_AC_2BF55357.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_2BF55357.GetValue(i, "B2NOLN").ToString() != "1")
                    {
                        this.FPS91_TY_S_AC_2BF55357_Sheet1.Cells[i, 32].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    }
                    else
                    {
                        //this.FPS91_TY_S_AC_2BF55357_Sheet1.Cells[i, 28].Image = global::TY.Service.Library.Properties.Resources.magnifier;
                    }
                }

                this.ShowMessage("TY_M_GB_2BF7Y364");
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYACBJ001I(string.Empty, string.Empty, string.Empty)) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : DTP01_GSTDATE_ValueChanged 이벤트
        private void DTP01_GSTDATE_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_B2DPMK.DummyValue = this.DTP01_GSTDATE.GetString();
        }
        #endregion

        #region  Description : DTP01_GSTDATE_ValueChanged 이벤트
        private void FPS91_TY_S_AC_2BF55357_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sB2DPMK = this.FPS91_TY_S_AC_2BF55357.GetValue("JUNPYO").ToString().Substring(0, 6);
            string sB2DTMK = this.FPS91_TY_S_AC_2BF55357.GetValue("JUNPYO").ToString().Substring(7, 8);
            string sB2NOSQ = this.FPS91_TY_S_AC_2BF55357.GetValue("JUNPYO").ToString().Substring(16, 3);


            if (this.OpenModalPopup(new TYACBJ001I(sB2DPMK, sB2DTMK, sB2NOSQ)) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 전표 출력
        private void FPS91_TY_S_AC_2BF55357_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "32")
            {
                if (this.FPS91_TY_S_AC_2BF55357.GetValue("JUNPYO").ToString() != "")
                {
                    string sB2DPMK = this.FPS91_TY_S_AC_2BF55357.GetValue("JUNPYO").ToString().Substring(0, 6);
                    string sB2DTMK = this.FPS91_TY_S_AC_2BF55357.GetValue("JUNPYO").ToString().Substring(7, 8);
                    string sB2NOSQ = this.FPS91_TY_S_AC_2BF55357.GetValue("JUNPYO").ToString().Substring(16, 3);

                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach
                        (
                        "TY_P_AC_2AU2M916",
                        sB2DPMK,
                        sB2DTMK,
                        sB2NOSQ, // 시작 번호
                        sB2NOSQ  // 종료 번호
                        );

                    if (Convert.ToDouble(sB2DTMK.Substring(0, 4)) > 2014)
                    {
                        SectionReport rpt = new TYACBJ0012R();
                        // 세로 출력
                        rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                        DataTable dt = this.DbConnector.ExecuteDataTable();
                        if (dt.Rows.Count > 0)
                        {
                            (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                        }
                    }
                    else
                    {
                        SectionReport rpt = new TYACBJ001R();
                        // 세로 출력
                        rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                        DataTable dt = this.DbConnector.ExecuteDataTable();                                                
                        if (dt.Rows.Count > 0)
                        {
                            (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                        }
                    }

                }

            }
        }
        #endregion

        #region Description : FPS91_TY_S_AC_2BF55357_KeyPress 이벤트
        private void FPS91_TY_S_AC_2BF55357_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {

            if (e.KeyChar == 13)
            {
                this.FPS91_TY_S_AC_2BF55357_CellDoubleClick(null, null);
            }
        }
        #endregion

        #region Description : FPS91_TY_S_AC_2BF55357_KeyPress 이벤트
        private void TXT01_ARJUNNO_Enter(object sender, EventArgs e)
        {
            TXT01_ARJUNNO.SetValue("");
        }
        #endregion

       


    }
}
