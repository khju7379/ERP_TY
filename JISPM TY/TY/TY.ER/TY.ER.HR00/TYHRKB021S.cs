using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;
using GrapeCity.ActiveReports;
using TY.ER.GB00;


namespace TY.ER.HR00
{
    /// <summary>
    /// 학자금 지원상세내역 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.03.17 17:51
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_73HI1959 : 학자금 지원상세내역 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_73K92964 : 학자금 지원상세내역 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  HKHLGUBN : 학력구분
    ///  HKSCRYEAR : 지원년도
    ///  HKSSABUN : 사번
    /// </summary>
    public partial class TYHRKB021S : TYBase
    {

        private string fsHKSHLGUBN;
        private string fsHKSCRYEAR;
        private string fsHKSCRBUNGI;
        private string fsHKSCRDATE;

        #region  Description : 폼 로드 이벤트
        public TYHRKB021S()
        {
            InitializeComponent();
        }

        private void TYHRKB021S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_PRT.Visible = false;

            this.SetStartingFocus(TXT01_HKSCRYEAR);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_73K9X968.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_73K9V965", TXT01_HKSCRYEAR.GetValue().ToString(), CBH01_HKHLGUBN.GetValue());
            this.FPS91_TY_S_HR_73K9X968.SetValue(this.DbConnector.ExecuteDataTable());

            this.BTN61_PRT.Visible = false;

        }
        #endregion

        #region  Description : FPS91_TY_S_HR_73K9X968_CellDoubleClick 이벤트
        private void FPS91_TY_S_HR_73K9X968_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

            this.FPS91_TY_S_HR_73K92964.Initialize();

