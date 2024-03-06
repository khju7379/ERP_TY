using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 결산 환율 관리 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2014.07.08 13:06
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_478E4964 : 환율관리 삭제
    ///  TY_P_AC_478E5965 : 환율관리 수정
    ///  TY_P_AC_478E6966 : 환율관리 입력
    ///  TY_P_AC_478E7967 : 환율관리 조회
    ///  TY_P_AC_478E8968 : 환율관리 체크
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_478DX963 : 환율 관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_AC_28D5W379 : 자료가 존재합니다! 삭제후 작업하세요
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
    ///  AERDATE : 기준일자
    /// </summary>
    public partial class TYACSE007I : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYACSE007I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_478DX963, "AERCURR", "AERCURRNM", "AERCURR");
        }

        private void TYACSE007I_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_478DX963, "AERDATE");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_478DX963, "AERCURR");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_478DX963, "AERCURRNM");

            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_478DX963, "AERDATE");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            
            //this.DTP01_AERDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            
            this.SetStartingFocus(this.DTP01_AERDATE);

            this.BTN61_INQ_Click(null, null);  
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_478E7967", this.ControlFactory, "01");
            this.FPS91_TY_S_AC_478DX963.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_478E4964", dt);
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
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_478E6966", ds.Tables[0].Rows[i][0].ToString(), //기준일자
                       ds.Tables[0].Rows[i][1].ToString(), // 화페구분
                       ds.Tables[0].Rows[i][2].ToString(), // 기준환율
                       ds.Tables[0].Rows[i][3].ToString(), // 현찰
                       ds.Tables[0].Rows[i][4].ToString()); // 송금

                }
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                this.DbConnector.Attach("TY_P_AC_478E5965", ds.Tables[1]); // 수정
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
            ds.Tables.Add(this.FPS91_TY_S_AC_478DX963.GetDataSourceInclude(TSpread.TActionType.New, "AERDATE", "AERCURR", "AERSTAD", "AERCASH", "AERREMI"));
            ds.Tables.Add(this.FPS91_TY_S_AC_478DX963.GetDataSourceInclude(TSpread.TActionType.Update, "AERSTAD", "AERCASH", "AERREMI", "AERDATE", "AERCURR"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
                e.Successed = false;
                return;
            }


            // 저장 체크
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_478E8968", ds.Tables[0].Rows[i]["AERDATE"].ToString(), ds.Tables[0].Rows[i]["AERCURR"].ToString()); // 저장
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
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_478E8968", ds.Tables[1].Rows[i]["AERDATE"].ToString(), ds.Tables[1].Rows[i]["AERCURR"].ToString()); // 저장
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                if (iCnt == 0)
                {
                    this.ShowMessage("TY_M_AC_2422N250");
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
            DataTable dt = this.FPS91_TY_S_AC_478DX963.GetDataSourceInclude(TSpread.TActionType.Remove, "AERDATE", "AERCURR");

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

        #region Description : FPS91_TY_S_AC_478DX963_RowInserted 이벤트
        private void FPS91_TY_S_AC_478DX963_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_478DX963.SetValue(e.RowIndex, "AERSTAD", 0.00);
            this.FPS91_TY_S_AC_478DX963.SetValue(e.RowIndex, "AERCASH", 0.00);
            this.FPS91_TY_S_AC_478DX963.SetValue(e.RowIndex, "AERREMI", 0.00);

            this.FPS91_TY_S_AC_478DX963.SetValue(e.RowIndex, "AERDATE", this.DTP01_AERDATE.GetValue());

            //string year = FPS91_TY_S_AC_478DX963.GetValue(e.RowIndex, "AERDATE").ToString();
            //TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_478DX963, "AYDDPAC");
            //if (tyCodeBox != null)
            //    tyCodeBox.DummyValue = year;
        }
        #endregion

        #region Description : FPS91_TY_S_AC_478DX963_EnterCell 이벤트
        private void FPS91_TY_S_AC_478DX963_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {

            this.FPS91_TY_S_AC_478DX963.SetValue(e.Row, "AERDATE", FPS91_TY_S_AC_478DX963.GetValue(e.Row, "AERDATE").ToString());
        }
        #endregion


    }
}
