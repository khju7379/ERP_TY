using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using System.IO;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Export.Html;
using GrapeCity.ActiveReports.Export.Pdf;
using GrapeCity.ActiveReports.Export.Pdf.Section;

namespace TY.ER.UT00
{
    /// <summary>
    /// 화물료 거래명세서 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.09.01 13:27
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_791DS515 : 화물료 거래명세서 출력
    ///  TY_P_UT_791DS516 : 화물료 거래명세서 출력(화주x)
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  CHHWAJU : 화주
    ///  CHHWAMUL : 화물
    ///  SVIPHANG : 입항일자
    ///  UTDATE : 매출일자
    /// </summary>
    public partial class TYUTME030P : TYBase
    {
        private string fsFileDownPath = string.Empty;

        #region Description : 폼 로드
        public TYUTME030P()
        {
            InitializeComponent();
        }

        private void TYUTME030P_Load(object sender, System.EventArgs e)
        {
            this.DTP01_UTDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_SVIPHANG.SetValue("");

            SetStartingFocus(this.DTP01_UTDATE);
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sProcedure = string.Empty;
            string sIPDATE = string.Empty;

            if (this.DTP01_SVIPHANG.GetString() == "" || this.DTP01_SVIPHANG.GetString() == "19000101")
            {
                sIPDATE = "";
            }
            else
            {
                sIPDATE = this.DTP01_SVIPHANG.GetString();
            }

            DataTable dt = new DataTable();

            if (double.Parse(Get_Date(this.DTP01_UTDATE.GetString())) <= 20171231)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_791DS515", this.DTP01_UTDATE.GetString(),
                                                            this.CBH01_CHHWAJU.GetValue().ToString(),
                                                            this.CBH01_CHHWAMUL.GetValue().ToString(),
                                                            sIPDATE);

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    SectionReport rpt = new TYUTME030R();

                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Default;

                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
                else
                {
                    this.ShowMessage("TY_M_AC_2422N250");
                }
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_7BHDX033", this.DTP01_UTDATE.GetString(),
                                                            this.CBH01_CHHWAJU.GetValue().ToString(),
                                                            this.CBH01_CHHWAMUL.GetValue().ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    SectionReport rpt = new TYUTME030R1(this.DTP01_UTDATE.GetString());

                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Default;

                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
                else
                {
                    this.ShowMessage("TY_M_AC_2422N250");
                }
            }
        }
        #endregion

        #region Description : 경로지정 버튼
        private void BTN61_SEARCH_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtFolder.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }
        #endregion

        #region Description : 다운로드 버튼
        private void BTN61_DOWN_Click(object sender, EventArgs e)
        {
            if (txtFolder.Text.Trim() == "")
            {
                this.ShowMessage("TY_M_UT_81PBD533");
            }
            else
            {
                string sGUBUN = string.Empty;
                string sFileName = string.Empty;

                string sProcedure = string.Empty;
                string sIPDATE = string.Empty;

                int i = 0;

                if (this.DTP01_SVIPHANG.GetString() == "" || this.DTP01_SVIPHANG.GetString() == "19000101")
                {
                    sIPDATE = "";
                }
                else
                {
                    sIPDATE = this.DTP01_SVIPHANG.GetString();
                }

                DataTable dt_hwaju = new DataTable();
                DataTable dt = new DataTable();

                if (double.Parse(Get_Date(this.DTP01_UTDATE.GetString())) <= 20171231)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_7CCF0249", this.DTP01_UTDATE.GetString(),
                                                                this.CBH01_CHHWAJU.GetValue().ToString(),
                                                                this.CBH01_CHHWAMUL.GetValue().ToString(),
                                                                sIPDATE
                                                                );

                    dt_hwaju = this.DbConnector.ExecuteDataTable();

                    for (i = 0; i < dt_hwaju.Rows.Count; i++)
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_791DS515", dt_hwaju.Rows[i]["M1DATE"].ToString(),
                                                                    dt_hwaju.Rows[i]["M1HWAJU"].ToString(),
                                                                    dt_hwaju.Rows[i]["M1HWAMUL"].ToString(),
                                                                    dt_hwaju.Rows[i]["M1IPHANG"].ToString()
                                                                    );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            SectionReport rpt = new TYUTME030R();

                            sFileName = "화물료-" + dt.Rows[0]["M1HWAJU"].ToString() + "-" + dt_hwaju.Rows[i]["M1HWAMUL"].ToString() + "-" + dt_hwaju.Rows[i]["M1IPHANG"].ToString() + ".pdf";

                            UP_Invoice_PdfFileDown(rpt, dt, this.DTP01_UTDATE.GetString().Substring(0, 6), sFileName.ToString());

                            sGUBUN = "EXISTS";
                        }
                    }
                }
                else
                {
                    string sM3IPHANG = string.Empty;

                    this.DbConnector.CommandClear();

                    if (sIPDATE.ToString() == "")
                    {
                        this.DbConnector.Attach("TY_P_UT_81QD0545", this.DTP01_UTDATE.GetString(),
                                                                    this.CBH01_CHHWAJU.GetValue().ToString()
                                                                    );

                        dt_hwaju = this.DbConnector.ExecuteDataTable();
                    }
                    else
                    {
                        this.DbConnector.Attach("TY_P_UT_7CCFC250", this.DTP01_UTDATE.GetString(),
                                                                    this.CBH01_CHHWAJU.GetValue().ToString(),
                                                                    sIPDATE.ToString()
                                                                    );

                        dt_hwaju = this.DbConnector.ExecuteDataTable();
                    }

                    for (i = 0; i < dt_hwaju.Rows.Count; i++)
                    {
                        if (sIPDATE.ToString() == "")
                        {
                            sM3IPHANG = "";
                        }
                        else
                        {
                            sM3IPHANG = dt_hwaju.Rows[i]["M3IPHANG"].ToString();
                        }

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_7BHDX033", dt_hwaju.Rows[i]["M3DATE"].ToString(),
                                                                    dt_hwaju.Rows[i]["M3HWAJU"].ToString(),
                                                                    sM3IPHANG.ToString()
                                                                    );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            SectionReport rpt = new TYUTME030R1(this.DTP01_UTDATE.GetString());

                            if (sM3IPHANG.ToString() == "")
                            {
                                sFileName = dt.Rows[0]["VNSANGHO"].ToString() + "-" + dt.Rows[0]["M1HWAJU"].ToString() + "-화물료.pdf";
                            }
                            else
                            {
                                sFileName = dt.Rows[0]["VNSANGHO"].ToString() + "-" + sM3IPHANG.ToString() + "-" + dt.Rows[0]["M1HWAJU"].ToString() + "-화물료.pdf";
                            }

                            UP_Invoice_PdfFileDown(rpt, dt, this.DTP01_UTDATE.GetString().Substring(0, 6), sFileName.ToString());

                            sGUBUN = "EXISTS";
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
        }
        #endregion

        #region Description : pdf 다운로드
        private void UP_Invoice_PdfFileDown(GrapeCity.ActiveReports.SectionReport rpt, DataTable dt, string sYYMM, string sFileName)
        {
            GrapeCity.ActiveReports.Document.SectionDocument doc;

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
            catch (Exception ex)
            {
                string sErr = string.Empty;

                sErr = ex.ToString();
            }
            finally
            {

            }
        }
        #endregion
    }
}
