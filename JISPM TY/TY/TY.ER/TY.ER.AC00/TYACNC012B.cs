using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// EVA 계획금액 일괄등록 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.09.06 09:26
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_29573833 : EVA 계획금액관리 입력
    ///  TY_P_AC_296AI854 : EVA 계획금액관리 일괄등록
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_296AJ855 : EVA 계획금액관리 일괄등록 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  SAV : 저장
    /// </summary>
    public partial class TYACNC012B : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYACNC012B()
        {
            InitializeComponent();

            this.SetPopupStyle(); 
        }

        private void TYACNC012B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_296AJ855, "AMPYEAR");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_296AJ855, "AMPMONTH");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_296AJ855, "AMPDPMK");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_296AJ855, "AMPCDAC");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_296AJ855, "AMPAMT");


            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_296AI854");
            this.FPS91_TY_S_AC_296AJ855.SetValue(this.DbConnector.ExecuteDataTable());          
           
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_29573833", ds.Tables[0]); // 저장

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            DataSet ds = new DataSet();

            // 저장
            ds.Tables.Add(this.FPS91_TY_S_AC_296AJ855.GetDataSourceInclude(TSpread.TActionType.New, "AMPYEAR", "AMPMONTH", "AMPDPMK", "AMPCDAC", "AMPAMT"));

            if (ds.Tables[0].Rows.Count == 0 )
            {
                this.ShowMessage("TY_M_AC_2422N250");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : BTN61_BTNADDROW_Click 이벤트
        private void BTN61_BTNADDROW_Click(object sender, EventArgs e)
        {
            Int16 iRowCnt = 0;

            iRowCnt = Convert.ToInt16(this.TXT01_GTADDROW.GetValue().ToString());

            //빈칸 Add
            this.FPS91_TY_S_AC_296AJ855.ActiveSheet.AddRows(0, iRowCnt);
            for (int i = 0; i < iRowCnt; i++)
            {
                this.FPS91_TY_S_AC_296AJ855.ActiveSheet.RowHeader.Cells[i, 0].Text = "N";
            }
        }
        #endregion

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
