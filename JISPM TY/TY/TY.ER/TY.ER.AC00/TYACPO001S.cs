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
    ///  # 알림문자 정보 ####ddd
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  ERSCDDP : 사업부
    ///  ERSYYMM : 년월
    /// </summary>
    public partial class TYACPO001S : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYACPO001S()
        {
            InitializeComponent();
        }

        private void TYACPO001S_Load(object sender, System.EventArgs e)
        {
            this.BTN62_BATCH.ProcessCheck += new TButton.CheckHandler(BTN62_BATCH_ProcessCheck);
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.FPS91_TY_S_AC_37J55184.Visible = true;

            // 버튼 숨김
            this.BTN62_INQ.Visible   = false;
            this.BTN62_BATCH.Visible = false;

            this.LBL51_EDQYEAR.Visible = false;
            this.TXT01_EDQYEAR.Visible = false;

            this.LBL51_EDQCDDP.Visible = false;
            this.CBO01_EDQCDDP.Visible = false;

            UP_Spread_Title(this.CBO01_INQOPTION.GetValue().ToString());

            this.DTP01_EDUYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.SetStartingFocus(this.DTP01_EDUYYMM);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sSTEDUYYMM = string.Empty;
            string sEDEDUYYMM = string.Empty;
            string sEDUCDDP   = string.Empty;

            UP_Spread_Title(this.CBO01_INQOPTION.GetValue().ToString());

            DataTable dt = new DataTable();

            if (CBO01_INQOPTION.GetValue().ToString() == "1")
            {
                sSTEDUYYMM = this.DTP01_EDUYYMM.GetString().ToString().Substring(0, 4) + "01";
                sEDEDUYYMM = this.DTP01_EDUYYMM.GetString().ToString().Substring(0, 6);

                if (this.CBO01_EDUCDDP.GetValue().ToString() != "A00000")
                {
                    sEDUCDDP = this.CBO01_EDUCDDP.GetValue().ToString();
                }

                this.FPS91_TY_S_AC_37J55184.Visible = true;
 
                this.FPS91_TY_S_AC_37J55184.Initialize();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_37M4V206",
                    this.DTP01_EDUYYMM.GetString().ToString().Substring(0, 6),
                    this.DTP01_EDUYYMM.GetString().ToString().Substring(0, 4),
                    sSTEDUYYMM.ToString(),
                    sEDEDUYYMM.ToString(),
                    this.DTP01_EDUYYMM.GetString().ToString().Substring(0, 6),
                    this.DTP01_EDUYYMM.GetString().ToString().Substring(0, 6),
                    sEDUCDDP.ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_AC_37J55184.SetValue(dt);
            }
            else
            {
                string sSTEDUYYMM_Pre = string.Empty;
                string sEDEDUYYMM_Pre = string.Empty;

                sSTEDUYYMM = this.DTP01_EDUYYMM.GetString().ToString().Substring(0, 4) + "01";
                sEDEDUYYMM = this.DTP01_EDUYYMM.GetString().ToString().Substring(0, 6);


                sSTEDUYYMM_Pre = Convert.ToString(int.Parse(sSTEDUYYMM.Substring(0, 4)) - 1) + "01";
                sEDEDUYYMM_Pre = Convert.ToString(int.Parse(sEDEDUYYMM.Substring(0, 4)) - 1) + sEDEDUYYMM.Substring(4, 2);

                if (this.CBO01_EDUCDDP.GetValue().ToString() != "A00000")
                {
                    sEDUCDDP = this.CBO01_EDUCDDP.GetValue().ToString();
                }

                this.FPS91_TY_S_AC_37J55184.Visible = true;

                this.FPS91_TY_S_AC_37J55184.Initialize();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_37M5V208",
                    this.DTP01_EDUYYMM.GetString().ToString().Substring(0, 6),
                    this.DTP01_EDUYYMM.GetString().ToString().Substring(0, 6),
                    sSTEDUYYMM_Pre.ToString(),
                    sEDEDUYYMM_Pre.ToString(),
                    sSTEDUYYMM.ToString(),
                    sEDEDUYYMM.ToString(),
                    sSTEDUYYMM_Pre.ToString(),
                    sEDEDUYYMM_Pre.ToString(),
                    sSTEDUYYMM.ToString(),
                    sEDEDUYYMM.ToString(),
                    sSTEDUYYMM_Pre.ToString(),
                    sEDEDUYYMM_Pre.ToString(),
                    sSTEDUYYMM.ToString(),
                    sEDEDUYYMM.ToString(),
                    sEDUCDDP.ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_AC_37J55184.SetValue(dt);
            }
        }
        #endregion

        #region Description : 스프레드 타이틀
        private void UP_Spread_Title(string sGUBUN)
        {
            if (sGUBUN.ToString() == "1") // 월별 기준
            {
                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeaderRowCount = 2;
                this.FPS91_TY_S_AC_37A35071_Sheet1.RowHeaderColumnCount = 1;

                this.FPS91_TY_S_AC_37A35071_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
                this.FPS91_TY_S_AC_37A35071_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
                this.FPS91_TY_S_AC_37A35071_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);
                this.FPS91_TY_S_AC_37A35071_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);

                this.FPS91_TY_S_AC_37A35071_Sheet1.AddColumnHeaderSpanCell(0, 4, 1, 3);
                this.FPS91_TY_S_AC_37A35071_Sheet1.AddColumnHeaderSpanCell(0, 7, 1, 3);
                this.FPS91_TY_S_AC_37A35071_Sheet1.AddColumnHeaderSpanCell(0, 10, 1, 2);

                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[0, 0].Value = "년  월";
                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[0, 1].Value = "부  서";
                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[0, 2].Value = "부서명";
                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[0, 3].Value = "구  분";
                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[0, 4].Value = "월  간";

                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[1, 4].Value = "계  획";
                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[1, 5].Value = "실  적";
                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[1, 6].Value = "달성율(%)";

                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[0, 7].Value = "누  계";

                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[1, 7].Value = "계  획";
                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[1, 8].Value = "실  적";
                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[1, 9].Value = "달성율(%)";

                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[0, 10].Value = "년  간";

                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[1, 10].Value = "계  획";
                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[1, 11].Value = "달성율(%)";


                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[0, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            }
            else // 전년도 기준
            {
                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeaderRowCount = 2;
                this.FPS91_TY_S_AC_37A35071_Sheet1.RowHeaderColumnCount = 1;

                this.FPS91_TY_S_AC_37A35071_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
                this.FPS91_TY_S_AC_37A35071_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
                this.FPS91_TY_S_AC_37A35071_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);
                this.FPS91_TY_S_AC_37A35071_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);

                this.FPS91_TY_S_AC_37A35071_Sheet1.AddColumnHeaderSpanCell(0, 4, 1, 3);
                this.FPS91_TY_S_AC_37A35071_Sheet1.AddColumnHeaderSpanCell(0, 7, 1, 3);
                this.FPS91_TY_S_AC_37A35071_Sheet1.AddColumnHeaderSpanCell(0, 10, 1, 2);

                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[0, 0].Value = "년  월";
                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[0, 1].Value = "부  서";
                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[0, 2].Value = "부서명";
                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[0, 3].Value = "구  분";

                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[0, 4].Value = "전년도 누계";

                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[1, 4].Value = "계  획";
                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[1, 5].Value = "실  적";
                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[1, 6].Value = "달성율(%)";

                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[0, 7].Value = "당해년도 누계";

                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[1, 7].Value = "계  획";
                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[1, 8].Value = "실  적";
                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[1, 9].Value = "달성율(%)";

                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[0, 10].Value = "전년대비증(감)";

                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[1, 10].Value = "증(감액)";
                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[1, 11].Value = "달성율(%)";


                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                this.FPS91_TY_S_AC_37A35071_Sheet1.ColumnHeader.Cells[0, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            }
        }
        #endregion

        #region  Description : 생성 버튼 이벤트
        private void BTN61_CREATE_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYACPO001B()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 계획관리 조회 버튼 이벤트
        private void BTN62_INQ_Click(object sender, EventArgs e)
        {
            string sEDQCDDP = string.Empty;
            string sEDQCDDPNM = string.Empty;

            if (this.CBO01_EDQCDDP.GetValue().ToString() == "T00000") 
            {
                sEDQCDDP   = "T00000";
                sEDQCDDPNM = "UTT사업본부";
            }
            else
            {
                sEDQCDDP   = "S00000";
                sEDQCDDPNM = "SILO사업본부";
            }

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_37N5M228",
                sEDQCDDP.ToString(),
                this.TXT01_EDQYEAR.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_37N2I216.SetValue(dt);

            if (dt.Rows.Count <= 0)
            {
                for (int i = 0; i < 12; i++)
                {
                    this.FPS91_TY_S_AC_37N2I216_Sheet1.AddRows(i, 1);

                    this.FPS91_TY_S_AC_37N2I216.ActiveSheet.RowHeader.Cells[i, 0].Text = "N";

                    this.FPS91_TY_S_AC_37N2I216.SetValue(i, "EDQCDDP",   sEDQCDDP.ToString());
                    this.FPS91_TY_S_AC_37N2I216.SetValue(i, "EDQCDDPNM", sEDQCDDPNM.ToString());
                    this.FPS91_TY_S_AC_37N2I216.SetValue(i, "EDQYEAR",   this.TXT01_EDQYEAR.GetValue().ToString());
                    this.FPS91_TY_S_AC_37N2I216.SetValue(i, "EDQMONTH",  (i+1).ToString("00"));
                    this.FPS91_TY_S_AC_37N2I216.SetValue(i, "EDQPLQUAN", 0);
                }
            }
        }
        #endregion

        #region  Description : 계획관리 처리 버튼 이벤트
        private void BTN62_BATCH_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // 신규
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_37O9Z230", ds.Tables[0].Rows[i]["EDQCDDP"].ToString(),
                                                                ds.Tables[0].Rows[i]["EDQYEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["EDQMONTH"].ToString(),
                                                                ds.Tables[0].Rows[i]["EDQPLQUAN"].ToString());
                }

                this.DbConnector.ExecuteTranQueryList();
            }

            // 수정
            if (ds.Tables[1].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_37OA3231", ds.Tables[1].Rows[i]["EDQPLQUAN"].ToString(),
                                                                ds.Tables[1].Rows[i]["EDQCDDP"].ToString(),
                                                                ds.Tables[1].Rows[i]["EDQYEAR"].ToString(),
                                                                ds.Tables[1].Rows[i]["EDQMONTH"].ToString()
                                                                );
                }

                this.DbConnector.ExecuteTranQueryList();
            }

            this.ShowMessage("TY_M_MR_2BF50354");

            this.BTN62_INQ_Click(null, null);
        }
        #endregion

        #region Description : 처리 체크
        private void BTN62_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_37N2I216.GetDataSourceInclude(TSpread.TActionType.New, "EDQCDDP", "EDQYEAR", "EDQMONTH", "EDQPLQUAN"));
            ds.Tables.Add(this.FPS91_TY_S_AC_37N2I216.GetDataSourceInclude(TSpread.TActionType.Update, "EDQCDDP", "EDQYEAR", "EDQMONTH", "EDQPLQUAN"));

            // 신규
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["EDQPLQUAN"].ToString())) == 0)
                {
                    this.ShowMessage("TY_M_AC_37O9W229");
                    e.Successed = false;
                    return;
                }
            }

            // 수정
            for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                if (double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["EDQPLQUAN"].ToString())) == 0)
                {
                    this.ShowMessage("TY_M_AC_37O9W229");
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_MR_2BF50353"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 탭 이벤트
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0) // 물량 조회
            {
                this.SetStartingFocus(this.DTP01_EDUYYMM);

                // 버튼 보임
                this.LBL51_EDUYYMM.Visible = true;
                this.DTP01_EDUYYMM.Visible = true;

                this.LBL51_INQOPTION.Visible = true;
                this.CBO01_INQOPTION.Visible = true;

                this.BTN61_INQ.Visible     = true;
                this.BTN61_CREATE.Visible  = true;
                
                // 버튼 숨김
                this.LBL51_EDQYEAR.Visible = false;
                this.TXT01_EDQYEAR.Visible = false;

                this.LBL51_EDQCDDP.Visible = false;
                this.CBO01_EDQCDDP.Visible = false;

                this.BTN62_INQ.Visible     = false;
                this.BTN62_BATCH.Visible   = false;

                this.BTN61_SAV.Visible = true;
            }
            else // 계획 관리
            {
                this.TXT01_EDQYEAR.Focus();

                this.FPS91_TY_S_AC_37N2I216.Initialize();

                // 버튼 보임
                this.LBL51_EDQYEAR.Visible = true;
                this.TXT01_EDQYEAR.Visible = true;

                this.LBL51_EDQCDDP.Visible = true;
                this.CBO01_EDQCDDP.Visible = true;

                this.BTN62_INQ.Visible     = true;
                this.BTN62_BATCH.Visible   = true;

                // 버튼 숨김
                this.LBL51_EDUYYMM.Visible = false;
                this.DTP01_EDUYYMM.Visible = false;

                this.LBL51_INQOPTION.Visible = false;
                this.CBO01_INQOPTION.Visible = false;

                this.BTN61_INQ.Visible     = false;
                this.BTN61_CREATE.Visible  = false;

                this.BTN61_SAV.Visible = false;
            }

        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // 신규
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_7AKDG860", ds.Tables[0].Rows[i]["EDUSAMT"].ToString(),
                                                                ds.Tables[0].Rows[i]["EDUYYMM"].ToString(),
                                                                ds.Tables[0].Rows[i]["EDUCDDP"].ToString().Substring(0,1));
                }

                this.DbConnector.ExecuteTranQueryList();
            }

            BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_37J55184.GetDataSourceInclude(TSpread.TActionType.Update, "EDUYYMM", "EDUCDDP", "EDGUBUN", "EDUSAMT"));

            if (ds.Tables[0].Rows.Count == 0)
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
    }
}