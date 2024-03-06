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
    /// 서울 근태엑셀 Upload 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.05.06 14:58
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_3562W600 : 서울근태엑셀 Upload
    ///  TY_P_HR_3562X601 : 서울근태엑셀 조회
    ///  TY_P_HR_35635603 : 서울 근태엑셀 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_3562Z602 : 서울 근태엑셀 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  EXCEL : 엑셀 업데이트
    ///  INQ : 조회
    ///  SAV : 저장
    ///  SEARCH : 찾아보기
    /// </summary>
    public partial class TYHRES008S : TYBase
    {
        #region Description : 폼로드 
        public TYHRES008S()
        {
            InitializeComponent();
        }

        private void TYHRES008S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.FPS91_TY_S_HR_3562Z602.Initialize();
        }
        #endregion

        #region Description : 엑셀 UPLOAD
        private void BTN61_EXCEL_Click(object sender, EventArgs e)
        {
            if (this.txtFile.Text.Trim() != "")
            {
                this.FPS91_TY_S_HR_3562Z602.Initialize();
                
                // TEMP 삭제
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_35635603");
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
                    this.DbConnector.Attach("TY_P_HR_3562W600", ds.Tables[0].Rows[i][0].ToString(),
                                                                ds.Tables[0].Rows[i][1].ToString(),
                                                                ds.Tables[0].Rows[i][2].ToString(),
                                                                ds.Tables[0].Rows[i][3].ToString(),
                                                                ds.Tables[0].Rows[i][4].ToString(),
                                                                ds.Tables[0].Rows[i][5].ToString(),
                                                                ds.Tables[0].Rows[i][6].ToString(),
                                                                ds.Tables[0].Rows[i][7].ToString(),
                                                                ds.Tables[0].Rows[i][8].ToString(),
                                                                ds.Tables[0].Rows[i][9].ToString(),
                                                                ds.Tables[0].Rows[i][10].ToString(),
                                                                ds.Tables[0].Rows[i][11].ToString(),
                                                                ds.Tables[0].Rows[i][12].ToString());
                }             
                this.DbConnector.ExecuteNonQueryList();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_3562X601", "");
                this.FPS91_TY_S_HR_3562Z602.SetValue(this.DbConnector.ExecuteDataTable()); 
                
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
            this.FPS91_TY_S_HR_3562Z602.Initialize(); 

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_3562X601", "");
            this.FPS91_TY_S_HR_3562Z602.SetValue(this.DbConnector.ExecuteDataTable()); 

        }
        #endregion

        #region Description : 근태생성 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sSabun = "";

            //인사 임시 근태파일 삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_3578V622");
            DataTable dx = this.DbConnector.ExecuteDataTable();
            if (dx.Rows.Count > 0)
            {
                if (dx.Rows[0]["MINDATE"].ToString().Trim() != "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_3578Y623", dx.Rows[0]["MINDATE"].ToString().Trim().Substring(0, 6), dx.Rows[0]["MAXDATE"].ToString().Trim().Substring(0, 6));
                    this.DbConnector.ExecuteTranQuery();
                }
            }

            //서울 인사 기본파일 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_6C1E8929", "2", "");

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_3564S616", dt.Rows[i]["KBRFID"].ToString().Trim());

                    DataTable dk = this.DbConnector.ExecuteDataTable();

                    if (dk.Rows.Count > 0)
                    {
                        this.DbConnector.CommandClear();
                        for (int j = 0; j < dk.Rows.Count; j++)
                        {
                            //인사 임시 근태파일 등록
                            //출근
                            //if (dt.Rows[i]["KBSABUN"].ToString().Substring(0, 1) == "C")
                            //{
                            //    sSabun = dt.Rows[i]["KBSABUN"].ToString().Substring(0, 1) + dt.Rows[i]["KBSABUN"].ToString().Substring(2, 4);
                            //}
                            //else
                            //{
                            //    sSabun = dt.Rows[i]["KBSABUN"].ToString().Substring(0, 6);
                            //}

                            sSabun = dt.Rows[i]["KBSABUN"].ToString().Substring(0, 6);

                            this.DbConnector.Attach("TY_P_HR_4BPFN497", dk.Rows[j][0].ToString().Trim(),
                                                                        sSabun,
                                                                        "01",
                                                                        "01",
                                                                        dk.Rows[j][1].ToString().Replace(":",""),
                                                                        "9",
                                                                        "");
                            //퇴근
                            this.DbConnector.Attach("TY_P_HR_4BPFN497", dk.Rows[j][0].ToString().Trim(),
                                                                        sSabun,
                                                                        "01",
                                                                        "02",
                                                                        "180000",
                                                                        "9",
                                                                        "");
                            this.DbConnector.ExecuteTranQueryList(); 
                        }                        
                    }
                }
                //엑셀임사파일 삭제
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_35635603");
                this.DbConnector.ExecuteTranQuery();
            }

            this.ShowMessage("TY_M_HR_35651618");
        }
        #endregion

        #region Description : 찾아보기
        private void BTN61_SEARCH_Click(object sender, EventArgs e)
        {
            OpenFile.Filter = "Excel 97-2003통합 문서(*.xls)|*.xls|Excel 통합 문서 (.xlsx)|*.xlsx|All Files (*.*)|*.*";

            if (this.OpenFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.txtFile.Text = this.OpenFile.FileName;
        }
        #endregion       

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            if (!this.ShowMessage("TY_M_HR_3564B614"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion


    }
}
