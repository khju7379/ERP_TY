using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using System.IO;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Export.Html;
using DataDynamics.ActiveReports.Export.Pdf;

namespace TY.ER.UT00
{
    /// <summary>
    /// 보험영수증(협회용) 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.09.27 15:58
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_66FD4200 : 대표 거래처 코드 조회
    ///  TY_P_UT_79RG0681 : 보험영수증(협회용) 조회
    ///  TY_P_UT_79RG0682 : 보험영수증(협회용) 조회(화주X)
    ///  TY_P_UT_79RGG685 : 보험영수증(협회용) 화주별 집계 출력
    ///  TY_P_UT_79RGG686 : 보험영수증(협회용) 화주별 집계 출력(화주X)
    ///  TY_P_UT_79RGM687 : 보험영수증(협회용) 화주별 영수증 출력
    ///  TY_P_UT_79RGN688 : 보험영수증(협회용) 화주별 영수증 출력(화주X)
    ///  TY_P_UT_79RGN689 : 보험영수증(협회용) 일자별 영수증 출력
    ///  TY_P_UT_79RGO690 : 보험영수증(협회용) 일자별 영수증 출력(화주X)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_79RGP692 : 보험영수증(협회용) 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  CHHWAJU : 화주
    ///  CHHWAMUL : 화물
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYUTME008S : TYBase
    {
        private string fsFileDownPath = string.Empty;

        #region Description : 폼 로드
        public TYUTME008S()
        {
            InitializeComponent();
        }

        private void TYUTME008S_Load(object sender, System.EventArgs e)
        {
            string sSTDATE = string.Empty;
            string sEDDATE = string.Empty;

            sSTDATE = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            sEDDATE = DateTime.Now.ToString("yyyy-MM-dd");

            this.DTP01_STDATE.SetValue(sSTDATE.Substring(0, 8) + "26");
            this.DTP01_EDDATE.SetValue(sEDDATE.Substring(0, 8) + "25");
            this.CBO01_GGUBUN.SetValue("HWAJU");

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sHWAJU = string.Empty;

            // 대표거래처 코드 가져오기
            sHWAJU = Get_VNCODE(this.CBH01_CHHWAJU.GetValue().ToString());

            if (Convert.ToDouble(this.DTP01_STDATE.GetString()) > Convert.ToDouble(this.DTP01_EDDATE.GetString()))
            {
                this.ShowCustomMessage("시작일자가 종료일자보다 클수 없습니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            else
            {
                DataTable dt = new DataTable();

                this.FPS91_TY_S_UT_79RGP692.Initialize();

                this.DbConnector.CommandClear();

                if (sHWAJU != "")
                {
                    this.DbConnector.Attach(
                        "TY_P_UT_79RG0681",
                        this.DTP01_STDATE.GetString(),
                        this.DTP01_EDDATE.GetString(),
                        sHWAJU,
                        this.CBH01_CHHWAMUL.GetValue().ToString()
                        );
                }
                else
                {
                    this.DbConnector.Attach(
                        "TY_P_UT_79RG0682",
                        this.DTP01_STDATE.GetString(),
                        this.DTP01_EDDATE.GetString(),
                        this.CBH01_CHHWAMUL.GetValue().ToString()
                        );
                }
                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_UT_79RGP692.SetValue(dt);
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sHWAJU = string.Empty;

            // 대표거래처 코드 가져오기
            sHWAJU = Get_VNCODE(this.CBH01_CHHWAJU.GetValue().ToString());

            if (Convert.ToDouble(this.DTP01_STDATE.GetString()) > Convert.ToDouble(this.DTP01_EDDATE.GetString()))
            {
                this.ShowCustomMessage("시작일자가 종료일자보다 클수 없습니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            else
            {
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();

                // 영수증 출력
                if(this.CBO01_GAGUBUN.GetValue().ToString() == "1")
                {
                    // 일자별
                    if(this.CBO01_GGUBUN.GetValue().ToString() == "DAY")
                    {
                        if (sHWAJU != "")
                        {
                            this.DbConnector.Attach(
                                "TY_P_UT_79RGN689",
                                this.DTP01_STDATE.GetString(),
                                this.DTP01_EDDATE.GetString(),
                                sHWAJU,
                                this.CBH01_CHHWAMUL.GetValue().ToString()
                                );
                        }
                        else
                        {
                            this.DbConnector.Attach(
                                "TY_P_UT_79RGO690",
                                this.DTP01_STDATE.GetString(),
                                this.DTP01_EDDATE.GetString(),
                                this.CBH01_CHHWAMUL.GetValue().ToString()
                                );
                        }
                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            ActiveReport rpt = new TYUTME008R1();

                            rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                            (new TYERGB001P(rpt, dt)).ShowDialog();
                        }
                        else
                        {
                            this.ShowMessage("TY_M_AC_2422N250");
                        }
                    }
                    // 화주별
                    else
                    {
                        if (sHWAJU != "")
                        {
                            this.DbConnector.Attach(
                                "TY_P_UT_79RGM687",
                                this.DTP01_STDATE.GetString(),
                                this.DTP01_EDDATE.GetString(),
                                sHWAJU,
                                this.CBH01_CHHWAMUL.GetValue().ToString()
                                );
                        }
                        else
                        {
                            this.DbConnector.Attach(
                                "TY_P_UT_79RGN688",
                                this.DTP01_STDATE.GetString(),
                                this.DTP01_EDDATE.GetString(),
                                this.CBH01_CHHWAMUL.GetValue().ToString()
                                );
                        }
                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            ActiveReport rpt = new TYUTME008R1();

                            rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                            (new TYERGB001P(rpt, dt)).ShowDialog();
                        }
                        else
                        {
                            this.ShowMessage("TY_M_AC_2422N250");
                        }
                    }
                }
                // 화주별 집계 출력
                else 
                {
                    string sDATE1 = string.Empty;
                    string sSDATE = this.DTP01_STDATE.GetString();
                    string sEDATE = this.DTP01_EDDATE.GetString();
                    string sDATE = sEDATE.Substring(4, 2);

                    sSDATE = sSDATE.Substring(0, 4) + "." + sSDATE.Substring(4, 2) + "." + sSDATE.Substring(6, 2);
                    sEDATE = sEDATE.Substring(0, 4) + "." + sEDATE.Substring(4, 2) + "." + sEDATE.Substring(6, 2);

                    sDATE1 = "(" + sSDATE + " ~ " + sEDATE + ")";

                    if (sHWAJU != "")
                    {
                        this.DbConnector.Attach(
                            "TY_P_UT_79RGG685",
                            sDATE1,
                            sDATE,
                            this.DTP01_STDATE.GetString(),
                            this.DTP01_EDDATE.GetString(),
                            sHWAJU,
                            this.CBH01_CHHWAMUL.GetValue().ToString()
                            );
                    }
                    else
                    {
                        this.DbConnector.Attach(
                            "TY_P_UT_79RGG686",
                            sDATE1,
                            sDATE,
                            this.DTP01_STDATE.GetString(),
                            this.DTP01_EDDATE.GetString(),                            
                            this.CBH01_CHHWAMUL.GetValue().ToString()
                            );
                    }
                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        ActiveReport rpt = new TYUTME008R2();

                        rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                        (new TYERGB001P(rpt, dt)).ShowDialog();
                    }
                    else
                    {
                        this.ShowMessage("TY_M_AC_2422N250");
                    }
                }
            }
        }
        #endregion

        #region Description : 다운로드 버튼
        private void BTN61_DOWN_Click(object sender, EventArgs e)
        {
            string sGUBUN    = string.Empty;
            string sFileName = string.Empty;

            string sHWAJU    = string.Empty;

            int i = 0;

            // 대표거래처 코드 가져오기
            sHWAJU = Get_VNCODE(this.CBH01_CHHWAJU.GetValue().ToString());

            if (Convert.ToDouble(this.DTP01_STDATE.GetString()) > Convert.ToDouble(this.DTP01_EDDATE.GetString()))
            {
                this.ShowCustomMessage("시작일자가 종료일자보다 클수 없습니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            else
            {
                DataTable dt = new DataTable();
                DataTable dt_hwaju = new DataTable();

                // 영수증 출력
                if (this.CBO01_GAGUBUN.GetValue().ToString() == "1")
                {
                    this.DbConnector.CommandClear();
                    
                    if (sHWAJU != "")
                    {
                        this.DbConnector.Attach("TY_P_UT_83CAT689", this.DTP01_STDATE.GetString(),
                                                                    this.DTP01_EDDATE.GetString(),
                                                                    sHWAJU,
                                                                    this.CBH01_CHHWAMUL.GetValue().ToString()
                                                                    );
                    }
                    else
                    {
                        this.DbConnector.Attach("TY_P_UT_83CHB692", this.DTP01_STDATE.GetString(),
                                                                    this.DTP01_EDDATE.GetString(),
                                                                    this.CBH01_CHHWAMUL.GetValue().ToString()
                                                                    );
                    }
                                                                

                    dt_hwaju = this.DbConnector.ExecuteDataTable();

                    for (i = 0; i < dt_hwaju.Rows.Count; i++)
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_79RGN689", this.DTP01_STDATE.GetString(),
                                                                    this.DTP01_EDDATE.GetString(),
                                                                    dt_hwaju.Rows[i]["ISHWAJU"].ToString(),
                                                                    dt_hwaju.Rows[i]["ISHWAMUL"].ToString()
                                                                    );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            ActiveReport rpt = new TYUTME008R1();

                            sFileName = dt.Rows[0]["VNSANGHO"].ToString() + "-" + dt_hwaju.Rows[i]["ISHWAJU"].ToString() + "-" + dt.Rows[0]["HMDESC1"].ToString() + "-보험료 영수증.pdf";

                            UP_Invoice_PdfFileDown(rpt, dt, this.DTP01_EDDATE.GetString().Substring(0, 6), sFileName.ToString());

                            sGUBUN = "EXISTS";
                        }
                        else
                        {
                            this.ShowMessage("TY_M_AC_2422N250");
                        }
                    }
                }
                // 화주별 집계 출력
                else
                {
                    string sDATE1    = string.Empty;
                    string sSDATE    = this.DTP01_STDATE.GetString();
                    string sEDATE    = this.DTP01_EDDATE.GetString();
                    string sDATE     = sEDATE.Substring(4, 2);

                    sSDATE = sSDATE.Substring(0, 4) + "." + sSDATE.Substring(4, 2) + "." + sSDATE.Substring(6, 2);
                    sEDATE = sEDATE.Substring(0, 4) + "." + sEDATE.Substring(4, 2) + "." + sEDATE.Substring(6, 2);

                    sDATE1 = "(" + sSDATE + " ~ " + sEDATE + ")";

                    this.DbConnector.CommandClear();
                    if (sHWAJU != "")
                    {
                        this.DbConnector.Attach("TY_P_UT_83CAT689", this.DTP01_STDATE.GetString(),
                                                                    this.DTP01_EDDATE.GetString(),
                                                                    sHWAJU,
                                                                    this.CBH01_CHHWAMUL.GetValue().ToString()
                                                                    );
                    }
                    else
                    {
                        this.DbConnector.Attach("TY_P_UT_83CHB692", this.DTP01_STDATE.GetString(),
                                                                    this.DTP01_EDDATE.GetString(),
                                                                    this.CBH01_CHHWAMUL.GetValue().ToString()
                                                                    );
                    }

                    dt_hwaju = this.DbConnector.ExecuteDataTable();

                    for (i = 0; i < dt_hwaju.Rows.Count; i++)
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_79RGG685", sDATE1,
                                                                    sDATE,
                                                                    this.DTP01_STDATE.GetString(),
                                                                    this.DTP01_EDDATE.GetString(),
                                                                    dt_hwaju.Rows[i]["ISHWAJU"].ToString(),
                                                                    dt_hwaju.Rows[i]["ISHWAMUL"].ToString()
                                                                    );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            ActiveReport rpt = new TYUTME008R2();

                            sFileName = dt.Rows[0]["VNSANGHO"].ToString() + "-" + dt.Rows[0]["ISHWAJU"].ToString() + "-" + dt.Rows[0]["HMDESC1"].ToString() + "-보험료 집계.pdf";

                            UP_Invoice_PdfFileDown(rpt, dt, this.DTP01_EDDATE.GetString().Substring(0, 6), sFileName.ToString());

                            sGUBUN = "EXISTS";
                        }
                        else
                        {
                            this.ShowMessage("TY_M_AC_2422N250");
                        }
                    }
                }
            }

            if (sGUBUN.ToString() != "")
            {
                this.ShowMessage("TY_M_UT_7CCDS246");
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Description : 경로 지정
        private void BTN61_SEARCH_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtFolder.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }
        #endregion

        #region Description : pdf 다운로드
        private void UP_Invoice_PdfFileDown(ActiveReport rpt, DataTable dt, string sYYMM, string sFileName)
        {
            DataDynamics.ActiveReports.Document.Document doc;

            try
            {
                fsFileDownPath = txtFolder.Text + "\\" + sYYMM + "\\";

                if (Directory.Exists(fsFileDownPath) == false)
                {
                    Directory.CreateDirectory(fsFileDownPath);
                }

                rpt.DataSource = dt;
                rpt.Run(false);

                string sfilename = fsFileDownPath + "\\" + sFileName;

                object export = null;

                doc = rpt.Document;

                export = new PdfExport();

                ((PdfExport)export).Export(doc, sfilename);
            }
            catch
            {

            }
            finally
            {

            }
        }
        #endregion
    }
}
