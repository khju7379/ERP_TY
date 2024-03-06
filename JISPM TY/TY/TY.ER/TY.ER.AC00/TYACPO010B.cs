using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 장기채권현황 생성 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.07.18 10:56
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_37IAY151 : EIS 장기채권현황 생성
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
    public partial class TYACPO010B : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYACPO010B()
        {
            InitializeComponent();
        }

        private void TYACPO010B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_GSTYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.SetStartingFocus(this.DTP01_GSTYYMM);
        }
        #endregion

        #region  Description : 생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sOUTMSG = "";

            DateTime dt = new DateTime();

            dt = Convert.ToDateTime(this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 4) + "-" + this.DTP01_GSTYYMM.GetString().ToString().Substring(4, 2) + "-01");

            dt = dt.AddMonths(1).AddDays(-1);

            string sDate = dt.Year.ToString() + Set_Fill2(dt.Month.ToString()) + Set_Fill2(dt.Day.ToString());

            /*
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_27J1V123", sDate, "D", Employer.UserID, "");
            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_27J1V123", sDate, "A", Employer.UserID, "");
            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
            */

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_37IAY151", this.DTP01_GSTYYMM.GetString().Substring(0, 6), "A", TYUserInfo.EmpNo, sOUTMSG.ToString());           
            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUTMSG.ToString().Substring(0, 1) != "I")
            {
                this.ShowMessage("TY_M_GB_26E31876");
                return; 
            }
            
            this.ShowMessage("TY_M_GB_26E30875");
        }
        #endregion

        #region Description : 처리 체크
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            //EIS 마감 체크      
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_27H64059", this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4), this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2));
            DataTable dt1 = this.DbConnector.ExecuteDataTable();

            if (dt1.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                e.Successed = false;
                return;
            }
            else
            {
                if (dt1.Rows[0]["ECGUBUN"].ToString() == "Y" || dt1.Rows[0]["ECGUBUN"].ToString() == "Z")
                {
                    this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                    e.Successed = false;
                    return;
                }
            }


            //연령분석 생성 유무 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_3CI4G807", this.DTP01_GSTYYMM.GetString() );
            Int32 iCnt = Convert.ToInt32(this.DbConnector.ExecuteScalar());

            if (iCnt <= 0)
            {
                this.ShowMessage("TY_M_AC_28989350");
                e.Successed = false;
                return;
            }
            
            if (!this.ShowMessage("TY_M_GB_26E2Z874"))
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
