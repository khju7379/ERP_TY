using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 급여 세금가중치 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.12.22 11:41
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4CMB5917 : 급여 세금가중치 관리 조회
    ///  TY_P_HR_4CMBA918 : 급여 세금가중치 관리 등록
    ///  TY_P_HR_4CMBB919 : 급여 세금가중치 관리 수정
    ///  TY_P_HR_4CMBC920 : 급여 세금가중치 관리 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_4CMBP922 : 급여 세금가중치 관리
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  REM : 삭제
    ///  SAV : 저장
    ///  PAYGUBN : 급여구분
    ///  PAYJIDATE : 지급일자
    ///  PAYYYMM : 급여년월
    /// </summary>
    public partial class TYHRPY06C2 : TYBase
    {
        private string fsTXGUBN = string.Empty;
        private string fsTXYYMM = string.Empty;
        private string fsTXJIDATE = string.Empty;

        #region Description : 페이지 로드
        public TYHRPY06C2(string sTXGUBN, string sTXYYMM, string sTXJIDATE)
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_4CMBP922, "TXJKCODE", "TXJKCODENM", "TXJKCODE");

            this.fsTXGUBN = sTXGUBN;
            this.fsTXYYMM = sTXYYMM;
            this.fsTXJIDATE = sTXJIDATE;
        }

        private void TYHRPY06C2_Load(object sender, System.EventArgs e)
        {

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.CBH01_PAYGUBN.SetValue(fsTXGUBN);
            this.DTP01_PAYYYMM.SetValue(fsTXYYMM);
            this.DTP01_PAYJIDATE.SetValue(fsTXJIDATE);

            CBH01_PAYGUBN.SetReadOnly(true);
            DTP01_PAYYYMM.SetReadOnly(true);
            DTP01_PAYJIDATE.SetReadOnly(true);

            UP_Select();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_535G1512", this.CBH01_PAYGUBN.GetValue().ToString(), this.DTP01_PAYYYMM.GetString().Substring(0,6), this.DTP01_PAYJIDATE.GetString());
            Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
            if (iCnt > 0)
            {
                this.BTN61_SAV.Visible = false;
                this.BTN61_REM.Visible = false;
            }
        }
        #endregion

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4CMBC920", dt);
            this.DbConnector.ExecuteNonQueryList();

            UP_Select();

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_4CMBP922.GetDataSourceInclude(TSpread.TActionType.Remove, "TXGUBN", "TXYYMM", "TXJIDATE", "TXJKCODE");

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

            this.DbConnector.CommandClear();

            this.DataTableColumnAdd(ds.Tables[0], "TXHISAB", TYUserInfo.EmpNo);
            this.DataTableColumnAdd(ds.Tables[1], "TXHISAB", TYUserInfo.EmpNo);

            this.DbConnector.Attach("TY_P_HR_4CMBA918", ds.Tables[0]);
            this.DbConnector.Attach("TY_P_HR_4CMBB919", ds.Tables[1]);
            this.DbConnector.ExecuteTranQueryList();

            UP_Select();

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_HR_4CMBP922.GetDataSourceInclude(TSpread.TActionType.New, "TXGUBN", "TXYYMM", "TXJIDATE", "TXJKCODE", "TXAPGUBN", "TXAMOUNT"));
            ds.Tables.Add(this.FPS91_TY_S_HR_4CMBP922.GetDataSourceInclude(TSpread.TActionType.Update, "TXGUBN", "TXYYMM", "TXJIDATE", "TXJKCODE", "TXAPGUBN", "TXAMOUNT"));


            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4CTAZ985", ds.Tables[0].Rows[i]["TXGUBN"].ToString(),
                                                            ds.Tables[0].Rows[i]["TXYYMM"].ToString(),
                                                            ds.Tables[0].Rows[i]["TXJIDATE"].ToString(),
                                                            ds.Tables[0].Rows[i]["TXJKCODE"].ToString());
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_3219C986");
                    e.Successed = false;
                    return;
                }
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    if (i != j)
                    {
                        if (ds.Tables[0].Rows[i]["TXGUBN"].ToString() == ds.Tables[0].Rows[j]["TXGUBN"].ToString() && ds.Tables[0].Rows[i]["TXYYMM"].ToString() == ds.Tables[0].Rows[j]["TXYYMM"].ToString() &&
                            ds.Tables[0].Rows[i]["TXJIDATE"].ToString() == ds.Tables[0].Rows[j]["TXJIDATE"].ToString() && ds.Tables[0].Rows[i]["TXJKCODE"].ToString() == ds.Tables[0].Rows[j]["TXJKCODE"].ToString())
                        {
                            this.ShowMessage("TY_M_AC_3219C986");
                            e.Successed = false;
                            return;
                        }
                    }
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

        #region Description : 그리드 조회
        private void UP_Select()
        {   
            this.FPS91_TY_S_HR_4CMBP922.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4CMB5917", this.CBH01_PAYGUBN.GetValue().ToString(), this.DTP01_PAYYYMM.GetString().Substring(0, 6), this.DTP01_PAYJIDATE.GetString());
            DataTable dt = this.DbConnector.ExecuteDataTable();
            this.FPS91_TY_S_HR_4CMBP922.SetValue(dt);
        }
        #endregion

        #region Description : 행 추가 이벤트
        private void FPS91_TY_S_HR_4CMBP922_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_HR_4CMBP922.SetValue(e.RowIndex, "TXGUBN", this.CBH01_PAYGUBN.GetValue().ToString());
            this.FPS91_TY_S_HR_4CMBP922.SetValue(e.RowIndex, "TXYYMM", this.DTP01_PAYYYMM.GetString().Substring(0, 6));
            this.FPS91_TY_S_HR_4CMBP922.SetValue(e.RowIndex, "TXJIDATE", this.DTP01_PAYJIDATE.GetString());
            this.FPS91_TY_S_HR_4CMBP922.SetValue(e.RowIndex, "TXAPGUBN", "1");
        }
        #endregion
    }
}
