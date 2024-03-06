using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using FarPoint.Win.Spread.CellType;
using System.Drawing; 

namespace TY.ER.AF00
{
    /// <summary>
    /// EIS 품목별매출현황 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.07.10 14:52
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_37A2X065 : EIS 품목별매출현황 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_37A35071 : EIS 품목별매출현황 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  ERSCDDP : 사업부
    ///  ERSYYMM : 년월
    /// </summary>
    public partial class TYAFMA009S : TYBase
    {
        private string fsCompanyCode = string.Empty;
        private string fsTab = string.Empty;

        #region Description : 폼 로드 이벤트
        public TYAFMA009S()
        {
            InitializeComponent();
        }

        private void TYAFMA009S_Load(object sender, System.EventArgs e)
        {
            switch (TYUserInfo.EmpNo.Substring(0, 2))
            {
                case "HT":
                    fsCompanyCode = "TH";
                    break;
                case "TG":
                    fsCompanyCode = "TG";
                    break;
                case "TS":
                    fsCompanyCode = "TS";
                    break;
                case "TL":
                    fsCompanyCode = "TL";
                    break;
                default:
                    fsCompanyCode = "";
                    break;
            }

            if (fsCompanyCode != "")
            {
                this.CBH01_ECFMCUST.SetValue(fsCompanyCode);
                this.CBH01_ECFMCUST.SetReadOnly(true);
            }

            this.TXT01_ECFMYYHD.SetValue(DateTime.Now.ToString("yyyy"));

            if (fsCompanyCode != "")
            {
                this.SetStartingFocus(this.TXT01_ECFMYYHD);
            }
            else
            {
                this.SetStartingFocus(this.CBH01_ECFMCUST.CodeText);
            }

            fsTab = "Tab1";
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_3A84H012.Initialize();
            this.FPS91_TY_S_AC_3A84E010.Initialize();

            DataTable dt = new DataTable();

            if (fsTab.ToString() == "Tab1") // 손익현황
            {
                // 태영그레인DB
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3A84D008",
                    this.CBH01_ECFMCUST.GetValue(),
                    this.TXT01_ECFMYYHD.GetValue()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_AC_3A84H012.SetValue(dt);

                this.FPS91_TY_S_AC_3A84H012.ActiveSheet.Columns["TOTAMT"].BackColor = Color.FromArgb(254, 209, 164);
            }
            else // 재무상태표
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3A84E009",
                    this.CBH01_ECFMCUST.GetValue(),
                    this.TXT01_ECFMYYHD.GetValue()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_AC_3A84E010.SetValue(dt);
            }
        }
        #endregion

        #region Description : 탭 이벤트
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0) // 손익현황
            {
                fsTab = "Tab1";
            }
            else // 재무상태표
            {
                fsTab = "Tab2";
            }

            this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}