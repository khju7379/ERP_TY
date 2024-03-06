using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using TY.Service.Library;
using TY.Service.Library.Controls;
using GrapeCity.ActiveReports;

using Shoveling2010.SmartClient.SystemUtility;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Component;
using Shoveling2010.SmartClient.SystemUtility.Controls.SystemForm;

using TY.ER.GB00;

namespace TY.ER.UT00
{
    /// <summary>
    /// 통관화주별 재고조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.06.16 15:15
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_66FD4200 : 대표 거래처 코드 조회
    ///  TY_P_UT_66GF1282 : 통관화주별 재고조회(마스타)
    ///  TY_P_UT_66GF2283 : 통관화주별 재고조회(디테일)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_66GFA285 : 통관화주별 재고조회(마스타)
    ///  TY_S_UT_66GFB286 : 통관화주별 재고조회(디테일)
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_GB_2BF7Y364 : 조회가 완료되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  CNHWAJU : 화주
    ///  IPHANG : 접안일자
    ///  CJCUQTYTOT : 총 통관량
    ///  CUJEQTYTOT : 총 통관재고
    /// </summary>
    public partial class TYUTIN036S : TYBase
    {
        private int fiPage = 0;

        #region Description : 페이지 로드
        public TYUTIN036S()
        {
            InitializeComponent();
        }

        private void TYUTIN036S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_PRT.ProcessCheck += new TButton.CheckHandler(BTN61_PRT_ProcessCheck);

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            // 반입 조회
            UP_SEARCH_BANIP();

            // 미통관 조회
            UP_SEARCH_CUSTOM();

            // 재고조회
            UP_SEARCH_JEGO();
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            DataTable Rptdt = new DataTable();

            Rptdt = UP_Report_Datatable(ds.Tables[0]);

            if (Rptdt.Rows.Count > 0)
            {
                SectionReport rpt = new TYUTIN036R();
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait;
                (new TYERGB001P(rpt, Rptdt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Description : 반입 조회
        private void UP_SEARCH_BANIP()
        {
            string sSTDATE = string.Empty;
            string sEDDATE = string.Empty;
            string sYY = string.Empty;
            string sMM = string.Empty;
            string sDD = string.Empty;

            sSTDATE = Get_Date(DateTime.Now.ToString("yyyyMMdd"));

            if (int.Parse(sSTDATE.Substring(6, 2).ToString()) <= 11)
            {
                sDD = "20";
            }
            else
            {
                sDD = Convert.ToString(int.Parse(sSTDATE.Substring(6, 2).ToString()) - 10);
            }

            sYY = sSTDATE.Substring(0, 4).ToString();
            sMM = sSTDATE.Substring(4, 2).ToString();
            if (sMM == "01")
            {
                sMM = "12";
                sYY = Convert.ToString(int.Parse(sYY.ToString()) - 1);
            }

            sSTDATE = sYY.ToString() + sMM.ToString() + Set_Fill2(sDD.ToString());
            sEDDATE = Get_Date(DateTime.Now.ToString("yyyyMMdd"));

            this.FPS91_TY_S_UT_7C7FY210.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_7C7FW209", sSTDATE.ToString(),
                                                        sEDDATE.ToString());

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_7C7FY210.SetValue(dt);
        }
        #endregion

        #region Description : 입항일자로부터 5개월 이상된 미통관 조회
        private void UP_SEARCH_CUSTOM()
        {
            string sDATE = string.Empty;

            sDATE = Get_Date(DateTime.Now.AddMonths(-5).ToString("yyyyMMdd"));

            DataTable dt = new DataTable();
            DataTable retDT = new DataTable();

            this.FPS91_TY_S_UT_7C7GA213.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_7C7G7211", sDATE.ToString());

            dt = this.DbConnector.ExecuteDataTable();

            retDT = UP_ChangeDatatable(dt);

            this.FPS91_TY_S_UT_7C7GA213.SetValue(retDT);
        }
        #endregion

        #region Description : 통관일자로부터 5개월 이상된 재고 조회
        private void UP_SEARCH_JEGO()
        {
            string sDATE = string.Empty;

            sDATE = Get_Date(DateTime.Now.AddMonths(-5).ToString("yyyyMMdd"));

            DataTable dt = new DataTable();

            this.FPS91_TY_S_UT_7C7GH215.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_867HB191", sDATE.ToString());

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_7C7GH215.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_UT_7C7GH215.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_UT_7C7GH215.GetValue(i, "EDAAPVALNO").ToString() != "")
                    {
                        //this.FPS91_TY_S_UT_7C7GH215.ActiveSheet.Rows[i].BackColor = Color.DodgerBlue;                      

                        for (int j = 0; j < this.FPS91_TY_S_UT_7C7GH215.ActiveSheet.ColumnCount; j++)
                        {
                            this.FPS91_TY_S_UT_7C7GH215.ActiveSheet.Cells[i, j].ForeColor = Color.Red;
                        }
                    }
                }
            }
        }
        #endregion

