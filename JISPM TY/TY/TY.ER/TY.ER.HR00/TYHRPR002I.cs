using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 승호관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2015.02.03 16:53
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_522EE251 : 승호파일 삭제
    ///  TY_P_HR_523BJ258 : 승호파일 수정
    ///  TY_P_HR_523C3260 : 승호파일 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_523DH264 : 승호관리
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
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
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  KBJKCD : 직급
    ///  KBSABUN : 사번
    ///  YYYYMM : 기준 년월
    /// </summary>
    public partial class TYHRPR002I : TYBase
    {
        #region Description : 폼 로드
        public TYHRPR002I()
        {
            InitializeComponent();
        }

        private void TYHRPR002I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_YYYYMM.SetValue(DateTime.Now.ToString("yyyyMM"));
            SetStartingFocus(DTP01_YYYYMM);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {   
            this.DbConnector.Attach
            (
            "TY_P_HR_523C3260",
            DTP01_YYYYMM.GetString().Substring(0, 6),
            CBH01_KBSABUN.GetValue().ToString(),
            CBH01_KBJKCD.GetValue().ToString()
            );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_HR_523DH264.SetValue(dt);

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_522EE251", dt);  //삭제
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 ProcessCheck
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {   
            DataTable dt = this.FPS91_TY_S_HR_523DH264.GetDataSourceInclude(TSpread.TActionType.Remove, "SNYYMM", "SNSABUN");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_524D2275", dt.Rows[i]["SNYYMM"].ToString(), dt.Rows[i]["SNSABUN"].ToString());
                    DataTable dt2 = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        if (dt2.Rows[0]["SNBALYY"].ToString() != "" || dt2.Rows[0]["SNBALSEQ"].ToString() != "0")
                        {
                            this.ShowCustomMessage("발령사항이 존재합니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            e.Successed = false;
                            return;
                        }
                    }
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

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sSNCHHOBN = string.Empty;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    sSNCHHOBN = ds.Tables[0].Rows[i]["SNHOBN"].ToString().Substring(0, 2) +
                                Set_Fill2(Convert.ToString(Convert.ToInt16(ds.Tables[0].Rows[i]["SNHOBN"].ToString().Substring(2, 2)) + 
                                                           Convert.ToInt16(ds.Tables[0].Rows[i]["SNGJGUBN"].ToString()) +
                                                           Convert.ToInt16(ds.Tables[0].Rows[i]["SNGTGUBN"].ToString())
                                                           ));

                    this.DbConnector.Attach("TY_P_HR_523BJ258", ds.Tables[0].Rows[i]["SNGJGUBN"].ToString(),
                                                                ds.Tables[0].Rows[i]["SNGTGUBN"].ToString(),
                                                                sSNCHHOBN,
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[0].Rows[i]["SNYYMM"].ToString(),
                                                                ds.Tables[0].Rows[i]["SNSABUN"].ToString()
                                                                );
                }
            }

            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD873");   
        }
        #endregion

        #region Description : 저장 ProcessCheck
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            ds.Tables.Add(this.FPS91_TY_S_HR_523DH264.GetDataSourceInclude(TSpread.TActionType.Update, "SNCOMPANY", "SNYYMM", "SNSABUN", "SNHOBN", "SNGJGUBN","SNGTGUBN"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }
            else
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_524D2275", ds.Tables[0].Rows[i]["SNYYMM"].ToString(), ds.Tables[0].Rows[i]["SNSABUN"].ToString());
                    dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["SNBALYY"].ToString() != "" || dt.Rows[0]["SNBALSEQ"].ToString() != "0")
                        {
                            this.ShowCustomMessage("발령사항이 존재합니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
    }
}
