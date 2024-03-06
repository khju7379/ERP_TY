using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 사업계획 예산 생성 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2018.05.23 09:39
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_85ID0067 : 사업계획 인사 4대보험 예산 생성 SP
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  CREATE : 생성
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRBS003B : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRBS003B()
        {
            InitializeComponent();
            this.SetPopupStyle();
        }

        private void TYHRBS003B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_CREATE.ProcessCheck += new TButton.CheckHandler(BTN61_CREATE_ProcessCheck);

            LBL52_BOTYEAR.Text = "예산구분";
            this.TXT01_BOTYEAR.SetValue(DateTime.Now.ToString("yyyy"));

            TXT01_BOTNATIORATE.SetReadOnly(true);
            TXT01_BOTEMPLOYRATE.SetReadOnly(true);
            TXT01_BOTHEALTRATE.SetReadOnly(true);
            TXT01_BOTLTERMRATE.SetReadOnly(true);
            TXT01_BOTSINDUSTRATE.SetReadOnly(true);
            TXT01_BOTUINDUSTRATE.SetReadOnly(true);

            this.SetStartingFocus(TXT01_BOTYEAR);
        }
        #endregion

        #region  Description : 생성 이벤트
        private void BTN61_CREATE_Click(object sender, EventArgs e)
        {
            string sOUT_MSG = string.Empty;

            //인건비 예산 
            if (CKB01_CHK_POERCOST.Checked)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_85PDK101",
                                        this.TXT01_BOTYEAR.GetValue().ToString(),
                                        TYUserInfo.EmpNo,
                                        sOUT_MSG.ToString()
                                        );
                sOUT_MSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                if (sOUT_MSG.Substring(0, 2).ToString() == "OK")
                {
                    this.ShowCustomMessage("인건비 예산이 생산되었습니다! ", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
                else
                {
                    this.ShowMessage("TY_M_GB_26E31876");
                }
            }

            if (CKB01_CHK_INSCOST.Checked)
            {
                //4대보험 예산
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_85ID0067",
                                        this.TXT01_BOTYEAR.GetValue().ToString(),
                                        this.TXT01_BOTNATIORATE.GetValue().ToString(),
                                        this.TXT01_BOTHEALTRATE.GetValue().ToString(),
                                        this.TXT01_BOTEMPLOYRATE.GetValue().ToString(),
                                        this.TXT01_BOTLTERMRATE.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_BOTUINDUSTRATE.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_BOTSINDUSTRATE.GetValue().ToString()),
                                        TYUserInfo.EmpNo,
                                        sOUT_MSG.ToString()
                                        );

                sOUT_MSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                if (sOUT_MSG.Substring(0, 2).ToString() == "OK")
                {
                    this.ShowCustomMessage("4대보험 예산이 생산되었습니다! ", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
                else
                {
                    this.ShowMessage("TY_M_GB_26E31876");
                }
            }
           
        }
        private void BTN61_CREATE_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int iCnt = 0;           
            
            if (CKB01_CHK_POERCOST.Checked)
            {

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_85SDA117",
                                        this.TXT01_BOTYEAR.GetValue().ToString()
                                        );
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());
                if (iCnt > 0)
                {
                    this.ShowCustomMessage("인건비 예산 자료가 이미 사업계획으로 전송되었습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_85PEP103",
                                        this.TXT01_BOTYEAR.GetValue().ToString()
                                        );
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());
                if (iCnt <= 0)
                {
                    this.ShowCustomMessage("인건비 급여 자료가 존재하지않습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }

            }

            if (CKB01_CHK_INSCOST.Checked)
            {

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_85SDA116",
                                        this.TXT01_BOTYEAR.GetValue().ToString()
                                        );
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());
                if (iCnt > 0)
                {
                    this.ShowCustomMessage("4대보험 예산 자료가 이미 사업계획으로 전송되었습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }

                //4대보험 실적자료가 존재하는지 체크 this.DbConnector.CommandClear();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_85PEM102",
                                        this.TXT01_BOTYEAR.GetValue().ToString()
                                        );
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());
                if (iCnt <= 0)
                {
                    this.ShowCustomMessage("4대보험 급여실적 자료가 존재하지않습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }

                if (this.TXT01_BOTNATIORATE.GetValue().ToString() == "" ||
                    Convert.ToDouble(Get_Numeric(this.TXT01_BOTNATIORATE.GetValue().ToString())) <= 0)
                {
                    this.SetFocus(TXT01_BOTNATIORATE);
                    this.ShowCustomMessage("국민연금 요율을 입력하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }

                if (this.TXT01_BOTHEALTRATE.GetValue().ToString() == "" ||
                   Convert.ToDouble(Get_Numeric(this.TXT01_BOTHEALTRATE.GetValue().ToString())) <= 0)
                {
                    this.SetFocus(TXT01_BOTHEALTRATE);
                    this.ShowCustomMessage("건강보험 요율을 입력하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }

                if (this.TXT01_BOTEMPLOYRATE.GetValue().ToString() == "" ||
                  Convert.ToDouble(Get_Numeric(this.TXT01_BOTEMPLOYRATE.GetValue().ToString())) <= 0)
                {
                    this.SetFocus(TXT01_BOTEMPLOYRATE);
                    this.ShowCustomMessage("고용보험 요율을 입력하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }

                if (this.TXT01_BOTLTERMRATE.GetValue().ToString() == "" ||
                Convert.ToDouble(Get_Numeric(this.TXT01_BOTLTERMRATE.GetValue().ToString())) <= 0)
                {
                    this.SetFocus(TXT01_BOTLTERMRATE);
                    this.ShowCustomMessage("장기요양보험 요율을 입력하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_26E2Z874"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : 닫기 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region  Description : CKB01_CHK_INSCOST_CheckedChanged 이벤트
        private void CKB01_CHK_INSCOST_CheckedChanged(object sender, EventArgs e)
        {

            if (CKB01_CHK_INSCOST.Checked)
            {
                TXT01_BOTNATIORATE.SetReadOnly(false);
                TXT01_BOTEMPLOYRATE.SetReadOnly(false);
                TXT01_BOTHEALTRATE.SetReadOnly(false);
                TXT01_BOTLTERMRATE.SetReadOnly(false);
                TXT01_BOTSINDUSTRATE.SetReadOnly(false);
                TXT01_BOTUINDUSTRATE.SetReadOnly(false);
            }
            else
            {
                TXT01_BOTNATIORATE.SetReadOnly(true);
                TXT01_BOTEMPLOYRATE.SetReadOnly(true);
                TXT01_BOTHEALTRATE.SetReadOnly(true);
                TXT01_BOTLTERMRATE.SetReadOnly(true);
                TXT01_BOTSINDUSTRATE.SetReadOnly(true);
                TXT01_BOTUINDUSTRATE.SetReadOnly(true);
            }
        }
        #endregion

    }
}
