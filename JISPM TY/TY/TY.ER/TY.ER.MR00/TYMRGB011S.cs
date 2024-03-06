using System;
using System.Data;
using System.Windows.Forms;
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
    ///  TY_P_MR_2BM1D571 : 코드박스 - 자산분류코드 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_2BM1E572 : 코드박스 - 자산분류코드 조회
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
    public partial class TYMRGB011S : TYBase, IPopupHelper
    {
        public string fsFXSLASCODE = string.Empty;
        public string fsFXSLMCODE  = string.Empty;
        public string fsFXSMMCODE  = string.Empty;
        public string fsFXSMCODE   = string.Empty;
        public string fsFXSMDESC   = string.Empty;

        private string fsGUBUN       = string.Empty;
        private string fsWKGUBUN     = string.Empty;
        private string fsJASANCODE   = string.Empty;
        private string fsJASANCODENM = string.Empty;

        private TCodeBox _TComboHelper;
        private DataRow _SelectedDataRow;

        #region Description : 페이지 로드
        public TYMRGB011S()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }

        private void TYMRGB011S_Load(object sender, System.EventArgs e)
        {
            if (this.DesignMode)
                return;

            if (fsWKGUBUN == "" && fsGUBUN == "Search" && this.fsJASANCODE.ToString() != "")
            {
                this.CBO01_FXSLASCODE.Initialize();
                this.CBO01_FXC3SAUP.Initialize();
                this.CBO01_FXSLMCODE.Initialize();
                this.CBO01_FXSMMCODE.Initialize();
                this.CBO01_FXSMCODE.Initialize();
            }

            this.BTN61_INQ_Click(null, null);

            fsWKGUBUN = "Search";

            SetStartingFocus(this.CBO01_FXSLASCODE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sCheck = string.Empty;

            if (fsWKGUBUN == "" && fsGUBUN == "Search" && this.fsJASANCODE.ToString() != "") // 코드박스에 데이터가 처음 뿌려 졌을때
            {
                sCheck = "PASS";
            }
            else if (fsWKGUBUN == "Search" && fsGUBUN == "Search" && this.fsJASANCODE.ToString() != "") // 코드박스에 코드를 입력했을때
            {
                sCheck = "CONTINUE";
            }
            else if (fsWKGUBUN == "Search" && fsGUBUN == "Search" && this.fsJASANCODENM.ToString() != "") // 코드박스에 코드명를 입력했을때
            {
                sCheck = "CONTINUE";
            }
            else if (fsWKGUBUN == "Search" && fsGUBUN == "Search" && this.fsJASANCODE.ToString() == "" && this.fsJASANCODENM.ToString() == "") // 코드박스에 코드와 코드명을 입력 안했을때
            {
                sCheck = "PASS";
            }

            if (this.CBO01_FXSLASCODE.GetValue().ToString() != "")
            {
                sCheck = "CONTINUE";

                fsJASANCODE = "";
                fsJASANCODE = this.CBO01_FXC3SAUP.GetValue().ToString() + this.CBO01_FXSLMCODE.GetValue().ToString() + this.CBO01_FXSMMCODE.GetValue().ToString() + this.CBO01_FXSMCODE.GetValue().ToString();
            }

            if (this.TXT01_FXSMDESC.GetValue().ToString() != "")
            {
                sCheck = "CONTINUE";

                fsJASANCODENM = "";
                this.fsJASANCODENM = this.TXT01_FXSMDESC.GetValue().ToString();
            }

            DataTable dt = new DataTable();

            if (sCheck == "CONTINUE")
            {
                if (this.CBO01_FXSLASCODE.GetValue().ToString() == "")
                {
                    this.ShowCustomMessage("자산 구분을 선택하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.CBO01_FXSLASCODE);

                    return;
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                   (
                   "TY_P_MR_2BM1D571",
                   this.CBO01_FXSLASCODE.GetValue(),
                   this.CBO01_FXSLASCODE.GetValue(),
                   fsJASANCODE.ToString(),
                   fsJASANCODENM.ToString()
                   );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.FPS91_TY_S_MR_2BM1E572.SetValue(dt);

                    if (sender != null && this.FPS91_TY_S_MR_2BM1E572.ActiveSheet.Rows.Count > 0)
                        this.SetFocus(this.FPS91_TY_S_MR_2BM1E572);
                }
                else
                {
                    this.FPS91_TY_S_MR_2BM1E572.SetValue(dt);
                    //this.ShowMessage("TY_M_AC_2422N250");
                }
            }
            else
            {
                this.FPS91_TY_S_MR_2BM1E572.SetValue(dt);
            }

            fsJASANCODE   = "";
            fsJASANCODENM = "";
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        //#region Description : 스프레드 이벤트
        //private void FPS91_TY_S_MR_2BM1E572_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        //{
        //    fsFXSLASCODE = this.FPS91_TY_S_MR_2BM1E572.GetValue("FXSLASCODE").ToString();
        //    fsFXSLMCODE  = this.FPS91_TY_S_MR_2BM1E572.GetValue("FXSLMCODE").ToString();
        //    fsFXSMMCODE  = this.FPS91_TY_S_MR_2BM1E572.GetValue("FXSMMCODE").ToString();
        //    fsFXSMCODE   = this.FPS91_TY_S_MR_2BM1E572.GetValue("FXSMCODE").ToString();
        //    fsFXSMDESC   = this.FPS91_TY_S_MR_2BM1E572.GetValue("FXSMDESC").ToString();

        //    this.DialogResult = System.Windows.Forms.DialogResult.OK;

        //    this.Close();
        //}
        //#endregion

        #region Description : 자산구분 콤보박스 이벤트
        private void CBO01_FXSLASCODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (fsWKGUBUN == "Search" && fsGUBUN == "Search")
            //{
            //    DataTable dt = new DataTable();

            //    this.DbConnector.CommandClear();
            //    this.DbConnector.Attach
            //        (
            //        "TY_P_MR_2BMB0562",
            //        this.CBO01_FXSLASCODE.GetValue()
            //        );

            //    dt = this.DbConnector.ExecuteDataTable();

            //    this.CBO01_FXSLMCODE.DataBind(dt, true);
            //}

            //if (fsWKGUBUN == "Search" && fsGUBUN == "")
            //{
            //    DataTable dt = new DataTable();

            //    this.DbConnector.CommandClear();
            //    this.DbConnector.Attach
            //        (
            //        "TY_P_MR_2BMB0562",
            //        this.CBO01_FXSLASCODE.GetValue()
            //        );

            //    dt = this.DbConnector.ExecuteDataTable();

            //    this.CBO01_FXSLMCODE.DataBind(dt, true);
            //}
        }
        #endregion

        #region Description : 사업부 콤보박스 이벤트
        private void CBO01_FXC3SAUP_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sFXSLASCODE = string.Empty;

            if (fsWKGUBUN == "Search" && fsGUBUN == "Search")
            {
                DataTable dt = new DataTable();

                if (this.CBO01_FXSLASCODE.GetValue().ToString() != "2" &&
                    this.CBO01_FXSLASCODE.GetValue().ToString() != "3" &&
                    this.CBO01_FXSLASCODE.GetValue().ToString() != "4" &&
                    this.CBO01_FXSLASCODE.GetValue().ToString() != "9")
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

            if (fsWKGUBUN == "Search" && fsGUBUN == "")
            {
                DataTable dt = new DataTable();

                if (this.CBO01_FXSLASCODE.GetValue().ToString() != "2" &&
                    this.CBO01_FXSLASCODE.GetValue().ToString() != "3" &&
                    this.CBO01_FXSLASCODE.GetValue().ToString() != "4" &&
                    this.CBO01_FXSLASCODE.GetValue().ToString() != "9")
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
        }
        #endregion


        #region Description : 대분류 콤보박스 이벤트
        private void CBO01_FXSLMCODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fsWKGUBUN == "Search" && fsGUBUN == "Search")
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

            if (fsWKGUBUN == "Search" && fsGUBUN == "")
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
        }
        #endregion

        #region Description : 중분류 콤보박스 이벤트
        private void CBO01_FXSMMCODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fsWKGUBUN == "Search" && fsGUBUN == "Search")
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

            if (fsWKGUBUN == "Search" && fsGUBUN == "")
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
        }
        #endregion

        #region Description : 소분류 콤보박스 이벤트
        private void CBO01_FXSMCODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fsWKGUBUN == "Search" && fsGUBUN == "Search")
            {
                //BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        ////필수..시작
        #region Description : 필수 선택 조회
        public void ConfirmEventInterface()
        {
            int row = 0;

            string sCode = "";
            string sName = "";

            row = FPS91_TY_S_MR_2BM1E572.ActiveSheet.ActiveRowIndex;

            //if (row > 0)
            //{
                // 제품코드
                sCode = this.FPS91_TY_S_MR_2BM1E572.GetValue(row, "JASANCODE1").ToString();
                // 제품명
                sName = this.FPS91_TY_S_MR_2BM1E572.GetValue(row, "FXSMDESC1").ToString();

                this._SelectedDataRow = this.FPS91_TY_S_MR_2BM1E572.GetDataRow(row);

                if (this._TComboHelper != null)
                {
                    this._TComboHelper.SetValue(sCode, sName);
                    this._TComboHelper.BindedDataRow = _SelectedDataRow;
                }

                fsGUBUN = "Search";

                Initialize_Controls("01");

                this.Close();
            //}
        }

        public DataTable GetDataSource(params string[] parameters)
        {
            fsGUBUN = "Search";

            fsJASANCODE   = parameters[1];
            fsJASANCODENM = parameters[0];

            string sASSET = string.Empty;

            DataTable dt = new DataTable();
           

            if (fsJASANCODE.ToString() != "" && fsJASANCODE.ToString().Length == 11)
            {
                sASSET = fsJASANCODE.ToString().Substring(0, 1);
            }


            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BM1D571",
                sASSET.ToString(),
                sASSET.ToString(),
                fsJASANCODE.ToString().Substring(1,10),
                fsJASANCODENM.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_MR_2BM1E572.SetValue(dt);

            return dt;
        }

        public void Initialize_Helper(TCodeBox sender)
        {
            this._TComboHelper = sender;
            this._SelectedDataRow = null;

            this.Initialize_DbConnector();

            this.CBO01_FXSLASCODE.Initialize();
            this.CBO01_FXC3SAUP.Initialize();
            this.CBO01_FXSLMCODE.Initialize();
            this.CBO01_FXSMMCODE.Initialize();
            this.CBO01_FXSMCODE.Initialize();

        }

        public DataRow SelectedRow
        {
            get { return this._SelectedDataRow; }
        }

        public void ShowPopupHelper()
        {
            if (this.IsHandleCreated)
            {
                if (fsWKGUBUN == "Search" && fsGUBUN == "")
                {
                    this.CBO01_FXSLASCODE.Initialize();
                    this.CBO01_FXC3SAUP.Initialize();
                    this.CBO01_FXSLMCODE.Initialize();
                    this.CBO01_FXSMMCODE.Initialize();
                    this.CBO01_FXSMCODE.Initialize();
                }

                this.TYMRGB011S_Load(null, null);
                this._TComboHelper.CodeBoxPopupHelper_Init();
            }

            base.Show();
        }

        private void FPS91_TY_S_MR_2BM1E572_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader)
                return;
            this.ConfirmEventInterface();
        }
        #endregion

        private void TYMRGB011S_FormClosing(object sender, FormClosingEventArgs e)
        {
            Initialize_Controls("01");
        }

        //필수...끝
    }
}