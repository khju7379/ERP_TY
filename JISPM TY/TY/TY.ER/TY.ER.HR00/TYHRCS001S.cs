using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 연장관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2014.11.25 16:53
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BPJQ529 : 연장관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_614DY379 : 연장관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  GYSABUN : 사  번
    ///  GYGUBN : 신청형태
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRCS001S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRCS001S()
        {
            InitializeComponent();
        }

        private void TYHRCS001S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.TXT01_GSTYYMM.SetValue(DateTime.Now.ToString("yyyy"));
            this.TXT01_GEDYYMM.SetValue(DateTime.Now.ToString("yyyy"));

            this.SetStartingFocus(this.TXT01_GSTYYMM);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            bool returnValue = UP_SearchCheck();

            if (returnValue != true)
            {
                return;
            }

            this.FPS91_TY_S_HR_614DY379.Initialize();
            this.DbConnector.CommandClear();

            if (this.CBO01_GGUBUN.GetValue().ToString() == "1")
            {
                if (this.TXT01_GDATE.GetValue().ToString() != "")
                {
                    this.DbConnector.Attach("TY_P_HR_614FF384", this.TXT01_GSTYYMM.GetValue(), this.TXT01_GEDYYMM.GetValue(), this.TXT01_GDATE.GetValue(), this.TXT01_GDATE.GetValue());
                }
            }
            else if (this.CBO01_GGUBUN.GetValue().ToString() == "2")
            {
                if (this.TXT01_GDATE.GetValue().ToString() != "")
                {
                    this.DbConnector.Attach("TY_P_HR_614FG385", this.TXT01_GSTYYMM.GetValue(), this.TXT01_GEDYYMM.GetValue(), this.TXT01_GDATE.GetValue());
                }
            }
            else
            {
                this.DbConnector.Attach("TY_P_HR_614DY378", this.TXT01_GSTYYMM.GetValue(), this.TXT01_GEDYYMM.GetValue());
            }
            this.FPS91_TY_S_HR_614DY379.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYHRCS001I(string.Empty, string.Empty, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);

        }
        #endregion

        #region Description : 그리드 더블 클릭 이벤트
        private void FPS91_TY_S_HR_614DY379_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYHRCS001I(this.FPS91_TY_S_HR_614DY379.GetValue("FGYEAR").ToString(), this.FPS91_TY_S_HR_614DY379.GetValue("FGSEQ").ToString(), this.FPS91_TY_S_HR_614DY379.GetValue("FGYOILGN").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_614FP386", dt);
            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);
                       
            this.ShowMessage("TY_M_GB_23NAD874");

        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_614DY379.GetDataSourceInclude(TSpread.TActionType.Remove, "FGYEAR", "FGSEQ", "FGYOILGN");

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

        #region  Description : 조회 체크
        private bool UP_SearchCheck()
        {
            if (Convert.ToInt32(this.TXT01_GSTYYMM.GetValue().ToString()) > Convert.ToInt32(this.TXT01_GEDYYMM.GetValue().ToString()))
            {
                this.ShowCustomMessage("시작일자는 종료일자보다 이전이어야 합니다. ", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                return false;
            }


            if (this.TXT01_GDATE.GetValue().ToString() != "" && int.Parse(this.TXT01_GSTYYMM.GetValue().ToString()) < int.Parse(this.TXT01_GDATE.GetValue().ToString().Substring(0, 4)) && int.Parse(this.TXT01_GEDYYMM.GetValue().ToString()) < int.Parse(this.TXT01_GDATE.GetValue().ToString().Substring(0, 4)))
            {
                this.ShowCustomMessage("기준년월을 년도 범위내에서 지정해주세요!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                return false;
            }
            if (this.TXT01_GDATE.GetValue().ToString() != "" && int.Parse(this.TXT01_GSTYYMM.GetValue().ToString()) > int.Parse(this.TXT01_GDATE.GetValue().ToString().Substring(0, 4)))
            {
                this.ShowCustomMessage("기준년월을 년도 범위내에서 지정해주세요!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                return false;
            }
            if (this.TXT01_GDATE.GetValue().ToString() != "" && int.Parse(this.TXT01_GEDYYMM.GetValue().ToString()) < int.Parse(this.TXT01_GDATE.GetValue().ToString().Substring(0, 4)))
            {
                this.ShowCustomMessage("기준년월을 년도 범위내에서 지정해주세요!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                return false;
            }
            if (this.TXT01_GDATE.GetValue().ToString() == "" && CBO01_GGUBUN.GetValue().ToString() != "3")
            {
                this.ShowCustomMessage("진행구분이 전체가 아닌경우에는 기준년월을 입력하세요!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                return false;
            }

            return true;
        }
        #endregion
    }
}
