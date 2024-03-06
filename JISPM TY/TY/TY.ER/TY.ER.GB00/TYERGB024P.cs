using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using System.Windows.Forms;
using System;

namespace TY.ER.GB00
{
    public partial class TYERGB024P : TYBase, IPopupHelper
    {
        public string fsCode = string.Empty;
        private string fsProcedure = string.Empty;
        private TCodeBox _TComboHelper;
        private DataRow _SelectedDataRow;
        private ToolStripMenuItem _favorItem = new ToolStripMenuItem("즐겨찾기 추가");
        //private bool _isShow = false;

        public TYERGB024P(string sProcedure)
            : base()
        {
            InitializeComponent();

            this.SetPopupStyle();

            this.FormClosing += new FormClosingEventHandler(TYERGB024P_FormClosing);
            this.PreLoad += new System.EventHandler(TYERGB024P_PreLoad);
            this._favorItem.Click += new EventHandler(_favorItem_Click);

            fsProcedure = sProcedure;
        }

        private void TYERGB024P_PreLoad(object sender, System.EventArgs e)
        {
            //this._isShow = true;
        }

        private void TYERGB024P_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.SaveSearchCondition();
            //this._isShow = false;
            this.BTN61_CLR_Click(null, null);
        }

        private void TYERGB024P_Load(object sender, System.EventArgs e)
        {
            if (this.DesignMode)
                return;

            if (this.FPS91_TY_S_GB_B9EDQ567.CurrentContextMenu.Items.IndexOf(this._favorItem) < 0)
            {
                this.FPS91_TY_S_GB_B9EDQ567.CurrentContextMenu.Items.Insert(0, new ToolStripSeparator());
                this.FPS91_TY_S_GB_B9EDQ567.CurrentContextMenu.Items.Insert(0, this._favorItem);
            }

            this.SetSearchCondition();

            if (this._TComboHelper != null)
            {
                if (string.IsNullOrEmpty(this._TComboHelper.GetText()) || string.IsNullOrEmpty(this._TComboHelper.GetValue().ToString()))
                {
                    this.TXT01_CODE_NAME.SetValue(this._TComboHelper.GetText());
                    this.TXT01_CODE.SetValue(this._TComboHelper.GetValue());
                }

                if (string.IsNullOrEmpty(this._TComboHelper.GetText()) ^ string.IsNullOrEmpty(this._TComboHelper.GetValue().ToString()))
                    this.SetStartingFocus(this.FPS91_TY_S_GB_B9EDQ567);
                else
                    this.SetStartingFocus(this.TXT01_CODE_NAME);

                if (!string.IsNullOrEmpty(this._TComboHelper.GetText()) || !string.IsNullOrEmpty(this._TComboHelper.GetValue().ToString()))
                {                   
                    if (!string.IsNullOrEmpty(this._TComboHelper.GetText()) && !string.IsNullOrEmpty(this._TComboHelper.GetValue().ToString()))
                    {
                        this.TXT01_CODE.SetValue(this._TComboHelper.GetValue());
                    }

                    this.BTN61_INQ_Click(null, null);
                }
            }
            
            //this.BTN61_INQ_Click(null, null);
        }

