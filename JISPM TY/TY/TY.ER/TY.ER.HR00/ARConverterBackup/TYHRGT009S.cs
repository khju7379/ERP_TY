using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using System.Drawing;
using TY.Service.Library;
using TY.Service.Library.Controls;
using DataDynamics.ActiveReports;
using TY.ER.GB00;

namespace TY.ER.HR00
{
    /// <summary>
    /// 개인별 월연장수당 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.12.18 13:34
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4CIDI866 : 개인별 월연장수당 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_4CIDM867 : 개인별 월연장수당 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  KBBUSEO : 부서
    ///  KBSABUN : 사번
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYHRGT009S : TYBase
    {
        #region Description : 페이지 로드
        public TYHRGT009S()
        {
            InitializeComponent();

            this.CBH01_KBBUSEO.DummyValue = DateTime.Now.ToString("yyyyMMdd");
            this.CBH01_KBBSTEAM.DummyValue = DateTime.Now.ToString("yyyyMMdd");
        }

        private void TYHRGT009S_Load(object sender, System.EventArgs e)
        {
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            bool returnValue = UP_SearchCheck();

            if (returnValue != true)
            {
                return;
            }
            
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_HR_4CIDI866",
                DTP01_STDATE.GetString(),
                DTP01_EDDATE.GetString(),
                CBH01_KBSABUN.GetValue().ToString(),
                CBH01_KBBUSEO.GetValue().ToString(),
                CBH01_KBBSTEAM.GetValue().ToString()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                dt = UP_DateTableChange(dt);
            }

            this.FPS91_TY_S_HR_4CIDM867.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_HR_4CIDM867.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_HR_4CIDM867.GetValue(i, "KBHANGL").ToString() == "개인별계")
                    {
                        // 특정 칼럼 색깔 입히기
                        this.FPS91_TY_S_HR_4CIDM867.ActiveSheet.Rows[i].BackColor = Color.SkyBlue;
                    }
                    else if (this.FPS91_TY_S_HR_4CIDM867.GetValue(i, "KBBUSEONM").ToString() == "부서계")
                    {
                        // 특정 칼럼 색깔 입히기
                        this.FPS91_TY_S_HR_4CIDM867.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    }
                    else if (this.FPS91_TY_S_HR_4CIDM867.GetValue(i, "KBBUSEONM").ToString() == "소속계")
                    {
                        // 특정 칼럼 색깔 입히기
                        this.FPS91_TY_S_HR_4CIDM867.ActiveSheet.Rows[i].BackColor = Color.FromArgb(242, 231, 147);
                    }
                    else if (this.FPS91_TY_S_HR_4CIDM867.GetValue(i, "KBBUSEO").ToString() == "총 계")
                    {
                        // 특정 칼럼 색깔 입히기
                        this.FPS91_TY_S_HR_4CIDM867.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
                    }
                }
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Description : 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_HR_4CIDI866",
                DTP01_STDATE.GetString(),
                DTP01_EDDATE.GetString(),
                CBH01_KBSABUN.GetValue().ToString(),
                CBH01_KBBUSEO.GetValue().ToString(),
                CBH01_KBBSTEAM.GetValue().ToString()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            ActiveReport rpt = new TYHRGT009R("( " + this.DTP01_STDATE.GetValue().ToString() + " ~ " + this.DTP01_EDDATE.GetValue().ToString() + " )");

            (new TYERGB001P(rpt, dt)).ShowDialog();
        }
        #endregion

        #region Description : 출력 데이터 테이블 변환
        private DataTable UP_DateTableChange(DataTable dt)
        {
            DataTable rtnDt = new DataTable();

            DataRow row;

            double dGIJOTIME1_HAP = 0;
            double dGIHTTIME1_HAP = 0;
            double dGIOTTIME1_HAP = 0;
            double dGINTTIME1_HAP = 0;
            double dGIHUTIME1_HAP = 0;
            double dGINUTIME1_HAP = 0;
            double dGIGJTIME1_HAP = 0;
            double dGIINTIME1_HAP = 0;
            double dPMORDPAY1_HAP = 0;
            double dGIAMT1_HAP = 0;
            double dGJAMT1_HAP = 0;

            double dGIJOTIME2_HAP = 0;
            double dGIHTTIME2_HAP = 0;
            double dGIOTTIME2_HAP = 0;
            double dGINTTIME2_HAP = 0;
            double dGIHUTIME2_HAP = 0;
            double dGINUTIME2_HAP = 0;
            double dGIGJTIME2_HAP = 0;
            double dGIINTIME2_HAP = 0;
            double dPMORDPAY2_HAP = 0;
            double dGIAMT2_HAP = 0;
            double dGJAMT2_HAP = 0;

            double dGIJOTIME3_HAP = 0;
            double dGIHTTIME3_HAP = 0;
            double dGIOTTIME3_HAP = 0;
            double dGINTTIME3_HAP = 0;
            double dGIHUTIME3_HAP = 0;
            double dGINUTIME3_HAP = 0;
            double dGIGJTIME3_HAP = 0;
            double dGIINTIME3_HAP = 0;
            double dPMORDPAY3_HAP = 0;
            double dGIAMT3_HAP = 0;
            double dGJAMT3_HAP = 0;

            double dGIJOTIME4_HAP = 0;
            double dGIHTTIME4_HAP = 0;
            double dGIOTTIME4_HAP = 0;
            double dGINTTIME4_HAP = 0;
            double dGIHUTIME4_HAP = 0;
            double dGINUTIME4_HAP = 0;
            double dGIGJTIME4_HAP = 0;
            double dGIINTIME4_HAP = 0;
            double dPMORDPAY4_HAP = 0;
            double dGIAMT4_HAP = 0;
            double dGJAMT4_HAP = 0;

            rtnDt.Columns.Add("SABUNGB", typeof(System.String));
            rtnDt.Columns.Add("SOSOKGB", typeof(System.String));
            rtnDt.Columns.Add("BUSEOGB", typeof(System.String));
            rtnDt.Columns.Add("KBSOSOK", typeof(System.String));
            rtnDt.Columns.Add("KBSOSOKNM", typeof(System.String));
            rtnDt.Columns.Add("KBBUSEO", typeof(System.String));
            rtnDt.Columns.Add("KBBUSEONM", typeof(System.String));
            rtnDt.Columns.Add("KBBSTEAM", typeof(System.String));
            rtnDt.Columns.Add("KBBSTEAMNM", typeof(System.String));
            rtnDt.Columns.Add("GISABUN", typeof(System.String));
            rtnDt.Columns.Add("KBHANGL", typeof(System.String));
            rtnDt.Columns.Add("GIDATE", typeof(System.String));
            rtnDt.Columns.Add("GIYOIL", typeof(System.String));
            rtnDt.Columns.Add("UYYOILCD", typeof(System.String));
            rtnDt.Columns.Add("GIYOILNM", typeof(System.String));
            rtnDt.Columns.Add("GIHTTIME", typeof(System.String));
            rtnDt.Columns.Add("GIJOTIME", typeof(System.String));
            rtnDt.Columns.Add("GIOTTIME", typeof(System.String));
            rtnDt.Columns.Add("GINTTIME", typeof(System.String));
            rtnDt.Columns.Add("GIHUTIME", typeof(System.String));
            rtnDt.Columns.Add("GINUTIME", typeof(System.String));
            rtnDt.Columns.Add("GIGJTIME", typeof(System.String));
            rtnDt.Columns.Add("GIINTIME", typeof(System.String));
            rtnDt.Columns.Add("PMORDPAY", typeof(System.String));
            rtnDt.Columns.Add("GIAMT", typeof(System.String));
            rtnDt.Columns.Add("GJAMT", typeof(System.String));

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                if (i > 0)
                {
                    //-----------개인별 합계출력---------------
                    if (dt.Rows[i - 1]["GISABUN"].ToString() != dt.Rows[i]["GISABUN"].ToString())
                    {
                        row = rtnDt.NewRow();

                        row["KBSOSOK"] = DBNull.Value;
                        row["KBSOSOKNM"] = DBNull.Value;
                        row["KBBUSEO"] = DBNull.Value;
                        row["KBBUSEONM"] = DBNull.Value;
                        row["KBBSTEAM"] = DBNull.Value;
                        row["KBBSTEAMNM"] = DBNull.Value;
                        row["GISABUN"] = DBNull.Value;
                        row["KBHANGL"] = "개인별계"; 
                        row["GIDATE"] = DBNull.Value;
                        row["GIYOIL"] = DBNull.Value;
                        row["GIYOILNM"] = DBNull.Value;
                        row["GIJOTIME"] = string.Format("{0:#,##0.0}", dGIJOTIME1_HAP);
                        row["GIHTTIME"] = string.Format("{0:#,##0.0}", dGIHTTIME1_HAP);
                        row["GIOTTIME"] = string.Format("{0:#,##0.0}", dGIOTTIME1_HAP);
                        row["GINTTIME"] = string.Format("{0:#,##0.0}", dGINTTIME1_HAP);
                        row["GIHUTIME"] = string.Format("{0:#,##0.0}", dGIHUTIME1_HAP);
                        row["GINUTIME"] = string.Format("{0:#,##0.0}", dGINUTIME1_HAP);
                        row["GIGJTIME"] = string.Format("{0:#,##0.0}", dGIGJTIME1_HAP);
                        row["GIINTIME"] = string.Format("{0:#,##0.00}", dGIINTIME1_HAP);
                        row["PMORDPAY"] = string.Format("{0:#,##0}", 0);                        
                        row["GIAMT"] = string.Format("{0:#,##0}", dGIAMT1_HAP);
                        row["GJAMT"] = string.Format("{0:#,##0}", dGJAMT1_HAP);
                        
                        rtnDt.Rows.Add(row);

                        dGIJOTIME1_HAP = double.Parse(dt.Rows[i]["GIJOTIME"].ToString());
                        dGIHTTIME1_HAP = double.Parse(dt.Rows[i]["GIHTTIME"].ToString());
                        dGIOTTIME1_HAP = double.Parse(dt.Rows[i]["GIOTTIME"].ToString());
                        dGINTTIME1_HAP = double.Parse(dt.Rows[i]["GINTTIME"].ToString());
                        dGIHUTIME1_HAP = double.Parse(dt.Rows[i]["GIHUTIME"].ToString());
                        dGINUTIME1_HAP = double.Parse(dt.Rows[i]["GINUTIME"].ToString());
                        dGIGJTIME1_HAP = double.Parse(dt.Rows[i]["GIGJTIME"].ToString());
                        dGIINTIME1_HAP = double.Parse(dt.Rows[i]["GIINTIME"].ToString());
                        //dPMORDPAY1_HAP = double.Parse(dt.Rows[i]["PMORDPAY"].ToString());
                        dPMORDPAY1_HAP = 0;
                        dGIAMT1_HAP = double.Parse(dt.Rows[i]["GIAMT"].ToString());
                        dGJAMT1_HAP = double.Parse(dt.Rows[i]["GJAMT"].ToString());
                    }
                    else
                    {
                        dGIJOTIME1_HAP = dGIJOTIME1_HAP + double.Parse(dt.Rows[i]["GIJOTIME"].ToString());
                        dGIHTTIME1_HAP = dGIHTTIME1_HAP + double.Parse(dt.Rows[i]["GIHTTIME"].ToString());
                        dGIOTTIME1_HAP = dGIOTTIME1_HAP + double.Parse(dt.Rows[i]["GIOTTIME"].ToString());
                        dGINTTIME1_HAP = dGINTTIME1_HAP + double.Parse(dt.Rows[i]["GINTTIME"].ToString());
                        dGIHUTIME1_HAP = dGIHUTIME1_HAP + double.Parse(dt.Rows[i]["GIHUTIME"].ToString());
                        dGINUTIME1_HAP = dGINUTIME1_HAP + double.Parse(dt.Rows[i]["GINUTIME"].ToString());
                        dGIGJTIME1_HAP = dGIGJTIME1_HAP + double.Parse(dt.Rows[i]["GIGJTIME"].ToString());
                        dGIINTIME1_HAP = dGIINTIME1_HAP + double.Parse(dt.Rows[i]["GIINTIME"].ToString());
                        //dPMORDPAY1_HAP = dPMORDPAY1_HAP + double.Parse(dt.Rows[i]["PMORDPAY"].ToString());
                        dPMORDPAY1_HAP = 0;
                        dGIAMT1_HAP = dGIAMT1_HAP + double.Parse(dt.Rows[i]["GIAMT"].ToString());
                        dGJAMT1_HAP = dGJAMT1_HAP + double.Parse(dt.Rows[i]["GJAMT"].ToString());
                    }
                    //-----------부서별 합계출력---------------
                    if (dt.Rows[i - 1]["KBBSTEAM"].ToString() != dt.Rows[i]["KBBSTEAM"].ToString())
                    {
                        row = rtnDt.NewRow();

                        row["KBSOSOK"] = DBNull.Value;
                        row["KBSOSOKNM"] = DBNull.Value;
                        row["KBBUSEO"] = DBNull.Value;
                        row["KBBUSEONM"] = "부서계";
                        row["KBBSTEAM"] = DBNull.Value;
                        row["KBBSTEAMNM"] = DBNull.Value;
                        row["GISABUN"] = DBNull.Value;
                        row["KBHANGL"] = DBNull.Value;
                        row["GIDATE"] = DBNull.Value;
                        row["GIYOIL"] = DBNull.Value;
                        row["GIYOILNM"] = DBNull.Value;
                        row["GIJOTIME"] = string.Format("{0:#,##0.0}", dGIJOTIME2_HAP);
                        row["GIHTTIME"] = string.Format("{0:#,##0.0}", dGIHTTIME2_HAP);
                        row["GIOTTIME"] = string.Format("{0:#,##0.0}", dGIOTTIME2_HAP);
                        row["GINTTIME"] = string.Format("{0:#,##0.0}", dGINTTIME2_HAP);
                        row["GIHUTIME"] = string.Format("{0:#,##0.0}", dGIHUTIME2_HAP);
                        row["GINUTIME"] = string.Format("{0:#,##0.0}", dGINUTIME2_HAP);
                        row["GIGJTIME"] = string.Format("{0:#,##0.0}", dGIGJTIME2_HAP);
                        row["GIINTIME"] = string.Format("{0:#,##0.00}", dGIINTIME2_HAP);
                        //row["PMORDPAY"] = string.Format("{0:#,##0}", dPMORDPAY2_HAP);
                        row["PMORDPAY"] = string.Format("{0:#,##0}", 0);
                        row["GIAMT"] = string.Format("{0:#,##0}", dGIAMT2_HAP);
                        row["GJAMT"] = string.Format("{0:#,##0}", dGJAMT2_HAP);

                        rtnDt.Rows.Add(row);

                        dGIJOTIME2_HAP = double.Parse(dt.Rows[i]["GIJOTIME"].ToString());
                        dGIHTTIME2_HAP = double.Parse(dt.Rows[i]["GIHTTIME"].ToString());
                        dGIOTTIME2_HAP = double.Parse(dt.Rows[i]["GIOTTIME"].ToString());
                        dGINTTIME2_HAP = double.Parse(dt.Rows[i]["GINTTIME"].ToString());
                        dGIHUTIME2_HAP = double.Parse(dt.Rows[i]["GIHUTIME"].ToString());
                        dGINUTIME2_HAP = double.Parse(dt.Rows[i]["GINUTIME"].ToString());
                        dGIGJTIME2_HAP = double.Parse(dt.Rows[i]["GIGJTIME"].ToString());
                        dGIINTIME2_HAP = double.Parse(dt.Rows[i]["GIINTIME"].ToString());
                        //dPMORDPAY2_HAP = double.Parse(dt.Rows[i]["PMORDPAY"].ToString());
                        dPMORDPAY2_HAP = 0;
                        dGIAMT2_HAP = double.Parse(dt.Rows[i]["GIAMT"].ToString());
                        dGJAMT2_HAP = double.Parse(dt.Rows[i]["GJAMT"].ToString());
                    }
                    else
                    {
                        dGIJOTIME2_HAP = dGIJOTIME2_HAP + double.Parse(dt.Rows[i]["GIJOTIME"].ToString());
                        dGIHTTIME2_HAP = dGIHTTIME2_HAP + double.Parse(dt.Rows[i]["GIHTTIME"].ToString());
                        dGIOTTIME2_HAP = dGIOTTIME2_HAP + double.Parse(dt.Rows[i]["GIOTTIME"].ToString());
                        dGINTTIME2_HAP = dGINTTIME2_HAP + double.Parse(dt.Rows[i]["GINTTIME"].ToString());
                        dGIHUTIME2_HAP = dGIHUTIME2_HAP + double.Parse(dt.Rows[i]["GIHUTIME"].ToString());
                        dGINUTIME2_HAP = dGINUTIME2_HAP + double.Parse(dt.Rows[i]["GINUTIME"].ToString());
                        dGIGJTIME2_HAP = dGIGJTIME2_HAP + double.Parse(dt.Rows[i]["GIGJTIME"].ToString());
                        dGIINTIME2_HAP = dGIINTIME2_HAP + double.Parse(dt.Rows[i]["GIINTIME"].ToString());
                        //dPMORDPAY2_HAP = dPMORDPAY1_HAP + double.Parse(dt.Rows[i]["PMORDPAY"].ToString());
                        dPMORDPAY2_HAP = 0;
                        dGIAMT2_HAP = dGIAMT2_HAP + double.Parse(dt.Rows[i]["GIAMT"].ToString());
                        dGJAMT2_HAP = dGJAMT2_HAP + double.Parse(dt.Rows[i]["GJAMT"].ToString());
                    }
                    //-----------소속별 합계출력---------------
                    if (dt.Rows[i - 1]["KBSOSOK"].ToString() != dt.Rows[i]["KBSOSOK"].ToString())
                    {
                        row = rtnDt.NewRow();

                        row["KBSOSOK"] = DBNull.Value;
                        row["KBSOSOKNM"] = DBNull.Value;
                        row["KBBUSEO"] = DBNull.Value;
                        row["KBBUSEONM"] = "소속계";
                        row["KBBSTEAM"] = DBNull.Value;
                        row["KBBSTEAMNM"] = DBNull.Value;
                        row["GISABUN"] = DBNull.Value;
                        row["KBHANGL"] = DBNull.Value;
                        row["GIDATE"] = DBNull.Value;
                        row["GIYOIL"] = DBNull.Value;
                        row["GIYOILNM"] = DBNull.Value;
                        row["GIJOTIME"] = string.Format("{0:#,##0.0}", dGIJOTIME3_HAP);
                        row["GIHTTIME"] = string.Format("{0:#,##0.0}", dGIHTTIME3_HAP);
                        row["GIOTTIME"] = string.Format("{0:#,##0.0}", dGIOTTIME3_HAP);
                        row["GINTTIME"] = string.Format("{0:#,##0.0}", dGINTTIME3_HAP);
                        row["GIHUTIME"] = string.Format("{0:#,##0.0}", dGIHUTIME3_HAP);
                        row["GINUTIME"] = string.Format("{0:#,##0.0}", dGINUTIME3_HAP);
                        row["GIGJTIME"] = string.Format("{0:#,##0.0}", dGIGJTIME3_HAP);
                        row["GIINTIME"] = string.Format("{0:#,##0.00}", dGIINTIME3_HAP);
                        row["PMORDPAY"] = string.Format("{0:#,##0}", 0);
                        row["GIAMT"] = string.Format("{0:#,##0}", dGIAMT3_HAP);
                        row["GJAMT"] = string.Format("{0:#,##0}", dGJAMT3_HAP);

                        rtnDt.Rows.Add(row);

                        dGIJOTIME3_HAP = double.Parse(dt.Rows[i]["GIJOTIME"].ToString());
                        dGIHTTIME3_HAP = double.Parse(dt.Rows[i]["GIHTTIME"].ToString());
                        dGIOTTIME3_HAP = double.Parse(dt.Rows[i]["GIOTTIME"].ToString());
                        dGINTTIME3_HAP = double.Parse(dt.Rows[i]["GINTTIME"].ToString());
                        dGIHUTIME3_HAP = double.Parse(dt.Rows[i]["GIHUTIME"].ToString());
                        dGINUTIME3_HAP = double.Parse(dt.Rows[i]["GINUTIME"].ToString());
                        dGIGJTIME3_HAP = double.Parse(dt.Rows[i]["GIGJTIME"].ToString());
                        dGIINTIME3_HAP = double.Parse(dt.Rows[i]["GIINTIME"].ToString());
                        //dPMORDPAY3_HAP = double.Parse(dt.Rows[i]["PMORDPAY"].ToString());
                        dPMORDPAY3_HAP = 0;
                        dGIAMT3_HAP = double.Parse(dt.Rows[i]["GIAMT"].ToString());
                        dGJAMT3_HAP = double.Parse(dt.Rows[i]["GJAMT"].ToString());
                    }
                    else
                    {
                        dGIJOTIME3_HAP = dGIJOTIME3_HAP + double.Parse(dt.Rows[i]["GIJOTIME"].ToString());
                        dGIHTTIME3_HAP = dGIHTTIME3_HAP + double.Parse(dt.Rows[i]["GIHTTIME"].ToString());
                        dGIOTTIME3_HAP = dGIOTTIME3_HAP + double.Parse(dt.Rows[i]["GIOTTIME"].ToString());
                        dGINTTIME3_HAP = dGINTTIME3_HAP + double.Parse(dt.Rows[i]["GINTTIME"].ToString());
                        dGIHUTIME3_HAP = dGIHUTIME3_HAP + double.Parse(dt.Rows[i]["GIHUTIME"].ToString());
                        dGINUTIME3_HAP = dGINUTIME3_HAP + double.Parse(dt.Rows[i]["GINUTIME"].ToString());
                        dGIGJTIME3_HAP = dGIGJTIME3_HAP + double.Parse(dt.Rows[i]["GIGJTIME"].ToString());
                        dGIINTIME3_HAP = dGIINTIME3_HAP + double.Parse(dt.Rows[i]["GIINTIME"].ToString());
                        //dPMORDPAY3_HAP = dPMORDPAY1_HAP + double.Parse(dt.Rows[i]["PMORDPAY"].ToString());
                        dPMORDPAY3_HAP = 0;
                        dGIAMT3_HAP = dGIAMT3_HAP + double.Parse(dt.Rows[i]["GIAMT"].ToString());
                        dGJAMT3_HAP = dGJAMT3_HAP + double.Parse(dt.Rows[i]["GJAMT"].ToString());
                    }
                }
                else
                {
                    dGIJOTIME1_HAP = double.Parse(dt.Rows[i]["GIJOTIME"].ToString());
                    dGIHTTIME1_HAP = double.Parse(dt.Rows[i]["GIHTTIME"].ToString());
                    dGIOTTIME1_HAP = double.Parse(dt.Rows[i]["GIOTTIME"].ToString());
                    dGINTTIME1_HAP = double.Parse(dt.Rows[i]["GINTTIME"].ToString());
                    dGIHUTIME1_HAP = double.Parse(dt.Rows[i]["GIHUTIME"].ToString());
                    dGINUTIME1_HAP = double.Parse(dt.Rows[i]["GINUTIME"].ToString());
                    dGIGJTIME1_HAP = double.Parse(dt.Rows[i]["GIGJTIME"].ToString());
                    dGIINTIME1_HAP = double.Parse(dt.Rows[i]["GIINTIME"].ToString());
                    dPMORDPAY1_HAP = double.Parse(dt.Rows[i]["PMORDPAY"].ToString());
                    dGIAMT1_HAP = double.Parse(dt.Rows[i]["GIAMT"].ToString());
                    dGIJOTIME2_HAP = double.Parse(dt.Rows[i]["GIJOTIME"].ToString());
                    dGIHTTIME2_HAP = double.Parse(dt.Rows[i]["GIHTTIME"].ToString());
                    dGIOTTIME2_HAP = double.Parse(dt.Rows[i]["GIOTTIME"].ToString());
                    dGINTTIME2_HAP = double.Parse(dt.Rows[i]["GINTTIME"].ToString());
                    dGIHUTIME2_HAP = double.Parse(dt.Rows[i]["GIHUTIME"].ToString());
                    dGINUTIME2_HAP = double.Parse(dt.Rows[i]["GINUTIME"].ToString());
                    dGIGJTIME2_HAP = double.Parse(dt.Rows[i]["GIGJTIME"].ToString());
                    dGIINTIME2_HAP = double.Parse(dt.Rows[i]["GIINTIME"].ToString());
                    dPMORDPAY2_HAP = double.Parse(dt.Rows[i]["PMORDPAY"].ToString());
                    dGIAMT2_HAP = double.Parse(dt.Rows[i]["GIAMT"].ToString());
                    dGIJOTIME3_HAP = double.Parse(dt.Rows[i]["GIJOTIME"].ToString());
                    dGIHTTIME3_HAP = double.Parse(dt.Rows[i]["GIHTTIME"].ToString());
                    dGIOTTIME3_HAP = double.Parse(dt.Rows[i]["GIOTTIME"].ToString());
                    dGINTTIME3_HAP = double.Parse(dt.Rows[i]["GINTTIME"].ToString());
                    dGIHUTIME3_HAP = double.Parse(dt.Rows[i]["GIHUTIME"].ToString());
                    dGINUTIME3_HAP = double.Parse(dt.Rows[i]["GINUTIME"].ToString());
                    dGIGJTIME3_HAP = double.Parse(dt.Rows[i]["GIGJTIME"].ToString());
                    dGIINTIME3_HAP = double.Parse(dt.Rows[i]["GIINTIME"].ToString());
                    dPMORDPAY3_HAP = double.Parse(dt.Rows[i]["PMORDPAY"].ToString());
                    dGIAMT3_HAP = double.Parse(dt.Rows[i]["GIAMT"].ToString());
                    dGJAMT3_HAP = double.Parse(dt.Rows[i]["GJAMT"].ToString());
                }

                row = rtnDt.NewRow();

                row["KBSOSOK"] = dt.Rows[i]["KBSOSOK"].ToString();
                row["KBSOSOKNM"] = dt.Rows[i]["KBSOSOKNM"].ToString();
                row["KBBUSEO"] = dt.Rows[i]["KBBUSEO"].ToString();
                row["KBBUSEONM"] = dt.Rows[i]["KBBUSEONM"].ToString();
                row["KBBSTEAM"] = dt.Rows[i]["KBBSTEAM"].ToString();
                row["KBBSTEAMNM"] = dt.Rows[i]["KBBSTEAMNM"].ToString();
                row["GISABUN"] = dt.Rows[i]["GISABUN"].ToString();
                row["KBHANGL"] = dt.Rows[i]["KBHANGL"].ToString();
                row["GIDATE"] = dt.Rows[i]["GIDATE"].ToString();
                row["GIYOIL"] = dt.Rows[i]["GIYOIL"].ToString();
                row["GIYOILNM"] = dt.Rows[i]["GIYOILNM"].ToString();
                row["GIJOTIME"] = dt.Rows[i]["GIJOTIME"].ToString();
                row["GIHTTIME"] = dt.Rows[i]["GIHTTIME"].ToString();
                row["GIOTTIME"] = dt.Rows[i]["GIOTTIME"].ToString();
                row["GINTTIME"] = dt.Rows[i]["GINTTIME"].ToString();
                row["GIHUTIME"] = dt.Rows[i]["GIHUTIME"].ToString();
                row["GINUTIME"] = dt.Rows[i]["GINUTIME"].ToString();
                row["GIGJTIME"] = dt.Rows[i]["GIGJTIME"].ToString();
                row["GIINTIME"] = dt.Rows[i]["GIINTIME"].ToString();
                row["PMORDPAY"] = string.Format("{0:#,###}", dt.Rows[i]["PMORDPAY"].ToString());
                row["GIAMT"] = string.Format("{0:#,###}", dt.Rows[i]["GIAMT"].ToString());
                row["GJAMT"] = string.Format("{0:#,###}", dt.Rows[i]["GJAMT"].ToString());

                rtnDt.Rows.Add(row);

                dGIJOTIME4_HAP = dGIJOTIME4_HAP + double.Parse(dt.Rows[i]["GIJOTIME"].ToString());
                dGIHTTIME4_HAP = dGIHTTIME4_HAP + double.Parse(dt.Rows[i]["GIHTTIME"].ToString());
                dGIOTTIME4_HAP = dGIOTTIME4_HAP + double.Parse(dt.Rows[i]["GIOTTIME"].ToString());
                dGINTTIME4_HAP = dGINTTIME4_HAP + double.Parse(dt.Rows[i]["GINTTIME"].ToString());
                dGIHUTIME4_HAP = dGIHUTIME4_HAP + double.Parse(dt.Rows[i]["GIHUTIME"].ToString());
                dGINUTIME4_HAP = dGINUTIME4_HAP + double.Parse(dt.Rows[i]["GINUTIME"].ToString());
                dGIGJTIME4_HAP = dGIGJTIME4_HAP + double.Parse(dt.Rows[i]["GIGJTIME"].ToString());
                dGIINTIME4_HAP = dGIINTIME4_HAP + double.Parse(dt.Rows[i]["GIINTIME"].ToString());
                dPMORDPAY4_HAP = dPMORDPAY4_HAP + double.Parse(dt.Rows[i]["PMORDPAY"].ToString());
                dGIAMT4_HAP = dGIAMT4_HAP + double.Parse(dt.Rows[i]["GIAMT"].ToString());
                dGJAMT4_HAP = dGJAMT4_HAP + double.Parse(dt.Rows[i]["GJAMT"].ToString());
            }

            //-----------마지막 로우 합계출력---------------
            row = rtnDt.NewRow();

            row["KBSOSOK"] = DBNull.Value;
            row["KBSOSOKNM"] = DBNull.Value;
            row["KBBUSEO"] = DBNull.Value;
            row["KBBUSEONM"] = DBNull.Value;
            row["KBBSTEAM"] = DBNull.Value;
            row["KBBSTEAMNM"] = DBNull.Value;
            row["GISABUN"] = DBNull.Value;
            row["KBHANGL"] = "개인별계"; 
            row["GIDATE"] = DBNull.Value;
            row["GIYOIL"] = DBNull.Value;
            row["GIYOILNM"] = DBNull.Value;
            row["GIJOTIME"] = string.Format("{0:#,##0.0}", dGIJOTIME1_HAP);
            row["GIHTTIME"] = string.Format("{0:#,##0.0}", dGIHTTIME1_HAP);
            row["GIOTTIME"] = string.Format("{0:#,##0.0}", dGIOTTIME1_HAP);
            row["GINTTIME"] = string.Format("{0:#,##0.0}", dGINTTIME1_HAP);
            row["GIHUTIME"] = string.Format("{0:#,##0.0}", dGIHUTIME1_HAP);
            row["GINUTIME"] = string.Format("{0:#,##0.0}", dGINUTIME1_HAP);
            row["GIGJTIME"] = string.Format("{0:#,##0.0}", dGIGJTIME1_HAP);
            row["GIINTIME"] = string.Format("{0:#,##0.00}", dGIINTIME1_HAP);
            row["PMORDPAY"] = string.Format("{0:#,##0}", 0);
            row["GIAMT"] = string.Format("{0:#,##0}", dGIAMT1_HAP);
            row["GJAMT"] = string.Format("{0:#,##0}", dGJAMT1_HAP);

            rtnDt.Rows.Add(row);

            row = rtnDt.NewRow();

            row["KBSOSOK"] = DBNull.Value;
            row["KBSOSOKNM"] = DBNull.Value;
            row["KBBUSEO"] = DBNull.Value;
            row["KBBUSEONM"] = "부서계";
            row["KBBSTEAM"] = DBNull.Value;
            row["KBBSTEAMNM"] = DBNull.Value;
            row["GISABUN"] = DBNull.Value;
            row["KBHANGL"] = DBNull.Value;
            row["GIDATE"] = DBNull.Value;
            row["GIYOIL"] = DBNull.Value;
            row["GIYOILNM"] = DBNull.Value;
            row["GIJOTIME"] = string.Format("{0:#,##0.0}", dGIJOTIME2_HAP);
            row["GIHTTIME"] = string.Format("{0:#,##0.0}", dGIHTTIME2_HAP);
            row["GIOTTIME"] = string.Format("{0:#,##0.0}", dGIOTTIME2_HAP);
            row["GINTTIME"] = string.Format("{0:#,##0.0}", dGINTTIME2_HAP);
            row["GIHUTIME"] = string.Format("{0:#,##0.0}", dGIHUTIME2_HAP);
            row["GINUTIME"] = string.Format("{0:#,##0.0}", dGINUTIME2_HAP);
            row["GIGJTIME"] = string.Format("{0:#,##0.0}", dGIGJTIME2_HAP);
            row["GIINTIME"] = string.Format("{0:#,##0.00}", dGIINTIME2_HAP);
            row["PMORDPAY"] = string.Format("{0:#,##0}", 0);
            row["GIAMT"] = string.Format("{0:#,##0}", dGIAMT2_HAP);
            row["GJAMT"] = string.Format("{0:#,##0}", dGJAMT2_HAP);

            rtnDt.Rows.Add(row);

            row = rtnDt.NewRow();
            
            row["KBSOSOK"] = DBNull.Value;
            row["KBSOSOKNM"] = DBNull.Value;
            row["KBBUSEO"] = DBNull.Value;
            row["KBBUSEONM"] = "소속계";
            row["KBBSTEAM"] = DBNull.Value;
            row["KBBSTEAMNM"] = DBNull.Value;
            row["GISABUN"] = DBNull.Value;
            row["KBHANGL"] = DBNull.Value;
            row["GIDATE"] = DBNull.Value;
            row["GIYOIL"] = DBNull.Value;
            row["GIYOILNM"] = DBNull.Value;
            row["GIJOTIME"] = string.Format("{0:#,##0.0}", dGIJOTIME3_HAP);
            row["GIHTTIME"] = string.Format("{0:#,##0.0}", dGIHTTIME3_HAP);
            row["GIOTTIME"] = string.Format("{0:#,##0.0}", dGIOTTIME3_HAP);
            row["GINTTIME"] = string.Format("{0:#,##0.0}", dGINTTIME3_HAP);
            row["GIHUTIME"] = string.Format("{0:#,##0.0}", dGIHUTIME3_HAP);
            row["GINUTIME"] = string.Format("{0:#,##0.0}", dGINUTIME3_HAP);
            row["GIGJTIME"] = string.Format("{0:#,##0.0}", dGIGJTIME3_HAP);
            row["GIINTIME"] = string.Format("{0:#,##0.00}", dGIINTIME3_HAP);
            row["PMORDPAY"] = string.Format("{0:#,##0}", 0);
            row["GIAMT"] = string.Format("{0:#,##0}", dGIAMT3_HAP);
            row["GJAMT"] = string.Format("{0:#,##0}", dGJAMT3_HAP);

            rtnDt.Rows.Add(row);

            row = rtnDt.NewRow();

            row["KBSOSOK"] = DBNull.Value;
            row["KBSOSOKNM"] = DBNull.Value;
            row["KBBUSEO"] = "총 계";
            row["KBBUSEONM"] = DBNull.Value;
            row["KBBSTEAM"] = DBNull.Value;
            row["KBBSTEAMNM"] = DBNull.Value;
            row["GISABUN"] = DBNull.Value;
            row["KBHANGL"] = DBNull.Value;
            row["GIDATE"] = DBNull.Value;
            row["GIYOIL"] = DBNull.Value;
            row["GIYOILNM"] = DBNull.Value;
            row["GIJOTIME"] = string.Format("{0:#,##0.0}", dGIJOTIME4_HAP);
            row["GIHTTIME"] = string.Format("{0:#,##0.0}", dGIHTTIME4_HAP);
            row["GIOTTIME"] = string.Format("{0:#,##0.0}", dGIOTTIME4_HAP);
            row["GINTTIME"] = string.Format("{0:#,##0.0}", dGINTTIME4_HAP);
            row["GIHUTIME"] = string.Format("{0:#,##0.0}", dGIHUTIME4_HAP);
            row["GINUTIME"] = string.Format("{0:#,##0.0}", dGINUTIME4_HAP);
            row["GIGJTIME"] = string.Format("{0:#,##0.0}", dGIGJTIME4_HAP);
            row["GIINTIME"] = string.Format("{0:#,##0.00}", dGIINTIME4_HAP);
            row["PMORDPAY"] = string.Format("{0:#,##0}", 0);
            row["GIAMT"] = string.Format("{0:#,##0}", dGIAMT4_HAP);
            row["GJAMT"] = string.Format("{0:#,##0}", dGJAMT4_HAP);

            rtnDt.Rows.Add(row);

            return rtnDt;
        }
        #endregion

        #region  Description : 조회 체크
        private bool UP_SearchCheck()
        {
            if (Convert.ToInt32(this.DTP01_STDATE.GetString().ToString()) > Convert.ToInt32(this.DTP01_EDDATE.GetString().ToString()))
            {
                this.ShowCustomMessage("시작일자는 종료일자보다 이전이어야 합니다. ", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                return false;
            }

            return true;
        }
        #endregion

        #region  Description : DTP01_STDATE_ValueChanged 이벤트
        private void DTP01_STDATE_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_KBBUSEO.DummyValue = this.DTP01_STDATE.GetString();
            this.CBH01_KBBSTEAM.DummyValue = this.DTP01_STDATE.GetString();
        }
        #endregion

        #region  Description : FPS91_TY_S_HR_4CIDM867_CellDoubleClick 이벤트
        private void FPS91_TY_S_HR_4CIDM867_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYHRGT009I(this.FPS91_TY_S_HR_4CIDM867.GetValue("GIDATE").ToString().Replace("-","").Trim(),
                                this.FPS91_TY_S_HR_4CIDM867.GetValue("GISABUN").ToString()                                
                                )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

    }
}
