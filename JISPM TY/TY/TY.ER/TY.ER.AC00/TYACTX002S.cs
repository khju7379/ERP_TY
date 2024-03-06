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

namespace TY.ER.AC00
{
    /// <summary>
    /// 세무구분별 매입명세서 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.05.14 16:13
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_25EAH372 : 세무구분별 매입명세서 조회
    ///  TY_P_AC_25G19489 : 세무구분별 매입명세서 출력
    ///  TY_P_AC_25H3V532 : 세무구분별 매입명세서 집계표
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_25E4Y431 : 세무구분별 매입명세서
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_25GAZ484 : 세무 구분을 선택하세요.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  VNCODE : 거래처코드
    ///  B4VLMI1 : 관리항목값１
    ///  B4VLMI2 : 관리항목값２
    ///  B4VLMI4 : 관리항목값４
    ///  GDATEGUBUN : 일자구분
    ///  CBO01_GPRTGN : 출력구분
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACTX002S : TYBase
    {
        #region Description : 페이지 로드
        public TYACTX002S()
        {
            InitializeComponent();
        }

        private void TYACTX002S_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.DTP01_GSTYYMM);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sSTYYMM  = this.DTP01_GSTYYMM.GetValue().ToString();
            string sEDYYMM  = this.DTP01_GEDYYMM.GetValue().ToString();
            string sB4VLMI2 = this.CBO01_B4VLMI2.GetValue().ToString();

            if (sB4VLMI2.ToString() == "1")
            {
                sB4VLMI2 = "11103101";
            }
            else
            {
                sB4VLMI2 = "11103102";
            }

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3BK4K389",
                sSTYYMM.ToString(),
                sEDYYMM.ToString(),
                sB4VLMI2.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_3BK4K390.SetValue(dt);

                if (this.FPS91_TY_S_AC_3BK4K390.CurrentRowCount > 0)
                {
                    this.SpreadSumRowAdd(this.FPS91_TY_S_AC_3BK4K390, "B4VLMI2", "합 계", SumRowType.Total, "V3AMT", "V3VAT");
                }

                //// 특정 ROW 색깔 입히기
                //for (int i = 0; i < this.FPS91_TY_S_AC_3BK4K390.ActiveSheet.RowCount; i++)
                //{
                //    if (this.FPS91_TY_S_AC_3BK4K390.GetValue(i, "VNSANGHO").ToString() == "소계")
                //    {
                //        // 특정 ROW 색깔 입히기
                //        this.FPS91_TY_S_AC_3BK4K390.ActiveSheet.Rows[i].BackColor = Color.FromArgb(228, 242, 194);
                //    }

                //    if (this.FPS91_TY_S_AC_3BK4K390.GetValue(i, "VNSANGHO").ToString() == "총계")
                //    {
                //        // 특정 ROW 색깔 입히기
                //        this.FPS91_TY_S_AC_3BK4K390.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                //    }
                //}
            }
            else
            {
                this.FPS91_TY_S_AC_3BK4K390.SetValue(dt);

                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        }
        #endregion

        #region Description : 미승인 전표 화면 띄우기
        private void FPS91_TY_S_AC_3BK4K390_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column.ToString() == "4")
            {
                string sB2DPMK = this.FPS91_TY_S_AC_3BK4K390.GetValue("B4NOJP").ToString().Substring(0, 6);
                string sB2DTMK = this.FPS91_TY_S_AC_3BK4K390.GetValue("B4NOJP").ToString().Substring(6, 8);
                string sB2NOSQ = this.FPS91_TY_S_AC_3BK4K390.GetValue("B4NOJP").ToString().Substring(14, 3);

                if (this.OpenModalPopup(new TYACBJ001I(sB2DPMK, sB2DTMK, sB2NOSQ)) == System.Windows.Forms.DialogResult.OK)
                   this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion
    }
}