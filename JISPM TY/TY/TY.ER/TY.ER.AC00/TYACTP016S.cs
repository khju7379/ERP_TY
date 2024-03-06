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

namespace TY.ER.AC00
{
    /// <summary>
    /// 원천세 일용근로 명세서 조회 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2014.12.11 17:57
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_4CBHX739 : 명세서 일용근로소득 조회(집계)
    ///  TY_P_AC_4CBHZ740 : 명세서 일용근로소득 조회(상세-항운노조)
    ///  TY_P_AC_53IGS704 : 명세서 일용근로소득 조회(상세-일용자)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_4CBHW737 : 명세서 일용근로소득 조회(집계)
    ///  TY_S_AC_4CBHZ741 : 명세서 일용근로소득 조회(상세)
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  GBUNGI : 분기
    ///  WABRANCH : 지점구분
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    ///  WREYYMM : 귀속년월
    /// </summary>
    public partial class TYACTP016S : TYBase
    {

        #region Description : 페이지 로드
        public TYACTP016S()
        {
            InitializeComponent();
        }

        private void TYACTP016S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_INQ.ProcessCheck += new TButton.CheckHandler(BTN61_INQ_ProcessCheck);

            this.FPS91_TY_S_AC_4CBHZ741.Initialize();
            this.DTP01_GSTYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            this.DTP01_GEDYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.UP_Set_JuminAuthCheck(CBO01_INQ_AUTH);

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.CBO01_WAGUNMU);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sREYYMMDD = string.Empty;
            string sSTYYMM = string.Empty;
            string sEDYYMM = string.Empty;
            string sYear  = string.Empty;
            string sMonth = string.Empty;

            int iDD = 0;

            sYear = this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 4);
            sMonth = this.DTP01_GEDYYMM.GetValue().ToString().Substring(4, 2);
            // 해당월 마지막 일자 가져오기
            iDD = DateTime.DaysInMonth(int.Parse(sYear.ToString()), int.Parse(sMonth.ToString()));
            sREYYMMDD = this.DTP01_GEDYYMM.GetValue().ToString() + Convert.ToString(iDD);

            this.FPS91_TY_S_AC_4CBHW737.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_4CBHX739", this.CBO01_WAGUNMU.GetValue().ToString(),
                                    sREYYMMDD, this.DTP01_GSTYYMM.GetValue(), this.DTP01_GEDYYMM.GetValue(), TYUserInfo.SecureKey, CBO01_INQ_AUTH.GetValue().ToString()) ;
             DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_4CBHW737.SetValue(dt);
            }

        }
        #endregion

        #region Description : 명세서 일용근로소득 조회(상세)
        private void UP_SET_LIST(string sGUBN, string sSYYMM, string sEYYMM)
        {

            string sREYYMMDD = string.Empty;
            string sSTYYMM = string.Empty;
            string sEDYYMM = string.Empty;
            string sYear = string.Empty;
            string sMonth = string.Empty;
            string sProcedid = string.Empty;
            int iDD = 0;

            sYear = this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 4);
            sMonth = this.DTP01_GEDYYMM.GetValue().ToString().Substring(4, 2);
            // 해당월 마지막 일자 가져오기
            iDD = DateTime.DaysInMonth(int.Parse(sYear.ToString()), int.Parse(sMonth.ToString()));
            sREYYMMDD = this.DTP01_GEDYYMM.GetValue().ToString() + Convert.ToString(iDD);

            if (sGUBN == "O01")  //항운노조
            {                
                DataTable dt = new DataTable();
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_AC_4CBHZ740", this.CBO01_WAGUNMU.GetValue().ToString(), sREYYMMDD, sSYYMM, sEYYMM, TYUserInfo.SecureKey, CBO01_INQ_AUTH.GetValue().ToString() );
                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_AC_4CBHZ741.SetValue(dt);
            }
            else if (sGUBN == "O02") // 일용자
            {                
                DataTable dt = new DataTable();
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_AC_53IGS704", this.CBO01_WAGUNMU.GetValue().ToString(), sREYYMMDD, sSYYMM, sEYYMM, TYUserInfo.SecureKey, CBO01_INQ_AUTH.GetValue().ToString());
                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_AC_4CBHZ741.SetValue(dt);
            }
           
        }
        #endregion
        
        #region Description : 조회 CHECK
        private void BTN61_INQ_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sREYYMMDD = string.Empty;
            string sSTYYMM = string.Empty;
            string sEDYYMM = string.Empty;
            string sYear = string.Empty;
            string sMonth = string.Empty;

            int iDD = 0;

            sYear = this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 4);
            sMonth = this.DTP01_GEDYYMM.GetValue().ToString().Substring(4, 2);
            // 해당월 마지막 일자 가져오기
            iDD = DateTime.DaysInMonth(int.Parse(sYear.ToString()), int.Parse(sMonth.ToString()));
            sREYYMMDD = this.DTP01_GEDYYMM.GetValue().ToString() + Convert.ToString(iDD);

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_4CBHX739", this.CBO01_WAGUNMU.GetValue().ToString(), sREYYMMDD, this.DTP01_GSTYYMM.GetValue(), this.DTP01_GEDYYMM.GetValue(),TYUserInfo.SecureKey, "Y");

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count == 0)
            {
                this.ShowCustomMessage("자료가 존재하지 않습니다.", "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : Spread CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_4CBHW737_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

            UP_SET_LIST(this.FPS91_TY_S_AC_4CBHW737.GetValue("INCOME").ToString() ,
                        this.FPS91_TY_S_AC_4CBHW737.GetValue("JIYYMM").ToString(),
                        this.FPS91_TY_S_AC_4CBHW737.GetValue("JIYYMM").ToString());
        }
        #endregion
    }
}