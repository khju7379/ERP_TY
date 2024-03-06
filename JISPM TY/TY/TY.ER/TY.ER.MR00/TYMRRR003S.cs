using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.MR00
{
    /// <summary>
    /// 입고 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2013.01.21 14:33
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_31L2P832 : 입고 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_31L2Q834 : 입고 조회(거래처)
    ///  TY_S_MR_31L2Q835 : 입고 조회(품목
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_2BF7Y364 : 조회가 완료되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  RRN1100 : 입고거래처
    ///  RRN1050 : 품목
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    /// </summary>
    public partial class TYMRRR003S : TYBase
    {
        #region Description : 페이지 로드
        public TYMRRR003S()
        {
            InitializeComponent();
        }

        private void TYMRRR003S_Load(object sender, System.EventArgs e)
        {
            DTP01_GSTDATE.SetValue(DateTime.Now.ToString("yyyyMM") + "01");
            DTP01_GEDDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));

            SetStartingFocus(this.DTP01_GSTDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sRRN1050 = string.Empty;
            sRRN1050 = this.CBO01_RRN1050.GetValue().ToString();

            this.FPS91_TY_S_MR_31L2Q834.Initialize();

            this.DbConnector.CommandClear();

            // 품목별 조회
            if (sRRN1050.ToString() == "1")
            {
                this.DbConnector.Attach
                (
                "TY_P_MR_31L2P832",
                this.DTP01_GSTDATE.GetValue().ToString(),
                this.DTP01_GEDDATE.GetValue().ToString(),
                this.CBH01_RRN1100.GetValue().ToString()
                );

                this.FPS91_TY_S_MR_31L2Q835.SetValue(UP_SumData(this.DbConnector.ExecuteDataTable(), sRRN1050));
                if (this.FPS91_TY_S_MR_31L2Q835.CurrentRowCount > 0)
                {

                    this.FPS91_TY_S_MR_31L2Q834.Visible = false;
                    this.FPS91_TY_S_MR_31L2Q835.Visible = true;
                    this.SetSpreadSumRow(this.FPS91_TY_S_MR_31L2Q835, "Z105013", "[품목별 소계]", SumRowType.SubTotal);

                }
            }
            // 거래처 및 일자별 조회
            else if (sRRN1050.ToString() == "2")
            {
                this.DbConnector.Attach
                (
                "TY_P_MR_31MC5856",
                this.DTP01_GSTDATE.GetValue().ToString(),
                this.DTP01_GEDDATE.GetValue().ToString(),
                this.CBH01_RRN1100.GetValue().ToString()
                );

                this.FPS91_TY_S_MR_31L2Q834.SetValue(UP_SumData(this.DbConnector.ExecuteDataTable(), sRRN1050));
                if (this.FPS91_TY_S_MR_31L2Q834.CurrentRowCount > 0)
                {
                    this.FPS91_TY_S_MR_31L2Q834.Visible = true;
                    this.FPS91_TY_S_MR_31L2Q835.Visible = false;
                    this.SetSpreadSumRow(this.FPS91_TY_S_MR_31L2Q834, "RRM1100", "[일자별 소계]", SumRowType.SubTotal);
                    this.SetSpreadSumRow(this.FPS91_TY_S_MR_31L2Q834, "RRM1100", "[거래처별 소계]", SumRowType.Total);
                }
            }

            this.ShowMessage("TY_M_GB_2BF7Y364");
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            // 품목별 출력
            if (this.CBO01_RRN1050.GetValue().ToString() == "1")
            {
                this.DbConnector.Attach
                    (
                    "TY_P_MR_31O5P898",
                    this.DTP01_GSTDATE.GetValue().ToString(),
                    this.DTP01_GEDDATE.GetValue().ToString(),
                    this.CBH01_RRN1100.GetValue().ToString()
                    );

                SectionReport rpt = new TYMRRR0032R(this.DTP01_GSTDATE.GetValue().ToString(), this.DTP01_GEDDATE.GetValue().ToString());

                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, this.DbConnector.ExecuteDataTable())).ShowDialog();
            }
            // 거래처 및 일자별 출력
            else if (this.CBO01_RRN1050.GetValue().ToString() == "2")
            {
                this.DbConnector.Attach
                    (
                    "TY_P_MR_31M4B861",
                    this.DTP01_GSTDATE.GetValue().ToString(),
                    this.DTP01_GEDDATE.GetValue().ToString(),
                    this.CBH01_RRN1100.GetValue().ToString()
                    );

                SectionReport rpt = new TYMRRR0031R(this.DTP01_GSTDATE.GetValue().ToString(), this.DTP01_GEDDATE.GetValue().ToString());

                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, this.DbConnector.ExecuteDataTable())).ShowDialog();
            }
        }
        #endregion

        #region Description : 품목별 소계 및 거래처별 소계
        public DataTable UP_SumData(DataTable ds, String RRN1050)
        {
            int nNum = ds.Rows.Count;

            double dHapAmt = 0;
            double dHapdate = 0;

            DataTable dt = new DataTable();
            DataRow dtRow;

            dt = ds;

            if (nNum > 0)
            {
                string sCond = "";

                int i = 0;
                // 품목별 조회
                if (RRN1050.ToString() == "1")
                {
                    for (i = 1; i < nNum; i++)
                    {
                        // 품목별 소계 출력
                        // 현재 행의 품목명과 다음 행이 품목명이 다른 경우 실행
                        if (dt.Rows[i - 1]["RRN1050"].ToString() != dt.Rows[i]["RRN1050"].ToString())
                        {
                            dtRow = dt.NewRow();
                            dt.Rows.InsertAt(dtRow, i);

                            dt.Rows[i]["Z105013"] = "[품목별 소계]";
                            //품목별 소계 조건
                            sCond = "RRN1050 = '" + dt.Rows[i - 1]["RRN1050"].ToString().Trim() + "' ";

                            dt.Rows[i]["RRN1230"] = double.Parse(Get_Numeric(dt.Compute("Sum(RRN1230)", sCond).ToString()));
                            dHapAmt = dHapAmt + Double.Parse(Get_Numeric(dt.Compute("Sum(RRN1230)", sCond).ToString()));

                            nNum++;
                            i++;
                        }
                    }
                    // 마지막 품목에 대한 소계
                    // row 생성 및 데이터 입력
                    dtRow = dt.NewRow();
                    dt.Rows.InsertAt(dtRow, i);

                    dt.Rows[i]["Z105013"] = "[품목별 소계]";
                    // 품목별 소계 조건
                    sCond = "RRN1050 = '" + dt.Rows[i - 1]["RRN1050"].ToString().Trim() + "' ";

                    dt.Rows[i]["RRN1230"] = double.Parse(Get_Numeric(dt.Compute("Sum(RRN1230)", sCond).ToString()));
                    dHapAmt = dHapAmt + Double.Parse(Get_Numeric(dt.Compute("Sum(RRN1230)", sCond).ToString()));
                }
                // 거래처 및 일자별 조회
                else if (RRN1050.ToString() == "2")
                {
                    for (i = 1; i < nNum; i++)
                    {
                        // 일자별 소계 출력
                        // 현재 행의 입고일자와 다음행의 입고일자가 다르거나 상호명이 다른경우 실행
                        if (dt.Rows[i - 1]["RRM1100"].ToString() != dt.Rows[i]["RRM1100"].ToString() || dt.Rows[i - 1]["VNSANGHO"].ToString() != dt.Rows[i]["VNSANGHO"].ToString())
                        {
                            dtRow = dt.NewRow();
                            dt.Rows.InsertAt(dtRow, i);

                            dt.Rows[i]["RRM1100"] = "[일자별 소계]";

                            // 일자별 소계 조건
                            sCond = "RRM1100 = '" + dt.Rows[i - 1]["RRM1100"].ToString().Trim() + "' AND VNSANGHO = '" + dt.Rows[i - 1]["VNSANGHO"].ToString().Trim() + "' ";

                            dt.Rows[i]["RRN1230"] = double.Parse(Get_Numeric(dt.Compute("Sum(RRN1230)", sCond).ToString()));
                            dHapdate = dHapdate + Double.Parse(Get_Numeric(dt.Compute("Sum(RRN1230)", sCond).ToString()));

                            // 거래처별 소계 출력
                            if (dt.Rows[i - 1]["VNSANGHO"].ToString() != dt.Rows[i + 1]["VNSANGHO"].ToString())
                            {
                                dtRow = dt.NewRow();
                                dt.Rows.InsertAt(dtRow, i + 1);

                                dt.Rows[i + 1]["RRM1100"] = "[거래처별 소계]";

                                // 거래처별 소계 조건
                                sCond = "VNSANGHO = '" + dt.Rows[i - 1]["VNSANGHO"].ToString().Trim() + "' ";

                                dt.Rows[i + 1]["RRN1230"] = double.Parse(Get_Numeric(dt.Compute("Sum(RRN1230)", sCond).ToString()));
                                dHapAmt = dHapAmt + Double.Parse(Get_Numeric(dt.Compute("Sum(RRN1230)", sCond).ToString()));

                                nNum++;
                                i++;
                            }

                            nNum++;
                            i++;
                        }

                    }
                    // 마지막 거래처에 대한 소계
                    // row 생성 및 데이터 입력
                    dtRow = dt.NewRow();
                    dt.Rows.InsertAt(dtRow, i);
                    dt.Rows[i]["RRM1100"] = "[일자별 소계]";

                    // 일자별 소계 조건
                    sCond = "RRM1100 = '" + dt.Rows[i - 1]["RRM1100"].ToString().Trim() + "' AND VNSANGHO = '" + dt.Rows[i - 1]["VNSANGHO"].ToString().Trim() + "' ";

                    dt.Rows[i]["RRN1230"] = double.Parse(Get_Numeric(dt.Compute("Sum(RRN1230)", sCond).ToString()));
                    dHapdate = dHapdate + Double.Parse(Get_Numeric(dt.Compute("Sum(RRN1230)", sCond).ToString()));

                    dtRow = dt.NewRow();
                    dt.Rows.InsertAt(dtRow, i + 1);
                    dt.Rows[i + 1]["RRM1100"] = "[거래처별 소계]";

                    // 거래처별 소계 조건
                    sCond = "VNSANGHO = '" + dt.Rows[i - 1]["VNSANGHO"].ToString().Trim() + "' ";

                    dt.Rows[i + 1]["RRN1230"] = double.Parse(Get_Numeric(dt.Compute("Sum(RRN1230)", sCond).ToString()));
                    dHapAmt = dHapAmt + Double.Parse(Get_Numeric(dt.Compute("Sum(RRN1230)", sCond).ToString()));
                }
            }

            return dt;
        }
        #endregion
    }
}
