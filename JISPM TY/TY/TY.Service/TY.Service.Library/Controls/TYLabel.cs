using System;
using System.Data;
using System.Drawing;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;

namespace TY.Service.Library.Controls
{
    public class TYLabel : TLabel, IControlFactory
    {
        public TYLabel()
            : base()
        {
        }

        #region IControlFactory 재정의

        new public void ControlSetting()
        {
            string c01 = "243,240,229";            
            string c02 = "0,0,0";

            #region 대메뉴별 기본 색상 변경 구현 실패
            //TYBase form = (TYBase)this.FindForm();
            //if (form != null && form.OwnerMDI != null)
            //{
            //    string[] backColors = { "243,240,229", "215,228,242" };
            //    string[] foreColors = { "0,0,0" };
            //    int curLevel;
            //    DataRow[] upMenus = form.OwnerMDI.UCMM_MenuData.Select(string.Format("PROGRAM_FULL_NAME LIKE '%{0}'", form.ProgramNo));
            //    if (upMenus.Length > 0)
            //    {
            //        for (; ; )
            //        {
            //            upMenus = form.OwnerMDI.UCMM_MenuData.Select(string.Format("MENU_NO = '{0}'", upMenus[0]["UP_MENU_NO"]));

            //            if (upMenus.Length == 0)
            //                break;

            //            curLevel = int.TryParse(Convert.ToString(upMenus[0]["LEVEL_NO"]), out curLevel) ? curLevel : -1;

            //            if (curLevel < form.OwnerMDI.UCMM_LeftMenuDepth - 1)
            //                break;
            //            else if (curLevel > form.OwnerMDI.UCMM_LeftMenuDepth - 1)
            //                continue;

            //            int idx = 0;
            //            foreach (DataRow dr in form.OwnerMDI.UCMM_MenuData.Select(string.Format("LEVEL_NO = '{0}'", form.OwnerMDI.UCMM_LeftMenuDepth - 1)))
            //            {
            //                if (Convert.ToString(dr["MENU_NO"]) == Convert.ToString(upMenus[0]["MENU_NO"]))
            //                {
            //                    c01 = backColors[idx % backColors.Length];
            //                    c02 = foreColors[idx % foreColors.Length];
            //                    break;
            //                }

            //                idx++;
            //            }
            //        }
            //    }
            //}
            #endregion

            if (!base.Option.ContainsKey("C01"))
                base.Option.Add("C01", c01);

            if (!base.Option.ContainsKey("C02"))
                base.Option.Add("C02", c02);

            if (!base.IsCreated)
            {
                if (base.Option.ContainsKey("C01"))
                {
                    if (base.Option["C01"].Length == 0) return;
                    string[] rgb = base.Option["C01"].Replace(" ", "").Split(',');
                    if (rgb.Length != 3) return;

                    int r = int.Parse(rgb[0]);
                    int g = int.Parse(rgb[1]);
                    int b = int.Parse(rgb[2]);

                    this.BackColor = Color.FromArgb(r, g, b);
                }

                if (base.Option.ContainsKey("C02"))
                {
                    if (base.Option["C02"].Length == 0) return;
                    string[] rgb = base.Option["C02"].Replace(" ", "").Split(',');
                    if (rgb.Length != 3) return;

                    int r = int.Parse(rgb[0]);
                    int g = int.Parse(rgb[1]);
                    int b = int.Parse(rgb[2]);

                    this.ForeColor = Color.FromArgb(r, g, b);
                }
            }

            base.ControlSetting();
        }
        
        #endregion
    }
}
