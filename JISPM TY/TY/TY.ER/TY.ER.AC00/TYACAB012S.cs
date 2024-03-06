using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.AC00
{
    /// <summary>
    /// 입금표관리 등록 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.06.13 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_26D92837 : 입금표 조회
    ///  TY_P_AC_26D90842 : 입금표 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_26D93839 : 입금표 조회
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
    ///  ARLOCAL :  지　　역
    ///  ARSEQ   :  순　　번
    ///  ARYEAR  :  년　　도
    /// </summary>
    public partial class TYACAB012S : TYBase
    {
        #region Description : 페이지 로드
        public TYACAB012S()
        {
            InitializeComponent();
        }

        private void TYACAB012S_Load(object sender, System.EventArgs e)
        {
            // 부서
            this.CBH01_ARDPMK.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.TXT01_ARYEAR.SetValue(DateTime.Now.ToString("yyyyMMdd").Substring(0, 4));

            SetStartingFocus(this.TXT01_ARYEAR);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_26D92837",
                this.TXT01_ARYEAR.GetValue(),
                this.CBH01_ARDPMK.GetValue(),
                this.TXT01_ARSEQ.GetValue(),
                this.CBH01_ARCDSB.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_26D93839.SetValue(dt);
            }
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYACAB012I(string.Empty, string.Empty, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            // 삭제 프로시저
            this.DbConnector.Attach("TY_P_AC_26D90842", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // 삭제 체크되어 있는 행의 칼럼(VNCODE)을 가져와서 체크하는 부분
            DataTable dt = this.FPS91_TY_S_AC_26D93839.GetDataSourceInclude(TSpread.TActionType.Remove, "ARYEAR", "ARDPMK", "ARSEQ", "ARJUNNO");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["ARJUNNO"].ToString() != "")
                {
                    this.ShowMessage("TY_M_GB_25F8V482");
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            // 스프레드 칼럼 데이터 넘겨주는 부분
            e.ArgData = dt;
        }
        #endregion

        #region Description : 스프레드 더블클릭 이벤트
        private void FPS91_TY_S_AC_26D93839_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            // 파라미터값 보내기
            if ((new TYACAB012I(
                                this.FPS91_TY_S_AC_26D93839.GetValue("ARYEAR").ToString(),
                                this.FPS91_TY_S_AC_26D93839.GetValue("ARDPMK").ToString(),
                                this.FPS91_TY_S_AC_26D93839.GetValue("ARSEQ").ToString()
                               )).ShowDialog() == System.Windows.Forms.DialogResult.OK)

                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : TXT01_ARYEAR_TextChanged 이벤트
        private void TXT01_ARYEAR_TextChanged(object sender, EventArgs e)
        {
            if (TXT01_ARYEAR.GetValue().ToString() != "")
            {
                this.CBH01_ARDPMK.DummyValue = TXT01_ARYEAR.GetValue() + "0101";
            }
            else
            {
                this.CBH01_ARDPMK.DummyValue = DateTime.Now.ToString("yyyyMMdd");
            }
        }
        #endregion
    }
}