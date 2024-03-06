using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.Data.OleDb;

namespace TY.ER.BS00
{
    /// <summary>
    /// 영업계획 자료 UPLOAD 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.08.02 10:32
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_782BK327 : 사업계획-영업계획(매출액,취급량) 등록
    ///  TY_P_AC_782BK328 : 사업계획-영업계획(매출액,취급량) 삭제
    ///  TY_P_AC_782BM329 : 사업계획-영업계획(공통,자체,영업외손익, 영업외비용) 등록
    ///  TY_P_AC_782BN330 : 사업계획-영업계획(공통,자체,영업외손익, 영업외비용) 삭제
    ///  TY_P_AC_782BP331 : 사업계획-영업계획(투자,수선) 등록
    ///  TY_P_AC_782BQ332 : 사업계획-영업계획(투자,수선) 삭제
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  EXCEL : 엑셀 업데이트
    ///  SAV : 저장
    ///  SEARCH : 찾아보기
    /// </summary>
    public partial class TYBSUP001I : TYBase
    {

        private string fsSSID;

        private bool fbExcelCheck;

        #region  Description : 폼 로드 이벤트
        public TYBSUP001I()
        {
            InitializeComponent();
        }

        private void TYBSUP001I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_EXCEL.ProcessCheck += new TButton.CheckHandler(BTN61_EXCEL_ProcessCheck);

            UP_SpreadClear();

            fsSSID = this.IPAdresss + TYUserInfo.EmpNo;

            fbExcelCheck = false;
        }
        #endregion

        #region  Description : 엑셀UPDATE 버튼 이벤트
        private void BTN61_EXCEL_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;

            UP_SpreadClear();

            //삭제
            UP_TempDbClear();


