using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 유형자산 배부비율 관리 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2012.09.04 11:58
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_36B2Q835 : 유형자산 배부비율 체크
    ///  TY_P_AC_36B2R836 : 유형자산 배부비율 조회
    ///  TY_P_AC_36B2S837 : 유형자산 배부비율 등록
    ///  TY_P_AC_36B2S838 : 유형자산 배부비율 수정
    ///  TY_P_AC_36B2T839 : 유형자산 배부비율 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_36B2T841 : 유형자산 배부비율
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_AC_28D5W379 : 자료가 존재합니다! 삭제후 작업하세요
    ///  TY_M_AC_2943X749 : 사업부별 배부율(%)은 100% 이어야 합니다!
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
    ///  ACFSEQ : 자산순번
    ///  ACFSUBNUM : 가족코드
    ///  ACFYEAR : 자산년도
    /// </summary>
    public partial class TYACHF012S : TYBase
    {
        public TYACHF012S()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_36B2T841, "FXFULLNUM", "FXSNAME", "FXFULLNUM");
        }

        #region Description  : Page_Load
        private void TYACHF012S_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_36B2T841, "AYYYMM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_36B2T841, "FXFULLNUM");
            //this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_36B2T841, "FXSNAME");

            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_36B2T841, "AYYYMM");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_GSTYYMM.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"));
            this.DTP01_GEDYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.SetStartingFocus(this.DTP01_GSTYYMM);

            this.BTN61_INQ_Click(null, null);  
        } 
        #endregion

        #region Description  : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_36B2R836", this.DTP01_GSTYYMM.GetValue().ToString().Trim(), this.DTP01_GEDYYMM.GetValue().ToString().Trim(), "");
            this.FPS91_TY_S_AC_36B2T841.SetValue(this.DbConnector.ExecuteDataTable());
        } 
        #endregion

        #region Description  : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_36B2T839", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        } 
        #endregion

        #region Description  : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //this.DbConnector.Attach("TY_P_AC_36B2S837", ds.Tables[0]); // 저장

                    this.DbConnector.Attach("TY_P_AC_36B2S837", ds.Tables[0].Rows[i][8].ToString(),
                       ds.Tables[0].Rows[i][9].ToString(),
                       ds.Tables[0].Rows[i][1].ToString(),
                       ds.Tables[0].Rows[i][2].ToString(),
                       ds.Tables[0].Rows[i][3].ToString(),
                       ds.Tables[0].Rows[i][4].ToString(),
                       ds.Tables[0].Rows[i][5].ToString(),
                       ds.Tables[0].Rows[i][6].ToString(),
                       ds.Tables[0].Rows[i][7].ToString()); // 저장
                }
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                this.DbConnector.Attach("TY_P_AC_36B2S838", ds.Tables[1]); // 수정
            }

            this.DbConnector.ExecuteNonQueryList();

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
            ds.Tables.Add(this.FPS91_TY_S_AC_36B2T841.GetDataSourceInclude(TSpread.TActionType.New, "ACFYEAR", "ACFSEQ", "ACFSUBNUM", "ACFDYLTT", "ACFDYLSS", "ACFDYLOO", "ACFDYLGG", "ACFDYEAR", "ACFDMONTH", "AYYYMM"));
            // 수정
            ds.Tables.Add(this.FPS91_TY_S_AC_36B2T841.GetDataSourceInclude(TSpread.TActionType.Update, "ACFDYLTT", "ACFDYLSS", "ACFDYLOO", "ACFDYLGG" , "ACFDYEAR", "ACFDMONTH", "ACFYEAR", "ACFSEQ", "ACFSUBNUM"));

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
                dRateTotal = Convert.ToDecimal(ds.Tables[0].Rows[i]["ACFDYLTT"].ToString()) + Convert.ToDecimal(ds.Tables[0].Rows[i]["ACFDYLSS"].ToString()) +
                             Convert.ToDecimal(ds.Tables[0].Rows[i]["ACFDYLOO"].ToString()) + Convert.ToDecimal(ds.Tables[0].Rows[i]["ACFDYLGG"].ToString());
                if (dRateTotal != 100)
                {
                    this.ShowMessage("TY_M_AC_2943X749");
                    e.Successed = false;
                    return;
                }

                // 존재 유무 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_36B2Q835", ds.Tables[0].Rows[i]["ACFDYEAR"].ToString() , 
                                                            ds.Tables[0].Rows[i]["ACFDMONTH"].ToString(),
                                                            ds.Tables[0].Rows[i]["ACFYEAR"].ToString(), 
                                                            ds.Tables[0].Rows[i]["ACFSEQ"].ToString() ,
                                                            ds.Tables[0].Rows[i]["ACFSUBNUM"].ToString()); // 저장 체크
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
                dRateTotal = Convert.ToDecimal(ds.Tables[1].Rows[i]["ACFDYLTT"].ToString()) + Convert.ToDecimal(ds.Tables[1].Rows[i]["ACFDYLSS"].ToString()) +
                             Convert.ToDecimal(ds.Tables[1].Rows[i]["ACFDYLOO"].ToString()) + Convert.ToDecimal(ds.Tables[1].Rows[i]["ACFDYLGG"].ToString());
                if (dRateTotal != 100)
                {
                    this.ShowMessage("TY_M_AC_2943X749");
                    e.Successed = false;
                    return;
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
            DataTable dt = this.FPS91_TY_S_AC_36B2T841.GetDataSourceInclude(TSpread.TActionType.Remove, "ACFDYEAR", "ACFDMONTH", "ACFYEAR", "ACFSEQ", "ACFSUBNUM");

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

        #region Description : Spread 처리
        private void FPS91_TY_S_AC_36B2T841_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_36B2T841.SetValue(e.RowIndex, "ACFDYLTT", 0.00);
            this.FPS91_TY_S_AC_36B2T841.SetValue(e.RowIndex, "ACFDYLSS", 0.00);
            this.FPS91_TY_S_AC_36B2T841.SetValue(e.RowIndex, "ACFDYLOO", 0.00);
            this.FPS91_TY_S_AC_36B2T841.SetValue(e.RowIndex, "ACFDYLGG", 0.00);
        }

        private void FPS91_TY_S_AC_36B2T841_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            //자산번호 , 년월
            string sACFYEAR = string.Empty;
            string sACFSEQ = string.Empty;
            string sACFSUBNUM = string.Empty;

            string sACFDYEAR = string.Empty;
            string sACFDMONTH = string.Empty;

            // 자산번호
            if (e.Column == 4)
            {
                if (this.FPS91_TY_S_AC_36B2T841.GetValue(e.Row, "FXFULLNUM").ToString().Trim() != "")
                {
                    sACFYEAR = this.FPS91_TY_S_AC_36B2T841.GetValue(e.Row, "FXFULLNUM").ToString().Substring(0, 4).Trim();
                    sACFSEQ = this.FPS91_TY_S_AC_36B2T841.GetValue(e.Row, "FXFULLNUM").ToString().Substring(5, 4).Trim();
                    sACFSUBNUM = this.FPS91_TY_S_AC_36B2T841.GetValue(e.Row, "FXFULLNUM").ToString().Substring(10, 3).Trim();

                    this.FPS91_TY_S_AC_36B2T841.SetValue(e.Row, "ACFYEAR", sACFYEAR);
                    this.FPS91_TY_S_AC_36B2T841.SetValue(e.Row, "ACFSEQ", sACFSEQ);
                    this.FPS91_TY_S_AC_36B2T841.SetValue(e.Row, "ACFSUBNUM", sACFSUBNUM);
                }
            }

            // 년월
            if (e.Column == 0)
            {
                if (this.FPS91_TY_S_AC_36B2T841.GetValue(e.Row, "AYYYMM").ToString().Trim() != "")
                {
                    sACFDYEAR = this.FPS91_TY_S_AC_36B2T841.GetValue(e.Row, "AYYYMM").ToString().Substring(0, 4).Trim();
                    sACFDMONTH = this.FPS91_TY_S_AC_36B2T841.GetValue(e.Row, "AYYYMM").ToString().Substring(4, 2).Trim();

                    if (sACFDYEAR == "1900")
                    {
                        sACFDYEAR = this.DTP01_GEDYYMM.GetValue().ToString().Trim().Substring(0,4);
                        sACFDMONTH = this.DTP01_GEDYYMM.GetValue().ToString().Trim().Substring(4, 2); 
                    }

                    this.FPS91_TY_S_AC_36B2T841.SetValue(e.Row, "ACFDYEAR", sACFDYEAR);
                    this.FPS91_TY_S_AC_36B2T841.SetValue(e.Row, "ACFDMONTH", sACFDMONTH);
                }
            }
        }

        private void FPS91_TY_S_AC_36B2T841_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            //자산번호 , 년월
            string sACFYEAR = string.Empty;
            string sACFSEQ = string.Empty;
            string sACFSUBNUM = string.Empty;

            string sACFDYEAR = string.Empty;
            string sACFDMONTH = string.Empty;

            // 자산번호
            if (e.Column == 4)
            {
                if (this.FPS91_TY_S_AC_36B2T841.GetValue(e.Row, "FXFULLNUM").ToString().Trim() != "")
                {
                    sACFYEAR = this.FPS91_TY_S_AC_36B2T841.GetValue(e.Row, "FXFULLNUM").ToString().Substring(0, 4).Trim();
                    sACFSEQ = this.FPS91_TY_S_AC_36B2T841.GetValue(e.Row, "FXFULLNUM").ToString().Substring(5, 4).Trim();
                    sACFSUBNUM = this.FPS91_TY_S_AC_36B2T841.GetValue(e.Row, "FXFULLNUM").ToString().Substring(10, 3).Trim();

                    this.FPS91_TY_S_AC_36B2T841.SetValue(e.Row, "ACFYEAR", sACFYEAR);
                    this.FPS91_TY_S_AC_36B2T841.SetValue(e.Row, "ACFSEQ", sACFSEQ);
                    this.FPS91_TY_S_AC_36B2T841.SetValue(e.Row, "ACFSUBNUM", sACFSUBNUM);
                }
            }

            // 년월
            if (e.Column == 0)
            {
                if (this.FPS91_TY_S_AC_36B2T841.GetValue(e.Row, "AYYYMM").ToString().Trim() != "")
                {
                    sACFDYEAR = this.FPS91_TY_S_AC_36B2T841.GetValue(e.Row, "AYYYMM").ToString().Substring(0, 4).Trim();
                    sACFDMONTH = this.FPS91_TY_S_AC_36B2T841.GetValue(e.Row, "AYYYMM").ToString().Substring(4, 2).Trim();

                    if (sACFDYEAR == "1900")
                    {
                        sACFDYEAR = this.DTP01_GEDYYMM.GetValue().ToString().Trim().Substring(0, 4);
                        sACFDMONTH = this.DTP01_GEDYYMM.GetValue().ToString().Trim().Substring(4, 2);
                    }

                    this.FPS91_TY_S_AC_36B2T841.SetValue(e.Row, "ACFDYEAR", sACFDYEAR);
                    this.FPS91_TY_S_AC_36B2T841.SetValue(e.Row, "ACFDMONTH", sACFDMONTH);
                }
            }
        } 
        #endregion

    }
}
