using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library; 
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.UT00
{
    /// <summary>
    /// 질소 단가 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2022.08.23 15:00
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_C8NBS929 : 질소 단가 관리 조회
    ///  TY_P_UT_C8NBX930 : 질소 단가 관리 등록
    ///  TY_P_UT_C8NBY931 : 질소 단가 관리 수정
    ///  TY_P_UT_C8NBZ932 : 질소 단가 관리 삭제
    ///  TY_P_UT_C8NDY935 : 질소 사용료 생성체크
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_C8NDY937 : 질소 단가 관리
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
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYUTIL021I : TYBase
    {
        #region Description : 폼 로드
        public TYUTIL021I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_C8NDY937, "JSDHWAJU", "JSDHWAJUNM", "JSDHWAJU");
        }

        private void TYUTIL021I_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_C8NDY937, "JSDYYMM", "JSDHWAJU");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.DTP01_STDATE.SetValue(System.DateTime.Now.AddYears(-1).ToString("yyyy-MM"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM"));

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_C8NDY937.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_C8NBS929",
                this.DTP01_STDATE.GetString().Substring(0, 6),
                this.DTP01_EDDATE.GetString().Substring(0, 6)
                );


            this.FPS91_TY_S_UT_C8NDY937.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sSEQ = string.Empty;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            DataTable dt = new DataTable();

            try
            {
                

                //신규등록
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    // 순번생성
                    sSEQ = UP_Get_JSDSEQ(ds.Tables[0].Rows[i]["JSDYYMM"].ToString().Substring(0, 6));

                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_UT_C8NBX930", ds.Tables[0].Rows[i]["JSDYYMM"].ToString().Substring(0, 6),
                                                                sSEQ,
                                                                ds.Tables[0].Rows[i]["JSDHWAJU"].ToString(),
                                                                ds.Tables[0].Rows[i]["JSDDANGA"].ToString(),
                                                                ds.Tables[0].Rows[i]["JSDHP"].ToString(),
                                                                TYUserInfo.EmpNo
                                                                );
                    this.DbConnector.ExecuteTranQuery();
                }

                //수정
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_UT_C8NBY931", ds.Tables[1].Rows[i]["JSDYYMM"].ToString(),
                                                                ds.Tables[1].Rows[i]["JSDSEQ"].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["JSDHWAJU"].ToString().Substring(0, 6),
                                                                ds.Tables[1].Rows[i]["JSDDANGA"].ToString(),
                                                                ds.Tables[1].Rows[i]["JSDHP"].ToString()
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

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            ds.Tables.Add(this.FPS91_TY_S_UT_C8NDY937.GetDataSourceInclude(TSpread.TActionType.New, "JSDYYMM", "JSDSEQ", "JSDHWAJU", "JSDDANGA", "JSDHP"));

            ds.Tables.Add(this.FPS91_TY_S_UT_C8NDY937.GetDataSourceInclude(TSpread.TActionType.Update, "JSDYYMM", "JSDSEQ", "JSDHWAJU", "JSDDANGA", "JSDHP"));

     
            // 저장 체크
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_C8NEM940", ds.Tables[0].Rows[i]["JSDYYMM"].ToString().Substring(0, 6),
                                                            ds.Tables[0].Rows[i]["JSDHWAJU"].ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowCustomMessage("이미 등록된 자료입니다.[" + ds.Tables[0].Rows[i]["JSDYYMM"].ToString() + "][" + ds.Tables[0].Rows[i]["JSDHWAJU"].ToString() + "]",
                                            "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }
            }

            // 수정 체크
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_C8NDY935", ds.Tables[1].Rows[i]["JSDYYMM"].ToString().Substring(0, 6),
                                                            ds.Tables[1].Rows[i]["JSDHWAJU"].ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowCustomMessage("질소 사용료 생성자료가 존재하여 수정이 불가합니다.[" + ds.Tables[0].Rows[i]["JSDYYMM"].ToString() + "][" + ds.Tables[0].Rows[i]["JSDHWAJU"].ToString() + "]",
                                            "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
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

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_C8NBZ932", dt);
                this.DbConnector.ExecuteNonQuery();

                this.BTN61_INQ_Click(null, null);
                this.ShowMessage("TY_M_GB_23NAD874");
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
            DataTable dt = this.FPS91_TY_S_UT_C8NDY937.GetDataSourceInclude(TSpread.TActionType.Remove, "JSDYYMM", "JSDSEQ", "JSDHWAJU");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_C8NDY935", dt.Rows[i]["JSDYYMM"].ToString().Substring(0, 6),
                                                            dt.Rows[i]["JSDHWAJU"].ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowCustomMessage("질소 사용료 생성자료가 존재하여 삭제가 불가합니다.[" + dt.Rows[i]["JSDYYMM"].ToString() + "][" + dt.Rows[i]["JSDHWAJU"].ToString() + "]",
                                            "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
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

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 순번 생성
        private string UP_Get_JSDSEQ(string JDSYYMM)
        {
            string sJSDSEQ = string.Empty;

            this.FPS91_TY_S_UT_C8NDY937.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_C8NFB944",JDSYYMM);

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sJSDSEQ = dt.Rows[0]["JSDSEQ"].ToString();
            }

            return sJSDSEQ;
        }

        #endregion
    }
}
