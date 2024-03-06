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
    /// 사업계획 기초자료 삭제 팝업 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.08.28 11:26
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  REM : 삭제
    ///  BLCHKMC : 매출
    ///  KBBSTEAM : 부서(반)
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYBSUP01C1 : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYBSUP01C1()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }

        private void TYBSUP01C1_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_SDATE.SetValue(DateTime.Now.AddYears(1).ToString("yyyy"));

            this.CBH01_KBSABUN.SetValue(TYUserInfo.UserID);

            this.SetStartingFocus(this.DTP01_SDATE);
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {           
            
           
                this.DbConnector.CommandClear();

                if (CKB01_BPCHK_MA.Checked == true)
                {
                    this.DbConnector.Attach("TY_P_AC_78SE0501", "MA", DTP01_SDATE.GetString().ToString().Substring(0, 4), CBH01_KBSABUN.GetValue().ToString() );
                }
                if (CKB01_BPCHK_SU.Checked == true)
                {
                    this.DbConnector.Attach("TY_P_AC_78SE0501", "SU", DTP01_SDATE.GetString().ToString().Substring(0, 4), CBH01_KBSABUN.GetValue().ToString());
                }

                if (CKB01_BPCHK_IN.Checked == true)
                {
                    this.DbConnector.Attach("TY_P_AC_78SEG503", "IN", DTP01_SDATE.GetString().ToString().Substring(0, 4), CBH01_KBSABUN.GetValue().ToString());
                }
                if (CKB01_BPCHK_RE.Checked == true)
                {
                    this.DbConnector.Attach("TY_P_AC_78SEG503", "RE", DTP01_SDATE.GetString().ToString().Substring(0, 4), CBH01_KBSABUN.GetValue().ToString());
                }

                if (CKB01_BPCHK_CM.Checked == true)
                {
                    this.DbConnector.Attach("TY_P_AC_78SEE502", "CM", DTP01_SDATE.GetString().ToString().Substring(0, 4), CBH01_KBSABUN.GetValue().ToString(), "CM", DTP01_SDATE.GetString().ToString().Substring(0, 4), "CM", DTP01_SDATE.GetString().ToString().Substring(0, 4));
                }
                if (CKB01_BPCHK_SE.Checked == true)
                {
                    this.DbConnector.Attach("TY_P_AC_78SEE502", "SE", DTP01_SDATE.GetString().ToString().Substring(0, 4), CBH01_KBSABUN.GetValue().ToString(), "SE", DTP01_SDATE.GetString().ToString().Substring(0, 4), "SE", DTP01_SDATE.GetString().ToString().Substring(0, 4));
                }
                if (CKB01_BPCHK_PR.Checked == true)
                {
                    this.DbConnector.Attach("TY_P_AC_78SEE502", "PR", DTP01_SDATE.GetString().ToString().Substring(0, 4), CBH01_KBSABUN.GetValue().ToString(), "PR", DTP01_SDATE.GetString().ToString().Substring(0, 4), "PR", DTP01_SDATE.GetString().ToString().Substring(0, 4));
                }
                if (CKB01_BPCHK_CO.Checked == true)
                {
                    this.DbConnector.Attach("TY_P_AC_78SEE502", "CO", DTP01_SDATE.GetString().ToString().Substring(0, 4), CBH01_KBSABUN.GetValue().ToString(), "CO", DTP01_SDATE.GetString().ToString().Substring(0, 4), "CO", DTP01_SDATE.GetString().ToString().Substring(0, 4));
                }                    
                
                if (this.DbConnector.CommandCount > 0)
                    this.DbConnector.ExecuteTranQueryList();
            

            this.ShowMessage("TY_M_GB_23NAD874");                    
        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            //마감체크         
                       
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_78SDI500", DTP01_SDATE.GetString().ToString().Substring(0,4) );
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                        if (CKB01_BPCHK_MA.Checked == true || CKB01_BPCHK_SU.Checked == true )
                        {
                            if (dt.Rows[0]["BLCHKMC"].ToString() == "Y")
                            {
                                this.ShowCustomMessage("매출액,취급량은 기초자료 마감되어 삭제할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                e.Successed = false;
                                return;
                            }
                        }

                        if (CKB01_BPCHK_CM.Checked == true || CKB01_BPCHK_SE.Checked == true)
                        {
                            if (dt.Rows[0]["BLCHKCM"].ToString() == "Y")
                            {
                                this.ShowCustomMessage("영업비용(공통,자체) 기초자료 마감되어 삭제할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                e.Successed = false;
                                return;
                            }
                        }
                        if (CKB01_BPCHK_PR.Checked == true || CKB01_BPCHK_CO.Checked == true)
                        {
                            if (dt.Rows[0]["BLCHKPR"].ToString() == "Y")
                            {
                                this.ShowCustomMessage("영업외손익,비용은 기초자료 마감되어 삭제할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                e.Successed = false;
                                return;
                            }
                        }
                        if (CKB01_BPCHK_IN.Checked == true || CKB01_BPCHK_RE.Checked == true)
                        {
                            if (dt.Rows[0]["BLCHKIN"].ToString() == "Y")
                            {
                                this.ShowCustomMessage("투자.수선 기초자료 마감되어 삭제할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                e.Successed = false;
                                return;
                            }
                        }
                    
                }
            

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }            

        }

        #endregion

        #region  Description : UP_Set_LockCheck 이벤트
        private Boolean UP_Set_LockCheck()
        {
            bool bResult = true;

            //강경석, 방재신, 임규철, 임경화
            if (TYUserInfo.EmpNo == "0269-M" || TYUserInfo.EmpNo == "0416-M" || TYUserInfo.EmpNo == "0150-M" || TYUserInfo.EmpNo == "0287-M")
            {
                bResult = false;
            }

            return bResult;
        }
        #endregion

        #region  Description : DTP01_SDATE_ValueChanged 이벤트
        private void DTP01_SDATE_ValueChanged(object sender, EventArgs e)
        {
            //this.CBH01_KBBSTEAM.DummyValue = this.DTP01_SDATE.GetString().ToString().Substring(0, 4) + "0101";
        }
        #endregion


        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void CHK01_BPCHK_ALL_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_BPCHK_ALL.Checked == true)
            {
                CKB01_BPCHK_MA.Checked = true;
                CKB01_BPCHK_SU.Checked = true;
                CKB01_BPCHK_IN.Checked = true;
                CKB01_BPCHK_RE.Checked = true;
                CKB01_BPCHK_CM.Checked = true;
                CKB01_BPCHK_SE.Checked = true;
                CKB01_BPCHK_PR.Checked = true;
                CKB01_BPCHK_CO.Checked = true;
            }
            else
            {
                CKB01_BPCHK_MA.Checked = false;
                CKB01_BPCHK_SU.Checked = false;
                CKB01_BPCHK_IN.Checked = false;
                CKB01_BPCHK_RE.Checked = false;
                CKB01_BPCHK_CM.Checked = false;
                CKB01_BPCHK_SE.Checked = false;
                CKB01_BPCHK_PR.Checked = false;
                CKB01_BPCHK_CO.Checked = false;
            }
        }


    }
}
