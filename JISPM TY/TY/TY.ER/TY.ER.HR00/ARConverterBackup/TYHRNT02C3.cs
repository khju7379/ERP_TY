using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using System.IO;
using DataDynamics.ActiveReports;
using TY.ER.GB00;


namespace TY.ER.HR00
{
    /// <summary>
    /// 연말정산 원천징수부 및 영수증 출력 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2018.01.08 13:24
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  KBSABUN : 사번
    ///  INQOPTION2 : 조회구분
    ///  KBGUNMU : 근무처
    ///  SDATE : 시작일자
    ///  INQOPTION : 조회구분
    /// </summary>
    public partial class TYHRNT02C3 : TYBase
    {
        private string fsYACOMPANY;

        #region  Description : 폼 로드 이벤트
        public TYHRNT02C3(string sYACOMPANY)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsYACOMPANY = sYACOMPANY;
        }

        private void TYHRNT02C3_Load(object sender, System.EventArgs e)
        {
            this.BTN61_PRT.ProcessCheck += new TButton.CheckHandler(BTN61_PRT_ProcessCheck);

            CBO01_KBGUNMU.SetValue("1");

            TXT01_SDATE.SetValue(DateTime.Now.ToString("yyyy"));
            
        }
        #endregion      

        #region  Description : 연말정산 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            if (CBO01_INQOPTION.GetValue().ToString() == "1")
            {
                //공제신고서
                
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_7CLI5349", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", fsYACOMPANY, TXT01_SDATE.GetValue().ToString(), this.CBH01_KBSABUN.GetValue().ToString());
                DataTable dMaster = this.DbConnector.ExecuteDataTable();

                //부양가족
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_7CREZ376", fsYACOMPANY, TXT01_SDATE.GetValue().ToString(), this.CBH01_KBSABUN.GetValue().ToString(), TYUserInfo.SecureKey, "Y");
                DataTable dFamy = this.DbConnector.ExecuteDataTable();

                ActiveReport rpt = new TYHRNT002R1(dFamy);

                rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Default;

