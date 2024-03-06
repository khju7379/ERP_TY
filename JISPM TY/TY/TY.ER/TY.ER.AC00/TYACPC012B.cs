using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 확정 CASH 생성 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2013.01.08 11:50
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_31881541 : EIS 확정 일일 CASH 확정구분 처리
    ///  TY_P_AC_31885542 : EIS 확정 일일CASH 삭제
    ///  TY_P_AC_31887540 : EIS 확정 일일CASH 생성
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  SAV : 저장
    ///  EICHYMD : 년월일
    /// </summary>
    public partial class TYACPC012B : TYBase
    {
        private string _sYYMMDD;
        public TYACPC012B(string sYYMMDD)
        {
            this.SetPopupStyle();
            InitializeComponent();

            this._sYYMMDD = sYYMMDD;
        }

        private void TYACPC012B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            if (string.IsNullOrEmpty(_sYYMMDD))
            {
                this.SetFocus(TXT01_EICHYMD);
            }
            else
            {
                this.TXT01_EICHYMD.SetValue(_sYYMMDD);

                this.SetFocus(TXT01_EICHYMD);
            }
        }

        #region Description : 저장
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            // 기존 확정 자료 삭제
            this.DbConnector.CommandClear(); // EMIHCASHF
            this.DbConnector.Attach("TY_P_AC_31885542", this.TXT01_EICHYMD.GetValue().ToString());
            this.DbConnector.ExecuteNonQuery();

            // 기존 확정 자료 생성
            this.DbConnector.CommandClear(); // EMIHCASHF
            this.DbConnector.Attach("TY_P_AC_31887540", this.TXT01_EICHYMD.GetValue().ToString());
            this.DbConnector.ExecuteNonQuery();

            // 기존 확정 자료 처리 구분 세팅
            this.DbConnector.CommandClear(); //EMICASHF
            this.DbConnector.Attach("TY_P_AC_31881541", this.TXT01_EICHYMD.GetValue().ToString());
            this.DbConnector.ExecuteNonQuery();

            
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
            
        }
        #endregion


        #region Description : 저장 처리시 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // 생성할 자료 체크 
            this.DbConnector.CommandClear(); // EMICASHF
            this.DbConnector.Attach("TY_P_AC_31895546", this.TXT01_EICHYMD.GetValue().ToString());
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count == 0)
            {
                this.SetFocus(this.TXT01_EICHYMD);
                this.ShowMessage("TY_M_AC_2CV43442");
                e.Successed = false;
                return;
            }
        }
        #endregion


        #region Description : 닫기
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion
    }
}
