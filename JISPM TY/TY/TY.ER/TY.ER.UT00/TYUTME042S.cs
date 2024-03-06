using System;
using System.Data;
using System.Drawing;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.UT00
{
    /// <summary>
    /// 거래처별 매출현황 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.10.17 17:32
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_699DO131 : 거래처별 매출현황 출력(화주X)
    ///  TY_P_UT_699DS132 : 거래처별 매출현황 출력(화주O)
    ///  TY_P_UT_6AHHZ392 : 거래처별 매출현황 임시파일 삭제
    ///  TY_P_UT_6AHI0398 : 거래처별 매출현황 보관 취급료 조회
    ///  TY_P_UT_6AHI4393 : 거래처별 매출현황 임시파일 생성(화주O)
    ///  TY_P_UT_6AHI5395 : 거래처별 매출현황 임시파일 생성(화주X)
    ///  TY_P_UT_6AIA0401 : 거래처별 매출현황 임시파일 존재유무
    ///  TY_P_UT_6AIA8400 : 거래처별 매출현황 임시파일 수정(보관 취급료)
    ///  TY_P_UT_6AIAB402 : 거래처별 매출현황 임시파일 등록(보관 취급료)
    ///  TY_P_UT_6AIAD403 : 거래처별 매출현황 하역료 조회
    ///  TY_P_UT_6AIAE404 : 거래처별 매출현황 임시파일 수정(하역료)
    ///  TY_P_UT_6AIAH405 : 거래처별 매출현황 임시파일 등록(하역료)
    ///  TY_P_UT_6AIB4406 : 거래처별 매출현황 수정세금계산서 조회
    ///  TY_P_UT_6AIBA407 : 거래처별 매출현황 임시파일 수정(세금계산서)
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  CMHWAJU : 화주
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYUTME042S : TYBase
    {
        #region Description : 페이지 로드
        public TYUTME042S()
        {
            InitializeComponent();
        }

        private void TYUTME042S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Descriptoin : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.FPS91_TY_S_UT_C3EAY147.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_UT_C3EAY146", Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                                                        Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                                                        this.CBH01_SHWAJU.GetValue().ToString()
                                                        );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_C3EAY147.SetValue(dt);

            if (this.FPS91_TY_S_UT_C3EAY147.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_UT_C3EAY147.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_UT_C3EAY147.GetValue(i, "VNSANGHO").ToString() == "[합 계]")
                    {
                        // 특정 ROW 글자 크기 변경
                        this.FPS91_TY_S_UT_C3EAY147.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);

                        // 특정 칼럼 색깔 입히기
                        this.FPS91_TY_S_UT_C3EAY147.ActiveSheet.Rows[i].BackColor = Color.SkyBlue;
                    }
                }
            }
        }
        #endregion
    }
}
