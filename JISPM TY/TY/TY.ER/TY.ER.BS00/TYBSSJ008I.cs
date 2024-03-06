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
    /// 당기실적 특별손익관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.09.13 14:02
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_79DEA572 : 당기실적 특별손익관리 조회
    ///  TY_P_AC_79DEF573 : 당기실적 특별손익관리 등록
    ///  TY_P_AC_79DFC575 : 당기실적 특별손익관리 수정
    ///  TY_P_AC_79DFD576 : 당기실적 특별손익관리 삭제
    ///  TY_P_AC_79DFU578 : 당기실적 특별손익관리 확인
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_79DFE577 : 당기실적 특별손익관리
    /// 
    ///  # 알림문자 정보 ####
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
    ///  BSJYYMM : 실적생성년월
    /// </summary>
    public partial class TYBSSJ008I : TYBase
    {
        #region Description : 폼 로드
        public TYBSSJ008I()
        {
            InitializeComponent();
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_79DFE577, "BPJBPAC", "BPJBPACNM", "BPJBPAC");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_79DFE577, "BPJCDAC", "BPJCDACNM", "BPJCDAC");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_79DFE577, "BPJDPAC", "BPJDPACNM", "BPJDPAC");
        }

        private void TYBSSJ008I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_79DFE577, "BPJYYMM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_79DFE577, "BPJYEAR");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_79DFE577, "BPJAPDATE");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_79DFE577, "BPJBPAC");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_79DFE577, "BPJCDAC");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.FPS91_TY_S_AC_79DFE577.Initialize();

            //this.DTP01_BSJYYMM.SetValue(System.DateTime.Now.ToString("yyyy-MM"));

            CBH01_BSJYYMM.SetValue(UP_Get_LastSJYYMM());

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.CBH01_BSJYYMM);

            TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_79DFE577, "BPJDPAC");
            if (tyCodeBox != null)
                tyCodeBox.DummyValue = System.DateTime.Now.ToString("yyyyMM");
            return;
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            try
            {
                this.FPS91_TY_S_AC_79DFE577.Initialize();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_79DEA572", this.CBH01_BSJYYMM.GetValue().ToString());
                DataTable dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_AC_79DFE577.SetValue(dt);
            }
            catch
            {
                
            }
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_79DFD576", dt);
                this.DbConnector.ExecuteNonQuery();

                this.ShowMessage("TY_M_GB_23NAD874");

                BTN61_INQ_Click(null, null);
            }
            catch
            {
                this.ShowMessage("TY_M_GB_43C9G671");
            }
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_79DFE577.GetDataSourceInclude(TSpread.TActionType.Remove, "BPJYYMM", "BPJYEAR", "BPJAPDATE", "BPJBPAC", "BPJCDAC", "BPJDPAC" );

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

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            DataTable dt = new DataTable();

            try
            {
                //신규등록
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_AC_79DEF573", ds.Tables[0].Rows[i]["BPJYYMM"].ToString(),
                                                                ds.Tables[0].Rows[i]["BPJYEAR"].ToString(),
                                                                Set_Fill2(ds.Tables[0].Rows[i]["BPJAPDATE"].ToString()),
                                                                ds.Tables[0].Rows[i]["BPJBPAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["BPJCDAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["BPJDPAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["BPJMONAMT"].ToString(),
                                                                ds.Tables[0].Rows[i]["BPJMEMO"].ToString(),
                                                                TYUserInfo.EmpNo
                                                                );
                    this.DbConnector.ExecuteTranQuery();
                }

                //수정
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_AC_79DFC575", 
                                                                ds.Tables[1].Rows[i]["BPJMONAMT"].ToString(),
                                                                ds.Tables[1].Rows[i]["BPJMEMO"].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["BPJYYMM"].ToString(),
                                                                ds.Tables[1].Rows[i]["BPJYEAR"].ToString(),
                                                                Set_Fill2(ds.Tables[1].Rows[i]["BPJAPDATE"].ToString()),
                                                                ds.Tables[1].Rows[i]["BPJBPAC"].ToString(),
                                                                ds.Tables[1].Rows[i]["BPJCDAC"].ToString(),
                                                                ds.Tables[1].Rows[i]["BPJDPAC"].ToString()
                                                                );
                    this.DbConnector.ExecuteTranQuery();
                }

                this.ShowMessage("TY_M_GB_23NAD873");

                BTN61_INQ_Click(null, null);
            }
            catch
            {
                this.ShowMessage("TY_M_AC_246A2488");
            }
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            ds.Tables.Add(this.FPS91_TY_S_AC_79DFE577.GetDataSourceInclude(TSpread.TActionType.New, "BPJYYMM", "BPJYEAR", "BPJAPDATE", "BPJBPAC", "BPJCDAC", "BPJDPAC", "BPJMONAMT", "BPJMEMO"));

            ds.Tables.Add(this.FPS91_TY_S_AC_79DFE577.GetDataSourceInclude(TSpread.TActionType.Update, "BPJYYMM", "BPJYEAR", "BPJAPDATE", "BPJBPAC", "BPJCDAC", "BPJDPAC", "BPJMONAMT", "BPJMEMO"));

            // 저장 체크
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_79DFU578", ds.Tables[0].Rows[i]["BPJYYMM"].ToString(),
                                                            ds.Tables[0].Rows[i]["BPJYEAR"].ToString(),
                                                            Set_Fill2(ds.Tables[0].Rows[i]["BPJAPDATE"].ToString()),
                                                            ds.Tables[0].Rows[i]["BPJBPAC"].ToString(),
                                                            ds.Tables[0].Rows[i]["BPJCDAC"].ToString(),
                                                            ds.Tables[0].Rows[i]["BPJDPAC"].ToString()
                                                            );

                DataTable dtTmp = this.DbConnector.ExecuteDataTable();

                if (dtTmp.Rows.Count > 0)
                {
                    this.ShowCustomMessage("이미 등록된 항목입니다.[" + ds.Tables[0].Rows[i]["BPJCDAC"].ToString() + "]",
                                            "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }
            }

            // 수정 체크
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {

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

        #region Description : row 추가 이벤트
        private void FPS91_TY_S_AC_79DFE577_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_79DFE577.SetValue("BPJYYMM", this.CBH01_BSJYYMM.GetValue().ToString());
        }
        #endregion

        #region Description : 최종 실적년월 가져오기
        private string UP_Get_LastSJYYMM()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_7AKAW859");
            string sYYMM = this.DbConnector.ExecuteScalar().ToString();

            return sYYMM;
        }
        #endregion
    }
}
