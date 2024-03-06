using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 관리공통비 배부관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.09.04 11:58
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_29414741 : 관리공통비 배부관리 입력
    ///  TY_P_AC_2941A742 : 관리공통비 배부관리 수정
    ///  TY_P_AC_2941A743 : 관리공통비 배부관리 삭제
    ///  TY_P_AC_294BZ740 : 관리공통비 배부관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_34A4S492 : 관리공통비 배부관리
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
    ///  AYDDPAC : 부서
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACNC099S : TYBase
    {
        private int fiCount = 0;

        #region Description : 폼 로드 이벤트
        public TYACNC099S()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_34A4S492, "AYDDPAC", "AYDDPACNM", "AYDDPAC");
        }

        private void TYACNC099S_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_34A4S492, "AYYYMM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_34A4S492, "AYDDPAC");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_34A4S492, "AYDDPACNM");

            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_34A4S492, "AYYYMM");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            
            this.DTP01_GSTYYMM.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"));
            this.DTP01_GEDYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.CBH01_AYDDPAC.DummyValue = this.DTP01_GSTYYMM.GetValue()  + "01"; 
            
            this.SetStartingFocus(this.DTP01_GSTYYMM);

            //this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_34A4Q489", this.ControlFactory, "01");

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_34A4S492.SetValue(dt);
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2941A743", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    this.DbConnector.Attach("TY_P_AC_34A4R490", ds.Tables[0].Rows[i]["AYDYEAR"].ToString(),
                //                                                ds.Tables[0].Rows[i]["AYDMONTH"].ToString(),
                //                                                ds.Tables[0].Rows[i]["AYDDPAC"].ToString(),
                //                                                ds.Tables[0].Rows[i]["AYDYLTT"].ToString(),
                //                                                ds.Tables[0].Rows[i]["AYDYLSS"].ToString(),
                //                                                ds.Tables[0].Rows[i]["AYDYLOO"].ToString(),
                //                                                ds.Tables[0].Rows[i]["AYDYLO1"].ToString(),
                //                                                ds.Tables[0].Rows[i]["AYDYLO2"].ToString()
                //                                                );
                //    //this.DbConnector.Attach("TY_P_AC_34A4R490", ds.Tables[0]); // 저장
                //}
                //this.DbConnector.Attach("TY_P_AC_34A4R491", ds.Tables[1]); // 수정

                this.DbConnector.Attach("TY_P_AC_34A4R490", ds.Tables[0]); // 저장

                this.DbConnector.ExecuteTranQueryList();
            }

            this.BTN61_INQ_Click(null, null);  
            
            this.ShowMessage("TY_M_GB_23NAD873");            
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            Int16 iCnt = 0;

            DataSet ds = new DataSet();

            // 저장
            ds.Tables.Add(this.FPS91_TY_S_AC_34A4S492.GetDataSourceInclude(TSpread.TActionType.New, "AYDYEAR","AYDMONTH", "AYDDPAC", "AYDYLTT","AYDYLSS","AYDYLOO","AYDYLO1","AYDYLO2"));
            // 수정
            ds.Tables.Add(this.FPS91_TY_S_AC_34A4S492.GetDataSourceInclude(TSpread.TActionType.Update, "AYDYEAR", "AYDMONTH", "AYDDPAC", "AYDYLTT", "AYDYLSS", "AYDYLOO", "AYDYLO1", "AYDYLO2"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
                e.Successed = false;
                return;
            }

            //decimal dRateTotal = 0;

            //// 저장 체크
            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //    dRateTotal = Convert.ToDecimal(ds.Tables[0].Rows[i]["AYDYLTT"].ToString()) + Convert.ToDecimal(ds.Tables[0].Rows[i]["AYDYLSS"].ToString()) + Convert.ToDecimal(ds.Tables[0].Rows[i]["AYDYLOO"].ToString());
            //    if (dRateTotal != 100)
            //    {
            //        this.ShowMessage("TY_M_AC_2943X749");
            //        e.Successed = false;
            //        return;
            //    }
            //    if (Convert.ToDecimal(ds.Tables[0].Rows[i]["AYDYLOO"].ToString()) != 0)
            //    {
            //        dRateTotal = Convert.ToDecimal(ds.Tables[0].Rows[i]["AYDYLO1"].ToString()) + Convert.ToDecimal(ds.Tables[0].Rows[i]["AYDYLO2"].ToString());
            //        if (dRateTotal != 100)
            //        {
            //            this.ShowMessage("TY_M_AC_2943Y750");
            //            e.Successed = false;
            //            return;
            //        }
            //    }

            //    this.DbConnector.CommandClear();
            //    this.DbConnector.Attach("TY_P_AC_29486785", ds.Tables[0].Rows[i]["AYDYEAR"].ToString() + ds.Tables[0].Rows[i]["AYDMONTH"].ToString(), ds.Tables[0].Rows[i]["AYDDPAC"].ToString()); // 저장
            //    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
            //    if (iCnt > 0)
            //    {
            //        this.ShowMessage("TY_M_AC_28D5W379");
            //        e.Successed = false;
            //        return;
            //    }
            //}

            //// 수정 체크
            //for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            //{
            //    dRateTotal = Convert.ToDecimal(ds.Tables[1].Rows[i]["AYDYLTT"].ToString()) + Convert.ToDecimal(ds.Tables[1].Rows[i]["AYDYLSS"].ToString()) + Convert.ToDecimal(ds.Tables[1].Rows[i]["AYDYLOO"].ToString());
            //    if (dRateTotal != 100)
            //    {
            //        this.ShowMessage("TY_M_AC_2943X749");
            //        e.Successed = false;
            //        return;
            //    }

            //    if (Convert.ToDecimal(ds.Tables[1].Rows[i]["AYDYLOO"].ToString()) != 0)
            //    {
            //        dRateTotal = Convert.ToDecimal(ds.Tables[1].Rows[i]["AYDYLO1"].ToString()) + Convert.ToDecimal(ds.Tables[1].Rows[i]["AYDYLO2"].ToString());
            //        if (dRateTotal != 100)
            //        {
            //            this.ShowMessage("TY_M_AC_2943Y750");
            //            e.Successed = false;
            //            return;
            //        }
            //    }
            //}

            //if (!this.ShowMessage("TY_M_GB_23NAD871"))
            //{
            //    e.Successed = false;
            //    return;
            //}

            e.ArgData = ds;
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_34A4S492.GetDataSourceInclude(TSpread.TActionType.Remove, "AYYYMM", "AYDDPAC");

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

        #region Description : FPS91_TY_S_AC_34A4S492_RowInserted 이벤트
        private void FPS91_TY_S_AC_34A4S492_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_34A4S492.SetValue(e.RowIndex, "AYDYEAR", "2013");
            this.FPS91_TY_S_AC_34A4S492.SetValue(e.RowIndex, "AYDMONTH", "05");
            this.FPS91_TY_S_AC_34A4S492.SetValue(e.RowIndex, "AYYYMM", "201305");

            if (fiCount == 0)
            {
                this.FPS91_TY_S_AC_34A4S492.SetValue(e.RowIndex, "AYDDPAC", "A10000");

                fiCount++;
            }
            else
            {
                this.FPS91_TY_S_AC_34A4S492.SetValue(e.RowIndex, "AYDDPAC", "C10000");
            }

            this.FPS91_TY_S_AC_34A4S492.SetValue(e.RowIndex, "AYDYLTT", 100.00);
            this.FPS91_TY_S_AC_34A4S492.SetValue(e.RowIndex, "AYDYLSS", 0.00);
            this.FPS91_TY_S_AC_34A4S492.SetValue(e.RowIndex, "AYDYLOO", 0.00);
            this.FPS91_TY_S_AC_34A4S492.SetValue(e.RowIndex, "AYDYLO1", 0.00);
            this.FPS91_TY_S_AC_34A4S492.SetValue(e.RowIndex, "AYDYLO2", 0.00);

            this.FPS91_TY_S_AC_34A4S492.SetValue(e.RowIndex, "AYRATETOTAL", 100.00);
            this.FPS91_TY_S_AC_34A4S492.SetValue(e.RowIndex, "AYTRDTOTAL", 100.00);

            string year = FPS91_TY_S_AC_34A4S492.GetValue(e.RowIndex, "AYYYMM").ToString() + "01";
            TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_34A4S492, "AYDDPAC");
            if (tyCodeBox != null)
                tyCodeBox.DummyValue = year;
        }
        #endregion

        #region Description : FPS91_TY_S_AC_34A4S492_EnterCell 이벤트
        private void FPS91_TY_S_AC_34A4S492_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {

            //this.FPS91_TY_S_AC_34A4S492.SetValue(e.Row, "AYDYEAR", FPS91_TY_S_AC_34A4S492.GetValue(e.Row, "AYYYMM").ToString().Substring(0,4));
            //this.FPS91_TY_S_AC_34A4S492.SetValue(e.Row, "AYDMONTH", FPS91_TY_S_AC_34A4S492.GetValue(e.Row, "AYYYMM").ToString().Substring(4,2));

            //if (e.Column == 3)
            //{
            //    string year = FPS91_TY_S_AC_34A4S492.GetValue(e.Row, "AYYYMM").ToString() + "01";
            //    TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_34A4S492, "AYDDPAC");
            //    if (tyCodeBox != null)
            //        tyCodeBox.DummyValue = year;
            //    return;
            //}                

            ////사업부 합계
            //decimal dSaupTotal = 0;
            //dSaupTotal = Convert.ToDecimal(FPS91_TY_S_AC_34A4S492.GetValue(e.Row, "AYDYLTT").ToString()) +
            //             Convert.ToDecimal(FPS91_TY_S_AC_34A4S492.GetValue(e.Row, "AYDYLSS").ToString()) +
            //             Convert.ToDecimal(FPS91_TY_S_AC_34A4S492.GetValue(e.Row, "AYDYLOO").ToString());

            //this.FPS91_TY_S_AC_34A4S492.SetValue(e.Row, "AYRATETOTAL", dSaupTotal);
  
            ////무역부 합계
            //dSaupTotal = Convert.ToDecimal(FPS91_TY_S_AC_34A4S492.GetValue(e.Row, "AYDYLO1").ToString()) +
            //             Convert.ToDecimal(FPS91_TY_S_AC_34A4S492.GetValue(e.Row, "AYDYLO2").ToString());

            //this.FPS91_TY_S_AC_34A4S492.SetValue(e.Row, "AYTRDTOTAL", dSaupTotal);


        }
        #endregion

        #region Description : FPS91_TY_S_AC_34A4S492_LeaveCell 이벤트
        private void FPS91_TY_S_AC_34A4S492_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            ////사업부 합계
            //decimal dSaupTotal = 0;
            //dSaupTotal = Convert.ToDecimal(FPS91_TY_S_AC_34A4S492.GetValue(e.Row, "AYDYLTT").ToString()) +
            //             Convert.ToDecimal(FPS91_TY_S_AC_34A4S492.GetValue(e.Row, "AYDYLSS").ToString()) +
            //             Convert.ToDecimal(FPS91_TY_S_AC_34A4S492.GetValue(e.Row, "AYDYLOO").ToString());

            //this.FPS91_TY_S_AC_34A4S492.SetValue(e.Row, "AYRATETOTAL", dSaupTotal);

            ////무역부 합계
            //dSaupTotal = Convert.ToDecimal(FPS91_TY_S_AC_34A4S492.GetValue(e.Row, "AYDYLO1").ToString()) +
            //             Convert.ToDecimal(FPS91_TY_S_AC_34A4S492.GetValue(e.Row, "AYDYLO2").ToString());

            //this.FPS91_TY_S_AC_34A4S492.SetValue(e.Row, "AYTRDTOTAL", dSaupTotal);
        }
        #endregion

        #region Description : DTP01_GSTYYMM_ValueChanged 이벤트
        private void DTP01_GSTYYMM_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_AYDDPAC.DummyValue = this.DTP01_GSTYYMM.GetValue() + "01";
        }
        #endregion
    }
}
