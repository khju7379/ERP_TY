using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 계열사 주요시설현황 관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.07.22 10:06
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_37MAD187 : EIS 계열사 주요시설현황 조회
    ///  TY_P_AC_37MAF188 : EIS 계열사 주요시설현황 등록
    ///  TY_P_AC_37MAH189 : EIS 계열사 주요시설현황 수정
    ///  TY_P_AC_37MAH190 : EIS 계열사 주요시설현황 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_37MAM191 : EIS 계열사 주요시설현황 조회
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
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  ESPLCMPY : 계열사구분
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACPO012I : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYACPO012I()
        {
            InitializeComponent();
        }

        private void TYACPO012I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
                        
            this.DTP01_GSTYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            this.CBH01_ESPLCMPY.SetValue("TY");

            this.BTN61_INQ_Click(null, null);

            this.SetStartingFocus(this.DTP01_GSTYYMM);

        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_37MAM191.Initialize(); 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_37MAD187", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), this.CBH01_ESPLCMPY.GetValue());
            this.FPS91_TY_S_AC_37MAM191.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion
        
        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_37MAH190", dt);
            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            //저장
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_37MAF188", ds.Tables[0].Rows[i]["EIYYMM"].ToString(),
                                                            ds.Tables[0].Rows[i]["EISUBGN"].ToString(),
                                                            ds.Tables[0].Rows[i]["EIYYMM"].ToString(),
                                                            ds.Tables[0].Rows[i]["EISUBGN"].ToString(),
                                                            ds.Tables[0].Rows[i]["EIASSETNAME"].ToString(),
                                                            ds.Tables[0].Rows[i]["EIOWN"].ToString(),
                                                            ds.Tables[0].Rows[i]["EIQTY"].ToString(),
                                                            ds.Tables[0].Rows[i]["EIAREA"].ToString(),
                                                            ds.Tables[0].Rows[i]["EIBIGO"].ToString(),
                                                            TYUserInfo.EmpNo
                                                            ); 
            }
            //수정
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_37MAH189", ds.Tables[1].Rows[i]["EIASSETNAME"].ToString(),
                                                            ds.Tables[1].Rows[i]["EIOWN"].ToString(),
                                                            ds.Tables[1].Rows[i]["EIQTY"].ToString(),
                                                            ds.Tables[1].Rows[i]["EIAREA"].ToString(),
                                                            ds.Tables[1].Rows[i]["EIBIGO"].ToString(),
                                                            TYUserInfo.EmpNo,
                                                            ds.Tables[1].Rows[i]["EIYYMM"].ToString(),
                                                            ds.Tables[1].Rows[i]["EISUBGN"].ToString(),
                                                            ds.Tables[1].Rows[i]["EISEQ"].ToString()
                                                            ); 
            }         
            
            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD873");   
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_37MAM191.GetDataSourceInclude(TSpread.TActionType.New, "EIYYMM", "EISUBGN", "EISEQ", "EIASSETNAME", "EIOWN", "EIQTY", "EIAREA", "EIBIGO" ));
            ds.Tables.Add(this.FPS91_TY_S_AC_37MAM191.GetDataSourceInclude(TSpread.TActionType.Update, "EIYYMM", "EISUBGN", "EISEQ", "EIASSETNAME", "EIOWN", "EIQTY", "EIAREA", "EIBIGO"));

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

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_37MAM191.GetDataSourceInclude(TSpread.TActionType.Remove, "EIYYMM", "EISUBGN", "EISEQ");

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

        #region Description : FPS91_TY_S_AC_37MAM191_RowInserted 이벤트
        private void FPS91_TY_S_AC_37MAM191_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_37MAM191.SetValue(e.RowIndex, "EIYYMM", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
            this.FPS91_TY_S_AC_37MAM191.SetValue(e.RowIndex, "EISUBGN", this.CBH01_ESPLCMPY.GetValue().ToString());
        }
        #endregion

        #region Description : 복사 버튼 이벤트
        private void BTN61_COPY_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYACPO012B()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}