        private void BTN61_INQ_Click(object sender, System.EventArgs e)
        {
            fsCode = "";

            DataSet ds = new DataSet();
            DataTable dt;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach(fsProcedure.ToString(), TYUserInfo.SecureKey, "Y", this.TXT01_CODE.GetValue(), this.TXT01_CODE_NAME.GetValue());
            dt = this.DbConnector.ExecuteDataTable();
            dt.TableName = "LIST";
            dt.DataSet.Tables.Remove(dt);
            ds.Tables.Add(dt);

            this.DbConnector.CommandClear();
            this.DbConnector.Attach(
                "TY_P_GB_2449V392"
                , TYUserInfo.EmpNo
                , fsProcedure.ToString()
                , "NO CUSTOM PARAM"
                );
            dt = this.DbConnector.ExecuteDataTable();
            dt.TableName = "INFO";
            dt.DataSet.Tables.Remove(dt);
            ds.Tables.Add(dt);

            DataColumn dcList = ds.Tables["LIST"].Columns["CODE"];
            DataColumn dcInfo = ds.Tables["INFO"].Columns["CODE"];

            ds.Tables["LIST"].Columns.Add("FAVOR_YN", typeof(string));
            ds.Tables["LIST"].Columns.Add("LAST_USE_DTM", typeof(string));
            ds.Tables["LIST"].Columns.Add("USE_COUNT", typeof(int));

            DataTable source = ds.Tables["LIST"].Clone();

            ds.Relations.Add("REL_CODE_INFO", dcList, dcInfo, false);

            foreach (DataRow dr in ds.Tables["LIST"].Rows)
            {
                dr["FAVOR_YN"] = dr.GetChildRows("REL_CODE_INFO").Length != 0
                    ? dr.GetChildRows("REL_CODE_INFO")[0]["FAVOR_YN"]
                    : "N";
                dr["LAST_USE_DTM"] = dr.GetChildRows("REL_CODE_INFO").Length != 0
                    ? dr.GetChildRows("REL_CODE_INFO")[0]["LAST_USE_DTM"]
                    : "00000000000000";
                dr["USE_COUNT"] = dr.GetChildRows("REL_CODE_INFO").Length != 0
                    ? dr.GetChildRows("REL_CODE_INFO")[0]["USE_COUNT"]
                    : "0";
            }

            DataRow[] selectedRows;
            switch (this.CBO02_CODE_SCH_TYPE.GetValue().ToString())
            {
                case "2":   //즐겨찾기
                    selectedRows = ds.Tables["LIST"].Select("FAVOR_YN = 'Y'");
                    break;
                case "3":   //최근등록순
                    if (ds.Tables["LIST"].Columns.IndexOf("REG_DTM") < 0)
                    {
                        DataRow errRow = source.NewRow();
                        errRow["CODE"] = "XXXXX";
                        errRow["CODE_NAME"] = "등록일시 데이터가 없습니다.";
                        source.Rows.Add(errRow);
                        this.FPS91_TY_S_GB_B9EDQ567.SetValue(source);
                        return;
                    }
                    selectedRows = ds.Tables["LIST"].Select("", "REG_DTM DESC");
                    break;
                case "4":   //최근사용코드
                    selectedRows = ds.Tables["LIST"].Select("USE_COUNT > 0", "LAST_USE_DTM DESC");
                    break;
                case "5":   //많이사용한코드
                    selectedRows = ds.Tables["LIST"].Select("USE_COUNT > 0", "USE_COUNT DESC");
                    break;
                default:
                    selectedRows = ds.Tables["LIST"].Select();
                    break;
            }

            foreach (DataRow dr in selectedRows)
            {
                source.Rows.Add(dr.ItemArray);
            }

            this.FPS91_TY_S_GB_B9EDQ567.SetValue(source);

            if (sender != null && this.FPS91_TY_S_GB_B9EDQ567.ActiveSheet.Rows.Count > 0)
                this.SetFocus(this.FPS91_TY_S_GB_B9EDQ567);
        }

        private void BTN61_CLR_Click(object sender, System.EventArgs e)
        {
            Initialize_Controls("01");
        }

        private void CBO02_CODE_SCH_TYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._favorItem.Text = this.CBO02_CODE_SCH_TYPE.GetValue().ToString() == "2"
                ? "즐겨찾기 취소"
                : "즐겨찾기 추가";

