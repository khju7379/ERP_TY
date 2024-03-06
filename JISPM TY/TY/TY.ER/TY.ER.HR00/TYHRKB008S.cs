using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using TY.ER.GB00;
using System.Windows.Forms;
using GrapeCity.ActiveReports;


namespace TY.ER.HR00
{
    /// <summary>
    /// 재직증명서 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.11.25 17:53
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BPGX507 : 재직증명서 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_4BPGY509 : 재직증명서 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  KBSABUN : 사번
    ///  CEGUBUN : 발급구분
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYHRKB008S : TYBase
    {

        #region Description : 페이지 로드
        public TYHRKB008S()
        {
            InitializeComponent();
        }

        private void TYHRKB008S_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_4BPGY509, "CEPRT");

            (this.FPS91_TY_S_HR_4BPGY509.Sheets[0].Columns[13].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.printer;

            DTP01_STDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            DTP01_EDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {

            bool returnValue = UP_SearchCheck();

            if (returnValue != true)
            {
                return;
            }

            this.FPS91_TY_S_HR_4BPGY509.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_HR_4BPGX507",
                this.CBH01_KBSABUN.GetValue(),
                this.DTP01_STDATE.GetString(),
                this.DTP01_EDDATE.GetString(),
                this.CBO01_CEGUBUN.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_HR_4BPGY509.SetValue(dt);
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        }
        #endregion

        #region Description : 신규버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYHRKB008I("", "")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 그리드 더블클릭 이벤트
        private void FPS91_TY_S_HR_4BPGY509_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYHRKB008I(this.FPS91_TY_S_HR_4BPGY509.GetValue("CEYEAR").ToString(), this.FPS91_TY_S_HR_4BPGY509.GetValue("CESEQ").ToString()
                                )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 그리드 출력버튼
        private void FPS91_TY_S_HR_4BPGY509_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "13")
            {
                /*
                if (this.FPS91_TY_S_HR_4BPGY509.GetValue("CEBALYN").ToString() == "Y")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_4BPJJ528",
                                            this.FPS91_TY_S_HR_4BPGY509.GetValue("CEYEAR").ToString(),
                                            this.FPS91_TY_S_HR_4BPGY509.GetValue("CESEQ").ToString()
                                            );
                    DataTable dt = this.DbConnector.ExecuteDataTable();

                    SectionReport rpt = new TYHRKB008R();
                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
                else
                {
                    this.ShowCustomMessage("발급여부를 Y 로 수정하셔야만 출력가능합니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }*/

                //발급여부를 Y로 UPDATE                        
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_623IV511",
                                        "Y",
                                        TYUserInfo.EmpNo,
                                        this.FPS91_TY_S_HR_4BPGY509.GetValue("CEYEAR").ToString(),
                                        this.FPS91_TY_S_HR_4BPGY509.GetValue("CESEQ").ToString()
                                        );
                this.DbConnector.ExecuteNonQuery();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BPJJ528",
                                           TYUserInfo.SecureKey,
                                          "Y",
                                          TYUserInfo.SecureKey, "Y",
                                        this.FPS91_TY_S_HR_4BPGY509.GetValue("CEYEAR").ToString(),
                                        this.FPS91_TY_S_HR_4BPGY509.GetValue("CESEQ").ToString()
                                        );
                DataTable dt = this.DbConnector.ExecuteDataTable();

                SectionReport rpt = new TYHRKB008R();
                (new TYERGB001P(rpt, dt)).ShowDialog();

            }
        }
        #endregion

        #region  Description : 조회 체크
        private bool UP_SearchCheck()
        {
            if (Convert.ToInt32(this.DTP01_STDATE.GetString().ToString()) > Convert.ToInt32(this.DTP01_EDDATE.GetString().ToString()))
            {
                this.ShowCustomMessage("시작일자는 종료일자보다 이전이어야 합니다. ", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                return false;
            }

            return true;
        }
        #endregion
    }
}
