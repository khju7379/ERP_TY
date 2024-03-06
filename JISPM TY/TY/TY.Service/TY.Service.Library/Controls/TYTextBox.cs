using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using System.Drawing;

namespace TY.Service.Library.Controls
{
    public class TYTextBox : TTextBox, IControlFactory
    {
        public TYTextBox()
            : base()
        {
            //this.Font = new System.Drawing.Font("맑은 고딕", 8F);
        }

        #region IControlFactory 재정의

        new public void ControlSetting()
        {
            if (base.Option.ContainsKey("C21"))
            {
                if (base.Option["C21"] == "01" || base.Option["C21"] == "02") //Alphabet || AlphabetOnly
                {
                    if (!base.Option.ContainsKey("C29"))
                        base.Option.Add("C29", "");

                    base.Option["C29"] = base.Option["C21"] == "01" ? @"[a-zA-Z0-9\b]" : @"[a-zA-Z\b]";

                    base.Option["C21"] = "06";  //Regular
                }
           }

            base.ControlSetting();
        }

        #endregion

        protected override void OnReadOnlyChanged(System.EventArgs e)
        {
            base.OnReadOnlyChanged(e);

            if (this.ReadOnly)
                this.BackColor = Color.FromArgb(255, 240, 240, 240);
            else if(!this.Focused)
                this.BackColor = Color.FromArgb(255, 255, 255, 255);
        }

        protected override void OnBackColorChanged(System.EventArgs e)
        {
            base.OnBackColorChanged(e);

            if (this.ReadOnly)
                this.BackColor = Color.FromArgb(255, 240, 240, 240);
            else if (!this.Focused)
                this.BackColor = Color.FromArgb(255, 255, 255, 255);
        }
    }
}
