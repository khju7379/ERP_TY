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
    /// 퇴충금명세서 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.02.20 13:28
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
    ///  INQ : 조회
    ///  NEW : 신규
    /// </summary>
    public partial class TYHRKB018S : TYBase
    {      

        #region  Description : 폼 로드 이벤트
        public TYHRKB018S()
        {
            InitializeComponent();                       
        }

        private void TYHRKB018S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);                      
            
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            
            FPS91_TY_S_HR_84NFD859.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_84NFC857", CBH01_TOYEAR.GetValue().ToString(), CBO01_INQOPTION.GetValue().ToString(), TYUserInfo.SecureKey, "Y" );
            FPS91_TY_S_HR_84NFD859.SetValue(this.DbConnector.ExecuteDataTable());

            if (FPS91_TY_S_HR_84NFD859.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_HR_84NFD859, "KBBUSEONM", "합  계", SumRowType.Sum, "TLCOMTOTAL");
            }
        }
        #endregion

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {        
            TYHRKB018I popup = new TYHRKB018I();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.CBH01_TOYEAR.SetValue(popup.fsTOYEARNUM);   // 자산년도
                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region  Description : 생성 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {            

            if ((new TYHRKB018B(CBH01_TOYEAR.GetValue().ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);

        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            if (!this.ShowMessage("TY_M_GB_26E2Z874"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : FPS91_TY_S_HR_84NFD859_CellDoubleClick 이벤트
        private void FPS91_TY_S_HR_84NFD859_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYHRKB018P(this.FPS91_TY_S_HR_84NFD859.GetValue("TLYEAR").ToString(), this.FPS91_TY_S_HR_84NFD859.GetValue("TLSEQ").ToString(), this.FPS91_TY_S_HR_84NFD859.GetValue("TLSABUN").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

    }
}
