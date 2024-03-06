using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;


namespace TY.ER.BS00
{
    /// <summary>
    /// 사업계획 마감관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.08.22 16:21
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_78MGQ480 : 사업계획 마감관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_78MGQ481 : 사업계획 마감관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    /// </summary>
    public partial class TYBSCR005B : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYBSCR005B()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }

        private void TYBSCR005B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_78MGQ481, "BLYEAR");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_78MGQ481, "BLYEAR");

            this.UP_DataBinding();
        }
        #endregion

        #region  Description : UP_DataBinding 이벤트
        private void UP_DataBinding()
        {
            this.FPS91_TY_S_AC_78MGQ481.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_78MGQ480");
            this.FPS91_TY_S_AC_78MGQ481.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;
            
            this.DbConnector.CommandClear();

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_78MH1484", ds.Tables[0].Rows[i]["BLYEAR"].ToString(),
                                                                (ds.Tables[0].Rows[i]["BLCHKMC"].ToString() == "True" || ds.Tables[0].Rows[i]["BLCHKMC"].ToString() == "Y" ) ? "Y": "N",
                                                                (ds.Tables[0].Rows[i]["BLCHKCM"].ToString() == "True" || ds.Tables[0].Rows[i]["BLCHKCM"].ToString() == "Y") ? "Y" : "N",
                                                                (ds.Tables[0].Rows[i]["BLCHKPR"].ToString() == "True" || ds.Tables[0].Rows[i]["BLCHKPR"].ToString() == "Y") ? "Y" : "N",
                                                                (ds.Tables[0].Rows[i]["BLCHKIN"].ToString() == "True" || ds.Tables[0].Rows[i]["BLCHKIN"].ToString() == "Y") ? "Y" : "N",
                                                                TYUserInfo.EmpNo
                                                                );
                }
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_78MH5486",
                                                                (ds.Tables[1].Rows[i]["BLCHKMC"].ToString() == "True" || ds.Tables[1].Rows[i]["BLCHKMC"].ToString() == "Y") ? "Y" : "N",
                                                                (ds.Tables[1].Rows[i]["BLCHKCM"].ToString() == "True" || ds.Tables[1].Rows[i]["BLCHKCM"].ToString() == "Y") ? "Y" : "N",
                                                                (ds.Tables[1].Rows[i]["BLCHKPR"].ToString() == "True" || ds.Tables[1].Rows[i]["BLCHKPR"].ToString() == "Y") ? "Y" : "N",
                                                                (ds.Tables[1].Rows[i]["BLCHKIN"].ToString() == "True" || ds.Tables[1].Rows[i]["BLCHKIN"].ToString() == "Y") ? "Y" : "N",
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["BLYEAR"].ToString()
                                                                );
                }
            }

            if (this.DbConnector.CommandCount > 0)
            {
                this.DbConnector.ExecuteTranQueryList();
            }

            UP_DataBinding();

            this.ShowMessage("TY_M_GB_23NAD873");

        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            Int16 iCnt = 0;

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_78MGQ481.GetDataSourceInclude(TSpread.TActionType.New, "BLYEAR", "BLCHKMC", "BLCHKCM", "BLCHKPR", "BLCHKIN"));
            ds.Tables.Add(this.FPS91_TY_S_AC_78MGQ481.GetDataSourceInclude(TSpread.TActionType.Update, "BLYEAR", "BLCHKMC", "BLCHKCM", "BLCHKPR", "BLCHKIN"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_7AO9F875", ds.Tables[0].Rows[i]["BLYEAR"].ToString());
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                if (iCnt > 0)
                {
                    this.ShowCustomMessage("사업계획 손익생성이 완료되었습니다! 저장할수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_7AO9F875", ds.Tables[1].Rows[i]["BLYEAR"].ToString());
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                if (iCnt > 0)
                {
                    this.ShowCustomMessage("사업계획 손익생성이 완료되었습니다! 저장할수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }


            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_78MH5485", dt);
            this.DbConnector.ExecuteNonQueryList();

            UP_DataBinding();

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            Int16 iCnt = 0;

            DataTable dt = this.FPS91_TY_S_AC_78MGQ481.GetDataSourceInclude(TSpread.TActionType.Remove, "BLYEAR");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            //사업계획 생성후 삭제 불가
            for( int i = 0; i < dt.Rows.Count; i++ )
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_7AO9F875", dt.Rows[i]["BLYEAR"].ToString());
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                if (iCnt > 0)
                {
                    this.ShowCustomMessage("사업계획 손익생성이 완료되었습니다! 삭제할수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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

        #region  Description : FPS91_TY_S_AC_78MGQ481_RowInserted 이벤트
        private void FPS91_TY_S_AC_78MGQ481_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_78MGQ481.SetValue(e.RowIndex, "BLYEAR", DateTime.Now.ToString("yyyy")  );
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

      

      

    }
}
