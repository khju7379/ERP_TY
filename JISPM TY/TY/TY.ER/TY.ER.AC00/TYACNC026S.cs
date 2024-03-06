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
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using FarPoint.Win.Spread.CellType;

namespace TY.ER.AC00
{
    /// <summary>
    /// 영업비용명세서 출력 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.09.18 10:09
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_29I3V175 : 영업비용명세서 출력
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_29I5D178 : 영업비용명세서 출력
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_AC_29I6V182 : 부서코드를 입력하세요.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  GCDDP : 사업장코드
    ///  MMCDDP : 예산부서
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACNC026S : TYBase
    {
        #region Description : 페이지 로드
        public TYACNC026S()
        {
            InitializeComponent();
        }

        private void TYACNC026S_Load(object sender, System.EventArgs e)
        {
            UP_Spread_Load();

            this.CBH01_GCDDP.DummyValue = DateTime.Now.ToString("yyyyMMdd");
            
            SetStartingFocus(this.DTP01_GSTYYMM);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sCDDP = string.Empty;
            string sProCedureID = string.Empty;

            this.FPS91_TY_S_AC_29I5D178.Initialize();

            if (this.CBO01_MMCDDP.GetValue().ToString() == "" && this.CBH01_GCDDP.GetValue().ToString() == "")
            {
                this.ShowMessage("TY_M_AC_29I6V182");
                return;
            }
            else if(this.CBO01_MMCDDP.GetValue().ToString() != "")
            {
                sCDDP = this.CBO01_MMCDDP.GetValue().ToString();
            }
            else if (this.CBH01_GCDDP.GetValue().ToString() != "")
            {
                sCDDP = this.CBH01_GCDDP.GetValue().ToString();
            }

            if (CBO01_INQOPTION.GetValue().ToString() == "N")
            {
                sProCedureID = "TY_P_AC_29I3V175";
            }
            else
            {

                sProCedureID = "TY_P_AC_67MJ6899";
            }

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                sProCedureID,
                this.DTP01_GSTYYMM.GetValue(),
                sCDDP.ToString(),
                CBO01_INQOPTION.GetValue().ToString()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (CBO01_INQOPTION.GetValue().ToString() == "N")
                {
                    this.FPS91_TY_S_AC_29I5D178.SetValue(UP_ConvertDt(dt, "SEL"));
                }
                else
                {
                    this.FPS91_TY_S_AC_29I5D178.SetValue(UP_ConvertSeMok(dt, "SEL"));                    
                }

                // 특정 ROW 색깔 입히기
                for (int i = 0; i < this.FPS91_TY_S_AC_29I5D178.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_29I5D178.GetValue(i, "A1ABAC").ToString() == "소   계")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_29I5D178.ActiveSheet.Rows[i].BackColor = Color.FromArgb(228, 242, 194);
                    }
                    else if (this.FPS91_TY_S_AC_29I5D178.GetValue(i, "A1ABAC").ToString() == "총   계")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_29I5D178.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
                    }
                }
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }

            UP_Spread_Load();
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sCDDP = string.Empty;
            string sProCedureID = string.Empty;

            if (this.CBO01_MMCDDP.GetValue().ToString() == "" && this.CBH01_GCDDP.GetValue().ToString() == "")
            {
                this.ShowMessage("TY_M_AC_29I6V182");
                return;
            }
            else if (this.CBO01_MMCDDP.GetValue().ToString() != "")
            {
                sCDDP = this.CBO01_MMCDDP.GetValue().ToString();
            }
            else if (this.CBH01_GCDDP.GetValue().ToString() != "")
            {
                sCDDP = this.CBH01_GCDDP.GetValue().ToString();
            }

            if (CBO01_INQOPTION.GetValue().ToString() == "N")
            {
                sProCedureID = "TY_P_AC_29I3V175";
            }
            else
            {
                sProCedureID = "TY_P_AC_67MJ6899";
            }


            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                sProCedureID,
                this.DTP01_GSTYYMM.GetValue(),
                sCDDP.ToString(),
                CBO01_INQOPTION.GetValue().ToString()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYACNC026R();

                // 가로 출력
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                if (CBO01_INQOPTION.GetValue().ToString() == "N")
                {
                    (new TYERGB001P(rpt, UP_ConvertDt(dt, "PRT"))).ShowDialog();
                }
                else
                {
                    (new TYERGB001P(rpt, UP_ConvertSeMok(dt, "PRT"))).ShowDialog();
                }
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        }
        #endregion

        #region Description : 데이터테이블 컨버젼
        private DataTable UP_ConvertDt(DataTable dt, string sGUBUN)
        {
            int i = 0;

            double dTMPLAMJN_TOT  = 0;
            double dTMPLAMDN_TOT  = 0;
            double dYESANHAP_TOT  = 0;
            double dTMUSAMJN_TOT  = 0;
            double dTMUSAMDN_TOT  = 0;
            double dSILJUKHAP_TOT = 0;
            double dCHA_TOT       = 0;

            double dMOD           = 0;

            string sNEWTMCDDP = string.Empty;
            string sOLDTMCDDP = string.Empty;

            string sTMLEVEL   = string.Empty;

            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;

            for (i = 1; i < nNum; i++)
            {
                
                    if (table.Rows[i - 1]["TMLEVEL"].ToString() != table.Rows[i]["TMLEVEL"].ToString())
                    {
                        // 소계
                        
                            row = table.NewRow();
                            table.Rows.InsertAt(row, i);

                            table.Rows[i]["DATE"] = table.Rows[i - 1]["DATE"].ToString();
                            table.Rows[i]["TMCDDP"] = table.Rows[i - 1]["TMCDDP"].ToString();
                            table.Rows[i]["TMCDAC"] = "";
                            table.Rows[i]["TMLEVEL"] = table.Rows[i - 1]["TMLEVEL"].ToString();

                            // 소 계 이름 넣기
                            table.Rows[i]["A1ABAC"] = "소   계";

                            sTMLEVEL = "TMLEVEL = '" + table.Rows[i - 1]["TMLEVEL"].ToString() + "' ";

                            table.Rows[i]["TMPLAMJN"] = table.Compute("SUM(TMPLAMJN)", sTMLEVEL).ToString();
                            table.Rows[i]["TMPLAMDN"] = table.Compute("SUM(TMPLAMDN)", sTMLEVEL).ToString();
                            table.Rows[i]["YESANHAP"] = table.Compute("SUM(YESANHAP)", sTMLEVEL).ToString();
                            table.Rows[i]["TMUSAMJN"] = table.Compute("SUM(TMUSAMJN)", sTMLEVEL).ToString();
                            table.Rows[i]["TMUSAMDN"] = table.Compute("SUM(TMUSAMDN)", sTMLEVEL).ToString();
                            table.Rows[i]["SILJUKHAP"] = table.Compute("SUM(SILJUKHAP)", sTMLEVEL).ToString();
                            table.Rows[i]["CHA"] = table.Compute("SUM(CHA)", sTMLEVEL).ToString();

                            // 총계
                            dTMPLAMJN_TOT = dTMPLAMJN_TOT + Convert.ToDouble(table.Rows[i]["TMPLAMJN"].ToString());
                            dTMPLAMDN_TOT = dTMPLAMDN_TOT + Convert.ToDouble(table.Rows[i]["TMPLAMDN"].ToString());
                            dYESANHAP_TOT = dYESANHAP_TOT + Convert.ToDouble(table.Rows[i]["YESANHAP"].ToString());
                            dTMUSAMJN_TOT = dTMUSAMJN_TOT + Convert.ToDouble(table.Rows[i]["TMUSAMJN"].ToString());
                            dTMUSAMDN_TOT = dTMUSAMDN_TOT + Convert.ToDouble(table.Rows[i]["TMUSAMDN"].ToString());
                            dSILJUKHAP_TOT = dSILJUKHAP_TOT + Convert.ToDouble(table.Rows[i]["SILJUKHAP"].ToString());
                            dCHA_TOT = dCHA_TOT + Convert.ToDouble(table.Rows[i]["CHA"].ToString());

                            if ((Convert.ToDouble(table.Rows[i]["YESANHAP"].ToString())) != 0)
                            {
                                dMOD = double.Parse(UP_DotDelete(Convert.ToString(((Convert.ToDouble(table.Rows[i]["SILJUKHAP"].ToString()) / Convert.ToDouble(table.Rows[i]["YESANHAP"].ToString())) * 1000)))) / 10;
                            }
                            else
                            {
                                dMOD = 0;
                            }

                            table.Rows[i]["MOD"] = string.Format("{0:###0.00}", dMOD);
                        

                        nNum = nNum + 1;

                        i = i + 1;
                    }
                
            }

            
                // 소계
                row = table.NewRow();
                table.Rows.InsertAt(row, i);

                table.Rows[i]["DATE"] = table.Rows[i - 1]["DATE"].ToString();
                table.Rows[i]["TMCDDP"] = table.Rows[i - 1]["TMCDDP"].ToString();
                table.Rows[i]["TMCDAC"] = "";
                table.Rows[i]["TMLEVEL"] = table.Rows[i - 1]["TMLEVEL"].ToString();

                // 소 계 이름 넣기
                table.Rows[i]["A1ABAC"] = "소   계";

                sTMLEVEL = "TMLEVEL = '" + table.Rows[i - 1]["TMLEVEL"].ToString() + "' ";

                table.Rows[i]["TMPLAMJN"] = table.Compute("SUM(TMPLAMJN)", sTMLEVEL).ToString();
                table.Rows[i]["TMPLAMDN"] = table.Compute("SUM(TMPLAMDN)", sTMLEVEL).ToString();
                table.Rows[i]["YESANHAP"] = table.Compute("SUM(YESANHAP)", sTMLEVEL).ToString();
                table.Rows[i]["TMUSAMJN"] = table.Compute("SUM(TMUSAMJN)", sTMLEVEL).ToString();
                table.Rows[i]["TMUSAMDN"] = table.Compute("SUM(TMUSAMDN)", sTMLEVEL).ToString();
                table.Rows[i]["SILJUKHAP"] = table.Compute("SUM(SILJUKHAP)", sTMLEVEL).ToString();
                table.Rows[i]["CHA"] = table.Compute("SUM(CHA)", sTMLEVEL).ToString();

                // 총계
                dTMPLAMJN_TOT = dTMPLAMJN_TOT + Convert.ToDouble(table.Rows[i]["TMPLAMJN"].ToString());
                dTMPLAMDN_TOT = dTMPLAMDN_TOT + Convert.ToDouble(table.Rows[i]["TMPLAMDN"].ToString());
                dYESANHAP_TOT = dYESANHAP_TOT + Convert.ToDouble(table.Rows[i]["YESANHAP"].ToString());
                dTMUSAMJN_TOT = dTMUSAMJN_TOT + Convert.ToDouble(table.Rows[i]["TMUSAMJN"].ToString());
                dTMUSAMDN_TOT = dTMUSAMDN_TOT + Convert.ToDouble(table.Rows[i]["TMUSAMDN"].ToString());
                dSILJUKHAP_TOT = dSILJUKHAP_TOT + Convert.ToDouble(table.Rows[i]["SILJUKHAP"].ToString());
                dCHA_TOT = dCHA_TOT + Convert.ToDouble(table.Rows[i]["CHA"].ToString());

                if (Convert.ToDouble(table.Rows[i]["YESANHAP"].ToString()) != 0)
                {
                    dMOD = double.Parse(UP_DotDelete(Convert.ToString(((Convert.ToDouble(table.Rows[i]["SILJUKHAP"].ToString()) / Convert.ToDouble(table.Rows[i]["YESANHAP"].ToString())) * 1000)))) / 10;
                }
                else
                {
                    dMOD = 0;
                }
                table.Rows[i]["MOD"] = string.Format("{0:###0.00}", dMOD);
                        

            i = i + 1;
            // 총계
            row = table.NewRow();
            table.Rows.InsertAt(row, i);

            table.Rows[i]["DATE"]    = table.Rows[i - 1]["DATE"].ToString();
            table.Rows[i]["TMCDDP"] = table.Rows[i - 1]["TMCDDP"].ToString();
            table.Rows[i]["TMCDAC"] = "";
            table.Rows[i]["TMLEVEL"] = table.Rows[i - 1]["TMLEVEL"].ToString();

            // 소 계 이름 넣기
            table.Rows[i]["A1ABAC"]  = "총   계";

            table.Rows[i]["TMPLAMJN"]  = string.Format("{0:###0}", dTMPLAMJN_TOT);
            table.Rows[i]["TMPLAMDN"]  = string.Format("{0:###0}", dTMPLAMDN_TOT);
            table.Rows[i]["YESANHAP"]  = string.Format("{0:###0}", dYESANHAP_TOT);
            table.Rows[i]["TMUSAMJN"]  = string.Format("{0:###0}", dTMUSAMJN_TOT);
            table.Rows[i]["TMUSAMDN"]  = string.Format("{0:###0}", dTMUSAMDN_TOT);
            table.Rows[i]["SILJUKHAP"] = string.Format("{0:###0}", dSILJUKHAP_TOT);
            table.Rows[i]["CHA"]       = string.Format("{0:###0}", dCHA_TOT);

            if (dYESANHAP_TOT != 0)
            {
                dMOD = double.Parse(UP_DotDelete(Convert.ToString(((dSILJUKHAP_TOT / dYESANHAP_TOT) * 1000)))) / 10;
            }
            else
            {
                dMOD = 0;
            }
            table.Rows[i]["MOD"]       = string.Format("{0:###0.00}", dMOD);

            DataTable Condt = new DataTable();

            Condt.Columns.Add("TMCDDP",    typeof(System.String));
            Condt.Columns.Add("TMCDAC", typeof(System.String));
            Condt.Columns.Add("A1ABAC",    typeof(System.String));
            Condt.Columns.Add("TMLEVEL",   typeof(System.String));
            Condt.Columns.Add("TMPLAMJN",  typeof(System.String));
            Condt.Columns.Add("TMPLAMDN",  typeof(System.String));
            Condt.Columns.Add("YESANHAP",  typeof(System.String));
            Condt.Columns.Add("TMUSAMJN",  typeof(System.String));
            Condt.Columns.Add("TMUSAMDN",  typeof(System.String));
            Condt.Columns.Add("SILJUKHAP", typeof(System.String));
            Condt.Columns.Add("CHA",       typeof(System.String));
            Condt.Columns.Add("MOD",       typeof(System.String));            
            Condt.Columns.Add("DATE",      typeof(System.String));

            for (i = 0; i < table.Rows.Count; i++)
            {
                sNEWTMCDDP = table.Rows[i]["TMCDDP"].ToString();
                
                row = Condt.NewRow();

                if (sGUBUN == "SEL")
                {
                    if (sNEWTMCDDP != sOLDTMCDDP)
                    {
                        row["TMCDDP"] = table.Rows[i]["TMCDDP"].ToString();
                        sOLDTMCDDP = sNEWTMCDDP;
                    }
                    else
                    {
                        row["TMCDDP"] = "";
                    }
                }
                else
                {
                    if (sNEWTMCDDP != sOLDTMCDDP)
                    {
                        row["TMCDDP"] = table.Rows[i]["TMCDDP"].ToString();
                        sOLDTMCDDP = sNEWTMCDDP;
                    }
                    else
                    {
                        row["TMCDDP"] = table.Rows[i]["TMCDDP"].ToString();
                    }
                }

                row["TMCDAC"] = table.Rows[i]["TMCDAC"].ToString();
                row["A1ABAC"]    = table.Rows[i]["A1ABAC"].ToString();
                row["TMLEVEL"]   = table.Rows[i]["TMLEVEL"].ToString();
                row["TMPLAMJN"]  = table.Rows[i]["TMPLAMJN"].ToString();
                row["TMPLAMDN"]  = table.Rows[i]["TMPLAMDN"].ToString();
                row["YESANHAP"]  = table.Rows[i]["YESANHAP"].ToString();
                row["TMUSAMJN"]  = table.Rows[i]["TMUSAMJN"].ToString();
                row["TMUSAMDN"]  = table.Rows[i]["TMUSAMDN"].ToString();
                row["SILJUKHAP"] = table.Rows[i]["SILJUKHAP"].ToString();
                row["CHA"]       = table.Rows[i]["CHA"].ToString();
                row["MOD"]       = table.Rows[i]["MOD"].ToString();
                row["DATE"]      = table.Rows[i]["DATE"].ToString();

                Condt.Rows.Add(row);
            }

            return Condt;
        }
        #endregion

        #region Description : 데이터테이블 컨버젼
        private DataTable UP_ConvertSeMok(DataTable dt, string sGUBUN)
        {
            int i = 0;

            double dTMPLAMJN_TOT = 0;
            double dTMPLAMDN_TOT = 0;
            double dYESANHAP_TOT = 0;
            double dTMUSAMJN_TOT = 0;
            double dTMUSAMDN_TOT = 0;
            double dSILJUKHAP_TOT = 0;
            double dCHA_TOT = 0;

            double dMOD = 0;

            string sNEWTMCDDP = string.Empty;
            string sOLDTMCDDP = string.Empty;

            string sTMLEVEL = string.Empty;

            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;

            for (i = 0; i < nNum ; i++)
            {

                if (table.Rows[i]["TAG02"].ToString() == "Y" )
                {
                    // 총계
                    dTMPLAMJN_TOT = dTMPLAMJN_TOT + Convert.ToDouble(table.Rows[i]["TMPLAMJN"].ToString());
                    dTMPLAMDN_TOT = dTMPLAMDN_TOT + Convert.ToDouble(table.Rows[i]["TMPLAMDN"].ToString());
                    dYESANHAP_TOT = dYESANHAP_TOT + Convert.ToDouble(table.Rows[i]["YESANHAP"].ToString());
                    dTMUSAMJN_TOT = dTMUSAMJN_TOT + Convert.ToDouble(table.Rows[i]["TMUSAMJN"].ToString());
                    dTMUSAMDN_TOT = dTMUSAMDN_TOT + Convert.ToDouble(table.Rows[i]["TMUSAMDN"].ToString());
                    dSILJUKHAP_TOT = dSILJUKHAP_TOT + Convert.ToDouble(table.Rows[i]["SILJUKHAP"].ToString());
                    dCHA_TOT = dCHA_TOT + Convert.ToDouble(table.Rows[i]["CHA"].ToString());
                }

            }
            
            // 총계
            row = table.NewRow();
            table.Rows.InsertAt(row, i);

            table.Rows[i]["DATE"] = table.Rows[i - 1]["DATE"].ToString();
            table.Rows[i]["TMCDDP"] = table.Rows[i - 1]["TMCDDP"].ToString();
            table.Rows[i]["TMLEVEL"] = table.Rows[i - 1]["TMLEVEL"].ToString();

            // 소 계 이름 넣기
            table.Rows[i]["A1ABAC"] = "총   계";

            table.Rows[i]["TMPLAMJN"] = string.Format("{0:###0}", dTMPLAMJN_TOT);
            table.Rows[i]["TMPLAMDN"] = string.Format("{0:###0}", dTMPLAMDN_TOT);
            table.Rows[i]["YESANHAP"] = string.Format("{0:###0}", dYESANHAP_TOT);
            table.Rows[i]["TMUSAMJN"] = string.Format("{0:###0}", dTMUSAMJN_TOT);
            table.Rows[i]["TMUSAMDN"] = string.Format("{0:###0}", dTMUSAMDN_TOT);
            table.Rows[i]["SILJUKHAP"] = string.Format("{0:###0}", dSILJUKHAP_TOT);
            table.Rows[i]["CHA"] = string.Format("{0:###0}", dCHA_TOT);

            if (dYESANHAP_TOT != 0)
            {
                dMOD = double.Parse(UP_DotDelete(Convert.ToString(((dSILJUKHAP_TOT / dYESANHAP_TOT) * 1000)))) / 10;
            }
            else
            {
                dMOD = 0;
            }
            table.Rows[i]["MOD"] = string.Format("{0:###0.00}", dMOD);

            DataTable Condt = new DataTable();

            Condt.Columns.Add("TMCDDP", typeof(System.String));
            Condt.Columns.Add("TMCDAC", typeof(System.String));
            Condt.Columns.Add("A1ABAC", typeof(System.String));
            Condt.Columns.Add("TMLEVEL", typeof(System.String));
            Condt.Columns.Add("TMPLAMJN", typeof(System.String));
            Condt.Columns.Add("TMPLAMDN", typeof(System.String));
            Condt.Columns.Add("YESANHAP", typeof(System.String));
            Condt.Columns.Add("TMUSAMJN", typeof(System.String));
            Condt.Columns.Add("TMUSAMDN", typeof(System.String));
            Condt.Columns.Add("SILJUKHAP", typeof(System.String));
            Condt.Columns.Add("CHA", typeof(System.String));
            Condt.Columns.Add("MOD", typeof(System.String));
            Condt.Columns.Add("DATE", typeof(System.String));

            for (i = 0; i < table.Rows.Count; i++)
            {
                sNEWTMCDDP = table.Rows[i]["TMCDDP"].ToString();

                row = Condt.NewRow();

                if (sGUBUN == "SEL")
                {
                    if (sNEWTMCDDP != sOLDTMCDDP)
                    {
                        row["TMCDDP"] = table.Rows[i]["TMCDDP"].ToString();
                        sOLDTMCDDP = sNEWTMCDDP;
                    }
                    else
                    {
                        row["TMCDDP"] = "";
                    }
                }
                else
                {
                    if (sNEWTMCDDP != sOLDTMCDDP)
                    {
                        row["TMCDDP"] = table.Rows[i]["TMCDDP"].ToString();
                        sOLDTMCDDP = sNEWTMCDDP;
                    }
                    else
                    {
                        row["TMCDDP"] = table.Rows[i]["TMCDDP"].ToString();
                    }
                }

                row["TMCDAC"] = table.Rows[i]["TMCDAC"].ToString();
                row["A1ABAC"] = table.Rows[i]["A1ABAC"].ToString();
                row["TMLEVEL"] = table.Rows[i]["TMLEVEL"].ToString();
                row["TMPLAMJN"] = table.Rows[i]["TMPLAMJN"].ToString();
                row["TMPLAMDN"] = table.Rows[i]["TMPLAMDN"].ToString();
                row["YESANHAP"] = table.Rows[i]["YESANHAP"].ToString();
                row["TMUSAMJN"] = table.Rows[i]["TMUSAMJN"].ToString();
                row["TMUSAMDN"] = table.Rows[i]["TMUSAMDN"].ToString();
                row["SILJUKHAP"] = table.Rows[i]["SILJUKHAP"].ToString();
                row["CHA"] = table.Rows[i]["CHA"].ToString();
                row["MOD"] = table.Rows[i]["MOD"].ToString();
                row["DATE"] = table.Rows[i]["DATE"].ToString();

                Condt.Rows.Add(row);
            }

            return Condt;
        }
        #endregion

        #region Description : 기준년월 이벤트
        private void DTP01_GSTYYMM_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_GCDDP.DummyValue = this.DTP01_GSTYYMM.GetValue() + "01";
        }
        #endregion

        #region Description : 스프레드 로드
        private void UP_Spread_Load()
        {
            this.FPS91_TY_S_AC_29I5D178_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_29I5D178_Sheet1.RowHeaderColumnCount = 1;
            //(현재ROW, 현재COLUMN, 묶을ROW수, 묶을 COLUMN수)
            this.FPS91_TY_S_AC_29I5D178_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1); // 부서

            this.FPS91_TY_S_AC_29I5D178_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1); // 계정

            this.FPS91_TY_S_AC_29I5D178_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1); // 구분
            
            this.FPS91_TY_S_AC_29I5D178_Sheet1.AddColumnHeaderSpanCell(0, 4, 1, 3); // 예산

            this.FPS91_TY_S_AC_29I5D178_Sheet1.AddColumnHeaderSpanCell(0, 7, 1, 3); // 실적

            this.FPS91_TY_S_AC_29I5D178_Sheet1.AddColumnHeaderSpanCell(0, 10, 2, 1); // 차액(실적계-예산계

            this.FPS91_TY_S_AC_29I5D178_Sheet1.AddColumnHeaderSpanCell(0, 11, 2, 1); // 비율(실적/예산)

            this.FPS91_TY_S_AC_29I5D178_Sheet1.ColumnHeader.Cells[0, 1].Value  = "부서";
            this.FPS91_TY_S_AC_29I5D178_Sheet1.ColumnHeader.Cells[0, 2].Value =  "계정";
            this.FPS91_TY_S_AC_29I5D178_Sheet1.ColumnHeader.Cells[0, 3].Value  = "구분";
            this.FPS91_TY_S_AC_29I5D178_Sheet1.ColumnHeader.Cells[0, 4].Value  = "예산";
            this.FPS91_TY_S_AC_29I5D178_Sheet1.ColumnHeader.Cells[0, 7].Value  = "실적";
            this.FPS91_TY_S_AC_29I5D178_Sheet1.ColumnHeader.Cells[0, 10].Value  = "차액 실적계-예산계";
            this.FPS91_TY_S_AC_29I5D178_Sheet1.ColumnHeader.Cells[0, 11].Value = "비율 실적/예산";

            this.FPS91_TY_S_AC_29I5D178_Sheet1.ColumnHeader.Cells[1, 1].Value  = "";
            this.FPS91_TY_S_AC_29I5D178_Sheet1.ColumnHeader.Cells[1, 2].Value = "";
            this.FPS91_TY_S_AC_29I5D178_Sheet1.ColumnHeader.Cells[1, 3].Value  = "";
            this.FPS91_TY_S_AC_29I5D178_Sheet1.ColumnHeader.Cells[1, 4].Value  = "1-전월";
            this.FPS91_TY_S_AC_29I5D178_Sheet1.ColumnHeader.Cells[1, 5].Value  = "당월";
            this.FPS91_TY_S_AC_29I5D178_Sheet1.ColumnHeader.Cells[1, 6].Value  = "계";
            this.FPS91_TY_S_AC_29I5D178_Sheet1.ColumnHeader.Cells[1, 7].Value  = "1-전월";
            this.FPS91_TY_S_AC_29I5D178_Sheet1.ColumnHeader.Cells[1, 8].Value  = "당월";
            this.FPS91_TY_S_AC_29I5D178_Sheet1.ColumnHeader.Cells[1, 9].Value  = "계";
            this.FPS91_TY_S_AC_29I5D178_Sheet1.ColumnHeader.Cells[1, 10].Value  = "";
            this.FPS91_TY_S_AC_29I5D178_Sheet1.ColumnHeader.Cells[1, 11].Value = "";

            //for (int i = 0; i < this.FPS91_TY_S_AC_29I5D178_Sheet1.RowCount; i++)
            //{
            //    // 스프레드 칼럼 ZERO인 경우 안나오게 함.
            //    GeneralCellType tmpCellType = new GeneralCellType();
            //    tmpCellType.FormatString = "#,###";

            //    this.FPS91_TY_S_AC_29I5D178_Sheet1.Cells[i, 3].CellType  = tmpCellType;
            //    this.FPS91_TY_S_AC_29I5D178_Sheet1.Cells[i, 4].CellType  = tmpCellType;
            //    this.FPS91_TY_S_AC_29I5D178_Sheet1.Cells[i, 5].CellType  = tmpCellType;
            //    this.FPS91_TY_S_AC_29I5D178_Sheet1.Cells[i, 6].CellType  = tmpCellType;
            //    this.FPS91_TY_S_AC_29I5D178_Sheet1.Cells[i, 7].CellType  = tmpCellType;
            //    this.FPS91_TY_S_AC_29I5D178_Sheet1.Cells[i, 8].CellType  = tmpCellType;
            //    this.FPS91_TY_S_AC_29I5D178_Sheet1.Cells[i, 9].CellType  = tmpCellType;

            //    tmpCellType.FormatString = "#,###.00";
            //    this.FPS91_TY_S_AC_29I5D178_Sheet1.Cells[i, 10].CellType = tmpCellType;

            //    this.FPS91_TY_S_AC_29I5D178_Sheet1.Cells[i, 3].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //    this.FPS91_TY_S_AC_29I5D178_Sheet1.Cells[i, 4].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //    this.FPS91_TY_S_AC_29I5D178_Sheet1.Cells[i, 5].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //    this.FPS91_TY_S_AC_29I5D178_Sheet1.Cells[i, 6].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //    this.FPS91_TY_S_AC_29I5D178_Sheet1.Cells[i, 7].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //    this.FPS91_TY_S_AC_29I5D178_Sheet1.Cells[i, 8].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //    this.FPS91_TY_S_AC_29I5D178_Sheet1.Cells[i, 9].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //    this.FPS91_TY_S_AC_29I5D178_Sheet1.Cells[i, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //}

            if (this.FPS91_TY_S_AC_29I5D178_Sheet1.AlternatingRows.Count > 0)
                this.FPS91_TY_S_AC_29I5D178_Sheet1.AlternatingRows[0].BackColor = Color.White;
        }
        #endregion

    }
}