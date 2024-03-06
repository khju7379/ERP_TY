using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 투하자금 관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.09.04 20:14
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2948D791 : 투하자금 관리 조회
    ///  TY_P_AC_2948G793 : 투하자금관리 입력
    ///  TY_P_AC_2948G794 : 투하자금관리 수정
    ///  TY_P_AC_2948H795 : 투하자금관리 삭제
    ///  TY_P_AC_2948H796 : 투하자금관리 체크
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2948J800 : 투하자금관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_28D5W379 : 자료가 존재합니다! 삭제후 작업하세요
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
    ///  AJDCDAC : 계정코드
    ///  AJDDPAC : 귀속부서
    ///  AJDTHGB : 투하자금구분
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACNC019S : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYACNC019S()
        {
            InitializeComponent();
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_2948J800, "AJDTHGB", "AJDTHGBNM", "AJDTHGB");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_2948J800, "AJDCDAC", "AJDCDACNM", "AJDCDAC");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_2948J800, "AJDDPAC", "AJDDPACNM", "AJDDPAC");
        }

        private void TYACNC019S_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_2948J800, "AJDDTAC");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_2948J800, "AJDTHGB");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_2948J800, "AJDCDAC");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_2948J800, "AJDDPAC");

            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_2948J800, "AJDDTAC");

            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_2948J800, "AJDTHGB");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_2948J800, "AJDTHGBNM");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_2948J800, "AJDCDAC");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_2948J800, "AJDCDACNM");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_2948J800, "AJDDPAC");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_2948J800, "AJDDPACNM");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_2948J800, "AJDAMT");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            
            this.DTP01_GSTYYMM.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"));
            this.DTP01_GEDYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.CBH01_AJDDPAC.DummyValue = this.DTP01_GSTYYMM.GetValue() + "01";
            
            this.SetStartingFocus(this.DTP01_GSTYYMM);

            this.BTN61_INQ_Click(null, null);  
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_2948D791", this.ControlFactory, "01");
            this.FPS91_TY_S_AC_2948J800.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2948H795", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();            

            this.DbConnector.Attach("TY_P_AC_2948G793", ds.Tables[0]); // 저장
            this.DbConnector.Attach("TY_P_AC_2948G794", ds.Tables[1]); // 수정

            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            Int16 iCnt = 0;

            DataSet ds = new DataSet();

            // 저장
            ds.Tables.Add(this.FPS91_TY_S_AC_2948J800.GetDataSourceInclude(TSpread.TActionType.New, "AJDDTAC", "AJDTHGB", "AJDCDAC", "AJDDPAC", "AJDAMT"));
            // 수정
            ds.Tables.Add(this.FPS91_TY_S_AC_2948J800.GetDataSourceInclude(TSpread.TActionType.Update, "AJDDTAC", "AJDTHGB", "AJDCDAC", "AJDDPAC", "AJDAMT"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
                e.Successed = false;
                return;
            }            

            // 저장 체크
            ds.Tables[0].Columns.Add("AJDHISAB", typeof(string));    
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2948H796", ds.Tables[0].Rows[i]["AJDDTAC"].ToString(), ds.Tables[0].Rows[i]["AJDTHGB"].ToString(), ds.Tables[0].Rows[i]["AJDCDAC"].ToString(), ds.Tables[0].Rows[i]["AJDDPAC"].ToString()); // 저장
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                if (iCnt > 0)
                {
                    this.ShowMessage("TY_M_AC_28D5W379");
                    e.Successed = false;
                    return;
                }

                ds.Tables[0].Rows[i]["AJDHISAB"] = Employer.EmpNo;
            }

            // 수정 체크
            ds.Tables[1].Columns.Add("AJDHISAB", typeof(string));    
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                ds.Tables[1].Rows[i]["AJDHISAB"] = Employer.EmpNo;
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
            DataTable dt = this.FPS91_TY_S_AC_2948J800.GetDataSourceInclude(TSpread.TActionType.Remove, "AJDDTAC", "AJDTHGB", "AJDCDAC", "AJDDPAC");

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

        #region Description : FPS91_TY_S_AC_2948J800_EnterCell 이벤트
        private void FPS91_TY_S_AC_2948J800_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            if (e.Column != 5)
                return;

            string year = FPS91_TY_S_AC_2948J800.GetValue(e.Row, "AJDDTAC").ToString() + "01";
            TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_2948J800, "AJDDPAC");
            if (tyCodeBox != null)
                tyCodeBox.DummyValue = year;
            return;
        }
        #endregion
    }
}
