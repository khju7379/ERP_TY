using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.UT00
{
    /// <summary>
    /// TANK 재원관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.10.26 13:16
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_6AQIG575 : TANK 재원관리 조회
    ///  TY_P_UT_6AQIH576 : TANK 재원관리 등록
    ///  TY_P_UT_6AQII577 : TANK 재원관리 수정
    ///  TY_P_UT_6AQIJ578 : TANK 재원관리 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_6AQIL579 : TANK 재원관리
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
    ///  JLTANK : 탱크번호
    /// </summary>
    public partial class TYUTAU001I : TYBase
    {
        #region Description : 페이지 로드
        public TYUTAU001I()
        {
            InitializeComponent();
        }

        private void TYUTAU001I_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_6AQIL579, "TANKNO");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.TXT01_JLTANK);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sTANKNO = this.TXT01_JLTANK.GetValue().ToString();

            if (sTANKNO.Length >= 3)
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_66SDH426", this.TXT01_JLTANK.GetValue().ToString().Trim());

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowCustomMessage("탱크번호를 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                else
                {
                    this.FPS91_TY_S_UT_6AQIL579.Initialize();

                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_UT_6AQIG575", sTANKNO);

                    this.FPS91_TY_S_UT_6AQIL579.SetValue(this.DbConnector.ExecuteDataTable());
                }
            }
            else if (sTANKNO == "")
            {
                this.FPS91_TY_S_UT_6AQIL579.Initialize();

                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_6AQIG575", sTANKNO);

                this.FPS91_TY_S_UT_6AQIL579.SetValue(this.DbConnector.ExecuteDataTable());
            }
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_6AQIJ578", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_UT_6AQIL579.GetDataSourceInclude(TSpread.TActionType.Remove, "TANKNO");

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

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();

                // 신규등록
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_UT_6AQIH576", ds.Tables[0].Rows[i]["TANKNO"].ToString().Trim(),
                                                                ds.Tables[0].Rows[i]["TANKCAPA"].ToString(),
                                                                ds.Tables[0].Rows[i]["TANKHIGH"].ToString(),
                                                                ds.Tables[0].Rows[i]["TANKLEVEL"].ToString(),
                                                                ds.Tables[0].Rows[i]["TANKTEMPH"].ToString(),
                                                                ds.Tables[0].Rows[i]["TANKTEMPL"].ToString(),
                                                                "A",
                                                                TYUserInfo.EmpNo
                                                                );
                    
                }
                this.DbConnector.ExecuteTranQuery();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();

                // 수정
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_UT_6AQII577", ds.Tables[1].Rows[i]["TANKCAPA"].ToString(),
                                                                ds.Tables[1].Rows[i]["TANKHIGH"].ToString(),
                                                                ds.Tables[1].Rows[i]["TANKLEVEL"].ToString(),
                                                                ds.Tables[1].Rows[i]["TANKTEMPH"].ToString(),
                                                                ds.Tables[1].Rows[i]["TANKTEMPL"].ToString(),
                                                                "C",
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["TANKNO"].ToString().Trim()
                                                                );
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            ds.Tables.Add(this.FPS91_TY_S_UT_6AQIL579.GetDataSourceInclude(TSpread.TActionType.New, "TANKNO", "TANKCAPA", "TANKHIGH", "TANKLEVEL", "TANKTEMPH", "TANKTEMPL"));

            ds.Tables.Add(this.FPS91_TY_S_UT_6AQIL579.GetDataSourceInclude(TSpread.TActionType.Update, "TANKNO", "TANKCAPA", "TANKHIGH", "TANKLEVEL", "TANKTEMPH", "TANKTEMPL"));

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

        #region Description : 그리드 탱크 번호 체크
        private void FPS91_TY_S_UT_6AQIL579_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            string sTANKNO = this.FPS91_TY_S_UT_6AQIL579.GetValue("TANKNO").ToString();

            if (sTANKNO.Length > 3)
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_66SDH426", sTANKNO.Trim());

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowCustomMessage("탱크번호를 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_UT_6AQIL579.SetValue("TANKNO", "");
                }
            }
        }
        #endregion
    }
}
