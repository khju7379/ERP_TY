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
    /// 관리공통비 출력 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.09.19 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_29J25197 : 관리공통비 출력
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_29J2B198 : 관리공통비 출력
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACNC031S : TYBase
    {
        #region Description : 페이지 로드
        public TYACNC031S()
        {
            InitializeComponent();
        }

        private void TYACNC031S_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.DTP01_GSTYYMM);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sDATE = string.Empty;

            sDATE = "(" + this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4) + "/" + this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2) + ")";
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_29J25197",
                sDATE.ToString(),
                this.DTP01_GSTYYMM.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_29J2B198.SetValue(dt);
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
            string sDATE = string.Empty;

            sDATE = "(" + this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4) + "/" + this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2) + ")";
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_29J25197",
                sDATE.ToString(),
                this.DTP01_GSTYYMM.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYACNC031R();

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
    }
}