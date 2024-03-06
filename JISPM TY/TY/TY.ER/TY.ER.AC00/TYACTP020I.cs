using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;
using TY.Service.Library.Controls.TYSpreadCellType;
using GrapeCity.ActiveReports;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using TY.ER.GB00;

namespace TY.ER.AC00
{
    /// <summary>
    /// 선급자재 관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2013.02.19 09:59
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_32J79125 : 선급자재 미생성 조회
    ///  TY_P_MR_32J7A126 : 선급자재 생성 조회
    ///  TY_P_MR_32J7A127 : 선급자재 DETAIL 조회
    ///  TY_P_MR_32J7A128 : 선급자재 DETAIL 하위 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_32J7C129 : 선급자재 생성 조회
    ///  TY_S_MR_32J7M130 : 선급자재 DETAIL 조회
    ///  TY_S_AC_BB8EH709 : 선급자재 DETAIL 하위 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CANCEL : 취소
    ///  CREATE : 생성
    ///  INQ : 조회
    ///  JASAN_CRE : 자산생성
    ///  JASAN_DEL : 자산삭제
    ///  JPNO_CRE : 전표생성
    ///  JPNO_DEL : 전표삭제
    ///  FXDDPMK : 귀속부서
    ///  FXDSAUP : 선급사업부
    ///  FXDGETDATE : 취득일
    ///  GCDACGHAP : 계정총액
    ///  GDAESANGHAP : 대상총액
    ///  GJANGHAP : 잔액
    /// </summary>
    public partial class TYACTP020I : TYBase
    {
        private string fsWJSABUN = string.Empty;

        #region Description : 페이지 로드
        public TYACTP020I()
        {
            InitializeComponent();

            // 사번
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_BB8EH709, "WJSABUN", "KBHANGL", "WJSABUN");
        }