            this.BTN61_INQ_Click(null, null);
        }

        private void FPS91_TY_S_GB_B9EDQ567_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader)
                return;
            this.ConfirmEventInterface();
        }

        private void _favorItem_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_GB_2449G388"
                , TYUserInfo.EmpNo
                , fsProcedure.ToString()
                , "NO CUSTOM PARAM"
                , this.FPS91_TY_S_GB_B9EDQ567.GetValue(this.FPS91_TY_S_GB_B9EDQ567.ActiveRowIndex, "CODE").ToString()
                , (this.CBO02_CODE_SCH_TYPE.GetValue().ToString() == "2" ? "N" : "Y"));
            this.DbConnector.ExecuteNonQuery();

            this.CBO02_CODE_SCH_TYPE.SetValue("2");
            this.BTN61_INQ_Click(null, null);
        }

        #region IPopupHelper 멤버

        public void ConfirmEventInterface()
        {
            fsCode = "";
            if (this.FPS91_TY_S_GB_B9EDQ567.ActiveSheet.Rows.Count == 0)
                return;

            int row = this.FPS91_TY_S_GB_B9EDQ567.ActiveRowIndex;

            fsCode = this.FPS91_TY_S_GB_B9EDQ567.GetValue(row, "CODE").ToString();
            string code = this.FPS91_TY_S_GB_B9EDQ567.GetValue(row, "CODE").ToString();
            string name = this.FPS91_TY_S_GB_B9EDQ567.GetValue(row, "CODE_NAME").ToString();

            this._SelectedDataRow = this.FPS91_TY_S_GB_B9EDQ567.GetDataRow(row);
            if (this._TComboHelper != null)
            {
                this._TComboHelper.SetValue(code, name);
                this._TComboHelper.BindedDataRow = _SelectedDataRow;
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach(
                "TY_P_GB_2449G388"
                , TYUserInfo.EmpNo
                , fsProcedure.ToString()
                , "NO CUSTOM PARAM"
                , code
                , string.Empty);
            this.DbConnector.ExecuteNonQuery();

            this.SaveSearchCondition();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }

        public DataTable GetDataSource(params string[] parameters)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_GB_2A9BT513", parameters[1], parameters[0], string.Empty);
            return this.DbConnector.ExecuteDataTable();
        }

        public void Initialize_Helper(Shoveling2010.SmartClient.SystemUtility.Controls.TCodeBox sender)
        {
            this._TComboHelper = sender;
            this._SelectedDataRow = null;

            this.Initialize_DbConnector();

            if (this._TComboHelper != null)
            {
                this.SetSearchCondition();

                if (string.IsNullOrEmpty(this._TComboHelper.GetText()) || string.IsNullOrEmpty(this._TComboHelper.GetValue().ToString()))
                {
                    this.TXT01_CODE_NAME.SetValue(this._TComboHelper.GetText());
                    this.TXT01_CODE.SetValue(this._TComboHelper.GetValue());
                }
            }
        }

        public DataRow SelectedRow
        {
            get { return this._SelectedDataRow; }
        }

        public void ShowPopupHelper()
        {
            if (this.IsHandleCreated)
            {
                this.TYERGB024P_Load(null, null);
                this._TComboHelper.CodeBoxPopupHelper_Init();
            }

            base.Show();

            if (string.IsNullOrEmpty(this.TXT01_CODE_NAME.GetValue().ToString()) ^ string.IsNullOrEmpty(this.TXT01_CODE.GetValue().ToString()))
                this.SetFocus(this.FPS91_TY_S_GB_B9EDQ567);
            else
                this.SetFocus(this.TXT01_CODE_NAME);
        }

        #endregion

        private void SetSearchCondition()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach(
                "TY_P_GB_25I4G548"
                , TYUserInfo.EmpNo
                , fsProcedure.ToString()
                , "NO CUSTOM PARAM"
                );
            using (DataTable dt = this.DbConnector.ExecuteDataTable())
            {
                if (dt != null && dt.Rows.Count > 0)
                    this.CBO02_CODE_SCH_TYPE.SetValue(dt.Rows[0]["SEARCH_OPTION"]);
            }
        }

        private void SaveSearchCondition()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach(
                "TY_P_GB_25I4H549"
                , TYUserInfo.EmpNo
                , fsProcedure.ToString()
                , "NO CUSTOM PARAM"
                , this.CBO02_CODE_SCH_TYPE.GetValue()
                );
            this.DbConnector.ExecuteNonQuery();
        }
    }
}
