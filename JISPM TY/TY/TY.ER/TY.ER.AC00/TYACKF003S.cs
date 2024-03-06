using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 자금계획관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.12.27 14:07
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_3139H459 : 자금계획-부서코드 가져오기
    ///  TY_P_AC_313B0461 : 자금계획관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_313B1463 : 자금계획관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  PHDPAC : 부　　서
    ///  PHSABUN : 사　　번
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    /// </summary>
    public partial class TYACKF003S : TYBase
    {
        #region Description : 페이지 로드
        public TYACKF003S()
        {
            InitializeComponent();
        }

        private void TYACKF003S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_GSTDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));

            this.DTP01_GEDDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));

            this.CBH01_PHSABUN.SetValue(TYUserInfo.EmpNo);
            
            // 부서코드
            this.CBH01_PHDPAC.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            SetStartingFocus(this.DTP01_GSTDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3139H459",
                this.DTP01_GSTDATE.GetString(),
                this.CBH01_PHSABUN.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["KBJKCD"].ToString() != "01")
                {
                    if (dt.Rows[0]["KBBUSEO"].ToString().Substring(0, 1) != "A")
                    {
                        if (dt.Rows[0]["KBBUSEO"].ToString() != this.CBH01_PHDPAC.GetValue().ToString())
                        {
                            this.ShowMessage("TY_M_AC_3131B466");
                            return;
                        }
                    }
                }
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_313B0461",
                this.DTP01_GSTDATE.GetValue(),
                this.DTP01_GEDDATE.GetValue(),
                this.CBH01_PHDPAC.GetValue().ToString(),
                this.CBH01_PHSABUN.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_313B1463.SetValue(dt);
            }
            else
            {
                this.FPS91_TY_S_AC_313B1463.SetValue(dt);
            }
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYACKF003I(string.Empty, string.Empty, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            // 삭제 프로시저
            this.DbConnector.Attach("TY_P_AC_31325468", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_313B1463.GetDataSourceInclude(TSpread.TActionType.Remove, "PHDPAC", "PHSABUN", "PHNOSQ");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        private void DTP01_GSTDATE_ValueChanged(object sender, EventArgs e)
        {
            // 부서코드
            this.CBH01_PHDPAC.DummyValue = this.DTP01_GSTDATE.GetValue().ToString();
        }

        #region Description : 스프레드 클릭 이벤트
        private void FPS91_TY_S_AC_313B1463_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            // 파라미터값 보내기
            if ((new TYACKF003I(this.FPS91_TY_S_AC_313B1463.GetValue("PHDPAC").ToString(), this.FPS91_TY_S_AC_313B1463.GetValue("PHSABUN").ToString(), this.FPS91_TY_S_AC_313B1463.GetValue("PHNOSQ").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}