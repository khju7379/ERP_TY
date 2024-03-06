using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 퇴충금 생성 팝업 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2018.04.25 11:37
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_84GBH814 : 인사 기본사항 조회(퇴충금 계산 용)
    ///  TY_P_HR_84GED815 : 인사 급여상세내역 조회(퇴충금 계산 용)
    ///  TY_P_HR_84GEH816 : 퇴충금명세서 DETAIL 등록
    ///  TY_P_HR_84IAU834 : 인사 급여평균OT 시간 조회(퇴충금 계산 용)
    ///  TY_P_HR_84IBM836 : 인사 급여상세내역 조회(퇴충금 계산 용)
    ///  TY_P_HR_84IG4837 : 인사 급여상세내역 조회(퇴충금 계산 용)
    ///  TY_P_HR_84NBT852 : 퇴충금명세서 MAST 등록
    ///  TY_P_HR_84NE2854 : 인사 급여상세내역 조회(퇴충금 계산 용)
    ///  TY_P_HR_84NEP855 : 퇴충금명세서 MAST 삭제
    ///  TY_P_HR_84NEP856 : 퇴충금명세서 DETAIL 삭제
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  TOYEAR : 년도
    /// </summary>
    public partial class TYHRKB018B : TYBase
    {
        private string fsTOYEAR;

        private int fiAvgMCnt;

        private TYData DAT02_TLYEAR;
        private TYData DAT02_TLSEQ;
        private TYData DAT02_TLSABUN;
        private TYData DAT02_TLNAME;
        private TYData DAT02_TLCOMDATE;
        private TYData DAT02_TLKBDATE;
        private TYData DAT02_TLJKCD;
        private TYData DAT02_TLBUSEO;
        private TYData DAT02_TLTYPE;
        private TYData DAT02_TLWKYEAR;
        private TYData DAT02_TLWKMONTH;
        private TYData DAT02_TLWKDAY;
        private TYData DAT02_TLAVG_M;
        private TYData DAT02_TLAVG_S;
        private TYData DAT02_TLAVGTOTAL;
        private TYData DAT02_TLAVG_OTTIME;
        private TYData DAT02_TLCOMTOTAL;


        private string fsTOOTDATES;
        private string fsTOOTDATEE;
        private string fsTOPYSDATE_FROM;
        private string fsTOPYSDATE_TO;
        private string fsTOPYMDATE3;

        private double fdMPayTotal;   //급여 총액
        private double fdSPayTotal;   //상여 총액
        private double fdYPayTotal;   //년차 총액       
        private string fsAvgOttime;   //평균ot시간

        private string fsKBIDATE;   //입사일자   

        System.Collections.Generic.List<object[]> datamf = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> datasf = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> dataxf = new System.Collections.Generic.List<object[]>();

        #region  Description : 폼 로드 이벤트
        public TYHRKB018B(string sTOYEAR)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsTOYEAR = sTOYEAR;

            DAT02_TLYEAR = new TYData("DAT02_TLYEAR", null);
            DAT02_TLSEQ = new TYData("DAT02_TLSEQ", null);
            DAT02_TLSABUN = new TYData("DAT02_TLSABUN", null);
            DAT02_TLNAME = new TYData("DAT02_TLNAME", null);
            DAT02_TLCOMDATE = new TYData("DAT02_TLCOMDATE", null);
            DAT02_TLKBDATE = new TYData("DAT02_TLKBDATE", null);
            DAT02_TLJKCD = new TYData("DAT02_TLJKCD", null);
            DAT02_TLBUSEO = new TYData("DAT02_TLBUSEO", null);
            DAT02_TLTYPE = new TYData("DAT02_TLTYPE", null);
            DAT02_TLWKYEAR = new TYData("DAT02_TLWKYEAR", null);
            DAT02_TLWKMONTH = new TYData("DAT02_TLWKMONTH", null);
            DAT02_TLWKDAY = new TYData("DAT02_TLWKDAY", null);
            DAT02_TLAVG_M = new TYData("DAT02_TLAVG_M", null);
            DAT02_TLAVG_S = new TYData("DAT02_TLAVG_S", null);
            DAT02_TLAVGTOTAL = new TYData("DAT02_TLAVGTOTAL", null);
            DAT02_TLAVG_OTTIME = new TYData("DAT02_TLAVG_OTTIME", null);
            DAT02_TLCOMTOTAL = new TYData("DAT02_TLCOMTOTAL", null);
        }

        private void TYHRKB018B_Load(object sender, System.EventArgs e)
        {
            CBH01_TOYEAR.SetValue(fsTOYEAR);

            this.ControlFactory.Add(this.DAT02_TLYEAR);
            this.ControlFactory.Add(this.DAT02_TLSEQ);
            this.ControlFactory.Add(this.DAT02_TLSABUN);
            this.ControlFactory.Add(this.DAT02_TLNAME);
            this.ControlFactory.Add(this.DAT02_TLCOMDATE);
            this.ControlFactory.Add(this.DAT02_TLKBDATE);
            this.ControlFactory.Add(this.DAT02_TLJKCD);
            this.ControlFactory.Add(this.DAT02_TLBUSEO);
            this.ControlFactory.Add(this.DAT02_TLTYPE);
            this.ControlFactory.Add(this.DAT02_TLWKYEAR);
            this.ControlFactory.Add(this.DAT02_TLWKMONTH);
            this.ControlFactory.Add(this.DAT02_TLWKDAY);
            this.ControlFactory.Add(this.DAT02_TLAVG_M);
            this.ControlFactory.Add(this.DAT02_TLAVG_S);
            this.ControlFactory.Add(this.DAT02_TLAVGTOTAL);
            this.ControlFactory.Add(this.DAT02_TLAVG_OTTIME);
            this.ControlFactory.Add(this.DAT02_TLCOMTOTAL);

            
        }
        #endregion

        #region  Description : 생성 버튼 이벤트
        private void UP_BATCH_Create()
        {
            this.DbConnector.CommandClear();
            //퇴충금 마스타 삭제            
            this.DbConnector.Attach("TY_P_HR_84NEP855", CBH01_TOYEAR.GetValue().ToString().Substring(0, 4), CBH01_TOYEAR.GetValue().ToString().Substring(5, 3));
            //퇴충금 내역 삭제
            this.DbConnector.Attach("TY_P_HR_84NEP856", CBH01_TOYEAR.GetValue().ToString().Substring(0, 4), CBH01_TOYEAR.GetValue().ToString().Substring(5, 3));
            //퇴충금명세서 배수내역(임원용)  삭제
            this.DbConnector.Attach("TY_P_HR_BC1FV855", CBH01_TOYEAR.GetValue().ToString().Substring(0, 4), CBH01_TOYEAR.GetValue().ToString().Substring(5, 3));
            this.DbConnector.ExecuteTranQueryList();   

            UP_BATCH_ProCess("DB");

            UP_BATCH_ProCess("DC");

            this.ShowMessage("TY_M_GB_26E30875");

            BTN61_CLO_Click(null, null);
        }
        #endregion

        #region  Description : 생성 함수 
        private void UP_BATCH_ProCess(string sType)
        {
            double dInCAmt1 = 0;
            double dInCAmt2 = 0;
            double dInCAmt3 = 0;

            double dInCAmt_Add1 = 0;
            double dInCAmt_Add2 = 0;
            double dInCAmt_Add3 = 0;

            string sKBPENSGUBN = string.Empty;
            string sKBPYPEAK = string.Empty;
            string sYYMM = string.Empty;

            fiAvgMCnt = 0;

            try
            {

                 datamf.Clear();
                 datasf.Clear();
                 dataxf.Clear();

                                            


                //재직자 대상자 조회(인사기본사항)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_84GBH814", TYUserInfo.SecureKey, "Y", "B", CBH01_TOYEAR.GetValue().ToString().Substring(0, 4), CBH01_TOYEAR.GetValue().ToString().Substring(5, 3), sType);
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    pgb_Status.Value = 0;
                    pgb_Status.Minimum = 0;
                    pgb_Status.Maximum = dt.Rows.Count;                                      
                                        
                    //전체 평균급여 조회
                    this.DbConnector.CommandClear();
                    //this.DbConnector.Attach("TY_P_HR_84GED815", dt.Rows[0]["TOPYMDATE1"].ToString().Trim().Substring(0, 4) + "01", dt.Rows[0]["TOPYMDATE3"].ToString().Trim().Substring(0, 4) + "12", "", dt.Rows[0]["TOKIJUNDATE"].ToString().Trim().Substring(0,4));
                    this.DbConnector.Attach("TY_P_HR_84GED815", dt.Rows[0]["TOPYMDATE1"].ToString().Trim().Substring(0, 4) + "01", dt.Rows[0]["TOPYMDATE3"].ToString().Trim().Substring(0, 4) + "12", "", dt.Rows[0]["TOPYMDATE3"].ToString().Trim().Substring(0, 4));
                    DataTable dAvgM_DB = this.DbConnector.ExecuteDataTable();

                    this.DbConnector.CommandClear();
                    //this.DbConnector.Attach("TY_P_HR_84GED815", dt.Rows[0]["TOPYMDATE1"].ToString().Trim().Substring(0, 4) + "01", dt.Rows[0]["TOPYMDATE3"].ToString().Trim().Substring(0, 4) + "12", "", dt.Rows[0]["TOKIJUNDATE"].ToString().Trim().Substring(0,4));
                    this.DbConnector.Attach("TY_P_HR_C15EY989", dt.Rows[0]["TOPYMDATE1"].ToString().Trim().Substring(0, 4) + "01", dt.Rows[0]["TOPYMDATE3"].ToString().Trim().Substring(0, 4) + "12", "", dt.Rows[0]["TOPYMDATE3"].ToString().Trim().Substring(0, 4));
                    DataTable dAvgM_DC = this.DbConnector.ExecuteDataTable();                    

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        UP_Set_Clear();

                        sKBPENSGUBN = dt.Rows[i]["KBPENSGUBN"].ToString();
                        sKBPYPEAK = dt.Rows[i]["KBPYPEAK"].ToString();

                        dInCAmt1 = 0;
                        dInCAmt2 = 0;
                        dInCAmt3 = 0;

                        dInCAmt_Add1 = 0;
                        dInCAmt_Add2 = 0;
                        dInCAmt_Add3 = 0;
                        //기본사항
                        this.DAT02_TLYEAR.SetValue(CBH01_TOYEAR.GetValue().ToString().Substring(0, 4));
                        this.DAT02_TLSEQ.SetValue(CBH01_TOYEAR.GetValue().ToString().Substring(5, 3));
                        this.DAT02_TLSABUN.SetValue(dt.Rows[i]["KBSABUN"].ToString().Trim());

                    
                        this.DAT02_TLNAME.SetValue(dt.Rows[i]["KBHANGL"].ToString().Trim());                    
                        
                        this.DAT02_TLCOMDATE.SetValue(dt.Rows[i]["TOKIJUNDATE"].ToString().Trim());  //계산기준일자                                          

                        if (dt.Rows[i]["KBJDATE"].ToString().Trim() != "")
                        {
                            this.DAT02_TLKBDATE.SetValue(dt.Rows[i]["KBJDATE"].ToString().Trim());  //중도정산일자
                        }
                        else
                        {
                            this.DAT02_TLKBDATE.SetValue(dt.Rows[i]["KBIDATE"].ToString().Trim());  //입사일자
                        }
                     

                        fsKBIDATE = dt.Rows[i]["KBIDATE"].ToString().Trim();

                        this.DAT02_TLJKCD.SetValue(dt.Rows[i]["KBJKCD"].ToString().Trim());
                        this.DAT02_TLBUSEO.SetValue(dt.Rows[i]["KBBUSEO"].ToString().Trim());

                        this.DAT02_TLTYPE.SetValue(dt.Rows[i]["KBPENSGUBN"].ToString().Trim());

                        fsTOOTDATES = dt.Rows[i]["TOOTDATES"].ToString().Trim().Substring(0, 6);  //ot시작년월
                        fsTOOTDATEE = dt.Rows[i]["TOOTDATEE"].ToString().Trim().Substring(0, 6);  //ot종료년월

                        fsTOPYSDATE_FROM = dt.Rows[i]["TOPYSDATE_FROM"].ToString().Trim().Substring(0, 6);  //상여시작년월
                        fsTOPYSDATE_TO = dt.Rows[i]["TOPYSDATE_TO"].ToString().Trim().Substring(0, 6);  //상여종료년월

                        if (sKBPENSGUBN != "DC")
                        {
                            #region  Description : DB형 퇴충금 계산
                            if (dt.Rows[i]["TOCOMGUBN"].ToString().Trim() == "1" || dt.Rows[i]["TOCOMGUBN"].ToString().Trim() == "3") //예상
                            {
                                //해당월에 통상임금 조회
                                //인상액 계산
                                if (dt.Rows[i]["TKPAYGN"].ToString().Trim() == "1") //1-통상임금 기준
                                {
                                    dInCAmt1 = Math.Floor(Convert.ToDouble(dt.Rows[i]["TOPYTONGAMT1"].ToString().Trim()) * Convert.ToDouble(dt.Rows[i]["TKINRATE1"].ToString().Trim()));
                                    dInCAmt2 = Math.Floor(Convert.ToDouble(dt.Rows[i]["TOPYTONGAMT2"].ToString().Trim()) * Convert.ToDouble(dt.Rows[i]["TKINRATE1"].ToString().Trim()));
                                    dInCAmt3 = Math.Floor(Convert.ToDouble(dt.Rows[i]["TOPYTONGAMT3"].ToString().Trim()) * Convert.ToDouble(dt.Rows[i]["TKINRATE1"].ToString().Trim()));

                                    //추가인상 적용
                                    if (Convert.ToDouble(dt.Rows[i]["TKINRATE2"].ToString().Trim()) > 0)
                                    {
                                        dInCAmt_Add1 = Math.Floor((Convert.ToDouble(dt.Rows[i]["TOPYTONGAMT1"].ToString().Trim()) + dInCAmt1) * Convert.ToDouble(dt.Rows[i]["TKINRATE2"].ToString().Trim()));
                                        dInCAmt_Add2 = Math.Floor((Convert.ToDouble(dt.Rows[i]["TOPYTONGAMT2"].ToString().Trim()) + dInCAmt2) * Convert.ToDouble(dt.Rows[i]["TKINRATE2"].ToString().Trim()));
                                        dInCAmt_Add3 = Math.Floor((Convert.ToDouble(dt.Rows[i]["TOPYTONGAMT3"].ToString().Trim()) + dInCAmt3) * Convert.ToDouble(dt.Rows[i]["TKINRATE2"].ToString().Trim()));
                                    }
                                }

                                if (dt.Rows[i]["TKPAYGN"].ToString().Trim() == "2") //2-지급금액 기준
                                {
                                    //2-지급합계 기준
                                    dInCAmt1 = Math.Floor(Convert.ToDouble(dt.Rows[i]["TOPYTONGAMT1"].ToString().Trim()) * Convert.ToDouble(dt.Rows[i]["TKINRATE1"].ToString().Trim()));
                                    dInCAmt2 = Math.Floor(Convert.ToDouble(dt.Rows[i]["TOPYTONGAMT2"].ToString().Trim()) * Convert.ToDouble(dt.Rows[i]["TKINRATE1"].ToString().Trim()));
                                    dInCAmt3 = Math.Floor(Convert.ToDouble(dt.Rows[i]["TOPYTONGAMT3"].ToString().Trim()) * Convert.ToDouble(dt.Rows[i]["TKINRATE1"].ToString().Trim()));

                                    //추가인상 적용
                                    if (Convert.ToDouble(dt.Rows[i]["TKINRATE2"].ToString().Trim()) > 0)
                                    {
                                        dInCAmt_Add1 = Math.Floor((Convert.ToDouble(dt.Rows[i]["TOPYTONGAMT1"].ToString().Trim()) + dInCAmt1) * Convert.ToDouble(dt.Rows[i]["TKINRATE2"].ToString().Trim()));
                                        dInCAmt_Add2 = Math.Floor((Convert.ToDouble(dt.Rows[i]["TOPYTONGAMT2"].ToString().Trim()) + dInCAmt2) * Convert.ToDouble(dt.Rows[i]["TKINRATE2"].ToString().Trim()));
                                        dInCAmt_Add3 = Math.Floor((Convert.ToDouble(dt.Rows[i]["TOPYTONGAMT3"].ToString().Trim()) + dInCAmt3) * Convert.ToDouble(dt.Rows[i]["TKINRATE2"].ToString().Trim()));
                                    }
                                }

                                //퇴충금 예상일경우 OT평균 시간을 구해서 넣는다
                                //평균ot시간 구하기
                                fsAvgOttime = "0";

                                if ((this.DAT02_TLJKCD.GetValue().ToString() == "2C" || this.DAT02_TLJKCD.GetValue().ToString() == "3A" || this.DAT02_TLJKCD.GetValue().ToString() == "3B" ||
                                      this.DAT02_TLJKCD.GetValue().ToString() == "3C" || this.DAT02_TLJKCD.GetValue().ToString() == "3D" || this.DAT02_TLJKCD.GetValue().ToString() == "6C") ||
                                    (this.DAT02_TLSABUN.GetValue().ToString() == "0081-M" || this.DAT02_TLSABUN.GetValue().ToString() == "0079-M")
                                    )
                                {
                                    fsAvgOttime = dt.Rows[i]["AVGOTTIME"].ToString().Trim();
                                }

                                // 1
                                UP_Set_DBMPayAvg(dAvgM_DB, dt.Rows[i]["KBSABUN"].ToString().Trim(), dt.Rows[i]["TOPYMDATE1"].ToString().Trim().Substring(0, 6), dInCAmt1, dInCAmt_Add1, dt.Rows[i]["TOCOMGUBN"].ToString().Trim());
                                // 2
                                UP_Set_DBMPayAvg(dAvgM_DB, dt.Rows[i]["KBSABUN"].ToString().Trim(), dt.Rows[i]["TOPYMDATE2"].ToString().Trim().Substring(0, 6), dInCAmt2, dInCAmt_Add2, dt.Rows[i]["TOCOMGUBN"].ToString().Trim());
                                // 3
                                UP_Set_DBMPayAvg(dAvgM_DB, dt.Rows[i]["KBSABUN"].ToString().Trim(), dt.Rows[i]["TOPYMDATE3"].ToString().Trim().Substring(0, 6), dInCAmt3, dInCAmt_Add3, dt.Rows[i]["TOCOMGUBN"].ToString().Trim());

                                //평균상여 계산
                                UP_Set_DBSPayAvg(dt.Rows[i]["KBSABUN"].ToString().Trim(), dt.Rows[i]["TOPYMDATE3"].ToString().Trim().Substring(0, 6), dInCAmt3, dInCAmt_Add3, dt.Rows[i]["TOCOMGUBN"].ToString().Trim());

                                //년차 계산
                                UP_Set_YPayCompute(dt.Rows[i]["KBSABUN"].ToString().Trim(), dt.Rows[i]["TOYCHADATE"].ToString().Trim().Substring(0, 6));


                                UP_Add_Calcuate(dt, i, sKBPENSGUBN);

                            }
                            else
                            {
                                //실적
                                dInCAmt1 = 0;
                                dInCAmt2 = 0;
                                dInCAmt3 = 0;

                                //평균급여 계산
                                // 1
                                UP_Set_DBMPayAvg(dAvgM_DB, dt.Rows[i]["KBSABUN"].ToString().Trim(), dt.Rows[i]["TOPYMDATE1"].ToString().Trim().Substring(0, 6), (dInCAmt1 + dInCAmt2 + dInCAmt3), (dInCAmt_Add1 + dInCAmt_Add2 + dInCAmt_Add3), dt.Rows[i]["TOCOMGUBN"].ToString().Trim());
                                // 2
                                UP_Set_DBMPayAvg(dAvgM_DB, dt.Rows[i]["KBSABUN"].ToString().Trim(), dt.Rows[i]["TOPYMDATE2"].ToString().Trim().Substring(0, 6), (dInCAmt1 + dInCAmt2 + dInCAmt3), (dInCAmt_Add1 + dInCAmt_Add2 + dInCAmt_Add3), dt.Rows[i]["TOCOMGUBN"].ToString().Trim());
                                // 3
                                UP_Set_DBMPayAvg(dAvgM_DB, dt.Rows[i]["KBSABUN"].ToString().Trim(), dt.Rows[i]["TOPYMDATE3"].ToString().Trim().Substring(0, 6), (dInCAmt1 + dInCAmt2 + dInCAmt3), (dInCAmt_Add1 + dInCAmt_Add2 + dInCAmt_Add3), dt.Rows[i]["TOCOMGUBN"].ToString().Trim());

                                //평균상여 계산
                                UP_Set_DBSPayAvg(dt.Rows[i]["KBSABUN"].ToString().Trim(), dt.Rows[i]["TOPYMDATE3"].ToString().Trim().Substring(0, 6), (dInCAmt1 + dInCAmt2 + dInCAmt3), (dInCAmt_Add1 + dInCAmt_Add2 + dInCAmt_Add3), dt.Rows[i]["TOCOMGUBN"].ToString().Trim());

                                //년차 계산
                                UP_Set_YPayCompute(dt.Rows[i]["KBSABUN"].ToString().Trim(), dt.Rows[i]["TOYCHADATE"].ToString().Trim().Substring(0, 6));

                                UP_Add_Calcuate(dt, i, sKBPENSGUBN);
                            }
                            #endregion
                        }
                        else
                        {
                            #region  Description : DC형 퇴충금 계산
                            if (dt.Rows[i]["TOCOMGUBN"].ToString().Trim() == "1" || dt.Rows[i]["TOCOMGUBN"].ToString().Trim() == "3") //예상
                            {
                                //해당월에 통상임금 조회
                                //인상액 계산
                                if (dt.Rows[i]["TKPAYGN"].ToString().Trim() == "1") //1-통상임금 기준
                                {
                                    dInCAmt1 = Math.Floor(Convert.ToDouble(dt.Rows[i]["TOPYTONGAMT1"].ToString().Trim()) * Convert.ToDouble(dt.Rows[i]["TKINRATE1"].ToString().Trim()));
                                    dInCAmt2 = Math.Floor(Convert.ToDouble(dt.Rows[i]["TOPYTONGAMT2"].ToString().Trim()) * Convert.ToDouble(dt.Rows[i]["TKINRATE1"].ToString().Trim()));
                                    dInCAmt3 = Math.Floor(Convert.ToDouble(dt.Rows[i]["TOPYTONGAMT3"].ToString().Trim()) * Convert.ToDouble(dt.Rows[i]["TKINRATE1"].ToString().Trim()));

                                    //추가인상 적용
                                    if (Convert.ToDouble(dt.Rows[i]["TKINRATE2"].ToString().Trim()) > 0)
                                    {
                                        dInCAmt_Add1 = Math.Floor((Convert.ToDouble(dt.Rows[i]["TOPYTONGAMT1"].ToString().Trim()) + dInCAmt1) * Convert.ToDouble(dt.Rows[i]["TKINRATE2"].ToString().Trim()));
                                        dInCAmt_Add2 = Math.Floor((Convert.ToDouble(dt.Rows[i]["TOPYTONGAMT2"].ToString().Trim()) + dInCAmt2) * Convert.ToDouble(dt.Rows[i]["TKINRATE2"].ToString().Trim()));
                                        dInCAmt_Add3 = Math.Floor((Convert.ToDouble(dt.Rows[i]["TOPYTONGAMT3"].ToString().Trim()) + dInCAmt3) * Convert.ToDouble(dt.Rows[i]["TKINRATE2"].ToString().Trim()));
                                    }
                                }

                                if (dt.Rows[i]["TKPAYGN"].ToString().Trim() == "2") //2-지급금액 기준
                                {
                                    //2-지급합계 기준
                                    dInCAmt1 = Math.Floor(Convert.ToDouble(dt.Rows[i]["TOPYTONGAMT1"].ToString().Trim()) * Convert.ToDouble(dt.Rows[i]["TKINRATE1"].ToString().Trim()));
                                    dInCAmt2 = Math.Floor(Convert.ToDouble(dt.Rows[i]["TOPYTONGAMT2"].ToString().Trim()) * Convert.ToDouble(dt.Rows[i]["TKINRATE1"].ToString().Trim()));
                                    dInCAmt3 = Math.Floor(Convert.ToDouble(dt.Rows[i]["TOPYTONGAMT3"].ToString().Trim()) * Convert.ToDouble(dt.Rows[i]["TKINRATE1"].ToString().Trim()));

                                    //추가인상 적용
                                    if (Convert.ToDouble(dt.Rows[i]["TKINRATE2"].ToString().Trim()) > 0)
                                    {
                                        dInCAmt_Add1 = Math.Floor((Convert.ToDouble(dt.Rows[i]["TOPYTONGAMT1"].ToString().Trim()) + dInCAmt1) * Convert.ToDouble(dt.Rows[i]["TKINRATE2"].ToString().Trim()));
                                        dInCAmt_Add2 = Math.Floor((Convert.ToDouble(dt.Rows[i]["TOPYTONGAMT2"].ToString().Trim()) + dInCAmt2) * Convert.ToDouble(dt.Rows[i]["TKINRATE2"].ToString().Trim()));
                                        dInCAmt_Add3 = Math.Floor((Convert.ToDouble(dt.Rows[i]["TOPYTONGAMT3"].ToString().Trim()) + dInCAmt3) * Convert.ToDouble(dt.Rows[i]["TKINRATE2"].ToString().Trim()));
                                    }
                                }

                                //퇴충금 예상일경우 OT평균 시간을 구해서 넣는다
                                //평균ot시간 구하기
                                fsAvgOttime = "0";

                                if ((this.DAT02_TLJKCD.GetValue().ToString() == "2C" || this.DAT02_TLJKCD.GetValue().ToString() == "3A" || this.DAT02_TLJKCD.GetValue().ToString() == "3B" ||
                                      this.DAT02_TLJKCD.GetValue().ToString() == "3C" || this.DAT02_TLJKCD.GetValue().ToString() == "3D" || this.DAT02_TLJKCD.GetValue().ToString() == "6C") ||
                                    (this.DAT02_TLSABUN.GetValue().ToString() == "0081-M" || this.DAT02_TLSABUN.GetValue().ToString() == "0079-M")
                                    )
                                {
                                    fsAvgOttime = dt.Rows[i]["AVGOTTIME"].ToString().Trim();
                                }

                                for (int j = 0; j < 12; j++)
                                {
                                    sYYMM = dt.Rows[i]["TOPYMDATE3"].ToString().Trim().Substring(0, 4) + Set_Fill2((j + 1).ToString());                                    

                                    //평균급여 계산
                                    if ( Convert.ToInt32(sYYMM)  <= Convert.ToInt32(dt.Rows[i]["TOPYMDATE3"].ToString().Trim().Substring(0,6)) )
                                    {
                                        fsTOPYMDATE3 = sYYMM;

                                        if (Convert.ToInt32(sYYMM.Substring(4,2).Trim()) >= 3)
                                        {
                                            UP_Set_DCMPayAvg(dAvgM_DC, dt.Rows[i]["KBSABUN"].ToString().Trim(), sYYMM, (dInCAmt1 ), (dInCAmt_Add1), dt.Rows[i]["TOCOMGUBN"].ToString().Trim(), "1");
                                        }
                                        else
                                        {
                                            if (dt.Rows[i]["TOCOMGUBN"].ToString().Trim() == "1" )  //당해예상
                                            {
                                                UP_Set_DCMPayAvg(dAvgM_DC, dt.Rows[i]["KBSABUN"].ToString().Trim(), sYYMM, 0, 0, dt.Rows[i]["TOCOMGUBN"].ToString().Trim(), "1");
                                            }
                                            else if (dt.Rows[i]["TOCOMGUBN"].ToString().Trim() == "3")  //내년예상
                                            {
                                                UP_Set_DCMPayAvg(dAvgM_DC, dt.Rows[i]["KBSABUN"].ToString().Trim(), sYYMM, (dInCAmt1), 0, dt.Rows[i]["TOCOMGUBN"].ToString().Trim(), "1");
                                            }
                                            else
                                            {
                                                UP_Set_DCMPayAvg(dAvgM_DC, dt.Rows[i]["KBSABUN"].ToString().Trim(), sYYMM, (dInCAmt1), (dInCAmt_Add1), dt.Rows[i]["TOCOMGUBN"].ToString().Trim(), "1");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        fsTOPYMDATE3 = dt.Rows[i]["TOPYMDATE3"].ToString().Trim().Substring(0, 6);
                                        UP_Set_DCMPayAvg(dAvgM_DC, dt.Rows[i]["KBSABUN"].ToString().Trim(), sYYMM, (dInCAmt1 ), (dInCAmt_Add1 ), dt.Rows[i]["TOCOMGUBN"].ToString().Trim(), "2");
                                    }
                                }                                

                                //평균상여 계산
                                UP_Set_DCSPayAvg(dt.Rows[i]["KBSABUN"].ToString().Trim(), dt.Rows[i]["TOPYMDATE3"].ToString().Trim().Substring(0, 6), (dInCAmt1 ), (dInCAmt_Add1 ), dt.Rows[i]["TOCOMGUBN"].ToString().Trim());

                                //년차 계산
                                UP_Set_YPayCompute(dt.Rows[i]["KBSABUN"].ToString().Trim(), dt.Rows[i]["TOYCHADATE"].ToString().Trim().Substring(0, 6));

                                UP_Add_Calcuate(dt, i, sKBPENSGUBN);

                            }
                            else
                            {
                                //실적                                
                                dInCAmt1 = 0;
                                dInCAmt2 = 0;
                                dInCAmt3 = 0;

                                for (int j = 0; j < 12; j++)
                                {
                                    sYYMM = dt.Rows[i]["TOPYMDATE3"].ToString().Trim().Substring(0, 4) + Set_Fill2((j + 1).ToString());
                                    
                                    fsTOPYMDATE3 = sYYMM;

                                    //평균급여 계산                                    
                                    UP_Set_DCMPayAvg(dAvgM_DC, dt.Rows[i]["KBSABUN"].ToString().Trim(), sYYMM, (dInCAmt1 + dInCAmt2 + dInCAmt3), (dInCAmt_Add1 + dInCAmt_Add2 + dInCAmt_Add3), dt.Rows[i]["TOCOMGUBN"].ToString().Trim(), "1");
                                }

                                if (12 - fiAvgMCnt > 0)
                                {
                                    int iGap = 12 - fiAvgMCnt;

                                    for (int k = 1; k < iGap; k++)
                                    {
                                        UP_Set_DCMPayAvg(dAvgM_DC, dt.Rows[i]["KBSABUN"].ToString().Trim(), dt.Rows[i]["TOPYMDATE3"].ToString().Trim().Substring(0, 6), (dInCAmt1 + dInCAmt2 + dInCAmt3), (dInCAmt_Add1 + dInCAmt_Add2 + dInCAmt_Add3), dt.Rows[i]["TOCOMGUBN"].ToString().Trim(), "2");
                                    }
                                }

                                //평균상여 계산
                                UP_Set_DCSPayAvg(dt.Rows[i]["KBSABUN"].ToString().Trim(), dt.Rows[i]["TOPYMDATE3"].ToString().Trim().Substring(0, 6), (dInCAmt1 + dInCAmt2 + dInCAmt3), (dInCAmt_Add1 + dInCAmt_Add2 + dInCAmt_Add3), dt.Rows[i]["TOCOMGUBN"].ToString().Trim());

                                //년차 계산
                                UP_Set_YPayCompute(dt.Rows[i]["KBSABUN"].ToString().Trim(), dt.Rows[i]["TOYCHADATE"].ToString().Trim().Substring(0, 6));

                                UP_Add_Calcuate(dt, i, sKBPENSGUBN);
                            }
                            #endregion

                        }

                        pgb_Status.Value = pgb_Status.Value + 1;
                    }
                }
            }
            catch( Exception ex )
            {

                string dd = ex.Message;
            }
            finally
            {
                if (datamf.Count > 0)
                {
                    this.DbConnector.CommandClear();
                    foreach (object[] data in datamf)
                    {
                        this.DbConnector.Attach("TY_P_HR_84NBT852", data);
                    }
                }

                if (datasf.Count > 0)
                {
                    foreach (object[] data in datasf)
                    {
                        this.DbConnector.Attach("TY_P_HR_84GEH816", data);
                    }
                }

                if (dataxf.Count > 0)
                {
                    foreach (object[] data in dataxf)
                    {
                        this.DbConnector.Attach("TY_P_HR_BC1FF852", data);
                    }
                }


                if (this.DbConnector.CommandCount > 0)
                {
                    this.DbConnector.ExecuteTranQueryList();
                }             
            }

            
        }       
        #endregion

        #region  Description : 추계액 등록
        private void UP_Add_Calcuate(DataTable dt, int i, string sKBPENSGUBN)
        {
            //급여평균
            double dMPayAvg = 0;
            //상여평균
            double dSPayAvg = 0;

            try
            {
                //근무년수 계산
                string sGeukMuYear = UP_Get_WorkYears(Convert.ToDateTime(Set_Date(this.DAT02_TLKBDATE.GetValue().ToString())), Convert.ToDateTime(Set_Date(this.DAT02_TLCOMDATE.GetValue().ToString())));

                double iYear = Convert.ToDouble(sGeukMuYear.Substring(0, 4));
                double iMonth = Convert.ToDouble(sGeukMuYear.Substring(5, 2));
                double iDay = Convert.ToDouble(sGeukMuYear.Substring(8, 2));

                if (iDay >= 31)
                {
                    iMonth = iMonth + 1;
                    iDay = 0;
                }

                if (iMonth >= 12)
                {
                    iYear = iYear + 1;
                    iMonth = 0;
                }

                if (sKBPENSGUBN != "DC")
                {
                    //급여평균
                     dMPayAvg = Math.Floor(fdMPayTotal / 3);
                    //상여평균
                     dSPayAvg = Math.Floor((fdSPayTotal + fdYPayTotal) / 12);
                }
                else
                {
                    dMPayAvg = Math.Floor((fdMPayTotal + fdSPayTotal + fdYPayTotal) / 12);
                }                

                double dYearPay = (dMPayAvg + dSPayAvg) * iYear;
                double dMonthPay = Math.Round((dMPayAvg + dSPayAvg) * Convert.ToDouble(iMonth / 12));
                double dDayPay = Math.Round((dMPayAvg + dSPayAvg) * Convert.ToDouble(iDay / 365));

                double dPYChuKyeAmt = dYearPay + dMonthPay + dDayPay;

                double dWkChuKyeAmt = 0;

                //임원경우 배수 적용
                if (this.DAT02_TLJKCD.GetValue().ToString() == "01")
                {
                    dPYChuKyeAmt = 0;

                    double iKiBonBeSu = 0;

                    double iBeSu = Convert.ToDouble(dt.Rows[i]["TFBAESU"].ToString().Trim());

                    //switch (dt.Rows[i]["KBJJCD"].ToString().Trim())
                    //{
                    //    case "025": //대표사장
                    //        iKiBonBeSu = 4;
                    //        break;
                    //    case "030": //사장
                    //        iKiBonBeSu = 4;
                    //        break;
                    //    case "040": //부사장
                    //        iKiBonBeSu = 3;
                    //        break;
                    //    case "050": //전무
                    //        iKiBonBeSu = 3;
                    //        break;
                    //    case "055": //상무
                    //        iKiBonBeSu = 2;
                    //        break;
                    //    case "060": //상무(갑)
                    //        iKiBonBeSu = 2;
                    //        break;
                    //    case "070": //상무(을)
                    //        iKiBonBeSu = 2;
                    //        break;
                    //    case "080": //상무보
                    //        iKiBonBeSu = 2;
                    //        break;
                    //}

                    //if (iBeSu > 0)
                    //{
                    //    dPYChuKyeAmt = Convert.ToDouble(Math.Round((dYearPay + dMonthPay + dDayPay) * iBeSu));
                    //}
                    //else
                    //{
                    //    dPYChuKyeAmt = Convert.ToDouble(Math.Round((dYearPay + dMonthPay + dDayPay) * iKiBonBeSu));
                    //}

                    //임원일 경우 임원배수 파일 조회해서 계산 
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_BB9BH721", DAT02_TLSABUN.GetValue().ToString());
                    DataTable dtex = this.DbConnector.ExecuteDataTable();

                    if (dtex.Rows.Count > 0)
                    {

                        string sExGeukMuYear = "";

                        double iExYear = 0;
                        double iExMonth = 0;
                        double iExDay = 0;

                        for (int j = 0; j < dtex.Rows.Count; j++)
                        {
                            string sEndDate = dtex.Rows[j]["KXEDATE"].ToString() != "" ? dtex.Rows[j]["KXEDATE"].ToString() : this.DAT02_TLCOMDATE.GetValue().ToString();

                            sExGeukMuYear = UP_Get_WorkYears(Convert.ToDateTime(Set_Date(dtex.Rows[j]["KXSDATE"].ToString())), Convert.ToDateTime(Set_Date(sEndDate)));

                            iExYear = Convert.ToDouble(sExGeukMuYear.Substring(0, 4));
                            iExMonth = Convert.ToDouble(sExGeukMuYear.Substring(5, 2));
                            iExDay = Convert.ToDouble(sExGeukMuYear.Substring(8, 2));

                            if (iExDay >= 31)
                            {
                                iExMonth = iExMonth + 1;
                                iExDay = 0;
                            }

                            if (iExMonth >= 12)
                            {
                                iExYear = iExYear + 1;
                                iExMonth = 0;
                            }

                            iBeSu = Convert.ToDouble(dtex.Rows[j]["KXRATENUM"].ToString());

                            dYearPay = (dMPayAvg + dSPayAvg) * iExYear;
                            dMonthPay = Math.Round((dMPayAvg + dSPayAvg) * Convert.ToDouble(iExMonth / 12));
                            dDayPay = Math.Round((dMPayAvg + dSPayAvg) * Convert.ToDouble(iExDay / 365));

                            dWkChuKyeAmt = Convert.ToDouble(Math.Round((dYearPay + dMonthPay + dDayPay) * iBeSu));

                            dataxf.Add(new object[] {              DAT02_TLYEAR.GetValue().ToString(),
                                                                   DAT02_TLSEQ.GetValue().ToString(),
                                                                   DAT02_TLSABUN.GetValue().ToString(),
                                                                   (j+1).ToString(),
                                                                   dtex.Rows[j]["KXSDATE"].ToString(),
                                                                   sEndDate,
                                                                   iExYear.ToString(),
                                                                   iExMonth.ToString(),
                                                                   iExDay.ToString(),
                                                                   dtex.Rows[j]["KXRATENUM"].ToString(),
                                                                   dWkChuKyeAmt.ToString(),
                                                                   TYUserInfo.EmpNo
                                                 });
                            if( dWkChuKyeAmt > 0 )
                            {
                                dPYChuKyeAmt += dWkChuKyeAmt;
                            }

                            dWkChuKyeAmt = 0;

                        }
                    }


                }

                if (dPYChuKyeAmt > 0)
                {
                    //this.DbConnector.CommandClear();
                    //this.DbConnector.Attach("TY_P_HR_84NBT852", DAT02_TLYEAR.GetValue().ToString(),
                    //                                               DAT02_TLSEQ.GetValue().ToString(),
                    //                                               DAT02_TLSABUN.GetValue().ToString(),
                    //                                               dt.Rows[i]["KBHANGL"].ToString().Trim(),
                    //                                               this.DAT02_TLCOMDATE.GetValue().ToString(),
                    //                                               this.DAT02_TLKBDATE.GetValue().ToString(),
                    //                                               dt.Rows[i]["KBJKCD"].ToString().Trim(),
                    //                                               dt.Rows[i]["KBBUSEO"].ToString().Trim(),
                    //                                               iYear.ToString(),
                    //                                               iMonth.ToString(),
                    //                                               iDay.ToString(),
                    //                                               dMPayAvg.ToString(),
                    //                                               dSPayAvg.ToString(),
                    //                                               (dMPayAvg + dSPayAvg).ToString(),
                    //                                               fsAvgOttime,
                    //                                               dPYChuKyeAmt.ToString(),
                    //                                               TYUserInfo.EmpNo);
                    //this.DbConnector.ExecuteTranQuery();

                    datamf.Add(new object[] {   DAT02_TLYEAR.GetValue().ToString(),
                                                                   DAT02_TLSEQ.GetValue().ToString(),
                                                                   DAT02_TLSABUN.GetValue().ToString(),
                                                                   dt.Rows[i]["KBHANGL"].ToString().Trim(),
                                                                   this.DAT02_TLCOMDATE.GetValue().ToString(),
                                                                   this.DAT02_TLKBDATE.GetValue().ToString(),
                                                                   dt.Rows[i]["KBJKCD"].ToString().Trim(),
                                                                   dt.Rows[i]["KBBUSEO"].ToString().Trim(),
                                                                   DAT02_TLTYPE.GetValue().ToString(),
                                                                   iYear.ToString(),
                                                                   iMonth.ToString(),
                                                                   iDay.ToString(),
                                                                   dMPayAvg.ToString(),
                                                                   dSPayAvg.ToString(),
                                                                   (dMPayAvg + dSPayAvg).ToString(),
                                                                   fsAvgOttime,
                                                                   dPYChuKyeAmt.ToString(),
                                                                   TYUserInfo.EmpNo
                                                 });
                }
            }
            catch
            {
            }

        }
        #endregion

        #region  Description : 월 평균 급여 계산(DB형)
        private void UP_Set_DBMPayAvg(DataTable dAvgM, string sSABUN, string sYYMM, double dInCAmt, double dInCAmt_Add, string sTOCOMGUBN)
        {
            double dPyAmount = 0;
            double dPyTongSangAmt = 0;  //통상임금
            double dPSPAYAMOUNT = 0;  //지급금액
            double dBonusInCAmt = 0;

            bool bFixOttimeCheck = false;  //utt 고정ot수당이 있는사람

            
            DataTable dt = dAvgM.Clone();
            foreach (DataRow dr in dAvgM.Select("PSYYMM = '" + sYYMM + "' AND PSSABUN = '" + sSABUN+"'"))
                    dt.Rows.Add(dr.ItemArray);            
 
            if (dt.Rows.Count > 0)
            {
                //this.DbConnector.CommandClear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //통상임금
                    dPyTongSangAmt = Convert.ToDouble(dt.Rows[i]["TONGSANGAMT"].ToString().Trim());
                    //지급금액
                    dPSPAYAMOUNT = Convert.ToDouble(dt.Rows[i]["PSPAYAMOUNT"].ToString().Trim());

                    if (dt.Rows[i]["PSPAYCODE"].ToString().Trim() == "1001")
                    {
                        dPyAmount = Convert.ToDouble(dt.Rows[i]["PSPAYAMOUNT"].ToString().Trim()) + dInCAmt + dInCAmt_Add;
                    }
                    else
                    {
                        dPyAmount = Convert.ToDouble(dt.Rows[i]["PSPAYAMOUNT"].ToString().Trim());
                    }

                    if (sTOCOMGUBN == "1" || sTOCOMGUBN == "3") //예상
                    {
                        if (dt.Rows[i]["PSPAYCODE"].ToString().Trim() == "1408")
                        {
                            bFixOttimeCheck = true;
                        }

                        if (dt.Rows[i]["PSPAYCODE"].ToString().Trim() != "1401" && dt.Rows[i]["PSPAYCODE"].ToString().Trim() != "1402" &&
                            dt.Rows[i]["PSPAYCODE"].ToString().Trim() != "1403" && dt.Rows[i]["PSPAYCODE"].ToString().Trim() != "1404" &&
                            dt.Rows[i]["PSPAYCODE"].ToString().Trim() != "1405" && dt.Rows[i]["PSPAYCODE"].ToString().Trim() != "1408")
                        {                           

                            datasf.Add(new object[] {   DAT02_TLYEAR.GetValue().ToString(),
                                                                        DAT02_TLSEQ.GetValue().ToString(),
                                                                        DAT02_TLSABUN.GetValue().ToString(),
                                                                        dt.Rows[i]["PSGUBN"].ToString().Trim(),
                                                                        dt.Rows[i]["PSPAYCODE"].ToString().Trim(),
                                                                        dt.Rows[i]["PSYYMM"].ToString().Trim(),
                                                                        dPSPAYAMOUNT.ToString().Trim(),
                                                                        (dPyAmount - dPSPAYAMOUNT).ToString().Trim(),
                                                                        dt.Rows[i]["SOAMT"].ToString().Trim(),
                                                                        "0",
                                                                        (dPyAmount + Convert.ToDouble(dt.Rows[i]["SOAMT"].ToString().Trim())).ToString(),
                                                                        TYUserInfo.EmpNo
                                                 });

                            fdMPayTotal += dPyAmount + Convert.ToDouble(dt.Rows[i]["SOAMT"].ToString().Trim());
                        }
                    }
                    else //실적
                    {

                        datasf.Add(new object[] {  DAT02_TLYEAR.GetValue().ToString(),
                                                                        DAT02_TLSEQ.GetValue().ToString(),
                                                                        DAT02_TLSABUN.GetValue().ToString(),
                                                                        dt.Rows[i]["PSGUBN"].ToString().Trim(),
                                                                        dt.Rows[i]["PSPAYCODE"].ToString().Trim(),
                                                                        dt.Rows[i]["PSYYMM"].ToString().Trim(),
                                                                        dt.Rows[i]["PSPAYAMOUNT"].ToString().Trim(),
                                                                        "0",
                                                                        dt.Rows[i]["SOAMT"].ToString().Trim(),
                                                                        "0",
                                                                        (Convert.ToDouble(dt.Rows[i]["PSPAYAMOUNT"].ToString().Trim()) + Convert.ToDouble(dt.Rows[i]["SOAMT"].ToString().Trim())).ToString(),
                                                                        TYUserInfo.EmpNo
                                                 });

                        fdMPayTotal += dPyAmount + Convert.ToDouble(dt.Rows[i]["SOAMT"].ToString().Trim());
                    }



                    //마지막 레코드
                    if (i == (dt.Rows.Count - 1))
                    {
                        if (sTOCOMGUBN == "1" || sTOCOMGUBN == "3") //예상
                        {
                            if (Convert.ToDouble(fsAvgOttime) > 0)
                            {
                                //ot금액 계산 = 통상임금 / 187 * ot시간
                                //상여금의 인상액 반영 계산
                                if (Convert.ToInt16(dt.Rows[i]["PSYYMM"].ToString().Trim().Substring(4, 2)) <= 2)
                                {
                                    dBonusInCAmt = Math.Floor(dInCAmt / 2);
                                }
                                else
                                {
                                    dBonusInCAmt = dInCAmt_Add > 0 ? Math.Floor(dInCAmt / 2) + Math.Floor(dInCAmt_Add / 2) : Math.Floor(dInCAmt / 2);
                                }

                                double dOtAmt = Math.Floor(((dPyTongSangAmt + dInCAmt + dInCAmt_Add + dBonusInCAmt) / 187) * Convert.ToDouble(fsAvgOttime));                             

                                datasf.Add(new object[] {  DAT02_TLYEAR.GetValue().ToString(),
                                                                                        DAT02_TLSEQ.GetValue().ToString(),
                                                                                        DAT02_TLSABUN.GetValue().ToString(),
                                                                                        dt.Rows[i]["PSGUBN"].ToString().Trim(),
                                                                                        "1402",
                                                                                        dt.Rows[i]["PSYYMM"].ToString().Trim(),
                                                                                        dOtAmt.ToString().Trim(),
                                                                                        "0",
                                                                                        "0",
                                                                                        fsAvgOttime,
                                                                                        dOtAmt.ToString().Trim(),
                                                                                        TYUserInfo.EmpNo
                                                 });    
                                fdMPayTotal += dOtAmt;
                            }

                            //고정ot수당이 있는 사람
                            if (bFixOttimeCheck)
                            {
                                //ot금액 계산 = 통상임금 / 187 * ot시간
                                //상여금의 인상액 반영 계산
                                if (Convert.ToInt16(dt.Rows[i]["PSYYMM"].ToString().Trim().Substring(4, 2)) <= 2)
                                {
                                    dBonusInCAmt = Math.Floor(dInCAmt / 2);
                                }
                                else
                                {
                                    dBonusInCAmt = dInCAmt_Add > 0 ? Math.Floor(dInCAmt / 2) + Math.Floor(dInCAmt_Add / 2) : Math.Floor(dInCAmt / 2);
                                }

                                double dOtAmt = Math.Floor(((dPyTongSangAmt + dInCAmt + dInCAmt_Add + dBonusInCAmt) / 187) * 13);

                                datasf.Add(new object[] {  DAT02_TLYEAR.GetValue().ToString(),
                                                                                        DAT02_TLSEQ.GetValue().ToString(),
                                                                                        DAT02_TLSABUN.GetValue().ToString(),
                                                                                        dt.Rows[i]["PSGUBN"].ToString().Trim(),
                                                                                        "1408",
                                                                                        dt.Rows[i]["PSYYMM"].ToString().Trim(),
                                                                                        dOtAmt.ToString().Trim(),
                                                                                        "0",
                                                                                        "0",
                                                                                        "13",
                                                                                        dOtAmt.ToString().Trim(),
                                                                                        TYUserInfo.EmpNo
                                                 });
                                fdMPayTotal += dOtAmt;
                            }
                        }
                    }

                }

            }



        }
        #endregion

        #region  Description : 월 평균 상여 계산(DB형)
        private void UP_Set_DBSPayAvg(string sSABUN, string sYYMM, double dInCAmt, double dInCAmt_Add, string sTOCOMGUBN)
        {
            int iSPYCOUNT = 0;  //상여 횟수 카운트
            int iS2COUNT = 0;  //명절상여 횟수 카운트

            int iH1COUNT = 0;  //하기휴가비 카운트

            double dPyAmount = 0;
            double dPSPAYAMOUNT = 0;

            string sPSGUBN = "";
            string sPSYYMM = "";
            string sPSJIDATE = "";

            string sNextPSJIDATE = "";
            string sNextPSYYMM = "";
                    

            if (dInCAmt > 0)
            {
                dInCAmt = Math.Floor(dInCAmt / 2);
            }

            if (dInCAmt_Add > 0)
            {
                dInCAmt_Add = Math.Floor(dInCAmt_Add / 2);
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_84IBF835", fsTOPYSDATE_FROM, fsTOPYSDATE_TO);
            DataTable dm = this.DbConnector.ExecuteDataTable();
            if (dm.Rows.Count > 0)
            {
                if (sTOCOMGUBN == "1" || sTOCOMGUBN == "2")
                {
                    #region  Description : 다음년도 예상이 아닌경우(실적)

                    this.DbConnector.CommandClear();
                    for (int i = 0; i < dm.Rows.Count; i++)
                    {
                        if (dm.Rows[i]["PSGUBN"].ToString().Trim().Substring(0, 1) == "S")
                        {
                            iSPYCOUNT += 1;

                            //마지막 구분 내용 저장
                            if (dm.Rows[i]["PSGUBN"].ToString().Trim() == "S1")
                            {
                                sPSGUBN = dm.Rows[i]["PSGUBN"].ToString().Trim();
                                sPSYYMM = dm.Rows[i]["PSYYMM"].ToString().Trim();
                                sPSJIDATE = dm.Rows[i]["PSJIDATE"].ToString().Trim();
                            }

                            //명절상여 횟수 저장
                            if (dm.Rows[i]["PSGUBN"].ToString().Trim() == "S2")
                            {
                                iS2COUNT += 1;
                            }
                        }

                        //하기휴가비 체크
                        if (dm.Rows[i]["PSGUBN"].ToString().Trim().Substring(0, 1) == "H")
                        {
                            iH1COUNT += 1;
                        }

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_HR_84IBM836", dm.Rows[i]["PSGUBN"].ToString().Trim(), dm.Rows[i]["PSYYMM"].ToString().Trim(), dm.Rows[i]["PSJIDATE"].ToString().Trim(), sSABUN, dm.Rows[i]["PSYYMM"].ToString().Trim().Substring(0, 4));
                        DataTable dt = this.DbConnector.ExecuteDataTable();
                        if (dt.Rows.Count > 0)
                        {
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                //지급금액
                                dPSPAYAMOUNT = Convert.ToDouble(dt.Rows[j]["PSPAYAMOUNT"].ToString().Trim());

                                if (dt.Rows[j]["PSPAYCODE"].ToString().Trim() == "1001")
                                {
                                    //2월이전이면 인상액 없음.
                                    if (Convert.ToInt16(dt.Rows[j]["PSYYMM"].ToString().Trim().Substring(4, 2)) <= 2)
                                    {
                                        if (dInCAmt_Add > 0)
                                        {
                                            dPyAmount = Convert.ToDouble(dt.Rows[j]["PSPAYAMOUNT"].ToString().Trim()) + dInCAmt;
                                        }
                                        else
                                        {
                                            dPyAmount = Convert.ToDouble(dt.Rows[j]["PSPAYAMOUNT"].ToString().Trim());
                                        }
                                    }
                                    else
                                    {
                                        if (dt.Rows[j]["PSGUBN"].ToString().Trim().Substring(0, 1) == "H")  //하기휴가비는 인상액 없음
                                        {
                                            dPyAmount = Convert.ToDouble(dt.Rows[j]["PSPAYAMOUNT"].ToString().Trim());
                                        }
                                        else
                                        {
                                            dPyAmount = Convert.ToDouble(dt.Rows[j]["PSPAYAMOUNT"].ToString().Trim()) + dInCAmt + dInCAmt_Add;                                            
                                        }
                                    }
                                }
                                else
                                {
                                    dPyAmount = Convert.ToDouble(dt.Rows[j]["PSPAYAMOUNT"].ToString().Trim());
                                }

                                datasf.Add(new object[] {  DAT02_TLYEAR.GetValue().ToString(),
                                                                               DAT02_TLSEQ.GetValue().ToString(),
                                                                               DAT02_TLSABUN.GetValue().ToString(),
                                                                               dt.Rows[j]["PSGUBN"].ToString().Trim(),
                                                                               dt.Rows[j]["PSPAYCODE"].ToString().Trim(),
                                                                               dt.Rows[j]["PSYYMM"].ToString().Trim(),
                                                                               dPSPAYAMOUNT.ToString().Trim(),
                                                                               (dPyAmount - dPSPAYAMOUNT).ToString(),
                                                                               dt.Rows[j]["SOAMT"].ToString().Trim(),
                                                                               "0",
                                                                               (dPyAmount + Convert.ToDouble(dt.Rows[j]["SOAMT"].ToString().Trim())).ToString(),
                                                                               TYUserInfo.EmpNo
                                                 });

                                fdSPayTotal += dPyAmount + Convert.ToDouble(dt.Rows[j]["SOAMT"].ToString().Trim());
                            }
                        }
                    }

                    //정기상여 12번 명절상여 2번 
                    if (iSPYCOUNT < 14)
                    {
                        //14번보다 부족하면 마직막 일반상여 정보를 기준으로 나머지 횟수를 계산한다.
                        DateTime dtYYMM = Convert.ToDateTime(Set_Date(sPSJIDATE));

                        for (int j = iSPYCOUNT; j < 14; j++)
                        {
                            sNextPSJIDATE = dtYYMM.AddMonths(1).Year.ToString() + Set_Fill2(dtYYMM.AddMonths(1).Month.ToString()) + Set_Fill2(dtYYMM.AddMonths(1).Day.ToString());
                            sNextPSYYMM = sNextPSJIDATE.Substring(0, 6);

                            dtYYMM = Convert.ToDateTime(Set_Date(sNextPSJIDATE));

                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_HR_84IBM836", sPSGUBN, sPSYYMM, sPSJIDATE, sSABUN, sPSYYMM.Substring(0, 4));
                            DataTable dk = this.DbConnector.ExecuteDataTable();
                            if (dk.Rows.Count > 0)
                            {
                                for (int i = 0; i < dk.Rows.Count; i++)
                                {
                                    //지급금액
                                    dPSPAYAMOUNT = Convert.ToDouble(dk.Rows[i]["PSPAYAMOUNT"].ToString().Trim());

                                    if (dk.Rows[i]["PSPAYCODE"].ToString().Trim() == "1001")
                                    {
                                        dPyAmount = Convert.ToDouble(dk.Rows[i]["PSPAYAMOUNT"].ToString().Trim()) + dInCAmt + dInCAmt_Add;
                                    }
                                    else
                                    {
                                        dPyAmount = Convert.ToDouble(dk.Rows[i]["PSPAYAMOUNT"].ToString().Trim());
                                    }

                                    datasf.Add(new object[] {   DAT02_TLYEAR.GetValue().ToString(),
                                                                                   DAT02_TLSEQ.GetValue().ToString(),
                                                                                   DAT02_TLSABUN.GetValue().ToString(),
                                                                                   dk.Rows[i]["PSGUBN"].ToString().Trim(),
                                                                                   dk.Rows[i]["PSPAYCODE"].ToString().Trim(),
                                                                                   sNextPSYYMM,
                                                                                   dPSPAYAMOUNT.ToString().Trim(),
                                                                                   (dPyAmount - dPSPAYAMOUNT).ToString().Trim(),
                                                                                   dk.Rows[i]["SOAMT"].ToString().Trim(),
                                                                                   "0",
                                                                                   (dPyAmount + Convert.ToDouble(dk.Rows[i]["SOAMT"].ToString().Trim())).ToString(),
                                                                                   TYUserInfo.EmpNo
                                                 });

                                    fdSPayTotal += dPyAmount + Convert.ToDouble(dk.Rows[i]["SOAMT"].ToString().Trim());

                                    //명절상여 2번이인데 1번이면 매년 9월을 추석으로 임의 지정하여 예상 작업시 등록해준다
                                    if (iS2COUNT <= 1 && sNextPSYYMM.Substring(4, 2) == "09")
                                    {
                                        datasf.Add(new object[] {   DAT02_TLYEAR.GetValue().ToString(),
                                                                                   DAT02_TLSEQ.GetValue().ToString(),
                                                                                   DAT02_TLSABUN.GetValue().ToString(),
                                                                                   "S2",
                                                                                   dk.Rows[i]["PSPAYCODE"].ToString().Trim(),
                                                                                   sNextPSYYMM,
                                                                                   dPSPAYAMOUNT.ToString().Trim(),
                                                                                   (dPyAmount - dPSPAYAMOUNT).ToString().Trim(),
                                                                                   dk.Rows[i]["SOAMT"].ToString().Trim(),
                                                                                   "0",
                                                                                   (dPyAmount + Convert.ToDouble(dk.Rows[i]["SOAMT"].ToString().Trim())).ToString(),
                                                                                   TYUserInfo.EmpNo
                                                 });

                                        fdSPayTotal += dPyAmount + Convert.ToDouble(dk.Rows[i]["SOAMT"].ToString().Trim());
                                    }
                                }

                                if (iS2COUNT <= 1 && sNextPSYYMM.Substring(4, 2) == "09")
                                {
                                    j += 1;
                                }
                            }
                        }
                    }

                    #endregion
                }
                else
                {
                    //다음년도 예상시
                    #region  Description : 다음년도 예상시
                    //정기상여 12번 명절상여 2번 
                    if (iSPYCOUNT < 14)
                    {
                        for (int i = 0; i < dm.Rows.Count; i++)
                        {
                            if (dm.Rows[i]["PSGUBN"].ToString().Trim().Substring(0, 1) == "S")
                            {
                                //마지막 구분 내용 저장
                                if (dm.Rows[i]["PSGUBN"].ToString().Trim() == "S1")
                                {
                                    sPSGUBN = dm.Rows[i]["PSGUBN"].ToString().Trim();
                                    sPSYYMM = dm.Rows[i]["PSYYMM"].ToString().Trim();
                                    sPSJIDATE = dm.Rows[i]["PSJIDATE"].ToString().Trim();                                    
                                }                               
                            }
                        }                        

                        //14번보다 부족하면 마직막 일반상여 정보를 기준으로 나머지 횟수를 계산한다.

                        DateTime dtYYMM = Convert.ToDateTime(Set_Date(this.DAT02_TLCOMDATE.GetValue().ToString().Substring(0, 4) + "0128"));
                        sNextPSJIDATE = dtYYMM.Year.ToString() + Set_Fill2(dtYYMM.Month.ToString()) + Set_Fill2(dtYYMM.Day.ToString());

                        for (int j = 0; j < 14; j++)
                        {
                            
                            sNextPSYYMM = sNextPSJIDATE.Substring(0, 6);

                            dtYYMM = Convert.ToDateTime(Set_Date(sNextPSJIDATE));

                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_HR_84IBM836", sPSGUBN, sPSYYMM, sPSJIDATE, sSABUN, sPSYYMM.Substring(0, 4));
                            DataTable dk = this.DbConnector.ExecuteDataTable();
                            if (dk.Rows.Count > 0)
                            {
                                for (int i = 0; i < dk.Rows.Count; i++)
                                {
                                    //지급금액
                                    dPSPAYAMOUNT = Convert.ToDouble(dk.Rows[i]["PSPAYAMOUNT"].ToString().Trim());

                                    if (dk.Rows[i]["PSPAYCODE"].ToString().Trim() == "1001")
                                    {
                                        if (Convert.ToInt16(sNextPSYYMM.Substring(4, 2).ToString()) <= 2)
                                        {
                                            dPyAmount = Convert.ToDouble(dk.Rows[i]["PSPAYAMOUNT"].ToString().Trim()) + dInCAmt;
                                        }
                                        else
                                        {
                                            dPyAmount = Convert.ToDouble(dk.Rows[i]["PSPAYAMOUNT"].ToString().Trim()) + dInCAmt + dInCAmt_Add;
                                        }
                                    }
                                    else
                                    {
                                        dPyAmount = Convert.ToDouble(dk.Rows[i]["PSPAYAMOUNT"].ToString().Trim());
                                    }

                                    datasf.Add(new object[] {   DAT02_TLYEAR.GetValue().ToString(),
                                                                                   DAT02_TLSEQ.GetValue().ToString(),
                                                                                   DAT02_TLSABUN.GetValue().ToString(),
                                                                                   dk.Rows[i]["PSGUBN"].ToString().Trim(),
                                                                                   dk.Rows[i]["PSPAYCODE"].ToString().Trim(),
                                                                                   sNextPSYYMM,
                                                                                   dPSPAYAMOUNT.ToString().Trim(),
                                                                                   (dPyAmount - dPSPAYAMOUNT).ToString().Trim(),
                                                                                   dk.Rows[i]["SOAMT"].ToString().Trim(),
                                                                                   "0",
                                                                                   (dPyAmount + Convert.ToDouble(dk.Rows[i]["SOAMT"].ToString().Trim())).ToString(),
                                                                                   TYUserInfo.EmpNo
                                                 });

                                    fdSPayTotal += dPyAmount + Convert.ToDouble(dk.Rows[i]["SOAMT"].ToString().Trim());                                    

                                    //명절상여 2번이인데 매년 2, 9월을 추석으로 임의 지정하여 예상 작업시 등록해준다
                                    if ( sNextPSYYMM.Substring(4, 2) == "02" || sNextPSYYMM.Substring(4, 2) == "09" )
                                    {
                                        datasf.Add(new object[] {   DAT02_TLYEAR.GetValue().ToString(),
                                                                                   DAT02_TLSEQ.GetValue().ToString(),
                                                                                   DAT02_TLSABUN.GetValue().ToString(),
                                                                                   "S2",
                                                                                   dk.Rows[i]["PSPAYCODE"].ToString().Trim(),
                                                                                   sNextPSYYMM,
                                                                                   dPSPAYAMOUNT.ToString().Trim(),
                                                                                   (dPyAmount - dPSPAYAMOUNT).ToString().Trim(),
                                                                                   dk.Rows[i]["SOAMT"].ToString().Trim(),
                                                                                   "0",
                                                                                   (dPyAmount + Convert.ToDouble(dk.Rows[i]["SOAMT"].ToString().Trim())).ToString(),
                                                                                   TYUserInfo.EmpNo
                                                 });

                                        fdSPayTotal += dPyAmount + Convert.ToDouble(dk.Rows[i]["SOAMT"].ToString().Trim());                                        
                                    }
                                }

                                if (sNextPSYYMM.Substring(4, 2) == "02" || sNextPSYYMM.Substring(4, 2) == "09")
                                {
                                    j += 1;
                                }
                            }

                            sNextPSJIDATE = dtYYMM.AddMonths(1).Year.ToString() + Set_Fill2(dtYYMM.AddMonths(1).Month.ToString()) + Set_Fill2(dtYYMM.AddMonths(1).Day.ToString());
                        }
                    }
                    #endregion
                }

            }

            if (sTOCOMGUBN == "1" || sTOCOMGUBN == "3")  //예상일 경우 휴가비 계산 
            {
                //하기휴가비
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_84NE2854", "H1", sPSYYMM.Substring(0, 4), sSABUN);
                DataTable dh = this.DbConnector.ExecuteDataTable();
                if (dh.Rows.Count > 0)
                {
                    if (iH1COUNT <= 0)  //하기휴가비 지급이력이 없어야 등록한다.
                    {
                        dPyAmount = Convert.ToDouble(dh.Rows[0]["PSPAYAMOUNT"].ToString().Trim());

                        fdSPayTotal += dPyAmount;

                        datasf.Add(new object[] {   DAT02_TLYEAR.GetValue().ToString(),
                                                                                     DAT02_TLSEQ.GetValue().ToString(),
                                                                                     DAT02_TLSABUN.GetValue().ToString(),
                                                                                     dh.Rows[0]["PSGUBN"].ToString().Trim(),
                                                                                     dh.Rows[0]["PSPAYCODE"].ToString().Trim(),
                                                                                     this.DAT02_TLCOMDATE.GetValue().ToString().Substring(0, 4) + "07",
                                                                                     dPyAmount.ToString().Trim(),
                                                                                     "0",
                                                                                     "0",
                                                                                     "0",
                                                                                     dPyAmount.ToString().Trim(),
                                                                                     TYUserInfo.EmpNo
                                                 });
                    }
                }
                else
                {
                    //해당년도에 지급이 안되었으면 입사일자 기준으로 1년 미만자는 25만원 1년이상자 50만원
                    if (fsKBIDATE.Substring(0, 4) == sPSYYMM.Substring(0, 4))
                    {
                        dPyAmount = 250000;
                    }
                    else
                    {
                        dPyAmount = 500000;
                    }
                    fdSPayTotal += dPyAmount;
                  
                    datasf.Add(new object[] {   DAT02_TLYEAR.GetValue().ToString(),
                                                                                     DAT02_TLSEQ.GetValue().ToString(),
                                                                                     DAT02_TLSABUN.GetValue().ToString(),
                                                                                     "H1",
                                                                                     "1001",
                                                                                     sPSYYMM.Substring(0, 4) + "07",
                                                                                     dPyAmount.ToString().Trim(),
                                                                                     "0",
                                                                                     "0",
                                                                                     "0",
                                                                                     dPyAmount.ToString().Trim(),
                                                                                     TYUserInfo.EmpNo
                                                 });    
                }
            }

        }
        #endregion

        #region  Description : 월 평균 급여 계산(DC형)
        private void UP_Set_DCMPayAvg(DataTable dAvgM, string sSABUN, string sYYMM, double dInCAmt, double dInCAmt_Add, string sTOCOMGUBN, string sWkgubn )
        {
            double dPyAmount = 0;
            double dPyTongSangAmt = 0;  //통상임금
            double dPSPAYAMOUNT = 0;  //지급금액     
            double dBonusInCAmt = 0;

            bool bFixOttimeCheck = false;  //utt 고정ot수당이 있는사람
            
            DataTable dt = dAvgM.Clone();
            foreach (DataRow dr in dAvgM.Select("PSYYMM = '" + fsTOPYMDATE3 + "' AND PSSABUN = '" + sSABUN + "'"))
                dt.Rows.Add(dr.ItemArray);

            if (dt.Rows.Count > 0)
            {

                fiAvgMCnt += 1;

                //this.DbConnector.CommandClear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //통상임금
                    dPyTongSangAmt = Convert.ToDouble(dt.Rows[i]["TONGSANGAMT"].ToString().Trim());
                    //지급금액
                    dPSPAYAMOUNT = Convert.ToDouble(dt.Rows[i]["PSPAYAMOUNT"].ToString().Trim());

                    if (dt.Rows[i]["PSPAYCODE"].ToString().Trim() == "1001")
                    {
                        dPyAmount = Convert.ToDouble(dt.Rows[i]["PSPAYAMOUNT"].ToString().Trim()) + dInCAmt + dInCAmt_Add;
                    }
                    else
                    {
                        dPyAmount = Convert.ToDouble(dt.Rows[i]["PSPAYAMOUNT"].ToString().Trim());
                    }

                    //if (sTOCOMGUBN == "1" || sTOCOMGUBN == "3") //예상
                    //{
                    //    if (sWkgubn == "1")
                    //    {
                    //        datasf.Add(new object[] {   DAT02_TLYEAR.GetValue().ToString(),
                    //                                                    DAT02_TLSEQ.GetValue().ToString(),
                    //                                                    DAT02_TLSABUN.GetValue().ToString(),
                    //                                                    dt.Rows[i]["PSGUBN"].ToString().Trim(),
                    //                                                    dt.Rows[i]["PSPAYCODE"].ToString().Trim(),
                    //                                                    dt.Rows[i]["PSYYMM"].ToString().Trim(),
                    //                                                    dPSPAYAMOUNT.ToString().Trim(),
                    //                                                    (dPyAmount - dPSPAYAMOUNT).ToString().Trim(),
                    //                                                    dt.Rows[i]["SOAMT"].ToString().Trim(),
                    //                                                    "0",
                    //                                                    (dPyAmount + Convert.ToDouble(dt.Rows[i]["SOAMT"].ToString().Trim())).ToString(),
                    //                                                    TYUserInfo.EmpNo
                    //                             });

                    //        fdMPayTotal += dPyAmount + Convert.ToDouble(dt.Rows[i]["SOAMT"].ToString().Trim());
                    //    }
                        
                    //}
                    //else //실적
                    //{

                    //    datasf.Add(new object[] {  DAT02_TLYEAR.GetValue().ToString(),
                    //                                                    DAT02_TLSEQ.GetValue().ToString(),
                    //                                                    DAT02_TLSABUN.GetValue().ToString(),
                    //                                                    dt.Rows[i]["PSGUBN"].ToString().Trim(),
                    //                                                    dt.Rows[i]["PSPAYCODE"].ToString().Trim(),
                    //                                                    dt.Rows[i]["PSYYMM"].ToString().Trim(),
                    //                                                    dt.Rows[i]["PSPAYAMOUNT"].ToString().Trim(),
                    //                                                    "0",
                    //                                                    dt.Rows[i]["SOAMT"].ToString().Trim(),
                    //                                                    "0",
                    //                                                    (Convert.ToDouble(dt.Rows[i]["PSPAYAMOUNT"].ToString().Trim()) + Convert.ToDouble(dt.Rows[i]["SOAMT"].ToString().Trim())).ToString(),
                    //                                                    TYUserInfo.EmpNo
                    //                             });

                    //    fdMPayTotal += dPyAmount + Convert.ToDouble(dt.Rows[i]["SOAMT"].ToString().Trim());
                    //}

                    if (sWkgubn == "1")
                    {
                        datasf.Add(new object[] {   DAT02_TLYEAR.GetValue().ToString(),
                                                                        DAT02_TLSEQ.GetValue().ToString(),
                                                                        DAT02_TLSABUN.GetValue().ToString(),
                                                                        dt.Rows[i]["PSGUBN"].ToString().Trim(),
                                                                        dt.Rows[i]["PSPAYCODE"].ToString().Trim(),
                                                                        dt.Rows[i]["PSYYMM"].ToString().Trim(),
                                                                        dPSPAYAMOUNT.ToString().Trim(),
                                                                        (dPyAmount - dPSPAYAMOUNT).ToString().Trim(),
                                                                        dt.Rows[i]["SOAMT"].ToString().Trim(),
                                                                        "0",
                                                                        (dPyAmount + Convert.ToDouble(dt.Rows[i]["SOAMT"].ToString().Trim())).ToString(),
                                                                        TYUserInfo.EmpNo
                                                 });

                        fdMPayTotal += dPyAmount + Convert.ToDouble(dt.Rows[i]["SOAMT"].ToString().Trim());
                    }
                    else
                    {
                        if (dt.Rows[i]["PSPAYCODE"].ToString().Trim() != "1401" &&
                            dt.Rows[i]["PSPAYCODE"].ToString().Trim() != "1402" &&
                            dt.Rows[i]["PSPAYCODE"].ToString().Trim() != "1403" &&
                            dt.Rows[i]["PSPAYCODE"].ToString().Trim() != "1404" &&
                            dt.Rows[i]["PSPAYCODE"].ToString().Trim() != "1405")
                        {
                            datasf.Add(new object[] {   DAT02_TLYEAR.GetValue().ToString(),
                                                                        DAT02_TLSEQ.GetValue().ToString(),
                                                                        DAT02_TLSABUN.GetValue().ToString(),
                                                                        dt.Rows[i]["PSGUBN"].ToString().Trim(),
                                                                        dt.Rows[i]["PSPAYCODE"].ToString().Trim(),
                                                                        sYYMM,
                                                                        dPSPAYAMOUNT.ToString().Trim(),
                                                                        (dPyAmount - dPSPAYAMOUNT).ToString().Trim(),
                                                                        dt.Rows[i]["SOAMT"].ToString().Trim(),
                                                                        "0",
                                                                        (dPyAmount + Convert.ToDouble(dt.Rows[i]["SOAMT"].ToString().Trim())).ToString(),
                                                                        TYUserInfo.EmpNo
                                                 });

                            fdMPayTotal += dPyAmount + Convert.ToDouble(dt.Rows[i]["SOAMT"].ToString().Trim());
                        }
                    }


                    //마지막 레코드
                    if (i == (dt.Rows.Count - 1))
                    {
                        if (sTOCOMGUBN == "1" || sTOCOMGUBN == "3") //예상
                        {
                            if (Convert.ToDouble(fsAvgOttime) > 0 && sWkgubn != "1")
                            {
                                //ot금액 계산 = 통상임금 / 187 * ot시간
                                //상여금의 인상액 반영 계산
                                if (Convert.ToInt16(dt.Rows[i]["PSYYMM"].ToString().Trim().Substring(4, 2)) <= 2)
                                {
                                    dBonusInCAmt = Math.Floor(dInCAmt / 2);
                                }
                                else
                                {
                                    dBonusInCAmt = dInCAmt_Add > 0 ? Math.Floor(dInCAmt / 2) + Math.Floor(dInCAmt_Add / 2) : Math.Floor(dInCAmt / 2);
                                }

                                double dOtAmt = Math.Floor(((dPyTongSangAmt + dInCAmt + dInCAmt_Add + dBonusInCAmt) / 187) * Convert.ToDouble(fsAvgOttime));

                                datasf.Add(new object[] {  DAT02_TLYEAR.GetValue().ToString(),
                                                                                        DAT02_TLSEQ.GetValue().ToString(),
                                                                                        DAT02_TLSABUN.GetValue().ToString(),
                                                                                        dt.Rows[i]["PSGUBN"].ToString().Trim(),
                                                                                        "1402",
                                                                                        sYYMM,
                                                                                        dOtAmt.ToString().Trim(),
                                                                                        "0",
                                                                                        "0",
                                                                                        fsAvgOttime,
                                                                                        dOtAmt.ToString().Trim(),
                                                                                        TYUserInfo.EmpNo
                                                 });
                                fdMPayTotal += dOtAmt;
                            }

                            //고정ot수당이 있는 사람
                            if (bFixOttimeCheck && sWkgubn != "1" )
                            {
                                //ot금액 계산 = 통상임금 / 187 * ot시간
                                //상여금의 인상액 반영 계산
                                if (Convert.ToInt16(dt.Rows[i]["PSYYMM"].ToString().Trim().Substring(4, 2)) <= 2)
                                {
                                    dBonusInCAmt = Math.Floor(dInCAmt / 2);
                                }
                                else
                                {
                                    dBonusInCAmt = dInCAmt_Add > 0 ? Math.Floor(dInCAmt / 2) + Math.Floor(dInCAmt_Add / 2) : Math.Floor(dInCAmt / 2);
                                }

                                double dOtAmt = Math.Floor(((dPyTongSangAmt + dInCAmt + dInCAmt_Add + dBonusInCAmt) / 187) * 13);

                                datasf.Add(new object[] {  DAT02_TLYEAR.GetValue().ToString(),
                                                                                        DAT02_TLSEQ.GetValue().ToString(),
                                                                                        DAT02_TLSABUN.GetValue().ToString(),
                                                                                        dt.Rows[i]["PSGUBN"].ToString().Trim(),
                                                                                        "1408",
                                                                                        sYYMM,
                                                                                        dOtAmt.ToString().Trim(),
                                                                                        "0",
                                                                                        "0",
                                                                                        "13",
                                                                                        dOtAmt.ToString().Trim(),
                                                                                        TYUserInfo.EmpNo
                                                 });
                                fdMPayTotal += dOtAmt;
                            }
                        }
                    }
                   
                }

            }

        }
        #endregion

        #region  Description : 월 평균 상여 계산(DC형)
        private void UP_Set_DCSPayAvg(string sSABUN, string sYYMM, double dInCAmt, double dInCAmt_Add, string sTOCOMGUBN)
        {
            int iSPYCOUNT = 0;  //상여 횟수 카운트
            int iS2COUNT = 0;  //명절상여 횟수 카운트

            int iH1COUNT = 0;  //하기휴가비 카운트

            double dPyAmount = 0;
            double dPSPAYAMOUNT = 0;

            string sPSGUBN = "";
            string sPSYYMM = "";
            string sPSJIDATE = "";

            string sNextPSJIDATE = "";
            string sNextPSYYMM = "";


            if (dInCAmt > 0)
            {
                dInCAmt = Math.Floor(dInCAmt / 2);
            }

            if (dInCAmt_Add > 0)
            {
                dInCAmt_Add = Math.Floor(dInCAmt_Add / 2);
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_C15FE990", fsTOPYSDATE_FROM, fsTOPYSDATE_TO);
            DataTable dm = this.DbConnector.ExecuteDataTable();
            if (dm.Rows.Count > 0)
            {
                if (sTOCOMGUBN == "1" || sTOCOMGUBN == "2")
                {
                    #region  Description : 다음년도 예상이 아닌경우(실적)

                    this.DbConnector.CommandClear();
                    for (int i = 0; i < dm.Rows.Count; i++)
                    {
                        if (dm.Rows[i]["PSGUBN"].ToString().Trim().Substring(0, 1) == "S")
                        {
                            //명절상여 횟수 저장
                            if (dm.Rows[i]["PSGUBN"].ToString().Trim() == "S2")
                            {
                                sPSGUBN = dm.Rows[i]["PSGUBN"].ToString().Trim();
                                sPSYYMM = dm.Rows[i]["PSYYMM"].ToString().Trim();
                                sPSJIDATE = dm.Rows[i]["PSJIDATE"].ToString().Trim();

                                iS2COUNT += 1;
                            }
                        }

                        //하기휴가비 체크
                        if (dm.Rows[i]["PSGUBN"].ToString().Trim().Substring(0, 1) == "H")
                        {
                            iH1COUNT += 1;
                        }

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_HR_84IBM836", dm.Rows[i]["PSGUBN"].ToString().Trim(), dm.Rows[i]["PSYYMM"].ToString().Trim(), dm.Rows[i]["PSJIDATE"].ToString().Trim(), sSABUN, dm.Rows[i]["PSYYMM"].ToString().Trim().Substring(0, 4));
                        DataTable dt = this.DbConnector.ExecuteDataTable();
                        if (dt.Rows.Count > 0)
                        {
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                //지급금액
                                dPSPAYAMOUNT = Convert.ToDouble(dt.Rows[j]["PSPAYAMOUNT"].ToString().Trim());

                                if (dt.Rows[j]["PSPAYCODE"].ToString().Trim() == "1001")
                                {
                                    //2월이전이면 인상액 없음.
                                    if (Convert.ToInt16(dt.Rows[j]["PSYYMM"].ToString().Trim().Substring(4, 2)) <= 2)
                                    {
                                        if (dInCAmt_Add > 0)
                                        {
                                            dPyAmount = Convert.ToDouble(dt.Rows[j]["PSPAYAMOUNT"].ToString().Trim()) + dInCAmt;
                                        }
                                        else
                                        {
                                            dPyAmount = Convert.ToDouble(dt.Rows[j]["PSPAYAMOUNT"].ToString().Trim());
                                        }
                                    }
                                    else
                                    {
                                        if (dt.Rows[j]["PSGUBN"].ToString().Trim().Substring(0, 1) == "H")  //하기휴가비는 인상액 없음
                                        {
                                            dPyAmount = Convert.ToDouble(dt.Rows[j]["PSPAYAMOUNT"].ToString().Trim());
                                        }
                                        else
                                        {
                                            dPyAmount = Convert.ToDouble(dt.Rows[j]["PSPAYAMOUNT"].ToString().Trim()) + dInCAmt + dInCAmt_Add;
                                        }
                                    }
                                }
                                else
                                {
                                    dPyAmount = Convert.ToDouble(dt.Rows[j]["PSPAYAMOUNT"].ToString().Trim());
                                }

                                datasf.Add(new object[] {  DAT02_TLYEAR.GetValue().ToString(),
                                                                               DAT02_TLSEQ.GetValue().ToString(),
                                                                               DAT02_TLSABUN.GetValue().ToString(),
                                                                               dt.Rows[j]["PSGUBN"].ToString().Trim(),
                                                                               dt.Rows[j]["PSPAYCODE"].ToString().Trim(),
                                                                               dt.Rows[j]["PSYYMM"].ToString().Trim(),
                                                                               dPSPAYAMOUNT.ToString().Trim(),
                                                                               (dPyAmount - dPSPAYAMOUNT).ToString(),
                                                                               dt.Rows[j]["SOAMT"].ToString().Trim(),
                                                                               "0",
                                                                               (dPyAmount + Convert.ToDouble(dt.Rows[j]["SOAMT"].ToString().Trim())).ToString(),
                                                                               TYUserInfo.EmpNo
                                                 });

                                fdSPayTotal += dPyAmount + Convert.ToDouble(dt.Rows[j]["SOAMT"].ToString().Trim());
                            }
                        }
                    }

                    // 명절상여 2번 
                    if (iS2COUNT < 2)
                    {
                        //14번보다 부족하면 마직막 일반상여 정보를 기준으로 나머지 횟수를 계산한다.
                        DateTime dtYYMM = Convert.ToDateTime(Set_Date(sPSJIDATE));

                        for (int j = iS2COUNT; j < 2; j++)
                        {
                            sNextPSJIDATE = sPSJIDATE.Substring(0, 4) + "0930";
                            sNextPSYYMM = sNextPSJIDATE.Substring(0, 6);

                            dtYYMM = Convert.ToDateTime(Set_Date(sNextPSJIDATE));

                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_HR_84IBM836", sPSGUBN, sPSYYMM, sPSJIDATE, sSABUN, sPSYYMM.Substring(0, 4));
                            DataTable dk = this.DbConnector.ExecuteDataTable();
                            if (dk.Rows.Count > 0)
                            {
                                for (int i = 0; i < dk.Rows.Count; i++)
                                {
                                    //지급금액
                                    dPSPAYAMOUNT = Convert.ToDouble(dk.Rows[i]["PSPAYAMOUNT"].ToString().Trim());

                                    if (dk.Rows[i]["PSPAYCODE"].ToString().Trim() == "1001")
                                    {
                                        dPyAmount = Convert.ToDouble(dk.Rows[i]["PSPAYAMOUNT"].ToString().Trim()) + dInCAmt + dInCAmt_Add;
                                    }
                                    else
                                    {
                                        dPyAmount = Convert.ToDouble(dk.Rows[i]["PSPAYAMOUNT"].ToString().Trim());
                                    }                                  

                                    //명절상여 2번이인데 1번이면 매년 9월을 추석으로 임의 지정하여 예상 작업시 등록해준다
                                    if (iS2COUNT <= 1 )
                                    {
                                        datasf.Add(new object[] {   DAT02_TLYEAR.GetValue().ToString(),
                                                                                   DAT02_TLSEQ.GetValue().ToString(),
                                                                                   DAT02_TLSABUN.GetValue().ToString(),
                                                                                   "S2",
                                                                                   dk.Rows[i]["PSPAYCODE"].ToString().Trim(),
                                                                                   sNextPSYYMM,
                                                                                   dPSPAYAMOUNT.ToString().Trim(),
                                                                                   (dPyAmount - dPSPAYAMOUNT).ToString().Trim(),
                                                                                   dk.Rows[i]["SOAMT"].ToString().Trim(),
                                                                                   "0",
                                                                                   (dPyAmount + Convert.ToDouble(dk.Rows[i]["SOAMT"].ToString().Trim())).ToString(),
                                                                                   TYUserInfo.EmpNo
                                                 });

                                        fdSPayTotal += dPyAmount + Convert.ToDouble(dk.Rows[i]["SOAMT"].ToString().Trim());
                                    }
                                }

                                if (iS2COUNT <= 1 )
                                {
                                    j += 1;
                                }
                            }
                        }
                    }

                    #endregion
                }
                else
                {
                    //다음년도 예상시
                    #region  Description : 다음년도 예상시
                    //정기상여 12번 명절상여 2번 
                    if (iSPYCOUNT < 14)
                    {
                        for (int i = 0; i < dm.Rows.Count; i++)
                        {
                            if (dm.Rows[i]["PSGUBN"].ToString().Trim().Substring(0, 1) == "S")
                            {
                                //마지막 구분 내용 저장
                                if (dm.Rows[i]["PSGUBN"].ToString().Trim() == "S2" )
                                {
                                    sPSGUBN = dm.Rows[i]["PSGUBN"].ToString().Trim();
                                    sPSYYMM = dm.Rows[i]["PSYYMM"].ToString().Trim();
                                    sPSJIDATE = dm.Rows[i]["PSJIDATE"].ToString().Trim();
                                }
                            }
                        }

                        //14번보다 부족하면 마직막 일반상여 정보를 기준으로 나머지 횟수를 계산한다.

                        DateTime dtYYMM = Convert.ToDateTime(Set_Date(this.DAT02_TLCOMDATE.GetValue().ToString().Substring(0, 4) + "0128"));
                        sNextPSJIDATE = dtYYMM.Year.ToString() + Set_Fill2(dtYYMM.Month.ToString()) + Set_Fill2(dtYYMM.Day.ToString());

                        for (int j = 0; j < 2; j++)
                        {

                            sNextPSYYMM = sNextPSJIDATE.Substring(0, 6);

                            dtYYMM = Convert.ToDateTime(Set_Date(sNextPSJIDATE));

                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_HR_84IBM836", sPSGUBN, sPSYYMM, sPSJIDATE, sSABUN, sPSYYMM.Substring(0, 4));
                            DataTable dk = this.DbConnector.ExecuteDataTable();
                            if (dk.Rows.Count > 0)
                            {
                                for (int i = 0; i < dk.Rows.Count; i++)
                                {
                                    //지급금액
                                    dPSPAYAMOUNT = Convert.ToDouble(dk.Rows[i]["PSPAYAMOUNT"].ToString().Trim());

                                    if (dk.Rows[i]["PSPAYCODE"].ToString().Trim() == "1001")
                                    {
                                        if (Convert.ToInt16(sNextPSYYMM.Substring(4, 2).ToString()) <= 2)
                                        {
                                            dPyAmount = Convert.ToDouble(dk.Rows[i]["PSPAYAMOUNT"].ToString().Trim()) + dInCAmt;
                                        }
                                        else
                                        {
                                            dPyAmount = Convert.ToDouble(dk.Rows[i]["PSPAYAMOUNT"].ToString().Trim()) + dInCAmt + dInCAmt_Add;
                                        }
                                    }
                                    else
                                    {
                                        dPyAmount = Convert.ToDouble(dk.Rows[i]["PSPAYAMOUNT"].ToString().Trim());
                                    }

                                    datasf.Add(new object[] {   DAT02_TLYEAR.GetValue().ToString(),
                                                                                   DAT02_TLSEQ.GetValue().ToString(),
                                                                                   DAT02_TLSABUN.GetValue().ToString(),
                                                                                   dk.Rows[i]["PSGUBN"].ToString().Trim(),
                                                                                   dk.Rows[i]["PSPAYCODE"].ToString().Trim(),
                                                                                   sNextPSYYMM,
                                                                                   dPSPAYAMOUNT.ToString().Trim(),
                                                                                   (dPyAmount - dPSPAYAMOUNT).ToString().Trim(),
                                                                                   dk.Rows[i]["SOAMT"].ToString().Trim(),
                                                                                   "0",
                                                                                   (dPyAmount + Convert.ToDouble(dk.Rows[i]["SOAMT"].ToString().Trim())).ToString(),
                                                                                   TYUserInfo.EmpNo
                                                 });

                                    fdSPayTotal += dPyAmount + Convert.ToDouble(dk.Rows[i]["SOAMT"].ToString().Trim());

                                    ////명절상여 2번이인데 매년 2, 9월을 추석으로 임의 지정하여 예상 작업시 등록해준다
                                    //if (sNextPSYYMM.Substring(4, 2) == "02" || sNextPSYYMM.Substring(4, 2) == "09")
                                    //{
                                    //    datasf.Add(new object[] {   DAT02_TLYEAR.GetValue().ToString(),
                                    //                                               DAT02_TLSEQ.GetValue().ToString(),
                                    //                                               DAT02_TLSABUN.GetValue().ToString(),
                                    //                                               "S2",
                                    //                                               dk.Rows[i]["PSPAYCODE"].ToString().Trim(),
                                    //                                               sNextPSYYMM,
                                    //                                               dPSPAYAMOUNT.ToString().Trim(),
                                    //                                               (dPyAmount - dPSPAYAMOUNT).ToString().Trim(),
                                    //                                               dk.Rows[i]["SOAMT"].ToString().Trim(),
                                    //                                               "0",
                                    //                                               (dPyAmount + Convert.ToDouble(dk.Rows[i]["SOAMT"].ToString().Trim())).ToString(),
                                    //                                               TYUserInfo.EmpNo
                                    //             });

                                    //    fdSPayTotal += dPyAmount + Convert.ToDouble(dk.Rows[i]["SOAMT"].ToString().Trim());
                                    //}
                                }

                                //if (sNextPSYYMM.Substring(4, 2) == "02" || sNextPSYYMM.Substring(4, 2) == "09")
                                //{
                                //    j += 1;
                                //}
                            }

                            sNextPSJIDATE = dtYYMM.AddMonths(1).Year.ToString() + Set_Fill2(dtYYMM.AddMonths(1).Month.ToString()) + Set_Fill2(dtYYMM.AddMonths(1).Day.ToString());
                        }
                    }
                    #endregion
                }

            }

            if (sTOCOMGUBN == "1" || sTOCOMGUBN == "3")  //예상일 경우 휴가비 계산 
            {
                //하기휴가비
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_84NE2854", "H1", sPSYYMM.Substring(0, 4), sSABUN);
                DataTable dh = this.DbConnector.ExecuteDataTable();
                if (dh.Rows.Count > 0)
                {
                    if (iH1COUNT <= 0)  //하기휴가비 지급이력이 없어야 등록한다.
                    {
                        dPyAmount = Convert.ToDouble(dh.Rows[0]["PSPAYAMOUNT"].ToString().Trim());

                        fdSPayTotal += dPyAmount;

                        datasf.Add(new object[] {   DAT02_TLYEAR.GetValue().ToString(),
                                                                                     DAT02_TLSEQ.GetValue().ToString(),
                                                                                     DAT02_TLSABUN.GetValue().ToString(),
                                                                                     dh.Rows[0]["PSGUBN"].ToString().Trim(),
                                                                                     dh.Rows[0]["PSPAYCODE"].ToString().Trim(),
                                                                                     this.DAT02_TLCOMDATE.GetValue().ToString().Substring(0, 4) + "07",
                                                                                     dPyAmount.ToString().Trim(),
                                                                                     "0",
                                                                                     "0",
                                                                                     "0",
                                                                                     dPyAmount.ToString().Trim(),
                                                                                     TYUserInfo.EmpNo
                                                 });
                    }
                }
                else
                {
                    //해당년도에 지급이 안되었으면 입사일자 기준으로 1년 미만자는 25만원 1년이상자 50만원
                    if (fsKBIDATE.Substring(0, 4) == sPSYYMM.Substring(0, 4))
                    {
                        dPyAmount = 250000;
                    }
                    else
                    {
                        dPyAmount = 500000;
                    }
                    fdSPayTotal += dPyAmount;

                    datasf.Add(new object[] {   DAT02_TLYEAR.GetValue().ToString(),
                                                                                     DAT02_TLSEQ.GetValue().ToString(),
                                                                                     DAT02_TLSABUN.GetValue().ToString(),
                                                                                     "H1",
                                                                                     "1001",
                                                                                     sPSYYMM.Substring(0, 4) + "07",
                                                                                     dPyAmount.ToString().Trim(),
                                                                                     "0",
                                                                                     "0",
                                                                                     "0",
                                                                                     dPyAmount.ToString().Trim(),
                                                                                     TYUserInfo.EmpNo
                                                 });
                }
            }

        }
        #endregion

        #region  Description : 년차 계산
        private void UP_Set_YPayCompute(string sSABUN, string sYYMM)
        {

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_84IG4837", sYYMM, sSABUN);
            DataTable dm = this.DbConnector.ExecuteDataTable();
            if (dm.Rows.Count > 0)
            {
                datasf.Add(new object[] {    DAT02_TLYEAR.GetValue().ToString(),
                                                                                   DAT02_TLSEQ.GetValue().ToString(),
                                                                                   DAT02_TLSABUN.GetValue().ToString(),
                                                                                   dm.Rows[0]["PSGUBN"].ToString().Trim(),
                                                                                   dm.Rows[0]["PSPAYCODE"].ToString().Trim(),
                                                                                   dm.Rows[0]["PSYYMM"].ToString().Trim(),
                                                                                   dm.Rows[0]["PSPAYAMOUNT"].ToString().Trim(),
                                                                                   "0",
                                                                                   "0",
                                                                                   "0",
                                                                                   dm.Rows[0]["PSPAYAMOUNT"].ToString().Trim(),
                                                                                   TYUserInfo.EmpNo
                                                 });    

                fdYPayTotal += Convert.ToDouble(dm.Rows[0]["PSPAYAMOUNT"].ToString().Trim());             
            }
        }
        #endregion

        #region  Description : 평균 OT 시간 구하기
        private string UP_Get_AvgOTTime(string sSABUN)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_84IAU834", fsTOOTDATES, fsTOOTDATEE, sSABUN);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }

            return "0";
        }
        #endregion

        #region Description : 두날짜사이의 차이구하기(년월일)
        private string UP_Get_WorkYears(DateTime t1, DateTime t2)
        {

            int 일 = t2.Day - t1.Day;
            int 월 = t2.Month - t1.Month;
            int 년 = t2.Year - t1.Year;
            if (일 < 0)
            {
                일 += DateTime.DaysInMonth(t2.Year, t2.Month - 1);
                월--;
            }
            if (월 < 0)
            {
                월 += 12; 년--;
            }
            일++;

            return Set_Fill4(년.ToString()) + "-" + Set_Fill2(월.ToString()) + "-" + Set_Fill2(일.ToString());

        }
        #endregion

        #region  Description : 변수 클리어
        private void UP_Set_Clear()
        {
            this.DAT02_TLYEAR.SetValue("");
            this.DAT02_TLSEQ.SetValue("");
            this.DAT02_TLSABUN.SetValue("");
            this.DAT02_TLNAME.SetValue("");
            this.DAT02_TLCOMDATE.SetValue("");
            this.DAT02_TLKBDATE.SetValue("");
            this.DAT02_TLJKCD.SetValue("");
            this.DAT02_TLBUSEO.SetValue("");
            this.DAT02_TLWKYEAR.SetValue("");
            this.DAT02_TLWKMONTH.SetValue("");
            this.DAT02_TLWKDAY.SetValue("");
            this.DAT02_TLAVG_M.SetValue("");
            this.DAT02_TLAVG_S.SetValue("");
            this.DAT02_TLAVGTOTAL.SetValue("");
            this.DAT02_TLAVG_OTTIME.SetValue("");
            this.DAT02_TLCOMTOTAL.SetValue("");


            fdMPayTotal = 0;
            fdSPayTotal = 0;
            fdYPayTotal = 0;

            fsAvgOttime = "0";
        }
        #endregion

        #region  Description : 타이머 이벤트
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            UP_BATCH_Create();            
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

       
    }
}
