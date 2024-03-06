using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Export.Html;
using DataDynamics.ActiveReports.Export.Pdf;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using TY.ER.GB00;
using TY.ER.AC00;

namespace TY.ER.US00
{
    /// <summary>
    /// 하역료 단가 조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2016.06.08 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_7
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  CHYMDATE : 기준일자
    ///  CHYMSEQ : 순번
    /// </summary>
    public partial class TYUSME017P : TYBase
    {
        private string fsFileDownPath = "C:\\Invoice\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";

        #region Descriptino : 페이지 로드
        public TYUSME017P()
        {
            InitializeComponent();
        }

        private void TYUSME017P_Load(object sender, System.EventArgs e)
        {
            this.BTN61_PRT.ProcessCheck += new TButton.CheckHandler(BTN61_PRT_ProcessCheck);

            this.MTB01_GDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.MTB01_GDATE);
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sB2DPMK = string.Empty;
            string sB2DTMK = string.Empty;
            string sB2NOSQ = string.Empty;

            string sProcedure     = string.Empty;
            string sSTATTRIBUTES6 = string.Empty;
            string sEDATTRIBUTES6 = string.Empty;

            int i = 0;

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();

            DataTable dt_JPNO = new DataTable();

            if (this.CBO01_GGUBUN.GetText().ToString() == "전표")
            {
                #region Description : 전표 출력

                // 미수금 업데이트
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_93DDO070", Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim().Substring(0, 6));
                this.DbConnector.ExecuteTranQueryList();

                if (this.CBO01_GMEGUBUN.GetText().ToString() == "시설사용료")
                {
                    sProcedure = "TY_P_US_93CG5062";

                    //sSTATTRIBUTES6 = "S12";
                    //sEDATTRIBUTES6 = "S12";
                }
                else if (this.CBO01_GMEGUBUN.GetText().ToString() == "하역료")
                {
                    sProcedure = "TY_P_US_93CG8063";

                    //sSTATTRIBUTES6 = "S13";
                    //sEDATTRIBUTES6 = "S13";
                }
                else if (this.CBO01_GMEGUBUN.GetText().ToString() == "보관료")
                {
                    sProcedure = "TY_P_US_93CG9064";

                    //sSTATTRIBUTES6 = "S14";
                    //sEDATTRIBUTES6 = "S14";
                }
                else if (this.CBO01_GMEGUBUN.GetText().ToString() == "조출료")
                {
                    sProcedure = "TY_P_US_93CG9065";

                    //sSTATTRIBUTES6 = "S15";
                    //sEDATTRIBUTES6 = "S15";
                }
                else if (this.CBO01_GMEGUBUN.GetText().ToString() == "난작업비")
                {
                    sProcedure = "TY_P_US_93CGK066";

                    //sSTATTRIBUTES6 = "S17";
                    //sEDATTRIBUTES6 = "S17";
                }
                else if (this.CBO01_GMEGUBUN.GetText().ToString() == "이송료")
                {
                    sProcedure = "TY_P_US_93CGL068";

                    //sSTATTRIBUTES6 = "S21";
                    //sEDATTRIBUTES6 = "S21";
                }
                else if (this.CBO01_GMEGUBUN.GetText().ToString() == "별첨화주")
                {
                    sProcedure = "TY_P_US_93CGK067";

                    //sSTATTRIBUTES6 = "S13";
                    //sEDATTRIBUTES6 = "S14";
                }

                // 현업 매출 전표 데이터 가져오기
                if (this.CBO01_GMEGUBUN.GetText().ToString() == "별첨화주")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach(sProcedure, Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim().Substring(0, 6),
                                                        Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim(),
                                                        this.CBH01_STHANGCHA.GetValue().ToString().Trim(),
                                                        this.CBH01_EDHANGCHA.GetValue().ToString().Trim(),
                                                        this.CBH01_GGOKJONG.GetValue().ToString().Trim(),
                                                        this.CBH01_GHWAJU.GetValue().ToString().Trim(),
                                                        Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim().Substring(0, 6),
                                                        Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim(),
                                                        this.CBH01_STHANGCHA.GetValue().ToString().Trim(),
                                                        this.CBH01_EDHANGCHA.GetValue().ToString().Trim(),
                                                        this.CBH01_GGOKJONG.GetValue().ToString().Trim(),
                                                        this.CBH01_GHWAJU.GetValue().ToString().Trim()
                                                        );
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach(sProcedure, Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim().Substring(0, 6),
                                                        Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim(),
                                                        this.CBH01_STHANGCHA.GetValue().ToString().Trim(),
                                                        this.CBH01_EDHANGCHA.GetValue().ToString().Trim(),
                                                        this.CBH01_GGOKJONG.GetValue().ToString().Trim(),
                                                        this.CBH01_GHWAJU.GetValue().ToString().Trim()
                                                        );
                }

