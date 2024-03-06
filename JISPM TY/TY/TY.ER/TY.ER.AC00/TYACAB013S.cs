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
using GrapeCity.ActiveReports.SectionReportModel;

namespace TY.ER.AC00
{
    /// <summary>
    /// 입금표 대장 출력 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.06.14 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_26E95861 : 입금표 대장 조회 및 출력
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_26E9R865 : 입금표 대장 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  ARCDSB :  수령자
    ///  ARLOCAL :  지　　역
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    ///  ARYEAR :  년　　도
    ///  EDSEQ : 발행순번
    ///  STSEQ : 발행순번
    /// </summary>
    public partial class TYACAB013S : TYBase
    {
        #region Description : 페이지 로드
        public TYACAB013S()
        {
            InitializeComponent();
        }

        private void TYACAB013S_Load(object sender, System.EventArgs e)
        {
            this.TXT01_ARYEAR.SetValue(DateTime.Now.ToString("yyyyMMdd").Substring(0, 4));

            // 부서
            this.CBH01_ARDPMK.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            SetStartingFocus(this.TXT01_ARYEAR);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_26E95861",
                this.TXT01_ARYEAR.GetValue().ToString(),
                this.CBH01_ARDPMK.GetValue().ToString(),
                this.TXT01_STSEQ.GetValue().ToString(),
                this.TXT01_EDSEQ.GetValue().ToString(),
                this.CBH01_ARCDSB.GetValue().ToString(),
                this.DTP01_STDATE.GetValue().ToString().Replace("-", ""),
                this.DTP01_EDDATE.GetValue().ToString().Replace("-", "")
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_26E9R865.SetValue(dt);
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_26E95861",
                this.TXT01_ARYEAR.GetValue().ToString(),
                this.CBH01_ARDPMK.GetValue().ToString(),
                this.TXT01_STSEQ.GetValue().ToString(),
                this.TXT01_EDSEQ.GetValue().ToString(),
                this.CBH01_ARCDSB.GetValue().ToString(),
                this.DTP01_STDATE.GetValue().ToString().Replace("-", ""),
                this.DTP01_EDDATE.GetValue().ToString().Replace("-", "")
                );

            SectionReport rpt = new TYACAB013R();

            rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

            (new TYERGB001P(rpt, this.DbConnector.ExecuteDataTable())).ShowDialog();
        }
        #endregion

        private void TXT01_ARYEAR_TextChanged(object sender, EventArgs e)
        {
            // 부서
            if (TXT01_ARYEAR.GetValue().ToString() != "")
            {
                this.CBH01_ARDPMK.DummyValue = TXT01_ARYEAR.GetValue() + "0101";
            }
            else
            {
                this.CBH01_ARDPMK.DummyValue = DateTime.Now.ToString("yyyyMMdd");
            }
        }
    }
}