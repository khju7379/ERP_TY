using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;
using TY.ER.AC00;

namespace TY.ER.MR00
{
    /// <summary>
    /// 자산 생성 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2013.02.18 09:59
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_31TAD935 : 자산 생성 - 구매
    ///  TY_P_MR_32I3D080 : 자산취득 미생성 조회
    ///  TY_P_MR_32I3F081 : 자산취득 생성 조회
    ///  TY_P_MR_32I3F082 : 자산수리 미생성 조회
    ///  TY_P_MR_32I3G083 : 자산수리 생성 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_32I3H084 : 자산생성 및 수리 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  GGUBUN : 구분
    ///  GOKCR : 생성구분
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    /// </summary>
    public partial class TYMRJA001B : TYBase
    {
        #region Description : 페이지 로드
        public TYMRJA001B()
        {
            InitializeComponent();
        }

        private void TYMRJA001B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_CREATE.ProcessCheck += new TButton.CheckHandler(BTN61_CREATE_ProcessCheck);
            this.BTN61_CANCEL.ProcessCheck += new TButton.CheckHandler(BTN61_CANCEL_ProcessCheck);

            this.LBL51_GGUBUN.Visible = false;
            this.CBO01_GGUBUN.Visible = false;

            this.LBL51_GOKCR.Visible = false;
            this.CBO01_GOKCR.Visible = false;

            this.BTN61_CREATE.Visible = false;
            this.BTN61_CANCEL.Visible = false;

            SetStartingFocus(this.TXT01_PRN6010);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sProcedure = string.Empty;

            //if (this.CBO01_GGUBUN.GetValue().ToString() == "1")    // 자산취득
            //{
                //if (this.CBO01_GOKCR.GetValue().ToString() == "A") // 미생성
                //{
                //    this.BTN61_CREATE.Visible = true;
                //    this.BTN61_CANCEL.Visible = false;

                //    sProcedure = "TY_P_MR_32I3D080";
                //}
                //else
                //{
                //    this.BTN61_CREATE.Visible = false;
                //    this.BTN61_CANCEL.Visible = true;

                //    sProcedure = "TY_P_MR_32I3F081";
                //}
            //}
            //else // 자산수리
            //{
                //if (this.CBO01_GOKCR.GetValue().ToString() == "A") // 생성
                //{
                //    this.BTN61_CREATE.Visible = true;
                //    this.BTN61_CANCEL.Visible = false;

                //    sProcedure = "TY_P_MR_32I3F082";
                //}
                //else
                //{
                //    this.BTN61_CREATE.Visible = false;
                //    this.BTN61_CANCEL.Visible = true;

                //    sProcedure = "TY_P_MR_32I3G083";
                //}
            //}

            string sJASAN = string.Empty;

            sJASAN = this.TXT01_PRN6010.GetValue().ToString() + this.TXT01_PRN6011.GetValue().ToString() + this.TXT01_PRN6012.GetValue().ToString();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_35GAD682",
                sJASAN.ToString(),
                this.MTB01_GSTDATE.GetValue().ToString().Replace("-", "").ToString(),
                this.MTB01_GEDDATE.GetValue().ToString().Replace("-", "").ToString(),
                this.TXT01_RRM1180.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_MR_32I3H084.SetValue(dt);
        }
        #endregion

