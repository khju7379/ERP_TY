using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.MR00
{
    /// <summary>
    /// 품목 입고내역 조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2013.02.13 14:33
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_32DBL057 : 품목 입고내역 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_32DBM059 : 품목 입고내역 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  RRN1050 : 품목
    /// </summary>
    public partial class TYMRRR005S : TYBase
    {
        #region Description : 페이지 로드
        public TYMRRR005S()
        {
            InitializeComponent();
        }

        private void TYMRRR005S_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.CBH01_RRN1050.CodeText);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_32DBL057",
                this.CBH01_RRN1050.GetValue().ToString()
                );

            this.FPS91_TY_S_MR_32DBM059.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion
    }
}