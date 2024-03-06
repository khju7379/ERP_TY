using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 학자금관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.03.13 09:56
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_73DAP897 : 학자금기본사항관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_73DAP895 : 학자금관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  HKJUGUBN : 재학상태
    ///  HKSABUN : 사번
    ///  HKCHDNAME : 자녀이름
    /// </summary>
    public partial class TYHRKB019S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRKB019S()
        {
            InitializeComponent();
        }

        private void TYHRKB019S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.UP_Set_JuminAuthCheck(CBO01_INQOPTION);

            this.SetStartingFocus(this.CBH01_HKSABUN.CodeText);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_73DAP895.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_73DAP897", TYUserInfo.SecureKey, CBO01_INQOPTION.GetValue().ToString(),  this.CBH01_HKSABUN.GetValue().ToString(), this.TXT01_HKCHDNAME.GetValue(), this.CBH01_HKJUGUBN.GetValue());
            this.FPS91_TY_S_HR_73DAP895.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYHRKB019I(string.Empty, string.Empty, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_73DAR903", dt);
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            BTN61_INQ_Click(null, null);
        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_73DAP895.GetDataSourceInclude(TSpread.TActionType.Remove, "HKSABUN", "HKYEAR", "HKSSEQ" );

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_73UFK163", dt.Rows[i]["HKSABUN"].ToString(), dt.Rows[i]["HKYEAR"].ToString(), dt.Rows[i]["HKSSEQ"].ToString());
                    Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());
                    if (iCnt > 0)
                    {
                        this.ShowMessage("TY_M_HR_73UFM164");
                        e.Successed = false;
                        return;
                    }
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

        #region  Description : FPS91_TY_S_HR_73DAP895_CellDoubleClick 버튼 이벤트
        private void FPS91_TY_S_HR_73DAP895_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.OpenModalPopup(new TYHRKB019I(this.FPS91_TY_S_HR_73DAP895.GetValue("HKSABUN").ToString(),
                                                   this.FPS91_TY_S_HR_73DAP895.GetValue("HKYEAR").ToString(),
                                                   this.FPS91_TY_S_HR_73DAP895.GetValue("HKSSEQ").ToString())) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        
    }
}
