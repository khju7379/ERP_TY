using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.ER.GB00;

namespace TY.ER.UT00
{
    /// <summary>
    /// LPG 사용료 등록 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2019.05.23 16:05
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_95NH1606 : LPG 사용량 조회
    ///  TY_P_UT_95NH2607 : LPG 사용료 등록
    ///  TY_P_UT_95NH6608 : LPG 사용료 수정
    ///  TY_P_UT_95NH6609 : LPG 사용료 삭제
    ///  TY_P_UT_95NH8610 : LPG 사용료 확인
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_95NH0612 : LPG 사용료 관리
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    ///  TY_M_GB_43C9G671 : 삭제 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  YYYYMM : 기준 년월
    /// </summary>
    public partial class TYUTIL015I : TYBase
    {
        #region Description : 폼 로드
        public TYUTIL015I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_95NH0612, "LPHWAJU", "VNSANGHO", "LPHWAJU");
        }

        private void TYUTIL015I_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_95NH0612, "LPYYMMDD", "LPHWAJU");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_YYYYMM.SetValue(System.DateTime.Now.ToString("yyyy-MM"));

            UP_Spread_Title();

            FPS91_TY_S_UT_95NH0612.Initialize();

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.DTP01_YYYYMM);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_95NH0612.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_95NH1606", this.DTP01_YYYYMM.GetString().Substring(0, 6));

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_95NH0612.SetValue(dt);
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_95NH6609", dt);
                this.DbConnector.ExecuteNonQuery();

                this.BTN61_INQ_Click(null, null);
                this.ShowMessage("TY_M_GB_23NAD874");
            }
            catch
            {
                this.ShowMessage("TY_M_GB_43C9G671");
            }
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_UT_95NH0612.GetDataSourceInclude(TSpread.TActionType.Remove, "LPYYMMDD", "LPHWAJU");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;

            }
            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
            e.ArgData = dt;
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            DataTable dt = new DataTable();

            try
            {
                this.DbConnector.CommandClear();

                //신규등록
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_UT_95NH2607", ds.Tables[0].Rows[i]["LPYYMMDD"].ToString(),
                                                                ds.Tables[0].Rows[i]["LPHWAJU"].ToString(),
                                                                ds.Tables[0].Rows[i]["LPUSQTY"].ToString(),
                                                                ds.Tables[0].Rows[i]["LPUSDANGA"].ToString(),
                                                                ds.Tables[0].Rows[i]["LPUSAMT"].ToString(),
                                                                ds.Tables[0].Rows[i]["LPELQTY"].ToString(),
                                                                ds.Tables[0].Rows[i]["LPELCTDANGA"].ToString(),
                                                                ds.Tables[0].Rows[i]["LPELSEDANGA"].ToString(),
                                                                ds.Tables[0].Rows[i]["LPELAMT"].ToString(),
                                                                ds.Tables[0].Rows[i]["LPREPAIRAMT"].ToString(),
                                                                ds.Tables[0].Rows[i]["LPGITAAMT"].ToString(),
                                                                ds.Tables[0].Rows[i]["LPTOTAMT"].ToString(),
                                                                TYUserInfo.EmpNo
                                                                );
                }

                //수정
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_UT_95NH6608", ds.Tables[1].Rows[i]["LPUSQTY"].ToString(),
                                                                ds.Tables[1].Rows[i]["LPUSDANGA"].ToString(),
                                                                ds.Tables[1].Rows[i]["LPUSAMT"].ToString(),
                                                                ds.Tables[1].Rows[i]["LPELQTY"].ToString(),
                                                                ds.Tables[1].Rows[i]["LPELCTDANGA"].ToString(),
                                                                ds.Tables[1].Rows[i]["LPELSEDANGA"].ToString(),
                                                                ds.Tables[1].Rows[i]["LPELAMT"].ToString(),
                                                                ds.Tables[1].Rows[i]["LPREPAIRAMT"].ToString(),
                                                                ds.Tables[1].Rows[i]["LPGITAAMT"].ToString(),
                                                                ds.Tables[1].Rows[i]["LPTOTAMT"].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["LPYYMMDD"].ToString(),
                                                                ds.Tables[1].Rows[i]["LPHWAJU"].ToString()
                                                                );
                    
                }
                this.DbConnector.ExecuteTranQuery();

                this.BTN61_INQ_Click(null, null);

                this.ShowMessage("TY_M_GB_23NAD873");
            }
            catch
            {
                this.ShowMessage("TY_M_AC_246A2488");
            }
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            ds.Tables.Add(this.FPS91_TY_S_UT_95NH0612.GetDataSourceInclude(TSpread.TActionType.New, "LPYYMMDD", "LPHWAJU", "LPUSQTY", "LPUSDANGA", "LPUSAMT", "LPELQTY", "LPELCTDANGA", "LPELSEDANGA", "LPELAMT", "LPREPAIRAMT", "LPGITAAMT", "LPTOTAMT"));

            ds.Tables.Add(this.FPS91_TY_S_UT_95NH0612.GetDataSourceInclude(TSpread.TActionType.Update, "LPYYMMDD", "LPHWAJU", "LPUSQTY", "LPUSDANGA", "LPUSAMT", "LPELQTY", "LPELCTDANGA", "LPELSEDANGA", "LPELAMT", "LPREPAIRAMT", "LPGITAAMT", "LPTOTAMT"));

            // 저장 체크
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_95NH8610", ds.Tables[0].Rows[i]["LPYYMMDD"].ToString().Substring(0, 6),
                                                            ds.Tables[0].Rows[i]["LPHWAJU"].ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowCustomMessage("이미 등록된 자료입니다.[" + ds.Tables[0].Rows[i]["LPYYMMDD"].ToString() + "][" + ds.Tables[0].Rows[i]["LPHWAJU"].ToString() + "]",
                                            "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }

                double dLPTOTAMT = 0;

                if(ds.Tables[0].Rows[i]["LPUSQTY"].ToString() != "" && ds.Tables[0].Rows[i]["LPUSDANGA"].ToString() != "")
                {
                    ds.Tables[0].Rows[i]["LPUSAMT"] = Math.Round(Convert.ToDouble(ds.Tables[0].Rows[i]["LPUSQTY"].ToString()) * Convert.ToDouble(ds.Tables[0].Rows[i]["LPUSDANGA"].ToString()),0);
                    dLPTOTAMT += Convert.ToDouble(ds.Tables[0].Rows[i]["LPUSAMT"].ToString());
                }

                if (ds.Tables[0].Rows[i]["LPELQTY"].ToString() != "" && ds.Tables[0].Rows[i]["LPELSEDANGA"].ToString() != "")
                {
                    ds.Tables[0].Rows[i]["LPELAMT"] = Math.Round(Convert.ToDouble(ds.Tables[0].Rows[i]["LPELQTY"].ToString()) * Convert.ToDouble(ds.Tables[0].Rows[i]["LPELCTDANGA"].ToString()) * Convert.ToDouble(ds.Tables[0].Rows[i]["LPELSEDANGA"].ToString()), 0);
                    dLPTOTAMT += Convert.ToDouble(ds.Tables[0].Rows[i]["LPELAMT"].ToString());
                }
                if (ds.Tables[0].Rows[i]["LPREPAIRAMT"].ToString() != "")
                {
                    dLPTOTAMT += Convert.ToDouble(ds.Tables[0].Rows[i]["LPREPAIRAMT"].ToString());
                }
                if (ds.Tables[0].Rows[i]["LPGITAAMT"].ToString() != "")
                {
                    dLPTOTAMT += Convert.ToDouble(ds.Tables[0].Rows[i]["LPGITAAMT"].ToString());
                }
                ds.Tables[0].Rows[i]["LPTOTAMT"] = Math.Floor(dLPTOTAMT / 10) * 10;
            }

            // 수정 체크
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                double dLPTOTAMT = 0;

                if (ds.Tables[1].Rows[i]["LPUSQTY"].ToString() != "" && ds.Tables[1].Rows[i]["LPUSDANGA"].ToString() != "")
                {
                    ds.Tables[1].Rows[i]["LPUSAMT"] = Math.Round(Convert.ToDouble(ds.Tables[1].Rows[i]["LPUSQTY"].ToString()) * Convert.ToDouble(ds.Tables[1].Rows[i]["LPUSDANGA"].ToString()), 0);
                    dLPTOTAMT += Convert.ToDouble(ds.Tables[1].Rows[i]["LPUSAMT"].ToString());
                }

                if (ds.Tables[1].Rows[i]["LPELQTY"].ToString() != "" && ds.Tables[1].Rows[i]["LPELSEDANGA"].ToString() != "")
                {
                    ds.Tables[1].Rows[i]["LPELAMT"] = Math.Round(Convert.ToDouble(ds.Tables[1].Rows[i]["LPELQTY"].ToString()) * Convert.ToDouble(ds.Tables[1].Rows[i]["LPELCTDANGA"].ToString()) * Convert.ToDouble(ds.Tables[1].Rows[i]["LPELSEDANGA"].ToString()), 0);
                    dLPTOTAMT += Convert.ToDouble(ds.Tables[1].Rows[i]["LPELAMT"].ToString());
                }
                if (ds.Tables[1].Rows[i]["LPREPAIRAMT"].ToString() != "")
                {
                    dLPTOTAMT += Convert.ToDouble(ds.Tables[1].Rows[i]["LPREPAIRAMT"].ToString());
                }
                if (ds.Tables[1].Rows[i]["LPGITAAMT"].ToString() != "")
                {
                    dLPTOTAMT += Convert.ToDouble(ds.Tables[1].Rows[i]["LPGITAAMT"].ToString());
                }
                ds.Tables[1].Rows[i]["LPTOTAMT"] = Math.Floor(dLPTOTAMT / 10) * 10;
            }

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
            e.ArgData = ds;
        }
        #endregion

        #region Description : 스프레드 타이틀 변경
        private void UP_Spread_Title()
        {
            this.FPS91_TY_S_UT_95NH0612_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_UT_95NH0612_Sheet1.RowHeaderColumnCount = 1;

            //(현재ROW, 현재COLUMN, 묶을ROW수, 묶을 COLUMN수)
            this.FPS91_TY_S_UT_95NH0612_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1); // 일자
            this.FPS91_TY_S_UT_95NH0612_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1); // 화주
            this.FPS91_TY_S_UT_95NH0612_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1); // 화주명
            this.FPS91_TY_S_UT_95NH0612_Sheet1.AddColumnHeaderSpanCell(0, 3, 1, 3); // 화주명
            this.FPS91_TY_S_UT_95NH0612_Sheet1.AddColumnHeaderSpanCell(0, 6, 1, 4); // 화주명
            this.FPS91_TY_S_UT_95NH0612_Sheet1.AddColumnHeaderSpanCell(0, 10, 2, 1); // 수선비
            this.FPS91_TY_S_UT_95NH0612_Sheet1.AddColumnHeaderSpanCell(0, 11, 2, 1); // 기타비용
            this.FPS91_TY_S_UT_95NH0612_Sheet1.AddColumnHeaderSpanCell(0, 12, 2, 1); // 최종청구금액

            this.FPS91_TY_S_UT_95NH0612_Sheet1.ColumnHeader.Cells[0, 3].Value = "LNG 사용 비용";
            this.FPS91_TY_S_UT_95NH0612_Sheet1.ColumnHeader.Cells[0, 6].Value = "전기 사용 비용";

            this.FPS91_TY_S_UT_95NH0612_Sheet1.ColumnHeader.Cells[1, 3].Value = "사용량";
            this.FPS91_TY_S_UT_95NH0612_Sheet1.ColumnHeader.Cells[1, 4].Value = "사용단가";
            this.FPS91_TY_S_UT_95NH0612_Sheet1.ColumnHeader.Cells[1, 5].Value = "사용금액";
            this.FPS91_TY_S_UT_95NH0612_Sheet1.ColumnHeader.Cells[1, 6].Value = "사용량";
            this.FPS91_TY_S_UT_95NH0612_Sheet1.ColumnHeader.Cells[1, 7].Value = "CT단가";
            this.FPS91_TY_S_UT_95NH0612_Sheet1.ColumnHeader.Cells[1, 8].Value = "계절별단가";
            this.FPS91_TY_S_UT_95NH0612_Sheet1.ColumnHeader.Cells[1, 9].Value = "사용금액";

            this.FPS91_TY_S_UT_95NH0612_Sheet1.ColumnHeader.Cells[0, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_UT_95NH0612_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion

        private void FPS91_TY_S_UT_95NH0612_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_UT_95NH0612.SetValue(e.RowIndex, "LPELCTDANGA", "6.00");
        }

    }
}
