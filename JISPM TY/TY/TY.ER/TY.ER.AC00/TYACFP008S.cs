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
    /// 화물료 납부전표관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2018.02.23 10:30
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_82NE1631 : 화물료 납부전표관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_82NE3633 : 화물료 납부전표관리 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  SAV : 저장
    ///  GGUBUN : 구분
    ///  INQOPTION : 조회구분
    ///  EDATE : 종료일자
    ///  HMHWAJU : 화주
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYACFP008S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYACFP008S()
        {
            InitializeComponent();
        }

        private void TYACFP008S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.DTP01_SDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.SetStartingFocus(DTP01_SDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_82NE3633.Initialize();
            
            this.DbConnector.CommandClear();
            // 20190114 수정전 소스
            //this.DbConnector.Attach("TY_P_AC_82NE1631", this.CBO01_INQOPTION.GetValue(), DTP01_SDATE.GetString().ToString(), DTP01_EDATE.GetString().ToString(), 
            //                                            CBH01_HMHWAJU.GetValue().ToString(), CBO01_GGUBUN.GetValue().ToString(),
            //                                            TXT01_HMJPNO.GetValue().ToString(), TXT01_HMBJJPNO.GetValue().ToString() );

            // 20190114 수정 후 소스
            this.DbConnector.Attach("TY_P_AC_91EF1496", this.CBO01_INQOPTION.GetValue(),
                                                        this.DTP01_SDATE.GetString().ToString(),  this.DTP01_EDATE.GetString().ToString(),
                                                        this.CBH01_HMHWAJU.GetValue().ToString(), this.CBO01_GGUBUN.GetValue().ToString(),
                                                        this.TXT01_HMJPNO.GetValue().ToString(),  this.TXT01_HMSEJPNO.GetValue().ToString(),
                                                        this.TXT01_HMBJJPNO.GetValue().ToString()
                                                        );

            this.FPS91_TY_S_AC_82NE3633.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;            

            if ((new TYACFP008I(dt, "A")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            DataTable dt = this.FPS91_TY_S_AC_82NE3633.GetDataSourceInclude(TSpread.TActionType.Select,"HMDATE","HMPAYDAT", "HMIPHANG", "HMCHDAT", "HMBONSUN", "VSDESC1", "HMHWAJU", "VNSANGHO", "HMSHQTY", "HMDANGA", "HMYUL", "HMBBLS", "HMCOMPBBLS", "HMCOMPAMT", "HMCHARGEAMT", "HMJPNO", "HMBJJPNO", "HMCRDATE","VNCODE", "HMSECOMPAMT", "HMSECHARGEAMT", "HMSEJPNO", "HMIPGOGB");

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_AC_25F59464");
                e.Successed = false;
                return;
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["HMBJJPNO"].ToString().Trim() != "")
                    {
                        this.ShowCustomMessage("납부전표가 발행된 자료가 존재합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }


            e.ArgData = dt;
        }
        #endregion

        #region  Description : FPS91_TY_S_AC_82NE3633_CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_82NE3633_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.FPS91_TY_S_AC_82NE3633.GetValue("HMBJJPNO").ToString() != "")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_82QG2651", this.FPS91_TY_S_AC_82NE3633.GetValue("HMBJJPNO").ToString().Substring(0, 17));
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if ((new TYACFP008I(dt, "D")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion
    }
}
