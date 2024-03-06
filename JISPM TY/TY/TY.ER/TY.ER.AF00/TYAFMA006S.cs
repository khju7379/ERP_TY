using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AF00
{
    /// <summary>
    /// EIS 계열사 자금수지 상세조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.11.15 11:53
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_3BFBW317 : EIS 계열사 자금수지 상세내역 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_3BF21321 : EIS 계열사 자금수지 상세내역 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PCCDFD : 자금코드
    ///  EFSUBGN : 계열사구분
    ///  EFYYMM : 년월
    /// </summary>
    public partial class TYAFMA006S : TYBase
    {
        private string fsCompany = string.Empty;
        private string fsDate = string.Empty;
        private string fsCDFD = string.Empty;

        #region  Description : 폼 로드 이벤트
        public TYAFMA006S(string sCompany, string sYYMM, string sCDFD)
        {
            InitializeComponent();

            this.fsCompany = sCompany;
            this.fsDate = sYYMM;
            this.fsCDFD = sCDFD;
        }

        private void TYAFMA006S_Load(object sender, System.EventArgs e)
        {
            CBH01_EFSUBGN.SetValue(fsCompany);
            DTP01_EFYYMM.SetValue(fsDate);
            CBH01_PCCDFD.SetValue(fsCDFD);

            DTP01_EFYYMM.SetReadOnly(true);  

            UP_Fund_Search();
        }
        #endregion

        #region  Description : 자금 상세 조회 
        private void UP_Fund_Search()
        {
            this.FPS91_TY_S_AC_3BF21321.Initialize(); 

            this.DbConnector.CommandClear();
            if (this.CBH01_EFSUBGN.GetValue().ToString() == "TH")
            {
                this.DbConnector.Attach("TY_P_AC_3BF2N322", this.CBH01_EFSUBGN.GetValue(), this.DTP01_EFYYMM.GetString().Substring(0, 6), CBH01_PCCDFD.GetValue());
            }
            if (this.CBH01_EFSUBGN.GetValue().ToString() == "TS")
            {
                this.DbConnector.Attach("TY_P_AC_3BFBW317", this.CBH01_EFSUBGN.GetValue(), this.DTP01_EFYYMM.GetString().Substring(0, 6), CBH01_PCCDFD.GetValue());
            }
            if (this.CBH01_EFSUBGN.GetValue().ToString() == "TG")
            {
                this.DbConnector.Attach("TY_P_AC_3BF2O323", this.CBH01_EFSUBGN.GetValue(), this.DTP01_EFYYMM.GetString().Substring(0, 6), CBH01_PCCDFD.GetValue());
            }

            this.FPS91_TY_S_AC_3BF21321.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_3BF21321.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_AC_3BF21321, "PCDATE", "합  계", SumRowType.Sum, "PCWAMT");
            }

        }
        #endregion

        #region  Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
        #endregion
    }
}
