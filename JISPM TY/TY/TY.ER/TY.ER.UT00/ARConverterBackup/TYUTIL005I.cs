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
using DataDynamics.ActiveReports;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using FarPoint.Win.Spread.CellType;

namespace TY.ER.UT00
{
    public partial class TYUTIL005I : TYBase
    {
        private string fsGUBUN = string.Empty;

        #region Description : 페이지 로드 
        public TYUTIL005I()
        {
            InitializeComponent();
        }

        private void TYUTIL005I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            UP_BUTTON_Visible("");

            this.DTP01_STDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"));
            this.DTP01_EDDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));
            this.DTP01_CLYYMMDD.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_CLYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            UP_SEARCH();
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            UP_TXTBOX_ReadOnly("NEW");
            UP_BUTTON_Visible("NEW");

            UP_FieldClear("");

            SetFocus(this.DTP01_CLYYMMDD);

            fsGUBUN = "NEW";
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sTempDate = string.Empty;
            string sTempDD   = string.Empty;
            string sTempMM   = string.Empty;
            string sTempYY   = string.Empty;

            string sYear    = string.Empty;
            string sMonth   = string.Empty;
            string sSTDATE  = string.Empty;
            string sEDDATE  = string.Empty;
            string sCLSTAMT = string.Empty;
            string sCLELAMT = string.Empty;

            sTempDD = Get_Date(this.DTP01_CLYYMMDD.GetValue().ToString()).Substring(6, 2);
            sTempMM = Get_Date(this.DTP01_CLYYMMDD.GetValue().ToString()).Substring(4, 2);
            sTempYY = Get_Date(this.DTP01_CLYYMMDD.GetValue().ToString()).Substring(0, 4);

            if (int.Parse(sTempDD) > 20)
            {
                sTempMM = Set_Fill2(Convert.ToString(Convert.ToInt16(sTempMM) + 1));
                if (int.Parse(sTempMM) > 12)
                {
                    sTempYY = Convert.ToString(Convert.ToInt16(sTempYY) + 1);
                    sTempMM = "01";
                }
            }

            sTempDate = sTempYY + sTempMM;

            sCLSTAMT = "0";
            sCLELAMT = "0";

            if (sTempDate.Substring(4, 2) == "01")
            {
                sYear = Convert.ToString(int.Parse(sTempDate.Substring(0, 4).ToString()) - 1);
                sMonth = "12";
            }
            else
            {
                sYear = sTempDate.Substring(0, 4);
                sMonth = Set_Fill2(Convert.ToString(int.Parse(sTempDate.Substring(4, 2).ToString()) - 1));
            }

            sSTDATE = sYear + sMonth + "21";
            sEDDATE = sTempDate + "20";


            this.DbConnector.CommandClear();

            if (fsGUBUN == "NEW") // 저장
            {
                // 적용년월 적용전
                //this.DbConnector.Attach("TY_P_UT_719FS367",
                //                        Get_Date(this.DTP01_CLYYMMDD.GetValue().ToString()),
                //                        this.CBH01_CLHWAJU.GetValue().ToString(),
                //                        Set_TankNo(this.TXT01_CLTANK.GetValue().ToString()),
                //                        this.CBH01_CLJNHWAMUL.GetValue().ToString(),
                //                        Get_Numeric(this.TXT01_CLIPHWAMUL.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLPSQTY.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLPSDANGA.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLDMQTY.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLDMDANGA.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLINWON.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLSTTIME.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLSTQTY.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLSTDANGA.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLSTAMT.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLELTIME.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLELDANGA.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLELAMT.GetValue().ToString()),
                //                        this.TXT01_CLNAME.GetValue().ToString(),
                //                        Get_Date(this.DTP01_CLSTDATE.GetValue().ToString()),
                //                        Get_Date(this.DTP01_CLEDDATE.GetValue().ToString()),
                //                        TYUserInfo.EmpNo.ToString().Trim().ToUpper()           // 작성사번
                //                        );

                // 세척단가, 횟수 적용전
                //this.DbConnector.Attach("TY_P_UT_B4S9Z273",
                //                        Get_Date(this.DTP01_CLYYMM.GetValue().ToString()).Substring(0,6),
                //                        Get_Date(this.DTP01_CLYYMMDD.GetValue().ToString()),
                //                        this.CBH01_CLHWAJU.GetValue().ToString(),
                //                        Set_TankNo(this.TXT01_CLTANK.GetValue().ToString()),
                //                        this.CBH01_CLJNHWAMUL.GetValue().ToString(),
                //                        Get_Numeric(this.TXT01_CLIPHWAMUL.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLPSQTY.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLPSDANGA.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLDMQTY.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLDMDANGA.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLINWON.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLSTTIME.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLSTQTY.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLSTDANGA.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLSTAMT.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLELTIME.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLELDANGA.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLELAMT.GetValue().ToString()),
                //                        this.TXT01_CLNAME.GetValue().ToString(),
                //                        Get_Date(this.DTP01_CLSTDATE.GetValue().ToString()),
                //                        Get_Date(this.DTP01_CLEDDATE.GetValue().ToString()),
                //                        TYUserInfo.EmpNo.ToString().Trim().ToUpper()           // 작성사번
                //                        );

                this.DbConnector.Attach("TY_P_UT_C2HGT081",
                                        Get_Date(this.DTP01_CLYYMM.GetValue().ToString()).Substring(0, 6),
                                        Get_Date(this.DTP01_CLYYMMDD.GetValue().ToString()),
                                        this.CBH01_CLHWAJU.GetValue().ToString(),
                                        Set_TankNo(this.TXT01_CLTANK.GetValue().ToString()),
                                        this.CBH01_CLJNHWAMUL.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_CLIPHWAMUL.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_CLPSQTY.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_CLPSDANGA.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_CLDMQTY.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_CLDMDANGA.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_CLINWON.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_CLSTTIME.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_CLSTQTY.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_CLSTDANGA.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_CLSTAMT.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_CLELTIME.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_CLELDANGA.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_CLELAMT.GetValue().ToString()),
                                        this.TXT01_CLNAME.GetValue().ToString(),
                                        Get_Date(this.DTP01_CLSTDATE.GetValue().ToString()),
                                        Get_Date(this.DTP01_CLEDDATE.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_CLDANGA.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_CLCOUNT.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_CLAMT.GetValue().ToString()),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper()           // 작성사번
                                        );
            }
            else
            {
                // 적용년월 적용전
                //this.DbConnector.Attach("TY_P_UT_719FS368",
                //                        this.CBH01_CLJNHWAMUL.GetValue().ToString(),
                //                        Get_Numeric(this.TXT01_CLIPHWAMUL.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLPSQTY.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLPSDANGA.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLDMQTY.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLDMDANGA.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLINWON.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLSTTIME.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLSTQTY.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLSTDANGA.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLSTAMT.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLELTIME.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLELDANGA.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLELAMT.GetValue().ToString()),
                //                        this.TXT01_CLNAME.GetValue().ToString(),
                //                        Get_Date(this.DTP01_CLSTDATE.GetValue().ToString()),
                //                        Get_Date(this.DTP01_CLEDDATE.GetValue().ToString()),
                //                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),          // 작성사번
                //                        Get_Date(this.DTP01_CLYYMMDD.GetValue().ToString()),
                //                        this.CBH01_CLHWAJU.GetValue().ToString(),
                //                        this.TXT01_CLTANK.GetValue().ToString().Trim()
                //                        );

                // 세척단가, 횟수 적용전
                //this.DbConnector.Attach("TY_P_UT_B4SA0274",
                //                        this.CBH01_CLJNHWAMUL.GetValue().ToString(),
                //                        Get_Numeric(this.TXT01_CLIPHWAMUL.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLPSQTY.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLPSDANGA.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLDMQTY.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLDMDANGA.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLINWON.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLSTTIME.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLSTQTY.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLSTDANGA.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLSTAMT.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLELTIME.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLELDANGA.GetValue().ToString()),
                //                        Get_Numeric(this.TXT01_CLELAMT.GetValue().ToString()),
                //                        this.TXT01_CLNAME.GetValue().ToString(),
                //                        Get_Date(this.DTP01_CLSTDATE.GetValue().ToString()),
                //                        Get_Date(this.DTP01_CLEDDATE.GetValue().ToString()),
                //                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),          // 작성사번
                //                        Get_Date(this.DTP01_CLYYMM.GetValue().ToString()).Substring(0, 6),
                //                        Get_Date(this.DTP01_CLYYMMDD.GetValue().ToString()),
                //                        this.CBH01_CLHWAJU.GetValue().ToString(),
                //                        this.TXT01_CLTANK.GetValue().ToString().Trim()
                //                        );

                this.DbConnector.Attach("TY_P_UT_C2HGY082",
                                        this.CBH01_CLJNHWAMUL.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_CLIPHWAMUL.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_CLPSQTY.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_CLPSDANGA.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_CLDMQTY.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_CLDMDANGA.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_CLINWON.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_CLSTTIME.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_CLSTQTY.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_CLSTDANGA.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_CLSTAMT.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_CLELTIME.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_CLELDANGA.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_CLELAMT.GetValue().ToString()),
                                        this.TXT01_CLNAME.GetValue().ToString(),
                                        Get_Date(this.DTP01_CLSTDATE.GetValue().ToString()),
                                        Get_Date(this.DTP01_CLEDDATE.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_CLDANGA.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_CLCOUNT.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_CLAMT.GetValue().ToString()),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),          // 작성사번
                                        Get_Date(this.DTP01_CLYYMM.GetValue().ToString()).Substring(0, 6),
                                        Get_Date(this.DTP01_CLYYMMDD.GetValue().ToString()),
                                        this.CBH01_CLHWAJU.GetValue().ToString(),
                                        this.TXT01_CLTANK.GetValue().ToString().Trim()
                                        );
            }

            this.DbConnector.ExecuteTranQueryList();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            // 적용년월 적용전
            //this.DbConnector.Attach
            //    (
            //    "TY_P_UT_71AEK386",
            //    sSTDATE.ToString(),
            //    sEDDATE.ToString()
            //    );
            this.DbConnector.Attach
                (
                "TY_P_UT_B4S9Q272",
                Get_Date(this.DTP01_CLYYMM.GetValue().ToString()).Substring(0,6)
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sCLSTAMT = Get_Numeric(SetDefaultValue(dt.Rows[0]["CLSTAMT"].ToString()));
                sCLELAMT = Get_Numeric(SetDefaultValue(dt.Rows[0]["CLELAMT"].ToString()));
            }

            this.DbConnector.CommandClear();

            // 가열료 스팀비용(TYO) 업데이트
            this.DbConnector.Attach("TY_P_UT_71AF0387",
                                    sCLSTAMT.ToString(),
                                    sCLELAMT.ToString(),
                                    "1",
                                    Get_Date(this.DTP01_CLYYMM.GetValue().ToString()).Substring(0, 6) + "20",
                                    "TYO"
                                    );

            // 월 가열료 집계 스팀비용(TYO) 업데이트
            this.DbConnector.Attach("TY_P_UT_71AF1388",
                                    sCLSTAMT.ToString(),
                                    sCLELAMT.ToString(),
                                    "1",
                                    Get_Date(this.DTP01_CLYYMM.GetValue().ToString()).Substring(0, 6),
                                    "TYO"
                                    );

            this.DbConnector.ExecuteTranQueryList();

            UP_TXTBOX_ReadOnly("");
            UP_BUTTON_Visible("");

            this.BTN61_INQ_Click(null, null);

            SetFocus(this.BTN61_NEW);

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            string sTempDate = string.Empty;
            string sTempDD = string.Empty;
            string sTempMM = string.Empty;
            string sTempYY = string.Empty;

            string sYear = string.Empty;
            string sMonth = string.Empty;
            string sSTDATE = string.Empty;
            string sEDDATE = string.Empty;
            string sCLSTAMT = string.Empty;
            string sCLELAMT = string.Empty;

            sTempDD = Get_Date(this.DTP01_CLYYMMDD.GetValue().ToString()).Substring(6, 2);
            sTempMM = Get_Date(this.DTP01_CLYYMMDD.GetValue().ToString()).Substring(4, 2);
            sTempYY = Get_Date(this.DTP01_CLYYMMDD.GetValue().ToString()).Substring(0, 4);

            if (int.Parse(sTempDD) > 20)
            {
                sTempMM = Set_Fill2(Convert.ToString(Convert.ToInt16(sTempMM) + 1));
                if (int.Parse(sTempMM) > 12)
                {
                    sTempYY = Convert.ToString(Convert.ToInt16(sTempYY) + 1);
                    sTempMM = "01";
                }
            }

            sTempDate = sTempYY + sTempMM;

            sCLSTAMT = "0";
            sCLELAMT = "0";

            if (sTempDate.Substring(4, 2) == "01")
            {
                sYear = Convert.ToString(int.Parse(sTempDate.Substring(0, 4).ToString()) - 1);
                sMonth = "12";
            }
            else
            {
                sYear = sTempDate.Substring(0, 4);
                sMonth = Set_Fill2(Convert.ToString(int.Parse(sTempDate.Substring(4, 2).ToString()) - 1));
            }

            sSTDATE = sYear + sMonth + "21";
            sEDDATE = sTempDate + "20";


            this.DbConnector.CommandClear();

            // 적용년월 적용전
            //this.DbConnector.Attach("TY_P_UT_719FT369",
            //                        Get_Date(this.DTP01_CLYYMMDD.GetValue().ToString()),
            //                        this.CBH01_CLHWAJU.GetValue().ToString(),
            //                        this.TXT01_CLTANK.GetValue().ToString().Trim()
            //                        );

            this.DbConnector.Attach("TY_P_UT_B4SA2275",
                                    Get_Date(this.DTP01_CLYYMM.GetValue().ToString()).Substring(0, 6),
                                    Get_Date(this.DTP01_CLYYMMDD.GetValue().ToString()),
                                    this.CBH01_CLHWAJU.GetValue().ToString(),
                                    this.TXT01_CLTANK.GetValue().ToString().Trim()
                                    );
            

            this.DbConnector.ExecuteTranQueryList();


            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            // 적용년월 적용전
            //this.DbConnector.Attach
            //    (
            //    "TY_P_UT_71AEK386",
            //    sSTDATE.ToString(),
            //    sEDDATE.ToString()
            //    );

            this.DbConnector.Attach
                (
                "TY_P_UT_B4S9Q272",
                Get_Date(this.DTP01_CLYYMM.GetValue().ToString()).Substring(0, 6)
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sCLSTAMT = Get_Numeric(SetDefaultValue(dt.Rows[0]["CLSTAMT"].ToString()));
                sCLELAMT = Get_Numeric(SetDefaultValue(dt.Rows[0]["CLELAMT"].ToString()));
            }

            this.DbConnector.CommandClear();

            // 가열료 스팀비용(TYO) 업데이트
            this.DbConnector.Attach("TY_P_UT_71AF0387",
                                    sCLSTAMT.ToString(),
                                    sCLELAMT.ToString(),
                                    "1",
                                    Get_Date(this.DTP01_CLYYMM.GetValue().ToString()).Substring(0, 6) + "20",
                                    "TYO"
                                    );

            // 월 가열료 집계 스팀비용(TYO) 업데이트
            this.DbConnector.Attach("TY_P_UT_71AF1388",
                                    sCLSTAMT.ToString(),
                                    sCLELAMT.ToString(),
                                    "1",
                                    Get_Date(this.DTP01_CLYYMM.GetValue().ToString()).Substring(0, 6),
                                    "TYO"
                                    );

            this.DbConnector.ExecuteTranQueryList();

            UP_TXTBOX_ReadOnly("");
            UP_BUTTON_Visible("");

            UP_FieldClear("DEL");

            SetFocus(this.DTP01_CLYYMMDD);

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : DRUM 포장 전체 조회 메소드
        private void UP_SEARCH()
        {
            string sProcedure = string.Empty;
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            if (int.Parse(this.DTP01_CLYYMM.GetString()) >= 20220301)
            {
                sProcedure = "TY_P_UT_C2HHA083";
            }
            else
            {
                sProcedure = "TY_P_UT_B6TEE414";
            }   

            this.DbConnector.Attach
                (
                sProcedure,
                Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                Get_Date(this.DTP01_EDDATE.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_719HK376.SetValue(dt);
        }
        #endregion

        #region Description : 확인 메소드
        private void UP_RUN()
        {
            string sProcedure = string.Empty;
            DataTable dt = new DataTable();

            if (int.Parse(this.DTP01_CLYYMM.GetString()) >= 20220301)
            {
                sProcedure = "TY_P_UT_C2HGJ080";
            }
            else{
                sProcedure = "TY_P_UT_B6TEP416";
            }   
                
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                sProcedure,
                this.DTP01_CLYYMM.GetValue().ToString(),     // 적용년월
                this.DTP01_CLYYMMDD.GetValue().ToString(),     // 작업일자
                this.CBH01_CLHWAJU.GetValue().ToString(),      // 화주
                this.TXT01_CLTANK.GetValue().ToString().Trim() // 탱크번호
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");

                // 1원 절사
                this.TXT01_CLPAMT.SetValue(UP_DotDelete(Convert.ToString(
                    Convert.ToDouble(this.TXT01_CLPSQTY.GetValue().ToString()) *
                    Convert.ToDouble(this.TXT01_CLPSDANGA.GetValue().ToString()) * 0.1)));

                this.TXT01_CLPAMT.SetValue(String.Format("{0,9:N0}", double.Parse(this.TXT01_CLPAMT.GetValue().ToString()) * 10));

                // 1원 절사
                this.TXT01_CLDMAMT.SetValue(UP_DotDelete(Convert.ToString
                    (Convert.ToDouble(Get_Numeric(this.TXT01_CLDMQTY.GetValue().ToString())) *
                     Convert.ToDouble(Get_Numeric(this.TXT01_CLDMDANGA.GetValue().ToString())) * 0.1)));

                this.TXT01_CLDMAMT.SetValue(String.Format("{0,9:N0}", double.Parse(this.TXT01_CLDMAMT.GetValue().ToString()) * 10));

                fsGUBUN = "UPT";

                UP_BUTTON_Visible(fsGUBUN);

                UP_TXTBOX_ReadOnly("UPT");

                // FOCUS
                Timer tmr = new Timer();

                tmr.Tick += delegate
                {
                    tmr.Stop();
                    this.SetFocus(this.CBH01_CLJNHWAMUL.CodeText);
                };

                tmr.Interval = 100;
                tmr.Start();
            }
        }
        #endregion

        #region Description : 저장 ProcessCheck
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sTempDate = "";
            string sTempDD = "";
            string sTempMM = "";
            string sTempYY = "";

            sTempDD = Get_Date(this.DTP01_CLYYMMDD.GetValue().ToString()).Substring(6, 2);
            sTempMM = Get_Date(this.DTP01_CLYYMMDD.GetValue().ToString()).Substring(4, 2);
            sTempYY = Get_Date(this.DTP01_CLYYMMDD.GetValue().ToString()).Substring(0, 4);

            if (int.Parse(sTempDD) > 20)
            {
                sTempMM = Set_Fill2(Convert.ToString(Convert.ToInt16(sTempMM) + 1));
                if (int.Parse(sTempMM) > 12)
                {
                    sTempYY = Convert.ToString(Convert.ToInt16(sTempYY) + 1);
                    sTempMM = "01";
                }
            }

            sTempDate = sTempYY + sTempMM;

            DataTable dt = new DataTable();

            if (fsGUBUN == "NEW") // 저장
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_719HG375",
                    this.DTP01_CLYYMMDD.GetValue().ToString(),     // 작업일자
                    this.CBH01_CLHWAJU.GetValue().ToString(),      // 화주
                    this.TXT01_CLTANK.GetValue().ToString().Trim() // 탱크번호
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_7B495940");
                    SetFocus(this.DTP01_CLYYMMDD);

                    e.Successed = false;
                    return;
                }
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_719I2377",
                this.TXT01_CLTANK.GetValue().ToString().Trim() // 탱크번호
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_719I3378");
                SetFocus(this.TXT01_CLTANK);

                e.Successed = false;
                return;
            }

            if (double.Parse(Get_Numeric(this.TXT01_CLSTTIME.GetValue().ToString())) != 0)
            {
                if (double.Parse(Get_Numeric(this.TXT01_CLSTQTY.Text.Trim())) == 0)
                {
                    this.ShowMessage("TY_M_UT_71AE0383");
                    SetFocus(this.TXT01_CLSTQTY);

                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (double.Parse(Get_Numeric(this.TXT01_CLSTQTY.GetValue().ToString())) != 0)
                {
                    this.ShowMessage("TY_M_UT_71AE1384");
                    SetFocus(this.TXT01_CLSTQTY);

                    e.Successed = false;
                    return;
                }
            }

            if (Get_Numeric(this.TXT01_CLSTTIME.Text.Trim()) == "0" ||
                Get_Numeric(this.TXT01_CLSTQTY.Text.Trim()) == "0")
            {
                this.TXT01_CLSTDANGA.SetValue("0");
                this.TXT01_CLSTAMT.SetValue("0");
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_71AED385",
                    Get_Date(this.DTP01_CLYYMM.GetValue().ToString()).Substring(0, 6)
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    double dCLSTDANGA = 0;
                    double dCLSTTIME  = 0;

                    // 스팀 단가
                    this.TXT01_CLSTDANGA.SetValue(Get_Numeric(dt.Rows[0]["DNSTDANGA"].ToString()));

                    dCLSTDANGA = Convert.ToDouble(this.TXT01_CLSTDANGA.GetValue().ToString());
                    dCLSTTIME  = Convert.ToDouble(this.TXT01_CLSTTIME.GetValue().ToString());

                    // 스팀 금액
                    this.TXT01_CLSTAMT.SetValue(Convert.ToString
                        (
                            Double.Parse
                            (
                                Convert.ToString
                                (
                                    Convert.ToDouble(UP_DotDelete(Convert.ToString(dCLSTDANGA * dCLSTTIME / 10))) * 10
                                )
                            ).ToString("#,0")
                        ));
                }
                else
                {
                    this.TXT01_CLSTDANGA.SetValue("0");
                    this.TXT01_CLSTAMT.SetValue("0");
                }
            }

            // 전기
            if (Get_Numeric(this.TXT01_CLELTIME.GetValue().ToString()) != "0")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_71AED385",
                    Get_Date(this.DTP01_CLYYMM.GetValue().ToString()).Substring(0, 6)
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    double dCLELDANGA = 0;
                    double dCLELTIME  = 0;
                    // 전기 단가
                    this.TXT01_CLELDANGA.SetValue(Get_Numeric(dt.Rows[0]["DNSELDANGA"].ToString()));

                    dCLELDANGA = double.Parse(this.TXT01_CLELDANGA.GetValue().ToString());
                    dCLELTIME  = double.Parse(this.TXT01_CLELTIME.GetValue().ToString());

                    // 전기 금액
                    this.TXT01_CLELAMT.SetValue(Convert.ToString
                        (
                            Double.Parse
                            (
                                Convert.ToString
                                (
                                    Convert.ToDouble(UP_DotDelete(Convert.ToString(dCLELDANGA * dCLELTIME / 10))) * 10
                                )
                            ).ToString("#,0")
                        ));
                }
                else
                {
                    this.TXT01_CLELDANGA.SetValue("0");
                    this.TXT01_CLELAMT.SetValue("0");
                }
            }
            else
            {
                this.TXT01_CLELDANGA.SetValue("0");
                this.TXT01_CLELAMT.SetValue("0");
            }

            // 세척비용
            if (Get_Numeric(this.TXT01_CLDANGA.GetValue().ToString()) == "0")
            {
                this.ShowCustomMessage("세척 단가를 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                SetFocus(this.TXT01_CLDANGA);

                e.Successed = false;
                return;
            }

            if (Get_Numeric(this.TXT01_CLDANGA.GetValue().ToString()) != "0" && Get_Numeric(this.TXT01_CLCOUNT.GetValue().ToString()) != "0")
            {
                double dCLDANGA = 0;
                double dCLCOUNT = 0;
                double dCLAMT = 0;

                dCLDANGA = double.Parse(this.TXT01_CLDANGA.GetValue().ToString());
                dCLCOUNT = double.Parse(this.TXT01_CLCOUNT.GetValue().ToString());
                dCLAMT = dCLDANGA * dCLCOUNT;

                this.TXT01_CLAMT.SetValue(Double.Parse(Convert.ToString(dCLAMT)).ToString("#,0")); 
            }

            // 저장하시겠습니까?
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 삭제 ProcessCheck
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 필드 클리어
        private void UP_FieldClear(string sGUBUN)
        {
            if (sGUBUN != "DEL")
            {
                this.CBH01_CLHWAJU.SetValue("");    // 화주
                this.CBH01_CLJNHWAMUL.SetValue("");   // 화물
            }

            this.TXT01_CLIPHWAMUL.SetValue("");
            this.TXT01_CLPSQTY.SetValue("");
            this.TXT01_CLPSDANGA.SetValue("");
            this.TXT01_CLDANGA.SetValue("");
            this.TXT01_CLCOUNT.SetValue("1");
            this.TXT01_CLAMT.SetValue("");
            this.TXT01_CLSTTIME.SetValue("");
            this.TXT01_CLPAMT.SetValue("");
            this.TXT01_CLSTQTY.SetValue("");
            this.TXT01_CLSTDANGA.SetValue("");
            this.TXT01_CLDMQTY.SetValue("");
            this.TXT01_CLELTIME.SetValue("");
            this.TXT01_CLDMDANGA.SetValue("");
            this.TXT01_CLELDANGA.SetValue("");
            this.TXT01_CLTANK.SetValue("");

            this.TXT01_CLINWON.SetValue("");
            this.TXT01_CLNAME.SetValue("");

            this.TXT01_CLDMAMT.SetValue("");
            this.TXT01_CLSTAMT.SetValue("");
            this.TXT01_CLELAMT.SetValue("");
        }
        #endregion

        #region Description : 버튼 Visible
        private void UP_BUTTON_Visible(string sGUBUN)
        {
            if (sGUBUN.ToString() == "NEW")
            {
                this.BTN61_SAV.Visible = true;
                this.BTN61_REM.Visible = false;
            }
            else if (sGUBUN.ToString() == "SAV" || sGUBUN.ToString() == "UPT")
            {
                this.BTN61_SAV.Visible = true;
                this.BTN61_REM.Visible = true;
            }
            else if (sGUBUN.ToString() == "")
            {
                this.BTN61_SAV.Visible = false;
                this.BTN61_REM.Visible = false;
            }
        }
        #endregion

        #region Description : TEXTBOX - ReadOnly
        private void UP_TXTBOX_ReadOnly(string sGUBUN)
        {
            if (sGUBUN.ToString() == "NEW")
            {
                this.DTP01_CLYYMMDD.SetReadOnly(false);
                this.DTP01_CLYYMM.SetReadOnly(false);
                this.CBH01_CLHWAJU.SetReadOnly(false);
                this.TXT01_CLTANK.SetReadOnly(false);
            }
            else if (sGUBUN.ToString() == "SAV" || sGUBUN.ToString() == "UPT")
            {
                this.DTP01_CLYYMMDD.SetReadOnly(true);
                this.DTP01_CLYYMM.SetReadOnly(true);
                this.CBH01_CLHWAJU.SetReadOnly(true);
                this.TXT01_CLTANK.SetReadOnly(true);
            }
            else if (sGUBUN.ToString() == "")
            {
                this.DTP01_CLYYMMDD.SetReadOnly(true);
                this.DTP01_CLYYMM.SetReadOnly(true);
                this.CBH01_CLHWAJU.SetReadOnly(true);
                this.TXT01_CLTANK.SetReadOnly(true);
            }
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_UT_719HK376_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DTP01_CLYYMM.SetValue(this.FPS91_TY_S_UT_719HK376.GetValue("CLYYMM").ToString());      // 적용년월
            this.DTP01_CLYYMMDD.SetValue(this.FPS91_TY_S_UT_719HK376.GetValue("CLYYMMDD").ToString());  // 작업일자
            this.CBH01_CLHWAJU.SetValue(this.FPS91_TY_S_UT_719HK376.GetValue("CLHWAJU").ToString());    // 화주
            this.TXT01_CLTANK.SetValue(this.FPS91_TY_S_UT_719HK376.GetValue("CLTANK").ToString());      // 탱크번호

            // 확인
            UP_RUN();
        }
        #endregion

        #region Description : 탱크번호 이벤트
        private void TXT01_CLTANK_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == '\r')
            //{
            //    if (this.TXT01_CLTANK.GetValue().ToString() != "" && this.DTP01_CLYYMM.GetValue().ToString() != "")
            //    {
            //        // 세척단가 조회
            //        this.DbConnector.CommandClear();
            //        this.DbConnector.Attach
            //            (
            //            "TY_P_UT_C2HG8079",
            //            Get_Date(this.DTP01_CLYYMM.GetValue().ToString()).Substring(0, 6),
            //            this.TXT01_CLTANK.GetValue().ToString().Trim()
            //            );

            //        DataTable dt = this.DbConnector.ExecuteDataTable();

            //        if (dt.Rows.Count > 0)
            //        {
            //            this.TXT01_CLDANGA.SetValue(dt.Rows[0]["YOGUMAIK"].ToString());
            //        }
            //        else
            //        {
            //            this.TXT01_CLDANGA.SetValue("");
            //        }
            //    }
            //    else
            //    {
            //        this.TXT01_CLDANGA.SetValue("");
            //    }

            //    SetFocus(this.CBH01_CLJNHWAMUL.CodeText);
            //}
        }
        #endregion

        #region Description : 세척기간 이벤트
        private void DTP01_CLEDDATE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.BTN61_SAV.Visible == true)
                {
                    SetFocus(this.BTN61_SAV);
                }
            }
        }
        #endregion

        #region Description : 종료일자 이벤트
        private void DTP01_EDDATE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                this.BTN61_INQ_Click(null, null);
            }

            if (e.KeyCode == Keys.Enter)
            {
                this.BTN61_INQ_Click(null, null);
            }
        }

        private void DTP01_CLEDDATE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.BTN61_SAV.Visible == true)
                {
                    SetFocus(this.BTN61_SAV);
                }
            }
        }
        #endregion

        private void TXT01_CLTANK_Leave(object sender, EventArgs e)
        {
            if (this.TXT01_CLTANK.GetValue().ToString() != "" && this.DTP01_CLYYMM.GetValue().ToString() != "")
            {
                // 세척단가 조회
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_C2HG8079",
                    Get_Date(this.DTP01_CLYYMM.GetValue().ToString()).Substring(0, 6),
                    this.TXT01_CLTANK.GetValue().ToString().Trim()
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_CLDANGA.SetValue(dt.Rows[0]["YOGUMAIK"].ToString());
                }
                else
                {
                    this.TXT01_CLDANGA.SetValue("");
                }
            }
            else
            {
                this.TXT01_CLDANGA.SetValue("");
            }

            SetFocus(this.CBH01_CLJNHWAMUL.CodeText);
        }
    }
}