using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.UT00
{
    /// <summary>
    /// DRUM 재고현황 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.05.12 13:26
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_75CDJ456 : DRUM 재고현황 임시파일 삭제
    ///  TY_P_UT_75CDK457 : DRUM 재고현황 임시파일 생성
    ///  TY_P_UT_75CFG469 : DRUM 재고현황 포장출고 조회
    ///  TY_P_UT_75CFH470 : DRUM 재고현황 임시파일 확인
    ///  TY_P_UT_75CFI471 : DRUM 재고현황 임시파일 업데이트
    ///  TY_P_UT_75CFJ472 : DRUM 재고현황 포장출고 등록
    ///  TY_P_UT_75CFK473 : DRUM 재고현황 출력
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_MR_2BF50353 : 처리하시겠습니까?
    ///  TY_M_MR_2BF50354 : 처리하였습니다.
    ///  TY_M_UT_71BDP399 : 처리 중 오류가 발생하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  CHHWAJU : 화주
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYUTPR017P : TYBase
    {
        #region Descriptoni : 폼 로드
        public TYUTPR017P()
        {
            InitializeComponent();
        }

        private void TYUTPR017P_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Descriptoni : 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            UP_Create_Temp();
        }
        #endregion

        #region Descriptoni : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Descriptoni : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75CFK473");

            dt = QueryDataSetReport(this.DbConnector.ExecuteDataTable());

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYUTPR017R();

                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Default;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Description : 임시파일 생성
        private void UP_Create_Temp()
        {
            try
            {
                DataTable dt = new DataTable();

                // TEMP 삭제
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_75CDJ456");
                this.DbConnector.Attach("TY_P_UT_75CDK457", this.DTP01_STDATE.GetString(),
                                                            this.DTP01_STDATE.GetString(),
                                                            this.DTP01_STDATE.GetString(),
                                                            this.DTP01_STDATE.GetString(),
                                                            this.DTP01_STDATE.GetString(),
                                                            this.DTP01_EDDATE.GetString());
                this.DbConnector.ExecuteNonQueryList();


                DataSet ds = new DataSet();
                DataSet dr = new DataSet();

                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_75CFG469", this.DTP01_STDATE.GetString(),
                                                            this.DTP01_EDDATE.GetString(),
                                                            this.CBH01_CHHWAJU.GetValue().ToString(),
                                                            this.CBH01_DPHWAMUL.GetValue().ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_75CFH470", dt.Rows[i]["TJACTHWAJU"].ToString(),
                                                                    dt.Rows[i]["TJHWAMUL"].ToString(),
                                                                    dt.Rows[i]["TJIPHANG"].ToString(),
                                                                    dt.Rows[i]["TJBONSUN"].ToString(),
                                                                    dt.Rows[i]["TJHWAJU"].ToString(),
                                                                    dt.Rows[i]["TJBLNO"].ToString(),
                                                                    dt.Rows[i]["TJMSNSEQ"].ToString(),
                                                                    dt.Rows[i]["TJHSNSEQ"].ToString(),
                                                                    dt.Rows[i]["TJJUNG"].ToString());

                        dt2 = this.DbConnector.ExecuteDataTable();

                        this.DbConnector.CommandClear();

                        if (dt2.Rows.Count > 0)
                        {
                            this.DbConnector.Attach("TY_P_UT_75CFI471", dt.Rows[i]["TJOVCHQTY"].ToString(),
                                                                        dt.Rows[i]["TJOVCHMT"].ToString(),
                                                                        dt.Rows[i]["TJCHQTY"].ToString(),
                                                                        dt.Rows[i]["TJCHMT"].ToString(),
                                                                        dt.Rows[i]["TJACTHWAJU"].ToString(),
                                                                        dt.Rows[i]["TJHWAMUL"].ToString(),
                                                                        dt.Rows[i]["TJIPHANG"].ToString(),
                                                                        dt.Rows[i]["TJBONSUN"].ToString(),
                                                                        dt.Rows[i]["TJHWAJU"].ToString(),
                                                                        dt.Rows[i]["TJBLNO"].ToString(),
                                                                        dt.Rows[i]["TJMSNSEQ"].ToString(),
                                                                        dt.Rows[i]["TJHSNSEQ"].ToString(),
                                                                        dt.Rows[i]["TJJUNG"].ToString()
                                                                        );
                        }
                        else
                        {
                            this.DbConnector.Attach("TY_P_UT_75CFJ472", dt.Rows[i]["TJACTHWAJU"].ToString(),
                                                                        dt.Rows[i]["TJHWAMUL"].ToString(),
                                                                        dt.Rows[i]["TJIPHANG"].ToString(),
                                                                        dt.Rows[i]["TJBONSUN"].ToString(),
                                                                        dt.Rows[i]["TJHWAJU"].ToString(),
                                                                        dt.Rows[i]["TJBLNO"].ToString(),
                                                                        dt.Rows[i]["TJMSNSEQ"].ToString(),
                                                                        dt.Rows[i]["TJHSNSEQ"].ToString(),
                                                                        dt.Rows[i]["TJJUNG"].ToString(),
                                                                        dt.Rows[i]["TJOVCHQTY"].ToString(),
                                                                        dt.Rows[i]["TJOVCHMT"].ToString(),
                                                                        dt.Rows[i]["TJCHQTY"].ToString(),
                                                                        dt.Rows[i]["TJCHMT"].ToString());
                        }
                        this.DbConnector.ExecuteNonQuery();
                    }
                }
                this.ShowCustomMessage("DRUM 재고 현황이 생성 되었습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            catch
            {
                this.ShowMessage("TY_M_UT_71BDP399");
            }
        }
        #endregion

        #region Description : 데이터 셋 변경
        private DataTable QueryDataSetReport(DataTable dt)
        {
            string sHWAJU = string.Empty;
            string sHWAJUNM = string.Empty;
            string sHWAMUL = string.Empty;
            string sHWAMULNM = string.Empty;
            string sIWOL = string.Empty;
            string sIWOLMT = string.Empty;
            string sPOQTY = string.Empty;
            string sMTQTY = string.Empty;
            string sCHQTY = string.Empty;
            string sCHMTQTY = string.Empty;
            string sJEQTY = string.Empty;
            string sJEMTQTY = string.Empty;

            double dWPOQTY = 0;
            string sWPOQTY = string.Empty;
            double dWMTQTY = 0;
            double dWCHQTY = 0;
            double dWCHMTQTY = 0;
            double dWJEQTY = 0;
            double dWJEMT = 0;
            double dJEQTY = 0;
            double dJEMT = 0;

            string sGHWAJU = string.Empty;
            string sGHWAJUNM = string.Empty;
            string sGHWAMUL = string.Empty;
            string sGHWAMULNM = string.Empty;

            DataTable retDt = new DataTable();

            retDt.Columns.Add("HWAJU", typeof(System.String));
            retDt.Columns.Add("HWAJUNM", typeof(System.String));
            retDt.Columns.Add("HWAMUL", typeof(System.String));
            retDt.Columns.Add("HWAMULNM", typeof(System.String));
            retDt.Columns.Add("POTQTY", typeof(System.String));
            retDt.Columns.Add("MTQTY", typeof(System.String));
            retDt.Columns.Add("CHQTY", typeof(System.String));
            retDt.Columns.Add("CHMTQTY", typeof(System.String));
            retDt.Columns.Add("JEQTY", typeof(System.String));
            retDt.Columns.Add("JEMQTY", typeof(System.String));
            retDt.Columns.Add("IWOL", typeof(System.String));
            retDt.Columns.Add("IWOLMT", typeof(System.String));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = retDt.NewRow();

                if (i == 0)
                {
                    sGHWAJU = dt.Rows[i]["TJACTHWAJU"].ToString();
                    sGHWAJUNM = dt.Rows[i]["CDDESC1"].ToString();
                    sGHWAMUL = dt.Rows[i]["TJHWAMUL"].ToString();
                    sGHWAMULNM = dt.Rows[i]["CDDESC1"].ToString();
                }

                if (sGHWAJU != dt.Rows[i]["TJACTHWAJU"].ToString())
                {
                    sGHWAJU = dt.Rows[i]["TJACTHWAJU"].ToString();
                    sGHWAJUNM = dt.Rows[i]["CDDESC1"].ToString();

                    #region Description : 화주가 같을때

                    if (sGHWAMUL != dt.Rows[i]["TJHWAMUL"].ToString())
                    {
                        DataTable dt2 = SelectSum(sGHWAJU, sGHWAMUL);

                        row["HWAJU"] = sGHWAJU;
                        row["HWAJUNM"] = sGHWAJUNM;

                        row["HWAMUL"] = sGHWAMUL;
                        row["HWAMULNM"] = sGHWAMULNM;

                        row["POTQTY"] = String.Format("{0,8:N}", dWPOQTY);
                        row["MTQTY"] = String.Format("{0,8:N3}", dWMTQTY);
                        row["CHQTY"] = String.Format("{0,8:N}", dWCHQTY);
                        row["CHMTQTY"] = String.Format("{0,8:N3}", dWCHMTQTY);

                        dJEQTY = (dWJEQTY - (Convert.ToDouble(dt2.Rows[0]["PO"].ToString()) - Convert.ToDouble(dt2.Rows[0]["CH"].ToString())));
                        dJEMT = (dWJEMT - (Convert.ToDouble(dt2.Rows[0]["POTOT"].ToString()) - Convert.ToDouble(dt2.Rows[0]["CHTOT"].ToString())));

                        row["JEQTY"] = String.Format("{0,8:N}", dJEQTY);
                        row["JEMQTY"] = String.Format("{0,8:N3}", dJEMT);

                        row["IWOL"] = String.Format("{0,8:N}", (dJEQTY + dWCHQTY) - (dWPOQTY));
                        row["IWOLMT"] = String.Format("{0,8:N3}", (dJEMT + dWCHMTQTY) - (dWMTQTY));

                        sGHWAMUL = dt.Rows[i]["TJHWAMUL"].ToString();
                        sGHWAMULNM = dt.Rows[i]["CDDESC1"].ToString();

                        dWPOQTY = 0;
                        dWMTQTY = 0;
                        dWCHQTY = 0;
                        dWCHMTQTY = 0;
                        dJEQTY = 0;
                        dWJEQTY = 0;
                        dWJEMT = 0;

                        dWPOQTY = dWPOQTY + (Convert.ToDouble(dt.Rows[i]["TJPOQTY"].ToString()));

                        dWMTQTY = dWMTQTY + (Convert.ToDouble(dt.Rows[i]["TJMTQTY"].ToString()));

                        dWCHQTY = dWCHQTY + (Convert.ToDouble(dt.Rows[i]["TJCHQTY"].ToString()));

                        dWCHMTQTY = dWCHMTQTY + (Convert.ToDouble(dt.Rows[i]["TJCHMT"].ToString()));

                        dWJEQTY = dWJEQTY + (Convert.ToDouble(dt.Rows[i]["DJJEQTY"].ToString()));
                        dWJEMT = dWJEMT + (Convert.ToDouble(dt.Rows[i]["DJJEQTY"].ToString()) * Convert.ToDouble(dt.Rows[i]["DJJUNG"].ToString()));
                        dJEQTY = dWJEQTY - dJEQTY;
                    }
                    else
                    {
                        dWPOQTY = dWPOQTY + (Convert.ToDouble(dt.Rows[i]["TJPOQTY"].ToString()));

                        dWMTQTY = dWMTQTY + (Convert.ToDouble(dt.Rows[i]["TJMTQTY"].ToString()));

                        dWCHQTY = dWCHQTY + (Convert.ToDouble(dt.Rows[i]["TJCHQTY"].ToString()));

                        dWCHMTQTY = dWCHMTQTY + (Convert.ToDouble(dt.Rows[i]["TJCHMT"].ToString()));

                        dWJEQTY = dWJEQTY + (Convert.ToDouble(dt.Rows[i]["DJJEQTY"].ToString()));
                        dWJEMT = dWJEMT + (Convert.ToDouble(dt.Rows[i]["DJJEQTY"].ToString()) * Convert.ToDouble(dt.Rows[i]["DJJUNG"].ToString()));
                        dJEQTY = dWJEQTY - dJEQTY;
                    }

                    #endregion

                    #region Description : 마지막 row

                    if (i == (dt.Rows.Count - 1))
                    {
                        DataTable dt2 = SelectSum(sGHWAJU, sGHWAMUL);

                        row["HWAJU"] = sGHWAJU;
                        row["HWAJUNM"] = sGHWAJUNM;

                        row["HWAMUL"] = sGHWAMUL;
                        row["HWAMULNM"] = sGHWAMULNM;
                        row["POTQTY"] = String.Format("{0,8:N3}", dWPOQTY);
                        row["MTQTY"] = String.Format("{0,8:N3}", dWMTQTY);
                        row["CHQTY"] = String.Format("{0,8:N3}", dWCHQTY);
                        row["CHMTQTY"] = String.Format("{0,8:N3}", dWCHMTQTY);
                        dJEQTY = (dWJEQTY - (Convert.ToDouble(dt2.Rows[0]["PO"].ToString()) - Convert.ToDouble(dt2.Rows[0]["CH"].ToString())));
                        dJEMT = (dWJEMT - (Convert.ToDouble(dt2.Rows[0]["POTOT"].ToString()) - Convert.ToDouble(dt2.Rows[0]["CHTOT"].ToString())));

                        row["JEQTY"] = String.Format("{0,8:N3}", dJEQTY);
                        row["JEMQTY"] = String.Format("{0,8:N3}", dJEMT);

                        row["IWOL"] = String.Format("{0,8:N3}", (dJEQTY + dWCHQTY) - (dWPOQTY));
                        row["IWOLMT"] = String.Format("{0,8:N3}", (dJEMT + dWCHMTQTY) - (dWMTQTY));
                    }

                    #endregion
                }
                else
                {
                    #region Description : 화주가 같을때

                    if (sGHWAMUL != dt.Rows[i]["TJHWAMUL"].ToString())
                    {
                        DataTable dt2 = SelectSum(sGHWAJU, sGHWAMUL);

                        row["HWAJU"] = sGHWAJU;
                        row["HWAJUNM"] = sGHWAJUNM;

                        row["HWAMUL"] = sGHWAMUL;
                        row["HWAMULNM"] = sGHWAMULNM;

                        row["POTQTY"] = String.Format("{0,8:N}", dWPOQTY);
                        row["MTQTY"] = String.Format("{0,8:N3}", dWMTQTY);
                        row["CHQTY"] = String.Format("{0,8:N}", dWCHQTY);
                        row["CHMTQTY"] = String.Format("{0,8:N3}", dWCHMTQTY);
                        dJEQTY = (dWJEQTY - (Convert.ToDouble(dt2.Rows[0]["PO"].ToString()) - Convert.ToDouble(dt2.Rows[0]["CH"].ToString())));
                        dJEMT = (dWJEMT - (Convert.ToDouble(dt2.Rows[0]["POTOT"].ToString()) - Convert.ToDouble(dt2.Rows[0]["CHTOT"].ToString())));

                        row["JEQTY"] = String.Format("{0,8:N}", dJEQTY);
                        row["JEMQTY"] = String.Format("{0,8:N3}", dJEMT);

                        row["IWOL"] = String.Format("{0,8:N}", (dJEQTY + dWCHQTY) - (dWPOQTY));
                        row["IWOLMT"] = String.Format("{0,8:N3}", (dJEMT + dWCHMTQTY) - (dWMTQTY));

                        sGHWAMUL = dt.Rows[i]["TJHWAMUL"].ToString();
                        sGHWAMULNM = dt.Rows[i]["CDDESC1"].ToString();

                        dWPOQTY = 0;
                        dWMTQTY = 0;
                        dWCHQTY = 0;
                        dWCHMTQTY = 0;
                        dJEQTY = 0;
                        dWJEQTY = 0;
                        dWJEMT = 0;

                        dWPOQTY = dWPOQTY + (Convert.ToDouble(dt.Rows[i]["TJPOQTY"].ToString()));

                        dWMTQTY = dWMTQTY + (Convert.ToDouble(dt.Rows[i]["TJMTQTY"].ToString()));

                        dWCHQTY = dWCHQTY + (Convert.ToDouble(dt.Rows[i]["TJCHQTY"].ToString()));

                        dWCHMTQTY = dWCHMTQTY + (Convert.ToDouble(dt.Rows[i]["TJCHMT"].ToString()));

                        dWJEQTY = dWJEQTY + (Convert.ToDouble(dt.Rows[i]["DJJEQTY"].ToString()));
                        dWJEMT = dWJEMT + (Convert.ToDouble(dt.Rows[i]["DJJEQTY"].ToString()) * Convert.ToDouble(dt.Rows[i]["DJJUNG"].ToString()));
                        dJEQTY = dWJEQTY - dJEQTY;
                    }
                    else
                    {
                        dWPOQTY = dWPOQTY + (Convert.ToDouble(dt.Rows[i]["TJPOQTY"].ToString()));

                        dWMTQTY = dWMTQTY + (Convert.ToDouble(dt.Rows[i]["TJMTQTY"].ToString()));

                        dWCHQTY = dWCHQTY + (Convert.ToDouble(dt.Rows[i]["TJCHQTY"].ToString()));

                        dWCHMTQTY = dWCHMTQTY + (Convert.ToDouble(dt.Rows[i]["TJCHMT"].ToString()));

                        dWJEQTY = dWJEQTY + (Convert.ToDouble(dt.Rows[i]["DJJEQTY"].ToString()));
                        dWJEMT = dWJEMT + (Convert.ToDouble(dt.Rows[i]["DJJEQTY"].ToString()) * Convert.ToDouble(dt.Rows[i]["DJJUNG"].ToString()));
                        dJEQTY = dWJEQTY - dJEQTY;
                    }

                    #endregion

                    #region Description : 마지막 row

                    if (i == (dt.Rows.Count - 1))
                    {
                        DataTable dt2 = SelectSum(sGHWAJU, sGHWAMUL);

                        row["HWAJU"] = sGHWAJU;
                        row["HWAJUNM"] = sGHWAJUNM;

                        row["HWAMUL"] = sGHWAMUL;
                        row["HWAMULNM"] = sGHWAMULNM;
                        row["POTQTY"] = String.Format("{0,8:N3}", dWPOQTY);
                        row["MTQTY"] = String.Format("{0,8:N3}", dWMTQTY);
                        row["CHQTY"] = String.Format("{0,8:N3}", dWCHQTY);
                        row["CHMTQTY"] = String.Format("{0,8:N3}", dWCHMTQTY);
                        dJEQTY = (dWJEQTY - (Convert.ToDouble(dt2.Rows[0]["PO"].ToString()) - Convert.ToDouble(dt2.Rows[0]["CH"].ToString())));
                        dJEMT = (dWJEMT - (Convert.ToDouble(dt2.Rows[0]["POTOT"].ToString()) - Convert.ToDouble(dt2.Rows[0]["CHTOT"].ToString())));

                        row["JEQTY"] = String.Format("{0,8:N3}", dJEQTY);
                        row["JEMQTY"] = String.Format("{0,8:N3}", dJEMT);

                        row["IWOL"] = String.Format("{0,8:N3}", (dJEQTY + dWCHQTY) - (dWPOQTY));
                        row["IWOLMT"] = String.Format("{0,8:N3}", (dJEMT + dWCHMTQTY) - (dWMTQTY));
                    }

                    #endregion
                }

                retDt.Rows.Add(row);

            }
            return retDt;
        }
        #endregion

        #region Description : 합계 조회
        private DataTable SelectSum(string sGHWAJU, string sGHWAMUL)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75GAN504", sGHWAJU, sGHWAMUL);

            DataTable dt = this.DbConnector.ExecuteDataTable();

            return dt;
        }
        #endregion
    }
}
