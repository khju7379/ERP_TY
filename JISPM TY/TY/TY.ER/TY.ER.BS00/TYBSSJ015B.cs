using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.BS00
{
    /// <summary>
    /// 당기실적 마감관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.10.23 11:09
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_7ANBA867 : 당기실적 마감관리 등록
    ///  TY_P_AC_7ANBC868 : 당기실적 마감관리 수정
    ///  TY_P_AC_7ANBD869 : 당기실적 마감관리 확인
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  BLJYYMM : 실적생성년월
    ///  GOKCR : 생성구분
    /// </summary>
    public partial class TYBSSJ015B : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYBSSJ015B()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }

        private void TYBSSJ015B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.CBH01_BLJYYMM.SetValue(UP_Get_LastSJYYMM());
        }
        #endregion

        #region  Description : 처리 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sCKB01_BPCHK_MA = string.Empty;
            string sCKB01_BPCHK_IN = string.Empty;
            string sCKB01_BPCHK_CM = string.Empty;
            string sCKB01_BPCHK_PR = string.Empty;            

            sCKB01_BPCHK_MA = CKB01_BPCHK_MA.Checked == true ? "Y" : "N";
            sCKB01_BPCHK_IN = CKB01_BPCHK_IN.Checked == true ? "Y" : "N";
            sCKB01_BPCHK_CM = CKB01_BPCHK_CM.Checked == true ? "Y" : "N";
            sCKB01_BPCHK_PR = CKB01_BPCHK_PR.Checked == true ? "Y" : "N";

            try
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_7ANBD869", this.CBH01_BLJYYMM.GetValue().ToString(), this.CBH01_BLJYYMM.GetValue().ToString().Substring(0,4));
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_7ANBC868", sCKB01_BPCHK_MA,
                                                                sCKB01_BPCHK_CM,
                                                                sCKB01_BPCHK_PR,
                                                                sCKB01_BPCHK_IN,                                                                
                                                                 TYUserInfo.EmpNo,
                                                                this.CBH01_BLJYYMM.GetValue().ToString(), this.CBH01_BLJYYMM.GetValue().ToString().Substring(0, 4)
                                                                );
                    this.DbConnector.ExecuteTranQuery();
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_7ANBA867", this.CBH01_BLJYYMM.GetValue().ToString(), this.CBH01_BLJYYMM.GetValue().ToString().Substring(0, 4),
                                                                sCKB01_BPCHK_MA, sCKB01_BPCHK_CM, sCKB01_BPCHK_PR, sCKB01_BPCHK_IN, TYUserInfo.EmpNo
                                                                );
                    this.DbConnector.ExecuteTranQuery();

                }

            }
            catch (Exception ex)
            {
                string ddd = ex.Message;
            }


            this.ShowMessage("TY_M_MR_2BF50354");
        }
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            if (!this.ShowMessage("TY_M_MR_2BF50353"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : CBH01_BLJYYMM_CodeBoxDataBinded
        private void CBH01_BLJYYMM_CodeBoxDataBinded(object sender, EventArgs e)
        {

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_7ANBD869", this.CBH01_BLJYYMM.GetValue().ToString(), this.CBH01_BLJYYMM.GetValue().ToString().Substring(0, 4));
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                CKB01_BPCHK_MA.Checked = dt.Rows[0]["BLJCHKMC"].ToString() == "Y" ? true : false;
                CKB01_BPCHK_IN.Checked = dt.Rows[0]["BLJCHKIN"].ToString() == "Y" ? true : false;
                CKB01_BPCHK_CM.Checked = dt.Rows[0]["BLJCHKCM"].ToString() == "Y" ? true : false;
                CKB01_BPCHK_PR.Checked = dt.Rows[0]["BLJCHKPR"].ToString() == "Y" ? true : false;
            }
        }
        #endregion

        #region Description : 최종 실적년월 가져오기
        private string UP_Get_LastSJYYMM()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_7AKAW859");
            string sYYMM = this.DbConnector.ExecuteScalar().ToString();

            return sYYMM;
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region  Description :  CKB01_BPCHK_ALL_CheckedChanged 이벤트
        private void CKB01_BPCHK_ALL_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_BPCHK_ALL.Checked == true)
            {
                CKB01_BPCHK_MA.Checked = true;
                CKB01_BPCHK_IN.Checked = true;
                CKB01_BPCHK_CM.Checked = true;
                CKB01_BPCHK_PR.Checked = true;
            }
            else
            {
                CKB01_BPCHK_MA.Checked = false;
                CKB01_BPCHK_IN.Checked = false;
                CKB01_BPCHK_CM.Checked = false;
                CKB01_BPCHK_PR.Checked = false;
            }
        }
        #endregion

    }
}
