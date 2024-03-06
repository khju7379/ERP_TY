using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.ED00
{
    /// <summary>
    /// 반입보고서 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.02.27 11:54
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_72RDC787 : 반입보고서 조회
    ///  TY_P_UT_72RDE788 : 내국화물 반입보고서 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_72RDH792 : 반입보고서 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  GOKCR : 생성구분
    ///  EDIDATE : 반입일자
    ///  EDIJUKHA : 적하목록
    /// </summary>
    public partial class TYEDKB012S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYEDKB012S()
        {
            InitializeComponent();
        }

        private void TYEDKB012S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            (this.FPS91_TY_S_UT_865A4173.Sheets[0].Columns[30].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.magnifier;
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_UT_865A4173, "BTN");

            UP_SetLockCheck();            

            this.DTP01_SDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.BTN61_INQ_Click(null, null);

            this.SetStartingFocus(DTP01_SDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {          
            this.FPS91_TY_S_UT_865A4173.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_865A1172", CBO01_EDIGJ.GetValue(), DTP01_SDATE.GetString().ToString(), DTP01_EDATE.GetString().ToString());
            this.FPS91_TY_S_UT_865A4173.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_UT_865A4173.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_UT_865A4173.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_UT_865A4173.GetValue(i, "EDIRCVGB").ToString() == "Y")
                    {
                        this.FPS91_TY_S_UT_865A4173_Sheet1.Rows[i].ForeColor = Color.Blue; 
                    }
                    else if (this.FPS91_TY_S_UT_865A4173.GetValue(i, "EDIRCVGB").ToString() == "E")
                    {
                        this.FPS91_TY_S_UT_865A4173_Sheet1.Rows[i].ForeColor = Color.Red; 
                    }

                    if (this.FPS91_TY_S_UT_865A4173.GetValue(i, "EDAAPVALNO").ToString() == "")
                    {
                        this.FPS91_TY_S_UT_865A4173_Sheet1.Cells[i, 30].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    }
                    else
                    {
                        //this.FPS91_TY_S_AC_2BF55357_Sheet1.Cells[i, 28].Image = global::TY.Service.Library.Properties.Resources.magnifier;
                    }
                }
            }

        }
        #endregion

        #region  Description : FPS91_TY_S_UT_865A4173_CellDoubleClick 이벤트
        private void FPS91_TY_S_UT_865A4173_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYEDKB012I(this.FPS91_TY_S_UT_865A4173.GetValue("EDIGJ").ToString(),
                                 this.FPS91_TY_S_UT_865A4173.GetValue("EDIDATE").ToString(),
                                 this.FPS91_TY_S_UT_865A4173.GetValue("EDINO1").ToString(),
                                 this.FPS91_TY_S_UT_865A4173.GetValue("EDINO2").ToString(),
                                 this.FPS91_TY_S_UT_865A4173.GetValue("EDINO3").ToString()
                                )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYEDKB012I(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : Lock Check
        private void UP_SetLockCheck()
        {
            if (TYUserInfo.DeptCode.Substring(0, 1) == "S")
            {
                CBO01_EDIGJ.SetValue("S");
            }
            else
            {
                CBO01_EDIGJ.SetValue("T");
            }

            if (TYUserInfo.DeptCode.Substring(0, 6) != "A10300")
            {
                CBO01_EDIGJ.SetReadOnly(true);
            }
        }
        #endregion

        #region  Description : FPS91_TY_S_UT_865A4173_ButtonClicked 이벤트
        private void FPS91_TY_S_UT_865A4173_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "30")
            {
                if ((new TYEDKB012P(this.FPS91_TY_S_UT_865A4173.GetValue("EDIGJ").ToString(),
                                    this.FPS91_TY_S_UT_865A4173.GetValue("EDINO1").ToString(),
                                    this.FPS91_TY_S_UT_865A4173.GetValue("EDINO2").ToString(),
                                    this.FPS91_TY_S_UT_865A4173.GetValue("EDINO3").ToString()
                                )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_UT_86C90202", dt.Rows[i]["EDIGJ"].ToString(),
                                                            dt.Rows[i]["EDIDATE"].ToString(),
                                                            dt.Rows[i]["EDINO1"].ToString(),
                                                            dt.Rows[i]["EDINO2"].ToString(),
                                                            dt.Rows[i]["EDINO3"].ToString());
            }
            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            DataTable dt = this.FPS91_TY_S_UT_865A4173.GetDataSourceInclude(TSpread.TActionType.Remove, "EDIGJ", "EDIDATE", "EDINO1", "EDINO2", "EDINO3", "EDIRCVGB");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if( dt.Rows[i]["EDIRCVGB"].ToString() == "Y" )
                    {
                        this.ShowCustomMessage("접수가 완료된 자료는 삭제할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return; 
                    }
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion






    }
}
