using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 주요설비투자현황 상세조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.07.23 17:25
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_37N5K225 : EIS 주요설비투자현황 상세조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_37N5L227 : EIS 주요설비투자현황 상세조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    /// </summary>
    public partial class TYACPO005P : TYBase
    {
        private string fsP2YEAR;
        private string fsP2CDDP;
        private string fsP2CDAC;


        #region  Description : 폼 로드 이벤트
        public TYACPO005P(string sP2YEAR, string sP2CDDP, string sP2CDAC)
        {
            InitializeComponent();
            this.SetPopupStyle();

            fsP2YEAR = sP2YEAR;
            fsP2CDDP = sP2CDDP;
            fsP2CDAC = sP2CDAC;  
        }

        private void TYACPO005P_Load(object sender, System.EventArgs e)
        {
            UP_Search();
        }
        #endregion

        #region  Description : 예산상세 조회 이벤트
        private void UP_Search()
        {
            this.FPS91_TY_S_AC_37N5L227.Initialize(); 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_37N5K225", fsP2YEAR.Substring(4,2) ,
                                                        fsP2YEAR.Substring(0,4),
                                                        fsP2CDDP, fsP2CDAC );
            this.FPS91_TY_S_AC_37N5L227.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_37N5L227.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_AC_37N5L227, "P2SEQNM", "합  계", SumRowType.Sum, "P2CRAMT","P2USAMT","P2JANAMT");
            }

        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
        #endregion
    }
}
