using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 개인급여관리 종료일자 일괄등록 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.03.09 15:06
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
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV_BATCH : 일괄등록
    ///  EDATE : 종료일자
    /// </summary>
    public partial class TYHRPY05C2 : TYBase
    {
        public string fsEndDate = string.Empty;

        public TYHRPY05C2()
        {
            InitializeComponent();
        }

        private void TYHRPY05C2_Load(object sender, System.EventArgs e)
        {
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.SetStartingFocus(this.DTP01_EDATE);
        }

        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTN61_SAV_BATCH_Click(object sender, EventArgs e)
        {
            fsEndDate = DTP01_EDATE.GetString();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
