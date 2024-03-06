using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 퇴직금 등록 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.11.04 17:42
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_4B4FV306 : 퇴직금 조회
    ///  TY_P_AC_4B4G3311 : 퇴직금 등록
    ///  TY_P_AC_4B4G4312 : 퇴직금 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_4B4FV308 : 퇴직금 등록
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
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
    ///  PSDEPT : 부서
    ///  PSSABUN : 사번
    ///  PSAREA : 지역구분
    ///  PSGBGIVE : 급여지급여부
    ///  PSGUBN : 퇴직구분
    ///  PSPAYDATE : 지급일자
    ///  PSYDATE : 등록일자
    /// </summary>
    public partial class TYACTP006I : TYBase
    {
        #region Description : 페이지 로드
        public TYACTP006I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_4B4FV308, "PSSABUN", "PSSABUNNM", "PSSABUN");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_4B4FV308, "PSDEPT", "PSDEPTNM", "PSDEPT");
        }

        private void TYACTP006I_Load(object sender, System.EventArgs e)
        {
            // Key필드 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_4B4FV308, "PSSABUN", "PSSABUNNM", "PSYDATE", "PSGUBN");
            // 등록 체크
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            // 삭제 체크
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.FPS91_TY_S_AC_4B4FV308.Initialize();
            this.DTP01_PSYDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));
            this.DTP02_PSYDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_4B4FV306",
                this.DTP01_PSYDATE.GetValue().ToString(),
                this.DTP02_PSYDATE.GetValue().ToString(),
                this.CBH01_PSSABUN.GetValue().ToString(),
                this.CBO01_PSGUBN.GetValue().ToString()
                );

            this.FPS91_TY_S_AC_4B4FV308.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_4B4G3311", ds.Tables[0].Rows[i]["PSSABUN"],    //사번
                                                            ds.Tables[0].Rows[i]["PSYDATE"],    //등록일자
                                                            ds.Tables[0].Rows[i]["PSGUBN"],     //퇴직구분
                                                            ds.Tables[0].Rows[i]["PSPAYDATE"],  //지급일자
                                                            ds.Tables[0].Rows[i]["PSDEPT"],     //부서
                                                            "",
                                                            "",
                                                            "",
                                                            "",
                                                            "",
                                                            "",
                                                            "",
                                                            "",
                                                            "",
                                                            ds.Tables[0].Rows[i]["PSSERVPAYAMT"], //퇴직금총액
                                                            "",
                                                            ds.Tables[0].Rows[i]["PSINCOMTAX"], //소득세
                                                            ds.Tables[0].Rows[i]["PSJUMINTAX"], //주민세
                                                            ds.Tables[0].Rows[i]["PSCHAINAMT"], //차인지급액
                                                            ds.Tables[0].Rows[i]["PSAREA"],     //지역구분
                                                            ds.Tables[0].Rows[i]["PSGBGIVE"],   //급여지급구분
                                                            "",
                                                            TYUserInfo.EmpNo.ToString()
                                                            ); //저장
            }
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_4B5BX319", ds.Tables[1].Rows[i]["PSPAYDATE"],  //지급일자
                                                            ds.Tables[1].Rows[i]["PSDEPT"],     //부서
                                                            "",
                                                            "",
                                                            "",
                                                            "",
                                                            "",
                                                            "",
                                                            "",
                                                            "",
                                                            "",
                                                            ds.Tables[1].Rows[i]["PSSERVPAYAMT"], //퇴직금총액
                                                            "",
                                                            ds.Tables[1].Rows[i]["PSINCOMTAX"], //소득세
                                                            ds.Tables[1].Rows[i]["PSJUMINTAX"], //주민세
                                                            ds.Tables[1].Rows[i]["PSCHAINAMT"], //차인지급액
                                                            ds.Tables[1].Rows[i]["PSAREA"],     //지역구분
                                                            ds.Tables[1].Rows[i]["PSGBGIVE"],   //급여지급구분
                                                            "",
                                                            TYUserInfo.EmpNo.ToString(),
                                                            ds.Tables[1].Rows[i]["PSSABUN"],    //사번
                                                            ds.Tables[1].Rows[i]["PSYDATE"],    //등록일자
                                                            ds.Tables[1].Rows[i]["PSGUBN"]     //퇴직구분
                                                            ); //수정
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873"); // 저장 메세지

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_4B4G4312", dt.Rows[i]["PSYDATE"],
                                                            dt.Rows[i]["PSSABUN"],
                                                            dt.Rows[i]["PSGUBN"]);
            }
            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            DataSet ds = new DataSet();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_4B4FV308.GetDataSourceInclude(TSpread.TActionType.New, "PSYDATE", "PSSABUN", "PSGUBN", "PSPAYDATE", "PSDEPT", "PSSERVPAYAMT", "PSINCOMTAX", "PSJUMINTAX", "PSCHAINAMT", "PSAREA", "PSGBGIVE"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_4B4FV308.GetDataSourceInclude(TSpread.TActionType.Update, "PSYDATE", "PSSABUN", "PSGUBN", "PSPAYDATE", "PSDEPT", "PSSERVPAYAMT", "PSINCOMTAX", "PSJUMINTAX", "PSCHAINAMT", "PSAREA", "PSGBGIVE"));

            //신규
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_AC_4B5G4320",
                                       ds.Tables[0].Rows[i]["PSYDATE"].ToString(),
                                       ds.Tables[0].Rows[i]["PSSABUN"].ToString(),
                                       ds.Tables[0].Rows[i]["PSGUBN"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_3219C986");
                    e.Successed = false;
                    return;
                }
            }

            // 신규등록
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // 생성 체크  WSUMMARYTF
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_4B3GF298", ds.Tables[0].Rows[i]["PSAREA"].ToString(), ds.Tables[0].Rows[i]["PSYDATE"].ToString().Substring(0, 6));

                if (this.DbConnector.ExecuteDataTable().Rows.Count != 0)
                {
                    this.ShowCustomMessage("신고서 생성이 완료 되었습니다(취소후 작업)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            // 수정
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                // 생성 체크  WSUMMARYTF
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_4B3GF298", ds.Tables[1].Rows[i]["PSAREA"].ToString(), ds.Tables[1].Rows[i]["PSYDATE"].ToString().Substring(0, 6));

                if (this.DbConnector.ExecuteDataTable().Rows.Count != 0)
                {
                    this.ShowCustomMessage("신고서 생성이 완료 되었습니다(취소후 작업)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
            DataTable dt = this.FPS91_TY_S_AC_4B4FV308.GetDataSourceInclude(TSpread.TActionType.Remove, "PSYDATE", "PSSABUN", "PSGUBN" , "PSAREA");

            // 삭제 체크
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // 생성 체크  WSUMMARYTF
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_4B3GF298", dt.Rows[i]["PSAREA"].ToString(), dt.Rows[i]["PSYDATE"].ToString().Substring(0, 6));

                if (this.DbConnector.ExecuteDataTable().Rows.Count != 0)
                {
                    this.ShowCustomMessage("신고서 생성이 완료 되었습니다(취소후 작업)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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

        #region Descripgion : 스프레드 선택 이벤트
        private void FPS91_TY_S_AC_4B4FV308_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            if (e.Column == 3)
            {
                // 부서명을 가져오기 위해서 스프레드의 예산년도에 파라미터 날짜를 넣음.
                string year = FPS91_TY_S_AC_4B4FV308.GetValue(e.Row, "PSYDATE").ToString();
                //((TCodeBoxCellType)FPS91_TY_S_AC_24917510.ActiveSheet.Columns["P1CDDP"].CellType).DummyValue = year;
                TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_4B4FV308, "PSDEPT");
                if (tyCodeBox != null)
                    tyCodeBox.DummyValue = year;
            }
        }
        #endregion

        #region Descripgion : 스프레드 퇴직구분 선택 이벤트
        private void FPS91_TY_S_AC_4B4FV308_ComboSelChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column == 3)
            {   
                string sPSGUBN = FPS91_TY_S_AC_4B4FV308.GetValue(e.Row, "PSGUBN").ToString();
                if (sPSGUBN == "4")
                {
                    FPS91_TY_S_AC_4B4FV308.SetValue("PSGBGIVE", "Y");
                }
                else 
                {
                    FPS91_TY_S_AC_4B4FV308.SetValue("PSGBGIVE", "N");
                }
            }
        }
        #endregion
    }
}
