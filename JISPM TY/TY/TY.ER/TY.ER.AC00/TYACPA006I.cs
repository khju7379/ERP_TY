using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;

namespace TY.ER.AC00
{
    /// <summary>
    /// 재고자산 품목관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.07.18 11:50
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_27I26079 : 재고자산 품목관리 조회
    ///  TY_P_AC_27I28080 : 재고자산 품목관리 등록
    ///  TY_P_AC_27I2M082 : 재고자산 품목관리 수정
    ///  TY_P_AC_27I2M083 : 재고자산 품목관리 삭제
    ///  TY_P_AC_27I5A099 : 재고자산 체크
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_27I2N084 : 재고자산 품목관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_26D6A858 : 데이터가 존재합니다.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    ///  TY_M_AC_27I5N101 : 월은 2자리입니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  EITCDDP  : 부서코드
    ///  EITITCD  : 품목코드
    ///  EITPLCD  : 중분류
    ///  EITPMCD  : 대분류
    ///  EITMONTH : 월
    ///  EITYEAR  : 년
    /// </summary>
    public partial class TYACPA006I : TYBase
    {
        #region Description : 페이지 로드
        public TYACPA006I()
        {
            InitializeComponent();

            // 스프레드에서 코드헬프 사용
            // 부서코드
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_27I2N084, "EITCDDP", "DTDESC1", "EITCDDP");
            // 대분류
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_27I2N084, "EITPMCD", "EITPMNM", "EITPMCD");
            // 중분류
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_27I2N084, "EITPLCD", "EITPLNM", "EITPLCD");
            // 품목코드
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_27I2N084, "EITITCD", "EITITNM", "EITITCD");
        }

        private void TYACPA006I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27I2N084, "EITYEAR");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27I2N084, "EITMONTH");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27I2N084, "EITCDDP");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27I2N084, "EITPMCD");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27I2N084, "EITPLCD");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27I2N084, "EITITCD");

            this.CBH01_EITCDDP.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            SetStartingFocus(this.TXT01_EITYEAR);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sEITMONTH = string.Empty;

            sEITMONTH = Set_Fill2(this.TXT01_EITMONTH.GetValue().ToString());

            if (Set_Fill2(this.TXT01_EITMONTH.GetValue().ToString()) == "00")
            {
                sEITMONTH = "";
            }
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_27I26079",
                this.TXT01_EITYEAR.GetValue().ToString(),
                sEITMONTH.ToString(),
                this.CBH01_EITCDDP.GetValue().ToString()
                );

            this.FPS91_TY_S_AC_27I2N084.SetValue(this.DbConnector.ExecuteDataTable());

            this.FPS91_TY_S_AC_27I2N084.Focus();
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // 기존 DATASET에 신규필드(사번 필드) 추가
            this.DataTableColumnAdd(ds.Tables[0], "EITHISAB", TYUserInfo.EmpNo);
            this.DataTableColumnAdd(ds.Tables[1], "EITHISAB", TYUserInfo.EmpNo);

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_27I28080", ds.Tables[0]); // 저장
            this.DbConnector.Attach("TY_P_AC_27I2M082", ds.Tables[1]); // 수정

            this.DbConnector.ExecuteNonQueryList();

            // 저장 메세지
            this.ShowMessage("TY_M_GB_23NAD873");
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_27I2M083", dt);

            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            // CODE 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_27I2N084.GetDataSourceInclude(TSpread.TActionType.New, "EITYEAR", "EITMONTH", "EITCDDP", "EITPMCD", "EITPLCD", "EITITCD", "EITPMNM", "EITPLNM", "EITITNM"));
            // CODE 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_27I2N084.GetDataSourceInclude(TSpread.TActionType.Update, "EITYEAR", "EITMONTH", "EITCDDP", "EITPMCD", "EITPLCD", "EITITCD", "EITPMNM", "EITPLNM", "EITITNM"));

            //신규
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["EITMONTH"].ToString().Length < 2)
                {
                    this.ShowMessage("TY_M_AC_27I5N101");
                    e.Successed = false;
                    return;
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_AC_27I5A099",
                                       ds.Tables[0].Rows[i]["EITYEAR"].ToString(),
                                       ds.Tables[0].Rows[i]["EITMONTH"].ToString(),
                                       ds.Tables[0].Rows[i]["EITCDDP"].ToString(),
                                       ds.Tables[0].Rows[i]["EITPMCD"].ToString(),
                                       ds.Tables[0].Rows[i]["EITPLCD"].ToString(),
                                       ds.Tables[0].Rows[i]["EITITCD"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_26D6A858");
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
            DataTable dt = this.FPS91_TY_S_AC_27I2N084.GetDataSourceInclude(TSpread.TActionType.Remove, "EITYEAR", "EITMONTH", "EITCDDP", "EITPMCD", "EITPLCD", "EITITCD");

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

        #region Description : 년도 이벤트
        private void TXT01_EITYEAR_TextChanged(object sender, EventArgs e)
        {
            if (TXT01_EITYEAR.GetValue().ToString() != "")
            {
                this.CBH01_EITCDDP.DummyValue = TXT01_EITYEAR.GetValue() + "0101";
            }
            else
            {
                this.CBH01_EITCDDP.DummyValue = DateTime.Now.ToString("yyyyMMdd");
            }
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_AC_27I2N084_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            if (e.Column != 1)
                return;

            if (e.Column == 1)
            {
                // 부서명을 가져오기 위해서 스프레드의 예산년도에 파라미터 날짜를 넣음.
                string year = FPS91_TY_S_AC_27I2N084.GetValue(e.Row, "EITYEAR").ToString() + "0101";
                //((TCodeBoxCellType)FPS91_TY_S_AC_24917510.ActiveSheet.Columns["P1CDDP"].CellType).DummyValue = year;
                TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_27I2N084, "EITCDDP");
                if (tyCodeBox != null)
                    tyCodeBox.DummyValue = year;
            }
        }
        #endregion
    }
}