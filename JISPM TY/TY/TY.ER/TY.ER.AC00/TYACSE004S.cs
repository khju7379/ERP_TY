using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 무형자산 월 상각조회 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2014.06.24 15:11
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_46OA0863 : 무형자산 충당금 명세서 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_46OA1864 : 무형자산 충당금 명세서 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  FXLYEAR : 자산년도
    /// </summary>
    public partial class TYACSE004S : TYBase
    {
        #region Description : 페이지 로드
        public TYACSE004S()
        {
            InitializeComponent();
        }

        private void TYACSE004S_Load(object sender, System.EventArgs e)
        {
           // this.BTN61_INQ_Click(null, null);

            this.SetFocus(this.TXT01_FXLYEAR);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {

            this.FPS91_TY_S_AC_46OA1864.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_46OA0863", this.TXT01_FXLYEAR.GetValue());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }

            this.FPS91_TY_S_AC_46OA1864.SetValue(dt);

            this.SetFocus(this.TXT01_FXLYEAR);
        }
        #endregion


    }
}