using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using GrapeCity.ActiveReports;
using TY.ER.AC00;
using TY.ER.GB00;

namespace TY.ER.AC00
{
    /// <summary>
    /// 부속서류 출력 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2013.12.03 15:39
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
    ///  PRT : 출력
    ///  INQOPTION : 조회구분
    ///  INQOPTION2 : 조회구분
    ///  VNGUBUN : 구분
    ///  S1CHK1 : 세금계산서 합계표
    ///  S1CHK10 : 부가세신고서
    ///  S1CHK2 : 매출계산서 합계표
    ///  S1CHK3 : 건물등감가상각자산취득명세서
    ///  S1CHK4 : 신용카드매출전표등수취명세서
    ///  S1CHK5 : 공제받지못할 매입세액명세서
    ///  S1CHK6 : 전자세금계산서 발급세액세서
    ///  S1CHK7 : 수출실적명세서
    ///  S1CHK8 : 영세율첨부서류명세서
    ///  S1CHK9 : 사업장별부가가치세과세표준및납부세액명세서
    ///  ELXYYMM : 기준년도
    /// </summary>
    public partial class TYACTX010P : TYBase
    {
        public TYACTX010P()
        {
            InitializeComponent();
        }

        private void TYACTX010P_Load(object sender, System.EventArgs e)
        {
            UP_Cookie_Load();

            this.SetStartingFocus(this.DTP01_ELXYYMM);
        }

        #region Description : 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            // 부가세 신고서
            if (CKB01_S1CHK10.Checked == true)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_4183O053",
                    this.DTP01_ELXYYMM.GetValue().ToString(),
                    this.CBO01_VNGUBUN.GetValue().ToString(),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3C910655",
                    this.DTP01_ELXYYMM.GetValue().ToString(),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                    CBO01_VNGUBUN.GetValue().ToString()
                    );