                dt_JPNO = this.DbConnector.ExecuteDataTable();

                if (dt_JPNO.Rows.Count > 0)
                {
                    sB2DPMK = dt_JPNO.Rows[0][0].ToString().Substring(0, 6);
                    sB2DTMK = dt_JPNO.Rows[0][0].ToString().Substring(6, 8);

                    // 순번 가져오기
                    for (i = 0; i < dt_JPNO.Rows.Count; i++)
                    {
                        if (sB2NOSQ == "")
                        {
                            sB2NOSQ = dt_JPNO.Rows[i]["JPNO"].ToString().Substring(14, 3);
                        }
                        else
                        {
                            sB2NOSQ = sB2NOSQ + "," + dt_JPNO.Rows[i]["JPNO"].ToString().Substring(14, 3);
                        }
                    }

                    // 전자 수정 세금계산서 매출 전표 데이터 가져오기
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_93CGN069", Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim());

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        // 순번 가져오기
                        for (i = 0; i < dt.Rows.Count; i++)
                        {
                            if (sB2NOSQ == "")
                            {
                                sB2NOSQ = dt.Rows[i]["JPNO"].ToString().Substring(14, 3);
                            }
                            else
                            {
                                sB2NOSQ = sB2NOSQ + "," + dt.Rows[i]["JPNO"].ToString().Substring(14, 3);
                            }
                        }
                    }


                    // 전표 출력
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_738FY877",
                        sB2DPMK.ToString(),
                        sB2DTMK.ToString(),
                        sB2NOSQ.ToString()
                        );

                    dt1 = this.DbConnector.ExecuteDataTable();

