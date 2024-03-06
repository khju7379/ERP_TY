using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.IO;

namespace TY.ER.UT00
{
    /// <summary>
    /// 전자세금계산서 거래명세서 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2018.01.31 13:06
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    /// </summary>
    public partial class TYUTME24C3 : TYBase
    {
        private string fsMCCONVERSATION_ID;

        #region  Description : 폼 로드 이벤트
        public TYUTME24C3(string sMCCONVERSATION_ID)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsMCCONVERSATION_ID = sMCCONVERSATION_ID;
        }

        private void TYUTME24C3_Load(object sender, System.EventArgs e)
        {
            UP_InvociePrt();
        }
        #endregion

        #region  Description : 거래명세서 출력 이벤트
        private void UP_InvociePrt()
        {
            FileStream stream = null;

            try
            {
                //Invocie 출력
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_7768R018", fsMCCONVERSATION_ID);
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    string sFileName = dt.Rows[0]["FILE_NAME"].ToString();
                    byte[] _AttachFile = dt.Rows[0]["FILE_BINARY"] as byte[];
                    int ArraySize = _AttachFile.GetUpperBound(0);
                    this.PDF81_AcroPDF.SetValue(_AttachFile);
                }
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


        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {            
            this.Close();
        }
        #endregion

       
    }
}
