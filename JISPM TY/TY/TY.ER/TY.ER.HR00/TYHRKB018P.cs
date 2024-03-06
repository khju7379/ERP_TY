using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 퇴충금 명세서 상세내역 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2018.04.25 17:49
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_84PI1892 : 퇴충금명세서 DETAIL 급여상세내역 조회(개인별)
    ///  TY_P_HR_84PI2893 : 퇴충금명세서 DETAIL 상여상세내역 조회(개인별)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_84PI3895 : 퇴충금명세서 DETAIL 상여상세내역 조회(개인별)
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  TLAVG_M : 평균급여
    ///  TLAVG_S : 평균상여
    ///  TLCOMDATE : 계산기분일자
    ///  TLSABUN : 사번
    ///  TLYEAR : 년도
    /// </summary>
    public partial class TYHRKB018P : TYBase
    {

        private string fsTLYEAR;
        private string fsTLSEQ;
        private string fsKBSABUN;


        #region  Description : 폼 로드 이벤트
        public TYHRKB018P(string sTLYEAR, string sTLSEQ, string sKBSABUN)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsTLYEAR = sTLYEAR;
            fsTLSEQ = sTLSEQ;
            fsKBSABUN = sKBSABUN;
        }

        private void TYHRKB018P_Load(object sender, System.EventArgs e)
        {
            CBH01_TLYEAR.SetValue(fsTLYEAR+"-"+Set_Fill3(fsTLSEQ));
            CBH01_TLSABUN.SetValue(fsKBSABUN);

            UP_DataBinding();
        }
        #endregion

        #region  Description : UP_DataBinding 이벤트
        private void UP_DataBinding()
        {
            //퇴충금 마스타 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_84QBI897", CBH01_TLYEAR.GetValue().ToString().Replace("-", ""), CBH01_TLSABUN.GetValue().ToString());
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                TXT01_TLAVG_M.SetValue(dt.Rows[0]["TLAVG_M"].ToString());
                TXT01_TLAVG_S.SetValue(dt.Rows[0]["TLAVG_S"].ToString());
            }

            FPS91_TY_S_HR_84PI3895.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_84PI1892", CBH01_TLYEAR.GetValue().ToString().Replace("-",""), CBH01_TLSABUN.GetValue().ToString());
            FPS91_TY_S_HR_84PI3895.SetValue(UP_Set_MDataTableAddSumRow(this.DbConnector.ExecuteDataTable()));
            if (FPS91_TY_S_HR_84PI3895.CurrentRowCount > 0)
            {
                this.SetSpreadSumRow(this.FPS91_TY_S_HR_84PI3895, "TLSPYYYMM", "[소 계]", SumRowType.SubTotal);
                this.SetSpreadSumRow(this.FPS91_TY_S_HR_84PI3895, "TLSPYYYMM", "[합 계]", SumRowType.Total);
            }
            
            FPS91_TY_S_HR_84QBN898.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_84PI2893", CBH01_TLYEAR.GetValue().ToString().Replace("-", ""), CBH01_TLSABUN.GetValue().ToString());
            FPS91_TY_S_HR_84QBN898.SetValue(UP_Set_SDataTableAddSumRow(this.DbConnector.ExecuteDataTable()));

            if (FPS91_TY_S_HR_84QBN898.CurrentRowCount > 0)
            {
                this.SetSpreadSumRow(this.FPS91_TY_S_HR_84QBN898, "TLSPYYYMM", "[소 계]", SumRowType.SubTotal);
                this.SetSpreadSumRow(this.FPS91_TY_S_HR_84QBN898, "TLSPYYYMM", "[합 계]", SumRowType.Total);
            }

            //임원배수 퇴충금산출 내역서
            FPS91_TY_S_HR_BC1G4858.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_BC1G3857", CBH01_TLYEAR.GetValue().ToString().Replace("-", ""), CBH01_TLSABUN.GetValue().ToString());
            FPS91_TY_S_HR_BC1G4858.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_HR_BC1G4858.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_HR_BC1G4858, "TXWKDAYNAME", "합   계", SumRowType.Sum, "TXREAMOUNT");                
            }

        }
        #endregion

        #region  Description : 급여 소계 라인 추가 함수
        private DataTable UP_Set_MDataTableAddSumRow(DataTable dt)
        {
            int i = 0;

            string sFilter = string.Empty;

            double dTotalAmt = 0;

            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;

            for (i = 1; i < nNum; i++)
            {
                if (table.Rows[i - 1]["TLSPYYYMM"].ToString() != table.Rows[i]["TLSPYYYMM"].ToString() 
                   )
                {
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    // 합 계 이름 넣기
                    table.Rows[i]["TLSPYYYMM"] = "[소 계]";

                    //  년월, 거래처
                    sFilter = "  TLSPYYYMM  = '" + table.Rows[i - 1]["TLSPYYYMM"].ToString() + "'";

                    //총액
                    table.Rows[i]["TLSTOTALAMT"] = table.Compute("SUM(TLSTOTALAMT)", sFilter).ToString();

                    dTotalAmt = dTotalAmt + Convert.ToDouble(table.Rows[i]["TLSTOTALAMT"].ToString());

                    nNum = nNum + 1;

                    i = i + 1;
                }
            }

            if (nNum > 0)
            {
                /******* 마지막 거래처의 대한 로우 생성*****/
                row = table.NewRow();
                table.Rows.InsertAt(row, i);

                table.Rows[i]["TLSPYYYMM"] = "[소 계]";

                //  년월, 거래처
                sFilter = "  TLSPYYYMM  = '" + table.Rows[i - 1]["TLSPYYYMM"].ToString() + "'";

                // 총액
                table.Rows[i]["TLSTOTALAMT"] = table.Compute("SUM(TLSTOTALAMT)", sFilter).ToString();

                dTotalAmt = dTotalAmt + Convert.ToDouble(table.Rows[i]["TLSTOTALAMT"].ToString());

                /******** 총계를 위한 Row 생성 **************/
                row = table.NewRow();
                table.Rows.InsertAt(row, i + 1);

                // 합 계 이름 넣기
                table.Rows[i + 1]["TLSPYYYMM"] = "[합 계]";
                table.Rows[i + 1]["TLSTOTALAMT"] = string.Format("{0:#,##0}", dTotalAmt);
            }

            return table;
            
        }
        #endregion

        #region  Description : 상여 소계 라인 추가 함수
        private DataTable UP_Set_SDataTableAddSumRow(DataTable dt)
        {
            int i = 0;

            string sFilter = string.Empty;

            double dTotalAmt = 0;

            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;

            //연봉직
            if (table.Rows[0]["KBJKCD"].ToString().Trim() == "01" ||
                table.Rows[0]["KBJKCD"].ToString().Trim() == "1A" ||
                table.Rows[0]["KBJKCD"].ToString().Trim() == "1B" ||
                table.Rows[0]["KBJKCD"].ToString().Trim() == "2A" ||
                table.Rows[0]["KBJKCD"].ToString().Trim() == "2B")
            {

                for (i = 1; i < nNum; i++)
                {
                    if (table.Rows[i - 1]["STRTLSPYGUBN"].ToString() != table.Rows[i]["STRTLSPYGUBN"].ToString()
                       )
                    {
                        row = table.NewRow();
                        table.Rows.InsertAt(row, i);

                        // 합 계 이름 넣기
                        table.Rows[i]["TLSPYYYMM"] = "[소 계]";

                        //  년월, 거래처
                        sFilter = "  STRTLSPYGUBN = '" + table.Rows[i - 1]["STRTLSPYGUBN"].ToString() + "'";
                        //총액
                        table.Rows[i]["TLSTOTALAMT"] = table.Compute("SUM(TLSTOTALAMT)", sFilter).ToString();

                        dTotalAmt = dTotalAmt + Convert.ToDouble(table.Rows[i]["TLSTOTALAMT"].ToString());

                        nNum = nNum + 1;

                        i = i + 1;
                    }
                }

                if (nNum > 0)
                {
                    /******* 마지막 거래처의 대한 로우 생성*****/
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    table.Rows[i]["TLSPYYYMM"] = "[소 계]";

                    //  년월, 거래처
                    sFilter = "  STRTLSPYGUBN = '" + table.Rows[i - 1]["STRTLSPYGUBN"].ToString() + "'";

                    // 총액
                    table.Rows[i]["TLSTOTALAMT"] = table.Compute("SUM(TLSTOTALAMT)", sFilter).ToString();

                    dTotalAmt = dTotalAmt + Convert.ToDouble(table.Rows[i]["TLSTOTALAMT"].ToString());

                    /******** 총계를 위한 Row 생성 **************/
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i + 1);

                    // 합 계 이름 넣기
                    table.Rows[i + 1]["TLSPYYYMM"] = "[합 계]";
                    table.Rows[i + 1]["TLSTOTALAMT"] = string.Format("{0:#,##0}", dTotalAmt);
                }
            }
            else
            {
                for (i = 1; i < nNum; i++)
                {
                    if (table.Rows[i - 1]["TLSPYGUBN"].ToString() != table.Rows[i]["TLSPYGUBN"].ToString() ||
                        table.Rows[i - 1]["TLSPYYYMM"].ToString() != table.Rows[i]["TLSPYYYMM"].ToString()
                       )
                    {
                        row = table.NewRow();
                        table.Rows.InsertAt(row, i);

                        // 합 계 이름 넣기
                        table.Rows[i]["TLSPYYYMM"] = "[소 계]";

                        //  년월, 거래처
                        sFilter = "  TLSPYGUBN = '" + table.Rows[i - 1]["TLSPYGUBN"].ToString() + "'";
                        sFilter = sFilter + " AND TLSPYYYMM = '" + table.Rows[i - 1]["TLSPYYYMM"].ToString() + "'";

                        //총액
                        table.Rows[i]["TLSTOTALAMT"] = table.Compute("SUM(TLSTOTALAMT)", sFilter).ToString();

                        dTotalAmt = dTotalAmt + Convert.ToDouble(table.Rows[i]["TLSTOTALAMT"].ToString());

                        nNum = nNum + 1;

                        i = i + 1;
                    }
                }

                if (nNum > 0)
                {
                    /******* 마지막 거래처의 대한 로우 생성*****/
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    table.Rows[i]["TLSPYYYMM"] = "[소 계]";

                    //  년월, 거래처
                    sFilter = "  TLSPYGUBN = '" + table.Rows[i - 1]["TLSPYGUBN"].ToString() + "'";
                    sFilter = sFilter + " AND TLSPYYYMM = '" + table.Rows[i - 1]["TLSPYYYMM"].ToString() + "'";

                    // 총액
                    table.Rows[i]["TLSTOTALAMT"] = table.Compute("SUM(TLSTOTALAMT)", sFilter).ToString();

                    dTotalAmt = dTotalAmt + Convert.ToDouble(table.Rows[i]["TLSTOTALAMT"].ToString());

                    /******** 총계를 위한 Row 생성 **************/
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i + 1);

                    // 합 계 이름 넣기
                    table.Rows[i + 1]["TLSPYYYMM"] = "[합 계]";
                    table.Rows[i + 1]["TLSTOTALAMT"] = string.Format("{0:#,##0}", dTotalAmt);
                }
            }

            return table;

        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
