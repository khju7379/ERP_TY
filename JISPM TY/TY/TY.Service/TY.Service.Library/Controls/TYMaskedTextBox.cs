using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;

namespace TY.Service.Library.Controls
{
    public class TYMaskedTextBox : MaskedTextBox, IControlFactory
    {
        public const string PREFIX = "MTB";

        private bool _IsCreated;
        private string _GroupNo;
        private string _FactoryID;
        private string _FactoryName;
        private OptionDictionary _OptionDictionary;

        public TYMaskedTextBox()
            : base()
        {
            _IsCreated = false;
            _GroupNo = "";
            _FactoryID = "";
            _OptionDictionary = new OptionDictionary();
        }

        /// <param name="e">이벤트 데이터가 들어 있는 System.Windows.Forms.KeyEventArgs입니다.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Escape)
                this.Initialize();
        }

        #region IControlFactory 정의

        public void ControlSetting()
        {
            if (_IsCreated) return;
            
            this.CutCopyMaskFormat = MaskFormat.IncludeLiterals;
            
            if (_OptionDictionary.Count > 0)
            {
                StaticCommon.ControlDefaultSetting(this, _OptionDictionary);

                if (_OptionDictionary.ContainsKey("C01")) StaticCommon.SetFromColor(this.BackColor, _OptionDictionary["C01"]);
                if (_OptionDictionary.ContainsKey("C02")) StaticCommon.SetFromColor(this.ForeColor, _OptionDictionary["C02"]);
                if (_OptionDictionary.ContainsKey("C03")) StaticCommon.SetFromPoint(this.Location, _OptionDictionary["C03"]);
                if (_OptionDictionary.ContainsKey("C04")) StaticCommon.SetFromSize(this.Size, _OptionDictionary["C04"]);
                if (_OptionDictionary.ContainsKey("C05")) this.Visible = StaticCommon.GetFromBool(_OptionDictionary["C05"]);
                if (_OptionDictionary.ContainsKey("C06")) this.Enabled = StaticCommon.GetFromBool(_OptionDictionary["C06"]);

                if (_OptionDictionary.ContainsKey("C27")) this.ReadOnly = StaticCommon.GetFromBool(_OptionDictionary["C27"]);

                if (_OptionDictionary.ContainsKey("C13"))
                    _FactoryName = _OptionDictionary["C13"];
            }

            _IsCreated = true;
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

        public void GenerateFactoryID()
        {
            if (_FactoryID.Length == 0)
                StaticCommon.ControlNamingRuleCheck(this.Name, PREFIX, ref _FactoryID, ref _GroupNo);
        }

        public object GetValue()
        {
            return this.Text;
        }

        public string GroupNo
        {
            get { return _GroupNo; }
        }

        public void Initialize()
        {
            string value = string.Empty;
            if (_OptionDictionary.ContainsKey("C14"))
                value = _OptionDictionary["C14"];

            this.SetValue(value);
        }

        public bool IsCreated
        {
            get { return _IsCreated; }
        }

        public bool IsEmpty
        {
            get { return this.GetValue().ToString().Length > 0 ? false : true; }
        }

        public bool IsReadOnly
        {
            get { return (!this.Enabled || this.ReadOnly) ? true : false; }
        }

        public OptionDictionary Option
        {
            get { return _OptionDictionary; }
        }

        public void SetReadOnly(bool read)
        {
            this.ReadOnly = read;
        }

        public void SetValue(object value)
        {
            this.Text = Convert.ToString(value);
        }

        #endregion
    }
}
