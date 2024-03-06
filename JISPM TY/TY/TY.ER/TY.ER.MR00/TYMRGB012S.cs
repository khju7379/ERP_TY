using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.MR00
{
    /// <summary>
    /// 코드박스 - 장기계약 조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.11.08 10:56
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_2B8C1196 : 코드박스 - 장기계약 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_2B84W204 : 코드박스-장기계약 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  OPM1020 : 계약업체
    ///  OPM1000 : 계약년도
    ///  OPM1040 : 계약내용
    ///  PRM1020 : 년월
    /// </summary>
    public partial class TYMRGB012S : TYBase, IPopupHelper
    {
        public string fsOPN1030   = string.Empty;
        public string fsOPN1030NM = string.Empty;
        public string fsOPN1040   = string.Empty;
        public string fsOPN1040NM = string.Empty;
        public string fsOPN1050   = string.Empty;
        public string fsOPN1050NM = string.Empty;
        public string fsOPN1060   = string.Empty;
        public string fsOPN1060NM = string.Empty;
        public string fsOPN1070   = string.Empty;
        public string fsOPN1080   = string.Empty;
        public string fsOPN1080NM = string.Empty;
        public string fsPRN1160   = string.Empty;
        public string fsPRN1100   = string.Empty;
        public string fsPRN1100NM = string.Empty;
        public string fsPRN1110   = string.Empty;
        public string fsPRN1110NM = string.Empty;

        private TCodeBox _TComboHelper;
        private DataRow _SelectedDataRow;

        #region Description : 페이지 로드
        public TYMRGB012S()
        {
            InitializeComponent();
            this.SetPopupStyle();
        }

        private void TYMRGB012S_Load(object sender, System.EventArgs e)
        {
            if (this.DesignMode)
                return;

            if (this._TComboHelper != null && this._TComboHelper.DummyValue != null)
            {
                string[] value = this._TComboHelper.DummyValue as string[];
                if (value != null && value.Length > 1)
                {
                    this.TXT01_OPN1000.SetValue(value[0].ToString()); // 계약년도
                    this.TXT01_OPN1010.SetValue(value[1].ToString()); // 계약순번////필수..시작
                }
            }
        }
        #endregion

        

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_MR_2C46H890.Initialize();
 
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
               (
               "TY_P_MR_2C46H889",
               this.TXT01_OPN1000.GetValue(),
               this.TXT01_OPN1010.GetValue()
               );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_MR_2C46H890.SetValue(dt);
            }
            else
            {
                this.FPS91_TY_S_MR_2C46H890.SetValue(dt);

                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        //#region Description : 스프레드 이벤트
        //private void FPS91_TY_S_MR_2C46H890_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        //{
        //    fsOPN1030   = this.FPS91_TY_S_MR_2C46H890.GetValue("OPN1030").ToString();
        //    fsOPN1030NM = this.FPS91_TY_S_MR_2C46H890.GetValue("DTDESC").ToString();
        //    fsOPN1040   = this.FPS91_TY_S_MR_2C46H890.GetValue("OPN1040").ToString();
        //    fsOPN1040NM = this.FPS91_TY_S_MR_2C46H890.GetValue("YSDESC").ToString();
        //    fsOPN1050   = this.FPS91_TY_S_MR_2C46H890.GetValue("OPN1050").ToString();
        //    fsOPN1050NM = this.FPS91_TY_S_MR_2C46H890.GetValue("A1NMAC").ToString();
        //    fsOPN1060   = this.FPS91_TY_S_MR_2C46H890.GetValue("OPN1060").ToString();
        //    fsOPN1060NM = this.FPS91_TY_S_MR_2C46H890.GetValue("BPDESC").ToString();
        //    fsOPN1070   = this.FPS91_TY_S_MR_2C46H890.GetValue("OPN1070").ToString();
        //    fsOPN1080   = this.FPS91_TY_S_MR_2C46H890.GetValue("OPN1080").ToString();
        //    fsOPN1080NM = this.FPS91_TY_S_MR_2C46H890.GetValue("Z105013").ToString();
        //    fsPRN1160   = this.FPS91_TY_S_MR_2C46H890.GetValue("PRN1160").ToString();
        //    fsPRN1100   = this.FPS91_TY_S_MR_2C46H890.GetValue("OPM1020").ToString();
        //    fsPRN1100NM = this.FPS91_TY_S_MR_2C46H890.GetValue("VNSANGHO").ToString();
        //    fsPRN1110   = this.FPS91_TY_S_MR_2C46H890.GetValue("OPM1150").ToString();
        //    fsPRN1110NM = this.FPS91_TY_S_MR_2C46H890.GetValue("TXDESC").ToString();

        //    this.DialogResult = System.Windows.Forms.DialogResult.OK;

        //    this.Close();
        //}
        //#endregion

        ////필수..시작
        #region Description : 필수 선택 조회
        public void ConfirmEventInterface()
        {
            int row = 0;

            string sCode = "";
            string sName = "";

            row = FPS91_TY_S_MR_2C46H890.ActiveSheet.ActiveRowIndex;

            //if (row > 0)
            //{
                // 제품코드
                sCode = this.FPS91_TY_S_MR_2C46H890.GetValue(row, "JEPUM").ToString();
                // 제품명
                sName = this.FPS91_TY_S_MR_2C46H890.GetValue(row, "JEPUMNM").ToString();


                this._SelectedDataRow = this.FPS91_TY_S_MR_2C46H890.GetDataRow(row);


                if (this._TComboHelper != null)
                {
                    this._TComboHelper.SetValue(sCode, sName);
                    this._TComboHelper.BindedDataRow = _SelectedDataRow;
                }

                this.Close();
            //}
        }

        public DataTable GetDataSource(params string[] parameters)
        {
            if (this._TComboHelper != null && this._TComboHelper.DummyValue != null)
            {
                string[] value = this._TComboHelper.DummyValue as string[];
                if (value != null && value.Length > 1)
                {
                    this.TXT01_OPN1000.SetValue(value[0].ToString()); // 계약년도
                    this.TXT01_OPN1010.SetValue(value[1].ToString()); // 계약순번
                }
            }

            string sJEPUM   = string.Empty;
            string sJEPUMNM = string.Empty;

            sJEPUM   = parameters[1];
            sJEPUMNM = parameters[0];

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2CIBJ244",
                this.TXT01_OPN1000.GetValue(),
                this.TXT01_OPN1010.GetValue(),
                sJEPUM.ToString(),
                sJEPUMNM.ToString()
                );

            return this.DbConnector.ExecuteDataTable();
        }

        public void Initialize_Helper(TCodeBox sender)
        {
            this._TComboHelper = sender;
            this._SelectedDataRow = null;

            this.Initialize_DbConnector();

            this.TXT01_OPN1000.Initialize();
            this.TXT01_OPN1000.Initialize();
        }

        public DataRow SelectedRow
        {
            get { return this._SelectedDataRow; }
        }

        public void ShowPopupHelper()
        {
            if (this.IsHandleCreated)
            {
                this.TYMRGB012S_Load(null, null);
                this._TComboHelper.CodeBoxPopupHelper_Init();
            }

            base.Show();
        }

        private void FPS91_TY_S_MR_2C46H890_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader)
                return;
            this.ConfirmEventInterface();
        }
        #endregion
        //필수...끝
    }
}