using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.AC00
{
    /// <summary>
    /// 자금실적 생성 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2020.03.19 08:48
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_A3J90121 : 자금실적 생성 SP
    ///  TY_P_AC_A3J93122 : 자금원천 생성 SP
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_26E2Z874 : 생성하시겠습니까?
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    ///  TY_M_GB_26E31876 : 생성 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    /// 

       


    public partial class TYACKF008B : TYBase
    {

        private object _TXT01_SDATE_Value;
        private object _TXT01_EDATE_Value;
        private object _TXT01_SABUN_Value;       
        

        #region  Description : 폼 로드 이벤트
        public TYACKF008B()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }

        private void TYACKF008B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.IsAsynchronous = true;  

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.SetStartingFocus(DTP01_SDATE);
        }
        #endregion

        #region  Description : 처리 버튼 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            this._TXT01_SDATE_Value = DTP01_SDATE.GetString();
            this._TXT01_EDATE_Value = DTP01_EDATE.GetString();
            this._TXT01_SABUN_Value = TYUserInfo.EmpNo;

            if (!this.ShowMessage("TY_M_GB_26E2Z874"))
            {
                e.Successed = false;
                return;
            }
        }
        private void BTN61_BATCH_InvokerStart(object sender, TButton.ClickEventCheckArgs e)
        {
            string sProCeDureId = string.Empty;

            if (RDB01_CHK01.Checked == true)
            {
                sProCeDureId = "TY_P_AC_A3J90121";  //자금실적
            }
            else
            {
                sProCeDureId = "TY_P_AC_A3J93122";  //자금원천
            }

            e.DbConnector.CommandClear();
            e.DbConnector.Attach(sProCeDureId, _TXT01_SDATE_Value, _TXT01_EDATE_Value, _TXT01_SABUN_Value, "");
            e.DbConnector.ExecuteScalar();
        }

        private void BTN61_BATCH_InvokerEnd(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = e.ArgData as DataSet;

            if (ds.Tables[0].Rows[0][0].ToString().Substring(0, 2) != "OK")
            {
                this.ShowMessage("TY_M_GB_26E31876");
            }
            else
            {
                this.ShowMessage("TY_M_GB_26E30875");
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
