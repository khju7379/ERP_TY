using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TY.Service.Library;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.AC00
{
    /// <summary>
    /// 일계표출력 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.08.07 10:11
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_287A3325 : 일계표출력
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_287AG327 : 일계표 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    /// </summary>
    public partial class TYACBJ013S : TYBase
    {
        #region Description : 페이지 로드
        public TYACBJ013S()
        {
            InitializeComponent();
        }

        private void TYACBJ013S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.DTP01_GSTDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_287A3325",
                this.DTP01_GSTDATE.GetString(),
                this.DTP01_GEDDATE.GetString()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_287AG327.SetValue(UP_ConvertDt(dt));
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_287A3325",
                this.DTP01_GSTDATE.GetValue(),
                this.DTP01_GEDDATE.GetValue()
                );

            SectionReport rpt = new TYACBJ013R();

            // 가로 출력
            rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {

                (new TYERGB001P(rpt, UP_ConvertDt(dt))).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        }
        #endregion

        #region Description : 데이터테이블 컨버젼
        private DataTable UP_ConvertDt(DataTable dt)
        {
            double dAMT1_HAP    = 0;
            double dTMAMDR_HAP  = 0;
            double dTMAMOUT_HAP = 0;
            double dTMAMIN_HAP  = 0;
            double dTMAMCR_HAP  = 0;
            double dAMT2_HAP    = 0;
            double dAMT3        = 0;

            string sSDATE1 = string.Empty;
            string sSDATE2 = string.Empty;

            DataTable Condt = new DataTable();

            DataRow row;

            Condt.Columns.Add("GUBUN",   typeof(System.String));
            Condt.Columns.Add("AMT1",    typeof(System.String));
            Condt.Columns.Add("TMAMDR",  typeof(System.String));
            Condt.Columns.Add("TMAMOUT", typeof(System.String));
            Condt.Columns.Add("TMCDAC",  typeof(System.String));
            Condt.Columns.Add("A1ABAC",  typeof(System.String));
            Condt.Columns.Add("TMAMIN",  typeof(System.String));
            Condt.Columns.Add("TMAMCR",  typeof(System.String));
            Condt.Columns.Add("AMT2",    typeof(System.String));
            Condt.Columns.Add("AMT3",    typeof(System.String));
            Condt.Columns.Add("SDATE1",  typeof(System.String));
            Condt.Columns.Add("SDATE2",  typeof(System.String));

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                sSDATE1 = dt.Rows[i]["SDATE1"].ToString();
                sSDATE2 = dt.Rows[i]["SDATE2"].ToString();

                dAMT1_HAP    =  dAMT1_HAP   + double.Parse(dt.Rows[i]["AMT1"].ToString());
                dTMAMDR_HAP  = dTMAMDR_HAP  + double.Parse(dt.Rows[i]["TMAMDR"].ToString());
                dTMAMOUT_HAP = dTMAMOUT_HAP + double.Parse(dt.Rows[i]["TMAMOUT"].ToString());
                dTMAMIN_HAP  = dTMAMIN_HAP  + double.Parse(dt.Rows[i]["TMAMIN"].ToString());
                dTMAMCR_HAP  = dTMAMCR_HAP  + double.Parse(dt.Rows[i]["TMAMCR"].ToString());
                dAMT2_HAP    = dAMT2_HAP    + double.Parse(dt.Rows[i]["AMT2"].ToString());
                dAMT3        = double.Parse(Get_Numeric(dt.Rows[i]["AMT3"].ToString()));

                row = Condt.NewRow();

                row["GUBUN"]   = "";
                row["AMT1"]    = string.Format("{0:#,###}",dt.Rows[i]["AMT1"].ToString());
                row["TMAMDR"]  = string.Format("{0:#,###}",dt.Rows[i]["TMAMDR"].ToString());
                row["TMAMOUT"] = string.Format("{0:#,###}",dt.Rows[i]["TMAMOUT"].ToString());
                row["TMCDAC"]  = dt.Rows[i]["TMCDAC"].ToString();
                row["A1ABAC"]  = dt.Rows[i]["A1ABAC"].ToString();
                row["TMAMIN"]  = string.Format("{0:#,###}",dt.Rows[i]["TMAMIN"].ToString());
                row["TMAMCR"]  = string.Format("{0:#,###}",dt.Rows[i]["TMAMCR"].ToString());
                row["AMT2"]    = string.Format("{0:#,###}", dt.Rows[i]["AMT2"].ToString());
                row["AMT3"]    = Get_Numeric(dt.Rows[i]["AMT3"].ToString());
                row["SDATE1"]  = dt.Rows[i]["SDATE1"].ToString();
                row["SDATE2"]  = dt.Rows[i]["SDATE2"].ToString();

                Condt.Rows.Add(row);
            }

            row = Condt.NewRow();

            row["GUBUN"]   = "HAP";
            row["AMT1"]    = string.Format("{0:#,###}", dAMT1_HAP);
            row["TMAMDR"]  = string.Format("{0:#,###}", dTMAMDR_HAP);
            row["TMAMOUT"] = string.Format("{0:#,###}", dTMAMOUT_HAP);
            row["TMCDAC"]  = "";
            row["A1ABAC"]  = "계";
            row["TMAMIN"]  = string.Format("{0:#,###}", dTMAMIN_HAP);
            row["TMAMCR"]  = string.Format("{0:#,###}", dTMAMCR_HAP);
            row["AMT2"]    = string.Format("{0:#,###}", dAMT2_HAP);
            row["AMT3"]    = string.Format("{0:#,###}", dAMT3);
            row["SDATE1"]  = sSDATE1.ToString();
            row["SDATE2"]  = sSDATE2.ToString();

            Condt.Rows.Add(row);

            row = Condt.NewRow();

            row["GUBUN"]   = "HAP";
            row["AMT1"]    = string.Format("{0:#,###}", dAMT3 + dTMAMIN_HAP - dTMAMOUT_HAP);
            row["TMAMDR"]  = "0";
            row["TMAMOUT"] = string.Format("{0:#,###}", dAMT3 + dTMAMIN_HAP - dTMAMOUT_HAP);
            row["TMCDAC"]  = "";
            row["A1ABAC"]  = "당일/전일";
            row["TMAMIN"]  = string.Format("{0:#,###}", dAMT3);
            row["TMAMCR"]  = "0";
            row["AMT2"]    = string.Format("{0:#,###}", dAMT3);
            row["AMT3"]    = string.Format("{0:#,###}", dAMT3);
            row["SDATE1"]  = sSDATE1.ToString();
            row["SDATE2"]  = sSDATE2.ToString();

            Condt.Rows.Add(row);

            row = Condt.NewRow();

            row["GUBUN"]   = "HAP";
            row["AMT1"]    = string.Format("{0:#,###}", dTMAMDR_HAP + dAMT3 + dTMAMIN_HAP);
            row["TMAMDR"]  = string.Format("{0:#,###}", dTMAMDR_HAP);
            row["TMAMOUT"] = string.Format("{0:#,###}", dAMT3 + dTMAMIN_HAP);
            row["TMCDAC"]  = "";
            row["A1ABAC"]  = "";
            row["TMAMIN"]  = string.Format("{0:#,###}", dTMAMIN_HAP + dAMT3);
            row["TMAMCR"]  = string.Format("{0:#,###}", dTMAMCR_HAP);
            row["AMT2"]    = string.Format("{0:#,###}", dAMT3 + dTMAMCR_HAP + dTMAMIN_HAP);
            row["AMT3"]    = string.Format("{0:#,###}", dAMT3);
            row["SDATE1"]  = sSDATE1.ToString();
            row["SDATE2"]  = sSDATE2.ToString();

            Condt.Rows.Add(row);

            return Condt;
        }
        #endregion
    }
}