using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;

namespace TY.ER.AC00
{
    /// <summary>
    /// 구분손익 제외전표관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2018.12.18 16:52
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_8CIAN337 : 구분손익 제외전표 등록
    ///  TY_P_AC_8CIAO338 : 구분손익 제외전표 삭제
    ///  TY_P_AC_8CIAT340 : 구분손익 제외전표 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_8CIAV341 : 구분손익 제외전표 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  INQOPTION : 조회구분
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYACNC039I : TYBase
    {
        #region Description : 폼 로드
        public TYACNC039I()
        {
            InitializeComponent();

            
        }

        private void TYACNC039I_Load(object sender, System.EventArgs e)
        {          

            (this.FPS91_TY_S_AC_8CIAV341.Sheets[0].Columns[3].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.magnifier;
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_8CIAV341, "AXBTN");

            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_8CIAV341, "AXYYMM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_8CIAV341, "AXGUBN");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_8CIAV341, "AXJPNO");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.FPS91_TY_S_AC_8CIAV341.Initialize();            

            DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy"));

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.DTP01_SDATE);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            try
            {
                this.FPS91_TY_S_AC_8CIAV341.Initialize();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_8CIAT340", this.DTP01_SDATE.GetString().ToString().Substring(0, 4), CBO01_INQOPTION.GetValue().ToString(), "");
                DataTable dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_AC_8CIAV341.SetValue(dt);

                if (this.FPS91_TY_S_AC_8CIAV341.CurrentRowCount > 0)
                {
                    for (int i = 0; i < this.FPS91_TY_S_AC_8CIAV341.ActiveSheet.RowCount; i++)
                    {
                        if (this.FPS91_TY_S_AC_8CIAV341.GetValue(i, "AXJPNO").ToString() != "")
                        {
                            this.FPS91_TY_S_AC_8CIAV341_Sheet1.Cells[i, 3].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                        }                      
                    }
                }
            }
            catch
            {
                
            }
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_8CIAO338", dt);
                this.DbConnector.ExecuteNonQuery();

                this.ShowMessage("TY_M_GB_23NAD874");

                BTN61_INQ_Click(null, null);
            }
            catch
            {
                this.ShowMessage("TY_M_GB_43C9G671");
            }
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_8CIAV341.GetDataSourceInclude(TSpread.TActionType.Remove, "AXYYMM", "AXGUBN", "AXJPNO");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;

            }
            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
            e.ArgData = dt;
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            DataTable dt = new DataTable();

            try
            {
                //신규등록
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_8CIAN337", ds.Tables[0].Rows[i]["AXYYMM"].ToString(),
                                                                ds.Tables[0].Rows[i]["AXGUBN"].ToString(),
                                                                ds.Tables[0].Rows[i]["AXJPNO"].ToString(),
                                                                ds.Tables[0].Rows[i]["AXMEMO"].ToString(),
                                                                TYUserInfo.EmpNo
                                                                );
                    this.DbConnector.ExecuteTranQuery();
                }

                //수정
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_AC_8CIHW348",
                                                                ds.Tables[1].Rows[i]["AXMEMO"].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["AXYYMM"].ToString(),
                                                                ds.Tables[1].Rows[i]["AXGUBN"].ToString(),
                                                                ds.Tables[1].Rows[i]["AXJPNO"].ToString()
                                                               );
                    this.DbConnector.ExecuteTranQuery();
                }

                this.ShowMessage("TY_M_GB_23NAD873");

                BTN61_INQ_Click(null, null);
            }
            catch
            {
                this.ShowMessage("TY_M_AC_246A2488");
            }
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();            

            ds.Tables.Add(this.FPS91_TY_S_AC_8CIAV341.GetDataSourceInclude(TSpread.TActionType.New, "AXYYMM", "AXGUBN", "AXJPNO", "AXMEMO"));
            ds.Tables.Add(this.FPS91_TY_S_AC_8CIAV341.GetDataSourceInclude(TSpread.TActionType.Update, "AXYYMM", "AXGUBN", "AXJPNO", "AXMEMO"));
         

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                int iRowCnt = 0;

                //전표번호 중복 등록 체크
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_8CIIA349", ds.Tables[0].Rows[i]["AXJPNO"].ToString()
                                                               );
                    iRowCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());
                    if( iRowCnt > 0 )
                    {
                        this.ShowMessage("TY_M_AC_6BABW707");
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : row 추가 이벤트
        private void FPS91_TY_S_AC_8CIAV341_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_8CIAV341.SetValue("AXYYMM", DateTime.Now.ToString("yyyy-MM"));
            this.FPS91_TY_S_AC_8CIAV341.SetValue("AXGUBN", "1");            
        }
        #endregion

        #region Description : row 추가 이벤트
        private void FPS91_TY_S_AC_8CIAV341_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "3")
            {
                TYACNC39C1 popup = new TYACNC39C1(this.FPS91_TY_S_AC_8CIAV341.GetValue("AXGUBN").ToString());

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.FPS91_TY_S_AC_8CIAV341.SetValue("AXJPNO", popup.fsB2JPNO);
                }
            }
        }
        #endregion

    }
}
