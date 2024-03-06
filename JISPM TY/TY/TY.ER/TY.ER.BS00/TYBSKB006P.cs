using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.BS00
{
    /// <summary>
    /// 사업부 및 부서관리 팝업 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.07.19 17:55
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_77JI0233 : 사업부 및 부서관리 조회
    ///  TY_P_AC_77JI1234 : 사업부 및 부서관리 체크
    ///  TY_P_AC_77JI5229 : 사업부 및 부서관리 등록
    ///  TY_P_AC_77JI7230 : 사업부 및 부서관리 수정
    ///  TY_P_AC_77JI8231 : 사업부 및 부서관리 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_77JI1235 : 사업부 및 부서관리 조회
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
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  PRYEAR : 년도
    /// </summary>
    public partial class TYBSKB006P : TYBase
    {
        #region Description : 폼 로드
        public TYBSKB006P()
        {
            InitializeComponent();
        }

        private void TYBSKB006P_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            //this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_77JI1235, "DPYEAR");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_77JI1235, "DPCODE");

            // 등록 체크
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            // 삭제 체크
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.TXT01_PRYEAR.SetValue(UP_Get_MaxYear());

            BTN61_INQ_Click(null, null);
        }   
        #endregion
        
        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_77JI1235.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_77JI0233", this.TXT01_PRYEAR.GetValue().ToString());
            this.FPS91_TY_S_AC_77JI1235.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_77JI8231", dt);
                this.DbConnector.ExecuteNonQueryList();

                bool b = UP_DelBSINWONMF(dt);

                if (b == true)
                {
                    this.BTN61_INQ_Click(null, null);
                    this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지
                }
                else
                {
                    this.ShowMessage("TY_M_GB_43C9G671"); 
                }
            }
            catch
            {
                this.ShowMessage("TY_M_GB_43C9G671"); 
            }
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_77JI1235.GetDataSourceInclude(TSpread.TActionType.Remove, "DPYEAR", "DPCODE");

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

            DataTable dt = new DataTable();

            try
            {
                //신규등록
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_AC_77JI5229", ds.Tables[0].Rows[i]["DPYEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["DPCODE"].ToString(),
                                                                ds.Tables[0].Rows[i]["DPNAME"].ToString(),
                                                                ds.Tables[0].Rows[i]["DPHRDP"].ToString(),
                                                                TYUserInfo.EmpNo
                                                                );
                    this.DbConnector.ExecuteTranQuery();
                }

                //수정
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_AC_77JI7230", ds.Tables[1].Rows[i]["DPNAME"].ToString(),
                                                                ds.Tables[1].Rows[i]["DPHRDP"].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["DPYEAR"].ToString(),
                                                                ds.Tables[1].Rows[i]["DPCODE"].ToString()
                                                                );
                    this.DbConnector.ExecuteTranQuery();
                }
                this.BTN61_INQ_Click(null, null);
                this.ShowMessage("TY_M_GB_23NAD873");
            }
            catch
            {
                this.ShowMessage("TY_M_AC_246A2488");
            }
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            DataSet ds = new DataSet();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_77JI1235.GetDataSourceInclude(TSpread.TActionType.New, "DPYEAR", "DPCODE", "DPNAME", "DPHRDP"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_77JI1235.GetDataSourceInclude(TSpread.TActionType.Update, "DPYEAR", "DPCODE", "DPNAME", "DPHRDP"));

            //신규
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_AC_77JI1234",
                                       ds.Tables[0].Rows[i]["DPYEAR"].ToString(),
                                       ds.Tables[0].Rows[i]["DPCODE"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_GB_23S40973");
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

        #region Description : Row 추가
        private void FPS91_TY_S_AC_77JI1235_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_77JI1235.SetValue("DPYEAR", this.TXT01_PRYEAR.GetValue().ToString());
        }
        #endregion

        #region Description : 부서별 임직원 관리 삭제
        private bool UP_DelBSINWONMF(DataTable dt)
        {   
            try
            {
                this.DbConnector.CommandClear();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_788E1390", dt.Rows[i]["DPYEAR"].ToString(),
                                                                dt.Rows[i]["DPCODE"].ToString());
                }
                this.DbConnector.ExecuteTranQueryList();

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Description : 최근년도 가져오기
        private string UP_Get_MaxYear()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_8AME7004");
            string sMaxYear = this.DbConnector.ExecuteScalar().ToString();

            return sMaxYear;
        }
        #endregion
    }
}
