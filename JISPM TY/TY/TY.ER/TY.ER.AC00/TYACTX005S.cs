using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 부가세 옵션 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2013.11.22 17:19
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_3BM5I426 : 부가세 옵션 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_3BM5J428 : 부가세 옵션 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_2BF7Y364 : 조회가 완료되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  BADVEND : 거래처
    ///  INQOPTION : 조회구분
    ///  INQOPTION2 : 조회구분
    ///  VNGUBUN : 구분
    ///  PRYEAR : 년도
    /// </summary>
    public partial class TYACTX005S : TYBase
    {
        #region Description : 페이지 로드
        public TYACTX005S()
        {
            InitializeComponent();
        }

        private void TYACTX005S_Load(object sender, System.EventArgs e)
        {
            UP_COMBOBOX("");
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.TXT01_PRYEAR.SetValue(DateTime.Now.ToString("yyyy"));
            this.SetStartingFocus(this.TXT01_PRYEAR);
        }
        #endregion

        #region Description : 조회버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {   
            this.FPS91_TY_S_AC_3BM5J428.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
            (
            "TY_P_AC_3BM5I426",
            TXT01_PRYEAR.GetValue(),
            CBO01_VNGUBUN.GetValue(),
            CBO01_BADVEND.GetValue(),
            getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
            getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)
            );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_3BM5J428.SetValue(dt);
                
        }
        #endregion

        #region Description : 신규버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYACTX005I("","","","","","","","","","","","","","","","","","","","","")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 스프레드 클릭
        private void FPS91_TY_S_AC_3BM5J428_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string[] AOIDEL = this.FPS91_TY_S_AC_3BM5J428.GetValue("AOIDENDEL").ToString().Split('/');
            string[] AOIPEN = this.FPS91_TY_S_AC_3BM5J428.GetValue("AOIDENPEN").ToString().Split('/');
            string[] AOIPAY = this.FPS91_TY_S_AC_3BM5J428.GetValue("AOIDENPAY").ToString().Split('/');
            string[] AOIZEO = this.FPS91_TY_S_AC_3BM5J428.GetValue("AOIDENZEO").ToString().Split('/');

            if ((new TYACTX005I(this.FPS91_TY_S_AC_3BM5J428.GetValue("AOYEAR").ToString(),
                                this.FPS91_TY_S_AC_3BM5J428.GetValue("AOBRANCH").ToString(),
                                this.FPS91_TY_S_AC_3BM5J428.GetValue("AOVNEDCD").ToString(),
                                this.FPS91_TY_S_AC_3BM5J428.GetValue("AOREPGB").ToString(),
                                this.FPS91_TY_S_AC_3BM5J428.GetValue("AOCONFGB").ToString(),
                                this.FPS91_TY_S_AC_3BM5J428.GetValue("AOCNELEAMT").ToString(),
                                this.FPS91_TY_S_AC_3BM5J428.GetValue("AOLMELEAMT").ToString(),
                                this.FPS91_TY_S_AC_3BM5J428.GetValue("AODEPRAMT").ToString(),
                                this.FPS91_TY_S_AC_3BM5J428.GetValue("AOZEROTAX").ToString(),
                                AOIDEL[0],
                                AOIDEL[1],
                                AOIPEN[0],
                                AOIPEN[1],
                                AOIPAY[0],
                                AOIPAY[1],
                                AOIZEO[0],
                                AOIZEO[1],
                                this.FPS91_TY_S_AC_3BM5J428.GetValue("AOBANKCD").ToString(),
                                this.FPS91_TY_S_AC_3BM5J428.GetValue("AOGUJANUM").ToString(),
                                this.FPS91_TY_S_AC_3BM5J428.GetValue("AOGENCHK").ToString(),
                                this.FPS91_TY_S_AC_3BM5J428.GetValue("AOGENNUM").ToString()
                                )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            
            this.DbConnector.Attach
                (
                "TY_P_AC_42BBW322",
                dt.Rows[0]["AOYEAR"].ToString(),
                dt.Rows[0]["AOBRANCH"].ToString(),
                dt.Rows[0]["AOREPGB"].ToString(),
                dt.Rows[0]["AOCONFGB"].ToString()
                );

            DataTable dt2 = this.DbConnector.ExecuteDataTable();
            if (dt2.Rows.Count > 0)
            {
                //마감 구분 (Y)
                if (dt2.Rows[0]["E1FINISH"].ToString() == "Y")
                {
                    this.ShowMessage("TY_M_AC_42E9B386");
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_3C212497", dt);
                    this.DbConnector.ExecuteNonQueryList();

                    this.DbConnector.Attach("TY_P_AC_42A55313",
                                            dt.Rows[0]["AOYEAR"].ToString(),
                                            dt.Rows[0]["AOBRANCH"].ToString(),
                                            dt.Rows[0]["AOVNEDCD"].ToString(),
                                            dt.Rows[0]["AOREPGB"].ToString(),
                                            dt.Rows[0]["AOCONFGB"].ToString()
                                            );
                    this.DbConnector.ExecuteNonQueryList();

                    this.BTN61_INQ_Click(null, null);
                    this.ShowMessage("TY_M_GB_23NAD874");
                }
            }
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_3BM5J428.GetDataSourceInclude(TSpread.TActionType.Remove, "AOYEAR", "AOBRANCH", "AOVNEDCD", "AOREPGB", "AOCONFGB");

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

        #region Description : 사업장 구분
        private void CBO01_VNGUBUN_SelectedIndexChanged(object sender, EventArgs e)
        {
            UP_COMBOBOX(this.CBO01_VNGUBUN.GetValue().ToString());
        }
        #endregion

        #region Description : 사업장 구분에 따른 거래처코드 가져오기
        private void UP_COMBOBOX(string sCODE)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3C3BY520",
                sCODE.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.CBO01_BADVEND.DataBind(dt, true);
        }
        #endregion
    }
}
