using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.ED00
{
    /// <summary>
    /// 반출기간연장 통관관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2018.06.07 09:54
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_867FZ186 : 반출기간연장 자료 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_7C7GH215 : 세관 EDI - 통관일자로부터 5개월 이상된 재고 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    /// </summary>
    public partial class TYEDKB12C1 : TYBase
    {

        public string fsJUKHA = string.Empty;
        public string fsMSN = string.Empty;
        public string fsHSN = string.Empty;
        public string fsCSSINNO = string.Empty;


        #region  Description : 폼 로드 이벤트
        public TYEDKB12C1()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }

        private void TYEDKB12C1_Load(object sender, System.EventArgs e)
        {
            BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sDATE = string.Empty;

            sDATE = Get_Date(DateTime.Now.AddMonths(-5).ToString("yyyyMMdd"));

            DataTable dt = new DataTable();

            this.FPS91_TY_S_UT_867G9187.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_867FZ186", sDATE.ToString());

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_867G9187.SetValue(dt);
        }
        #endregion

        #region  Description : FPS91_TY_S_UT_867G9187_CellDoubleClick 이벤트
        private void FPS91_TY_S_UT_867G9187_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int row = (e == null ? 0 : e.Row);

            fsJUKHA = this.FPS91_TY_S_UT_867G9187.GetValue(row, "VSJUKHANUM").ToString();
            fsMSN = Set_Fill4(this.FPS91_TY_S_UT_867G9187.GetValue(row, "CSMSNSEQ").ToString());
            fsHSN = this.FPS91_TY_S_UT_867G9187.GetValue(row, "CSHSNSEQ").ToString() == "0" ? "" : Set_Fill4(this.FPS91_TY_S_UT_867G9187.GetValue(row, "CSHSNSEQ").ToString());
            fsCSSINNO = this.FPS91_TY_S_UT_867G9187.GetValue(row, "CSSINNO").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

       

    }
}