        #region Description : 생성 버튼
        private void BTN61_CREATE_Click(object sender, EventArgs e)
        {
            string sOUTMSG = string.Empty;

            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_MR_31TAD935", ds.Tables[0].Rows[i]["MET1000"].ToString(),
                                                            ds.Tables[0].Rows[i]["RRNUM"].ToString(),
                                                            ds.Tables[0].Rows[i]["RRN1040"].ToString(),
                                                            ds.Tables[0].Rows[i]["RRN1050"].ToString(),
                                                            this.CBO01_GGUBUN.GetValue().ToString(),
                                                            this.CBO01_GOKCR.GetValue().ToString(),
                                                            TYUserInfo.EmpNo,
                                                            sOUTMSG.ToString());

                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                if (sOUTMSG.Substring(0, 2) != "OK")
                {
                    return;
                }
            }

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_26E30875");
        }
        #endregion

        #region Description : 취소 버튼
        private void BTN61_CANCEL_Click(object sender, EventArgs e)
        {
            string sOUTMSG = string.Empty;

            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_MR_31TAD935", ds.Tables[0].Rows[i]["MET1000"].ToString(),
                                                            ds.Tables[0].Rows[i]["RRNUM"].ToString(),
                                                            ds.Tables[0].Rows[i]["RRN1040"].ToString(),
                                                            ds.Tables[0].Rows[i]["RRN1050"].ToString(),
                                                            this.CBO01_GGUBUN.GetValue().ToString(),
                                                            this.CBO01_GOKCR.GetValue().ToString(),
                                                            TYUserInfo.EmpNo,
                                                            sOUTMSG.ToString());

                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                if (sOUTMSG.Substring(0, 2) != "OK")
                {
                    return;
                }
            }

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_AC_2CDB1167");
        }
        #endregion

        #region Description : 생성 ProcessCheck 이벤트
        private void BTN61_CREATE_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;
            int iCnt = 0;

            DataSet ds = new DataSet();

            // 선택
            ds.Tables.Add(this.FPS91_TY_S_MR_32I3H084.GetDataSourceInclude(TSpread.TActionType.Select, "MET1000", "RRNUM", "RRN1040", "RRN1050", "RRN6000", "RRN1080")); // 입고번호, 거래처, 입고일자

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (this.CBO01_GGUBUN.GetValue().ToString() == "1")    // 자산취득
                    {
                        if (ds.Tables[0].Rows[i]["RRN1080"].ToString() != "12200100")
                        {
                            iCnt = 0;

                            // 토지 제외한 자산
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach
                                (
                                "TY_P_MR_32I4V091",
                                ds.Tables[0].Rows[i]["RRNUM"].ToString()
                                );

                            iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                            if (iCnt > 0)
                            {
                                this.ShowMessage("TY_M_MR_32I51101");
                                e.Successed = false;
                                return;
                            }
                        }
                        else
                        {
                            iCnt = 0;

                            // 토지 자산
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach
                                (
                                "TY_P_MR_32I4V092",
                                ds.Tables[0].Rows[i]["RRNUM"].ToString()
                                );

                            iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                            if (iCnt > 0)
                            {
                                this.ShowMessage("TY_M_MR_32I51102");
                                e.Successed = false;
                                return;
                            }
                        }
                    }
                    else // 자산 수리
                    {
                        iCnt = 0;

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_32I4W098",
                            ds.Tables[0].Rows[i]["RRNUM"].ToString()
                            );

                        iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                        if (iCnt > 0)
                        {
                            this.ShowMessage("TY_M_MR_32I5A103");
                            e.Successed = false;
                            return;
                        }
                    }
                }
            }
            else
            {
                this.ShowMessage("TY_M_MR_3176V530");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_26E2Z874"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 취소 ProcessCheck 이벤트
        private void BTN61_CANCEL_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;
            int iCnt = 0;

            DataSet ds = new DataSet();

            // 선택
            ds.Tables.Add(this.FPS91_TY_S_MR_32I3H084.GetDataSourceInclude(TSpread.TActionType.Select, "MET1000", "RRNUM", "RRN1040", "RRN1050", "RRN6000", "RRN6010", "RRN1080")); // 입고번호, 거래처, 입고일자

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (this.CBO01_GGUBUN.GetValue().ToString() == "1")    // 자산취득
                {
                    if (ds.Tables[0].Rows[i]["RRN1080"].ToString() != "12200100")
                    {
                        iCnt = 0;

                        // 토지 제외한 자산
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_32I4W095",
                            ds.Tables[0].Rows[i]["RRNUM"].ToString(),
                            ds.Tables[0].Rows[i]["RRN6000"].ToString()
                            );

                        iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                        if (iCnt > 0)
                        {
                            this.ShowMessage("TY_M_MR_32I5B104");
                            e.Successed = false;
                            return;
                        }
                    }
                    else
                    {
                        iCnt = 0;

                        // 토지 자산
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_32I4W096",
                            ds.Tables[0].Rows[i]["RRNUM"].ToString(),
                            ds.Tables[0].Rows[i]["RRN6000"].ToString()
                            );

                        iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                        if (iCnt > 0)
                        {
                            this.ShowMessage("TY_M_MR_32I5C105");
                            e.Successed = false;
                            return;
                        }
                    }
                }
                else // 자산 수리
                {
                    iCnt = 0;

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_32I4W100",
                        ds.Tables[0].Rows[i]["RRN6010"].ToString().Substring(0, 4).ToString(),
                        ds.Tables[0].Rows[i]["RRN6010"].ToString().Substring(4, 4).ToString(),
                        ds.Tables[0].Rows[i]["RRN6010"].ToString().Substring(8, 3).ToString(),
                        ds.Tables[0].Rows[i]["MET1000"].ToString()
                        );

                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                    if (iCnt > 0)
                    {
                        this.ShowMessage("TY_M_MR_32I5E106");
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

            if (!this.ShowMessage("TY_M_AC_2CDB0166"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 자산번호 버튼 클릭
        private void BTN61_PRN60101_Click(object sender, EventArgs e)
        {
            TYMRGB010S popup = new TYMRGB010S();
            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_PRN6010.SetValue(popup.fsFXSYEAR);   // 자산년도
                this.TXT01_PRN6011.SetValue(popup.fsFXSSEQ);    // 자산순번
                this.TXT01_PRN6012.SetValue(popup.fsFXSSUBNUM); // 가족코드
                this.TXT01_PRN6010NM.SetValue(popup.fsFXSNAME); // 자산명
            }
        }
        #endregion

        private void TXT01_PRN6010_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                BTN61_PRN60101_Click(null, null);
            }
        }

        private void TXT01_PRN6011_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                BTN61_PRN60101_Click(null, null);
            }
        }

        private void TXT01_PRN6012_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                BTN61_PRN60101_Click(null, null);
            }
        }

        private void FPS91_TY_S_MR_32I3H084_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            // 파라미터값 보내기
            if ((new TYACHF004I(this.FPS91_TY_S_MR_32I3H084.GetValue("RRN6010").ToString().Substring(0, 4),
                                this.FPS91_TY_S_MR_32I3H084.GetValue("RRN6010").ToString().Substring(4, 4)
                                )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
    }
}