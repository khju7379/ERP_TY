using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.Service.Library.Controls.TYSpreadCellType;

namespace TY.ER.US00
{
    /// <summary>
    /// 거래처관리 등록 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.04.02 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2441H411 : 거래처 관리 삭제
    ///  TY_P_AC_244BN404 : 거래처관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_P_US_8BJHK186 : 거래처관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  VNCODE : 거래처코드
    ///  VNSANGHO : 상호
    /// </summary>
    public partial class TYUSME078S : TYBase
    {
        #region Description : 페이지 로드
        public TYUSME078S()
        {
            InitializeComponent();
        }

        private void TYUSME078S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_GDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.DTP01_GDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_92DHP756.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_92DHP755", Get_Date(this.DTP01_GDATE.GetValue().ToString()).Substring(0, 6));

            this.FPS91_TY_S_US_92DHP756.SetValue(this.DbConnector.ExecuteDataTable());

            for (int i = 0; i < this.FPS91_TY_S_US_92DHP756.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_US_92DHP756.GetValue(i, "BKMCYYMM").ToString() == "합 계")
                {
                    // 특정 ROW 글자 크기 변경
                    this.FPS91_TY_S_US_92DHP756.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);
                    this.FPS91_TY_S_US_92DHP756.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 194);
                }
            }
        }
        #endregion
    }
}