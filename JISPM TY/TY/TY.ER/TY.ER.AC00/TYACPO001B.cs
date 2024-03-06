using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 품목별매출현황 생성 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.07.10 15:27
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
    ///  TY_M_GB_26E2Z874 : 생성하시겠습니까?
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    ///  TY_M_GB_26E31876 : 생성 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACPO001B : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYACPO001B()
        {
            InitializeComponent();
            this.SetPopupStyle(); 
        }

        private void TYACPO001B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_GSTYYMM.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"));

            this.SetStartingFocus(this.DTP01_GSTYYMM);
        }
        #endregion

        #region  Description : 생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sGUBUN  = string.Empty;
            string sOUTMSG = string.Empty;

            if (this.CBO01_GGUBUN.GetValue().ToString() == "CREATE")
            {
                sGUBUN = "A";
            }
            else
            {
                sGUBUN = "D";
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_37NB2214",
                sGUBUN.ToString(),
                this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6),
                sOUTMSG.ToString()
                );

            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUTMSG.ToString().Substring(0, 1) == "I")
            {
                this.ShowMessage("TY_M_GB_26E30875");
            }
            else
            {
                this.ShowMessage("TY_M_GB_26E31876");
            }
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 처리 체크
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_GB_26E2Z874"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion
    }
}
