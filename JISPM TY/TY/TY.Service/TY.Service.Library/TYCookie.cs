using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;

namespace TY.Service.Library
{
    public static class TYCookie
    {
        private static string _Year;
        private static string _Branch;
        //private static string _RptGubn;
        private static string _Confgb;
        private static string _Chk;
        public static string Year { get { return TYCookie._Year; } }
        public static string Branch { get { return TYCookie._Branch; } }
        //public static string RptGubn { get { return TYCookie._RptGubn; } }
        public static string Confgb { get { return TYCookie._Confgb; } }
        public static string Chk { get { return TYCookie._Chk; } }

        static TYCookie()
        {
            TYCookie.Reset();
        }

        public static void Reset()
        {
            TYCookie._Year    = DateTime.Now.ToString("yyyy");
            TYCookie._Branch  = "1";
            //TYCookie._RptGubn = "1";
            TYCookie._Confgb  = "11";
        }

        public static void Save(string sYEAR, string sBRANCH, string sCONFGB)
        {
            TYCookie._Chk     = "Cookie";
            TYCookie._Year    = sYEAR.ToString();
            TYCookie._Branch  = sBRANCH.ToString();
            //TYCookie._RptGubn = sRptGubn.ToString();

            TYCookie._Confgb  = sCONFGB.ToString();
        }
    }
}