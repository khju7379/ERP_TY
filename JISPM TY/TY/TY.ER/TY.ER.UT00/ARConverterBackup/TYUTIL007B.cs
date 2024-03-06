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
    /// 유독물 관리 대장 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2016.07.15 16:38
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_6BAEI713 : 유독물 관리 대장 출력
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_MR_2BF50353 : 처리하시겠습니까?
    ///  TY_M_MR_2BF50354 : 처리하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  CSHWAJU : 화주
    ///  CSHWAMUL : 화물
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYUTIL007B : TYBase
    {
        #region Description : 페이지 로드
        public TYUTIL007B()
        {
            InitializeComponent();
        }

        private void TYUTIL007B_Load(object sender, System.EventArgs e)
        {
            this.DTP01_SDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_SDATE);
        }
        #endregion

        #region Description : 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sSDATE  = string.Empty;
            string sEDATE  = string.Empty;
            string sHWAJU  = string.Empty;
            string sHWAMUL = string.Empty;

            string sOUT_MSG = string.Empty;

            // 유독물 관리대장 삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_71BDZ400");

            this.DbConnector.ExecuteNonQuery();

            // 유독물관리대장 SP - CALL
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_71BAU397",
                                    Get_Date(this.DTP01_SDATE.GetValue().ToString()),
                                    Get_Date(this.DTP01_EDATE.GetValue().ToString()),
                                    this.CBH01_CSHWAJU.GetValue().ToString(),
                                    this.CBH01_CSHWAMUL.GetValue().ToString(),
                                    sOUT_MSG.ToString()
                                    );

            sOUT_MSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUT_MSG.Substring(0, 2).ToString() == "OK")
            {
                this.ShowMessage("TY_M_MR_2BF50354");
            }
            else
            {
                this.ShowMessage("TY_M_UT_71BDP399");
            }
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
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_UT_83QHL767",
                                    this.CBH01_CSHWAMUL.GetValue().ToString(),
                                    this.CBH01_CSHWAJU.GetValue().ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                dt = UP_DatatableChange(dt);

                ActiveReport rpt = new TYUTIL007R1();
                // 가로 출력
                rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Description : 출력 데이터 변경
        private DataTable UP_DatatableChange(DataTable dt)
        {
            string sOLDHAWMUL = string.Empty;
            string sw = string.Empty;
            double wkJEGOQTY = 0;

            DataTable dtRtn = new DataTable();

            dtRtn.Columns.Add("TUHWAMUL", typeof(System.String));
            dtRtn.Columns.Add("PEHWAMULNM", typeof(System.String));
            dtRtn.Columns.Add("PRGUBUN", typeof(System.String));
            dtRtn.Columns.Add("PRDATE1", typeof(System.String));
            dtRtn.Columns.Add("PRJUNMT", typeof(System.Double));
            dtRtn.Columns.Add("TUHWAJU", typeof(System.String));
            dtRtn.Columns.Add("PRIPSAUP", typeof(System.String));
            dtRtn.Columns.Add("PRIPHJNM", typeof(System.String));
            dtRtn.Columns.Add("PRIPMT", typeof(System.Double));
            dtRtn.Columns.Add("TUCHHWAJU", typeof(System.String));
            dtRtn.Columns.Add("PRCHSAUP", typeof(System.String));
            dtRtn.Columns.Add("PRCHHJNM", typeof(System.String));
            dtRtn.Columns.Add("PRCHMT", typeof(System.Double));
            dtRtn.Columns.Add("PRJEMT", typeof(System.Double));
            dtRtn.Columns.Add("PRIPJUSO", typeof(System.String));
            dtRtn.Columns.Add("PRCHJUSO", typeof(System.String));
            dtRtn.Columns.Add("PRCHDATE", typeof(System.String));
            dtRtn.Columns.Add("TYPE1", typeof(System.String));
            dtRtn.Columns.Add("TYPE2", typeof(System.String));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dtRtn.NewRow();

                if (sw == "")
                {
                    sOLDHAWMUL = dt.Rows[i]["PEHWAMULNM"].ToString();
                    sw = "*";
                }

                if (sOLDHAWMUL != dt.Rows[i]["PEHWAMULNM"].ToString())
                {
                    sOLDHAWMUL = dt.Rows[i]["PEHWAMULNM"].ToString();
                    wkJEGOQTY = 0;
                }

                row["PEHWAMULNM"] = dt.Rows[i]["PEHWAMULNM"].ToString();            // 유독물명

                if (dt.Rows[i]["PRGUBUN"].ToString() != "")
                {
                    row["PRGUBUN"] = dt.Rows[i]["PRGUBUN"].ToString() + " %";        // 성분
                }
                else
                {
                    row["PRGUBUN"] = dt.Rows[i]["PRGUBUN"].ToString();              // 성분
                }
                if ("0000/00/00" == dt.Rows[i]["PRDATE1"].ToString())               // 년월일
                {
                    row["PRDATE1"] = "         ";
                }
                else
                {
                    if (double.Parse(dt.Rows[i]["PRIPMT"].ToString()) != 0)
                    {
                        row["PRDATE1"] = dt.Rows[i]["PRDATE1"].ToString();
                    }
                }
                row["PRJUNMT"] = double.Parse(dt.Rows[i]["PRJUNMT"].ToString());  // 이월량
                row["PRIPHJNM"] = dt.Rows[i]["PRIPHJNM"].ToString();               // 입고화주
                row["PRIPSAUP"] = dt.Rows[i]["PRIPSAUP"].ToString();               // 입고사업자번호
                row["PRIPJUSO"] = dt.Rows[i]["PRIPJUSO"].ToString();               // 입고주소
                row["PRIPMT"] = double.Parse(dt.Rows[i]["PRIPMT"].ToString());   // 입고량
                if (double.Parse(dt.Rows[i]["PRCHMT"].ToString()) != 0)
                {
                    row["PRCHDATE"] = dt.Rows[i]["PRDATE1"].ToString();              // 일자
                }
                row["PRCHHJNM"] = dt.Rows[i]["PRCHHJNM"].ToString();               // 출고화주
                row["PRCHSAUP"] = dt.Rows[i]["PRCHSAUP"].ToString();               // 출고사업자번호
                row["PRCHJUSO"] = dt.Rows[i]["PRCHJUSO"].ToString();               // 출고주소				
                row["PRCHMT"] = double.Parse(dt.Rows[i]["PRCHMT"].ToString());   // 출고량

                double wkJUNMT = double.Parse(dt.Rows[i]["PRJUNMT"].ToString());  // 이월량
                double wkPMT = double.Parse(dt.Rows[i]["PRIPMT"].ToString());   // 입고량
                double wkCHMT = double.Parse(dt.Rows[i]["PRCHMT"].ToString());   // 출고량

                if (dt.Rows[i]["TUHWAMUL"].ToString() == "P06" || dt.Rows[i]["TUHWAMUL"].ToString() == "M06" || dt.Rows[i]["TUHWAMUL"].ToString() == "M15"
                    || dt.Rows[i]["TUHWAMUL"].ToString() == "O01" || dt.Rows[i]["TUHWAMUL"].ToString() == "D05" || dt.Rows[i]["TUHWAMUL"].ToString() == "E11"
                    || dt.Rows[i]["TUHWAMUL"].ToString() == "C02")
                {
                    row["TYPE1"] = "유독물질";
                    row["TYPE2"] = "";
                }
                else if (dt.Rows[i]["TUHWAMUL"].ToString() == "A03" || dt.Rows[i]["TUHWAMUL"].ToString() == "B05" || dt.Rows[i]["TUHWAMUL"].ToString() == "M03"
                    || dt.Rows[i]["TUHWAMUL"].ToString() == "M07" || dt.Rows[i]["TUHWAMUL"].ToString() == "P04" || dt.Rows[i]["TUHWAMUL"].ToString() == "T05")
                {
                    row["TYPE1"] = "유독물질";
                    row["TYPE2"] = "사고대비물질";
                }
                else
                {
                    row["TYPE1"] = "";
                    row["TYPE2"] = "";
                }

                wkJEGOQTY = wkJEGOQTY + wkJUNMT + wkPMT - wkCHMT;
                row["PRJEMT"] = wkJEGOQTY;  // 재고량

                dtRtn.Rows.Add(row);
            }

            return dtRtn;
        }
        #endregion
    }
}
