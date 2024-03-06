using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.UT00
{
    /// <summary>
    /// 선박사양조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.11.24 15:49
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_6BOF3847 : 선박사양관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_B1DH1302 : 선박사양관리
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  VESLCODE : 선박코드
    /// </summary>
    public partial class TYUTVS003S : TYBase
    {
        #region Description : 페이지 로드
        public TYUTVS003S()
        {
            InitializeComponent();
        }

        private void TYUTVS003S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(DateTime.Now.ToString("yyyyMMdd").ToString());
            this.DTP01_EDDATE.SetValue(DateTime.Now.ToString("yyyyMMdd").ToString());

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_B1DH1302.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_B1DH0301", Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                                                        Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                                                        this.CBH01_SBONSUN.GetValue().ToString(),
                                                        this.CBH01_SHWAJU.GetValue().ToString(),
                                                        this.CBH01_SHWAMUL.GetValue().ToString(),
                                                        this.CBH01_VSJUBAN.GetValue().ToString(),
                                                        this.TXT01_SVTANKNO.GetValue().ToString()
                                                        );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_UT_B1DH1302.SetValue(dt);
            }
        }
        #endregion
    }
}
