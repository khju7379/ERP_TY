using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.AC00
{
    /// <summary>
    /// 예산운영계획대실적집계표 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.08.28 09:54
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_28SA6581 : 예산운영계획대실적집계표
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_28SBM584 : 예산운영계획대실적집계표
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
    public partial class TYACLB021S : TYBase
    {
        #region Description : 페이지 로드
        public TYACLB021S()
        {
            InitializeComponent();
        }

        private void TYACLB021S_Load(object sender, System.EventArgs e)
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
                "TY_P_AC_28SA6581",
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GEDYYMM.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_28SBM584.SetValue(UP_ConvertDt(dt));

                // 특정 ROW 색깔 입히기
                for (int i = 0; i < this.FPS91_TY_S_AC_28SBM584.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_28SBM584.GetValue(i, "S_NM").ToString() == "소   계")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_28SBM584.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    }
                    else if (this.FPS91_TY_S_AC_28SBM584.GetValue(i, "S_NM").ToString() == "총   계")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_28SBM584.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
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
                "TY_P_AC_28SA6581",
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GEDYYMM.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYACLB021R();

                // 가로 출력
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

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
            int i = 0;

            string sCDDP = string.Empty;

            double dAMT1 = 0;
            double dAMT2 = 0;
            double dAMT3 = 0;
            double dAMT4 = 0;
            double dAMT5 = 0;

            double dAMT31 = 0;
            double dAMT32 = 0;
            double dAMT33 = 0;
            double dAMT34 = 0;
            double dAMT35 = 0;

            double dAMT11 = 0;
            double dAMT12 = 0;
            double dAMT13 = 0;
            double dAMT14 = 0;
            double dAMT15 = 0;

            double dAMT21 = 0;
            double dAMT22 = 0;


            double dTOTAMT1 = 0;
            double dTOTAMT2 = 0;
            double dTOTAMT3 = 0;
            double dTOTAMT4 = 0;
            double dTOTAMT5 = 0;

            double dTOTAMT31 = 0;
            double dTOTAMT32 = 0;
            double dTOTAMT33 = 0;
            double dTOTAMT34 = 0;
            double dTOTAMT35 = 0;

            double dTOTAMT11 = 0;
            double dTOTAMT12 = 0;
            double dTOTAMT13 = 0;
            double dTOTAMT14 = 0;
            double dTOTAMT15 = 0;

            double dTOTAMT21 = 0;
            double dTOTAMT22 = 0;

            string sNEWS_NM = string.Empty;
            string sOldS_NM = string.Empty;

            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;

            for (i = 1; i < nNum; i++)
            {
                if (table.Rows[i - 1]["CDDP"].ToString() != table.Rows[i]["CDDP"].ToString())
                {
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    table.Rows[i]["DATE"] = table.Rows[i - 1]["DATE"].ToString();
                    // 소 계 이름 넣기
                    table.Rows[i]["S_NM"] = "소   계";

                    // 사업장
                    sCDDP = "CDDP = '" + table.Rows[i - 1]["CDDP"].ToString() + "' ";

                    // 계획-접대비
                    table.Rows[i]["AMT1"] = table.Compute("SUM(AMT1)", sCDDP).ToString();
                    // 계획-판매촉진비
                    table.Rows[i]["AMT2"] = table.Compute("SUM(AMT2)", sCDDP).ToString();
                    // 계획-업무촉진비
                    table.Rows[i]["AMT3"] = table.Compute("SUM(AMT3)", sCDDP).ToString();
                    // 계획-분임토임비
                    table.Rows[i]["AMT4"] = table.Compute("SUM(AMT4)", sCDDP).ToString();
                    // 계획-계
                    table.Rows[i]["AMT5"] = table.Compute("SUM(AMT5)", sCDDP).ToString();

                    // 수정-접대비
                    table.Rows[i]["AMT31"] = table.Compute("SUM(AMT31)", sCDDP).ToString();
                    // 수정-판매촉진비
                    table.Rows[i]["AMT32"] = table.Compute("SUM(AMT32)", sCDDP).ToString();
                    // 수정-업무촉진비
                    table.Rows[i]["AMT33"] = table.Compute("SUM(AMT33)", sCDDP).ToString();
                    // 수정-분임토임비
                    table.Rows[i]["AMT34"] = table.Compute("SUM(AMT34)", sCDDP).ToString();
                    // 수정-계
                    table.Rows[i]["AMT35"] = table.Compute("SUM(AMT35)", sCDDP).ToString();

                    // 실적-접대비
                    table.Rows[i]["AMT11"] = table.Compute("SUM(AMT11)", sCDDP).ToString();
                    // 실적-판매촉진비
                    table.Rows[i]["AMT12"] = table.Compute("SUM(AMT12)", sCDDP).ToString();
                    // 실적-업무촉진비
                    table.Rows[i]["AMT13"] = table.Compute("SUM(AMT13)", sCDDP).ToString();
                    // 실적-분임토임비
                    table.Rows[i]["AMT14"] = table.Compute("SUM(AMT14)", sCDDP).ToString();
                    // 실적-계
                    table.Rows[i]["AMT15"] = table.Compute("SUM(AMT15)", sCDDP).ToString();

                    table.Rows[i]["AMT21"] = table.Compute("SUM(AMT21)", sCDDP).ToString();
                    table.Rows[i]["AMT22"] = table.Compute("SUM(AMT22)", sCDDP).ToString();

                    dAMT1 = Convert.ToDouble(table.Rows[i]["AMT1"].ToString());
                    dAMT2 = Convert.ToDouble(table.Rows[i]["AMT2"].ToString());
                    dAMT3 = Convert.ToDouble(table.Rows[i]["AMT3"].ToString());
                    dAMT4 = Convert.ToDouble(table.Rows[i]["AMT4"].ToString());
                    dAMT5 = Convert.ToDouble(table.Rows[i]["AMT5"].ToString());

                    dAMT31 = Convert.ToDouble(table.Rows[i]["AMT31"].ToString());
                    dAMT32 = Convert.ToDouble(table.Rows[i]["AMT32"].ToString());
                    dAMT33 = Convert.ToDouble(table.Rows[i]["AMT33"].ToString());
                    dAMT34 = Convert.ToDouble(table.Rows[i]["AMT34"].ToString());
                    dAMT35 = Convert.ToDouble(table.Rows[i]["AMT35"].ToString());

                    dAMT11 = Convert.ToDouble(table.Rows[i]["AMT11"].ToString());
                    dAMT12 = Convert.ToDouble(table.Rows[i]["AMT12"].ToString());
                    dAMT13 = Convert.ToDouble(table.Rows[i]["AMT13"].ToString());
                    dAMT14 = Convert.ToDouble(table.Rows[i]["AMT14"].ToString());
                    dAMT15 = Convert.ToDouble(table.Rows[i]["AMT15"].ToString());

                    dAMT21 = Convert.ToDouble(table.Rows[i]["AMT21"].ToString());
                    dAMT22 = Convert.ToDouble(table.Rows[i]["AMT22"].ToString());

                    // 실적율
                    if (dAMT5 == 0 || dAMT15 == 0)
                    {
                        table.Rows[i]["SILJUK"] = 100;
                    }
                    else
                    {
                        table.Rows[i]["SILJUK"] = (dAMT15 / dAMT5) * 100;
                    }

                    // CARD
                    if (dAMT21 == 0 || dAMT22 == 0)
                    {
                        table.Rows[i]["CARD"] = 100;
                    }
                    else
                    {
                        table.Rows[i]["CARD"] = (dAMT22 / (dAMT21 + dAMT22)) * 100;
                    }

                    dTOTAMT1 = dTOTAMT1 + Convert.ToDouble(table.Rows[i]["AMT1"].ToString());
                    dTOTAMT2 = dTOTAMT2 + Convert.ToDouble(table.Rows[i]["AMT2"].ToString());
                    dTOTAMT3 = dTOTAMT3 + Convert.ToDouble(table.Rows[i]["AMT3"].ToString());
                    dTOTAMT4 = dTOTAMT4 + Convert.ToDouble(table.Rows[i]["AMT4"].ToString());
                    dTOTAMT5 = dTOTAMT5 + Convert.ToDouble(table.Rows[i]["AMT5"].ToString());

                    dTOTAMT31 = dTOTAMT31 + Convert.ToDouble(table.Rows[i]["AMT31"].ToString());
                    dTOTAMT32 = dTOTAMT32 + Convert.ToDouble(table.Rows[i]["AMT32"].ToString());
                    dTOTAMT33 = dTOTAMT33 + Convert.ToDouble(table.Rows[i]["AMT33"].ToString());
                    dTOTAMT34 = dTOTAMT34 + Convert.ToDouble(table.Rows[i]["AMT34"].ToString());
                    dTOTAMT35 = dTOTAMT35 + Convert.ToDouble(table.Rows[i]["AMT35"].ToString());

                    dTOTAMT11 = dTOTAMT11 + Convert.ToDouble(table.Rows[i]["AMT11"].ToString());
                    dTOTAMT12 = dTOTAMT12 + Convert.ToDouble(table.Rows[i]["AMT12"].ToString());
                    dTOTAMT13 = dTOTAMT13 + Convert.ToDouble(table.Rows[i]["AMT13"].ToString());
                    dTOTAMT14 = dTOTAMT14 + Convert.ToDouble(table.Rows[i]["AMT14"].ToString());
                    dTOTAMT15 = dTOTAMT15 + Convert.ToDouble(table.Rows[i]["AMT15"].ToString());

                    dTOTAMT21 = dTOTAMT21 + Convert.ToDouble(table.Rows[i]["AMT21"].ToString());
                    dTOTAMT22 = dTOTAMT22 + Convert.ToDouble(table.Rows[i]["AMT22"].ToString());

                    nNum = nNum + 1;

                    i = i + 1;
                }
            }

            /******* 마지막 거래처의 대한 로우 생성*****/
            row = table.NewRow();
            table.Rows.InsertAt(row, i);
            // 합 계 이름 넣기
            table.Rows[i]["DATE"] = table.Rows[i - 1]["DATE"].ToString();
            table.Rows[i]["S_NM"] = "소   계";

            // 사업장
            sCDDP = "CDDP = '" + table.Rows[i - 1]["CDDP"].ToString() + "' ";

            // 계획-접대비
            table.Rows[i]["AMT1"] = table.Compute("SUM(AMT1)", sCDDP).ToString();
            // 계획-판매촉진비
            table.Rows[i]["AMT2"] = table.Compute("SUM(AMT2)", sCDDP).ToString();
            // 계획-업무촉진비
            table.Rows[i]["AMT3"] = table.Compute("SUM(AMT3)", sCDDP).ToString();
            // 계획-분임토임비
            table.Rows[i]["AMT4"] = table.Compute("SUM(AMT4)", sCDDP).ToString();
            // 계획-계
            table.Rows[i]["AMT5"] = table.Compute("SUM(AMT5)", sCDDP).ToString();

            // 수정-접대비
            table.Rows[i]["AMT31"] = table.Compute("SUM(AMT31)", sCDDP).ToString();
            // 수정-판매촉진비
            table.Rows[i]["AMT32"] = table.Compute("SUM(AMT32)", sCDDP).ToString();
            // 수정-업무촉진비
            table.Rows[i]["AMT33"] = table.Compute("SUM(AMT33)", sCDDP).ToString();
            // 수정-분임토임비
            table.Rows[i]["AMT34"] = table.Compute("SUM(AMT34)", sCDDP).ToString();
            // 수정-계
            table.Rows[i]["AMT35"] = table.Compute("SUM(AMT35)", sCDDP).ToString();

            // 실적-접대비
            table.Rows[i]["AMT11"] = table.Compute("SUM(AMT11)", sCDDP).ToString();
            // 실적-판매촉진비
            table.Rows[i]["AMT12"] = table.Compute("SUM(AMT12)", sCDDP).ToString();
            // 실적-업무촉진비
            table.Rows[i]["AMT13"] = table.Compute("SUM(AMT13)", sCDDP).ToString();
            // 실적-분임토임비
            table.Rows[i]["AMT14"] = table.Compute("SUM(AMT14)", sCDDP).ToString();
            // 실적-계
            table.Rows[i]["AMT15"] = table.Compute("SUM(AMT15)", sCDDP).ToString();

            table.Rows[i]["AMT21"] = table.Compute("SUM(AMT21)", sCDDP).ToString();
            table.Rows[i]["AMT22"] = table.Compute("SUM(AMT22)", sCDDP).ToString();

            dAMT1 = Convert.ToDouble(table.Rows[i]["AMT1"].ToString());
            dAMT2 = Convert.ToDouble(table.Rows[i]["AMT2"].ToString());
            dAMT3 = Convert.ToDouble(table.Rows[i]["AMT3"].ToString());
            dAMT4 = Convert.ToDouble(table.Rows[i]["AMT4"].ToString());
            dAMT5 = Convert.ToDouble(table.Rows[i]["AMT5"].ToString());

            dAMT31 = Convert.ToDouble(table.Rows[i]["AMT31"].ToString());
            dAMT32 = Convert.ToDouble(table.Rows[i]["AMT32"].ToString());
            dAMT33 = Convert.ToDouble(table.Rows[i]["AMT33"].ToString());
            dAMT34 = Convert.ToDouble(table.Rows[i]["AMT34"].ToString());
            dAMT35 = Convert.ToDouble(table.Rows[i]["AMT35"].ToString());

            dAMT11 = Convert.ToDouble(table.Rows[i]["AMT11"].ToString());
            dAMT12 = Convert.ToDouble(table.Rows[i]["AMT12"].ToString());
            dAMT13 = Convert.ToDouble(table.Rows[i]["AMT13"].ToString());
            dAMT14 = Convert.ToDouble(table.Rows[i]["AMT14"].ToString());
            dAMT15 = Convert.ToDouble(table.Rows[i]["AMT15"].ToString());

            dAMT21 = Convert.ToDouble(table.Rows[i]["AMT21"].ToString());
            dAMT22 = Convert.ToDouble(table.Rows[i]["AMT22"].ToString());

            // 실적율
            if (dAMT5 == 0 || dAMT15 == 0)
            {
                table.Rows[i]["SILJUK"] = 100;
            }
            else
            {
                table.Rows[i]["SILJUK"] = (dAMT15 / dAMT5) * 100;
            }

            // CARD
            if (dAMT21 == 0 || dAMT22 == 0)
            {
                table.Rows[i]["CARD"] = 100;
            }
            else
            {
                table.Rows[i]["CARD"] = (dAMT22 / (dAMT21 + dAMT22)) * 100;
            }

            dTOTAMT1 = dTOTAMT1 + Convert.ToDouble(table.Rows[i]["AMT1"].ToString());
            dTOTAMT2 = dTOTAMT2 + Convert.ToDouble(table.Rows[i]["AMT2"].ToString());
            dTOTAMT3 = dTOTAMT3 + Convert.ToDouble(table.Rows[i]["AMT3"].ToString());
            dTOTAMT4 = dTOTAMT4 + Convert.ToDouble(table.Rows[i]["AMT4"].ToString());
            dTOTAMT5 = dTOTAMT5 + Convert.ToDouble(table.Rows[i]["AMT5"].ToString());

            dTOTAMT31 = dTOTAMT31 + Convert.ToDouble(table.Rows[i]["AMT31"].ToString());
            dTOTAMT32 = dTOTAMT32 + Convert.ToDouble(table.Rows[i]["AMT32"].ToString());
            dTOTAMT33 = dTOTAMT33 + Convert.ToDouble(table.Rows[i]["AMT33"].ToString());
            dTOTAMT34 = dTOTAMT34 + Convert.ToDouble(table.Rows[i]["AMT34"].ToString());
            dTOTAMT35 = dTOTAMT35 + Convert.ToDouble(table.Rows[i]["AMT35"].ToString());

            dTOTAMT11 = dTOTAMT11 + Convert.ToDouble(table.Rows[i]["AMT11"].ToString());
            dTOTAMT12 = dTOTAMT12 + Convert.ToDouble(table.Rows[i]["AMT12"].ToString());
            dTOTAMT13 = dTOTAMT13 + Convert.ToDouble(table.Rows[i]["AMT13"].ToString());
            dTOTAMT14 = dTOTAMT14 + Convert.ToDouble(table.Rows[i]["AMT14"].ToString());
            dTOTAMT15 = dTOTAMT15 + Convert.ToDouble(table.Rows[i]["AMT15"].ToString());

            dTOTAMT21 = dTOTAMT21 + Convert.ToDouble(table.Rows[i]["AMT21"].ToString());
            dTOTAMT22 = dTOTAMT22 + Convert.ToDouble(table.Rows[i]["AMT22"].ToString());

            /******** 총계를 위한 Row 생성 **************/
            row = table.NewRow();
            table.Rows.InsertAt(row, i + 1);

            table.Rows[i + 1]["DATE"] = table.Rows[i - 2]["DATE"].ToString();
            // 합 계 이름 넣기
            table.Rows[i + 1]["S_NM"] = "총   계";

            // 계획-접대비
            table.Rows[i + 1]["AMT1"] = string.Format("{0:#,##0}", dTOTAMT1);
            // 계획-판매촉진비
            table.Rows[i + 1]["AMT2"] = string.Format("{0:#,##0}", dTOTAMT2);
            // 계획-업무촉진비
            table.Rows[i + 1]["AMT3"] = string.Format("{0:#,##0}", dTOTAMT3);
            // 계획-분임토임비
            table.Rows[i + 1]["AMT4"] = string.Format("{0:#,##0}", dTOTAMT4);
            // 계획-계
            table.Rows[i + 1]["AMT5"] = string.Format("{0:#,##0}", dTOTAMT5);

            // 수정-접대비
            table.Rows[i + 1]["AMT31"] = string.Format("{0:#,##0}", dTOTAMT31);
            // 수정-판매촉진비
            table.Rows[i + 1]["AMT32"] = string.Format("{0:#,##0}", dTOTAMT32);
            // 수정-업무촉진비
            table.Rows[i + 1]["AMT33"] = string.Format("{0:#,##0}", dTOTAMT33);
            // 수정-분임토임비
            table.Rows[i + 1]["AMT34"] = string.Format("{0:#,##0}", dTOTAMT34);
            // 수정-계
            table.Rows[i + 1]["AMT35"] = string.Format("{0:#,##0}", dTOTAMT35);

            // 실적-접대비
            table.Rows[i + 1]["AMT11"] = string.Format("{0:#,##0}", dTOTAMT11);
            // 실적-판매촉진비
            table.Rows[i + 1]["AMT12"] = string.Format("{0:#,##0}", dTOTAMT12);
            // 실적-업무촉진비
            table.Rows[i + 1]["AMT13"] = string.Format("{0:#,##0}", dTOTAMT13);
            // 실적-분임토임비
            table.Rows[i + 1]["AMT14"] = string.Format("{0:#,##0}", dTOTAMT14);
            // 실적-계
            table.Rows[i + 1]["AMT15"] = string.Format("{0:#,##0}", dTOTAMT15);

            table.Rows[i + 1]["AMT21"] = string.Format("{0:#,##0}", dTOTAMT21);
            table.Rows[i + 1]["AMT22"] = string.Format("{0:#,##0}", dTOTAMT22);

            // 실적율
            if (dTOTAMT5 == 0 || dTOTAMT15 == 0)
            {
                table.Rows[i + 1]["SILJUK"] = 100;
            }
            else
            {
                table.Rows[i + 1]["SILJUK"] = (dTOTAMT15 / dTOTAMT5) * 100;
            }

            // CARD
            if (dTOTAMT21 == 0 || dTOTAMT22 == 0)
            {
                table.Rows[i + 1]["CARD"] = 100;
            }
            else
            {
                table.Rows[i + 1]["CARD"] = (dTOTAMT22 / (dTOTAMT21 + dTOTAMT22)) * 100;
            }

            DataTable Condt = new DataTable();

            Condt.Columns.Add("CDDP",    typeof(System.String));
            Condt.Columns.Add("S_NM",    typeof(System.String));
            Condt.Columns.Add("Y2CDSB",  typeof(System.String));
            Condt.Columns.Add("KBHANGL", typeof(System.String));
            Condt.Columns.Add("Y2CDDP",  typeof(System.String));
            Condt.Columns.Add("PART_NM", typeof(System.String));
            Condt.Columns.Add("AMT1",    typeof(System.String));
            Condt.Columns.Add("AMT2",    typeof(System.String));
            Condt.Columns.Add("AMT3",    typeof(System.String));
            Condt.Columns.Add("AMT4",    typeof(System.String));
            Condt.Columns.Add("AMT5",    typeof(System.String));
            Condt.Columns.Add("AMT31",   typeof(System.String));
            Condt.Columns.Add("AMT32",   typeof(System.String));
            Condt.Columns.Add("AMT33",   typeof(System.String));
            Condt.Columns.Add("AMT34",   typeof(System.String));
            Condt.Columns.Add("AMT35",   typeof(System.String));
            Condt.Columns.Add("AMT11",   typeof(System.String));
            Condt.Columns.Add("AMT12",   typeof(System.String));
            Condt.Columns.Add("AMT13",   typeof(System.String));
            Condt.Columns.Add("AMT14",   typeof(System.String));
            Condt.Columns.Add("AMT15",   typeof(System.String));
            Condt.Columns.Add("AMT21",   typeof(System.String));
            Condt.Columns.Add("AMT22",   typeof(System.String));
            Condt.Columns.Add("DATE",    typeof(System.String));
            Condt.Columns.Add("SILJUK",  typeof(System.String));
            Condt.Columns.Add("CARD",    typeof(System.String));
                        
            for (i = 0; i < table.Rows.Count; i++)
            {
                sNEWS_NM = table.Rows[i]["S_NM"].ToString();                

                row = Condt.NewRow();

                row["CDDP"] = table.Rows[i]["CDDP"].ToString();

                if (sNEWS_NM != sOldS_NM)
                {
                    row["S_NM"] = table.Rows[i]["S_NM"].ToString();

                    sOldS_NM = sNEWS_NM;
                }
                else
                {
                    row["S_NM"] = "";
                }

                row["Y2CDSB"]  = table.Rows[i]["Y2CDSB"].ToString();
                row["KBHANGL"] = table.Rows[i]["KBHANGL"].ToString();
                row["Y2CDDP"]  = table.Rows[i]["Y2CDDP"].ToString();
                row["PART_NM"] = table.Rows[i]["PART_NM"].ToString();
                row["AMT1"]    = table.Rows[i]["AMT1"].ToString();
                row["AMT2"]    = table.Rows[i]["AMT2"].ToString();
                row["AMT3"]    = table.Rows[i]["AMT3"].ToString();
                row["AMT4"]    = table.Rows[i]["AMT4"].ToString();
                row["AMT5"]    = table.Rows[i]["AMT5"].ToString();
                row["AMT31"]   = table.Rows[i]["AMT31"].ToString();
                row["AMT32"]   = table.Rows[i]["AMT32"].ToString();
                row["AMT33"]   = table.Rows[i]["AMT33"].ToString();
                row["AMT34"]   = table.Rows[i]["AMT34"].ToString();
                row["AMT35"]   = table.Rows[i]["AMT35"].ToString();
                row["AMT11"]   = table.Rows[i]["AMT11"].ToString();
                row["AMT12"]   = table.Rows[i]["AMT12"].ToString();
                row["AMT13"]   = table.Rows[i]["AMT13"].ToString();
                row["AMT14"]   = table.Rows[i]["AMT14"].ToString();
                row["AMT15"]   = table.Rows[i]["AMT15"].ToString();
                row["AMT21"]   = table.Rows[i]["AMT21"].ToString();
                row["AMT22"]   = table.Rows[i]["AMT22"].ToString();
                row["DATE"]    = table.Rows[i]["DATE"].ToString();
                row["SILJUK"]  = table.Rows[i]["SILJUK"].ToString();
                row["CARD"]    = table.Rows[i]["CARD"].ToString();

                Condt.Rows.Add(row);
            }

            return Condt;
        }
        #endregion
    }
}