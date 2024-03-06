using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using DataDynamics.ActiveReports;
using TY.ER.MR00;
using TY.ER.GB00;

namespace TY.ER.MR00
{
    /// <summary>
    /// 구매발주 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2012.05.30 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_31A4H603 : 구매발주서 출력
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_31A4W608 : 구매발주서 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  EPMYYMM : 년월
    ///  POM1000 : 사업부
    ///  VNSANGHO : 거래처명
    /// </summary>
    public partial class TYMRPO002S : TYBase
    {
        #region Description : 페이지 로드
        public TYMRPO002S()
        {
            InitializeComponent();

        }

        private void TYMRPO002S_Load(object sender, System.EventArgs e)
        {
            DTP01_GSTYYMM.SetValue(DateTime.Now.ToString("yyyyMM") + "01");
            DTP01_GEDYYMM.SetValue(DateTime.Now.ToString("yyyyMMdd"));
            UP_SET_BUSEO();
            //this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.DTP01_GSTYYMM);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            //if (this.CBH01_GCDDP.GetValue().ToString() == "")
            //{
            //    this.ShowMessage("TY_M_AC_29I6V182");
            //    return;
            //}

            string sCDDP = string.Empty;

            if (this.CBH01_GCDDP.GetValue().ToString() != "")
            {
                if (this.CBH01_GCDDP.GetValue().ToString().Substring(1, 5) == "00000")
                {
                    sCDDP = this.CBH01_GCDDP.GetValue().ToString().Substring(0, 1);
                }
                else if (this.CBH01_GCDDP.GetValue().ToString().Substring(2, 4) == "0000")
                {
                    sCDDP = this.CBH01_GCDDP.GetValue().ToString().Substring(0, 2);
                }
                else
                {
                    sCDDP = this.CBH01_GCDDP.GetValue().ToString();
                }
            }
            else
            {
                sCDDP = "";
            }

            this.FPS91_TY_S_MR_31E9S672.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_MR_31E9P670",
                this.CBO01_GGUBUN.GetValue().ToString(),
                this.DTP01_GSTYYMM.GetValue().ToString(),
                this.DTP01_GEDYYMM.GetValue().ToString(),
                sCDDP.ToString(),
                this.CBH01_PON1100.GetValue().ToString(),
                this.CBO01_GGUBUN.GetValue().ToString(),
                this.CBO01_GGUBUN.GetValue().ToString(),
                this.CBO01_GGUBUN.GetValue().ToString()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_MR_31E9S672.SetValue(dt);
            }
            this.ShowMessage("TY_M_GB_2BF7Y364");
        }
        #endregion

        #region Description : 스프레드 출력 버튼
        private void FPS91_TY_S_MR_31E9S672_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_MR_31EAL678",
                this.FPS91_TY_S_MR_31E9S672.GetValue("BALBUN").ToString().Substring(0, 1),
                this.FPS91_TY_S_MR_31E9S672.GetValue("BALBUN").ToString().Substring(2, 1),
                this.FPS91_TY_S_MR_31E9S672.GetValue("BALBUN").ToString().Substring(4, 6),
                this.FPS91_TY_S_MR_31E9S672.GetValue("BALBUN").ToString().Substring(11, 4),
                this.FPS91_TY_S_MR_31E9S672.GetValue("PON1100").ToString()
                );

            ActiveReport rpt = new TYMRPO002R(this.FPS91_TY_S_MR_31E9S672.GetValue("VNSANGHO").ToString());
            (new TYERGB001P(rpt, this.DbConnector.ExecuteDataTable())).ShowDialog();
        }
        #endregion

        #region Description : 부서코드 가져오기
        private void UP_SET_BUSEO()
        {
            // 부서코드
            this.CBH01_GCDDP.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BEBB293",
                DateTime.Now.ToString("yyyyMMdd"),
                TYUserInfo.EmpNo
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                // 부서코드
                this.CBH01_GCDDP.SetValue(dt.Rows[0]["KBBUSEO"].ToString());
            }
        }
        #endregion
    }
}