                DataTable dt2 = this.DbConnector.ExecuteDataTable();
                if (Convert.ToInt16(Get_Numeric(DTP01_ELXYYMM.GetString().ToString()).Substring(0, 4)) >= 2020)
                {
                    if (Convert.ToInt16(Get_Numeric(DTP01_ELXYYMM.GetString().ToString()).Substring(0, 4)) == 2020 && this.CBO01_INQOPTION.GetValue().ToString() == "11")
                    {
                        SectionReport rpt = new TYACTX019R1(dt2, this.DTP01_ELXYYMM.GetValue().ToString(), getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1), getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2));
                        (new TYERGB001P(rpt, dt)).ShowDialog();
                    }
                    else
                    {
                        SectionReport rpt = new TYACTX019R2(dt2, this.DTP01_ELXYYMM.GetValue().ToString(), getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1), getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2));
                        (new TYERGB001P(rpt, dt)).ShowDialog();
                    }
                }
                else if (Convert.ToInt16(DTP01_ELXYYMM.GetString().ToString().Substring(0, 4)) >= 2016 && Convert.ToInt16(DTP01_ELXYYMM.GetString().ToString().Substring(0, 4)) <= 2019)
                {
                    SectionReport rpt = new TYACTX019R1(dt2, this.DTP01_ELXYYMM.GetValue().ToString(), getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1), getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2));
                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
                else
                {
                    SectionReport rpt = new TYACTX019R(dt2, this.DTP01_ELXYYMM.GetValue().ToString(), getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1), getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2));
                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }

            }
            // 세금계산서 합계표
            if (CKB01_S1CHK1.Checked == true)
            {   
                this.DbConnector.CommandClear();

                if (this.CKB01_ATTAXGUBN1.Checked == true)
                {
                    this.DbConnector.Attach
                    (
                    "TY_P_AC_3C95X667",
                    this.DTP01_ELXYYMM.GetValue().ToString(),
                    this.CBO01_VNGUBUN.GetValue().ToString(),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                    "1",
                    "51,52,54,55", //매입
                    "1"
                    );

                    DataTable dt = this.DbConnector.ExecuteDataTable();

                    DataTable dt2 = UP_TOTAL_SUM_11R("1");

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3C910655",
                        this.DTP01_ELXYYMM.GetValue().ToString(),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                        CBO01_VNGUBUN.GetValue().ToString()
                        );

                    DataTable dt3 = this.DbConnector.ExecuteDataTable();

                    SectionReport rpt = new TYACTX011R1(dt2, dt3, this.DTP01_ELXYYMM.GetValue().ToString(), getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1), getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2));
                    (new TYERGB001P(rpt, UP_ConvertHap(dt, "1"))).ShowDialog();

                }
                if (this.CKB01_ATTAXGUBN2.Checked == true)
                {
                    this.DbConnector.Attach
                    (
                    "TY_P_AC_3C95X667",
                    this.DTP01_ELXYYMM.GetValue().ToString(),
                    this.CBO01_VNGUBUN.GetValue().ToString(),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                    "2",
                    "11,12,19",  //매출
                    "1"
                    );

                    DataTable dt4 = this.DbConnector.ExecuteDataTable();

                    DataTable dt5 = UP_TOTAL_SUM_11R("2");

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3C910655",
                        this.DTP01_ELXYYMM.GetValue().ToString(),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                        CBO01_VNGUBUN.GetValue().ToString()
                        );

                    DataTable dt6 = this.DbConnector.ExecuteDataTable();

                    SectionReport rpt2 = new TYACTX011R3(dt5, dt6, this.DTP01_ELXYYMM.GetValue().ToString(), getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1), getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2));
                    (new TYERGB001P(rpt2, UP_ConvertHap(dt4, "2"))).ShowDialog();
                }
            }
            
            // 건물등 감가상각자산 취득명세서
            if (CKB01_S1CHK3.Checked == true)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3C910655",
                    this.DTP01_ELXYYMM.GetValue().ToString(),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                    CBO01_VNGUBUN.GetValue().ToString()
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                DataTable dt2 = UP_TOTAL_SUM_13R();
                SectionReport rpt = new TYACTX013R(dt, this.DTP01_ELXYYMM.GetValue().ToString(), getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1), getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2));
                (new TYERGB001P(rpt, dt2)).ShowDialog();
            }
            // 신용카드 매출전표등 수취명세서
            if (CKB01_S1CHK4.Checked == true)
            {   
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3CH30787",
                    this.DTP01_ELXYYMM.GetValue().ToString(),
                    this.CBO01_VNGUBUN.GetValue().ToString(),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                    "58"
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                DataTable dt2 = UP_TOTAL_SUM_14R();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3C910655",
                    this.DTP01_ELXYYMM.GetValue().ToString(),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                    CBO01_VNGUBUN.GetValue().ToString()
                    );

                DataTable dt3 = this.DbConnector.ExecuteDataTable();

                if ((int.Parse(this.DTP01_ELXYYMM.GetValue().ToString()) >= 2021) || (this.DTP01_ELXYYMM.GetValue().ToString() == "2020" && this.CBO01_INQOPTION.GetValue().ToString() == "22"))
                {
                    SectionReport rpt = new TYACTX014R3(dt2, dt3, this.DTP01_ELXYYMM.GetValue().ToString(), getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1), getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2));
                    (new TYERGB001P(rpt, UP_ConvertHap(dt, "4"))).ShowDialog();
                }
                else
                {
                    SectionReport rpt = new TYACTX014R1(dt2, dt3, this.DTP01_ELXYYMM.GetValue().ToString(), getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1), getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2));
                    (new TYERGB001P(rpt, UP_ConvertHap(dt, "4"))).ShowDialog();
                }
            }
            // 공제받지 못할 매입세액 명세서
            if (CKB01_S1CHK5.Checked == true)
            {

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3CK2B844",
                    this.DTP01_ELXYYMM.GetValue().ToString(),
                    this.CBO01_VNGUBUN.GetValue().ToString(),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3C910655",
                    this.DTP01_ELXYYMM.GetValue().ToString(),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                    CBO01_VNGUBUN.GetValue().ToString()
                    );

                DataTable dt2 = this.DbConnector.ExecuteDataTable();

                SectionReport rpt = new TYACTX015R(dt2, this.DTP01_ELXYYMM.GetValue().ToString(), getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1), getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2));
                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            // 수출실적명세서
            if (CKB01_S1CHK7.Checked == true)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3CN14860",
                    this.DTP01_ELXYYMM.GetValue().ToString(),
                    this.CBO01_VNGUBUN.GetValue().ToString(),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                    "1"
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                DataTable dt2 = UP_TOTAL_SUM_15R();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3C910655",
                    this.DTP01_ELXYYMM.GetValue().ToString(),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                    CBO01_VNGUBUN.GetValue().ToString()
                    );

                DataTable dt3 = this.DbConnector.ExecuteDataTable();

                SectionReport rpt = new TYACTX016R1(dt2, dt3, this.DTP01_ELXYYMM.GetValue().ToString(), getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1), getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2));
                (new TYERGB001P(rpt, UP_ConvertHap(dt,"5"))).ShowDialog();
            }
            // 열세율첨부서류제출명세서
            if (CKB01_S1CHK8.Checked == true)
            {

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3CO2H866",
                    this.DTP01_ELXYYMM.GetValue().ToString(),
                    this.CBO01_VNGUBUN.GetValue().ToString(),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3C910655",
                    this.DTP01_ELXYYMM.GetValue().ToString(),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                    CBO01_VNGUBUN.GetValue().ToString()
                    );

                DataTable dt2 = this.DbConnector.ExecuteDataTable();

                SectionReport rpt = new TYACTX017R(dt2, this.DTP01_ELXYYMM.GetValue().ToString(), getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1), getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2));
                (new TYERGB001P(rpt, UP_ConvertHap(dt,"6"))).ShowDialog();
            }
            
            // 열세율 매출명세서
            if (CKB01_S1CHK11.Checked == true)
            {

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3CQ5U872",
                    this.DTP01_ELXYYMM.GetValue().ToString(),
                    this.CBO01_VNGUBUN.GetValue().ToString(),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3C910655",
                    this.DTP01_ELXYYMM.GetValue().ToString(),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                    CBO01_VNGUBUN.GetValue().ToString()
                    );

                DataTable dt2 = this.DbConnector.ExecuteDataTable();

                SectionReport rpt = new TYACTX018R(dt2, this.DTP01_ELXYYMM.GetValue().ToString(), getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1), getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2));
                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            // 계산서 합계표
            if (CKB01_S1CHK2.Checked == true)
            {
                this.DbConnector.CommandClear();

                if (this.CKB01_ATTAXGUBN3.Checked == true)
                {
                    this.DbConnector.Attach
                    (
                    "TY_P_AC_3CB6O731",
                    this.DTP01_ELXYYMM.GetValue().ToString(),
                    this.CBO01_VNGUBUN.GetValue().ToString(),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                    "1",
                    "59", //59 매입 22 매출
                    "1"
                    );

                    DataTable dt = this.DbConnector.ExecuteDataTable();

                    DataTable dt2 = UP_TOTAL_SUM_12R("1");

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3C910655",
                        this.DTP01_ELXYYMM.GetValue().ToString(),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                        CBO01_VNGUBUN.GetValue().ToString()
                        );

                    DataTable dt3 = this.DbConnector.ExecuteDataTable();

                    string sStdate = "00000000";
                    string sEddate = "00000000";
                    int iDD = 0;

                    DataTable dt_date = new DataTable();

                    // 작업년월 가져오기
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3CAB6679",
                        this.DTP01_ELXYYMM.GetValue().ToString(),
                        this.CBO01_VNGUBUN.GetValue().ToString(),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                        "1"
                        );

                    dt_date = this.DbConnector.ExecuteDataTable();

                    if (dt_date.Rows.Count > 0)
                    {
                        iDD = DateTime.DaysInMonth(int.Parse(dt_date.Rows[0]["O1EDYYMM"].ToString().Substring(0, 4)), int.Parse(dt_date.Rows[0]["O1EDYYMM"].ToString().Substring(4, 2)));

                        sStdate = dt_date.Rows[0]["O1STYYMM"].ToString() + "01";
                        sEddate = dt_date.Rows[0]["O1EDYYMM"].ToString() + Set_Fill2(Convert.ToString(iDD));
                    }

                    SectionReport rpt = new TYACTX012R1(dt2, dt3, this.DTP01_ELXYYMM.GetValue().ToString(), getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1), getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2), sStdate, sEddate, "1");
                    (new TYERGB001P(rpt, UP_ConvertHap(dt, "3"))).ShowDialog();

                }
                if (this.CKB01_ATTAXGUBN4.Checked == true)
                {
                    this.DbConnector.Attach
                    (
                    "TY_P_AC_3CB6O731",
                    this.DTP01_ELXYYMM.GetValue().ToString(),
                    this.CBO01_VNGUBUN.GetValue().ToString(),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                    "2",
                    "22", //59 매입 22 매출
                    "1"
                    );

                    DataTable dt = this.DbConnector.ExecuteDataTable();

                    DataTable dt2 = UP_TOTAL_SUM_12R("2");

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3C910655",
                        this.DTP01_ELXYYMM.GetValue().ToString(),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                        this.CBO01_VNGUBUN.GetValue().ToString()
                        );

                    DataTable dt3 = this.DbConnector.ExecuteDataTable();

                    SectionReport rpt = new TYACTX012R3(dt2, dt3, this.DTP01_ELXYYMM.GetValue().ToString(), getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1), getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2));
                    (new TYERGB001P(rpt, UP_ConvertHap(dt, "3"))).ShowDialog();

                }
            }
            // 사업장별 부가가치세 과세표준 및 납부세액 신고명세서 출력
            if (CKB01_S1CHK13.Checked == true && this.CBO01_VNGUBUN.GetValue().ToString() == "1")
            {
                string sAOGENNUM = string.Empty;

                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_41K5E135",
                    this.DTP01_ELXYYMM.GetValue().ToString(),
                    this.CBO01_VNGUBUN.GetValue().ToString(),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sAOGENNUM = dt.Rows[0]["AOGENNUM"].ToString();
                    dt.Clone();

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_42B8L317",
                        this.DTP01_ELXYYMM.GetValue().ToString(),
                        this.CBO01_VNGUBUN.GetValue().ToString(),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_AC_41K5D134",
                            this.DTP01_ELXYYMM.GetValue().ToString(),
                            getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                            getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        SectionReport rpt = new TYACTX020R(dt, this.DTP01_ELXYYMM.GetValue().ToString(), getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1), getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2), sAOGENNUM.ToString());

                        rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                        (new TYERGB001P(rpt, UP_ConvertHap(dt, "7"))).ShowDialog();
                    }
                    else
                    {
                        this.ShowMessage("TY_M_AC_42D9S359");
                        return;
                    }
                }
            }

            UP_Cookie_Save();
        }
        #endregion

        #region Description : 세금계산서 총합계
        private DataTable UP_TOTAL_SUM_11R(string sGGUBUN)
        {
            string sS1TAXCDGN_HAP = string.Empty;
            string sS1TAXCDGN1 = string.Empty;
            string sS1TAXCDGN2 = string.Empty;

            if (sGGUBUN == "1")
            {
                sS1TAXCDGN_HAP = "71,72,74,75,51,52,54,55";
                sS1TAXCDGN1 = "71,72,74,75";
                sS1TAXCDGN2 = "51,52,54,55";
            }
            else
            {
                sS1TAXCDGN_HAP = "61,62,68,69,11,12,19";
                sS1TAXCDGN1 = "61,62,68,69";
                sS1TAXCDGN2 = "11,12,19";
            }

            DataTable Retdt = new DataTable();

            DataRow row;

            Retdt.Columns.Add("VNCODE_CNT", typeof(System.String));
            Retdt.Columns.Add("MAESU_CNT", typeof(System.String));
            Retdt.Columns.Add("HAP_AMT", typeof(System.String));
            Retdt.Columns.Add("HAP_VAT", typeof(System.String));

            DataTable dt = new DataTable();

            for (int i = 0; i < 7; i++)
            {
                // 총합계
                if (i == 0)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3C61T592",
                        this.DTP01_ELXYYMM.GetValue().ToString(),
                        this.CBO01_VNGUBUN.GetValue().ToString(),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                        sGGUBUN.ToString(),
                        sS1TAXCDGN_HAP.ToString(),
                        ""
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["VNCODE_CNT"] = dt.Rows[0]["VNCODE_CNT"].ToString();
                        row["MAESU_CNT"] = dt.Rows[0]["MAESU_CNT"].ToString();
                        row["HAP_AMT"] = dt.Rows[0]["HAP_AMT"].ToString();
                        row["HAP_VAT"] = dt.Rows[0]["HAP_VAT"].ToString();

                        Retdt.Rows.Add(row);
                    }
                }
                else if (i == 1) // 전자 - 사업자
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3C61T592",
                        this.DTP01_ELXYYMM.GetValue().ToString(),
                        this.CBO01_VNGUBUN.GetValue().ToString(),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                        sGGUBUN.ToString(),
                        sS1TAXCDGN1.ToString(),
                        "1"
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["VNCODE_CNT"] = dt.Rows[0]["VNCODE_CNT"].ToString();
                        row["MAESU_CNT"] = dt.Rows[0]["MAESU_CNT"].ToString();
                        row["HAP_AMT"] = dt.Rows[0]["HAP_AMT"].ToString();
                        row["HAP_VAT"] = dt.Rows[0]["HAP_VAT"].ToString();

                        Retdt.Rows.Add(row);
                    }
                }
                else if (i == 2) // 전자 - 주민
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3C61T592",
                        this.DTP01_ELXYYMM.GetValue().ToString(),
                        this.CBO01_VNGUBUN.GetValue().ToString(),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                        sGGUBUN.ToString(),
                        sS1TAXCDGN1.ToString(),
                        "2"
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["VNCODE_CNT"] = dt.Rows[0]["VNCODE_CNT"].ToString();
                        row["MAESU_CNT"] = dt.Rows[0]["MAESU_CNT"].ToString();
                        row["HAP_AMT"] = dt.Rows[0]["HAP_AMT"].ToString();
                        row["HAP_VAT"] = dt.Rows[0]["HAP_VAT"].ToString();

                        Retdt.Rows.Add(row);
                    }
                }
                else if (i == 3) // 전자 - 소계
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3C61T592",
                        this.DTP01_ELXYYMM.GetValue().ToString(),
                        this.CBO01_VNGUBUN.GetValue().ToString(),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                        sGGUBUN.ToString(),
                        sS1TAXCDGN1.ToString(),
                        ""
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["VNCODE_CNT"] = dt.Rows[0]["VNCODE_CNT"].ToString();
                        row["MAESU_CNT"] = dt.Rows[0]["MAESU_CNT"].ToString();
                        row["HAP_AMT"] = dt.Rows[0]["HAP_AMT"].ToString();
                        row["HAP_VAT"] = dt.Rows[0]["HAP_VAT"].ToString();

                        Retdt.Rows.Add(row);
                    }
                }

                else if (i == 4) // 전자외 - 사업자
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3C61T592",
                        this.DTP01_ELXYYMM.GetValue().ToString(),
                        this.CBO01_VNGUBUN.GetValue().ToString(),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                        sGGUBUN.ToString(),
                        sS1TAXCDGN2.ToString(),
                        "1"
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["VNCODE_CNT"] = dt.Rows[0]["VNCODE_CNT"].ToString();
                        row["MAESU_CNT"] = dt.Rows[0]["MAESU_CNT"].ToString();
                        row["HAP_AMT"] = dt.Rows[0]["HAP_AMT"].ToString();
                        row["HAP_VAT"] = dt.Rows[0]["HAP_VAT"].ToString();

                        Retdt.Rows.Add(row);
                    }
                }
                else if (i == 5) // 전자외 - 주민
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3C61T592",
                        this.DTP01_ELXYYMM.GetValue().ToString(),
                        this.CBO01_VNGUBUN.GetValue().ToString(),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                        sGGUBUN.ToString(),
                        sS1TAXCDGN2.ToString(),
                        "2"
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["VNCODE_CNT"] = dt.Rows[0]["VNCODE_CNT"].ToString();
                        row["MAESU_CNT"] = dt.Rows[0]["MAESU_CNT"].ToString();
                        row["HAP_AMT"] = dt.Rows[0]["HAP_AMT"].ToString();
                        row["HAP_VAT"] = dt.Rows[0]["HAP_VAT"].ToString();

                        Retdt.Rows.Add(row);
                    }
                }
                else if (i == 6) // 전자외 - 소계
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3C61T592",
                        this.DTP01_ELXYYMM.GetValue().ToString(),
                        this.CBO01_VNGUBUN.GetValue().ToString(),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                        sGGUBUN.ToString(),
                        sS1TAXCDGN2.ToString(),
                        ""
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["VNCODE_CNT"] = dt.Rows[0]["VNCODE_CNT"].ToString();
                        row["MAESU_CNT"] = dt.Rows[0]["MAESU_CNT"].ToString();
                        row["HAP_AMT"] = dt.Rows[0]["HAP_AMT"].ToString();
                        row["HAP_VAT"] = dt.Rows[0]["HAP_VAT"].ToString();

                        Retdt.Rows.Add(row);
                    }
                }
            }

            return Retdt;
        }
        #endregion

        #region Description : 계산서 총합계 
        private DataTable UP_TOTAL_SUM_12R(string sGGUBUN)
        {
            string sS2TAXCDGN_HAP = string.Empty;
            string sS2TAXCDGN1 = string.Empty;
            string sS2TAXCDGN2 = string.Empty;

            if (sGGUBUN.ToString() == "1")
            {
                sS2TAXCDGN_HAP = "79,59";
                sS2TAXCDGN1 = "79";
                sS2TAXCDGN2 = "59";
            }
            else
            {
                sS2TAXCDGN_HAP = "66,22";
                sS2TAXCDGN1 = "66";
                sS2TAXCDGN2 = "22";
            }

            DataTable Retdt = new DataTable();

            DataRow row;

            Retdt.Columns.Add("TITLE1", typeof(System.String));
            Retdt.Columns.Add("TITLE2", typeof(System.String));
            Retdt.Columns.Add("ATELECTGB", typeof(System.String));
            Retdt.Columns.Add("S2SJGUBN", typeof(System.String));
            Retdt.Columns.Add("VNCODE_CNT", typeof(System.String));
            Retdt.Columns.Add("MAESU_CNT", typeof(System.String));
            Retdt.Columns.Add("HAP_AMT", typeof(System.String));
            Retdt.Columns.Add("HAP_VAT", typeof(System.String));

            DataTable dt = new DataTable();

            for (int i = 0; i < 7; i++)
            {
                // 총합계
                if (i == 0)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3CA9V675",
                        this.DTP01_ELXYYMM.GetValue().ToString(),
                        this.CBO01_VNGUBUN.GetValue().ToString(),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                        sGGUBUN.ToString(),
                        sS2TAXCDGN_HAP.ToString(),
                        ""
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["TITLE1"] = "합      계";
                        row["TITLE2"] = "";
                        row["ATELECTGB"] = "";
                        row["S2SJGUBN"] = "";
                        row["VNCODE_CNT"] = dt.Rows[0]["VNCODE_CNT"].ToString();
                        row["MAESU_CNT"] = dt.Rows[0]["MAESU_CNT"].ToString();
                        row["HAP_AMT"] = dt.Rows[0]["HAP_AMT"].ToString();
                        row["HAP_VAT"] = dt.Rows[0]["HAP_VAT"].ToString();

                        Retdt.Rows.Add(row);
                    }
                }
                else if (i == 1) // 전자 - 사업자
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3CA9V675",
                        this.DTP01_ELXYYMM.GetValue().ToString(),
                        this.CBO01_VNGUBUN.GetValue().ToString(),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                        sGGUBUN.ToString(),
                        sS2TAXCDGN1.ToString(),
                        "1"
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["TITLE1"] = "과세기간 종료일 다음달 11일까지 전송된 전자세금계산서 발급분";
                        row["TITLE2"] = "사업자등록번호 발급분";
                        row["ATELECTGB"] = "1";
                        row["S2SJGUBN"] = "1";
                        row["VNCODE_CNT"] = dt.Rows[0]["VNCODE_CNT"].ToString();
                        row["MAESU_CNT"] = dt.Rows[0]["MAESU_CNT"].ToString();
                        row["HAP_AMT"] = dt.Rows[0]["HAP_AMT"].ToString();
                        row["HAP_VAT"] = dt.Rows[0]["HAP_VAT"].ToString();

                        Retdt.Rows.Add(row);
                    }
                }
                else if (i == 2) // 전자 - 주민
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3CA9V675",
                        this.DTP01_ELXYYMM.GetValue().ToString(),
                        this.CBO01_VNGUBUN.GetValue().ToString(),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                        sGGUBUN.ToString(),
                        sS2TAXCDGN1.ToString(),
                        "2"
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["TITLE1"] = "과세기간 종료일 다음달 11일까지 전송된 전자세금계산서 발급분";
                        row["TITLE2"] = "주민등록번호 발급분";
                        row["ATELECTGB"] = "1";
                        row["S2SJGUBN"] = "2";
                        row["VNCODE_CNT"] = dt.Rows[0]["VNCODE_CNT"].ToString();
                        row["MAESU_CNT"] = dt.Rows[0]["MAESU_CNT"].ToString();
                        row["HAP_AMT"] = dt.Rows[0]["HAP_AMT"].ToString();
                        row["HAP_VAT"] = dt.Rows[0]["HAP_VAT"].ToString();

                        Retdt.Rows.Add(row);
                    }
                }
                else if (i == 3) // 전자 - 소계
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3CA9V675",
                        this.DTP01_ELXYYMM.GetValue().ToString(),
                        this.CBO01_VNGUBUN.GetValue().ToString(),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                        sGGUBUN.ToString(),
                        sS2TAXCDGN1.ToString(),
                        ""
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["TITLE1"] = "과세기간 종료일 다음달 11일까지 전송된 전자세금계산서 발급분";
                        row["TITLE2"] = "소      계";
                        row["ATELECTGB"] = "1";
                        row["S2SJGUBN"] = "";
                        row["VNCODE_CNT"] = dt.Rows[0]["VNCODE_CNT"].ToString();
                        row["MAESU_CNT"] = dt.Rows[0]["MAESU_CNT"].ToString();
                        row["HAP_AMT"] = dt.Rows[0]["HAP_AMT"].ToString();
                        row["HAP_VAT"] = dt.Rows[0]["HAP_VAT"].ToString();

                        Retdt.Rows.Add(row);
                    }
                }

                else if (i == 4) // 전자외 - 사업자
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3CA9V675",
                        this.DTP01_ELXYYMM.GetValue().ToString(),
                        this.CBO01_VNGUBUN.GetValue().ToString(),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                        sGGUBUN.ToString(),
                        sS2TAXCDGN2.ToString(),
                        "1"
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["TITLE1"] = "위 전자세금계산서 외의 발급분";
                        row["TITLE2"] = "사업자등록번호 발급분";
                        row["ATELECTGB"] = "2";
                        row["S2SJGUBN"] = "1";
                        row["VNCODE_CNT"] = dt.Rows[0]["VNCODE_CNT"].ToString();
                        row["MAESU_CNT"] = dt.Rows[0]["MAESU_CNT"].ToString();
                        row["HAP_AMT"] = dt.Rows[0]["HAP_AMT"].ToString();
                        row["HAP_VAT"] = dt.Rows[0]["HAP_VAT"].ToString();

                        Retdt.Rows.Add(row);
                    }
                }
                else if (i == 5) // 전자외 - 주민
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3CA9V675",
                        this.DTP01_ELXYYMM.GetValue().ToString(),
                        this.CBO01_VNGUBUN.GetValue().ToString(),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                        sGGUBUN.ToString(),
                        sS2TAXCDGN2.ToString(),
                        "2"
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["TITLE1"] = "위 전자세금계산서 외의 발급분";
                        row["TITLE2"] = "주민등록번호 발급분";
                        row["ATELECTGB"] = "2";
                        row["S2SJGUBN"] = "2";
                        row["VNCODE_CNT"] = dt.Rows[0]["VNCODE_CNT"].ToString();
                        row["MAESU_CNT"] = dt.Rows[0]["MAESU_CNT"].ToString();
                        row["HAP_AMT"] = dt.Rows[0]["HAP_AMT"].ToString();
                        row["HAP_VAT"] = dt.Rows[0]["HAP_VAT"].ToString();

                        Retdt.Rows.Add(row);
                    }
                }
                else if (i == 6) // 전자외 - 소계
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3CA9V675",
                        this.DTP01_ELXYYMM.GetValue().ToString(),
                        this.CBO01_VNGUBUN.GetValue().ToString(),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                        sGGUBUN.ToString(),
                        sS2TAXCDGN2.ToString(),
                        ""
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["TITLE1"] = "위 전자세금계산서 외의 발급분";
                        row["TITLE2"] = "소      계";
                        row["ATELECTGB"] = "2";
                        row["S2SJGUBN"] = "";
                        row["VNCODE_CNT"] = dt.Rows[0]["VNCODE_CNT"].ToString();
                        row["MAESU_CNT"] = dt.Rows[0]["MAESU_CNT"].ToString();
                        row["HAP_AMT"] = dt.Rows[0]["HAP_AMT"].ToString();
                        row["HAP_VAT"] = dt.Rows[0]["HAP_VAT"].ToString();

                        Retdt.Rows.Add(row);
                    }
                }
            }

            return Retdt;
        }
        #endregion

        #region Description : 건물등감가상각자산 총합계
        private DataTable UP_TOTAL_SUM_13R()
        {
            string sS2TAXCDGN_HAP = string.Empty;
            string sS2TAXCDGN1 = string.Empty;
            string sS2TAXCDGN2 = string.Empty;

            DataTable Retdt = new DataTable();

            DataRow row;

            Retdt.Columns.Add("GUNSU_CNT", typeof(System.String));
            Retdt.Columns.Add("HAP_AMT", typeof(System.String));
            Retdt.Columns.Add("HAP_VAT", typeof(System.String));

            DataTable dt = new DataTable();

            for (int i = 0; i < 5; i++)
            {
                // 총합계
                if (i == 0)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3CG30769",
                        this.DTP01_ELXYYMM.GetValue().ToString(),
                        this.CBO01_VNGUBUN.GetValue().ToString(),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                        "12200200,12200300,12200400,12200500,12200600,12200700,12200800,12200900,12210000,11101001"
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["GUNSU_CNT"] = dt.Rows[0]["GUNSU_CNT"].ToString();
                        row["HAP_AMT"] = dt.Rows[0]["HAP_AMT"].ToString();
                        row["HAP_VAT"] = dt.Rows[0]["HAP_VAT"].ToString();

                        Retdt.Rows.Add(row);
                    }
                }
                else if (i == 1) // 건물.구축물
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3CG30769",
                        this.DTP01_ELXYYMM.GetValue().ToString(),
                        this.CBO01_VNGUBUN.GetValue().ToString(),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                        "12200200,12200300"
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["GUNSU_CNT"] = dt.Rows[0]["GUNSU_CNT"].ToString();
                        row["HAP_AMT"] = dt.Rows[0]["HAP_AMT"].ToString();
                        row["HAP_VAT"] = dt.Rows[0]["HAP_VAT"].ToString();

                        Retdt.Rows.Add(row);
                    }
                }
                else if (i == 2) // 기계장치
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3CG30769",
                        this.DTP01_ELXYYMM.GetValue().ToString(),
                        this.CBO01_VNGUBUN.GetValue().ToString(),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                        "12200400"
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["GUNSU_CNT"] = dt.Rows[0]["GUNSU_CNT"].ToString();
                        row["HAP_AMT"] = dt.Rows[0]["HAP_AMT"].ToString();
                        row["HAP_VAT"] = dt.Rows[0]["HAP_VAT"].ToString();

                        Retdt.Rows.Add(row);
                    }
                }
                else if (i == 3) // 차량 운반구
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3CG30769",
                        this.DTP01_ELXYYMM.GetValue().ToString(),
                        this.CBO01_VNGUBUN.GetValue().ToString(),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                        "12200500,12200600"
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["GUNSU_CNT"] = dt.Rows[0]["GUNSU_CNT"].ToString();
                        row["HAP_AMT"] = dt.Rows[0]["HAP_AMT"].ToString();
                        row["HAP_VAT"] = dt.Rows[0]["HAP_VAT"].ToString();

                        Retdt.Rows.Add(row);
                    }
                }

                else if (i == 4) // 기타 감가상각자산
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3CG30769",
                        this.DTP01_ELXYYMM.GetValue().ToString(),
                        this.CBO01_VNGUBUN.GetValue().ToString(),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                        "12200700,12200800,12200900,12210000,11101001"
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["GUNSU_CNT"] = dt.Rows[0]["GUNSU_CNT"].ToString();
                        row["HAP_AMT"] = dt.Rows[0]["HAP_AMT"].ToString();
                        row["HAP_VAT"] = dt.Rows[0]["HAP_VAT"].ToString();

                        Retdt.Rows.Add(row);
                    }
                }
            }

            return Retdt;
        }
        #endregion

        #region Description : 신용카드매출전표 수취명세서 총합계
        private DataTable UP_TOTAL_SUM_14R()
        {
            string sS2TAXCDGN_HAP = string.Empty;
            string sS2TAXCDGN1 = string.Empty;
            string sS2TAXCDGN2 = string.Empty;

            DataTable Retdt = new DataTable();

            DataRow row;

            Retdt.Columns.Add("GUNSU_CNT", typeof(System.String));
            Retdt.Columns.Add("HAP_AMT", typeof(System.String));
            Retdt.Columns.Add("HAP_VAT", typeof(System.String));

            DataTable dt = new DataTable();

            for (int i = 0; i < 3; i++)
            {
                // 총합계
                if (i == 0)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3CHBW786",
                        this.DTP01_ELXYYMM.GetValue().ToString(),
                        this.CBO01_VNGUBUN.GetValue().ToString(),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                        "57,58"
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["GUNSU_CNT"] = dt.Rows[0]["GUNSU_CNT"].ToString();
                        row["HAP_AMT"] = dt.Rows[0]["HAP_AMT"].ToString();
                        row["HAP_VAT"] = dt.Rows[0]["HAP_VAT"].ToString();

                        Retdt.Rows.Add(row);
                    }
                }
                else if (i == 1) // 현금영수증
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3CHBW786",
                        this.DTP01_ELXYYMM.GetValue().ToString(),
                        this.CBO01_VNGUBUN.GetValue().ToString(),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                        "57"
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["GUNSU_CNT"] = dt.Rows[0]["GUNSU_CNT"].ToString();
                        row["HAP_AMT"] = dt.Rows[0]["HAP_AMT"].ToString();
                        row["HAP_VAT"] = dt.Rows[0]["HAP_VAT"].ToString();

                        Retdt.Rows.Add(row);
                    }
                }
                else if (i == 2) // 기타 신용카드
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3CHBW786",
                        this.DTP01_ELXYYMM.GetValue().ToString(),
                        this.CBO01_VNGUBUN.GetValue().ToString(),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                        "58"
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["GUNSU_CNT"] = dt.Rows[0]["GUNSU_CNT"].ToString();
                        row["HAP_AMT"] = dt.Rows[0]["HAP_AMT"].ToString();
                        row["HAP_VAT"] = dt.Rows[0]["HAP_VAT"].ToString();

                        Retdt.Rows.Add(row);
                    }
                }
            }

            return Retdt;
        }
        #endregion

        #region Description : 수출실적명세서 총합계
        private DataTable UP_TOTAL_SUM_15R()
        {
            string sS7EXPGUBN = string.Empty;
            string sTITLE = string.Empty;
            string sGUBUN = string.Empty;

            DataTable Retdt = new DataTable();

            DataRow row;

            Retdt.Columns.Add("TITLE", typeof(System.String));
            Retdt.Columns.Add("CNT", typeof(System.String));
            Retdt.Columns.Add("S7FORGIAMT", typeof(System.String));
            Retdt.Columns.Add("S7WONHAAMT", typeof(System.String));
            Retdt.Columns.Add("BIGO", typeof(System.String));
            Retdt.Columns.Add("GUBUN", typeof(System.String));

            DataTable dt = new DataTable();

            for (int i = 0; i < 3; i++)
            {
                switch (i)
                {
                    case 0:
                        sS7EXPGUBN = "";
                        sTITLE = "⑨합계";
                        sGUBUN = "HAP";

                        break;

                    case 1:
                        sS7EXPGUBN = "1";
                        sTITLE = "⑩수출재화(=⑫합계)";
                        sGUBUN = "1";

                        break;
                    case 2:
                        sS7EXPGUBN = "2";
                        sTITLE = "⑪기타영세율적용";
                        sGUBUN = "2";

                        break;
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3CK13833",
                    this.DTP01_ELXYYMM.GetValue().ToString(),
                    this.CBO01_VNGUBUN.GetValue().ToString(),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),
                    sS7EXPGUBN.ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    row = Retdt.NewRow();

                    row["TITLE"] = sTITLE.ToString();
                    row["CNT"] = dt.Rows[0]["CNT"].ToString();
                    row["S7FORGIAMT"] = dt.Rows[0]["S7FORGIAMT"].ToString();
                    row["S7WONHAAMT"] = dt.Rows[0]["S7WONHAAMT"].ToString();
                    row["BIGO"] = "";
                    row["GUBUN"] = sGUBUN.ToString();

                    Retdt.Rows.Add(row);
                }
            }

            return Retdt;
        }
        #endregion

        #region Description : 출력 데이터셋 변환
        protected DataTable UP_ConvertHap(DataTable dt, string sGubn)
        {
            string sNUMBER = string.Empty;
            string sS1SAUPNO = string.Empty;
            string sVNSANGHO = string.Empty;
            string sS1TAXCDGN = string.Empty;
            string sMAESU_CNT = string.Empty;
            string sHAP_AMT = string.Empty;
            string sHAP_VAT = string.Empty;
            int iBLANK = 0;

            if (sGubn == "1")
            {
                iBLANK = 14 - ((dt.Rows.Count - 5) % 14);
                if (iBLANK == 14) {
                    iBLANK = 0;
                }
            }
            else if(sGubn == "2")
            {
                iBLANK = 15 - ((dt.Rows.Count - 5) % 15);
                if (iBLANK == 15)
                {
                    iBLANK = 0;
                }
            }
            else if (sGubn == "3")
            {
                iBLANK = 13 - ((dt.Rows.Count - 5) % 13);
                if (iBLANK == 13)
                {
                    iBLANK = 0;
                }
            }
            else if (sGubn == "4")
            {
                iBLANK = 26 - ((dt.Rows.Count - 15) % 26);
                if (iBLANK == 26)
                {
                    iBLANK = 0;
                }
            }
            else if (sGubn == "5")
            {
                iBLANK = 22 - ((dt.Rows.Count - 12) % 22);
                if (iBLANK == 22)
                {
                    iBLANK = 0;
                }
            }
            else if (sGubn == "6")
            {
                iBLANK = 17 - ((dt.Rows.Count - 12) % 18);
                if (iBLANK == 17)
                {
                    iBLANK = 0;
                }
            }
            else if (sGubn == "7")
            {
                iBLANK = 1;
            }

            int i = 0;

            sNUMBER = "";
            sS1SAUPNO = "";
            sVNSANGHO = "";
            sS1TAXCDGN = "";
            sMAESU_CNT = "";
            sHAP_AMT = "";
            sHAP_VAT = "";

            DataTable Retdt = dt;


            if (sGubn == "1" || sGubn == "2")
            {   
                if (dt != null && dt.Rows.Count > 5)
                {
                    DataRow row;

                    for (i = 1; i <= iBLANK; i++)
                    {
                        row = Retdt.NewRow();

                        row["NUMBER"] = DBNull.Value;
                        row["S1SAUPNO"] = "";
                        row["VNSANGHO"] = "";
                        row["S1TAXCDGN"] = "";
                        row["MAESU_CNT"] = DBNull.Value;
                        row["HAP_AMT"] = DBNull.Value;
                        row["HAP_VAT"] = DBNull.Value;

                        Retdt.Rows.Add(row);
                    }
                }
                else if (dt.Rows.Count < 5)
                {
                    DataRow row;

                    iBLANK = 5 - (dt.Rows.Count);

                    for (i = 1; i <= iBLANK; i++)
                    {
                        row = Retdt.NewRow();

                        row["NUMBER"] = DBNull.Value;
                        row["S1SAUPNO"] = "";
                        row["VNSANGHO"] = "";
                        row["S1TAXCDGN"] = "";
                        row["MAESU_CNT"] = DBNull.Value;
                        row["HAP_AMT"] = DBNull.Value;
                        row["HAP_VAT"] = DBNull.Value;

                        Retdt.Rows.Add(row);
                    }
                }
            }
            else if (sGubn == "3")
            {
                if (dt != null && dt.Rows.Count > 5)
                {
                    DataRow row;

                    for (i = 1; i <= iBLANK; i++)
                    {
                        row = Retdt.NewRow();

                        row["NUMBER"] = DBNull.Value;
                        row["S2SAUPNO"] = "";
                        row["VNSANGHO"] = "";
                        row["S2TAXCDGN"] = "";
                        row["MAESU_CNT"] = DBNull.Value;
                        row["HAP_AMT"] = DBNull.Value;
                        row["HAP_VAT"] = DBNull.Value;

                        Retdt.Rows.Add(row);
                    }
                }
                else if (dt.Rows.Count < 5)
                {
                    DataRow row;

                    iBLANK = 5 - (dt.Rows.Count);

                    for (i = 1; i <= iBLANK; i++)
                    {
                        row = Retdt.NewRow();

                        row["NUMBER"] = DBNull.Value;
                        row["S2SAUPNO"] = "";
                        row["VNSANGHO"] = "";
                        row["S2TAXCDGN"] = "";
                        row["MAESU_CNT"] = DBNull.Value;
                        row["HAP_AMT"] = DBNull.Value;
                        row["HAP_VAT"] = DBNull.Value;

                        Retdt.Rows.Add(row);
                    }
                }
            }
            else if (sGubn == "4")
            {
                if (dt != null && dt.Rows.Count > 15)
                {
                    DataRow row;

                    for (i = 1; i <= iBLANK; i++)
                    {
                        row = Retdt.NewRow();

                        row["NUMBER"] = DBNull.Value;
                        row["S4CREDITNO"] = "";
                        row["S4CORPNO"] = "";
                        row["S4TXCNT"] = DBNull.Value;
                        row["S4TXAMT"] = DBNull.Value;
                        row["S4TXVAT"] = DBNull.Value;

                        Retdt.Rows.Add(row);
                    }
                }
                else if (dt.Rows.Count < 15)
                {
                    DataRow row;

                    iBLANK = 15 - (dt.Rows.Count);

                    for (i = 1; i <= iBLANK; i++)
                    {
                        row = Retdt.NewRow();

                        row["NUMBER"] = DBNull.Value;
                        row["S4CREDITNO"] = "";
                        row["S4CORPNO"] = "";
                        row["S4TXCNT"] = DBNull.Value;
                        row["S4TXAMT"] = DBNull.Value;
                        row["S4TXVAT"] = DBNull.Value;

                        Retdt.Rows.Add(row);
                    }
                }
            }
            else if (sGubn == "5")
            {
                if (dt != null && dt.Rows.Count > 12)
                {
                    DataRow row;

                    for (i = 1; i <= iBLANK; i++)
                    {
                        row = Retdt.NewRow();

                        row["NUMBER"] = DBNull.Value;
                        row["S7SHIPDT"] = "";
                        row["S7CUSTCD"] = "";
                        row["S7CURRCD"] = DBNull.Value;
                        row["FRDESC"] = DBNull.Value;
                        row["S7CXCHAN"] = DBNull.Value;
                        row["S7FORGIAMT"] = DBNull.Value;
                        row["S7WONHAAMT"] = DBNull.Value;

                        Retdt.Rows.Add(row);
                    }
                }
                else if (dt.Rows.Count < 12)
                {
                    DataRow row;

                    iBLANK = 12 - (dt.Rows.Count);

                    for (i = 1; i <= iBLANK; i++)
                    {
                        row = Retdt.NewRow();

                        row["NUMBER"] = DBNull.Value;
                        row["S7SHIPDT"] = "";
                        row["S7CUSTCD"] = "";
                        row["S7CURRCD"] = DBNull.Value;
                        row["FRDESC"] = DBNull.Value;
                        row["S7CXCHAN"] = DBNull.Value;
                        row["S7FORGIAMT"] = DBNull.Value;
                        row["S7WONHAAMT"] = DBNull.Value;

                        Retdt.Rows.Add(row);
                    }
                }
            }
            else if (sGubn == "6")
            {
                if (dt != null && dt.Rows.Count > 12)
                {
                    DataRow row;

                    for (i = 1; i <= iBLANK; i++)
                    {
                        row = Retdt.NewRow();

                        row["NUMBER"] = DBNull.Value;
                        row["S8JPNO"] = "";
                        row["S8DATE"] = "";
                        row["S8SHIPDT"] = DBNull.Value;
                        row["S8DOCNUM"] = DBNull.Value;
                        row["S8ISSUER"] = DBNull.Value;
                        row["S8CURRCD"] = DBNull.Value;
                        row["FRDESC"] = DBNull.Value;
                        row["S8CXCHAN"] = DBNull.Value;
                        row["S8SBFORAMT"] = DBNull.Value;
                        row["S8SBWONAMT"] = DBNull.Value;

                        Retdt.Rows.Add(row);
                    }
                }
                else if (dt.Rows.Count < 12)
                {
                    DataRow row;

                    iBLANK = 11 - (dt.Rows.Count);

                    for (i = 1; i <= iBLANK; i++)
                    {
                        row = Retdt.NewRow();

                        row["NUMBER"] = DBNull.Value;
                        row["S8JPNO"] = "";
                        row["S8DATE"] = "";
                        row["S8SHIPDT"] = DBNull.Value;
                        row["S8DOCNUM"] = DBNull.Value;
                        row["S8ISSUER"] = DBNull.Value;
                        row["S8CURRCD"] = DBNull.Value;
                        row["FRDESC"] = DBNull.Value;
                        row["S8CXCHAN"] = DBNull.Value;
                        row["S8SBFORAMT"] = DBNull.Value;
                        row["S8SBWONAMT"] = DBNull.Value;

                        Retdt.Rows.Add(row);
                    }
                }
            }
            else if (sGubn == "7")
            {
                DataRow row;

                for (i = 1; i <= iBLANK; i++)
                {
                    row = Retdt.NewRow();

                    row["VSBRANCH"]    = DBNull.Value;
                    row["VSVENDCD"]    = DBNull.Value;
                    row["JUSO"]        = DBNull.Value;
                    row["SAUPNO"]      = DBNull.Value;
                    row["MAECHUL_AMT"] = DBNull.Value;
                    row["MAECHUL_TAX"] = DBNull.Value;
                    row["MAEIP_AMT"]   = DBNull.Value;
                    row["MAEIP_TAX"]   = DBNull.Value;
                    row["GASANSE"]     = DBNull.Value;
                    row["GONGJAESE"]   = DBNull.Value;
                    row["NAPBUSE"]     = DBNull.Value;
                    row["YOUNGSEYUL"]  = DBNull.Value;

                    Retdt.Rows.Add(row);
                }
            }

            return Retdt;
        }
        #endregion

        #region Description : 체크박스 이벤트
        private void CKB01_S1CHK13_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_S1CHK3.Checked == false)
            {
                this.CKB01_S1CHK12.Checked = false;
            }
        }
        private void CKB01_S1CHK1_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_S1CHK1.Checked == false)
            {
                this.CKB01_ATTAXGUBN1.Checked = false;
                this.CKB01_ATTAXGUBN2.Checked = false;
                this.CKB01_S1CHK12.Checked = false;
            }
            else if (CKB01_ATTAXGUBN1.Checked == true || CKB01_ATTAXGUBN2.Checked == true)
            {
                
            }
            else
            {
                this.CKB01_ATTAXGUBN1.Checked = true;
                this.CKB01_ATTAXGUBN2.Checked = true;
            }
        }

        private void CKB01_S1CHK2_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_S1CHK2.Checked == false)
            {
                this.CKB01_ATTAXGUBN3.Checked = false;
                this.CKB01_ATTAXGUBN4.Checked = false;
                this.CKB01_S1CHK12.Checked = false;
            }
            else if (CKB01_ATTAXGUBN3.Checked == true || CKB01_ATTAXGUBN4.Checked == true)
            {

            }
            else
            {   
                this.CKB01_ATTAXGUBN3.Checked = true;
                this.CKB01_ATTAXGUBN4.Checked = true;
            }
        }

        private void CKB01_S1CHK3_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_S1CHK3.Checked == false)
            {
                this.CKB01_S1CHK12.Checked = false;
            }
        }

        private void CKB01_S1CHK4_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_S1CHK4.Checked == false)
            {   
                this.CKB01_S1CHK12.Checked = false;
            }
        }

        private void CKB01_S1CHK5_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_S1CHK5.Checked == false)
            {
                this.CKB01_S1CHK12.Checked = false;
            }
        }

        //private void CKB01_S1CHK6_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (CKB01_S1CHK6.Checked == false)
        //    {
        //        this.CKB01_S1CHK12.Checked = false;
        //    }
        //}

        private void CKB01_S1CHK7_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_S1CHK7.Checked == false)
            {
                this.CKB01_S1CHK12.Checked = false;
            }
        }

        private void CKB01_S1CHK8_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_S1CHK8.Checked == false)
            {
                this.CKB01_S1CHK12.Checked = false;
            }
        }

        //private void CKB01_S1CHK9_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (CKB01_S1CHK9.Checked == false)
        //    {
        //        this.CKB01_S1CHK12.Checked = false;
        //    }
        //}

        private void CKB01_S1CHK10_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_S1CHK10.Checked == false)
            {   
                this.CKB01_S1CHK12.Checked = false;
            }
        }

        private void CKB01_S1CHK11_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_S1CHK11.Checked == false)
            {
                this.CKB01_S1CHK12.Checked = false;
            }
        }

        private void CKB01_S1CHK12_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_S1CHK12.Checked == true)
            {
                CKB01_S1CHK1.Checked = true;
                CKB01_S1CHK2.Checked = true;
                CKB01_S1CHK3.Checked = true;
                CKB01_S1CHK4.Checked = true;
                CKB01_S1CHK5.Checked = true;
                //CKB01_S1CHK6.Checked = true;
                CKB01_S1CHK7.Checked = true;
                CKB01_S1CHK8.Checked = true;
                //CKB01_S1CHK9.Checked = true;
                CKB01_S1CHK10.Checked = true;
                CKB01_S1CHK11.Checked = true;
                CKB01_S1CHK13.Checked = true;
            }
            else if (CKB01_S1CHK1.Checked == true && CKB01_S1CHK2.Checked == true && CKB01_S1CHK3.Checked == true && CKB01_S1CHK4.Checked == true && CKB01_S1CHK5.Checked == true &&
                    CKB01_S1CHK7.Checked == true && CKB01_S1CHK8.Checked == true && CKB01_S1CHK10.Checked == true && CKB01_S1CHK11.Checked == true && CKB01_S1CHK13.Checked == true)
            {
                CKB01_S1CHK1.Checked = false;
                CKB01_S1CHK2.Checked = false;
                CKB01_S1CHK3.Checked = false;
                CKB01_S1CHK4.Checked = false;
                CKB01_S1CHK5.Checked = false;
                //CKB01_S1CHK6.Checked = false;
                CKB01_S1CHK7.Checked = false;
                CKB01_S1CHK8.Checked = false;
                //CKB01_S1CHK9.Checked = false;
                CKB01_S1CHK10.Checked = false;
                CKB01_S1CHK11.Checked = false;
                CKB01_S1CHK13.Checked = false;
            }
        }
        #endregion

        #region Description : 매입매출버튼 이벤트
        private void CKB01_ATTAXGUBN1_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_ATTAXGUBN1.Checked == true || CKB01_ATTAXGUBN2.Checked == true)
            {
                CKB01_S1CHK1.Checked = true;
            }
            else
            {
                CKB01_S1CHK1.Checked = false;
            }
        }

        private void CKB01_ATTAXGUBN2_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_ATTAXGUBN1.Checked == true || CKB01_ATTAXGUBN2.Checked == true)
            {
                CKB01_S1CHK1.Checked = true;
            }
            else
            {
                CKB01_S1CHK1.Checked = false;
            }
        }

        private void CKB01_ATTAXGUBN3_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_ATTAXGUBN3.Checked == true || CKB01_ATTAXGUBN4.Checked == true)
            {
                CKB01_S1CHK2.Checked = true;
            }
            else
            {
                CKB01_S1CHK2.Checked = false;
            }
        }

        private void CKB01_ATTAXGUBN4_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_ATTAXGUBN3.Checked == true || CKB01_ATTAXGUBN4.Checked == true)
            {
                CKB01_S1CHK2.Checked = true;
            }
            else
            {
                CKB01_S1CHK2.Checked = false;
            }
        }
        #endregion

        #region Description : 쿠키 불러오기
        private void UP_Cookie_Load()
        {
            if (TYCookie.Chk == "Cookie")
            {
                this.DTP01_ELXYYMM.SetValue(TYCookie.Year);
                this.CBO01_VNGUBUN.SetValue(TYCookie.Branch);
                this.CBO01_INQOPTION.SetValue(TYCookie.Confgb);
            }
            else
            {
                this.DTP01_ELXYYMM.SetValue(DateTime.Now.ToString("yyyy"));
            }
        }
        #endregion

        #region Description : 쿠키 저장
        private void UP_Cookie_Save()
        {
            TYCookie.Save(this.DTP01_ELXYYMM.GetValue().ToString(), this.CBO01_VNGUBUN.GetValue().ToString(), this.CBO01_INQOPTION.GetValue().ToString());
        }
        #endregion

        private void CBO01_VNGUBUN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CBO01_VNGUBUN.GetValue().ToString() == "1"){
                this.CKB01_S1CHK13.Visible = true;
                this.LBL51_S1CHK13.Visible = true;
            }
            else
            {
                this.CKB01_S1CHK13.Visible = false;
                this.LBL51_S1CHK13.Visible = false;
            }
        }

    }
}