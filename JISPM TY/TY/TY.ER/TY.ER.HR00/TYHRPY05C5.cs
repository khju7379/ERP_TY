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
    /// 국민연금 보수월액 일괄등록 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2016.07.25 10:27
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BBGV367 : 인사기본사항 조회
    ///  TY_P_HR_4CCDY800 : 개인보수월액관리 등록
    ///  TY_P_HR_5CBHG309 : 개인별보수월액 진행중인자료 조회
    ///  TY_P_HR_5CBHQ311 : 개인보수월액관리 종료처리
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_67PBE902 : 국민연금 보수월액 일괄등록 조회
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
    ///  
    ///  # 국민연금 엑셀파일 타이틀 정보 ####
    ///  순번, 성명, 주민번호, 기준소득월액,   월보험료,  사용자부담금,  본인기여금 
    ///  위의 타이틀 순서로 엑셀파일 되어 있어야 함.
    /// 
    /// </summary>
    public partial class TYHRPY05C5 : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRPY05C5()
        {
            InitializeComponent();
        }

        private void TYHRPY05C5_Load(object sender, System.EventArgs e)
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
            string sKBSABUN = string.Empty;
            string sNATIONAMT = "0";
            string sENDDATE = "";

            //하루전일자 계산
            DateTime dDate = Convert.ToDateTime(DTP01_STDATE.GetValue().ToString()).AddDays(-1);  // 마지막 일자 구하기

            sENDDATE = dDate.Year.ToString() + Set_Fill2(dDate.Month.ToString()) + Set_Fill2(dDate.Day.ToString());

            if (this.FPS91_TY_S_HR_67PBE902.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_HR_67PBE902.CurrentRowCount; i++)
                {
                    sKBSABUN = this.FPS91_TY_S_HR_67PBE902.GetValue(i, "TMSABUN").ToString();
                    sNATIONAMT = this.FPS91_TY_S_HR_67PBE902.GetValue(i, "TMKIJUNPAY").ToString();

                    //진행중인 보수월액 종료
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_67PFV905", sKBSABUN);
                    DataTable dk = this.DbConnector.ExecuteDataTable();
                    if (dk.Rows.Count > 0)
                    {
                        //키중복방지
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_HR_97N95064", sKBSABUN, DTP01_STDATE.GetString().ToString());
                        Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());

                        this.DbConnector.CommandClear();
                        for (int j = 0; j < dk.Rows.Count; j++)
                        {
                            //종료
                            this.DbConnector.Attach("TY_P_HR_5CBHQ311", sENDDATE, TYUserInfo.EmpNo, sKBSABUN, dk.Rows[j]["TAPAYCODE"].ToString(), dk.Rows[j]["TASDATE"].ToString());

                            if (iCnt > 0)
                            {
                                //수정
                                this.DbConnector.Attach("TY_P_HR_4CCDY801", sNATIONAMT, "", TYUserInfo.EmpNo, sKBSABUN, dk.Rows[j]["TAPAYCODE"].ToString(), DTP01_STDATE.GetString().ToString());
                            }
                            else
                            {
                                //등록
                                this.DbConnector.Attach("TY_P_HR_4CCDY800", sKBSABUN, dk.Rows[j]["TAPAYCODE"].ToString(), DTP01_STDATE.GetString().ToString(), sNATIONAMT, "", TYUserInfo.EmpNo);
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
            string sKBSABUN = string.Empty;

            //사번 체크
            if (this.FPS91_TY_S_HR_67PBE902.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_HR_67PBE902.CurrentRowCount; i++)
                {
                    sKBSABUN = this.FPS91_TY_S_HR_67PBE902.GetValue(i, "TMSABUN").ToString();

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_4BBGV367", "", sKBSABUN, TYUserInfo.SecureKey, "Y");
                    DataTable dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowCustomMessage("사번:" + sKBSABUN + "  존재하지않는 사원번호 입니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }

                    //개인 보수월액 진행중인 자료가 있는지 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_67PFV905", sKBSABUN);
                    DataTable dk = this.DbConnector.ExecuteDataTable();
                    if (dk.Rows.Count <= 0)
                    {
                        this.ShowCustomMessage("사번:" + sKBSABUN + "  진행중인 보수월액 자료가 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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

            if (this.txtFile.Text.Trim() != "")
            {
                this.FPS91_TY_S_HR_67PBE902.Initialize();

                string strProvider = string.Empty;
                strProvider = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + this.txtFile.Text.Trim() + "; Extended Properties='Excel 12.0; HDR=NO;' ";

                string strQuery = "SELECT * FROM [Sheet1$]"; //  , Sheet1$

                OleDbConnection ExcelCon = new OleDbConnection(strProvider);
                ExcelCon.Open();

                //OleDbDataAdapter adapter = new OleDbDataAdapter(strQuery, strProvider);

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

                ExcelCon.Close();

                this.ShowMessage("TY_M_AC_31BAP617");
            }
            else
            {
                this.ShowMessage("TY_M_AC_31B1C623");
            }
        }
        #endregion

        #region  Description : 찾아보기 버튼 이벤트
        private void BTN61_SEARCH_Click(object sender, EventArgs e)
        {
            OpenFile.Filter = "Excel 97-2003통합 문서(*.xls)|*.xls|Excel 통합 문서 (.xlsx)|*.xlsx|All Files (*.*)|*.*";

            if (this.OpenFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.txtFile.Text = this.OpenFile.FileName;
        }
        #endregion

        #region  Description : 데이타 바인딩
        private void UP_Set_DataBinding(DataSet ds)
        {
            string sTMSABUN = string.Empty;
            string sTMJUMIN = string.Empty;
            double dTMNOWPAY = 0;

            this.FPS91_TY_S_HR_67PBE902.Initialize();

            DataTable dt = UP_SetDataTable();
            DataRow row;

            if (ds.Tables.Count > 0)
            {
                for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i][2].ToString() != "")
                    {
                        sTMJUMIN = ds.Tables[0].Rows[i][2].ToString().Replace("-", "").Substring(0, 13).Trim();

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_HR_67PEQ904", TYUserInfo.SecureKey, "Y", sTMJUMIN);
                        DataTable dm = this.DbConnector.ExecuteDataTable();
                        if (dm.Rows.Count > 0)
                        {
                            sTMSABUN = dm.Rows[0]["TASABUN"].ToString();
                            dTMNOWPAY = Convert.ToDouble(dm.Rows[0]["TAAVGAMOUNT"].ToString());
                        }
                        row = dt.NewRow();
                        row["TMSABUN"] = sTMSABUN;
                        row["TMJUMIN"] = sTMJUMIN;
                        row["TMHANGL"] = ds.Tables[0].Rows[i][1].ToString();
                        row["TMKIJUNPAY"] = Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][5].ToString())); //결정기준소득월액
                        row["TMMBOSUHAP"] = Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][4].ToString())); //신고소득총액
                        row["TMCOMAMT"] = Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][6].ToString()));  //월보험료
                        row["TMOWNAMT"] = Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][7].ToString()));  //종전내용기준소득월액
                        dt.Rows.Add(row);
                    }
                }
            }

            this.FPS91_TY_S_HR_67PBE902.SetValue(dt);
        }
        #endregion

        #region  Description : DataTable 만들기
        private DataTable UP_SetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TMSABUN", typeof(System.String));
            dt.Columns.Add("TMJUMIN", typeof(System.String));
            dt.Columns.Add("TMHANGL", typeof(System.String));
            dt.Columns.Add("TMKIJUNPAY", typeof(System.Double));
            dt.Columns.Add("TMMBOSUHAP", typeof(System.Double));
            dt.Columns.Add("TMCOMAMT", typeof(System.Double));
            dt.Columns.Add("TMOWNAMT", typeof(System.Double));
            dt.TableName = "TableNames";

            return dt;
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
