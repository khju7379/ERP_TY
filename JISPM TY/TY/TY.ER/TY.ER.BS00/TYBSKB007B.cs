using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.BS00
{
    /// <summary>
    /// 관리공통비 배부관리 복사 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.08.08 09:52
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_788D5387 : 관리공통비 배부관리 복사(사업계획)
    ///  TY_P_AC_788D6388 : 관리공통비 배부관리 복사체크(사업계획)
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
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYBSKB007B : TYBase
    {
        #region Description : 폼 로드
        public TYBSKB007B()
        {
            InitializeComponent();
        }

        private void TYBSKB007B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_STDATE.SetValue(DateTime.Now.ToString("yyyy"));
            this.DTP01_EDDATE.SetValue(DateTime.Now.AddYears(+1).ToString("yyyy"));

            this.SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            //삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_78LI4471", this.DTP01_EDDATE.GetString().ToString().Substring(0, 4));
            this.DbConnector.ExecuteNonQueryList();

            //복사
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_788D5387", this.DTP01_EDDATE.GetString().ToString().Substring(0, 4),
                                                        TYUserInfo.EmpNo,
                                                        this.DTP01_STDATE.GetString().ToString().Substring(0, 4));
            this.DbConnector.ExecuteTranQueryList();
            this.ShowMessage("TY_M_AC_27J83134");
        }
        #endregion

        #region Description : 처리 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (Convert.ToInt32(this.DTP01_STDATE.GetString()) > Convert.ToInt32(this.DTP01_EDDATE.GetString()))
            {
                this.ShowCustomMessage("기준년도가 복사년도보다 큽니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                e.Successed = false;
                return;
            }
            
            // 기준정보 등록 확인
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_77JBA209", this.DTP01_EDDATE.GetString().ToString().Substring(0, 4),
                                                        "CM");
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowCustomMessage("[" + this.DTP01_EDDATE.GetString().ToString().Substring(0, 4) + "]년도 기준정보가 존재하지 않습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                e.Successed = false;
                return;
            }
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_77JBA209", this.DTP01_EDDATE.GetString().ToString().Substring(0, 4),
                                                        "UN");
            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowCustomMessage("[" + this.DTP01_EDDATE.GetString().ToString().Substring(0, 4) + "]년도 기준정보가 존재하지 않습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                e.Successed = false;
                return;
            }

            if (UP_PBUPCheck(this.DTP01_EDDATE.GetString().ToString().Substring(0, 4)) == false)
            {
                this.ShowCustomMessage("[" + this.DTP01_EDDATE.GetString().ToString().Substring(0, 4) + "]년도 사업계획 자료가 존재합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_AC_27J81133"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 사업계획-영업계획 등록 체크
        private bool UP_PBUPCheck(string sYEAR)
        {
            bool bRtn = true;
            DataTable dt = new DataTable();

            // 공통, 자제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach(
                               "TY_P_AC_78IAW449", sYEAR);

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                bRtn = false;
            }
            // 매출액,취급량
            this.DbConnector.CommandClear();
            this.DbConnector.Attach(
                               "TY_P_AC_78IAX450", sYEAR);

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                bRtn = false;
            }
            // 투자,수선
            this.DbConnector.CommandClear();
            this.DbConnector.Attach(
                               "TY_P_AC_78IAY451", sYEAR);

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                bRtn = false;
            }

            return bRtn;
        }
        #endregion
    }
}
