using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 주요설비투자현황 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.07.23 10:06
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_37N2H215 : EIS 주요설비투자현황 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_37N2J217 : EIS 주요설비투자현황 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  INQOPTION : 조회구분
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACPO005S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYACPO005S()
        {
            InitializeComponent();
        }

        private void TYACPO005S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_GSTYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            this.SetStartingFocus(this.DTP01_GSTYYMM);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_37N2J217.Initialize(); 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_37N2H215", this.DTP01_GSTYYMM.GetString().ToString().Substring(4,2), this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 4), this.CBO01_INQOPTION.GetValue() );
            this.FPS91_TY_S_AC_37N2J217.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_37N2J217.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_AC_37N2J217.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_37N2J217.GetValue(i, "P2SEQ").ToString() != "99" )
                    {
                        this.FPS91_TY_S_AC_37N2J217_Sheet1.Cells[i, 7].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    }
                }

                this.SetSpreadSumRow(this.FPS91_TY_S_AC_37N2J217, "P2CDDP", "소 계", SumRowType.SubTotal);
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_37N2J217, "P2CDDP", "합 계", SumRowType.Total);
            }

        }
        #endregion

        #region  Description : FPS91_TY_S_AC_37N2J217_ButtonClicked 이벤트
        private void FPS91_TY_S_AC_37N2J217_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "7")
            {
                string sP2YEAR = this.FPS91_TY_S_AC_37N2J217.GetValue("P2YEAR").ToString() + this.DTP01_GSTYYMM.GetString().ToString().Substring(4, 2);
                string sP2CDDP = this.FPS91_TY_S_AC_37N2J217.GetValue("P2CDDP").ToString();
                string sP2CDAC = this.FPS91_TY_S_AC_37N2J217.GetValue("P2CDAC").ToString();

                if (this.OpenModalPopup(new TYACPO005P(sP2YEAR, sP2CDDP, sP2CDAC)) == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null); 
                  
            }
        }
        #endregion
    }
}