                (new TYERGB001P(rpt, dMaster)).ShowDialog();
            }
            else if (CBO01_INQOPTION.GetValue().ToString() == "2")
            {
                //원천징수 징수부                
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_83JHO718", TXT01_SDATE.GetValue().ToString(), this.CBH01_KBSABUN.GetValue().ToString(), TYUserInfo.SecureKey, "Y");
                DataTable dt = UP_Set_WonJinSuPrintForm(this.DbConnector.ExecuteDataTable());
                
                ActiveReport rpt = new TYHRNT003R3();

                rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Default;

                (new TYERGB001P(rpt, dt)).ShowDialog();

            }
            else
            {
                string sWNGUBN = string.Empty;
                string sKBGUNMU = string.Empty;

                if (this.CBH01_KBSABUN.GetValue().ToString() != "")
                {
                    sWNGUBN = "";
                    sKBGUNMU = "";
                }
                else
                {
                    sWNGUBN = CBO01_INQOPTION2.GetValue().ToString();
                    sKBGUNMU = CBO01_KBGUNMU.GetValue().ToString();
                }
                //원천징수 영수증(연말정산용)
                this.DbConnector.CommandClear();
                //this.DbConnector.Attach("TY_P_HR_7CT9B380", fsYACOMPANY, TXT01_SDATE.GetValue().ToString(), this.CBH01_KBSABUN.GetValue().ToString(), sWNGUBN, sKBGUNMU);
                this.DbConnector.Attach("TY_P_HR_7CT9B380", fsYACOMPANY, TXT01_SDATE.GetValue().ToString(), this.CBH01_KBSABUN.GetValue().ToString(), sWNGUBN, TYUserInfo.SecureKey, "Y");

                DataTable dt = this.DbConnector.ExecuteDataTable();

                ActiveReport rpt;

                if (Convert.ToInt16(TXT01_SDATE.GetValue().ToString()) > 2017)
                {
                    rpt = new TYHRNT003R4();
                }
                else
                {
                    rpt = new TYHRNT003R1();
                }

                rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Default;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
        }
        
        private void BTN61_PRT_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (CBO01_INQOPTION.GetValue().ToString() == "1")
            {
                //공제신고서
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_7CLI5349", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", fsYACOMPANY, TXT01_SDATE.GetValue().ToString(), this.CBH01_KBSABUN.GetValue().ToString());
                DataTable dMaster = this.DbConnector.ExecuteDataTable();

                if (dMaster.Rows.Count <= 0)
                {
                    this.ShowCustomMessage("출력할 자료가 존재하지 않습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return; 
                }
            }
            else if (CBO01_INQOPTION.GetValue().ToString() == "2")
            {
                //원천징수 영수증(총무용)
            }
            else
            {
                string sWNGUBN = string.Empty;
                string sKBGUNMU = string.Empty;

                if (this.CBH01_KBSABUN.GetValue().ToString() != "")
                {
                    sWNGUBN = "";
                    sKBGUNMU = "";
                }
                else
                {
                    sWNGUBN = CBO01_INQOPTION2.GetValue().ToString();
                    sKBGUNMU = CBO01_KBGUNMU.GetValue().ToString();
                }
                //원천징수 영수증(연말정산용)
                this.DbConnector.CommandClear();
                //this.DbConnector.Attach("TY_P_HR_7CT9B380", fsYACOMPANY, TXT01_SDATE.GetValue().ToString(), this.CBH01_KBSABUN.GetValue().ToString(), sWNGUBN, sKBGUNMU);
                this.DbConnector.Attach("TY_P_HR_7CT9B380", fsYACOMPANY, TXT01_SDATE.GetValue().ToString(), this.CBH01_KBSABUN.GetValue().ToString(), sWNGUBN, TYUserInfo.SecureKey, "Y");
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowCustomMessage("출력할 자료가 존재하지 않습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return; 
                }

            }
        }
        #endregion

        #region  Description : 원천징수부 출력 DataTable 변환 함수
        private DataTable UP_Set_WonJinSuPrintForm(DataTable dt)
        {
            double dM1PAYHAP = 0;
            double dS1PAYHAP = 0;
            double dYYMMHAP = 0;

            double dINCTAXHAP = 0;
            double dRESTAXHAP = 0;
            double dNATIONHAP = 0;
            double dHEALTHHAP = 0;
            double dEMPLOYHAP = 0;



            DataTable prtdt = new DataTable();

            prtdt.Columns.Add("WNYEAR", typeof(System.String));
            prtdt.Columns.Add("WNSABUN", typeof(System.String));
            prtdt.Columns.Add("WNNAME", typeof(System.String));
            prtdt.Columns.Add("WNSANGHO", typeof(System.String));
            prtdt.Columns.Add("WNSAUPNO", typeof(System.String));
            prtdt.Columns.Add("WNJUMIN", typeof(System.String));

            prtdt.Columns.Add("WRITEDATE", typeof(System.String));
            prtdt.Columns.Add("WNFAMCNT", typeof(System.String));

            prtdt.Columns.Add("YYMM1", typeof(System.String));
            prtdt.Columns.Add("YYMM2", typeof(System.String));
            prtdt.Columns.Add("YYMM3", typeof(System.String));
            prtdt.Columns.Add("YYMM4", typeof(System.String));
            prtdt.Columns.Add("YYMM5", typeof(System.String));
            prtdt.Columns.Add("YYMM6", typeof(System.String));
            prtdt.Columns.Add("YYMM7", typeof(System.String));
            prtdt.Columns.Add("YYMM8", typeof(System.String));
            prtdt.Columns.Add("YYMM9", typeof(System.String));
            prtdt.Columns.Add("YYMM10", typeof(System.String));
            prtdt.Columns.Add("YYMM11", typeof(System.String));
            prtdt.Columns.Add("YYMM12", typeof(System.String));

            prtdt.Columns.Add("M1PAY1", typeof(System.Double));
            prtdt.Columns.Add("M1PAY2", typeof(System.Double));
            prtdt.Columns.Add("M1PAY3", typeof(System.Double));
            prtdt.Columns.Add("M1PAY4", typeof(System.Double));
            prtdt.Columns.Add("M1PAY5", typeof(System.Double));
            prtdt.Columns.Add("M1PAY6", typeof(System.Double));
            prtdt.Columns.Add("M1PAY7", typeof(System.Double));
            prtdt.Columns.Add("M1PAY8", typeof(System.Double));
            prtdt.Columns.Add("M1PAY9", typeof(System.Double));
            prtdt.Columns.Add("M1PAY10", typeof(System.Double));
            prtdt.Columns.Add("M1PAY11", typeof(System.Double));
            prtdt.Columns.Add("M1PAY12", typeof(System.Double));

            prtdt.Columns.Add("M1PAYHAP", typeof(System.Double));

            prtdt.Columns.Add("S1PAY1", typeof(System.Double));
            prtdt.Columns.Add("S1PAY2", typeof(System.Double));
            prtdt.Columns.Add("S1PAY3", typeof(System.Double));
            prtdt.Columns.Add("S1PAY4", typeof(System.Double));
            prtdt.Columns.Add("S1PAY5", typeof(System.Double));
            prtdt.Columns.Add("S1PAY6", typeof(System.Double));
            prtdt.Columns.Add("S1PAY7", typeof(System.Double));
            prtdt.Columns.Add("S1PAY8", typeof(System.Double));
            prtdt.Columns.Add("S1PAY9", typeof(System.Double));
            prtdt.Columns.Add("S1PAY10", typeof(System.Double));
            prtdt.Columns.Add("S1PAY11", typeof(System.Double));
            prtdt.Columns.Add("S1PAY12", typeof(System.Double));

            prtdt.Columns.Add("S1PAYHAP", typeof(System.Double));

            prtdt.Columns.Add("YYMMTOTAL1", typeof(System.Double));
            prtdt.Columns.Add("YYMMTOTAL2", typeof(System.Double));
            prtdt.Columns.Add("YYMMTOTAL3", typeof(System.Double));
            prtdt.Columns.Add("YYMMTOTAL4", typeof(System.Double));
            prtdt.Columns.Add("YYMMTOTAL5", typeof(System.Double));
            prtdt.Columns.Add("YYMMTOTAL6", typeof(System.Double));
            prtdt.Columns.Add("YYMMTOTAL7", typeof(System.Double));
            prtdt.Columns.Add("YYMMTOTAL8", typeof(System.Double));
            prtdt.Columns.Add("YYMMTOTAL9", typeof(System.Double));
            prtdt.Columns.Add("YYMMTOTAL10", typeof(System.Double));
            prtdt.Columns.Add("YYMMTOTAL11", typeof(System.Double));
            prtdt.Columns.Add("YYMMTOTAL12", typeof(System.Double));

            prtdt.Columns.Add("YYMMHAP", typeof(System.Double));


            prtdt.Columns.Add("INCTAX1", typeof(System.Double));
            prtdt.Columns.Add("INCTAX2", typeof(System.Double));
            prtdt.Columns.Add("INCTAX3", typeof(System.Double));
            prtdt.Columns.Add("INCTAX4", typeof(System.Double));
            prtdt.Columns.Add("INCTAX5", typeof(System.Double));
            prtdt.Columns.Add("INCTAX6", typeof(System.Double));
            prtdt.Columns.Add("INCTAX7", typeof(System.Double));
            prtdt.Columns.Add("INCTAX8", typeof(System.Double));
            prtdt.Columns.Add("INCTAX9", typeof(System.Double));
            prtdt.Columns.Add("INCTAX10", typeof(System.Double));
            prtdt.Columns.Add("INCTAX11", typeof(System.Double));
            prtdt.Columns.Add("INCTAX12", typeof(System.Double));

            prtdt.Columns.Add("INCTAXHAP", typeof(System.Double));

            prtdt.Columns.Add("RESTAX1", typeof(System.Double));
            prtdt.Columns.Add("RESTAX2", typeof(System.Double));
            prtdt.Columns.Add("RESTAX3", typeof(System.Double));
            prtdt.Columns.Add("RESTAX4", typeof(System.Double));
            prtdt.Columns.Add("RESTAX5", typeof(System.Double));
            prtdt.Columns.Add("RESTAX6", typeof(System.Double));
            prtdt.Columns.Add("RESTAX7", typeof(System.Double));
            prtdt.Columns.Add("RESTAX8", typeof(System.Double));
            prtdt.Columns.Add("RESTAX9", typeof(System.Double));
            prtdt.Columns.Add("RESTAX10", typeof(System.Double));
            prtdt.Columns.Add("RESTAX11", typeof(System.Double));
            prtdt.Columns.Add("RESTAX12", typeof(System.Double));

            prtdt.Columns.Add("RESTAXHAP", typeof(System.Double));

            
            prtdt.Columns.Add("NATION1", typeof(System.Double));
            prtdt.Columns.Add("NATION2", typeof(System.Double));
            prtdt.Columns.Add("NATION3", typeof(System.Double));
            prtdt.Columns.Add("NATION4", typeof(System.Double));
            prtdt.Columns.Add("NATION5", typeof(System.Double));
            prtdt.Columns.Add("NATION6", typeof(System.Double));
            prtdt.Columns.Add("NATION7", typeof(System.Double));
            prtdt.Columns.Add("NATION8", typeof(System.Double));
            prtdt.Columns.Add("NATION9", typeof(System.Double));
            prtdt.Columns.Add("NATION10", typeof(System.Double));
            prtdt.Columns.Add("NATION11", typeof(System.Double));
            prtdt.Columns.Add("NATION12", typeof(System.Double));

            prtdt.Columns.Add("NATIONHAP", typeof(System.Double));

            prtdt.Columns.Add("HEALTH1", typeof(System.Double));
            prtdt.Columns.Add("HEALTH2", typeof(System.Double));
            prtdt.Columns.Add("HEALTH3", typeof(System.Double));
            prtdt.Columns.Add("HEALTH4", typeof(System.Double));
            prtdt.Columns.Add("HEALTH5", typeof(System.Double));
            prtdt.Columns.Add("HEALTH6", typeof(System.Double));
            prtdt.Columns.Add("HEALTH7", typeof(System.Double));
            prtdt.Columns.Add("HEALTH8", typeof(System.Double));
            prtdt.Columns.Add("HEALTH9", typeof(System.Double));
            prtdt.Columns.Add("HEALTH10", typeof(System.Double));
            prtdt.Columns.Add("HEALTH11", typeof(System.Double));
            prtdt.Columns.Add("HEALTH12", typeof(System.Double));

            prtdt.Columns.Add("HEALTHHAP", typeof(System.Double));

            prtdt.Columns.Add("EMPLOY1", typeof(System.Double));
            prtdt.Columns.Add("EMPLOY2", typeof(System.Double));
            prtdt.Columns.Add("EMPLOY3", typeof(System.Double));
            prtdt.Columns.Add("EMPLOY4", typeof(System.Double));
            prtdt.Columns.Add("EMPLOY5", typeof(System.Double));
            prtdt.Columns.Add("EMPLOY6", typeof(System.Double));
            prtdt.Columns.Add("EMPLOY7", typeof(System.Double));
            prtdt.Columns.Add("EMPLOY8", typeof(System.Double));
            prtdt.Columns.Add("EMPLOY9", typeof(System.Double));
            prtdt.Columns.Add("EMPLOY10", typeof(System.Double));
            prtdt.Columns.Add("EMPLOY11", typeof(System.Double));
            prtdt.Columns.Add("EMPLOY12", typeof(System.Double));

            prtdt.Columns.Add("EMPLOYHAP", typeof(System.Double));

            prtdt.TableName = "TableNames";

            DataRow rw;
            rw = prtdt.NewRow();

            rw["WNYEAR"] = dt.Rows[0]["YEAR"].ToString();
            rw["WNSABUN"] = dt.Rows[0]["KBSABUN"].ToString();
            rw["WNNAME"] = dt.Rows[0]["KBHANGL"].ToString();
            rw["WNSANGHO"] = dt.Rows[0]["KBSANGHO"].ToString();
            rw["WNSAUPNO"] = dt.Rows[0]["KBSAUPNO"].ToString();
            rw["WNJUMIN"] = dt.Rows[0]["KBJUMIN"].ToString();
            rw["WRITEDATE"] = dt.Rows[0]["WNEDATE"].ToString().Substring(0, 4) + "년 " + dt.Rows[0]["WNEDATE"].ToString().Substring(4, 2) + "월 " + dt.Rows[0]["WNEDATE"].ToString().Substring(6, 2) + "일";
            rw["WNFAMCNT"] = dt.Rows[0]["WNFAMCNT"].ToString();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                switch (dt.Rows[i]["PSYYMM"].ToString().Substring(4,2))
                {
                    case "01":
                        rw["YYMM1"] = dt.Rows[i]["PSYYMM"].ToString();
                        rw["M1PAY1"] = Convert.ToDouble(dt.Rows[i]["MPAYTOTAL"].ToString());
                        rw["S1PAY1"] = Convert.ToDouble(dt.Rows[i]["SPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["TPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["GITAINCOME"].ToString());
                        rw["YYMMTOTAL1"] = Convert.ToDouble(dt.Rows[i]["MPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["SPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["TPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["GITAINCOME"].ToString());
                        rw["INCTAX1"] = dt.Rows[i]["INCOMETAX"].ToString();
                        rw["RESTAX1"] = dt.Rows[i]["JUMINTAX"].ToString();
                        rw["NATION1"] = dt.Rows[i]["NATIONTAX"].ToString();
                        rw["HEALTH1"] = dt.Rows[i]["HEALTHTAX"].ToString();
                        rw["EMPLOY1"] = dt.Rows[i]["EMPLOYTAX"].ToString();
                        break;
                    case "02":
                        rw["YYMM2"] = dt.Rows[i]["PSYYMM"].ToString();
                        rw["M1PAY2"] = Convert.ToDouble(dt.Rows[i]["MPAYTOTAL"].ToString());
                        rw["S1PAY2"] = Convert.ToDouble(dt.Rows[i]["SPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["TPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["GITAINCOME"].ToString());
                        rw["YYMMTOTAL2"] = Convert.ToDouble(dt.Rows[i]["MPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["SPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["TPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["GITAINCOME"].ToString());
                        rw["INCTAX2"] = dt.Rows[i]["INCOMETAX"].ToString();
                        rw["RESTAX2"] = dt.Rows[i]["JUMINTAX"].ToString();
                        rw["NATION2"] = dt.Rows[i]["NATIONTAX"].ToString();
                        rw["HEALTH2"] = dt.Rows[i]["HEALTHTAX"].ToString();
                        rw["EMPLOY2"] = dt.Rows[i]["EMPLOYTAX"].ToString();
                        break;
                    case "03":
                        rw["YYMM3"] = dt.Rows[i]["PSYYMM"].ToString();
                        rw["M1PAY3"] = Convert.ToDouble(dt.Rows[i]["MPAYTOTAL"].ToString());
                        rw["S1PAY3"] = Convert.ToDouble(dt.Rows[i]["SPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["TPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["GITAINCOME"].ToString());
                        rw["YYMMTOTAL3"] = Convert.ToDouble(dt.Rows[i]["MPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["SPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["TPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["GITAINCOME"].ToString());
                        rw["INCTAX3"] = dt.Rows[i]["INCOMETAX"].ToString();
                        rw["RESTAX3"] = dt.Rows[i]["JUMINTAX"].ToString();
                        rw["NATION3"] = dt.Rows[i]["NATIONTAX"].ToString();
                        rw["HEALTH3"] = dt.Rows[i]["HEALTHTAX"].ToString();
                        rw["EMPLOY3"] = dt.Rows[i]["EMPLOYTAX"].ToString();
                        break;
                    case "04":
                        rw["YYMM4"] = dt.Rows[i]["PSYYMM"].ToString();
                        rw["M1PAY4"] = Convert.ToDouble(dt.Rows[i]["MPAYTOTAL"].ToString());
                        rw["S1PAY4"] = Convert.ToDouble(dt.Rows[i]["SPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["TPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["GITAINCOME"].ToString());
                        rw["YYMMTOTAL4"] = Convert.ToDouble(dt.Rows[i]["MPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["SPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["TPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["GITAINCOME"].ToString());
                        rw["INCTAX4"] = dt.Rows[i]["INCOMETAX"].ToString();
                        rw["RESTAX4"] = dt.Rows[i]["JUMINTAX"].ToString();
                        rw["NATION4"] = dt.Rows[i]["NATIONTAX"].ToString();
                        rw["HEALTH4"] = dt.Rows[i]["HEALTHTAX"].ToString();
                        rw["EMPLOY4"] = dt.Rows[i]["EMPLOYTAX"].ToString();
                        break;
                    case "05":
                        rw["YYMM5"] = dt.Rows[i]["PSYYMM"].ToString();
                        rw["M1PAY5"] = Convert.ToDouble(dt.Rows[i]["MPAYTOTAL"].ToString());
                        rw["S1PAY5"] = Convert.ToDouble(dt.Rows[i]["SPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["TPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["GITAINCOME"].ToString());
                        rw["YYMMTOTAL5"] = Convert.ToDouble(dt.Rows[i]["MPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["SPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["TPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["GITAINCOME"].ToString());
                        rw["INCTAX5"] = dt.Rows[i]["INCOMETAX"].ToString();
                        rw["RESTAX5"] = dt.Rows[i]["JUMINTAX"].ToString();
                        rw["NATION5"] = dt.Rows[i]["NATIONTAX"].ToString();
                        rw["HEALTH5"] = dt.Rows[i]["HEALTHTAX"].ToString();
                        rw["EMPLOY5"] = dt.Rows[i]["EMPLOYTAX"].ToString();
                        break;
                    case "06":
                        rw["YYMM6"] = dt.Rows[i]["PSYYMM"].ToString();
                        rw["M1PAY6"] = Convert.ToDouble(dt.Rows[i]["MPAYTOTAL"].ToString());
                        rw["S1PAY6"] = Convert.ToDouble(dt.Rows[i]["SPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["TPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["GITAINCOME"].ToString());
                        rw["YYMMTOTAL6"] = Convert.ToDouble(dt.Rows[i]["MPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["SPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["TPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["GITAINCOME"].ToString());
                        rw["INCTAX6"] = dt.Rows[i]["INCOMETAX"].ToString();
                        rw["RESTAX6"] = dt.Rows[i]["JUMINTAX"].ToString();
                        rw["NATION6"] = dt.Rows[i]["NATIONTAX"].ToString();
                        rw["HEALTH6"] = dt.Rows[i]["HEALTHTAX"].ToString();
                        rw["EMPLOY6"] = dt.Rows[i]["EMPLOYTAX"].ToString();
                        break;
                    case "07":
                        rw["YYMM7"] = dt.Rows[i]["PSYYMM"].ToString();
                        rw["M1PAY7"] = Convert.ToDouble(dt.Rows[i]["MPAYTOTAL"].ToString());
                        rw["S1PAY7"] = Convert.ToDouble(dt.Rows[i]["SPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["TPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["GITAINCOME"].ToString());
                        rw["YYMMTOTAL7"] = Convert.ToDouble(dt.Rows[i]["MPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["SPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["TPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["GITAINCOME"].ToString());
                        rw["INCTAX7"] = dt.Rows[i]["INCOMETAX"].ToString();
                        rw["RESTAX7"] = dt.Rows[i]["JUMINTAX"].ToString();
                        rw["NATION7"] = dt.Rows[i]["NATIONTAX"].ToString();
                        rw["HEALTH7"] = dt.Rows[i]["HEALTHTAX"].ToString();
                        rw["EMPLOY7"] = dt.Rows[i]["EMPLOYTAX"].ToString();
                        break;
                    case "08":
                        rw["YYMM8"] = dt.Rows[i]["PSYYMM"].ToString();
                        rw["M1PAY8"] = Convert.ToDouble(dt.Rows[i]["MPAYTOTAL"].ToString());
                        rw["S1PAY8"] = Convert.ToDouble(dt.Rows[i]["SPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["TPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["GITAINCOME"].ToString());
                        rw["YYMMTOTAL8"] = Convert.ToDouble(dt.Rows[i]["MPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["SPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["TPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["GITAINCOME"].ToString());
                        rw["INCTAX8"] = dt.Rows[i]["INCOMETAX"].ToString();
                        rw["RESTAX8"] = dt.Rows[i]["JUMINTAX"].ToString();
                        rw["NATION8"] = dt.Rows[i]["NATIONTAX"].ToString();
                        rw["HEALTH8"] = dt.Rows[i]["HEALTHTAX"].ToString();
                        rw["EMPLOY8"] = dt.Rows[i]["EMPLOYTAX"].ToString();
                        break;
                    case "09":
                        rw["YYMM9"] = dt.Rows[i]["PSYYMM"].ToString();
                        rw["M1PAY9"] = Convert.ToDouble(dt.Rows[i]["MPAYTOTAL"].ToString());
                        rw["S1PAY9"] = Convert.ToDouble(dt.Rows[i]["SPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["TPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["GITAINCOME"].ToString());
                        rw["YYMMTOTAL9"] = Convert.ToDouble(dt.Rows[i]["MPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["SPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["TPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["GITAINCOME"].ToString());
                        rw["INCTAX9"] = dt.Rows[i]["INCOMETAX"].ToString();
                        rw["RESTAX9"] = dt.Rows[i]["JUMINTAX"].ToString();
                        rw["NATION9"] = dt.Rows[i]["NATIONTAX"].ToString();
                        rw["HEALTH9"] = dt.Rows[i]["HEALTHTAX"].ToString();
                        rw["EMPLOY9"] = dt.Rows[i]["EMPLOYTAX"].ToString();
                        break;
                    case "10":
                        rw["YYMM10"] = dt.Rows[i]["PSYYMM"].ToString();
                        rw["M1PAY10"] = Convert.ToDouble(dt.Rows[i]["MPAYTOTAL"].ToString());
                        rw["S1PAY10"] = Convert.ToDouble(dt.Rows[i]["SPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["TPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["GITAINCOME"].ToString());
                        rw["YYMMTOTAL10"] = Convert.ToDouble(dt.Rows[i]["MPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["SPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["TPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["GITAINCOME"].ToString());
                        rw["INCTAX10"] = dt.Rows[i]["INCOMETAX"].ToString();
                        rw["RESTAX10"] = dt.Rows[i]["JUMINTAX"].ToString();
                        rw["NATION10"] = dt.Rows[i]["NATIONTAX"].ToString();
                        rw["HEALTH10"] = dt.Rows[i]["HEALTHTAX"].ToString();
                        rw["EMPLOY10"] = dt.Rows[i]["EMPLOYTAX"].ToString();
                        break;
                    case "11":
                        rw["YYMM11"] = dt.Rows[i]["PSYYMM"].ToString();
                        rw["M1PAY11"] = Convert.ToDouble(dt.Rows[i]["MPAYTOTAL"].ToString());
                        rw["S1PAY11"] = Convert.ToDouble(dt.Rows[i]["SPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["TPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["GITAINCOME"].ToString());
                        rw["YYMMTOTAL11"] = Convert.ToDouble(dt.Rows[i]["MPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["SPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["TPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["GITAINCOME"].ToString());
                        rw["INCTAX11"] = dt.Rows[i]["INCOMETAX"].ToString();
                        rw["RESTAX11"] = dt.Rows[i]["JUMINTAX"].ToString();
                        rw["NATION11"] = dt.Rows[i]["NATIONTAX"].ToString();
                        rw["HEALTH11"] = dt.Rows[i]["HEALTHTAX"].ToString();
                        rw["EMPLOY11"] = dt.Rows[i]["EMPLOYTAX"].ToString();
                        break;
                    case "12":
                        rw["YYMM12"] = dt.Rows[i]["PSYYMM"].ToString();
                        rw["M1PAY12"] = Convert.ToDouble(dt.Rows[i]["MPAYTOTAL"].ToString());
                        rw["S1PAY12"] = Convert.ToDouble(dt.Rows[i]["SPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["TPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["GITAINCOME"].ToString());
                        rw["YYMMTOTAL12"] = Convert.ToDouble(dt.Rows[i]["MPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["SPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["TPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["GITAINCOME"].ToString());
                        rw["INCTAX12"] = dt.Rows[i]["INCOMETAX"].ToString();
                        rw["RESTAX12"] = dt.Rows[i]["JUMINTAX"].ToString();
                        rw["NATION12"] = dt.Rows[i]["NATIONTAX"].ToString();
                        rw["HEALTH12"] = dt.Rows[i]["HEALTHTAX"].ToString();
                        rw["EMPLOY12"] = dt.Rows[i]["EMPLOYTAX"].ToString();
                        break;
                }

                dM1PAYHAP += Convert.ToDouble(dt.Rows[i]["MPAYTOTAL"].ToString());
                dS1PAYHAP += Convert.ToDouble(dt.Rows[i]["SPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["TPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["GITAINCOME"].ToString());
                dYYMMHAP += Convert.ToDouble(dt.Rows[i]["MPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["SPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["TPAYTOTAL"].ToString()) + Convert.ToDouble(dt.Rows[i]["GITAINCOME"].ToString());

                dINCTAXHAP += Convert.ToDouble(dt.Rows[i]["INCOMETAX"].ToString());
                dRESTAXHAP += Convert.ToDouble(dt.Rows[i]["JUMINTAX"].ToString());
                dNATIONHAP += Convert.ToDouble(dt.Rows[i]["NATIONTAX"].ToString());
                dHEALTHHAP += Convert.ToDouble(dt.Rows[i]["HEALTHTAX"].ToString());
                dEMPLOYHAP += Convert.ToDouble(dt.Rows[i]["EMPLOYTAX"].ToString());                
              
            }

            rw["M1PAYHAP"] = dM1PAYHAP;
            rw["S1PAYHAP"] = dS1PAYHAP;
            rw["YYMMHAP"] = dYYMMHAP;
            rw["INCTAXHAP"] = dINCTAXHAP;
            rw["RESTAXHAP"] = dRESTAXHAP;
            rw["NATIONHAP"] = dNATIONHAP;
            rw["HEALTHHAP"] = dHEALTHHAP;
            rw["EMPLOYHAP"] = dEMPLOYHAP;

            prtdt.Rows.Add(rw);

            return prtdt;
        }
        #endregion

        #region  Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion    

       

       
    }
}
