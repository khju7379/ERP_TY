using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 년 이월 작업 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2014.01.14 20:05
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_41E5F100 : 년 이월 작업(SP)
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2B9AE222 : 이월작업이 완료되었습니다!
    ///  TY_M_AC_2B9AE223 : 이월작업을 하시겠습니까?
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  H1DATE : 거래일자
    /// </summary>
    public partial class TYACBJ033B : TYBase
    {
        public TYACBJ033B()
        {
            InitializeComponent();
        }

        #region  Description : 폼 로드 이벤트
        private void TYACBJ033B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_H1DATE.SetValue(DateTime.Now.ToString("yyyy"));

            this.SetStartingFocus(this.DTP01_H1DATE); 
        }
        #endregion

        #region  Description :  처리 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sOUTMSG = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_41E5F100", this.DTP01_H1DATE.GetString().ToString().Substring(0, 4), sOUTMSG);
            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar()); // SP의 OUTPUT 값 가져오는 부분

            if (sOUTMSG.ToString().Substring(0, 2) == "OK")
            {
                this.ShowMessage("TY_M_AC_2B9AE222");
            }
            else
            {
                this.ShowMessage("TY_M_AC_2B9AE222");
            };

         }
        #endregion

        #region Description : 처리 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_AC_2B9AE223"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
