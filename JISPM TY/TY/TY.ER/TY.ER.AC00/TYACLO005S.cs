using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.ER.GB00;
using GrapeCity.ActiveReports;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using FarPoint.Win.Spread.CellType;

namespace TY.ER.AC00
{
    /// <summary>
    /// 차입금 잔액조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2018.07.03 13:11
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_873I5320 : 차입금 잔액조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_87GHV403 : 차입금 잔액조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  LOCCURRTYPE : 통화유형
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYACLO005S : TYBase
    {
        #region Description : 폼 로드
        public TYACLO005S()
        {
            InitializeComponent();
        }

        private void TYACLO005S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STYYMM.SetValue(System.DateTime.Now.AddYears(-1).ToString("yyyy-MM"));
            this.DTP01_EDYYMM.SetValue(System.DateTime.Now.ToString("yyyy-MM"));

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_87GHV402", Get_Date(this.DTP01_STYYMM.GetValue().ToString()),
                                                        Get_Date(this.DTP01_EDYYMM.GetValue().ToString()),
                                                        this.CBH01_LOCBANKCD.GetValue().ToString(),
                                                        this.CBH01_LOCCURRTYPE.GetValue().ToString(),
                                                        this.CBH01_LOCLOANTYPE.GetValue().ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_87GHV403.SetValue(dt);

            string sSTATUSNM = string.Empty;

            for (int i = 0; i < this.FPS91_TY_S_AC_87GHV403.ActiveSheet.RowCount; i++)
            {
                sSTATUSNM = this.FPS91_TY_S_AC_87GHV403.GetValue(i, "STATUSNM").ToString();

                if (sSTATUSNM.ToString() == "실행" || sSTATUSNM.ToString() == "상환" || sSTATUSNM.ToString() == "유동성 대체" || sSTATUSNM.ToString() == "유동성 재대체")
                {
                    this.FPS91_TY_S_AC_87GHV403_Sheet1.Cells[i, 3].Font = new Font("굴림", 9, FontStyle.Bold);
                }

                if (sSTATUSNM.ToString() == "실행")
                {
                    this.FPS91_TY_S_AC_87GHV403_Sheet1.Cells[i, 3].ForeColor = Color.Blue;
                }
                else if (sSTATUSNM.ToString() == "상환")
                {
                    this.FPS91_TY_S_AC_87GHV403_Sheet1.Cells[i, 3].ForeColor = Color.Red;
                }
                else if (sSTATUSNM.ToString() == "유동성 대체")
                {
                    this.FPS91_TY_S_AC_87GHV403_Sheet1.Cells[i, 3].ForeColor = Color.LimeGreen;
                }
                else if (sSTATUSNM.ToString() == "유동성 재대체")
                {
                    this.FPS91_TY_S_AC_87GHV403_Sheet1.Cells[i, 3].ForeColor = Color.Peru;
                }
            }
        }
        #endregion
    }
}
