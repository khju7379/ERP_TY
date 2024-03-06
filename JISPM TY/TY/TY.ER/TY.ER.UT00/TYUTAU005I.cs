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
    /// TANK 용량 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.10.31 18:03
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_6AVHN618 : 탱크 번호 조회
    ///  TY_P_UT_6AVHP619 : TANK 용량 관리 조회
    ///  TY_P_UT_6AVHV620 : TANK 용량 관리 등록
    ///  TY_P_UT_6AVHW621 : TANK 용량 관리 수정
    ///  TY_P_UT_6AVHX622 : TANK 용량 관리 삭제
    ///  TY_P_UT_6AVI2623 : TANK 용량 관리 체크
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_6AVI9624 : TANK 용량 관리(탱크 조회)
    ///  TY_S_UT_6AVI9625 : TANK 용량 관리
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
    ///  INQ_FXM : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  CNTANKNO : 탱크번호
    ///  IPKLQTY : K/L량
    ///  JLTANK : 탱크번호
    /// </summary>
    public partial class TYUTAU005I : TYBase
    {
        public string fsTANKNO = string.Empty;

        #region Description : 페이지 로드
        public TYUTAU005I()
        {
            InitializeComponent();
        }

        private void TYUTAU005I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_6AVI9625, "TATANKNO", "TALEVEL");

            SetStartingFocus(this.TXT01_JLTANK);
        }
        #endregion

        #region Description : 탱크 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.FPS91_TY_S_UT_6AVI9624.Initialize();
            this.FPS91_TY_S_UT_6AVI9625.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_6AVHN618", this.TXT01_JLTANK.GetValue().ToString().Trim());
            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_6AVI9624.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_6AVHP619", dt.Rows[0]["TATANKNO"].ToString().Trim());
                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_UT_6AVI9625.SetValue(dt);

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_TATANKNO.SetValue(dt.Rows[0]["TATANKNO"].ToString());
                    this.TXT01_TALEVEL.SetValue(dt.Rows[0]["TALEVEL"].ToString());
                    fsTANKNO = dt.Rows[0]["TATANKNO"].ToString();
                }
            }
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_FXM_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.FPS91_TY_S_UT_6AVI9625.Initialize();

            this.DbConnector.CommandClear();

            if (this.TXT01_TALEVEL.GetValue().ToString() != "")
            {

                this.DbConnector.Attach("TY_P_UT_6B1EC640", this.TXT01_TATANKNO.GetValue().ToString().Trim(),
                                                            this.TXT01_TALEVEL.GetValue().ToString());
            }
            else
            {
                this.DbConnector.Attach("TY_P_UT_6AVHP619", this.TXT01_TATANKNO.GetValue().ToString().Trim());
            }
            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_6AVI9625.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                this.TXT01_TATANKNO.SetValue(dt.Rows[0]["TATANKNO"].ToString());
                this.TXT01_TALEVEL.SetValue(dt.Rows[0]["TALEVEL"].ToString());
                fsTANKNO = dt.Rows[0]["TATANKNO"].ToString().Trim();
            }
            else
            {
                this.TXT01_TALEVEL.SetValue("");
            }
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_6AVHX622", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_FXM_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_UT_6AVI9625.GetDataSourceInclude(TSpread.TActionType.Remove, "TATANKNO", "TALEVEL");

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

            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    // 등록
                    this.DbConnector.CommandClear();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        this.DbConnector.Attach("TY_P_UT_6AVHV620", ds.Tables[0].Rows[i]["TATANKNO"].ToString(),
                                                                    ds.Tables[0].Rows[i]["TALEVEL"].ToString(),
                                                                    ds.Tables[0].Rows[i]["TAGKL"].ToString()
                                                                    );
                    }
                    this.DbConnector.ExecuteTranQueryList();
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    // 수정
                    this.DbConnector.CommandClear();
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        this.DbConnector.Attach("TY_P_UT_6AVHW621", ds.Tables[1].Rows[i]["TAGKL"].ToString(),
                                                                    ds.Tables[1].Rows[i]["TATANKNO"].ToString(),
                                                                    ds.Tables[1].Rows[i]["TALEVEL"].ToString()
                                                                    );
                    }
                    this.DbConnector.ExecuteTranQueryList();

                }
                this.BTN61_INQ_FXM_Click(null, null);

                this.ShowMessage("TY_M_GB_23NAD873");
            }
            catch
            {
            }
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            ds.Tables.Add(this.FPS91_TY_S_UT_6AVI9625.GetDataSourceInclude(TSpread.TActionType.New, "TATANKNO", "TALEVEL", "TAGKL"));

            ds.Tables.Add(this.FPS91_TY_S_UT_6AVI9625.GetDataSourceInclude(TSpread.TActionType.Update, "TATANKNO", "TALEVEL", "TAGKL"));

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_6AVI2623", ds.Tables[0].Rows[i]["TATANKNO"].ToString(),
                                                            ds.Tables[0].Rows[i]["TALEVEL"].ToString());
                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    //메시지
                    this.ShowCustomMessage("[" + ds.Tables[0].Rows[i]["TALEVEL"].ToString() + "/" + ds.Tables[0].Rows[i]["TAGKL"].ToString() + "] 이미 등록된 자료입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                    e.Successed = false;
                    return;
                }
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

        #region Description : 탱크번호 그리드 더블클릭
        private void FPS91_TY_S_UT_6AVI9624_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {   
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_UT_6AVHP619", this.FPS91_TY_S_UT_6AVI9624.GetValue("TATANKNO").ToString().Trim());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_6AVI9625.SetValue(dt);

            if (dt.Rows.Count > 0)
            {   
                this.TXT01_TALEVEL.SetValue(dt.Rows[0]["TALEVEL"].ToString());
            }
            this.TXT01_TATANKNO.SetValue(this.FPS91_TY_S_UT_6AVI9624.GetValue("TATANKNO").ToString().Trim());
            
            fsTANKNO = this.FPS91_TY_S_UT_6AVI9624.GetValue("TATANKNO").ToString().Trim();
        }
        #endregion

        #region Description : 용량관리 그리드 수정모드
        private void FPS91_TY_S_UT_6AVI9625_EditModeOn(object sender, EventArgs e)
        {
            this.TXT01_TATANKNO.SetValue(this.FPS91_TY_S_UT_6AVI9625.GetValue("TATANKNO").ToString().Trim());
            this.TXT01_TALEVEL.SetValue(this.FPS91_TY_S_UT_6AVI9625.GetValue("TALEVEL").ToString().Trim());
        }
        #endregion

        #region Description : 행 추가 이벤트 (탱크번호 입력)
        private void FPS91_TY_S_UT_6AVI9625_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_UT_6AVI9625.SetValue(e.RowIndex, "TATANKNO", fsTANKNO);
        }
        #endregion

        private void TXT01_JLTANK_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.BTN61_INQ);
            }
        }
    }
}
