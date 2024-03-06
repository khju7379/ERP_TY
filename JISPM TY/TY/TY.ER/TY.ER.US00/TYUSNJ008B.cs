using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using GrapeCity.ActiveReports;
using TY.ER.GB00;
using TY.ER.AC00;

namespace TY.ER.US00
{
    /// <summary>
    /// 하역료 단가 조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2016.06.08 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_7
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  CHYMDATE : 기준일자
    ///  CHYMSEQ : 순번
    /// </summary>
    public partial class TYUSNJ008B : TYBase
    {
        #region Descriptino : 페이지 로드
        public TYUSNJ008B()
        {
            InitializeComponent();
        }

        private void TYUSNJ008B_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.MTB01_GDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));

            Timer tmr = new Timer();

            tmr.Tick += delegate
            {
                tmr.Stop();
                this.CBH01_STHANGCHA.CodeText.Focus();
            };

            tmr.Interval = 100;
            tmr.Start();
        }
        #endregion

        #region Description : 임금 소급 생성 버튼
        private void BTN61_CREATE_Click(object sender, EventArgs e)
        {
            string sTMID = string.Empty;
            string sOUTMSG = string.Empty;

            // ID 부여
            sTMID = this.IPAdresss + Employer.EmpNo;

            // 매출 전표 SP 수행
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_96BBG767", sTMID.ToString(),
                                                        this.CBH01_STHANGCHA.GetValue().ToString(),
                                                        this.CBH01_EDHANGCHA.GetValue().ToString(),
                                                        Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                                                        Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                                                        Get_Date(this.MTB01_GDATE.GetValue().ToString()).Substring(0, 6),
                                                        sOUTMSG.ToString()
                                                        );

            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUTMSG.Substring(0, 2) == "OK")
            {
                this.ShowMessage("TY_M_US_96AGV760"); // 저장 메세지
            }
            else
            {
                this.ShowMessage("TY_M_US_96AGW761"); // 저장 메세지
            }

            SetFocus(this.CBH01_STHANGCHA.CodeText);
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
