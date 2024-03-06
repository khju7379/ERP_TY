using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.Data.OleDb;

namespace TY.ER.HR00
{
    /// <summary>
    /// RFCARD 다운관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2014.11.25 09:57
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BBGV367 : 인사기본사항 조회
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2B77B165 : 파일을 다운 작업을 하시겠습니까?
    ///  TY_M_GB_25UAA711 : 파일을 다운로드하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  AFFILENAME : 파일명
    /// </summary>
    public partial class TYHRKB005B : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRKB005B()
        {
            InitializeComponent();
        }

        private void TYHRKB005B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.TXT01_AFFILENAME.SetValue("C:\\TYC\\INKIBN.mdb");
        }
        #endregion

        #region  Description : 처리 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            int iCnt = 0;

            string sFilePath = "C:\\TYC\\";
            string sFileName = "INKIBN.mdb";

            string sSql = string.Empty;
            string sSABUN = string.Empty;
            string sKBNAME = string.Empty;
            string sRFID = string.Empty;

            if (System.IO.Directory.Exists(sFilePath) == false)
            {
                System.IO.Directory.CreateDirectory(sFilePath);
            }

            if (System.IO.File.Exists(sFilePath + sFileName))
            {
                System.IO.File.Delete(sFilePath + sFileName);
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_5BHGE177");
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.UP_DB_CREATE("INKIBN");

                string connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sFilePath + sFileName;
                OleDbConnection conn = new OleDbConnection(connStr);
                conn.Open();                

                sSql = "CREATE TABLE INKIBN (INSEQ nvarchar(3), INSABUN nvarchar(6),INNAME nvarchar(30),INRFID nvarchar(10))";
                OleDbCommand cmd = new OleDbCommand(sSql, conn);
                cmd.ExecuteNonQuery();                

                sSql = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    iCnt = iCnt + 1;
                    sSABUN = dt.Rows[i]["KBSABUN"].ToString();
                    sKBNAME = dt.Rows[i]["KBHANGL"].ToString();
                    sRFID = dt.Rows[i]["KBRFID"].ToString();

                    sSql = "INSERT INTO INKIBN VALUES( ";
                    sSql = sSql + "'"+ iCnt.ToString() + "',";
                    sSql = sSql + "'" + sSABUN + "',";
                    sSql = sSql + "'" + sKBNAME + "',";
                    sSql = sSql + "'" + sRFID + "')";
                    
                    cmd.CommandText = sSql;
                    cmd.ExecuteNonQuery();
                }
                conn.Close();                
            }
            this.ShowMessage("TY_M_GB_25UAA711");
        }
        #endregion

        #region Description : 처리 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_HR_4BPCH495"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion


        #region  Description : MDB 파일 생성 이벤트
        private string UP_MdbConn(string strMDB_NM)
        {
            string strMDB_PATH = "C:\\TYC\\" + strMDB_NM;
            string strMDB_CONT = @"Provider=Microsoft.JET.OLEDB.4.0;data source=" + strMDB_PATH + ".mdb;";
            return (string)strMDB_CONT;
        }

        private void UP_DB_CREATE(string strMDB_NM)
        {
            string newMDB = UP_MdbConn(strMDB_NM);
            Type objClassType = Type.GetTypeFromProgID("ADOX.Catalog");
            try
            {
                if (objClassType != null)
                {
                    object obj = Activator.CreateInstance(objClassType);
                    obj.GetType().InvokeMember(
                        "Create", System.Reflection.BindingFlags.InvokeMethod, null, obj, new object[] { newMDB }
                    );

                    System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception) { this.ShowMessage("TY_M_HR_4BPCI496"); }
        }
        #endregion


    }
}
