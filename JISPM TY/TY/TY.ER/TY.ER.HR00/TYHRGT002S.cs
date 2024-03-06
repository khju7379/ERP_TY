using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 일별근무자관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.12.05 14:08
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BSC3589 : 근태월력 일자별 조회
    ///  TY_P_HR_4C5EL649 : 일별근무자관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_4C5EM651 : 일별근무자관리 조회
    ///  TY_S_HR_4C5ER652 : 일별근무자관리 조회(월력조회)
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  ROTGROUP : 교대조
    ///  WORKTABLE : 근무표
    ///  AERDATE : 기준일자
    ///  YYYYMM : 기준 년월
    ///  SYYOILCD : 요일
    /// </summary>
    public partial class TYHRGT002S : TYBase
    {
        #region Description : 페이지 로드
        public TYHRGT002S()
        {
            InitializeComponent();
        }

        private void TYHRGT002S_Load(object sender, System.EventArgs e)
        {

            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            DTP01_YYYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            DTP01_AERDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            DTP01_AERDATE.SetReadOnly(true);
            TXT01_SYYOILCD.SetReadOnly(true);

            this.BTN61_INQ_Click(null, null);

            this.SetStartingFocus(DTP01_YYYYMM);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_HR_4BSC3589",
                DTP01_YYYYMM.GetString().Substring(0, 4),
                DTP01_YYYYMM.GetString().Substring(4, 2)
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                DTP01_AERDATE.SetValue(dt.Rows[0]["UYDATE"].ToString());
                TXT01_SYYOILCD.SetValue(dt.Rows[0]["UYYOILCDNM"].ToString());

                this.FPS91_TY_S_HR_4C5ER652.SetValue(dt);

                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                    (
                    "TY_P_HR_4C5EL649",
                    dt.Rows[0]["UYDATE"].ToString(),
                    "", "", ""
                    );

                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_HR_4C5EM651.SetValue(dt);

                //if (dt.Rows.Count > 0)
                //{
                //    UP_Spread_Desc(dt);
                //}
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        }
        #endregion

        #region Description : 교대조 버튼
        private void BTN61_ROTGROUP_Click(object sender, EventArgs e)
        {
            (new TYHRGT02C1()).ShowDialog();
        }
        #endregion

        #region Description : 근무표 버튼
        private void BTN61_WORKTABLE_Click(object sender, EventArgs e)
        {
            if ((new TYHRGT002B()).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                BTN61_INQ_Click(null,null);
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYHRGT002I(this.DTP01_AERDATE.GetValue().ToString(), string.Empty, string.Empty, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                UP_SelGDRE();
        }
        #endregion

        #region Description : FPS91_TY_S_HR_4C5ER652_CellDoubleClick 이벤트
        private void FPS91_TY_S_HR_4C5ER652_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.UP_RightSpreadDataBinding(this.FPS91_TY_S_HR_4C5ER652.GetValue("UYDATE").ToString());

            this.DTP01_AERDATE.SetValue(this.FPS91_TY_S_HR_4C5ER652.GetValue("UYDATE").ToString());
            this.TXT01_SYYOILCD.SetValue(this.FPS91_TY_S_HR_4C5ER652.GetValue("UYYOILCDNM").ToString());
        }
        #endregion

        #region Description : 일별근무자관리 그리드 더블클릭
        private void UP_RightSpreadDataBinding(string sUYDATE)
        {
            DataTable dt = new DataTable();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_HR_4C5EL649",
                sUYDATE,
                "", "", ""
                );
            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_HR_4C5EM651.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                UP_Spread_Desc(dt);
            }
        }
        #endregion

        #region Description : 일별근무자관리 그리드 더블클릭
        private void FPS91_TY_S_HR_4C5EM651_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

            if ((new TYHRGT002I(this.FPS91_TY_S_HR_4C5EM651.GetValue("GDDATE").ToString(), this.FPS91_TY_S_HR_4C5EM651.GetValue("GDGROUP").ToString(),
                                this.FPS91_TY_S_HR_4C5EM651.GetValue("GDSABUN").ToString(), this.FPS91_TY_S_HR_4C5EM651.GetValue("GDROTGN").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                UP_SelGDRE();
        }
        #endregion

        #region Description : 일별근무자 조회
        private void UP_SelGDRE()
        {
            DataTable dt = new DataTable();
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_HR_4C5EL649",
                this.DTP01_AERDATE.GetValue().ToString(),
                "", "", ""
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_HR_4C5EM651.SetValue(dt);

            UP_Spread_Desc(dt);
        }
        #endregion

        #region Description : 스프레트 틀 만들기
        private void UP_Spread_Desc(DataTable dt)
        {   
            int iE = 0;
            int iN = 0;
            int iO = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if(i > 0)
                {
                    if (dt.Rows[i-1]["GDROTGN"].ToString() != dt.Rows[i]["GDROTGN"].ToString())
                    {   
                        if (dt.Rows[i]["GDROTGN"].ToString() == "2")
                        {
                            iE = i;
                        }
                        else if (dt.Rows[i]["GDROTGN"].ToString() == "3")
                        {
                            iN = i;
                        }
                        else if (dt.Rows[i]["GDROTGN"].ToString() == "5")
                        {
                            iO = i;
                        }
                    }
                }
            }

            this.FPS91_TY_S_HR_4C5EM651_Sheet1.ColumnCount = 10;
            this.FPS91_TY_S_HR_4C5EM651_Sheet1.RowCount = dt.Rows.Count;

            this.FPS91_TY_S_HR_4C5EM651_Sheet1.AddSpanCell(0, 1, iE, 2); 
            this.FPS91_TY_S_HR_4C5EM651_Sheet1.AddSpanCell(iE, 1, iN - iE, 2); 
            this.FPS91_TY_S_HR_4C5EM651_Sheet1.AddSpanCell(iN, 1, iO - iN, 2); 
            this.FPS91_TY_S_HR_4C5EM651_Sheet1.AddSpanCell(iO, 1, dt.Rows.Count - iO, 2); 
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;
                        
            this.DbConnector.CommandClear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_HR_4C8F1664", dt.Rows[i]["GDDATE"].ToString(),
                                                            dt.Rows[i]["GDGROUP"].ToString(),
                                                            dt.Rows[i]["GDSABUN"].ToString(),
                                                            dt.Rows[i]["GDROTGN"].ToString());
            }
            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.UP_RightSpreadDataBinding(this.DTP01_AERDATE.GetString().ToString());          
            
        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            DataTable dt = this.FPS91_TY_S_HR_4C5EM651.GetDataSourceInclude(TSpread.TActionType.Remove, "GDDATE", "GDGROUP",  "GDSABUN",  "GDROTGN");

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
    }
}
