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
    /// 차입금 잔액조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2018.07.03 13:11
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_873I5320 : 차입금 잔액조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_873I8318 : 차입금 잔액조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  LOCCURRTYPE : 통화유형
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYACLO003S : TYBase
    {
        #region Description : 폼 로드
        public TYACLO003S()
        {
            InitializeComponent();
        }

        private void TYACLO003S_Load(object sender, System.EventArgs e)
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

            string sYYMMDD = (Convert.ToDouble(this.DTP01_STDATE.GetString().Substring(0,4)) - 1).ToString() + "1231";
            this.DbConnector.CommandClear();

            // 20180827 수정전 소스
            //this.DbConnector.Attach("TY_P_AC_873I5320", sYYMMDD,
            //                                            this.DTP01_STDATE.GetString(),
            //                                            this.CBH01_LOCCURRTYPE.GetValue().ToString());

            // 20190430 수정전 소스
            //if (int.Parse(sYYMMDD.ToString().ToString().Substring(0, 4)) < 2019)
            //{
            //    sProcedure = "TY_P_AC_897DA696";
            //}
            //else
            //{
            //    sProcedure = "TY_P_AC_88RH8642";
            //}


            // 20190430 수정후 소스
            //sProcedure = "TY_P_AC_94UA9486";

            // 20210312 수정후 소스(이성재 요청)
            // 유동성 재대체일 경우 재대체 금액을 그전 계약금액에서 빼고, 현재 계약에서 나오게 해야 함
            //sProcedure = "TY_P_AC_B3CBI931";
            //this.DbConnector.Attach(sProcedure.ToString(), sYYMMDD,
            //                                               this.DTP01_STDATE.GetString(),
            //                                               this.CBH01_LOCCURRTYPE.GetValue().ToString());

            // 20221222 수정 전 소스
            // 20210323 수정(잔액 구분 추가)
            //if(this.CBO01_GGUBUN.GetValue().ToString() == "A")
            //{
            //    sProcedure = "TY_P_AC_B3GFA984";
            //}
            //else if (this.CBO01_GGUBUN.GetValue().ToString() == "O")
            //{
            //    sProcedure = "TY_P_AC_B3N9R022";
            //}
            //else if (this.CBO01_GGUBUN.GetValue().ToString() == "X")
            //{
            //    sProcedure = "TY_P_AC_B3N9U023";
            //}

            //this.DbConnector.Attach(sProcedure.ToString(), sYYMMDD,
            //                                               this.DTP01_STDATE.GetString(),
            //                                               this.DTP01_STDATE.GetString(),
            //                                               this.CBH01_LOCCURRTYPE.GetValue().ToString());

            // 20221222 수정 후 소스
            if (this.CBO01_GGUBUN.GetValue().ToString() == "A")
            {
                sProcedure = "TY_P_AC_CCM9T419";
            }
            else if (this.CBO01_GGUBUN.GetValue().ToString() == "O")
            {
                sProcedure = "TY_P_AC_CCMHA426";
            }
            else if (this.CBO01_GGUBUN.GetValue().ToString() == "X")
            {
                sProcedure = "TY_P_AC_CCMHA427";
            }
            
            this.DbConnector.Attach(sProcedure.ToString(), sYYMMDD,
                                                           this.DTP01_STDATE.GetString(),
                                                           this.CBH01_LOCCURRTYPE.GetValue().ToString());
            

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_88AD7528.SetValue(UP_DataChange(dt));


            for (int i = 0; i < this.FPS91_TY_S_AC_88AD7528.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_88AD7528.GetValue(i, "BJDESC").ToString() == "계" || this.FPS91_TY_S_AC_88AD7528.GetValue(i, "TYDESC").ToString() == "소 계" ||
                    this.FPS91_TY_S_AC_88AD7528.GetValue(i, "LOCGIGANTYPENM").ToString() == "합 계" || this.FPS91_TY_S_AC_88AD7528.GetValue(i, "CUDESC").ToString() == "총 계")
                {
                    // 특정 ROW 글자 크기 변경
                    this.FPS91_TY_S_AC_88AD7528.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);
                }

                if (this.FPS91_TY_S_AC_88AD7528.GetValue(i, "LOCGIGANTYPENM").ToString() == "합 계")
                {
                    // 특정 ROW 색깔 변경
                    this.FPS91_TY_S_AC_88AD7528.ActiveSheet.Rows[i].BackColor = Color.YellowGreen;
                }

                if (this.FPS91_TY_S_AC_88AD7528.GetValue(i, "CUDESC").ToString() == "총 계")
                {
                    // 특정 ROW 색깔 변경
                    this.FPS91_TY_S_AC_88AD7528.ActiveSheet.Rows[i].BackColor = Color.ForestGreen;
                }
            }

            //if (this.FPS91_TY_S_AC_2AU3I922.CurrentRowCount > 0)
            //{
            //    this.SpreadSumRowAdd(this.FPS91_TY_S_AC_2AU3I922, "B1CDBKNM", "합   계", SumRowType.Sum, "B1AMIO");
            //}

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
            //    sProcedure = "TY_P_AC_897DA696";
            //}
            //else
            //{
            //    sProcedure = "TY_P_AC_88RH8642";
            //}

            // 20190430 수정후 소스
            //sProcedure = "TY_P_AC_94UA9486";

            // 20210312 수정후 소스(이성재 요청)
            // 유동성 재대체일 경우 재대체 금액을 그전 계약금액에서 빼고, 현재 계약에서 나오게 해야 함
            //sProcedure = "TY_P_AC_B3CBI931";

            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach
            //    (
            //    sProcedure.ToString(),
            //    sYYMMDD,
            //    this.DTP01_STDATE.GetString(),
            //    this.CBH01_LOCCURRTYPE.GetValue()
            //    );

            // 20221222 수정 전 소스
            //// 20210323 수정(잔액 구분 추가)
            //if (this.CBO01_GGUBUN.GetValue().ToString() == "A")
            //{
            //    sProcedure = "TY_P_AC_B3GFA984";
            //}
            //else if (this.CBO01_GGUBUN.GetValue().ToString() == "O")
            //{
            //    sProcedure = "TY_P_AC_B3N9R022";
            //}
            //else if (this.CBO01_GGUBUN.GetValue().ToString() == "X")
            //{
            //    sProcedure = "TY_P_AC_B3N9U023";
            //}

            //this.DbConnector.Attach(sProcedure.ToString(), sYYMMDD,
            //                                               this.DTP01_STDATE.GetString(),
            //                                               this.DTP01_STDATE.GetString(),
            //                                               this.CBH01_LOCCURRTYPE.GetValue().ToString());

            // 20221222 수정 후 소스
            if (this.CBO01_GGUBUN.GetValue().ToString() == "A")
            {
                sProcedure = "TY_P_AC_CCM9T419";
            }
            else if (this.CBO01_GGUBUN.GetValue().ToString() == "O")
            {
                sProcedure = "TY_P_AC_CCMHA426";
            }
            else if (this.CBO01_GGUBUN.GetValue().ToString() == "X")
            {
                sProcedure = "TY_P_AC_CCMHA427";
            }

            this.DbConnector.Attach(sProcedure.ToString(), sYYMMDD,
                                                           this.DTP01_STDATE.GetString(),
                                                           this.CBH01_LOCCURRTYPE.GetValue().ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYACLO003R(sDATE);

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

        #region Description : 데이터셋 변환
        private DataTable UP_DataChange(DataTable dt)
        {
            double dLOCLOANTYPETOTALY = 0;   // 차입유형 계 (전년도)
            double dLOCLOANTYPETOTALD = 0;   // 차입유형 계 (현재)
            double dLOCLOANTYPETOTALI = 0;   // 차입유형 계 (증감)
            double dLOCLOANTOTALY = 0;       // 차입용도 계 (전년도)
            double dLOCLOANTOTALD = 0;       // 차입용도 계 (현재)
            double dLOCLOANTOTALI = 0;       // 차입용도 계 (증감)
            double dCUDESCTOTALY = 0;        // 통화유형 계 (전년도)
            double dCUDESCTOTALD = 0;        // 통화유형 계 (현재)
            double dCUDESCTOTALI = 0;        // 통화유형 계 (증감)
            double dTOTALY = 0;              // 총 계 (전년도)
            double dTOTALD = 0;              // 총 계 (현재)
            double dTOTALI = 0;              // 총 계 (증감)

            DataTable Retdt = new DataTable();
            DataRow row;

            Retdt.Columns.Add("LOCCONTNO", typeof(System.String));
            //Retdt.Columns.Add("LOCCONTSEQ", typeof(System.String));
            Retdt.Columns.Add("LOCCURRTYPE", typeof(System.String));
            Retdt.Columns.Add("CUDESC", typeof(System.String));
            Retdt.Columns.Add("LOCGIGANTYPE", typeof(System.String));
            Retdt.Columns.Add("LOCGIGANTYPENM", typeof(System.String));
            Retdt.Columns.Add("LOCLOAN", typeof(System.String));
            Retdt.Columns.Add("USDESC", typeof(System.String));
            Retdt.Columns.Add("LOCLOANTYPE", typeof(System.String));
            Retdt.Columns.Add("TYDESC", typeof(System.String));
            Retdt.Columns.Add("LOCBANKCD", typeof(System.String));
            Retdt.Columns.Add("BJDESC", typeof(System.String));
            Retdt.Columns.Add("LOCCONTAMT", typeof(System.String));
            Retdt.Columns.Add("LOCCONTDATE", typeof(System.String));
            Retdt.Columns.Add("LOCENDDATE", typeof(System.String));
            Retdt.Columns.Add("LOCCONTRATE", typeof(System.String));
            Retdt.Columns.Add("LOCRATEGB", typeof(System.String));
            Retdt.Columns.Add("JANAMTY", typeof(System.String));
            Retdt.Columns.Add("JANAMTD", typeof(System.String));
            Retdt.Columns.Add("INCAMT", typeof(System.String));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row = Retdt.NewRow();

                row["LOCCONTNO"] = dt.Rows[i]["LOCCONTNO"].ToString();
                //row["LOCCONTSEQ"] = dt.Rows[i]["LOCCONTSEQ"].ToString();
                row["LOCCURRTYPE"] = dt.Rows[i]["LOCCURRTYPE"].ToString();
                row["CUDESC"] = dt.Rows[i]["CUDESC"].ToString();
                row["LOCGIGANTYPE"] = dt.Rows[i]["LOCGIGANTYPE"].ToString();
                row["LOCGIGANTYPENM"] = dt.Rows[i]["LOCGIGANTYPENM"].ToString();
                row["LOCLOAN"] = dt.Rows[i]["LOCLOAN"].ToString();
                row["USDESC"] = dt.Rows[i]["USDESC"].ToString();
                row["LOCLOANTYPE"] = dt.Rows[i]["LOCLOANTYPE"].ToString();
                row["TYDESC"] = dt.Rows[i]["TYDESC"].ToString();
                row["LOCBANKCD"] = dt.Rows[i]["LOCBANKCD"].ToString();
                row["BJDESC"] = dt.Rows[i]["BJDESC"].ToString();
                row["LOCCONTAMT"] = dt.Rows[i]["LOCCONTAMT"].ToString();
                row["LOCCONTDATE"] = dt.Rows[i]["LOCCONTDATE"].ToString();
                row["LOCENDDATE"] = dt.Rows[i]["LOCENDDATE"].ToString();
                row["LOCCONTRATE"] = dt.Rows[i]["LOCCONTRATE"].ToString();
                row["LOCRATEGB"] = dt.Rows[i]["LOCRATEGB"].ToString();
                row["JANAMTY"] = dt.Rows[i]["JANAMTY"].ToString();
                row["JANAMTD"] = dt.Rows[i]["JANAMTD"].ToString();
                row["INCAMT"] = dt.Rows[i]["INCAMT"].ToString();

                Retdt.Rows.Add(row);

                dLOCLOANTYPETOTALY += Convert.ToDouble(dt.Rows[i]["JANAMTY"].ToString());
                dLOCLOANTYPETOTALD += Convert.ToDouble(dt.Rows[i]["JANAMTD"].ToString());
                dLOCLOANTYPETOTALI += Convert.ToDouble(dt.Rows[i]["INCAMT"].ToString());
                dLOCLOANTOTALY += Convert.ToDouble(dt.Rows[i]["JANAMTY"].ToString());
                dLOCLOANTOTALD += Convert.ToDouble(dt.Rows[i]["JANAMTD"].ToString());
                dLOCLOANTOTALI += Convert.ToDouble(dt.Rows[i]["INCAMT"].ToString());
                dCUDESCTOTALY += Convert.ToDouble(dt.Rows[i]["JANAMTY"].ToString());
                dCUDESCTOTALD += Convert.ToDouble(dt.Rows[i]["JANAMTD"].ToString());
                dCUDESCTOTALI += Convert.ToDouble(dt.Rows[i]["INCAMT"].ToString());
                dTOTALY += Convert.ToDouble(dt.Rows[i]["JANAMTY"].ToString());
                dTOTALD += Convert.ToDouble(dt.Rows[i]["JANAMTD"].ToString());
                dTOTALI += Convert.ToDouble(dt.Rows[i]["INCAMT"].ToString());




                if ((i + 1) < dt.Rows.Count)
                {
                    if ((dt.Rows[i]["LOCCURRTYPE"].ToString() + dt.Rows[i]["LOCGIGANTYPE"].ToString() + dt.Rows[i]["LOCLOAN"].ToString() + dt.Rows[i]["LOCLOANTYPE"].ToString()) !=
                        (dt.Rows[i + 1]["LOCCURRTYPE"].ToString() + dt.Rows[i + 1]["LOCGIGANTYPE"].ToString() + dt.Rows[i + 1]["LOCLOAN"].ToString() + dt.Rows[i + 1]["LOCLOANTYPE"].ToString()))
                    {
                        // 차입유형 계

                        row = Retdt.NewRow();

                        row["LOCCONTNO"] = dt.Rows[i]["LOCCONTNO"].ToString();
                        //row["LOCCONTSEQ"] = dt.Rows[i]["LOCCONTSEQ"].ToString();
                        row["LOCCURRTYPE"] = dt.Rows[i]["LOCCURRTYPE"].ToString();
                        row["CUDESC"] = dt.Rows[i]["CUDESC"].ToString();
                        row["LOCGIGANTYPE"] = dt.Rows[i]["LOCGIGANTYPE"].ToString();
                        row["LOCGIGANTYPENM"] = dt.Rows[i]["LOCGIGANTYPENM"].ToString();
                        row["LOCLOAN"] = dt.Rows[i]["LOCLOAN"].ToString();
                        row["USDESC"] = dt.Rows[i]["USDESC"].ToString();
                        row["LOCLOANTYPE"] = dt.Rows[i]["LOCLOANTYPE"].ToString();
                        row["TYDESC"] = dt.Rows[i]["TYDESC"].ToString();
                        row["LOCBANKCD"] = DBNull.Value;
                        row["BJDESC"] = "계";
                        row["LOCCONTAMT"] = DBNull.Value;
                        row["LOCCONTDATE"] = DBNull.Value;
                        row["LOCENDDATE"] = DBNull.Value;
                        row["LOCCONTRATE"] = DBNull.Value;
                        row["LOCRATEGB"] = DBNull.Value;
                        row["JANAMTY"] = dLOCLOANTYPETOTALY;
                        row["JANAMTD"] = dLOCLOANTYPETOTALD;
                        row["INCAMT"] = dLOCLOANTYPETOTALI;

                        Retdt.Rows.Add(row);

                        dLOCLOANTYPETOTALY = 0;
                        dLOCLOANTYPETOTALD = 0;
                        dLOCLOANTYPETOTALI = 0;
                    }

                    if ((dt.Rows[i]["LOCCURRTYPE"].ToString() + dt.Rows[i]["LOCGIGANTYPE"].ToString() + dt.Rows[i]["LOCLOAN"].ToString()) !=
                        (dt.Rows[i + 1]["LOCCURRTYPE"].ToString() + dt.Rows[i + 1]["LOCGIGANTYPE"].ToString() + dt.Rows[i + 1]["LOCLOAN"].ToString()))
                    {
                        // 차입용도 계

                        row = Retdt.NewRow();

                        row["LOCCONTNO"] = dt.Rows[i]["LOCCONTNO"].ToString();
                        //row["LOCCONTSEQ"] = dt.Rows[i]["LOCCONTSEQ"].ToString();
                        row["LOCCURRTYPE"] = dt.Rows[i]["LOCCURRTYPE"].ToString();
                        row["CUDESC"] = dt.Rows[i]["CUDESC"].ToString();
                        row["LOCGIGANTYPE"] = dt.Rows[i]["LOCGIGANTYPE"].ToString();
                        row["LOCGIGANTYPENM"] = dt.Rows[i]["LOCGIGANTYPENM"].ToString();
                        row["LOCLOAN"] = dt.Rows[i]["LOCLOAN"].ToString();
                        row["USDESC"] = dt.Rows[i]["USDESC"].ToString();
                        row["LOCLOANTYPE"] = DBNull.Value;
                        row["TYDESC"] = "소 계"; 
                        row["LOCBANKCD"] = DBNull.Value;
                        row["BJDESC"] = DBNull.Value;
                        row["LOCCONTAMT"] = DBNull.Value;
                        row["LOCCONTDATE"] = DBNull.Value;
                        row["LOCENDDATE"] = DBNull.Value;
                        row["LOCCONTRATE"] = DBNull.Value;
                        row["LOCRATEGB"] = DBNull.Value;
                        row["JANAMTY"] = dLOCLOANTOTALY;
                        row["JANAMTD"] = dLOCLOANTOTALD;
                        row["INCAMT"] = dLOCLOANTOTALI;

                        Retdt.Rows.Add(row);

                        dLOCLOANTOTALY = 0;
                        dLOCLOANTOTALD = 0;
                        dLOCLOANTOTALI = 0;
                    }

                    if ((dt.Rows[i]["LOCCURRTYPE"].ToString() + dt.Rows[i]["LOCGIGANTYPE"].ToString()) !=
                        (dt.Rows[i + 1]["LOCCURRTYPE"].ToString() + dt.Rows[i + 1]["LOCGIGANTYPE"].ToString()))
                    {
                        // 통화유형 계

                        row = Retdt.NewRow();

                        row["LOCCONTNO"] = dt.Rows[i]["LOCCONTNO"].ToString();
                        //row["LOCCONTSEQ"] = dt.Rows[i]["LOCCONTSEQ"].ToString();
                        row["LOCCURRTYPE"] = dt.Rows[i]["LOCCURRTYPE"].ToString();
                        row["CUDESC"] = dt.Rows[i]["CUDESC"].ToString();
                        row["LOCGIGANTYPE"] = DBNull.Value;
                        row["LOCGIGANTYPENM"] = "합 계"; 
                        row["LOCLOAN"] = DBNull.Value;
                        row["USDESC"] = DBNull.Value;
                        row["LOCLOANTYPE"] = DBNull.Value;
                        row["TYDESC"] = DBNull.Value;
                        row["LOCBANKCD"] = DBNull.Value;
                        row["BJDESC"] = DBNull.Value;
                        row["LOCCONTAMT"] = DBNull.Value;
                        row["LOCCONTDATE"] = DBNull.Value;
                        row["LOCENDDATE"] = DBNull.Value;
                        row["LOCCONTRATE"] = DBNull.Value;
                        row["LOCRATEGB"] = DBNull.Value;
                        row["JANAMTY"] = dCUDESCTOTALY;
                        row["JANAMTD"] = dCUDESCTOTALD;
                        row["INCAMT"] = dCUDESCTOTALI;

                        Retdt.Rows.Add(row);

                        dCUDESCTOTALY = 0;
                        dCUDESCTOTALD = 0;
                        dCUDESCTOTALI = 0;
                    }
                }
                else
                {
                    // 차입유형 계

                    row = Retdt.NewRow();

                    row["LOCCONTNO"] = dt.Rows[i]["LOCCONTNO"].ToString();
                    //row["LOCCONTSEQ"] = dt.Rows[i]["LOCCONTSEQ"].ToString();
                    row["LOCCURRTYPE"] = dt.Rows[i]["LOCCURRTYPE"].ToString();
                    row["CUDESC"] = dt.Rows[i]["CUDESC"].ToString();
                    row["LOCGIGANTYPE"] = dt.Rows[i]["LOCGIGANTYPE"].ToString();
                    row["LOCGIGANTYPENM"] = dt.Rows[i]["LOCGIGANTYPENM"].ToString();
                    row["LOCLOAN"] = dt.Rows[i]["LOCLOAN"].ToString();
                    row["USDESC"] = dt.Rows[i]["USDESC"].ToString();
                    row["LOCLOANTYPE"] = dt.Rows[i]["LOCLOANTYPE"].ToString();
                    row["TYDESC"] = dt.Rows[i]["TYDESC"].ToString();
                    row["LOCBANKCD"] = DBNull.Value;
                    row["BJDESC"] = "계";
                    row["LOCCONTAMT"] = DBNull.Value;
                    row["LOCCONTDATE"] = DBNull.Value;
                    row["LOCENDDATE"] = DBNull.Value;
                    row["LOCCONTRATE"] = DBNull.Value;
                    row["LOCRATEGB"] = DBNull.Value;
                    row["JANAMTY"] = dLOCLOANTYPETOTALY;
                    row["JANAMTD"] = dLOCLOANTYPETOTALD;
                    row["INCAMT"] = dLOCLOANTYPETOTALI;

                    Retdt.Rows.Add(row);

                    dLOCLOANTYPETOTALY = 0;
                    dLOCLOANTYPETOTALD = 0;
                    dLOCLOANTYPETOTALI = 0;

                    // 차입용도 계

                    row = Retdt.NewRow();

                    row["LOCCONTNO"] = dt.Rows[i]["LOCCONTNO"].ToString();
                    //row["LOCCONTSEQ"] = dt.Rows[i]["LOCCONTSEQ"].ToString();
                    row["LOCCURRTYPE"] = dt.Rows[i]["LOCCURRTYPE"].ToString();
                    row["CUDESC"] = dt.Rows[i]["CUDESC"].ToString();
                    row["LOCGIGANTYPE"] = dt.Rows[i]["LOCGIGANTYPE"].ToString();
                    row["LOCGIGANTYPENM"] = dt.Rows[i]["LOCGIGANTYPENM"].ToString();
                    row["LOCLOAN"] = dt.Rows[i]["LOCLOAN"].ToString();
                    row["USDESC"] = dt.Rows[i]["USDESC"].ToString();
                    row["LOCLOANTYPE"] = DBNull.Value;
                    row["TYDESC"] = "소 계";
                    row["LOCBANKCD"] = DBNull.Value;
                    row["BJDESC"] = DBNull.Value;
                    row["LOCCONTAMT"] = DBNull.Value;
                    row["LOCCONTDATE"] = DBNull.Value;
                    row["LOCENDDATE"] = DBNull.Value;
                    row["LOCCONTRATE"] = DBNull.Value;
                    row["LOCRATEGB"] = DBNull.Value;
                    row["JANAMTY"] = dLOCLOANTOTALY;
                    row["JANAMTD"] = dLOCLOANTOTALD;
                    row["INCAMT"] = dLOCLOANTOTALI;

                    Retdt.Rows.Add(row);

                    dLOCLOANTOTALY = 0;
                    dLOCLOANTOTALD = 0;
                    dLOCLOANTOTALI = 0;

                    // 통화유형 계

                    row = Retdt.NewRow();

                    row["LOCCONTNO"] = dt.Rows[i]["LOCCONTNO"].ToString();
                    //row["LOCCONTSEQ"] = dt.Rows[i]["LOCCONTSEQ"].ToString();
                    row["LOCCURRTYPE"] = dt.Rows[i]["LOCCURRTYPE"].ToString();
                    row["CUDESC"] = dt.Rows[i]["CUDESC"].ToString();
                    row["LOCGIGANTYPE"] = DBNull.Value;
                    row["LOCGIGANTYPENM"] = "합 계";
                    row["LOCLOAN"] = DBNull.Value;
                    row["USDESC"] = DBNull.Value;
                    row["LOCLOANTYPE"] = DBNull.Value;
                    row["TYDESC"] = DBNull.Value;
                    row["LOCBANKCD"] = DBNull.Value;
                    row["BJDESC"] = DBNull.Value;
                    row["LOCCONTAMT"] = DBNull.Value;
                    row["LOCCONTDATE"] = DBNull.Value;
                    row["LOCENDDATE"] = DBNull.Value;
                    row["LOCCONTRATE"] = DBNull.Value;
                    row["LOCRATEGB"] = DBNull.Value;
                    row["JANAMTY"] = dCUDESCTOTALY;
                    row["JANAMTD"] = dCUDESCTOTALD;
                    row["INCAMT"] = dCUDESCTOTALI;

                    Retdt.Rows.Add(row);

                    dCUDESCTOTALY = 0;
                    dCUDESCTOTALD = 0;
                    dCUDESCTOTALI = 0;

                    // 총 계

                    row = Retdt.NewRow();

                    row["LOCCONTNO"] = dt.Rows[i]["LOCCONTNO"].ToString();
                    //row["LOCCONTSEQ"] = dt.Rows[i]["LOCCONTSEQ"].ToString();
                    row["LOCCURRTYPE"] = DBNull.Value; ;
                    row["CUDESC"] = "총 계"; 
                    row["LOCGIGANTYPE"] = DBNull.Value;
                    row["LOCGIGANTYPENM"] = DBNull.Value;
                    row["LOCLOAN"] = DBNull.Value;
                    row["USDESC"] = DBNull.Value;
                    row["LOCLOANTYPE"] = DBNull.Value;
                    row["TYDESC"] = DBNull.Value;
                    row["LOCBANKCD"] = DBNull.Value;
                    row["BJDESC"] = DBNull.Value;
                    row["LOCCONTAMT"] = DBNull.Value;
                    row["LOCCONTDATE"] = DBNull.Value;
                    row["LOCENDDATE"] = DBNull.Value;
                    row["LOCCONTRATE"] = DBNull.Value;
                    row["LOCRATEGB"] = DBNull.Value;
                    row["JANAMTY"] = dTOTALY;
                    row["JANAMTD"] = dTOTALD;
                    row["INCAMT"] = dTOTALI;

                    Retdt.Rows.Add(row);

                    dTOTALY = 0;
                    dTOTALD = 0;
                    dTOTALI = 0;
                }
            }

            return Retdt;
        }
        #endregion

        #region Description : 스프레드 로드
        private void UP_Spread_Title()
        {
            this.FPS91_TY_S_AC_88AD7528_Sheet1.ColumnHeaderRowCount = 1;
            this.FPS91_TY_S_AC_88AD7528_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_88AD7528_Sheet1.AddColumnHeaderSpanCell(0, 0, 1, 4);

            this.FPS91_TY_S_AC_88AD7528_Sheet1.AddColumnHeaderSpanCell(0, 4, 1, 1);
            this.FPS91_TY_S_AC_88AD7528_Sheet1.AddColumnHeaderSpanCell(0, 5, 1, 1);
            this.FPS91_TY_S_AC_88AD7528_Sheet1.AddColumnHeaderSpanCell(0, 6, 1, 1);
            this.FPS91_TY_S_AC_88AD7528_Sheet1.AddColumnHeaderSpanCell(0, 7, 1, 1);
            this.FPS91_TY_S_AC_88AD7528_Sheet1.AddColumnHeaderSpanCell(0, 8, 1, 1);
            this.FPS91_TY_S_AC_88AD7528_Sheet1.AddColumnHeaderSpanCell(0, 9, 1, 1);
            this.FPS91_TY_S_AC_88AD7528_Sheet1.AddColumnHeaderSpanCell(0, 10, 1, 1);
            this.FPS91_TY_S_AC_88AD7528_Sheet1.AddColumnHeaderSpanCell(0, 11, 1, 1);
            this.FPS91_TY_S_AC_88AD7528_Sheet1.AddColumnHeaderSpanCell(0, 12, 1, 1);

            this.FPS91_TY_S_AC_88AD7528_Sheet1.ColumnHeader.Cells[0, 0].Value = "대출과목 유형";
            this.FPS91_TY_S_AC_88AD7528_Sheet1.ColumnHeader.Cells[0, 4].Value = "은행명";
            this.FPS91_TY_S_AC_88AD7528_Sheet1.ColumnHeader.Cells[0, 5].Value = "대출액(약정)";
            this.FPS91_TY_S_AC_88AD7528_Sheet1.ColumnHeader.Cells[0, 6].Value = "약정일";
            this.FPS91_TY_S_AC_88AD7528_Sheet1.ColumnHeader.Cells[0, 7].Value = "만기일";
            this.FPS91_TY_S_AC_88AD7528_Sheet1.ColumnHeader.Cells[0, 8].Value = "이자율";
            this.FPS91_TY_S_AC_88AD7528_Sheet1.ColumnHeader.Cells[0, 9].Value = "상태";
            this.FPS91_TY_S_AC_88AD7528_Sheet1.ColumnHeader.Cells[0, 10].Value = "전년도 년말 잔액";
            this.FPS91_TY_S_AC_88AD7528_Sheet1.ColumnHeader.Cells[0, 11].Value = "현재 잔액";
            this.FPS91_TY_S_AC_88AD7528_Sheet1.ColumnHeader.Cells[0, 12].Value = "증감액";
        }
        #endregion
    }
}
