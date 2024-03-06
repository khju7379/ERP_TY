using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 급여프로시저관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.05.27 09:39
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_55R9P335 : 급여프로시저관리 등록
    ///  TY_P_HR_55R9Q336 : 급여프로시저관리 삭제
    ///  TY_P_HR_55RA2337 : 급여프로시저관리 조회
    ///  TY_P_HR_55RA6339 : 급여프로시저관리 수정
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_55RA3338 : 급여프로시저관리 조회
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
    ///  CLO : 닫기
    ///  REM : 삭제
    ///  SAV : 저장
    /// </summary>
    public partial class TYHRPY015I : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRPY015I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_55RA3338, "PRPROCNO", "PRPROCNAME", "PRPROCNO");
        }

        private void TYHRPY015I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_55RA3338, "PRSIDATE");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_55RA3338, "PRPROCNO");

            this.UP_DataBinding();
        }
        #endregion

        #region  Description : 자료 조회 
        private void UP_DataBinding()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_55RA2337");
            this.FPS91_TY_S_HR_55RA3338.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_55R9Q336", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.UP_DataBinding();

            this.ShowMessage("TY_M_GB_23NAD874");
        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            Int32 iCnt = 0;

            DataTable dt = this.FPS91_TY_S_HR_55RA3338.GetDataSourceInclude(TSpread.TActionType.Remove, "PRPROCNO", "PRSIDATE");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            //급여 체크
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //급여결과내역에 있으면 삭제 불가
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_55RBT342", dt.Rows[i]["PRSIDATE"].ToString());
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                if (iCnt > 0)
                {
                    this.ShowCustomMessage("급여결과내역에 급여코드가 존재합니다! 삭제할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DataTableColumnAdd(ds.Tables[0], "PRHISAB", TYUserInfo.EmpNo);
            this.DataTableColumnAdd(ds.Tables[1], "PRHISAB", TYUserInfo.EmpNo);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_55R9P335", ds.Tables[0].Rows[i]["PRSIDATE"].ToString().Replace("19000101", "").ToString(),
                                                                ds.Tables[0].Rows[i]["PRPROCNO"].ToString(),
                                                                ds.Tables[0].Rows[i]["PRPROCNAME"].ToString(),
                                                                ds.Tables[0].Rows[i]["PREIDATE"].ToString().Replace("19000101", "").ToString(),
                                                                ds.Tables[0].Rows[i]["PRMEMO"].ToString(),
                                                                ds.Tables[0].Rows[i]["PRHISAB"].ToString()
                                                                );
                }
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_55RA6339", ds.Tables[1].Rows[i]["PREIDATE"].ToString().Replace("19000101", "").ToString(),
                                                                ds.Tables[1].Rows[i]["PRMEMO"].ToString(),
                                                                ds.Tables[1].Rows[i]["PRHISAB"].ToString(),
                                                                ds.Tables[1].Rows[i]["PRSIDATE"].ToString().Replace("19000101", "").ToString(),
                                                                ds.Tables[1].Rows[i]["PRPROCNO"].ToString()
                                                                );
                }
            }
            this.DbConnector.ExecuteTranQueryList();

            this.UP_DataBinding();

            this.ShowMessage("TY_M_GB_23NAD873");

        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_HR_55RA3338.GetDataSourceInclude(TSpread.TActionType.New, "PRPROCNO", "PRPROCNAME", "PRSIDATE", "PREIDATE", "PRMEMO"));
            ds.Tables.Add(this.FPS91_TY_S_HR_55RA3338.GetDataSourceInclude(TSpread.TActionType.Update, "PRPROCNO", "PRPROCNAME", "PRSIDATE", "PREIDATE", "PRMEMO"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            if (ds.Tables[0].Rows.Count > 1)
            {
                this.ShowCustomMessage("신규 등록은 한개이상 할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_55RA2337");
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["PREIDATE"].ToString() == "")
                        {
                            this.ShowCustomMessage("종료되지 않은 프로시저가 있습니다. 종료후 등록하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            e.Successed = false;
                            return;
                        }
                    }
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

        #region  Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

    }
}
