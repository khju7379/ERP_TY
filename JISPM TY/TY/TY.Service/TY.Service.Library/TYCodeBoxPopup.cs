using System;
using System.Data;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;

namespace TY.Service.Library
{
    public partial class TYCodeBoxPopup : TYBase, IPopupHelper
    {
        private TCodeBox _TComboHelper;
        private DataRow _SelectedDataRow;
        private ToolStripMenuItem _favorItem = new ToolStripMenuItem("즐겨찾기 추가");
        private bool _isShow = false;

        private OptionDictionary _OptionDictionary;
        private string _ApplyCode = "";
        private string _ApplySpread = "";
        private string _ApplyProcedure = "";
        private string _CustomParameter = "";
        private string _CodeColumn = "CODE";
        private string _NameColumn = "CODE_NAME";
        private string _PopupTitle = "";

        public TYCodeBoxPopup()
            : base()
        {
            InitializeComponent();

            this.SetPopupStyle();

            this.ProgramNo = "TYERGB003P";
            this.FormClosing += new FormClosingEventHandler(TYCodeBoxPopup_FormClosing);
            this.PreLoad += new EventHandler(TYCodeBoxPopup_PreLoad);
            this._favorItem.Click += new EventHandler(_favorItem_Click);
        }

        private void TYCodeBoxPopup_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.CBO02_CODE_SCH_TYPE.SetValue("1");
            this.SaveSearchCondition();
            this._isShow = false;
        }

        private void TYCodeBoxPopup_PreLoad(object sender, EventArgs e)
        {
            this._isShow = true;

            if (_ApplySpread.Length == 0) return;

            this.FPS91_TY_S_GB_2422G248.FactoryID = _ApplySpread;
        }

        private void TYCodeBoxPopup_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
                return;

            int code_index = this.FPS91_TY_S_GB_2422G248.GetColumnIndex(_CodeColumn);
            int name_index = this.FPS91_TY_S_GB_2422G248.GetColumnIndex(_NameColumn);
            this.LBL51_CODE.SetValue(
                this.FPS91_TY_S_GB_2422G248.Sheets[0].Columns.Get(code_index).Label);
            this.LBL51_CODE_NAME.SetValue(
                this.FPS91_TY_S_GB_2422G248.Sheets[0].Columns.Get(name_index).Label);

            if (this.FPS91_TY_S_GB_2422G248.CurrentContextMenu.Items.IndexOf(this._favorItem) < 0)
            {
                this.FPS91_TY_S_GB_2422G248.CurrentContextMenu.Items.Insert(0, new ToolStripSeparator());
                this.FPS91_TY_S_GB_2422G248.CurrentContextMenu.Items.Insert(0, this._favorItem);
            }

            this.SetSearchCondition();

            if (this._TComboHelper != null)
            {

                //이전 소스 이부분 주석 지우면 코드값이 있어도 전체 조회 기능으로 된다.
                if (string.IsNullOrEmpty(this._TComboHelper.GetText()) || string.IsNullOrEmpty(this._TComboHelper.GetValue().ToString()))
                {
                    this.TXT01_CODE_NAME.SetValue(this._TComboHelper.GetText());
                    this.TXT01_CODE.SetValue(this._TComboHelper.GetValue());
                }

                ////2013.12.26 김영우 대리 수정 - 팝업창에 코드값 있으면 그대로 바인딩
                //this.TXT01_CODE_NAME.SetValue(this._TComboHelper.GetText());
                //this.TXT01_CODE.SetValue(this._TComboHelper.GetValue());


                if (string.IsNullOrEmpty(this._TComboHelper.GetText()) ^ string.IsNullOrEmpty(this._TComboHelper.GetValue().ToString()))
                    this.SetStartingFocus(this.FPS91_TY_S_GB_2422G248);
                else
                    this.SetStartingFocus(this.TXT01_CODE_NAME);
            }

