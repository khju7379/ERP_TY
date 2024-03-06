using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.ER.GB00;
using GrapeCity.ActiveReports;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using FarPoint.Win.Spread.CellType;

namespace TY.ER.AC00
{
    /// <summary>
    /// 차입금 잔액조회(외화) 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2018.08.03 17:15
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_883EQ504 : 차입금 잔액 조회(외화)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_873I8318 : 차입금 잔액조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYACLO009S : TYBase
    {
        #region Description : 폼 로드
        public TYACLO009S()
        {
            InitializeComponent();
        }

        private void TYACLO009S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            SetStartingFocus(this.DTP01_STDATE);

            UP_Spread_Title();
            //BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sProcedure = string.Empty;

            string sYYMMDD = (Convert.ToDouble(this.DTP01_STDATE.GetString().Substring(0, 4)) - 1).ToString() + "1231";
            this.DbConnector.CommandClear();

            // 20190430 수정전 소스
            //if (int.Parse(sYYMMDD.ToString().ToString().Substring(0, 4)) < 2019)
            //{
            //    sProcedure = "TY_P_AC_897DG699";
            //}
            //else
            //{
            //    sProcedure = "TY_P_AC_88S9T645";
            //}

            // 20190430 수정후 소스
            sProcedure = "TY_P_AC_94UB6491";


            // 20180827 수정전 소스
            //this.DbConnector.Attach("TY_P_AC_883EQ504", sYYMMDD,
            //                                            this.DTP01_STDATE.GetString());

            // 20180827 수정후 소스
            this.DbConnector.Attach(sProcedure.ToString(), sYYMMDD,
                                                           this.DTP01_STDATE.GetString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_873I8318.SetValue(UP_DataChange(dt));

            for (int i = 0; i < this.FPS91_TY_S_AC_873I8318.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_873I8318.GetValue(i, "BJDESC").ToString() == "계" || this.FPS91_TY_S_AC_873I8318.GetValue(i, "TYDESC").ToString() == "소 계" ||
                    this.FPS91_TY_S_AC_873I8318.GetValue(i, "LOCGIGANTYPENM").ToString() == "합 계" || this.FPS91_TY_S_AC_873I8318.GetValue(i, "CUDESC").ToString() == "총 계")
                {
                    // 특정 ROW 글자 크기 변경
                    this.FPS91_TY_S_AC_873I8318.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);
                }

                if (this.FPS91_TY_S_AC_873I8318.GetValue(i, "LOCGIGANTYPENM").ToString() == "합 계")
                {
                    // 특정 ROW 색깔 변경
                    this.FPS91_TY_S_AC_873I8318.ActiveSheet.Rows[i].BackColor = Color.YellowGreen;
                }

                if (this.FPS91_TY_S_AC_873I8318.GetValue(i, "CUDESC").ToString() == "총 계")
                {
                    // 특정 ROW 색깔 변경
                    this.FPS91_TY_S_AC_873I8318.ActiveSheet.Rows[i].BackColor = Color.ForestGreen;
                }
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sProcedure = string.Empty;

            string sDATE = string.Empty;
            string sYYMMDD = (Convert.ToDouble(this.DTP01_STDATE.GetString().Substring(0, 4)) - 1).ToString() + "1231";

            sDATE = "(" + this.DTP01_STDATE.GetValue().ToString() + ")";

            // 20190430 수정전 소스
            //if (int.Parse(sYYMMDD.ToString().ToString().Substring(0, 4)) < 2019)
            //{
            //    sProcedure = "TY_P_AC_897DG699";
            //}
            //else
            //{
            //    sProcedure = "TY_P_AC_88S9T645";
            //}

            // 20190430 수정후 소스
            sProcedure = "TY_P_AC_94UB6491";

            this.DbConnector.CommandClear();

            // 20180827 수정전 소스
            //this.DbConnector.Attach
            //    (
            //    "TY_P_AC_883EQ504",
            //    sYYMMDD,
            //    this.DTP01_STDATE.GetString()
            //    );

            // 20180827 수정후 소스
            this.DbConnector.Attach
                (
                sProcedure.ToString(),
                sYYMMDD,
                this.DTP01_STDATE.GetString()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYACLO009R(sDATE);

                // 가로 출력
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, UP_DataChange(dt))).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        }
        #endregion

        #region Description : 데이터셋 변경
        private DataTable UP_DataChange(DataTable dt)
        {
            double dLOCLOANTYPETOTALY        = 0;   // 차입유형 계 (전년도)
            double dLOCLOANTYPETOTALY_DOLLAR = 0;   // 차입유형 계 (전년도)
            double dLOCLOANTYPETOTALD        = 0;   // 차입유형 계 (현재)
            double dLOCLOANTYPETOTALD_DOLLAR = 0;   // 차입유형 계 (현재)
            double dLOCLOANTYPETOTALI        = 0;   // 차입유형 계 (증감)
            double dLOCLOANTYPETOTALI_DOLLAR = 0;   // 차입유형 계 (증감)
            double dLOCLOANTOTALY            = 0;   // 차입용도 계 (전년도)
            double dLOCLOANTOTALY_DOLLAR     = 0;   // 차입용도 계 (전년도)
            double dLOCLOANTOTALD            = 0;   // 차입용도 계 (현재)
            double dLOCLOANTOTALD_DOLLAR     = 0;   // 차입용도 계 (현재)
            double dLOCLOANTOTALI            = 0;   // 차입용도 계 (증감)
            double dLOCLOANTOTALI_DOLLAR     = 0;   // 차입용도 계 (증감)
            double dCUDESCTOTALY             = 0;   // 통화유형 계 (전년도)
            double dCUDESCTOTALY_DOLLAR      = 0;   // 통화유형 계 (전년도)
            double dCUDESCTOTALD             = 0;   // 통화유형 계 (현재)
            double dCUDESCTOTALD_DOLLAR      = 0;   // 통화유형 계 (현재)
            double dCUDESCTOTALI             = 0;   // 통화유형 계 (증감)
            double dCUDESCTOTALI_DOLLAR      = 0;   // 통화유형 계 (증감)
            double dTOTALY                   = 0;   // 총 계 (전년도)
            double dTOTALY_DOLLAR            = 0;   // 총 계 (전년도)
            double dTOTALD                   = 0;   // 총 계 (현재)
            double dTOTALD_DOLLAR            = 0;   // 총 계 (현재)
            double dTOTALI                   = 0;   // 총 계 (증감)
            double dTOTALI_DOLLAR            = 0;   // 총 계 (증감)

            DataTable Retdt = new DataTable();
            DataRow row;

            Retdt.Columns.Add("LOCCONTNO",      typeof(System.String));
            //Retdt.Columns.Add("LOCCONTSEQ",     typeof(System.String));
            Retdt.Columns.Add("LOCCURRTYPE",    typeof(System.String));
            Retdt.Columns.Add("CUDESC",         typeof(System.String));
            Retdt.Columns.Add("LOCGIGANTYPE",   typeof(System.String));
            Retdt.Columns.Add("LOCGIGANTYPENM", typeof(System.String));
            Retdt.Columns.Add("LOCLOAN",        typeof(System.String));
            Retdt.Columns.Add("USDESC",         typeof(System.String));
            Retdt.Columns.Add("LOCLOANTYPE",    typeof(System.String));
            Retdt.Columns.Add("TYDESC",         typeof(System.String));
            Retdt.Columns.Add("LOCBANKCD",      typeof(System.String));
            Retdt.Columns.Add("BJDESC",         typeof(System.String));
            Retdt.Columns.Add("LOCCONTAMT",     typeof(System.String));
            Retdt.Columns.Add("LOCCONTDATE",    typeof(System.String));
            Retdt.Columns.Add("LOCENDDATE",     typeof(System.String));
            Retdt.Columns.Add("LOCCONTRATE",    typeof(System.String));
            Retdt.Columns.Add("LOCRATEGB",      typeof(System.String));
            Retdt.Columns.Add("LOCCURRCD",      typeof(System.String));
            Retdt.Columns.Add("JANAMTY",        typeof(System.String));
            Retdt.Columns.Add("JANAMTYDOLLAR",  typeof(System.String));
            Retdt.Columns.Add("JANAMTD",        typeof(System.String));
            Retdt.Columns.Add("JANAMTDDOLLAR",  typeof(System.String));
            Retdt.Columns.Add("INCAMT",         typeof(System.String));
            Retdt.Columns.Add("INCAMTDOLLAR",   typeof(System.String));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row = Retdt.NewRow();

                row["LOCCONTNO"]      = dt.Rows[i]["LOCCONTNO"].ToString();
                //row["LOCCONTSEQ"]     = dt.Rows[i]["LOCCONTSEQ"].ToString();
                row["LOCCURRTYPE"]    = dt.Rows[i]["LOCCURRTYPE"].ToString();
                row["CUDESC"]         = dt.Rows[i]["CUDESC"].ToString();
                row["LOCGIGANTYPE"]   = dt.Rows[i]["LOCGIGANTYPE"].ToString();
                row["LOCGIGANTYPENM"] = dt.Rows[i]["LOCGIGANTYPENM"].ToString();
                row["LOCLOAN"]        = dt.Rows[i]["LOCLOAN"].ToString();
                row["USDESC"]         = dt.Rows[i]["USDESC"].ToString();
                row["LOCLOANTYPE"]    = dt.Rows[i]["LOCLOANTYPE"].ToString();
                row["TYDESC"]         = dt.Rows[i]["TYDESC"].ToString();
                row["LOCBANKCD"]      = dt.Rows[i]["LOCBANKCD"].ToString();
                row["BJDESC"]         = dt.Rows[i]["BJDESC"].ToString();
                row["LOCCONTAMT"]     = dt.Rows[i]["LOCCONTAMT"].ToString();
                row["LOCCONTDATE"]    = dt.Rows[i]["LOCCONTDATE"].ToString();
                row["LOCENDDATE"]     = dt.Rows[i]["LOCENDDATE"].ToString();
                row["LOCCONTRATE"]    = dt.Rows[i]["LOCCONTRATE"].ToString();
                row["LOCRATEGB"]      = dt.Rows[i]["LOCRATEGB"].ToString();
                row["LOCCURRCD"]      = dt.Rows[i]["LOCCURRCD"].ToString();
                row["JANAMTY"]        = dt.Rows[i]["JANAMTY"].ToString();
                row["JANAMTYDOLLAR"]  = dt.Rows[i]["JANAMTYDOLLAR"].ToString();
                row["JANAMTD"]        = dt.Rows[i]["JANAMTD"].ToString();
                row["JANAMTDDOLLAR"]  = dt.Rows[i]["JANAMTDDOLLAR"].ToString();
                row["INCAMT"]         = dt.Rows[i]["INCAMT"].ToString();
                row["INCAMTDOLLAR"]   = dt.Rows[i]["INCAMTDOLLAR"].ToString();

                Retdt.Rows.Add(row);

                dLOCLOANTYPETOTALY        += Convert.ToDouble(dt.Rows[i]["JANAMTY"].ToString());
                dLOCLOANTYPETOTALY_DOLLAR += Convert.ToDouble(dt.Rows[i]["JANAMTYDOLLAR"].ToString());
                dLOCLOANTYPETOTALD        += Convert.ToDouble(dt.Rows[i]["JANAMTD"].ToString());
                dLOCLOANTYPETOTALD_DOLLAR += Convert.ToDouble(dt.Rows[i]["JANAMTDDOLLAR"].ToString());
                dLOCLOANTYPETOTALI        += Convert.ToDouble(dt.Rows[i]["INCAMT"].ToString());
                dLOCLOANTYPETOTALI_DOLLAR += Convert.ToDouble(dt.Rows[i]["INCAMTDOLLAR"].ToString());
                dLOCLOANTOTALY            += Convert.ToDouble(dt.Rows[i]["JANAMTY"].ToString());
                dLOCLOANTOTALY_DOLLAR     += Convert.ToDouble(dt.Rows[i]["JANAMTYDOLLAR"].ToString());
                dLOCLOANTOTALD            += Convert.ToDouble(dt.Rows[i]["JANAMTD"].ToString());
                dLOCLOANTOTALD_DOLLAR     += Convert.ToDouble(dt.Rows[i]["JANAMTDDOLLAR"].ToString());
                dLOCLOANTOTALI            += Convert.ToDouble(dt.Rows[i]["INCAMT"].ToString());
                dLOCLOANTOTALI_DOLLAR     += Convert.ToDouble(dt.Rows[i]["INCAMTDOLLAR"].ToString());
                dCUDESCTOTALY             += Convert.ToDouble(dt.Rows[i]["JANAMTY"].ToString());
                dCUDESCTOTALY_DOLLAR      += Convert.ToDouble(dt.Rows[i]["JANAMTYDOLLAR"].ToString());
                dCUDESCTOTALD             += Convert.ToDouble(dt.Rows[i]["JANAMTD"].ToString());
                dCUDESCTOTALD_DOLLAR      += Convert.ToDouble(dt.Rows[i]["JANAMTDDOLLAR"].ToString());
                dCUDESCTOTALI             += Convert.ToDouble(dt.Rows[i]["INCAMT"].ToString());
                dCUDESCTOTALI_DOLLAR      += Convert.ToDouble(dt.Rows[i]["INCAMTDOLLAR"].ToString());
                dTOTALY                   += Convert.ToDouble(dt.Rows[i]["JANAMTY"].ToString());
                dTOTALY_DOLLAR            += Convert.ToDouble(dt.Rows[i]["JANAMTYDOLLAR"].ToString());
                dTOTALD                   += Convert.ToDouble(dt.Rows[i]["JANAMTD"].ToString());
                dTOTALD_DOLLAR            += Convert.ToDouble(dt.Rows[i]["JANAMTDDOLLAR"].ToString());
                dTOTALI                   += Convert.ToDouble(dt.Rows[i]["INCAMT"].ToString());
                dTOTALI_DOLLAR            += Convert.ToDouble(dt.Rows[i]["INCAMTDOLLAR"].ToString());

                if ((i + 1) < dt.Rows.Count)
                {
                    if ((dt.Rows[i]["LOCCURRTYPE"].ToString() + dt.Rows[i]["LOCGIGANTYPE"].ToString() + dt.Rows[i]["LOCLOAN"].ToString() + dt.Rows[i]["LOCLOANTYPE"].ToString()) !=
                        (dt.Rows[i + 1]["LOCCURRTYPE"].ToString() + dt.Rows[i + 1]["LOCGIGANTYPE"].ToString() + dt.Rows[i + 1]["LOCLOAN"].ToString() + dt.Rows[i + 1]["LOCLOANTYPE"].ToString()))
                    {
                        // 차입유형 계

                        row = Retdt.NewRow();

                        row["LOCCONTNO"]      = dt.Rows[i]["LOCCONTNO"].ToString();
                        //row["LOCCONTSEQ"]     = dt.Rows[i]["LOCCONTSEQ"].ToString();
                        row["LOCCURRTYPE"]    = dt.Rows[i]["LOCCURRTYPE"].ToString();
                        row["CUDESC"]         = dt.Rows[i]["CUDESC"].ToString();
                        row["LOCGIGANTYPE"]   = dt.Rows[i]["LOCGIGANTYPE"].ToString();
                        row["LOCGIGANTYPENM"] = dt.Rows[i]["LOCGIGANTYPENM"].ToString();
                        row["LOCLOAN"]        = dt.Rows[i]["LOCLOAN"].ToString();
                        row["USDESC"]         = dt.Rows[i]["USDESC"].ToString();
                        row["LOCLOANTYPE"]    = dt.Rows[i]["LOCLOANTYPE"].ToString();
                        row["TYDESC"]         = dt.Rows[i]["TYDESC"].ToString();
                        row["LOCBANKCD"]      = DBNull.Value;
                        row["BJDESC"]         = "계";
                        row["LOCCONTAMT"]     = DBNull.Value;
                        row["LOCCONTDATE"]    = DBNull.Value;
                        row["LOCENDDATE"]     = DBNull.Value;
                        row["LOCCONTRATE"]    = DBNull.Value;
                        row["LOCRATEGB"]      = DBNull.Value;
                        row["LOCCURRCD"]      = DBNull.Value;
                        row["JANAMTY"]        = dLOCLOANTYPETOTALY;
                        row["JANAMTYDOLLAR"]  = dLOCLOANTYPETOTALY_DOLLAR;
                        row["JANAMTD"]        = dLOCLOANTYPETOTALD;
                        row["JANAMTDDOLLAR"]  = dLOCLOANTYPETOTALD_DOLLAR;
                        row["INCAMT"]         = dLOCLOANTYPETOTALI;
                        row["INCAMTDOLLAR"]   = dLOCLOANTYPETOTALI_DOLLAR;

                        Retdt.Rows.Add(row);

                        dLOCLOANTYPETOTALY        = 0;
                        dLOCLOANTYPETOTALY_DOLLAR = 0;                        
                        dLOCLOANTYPETOTALD        = 0;
                        dLOCLOANTYPETOTALD_DOLLAR = 0;
                        dLOCLOANTYPETOTALI        = 0;
                        dLOCLOANTYPETOTALI_DOLLAR = 0;
                    }

                    if ((dt.Rows[i]["LOCCURRTYPE"].ToString() + dt.Rows[i]["LOCGIGANTYPE"].ToString() + dt.Rows[i]["LOCLOAN"].ToString()) !=
                        (dt.Rows[i + 1]["LOCCURRTYPE"].ToString() + dt.Rows[i + 1]["LOCGIGANTYPE"].ToString() + dt.Rows[i + 1]["LOCLOAN"].ToString()))
                    {
                        // 차입용도 계

                        row = Retdt.NewRow();

                        row["LOCCONTNO"]      = dt.Rows[i]["LOCCONTNO"].ToString();
                        //row["LOCCONTSEQ"]     = dt.Rows[i]["LOCCONTSEQ"].ToString();
                        row["LOCCURRTYPE"]    = dt.Rows[i]["LOCCURRTYPE"].ToString();
                        row["CUDESC"]         = dt.Rows[i]["CUDESC"].ToString();
                        row["LOCGIGANTYPE"]   = dt.Rows[i]["LOCGIGANTYPE"].ToString();
                        row["LOCGIGANTYPENM"] = dt.Rows[i]["LOCGIGANTYPENM"].ToString();
                        row["LOCLOAN"]        = dt.Rows[i]["LOCLOAN"].ToString();
                        row["USDESC"]         = dt.Rows[i]["USDESC"].ToString();
                        row["LOCLOANTYPE"]    = DBNull.Value;
                        row["TYDESC"]         = "소 계";
                        row["LOCBANKCD"]      = DBNull.Value;
                        row["BJDESC"]         = DBNull.Value;
                        row["LOCCONTAMT"]     = DBNull.Value;
                        row["LOCCONTDATE"]    = DBNull.Value;
                        row["LOCENDDATE"]     = DBNull.Value;
                        row["LOCCONTRATE"]    = DBNull.Value;
                        row["LOCRATEGB"]      = DBNull.Value;
                        row["LOCCURRCD"]      = DBNull.Value;
                        
                        row["JANAMTY"]        = dLOCLOANTOTALY;
                        row["JANAMTYDOLLAR"]  = dLOCLOANTOTALY_DOLLAR;
                        row["JANAMTD"]        = dLOCLOANTOTALD;
                        row["JANAMTDDOLLAR"]  = dLOCLOANTOTALD_DOLLAR;
                        row["INCAMT"]         = dLOCLOANTOTALI;
                        row["INCAMTDOLLAR"]   = dLOCLOANTOTALI_DOLLAR;

                        Retdt.Rows.Add(row);

                        dLOCLOANTOTALY        = 0;
                        dLOCLOANTOTALD        = 0;
                        dLOCLOANTOTALI        = 0;

                        dLOCLOANTOTALY_DOLLAR = 0;
                        dLOCLOANTOTALD_DOLLAR = 0;
                        dLOCLOANTOTALI_DOLLAR = 0;
                    }

                    if ((dt.Rows[i]["LOCCURRTYPE"].ToString() + dt.Rows[i]["LOCGIGANTYPE"].ToString()) !=
                        (dt.Rows[i + 1]["LOCCURRTYPE"].ToString() + dt.Rows[i + 1]["LOCGIGANTYPE"].ToString()))
                    {
                        // 통화유형 계

                        row = Retdt.NewRow();

                        row["LOCCONTNO"]      = dt.Rows[i]["LOCCONTNO"].ToString();
                        //row["LOCCONTSEQ"]     = dt.Rows[i]["LOCCONTSEQ"].ToString();
                        row["LOCCURRTYPE"]    = dt.Rows[i]["LOCCURRTYPE"].ToString();
                        row["CUDESC"]         = dt.Rows[i]["CUDESC"].ToString();
                        row["LOCGIGANTYPE"]   = DBNull.Value;
                        row["LOCGIGANTYPENM"] = "합 계";
                        row["LOCLOAN"]        = DBNull.Value;
                        row["USDESC"]         = DBNull.Value;
                        row["LOCLOANTYPE"]    = DBNull.Value;
                        row["TYDESC"]         = DBNull.Value;
                        row["LOCBANKCD"]      = DBNull.Value;
                        row["BJDESC"]         = DBNull.Value;
                        row["LOCCONTAMT"]     = DBNull.Value;
                        row["LOCCONTDATE"]    = DBNull.Value;
                        row["LOCENDDATE"]     = DBNull.Value;
                        row["LOCCONTRATE"]    = DBNull.Value;
                        row["LOCRATEGB"]      = DBNull.Value;
                        row["LOCCURRCD"]      = DBNull.Value;

                        row["JANAMTY"]        = dCUDESCTOTALY;
                        row["JANAMTD"]        = dCUDESCTOTALD;
                        row["INCAMT"]         = dCUDESCTOTALI;

                        row["JANAMTYDOLLAR"]  = dCUDESCTOTALY_DOLLAR;
                        row["JANAMTDDOLLAR"]  = dCUDESCTOTALD_DOLLAR;
                        row["INCAMTDOLLAR"]   = dCUDESCTOTALI_DOLLAR;

                        Retdt.Rows.Add(row);

                        dCUDESCTOTALY = 0;
                        dCUDESCTOTALD = 0;
                        dCUDESCTOTALI = 0;

                        dCUDESCTOTALY_DOLLAR = 0;
                        dCUDESCTOTALD_DOLLAR = 0;
                        dCUDESCTOTALI_DOLLAR = 0;
                    }
                }
                else
                {
                    // 차입유형 계

                    row = Retdt.NewRow();

                    row["LOCCONTNO"]      = dt.Rows[i]["LOCCONTNO"].ToString();
                    //row["LOCCONTSEQ"]     = dt.Rows[i]["LOCCONTSEQ"].ToString();
                    row["LOCCURRTYPE"]    = dt.Rows[i]["LOCCURRTYPE"].ToString();
                    row["CUDESC"]         = dt.Rows[i]["CUDESC"].ToString();
                    row["LOCGIGANTYPE"]   = dt.Rows[i]["LOCGIGANTYPE"].ToString();
                    row["LOCGIGANTYPENM"] = dt.Rows[i]["LOCGIGANTYPENM"].ToString();
                    row["LOCLOAN"]        = dt.Rows[i]["LOCLOAN"].ToString();
                    row["USDESC"]         = dt.Rows[i]["USDESC"].ToString();
                    row["LOCLOANTYPE"]    = dt.Rows[i]["LOCLOANTYPE"].ToString();
                    row["TYDESC"]         = dt.Rows[i]["TYDESC"].ToString();
                    row["LOCBANKCD"]      = DBNull.Value;
                    row["BJDESC"]         = "계";
                    row["LOCCONTAMT"]     = DBNull.Value;
                    row["LOCCONTDATE"]    = DBNull.Value;
                    row["LOCENDDATE"]     = DBNull.Value;
                    row["LOCCONTRATE"]    = DBNull.Value;
                    row["LOCRATEGB"]      = DBNull.Value;
                    row["LOCCURRCD"]      = DBNull.Value;

                    row["JANAMTY"]        = dLOCLOANTYPETOTALY;
                    row["JANAMTD"]        = dLOCLOANTYPETOTALD;
                    row["INCAMT"]         = dLOCLOANTYPETOTALI;

                    row["JANAMTYDOLLAR"]  = dLOCLOANTYPETOTALY_DOLLAR;
                    row["JANAMTDDOLLAR"]  = dLOCLOANTYPETOTALD_DOLLAR;
                    row["INCAMTDOLLAR"]   = dLOCLOANTYPETOTALI_DOLLAR;

                    Retdt.Rows.Add(row);

                    dLOCLOANTYPETOTALY        = 0;
                    dLOCLOANTYPETOTALD        = 0;
                    dLOCLOANTYPETOTALI        = 0;

                    dLOCLOANTYPETOTALY_DOLLAR = 0;
                    dLOCLOANTYPETOTALD_DOLLAR = 0;
                    dLOCLOANTYPETOTALI_DOLLAR = 0;

                    // 차입용도 계

                    row = Retdt.NewRow();

                    row["LOCCONTNO"]      = dt.Rows[i]["LOCCONTNO"].ToString();
                    //row["LOCCONTSEQ"]     = dt.Rows[i]["LOCCONTSEQ"].ToString();
                    row["LOCCURRTYPE"]    = dt.Rows[i]["LOCCURRTYPE"].ToString();
                    row["CUDESC"]         = dt.Rows[i]["CUDESC"].ToString();
                    row["LOCGIGANTYPE"]   = dt.Rows[i]["LOCGIGANTYPE"].ToString();
                    row["LOCGIGANTYPENM"] = dt.Rows[i]["LOCGIGANTYPENM"].ToString();
                    row["LOCLOAN"]        = dt.Rows[i]["LOCLOAN"].ToString();
                    row["USDESC"]         = dt.Rows[i]["USDESC"].ToString();
                    row["LOCLOANTYPE"]    = DBNull.Value;
                    row["TYDESC"]         = "소 계";
                    row["LOCBANKCD"]      = DBNull.Value;
                    row["BJDESC"]         = DBNull.Value;
                    row["LOCCONTAMT"]     = DBNull.Value;
                    row["LOCCONTDATE"]    = DBNull.Value;
                    row["LOCENDDATE"]     = DBNull.Value;
                    row["LOCCONTRATE"]    = DBNull.Value;
                    row["LOCRATEGB"]      = DBNull.Value;
                    row["LOCCURRCD"]      = DBNull.Value;

                    row["JANAMTY"]        = dLOCLOANTOTALY;
                    row["JANAMTD"]        = dLOCLOANTOTALD;
                    row["INCAMT"]         = dLOCLOANTOTALI;

                    row["JANAMTYDOLLAR"]  = dLOCLOANTOTALY_DOLLAR;
                    row["JANAMTDDOLLAR"]  = dLOCLOANTOTALD_DOLLAR;
                    row["INCAMTDOLLAR"]   = dLOCLOANTOTALI_DOLLAR;

                    Retdt.Rows.Add(row);

                    dLOCLOANTOTALY        = 0;
                    dLOCLOANTOTALD        = 0;
                    dLOCLOANTOTALI        = 0;

                    dLOCLOANTOTALY_DOLLAR = 0;
                    dLOCLOANTOTALD_DOLLAR = 0;
                    dLOCLOANTOTALI_DOLLAR = 0;

                    // 통화유형 계

                    row = Retdt.NewRow();

                    row["LOCCONTNO"]      = dt.Rows[i]["LOCCONTNO"].ToString();
                    //row["LOCCONTSEQ"]     = dt.Rows[i]["LOCCONTSEQ"].ToString();
                    row["LOCCURRTYPE"]    = dt.Rows[i]["LOCCURRTYPE"].ToString();
                    row["CUDESC"]         = dt.Rows[i]["CUDESC"].ToString();
                    row["LOCGIGANTYPE"]   = DBNull.Value;
                    row["LOCGIGANTYPENM"] = "합 계";
                    row["LOCLOAN"]        = DBNull.Value;
                    row["USDESC"]         = DBNull.Value;
                    row["LOCLOANTYPE"]    = DBNull.Value;
                    row["TYDESC"]         = DBNull.Value;
                    row["LOCBANKCD"]      = DBNull.Value;
                    row["BJDESC"]         = DBNull.Value;
                    row["LOCCONTAMT"]     = DBNull.Value;
                    row["LOCCONTDATE"]    = DBNull.Value;
                    row["LOCENDDATE"]     = DBNull.Value;
                    row["LOCCONTRATE"]    = DBNull.Value;
                    row["LOCRATEGB"]      = DBNull.Value;
                    row["LOCCURRCD"]      = DBNull.Value;

                    row["JANAMTY"]        = dCUDESCTOTALY;
                    row["JANAMTD"]        = dCUDESCTOTALD;
                    row["INCAMT"]         = dCUDESCTOTALI;

                    row["JANAMTYDOLLAR"]  = dCUDESCTOTALY_DOLLAR;
                    row["JANAMTDDOLLAR"]  = dCUDESCTOTALD_DOLLAR;
                    row["INCAMTDOLLAR"]   = dCUDESCTOTALI_DOLLAR;

                    Retdt.Rows.Add(row);

                    dCUDESCTOTALY = 0;
                    dCUDESCTOTALD = 0;
                    dCUDESCTOTALI = 0;

                    dCUDESCTOTALY_DOLLAR = 0;
                    dCUDESCTOTALD_DOLLAR = 0;
                    dCUDESCTOTALI_DOLLAR = 0;

                    // 총 계

                    row = Retdt.NewRow();

                    row["LOCCONTNO"]      = dt.Rows[i]["LOCCONTNO"].ToString();
                    //row["LOCCONTSEQ"]     = dt.Rows[i]["LOCCONTSEQ"].ToString();
                    row["LOCCURRTYPE"]    = DBNull.Value; ;
                    row["CUDESC"]         = "총 계";
                    row["LOCGIGANTYPE"]   = DBNull.Value;
                    row["LOCGIGANTYPENM"] = DBNull.Value;
                    row["LOCLOAN"]        = DBNull.Value;
                    row["USDESC"]         = DBNull.Value;
                    row["LOCLOANTYPE"]    = DBNull.Value;
                    row["TYDESC"]         = DBNull.Value;
                    row["LOCBANKCD"]      = DBNull.Value;
                    row["BJDESC"]         = DBNull.Value;
                    row["LOCCONTAMT"]     = DBNull.Value;
                    row["LOCCONTDATE"]    = DBNull.Value;
                    row["LOCENDDATE"]     = DBNull.Value;
                    row["LOCCONTRATE"]    = DBNull.Value;
                    row["LOCRATEGB"]      = DBNull.Value;
                    row["LOCCURRCD"]      = DBNull.Value;

                    row["JANAMTY"]        = dTOTALY;
                    row["JANAMTD"]        = dTOTALD;
                    row["INCAMT"]         = dTOTALI;

                    row["JANAMTYDOLLAR"]  = dTOTALY_DOLLAR;
                    row["JANAMTDDOLLAR"]  = dTOTALD_DOLLAR;
                    row["INCAMTDOLLAR"]   = dTOTALI_DOLLAR;

                    Retdt.Rows.Add(row);

                    dTOTALY        = 0;
                    dTOTALD        = 0;
                    dTOTALI        = 0;

                    dTOTALY_DOLLAR = 0;
                    dTOTALD_DOLLAR = 0;
                    dTOTALI_DOLLAR = 0;
                }
            }

            return Retdt;
        }
        #endregion

        #region Description : 스프레드 로드
        private void UP_Spread_Title()
        {
            this.FPS91_TY_S_AC_873I8318_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_873I8318_Sheet1.RowHeaderColumnCount = 2;

            this.FPS91_TY_S_AC_873I8318_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 4);
            this.FPS91_TY_S_AC_873I8318_Sheet1.AddColumnHeaderSpanCell(0, 11, 1, 2);
            this.FPS91_TY_S_AC_873I8318_Sheet1.AddColumnHeaderSpanCell(0, 13, 1, 2);
            this.FPS91_TY_S_AC_873I8318_Sheet1.AddColumnHeaderSpanCell(0, 15, 1, 2);


            this.FPS91_TY_S_AC_873I8318_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1);
            this.FPS91_TY_S_AC_873I8318_Sheet1.AddColumnHeaderSpanCell(0, 5, 2, 1);
            this.FPS91_TY_S_AC_873I8318_Sheet1.AddColumnHeaderSpanCell(0, 6, 2, 1);
            this.FPS91_TY_S_AC_873I8318_Sheet1.AddColumnHeaderSpanCell(0, 7, 2, 1);
            this.FPS91_TY_S_AC_873I8318_Sheet1.AddColumnHeaderSpanCell(0, 8, 2, 1);
            this.FPS91_TY_S_AC_873I8318_Sheet1.AddColumnHeaderSpanCell(0, 9, 2, 1);
            this.FPS91_TY_S_AC_873I8318_Sheet1.AddColumnHeaderSpanCell(0, 10, 2, 1);

            this.FPS91_TY_S_AC_873I8318_Sheet1.ColumnHeader.Cells[0, 0].Value  = "대출과목 유형";
            this.FPS91_TY_S_AC_873I8318_Sheet1.ColumnHeader.Cells[0, 4].Value  = "은행명";
            this.FPS91_TY_S_AC_873I8318_Sheet1.ColumnHeader.Cells[0, 5].Value  = "대출액(약정)";
            this.FPS91_TY_S_AC_873I8318_Sheet1.ColumnHeader.Cells[0, 6].Value  = "약정일";
            this.FPS91_TY_S_AC_873I8318_Sheet1.ColumnHeader.Cells[0, 7].Value  = "만기일";
            this.FPS91_TY_S_AC_873I8318_Sheet1.ColumnHeader.Cells[0, 8].Value  = "이자율";
            this.FPS91_TY_S_AC_873I8318_Sheet1.ColumnHeader.Cells[0, 9].Value  = "상태";
            this.FPS91_TY_S_AC_873I8318_Sheet1.ColumnHeader.Cells[0, 10].Value = "화폐구분";

            this.FPS91_TY_S_AC_873I8318_Sheet1.ColumnHeader.Cells[0, 11].Value = "전년도 년말 잔액";
            this.FPS91_TY_S_AC_873I8318_Sheet1.ColumnHeader.Cells[0, 13].Value = "현재 잔액";
            this.FPS91_TY_S_AC_873I8318_Sheet1.ColumnHeader.Cells[0, 15].Value = "증감액";

            this.FPS91_TY_S_AC_873I8318_Sheet1.ColumnHeader.Cells[1, 0].Value  = "";
            this.FPS91_TY_S_AC_873I8318_Sheet1.ColumnHeader.Cells[1, 1].Value  = "";
            this.FPS91_TY_S_AC_873I8318_Sheet1.ColumnHeader.Cells[1, 2].Value  = "";
            this.FPS91_TY_S_AC_873I8318_Sheet1.ColumnHeader.Cells[1, 3].Value  = "";
            this.FPS91_TY_S_AC_873I8318_Sheet1.ColumnHeader.Cells[1, 4].Value  = "";
            this.FPS91_TY_S_AC_873I8318_Sheet1.ColumnHeader.Cells[1, 5].Value  = "";
            this.FPS91_TY_S_AC_873I8318_Sheet1.ColumnHeader.Cells[1, 6].Value  = "";
            this.FPS91_TY_S_AC_873I8318_Sheet1.ColumnHeader.Cells[1, 7].Value  = "";
            this.FPS91_TY_S_AC_873I8318_Sheet1.ColumnHeader.Cells[1, 8].Value  = "";
            this.FPS91_TY_S_AC_873I8318_Sheet1.ColumnHeader.Cells[1, 9].Value  = "";
            this.FPS91_TY_S_AC_873I8318_Sheet1.ColumnHeader.Cells[1, 10].Value = "";

            this.FPS91_TY_S_AC_873I8318_Sheet1.ColumnHeader.Cells[1, 11].Value = "원화";
            this.FPS91_TY_S_AC_873I8318_Sheet1.ColumnHeader.Cells[1, 12].Value = "외화";
            this.FPS91_TY_S_AC_873I8318_Sheet1.ColumnHeader.Cells[1, 13].Value = "원화";
            this.FPS91_TY_S_AC_873I8318_Sheet1.ColumnHeader.Cells[1, 14].Value = "외화";
            this.FPS91_TY_S_AC_873I8318_Sheet1.ColumnHeader.Cells[1, 15].Value = "원화";
            this.FPS91_TY_S_AC_873I8318_Sheet1.ColumnHeader.Cells[1, 16].Value = "외화";

            this.FPS91_TY_S_AC_873I8318_Sheet1.ColumnHeader.Cells[0, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_873I8318_Sheet1.ColumnHeader.Cells[0, 13].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_873I8318_Sheet1.ColumnHeader.Cells[0, 15].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion
    }
}