            fsHKSHLGUBN = this.FPS91_TY_S_HR_73K9X968.GetValue("HKSHLGUBN").ToString();
            fsHKSCRYEAR = this.FPS91_TY_S_HR_73K9X968.GetValue("HKSCRYEAR").ToString();
            fsHKSCRBUNGI = this.FPS91_TY_S_HR_73K9X968.GetValue("HKSCRBUNGI").ToString();
            fsHKSCRDATE = this.FPS91_TY_S_HR_73K9X968.GetValue("HKSCRDATE").ToString();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_73KAD980", this.FPS91_TY_S_HR_73K9X968.GetValue("HKSHLGUBN").ToString(),
                                                        this.FPS91_TY_S_HR_73K9X968.GetValue("HKSCRYEAR").ToString(),
                                                        this.FPS91_TY_S_HR_73K9X968.GetValue("HKSCRBUNGI").ToString(),
                                                        this.FPS91_TY_S_HR_73K9X968.GetValue("HKSCRDATE").ToString());
            DataTable dt = this.DbConnector.ExecuteDataTable();
            this.FPS91_TY_S_HR_73K92964.SetValue(dt);

            if (this.FPS91_TY_S_HR_73K92964.CurrentRowCount > 0)
            {
                DTP01_HKSPYDATE.SetValue(dt.Rows[0]["HKSPYDATE"].ToString());
                this.SpreadSumRowAdd(this.FPS91_TY_S_HR_73K92964, "HKSSABUNNM", "[합   계]", SumRowType.Sum, "HKSIPHAKAMT", "HKSDUROKAMT", "HKSHAKSANGAMT", "HKSTOTALAMT", "HKSJANGHAKAMT");

                this.BTN61_PRT.Visible = true;
            }


        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN62_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_73KC0986.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_73HI1959", CBH01_HKSSABUN.GetValue().ToString(), TXT01_HKCHDNAME.GetValue(), TXT01_HKSHAKYEAR.GetValue(), CBH02_HKHLGUBN.GetValue().ToString());
            this.FPS91_TY_S_HR_73KC0986.SetValue(UP_InSertRowTodt(this.DbConnector.ExecuteDataTable()));

            if (this.FPS91_TY_S_HR_73KC0986.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_HR_73KC0986.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_HR_73KC0986.GetValue(i, "HKCHDNAME").ToString() == "[ 소 계 ]")
                    {
                        this.FPS91_TY_S_HR_73KC0986.ActiveSheet.Rows[i].BackColor = Color.FromArgb(228, 242, 194);
                    }
                    else if (this.FPS91_TY_S_HR_73KC0986.GetValue(i, "HKSSABUNNM").ToString() == "[ 합  계 ]")
                    {
                        this.FPS91_TY_S_HR_73KC0986.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    }
                }
            }
        }
        #endregion

        #region Description : FPS91_TY_S_HR_73K92964_CellDoubleClick 함수
        private void FPS91_TY_S_HR_73K92964_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.FPS91_TY_S_HR_73K92964.GetValue("HKSHLGUBN").ToString() != "")
            {
                if (this.OpenModalPopup(new TYHRKB021I(this.FPS91_TY_S_HR_73K92964.GetValue("HKSHLGUBN").ToString(),
                                                       this.FPS91_TY_S_HR_73K92964.GetValue("HKSCRYEAR").ToString(),
                                                       this.FPS91_TY_S_HR_73K92964.GetValue("HKSCRBUNGI").ToString(),
                                                       this.FPS91_TY_S_HR_73K92964.GetValue("HKSCRDATE").ToString(),
                                                       this.FPS91_TY_S_HR_73K92964.GetValue("HKSSABUN").ToString(),
                                                       this.FPS91_TY_S_HR_73K92964.GetValue("HKSYEAR").ToString(),
                                                       this.FPS91_TY_S_HR_73K92964.GetValue("HKSSSEQ").ToString()
                                                       )) == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region Description : FPS91_TY_S_HR_73KC0986_CellDoubleClick 함수
        private void FPS91_TY_S_HR_73KC0986_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.FPS91_TY_S_HR_73KC0986.GetValue("HKSHLGUBN").ToString() != "")
            {
                if (this.OpenModalPopup(new TYHRKB021I(this.FPS91_TY_S_HR_73KC0986.GetValue("HKSHLGUBN").ToString(),
                                                      this.FPS91_TY_S_HR_73KC0986.GetValue("HKSCRYEAR").ToString(),
                                                      this.FPS91_TY_S_HR_73KC0986.GetValue("HKSCRBUNGI").ToString(),
                                                      this.FPS91_TY_S_HR_73KC0986.GetValue("HKSCRDATE").ToString(),
                                                      this.FPS91_TY_S_HR_73KC0986.GetValue("HKSSABUN").ToString(),
                                                      this.FPS91_TY_S_HR_73KC0986.GetValue("HKSYEAR").ToString(),
                                                      this.FPS91_TY_S_HR_73KC0986.GetValue("HKSSSEQ").ToString()
                                                      )) == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region Description : 데이터테이블 소계 처리 함수
        private DataTable UP_InSertRowTodt(DataTable dt)
        {
            int i = 0;

            string sFilter = string.Empty;

            double dHKSIPHAKAMT = 0;
            double dHKSDUROKAMT = 0;
            double dHKSHAKSANGAMT = 0;
            double dHKSTOTALAMT = 0;
            double dHKSJANGHAKAMT = 0;

            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;

            for (i = 1; i < nNum; i++)
            {
                if (table.Rows[i - 1]["HKSSABUN"].ToString() != table.Rows[i]["HKSSABUN"].ToString() ||
                      table.Rows[i - 1]["HKSYEAR"].ToString() != table.Rows[i]["HKSYEAR"].ToString() ||
                    table.Rows[i - 1]["HKSSSEQ"].ToString() != table.Rows[i]["HKSSSEQ"].ToString()
                   )
                {
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    // 합 계 이름 넣기
                    table.Rows[i]["HKCHDNAME"] = "[ 소 계 ]";


                    sFilter = "  HKSSABUN  = '" + table.Rows[i - 1]["HKSSABUN"].ToString() + "'";
                    sFilter = sFilter + " AND  HKSYEAR  = '" + table.Rows[i - 1]["HKSYEAR"].ToString() + "'";
                    sFilter = sFilter + " AND  HKSSSEQ  =  " + table.Rows[i - 1]["HKSSSEQ"].ToString() + "";

                    //입학금
                    table.Rows[i]["HKSIPHAKAMT"] = table.Compute("SUM(HKSIPHAKAMT)", sFilter).ToString();
                    //등록금
                    table.Rows[i]["HKSDUROKAMT"] = table.Compute("SUM(HKSDUROKAMT)", sFilter).ToString();
                    //학생회비
                    table.Rows[i]["HKSHAKSANGAMT"] = table.Compute("SUM(HKSHAKSANGAMT)", sFilter).ToString();
                    //지급총액
                    table.Rows[i]["HKSTOTALAMT"] = table.Compute("SUM(HKSTOTALAMT)", sFilter).ToString();
                    //장학금
                    table.Rows[i]["HKSJANGHAKAMT"] = table.Compute("SUM(HKSJANGHAKAMT)", sFilter).ToString();

                    dHKSIPHAKAMT = dHKSIPHAKAMT + Convert.ToDouble(table.Rows[i]["HKSIPHAKAMT"].ToString());
                    dHKSDUROKAMT = dHKSDUROKAMT + Convert.ToDouble(table.Rows[i]["HKSDUROKAMT"].ToString());
                    dHKSHAKSANGAMT = dHKSHAKSANGAMT + Convert.ToDouble(table.Rows[i]["HKSHAKSANGAMT"].ToString());
                    dHKSTOTALAMT = dHKSTOTALAMT + Convert.ToDouble(table.Rows[i]["HKSTOTALAMT"].ToString());
                    dHKSJANGHAKAMT = dHKSJANGHAKAMT + Convert.ToDouble(table.Rows[i]["HKSJANGHAKAMT"].ToString());

                    nNum = nNum + 1;

                    i = i + 1;
                }
            }

            if (nNum > 0)
            {
                /******* 마지막 거래처의 대한 로우 생성*****/
                row = table.NewRow();
                table.Rows.InsertAt(row, i);

                table.Rows[i]["HKCHDNAME"] = "[ 소 계 ]";

                //  년월, 거래처
                sFilter = "  HKSSABUN  = '" + table.Rows[i - 1]["HKSSABUN"].ToString() + "'";
                sFilter = sFilter + " AND  HKSYEAR  = '" + table.Rows[i - 1]["HKSYEAR"].ToString() + "'";
                sFilter = sFilter + " AND  HKSSSEQ  =  " + table.Rows[i - 1]["HKSSSEQ"].ToString() + "";

                //입학금
                table.Rows[i]["HKSIPHAKAMT"] = table.Compute("SUM(HKSIPHAKAMT)", sFilter).ToString();
                //등록금
                table.Rows[i]["HKSDUROKAMT"] = table.Compute("SUM(HKSDUROKAMT)", sFilter).ToString();
                //학생회비
                table.Rows[i]["HKSHAKSANGAMT"] = table.Compute("SUM(HKSHAKSANGAMT)", sFilter).ToString();
                //지급총액
                table.Rows[i]["HKSTOTALAMT"] = table.Compute("SUM(HKSTOTALAMT)", sFilter).ToString();
                //장학금
                table.Rows[i]["HKSJANGHAKAMT"] = table.Compute("SUM(HKSJANGHAKAMT)", sFilter).ToString();

                dHKSIPHAKAMT = dHKSIPHAKAMT + Convert.ToDouble(table.Rows[i]["HKSIPHAKAMT"].ToString());
                dHKSDUROKAMT = dHKSDUROKAMT + Convert.ToDouble(table.Rows[i]["HKSDUROKAMT"].ToString());
                dHKSHAKSANGAMT = dHKSHAKSANGAMT + Convert.ToDouble(table.Rows[i]["HKSHAKSANGAMT"].ToString());
                dHKSTOTALAMT = dHKSTOTALAMT + Convert.ToDouble(table.Rows[i]["HKSTOTALAMT"].ToString());
                dHKSJANGHAKAMT = dHKSJANGHAKAMT + Convert.ToDouble(table.Rows[i]["HKSJANGHAKAMT"].ToString());

                /******** 총계를 위한 Row 생성 **************/
                row = table.NewRow();
                table.Rows.InsertAt(row, i + 1);

                // 합 계 이름 넣기
                table.Rows[i + 1]["HKSSABUNNM"] = "[ 합  계 ]";

                table.Rows[i + 1]["HKSIPHAKAMT"] = string.Format("{0:#,##0}", dHKSIPHAKAMT);
                table.Rows[i + 1]["HKSDUROKAMT"] = string.Format("{0:#,##0}", dHKSDUROKAMT);
                table.Rows[i + 1]["HKSHAKSANGAMT"] = string.Format("{0:#,##0}", dHKSHAKSANGAMT);
                table.Rows[i + 1]["HKSTOTALAMT"] = string.Format("{0:#,##0}", dHKSTOTALAMT);
                table.Rows[i + 1]["HKSJANGHAKAMT"] = string.Format("{0:#,##0}", dHKSJANGHAKAMT);
            }

            return table;
        }
        #endregion   

        #region  Description : 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_73KAD980", fsHKSHLGUBN,
                                                        fsHKSCRYEAR,
                                                        fsHKSCRBUNGI,
                                                        fsHKSCRDATE);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            SectionReport rpt = new TYHRKB021R(dt);

            rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

            (new TYERGB001P(rpt, dt)).ShowDialog();

        }
        #endregion


    }
}
