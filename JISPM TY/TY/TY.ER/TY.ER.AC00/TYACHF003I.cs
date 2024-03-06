using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 품목 소분류코드관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.11.05 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_2B54K086 : 품목 존재유무 체크
    ///  TY_P_MR_2B54M087 : 품목 소분류코드 등록
    ///  TY_P_MR_2B54M088 : 품목 소분류코드 수정
    ///  TY_P_MR_2B54M089 : 품목 소분류코드 삭제
    ///  TY_P_MR_2B54Q090 : 품목 소분류코드 조회
    ///  TY_P_MR_2B54T092 : 품목 소분류코드 체크
    ///  TY_P_MR_2B24D041 : 품목 중분류조회(콤보박스)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2C475899 : 품목 소분류코드 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_AC_243AY315 : 작업이 불가합니다.
    ///  TY_M_AC_26D6A858 : 데이터가 존재합니다.
    ///  TY_M_MR_2B68U149 : 소분류코드 3자리를 입력해야 합니다.
    ///  TY_M_MR_2B559095 : 품목 대분류코드를 선택하세요.
    ///  TY_M_MR_2B550096 : 품목 중분류코드를 선택하세요.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_23S40973 : 동일한 코드가 존재합니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    ///  TY_M_MR_2B544082 : 품목코드가 존재합니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  SLMCODE : 대분류 코드
    ///  SMMCODE : 중분류 코드
    ///  SMDESC : 소분류명
    /// </summary>
    public partial class TYACHF003I : TYBase
    {
        private bool _Isloaded = false;
        private bool fbRowInsertChk = false;
        
        #region Description : 페이지 로드
        public TYACHF003I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_2C475899, "FXC3SAUP", "FXC3SAUPNM", "FXC3SAUP");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_2C475899, "FXC3BCODE", "FXC3BCODENM", "FXC3BCODE");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_2C475899, "FXC3MCODE", "FXC3MCODENM", "FXC3MCODE");
        }

        private void TYACHF003I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_2C475899, "FXC3SAUP");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_2C475899, "FXC3BCODE");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_2C475899, "FXC3MCODE");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_2C475899, "FXC3SCODE");
            
            // 등록 체크
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            // 삭제 체크
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.CBH01_FXC3SAUP_CodeBoxDataBinded(null, null);

            this.CBH01_FXC3BCODE_CodeBoxDataBinded(null, null);

            this.SetStartingFocus(this.CBH01_FXC3SAUP.CodeText);

            this._Isloaded = true;
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_2C475899.Initialize();
 
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_2C478894",
                this.CBH01_FXC3SAUP.GetValue(),
                this.CBH01_FXC3BCODE.GetValue(),
                this.CBH01_FXC3MCODE.GetValue(),
                this.TXT01_FXC3DESC.GetValue()
                );
            this.FPS91_TY_S_AC_2C475899.SetValue(this.DbConnector.ExecuteDataTable());            
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // 기존 DATASET에 신규필드(사번 필드) 추가
            this.DataTableColumnAdd(ds.Tables[0], "FXC3HISAB", TYUserInfo.EmpNo);
            this.DataTableColumnAdd(ds.Tables[1], "FXC3HISAB", TYUserInfo.EmpNo);

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_2C470895", ds.Tables[0]); //저장
            this.DbConnector.Attach("TY_P_AC_2C473896", ds.Tables[1]); //수정

            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD873"); // 저장 메세지

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2C473897", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sFilter = string.Empty;
            bool bChekisnum = false;
            double dCOUNT = 0;

            DataSet ds = new DataSet();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_2C475899.GetDataSourceInclude(TSpread.TActionType.New, "FXC3SAUP", "FXC3BCODE", "FXC3MCODE", "FXC3SCODE", "FXC3DESC", "FXC3BIGO"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_2C475899.GetDataSourceInclude(TSpread.TActionType.Update, "FXC3SAUP", "FXC3BCODE", "FXC3MCODE", "FXC3SCODE", "FXC3DESC", "FXC3BIGO"));

            //중분류 입력코드 숫자체크
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                bChekisnum = ChkIsNum(ds.Tables[0].Rows[i]["FXC3SCODE"].ToString());

                if (bChekisnum != true)
                {
                    this.ShowCustomMessage("소분류코드는 숫자만 가능합니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }

                // 대분류 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_AC_2C44G874",
                                       ds.Tables[0].Rows[i]["FXC3SAUP"].ToString(),
                                       ds.Tables[0].Rows[i]["FXC3BCODE"].ToString(),
                                       ""
                                       );
                if (this.DbConnector.ExecuteDataTable().Rows.Count <= 0)
                {
                    this.ShowCustomMessage("대분류코드를 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                } 

                //중분류 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_7AP9V892",
                    ds.Tables[0].Rows[i]["FXC3SAUP"].ToString(),
                    ds.Tables[0].Rows[i]["FXC3BCODE"].ToString(),
                    ds.Tables[0].Rows[i]["FXC3MCODE"].ToString()                    
                    );
                if (this.DbConnector.ExecuteDataTable().Rows.Count <= 0)
                {
                    this.ShowCustomMessage("중분류코드를 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }                 
            }

            //신규
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // 소분류유무 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_AC_2C47J901",
                                       ds.Tables[0].Rows[i]["FXC3SAUP"].ToString(),
                                       ds.Tables[0].Rows[i]["FXC3BCODE"].ToString(),
                                       ds.Tables[0].Rows[i]["FXC3MCODE"].ToString(),
                                       ds.Tables[0].Rows[i]["FXC3SCODE"].ToString()
                                       );
                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_GB_23S40973");
                    e.Successed = false;
                    return;
                }
                //else
                //{
                //    if (ds.Tables[0].Rows[i]["FXSMCODE"].ToString().Length != 3)
                //    {
                //        this.ShowMessage("TY_M_MR_2B68U149");
                //        e.Successed = false;
                //        return;
                //    }
                //}

                sFilter = "";
                sFilter = sFilter + " FXC3SAUP = '" + ds.Tables[0].Rows[i]["FXC3SAUP"].ToString() + "' AND ";
                sFilter = sFilter + " FXC3BCODE = '" + ds.Tables[0].Rows[i]["FXC3BCODE"].ToString() + "'  AND ";
                sFilter = sFilter + " FXC3MCODE  = '" + ds.Tables[0].Rows[i]["FXC3MCODE"].ToString() + "' AND ";
                sFilter = sFilter + " FXC3SCODE  = '" + ds.Tables[0].Rows[i]["FXC3SCODE"].ToString() + "'      ";

                dCOUNT = Convert.ToDouble(ds.Tables[0].Compute("COUNT(FXC3SCODE)", sFilter).ToString());

                if (dCOUNT > 1)
                {
                    this.ShowMessage("TY_M_GB_23S40973");
                    e.Successed = false;
                    return;
                }
            }

            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                if (ds.Tables[1].Rows[i]["FXSMCODE"].ToString().Length != 3)
                {
                    this.ShowMessage("TY_M_MR_2B68U149");
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

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_2C475899.GetDataSourceInclude(TSpread.TActionType.Remove, "FXC3SAUP", "FXC3BCODE", "FXC3MCODE", "FXC3SCODE");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // 품목코드 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_AC_2C47N902",
                                       dt.Rows[i]["FXC3SAUP"].ToString() + dt.Rows[i]["FXC3BCODE"].ToString() + dt.Rows[i]["FXC3MCODE"].ToString() + dt.Rows[i]["FXC3SCODE"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_2C47Q903");
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
        private void FPS91_TY_S_AC_2C475899_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            //try
            //{
            //    if (fbRowInsertChk == false)
            //    {
            //        this.FPS91_TY_S_AC_2C475899.SetValue(e.RowIndex, "FXSLASCODE", this.CBH01_FXC3SAUP.GetValue().ToString());
            //        this.FPS91_TY_S_AC_2C475899.SetValue(e.RowIndex, "FXSLASCODENM", this.CBH01_FXC3SAUP.GetText().ToString());
            //        this.FPS91_TY_S_AC_2C475899.SetValue(e.RowIndex, "FXSLMCODE", this.CBH01_FXC3BCODE.GetValue().ToString());
            //        this.FPS91_TY_S_AC_2C475899.SetValue(e.RowIndex, "FXSLMCODENM", this.CBH01_FXC3BCODE.GetText().ToString());
            //        this.FPS91_TY_S_AC_2C475899.SetValue(e.RowIndex, "FXSMMCODE", this.CBH01_FXC3MCODE.GetValue().ToString());
            //        this.FPS91_TY_S_AC_2C475899.SetValue(e.RowIndex, "FXSMMCODENM", this.CBH01_FXC3MCODE.GetText().ToString());
            //    }
            //    else
            //    {
            //        this.FPS91_TY_S_AC_2C475899.ActiveSheet.Rows.Remove(e.RowIndex, e.RowCount);
            //    }
            //}
            //finally
            //{
            //}

        }
        #endregion       

        #region Description : CBH01_FXC3SAUP_CodeBoxDataBinded 이벤트
        private void CBH01_FXC3SAUP_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH01_FXC3SAUP.GetValue().ToString();
            this.CBH01_FXC3BCODE.DummyValue = groupCode;

            this.CBH01_FXC3BCODE.SetReadOnly(string.IsNullOrEmpty(groupCode));

            if (this._Isloaded)
            {
                this.CBH01_FXC3BCODE.Initialize();
                this.CBH01_FXC3MCODE.Initialize();
            }
        }
        #endregion

        #region Description : CBH01_FXC3BCODE_CodeBoxDataBinded 이벤트
        private void CBH01_FXC3BCODE_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH01_FXC3SAUP.GetValue().ToString() + this.CBH01_FXC3BCODE.GetValue().ToString();
            this.CBH01_FXC3MCODE.SetReadOnly(string.IsNullOrEmpty(groupCode));

            if (groupCode.Length > 1)
            {
                groupCode = groupCode;
            }
            else
            {
                groupCode = "";
            }
            this.CBH01_FXC3MCODE.DummyValue = groupCode;
            this.CBH01_FXC3MCODE.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) this.CBH01_FXC3MCODE.Initialize();
        }
        #endregion

        #region Description : FPS91_TY_S_AC_2C475899_RowInserting 이벤트
        private void FPS91_TY_S_AC_2C475899_RowInserting(object sender, TSpread.TAlterEventRow e)
        {
            fbRowInsertChk = false;

            //if (this.CBH01_FXC3BCODE.GetValue().ToString() == "")
            //{
            //    fbRowInsertChk = true;

            //    this.ShowMessage("TY_M_MR_2B559095");

            //    this.SetFocus(this.CBH01_FXC3BCODE);

            //    return;
            //}

            //if (this.CBH01_FXC3MCODE.GetValue().ToString() == "")
            //{
            //    fbRowInsertChk = true;

            //    this.ShowMessage("TY_M_MR_2B550096");

            //    this.SetFocus(this.CBH01_FXC3MCODE);

            //    return;
            //}
        }
        #endregion

        #region Description : 자산분류 상세조회(소분류) -- FPS91_TY_S_AC_2C475899_CellDoubleClick
        private void FPS91_TY_S_AC_2C475899_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.FPS91_TY_S_AC_37A2F064.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_37A2E063",
                this.FPS91_TY_S_AC_2C475899.GetValue("FXC3SAUP").ToString() +
                this.FPS91_TY_S_AC_2C475899.GetValue("FXC3BCODE").ToString() +
                this.FPS91_TY_S_AC_2C475899.GetValue("FXC3MCODE").ToString() +
                this.FPS91_TY_S_AC_2C475899.GetValue("FXC3SCODE").ToString()

                );

            this.FPS91_TY_S_AC_37A2F064.SetValue(this.DbConnector.ExecuteDataTable());
        } 
        #endregion

        #region Description : FPS91_TY_S_AC_2C475899_LeaveCell
        private void FPS91_TY_S_AC_2C475899_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            string groupCode = string.Empty;

            groupCode = this.FPS91_TY_S_AC_2C475899.GetValue(e.Row, "FXC3SAUP").ToString();
            //대분류
            TYCodeBox tyCodeBox_FXC3BCODE = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_2C475899, "FXC3BCODE");
            if (tyCodeBox_FXC3BCODE != null)
                tyCodeBox_FXC3BCODE.DummyValue = groupCode;
            tyCodeBox_FXC3BCODE.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) tyCodeBox_FXC3BCODE.Initialize();

            //중분류
            groupCode = this.FPS91_TY_S_AC_2C475899.GetValue(e.Row, "FXC3SAUP").ToString() + this.FPS91_TY_S_AC_2C475899.GetValue(e.Row, "FXC3BCODE").ToString();
            TYCodeBox tyCodeBox_FXC3MCODE = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_2C475899, "FXC3MCODE");
            if (tyCodeBox_FXC3MCODE != null)
                tyCodeBox_FXC3MCODE.DummyValue = groupCode;
            tyCodeBox_FXC3MCODE.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) tyCodeBox_FXC3MCODE.Initialize();
        }
        #endregion

        #region Description : FPS91_TY_S_AC_2C475899_LeaveCell
        private void FPS91_TY_S_AC_2C475899_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1 )
            {
                if (this.FPS91_TY_S_AC_2C475899.ActiveSheet.ActiveColumnIndex == 2)
                {
                    if (FPS91_TY_S_AC_2C475899.GetValue(FPS91_TY_S_AC_2C475899.ActiveSheet.ActiveRowIndex, "FXC3SAUP").ToString() == "")
                    {
                        this.ShowCustomMessage("사업부코드를 먼저 선택하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                }
                if (this.FPS91_TY_S_AC_2C475899.ActiveSheet.ActiveColumnIndex == 4)
                {
                    if (FPS91_TY_S_AC_2C475899.GetValue(FPS91_TY_S_AC_2C475899.ActiveSheet.ActiveRowIndex, "FXC3SAUP").ToString() == "")
                    {
                        this.ShowCustomMessage("사업부코드를 먼저 선택하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                    if (FPS91_TY_S_AC_2C475899.GetValue(FPS91_TY_S_AC_2C475899.ActiveSheet.ActiveRowIndex, "FXC3BCODE").ToString() == "")
                    {
                        this.ShowCustomMessage("대분류코드를 먼저 선택하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                }
            }
        }
        #endregion


    }
}
