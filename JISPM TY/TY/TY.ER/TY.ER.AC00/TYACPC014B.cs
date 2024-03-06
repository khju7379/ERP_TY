using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 자료생성(2단계) 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.12.17 16:16
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
    ///  TY_M_AC_27H6I062 : EIS 마감 년월이 존재 하지 않습니다.
    ///  TY_M_AC_27H6I063 : EIS 적용 완료상태 입니다. (처리 불가)
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  TAMAGB : 매출현황
    ///  TAMUGB : 물량현황
    ///  TASBGB : 주요시설현황
    ///  TATRFUNDGB : 장기채권현황
    ///  TATRITEMGB : 품목별매출현황
    ///  TATRJEGOGB : 장기재고현황
    ///  TAUGGB : 유형자산현황
    /// </summary>
    public partial class TYACPC014B : TYBase
    {
        #region Description : 페이지 로드
        public TYACPC014B()
        {
            InitializeComponent();
        }

        private void TYACPC014B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_GSTYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));            

            this.SetStartingFocus(this.DTP01_GSTYYMM);
        }
        #endregion

        #region Description : 생성 작업
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sGUBUN = string.Empty;
            string sOUTMSG = string.Empty;

            string s물량현황 = this.CKB01_TAMAGB.GetValue().ToString();
            string s매출현황 = this.CKB01_TAMUGB.GetValue().ToString();
            string s유형자산 = this.CKB01_TAUGGB.GetValue().ToString();
            string s주요시설 = this.CKB01_TASBGB.GetValue().ToString();
            string s무역품목별매출현황 = this.CKB01_TATRITEMGB.GetValue().ToString();
            string s무역장기재고 = this.CKB01_TATRJEGOGB.GetValue().ToString();
            string s무역장기채권 = this.CKB01_TATRFUNDGB.GetValue().ToString();


            //물량현황
            #region Description : EIS 물량현황
            if (s물량현황 == "A")
            {

                if (this.CBO01_GOKCR.GetValue().ToString() == "A")
                {
                    sGUBUN = "A";
                }
                else
                {
                    sGUBUN = "D";
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_37NB2214",
                    sGUBUN.ToString(),
                    this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6),
                    sOUTMSG.ToString()
                    );

                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                this.TXT01_TAMAGB.Text = sOUTMSG;
            }
            #endregion

            //매출현황
            #region Description : EIS 매출현황
            if (s매출현황 == "A")
            {
                string sYYMM_AGO = string.Empty;
                string sYEAR = string.Empty;
                string sMONTH = string.Empty;

                sYEAR = this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4);
                sMONTH = this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2);

                if (sMONTH.ToString() == "01")
                {
                    sYEAR = Convert.ToString(int.Parse(sYEAR) - 1);
                    sMONTH = "12";
                }
                else
                {
                    sMONTH = Set_Fill2(Convert.ToString(int.Parse(sMONTH) - 1));
                }

                sYYMM_AGO = sYEAR.ToString() + sMONTH.ToString();

                if (this.CBO01_GOKCR.GetValue().ToString() == "A")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3872T364",
                        this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6),
                        sOUTMSG.ToString()
                        );

                    sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_3874B368", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));

                    this.DbConnector.ExecuteNonQuery();

                    sOUTMSG = "OK-정상적으로 처리되었습니다";
                }

                this.TXT01_TAMAGB.Text = sOUTMSG;

            }
            #endregion

            //유형자산
            #region Description : EIS 유형자산
            if (s유형자산 == "A")
            {              

            }
            #endregion

            //주요시설
            #region Description : EIS 주요시설
            if (s주요시설 == "A")
            {
                UP_Installation_Create();
            }
            #endregion

            //무역품목별매출현황
            #region Description : EIS 무역품목별매출현황
            if (s무역품목별매출현황 == "A")
            {
                UP_ITEMSales_Create();                
            }
            #endregion

            //장기재고현황
            #region Description : EIS s무역장기재고
            if (s무역장기재고 == "A")
            {
                UP_LongJego_Create();               
            }
            #endregion

            //장기채권현황
            #region Description : EIS 장기채권
            if (s무역장기채권 == "A")
            {
                UP_LongFund_Create();
            }
            #endregion

        }
        #endregion

        #region Description : 종료
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 처리 CHECK
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // 마감 완료 CHECK 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_27H64059", this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4), this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2));
            DataTable dt1 = this.DbConnector.ExecuteDataTable();

            if (dt1.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                e.Successed = false;
                return;
            }
            else
            {
                if (dt1.Rows[0]["ECGUBUN"].ToString() == "Y" || dt1.Rows[0]["ECGUBUN"].ToString() == "Z" )
                {
                    this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                    e.Successed = false;
                    return;
                }
            }
           
        }
        #endregion

        #region Description : 품목별매출이익현황 생성
        private void UP_Sales_Create(string sYYMM, string sParamCDDP, string sParamITEMCODE, string sGRCODE)
        {
            DataTable dt = new DataTable();

            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

            string sYYMM_S = "";

            sYYMM_S = sYYMM.Substring(0, 4) + "01";

            this.DbConnector.CommandClear();
            //월별 금액
            if (sGRCODE != "900")
            {
                this.DbConnector.Attach("TY_P_AC_37A51076", sYYMM_S, sYYMM, sParamCDDP, sParamITEMCODE);
                dt = this.DbConnector.ExecuteDataTable();
            }
            else
            {
                this.DbConnector.Attach("TY_P_AC_37BBK079", sYYMM_S, sYYMM, sParamCDDP, sParamITEMCODE);
                dt = this.DbConnector.ExecuteDataTable();
            }

            if (dt.Rows.Count > 0)
            {
                datas.Add(new object[] { sYYMM,
                                         sParamCDDP,
                                         sGRCODE,
                                         dt.Rows[0]["PDMAEAMT17"].ToString(),
                                         dt.Rows[0]["PDMAWAMT8"].ToString(),                                         
                                         dt.Rows[0]["PDMCIAMT11"].ToString(),
                                         dt.Rows[0]["PDMAEAMT17_TOTAL"].ToString(),
                                         dt.Rows[0]["PDMAWAMT8_TOTAL"].ToString(),                                         
                                         dt.Rows[0]["PDMCIAMT11_TOTAL"].ToString(),
                                         "A",
                                         TYUserInfo.EmpNo});
            }

            if (datas.Count > 0)
            {
                this.DbConnector.CommandClear();
                foreach (object[] data in datas)
                {
                    this.DbConnector.Attach("TY_P_AC_37A2Z066", data); //대변 저장                
                }

                this.DbConnector.ExecuteTranQueryList();
            }

        }
        #endregion

        #region Description : 품목별매출이익현황 생성 함수
        private void UP_ITEMSales_Create()
        {
            string sOUTMSG = "";
            string sParamITEMCODE = "";
            string sITEMCODETOTAL = "";

            string sCDDP_Old = "";
            string sGRCODE_Old = "";

            //무역 품목분류관리 복사
            DateTime dtime = Convert.ToDateTime(this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 4) + "-" + this.DTP01_GSTYYMM.GetString().ToString().Substring(4, 2) + "-" + "01");
            dtime = dtime.AddMonths(-1);
            string sJWDATE = Convert.ToString(dtime.Year) + Set_Fill2(Convert.ToString(dtime.Month));

            //삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_37B4G088", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
            this.DbConnector.ExecuteTranQuery();
            //복사
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_37B4G087", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), TYUserInfo.EmpNo, sJWDATE);
            this.DbConnector.ExecuteTranQuery();

            //삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_37A30067", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
            this.DbConnector.ExecuteTranQuery();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_37BBR080", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
            DataTable dr = this.DbConnector.ExecuteDataTable();

            if (dr.Rows.Count > 0)
            {
                for (int k = 0; k < dr.Rows.Count; k++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_37AAJ055", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), dr.Rows[k]["ERCDDP"].ToString());
                    DataTable dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //부서,그룹코드가 바뀌면 생성
                            if (i != 0 && (sCDDP_Old != dt.Rows[i]["ERCDDP"].ToString() || sGRCODE_Old != dt.Rows[i]["ERGRCODE"].ToString()))
                            {
                                sParamITEMCODE = sParamITEMCODE.Substring(0, sParamITEMCODE.Length - 1);

                                UP_Sales_Create(this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), sCDDP_Old, sParamITEMCODE, dt.Rows[i - 1]["ERGRCODE"].ToString());

                                sParamITEMCODE = "";
                                sCDDP_Old = "";
                                sGRCODE_Old = "";
                            }
                            sParamITEMCODE = sParamITEMCODE + dt.Rows[i]["ERITEMCODE"].ToString() + ",";

                            sITEMCODETOTAL = sITEMCODETOTAL + dt.Rows[i]["ERITEMCODE"].ToString() + ",";

                            sCDDP_Old = dt.Rows[i]["ERCDDP"].ToString();
                            sGRCODE_Old = dt.Rows[i]["ERGRCODE"].ToString();

                            if (i == dt.Rows.Count - 1)
                            {
                                sParamITEMCODE = sParamITEMCODE.Substring(0, sParamITEMCODE.Length - 1);

                                UP_Sales_Create(this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), sCDDP_Old, sParamITEMCODE, dt.Rows[i]["ERGRCODE"].ToString());
                            }
                        } //for (int i = 0; i < dt.Rows.Count; i++)...end

                        //주요품목처리 완료후 기타부분 처리
                        sITEMCODETOTAL = sITEMCODETOTAL.Substring(0, sITEMCODETOTAL.Length - 1);

                        UP_Sales_Create(this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), sCDDP_Old, sITEMCODETOTAL, "900");

                        sParamITEMCODE = "";
                        sCDDP_Old = "";
                        sGRCODE_Old = "";
                        sITEMCODETOTAL = "";
                    }

                } // for (int k = 0; k < dr.Rows.Count; k++)..end
            }

            sOUTMSG = "OK-정상적으로 처리되었습니다";

            this.TXT01_TATRITEMGB.Text = sOUTMSG;
        }
        #endregion

        #region Description : 장기재고 생성 함수
        private void UP_LongJego_Create()
        {
            string sOUTMSG = "";
            double dStockPriceGap = 0;
            double dStockPrice = 0;

            string sPO_NO_OLD = "";
            string sBL_NO_OLD = "";
            string sITEMCODE_OLD = "";

            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

            //삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_37F3J108", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
            this.DbConnector.ExecuteTranQuery();

            DateTime dt = Convert.ToDateTime(this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 4) + "-" + this.DTP01_GSTYYMM.GetString().ToString().Substring(4, 2) + "-" + "01");

            dt = dt.AddMonths(1);
            dt = dt.AddDays(-1);

            string sKIJUNDATE = Convert.ToString(dt.Year) + Set_Fill2(dt.Month.ToString()) + Set_Fill2(dt.Day.ToString());

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_37F22106", sKIJUNDATE, sKIJUNDATE, sKIJUNDATE, sKIJUNDATE, this.DTP01_GSTYYMM.GetString().Substring(0, 6), this.DTP01_GSTYYMM.GetString().Substring(0, 6));
            DataTable dr = this.DbConnector.ExecuteDataTable();

            if (dr.Rows.Count > 0)
            {
                for (int i = 0; i < dr.Rows.Count; i++)
                {
                    dStockPriceGap = 0;

                    if (Convert.ToDouble(dr.Rows[i]["groupjegoqty"].ToString()) >= Convert.ToDouble(dr.Rows[i]["jgsjgqty"].ToString()))
                    {
                        if (Convert.ToDouble(dr.Rows[i]["jgisjamt"].ToString()) > Convert.ToDouble(dr.Rows[i]["groupstockprice"].ToString()))
                        {
                            dStockPriceGap = Convert.ToDouble(dr.Rows[i]["jgisjamt"].ToString()) - Convert.ToDouble(dr.Rows[i]["groupstockprice"].ToString());
                        }
                        else
                        {
                            dStockPriceGap = Convert.ToDouble(dr.Rows[i]["groupstockprice"].ToString()) - Convert.ToDouble(dr.Rows[i]["jgisjamt"].ToString());
                        }
                    }

                    if (i != 0 &&
                        (dr.Rows[i]["PO_NO"].ToString() != sPO_NO_OLD || dr.Rows[i]["HOUSE_BL_NO"].ToString() != sBL_NO_OLD || dr.Rows[i]["ITEM_CODE"].ToString() != sITEMCODE_OLD)
                       )
                    {
                        dStockPrice = Convert.ToDouble(dr.Rows[i]["STOCKPRICE"].ToString()) - dStockPriceGap;
                    }
                    else
                    {
                        dStockPrice = Convert.ToDouble(dr.Rows[i]["STOCKPRICE"].ToString());
                    }

                    if (i == (dr.Rows.Count - 1))
                    {
                        dStockPrice = Convert.ToDouble(dr.Rows[i]["STOCKPRICE"].ToString()) - dStockPriceGap;
                    }

                    dStockPrice = Math.Floor(dStockPrice / 1000000) * 1000000;

                    datas.Add(new object[] { this.DTP01_GSTYYMM.GetString().Substring(0, 6),
                                             dr.Rows[i]["PO_NO"].ToString().Substring(4,6),
                                             dr.Rows[i]["SN"].ToString(),
                                             dr.Rows[i]["PO_NO"].ToString(),
                                             dr.Rows[i]["HOUSE_BL_NO"].ToString(),
                                             dr.Rows[i]["ITEM_CODE"].ToString(),
                                             dr.Rows[i]["RECEIPT_DATE"].ToString(),
                                             dr.Rows[i]["JEGOQTY"].ToString(),
                                             dStockPrice.ToString(),
                                             TYUserInfo.EmpNo});

                    sPO_NO_OLD = dr.Rows[i]["PO_NO"].ToString();
                    sBL_NO_OLD = dr.Rows[i]["HOUSE_BL_NO"].ToString();
                    sITEMCODE_OLD = dr.Rows[i]["ITEM_CODE"].ToString();
                }
            }

            if (datas.Count > 0)
            {
                this.DbConnector.CommandClear();
                foreach (object[] data in datas)
                {
                    this.DbConnector.Attach("TY_P_AC_37F44109", data);
                }

                this.DbConnector.ExecuteTranQueryList();
            }

            sOUTMSG = "OK-정상적으로 처리되었습니다";

            this.TXT01_TATRJEGOGB.Text = sOUTMSG;
        }
        #endregion

        #region Description : 장기채권 생성 함수
        private void UP_LongFund_Create()
        {
            string sOUTMSG = "";

            DateTime dt = new DateTime();

            dt = Convert.ToDateTime(this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 4) + "-" + this.DTP01_GSTYYMM.GetString().ToString().Substring(4, 2) + "-01");

            dt = dt.AddMonths(1).AddDays(-1);

            string sDate = dt.Year.ToString() + Set_Fill2(dt.Month.ToString()) + Set_Fill2(dt.Day.ToString());

            /*
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_27J1V123", sDate, "D", Employer.UserID, "");
            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_27J1V123", sDate, "A", Employer.UserID, "");
            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
            */

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_37IAY151", this.DTP01_GSTYYMM.GetString().Substring(0, 6), "A", TYUserInfo.EmpNo, sOUTMSG.ToString());
            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUTMSG.ToString().Substring(0, 1) != "I")
            {
                sOUTMSG = "ER-비정상적으로 처리되었습니다";
            }
            else
            {
                sOUTMSG = "OK-정상적으로 처리되었습니다";
            }

            this.TXT01_TAUGGB.Text = sOUTMSG;
        }
        #endregion

        #region Description : 주요시설 생성 함수
        private void UP_Installation_Create()
        {
            string sOUTMSG = "";

            DateTime dt = Convert.ToDateTime(this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 4) + "-" + this.DTP01_GSTYYMM.GetString().ToString().Substring(4, 2) + "-" + "01");
            dt = dt.AddMonths(-1);
            string sJWDATE = Convert.ToString(dt.Year) + Set_Fill2(Convert.ToString(dt.Month));

            //전월 시설현황 복사
            this.DbConnector.CommandClear();
            //삭제
            this.DbConnector.Attach("TY_P_AC_38G1V391", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));

            //복사
            this.DbConnector.Attach("TY_P_AC_38G1V390", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), sJWDATE);

            this.DbConnector.ExecuteTranQueryList();

            sOUTMSG = "OK-정상적으로 처리되었습니다";

            this.TXT01_TASBGB.Text = sOUTMSG;
        }
        #endregion
    }
}
