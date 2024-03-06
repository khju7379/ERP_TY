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
    /// 코드박스-계약관리조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2019.05.05 00:59
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_95518504 : 계약관리 조회 (소속별)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_95510506 : 계약관리 조회(소속별)
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  VNSOSOK : 소속협회
    /// </summary>
    public partial class TYUSGB005S : TYBase
    {
        public string fsCONTNO = string.Empty;
        public string fsHWAJU  = string.Empty;
        public string fsDATE   = string.Empty;

        #region Description : 폼 로드
        public TYUSGB005S(string sDATE, string sHWAJU)
        {
            InitializeComponent();

            fsHWAJU = sHWAJU;
            fsDATE = sDATE;
        }

        private void TYUSGB005S_Load(object sender, System.EventArgs e)
        {
            this.CBH01_VNSOSOK.SetReadOnly(true);

            BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_US_95518504",
                                    fsDATE.ToString(),
                                    fsDATE.ToString(),
                                    fsHWAJU.ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_95510506.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                this.CBH01_VNSOSOK.SetValue(dt.Rows[0]["CNSOSOK"].ToString());
                SetFocus(FPS91_TY_S_US_95510506);
            }
            else
            {
                SetFocus(CBH01_VNSOSOK);
            }
        }
        #endregion

        #region Description : 스프레드 더블클릭
        private void FPS91_TY_S_US_95510506_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            UP_Get_Data();
        }
        #endregion

        private void FPS91_TY_S_US_95510506_KeyPress(object sender, KeyPressEventArgs e)
        {
            DataTable dt = (DataTable)FPS91_TY_S_US_95510506.DataSource;

            if (e.KeyChar == '\r' && dt.Rows.Count > 0)
            {
                UP_Get_Data();
            }
        }

        private void UP_Get_Data()
        {
            fsCONTNO = Set_Fill4(this.FPS91_TY_S_US_95510506.GetValue("CNYEAR").ToString()) + Set_Fill2(this.FPS91_TY_S_US_95510506.GetValue("CNSEQ").ToString());

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
    }
}
