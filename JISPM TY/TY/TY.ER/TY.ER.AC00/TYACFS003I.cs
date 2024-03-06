using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 기준일수별 충당금 설정관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.07.23 09:20
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_27N9E177 : 기준일수별 충당금 설정율 조회
    ///  TY_P_AC_27N9G178 : 기준일수별 충당금 설정율 등록
    ///  TY_P_AC_27N9H179 : 기준일수별 충당금 설정율 수정
    ///  TY_P_AC_27N9H180 : 기준일수별 충당금 설정율 삭제
    ///  TY_P_AC_27N14205 : 기준일수별 충당금 설정율 체크
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_27NBK191 : 기준일수 충당금 설정율 관리
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  COPY : 복사
    ///  INQ : 조회
    ///  SAV : 저장
    ///  BRYYMM : 기준년월
    /// </summary>
    public partial class TYACFS003I : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYACFS003I()
        {
            InitializeComponent();
        }

        private void TYACFS003I_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_27NBK191, "BRYYMM");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_27NBK191, "BRDAY");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_27NBK191, "BRRATE");

            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27NBK191, "BRYYMM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27NBK191, "BRDAY");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_BRYYMM.SetValue(DateTime.Now.ToString("yyyyMM"));

            SetStartingFocus(DTP01_BRYYMM);

            this.BTN61_INQ_Click(null, null);  

        }
        #endregion

        #region Description : 복사 버튼 이벤트
        private void BTN61_COPY_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYACFS003B()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_27N9E177", this.ControlFactory, "01");
            this.FPS91_TY_S_AC_27NBK191.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DataTableColumnAdd(ds.Tables[0], "BRHISAB", TYUserInfo.EmpNo);
            this.DataTableColumnAdd(ds.Tables[1], "BRHISAB", TYUserInfo.EmpNo);

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_27N9G178", ds.Tables[0]); //ADD
            this.DbConnector.Attach("TY_P_AC_27N9H179", ds.Tables[1]); //UPDATE

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");
            this.BTN61_INQ_Click(null, null);            
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_27N9H180", dt);

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");
            this.BTN61_INQ_Click(null, null);

        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int iRowCnt = 0;
            int iRowEqual = 0;

            DataSet ds = new DataSet();

            // 저장
            ds.Tables.Add(this.FPS91_TY_S_AC_27NBK191.GetDataSourceInclude(TSpread.TActionType.New, "BRYYMM", "BRDAY","BRRATE"));

            // 수정
            ds.Tables.Add(this.FPS91_TY_S_AC_27NBK191.GetDataSourceInclude(TSpread.TActionType.Update, "BRYYMM", "BRDAY","BRRATE"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
                e.Successed = false;
                return;
            }

            // 저장 체크
            DataSet dsChk = ds.Copy();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                iRowEqual = 0;

                if (ds.Tables[0].Rows[i]["BRDAY"].ToString() != "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_27N14205", ds.Tables[0].Rows[i]["BRYYMM"].ToString(),
                                                                ds.Tables[0].Rows[i]["BRDAY"].ToString());
                    iRowCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                    if (iRowCnt > 0)
                    {
                        this.ShowMessage("TY_M_AC_27N1A207");
                        e.Successed = false;
                        return;
                    }
                }

                for (int j = 0; j < dsChk.Tables[0].Rows.Count; j++)
                {
                    if (ds.Tables[0].Rows[i]["BRDAY"].ToString() == dsChk.Tables[0].Rows[j]["BRDAY"].ToString())
                    {
                        iRowEqual = iRowEqual + 1;
                    }
                }

                if (iRowEqual > 1)
                {
                    this.ShowMessage("TY_M_AC_27N1A207");
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

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_27NBK191.GetDataSourceInclude(TSpread.TActionType.Remove, "BRYYMM", "BRDAY");

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

        #region Description : FPS91_TY_S_AC_27NBK191_RowInserted 이벤트
        private void FPS91_TY_S_AC_27NBK191_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_27NBK191.SetValue(e.RowIndex, "BRYYMM", DateTime.Now.ToString("yyyy-MM"));
        }
        #endregion

    }
}
