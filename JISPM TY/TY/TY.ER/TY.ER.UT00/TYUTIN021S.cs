using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.UT00
{
    /// <summary>
    /// 드럼재고조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.06.21 17:15
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_66FD4200 : 대표 거래처 코드 조회
    ///  TY_P_UT_66LHD335 : 드럼재고 조회(화주X)
    ///  TY_P_UT_66LHD336 : 드럼재고 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_66LHD337 : 드럼재고 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_GB_2BF7Y364 : 조회가 완료되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  CNHWAJU : 화주
    ///  CNHWAMUL : 화물
    /// </summary>
    public partial class TYUTIN021S : TYBase
    {
        public TYUTIN021S()
        {
            InitializeComponent();
        }

        #region Description : 페이지 로드
        private void TYUTIN021S_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.CBH01_CNHWAJU.CodeText);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sHWAJU = this.CBH01_CNHWAJU.GetValue().ToString();

            // 대표거래처 코드 가져오기
            sHWAJU = Get_VNCODE(this.CBH01_CNHWAJU.GetValue().ToString());

            DataTable dt = new DataTable();

            this.FPS91_TY_S_UT_66LHD337.Initialize();

            this.DbConnector.CommandClear();

            if (sHWAJU != "")
            {
                this.DbConnector.Attach("TY_P_UT_66LHD336", sHWAJU,
                                                            this.CBH01_CNHWAMUL.GetValue().ToString());
            }
            else
            {
                this.DbConnector.Attach("TY_P_UT_66LHD335", this.CBH01_CNHWAMUL.GetValue().ToString());
            }

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_66LHD337.SetValue(dt);
        }
        #endregion
    }
}
