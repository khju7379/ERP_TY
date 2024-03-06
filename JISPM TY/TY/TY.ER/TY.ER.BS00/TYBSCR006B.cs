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
    /// 사업계획 손익계산서 생성 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.09.01 15:19
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_78TH3505 : 사업계획 손익 생성 SP
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_MR_35O21733 : 취소하시겠습니까?
    ///  TY_M_MR_35O2G735 : 취소하였습니다.
    ///  TY_M_UT_721E3629 : 생성 하시겠습니까?
    ///  TY_M_UT_721E4630 : 생성 작업이 완료 되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  GOKCR : 생성구분
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYBSCR006B : TYBase
    {
        private object _TXT01_SDATE_Value;
        private object _CBO01_GOKCR_Value;
        private object _WKSABUN_Value;

        private string fsProceDures;

        #region  Description : 폼 로드 이벤트
        public TYBSCR006B()
        {
            InitializeComponent();

            this.SetPopupStyle();

        }

        private void TYBSCR006B_Load(object sender, System.EventArgs e)
        {

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.BTN61_BATCH.IsAsynchronous = true;

            this.TXT01_SDATE.SetValue(DateTime.Now.ToString("yyyy"));

            this.SetStartingFocus(this.TXT01_SDATE);
        }
        #endregion

        #region  Description : 처리 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            _TXT01_SDATE_Value = TXT01_SDATE.GetValue().ToString();
            _CBO01_GOKCR_Value = CBO01_GOKCR.GetValue().ToString();
            _WKSABUN_Value = TYUserInfo.EmpNo;
        }

        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            _CBO01_GOKCR_Value = CBO01_GOKCR.GetValue().ToString();
            _TXT01_SDATE_Value = TXT01_SDATE.GetValue().ToString();

            if (CBO01_GOKCR.GetValue().ToString() == "A")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_78SDI500", _TXT01_SDATE_Value);
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["BLCHKMC"].ToString() != "Y" || dt.Rows[0]["BLCHKCM"].ToString() != "Y" || dt.Rows[0]["BLCHKPR"].ToString() != "Y" || dt.Rows[0]["BLCHKIN"].ToString() != "Y")
                    {
                        this.ShowCustomMessage("기초자료 마감 완료후 생성하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }


            if (_CBO01_GOKCR_Value.ToString() == "A")
            {
                if (!this.ShowMessage("TY_M_GB_26E2Z874"))
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

        #region  Description : BTN61_BATCH_Invoker 이벤트
        private void BTN61_BATCH_InvokerStart(object sender, TButton.ClickEventCheckArgs e)
        {
            e.DbConnector.CommandClear();
            e.DbConnector.Attach("TY_P_AC_78TH3505", _CBO01_GOKCR_Value, _TXT01_SDATE_Value, _WKSABUN_Value, "");
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

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

       

       
    }
}
