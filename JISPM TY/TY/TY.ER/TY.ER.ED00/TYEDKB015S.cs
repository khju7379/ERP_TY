using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using System.Drawing;

namespace TY.ER.ED00
{
    /// <summary>
    /// 반출통고목록보고서 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2020.05.25 13:06
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_A5PDC550 : 반출통고목록보고서 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_A5PDD552 : 반출통고목록보고서 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYEDKB015S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYEDKB015S()
        {
            InitializeComponent();
        }

        private void TYEDKB015S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            ToolStripMenuItem InsaReport = new ToolStripMenuItem("재전송");
            InsaReport.Click += new EventHandler(ReSend_ToolStripMenuItem_Click);

            this.FPS91_TY_S_UT_A5PDD552.CurrentContextMenu.Items.AddRange(
                  new System.Windows.Forms.ToolStripItem[] { new ToolStripSeparator(), InsaReport });


            this.DTP01_SDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.BTN61_INQ_Click(null, null);

            this.SetStartingFocus(DTP01_SDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_A5PDD552.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_A5PDC550", this.DTP01_SDATE.GetString(), this.DTP01_EDATE.GetString());
            this.FPS91_TY_S_UT_A5PDD552.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_UT_A5PDD552.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_UT_A5PDD552.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_UT_A5PDD552.GetValue(i, "EDNRCVGB").ToString() == "Y")
                    {
                        this.FPS91_TY_S_UT_A5PDD552_Sheet1.Rows[i].ForeColor = Color.Blue;
                    }
                    else if (this.FPS91_TY_S_UT_A5PDD552.GetValue(i, "EDNRCVGB").ToString() == "E")
                    {
                        this.FPS91_TY_S_UT_A5PDD552_Sheet1.Rows[i].ForeColor = Color.Red;
                    }
                }
            }

        }
        #endregion

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYEDKB015I("", "","")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
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
                this.DbConnector.Attach("TY_P_UT_A5TBC594", dt.Rows[i]["EDNDATE"].ToString(),
                                                            dt.Rows[i]["EDNSEQ"].ToString(),
                                                            dt.Rows[i]["EDNJSGB"].ToString());
            }
            this.DbConnector.ExecuteNonQueryList();          

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_UT_A5PDD552.GetDataSourceInclude(TSpread.TActionType.Remove, "EDNDATE", "EDNSEQ", "EDNJSGB", "EDNRCVGB");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["EDNRCVGB"].ToString() == "Y")
                    {

                        this.ShowMessage("TY_M_UT_72SA1811");
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region  Description : FPS91_TY_S_UT_A5PDD552_CellDoubleClick 이벤트
        private void FPS91_TY_S_UT_A5PDD552_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYEDKB015I(this.FPS91_TY_S_UT_A5PDD552.GetValue("EDNDATE").ToString(),
                                this.FPS91_TY_S_UT_A5PDD552.GetValue("EDNSEQ").ToString(),
                                this.FPS91_TY_S_UT_A5PDD552.GetValue("EDNJSGB").ToString()
            )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 반출통고목록 팝업 이벤트
        private void ReSend_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.FPS91_TY_S_UT_A5PDD552.GetValue("EDNRCVGB").ToString() != "Y")
            {
                this.ShowCustomMessage("접수완료된 자료만 재전송 할수 있습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return; 
            }

            if (this.FPS91_TY_S_UT_A5PDD552.GetValue("EDNJSGB").ToString() != "9")
            {
                this.ShowCustomMessage("원본 자료만 재전송 할수 있습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_A5TDM596", this.FPS91_TY_S_UT_A5PDD552.GetValue("EDNDATE").ToString(),
                                                        this.FPS91_TY_S_UT_A5PDD552.GetValue("EDNSEQ").ToString(),
                                                        this.FPS91_TY_S_UT_A5PDD552.GetValue("EDNJSGB").ToString());
            this.DbConnector.ExecuteTranQuery();

            this.ShowCustomMessage("재전송 자료가 복사되었습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            
            BTN61_INQ_Click(null, null);        
        }
        #endregion
    }
}
