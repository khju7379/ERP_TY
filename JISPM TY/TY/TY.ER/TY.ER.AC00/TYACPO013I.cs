using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 투자현황 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.09.10 16:04
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_39A5E647 : EIS 투자현황 등록
    ///  TY_P_AC_39A5I648 : EIS 투자현황 수정
    ///  TY_P_AC_39A5Q650 : EIS 투자현황 삭제
    ///  TY_P_AC_39A5S651 : EIS 투자배당내역 등록
    ///  TY_P_AC_39A5T652 : EIS 투자배당내역 수정
    ///  TY_P_AC_39A66653 : EIS 투자배당내역 삭제
    ///  TY_P_AC_39B2N662 : EIS 투자현황 조회(종속관계기업)
    ///  TY_P_AC_39B2R663 : EIS 투자현황 조회(일반투자)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_39B3G665 : EIS 투자현황(종속관계기업)
    ///  TY_S_AC_39B3I666 : EIS 투자현황 조회(일반투자)
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACPO013I : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYACPO013I()
        {
            InitializeComponent();

            

            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_39B3G665, "EDVNCODE", "VNSANGHO", "EDVNCODE");           
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_39B3G665, "EDGUBN", "EDGUBNNM", "EDGUBN");

            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_39B3I666, "EDVNCODE", "VNSANGHO", "EDVNCODE");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_39B3I666, "EDGUBN", "EDGUBNNM", "EDGUBN");
          
        }

        private void TYACPO013I_Load(object sender, System.EventArgs e)
        {

            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_39B3G665, "EDYYMM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_39B3G665, "EDVNCODE");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_39B3G665, "EDGUBN");

            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_39B3I666, "EDYYMM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_39B3I666, "EDVNCODE");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_39B3I666, "EDGUBN");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_GSTYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            
            this.BTN61_INQ_Click(null, null);

            this.SetStartingFocus(this.DTP01_GSTYYMM);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_39B3G665.Initialize();
            this.FPS91_TY_S_AC_39B3I666.Initialize();

            this.DbConnector.CommandClear();
            //관계종속기업 조회
            this.DbConnector.Attach("TY_P_AC_39B2N662", this.DTP01_GSTYYMM.GetString().Substring(0,6));
            this.FPS91_TY_S_AC_39B3G665.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_39B3G665.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_AC_39B3G665, "VNSANGHO", "합   계", SumRowType.Sum, "EDSTOCKAMOUNT", "EDBOOKAMOUNT");

                for (int i = 0; i < this.FPS91_TY_S_AC_39B3G665.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_39B3G665.GetValue(i, "EDYYMM").ToString() == "")
                    {
                        this.FPS91_TY_S_AC_39B3G665_Sheet1.Cells[i, 10].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    }                    
                }
            }

            this.DbConnector.CommandClear();
            //일반투자 기업 조회
            this.DbConnector.Attach("TY_P_AC_39B2R663", this.DTP01_GSTYYMM.GetString().Substring(0, 6));
            this.FPS91_TY_S_AC_39B3I666.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_39B3I666.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_AC_39B3I666, "VNSANGHO", "합   계", SumRowType.Sum, "EDSTOCKAMOUNT", "EDBOOKAMOUNT");                
            }

        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            //관계종속기업 

            //저장
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_39A5E647", ds.Tables[0].Rows[i]["EDYYMM"].ToString(),
                                                            ds.Tables[0].Rows[i]["EDVNCODE"].ToString(),
                                                            ds.Tables[0].Rows[i]["EDGUBN"].ToString(),
                                                            ds.Tables[0].Rows[i]["EDSTRATE"].ToString(),
                                                            ds.Tables[0].Rows[i]["EDINDATE"].ToString(),
                                                            ds.Tables[0].Rows[i]["EDSTOCKQTY"].ToString(),
                                                            ds.Tables[0].Rows[i]["EDSTOCKAMOUNT"].ToString(),
                                                            ds.Tables[0].Rows[i]["EDBOOKAMOUNT"].ToString(),                                                            
                                                            TYUserInfo.EmpNo
                                                            );
            }
            //수정
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_39A5I648", ds.Tables[1].Rows[i]["EDSTRATE"].ToString(),
                                                            ds.Tables[1].Rows[i]["EDINDATE"].ToString(),
                                                            ds.Tables[1].Rows[i]["EDSTOCKQTY"].ToString(),
                                                            ds.Tables[1].Rows[i]["EDSTOCKAMOUNT"].ToString(),
                                                            ds.Tables[1].Rows[i]["EDBOOKAMOUNT"].ToString(),
                                                            TYUserInfo.EmpNo,
                                                            ds.Tables[1].Rows[i]["EDYYMM"].ToString(),
                                                            ds.Tables[1].Rows[i]["EDVNCODE"].ToString(),
                                                            ds.Tables[1].Rows[i]["EDGUBN"].ToString()
                                                            );
            }

            //일반투자

            //저장
            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_39A5E647", ds.Tables[2].Rows[i]["EDYYMM"].ToString(),
                                                            ds.Tables[2].Rows[i]["EDVNCODE"].ToString(),
                                                            ds.Tables[2].Rows[i]["EDGUBN"].ToString(),
                                                            ds.Tables[2].Rows[i]["EDSTRATE"].ToString(),
                                                            ds.Tables[2].Rows[i]["EDINDATE"].ToString(),
                                                            ds.Tables[2].Rows[i]["EDSTOCKQTY"].ToString(),
                                                            ds.Tables[2].Rows[i]["EDSTOCKAMOUNT"].ToString(),
                                                            ds.Tables[2].Rows[i]["EDBOOKAMOUNT"].ToString(),
                                                            TYUserInfo.EmpNo
                                                            );
            }
            //수정
            for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_39A5I648", ds.Tables[3].Rows[i]["EDSTRATE"].ToString(),
                                                            ds.Tables[3].Rows[i]["EDINDATE"].ToString(),
                                                            ds.Tables[3].Rows[i]["EDSTOCKQTY"].ToString(),
                                                            ds.Tables[3].Rows[i]["EDSTOCKAMOUNT"].ToString(),
                                                            ds.Tables[3].Rows[i]["EDBOOKAMOUNT"].ToString(),
                                                            TYUserInfo.EmpNo,
                                                            ds.Tables[3].Rows[i]["EDYYMM"].ToString(),
                                                            ds.Tables[3].Rows[i]["EDVNCODE"].ToString(),
                                                            ds.Tables[3].Rows[i]["EDGUBN"].ToString()
                                                            );
            }

            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD873");  
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_39B3G665.GetDataSourceInclude(TSpread.TActionType.New, "EDYYMM", "EDVNCODE", "EDGUBN", "EDSTRATE", "EDINDATE", "EDSTOCKQTY", "EDSTOCKAMOUNT", "EDBOOKAMOUNT"));
            ds.Tables.Add(this.FPS91_TY_S_AC_39B3G665.GetDataSourceInclude(TSpread.TActionType.Update, "EDYYMM", "EDVNCODE", "EDGUBN", "EDSTRATE", "EDINDATE", "EDSTOCKQTY", "EDSTOCKAMOUNT", "EDBOOKAMOUNT"));

            ds.Tables.Add(this.FPS91_TY_S_AC_39B3I666.GetDataSourceInclude(TSpread.TActionType.New, "EDYYMM", "EDVNCODE", "EDGUBN", "EDSTRATE", "EDINDATE", "EDSTOCKQTY", "EDSTOCKAMOUNT", "EDBOOKAMOUNT"));
            ds.Tables.Add(this.FPS91_TY_S_AC_39B3I666.GetDataSourceInclude(TSpread.TActionType.Update, "EDYYMM", "EDVNCODE", "EDGUBN", "EDSTRATE", "EDINDATE", "EDSTOCKQTY", "EDSTOCKAMOUNT", "EDBOOKAMOUNT"));


            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0 && ds.Tables[3].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
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

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_39A5Q650", ds.Tables[0]);
            this.DbConnector.Attach("TY_P_AC_39A5Q650", ds.Tables[1]);
            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);

        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {            
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_39B3G665.GetDataSourceInclude(TSpread.TActionType.Remove, "EDYYMM", "EDVNCODE", "EDGUBN"));
            ds.Tables.Add(this.FPS91_TY_S_AC_39B3I666.GetDataSourceInclude(TSpread.TActionType.Remove, "EDYYMM", "EDVNCODE", "EDGUBN"));
            
            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
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

            e.ArgData = ds;

        }
        #endregion

        #region Description : 복사 버튼 이벤트
        private void BTN61_COPY_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYACPO013B()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : FPS91_TY_S_AC_39B3G665_RowInserted 이벤트
        private void FPS91_TY_S_AC_39B3G665_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_39B3G665.SetValue(e.RowIndex, "EDYYMM", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
        }
        #endregion

        #region Description : FPS91_TY_S_AC_39B3I666_RowInserted 이벤트
        private void FPS91_TY_S_AC_39B3I666_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_39B3I666.SetValue(e.RowIndex, "EDYYMM", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
        }
        #endregion

        #region Description : FPS91_TY_S_AC_39B3G665_ButtonClicked(배당내역) 이벤트
        private void FPS91_TY_S_AC_39B3G665_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "10")
            {                

                if (this.OpenModalPopup(new TYACPO014I(this.FPS91_TY_S_AC_39B3G665.GetValue("EDVNCODE").ToString())) == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);

            }
        }
        #endregion


    }
}
