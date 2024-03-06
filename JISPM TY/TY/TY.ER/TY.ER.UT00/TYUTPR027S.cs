using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.UT00
{
    /// <summary>
    /// 출고대장 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.03.21 11:38
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_73LBS023 : 출고대장 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_7BOAM121 : 출고대장 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  CHHWAJU : 화주
    ///  CHHWAMUL : 화물
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYUTPR027S : TYBase
    {
        #region Description : 폼 로드
        public TYUTPR027S()
        {
            InitializeComponent();
        }

        private void TYUTPR027S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            this.CBH01_SHWAJU.SetValue("GSG");

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            double dCHMTQTY = 0;
            double dLTQTY = 0;

            string sJIWKTYPE = string.Empty;

            sJIWKTYPE = this.CBO01_JIWKTYPE.GetValue().ToString();

            if (this.CBO01_JIWKTYPE.GetValue().ToString() == "")
            {
                sJIWKTYPE = "02,03";
            }

            this.FPS91_TY_S_UT_7BOAM121.Initialize();

            DataTable dt = new DataTable();

            // 20180109 박선형이 LT 계산 바꿔달라고 함
            // 기존은 출고파일의 KLQTY * 1000
            // 변경은 (출고량 / 비중) * 1000 해서 소숫점 올림

            this.DbConnector.CommandClear();

            if (this.CBH01_SHWAJU.GetValue().ToString() != "")
            {
                // 대표거래처 코드 가져오기
                string sHWAJU = Get_VNCODE(this.CBH01_SHWAJU.GetValue().ToString());

                this.DbConnector.Attach("TY_P_UT_7BOAJ119", this.DTP01_STDATE.GetString(),
                                                            this.DTP01_EDDATE.GetString(),
                                                            sHWAJU,
                                                            this.CBH01_SHWAMUL.GetValue().ToString(),
                                                            sJIWKTYPE.ToString(),
                                                            this.CBH01_CHCHHJ.GetValue().ToString(),
                                                            this.TXT01_CHCHTANK.GetValue().ToString().Trim(),
                                                            this.CBH01_CHDNST.GetValue().ToString(),
                                                            this.DTP01_STDATE.GetString(),
                                                            this.DTP01_EDDATE.GetString(),
                                                            sHWAJU,
                                                            this.CBH01_SHWAMUL.GetValue().ToString(),
                                                            sJIWKTYPE.ToString(),
                                                            this.CBH01_CHCHHJ.GetValue().ToString(),
                                                            this.TXT01_CHCHTANK.GetValue().ToString().Trim()
                                                            );
            }
            else
            {
                this.DbConnector.Attach("TY_P_UT_7BOAK120", this.DTP01_STDATE.GetString(),
                                                            this.DTP01_EDDATE.GetString(),
                                                            this.CBH01_SHWAMUL.GetValue().ToString(),
                                                            sJIWKTYPE.ToString(),
                                                            this.CBH01_CHCHHJ.GetValue().ToString(),
                                                            this.TXT01_CHCHTANK.GetValue().ToString().Trim(),
                                                            this.CBH01_CHDNST.GetValue().ToString(),
                                                            this.DTP01_STDATE.GetString(),
                                                            this.DTP01_EDDATE.GetString(),
                                                            this.CBH01_SHWAMUL.GetValue().ToString(),
                                                            sJIWKTYPE.ToString(),
                                                            this.CBH01_CHCHHJ.GetValue().ToString(),
                                                            this.TXT01_CHCHTANK.GetValue().ToString().Trim()
                                                            );
            }

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_7BOAM121.SetValue(dt);

            for (int i = 0; i < FPS91_TY_S_UT_7BOAM121.CurrentRowCount; i++)
            {
                dCHMTQTY = dCHMTQTY + double.Parse(Get_Numeric(this.FPS91_TY_S_UT_7BOAM121.GetValue(i, "CHMTQTY").ToString()));
                dLTQTY = dLTQTY + double.Parse(Get_Numeric(this.FPS91_TY_S_UT_7BOAM121.GetValue(i, "LTQTY").ToString()));
            }

            // 출고량(MT)
            this.TXT01_CHMTQTY.SetValue((dCHMTQTY).ToString("0.000"));

            // 출고량(L)
            this.TXT01_CHKLQTY.SetValue((dLTQTY).ToString("0"));
        }
        #endregion
    }
}
