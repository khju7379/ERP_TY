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
    /// SOUNDING 대장 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.03.28 18:47
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_73SIP124 : SOUNDING 대장 조회
    ///  TY_P_UT_73SIV126 : SERVER 파일 조회
    ///  TY_P_UT_73SIV127 : SOUNDING 파일 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_73SIS125 : SOUNDING 대장 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  SNDATE : 일자
    ///  SNTANKNO :  TANK 번호
    /// </summary>
    public partial class TYUTPR005P : TYBase
    {
        #region Descriptin : 폼 로드
        public TYUTPR005P()
        {
            InitializeComponent();
        }

        private void TYUTPR005P_Load(object sender, System.EventArgs e)
        {
            this.DTP01_SNDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_SNDATE);
        }
        #endregion

        #region Descriptin : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            double dSNMTQTY = 0;

            this.FPS91_TY_S_UT_73SIS125.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_B4K92207", this.DTP01_SNDATE.GetString(),
                                                        this.TXT01_SNTANKNO.GetValue().ToString().Trim(),
                                                        this.CBO01_CHMEMGUBN.GetValue().ToString()
                                                        );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_73SIS125.SetValue(QueryDataSetReport(dt));



            for (int i = 0; i < FPS91_TY_S_UT_73SIS125.CurrentRowCount; i++)
            {
                dSNMTQTY = dSNMTQTY + double.Parse(Get_Numeric(this.FPS91_TY_S_UT_73SIS125.GetValue(i, "SNMTQTY").ToString()));
            }

            // MT량
            this.TXT01_SNMTQTY.SetValue((dSNMTQTY).ToString("0.000"));
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_B1IDN334", this.DTP01_SNDATE.GetString(),
                                                        this.TXT01_SNTANKNO.GetValue().ToString().Trim(),
                                                        this.CBO01_CHMEMGUBN.GetValue().ToString()
                                                        );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYUTPR005R();
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
            DataTable tmpDt = new DataTable();

            string sSql = string.Empty;
            string sHWAMUL = string.Empty;
            string sNEWTANKNO = string.Empty;
            string sSNDATE = this.DTP01_SNDATE.GetString();
            string sSNTANKNO = this.TXT01_SNTANKNO.GetValue().ToString().Trim();

            int iCount = 0;

            int i = 0;

            string[] sOLDTANKNO = new string[110];

            if (this.CBO01_CHMEMGUBN.GetValue().ToString() == "")
            {
                sOLDTANKNO[0] = "101";
                sOLDTANKNO[1] = "102";
                sOLDTANKNO[2] = "103";
                sOLDTANKNO[3] = "104";
                sOLDTANKNO[4] = "105";
                sOLDTANKNO[5] = "106";
                sOLDTANKNO[6] = "107";
                sOLDTANKNO[7] = "108";
                sOLDTANKNO[8] = "109";
                sOLDTANKNO[9] = "110";
                sOLDTANKNO[10] = "111";
                sOLDTANKNO[11] = "201";
                sOLDTANKNO[12] = "202";
                sOLDTANKNO[13] = "203";
                sOLDTANKNO[14] = "204";
                sOLDTANKNO[15] = "205";
                sOLDTANKNO[16] = "206";
                sOLDTANKNO[17] = "207";
                sOLDTANKNO[18] = "208";
                sOLDTANKNO[19] = "301";
                sOLDTANKNO[20] = "302";
                sOLDTANKNO[21] = "303";
                sOLDTANKNO[22] = "304";
                sOLDTANKNO[23] = "305";
                sOLDTANKNO[24] = "306";
                sOLDTANKNO[25] = "307";
                sOLDTANKNO[26] = "308";
                sOLDTANKNO[27] = "309";
                sOLDTANKNO[28] = "310";
                sOLDTANKNO[29] = "501";
                sOLDTANKNO[30] = "502";
                sOLDTANKNO[31] = "503";
                sOLDTANKNO[32] = "504";
                sOLDTANKNO[33] = "505";
                sOLDTANKNO[34] = "506";
                sOLDTANKNO[35] = "507";
                sOLDTANKNO[36] = "508";
                sOLDTANKNO[37] = "509";
                sOLDTANKNO[38] = "601";
                sOLDTANKNO[39] = "602";
                sOLDTANKNO[40] = "603";
                sOLDTANKNO[41] = "604";
                sOLDTANKNO[42] = "605";
                sOLDTANKNO[43] = "606";
                sOLDTANKNO[44] = "607";
                sOLDTANKNO[45] = "608";
                sOLDTANKNO[46] = "701";
                sOLDTANKNO[47] = "702";
                sOLDTANKNO[48] = "703";
                sOLDTANKNO[49] = "704";
                sOLDTANKNO[50] = "705";
                sOLDTANKNO[51] = "706";
                sOLDTANKNO[52] = "707";
                sOLDTANKNO[53] = "708";
                sOLDTANKNO[54] = "709";
                sOLDTANKNO[55] = "710";
                sOLDTANKNO[56] = "801";
                sOLDTANKNO[57] = "802";
                sOLDTANKNO[58] = "803";
                sOLDTANKNO[59] = "804";
                sOLDTANKNO[60] = "805";
                sOLDTANKNO[61] = "806";
                sOLDTANKNO[62] = "807";
                sOLDTANKNO[63] = "808";
                sOLDTANKNO[64] = "809";
                sOLDTANKNO[65] = "810";
                sOLDTANKNO[66] = "811";
                sOLDTANKNO[67] = "812";
                sOLDTANKNO[68] = "901";
                sOLDTANKNO[69] = "902";
                sOLDTANKNO[70] = "903";
                sOLDTANKNO[71] = "904";
                sOLDTANKNO[72] = "905";
                sOLDTANKNO[73] = "906";
                sOLDTANKNO[74] = "907";
                sOLDTANKNO[75] = "908";
                sOLDTANKNO[76] = "909";
                sOLDTANKNO[77] = "910";
                sOLDTANKNO[78] = "911";
                sOLDTANKNO[79] = "912";
                sOLDTANKNO[80] = "913";
                sOLDTANKNO[81] = "1101";
                sOLDTANKNO[82] = "1102";
                sOLDTANKNO[83] = "1103";
                sOLDTANKNO[84] = "1104";
                sOLDTANKNO[85] = "1105";
                sOLDTANKNO[86] = "1106";
                sOLDTANKNO[87] = "1107";
                sOLDTANKNO[88] = "2001";
                sOLDTANKNO[89] = "2002";
                sOLDTANKNO[90] = "2003";
                sOLDTANKNO[91] = "2004";
                sOLDTANKNO[92] = "2005";
                sOLDTANKNO[93] = "3001";
                sOLDTANKNO[94] = "3002";
                sOLDTANKNO[95] = "3003";
                sOLDTANKNO[96] = "3004";
                sOLDTANKNO[97] = "3005";
                sOLDTANKNO[98] = "3006";
                sOLDTANKNO[99] = "3007";
                sOLDTANKNO[100] = "5001";
                sOLDTANKNO[101] = "5002";
                sOLDTANKNO[102] = "5003";
                sOLDTANKNO[103] = "5004";
                sOLDTANKNO[104] = "5005";
                sOLDTANKNO[105] = "5006";
                sOLDTANKNO[106] = "6001";
                sOLDTANKNO[107] = "6002";
                sOLDTANKNO[108] = "6003";
                sOLDTANKNO[109] = "6004";
            }
            else
            {
                sOLDTANKNO[0] = "";
                sOLDTANKNO[1] = "";
                sOLDTANKNO[2] = "";
                sOLDTANKNO[3] = "";
                sOLDTANKNO[4] = "";
                sOLDTANKNO[5] = "";
                sOLDTANKNO[6] = "";
                sOLDTANKNO[7] = "";
                sOLDTANKNO[8] = "";
                sOLDTANKNO[9] = "";
                sOLDTANKNO[10] = "";
                sOLDTANKNO[11] = "";
                sOLDTANKNO[12] = "";
                sOLDTANKNO[13] = "";
                sOLDTANKNO[14] = "";
                sOLDTANKNO[15] = "";
                sOLDTANKNO[16] = "";
                sOLDTANKNO[17] = "";
                sOLDTANKNO[18] = "";
                sOLDTANKNO[19] = "";
                sOLDTANKNO[20] = "";
                sOLDTANKNO[21] = "";
                sOLDTANKNO[22] = "";
                sOLDTANKNO[23] = "";
                sOLDTANKNO[24] = "";
                sOLDTANKNO[25] = "";
                sOLDTANKNO[26] = "";
                sOLDTANKNO[27] = "";
                sOLDTANKNO[28] = "";
                sOLDTANKNO[29] = "";
                sOLDTANKNO[30] = "";
                sOLDTANKNO[31] = "";
                sOLDTANKNO[32] = "";
                sOLDTANKNO[33] = "";
                sOLDTANKNO[34] = "";
                sOLDTANKNO[35] = "";
                sOLDTANKNO[36] = "";
                sOLDTANKNO[37] = "";
                sOLDTANKNO[38] = "";
                sOLDTANKNO[39] = "";
                sOLDTANKNO[40] = "";
                sOLDTANKNO[41] = "";
                sOLDTANKNO[42] = "";
                sOLDTANKNO[43] = "";
                sOLDTANKNO[44] = "";
                sOLDTANKNO[45] = "";
                sOLDTANKNO[46] = "";
                sOLDTANKNO[47] = "";
                sOLDTANKNO[48] = "";
                sOLDTANKNO[49] = "";
                sOLDTANKNO[50] = "";
                sOLDTANKNO[51] = "";
                sOLDTANKNO[52] = "";
                sOLDTANKNO[53] = "";
                sOLDTANKNO[54] = "";
                sOLDTANKNO[55] = "";
                sOLDTANKNO[56] = "";
                sOLDTANKNO[57] = "";
                sOLDTANKNO[58] = "";
                sOLDTANKNO[59] = "";
                sOLDTANKNO[60] = "";
                sOLDTANKNO[61] = "";
                sOLDTANKNO[62] = "";
                sOLDTANKNO[63] = "";
                sOLDTANKNO[64] = "";
                sOLDTANKNO[65] = "";
                sOLDTANKNO[66] = "";
                sOLDTANKNO[67] = "";
                sOLDTANKNO[68] = "";
                sOLDTANKNO[69] = "";
                sOLDTANKNO[70] = "";
                sOLDTANKNO[71] = "";
                sOLDTANKNO[72] = "";
                sOLDTANKNO[73] = "";
                sOLDTANKNO[74] = "";
                sOLDTANKNO[75] = "";
                sOLDTANKNO[76] = "";
                sOLDTANKNO[77] = "";
                sOLDTANKNO[78] = "";
                sOLDTANKNO[79] = "";
                sOLDTANKNO[80] = "";
                sOLDTANKNO[81] = "";
                sOLDTANKNO[82] = "";
                sOLDTANKNO[83] = "";
                sOLDTANKNO[84] = "";
                sOLDTANKNO[85] = "";
                sOLDTANKNO[86] = "";
                sOLDTANKNO[87] = "";
                sOLDTANKNO[88] = "";
                sOLDTANKNO[89] = "";
                sOLDTANKNO[90] = "";
                sOLDTANKNO[91] = "";
                sOLDTANKNO[92] = "";
                sOLDTANKNO[93] = "";
                sOLDTANKNO[94] = "";
                sOLDTANKNO[95] = "";
                sOLDTANKNO[96] = "";
                sOLDTANKNO[97] = "";
                sOLDTANKNO[98] = "";
                sOLDTANKNO[99] = "";
                sOLDTANKNO[100] = "";
                sOLDTANKNO[101] = "";
                sOLDTANKNO[102] = "";
                sOLDTANKNO[103] = "";
                sOLDTANKNO[104] = "";
                sOLDTANKNO[105] = "";
                sOLDTANKNO[106] = "";
                sOLDTANKNO[107] = "";
                sOLDTANKNO[108] = "";
                sOLDTANKNO[109] = "";

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    sOLDTANKNO[i] = dt.Rows[i]["SNTANKNO"].ToString().Trim();
                }
            }

            DataTable retDt = new DataTable();

            retDt.Columns.Add("SNTANKNO", typeof(System.String));
            retDt.Columns.Add("SNDATE", typeof(System.String));
            retDt.Columns.Add("SNHWAMUL", typeof(System.String));
            retDt.Columns.Add("HMDESC1", typeof(System.String));
            retDt.Columns.Add("SNHIGH", typeof(System.String));
            retDt.Columns.Add("SNTEMP", typeof(System.String));
            retDt.Columns.Add("SNGKLQTY", typeof(System.String));
            retDt.Columns.Add("SNNKLQTY", typeof(System.String));
            retDt.Columns.Add("SNMTQTY", typeof(System.String));
            retDt.Columns.Add("SNNOTE", typeof(System.String));
            retDt.Columns.Add("BALANCE", typeof(System.String));
            retDt.Columns.Add("DTJUNG", typeof(System.String));
            retDt.Columns.Add("DTJEQTY", typeof(System.String));
            retDt.Columns.Add("DRJEMT", typeof(System.String));
            retDt.Columns.Add("VNSANGHO", typeof(System.String));
            retDt.Columns.Add("TNFIRECAPA", typeof(System.String));

            for (i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    iCount = i;
                }

                sNEWTANKNO = dt.Rows[i]["SNTANKNO"].ToString().Trim();

                if (sSNTANKNO == "")
                {
                    if (sOLDTANKNO[iCount].ToString() != "")
                    {
                        if (sNEWTANKNO.ToString() != sOLDTANKNO[iCount].ToString())
                        {
                            DataRow row1 = retDt.NewRow();

                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_UT_73SIV127", sSNDATE,
                                                                        sOLDTANKNO[iCount].ToString()
                                                                        );

                            tmpDt = this.DbConnector.ExecuteDataTable();

                            if (tmpDt.Rows.Count > 0)
                            {
                                if ((this.CBO01_GGUBUN.GetValue().ToString() == "") ||
                                   ((this.CBO01_GGUBUN.GetValue().ToString() != "") && (this.CBO01_GGUBUN.GetValue().ToString() == tmpDt.Rows[0]["TNLOCATE"].ToString())))
                                {
                                    row1["SNTANKNO"] = tmpDt.Rows[0]["SNTANKNO"].ToString();
                                    row1["SNDATE"] = tmpDt.Rows[0]["SNDATE"].ToString();
                                    row1["SNHWAMUL"] = tmpDt.Rows[0]["SNHWAMUL"].ToString();
                                    sHWAMUL = tmpDt.Rows[0]["SNHWAMUL"].ToString();
                                    row1["HMDESC1"] = tmpDt.Rows[0]["HMDESC1"].ToString();
                                    row1["SNHIGH"] = "0";
                                    row1["SNTEMP"] = "0";
                                    row1["SNGKLQTY"] = "0";
                                    row1["SNNKLQTY"] = "0";
                                    row1["SNMTQTY"] = "0";
                                    row1["SNNOTE"] = "0";
                                    row1["BALANCE"] = "0";
                                    row1["DTJEQTY"] = "0";
                                    row1["TNFIRECAPA"] = "0";

                                    this.DbConnector.CommandClear();
                                    this.DbConnector.Attach("TY_P_UT_73SIV126", sSNDATE,
                                                                                sOLDTANKNO[iCount].ToString(),
                                                                                sHWAMUL.ToString()
                                                                                );

                                    tmpDt = this.DbConnector.ExecuteDataTable();

                                    if (tmpDt.Rows.Count > 0)
                                    {
                                        row1["VNSANGHO"] = tmpDt.Rows[0]["VNSANGHO"].ToString();
                                    }
                                    else
                                    {
                                        row1["VNSANGHO"] = "";
                                    }
                                }
                            }
                            else
                            {

                            }
                            retDt.Rows.Add(row1);

                            i = i - 1;
                        }
                        else
                        {
                            DataRow row = retDt.NewRow();

                            if ((this.CBO01_GGUBUN.GetValue().ToString() == "") ||
                                   ((this.CBO01_GGUBUN.GetValue().ToString() != "") && (this.CBO01_GGUBUN.GetValue().ToString() == dt.Rows[i]["TNLOCATE"].ToString())))
                            {
                                row["SNTANKNO"] = dt.Rows[i]["SNTANKNO"].ToString();
                                row["SNDATE"] = dt.Rows[i]["SNDATE"].ToString();
                                row["SNHWAMUL"] = dt.Rows[i]["SNHWAMUL"].ToString();
                                sHWAMUL = dt.Rows[i]["SNHWAMUL"].ToString();
                                row["HMDESC1"] = dt.Rows[i]["HMDESC1"].ToString();
                                row["SNHIGH"] = double.Parse(dt.Rows[i]["SNHIGH"].ToString());
                                row["SNTEMP"] = double.Parse(dt.Rows[i]["SNTEMP"].ToString());
                                row["SNGKLQTY"] = double.Parse(dt.Rows[i]["SNGKLQTY"].ToString());
                                row["SNNKLQTY"] = double.Parse(dt.Rows[i]["SNNKLQTY"].ToString());
                                row["SNMTQTY"] = double.Parse(dt.Rows[i]["SNMTQTY"].ToString());
                                row["SNNOTE"] = double.Parse(dt.Rows[i]["SNNOTE"].ToString());
                                row["BALANCE"] = double.Parse(dt.Rows[i]["BALANCE"].ToString());
                                row["DTJEQTY"] = double.Parse(dt.Rows[i]["DTJEQTY"].ToString());
                                row["TNFIRECAPA"] = double.Parse(dt.Rows[i]["TNFIRECAPA"].ToString());

                                this.DbConnector.CommandClear();
                                this.DbConnector.Attach("TY_P_UT_73SIV126", sSNDATE,
                                                                            dt.Rows[i]["SNTANKNO"].ToString().Trim(),
                                                                            sHWAMUL
                                                                            );

                                tmpDt = this.DbConnector.ExecuteDataTable();

                                if (tmpDt.Rows.Count > 0)
                                {
                                    row["VNSANGHO"] = tmpDt.Rows[0]["VNSANGHO"].ToString();
                                }
                                else
                                {
                                    row["VNSANGHO"] = "";
                                }
                                retDt.Rows.Add(row);
                            }
                        }

                        iCount = iCount + 1;
                    }
                }
                else
                {
                    DataRow row5 = retDt.NewRow();

                    if ((this.CBO01_GGUBUN.GetValue().ToString() == "") ||
                                   ((this.CBO01_GGUBUN.GetValue().ToString() != "") && (this.CBO01_GGUBUN.GetValue().ToString() == dt.Rows[i]["TNLOCATE"].ToString())))
                    {
                        row5["SNTANKNO"] = dt.Rows[i]["SNTANKNO"].ToString();
                        row5["SNDATE"] = dt.Rows[i]["SNDATE"].ToString();
                        row5["SNHWAMUL"] = dt.Rows[i]["SNHWAMUL"].ToString();
                        sHWAMUL = dt.Rows[i]["SNHWAMUL"].ToString();
                        row5["HMDESC1"] = dt.Rows[i]["HMDESC1"].ToString();
                        row5["SNHIGH"] = double.Parse(dt.Rows[i]["SNHIGH"].ToString());
                        row5["SNTEMP"] = double.Parse(dt.Rows[i]["SNTEMP"].ToString());
                        row5["SNGKLQTY"] = double.Parse(dt.Rows[i]["SNGKLQTY"].ToString());
                        row5["SNNKLQTY"] = double.Parse(dt.Rows[i]["SNNKLQTY"].ToString());
                        row5["SNMTQTY"] = double.Parse(dt.Rows[i]["SNMTQTY"].ToString());
                        row5["SNNOTE"] = double.Parse(dt.Rows[i]["SNNOTE"].ToString());
                        row5["BALANCE"] = double.Parse(dt.Rows[i]["BALANCE"].ToString());
                        row5["DTJEQTY"] = double.Parse(dt.Rows[i]["DTJEQTY"].ToString());
                        row5["TNFIRECAPA"] = double.Parse(dt.Rows[i]["TNFIRECAPA"].ToString());

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_73SIV126", sSNDATE,
                                                                    dt.Rows[i]["SNTANKNO"].ToString().Trim(),
                                                                    sHWAMUL
                                                                    );

                        tmpDt = this.DbConnector.ExecuteDataTable();

                        if (tmpDt.Rows.Count > 0)
                        {
                            row5["VNSANGHO"] = tmpDt.Rows[0]["VNSANGHO"].ToString();
                        }
                        else
                        {
                            row5["VNSANGHO"] = "";
                        }

                        retDt.Rows.Add(row5);
                    }

                    iCount = iCount + 1;
                }
            }

            return retDt;
        }
        #endregion
    }
}
