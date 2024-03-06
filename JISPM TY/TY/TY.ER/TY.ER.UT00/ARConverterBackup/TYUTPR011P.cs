using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using DataDynamics.ActiveReports;

namespace TY.ER.UT00
{
    /// <summary>
    /// 월별 탱크별 입고현황 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.04.07 15:59
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_74BGJ239 : 월별 탱크별 입고현황 출력
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  CHHWAJU : 화주
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYUTPR011P : TYBase
    {
        #region Description : 폼 로드
        public TYUTPR011P()
        {
            InitializeComponent();
        }

        private void TYUTPR011P_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_7CMDK353.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            // 대표거래처 코드 가져오기
            string sHWAJU = Get_VNCODE(this.CBH01_CHHWAJU.GetValue().ToString());

            this.DbConnector.Attach("TY_P_UT_74BGJ239", this.DTP01_STDATE.GetString(),
                                                        this.DTP01_EDDATE.GetString(),
                                                        sHWAJU,
                                                        this.CBH01_SHWAMUL.GetValue().ToString(),
                                                        this.TXT01_SVTANKNO.GetValue().ToString(),
                                                        this.CBO01_GGUBUN.GetValue().ToString()
                                                        );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_7CMDK353.SetValue(dt);
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            // 대표거래처 코드 가져오기
            string sHWAJU = Get_VNCODE(this.CBH01_CHHWAJU.GetValue().ToString());

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_74BGJ239", this.DTP01_STDATE.GetString(),
                                                        this.DTP01_EDDATE.GetString(),
                                                        sHWAJU,
                                                        this.CBH01_SHWAMUL.GetValue().ToString(),
                                                        this.TXT01_SVTANKNO.GetValue().ToString(),
                                                        this.CBO01_GGUBUN.GetValue().ToString()
                                                        );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                ActiveReport rpt = new TYUTPR011R();
                // 가로 출력
                rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion
    }
}
