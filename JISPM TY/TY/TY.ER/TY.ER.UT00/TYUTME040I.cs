using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.ER.GB00;

namespace TY.ER.UT00
{
    /// <summary>
    /// 선급금 위임요율 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2020.03.04 16:20
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_A34GE006 : 선급금 위임요율 조회
    ///  TY_P_UT_A34GG007 : 선급금 위임요율 등록
    ///  TY_P_UT_A34GH008 : 선급금 위임요율 수정
    ///  TY_P_UT_A34GJ009 : 선급금 위임요율 삭제
    ///  TY_P_UT_A34GN012 : 선급금 위임요율 확인
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_A34GJ011 : 선급금 위임요율 관리
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
    /// </summary>
    public partial class TYUTME040I : TYBase
    {
        #region Description : 폼 로드
        public TYUTME040I()
        {
            InitializeComponent();

            // 거래처
            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_A34GJ011, "MDVNCODE", "MDVNCODENM", "MDVNCODE");
        }

        private void TYUTME040I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_A34GJ011, "MDDATE");

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_A34GJ011.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_A34GE006");

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_A34GJ011.SetValue(dt);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_A34GG007", ds.Tables[0].Rows[i]["MDDATE"].ToString(),
                                                                ds.Tables[0].Rows[i]["MDVNCODE"].ToString(),
                                                                ds.Tables[0].Rows[i]["MDYOYUL"].ToString(),
                                                                TYUserInfo.EmpNo
                                                                ); // 저장

                    this.DbConnector.ExecuteNonQuery();
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();

                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        this.DbConnector.Attach("TY_P_UT_A34GH008", ds.Tables[1].Rows[i]["MDVNCODE"].ToString(),
                                                                    ds.Tables[1].Rows[i]["MDYOYUL"].ToString(),
                                                                    TYUserInfo.EmpNo,
                                                                    ds.Tables[1].Rows[i]["MDDATE"].ToString()
                                                                    ); // 수정
                    }

                    this.DbConnector.ExecuteTranQueryList();
                }

                this.BTN61_INQ_Click(null, null);
                this.ShowMessage("TY_M_GB_23NAD873"); // 저장 메세지
            }
            catch
            {
                this.ShowMessage("TY_M_AC_246A2488");
            }
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_A34GJ009", dt);
                this.DbConnector.ExecuteTranQueryList();

                this.BTN61_INQ_Click(null, null);
                this.ShowMessage("TY_M_GB_23NAD874");
            }
            catch
            {
                this.ShowMessage("TY_M_GB_43C9G671");
            }
        }
        #endregion

        #region Description : 저장 ProcessCheck
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_UT_A34GJ011.GetDataSourceInclude(TSpread.TActionType.New, "MDDATE", "MDVNCODE", "MDYOYUL"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_UT_A34GJ011.GetDataSourceInclude(TSpread.TActionType.Update, "MDDATE", "MDVNCODE", "MDYOYUL"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            // 중복 체크
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["MDDATE"].ToString().Length < 8)
                {
                    this.ShowCustomMessage("기준일자를 확인하세요. [" + ds.Tables[0].Rows[i]["MDDATE"].ToString() + "]", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_A34GN012", ds.Tables[0].Rows[i]["MDDATE"].ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowCustomMessage("동일 자료가 등록되어 있습니다. [" + ds.Tables[0].Rows[i]["MDDATE"].ToString() + "]", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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

        #region Description : 삭제 ProcessCheck
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_UT_A34GJ011.GetDataSourceInclude(TSpread.TActionType.Remove, "MDDATE");

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
    }
}