                    if (Convert.ToDouble(sB2DTMK.ToString().Substring(0, 4)) > 2014)
                    {
                        ActiveReport rpt = new TYACBJ0012R();
                        // 세로 출력
                        rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;

                        if (dt1.Rows.Count > 0)
                        {
                            (new TYERGB001P(rpt, UP_ConvertJunPyo(dt1))).ShowDialog();
                        }
                    }
                    else
                    {
                        ActiveReport rpt = new TYACBJ001R();
                        // 세로 출력
                        rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;

                        if (dt1.Rows.Count > 0)
                        {
                            (new TYERGB001P(rpt, UP_ConvertJunPyo(dt1))).ShowDialog();
                        }
                    }
                }
                else
                {
                    this.ShowCustomMessage("전표자료가 존재하지 않습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.MTB01_GDATE);

                    return;
                }

                #endregion

            }
            else if (this.CBO01_GGUBUN.GetText().ToString() == "세금계산서")
            {
                #region Description : 세금계산서 출력

                if (this.CBO01_GMEGUBUN.GetText().ToString() == "시설사용료")
                {
                    sProcedure = "TY_P_US_93DF3073";
                }
                else if (this.CBO01_GMEGUBUN.GetText().ToString() == "하역료")
                {
                    sProcedure = "TY_P_US_93DF4074";
                }
                else if (this.CBO01_GMEGUBUN.GetText().ToString() == "보관료")
                {
                    sProcedure = "TY_P_US_93DF4075";
                }
                else if (this.CBO01_GMEGUBUN.GetText().ToString() == "조출료")
                {
                    sProcedure = "TY_P_US_93DF5076";
                }
                else if (this.CBO01_GMEGUBUN.GetText().ToString() == "난작업비")
                {
                    sProcedure = "TY_P_US_93DF6077";
                }
                else if (this.CBO01_GMEGUBUN.GetText().ToString() == "이송료")
                {
                    sProcedure = "TY_P_US_93DF6078";
                }
                else if (this.CBO01_GMEGUBUN.GetText().ToString() == "별첨화주")
                {
                    sProcedure = "TY_P_US_93DF1071";
                }

                // 세금계산서 데이터 가져오기
                if (this.CBO01_GMEGUBUN.GetText().ToString() == "별첨화주")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach(sProcedure, Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim().Substring(0, 6),
                                                        Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim(),
                                                        this.CBH01_STHANGCHA.GetValue().ToString().Trim(),
                                                        this.CBH01_EDHANGCHA.GetValue().ToString().Trim(),
                                                        this.CBH01_GGOKJONG.GetValue().ToString().Trim(),
                                                        this.CBH01_GHWAJU.GetValue().ToString().Trim(),
                                                        Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim().Substring(0, 6),
                                                        Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim(),
                                                        this.CBH01_STHANGCHA.GetValue().ToString().Trim(),
                                                        this.CBH01_EDHANGCHA.GetValue().ToString().Trim(),
                                                        this.CBH01_GGOKJONG.GetValue().ToString().Trim(),
                                                        this.CBH01_GHWAJU.GetValue().ToString().Trim()
                                                        );
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach(sProcedure, Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim().Substring(0, 6),
                                                        Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim(),
                                                        this.CBH01_STHANGCHA.GetValue().ToString().Trim(),
                                                        this.CBH01_EDHANGCHA.GetValue().ToString().Trim(),
                                                        this.CBH01_GGOKJONG.GetValue().ToString().Trim(),
                                                        this.CBH01_GHWAJU.GetValue().ToString().Trim()
                                                        );
                }

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    ActiveReport rpt = new TYUSME017R();
                    // 세로 출력
                    rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;

                    // 수정세금계산서 가져오기
                    dt1 = UP_Get_ConvertTax(dt);

                    // 세금계산서 출력
                    (new TYERGB001P(rpt, dt1)).ShowDialog();
                }

                #endregion
            }
            else if (this.CBO01_GGUBUN.GetText().ToString() == "거래명세서")
            {
                #region Description : 거래명세서 출력

                if (this.CBO01_GMEGUBUN.GetText().ToString() == "시설사용료")
                {
                    sProcedure = "TY_P_US_941DJ210";
                }
                else if (this.CBO01_GMEGUBUN.GetText().ToString() == "하역료")
                {
                    sProcedure = "TY_P_US_941EE213";
                }
                else if (this.CBO01_GMEGUBUN.GetText().ToString() == "조출료")
                {
                    sProcedure = "TY_P_US_941HB216";
                }
                else if (this.CBO01_GMEGUBUN.GetText().ToString() == "난작업비")
                {
                    sProcedure = "TY_P_US_942FM221";
                }
                else if (this.CBO01_GMEGUBUN.GetText().ToString() == "이송료")
                {
                    sProcedure = "TY_P_US_942E9218";
                }
                else if (this.CBO01_GMEGUBUN.GetText().ToString() == "보관료")
                {
                    sProcedure = "TY_P_US_942HA222";
                }

                if (this.CBO01_GMEGUBUN.GetText().ToString() != "별첨화주")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach(sProcedure, Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim(),
                                                        this.CBH01_STHANGCHA.GetValue().ToString().Trim(),
                                                        this.CBH01_EDHANGCHA.GetValue().ToString().Trim(),
                                                        this.CBH01_GGOKJONG.GetValue().ToString().Trim(),
                                                        this.CBH01_GHWAJU.GetValue().ToString().Trim(),
                                                        "" // 전표번호
                                                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        if (this.CBO01_GMEGUBUN.GetText().ToString() == "시설사용료")
                        {
                            ActiveReport rpt = new TYUSME017R1();

                            // 세로 출력
                            rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;

                            // 거래명세서 출력
                            (new TYERGB001P(rpt, dt)).ShowDialog();
                        }
                        else if (this.CBO01_GMEGUBUN.GetText().ToString() == "하역료")
                        {
                            ActiveReport rpt = new TYUSME017R2();

                            // 세로 출력
                            rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;

                            // 거래명세서 출력
                            (new TYERGB001P(rpt, dt)).ShowDialog();
                        }
                        else if (this.CBO01_GMEGUBUN.GetText().ToString() == "조출료")
                        {
                            ActiveReport rpt = new TYUSME017R3();

                            // 세로 출력
                            rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;

                            // 거래명세서 출력
                            (new TYERGB001P(rpt, dt)).ShowDialog();
                        }
                        else if (this.CBO01_GMEGUBUN.GetText().ToString() == "난작업비")
                        {
                            ActiveReport rpt = new TYUSME017R4();

                            // 세로 출력
                            rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;

                            // 거래명세서 출력
                            (new TYERGB001P(rpt, dt)).ShowDialog();
                        }
                        else if (this.CBO01_GMEGUBUN.GetText().ToString() == "이송료")
                        {
                            ActiveReport rpt = new TYUSME017R5();

                            // 세로 출력
                            rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;

                            // 거래명세서 출력
                            (new TYERGB001P(rpt, dt)).ShowDialog();
                        }
                        else if (this.CBO01_GMEGUBUN.GetText().ToString() == "보관료")
                        {
                            DataTable retDt = QueryDataSetReport(dt);

                            ActiveReport rpt = new TYUSME017R6();

                            // 세로 출력
                            rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;

                            // 거래명세서 출력
                            (new TYERGB001P(rpt, retDt)).ShowDialog();
                        }
                    }
                }
                else
                {
                    ActiveReport rpt1  = new ActiveReport();
                    ActiveReport rpt2  = new ActiveReport();

                    DataTable dt_MCHY  = new DataTable();
                    DataTable dt_MCBK  = new DataTable();
                    DataTable ret_MCBK = new DataTable();

                    // 별첨화주 - 하역료 거래명세서
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_95KFE589", Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim(),
                                                        this.CBH01_STHANGCHA.GetValue().ToString().Trim(),
                                                        this.CBH01_EDHANGCHA.GetValue().ToString().Trim(),
                                                        this.CBH01_GGOKJONG.GetValue().ToString().Trim(),
                                                        this.CBH01_GHWAJU.GetValue().ToString().Trim(),
                                                        "" // 전표번호
                                                        );

                    dt_MCHY = this.DbConnector.ExecuteDataTable();

                    if (dt_MCHY.Rows.Count > 0)
                    {
                        rpt1 = new TYUSME017R2();
                    }

                    // 별첨화주 - 보관료 거래명세서
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_95KFH590", Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim(),
                                                        this.CBH01_STHANGCHA.GetValue().ToString().Trim(),
                                                        this.CBH01_EDHANGCHA.GetValue().ToString().Trim(),
                                                        this.CBH01_GGOKJONG.GetValue().ToString().Trim(),
                                                        this.CBH01_GHWAJU.GetValue().ToString().Trim(),
                                                        "" // 전표번호
                                                        );

                    dt_MCBK = this.DbConnector.ExecuteDataTable();

                    if (dt_MCBK.Rows.Count > 0)
                    {
                        ret_MCBK = QueryDataSetReport(dt_MCBK);
                    }

                    if (ret_MCBK.Rows.Count > 0)
                    {
                        rpt2 = new TYUSME017R6();
                    }

                    // 별첨화주 - (하역료, 보관료) 거래명세서 출력
                    if (dt_MCHY.Rows.Count > 0 || ret_MCBK.Rows.Count > 0)
                    {
                        // 되는 소스
                        //(new TYERGB001P(rpt1, dt_MCHY)).Hide();
                        //(new TYERGB001P(rpt2, ret_MCBK)).Hide();

                        //for (i = 0; i < rpt2.Document.Pages.Count; i++)
                        //{
                        //    rpt1.Document.Pages.Add(rpt2.Document.Pages[i].Clone());
                        //}

                        //this.AVW01_REPORT.Document = rpt1.Document;



                        // 되는 소스
                        //(new TYERGB001P(rpt1, dt_MCHY)).Hide();
                        //(new TYERGB001P(rpt2, ret_MCBK)).Hide();

                        //ActiveReport rpt = new TYUSME017R7();

                        //for (i = 0; i < rpt1.Document.Pages.Count; i++)
                        //{
                        //    rpt.Document.Pages.Add(rpt1.Document.Pages[i].Clone());
                        //}

                        //for (i = 0; i < rpt2.Document.Pages.Count; i++)
                        //{
                        //    rpt.Document.Pages.Add(rpt2.Document.Pages[i].Clone());
                        //}

                        //(new TYERGB001P(rpt)).ShowDialog();




                        rpt1.DataSource = dt_MCHY;
                        rpt2.DataSource = ret_MCBK;

                        rpt1.Run(false);
                        rpt2.Run(false);

                        ActiveReport rpt = new TYUSME017R7();

                        // 별첨화주 - 하역료 거래명세서 출력
                        for (i = 0; i < rpt1.Document.Pages.Count; i++)
                        {
                            rpt.Document.Pages.Add(rpt1.Document.Pages[i].Clone());
                        }

                        // 별첨화주 - 보관료 거래명세서 출력
                        for (i = 0; i < rpt2.Document.Pages.Count; i++)
                        {
                            rpt.Document.Pages.Add(rpt2.Document.Pages[i].Clone());
                        }

                        (new TYERGB001P(rpt)).ShowDialog();
                    }
                }

                #endregion
            }

            SetFocus(this.CBH01_GHWAJU.CodeText);
        }
        #endregion

        #region Description : 출력 ProcessCheck
        private void BTN61_PRT_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            if (this.CBO01_GGUBUN.GetText().ToString() == "세금계산서" || this.CBO01_GGUBUN.GetText().ToString() == "거래명세서")
            {
                string sProcedure = string.Empty;

                if (this.CBO01_GMEGUBUN.GetText().ToString() == "시설사용료")
                {
                    sProcedure = "TY_P_US_93CE4050";
                }
                else if (this.CBO01_GMEGUBUN.GetText().ToString() == "하역료")
                {
                    sProcedure = "TY_P_US_93CE1051";
                }
                else if (this.CBO01_GMEGUBUN.GetText().ToString() == "보관료")
                {
                    sProcedure = "TY_P_US_93CE2052";
                }
                else if (this.CBO01_GMEGUBUN.GetText().ToString() == "조출료")
                {
                    sProcedure = "TY_P_US_93CE2053";
                }
                else if (this.CBO01_GMEGUBUN.GetText().ToString() == "난작업비")
                {
                    sProcedure = "TY_P_US_93CE3054";
                }
                else if (this.CBO01_GMEGUBUN.GetText().ToString() == "이송료")
                {
                    sProcedure = "TY_P_US_93CE4055";
                }
                else if (this.CBO01_GMEGUBUN.GetText().ToString() == "별첨화주")
                {
                    sProcedure = "TY_P_US_93CE4056";
                }

                if (this.CBO01_GMEGUBUN.GetText().ToString() != "별첨화주")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach(sProcedure, Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim(),
                                                        Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim().Substring(0, 6),
                                                        Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim(),
                                                        this.CBH01_STHANGCHA.GetValue().ToString().Trim(),
                                                        this.CBH01_EDHANGCHA.GetValue().ToString().Trim(),
                                                        this.CBH01_GGOKJONG.GetValue().ToString().Trim(),
                                                        this.CBH01_GHWAJU.GetValue().ToString().Trim()
                                                        );
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach(sProcedure, Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim(),
                                                        Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim().Substring(0, 6),
                                                        Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim(),
                                                        this.CBH01_STHANGCHA.GetValue().ToString().Trim(),
                                                        this.CBH01_EDHANGCHA.GetValue().ToString().Trim(),
                                                        this.CBH01_GGOKJONG.GetValue().ToString().Trim(),
                                                        this.CBH01_GHWAJU.GetValue().ToString().Trim(),
                                                        Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim(),
                                                        Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim().Substring(0, 6),
                                                        Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim(),
                                                        this.CBH01_STHANGCHA.GetValue().ToString().Trim(),
                                                        this.CBH01_EDHANGCHA.GetValue().ToString().Trim(),
                                                        this.CBH01_GGOKJONG.GetValue().ToString().Trim(),
                                                        this.CBH01_GHWAJU.GetValue().ToString().Trim()
                                                        );
                }
                

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    string sVNCODE = string.Empty;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            sVNCODE = dt.Rows[i][0].ToString();
                        }
                        else
                        {
                            sVNCODE = "," + sVNCODE + dt.Rows[i][0].ToString();
                        }
                    }

                    this.ShowCustomMessage("회계 거래처 파일의 사업자 번호와 SILO 거래처 파일의 사업자 번호가 일치 하지 않습니다." + " SILO거래처 코드 " + sVNCODE + "를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                    e.Successed = false;
                    return;
                }
            }
        }
        #endregion

        #region Description : 수정 세금계산서 가져오기
        private DataTable UP_Get_ConvertTax(DataTable Orgdt)
        {
            int i = 0;

            DataTable retDt  = new DataTable();

            DataRow dtRow;

            retDt.Columns.Add("SAUP1",     typeof(System.String));
            retDt.Columns.Add("SAUP2",     typeof(System.String));
            retDt.Columns.Add("SAUP3",     typeof(System.String));
            retDt.Columns.Add("VNSANGHO",  typeof(System.String));
            retDt.Columns.Add("VNIRUM",    typeof(System.String));
            retDt.Columns.Add("JS",        typeof(System.String));
            retDt.Columns.Add("VNTAXJUSO", typeof(System.String));
            retDt.Columns.Add("VNUPTAE",   typeof(System.String));
            retDt.Columns.Add("VNUPJONG",  typeof(System.String));
            retDt.Columns.Add("VSDESC1",   typeof(System.String));
            retDt.Columns.Add("YYMMDD",    typeof(System.String));
            retDt.Columns.Add("BLANKCNT",  typeof(System.Decimal));
            retDt.Columns.Add("AMT",       typeof(System.Decimal));
            retDt.Columns.Add("VAT",       typeof(System.Decimal));
            retDt.Columns.Add("MMDD",      typeof(System.String));
            retDt.Columns.Add("TOTAMT",    typeof(System.Decimal));
            

            for (i= 0; i < Orgdt.Rows.Count; i++)
            {
                dtRow = retDt.NewRow();

                dtRow["SAUP1"]     = Orgdt.Rows[i]["SAUP1"].ToString();
                dtRow["SAUP2"]     = Orgdt.Rows[i]["SAUP2"].ToString();
                dtRow["SAUP3"]     = Orgdt.Rows[i]["SAUP3"].ToString();
                dtRow["VNSANGHO"]  = Orgdt.Rows[i]["VNSANGHO"].ToString();
                dtRow["VNIRUM"]    = Orgdt.Rows[i]["VNIRUM"].ToString();
                dtRow["JS"]        = Orgdt.Rows[i]["JS"].ToString();
                dtRow["VNTAXJUSO"] = Orgdt.Rows[i]["VNTAXJUSO"].ToString();
                dtRow["VNUPTAE"]   = Orgdt.Rows[i]["VNUPTAE"].ToString();
                dtRow["VNUPJONG"]  = Orgdt.Rows[i]["VNUPJONG"].ToString();
                dtRow["VSDESC1"]   = Orgdt.Rows[i]["VSDESC1"].ToString();
                dtRow["YYMMDD"]    = Orgdt.Rows[i]["YYMMDD"].ToString();
                dtRow["BLANKCNT"]  = Convert.ToDecimal(Orgdt.Rows[i]["BLANKCNT"].ToString());
                dtRow["AMT"]       = Convert.ToDecimal(Orgdt.Rows[i]["AMT"].ToString());
                dtRow["VAT"]       = Convert.ToDecimal(Orgdt.Rows[i]["VAT"].ToString());
                dtRow["MMDD"]      = Set_Fill4(Orgdt.Rows[i]["MMDD"].ToString());
                dtRow["TOTAMT"]    = Convert.ToDecimal(Orgdt.Rows[i]["TOTAMT"].ToString());

                retDt.Rows.Add(dtRow);
            }


            string sGUBUN = string.Empty;
            string sVNCODE = string.Empty;

            DataTable dt = new DataTable();
            DataTable ByulDs = new DataTable();

            if (this.CBO01_GMEGUBUN.GetText().ToString() == "별첨화주")
            {
                sGUBUN = "하역수수료 외";
            }
            else
            {
                sGUBUN = this.CBO01_GMEGUBUN.GetText().ToString();
            }


            if (this.CBO01_GMEGUBUN.GetText().ToString() == "하역료" || this.CBO01_GMEGUBUN.GetText().ToString() == "보관료" || this.CBO01_GMEGUBUN.GetText().ToString() == "별첨화주")
            {
                // 별도화주 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_93FAJ129");

                ByulDs = this.DbConnector.ExecuteDataTable();

                for (i = 0; i < ByulDs.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        sVNCODE = "'" + ByulDs.Rows[i][0].ToString() + "'";
                    }
                    else
                    {
                        sVNCODE = sVNCODE + ",'" + ByulDs.Rows[i][0].ToString() + "'";
                    }
                }
            }

            // 전자 수정세금계산서 가져오기
            if (this.CBO01_GMEGUBUN.GetText().ToString() == "하역료" || this.CBO01_GMEGUBUN.GetText().ToString() == "보관료")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_93FAM130", sGUBUN.ToString(),
                                                            sVNCODE.ToString(),
                                                            Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim()
                                                            );
            }

            // 전자 수정세금계산서 가져오기
            if (this.CBO01_GMEGUBUN.GetText().ToString() != "하역료" && this.CBO01_GMEGUBUN.GetText().ToString() != "보관료" && this.CBO01_GMEGUBUN.GetText().ToString() != "별첨화주")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_93FAW131", sGUBUN.ToString(),
                                                            Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim()
                                                            );
            }

            // 전자 수정세금계산서 가져오기
            if (this.CBO01_GMEGUBUN.GetText().ToString() == "별첨화주")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_93FAY132", sGUBUN.ToString(),
                                                            sVNCODE.ToString(),
                                                            Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim()
                                                            );
            }

            dt = this.DbConnector.ExecuteDataTable();

            for (i = 0; i < dt.Rows.Count; i++)
            {
                dtRow = retDt.NewRow();

                dtRow["SAUP1"]     = dt.Rows[i]["SAUP1"].ToString();
                dtRow["SAUP2"]     = dt.Rows[i]["SAUP2"].ToString();
                dtRow["SAUP3"]     = dt.Rows[i]["SAUP3"].ToString();
                dtRow["VNSANGHO"]  = dt.Rows[i]["VNSANGHO"].ToString();
                dtRow["VNIRUM"]    = dt.Rows[i]["VNIRUM"].ToString();
                dtRow["JS"]        = dt.Rows[i]["JS"].ToString();
                dtRow["VNTAXJUSO"] = dt.Rows[i]["VNTAXJUSO"].ToString();
                dtRow["VNUPTAE"]   = dt.Rows[i]["VNUPTAE"].ToString();
                dtRow["VNUPJONG"]  = dt.Rows[i]["VNUPJONG"].ToString();
                dtRow["VSDESC1"]   = dt.Rows[i]["VSDESC1"].ToString();
                dtRow["YYMMDD"]    = dt.Rows[i]["YYMMDD"].ToString();
                dtRow["BLANKCNT"]  = Convert.ToDecimal(dt.Rows[i]["BLANKCNT"].ToString());
                dtRow["AMT"]       = Convert.ToDecimal(dt.Rows[i]["AMT"].ToString());
                dtRow["VAT"]       = Convert.ToDecimal(dt.Rows[i]["VAT"].ToString());
                dtRow["MMDD"]      = dt.Rows[i]["MMDD"].ToString();
                dtRow["TOTAMT"]    = Convert.ToDecimal(dt.Rows[i]["TOTAMT"].ToString());

                retDt.Rows.Add(dtRow);
            }

            return retDt;
        }
        #endregion

        #region Description : 데이터셋 변경
        private DataTable QueryDataSetReport(DataTable dt)
        {
            DataTable dt1 = new DataTable();

            DataTable retDt = new DataTable();

            retDt.Columns.Add("VNSAUPJA",  typeof(System.String));
            retDt.Columns.Add("VNSANGHO",  typeof(System.String));
            retDt.Columns.Add("VNIRUM",    typeof(System.String));
            retDt.Columns.Add("VNTAXJUSO", typeof(System.String));
            retDt.Columns.Add("MONTH",     typeof(System.String));
            retDt.Columns.Add("YSNAME",    typeof(System.String));
            retDt.Columns.Add("JBEJNQTY",  typeof(System.Double));
            retDt.Columns.Add("JYSQTY",    typeof(System.Double));
            retDt.Columns.Add("FLDNAME",   typeof(System.String));
            retDt.Columns.Add("YDHWAJU",   typeof(System.String));
            retDt.Columns.Add("YLINE",     typeof(System.String));
            retDt.Columns.Add("HANGCHANM", typeof(System.String));
            retDt.Columns.Add("BKHANGCHA", typeof(System.String));
            retDt.Columns.Add("BKCHHWAJU", typeof(System.String));
            retDt.Columns.Add("IPHANG",    typeof(System.String));
            retDt.Columns.Add("JAKENDAT",  typeof(System.String));
            retDt.Columns.Add("BDATE",     typeof(System.String));
            retDt.Columns.Add("GOKJONGNM", typeof(System.String));
            retDt.Columns.Add("JCHQTY",    typeof(System.Double));
            retDt.Columns.Add("CHQTY",     typeof(System.Double));
            retDt.Columns.Add("CHDAT",     typeof(System.String));
            retDt.Columns.Add("JEGOQTY",   typeof(System.Double));
            retDt.Columns.Add("DANGA",     typeof(System.Double));
            retDt.Columns.Add("BOKAMT",    typeof(System.Double));
            retDt.Columns.Add("BIGO",      typeof(System.String));
            retDt.Columns.Add("TOTAMT",    typeof(System.Double));
            retDt.Columns.Add("TOTVAT",    typeof(System.Double));
            retDt.Columns.Add("AMOUNT",    typeof(System.Double));
            retDt.Columns.Add("BKYSDATE",  typeof(System.String));


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = retDt.NewRow();

                row["VNSAUPJA"]   = dt.Rows[i]["VNSAUPJA"].ToString();
                string fsVNSAUPJA = dt.Rows[i]["VNSAUPJA"].ToString();

                row["VNSANGHO"]   = dt.Rows[i]["VNSANGHO"].ToString();
                row["VNIRUM"]     = dt.Rows[i]["VNIRUM"].ToString();
                row["VNTAXJUSO"]  = dt.Rows[i]["VNTAXJUSO"].ToString();
                row["MONTH"]      = dt.Rows[i]["MONTH"].ToString();
                string fsBKBOKGB  = Convert.ToString(dt.Rows[i]["BKBOKGB"].ToString());

                if (fsBKBOKGB.Trim() == "1")
                {
                    row["YSNAME"]   = "";
                    row["JBEJNQTY"] = double.Parse(dt.Rows[i]["JBEJNQTY"].ToString());
                    row["JYSQTY"]   = double.Parse(dt.Rows[i]["JYSQTY"].ToString());
                    row["FLDNAME"]  = "확 정 량 ";
                }
                else
                {
                    if (dt.Rows[i]["BKYDHWAJU"].ToString() == dt.Rows[i]["BKHWAJU"].ToString())
                    {
                        row["YSNAME"] = "(양도분)";
                    }
                    else
                    {
                        row["YSNAME"] = "(양수분)";
                    }

                    row["YLINE"]   = "===============";
                    row["FLDNAME"] = "양 수 량";

                    if (dt.Rows[i]["BKYDHWAJU"].ToString() == "")
                    {
                        row["JYSQTY"] = double.Parse(dt.Rows[i]["JGYSQTY"].ToString());
                    }
                    else
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_US_94397223", dt.Rows[i]["BKHANGCHA1"].ToString(),
                                                                    dt.Rows[i]["BKGOKJONG1"].ToString(),
                                                                    dt.Rows[i]["BKYDHWAJU1"].ToString(),
                                                                    dt.Rows[i]["BKBLMSN1"].ToString(),
                                                                    dt.Rows[i]["BKBLHSN1"].ToString(),
                                                                    dt.Rows[i]["BKHWAJU1"].ToString(),
                                                                    dt.Rows[i]["BKYSDATE1"].ToString(),
                                                                    dt.Rows[i]["BKYSSEQ1"].ToString(),
                                                                    dt.Rows[i]["BKYDSEQ1"].ToString());

                        dt1 = this.DbConnector.ExecuteDataTable();

                        if (dt1.Rows.Count > 0)
                        {
                            row["JYSQTY"] = dt1.Rows[0][0].ToString().Trim();
                        }
                    }
                }

                if (dt.Rows[i]["BKYDHWAJU"].ToString() == "")
                {
                    row["YDHWAJU"] = dt.Rows[i]["YDHWAJU"].ToString();
                }
                else
                {
                    if (int.Parse(Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim()) >= 20200120)
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_US_94395224", Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim().Substring(0, 6),
                                                                    dt.Rows[i]["BKHANGCHA1"].ToString(),
                                                                    dt.Rows[i]["BKGOKJONG1"].ToString(),
                                                                    dt.Rows[i]["BKHWAJU1"].ToString(),
                                                                    dt.Rows[i]["BKBLMSN1"].ToString(),
                                                                    dt.Rows[i]["BKBLHSN1"].ToString(),
                                                                    dt.Rows[i]["BKBOKGB"].ToString(),
                                                                    dt.Rows[i]["BKYDHWAJU1"].ToString(),
                                                                    dt.Rows[i]["BKCHHWAJU1"].ToString(),
                                                                    dt.Rows[i]["BKYSDATE1"].ToString(),
                                                                    dt.Rows[i]["BKYSSEQ1"].ToString(),
                                                                    dt.Rows[i]["BKYDSEQ1"].ToString(),
                                                                    Get_Date(this.MTB01_GDATE.GetValue().ToString()).Trim()
                                                                    );
                    }
                    else
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_US_943AX228", dt.Rows[i]["BKHANGCHA1"].ToString(),
                                                                    dt.Rows[i]["BKGOKJONG1"].ToString(),
                                                                    dt.Rows[i]["BKYDHWAJU1"].ToString(),
                                                                    dt.Rows[i]["BKBLMSN1"].ToString(),
                                                                    dt.Rows[i]["BKBLHSN1"].ToString(),
                                                                    dt.Rows[i]["BKYSDATE1"].ToString(),
                                                                    dt.Rows[i]["BKYSSEQ1"].ToString(),
                                                                    dt.Rows[i]["BKYDSEQ1"].ToString()
                                                                    );
                    }

                    dt1 = this.DbConnector.ExecuteDataTable();

                    if (dt1.Rows.Count > 0)
                    {
                        row["YDHWAJU"] = dt1.Rows[0][0].ToString().Trim();
                    }
                }
                row["YLINE"] = dt.Rows[i]["YLINE"].ToString();

                row["HANGCHANM"] = dt.Rows[i]["HANGCHANM"].ToString();
                row["BKHANGCHA"] = dt.Rows[i]["BKHANGCHA"].ToString();
                row["BKCHHWAJU"] = dt.Rows[i]["BKCHHWAJU"].ToString();
                row["IPHANG"]    = dt.Rows[i]["IPHANG"].ToString();
                row["JAKENDAT"]  = dt.Rows[i]["JAKENDAT"].ToString();
                row["BDATE"]     = dt.Rows[i]["BDATE"].ToString();
                row["GOKJONGNM"] = dt.Rows[i]["GOKJONGNM"].ToString();

                row["JCHQTY"]    = double.Parse(dt.Rows[i]["JCHQTY"].ToString());

                row["CHDAT"]     = dt.Rows[i]["CHDAT"].ToString();
                row["CHQTY"]     = double.Parse(dt.Rows[i]["CHQTY"].ToString());
                row["JEGOQTY"]   = double.Parse(dt.Rows[i]["JEGOQTY"].ToString());
                row["DANGA"]     = double.Parse(dt.Rows[i]["DANGA"].ToString());
                row["BOKAMT"]    = double.Parse(dt.Rows[i]["BOKAMT"].ToString());
                row["BIGO"]      = dt.Rows[i]["BIGO"].ToString();

                double fdTOTAMT  = double.Parse(dt.Rows[i]["TOTAMT"].ToString());
                row["TOTAMT"]    = double.Parse(dt.Rows[i]["TOTAMT"].ToString());
                double fdTOTVAT  = double.Parse(dt.Rows[i]["TOTAMT"].ToString());
                fdTOTVAT         = (fdTOTVAT) - (fdTOTVAT % 10);
                fdTOTVAT         = fdTOTVAT / 10;
                row["TOTVAT"]    = fdTOTVAT;
                row["AMOUNT"]    = fdTOTAMT + fdTOTVAT;

                // 소스 추가 20081130
                row["BKYSDATE"]  = dt.Rows[i]["BKYSDATE"].ToString();

                retDt.Rows.Add(row);
            }

            return retDt;
        }
        #endregion
    }
}
