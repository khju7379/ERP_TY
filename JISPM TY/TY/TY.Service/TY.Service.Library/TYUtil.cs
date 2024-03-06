using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Security.AccessControl;

namespace TY.Service.Library
{
    public static class TYUtil
    {
        public static void SetRegistry(string path, string name, string data)
        {
            try
            {
                RegistryKey home = Registry.LocalMachine.OpenSubKey(path,
                    RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.SetValue);

                if (home == null)
                {
                    home = Registry.LocalMachine.CreateSubKey(path, RegistryKeyPermissionCheck.ReadWriteSubTree);
                }

                home.SetValue(name, data, RegistryValueKind.String);
            }
            catch (Exception exc)
            {
                
            }
        }
    }
}
