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
    /// 미수금 현황 출력 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.10.20 17:17
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_66FD4200 : 대표 거래처 코드 조회
    ///  TY_P_UT_6AKH0466 : 미수금 현황 출력(화주O)
    ///  TY_P_UT_6AKH0467 : 미수금 현황 출력(화주X)
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
    public partial class TYUTSU005S : TYBase
    {
        #region Description : 페이지 로드
        public TYUTSU005S()
        {
            InitializeComponent();
        }

        private void TYUTSU005S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_SDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM"));

            this.FPS91_TY_S_UT_84OG0873.Initialize();

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.DTP01_SDATE);
        }
        #endregion

        #region Desription : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.FPS91_TY_S_UT_84OG0873.Initialize();


            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_84OFZ872", this.DTP01_SDATE.GetValue().ToString(),
                                                        this.CBH01_SHWAJU.GetValue().ToString());
            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_84OG0873.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_UT_84OG0873, "VNSANGHO", "합  계", SumRowType.Sum, "MIJUNAMT", "MIDANGAMT", "MIDANGVAT", "MIIPAMT", "MIMISUAMT");
            }
        }
        #endregion

    }
}
