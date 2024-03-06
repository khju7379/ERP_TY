using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 부가세신고자료생성 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.06.14 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_26E2M868 : 부가세신고자료생성
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
    ///  GGUBUN : 구분
    ///  V1GUBN : 매입．매출구분
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACGT001B : TYBase
    {
        private TYData DAT01_SABUN;

        #region Description : 페이지 로드
        public TYACGT001B()
        {
            InitializeComponent();
        }

        private void TYACGT001B_Load(object sender, System.EventArgs e)
        {
            // 로그인 사번 가져오기
            this.DAT01_SABUN = new TYData("DAT01_SABUN", TYUserInfo.EmpNo);
            //this.DAT01_SABUN = new TYData("DAT01_SABUN", "0311-M");

            SetStartingFocus(this.DTP01_GSTYYMM);
        }
        #endregion

        #region Description : 배치 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sV1GUBN  = string.Empty;
            string sMessage = string.Empty;

            sV1GUBN = this.CBO01_V1GUBN.GetValue().ToString();

            if (this.CBO01_V1GUBN.GetValue().ToString() == "")
            {
                sV1GUBN = "0";
            }

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_26E2M868",
                this.DTP01_GSTYYMM.GetValue().ToString().Replace("-",""),
                this.DTP01_GEDYYMM.GetValue().ToString().Replace("-", ""),
                sV1GUBN.ToString(),
                this.CBO01_GGUBUN.GetValue().ToString(),
                this.DAT01_SABUN.GetValue().ToString(),
                ""
                );

            sMessage = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sMessage.ToString() == "OK")
            {
                this.ShowMessage("TY_M_GB_26E30875");
            }
            else
            {
                this.ShowMessage("TY_M_GB_26E31876");
            }
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