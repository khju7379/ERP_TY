using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.IO;
using System.Windows.Forms;



namespace TY.ER.HR00
{
    /// <summary>
    /// 사진등록 팝업 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2014.11.17 16:11
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BHFB430 : 첨부파일 저장
    ///  TY_P_HR_4BHFB431 : 첨부파일 삭제
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  REM : 삭제
    ///  SAV : 저장
    ///  SEARCH : 찾아보기
    ///  AFFILEGUBN : 파일구분
    ///  AFDESC : 파일내용
    ///  AFFILENAME : 파일명
    ///  AFFILESIZE : 파일용량
    ///  AFSABUN : 사번
    ///  AFSEQ : 순번
    /// </summary>
    public partial class TYHRKB002P : TYBase
    {
        private string fsKBSABUN;

        private TYData DAT30_AFSABUN;
        private TYData DAT30_AFSEQ;
        private TYData DAT30_AFFILEGUBN;
        private TYData DAT30_AFDESC;
        private TYData DAT30_AFFILENAME;
        private TYData DAT30_AFFILESIZE;
        private TYData DAT30_AFFILEBYTE;
        private TYData DAT30_AFHISAB;

        private byte[] _fbAttachFile;

        #region Description : 폼 로드 이벤트
        public TYHRKB002P(string sKBSABUN)
        {
            _fbAttachFile = null;

            InitializeComponent();

            this.fsKBSABUN = sKBSABUN;

            this.DAT30_AFSABUN = new TYData("DAT30_AFSABUN", null);
            this.DAT30_AFSEQ = new TYData("DAT30_AFSEQ", null);
            this.DAT30_AFFILEGUBN = new TYData("DAT30_AFFILEGUBN", null);
            this.DAT30_AFDESC = new TYData("DAT30_AFDESC", null);
            this.DAT30_AFFILENAME = new TYData("DAT30_AFFILENAME", null);
            this.DAT30_AFFILESIZE = new TYData("DAT30_AFFILESIZE", null);
            this.DAT30_AFFILEBYTE = new TYData("DAT30_AFFILEBYTE", null);
            this.DAT30_AFHISAB = new TYData("DAT30_AFHISAB", null);

        }

        private void TYHRKB002P_Load(object sender, System.EventArgs e)
        {
            this.ControlFactory.Add(this.DAT30_AFSABUN);
            this.ControlFactory.Add(this.DAT30_AFSEQ);
            this.ControlFactory.Add(this.DAT30_AFFILEGUBN);
            this.ControlFactory.Add(this.DAT30_AFDESC);
            this.ControlFactory.Add(this.DAT30_AFFILENAME);
            this.ControlFactory.Add(this.DAT30_AFFILESIZE);
            this.ControlFactory.Add(this.DAT30_AFFILEBYTE);
            this.ControlFactory.Add(this.DAT30_AFHISAB);

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.UP_Run();            
        }
        #endregion

        #region Description : UP_Run 이벤트
        private void UP_Run()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BHFV434", this.fsKBSABUN, "1" ,"");
            DataSet ds = this.DbConnector.ExecuteDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.CBH01_AFSABUN.SetValue(ds.Tables[0].Rows[0]["AFSABUN"].ToString());
                this.TXT01_AFSEQ.SetValue(ds.Tables[0].Rows[0]["AFSEQ"].ToString());
                this.CBO01_AFFILEGUBN.SetValue(ds.Tables[0].Rows[0]["AFFILEGUBN"].ToString());
                this.TXT01_AFFILENAME.SetValue(ds.Tables[0].Rows[0]["AFFILENAME"].ToString());
                this.TXT01_AFDESC.SetValue(ds.Tables[0].Rows[0]["AFDESC"].ToString());
                this.TXT01_AFFILESIZE.SetValue(ds.Tables[0].Rows[0]["AFFILESIZE"].ToString());

                this.UP_AttachFile_Display(ds.Tables[0]);

