using System;
using System.Data;
using System.Drawing;
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
    ///  TY_S_HR_59PES922 : 승호관리
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
    public partial class TYHRYB001I : TYBase
    {
        #region Description : 폼 로드
        public TYHRYB001I()
        {
            InitializeComponent();

            // 사번
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_59PES922, "YYSABUN", "KBHANGL", "YYSABUN");
        }

        private void TYHRYB001I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_59PES922, "YYSABUN");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_59PES922, "KBHANGL");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_YYDATE.SetValue(DateTime.Now.ToString("yyyyMM"));
            SetStartingFocus(this.DTP01_YYDATE);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.BTN61_SAV.Visible = true;
            this.BTN61_REM.Visible = true;

            DataTable dt = new DataTable();

            this.DbConnector.Attach
            (
            "TY_P_HR_59PEA919",
            this.DTP01_YYDATE.GetString().Substring(0, 6),
            this.CBH01_YYSABUN.GetValue().ToString()
            );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_HR_59PES922.SetValue(dt);

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
            else
            {
                // 급여 파일 존재 유무 체크
                this.DbConnector.Attach
                (
                "TY_P_HR_59UD6929",
                this.DTP01_YYDATE.GetString().Substring(0, 6),
                this.CBH01_YYSABUN.GetValue().ToString()
                );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_HR_59UD1930");

                    this.BTN61_SAV.Visible = false;
                    this.BTN61_REM.Visible = false;
                }
            }
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // 등록
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_59UDG931", this.DTP01_YYDATE.GetString().Substring(0, 6),
                                                                ds.Tables[0].Rows[i]["YYSABUN"].ToString(),
                                                                Get_Numeric(ds.Tables[0].Rows[i]["YYTOTIL"].ToString()),
                                                                Get_Numeric(ds.Tables[0].Rows[i]["YYTOTGJ"].ToString()),
                                                                Get_Numeric(ds.Tables[0].Rows[i]["YYGIGOJE"].ToString()),
                                                                Get_Numeric(ds.Tables[0].Rows[i]["YYTOTDD"].ToString()),
                                                                Get_Numeric(ds.Tables[0].Rows[i]["YYSUDANG"].ToString()),
                                                                Get_Numeric(ds.Tables[0].Rows[i]["YYGOYONG"].ToString()),
                                                                Get_Numeric(ds.Tables[0].Rows[i]["YYJIGUB"].ToString()),
                                                                Get_Numeric(ds.Tables[0].Rows[i]["YYGWTOTIL"].ToString()),
                                                                TYUserInfo.EmpNo
                                                                );
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            // 수정
            if (ds.Tables[1].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_59PED920", Get_Numeric(ds.Tables[1].Rows[i]["YYTOTIL"].ToString()),
                                                                Get_Numeric(ds.Tables[1].Rows[i]["YYTOTGJ"].ToString()),
                                                                Get_Numeric(ds.Tables[1].Rows[i]["YYGIGOJE"].ToString()),
                                                                Get_Numeric(ds.Tables[1].Rows[i]["YYTOTDD"].ToString()),
                                                                Get_Numeric(ds.Tables[1].Rows[i]["YYSUDANG"].ToString()),
                                                                Get_Numeric(ds.Tables[1].Rows[i]["YYGOYONG"].ToString()),
                                                                Get_Numeric(ds.Tables[1].Rows[i]["YYJIGUB"].ToString()),
                                                                Get_Numeric(ds.Tables[1].Rows[i]["YYGWTOTIL"].ToString()),
                                                                TYUserInfo.EmpNo,
                                                                this.DTP01_YYDATE.GetString().Substring(0, 6),
                                                                ds.Tables[1].Rows[i]["YYSABUN"].ToString()
                                                                );
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_59UDZ932", this.DTP01_YYDATE.GetString().Substring(0, 6),
                                                                ds.Tables[0].Rows[i]["YYSABUN"].ToString()
                                                                );
                }
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 저장 ProcessCheck
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_HR_59PES922.GetDataSourceInclude(TSpread.TActionType.New,    "YYSABUN", "YYTOTIL", "YYTOTGJ", "YYGIGOJE", "YYTOTDD", "YYSUDANG", "YYGOYONG", "YYJIGUB", "YYGWTOTIL"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_HR_59PES922.GetDataSourceInclude(TSpread.TActionType.Update, "YYSABUN", "YYTOTIL", "YYTOTGJ", "YYGIGOJE", "YYTOTDD", "YYSUDANG", "YYGOYONG", "YYJIGUB", "YYGWTOTIL"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }
            else
            {
                // 급여 파일 존재 유무 체크
                this.DbConnector.Attach
                (
                "TY_P_HR_59UD6929",
                this.DTP01_YYDATE.GetString().Substring(0, 6),
                this.CBH01_YYSABUN.GetValue().ToString()
                );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_HR_59UD1930");
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
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();

            ds.Tables.Add(this.FPS91_TY_S_HR_59PES922.GetDataSourceInclude(TSpread.TActionType.Remove, "YYSABUN"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }
            else
            {
                // 급여 파일 존재 유무 체크
                this.DbConnector.Attach
                (
                "TY_P_HR_59UD6929",
                this.DTP01_YYDATE.GetString().Substring(0, 6),
                this.CBH01_YYSABUN.GetValue().ToString()
                );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_HR_59UD1930");
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_HR_59PES922_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            if (e.Column == 2 || e.Column == 3 || e.Column == 4 || e.Column == 6 || e.Column == 8)
            {
                // 년간 년차 발생 개수
                double dYYTOTIL = double.Parse(Get_Numeric(this.FPS91_TY_S_HR_59PES922.GetValue(e.Row, "YYTOTIL").ToString()));
                // 년차 사용개수
                double dYYTOTGJ = double.Parse(Get_Numeric(this.FPS91_TY_S_HR_59PES922.GetValue(e.Row, "YYTOTGJ").ToString()));
                // 년차 공제 개수
                double dYYGIGOJE = double.Parse(Get_Numeric(this.FPS91_TY_S_HR_59PES922.GetValue(e.Row, "YYGIGOJE").ToString()));

                // 년차 개수(년차 수당 계산에 사용)
                double dNUNCHA = dYYTOTIL - dYYTOTGJ - dYYGIGOJE;
                // 일평균 급여
                double dYYTOTDD = double.Parse(Get_Numeric(this.FPS91_TY_S_HR_59PES922.GetValue(e.Row, "YYTOTDD").ToString()));

                dNUNCHA = dNUNCHA < 0 ? 0 : dNUNCHA;

                // 년차
                this.FPS91_TY_S_HR_59PES922.Sheets[0].SetValue(e.Row, 5, dNUNCHA);

                // GW 년차 총 개수
                this.FPS91_TY_S_HR_59PES922.Sheets[0].SetValue(e.Row, 10, dYYTOTIL);

                // 년차수당
                double dYYSUDANG = Math.Floor(Math.Round(dNUNCHA * dYYTOTDD, 2) / 10) * 10;

                dYYSUDANG = dYYSUDANG < 0 ? 0 : dYYSUDANG;

                this.FPS91_TY_S_HR_59PES922.Sheets[0].SetValue(e.Row, 7, dYYSUDANG);

                // 고용보험
                double dYYGOYONG = double.Parse(Get_Numeric(this.FPS91_TY_S_HR_59PES922.GetValue(e.Row, "YYGOYONG").ToString()));

                if (dYYGOYONG < 0)
                {
                    dYYGOYONG = 0;

                    this.FPS91_TY_S_HR_59PES922.Sheets[0].SetValue(e.Row, 8, dYYGOYONG);
                }

                // 지급금액
                double dYYJIGUB = dYYSUDANG - dYYGOYONG;
                this.FPS91_TY_S_HR_59PES922.Sheets[0].SetValue(e.Row, 9, dYYJIGUB);
            }
        }
        #endregion

        private void BTN61_CREATE_Click(object sender, EventArgs e)
        {
            TYHRYB001B popup = new TYHRYB001B();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (popup.fsYYDATE.ToString() != "")
                {
                    this.DTP01_YYDATE.SetValue(popup.fsYYDATE.ToString());
                    this.CBH01_YYSABUN.SetValue(popup.fsYYSABUN.ToString());

                    this.BTN61_INQ_Click(null, null);
                }
            }
        }

        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            (new TYHRYB001P(this.DTP01_YYDATE.GetValue().ToString(), this.CBH01_YYSABUN.GetValue().ToString())).ShowDialog();
        }
    }
}
