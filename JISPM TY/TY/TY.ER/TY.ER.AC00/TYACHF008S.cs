using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 고정자산 월상각 조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2013.02.01 20:38
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_3213S994 : 고정자산 월상각 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_3213X997 : 고정자산 월상각 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  FXMYYMM : 상각년월
    /// </summary>
    public partial class TYACHF008S : TYBase
    {
        #region Description : 페이지 로드
        public TYACHF008S()
        {
            InitializeComponent();
        }

        private void TYACHF008S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_INQ_FXL.Image = global::TY.Service.Library.Properties.Resources.magnifier;

            this.SetStartingFocus(this.TXT01_FXMYYMM);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sFXLNUM    = string.Empty;
            string sFXLYEAR   = string.Empty;
            string sFXLSEQ    = string.Empty;
            string sFXLSUBNUM = string.Empty;
            
            sFXLYEAR = this.TXT03_FXLYEAR.GetValue().ToString();

            if (this.TXT03_FXLSEQ.GetValue().ToString().Trim() != "")
            {
                sFXLSEQ = Set_Fill4(this.TXT03_FXLSEQ.GetValue().ToString());
            }
            else
            {
                sFXLSEQ = "";
            }

            if (this.TXT03_FXLSUBNUM.GetValue().ToString().Trim() != "")
            {
                sFXLSUBNUM = Set_Fill3(this.TXT03_FXLSUBNUM.GetValue().ToString());
            }
            else
            {
                sFXLSUBNUM = "";
            }

            sFXLNUM = sFXLYEAR + sFXLSEQ + sFXLSUBNUM;

            this.FPS91_TY_S_AC_3213X997.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_3213S994", this.TXT01_FXMYYMM.GetValue(), sFXLNUM.ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }

            this.FPS91_TY_S_AC_3213X997.SetValue(dt);

            this.SetFocus(this.TXT01_FXMYYMM);
        }
        #endregion

        #region Description : 자산번호 버튼
        private void BTN61_INQ_FXL_Click(object sender, EventArgs e)
        {
            TYAZHF05C1 popup = new TYAZHF05C1();
            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT03_FXLYEAR.SetValue(popup.fsASNUM.Substring(0, 4));
                this.TXT03_FXLSEQ.SetValue(popup.fsASNUM.Substring(5, 4));
                this.TXT03_FXLSUBNUM.SetValue(popup.fsASNUM.Substring(10, 3));
            }
        }
        #endregion
    }
}