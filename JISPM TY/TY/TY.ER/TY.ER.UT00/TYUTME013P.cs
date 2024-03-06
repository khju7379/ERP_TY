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
    /// 접안료 거래명세서 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.06.20 09:25
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_76K9J886 : 접안료 거래명세서 출력
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
    ///  IPHANG : 접안일자
    ///  MAECHIL : 매출일자
    ///  TOSEQ : 순번
    /// </summary>
    public partial class TYUTME013P : TYBase
    {
        private string fsFileDownPath = string.Empty;

        #region Description : 폼 로드
        public TYUTME013P()
        {
            InitializeComponent();
        }

        private void TYUTME013P_Load(object sender, System.EventArgs e)
        {
            this.DTP01_MAECHIL.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_IPHANG.SetValue("");

            SetStartingFocus(this.DTP01_MAECHIL);
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();

                string sIPHANG = string.Empty;

                if (this.DTP01_IPHANG.GetValue().ToString() != "" && this.DTP01_IPHANG.GetValue().ToString() != "19000101")
                {
                    sIPHANG = this.DTP01_IPHANG.GetString();
                }

                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_81JBS493", this.DTP01_MAECHIL.GetString(),
                                                            this.DTP01_MAECHIL.GetString(),
                                                            this.CBH01_CHHWAJU.GetValue().ToString(),
                                                            this.TXT01_TOSEQ.GetValue().ToString(),
                                                            sIPHANG
                                                            );

                dt = this.DbConnector.ExecuteDataTable();

                //// 접안료 현재요율이 적용되는 SQL문
                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach
                //    (
                //    "TY_P_UT_73KCS987",
                //    Get_Date(sIPHANG)
                //    );

                //DataTable dtDG = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    SectionReport rpt = new TYUTME013R();
                    // 가로 출력
                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait;
                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
                else
                {
                    this.ShowMessage("TY_M_AC_2422N250");
                }

            }
            catch (Exception ex)
            {
                string str = ex.Message;
                string aa = str;
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

        #region Description : 다운로드
        private void BTN61_DOWN_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFolder.Text.Trim() == "")
                {
                    this.ShowMessage("TY_M_UT_81PBD533");
                }
                else
                {
                    string sGUBUN = string.Empty;
                    string sFileName = string.Empty;

                    string sIPHANG = string.Empty;

                    if (this.DTP01_IPHANG.GetValue().ToString() != "" && this.DTP01_IPHANG.GetValue().ToString() != "19000101")
                    {
                        sIPHANG = this.DTP01_IPHANG.GetString();
                    }

                    DataTable dt = new DataTable();
                    DataTable dt1 = new DataTable();

                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_UT_81GFO467", this.DTP01_MAECHIL.GetString(),
                                                                this.CBH01_CHHWAJU.GetValue().ToString(),
                                                                this.TXT01_TOSEQ.GetValue().ToString(),
                                                                sIPHANG
                                                                );

                    dt1 = this.DbConnector.ExecuteDataTable();

                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_81JBS493", dt1.Rows[i]["JBDATE"].ToString(),
                                                                    dt1.Rows[i]["JBDATE"].ToString(),
                                                                    dt1.Rows[i]["JBBRANCH"].ToString(),
                                                                    dt1.Rows[i]["JBSEQ"].ToString(),
                                                                    dt1.Rows[i]["JBIPHANG"].ToString()
                                                                    );

                        dt = this.DbConnector.ExecuteDataTable();

                        //// 접안료 현재요율이 적용되는 SQL문
                        //this.DbConnector.CommandClear();
                        //this.DbConnector.Attach
                        //    (
                        //    "TY_P_UT_73KCS987",
                        //    Get_Date(sIPHANG)
                        //    );

                        //DataTable dtDG = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            SectionReport rpt = new TYUTME013R();

                            sFileName = dt.Rows[0]["BONSUN"].ToString() + "-" + dt1.Rows[i]["JBSEQ"].ToString() + "-" + dt1.Rows[i]["JBIPHANG"].ToString() + ".pdf";

                            UP_Invoice_PdfFileDown(rpt, dt, this.DTP01_MAECHIL.GetString().Substring(0, 6), sFileName.ToString());

                            sGUBUN = "EXISTS";
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
            catch (Exception ex)
            {
                string str = ex.Message;
                string aa = str;
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
