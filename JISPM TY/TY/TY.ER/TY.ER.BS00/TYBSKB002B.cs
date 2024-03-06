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
    /// 영업외손익 항목코드 복사 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.06.16 15:45
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_76GAF829 : 영업외손익 항목코드 복사
    ///  TY_P_AC_76GAG831 : 영업외손익 항목코드 삭제(년도별)
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_27J81133 : 복사 하시겠습니까?
    ///  TY_M_AC_27J83134 : 복사가 완료되었습니다
    ///  TY_M_MR_2BF4Z352 : 처리 할 데이터가 없습니다.
    ///  TY_M_MR_2BF50353 : 처리하시겠습니까?
    ///  TY_M_MR_2BF50354 : 처리하였습니다.
    ///  TY_M_UT_71BDP399 : 처리 중 오류가 발생하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYBSKB002B : TYBase
    {
        #region Description : 폼 로드
        public TYBSKB002B()
        {
            InitializeComponent();
        }

        private void TYBSKB002B_Load(object sender, System.EventArgs e)
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
            try
            {
                this.DbConnector.CommandClear();

                //복사
                this.DbConnector.Attach("TY_P_AC_76GAF829", this.DTP01_EDDATE.GetString().ToString().Substring(0, 4),
                                                            TYUserInfo.EmpNo,
                                                            this.DTP01_STDATE.GetString().ToString().Substring(0, 4));
                this.DbConnector.ExecuteTranQueryList();

                this.ShowMessage("TY_M_AC_27J83134");
            }
            catch
            {
            }
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

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_76GAG831", this.DTP01_EDDATE.GetString().ToString().Substring(0, 4));
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowCustomMessage("[" + this.DTP01_EDDATE.GetString().ToString().Substring(0, 4) + "]년도 자료가 존재합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

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
    }
}
