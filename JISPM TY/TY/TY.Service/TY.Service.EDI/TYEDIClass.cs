using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace TY.Service.EDI
{
    public static class TYEDIClass
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

        public static string UP_Get_Login(string KCSAPI4LoginId, string KCSAPI4DocUserId)
        {
            string Login = LoginSecuMdle(KCSAPI4LoginId, KCSAPI4DocUserId);

            return Login;
        }
    }
}
