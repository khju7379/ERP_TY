using System;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;

namespace TY.Service.Library.Controls.TYSpreadCellType
{
    public class TYTextCellType : TTextCellType
    {
        public override void StartEditing(EventArgs e, bool selectAll, bool autoClipboard)
        {
            base.StartEditing(e, true, autoClipboard);
        }
    }
}
