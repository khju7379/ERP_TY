using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 계정 과목 코드 등록 프로그램입니다.
    /// 
    /// 작성자 : 김영우
    /// 작성일 : 2012.03.19 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_23N3K882 : 계정 과목 코드 삭제
    ///  TY_P_AC_23N3M888 : 계정 과목 코드 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_23N3Q894 : 계정 과목 코드 조회
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
    ///  A1ACHL1 : 상위계정코드１
    ///  A1ACHL2 : 상위계정코드２
    ///  A1ACHL3 : 상위계정코드３
    ///  A1ACHL4 : 상위계정코드４
    ///  A1ACHL5 : 상위계정코드５
    ///  A1CDMI1 : 관리항목코드１
    ///  A1CDMI2 : 관리항목코드２
    ///  A1CDMI3 : 관리항목코드３
    ///  A1CDMI4 : 관리항목코드４
    ///  A1CDMI5 : 관리항목코드５
    ///  A1CDMI6 : 관리항목코드６
    ///  A1LVAC : LEVEL
    ///  A1TAG01 : 차／대(D/C)
    ///  A1TAG02 : 전표계정
    ///  A1TAG06 : 예산통제여부
    ///  A1TAG07 : 반제관리
    ///  A1YNBS : Ｂ／Ｓ계정
    ///  A1YNIS : Ｉ／Ｓ계정여부
    ///  A1YNTBD : 일계표계정여부
    ///  A1YNTB_ : Ｔ／Ｂ계정여부
    ///  A1ABAC : 계정과목약명
    ///  A1CDAC : 계정코드
    ///  A1NMAC : 계정과목명
    ///  A1NMENAC : 계정과목영문명
    /// </summary>
    public partial class TYERAC009S : TYBase
    {
        public TYERAC009S()
        {
            InitializeComponent();
        }

        private void TYERAC001S_Load(object sender, System.EventArgs e)
        {
        }

        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
        }

        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
        }

        private void BTN61_REM_Click(object sender, EventArgs e)
        {
        }
    }
}
