using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using DataDynamics.ActiveReports;

namespace TY.ER.UT00
{
    /// <summary>
    /// 매출세금계산서 CHECK LIST 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.09.05 18:58
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_795IJ533 : 매출세금계산서 CHECK LIST 출력
    ///  TY_P_UT_795IN534 : 전표파일 체크
    ///  TY_P_UT_795IN535 : 미승인파일(공급가) 조회
    ///  TY_P_UT_795IO536 : 미승인파일(부가세) 확인
    ///  TY_P_UT_795IO537 : 미승인파일 수정세금계산서(공급가) 확인
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  KBSABUN : 사번
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYUTME028P : TYBase
    {
        #region Description : 폼 로드
        public TYUTME028P()
        {
            InitializeComponent();
        }

        private void TYUTME028P_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {   
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_UT_795IJ533", this.DTP01_STDATE.GetString(),
            //                                            this.DTP01_EDDATE.GetString());

            this.DbConnector.Attach("TY_P_UT_796G3552", this.DTP01_STDATE.GetString(),
                                                        this.DTP01_EDDATE.GetString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                ActiveReport rpt = new TYUTME028R();

                rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                (new TYERGB001P(rpt, QueryDataSetReport2(dt))).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Description : 데이터셋 변경
        private DataTable QueryDataSetReport(DataTable dt)
        {
            string sJPNO = string.Empty;
            string sVNSANGHO = string.Empty;
            string sVNSAUPNO = string.Empty;
            string sTXGUBUN = string.Empty;

            string sGUBUN = string.Empty;
            string sSABUN = string.Empty;

            string sDATE = string.Empty;
            string sSTYEAR = string.Empty;
            string sSTMONTH = string.Empty;
            string sSTDAY = string.Empty;
            string sEDYEAR = string.Empty;
            string sEDMONTH = string.Empty;
            string sEDDAY = string.Empty;

            DataTable retDt = new DataTable();
            DataTable dtTemp = new DataTable();

            double dAMOUNT = 0;
            double dVAT = 0;

            string sTXGUBUN1 = string.Empty;

            sSTYEAR = this.DTP01_STDATE.GetString().Substring(0, 4) + "년";
            sSTMONTH = this.DTP01_STDATE.GetString().Substring(4, 2) + "월";
            sSTDAY = this.DTP01_STDATE.GetString().Substring(6, 2) + "일";

            sEDYEAR = this.DTP01_EDDATE.GetString().Substring(0, 4) + "년";
            sEDMONTH = this.DTP01_EDDATE.GetString().Substring(4, 2) + "월";
            sEDDAY = this.DTP01_EDDATE.GetString().Substring(6, 2) + "일";

            sDATE = "( " + sSTYEAR + sSTMONTH + sSTDAY + " ~ " + sEDYEAR + sEDMONTH + sEDDAY + ")";

            retDt.Columns.Add("JPNO", typeof(System.String));
            retDt.Columns.Add("VNSANGHO", typeof(System.String));
            retDt.Columns.Add("VNSAUPNO", typeof(System.String));
            retDt.Columns.Add("TXDESC1", typeof(System.String));
            retDt.Columns.Add("AMOUNT", typeof(System.Double));
            retDt.Columns.Add("VAT", typeof(System.Double));
            retDt.Columns.Add("TOT", typeof(System.Double));
            retDt.Columns.Add("SAUPBU", typeof(System.String));
            retDt.Columns.Add("GUBUN", typeof(System.String));
            retDt.Columns.Add("DATE", typeof(System.String));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sJPNO = "";
                sVNSANGHO = "";
                sVNSAUPNO = "";
                sTXGUBUN = "";

                dAMOUNT = 0;
                dVAT = 0;

                sTXGUBUN1 = "";

                // 확인
                sGUBUN = "";

                sSABUN = "";

                // 전표번호
                sJPNO = dt.Rows[i]["JPNO"].ToString().Substring(0, 17);

                // 전표 존재 체크 TY_P_UT_795IN534
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_795IN534", sJPNO.ToString().Substring(0, 6),
                                                            sJPNO.ToString().Substring(6, 8),
                                                            sJPNO.ToString().Substring(14, 3));

                dtTemp = this.DbConnector.ExecuteDataTable();

                if (dtTemp.Rows.Count <= 0)
                {
                    // 확인
                    sGUBUN = "미승인전표 없음";
                }
                else
                {
                    sSABUN = dtTemp.Rows[0]["B2HISAB"].ToString();

                    if (CBH01_KBSABUN.GetValue().ToString() != "")
                    {
                        if (CBH01_KBSABUN.GetValue().ToString() != sSABUN.ToString())
                        {
                            sGUBUN = "Error";
                        }
                    }
                }

                if (sGUBUN == "")
                {
                    // 전표 공급가 TY_P_UT_795IN535
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_795IN535", sJPNO.ToString().Substring(0, 6),
                                                                sJPNO.ToString().Substring(6, 8),
                                                                sJPNO.ToString().Substring(14, 3));

                    dtTemp = this.DbConnector.ExecuteDataTable();

                    if (dtTemp.Rows.Count > 0)
                    {
                        // 공급가
                        dAMOUNT = double.Parse(dtTemp.Rows[0]["B2AMCR"].ToString());
                    }

                    // 수정세금계산서 TY_P_UT_795IO537
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_795IO537", sJPNO.ToString().Substring(0, 17));

                    dtTemp = this.DbConnector.ExecuteDataTable();

                    if (dtTemp.Rows.Count > 0)
                    {
                        sTXGUBUN1 = "(수정)";
                    }

                    // 전표 부가세 TY_P_UT_795IO536
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_795IO536", sJPNO.ToString().Substring(0, 6),
                                                                sJPNO.ToString().Substring(6, 8),
                                                                sJPNO.ToString().Substring(14, 3));

                    dtTemp = this.DbConnector.ExecuteDataTable();

                    if (dtTemp.Rows.Count > 0)
                    {
                        // 거래처명
                        sVNSANGHO = dtTemp.Rows[0]["VNSANGHO"].ToString();
                        // 사업자번호
                        sVNSAUPNO = dtTemp.Rows[0]["VNSAUPNO"].ToString();
                        // 과세 종류
                        sTXGUBUN = dtTemp.Rows[0]["TXDESC1"].ToString();
                        // 부가세
                        dVAT = double.Parse(dtTemp.Rows[0]["B2AMCR"].ToString());
                    }
                }

                if (sGUBUN != "Error")
                {
                    DataRow row = retDt.NewRow();

                    row["JPNO"] = sJPNO.ToString();
                    row["VNSANGHO"] = sVNSANGHO.ToString();
                    row["VNSAUPNO"] = sVNSAUPNO.ToString().Substring(0, 3) + "-" + sVNSAUPNO.Substring(3, 2) + "-" + sVNSAUPNO.Substring(5, 5);
                    row["TXDESC1"] = sTXGUBUN.ToString() + sTXGUBUN1.ToString();
                    row["AMOUNT"] = dAMOUNT;
                    row["VAT"] = dVAT;
                    row["TOT"] = dAMOUNT + dVAT;
                    if (this.CBH01_KBSABUN.GetValue().ToString() != "")
                    {
                        row["SAUPBU"] = "UTT사업본부" + " - " + sJPNO.Substring(0, 6).ToString();
                    }
                    else
                    {
                        row["SAUPBU"] = "UTT사업본부";
                    }
                    row["GUBUN"] = sGUBUN.ToString();
                    row["DATE"] = sDATE.ToString();

                    retDt.Rows.Add(row);
                }
            }
            
