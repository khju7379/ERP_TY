using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 개인급여관리 급여복사관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.03.09 16:27
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
    ///  TY_M_AC_27J81133 : 복사 하시겠습니까?
    ///  TY_M_AC_27J83134 : 복사가 완료되었습니다
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  COPY : 복사
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRPY05C3 : TYBase
    {
        private DataTable fsdt;

        public TYHRPY05C3(DataTable dt)
        {
            InitializeComponent();

            fsdt = dt;
        }

        private void TYHRPY05C3_Load(object sender, System.EventArgs e)
        {
            this.BTN61_COPY.ProcessCheck += new TButton.CheckHandler(BTN61_COPY_ProcessCheck);

            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
        }

        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        #region Description : 복사 이벤트
        private void BTN61_COPY_Click(object sender, EventArgs e)
        {
            string sDate = DTP01_SDATE.GetString();

            this.DbConnector.CommandClear();
            this.DataTableColumnAdd(fsdt, "PDHISAB", TYUserInfo.EmpNo);

            if (fsdt.Rows.Count > 0)
            {
                for (int i = 0; i < fsdt.Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_4CCDC786", fsdt.Rows[i]["PDSABUN"].ToString(),
                                                                fsdt.Rows[i]["PDPAYCODE"].ToString(),
                                                                sDate,
                                                                fsdt.Rows[i]["PDSTAMOUNT"].ToString(),
                                                                fsdt.Rows[i]["PDSTRATE"].ToString(),
                                                                "",
                                                                fsdt.Rows[i]["PDBIGO"].ToString(),
                                                                fsdt.Rows[i]["PDHISAB"].ToString()
                                                                );
                }

                this.DbConnector.ExecuteTranQueryList();
            }
            this.ShowMessage("TY_M_AC_27J83134");
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        
        private void BTN61_COPY_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_AC_27J81133"))
            {
                e.Successed = false;
                return;
            }            
        }
        #endregion
    }
}
