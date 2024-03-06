using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 품목 중분류코드관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.11.02 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_2AV6W969 : 품목 중분류코드 조회
    ///  TY_P_MR_2B24W046 : 품목 중분류코드 등록
    ///  TY_P_MR_2B24W047 : 품목 중분류코드 수정
    ///  TY_P_MR_2B24X048 : 품목 중분류코드 삭제
    ///  TY_P_MR_2B24Y049 : 품목 중분류코드 체크
    ///  TY_P_MR_2B543080 : 품목 소분류코드 존재 체크
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2C44N879 : 품목 중분류코드 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_AC_243AY315 : 작업이 불가합니다.
    ///  TY_M_AC_26D6A858 : 데이터가 존재합니다.
    ///  TY_M_MR_2B68S148 : 중분류코드 3자리를 입력해야 합니다.
    ///  TY_M_MR_2B543081 : 소분류코드가 존재합니다.
    ///  TY_M_MR_2B559095 : 품목 대분류코드를 선택하세요.
    ///  TY_M_GB_23S40973 : 동일한 코드가 존재합니다.
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
    ///  MLMCODE : 대분류 코드
    ///  MMDESC : 중분류명
    /// </summary>
    public partial class TYACHF002I : TYBase
    {
        private bool _Isloaded = false; 

        #region Description : 페이지 로드
        public TYACHF002I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_2C44N879, "FXC2SAUP", "FXC2SAUPNM", "FXC2SAUP");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_2C44N879, "FXC2BCODE", "FXC2BCODENM", "FXC2BCODE");
        }

        private void TYACHF002I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_2C44N879, "FXC2SAUP");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_2C44N879, "FXC2BCODE");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_2C44N879, "FXC2MCODE");

            // 등록 체크
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            // 삭제 체크
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.CBH01_FXC2SAUP_CodeBoxDataBinded(null, null); 

            this.SetStartingFocus(this.CBH01_FXC2SAUP.CodeText);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_2C44N879.Initialize(); 

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_2C44G874",
                this.CBH01_FXC2SAUP.GetValue(),
                this.CBH01_FXC2BCODE.GetValue(),
                this.TXT01_FXC2DESC.GetValue()
                );
            this.FPS91_TY_S_AC_2C44N879.SetValue(this.DbConnector.ExecuteDataTable());            
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // 기존 DATASET에 신규필드(사번 필드) 추가
            this.DataTableColumnAdd(ds.Tables[0], "FXC2HISAB", TYUserInfo.EmpNo);
            this.DataTableColumnAdd(ds.Tables[1], "FXC2HISAB", TYUserInfo.EmpNo);

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_2C44I875", ds.Tables[0]); //저장
            this.DbConnector.Attach("TY_P_AC_2C44J876", ds.Tables[1]); //수정

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873"); // 저장 메세지

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2C44J877", dt);
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sFilter = "";
            bool bChekisnum = false;

            DataRow[] dr;

            DataSet ds = new DataSet();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_2C44N879.GetDataSourceInclude(TSpread.TActionType.New, "FXC2SAUP", "FXC2BCODE", "FXC2MCODE", "FXC2DESC", "FXC2BIGO"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_2C44N879.GetDataSourceInclude(TSpread.TActionType.Update, "FXC2SAUP", "FXC2BCODE", "FXC2MCODE", "FXC2DESC", "FXC2BIGO"));

            //중분류 입력코드 숫자체크
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                bChekisnum = ChkIsNum(ds.Tables[0].Rows[i]["FXC2MCODE"].ToString());

                if (bChekisnum != true)
                {
                    this.ShowCustomMessage("중분류코드는 숫자만 가능합니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return; 
                }
            }

            //신규
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_AC_2C44Z883",
                                       ds.Tables[0].Rows[i]["FXC2SAUP"].ToString(),
                                       ds.Tables[0].Rows[i]["FXC2BCODE"].ToString(),
                                       ds.Tables[0].Rows[i]["FXC2MCODE"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_GB_23S40973");
                    e.Successed = false;
                    return;
                }
                //else
                //{
                //    if (ds.Tables[0].Rows[i]["FXMMCODE"].ToString().Length != 3)
                //    {
                //        this.ShowMessage("TY_M_MR_2B68S148");
                //        e.Successed = false;
                //        return;
                //    }
                //}

                sFilter = " FXC2SAUP = '" + ds.Tables[0].Rows[i]["FXC2SAUP"].ToString() + "'";
                sFilter += " AND FXC2BCODE = '" + ds.Tables[0].Rows[i]["FXC2BCODE"].ToString() + "'";
                sFilter += " AND FXC2MCODE = '" + ds.Tables[0].Rows[i]["FXC2MCODE"].ToString() + "'";

                dr = ds.Tables[0].Select(sFilter);

                if (dr.Length > 1)
                {
                    this.ShowMessage("TY_M_GB_23S40973");
                    e.Successed = false;
                    return;
                }
            }

            //for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            //{
            //    if (ds.Tables[1].Rows[i]["FXMMCODE"].ToString().Length != 3)
            //    {
            //        this.ShowMessage("TY_M_MR_2B68S148");
            //        e.Successed = false;
            //        return;
            //    }
            //}

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
            DataTable dt = this.FPS91_TY_S_AC_2C44N879.GetDataSourceInclude(TSpread.TActionType.Remove, "FXC2SAUP", "FXC2BCODE", "FXC2MCODE");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_AC_2C454884",
                                       dt.Rows[i]["FXC2SAUP"].ToString(),
                                       dt.Rows[i]["FXC2BCODE"].ToString(),
                                       dt.Rows[i]["FXC2MCODE"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_MR_2B543081");
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

        #region Description : 스프레드 추가 이벤트
        private void FPS91_TY_S_AC_2C44N879_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            if (this.CBH01_FXC2SAUP.GetValue().ToString() == "" || this.CBH01_FXC2BCODE.GetValue().ToString() == "")
            {
                //this.FPS91_TY_S_AC_2C44N879.ActiveSheet.Rows.Remove(e.RowIndex, 1);                
                //this.ShowMessage("TY_M_MR_2B559095");
                return;
            }
            else
            {
                this.FPS91_TY_S_AC_2C44N879.SetValue(e.RowIndex, "FXC2SAUP", this.CBH01_FXC2SAUP.GetValue().ToString());
                this.FPS91_TY_S_AC_2C44N879.SetValue(e.RowIndex, "FXC2SAUPNM", this.CBH01_FXC2SAUP.GetText().ToString());
                this.FPS91_TY_S_AC_2C44N879.SetValue(e.RowIndex, "FXC2BCODE", this.CBH01_FXC2BCODE.GetValue().ToString());
                this.FPS91_TY_S_AC_2C44N879.SetValue(e.RowIndex, "FXC2BCODENM", this.CBH01_FXC2BCODE.GetText().ToString());
            }
        }
        #endregion       

        #region Description : 중분류 상세조회(중분류)
        private void FPS91_TY_S_AC_2C44N879_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_37A91049",
                this.FPS91_TY_S_AC_2C44N879.GetValue("FXC2SAUP").ToString(),
                this.FPS91_TY_S_AC_2C44N879.GetValue("FXC2BCODE").ToString(),
                this.FPS91_TY_S_AC_2C44N879.GetValue("FXC2MCODE").ToString()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_37A92050.SetValue(dt);
        }
        #endregion

        #region Description : CBH01_FXC2SAUP_CodeBoxDataBinded
        private void CBH01_FXC2SAUP_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH01_FXC2SAUP.GetValue().ToString();
            this.CBH01_FXC2BCODE.DummyValue = groupCode;
            this.CBH01_FXC2BCODE.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) this.CBH01_FXC2BCODE.Initialize();
        }
        #endregion

        #region Description : CBH01_FXC2SAUP_CodeBoxDataBinded
        private void FPS91_TY_S_AC_2C44N879_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            string groupCode = this.FPS91_TY_S_AC_2C44N879.GetValue(e.Row, "FXC2SAUP").ToString();
            TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_2C44N879, "FXC2BCODE");
            if (tyCodeBox != null)
                tyCodeBox.DummyValue = groupCode;
            tyCodeBox.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) tyCodeBox.Initialize();
        }
        #endregion
    }
}