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
    /// 호봉일괄등록 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.08.28 13:50
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4CGB1823 : 호봉관리 상세내역 등록
    ///  TY_P_HR_4CGB9821 : 호봉관리 마스타 등록
    ///  TY_P_HR_58SB4780 : 호봉마스타 이후자료 체크
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_58SDZ781 : 호봉일관등록 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_25F59464 : 선택한 자료가 없습니다.
    ///  TY_M_HR_4BPFV500 : 파일 경로를 선택해주세요!
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  EXCEL : 엑셀 업데이트
    ///  INQ : 조회
    ///  SEARCH : 찾아보기
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRPY01C2 : TYBase
    {
        #region Description : 폼로드 이벤트
        public TYHRPY01C2()
        {
            InitializeComponent();
        }

        private void TYHRPY01C2_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
        }
        #endregion

        #region Description : 처리 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_58SFU787", this.DTP01_SDATE.GetString().ToString(), TYUserInfo.EmpNo);
            this.DbConnector.Attach("TY_P_HR_58SG4788", this.DTP01_SDATE.GetString().ToString(), TYUserInfo.EmpNo);
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_HR_58SFN786");
        }

        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sNEWDATE = string.Empty;

            sNEWDATE = DTP01_SDATE.GetString().ToString();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_58SGI790");
            DataTable dt =  this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();   
                    this.DbConnector.Attach(
                                           "TY_P_HR_58SB4780",
                                           dt.Rows[i]["TMHBSJKCD"].ToString(),
                                           sNEWDATE,
                                           dt.Rows[i]["TMHBSJKCD"].ToString(),
                                           sNEWDATE
                                           );
                    Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());
                    if (iCnt > 0)
                    {
                        this.ShowCustomMessage("시작일자이후 호봉자료가 있거나 종료되지 않은 호봉자료가 존재합니다! 등록할수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (!this.ShowMessage("TY_M_HR_58SFN785"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 엑셀 버튼 이벤트
        private void BTN61_EXCEL_Click(object sender, EventArgs e)
        {
            if (this.txtFile.Text.Trim() != "")
            {
                this.FPS91_TY_S_HR_58SDZ781.Initialize();

                // TEMP 삭제
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_58SEQ782");
                this.DbConnector.ExecuteTranQuery();

                string strProvider = string.Empty;
                strProvider = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + this.txtFile.Text.Trim() + "; Extended Properties=Excel 12.0";

                string strQuery = "SELECT * FROM [Sheet1$] "; //  , Sheet1$

                OleDbConnection ExcelCon = new OleDbConnection(strProvider);
                ExcelCon.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(strQuery, strProvider);

                DataSet ds = new DataSet();
                adapter.Fill(ds, "EXCEL");

                this.DbConnector.CommandClear();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_58SES783", ds.Tables[0].Rows[i][0].ToString(),
                                                                ds.Tables[0].Rows[i][1].ToString(),
                                                                ds.Tables[0].Rows[i][2].ToString(),
                                                                ds.Tables[0].Rows[i][3].ToString(),
                                                                ds.Tables[0].Rows[i][4].ToString(),
                                                                TYUserInfo.EmpNo );
                }
                this.DbConnector.ExecuteTranQueryList();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_58SET784");
                this.FPS91_TY_S_HR_58SDZ781.SetValue(this.DbConnector.ExecuteDataTable());                                
               
                this.ShowMessage("TY_M_AC_31BAP617");
            }
            else
            {
                this.ShowMessage("TY_M_AC_31B1C623");
            }
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_58SDZ781.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_58SET784");
            this.FPS91_TY_S_HR_58SDZ781.SetValue(this.DbConnector.ExecuteDataTable()); 
        }
        #endregion

        #region Description : 찾아보기 버튼 이벤트
        private void BTN61_SEARCH_Click(object sender, EventArgs e)
        {
            OpenFile.Filter = "Excel 97-2003통합 문서(*.xls)|*.xls|Excel 통합 문서 (.xlsx)|*.xlsx|All Files (*.*)|*.*";

            if (this.OpenFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.txtFile.Text = this.OpenFile.FileName;
        }
        #endregion

        #region Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

    }
}