            return retDt;
        }
        #endregion		

        #region Description : 데이터셋 변경
        private DataTable QueryDataSetReport2(DataTable dt)
        {
            string sJPNO = string.Empty;
            string sVNSANGHO = string.Empty;
            string sVNSAUPNO = string.Empty;
            string sTXGUBUN = string.Empty;

            string sGUBUN = string.Empty;
            string sSABUN = string.Empty;

            string sDATE = string.Empty;
            string sSTYEAR = string.Empty;
            string sSTMONTH = string.Empty;
            string sSTDAY = string.Empty;
            string sEDYEAR = string.Empty;
            string sEDMONTH = string.Empty;
            string sEDDAY = string.Empty;

            DataTable retDt = new DataTable();
            DataTable dtTemp = new DataTable();

            double dAMOUNT = 0;
            double dVAT = 0;

            string sTXGUBUN1 = string.Empty;

            sSTYEAR = this.DTP01_STDATE.GetString().Substring(0, 4) + "년";
            sSTMONTH = this.DTP01_STDATE.GetString().Substring(4, 2) + "월";
            sSTDAY = this.DTP01_STDATE.GetString().Substring(6, 2) + "일";

            sEDYEAR = this.DTP01_EDDATE.GetString().Substring(0, 4) + "년";
            sEDMONTH = this.DTP01_EDDATE.GetString().Substring(4, 2) + "월";
            sEDDAY = this.DTP01_EDDATE.GetString().Substring(6, 2) + "일";

            sDATE = "( " + sSTYEAR + sSTMONTH + sSTDAY + " ~ " + sEDYEAR + sEDMONTH + sEDDAY + ")";

            retDt.Columns.Add("JPNO", typeof(System.String));
            retDt.Columns.Add("VNSANGHO", typeof(System.String));
            retDt.Columns.Add("VNSAUPNO", typeof(System.String));
            retDt.Columns.Add("TXDESC1", typeof(System.String));
            retDt.Columns.Add("AMOUNT", typeof(System.Double));
            retDt.Columns.Add("VAT", typeof(System.Double));
            retDt.Columns.Add("TOT", typeof(System.Double));
            retDt.Columns.Add("SAUPBU", typeof(System.String));
            retDt.Columns.Add("GUBUN", typeof(System.String));
            retDt.Columns.Add("DATE", typeof(System.String));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sJPNO = "";
                sVNSANGHO = "";
                sVNSAUPNO = "";
                sTXGUBUN = "";

                dAMOUNT = 0;
                dVAT = 0;

                sTXGUBUN1 = "";

                // 확인
                sGUBUN = "";

                sSABUN = "";

                // 전표번호
                sJPNO = dt.Rows[i]["JPNO"].ToString().Substring(0, 17);

                // 전표 존재 체크 TY_P_UT_795IN534
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_795IN534", sJPNO.ToString().Substring(0, 6),
                                                            sJPNO.ToString().Substring(6, 8),
                                                            sJPNO.ToString().Substring(14, 3));

                dtTemp = this.DbConnector.ExecuteDataTable();

                if (dtTemp.Rows.Count <= 0)
                {
                    // 확인
                    sGUBUN = "미승인전표 없음";
                }
                else
                {
                    sSABUN = dtTemp.Rows[0]["B2HISAB"].ToString();

                    if (CBH01_KBSABUN.GetValue().ToString() != "")
                    {
                        if (CBH01_KBSABUN.GetValue().ToString() != sSABUN.ToString())
                        {
                            sGUBUN = "Error";
                        }
                    }
                }

                if (sGUBUN != "Error")
                {
                    DataRow row = retDt.NewRow();

                    row["JPNO"] = sJPNO.ToString().Substring(0, 17);
                    row["VNSANGHO"] = dt.Rows[i]["VNSANGHO"].ToString();
                    row["VNSAUPNO"] = dt.Rows[i]["VNSAUPNO"].ToString();
                    row["TXDESC1"] = dt.Rows[i]["TXDESC1"].ToString() + dt.Rows[i]["TXGUBUN"].ToString();
                    row["AMOUNT"] = dt.Rows[i]["AMOUNT"].ToString();
                    row["VAT"] = dt.Rows[i]["VAT"].ToString();
                    row["TOT"] = dt.Rows[i]["TOT"].ToString();
                    if (this.CBH01_KBSABUN.GetValue().ToString() != "")
                    {
                        row["SAUPBU"] = "UTT사업본부" + " - " + sJPNO.Substring(0, 6).ToString();
                    }
                    else
                    {
                        row["SAUPBU"] = "UTT사업본부"; 
                    }
                    row["GUBUN"] = sGUBUN.ToString();
                    row["DATE"] = sDATE.ToString();

                    retDt.Rows.Add(row);
                }
            }

            return retDt;
        }
        #endregion		

    }
}
