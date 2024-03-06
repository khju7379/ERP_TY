using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using TY.ER.GB00;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 원천세 수정신고 가산세 관리 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2014.09.19 11:12
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_49JDV965 : 원천세 수정신고(가산세) 조회
    ///  TY_P_AC_49JDW966 : 원천세 수정신고(가산세) 수정
    ///  TY_P_AC_49JDX967 : 원천세 수정신고(가산세) 등록
    ///  TY_P_AC_49JDY968 : 원천세 수정신고(가산세) 삭제
    ///  TY_P_AC_4B3GF298 : 원천세 생성 체크
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_49JDZ969 : 원천세 수정신고(가산세) 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_AC_382BD291 : 금액을 입력하세요.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    ///  TY_M_GB_26I24916 : 일자를 확인하세요.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  WABRANCH : 지점구분
    ///  WREYYMM : 귀속년월
    /// </summary>
    public partial class TYACTP005S : TYBase
    {
        public TYACTP005S()
        {
            InitializeComponent();
            // 스프레드에서 코드헬프 사용
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_49JDZ969, "WASABUN", "KBHANGL", "WASABUN");
        }

        #region Description : 페이지 로드
        private void TYACTP005S_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_49JDZ969, "WABRANCH");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_49JDZ969, "WAGUNMU");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_49JDZ969, "WADYYMM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_49JDZ969, "WREYYMM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_49JDZ969, "WASABUN");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_WREYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            UP_Spread_Title();

            this.SetStartingFocus(this.DTP01_WREYYMM);

            this.FPS91_TY_S_AC_49JDZ969.Initialize();
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_49JDV965", "", this.DTP01_WREYYMM.GetValue().ToString());

            this.FPS91_TY_S_AC_49JDZ969.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion


        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            int i = 0;
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;
            this.DbConnector.CommandClear();

            for (i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_49JDY968", dt.Rows[i]["WABRANCH"].ToString(),
                                                            dt.Rows[i]["WAGUNMU"].ToString(),
                                                            dt.Rows[i]["WREYYMM"].ToString(),
                                                            dt.Rows[i]["WADYYMM"].ToString(),
                                                            "A90",
                                                            dt.Rows[i]["WASABUN"].ToString()
                                                            );
            }
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_49JDX967", ds.Tables[0].Rows[i]["WABRANCH"].ToString(),
                                                                ds.Tables[0].Rows[i]["WAGUNMU"].ToString(),
                                                                ds.Tables[0].Rows[i]["WREYYMM"].ToString(),
                                                                ds.Tables[0].Rows[i]["WADYYMM"].ToString(),
                                                                "A90",
                                                                ds.Tables[0].Rows[i]["WASABUN"].ToString(),
                                                                ds.Tables[0].Rows[i]["WREASON"].ToString(),
                                                                Get_Numeric(ds.Tables[0].Rows[i]["WADDIJAMT"].ToString()),
                                                                Get_Numeric(ds.Tables[0].Rows[i]["WRESITAMT"].ToString()),
                                                                Get_Numeric(ds.Tables[0].Rows[i]["WAINCOAMT"].ToString()),
                                                                Get_Numeric(ds.Tables[0].Rows[i]["WARESIAMT"].ToString())
                                                                );

                }
                this.DbConnector.ExecuteTranQueryList();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_49JDW966", ds.Tables[1].Rows[i]["WREASON"].ToString(),
                                                                Get_Numeric(ds.Tables[1].Rows[i]["WADDIJAMT"].ToString()),
                                                                Get_Numeric(ds.Tables[1].Rows[i]["WRESITAMT"].ToString()),
                                                                Get_Numeric(ds.Tables[1].Rows[i]["WAINCOAMT"].ToString()),
                                                                Get_Numeric(ds.Tables[1].Rows[i]["WARESIAMT"].ToString()),
                                                                ds.Tables[1].Rows[i]["WABRANCH"].ToString(),
                                                                ds.Tables[1].Rows[i]["WAGUNMU"].ToString(),
                                                                ds.Tables[1].Rows[i]["WREYYMM"].ToString(),
                                                                ds.Tables[1].Rows[i]["WADYYMM"].ToString(),
                                                                "A90",
                                                                ds.Tables[1].Rows[i]["WASABUN"].ToString()
                                                                );
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            this.ShowMessage("TY_M_GB_23NAD873");
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 자료 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            // 저장
            ds.Tables.Add(this.FPS91_TY_S_AC_49JDZ969.GetDataSourceInclude(TSpread.TActionType.New,    "WABRANCH", "WAGUNMU", "WREYYMM", "WADYYMM", "WASABUN", "WREASON", "WADDIJAMT", "WRESITAMT", "WAINCOAMT", "WARESIAMT"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_49JDZ969.GetDataSourceInclude(TSpread.TActionType.Update, "WABRANCH", "WAGUNMU", "WREYYMM", "WADYYMM", "WASABUN", "WREASON", "WADDIJAMT", "WRESITAMT", "WAINCOAMT", "WARESIAMT"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
                e.Successed = false;
                return;
            }

           
            // 저장 체크
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // 21.11.23일 추가
                ds.Tables[0].Rows[i]["WABRANCH"] = "1";

                // 생성 체크  WSUMMARYTF
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_4B3GF298", ds.Tables[0].Rows[i]["WABRANCH"].ToString(),ds.Tables[0].Rows[i]["WREYYMM"].ToString());

                if (this.DbConnector.ExecuteDataTable().Rows.Count != 0)
                {
                    this.ShowCustomMessage("신고서 생성이 완료 되었습니다(취소후 작업)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    this.SetFocus(this.DTP01_WREYYMM);
                    return;
                }
            }

            // 수정 체크
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                // 생성 체크  WSUMMARYTF
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_4B3GF298", ds.Tables[1].Rows[i]["WABRANCH"].ToString(), ds.Tables[1].Rows[i]["WREYYMM"].ToString());

                if (this.DbConnector.ExecuteDataTable().Rows.Count != 0)
                {
                    this.ShowCustomMessage("신고서 생성이 완료 되었습니다(취소후 작업)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    this.SetFocus(this.DTP01_WREYYMM);
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

        #region Description : 자료 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_49JDZ969.GetDataSourceInclude(TSpread.TActionType.Remove, "WABRANCH", "WAGUNMU", "WREYYMM", "WADYYMM", "WASABUN");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            // 삭제 체크
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // 생성 체크  WSUMMARYTF
                this.DbConnector.CommandClear();
                this.DbConnector.Attach ("TY_P_AC_4B3GF298",dt.Rows[i]["WABRANCH"].ToString(), dt.Rows[i]["WREYYMM"].ToString());

                if (this.DbConnector.ExecuteDataTable().Rows.Count != 0)
                {
                    this.ShowCustomMessage("신고서 생성이 완료 되었습니다(취소후 작업)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    this.SetFocus(this.DTP01_WREYYMM);
                    return;
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

        #region Description : 스프레드 타이틀
        private void UP_Spread_Title()
        {
            this.FPS91_TY_S_AC_49JDZ969_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_49JDZ969_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_49JDZ969_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_AC_49JDZ969_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_AC_49JDZ969_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);
            this.FPS91_TY_S_AC_49JDZ969_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);
            this.FPS91_TY_S_AC_49JDZ969_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1);
            this.FPS91_TY_S_AC_49JDZ969_Sheet1.AddColumnHeaderSpanCell(0, 5, 2, 1);
            this.FPS91_TY_S_AC_49JDZ969_Sheet1.AddColumnHeaderSpanCell(0, 6, 2, 1);

            this.FPS91_TY_S_AC_49JDZ969_Sheet1.AddColumnHeaderSpanCell(0, 7, 1, 2);
            this.FPS91_TY_S_AC_49JDZ969_Sheet1.AddColumnHeaderSpanCell(0, 9, 1, 2);

            this.FPS91_TY_S_AC_49JDZ969_Sheet1.ColumnHeader.Cells[0, 0].Value = "본지점구분";
            this.FPS91_TY_S_AC_49JDZ969_Sheet1.ColumnHeader.Cells[0, 1].Value = "지역구분";
            this.FPS91_TY_S_AC_49JDZ969_Sheet1.ColumnHeader.Cells[0, 2].Value = "귀속년월";
            this.FPS91_TY_S_AC_49JDZ969_Sheet1.ColumnHeader.Cells[0, 3].Value = "수정신고대상년월";
            this.FPS91_TY_S_AC_49JDZ969_Sheet1.ColumnHeader.Cells[0, 4].Value = "사번";
            this.FPS91_TY_S_AC_49JDZ969_Sheet1.ColumnHeader.Cells[0, 5].Value = "성명";
            this.FPS91_TY_S_AC_49JDZ969_Sheet1.ColumnHeader.Cells[0, 6].Value = "수정 사유";
            this.FPS91_TY_S_AC_49JDZ969_Sheet1.ColumnHeader.Cells[0, 7].Value = "추가 납부세액";
            this.FPS91_TY_S_AC_49JDZ969_Sheet1.ColumnHeader.Cells[0, 9].Value = "가산세";

            this.FPS91_TY_S_AC_49JDZ969_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_49JDZ969_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_49JDZ969_Sheet1.ColumnHeader.Cells[0, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_49JDZ969_Sheet1.ColumnHeader.Cells[0, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_49JDZ969_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_49JDZ969_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_49JDZ969_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_49JDZ969_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_49JDZ969_Sheet1.ColumnHeader.Cells[0, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            


            this.FPS91_TY_S_AC_49JDZ969_Sheet1.ColumnHeader.Cells[1, 7].Value = "소득세";
            this.FPS91_TY_S_AC_49JDZ969_Sheet1.ColumnHeader.Cells[1, 8].Value = "지방소득세";
            this.FPS91_TY_S_AC_49JDZ969_Sheet1.ColumnHeader.Cells[1, 9].Value = "소득세";
            this.FPS91_TY_S_AC_49JDZ969_Sheet1.ColumnHeader.Cells[1, 10].Value = "지방소득세";

            this.FPS91_TY_S_AC_49JDZ969_Sheet1.ColumnHeader.Cells[1, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_49JDZ969_Sheet1.ColumnHeader.Cells[1, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_49JDZ969_Sheet1.ColumnHeader.Cells[1, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_49JDZ969_Sheet1.ColumnHeader.Cells[1, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion
    }
}
