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
    /// 사업계획 마감관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.08.22 16:21
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_78MGQ480 : 사업계획 마감관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_78MGQ481 : 사업계획 마감관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    /// </summary>
    public partial class TYBSCR010B : TYBase
    {
        private object _TXT01_SDATE_Value;
        private object _WKSABUN_Value;


        #region  Description : 폼 로드 이벤트
        public TYBSCR010B()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }

        private void TYBSCR010B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);
            this.BTN61_BATCH.IsAsynchronous = true;

            this.TXT01_SDATE.SetValue(DateTime.Now.ToString("yyyy"));

            this.SetStartingFocus(this.TXT01_SDATE);
            
        }
        #endregion        

        #region  Description : 전송 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            _TXT01_SDATE_Value = TXT01_SDATE.GetValue().ToString();
            _WKSABUN_Value = TYUserInfo.EmpNo;
        }
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            Int16 iCnt = 0;

            _TXT01_SDATE_Value = TXT01_SDATE.GetValue().ToString();
            _WKSABUN_Value = TYUserInfo.EmpNo;


            //해당년도 예산이 등록되어 있으면 생성 불가
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_7AU9T907", TXT01_SDATE.GetValue().ToString());
            iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
            if (iCnt > 0)
            {
                this.ShowCustomMessage("해당년도 예산이 등록되어 있습니다! 생성할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            //해당년도 사업계획 자료가 없으면 생성 안됨
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_7AU9W908", TXT01_SDATE.GetValue().ToString(), TXT01_SDATE.GetValue().ToString());
            iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
            if (iCnt <= 0)
            {
                this.ShowCustomMessage("해당년도 사업계획가 존재하지 않습니다! ", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_HR_7ARFB905"))
            {
                e.Successed = false;
                return;
            }            
        }
        #endregion       

        #region  Description : BTN61_BATCH_Invoker 이벤트
        private void BTN61_BATCH_InvokerStart(object sender, TButton.ClickEventCheckArgs e)
        {
            e.DbConnector.CommandClear();
            e.DbConnector.Attach("TY_P_AC_7ARFA904", _TXT01_SDATE_Value, _WKSABUN_Value, "");
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
               this.ShowMessage("TY_M_UT_74RE0394");
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
