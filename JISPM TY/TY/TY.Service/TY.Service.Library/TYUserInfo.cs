using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using System.Windows.Forms;

namespace TY.Service.Library
{
    public static class TYUserInfo
    {
        private static string _empNo;
        private static string _userID;
        private static string _userName;
        private static string _deptCode;
        private static string _deptName;
        private static string _perAuth;
        private static string _SecureKey;
        
        public static string EmpNo { get { return TYUserInfo._empNo; } }
        public static string UserID { get { return TYUserInfo._userID; } }
        public static string UserName { get { return TYUserInfo._userName; } }
        public static string DeptCode { get { return TYUserInfo._deptCode; } }
        public static string DeptName { get { return TYUserInfo._deptName; } }
        public static string PerAuth { get { return TYUserInfo._perAuth; } }
        public static string SecureKey { get { return TYUserInfo._SecureKey; } }
        

        static TYUserInfo()
        {
            TYUserInfo.Reset();
        }

        public static void Reset()
        {
            TYUserInfo._empNo = Employer.EmpNo;
            TYUserInfo._userID = Employer.UserID;
            TYUserInfo._userName = Employer.UserName;

        
            if (EmpNo.Length > 6)
            {
                TYUserInfo._deptCode = string.Empty;
                TYUserInfo._deptName = string.Empty;
            }
            else
            {
                DbConnector dbConnector = new DbConnector(CurrentSystem.VirtualProgram);
                //dbConnector.Attach("TY_P_GB_24G9S659", EmpNo);
                //2014.12.31
                dbConnector.Attach("TY_P_GB_4CVJ7024", DateTime.Now.ToString("yyyyMMdd"), EmpNo);
                DataTable deptInfo = dbConnector.ExecuteDataTable();

                if (deptInfo.Rows.Count > 0)
                {
                    TYUserInfo._deptCode = Convert.ToString(deptInfo.Rows[0]["KBBUSEO"]);
                    TYUserInfo._deptName = Convert.ToString(deptInfo.Rows[0]["KBBUSEONM"]);
                    TYUserInfo._perAuth = Convert.ToString(deptInfo.Rows[0]["KBPERAUTH"]);
                }

                //db 암호화 키 값 받아오기
                dbConnector.CommandClear();
                dbConnector.Attach("TY_P_GB_B538N276");
                DataTable dbkey = dbConnector.ExecuteDataTable();

                if (dbkey.Rows.Count > 0)
                {
                    TYUserInfo._SecureKey = Convert.ToString(dbkey.Rows[0]["key"]);
                }
            }
        }
    }
}
