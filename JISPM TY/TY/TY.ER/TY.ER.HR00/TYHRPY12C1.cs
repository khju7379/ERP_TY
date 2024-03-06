using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using GrapeCity.ActiveReports;
using TY.ER.GB00;

namespace TY.ER.HR00
{
    /// <summary>
    /// 급여지급명세서 출력팝업 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.12.30 18:04
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
    ///  KBBUSEO : 부서
    ///  KBSABUN : 사번
    ///  PAYGUBN : 급여구분
    ///  EDGUBN : 구분
    ///  PAYJIDATE : 지급일자
    ///  PAYYYMM : 급여년월
    /// </summary>
    public partial class TYHRPY12C1 : TYBase
    {
        string fsPAYYYMM = string.Empty;
        string fsPAYGUBN = string.Empty;
        string fsPAYJIDATE = string.Empty;
        private string fsSessionId = string.Empty;


        #region description : 페이지 로드
        public TYHRPY12C1(string sPAYYYMM, string sPAYGUBN, string sPAYJIDATE)
        {
            InitializeComponent();

            fsPAYYYMM = sPAYYYMM;
            fsPAYGUBN = sPAYGUBN;
            fsPAYJIDATE = sPAYJIDATE;
        }

        private void TYHRPY12C1_Load(object sender, System.EventArgs e)
        {
            this.BTN61_PRT.ProcessCheck += new TButton.CheckHandler(BTN61_PRT_ProcessCheck);

            this.TXT01_PAYYYMM.SetValue(fsPAYYYMM.Substring(0, 4) + "-" + fsPAYYYMM.Substring(4, 2));
            this.CBH01_PAYGUBN.SetValue(fsPAYGUBN);
            this.TXT01_PAYJIDATE.SetValue(fsPAYJIDATE.Substring(0, 4) + "-" + fsPAYJIDATE.Substring(4, 2) + "-" + fsPAYJIDATE.Substring(6, 2));
            this.TXT01_PAYYYMM.SetReadOnly(true);
            this.CBH01_PAYGUBN.SetReadOnly(true);
            this.TXT01_PAYJIDATE.SetReadOnly(true);
            this.CBH01_KBBUSEO.DummyValue = fsPAYJIDATE.ToString();

            //BATID번호 부여(SSID)
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7M958");
            decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());
            this.fsSessionId = this.IPAdresss + Employer.EmpNo + dAutoSeq.ToString();
        }
        #endregion

        #region description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region description : 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {

            string sProCedureId = string.Empty;

