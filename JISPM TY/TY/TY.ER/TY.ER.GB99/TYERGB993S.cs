using System;
using TY.Service.Library;
using System.Runtime.InteropServices;
using System.IO;
using Shoveling2010.SmartClient.SystemUtility.Library;
using System.Windows.Forms;
using System.Text;
using System.Xml;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;

namespace TY.ER.GB99
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TYERGB993S : TYBase
    {
        private static string _assemblyName;

        private string[] _NodeValueList;
        private string _NodeString;
        private int fiArrayIndex = 0;

        //[DllImport(@"D:\KCSIPTModule.dll")]
        //private static extern Int32 ReceiveDocBatch(string USERID, string cBTID, string bfilename);
        //[DllImport(@"D:\KCSIPTModule.dll")]
        //private static extern string TransferDocFile(string USERID, string cBTID, string doccode, string convid, string payload);

        //[DllImport(@".\KCSAPI4.dll")]
        //private static extern string TrsmDocReqApre(string USERID, string FromCbtID, string ToCbtID, string DocCode, string ConversationID, string Payload);

        //[DllImport(@".\KCSAPI4.dll")]
        //private static extern string GetSrvrInfo(string USERID, string FromCbtID);

        //[DllImport(@".\KCSAPI4.dll")]
        //private static extern string TrsmDocCscl(string USERID, string FromCbtID, string DocCode, string ConversationID, string Payload);

        //[DllImport(@".\KCSAPI4.dll")]
        //private static extern string RcpnDocLstCscl(string USERID, string FromCbtID);

        //[DllImport(@".\KCSAPI4.dll")]
        //private static extern string RcpnDocCscl(string USERID, string FromCbtID, string DocCode, string ConversationID);

        //[DllImport(@".\KCSAPI4.dll")]
        //private static extern string RcpnDocReqApre(string USERID, string FromCbtID, string DocCode, string ConversationID);

        //[DllImport(@".\KCSAPI4.dll")]
        //private static extern string RcpnDocLstReqApre(string USERID, string FromCbtID);

        //[DllImport(@".\KCSAPI4.dll")]
        //private static extern string LoginSecuMdle(string USERID, string FromCbtID);

        //[DllImport(@".\KCSAPI4.dll")]
        //private static extern string LogoutSecuMdle();


        public TYERGB993S()
        {
            InitializeComponent();
        }

        private void TYERGB993S_Load(object sender, EventArgs e)
        {
            _assemblyName = CurrentSystem.DeployUrl;          
            
        }

        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            //string xml = TYEdiLayout.UP_Get_XmlGOVCBR6NB();

            //StreamWriter sw = new StreamWriter("C:\\KCSAPI4\\test.xml", false, Encoding.Default);
            //sw.WriteLine(xml);
            //sw.Close();
            
            //string Login = LoginSecuMdle("taeyoungin", "VC610811044901");

            //string ret = GetSrvrInfo("taeyoungin", "VC610811044901");

            //반입,반출 XML 전송 예제
            //string ret2 = TrsmDocCscl("taeyoungin", "VC610811044901", "GOVCBR6NB", "110110551800005283", "C:\\Users\\Administrator\\Documents\\인사\\20180725110110551800005283.xml");

            //반입,반출 XML 수신 예제
            //string ret33 = RcpnDocLstCscl("taeyoungin", "VC610811044901");

            //string ret2 = RcpnDocCscl("taeyoungin", "VC610811044901", "GOVCBRRAQ", "2018072614374620180726-ELI-d3f3bd1f-7d51-41ce-86c0-d8d2b5f46b10");

            //string ret2 = RcpnDocReqApre("taeyoungin", "VC610811044901", "GOVCBRR20", "20180726-ELI-c86e154d-ab54-45d0-b96d-ea3811e7daa7");
            
            //LogoutSecuMdle();


            //string  ret2 = TrsmDocReqApre("taeyoungin", "VC610811044901", "dddd", "GOVCBR6NB", "110110551800005283", "C:\\Users\\Administrator\\Documents\\인사\\20180726110110551800005283.xml");

            //Int32 ret2 = TrsmDocReqApre("taeyoungin", "VC610811044901", "", "", "", "");

            //Int32 ret = ReceiveDocBatch("taeyoungin", "VC123120000303", "");

            //string ret = RcpnMltDoc("taeyoungin", "VC610811044901");


            //this.textBox2.Text = ret.ToString();

            //if (this.OFD01_SOURCEFILE.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //  this.textBox2.Text = this.OFD01_SOURCEFILE.FileName; 


            //var adds = new List<List<string>>();

            //XmlDocument xml = new XmlDocument(); // XmlDocument 생성
            //xml.Load("D:\\201602181058481611001.xml");
            //XmlNodeList xnList = xml.GetElementsByTagName("rsm:Document"); //접근할 노드

            //foreach (XmlNode xn in xnList)
            //{
            //    List<string> add = new List<string>();
            //    add.Add(xn["ram:TypeCode"].InnerText);

            //    adds.Add(add);
            //}

            //lstAddress.BeginUpdate();
            //foreach (var row in adds)
            //{
            //    var item = new ListViewItem(lstAddress.Items.Count.ToString());
            //    item.SubItems.AddRange(row.ToArray());
            //    lstAddress.Items.Add(item);
            //}
            //lstAddress.EndUpdate();


            //ParseXml("C:\\KCSAPI4\\20180727\\Rcv\\2018072614374620180726-ELI-d3f3bd1f-7d51-41ce-86c0-d8d2b5f46b10.xml");

        }

        private  void ParseXml(String path)
        {
            fiArrayIndex = 0;

            // Xml 작업을 하기 위한 Xml 문서 생성
            XmlDocument xmlDoc = new XmlDocument();
            
            // Xml 파일을 불러옵니다.
            xmlDoc.Load(path);            

            // 자식 노드를 모두 순환합니다.
            foreach (XmlNode n in xmlDoc.ChildNodes)
            {
                FetchNodes(n, false);
            }

            _NodeValueList = _NodeString.Split('/');


        }
        private  void FetchNodes(XmlNode n, bool isChild)
        {

            // 노드가 null 인 경우, 함수 실행을 종료한다.
            if (n == null) return;

            // 하위 노드를 다 순환한다.
            foreach (XmlNode n2 in n.ChildNodes)
            {

                // Xml 요소가 아닌 Text 등의 값이면 순환하지 않는다.
                if (n2.GetType() != typeof(XmlElement)) continue;

                // 순환하는 모든 노드는 자식으로 처리
                FetchNodes(n2, true);
            }

            string dd1 = n.ParentNode.Name;            

            // 자식이고, 자식 갯수가 하나면서, Xml 요소가 아닌 경우엔 요소 이름과 값 출력
            if (isChild && n.ChildNodes.Count == 1 && n.ChildNodes[0].GetType() != typeof(XmlElement))
            {
                Console.WriteLine("요소: {0} / 값: {1}", n.Name, n.InnerText);
                _NodeString += n.Name + "," + n.InnerText + "," + n.ParentNode.Name+"/";
            }
            else
            {
                Console.WriteLine();
            }
        }


        private void BTN01_BATCH_Click(object sender, EventArgs e)
        {
            if (this.SFD01_TARGETFILE.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            int handle = 0;
            string password = "88888888";
            int option = 1;

            this.textBox1.AppendText("C# DSFC_EncryptFile(); Test ====================\r\n");
            // @. 호출부
            int ret = DSFC_EncryptFile(handle, this.OFD01_SOURCEFILE.FileName, this.SFD01_TARGETFILE.FileName + ".enc", password, option);
            this.textBox1.AppendText(string.Format("DSFC_EncryptFile(); return code : {0}\r\n", ret));

            this.textBox1.AppendText("C# DSFC_EncryptData(); Test ====================\r\n");
            // 데이터 파일 읽기
            Stream stream = this.OFD01_SOURCEFILE.OpenFile();
            BinaryReader r = new BinaryReader(stream);
            int dataLen = (int)stream.Length;
            byte[] buf = r.ReadBytes((int)stream.Length);
            stream.Close();

            // @. 호출부
            ret = DSFC_EncryptData(handle, buf, dataLen, this.SFD01_TARGETFILE.FileName+".denc", password, option);
            this.textBox1.AppendText(string.Format("DSFC_EncryptData(); return code : {0}\r\n", ret));
        }

        [DllImport("fcrypt_e.dll", EntryPoint = "DSFC_EncryptFile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern int DSFC_EncryptFile(int hWnd, string inputPath, string outputPath, string password, int option);

        [DllImport("fcrypt_e.dll", EntryPoint = "DSFC_EncryptData", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern int DSFC_EncryptData(int hWnd, byte[] inputData, int dataLen, string outputPath, string password, int option);
    }
}
