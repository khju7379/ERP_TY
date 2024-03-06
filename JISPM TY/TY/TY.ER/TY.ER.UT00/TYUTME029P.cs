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
    /// 매출 집계표 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.09.01 18:58
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_794HD528 : 매출 집계표 출력
    ///  TY_P_UT_794HE529 : 매출 집계표 출력(화주X)
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
    ///  UTDATE : 매출일자
    /// </summary>
    public partial class TYUTME029P : TYBase
    {
        private string fsFileDownPath = string.Empty;

        #region Description : 폼 로드
        public TYUTME029P()
        {
            InitializeComponent();
        }

        private void TYUTME029P_Load(object sender, System.EventArgs e)
        {
            this.DTP01_UTDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM"));
            SetStartingFocus(this.DTP01_UTDATE);
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            // 대표 거래처 코드 조회
            string sHWAJU = Get_VNCODE(this.CBH01_CHHWAJU.GetValue().ToString());

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            if (int.Parse(this.DTP01_UTDATE.GetValue().ToString()) <= 201712)
            {
                if (sHWAJU != "")
                {
                    this.DbConnector.Attach("TY_P_UT_794HD528", this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                sHWAJU,
                                                                this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                sHWAJU,
                                                                this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                sHWAJU);
                }
                else
                {
                    this.DbConnector.Attach("TY_P_UT_794HE529", this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                this.DTP01_UTDATE.GetString().Substring(0, 6));
                }
            }
            else
            {
                if (sHWAJU != "")
                {
                    if (this.CBO01_GPRTGN.GetValue().ToString() == "N")
                    {
                        this.DbConnector.Attach("TY_P_UT_C4JGE271", this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                    sHWAJU,
                                                                    this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                    sHWAJU,
                                                                    this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                    sHWAJU,
                                                                    this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                    sHWAJU);
                    }
                    else
                    {
                        this.DbConnector.Attach("TY_P_UT_7BKB5053", this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                    sHWAJU,
                                                                    this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                    sHWAJU,
                                                                    this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                    sHWAJU,
                                                                    this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                    sHWAJU);
                    }
                }
                else
                {
                    if (this.CBO01_GPRTGN.GetValue().ToString() == "N")
                    {
                        this.DbConnector.Attach("TY_P_UT_C4JGO272", this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                    this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                    this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                    this.DTP01_UTDATE.GetString().Substring(0, 6));
                    }
                    else
                    {
                        this.DbConnector.Attach("TY_P_UT_7BKBD055", this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                    this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                    this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                    this.DTP01_UTDATE.GetString().Substring(0, 6));
                    }
                }
            }

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (int.Parse(this.DTP01_UTDATE.GetValue().ToString()) <= 201712)
                {
                    SectionReport rpt = new TYUTME029R(this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                      System.DateTime.Now.ToString("yyyy-MM-dd"));

                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Default;

                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
                else
                {
                    SectionReport rpt = new TYUTME029R2(this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                      System.DateTime.Now.ToString("yyyy-MM-dd"));

                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Default;

                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
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

                int i = 0;

                // 대표 거래처 코드 조회
                string sHWAJU = Get_VNCODE(this.CBH01_CHHWAJU.GetValue().ToString());

                DataTable dt_hwaju = new DataTable();
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();

                if (int.Parse(this.DTP01_UTDATE.GetValue().ToString()) <= 201712)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_7CCG9252", this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                this.CBH01_CHHWAJU.GetValue().ToString(),
                                                                this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                this.CBH01_CHHWAJU.GetValue().ToString(),
                                                                this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                this.CBH01_CHHWAJU.GetValue().ToString()
                                                                );

                    dt_hwaju = this.DbConnector.ExecuteDataTable();

                    for (i = 0; i < dt_hwaju.Rows.Count; i++)
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_794HD528", this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                    dt_hwaju.Rows[i]["HWAJU"].ToString(),
                                                                    this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                    dt_hwaju.Rows[i]["HWAJU"].ToString(),
                                                                    this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                    dt_hwaju.Rows[i]["HWAJU"].ToString()
                                                                    );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            SectionReport rpt = new TYUTME029R(this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                              System.DateTime.Now.ToString("yyyy-MM-dd"));

                            sFileName = dt.Rows[0]["VNSANGHO"].ToString() + "-매출청구서.pdf";

                            UP_Invoice_PdfFileDown(rpt, dt, this.DTP01_UTDATE.GetString().Substring(0, 6), sFileName.ToString());

                            sGUBUN = "EXISTS";
                        }
                    }
                }
                else
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_UT_7CCGB253", this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                this.CBH01_CHHWAJU.GetValue().ToString(),
                                                                this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                this.CBH01_CHHWAJU.GetValue().ToString(),
                                                                this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                this.CBH01_CHHWAJU.GetValue().ToString(),
                                                                this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                this.CBH01_CHHWAJU.GetValue().ToString()
                                                                );

                    dt_hwaju = this.DbConnector.ExecuteDataTable();

                    for (i = 0; i < dt_hwaju.Rows.Count; i++)
                    {
                        if (this.CBO01_GPRTGN.GetValue().ToString() == "N")
                        {
                            this.DbConnector.Attach("TY_P_UT_C4JGE271", this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                        dt_hwaju.Rows[i]["HWAJU"].ToString(),
                                                                        this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                        dt_hwaju.Rows[i]["HWAJU"].ToString(),
                                                                        this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                        dt_hwaju.Rows[i]["HWAJU"].ToString(),
                                                                        this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                        dt_hwaju.Rows[i]["HWAJU"].ToString()
                                                                        );
                        }
                        else
                        {
                            this.DbConnector.Attach("TY_P_UT_7BKB5053", this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                        dt_hwaju.Rows[i]["HWAJU"].ToString(),
                                                                        this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                        dt_hwaju.Rows[i]["HWAJU"].ToString(),
                                                                        this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                        dt_hwaju.Rows[i]["HWAJU"].ToString(),
                                                                        this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                        dt_hwaju.Rows[i]["HWAJU"].ToString()
                                                                        );
                        }

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            SectionReport rpt = new TYUTME029R2(this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                               System.DateTime.Now.ToString("yyyy-MM-dd"));

                            sFileName = dt.Rows[0]["VNSANGHO"].ToString() + "-" + dt_hwaju.Rows[i]["HWAJU"].ToString() + "-매출청구서.pdf";

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
