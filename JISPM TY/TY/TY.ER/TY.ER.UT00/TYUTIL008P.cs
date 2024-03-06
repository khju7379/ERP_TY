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

namespace TY.ER.UT00
{
    /// <summary>
    /// 탱크세척 요율표 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.07.15 16:51
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// ==========================================6===========================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_67FGU790 : 탱크세척 요율표 출력
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  JLTANK : 탱크번호
    /// </summary>
    public partial class TYUTIL008P : TYBase
    {
        #region Description : 페이지 로드
        public TYUTIL008P()
        {
            InitializeComponent();
        }

        private void TYUTIL008P_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.TXT01_JLTANK);
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_UT_67FGU790", this.TXT01_JLTANK.GetValue().ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYUTIL008R();
                // 세로 출력
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }


        }
        #endregion
    }
}
