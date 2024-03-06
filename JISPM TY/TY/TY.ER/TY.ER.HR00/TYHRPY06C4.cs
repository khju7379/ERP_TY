using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 급여대상자관리 지급율 일괄등록 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.03.06 13:01
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
    ///  TY_M_HR_536D4528 : 적용하시겠습니까?
    ///  TY_M_HR_536D4529 : 적용 되었습니다!
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV_BATCH : 일괄등록
    ///  PTPAYRATE : 지급율
    /// </summary>
    public partial class TYHRPY06C4 : TYBase
    {
        public Int16 fiPayRate;

        public TYHRPY06C4()
        {
            InitializeComponent();
        }

        private void TYHRPY06C4_Load(object sender, System.EventArgs e)
        {
            this.TXT01_PTPAYRATE.SetValue("0");
            this.SetStartingFocus(this.TXT01_PTPAYRATE);
        }

        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTN61_SAV_BATCH_Click(object sender, EventArgs e)
        {
            fiPayRate = Convert.ToInt16(this.TXT01_PTPAYRATE.GetValue().ToString());
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
