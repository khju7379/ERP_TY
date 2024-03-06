using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.Windows.Forms;

namespace TY.ER.HR00
{
    /// <summary>
    /// 인사기본사항 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2014.11.05 16:11
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BBGV367 : 인사기본사항 조회
    ///  TY_P_HR_4BBGY368 : 인사기본사항 등록
    ///  TY_P_HR_4BBH0369 : 인사기본사항 수정
    ///  TY_P_HR_4BBH2370 : 인사기본사항 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_4BBH3371 : 인사기본사항 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  KBSABUN : 사번
    ///  KBHANGL : 한글이름
    /// </summary>
    public partial class TYHRKB002S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRKB002S()
        {
            InitializeComponent();
        }

        private void TYHRKB002S_Load(object sender, System.EventArgs e)
        {
            ToolStripMenuItem InsaReport = new ToolStripMenuItem("인사기록부");
            InsaReport.Click += new EventHandler(InsaReport_ToolStripMenuItem_Click);

            this.FPS91_TY_S_HR_4BBH3371.CurrentContextMenu.Items.AddRange(
                  new System.Windows.Forms.ToolStripItem[] { new ToolStripSeparator(), InsaReport });

            this.UP_Set_JuminAuthCheck(CBO01_INQ_AUTH);

            this.CBH01_KBBUSEO.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.SetStartingFocus(this.TXT01_KBSABUN);            

        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            
            this.FPS91_TY_S_HR_4BBH3371.Initialize();
            this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_HR_4BBGV367", this.TXT01_KBHANGL.GetValue(), this.TXT01_KBSABUN.GetValue().ToString());
            this.DbConnector.Attach("TY_P_HR_58PH2772", this.CBO01_INQOPTION.GetValue(),
                                                        this.CBO01_KBPENSGUBN.GetValue(),
                                                        TYUserInfo.SecureKey,
                                                        this.CBO01_INQ_AUTH.GetValue().ToString(),
                                                        this.TXT01_KBHANGL.GetValue().ToString(),
                                                        this.TXT01_KBSABUN.GetValue().ToString(),
                                                        this.CBH01_KBJKCD.GetValue(), 
                                                        this.CBH01_KBBUSEO.GetValue()                                                        
                                                        );
            string dd = this.CBO01_KBPENSGUBN.GetValue().ToString();


            this.FPS91_TY_S_HR_4BBH3371.SetValue(this.DbConnector.ExecuteDataTable());           

        }
        #endregion

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYHRKB002I(string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : FPS91_TY_S_AC_2AU3I922_CellDoubleClick 이벤트
        private void FPS91_TY_S_HR_4BBH3371_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYHRKB002I(this.FPS91_TY_S_HR_4BBH3371.GetValue("KBSABUN").ToString()
                     )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BBH2370", dt);
            this.DbConnector.Attach("TY_P_HR_A7RFD829", dt);
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null,null);
        }
        
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_4BBH3371.GetDataSourceInclude(TSpread.TActionType.Remove, "KBSABUN");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            //발령사항에 자료가 있는지 체크
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BCH4388", dt.Rows[i]["KBSABUN"].ToString());
                DataTable ds = this.DbConnector.ExecuteDataTable();

                if (ds.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_HR_4BLAD468");
                    e.Successed = false;
                    return;
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

        #region  Description : 인사기록부 팝업 이벤트
        private void InsaReport_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((new TYHRKB016P(this.FPS91_TY_S_HR_4BBH3371.GetValue("KBSABUN").ToString()
                                )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 피크대상자 생성 팝업 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            if ((new TYHRKB002B()).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        //#region  Description : 주민번호 조회 권한 체크
        //private void UP_Set_JuminAuthCheck(TYComboBox CboCheck )
        //{
        //    if (TYUserInfo.PerAuth != "Y")
        //    {
        //        CboCheck.SetValue("N");
        //        CboCheck.Enabled = false;
        //    }
        //}
        //#endregion
    }
}
