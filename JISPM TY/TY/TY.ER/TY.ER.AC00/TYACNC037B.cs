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
    /// 투하자금이자 생성 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2018.12.14 16:52
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_8CHBG322 : 투하자금 이자 계산 내역 조회
    ///  TY_P_AC_8CHBG323 : 투하자금 이자 등록
    ///  TY_P_AC_8CHBH324 : 투하자금 이자 삭제
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYACNC037B : TYBase
    {
        public TYACNC037B()
        {
            InitializeComponent();
        }

        #region Description : Page_Load
        private void TYACNC037B_Load(object sender, System.EventArgs e)
        {

            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));
            this.DTP01_SDATE.Focus();

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);
        } 
        #endregion

        #region Description : 처리
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            double dAJMCPNUAMT = 0;
            double dAJMINTERAMT = 0;

            //투하자금 이자계산
            //삭제후 등록
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_8CHBH324", this.DTP01_SDATE.GetString().ToString().Substring(0, 6));
            this.DbConnector.ExecuteTranQueryList();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_8CHBG322", this.DTP01_SDATE.GetString().ToString().Substring(0, 6));
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count != 0)
            {
                this.DbConnector.CommandClear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToDouble(dt.Rows[i]["B4AMDR"].ToString()) > 0 && Convert.ToDouble(dt.Rows[i]["BNYINTERRATE"].ToString()) > 0)
                    {
                        dAJMINTERAMT = Math.Floor(Convert.ToDouble(dt.Rows[i]["B4AMDR"].ToString()) * (Convert.ToDouble(dt.Rows[i]["BNYINTERRATE"].ToString()) / 100));

                        if (dAJMCPNUAMT > 0)
                        {
                            dAJMINTERAMT = Convert.ToDouble(dt.Rows[i]["B4AMDR"].ToString()) - dAJMCPNUAMT;
                        }

                        this.DbConnector.Attach("TY_P_AC_8CHBG323", this.DTP01_SDATE.GetString().ToString().Substring(0, 4),
                                                                    this.DTP01_SDATE.GetString().ToString().Substring(4, 2),
                                                                    dt.Rows[i]["BNDPAC"].ToString(),
                                                                    dt.Rows[i]["B4AMDR"].ToString(),
                                                                    dAJMINTERAMT,
                                                                    dt.Rows[i]["BNYINTERRATE"].ToString(),
                                                                    TYUserInfo.EmpNo
                                                                    );
                        dAJMCPNUAMT += dAJMINTERAMT;
                    }
                }
                if (this.DbConnector.CommandCount > 0)
                    this.DbConnector.ExecuteTranQueryList();
            }

            //프로젝트이자계산

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_AB5G5138", this.DTP01_SDATE.GetString().ToString().Substring(0, 6));
            this.DbConnector.ExecuteTranQueryList();


            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_AB5F0136", this.DTP01_SDATE.GetString().ToString().Substring(0, 6));
            DataTable dm = this.DbConnector.ExecuteDataTable();
            if (dm.Rows.Count != 0)
            {
                this.DbConnector.CommandClear();
                for (int i = 0; i < dm.Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_AB5FX137", dm.Rows[i]["AJNPJGB"].ToString(),
                                                                dm.Rows[i]["AJNDATE"].ToString(),
                                                                dm.Rows[i]["AJNDPAC"].ToString(),
                                                                dm.Rows[i]["PDATE"].ToString(),
                                                                dm.Rows[i]["AJNINAMT"].ToString(),
                                                                dm.Rows[i]["AJRRATE"].ToString(),
                                                                dm.Rows[i]["MONTHIJAMT"].ToString(),
                                                                   TYUserInfo.EmpNo
                                                                   );
                }
                if (this.DbConnector.CommandCount > 0)
                    this.DbConnector.ExecuteTranQueryList();

            }

            this.ShowMessage("TY_M_MR_2BF50354");
        } 
        #endregion

        #region Description : 닫기
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        } 
        #endregion

        #region Description : 처리 CHECK
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            //if (Convert.ToInt16(this.DTP01_SDATE.GetString().ToString().Substring(0, 4)) < 2019)
            //{
            //    this.ShowCustomMessage("2019년 이전 자료는 생성이 불가합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //    e.Successed = false;
            //    return;
            //}

            if (!this.ShowMessage("TY_M_MR_2BF50353"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion
    }
}
