using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AF00
{
    /// <summary>
    /// EIS GLS 매출관리 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2013.11.01 14:52
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_27H64059 : EIS 마감 CHECK  확인
    ///  TY_P_AC_3C92V659 : EIS 계열사 마감 CHECK  확인
    ///  TY_P_AC_3C94Q662 : EIS 계열사 최종 마감 월 조회
    ///  TY_P_AC_41H5M104 : EIS 계열사 GLS 주요매출 등록
    ///  TY_P_AC_41H5M105 : EIS 계열사 GLS 주요매출  삭제
    ///  TY_P_AC_41H5N106 : EIS 계열사 GLS 주요매출  수정
    ///  TY_P_AC_41H5N107 : EIS 계열사 GLS 주요매출 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_41H5S108 : EIS 계열사 GLS 주요매출 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_AC_27H6I062 : EIS 마감 년월이 존재 하지 않습니다.
    ///  TY_M_AC_27H6I063 : EIS 적용 완료상태 입니다. (처리 불가)
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
    public partial class TYAFMA014I : TYBase
    {
        private string fsCompanyCode = string.Empty;

        public TYAFMA014I()
        {
            InitializeComponent();
        }

        #region Description : Page_Load
        private void TYAFMA014I_Load(object sender, System.EventArgs e)
        {

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            switch (TYUserInfo.EmpNo.Substring(0, 2))
            {
                case "HT":
                    fsCompanyCode = "TH";
                    break;
                case "TG":
                    fsCompanyCode = "TG";
                    break;
                case "TS":
                    fsCompanyCode = "TS";
                    break;
                case "TL":
                    fsCompanyCode = "TL";
                    break;
                default:
                    fsCompanyCode = "";
                    break;
            }

            if (fsCompanyCode != "")
            {
                this.CBH01_ESPLCMPY.SetValue(fsCompanyCode);
                this.CBH01_ESPLCMPY.SetReadOnly(true);
            }

            if (fsCompanyCode != "")
            {
                this.SetStartingFocus(this.DTP01_GSTYYMM);
            }
            else
            {
                this.SetStartingFocus(this.CBH01_ESPLCMPY.CodeText);
            }

            UP_start_dsp_month();

            this.BTN61_INQ_Click(null, null);

            this.SetStartingFocus(this.DTP01_GSTYYMM);

            if (fsCompanyCode == "TH" || fsCompanyCode == "TG" || fsCompanyCode == "TL")
            {
                this.BTN61_SAV.Visible = false;
                this.BTN61_REM.Visible = false;
                this.BTN61_INQ.Visible = false;
            }
        } 
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_41H5S108.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_41H5N107", this.CBH01_ESPLCMPY.GetValue() , this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
            this.FPS91_TY_S_AC_41H5S108.SetValue(this.DbConnector.ExecuteDataTable());
        } 
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_41H5M105", dt);
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
                this.DbConnector.Attach("TY_P_AC_41H5M104", ds.Tables[0].Rows[i]["ESSUBGB"].ToString(),
                                                            ds.Tables[0].Rows[i]["ESSYYMM"].ToString(),
                                                            ds.Tables[0].Rows[i]["ESSPUMM"].ToString(),
                                                            ds.Tables[0].Rows[i]["ESSMAEAMT"].ToString()
                                                            );
            }
            //수정
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_41H5N106", ds.Tables[1].Rows[i]["ESSMAEAMT"].ToString(),
                                                            ds.Tables[1].Rows[i]["ESSUBGB"].ToString(),
                                                            ds.Tables[1].Rows[i]["ESSYYMM"].ToString(),
                                                            ds.Tables[1].Rows[i]["ESSPUMM"].ToString()
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
            // ------------------------   마감 완료 CHECK 시작  ------------------------------------------ //

            this.DbConnector.CommandClear(); // TY_P_AC_27H64059
            this.DbConnector.Attach("TY_P_AC_3C92V659", this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4), this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2));
            DataTable dt1 = this.DbConnector.ExecuteDataTable();

            if (dt1.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                e.Successed = false;
                return;
            }
            else
            {
                if (dt1.Rows[0]["ECSBBUN"].ToString() == "Z")
                {
                    this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                    e.Successed = false;
                    return;
                }
            }

            // ------------------------   마감 완료 CHECK 끝 ------------------------------------------ //

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_41H5S108.GetDataSourceInclude(TSpread.TActionType.New, "ESSUBGB", "ESSYYMM", "ESSPUMM", "ESSMAEAMT"));
            ds.Tables.Add(this.FPS91_TY_S_AC_41H5S108.GetDataSourceInclude(TSpread.TActionType.Update, "ESSUBGB", "ESSYYMM", "ESSPUMM", "ESSMAEAMT"));

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

            // ------------------------   마감 완료 CHECK 시작  ------------------------------------------ //

            this.DbConnector.CommandClear(); // TY_P_AC_27H64059
            this.DbConnector.Attach("TY_P_AC_3C92V659", this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4), this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2));
            DataTable dt1 = this.DbConnector.ExecuteDataTable();

            if (dt1.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                e.Successed = false;
                return;
            }
            else
            {
                if (dt1.Rows[0]["ECSBBUN"].ToString() == "Z")
                {
                    this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                    e.Successed = false;
                    return;
                }
            }

            // ------------------------   마감 완료 CHECK 끝 ------------------------------------------ //

            DataTable dt = this.FPS91_TY_S_AC_41H5S108.GetDataSourceInclude(TSpread.TActionType.Remove, "ESSUBGB", "ESSYYMM","ESSPUMM");

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


        #region Description : EIS 계열사 최종 마감 월 조회
        private void UP_start_dsp_month()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_3C94Q662");
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count == 0)
            {
                this.DTP01_GSTYYMM.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"));
            }
            else
            {
                this.DTP01_GSTYYMM.SetValue(dt.Rows[0]["YYMM"].ToString());
            }
        }
        #endregion

        #region Description : FPS91_TY_S_AC_41H5S108_RowInserted 이벤트
        private void FPS91_TY_S_AC_41H5S108_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_41H5S108.SetValue(e.RowIndex, "ESSUBGB", this.CBH01_ESPLCMPY.GetValue().ToString());
            this.FPS91_TY_S_AC_41H5S108.SetValue(e.RowIndex, "ESSYYMM", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));

        }
        #endregion
    }
}
