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
    /// 보관료 거래명세서 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.06.21 16:06
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_MR_2BF4Z352 : 처리 할 데이터가 없습니다.
    ///  TY_M_MR_2BF50353 : 처리하시겠습니까?
    ///  TY_M_MR_2BF50354 : 처리하였습니다.
    ///  TY_M_UT_71BDP399 : 처리 중 오류가 발생하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  CHHWAJU : 화주
    ///  CHHWAMUL : 화물
    ///  UTDATE : 매출일자
    /// </summary>
    public partial class TYUTME022P : TYBase
    {
        private string fsFileDownPath = string.Empty;

        #region Description : 폼 로드
        public TYUTME022P()
        {
            InitializeComponent();
        }

        private void TYUTME022P_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_UTDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_UTDATE);
        }
        #endregion

        #region Description : 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            try
            {
                string sRET_MSG = string.Empty;

                this.DbConnector.CommandClear();

                // 20190523 수정전
                //this.DbConnector.Attach("TY_P_UT_76J9J854", this.DTP01_UTDATE.GetString(),
                //                                            this.CBH01_CHHWAJU.GetValue().ToString(),
                //                                            this.CBH01_CHHWAMUL.GetValue().ToString(),
                //                                            sRET_MSG
                //                                            );

                // 20190523 수정후(LPG 포함)
                this.DbConnector.Attach("TY_P_UT_95NIH616", this.DTP01_UTDATE.GetString(),
                                                            this.CBH01_CHHWAJU.GetValue().ToString(),
                                                            this.CBH01_CHHWAMUL.GetValue().ToString(),
                                                            "",
                                                            sRET_MSG
                                                            );

                sRET_MSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                this.ShowMessage("TY_M_MR_2BF50354");
            }
            catch
            {
                this.ShowMessage("TY_M_UT_71BDP399");
            }
        }
        #endregion

        #region Description : 처리 ProcessCheck
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_MR_2BF50353"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            try
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_76LHY930", this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                            this.CBH01_CHHWAJU.GetValue().ToString()
                                                            );

                DataTable dt = this.DbConnector.ExecuteDataTable();


                if (TYUserInfo.EmpNo.ToString() == "0391-F" || TYUserInfo.EmpNo.ToString() == "0185-M" ||
                    TYUserInfo.EmpNo.ToString() == "0280-M")
                {
                    if (dt.Rows.Count > 0)
                    {
                        SectionReport rpt = new TYUTME022R2();

                        rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait;
                        (new TYERGB001P(rpt, dt)).ShowDialog();
                    }
                    else
                    {
                        this.ShowMessage("TY_M_AC_2422N250");
                    }
                }
                else
                {
                    if (dt.Rows.Count > 0)
                    {
                        SectionReport rpt = new TYUTME022R1();

                        rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait;
                        (new TYERGB001P(rpt, dt)).ShowDialog();
                    }
                    else
                    {
                        this.ShowMessage("TY_M_AC_2422N250");
                    }
                }
            }
            catch (Exception ex)
            {

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

                DataTable dt_hwaju = new DataTable();
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_7CCDJ245", this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                            this.CBH01_CHHWAJU.GetValue().ToString()
                                                            );

                dt_hwaju = this.DbConnector.ExecuteDataTable();

                for (int i = 0; i < dt_hwaju.Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_76LHY930", this.DTP01_UTDATE.GetString().Substring(0, 6),
                                                                dt_hwaju.Rows[i]["EDBHWAJU"].ToString()
                                                                );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        SectionReport rpt = new TYUTME022R2();

                        sFileName = dt.Rows[0]["SANGHO"].ToString() + "-" + dt_hwaju.Rows[i]["EDBHWAJU"].ToString() + "-보관료.pdf";

                        UP_Invoice_PdfFileDown(rpt, dt, this.DTP01_UTDATE.GetString().Substring(0, 6), sFileName.ToString());

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
