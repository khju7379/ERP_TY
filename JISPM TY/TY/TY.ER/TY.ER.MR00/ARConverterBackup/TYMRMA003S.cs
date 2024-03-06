using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;
using DataDynamics.ActiveReports;
using TY.ER.GB00;
using TY.ER.AC00;

namespace TY.ER.MR00
{
    /// <summary>
    /// 월마감 전표발행 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2013.01.09 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_3192S565 : 월마감 전표 발행 - 조회(발행)
    ///  TY_P_MR_31941575 : 월마감 전표 발행 - 조회(미발행)
    ///  
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_31AA1589 : 월마감 전표 발행 - 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_MR_3174H522 : 전표자료가 존재합니다. 삭제 후 작업하세요.
    ///  TY_M_MR_31932568 : 해당 거래업체의 지불 조건을 확인하세요.
    ///  TY_M_MR_31933569 : 전표번호가 존재 하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  JUNPYO_CANCEL : 전표취소
    ///  JUNPYO_OK : 전표발행
    ///  MMT1000 : 거래처
    ///  GPRTGN : 출력구분
    ///  MMT2000 : 사업부
    ///  MMT1020 : 마감일자
    /// </summary>
    public partial class TYMRMA003S : TYBase
    {
        #region Description : 페이지 로드
        public TYMRMA003S()
        {
            InitializeComponent();
        }

        private void TYMRMA003S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_JUNPYO_OK.ProcessCheck += new TButton.CheckHandler(BTN61_JUNPYO_OK_ProcessCheck);
            this.BTN61_JUNPYO_CANCEL.ProcessCheck += new TButton.CheckHandler(BTN61_JUNPYO_CANCEL_ProcessCheck);

            this.BTN61_JUNPYO_OK.Visible = true;
            this.BTN61_JUNPYO_CANCEL.Visible = false;

