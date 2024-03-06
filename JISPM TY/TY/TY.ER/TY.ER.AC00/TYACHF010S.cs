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
    /// 유형자산 증감관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2013.05.23 18:14
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_35NBC718 : 유형자산 증감관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_35NBE719 : 유형자산 증감관리 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  FXDEMDATE : 마감일자
    ///  FXSFIXNUM : 자산번호
    /// </summary>
    public partial class TYACHF010S : TYBase
    {
        public TYACHF010S()
        {
            InitializeComponent();
        }

        #region Description : Page_Load
        private void TYACHF010S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_INQ_FXL.Image = global::TY.Service.Library.Properties.Resources.magnifier;

            this.CBH01_FXSAUP.DummyValue = "19900101";

            SetStartingFocus(this.TXT01_FXLYEAR);

            //this.DTP01_FXDEMDATE.Visible = false;
            //this.LBL51_FXDEMDATE.Visible = false;

            // 마스터 스프레드 조회
            this.BTN61_INQ_Click(null, null);
                     
        }
        #endregion

        #region Description : 조회
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_35NBC718",
                 this.TXT01_FXLYEAR.GetValue().ToString().Trim() + this.TXT01_FXLSEQ.GetValue().ToString().Trim() + this.TXT01_FXLSUBNUM.GetValue().ToString().Trim(),
                 this.CBH01_FXMLASCODE.GetValue().ToString().Trim(),
                 this.TXT01_FXSNAME.GetValue().ToString().Trim(),
                 this.CBH01_FXSAUP.GetValue().ToString().Trim(),
                 ""
                );

            this.FPS91_TY_S_AC_35NBE719.SetValue(this.DbConnector.ExecuteDataTable());

        } 
        #endregion

        private void BTN61_INQ_FXL_Click(object sender, EventArgs e)
        {
            TYAZHF05C1 popup = new TYAZHF05C1();
            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_FXLYEAR.SetValue(popup.fsASNUM.Substring(0, 4));
                this.TXT01_FXLSEQ.SetValue(popup.fsASNUM.Substring(5, 4));
                this.TXT01_FXLSUBNUM.SetValue(popup.fsASNUM.Substring(10, 3));
            }
        }


    }
}
