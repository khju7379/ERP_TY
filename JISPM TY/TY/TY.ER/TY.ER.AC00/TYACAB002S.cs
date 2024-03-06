using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 관리항목코드 등록 프로그램입니다.
    /// 
    /// 작성자 : 관리자
    /// 작성일 : 2012.03.28 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_23S15942 : 관리항목코드 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_23S19943 : 관리항목코드 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  A2CDMI : 관리항목코드
    /// </summary>
    public partial class TYACAB002S : TYBase
    {

        private TYData DAT02_A2CDMI;
        private TYData DAT02_A2NMCD;
        private TYData DAT02_A2IDPG;
        private TYData DAT02_A2WNPG;

        
        #region Description : Page Load()
        public TYACAB002S()
        {
            InitializeComponent();

            this.DAT02_A2CDMI = new TYData("DAT02_A2CDMI", null);
            this.DAT02_A2NMCD = new TYData("DAT02_A2NMCD", null);
            this.DAT02_A2IDPG = new TYData("DAT02_A2IDPG", null);
            this.DAT02_A2WNPG = new TYData("DAT02_A2WNPG", null);
        }

        private void TYACAB002S_Load(object sender, System.EventArgs e)
        {
            this.ControlFactory.Add(this.DAT02_A2CDMI);
            this.ControlFactory.Add(this.DAT02_A2NMCD);
            this.ControlFactory.Add(this.DAT02_A2IDPG);
            this.ControlFactory.Add(this.DAT02_A2WNPG);

            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            //키필드는 신규일때만 수정된다.
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_23S19943, "A2CDMI");

            this.BTN61_INQ_Click(null, null);

            this.SetStartingFocus(this.TXT01_A2CDMI); 

        }
       #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_23S15942", this.ControlFactory, "01");
            this.FPS91_TY_S_AC_23S19943.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_23S3R968", ds.Tables[0]); //저장

            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //    //this.DbConnector.Attach("TY_P_AC_23S3R968", ds.Tables[0]); //저장

            //    this.DAT02_A2CDMI.SetValue(ds.Tables[0].Rows[i]["A2CDMI"].ToString());
            //    this.DAT02_A2NMCD.SetValue(ds.Tables[0].Rows[i]["A2NMCD"].ToString());
            //    this.DAT02_A2IDPG.SetValue(ds.Tables[0].Rows[i]["A2IDPG"].ToString());
            //    this.DAT02_A2WNPG.SetValue(ds.Tables[0].Rows[i]["A2WNPG"].ToString());

            //    this.DbConnector.Attach("TY_P_AC_23S3R968", this.ControlFactory, "02");

            //    //this.DbConnector.Attach("TY_P_AC_23S3R968", ds.Tables[0].Rows[i]["A2CDMI"].ToString(), ds.Tables[0].Rows[i]["A2NMCD"].ToString(),
            //    //    ds.Tables[0].Rows[i]["A2IDPG"].ToString(), ds.Tables[0].Rows[i]["A2WNPG"].ToString()); //저장

            //}

            this.DbConnector.Attach("TY_P_AC_23S3U970", ds.Tables[1]); //수정

            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD873");         

        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_23S3S969", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_AC_23S19943.GetDataSourceInclude(TSpread.TActionType.New, "A2CDMI", "A2NMCD", "A2IDPG", "A2WNPG"));
            ds.Tables.Add(this.FPS91_TY_S_AC_23S19943.GetDataSourceInclude(TSpread.TActionType.Update, "A2CDMI", "A2NMCD", "A2IDPG", "A2WNPG"));

            //신규
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_23S15942", ds.Tables[0].Rows[i]["A2CDMI"].ToString());
                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_GB_23S40973");
                    e.Successed = false;
                    return;
                }
            }

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 )
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

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_23S19943.GetDataSourceInclude(TSpread.TActionType.Remove, "A2CDMI");

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

        //#region Description : Spread 클릭 이벤트
        //private void FPS91_TY_S_AC_23S19943_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        //{
        //    if ((new TYACAB002I(this.FPS91_TY_S_AC_23S19943.GetValue("A2CDMI").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //        this.BTN61_INQ_Click(null, null);

        //}
        //#endregion

    }
}
