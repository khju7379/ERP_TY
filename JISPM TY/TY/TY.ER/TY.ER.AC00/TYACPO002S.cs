using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 품목별매출현황 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.07.10 14:52
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_37A2X065 : EIS 품목별매출현황 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_37A35071 : EIS 품목별매출현황 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  ERSCDDP : 사업부
    ///  ERSYYMM : 년월
    /// </summary>
    public partial class TYACPO002S : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYACPO002S()
        {
            InitializeComponent();
        }

        private void TYACPO002S_Load(object sender, System.EventArgs e)
        {
            this.BTN62_REM.ProcessCheck += new TButton.CheckHandler(BTN62_REM_ProcessCheck);

            // 버튼 숨김
            this.BTN62_INQ.Visible   = false;
            this.BTN62_NEW.Visible   = false;
            this.BTN62_REM.Visible   = false;

            this.DTP01_EDCYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.LBL51_EDCCDDP.Visible = true;
            this.CBO01_EDCCDDP.Visible = true;

            this.LBL51_EDUCDDP.Visible = false;
            this.CBO01_EDUCDDP.Visible = false;

            this.LBL51_EDCVEND.Visible = false;
            this.CBH01_EDCVEND.Visible = false;

            UP_Spread_Title(this.DTP01_EDCYYMM.GetValue().ToString());

            this.SetStartingFocus(this.DTP01_EDCYYMM);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sEDCYYMM_TWO = string.Empty;
            string sEDCYYMM_ONE = string.Empty;

            string sSTEDCYYMM = string.Empty;
            string sEDEDCYYMM = string.Empty;
            string sEDCCDDP   = string.Empty;

            sEDCYYMM_TWO = Convert.ToString(int.Parse(this.DTP01_EDCYYMM.GetString().ToString().Substring(0, 4)) - 2);
            sEDCYYMM_ONE = Convert.ToString(int.Parse(this.DTP01_EDCYYMM.GetString().ToString().Substring(0, 4)) - 1);

            sSTEDCYYMM = this.DTP01_EDCYYMM.GetString().ToString().Substring(0, 4) + "01";
            sEDEDCYYMM = this.DTP01_EDCYYMM.GetString().ToString().Substring(0, 6);

            if (this.CBO01_EDCCDDP.GetValue().ToString() != "A00000")
            {
                sEDCCDDP = this.CBO01_EDCCDDP.GetValue().ToString();
            }

            UP_Spread_Title(this.DTP01_EDCYYMM.GetValue().ToString());

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_385AH305",
                sEDCYYMM_TWO.ToString(), // 2년전
                sEDCYYMM_ONE.ToString(), // 1년전
                sSTEDCYYMM.ToString(),
                sEDEDCYYMM.ToString(),
                this.DTP01_EDCYYMM.GetString().ToString().Substring(0, 6),
                sEDCCDDP.ToString()
                );

            // 원본
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach
            //    (
            //    "TY_P_AC_385AH305",
            //    // UTT
            //    this.DTP01_EDCYYMM.GetString().ToString().Substring(0, 6),
            //    sEDCYYMM_TWO.ToString(), // 2년전
            //    sEDCYYMM_ONE.ToString(), // 1년전
            //    sSTEDCYYMM.ToString(),
            //    sEDEDCYYMM.ToString(),
            //    sEDCYYMM_TWO.ToString(), // 2년전
            //    sEDCYYMM_ONE.ToString(), // 1년전
            //    sSTEDCYYMM.ToString(),
            //    sEDEDCYYMM.ToString(),
            //    this.DTP01_EDCYYMM.GetString().ToString().Substring(0, 6),
            //    // SILO
            //    this.DTP01_EDCYYMM.GetString().ToString().Substring(0, 6),
            //    sEDCYYMM_TWO.ToString(), // 2년전
            //    sEDCYYMM_ONE.ToString(), // 1년전
            //    sSTEDCYYMM.ToString(),
            //    sEDEDCYYMM.ToString(),
            //    sEDCYYMM_TWO.ToString(), // 2년전
            //    sEDCYYMM_ONE.ToString(), // 1년전
            //    sSTEDCYYMM.ToString(),
            //    sEDEDCYYMM.ToString(),
            //    this.DTP01_EDCYYMM.GetString().ToString().Substring(0, 6),
            //    sEDCCDDP.ToString()
            //    );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_3825N300.SetValue(dt);
        }
        #endregion

        #region Description : 스프레드 타이틀
        private void UP_Spread_Title(string sDATE)
        {
            string sTwo_Years_Ago = string.Empty;
            string sYears_Ago     = string.Empty;
            string sNow_Date      = string.Empty;

            sTwo_Years_Ago = Convert.ToString(int.Parse(sDATE.ToString().Substring(0, 4)) - 2) + "년";
            sYears_Ago     = Convert.ToString(int.Parse(sDATE.ToString().Substring(0, 4)) - 1) + "년";
            sNow_Date = sDATE.Substring(0, 4) + "년" + sDATE.Substring(4, 2) + "누계";

            this.FPS91_TY_S_AC_3825N300_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_3825N300_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_3825N300_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_AC_3825N300_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_AC_3825N300_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);
            this.FPS91_TY_S_AC_3825N300_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);
            this.FPS91_TY_S_AC_3825N300_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1);

            this.FPS91_TY_S_AC_3825N300_Sheet1.AddColumnHeaderSpanCell(0, 5, 1, 3);
            this.FPS91_TY_S_AC_3825N300_Sheet1.AddColumnHeaderSpanCell(0, 8, 1, 3);

            this.FPS91_TY_S_AC_3825N300_Sheet1.ColumnHeader.Cells[0, 0].Value = "부    서";
            this.FPS91_TY_S_AC_3825N300_Sheet1.ColumnHeader.Cells[0, 1].Value = "부 서 명";
            this.FPS91_TY_S_AC_3825N300_Sheet1.ColumnHeader.Cells[0, 2].Value = "거 래 처";
            this.FPS91_TY_S_AC_3825N300_Sheet1.ColumnHeader.Cells[0, 3].Value = "거래처명";
            this.FPS91_TY_S_AC_3825N300_Sheet1.ColumnHeader.Cells[0, 4].Value = "주요취급화물";

            this.FPS91_TY_S_AC_3825N300_Sheet1.ColumnHeader.Cells[0, 5].Value = "물    량";
            this.FPS91_TY_S_AC_3825N300_Sheet1.ColumnHeader.Cells[0, 8].Value = "매 출 액";

            this.FPS91_TY_S_AC_3825N300_Sheet1.ColumnHeader.Cells[1, 5].Value = sTwo_Years_Ago;
            this.FPS91_TY_S_AC_3825N300_Sheet1.ColumnHeader.Cells[1, 6].Value = sYears_Ago;
            this.FPS91_TY_S_AC_3825N300_Sheet1.ColumnHeader.Cells[1, 7].Value = sNow_Date;

            this.FPS91_TY_S_AC_3825N300_Sheet1.ColumnHeader.Cells[1, 8].Value = sTwo_Years_Ago;
            this.FPS91_TY_S_AC_3825N300_Sheet1.ColumnHeader.Cells[1, 9].Value = sYears_Ago;
            this.FPS91_TY_S_AC_3825N300_Sheet1.ColumnHeader.Cells[1, 10].Value = sNow_Date;

            this.FPS91_TY_S_AC_3825N300_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_3825N300_Sheet1.ColumnHeader.Cells[0, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion

        #region  Description : 생성 버튼 이벤트
        private void BTN61_CREATE_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYACPO002B()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 계획관리 조회 버튼 이벤트
        private void BTN62_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_38627341",
                this.DTP01_EDCYYMM.GetValue().ToString(),
                this.DTP01_EDCYYMM.GetValue().ToString(),
                this.CBO01_EDUCDDP.GetValue().ToString(),
                this.CBH01_EDCVEND.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_3863H344.SetValue(dt);
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN62_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYACPO002I(this.DTP01_EDCYYMM.GetValue().ToString(), this.CBO01_EDUCDDP.GetValue().ToString(), "")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN62_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 삭제 버튼
        private void BTN62_REM_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // 삭제
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_38642345", this.CBO01_EDUCDDP.GetValue().ToString(),
                                                                ds.Tables[0].Rows[i]["EDCVEND"].ToString(),
                                                                this.DTP01_EDCYYMM.GetValue().ToString()
                                                                );
                }

                this.DbConnector.ExecuteTranQueryList();
            }

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN62_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN62_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_3863H344.GetDataSourceInclude(TSpread.TActionType.Remove, "EDCVEND"));

            // 삭제
            if(ds.Tables[0].Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            DataTable dt = new DataTable();

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3871W361",
                    this.CBO01_EDUCDDP.GetValue().ToString(),
                    ds.Tables[0].Rows[i]["EDCVEND"].ToString(),
                    this.DTP01_EDCYYMM.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    //if (double.Parse(dt.Rows[0]["EDCVOLME"].ToString()) != 0 ||
                    //    double.Parse(dt.Rows[0]["EDCMAEAMT"].ToString()) != 0)
                    //{
                    //    this.ShowMessage("TY_M_AC_3871Y362");
                    //    e.Successed = false;
                    //    return;
                    //}
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 주요매출처 스프레드 더블클릭
        private void FPS91_TY_S_AC_3863H344_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYACPO002I(this.DTP01_EDCYYMM.GetValue().ToString(), this.CBO01_EDUCDDP.GetValue().ToString(), this.FPS91_TY_S_AC_3863H344.GetValue("EDCVEND").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN62_INQ_Click(null, null);
        }
        #endregion

        #region Description : 탭 이벤트
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetStartingFocus(this.DTP01_EDCYYMM);

            if (tabControl1.SelectedIndex == 0) // 물량 조회
            {
                // 버튼 보임
                this.LBL51_EDCCDDP.Visible = true;
                this.CBO01_EDCCDDP.Visible = true;

                this.BTN61_INQ.Visible     = true;
                this.BTN61_CREATE.Visible  = true;
                
                // 버튼 숨김
                this.LBL51_EDUCDDP.Visible = false;
                this.CBO01_EDUCDDP.Visible = false;

                this.LBL51_EDCVEND.Visible = false;
                this.CBH01_EDCVEND.Visible = false;

                this.BTN62_INQ.Visible     = false;
                this.BTN62_NEW.Visible     = false;
                this.BTN62_REM.Visible     = false;

                this.BTN61_INQ_Click(null, null);
            }
            else // 계획 관리
            {
                this.FPS91_TY_S_AC_3863H344.Initialize();

                //// 버튼 보임
                this.LBL51_EDUCDDP.Visible = true;
                this.CBO01_EDUCDDP.Visible = true;

                this.LBL51_EDCVEND.Visible = true;
                this.CBH01_EDCVEND.Visible = true;

                this.BTN62_INQ.Visible     = true;
                this.BTN62_NEW.Visible     = true;
                this.BTN62_REM.Visible     = true;

                // 버튼 숨김
                this.LBL51_EDCCDDP.Visible = false;
                this.CBO01_EDCCDDP.Visible = false;

                this.BTN61_INQ.Visible     = false;
                this.BTN61_CREATE.Visible  = false;

                this.BTN62_INQ_Click(null, null);
            }
        }
        #endregion
    }
}