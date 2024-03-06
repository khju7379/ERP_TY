using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.AC00
{
    /// <summary>
    /// 거래처관리 등록 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.04.02 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2441H411 : 거래처 관리 삭제
    ///  TY_P_AC_244BN404 : 거래처관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2441F406 : 거래처관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  VNCODE : 거래처코드
    ///  VNSANGHO : 상호
    /// </summary>
    public partial class TYACAB006S : TYBase
    {
        private string fsGUBUN = string.Empty;

        #region Description : 페이지 로드
        public TYACAB006S()
        {
            InitializeComponent();
        }

        private void TYACAB006S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            SetStartingFocus(this.TXT01_VNSANGHO);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            if (fsGUBUN == "")
            {
                if (this.TXT01_VNSANGHO.GetValue().ToString() == "" && this.TXT01_VNSAUPNO.GetValue().ToString() == "" && this.TXT01_VNIRUM.GetValue().ToString() == ""
                     && this.TXT01_VNCODE.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_2BU5B773");

                    SetFocus(this.TXT01_VNSANGHO);
                    return;
                }
            }

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_46QF8893",  // TY_P_AC_244BN404
                this.TXT01_VNSANGHO.GetValue().ToString(),
                this.TXT01_VNSAUPNO.GetValue().ToString(),
                this.TXT01_VNIRUM.GetValue().ToString() ,
                this.TXT01_VNCODE.GetValue().ToString()
                );

            this.FPS91_TY_S_AC_2441F406.SetValue(this.DbConnector.ExecuteDataTable());

            fsGUBUN = "";
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYACAB006I(string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fsGUBUN = "ADD";

                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            // 삭제 프로시저
            this.DbConnector.Attach("TY_P_AC_2441H411", dt);
            this.DbConnector.Attach("TY_P_AC_33T25391", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // 삭제 체크되어 있는 행의 칼럼(VNCODE)을 가져와서 체크하는 부분
            DataTable dt = this.FPS91_TY_S_AC_2441F406.GetDataSourceInclude(TSpread.TActionType.Remove, "VNCODE");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_AC_24433418",
                                       dt.Rows[i]["VNCODE"].ToString(),
                                       dt.Rows[i]["VNCODE"].ToString(),
                                       dt.Rows[i]["VNCODE"].ToString(),
                                       dt.Rows[i]["VNCODE"].ToString(),
                                       dt.Rows[i]["VNCODE"].ToString(),
                                       dt.Rows[i]["VNCODE"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_2443N422");
                    e.Successed = false;
                    return;
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

        #region Description : 스프레드 클릭 이벤트
        private void FPS91_TY_S_AC_2441F406_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            // 파라미터값 보내기
            if ((new TYACAB006I(this.FPS91_TY_S_AC_2441F406.GetValue("VNCODE").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fsGUBUN = "UPT";

                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion
    }
}