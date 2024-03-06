using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.MR00
{
    /// <summary>
    /// 코드박스 - 품목코드 조회 프로그램입니다.
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
    ///  TY_P_MR_2BLA3550 : 코드박스 - 비품 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_2BM44576 : 코드박스 - 비품 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  Z105000 : 대분류코드
    ///  Z105001 : 중분류코드
    ///  Z105002 : 소분류코드
    ///  Z105013 : 자재명１
    /// </summary>
    public partial class TYMRGB010S : TYBase
    {
        public string fsFXSYEAR   = string.Empty;
        public string fsFXSSEQ    = string.Empty;
        public string fsFXSSUBNUM = string.Empty;
        public string fsFXSNAME   = string.Empty;

        // 자산분류
        public string fsJASAN   = string.Empty;
        public string fsSAUPBU = string.Empty;
        public string fsLARGE   = string.Empty;
        public string fsMIDDLE  = string.Empty;
        public string fsSMALL   = string.Empty;
        public string fsJASANNM = string.Empty;
        public string fsFXSTOCKNUM = string.Empty;

        #region Description : 페이지 로드
        public TYMRGB010S()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }

        private void TYMRGB010S_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.CBO01_FXSLASCODE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_MR_2BM44576.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
               (
               "TY_P_MR_2BLAG551",
               this.CBO01_FXSLASCODE.GetValue(),
               this.CBO01_FXC3SAUP.GetValue(),
               this.CBO01_FXSLMCODE.GetValue(),
               this.CBO01_FXSMMCODE.GetValue(),
               this.CBO01_FXSMCODE.GetValue(),
               this.TXT01_FXSMDESC.GetValue()
               );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_MR_2BM44576.SetValue(dt);
            }
            else
            {
                this.FPS91_TY_S_MR_2BM44576.SetValue(dt);

                //this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_MR_2BM44576_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            fsFXSYEAR    = this.FPS91_TY_S_MR_2BM44576.GetValue("FXSYEAR").ToString();    // 자산년도
            fsFXSSEQ     = this.FPS91_TY_S_MR_2BM44576.GetValue("FXSSEQ").ToString();     // 자산순번
            fsFXSSUBNUM  = this.FPS91_TY_S_MR_2BM44576.GetValue("FXSSUBNUM").ToString();  // 가족코드
            fsFXSNAME    = this.FPS91_TY_S_MR_2BM44576.GetValue("FXSNAME").ToString();    // 자산명
            fsFXSTOCKNUM = this.FPS91_TY_S_MR_2BM44576.GetValue("FXSTOCKNUM").ToString(); // 선급자재번호

            fsJASAN   = this.FPS91_TY_S_MR_2BM44576.GetValue("FXGUBN").ToString().Substring(0, 1).ToString();
            fsSAUPBU  = this.FPS91_TY_S_MR_2BM44576.GetValue("FXGUBN").ToString().Substring(1, 1).ToString();
            fsLARGE   = this.FPS91_TY_S_MR_2BM44576.GetValue("FXGUBN").ToString().Substring(2, 2).ToString();
            fsMIDDLE  = this.FPS91_TY_S_MR_2BM44576.GetValue("FXGUBN").ToString().Substring(4, 4).ToString();
            fsSMALL   = this.FPS91_TY_S_MR_2BM44576.GetValue("FXGUBN").ToString().Substring(8, 3).ToString();
            fsJASANNM = this.FPS91_TY_S_MR_2BM44576.GetValue("FXSMDESC").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion

        #region Description : 자산구분 콤보박스 이벤트
        private void CBO01_FXSLASCODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable();

            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach
            //    (
            //    "TY_P_MR_2BMB0562",
            //    this.CBO01_FXSLASCODE.GetValue()
            //    );

            //dt = this.DbConnector.ExecuteDataTable();

            //this.CBO01_FXSLMCODE.DataBind(dt, true);
        }
        #endregion

        #region Description : 사업부 콤보박스 이벤트
        private void CBO01_FXC3SAUP_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            if (this.CBO01_FXSLASCODE.GetValue().ToString() != "2" &&
                this.CBO01_FXSLASCODE.GetValue().ToString() != "3" &&
                this.CBO01_FXSLASCODE.GetValue().ToString() != "4")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_654HT838",
                    this.CBO01_FXC3SAUP.GetValue(),
                    this.CBO01_FXSLASCODE.GetValue()
                    );
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_654HV839",
                    this.CBO01_FXC3SAUP.GetValue()
                    );
            }

            dt = this.DbConnector.ExecuteDataTable();

            this.CBO01_FXSLMCODE.DataBind(dt, true);
        }
        #endregion

        #region Description : 대분류 콤보박스 이벤트
        private void CBO01_FXSLMCODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BMB4563",
                this.CBO01_FXC3SAUP.GetValue(),
                this.CBO01_FXSLMCODE.GetValue()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.CBO01_FXSMMCODE.DataBind(dt, true);
        }
        #endregion

        #region Description : 중분류 콤보박스 이벤트
        private void CBO01_FXSMMCODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BMBB564",
                this.CBO01_FXC3SAUP.GetValue(),
                this.CBO01_FXSLMCODE.GetValue(),
                this.CBO01_FXSMMCODE.GetValue()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.CBO01_FXSMCODE.DataBind(dt, true);
        }
        #endregion

        #region Description : 소분류 콤보박스 이벤트
        private void CBO01_FXSMCODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}