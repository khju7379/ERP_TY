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
    ///  TY_S_UT_71DA4440 : 거래처관리 조회
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
    public partial class TYUTAU003S : TYBase
    {
        #region Description : 페이지 로드
        public TYUTAU003S()
        {
            InitializeComponent();
        }

        private void TYUTAU003S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_71DA6439",
                Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                this.CBH01_SHWAJU.GetValue().ToString(),
                this.CBH01_SHWAMUL.GetValue().ToString(),
                this.TXT01_SFTANKNO.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_71DA4440.SetValue(dt);
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYUTAU003I(string.Empty, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.CommandClear();

                // 작업지시
                this.DbConnector.Attach("TY_P_UT_71DBP449", dt.Rows[i]["JISTDATE"].ToString(),
                                                            dt.Rows[i]["JIENDATE"].ToString(),
                                                            dt.Rows[i]["JISIIL"].ToString(),
                                                            dt.Rows[i]["JISISQ"].ToString()
                                                            );

                // 자동화 TANK자동화 HISTORY
                this.DbConnector.Attach("TY_P_UT_71DBO448", TYUserInfo.EmpNo,
                                                            "D",
                                                            dt.Rows[i]["JISIIL"].ToString(),
                                                            dt.Rows[i]["JISISQ"].ToString(),
                                                            dt.Rows[i]["JISIHT"].ToString(),
                                                            dt.Rows[i]["JISITK"].ToString(),
                                                            dt.Rows[i]["JIIPHANG"].ToString(),
                                                            dt.Rows[i]["JISIVS"].ToString(),
                                                            dt.Rows[i]["JISIVSNM"].ToString(),
                                                            dt.Rows[i]["JISIHJ"].ToString(),
                                                            dt.Rows[i]["JISIHJNM"].ToString(),
                                                            dt.Rows[i]["JISIHM"].ToString(),
                                                            dt.Rows[i]["JISIHMNM"].ToString(),
                                                            dt.Rows[i]["JISIQTY"].ToString(),
                                                            dt.Rows[i]["JISTDATE"].ToString(),
                                                            dt.Rows[i]["JISTTIME"].ToString(),
                                                            dt.Rows[i]["JIENDATE"].ToString(),
                                                            dt.Rows[i]["JIENTIME"].ToString(),
                                                            dt.Rows[i]["JISIIGTK"].ToString(),
                                                            dt.Rows[i]["JISABUN"].ToString(),
                                                            dt.Rows[i]["JISABUNNM"].ToString(),
                                                            dt.Rows[i]["JIVCF"].ToString(),
                                                            dt.Rows[i]["JIWCF"].ToString(),
                                                            dt.Rows[i]["JIPUMP"].ToString(),
                                                            dt.Rows[i]["JISISJ"].ToString(),
                                                            dt.Rows[i]["JISIWT"].ToString()
                                                            );


                this.DbConnector.ExecuteTranQueryList();
            }

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // 삭제 체크되어 있는 행의 칼럼(VNCODE)을 가져와서 체크하는 부분
            DataTable dt = this.FPS91_TY_S_UT_71DA4440.GetDataSourceInclude(TSpread.TActionType.Remove, "JISIIL",   "JISISQ",   "JISIWT",   "JISIHT",   "JISTDATE", "JIENDATE",  "JISITK",
                                                                                                        "JIIPHANG", "JISIVS",   "JISIVSNM", "JISIHJ",   "JISIHJNM", "JISIHM",    "JISIHMNM",
                                                                                                        "JISIQTY",  "JISTTIME", "JIENTIME", "JISIIGTK", "JISABUN",  "JISABUNNM", "JIVCF",
                                                                                                        "JIWCF",    "JIPUMP",   "JISISJ");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            DataTable dt1 = new DataTable();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["JISIWT"].ToString() == "Y")
                {
                    this.ShowMessage("TY_M_UT_71DAV443");
                    e.Successed = false;
                    return;
                }

                if (dt.Rows[i]["JISIHT"].ToString() == "1" || dt.Rows[i]["JISIHT"].ToString() == "4")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach(
                                           "TY_P_UT_71DBE445",
                                           dt.Rows[i]["JISIIL"].ToString(),
                                           dt.Rows[i]["JISISQ"].ToString()
                                           );

                    dt1 = this.DbConnector.ExecuteDataTable();

                    if (dt1.Rows.Count > 0)
                    {
                        if (Int32.Parse(dt1.Rows[0]["VJJBSTIL"].ToString()) != 0)
                        {
                            this.ShowMessage("TY_M_UT_71DBG446");
                            e.Successed = false;
                            return;
                        }

                        if (Int32.Parse(dt1.Rows[0]["VJJBENIL"].ToString()) != 0)
                        {
                            this.ShowMessage("TY_M_UT_71DBG447");
                            e.Successed = false;
                            return;
                        }
                    }
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
        private void FPS91_TY_S_UT_71DA4440_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            // 파라미터값 보내기
            if ((new TYUTAU003I(this.FPS91_TY_S_UT_71DA4440.GetValue("JISIIL").ToString(), this.FPS91_TY_S_UT_71DA4440.GetValue("JISISQ").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion
    }
}