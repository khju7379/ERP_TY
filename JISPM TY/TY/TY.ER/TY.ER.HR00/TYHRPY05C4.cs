using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.Data.OleDb;

namespace TY.ER.HR00
{
    /// <summary>
    /// 급여대상자관리 팝업 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2014.12.18 17:03
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4CJDS888 : 급여대상자관리 삭제
    ///  TY_P_HR_4CJDT890 : 급여대상자관리 등록
    ///  TY_P_HR_4CJDT891 : 급여대상자관리 수정
    ///  TY_P_HR_4CJDX894 : 급여대상자관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_4CJDZ899 : 급여대상자관리 조회
    ///  TY_S_HR_4CJE3900 : 급여대상자 리스트 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  PTGUBN : 급여구분
    ///  PTJIDATE : 지급일자
    ///  PTYYMM : 급여년월
    /// </summary>
    public partial class TYHRPY05C4 : TYBase
    {
        

        #region  Description : 폼 로드 이벤트
        public TYHRPY05C4()
        {
            InitializeComponent();
        }

        private void TYHRPY05C4_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_HR_72GDZ701.Initialize();

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN62_INQ.ProcessCheck += new TButton.CheckHandler(BTN62_INQ_ProcessCheck);

            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));
                                   

            this.BTN62_INQ.Text = ">>";

            TXT01_COAMT.SetValue("0");
         
            this.BTN63_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 급여대상자 리스트
        private void UP_GetSABUNLIST()
        {           
            //급여대상자 리스트
            this.FPS91_TY_S_HR_4CJE3900.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4CJEM901", this.CBH01_PTGUBN.GetValue().ToString(), this.DTP01_SDATE.GetString().Substring(0,6), this.DTP01_SDATE.GetString(), this.CBH01_KBSABUN.GetValue());
            this.FPS91_TY_S_HR_4CJE3900.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion      

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion     

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sPCJOBGN = string.Empty;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;


            DateTime dts = Convert.ToDateTime(Set_Date(DTP01_SDATE.GetString().ToString().Substring(0,6)+"01"));

            string sStartDate = dts.Year + Set_Fill2(dts.Month.ToString()) + Set_Fill2(dts.Day.ToString());

            dts = dts.AddMonths(1).AddDays(-1);

            string sEndDate = dts.Year + Set_Fill2(dts.Month.ToString()) + Set_Fill2(dts.Day.ToString());
            
            this.DbConnector.CommandClear();

            //삭제
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sPCJOBGN = "A";
                this.DbConnector.Attach("TY_P_HR_4CCDM791", sPCJOBGN,
                                                            ds.Tables[0].Rows[i]["PTSABUN"].ToString(),
                                                            CBH01_PTGUBN.GetValue().ToString(),
                                                            CBH01_PSPAYCODE.GetValue().ToString(),
                                                            sStartDate
                                                           );
            }     
       
            //등록
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //sPCJOBGN = CBH01_PSPAYCODE.GetValue().ToString().Substring(0, 1) == "1" ? "A" : "D";

                sPCJOBGN = "A";

                this.DbConnector.Attach("TY_P_HR_4CCDK789", sPCJOBGN,
                                                            ds.Tables[0].Rows[i]["PTSABUN"].ToString(),
                                                            CBH01_PTGUBN.GetValue().ToString(),
                                                            CBH01_PSPAYCODE.GetValue().ToString(),
                                                            sStartDate,
                                                            ds.Tables[0].Rows[i]["PTPAYAMOUNT"].ToString(),
                                                            sEndDate,
                                                            ds.Tables[0].Rows[i]["PTMEMO"].ToString(),
                                                            "",
                                                            TYUserInfo.EmpNo
                                                           );
            }            
            this.DbConnector.ExecuteTranQueryList();            
            
            this.ShowMessage("TY_M_GB_23NAD873");

            this.FPS91_TY_S_HR_72GDZ701.Initialize();
        }
        
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_HR_72GDZ701.GetDataSourceInclude(TSpread.TActionType.New, "PTSABUN","PTSABUNNM", "PTTEAM","PTTEAMNM", "PTJKCD","PTJKCDNM", "PTPAYAMOUNT", "PTMEMO"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }
            //else
            //{
            //     //급여지급된 일자와 겹치면 등록 안된다.
            //     this.DbConnector.CommandClear();
            //     this.DbConnector.Attach("TY_P_HR_614GM389", CBH01_PTGUBN.GetValue().ToString(),
            //                                                 DTP01_SDATE.GetString().ToString() );
            //     Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());

            //     if (iCnt > 0)
            //     {
            //         this.ShowCustomMessage("급여시작일자이후 급여지급내역이 존재합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //         e.Successed = false;
            //         return;
            //     }
            //}

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;

        }
        #endregion

        #region  Description : 선택 버튼 이벤트
        private void BTN62_INQ_Click(object sender, EventArgs e)
        {
            int iRowIndex = 0;
            
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            if (dt.Rows.Count > 0)
            {
                iRowIndex = this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Rows.Count;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    iRowIndex = iRowIndex + 1;

                    this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.AddRows(iRowIndex - 1, 1);
                    this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.RowHeader.Cells[iRowIndex - 1, 0].Text = "N";

                    this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Cells[iRowIndex - 1, 0].Text = dt.Rows[i]["KBSABUN"].ToString();
                    this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Cells[iRowIndex - 1, 1].Text = dt.Rows[i]["KBHANGL"].ToString();
                    this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Cells[iRowIndex - 1, 2].Text = dt.Rows[i]["KBBSTEAM"].ToString();
                    this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Cells[iRowIndex - 1, 3].Text = dt.Rows[i]["KBBSTEAMNM"].ToString();
                    this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Cells[iRowIndex - 1, 4].Text = dt.Rows[i]["KBJKCD"].ToString();
                    this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Cells[iRowIndex - 1, 5].Text = dt.Rows[i]["KBJKCDNM"].ToString();
                    this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Cells[iRowIndex - 1, 6].Text = "0";
                }

                this.UP_PayListCount(Convert.ToInt16(this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Rows.Count));
            }



            this.UP_GetSABUNLIST();
        }

        private void BTN62_INQ_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_4CJE3900.GetDataSourceInclude(TSpread.TActionType.Select, "KBSABUN", "KBHANGL", "KBJJCD", "KBJJCDNM", "KBJKCD", "KBJKCDNM", "KBSOSOK", "KBSOSOKNM", "KBBUSEO", "KBBUSEONM", "KBBSTEAM", "KBBSTEAMNM", "KBHOBN", "KBIDATE", "KBBALCD", "KBBALCDNM", "KBBDATE","PAYRATE");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_25F59464");
                e.Successed = false;
                return;
            }

            //급여대상자에 있는지 체크
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Rows.Count; j++)
                {
                    if (dt.Rows[i]["KBSABUN"].ToString() == this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Cells[j, 0].Text.Trim())
                    {
                        this.ShowCustomMessage("급여대상자에 등록되어 있습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }            

            //if (!this.ShowMessage("TY_M_HR_4CJGE904"))
            //{
            //    e.Successed = false;
            //    return;
            //}

            e.ArgData = dt;

        }
        #endregion

        #region  Description : 대상자 표시
        private void UP_PayListCount(Int16 iCnt)
        {
            TXT01_ESCEMPCNT.SetValue(iCnt.ToString());
        }
        #endregion        

        #region  Description : 급여대상자 조회 버튼 이벤트
        private void BTN63_INQ_Click(object sender, EventArgs e)
        {
            UP_GetSABUNLIST();

            this.FPS91_TY_S_HR_72GDZ701.Initialize();
        }
        #endregion

        #region  Description : 엑셀 찾아보기 조회 버튼 이벤트
        private void BTN61_SEARCH_Click(object sender, EventArgs e)
        {
            OpenFile.Filter = "Excel 통합 문서 (.xlsx)|*.xlsx|All Files (*.*)|*.*";

            if (this.OpenFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.txtFile.Text = this.OpenFile.FileName;
        }
        #endregion

        #region  Description : 엑셀파일 조회 버튼 이벤트
        private void BTN61_EXCEL_Click(object sender, EventArgs e)
        {
            string sTableName = string.Empty;
            string strQuery = string.Empty;

            TXT01_COAMT.SetValue("0");

            if (this.txtFile.Text.Trim() != "")
            {
                this.FPS91_TY_S_HR_72GDZ701.Initialize();

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

                this.ShowMessage("TY_M_AC_31BAP617");
            }
            else
            {
                this.ShowMessage("TY_M_AC_31B1C623");
            }
        }
        #endregion

        #region  Description : 엑셀파일 바인딩 함수
        private void UP_Set_DataBinding(DataSet ds)
        {
            int iRowIndex = 0;

            string sKBSABUN = string.Empty;
            string sKBHANGL = string.Empty;
            string sKBBSTEAM = string.Empty;
            string sKBBSTEAMNM = string.Empty;
            string sKBJKCD = string.Empty;
            string sKBJKCDNM = string.Empty;
            string sKBJUMIN = string.Empty;

            double dHeathAmt = 0;

            double dHeathAmtTotal = 0;

            if (ds.Tables[0].Rows.Count > 0)
            {
                iRowIndex = this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Rows.Count;

                for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
                {
                   
                    if (sKBSABUN != "" && sKBJUMIN != ds.Tables[0].Rows[i][7].ToString().Replace("-", "").Trim())
                    {
                        if (dHeathAmt != 0)
                        {
                            iRowIndex = iRowIndex + 1;

                            this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.AddRows(iRowIndex - 1, 1);
                            this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.RowHeader.Cells[iRowIndex - 1, 0].Text = "N";

                            this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Cells[iRowIndex - 1, 0].Text = sKBSABUN;
                            this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Cells[iRowIndex - 1, 1].Text = sKBHANGL;
                            this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Cells[iRowIndex - 1, 2].Text = sKBBSTEAM;
                            this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Cells[iRowIndex - 1, 3].Text = sKBBSTEAMNM;
                            this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Cells[iRowIndex - 1, 4].Text = sKBJKCD;
                            this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Cells[iRowIndex - 1, 5].Text = sKBJKCDNM;
                            this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Cells[iRowIndex - 1, 6].Text = dHeathAmt.ToString();


                            dHeathAmtTotal += dHeathAmt;

                            sKBSABUN = "";
                            sKBHANGL = "";
                            sKBBSTEAM = "";
                            sKBBSTEAMNM = "";
                            sKBJKCD = "";
                            sKBJKCDNM = "";
                            sKBJUMIN = "";
                            dHeathAmt = 0;
                        }
                    }
                    

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_HR_84KBJ845", TYUserInfo.SecureKey, "Y", ds.Tables[0].Rows[i][5].ToString(), TYUserInfo.SecureKey, "Y", ds.Tables[0].Rows[i][7].ToString().Replace("-", "").Trim());
                        DataTable dt = this.DbConnector.ExecuteDataTable();
                        if (dt.Rows.Count > 0)
                        {
                            //연말정산+요양연말정산보험료
                            dHeathAmt += Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][18].ToString())) + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i][27].ToString()));                                                  

                            sKBSABUN = dt.Rows[0]["KBSABUN"].ToString();
                            sKBHANGL = dt.Rows[0]["KBHANGL"].ToString();
                            sKBBSTEAM = dt.Rows[0]["KBBSTEAM"].ToString();
                            sKBBSTEAMNM = dt.Rows[0]["KBBSTEAMNM"].ToString();
                            sKBJKCD = dt.Rows[0]["KBJKCD"].ToString();
                            sKBJKCDNM = dt.Rows[0]["KBJKCDNM"].ToString();
                            sKBJUMIN = dt.Rows[0]["KBJUMIN"].ToString();
                        }

                       //마지막 라인 처리
                        if (i == (ds.Tables[0].Rows.Count - 1))
                        {
                            if (dHeathAmt != 0)
                            {
                                iRowIndex = iRowIndex + 1;

                                this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.AddRows(iRowIndex - 1, 1);
                                this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.RowHeader.Cells[iRowIndex - 1, 0].Text = "N";

                                this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Cells[iRowIndex - 1, 0].Text = sKBSABUN;
                                this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Cells[iRowIndex - 1, 1].Text = sKBHANGL;
                                this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Cells[iRowIndex - 1, 2].Text = sKBBSTEAM;
                                this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Cells[iRowIndex - 1, 3].Text = sKBBSTEAMNM;
                                this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Cells[iRowIndex - 1, 4].Text = sKBJKCD;
                                this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Cells[iRowIndex - 1, 5].Text = sKBJKCDNM;
                                this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Cells[iRowIndex - 1, 6].Text = dHeathAmt.ToString();


                                dHeathAmtTotal += dHeathAmt;

                                sKBSABUN = "";
                                sKBHANGL = "";
                                sKBBSTEAM = "";
                                sKBBSTEAMNM = "";
                                sKBJKCD = "";
                                sKBJKCDNM = "";
                                sKBJUMIN = "";
                                dHeathAmt = 0;
                            }
                        }
                    
                }

                this.UP_PayListCount(Convert.ToInt16(this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Rows.Count));

                this.TXT01_COAMT.SetValue(Set_Numeric2(dHeathAmtTotal.ToString(),0));
            }
            
        }
        #endregion

        #region  Description : FPS91_TY_S_HR_4CJE3900_CellDoubleClick 이벤트
        private void FPS91_TY_S_HR_4CJE3900_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {           

            int iRowIndex = 0;

            iRowIndex = this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Rows.Count;

                iRowIndex = iRowIndex + 1;

                this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.AddRows(iRowIndex - 1, 1);
                this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.RowHeader.Cells[iRowIndex - 1, 0].Text = "N";

                this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Cells[iRowIndex - 1, 0].Text = this.FPS91_TY_S_HR_4CJE3900.GetValue("KBSABUN").ToString();
                this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Cells[iRowIndex - 1, 1].Text = this.FPS91_TY_S_HR_4CJE3900.GetValue("KBHANGL").ToString();
                this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Cells[iRowIndex - 1, 2].Text = this.FPS91_TY_S_HR_4CJE3900.GetValue("KBBSTEAM").ToString();
                this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Cells[iRowIndex - 1, 3].Text = this.FPS91_TY_S_HR_4CJE3900.GetValue("KBBSTEAMNM").ToString();
                this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Cells[iRowIndex - 1, 4].Text = this.FPS91_TY_S_HR_4CJE3900.GetValue("KBJKCD").ToString();
                this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Cells[iRowIndex - 1, 5].Text = this.FPS91_TY_S_HR_4CJE3900.GetValue("KBJKCDNM").ToString();
                this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Cells[iRowIndex - 1, 6].Text = "0";

            this.UP_PayListCount(Convert.ToInt16(this.FPS91_TY_S_HR_72GDZ701.ActiveSheet.Rows.Count));

            this.UP_GetSABUNLIST();
        }
        #endregion


    }
}
