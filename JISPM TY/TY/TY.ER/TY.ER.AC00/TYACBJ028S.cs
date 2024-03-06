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
    /// 월별현금출납장 프로그램입니다.
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
    ///  TY_P_AC_28750332 : 월별현금출납장
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2875L334 : 월별현금출납장 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACBJ028S : TYBase
    {
        #region Description : 페이지 로드
        public TYACBJ028S()
        {
            InitializeComponent();
        }

        private void TYACBJ028_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.DTP01_GSTYYMM);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_28750332",
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GEDYYMM.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_2875L334.SetValue(UP_ConvertDt(dt));

                // 특정 ROW 색깔 입히기
                for (int i = 0; i < this.FPS91_TY_S_AC_2875L334.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_2875L334.GetValue(i, "DATE").ToString() == "전월잔고")
                    {
                        // 특정 ROW 글자 크기 변경
                        this.FPS91_TY_S_AC_2875L334.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);
                    }
                    else if (this.FPS91_TY_S_AC_2875L334.GetValue(i, "DATE").ToString() == "일   계")
                    {
                        // 특정 칼럼 색깔 입히기
                        this.FPS91_TY_S_AC_2875L334.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 194);
                    }
                    else if (this.FPS91_TY_S_AC_2875L334.GetValue(i, "DATE").ToString() == "월   계")
                    {
                        // 특정 칼럼 색깔 입히기
                        this.FPS91_TY_S_AC_2875L334.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    }
                    else if (this.FPS91_TY_S_AC_2875L334.GetValue(i, "DATE").ToString() == "누   계")
                    {
                        // 특정 칼럼 색깔 입히기
                        this.FPS91_TY_S_AC_2875L334.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
                    }
                }
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
                "TY_P_AC_28750332",
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GEDYYMM.GetValue()
                );

            SectionReport rpt = new TYACBJ028R();

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
            string sNEWDATE = string.Empty;
            string sOLDDATE = string.Empty;

            string sDATE    = string.Empty;

            string sNEWYYMM = string.Empty;
            string sOLDYYMM = string.Empty;

            double dB4AMJAN = 0;

            double dB4AMDR_IL_HAP  = 0;
            double dB4AMCR_IL_HAP  = 0;
            double dB4AMDR_WOL_HAP = 0;
            double dB4AMCR_WOL_HAP = 0;
            double dB4AMDR_TOTAL   = 0;
            double dB4AMCR_TOTAL   = 0;

            DataTable Condt = new DataTable();

            DataRow row;

            Condt.Columns.Add("YYMM",    typeof(System.String));
            Condt.Columns.Add("DATE",    typeof(System.String));
            Condt.Columns.Add("B4DPAC", typeof(System.String));
            Condt.Columns.Add("B4CMAC",  typeof(System.String));
            Condt.Columns.Add("B4NOJP", typeof(System.String));
            Condt.Columns.Add("B4NOSQ",  typeof(System.String));
            Condt.Columns.Add("B4NOLN",  typeof(System.String));
            Condt.Columns.Add("B4RKAC",  typeof(System.String));
            Condt.Columns.Add("B4RKCU",  typeof(System.String));
            Condt.Columns.Add("B4AMDR",  typeof(System.String));
            Condt.Columns.Add("B4AMCR",  typeof(System.String));
            Condt.Columns.Add("B4AMJAN", typeof(System.String));

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                sNEWDATE = dt.Rows[i]["DATE"].ToString();

                sNEWYYMM = dt.Rows[i]["YYMM"].ToString();

                if (sNEWYYMM != sOLDYYMM)
                {
                    if (dt.Rows[i]["B4RKAC"].ToString() == "전일잔액")
                    {
                        sDATE = "전월잔고";

                        sOLDYYMM = sNEWYYMM.ToString();

                        // 일계 합
                        dB4AMDR_IL_HAP = dB4AMDR_IL_HAP + double.Parse(dt.Rows[i]["B4AMDR"].ToString());
                        dB4AMCR_IL_HAP = dB4AMCR_IL_HAP + double.Parse(dt.Rows[i]["B4AMCR"].ToString());

                        // 누계 합
                        dB4AMDR_TOTAL = dB4AMDR_TOTAL + double.Parse(dt.Rows[i]["B4AMDR"].ToString());
                        dB4AMCR_TOTAL = dB4AMCR_TOTAL + double.Parse(dt.Rows[i]["B4AMCR"].ToString());
                    }
                    else
                    {
                        #region Description : 일계

                        // 일계
                        row = Condt.NewRow();

                        row["YYMM"]    = sOLDYYMM.ToString();
                        row["DATE"]    = "일   계";
                        row["B4DPAC"]  = "";
                        row["B4CMAC"] = "";
                        row["B4NOJP"] = "";
                        row["B4NOSQ"]  = "";
                        row["B4NOLN"]  = "";
                        row["B4RKAC"]  = "";
                        row["B4RKCU"]  = "";
                        row["B4AMDR"]  = string.Format("{0:#,###}", dB4AMDR_IL_HAP);
                        row["B4AMCR"]  = string.Format("{0:#,###}", dB4AMCR_IL_HAP);
                        row["B4AMJAN"] = "0";

                        Condt.Rows.Add(row);

                        #endregion

                        dB4AMDR_IL_HAP = 0;
                        dB4AMCR_IL_HAP = 0;

                        #region Description : 월계

                        // 월계
                        row = Condt.NewRow();

                        row["YYMM"]    = sOLDYYMM.ToString();
                        row["DATE"]    = "월   계";
                        row["B4DPAC"] = "";
                        row["B4CMAC"] = "";
                        row["B4NOJP"]  = "";;
                        row["B4NOSQ"]  = "";
                        row["B4NOLN"]  = "";
                        row["B4RKAC"]  = "";
                        row["B4RKCU"]  = "";
                        row["B4AMDR"]  = string.Format("{0:#,###}", dB4AMDR_WOL_HAP);
                        row["B4AMCR"]  = string.Format("{0:#,###}", dB4AMCR_WOL_HAP);
                        row["B4AMJAN"] = string.Format("{0:#,###}", dB4AMDR_WOL_HAP - dB4AMCR_WOL_HAP);

                        Condt.Rows.Add(row);

                        #endregion

                        // 누계 합
                        dB4AMDR_TOTAL = dB4AMDR_TOTAL + dB4AMDR_WOL_HAP;
                        dB4AMCR_TOTAL = dB4AMCR_TOTAL + dB4AMCR_WOL_HAP;

                        dB4AMDR_WOL_HAP = 0;
                        dB4AMCR_WOL_HAP = 0;

                        #region Description : 누계

                        // 누계
                        row = Condt.NewRow();

                        row["YYMM"]    = sOLDYYMM.ToString();
                        row["DATE"]    = "누   계";
                        row["B4DPAC"] = "";
                        row["B4CMAC"]  = "";
                        row["B4NOJP"] = "";
                        row["B4NOSQ"]  = "";
                        row["B4NOLN"]  = "";
                        row["B4RKAC"]  = "";
                        row["B4RKCU"]  = "";
                        row["B4AMDR"]  = string.Format("{0:#,###}", dB4AMDR_TOTAL);
                        row["B4AMCR"]  = string.Format("{0:#,###}", dB4AMCR_TOTAL);
                        row["B4AMJAN"] = string.Format("{0:#,###}", dB4AMDR_TOTAL - dB4AMCR_TOTAL);

                        Condt.Rows.Add(row);

                        #endregion

                        // 잔액
                        dB4AMJAN = dB4AMDR_TOTAL - dB4AMCR_TOTAL;

                        #region Description : 전월잔고

                        // 전월잔고
                        row = Condt.NewRow();

                        row["YYMM"]    = sNEWYYMM.ToString();
                        row["DATE"]    = "전월잔고";
                        row["B4DPAC"] = "";
                        row["B4CMAC"]  = "";
                        row["B4NOJP"] = "";
                        row["B4NOSQ"]  = "";
                        row["B4NOLN"]  = "";
                        row["B4RKAC"]  = "";
                        row["B4RKCU"]  = "";
                        row["B4AMDR"]  = string.Format("{0:#,###}", dB4AMDR_TOTAL);
                        row["B4AMCR"]  = string.Format("{0:#,###}", dB4AMCR_TOTAL);
                        row["B4AMJAN"] = string.Format("{0:#,###}", dB4AMJAN);

                        Condt.Rows.Add(row);

                        #endregion

                        sOLDYYMM = sNEWYYMM.ToString();

                        sDATE    = sNEWDATE;

                        sOLDDATE = sNEWDATE;

                        // 일계 합
                        dB4AMDR_IL_HAP = dB4AMDR_IL_HAP + double.Parse(dt.Rows[i]["B4AMDR"].ToString());
                        dB4AMCR_IL_HAP = dB4AMCR_IL_HAP + double.Parse(dt.Rows[i]["B4AMCR"].ToString());

                        // 월계 합
                        dB4AMDR_WOL_HAP = dB4AMDR_WOL_HAP + double.Parse(dt.Rows[i]["B4AMDR"].ToString());
                        dB4AMCR_WOL_HAP = dB4AMCR_WOL_HAP + double.Parse(dt.Rows[i]["B4AMCR"].ToString());
                    }
                }
                else
                {
                    // 월계 합
                    dB4AMDR_WOL_HAP = dB4AMDR_WOL_HAP + double.Parse(dt.Rows[i]["B4AMDR"].ToString());
                    dB4AMCR_WOL_HAP = dB4AMCR_WOL_HAP + double.Parse(dt.Rows[i]["B4AMCR"].ToString());

                    if (sNEWDATE != "" && sOLDDATE == "")
                    {
                        // 일계 합
                        dB4AMDR_IL_HAP = dB4AMDR_IL_HAP + double.Parse(dt.Rows[i]["B4AMDR"].ToString());
                        dB4AMCR_IL_HAP = dB4AMCR_IL_HAP + double.Parse(dt.Rows[i]["B4AMCR"].ToString());

                        sDATE = sNEWDATE;

                        sOLDDATE = sNEWDATE;
                    }
                    else if (sNEWDATE == sOLDDATE)
                    {
                        // 일계 합
                        dB4AMDR_IL_HAP = dB4AMDR_IL_HAP + double.Parse(dt.Rows[i]["B4AMDR"].ToString());
                        dB4AMCR_IL_HAP = dB4AMCR_IL_HAP + double.Parse(dt.Rows[i]["B4AMCR"].ToString());

                        sDATE = "";
                    }
                    else if(sNEWDATE != sOLDDATE)
                    {
                        // 일계
                        row = Condt.NewRow();

                        row["YYMM"]    = dt.Rows[i]["YYMM"].ToString();
                        row["DATE"]    = "일   계";
                        row["B4DPAC"] = "";
                        row["B4CMAC"]  = "";
                        row["B4NOJP"] = "";
                        row["B4NOSQ"]  = "";
                        row["B4NOLN"]  = "";
                        row["B4RKAC"]  = "";
                        row["B4RKCU"]  = "";
                        row["B4AMDR"]  = string.Format("{0:#,###}", dB4AMDR_IL_HAP);
                        row["B4AMCR"]  = string.Format("{0:#,###}", dB4AMCR_IL_HAP);
                        row["B4AMJAN"] = "0";

                        Condt.Rows.Add(row);

                        dB4AMDR_IL_HAP = 0;
                        dB4AMCR_IL_HAP = 0;

                        dB4AMDR_IL_HAP = dB4AMDR_IL_HAP + double.Parse(dt.Rows[i]["B4AMDR"].ToString());
                        dB4AMCR_IL_HAP = dB4AMCR_IL_HAP + double.Parse(dt.Rows[i]["B4AMCR"].ToString());

                        sDATE   = sNEWDATE;

                        sOLDDATE = sNEWDATE;
                    }
                }

                // 잔액
                dB4AMJAN = dB4AMJAN + double.Parse(dt.Rows[i]["B4AMDR"].ToString()) - double.Parse(dt.Rows[i]["B4AMCR"].ToString());

                row = Condt.NewRow();

                row["YYMM"] = dt.Rows[i]["YYMM"].ToString();
                row["DATE"] = sDATE.ToString();

                if (dt.Rows[i]["B4RKAC"].ToString() == "전일잔액")
                {
                    row["B4DPAC"] = "";
                    row["B4CMAC"] = "";
                    row["B4NOJP"] = "";
                    row["B4NOSQ"] = "";
                    row["B4NOLN"] = "";
                    row["B4RKAC"] = "";
                    row["B4RKCU"] = "";
                }
                else
                {
                    row["B4DPAC"] = dt.Rows[i]["B4DPAC"].ToString();
                    row["B4CMAC"] = dt.Rows[i]["B4CMAC"].ToString();
                    row["B4NOJP"] = dt.Rows[i]["B4NOJP"].ToString();
                    row["B4NOSQ"] = dt.Rows[i]["B4NOSQ"].ToString();
                    row["B4NOLN"] = dt.Rows[i]["B4NOLN"].ToString();
                    row["B4RKAC"] = dt.Rows[i]["B4RKAC"].ToString();
                    row["B4RKCU"] = dt.Rows[i]["B4RKCU"].ToString();
                }
                row["B4AMDR"]  = string.Format("{0:#,###}", dt.Rows[i]["B4AMDR"].ToString());
                row["B4AMCR"]  = string.Format("{0:#,###}", dt.Rows[i]["B4AMCR"].ToString());
                row["B4AMJAN"] = string.Format("{0:#,###}", dB4AMJAN);

                Condt.Rows.Add(row);
            }

            #region Description : 일계

            // 일계
            row = Condt.NewRow();

            row["YYMM"]    = sOLDYYMM.ToString();
            row["DATE"]    = "일   계";
            row["B4DPAC"] = "";
            row["B4CMAC"]  = "";
            row["B4NOJP"] = "";
            row["B4NOSQ"]  = "";
            row["B4NOLN"]  = "";
            row["B4RKAC"]  = "";
            row["B4RKCU"]  = "";
            row["B4AMDR"]  = string.Format("{0:#,###}", dB4AMDR_IL_HAP);
            row["B4AMCR"]  = string.Format("{0:#,###}", dB4AMCR_IL_HAP);
            row["B4AMJAN"] = "0";

            Condt.Rows.Add(row);

            #endregion

            #region Description : 월계

            // 월계
            row = Condt.NewRow();

            row["YYMM"]    = sOLDYYMM.ToString();
            row["DATE"]    = "월   계";
            row["B4DPAC"] = "";
            row["B4CMAC"] = "";
            row["B4NOJP"] = "";
            row["B4NOSQ"]  = "";
            row["B4NOLN"]  = "";
            row["B4RKAC"]  = "";
            row["B4RKCU"]  = "";
            row["B4AMDR"]  = string.Format("{0:#,###}", dB4AMDR_WOL_HAP);
            row["B4AMCR"]  = string.Format("{0:#,###}", dB4AMCR_WOL_HAP);
            row["B4AMJAN"] = string.Format("{0:#,###}", dB4AMDR_WOL_HAP - dB4AMCR_WOL_HAP);

            Condt.Rows.Add(row);

            #endregion

            #region Description : 누계

            // 누계 합
            dB4AMDR_TOTAL = dB4AMDR_TOTAL + dB4AMDR_WOL_HAP;
            dB4AMCR_TOTAL = dB4AMCR_TOTAL + dB4AMCR_WOL_HAP;

            // 누계
            row = Condt.NewRow();

            row["YYMM"]    = sOLDYYMM.ToString();
            row["DATE"]    = "누   계";
            row["B4DPAC"] = "";
            row["B4CMAC"] = "";
            row["B4NOJP"] = "";
            row["B4NOSQ"]  = "";
            row["B4NOLN"]  = "";
            row["B4RKAC"]  = "";
            row["B4RKCU"]  = "";
            row["B4AMDR"]  = string.Format("{0:#,###}", dB4AMDR_TOTAL);
            row["B4AMCR"]  = string.Format("{0:#,###}", dB4AMCR_TOTAL);
            row["B4AMJAN"] = string.Format("{0:#,###}", dB4AMDR_TOTAL - dB4AMCR_TOTAL);

            Condt.Rows.Add(row);

            #endregion

            return Condt;
        }
        #endregion
    }
}