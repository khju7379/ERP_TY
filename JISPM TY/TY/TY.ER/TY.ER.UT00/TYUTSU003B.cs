using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.UT00
{
    /// <summary>
    /// 승호생성 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2015.01.30 16:53
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_522E0249 : 승호생성 인사기본사항 조회
    ///  TY_P_HR_522ED250 : 승호파일 등록
    ///  TY_P_HR_522EE251 : 승호파일 삭제
    ///  TY_P_HR_522EF252 : 승호파일 기준년월 이상 존재 유무
    ///  TY_P_HR_522EG253 : 승호파일 발련번호 존재 유무
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2CDB0166 : 취소 하시겠습니까?
    ///  TY_M_AC_2CDB1167 : 취소 되었습니다!
    ///  TY_M_AC_2CDB1168 : 취소 작업에 실패했습니다!
    ///  TY_M_GB_26E2Z874 : 생성하시겠습니까?
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    ///  TY_M_GB_26E31876 : 생성 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  GGUBUN : 구분
    ///  YYYYMM : 기준 년월
    /// </summary>
    public partial class TYUTSU003B : TYBase
    {
        public string fsYYDATE  = string.Empty;
        public string fsYYSABUN = string.Empty;

        #region Description : 폼 로드
        public TYUTSU003B()
        {
            InitializeComponent();
        }

        private void TYUTSU003B_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM"));

            this.DTP01_SDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM"));

            this.FPS91_TY_S_UT_84OGK878.Initialize();
            this.FPS91_TY_S_UT_84OGL880.Initialize();

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 처리 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            try
            {
                string sOUT_MSG = string.Empty;

                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_6ALDE475",
                                        Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                                        Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                                        this.CBO01_SPGUBUN.GetValue().ToString(),
                                        sOUT_MSG.ToString()
                                        );

                sOUT_MSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                if (sOUT_MSG.Substring(0, 2).ToString() == "OK")
                {
                    this.ShowMessage("TY_M_UT_6ALDE476");
                }
                else
                {
                    this.ShowMessage("TY_M_UT_6ALDE477");
                }
            }
            catch
            {
            }
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.FPS91_TY_S_UT_84OGK878.Initialize();
            this.FPS91_TY_S_UT_84OGL880.Initialize();


            string sDate = this.DTP01_SDATE.GetValue().ToString().Substring(0, 4) + "-" + this.DTP01_SDATE.GetValue().ToString().Substring(4, 2) + "-01";
            
            DateTime dDate = Convert.ToDateTime(sDate).AddMonths(-1);


            groupBox5.Text = Convert.ToString(dDate).ToString().Replace("-", "").Substring(0, 4) + "년" + Convert.ToString(dDate).ToString().Replace("-", "").Substring(4, 2) + "월";

            // 전월 미수 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_84OGH877", Convert.ToString(dDate).ToString().Replace("-", "").Substring(0,6));
            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_84OGK878.SetValue(dt);


            groupBox6.Text = this.DTP01_SDATE.GetValue().ToString().Substring(0, 4) + "년" + this.DTP01_SDATE.GetValue().ToString().Substring(4, 2) + "월";

            // 당월 미수 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_84OGH877", this.DTP01_SDATE.GetValue().ToString());
            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_84OGL880.SetValue(dt);
        }
        #endregion
    }
}