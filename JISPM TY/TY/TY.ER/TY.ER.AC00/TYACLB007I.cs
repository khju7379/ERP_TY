using System;
using System.Data;
using System.Drawing; 
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 본예산관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.04.24 09:19
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_24C2E601 : 본예산 삭제
    ///  TY_P_AC_24C9K597 : 본예산 등록
    ///  TY_P_AC_24C9L598 : 본예산 수정
    ///  TY_P_AC_24H24712 : 본예산 조회
    ///  TY_P_AC_24OBT818 : 본예산 분기별 집계 조회
    ///  TY_P_AC_24OBW819 : 본예산 월별 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_24OBY821 : 본예산 조회
    ///  TY_S_AC_24P5E852 : 본예산 월별 조회
    ///  TY_S_AC_24P5P853 : 본예산 분기집계 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  MMCDAC : 계정코드
    ///  MMCDDP : 예산부서
    ///  MMYEAR : 예산년도
    /// </summary>
    public partial class TYACLB007I : TYBase
    {

        #region Description : 폼 로드
        public TYACLB007I()
        {
            InitializeComponent();
        }

        private void TYACLB007I_Load(object sender, System.EventArgs e)
        {

            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.BTN62_SAV.ProcessCheck += new TButton.CheckHandler(BTN62_SAV_ProcessCheck);

            this.TXT01_MMYEAR_TextChanged(null, null);
            this.TXT02_MMYEAR_TextChanged(null, null); 

            UP_SetReadOnly(true);

            this.SetStartingFocus(TXT01_MMYEAR);
        }
        #endregion

        #region Description : 조회  이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_24H24712", this.ControlFactory, "01");
            this.FPS91_TY_S_AC_24OBY821.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            string sValue = "";
            string sCDAC = "";

            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 1; j < 13; j++)
                {
                    //본예산 삭제
                    this.DbConnector.Attach("TY_P_AC_24C2E601", dt.Rows[i]["MMYEAR"].ToString(),
                                                                j.ToString("00"),
                                                                dt.Rows[i]["MMCDDP"].ToString(),
                                                                dt.Rows[i]["MMCDAC"].ToString()
                                                                );
                }

                sValue = UP_EnterBudGet_Check(dt.Rows[i]["MMCDAC"].ToString());

                if (sValue != "00")
                {
                    if (sValue == "01" || sValue == "02")  // 접대비 체크
                    {
                        sCDAC = CBH02_MMCDAC.GetValue().ToString().Substring(0, 6) + "00";
                    }
                    if (sValue == "03" || sValue == "04")  // 운영비 및 분임 토의비
                    {
                        sCDAC = CBH02_MMCDAC.GetValue().ToString();
                    }

                    //접대비 년파일, 월파일 삭제
                    this.DbConnector.Attach("TY_P_AC_244AF393", dt.Rows[i]["MMYEAR"].ToString(), dt.Rows[i]["MMCDDP"].ToString(), "", sCDAC);
                    for (int j = 1; j < 13; j++)
                    {
                        this.DbConnector.Attach("TY_P_AC_243AR311", dt.Rows[i]["MMYEAR"].ToString(),
                                                                    j.ToString("00"),
                                                                    dt.Rows[i]["MMCDDP"].ToString(),
                                                                    "",
                                                                    sCDAC);
                    }

                    //접대비 년파일, 월파일 등록
                    if (sValue == "01" || sValue == "02")  // 접대비 체크
                    {
                        this.DbConnector.Attach("TY_P_AC_24Q7S868", TYUserInfo.UserID, dt.Rows[i]["MMYEAR"].ToString(), dt.Rows[i]["MMCDDP"].ToString(), sCDAC.Substring(0,6) );
                        this.DbConnector.Attach("TY_P_AC_24Q7Z869", TYUserInfo.UserID, dt.Rows[i]["MMYEAR"].ToString(), dt.Rows[i]["MMCDDP"].ToString(), sCDAC.Substring(0, 6));
                    }
                    if (sValue == "03" || sValue == "04")  // 운영비 및 분임 토의비
                    {
                        this.DbConnector.Attach("TY_P_AC_24R95872", TYUserInfo.UserID, dt.Rows[i]["MMYEAR"].ToString(), dt.Rows[i]["MMCDDP"].ToString(), sCDAC);
                        this.DbConnector.Attach("TY_P_AC_24R98873", TYUserInfo.UserID, dt.Rows[i]["MMYEAR"].ToString(), dt.Rows[i]["MMCDDP"].ToString(), sCDAC);
                    }
                }
            }          

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");
            this.BTN61_INQ_Click(null, null);

            this.Initialize_Controls("02");
            FPS91_TY_S_AC_24P5E852.Initialize();
            FPS91_TY_S_AC_24P5P853.Initialize(); 
        }
        #endregion

        #region Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            UP_SetReadOnly(false);
            this.Initialize_Controls("02");

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_24OBW819", this.TXT02_MMYEAR.GetValue(), this.CBH02_MMCDDP.GetValue(), this.CBH02_MMCDAC.GetValue());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.CurrentDataTableRowMapping(dt, "02");
                this.FPS91_TY_S_AC_24P5E852.SetValue(UP_NewRowAdd(dt));

                // 강제 세팅 (신규 월 강제 셋팅)
                for (int i = 0; i < this.FPS91_TY_S_AC_24P5E852.CurrentRowCount ; i++)
                {
                    this.FPS91_TY_S_AC_24P5E852.ActiveSheet.RowHeader.Cells[i, 0].Text = "N";
                }
            }

            UP_SumRowAdd();

            TXT02_MMYEAR.Focus();  
        }
        #endregion

        #region Description : TXT01_MMYEAR_TextChanged 이벤트
        private void TXT01_MMYEAR_TextChanged(object sender, EventArgs e)
        {
            if (TXT01_MMYEAR.GetValue().ToString() != "")
            {
                this.CBH01_MMCDDP.DummyValue = TXT01_MMYEAR.GetValue() + "0101";
            }
            else
            {
                this.CBH01_MMCDDP.DummyValue = DateTime.Now.ToString("yyyyMMdd");
            }
        }
        #endregion

        #region Description : TXT02_MMYEAR_TextChanged 이벤트
        private void TXT02_MMYEAR_TextChanged(object sender, EventArgs e)
        {
            if (TXT02_MMYEAR.GetValue().ToString() != "")
            {
                this.CBH02_MMCDDP.DummyValue = TXT02_MMYEAR.GetValue() + "0101";
            }
            else
            {
                this.CBH02_MMCDDP.DummyValue = DateTime.Now.ToString("yyyyMMdd");
            }
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN62_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_23N3M888", this.CBH02_MMCDAC.GetValue(),"");
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["A1TAG02"].ToString().Trim() != "Y")
                {
                    this.ShowMessage("TY_M_AC_24RBZ877");
                    e.Successed = false;
                    return;
                }
            }
            
            //	계정코드 체크
            int nAcCode = int.Parse(this.CBH02_MMCDAC.GetValue().ToString());

            if (
                //---- 자산투자 ---------//
                (nAcCode >= 12200100 & nAcCode <= 12200900) ||
                //---- 기부금   ---------//
                (nAcCode >= 52001501 & nAcCode <= 52001502) ||
                //---- 수선비   ---------//
                (nAcCode >= 42411801 & nAcCode <= 42411888) ||
                (nAcCode >= 44121801 & nAcCode <= 44121888) ||
                (nAcCode >= 44130000 & nAcCode <= 44130000) ||
                (nAcCode >= 44111801 & nAcCode <= 44111888) ||
                (nAcCode >= 44211801 & nAcCode <= 44211888) ||
                //----  교육비  ----//
                (nAcCode == 42412901) ||
                (nAcCode == 44122901) ||
                (nAcCode == 44112901) ||
                (nAcCode == 44212901) ||
                //----  국내 출장  ----//
                //(nAcCode == 42411201) ||
                //(nAcCode == 44121201) ||
                //(nAcCode == 44111201) ||
                //(nAcCode == 44211201) ||
                //----  국외 출장  ----//
                //(nAcCode == 42411202) ||
                //(nAcCode == 44121202) ||
                //(nAcCode == 44111202) ||
                //(nAcCode == 44211202) ||
                //----  조합비,회비  ----//
                (nAcCode == 42411503) ||
                (nAcCode == 44121503) ||
                (nAcCode == 44111503) ||
                (nAcCode == 44211503) ||
                //----  외부 수수료  ----//
                (nAcCode == 42412803) ||
                (nAcCode == 44122803) ||
                (nAcCode == 44112803) ||
                (nAcCode == 44212803) ||
                //----  소모성 비품  ----//
                (nAcCode == 42413301) ||
                (nAcCode == 44123301) ||
                (nAcCode == 44113301) ||
                (nAcCode == 44213301) ||
                //----  기타 소모품  ----//
                //(nAcCode == 42413388) ||
                //(nAcCode == 44123388) ||
                //(nAcCode == 44113388) ||
                //(nAcCode == 44213388) ||
                //----  전산 판매비  ----//
                (nAcCode == 44130000)   
                 )
            {
                this.ShowMessage("TY_M_AC_24RBV876");
                e.Successed = false;
                return;               
            }


            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_AC_24P5E852.GetDataSourceInclude(TSpread.TActionType.Update, "MMMONTH", "MMCRAMT", "MMPLAMT", "MMFLAG")); // 수정
            ds.Tables.Add(this.FPS91_TY_S_AC_24P5E852.GetDataSourceInclude(TSpread.TActionType.New, "MMMONTH", "MMCRAMT", "MMPLAMT", "MMFLAG"));   // 신규

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
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

            e.ArgData = ds;
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN62_SAV_Click(object sender, EventArgs e)
        {
            string sValue = "";
            string sCDAC = "";

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            // 수정
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ////본예산 등록
                //this.DbConnector.Attach("TY_P_AC_24C9K597", TXT02_MMYEAR.GetValue(),
                //                                               ds.Tables[0].Rows[i]["MMMONTH"].ToString(),
                //                                               CBH02_MMCDDP.GetValue(),
                //                                               CBH02_MMCDAC.GetValue(),
                //                                               Get_Numeric(ds.Tables[0].Rows[i]["MMCRAMT"].ToString()),
                //                                               Get_Numeric(ds.Tables[0].Rows[i]["MMPLAMT"].ToString()),
                //                                               "0", "0", "I", TYUserInfo.UserID,
                //                                               TXT02_MMYEAR.GetValue(),
                //                                               ds.Tables[0].Rows[i]["MMMONTH"].ToString(),
                //                                               CBH02_MMCDDP.GetValue(),
                //                                               CBH02_MMCDAC.GetValue());
                //본예산 수정
                this.DbConnector.Attach("TY_P_AC_24C9L598", Get_Numeric(ds.Tables[0].Rows[i]["MMCRAMT"].ToString()),
                                                            Get_Numeric(ds.Tables[0].Rows[i]["MMPLAMT"].ToString()),
                                                            "U",
                                                            "C",
                                                            TYUserInfo.UserID,
                                                            TXT02_MMYEAR.GetValue(),
                                                            ds.Tables[0].Rows[i]["MMMONTH"].ToString(),
                                                            CBH02_MMCDDP.GetValue(),
                                                            CBH02_MMCDAC.GetValue());

            }

            // 신규등록 
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                //본예산 등록
                this.DbConnector.Attach("TY_P_AC_24C9K597", TXT02_MMYEAR.GetValue(),
                                                               ds.Tables[1].Rows[i]["MMMONTH"].ToString(),
                                                               CBH02_MMCDDP.GetValue(),
                                                               CBH02_MMCDAC.GetValue(),
                                                               Get_Numeric(ds.Tables[1].Rows[i]["MMCRAMT"].ToString()),
                                                               Get_Numeric(ds.Tables[1].Rows[i]["MMPLAMT"].ToString()),
                                                               "0", "0", "I", TYUserInfo.UserID,
                                                               TXT02_MMYEAR.GetValue(),
                                                               ds.Tables[1].Rows[i]["MMMONTH"].ToString(),
                                                               CBH02_MMCDDP.GetValue(),
                                                               CBH02_MMCDAC.GetValue());
            }

            sValue = UP_EnterBudGet_Check(CBH02_MMCDAC.GetValue().ToString());

            if (sValue != "00")
            {
                if (sValue == "01" || sValue == "02")  // 접대비 체크
                {
                    sCDAC = CBH02_MMCDAC.GetValue().ToString().Substring(0, 6) + "00";
                }
                if (sValue == "03" || sValue == "04")  // 운영비 및 분임 토의비
                {
                    sCDAC = CBH02_MMCDAC.GetValue().ToString();
                }

                ////접대비 년파일, 월파일 삭제
                //this.DbConnector.Attach("TY_P_AC_244AF393", TXT02_MMYEAR.GetValue(), CBH02_MMCDDP.GetValue(), "", sCDAC);
                //for (int j = 1; j < 13; j++)
                //{
                //    this.DbConnector.Attach("TY_P_AC_243AR311", TXT02_MMYEAR.GetValue(),
                //                                                j.ToString("00"),
                //                                                CBH02_MMCDDP.GetValue(),
                //                                                "",
                //                                                sCDAC);
                //}

                ////접대비 년파일, 월파일 등록
                //if (sValue == "01" || sValue == "02")  // 접대비 체크
                //{
                //    this.DbConnector.Attach("TY_P_AC_24Q7S868", TYUserInfo.UserID, TXT02_MMYEAR.GetValue(), CBH02_MMCDDP.GetValue(), sCDAC.Substring(0, 6));
                //    this.DbConnector.Attach("TY_P_AC_24Q7Z869", TYUserInfo.UserID, TXT02_MMYEAR.GetValue(), CBH02_MMCDDP.GetValue(), sCDAC.Substring(0, 6));
                //}
                //if (sValue == "03" || sValue == "04")  // 운영비 및 분임 토의비
                //{
                //    this.DbConnector.Attach("TY_P_AC_24R95872", TYUserInfo.UserID, TXT02_MMYEAR.GetValue(), CBH02_MMCDDP.GetValue(), sCDAC);
                //    this.DbConnector.Attach("TY_P_AC_24R98873", TYUserInfo.UserID, TXT02_MMYEAR.GetValue(), CBH02_MMCDDP.GetValue(), sCDAC);
                //}

            }

            this.DbConnector.ExecuteTranQueryList();
            this.ShowMessage("TY_M_GB_23NAD873");
            this.BTN61_INQ_Click(null, null);

            //this.FPS91_TY_S_AC_24OBY821_CellDoubleClick(null, null);

            UP_SetReadOnly(true);
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            double dMMUSAMT = 0;

            DataTable dt = this.FPS91_TY_S_AC_24OBY821.GetDataSourceInclude(TSpread.TActionType.Remove, "MMYEAR", "MMCDDP", "MMCDAC");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            //집행금액 체크
            //집행금액이 있으면 삭제안됨
            this.DbConnector.CommandClear();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                this.DbConnector.Attach("TY_P_AC_24H24712", dt.Rows[j].ItemArray);
            }
            DataSet dsChk = this.DbConnector.ExecuteDataSet();

            for (int i = 0; i < dsChk.Tables.Count; i++)
            {
                dMMUSAMT = Convert.ToDouble(Get_Numeric(dsChk.Tables[i].Rows[0]["MMUSAMT"].ToString()));
                if (dMMUSAMT > 0)
                {
                    this.ShowMessage("TY_M_AC_24C3F612");
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }


            e.ArgData = dt;
        }
        #endregion

        #region Description : FPS91_TY_S_AC_24OBY821_CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_24OBY821_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //분기집계 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_24OBT818", this.FPS91_TY_S_AC_24OBY821.GetValue("MMYEAR").ToString(),
                                                        this.FPS91_TY_S_AC_24OBY821.GetValue("MMCDDP").ToString(),
                                                        this.FPS91_TY_S_AC_24OBY821.GetValue("MMCDAC").ToString() );
            this.FPS91_TY_S_AC_24P5P853.SetValue(this.DbConnector.ExecuteDataTable());

            //월별 조회
            this.TXT02_MMYEAR.SetValue(this.FPS91_TY_S_AC_24OBY821.GetValue("MMYEAR").ToString());
            this.CBH02_MMCDDP.DummyValue = TXT02_MMYEAR.GetValue() + "0101";
            this.CBH02_MMCDDP.SetValue(this.FPS91_TY_S_AC_24OBY821.GetValue("MMCDDP").ToString());
            this.CBH02_MMCDAC.SetValue(this.FPS91_TY_S_AC_24OBY821.GetValue("MMCDAC").ToString());            

            UP_SetReadOnly(true);
            UP_SetGridMaster();
        }
        #endregion

        #region Description : 사용자 정의 함수
        private void UP_SetReadOnly(bool bTrueAndFalse)
        {
            this.TXT02_MMYEAR.SetReadOnly(bTrueAndFalse);
            this.CBH02_MMCDDP.SetReadOnly(bTrueAndFalse);
            this.CBH02_MMCDAC.SetReadOnly(bTrueAndFalse);
        }

        private void UP_SetGridMaster()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_24OBW819", this.TXT02_MMYEAR.GetValue(), this.CBH02_MMCDDP.GetValue(), this.CBH02_MMCDAC.GetValue());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "02");
                if (dt.Rows.Count > 0)
                {
                    this.FPS91_TY_S_AC_24P5E852.SetValue(UP_MonRowAdd(dt));

                    // 강제 세팅 (존재하지 않은 월 강제 셋팅)
                    for (int i = 0; i < this.FPS91_TY_S_AC_24P5E852.CurrentRowCount ; i++)
                    {
                        if (this.FPS91_TY_S_AC_24P5E852.GetValue(i, "MMFLAG").ToString() == "N")
                        {
                            this.FPS91_TY_S_AC_24P5E852.ActiveSheet.RowHeader.Cells[i, 0].Text = "N";
                        }
                    }

                    for (int i = 0; i < this.FPS91_TY_S_AC_24P5E852.CurrentRowCount ; i++)
                    {
                        if (this.FPS91_TY_S_AC_24P5E852.GetValue(i, "MMFLAG").ToString() == "N")
                        {
                            this.FPS91_TY_S_AC_24P5E852.SetValue(i, "MMFLAG", "");
                        }
                    }

                }
                else
                {
                    this.FPS91_TY_S_AC_24P5E852.SetValue(UP_NewRowAdd(dt));

                    // 강제 세팅 (신규 월 강제 셋팅)
                    for (int i = 0; i < this.FPS91_TY_S_AC_24P5E852.CurrentRowCount; i++)
                    {
                        this.FPS91_TY_S_AC_24P5E852.ActiveSheet.RowHeader.Cells[i, 0].Text = "N";
                    }
                }

                UP_SumRowAdd();
            }
        }

        private void UP_SumRowAdd()
        {
            this.SpreadSumRowAdd(this.FPS91_TY_S_AC_24P5E852, "MMMONTH", "합 계", Color.Yellow);
            this.FPS91_TY_S_AC_24P5E852_Sheet1.SetFormula(
                FPS91_TY_S_AC_24P5E852_Sheet1.RowCount - 1,
                FPS91_TY_S_AC_24P5E852_Sheet1.ColumnCount - 2,
                "R[0]C[-3] + R[0]C[-2] - R[0]C[-1]"); //잔액 구하기        
        }

        private DataTable UP_NewRowAdd(DataTable dt)
        {
            DataTable Rowdt = new DataTable();
            DataRow rw;
            Rowdt = dt.Clone();
            for (int i = 1; i < 13; i++)
            {
                rw = Rowdt.NewRow();
                rw["MMMONTH"] = i.ToString("00");
                //rw["MMCRAMT"] = DBNull.Value;
                rw["MMCRAMT"] = 0;
                rw["MMPLAMT"] = 0;
                rw["MMUSAMT"] = 0;
                rw["MMJAMT"] = 0;
                rw["MMFLAG"] = "A";
                Rowdt.Rows.Add(rw);
            }
            return Rowdt;
        }       

        private DataTable UP_MonRowAdd(DataTable dt)
        {
            string sCHKGB = string.Empty;

            DataTable Rowdt = new DataTable();
            DataRow rw;

            Rowdt = dt.Clone();

            for (int i = 1; i < 13; i++)
            {
                rw = Rowdt.NewRow();
                rw["MMYEAR"] = dt.Rows[0]["MMYEAR"].ToString();
                rw["MMMONTH"] = i.ToString("00");
                rw["MMCDDP"] = dt.Rows[0]["MMCDDP"].ToString();
                rw["MMCDDPNM"] = dt.Rows[0]["MMCDDPNM"].ToString();
                rw["MMCDAC"] = dt.Rows[0]["MMCDAC"].ToString();
                rw["MMCDACNM"] = dt.Rows[0]["MMCDACNM"].ToString();
                rw["MMCRAMT"] = Get_Numeric(dt.Compute("Sum(MMCRAMT)", "MMMONTH = " + i.ToString("00")).ToString());                
                rw["MMPLAMT"] = Get_Numeric(dt.Compute("Sum(MMPLAMT)", "MMMONTH = " + i.ToString("00")).ToString());
                rw["MMUSAMT"] = Get_Numeric(dt.Compute("Sum(MMUSAMT)", "MMMONTH = " + i.ToString("00")).ToString());
                rw["MMJAMT"] = Convert.ToDouble(Get_Numeric(dt.Compute("Sum(MMCRAMT)", "MMMONTH <= " + i.ToString("00")).ToString())) +
                                 Convert.ToDouble(Get_Numeric(dt.Compute("Sum(MMPLAMT)", "MMMONTH <= " + i.ToString("00")).ToString())) -
                                 Convert.ToDouble(Get_Numeric(dt.Compute("Sum(MMUSAMT)", "MMMONTH <= " + i.ToString("00")).ToString()));


                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j]["MMMONTH"].ToString() == i.ToString("00"))
                    {
                        sCHKGB = "Y";
                    }
                }

                if (sCHKGB == "Y")
                {
                    rw["MMFLAG"] = dt.Rows[0]["MMFLAG"].ToString();
                }
                else
                {
                    rw["MMFLAG"] = "N";
                }

                sCHKGB = "";
                       
               
                Rowdt.Rows.Add(rw);
            }

            return Rowdt;
        }

        private string UP_EnterBudGet_Check(string sCDAC)
        {
            string sValue = "00";
            //접대비 체크
            switch (sCDAC.Substring(0, 6))
            {
                //case "461310":
                //case "433310":
                //case "700310":
                //case "471310":
                //case "451310":
                //case "434310":
                //case "436310":
                case "442120":
                case "424120":
                case "441120":
                case "441220":
                //case "441300":
                    return "01";  // 접대비
                //case "461311":
                //case "433311":
                //case "700311":
                //case "471311":
                //case "451311":
                //case "434311":
                //case "436311":
                case "442121":
                case "424121":
                case "441121":
                case "441221":
                //case "441300":
                    return "02";  // 판매촉진비
                default:
                    sValue = "00";
                    break;
            }
            // 운영비 및 분임 토의비
            switch (sCDAC)
            {
                //case "46130115":
                //case "43330115":
                //case "70030115":
                //case "47130115":
                //case "45130115":
                //case "43430115":
                //case "43630115":
                case "42411110":
                case "44121110":
                case "44111110":
                case "44211110":	
                    return "03";  // 회식대
                //case "46131805":
                //case "43331805":
                //case "70031805":
                //case "47131805":
                //case "45131805":
                //case "43431805":
                //case "43631805":
                case "42412903":
                case "44122903":
                case "44112903":
                case "44212903":	
                    return "04";  // 안전회의비
                default:
                    sValue = "00";
                    break;
            }

            return sValue;
        }
        #endregion

        
    }
}
