using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.ER.GB00;

namespace TY.ER.US00
{
    /// <summary>
    /// BIN 별 작업현황 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2019.10.01 11:23
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_9A19B268 : BIN 별 작업현황 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_9A19B269 : BIN 별 작업현황 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  STDATE : 시작일자
    ///  BINO : BIN
    /// </summary>
    public partial class TYUSGA01C1 : TYBase
    {
        public string fsBTCHULDAT = string.Empty;
        public string fsBTTKNO = string.Empty;
        public string fsBTNUMBER = string.Empty;

        #region Description : 폼 로드
        public TYUSGA01C1()
        {
            InitializeComponent();
        }

        private void TYUSGA01C1_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            //this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.FPS91_TY_S_US_9A19B269.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9A19B268", 
                                    Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                                    this.TXT01_BINO.GetValue().ToString().Trim());
            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_9A19B269.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                this.SetFocus(FPS91_TY_S_US_9A19B269);
            }
            else
            {
                this.SetFocus(this.DTP01_STDATE);
            }
        }
        #endregion

        #region Description : 스프레드 선택 이벤트
        private void FPS91_TY_S_US_9A19B269_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            UP_Get_Data();
        }

        private void FPS91_TY_S_US_9A19B269_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                UP_Get_Data();
            }
        }
        #endregion

        #region Description : 데이터 선택
        private void UP_Get_Data()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9ANES414",
                                    Get_Date(this.DTP01_STDATE.GetValue().ToString()),              // 출고 일자
                                    this.FPS91_TY_S_US_9A19B269.GetValue("BTHWAJU").ToString(),     // 화주
                                    this.FPS91_TY_S_US_9A19B269.GetValue("BTBINNO").ToString(),     // BIN 번호
                                    this.FPS91_TY_S_US_9A19B269.GetValue("BTGOKJONG").ToString(),   // 곡종
                                    this.FPS91_TY_S_US_9A19B269.GetValue("BTWONSAN").ToString(),    // 원산지
                                    this.FPS91_TY_S_US_9A19B269.GetValue("BTNUMNM").ToString()      // 차량번호
                                    );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsBTCHULDAT = dt.Rows[0]["CHCHULDAT"].ToString();
                fsBTTKNO = dt.Rows[0]["CHTKNO"].ToString();
                fsBTNUMBER = dt.Rows[0]["CHNUMBER"].ToString();
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion
    }
}
