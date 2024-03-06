using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 연말정산 연금.저축 등 소득.세액 명세서 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.12.05 18:02
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_7C5I4192 : 연말정산 연금.저축등 소득.세액 명세 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_7C5IA193 : 연말정산 연금.저축등 소득.세액 명세 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  ANTYPECODE : 소득공제구분코드
    ///  KBSABUN : 사번
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRNT010S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRNT010S()
        {
            InitializeComponent();
        }

        private void TYHRNT010S_Load(object sender, System.EventArgs e)
        {
            this.TXT01_SDATE.SetValue(DateTime.Now.ToString("yyyy"));
                       

            this.SetStartingFocus(TXT01_SDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_7C5IA193.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_7C5I4192", "TY", TXT01_SDATE.GetValue(), CBH01_KBSABUN.GetValue(), CBH01_ANTYPECODE.GetValue());
            this.FPS91_TY_S_HR_7C5IA193.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_HR_7C5IA193.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_HR_7C5IA193, "ANSABUNNM", "합   계", SumRowType.Sum, "ANPAYAMOUNT", "ANDEDAMOUNT");
            }

        }
        #endregion
    }
}
