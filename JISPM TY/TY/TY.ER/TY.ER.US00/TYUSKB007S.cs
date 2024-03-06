using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.US00
{
    /// <summary>
    /// 재고관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.02.15 14:06
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_92SDV949 : 모선별 재고관리 조회
    ///  TY_P_US_92SDV950 : 화주별 재고관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_92SDW952 : 모선별 재고관리 조회
    ///  TY_S_US_92SDZ953 : 화주별 재고관리 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  JGGOKJONG : 곡종
    ///  JGHANGCHA : 항차
    ///  JGHWAJU : 화 주
    /// </summary>
    public partial class TYUSKB007S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYUSKB007S()
        {
            InitializeComponent();
        }

        private void TYUSKB007S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_INQ.ProcessCheck += new TButton.CheckHandler(BTN61_INQ_ProcessCheck);

            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.SetStartingFocus(CBH01_STHANGCHA.CodeText);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            //모선별
            FPS91_TY_S_US_92SDW952.Initialize();
            //if (this.CBH01_STHANGCHA.GetValue().ToString() != "")
            //{
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92SDV949", this.DTP01_SDATE.GetString().ToString(),
                                                            this.CBH01_STHANGCHA.GetValue().ToString(),
                                                            this.CBH01_EDHANGCHA.GetValue().ToString(),
                                                            this.CBH01_JGHWAJU.GetValue(),
                                                            this.CBH01_JGGOKJONG.GetValue()
                                                            );

                this.FPS91_TY_S_US_92SDW952.SetValue(this.DbConnector.ExecuteDataTable());
            //}

            ////화주별
            FPS91_TY_S_US_92SDZ953.Initialize();
            //if (this.CBH01_JGHWAJU.GetValue().ToString() != "")
            //{
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92SDV950", this.DTP01_SDATE.GetString().ToString(),
                                                            this.CBH01_STHANGCHA.GetValue().ToString(),
                                                            this.CBH01_EDHANGCHA.GetValue().ToString(),
                                                            this.CBH01_JGHWAJU.GetValue(),
                                                            this.CBH01_JGGOKJONG.GetValue()
                                                            );

                this.FPS91_TY_S_US_92SDZ953.SetValue(this.DbConnector.ExecuteDataTable());
            //}
        }

        private void BTN61_INQ_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.CBH01_STHANGCHA.GetValue().ToString() == "" && this.CBH01_JGHWAJU.GetValue().ToString() == "" && this.CBH01_JGGOKJONG.GetValue().ToString() == "")
            {
                this.ShowCustomMessage("한가지 조건은 반드시 입력해야 합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return; 
            }
        }
        #endregion
    }
}
