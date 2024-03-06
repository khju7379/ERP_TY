using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.UT00
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
    ///  TY_S_UT_669HG121 : 거래처관리 조회
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
    public partial class TYUTIN002S : TYBase
    {
        private string fsGUBUN = string.Empty;

        #region Description : 페이지 로드
        public TYUTIN002S()
        {
            InitializeComponent();
        }

        private void TYUTIN002S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            SetStartingFocus(this.TXT01_VNSANGHO);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            //if (fsGUBUN == "")
            //{
            //    if (this.TXT01_VNSANGHO.GetValue().ToString() == "" && this.TXT01_VNSAUPNO.GetValue().ToString() == "" && this.TXT01_VNIRUM.GetValue().ToString() == ""
            //         && this.TXT01_VNCODE.GetValue().ToString() == "")
            //    {
            //        this.ShowMessage("TY_M_AC_2BU5B773");

            //        SetFocus(this.TXT01_VNSANGHO);
            //        return;
            //    }
            //}

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_UT_669GX118",  // TY_P_AC_244BN404
                this.TXT01_VNSANGHO.GetValue().ToString(),
                this.TXT01_VNSAUPNO.GetValue().ToString(),
                this.TXT01_VNIRUM.GetValue().ToString() ,
                this.TXT01_VNCODE.GetValue().ToString()
                );

            this.FPS91_TY_S_UT_669HG121.SetValue(this.DbConnector.ExecuteDataTable());

            fsGUBUN = "";
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYUTIN002I(string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
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
            this.DbConnector.Attach("TY_P_UT_669GM115", dt);
            this.DbConnector.ExecuteNonQuery();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // 삭제 체크되어 있는 행의 칼럼(VNCODE)을 가져와서 체크하는 부분
            DataTable dt = this.FPS91_TY_S_UT_669HG121.GetDataSourceInclude(TSpread.TActionType.Remove, "VNCODE");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                /******************************************************************
			     * 1. 등록, 수정시 동일한 사업자 번호는 안들어가짐.               *
			     * 2. 사업자번호 업데이트 안됨.                                   *
			     *    2-1. 수정시 한번이라도 거래된 경우(재고테이블)              *
			     *    2-2. 수정시 한번이라도 전표가 발행된 경우                   *
			     * 3. 삭제 불가                                                   *
			     *    3-1. 한번이라도 거래된 경우(재고테이블)                     *
			     *    3-2. 한번이라도 전표가 발행된 경우                          *
			     ******************************************************************/

                #region Description : 3-1경우

                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_669HK122",
                                       dt.Rows[i]["VNCODE"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_669HP130");
                    e.Successed = false;
                    return;
                }

                #endregion

                #region Description : 3-2경우

                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_669HN123",
                                       dt.Rows[i]["VNCODE"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_669HP130");
                    e.Successed = false;
                    return;
                }
                
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_669HN124",
                                       dt.Rows[i]["VNCODE"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_669HP130");
                    e.Successed = false;
                    return;
                }


                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_669HO125",
                                       dt.Rows[i]["VNCODE"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_669HP130");
                    e.Successed = false;
                    return;
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_669HO127",
                                       dt.Rows[i]["VNCODE"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_669HP130");
                    e.Successed = false;
                    return;
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_669HO128",
                                       dt.Rows[i]["VNCODE"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_669HP130");
                    e.Successed = false;
                    return;
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_669HP129",
                                       dt.Rows[i]["VNCODE"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_669HP130");
                    e.Successed = false;
                    return;
                }

                #endregion
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
        private void FPS91_TY_S_UT_669HG121_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            // 파라미터값 보내기
            if ((new TYUTIN002I(this.FPS91_TY_S_UT_669HG121.GetValue("VNCODE").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fsGUBUN = "UPT";

                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion
    }
}