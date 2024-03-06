using System.ComponentModel;
using Shoveling2010.SmartClient.SystemUtility.Controls;

namespace TY.Service.Library.Controls
{
    public class TYComboBox : TComboBox
    {
        private bool _editable = false;

        public TYComboBox()
            : base()
        {
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(TYComboBox_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TYComboBox_KeyPress);
        }

        [
        Localizable(true),
        DefaultValue(false)
        ]
        public bool Editable
        {
            get { return this._editable; }
            set { this._editable = value; }
        }

        private void TYComboBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            e.Handled = !this.Editable && e.KeyCode != System.Windows.Forms.Keys.Up && e.KeyCode != System.Windows.Forms.Keys.Down;
        }

        private void TYComboBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            e.Handled = !this.Editable;
        }

        #region Factory Setting 중 이벤트 발생 방지

        protected override void OnSelectedValueChanged(System.EventArgs e)
        {
            if (base.IsCreated)
                base.OnSelectedValueChanged(e);
        }

        protected override void OnSelectedIndexChanged(System.EventArgs e)
        {
            if (base.IsCreated)
                base.OnSelectedIndexChanged(e);
        }

        protected override void OnSelectedItemChanged(System.EventArgs e)
        {
            if (base.IsCreated)
                base.OnSelectedItemChanged(e);
        }

        #endregion
    }
}
