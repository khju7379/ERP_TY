using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 채권연령분석 생성 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.07.20 13:27
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_27J1V123 : 채권 연령분석 생성
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
    /// </summary>
    public partial class TYACFS002B : TYBase
    {
        private object _DTP01_BMYYMM_Value;
        private object _CBO01_GOKCR_Value;

        #region Description : 폼로드 이벤트
        public TYACFS002B()
        {
            InitializeComponent();            
        }

        private void TYACFS002B_Load(object sender, System.EventArgs e)
        {
            this.DTP01_BMYYMM.SetValue(DateTime.Now.ToString("yyyyMM"));

            this.BTN61_BATCH.IsAsynchronous = true;  
        }
        #endregion


        #region Description : 생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            this._CBO01_GOKCR_Value = this.CBO01_GOKCR.GetValue();
            this._DTP01_BMYYMM_Value = this.DTP01_BMYYMM.GetString();

            //string sOUTMSG = string.Empty;

            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_AC_27J1V123", this.DTP01_BMYYMM.GetString().ToString().Substring(0,6)+"31", this.CBO01_GOKCR.GetValue(), Employer.UserID, sOUTMSG.ToString());

            //sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            //if (sOUTMSG.Substring(0, 1) == "E")
            //{
            //    this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //}
            //else
            //{
            //    this.ShowMessage("TY_M_GB_26E30875");
            //}
        }
        #endregion

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : BTN61_BATCH_InvokerStart 이벤트
        private void BTN61_BATCH_InvokerStart(object sender, TButton.ClickEventCheckArgs e)
        {

            DateTime dt = new DateTime();

            dt = Convert.ToDateTime(this._DTP01_BMYYMM_Value.ToString().Substring(0, 4) + "-" + this._DTP01_BMYYMM_Value.ToString().Substring(4, 2) + "-01");

            dt = dt.AddMonths(1).AddDays(-1);

            string sDate = dt.Year.ToString() + Set_Fill2(dt.Month.ToString()) + Set_Fill2(dt.Day.ToString());

            e.DbConnector.CommandClear();
            e.DbConnector.Attach("TY_P_AC_27J1V123", sDate, this._CBO01_GOKCR_Value, Employer.UserID, "");
            e.DbConnector.ExecuteScalar();
        }
        #endregion

        #region Description : BTN61_BATCH_InvokerEnd 이벤트
        private void BTN61_BATCH_InvokerEnd(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = e.ArgData as DataSet;

            if (ds.Tables[0].Rows[0][0].ToString().Substring(0, 1) == "E")
            {
                this.ShowCustomMessage(ds.Tables[0].Rows[0][0].ToString(), "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            else
            {
                this.ShowMessage("TY_M_GB_26E30875");
            }
        }
        #endregion

    }
}