                this.BTN61_DWN.Visible = true;
            }
            else
            {
                this.UP_FileClear();

                this.UP_Save_Seq();

                this.BTN61_DWN.Visible = false;
            }
        }
        #endregion

        #region Description : UP_FileClear 이벤트
        private void UP_FileClear()
        {
            this.CBH01_AFSABUN.SetValue(this.fsKBSABUN);

            this.TXT01_AFSEQ.SetValue("");
            this.CBO01_AFFILEGUBN.SetValue("1");
            this.TXT01_AFFILENAME.SetValue("");
            this.TXT01_AFDESC.SetValue("");
            this.TXT01_AFFILESIZE.SetValue("");

            this.PBX01_IMG.Image = TY.ER.HR00.Properties.Resources.nophoto;
        }
        #endregion

        #region Description : 종료 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 삭제 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BHFB431", this.CBH01_AFSABUN.GetValue().ToString(), TXT01_AFSEQ.GetValue(),CBO01_AFFILEGUBN.GetValue().ToString());
            this.DbConnector.ExecuteNonQuery();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.UP_FileClear();
        }
        #endregion

        #region Description : 저장 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {            

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BHFB430", this.ControlFactory, "30");

            this.DbConnector.ExecuteTranQuery();

            this.ShowMessage("TY_M_GB_23NAD873");

            UP_Run();
        }
        #endregion

        #region Description : 찾아보기 이벤트
        private void BTN61_SEARCH_Click(object sender, EventArgs e)
        {
            this.TXT01_AFFILENAME.SetValue("");

            OpenFileDialog.Filter = "JPG(*.jpg)|*.jpg|JPEG(*.jpeg)|*.jpeg|GIF(*.gif)|*.gif|비트맵 파일(*.bmp)|*.bmp|TIFF(*.tif)|*.tif|All Files (*.*)|*.*";

            if (this.OpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_ATTACH_FILENAME.Text = this.OpenFileDialog.FileName;

                this.TXT01_AFFILENAME.SetValue(UP_Set_FileName(this.TXT01_ATTACH_FILENAME.Text));
            }            
        }
        #endregion

        #region Description : 이미지 다운 버튼 이벤트
        private void BTN61_DWN_Click(object sender, EventArgs e)
        {
            FileStream stream = null;

            try
            {
                this.saveFileDialog.FileName = this.TXT01_AFFILENAME.GetValue().ToString();
                if (this.saveFileDialog.ShowDialog() == DialogResult.Cancel) return;
                string fileName = this.saveFileDialog.FileName;

                int ArraySize = _fbAttachFile.GetUpperBound(0);
                stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                stream.Write(_fbAttachFile, 0, ArraySize + 1);
                
                this.ShowMessage("TY_M_GB_25UAA711");

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }
        #endregion

        #region Description : BTN61_SAV_ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            //삭제후 저장 이벤트
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BHFB431", this.CBH01_AFSABUN.GetValue().ToString(), TXT01_AFSEQ.GetValue(), CBO01_AFFILEGUBN.GetValue().ToString());
            this.DbConnector.ExecuteNonQuery();

            byte[] _AttachFile = null;

            object _objAttachFile = null;

            string filePath = this.TXT01_ATTACH_FILENAME.GetValue().ToString();

            _AttachFile = UP_Get_Byte(filePath);

            _objAttachFile = _AttachFile;

            int ArraySize = _AttachFile.GetUpperBound(0);

                // 용량체크(1메가)            
                if (ArraySize > 1000000)
                {
                    this.ShowMessage("TY_M_MR_3421W409");

                    e.Successed = false;
                    return;
                }


            this.UP_Save_Seq();

            this.TXT01_AFFILESIZE.SetValue(ArraySize.ToString());
            
            this.DAT30_AFSABUN.SetValue(this.CBH01_AFSABUN.GetValue().ToString());
            this.DAT30_AFSEQ.SetValue(this.TXT01_AFSEQ.GetValue());
            this.DAT30_AFFILEGUBN.SetValue(this.CBO01_AFFILEGUBN.GetValue());
            this.DAT30_AFDESC.SetValue(this.TXT01_AFDESC.GetValue());
            this.DAT30_AFFILENAME.SetValue(this.TXT01_AFFILENAME.GetValue());
            this.DAT30_AFFILESIZE.SetValue(this.TXT01_AFFILESIZE.GetValue());
            this.DAT30_AFFILEBYTE.SetValue(_objAttachFile);
            this.DAT30_AFHISAB.SetValue(TYUserInfo.EmpNo);

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : UP_Save_Seq(순번생성) 이벤트
        private void UP_Save_Seq()
        {
            string dd = this.CBH01_AFSABUN.GetValue().ToString();

            // 순번 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_HR_4BHGE435",
                this.CBH01_AFSABUN.GetValue().ToString(),
                this.CBO01_AFFILEGUBN.GetValue().ToString()
                );

            Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

            this.TXT01_AFSEQ.SetValue(Set_Fill3(iCnt.ToString()));

        }
        #endregion

        #region Description : BTN61_REM_ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            //자료 존재 체크

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BHFV434", this.fsKBSABUN, "1", TXT01_AFSEQ.GetValue());
            DataSet ds = this.DbConnector.ExecuteDataSet();

            if (ds.Tables[0].Rows.Count <= 0)
            {
                this.ShowCustomMessage("삭제할수 없는 상태입니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Descrioption : 파일 이름 가져오기
        protected string UP_Set_FileName(string sStr)
        {
            string sValue = "";
            int i = 0;
            int iPoint = 0;
            for (i = 0; i < sStr.Length; i++)
            {
                if (sStr.Substring(i, 1) == "\\")
                {
                    iPoint = i;
                }
            }

            for (i = iPoint + 1; i < sStr.Length; i++)
            {
                sValue = sValue + sStr.Substring(i, 1);
            }

            return sValue;
        }
        #endregion

        #region Description : 이미지 디스플레이
        private void UP_AttachFile_Display(DataTable dt)
        {
            FileStream stream = null;
            byte[] _AttachFile = null;

            try
            {
                string fileName = "c:\\" + dt.Rows[0]["AFFILENAME"].ToString();

                _AttachFile = dt.Rows[0]["AFFILEBYTE"] as byte[];
                _fbAttachFile = _AttachFile;

                int ArraySize = _AttachFile.GetUpperBound(0);

                PBX01_IMG.SetValue(_AttachFile);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }
        #endregion

        #region Description : 첨부파일 byte 변환
        public static byte[] UP_Get_Byte(string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            //FileStream stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            FileStream stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 2000);

            byte[] rawAssembly = new byte[(int)stream.Length];
            stream.Read(rawAssembly, 0, rawAssembly.Length);
            return rawAssembly; // <= byte[] 임
        }
        #endregion

        
    }
}
