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
    public partial class TYHRBS004B : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRBS004B()
        {
            InitializeComponent();
            this.SetPopupStyle();
        }

        private void TYHRBS004B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_CREATE_ProcessCheck);

            LBL52_BOTYEAR.Text = "예산구분";
            this.TXT01_BOTYEAR.SetValue(DateTime.Now.ToString("yyyy"));

            this.SetStartingFocus(TXT01_BOTYEAR);
        }
        #endregion

        #region  Description : 생성 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sOUT_MSG = string.Empty;

            if (CBO01_GOKCR.GetValue().ToString() == "A")
            {
                //인건비 예산 
                if (CKB01_CHK_POERCOST.Checked)
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_HR_85SBM113",
                                            "CM",
                                            TYUserInfo.DeptCode,
                                            this.TXT01_BOTYEAR.GetValue().ToString()
                                            );

                    this.DbConnector.Attach("TY_P_HR_85PHJ106",
                                            "CM",
                                            TYUserInfo.DeptCode,
                                            TYUserInfo.EmpNo,
                                            this.TXT01_BOTYEAR.GetValue().ToString()
                                            );
                    this.DbConnector.Attach("TY_P_HR_85PHK107",
                                            "Y",
                                            TYUserInfo.EmpNo,
                                            this.TXT01_BOTYEAR.GetValue().ToString()
                                            );
                    this.DbConnector.ExecuteTranQueryList();

                    this.ShowCustomMessage("인건비 예산 자료가 전송되었습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }

                if (CKB01_CHK_INSCOST.Checked)
                {
                    //4대보험 예산
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_HR_85SBL112",
                                          "CM",
                                          TYUserInfo.DeptCode,
                                          this.TXT01_BOTYEAR.GetValue().ToString()
                                          );

                    this.DbConnector.Attach("TY_P_HR_85S9G108",
                                            "CM",
                                            TYUserInfo.DeptCode,
                                            TYUserInfo.EmpNo,
                                            this.TXT01_BOTYEAR.GetValue().ToString()
                                            );

                    this.DbConnector.Attach("TY_P_HR_85S9H109",
                                            "Y",
                                            TYUserInfo.EmpNo,
                                            this.TXT01_BOTYEAR.GetValue().ToString()
                                            );
                    this.DbConnector.ExecuteTranQueryList();

                    this.ShowCustomMessage("4대보험 예산 자료가 전송되었습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
            }
            else
            {
                //인건비 예산 
                if (CKB01_CHK_POERCOST.Checked)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_85SBM113",
                                            "CM",
                                            TYUserInfo.DeptCode,
                                            this.TXT01_BOTYEAR.GetValue().ToString()
                                            );
                    this.DbConnector.Attach("TY_P_HR_85PHK107",
                                            "N",
                                            TYUserInfo.EmpNo,
                                            this.TXT01_BOTYEAR.GetValue().ToString()
                                            );
                    this.DbConnector.ExecuteTranQueryList();

                    this.ShowCustomMessage("인건비 예산 자료가 취소되었습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }

                if (CKB01_CHK_INSCOST.Checked)
                {
                    //4대보험 예산
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_HR_85SBL112",
                                           "CM",
                                           TYUserInfo.DeptCode,
                                           this.TXT01_BOTYEAR.GetValue().ToString()
                                          );

                    this.DbConnector.Attach("TY_P_HR_85S9H109",
                                             "N",
                                            TYUserInfo.EmpNo,
                                            this.TXT01_BOTYEAR.GetValue().ToString()
                                            );
                    this.DbConnector.ExecuteTranQueryList();

                    this.ShowCustomMessage("4대보험 예산 자료가 취소되었습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
            }

           
        }
        private void BTN61_CREATE_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            //사업계획 마감 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_78SDI500",
                                   this.TXT01_BOTYEAR.GetValue().ToString()
                                  );
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["BLCHKCM"].ToString() == "Y")
                {
                    this.ShowCustomMessage("사업계획 마감이 완료되었습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            if (CBO01_GOKCR.GetValue().ToString() == "A")
            {
                //전송
                if (CKB01_CHK_POERCOST.Checked != true && CKB01_CHK_INSCOST.Checked != true)
                {
                    this.ShowCustomMessage("인건비 또는 4대보험 예산중 하나는 체크 되어야 합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }

                if (CKB01_CHK_POERCOST.Checked)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_85SAT110",
                                            this.TXT01_BOTYEAR.GetValue().ToString()
                                            );
                    int iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());
                    if (iCnt <= 0)
                    {
                        this.ShowCustomMessage("인건비 예산 자료가 존재하지않습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }

                if (CKB01_CHK_INSCOST.Checked)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_85SAV111",
                                            this.TXT01_BOTYEAR.GetValue().ToString()
                                            );
                    int iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());
                    if (iCnt <= 0)
                    {
                        this.ShowCustomMessage("4대보험 예산 자료가 존재하지않습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }           

            if (CBO01_GOKCR.GetValue().ToString() == "A")
            {
                if (!this.ShowMessage("TY_M_UT_74OAE364"))
                {
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (!this.ShowMessage("TY_M_MR_35O21733"))
                {
                    e.Successed = false;
                    return;
                }
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

           
        }
        #endregion

    }
}
