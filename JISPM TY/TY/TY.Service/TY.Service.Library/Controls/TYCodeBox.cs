using System;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;

namespace TY.Service.Library.Controls
{
    public class TYCodeBox : TCodeBox
    {
        private bool _isSpreadCodeHelper = false;
        private bool _occurCodeBoxDataBinded = false;
        private bool _keyShift = false;

        public TYCodeBox()
            : base()
        {
            //this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            //this.CodeText.Font = new System.Drawing.Font("맑은 고딕", 9F);

            this.SetIPopupHelper(new TYCodeBoxPopup());
        }

        public override void ControlSetting()
        {
            if (!this.Option.ContainsKey("C39"))
                this.Option.Add("C39", "TY.ER.GB00.TYERGB003P");
            
            base.ControlSetting();
        }

        internal bool IsSpreadCodeHelper
        {
            get { return this._isSpreadCodeHelper; }
            set { this._isSpreadCodeHelper = value; }
        }

        /// <summary>
        /// PopupHelper 표시
        /// </summary>
        public void ShowPopupHelper()
        {
            this.OnKeyDown(new KeyEventArgs(Keys.F1));
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            this._keyShift = e.Shift;
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            this._occurCodeBoxDataBinded = false;

            base.OnKeyPress(e);

            if (e.KeyChar == 13 && this.Focused && !this._occurCodeBoxDataBinded && this.FindForm() is TYBase)
            {
                ((TYBase)this.FindForm()).SetControlFocus(this, (this._keyShift ? -1 : 1));
                this.Select(0, 0);
            }
            
            this._occurCodeBoxDataBinded = false;
            this._keyShift = false;
        }

        public override void OnCodeKeyDown(object sender, KeyEventArgs e)
        {
            base.OnCodeKeyDown(sender, e);
            this._keyShift = e.Shift;
        }

        public override void OnCodeKeyPress(object sender, KeyPressEventArgs e)
        {
            this._occurCodeBoxDataBinded = false;

            base.OnCodeKeyPress(sender, e);

            if (e.KeyChar == 13 && this.CodeText.Focused && !this._occurCodeBoxDataBinded && this.FindForm() is TYBase)
                ((TYBase)this.FindForm()).SetControlFocus(this.CodeText, (this._keyShift ? -1 : 1));

            this._occurCodeBoxDataBinded = false;
            this._keyShift = false;
        }

        public override void OnCodeBoxDataBinded(object sender, EventArgs e)
        {
            base.OnCodeBoxDataBinded(sender, e);
            this._occurCodeBoxDataBinded = true;
        }
    }
}
