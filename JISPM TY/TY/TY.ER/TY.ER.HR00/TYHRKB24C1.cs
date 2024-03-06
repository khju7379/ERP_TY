using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 원천번호 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2015.03.24 15:37
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_53OFO856 : 원천번호 조회(인사)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_53OFQ858 : 원전번호 조회(인사)
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  B7CDAC : 계정코드
    /// </summary>
    public partial class TYHRKB24C1 : TYBase
    {
        private bool _Isloaded = false;

        private string fsB7CDAC;

        public string fsPOSABUN;
        public string fsPOYDATE;
        public string fsPOGUBN;

        #region Description : 폼 로드 이벤트
        public TYHRKB24C1(string sPOSABUN, string sPOYDATE, string sPOGUBN)
        {
            InitializeComponent();

            this.fsPOSABUN = sPOSABUN;
            this.fsPOYDATE = sPOYDATE;
            this.fsPOGUBN = sPOGUBN;
        }

        private void TYHRKB24C1_Load(object sender, System.EventArgs e)
        {
            this.CBH01_KBSABUN.SetValue(this.fsPOSABUN);
            
            this.CBH01_KBSABUN.SetReadOnly(true);

            UP_DataBinding();
            
        }
        #endregion

        #region Description : 데이터 조회 이벤트
        private void UP_DataBinding()
        {
            this.FPS91_TY_S_HR_BC2A6863.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_HR_BC2A6862", fsPOSABUN, fsPOYDATE, fsPOGUBN
                );

          this.FPS91_TY_S_HR_BC2A6863.SetValue(this.DbConnector.ExecuteDataTable());

          this.SpreadSumRowAdd(this.FPS91_TY_S_HR_BC2A6863, "POGUBNTEXT", "임원 퇴직금 한도금액", SumRowType.Sum, "POOVAMOUNT");

        }
        #endregion


        #region Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
        #endregion

       

      
    }
}
