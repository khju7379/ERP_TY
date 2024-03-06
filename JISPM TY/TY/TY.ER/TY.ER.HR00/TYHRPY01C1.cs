using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 호봉복사관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.08.27 18:03
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
    ///  TY_M_AC_27J81133 : 복사 하시겠습니까?
    ///  TY_M_AC_27J83134 : 복사가 완료되었습니다
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  COPY : 복사
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRPY01C1 : TYBase
    {
        private DataTable fdt;

        #region  Description : 폼로드 이벤트
        public TYHRPY01C1(DataTable dt)
        {
            InitializeComponent();

            fdt = dt;
        }

        private void TYHRPY01C1_Load(object sender, System.EventArgs e)
        {
            this.BTN61_COPY.ProcessCheck += new TButton.CheckHandler(BTN61_COPY_ProcessCheck);

            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
        }
        #endregion

        #region  Description : 종료 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region  Description : 복사 버튼 이벤트
        private void BTN61_COPY_Click(object sender, EventArgs e)
        {
            string sNEWDATE = string.Empty;

            sNEWDATE = DTP01_SDATE.GetString().ToString();

            this.DbConnector.CommandClear();
            for (int i = 0; i < fdt.Rows.Count; i++)
            {
                //호봉마스타
                this.DbConnector.Attach("TY_P_HR_58S8M778", sNEWDATE, TYUserInfo.EmpNo, fdt.Rows[i]["HBMJKCD"].ToString(), fdt.Rows[i]["HBMSDATE"].ToString().Replace("-","").Trim());
                //호봉내역
                this.DbConnector.Attach("TY_P_HR_58S8M779", sNEWDATE, TYUserInfo.EmpNo, fdt.Rows[i]["HBMJKCD"].ToString(), fdt.Rows[i]["HBMSDATE"].ToString().Replace("-","").Trim());
                
            }
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_AC_27J83134");
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region  Description : 복사 체크
        private void BTN61_COPY_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sNEWDATE = string.Empty;

            sNEWDATE = DTP01_SDATE.GetString().ToString();

            if (fdt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_AC_25F59464");
                e.Successed = false;
                return;
            }

            for (int i = 0; i < fdt.Rows.Count; i++)
            {
                if (Convert.ToInt32(sNEWDATE) <= Convert.ToInt32(fdt.Rows[i]["HBMEDATE"].ToString()))
                {
                    this.ShowCustomMessage("시작일자는 선택한 자료의 종료일자보다 커야 합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_HR_58SB4780",
                                       fdt.Rows[i]["HBMJKCD"].ToString(),
                                       sNEWDATE,
                                       fdt.Rows[i]["HBMJKCD"].ToString(),
                                       sNEWDATE
                                       );
                Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());
                if ( iCnt > 0)
                {
                    this.ShowCustomMessage("시작일자이후 호봉자료가 존재합니다! 복사할수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_AC_27J81133"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion
    }
}
