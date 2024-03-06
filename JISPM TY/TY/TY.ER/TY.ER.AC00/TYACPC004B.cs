using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using System.Data.OleDb;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 재무상태표 엑셀 UPLOAD 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.11.11 13:16
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_9BBFP500 : EIS 재무상태표 등록
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_9BBDH499 : EIS 재무상태표 엑셀 UPLOAD
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  SEARCH : 찾아보기
    ///  AFFILENAME : 파일명
    /// </summary>
    public partial class TYACPC004B : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYACPC004B()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }

        private void TYACPC004B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sEMDCGUBN = string.Empty;

            if (this.FPS91_TY_S_AC_9BBDH499.CurrentRowCount > 0)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_9BBFX501", this.FPS91_TY_S_AC_9BBDH499.GetValue(1, "EMYEAR").ToString());
                this.DbConnector.ExecuteTranQuery();


                this.DbConnector.CommandClear();
                for (int i = 0; i < this.FPS91_TY_S_AC_9BBDH499.CurrentRowCount; i++)
                {
                    sEMDCGUBN = this.FPS91_TY_S_AC_9BBDH499.GetValue(i, "EMDCGUBN").ToString();

                    if (sEMDCGUBN == "D")
                    {
                        
                        this.DbConnector.Attach("TY_P_AC_9BBFP500", this.FPS91_TY_S_AC_9BBDH499.GetValue(i, "EMYEAR").ToString(),
                                                                    this.FPS91_TY_S_AC_9BBDH499.GetValue(i, "EMCDAC").ToString(),
                                                                    "0",
                                                                    Get_Numeric(this.FPS91_TY_S_AC_9BBDH499.GetValue(i, "EMMONTH01").ToString()),
                                                                    Get_Numeric(this.FPS91_TY_S_AC_9BBDH499.GetValue(i, "EMMONTH02").ToString()),
                                                                    Get_Numeric(this.FPS91_TY_S_AC_9BBDH499.GetValue(i, "EMMONTH03").ToString()),
                                                                    Get_Numeric(this.FPS91_TY_S_AC_9BBDH499.GetValue(i, "EMMONTH04").ToString()),
                                                                    Get_Numeric(this.FPS91_TY_S_AC_9BBDH499.GetValue(i, "EMMONTH05").ToString()),
                                                                    Get_Numeric(this.FPS91_TY_S_AC_9BBDH499.GetValue(i, "EMMONTH06").ToString()),
                                                                    Get_Numeric(this.FPS91_TY_S_AC_9BBDH499.GetValue(i, "EMMONTH07").ToString()),
                                                                    Get_Numeric(this.FPS91_TY_S_AC_9BBDH499.GetValue(i, "EMMONTH08").ToString()),
                                                                    Get_Numeric(this.FPS91_TY_S_AC_9BBDH499.GetValue(i, "EMMONTH09").ToString()),
                                                                    Get_Numeric(this.FPS91_TY_S_AC_9BBDH499.GetValue(i, "EMMONTH10").ToString()),
                                                                    Get_Numeric(this.FPS91_TY_S_AC_9BBDH499.GetValue(i, "EMMONTH11").ToString()),
                                                                    Get_Numeric(this.FPS91_TY_S_AC_9BBDH499.GetValue(i, "EMMONTH12").ToString()),
                                                                    "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", sEMDCGUBN
                            );
                    }
                    else
                    {                        
                        this.DbConnector.Attach("TY_P_AC_9BBFP500", this.FPS91_TY_S_AC_9BBDH499.GetValue(i, "EMYEAR").ToString(),
                                                                    this.FPS91_TY_S_AC_9BBDH499.GetValue(i, "EMCDAC").ToString(),
                                                                    "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0",
                                                                    Get_Numeric(this.FPS91_TY_S_AC_9BBDH499.GetValue(i, "EMMONTH01").ToString()),
                                                                    Get_Numeric(this.FPS91_TY_S_AC_9BBDH499.GetValue(i, "EMMONTH02").ToString()),
                                                                    Get_Numeric(this.FPS91_TY_S_AC_9BBDH499.GetValue(i, "EMMONTH03").ToString()),
                                                                    Get_Numeric(this.FPS91_TY_S_AC_9BBDH499.GetValue(i, "EMMONTH04").ToString()),
                                                                    Get_Numeric(this.FPS91_TY_S_AC_9BBDH499.GetValue(i, "EMMONTH05").ToString()),
                                                                    Get_Numeric(this.FPS91_TY_S_AC_9BBDH499.GetValue(i, "EMMONTH06").ToString()),
                                                                    Get_Numeric(this.FPS91_TY_S_AC_9BBDH499.GetValue(i, "EMMONTH07").ToString()),
                                                                    Get_Numeric(this.FPS91_TY_S_AC_9BBDH499.GetValue(i, "EMMONTH08").ToString()),
                                                                    Get_Numeric(this.FPS91_TY_S_AC_9BBDH499.GetValue(i, "EMMONTH09").ToString()),
                                                                    Get_Numeric(this.FPS91_TY_S_AC_9BBDH499.GetValue(i, "EMMONTH10").ToString()),
                                                                    Get_Numeric(this.FPS91_TY_S_AC_9BBDH499.GetValue(i, "EMMONTH11").ToString()),
                                                                    Get_Numeric(this.FPS91_TY_S_AC_9BBDH499.GetValue(i, "EMMONTH12").ToString()), sEMDCGUBN                                                                    
                            );
                    }
                }

                if (this.DbConnector.CommandCount > 0)
                {
                    this.DbConnector.ExecuteTranQueryList();
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_9BBG0502", this.FPS91_TY_S_AC_9BBDH499.GetValue(1, "EMYEAR").ToString());
                this.DbConnector.ExecuteTranQuery();
            }

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.FPS91_TY_S_AC_9BBDH499.CurrentRowCount <= 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return; 
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : 찾기 버튼 이벤트
        private void BTN61_SEARCH_Click(object sender, EventArgs e)
        {
            try
            {
                this.TXT01_AFFILENAME.SetValue("");

                OpenFileDialog.Filter = "Excel 통합 문서 (.xlsx)|*.xlsx|Excel 97-2003통합 문서(*.xls)|*.xls|All Files (*.*)|*.*";

                if (this.OpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    this.TXT01_AFFILENAME.SetValue(this.OpenFileDialog.FileName);

                string sTableName = string.Empty;

                if (this.TXT01_AFFILENAME.GetValue().ToString() != "")
                {
                    this.FPS91_TY_S_AC_9BBDH499.Initialize();

                    string strProvider = string.Empty;
                    strProvider = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + this.TXT01_AFFILENAME.GetValue().ToString() + "; Extended Properties='Excel 12.0; HDR=NO;' ";

                    string strQuery = "SELECT * FROM [Sheet1$]"; //  , Sheet1$

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

                    ExcelCon.Close();

                    this.ShowMessage("TY_M_AC_31BAP617");
                }
                else
                {
                    this.ShowMessage("TY_M_AC_31B1C623");
                }
            }
            catch (Exception ex)
            {
                this.ShowCustomMessage(ex.Message, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                
            }
        }
        #endregion

        #region  Description : 데이타 바인딩
        private void UP_Set_DataBinding(DataSet ds)
        {

            this.FPS91_TY_S_AC_9BBDH499.Initialize();

            DataTable dt = UP_SetDataTable();
            DataRow row;

            if (ds.Tables.Count > 0)
            {
                for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i][0].ToString() != "")
                    {                        
                        row = dt.NewRow();
                        row["EMYEAR"] = ds.Tables[0].Rows[i][0].ToString();
                        row["EMCDAC"] = ds.Tables[0].Rows[i][1].ToString();
                        row["EMCDACNM"] = ds.Tables[0].Rows[i][2].ToString();
                        row["EMLEVEL"] = Convert.ToInt16(ds.Tables[0].Rows[i][3].ToString());

                        row["EMMONTH01"] = Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][4].ToString()));
                        row["EMMONTH02"] = Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][5].ToString()));
                        row["EMMONTH03"] = Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][6].ToString()));
                        row["EMMONTH04"] = Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][7].ToString()));
                        row["EMMONTH05"] = Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][8].ToString()));
                        row["EMMONTH06"] = Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][9].ToString()));
                        row["EMMONTH07"] = Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][10].ToString()));
                        row["EMMONTH08"] = Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][11].ToString()));
                        row["EMMONTH09"] = Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][12].ToString()));
                        row["EMMONTH10"] = Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][13].ToString()));
                        row["EMMONTH11"] = Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][14].ToString()));
                        row["EMMONTH12"] = Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][15].ToString()));

                        row["EMDCGUBN"] = ds.Tables[0].Rows[i][16].ToString();
                        
                        dt.Rows.Add(row);
                    }
                }
            }

            this.FPS91_TY_S_AC_9BBDH499.SetValue(dt);
        }
        #endregion

        #region  Description : DataTable 만들기
        private DataTable UP_SetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EMYEAR", typeof(System.String));
            dt.Columns.Add("EMCDAC", typeof(System.String));
            dt.Columns.Add("EMCDACNM", typeof(System.String));
            dt.Columns.Add("EMLEVEL", typeof(System.Int16));            
            dt.Columns.Add("EMMONTH01", typeof(System.Double));
            dt.Columns.Add("EMMONTH02", typeof(System.Double));
            dt.Columns.Add("EMMONTH03", typeof(System.Double));
            dt.Columns.Add("EMMONTH04", typeof(System.Double));
            dt.Columns.Add("EMMONTH05", typeof(System.Double));
            dt.Columns.Add("EMMONTH06", typeof(System.Double));
            dt.Columns.Add("EMMONTH07", typeof(System.Double));
            dt.Columns.Add("EMMONTH08", typeof(System.Double));            
            dt.Columns.Add("EMMONTH09", typeof(System.Double));
            dt.Columns.Add("EMMONTH10", typeof(System.Double));
            dt.Columns.Add("EMMONTH11", typeof(System.Double));
            dt.Columns.Add("EMMONTH12", typeof(System.Double));
            dt.Columns.Add("EMDCGUBN", typeof(System.String));

            dt.TableName = "TableNames";

            return dt;
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

    }
}
