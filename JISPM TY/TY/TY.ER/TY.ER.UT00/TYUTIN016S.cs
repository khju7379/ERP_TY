using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.UT00
{
    /// <summary>
    /// 통관화주 화물조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.06.15 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_66FD4200 : 대표 거래처 코드 조회
    ///  TY_P_UT_66FDM206 : 통관화주 화물조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_66FDN208 : 통관화주 화물조회
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
    public partial class TYUTIN016S : TYBase
    {
        public TYUTIN016S()
        {
            InitializeComponent();
        }

        private void TYUTIN016S_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.CBH01_CNHWAJU.CodeText);
        }

        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sHWAJU = this.CBH01_CNHWAJU.GetValue().ToString();

            // 대표거래처 코드 가져오기
            sHWAJU = Get_VNCODE(this.CBH01_CNHWAJU.GetValue().ToString());

            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_UT_66FD4200", this.CBH01_CNHWAJU.GetValue().ToString());

            //DataTable dt = this.DbConnector.ExecuteDataTable();

            //if (dt.Rows.Count > 0)
            //{
            //    sHWAJU = dt.Rows[0]["VNCODE"].ToString();
            //}

            this.FPS91_TY_S_UT_66FDN208.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_66FDM206", sHWAJU,
                                                        this.CBH01_CNHWAMUL.GetValue().ToString()
                                                        );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT02_CJCUQTYTOT.Text = string.Format("{0:#,##0.000}", double.Parse(dt.Compute("Sum(CJCUQTY)", null).ToString()));
                this.TXT02_CJCHQTYTOT.Text = string.Format("{0:#,##0.000}", double.Parse(dt.Compute("Sum(CJCHQTY)", null).ToString()));
                this.TXT02_CJJEQTYTOT.Text = string.Format("{0:#,##0.000}", double.Parse(dt.Compute("Sum(CJJEQTY)", null).ToString()));
            }
            else
            {
                this.TXT02_CJCUQTYTOT.Text = "0.000";
                this.TXT02_CJCHQTYTOT.Text = "0.000";
                this.TXT02_CJJEQTYTOT.Text = "0.000";
            }

            this.FPS91_TY_S_UT_66FDN208.SetValue(dt);
        }
    }
}
