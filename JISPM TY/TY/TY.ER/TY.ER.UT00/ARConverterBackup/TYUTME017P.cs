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
    /// 하역료 거래명세서 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.06.29 21:59
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
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
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
    ///  MAECHIL : 매출일자
    ///  SVIPHANG : 입항일자
    /// </summary>
    public partial class TYUTME017P : TYBase
    {
        private string fsFileDownPath = string.Empty;

        #region Description : 폼 로드
        public TYUTME017P()
        {
            InitializeComponent();
        }

        private void TYUTME017P_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_MAECHIL.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_SVIPHANG.SetValue("");

            SetStartingFocus(this.DTP01_MAECHIL);
        }
        #endregion

        #region Description : 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            try
            {
                UP_Batch("CREATE");
            }
            catch
            {
            }
        }
        #endregion

        #region Description : 하역료 거래명세서 생성
        private void UP_Batch(string sGUBUN)
        {
            try
            {
                string sRET_MSG = string.Empty;
                string sIPHANG = string.Empty;

                if (this.DTP01_SVIPHANG.GetValue().ToString() != "" && this.DTP01_SVIPHANG.GetValue().ToString() != "19000101")
                {
                    sIPHANG = this.DTP01_SVIPHANG.GetString();
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_76LBJ923", this.DTP01_MAECHIL.GetString(),
                                                            this.CBH01_CHHWAJU.GetValue().ToString(),
                                                            this.CBH01_CHHWAMUL.GetValue().ToString(),
                                                            sIPHANG,
                                                            this.TXT01_M1SEQ.GetValue().ToString(),
                                                            "",
                                                            sRET_MSG
                                                            );
                sRET_MSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                if (sGUBUN == "CREATE")
                {
                    this.ShowMessage("TY_M_MR_2BF50354");
                }
            }
            catch
            {
                if (sGUBUN == "CREATE")
                {
                    this.ShowMessage("TY_M_UT_71BDP399");
                }
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            try
            {
                string sIPHANG = string.Empty;

                if (this.DTP01_SVIPHANG.GetValue().ToString() != "")
                {
                    sIPHANG = this.DTP01_SVIPHANG.GetString();
                }

                UP_Batch("PRT");

                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_81PBS534", this.DTP01_MAECHIL.GetString().Substring(0, 6),
                                                            this.CBH01_CHHWAJU.GetValue().ToString(),
                                                            sIPHANG,
                                                            this.TXT01_M1SEQ.GetValue().ToString()
                                                            );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (TYUserInfo.EmpNo.ToString() == "0391-F" || TYUserInfo.EmpNo.ToString() == "0185-M" ||
                        TYUserInfo.EmpNo.ToString() == "0280-M")
                {

                    if (dt.Rows.Count > 0)
                    {
                        ActiveReport rpt = new TYUTME017R2();

                        rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;
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
                        ActiveReport rpt = new TYUTME017R1();

                        rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;
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

                    DataTable dt_hwaju = new DataTable();
                    DataTable dt = new DataTable();


                    UP_Batch("PRT");

                    if (this.DTP01_SVIPHANG.GetValue().ToString() != "")
                    {
                        sIPHANG = this.DTP01_SVIPHANG.GetString();
                    }

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_7CCEN248", this.DTP01_MAECHIL.GetString().Substring(0, 6),
                                                                this.CBH01_CHHWAJU.GetValue().ToString(),
                                                                sIPHANG,
                                                                this.TXT01_M1SEQ.GetValue().ToString()
                                                                );

                    dt_hwaju = this.DbConnector.ExecuteDataTable();

                    if (dt_hwaju.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt_hwaju.Rows.Count; i++)
                        {
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_UT_81PBS534", DTP01_MAECHIL.GetString().Substring(0, 6),
                                                                        dt_hwaju.Rows[i]["EDHHWAJU"].ToString(),
                                                                        sIPHANG,
                                                                        this.TXT01_M1SEQ.GetValue().ToString()
                                                                        );

                            dt = this.DbConnector.ExecuteDataTable();

                            if (dt.Rows.Count > 0)
                            {
                                ActiveReport rpt = new TYUTME017R2();

                                // 원본
                                //sFileName = dt.Rows[0]["SANGHO"].ToString() + "-" + dt_hwaju.Rows[i]["EDHHWAJU"].ToString() + "-" + dt_hwaju.Rows[i]["EDHIPHANG"].ToString() + "-하역료.pdf";

                                // 수정
                                sFileName = dt.Rows[0]["SANGHO"].ToString() + "-" + dt_hwaju.Rows[i]["EDHHWAJU"].ToString() + "-하역료.pdf";

                                UP_Invoice_PdfFileDown(rpt, dt, this.DTP01_MAECHIL.GetString().Substring(0, 6), sFileName.ToString());

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
            catch (Exception ex)
            {

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
    }
}
