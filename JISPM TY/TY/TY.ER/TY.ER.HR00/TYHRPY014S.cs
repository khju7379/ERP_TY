using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using System.Drawing;

namespace TY.ER.HR00
{
    /// <summary>
    /// 급여변동사항 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.03.25 11:55
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_53PBW886 : 급여변동사항 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_53PBY887 : 급여변동사항 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  SEL : 선택
    ///  PAYGUBN : 급여구분
    ///  PAYJIDATE : 지급일자
    ///  PAYYYMM : 급여년월
    /// </summary>
    public partial class TYHRPY014S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRPY014S()
        {
            InitializeComponent();
        }

        private void TYHRPY014S_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_53PBY887, "KBSABUN");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_53PBY887, "KBHANGL");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_53PBY887, "BTNDETAIL");

            (this.FPS91_TY_S_HR_53PBY887.Sheets[0].Columns[16].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.application_view_detail;

            DTP01_PAYYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            DTP01_PAYJIDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.SetStartingFocus(CBH01_PAYGUBN);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {

            string sPrePAYYYMM = string.Empty;
            string sPrePAYJIDATE = string.Empty;

            //직전 급여정보 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_A4LGQ319", this.CBH01_PAYGUBN.GetValue().ToString(), this.DTP01_PAYYYMM.GetString().Substring(0, 6), this.DTP01_PAYJIDATE.GetString());
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                sPrePAYYYMM = dt.Rows[0]["PAYYYMM"].ToString();
                sPrePAYJIDATE = dt.Rows[0]["PAYJIDATE"].ToString();
            }
            else
            {
                sPrePAYYYMM = this.DTP01_PAYYYMM.GetString().Substring(0, 6);
                sPrePAYJIDATE = this.DTP01_PAYJIDATE.GetString();
            }

            this.FPS91_TY_S_HR_53PBY887.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_53PBW886", this.DTP01_PAYYYMM.GetString().Substring(0, 6), this.CBH01_PAYGUBN.GetValue().ToString(), this.DTP01_PAYJIDATE.GetString(),
                                                        sPrePAYYYMM,
                                                        this.CBH01_PAYGUBN.GetValue().ToString(),
                                                        sPrePAYJIDATE
                                                        );
            this.FPS91_TY_S_HR_53PBY887.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_HR_53PBY887.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_HR_53PBY887, "KBJJCDNM", "[합   계]", SumRowType.Sum, "JUNPMPAYTOTAL", "DANGPMPAYTOTAL", "GAP");
            }

            for (int i = 0; i < this.FPS91_TY_S_HR_53PBY887.ActiveSheet.RowCount; i++)
            {
                if ( Convert.ToDouble(this.FPS91_TY_S_HR_53PBY887.GetValue(i, "GAP").ToString()) != 0 )
                {
                    this.FPS91_TY_S_HR_53PBY887_Sheet1.Cells[i, 15].ForeColor = Color.Red;
                    this.FPS91_TY_S_HR_53PBY887_Sheet1.Cells[i, 15].Font = new Font("굴림", 9, FontStyle.Bold);                    
                }
            }

        }
        #endregion

        #region  Description : 선택 버튼 이벤트
        private void BTN61_SEL_Click(object sender, EventArgs e)
        {
            TYHRPY006P popup = new TYHRPY006P();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DTP01_PAYYYMM.SetValue(popup.fsPAYYYMM);
                this.CBH01_PAYGUBN.SetValue(popup.fsPAYGUBN);
                this.DTP01_PAYJIDATE.SetValue(popup.fsPAYJIDATE);
            }

            this.CBH01_PAYGUBN.Focus();
        }
        #endregion

        #region  Description : FPS91_TY_S_HR_53PBY887_ButtonClicked 이벤트
        private void FPS91_TY_S_HR_53PBY887_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "16")
            {
                if ((new TYHRPY014P(this.FPS91_TY_S_HR_53PBY887.GetValue("PYDATE").ToString(),
                                     this.FPS91_TY_S_HR_53PBY887.GetValue("PYGUBN").ToString(),
                                     this.FPS91_TY_S_HR_53PBY887.GetValue("PYJIDATE").ToString(),
                                     this.FPS91_TY_S_HR_53PBY887.GetValue("KBSABUN").ToString()
                                     )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    return;
            }
        }
        #endregion

        #region  Description : 옵션 버튼 이벤트
        private void BTN61_INQOPTION_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYHRPY14C1()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}
