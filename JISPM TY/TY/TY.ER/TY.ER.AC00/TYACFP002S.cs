using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.AC00
{
    /// <summary>
    /// 미지급금 관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.05.09 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2596Z193 : 미지급금 삭제
    ///  TY_P_AC_259BZ118 : 미지급금 관리
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2596S191 : 미지급금 관리(조회)
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  M1VNCD : 거래처
    ///  M1SAGB : 지역구분
    ///  M1DTED : 지급일자
    ///  M1NOSQ : 순차번호
    /// </summary>
    public partial class TYACFP002S : TYBase
    {
        #region Description : 페이지 로드
        public TYACFP002S()
        {
            InitializeComponent();
        }

        private void TYACFP002S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            SetStartingFocus(this.DTP01_M1DTED);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {

            string sM1SAGB = string.Empty;

            if (this.CBO01_M1SAGB.GetValue().ToString() != "")
            {
                sM1SAGB = this.CBO01_M1SAGB.GetValue().ToString() == "1" ? "1" : "6";
            }

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_259BZ118",
                this.DTP01_M1DTED.GetValue(),
                this.CBH01_M1VNCD.GetValue(),
                this.TXT01_M1NOSQ.GetValue(),
                sM1SAGB,
                this.CBO01_VNBKYN.GetValue()
                );

            this.FPS91_TY_S_AC_2596S191.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            // 삭제 프로시저
            this.DbConnector.Attach("TY_P_AC_2596Z193", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowCustomMessage(Convert.ToString(dt.Rows.Count) + "건 삭제 되었습니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // 삭제 체크되어 있는 행의 칼럼(VNCODE)을 가져와서 체크하는 부분
            DataTable dt = this.FPS91_TY_S_AC_2596S191.GetDataSourceInclude
                (
                TSpread.TActionType.Select,
                "M1DTED1",
                "M1VNCD",
                "M1NOSQ"
                );

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            Int16 iCnt = 0;

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_7CF9O276", dt.Rows[i]["M1DTED1"].ToString(), dt.Rows[i]["M1VNCD"].ToString(), dt.Rows[i]["M1NOSQ"].ToString());
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                    if (iCnt > 0)
                    {
                        this.ShowMessage("TY_M_GB_25F8V482");
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

        #region Description : 스프레드 더블클릭 이벤트
        private void FPS91_TY_S_AC_2596S191_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            // 파라미터값 보내기
            if ((new TYACFP002I(this.FPS91_TY_S_AC_2596S191.GetValue("M1DTED").ToString(),
                                this.FPS91_TY_S_AC_2596S191.GetValue("M1VNCD").ToString(),
                                this.FPS91_TY_S_AC_2596S191.GetValue("M1NOSQ").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)

                this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}