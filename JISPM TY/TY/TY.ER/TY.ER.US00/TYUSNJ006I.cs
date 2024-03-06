using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.US00
{
    /// <summary>
    /// 연안선적 관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.04.08 17:17
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_948HG288 : 연안선적관리 등록
    ///  TY_P_US_948HG289 : 연안선적관리 수정
    ///  TY_P_US_948HG290 : 연안선적관리 삭제
    ///  TY_P_US_948HH291 : 연안선적관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_948HI293 : 연안선적관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
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
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYUSNJ006I : TYBase
    {

        #region  Description : 폼 로드 이벤트
        public TYUSNJ006I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_948HI293, "HYGOKJONG", "HYGOKJONGNM", "HYGOKJONG");
            
        }

        private void TYUSNJ006I_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_948HI293, "HYYYMM");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.TXT01_SDATE.SetValue(DateTime.Now.ToString("yyyy"));

            UP_DataBinding();
        }
        #endregion

        #region  Description :  데이터 바인딩 이벤트
        private void UP_DataBinding()
        {
            this.FPS91_TY_S_US_948HI293.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_948HH291", TXT01_SDATE.GetValue());

            this.FPS91_TY_S_US_948HI293.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            UP_DataBinding();
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_948HG290", dt);
            this.DbConnector.ExecuteNonQueryList();

            UP_DataBinding();

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_US_948HI293.GetDataSourceInclude(TSpread.TActionType.Remove, "HYYYMM");

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

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_US_948HG288", ds.Tables[0].Rows[i]["HYYYMM"].ToString(),
                                            ds.Tables[0].Rows[i]["HYVSNAME"].ToString(),
                                            ds.Tables[0].Rows[i]["HYGOKJONG"].ToString(),
                                            ds.Tables[0].Rows[i]["HYJAKUPTM"].ToString(),
                                            ds.Tables[0].Rows[i]["HYSTDATE"].ToString(),
                                            ds.Tables[0].Rows[i]["HYEDDATE"].ToString(),
                                            ds.Tables[0].Rows[i]["HYWKINWON"].ToString(),
                                            ds.Tables[0].Rows[i]["HYWKQTY"].ToString(),
                                            ds.Tables[0].Rows[i]["HYDANGA"].ToString(),
                                            ds.Tables[0].Rows[i]["HYAMOUNT"].ToString()
                                            );
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_US_948HG289",                                             
                                            ds.Tables[1].Rows[i]["HYVSNAME"].ToString(),
                                            ds.Tables[1].Rows[i]["HYGOKJONG"].ToString(),
                                            ds.Tables[1].Rows[i]["HYJAKUPTM"].ToString(),
                                            ds.Tables[1].Rows[i]["HYSTDATE"].ToString(),
                                            ds.Tables[1].Rows[i]["HYEDDATE"].ToString(),
                                            ds.Tables[1].Rows[i]["HYWKINWON"].ToString(),
                                            ds.Tables[1].Rows[i]["HYWKQTY"].ToString(),
                                            ds.Tables[1].Rows[i]["HYDANGA"].ToString(),
                                            ds.Tables[1].Rows[i]["HYAMOUNT"].ToString(),
                                            ds.Tables[1].Rows[i]["HYYYMM"].ToString()
                                            );
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            UP_DataBinding();

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            if (this.FPS91_TY_S_US_948HI293.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_US_948HI293.CurrentRowCount; i++)
                {
                    string sDanga = UP_Get_Danga(this.FPS91_TY_S_US_948HI293.GetValue(i, "HYYYMM").ToString().Replace("-", ""));

                    this.FPS91_TY_S_US_948HI293.SetValue(i, "HYDANGA", sDanga);

                    double dHYAMOUNT = Convert.ToDouble(this.FPS91_TY_S_US_948HI293.GetValue(i, "HYWKQTY").ToString()) * Convert.ToDouble(sDanga);

                    this.FPS91_TY_S_US_948HI293.SetValue(i, "HYAMOUNT", dHYAMOUNT);
                }
            }

            ds.Tables.Add(this.FPS91_TY_S_US_948HI293.GetDataSourceInclude(TSpread.TActionType.New, "HYYYMM", "HYVSNAME", "HYGOKJONG", "HYJAKUPTM", "HYSTDATE", "HYEDDATE","HYWKINWON","HYWKQTY","HYDANGA","HYAMOUNT"));
            ds.Tables.Add(this.FPS91_TY_S_US_948HI293.GetDataSourceInclude(TSpread.TActionType.Update, "HYYYMM", "HYVSNAME", "HYGOKJONG", "HYJAKUPTM", "HYSTDATE", "HYEDDATE", "HYWKINWON", "HYWKQTY", "HYDANGA", "HYAMOUNT"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            //동일코드 체크
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_948HH291",
                                            ds.Tables[0].Rows[i]["HYYYMM"].ToString()
                                            );
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if ( dt.Rows.Count > 0 )
                {
                    this.ShowCustomMessage("작업년월을 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return; 
                }

                //단가 체크
                string sDanga = UP_Get_Danga(ds.Tables[0].Rows[i]["HYYYMM"].ToString());
                if ( Convert.ToDouble(sDanga) <= 0)
                {
                    this.ShowCustomMessage("단가관리에 미등록된 자료입니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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

        #region  Description :  단가 조회 
        private string UP_Get_Danga(string sYYMM)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_949A2295",sYYMM);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return "0";
            }
        }
        #endregion

        #region  Description :  FPS91_TY_S_US_948HI293_RowInserted 이벤트
        private void FPS91_TY_S_US_948HI293_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_US_948HI293.SetValue(e.RowIndex, "HYDANGA", 0);
            this.FPS91_TY_S_US_948HI293.SetValue(e.RowIndex, "HYAMOUNT", 0);
            this.FPS91_TY_S_US_948HI293.SetValue(e.RowIndex, "HYWKQTY", 0);            
        }
        #endregion

        private void FPS91_TY_S_US_948HI293_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            //string sDanga = UP_Get_Danga(this.FPS91_TY_S_US_948HI293.GetValue(e.Row, "HYYYMM").ToString().Replace("-", ""));

            //this.FPS91_TY_S_US_948HI293.SetValue(e.Row, "HYDANGA", sDanga);
        }

    }
}

