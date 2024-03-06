using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;

namespace TY.Service.Library.Controls
{
    public class TYRadioButton : RadioButton, IControlFactory
    {
        public const string PREFIX = "RDB";

        private bool _IsCreated;
        private string _GroupNo;
        private string _FactoryID;
        private string _FactoryName;
        private OptionDictionary _OptionDictionary;

        public TYRadioButton()
            : base()
        {
            _IsCreated = false;
            _GroupNo   = "";
            _FactoryID = "";
            _OptionDictionary = new OptionDictionary();
        }

        #region Description: IControlFactory 멤버

        public bool IsCreated
        {
            get { return _IsCreated; }
        }

        public bool IsEmpty
        {
            get { return this.Checked ? true : false; }
        }

        public bool IsReadOnly
        {
            get { return !this.Enabled; }
        }

        public string FactoryID
        {
            get { return _FactoryID; }
            set { _FactoryID = value; }
        }

        public string FactoryName
        {
            get { return _FactoryName; }
            set { _FactoryName = value; }
        }

        public string FactoryOption
        {
            set { _OptionDictionary = StaticCommon.CreateOptionDictionary(value); }
        }

        public string GroupNo
        {
            get { return _GroupNo; }
        }

        public OptionDictionary Option
        {
            get { return _OptionDictionary; }
        }

        public void GenerateFactoryID()
        {
            if (_FactoryID.Length == 0)
                StaticCommon.ControlNamingRuleCheck(this.Name, PREFIX, ref _FactoryID, ref _GroupNo);
        }

        public object GetValue()
        {
            if (!_OptionDictionary.ContainsKey("C42"))
                return StaticCommon.Bool(this.Checked);
            else
            {
                string failtext = _OptionDictionary.ContainsKey("C43") ? _OptionDictionary["C43"] : "N";
                return this.Checked ? _OptionDictionary["C42"] : StaticCommon.Nvl(failtext, "N");
            }
        }

        public void SetValue(object value)
        {
            if (!_OptionDictionary.ContainsKey("C42"))
                this.Checked = StaticCommon.GetFromBool(value.ToString());
            else
                this.Checked = _OptionDictionary["C42"].Equals(StaticCommon.Nvl(value, "")) ? true : false;
        }

        public void SetReadOnly(bool read)
        {
            this.Enabled = !read;
        }

        public void ControlSetting()
        {
            if (_IsCreated) return;

            if (_OptionDictionary.Count > 0)
            {
                StaticCommon.ControlDefaultSetting(this, _OptionDictionary);

                if (_OptionDictionary.ContainsKey("C01")) StaticCommon.SetFromColor(this.BackColor, _OptionDictionary["C01"]);
                if (_OptionDictionary.ContainsKey("C02")) StaticCommon.SetFromColor(this.ForeColor, _OptionDictionary["C02"]);
                if (_OptionDictionary.ContainsKey("C03")) StaticCommon.SetFromPoint(this.Location,  _OptionDictionary["C03"]);
                if (_OptionDictionary.ContainsKey("C04")) StaticCommon.SetFromSize(this.Size,       _OptionDictionary["C04"]);
                if (_OptionDictionary.ContainsKey("C05")) this.Visible = StaticCommon.GetFromBool(  _OptionDictionary["C05"]);
                if (_OptionDictionary.ContainsKey("C06")) this.Enabled = StaticCommon.GetFromBool(  _OptionDictionary["C06"]);
                
                if (_OptionDictionary.ContainsKey("C11")) this.FlatStyle    = StaticCommon.GetFromFlatStyle(_OptionDictionary["C11"]);
                if (_OptionDictionary.ContainsKey("C12")) this.TextAlign    = StaticCommon.GetFromContentAlignment(_OptionDictionary["C12"]);
                if (_OptionDictionary.ContainsKey("C17")) this.AutoEllipsis = StaticCommon.GetFromBool(_OptionDictionary["C17"]);
                
                if (_OptionDictionary.ContainsKey("C41")) this.CheckAlign  = StaticCommon.GetFromContentAlignment(_OptionDictionary["C41"]);
                if (_OptionDictionary.ContainsKey("C44")) this.Checked     = StaticCommon.GetFromBool(_OptionDictionary["C44"]);
                if (_OptionDictionary.ContainsKey("C13"))
                {
                    this.Text    = _OptionDictionary["C13"];
                    _FactoryName = _OptionDictionary["C13"];
                }

                if (_OptionDictionary.ContainsKey("C45"))
                {
                    this.Text    = _OptionDictionary["C45"];
                    _FactoryName = _OptionDictionary["C45"];
                }
            }

            _IsCreated = true;
        }

        public void Initialize()
        {
            string value = "N";
            if (_OptionDictionary.ContainsKey("C14"))
                value = _OptionDictionary["C14"];

            this.SetValue(value);
        }

        #endregion
    }
}