            DTP01_STDATE.SetValue(DateTime.Now.ToString("yyyyMM") + "01");
            DTP01_EDDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));

            SetStartingFocus(this.CBO01_MMT2000);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sProcedure = string.Empty;
            string sKBBUSEO   = string.Empty;

            if (this.CBO01_GPRTGN.GetValue().ToString() == "1") // 발행
            {
                this.BTN61_JUNPYO_OK.Visible = false;
                this.BTN61_JUNPYO_CANCEL.Visible = true;

                sProcedure = "TY_P_MR_31AA1586";
            }
            else // 미발행
            {
                this.BTN61_JUNPYO_OK.Visible = true;
                this.BTN61_JUNPYO_CANCEL.Visible = false;

                sProcedure = "TY_P_MR_31AA8587";
            }


            DataTable dt = new DataTable();

            // 부서코드 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BEBB293",
                DateTime.Now.ToString("yyyyMMdd"),
                TYUserInfo.EmpNo.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                // 부서코드
                sKBBUSEO = dt.Rows[0]["KBBUSEO"].ToString();
            }

            if (TYUserInfo.EmpNo.ToString() == "0150-M" ||
                TYUserInfo.EmpNo.ToString() == "0287-M" ||
                TYUserInfo.EmpNo.ToString() == "0310-M" ||
                TYUserInfo.EmpNo.ToString() == "0311-M" ||
                TYUserInfo.EmpNo.ToString() == "0403-M" ||
                TYUserInfo.EmpNo.ToString() == "0425-M")
            {
                sKBBUSEO = "";
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                sProcedure.ToString(),
                this.DTP01_STDATE.GetValue().ToString().Replace("-", ""),
                this.DTP01_EDDATE.GetValue().ToString().Replace("-", ""),
                this.CBO01_MMT2000.GetValue().ToString(),
                this.CBH01_MMT1000.GetValue().ToString(),
                sKBBUSEO.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_MR_31AA1589.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_MR_31AA1589.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_MR_31AA1589.GetValue(i, "MMT2300").ToString() == "")
                {
                    //this.FPS91_TY_S_MR_31AA1589_Sheet1.Cells[i, 11].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    this.FPS91_TY_S_MR_31AA1589_Sheet1.Cells[i, 12].CellType = new FarPoint.Win.Spread.CellType.TextCellType(); // 노무비닷컴
                }
            }
        }
        #endregion

        #region Description : 전표발행 버튼
        private void BTN61_JUNPYO_OK_Click(object sender, EventArgs e)
        {
            string sOUTMSG = string.Empty;
            string sB2SSID = string.Empty;

            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // BATID번호 부여
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29C7M958");
                decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());
                sB2SSID = this.IPAdresss + Employer.EmpNo + dAutoSeq.ToString();

                // 즉시마감 전표생성
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_MR_3196Z582", sB2SSID.ToString(),                         // SESSION-ID
                                                            ds.Tables[0].Rows[i]["MMT1020"].ToString(), // 마감일자
                                                            ds.Tables[0].Rows[i]["MMT1000"].ToString(), // 거래처
                                                            ds.Tables[0].Rows[i]["MMT1010"].ToString(), // 부가세구분
                                                            ds.Tables[0].Rows[i]["MMT1050"].ToString(), // 입고번호
                                                            ds.Tables[0].Rows[i]["GUBUN"].ToString(),   // 전표구분
                                                            "TYMRMA003S",                               // 화면이름
                                                            TYUserInfo.EmpNo,                           // 사번
                                                            "A",                                        // 생성구분
                                                            sOUTMSG.ToString());                        // 출력메세지

                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                if (sOUTMSG.Substring(0, 2) != "OK")
                {
                    return;
                }
            }

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_MR_31945578");
        }
        #endregion

        #region Description : 전표취소 버튼
        private void BTN61_JUNPYO_CANCEL_Click(object sender, EventArgs e)
        {
            string sOUTMSG = string.Empty;
            string sB2SSID = string.Empty;

            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // BATID번호 부여
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29C7M958");
                decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());
                sB2SSID = this.IPAdresss + Employer.EmpNo + dAutoSeq.ToString();

                // 즉시마감 전표취소
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_MR_3196Z582", sB2SSID.ToString(),                         // SESSION-ID
                                                            ds.Tables[0].Rows[i]["MMT1020"].ToString(), // 마감일자
                                                            ds.Tables[0].Rows[i]["MMT1000"].ToString(), // 거래처
                                                            ds.Tables[0].Rows[i]["MMT1010"].ToString(), // 부가세구분
                                                            ds.Tables[0].Rows[i]["MMT1050"].ToString(), // 입고번호
                                                            ds.Tables[0].Rows[i]["GUBUN"].ToString(),   // 전표구분
                                                            "TYMRMA003S",                               // 화면이름
                                                            TYUserInfo.EmpNo,                           // 사번
                                                            "D",                                        // 생성구분
                                                            sOUTMSG.ToString());                        // 출력메세지

                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                if (sOUTMSG.Substring(0, 2) != "OK")
                {
                    return;
                }
            }

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_MR_3194D581");
        }
        #endregion

        #region Description : 전표발행 ProcessCheck 이벤트
        private void BTN61_JUNPYO_OK_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;
            int j = 0;

            int iCnt = 0;

            DataSet ds = new DataSet();

            DataTable dt  = new DataTable();
            DataTable dt1 = new DataTable();

            // 생성
            ds.Tables.Add(this.FPS91_TY_S_MR_31AA1589.GetDataSourceInclude(TSpread.TActionType.Select,
                                                      "MMT1020", "MMT1000", "MMT1010", "MMT2300", "GUBUN", "MMT1050", "MET7000"));
                                                      // 마감일자, 거래처, 부가구분, 전표번호, 전표구분, 입고번호

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["GUBUN"].ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_31ABB593");
                        e.Successed = false;
                        return;
                    }

                    // 노무비 닷컴 구분 'Y' 인경우 '미지급금(본점)'만 사용가능
                    if (ds.Tables[0].Rows[i]["MET7000"].ToString() == "Y" && ds.Tables[0].Rows[i]["GUBUN"].ToString() != "01")
                    {
                        this.ShowMessage("TY_M_MR_A1DH3730");
                        e.Successed = false;
                        return;
                    }

                    // 신용카드 사용분 선택시 부가세 구분 = 신용카드 경우에만 선택 할 수 있음.
                    if (ds.Tables[0].Rows[i]["GUBUN"].ToString() == "04" && (ds.Tables[0].Rows[i]["MMT1010"].ToString() != "58" && ds.Tables[0].Rows[i]["MMT1010"].ToString() != "99"))
                    {
                        this.ShowMessage("TY_M_MR_31F1J721");
                        e.Successed = false;
                        return;
                    }

                    // 수리일 경우 전월 상각 자료 존재 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_35LBE701",
                        ds.Tables[0].Rows[i]["MMT1020"].ToString(),
                        ds.Tables[0].Rows[i]["MMT1000"].ToString(),
                        ds.Tables[0].Rows[i]["MMT1010"].ToString(),
                        "2"
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        for (j = 0; j < dt.Rows.Count; j++)
                        {
                            // 20161103 풀어야 함
                            //// 수리시 전월에 상각 자료가 존재하는지 체크
                            //if (dt.Rows[j]["MET2070"].ToString() != "")
                            //{
                            //    this.DbConnector.CommandClear();
                            //    this.DbConnector.Attach
                            //        (
                            //        "TY_P_MR_35LBK702",
                            //        dt.Rows[j]["MET2070"].ToString()
                            //        );

                            //    dt1 = this.DbConnector.ExecuteDataTable();

                            //    if (dt1.Rows.Count > 0)
                            //    {
                            //        string sYEAR  = string.Empty;
                            //        string sMonth = string.Empty;
                            //        string sDATE  = string.Empty;

                            //        if (int.Parse(this.DTP01_MMT1020.GetValue().ToString().Substring(4, 2)) == 01)
                            //        {
                            //            sYEAR = Convert.ToString(int.Parse(this.DTP01_MMT1020.GetValue().ToString().Substring(0, 4)) - 1);
                            //            sMonth = "12";
                            //        }
                            //        else
                            //        {
                            //            sYEAR  = Convert.ToString(int.Parse(this.DTP01_MMT1020.GetValue().ToString().Substring(0, 4)));
                            //            sMonth = Set_Fill2(Convert.ToString(int.Parse(this.DTP01_MMT1020.GetValue().ToString().Substring(4, 2)) - 1));
                            //        }

                            //        sDATE = sYEAR.ToString() + sMonth.ToString();

                            //        if ((int.Parse(sDATE.ToString())) != int.Parse(dt1.Rows[0]["FXMYYMM"].ToString()))
                            //        {
                            //            this.ShowMessage("TY_M_MR_35LBM703");
                            //            e.Successed = false;
                            //            return;
                            //        }
                            //    }
                            //    else
                            //    {
                            //        this.ShowMessage("TY_M_MR_35LBM703");
                            //        e.Successed = false;
                            //        return;
                            //    }
                            //}
                        }
                    }

                    iCnt = 0;

                    //// 현금만 발행(체크)
                    //this.DbConnector.CommandClear();
                    //this.DbConnector.Attach
                    //    (
                    //    "TY_P_MR_31972583",
                    //    ds.Tables[0].Rows[i]["MMT1020"].ToString(),
                    //    ds.Tables[0].Rows[i]["MMT1000"].ToString(),
                    //    ds.Tables[0].Rows[i]["MMT1010"].ToString(),
                    //    ds.Tables[0].Rows[i]["MMT1050"].ToString()
                    //    );

                    //iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                    //if (iCnt == 0)
                    //{
                    //    this.ShowMessage("TY_M_MR_31AAB592");
                    //    e.Successed = false;
                    //    return;
                    //}

                    if (ds.Tables[0].Rows[i]["MMT2300"].ToString() != "")
                    {
                        this.ShowMessage("TY_M_MR_3174H522");
                        e.Successed = false;
                        return;
                    }
                }
            }
            else
            {
                this.ShowMessage("TY_M_MR_3176V530");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_MR_31943577"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 전표취소 ProcessCheck 이벤트
        private void BTN61_JUNPYO_CANCEL_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;
            int iCnt = 0;

            DataSet ds = new DataSet();

            // 취소
            ds.Tables.Add(this.FPS91_TY_S_MR_31AA1589.GetDataSourceInclude(TSpread.TActionType.Select,
                                                      "MMT1020", "MMT1000", "MMT1010", "MMT2300", "GUBUN", "MMT1050"));
                                                      // 마감일자, 거래처, 부가구분, 전표번호, 전표구분, 입고번호

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["GUBUN"].ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_31ABB593");
                        e.Successed = false;
                        return;
                    }

                    iCnt = 0;

                    // 즉시마감 전표발행 - 취소 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_31AA0588",
                        ds.Tables[0].Rows[i]["MMT1020"].ToString(),
                        ds.Tables[0].Rows[i]["MMT1000"].ToString(),
                        ds.Tables[0].Rows[i]["MMT1010"].ToString(),
                        ds.Tables[0].Rows[i]["MMT1050"].ToString()
                        );

                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                    if (iCnt == 0)
                    {
                        this.ShowMessage("TY_M_MR_31933569");
                        e.Successed = false;
                        return;
                    }

                    if (ds.Tables[0].Rows[i]["MMT2300"].ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_31933569");
                        e.Successed = false;
                        return;
                    }
                }
            }
            else
            {
                this.ShowMessage("TY_M_MR_3176V530");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_MR_3194D580"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 전표출력 버튼 이벤트
        private void FPS91_TY_S_MR_31AA1589_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "12")   // 노무비닷컴
            //if (e.Column.ToString() == "11")
            {
                if (this.FPS91_TY_S_MR_31AA1589.GetValue("MMT2300").ToString() != "")
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach
                        (
                        "TY_P_AC_2AU2M916",
                        this.FPS91_TY_S_MR_31AA1589.GetValue("MMT2300").ToString().Substring(0, 6),
                        this.FPS91_TY_S_MR_31AA1589.GetValue("MMT2300").ToString().Substring(6, 8),
                        this.FPS91_TY_S_MR_31AA1589.GetValue("MMT2300").ToString().Substring(14, 3),
                        this.FPS91_TY_S_MR_31AA1589.GetValue("MMT2300").ToString().Substring(14, 3)
                        );

                    if (Convert.ToDouble(this.FPS91_TY_S_MR_31AA1589.GetValue("MMT2300").ToString().Substring(6, 4)) > 2014)
                    {
                        ActiveReport rpt = new TYACBJ0012R();
                        // 세로 출력
                        rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;
                        DataTable dt = this.DbConnector.ExecuteDataTable();
                        if (dt.Rows.Count > 0)
                        {
                            (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                        }
                    }
                    else
                    {
                        ActiveReport rpt = new TYACBJ001R();
                        // 세로 출력
                        rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;
                        DataTable dt = this.DbConnector.ExecuteDataTable();
                        if (dt.Rows.Count > 0)
                        {
                            (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                        }
                    }
                }
                else
                {
                    this.ShowMessage("TY_M_MR_31933569");
                    return;
                }
            }
        }
        #endregion
    }
}