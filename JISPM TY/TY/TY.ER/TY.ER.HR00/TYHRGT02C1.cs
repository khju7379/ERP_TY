using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 교대조관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.12.09 14:08
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4C9D4665 : 교대조관리 조회
    ///  TY_P_HR_4C9D5666 : 교대조관리 등록
    ///  TY_P_HR_4C9FU669 : 교대조관리 수정
    ///  TY_P_HR_4C9FV670 : 교대조관리 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_4C9FW671 : 교대조관리
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
    public partial class TYHRGT02C1 : TYBase
    {
        #region Description : 페이지 로드
        public TYHRGT02C1()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_4C9FW671, "GJSABUN", "KBHANGL", "GJSABUN");
        }

        private void TYHRGT02C1_Load(object sender, System.EventArgs e)
        {
            
            // Key필드 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4C9FW671, "GJGUBUN", "GJSABUN");
            // 등록 체크
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            // 삭제 체크
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.FPS91_TY_S_HR_4C9FW671.Initialize();

            UP_Select();
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_HR_4C9D5666", ds.Tables[0].Rows[i]["GJGUBUN"],
                                                            ds.Tables[0].Rows[i]["GJSABUN"],
                                                            ds.Tables[0].Rows[i]["GJBIGO"],
                                                            TYUserInfo.EmpNo
                                                            ); //신규

            }
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_HR_4C9FU669", ds.Tables[1].Rows[i]["GJBIGO"],
                                                            TYUserInfo.EmpNo,
                                                            ds.Tables[1].Rows[i]["GJGUBUN"],
                                                            ds.Tables[1].Rows[i]["GJSABUN"]); //수정
            }

            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD873"); // 저장 메세지

            this.UP_Select();
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            DataSet ds = new DataSet();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_HR_4C9FW671.GetDataSourceInclude(TSpread.TActionType.New, "GJGUBUN", "GJSABUN", "GJBIGO"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_HR_4C9FW671.GetDataSourceInclude(TSpread.TActionType.Update, "GJGUBUN", "GJSABUN", "GJBIGO"));

            //신규
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_HR_4C9D4665",
                                       //ds.Tables[0].Rows[i]["GJGUBUN"].ToString(),
                                       "",
                                       ds.Tables[0].Rows[i]["GJSABUN"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_3219C986");
                    e.Successed = false;
                    return;
                }

                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    if (i != j)
                    {
                        //if (ds.Tables[0].Rows[i]["GJGUBUN"].ToString() == ds.Tables[0].Rows[j]["GJGUBUN"].ToString() && 
                        if (ds.Tables[0].Rows[i]["GJSABUN"].ToString() == ds.Tables[0].Rows[j]["GJSABUN"].ToString())
                        {
                            this.ShowMessage("TY_M_AC_3219C986");
                            e.Successed = false;
                            return;
                        }
                    }
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

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_HR_4C9FV670", dt.Rows[i]["GJGUBUN"],
                                                            dt.Rows[i]["GJSABUN"]);
            }
            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지

            UP_Select();
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_4C9FW671.GetDataSourceInclude(TSpread.TActionType.Remove, "GJGUBUN", "GJSABUN");

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

        #region Description : 데이터 조회
        private void UP_Select()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4C9D4665","","");
            DataTable dt = this.DbConnector.ExecuteDataTable();

            FPS91_TY_S_HR_4C9FW671.SetValue(dt);
        }
        #endregion
    }
}
