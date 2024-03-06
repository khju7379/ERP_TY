using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using DataDynamics.ActiveReports;
using TY.ER.GB00;
using System.Drawing;

namespace TY.ER.HR00
{
    /// <summary>
    /// 개인 급여 통합 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.12.23 10:10
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4CNJ0949 : 개인 급여 통합 조회(급여지급관리)
    ///  TY_P_HR_4CNJ2950 : 개인 급여 통합 조회(급여결과마스타)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_4CNJE951 : 개인 급여 통합 조회(급여지급관리)
    ///  TY_S_HR_5BCDZ151 : 개인 급여 통합 조회(급여결과마스타)
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  INQ_FXM : 조회
    ///  KBBUSEO : 부서
    ///  KBJKCD : 직급
    ///  KBSABUN : 사번
    ///  PAYGUBN : 급여구분
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYHRSB001S : TYBase
    {
        string fsGUBN = string.Empty;

        #region Description : 페이지 로드
        public TYHRSB001S()
        {
            InitializeComponent();
        }

        private void TYHRSB001S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.SetStartingFocus(DTP01_GSTYYMM);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sSMJJCD = string.Empty;

            if (this.CBO01_SMJJCD.GetValue().ToString() == "1")
            {
                sSMJJCD = "";
            }
            else if (this.CBO01_SMJJCD.GetValue().ToString() == "2")
            {
                sSMJJCD = "1A,1B,2A,2B";
            }
            else if (this.CBO01_SMJJCD.GetValue().ToString() == "3")
            {
                sSMJJCD = "2C,3A,3B,4A";
            }
            else if (this.CBO01_SMJJCD.GetValue().ToString() == "4")
            {
                sSMJJCD = "3C,3D,6C";
            }

            this.FPS91_TY_S_HR_5BCDZ151.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_5BCHQ161",
                                    sSMJJCD.ToString(),
                                    this.DTP01_GSTYYMM.GetValue().ToString(),
                                    this.DTP01_GEDYYMM.GetValue().ToString(),
                                    this.CBH01_SMSABUN.GetValue().ToString()
                                    );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_HR_5BCDZ151.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_HR_5BCDZ151.ActiveSheet.RowCount; i++)
            {
                this.FPS91_TY_S_HR_5BCDZ151.ActiveSheet.Columns[10].BackColor = Color.LightBlue;
                this.FPS91_TY_S_HR_5BCDZ151.ActiveSheet.Columns[10].Font = new Font("굴림", 9, FontStyle.Bold);
            }
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            DataTable dm = new DataTable();

            this.DbConnector.CommandClear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dm.Clear();

                this.DbConnector.Attach("TY_P_HR_9CHEB593",
                                       dt.Rows[i][0].ToString(),
                                       dt.Rows[i][1].ToString()
                                       );
                dm = this.DbConnector.ExecuteDataTable();
                if (dm.Rows.Count > 0)
                {
                    
                    this.DbConnector.Attach("TY_P_HR_9CHFC596",
                                     "S", "M1",
                                     dm.Rows[0][0].ToString(),
                                     dm.Rows[0][1].ToString(),
                                     dt.Rows[i][1].ToString()
                                     );
                    
                }
            }
            this.DbConnector.ExecuteTranQueryList();


            this.DbConnector.CommandClear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // 소급 마스타 삭제
                this.DbConnector.Attach("TY_P_HR_5BHB6169",
                                        dt.Rows[i][0].ToString(),
                                        dt.Rows[i][1].ToString()
                                        );

                // 소급 내역 삭제
                this.DbConnector.Attach("TY_P_HR_5BHB7170",
                                        dt.Rows[i][0].ToString(),
                                        dt.Rows[i][1].ToString()
                                        );

                // 소급 집계 삭제
                this.DbConnector.Attach("TY_P_HR_5BHB7171",
                                        dt.Rows[i][0].ToString(),
                                        dt.Rows[i][1].ToString()
                                        );
            }
            this.DbConnector.ExecuteTranQueryList();
             

            


            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_5BCDZ151.GetDataSourceInclude(TSpread.TActionType.Remove, "SMJIDATE", "SMSABUN");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // 급여파일 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_5BHB0172", dt.Rows[i]["SMJIDATE"].ToString(), dt.Rows[i]["SMSABUN"].ToString());
                DataTable dtPy = this.DbConnector.ExecuteDataTable();

                if (dtPy.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_HR_5BBHO142");
                    e.Successed = false;
                    return;
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

        #region Description : 그리드 이벤트
        private void FPS91_TY_S_HR_5BCDZ151_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYHRSB001I(this.FPS91_TY_S_HR_5BCDZ151.GetValue("SMJIDATE").ToString(), this.FPS91_TY_S_HR_5BCDZ151.GetValue("SMSABUN").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region Description : 출력 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sSMJJCD = string.Empty;

            if (this.CBO01_SMJJCD.GetValue().ToString() == "1")
            {
                sSMJJCD = "";
            }
            else if (this.CBO01_SMJJCD.GetValue().ToString() == "2")
            {
                sSMJJCD = "1A,1B,2A,2B";
            }
            else if (this.CBO01_SMJJCD.GetValue().ToString() == "3")
            {
                sSMJJCD = "2C,3A,3B,4A";
            }
            else if (this.CBO01_SMJJCD.GetValue().ToString() == "4")
            {
                sSMJJCD = "3C,3D,6C";
            }

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_HR_5C4E3270",
                sSMJJCD.ToString(),
                this.DTP01_GSTYYMM.GetValue().ToString(),
                this.DTP01_GEDYYMM.GetValue().ToString(),
                this.CBH01_SMSABUN.GetValue().ToString(),
                sSMJJCD.ToString(),
                this.DTP01_GSTYYMM.GetValue().ToString(),
                this.DTP01_GEDYYMM.GetValue().ToString(),
                this.CBH01_SMSABUN.GetValue().ToString()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                ActiveReport rpt = new TYHRSB001R();
                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
        }
        #endregion
    }
}