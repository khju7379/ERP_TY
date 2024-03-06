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
    /// BIN 출고관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.04.08 11:23
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_948BD270 : BIN 출고관리 조회
    ///  TY_P_US_948BI273 : BIN 출고관리 등록
    ///  TY_P_US_948BI274 : BIN 출고관리 수정
    ///  TY_P_US_948BJ275 : BIN 출고관리 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_948BF272 : BIN 출고관리 조회
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
    ///  BINO : BIN
    /// </summary>
    public partial class TYUSAU001I : TYBase
    {
        private string fsBINO = string.Empty;

        #region  Description : 폼 로드 이벤트
        public TYUSAU001I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_948BF272, "BIGOKJONG", "BIGOKJONGNM", "BIGOKJONG");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_948BF272, "BICOUNTRY", "BICOUNTRYNM", "BICOUNTRY");
        }

        private void TYUSAU001I_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_948BF272, "BINO");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_948BF272, "BIGOKJONG");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_948BF272, "BICOUNTRY");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.BTN61_INQ_FXM_Click(null, null);
        }
        #endregion

        #region Description : 왼쪽 조회 버튼
        private void BTN61_INQ_FXM_Click(object sender, EventArgs e)
        {
            this.TXT01_BINO.SetValue("");

            this.TXT01_BINO.SetReadOnly(false);

            this.FPS91_TY_S_US_97GBA049.Initialize();
            this.FPS91_TY_S_US_948BF272.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_97GB2048");

            this.FPS91_TY_S_US_97GBA049.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            UP_DataBinding(this.TXT01_BINO.GetValue().ToString());
        }
        #endregion

        #region  Description :  데이터 바인딩 이벤트
        private void UP_DataBinding(string sBINNO)
        {
            this.FPS91_TY_S_US_948BF272.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_948BD270", sBINNO.ToString(), "", "");

            this.FPS91_TY_S_US_948BF272.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            if (dt.Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_US_948BJ275", this.TXT01_BINO.GetValue().ToString(),
                                                                dt.Rows[i]["BIGOKJONG"].ToString(),
                                                                dt.Rows[i]["BICOUNTRY"].ToString()
                                                                );
                }
                this.DbConnector.ExecuteNonQueryList();
            }

            //this.BTN61_INQ_FXM_Click(null, null);
            UP_DataBinding(this.TXT01_BINO.GetValue().ToString());

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_US_948BF272.GetDataSourceInclude(TSpread.TActionType.Remove, "BIGOKJONG", "BICOUNTRY");

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
                    this.DbConnector.Attach("TY_P_US_948BI273", this.TXT01_BINO.GetValue().ToString(),
                                                                ds.Tables[0].Rows[i]["BIGOKJONG"].ToString(),
                                                                ds.Tables[0].Rows[i]["BICOUNTRY"].ToString(),
                                                                ds.Tables[0].Rows[i]["BIJUNG"].ToString(),
                                                                ds.Tables[0].Rows[i]["BICHQTY"].ToString(),
                                                                ds.Tables[0].Rows[i]["BIAREA"].ToString()
                                                                );
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_US_948BI274", ds.Tables[1].Rows[i]["BIJUNG"].ToString(),
                                                                ds.Tables[1].Rows[i]["BICHQTY"].ToString(),
                                                                ds.Tables[1].Rows[i]["BIAREA"].ToString(),
                                                                this.TXT01_BINO.GetValue().ToString(),
                                                                ds.Tables[1].Rows[i]["BIGOKJONG"].ToString(),
                                                                ds.Tables[1].Rows[i]["BICOUNTRY"].ToString()
                                                                );
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            //this.BTN61_INQ_FXM_Click(null, null);
            UP_DataBinding(this.TXT01_BINO.GetValue().ToString());

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_US_948BF272.GetDataSourceInclude(TSpread.TActionType.New,    "BIGOKJONG", "BICOUNTRY", "BIJUNG", "BICHQTY", "BIAREA"));
            ds.Tables.Add(this.FPS91_TY_S_US_948BF272.GetDataSourceInclude(TSpread.TActionType.Update, "BIGOKJONG", "BICOUNTRY", "BIJUNG", "BICHQTY", "BIAREA"));

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
                this.DbConnector.Attach("TY_P_US_948BD270",
                                            this.TXT01_BINO.GetValue().ToString(),
                                            ds.Tables[0].Rows[i]["BIGOKJONG"].ToString(),
                                            ds.Tables[0].Rows[i]["BICOUNTRY"].ToString()
                                            );
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

        #region Description : 왼쪽 스프레드 이벤트
        private void FPS91_TY_S_US_97GBA049_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.TXT01_BINO.SetReadOnly(true);

            this.TXT01_BINO.SetValue("");
            this.TXT01_BINO.SetValue(this.FPS91_TY_S_US_97GBA049.GetValue("BINO").ToString());

            UP_DataBinding(this.FPS91_TY_S_US_97GBA049.GetValue("BINO").ToString());
        }
        #endregion
    }
}
