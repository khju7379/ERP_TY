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
    /// 탱크별화물관리(감사용) 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2018.11.07 11:37
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_8B7BK089 : 탱크별화물관리 조회(감사용)
    ///  TY_P_UT_8B7BL090 : 탱크별화물관리 등록(감사용)
    ///  TY_P_UT_8B7BL091 : 탱크별화물관리 수정(감사용)
    ///  TY_P_UT_8B7BM095 : 탱크별화물관리 삭제(감사용)
    ///  TY_P_UT_8B7BO096 : 탱크별화물관리 확인(감사용)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_8B7BP098 : 탱크별화물관리(감사용)
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
    ///  REM : 삭제
    ///  SAV : 저장
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    ///  CNTANKNO : 탱크번호
    /// </summary>
    public partial class TYUTPR029I : TYBase
    {
        #region Description : 폼 로드
        public TYUTPR029I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_8B7BP098, "CHGHWAMUL", "CHGHWAMULNM", "CHGHWAMUL");
        }

        private void TYUTPR029I_Load(object sender, System.EventArgs e)
        {
            //this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_8B7BP098, "CHGDATE");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_8B7BP098, "CHGTKNO");
            //this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_8B7BP098, "CHGHWAMUL");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            //this.DTP01_STDATE.SetValue(DateTime.Now.AddMonths(-6).ToString("yyyy-MM-dd"));
            //this.DTP01_EDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.TXT01_CNTANKNO);

            BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_8B7BP098.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_8B7BK089", this.TXT01_CNTANKNO.GetValue().ToString().Trim());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_8B7BP098.SetValue(dt);
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_8B7BM095", dt);
                this.DbConnector.ExecuteTranQuery();
            }

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_UT_8B7BP098.GetDataSourceInclude(TSpread.TActionType.Remove, "CHGTKNO");

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

            try
            {
                // 신규등록
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_UT_8B7BL090", ds.Tables[0].Rows[i]["CHGTKNO"].ToString(),
                                                                ds.Tables[0].Rows[i]["CHGHWAMUL"].ToString(),
                                                                TYUserInfo.EmpNo
                                                                );
                    this.DbConnector.ExecuteTranQuery();
                }

                // 수정
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {   

                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_UT_8B7BL091", ds.Tables[1].Rows[i]["CHGHWAMUL"].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["CHGTKNO"].ToString()
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

            ds.Tables.Add(this.FPS91_TY_S_UT_8B7BP098.GetDataSourceInclude(TSpread.TActionType.New, "CHGTKNO", "CHGHWAMUL"));

            ds.Tables.Add(this.FPS91_TY_S_UT_8B7BP098.GetDataSourceInclude(TSpread.TActionType.Update, "CHGTKNO", "CHGHWAMUL"));

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_8B7BO096", ds.Tables[0].Rows[i]["CHGTKNO"].ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowCustomMessage("[" + ds.Tables[0].Rows[i]["CHGTKNO"].ToString() + "] 이미 등록된 탱크번호입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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

        #region Description : 그리드 탱크 번호 체크
        private void FPS91_TY_S_UT_8B7BP098_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            string sTANKNO = this.FPS91_TY_S_UT_8B7BP098.GetValue("CHGTKNO").ToString();

            if (sTANKNO.Length > 3)
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_66SDH426", sTANKNO.Trim());

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowCustomMessage("탱크번호를 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.FPS91_TY_S_UT_8B7BP098.SetValue("CHGTKNO", "");
                }
            }
        }
        #endregion
    }
}
