using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 투하자산 부실채권 관리 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2012.07.19 10:52
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_27H64059 : EIS 마감 CHECK  확인
    ///  TY_P_AC_27JAS105 : EIS 투하자산 부실채권 조회
    ///  TY_P_AC_27JAS106 : EIS 투하자산 부실채권 등록
    ///  TY_P_AC_27JAT107 : EIS 투하자산 부실채권 수정
    ///  TY_P_AC_27JAT108 : EIS 투하자산 부실채권 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_27JB2110 : EIS 투하자산 부실채권 관리
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_243AY315 : 작업이 불가합니다.
    ///  TY_M_AC_27H6I062 : EIS 마감 년월이 존재 하지 않습니다.
    ///  TY_M_AC_27H6I063 : EIS 적용 완료상태 입니다. (처리 불가)
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_23S40973 : 동일한 코드가 존재합니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  EINCDDP : 부서코드
    ///  EINVEND : 거래처
    ///  EINYYMM : 년월
    /// </summary>
    public partial class TYACPC007S : TYBase
    {
        #region Description : Page_Load
        public TYACPC007S()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_27JB2110, "EINCDDP", "CDSONM", "EINCDDP"); // 스프레드 CODE HELP (부서코드)
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_27JB2110, "EINVEND", "VENDNM", "EINVEND"); // 스프레드 CODE HELP (거래처 코드)

        }

        private void TYACPC007S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            //키필드는 신규일때만 수정된다.
            //this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27JB2110, "EINYYMM","EINCDDP", "EINVEND");

            //this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27JB2110, "EINYYMM");
            //this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27JB2110, "EINCDDP");
            //this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27JB2110, "EINVEND");

            this.CBH01_EINCDDP.DummyValue = DateTime.Now.ToString("yyyyMMdd"); // 부서 기준일자 처리

            this.TXT01_EINYYMM.Focus();

            this.BTN61_INQ_Click(null, null);
        } 
        #endregion

        #region Description : 조회
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_27JAS105", this.ControlFactory, "01");
            this.FPS91_TY_S_AC_27JB2110.SetValue(this.DbConnector.ExecuteDataTable());
        } 
        #endregion

        #region Description : 삭제
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_27JAT108", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        } 
        #endregion

        #region Description : 저장
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_27JAS106", ds.Tables[0]); //저장
            this.DbConnector.Attach("TY_P_AC_27JAT107", ds.Tables[1]); //수정
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD873");
        } 
        #endregion


        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_AC_27JB2110.GetDataSourceInclude(TSpread.TActionType.New, "EINYYMM", "EINCDDP", "EINVEND", "EINNAMT", "EINSAYU", "EINBIGO"));
            ds.Tables.Add(this.FPS91_TY_S_AC_27JB2110.GetDataSourceInclude(TSpread.TActionType.Update, "EINYYMM", "EINCDDP", "EINVEND", "EINNAMT", "EINSAYU", "EINBIGO"));

            //신규
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_27JAS105", ds.Tables[0].Rows[i]["EINYYMM"].ToString(), ds.Tables[0].Rows[i]["EINCDDP"].ToString(), ds.Tables[0].Rows[i]["EINVEND"].ToString());
                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_GB_23S40973");
                    e.Successed = false;
                    return;
                }

                // 마감 완료 CHECK 
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_27H64059", ds.Tables[0].Rows[i]["EINYYMM"].ToString().Substring(0, 4), ds.Tables[0].Rows[i]["EINYYMM"].ToString().Substring(4, 2));
                DataTable dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count == 0)
                {
                    this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                    e.Successed = false;
                    return;
                }
                else
                {
                    if (dt1.Rows[0]["ECGUBUN"].ToString() == "Y")
                    {
                        this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                        e.Successed = false;
                        return;
                    }
                }
            }

            //수정시 CHECK
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                // 마감 완료 CHECK 
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_27H64059", ds.Tables[1].Rows[i]["EINYYMM"].ToString().Substring(0, 4), ds.Tables[1].Rows[i]["EINYYMM"].ToString().Substring(4, 2));
                DataTable dt2 = this.DbConnector.ExecuteDataTable();

                if (dt2.Rows.Count == 0)
                {
                    this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                    e.Successed = false;
                    return;
                }
                else
                {
                    if (dt2.Rows[0]["ECGUBUN"].ToString() == "Y")
                    {
                        this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
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
            DataTable dt = this.FPS91_TY_S_AC_27JB2110.GetDataSourceInclude(TSpread.TActionType.Remove, "EINYYMM");

            //삭제시 CHECK
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // 마감 완료 CHECK 
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_27H64059", dt.Rows[i]["EINYYMM"].ToString().Substring(0, 4), dt.Rows[i]["EINYYMM"].ToString().Substring(4, 2));
                DataTable dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count == 0)
                {
                    this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                    e.Successed = false;
                    return;
                }
                else
                {
                    if (dt1.Rows[0]["ECGUBUN"].ToString() == "Y")
                    {
                        this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                        e.Successed = false;
                        return;
                    }
                }
            }

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

        #region  Description : 그리드 이벤트 처리(부서코드 처리)
        private void FPS91_TY_S_AC_27JB2110_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            if (e.Column != 1)
                return;

            // 부서명을 가져오기 위해서 스프레드의 예산년도에 파라미터 날짜를 넣음.
            string year = FPS91_TY_S_AC_27JB2110.GetValue(e.Row, "EINYYMM").ToString() + "01";

            TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_27JB2110, "EINCDDP");
            if (tyCodeBox != null)
                tyCodeBox.DummyValue = year;
        }
        #endregion

        #region Description : TXT01_EINYYMM_ValueChanged 이벤트 (부서코드 처리)
        private void TXT01_EINYYMM_TextChanged(object sender, EventArgs e)
        {
            if (TXT01_EINYYMM.GetValue().ToString() != "")
            {
                this.CBH01_EINCDDP.DummyValue = TXT01_EINYYMM.GetValue() + "01";
            }
            else
            {
                this.CBH01_EINCDDP.DummyValue = DateTime.Now.ToString("yyyyMMdd");
            }
        }
        #endregion
    }
}