using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.AC00
{
    /// <summary>
    /// 품목코드관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.07.17 14:36
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_27H70069 : 품목코드관리 삭제
    ///  TY_P_AC_27H73066 : 품목코드관리 조회
    ///  TY_P_AC_27H74067 : 품목코드관리 등록
    ///  TY_P_AC_27H77068 : 품목코드관리 수정
    ///  TY_P_AC_27KAC142 : 오라클 품목코드 가져오기
    ///  TY_P_AC_27K20153 : 품목코드 존재 유무 CHECK
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_27H71070 : 품목코드관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_AC_26D6A858 : 데이터가 존재합니다.
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
    ///  GET : 값가져오기
    /// </summary>
    public partial class TYACPA005I : TYBase
    {
        #region Description : 페이지 로드
        public TYACPA005I()
        {
            InitializeComponent();
        }

        private void TYACPA005I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27H71070, "EITCODE");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_27H73066");

            this.FPS91_TY_S_AC_27H71070.SetValue(this.DbConnector.ExecuteDataTable());

            this.FPS91_TY_S_AC_27H71070.Focus();
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // 기존 DATASET에 신규필드(사번 필드) 추가
            this.DataTableColumnAdd(ds.Tables[0], "EIHISAB", TYUserInfo.EmpNo);
            this.DataTableColumnAdd(ds.Tables[1], "EIHISAB", TYUserInfo.EmpNo);

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_27H74067", ds.Tables[0]); // 저장
            this.DbConnector.Attach("TY_P_AC_27H77068", ds.Tables[1]); // 수정

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_27H70069", dt);

            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            //int i = 0;

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_27H71070.GetDataSourceInclude(TSpread.TActionType.New, "EITCODE", "EITNAME"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_27H71070.GetDataSourceInclude(TSpread.TActionType.Update, "EITCODE", "EITNAME"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
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
            DataTable dt = this.FPS91_TY_S_AC_27H71070.GetDataSourceInclude(TSpread.TActionType.Remove, "EITCODE");

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

        #region Description : 오라클 품목코드 가져오기
        private void BTN61_GET_Click(object sender, EventArgs e)
        {
            string sITEM_CODE = string.Empty;
            string sITEM_LNAME = string.Empty;
            string sITEM_SNAME = string.Empty;

            int icnt = 0;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_27KAC142");
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sITEM_CODE  = dt.Rows[i]["ITEM_CODE"].ToString();
                    sITEM_LNAME = dt.Rows[i]["ITEM_LNAME"].ToString();
                    sITEM_SNAME = dt.Rows[i]["ITEM_SNAME"].ToString();

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_27K20153", sITEM_CODE); // 체크
                    this.DbConnector.ExecuteNonQuery();
                    DataTable dt1 = this.DbConnector.ExecuteDataTable();

                    if (dt1.Rows.Count == 0)
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_27H74067", sITEM_CODE, sITEM_LNAME, TYUserInfo.EmpNo); // 저장
                        this.DbConnector.ExecuteNonQuery();

                        icnt = icnt + 1;
                    }


                }
            }


            string sOUTMSG = "신규 "+Convert.ToString(icnt) + " 건 추가 되었습니다.";
            this.ShowCustomMessage(sOUTMSG, "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

            this.BTN61_INQ_Click(null, null);
            
        } 
        #endregion
    }
}