using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using FarPoint.Win.Spread.CellType;
//using System.Net.Mail;
using System.Web.Mail;

namespace TY.ER.MR00
{
    /// <summary>
    /// 구매요청 총무승인관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2013.05.28 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_35S2H752 : 구매요청 승인 조회(요청일자기간)
    ///  TY_P_MR_35S2U753 : 구매요청 승인 조회
    ///  TY_P_MR_35S2V754 : 구매요청 승인 등록
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_35S37755 : 구매요청 승인조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_MR_2BF4Z352 : 처리 할 데이터가 없습니다.
    ///  TY_M_MR_2BF50353 : 처리하시겠습니까?
    ///  TY_M_MR_2BF50354 : 처리하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  INQ : 조회
    ///  PRS2000 : 승인자
    ///  PRS2010 : 승인구분(일괄승인:1, 승인:2, 미승인:3)
    ///  PRS1000 : 사업부
    ///  PRS1010 : PR(P)
    ///  PRS1020 : 년월
    ///  PRS1030 : 순서
    ///  PRS1040 : 구분(총무승인:M, 회계승인:A)
    ///  PRS1050 : 승인일자
    ///  PRS1060 : 승인순번
    ///  PRS2020 : 사유
    ///  PRSHISAB : 작업사번
    /// </summary>
    public partial class TYMRPR006I : TYBase
    {
        string fsPRM1000   = string.Empty;
        string fsPRM1010   = string.Empty;
        string fsPRM1020   = string.Empty;
        string fsPRM1030   = string.Empty;

        string fsPRS1040   = string.Empty;
        string fsPRS2010   = string.Empty;
        string fsPRS2020   = string.Empty;
        string fsPRS2010_Account = string.Empty;

        string fsPRM2040   = string.Empty;

        #region Description : 페이지 로드
        public TYMRPR006I()
        {
            InitializeComponent();

            fsPRS1040 = "M";

            // 구입처
            this.SetSpreadCodeHelper(this.FPS91_TY_S_MR_35S37755, "PRS2000", "KBHANGL", "PRS2000");
        }

        private void TYMRPR006I_Load(object sender, System.EventArgs e)
        {
            this.TXT01_PRM1000.SetReadOnly(true);
            this.TXT01_PRM1010.SetReadOnly(true);
            this.TXT01_PRM1020.SetReadOnly(true);
            this.TXT01_PRM1030.SetReadOnly(true);
            this.TXT02_PRM2120.SetReadOnly(true);

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.TXT01_PRS1010.SetReadOnly(true);

            this.TXT01_PRS1020.SetValue(DateTime.Now.ToString("yyyyMMdd").Substring(0, 6));

            //this.TXT01_GSTDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));
            //this.TXT01_GEDDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));

            SetStartingFocus(this.TXT01_PRS1000);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sPRMNUM = string.Empty;
            string sPRS2010 = string.Empty;

            this.TXT01_PRM1000.SetValue("");
            this.TXT01_PRM1010.SetValue("");
            this.TXT01_PRM1020.SetValue("");
            this.TXT01_PRM1030.SetValue("");
            this.TXT02_PRM2120.SetValue("");

            sPRMNUM = this.TXT01_PRS1000.GetValue().ToString() + this.TXT01_PRS1010.GetValue().ToString() + this.TXT01_PRS1020.GetValue().ToString() + this.TXT01_PRS1030.GetValue().ToString();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
               (
               "TY_P_MR_3649W809",
               sPRMNUM.ToString(),
               this.TXT01_GSTDATE.GetValue().ToString(),
               this.TXT01_GEDDATE.GetValue().ToString(),
               this.CBO01_PRS2010.GetValue().ToString()
               );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_MR_364AN810.SetValue(dt);

            DataTable dt1 = new DataTable();

            this.FPS91_TY_S_MR_35S37755.SetValue(dt1);

            //this.BTN61_BATCH.Visible = false;
        }
        #endregion

        #region Description : 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sPRSNUM  = string.Empty;
            string sPRS1060 = string.Empty;

            fsPRS2010_Account = "";
            fsPRS2010         = "";
            fsPRS2020         = "";
            
