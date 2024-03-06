using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 년예산관리 등록 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.04.02 13:05
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2423A253 : 년예산 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2423D255 : 년예산 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  Y1CDDP : 예산부서
    ///  Y1YEAR : 예산년도
    /// </summary>
    public partial class TYACLB001I : TYBase
    {
        public TYACLB001I()
        {
            InitializeComponent();
        }

        private void TYACLB001I_Load(object sender, System.EventArgs e)
        {
            
        }

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_2423A253", this.ControlFactory, "01");
            this.FPS91_TY_S_AC_2423D255.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        private void BTN61_REM_Click(object sender, EventArgs e)
        {
        }

        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
        }
    }
}
