using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.Data.OleDb;

namespace TY.ER.HR00
{
    /// <summary>
    /// 보수월액 일괄등록 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.12.11 16:24
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_5CBGG307 : 보수월액 일괄등록 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_25F59464 : 선택한 자료가 없습니다.
    ///  TY_M_AC_31B1C623 : EXCEL 업데이트 할 파일을 선택하세요.
    ///  TY_M_AC_31BAP617 : EXCEL 업데이트가 완료 되었습니다.
    ///  TY_M_HR_4BPFV500 : 파일 경로를 선택해주세요!
    ///  TY_M_MR_2BF4Z352 : 처리 할 데이터가 없습니다.
    ///  TY_M_MR_2BF50353 : 처리하시겠습니까?
    ///  TY_M_MR_2BF50354 : 처리하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  EXCEL : 엑셀 업데이트
    ///  SEARCH : 찾아보기
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYHRPY005P : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRPY005P()
        {
            InitializeComponent();
        }

        private void TYHRPY005P_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_STDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            DataSet ds = new DataSet();
            UP_Set_DataBinding(ds);

        }
        #endregion

        #region  Description : 처리 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sKBJUMIN = string.Empty;
            string sEMPLOYAMT = "0";
            string sHEALTHAMT = "0";
            string sENDDATE = "";

            //하루전일자 계산
            DateTime dDate = Convert.ToDateTime(DTP01_STDATE.GetValue().ToString()).AddDays(-1);  // 마지막 일자 구하기

            sENDDATE = dDate.Year.ToString() + Set_Fill2(dDate.Month.ToString()) + Set_Fill2(dDate.Day.ToString());
            
            if (this.FPS91_TY_S_HR_5CBGG307.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_HR_5CBGG307.CurrentRowCount; i++)
                {
                    sKBJUMIN   = this.FPS91_TY_S_HR_5CBGG307.GetValue(i, "TMJUMIN").ToString();
                    sEMPLOYAMT = this.FPS91_TY_S_HR_5CBGG307.GetValue(i, "TMEMPLOYAMT").ToString();
                    sHEALTHAMT = this.FPS91_TY_S_HR_5CBGG307.GetValue(i, "TMHEALTHAMT").ToString();
                    
                    //진행중인 보수월액 종료
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_5CBHG309", sKBJUMIN.Replace("-","").Trim(), TYUserInfo.SecureKey, "Y");
                    DataTable dk = this.DbConnector.ExecuteDataTable();
                    if (dk.Rows.Count > 0)
                    {
                        this.DbConnector.CommandClear();
                        for (int j = 0; j < dk.Rows.Count; j++)
                        {
                            //종료
                            if (Convert.ToDouble(Get_Numeric(sHEALTHAMT)) > 0 && dk.Rows[j]["TAPAYCODE"].ToString() == "2101" )
                            {
                                this.DbConnector.Attach("TY_P_HR_5CBHQ311", sENDDATE, TYUserInfo.EmpNo, dk.Rows[j]["TASABUN"].ToString(), dk.Rows[j]["TAPAYCODE"].ToString(), dk.Rows[j]["TASDATE"].ToString());
                            }
                            if (Convert.ToDouble(Get_Numeric(sEMPLOYAMT)) > 0 && dk.Rows[j]["TAPAYCODE"].ToString() == "2401")
                            {
                                this.DbConnector.Attach("TY_P_HR_5CBHQ311", sENDDATE, TYUserInfo.EmpNo, dk.Rows[j]["TASABUN"].ToString(), dk.Rows[j]["TAPAYCODE"].ToString(), dk.Rows[j]["TASDATE"].ToString());
                            }

                            //등록
                            if (j == 0)
                            {
                                if (Convert.ToDouble(Get_Numeric(sHEALTHAMT)) > 0 )
                                {
                                    this.DbConnector.Attach("TY_P_HR_4CCDY800", dk.Rows[j]["TASABUN"].ToString(), dk.Rows[j]["TAPAYCODE"].ToString(), DTP01_STDATE.GetString().ToString(), sHEALTHAMT, "", TYUserInfo.EmpNo);
                                }
                            }
                            else
                            {
                                if (Convert.ToDouble(Get_Numeric(sEMPLOYAMT)) > 0)
                                {
                                    this.DbConnector.Attach("TY_P_HR_4CCDY800", dk.Rows[j]["TASABUN"].ToString(), dk.Rows[j]["TAPAYCODE"].ToString(), DTP01_STDATE.GetString().ToString(), sEMPLOYAMT, "", TYUserInfo.EmpNo);
                                }
                            }
                        }
                        this.DbConnector.ExecuteTranQueryList();
                    }
                }
            }

            this.ShowMessage("TY_M_MR_2BF50354");

        }

        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {   
            string sKBJUMIN = string.Empty;

            //사번 체크
            if (this.FPS91_TY_S_HR_5CBGG307.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_HR_5CBGG307.CurrentRowCount; i++)
                {
                    sKBJUMIN = this.FPS91_TY_S_HR_5CBGG307.GetValue(i, "TMJUMIN").ToString();

                    //this.DbConnector.CommandClear();
                    //this.DbConnector.Attach("TY_P_HR_4BBGV367", "",sKBSABUN);
                    //DataTable dt = this.DbConnector.ExecuteDataTable();
                    //if (dt.Rows.Count <= 0)
                    //{
                    //    this.ShowCustomMessage("사번:" + dt.Rows[0]["TASABUN"].ToString() + "  존재하지않는 사원번호 입니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    //    e.Successed = false;
                    //    return;
                    //}

                    //개인 보수월액 진행중인 자료가 있는지 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_5CBHG309", sKBJUMIN.Replace("-","").Trim(),TYUserInfo.SecureKey, "Y");
                    DataTable dk = this.DbConnector.ExecuteDataTable();
                    if (dk.Rows.Count <= 0)
                    {
                        this.ShowCustomMessage("주민번호:" + sKBJUMIN + "  진행중인 보수월액 자료가 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (!this.ShowMessage("TY_M_MR_2BF50353"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : 엑셀업데이트 버튼 이벤트
        private void BTN61_EXCEL_Click(object sender, EventArgs e)
        {
            string sTableName = string.Empty;
            string strQuery = string.Empty;

            if (this.txtFile.Text.Trim() != "")
            {
                this.FPS91_TY_S_HR_5CBGG307.Initialize();

                string strProvider = string.Empty;
                strProvider = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + this.txtFile.Text.Trim() + "; Extended Properties='Excel 12.0; HDR=NO;'";
                OleDbConnection ExcelCon = new OleDbConnection(strProvider);
                ExcelCon.Open();

                DataTable dtSheetList = ExcelCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                if (dtSheetList.Rows.Count > 0)
                {
                    sTableName = dtSheetList.Rows[0]["TABLE_NAME"].ToString();

                    strQuery = "SELECT * FROM [" + sTableName + "] ";

                    OleDbDataAdapter adapter = new OleDbDataAdapter(strQuery, strProvider);

                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "EXCEL");

                    UP_Set_DataBinding(ds);
                }

                //string strQuery = "SELECT * FROM [Sheet1$] "; //  , Sheet1$

                //OleDbConnection ExcelCon = new OleDbConnection(strProvider);
                //ExcelCon.Open();

                //OleDbDataAdapter adapter = new OleDbDataAdapter(strQuery, strProvider);

                //DataSet ds = new DataSet();
                //adapter.Fill(ds, "EXCEL");

                //UP_Set_DataBinding(ds);              

                this.ShowMessage("TY_M_AC_31BAP617");
            }
            else
            {
                this.ShowMessage("TY_M_AC_31B1C623");
            }
        }
        #endregion

        #region  Description : 데이타 바인딩
        private void UP_Set_DataBinding(DataSet ds)
        {
            this.FPS91_TY_S_HR_5CBGG307.Initialize();

            DataTable dt = UP_SetDataTable();
            DataRow row;

            if (ds.Tables.Count > 0)
            {
                for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
                {
                    row = dt.NewRow();
                    row["TMJUMIN"] = ds.Tables[0].Rows[i][0].ToString();
                    row["TMNAME"] = ds.Tables[0].Rows[i][1].ToString();
                    row["TMMEDNUM"] = ds.Tables[0].Rows[i][2].ToString();
                    row["TMEMPLOYAMT"] = Convert.ToDouble(ds.Tables[0].Rows[i][3].ToString());
                    row["TMHEALTHAMT"] = Convert.ToDouble(ds.Tables[0].Rows[i][4].ToString());
                    dt.Rows.Add(row);
                }
            }

            this.FPS91_TY_S_HR_5CBGG307.SetValue(dt);
        }
        #endregion

        #region  Description : DataTable 만들기
        private DataTable UP_SetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TMJUMIN", typeof(System.String));
            dt.Columns.Add("TMNAME", typeof(System.String));
            dt.Columns.Add("TMMEDNUM", typeof(System.String));
            dt.Columns.Add("TMEMPLOYAMT", typeof(System.Double));
            dt.Columns.Add("TMHEALTHAMT", typeof(System.Double));
            dt.TableName = "TableNames";

            return dt;
        }
        #endregion

        #region  Description : 찾아보기 버튼 이벤트
        private void BTN61_SEARCH_Click(object sender, EventArgs e)
        {
            OpenFile.Filter = "Excel 통합 문서 (.xlsx)|*.xlsx|All Files (*.*)|*.*";

            if (this.OpenFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.txtFile.Text = this.OpenFile.FileName;
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
