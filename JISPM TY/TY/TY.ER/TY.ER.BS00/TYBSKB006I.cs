using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.BS00
{
    /// <summary>
    /// 부서별 임직원 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.07.19 10:50
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_77KBY240 : 부서별 임직원 관리 등록
    ///  TY_P_AC_77KC0241 : 부서별 임직원 관리 수정
    ///  TY_P_AC_77KDF248 : 부서별 임직원 관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_77KDG249 : 부서별 임직원 관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  DEPTBTN : 부서관리
    ///  INQ : 조회
    ///  SAV : 저장
    ///  VSYEAR : 기준년도
    /// </summary>
    public partial class TYBSKB006I : TYBase
    {
        #region Description : 폼 로드
        public TYBSKB006I()
        {
            InitializeComponent();
        }

        private void TYBSKB006I_Load(object sender, System.EventArgs e)
        {
            // 등록 체크
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.TXT01_VSYEAR.SetValue(UP_Get_MaxYear());

            BTN61_INQ_Click(null, null);

            SetStartingFocus(this.TXT01_VSYEAR);
        }
        #endregion

        #region Description : 부서관리 버튼
        private void BTN61_DEPTBTN_Click(object sender, EventArgs e)
        {
            if ((new TYBSKB006P(
                     )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sPREYEAR = (Convert.ToInt32(this.TXT01_VSYEAR.GetValue().ToString()) - 1).ToString();

            this.FPS91_TY_S_AC_77KDG249.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_77KDF248", this.TXT01_VSYEAR.GetValue().ToString(),
                                                        sPREYEAR,
                                                        this.TXT01_VSYEAR.GetValue().ToString());
            this.FPS91_TY_S_AC_77KDG249.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_77KDG249.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_AC_77KDG249, "HRDPNAME", "합   계", SumRowType.Sum, "PREIWBMMAN", "PREIWEMPMAN", "PREIWMANSUM", "IWBMMAN", "IWEMPMAN", "IWMANSUM", "IWBMMANCHA", "IWEMPMANCHA", "IWMANSUMCHA");
            }


            UP_Spread_Load();
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            DataTable dt = new DataTable();

            try
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    // 등록 체크
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_AC_77KF3255", ds.Tables[0].Rows[i]["IWYEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["IWDPCODE"].ToString());
                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0) //수정
                    {
                        //전년도 수정
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_77KC0241", ds.Tables[0].Rows[i]["PREIWBMMAN"].ToString(),
                                                                    ds.Tables[0].Rows[i]["PREIWEMPMAN"].ToString(),
                                                                    ds.Tables[0].Rows[i]["IWBIGO"].ToString(),
                                                                    TYUserInfo.EmpNo,
                                                                    Convert.ToString(Convert.ToInt32(ds.Tables[0].Rows[i]["IWYEAR"].ToString()) - 1),
                                                                    ds.Tables[0].Rows[i]["IWDPCODE"].ToString()
                                                                    );
                        this.DbConnector.ExecuteNonQuery();


                        //예산년도 수정
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_77KC0241", ds.Tables[0].Rows[i]["IWBMMAN"].ToString(),
                                                                    ds.Tables[0].Rows[i]["IWEMPMAN"].ToString(),
                                                                    ds.Tables[0].Rows[i]["IWBIGO"].ToString(),
                                                                    TYUserInfo.EmpNo,
                                                                    ds.Tables[0].Rows[i]["IWYEAR"].ToString(),
                                                                    ds.Tables[0].Rows[i]["IWDPCODE"].ToString()
                                                                    );
                        this.DbConnector.ExecuteNonQuery();
                    }
                    else // 신규등록
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_77KBY240", ds.Tables[0].Rows[i]["IWYEAR"].ToString(),
                                                                    ds.Tables[0].Rows[i]["IWDPCODE"].ToString(),
                                                                    ds.Tables[0].Rows[i]["IWBMMAN"].ToString(),
                                                                    ds.Tables[0].Rows[i]["IWEMPMAN"].ToString(),
                                                                    ds.Tables[0].Rows[i]["IWBIGO"].ToString(),
                                                                    TYUserInfo.EmpNo
                                                                    );
                        this.DbConnector.ExecuteNonQuery();
                    }
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

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            DataSet ds = new DataSet();

            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_77KDG249.GetDataSourceInclude(TSpread.TActionType.Update, "IWYEAR", "IWDPCODE","PREIWBMMAN","PREIWEMPMAN", "IWBMMAN", "IWEMPMAN", "IWBIGO"));

            if (ds.Tables[0].Rows.Count == 0)
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

        #region Description : 스프레드 로드
        private void UP_Spread_Load()
        {
            this.FPS91_TY_S_AC_77KDG249_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_77KDG249_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_77KDG249_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_AC_77KDG249_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_AC_77KDG249_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);
            this.FPS91_TY_S_AC_77KDG249_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);
            this.FPS91_TY_S_AC_77KDG249_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1);

            this.FPS91_TY_S_AC_77KDG249_Sheet1.AddColumnHeaderSpanCell(0, 5, 1, 3);  // 전년도

            this.FPS91_TY_S_AC_77KDG249_Sheet1.AddColumnHeaderSpanCell(0, 8, 1, 3);  // 당해년도

            this.FPS91_TY_S_AC_77KDG249_Sheet1.AddColumnHeaderSpanCell(0, 11, 1, 3); // 증감

            this.FPS91_TY_S_AC_77KDG249_Sheet1.ColumnHeader.Cells[0, 0].Value = "년도";
            this.FPS91_TY_S_AC_77KDG249_Sheet1.ColumnHeader.Cells[0, 1].Value = "상위부서";
            this.FPS91_TY_S_AC_77KDG249_Sheet1.ColumnHeader.Cells[0, 2].Value = "구분";
            this.FPS91_TY_S_AC_77KDG249_Sheet1.ColumnHeader.Cells[0, 3].Value = "부서코드";
            this.FPS91_TY_S_AC_77KDG249_Sheet1.ColumnHeader.Cells[0, 4].Value = "부서";
            this.FPS91_TY_S_AC_77KDG249_Sheet1.ColumnHeader.Cells[0, 5].Value = (Convert.ToInt32(this.TXT01_VSYEAR.GetValue().ToString())-1).ToString() + "년";
            this.FPS91_TY_S_AC_77KDG249_Sheet1.ColumnHeader.Cells[0, 8].Value = this.TXT01_VSYEAR.GetValue().ToString() + "년";
            this.FPS91_TY_S_AC_77KDG249_Sheet1.ColumnHeader.Cells[0, 11].Value = "증(감)인원";
            this.FPS91_TY_S_AC_77KDG249_Sheet1.ColumnHeader.Cells[0, 14].Value = "비고";

            this.FPS91_TY_S_AC_77KDG249_Sheet1.ColumnHeader.Cells[1, 5].Value = "임원";
            this.FPS91_TY_S_AC_77KDG249_Sheet1.ColumnHeader.Cells[1, 6].Value = "직원";
            this.FPS91_TY_S_AC_77KDG249_Sheet1.ColumnHeader.Cells[1, 7].Value = "계";
            this.FPS91_TY_S_AC_77KDG249_Sheet1.ColumnHeader.Cells[1, 8].Value = "임원";
            this.FPS91_TY_S_AC_77KDG249_Sheet1.ColumnHeader.Cells[1, 9].Value = "직원";
            this.FPS91_TY_S_AC_77KDG249_Sheet1.ColumnHeader.Cells[1, 10].Value = "계";
            this.FPS91_TY_S_AC_77KDG249_Sheet1.ColumnHeader.Cells[1, 11].Value = "임원";
            this.FPS91_TY_S_AC_77KDG249_Sheet1.ColumnHeader.Cells[1, 12].Value = "직원";
            this.FPS91_TY_S_AC_77KDG249_Sheet1.ColumnHeader.Cells[1, 13].Value = "계";
            this.FPS91_TY_S_AC_77KDG249_Sheet1.ColumnHeader.Cells[1, 14].Value = "(증감원인기재)";

            this.FPS91_TY_S_AC_77KDG249_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_77KDG249_Sheet1.ColumnHeader.Cells[0, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_77KDG249_Sheet1.ColumnHeader.Cells[0, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_77KDG249_Sheet1.ColumnHeader.Cells[1, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_77KDG249_Sheet1.ColumnHeader.Cells[1, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_77KDG249_Sheet1.ColumnHeader.Cells[1, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_77KDG249_Sheet1.ColumnHeader.Cells[1, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_77KDG249_Sheet1.ColumnHeader.Cells[1, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_77KDG249_Sheet1.ColumnHeader.Cells[1, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_77KDG249_Sheet1.ColumnHeader.Cells[1, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_77KDG249_Sheet1.ColumnHeader.Cells[1, 12].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_77KDG249_Sheet1.ColumnHeader.Cells[1, 13].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion

        #region Descriptio : 복사 버튼
        private void BTN61_COPY_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYBSKB06C1()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 최근년도 가져오기
        private string UP_Get_MaxYear()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_8AME7004");
            string sMaxYear = this.DbConnector.ExecuteScalar().ToString();

            return sMaxYear;
        }
        #endregion
    }
}
