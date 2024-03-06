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
    /// 상조회사용내역조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.11.26 16:45
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_5BQJH233 : 상조회 사용내역 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_5BQJH234 : 상조회 사용내역 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  KBSABUN : 사번
    ///  SJACHL1 : 상조회 상위계정1
    ///  INQOPTION2 : 조회구분
    ///  EDATE : 종료일자
    ///  INQOPTION : 조회구분
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRSJ002S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRSJ002S()
        {
            InitializeComponent();
        }

        private void TYHRSJ002S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_SDATE.SetValue(System.DateTime.Now.AddDays(-15).ToString("yyyyMMdd"));
            this.DTP01_EDATE.SetValue(System.DateTime.Now.ToString("yyyyMMdd"));
            this.SetStartingFocus(DTP01_SDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_5BQJH234.Initialize();
            this.DbConnector.CommandClear();
            if (CBO01_INQOPTION2.GetValue().ToString() == "1")
            {
                this.DbConnector.Attach("TY_P_HR_5BQJH233", this.DTP01_SDATE.GetString().ToString(), this.DTP01_EDATE.GetString().ToString(), this.CBH01_KBSABUN.GetValue(), this.CBH01_SJACHL1.GetValue());
            }
            else if (CBO01_INQOPTION2.GetValue().ToString() == "2")
            {
                this.DbConnector.Attach("TY_P_HR_69JID172", this.DTP01_SDATE.GetString().ToString(), this.DTP01_EDATE.GetString().ToString(), this.CBH01_KBSABUN.GetValue(), this.CBH01_SJACHL1.GetValue());
            }
            else
            {
                this.DbConnector.Attach("TY_P_HR_69JID173", this.DTP01_SDATE.GetString().ToString(), this.DTP01_EDATE.GetString().ToString(), this.CBH01_KBSABUN.GetValue(), this.CBH01_SJACHL1.GetValue());
            }
            this.FPS91_TY_S_HR_5BQJH234.SetValue(UP_Convertdt(this.DbConnector.ExecuteDataTable()));                       

            if (this.FPS91_TY_S_HR_5BQJH234.CurrentRowCount > 0)
            {
                this.SetSpreadSumRow(this.FPS91_TY_S_HR_5BQJH234, "SJIOSABUNNM", "[합 계]", SumRowType.Total);  
            }
        }
        #endregion

        #region  Description : UP_Convertdt 이벤트
        private DataTable  UP_Convertdt( DataTable dt )
        {
            DataRow row;
            int nNum = dt.Rows.Count;

            if (nNum > 0)
            {
                row = dt.NewRow();
                dt.Rows.InsertAt(row, nNum);

                dt.Rows[nNum]["ROWNUM"] = 0;
                dt.Rows[nNum]["SJIODATE"] = "";
                dt.Rows[nNum]["SJIOCODE"] = "";
                dt.Rows[nNum]["SJIOCODENM"] = "";
                dt.Rows[nNum]["SJIOSABUN"] = "";
                dt.Rows[nNum]["SJIOSABUNNM"] = "[합 계]";

                dt.Rows[nNum]["SJIOAMT"] = 0;
                dt.Rows[nNum]["SJIORKAC"] = "";
                dt.Rows[nNum]["WOLJAN"] = dt.Compute("SUM(WOLJAN)", " ROWNUM = 1 ").ToString();
                dt.Rows[nNum]["INSJIOAMT"] = dt.Compute("SUM(INSJIOAMT)", " ROWNUM > 0 ").ToString();
                dt.Rows[nNum]["OUTSJIOAMT"] = dt.Compute("SUM(OUTSJIOAMT)", " ROWNUM > 0 ").ToString();
                if (CBO01_INQOPTION2.GetValue().ToString() == "1")
                {
                    dt.Rows[nNum]["JANAMT"] = Convert.ToDouble(Get_Numeric(dt.Compute("SUM(WOLJAN)", " ROWNUM = 1 ").ToString())) + Convert.ToDouble(Get_Numeric(dt.Compute("SUM(INSJIOAMT)", " ROWNUM > 0 ").ToString())) - Convert.ToDouble(Get_Numeric(dt.Compute("SUM(OUTSJIOAMT)", " ROWNUM > 0 ").ToString()));
                }
                else
                {
                    dt.Rows[nNum]["JANAMT"] = 0;
                }
            }

            return dt;
        }
        #endregion

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYHRSJ002I(string.Empty, string.Empty, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 그리드 더블 클릭 이벤트
        private void FPS91_TY_S_HR_5BQJH234_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYHRSJ002I(this.FPS91_TY_S_HR_5BQJH234.GetValue("SJIODATE").ToString(),
                                this.FPS91_TY_S_HR_5BQJH234.GetValue("SJIOCODE").ToString(),
                                this.FPS91_TY_S_HR_5BQJH234.GetValue("SJIOSABUN").ToString()
                                )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 생성 버튼 이벤트
        private void BTN61_CREATE_Click(object sender, EventArgs e)
        {
            if ((new TYHRSJ002B()).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_HR_69JEU155",
                                        dt.Rows[i][0].ToString(),
                                        dt.Rows[i][1].ToString(),
                                        dt.Rows[i][2].ToString()
                                        );
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);
        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_5BQJH234.GetDataSourceInclude(TSpread.TActionType.Remove, "SJIODATE", "SJIOCODE", "SJIOSABUN");

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