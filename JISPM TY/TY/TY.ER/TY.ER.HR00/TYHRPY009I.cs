using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 급여인상액관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.04.13 14:44
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_54DFW178 : 급여인상액관리 기초자료 조회(사번별)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_54DFY179 : 급여인상액관리 기초자료 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  ESCEMPCNT : 종업원수
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRPY009I : TYBase
    {
        private DataTable fsdt;

        private string fsPEYEAR;
        private string fsPESEQ;
        private string fsPECRDATE;
        private string fsPESABUN;
        private string fsPEJKCD;
        private string fsCallPg;


        #region  Description : 폼 로드 이벤트
        public TYHRPY009I(DataTable dt, string sPEYEAR, string sPESEQ, string sPECRDATE, string sPESABUN, string sPEJKCD, string sCallPg)
        {
            InitializeComponent();

            fsdt = dt;

            fsPEYEAR =  sPEYEAR;
            fsPESEQ = sPESEQ;
            fsPECRDATE = sPECRDATE;
            fsPESABUN = sPESABUN;
            fsPEJKCD = sPEJKCD;
            fsCallPg = sCallPg;
        }

        private void TYHRPY009I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.UP_Spread_Title();

            if (string.IsNullOrEmpty(this.fsPEYEAR))
            {
                this.BTN61_SAV.Visible = true;
                this.BTN61_REM.Visible = false;

                if (string.IsNullOrEmpty(this.fsCallPg))
                {
                    this.UP_DataBinding();
                }
                else
                {
                    //호봉인상 대상자
                    this.UP_HoBNDataBinding();
                }
            }
            else
            {
                this.BTN61_SAV.Visible = false;
                this.BTN61_REM.Visible = true;
                this.UP_DataRun();
            }

            this.SetStartingFocus(this.DTP01_SDATE);
        }
        #endregion

        #region Description : 스프레드 타이틀 변경
        private void UP_Spread_Title()
        {
            this.FPS91_TY_S_HR_54DFY179_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_HR_54DFY179_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_HR_54DFY179_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_HR_54DFY179_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_HR_54DFY179_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);
            this.FPS91_TY_S_HR_54DFY179_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);
            this.FPS91_TY_S_HR_54DFY179_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1);
            
            this.FPS91_TY_S_HR_54DFY179_Sheet1.AddColumnHeaderSpanCell(0, 13, 2, 1);
            this.FPS91_TY_S_HR_54DFY179_Sheet1.AddColumnHeaderSpanCell(0, 14, 2, 1);
            this.FPS91_TY_S_HR_54DFY179_Sheet1.AddColumnHeaderSpanCell(0, 15, 2, 1);
            this.FPS91_TY_S_HR_54DFY179_Sheet1.AddColumnHeaderSpanCell(0, 16, 2, 1);
            this.FPS91_TY_S_HR_54DFY179_Sheet1.AddColumnHeaderSpanCell(0, 17, 2, 1);

            this.FPS91_TY_S_HR_54DFY179_Sheet1.ColumnHeader.Cells[0, 0].Value = "사 번";
            this.FPS91_TY_S_HR_54DFY179_Sheet1.ColumnHeader.Cells[0, 1].Value = "이 름";
            this.FPS91_TY_S_HR_54DFY179_Sheet1.ColumnHeader.Cells[0, 2].Value = "직 급";
            this.FPS91_TY_S_HR_54DFY179_Sheet1.ColumnHeader.Cells[0, 3].Value = "급여코드";
            this.FPS91_TY_S_HR_54DFY179_Sheet1.ColumnHeader.Cells[0, 4].Value = "급여명";

            this.FPS91_TY_S_HR_54DFY179_Sheet1.ColumnHeader.Cells[0, 13].Value = "증감액";

            this.FPS91_TY_S_HR_54DFY179_Sheet1.ColumnHeader.Cells[0, 14].Value = "피크년차";
            this.FPS91_TY_S_HR_54DFY179_Sheet1.ColumnHeader.Cells[0, 15].Value = "감액율(%)";
            this.FPS91_TY_S_HR_54DFY179_Sheet1.ColumnHeader.Cells[0, 16].Value = "비  고";
            this.FPS91_TY_S_HR_54DFY179_Sheet1.ColumnHeader.Cells[0, 17].Value = "기준율";


            this.FPS91_TY_S_HR_54DFY179_Sheet1.AddColumnHeaderSpanCell(0, 5, 1, 4);
            this.FPS91_TY_S_HR_54DFY179_Sheet1.AddColumnHeaderSpanCell(0, 9, 1, 4);

            this.FPS91_TY_S_HR_54DFY179_Sheet1.ColumnHeader.Cells[0, 5].Value = "변경전";
            this.FPS91_TY_S_HR_54DFY179_Sheet1.ColumnHeader.Cells[0, 9].Value = "변경후";


            this.FPS91_TY_S_HR_54DFY179_Sheet1.ColumnHeader.Cells[1, 5].Value = "시작일자";
            this.FPS91_TY_S_HR_54DFY179_Sheet1.ColumnHeader.Cells[1, 6].Value = "급여기준금액";
            this.FPS91_TY_S_HR_54DFY179_Sheet1.ColumnHeader.Cells[1, 7].Value = "호  봉";
            this.FPS91_TY_S_HR_54DFY179_Sheet1.ColumnHeader.Cells[1, 8].Value = "급여합계";

            this.FPS91_TY_S_HR_54DFY179_Sheet1.ColumnHeader.Cells[1, 9].Value = "시작일자";
            this.FPS91_TY_S_HR_54DFY179_Sheet1.ColumnHeader.Cells[1, 10].Value = "급여기준금액";
            this.FPS91_TY_S_HR_54DFY179_Sheet1.ColumnHeader.Cells[1, 11].Value = "호  봉";
            this.FPS91_TY_S_HR_54DFY179_Sheet1.ColumnHeader.Cells[1, 12].Value = "급여합계";

            this.FPS91_TY_S_HR_54DFY179_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_54DFY179_Sheet1.ColumnHeader.Cells[0, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;  
            
        }
        #endregion

        #region  Description : 기초자료 조회
        private void UP_DataBinding()
        {            
            string sPreSabun = string.Empty;

            string sKIJUNDATE = this.DTP01_SDATE.GetString().ToString().Substring(0,6)+"01";
            string sPYSABUN = string.Empty;

            this.TXT01_ESCEMPCNT.SetValue(fsdt.Rows.Count.ToString());

            if (fsdt.Rows.Count > 0)
            {
                TXT01_PEYEAR.SetValue(sKIJUNDATE.Substring(0, 4));
                TXT01_PESEQ.SetValue(Set_Fill3(Convert.ToString(UP_KeyAuto(sKIJUNDATE.Substring(0,4)))));

                for (int i = 0; i < fsdt.Rows.Count; i++)
                {
                    sPYSABUN = sPYSABUN + fsdt.Rows[i]["KBSABUN"].ToString()+ ",";
                }

                this.FPS91_TY_S_HR_54DFY179.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_54DFW178", sKIJUNDATE, sPYSABUN);
                this.FPS91_TY_S_HR_54DFY179.SetValue(this.DbConnector.ExecuteDataTable());

                for (int i = 0; i < FPS91_TY_S_HR_54DFY179.ActiveSheet.Rows.Count; i++)
                {
                    if (this.FPS91_TY_S_HR_54DFY179.GetValue(i, "KBCHHOBN").ToString() != "")
                    {
                        if (sPreSabun != "" && sPreSabun == this.FPS91_TY_S_HR_54DFY179.GetValue(i, "PDSABUN").ToString())
                        {
                            this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 7].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                            this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 7].Column.Locked = true;
                            this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 7].Text = "";

                            this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 11].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                            this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 11].Text = "";
                        }
                    }
                    else
                    {
                        this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 7].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                        this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 7].Column.Locked = true;
                        this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 7].Text = "";

                        this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 11].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                        this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 11].Column.Locked = true;
                        this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 11].Text = "";
                    }

                    sPreSabun = this.FPS91_TY_S_HR_54DFY179.GetValue(i, "PDSABUN").ToString();
                }

            }
        }
        #endregion

        #region  Description : 자료 확인 
        private void UP_DataRun()
        {
            string sPreSabun = string.Empty;

            this.FPS91_TY_S_HR_54DFY179.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_5B5ID108", fsPEYEAR, fsPESEQ, fsPECRDATE, fsPESABUN, fsPEJKCD);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            this.FPS91_TY_S_HR_54DFY179.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                this.TXT01_PEYEAR.SetValue(dt.Rows[0]["PEYEAR"].ToString());
                this.TXT01_PESEQ.SetValue(Set_Fill3(dt.Rows[0]["PESEQ"].ToString()));
                this.DTP01_SDATE.SetValue(dt.Rows[0]["PECRDATE"].ToString());
                this.TXT01_ESCEMPCNT.SetValue("1");
                this.DTP01_SDATE.SetReadOnly(true);
            }

            for (int i = 0; i < FPS91_TY_S_HR_54DFY179.ActiveSheet.Rows.Count; i++)
            {
                if (this.FPS91_TY_S_HR_54DFY179.GetValue(i, "KBCHHOBN").ToString() != "")
                {
                    if (sPreSabun != "" && sPreSabun == this.FPS91_TY_S_HR_54DFY179.GetValue(i, "PDSABUN").ToString())
                    {
                        this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 7].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                        this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 7].Column.Locked = true;
                        this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 7].Text = "";

                        this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 11].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                        this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 11].Text = "";
                    }
                }
                else
                {
                    this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 7].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 7].Column.Locked = true;
                    this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 7].Text = "";

                    this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 11].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 11].Column.Locked = true;
                    this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 11].Text = "";
                }

                sPreSabun = this.FPS91_TY_S_HR_54DFY179.GetValue(i, "PDSABUN").ToString();
            }
        }
        #endregion

        #region  Description : 호봉대상자 자료 조회
        private void UP_HoBNDataBinding()
        {
            string sPreSabun = string.Empty;

            string sKIJUNDATE = this.DTP01_SDATE.GetString().ToString().Substring(0,6)+"01";
            string sPYSABUN = string.Empty;

            this.TXT01_ESCEMPCNT.SetValue(fsdt.Rows.Count.ToString());

            if (fsdt.Rows.Count > 0)
            {
                TXT01_PEYEAR.SetValue(sKIJUNDATE.Substring(0, 4));
                TXT01_PESEQ.SetValue(Set_Fill3(Convert.ToString(UP_KeyAuto(sKIJUNDATE.Substring(0, 4)))));

                this.FPS91_TY_S_HR_54DFY179.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_5B6FE122", sKIJUNDATE, fsdt.Rows[0]["SNYYMM"].ToString(), fsdt.Rows[0]["SNBALYY"].ToString(),fsdt.Rows[0]["SNBALSEQ"].ToString());
                this.FPS91_TY_S_HR_54DFY179.SetValue(this.DbConnector.ExecuteDataTable());

                for (int i = 0; i < FPS91_TY_S_HR_54DFY179.ActiveSheet.Rows.Count; i++)
                {
                    if (this.FPS91_TY_S_HR_54DFY179.GetValue(i, "KBCHHOBN").ToString() != "")
                    {
                        if (sPreSabun != "" && sPreSabun == this.FPS91_TY_S_HR_54DFY179.GetValue(i, "PDSABUN").ToString())
                        {
                            this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 7].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                            this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 7].Column.Locked = true;
                            this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 7].Text = "";

                            this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 11].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                            this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 11].Text = "";
                        }
                    }
                    else
                    {
                        this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 7].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                        this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 7].Column.Locked = true;
                        this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 7].Text = "";

                        this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 11].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                        this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 11].Column.Locked = true;
                        this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[i, 11].Text = "";
                    }

                    sPreSabun = this.FPS91_TY_S_HR_54DFY179.GetValue(i, "PDSABUN").ToString();
                }

            }
        }
        #endregion

        #region  Description : 급여 인상번호 생성
        private Int16 UP_KeyAuto(string sYear)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_54F9V209", sYear);
            Int16 iSeq = Convert.ToInt16(this.DbConnector.ExecuteScalar());

            return iSeq;
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sSABUN   = string.Empty;
            string sPAYCODE = string.Empty;
            string sPAYSDATE = string.Empty;
            string sPAYEDATE = string.Empty;
            string sPAYAFSDATE = string.Empty;
            string sJKCD = string.Empty;
            string sCHHOBN = string.Empty;
            string sAFHOBN = string.Empty;
            string sPDSTRATE = string.Empty;
            
            double dPAYCHSTAMOUT = 0;
            double dPAYAFSTAMOUT = 0;

            DateTime TmEdate = new DateTime();           
            
            this.DbConnector.CommandClear();
            for (int i = 0; i < FPS91_TY_S_HR_54DFY179.ActiveSheet.Rows.Count; i++)
            {
                sSABUN    = this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 0].Text;
                sJKCD     = this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 2].Text;
                sPAYCODE  = this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 3].Text;
                sPAYSDATE   = this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 5].Value.ToString();
                sPAYAFSDATE = this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 9].Value.ToString();

                sCHHOBN = (this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 7].Value != null ? this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 7].Value.ToString(): "");
                sAFHOBN = (this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 11].Value != null ? this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 11].Value.ToString() : "");

                TmEdate = Convert.ToDateTime(Set_Date(sPAYAFSDATE.ToString())).AddDays(-1);
                sPAYEDATE = TmEdate.Year + Set_Fill2(TmEdate.Month.ToString()) + Set_Fill2(TmEdate.Day.ToString());

                dPAYCHSTAMOUT = Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 6].Text));
                dPAYAFSTAMOUT = Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 10].Text));

                sPDSTRATE = Get_Numeric(this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 17].Text);
                
                //변경전
                //개인급여기준관리 종료처리
                this.DbConnector.Attach("TY_P_HR_54FDD211", sPAYEDATE, TYUserInfo.EmpNo, sSABUN, sPAYCODE, sPAYSDATE);

                //변경후
                this.DbConnector.Attach("TY_P_HR_4CCDC786", sSABUN, sPAYCODE, sPAYAFSDATE, dPAYAFSTAMOUT.ToString(), sPDSTRATE, "", "", TYUserInfo.EmpNo);


                //개인인상액관리 등록
                this.DbConnector.Attach("TY_P_HR_54FBW210", TXT01_PEYEAR.GetValue().ToString(), 
                                                            TXT01_PESEQ.GetValue().ToString(), 
                                                            sPAYAFSDATE, 
                                                            sSABUN,
                                                            sJKCD,
                                                            sPAYCODE,
                                                            sPAYSDATE,
                                                            sPAYEDATE,
                                                            dPAYCHSTAMOUT.ToString(),
                                                            sPAYAFSDATE,
                                                            dPAYAFSTAMOUT.ToString(),
                                                            sCHHOBN,
                                                            sAFHOBN,
                                                            "",
                                                            TYUserInfo.EmpNo
                                                            );
            }

            //호봉인상자 경우 승진급 TABLE 인상번호 표시
            if (fsCallPg == "HB")
            {
                this.DbConnector.Attach("TY_P_HR_5B6FZ123", TXT01_PEYEAR.GetValue().ToString(), TXT01_PESEQ.GetValue().ToString(),
                                                             TYUserInfo.EmpNo, 
                                                            fsdt.Rows[0]["SNYYMM"].ToString(),
                                                            fsdt.Rows[0]["SNBALYY"].ToString(),
                                                            fsdt.Rows[0]["SNBALSEQ"].ToString());
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");
        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            //DataSet ds = new DataSet();

            //ds.Tables.Add(this.FPS91_TY_S_HR_54DFY179.GetDataSourceInclude(TSpread.TActionType.Select, "PDSABUN", "KBJKCD", "KBCHHOBN","KBAFHOBN", "PDPAYCODE", "PDCHSDATE", "PDCHSTAMOUNT", "PDAFDATE", "PDAFSTAMOUNT", "PDBIGO" ));

            //if (ds.Tables[0].Rows.Count == 0)
            //{
            //    this.ShowMessage("TY_M_AC_25F59464");
            //    e.Successed = false;
            //    return;
            //}

            if (FPS91_TY_S_HR_54DFY179.ActiveSheet.Rows.Count <= 0)
            {
                this.ShowCustomMessage("저장할 자료가 없습니다!.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                e.Successed = false;
                return;
            }

            DateTime TmSdate = new DateTime();
            DateTime TmWdate = new DateTime();
            
            for (int i = 0; i < FPS91_TY_S_HR_54DFY179.ActiveSheet.Rows.Count; i++)
            {
                TmSdate = Convert.ToDateTime(Set_Date(this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 5].Value.ToString()));

                if( this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 9].Value.ToString().Length > 8 )
                    TmWdate = Convert.ToDateTime(this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 9].Value.ToString());
                else                     
                    TmWdate = Convert.ToDateTime(Set_Date(this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 9].Value.ToString()));
                

                if (TmSdate >= TmWdate)
                {
                    this.ShowCustomMessage("변경전 시작일자와 변경후 시작일자가 같거나 작을수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }             
                
                //호봉자만 체크
                if (this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 2].Value.ToString() == "3C" ||
                    this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 2].Value.ToString() == "3D" ||
                    this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 2].Value.ToString() == "2C" ||
                    this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 2].Value.ToString() == "3A" ||
                    this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 2].Value.ToString() == "3B" ||
                    this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 2].Value.ToString() == "4A")
                {
                    if (this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 11].Text.Trim() != "")
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_HR_54FHO215", this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 2].Value.ToString(),
                                                                   this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 11].Value.ToString(),
                                                                   this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 9].Value.ToString());
                        DataTable dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count <= 0)
                        {
                            this.ShowCustomMessage("사번:" + this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 0].Value.ToString() + " 직급:" + this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 2].Value.ToString() + " 호봉:" + this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 11].Value.ToString() + " 자료가 존재하지않습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            e.Successed = false;
                            return;
                        }
                    }
                }
            }
          
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }            

        }
        #endregion

        #region  Description : FPS91_TY_S_HR_54DFY179_LeaveCell 이벤트
        private void FPS91_TY_S_HR_54DFY179_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            if (e.Column == 10)
            {               

                this.UP_Spread_PayCompute(this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[e.Row, 0].Text.ToString(),
                                          this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[e.Row, 3].Text.ToString(),
                                          Convert.ToDouble(this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[e.Row, 10].Text.ToString()));
            }
        }
        #endregion

        #region  Description : FPS91_TY_S_HR_54DFY179_KeyPress 이벤트
        private void FPS91_TY_S_HR_54DFY179_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                
                int row = (e == null ? 0 : FPS91_TY_S_HR_54DFY179.ActiveSheet.ActiveRowIndex);
                this.UP_Spread_PayCompute(this.FPS91_TY_S_HR_54DFY179.GetValue(row, "PDSABUN").ToString(),
                                          this.FPS91_TY_S_HR_54DFY179.GetValue(row, "PDPAYCODE").ToString(),
                                          Convert.ToDouble(this.FPS91_TY_S_HR_54DFY179.GetValue(row, "PDAFSTAMOUNT").ToString()));
            }
        }
        #endregion

        #region  Description : 해당사번의 스트레드 값 재계산
        private void UP_Spread_PayCompute(string sKBSABUN, string sPAYCODE, double dSTAMOUNT)
        {
            double dCHPayTotal = 0;
            double dAFPayTotal = 0;

            //해당사번에 변경후 금액 합계 구하기
            for (int i = 0; i < FPS91_TY_S_HR_54DFY179.ActiveSheet.Rows.Count; i++)
            {
                if (this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 0].Text == sKBSABUN)
                {
                    dCHPayTotal += Convert.ToDouble(this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 6].Value);
                    dAFPayTotal += Convert.ToDouble(this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 10].Value);
                }          
            }
            
            //해당사번에 금액 표시
            for (int i = 0; i < FPS91_TY_S_HR_54DFY179.ActiveSheet.Rows.Count; i++)
            {
                if (this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 0].Text == sKBSABUN)
                {
                    this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 12].Text = dAFPayTotal.ToString();
                    this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 13].Text = (dAFPayTotal - dCHPayTotal).ToString();
                }
            }

        }
        #endregion

        #region  Description : DTP01_SDATE_ValueChanged 이벤트
        private void DTP01_SDATE_ValueChanged(object sender, EventArgs e)
        {
            string sDate = this.DTP01_SDATE.GetString().ToString();

            if (fsPEYEAR == "" && fsPESEQ == "")
            {
                TXT01_PEYEAR.SetValue(sDate.Substring(0, 4));
                TXT01_PESEQ.SetValue(Set_Fill3(Convert.ToString(UP_KeyAuto(sDate.Substring(0, 4)))));

                for (int i = 0; i < FPS91_TY_S_HR_54DFY179.ActiveSheet.Rows.Count; i++)
                {
                    this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 9].Text = Convert.ToString(sDate.Substring(0, 4) + "-" + sDate.Substring(4, 2) + "-" + sDate.Substring(6, 2));
                    this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 9].Value = Convert.ToString(sDate.Substring(0, 4) + sDate.Substring(4, 2) + sDate.Substring(6, 2));
                }
            }
        }
        #endregion       

        #region  Description : DTP01_SDATE_ValueChanged 이벤트
        private void FPS91_TY_S_HR_54DFY179_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                int row = (e == null ? 0 : FPS91_TY_S_HR_54DFY179.ActiveSheet.ActiveRowIndex);
                this.UP_Spread_PayCompute(this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[row, 0].Text.ToString(),
                                          this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[row, 3].Text.ToString(),
                                          Convert.ToDouble(this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[row, 10].Text.ToString()));
            }
        }
        #endregion

        #region  Description : FPS91_TY_S_HR_54DFY179_EditChange 이벤트
        private void FPS91_TY_S_HR_54DFY179_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column == 11)
            {                
                //호봉 조회
                if (this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[e.Row, 11].Value != null && this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[e.Row, 11].Value.ToString() != "")
                {
                    this.UP_GetHOBNTable(this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[e.Row, 0].Text.ToString(),
                                         this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[e.Row, 2].Text.ToString(),
                                         this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[e.Row, 11].Value.ToString(),
                                         this.FPS91_TY_S_HR_54DFY179_Sheet1.Cells[e.Row, 9].Value.ToString());
                }
            }
        }
        #endregion

        #region  Description : 호봉 조회/호봉 조정 함수
        private void UP_GetHOBNTable(string sKBSABUN, string sJKCD, string sHOBN, string sSDATE)
        {
            string sPAYCODE = string.Empty;
                       
            double dPAYSTAMOUNT = 0;
            double dPAYCHSTAMOUNTHAP = 0;
            double dPAYAFSTAMOUNTHAP = 0;

            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_54FHO215", sJKCD, sHOBN, sSDATE);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowCustomMessage("직급:" + sJKCD + " 호봉:" + sHOBN+" 자료가 존재하지않습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            //호봉 표시
            for (int i = 0; i < FPS91_TY_S_HR_54DFY179.ActiveSheet.Rows.Count; i++)
            {
                if (this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 0].Text == sKBSABUN)
                {
                    dPAYCHSTAMOUNTHAP = Convert.ToDouble(this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 8].Text);

                    sPAYCODE     = this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 3].Text;
                    dPAYSTAMOUNT = Convert.ToDouble(Get_Numeric(dt.Compute("sum(HBSSTAMOUNT)","HBSPAYCODE = '"+sPAYCODE+"'").ToString()));
                    if( dPAYSTAMOUNT == 0 )
                        dPAYSTAMOUNT = Convert.ToDouble(this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 10].Text);
                    
                    dPAYAFSTAMOUNTHAP += dPAYSTAMOUNT;

                    this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 10].Text = dPAYSTAMOUNT.ToString();
                }
            }

            for (int i = 0; i < FPS91_TY_S_HR_54DFY179.ActiveSheet.Rows.Count; i++)
            {
                if (this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 0].Text == sKBSABUN)
                {
                    this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 12].Text = dPAYAFSTAMOUNTHAP.ToString();
                    this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 13].Text = (dPAYAFSTAMOUNTHAP - dPAYCHSTAMOUNTHAP).ToString();
                }
            }


        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion       

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            string sSABUN = string.Empty;
            string sPAYCODE = string.Empty;
            string sPAYSDATE = string.Empty;
            string sPAYEDATE = string.Empty;
            string sPAYAFSDATE = string.Empty;
            string sJKCD = string.Empty;
            string sCHHOBN = string.Empty;
            string sAFHOBN = string.Empty;

            double dPAYCHSTAMOUT = 0;
            double dPAYAFSTAMOUT = 0;

            DateTime TmEdate = new DateTime();

            this.DbConnector.CommandClear();
            for (int i = 0; i < FPS91_TY_S_HR_54DFY179.ActiveSheet.Rows.Count; i++)
            {
                sSABUN = this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 0].Text;
                sJKCD = this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 2].Text;
                sPAYCODE = this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 3].Text;
                sPAYSDATE = this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 5].Value.ToString();
                sPAYAFSDATE = this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 9].Value.ToString();

                sCHHOBN = (this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 7].Value != null ? this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 7].Value.ToString() : "");
                sAFHOBN = (this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 11].Value != null ? this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 11].Value.ToString() : "");

                TmEdate = Convert.ToDateTime(Set_Date(sPAYAFSDATE.ToString())).AddDays(-1);
                sPAYEDATE = TmEdate.Year + Set_Fill2(TmEdate.Month.ToString()) + Set_Fill2(TmEdate.Day.ToString());

                dPAYCHSTAMOUT = Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 6].Text));
                dPAYAFSTAMOUT = Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 10].Text));


                //TmEdate = Convert.ToDateTime(this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 9].Value.ToString());
                //sPAYAFSDATE = TmEdate.Year + Set_Fill2(TmEdate.Month.ToString()) + Set_Fill2(TmEdate.Day.ToString());

                //변경후 
                //개인급여기준관리 삭제처리
                this.DbConnector.Attach("TY_P_HR_4CCDE788", sSABUN, sPAYCODE, sPAYAFSDATE);

                //변경전
                //개인급여기준관리 종료일자 클리어
                this.DbConnector.Attach("TY_P_HR_54FDD211", "", TYUserInfo.EmpNo, sSABUN, sPAYCODE, sPAYSDATE);

                //개인급여기준관리 공제항목 종료일자 클리어
                this.DbConnector.Attach("TY_P_HR_54FDD211", "", TYUserInfo.EmpNo, sSABUN, "2", sPAYSDATE);

                //개인인상액관리 삭제
                this.DbConnector.Attach("TY_P_HR_5B6AG111", TXT01_PEYEAR.GetValue().ToString(),
                                                            TXT01_PESEQ.GetValue().ToString(),
                                                            sPAYAFSDATE,
                                                            sSABUN,
                                                            sJKCD
                                                            );
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            
            string sPAYEDATE;

            if (FPS91_TY_S_HR_54DFY179.ActiveSheet.Rows.Count <= 0)
            {
                this.ShowCustomMessage("삭제할 자료가 없습니다!.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                e.Successed = false;
                return;
            }

            //급여지급 자료가 있는지 체크 / 개인급여기준에 있는 급여자료와 동일한지 체크
            for (int i = 0; i < FPS91_TY_S_HR_54DFY179.ActiveSheet.Rows.Count; i++)
            {

                //TmEdate = Convert.ToDateTime(this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 9].Value.ToString());
                //sPAYEDATE = TmEdate.Year + Set_Fill2(TmEdate.Month.ToString()) + Set_Fill2(TmEdate.Day.ToString());

                sPAYEDATE = this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 9].Value.ToString();
                
                //급여지급내역 체크
                
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_5B6A4109", sPAYEDATE.Substring(0,6),
                                                           this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 0].Value.ToString());
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToInt16(dt.Rows[0][0].ToString()) > 0)
                    {
                        this.ShowCustomMessage("급여지급내역이 존재합니다! 삭제할수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }

                //개인급여지급 기준에 있는 자료와 동일한 자료인지 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_5B6AJ113", this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 0].Value.ToString(),
                                                            this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 3].Value.ToString(),
                                                            sPAYEDATE);
                DataTable dk = this.DbConnector.ExecuteDataTable();

                if (dk.Rows.Count <= 0)
                {
                    this.ShowCustomMessage("개인기준관리에 자료가 존재하지 않습니다! 삭제할수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
                else
                {                   
                    if ( Convert.ToDouble(dk.Rows[0]["PDSTAMOUNT"].ToString()) != Convert.ToDouble(this.FPS91_TY_S_HR_54DFY179.ActiveSheet.Cells[i, 10].Value.ToString()))
                    {
                        this.ShowCustomMessage("개인기준관리에 기준금액과 인상액 금액이 다릅니다! 삭제할수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
                
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

        }
        #endregion

    }
}
