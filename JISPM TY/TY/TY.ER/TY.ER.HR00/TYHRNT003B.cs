using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using System.IO;


namespace TY.ER.HR00
{
    /// <summary>
    /// 연말정산 첨부파일 조회 팝업 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.07.28 08:54
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_77S8Z291 : 연말정산 첨부파일 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_77S8Z293 : 연말정산 첨부파일 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    /// </summary>
    public partial class TYHRNT003B : TYBase
    {
        private string fsYACOMPANY;

        private object _TXT01_SDATE_Value;
        private object _CBH01_KBSABUN_Value;
        private object _CBO01_GOKCR_Value;
        private object _CBO01_INQOPTION_Value;
        private object _WKSABUN_Value;
        private object _DTP01_PMYYMM_Value;
        private object _DTP01_RESIGNDATE_Value;

        #region  Description : 폼 로드 이벤트
        public TYHRNT003B(string sYACOMPANY)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsYACOMPANY = sYACOMPANY;
        }

        private void TYHRNT003B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.BTN61_BATCH.IsAsynchronous = true;

            TXT01_SDATE.SetValue(DateTime.Now.ToString("yyyy"));
            DTP01_PMYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            DTP01_RESIGNDATE.SetValue("");
            
        }
        #endregion      

        #region  Description : 연말정산 정산결과 생성 버튼 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {            
            _TXT01_SDATE_Value   = TXT01_SDATE.GetValue().ToString();
            _CBH01_KBSABUN_Value   = CBH01_KBSABUN.GetValue().ToString();
            _CBO01_INQOPTION_Value = CBO01_INQOPTION.GetValue().ToString();
            _CBO01_GOKCR_Value = CBO01_GOKCR.GetValue().ToString();
            _DTP01_PMYYMM_Value = DTP01_PMYYMM.GetString().Substring(0, 6);
            _DTP01_RESIGNDATE_Value = DTP01_RESIGNDATE.GetString().ToString();
            _WKSABUN_Value = TYUserInfo.EmpNo;

            if (CBO01_GOKCR.GetValue().ToString() != "D" && DTP01_PMYYMM.GetString().Substring(0, 6) == "")
            {
                this.ShowCustomMessage("연말정산 계산작업시에는 급여적용년월을 필수사항입니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (CBO01_INQOPTION.GetValue().ToString() == "2" && CBH01_KBSABUN.GetValue().ToString() == "" )
            {
                this.ShowCustomMessage("중도퇴사인경우 퇴사사번은 필수사항입니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }
            
            if (CBO01_INQOPTION.GetValue().ToString() == "2" &&  (DTP01_RESIGNDATE.GetString().ToString() == "" || DTP01_RESIGNDATE.GetString().ToString() == "19000101") )
            {
                this.ShowCustomMessage("중도퇴사인경우 퇴사일자는 필수사항입니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            //급여가 생성되었는지 체크            
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_HR_7CT9B380", "TY", TXT01_SDATE.GetValue().ToString(), CBH01_KBSABUN.GetValue().ToString(), CBO01_INQOPTION.GetValue().ToString(), ""                                                        
            //                                            );
            //DataTable dt = this.DbConnector.ExecuteDataTable();
            //if (dt.Rows.Count > 0)
            //{
            //    if (Convert.ToInt16(dt.Rows[0]["PYJICNT"].ToString()) > 0)
            //    {
            //        this.ShowCustomMessage("급여가 생성되었습니다! 급여 취소후 작업하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //        e.Successed = false;
            //        return;
            //    }
            //}

            //중도퇴사 경우 해당월 급여지급이후는 해당월로 등록할수 없다.
            if (CBO01_INQOPTION.GetValue().ToString() != "1")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_BC8E9897", DTP01_PMYYMM.GetString().Substring(0, 6), "M1"
                                                            );
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["APMJPNO"].ToString() != "")
                    {
                        this.ShowCustomMessage("급여가 생성되었습니다! 중도퇴사는 이후 급여월을 선택하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (!this.ShowMessage("TY_M_GB_26E2Z874"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : BTN61_BATCH_Invoker 버튼 이벤트
        private void BTN61_BATCH_InvokerStart(object sender, TButton.ClickEventCheckArgs e)
        {
            e.DbConnector.CommandClear();
            e.DbConnector.Attach("TY_P_HR_7BMED092", fsYACOMPANY, _TXT01_SDATE_Value, _CBH01_KBSABUN_Value, _CBO01_INQOPTION_Value, _CBO01_GOKCR_Value, _WKSABUN_Value, _DTP01_PMYYMM_Value, _DTP01_RESIGNDATE_Value, TYUserInfo.SecureKey, "Y", "N", "");
            e.DbConnector.ExecuteScalar();
        }
        private void BTN61_BATCH_InvokerEnd(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = e.ArgData as DataSet;

            if (ds.Tables[0].Rows[0][0].ToString().Substring(0, 2) != "OK")
            {
                this.ShowCustomMessage(ds.Tables[0].Rows[0][0].ToString(), "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            else
            {
                if (_CBO01_GOKCR_Value.ToString() == "A")
                {
                    this.ShowMessage("TY_M_GB_26E30875");
                }
                else
                {
                    this.ShowMessage("TY_M_MR_35O2G735");
                }
            }
        }
        #endregion

        #region  Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }








        #endregion
    }
}
