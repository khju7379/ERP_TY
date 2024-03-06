using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.UT00
{
    /// <summary>
    /// 코드관리 등록 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.03.29 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_668DK092 : 코드관리 INDEX 조회
    ///  TY_P_UT_668DU094 : 코드관리 CODE 조회
    ///  TY_P_MR_2B21D026 : 코드관리 체크
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_85VBA147 : 코드관리 INDEX 조회
    ///  TY_S_UT_85VBH148 : 코드관리 CODE  조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_AC_243AY315 : 작업이 불가합니다.
    ///  TY_M_AC_26B9D824 : 인덱스를 확인하세요.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    /// </summary>
    public partial class TYUTIN037S : TYBase
    {
        private string fsCDINDEX;

        #region Description : Page Load()
        public TYUTIN037S()
        {
            InitializeComponent();
        }

        private void TYUTIN037S_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_UT_85VBA147.Initialize();
            this.FPS91_TY_S_UT_85VBH148.Initialize();

            this.DTP01_STIPHANG.SetValue(DateTime.Now.ToString("yyyyMMdd"));
            this.DTP01_EDIPHANG.SetValue(DateTime.Now.ToString("yyyyMMdd"));

            this.SetStartingFocus(this.DTP01_STIPHANG);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sCHCHULGB = string.Empty;
            string sCOBUNSUN = string.Empty;
            string sHWAJU    = string.Empty;


            // 대표거래처 코드 가져오기
            sHWAJU = Get_VNCODE(this.CBH01_SHWAJU.GetValue().ToString());


            sCHCHULGB = "";
            sCOBUNSUN = "";

            if (this.CBO01_INQOPTION.GetValue().ToString() == "S") // 송유
            {
                sCHCHULGB = "05";
                sCOBUNSUN = "PP1,PP2";
            }

            DataTable dt = new DataTable();

            this.FPS91_TY_S_UT_85VBA147.Initialize();
            this.FPS91_TY_S_UT_85VBH148.Initialize();

            // 출고
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_85VAR143", Get_Date(this.DTP01_STIPHANG.GetValue().ToString()),
                                                        Get_Date(this.DTP01_EDIPHANG.GetValue().ToString()),
                                                        this.CBH01_SHWAJU.GetValue().ToString(),
                                                        this.CBH01_SHWAMUL.GetValue().ToString(),
                                                        sCHCHULGB.ToString()
                                                        );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_85VBA147.SetValue(dt);
            

            // 입고
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_85VAS144", sCOBUNSUN.ToString(),
                                                        Get_Date(this.DTP01_STIPHANG.GetValue().ToString()),
                                                        Get_Date(this.DTP01_EDIPHANG.GetValue().ToString()), 
                                                        this.CBH01_SHWAJU.GetValue().ToString(),
                                                        this.CBH01_SHWAMUL.GetValue().ToString()
                                                        );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_85VBH148.SetValue(dt);
        }
        #endregion
    }
}