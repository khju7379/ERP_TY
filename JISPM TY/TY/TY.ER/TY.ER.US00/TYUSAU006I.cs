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
    /// 바코드 TAG관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.04.05 15:00
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_945FZ255 : 바코드 TAG 등록
    ///  TY_P_US_945FZ256 : 바코드 TAG 수정
    ///  TY_P_US_945FZ257 : 바코드 TAG 삭제
    ///  TY_P_US_945G0258 : 바코드 TAG 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_945G1260 : 바코드 TAG 조회
    ///  TY_S_US_945GE261 : 바코드 TAG 확인
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
    /// </summary>
    public partial class TYUSAU006I : TYBase
    {
        private string fsBDINDEX;

        #region  Description : 폼 로드 이벤트
        public TYUSAU006I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_945GE261, "BDBIGO", "BDBIGONM", "BDBIGO");
        }

        private void TYUSAU006I_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_945GE261, "BDINDEX");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_945GE261, "BDCODE");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            FPS91_TY_S_US_945G1260.Initialize();
            FPS91_TY_S_US_945G1260.SetValue(UP_Index_DataBinding("00", ""));
        }
        #endregion

        #region  Description : 데이터 바인딩 이벤트
        private DataTable UP_Index_DataBinding(string sINDEX, string sCode)
        {

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_945G0258", sINDEX, sCode);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            return dt;
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            FPS91_TY_S_US_945GE261.Initialize();

            FPS91_TY_S_US_945G1260.Initialize();
            FPS91_TY_S_US_945G1260.SetValue(UP_Index_DataBinding("00", ""));

        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_945FZ257", dt);
            this.DbConnector.ExecuteNonQueryList();

            FPS91_TY_S_US_945GE261.Initialize();
            FPS91_TY_S_US_945GE261.SetValue(
            UP_Index_DataBinding(fsBDINDEX,
                                 ""));

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_US_945GE261.GetDataSourceInclude(TSpread.TActionType.Remove, "BDINDEX", "BDCODE");

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
                    this.DbConnector.Attach("TY_P_US_945FZ255", ds.Tables[0].Rows[i]["BDINDEX"].ToString(),
                                            ds.Tables[0].Rows[i]["BDCODE"].ToString(),
                                            ds.Tables[0].Rows[i]["BDDESC"].ToString(),
                                            ds.Tables[0].Rows[i]["BDBIGO"].ToString(), TYUserInfo.EmpNo);
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_US_945FZ256",                                             
                                            ds.Tables[1].Rows[i]["BDDESC"].ToString(),
                                            ds.Tables[1].Rows[i]["BDBIGO"].ToString(), TYUserInfo.EmpNo,
                                            ds.Tables[1].Rows[i]["BDINDEX"].ToString(),
                                            ds.Tables[1].Rows[i]["BDCODE"].ToString());
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            FPS91_TY_S_US_945GE261.Initialize();
            FPS91_TY_S_US_945GE261.SetValue(
            UP_Index_DataBinding(fsBDINDEX,
                                 ""));

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_US_945GE261.GetDataSourceInclude(TSpread.TActionType.New, "BDINDEX", "BDCODE", "BDDESC", "BDBIGO"));
            ds.Tables.Add(this.FPS91_TY_S_US_945GE261.GetDataSourceInclude(TSpread.TActionType.Update, "BDINDEX", "BDCODE", "BDDESC", "BDBIGO"));

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
                this.DbConnector.Attach("TY_P_US_945G0258",
                                            ds.Tables[0].Rows[i]["BDINDEX"].ToString(),
                                            ds.Tables[0].Rows[i]["BDCODE"].ToString());
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if ( dt.Rows.Count > 0 )
                {
                    this.ShowCustomMessage("동일한 코드가 존재합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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

        #region  Description : FPS91_TY_S_US_945G1260_CellDoubleClick 버튼 이벤트
        private void FPS91_TY_S_US_945G1260_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            FPS91_TY_S_US_945GE261.Initialize();
            FPS91_TY_S_US_945GE261.SetValue(
            UP_Index_DataBinding(this.FPS91_TY_S_US_945G1260.GetValue("BDCODE").ToString(),
                                 ""));
            fsBDINDEX = this.FPS91_TY_S_US_945G1260.GetValue("BDCODE").ToString();

            string sIndex = string.Empty;

            if (fsBDINDEX == "01")
            {
                sIndex = "HJ";
            }
            else if (fsBDINDEX == "02")
            {
                sIndex = "GK";
            }
            else if (fsBDINDEX == "03")
            {
                sIndex = "WN";
            }
            else
            {
                sIndex = "";
            }

            if (sIndex != "")
            {
                TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_US_945GE261, "BDBIGO");
                if (tyCodeBox != null)
                    tyCodeBox.DummyValue = sIndex;

                this.FPS91_TY_S_US_945GE261.ActiveSheet.Columns["BDBIGO"].Locked = false;
            }
            else
            {

                this.FPS91_TY_S_US_945GE261.ActiveSheet.Columns["BDBIGO"].Locked = true;
            }
        }
        #endregion

        #region  Description : FPS91_TY_S_US_945GE261_RowInserted 버튼 이벤트
        private void FPS91_TY_S_US_945GE261_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_US_945GE261.SetValue(e.RowIndex, "BDINDEX", fsBDINDEX);
        }
        #endregion

    }
}
