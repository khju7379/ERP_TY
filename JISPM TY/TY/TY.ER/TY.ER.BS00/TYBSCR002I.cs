using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.BS00
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
    public partial class TYBSCR002I : TYBase
    {
        #region Description : 폼 로드
        public TYBSCR002I()
        {
            InitializeComponent();
        }

        private void TYBSCR002I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_78AHA424, "BNYEAR", "BNDPAC");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN62_SAV.ProcessCheck += new TButton.CheckHandler(BTN62_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.TXT01_BSYEAR.Text = System.DateTime.Now.ToString("yyyy");

            this.CKB01_RATE.Checked = true;
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

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_78AH0421", dt);
                this.DbConnector.ExecuteNonQuery();

                BTN61_INQ_Click(null, null);

                this.ShowMessage("TY_M_GB_23NAD874");
            }
            catch
            {
                this.ShowMessage("TY_M_GB_43C9G671");
            }
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            if (Convert.ToInt16(this.TXT01_BSYEAR.GetValue().ToString()) < 2019)
            {
                dt = this.FPS91_TY_S_AC_78AHA424.GetDataSourceInclude(TSpread.TActionType.Remove, "BNYEAR", "BNDPAC");
            }
            else
            {
                dt = this.FPS91_TY_S_AC_8CBED299.GetDataSourceInclude(TSpread.TActionType.Remove, "BNYEAR", "BNDPAC");
            }

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;

            }
            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
            e.ArgData = dt;
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            DataTable dt = new DataTable();

            double dBNAMTTOTAL = 0;
            
            try
            {
                double[] dBNFUNDTOTAL = new double[ds.Tables[0].Rows.Count];
                double[] dBNYEARRATE = new double[this.FPS91_TY_S_AC_8CBED299.CurrentRowCount - 1];

                if (Convert.ToInt16(this.TXT01_BSYEAR.GetValue().ToString()) < 2019)
                {
                    dBNFUNDTOTAL = UP_GetBNFUNDTOTAL(ds.Tables[0]);
                }
                else
                {
                    dBNFUNDTOTAL = UP_Get_SpreadBNFUNDTOTAL(this.FPS91_TY_S_AC_8CBED299);
                }

                //귀속별 총금액
                for (int i = 0; i < dBNFUNDTOTAL.Length; i++)
                {
                    dBNAMTTOTAL += dBNFUNDTOTAL[i];
                }

                if (Convert.ToInt16(this.TXT01_BSYEAR.GetValue().ToString()) < 2019)
                {
                    //신규등록
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_78AH1422", ds.Tables[0].Rows[i]["BNYEAR"].ToString(),
                                                                    ds.Tables[0].Rows[i]["BNDPAC"].ToString());
                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            this.DbConnector.CommandClear();

                            this.DbConnector.Attach("TY_P_AC_78AH9420", ds.Tables[0].Rows[i]["BNTAASETAMT"].ToString(),
                                                                        ds.Tables[0].Rows[i]["BNBLASETAMT"].ToString(),
                                                                        ds.Tables[0].Rows[i]["BNPROFITAMT"].ToString(),
                                                                        ds.Tables[0].Rows[i]["BNSALECREDAMT"].ToString(),
                                                                        ds.Tables[0].Rows[i]["BNBUYDEBTAMT"].ToString(),
                                                                        dBNFUNDTOTAL[i],
                                                                        ds.Tables[0].Rows[i]["BNYINTERAMT"].ToString(),
                                                                        ds.Tables[0].Rows[i]["BNMINTERAMT"].ToString(),
                                                                        ds.Tables[0].Rows[i]["BNYINTERRATE"].ToString(),
                                                                        TYUserInfo.EmpNo,
                                                                        ds.Tables[0].Rows[i]["BNYEAR"].ToString(),
                                                                        ds.Tables[0].Rows[i]["BNDPAC"].ToString()
                                                                        );
                            this.DbConnector.ExecuteTranQuery();
                        }
                        else
                        {
                            this.DbConnector.CommandClear();

                            this.DbConnector.Attach("TY_P_AC_78AH7419", ds.Tables[0].Rows[i]["BNYEAR"].ToString(),
                                                                        ds.Tables[0].Rows[i]["BNDPAC"].ToString(),
                                                                        ds.Tables[0].Rows[i]["BNTAASETAMT"].ToString(),
                                                                        ds.Tables[0].Rows[i]["BNBLASETAMT"].ToString(),
                                                                        ds.Tables[0].Rows[i]["BNPROFITAMT"].ToString(),
                                                                        ds.Tables[0].Rows[i]["BNSALECREDAMT"].ToString(),
                                                                        ds.Tables[0].Rows[i]["BNBUYDEBTAMT"].ToString(),
                                                                        dBNFUNDTOTAL[i],
                                                                        ds.Tables[0].Rows[i]["BNYINTERAMT"].ToString(),
                                                                        ds.Tables[0].Rows[i]["BNMINTERAMT"].ToString(),
                                                                        ds.Tables[0].Rows[i]["BNYINTERRATE"].ToString(),
                                                                        TYUserInfo.EmpNo
                                                                        );
                            this.DbConnector.ExecuteTranQuery();
                        }
                    }
                }
                else
                {
                    //2019년이후 년반영율 자동계산
                    for (int i = 0; i < this.FPS91_TY_S_AC_8CBED299.CurrentRowCount - 1; i++)
                    {
                        if (i == 0)
                        {
                            dBNYEARRATE[i] = Math.Round(dBNFUNDTOTAL[i] * 100 / dBNAMTTOTAL);
                        }
                        else
                        {
                            dBNYEARRATE[i] = 100 - dBNYEARRATE[0];
                        }

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_78AH1422", this.FPS91_TY_S_AC_8CBED299.GetValue(i, "BNYEAR").ToString(), 
                                                                    this.FPS91_TY_S_AC_8CBED299.GetValue(i, "BNDPAC").ToString());
                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            this.DbConnector.CommandClear();

                            this.DbConnector.Attach("TY_P_AC_78AH9420", this.FPS91_TY_S_AC_8CBED299.GetValue(i, "BNTAASETAMT").ToString(),
                                                                        this.FPS91_TY_S_AC_8CBED299.GetValue(i, "BNBLASETAMT").ToString(),
                                                                        this.FPS91_TY_S_AC_8CBED299.GetValue(i, "BNPROFITAMT").ToString(),
                                                                        this.FPS91_TY_S_AC_8CBED299.GetValue(i, "BNSALECREDAMT").ToString(),
                                                                        this.FPS91_TY_S_AC_8CBED299.GetValue(i, "BNBUYDEBTAMT").ToString(),
                                                                        dBNFUNDTOTAL[i],
                                                                        this.FPS91_TY_S_AC_8CBED299.GetValue(i, "BNYINTERAMT").ToString(),
                                                                        this.FPS91_TY_S_AC_8CBED299.GetValue(i, "BNMINTERAMT").ToString(),
                                                                        CKB01_RATE.Checked != true ? Convert.ToInt16(this.FPS91_TY_S_AC_8CBED299.GetValue(i, "BNYINTERRATE").ToString()) : dBNYEARRATE[i],
                                                                        TYUserInfo.EmpNo,
                                                                        this.FPS91_TY_S_AC_8CBED299.GetValue(i, "BNYEAR").ToString(),
                                                                        this.FPS91_TY_S_AC_8CBED299.GetValue(i, "BNDPAC").ToString()
                                                                        );
                            this.DbConnector.ExecuteTranQuery();
                        }
                        else
                        {
                            this.DbConnector.CommandClear();

                            this.DbConnector.Attach("TY_P_AC_78AH7419", this.FPS91_TY_S_AC_8CBED299.GetValue(i, "BNYEAR").ToString(),
                                                                        this.FPS91_TY_S_AC_8CBED299.GetValue(i, "BNDPAC").ToString(),
                                                                       this.FPS91_TY_S_AC_8CBED299.GetValue(i, "BNTAASETAMT").ToString(),
                                                                        this.FPS91_TY_S_AC_8CBED299.GetValue(i, "BNBLASETAMT").ToString(),
                                                                        this.FPS91_TY_S_AC_8CBED299.GetValue(i, "BNPROFITAMT").ToString(),
                                                                        this.FPS91_TY_S_AC_8CBED299.GetValue(i, "BNSALECREDAMT").ToString(),
                                                                        this.FPS91_TY_S_AC_8CBED299.GetValue(i, "BNBUYDEBTAMT").ToString(),
                                                                        dBNFUNDTOTAL[i],
                                                                        this.FPS91_TY_S_AC_8CBED299.GetValue(i, "BNYINTERAMT").ToString(),
                                                                        this.FPS91_TY_S_AC_8CBED299.GetValue(i, "BNMINTERAMT").ToString(),
                                                                        CKB01_RATE.Checked != true ? Convert.ToInt16(this.FPS91_TY_S_AC_8CBED299.GetValue(i, "BNYINTERRATE").ToString()) : dBNYEARRATE[i],
                                                                        TYUserInfo.EmpNo
                                                                        );
                            this.DbConnector.ExecuteTranQuery();
                        }
                    }

                    //투하자금이자관리 삭제후 등록
                    if( this.FPS91_TY_S_AC_8CBED299.CurrentRowCount > 0 )
                    {
                       this.DbConnector.CommandClear();
                       this.DbConnector.Attach("TY_P_AC_8CCAQ305", this.FPS91_TY_S_AC_8CBED299.GetValue(0, "BNYEAR").ToString()
                                                                );
                       this.DbConnector.Attach("TY_P_AC_8CCAP304", this.FPS91_TY_S_AC_8CBED299.GetValue(0, "BNYEAR").ToString(),
                                                                   "52000100",
                                                                   TYUserInfo.EmpNo
                                                                );
                       this.DbConnector.ExecuteTranQueryList();
                    }

                }

                BTN61_INQ_Click(null, null);

                this.ShowMessage("TY_M_GB_23NAD873");
            }
            catch
            {
                this.ShowMessage("TY_M_AC_246A2488");
            }
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            if (Convert.ToInt16(this.TXT01_BSYEAR.GetValue().ToString()) < 2019)
            {
                ds.Tables.Add(this.FPS91_TY_S_AC_78AHA424.GetDataSourceInclude(TSpread.TActionType.Update, "BNYEAR", "BNDPAC", "BNTAASETAMT", "BNBLASETAMT", "BNPROFITAMT", "BNSALECREDAMT", "BNBUYDEBTAMT",
                                                                                                           "BNFUNDTOTAL", "BNYINTERAMT", "BNMINTERAMT","BNYINTERRATE"));
            }
            else
            {
                ds.Tables.Add(this.FPS91_TY_S_AC_8CBED299.GetDataSourceInclude(TSpread.TActionType.Update, "BNYEAR", "BNDPAC", "BNTAASETAMT", "BNBLASETAMT", "BNPROFITAMT", "BNSALECREDAMT", "BNBUYDEBTAMT",
                                                                                                           "BNFUNDTOTAL", "BNYINTERAMT", "BNMINTERAMT", "BNYINTERRATE" ));
             
                Int16 iRateTotal = 0;

                if (this.CKB01_RATE.Checked != true)  //수동계산일 경우 체크
                {
                    // 투하자금계 기준으로 년반영율 계산
                    if (this.FPS91_TY_S_AC_8CBED299.CurrentRowCount > 0)
                    {
                        for (int i = 0; i < this.FPS91_TY_S_AC_8CBED299.CurrentRowCount - 1; i++)
                        {
                            if (i > 0 && this.FPS91_TY_S_AC_8CBED299.GetValue(i, "BNDPAC").ToString() != "")
                            {
                                this.FPS91_TY_S_AC_8CBED299.SetValue(i, "BNYINTERRATE", (100 - iRateTotal).ToString());
                            }

                            iRateTotal += Convert.ToInt16(this.FPS91_TY_S_AC_8CBED299.GetValue(i, "BNYINTERRATE").ToString());                            
                        }
                    }

                    if ((iRateTotal > 100 || iRateTotal < 100) && (iRateTotal > 0))
                    {
                        this.ShowCustomMessage("전체 비율합은 100% 이여야 합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }            

            //if (ds.Tables[0].Rows.Count == 0)
            //{
            //    this.ShowMessage("TY_M_GB_2452W459");
            //    e.Successed = false;
            //    return;
            //}

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
            e.ArgData = ds;
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

        #region Description : TabList_TabIndexChanged 이벤트
        private void TabList_TabIndexChanged(object sender, EventArgs e)
        {
            if (TabList.TabIndex == 0)
            {
                this.BTN61_SAV.Visible = true;
                this.BTN62_SAV.Visible = false;
                this.BTN61_REM.Visible = true;
                CKB01_RATE.Visible = true;
            }
            else
            {
                this.BTN62_SAV.Visible = true;
                this.BTN61_SAV.Visible = false;
                this.BTN61_REM.Visible = false;
                CKB01_RATE.Visible = false;
            }
        }
        #endregion

        #region Description : TabList_SelectedIndexChanged 이벤트
        private void TabList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TabList.SelectedIndex == 0)
            {
                this.BTN61_SAV.Visible = true;
                this.BTN62_SAV.Visible = false;
                this.BTN61_REM.Visible = true;
                CKB01_RATE.Visible = true;
            }
            else
            {
                this.BTN62_SAV.Visible = true;
                this.BTN61_SAV.Visible = false;
                this.BTN61_REM.Visible = false;
                CKB01_RATE.Visible = false;
            }
        }
        #endregion

        #region Description : 저장 체크
        private void BTN62_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;            

            try
            {
                    this.DbConnector.CommandClear();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                       
                            
                            this.DbConnector.Attach("TY_P_AC_8CE9E311", ds.Tables[0].Rows[i]["BNSMONAMT01"].ToString(),
                                                                        ds.Tables[0].Rows[i]["BNSMONAMT02"].ToString(),
                                                                        ds.Tables[0].Rows[i]["BNSMONAMT03"].ToString(),
                                                                        ds.Tables[0].Rows[i]["BNSMONAMT04"].ToString(),
                                                                        ds.Tables[0].Rows[i]["BNSMONAMT05"].ToString(),
                                                                        ds.Tables[0].Rows[i]["BNSMONAMT06"].ToString(),
                                                                        ds.Tables[0].Rows[i]["BNSMONAMT07"].ToString(),
                                                                        ds.Tables[0].Rows[i]["BNSMONAMT08"].ToString(),
                                                                        ds.Tables[0].Rows[i]["BNSMONAMT09"].ToString(),
                                                                        ds.Tables[0].Rows[i]["BNSMONAMT10"].ToString(),
                                                                        ds.Tables[0].Rows[i]["BNSMONAMT11"].ToString(),
                                                                        ds.Tables[0].Rows[i]["BNSMONAMT12"].ToString(),
                                                                        Convert.ToDouble(ds.Tables[0].Rows[i]["BNSMONAMT01"].ToString()) +
                                                                        Convert.ToDouble(ds.Tables[0].Rows[i]["BNSMONAMT02"].ToString()) +
                                                                        Convert.ToDouble(ds.Tables[0].Rows[i]["BNSMONAMT03"].ToString()) +
                                                                        Convert.ToDouble(ds.Tables[0].Rows[i]["BNSMONAMT04"].ToString()) +
                                                                        Convert.ToDouble(ds.Tables[0].Rows[i]["BNSMONAMT05"].ToString()) +
                                                                        Convert.ToDouble(ds.Tables[0].Rows[i]["BNSMONAMT06"].ToString()) +
                                                                        Convert.ToDouble(ds.Tables[0].Rows[i]["BNSMONAMT07"].ToString()) +
                                                                        Convert.ToDouble(ds.Tables[0].Rows[i]["BNSMONAMT08"].ToString()) +
                                                                        Convert.ToDouble(ds.Tables[0].Rows[i]["BNSMONAMT09"].ToString()) +
                                                                        Convert.ToDouble(ds.Tables[0].Rows[i]["BNSMONAMT10"].ToString()) +
                                                                        Convert.ToDouble(ds.Tables[0].Rows[i]["BNSMONAMT11"].ToString()) +
                                                                        Convert.ToDouble(ds.Tables[0].Rows[i]["BNSMONAMT12"].ToString()), 
                                                                        TYUserInfo.EmpNo,
                                                                        ds.Tables[0].Rows[i]["BNSYEAR"].ToString(),
                                                                        ds.Tables[0].Rows[i]["BNSDPAC"].ToString()
                                                                        );                            
                        
                    }

                    if (this.DbConnector.CommandCount > 0)
                        this.DbConnector.ExecuteTranQueryList();
                                
                BTN61_INQ_Click(null, null);

                this.ShowMessage("TY_M_GB_23NAD873");
            }
            catch
            {
                this.ShowMessage("TY_M_AC_246A2488");
            }
        }
        private void BTN62_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_8CCB5307.GetDataSourceInclude(TSpread.TActionType.Update, "BNSYEAR", "BNSDPAC", "BNSMONAMT01", "BNSMONAMT02", "BNSMONAMT03", "BNSMONAMT04", "BNSMONAMT05",
                                                                                                       "BNSMONAMT06", "BNSMONAMT07", "BNSMONAMT08", "BNSMONAMT09","BNSMONAMT10","BNSMONAMT11","BNSMONAMT12"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            //수정한 금액이 자금관리에 이자비용의 월 합계와 맞는지 체크
            if (this.FPS91_TY_S_AC_8CCB5307.CurrentRowCount > 0)
            {
                double[] dTBNSMONAMT = new double[12];
                double[] dSBNSMONAMT = new double[12];

                for (int i = 0; i < this.FPS91_TY_S_AC_8CCB5307.CurrentRowCount - 1; i++)
                {
                    if (this.FPS91_TY_S_AC_8CCB5307.GetValue(i, "BNSDPAC").ToString() != "")
                    {
                        if (this.FPS91_TY_S_AC_8CCB5307.GetValue(i, "BNSDPAC").ToString() == "T")
                        {
                            for (int j = 3; j < 15; j++)
                            {
                                dTBNSMONAMT[j - 3] = Convert.ToDouble(this.FPS91_TY_S_AC_8CCB5307.ActiveSheet.Cells[i, j].Value);
                            }
                        }
                        else
                        {
                            for (int j = 3; j < 15; j++)
                            {
                                dSBNSMONAMT[j - 3] = Convert.ToDouble(this.FPS91_TY_S_AC_8CCB5307.ActiveSheet.Cells[i, j].Value);
                            }
                        }
                    }                    
                }

                //자금관리 이자 비용 조회
                string sMonth = string.Empty;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_789DC396", this.TXT01_BSYEAR.GetValue().ToString(), "52000100");
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    sMonth = "";
                    for (int j = 3; j < 15; j++)
                    {
                        if (Convert.ToDouble(dt.Rows[0][j].ToString()) != (dTBNSMONAMT[j-3] + dSBNSMONAMT[j-3]))
                        {
                            switch (j)
                            {
                                case 3:
                                    sMonth = "1월";
                                    break;
                                case 4:
                                    sMonth = "2월";
                                    break;
                                case 5:
                                    sMonth = "3월";
                                    break;
                                case 6:
                                    sMonth = "4월";
                                    break;
                                case 7:
                                    sMonth = "5월";
                                    break;
                                case 8:
                                    sMonth = "6월";
                                    break;
                                case 9:
                                    sMonth = "7월";
                                    break;
                                case 10:
                                    sMonth = "8월";
                                    break;
                                case 11:
                                    sMonth = "9월";
                                    break;
                                case 12:
                                    sMonth = "10월";
                                    break;
                                case 13:
                                    sMonth = "11월";
                                    break;
                                case 14:
                                    sMonth = "12월";
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    if (sMonth != "")
                    {
                        this.ShowCustomMessage(sMonth + " 합계금액이 자금관리 이자비용과 다릅니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
            e.ArgData = ds;
        }
        #endregion
       
    }
}
