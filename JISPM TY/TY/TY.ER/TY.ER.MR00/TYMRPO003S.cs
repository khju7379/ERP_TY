using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.MR00
{
    /// <summary>
    /// 구매발주 승인내역 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2013.01.18 17:41
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_31I44822 : 구매발주 승인내역 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_31I45823 : 구매발주 승인내역 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_2BF7Y364 : 조회가 완료되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  GCDDP : 사업장코드
    ///  FXAPPGUBN : 승인유무
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자 
    /// </summary>
    public partial class TYMRPO003S : TYBase
    {
        #region Description : 페이지 로드
        public TYMRPO003S()
        {
            InitializeComponent();
        }

        private void TYMRPO003S_Load(object sender, System.EventArgs e)
        {
            DTP01_GSTDATE.SetValue(DateTime.Now.ToString("yyyyMM") + "01");
            DTP01_GEDDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));
            UP_SET_BUSEO();
            SetStartingFocus(this.DTP01_GSTDATE);
        }
        #endregion.

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
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

            this.FPS91_TY_S_MR_31I45823.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach(
            "TY_P_MR_31I44822",
            this.DTP01_GSTDATE.GetValue().ToString(),
            this.DTP01_GEDDATE.GetValue().ToString(),
            sCDDP.ToString(),
            this.CBO01_FXAPPGUBN.GetValue().ToString(),
            this.CBO01_FXAPPGUBN.GetValue().ToString(),
            this.CBO01_FXAPPGUBN.GetValue().ToString()
            );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_MR_31I45823.SetValue(dt);
            }
            this.ShowMessage("TY_M_GB_2BF7Y364");
        }
        #endregion.

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
