using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.UT00
{
    /// <summary>
    /// 보관료 거래명세서 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.06.21 16:06
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_MR_2BF4Z352 : 처리 할 데이터가 없습니다.
    ///  TY_M_MR_2BF50353 : 처리하시겠습니까?
    ///  TY_M_MR_2BF50354 : 처리하였습니다.
    ///  TY_M_UT_71BDP399 : 처리 중 오류가 발생하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  CHHWAJU : 화주
    ///  CHHWAMUL : 화물
    ///  UTDATE : 매출일자
    /// </summary>
    public partial class TYUTME031P : TYBase
    {
        #region Description : 폼 로드
        public TYUTME031P()
        {
            InitializeComponent();
        }

        private void TYUTME031P_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            try
            {
                string sRET_MSG = string.Empty;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_7ADDA773", this.DTP01_STDATE.GetString(),
                                                            this.DTP01_EDDATE.GetString(),
                                                            this.CBH01_CHHWAJU.GetValue().ToString(),
                                                            this.CBH01_CHHWAMUL.GetValue().ToString(),
                                                            sRET_MSG
                                                            );
                sRET_MSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                this.ShowMessage("TY_M_MR_2BF50354");
            }
            catch
            {
                this.ShowMessage("TY_M_UT_71BDP399");
            }
        }
        #endregion

        #region Description : 처리 ProcessCheck
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_MR_2BF50353"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            try
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_7ADD4770", this.DTP01_STDATE.GetString().Substring(0, 6),
                                                            this.DTP01_EDDATE.GetString().Substring(0, 6),
                                                            this.CBH01_CHHWAJU.GetValue().ToString()
                                                            );

                DataTable dt = this.DbConnector.ExecuteDataTable();


                if (dt.Rows.Count > 0)
                {
                    SectionReport rpt = new TYUTME031R();

                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait;
                    (new TYERGB001P(rpt, dt)).ShowDialog();
                }
                else
                {
                    this.ShowMessage("TY_M_AC_2422N250");
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
