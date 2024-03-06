using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.MR00
{
    /// <summary>
    /// 거래처관리 등록 팝업 프로그램입니다.
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
    ///  TY_P_AC_2454Y465 : 사업자번호 체크(등록)
    ///  TY_P_AC_24550471 : 사업자번호 체크(수정)
    ///  TY_P_AC_245BX447 : 거래처코드 가져오는 SP
    ///  TY_P_AC_2444X432 : 거래처관리 등록
    ///  TY_P_AC_2444Y433 : 거래처관리 수정
    ///  TY_P_AC_2445D438 : 거래처관리 확인
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2445M440 : 은행코드를 입력하세요.
    ///  TY_M_AC_2445M441 : 계좌번호를 입력하세요.
    ///  TY_M_AC_2454S464 : 사업자 번호 또는 주민등록번호중 한가지만 입력이 가능 합니다.
    ///  TY_M_AC_2443N422 : 해당거래처 코드는 사용내용이 존재하여 작업이 불가합니다.
    ///  TY_M_AC_2445G439 : 동일 사업자 번호가 존재합니다.
    ///  TY_M_AC_2445M440 : 은행코드를 입력하세요.
    ///  TY_M_AC_2445M441 : 계좌번호를 입력하세요.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  VNCDBK : 은행
    ///  VNGUBUN : 구분
    ///  VNJJGUB : 자재사용구분
    ///  VNPGUBN : 거래처구분
    ///  VNBKYN : 전자계좌계설
    ///  VNBIGO : 비고
    ///  VNCODE : 거래처코드
    ///  VNHIDAT : 작성일자
    ///  VNIRUM : 대표자명
    ///  VNJUSO : 주소
    ///  VNNOAC : 계좌번호
    ///  VNSANGHO : 거래처명
    ///  VNSAUPNO1 : 사업자등록번호1
    ///  VNSAUPNO2 : 사업자등록번호2
    ///  VNSAUPNO3 : 사업자등록번호3
    ///  VNTEL : 전화번호
    ///  VNUPJONG : 업종
    ///  VNUPTE : 업태
    ///  VNUPYUN1 : 우편번호1
    ///  VNUPYUN2 : 우편번호2
    /// </summary>
    public partial class TYMRPR005S : TYBase
    {
        public string fsPRM4050;

        #region Description : 페이지 로드
        public TYMRPR005S(string sPRM4050)
        {
            InitializeComponent();

            this.SetPopupStyle();

            // 파라미터값 가져오기
            this.fsPRM4050= sPRM4050;
        }

        private void TYMRPR005S_Load(object sender, System.EventArgs e)
        {
            webBrowser1.Navigate(fsPRM4050);
        }
        #endregion
    }
}