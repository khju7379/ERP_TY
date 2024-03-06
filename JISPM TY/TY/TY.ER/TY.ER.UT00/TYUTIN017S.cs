using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.UT00
{
    /// <summary>
    /// 입고화주 화물조회 프로그램입니다.
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
    ///  TY_P_UT_66FG1226 : 입고화주 화물조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_66FG2227 : 입고화주 화물조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_GB_2BF7Y364 : 조회가 완료되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  CNHWAJU : 화주
    ///  CNHWAMUL : 화물
    ///  CJCHQTYTOT : 총 출고량
    ///  CJCUQTYTOT : 총 통관량
    ///  CJJEQTYTOT : 총 재고량
    ///  CUJEQTYTOT : 총 통관재고
    ///  IPMTQTYTOT : 총 입고량
    ///  MICUQTYTOT : 총 미통관재고
    /// </summary>
    public partial class TYUTIN017S : TYBase
    {
        public TYUTIN017S()
        {
            InitializeComponent();
        }

        private void TYUTIN017S_Load(object sender, System.EventArgs e)
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

            this.FPS91_TY_S_UT_66FG2227.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_66FG1226", sHWAJU,
                                                        this.CBH01_CNHWAMUL.GetValue().ToString(),
                                                        sHWAJU,
                                                        this.CBH01_CNHWAMUL.GetValue().ToString()
                                                        );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_IPMTQTYTOT.Text = string.Format("{0:#,##0.000}", double.Parse(dt.Compute("Sum(IPMTQTY)", null).ToString()));
                this.TXT01_CJCUQTYTOT.Text = string.Format("{0:#,##0.000}", double.Parse(dt.Compute("Sum(IPPAQTY)", null).ToString()));
                this.TXT01_CJCHQTYTOT.Text = string.Format("{0:#,##0.000}", double.Parse(dt.Compute("Sum(IPCHQTY)", null).ToString()));
                this.TXT01_MICUQTYTOT.Text = string.Format("{0:#,##0.000}", double.Parse(dt.Compute("Sum(MICUQTY)", null).ToString()));
                this.TXT01_CUJEQTYTOT.Text = string.Format("{0:#,##0.000}", double.Parse(dt.Compute("Sum(CUJEQTY)", null).ToString()));
                this.TXT01_CJJEQTYTOT.Text = string.Format("{0:#,##0.000}", double.Parse(dt.Compute("Sum(TOTJEGO)", null).ToString()));
            }
            else
            {
                this.TXT01_IPMTQTYTOT.Text = "0.000";
                this.TXT01_CJCUQTYTOT.Text = "0.000";
                this.TXT01_CJCHQTYTOT.Text = "0.000";
                this.TXT01_MICUQTYTOT.Text = "0.000";
                this.TXT01_CUJEQTYTOT.Text = "0.000";
                this.TXT01_CJJEQTYTOT.Text = "0.000";
            }

            this.FPS91_TY_S_UT_66FG2227.SetValue(dt);
        }
    }
}
