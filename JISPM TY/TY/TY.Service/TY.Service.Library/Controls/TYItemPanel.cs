using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using System.ComponentModel;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using System.Drawing;

namespace TY.Service.Library.Controls
{
    public class TYItemPanel : Panel
    {
        #region struct CodeData
        private struct CodeData
        {
            private string _code;
            private string _codeName;
            private IControlFactory _control;
            private object _dummyValue;

            public CodeData(string code, string codeName, IControlFactory control, object dummyValue)
            {
                this._code = code;
                this._codeName = codeName;
                this._control = control;
                this._dummyValue = dummyValue;
            }

            public string Code
            {
                get { return this._code; }
            }

            public string CodeName
            {
                get { return this._codeName; }
            }

            public IControlFactory Control
            {
                get { return this._control; }
            }

            public object DummyValue
            {
                get { return this._dummyValue; }
            }
        }
        #endregion

        private Dictionary<string, CodeData> _DicControl;
        private Panel _PnlCodeName;
        private string _CurCode;
        private Label _LblCodeName;
        private Panel _PnlControl;

        public TYItemPanel()
            : base()
        {
            this._DicControl = new Dictionary<string, CodeData>();

            this._PnlCodeName = new Panel();
            this._PnlCodeName.Width = 60;
            this._PnlCodeName.Location = new System.Drawing.Point(0, 0);
            this._PnlControl = new Panel();
            this._LblCodeName = new Label();
            this._LblCodeName.Width = this._PnlCodeName.Width - 3;
            this._LblCodeName.Location = new System.Drawing.Point(0, 0);
            this._LblCodeName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._LblCodeName.BackColor = Color.FromArgb(243, 240, 229);
            this._LblCodeName.ForeColor = Color.FromArgb(0, 0, 0);

            this.Controls.Add(this._PnlControl);
            this._PnlCodeName.Controls.Add(this._LblCodeName);
            this.Controls.Add(this._PnlCodeName);

            this.SizeChanged += new EventHandler(TYItemPanel_SizeChanged);
        }

        [DefaultValue(60)]
        public int CodeNameWidth
        {
            get
            {
                return this._PnlCodeName.Width;
            }
            set
            {
                this._PnlCodeName.Width = value;
                this._LblCodeName.Width = this._PnlCodeName.Width - 3;
                this.SetControlsSize();
            }
        }

        public IControlFactory CurControlFactory
        {
            get
            {
                IControlFactory rtnValue = null;

                if (this._DicControl.ContainsKey(this._CurCode))
                    rtnValue = this._DicControl[this._CurCode].Control;

                return rtnValue;

            }
        }

        public Control CurControl
        {
            get { return this.CurControlFactory as Control; }
        }

        public string GetCurCode()
        {
            return this._CurCode;
        }

        public void SetCurCode(string code)
        {
            this._CurCode = code;

            this._LblCodeName.Text = string.Empty;
            Control tmpControl;
            foreach (KeyValuePair<string, CodeData> tmpPair in this._DicControl)
            {
                tmpPair.Value.Control.Initialize();

                tmpControl = tmpPair.Value.Control as Control;
                if (tmpControl != null)
                {
                    if (tmpPair.Value.Control is TCodeBox)
                        ((TCodeBox)tmpPair.Value.Control).Visible = false;
                    else if (tmpPair.Value.Control is TYTextButtonBox)
                        ((TYTextButtonBox)tmpPair.Value.Control).Visible = false;
                    else
                        tmpControl.Visible = false;
                }
            }

            if (this._CurCode != null && this._DicControl.ContainsKey(this._CurCode))
            {
                tmpControl = this._DicControl[this._CurCode].Control as Control;
                if (tmpControl != null)
                {
                    this._LblCodeName.Text = this._DicControl[this._CurCode].CodeName;
                    if (this._DicControl[this._CurCode].Control is TCodeBox)
                    {
                        ((TCodeBox)this._DicControl[this._CurCode].Control).Visible = true;
                        ((TCodeBox)this._DicControl[this._CurCode].Control).DummyValue = this._DicControl[this._CurCode].DummyValue;
                    }
                    else if (this._DicControl[this._CurCode].Control is TYTextButtonBox)
                        ((TYTextButtonBox)this._DicControl[this._CurCode].Control).Visible = true;
                    else
                        tmpControl.Visible = true;
                }
            }
        }

        private void TYItemPanel_SizeChanged(object sender, EventArgs e)
        {
            this.SetControlsSize();
        }

        public void AddControl(string code, string codeName, IControlFactory control)
        {
            this.AddControl(code, codeName, control, null);
        }

        public void AddControl(string code, string codeName, IControlFactory control, object dummyValue)
        {
            this._DicControl.Add(code, new CodeData(code, codeName, control, dummyValue));

            Control tmpControl = control as Control;
            if (tmpControl != null)
            {
                tmpControl.Width = this._PnlControl.Width - 2;
                tmpControl.Height = this._PnlControl.Height;
                tmpControl.Location = new System.Drawing.Point(0, 0);
                this._PnlControl.Controls.Add(tmpControl);
            }
        }

        private void SetControlsSize()
        {
            this._PnlControl.Location = new System.Drawing.Point(this._PnlCodeName.Width, 0);
            this._PnlCodeName.Height = this.Height;
            this._PnlControl.Width = this.Width - (this._PnlCodeName.Width + 2);
            this._PnlControl.Height = this.Height;
            this._PnlControl.Location = new System.Drawing.Point(this._PnlCodeName.Width + 1, 0);
        }

        #region IControlFactory형 메소드

        public bool IsEmpty
        {
            get { return this.GetValue().ToString().Length > 0 ? false : true; }
        }

        public bool IsReadOnly
        {
            get { return !this.Enabled; }
        }

        public object GetValue()
        {
            object rtnValue = null;

            if (this.CurControlFactory != null)
                rtnValue = this.CurControlFactory.GetValue();

            return rtnValue;
        }

        public void SetValue(object value)
        {
            if (this.CurControlFactory != null)
                this.CurControlFactory.SetValue(value);
        }

        public void SetReadOnly(bool read)
        {
            foreach (CodeData codeData in this._DicControl.Values)
                codeData.Control.SetReadOnly(read);
        }

        public void Initialize()
        {
            this.SetCurCode(null);
            foreach (CodeData codeData in this._DicControl.Values)
                codeData.Control.Initialize();
        }

        #endregion
    }
}
