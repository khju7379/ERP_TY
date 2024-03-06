using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.ER.GB00;
using DataDynamics.ActiveReports;

namespace TY.ER.US00
{
    /// <summary>
    /// 일일 출고현황 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.03.28 15:23
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_93SFM197 : 일일 출고현황 조회
    ///  TY_P_US_93SFR198 : 일일 출고현황 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_93SFS199 : 일일 출고현황 조회(일일기준)
    ///  TY_S_US_93SFT200 : 일일 출고현황 조회(모선기준)
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  EDATE : 종료일자
    ///  INQOPTION : 조회구분
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYUSPR008S : TYBase
    {
        #region Description : 페이지 로드
        public TYUSPR008S()
        {
            InitializeComponent();
        }

        private void TYUSPR008S_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_US_93SFS199.Visible = true;
            this.FPS91_TY_S_US_93SFT200.Visible = false;

            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_SDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_93SFS199.Initialize();
            this.FPS91_TY_S_US_93SFT200.Initialize();

            if (this.CBO01_INQOPTION.GetValue().ToString() == "1")  // 일일 출고현황 기준
            {
                this.FPS91_TY_S_US_93SFS199.Visible = true;
                this.FPS91_TY_S_US_93SFT200.Visible = false;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                     "TY_P_US_93SFM197",
                     this.DTP01_SDATE.GetString().ToString(),
                     this.DTP01_EDATE.GetString().ToString()
                    );

                this.FPS91_TY_S_US_93SFS199.SetValue(this.DbConnector.ExecuteDataTable());

                this.SetSpreadSumRow(this.FPS91_TY_S_US_93SFS199, "CHHWAJUNM", "[소 계]", SumRowType.SubTotal);                
            }
            else // 모선 출고현황 기준
            {
                this.FPS91_TY_S_US_93SFS199.Visible = false;
                this.FPS91_TY_S_US_93SFT200.Visible = true;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                     "TY_P_US_93SFR198",
                     this.DTP01_SDATE.GetString().ToString(),
                     this.DTP01_EDATE.GetString().ToString()
                    );

                this.FPS91_TY_S_US_93SFT200.SetValue(this.DbConnector.ExecuteDataTable());
                this.SetSpreadSumRow(this.FPS91_TY_S_US_93SFT200, "CHGOKJONGNM",   "[곡종소계]",    SumRowType.Sum);
                this.SetSpreadSumRow(this.FPS91_TY_S_US_93SFT200, "CHGOKJONGCODE", "[일 계]",       SumRowType.SubTotal);
                this.SetSpreadSumRow(this.FPS91_TY_S_US_93SFT200, "CHGOKJONGCODE", "[월 누 계]",    SumRowType.SubTotal);
                this.SetSpreadSumRow(this.FPS91_TY_S_US_93SFT200, "CHGOKJONGCODE", "[출고량 누계]", SumRowType.Total);
                
            }
        }
        #endregion        

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sProceduereid = string.Empty;

            DataTable dt = new DataTable();

            sProceduereid = this.CBO01_INQOPTION.GetValue().ToString() == "1" ? "TY_P_US_93SFM197" : "TY_P_US_93SFR198";

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                 sProceduereid,
                 this.DTP01_SDATE.GetString().ToString(),
                 this.DTP01_EDATE.GetString().ToString()
                );

            if (this.CBO01_INQOPTION.GetValue().ToString() == "1")
            {
                dt = UP_Day_RemoveAtDt(this.DbConnector.ExecuteDataTable());
            }
            else
            {
                dt = this.DbConnector.ExecuteDataTable();
            }

            if (dt.Rows.Count > 0)
            {
                if (this.CBO01_INQOPTION.GetValue().ToString() == "1")
                {
                    ActiveReport rpt = new TYUSPR008R1();

                    rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
                else
                {
                    ActiveReport rpt = new TYUSPR008R2();

                    rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Description : 소계합계라인 삭제(일별)
        private DataTable UP_Day_RemoveAtDt(DataTable dt)
        {
            int i = 0;           

            DataTable table = new DataTable();

            int nNum = dt.Rows.Count;

            table = dt.Clone();         

            for (i = 0; i < nNum; i++)
            {
                if ( Convert.ToDouble(dt.Rows[i]["ROWNUM"].ToString()) < 100000  )
                {                    
                    DataRow  rw = table.NewRow();
                    rw = dt.Rows[i];
                    table.Rows.Add(rw.ItemArray);
                }
            }            

            return table;
        }
        #endregion   

        #region Description : 소계합계라인 삭제(모선별)
        private DataTable UP_HangCha_RemoveAtDt(DataTable dt)
        {
            int i = 0;

            DataTable table = new DataTable();

            int nNum = dt.Rows.Count;

            table = dt.Clone();

            for (i = 0; i < nNum; i++)
            {
                if (Convert.ToDouble(dt.Rows[i]["ROWNUM"].ToString()) < 1000000)
                {
                    DataRow rw = table.NewRow();

                    rw["ROWNUM"] = dt.Rows[i]["ROWNUM"].ToString();
                    rw["DATE"] =  "("+dt.Rows[i]["DATE"].ToString().Substring(0, 4) + "-" + dt.Rows[i]["DATE"].ToString().Substring(4, 2) + "-" + dt.Rows[i]["DATE"].ToString().Substring(6, 2)+")";
                    rw["CHGOKJONG"] = dt.Rows[i]["CHHANGCHA"].ToString() == "" ? "" : dt.Rows[i]["CHGOKJONG"].ToString();
                    rw["CHGOKJONGNM"] = dt.Rows[i]["CHGOKJONGNM"].ToString();
                    rw["CHHANGCHA"] = dt.Rows[i]["CHHANGCHA"].ToString();
                    rw["CHHANGCHANM"] = dt.Rows[i]["CHHANGCHANM"].ToString();
                    rw["CHMTQTY"] = dt.Rows[i]["CHMTQTY"].ToString();
                    rw["COUNT"] = dt.Rows[i]["COUNT"].ToString();
                    
                    table.Rows.Add(rw);
                }
            }

            return table;
        }
        #endregion   

    }
}