        #region Description : 데이터 테이블 변경
        private DataTable UP_ChangeDatatable(DataTable dt)
        {
            string sIPIPHANG = string.Empty;
            string sIPBONSUN = string.Empty;
            string sIPHWAJU = string.Empty;
            string sIPHWAMUL = string.Empty;
            string sIPBLNO = string.Empty;
            string sIPMSNSEQ = string.Empty;
            string sIPHSNSEQ = string.Empty;
            double dCSJEQTY = 0;
            double dJUNIPMTQTY = 0;

            string sGUBUN = string.Empty;

            string sDate = string.Empty;

            DataTable dt1 = new DataTable();

            DataTable Table = new DataTable();

            DataRow row;

            Table.Columns.Add("VSJUKHA1", typeof(System.String));
            Table.Columns.Add("MSNHSN", typeof(System.String));
            Table.Columns.Add("CMBOGODAT", typeof(System.String));
            Table.Columns.Add("IPIPHANG", typeof(System.String));
            Table.Columns.Add("VSDESC1", typeof(System.String));
            Table.Columns.Add("HJDESC1", typeof(System.String));
            Table.Columns.Add("HMDESC1", typeof(System.String));
            Table.Columns.Add("IPBLNO", typeof(System.String));
            Table.Columns.Add("IPBSQTY", typeof(System.String));

            Table.Columns.Add("IPMTQTY", typeof(System.String));
            Table.Columns.Add("CSCUQTY", typeof(System.String));
            Table.Columns.Add("JUNIPMTQTY", typeof(System.String));
            Table.Columns.Add("CSJEQTY", typeof(System.String));

            Table.Columns.Add("VSJUKHA", typeof(System.String));
            Table.Columns.Add("IPHWAJU", typeof(System.String));
            Table.Columns.Add("IPBONSUN", typeof(System.String));

            Table.Columns.Add("NAME", typeof(System.String));
            Table.Columns.Add("NOWDATE", typeof(System.String));
            Table.Columns.Add("INFORDATE", typeof(System.String));
            Table.Columns.Add("SITENAME", typeof(System.String));


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sGUBUN = "";

                sIPIPHANG = SetDefaultValue(dt.Rows[i]["IPIPHANG"].ToString()).Trim();
                sIPBONSUN = SetDefaultValue(dt.Rows[i]["IPBONSUN"].ToString()).Trim();
                sIPHWAJU = SetDefaultValue(dt.Rows[i]["IPHWAJU"].ToString()).Trim();
                sIPHWAMUL = SetDefaultValue(dt.Rows[i]["IPHWAMUL"].ToString()).Trim();
                sIPBLNO = SetDefaultValue(dt.Rows[i]["IPBLNO"].ToString()).Trim();
                sIPMSNSEQ = SetDefaultValue(dt.Rows[i]["IPMSNSEQ"].ToString()).Trim();
                sIPHSNSEQ = SetDefaultValue(dt.Rows[i]["IPHSNSEQ"].ToString()).Trim();
                dCSJEQTY = double.Parse(String.Format("{0,9:N3}", SetDefaultValue(dt.Rows[i]["CSJEQTY"].ToString()).Trim()));

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_7C7G1212", sIPIPHANG.ToString(),
                                                            sIPBONSUN.ToString(),
                                                            sIPHWAJU.ToString(),
                                                            sIPHWAMUL.ToString(),
                                                            sIPBLNO.ToString(),
                                                            sIPMSNSEQ.ToString(),
                                                            sIPHSNSEQ.ToString()
                                                            );

                dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count > 0)
                {
                    dJUNIPMTQTY = double.Parse(String.Format("{0,9:N3}", SetDefaultValue(dt1.Rows[0][0].ToString()).Trim()));
                }

                if (dCSJEQTY - dJUNIPMTQTY > 0)
                {
                    row = Table.NewRow();

                    row["VSJUKHA1"] = SetDefaultValue(dt.Rows[i]["VSJUKHA1"].ToString()).Trim();
                    row["CMBOGODAT"] = SetDefaultValue(dt.Rows[i]["CMBOGODAT"].ToString()).Trim();
                    row["IPIPHANG"] = SetDefaultValue(dt.Rows[i]["IPIPHANG"].ToString()).Trim();
                    row["VSDESC1"] = SetDefaultValue(dt.Rows[i]["VSDESC1"].ToString()).Trim();
                    row["HJDESC1"] = SetDefaultValue(dt.Rows[i]["HJDESC1"].ToString()).Trim();
                    row["HMDESC1"] = SetDefaultValue(dt.Rows[i]["HMDESC1"].ToString()).Trim();
                    row["IPBLNO"] = SetDefaultValue(dt.Rows[i]["IPBLNO"].ToString()).Trim();
                    row["IPBSQTY"] = SetDefaultValue(dt.Rows[i]["IPBSQTY"].ToString()).Trim();

                    row["IPMTQTY"] = SetDefaultValue(dt.Rows[i]["IPMTQTY"].ToString()).Trim();
                    row["CSCUQTY"] = SetDefaultValue(dt.Rows[i]["CSCUQTY"].ToString()).Trim();
                    row["JUNIPMTQTY"] = Convert.ToString(dJUNIPMTQTY);
                    row["CSJEQTY"] = String.Format("{0,9:N3}", double.Parse(SetDefaultValue(dt.Rows[i]["CSJEQTY"].ToString()).Trim()) - dJUNIPMTQTY);

                    row["VSJUKHA"] = SetDefaultValue(dt.Rows[i]["VSJUKHA"].ToString()).Trim();
                    row["IPHWAJU"] = SetDefaultValue(dt.Rows[i]["IPHWAJU"].ToString()).Trim();
                    row["IPBONSUN"] = SetDefaultValue(dt.Rows[i]["IPBONSUN"].ToString()).Trim();
                    row["SITENAME"] = SetDefaultValue(dt.Rows[i]["SITENAME"].ToString()).Trim();

                    row["NAME"] = this.TXT01_KBHANGL.GetValue().ToString();
                    row["NOWDATE"] = Get_Date(DateTime.Now.ToString("yyyyMMdd"));

                    sDate = SetDefaultValue(dt.Rows[i]["CMBOGODAT"].ToString()).Trim();

                    DateTime dDateTime1 = new DateTime(Convert.ToInt16(sDate.Substring(0, 4)), Convert.ToInt16(sDate.Substring(4, 2)), Convert.ToInt16(sDate.Substring(6, 2)));
                    sDate = dDateTime1.AddMonths(6).ToString("yyyyMMdd");

                    DateTime dDateTime2 = new DateTime(Convert.ToInt16(sDate.Substring(0, 4)), Convert.ToInt16(sDate.Substring(4, 2)), Convert.ToInt16(sDate.Substring(6, 2)));
                    sDate = dDateTime2.AddDays(-1).ToString("yyyyMMdd");

                    row["INFORDATE"] = sDate.ToString();

                    Table.Rows.Add(row);
                }
            }

            return Table;
        }
        #endregion

        #region Description : 출력 데이터 테이블 변경
        private DataTable UP_Report_Datatable(DataTable dt)
        {
            DataTable Table = new DataTable();

            DataRow row;

            Table.Columns.Add("HJDESC1", typeof(System.String));
            Table.Columns.Add("NAME", typeof(System.String));
            Table.Columns.Add("INFORDATE", typeof(System.String));
            Table.Columns.Add("VSJUKHA", typeof(System.String));
            Table.Columns.Add("CMBOGODAT", typeof(System.String));
            Table.Columns.Add("VSDESC1", typeof(System.String));
            Table.Columns.Add("IPBLNO", typeof(System.String));
            Table.Columns.Add("HMDESC1", typeof(System.String));
            Table.Columns.Add("CSJEQTY", typeof(System.String));
            Table.Columns.Add("SITENAME", typeof(System.String));
            Table.Columns.Add("NOWDATE", typeof(System.String));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row = Table.NewRow();

                row["HJDESC1"] = SetDefaultValue(dt.Rows[i]["HJDESC1"].ToString()).Trim();
                row["NAME"] = this.TXT01_KBHANGL.GetValue().ToString();
                row["INFORDATE"] = SetDefaultValue(dt.Rows[i]["INFORDATE"].ToString()).Trim();
                row["VSJUKHA"] = SetDefaultValue(dt.Rows[i]["VSJUKHA1"].ToString()).Trim();
                row["CMBOGODAT"] = Set_Date(SetDefaultValue(dt.Rows[i]["CMBOGODAT"].ToString()).Trim());
                row["VSDESC1"] = SetDefaultValue(dt.Rows[i]["VSDESC1"].ToString()).Trim();
                row["IPBLNO"] = SetDefaultValue(dt.Rows[i]["IPBLNO"].ToString()).Trim();
                row["HMDESC1"] = SetDefaultValue(dt.Rows[i]["HMDESC1"].ToString()).Trim();
                row["CSJEQTY"] = String.Format("{0,9:N3}", double.Parse(SetDefaultValue(dt.Rows[i]["CSJEQTY"].ToString()).Trim()));
                row["SITENAME"] = SetDefaultValue(dt.Rows[i]["SITENAME"].ToString()).Trim();
                row["NOWDATE"] = Get_Date(DateTime.Now.ToString("yyyyMMdd"));

                Table.Rows.Add(row);
            }

            return Table;
        }
        #endregion

        #region Description : 입항관리 출력 ProcessCheck
        private void BTN61_PRT_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sVSJUKHA = string.Empty;
            string sCMBOGODAT = string.Empty;
            string sIPIPHANG = string.Empty;
            string sIPBONSUN = string.Empty;
            string sIPHWAJU = string.Empty;

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_UT_7C7GA213.GetDataSourceInclude(TSpread.TActionType.Select, "HJDESC1", "NAME", "INFORDATE", "VSJUKHA1", "CMBOGODAT", "VSDESC1", "IPBLNO", "HMDESC1", "CSJEQTY", "SITENAME", "NOWDATE", "IPIPHANG", "IPHWAJU", "IPBONSUN"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_25F59464");
                e.Successed = false;
                return;
            }

            if (ds.Tables[0].Rows.Count > 4)
            {
                this.ShowMessage("TY_M_UT_7CK9K335");
                e.Successed = false;
                return;
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    sVSJUKHA = ds.Tables[0].Rows[i]["VSJUKHA1"].ToString();
                    sCMBOGODAT = ds.Tables[0].Rows[i]["CMBOGODAT"].ToString();
                    sIPIPHANG = ds.Tables[0].Rows[i]["IPIPHANG"].ToString();
                    sIPBONSUN = ds.Tables[0].Rows[i]["IPBONSUN"].ToString();
                    sIPHWAJU = ds.Tables[0].Rows[i]["IPHWAJU"].ToString();
                }

                if (sVSJUKHA.ToString() != ds.Tables[0].Rows[i]["VSJUKHA1"].ToString() ||
                    sCMBOGODAT.ToString() != ds.Tables[0].Rows[i]["CMBOGODAT"].ToString() ||
                    sIPIPHANG.ToString() != ds.Tables[0].Rows[i]["IPIPHANG"].ToString() ||
                    sIPBONSUN.ToString() != ds.Tables[0].Rows[i]["IPBONSUN"].ToString() ||
                    sIPHWAJU.ToString() != ds.Tables[0].Rows[i]["IPHWAJU"].ToString()
                    )
                {
                    this.ShowMessage("TY_M_UT_7CK9M336");
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_2BN4U622"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 통관일자로부터 5개월 이상된 재고 스프레드 이벤트
        private void FPS91_TY_S_UT_7C7GH215_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sEADNUM = string.Empty;
            string sEDADATE = string.Empty;
            string sEDAEXTSDATE = string.Empty;
            string sEDAEXTEDATE = string.Empty;
            string sEDAAPVALNO = string.Empty;

            sEADNUM = "";
            sEDADATE = "";
            sEDAEXTSDATE = "";
            sEDAEXTEDATE = "";
            sEDAAPVALNO = "";


            sEADNUM = this.FPS91_TY_S_UT_7C7GH215.GetValue("EADNUM").ToString();
            sEDADATE = this.FPS91_TY_S_UT_7C7GH215.GetValue("EDADATE").ToString();
            sEDAEXTSDATE = this.FPS91_TY_S_UT_7C7GH215.GetValue("EDAEXTSDATE").ToString();
            sEDAEXTEDATE = this.FPS91_TY_S_UT_7C7GH215.GetValue("EDAEXTEDATE").ToString();
            sEDAAPVALNO = this.FPS91_TY_S_UT_7C7GH215.GetValue("EDAAPVALNO").ToString();

            if (sEADNUM.ToString() != "" && sEDADATE.ToString() != "" && sEDAEXTSDATE.ToString() != "" && sEDAEXTEDATE.ToString() != "" && sEDAAPVALNO.ToString() != "")
            {
                if (this.OpenModalPopup(new TYUTIN036I(sEADNUM, sEDADATE, sEDAEXTSDATE, sEDAEXTEDATE, sEDAAPVALNO)) != System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
            }
            else
            {
                string sCSIPHANG = string.Empty;
                string sCSBONSUN = string.Empty;
                string sCSHWAJU = string.Empty;
                string sCSHWAMUL = string.Empty;
                string sCSBLNO = string.Empty;
                string sCSMSNSEQ = string.Empty;
                string sCSHSNSEQ = string.Empty;
                string sCSCUSTIL = string.Empty;
                string sCSCHASU = string.Empty;

                sCSIPHANG = string.Empty;
                sCSBONSUN = string.Empty;
                sCSHWAJU = string.Empty;
                sCSHWAMUL = string.Empty;
                sCSBLNO = string.Empty;
                sCSMSNSEQ = string.Empty;
                sCSHSNSEQ = string.Empty;
                sCSCUSTIL = string.Empty;
                sCSCHASU = string.Empty;

                sCSIPHANG = this.FPS91_TY_S_UT_7C7GH215.GetValue("CSIPHANG").ToString();
                sCSBONSUN = this.FPS91_TY_S_UT_7C7GH215.GetValue("CSBONSUN").ToString();
                sCSHWAJU = this.FPS91_TY_S_UT_7C7GH215.GetValue("CSHWAJU").ToString();
                sCSHWAMUL = this.FPS91_TY_S_UT_7C7GH215.GetValue("CSHWAMUL").ToString();
                sCSBLNO = this.FPS91_TY_S_UT_7C7GH215.GetValue("CSBLNO").ToString();
                sCSMSNSEQ = this.FPS91_TY_S_UT_7C7GH215.GetValue("CSMSNSEQ").ToString();
                sCSHSNSEQ = this.FPS91_TY_S_UT_7C7GH215.GetValue("CSHSNSEQ").ToString();
                sCSCUSTIL = this.FPS91_TY_S_UT_7C7GH215.GetValue("CSCUSTIL").ToString();
                sCSCHASU = this.FPS91_TY_S_UT_7C7GH215.GetValue("CSCHASU").ToString();

                // 탭 메뉴를 열어주는 소스(form, 메뉴의 id, 탭 이름, 프로그램id)
                //TabPage_Add(new TYUTIN005I(sCSIPHANG, sCSBONSUN, sCSHWAJU, sCSHWAMUL, sCSBLNO, sCSMSNSEQ, sCSHSNSEQ, sCSCUSTIL, sCSCHASU, "EDI"), "TY66HF4298", "입항 및 통관관리", "TYUTIN005I");

                if (this.OpenModalPopup(new TYUTIN005I(sCSIPHANG, sCSBONSUN, sCSHWAJU, sCSHWAMUL, sCSBLNO, sCSMSNSEQ, sCSHSNSEQ, sCSCUSTIL, sCSCHASU, "EDI")) != System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion


    }
}
