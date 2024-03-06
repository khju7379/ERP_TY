using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;
using TY.Service.Library.Controls.TYSpreadCellType;
using DataDynamics.ActiveReports;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using TY.ER.GB00;
using TY.ER.AC00;

namespace TY.ER.US00
{
    /// <summary>
    /// 선급자재 관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2013.02.19 09:59
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// </summary>
    public partial class TYUSME025S : TYBase
    {
        #region Description : 페이지 로드
        public TYUSME025S()
        {
            InitializeComponent();
        }

        private void TYUSME025S_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_US_95SBU639.Initialize();

            this.DTP01_STDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));
            this.DTP01_EDDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));

            UP_Spread_Load();

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sProcedure = string.Empty;

            if (this.CBO01_GGUBUN.GetValue().ToString() == "1")
            {
                // 화주별
                sProcedure = "TY_P_US_95SBU638";
            }
            else
            {
                // 협회별
                sProcedure = "TY_P_US_95SBT637";
            }

            // 스프레드 타이틀 변경
            UP_Spread_Load();

            DataTable dt = new DataTable();

            this.DbConnector.Attach
                (
                sProcedure.ToString(),
                Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                Get_Date(this.DTP01_EDDATE.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_95SBU639.SetValue(dt);

            this.SpreadSumRowAdd(this.FPS91_TY_S_US_95SBU639, "GUBUN2", "합  계", SumRowType.Sum, "USAMT", "HYAMT", "BKAMT", "LMAMT", "EXAMT", "ISAMT", "TOTAL");            
        }
        #endregion

        #region Description : 화주 및 협회 구분 이벤트
        private void CBO01_GGUBUN_SelectedIndexChanged(object sender, EventArgs e)
        {
            UP_Spread_Load();
        }
        #endregion

        #region Description : 스프레드 로드
        private void UP_Spread_Load()
        {
            this.FPS91_TY_S_US_95SBU639.Initialize();

            if (this.CBO01_GGUBUN.GetValue().ToString() == "1")
            {
                this.FPS91_TY_S_US_95SBU639_Sheet1.ColumnHeader.Cells[0, 1].Value = "화 주";
            }
            else
            {
                this.FPS91_TY_S_US_95SBU639_Sheet1.ColumnHeader.Cells[0, 1].Value = "협 회";
            }
        }
        #endregion
    }
}