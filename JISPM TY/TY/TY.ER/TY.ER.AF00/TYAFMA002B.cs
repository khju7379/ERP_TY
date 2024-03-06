using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AF00
{
    /// <summary>
    /// EIS 계열사 인원주주현황 복사 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.07.24 11:33
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_37OBQ232 : EIS 계열사 임원현황 복사
    ///  TY_P_AC_37OBT233 : EIS 계열사 주주현황 복사
    ///  TY_P_AC_37OBT234 : EIS 계열사 주주현황 전체삭제
    ///  TY_P_AC_37OBU235 : EIS 계열사 임원현황 전체삭제
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_27J81133 : 복사 하시겠습니까?
    ///  TY_M_AC_27J83134 : 복사가 완료되었습니다
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYAFMA002B : TYBase
    {
        private string fsEFSUBGN = string.Empty;

        #region  Description : 폼 로드 이벤트
        public TYAFMA002B(string sEFSUBGN)
        {
            InitializeComponent();
            this.SetPopupStyle();

            fsEFSUBGN = sEFSUBGN.ToString();
        }

        private void TYAFMA002B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_GEDYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.SetStartingFocus(this.DTP01_GEDYYMM);
        }
        #endregion

        #region  Description : 복사 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sYYMM  = string.Empty;
            string sYEAR  = string.Empty;
            string sMONTH = string.Empty;

            if (this.DTP01_GEDYYMM.GetString().ToString().Substring(4, 2) == "01")
            {
                sYEAR  = Convert.ToString(int.Parse(this.DTP01_GEDYYMM.GetString().ToString().Substring(0, 4)) - 1);
                sMONTH = "12";
            }
            else
            {
                sYEAR  = this.DTP01_GEDYYMM.GetString().ToString().Substring(0, 4);
                sMONTH = Set_Fill2(Convert.ToString(int.Parse(this.DTP01_GEDYYMM.GetString().ToString().Substring(4, 2)) - 1));
            }

            sYYMM = sYEAR + sMONTH;

            this.DbConnector.CommandClear();
            // 삭제
            this.DbConnector.Attach("TY_P_AC_3992G619", this.DTP01_GEDYYMM.GetString().ToString().Substring(0, 6), fsEFSUBGN.ToString()); // 주주현황
            this.DbConnector.Attach("TY_P_AC_3992H620", this.DTP01_GEDYYMM.GetString().ToString().Substring(0, 6), fsEFSUBGN.ToString()); // 임원현황
            this.DbConnector.Attach("TY_P_AC_3992K621", this.DTP01_GEDYYMM.GetString().ToString().Substring(0, 6), fsEFSUBGN.ToString()); // 임원 겸직 및 경력사항

            // 복사
            this.DbConnector.Attach("TY_P_AC_3992L622", this.DTP01_GEDYYMM.GetString().ToString().Substring(0, 6), sYYMM.ToString(), fsEFSUBGN.ToString()); // 임원현황
            this.DbConnector.Attach("TY_P_AC_3992M623", this.DTP01_GEDYYMM.GetString().ToString().Substring(0, 6), sYYMM.ToString(), fsEFSUBGN.ToString()); // 임원 겸직 및 경력사항
            this.DbConnector.Attach("TY_P_AC_3992M624", this.DTP01_GEDYYMM.GetString().ToString().Substring(0, 6), sYYMM.ToString(), fsEFSUBGN.ToString()); // 주주현황

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_AC_27J83134");
        }
        #endregion

        #region Description : 처리 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_AC_27J81133"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion
    }
}