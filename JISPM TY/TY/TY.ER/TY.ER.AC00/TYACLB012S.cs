using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.AC00
{
    /// <summary>
    /// 사원별예산실적집계표 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.08.29 14:27
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_28T2J628 : 사원별예산실적집계표
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_28T3B629 : 사원별예산실적집계표
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  GCDDP : 사업장코드
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACLB012S : TYBase
    {
        #region Description : 페이지 로드
        public TYACLB012S()
        {
            InitializeComponent();
        }

        private void TYACLB012S_Load(object sender, System.EventArgs e)
        {
            this.CBH01_GCDDP.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            SetStartingFocus(this.DTP01_GSTYYMM);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_28T2J628",
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GEDYYMM.GetValue(),
                this.CBH01_GCDDP.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_28T3B629.SetValue(dt);

                // 특정 ROW 색깔 입히기
                for (int i = 0; i < this.FPS91_TY_S_AC_28T3B629.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_28T3B629.GetValue(i, "B2HISAB").ToString() == "합   계")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_28T3B629.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
                    }
                }
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_28T2J628",
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GEDYYMM.GetValue(),
                this.CBH01_GCDDP.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYACLB012R();

                // 가로 출력
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        }
        #endregion

        #region Description : 발생년월 이벤트
        private void DTP01_GSTYYMM_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_GCDDP.DummyValue = this.DTP01_GSTYYMM.GetValue() + "01";
        }
        #endregion
    }
}