            // 명세서 출력
            if (this.CBO01_EDGUBN.GetValue().ToString() == "1")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_55QFD334",
                                        this.fsSessionId,
                                        this.TXT01_PAYYYMM.GetValue().ToString().Replace("-", ""),
                                        this.CBH01_PAYGUBN.GetValue().ToString(),
                                        this.TXT01_PAYJIDATE.GetValue().ToString().Replace("-", ""),
                                        this.CBH01_KBBUSEO.GetValue().ToString(),
                                        this.CBH01_KBSABUN.GetValue().ToString()
                                        );
                DataTable dt = this.DbConnector.ExecuteDataTable();
                DataTable dt2 = UP_SumTable(dt);

                //SectionReport rpt = new TYHRPY12R2(dt2);
                SectionReport rpt = new TYHRPY12R5(dt2);
                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            // 집계표 출력
            else
            {
                string sGUBUNNM = string.Empty;

                if (this.CBH01_PAYGUBN.GetValue().ToString() == "M1")
                {
                    sGUBUNNM = "급.상여";
                }
                else if (this.CBH01_PAYGUBN.GetValue().ToString() == "M2")
                {
                    sGUBUNNM = "소 급";
                }
                else if (this.CBH01_PAYGUBN.GetValue().ToString() == "R1")
                {
                    sGUBUNNM = "소 급";
                }
                else if (this.CBH01_PAYGUBN.GetValue().ToString() == "S2")
                {
                    sGUBUNNM = "상여금";
                }
                else if (this.CBH01_PAYGUBN.GetValue().ToString() == "Y1")
                {
                    sGUBUNNM = "년차";
                }
                else if (this.CBH01_PAYGUBN.GetValue().ToString() == "H1")
                {
                    sGUBUNNM = "휴가비";
                }
                else if (this.CBH01_PAYGUBN.GetValue().ToString() == "T1")
                {
                    sGUBUNNM = "성과급";
                }
                else if (this.CBH01_PAYGUBN.GetValue().ToString() == "T2")
                {
                    sGUBUNNM = "특별상여Ⅰ";
                }
                else if (this.CBH01_PAYGUBN.GetValue().ToString() == "T3")
                {
                    sGUBUNNM = "특별상여Ⅱ";
                }
                else if (this.CBH01_PAYGUBN.GetValue().ToString() == "T4")
                {
                    sGUBUNNM = "타결일시금";
                }

                if (Convert.ToInt32(this.TXT01_PAYYYMM.GetValue().ToString().Replace("-", "").Substring(0, 4)) > 2018)
                {
                    sProCedureId = "TY_P_HR_568DD410";
                }
                else if (Convert.ToInt32(this.TXT01_PAYYYMM.GetValue().ToString().Replace("-", "").Substring(0, 4)) >= 2015 &&
                         Convert.ToInt32(this.TXT01_PAYYYMM.GetValue().ToString().Replace("-", "").Substring(0, 4)) <= 2018)
                {
                    sProCedureId = "TY_P_HR_91N9P541";
                }
                else
                {
                    sProCedureId = "TY_P_HR_996B4204";
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach(sProCedureId,
                                        this.fsSessionId,
                                        this.CBH01_PAYGUBN.GetValue().ToString(),
                                        this.TXT01_PAYYYMM.GetValue().ToString().Replace("-", ""),
                                        this.TXT01_PAYJIDATE.GetValue().ToString().Replace("-", "")
                                        );
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (Convert.ToInt32(this.TXT01_PAYYYMM.GetValue().ToString().Replace("-", "")) >= 202101)
                {
                    SectionReport rpt = new TYHRPY12R4(this.TXT01_PAYYYMM.GetValue().ToString().Replace("-", ""), sGUBUNNM);
                    (new TYERGB001P(rpt, UP_DtChange(dt, "1"))).ShowDialog();
                }
                else if (Convert.ToInt32(this.TXT01_PAYYYMM.GetValue().ToString().Replace("-", "")) <= 202012)
                {
                    SectionReport rpt = new TYHRPY12R1(this.TXT01_PAYYYMM.GetValue().ToString().Replace("-", ""), sGUBUNNM);
                    (new TYERGB001P(rpt, UP_DtChange(dt, "1"))).ShowDialog();
                }
                else
                {
                    SectionReport rpt = new TYHRPY12R3(this.TXT01_PAYYYMM.GetValue().ToString().Replace("-", ""), sGUBUNNM);
                    (new TYERGB001P(rpt, UP_DtChange(dt, "2"))).ShowDialog();
                }

            }
        }

        private void BTN61_PRT_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int iManCnt = 0;
            int iManCntTotal = 0;
            double dPayHap = 0;
            double dPayHapTotal = 0;

            string sProCedureId = string.Empty;

            // 급여지급명세서 임시테이블 생성
            string sRET_MSG = string.Empty;
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_55TBK344", this.fsSessionId,
                                                        this.TXT01_PAYYYMM.GetValue().ToString().Replace("-", ""),
                                                        this.CBH01_PAYGUBN.GetValue().ToString(),
                                                        this.TXT01_PAYJIDATE.GetValue().ToString().Replace("-", ""),
                                                        TYUserInfo.EmpNo,
                                                        sRET_MSG
                                                        );
            sRET_MSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (Convert.ToInt32(this.TXT01_PAYYYMM.GetValue().ToString().Replace("-", "").Substring(0, 4)) > 2018)
            {
                sProCedureId = "TY_P_HR_568DD410";
            }
            else if (Convert.ToInt32(this.TXT01_PAYYYMM.GetValue().ToString().Replace("-", "").Substring(0, 4)) >= 2015 &&
                     Convert.ToInt32(this.TXT01_PAYYYMM.GetValue().ToString().Replace("-", "").Substring(0, 4)) <= 2018)
            {
                sProCedureId = "TY_P_HR_91N9P541";
            }
            else
            {
                sProCedureId = "TY_P_HR_996B4204";
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach(sProCedureId,
                                    this.fsSessionId,
                                    this.CBH01_PAYGUBN.GetValue().ToString(),
                                    this.TXT01_PAYYYMM.GetValue().ToString().Replace("-", ""),
                                    this.TXT01_PAYJIDATE.GetValue().ToString().Replace("-", "")
                                    );
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ROWGUBN"].ToString().Trim() == "S")
                    {
                        iManCnt = iManCnt + Convert.ToInt16(Get_Numeric(dt.Rows[i]["COUNT"].ToString().Trim()));
                        dPayHap = dPayHap + Convert.ToDouble(Get_Numeric(dt.Rows[i]["REJPTOTAL"].ToString()));
                    }

                    if (dt.Rows[i]["ROWGUBN"].ToString().Trim() == "T")
                    {
                        iManCntTotal = Convert.ToInt16(Get_Numeric(dt.Rows[i]["COUNT"].ToString().Trim()));
                        dPayHapTotal = Convert.ToDouble(Get_Numeric(dt.Rows[i]["REJPTOTAL"].ToString().Trim()));
                    }
                }

                if (iManCnt != iManCntTotal)
                {
                    this.ShowCustomMessage("급여집계표 출력인원과 급여작업의 인원의 합계가 다릅니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }

                if (dPayHap != dPayHapTotal)
                {
                    this.ShowCustomMessage("급여집계표 출력 지급합계와 급여작업의 지급합계가 다릅니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }
        }
        #endregion

        #region description : 출력 구분 선택 이벤트
        private void CBO01_EDGUBN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CBO01_EDGUBN.GetValue().ToString() == "1")
            {
                this.CBH01_KBBUSEO.SetReadOnly(false);
                this.CBH01_KBSABUN.SetReadOnly(false);
            }
            else
            {
                this.CBH01_KBBUSEO.SetReadOnly(true);
                this.CBH01_KBSABUN.SetReadOnly(true);
            }
        }
        #endregion

        #region description : 급여 지급내역 부서별 합계 테이블 생성
        private DataTable UP_SumTable(DataTable dt)
        {
            double dREJPAMOUNT1 = 0;
            double dREJPAMOUNT2 = 0;
            double dREJPAMOUNT3 = 0;
            double dREJPAMOUNT4 = 0;
            double dREJPAMOUNT5 = 0;
            double dREJPAMOUNT6 = 0;
            double dREJPAMOUNT7 = 0;
            double dREJPAMOUNT8 = 0;
            double dREJPAMOUNT9 = 0;
            double dREJPAMOUNT10 = 0;

            double dREJPAMOUNT11 = 0;
            double dREJPAMOUNT12 = 0;
            double dREJPAMOUNT13 = 0;
            double dREJPAMOUNT14 = 0;
            double dREJPAMOUNT15 = 0;
            double dREJPAMOUNT16 = 0;

            double dREGPAMOUNT1 = 0;
            double dREGPAMOUNT2 = 0;
            double dREGPAMOUNT3 = 0;
            double dREGPAMOUNT4 = 0;
            double dREGPAMOUNT5 = 0;
            double dREGPAMOUNT6 = 0;
            double dREGPAMOUNT7 = 0;
            double dREGPAMOUNT8 = 0;
            double dREGPAMOUNT9 = 0;
            double dREGPAMOUNT10 = 0;

            double dREGPAMOUNT11 = 0;
            double dREGPAMOUNT12 = 0;
            double dREGPAMOUNT13 = 0;
            double dREGPAMOUNT14 = 0;
            double dREGPAMOUNT15 = 0;
            double dREGPAMOUNT16 = 0;

            double dREJPTOTAL = 0;
            double dREGPTOTAL = 0;
            double dRECHAIN = 0;

            double dREJPAMOUNTSUM1 = 0;
            double dREJPAMOUNTSUM2 = 0;
            double dREJPAMOUNTSUM3 = 0;
            double dREJPAMOUNTSUM4 = 0;
            double dREJPAMOUNTSUM5 = 0;
            double dREJPAMOUNTSUM6 = 0;
            double dREJPAMOUNTSUM7 = 0;
            double dREJPAMOUNTSUM8 = 0;
            double dREJPAMOUNTSUM9 = 0;
            double dREJPAMOUNTSUM10 = 0;

            double dREJPAMOUNTSUM11 = 0;
            double dREJPAMOUNTSUM12 = 0;
            double dREJPAMOUNTSUM13 = 0;
            double dREJPAMOUNTSUM14 = 0;
            double dREJPAMOUNTSUM15 = 0;
            double dREJPAMOUNTSUM16 = 0;

            double dREGPAMOUNTSUM1 = 0;
            double dREGPAMOUNTSUM2 = 0;
            double dREGPAMOUNTSUM3 = 0;
            double dREGPAMOUNTSUM4 = 0;
            double dREGPAMOUNTSUM5 = 0;
            double dREGPAMOUNTSUM6 = 0;
            double dREGPAMOUNTSUM7 = 0;
            double dREGPAMOUNTSUM8 = 0;
            double dREGPAMOUNTSUM9 = 0;
            double dREGPAMOUNTSUM10 = 0;

            double dREGPAMOUNTSUM11 = 0;
            double dREGPAMOUNTSUM12 = 0;
            double dREGPAMOUNTSUM13 = 0;
            double dREGPAMOUNTSUM14 = 0;
            double dREGPAMOUNTSUM15 = 0;
            double dREGPAMOUNTSUM16 = 0;

            double dREJPTOTALSUM = 0;
            double dREGPTOTALSUM = 0;
            double dRECHAINSUM = 0;

            DataTable rtnDt = new DataTable();

            rtnDt.Columns.Add("REJPAMOUNT1", typeof(System.Double));
            rtnDt.Columns.Add("REJPAMOUNT2", typeof(System.Double));
            rtnDt.Columns.Add("REJPAMOUNT3", typeof(System.Double));
            rtnDt.Columns.Add("REJPAMOUNT4", typeof(System.Double));
            rtnDt.Columns.Add("REJPAMOUNT5", typeof(System.Double));
            rtnDt.Columns.Add("REJPAMOUNT6", typeof(System.Double));
            rtnDt.Columns.Add("REJPAMOUNT7", typeof(System.Double));
            rtnDt.Columns.Add("REJPAMOUNT8", typeof(System.Double));
            rtnDt.Columns.Add("REJPAMOUNT9", typeof(System.Double));
            rtnDt.Columns.Add("REJPAMOUNT10", typeof(System.Double));

            rtnDt.Columns.Add("REJPAMOUNT11", typeof(System.Double));
            rtnDt.Columns.Add("REJPAMOUNT12", typeof(System.Double));
            rtnDt.Columns.Add("REJPAMOUNT13", typeof(System.Double));
            rtnDt.Columns.Add("REJPAMOUNT14", typeof(System.Double));
            rtnDt.Columns.Add("REJPAMOUNT15", typeof(System.Double));
            rtnDt.Columns.Add("REJPAMOUNT16", typeof(System.Double));

            rtnDt.Columns.Add("REGPAMOUNT1", typeof(System.Double));
            rtnDt.Columns.Add("REGPAMOUNT2", typeof(System.Double));
            rtnDt.Columns.Add("REGPAMOUNT3", typeof(System.Double));
            rtnDt.Columns.Add("REGPAMOUNT4", typeof(System.Double));
            rtnDt.Columns.Add("REGPAMOUNT5", typeof(System.Double));
            rtnDt.Columns.Add("REGPAMOUNT6", typeof(System.Double));
            rtnDt.Columns.Add("REGPAMOUNT7", typeof(System.Double));
            rtnDt.Columns.Add("REGPAMOUNT8", typeof(System.Double));
            rtnDt.Columns.Add("REGPAMOUNT9", typeof(System.Double));
            rtnDt.Columns.Add("REGPAMOUNT10", typeof(System.Double));

            rtnDt.Columns.Add("REGPAMOUNT11", typeof(System.Double));
            rtnDt.Columns.Add("REGPAMOUNT12", typeof(System.Double));
            rtnDt.Columns.Add("REGPAMOUNT13", typeof(System.Double));
            rtnDt.Columns.Add("REGPAMOUNT14", typeof(System.Double));
            rtnDt.Columns.Add("REGPAMOUNT15", typeof(System.Double));
            rtnDt.Columns.Add("REGPAMOUNT16", typeof(System.Double));

            rtnDt.Columns.Add("REJPTOTAL", typeof(System.Double));
            rtnDt.Columns.Add("REGPTOTAL", typeof(System.Double));
            rtnDt.Columns.Add("RECHAIN", typeof(System.Double));

            DataRow row;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0)
                {
                    if (dt.Rows[i]["REDEPT"].ToString() != dt.Rows[i - 1]["REDEPT"].ToString())
                    {
                        row = rtnDt.NewRow();

                        row["REJPAMOUNT1"] = dREJPAMOUNT1;
                        row["REJPAMOUNT2"] = dREJPAMOUNT2;
                        row["REJPAMOUNT3"] = dREJPAMOUNT3;
                        row["REJPAMOUNT4"] = dREJPAMOUNT4;
                        row["REJPAMOUNT5"] = dREJPAMOUNT5;
                        row["REJPAMOUNT6"] = dREJPAMOUNT6;
                        row["REJPAMOUNT7"] = dREJPAMOUNT7;
                        row["REJPAMOUNT8"] = dREJPAMOUNT8;
                        row["REJPAMOUNT9"] = dREJPAMOUNT9;
                        row["REJPAMOUNT10"] = dREJPAMOUNT10;

                        row["REJPAMOUNT11"] = dREJPAMOUNT11;
                        row["REJPAMOUNT12"] = dREJPAMOUNT12;
                        row["REJPAMOUNT13"] = dREJPAMOUNT13;
                        row["REJPAMOUNT14"] = dREJPAMOUNT14;
                        row["REJPAMOUNT15"] = dREJPAMOUNT15;
                        row["REJPAMOUNT16"] = dREJPAMOUNT16;

                        row["REGPAMOUNT1"] = dREGPAMOUNT1;
                        row["REGPAMOUNT2"] = dREGPAMOUNT2;
                        row["REGPAMOUNT3"] = dREGPAMOUNT3;
                        row["REGPAMOUNT4"] = dREGPAMOUNT4;
                        row["REGPAMOUNT5"] = dREGPAMOUNT5;
                        row["REGPAMOUNT6"] = dREGPAMOUNT6;
                        row["REGPAMOUNT7"] = dREGPAMOUNT7;
                        row["REGPAMOUNT8"] = dREGPAMOUNT8;
                        row["REGPAMOUNT9"] = dREGPAMOUNT9;
                        row["REGPAMOUNT10"] = dREGPAMOUNT10;

                        row["REGPAMOUNT11"] = dREGPAMOUNT11;
                        row["REGPAMOUNT12"] = dREGPAMOUNT12;
                        row["REGPAMOUNT13"] = dREGPAMOUNT13;
                        row["REGPAMOUNT14"] = dREGPAMOUNT14;
                        row["REGPAMOUNT15"] = dREGPAMOUNT15;
                        row["REGPAMOUNT16"] = dREGPAMOUNT16;

                        row["REJPTOTAL"] = dREJPTOTAL;
                        row["REGPTOTAL"] = dREGPTOTAL;
                        row["RECHAIN"] = dRECHAIN;

                        dREJPTOTAL = 0;
                        dREGPTOTAL = 0;
                        dRECHAIN = 0;

                        rtnDt.Rows.Add(row);

                        dREJPAMOUNT1 = 0;
                        dREJPAMOUNT2 = 0;
                        dREJPAMOUNT3 = 0;
                        dREJPAMOUNT4 = 0;
                        dREJPAMOUNT5 = 0;
                        dREJPAMOUNT6 = 0;
                        dREJPAMOUNT7 = 0;
                        dREJPAMOUNT8 = 0;
                        dREJPAMOUNT9 = 0;
                        dREJPAMOUNT10 = 0;

                        dREJPAMOUNT11 = 0;
                        dREJPAMOUNT12 = 0;
                        dREJPAMOUNT13 = 0;
                        dREJPAMOUNT14 = 0;
                        dREJPAMOUNT15 = 0;
                        dREJPAMOUNT16 = 0;

                        dREGPAMOUNT1 = 0;
                        dREGPAMOUNT2 = 0;
                        dREGPAMOUNT3 = 0;
                        dREGPAMOUNT4 = 0;
                        dREGPAMOUNT5 = 0;
                        dREGPAMOUNT6 = 0;
                        dREGPAMOUNT7 = 0;
                        dREGPAMOUNT8 = 0;
                        dREGPAMOUNT9 = 0;
                        dREGPAMOUNT10 = 0;

                        dREGPAMOUNT11 = 0;
                        dREGPAMOUNT12 = 0;
                        dREGPAMOUNT13 = 0;
                        dREGPAMOUNT14 = 0;
                        dREGPAMOUNT15 = 0;
                        dREGPAMOUNT16 = 0;

                        dREJPTOTAL = 0;
                        dREGPTOTAL = 0;
                        dRECHAIN = 0;
                    }
                }
                dREJPAMOUNT1 += Convert.ToDouble(dt.Rows[i]["REJPAMOUNT1"]);
                dREJPAMOUNT2 += Convert.ToDouble(dt.Rows[i]["REJPAMOUNT2"]);
                dREJPAMOUNT3 += Convert.ToDouble(dt.Rows[i]["REJPAMOUNT3"]);
                dREJPAMOUNT4 += Convert.ToDouble(dt.Rows[i]["REJPAMOUNT4"]);
                dREJPAMOUNT5 += Convert.ToDouble(dt.Rows[i]["REJPAMOUNT5"]);
                dREJPAMOUNT6 += Convert.ToDouble(dt.Rows[i]["REJPAMOUNT6"]);
                dREJPAMOUNT7 += Convert.ToDouble(dt.Rows[i]["REJPAMOUNT7"]);
                dREJPAMOUNT8 += Convert.ToDouble(dt.Rows[i]["REJPAMOUNT8"]);
                dREJPAMOUNT9 += Convert.ToDouble(dt.Rows[i]["REJPAMOUNT9"]);
                dREJPAMOUNT10 += Convert.ToDouble(dt.Rows[i]["REJPAMOUNT10"]);

                dREJPAMOUNT11 += Convert.ToDouble(dt.Rows[i]["REJPAMOUNT11"]);
                dREJPAMOUNT12 += Convert.ToDouble(dt.Rows[i]["REJPAMOUNT12"]);
                dREJPAMOUNT13 += Convert.ToDouble(dt.Rows[i]["REJPAMOUNT13"]);
                dREJPAMOUNT14 += Convert.ToDouble(dt.Rows[i]["REJPAMOUNT14"]);
                dREJPAMOUNT15 += Convert.ToDouble(dt.Rows[i]["REJPAMOUNT15"]);
                dREJPAMOUNT16 += Convert.ToDouble(dt.Rows[i]["REJPAMOUNT16"]);

                dREGPAMOUNT1 += Convert.ToDouble(dt.Rows[i]["REGPAMOUNT1"]);
                dREGPAMOUNT2 += Convert.ToDouble(dt.Rows[i]["REGPAMOUNT2"]);
                dREGPAMOUNT3 += Convert.ToDouble(dt.Rows[i]["REGPAMOUNT3"]);
                dREGPAMOUNT4 += Convert.ToDouble(dt.Rows[i]["REGPAMOUNT4"]);
                dREGPAMOUNT5 += Convert.ToDouble(dt.Rows[i]["REGPAMOUNT5"]);
                dREGPAMOUNT6 += Convert.ToDouble(dt.Rows[i]["REGPAMOUNT6"]);
                dREGPAMOUNT7 += Convert.ToDouble(dt.Rows[i]["REGPAMOUNT7"]);
                dREGPAMOUNT8 += Convert.ToDouble(dt.Rows[i]["REGPAMOUNT8"]);
                dREGPAMOUNT9 += Convert.ToDouble(dt.Rows[i]["REGPAMOUNT9"]);
                dREGPAMOUNT10 += Convert.ToDouble(dt.Rows[i]["REGPAMOUNT10"]);

                dREGPAMOUNT11 += Convert.ToDouble(dt.Rows[i]["REGPAMOUNT11"]);
                dREGPAMOUNT12 += Convert.ToDouble(dt.Rows[i]["REGPAMOUNT12"]);
                dREGPAMOUNT13 += Convert.ToDouble(dt.Rows[i]["REGPAMOUNT13"]);
                dREGPAMOUNT14 += Convert.ToDouble(dt.Rows[i]["REGPAMOUNT14"]);
                dREGPAMOUNT15 += Convert.ToDouble(dt.Rows[i]["REGPAMOUNT15"]);
                dREGPAMOUNT16 += Convert.ToDouble(dt.Rows[i]["REGPAMOUNT16"]);

                dREJPTOTAL += Convert.ToDouble(dt.Rows[i]["REJPTOTAL"]);
                dREGPTOTAL += Convert.ToDouble(dt.Rows[i]["REGPTOTAL"]);
                dRECHAIN += Convert.ToDouble(dt.Rows[i]["RECHAIN"]);

                dREJPAMOUNTSUM1 += Convert.ToDouble(dt.Rows[i]["REJPAMOUNT1"]);
                dREJPAMOUNTSUM2 += Convert.ToDouble(dt.Rows[i]["REJPAMOUNT2"]);
                dREJPAMOUNTSUM3 += Convert.ToDouble(dt.Rows[i]["REJPAMOUNT3"]);
                dREJPAMOUNTSUM4 += Convert.ToDouble(dt.Rows[i]["REJPAMOUNT4"]);
                dREJPAMOUNTSUM5 += Convert.ToDouble(dt.Rows[i]["REJPAMOUNT5"]);
                dREJPAMOUNTSUM6 += Convert.ToDouble(dt.Rows[i]["REJPAMOUNT6"]);
                dREJPAMOUNTSUM7 += Convert.ToDouble(dt.Rows[i]["REJPAMOUNT7"]);
                dREJPAMOUNTSUM8 += Convert.ToDouble(dt.Rows[i]["REJPAMOUNT8"]);
                dREJPAMOUNTSUM9 += Convert.ToDouble(dt.Rows[i]["REJPAMOUNT9"]);
                dREJPAMOUNTSUM10 += Convert.ToDouble(dt.Rows[i]["REJPAMOUNT10"]);

                dREJPAMOUNTSUM11 += Convert.ToDouble(dt.Rows[i]["REJPAMOUNT11"]);
                dREJPAMOUNTSUM12 += Convert.ToDouble(dt.Rows[i]["REJPAMOUNT12"]);
                dREJPAMOUNTSUM13 += Convert.ToDouble(dt.Rows[i]["REJPAMOUNT13"]);
                dREJPAMOUNTSUM14 += Convert.ToDouble(dt.Rows[i]["REJPAMOUNT14"]);
                dREJPAMOUNTSUM15 += Convert.ToDouble(dt.Rows[i]["REJPAMOUNT15"]);
                dREJPAMOUNTSUM16 += Convert.ToDouble(dt.Rows[i]["REJPAMOUNT16"]);

                dREGPAMOUNTSUM1 += Convert.ToDouble(dt.Rows[i]["REGPAMOUNT1"]);
                dREGPAMOUNTSUM2 += Convert.ToDouble(dt.Rows[i]["REGPAMOUNT2"]);
                dREGPAMOUNTSUM3 += Convert.ToDouble(dt.Rows[i]["REGPAMOUNT3"]);
                dREGPAMOUNTSUM4 += Convert.ToDouble(dt.Rows[i]["REGPAMOUNT4"]);
                dREGPAMOUNTSUM5 += Convert.ToDouble(dt.Rows[i]["REGPAMOUNT5"]);
                dREGPAMOUNTSUM6 += Convert.ToDouble(dt.Rows[i]["REGPAMOUNT6"]);
                dREGPAMOUNTSUM7 += Convert.ToDouble(dt.Rows[i]["REGPAMOUNT7"]);
                dREGPAMOUNTSUM8 += Convert.ToDouble(dt.Rows[i]["REGPAMOUNT8"]);
                dREGPAMOUNTSUM9 += Convert.ToDouble(dt.Rows[i]["REGPAMOUNT9"]);
                dREGPAMOUNTSUM10 += Convert.ToDouble(dt.Rows[i]["REGPAMOUNT10"]);

                dREGPAMOUNTSUM11 += Convert.ToDouble(dt.Rows[i]["REGPAMOUNT11"]);
                dREGPAMOUNTSUM12 += Convert.ToDouble(dt.Rows[i]["REGPAMOUNT12"]);
                dREGPAMOUNTSUM13 += Convert.ToDouble(dt.Rows[i]["REGPAMOUNT13"]);
                dREGPAMOUNTSUM14 += Convert.ToDouble(dt.Rows[i]["REGPAMOUNT14"]);
                dREGPAMOUNTSUM15 += Convert.ToDouble(dt.Rows[i]["REGPAMOUNT15"]);
                dREGPAMOUNTSUM16 += Convert.ToDouble(dt.Rows[i]["REGPAMOUNT16"]);

                dREJPTOTALSUM += Convert.ToDouble(dt.Rows[i]["REJPTOTAL"]);
                dREGPTOTALSUM += Convert.ToDouble(dt.Rows[i]["REGPTOTAL"]);
                dRECHAINSUM += Convert.ToDouble(dt.Rows[i]["RECHAIN"]);
            }
            // 마지막 부서별 합계 추가
            row = rtnDt.NewRow();

            row["REJPAMOUNT1"] = dREJPAMOUNT1;
            row["REJPAMOUNT2"] = dREJPAMOUNT2;
            row["REJPAMOUNT3"] = dREJPAMOUNT3;
            row["REJPAMOUNT4"] = dREJPAMOUNT4;
            row["REJPAMOUNT5"] = dREJPAMOUNT5;
            row["REJPAMOUNT6"] = dREJPAMOUNT6;
            row["REJPAMOUNT7"] = dREJPAMOUNT7;
            row["REJPAMOUNT8"] = dREJPAMOUNT8;
            row["REJPAMOUNT9"] = dREJPAMOUNT9;
            row["REJPAMOUNT10"] = dREJPAMOUNT10;

            row["REJPAMOUNT11"] = dREJPAMOUNT11;
            row["REJPAMOUNT12"] = dREJPAMOUNT12;
            row["REJPAMOUNT13"] = dREJPAMOUNT13;
            row["REJPAMOUNT14"] = dREJPAMOUNT14;
            row["REJPAMOUNT15"] = dREJPAMOUNT15;
            row["REJPAMOUNT16"] = dREJPAMOUNT16;

            row["REGPAMOUNT1"] = dREGPAMOUNT1;
            row["REGPAMOUNT2"] = dREGPAMOUNT2;
            row["REGPAMOUNT3"] = dREGPAMOUNT3;
            row["REGPAMOUNT4"] = dREGPAMOUNT4;
            row["REGPAMOUNT5"] = dREGPAMOUNT5;
            row["REGPAMOUNT6"] = dREGPAMOUNT6;
            row["REGPAMOUNT7"] = dREGPAMOUNT7;
            row["REGPAMOUNT8"] = dREGPAMOUNT8;
            row["REGPAMOUNT9"] = dREGPAMOUNT9;
            row["REGPAMOUNT10"] = dREGPAMOUNT10;

            row["REGPAMOUNT11"] = dREGPAMOUNT11;
            row["REGPAMOUNT12"] = dREGPAMOUNT12;
            row["REGPAMOUNT13"] = dREGPAMOUNT13;
            row["REGPAMOUNT14"] = dREGPAMOUNT14;
            row["REGPAMOUNT15"] = dREGPAMOUNT15;
            row["REGPAMOUNT16"] = dREGPAMOUNT16;

            row["REJPTOTAL"] = dREJPTOTAL;
            row["REGPTOTAL"] = dREGPTOTAL;
            row["RECHAIN"] = dRECHAIN;

            rtnDt.Rows.Add(row);

            // 총계 추가
            row = rtnDt.NewRow();

            row["REJPAMOUNT1"] = dREJPAMOUNTSUM1;
            row["REJPAMOUNT2"] = dREJPAMOUNTSUM2;
            row["REJPAMOUNT3"] = dREJPAMOUNTSUM3;
            row["REJPAMOUNT4"] = dREJPAMOUNTSUM4;
            row["REJPAMOUNT5"] = dREJPAMOUNTSUM5;
            row["REJPAMOUNT6"] = dREJPAMOUNTSUM6;
            row["REJPAMOUNT7"] = dREJPAMOUNTSUM7;
            row["REJPAMOUNT8"] = dREJPAMOUNTSUM8;
            row["REJPAMOUNT9"] = dREJPAMOUNTSUM9;
            row["REJPAMOUNT10"] = dREJPAMOUNTSUM10;

            row["REJPAMOUNT11"] = dREJPAMOUNTSUM11;
            row["REJPAMOUNT12"] = dREJPAMOUNTSUM12;
            row["REJPAMOUNT13"] = dREJPAMOUNTSUM13;
            row["REJPAMOUNT14"] = dREJPAMOUNTSUM14;
            row["REJPAMOUNT15"] = dREJPAMOUNTSUM15;
            row["REJPAMOUNT16"] = dREJPAMOUNTSUM16;

            row["REGPAMOUNT1"] = dREGPAMOUNTSUM1;
            row["REGPAMOUNT2"] = dREGPAMOUNTSUM2;
            row["REGPAMOUNT3"] = dREGPAMOUNTSUM3;
            row["REGPAMOUNT4"] = dREGPAMOUNTSUM4;
            row["REGPAMOUNT5"] = dREGPAMOUNTSUM5;
            row["REGPAMOUNT6"] = dREGPAMOUNTSUM6;
            row["REGPAMOUNT7"] = dREGPAMOUNTSUM7;
            row["REGPAMOUNT8"] = dREGPAMOUNTSUM8;
            row["REGPAMOUNT9"] = dREGPAMOUNTSUM9;
            row["REGPAMOUNT10"] = dREGPAMOUNTSUM10;

            row["REGPAMOUNT11"] = dREGPAMOUNTSUM11;
            row["REGPAMOUNT12"] = dREGPAMOUNTSUM12;
            row["REGPAMOUNT13"] = dREGPAMOUNTSUM13;
            row["REGPAMOUNT14"] = dREGPAMOUNTSUM14;
            row["REGPAMOUNT15"] = dREGPAMOUNTSUM15;
            row["REGPAMOUNT16"] = dREGPAMOUNTSUM16;

            row["REJPTOTAL"] = dREJPTOTALSUM;
            row["REGPTOTAL"] = dREGPTOTALSUM;
            row["RECHAIN"] = dRECHAINSUM;

            rtnDt.Rows.Add(row);

            return rtnDt;
        }
        #endregion

        #region Description : 급여 집계표 데이터셋 변경
        private DataTable UP_DtChange(DataTable dt, string Gubn)
        {
            DataTable rtnDt = new DataTable();

            if (Gubn == "1")
            {
                rtnDt.Columns.Add("TITLE", typeof(System.String));
                rtnDt.Columns.Add("AMOUNT1", typeof(System.Double));
                rtnDt.Columns.Add("AMOUNT2", typeof(System.Double));
                rtnDt.Columns.Add("AMOUNT3", typeof(System.Double));
                rtnDt.Columns.Add("AMOUNT4", typeof(System.Double));
                rtnDt.Columns.Add("AMOUNT5", typeof(System.Double));
                rtnDt.Columns.Add("AMOUNT6", typeof(System.Double));
                rtnDt.Columns.Add("AMOUNT7", typeof(System.Double));
                rtnDt.Columns.Add("AMOUNT8", typeof(System.Double));
                rtnDt.Columns.Add("AMOUNT9", typeof(System.Double));
            }
            else
            {
                rtnDt.Columns.Add("TITLE", typeof(System.String));
                rtnDt.Columns.Add("AMOUNT1", typeof(System.Double));
                rtnDt.Columns.Add("AMOUNT2", typeof(System.Double));
                rtnDt.Columns.Add("AMOUNT3", typeof(System.Double));
                rtnDt.Columns.Add("AMOUNT4", typeof(System.Double));
                rtnDt.Columns.Add("AMOUNT5", typeof(System.Double));
                rtnDt.Columns.Add("AMOUNT6", typeof(System.Double));
                rtnDt.Columns.Add("AMOUNT7", typeof(System.Double));
                rtnDt.Columns.Add("AMOUNT8", typeof(System.Double));
                rtnDt.Columns.Add("AMOUNT9", typeof(System.Double));
                rtnDt.Columns.Add("AMOUNT10", typeof(System.Double));
                rtnDt.Columns.Add("AMOUNT11", typeof(System.Double));
            }

            if (Gubn == "1")
            {
                DataRow row;

                row = rtnDt.NewRow();

                row["TITLE"] = "총지급금액";
                row["AMOUNT1"] = dt.Rows[0]["REJPTOTAL"];
                row["AMOUNT2"] = dt.Rows[1]["REJPTOTAL"];
                row["AMOUNT3"] = dt.Rows[2]["REJPTOTAL"];
                row["AMOUNT4"] = dt.Rows[3]["REJPTOTAL"];
                row["AMOUNT5"] = dt.Rows[4]["REJPTOTAL"];
                row["AMOUNT6"] = dt.Rows[5]["REJPTOTAL"];
                row["AMOUNT7"] = dt.Rows[6]["REJPTOTAL"];
                row["AMOUNT8"] = dt.Rows[7]["REJPTOTAL"];
                row["AMOUNT9"] = dt.Rows[8]["REJPTOTAL"];

                rtnDt.Rows.Add(row);

                row = rtnDt.NewRow();

                row["TITLE"] = "(공제내역)";
                row["AMOUNT1"] = 0;
                row["AMOUNT2"] = 0;
                row["AMOUNT3"] = 0;
                row["AMOUNT4"] = 0;
                row["AMOUNT5"] = 0;
                row["AMOUNT6"] = 0;
                row["AMOUNT7"] = 0;
                row["AMOUNT8"] = 0;
                row["AMOUNT9"] = 0;

                rtnDt.Rows.Add(row);

                row = rtnDt.NewRow();

                row["TITLE"] = "갑 근 세";
                row["AMOUNT1"] = dt.Rows[0]["REGPAMOUNT1"];
                row["AMOUNT2"] = dt.Rows[1]["REGPAMOUNT1"];
                row["AMOUNT3"] = dt.Rows[2]["REGPAMOUNT1"];
                row["AMOUNT4"] = dt.Rows[3]["REGPAMOUNT1"];
                row["AMOUNT5"] = dt.Rows[4]["REGPAMOUNT1"];
                row["AMOUNT6"] = dt.Rows[5]["REGPAMOUNT1"];
                row["AMOUNT7"] = dt.Rows[6]["REGPAMOUNT1"];
                row["AMOUNT8"] = dt.Rows[7]["REGPAMOUNT1"];
                row["AMOUNT9"] = dt.Rows[8]["REGPAMOUNT1"];

                rtnDt.Rows.Add(row);

                row = rtnDt.NewRow();

                row["TITLE"] = "주 민 세";
                row["AMOUNT1"] = dt.Rows[0]["REGPAMOUNT2"];
                row["AMOUNT2"] = dt.Rows[1]["REGPAMOUNT2"];
                row["AMOUNT3"] = dt.Rows[2]["REGPAMOUNT2"];
                row["AMOUNT4"] = dt.Rows[3]["REGPAMOUNT2"];
                row["AMOUNT5"] = dt.Rows[4]["REGPAMOUNT2"];
                row["AMOUNT6"] = dt.Rows[5]["REGPAMOUNT2"];
                row["AMOUNT7"] = dt.Rows[6]["REGPAMOUNT2"];
                row["AMOUNT8"] = dt.Rows[7]["REGPAMOUNT2"];
                row["AMOUNT9"] = dt.Rows[8]["REGPAMOUNT2"];

                rtnDt.Rows.Add(row);

                row = rtnDt.NewRow();

                row["TITLE"] = "건강보험료";
                row["AMOUNT1"] = dt.Rows[0]["REGPAMOUNT5"];
                row["AMOUNT2"] = dt.Rows[1]["REGPAMOUNT5"];
                row["AMOUNT3"] = dt.Rows[2]["REGPAMOUNT5"];
                row["AMOUNT4"] = dt.Rows[3]["REGPAMOUNT5"];
                row["AMOUNT5"] = dt.Rows[4]["REGPAMOUNT5"];
                row["AMOUNT6"] = dt.Rows[5]["REGPAMOUNT5"];
                row["AMOUNT7"] = dt.Rows[6]["REGPAMOUNT5"];
                row["AMOUNT8"] = dt.Rows[7]["REGPAMOUNT5"];
                row["AMOUNT9"] = dt.Rows[8]["REGPAMOUNT5"];

                rtnDt.Rows.Add(row);

                row = rtnDt.NewRow();

                row["TITLE"] = "국민 연금";
                row["AMOUNT1"] = dt.Rows[0]["REGPAMOUNT8"];
                row["AMOUNT2"] = dt.Rows[1]["REGPAMOUNT8"];
                row["AMOUNT3"] = dt.Rows[2]["REGPAMOUNT8"];
                row["AMOUNT4"] = dt.Rows[3]["REGPAMOUNT8"];
                row["AMOUNT5"] = dt.Rows[4]["REGPAMOUNT8"];
                row["AMOUNT6"] = dt.Rows[5]["REGPAMOUNT8"];
                row["AMOUNT7"] = dt.Rows[6]["REGPAMOUNT8"];
                row["AMOUNT8"] = dt.Rows[7]["REGPAMOUNT8"];
                row["AMOUNT9"] = dt.Rows[8]["REGPAMOUNT8"];

                rtnDt.Rows.Add(row);

                row = rtnDt.NewRow();

                row["TITLE"] = "상조 회비";
                row["AMOUNT1"] = dt.Rows[0]["REGPAMOUNT9"];
                row["AMOUNT2"] = dt.Rows[1]["REGPAMOUNT9"];
                row["AMOUNT3"] = dt.Rows[2]["REGPAMOUNT9"];
                row["AMOUNT4"] = dt.Rows[3]["REGPAMOUNT9"];
                row["AMOUNT5"] = dt.Rows[4]["REGPAMOUNT9"];
                row["AMOUNT6"] = dt.Rows[5]["REGPAMOUNT9"];
                row["AMOUNT7"] = dt.Rows[6]["REGPAMOUNT9"];
                row["AMOUNT8"] = dt.Rows[7]["REGPAMOUNT9"];
                row["AMOUNT9"] = dt.Rows[8]["REGPAMOUNT9"];

                rtnDt.Rows.Add(row);

                row = rtnDt.NewRow();

                row["TITLE"] = "고용 보험";
                row["AMOUNT1"] = dt.Rows[0]["REGPAMOUNT7"];
                row["AMOUNT2"] = dt.Rows[1]["REGPAMOUNT7"];
                row["AMOUNT3"] = dt.Rows[2]["REGPAMOUNT7"];
                row["AMOUNT4"] = dt.Rows[3]["REGPAMOUNT7"];
                row["AMOUNT5"] = dt.Rows[4]["REGPAMOUNT7"];
                row["AMOUNT6"] = dt.Rows[5]["REGPAMOUNT7"];
                row["AMOUNT7"] = dt.Rows[6]["REGPAMOUNT7"];
                row["AMOUNT8"] = dt.Rows[7]["REGPAMOUNT7"];
                row["AMOUNT9"] = dt.Rows[8]["REGPAMOUNT7"];

                rtnDt.Rows.Add(row);

                row = rtnDt.NewRow();

                row["TITLE"] = "정산갑근세";
                row["AMOUNT1"] = dt.Rows[0]["REGPAMOUNT3"];
                row["AMOUNT2"] = dt.Rows[1]["REGPAMOUNT3"];
                row["AMOUNT3"] = dt.Rows[2]["REGPAMOUNT3"];
                row["AMOUNT4"] = dt.Rows[3]["REGPAMOUNT3"];
                row["AMOUNT5"] = dt.Rows[4]["REGPAMOUNT3"];
                row["AMOUNT6"] = dt.Rows[5]["REGPAMOUNT3"];
                row["AMOUNT7"] = dt.Rows[6]["REGPAMOUNT3"];
                row["AMOUNT8"] = dt.Rows[7]["REGPAMOUNT3"];
                row["AMOUNT9"] = dt.Rows[8]["REGPAMOUNT3"];

                rtnDt.Rows.Add(row);

                row = rtnDt.NewRow();

                row["TITLE"] = "정산주민세";
                row["AMOUNT1"] = dt.Rows[0]["REGPAMOUNT4"];
                row["AMOUNT2"] = dt.Rows[1]["REGPAMOUNT4"];
                row["AMOUNT3"] = dt.Rows[2]["REGPAMOUNT4"];
                row["AMOUNT4"] = dt.Rows[3]["REGPAMOUNT4"];
                row["AMOUNT5"] = dt.Rows[4]["REGPAMOUNT4"];
                row["AMOUNT6"] = dt.Rows[5]["REGPAMOUNT4"];
                row["AMOUNT7"] = dt.Rows[6]["REGPAMOUNT4"];
                row["AMOUNT8"] = dt.Rows[7]["REGPAMOUNT4"];
                row["AMOUNT9"] = dt.Rows[8]["REGPAMOUNT4"];

                rtnDt.Rows.Add(row);

                row = rtnDt.NewRow();

                row["TITLE"] = "농 특 세";
                row["AMOUNT1"] = dt.Rows[0]["REGPAMOUNT11"];
                row["AMOUNT2"] = dt.Rows[1]["REGPAMOUNT11"];
                row["AMOUNT3"] = dt.Rows[2]["REGPAMOUNT11"];
                row["AMOUNT4"] = dt.Rows[3]["REGPAMOUNT11"];
                row["AMOUNT5"] = dt.Rows[4]["REGPAMOUNT11"];
                row["AMOUNT6"] = dt.Rows[5]["REGPAMOUNT11"];
                row["AMOUNT7"] = dt.Rows[6]["REGPAMOUNT11"];
                row["AMOUNT8"] = dt.Rows[7]["REGPAMOUNT11"];
                row["AMOUNT9"] = dt.Rows[8]["REGPAMOUNT11"];

                rtnDt.Rows.Add(row);

                row = rtnDt.NewRow();

                row["TITLE"] = "노조회비";
                row["AMOUNT1"] = dt.Rows[0]["REGPAMOUNT10"];
                row["AMOUNT2"] = dt.Rows[1]["REGPAMOUNT10"];
                row["AMOUNT3"] = dt.Rows[2]["REGPAMOUNT10"];
                row["AMOUNT4"] = dt.Rows[3]["REGPAMOUNT10"];
                row["AMOUNT5"] = dt.Rows[4]["REGPAMOUNT10"];
                row["AMOUNT6"] = dt.Rows[5]["REGPAMOUNT10"];
                row["AMOUNT7"] = dt.Rows[6]["REGPAMOUNT10"];
                row["AMOUNT8"] = dt.Rows[7]["REGPAMOUNT10"];
                row["AMOUNT9"] = dt.Rows[8]["REGPAMOUNT10"];

                rtnDt.Rows.Add(row);

                row = rtnDt.NewRow();

                row["TITLE"] = "기타공제";
                row["AMOUNT1"] = dt.Rows[0]["REGPAMOUNT12"];
                row["AMOUNT2"] = dt.Rows[1]["REGPAMOUNT12"];
                row["AMOUNT3"] = dt.Rows[2]["REGPAMOUNT12"];
                row["AMOUNT4"] = dt.Rows[3]["REGPAMOUNT12"];
                row["AMOUNT5"] = dt.Rows[4]["REGPAMOUNT12"];
                row["AMOUNT6"] = dt.Rows[5]["REGPAMOUNT12"];
                row["AMOUNT7"] = dt.Rows[6]["REGPAMOUNT12"];
                row["AMOUNT8"] = dt.Rows[7]["REGPAMOUNT12"];
                row["AMOUNT9"] = dt.Rows[8]["REGPAMOUNT12"];

                rtnDt.Rows.Add(row);

                row = rtnDt.NewRow();

                row["TITLE"] = "공제액 계";
                row["AMOUNT1"] = dt.Rows[0]["REGPTOTAL"];
                row["AMOUNT2"] = dt.Rows[1]["REGPTOTAL"];
                row["AMOUNT3"] = dt.Rows[2]["REGPTOTAL"];
                row["AMOUNT4"] = dt.Rows[3]["REGPTOTAL"];
                row["AMOUNT5"] = dt.Rows[4]["REGPTOTAL"];
                row["AMOUNT6"] = dt.Rows[5]["REGPTOTAL"];
                row["AMOUNT7"] = dt.Rows[6]["REGPTOTAL"];
                row["AMOUNT8"] = dt.Rows[7]["REGPTOTAL"];
                row["AMOUNT9"] = dt.Rows[8]["REGPTOTAL"];

                rtnDt.Rows.Add(row);

                row = rtnDt.NewRow();

                row["TITLE"] = "차인지급액";
                row["AMOUNT1"] = dt.Rows[0]["RECHAIN"];
                row["AMOUNT2"] = dt.Rows[1]["RECHAIN"];
                row["AMOUNT3"] = dt.Rows[2]["RECHAIN"];
                row["AMOUNT4"] = dt.Rows[3]["RECHAIN"];
                row["AMOUNT5"] = dt.Rows[4]["RECHAIN"];
                row["AMOUNT6"] = dt.Rows[5]["RECHAIN"];
                row["AMOUNT7"] = dt.Rows[6]["RECHAIN"];
                row["AMOUNT8"] = dt.Rows[7]["RECHAIN"];
                row["AMOUNT9"] = dt.Rows[8]["RECHAIN"];

                rtnDt.Rows.Add(row);

                row = rtnDt.NewRow();

                row["TITLE"] = "인    원";
                row["AMOUNT1"] = dt.Rows[0]["COUNT"];
                row["AMOUNT2"] = dt.Rows[1]["COUNT"];
                row["AMOUNT3"] = dt.Rows[2]["COUNT"];
                row["AMOUNT4"] = dt.Rows[3]["COUNT"];
                row["AMOUNT5"] = dt.Rows[4]["COUNT"];
                row["AMOUNT6"] = dt.Rows[5]["COUNT"];
                row["AMOUNT7"] = dt.Rows[6]["COUNT"];
                row["AMOUNT8"] = dt.Rows[7]["COUNT"];
                row["AMOUNT9"] = dt.Rows[8]["COUNT"];

                rtnDt.Rows.Add(row);
            }
            else
            {
                DataRow row;

                row = rtnDt.NewRow();

                row["TITLE"] = "총지급금액";
                row["AMOUNT1"] = dt.Rows[0]["REJPTOTAL"];
                row["AMOUNT2"] = dt.Rows[1]["REJPTOTAL"];
                row["AMOUNT3"] = dt.Rows[2]["REJPTOTAL"];
                row["AMOUNT4"] = dt.Rows[3]["REJPTOTAL"];
                row["AMOUNT5"] = dt.Rows[4]["REJPTOTAL"];
                row["AMOUNT6"] = dt.Rows[5]["REJPTOTAL"];
                row["AMOUNT7"] = dt.Rows[6]["REJPTOTAL"];
                row["AMOUNT8"] = dt.Rows[7]["REJPTOTAL"];
                row["AMOUNT9"] = dt.Rows[8]["REJPTOTAL"];
                row["AMOUNT10"] = dt.Rows[9]["REJPTOTAL"];
                row["AMOUNT11"] = dt.Rows[10]["REJPTOTAL"];

                rtnDt.Rows.Add(row);

                row = rtnDt.NewRow();

                row["TITLE"] = "(공제내역)";
                row["AMOUNT1"] = 0;
                row["AMOUNT2"] = 0;
                row["AMOUNT3"] = 0;
                row["AMOUNT4"] = 0;
                row["AMOUNT5"] = 0;
                row["AMOUNT6"] = 0;
                row["AMOUNT7"] = 0;
                row["AMOUNT8"] = 0;
                row["AMOUNT9"] = 0;
                row["AMOUNT10"] = 0;
                row["AMOUNT11"] = 0;

                rtnDt.Rows.Add(row);

                row = rtnDt.NewRow();

                row["TITLE"] = "갑 근 세";
                row["AMOUNT1"] = dt.Rows[0]["REGPAMOUNT1"];
                row["AMOUNT2"] = dt.Rows[1]["REGPAMOUNT1"];
                row["AMOUNT3"] = dt.Rows[2]["REGPAMOUNT1"];
                row["AMOUNT4"] = dt.Rows[3]["REGPAMOUNT1"];
                row["AMOUNT5"] = dt.Rows[4]["REGPAMOUNT1"];
                row["AMOUNT6"] = dt.Rows[5]["REGPAMOUNT1"];
                row["AMOUNT7"] = dt.Rows[6]["REGPAMOUNT1"];
                row["AMOUNT8"] = dt.Rows[7]["REGPAMOUNT1"];
                row["AMOUNT9"] = dt.Rows[8]["REGPAMOUNT1"];
                row["AMOUNT10"] = dt.Rows[9]["REGPAMOUNT1"];
                row["AMOUNT11"] = dt.Rows[10]["REGPAMOUNT1"];

                rtnDt.Rows.Add(row);

                row = rtnDt.NewRow();

                row["TITLE"] = "주 민 세";
                row["AMOUNT1"] = dt.Rows[0]["REGPAMOUNT2"];
                row["AMOUNT2"] = dt.Rows[1]["REGPAMOUNT2"];
                row["AMOUNT3"] = dt.Rows[2]["REGPAMOUNT2"];
                row["AMOUNT4"] = dt.Rows[3]["REGPAMOUNT2"];
                row["AMOUNT5"] = dt.Rows[4]["REGPAMOUNT2"];
                row["AMOUNT6"] = dt.Rows[5]["REGPAMOUNT2"];
                row["AMOUNT7"] = dt.Rows[6]["REGPAMOUNT2"];
                row["AMOUNT8"] = dt.Rows[7]["REGPAMOUNT2"];
                row["AMOUNT9"] = dt.Rows[8]["REGPAMOUNT2"];
                row["AMOUNT10"] = dt.Rows[9]["REGPAMOUNT2"];
                row["AMOUNT11"] = dt.Rows[10]["REGPAMOUNT2"];

                rtnDt.Rows.Add(row);

                row = rtnDt.NewRow();

                row["TITLE"] = "건강보험료";
                row["AMOUNT1"] = dt.Rows[0]["REGPAMOUNT5"];
                row["AMOUNT2"] = dt.Rows[1]["REGPAMOUNT5"];
                row["AMOUNT3"] = dt.Rows[2]["REGPAMOUNT5"];
                row["AMOUNT4"] = dt.Rows[3]["REGPAMOUNT5"];
                row["AMOUNT5"] = dt.Rows[4]["REGPAMOUNT5"];
                row["AMOUNT6"] = dt.Rows[5]["REGPAMOUNT5"];
                row["AMOUNT7"] = dt.Rows[6]["REGPAMOUNT5"];
                row["AMOUNT8"] = dt.Rows[7]["REGPAMOUNT5"];
                row["AMOUNT9"] = dt.Rows[8]["REGPAMOUNT5"];
                row["AMOUNT10"] = dt.Rows[9]["REGPAMOUNT5"];
                row["AMOUNT11"] = dt.Rows[10]["REGPAMOUNT5"];

                rtnDt.Rows.Add(row);

                row = rtnDt.NewRow();

                row["TITLE"] = "국민 연금";
                row["AMOUNT1"] = dt.Rows[0]["REGPAMOUNT8"];
                row["AMOUNT2"] = dt.Rows[1]["REGPAMOUNT8"];
                row["AMOUNT3"] = dt.Rows[2]["REGPAMOUNT8"];
                row["AMOUNT4"] = dt.Rows[3]["REGPAMOUNT8"];
                row["AMOUNT5"] = dt.Rows[4]["REGPAMOUNT8"];
                row["AMOUNT6"] = dt.Rows[5]["REGPAMOUNT8"];
                row["AMOUNT7"] = dt.Rows[6]["REGPAMOUNT8"];
                row["AMOUNT8"] = dt.Rows[7]["REGPAMOUNT8"];
                row["AMOUNT9"] = dt.Rows[8]["REGPAMOUNT8"];
                row["AMOUNT10"] = dt.Rows[9]["REGPAMOUNT8"];
                row["AMOUNT11"] = dt.Rows[10]["REGPAMOUNT8"];

                rtnDt.Rows.Add(row);

                row = rtnDt.NewRow();

                row["TITLE"] = "상조 회비";
                row["AMOUNT1"] = dt.Rows[0]["REGPAMOUNT9"];
                row["AMOUNT2"] = dt.Rows[1]["REGPAMOUNT9"];
                row["AMOUNT3"] = dt.Rows[2]["REGPAMOUNT9"];
                row["AMOUNT4"] = dt.Rows[3]["REGPAMOUNT9"];
                row["AMOUNT5"] = dt.Rows[4]["REGPAMOUNT9"];
                row["AMOUNT6"] = dt.Rows[5]["REGPAMOUNT9"];
                row["AMOUNT7"] = dt.Rows[6]["REGPAMOUNT9"];
                row["AMOUNT8"] = dt.Rows[7]["REGPAMOUNT9"];
                row["AMOUNT9"] = dt.Rows[8]["REGPAMOUNT9"];
                row["AMOUNT10"] = dt.Rows[9]["REGPAMOUNT9"];
                row["AMOUNT11"] = dt.Rows[10]["REGPAMOUNT9"];

                rtnDt.Rows.Add(row);

                row = rtnDt.NewRow();

                row["TITLE"] = "고용 보험";
                row["AMOUNT1"] = dt.Rows[0]["REGPAMOUNT7"];
                row["AMOUNT2"] = dt.Rows[1]["REGPAMOUNT7"];
                row["AMOUNT3"] = dt.Rows[2]["REGPAMOUNT7"];
                row["AMOUNT4"] = dt.Rows[3]["REGPAMOUNT7"];
                row["AMOUNT5"] = dt.Rows[4]["REGPAMOUNT7"];
                row["AMOUNT6"] = dt.Rows[5]["REGPAMOUNT7"];
                row["AMOUNT7"] = dt.Rows[6]["REGPAMOUNT7"];
                row["AMOUNT8"] = dt.Rows[7]["REGPAMOUNT7"];
                row["AMOUNT9"] = dt.Rows[8]["REGPAMOUNT7"];
                row["AMOUNT10"] = dt.Rows[9]["REGPAMOUNT7"];
                row["AMOUNT11"] = dt.Rows[10]["REGPAMOUNT7"];

                rtnDt.Rows.Add(row);

                row = rtnDt.NewRow();

                row["TITLE"] = "정산갑근세";
                row["AMOUNT1"] = dt.Rows[0]["REGPAMOUNT3"];
                row["AMOUNT2"] = dt.Rows[1]["REGPAMOUNT3"];
                row["AMOUNT3"] = dt.Rows[2]["REGPAMOUNT3"];
                row["AMOUNT4"] = dt.Rows[3]["REGPAMOUNT3"];
                row["AMOUNT5"] = dt.Rows[4]["REGPAMOUNT3"];
                row["AMOUNT6"] = dt.Rows[5]["REGPAMOUNT3"];
                row["AMOUNT7"] = dt.Rows[6]["REGPAMOUNT3"];
                row["AMOUNT8"] = dt.Rows[7]["REGPAMOUNT3"];
                row["AMOUNT9"] = dt.Rows[8]["REGPAMOUNT3"];
                row["AMOUNT10"] = dt.Rows[9]["REGPAMOUNT3"];
                row["AMOUNT11"] = dt.Rows[10]["REGPAMOUNT3"];

                rtnDt.Rows.Add(row);

                row = rtnDt.NewRow();

                row["TITLE"] = "정산주민세";
                row["AMOUNT1"] = dt.Rows[0]["REGPAMOUNT4"];
                row["AMOUNT2"] = dt.Rows[1]["REGPAMOUNT4"];
                row["AMOUNT3"] = dt.Rows[2]["REGPAMOUNT4"];
                row["AMOUNT4"] = dt.Rows[3]["REGPAMOUNT4"];
                row["AMOUNT5"] = dt.Rows[4]["REGPAMOUNT4"];
                row["AMOUNT6"] = dt.Rows[5]["REGPAMOUNT4"];
                row["AMOUNT7"] = dt.Rows[6]["REGPAMOUNT4"];
                row["AMOUNT8"] = dt.Rows[7]["REGPAMOUNT4"];
                row["AMOUNT9"] = dt.Rows[8]["REGPAMOUNT4"];
                row["AMOUNT10"] = dt.Rows[9]["REGPAMOUNT4"];
                row["AMOUNT11"] = dt.Rows[10]["REGPAMOUNT4"];

                rtnDt.Rows.Add(row);

                row = rtnDt.NewRow();

                row["TITLE"] = "농 특 세";
                row["AMOUNT1"] = dt.Rows[0]["REGPAMOUNT11"];
                row["AMOUNT2"] = dt.Rows[1]["REGPAMOUNT11"];
                row["AMOUNT3"] = dt.Rows[2]["REGPAMOUNT11"];
                row["AMOUNT4"] = dt.Rows[3]["REGPAMOUNT11"];
                row["AMOUNT5"] = dt.Rows[4]["REGPAMOUNT11"];
                row["AMOUNT6"] = dt.Rows[5]["REGPAMOUNT11"];
                row["AMOUNT7"] = dt.Rows[6]["REGPAMOUNT11"];
                row["AMOUNT8"] = dt.Rows[7]["REGPAMOUNT11"];
                row["AMOUNT9"] = dt.Rows[8]["REGPAMOUNT11"];
                row["AMOUNT10"] = dt.Rows[9]["REGPAMOUNT11"];
                row["AMOUNT11"] = dt.Rows[10]["REGPAMOUNT11"];

                rtnDt.Rows.Add(row);

                row = rtnDt.NewRow();

                row["TITLE"] = "노조회비";
                row["AMOUNT1"] = dt.Rows[0]["REGPAMOUNT10"];
                row["AMOUNT2"] = dt.Rows[1]["REGPAMOUNT10"];
                row["AMOUNT3"] = dt.Rows[2]["REGPAMOUNT10"];
                row["AMOUNT4"] = dt.Rows[3]["REGPAMOUNT10"];
                row["AMOUNT5"] = dt.Rows[4]["REGPAMOUNT10"];
                row["AMOUNT6"] = dt.Rows[5]["REGPAMOUNT10"];
                row["AMOUNT7"] = dt.Rows[6]["REGPAMOUNT10"];
                row["AMOUNT8"] = dt.Rows[7]["REGPAMOUNT10"];
                row["AMOUNT9"] = dt.Rows[8]["REGPAMOUNT10"];
                row["AMOUNT10"] = dt.Rows[9]["REGPAMOUNT10"];
                row["AMOUNT11"] = dt.Rows[10]["REGPAMOUNT10"];

                rtnDt.Rows.Add(row);

                row = rtnDt.NewRow();

                row["TITLE"] = "기타공제";
                row["AMOUNT1"] = dt.Rows[0]["REGPAMOUNT12"];
                row["AMOUNT2"] = dt.Rows[1]["REGPAMOUNT12"];
                row["AMOUNT3"] = dt.Rows[2]["REGPAMOUNT12"];
                row["AMOUNT4"] = dt.Rows[3]["REGPAMOUNT12"];
                row["AMOUNT5"] = dt.Rows[4]["REGPAMOUNT12"];
                row["AMOUNT6"] = dt.Rows[5]["REGPAMOUNT12"];
                row["AMOUNT7"] = dt.Rows[6]["REGPAMOUNT12"];
                row["AMOUNT8"] = dt.Rows[7]["REGPAMOUNT12"];
                row["AMOUNT9"] = dt.Rows[8]["REGPAMOUNT12"];
                row["AMOUNT10"] = dt.Rows[9]["REGPAMOUNT12"];
                row["AMOUNT11"] = dt.Rows[10]["REGPAMOUNT12"];

                rtnDt.Rows.Add(row);

                row = rtnDt.NewRow();

                row["TITLE"] = "공제액 계";
                row["AMOUNT1"] = dt.Rows[0]["REGPTOTAL"];
                row["AMOUNT2"] = dt.Rows[1]["REGPTOTAL"];
                row["AMOUNT3"] = dt.Rows[2]["REGPTOTAL"];
                row["AMOUNT4"] = dt.Rows[3]["REGPTOTAL"];
                row["AMOUNT5"] = dt.Rows[4]["REGPTOTAL"];
                row["AMOUNT6"] = dt.Rows[5]["REGPTOTAL"];
                row["AMOUNT7"] = dt.Rows[6]["REGPTOTAL"];
                row["AMOUNT8"] = dt.Rows[7]["REGPTOTAL"];
                row["AMOUNT9"] = dt.Rows[8]["REGPTOTAL"];
                row["AMOUNT10"] = dt.Rows[9]["REGPTOTAL"];
                row["AMOUNT11"] = dt.Rows[10]["REGPTOTAL"];

                rtnDt.Rows.Add(row);

                row = rtnDt.NewRow();

                row["TITLE"] = "차인지급액";
                row["AMOUNT1"] = dt.Rows[0]["RECHAIN"];
                row["AMOUNT2"] = dt.Rows[1]["RECHAIN"];
                row["AMOUNT3"] = dt.Rows[2]["RECHAIN"];
                row["AMOUNT4"] = dt.Rows[3]["RECHAIN"];
                row["AMOUNT5"] = dt.Rows[4]["RECHAIN"];
                row["AMOUNT6"] = dt.Rows[5]["RECHAIN"];
                row["AMOUNT7"] = dt.Rows[6]["RECHAIN"];
                row["AMOUNT8"] = dt.Rows[7]["RECHAIN"];
                row["AMOUNT9"] = dt.Rows[8]["RECHAIN"];
                row["AMOUNT10"] = dt.Rows[9]["RECHAIN"];
                row["AMOUNT11"] = dt.Rows[10]["RECHAIN"];

                rtnDt.Rows.Add(row);

                row = rtnDt.NewRow();

                row["TITLE"] = "인    원";
                row["AMOUNT1"] = dt.Rows[0]["COUNT"];
                row["AMOUNT2"] = dt.Rows[1]["COUNT"];
                row["AMOUNT3"] = dt.Rows[2]["COUNT"];
                row["AMOUNT4"] = dt.Rows[3]["COUNT"];
                row["AMOUNT5"] = dt.Rows[4]["COUNT"];
                row["AMOUNT6"] = dt.Rows[5]["COUNT"];
                row["AMOUNT7"] = dt.Rows[6]["COUNT"];
                row["AMOUNT8"] = dt.Rows[7]["COUNT"];
                row["AMOUNT9"] = dt.Rows[8]["COUNT"];
                row["AMOUNT10"] = dt.Rows[9]["COUNT"];
                row["AMOUNT11"] = dt.Rows[10]["COUNT"];

                rtnDt.Rows.Add(row);
            }

            return rtnDt;
        }
        #endregion
    }
}
