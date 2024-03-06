using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.AC00
{
    /// <summary>
    /// 투하자금 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.08.10 17:00
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_78AH0421 : 투하자금 관리 삭제(사업계획)
    ///  TY_P_AC_78AH1422 : 투하자금 관리 확인(사업계획)
    ///  TY_P_AC_78AH5423 : 투하자금 관리 조회(사업계획)
    ///  TY_P_AC_78AH7419 : 투하자금 관리 등록(사업계획)
    ///  TY_P_AC_78AH9420 : 투하자금 관리 수정(사업계획)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_78AHA424 : 투하자금 관리(사업계획)
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    ///  TY_M_GB_43C9G671 : 삭제 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  BSYEAR : 년도
    /// </summary>
    public partial class TYACNC036S : TYBase
    {
        #region Description : 폼 로드
        public TYACNC036S()
        {
            InitializeComponent();
        }

        private void TYACNC036S_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_78AHA424, "BNYEAR", "BNDPAC");

            this.TXT01_BSYEAR.Text = System.DateTime.Now.ToString("yyyy");
            
            this.TabList.SelectedIndex = 0;

            BTN61_INQ_Click(null, null);

            SetStartingFocus(this.TXT01_BSYEAR);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            try
            {
                
                    this.FPS91_TY_S_AC_78AHA424.Initialize();
                    this.FPS91_TY_S_AC_8CBED299.Initialize();

                    if (Convert.ToInt16(this.TXT01_BSYEAR.GetValue().ToString()) < 2019)
                    {
                        this.FPS91_TY_S_AC_78AHA424.Visible = true;
                        this.FPS91_TY_S_AC_8CBED299.Visible = false;
                    }
                    else
                    {
                        this.FPS91_TY_S_AC_78AHA424.Visible = false;
                        this.FPS91_TY_S_AC_8CBED299.Visible = true;
                    }

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_78AH5423", this.TXT01_BSYEAR.GetValue().ToString());
                    DataTable dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToInt16(this.TXT01_BSYEAR.GetValue().ToString()) < 2019)
                        {
                            this.FPS91_TY_S_AC_78AHA424.SetValue(UP_ChangeDt(dt));

                            for (int i = 0; i < this.FPS91_TY_S_AC_78AHA424.ActiveSheet.RowCount; i++)
                            {
                                if (this.FPS91_TY_S_AC_78AHA424.GetValue(i, "BNDPAC").ToString() == "A")
                                {
                                    // 합계 ROW 잠그기
                                    this.FPS91_TY_S_AC_78AHA424.ActiveSheet.Rows[i].Locked = true;
                                }
                            }
                        }
                        else
                        {
                            this.FPS91_TY_S_AC_8CBED299.SetValue(UP_ChangeDt(dt));

                            for (int i = 0; i < this.FPS91_TY_S_AC_8CBED299.ActiveSheet.RowCount; i++)
                            {
                                if (this.FPS91_TY_S_AC_8CBED299.GetValue(i, "BNDPAC").ToString() == "A")
                                {
                                    // 합계 ROW 잠그기
                                    this.FPS91_TY_S_AC_8CBED299.ActiveSheet.Rows[i].Locked = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Convert.ToInt16(this.TXT01_BSYEAR.GetValue().ToString()) < 2019)
                        {
                            this.FPS91_TY_S_AC_78AHA424.SetValue(Set_EmptyDt());
                        }
                        else
                        {
                            this.FPS91_TY_S_AC_8CBED299.SetValue(Set_EmptyDt());
                        }
                    }
                    if (Convert.ToInt16(this.TXT01_BSYEAR.GetValue().ToString()) < 2019)
                    {
                        UP_Spread_Load();
                    }
                
                    //투하자금 이자조회
                    FPS91_TY_S_AC_8CCB5307.Initialize();
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_8CCB4306", this.TXT01_BSYEAR.GetValue().ToString());
                    FPS91_TY_S_AC_8CCB5307.SetValue(this.DbConnector.ExecuteDataTable());
                    if (FPS91_TY_S_AC_8CCB5307.CurrentRowCount > 0)
                    {
                        
                        for (int i = 0; i < FPS91_TY_S_AC_8CCB5307.CurrentRowCount; i++)
                        {
                            if (this.FPS91_TY_S_AC_8CBED299.GetValue(i, "BNSDPACNM").ToString() == "[합 계]")
                            {
                                for (int j = 3; j < 15; j++)
                                {
                                    FPS91_TY_S_AC_8CCB5307.ActiveSheet.Cells[i, j].Locked = true;
                                }
                            }
                        }

                        this.SetSpreadSumRow(this.FPS91_TY_S_AC_8CCB5307, "BNSDPACNM", "[합 계]", SumRowType.Total);
                    }
                
            }
            catch
            {

            }
        }
        #endregion     

        #region Description : 스프레드 타이틀 변경
        private void UP_Spread_Load()
        {
            this.FPS91_TY_S_AC_78AHA424_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_78AHA424_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_78AHA424_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_AC_78AHA424_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_AC_78AHA424_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);
            this.FPS91_TY_S_AC_78AHA424_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);
            this.FPS91_TY_S_AC_78AHA424_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1);
            this.FPS91_TY_S_AC_78AHA424_Sheet1.AddColumnHeaderSpanCell(0, 5, 2, 1);
            this.FPS91_TY_S_AC_78AHA424_Sheet1.AddColumnHeaderSpanCell(0, 6, 2, 1);
            this.FPS91_TY_S_AC_78AHA424_Sheet1.AddColumnHeaderSpanCell(0, 7, 2, 1);
            this.FPS91_TY_S_AC_78AHA424_Sheet1.AddColumnHeaderSpanCell(0, 8, 2, 1);
            this.FPS91_TY_S_AC_78AHA424_Sheet1.AddColumnHeaderSpanCell(0, 9, 1, 2);

            this.FPS91_TY_S_AC_78AHA424_Sheet1.ColumnHeader.Cells[0, 0].Value = "년도";
            this.FPS91_TY_S_AC_78AHA424_Sheet1.ColumnHeader.Cells[0, 1].Value = "귀속코드";
            this.FPS91_TY_S_AC_78AHA424_Sheet1.ColumnHeader.Cells[0, 2].Value = "귀속";
            this.FPS91_TY_S_AC_78AHA424_Sheet1.ColumnHeader.Cells[0, 3].Value = "유형자산";
            this.FPS91_TY_S_AC_78AHA424_Sheet1.ColumnHeader.Cells[0, 4].Value = "건설중인자산";
            this.FPS91_TY_S_AC_78AHA424_Sheet1.ColumnHeader.Cells[0, 5].Value = "세전이익";
            this.FPS91_TY_S_AC_78AHA424_Sheet1.ColumnHeader.Cells[0, 6].Value = "매출채권";
            this.FPS91_TY_S_AC_78AHA424_Sheet1.ColumnHeader.Cells[0, 7].Value = "매입채무";
            this.FPS91_TY_S_AC_78AHA424_Sheet1.ColumnHeader.Cells[0, 8].Value = "투하자금 계";
            this.FPS91_TY_S_AC_78AHA424_Sheet1.ColumnHeader.Cells[0, 9].Value = "투하자금 이자";

            this.FPS91_TY_S_AC_78AHA424_Sheet1.ColumnHeader.Cells[1, 9].Value = "년 반영액";
            this.FPS91_TY_S_AC_78AHA424_Sheet1.ColumnHeader.Cells[1, 10].Value = "월 반영액";

            this.FPS91_TY_S_AC_78AHA424_Sheet1.ColumnHeader.Cells[1, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion

        #region Description : 빈 테이블 생성
        private DataTable Set_EmptyDt()
        {
            DataTable retDt = new DataTable();

            retDt.Columns.Add("BNYEAR", typeof(System.String));
            retDt.Columns.Add("BNDPAC", typeof(System.String));
            retDt.Columns.Add("BNDPACNM", typeof(System.String));
            retDt.Columns.Add("BNTAASETAMT", typeof(System.String));
            retDt.Columns.Add("BNBLASETAMT", typeof(System.String));
            retDt.Columns.Add("BNPROFITAMT", typeof(System.String));
            retDt.Columns.Add("BNSALECREDAMT", typeof(System.String));
            retDt.Columns.Add("BNBUYDEBTAMT", typeof(System.String));
            retDt.Columns.Add("BNFUNDTOTAL", typeof(System.String));
            retDt.Columns.Add("BNYINTERAMT", typeof(System.String));
            retDt.Columns.Add("BNMINTERAMT", typeof(System.String));
            retDt.Columns.Add("BNYINTERRATE", typeof(System.Int16));
            
            DataRow row = retDt.NewRow();

            string sYEAR = this.TXT01_BSYEAR.GetValue().ToString();

            row["BNYEAR"] = sYEAR;
            row["BNDPAC"] = "T";
            row["BNDPACNM"] = "UTT";
            row["BNTAASETAMT"] = "0";
            row["BNBLASETAMT"] = "0";
            row["BNPROFITAMT"] = "0";
            row["BNSALECREDAMT"] = "0";
            row["BNBUYDEBTAMT"] = "0";
            row["BNFUNDTOTAL"] = "0";
            row["BNYINTERAMT"] = "0";
            row["BNMINTERAMT"] = "0";
            row["BNYINTERRATE"] = 0;

            retDt.Rows.Add(row);

            row = retDt.NewRow();

            row["BNYEAR"] = sYEAR;
            row["BNDPAC"] = "S";
            row["BNDPACNM"] = "SILO";
            row["BNTAASETAMT"] = "0";
            row["BNBLASETAMT"] = "0";
            row["BNPROFITAMT"] = "0";
            row["BNSALECREDAMT"] = "0";
            row["BNBUYDEBTAMT"] = "0";
            row["BNFUNDTOTAL"] = "0";
            row["BNYINTERAMT"] = "0";
            row["BNMINTERAMT"] = "0";
            row["BNYINTERRATE"] = 0;

            retDt.Rows.Add(row);

            row = retDt.NewRow();

            row["BNYEAR"] = sYEAR;
            row["BNDPAC"] = "";
            row["BNDPACNM"] = "계";
            row["BNTAASETAMT"] = "0";
            row["BNBLASETAMT"] = "0";
            row["BNPROFITAMT"] = "0";
            row["BNSALECREDAMT"] = "0";
            row["BNBUYDEBTAMT"] = "0";
            row["BNFUNDTOTAL"] = "0";
            row["BNYINTERAMT"] = "0";
            row["BNMINTERAMT"] = "0";
            row["BNYINTERRATE"] = 0;

            retDt.Rows.Add(row);

            row = retDt.NewRow();

            return retDt;
        }
        #endregion

        #region Description : 데이터테이블 변경
        private DataTable UP_ChangeDt(DataTable dt)
        {
            DataTable retDt = new DataTable();

            retDt.Columns.Add("BNYEAR", typeof(System.String));
            retDt.Columns.Add("BNDPAC", typeof(System.String));
            retDt.Columns.Add("BNDPACNM", typeof(System.String));
            retDt.Columns.Add("BNTAASETAMT", typeof(System.String));
            retDt.Columns.Add("BNBLASETAMT", typeof(System.String));
            retDt.Columns.Add("BNPROFITAMT", typeof(System.String));
            retDt.Columns.Add("BNSALECREDAMT", typeof(System.String));
            retDt.Columns.Add("BNBUYDEBTAMT", typeof(System.String));
            retDt.Columns.Add("BNFUNDTOTAL", typeof(System.String));
            retDt.Columns.Add("BNYINTERAMT", typeof(System.String));
            retDt.Columns.Add("BNMINTERAMT", typeof(System.String));
            retDt.Columns.Add("BNYINTERRATE", typeof(System.Int16));
            
            DataRow row = retDt.NewRow();

            string sYEAR = this.TXT01_BSYEAR.GetValue().ToString();

            string[] sBNDPAC = new string[3];

            for(int i = 0 ; i < dt.Rows.Count ; i++)
            {
                if(dt.Rows[i]["BNDPAC"].ToString() == "T")
                {
                    sBNDPAC[0] = i.ToString();
                }
                else if(dt.Rows[i]["BNDPAC"].ToString() == "S")
                {
                    sBNDPAC[1] = i.ToString();
                }
                else if(dt.Rows[i]["BNDPAC"].ToString() == "A")
                {
                    sBNDPAC[2] = i.ToString();
                }
            }

            if(sBNDPAC[0] != "" && sBNDPAC[0] != null)
            {
                int iCnt = Convert.ToInt32(sBNDPAC[0]);

                row["BNYEAR"] = dt.Rows[iCnt]["BNYEAR"].ToString();
                row["BNDPAC"] = dt.Rows[iCnt]["BNDPAC"].ToString();
                row["BNDPACNM"] = dt.Rows[iCnt]["BNDPACNM"].ToString();
                row["BNTAASETAMT"] = dt.Rows[iCnt]["BNTAASETAMT"].ToString();
                row["BNBLASETAMT"] = dt.Rows[iCnt]["BNBLASETAMT"].ToString();
                row["BNPROFITAMT"] = dt.Rows[iCnt]["BNPROFITAMT"].ToString();
                row["BNSALECREDAMT"] = dt.Rows[iCnt]["BNSALECREDAMT"].ToString();
                row["BNBUYDEBTAMT"] = dt.Rows[iCnt]["BNBUYDEBTAMT"].ToString();
                row["BNFUNDTOTAL"] = dt.Rows[iCnt]["BNFUNDTOTAL"].ToString();
                row["BNYINTERAMT"] = dt.Rows[iCnt]["BNYINTERAMT"].ToString();
                row["BNMINTERAMT"] = dt.Rows[iCnt]["BNMINTERAMT"].ToString();
                row["BNYINTERRATE"] = Convert.ToInt16(dt.Rows[iCnt]["BNYINTERRATE"].ToString());
            }
            else
            {
                row["BNYEAR"] = sYEAR;
                row["BNDPAC"] = "T";
                row["BNDPACNM"] = "UTT";
                row["BNTAASETAMT"] = "0";
                row["BNBLASETAMT"] = "0";
                row["BNPROFITAMT"] = "0";
                row["BNSALECREDAMT"] = "0";
                row["BNBUYDEBTAMT"] = "0";
                row["BNFUNDTOTAL"] = "0";
                row["BNYINTERAMT"] = "0";
                row["BNMINTERAMT"] = "0";
                row["BNYINTERRATE"] = 0;
            }

            retDt.Rows.Add(row);

            row = retDt.NewRow();

            if(sBNDPAC[1] != "" && sBNDPAC[1] != null)
            {
                int iCnt = Convert.ToInt32(sBNDPAC[1]);

                row["BNYEAR"] = dt.Rows[iCnt]["BNYEAR"].ToString();
                row["BNDPAC"] = dt.Rows[iCnt]["BNDPAC"].ToString();
                row["BNDPACNM"] = dt.Rows[iCnt]["BNDPACNM"].ToString();
                row["BNTAASETAMT"] = dt.Rows[iCnt]["BNTAASETAMT"].ToString();
                row["BNBLASETAMT"] = dt.Rows[iCnt]["BNBLASETAMT"].ToString();
                row["BNPROFITAMT"] = dt.Rows[iCnt]["BNPROFITAMT"].ToString();
                row["BNSALECREDAMT"] = dt.Rows[iCnt]["BNSALECREDAMT"].ToString();
                row["BNBUYDEBTAMT"] = dt.Rows[iCnt]["BNBUYDEBTAMT"].ToString();
                row["BNFUNDTOTAL"] = dt.Rows[iCnt]["BNFUNDTOTAL"].ToString();
                row["BNYINTERAMT"] = dt.Rows[iCnt]["BNYINTERAMT"].ToString();
                row["BNMINTERAMT"] = dt.Rows[iCnt]["BNMINTERAMT"].ToString();
                row["BNYINTERRATE"] = Convert.ToInt16(dt.Rows[iCnt]["BNYINTERRATE"].ToString());
            }
            else
            {
                row["BNYEAR"] = sYEAR;
                row["BNDPAC"] = "S";
                row["BNDPACNM"] = "SILO";
                row["BNTAASETAMT"] = "0";
                row["BNBLASETAMT"] = "0";
                row["BNPROFITAMT"] = "0";
                row["BNSALECREDAMT"] = "0";
                row["BNBUYDEBTAMT"] = "0";
                row["BNFUNDTOTAL"] = "0";
                row["BNYINTERAMT"] = "0";
                row["BNMINTERAMT"] = "0";
                row["BNYINTERRATE"] = 0;
            }

            retDt.Rows.Add(row);

            row = retDt.NewRow();

            if(sBNDPAC[2] != "" && sBNDPAC[2] != null)
            {
                int iCnt = Convert.ToInt32(sBNDPAC[2]);

                row["BNYEAR"] = dt.Rows[iCnt]["BNYEAR"].ToString();
                row["BNDPAC"] = dt.Rows[iCnt]["BNDPAC"].ToString();
                row["BNDPACNM"] = dt.Rows[iCnt]["BNDPACNM"].ToString();
                row["BNTAASETAMT"] = dt.Rows[iCnt]["BNTAASETAMT"].ToString();
                row["BNBLASETAMT"] = dt.Rows[iCnt]["BNBLASETAMT"].ToString();
                row["BNPROFITAMT"] = dt.Rows[iCnt]["BNPROFITAMT"].ToString();
                row["BNSALECREDAMT"] = dt.Rows[iCnt]["BNSALECREDAMT"].ToString();
                row["BNBUYDEBTAMT"] = dt.Rows[iCnt]["BNBUYDEBTAMT"].ToString();
                row["BNFUNDTOTAL"] = dt.Rows[iCnt]["BNFUNDTOTAL"].ToString();
                row["BNYINTERAMT"] = dt.Rows[iCnt]["BNYINTERAMT"].ToString();
                row["BNMINTERAMT"] = dt.Rows[iCnt]["BNMINTERAMT"].ToString();
                row["BNYINTERRATE"] = Convert.ToInt16(dt.Rows[iCnt]["BNYINTERRATE"].ToString());
            }
            else
            {
                row["BNYEAR"] = sYEAR;
                row["BNDPAC"] = "";
                row["BNDPACNM"] = "계";
                row["BNTAASETAMT"] = "0";
                row["BNBLASETAMT"] = "0";
                row["BNPROFITAMT"] = "0";
                row["BNSALECREDAMT"] = "0";
                row["BNBUYDEBTAMT"] = "0";
                row["BNFUNDTOTAL"] = "0";
                row["BNYINTERAMT"] = "0";
                row["BNMINTERAMT"] = "0";
                row["BNYINTERRATE"] = 0;
            }

            retDt.Rows.Add(row);

            row = retDt.NewRow();

            return retDt;
        }
        #endregion

        #region Description : 투하자금 합계 계산
        private double[] UP_GetBNFUNDTOTAL(DataTable dt)
        {
            double[] dBFMONTOTAL = new double[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["BNTAASETAMT"].ToString() != "")
                {
                    dBFMONTOTAL[i] = Convert.ToDouble(dt.Rows[i]["BNTAASETAMT"].ToString());
                }
                if (dt.Rows[i]["BNBLASETAMT"].ToString() != "")
                {
                    dBFMONTOTAL[i] += Convert.ToDouble(dt.Rows[i]["BNBLASETAMT"].ToString());
                }
                if (dt.Rows[i]["BNPROFITAMT"].ToString() != "")
                {
                    dBFMONTOTAL[i] -= Convert.ToDouble(dt.Rows[i]["BNPROFITAMT"].ToString());
                }
                if (dt.Rows[i]["BNSALECREDAMT"].ToString() != "")
                {
                    dBFMONTOTAL[i] += Convert.ToDouble(dt.Rows[i]["BNSALECREDAMT"].ToString());
                }
                if (dt.Rows[i]["BNBUYDEBTAMT"].ToString() != "")
                {
                    dBFMONTOTAL[i] -= Convert.ToDouble(dt.Rows[i]["BNBUYDEBTAMT"].ToString());
                }
            }
            return dBFMONTOTAL;
        }

        private double[] UP_Get_SpreadBNFUNDTOTAL(TYSpread Spread)
        {
            double[] dBFMONTOTAL = new double[Spread.CurrentRowCount - 1];

            for (int i = 0; i < Spread.CurrentRowCount - 1; i++)
            {               
                if (Spread.GetValue(i, "BNTAASETAMT").ToString() != "")
                {
                    dBFMONTOTAL[i] = Convert.ToDouble(Spread.GetValue(i, "BNTAASETAMT").ToString());
                }
                if (Spread.GetValue(i, "BNBLASETAMT").ToString() != "")
                {
                    dBFMONTOTAL[i] += Convert.ToDouble(Spread.GetValue(i, "BNBLASETAMT").ToString());
                }
                if (Spread.GetValue(i, "BNPROFITAMT").ToString() != "")
                {
                    dBFMONTOTAL[i] -= Convert.ToDouble(Spread.GetValue(i, "BNPROFITAMT").ToString());
                }
                if (Spread.GetValue(i, "BNSALECREDAMT").ToString() != "")
                {
                    dBFMONTOTAL[i] += Convert.ToDouble(Spread.GetValue(i, "BNSALECREDAMT").ToString());
                }
                if (Spread.GetValue(i, "BNBUYDEBTAMT").ToString() != "")
                {
                    dBFMONTOTAL[i] -= Convert.ToDouble(Spread.GetValue(i, "BNBUYDEBTAMT").ToString());
                }
            }
            return dBFMONTOTAL;
        }
        #endregion         
       
    }
}
