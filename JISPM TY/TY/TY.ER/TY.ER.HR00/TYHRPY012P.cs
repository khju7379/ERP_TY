using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 급여지급명세서 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.12.29 18:18
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4CUGQ001 : 급여지급명세서 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_4CUGR002 : 급여지급명세서
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PAYGUBN : 급여구분
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYHRPY012P : TYBase
    {
        #region Description : 페이지 로드
        public TYHRPY012P()
        {
            InitializeComponent();
        }

        private void TYHRPY012P_Load(object sender, System.EventArgs e)
        {
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_4CUGR002.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_HR_4CUGQ001",
                this.DTP01_STDATE.GetString().Substring(0, 6),
                this.DTP01_EDDATE.GetString().Substring(0, 6),
                this.CBH01_PAYGUBN.GetValue().ToString()
                );

            this.FPS91_TY_S_HR_4CUGR002.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        private void FPS91_TY_S_HR_4CUGR002_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "14")
            {
                if ((new TYHRPY12C1(this.FPS91_TY_S_HR_4CUGR002.GetValue("PAYYYMM").ToString(), this.FPS91_TY_S_HR_4CUGR002.GetValue("PAYGUBN").ToString(), this.FPS91_TY_S_HR_4CUGR002.GetValue("PAYJIDATE").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
            }
        }
    }
}
