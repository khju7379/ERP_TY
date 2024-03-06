using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 전표 월마감 관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.12.14 00:09
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2CA2M007 : 월마감 작업(SP 호출)
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
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACBJ005B : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYACBJ005B()
        {
            InitializeComponent();

            this.SetPopupStyle();  
        }

        private void TYACBJ005B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_GSTYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.SetStartingFocus(this.DTP01_GSTYYMM);
        }
        #endregion

        #region  Description : 처리 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sOUTMSG = "";

            try
            {
                // 월 총계정 원장 생성(일반, KGAAP, IFRS)
                // 신계정 월 관리대장 생성
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2CA2M007", this.DTP01_GSTYYMM.GetString(), TYUserInfo.EmpNo, this.CBO01_GOKCR.GetValue().ToString(), sOUTMSG.ToString());
                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
            }
            catch (Exception ex)
            {
                string ddd = ex.Message;
            }
            finally
            {
                if (sOUTMSG.Length > 0)
                {                   
                        if (sOUTMSG.Substring(0, 2) == "ER")
                        {
                            this.ShowMessage("TY_M_AC_2CECE181");
                        }
                        else
                        {
                            this.ShowMessage("TY_M_AC_2CECE180");
                        }                  
                }
                else
                {
                    this.ShowMessage("TY_M_AC_2CECE181");                    
                }
            }
        }
        #endregion

        #region Description : BTN61_BATCH_ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            //월마감전에 POSITNG이 안되어 있는 전표가 있으면 월마감 불가
            string sSGSTYYMM = string.Empty;
            string sEGSTYYMM = string.Empty;
            string sJuno = string.Empty;

            sSGSTYYMM = this.DTP01_GSTYYMM.GetString().Substring(0, 4) + "0101";
            sEGSTYYMM = this.DTP01_GSTYYMM.GetString();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_6AVES614", sSGSTYYMM, sEGSTYYMM);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                //A102002016100400101
                sJuno = dt.Rows[0]["B4NOJP"].ToString().Substring(0, 6) + "-" + dt.Rows[0]["B4NOJP"].ToString().Substring(6, 8) + "-" + dt.Rows[0]["B4NOJP"].ToString().Substring(14, 3);

                this.ShowCustomMessage("전표번호:"+sJuno + "가 전표POSTING 되어 있지않습니다. 월마감 작업이 불가합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_AC_2CECE179"))
            {
                 e.Successed = false;
                 return;
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
