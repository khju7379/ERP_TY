using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
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
    ///  TY_S_HR_85NDD074 : 개인 급여 통합 조회(급여결과마스타)
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
    public partial class TYHRBS002S : TYBase
    {
        string fsGUBN = string.Empty;

        #region Description : 페이지 로드
        public TYHRBS002S()
        {
            InitializeComponent();

            this.CBH01_BPMDPAC.DummyValue = DateTime.Now.ToString("yyyyMMdd");
        }

        private void TYHRBS002S_Load(object sender, System.EventArgs e)
        {
            this.TXT01_BISYEAR.SetValue(DateTime.Now.ToString("yyyyMMdd").Substring(0, 4));

            this.SetStartingFocus(this.TXT01_BISYEAR);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_85NDD074.Initialize();

            string sJKCD  = string.Empty;
            string sBUSEO = string.Empty;

            if (this.CBO01_INQOPTION.GetValue().ToString() == "1") // 임원
            {
                sJKCD = "01";
            }
            else if (this.CBO01_INQOPTION.GetValue().ToString() == "2") // 연봉직
            {
                sJKCD = "1A,1B,2A,2B";
            }
            else if (this.CBO01_INQOPTION.GetValue().ToString() == "3") // 사무직
            {
                sJKCD = "3B,3A,2C,6C";
            }
            else if (this.CBO01_INQOPTION.GetValue().ToString() == "4") // 운영
            {
                sJKCD = "3C,3D";
            }

            if (this.CBH01_BPMDPAC.GetValue().ToString() != "")
            {
                sBUSEO = this.CBH01_BPMDPAC.GetValue().ToString().Substring(0, 2).ToString();
            }

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_85ND0073", sJKCD.ToString(),
                                                        this.TXT01_BISYEAR.GetValue().ToString(),
                                                        sBUSEO.ToString(),
                                                        this.CBH01_BISSABUN.GetValue().ToString()
                                                        );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_HR_85NDD074.SetValue(dt);

                for (int i = 0; i < this.FPS91_TY_S_HR_85NDD074.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_HR_85NDD074.GetValue(i, "GUBUN").ToString() == "HAP")
                    {
                        // 특정 칼럼 색깔 입히기
                        this.FPS91_TY_S_HR_85NDD074.ActiveSheet.Rows[i].BackColor = Color.SkyBlue;
                        this.FPS91_TY_S_HR_85NDD074.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);
                    }
                }
            }
        }
        #endregion

        #region Description : 생성 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            TYHRBS002B popup = new TYHRBS002B();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (popup.fsBISYEAR.ToString() != "")
                {
                    this.TXT01_BISYEAR.SetValue(popup.fsBISYEAR.ToString());

                    this.BTN61_INQ_Click(null, null);
                }
            }
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_HR_85NDD074_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.FPS91_TY_S_HR_85NDD074.GetValue("GUBUN").ToString() == "HAP")
            {
                ShowMessage("TY_M_MR_2BF8A365");
            }
            else
            {
                string sBISYEAR  = string.Empty;
                string sBISSEQ   = string.Empty;
                string sBISSABUN = string.Empty;

                sBISYEAR  = this.FPS91_TY_S_HR_85NDD074.GetValue("BISYEAR").ToString();
                sBISSEQ   = this.FPS91_TY_S_HR_85NDD074.GetValue("BISSEQ").ToString();
                sBISSABUN = this.FPS91_TY_S_HR_85NDD074.GetValue("BISSABUN").ToString();

                if ((new TYHRBS002I(sBISYEAR.ToString(), sBISSEQ.ToString(), sBISSABUN.ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion
    }
}
