using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// EIS 사업부별 인건비 생성 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.11.21 20:33
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_2BL7I540 : EIS 사업부별 인건비(직원) 조회
    ///  TY_P_HR_2BL7K541 : EIS 사업부별 인건비(임원) 조회
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_26E2Z874 : 생성하시겠습니까?
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  ELSYYMM : 년월
    /// </summary>
    public partial class TYHRES007B : TYBase
    {
        private TYData DAT02_ELSYYMM;
        private TYData DAT02_ELSSAUP;
        private TYData DAT02_ELSGUBN;
        private TYData DAT02_ELSPAYTOTAL;
        private TYData DAT02_ELSBONUS;
        private TYData DAT02_ELSPLANBONUS;
        private TYData DAT02_ELSRETIREFUND;
        private TYData DAT02_ELSINSURANCE;
        private TYData DAT02_ELSPEOPLE;
        private TYData DAT02_ELSWKGUBN;
        private TYData DAT02_ELSHISAB;

        #region Description : 폼 로드 이벤트
        public TYHRES007B()
        {
            InitializeComponent();

            this.DAT02_ELSYYMM = new TYData("DAT02_ELSYYMM", null);
            this.DAT02_ELSSAUP = new TYData("DAT02_ELSSAUP", null);
            this.DAT02_ELSGUBN = new TYData("DAT02_ELSGUBN", null);
            this.DAT02_ELSPAYTOTAL = new TYData("DAT02_ELSPAYTOTAL", null);
            this.DAT02_ELSBONUS = new TYData("DAT02_ELSBONUS", null);
            this.DAT02_ELSPLANBONUS = new TYData("DAT02_ELSPLANBONUS", null);
            this.DAT02_ELSRETIREFUND = new TYData("DAT02_ELSRETIREFUND", null);
            this.DAT02_ELSINSURANCE = new TYData("DAT02_ELSINSURANCE", null);
            this.DAT02_ELSPEOPLE = new TYData("DAT02_ELSPEOPLE", null);
            this.DAT02_ELSWKGUBN = new TYData("DAT02_ELSWKGUBN", null);
            this.DAT02_ELSHISAB = new TYData("DAT02_ELSHISAB", null);  

        }

        private void TYHRES007B_Load(object sender, System.EventArgs e)
        {
            this.ControlFactory.Add(DAT02_ELSYYMM);
            this.ControlFactory.Add(DAT02_ELSSAUP);
            this.ControlFactory.Add(DAT02_ELSGUBN);
            this.ControlFactory.Add(DAT02_ELSPAYTOTAL);
            this.ControlFactory.Add(DAT02_ELSBONUS);
            this.ControlFactory.Add(DAT02_ELSPLANBONUS);
            this.ControlFactory.Add(DAT02_ELSRETIREFUND);
            this.ControlFactory.Add(DAT02_ELSINSURANCE);
            this.ControlFactory.Add(DAT02_ELSPEOPLE);
            this.ControlFactory.Add(DAT02_ELSWKGUBN);
            this.ControlFactory.Add(DAT02_ELSHISAB);       

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.MTB01_ELSYYMM.SetValue(DateTime.Now.ToString("yyyy"));
            this.SetStartingFocus(this.MTB01_ELSYYMM);

        }
        #endregion

        #region Description : 생성 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_GB_26E2Z874"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : 생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {

            //신인사 적용후 사용함
            UP_PAY_Create();

            /*
            double dPAYTOTAL = 0;
            double dBONUS = 0;
            Int16  iPeople = 0;

            string sSDATE = "";
            string sEDATE = "";            
            string sYYMM = "";
            string sMaxValue = "";

            string sFilter = "";

            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

            sSDATE = MTB01_ELSYYMM.GetValue().ToString().Substring(0, 4) + "01";
            sEDATE = MTB01_ELSYYMM.GetValue().ToString().Substring(0, 4) + "12";

            sYYMM = MTB01_ELSYYMM.GetValue().ToString().Substring(0, 4);

            //삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_2BL8U549", MTB01_ELSYYMM.GetValue().ToString().Substring(0, 4));
            this.DbConnector.ExecuteTranQuery();

            //임원
            for (int j = 0; j < 2; j++)
            {
                this.DbConnector.CommandClear();
                //인사 급여 
                if (j == 0)
                {
                    this.DbConnector.Attach("TY_P_HR_2BL7K541", sSDATE, sEDATE, sSDATE, sEDATE);  //임원
                }
                else
                {
                    this.DbConnector.Attach("TY_P_HR_2BL7I540", sSDATE, sEDATE, sSDATE, sEDATE);  //직원
                }
                DataTable dt = this.DbConnector.ExecuteDataTable();

                //성과급 계정 가져오기
                this.DbConnector.CommandClear();
                if (j == 0)
                {
                    this.DbConnector.Attach("TY_P_HR_2BM5P586", sSDATE.Substring(0, 4));
                }
                else
                {
                    this.DbConnector.Attach("TY_P_HR_2BM5P585", sSDATE.Substring(0, 4));
                }
                DataTable dtbs = this.DbConnector.ExecuteDataTable();

                //퇴직급여충당금 가져오기
                this.DbConnector.CommandClear();
                if (j == 0)
                {
                    this.DbConnector.Attach("TY_P_HR_2BM6M590", sSDATE.Substring(0, 4), "Y");
                }
                else
                {
                    this.DbConnector.Attach("TY_P_HR_2BM6M590", sSDATE.Substring(0, 4), "N");
                }
                DataTable dtfn = this.DbConnector.ExecuteDataTable();

                //산재보험 가져오기
                this.DbConnector.CommandClear();
                if (j == 0)
                {
                    //임원 산재보험료를 없는것으로 한다.
                    this.DbConnector.Attach("TY_P_HR_2BM92602", "9999");
                }
                else
                {
                    this.DbConnector.Attach("TY_P_HR_2BM92602", sSDATE.Substring(0, 4));
                }
                DataTable dtsj = this.DbConnector.ExecuteDataTable();

                //년차 가져오기(임원은 년차수당이 없다)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_2BN9V610", sSDATE.Substring(0, 4));
                DataTable dtyc = this.DbConnector.ExecuteDataTable();
                
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.DAT02_ELSYYMM.SetValue(dt.Rows[i]["PYDATE"].ToString());
                        this.DAT02_ELSSAUP.SetValue(dt.Rows[i]["PYDEPT"].ToString());
                        if (j == 0) { this.DAT02_ELSGUBN.SetValue("Y"); }
                        else { this.DAT02_ELSGUBN.SetValue("N");  }                       
                        
                        dPAYTOTAL = 0;
                        //12월에 년차수당체크해서 지급됐으면 실적으로 아니면 예산금액으로 한다.
                        if (j != 0)
                        {
                            if (dt.Rows[i]["PYDATE"].ToString().Substring(4, 2) == "12" && Convert.ToDouble(dt.Rows[i]["YUNCHA"].ToString()) > 0)
                            {
                                dPAYTOTAL = 0;
                            }
                            else
                            {
                                sFilter = "";
                                sFilter = "MMYEAR = '" + dt.Rows[i]["PYDATE"].ToString().Substring(0, 4) + "'";
                                sFilter = sFilter + " AND MMMONTH =  '" + dt.Rows[i]["PYDATE"].ToString().Substring(4, 2) + "'";
                                sFilter = sFilter + " AND MMCDDP  = '" + dt.Rows[i]["PYDATE"].ToString() + "'";

                                dPAYTOTAL = Convert.ToDouble(Get_Numeric(dtyc.Compute("SUM(MMCRAMT)", sFilter).ToString()));                                
                            }
                        }

                        dPAYTOTAL += Convert.ToDouble(dt.Rows[i]["PAYTOTAL"].ToString()) - Convert.ToDouble(dt.Rows[i]["BONUS"].ToString());
                        this.DAT02_ELSPAYTOTAL.SetValue(dPAYTOTAL.ToString());          //급여총액

                        dBONUS = 0;
                        //12월이면 성과급 지급을 체크하여 지급안되면 예산 등록에서 가져온다
                        if (sEDATE == dt.Rows[i]["PYDATE"].ToString().Trim() )
                        {
                            if (Convert.ToDouble(dt.Rows[i]["BONUS"].ToString()) <= 0)
                            {
                                sFilter = "ELXYYMM >= '" + sSDATE + "' AND  ELXYYMM <= '" + sEDATE + "'";
                                sFilter = sFilter + " AND ELXSAUP = '" + this.DAT02_ELSSAUP.GetValue().ToString() + "'";

                                dBONUS = Convert.ToDouble(Get_Numeric(dtbs.Compute("SUM(ELXCRAMT)", sFilter).ToString()));
                            }
                            else
                            {
                                dBONUS = Convert.ToDouble(dt.Rows[i]["BONUS"].ToString());
                            }
                        }
                        this.DAT02_ELSBONUS.SetValue(dBONUS.ToString());   //성과급
                        this.DAT02_ELSPLANBONUS.SetValue("0");
                        
                        //퇴직급여충당금은 예산 등록 파일에서 무조건 가져온다
                        dBONUS = 0;
                        //sFilter = "ELXYYMM = '" + sSDATE + "'";
                        sFilter = "ELXYYMM = '" + this.DAT02_ELSYYMM.GetValue().ToString() + "'";
                        sFilter = sFilter + " AND ELXSAUP = '" + this.DAT02_ELSSAUP.GetValue().ToString() + "'";
                        dBONUS = Convert.ToDouble(Get_Numeric(dtfn.Compute("SUM(ELXCRAMT)", sFilter).ToString()));
                        this.DAT02_ELSRETIREFUND.SetValue(dBONUS.ToString());  //퇴충금

                        //산재보험료
                        dBONUS = 0;
                        //sFilter = "ELXYYMM = '" + sSDATE + "'";
                        sFilter = "ELXYYMM = '" + this.DAT02_ELSYYMM.GetValue().ToString() + "'";
                        sFilter = sFilter + " AND ELXSAUP = '" + this.DAT02_ELSSAUP.GetValue().ToString() + "'";
                        dBONUS = Convert.ToDouble(Get_Numeric(dtsj.Compute("SUM(ELXCRAMT)", sFilter).ToString()));

                        dBONUS += Convert.ToDouble(dt.Rows[i]["FUNDAMT"].ToString());

                        this.DAT02_ELSINSURANCE.SetValue(dBONUS.ToString());  //4대보험(국민연금+의료보험+고용보험)

                        this.DAT02_ELSPEOPLE.SetValue(dt.Rows[i]["PEOPLECNT"].ToString()); //년평균인원
                        this.DAT02_ELSWKGUBN.SetValue("Y");  //Y 실적 N 예상
                        this.DAT02_ELSHISAB.SetValue(TYUserInfo.EmpNo);

                        datas.Add(new object[] {  this.DAT02_ELSYYMM.GetValue(), 
                                                  this.DAT02_ELSSAUP.GetValue(),
                                                  this.DAT02_ELSGUBN.GetValue(),
                                                  this.DAT02_ELSPAYTOTAL.GetValue(),
                                                  this.DAT02_ELSBONUS.GetValue(), 
                                                  this.DAT02_ELSPLANBONUS.GetValue(),
                                                  this.DAT02_ELSRETIREFUND.GetValue(),
                                                  this.DAT02_ELSINSURANCE.GetValue(),
                                                  this.DAT02_ELSPEOPLE.GetValue(),
                                                  this.DAT02_ELSWKGUBN.GetValue(),
                                                  this.DAT02_ELSHISAB.GetValue()
                                                });
                    } //for (int i = 0; i < dtjk.Rows.Count; i++)..end                   

                    sMaxValue = dt.Compute("MAX(PYDATE)", "").ToString();

                    if (sMaxValue != sEDATE)
                    {
                        //급여 계획 금액 가져오기
                        this.DbConnector.CommandClear();
                        if (j == 0)
                        {
                            this.DbConnector.Attach("TY_P_HR_2BMAF560", sMaxValue.Substring(0, 4), sMaxValue.Substring(4, 2)); //임원
                        }
                        else
                        {
                            this.DbConnector.Attach("TY_P_HR_2BMAE559", sMaxValue.Substring(0, 4), sMaxValue.Substring(4, 2)); //직원
                        }
                        DataTable dtpl = this.DbConnector.ExecuteDataTable();

                        for (int k = 0; k < dtpl.Rows.Count; k++)
                        {
                            this.DAT02_ELSYYMM.SetValue(dtpl.Rows[k]["MMYEAR"].ToString() + dtpl.Rows[k]["MMMONTH"].ToString());
                            this.DAT02_ELSSAUP.SetValue(dtpl.Rows[k]["MMCDDP"].ToString());
                            if (j == 0)
                            {
                                this.DAT02_ELSGUBN.SetValue("Y");
                            }
                            else
                            {
                                this.DAT02_ELSGUBN.SetValue("N");
                            }
                            dBONUS = 0;
                            dBONUS = Convert.ToDouble(dtpl.Rows[k]["MMCRAMT"].ToString()) + Convert.ToDouble(dtpl.Rows[k]["PLANYUNCHA"].ToString());
                            this.DAT02_ELSPAYTOTAL.SetValue(dBONUS.ToString());  //급여총액
                            this.DAT02_ELSBONUS.SetValue(dtpl.Rows[k]["PLANBONUS"].ToString());   //성과급
                            this.DAT02_ELSPLANBONUS.SetValue("0");
                            this.DAT02_ELSRETIREFUND.SetValue(dtpl.Rows[k]["PLANRETIREFUND"].ToString());  //퇴충금
                            this.DAT02_ELSINSURANCE.SetValue(dtpl.Rows[k]["PLANINSURANCE"].ToString());  //4대보험

                            sFilter = "PYDEPT = '" + this.DAT02_ELSSAUP.GetValue().ToString() + "'";
                            iPeople = Convert.ToInt16(Get_Numeric(dt.Compute("AVG(PEOPLECNT)", sFilter).ToString()));

                            this.DAT02_ELSPEOPLE.SetValue(iPeople.ToString()); //년평균인원

                            this.DAT02_ELSWKGUBN.SetValue("N");  //Y 실적 N 예상
                            this.DAT02_ELSHISAB.SetValue(TYUserInfo.EmpNo);

                            datas.Add(new object[] {  this.DAT02_ELSYYMM.GetValue(), 
                                                  this.DAT02_ELSSAUP.GetValue(),
                                                  this.DAT02_ELSGUBN.GetValue(),
                                                  this.DAT02_ELSPAYTOTAL.GetValue(),
                                                  this.DAT02_ELSBONUS.GetValue(), 
                                                  this.DAT02_ELSPLANBONUS.GetValue(),
                                                  this.DAT02_ELSRETIREFUND.GetValue(),
                                                  this.DAT02_ELSINSURANCE.GetValue(),
                                                  this.DAT02_ELSPEOPLE.GetValue(),
                                                  this.DAT02_ELSWKGUBN.GetValue(),
                                                  this.DAT02_ELSHISAB.GetValue()
                                                });
                        } //for (int k = 0; k < dtpl.Rows.Count; k++)...end
                    } //if (sMaxValue != sEDATE)...end                 

                } //if (dt.Rows.Count > 0)...end
            }

            if (datas.Count > 0)
            {
                this.DbConnector.CommandClear();
                foreach (object[] data in datas)
                {
                    this.DbConnector.Attach("TY_P_HR_2BM7G595", data);
                }

                this.DbConnector.ExecuteTranQueryList();
            }            

            this.ShowMessage("TY_M_GB_26E30875");
             * */


        }
        #endregion

        #region  Description : 신 인사용 사업부별 급여계산 함수
        private void UP_PAY_Create()
        {
            double dPAYTOTAL = 0;
            double dBONUS = 0;
            Int16 iPeople = 0;

            string sSDATE = "";
            string sEDATE = "";
            string sYYMM = "";
            string sMaxValue = "";

            string sFilter = "";

            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

            sSDATE = MTB01_ELSYYMM.GetValue().ToString().Substring(0, 4) + "01";
            sEDATE = MTB01_ELSYYMM.GetValue().ToString().Substring(0, 4) + "12";

            sYYMM = MTB01_ELSYYMM.GetValue().ToString().Substring(0, 4);

            //삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_2BL8U549", MTB01_ELSYYMM.GetValue().ToString().Substring(0, 4));
            this.DbConnector.ExecuteTranQuery();

            //임원
            for (int j = 0; j < 2; j++)
            {
                this.DbConnector.CommandClear();
                //인사 급여 
                if (j == 0)
                {
                    this.DbConnector.Attach("TY_P_HR_5C4A3266", sSDATE, sEDATE, sSDATE, sEDATE);  //임원
                }
                else
                {
                    this.DbConnector.Attach("TY_P_HR_5C4A5267", sSDATE, sEDATE, sSDATE, sEDATE);  //직원
                }
                DataTable dt = this.DbConnector.ExecuteDataTable();

                //성과급 계정 가져오기
                this.DbConnector.CommandClear();
                if (j == 0)
                {
                    this.DbConnector.Attach("TY_P_HR_2BM5P586", sSDATE.Substring(0, 4));
                }
                else
                {
                    this.DbConnector.Attach("TY_P_HR_2BM5P585", sSDATE.Substring(0, 4));
                }
                DataTable dtbs = this.DbConnector.ExecuteDataTable();

                //퇴직급여충당금 가져오기
                this.DbConnector.CommandClear();
                if (j == 0)
                {
                    this.DbConnector.Attach("TY_P_HR_2BM6M590", sSDATE.Substring(0, 4), "Y");
                }
                else
                {
                    this.DbConnector.Attach("TY_P_HR_2BM6M590", sSDATE.Substring(0, 4), "N");
                }
                DataTable dtfn = this.DbConnector.ExecuteDataTable();

                //산재보험 가져오기
                this.DbConnector.CommandClear();
                if (j == 0)
                {
                    //임원 산재보험료를 없는것으로 한다.
                    this.DbConnector.Attach("TY_P_HR_2BM92602", "9999");
                }
                else
                {
                    this.DbConnector.Attach("TY_P_HR_2BM92602", sSDATE.Substring(0, 4));
                }
                DataTable dtsj = this.DbConnector.ExecuteDataTable();

                //년차 가져오기(임원은 년차수당이 없다)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_2BN9V610", sSDATE.Substring(0, 4));
                DataTable dtyc = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.DAT02_ELSYYMM.SetValue(dt.Rows[i]["PYDATE"].ToString());
                        this.DAT02_ELSSAUP.SetValue(dt.Rows[i]["PYDEPT"].ToString());
                        if (j == 0) { this.DAT02_ELSGUBN.SetValue("Y"); }
                        else { this.DAT02_ELSGUBN.SetValue("N"); }

                        dPAYTOTAL = 0;
                        //12월에 년차수당체크해서 지급됐으면 실적으로 아니면 예산금액으로 한다.
                        if (j != 0)
                        {
                            if (dt.Rows[i]["PYDATE"].ToString().Substring(4, 2) == "12" && Convert.ToDouble(dt.Rows[i]["YUNCHA"].ToString()) > 0)
                            {
                                dPAYTOTAL = 0;
                            }
                            else
                            {
                                sFilter = "";
                                sFilter = "MMYEAR = '" + dt.Rows[i]["PYDATE"].ToString().Substring(0, 4) + "'";
                                sFilter = sFilter + " AND MMMONTH =  '" + dt.Rows[i]["PYDATE"].ToString().Substring(4, 2) + "'";
                                sFilter = sFilter + " AND MMCDDP  = '" + dt.Rows[i]["PYDATE"].ToString() + "'";

                                dPAYTOTAL = Convert.ToDouble(Get_Numeric(dtyc.Compute("SUM(MMCRAMT)", sFilter).ToString()));
                            }
                        }

                        dPAYTOTAL += Convert.ToDouble(dt.Rows[i]["PAYTOTAL"].ToString()) - Convert.ToDouble(dt.Rows[i]["BONUS"].ToString());
                        this.DAT02_ELSPAYTOTAL.SetValue(dPAYTOTAL.ToString());          //급여총액

                        dBONUS = 0;
                        //12월이면 성과급 지급을 체크하여 지급안되면 예산 등록에서 가져온다
                        if (sEDATE == dt.Rows[i]["PYDATE"].ToString().Trim())
                        {
                            if (Convert.ToDouble(dt.Rows[i]["BONUS"].ToString()) <= 0)
                            {
                                sFilter = "ELXYYMM >= '" + sSDATE + "' AND  ELXYYMM <= '" + sEDATE + "'";
                                sFilter = sFilter + " AND ELXSAUP = '" + this.DAT02_ELSSAUP.GetValue().ToString() + "'";

                                dBONUS = Convert.ToDouble(Get_Numeric(dtbs.Compute("SUM(ELXCRAMT)", sFilter).ToString()));
                            }
                            else
                            {
                                dBONUS = Convert.ToDouble(dt.Rows[i]["BONUS"].ToString());
                            }
                        }
                        this.DAT02_ELSBONUS.SetValue(dBONUS.ToString());   //성과급
                        this.DAT02_ELSPLANBONUS.SetValue("0");

                        //퇴직급여충당금은 예산 등록 파일에서 무조건 가져온다
                        dBONUS = 0;
                        //sFilter = "ELXYYMM = '" + sSDATE + "'";
                        sFilter = "ELXYYMM = '" + this.DAT02_ELSYYMM.GetValue().ToString() + "'";
                        sFilter = sFilter + " AND ELXSAUP = '" + this.DAT02_ELSSAUP.GetValue().ToString() + "'";
                        dBONUS = Convert.ToDouble(Get_Numeric(dtfn.Compute("SUM(ELXCRAMT)", sFilter).ToString()));
                        this.DAT02_ELSRETIREFUND.SetValue(dBONUS.ToString());  //퇴충금

                        //산재보험료
                        dBONUS = 0;
                        //sFilter = "ELXYYMM = '" + sSDATE + "'";
                        sFilter = "ELXYYMM = '" + this.DAT02_ELSYYMM.GetValue().ToString() + "'";
                        sFilter = sFilter + " AND ELXSAUP = '" + this.DAT02_ELSSAUP.GetValue().ToString() + "'";
                        dBONUS = Convert.ToDouble(Get_Numeric(dtsj.Compute("SUM(ELXCRAMT)", sFilter).ToString()));

                        dBONUS += Convert.ToDouble(dt.Rows[i]["FUNDAMT"].ToString());

                        this.DAT02_ELSINSURANCE.SetValue(dBONUS.ToString());  //4대보험(국민연금+의료보험+고용보험)

                        this.DAT02_ELSPEOPLE.SetValue(dt.Rows[i]["PEOPLECNT"].ToString()); //년평균인원
                        this.DAT02_ELSWKGUBN.SetValue("Y");  //Y 실적 N 예상
                        this.DAT02_ELSHISAB.SetValue(TYUserInfo.EmpNo);

                        datas.Add(new object[] {  this.DAT02_ELSYYMM.GetValue(), 
                                                  this.DAT02_ELSSAUP.GetValue(),
                                                  this.DAT02_ELSGUBN.GetValue(),
                                                  this.DAT02_ELSPAYTOTAL.GetValue(),
                                                  this.DAT02_ELSBONUS.GetValue(), 
                                                  this.DAT02_ELSPLANBONUS.GetValue(),
                                                  this.DAT02_ELSRETIREFUND.GetValue(),
                                                  this.DAT02_ELSINSURANCE.GetValue(),
                                                  this.DAT02_ELSPEOPLE.GetValue(),
                                                  this.DAT02_ELSWKGUBN.GetValue(),
                                                  this.DAT02_ELSHISAB.GetValue()
                                                });
                    } //for (int i = 0; i < dtjk.Rows.Count; i++)..end                   

                    sMaxValue = dt.Compute("MAX(PYDATE)", "").ToString();

                    if (sMaxValue != sEDATE)
                    {
                        //급여 계획 금액 가져오기
                        this.DbConnector.CommandClear();
                        if (j == 0)
                        {
                            this.DbConnector.Attach("TY_P_HR_2BMAF560", sMaxValue.Substring(0, 4), sMaxValue.Substring(4, 2)); //임원
                        }
                        else
                        {
                            this.DbConnector.Attach("TY_P_HR_2BMAE559", sMaxValue.Substring(0, 4), sMaxValue.Substring(4, 2)); //직원
                        }
                        DataTable dtpl = this.DbConnector.ExecuteDataTable();

                        for (int k = 0; k < dtpl.Rows.Count; k++)
                        {
                            this.DAT02_ELSYYMM.SetValue(dtpl.Rows[k]["MMYEAR"].ToString() + dtpl.Rows[k]["MMMONTH"].ToString());
                            this.DAT02_ELSSAUP.SetValue(dtpl.Rows[k]["MMCDDP"].ToString());
                            if (j == 0)
                            {
                                this.DAT02_ELSGUBN.SetValue("Y");
                            }
                            else
                            {
                                this.DAT02_ELSGUBN.SetValue("N");
                            }
                            dBONUS = 0;
                            dBONUS = Convert.ToDouble(dtpl.Rows[k]["MMCRAMT"].ToString()) + Convert.ToDouble(dtpl.Rows[k]["PLANYUNCHA"].ToString());
                            this.DAT02_ELSPAYTOTAL.SetValue(dBONUS.ToString());  //급여총액
                            this.DAT02_ELSBONUS.SetValue(dtpl.Rows[k]["PLANBONUS"].ToString());   //성과급
                            this.DAT02_ELSPLANBONUS.SetValue("0");
                            this.DAT02_ELSRETIREFUND.SetValue(dtpl.Rows[k]["PLANRETIREFUND"].ToString());  //퇴충금
                            this.DAT02_ELSINSURANCE.SetValue(dtpl.Rows[k]["PLANINSURANCE"].ToString());  //4대보험

                            sFilter = "PYDEPT = '" + this.DAT02_ELSSAUP.GetValue().ToString() + "'";
                            iPeople = Convert.ToInt16(Get_Numeric(dt.Compute("AVG(PEOPLECNT)", sFilter).ToString()));

                            this.DAT02_ELSPEOPLE.SetValue(iPeople.ToString()); //년평균인원

                            this.DAT02_ELSWKGUBN.SetValue("N");  //Y 실적 N 예상
                            this.DAT02_ELSHISAB.SetValue(TYUserInfo.EmpNo);

                            datas.Add(new object[] {  this.DAT02_ELSYYMM.GetValue(), 
                                                  this.DAT02_ELSSAUP.GetValue(),
                                                  this.DAT02_ELSGUBN.GetValue(),
                                                  this.DAT02_ELSPAYTOTAL.GetValue(),
                                                  this.DAT02_ELSBONUS.GetValue(), 
                                                  this.DAT02_ELSPLANBONUS.GetValue(),
                                                  this.DAT02_ELSRETIREFUND.GetValue(),
                                                  this.DAT02_ELSINSURANCE.GetValue(),
                                                  this.DAT02_ELSPEOPLE.GetValue(),
                                                  this.DAT02_ELSWKGUBN.GetValue(),
                                                  this.DAT02_ELSHISAB.GetValue()
                                                });
                        } //for (int k = 0; k < dtpl.Rows.Count; k++)...end
                    } //if (sMaxValue != sEDATE)...end                 

                } //if (dt.Rows.Count > 0)...end
            }

            if (datas.Count > 0)
            {
                this.DbConnector.CommandClear();
                foreach (object[] data in datas)
                {
                    this.DbConnector.Attach("TY_P_HR_2BM7G595", data);
                }

                this.DbConnector.ExecuteTranQueryList();
            }

            this.ShowMessage("TY_M_GB_26E30875");
                        
        }
        #endregion

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();  
        }
        #endregion
    }
}
