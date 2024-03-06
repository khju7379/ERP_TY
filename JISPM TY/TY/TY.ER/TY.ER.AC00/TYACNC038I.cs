using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.AC00
{
    /// <summary>
    /// 특별손익관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2018.12.17 16:52
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_8CHFB326 : 특별손익관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_8CHFB327 : 특별손익관리 조회
    /// 
    ///  # 알림문자 정보 ####
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
    ///  AJBPAC : 손익계정코드
    ///  AJCDAC : 계정과목
    ///  AJDPAC : 귀속부서
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYACNC038I : TYBase
    {
        #region Description : 폼 로드
        public TYACNC038I()
        {
            InitializeComponent();
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_8CHFB327, "AJBPAC", "AJBPACNM", "AJBPAC");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_8CHFB327, "AJCDAC", "AJCDACNM", "AJCDAC");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_8CHFB327, "AJDPAC", "AJDPACNM", "AJDPAC");
        }

        private void TYACNC038I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_8CHFB327, "AJYYMM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_8CHFB327, "AJBPAC");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_8CHFB327, "AJCDAC");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_8CHFB327, "AJDPAC");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.FPS91_TY_S_AC_8CHFB327.Initialize();            

            DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy"));

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.DTP01_SDATE);

            TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_8CHFB327, "AJDPAC");
            if (tyCodeBox != null)
                tyCodeBox.DummyValue = System.DateTime.Now.ToString("yyyyMM");
            return;
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            try
            {
                this.FPS91_TY_S_AC_8CHFB327.Initialize();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_8CHFB326", this.DTP01_SDATE.GetValue().ToString(), "", "", "", "");
                DataTable dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_AC_8CHFB327.SetValue(dt);
            }
            catch
            {
                
            }
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_8CHFQ332", dt);
                this.DbConnector.ExecuteNonQuery();

                this.ShowMessage("TY_M_GB_23NAD874");

                BTN61_INQ_Click(null, null);
            }
            catch
            {
                this.ShowMessage("TY_M_GB_43C9G671");
            }
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_8CHFB327.GetDataSourceInclude(TSpread.TActionType.Remove, "AJYYMM", "AJSEQ", "AJBPAC", "AJCDAC", "AJDPAC");

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

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            DataTable dt = new DataTable();

            try
            {
                //신규등록
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_8CHFP330", ds.Tables[0].Rows[i]["AJYYMM"].ToString(),
                                                                ds.Tables[0].Rows[i]["AJYYMM"].ToString(),
                                                                ds.Tables[0].Rows[i]["AJBPAC"].ToString(),                                                                
                                                                ds.Tables[0].Rows[i]["AJCDAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["AJDPAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["AJMONAMT"].ToString(),
                                                                ds.Tables[0].Rows[i]["AJMEMO"].ToString(),
                                                                TYUserInfo.EmpNo
                                                                );
                    this.DbConnector.ExecuteTranQuery();
                }

                //수정
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_AC_8CHFP331",
                                                                ds.Tables[1].Rows[i]["AJMONAMT"].ToString(),
                                                                ds.Tables[1].Rows[i]["AJMEMO"].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["AJYYMM"].ToString(),
                                                                ds.Tables[1].Rows[i]["AJSEQ"].ToString(),
                                                                ds.Tables[1].Rows[i]["AJBPAC"].ToString(),
                                                                ds.Tables[1].Rows[i]["AJCDAC"].ToString(),
                                                                ds.Tables[1].Rows[i]["AJDPAC"].ToString()
                                                                );
                    this.DbConnector.ExecuteTranQuery();
                }

                this.ShowMessage("TY_M_GB_23NAD873");

                BTN61_INQ_Click(null, null);
            }
            catch
            {
                this.ShowMessage("TY_M_AC_246A2488");
            }
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            ds.Tables.Add(this.FPS91_TY_S_AC_8CHFB327.GetDataSourceInclude(TSpread.TActionType.New, "AJYYMM","AJSEQ", "AJDPAC", "AJBPAC", "AJCDAC", "AJDPAC", "AJMONAMT", "AJMEMO"));
            ds.Tables.Add(this.FPS91_TY_S_AC_8CHFB327.GetDataSourceInclude(TSpread.TActionType.Update, "AJYYMM", "AJSEQ", "AJDPAC", "AJBPAC", "AJCDAC", "AJDPAC", "AJMONAMT", "AJMEMO"));
         

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_8CHFB326", ds.Tables[0].Rows[i]["AJYYMM"].ToString(),
                                                            ds.Tables[0].Rows[i]["AJSEQ"].ToString(),
                                                            ds.Tables[0].Rows[i]["AJBPAC"].ToString(),
                                                            ds.Tables[0].Rows[i]["AJCDAC"].ToString(),
                                                            ds.Tables[0].Rows[i]["AJDPAC"].ToString()
                                                            );
                dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_GB_23S40973");
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

        #region Description : row 추가 이벤트
        private void FPS91_TY_S_AC_8CHFB327_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_8CHFB327.SetValue("AJYYMM", DateTime.Now.ToString("yyyy-MM"));
            this.FPS91_TY_S_AC_8CHFB327.SetValue("AJSEQ", "0");
        }
        #endregion

        
    }
}
