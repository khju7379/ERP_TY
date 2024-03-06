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
    ///  TY_P_MR_2B86I206 : 코드박스 - 품목코드 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_2B86J207 : 코드박스 - 품목코드 조회
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
    public partial class TYMRGB003S : TYBase, IPopupHelper
    {
        private string fsGUBUN   = string.Empty;
        private string fsWKGUBUN = string.Empty;
        private string fsJEPUM   = string.Empty;
        private string fsJEPUMNM = string.Empty;

        private TCodeBox _TComboHelper;
        private DataRow _SelectedDataRow;

        #region Description : 페이지 로드
        public TYMRGB003S()
        {
            this.SetPopupStyle();
            InitializeComponent();
        }

        private void TYMRGB003S_Load(object sender, System.EventArgs e)
        {
            if (this.DesignMode)
                return;

            if (fsWKGUBUN == "" && fsGUBUN == "Search" && this.fsJEPUM.ToString() != "")
            {
                this.CBO01_Z105000.Initialize();
                this.CBO01_Z105001.Initialize();
                this.CBO01_Z105002.Initialize();
                this.TXT01_Z105013.Initialize();
            }

            this.BTN61_INQ_Click(null, null);

            fsWKGUBUN = "Search";
            
            SetStartingFocus(this.CBO01_Z105000);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sCheck = string.Empty;

            if (fsWKGUBUN == "" && fsGUBUN == "Search" && this.fsJEPUM.ToString() != "") // 코드박스에 데이터가 처음 뿌려 졌을때
            {
                sCheck = "PASS";
            }
            else if (fsWKGUBUN == "Search" && fsGUBUN == "Search" && this.fsJEPUM.ToString() != "") // 코드박스에 코드를 입력했을때
            {
                sCheck = "CONTINUE";
            }
            else if (fsWKGUBUN == "Search" && fsGUBUN == "Search" && this.fsJEPUMNM.ToString() != "") // 코드박스에 코드명를 입력했을때
            {
                sCheck = "CONTINUE";
            }
            else if (fsWKGUBUN == "Search" && fsGUBUN == "Search" && this.fsJEPUM.ToString() == "" && this.fsJEPUMNM.ToString() == "") // 코드박스에 코드와 코드명을 입력 안했을때
            {
                sCheck = "PASS";
            }

            if (this.CBO01_Z105000.GetValue().ToString() != "")
            {
                sCheck = "CONTINUE";

                fsJEPUM = "";
                fsJEPUM = this.CBO01_Z105000.GetValue().ToString() + this.CBO01_Z105001.GetValue().ToString() + this.CBO01_Z105002.GetValue().ToString();
            }

            if (this.TXT01_Z105013.GetValue().ToString() != "")
            {
                sCheck = "CONTINUE";

                fsJEPUMNM = "";
                this.fsJEPUMNM = this.TXT01_Z105013.GetValue().ToString();
            }

            if (this.TXT01_Z105029.GetValue().ToString() != "")
            {
                sCheck = "CONTINUE";
            }

            DataTable dt = new DataTable();

            if (sCheck == "CONTINUE")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                   (
                   "TY_P_MR_2B86I206",
                   fsJEPUM.ToString(),
                   fsJEPUMNM.ToString(),
                   this.TXT01_Z105029.GetValue().ToString()
                   );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.FPS91_TY_S_MR_2B86J207.SetValue(dt);

                    if (sender != null && this.FPS91_TY_S_MR_2B86J207.ActiveSheet.Rows.Count > 0)
                        this.SetFocus(this.FPS91_TY_S_MR_2B86J207);
                }
                else
                {
                    this.FPS91_TY_S_MR_2B86J207.SetValue(dt);
                    //this.ShowMessage("TY_M_AC_2422N250");
                }
            }
            else
            {
                this.FPS91_TY_S_MR_2B86J207.SetValue(dt);
            }

            fsJEPUM   = "";
            fsJEPUMNM = "";
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 대분류 콤보박스 이벤트
        private void CBO01_Z105000_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fsWKGUBUN == "Search" && fsGUBUN == "Search")
            {
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2B24D041",
                    this.CBO01_Z105000.GetValue()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                this.CBO01_Z105001.DataBind(dt, true);
            }

            if (fsWKGUBUN == "Search" && fsGUBUN == "")
            {
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2B24D041",
                    this.CBO01_Z105000.GetValue()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                this.CBO01_Z105001.DataBind(dt, true);
            }

            //BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 중분류 콤보박스 이벤트
        private void CBO01_Z105001_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fsWKGUBUN == "Search" && fsGUBUN == "Search")
            {
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2B24D042",
                    this.CBO01_Z105000.GetValue(),
                    this.CBO01_Z105001.GetValue()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                this.CBO01_Z105002.DataBind(dt, true);

                if (this.CBO01_Z105000.GetValue().ToString() != "")
                {
                    //BTN61_INQ_Click(null, null);
                }
            }

            if (fsWKGUBUN == "Search" && fsGUBUN == "")
            {
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2B24D042",
                    this.CBO01_Z105000.GetValue(),
                    this.CBO01_Z105001.GetValue()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                this.CBO01_Z105002.DataBind(dt, true);

                if (this.CBO01_Z105000.GetValue().ToString() != "")
                {
                    //BTN61_INQ_Click(null, null);
                }
            }
        }
        #endregion

        #region Description : 소분류 콤보박스 이벤트
        private void CBO01_Z105002_SelectedIndexChanged(object sender, EventArgs e)
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

            row = FPS91_TY_S_MR_2B86J207.ActiveSheet.ActiveRowIndex;

            //if (row > 0)
            //{
                // 제품코드
                sCode = this.FPS91_TY_S_MR_2B86J207.GetValue(row, "JEPUM").ToString();
                // 제품명
                sName = this.FPS91_TY_S_MR_2B86J207.GetValue(row, "Z105013").ToString();

                this._SelectedDataRow = this.FPS91_TY_S_MR_2B86J207.GetDataRow(row);

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

            fsJEPUM   = parameters[1];
            fsJEPUMNM = parameters[0];

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2B86I206",
                fsJEPUM,
                fsJEPUMNM,
                ""
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_MR_2B86J207.SetValue(dt);

            return dt;
        }

        public void Initialize_Helper(TCodeBox sender)
        {
            this._TComboHelper = sender;
            this._SelectedDataRow = null;

            this.Initialize_DbConnector();

            this.CBO01_Z105000.Initialize();
            this.CBO01_Z105001.Initialize();
            this.CBO01_Z105002.Initialize();
            this.TXT01_Z105013.Initialize();
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
                    this.CBO01_Z105000.Initialize();
                    this.CBO01_Z105001.Initialize();
                    this.CBO01_Z105002.Initialize();
                    this.TXT01_Z105013.Initialize();
                }

                this.TYMRGB003S_Load(null, null);
                this._TComboHelper.CodeBoxPopupHelper_Init();
            }
            else
            {
            }

            base.Show();
        }

        private void FPS91_TY_S_MR_2B86J207_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader)
                return;
            this.ConfirmEventInterface();
        }
        #endregion

        private void CBO01_Z105000_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TXT01_Z105013_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                fsGUBUN = "Search";
                Initialize_Controls("01");
            }

        }
        //필수...끝
    }
}