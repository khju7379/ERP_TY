using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.UT00
{
    /// <summary>
    /// I계근 출고조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.06.23 16:55
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_66FD4200 : 대표 거래처 코드 조회
    ///  TY_P_UT_66NGW384 : I계근 출고조회
    ///  TY_P_UT_66NGW385 : I계근 출고조회(화주X)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_66NGY386 : I계근 출고조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_GB_2BF7Y364 : 조회가 완료되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  CNHWAJU : 화주
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    ///  KLTOTAL : KL합계
    ///  MTTOTAL : MT합계
    /// </summary>
    public partial class TYUTIN025S : TYBase
    {
        public TYUTIN025S()
        {
            InitializeComponent();
        }

        #region Description : 페이지 로드
        private void TYUTIN025S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_STDATE);

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Descriptoin : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sHWAJU = this.CBH01_CNHWAJU.GetValue().ToString();

            // 대표거래처 코드 가져오기
            sHWAJU = Get_VNCODE(this.CBH01_CNHWAJU.GetValue().ToString());

            DataTable dt = new DataTable();

            this.FPS91_TY_S_UT_66NGY386.Initialize();

            this.DbConnector.CommandClear();

            if (sHWAJU != "")
            {
                this.DbConnector.Attach("TY_P_UT_66NGW384", this.DTP01_STDATE.GetString(),
                                                            this.DTP01_EDDATE.GetString(),
                                                            sHWAJU,
                                                            this.CBH01_SHWAMUL.GetValue().ToString(),
                                                            this.TXT01_CJTANKNO1.GetValue().ToString().Trim()
                                                            );
            }
            else
            {
                this.DbConnector.Attach("TY_P_UT_66NGW385", this.DTP01_STDATE.GetString(),
                                                            this.DTP01_EDDATE.GetString(),
                                                            this.CBH01_SHWAMUL.GetValue().ToString(),
                                                            this.TXT01_CJTANKNO1.GetValue().ToString().Trim()
                                                            );
            }

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_MTTOTAL.Text = string.Format("{0:#,##0.000}", double.Parse(dt.Compute("Sum(CJMTQTY)", null).ToString()));
                //this.TXT01_KLTOTAL.Text = string.Format("{0:#,##0.000}", double.Parse(dt.Compute("Sum(CHKLQTY)", null).ToString()));
                this.TXT01_KLTOTAL.Text = "0.000";
            }
            else
            {
                this.TXT01_MTTOTAL.Text = "0.000";
                this.TXT01_KLTOTAL.Text = "0.000";
            }

            this.FPS91_TY_S_UT_66NGY386.SetValue(dt);
        }
        #endregion
    }
}