            this.BTN61_INQ_Click(null, null);
        }

        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt;

            dt = this.GetDataSource(
                this.TXT01_CODE_NAME.GetValue().ToString(),
                this.TXT01_CODE.GetValue().ToString());
            dt.TableName = "LIST";
            dt.DataSet.Tables.Remove(dt);
            ds.Tables.Add(dt);

            this.DbConnector.CommandClear();
            this.DbConnector.Attach(
                "TY_P_GB_2449V392"
                , TYUserInfo.EmpNo
                , (this._ApplyCode.Length > 0 ? "C_" + this._ApplyCode : "P_" + this._ApplyProcedure)
                , (this._TComboHelper != null && this._TComboHelper.DummyValue != null ? this._TComboHelper.DummyValue.ToString() : this._CustomParameter)
                );
            dt = this.DbConnector.ExecuteDataTable();
            dt.TableName = "INFO";
            dt.DataSet.Tables.Remove(dt);
            ds.Tables.Add(dt);

            DataColumn dcList = ds.Tables["LIST"].Columns[this._CodeColumn];
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
                        errRow[this._CodeColumn] = "XXXXX";
                        errRow[this._NameColumn] = "등록일시 데이터가 없습니다.";
                        source.Rows.Add(errRow);
                        this.FPS91_TY_S_GB_2422G248.SetValue(source);
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

            this.FPS91_TY_S_GB_2422G248.SetValue(source);

            if (sender != null && this.FPS91_TY_S_GB_2422G248.ActiveSheet.Rows.Count > 0)
                this.SetFocus(this.FPS91_TY_S_GB_2422G248);
        }

        private void BTN61_CLR_Click(object sender, EventArgs e)
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

        private void FPS91_TY_S_GB_2422G248_CellDoubleClick(object sender, CellClickEventArgs e)
        {
            if (e.ColumnHeader) return;
            this.ConfirmEventInterface();
        }

        private void _favorItem_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_GB_2449G388"
                , TYUserInfo.EmpNo
                , (this._ApplyCode.Length > 0 ? "C_" + this._ApplyCode : "P_" + this._ApplyProcedure)
                , (this._TComboHelper != null && this._TComboHelper.DummyValue != null ? this._TComboHelper.DummyValue.ToString() : this._CustomParameter)
                , this.FPS91_TY_S_GB_2422G248.GetValue(this.FPS91_TY_S_GB_2422G248.ActiveRowIndex, this._CodeColumn).ToString()
                , (this.CBO02_CODE_SCH_TYPE.GetValue().ToString() == "2" ? "N" : "Y"));
            this.DbConnector.ExecuteNonQuery();

            this.CBO02_CODE_SCH_TYPE.SetValue("2");
            this.BTN61_INQ_Click(null, null);
        }

        #region IPopupHelper 멤버

        public DataRow SelectedRow
        {
            get { return _SelectedDataRow; }
        }

        public void Initialize_Helper(TCodeBox sender)
        {
            _TComboHelper = sender;
            _SelectedDataRow = null;

            if (!this._TComboHelper.IsHandleCreated) //디자인모드에서 TYUserInfo에서 IP 접속 오류가 나므로 처리, this.DesignMode(TYBase, TYCodeBox 둘 다) 같은걸로 처리 불가
                return;

            this.Initialize_DbConnector();

            if (this._TComboHelper != null)
            {
                this._OptionDictionary = this._TComboHelper.Option;

                if (_OptionDictionary.ContainsKey("C36")) _ApplyProcedure = _OptionDictionary["C36"];
                if (_OptionDictionary.ContainsKey("C37")) _ApplyCode = _OptionDictionary["C37"];
                if (_OptionDictionary.ContainsKey("C67")) _CustomParameter = _OptionDictionary["C67"];
                if (_OptionDictionary.ContainsKey("C74")) _ApplySpread = _OptionDictionary["C74"];
                if (_OptionDictionary.ContainsKey("C75")) _CodeColumn = _OptionDictionary["C75"];
                if (_OptionDictionary.ContainsKey("C76")) _NameColumn = _OptionDictionary["C76"];
                if (_OptionDictionary.ContainsKey("C77")) _PopupTitle = _OptionDictionary["C77"];

                this.Text = _PopupTitle;

                this.SetSearchCondition();

                if (string.IsNullOrEmpty(this._TComboHelper.GetText()) || string.IsNullOrEmpty(this._TComboHelper.GetValue().ToString()))
                {
                    this.TXT01_CODE_NAME.SetValue(this._TComboHelper.GetText());
                    this.TXT01_CODE.SetValue(this._TComboHelper.GetValue());
                }
            }
        }

        public void ShowPopupHelper()
        {
            if (this.IsHandleCreated)
            {
                this.TYCodeBoxPopup_Load(null, null);
                _TComboHelper.CodeBoxPopupHelper_Init();
            }

            base.Show();

            if (string.IsNullOrEmpty(this.TXT01_CODE_NAME.GetValue().ToString()) ^ string.IsNullOrEmpty(this.TXT01_CODE.GetValue().ToString()))
                this.SetFocus(this.FPS91_TY_S_GB_2422G248);
            else
                this.SetFocus(this.TXT01_CODE_NAME);
        }

        public void ConfirmEventInterface()
        {
            if (this.FPS91_TY_S_GB_2422G248.ActiveSheet.Rows.Count == 0)
                return;

            int row = this.FPS91_TY_S_GB_2422G248.ActiveRowIndex;

            string code = this.FPS91_TY_S_GB_2422G248.GetValue(row, _CodeColumn).ToString();
            string name = this.FPS91_TY_S_GB_2422G248.GetValue(row, _NameColumn).ToString();

            _SelectedDataRow = this.FPS91_TY_S_GB_2422G248.GetDataRow(row);

            if (_TComboHelper != null)
            {
                _TComboHelper.SetValue(code, name);
                _TComboHelper.BindedDataRow = _SelectedDataRow;
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach(
                "TY_P_GB_2449G388"
                , TYUserInfo.EmpNo
                , (this._ApplyCode.Length > 0 ? "C_" + this._ApplyCode : "P_" + this._ApplyProcedure)
                , (this._TComboHelper != null && this._TComboHelper.DummyValue != null ? this._TComboHelper.DummyValue.ToString() : this._CustomParameter)
                , code
                , string.Empty);
            this.DbConnector.ExecuteNonQuery();

            this.SaveSearchCondition();

            this.Close();
        }

        public DataTable GetDataSource(params string[] parameters)
        {
            DataTable rtnValue = null;

            string code = parameters[0];
            string text = parameters[1];

            if (code.IndexOf("%") < 0) code = code + "%";
            if (text.IndexOf("%") < 0) text = text + "%";

            this.DbConnector.CommandClear();

            if (_ApplyCode.Length > 0)
                this.DbConnector.Attach("TY_P_GB_24441429", _ApplyCode, text, code);
            else
            {
                if (_ApplyProcedure.Length > 0)
                {
                    string customparameter = _CustomParameter;
                    if (customparameter.Length == 0)
                        customparameter = (_TComboHelper.DummyValue ?? "").ToString();

                    this.DbConnector.ProcedureDirectCall();

                    if (customparameter.Length > 0)
                        this.DbConnector.Attach(_ApplyProcedure, customparameter, text, code);
                    else
                        this.DbConnector.Attach(_ApplyProcedure, text, code);
                }
            }

            if (this.DbConnector.CommandCount == 0)
                throw new Exception("필드사전 옵션에서 해당 코드박스에 정의된 ApplyCode 혹은 ApplyProcedure가 없습니다.");

            rtnValue = this.DbConnector.ExecuteDataTable();
            if (rtnValue.Rows.Count == 1 && !this._isShow)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                    "TY_P_GB_2449G388"
                    , TYUserInfo.EmpNo
                    , (this._ApplyCode.Length > 0 ? "C_" + this._ApplyCode : "P_" + this._ApplyProcedure)
                    , (this._TComboHelper != null && this._TComboHelper.DummyValue != null ? this._TComboHelper.DummyValue.ToString() : this._CustomParameter)
                    , rtnValue.Rows[0][this._CodeColumn].ToString()
                    , string.Empty);
                this.DbConnector.ExecuteNonQuery();
            }

            return rtnValue;
        }

        #endregion

        private void SetSearchCondition()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach(
                "TY_P_GB_25I4G548"
                , TYUserInfo.EmpNo
                , (this._ApplyCode.Length > 0 ? "C_" + this._ApplyCode : "P_" + this._ApplyProcedure)
                , (this._TComboHelper != null && this._TComboHelper.DummyValue != null ? this._TComboHelper.DummyValue.ToString() : this._CustomParameter)
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
                , (this._ApplyCode.Length > 0 ? "C_" + this._ApplyCode : "P_" + this._ApplyProcedure)
                , (this._TComboHelper != null && this._TComboHelper.DummyValue != null ? this._TComboHelper.DummyValue.ToString() : this._CustomParameter)
                , this.CBO02_CODE_SCH_TYPE.GetValue()
                );
            this.DbConnector.ExecuteNonQuery();
        }
    }
}
