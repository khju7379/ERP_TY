using System.Drawing;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;

namespace TY.Service.Library.Controls
{
    public class TYButton : TButton, IControlFactory
    {
        #region IControlFactory 재정의

        new public void ControlSetting()
        {
            if (!base.Option.ContainsKey("C01"))
                this.BackColor = SystemColors.ButtonFace;

            if (!base.Option.ContainsKey("C02"))
                base.Option.Add("C02", "0,0,0");
            
            this.Font = new Font("굴림", 9, FontStyle.Bold);

            base.ControlSetting();
        }

        #endregion
    }
}
