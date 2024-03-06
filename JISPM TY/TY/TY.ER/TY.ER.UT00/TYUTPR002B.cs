using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.UT00
{
    /// <summary>
    /// 재고대장 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.03.21 18:41
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_7338Y829 : 재고대장 SP
    ///  TY_P_UT_73DAJ890 : 재고대장 임시파일 삭제
    ///  TY_P_UT_73EHN940 : 재고대장 삭제
    ///  TY_P_UT_8AJA3989 : 재고대장 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_73LIX051 : 재고대장 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_MR_2BF50353 : 처리하시겠습니까?
    ///  TY_M_MR_2BF50354 : 처리하였습니다.
    ///  TY_M_UT_71BDP399 : 처리 중 오류가 발생하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  INQ : 조회
    ///  PRT : 출력
    ///  CHHWAJU : 화주
    ///  CHHWAMUL : 화물
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYUTPR002B : TYBase
    {
        #region Description : 폼 로드
        public TYUTPR002B()
        {
            InitializeComponent();
        }

        private void TYUTPR002B_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            //this.DbConnector.CommandClear();
            ////// 재고대장 임시파일 삭제
            //this.DbConnector.Attach("TY_P_UT_73EHN940");
            //this.DbConnector.ExecuteTranQueryList();
            // 재고대장 임시파일 삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_73DAJ890");
            this.DbConnector.ExecuteTranQueryList();

            this.DbConnector.CommandClear();
            // 재고대장 생성 SP 호출
            this.DbConnector.Attach("TY_P_UT_7338Y829",
                                    this.DTP01_STDATE.GetString(),
                                    this.DTP01_EDDATE.GetString(),
                                    "",
                                    "",
                                    "",
                                    "",
                                    TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                    ""
                                    );

            string sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            this.ShowMessage("TY_M_MR_2BF50354");
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sSTDATE = this.DTP01_STDATE.GetString();
            string sEDDATE = this.DTP01_EDDATE.GetString();
            string sDATE = "(" + sSTDATE.Substring(0, 4) + "/" + sSTDATE.Substring(4, 2) + "/" + sSTDATE.Substring(6, 2) + "-" + sEDDATE.Substring(0, 4) + "/" + sEDDATE.Substring(4, 2) + "/" + sEDDATE.Substring(6, 2) + ")";

            this.FPS91_TY_S_UT_73LIX051.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            if (this.CBO01_GGUBUN.GetValue().ToString() == "Y")
            {

                this.DbConnector.Attach("TY_P_UT_8AJA3989", sDATE,
                                                            this.CBH01_CHHWAJU.GetValue().ToString(),
                                                            this.CBH01_CHHWAMUL.GetValue().ToString()
                                                            );
            }
            else
            {
                this.DbConnector.Attach("TY_P_UT_81FG3455", sDATE,
                                                            this.CBH01_CHHWAJU.GetValue().ToString(),
                                                            this.CBH01_CHHWAMUL.GetValue().ToString()
                                                            );
            }

            this.FPS91_TY_S_UT_73LIX051.SetValue(QueryDataSetReport(this.DbConnector.ExecuteDataTable()));
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sSTDATE = this.DTP01_STDATE.GetString();
            string sEDDATE = this.DTP01_EDDATE.GetString();
            string sDATE = "(" + sSTDATE.Substring(0, 4) + "/" + sSTDATE.Substring(4, 2) + "/" + sSTDATE.Substring(6, 2) + "-" + sEDDATE.Substring(0, 4) + "/" + sEDDATE.Substring(4, 2) + "/" + sEDDATE.Substring(6, 2) + ")";

            if (this.CBO01_GGUBUN.GetValue().ToString() == "Y")
            {

                this.DbConnector.Attach("TY_P_UT_8AJA3989", sDATE,
                                                            this.CBH01_CHHWAJU.GetValue().ToString(),
                                                            this.CBH01_CHHWAMUL.GetValue().ToString()
                                                            );
            }
            else
            {
                this.DbConnector.Attach("TY_P_UT_81FG3455", sDATE,
                                                            this.CBH01_CHHWAJU.GetValue().ToString(),
                                                            this.CBH01_CHHWAMUL.GetValue().ToString()
                                                            );
            }
            //
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYUTPR002R();
                // 가로 출력
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, QueryDataSetReport(dt))).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Description : 데이터셋 변경
        private DataTable QueryDataSetReport(DataTable dt)
        {
            DataTable retDt = new DataTable();
            DataTable TnoDt = new DataTable();
            DataTable jegoDt = new DataTable();

            string sNEWJEHWAJU = string.Empty;
            string sOLDJEHWAJU = string.Empty;
            string sNEWVNSANGHO = string.Empty;
            string sOLDVNSANGHO = string.Empty;
            string sNEWJEHWAMUL = string.Empty;
            string sOLDJEHWAMUL = string.Empty;
            string sNEWHMDESC1 = string.Empty;
            string sOLDHMDESC1 = string.Empty;
            string sJEGUBUN = string.Empty;
            string sTANKNO = string.Empty;
            string gsMySql = string.Empty;
            string sSql = string.Empty;
            string sJEJOSTR = string.Empty;
            string sJEJOEND = string.Empty;
            string sJEOVSTR = string.Empty;
            string sJEOVEND = string.Empty;

            double dIPMTQTY = 0;
            double dIPKLQTY = 0;
            double dCHMTQTY = 0;
            double dCHKLQTY = 0;
            double dJEOVMT = 0;
            double dJEOVKL = 0;
            double dJEJEMT = 0;
            double dJEJEKL = 0;

            double dHAPIPMTQTY = 0;
            double dHAPIPKLQTY = 0;
            double dHAPCHMTQTY = 0;
            double dHAPCHKLQTY = 0;
            double dHAPJEOVMT = 0;
            double dHAPJEOVKL = 0;
            double dHAPJEJEMT = 0;
            double dHAPJEJEKL = 0;

            retDt.Columns.Add("JEHWAJU", typeof(System.String));
            retDt.Columns.Add("VNSANGHO", typeof(System.String));
            retDt.Columns.Add("JEHWAMUL", typeof(System.String));
            retDt.Columns.Add("HMDESC1", typeof(System.String));
            retDt.Columns.Add("JEGUBUN1", typeof(System.String));
            retDt.Columns.Add("JEDATE", typeof(System.String));
            retDt.Columns.Add("JETANKNO", typeof(System.String));
            retDt.Columns.Add("IPMTQTY", typeof(System.String));
            retDt.Columns.Add("IPKLQTY", typeof(System.String));
            retDt.Columns.Add("CHMTQTY", typeof(System.String));
            retDt.Columns.Add("CHKLQTY", typeof(System.String));
            retDt.Columns.Add("JEJEMT", typeof(System.String));
            retDt.Columns.Add("JEJEKL", typeof(System.String));
            retDt.Columns.Add("JEOVMT", typeof(System.String));
            retDt.Columns.Add("JEOVKL", typeof(System.String));
            retDt.Columns.Add("JEJOSTR", typeof(System.String));
            retDt.Columns.Add("JEJOEND", typeof(System.String));
            retDt.Columns.Add("JEOVSTR", typeof(System.String));
            retDt.Columns.Add("JEOVEND", typeof(System.String));
            retDt.Columns.Add("JEBONSUN", typeof(System.String));
            retDt.Columns.Add("JEMVTANK", typeof(System.String));
            retDt.Columns.Add("VSNM", typeof(System.String));
            retDt.Columns.Add("DATE", typeof(System.String));
            retDt.Columns.Add("JEGUBUN", typeof(System.String));
            retDt.Columns.Add("JEDESC1", typeof(System.String));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sNEWJEHWAJU = dt.Rows[i]["JEHWAJU"].ToString();
                sNEWVNSANGHO = dt.Rows[i]["VNSANGHO"].ToString();
                sNEWJEHWAMUL = dt.Rows[i]["JEHWAMUL"].ToString();
                sNEWHMDESC1 = dt.Rows[i]["HMDESC1"].ToString();

                if (i == 0)
                {
                    sOLDJEHWAJU = sNEWJEHWAJU.ToString();
                    sOLDVNSANGHO = sNEWVNSANGHO.ToString();
                    sOLDJEHWAMUL = sNEWJEHWAMUL.ToString();
                    sOLDHMDESC1 = sNEWHMDESC1.ToString();
                }

                dIPMTQTY = double.Parse(Get_Numeric(dt.Rows[i]["IPMTQTY"].ToString()));
                dIPKLQTY = double.Parse(Get_Numeric(dt.Rows[i]["IPKLQTY"].ToString()));
                dCHMTQTY = double.Parse(Get_Numeric(dt.Rows[i]["CHMTQTY"].ToString()));
                dCHKLQTY = double.Parse(Get_Numeric(dt.Rows[i]["CHKLQTY"].ToString()));
                dJEJEMT = double.Parse(Get_Numeric(dt.Rows[i]["JEJEMT"].ToString()));
                dJEJEKL = double.Parse(Get_Numeric(dt.Rows[i]["JEJEKL"].ToString()));
                dJEOVMT = double.Parse(Get_Numeric(dt.Rows[i]["JEOVMT"].ToString()));
                dJEOVKL = double.Parse(Get_Numeric(dt.Rows[i]["JEOVKL"].ToString()));

                if (dIPMTQTY + dIPKLQTY + dCHMTQTY + dCHKLQTY + dJEJEMT + dJEJEKL + dJEOVMT + dJEOVKL != 0)
                {

                    if (sNEWJEHWAJU.ToString() == sOLDJEHWAJU.ToString())
                    {
                        if (sNEWJEHWAMUL.ToString() == sOLDJEHWAMUL.ToString())
                        {
                            DataRow row = retDt.NewRow();
                            row["VNSANGHO"] = sOLDVNSANGHO.ToString();
                            row["HMDESC1"] = sOLDHMDESC1.ToString();
                            sJEGUBUN = dt.Rows[i]["JEGUBUN"].ToString();

                            row["JEDATE"] = dt.Rows[i]["JEDATE"].ToString();
                            row["JETANKNO"] = dt.Rows[i]["JETANKNO"].ToString();
                            row["IPMTQTY"] = double.Parse(Get_Numeric(dt.Rows[i]["IPMTQTY"].ToString()));
                            row["IPKLQTY"] = double.Parse(Get_Numeric(dt.Rows[i]["IPKLQTY"].ToString()));
                            row["CHMTQTY"] = double.Parse(Get_Numeric(dt.Rows[i]["CHMTQTY"].ToString()));
                            row["CHKLQTY"] = double.Parse(Get_Numeric(dt.Rows[i]["CHKLQTY"].ToString()));
                            row["JEJEMT"] = double.Parse(Get_Numeric(dt.Rows[i]["JEJEMT"].ToString()));
                            row["JEJEKL"] = double.Parse(Get_Numeric(dt.Rows[i]["JEJEKL"].ToString()));
                            row["JEOVMT"] = double.Parse(Get_Numeric(dt.Rows[i]["JEOVMT"].ToString()));
                            row["JEOVKL"] = double.Parse(Get_Numeric(dt.Rows[i]["JEOVKL"].ToString()));

                            sJEJOSTR = "";
                            sJEJOEND = "";
                            sJEOVSTR = "";
                            sJEOVEND = "";

                            if (dt.Rows[i]["JEJOSTR"].ToString() != "0")
                            {
                                sJEJOSTR = Set_Fill4(dt.Rows[i]["JEJOSTR"].ToString()).Substring(0, 2) + ":" + Set_Fill4(dt.Rows[i]["JEJOSTR"].ToString()).Substring(2, 2) + "~";
                            }
                            else
                            {
                                if (double.Parse(Get_Numeric(dt.Rows[i]["IPMTQTY"].ToString())) != 0)
                                {
                                    sJEJOSTR = Set_Fill4(dt.Rows[i]["JEJOSTR"].ToString()).Substring(0, 2) + ":" + Set_Fill4(dt.Rows[i]["JEJOSTR"].ToString()).Substring(2, 2) + "~";
                                }
                            }

                            if (dt.Rows[i]["JEJOEND"].ToString() != "0")
                            {
                                sJEJOEND = Set_Fill4(dt.Rows[i]["JEJOEND"].ToString()).Substring(0, 2) + ":" + Set_Fill4(dt.Rows[i]["JEJOEND"].ToString()).Substring(2, 2);
                            }

                            if (dt.Rows[i]["JEOVSTR"].ToString() != "0")
                            {
                                sJEOVSTR = Set_Fill4(dt.Rows[i]["JEOVSTR"].ToString()).Substring(0, 2) + ":" + Set_Fill4(dt.Rows[i]["JEOVSTR"].ToString()).Substring(2, 2) + "~";
                            }
                            else
                            {
                                if (double.Parse(Get_Numeric(dt.Rows[i]["IPMTQTY"].ToString())) != 0)
                                {
                                    sJEOVSTR = Set_Fill4(dt.Rows[i]["JEOVSTR"].ToString()).Substring(0, 2) + ":" + Set_Fill4(dt.Rows[i]["JEOVSTR"].ToString()).Substring(2, 2) + "~";
                                }
                            }

                            if (dt.Rows[i]["JEOVEND"].ToString() != "0")
                            {
                                sJEOVEND = Set_Fill4(dt.Rows[i]["JEOVEND"].ToString()).Substring(0, 2) + ":" + Set_Fill4(dt.Rows[i]["JEOVEND"].ToString()).Substring(2, 2);
                            }

                            row["JEJOSTR"] = sJEJOSTR.ToString() + sJEJOEND.ToString();
                            row["JEJOEND"] = "";
                            row["JEOVSTR"] = sJEOVSTR.ToString() + sJEOVEND.ToString();
                            row["JEOVEND"] = "";


                            if (sJEGUBUN == "6")
                            {
                                row["JEDESC1"] = "이고" + dt.Rows[i]["JEMVTANK"].ToString();
                            }
                            else if (sJEGUBUN == "2")
                            {
                                row["JEDESC1"] = "이고입고";
                            }
                            else if (sJEGUBUN != "1")
                            {
                                if (sJEGUBUN == "4")
                                {
                                    row["JEDESC1"] = "선적  (" + dt.Rows[i]["JEBONSUN"].ToString() + " )";
                                }
                                else if (sJEGUBUN == "8")
                                {
                                    row["JEDESC1"] = "재선적  (" + dt.Rows[i]["JEBONSUN"].ToString() + " )";
                                }
                                else if (sJEGUBUN == "5")
                                {
                                    row["JEDESC1"] = "송유";
                                }
                                else if (sJEGUBUN == "7")
                                {
                                    row["JEDESC1"] = "PIPE";
                                }
                                else if (sJEGUBUN == "9")
                                {
                                    row["JEDESC1"] = "ISOTANK";
                                }
                                else
                                {
                                    row["JEDESC1"] = "";
                                }
                            }
                            else
                            {
                                row["JEDESC1"] = dt.Rows[i]["JEBONSUN"].ToString();
                            }

                            row["DATE"] = dt.Rows[i]["DATE"].ToString();

                            retDt.Rows.Add(row);
                        }
                        else  // 화물이 다를 경우
                        {
                            // 화주,화물에 따른 탱크번호를 가져옴
                            //this.FPS91_TY_S_UT_73LIX051.Initialize();

                            this.DbConnector.Attach("TY_P_UT_73SGS121", sOLDJEHWAJU,
                                                                        sOLDJEHWAMUL
                                                                        );

                            TnoDt = this.DbConnector.ExecuteDataTable();

                            for (int j = 0; j < TnoDt.Rows.Count; j++)
                            {
                                // 화주,화물,탱크번호에 따른 총 입고,출고,할증(MT,K/L)량을 가져옴
                                //this.FPS91_TY_S_UT_73LIX051.Initialize();

                                this.DbConnector.Attach("TY_P_UT_73SHG122", sOLDJEHWAJU.ToString(),
                                                                            sOLDJEHWAMUL.ToString(),
                                                                            TnoDt.Rows[j]["JETANKNO"].ToString()
                                                                            );

                                jegoDt = this.DbConnector.ExecuteDataTable();


                                if (jegoDt.Rows.Count > 0)
                                {
                                    dIPMTQTY = double.Parse(Get_Numeric(jegoDt.Rows[0]["IPMTQTY"].ToString()));
                                    dIPKLQTY = double.Parse(Get_Numeric(jegoDt.Rows[0]["IPKLQTY"].ToString()));
                                    dCHMTQTY = double.Parse(Get_Numeric(jegoDt.Rows[0]["CHMTQTY"].ToString()));
                                    dCHKLQTY = double.Parse(Get_Numeric(jegoDt.Rows[0]["CHKLQTY"].ToString()));
                                    dJEOVMT = double.Parse(Get_Numeric(jegoDt.Rows[0]["JEOVMT"].ToString()));
                                    dJEOVKL = double.Parse(Get_Numeric(jegoDt.Rows[0]["JEOVKL"].ToString()));
                                }

                                // 화주,화물,탱크번호에 따른 마지막 재고(MT,K/L)량을 가져옴
                                //this.FPS91_TY_S_UT_73LIX051.Initialize();

                                this.DbConnector.Attach("TY_P_UT_8ACFZ941", sOLDJEHWAJU.ToString(),
                                                                            sOLDJEHWAMUL.ToString(),
                                                                            TnoDt.Rows[j]["JETANKNO"].ToString()
                                                                            );

                                jegoDt = this.DbConnector.ExecuteDataTable();

                                if (jegoDt.Rows.Count > 0)
                                {
                                    dJEJEMT = double.Parse(Get_Numeric(jegoDt.Rows[0]["JEJEMT"].ToString()));
                                    dJEJEKL = double.Parse(Get_Numeric(jegoDt.Rows[0]["JEJEKL"].ToString()));
                                }
                                else
                                {
                                    dJEJEMT = 0;
                                    dJEJEKL = 0;
                                }

                                //dJEJEMT = dJEJEMT + dIPMTQTY - dCHMTQTY;
                                //dJEJEKL = dJEJEKL + dIPKLQTY - dCHKLQTY;

                                if (dIPMTQTY + dIPKLQTY + dCHMTQTY + dCHKLQTY + dJEJEMT + dJEJEKL + dJEOVMT + dJEOVKL != 0)
                                {
                                    DataRow row1 = retDt.NewRow();
                                    row1["VNSANGHO"] = sOLDVNSANGHO.ToString();
                                    row1["HMDESC1"] = sOLDHMDESC1.ToString();
                                    row1["JEDATE"] = "SUB";
                                    row1["JETANKNO"] = TnoDt.Rows[j]["JETANKNO"].ToString();
                                    row1["IPMTQTY"] = dIPMTQTY;
                                    row1["IPKLQTY"] = dIPKLQTY;
                                    row1["CHMTQTY"] = dCHMTQTY;
                                    row1["CHKLQTY"] = dCHKLQTY;
                                    row1["JEJEMT"] = dJEJEMT;
                                    row1["JEJEKL"] = dJEJEKL;
                                    row1["JEOVMT"] = dJEOVMT;
                                    row1["JEOVKL"] = dJEOVKL;
                                    row1["JEJOSTR"] = "";
                                    row1["JEJOEND"] = "";
                                    row1["JEOVSTR"] = "";
                                    row1["JEOVEND"] = "";
                                    row1["JEDESC1"] = "";
                                    retDt.Rows.Add(row1);
                                }

                                dHAPIPMTQTY = dHAPIPMTQTY + dIPMTQTY;
                                dHAPIPKLQTY = dHAPIPKLQTY + dIPKLQTY;
                                dHAPCHMTQTY = dHAPCHMTQTY + dCHMTQTY;
                                dHAPCHKLQTY = dHAPCHKLQTY + dCHKLQTY;
                                dHAPJEJEMT = dHAPJEJEMT + dJEJEMT;
                                dHAPJEJEKL = dHAPJEJEKL + dJEJEKL;
                                dHAPJEOVMT = dHAPJEOVMT + dJEOVMT;
                                dHAPJEOVKL = dHAPJEOVKL + dJEOVKL;
                            }

                            // 화주,화물,탱크에 따른 총계를 구함
                            DataRow row2 = retDt.NewRow();
                            row2["VNSANGHO"] = sOLDVNSANGHO.ToString();
                            row2["HMDESC1"] = sOLDHMDESC1.ToString();
                            row2["JEDATE"] = "TOT";
                            row2["JETANKNO"] = "";
                            row2["IPMTQTY"] = dHAPIPMTQTY;
                            row2["IPKLQTY"] = dHAPIPKLQTY;
                            row2["CHMTQTY"] = dHAPCHMTQTY;
                            row2["CHKLQTY"] = dHAPCHKLQTY;
                            row2["JEJEMT"] = dHAPJEJEMT;
                            row2["JEJEKL"] = dHAPJEJEKL;
                            row2["JEOVMT"] = dHAPJEOVMT;
                            row2["JEOVKL"] = dHAPJEOVKL;
                            row2["JEJOSTR"] = "";
                            row2["JEJOEND"] = "";
                            row2["JEOVSTR"] = "";
                            row2["JEOVEND"] = "";
                            row2["JEDESC1"] = "";
                            retDt.Rows.Add(row2);

                            dIPMTQTY = 0;
                            dIPKLQTY = 0;
                            dCHMTQTY = 0;
                            dCHKLQTY = 0;
                            dJEJEMT = 0;
                            dJEJEKL = 0;
                            dJEOVMT = 0;
                            dJEOVKL = 0;
                            dHAPIPMTQTY = 0;
                            dHAPIPKLQTY = 0;
                            dHAPCHMTQTY = 0;
                            dHAPCHKLQTY = 0;
                            dHAPJEJEMT = 0;
                            dHAPJEJEKL = 0;
                            dHAPJEOVMT = 0;
                            dHAPJEOVKL = 0;

                            // 바뀐 화물에 대한 자료를 INSERT함
                            DataRow row3 = retDt.NewRow();

                            row3["VNSANGHO"] = dt.Rows[i]["VNSANGHO"].ToString();
                            row3["HMDESC1"] = dt.Rows[i]["HMDESC1"].ToString();
                            sJEGUBUN = dt.Rows[i]["JEGUBUN"].ToString();

                            row3["JEDATE"] = dt.Rows[i]["JEDATE"].ToString();
                            row3["JETANKNO"] = dt.Rows[i]["JETANKNO"].ToString();
                            row3["IPMTQTY"] = double.Parse(Get_Numeric(dt.Rows[i]["IPMTQTY"].ToString()));
                            row3["IPKLQTY"] = double.Parse(Get_Numeric(dt.Rows[i]["IPKLQTY"].ToString()));
                            row3["CHMTQTY"] = double.Parse(Get_Numeric(dt.Rows[i]["CHMTQTY"].ToString()));
                            row3["CHKLQTY"] = double.Parse(Get_Numeric(dt.Rows[i]["CHKLQTY"].ToString()));
                            row3["JEJEMT"] = double.Parse(Get_Numeric(dt.Rows[i]["JEJEMT"].ToString()));
                            row3["JEJEKL"] = double.Parse(Get_Numeric(dt.Rows[i]["JEJEKL"].ToString()));
                            row3["JEOVMT"] = double.Parse(Get_Numeric(dt.Rows[i]["JEOVMT"].ToString()));
                            row3["JEOVKL"] = double.Parse(Get_Numeric(dt.Rows[i]["JEOVKL"].ToString()));

                            sJEJOSTR = "";
                            sJEJOEND = "";
                            sJEOVSTR = "";
                            sJEOVEND = "";

                            if (dt.Rows[i]["JEJOSTR"].ToString() != "0")
                            {
                                sJEJOSTR = Set_Fill4(dt.Rows[i]["JEJOSTR"].ToString()).Substring(0, 2) + ":" + Set_Fill4(dt.Rows[i]["JEJOSTR"].ToString()).Substring(2, 2) + "~";
                            }
                            else
                            {
                                if (double.Parse(Get_Numeric(dt.Rows[i]["IPMTQTY"].ToString())) != 0)
                                {
                                    sJEJOSTR = Set_Fill4(dt.Rows[i]["JEJOSTR"].ToString()).Substring(0, 2) + ":" + Set_Fill4(dt.Rows[i]["JEJOSTR"].ToString()).Substring(2, 2) + "~";
                                }
                            }

                            if (dt.Rows[i]["JEJOEND"].ToString() != "0")
                            {
                                sJEJOEND = Set_Fill4(dt.Rows[i]["JEJOEND"].ToString()).Substring(0, 2) + ":" + Set_Fill4(dt.Rows[i]["JEJOEND"].ToString()).Substring(2, 2);
                            }

                            if (dt.Rows[i]["JEOVSTR"].ToString() != "0")
                            {
                                sJEOVSTR = Set_Fill4(dt.Rows[i]["JEOVSTR"].ToString()).Substring(0, 2) + ":" + Set_Fill4(dt.Rows[i]["JEOVSTR"].ToString()).Substring(2, 2) + "~";
                            }
                            else
                            {
                                if (double.Parse(Get_Numeric(dt.Rows[i]["IPMTQTY"].ToString())) != 0)
                                {
                                    sJEOVSTR = Set_Fill4(dt.Rows[i]["JEOVSTR"].ToString()).Substring(0, 2) + ":" + Set_Fill4(dt.Rows[i]["JEOVSTR"].ToString()).Substring(2, 2) + "~";
                                }
                            }

                            if (dt.Rows[i]["JEOVEND"].ToString() != "0")
                            {
                                sJEOVEND = Set_Fill4(dt.Rows[i]["JEOVEND"].ToString()).Substring(0, 2) + ":" + Set_Fill4(dt.Rows[i]["JEOVEND"].ToString()).Substring(2, 2);
                            }

                            row3["JEJOSTR"] = sJEJOSTR.ToString() + sJEJOEND.ToString();
                            row3["JEJOEND"] = "";
                            row3["JEOVSTR"] = sJEOVSTR.ToString() + sJEOVEND.ToString();
                            row3["JEOVEND"] = "";

                            if (sJEGUBUN == "6")
                            {
                                row3["JEDESC1"] = "이고" + dt.Rows[i]["JEMVTANK"].ToString();
                            }
                            else if (sJEGUBUN == "2")
                            {
                                row3["JEDESC1"] = "이고입고";
                            }
                            else if (sJEGUBUN != "1")
                            {
                                if (sJEGUBUN == "4")
                                {
                                    row3["JEDESC1"] = "선적  (" + dt.Rows[i]["JEBONSUN"].ToString() + " )";
                                }
                                else if (sJEGUBUN == "8")
                                {
                                    row3["JEDESC1"] = "재선적  (" + dt.Rows[i]["JEBONSUN"].ToString() + " )";
                                }
                                else if (sJEGUBUN == "5")
                                {
                                    row3["JEDESC1"] = "송유";
                                }
                                else if (sJEGUBUN == "7")
                                {
                                    row3["JEDESC1"] = "PIPE";
                                }
                                else if (sJEGUBUN == "9")
                                {
                                    row3["JEDESC1"] = "ISOTANK";
                                }
                                else
                                {
                                    row3["JEDESC1"] = "";
                                }
                            }
                            else
                            {
                                row3["JEDESC1"] = dt.Rows[i]["JEBONSUN"].ToString();
                            }

                            row3["DATE"] = dt.Rows[i]["DATE"].ToString();

                            retDt.Rows.Add(row3);

                            sOLDJEHWAMUL = sNEWJEHWAMUL.ToString();
                            sOLDHMDESC1 = sNEWHMDESC1.ToString();
                        }
                    }
                    else //  화주가 다를 경우
                    {

                        // 화주,화물에 따른 탱크번호를 가져옴
                        //this.FPS91_TY_S_UT_73LIX051.Initialize();

                        this.DbConnector.Attach("TY_P_UT_73SGS121", sOLDJEHWAJU,
                                                                    sOLDJEHWAMUL
                                                                    );

                        TnoDt = this.DbConnector.ExecuteDataTable();

                        for (int j = 0; j < TnoDt.Rows.Count; j++)
                        {
                            // 화주,화물,탱크번호에 따른 총 입고,출고,할증(MT,K/L)량을 가져옴
                            //this.FPS91_TY_S_UT_73LIX051.Initialize();

                            this.DbConnector.Attach("TY_P_UT_73SHG122", sOLDJEHWAJU.ToString(),
                                                                        sOLDJEHWAMUL.ToString(),
                                                                        TnoDt.Rows[j]["JETANKNO"].ToString()
                                                                        );

                            jegoDt = this.DbConnector.ExecuteDataTable();

                            if (jegoDt.Rows.Count > 0)
                            {
                                dIPMTQTY = double.Parse(Get_Numeric(jegoDt.Rows[0]["IPMTQTY"].ToString()));
                                dIPKLQTY = double.Parse(Get_Numeric(jegoDt.Rows[0]["IPKLQTY"].ToString()));
                                dCHMTQTY = double.Parse(Get_Numeric(jegoDt.Rows[0]["CHMTQTY"].ToString()));
                                dCHKLQTY = double.Parse(Get_Numeric(jegoDt.Rows[0]["CHKLQTY"].ToString()));
                                dJEOVMT = double.Parse(Get_Numeric(jegoDt.Rows[0]["JEOVMT"].ToString()));
                                dJEOVKL = double.Parse(Get_Numeric(jegoDt.Rows[0]["JEOVKL"].ToString()));
                            }

                            // 화주,화물,탱크번호에 따른 마지막 재고(MT,K/L)량을 가져옴
                            //this.FPS91_TY_S_UT_73LIX051.Initialize();

                            this.DbConnector.Attach("TY_P_UT_8ACFZ941", sOLDJEHWAJU.ToString(),
                                                                        sOLDJEHWAMUL.ToString(),
                                                                        TnoDt.Rows[j]["JETANKNO"].ToString()
                                                                        );

                            jegoDt = this.DbConnector.ExecuteDataTable();

                            if (jegoDt.Rows.Count > 0)
                            {
                                dJEJEMT = double.Parse(Get_Numeric(jegoDt.Rows[0]["JEJEMT"].ToString()));
                                dJEJEKL = double.Parse(Get_Numeric(jegoDt.Rows[0]["JEJEKL"].ToString()));
                            }
                            else
                            {
                                dJEJEMT = 0;
                                dJEJEKL = 0;
                            }

                            //dJEJEMT = dJEJEMT + dIPMTQTY - dCHMTQTY;
                            //dJEJEKL = dJEJEKL + dIPKLQTY - dCHKLQTY;

                            if (dIPMTQTY + dIPKLQTY + dCHMTQTY + dCHKLQTY + dJEJEMT + dJEJEKL + dJEOVMT + dJEOVKL != 0)
                            {
                                DataRow row7 = retDt.NewRow();
                                row7["VNSANGHO"] = sOLDVNSANGHO.ToString();
                                row7["HMDESC1"] = sOLDHMDESC1.ToString();
                                row7["JEDATE"] = "SUB";
                                row7["JETANKNO"] = TnoDt.Rows[j]["JETANKNO"].ToString();
                                row7["IPMTQTY"] = dIPMTQTY;
                                row7["IPKLQTY"] = dIPKLQTY;
                                row7["CHMTQTY"] = dCHMTQTY;
                                row7["CHKLQTY"] = dCHKLQTY;
                                row7["JEJEMT"] = dJEJEMT;
                                row7["JEJEKL"] = dJEJEKL;
                                row7["JEOVMT"] = dJEOVMT;
                                row7["JEOVKL"] = dJEOVKL;
                                row7["JEJOSTR"] = "";
                                row7["JEJOEND"] = "";
                                row7["JEOVSTR"] = "";
                                row7["JEOVEND"] = "";
                                row7["JEDESC1"] = "";
                                retDt.Rows.Add(row7);
                            }

                            dHAPIPMTQTY = dHAPIPMTQTY + dIPMTQTY;
                            dHAPIPKLQTY = dHAPIPKLQTY + dIPKLQTY;
                            dHAPCHMTQTY = dHAPCHMTQTY + dCHMTQTY;
                            dHAPCHKLQTY = dHAPCHKLQTY + dCHKLQTY;
                            dHAPJEJEMT = dHAPJEJEMT + dJEJEMT;
                            dHAPJEJEKL = dHAPJEJEKL + dJEJEKL;
                            dHAPJEOVMT = dHAPJEOVMT + dJEOVMT;
                            dHAPJEOVKL = dHAPJEOVKL + dJEOVKL;
                        }

                        // 화주,화물,탱크에 따른 총계를 구함
                        DataRow row8 = retDt.NewRow();
                        row8["VNSANGHO"] = sOLDVNSANGHO.ToString();
                        row8["HMDESC1"] = sOLDHMDESC1.ToString();
                        row8["JEDATE"] = "TOT";
                        row8["JETANKNO"] = "";
                        row8["IPMTQTY"] = dHAPIPMTQTY;
                        row8["IPKLQTY"] = dHAPIPKLQTY;
                        row8["CHMTQTY"] = dHAPCHMTQTY;
                        row8["CHKLQTY"] = dHAPCHKLQTY;
                        row8["JEJEMT"] = dHAPJEJEMT;
                        row8["JEJEKL"] = dHAPJEJEKL;
                        row8["JEOVMT"] = dHAPJEOVMT;
                        row8["JEOVKL"] = dHAPJEOVKL;
                        row8["JEJOSTR"] = "";
                        row8["JEJOEND"] = "";
                        row8["JEOVSTR"] = "";
                        row8["JEOVEND"] = "";
                        row8["JEDESC1"] = "";
                        retDt.Rows.Add(row8);

                        dIPMTQTY = 0;
                        dIPKLQTY = 0;
                        dCHMTQTY = 0;
                        dCHKLQTY = 0;
                        dJEJEMT = 0;
                        dJEJEKL = 0;
                        dJEOVMT = 0;
                        dJEOVKL = 0;
                        dHAPIPMTQTY = 0;
                        dHAPIPKLQTY = 0;
                        dHAPCHMTQTY = 0;
                        dHAPCHKLQTY = 0;
                        dHAPJEJEMT = 0;
                        dHAPJEJEKL = 0;
                        dHAPJEOVMT = 0;
                        dHAPJEOVKL = 0;

                        // 바뀐 화주에 대한 자료를 INSERT함
                        DataRow row9 = retDt.NewRow();

                        row9["VNSANGHO"] = dt.Rows[i]["VNSANGHO"].ToString();
                        row9["HMDESC1"] = dt.Rows[i]["HMDESC1"].ToString();
                        sJEGUBUN = dt.Rows[i]["JEGUBUN"].ToString();

                        row9["JEDATE"] = dt.Rows[i]["JEDATE"].ToString();
                        row9["JETANKNO"] = dt.Rows[i]["JETANKNO"].ToString();
                        row9["IPMTQTY"] = double.Parse(Get_Numeric(dt.Rows[i]["IPMTQTY"].ToString()));
                        row9["IPKLQTY"] = double.Parse(Get_Numeric(dt.Rows[i]["IPKLQTY"].ToString()));
                        row9["CHMTQTY"] = double.Parse(Get_Numeric(dt.Rows[i]["CHMTQTY"].ToString()));
                        row9["CHKLQTY"] = double.Parse(Get_Numeric(dt.Rows[i]["CHKLQTY"].ToString()));
                        row9["JEJEMT"] = double.Parse(Get_Numeric(dt.Rows[i]["JEJEMT"].ToString()));
                        row9["JEJEKL"] = double.Parse(Get_Numeric(dt.Rows[i]["JEJEKL"].ToString()));
                        row9["JEOVMT"] = double.Parse(Get_Numeric(dt.Rows[i]["JEOVMT"].ToString()));
                        row9["JEOVKL"] = double.Parse(Get_Numeric(dt.Rows[i]["JEOVKL"].ToString()));

                        sJEJOSTR = "";
                        sJEJOEND = "";
                        sJEOVSTR = "";
                        sJEOVEND = "";

                        if (dt.Rows[i]["JEJOSTR"].ToString() != "0")
                        {
                            sJEJOSTR = Set_Fill4(dt.Rows[i]["JEJOSTR"].ToString()).Substring(0, 2) + ":" + Set_Fill4(dt.Rows[i]["JEJOSTR"].ToString()).Substring(2, 2) + "~";
                        }
                        else
                        {
                            if (double.Parse(Get_Numeric(dt.Rows[i]["IPMTQTY"].ToString())) != 0)
                            {
                                sJEJOSTR = Set_Fill4(dt.Rows[i]["JEJOSTR"].ToString()).Substring(0, 2) + ":" + Set_Fill4(dt.Rows[i]["JEJOSTR"].ToString()).Substring(2, 2) + "~";
                            }
                        }

                        if (dt.Rows[i]["JEJOEND"].ToString() != "0")
                        {
                            sJEJOEND = Set_Fill4(dt.Rows[i]["JEJOEND"].ToString()).Substring(0, 2) + ":" + Set_Fill4(dt.Rows[i]["JEJOEND"].ToString()).Substring(2, 2);
                        }

                        if (dt.Rows[i]["JEOVSTR"].ToString() != "0")
                        {
                            sJEOVSTR = Set_Fill4(dt.Rows[i]["JEOVSTR"].ToString()).Substring(0, 2) + ":" + Set_Fill4(dt.Rows[i]["JEOVSTR"].ToString()).Substring(2, 2) + "~";
                        }
                        else
                        {
                            if (double.Parse(Get_Numeric(dt.Rows[i]["IPMTQTY"].ToString())) != 0)
                            {
                                sJEOVSTR = Set_Fill4(dt.Rows[i]["JEOVSTR"].ToString()).Substring(0, 2) + ":" + Set_Fill4(dt.Rows[i]["JEOVSTR"].ToString()).Substring(2, 2) + "~";
                            }
                        }

                        if (dt.Rows[i]["JEOVEND"].ToString() != "0")
                        {
                            sJEOVEND = Set_Fill4(dt.Rows[i]["JEOVEND"].ToString()).Substring(0, 2) + ":" + Set_Fill4(dt.Rows[i]["JEOVEND"].ToString()).Substring(2, 2);
                        }

                        row9["JEJOSTR"] = sJEJOSTR.ToString() + sJEJOEND.ToString();
                        row9["JEJOEND"] = "";
                        row9["JEOVSTR"] = sJEOVSTR.ToString() + sJEOVEND.ToString();
                        row9["JEOVEND"] = "";

                        if (sJEGUBUN == "6")
                        {
                            row9["JEDESC1"] = "이고" + dt.Rows[i]["JEMVTANK"].ToString();
                        }
                        else if (sJEGUBUN == "2")
                        {
                            row9["JEDESC1"] = "이고입고";
                        }
                        else if (sJEGUBUN != "1")
                        {
                            if (sJEGUBUN == "4")
                            {
                                row9["JEDESC1"] = "선적  (" + dt.Rows[i]["JEBONSUN"].ToString() + " )";
                            }
                            else if (sJEGUBUN == "8")
                            {
                                row9["JEDESC1"] = "재선적  (" + dt.Rows[i]["JEBONSUN"].ToString() + " )";
                            }
                            else if (sJEGUBUN == "5")
                            {
                                row9["JEDESC1"] = "송유";
                            }
                            else if (sJEGUBUN == "7")
                            {
                                row9["JEDESC1"] = "PIPE";
                            }
                            else if (sJEGUBUN == "9")
                            {
                                row9["JEDESC1"] = "ISOTANK";
                            }
                            else
                            {
                                row9["JEDESC1"] = "";
                            }
                        }
                        else
                        {
                            row9["JEDESC1"] = dt.Rows[i]["JEBONSUN"].ToString();
                        }

                        row9["DATE"] = dt.Rows[i]["DATE"].ToString();

                        retDt.Rows.Add(row9);

                        sOLDJEHWAJU = sNEWJEHWAJU.ToString();
                        sOLDVNSANGHO = sNEWVNSANGHO.ToString();
                        sOLDJEHWAMUL = sNEWJEHWAMUL.ToString();
                        sOLDHMDESC1 = sNEWHMDESC1.ToString();
                    }
                }
                // 
            }


            dIPMTQTY = 0;
            dIPKLQTY = 0;
            dCHMTQTY = 0;
            dCHKLQTY = 0;
            dJEOVMT = 0;
            dJEOVKL = 0;
            dJEJEMT = 0;
            dJEJEKL = 0;
            dHAPIPMTQTY = 0;
            dHAPIPKLQTY = 0;
            dHAPCHMTQTY = 0;
            dHAPCHKLQTY = 0;
            dHAPJEOVMT = 0;
            dHAPJEOVKL = 0;
            dHAPJEJEMT = 0;
            dHAPJEJEKL = 0;

            // 마지막 자료를 INSERT 함

            // 화주,화물에 따른 탱크번호를 가져옴
            //this.FPS91_TY_S_UT_73LIX051.Initialize();

            this.DbConnector.Attach("TY_P_UT_73SGS121", sNEWJEHWAJU,
                                                        sOLDJEHWAMUL
                                                        );

            TnoDt = this.DbConnector.ExecuteDataTable();

            for (int j = 0; j < TnoDt.Rows.Count; j++)
            {
                // 화주,화물,탱크번호에 따른 총 입고,출고,할증(MT,K/L)량을 가져옴
                //this.FPS91_TY_S_UT_73LIX051.Initialize();

                this.DbConnector.Attach("TY_P_UT_73SHG122", sNEWJEHWAJU.ToString(),
                                                            sOLDJEHWAMUL.ToString(),
                                                            TnoDt.Rows[j]["JETANKNO"].ToString()
                                                            );

                jegoDt = this.DbConnector.ExecuteDataTable();

                if (jegoDt.Rows.Count > 0)
                {
                    dIPMTQTY = double.Parse(Get_Numeric(jegoDt.Rows[0]["IPMTQTY"].ToString()));
                    dIPKLQTY = double.Parse(Get_Numeric(jegoDt.Rows[0]["IPKLQTY"].ToString()));
                    dCHMTQTY = double.Parse(Get_Numeric(jegoDt.Rows[0]["CHMTQTY"].ToString()));
                    dCHKLQTY = double.Parse(Get_Numeric(jegoDt.Rows[0]["CHKLQTY"].ToString()));
                    dJEOVMT = double.Parse(Get_Numeric(jegoDt.Rows[0]["JEOVMT"].ToString()));
                    dJEOVKL = double.Parse(Get_Numeric(jegoDt.Rows[0]["JEOVKL"].ToString()));
                }

                // 화주,화물,탱크번호에 따른 마지막 재고(MT,K/L)량을 가져옴
                //this.FPS91_TY_S_UT_73LIX051.Initialize();

                this.DbConnector.Attach("TY_P_UT_8ACFZ941", sNEWJEHWAJU.ToString(),
                                                            sOLDJEHWAMUL.ToString(),
                                                            TnoDt.Rows[j]["JETANKNO"].ToString()
                                                            );

                jegoDt = this.DbConnector.ExecuteDataTable();

                if (jegoDt.Rows.Count > 0)
                {
                    dJEJEMT = double.Parse(Get_Numeric(jegoDt.Rows[0]["JEJEMT"].ToString()));
                    dJEJEKL = double.Parse(Get_Numeric(jegoDt.Rows[0]["JEJEKL"].ToString()));
                }
                else
                {
                    dJEJEMT = 0;
                    dJEJEKL = 0;
                }

                //dJEJEMT = dJEJEMT + dIPMTQTY - dCHMTQTY;
                //dJEJEKL = dJEJEKL + dIPKLQTY - dCHKLQTY;

                if (dIPMTQTY + dIPKLQTY + dCHMTQTY + dCHKLQTY + dJEJEMT + dJEJEKL + dJEOVMT + dJEOVKL != 0)
                {
                    DataRow row4 = retDt.NewRow();

                    row4["VNSANGHO"] = sOLDVNSANGHO.ToString();
                    row4["HMDESC1"] = sOLDHMDESC1.ToString();
                    row4["JEDATE"] = "SUB";
                    row4["JETANKNO"] = TnoDt.Rows[j]["JETANKNO"].ToString();
                    row4["IPMTQTY"] = dIPMTQTY;
                    row4["IPKLQTY"] = dIPKLQTY;
                    row4["CHMTQTY"] = dCHMTQTY;
                    row4["CHKLQTY"] = dCHKLQTY;
                    row4["JEJEMT"] = dJEJEMT;
                    row4["JEJEKL"] = dJEJEKL;
                    row4["JEOVMT"] = dJEOVMT;
                    row4["JEOVKL"] = dJEOVKL;
                    row4["JEJOSTR"] = "";
                    row4["JEJOEND"] = "";
                    row4["JEOVSTR"] = "";
                    row4["JEOVEND"] = "";
                    row4["JEDESC1"] = "";

                    retDt.Rows.Add(row4);
                }

                dHAPIPMTQTY = dHAPIPMTQTY + dIPMTQTY;
                dHAPIPKLQTY = dHAPIPKLQTY + dIPKLQTY;
                dHAPCHMTQTY = dHAPCHMTQTY + dCHMTQTY;
                dHAPCHKLQTY = dHAPCHKLQTY + dCHKLQTY;
                dHAPJEJEMT = dHAPJEJEMT + dJEJEMT;
                dHAPJEJEKL = dHAPJEJEKL + dJEJEKL;
                dHAPJEOVMT = dHAPJEOVMT + dJEOVMT;
                dHAPJEOVKL = dHAPJEOVKL + dJEOVKL;
            }

            // 화주,화물,탱크에 따른 총계를 구함
            DataRow row5 = retDt.NewRow();
            row5["VNSANGHO"] = sOLDVNSANGHO.ToString();
            row5["HMDESC1"] = sOLDHMDESC1.ToString();
            row5["JEDATE"] = "TOT";
            row5["JETANKNO"] = "";
            row5["IPMTQTY"] = dHAPIPMTQTY;
            row5["IPKLQTY"] = dHAPIPKLQTY;
            row5["CHMTQTY"] = dHAPCHMTQTY;
            row5["CHKLQTY"] = dHAPCHKLQTY;
            row5["JEJEMT"] = dHAPJEJEMT;
            row5["JEJEKL"] = dHAPJEJEKL;
            row5["JEOVMT"] = dHAPJEOVMT;
            row5["JEOVKL"] = dHAPJEOVKL;
            row5["JEJOSTR"] = "";
            row5["JEJOEND"] = "";
            row5["JEOVSTR"] = "";
            row5["JEOVEND"] = "";
            row5["JEDESC1"] = "";
            retDt.Rows.Add(row5);

            dIPMTQTY = 0;
            dIPKLQTY = 0;
            dCHMTQTY = 0;
            dCHKLQTY = 0;
            dJEOVMT = 0;
            dJEOVKL = 0;
            dJEJEMT = 0;
            dJEJEKL = 0;
            dHAPIPMTQTY = 0;
            dHAPIPKLQTY = 0;
            dHAPCHMTQTY = 0;
            dHAPCHKLQTY = 0;
            dHAPJEOVMT = 0;
            dHAPJEOVKL = 0;
            dHAPJEJEMT = 0;
            dHAPJEJEKL = 0;

            return retDt;
        }
        #endregion
    }
}
