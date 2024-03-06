using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 접대비 사용내역 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.04.13 17:11
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_24D5G651 : 접대비 예산실적 조회
    ///  TY_P_AC_24G3S686 : 접대비 예산상세내역 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_24G3M682 : 접대비 예산실적 조회
    ///  TY_S_AC_24G3T688 : 접대비 예산상세내역 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  Y2CDDP : 예산부서
    ///  YYYYMM : 기준 년월
    /// </summary>
    public partial class TYACLB003S : TYBase
    {
        #region Description : 폼 Load
        public TYACLB003S()
        {
            InitializeComponent();
        }

        private void TYACLB003S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_YYYYMM.SetValue(DateTime.Now.ToString("yyyyMM"));
            this.CBH01_Y2CDDP.DummyValue = this.DTP01_YYYYMM.GetString().Substring(0,6) + "01";

            this.DTP01_YYYYMM_ValueChanged(null, null);

            this.SetStartingFocus(this.DTP01_YYYYMM); 

        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_24G3M682.Initialize();
            this.DbConnector.CommandClear();
            
            this.DbConnector.Attach("TY_P_AC_24D5G651", this.DTP01_YYYYMM.GetString().ToString().Substring(0,6),
                                                        this.CBH01_Y2CDDP.GetValue());
            this.FPS91_TY_S_AC_24G3M682.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : Spread  CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_24G3M682_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {          

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_24G3S686", this.FPS91_TY_S_AC_24G3M682.GetValue("Y2YEAR").ToString(),
                                                        this.FPS91_TY_S_AC_24G3M682.GetValue("Y2MONTH").ToString(),
                                                        this.FPS91_TY_S_AC_24G3M682.GetValue("Y2CDDP").ToString(),
                                                        this.FPS91_TY_S_AC_24G3M682.GetValue("Y2CDSB").ToString(),
                                                        this.FPS91_TY_S_AC_24G3M682.GetValue("Y2CDAC").ToString());
            this.FPS91_TY_S_AC_24G3T688.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : DTP01_YYYYMM_ValueChanged 이벤트
        //private void DTP01_YYYYMM_ValueChanged(object sender, EventArgs e)
        //{
        //    this.CBH01_Y2CDDP.DummyValue = this.DTP01_YYYYMM.GetString().ToString().Substring(0, 6) + "01";
        //}
        #endregion

        #region Description : Spread  CellDoubleClick 이벤트 (미승인전표 조회)
        private void FPS91_TY_S_AC_24G3T688_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

            if (e.Column.ToString() == "2")
            {
                if (this.FPS91_TY_S_AC_24G3T688.GetValue("Y3JPNO").ToString().Trim() == "")
                {
                    this.ShowMessage("TY_M_MR_2BF8A365");
                }
                else
                {
                    string sB2DPMK = this.FPS91_TY_S_AC_24G3T688.GetValue("Y3JPNO").ToString().Substring(0, 6);
                    string sB2DTMK = this.FPS91_TY_S_AC_24G3T688.GetValue("Y3JPNO").ToString().Substring(7, 8);
                    string sB2NOSQ = this.FPS91_TY_S_AC_24G3T688.GetValue("Y3JPNO").ToString().Substring(16, 3);

                    if (this.OpenModalPopup(new TYACBJ001I(sB2DPMK, sB2DTMK, sB2NOSQ)) == System.Windows.Forms.DialogResult.OK)
                    {
                    }
                }
            }

        } 
        #endregion

        private void DTP01_YYYYMM_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_Y2CDDP.DummyValue = this.DTP01_YYYYMM.GetString().ToString().Substring(0, 6) + "01";
        }
    }
}
