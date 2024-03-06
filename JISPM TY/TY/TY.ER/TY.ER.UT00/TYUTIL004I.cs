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

namespace TY.ER.UT00
{
    public partial class TYUTIL004I : TYBase
    {
        private int fiRow = 0;

        private string fsGASTAMT = string.Empty;
        private string fsGASTTIME = string.Empty;
        private string fsGASTQTY = string.Empty;

        private string fsGMYYMM = string.Empty;
        private string fsGMCHK = string.Empty;
        private double fdGMELTIME = 0;
        private double fdGMKYQTY = 0;
        private double fdGMELAMT = 0;
        private double fdGMKYAMT = 0;

        private string fsGAM_GUBUN = string.Empty;

        private string fsGUBUN = string.Empty;

        #region Description : 페이지 로드
        public TYUTIL004I()
        {
            InitializeComponent();
        }

        private void TYUTIL004I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            UP_BUTTON_Visible("");

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

            UP_GET_Total();

            SetFocus(this.CBH01_GAHWAJU.CodeText);

            fsGUBUN = "NEW";
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sUTBK_GUBUN = string.Empty;
            string sUTKY_GUBUN = string.Empty;
            string sUTST_GUBUN = string.Empty;
            string sUTDK_GUBUN = string.Empty;

            DataTable dt = new DataTable();

