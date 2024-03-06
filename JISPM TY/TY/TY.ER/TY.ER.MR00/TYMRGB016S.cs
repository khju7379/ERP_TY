using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.MR00
{
    /// <summary>
    /// 코드박스 - 투자수선예산 조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.11.08 10:56
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_2B88B213 : 코드박스 - 기타세목예산(투자&수선) 조회
    ///  TY_P_MR_2B88C214 : 코드박스 - 기타세목예산(투자&수선) 월 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_2B88D215 : 코드박스 - 기타세목예산(투자&수선) 조회
    ///  TY_S_MR_2B88E216 : 코드박스 - 예산(투자&수선, 소모품비, 기타) 월 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  GCDDP : 사업장코드
    ///  GDATE : 일자
    /// </summary>
    public partial class TYMRGB016S : TYBase
    {
        public string fsCDAC   = string.Empty;
        public string fsCDACNM = string.Empty;
        public string fsSEQ    = string.Empty;

        #region Description : 페이지 로드
        public TYMRGB016S(string sDATE, string sCDDP, string sCDDPNM)
        {
            InitializeComponent();

            this.SetPopupStyle();

            this.TXT01_GDATE.SetValue(sDATE);

            this.TXT01_GCDDP.SetValue(sCDDP);
            this.TXT01_GCDDPNM.SetValue(sCDDPNM);

            SetStartingFocus(this.TXT01_GCDAC);
        }

        private void TYMRGB016S_Load(object sender, System.EventArgs e)
        {
            this.TXT01_GDATE.SetReadOnly(true);
            this.TXT01_GCDDP.SetReadOnly(true);
            this.TXT01_GCDDPNM.SetReadOnly(true);

            BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_MR_2B88D215.Initialize();
            this.FPS91_TY_S_MR_2B88E216.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
               (
               "TY_P_MR_32P9W167",
               this.TXT01_GDATE.GetValue(),
               this.TXT01_GCDDP.GetValue(),
               this.TXT01_GCDAC.GetValue()
               );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_MR_2B88D215.SetValue(dt);
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 기타세목예산(투자&수선) 조회
        private void FPS91_TY_S_MR_2B88D215_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            fsCDAC   = "";
            fsCDACNM = "";
            fsSEQ    = "";

            fsCDAC = this.FPS91_TY_S_MR_2B88D215.GetValue("P1CDAC").ToString();

            if (fsCDAC.ToString() == "11100888")
            {
                fsCDAC   = this.FPS91_TY_S_MR_2B88D215.GetValue("P1CDAC").ToString();
                fsCDACNM = this.FPS91_TY_S_MR_2B88D215.GetValue("A1NMAC").ToString();
                fsSEQ    = this.FPS91_TY_S_MR_2B88D215.GetValue("P1SEQ").ToString();

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
            {
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                   (
                   "TY_P_MR_2B88C214",
                   this.TXT01_GDATE.GetValue(),
                   this.TXT01_GCDDP.GetValue(),
                   this.FPS91_TY_S_MR_2B88D215.GetValue("P1CDAC").ToString(),
                   this.FPS91_TY_S_MR_2B88D215.GetValue("P1SEQ").ToString()
                   );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.FPS91_TY_S_MR_2B88E216.SetValue(dt);

                    fsCDAC   = this.FPS91_TY_S_MR_2B88D215.GetValue("P1CDAC").ToString();
                    fsCDACNM = this.FPS91_TY_S_MR_2B88D215.GetValue("A1NMAC").ToString();
                    fsSEQ    = this.FPS91_TY_S_MR_2B88D215.GetValue("P1SEQ").ToString();
                }
            }
        }
        #endregion

        #region Description : 기타세목예산(투자&수선) 월 조회
        private void FPS91_TY_S_MR_2B88E216_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion
    }
}