            // 요청번호
            sPRSNUM = this.TXT01_PRM1000.GetValue().ToString() + this.TXT01_PRM1010.GetValue().ToString() + this.TXT01_PRM1020.GetValue().ToString() + Set_Fill4(this.TXT01_PRM1030.GetValue().ToString());

            DataTable dt = new DataTable();

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                   (
                   "TY_P_MR_35S65761",
                   sPRSNUM.ToString(),
                   fsPRS1040.ToString(), // 총무승인구분
                   ds.Tables[0].Rows[i]["PRS1050"].ToString()
                   );
                
                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sPRS1060 = dt.Rows[0]["PRS1060"].ToString();
                }

                fsPRS2010_Account = ds.Tables[0].Rows[i]["PRS2010"].ToString();

                // 승인구분
                if (ds.Tables[0].Rows[i]["PRS2010"].ToString() == "A")
                {
                    fsPRS2010 = "Y";
                }
                else
                {
                    fsPRS2010 = ds.Tables[0].Rows[i]["PRS2010"].ToString();
                }

                // 승인사유
                fsPRS2020 = ds.Tables[0].Rows[i]["PRS2020"].ToString();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_MR_35S2V754", this.TXT01_PRM1000.GetValue().ToString(),
                                                            this.TXT01_PRM1010.GetValue().ToString(),
                                                            this.TXT01_PRM1020.GetValue().ToString(),
                                                            this.TXT01_PRM1030.GetValue().ToString(),
                                                            fsPRS1040.ToString(),
                                                            ds.Tables[0].Rows[i]["PRS1050"].ToString(),
                                                            sPRS1060.ToString(),
                                                            ds.Tables[0].Rows[i]["PRS2000"].ToString(),
                                                            fsPRS2010.ToString(),
                                                            ds.Tables[0].Rows[i]["PRS2020"].ToString(),
                                                            TYUserInfo.EmpNo
                                                            ); //저장

                // 총무 승인
                //this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_MR_35T8Y767", fsPRS2010.ToString(),
                                                            ds.Tables[0].Rows[i]["PRS1050"].ToString(),
                                                            ds.Tables[0].Rows[i]["PRS2020"].ToString(),
                                                            this.TXT01_PRM1000.GetValue().ToString(),
                                                            this.TXT01_PRM1010.GetValue().ToString(),
                                                            this.TXT01_PRM1020.GetValue().ToString(),
                                                            this.TXT01_PRM1030.GetValue().ToString()
                                                            );

                // 일괄 승인
                if (ds.Tables[0].Rows[i]["PRS2010"].ToString() == "A")
                {
                    this.DbConnector.Attach("TY_P_MR_35S2V754", this.TXT01_PRM1000.GetValue().ToString(),
                                                                this.TXT01_PRM1010.GetValue().ToString(),
                                                                this.TXT01_PRM1020.GetValue().ToString(),
                                                                this.TXT01_PRM1030.GetValue().ToString(),
                                                                "A",
                                                                ds.Tables[0].Rows[i]["PRS1050"].ToString(),
                                                                sPRS1060.ToString(),
                                                                ds.Tables[0].Rows[i]["PRS2000"].ToString(),
                                                                fsPRS2010.ToString(),
                                                                ds.Tables[0].Rows[i]["PRS2020"].ToString(),
                                                                TYUserInfo.EmpNo
                                                                ); //저장

                    // 회계 승인
                    this.DbConnector.Attach("TY_P_MR_35TBH772", fsPRS2010.ToString(),
                                                                ds.Tables[0].Rows[i]["PRS1050"].ToString(),
                                                                ds.Tables[0].Rows[i]["PRS2020"].ToString(),
                                                                this.TXT01_PRM1000.GetValue().ToString(),
                                                                this.TXT01_PRM1010.GetValue().ToString(),
                                                                this.TXT01_PRM1020.GetValue().ToString(),
                                                                this.TXT01_PRM1030.GetValue().ToString()
                                                                );
                }
            }

            this.DbConnector.ExecuteTranQueryList();

            // 메일 발송
            UP_Mail_Send();

            this.ShowMessage("TY_M_MR_2BF50354"); // 저장완료 메세지

            // 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_35S2U753",
                sPRSNUM.ToString(),
                fsPRS1040.ToString(), // 총무승인구분
                ""
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_MR_35S37755.SetValue(dt);
            }
            else
            {
                this.FPS91_TY_S_MR_35S37755.SetValue(dt);
            }
        }
        #endregion

        #region Description : 메일 발송
        private void UP_Mail_Send()
        {
            string sFirst       = string.Empty;
            string sLine        = string.Empty;


            string sMailFrom    = string.Empty;
            string sMailFromNM  = string.Empty;
            string sMailTo      = string.Empty;
            string sMailToNM    = string.Empty;

            string sPRSNUM  = string.Empty;

            sPRSNUM = this.TXT01_PRM1000.GetValue().ToString() + this.TXT01_PRM1010.GetValue().ToString() + this.TXT01_PRM1020.GetValue().ToString() + Set_Fill4(this.TXT01_PRM1030.GetValue().ToString());

            DataTable dt = new DataTable();

            // 발신자
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_35T1I774",
                TYUserInfo.EmpNo
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sMailFrom   = dt.Rows[0]["KBMAILID"].ToString() + "@taeyoung.co.kr";
                sMailFromNM = dt.Rows[0]["KBHANGL"].ToString();
            }


            string sSEND = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_37HAZ135",
                sPRSNUM.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sSEND = dt.Rows[0]["PRM2040"].ToString();
            }

            // 수신자
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_35T1I774",
                sSEND.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sMailTo = dt.Rows[0]["KBMAILID"].ToString() + "@taeyoung.co.kr";
            }

            MailMessage mess = new MailMessage();

            // 메일 발신자
            //mess.From = sMailFrom.ToString();
            mess.From = "appr_admin@taeyoung.co.kr";

            // 메일 수신자
            if (fsPRS2010.ToString() != "N")
            {
                if (fsPRS2010_Account.ToString() != "A")
                {
                    sMailTo = sMailTo.ToString() + "," + "kskang@taeyoung.co.kr";
                }
            }
            mess.To = sMailTo.ToString();
            
            if (fsPRS2010.ToString() == "Y")
            {
                if (fsPRS2010_Account.ToString() == "A")
                {
                    mess.Subject = "총무 및 회계 일괄승인 완료 - 요청번호:" + sPRSNUM.ToString();
                }
                else
                {
                    mess.Subject = "총무승인 완료 - 요청번호:" + sPRSNUM.ToString();
                }
            }
            else
            {
                mess.Subject = "총무승인 취소 - 요청번호:" + sPRSNUM.ToString() + "," + "사유 : " + fsPRS2020.ToString();
            }

            sLine = "";
            sLine += "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' > ";
            sLine += "<HTML>";
            sLine += "<HEAD>";
            sLine += "<TITLE> New Document </TITLE>";
            sLine += "<style type='text/css'>";
            sLine += "<!-- /* Global Css */ ";
            sLine += "body {background-color:#ffffff;margin:0 10px 0 10px;}";
            sLine += "body,td,input,textarea,select{color:#333333;font-family:굴림,Gulim,sans-serif;font-size:12px}";
            sLine += "img{border:none} --> </style> </HEAD>";
            sLine += "<BODY>";
            sLine += "<table width= 400 cellspacing=2 cellpadding=1 border=0.5>";
            sLine += "<tr>";
            sLine += "      <td colspan=5 style='height: 12px'></td></tr>";
            sLine += "<tr>";
            sLine += "      <td colspan=4 bgcolor=#D6C7B5 style='height: 40px; font-weight: bold; font-size: 12pt; color: #cc0033; font-family: 굴림;' align='center'>";
            sLine += "      ERP 구매요청 승인제출";
            sLine += "      </td>";
            sLine += "</tr>";
            sLine += "<tr bgcolor=#E7E3DE align=center>";
            sLine += "   <td height=23 class='br01' style='padding:4 0 0 0; font-size: 9pt; width: 84px;'>요청번호</td>";
            sLine += "   <td height=23 class='br01' style='padding:4 0 0 0; font-size: 9pt; width: 150px;'>구매명</td>";
            sLine += "   <td class='br01' style='padding:4 0 0 0; font-size: 9pt;' colspan='2'>사유</td>";
            sLine += "</tr>";
            sLine += "<tr bgcolor=#F7F7F7 align=center>";
            sLine += "   <td height=23 class='br01' style='padding:4 0 0 0; font-size: 9pt; width: 84px;'> " + sPRSNUM.ToString() + "</td>";
            sLine += "   <td height=23 class='br01' style='padding:4 0 0 0; font-size: 9pt; width: 150px;'> " + this.TXT02_PRM2120.GetValue().ToString() + " </td>";
            sLine += "   <td class='br01' style='padding:4 0 0 0; font-size: 9pt;' colspan='2'> " + fsPRS2020.ToString() + "</td>";
            sLine += "</tr>";
            sLine += "</table>";
            sLine += "</BODY>";
            sLine += "</HTML> ";

            mess.Body = sLine;
            mess.BodyFormat = MailFormat.Html;

            SmtpMail.SmtpServer = "mail.taeyoung.co.kr";

            // 메일 발송
            SmtpMail.Send(mess);
        }
        #endregion

        #region Description : 처리 체크
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_MR_35S37755.GetDataSourceInclude(TSpread.TActionType.New, "PRS1050", "PRS2000", "PRS2010", "PRS2020"));

            if (ds.Tables[0].Rows.Count > 1)
            {
                this.ShowMessage("TY_M_MR_35T8V764");
                e.Successed = false;
                return;
            }

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_MR_2BF4Z352");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_MR_2BF50353"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 구매요청 조회 버튼
        private void BTN61_PRM10001_Click(object sender, EventArgs e)
        {
            string sPRS1020 = string.Empty;

            if (this.TXT01_PRS1020.GetValue().ToString() == "")
            {
                sPRS1020 = DateTime.Now.ToString("yyyyMMdd").Substring(0, 6);
            }
            else if (this.TXT01_PRS1020.GetValue().ToString() != "")
            {
                if (this.TXT01_PRS1020.GetValue().ToString().Length != 6)
                {
                    sPRS1020 = DateTime.Now.ToString("yyyyMMdd").Substring(0, 6);
                }
                else
                {
                    sPRS1020 = this.TXT01_PRS1020.GetValue().ToString();
                }
            }

            // 구매요청 코드헬프
            TYMZPR06C1 popup = new TYMZPR06C1(this.TXT01_PRS1000.GetValue().ToString(), sPRS1020.ToString(), sPRS1020.ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_PRS1000.SetValue(popup.fsPRM1000); // 사업부
                this.TXT01_PRS1020.SetValue(popup.fsPRM1020); // 년월
                this.TXT01_PRS1030.SetValue(popup.fsPRM1030); // 순서
                fsPRM2040 = popup.fsPRM2040;                  // 요청자
                this.TXT01_PRM2120.SetValue(popup.fsPRM2120); // 구매요청명

                SetFocus(this.TXT01_GSTDATE);
            }
        }
        #endregion

        #region Description : 구매요청 코드헬프 버튼
        private void TXT01_PRS1000_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                this.BTN61_PRM10001_Click(null, null);
            }
        }

        private void TXT01_PRS1020_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                this.BTN61_PRM10001_Click(null, null);
            }
        }

        private void TXT01_PRS1030_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                this.BTN61_PRM10001_Click(null, null);
            }
        }
        #endregion

        #region Description : 구매요청 마스터
        private void FPS91_TY_S_MR_364AN810_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sPRSNUM  = string.Empty;
            string sPRS2000 = string.Empty;
            string sKBHANGL = string.Empty;

            this.TXT01_PRM1000.SetValue("");
            this.TXT01_PRM1010.SetValue("P");
            this.TXT01_PRM1020.SetValue("");
            this.TXT01_PRM1030.SetValue("");
            this.TXT02_PRM2120.SetValue("");

            this.TXT01_PRM1000.SetValue(this.FPS91_TY_S_MR_364AN810.GetValue("PRM1000").ToString());
            this.TXT01_PRM1010.SetValue(this.FPS91_TY_S_MR_364AN810.GetValue("PRM1010").ToString());
            this.TXT01_PRM1020.SetValue(this.FPS91_TY_S_MR_364AN810.GetValue("PRM1020").ToString());
            this.TXT01_PRM1030.SetValue(this.FPS91_TY_S_MR_364AN810.GetValue("PRM1030").ToString());
            this.TXT02_PRM2120.SetValue(this.FPS91_TY_S_MR_364AN810.GetValue("PRM2120").ToString());

            // 요청번호
            sPRSNUM = this.TXT01_PRM1000.GetValue().ToString() + this.TXT01_PRM1010.GetValue().ToString() + this.TXT01_PRM1020.GetValue().ToString() + Set_Fill4(this.TXT01_PRM1030.GetValue().ToString());

            DataTable dt = new DataTable();

            // 발신자
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_35T1I774",
                TYUserInfo.EmpNo
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sKBHANGL = dt.Rows[0]["KBHANGL"].ToString();
            }

            sPRS2000 = TYUserInfo.EmpNo;


            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_35S2H752",
                sPRSNUM.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_MR_35S37755.SetValue(dt);
            }
            else
            {
                this.FPS91_TY_S_MR_35S37755.SetValue(dt);

                this.FPS91_TY_S_MR_35S37755_Sheet1.AddRows(0, 1);

                this.FPS91_TY_S_MR_35S37755.ActiveSheet.RowHeader.Cells[0, 0].Text = "N";

                this.FPS91_TY_S_MR_35S37755.SetValue(0, "PRS1000", this.TXT01_PRM1000.GetValue().ToString());
                this.FPS91_TY_S_MR_35S37755.SetValue(0, "PRS1010", this.TXT01_PRM1010.GetValue().ToString());
                this.FPS91_TY_S_MR_35S37755.SetValue(0, "PRS1020", this.TXT01_PRM1020.GetValue().ToString());
                this.FPS91_TY_S_MR_35S37755.SetValue(0, "PRS1030", this.TXT01_PRM1030.GetValue().ToString());
                this.FPS91_TY_S_MR_35S37755.SetValue(0, "PRS1040", fsPRS1040.ToString());
                this.FPS91_TY_S_MR_35S37755.SetValue(0, "PRS1050", DateTime.Now.ToString("yyyyMMdd").ToString());
                this.FPS91_TY_S_MR_35S37755.SetValue(0, "PRS2000", sPRS2000.ToString());
                this.FPS91_TY_S_MR_35S37755.SetValue(0, "KBHANGL", sKBHANGL.ToString());
                this.FPS91_TY_S_MR_35S37755.SetValue(0, "PRS2010", "Y");
            }

            //// 회계승인유무
            //if (this.FPS91_TY_S_MR_364AN810.GetValue("PRM7060").ToString() == "Y")
            //{
            //    this.BTN61_BATCH.Visible = false;
            //}
            //else
            //{
            //    this.BTN61_BATCH.Visible = true;
            //}

            this.BTN61_BATCH.Visible = true;
        }
        #endregion

        #region Description : 구매요청 바로가기
        private void FPS91_TY_S_MR_364AN810_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "12") // (구매요청 바로가기)
            {
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_35T3C776",
                    this.FPS91_TY_S_MR_364AN810.GetValue("PRM1000").ToString(),
                    this.FPS91_TY_S_MR_364AN810.GetValue("PRM1010").ToString(),
                    this.FPS91_TY_S_MR_364AN810.GetValue("PRM1020").ToString(),
                    this.FPS91_TY_S_MR_364AN810.GetValue("PRM1030").ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    if ((new TYMRPR001I(this.FPS91_TY_S_MR_364AN810.GetValue("PRM1000").ToString(), this.FPS91_TY_S_MR_364AN810.GetValue("PRM1010").ToString(),
                                        this.FPS91_TY_S_MR_364AN810.GetValue("PRM1020").ToString(), this.FPS91_TY_S_MR_364AN810.GetValue("PRM1030").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        this.BTN61_INQ_Click(null, null);
                }
                else
                {
                    this.ShowMessage("TY_M_MR_2C4AJ830");
                    return;
                }
            }
        }
        #endregion

        #region Description : 그리드 추가 이벤트
        private void FPS91_TY_S_MR_35S37755_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            if (this.TXT01_PRM1000.GetValue().ToString() == "" ||
                this.TXT01_PRM1010.GetValue().ToString() == "" ||
                this.TXT01_PRM1020.GetValue().ToString() == "" ||
                this.TXT01_PRM1030.GetValue().ToString() == ""
                )
            {
                this.ShowMessage("TY_M_MR_364BG811");
                return;
            }
            else
            {
                string sPRS2000 = string.Empty;
                string sKBHANGL = string.Empty;

                DataTable dt = new DataTable();

                // 발신자
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_35T1I774",
                    TYUserInfo.EmpNo
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sKBHANGL = dt.Rows[0]["KBHANGL"].ToString();
                }

                sPRS2000 = TYUserInfo.EmpNo;

                this.FPS91_TY_S_MR_35S37755.SetValue(e.RowIndex, "PRS1000", this.TXT01_PRM1000.GetValue().ToString());
                this.FPS91_TY_S_MR_35S37755.SetValue(e.RowIndex, "PRS1010", this.TXT01_PRM1010.GetValue().ToString());
                this.FPS91_TY_S_MR_35S37755.SetValue(e.RowIndex, "PRS1020", this.TXT01_PRM1020.GetValue().ToString());
                this.FPS91_TY_S_MR_35S37755.SetValue(e.RowIndex, "PRS1030", this.TXT01_PRM1030.GetValue().ToString());
                this.FPS91_TY_S_MR_35S37755.SetValue(e.RowIndex, "PRS1040", fsPRS1040.ToString());
                this.FPS91_TY_S_MR_35S37755.SetValue(e.RowIndex, "PRS1050", DateTime.Now.ToString("yyyyMMdd").ToString());
                this.FPS91_TY_S_MR_35S37755.SetValue(e.RowIndex, "PRS2000", sPRS2000.ToString());
                this.FPS91_TY_S_MR_35S37755.SetValue(e.RowIndex, "KBHANGL", sKBHANGL.ToString());
                this.FPS91_TY_S_MR_35S37755.SetValue(e.RowIndex, "PRS2010", "Y");
            }
        }
        #endregion

        //#region Description : 요청 바로가기
        //private void BTN61_DIRECT_Click(object sender, EventArgs e)
        //{
        //    if (this.TXT01_PRS1000.GetValue().ToString() == "" || this.TXT01_PRS1010.GetValue().ToString() == "" ||
        //        this.TXT01_PRS1020.GetValue().ToString() == "" || this.TXT01_PRS1030.GetValue().ToString() == "")
        //    {
        //        this.ShowMessage("TY_M_MR_2BK3D500");
        //        return;
        //    }
        //    else
        //    {
        //        DataTable dt = new DataTable();

        //        this.DbConnector.CommandClear();
        //        this.DbConnector.Attach
        //            (
        //            "TY_P_MR_35T3C776",
        //            this.TXT01_PRS1000.GetValue().ToString(),
        //            this.TXT01_PRS1010.GetValue().ToString(),
        //            this.TXT01_PRS1020.GetValue().ToString(),
        //            this.TXT01_PRS1030.GetValue().ToString()
        //            );

        //        dt = this.DbConnector.ExecuteDataTable();

        //        if (dt.Rows.Count > 0)
        //        {
        //            if ((new TYMRPR001I(this.TXT01_PRS1000.GetValue().ToString(), this.TXT01_PRS1010.GetValue().ToString(),
        //                                this.TXT01_PRS1020.GetValue().ToString(), this.TXT01_PRS1030.GetValue().ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //                this.BTN61_INQ_Click(null, null);
        //        }
        //        else
        //        {
        //            this.ShowMessage("TY_M_MR_2C4AJ830");
        //            return;
        //        }
        //    }
        //}
        //#endregion
    }
}