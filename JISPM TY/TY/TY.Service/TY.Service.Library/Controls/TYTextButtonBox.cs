using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace TY.Service.Library.Controls
{
    public class TYTextButtonBox : TYTextBox, IControlFactory
    {
        private Button _btnPoup;
        private TYTextBox _txtText;
        private bool _textBoxVisible = true;

        public TYTextButtonBox()
            : base()
        {
            this._btnPoup = new Button();
            this._txtText = new TYTextBox();
        }

        #region Description: IControlFactory 멤버

        new public void ControlSetting()
        {
            this._btnPoup.Text = "";
            this._btnPoup.TabIndex = this.TabIndex;
            this._btnPoup.Image = global::TY.Service.Library.Properties.Resources.magnifier;
            this._btnPoup.Size = new Size(21, 21);
            this._btnPoup.UseVisualStyleBackColor = true;
            this._btnPoup.Anchor = this.Anchor;

            this._txtText.TabIndex = this.TabIndex;
            this._txtText.Anchor = this.Anchor;
            this._txtText.Name = string.Format("TXTCODE_{0}", this.Name);

            base.ControlSetting();
            
            this.Parent.Controls.Add(this._btnPoup);
            this.Parent.Controls.Add(this._txtText);
            this.SetTextButtonBoxStyle();
            
            this.Enabled = base.Enabled;
        }

        #endregion

        new public bool Enabled
        {
            get
            {
                return base.Enabled;
            }
            set
            {
                base.Enabled = value;
                this._btnPoup.Enabled = value;
                this._txtText.Enabled = value;
            }
        }

        [DefaultValue(true)]
        public bool TextBoxVisible
        {
            get
            {
                return this._textBoxVisible;
            }
            set
            {
                this._textBoxVisible = value;
                this._txtText.Visible = value;
            }
        }

        new public bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                base.Visible = value;
                this._btnPoup.Visible = value;
                this._txtText.Visible = value && this._textBoxVisible;
            }
        }

        public Button Button
        {
            get { return this._btnPoup; }
        }

        public TYTextBox TextBox
        {
            get { return this._txtText; }
        }

        private void SetTextButtonBoxStyle()
        {
            if (this.DesignMode || !base.IsCreated)
                return;

            this.Width = this.Width - this._btnPoup.Width - (this._textBoxVisible ? this._txtText.Width : 0);
            this._btnPoup.Location = new Point(this.Location.X + this.Width, this.Location.Y - 1);
            this._txtText.Location = new Point(this.Location.X + this.Width + this._btnPoup.Width, this.Location.Y);
        }
    }
}