            // 벙커 C유
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_715I0319",
                this.CBO01_GAGUBUN.GetValue().ToString(),           // 구분
                Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString()) // 작업일자
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sUTBK_GUBUN = "UPT";
            }
            else
            {
                sUTBK_GUBUN = "INS";
            }

            // 경유
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_715I0320",
                this.CBO01_GAGUBUN.GetValue().ToString(),           // 구분
                Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString()) // 작업일자
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sUTKY_GUBUN = "UPT";
            }
            else
            {
                sUTKY_GUBUN = "INS";
            }

            // 스팀
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_715I1321",
                this.CBO01_GAGUBUN.GetValue().ToString(),           // 구분
                Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString()) // 작업일자
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sUTST_GUBUN = "UPT";
            }
            else
            {
                sUTST_GUBUN = "INS";
            }

            // 등류
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_715I2322",
                this.CBO01_GAGUBUN.GetValue().ToString(),           // 구분
                Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString()) // 작업일자
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sUTDK_GUBUN = "UPT";
            }
            else
            {
                sUTDK_GUBUN = "INS";
            }

            this.DbConnector.CommandClear();

            if (fsGUBUN == "NEW") // 저장
            {
                this.DbConnector.Attach("TY_P_UT_716GD332",
                                        this.CBO01_GAGUBUN.GetValue().ToString(),
                                        Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString()),
                                        this.CBH01_GAHWAJU.GetValue().ToString(),
                                        this.CBH01_GAHWAMUL.GetValue().ToString(),
                                        Set_TankNo(this.TXT01_GATANK.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_GAELTIME.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_GASTTIME.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_GABKQTY.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_GAKYQTY.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_GASTQTY.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_GAELAMT.GetValue().ToString()),
                                        "0",
                                        Get_Numeric(this.TXT01_GAKYAMT.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_GASTAMT.GetValue().ToString()),
                                        "0",
                                        "0",
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper()           // 작성사번
                                        );
            }
            else
            {
                this.DbConnector.Attach("TY_P_UT_716GQ333",
                                        Set_TankNo(this.TXT01_GATANK.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_GAELTIME.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_GASTTIME.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_GABKQTY.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_GAKYQTY.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_GASTQTY.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_GAELAMT.GetValue().ToString()),
                                        "0",
                                        Get_Numeric(this.TXT01_GAKYAMT.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_GASTAMT.GetValue().ToString()),
                                        "0",
                                        "0",
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),          // 작성사번
                                        this.CBO01_GAGUBUN.GetValue().ToString(),
                                        Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString()),
                                        this.CBH01_GAHWAJU.GetValue().ToString(),
                                        this.CBH01_GAHWAMUL.GetValue().ToString()
                                        );
            }

            // 벙커 C유
            if (sUTBK_GUBUN == "INS")
            {
                this.DbConnector.Attach("TY_P_UT_716H7335",
                                        this.CBO01_GAGUBUN.GetValue().ToString(),
                                        Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString()),
                                        "0",
                                        this.TXT01_KYTIM.GetValue().ToString(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper()
                                        );
            }
            else
            {
                this.DbConnector.Attach("TY_P_UT_716H8336",
                                        "0",
                                        this.TXT01_KYTIM.GetValue().ToString(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                        this.CBO01_GAGUBUN.GetValue().ToString(),
                                        Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString())
                                        );
            }

            // 경유
            if (sUTKY_GUBUN == "INS")
            {
                this.DbConnector.Attach("TY_P_UT_716H8337",
                                        this.CBO01_GAGUBUN.GetValue().ToString(),
                                        Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString()),
                                        this.TXT01_KYQTY.GetValue().ToString(),
                                        this.TXT01_KYTIM.GetValue().ToString(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper()
                                        );
            }
            else
            {
                this.DbConnector.Attach("TY_P_UT_716H9338",
                                        this.TXT01_KYQTY.GetValue().ToString(),
                                        this.TXT01_KYTIM.GetValue().ToString(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                        this.CBO01_GAGUBUN.GetValue().ToString(),
                                        Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString())
                                        );
            }

            // 스팀
            if (sUTST_GUBUN == "INS")
            {
                this.DbConnector.Attach("TY_P_UT_716H9339",
                                        this.CBO01_GAGUBUN.GetValue().ToString(),
                                        Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString()),
                                        this.TXT01_STQTY.GetValue().ToString(),
                                        this.TXT01_STTIM.GetValue().ToString(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper()
                                        );
            }
            else
            {
                this.DbConnector.Attach("TY_P_UT_716H9340",
                                        this.TXT01_STQTY.GetValue().ToString(),
                                        this.TXT01_STTIM.GetValue().ToString(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                        this.CBO01_GAGUBUN.GetValue().ToString(),
                                        Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString())
                                        );
            }

            // 등유
            if (sUTDK_GUBUN == "INS")
            {
                this.DbConnector.Attach("TY_P_UT_716H0341",
                                        this.CBO01_GAGUBUN.GetValue().ToString(),
                                        Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString()),
                                        "0",
                                        this.TXT01_KYTIM.GetValue().ToString(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper()
                                        );
            }
            else
            {
                this.DbConnector.Attach("TY_P_UT_716H0342",
                                        "0",
                                        this.TXT01_KYTIM.GetValue().ToString(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                        this.CBO01_GAGUBUN.GetValue().ToString(),
                                        Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString())
                                        );
            }

            // 월 집계
            if (fsGAM_GUBUN == "INS")
            {
                this.DbConnector.Attach("TY_P_UT_716H1343",
                                        this.CBO01_GAGUBUN.GetValue().ToString(),
                                        fsGMYYMM.ToString(),
                                        fsGMCHK.ToString(),
                                        this.CBH01_GAHWAJU.GetValue().ToString(),
                                        this.CBH01_GAHWAMUL.GetValue().ToString(),
                                        Convert.ToString(fdGMELTIME).ToString(),
                                        Get_Numeric(this.TXT01_GASTTIME.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_GABKQTY.GetValue().ToString()),
                                        Convert.ToString(fdGMKYQTY).ToString(),
                                        Get_Numeric(this.TXT01_GASTQTY.GetValue().ToString()),
                                        Convert.ToString(fdGMELAMT).ToString(),
                                        "0",
                                        Convert.ToString(fdGMKYAMT).ToString(),
                                        Get_Numeric(this.TXT01_GASTAMT.GetValue().ToString()),
                                        "0",
                                        "0",
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper()
                                        );
            }
            else
            {
                this.DbConnector.Attach("TY_P_UT_716H1344",
                                        Convert.ToString(fdGMELTIME).ToString(),
                                        Get_Numeric(this.TXT01_GASTTIME.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_GABKQTY.GetValue().ToString()),
                                        Convert.ToString(fdGMKYQTY).ToString(),
                                        Get_Numeric(this.TXT01_GASTQTY.GetValue().ToString()),
                                        Convert.ToString(fdGMELAMT).ToString(),
                                        "0",
                                        Convert.ToString(fdGMKYAMT).ToString(),
                                        Get_Numeric(this.TXT01_GASTAMT.GetValue().ToString()),
                                        "0",
                                        "0",
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                        this.CBO01_GAGUBUN.GetValue().ToString(),
                                        fsGMYYMM.ToString(),
                                        fsGMCHK.ToString(),
                                        this.CBH01_GAHWAJU.GetValue().ToString(),
                                        this.CBH01_GAHWAMUL.GetValue().ToString()
                                        );
            }

            this.DbConnector.ExecuteTranQueryList();

            if ((TYUserInfo.EmpNo.ToString() == "0311-M" || TYUserInfo.EmpNo.ToString() == "0411-M" || TYUserInfo.EmpNo.ToString() == "0431-M") && this.CBO01_GAGUBUN.GetValue().ToString() == "2")
            {
                this.TXT01_GAELAMT.SetReadOnly(true);
                this.TXT01_GAKYAMT.SetReadOnly(true);
            }

            // 201012월부터 스팀을 사용하지 않고 자체적으로 보일러를 가동함.
            if (int.Parse(Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString())) <= 20101130)
            {
                if ((TYUserInfo.EmpNo.ToString() == "0311-M" || TYUserInfo.EmpNo.ToString() == "0411-M" || TYUserInfo.EmpNo.ToString() == "0431-M") && this.CBO01_GAGUBUN.GetValue().ToString() == "1")
                {
                    this.TXT01_GASTAMT.SetReadOnly(true);
                }
            }
            else
            {
                if ((TYUserInfo.EmpNo.ToString() == "0311-M" || TYUserInfo.EmpNo.ToString() == "0411-M" || TYUserInfo.EmpNo.ToString() == "0431-M") && this.CBO01_GAGUBUN.GetValue().ToString() == "1")
                {
                    this.TXT01_GAELAMT.SetReadOnly(true);
                    this.TXT01_GASTAMT.SetReadOnly(true);
                }
            }

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
            fsGMCHK = "";

            if (this.CBH01_GAHWAJU.GetValue().ToString() == "KHV" ||
                this.CBH01_GAHWAJU.GetValue().ToString() == "KPR" ||
                this.CBH01_GAHWAJU.GetValue().ToString() == "TKS")
            {
                fsGMCHK = "2";
            }
            else
            {
                fsGMCHK = "1";
            }

            this.DbConnector.CommandClear();

            // 가열료 삭제
            this.DbConnector.Attach("TY_P_UT_716GR334",
                                    this.CBO01_GAGUBUN.GetValue().ToString(),
                                    Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString()),
                                    this.CBH01_GAHWAJU.GetValue().ToString(),
                                    this.CBH01_GAHWAMUL.GetValue().ToString()
                                    );

            // 가열료 월 집계 삭제
            this.DbConnector.Attach("TY_P_UT_716HM345",
                                    this.CBO01_GAGUBUN.GetValue().ToString(),
                                    Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString()).Substring(0, 6).ToString(),
                                    fsGMCHK.ToString(),
                                    this.CBH01_GAHWAJU.GetValue().ToString(),
                                    this.CBH01_GAHWAMUL.GetValue().ToString()
                                    );

            this.DbConnector.ExecuteTranQueryList();

            if ((TYUserInfo.EmpNo.ToString() == "0311-M" || TYUserInfo.EmpNo.ToString() == "0411-M" || TYUserInfo.EmpNo.ToString() == "0431-M") && this.CBO01_GAGUBUN.GetValue().ToString() == "2")
            {
                this.TXT01_GAELAMT.SetReadOnly(true);
                this.TXT01_GAKYAMT.SetReadOnly(true);
            }

            // 201012월부터 스팀을 사용하지 않고 자체적으로 보일러를 가동함.
            if (int.Parse(Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString())) <= 20101130)
            {
                if ((TYUserInfo.EmpNo.ToString() == "0311-M" || TYUserInfo.EmpNo.ToString() == "0411-M" || TYUserInfo.EmpNo.ToString() == "0431-M") && this.CBO01_GAGUBUN.GetValue().ToString() == "1")
                {
                    this.TXT01_GASTAMT.SetReadOnly(true);
                }
            }
            else
            {
                if ((TYUserInfo.EmpNo.ToString() == "0311-M" || TYUserInfo.EmpNo.ToString() == "0411-M" || TYUserInfo.EmpNo.ToString() == "0431-M") && this.CBO01_GAGUBUN.GetValue().ToString() == "1")
                {
                    this.TXT01_GAELAMT.SetReadOnly(true);
                    this.TXT01_GASTAMT.SetReadOnly(true);
                }
            }

            UP_TXTBOX_ReadOnly("");
            UP_BUTTON_Visible("");

            UP_FieldClear("DEL");

            SetFocus(this.CBO01_GAGUBUN);

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 월별 가열료 생성 버튼
        private void BTN61_CREATE_Click(object sender, EventArgs e)
        {
            // 파라미터값 보내기
            if ((new TYUTIL004B()).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region Description : DRUM 포장 전체 조회 메소드
        private void UP_SEARCH()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_714DR309",
                Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                Get_Date(this.DTP01_EDDATE.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_714EF315.SetValue(dt);

            double dGASTTIME = 0;
            double dGASTQTY = 0;
            double dGAELTIME = 0;
            double dGAKYQTY = 0;

            double dGADKQTY = 0;

            double dHAPGASTAMT = 0;
            double dHAPGAELAMT = 0;
            double dHAPGAKYAMT = 0;
            double dHAPGADKAMT = 0;

            for (int i = 0; i < this.FPS91_TY_S_UT_714EF315.ActiveSheet.RowCount; i++)
            {
                dGASTTIME = dGASTTIME + double.Parse(Get_Numeric(this.FPS91_TY_S_UT_714EF315.GetValue(i, "GASTTIME").ToString()));
                dGASTQTY = dGASTQTY + double.Parse(Get_Numeric(this.FPS91_TY_S_UT_714EF315.GetValue(i, "GASTQTY").ToString()));
                dGAELTIME = dGAELTIME + double.Parse(Get_Numeric(this.FPS91_TY_S_UT_714EF315.GetValue(i, "GAELTIME").ToString()));
                dGAKYQTY = dGAKYQTY + double.Parse(Get_Numeric(this.FPS91_TY_S_UT_714EF315.GetValue(i, "GAKYQTY").ToString()));
                dGADKQTY = dGADKQTY + double.Parse(Get_Numeric(this.FPS91_TY_S_UT_714EF315.GetValue(i, "GADKQTY").ToString()));

                dHAPGASTAMT = dHAPGASTAMT + double.Parse(Get_Numeric(this.FPS91_TY_S_UT_714EF315.GetValue(i, "GASTAMT").ToString()));
                dHAPGAELAMT = dHAPGAELAMT + double.Parse(Get_Numeric(this.FPS91_TY_S_UT_714EF315.GetValue(i, "GAELAMT").ToString()));
                dHAPGAKYAMT = dHAPGAKYAMT + double.Parse(Get_Numeric(this.FPS91_TY_S_UT_714EF315.GetValue(i, "GAKYAMT").ToString()));
                dHAPGADKAMT = dHAPGADKAMT + double.Parse(Get_Numeric(this.FPS91_TY_S_UT_714EF315.GetValue(i, "GADKAMT").ToString()));
            }

            this.TXT01_HAPGASTTIME.SetValue(string.Format("{0:#,#}", dGASTTIME));
            this.TXT01_HAPSTQTY.SetValue(string.Format("{0,9:N3}", dGASTQTY));
            this.TXT01_HAPKYTIM.SetValue(string.Format("{0:#,#}", dGAELTIME));
            this.TXT01_HAPBKQTY.SetValue(string.Format("{0,9:N3}", dGAKYQTY));

            this.TXT01_HAPGASTAMT.SetValue(string.Format("{0:#,#}", dHAPGASTAMT));
            this.TXT01_HAPGAELAMT.SetValue(string.Format("{0:#,#}", dHAPGAELAMT));
            this.TXT01_HAPGAKYAMT.SetValue(string.Format("{0:#,#}", dHAPGAKYAMT));
        }
        #endregion

        #region Description : 총 사용량 및 사용시간 가져오기
        private void UP_GET_Total()
        {
            DataTable dt = new DataTable();

            // 벙커 C유
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_715I0319",
                this.CBO01_GAGUBUN.GetValue().ToString(),           // 구분
                Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString()) // 작업일자
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_KYTIM.SetValue(dt.Rows[0]["BKTIM"].ToString());
            }

            // 경유
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_715I0320",
                this.CBO01_GAGUBUN.GetValue().ToString(),           // 구분
                Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString()) // 작업일자
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_KYQTY.SetValue(dt.Rows[0]["KYQTY"].ToString());
            }

            // 스팀
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_715I1321",
                this.CBO01_GAGUBUN.GetValue().ToString(),           // 구분
                Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString()) // 작업일자
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_STTIM.SetValue(dt.Rows[0]["STTIM"].ToString());
                this.TXT01_STQTY.SetValue(dt.Rows[0]["STQTY"].ToString());
            }
        }
        #endregion

        #region Description : 확인 메소드
        private void UP_RUN()
        {
            fdGMKYQTY = 0;
            fdGMELAMT = 0;
            fdGMKYAMT = 0;
            fdGMELTIME = 0;

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_714HR317",
                this.CBO01_GAGUBUN.GetValue().ToString(),            // 구분
                Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString()), // 작업일자
                this.CBH01_GAHWAJU.GetValue().ToString(),            // 화주
                this.CBH01_GAHWAMUL.GetValue().ToString()            // 화물
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");

                fsGASTAMT = dt.Rows[0]["GASTAMT"].ToString();
                fsGASTTIME = dt.Rows[0]["GASTTIME"].ToString();
                fsGASTQTY = dt.Rows[0]["GASTQTY"].ToString();

                fdGMKYQTY = double.Parse(dt.Rows[0]["GAKYQTY"].ToString());
                fdGMELAMT = double.Parse(dt.Rows[0]["GAELAMT"].ToString());
                fdGMKYAMT = double.Parse(dt.Rows[0]["GAKYAMT"].ToString());
                fdGMELTIME = double.Parse(dt.Rows[0]["GAELTIME"].ToString());

                fsGUBUN = "UPT";

                UP_BUTTON_Visible(fsGUBUN);

                UP_TXTBOX_ReadOnly("UPT");

                SetFocus(this.TXT01_STTIM);

                if ((TYUserInfo.EmpNo.ToString() == "0311-M" || TYUserInfo.EmpNo.ToString() == "0411-M" || TYUserInfo.EmpNo.ToString() == "0431-M") && this.CBO01_GAGUBUN.GetValue().ToString() == "2")
                {
                    this.TXT01_GAELAMT.SetReadOnly(false);
                    this.TXT01_GAKYAMT.SetReadOnly(false);
                }

                // 201012월부터 스팀을 사용하지 않고 자체적으로 보일러를 가동함.
                if (int.Parse(Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString())) <= 20101130)
                {
                    if ((TYUserInfo.EmpNo.ToString() == "0311-M" || TYUserInfo.EmpNo.ToString() == "0411-M" || TYUserInfo.EmpNo.ToString() == "0431-M") && this.CBO01_GAGUBUN.GetValue().ToString() == "1")
                    {
                        this.TXT01_GASTAMT.SetReadOnly(false);
                    }
                }
                else
                {
                    if ((TYUserInfo.EmpNo.ToString() == "0311-M" || TYUserInfo.EmpNo.ToString() == "0411-M" || TYUserInfo.EmpNo.ToString() == "0431-M") && this.CBO01_GAGUBUN.GetValue().ToString() == "1")
                    {
                        this.TXT01_GAELAMT.SetReadOnly(false);
                        this.TXT01_GASTAMT.SetReadOnly(false);
                    }
                }
            }
        }
        #endregion

        #region Description : 저장 ProcessCheck
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            if (fsGUBUN == "NEW") // 저장
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_714HR317",
                    this.CBO01_GAGUBUN.GetValue().ToString(),            // 구분
                    Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString()), // 작업일자
                    this.CBH01_GAHWAJU.GetValue().ToString(),            // 화주
                    this.CBH01_GAHWAMUL.GetValue().ToString()            // 화물
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_7B495940");
                    SetFocus(this.CBO01_GAGUBUN);

                    e.Successed = false;
                    return;
                }
            }

            if (Get_Numeric(this.TXT01_STTIM.GetValue().ToString()) == "0" && Get_Numeric(this.TXT01_STQTY.GetValue().ToString()) == "0" &&
                Get_Numeric(this.TXT01_KYTIM.GetValue().ToString()) == "0" && Get_Numeric(this.TXT01_KYQTY.GetValue().ToString()) == "0")
            {
                this.ShowMessage("TY_M_UT_716BS325");
                SetFocus(this.TXT01_STTIM);

                e.Successed = false;
                return;
            }

            string sTempDate = string.Empty;
            string sTempDD = string.Empty;
            string sTempMM = string.Empty;
            string sTempYY = string.Empty;


            if (this.CBH01_GAHWAJU.GetValue().ToString() != "KHV" || this.CBH01_GAHWAJU.GetValue().ToString() != "KPR" || this.CBH01_GAHWAJU.GetValue().ToString() != "TKS")
            {
                if (this.CBH01_GAHWAJU.GetValue().ToString() == "SSS")
                {
                    sTempYY = Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString()).Substring(0, 4);
                    sTempMM = Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString()).Substring(4, 2);
                    sTempDD = Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString()).Substring(6, 2);

                    if (Convert.ToDouble(Get_Numeric(Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString()).Substring(0, 6))) >= 202002)
                    {
                        if (int.Parse(sTempDD) > 20)
                        {
                            sTempMM = Set_Fill2(Convert.ToString(Convert.ToInt16(sTempMM) + 1));
                            if (int.Parse(sTempMM) > 12)
                            {
                                sTempYY = Convert.ToString(Convert.ToInt16(sTempYY) + 1);
                                sTempMM = "01";
                            }

                            sTempDate = sTempYY + sTempMM;
                        }
                    }
                    else
                    {
                        if (int.Parse(sTempDD) > 25)
                        {
                            sTempMM = Set_Fill2(Convert.ToString(Convert.ToInt16(sTempMM) + 1));
                            if (int.Parse(sTempMM) > 12)
                            {
                                sTempYY = Convert.ToString(Convert.ToInt16(sTempYY) + 1);
                                sTempMM = "01";
                            }

                            sTempDate = sTempYY + sTempMM;
                        }
                    }

                    sTempDate = sTempYY + sTempMM;
                }
                else
                {
                    sTempDate = Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString()).Substring(0, 6);
                }
            }
            else
            {
                sTempDD = Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString()).Substring(6, 2);
                sTempMM = Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString()).Substring(4, 2);
                sTempYY = Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString()).Substring(0, 4);

                if (Convert.ToDouble(Get_Numeric(Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString()).Substring(0, 6))) >= 202002)
                {
                    if (int.Parse(sTempDD) > 25)
                    {
                        sTempMM = Set_Fill2(Convert.ToString(Convert.ToInt16(sTempMM) + 1));
                        if (int.Parse(sTempMM) > 12)
                        {
                            sTempYY = Convert.ToString(Convert.ToInt16(sTempYY) + 1);
                            sTempMM = "01";
                        }

                        sTempDate = sTempYY + sTempMM;
                    }
                }
                else
                {
                    if (int.Parse(sTempDD) > 20)
                    {
                        sTempMM = Set_Fill2(Convert.ToString(Convert.ToInt16(sTempMM) + 1));
                        if (int.Parse(sTempMM) > 12)
                        {
                            sTempYY = Convert.ToString(Convert.ToInt16(sTempYY) + 1);
                            sTempMM = "01";
                        }

                        sTempDate = sTempYY + sTempMM;
                    }
                }
            }

            string sDNMOTER1 = "";
            string sDNMOTER2 = "";
            string sDNYUL = "";
            string sDNELECT = "";
            string sDNBKCU = "";
            string sDNKYUNG = "";
            string sDNSKSTEAM = "";
            string sDNSKTAMT = "0";

            string sDNSTDANGA = "0"; // 자체 스팀단가
            string sDNSTAMT = "0"; // 자체 스팀총금액
            string sDNSELDANGA = "0"; // 전기료단가(상하)
            string sDNSELAMT = "0"; // 전기총금액(상하)

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_716BT326",
                sTempDate.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_716BU327");
                SetFocus(this.TXT01_STTIM);

                e.Successed = false;
                return;
            }
            else
            {
                sDNMOTER1 = Get_Numeric(dt.Rows[0]["DNMOTER1"].ToString());
                sDNMOTER2 = Get_Numeric(dt.Rows[0]["DNMOTER2"].ToString());
                sDNYUL = Get_Numeric(dt.Rows[0]["DNYUL"].ToString());
                sDNELECT = Get_Numeric(dt.Rows[0]["DNELECT"].ToString());
                sDNBKCU = Get_Numeric(dt.Rows[0]["DNBKCU"].ToString());
                sDNSKSTEAM = Get_Numeric(dt.Rows[0]["DNSKSTEAM"].ToString());
                sDNKYUNG = Get_Numeric(dt.Rows[0]["DNKYUNG"].ToString());
                sDNSKTAMT = Get_Numeric(dt.Rows[0]["DNSKTAMT"].ToString());

                sDNSTDANGA = Get_Numeric(dt.Rows[0]["DNSTDANGA"].ToString());  // 자체 스팀단가
                sDNSTAMT = Get_Numeric(dt.Rows[0]["DNSTAMT"].ToString());    // 자체 스팀총금액
                sDNSELDANGA = Get_Numeric(dt.Rows[0]["DNSELDANGA"].ToString()); // 전기료 단가(상하)
                sDNSELAMT = Get_Numeric(dt.Rows[0]["DNSELAMT"].ToString());   // 전기총금액(상하)

                // 전기료
                if (int.Parse(Get_Numeric(this.TXT01_GAELTIME.GetValue().ToString())) > 0)
                {
                    if (this.CBO01_GAGUBUN.GetValue().ToString() == "1")
                    {
                        string sGAELAMT = string.Empty;

                        // 201012월부터 상하단지 전기료는
                        // UTILITY단가등록에 등록된
                        // 전기료 단가 * 전기사용시간 = 전기사용금액임
                        if (int.Parse(Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString())) > 20101130)
                        {
                            // 전기사용금액 = 전기료 단가 * 전기사용시간

                            //if (fsGUBUN == "NEW") // 저장
                            //{
                            sGAELAMT = UP_DotDelete(Convert.ToString(
                                Convert.ToDouble(Get_Numeric(this.TXT01_GAELTIME.GetValue().ToString())) *
                                Convert.ToDouble(sDNSELDANGA) * 0.1));

                            sGAELAMT = Convert.ToString(double.Parse(sGAELAMT) * 10);
                            //}
                            //else
                            //{
                            //    sGAELAMT = UP_DotDelete(Convert.ToString(
                            //        Convert.ToDouble(Get_Numeric(this.TXT01_GAELTIME.GetValue().ToString())) *
                            //        Convert.ToDouble(sDNSELDANGA) * 0.1));
                            //}

                            if (sGAELAMT.ToString() != Get_Numeric(this.TXT01_GAELAMT.GetValue().ToString()))
                            {
                                if (double.Parse(Get_Numeric(this.TXT01_GAELAMT.GetValue().ToString())) == 0)
                                {
                                    this.TXT01_GAELAMT.SetValue(sGAELAMT);
                                }
                            }

                            if (this.CBH01_GAHWAJU.GetValue().ToString() == "TYC")
                            {
                                this.DbConnector.CommandClear();
                                this.DbConnector.Attach
                                    (
                                    "TY_P_UT_716C0328",
                                    Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString())
                                    );

                                dt = this.DbConnector.ExecuteDataTable();

                                if (dt.Rows.Count > 0)
                                {
                                    this.TXT01_GAELAMT.SetValue(Convert.ToString
                                        (
                                        double.Parse(sDNSELAMT) - double.Parse(dt.Rows[0]["GAELAMT"].ToString())
                                        ));
                                }
                            }
                        }
                        else
                        {
                            if (this.CBH01_GAHWAJU.GetValue().ToString() == "TYC")
                            {
                                // 1원 절사
                                this.TXT01_GAELAMT.SetValue(UP_DotDelete(Convert.ToString(
                                    Convert.ToDouble(Get_Numeric(this.TXT01_GAELTIME.GetValue().ToString())) *
                                    Convert.ToDouble(sDNMOTER1) *
                                    Convert.ToDouble(sDNYUL) *
                                    Convert.ToDouble(sDNELECT) * 0.1)));

                                this.TXT01_GAELAMT.SetValue(Convert.ToString(double.Parse(this.TXT01_GAELAMT.GetValue().ToString()) * 10));
                            }
                            else
                            {
                                // 1원 절사
                                this.TXT01_GAELAMT.SetValue(UP_DotDelete(Convert.ToString(
                                    Convert.ToDouble(Get_Numeric(this.TXT01_GAELTIME.GetValue().ToString())) *
                                    Convert.ToDouble(sDNMOTER1) *
                                    Convert.ToDouble(sDNYUL) *
                                    Convert.ToDouble(sDNELECT) * 0.1)));

                                this.TXT01_GAELAMT.SetValue(Convert.ToString(double.Parse(this.TXT01_GAELAMT.GetValue().ToString()) * 10));
                            }
                        }
                    }
                    else
                    {
                        if (fsGUBUN == "NEW") // 저장
                        {
                            // 1원 절사
                            this.TXT01_GAELAMT.SetValue(UP_DotDelete(Convert.ToString(
                                Convert.ToDouble(Get_Numeric(this.TXT01_GAELTIME.GetValue().ToString())) *
                                Convert.ToDouble(sDNMOTER2) *
                                Convert.ToDouble(sDNYUL) *
                                Convert.ToDouble(sDNELECT) * 0.1)));

                            this.TXT01_GAELAMT.SetValue(Convert.ToString(double.Parse(this.TXT01_GAELAMT.GetValue().ToString()) * 10));
                        }
                        else
                        {
                            // 1원 절사
                            this.TXT01_GAELAMT.SetValue(UP_DotDelete(Convert.ToString(
                                Convert.ToDouble(Get_Numeric(this.TXT01_GAELTIME.GetValue().ToString())) *
                                Convert.ToDouble(sDNMOTER2) *
                                Convert.ToDouble(sDNYUL) *
                                Convert.ToDouble(sDNELECT))));

                            this.TXT01_GAELAMT.SetValue(this.TXT01_GAELAMT.GetValue().ToString());
                        }


                    }
                }

                //벙커C유
                //if( int.Parse(Get_Numeric(this.TXT01_BKQTY.GetValue().ToString())) > 0 )
                //{

                //    this.TXT01_GABKQTY.SetValue(Convert.ToString(Convert.ToDouble(Get_Numeric(txt01_BKQTY.Text)) *
                //        ( Convert.ToDouble(this.TXT01_GAELTIME.GetValue().ToString()) / Convert.ToDouble(Get_Numeric(txtKYTIM.Text.Trim())))));
                //    /* 원래 소스
                //    txtGABKAMT.Text = Convert.ToString(Convert.ToDouble(Get_Numeric(txtGABKQTY.Text)) * Convert.ToDouble(sDNBKCU));
                //    */

                //    // 1원 절사
                //    txtGABKAMT.Text = UP_DotDelete(Convert.ToString(Convert.ToDouble(Get_Numeric(txtGABKQTY.Text)) * Convert.ToDouble(sDNBKCU) * 0.1));

                //    txtGABKAMT.Text = Convert.ToString(double.Parse(txtGABKAMT.Text) * 10);
                //}

                //경유
                if (double.Parse(Get_Numeric(this.TXT01_GAKYQTY.GetValue().ToString())) > 0)
                {
                    if (fsGUBUN == "NEW") // 저장
                    {
                        // 1원 절사
                        this.TXT01_GAKYAMT.SetValue(UP_DotDelete(Convert.ToString(Convert.ToDouble(Get_Numeric(this.TXT01_GAKYQTY.GetValue().ToString())) * Convert.ToDouble(sDNKYUNG) * 0.1)));

                        this.TXT01_GAKYAMT.SetValue(Convert.ToString(double.Parse(this.TXT01_GAKYAMT.GetValue().ToString()) * 10));
                    }
                    else
                    {
                        // 1원 절사
                        this.TXT01_GAKYAMT.SetValue(UP_DotDelete(Convert.ToString(Convert.ToDouble(Get_Numeric(this.TXT01_GAKYQTY.GetValue().ToString())) * Convert.ToDouble(sDNKYUNG))));
                    }
                }

                //// 등유
                //if( int.Parse(Get_Numeric(txt01_GADKQTY.Text.Trim())) > 0 )
                //{
                //    // 1원 절사
                //    txtGADKAMT.Text = UP_DotDelete(Convert.ToString(Convert.ToDouble(Get_Numeric(txtGADKQTY.Text)) * Convert.ToDouble(sDNSELDANGA) * 0.1));

                //    txtGADKAMT.Text = Convert.ToString(double.Parse(txtGADKAMT.Text) * 10);

                //    if(this.CBH01_GAHWAJU.GetValue().ToString() == "TYC")
                //    {
                //        // 201012월부터 스팀을 사용하지 않고 자체적으로 보일러를 가동함.
                //        if(int.Parse(Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString())) > 20101130)
                //        {
                //            ds = oCom.UP_TYC_GADKAMT(Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString()));

                //            if(ds.Tables[0].Rows.Count > 0 )
                //            {
                //                txtGADKAMT.Text = Convert.ToString
                //                    (
                //                    double.Parse(sDNSTAMT) - double.Parse(ds.Tables[0].Rows[0]["GADKAMT"].ToString())
                //                    );
                //            }
                //        }
                //    }
                //}

                // 20180426 이기훈 주임 수정 요청
                // 스팀사용시간이 마이너스더라도 자동 계산 되도록 수정 요청
                //스팀
                //if (int.Parse(Get_Numeric(this.TXT01_GASTTIME.GetValue().ToString())) > 0)
                //{
                if (int.Parse(Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString())) > 20101130)
                {
                    if (Get_Numeric(sDNSTDANGA.ToString()) != "0")
                    {
                        string sGASTAMT = string.Empty;

                        if (this.CBH01_GAHWAJU.GetValue().ToString() != "TYC")
                        {
                            // 스팀금액  = 화주별 스팀 사용시간 * 스팀 단가
                            sGASTAMT = UP_DotDelete(Convert.ToString(Convert.ToDouble(Get_Numeric(this.TXT01_GASTTIME.GetValue().ToString())) * Convert.ToDouble(sDNSTDANGA) * 0.1));

                            sGASTAMT = Convert.ToString(double.Parse(sGASTAMT) * 10);

                            if (sGASTAMT.ToString() != Get_Numeric(this.TXT01_GASTAMT.GetValue().ToString()))
                            {
                                if (double.Parse(Get_Numeric(this.TXT01_GASTAMT.GetValue().ToString())) == 0)
                                {
                                    this.TXT01_GASTAMT.SetValue(sGASTAMT.ToString());
                                }
                            }
                        }
                        else
                        {
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach
                                (
                                "TY_P_UT_716DQ329",
                                Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString())
                                );

                            dt = this.DbConnector.ExecuteDataTable();

                            if (dt.Rows.Count > 0)
                            {
                                this.TXT01_GASTAMT.SetValue(Convert.ToString
                                    (
                                    double.Parse(sDNSTAMT) -
                                    double.Parse(dt.Rows[0]["GASTAMT"].ToString())
                                    ));
                            }
                        }
                    }
                    else
                    {
                        if (Get_Numeric(this.TXT01_GASTQTY.GetValue().ToString()) != "0" && Get_Numeric(sDNSKSTEAM.ToString()) != "0")
                        {
                            if (this.CBH01_GAHWAJU.GetValue().ToString() != "TYC")
                            {
                                string sGASTAMT = string.Empty;

                                // 1원절사
                                sGASTAMT = UP_DotDelete(Convert.ToString(Convert.ToDouble(Get_Numeric(this.TXT01_GASTQTY.GetValue().ToString())) * Convert.ToDouble(sDNSKSTEAM) * 0.1));

                                sGASTAMT = Convert.ToString(double.Parse(sGASTAMT.ToString()) * 10);

                                // 원단위 절사로 인해 입력된 값을 가져오도록
                                if ((TYUserInfo.EmpNo.ToString() == "0311-M" || TYUserInfo.EmpNo.ToString() == "0411-M" || TYUserInfo.EmpNo.ToString() == "0431-M") && this.CBO01_GAGUBUN.GetValue().ToString() == "1")
                                {
                                    if (Get_Numeric(this.TXT01_GASTAMT.GetValue().ToString()) == Get_Numeric(fsGASTAMT.ToString()))
                                    {
                                        this.TXT01_GASTAMT.Text = sGASTAMT.ToString();
                                    }
                                    else
                                    {
                                        if (Get_Numeric(this.TXT01_GASTTIME.GetValue().ToString().Trim()) != Get_Numeric(fsGASTTIME.ToString()) ||
                                            Get_Numeric(this.TXT01_GASTQTY.Text.Trim()) != Get_Numeric(fsGASTQTY.ToString()))
                                        {
                                            this.TXT01_GASTAMT.Text = sGASTAMT.ToString();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                this.DbConnector.CommandClear();
                                this.DbConnector.Attach
                                    (
                                    "TY_P_UT_716DQ329",
                                    Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString())
                                    );

                                dt = this.DbConnector.ExecuteDataTable();

                                if (dt.Rows.Count > 0)
                                {
                                    this.TXT01_GASTAMT.SetValue(Convert.ToString
                                        (
                                        double.Parse(sDNSTAMT) -
                                        double.Parse(dt.Rows[0]["GASTAMT"].ToString())
                                        ));
                                }
                            }
                        }
                        else
                        {
                            this.TXT01_GASTAMT.Text = "0";
                        }
                    }
                }
                else
                {
                    if (Get_Numeric(this.TXT01_GASTQTY.Text.Trim()) != "0" && Get_Numeric(sDNSKSTEAM.ToString()) != "0")
                    {
                        if (this.CBH01_GAHWAJU.GetValue().ToString() != "TYC")
                        {
                            // 1원절사
                            this.TXT01_GASTAMT.Text = UP_DotDelete(Convert.ToString(Convert.ToDouble(Get_Numeric(this.TXT01_GASTQTY.Text)) * Convert.ToDouble(sDNSKSTEAM) * 0.1));

                            this.TXT01_GASTAMT.Text = Convert.ToString(double.Parse(this.TXT01_GASTAMT.Text) * 10);
                        }
                        else
                        {
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach
                                (
                                "TY_P_UT_716DQ329",
                                Get_Date(this.DTP01_GAYYMMDD.GetValue().ToString())
                                );

                            dt = this.DbConnector.ExecuteDataTable();

                            if (dt.Rows.Count > 0)
                            {
                                this.TXT01_GASTAMT.SetValue(Convert.ToString
                                    (
                                    double.Parse(sDNSTAMT) -
                                    double.Parse(dt.Rows[0]["GASTAMT"].ToString())
                                    ));
                            }
                        }
                    }
                    else
                    {
                        this.TXT01_GASTAMT.Text = "0";
                    }
                }
            }
            //}

            double dGMKYQTY = 0;
            double dGMELAMT = 0;
            double dGMKYAMT = 0;
            double dGMELTIME = 0;


            fsGMCHK = "";

            if (this.CBH01_GAHWAJU.GetValue().ToString() == "KHV" ||
                this.CBH01_GAHWAJU.GetValue().ToString() == "KPR" ||
                this.CBH01_GAHWAJU.GetValue().ToString() == "TKS")
            {
                fsGMCHK = "2";
            }
            else
            {
                fsGMCHK = "1";
            }

            fsGMYYMM = sTempDate.ToString();

            fsGAM_GUBUN = "";

            // 월집계 가열료 파일
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_716FH331",
                this.CBO01_GAGUBUN.GetValue().ToString(),   // 통관화주
                sTempDate.ToString(),                       // 일자
                fsGMCHK.ToString(),                         // 구분
                this.CBH01_GAHWAJU.GetValue().ToString(),   // 화주
                this.CBH01_GAHWAMUL.GetValue().ToString()   // 화물
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                dGMKYQTY = double.Parse(dt.Rows[0]["GMKYQTY"].ToString());
                dGMELAMT = double.Parse(dt.Rows[0]["GMELAMT"].ToString());
                dGMKYAMT = double.Parse(dt.Rows[0]["GMKYAMT"].ToString());
                dGMELTIME = double.Parse(dt.Rows[0]["GMELTIME"].ToString());

                fdGMKYQTY = dGMKYQTY + double.Parse(Get_Numeric(this.TXT01_GAKYQTY.Text.Trim())) - fdGMKYQTY;
                fdGMELAMT = dGMELAMT + double.Parse(Get_Numeric(this.TXT01_GAELAMT.Text.Trim())) - fdGMELAMT;
                fdGMKYAMT = dGMKYAMT + double.Parse(Get_Numeric(this.TXT01_GAKYAMT.Text.Trim())) - fdGMKYAMT;
                fdGMELTIME = dGMELTIME + double.Parse(Get_Numeric(this.TXT01_GAELTIME.Text.Trim())) - fdGMELTIME;

                fsGAM_GUBUN = "UPT";
            }
            else
            {
                fsGAM_GUBUN = "INS";
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
            this.DTP01_GAYYMMDD.SetValue(DateTime.Now.ToString("yyyyMM") + "25");

            if (sGUBUN != "DEL")
            {
                this.CBH01_GAHWAJU.SetValue("");    // 화주
                this.CBH01_GAHWAMUL.SetValue("");   // 화물
            }

            this.TXT01_STTIM.SetValue("");

            this.TXT01_STQTY.SetValue("");
            this.TXT01_KYTIM.SetValue("");
            this.TXT01_KYQTY.SetValue("");


            this.TXT01_GATANK.SetValue("");
            this.TXT01_GASTTIME.SetValue("");
            this.TXT01_GASTQTY.SetValue("");
            this.TXT01_GASTAMT.SetValue("");
            this.TXT01_GAELTIME.SetValue("");
            this.TXT01_GAELAMT.SetValue("");
            this.TXT01_GAKYQTY.SetValue("");
            this.TXT01_GABKQTY.SetValue("");
            this.TXT01_GAKYAMT.SetValue("");
            this.TXT01_STTIMEMSG.SetValue("");
            this.TXT01_STQTYMSG.SetValue("");
            this.TXT01_HAPGASTTIME.SetValue("");
            this.TXT01_HAPSTQTY.SetValue("");
            this.TXT01_HAPGASTAMT.SetValue("");
            this.TXT01_HAPKYTIM.SetValue("");

            this.TXT01_HAPGAELAMT.SetValue("");
            this.TXT01_HAPBKQTY.SetValue("");
            this.TXT01_HAPGAKYAMT.SetValue("");
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
                this.CBO01_GAGUBUN.SetReadOnly(false);
                this.DTP01_GAYYMMDD.SetReadOnly(false);
                this.CBH01_GAHWAJU.SetReadOnly(false);
                this.CBH01_GAHWAMUL.SetReadOnly(false);
            }
            else if (sGUBUN.ToString() == "SAV" || sGUBUN.ToString() == "UPT")
            {
                this.CBO01_GAGUBUN.SetReadOnly(true);
                this.DTP01_GAYYMMDD.SetReadOnly(true);
                this.CBH01_GAHWAJU.SetReadOnly(true);
                this.CBH01_GAHWAMUL.SetReadOnly(true);
            }
            else if (sGUBUN.ToString() == "")
            {
                this.CBO01_GAGUBUN.SetReadOnly(true);
                this.DTP01_GAYYMMDD.SetReadOnly(true);
                this.CBH01_GAHWAJU.SetReadOnly(true);
                this.CBH01_GAHWAMUL.SetReadOnly(true);
            }
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_UT_714EF315_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.CBO01_GAGUBUN.SetValue(this.FPS91_TY_S_UT_714EF315.GetValue("GAGUBUN").ToString());    // 구분
            this.DTP01_GAYYMMDD.SetValue(this.FPS91_TY_S_UT_714EF315.GetValue("GAYYMMDD").ToString());  // 작업일자
            this.CBH01_GAHWAJU.SetValue(this.FPS91_TY_S_UT_714EF315.GetValue("GAHWAJU").ToString());    // 화주
            this.CBH01_GAHWAMUL.SetValue(this.FPS91_TY_S_UT_714EF315.GetValue("GAHWAMUL").ToString());  // 화물

            UP_GET_Total();

            // 확인
            UP_RUN();

            UP_Message_Display();
        }
        #endregion

        #region Description : 메세지 DISPLAY
        private void UP_Message_Display()
        {
            string sGASTTIME = string.Empty;
            string sGASTQTY = string.Empty;

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_716BM324",
                this.DTP01_GAYYMMDD.GetValue().ToString() // 작업일자
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sGASTTIME = dt.Rows[0]["GASTTIME"].ToString();
                sGASTQTY = dt.Rows[0]["GASTQTY"].ToString();
            }

            string sCHA = string.Empty;

            if (Get_Numeric(this.TXT01_STTIM.GetValue().ToString()) != Get_Numeric(sGASTTIME.ToString()))
            {
                if (decimal.Parse(Get_Numeric(this.TXT01_STTIM.GetValue().ToString())) > decimal.Parse(Get_Numeric(sGASTTIME.ToString())))
                {
                    sCHA = Convert.ToString(decimal.Parse(Get_Numeric(this.TXT01_STTIM.GetValue().ToString())) - decimal.Parse(Get_Numeric(sGASTTIME.ToString())));

                    this.TXT01_STTIMEMSG.SetValue("스팀 총시간이 화주별 스팀시간 합계보다 " + sCHA + "만큼 많습니다.");
                }
                else
                {
                    sCHA = Convert.ToString(decimal.Parse(Get_Numeric(sGASTTIME.ToString())) - decimal.Parse(Get_Numeric(this.TXT01_STTIM.GetValue().ToString())));

                    this.TXT01_STTIMEMSG.SetValue("화주별 스팀시간 합계가 스팀 총시간보다 " + sCHA + "만큼 많습니다.");
                }
            }
            else
            {
                this.TXT01_STTIMEMSG.SetValue("");
            }

            if (Get_Numeric(this.TXT01_STQTY.GetValue().ToString()) != Get_Numeric(sGASTQTY.ToString()))
            {
                if (decimal.Parse(Get_Numeric(this.TXT01_STQTY.GetValue().ToString())) > decimal.Parse(Get_Numeric(sGASTQTY.ToString())))
                {
                    sCHA = Convert.ToString(decimal.Parse(Get_Numeric(this.TXT01_STQTY.GetValue().ToString())) - decimal.Parse(Get_Numeric(sGASTQTY.ToString())));

                    this.TXT01_STQTYMSG.SetValue("스팀 총사용량이 화주별 스팀사용량 합계보다 " + sCHA + "만큼 많습니다.");
                }
                else
                {
                    sCHA = Convert.ToString(decimal.Parse(Get_Numeric(sGASTQTY.ToString())) - decimal.Parse(Get_Numeric(this.TXT01_STQTY.GetValue().ToString())));

                    this.TXT01_STQTYMSG.SetValue("화주별 스팀사용량 합계가 스팀 총사용량보다 " + sCHA + "만큼 많습니다.");
                }
            }
            else
            {
                this.TXT01_STQTYMSG.SetValue("");
            }
        }
        #endregion

        #region Description : 화물 이벤트
        private void CBH01_GAHWAMUL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UP_GET_Total();

                SetFocus(this.TXT01_GATANK);
            }
        }
        #endregion

        #region Description : 스팀사용시간 이벤트
        private void TXT01_GASTTIME_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.TXT01_GASTQTY);
            }
        }
        #endregion

        #region Description : 스팀사용수량 이벤트
        private void TXT01_GASTQTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.TXT01_GAELTIME);
            }
        }
        #endregion

        #region Description : 전기사용시간 이벤트
        private void TXT01_GAELTIME_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.TXT01_GAKYQTY);
            }
        }
        #endregion

        #region Description : 경유사용량 이벤트
        private void TXT01_GAKYQTY_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}