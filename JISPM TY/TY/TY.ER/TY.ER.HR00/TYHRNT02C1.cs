using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using System.IO;


namespace TY.ER.HR00
{
    /// <summary>
    /// 연말정산 첨부파일 조회 팝업 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.07.28 08:54
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_77S8Z291 : 연말정산 첨부파일 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_77S8Z293 : 연말정산 첨부파일 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    /// </summary>
    public partial class TYHRNT02C1 : TYBase
    {
        private string fsYACOMPANY;
        private string fsYAYEAR;
        private string fsYASABUN;

        #region  Description : 폼 로드 이벤트
        public TYHRNT02C1(string sYACOMPANY, string sYAYEAR, string sYASABUN)
        {
            InitializeComponent();            

            fsYACOMPANY = sYACOMPANY;
            fsYAYEAR = sYAYEAR;
            fsYASABUN = sYASABUN;
        }

        private void TYHRNT02C1_Load(object sender, System.EventArgs e)
        {
            this.UP_Get_DataBinding();
        }
        #endregion

        #region  Description : UP_Get_DataBinding 이벤트
        private void UP_Get_DataBinding()
        {
            try
            {
                int iArraySize = 0;
                byte[] _AttachFile = null;
                string sAFFILENAME = string.Empty;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_77S8Z291", fsYACOMPANY, fsYAYEAR, fsYASABUN);
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_77SH0319", dt.Rows[0]["YACOMPANY"].ToString(),
                                                                dt.Rows[0]["YAYEAR"].ToString(),
                                                                dt.Rows[0]["YASABUN"].ToString(),
                                                                dt.Rows[0]["YASEQ"].ToString());
                    DataTable dtFile = this.DbConnector.ExecuteDataTable();
                    if (dtFile.Rows.Count > 0)
                    {
                        sAFFILENAME = dt.Rows[0]["YADESC"].ToString();
                        _AttachFile = dt.Rows[0]["YAFILEBYTE"] as byte[];
                        iArraySize = _AttachFile.GetUpperBound(0);
                    }
                    this.PDF81_AcroPDF.SetValue(_AttachFile);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        #endregion

        #region Description : 첨부관리 다운로드 이벤트
        private void UP_AttachFileDown(string sYACOMPANY, string sYAYEAR, string sYASABUN, string sYASEQ)
        {
            FileStream stream = null;
            int iArraySize = 0;
            byte[] _AttachFile = null;
            string sAFFILENAME = string.Empty;

            try
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_77SH0319", sYACOMPANY, sYAYEAR, sYASABUN, sYASEQ);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sAFFILENAME = dt.Rows[0]["YADESC"].ToString();
                    _AttachFile = dt.Rows[0]["YAFILEBYTE"] as byte[];
                    iArraySize = _AttachFile.GetUpperBound(0);
                }

                this.saveFileDialog.FileName = sAFFILENAME;
                if (this.saveFileDialog.ShowDialog() == DialogResult.Cancel) return;
                string fileName = this.saveFileDialog.FileName;

                stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                stream.Write(_AttachFile, 0, iArraySize + 1);

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

        #region  Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

       
    }
}