        private void TYACTP020I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_BB8EH709, "WJYYMM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_BB8EH709, "WJSABUN");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_BB8EH709, "WJDPAC");

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);
            
            this.FPS91_TY_S_AC_BB8EH709.Initialize();

            SetStartingFocus(this.DTP01_WJYYMM);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_BB8EH709.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.Attach
                (
                "TY_P_AC_BB8E5704",
                Get_Date(this.DTP01_WJYYMM.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_BB8EH709.SetValue(dt);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataTable dt = new DataTable();

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();

                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_BB8E5705", "1",
                                                                ds.Tables[0].Rows[i]["WJYYMM"].ToString(),
                                                                ds.Tables[0].Rows[i]["WJSABUN"].ToString(),
                                                                ds.Tables[0].Rows[i]["WJDPAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["WJMEMO"].ToString(),
                                                                Get_Numeric(ds.Tables[0].Rows[i]["WJAMOUNT"].ToString()),
                                                                ds.Tables[0].Rows[i]["WJBIGO"].ToString(),
                                                                TYUserInfo.EmpNo
                                                                ); // 저장
                }

                this.DbConnector.ExecuteTranQueryList();
            }

            // 수정
            if (ds.Tables[1].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();

                for (i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_BB8EF706", ds.Tables[1].Rows[i]["WJMEMO"].ToString(),
                                                                Get_Numeric(ds.Tables[1].Rows[i]["WJAMOUNT"].ToString()),
                                                                ds.Tables[1].Rows[i]["WJBIGO"].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["WJBRANCH"].ToString(),
                                                                ds.Tables[1].Rows[i]["WJYYMM"].ToString(),
                                                                ds.Tables[1].Rows[i]["WJSABUN"].ToString(),
                                                                ds.Tables[1].Rows[i]["WJDPAC"].ToString()
                                                                ); // 수정
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            // 삭제
            if (ds.Tables[2].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();

                for (i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_BB8EG707", ds.Tables[2].Rows[i]["WJBRANCH"].ToString(),
                                                                ds.Tables[2].Rows[i]["WJYYMM"].ToString(),
                                                                ds.Tables[2].Rows[i]["WJSABUN"].ToString(),
                                                                ds.Tables[2].Rows[i]["WJDPAC"].ToString()
                                                                ); // 삭제
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            this.ShowMessage("TY_M_MR_2BF50354"); // 저장 메세지

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 처리 ProcessCheck
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataSet ds = new DataSet();

            DataTable dt = new DataTable();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_BB8EH709.GetDataSourceInclude(TSpread.TActionType.New,    "WJYYMM", "WJSABUN", "WJDPAC", "WJMEMO", "WJAMOUNT", "WJBIGO"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_BB8EH709.GetDataSourceInclude(TSpread.TActionType.Update, "WJBRANCH", "WJYYMM", "WJSABUN", "WJDPAC", "WJMEMO", "WJAMOUNT", "WJBIGO"));
            // 스프레드에서 삭제 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_BB8EH709.GetDataSourceInclude(TSpread.TActionType.Remove, "WJBRANCH", "WJYYMM", "WJSABUN", "WJDPAC", "WJMEMO", "WJAMOUNT"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2CV43442");
                e.Successed = false;
                return;
            }

            // 신규
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["WJAMOUNT"].ToString())) == 0)
                {
                    this.ShowCustomMessage("금액을 입력하시기 바랍니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                    SetFocus(this.DTP01_WJYYMM);

                    e.Successed = false;
                    return;
                }
            }

            // 수정
            for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                if (double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["WJAMOUNT"].ToString())) == 0)
                {
                    this.ShowCustomMessage("금액을 입력하시기 바랍니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                    SetFocus(this.DTP01_WJYYMM);

                    e.Successed = false;
                    return;
                }

                // 원천세 생성이 되어 있으면 수정 및 삭제 불가
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_4AGHZ192", ds.Tables[1].Rows[i]["WJBRANCH"].ToString(),
                                                            ds.Tables[1].Rows[i]["WJYYMM"].ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowCustomMessage("원천세가 생성 되어 있으므로 수정 및 삭제 작업이 불가합니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                    SetFocus(this.DTP01_WJYYMM);

                    e.Successed = false;
                    return;
                }
            }

            // 삭제
            for (i = 0; i < ds.Tables[2].Rows.Count; i++)
            {
                // 원천세 생성이 되어 있으면 수정 및 삭제 불가
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_4AGHZ192", ds.Tables[2].Rows[i]["WJBRANCH"].ToString(),
                                                            ds.Tables[2].Rows[i]["WJYYMM"].ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowCustomMessage("원천세가 생성 되어 있으므로 수정 및 삭제 작업이 불가합니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                    SetFocus(this.DTP01_WJYYMM);

                    e.Successed = false;
                    return;
                }
            }            

            // 저장하시겠습니까?
            if (!this.ShowMessage("TY_M_MR_2BF50353"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 인사 기본사항 가져오기
        private void FPS91_TY_S_AC_BB8EH709_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            if (e.Column == 2)
            {
                string sWJSABUN = this.FPS91_TY_S_AC_BB8EH709.GetValue("WJSABUN").ToString();

                if (sWJSABUN.Length == 6)
                {
                    if (fsWJSABUN != sWJSABUN)
                    {
                        fsWJSABUN = sWJSABUN;

                        this.DbConnector.CommandClear();

                        this.DbConnector.Attach("TY_P_AC_BB8EG708", sWJSABUN);

                        DataTable dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            this.FPS91_TY_S_AC_BB8EH709.SetValue("KBHANGL", dt.Rows[0]["KBHANGL"].ToString());   // 성명
                            this.FPS91_TY_S_AC_BB8EH709.SetValue("WJDPAC", dt.Rows[0]["WJDPAC"].ToString());    // 귀속부서코드
                            this.FPS91_TY_S_AC_BB8EH709.SetValue("KBBUSEONM", dt.Rows[0]["KBBUSEONM"].ToString()); // 귀속부서명
                            this.FPS91_TY_S_AC_BB8EH709.SetValue("KBGUNMUNM", dt.Rows[0]["KBGUNMUNM"].ToString()); // 지역구분명
                        }
                    }
                }
            }
        }
        #endregion

        private void FPS91_TY_S_AC_BB8EH709_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {

        }

        private void FPS91_TY_S_AC_BB8EH709_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            if (e.Column == 2)
            {
                string sWJSABUN = this.FPS91_TY_S_AC_BB8EH709.GetValue("WJSABUN").ToString();

                if (sWJSABUN.Length == 6)
                {
                    if (fsWJSABUN != sWJSABUN)
                    {
                        fsWJSABUN = sWJSABUN;

                        this.DbConnector.CommandClear();

                        this.DbConnector.Attach("TY_P_AC_BB8EG708", sWJSABUN);

                        DataTable dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            this.FPS91_TY_S_AC_BB8EH709.SetValue("KBHANGL", dt.Rows[0]["KBHANGL"].ToString());   // 성명
                            this.FPS91_TY_S_AC_BB8EH709.SetValue("WJDPAC", dt.Rows[0]["WJDPAC"].ToString());    // 귀속부서코드
                            this.FPS91_TY_S_AC_BB8EH709.SetValue("KBBUSEONM", dt.Rows[0]["KBBUSEONM"].ToString()); // 귀속부서명
                            this.FPS91_TY_S_AC_BB8EH709.SetValue("KBGUNMUNM", dt.Rows[0]["KBGUNMUNM"].ToString()); // 지역구분명
                        }
                    }
                }
            }
        }
    }
}