            if (ListBox_FileName.Items.Count > 0)
            {
                for (int i = 0; i < ListBox_FileName.Items.Count; i++)
                {
                    filePath = ListBox_FileName.Items[i].ToString();

                    UP_Get_ExcelToFile(filePath, false);
                }

                this.UP_Grid_DataBinding();

                int iTabIndex = 0;

                //자료가 있는 tab 열어주기
                if (FPS91_TY_S_AC_782HJ339.CurrentRowCount > 0)
                {
                    iTabIndex = 0;
                }

                if (FPS91_TY_S_AC_782HT340.CurrentRowCount > 0)
                {
                    iTabIndex = 1;
                }

                if (FPS91_TY_S_AC_787FW366.CurrentRowCount > 0)
                {
                    iTabIndex = 2;
                }

                if (FPS91_TY_S_AC_787FZ369.CurrentRowCount > 0)
                {
                    iTabIndex = 3;
                }

                if (FPS91_TY_S_AC_783DO342.CurrentRowCount > 0)
                {
                    iTabIndex = 4;
                }

                if (FPS91_TY_S_AC_783E1344.CurrentRowCount > 0)
                {
                    iTabIndex  = 5;
                }

                if (FPS91_TY_S_AC_783E1345.CurrentRowCount > 0)
                {
                    iTabIndex = 6;
                }                

                if (FPS91_TY_S_AC_783E1346.CurrentRowCount > 0)
                {
                    iTabIndex  = 7;
                }

                tabContlExcel.SelectedIndex = iTabIndex;

                this.ShowCustomMessage("엑셀 불러오기가 완료되었습니다!", "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                return;
            }
            else
            {
                this.ShowCustomMessage("선택한 파일이 존재하지 않습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                return;
            }
        }

        private void BTN61_EXCEL_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string filePath = string.Empty;

            if (ListBox_FileName.Items.Count > 0)
            {
                fbExcelCheck = false;

                for (int i = 0; i < ListBox_FileName.Items.Count; i++)
                {
                    filePath = ListBox_FileName.Items[i].ToString();

                    UP_Get_ExcelToFile(filePath, true);

                    if (fbExcelCheck)
                    {
                        this.ShowCustomMessage("사업계획 엑셀파일중에 예산금액은 있지만 부서코드가 없는 자료가 있습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                }               
            }
            else
            {
                this.ShowCustomMessage("선택한 파일이 존재하지 않습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                return;
            }

            if (!this.ShowMessage("TY_M_UT_6APKH541"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : UP_Grid_DataBinding
        private void UP_Grid_DataBinding()
        {
            //매출액
            FPS91_TY_S_AC_782HJ339.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_783FZ349", fsSSID, "MA");
            FPS91_TY_S_AC_782HJ339.SetValue(this.DbConnector.ExecuteDataTable());

            if (FPS91_TY_S_AC_782HJ339.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_AC_782HJ339, "BSHMCDNM", "합   계", SumRowType.Sum, "BSMONAMT01", "BSMONAMT02", "BSMONAMT03", "BSMONAMT04", "BSMONAMT05", "BSMONAMT06", "BSMONAMT07", "BSMONAMT08", "BSMONAMT09", "BSMONAMT10", "BSMONAMT11", "BSMONAMT12", "BSMONTOTAL");
            }

            //취급량 
            FPS91_TY_S_AC_782HT340.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_783FZ349", fsSSID, "SU");
            FPS91_TY_S_AC_782HT340.SetValue(this.DbConnector.ExecuteDataTable());

            if (FPS91_TY_S_AC_782HT340.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_AC_782HT340, "BSHMCDNM", "합   계", SumRowType.Sum, "BSMONAMT01", "BSMONAMT02", "BSMONAMT03", "BSMONAMT04", "BSMONAMT05", "BSMONAMT06", "BSMONAMT07", "BSMONAMT08", "BSMONAMT09", "BSMONAMT10", "BSMONAMT11", "BSMONAMT12", "BSMONTOTAL");
            }

            //영업계획(투자)
            FPS91_TY_S_AC_787FW366.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_787FV364", fsSSID, "IN");
            FPS91_TY_S_AC_787FW366.SetValue(this.DbConnector.ExecuteDataTable());

            if (FPS91_TY_S_AC_787FW366.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_AC_787FW366, "BVITEMNAME", "합   계", SumRowType.Sum, "BVMONAMT01", "BVMONAMT02", "BVMONAMT03", "BVMONAMT04", "BVMONAMT05", "BVMONAMT06", "BVMONAMT07", "BVMONAMT08", "BVMONAMT09", "BVMONAMT10", "BVMONAMT11", "BVMONAMT12", "BVMONTOTAL");
            }

            //영업계획(수선)
            FPS91_TY_S_AC_787FZ369.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_787FV364", fsSSID, "RE");
            FPS91_TY_S_AC_787FZ369.SetValue(this.DbConnector.ExecuteDataTable());

            if (FPS91_TY_S_AC_787FZ369.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_AC_787FZ369, "BVITEMNAME", "합   계", SumRowType.Sum, "BVMONAMT01", "BVMONAMT02", "BVMONAMT03", "BVMONAMT04", "BVMONAMT05", "BVMONAMT06", "BVMONAMT07", "BVMONAMT08", "BVMONAMT09", "BVMONAMT10", "BVMONAMT11", "BVMONAMT12", "BVMONTOTAL");
            }

            //영업비용-공통
            FPS91_TY_S_AC_783DO342.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_783G1350", fsSSID, "CM");
            FPS91_TY_S_AC_783DO342.SetValue(this.DbConnector.ExecuteDataTable());

            if (FPS91_TY_S_AC_783DO342.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_AC_783DO342, "BCITEMNAME", "합   계", SumRowType.Sum, "BCMONAMT01", "BCMONAMT02", "BCMONAMT03", "BCMONAMT04", "BCMONAMT05", "BCMONAMT06", "BCMONAMT07", "BCMONAMT08", "BCMONAMT09", "BCMONAMT10", "BCMONAMT11", "BCMONAMT12", "BCMONTOTAL");
            }

            //영업비용-자체
            FPS91_TY_S_AC_783E1344.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_783G1350", fsSSID, "SE");
            FPS91_TY_S_AC_783E1344.SetValue(this.DbConnector.ExecuteDataTable());

            if (FPS91_TY_S_AC_783E1344.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_AC_783E1344, "BCITEMNAME", "합   계", SumRowType.Sum, "BCMONAMT01", "BCMONAMT02", "BCMONAMT03", "BCMONAMT04", "BCMONAMT05", "BCMONAMT06", "BCMONAMT07", "BCMONAMT08", "BCMONAMT09", "BCMONAMT10", "BCMONAMT11", "BCMONAMT12", "BCMONTOTAL");
            }

            //영업비용-영업외수익
            FPS91_TY_S_AC_783E1345.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_783G1350", fsSSID, "PR");
            FPS91_TY_S_AC_783E1345.SetValue(this.DbConnector.ExecuteDataTable());

            if (FPS91_TY_S_AC_783E1345.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_AC_783E1345, "BCITEMNAME", "합   계", SumRowType.Sum, "BCMONAMT01", "BCMONAMT02", "BCMONAMT03", "BCMONAMT04", "BCMONAMT05", "BCMONAMT06", "BCMONAMT07", "BCMONAMT08", "BCMONAMT09", "BCMONAMT10", "BCMONAMT11", "BCMONAMT12", "BCMONTOTAL");
            }

            //영업비용-영업외비용
            FPS91_TY_S_AC_783E1346.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_783G1350", fsSSID, "CO");
            FPS91_TY_S_AC_783E1346.SetValue(this.DbConnector.ExecuteDataTable());

            if (FPS91_TY_S_AC_783E1346.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_AC_783E1346, "BCITEMNAME", "합   계", SumRowType.Sum, "BCMONAMT01", "BCMONAMT02", "BCMONAMT03", "BCMONAMT04", "BCMONAMT05", "BCMONAMT06", "BCMONAMT07", "BCMONAMT08", "BCMONAMT09", "BCMONAMT10", "BCMONAMT11", "BCMONAMT12", "BCMONTOTAL");
            }

            
        }
        #endregion

        #region  Description : 엑셀 파일 읽기 함수
        private void UP_Get_ExcelToFile(string sFilePath, bool bCheck)
        {
            string strProvider = string.Empty;
            string sTableName = string.Empty;
            string strQuery = string.Empty;
            

            strProvider = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + sFilePath + "; Extended Properties='Excel 12.0; HDR=NO;'";

            OleDbConnection ExcelCon = new OleDbConnection(strProvider);
            ExcelCon.Open();

            DataTable dtSheetList = ExcelCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

            if (dtSheetList.Rows.Count > 0)
            {
                int iStartPoint = 0;
                int iChar = 0;

                for (int i = 0; i < dtSheetList.Rows.Count; i++)
                {
                    sTableName = dtSheetList.Rows[i]["TABLE_NAME"].ToString();
                    iChar = sTableName.IndexOf("$");
                    if (iChar > 0)
                    {
                        iChar = sTableName.ToUpper().IndexOf("CODE");
                        if (iChar < 0)
                        {
                            iStartPoint = i;
                            break;
                        }                      

                    }
                }

                for (int i = iStartPoint; i < dtSheetList.Rows.Count; i++)
                {
                    sTableName = dtSheetList.Rows[i]["TABLE_NAME"].ToString();
                    
                    iChar = sTableName.IndexOf("$");
                    if (iChar > 0)
                    {
                        strQuery = "SELECT * FROM [" + sTableName + "] ";
                        OleDbDataAdapter adapter = new OleDbDataAdapter(strQuery, strProvider);

                        DataSet ds = new DataSet();
                        adapter.Fill(ds, "EXCEL");

                        if (bCheck)
                        {
                            //엑셀 내용 체크
                            UP_Check_FileParsing(ds);
                        }
                        else
                        {
                            // 임시 db upload
                            UP_Set_FileParsing(ds);
                        }
                    }
                }
            }
            else
            {
                this.ShowCustomMessage("엑셀 파일을 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
            ExcelCon.Close();
        }
        #endregion

        #region  Description : 엑셀 파일 읽기 함수
        private void UP_Set_FileParsing(DataSet ds)
        {
            string sForm = string.Empty;
            double dTotal = 0;
            
            
            this.DbConnector.CommandClear();

            if (ds.Tables[0].Rows.Count > 0)
            {
                sForm = ds.Tables[0].Rows[0][1].ToString();

                for (int i = 2; i < ds.Tables[0].Rows.Count; i++)
                {
                    //매출액, 취급량
                    if (sForm == "MA" || sForm == "SU")
                    {
                        if (ds.Tables[0].Rows[i][0].ToString() != "")
                        {
                            dTotal = Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][7].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][8].ToString())) +
                                            Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][9].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][10].ToString())) +
                                            Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][11].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][12].ToString())) +
                                            Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][13].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][14].ToString())) +
                                            Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][15].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][16].ToString())) +
                                            Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][17].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][18].ToString()));

                            if (ds.Tables[0].Rows[i][1].ToString().Trim() != "")
                            {
                                this.DbConnector.Attach("TY_P_AC_782BK327", fsSSID, sForm,
                                                                            ds.Tables[0].Rows[i][0].ToString().Trim(),
                                                                            ds.Tables[0].Rows[i][1].ToString().Trim().ToUpper(),
                                                                            ds.Tables[0].Rows[i][3].ToString().Trim(),
                                                                            ds.Tables[0].Rows[i][5].ToString().Trim(),
                                                                            Get_Numeric(ds.Tables[0].Rows[i][7].ToString()),
                                                                            Get_Numeric(ds.Tables[0].Rows[i][8].ToString()),
                                                                            Get_Numeric(ds.Tables[0].Rows[i][9].ToString()),
                                                                            Get_Numeric(ds.Tables[0].Rows[i][10].ToString()),
                                                                            Get_Numeric(ds.Tables[0].Rows[i][11].ToString()),
                                                                            Get_Numeric(ds.Tables[0].Rows[i][12].ToString()),
                                                                            Get_Numeric(ds.Tables[0].Rows[i][13].ToString()),
                                                                            Get_Numeric(ds.Tables[0].Rows[i][14].ToString()),
                                                                            Get_Numeric(ds.Tables[0].Rows[i][15].ToString()),
                                                                            Get_Numeric(ds.Tables[0].Rows[i][16].ToString()),
                                                                            Get_Numeric(ds.Tables[0].Rows[i][17].ToString()),
                                                                            Get_Numeric(ds.Tables[0].Rows[i][18].ToString()),
                                                                            dTotal.ToString(),
                                                                            TYUserInfo.EmpNo
                                                                     );
                            }
                        }       
                    }

                    //영업비용(공통,자체,영업외손익,영업외비용)
                    if (sForm == "CM" || sForm == "SE" || sForm == "PR" || sForm == "CO")
                    {
                        dTotal = Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][12].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][13].ToString())) +
                                   Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][14].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][15].ToString())) +
                                   Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][16].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][17].ToString())) +
                                   Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][18].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][19].ToString())) +
                                   Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][20].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][21].ToString())) +
                                   Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][22].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][23].ToString()));

                        //귀속부서 필수
                        if (ds.Tables[0].Rows[i][9].ToString() != "")
                        {
                            if (ds.Tables[0].Rows[i][1].ToString().Trim() != "" && dTotal > 0 )
                            {
                                this.DbConnector.Attach("TY_P_AC_782BM329", fsSSID, sForm,
                                                                            ds.Tables[0].Rows[i][0].ToString().Trim(),
                                                                            ds.Tables[0].Rows[i][1].ToString().Trim().Substring(0,6).ToUpper(),
                                                                            ds.Tables[0].Rows[i][9].ToString().Trim().Substring(0,6).ToUpper(),
                                                                            ds.Tables[0].Rows[i][3].ToString().Trim(),
                                                                            ds.Tables[0].Rows[i][5].ToString().Trim(),
                                                                            i.ToString(),
                                                                            ds.Tables[0].Rows[i][7].ToString().Trim(),
                                                                            ds.Tables[0].Rows[i][8].ToString().Trim(),
                                                                            Get_Numeric(ds.Tables[0].Rows[i][12].ToString()),
                                                                            Get_Numeric(ds.Tables[0].Rows[i][13].ToString()),
                                                                            Get_Numeric(ds.Tables[0].Rows[i][14].ToString()),
                                                                            Get_Numeric(ds.Tables[0].Rows[i][15].ToString()),
                                                                            Get_Numeric(ds.Tables[0].Rows[i][16].ToString()),
                                                                            Get_Numeric(ds.Tables[0].Rows[i][17].ToString()),
                                                                            Get_Numeric(ds.Tables[0].Rows[i][18].ToString()),
                                                                            Get_Numeric(ds.Tables[0].Rows[i][19].ToString()),
                                                                            Get_Numeric(ds.Tables[0].Rows[i][20].ToString()),
                                                                            Get_Numeric(ds.Tables[0].Rows[i][21].ToString()),
                                                                            Get_Numeric(ds.Tables[0].Rows[i][22].ToString()),
                                                                            Get_Numeric(ds.Tables[0].Rows[i][23].ToString()),
                                                                            dTotal.ToString(),
                                                                            ds.Tables[0].Rows[i][11].ToString(),
                                                                            TYUserInfo.EmpNo
                                                                        );
                            }
                        }

                       
                    }

                    //영업계획(투자,수선)
                    if (sForm == "IN" || sForm == "RE")
                    {

                        dTotal = Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][10].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][11].ToString())) +
                                                                 Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][12].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][13].ToString())) +
                                                                 Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][14].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][15].ToString())) +
                                                                 Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][16].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][17].ToString())) +
                                                                 Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][18].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][19].ToString())) +
                                                                 Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][20].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][21].ToString()));
                        if (ds.Tables[0].Rows[i][1].ToString().Trim() != "")
                        {
                            this.DbConnector.Attach("TY_P_AC_782BP331", fsSSID, sForm,
                                                                        ds.Tables[0].Rows[i][0].ToString().Trim(),
                                                                        ds.Tables[0].Rows[i][1].ToString().Trim().Substring(0, 6).ToUpper(),
                                                                        ds.Tables[0].Rows[i][8].ToString().Trim().Substring(0, 6).ToUpper(),
                                                                        ds.Tables[0].Rows[i][3].ToString().Trim(),
                                                                        ds.Tables[0].Rows[i][5].ToString().Trim(),
                                                                        i.ToString(),
                                                                        "",
                                                                        ds.Tables[0].Rows[i][7].ToString().Trim(),
                                                                        Get_Numeric(ds.Tables[0].Rows[i][10].ToString()),
                                                                        Get_Numeric(ds.Tables[0].Rows[i][11].ToString()),
                                                                        Get_Numeric(ds.Tables[0].Rows[i][12].ToString()),
                                                                        Get_Numeric(ds.Tables[0].Rows[i][13].ToString()),
                                                                        Get_Numeric(ds.Tables[0].Rows[i][14].ToString()),
                                                                        Get_Numeric(ds.Tables[0].Rows[i][15].ToString()),
                                                                        Get_Numeric(ds.Tables[0].Rows[i][16].ToString()),
                                                                        Get_Numeric(ds.Tables[0].Rows[i][17].ToString()),
                                                                        Get_Numeric(ds.Tables[0].Rows[i][18].ToString()),
                                                                        Get_Numeric(ds.Tables[0].Rows[i][19].ToString()),
                                                                        Get_Numeric(ds.Tables[0].Rows[i][20].ToString()),
                                                                        Get_Numeric(ds.Tables[0].Rows[i][21].ToString()),
                                                                        dTotal.ToString(),
                                                                        sForm == "IN" ? ds.Tables[0].Rows[i][23].ToString().Trim() : "",
                                                                        TYUserInfo.EmpNo
                                                                    );
                        }

                    }
                }
            }

            if (this.DbConnector.CommandCount > 0)
            {
                this.DbConnector.ExecuteTranQueryList();
            }
        }
        #endregion             

        #region  Description : 엑셀 파일 내용 체크 함수
        private void UP_Check_FileParsing(DataSet ds)
        {
            string sForm = string.Empty;
            double dTotal = 0;

            if (ds.Tables[0].Rows.Count > 0)
            {
                sForm = ds.Tables[0].Rows[0][1].ToString();

                for (int i = 2; i < ds.Tables[0].Rows.Count; i++)
                {
                    //매출액, 취급량
                    if (sForm == "MA" || sForm == "SU")
                    {
                        if (ds.Tables[0].Rows[i][0].ToString() != "")
                        {
                            dTotal = Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][7].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][8].ToString())) +
                                            Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][9].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][10].ToString())) +
                                            Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][11].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][12].ToString())) +
                                            Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][13].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][14].ToString())) +
                                            Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][15].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][16].ToString())) +
                                            Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][17].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][18].ToString()));

                            if (ds.Tables[0].Rows[i][1].ToString().Trim() == "")
                            {                                
                                fbExcelCheck = true;
                                return;
                            }
                        }
                    }

                    //영업비용(공통,자체,영업외손익,영업외비용)
                    if (sForm == "CM" || sForm == "SE" || sForm == "PR" || sForm == "CO")
                    {
                        dTotal = Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][12].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][13].ToString())) +
                                   Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][14].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][15].ToString())) +
                                   Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][16].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][17].ToString())) +
                                   Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][18].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][19].ToString())) +
                                   Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][20].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][21].ToString())) +
                                   Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][22].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][23].ToString()));

                        //귀속부서 필수                        
                        if ((ds.Tables[0].Rows[i][1].ToString().Trim() == "" && dTotal > 0) || (ds.Tables[0].Rows[i][9].ToString().Trim() == "" && dTotal > 0) )
                        {
                            fbExcelCheck = true;
                            return;  
                        }
                    }

                    //영업계획(투자,수선)
                    if (sForm == "IN" || sForm == "RE")
                    {

                        dTotal = Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][10].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][11].ToString())) +
                                                                 Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][12].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][13].ToString())) +
                                                                 Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][14].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][15].ToString())) +
                                                                 Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][16].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][17].ToString())) +
                                                                 Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][18].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][19].ToString())) +
                                                                 Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][20].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][21].ToString()));
                        if (ds.Tables[0].Rows[i][1].ToString().Trim() == "" && dTotal > 0 )
                        {
                            fbExcelCheck = true;
                            return;
                        }

                    }
                }
            }            
        }
        #endregion             
        
        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_784DX352", fsSSID, TYUserInfo.EmpNo, "");
            string sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
            if (sOUTMSG.Substring(0, 2) != "OK")
            {
                this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            UP_TempDbClear();

            UP_SpreadClear();

            this.ShowMessage("TY_M_GB_23NAD873");
        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sName = string.Empty;
            string sLine = string.Empty;

            int iCnt = 0;

            if (this.FPS91_TY_S_AC_782HJ339.CurrentRowCount <= 0 &&
                this.FPS91_TY_S_AC_782HT340.CurrentRowCount <= 0 &&
                this.FPS91_TY_S_AC_783DO342.CurrentRowCount <= 0 &&
                this.FPS91_TY_S_AC_783E1344.CurrentRowCount <= 0 &&
                this.FPS91_TY_S_AC_783E1345.CurrentRowCount <= 0 &&
                this.FPS91_TY_S_AC_783E1346.CurrentRowCount <= 0 &&
                this.FPS91_TY_S_AC_787FW366.CurrentRowCount <= 0 &&
                this.FPS91_TY_S_AC_787FZ369.CurrentRowCount <= 0)
            {
                this.ShowCustomMessage("저장할 자료가 존재하지 않습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }
            //회계 마감 체크

            //KEY 중복라인 있는지 체크
            //(영업비용-공통,자체,영업외손익, 영업외비용)
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_784GP356", fsSSID);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                

                switch (dt.Rows[0]["BCFORM"].ToString())
                {
                    case "CM":
                        sName = "영업비용:공통";
                        break;
                    case "SE":
                        sName = "영업비용:자체";
                        break;
                    case "PR":
                        sName = "영업외손익";
                        break;
                    case "CO":
                        sName = "영업외비용";
                        break;
                }

                string sMessage = "구분:" + sName + "" + "\r\n" +
                                 "담당부서:" + dt.Rows[0]["BCFORM"].ToString() + "\r\n" +
                                 "귀속부서:" + dt.Rows[0]["BCDPAC"].ToString() + "\r\n" +
                                 "계정세목:" + dt.Rows[0]["BCCDAC"].ToString() + "\r\n" +
                                 "항목코드:" + dt.Rows[0]["BCITEM"].ToString() + "\r\n" +
                                 "위의 내용이 중복되었습니다! 확인후 다시 Upload ";

                this.ShowCustomMessage(sMessage, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;

            }

            // 담당부서가 2개이상 존재하면 안된다.
            // (영업비용-공통,자체,영업외손익, 영업외비용)
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_7AIFF839", fsSSID);
            DataTable dt2 = this.DbConnector.ExecuteDataTable();
            if (dt2.Rows.Count > 0)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    if (Convert.ToInt16(dt2.Rows[i]["CNT"].ToString()) > 1)
                    {
                        switch (dt2.Rows[i]["BCFORM"].ToString())
                        {
                            case "CM":
                                sName = "영업비용:공통";
                                break;
                            case "SE":
                                sName = "영업비용:자체";
                                break;
                            case "PR":
                                sName = "영업외손익";
                                break;
                            case "CO":
                                sName = "영업외비용";
                                break;
                        }

                        this.ShowCustomMessage(sName + " 자료에 담당부서가 2개이상 존재합니다. 자료 다시 확인하세요! ", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }

            //매출액 자료 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_783FZ349", fsSSID, "MA");
            DataTable dma = this.DbConnector.ExecuteDataTable();
            if (dma.Rows.Count > 0)
            {
                for (int i = 0; i < dma.Rows.Count; i++)
                {
                    if (dma.Rows[i]["BSDPAC"].ToString() != "T" && dma.Rows[i]["BSDPAC"].ToString() != "S")
                    {
                        this.ShowCustomMessage("매출액 귀속은 T 또는 S 만 가능합니다! ", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }

                    if (dma.Rows[i]["BSVNCDNM"].ToString() == "")
                    {
                        this.ShowCustomMessage("매출액 거래처코드가 올바르지 않습니다! ", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }

                    if (dma.Rows[i]["BSHMCDNM"].ToString() == "")
                    {
                        this.ShowCustomMessage("매출액 취급화물코드가 올바르지 않습니다! ", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }

            //취급량 자료 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_783FZ349", fsSSID, "SU");
            DataTable dsu = this.DbConnector.ExecuteDataTable();
            if (dsu.Rows.Count > 0)
            {
                for (int i = 0; i < dsu.Rows.Count; i++)
                {
                    if (dsu.Rows[i]["BSDPAC"].ToString() != "T" && dsu.Rows[i]["BSDPAC"].ToString() != "S")
                    {
                        this.ShowCustomMessage("매출액 귀속은 T 또는 S 만 가능합니다! ", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }

                    if (dsu.Rows[i]["BSVNCDNM"].ToString() == "")
                    {
                        this.ShowCustomMessage("매출액 거래처코드가 올바르지 않습니다! ", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }

                    if (dsu.Rows[i]["BSHMCDNM"].ToString() == "")
                    {
                        this.ShowCustomMessage("매출액 취급화물코드가 올바르지 않습니다! ", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }

            DataTable dcm = new DataTable();
            bool bresult = false;

            dcm.Clear();
            //영업비용-공통(CM),자체(SE) 영업외손익(PR).비용(CO)
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_783G1350", fsSSID, "CM");
            dcm = this.DbConnector.ExecuteDataTable();
            if (dcm.Rows.Count > 0)
            {
                bresult = UP_SaleRowDataCheck(dcm);
                if (bresult == false)
                {
                    e.Successed = false;
                    return;
                }
            }

            dcm.Clear();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_783G1350", fsSSID, "SE");
            dcm = this.DbConnector.ExecuteDataTable();
            if (dcm.Rows.Count > 0)
            {
                bresult = UP_SaleRowDataCheck(dcm);
                if (bresult == false)
                {
                    e.Successed = false;
                    return;
                }
            }

            dcm.Clear();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_783G1350", fsSSID, "PR");
            dcm = this.DbConnector.ExecuteDataTable();
            if (dcm.Rows.Count > 0)
            {
                bresult = UP_SaleRowDataCheck(dcm);
                if (bresult == false)
                {
                    e.Successed = false;
                    return;
                }
            }

            dcm.Clear();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_783G1350", fsSSID, "CO");
            dcm = this.DbConnector.ExecuteDataTable();
            if (dcm.Rows.Count > 0)
            {
                bresult = UP_SaleRowDataCheck(dcm);
                if (bresult == false)
                {
                    e.Successed = false;
                    return;
                }
            }

            dcm.Clear();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_787FV364", fsSSID, "IN");
            dcm = this.DbConnector.ExecuteDataTable();
            if (dcm.Rows.Count > 0)
            {
                bresult = UP_FundRowDataCheck(dcm);
                if (bresult == false)
                {
                    e.Successed = false;
                    return;
                }
            }

            dcm.Clear();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_787FV364", fsSSID, "RE");
            dcm = this.DbConnector.ExecuteDataTable();
            if (dcm.Rows.Count > 0)
            {
                bresult = UP_FundRowDataCheck(dcm);
                if (bresult == false)
                {
                    e.Successed = false;
                    return;
                }
            }

            //해당파트 자료가 등록되어 있는지
            //매출액, 취급량
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_7AGIS803", fsSSID, TYUserInfo.EmpNo);
            iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());
            if ( iCnt > 0 )
            {
                this.ShowCustomMessage("이미 등록된 매출액,취급량 자료입니다. 삭제후 다시 Upload 하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            //영업비용,영업외손익
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_7AGIT804", fsSSID, TYUserInfo.EmpNo);
            iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());
            if (iCnt > 0)
            {
                this.ShowCustomMessage("이미 등록된 영업비용,영업외손익 자료입니다. 삭제후 다시 Upload 하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            //투자.수선
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_7AGJ1805", fsSSID, TYUserInfo.EmpNo);
            iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());
            if (iCnt > 0)
            {
                this.ShowCustomMessage("이미 등록된 투자,수선 자료입니다. 삭제후 다시 Upload 하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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


        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            new TYBSUP01C1().ShowDialog();
        }
        #endregion

        #region  Description : UP_SaleRowDataCheck 이벤트
        private bool UP_SaleRowDataCheck(DataTable dcm )
        {
            string sName = string.Empty;
            string sLine = string.Empty;
            
            for (int i = 0; i < dcm.Rows.Count; i++)
            {
                switch (dcm.Rows[i]["BCFORM"].ToString())
                {
                    case "CM":
                        sName = "영업비용:공통";
                        break;
                    case "SE":
                        sName = "영업비용:자체";
                        break;
                    case "PR":
                        sName = "영업외손익";
                        break;
                    case "CO":
                        sName = "영업외비용";
                        break;
                }

                sLine = "(" + (i + 1).ToString() + " 행)";

                if (dcm.Rows[i]["BCDPMKNM"].ToString() == "")
                {
                    this.ShowCustomMessage(sLine + ": " + sName + " - 담당부서코드가  올바르지 않습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
                if (dcm.Rows[i]["BCDPACNM"].ToString() == "")
                {
                    this.ShowCustomMessage(sLine + ": " + sName + " - 귀속부서코드가  올바르지 않습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
                if (dcm.Rows[i]["BCADACNM"].ToString() == "")
                {
                    this.ShowCustomMessage(sLine + ": " + sName + " - 계정과목코드가  올바르지 않습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
                if (dcm.Rows[i]["BCCDACNM"].ToString() == "")
                {
                    this.ShowCustomMessage(sLine + ": " + sName + " - 계정세목코드가  올바르지 않습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }

                if (dcm.Rows[i]["BCITEM"].ToString() != "" && dcm.Rows[i]["BCITEMNAME"].ToString() == "")
                {
                    this.ShowCustomMessage(sLine + ": " + sName + " - 항목명이 올바르지 않습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;                    
                }
            }

            return true;
        }
        #endregion

        #region  Description : UP_FundRowDataCheck 이벤트
        private bool UP_FundRowDataCheck(DataTable dcm)
        {
            string sName = string.Empty;
            string sLine = string.Empty;

            for (int i = 0; i < dcm.Rows.Count; i++)
            {
                switch (dcm.Rows[i]["BVFORM"].ToString())
                {
                    case "IN":
                        sName = "투자";
                        break;
                    case "RE":
                        sName = "수선";
                        break;
                }

                sLine = "(" + (i + 1).ToString() + " 행)";

                if (dcm.Rows[i]["BVDPMKNM"].ToString() == "")
                {
                    this.ShowCustomMessage(sLine + ": " + sName + " - 담당부서코드가  올바르지 않습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
                if (dcm.Rows[i]["BVDPACNM"].ToString() == "")
                {
                    this.ShowCustomMessage(sLine + ": " + sName + " - 귀속부서코드가  올바르지 않습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region  Description : 찾아보기 버튼 이벤트
        private void BTN61_SEARCH_Click(object sender, EventArgs e)
        {
            UP_Set_DataClear();

            ListBox_FileName.Items.Clear();

            OpenFileDialog fileDlg = new OpenFileDialog();

            //fileDlg.Filter = "Excel 97-2003통합 문서(*.xls)|*.xls|Excel 통합 문서 (.xlsx)|*.xlsx|All Files (*.*)|*.*";
            fileDlg.Filter = "Excel 통합 문서 (.xlsx)|*.xlsx|Excel 97-2003통합 문서(*.xls)|*.xls|All Files (*.*)|*.*";
            fileDlg.Multiselect = true;
            
            if (fileDlg.ShowDialog() == DialogResult.OK)
            {
                string[] arrayFileName = fileDlg.FileNames;

                if (arrayFileName.Length > 0)
                {
                    for (int i = 0; i < arrayFileName.Length; i++)
                    {
                        ListBox_FileName.Items.Add(arrayFileName[i]);
                    }
                }
            }
        }
        #endregion

        #region  Description : 그리드 클리어 함수
        private void UP_SpreadClear()
        {
            this.FPS91_TY_S_AC_782HJ339.Initialize();
            this.FPS91_TY_S_AC_782HT340.Initialize();
            this.FPS91_TY_S_AC_783DO342.Initialize();
            this.FPS91_TY_S_AC_783E1344.Initialize();
            this.FPS91_TY_S_AC_783E1345.Initialize();
            this.FPS91_TY_S_AC_783E1346.Initialize();
            this.FPS91_TY_S_AC_787FW366.Initialize();
            this.FPS91_TY_S_AC_787FZ369.Initialize();
        }
        #endregion

        #region  Description : 임시 db 클리어 함수
        private void UP_TempDbClear()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_782BK328", fsSSID);
            this.DbConnector.Attach("TY_P_AC_782BN330", fsSSID);
            this.DbConnector.Attach("TY_P_AC_782BQ332", fsSSID);
            this.DbConnector.ExecuteTranQueryList();
        }
        #endregion

        #region  Description : 영업계획(매출액,취급량)  DataTable 만들기
        private DataTable UP_DataTable_BPUPSALEPLF()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("BSFORM", typeof(System.String));
            dt.Columns.Add("BSYEAR", typeof(System.String));
            dt.Columns.Add("BSDPAC", typeof(System.String));
            dt.Columns.Add("BSDPACNM", typeof(System.String));
            dt.Columns.Add("BSVNCD", typeof(System.String));
            dt.Columns.Add("BSVNCDNM", typeof(System.String));
            dt.Columns.Add("BSHMCD", typeof(System.String));
            dt.Columns.Add("BSHMCDNM", typeof(System.String));
            dt.Columns.Add("BSMONAMT01", typeof(System.Double));
            dt.Columns.Add("BSMONAMT02", typeof(System.Double));
            dt.Columns.Add("BSMONAMT03", typeof(System.Double));
            dt.Columns.Add("BSMONAMT04", typeof(System.Double));
            dt.Columns.Add("BSMONAMT05", typeof(System.Double));
            dt.Columns.Add("BSMONAMT06", typeof(System.Double));
            dt.Columns.Add("BSMONAMT07", typeof(System.Double));
            dt.Columns.Add("BSMONAMT08", typeof(System.Double));
            dt.Columns.Add("BSMONAMT09", typeof(System.Double));
            dt.Columns.Add("BSMONAMT10", typeof(System.Double));
            dt.Columns.Add("BSMONAMT11", typeof(System.Double));
            dt.Columns.Add("BSMONAMT12", typeof(System.Double));
            dt.Columns.Add("BSMONTOTAL", typeof(System.Double));

            dt.TableName = "BPUPSALEPLF";

            return dt;
        }
        #endregion

        #region  Description : 영업계획(공통,자체,영업외손익, 영업외비용)   DataTable 만들기
        private DataTable UP_DataTable_BPUPSALECOF()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("BCFORM", typeof(System.String));
            dt.Columns.Add("BCYEAR", typeof(System.String));
            dt.Columns.Add("BCDPMK", typeof(System.String));
            dt.Columns.Add("BCDPMKNM", typeof(System.String));
            dt.Columns.Add("BCDPAC", typeof(System.String));
            dt.Columns.Add("BCDPACNM", typeof(System.String));
            dt.Columns.Add("BCADAC", typeof(System.String));
            dt.Columns.Add("BCADACNM", typeof(System.String));
            dt.Columns.Add("BCCDAC", typeof(System.String));
            dt.Columns.Add("BCCDACNM", typeof(System.String));
            dt.Columns.Add("BCSEQ", typeof(System.String));
            dt.Columns.Add("BCITEM", typeof(System.String));
            dt.Columns.Add("BCITEMNAME", typeof(System.String));
            dt.Columns.Add("BCMONAMT01", typeof(System.Double));
            dt.Columns.Add("BCMONAMT02", typeof(System.Double));
            dt.Columns.Add("BCMONAMT03", typeof(System.Double));
            dt.Columns.Add("BCMONAMT04", typeof(System.Double));
            dt.Columns.Add("BCMONAMT05", typeof(System.Double));
            dt.Columns.Add("BCMONAMT06", typeof(System.Double));
            dt.Columns.Add("BCMONAMT07", typeof(System.Double));
            dt.Columns.Add("BCMONAMT08", typeof(System.Double));
            dt.Columns.Add("BCMONAMT09", typeof(System.Double));
            dt.Columns.Add("BCMONAMT10", typeof(System.Double));
            dt.Columns.Add("BCMONAMT11", typeof(System.Double));
            dt.Columns.Add("BCMONAMT12", typeof(System.Double));
            dt.Columns.Add("BCMONTOTAL", typeof(System.Double));
            dt.Columns.Add("BCMEMO", typeof(System.String));

            dt.TableName = "BPUPSALECOF";

            return dt;
        }
        #endregion

        #region  Description : 영업계획(투자,수선) DataTable 만들기
        private DataTable UP_DataTable_BPUPINVF()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("BVFORM", typeof(System.String));
            dt.Columns.Add("BVYEAR", typeof(System.String));
            dt.Columns.Add("BVDPMK", typeof(System.String));
            dt.Columns.Add("BVDPAC", typeof(System.String));
            dt.Columns.Add("BVTYPE", typeof(System.String));
            dt.Columns.Add("BVASETGN", typeof(System.String));
            dt.Columns.Add("BVSEQ", typeof(System.String));
            dt.Columns.Add("BVITEM", typeof(System.String));
            dt.Columns.Add("BVITEMNAME", typeof(System.String));
            dt.Columns.Add("BVMONAMT01", typeof(System.Double));
            dt.Columns.Add("BVMONAMT02", typeof(System.Double));
            dt.Columns.Add("BVMONAMT03", typeof(System.Double));
            dt.Columns.Add("BVMONAMT04", typeof(System.Double));
            dt.Columns.Add("BVMONAMT05", typeof(System.Double));
            dt.Columns.Add("BVMONAMT06", typeof(System.Double));
            dt.Columns.Add("BVMONAMT07", typeof(System.Double));
            dt.Columns.Add("BVMONAMT08", typeof(System.Double));
            dt.Columns.Add("BVMONAMT09", typeof(System.Double));
            dt.Columns.Add("BVMONAMT10", typeof(System.Double));
            dt.Columns.Add("BVMONAMT11", typeof(System.Double));
            dt.Columns.Add("BVMONAMT12", typeof(System.Double));
            dt.Columns.Add("BVMONTOTAL", typeof(System.Double));
            dt.Columns.Add("BVCMYYMM", typeof(System.String));

            dt.TableName = "BPUPINVF";

            return dt;
        }
        #endregion

        #region  Description : 데이타 클리어 함수
        private void UP_Set_DataClear()
        {
            this.FPS91_TY_S_AC_782HJ339.Initialize();
            this.FPS91_TY_S_AC_782HT340.Initialize();

        }
        #endregion

       

      
    }
}
