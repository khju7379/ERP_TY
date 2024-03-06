using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Xml;

namespace TY_KCSAPI
{
    public static class TYBase
    {

        /********************************************************************
         * 세관EDI 전자문서 지원 KCSAPI4 Dll
         ********************************************************************/
        //관세청 서버의 URL정보 조회
        [DllImport(@"C:\\KCSAPI4\\KCSAPI4.dll")]
        public static extern string GetSrvrInfo(string USERID, string FromCbtID);

        //통관관련 문서 송신
        [DllImport(@"C:\\KCSAPI4\\KCSAPI4.dll")]
        public static extern string TrsmDocCscl(string USERID, string FromCbtID, string DocCode, string ConversationID, string Payload);

        //요건신청문서 송신
        [DllImport(@"C:\\KCSAPI4\\KCSAPI4.dll")]
        public static extern string TrsmDocReqApre(string USERID, string FromCbtID, string ToCbtID, string DocCode, string ConversationID, string Payload);

        //통관관련 목록 수신
        [DllImport(@"C:\\KCSAPI4\\KCSAPI4.dll")]
        public static extern string RcpnDocLstCscl(string USERID, string FromCbtID);

        //문서번호에 해당하는 XML파일 수신
        [DllImport(@"C:\\KCSAPI4\\KCSAPI4.dll")]
        public static extern string RcpnDocCscl(string USERID, string FromCbtID, string DocCode, string ConversationID);

        //요건확인 목록 수신
        [DllImport(@"C:\\KCSAPI4\\KCSAPI4.dll")]
        public static extern string RcpnDocLstReqApre(string USERID, string FromCbtID);

        //문서번호에 해당하는 XML파일 수신
        [DllImport(@"C:\\KCSAPI4\\KCSAPI4.dll")]
        public static extern string RcpnDocReqApre(string USERID, string FromCbtID, string DocCode, string ConversationID);

        //다중문서 송신(TrsmMltDoc)으로 다량의 통보서가 발생한 경우에만 사용
        [DllImport(@"C:\\KCSAPI4\\KCSAPI4.dll")]
        public static extern string RcpnMltDoc(string USERID, string FromCbtID);

        //인증서 설정
        [DllImport(@"C:\\KCSAPI4\\KCSAPI4.dll")]
        public static extern string LoginSecuMdle(string USERID, string FromCbtID);

        //인증서 해지
        [DllImport(@"C:\\KCSAPI4\\KCSAPI4.dll")]
        public static extern string LogoutSecuMdle();

        public static string ConstKCSAPIPath = "C:\\KCSAPI4";

        //회사구분
        public static string ConstCompany;       
        //전송구분
        public static string ConstTransModul;

        public static string ConstKCSAPI4LoginId;   //관세청 등록 사용자 
        public static string ConstKCSAPI4DocUserId; //관세청 문서 사용자


        #region Description : 관세청 등록 사용자 
        public static string Get_KCSAPI4LoginId(string sComGubn)
        {
            string str = string.Empty;

            switch (sComGubn)
            {
                case "TY":
                    str =  "taeyoungin";
                    break;
                case "TG":
                    str = "ty01610070";
                    break;
                case "PT":
                    str = "pts00001";
                    break;
                default:
                    str = "taeyoungin";
                    break;
            }

            return str;


        }
        #endregion

        #region Description : 관세청 문서 사용자
        public static string Get_KCSAPI4DocUserId(string sComGubn)
        {
            string str = string.Empty;

            switch (sComGubn)
            {
                case "TY":
                    str = "VC610811044901";
                    break;
                case "TG":
                    str = "VC138813149001";
                    break;
                case "PT":
                    str = "VC177880069001";
                    break;
                default:
                    str = "VC610811044901";
                    break;
            }

            return str;            
        }
        #endregion

        #region  Description : KCSAPI4 Xml 문서코드 반환 함수
        public static string UP_Get_XmlToDocCode(string path)
        {
            string sDocCode = string.Empty;

            // Xml 작업을 하기 위한 Xml 문서 생성
            XmlDocument xmlDoc = new XmlDocument();

            // Xml 파일을 불러옵니다.
            xmlDoc.Load(path);

            XmlNodeList elemList = xmlDoc.GetElementsByTagName("wco:TypeCode");

            if (elemList.Count > 0)
            {
                sDocCode = elemList[0].InnerText;
            }

            return sDocCode;
        }
        #endregion
    }
}
