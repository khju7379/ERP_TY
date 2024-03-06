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
    /// 부두접안 현황 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.05.11 13:23
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_75BDE450 : 부두접안 현황 출력
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYUTPR014P : TYBase
    {
        #region Description : 폼 로드
        public TYUTPR014P()
        {
            InitializeComponent();
        }

        private void TYUTPR014P_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75BDE450", this.DTP01_STDATE.GetString(),
                                                        this.DTP01_EDDATE.GetString());

            dt = QueryDataSetReport(this.DbConnector.ExecuteDataTable(), this.DTP01_STDATE.GetString(), this.DTP01_EDDATE.GetString());

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYUTPR014R();
                // 가로 출력
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Description : 데이터셋 변경
        private DataTable QueryDataSetReport(DataTable dt, string sSTDATE, string sEDDATE)
        {
            string sSql = string.Empty;
            string sCHECK = "*";
            string sNEWVSIPHANG = string.Empty;
            string sOLDVSIPHANG = string.Empty;
            string sJUBAN = string.Empty;
            string sSDATE = string.Empty;
            string sEDATE = string.Empty;
            string sDATE = string.Empty;
            string sVSIPHANG = string.Empty;
            string sVSBONSUN = string.Empty;

            int iJUBAN1 = 0;
            int iJUBAN2 = 0;
            int iJUBAN3 = 0;
            int iJUBAN4 = 0;
            double dMTQTY = 0;
            double dCMSHQTY1 = 0;
            double dCMSHQTY2 = 0;
            double dCMSHQTY3 = 0;
            double dCMSHQTY4 = 0;

            sSDATE = sSTDATE.Substring(0, 4) + "/" + sSTDATE.Substring(4, 2) + "/" + sSTDATE.Substring(6, 2);
            sEDATE = sEDDATE.Substring(0, 4) + "/" + sEDDATE.Substring(4, 2) + "/" + sEDDATE.Substring(6, 2);

            sDATE = "(" + sSDATE + "~" + sEDATE + ")";

            DataTable retDt = new DataTable();

            retDt.Columns.Add("VSIPHANG", typeof(System.String));
            retDt.Columns.Add("JUBAN1", typeof(System.String));
            retDt.Columns.Add("CMSHQTY1", typeof(System.String));
            retDt.Columns.Add("JUBAN2", typeof(System.String));
            retDt.Columns.Add("CMSHQTY2", typeof(System.String));
            retDt.Columns.Add("JUBAN3", typeof(System.String));
            retDt.Columns.Add("CMSHQTY3", typeof(System.String));
            retDt.Columns.Add("JUBAN4", typeof(System.String));
            retDt.Columns.Add("CMSHQTY4", typeof(System.String));
            retDt.Columns.Add("DATE", typeof(System.String));

            DataTable dt2 = new DataTable();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dMTQTY = 0;
                sNEWVSIPHANG = dt.Rows[i]["VSIPHANG"].ToString().Substring(0, 6).ToString();

                // 처음돌때
                if (sCHECK == "*")
                {
                    sOLDVSIPHANG = sNEWVSIPHANG.ToString();
                    sCHECK = "";
                }
                else
                {
                    // 입항년월이 다를시
                    if (sOLDVSIPHANG != sNEWVSIPHANG)
                    {
                        DataRow row = retDt.NewRow();

                        row["VSIPHANG"] = sOLDVSIPHANG.ToString();
                        row["JUBAN1"] = iJUBAN1;
                        row["CMSHQTY1"] = dCMSHQTY1;
                        row["JUBAN2"] = iJUBAN2;
                        row["CMSHQTY2"] = dCMSHQTY2;
                        row["JUBAN3"] = iJUBAN3;
                        row["CMSHQTY3"] = dCMSHQTY3;
                        row["JUBAN4"] = iJUBAN4;
                        row["CMSHQTY4"] = dCMSHQTY4;
                        row["DATE"] = sDATE.ToString();

                        retDt.Rows.Add(row);

                        sOLDVSIPHANG = sNEWVSIPHANG.ToString();
                        iJUBAN1 = 0;
                        iJUBAN2 = 0;
                        iJUBAN3 = 0;
                        iJUBAN4 = 0;
                        dCMSHQTY1 = 0;
                        dCMSHQTY2 = 0;
                        dCMSHQTY3 = 0;
                        dCMSHQTY4 = 0;
                    }
                }
                // 접안장소
                sJUBAN = dt.Rows[i]["VSJUBAN"].ToString();

                // 입고유종파일 READ
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_75BDM451", dt.Rows[i]["VSIPHANG"].ToString(),
                                                            dt.Rows[i]["VSBONSUN"].ToString());

                dt2 = this.DbConnector.ExecuteDataTable();

                for (int x = 0; x < dt2.Rows.Count; x++)
                {
                    dMTQTY = dMTQTY + double.Parse(Get_Numeric(dt2.Rows[x]["CMSHQTY"].ToString()));
                }

                // 접안 장소
                if (sJUBAN == "1")
                {
                    sVSIPHANG = dt.Rows[i]["VSIPHANG"].ToString();
                    sVSBONSUN = dt.Rows[i]["VSBONSUN"].ToString();

                    iJUBAN1 = iJUBAN1 + 1;
                    dCMSHQTY1 = dCMSHQTY1 + dMTQTY;
                }
                else if (sJUBAN == "2")
                {
                    iJUBAN2 = iJUBAN2 + 1;
                    dCMSHQTY2 = dCMSHQTY2 + dMTQTY;
                }
                else if (sJUBAN == "3")
                {
                    iJUBAN3 = iJUBAN3 + 1;
                    dCMSHQTY3 = dCMSHQTY3 + dMTQTY;
                }
                else if (sJUBAN == "4")
                {
                    iJUBAN4 = iJUBAN4 + 1;
                    dCMSHQTY4 = dCMSHQTY4 + dMTQTY;
                }
            }

            DataRow row1 = retDt.NewRow();

            row1["VSIPHANG"] = sNEWVSIPHANG.ToString();
            row1["JUBAN1"] = iJUBAN1;
            row1["CMSHQTY1"] = dCMSHQTY1;
            row1["JUBAN2"] = iJUBAN2;
            row1["CMSHQTY2"] = dCMSHQTY2;
            row1["JUBAN3"] = iJUBAN3;
            row1["CMSHQTY3"] = dCMSHQTY3;
            row1["JUBAN4"] = iJUBAN4;
            row1["CMSHQTY4"] = dCMSHQTY4;
            row1["DATE"] = sDATE.ToString();

            retDt.Rows.Add(row1);

            return retDt;
        }
        #endregion
    }
}
