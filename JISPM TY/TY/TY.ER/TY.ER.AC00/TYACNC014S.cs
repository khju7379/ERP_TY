using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 영업외손익 배부관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.09.04 16:29
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2944J751 : 영업외손익 배부관리 조회
    ///  TY_P_AC_2944K752 : 영업외손인 배부관리 입력
    ///  TY_P_AC_2944K753 : 영업외손익 배부관리 수정
    ///  TY_P_AC_2944K754 : 영업외손익 배부관리 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2944L755 : 영업외손익 배부관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2943X749 : 사업부별 배부율(%)은 100% 이어야 합니다!
    ///  TY_M_AC_2943Y750 : 무역부 배부율(%) 합은 100% 이어야 합니다!
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
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACNC014S : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYACNC014S()
        {
            InitializeComponent();
        }

        private void TYACNC014S_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_2944L755, "AYBYYMM");

            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_2944L755, "AYBYYMM");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_GSTYYMM.SetValue(DateTime.Now.AddMonths(-2).ToString("yyyy-MM"));
            this.DTP01_GEDYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.SetStartingFocus(this.DTP01_GSTYYMM);

            this.BTN61_INQ_Click(null, null);  

        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_2944J751", this.ControlFactory, "01");
            this.FPS91_TY_S_AC_2944L755.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2944K754", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            if (ds.Tables[0].Rows.Count > 0)
            {
                //this.DbConnector.Attach("TY_P_AC_2944K752", ds.Tables[0]); // 저장
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_2944K752", ds.Tables[0].Rows[i][5].ToString(), //년도
                       ds.Tables[0].Rows[i][6].ToString(), // 월
                       ds.Tables[0].Rows[i][0].ToString(), // UTT
                       ds.Tables[0].Rows[i][1].ToString(), // SILO
                       ds.Tables[0].Rows[i][2].ToString(), // 무역
                       ds.Tables[0].Rows[i][3].ToString(), // 석유화학
                       ds.Tables[0].Rows[i][4].ToString()); // 농업자원
                }
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                this.DbConnector.Attach("TY_P_AC_2944K753", ds.Tables[1]); // 수정
            }

            //this.DbConnector.Attach("TY_P_AC_2944K752", ds.Tables[0]); // 저장
            //this.DbConnector.Attach("TY_P_AC_2944K753", ds.Tables[1]); // 수정

            this.DbConnector.ExecuteTranQueryList();

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
            ds.Tables.Add(this.FPS91_TY_S_AC_2944L755.GetDataSourceInclude(TSpread.TActionType.New, "AYBYEAR", "AYBMONTH", "AYBYLTT", "AYBYLSS", "AYBYLOO", "AYBYLO1", "AYBYLO2"));
            // 수정
            ds.Tables.Add(this.FPS91_TY_S_AC_2944L755.GetDataSourceInclude(TSpread.TActionType.Update, "AYBYYMM", "AYBYLTT", "AYBYLSS", "AYBYLOO", "AYBYLO1", "AYBYLO2"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
                e.Successed = false;
                return;
            }

            decimal dRateTotal = 0;

            // 저장 체크
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dRateTotal = Convert.ToDecimal(ds.Tables[0].Rows[i]["AYBYLTT"].ToString()) + Convert.ToDecimal(ds.Tables[0].Rows[i]["AYBYLSS"].ToString()) + Convert.ToDecimal(ds.Tables[0].Rows[i]["AYBYLOO"].ToString());
                if (dRateTotal != 100)
                {
                    this.ShowMessage("TY_M_AC_2943X749");
                    e.Successed = false;
                    return;
                }
                dRateTotal = Convert.ToDecimal(ds.Tables[0].Rows[i]["AYBYLO1"].ToString()) + Convert.ToDecimal(ds.Tables[0].Rows[i]["AYBYLO2"].ToString());

                // 2014년 04월 부터 무역부에 대한 자료는 없어 체크 안함
                string sYYMM = ds.Tables[0].Rows[i]["AYBYEAR"].ToString() + ds.Tables[0].Rows[i]["AYBMONTH"].ToString();
                if (Convert.ToDecimal(sYYMM) <= 201404)
                {
                    if (dRateTotal != 100)
                    {
                        this.ShowMessage("TY_M_AC_2943Y750");
                        e.Successed = false;
                        return;
                    }
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29483784", ds.Tables[0].Rows[i]["AYBYEAR"].ToString()+ds.Tables[0].Rows[i]["AYBMONTH"].ToString()); // 저장
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                if (iCnt > 0)
                {
                    this.ShowMessage("TY_M_AC_28D5W379");
                    e.Successed = false;
                    return;
                }
            }

            // 수정 체크
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                dRateTotal = Convert.ToDecimal(ds.Tables[1].Rows[i]["AYBYLTT"].ToString()) + Convert.ToDecimal(ds.Tables[1].Rows[i]["AYBYLSS"].ToString()) + Convert.ToDecimal(ds.Tables[1].Rows[i]["AYBYLOO"].ToString());
                if (dRateTotal != 100)
                {
                    this.ShowMessage("TY_M_AC_2943X749");
                    e.Successed = false;
                    return;
                }
                dRateTotal = Convert.ToDecimal(ds.Tables[1].Rows[i]["AYBYLO1"].ToString()) + Convert.ToDecimal(ds.Tables[1].Rows[i]["AYBYLO2"].ToString());

                // 2014년 04월 부터 무역부에 대한 자료는 없어 체크 안함
                string sYYMM = ds.Tables[1].Rows[i]["AYBYYMM"].ToString();
                if (Convert.ToDecimal(sYYMM) <= 201404)
                {
                    if (dRateTotal != 100)
                    {
                        this.ShowMessage("TY_M_AC_2943Y750");
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

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_2944L755.GetDataSourceInclude(TSpread.TActionType.Remove, "AYBYYMM");

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

        #region Description : FPS91_TY_S_AC_2944L755_RowInserted 이벤트
        private void FPS91_TY_S_AC_2944L755_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_2944L755.SetValue(e.RowIndex, "AYBYLTT", 0.00);
            this.FPS91_TY_S_AC_2944L755.SetValue(e.RowIndex, "AYBYLSS", 0.00);
            this.FPS91_TY_S_AC_2944L755.SetValue(e.RowIndex, "AYBYLOO", 0.00);
            this.FPS91_TY_S_AC_2944L755.SetValue(e.RowIndex, "AYBYLO1", 0.00);
            this.FPS91_TY_S_AC_2944L755.SetValue(e.RowIndex, "AYBYLO2", 0.00);
        }
        #endregion

        #region Description : FPS91_TY_S_AC_2944L755_EnterCell 이벤트
        private void FPS91_TY_S_AC_2944L755_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            this.FPS91_TY_S_AC_2944L755.SetValue(e.Row, "AYBYEAR", FPS91_TY_S_AC_2944L755.GetValue(e.Row, "AYBYYMM").ToString().Substring(0, 4));
            this.FPS91_TY_S_AC_2944L755.SetValue(e.Row, "AYBMONTH", FPS91_TY_S_AC_2944L755.GetValue(e.Row, "AYBYYMM").ToString().Substring(4, 2));

            //사업부 합계
            decimal dSaupTotal = 0;
            dSaupTotal = Convert.ToDecimal(FPS91_TY_S_AC_2944L755.GetValue(e.Row, "AYBYLTT").ToString()) +
                         Convert.ToDecimal(FPS91_TY_S_AC_2944L755.GetValue(e.Row, "AYBYLSS").ToString()) +
                         Convert.ToDecimal(FPS91_TY_S_AC_2944L755.GetValue(e.Row, "AYBYLOO").ToString());

            this.FPS91_TY_S_AC_2944L755.SetValue(e.Row, "AYRATETOTAL", dSaupTotal);

            //무역부 합계
            dSaupTotal = Convert.ToDecimal(FPS91_TY_S_AC_2944L755.GetValue(e.Row, "AYBYLO1").ToString()) +
                         Convert.ToDecimal(FPS91_TY_S_AC_2944L755.GetValue(e.Row, "AYBYLO2").ToString());

            this.FPS91_TY_S_AC_2944L755.SetValue(e.Row, "AYTRDTOTAL", dSaupTotal);

        }
        #endregion

        #region Description : FPS91_TY_S_AC_2944L755_LeaveCell 이벤트
        private void FPS91_TY_S_AC_2944L755_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            //사업부 합계
            decimal dSaupTotal = 0;
            dSaupTotal = Convert.ToDecimal(FPS91_TY_S_AC_2944L755.GetValue(e.Row, "AYBYLTT").ToString()) +
                         Convert.ToDecimal(FPS91_TY_S_AC_2944L755.GetValue(e.Row, "AYBYLSS").ToString()) +
                         Convert.ToDecimal(FPS91_TY_S_AC_2944L755.GetValue(e.Row, "AYBYLOO").ToString());

            this.FPS91_TY_S_AC_2944L755.SetValue(e.Row, "AYRATETOTAL", dSaupTotal);

            //무역부 합계
            dSaupTotal = Convert.ToDecimal(FPS91_TY_S_AC_2944L755.GetValue(e.Row, "AYBYLO1").ToString()) +
                         Convert.ToDecimal(FPS91_TY_S_AC_2944L755.GetValue(e.Row, "AYBYLO2").ToString());

            this.FPS91_TY_S_AC_2944L755.SetValue(e.Row, "AYTRDTOTAL", dSaupTotal);
        }
        #endregion
    }
}
