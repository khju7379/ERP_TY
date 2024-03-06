using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// EIS 직급별 인건비 추이 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.11.19 09:45
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_2BJ9Y446 : EIS 직급별 인건비 추이 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_2BJA4448 : EIS 직급별 인건비 추이 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  EMJKCD : 직급
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYHRES006S : TYBase
    {
        #region  Description : 조회 버튼 이벤트
        public TYHRES006S()
        {
            InitializeComponent();
        }

        private void TYHRES006S_Load(object sender, System.EventArgs e)
        {
            this.MTB01_GSTYYMM.SetValue(DateTime.Now.ToString("yyyy"));
            this.SetStartingFocus(this.MTB01_GSTYYMM);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_2BJA4448.Initialize(); 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_2BJ9Y446", this.MTB01_GSTYYMM.GetValue().ToString().Substring(0,4) , this.CBH01_EMJKCD.GetValue());
            this.FPS91_TY_S_HR_2BJA4448.SetValue(UP_SuTotal_ds(this.DbConnector.ExecuteDataSet()));

            if (this.FPS91_TY_S_HR_2BJA4448.CurrentRowCount > 0)
            {
                this.SetSpreadSumRow(this.FPS91_TY_S_HR_2BJA4448, "ELRJKCDNM", "[소     계]", SumRowType.SubTotal);
                this.SetSpreadSumRow(this.FPS91_TY_S_HR_2BJA4448, "ELRJKCD", "[총     계]", SumRowType.Total);
            }
        }
        #endregion

        #region  Description : 평균인건비 등록 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYHRES006I()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYHRES006B()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 소계 내기
        private DataTable UP_SuTotal_ds(DataSet ds)
        {
            string sELRJKCDNM = string.Empty;
            double dELRPAYTOTAL = 0;
            int i = 0;

            // 합계를 보여주기 위한 빈 로우 하나 생성
            DataTable table = new DataTable();
            table = ds.Tables[0];

            DataRow row;
            int nNum = table.Rows.Count;

            if (nNum > 0)
            {

                for (i = 1; i < nNum; i++)
                {
                    if (table.Rows[i - 1]["ELRJKCD"].ToString() != table.Rows[i]["ELRJKCD"].ToString() ||
                        table.Rows[i - 1]["ELRSAUP"].ToString() != table.Rows[i]["ELRSAUP"].ToString())
                    {
                        row = table.NewRow();
                        table.Rows.InsertAt(row, i);

                        table.Rows[i]["ELRJKCDNM"] = "[소     계]";

                        sELRJKCDNM = "ELRJKCD = '" + table.Rows[i - 1]["ELRJKCD"].ToString() + "' AND ELRSAUP = '" + table.Rows[i - 1]["ELRSAUP"].ToString() + "' ";

                        // 전일 원화잔액
                        dELRPAYTOTAL += Convert.ToDouble(table.Compute("Sum(ELRPAYTOTAL)", sELRJKCDNM).ToString());

                        table.Rows[i]["ELRPAYTOTAL"] = table.Compute("Sum(ELRPAYTOTAL)", sELRJKCDNM).ToString();

                        nNum = nNum + 1;

                        i = i + 1;
                    }
                }

                row = table.NewRow();
                table.Rows.InsertAt(row, i);

                table.Rows[i]["ELRJKCDNM"] = "[소     계]";

                sELRJKCDNM = "ELRJKCD = '" + table.Rows[i - 1]["ELRJKCD"].ToString() + "' AND ELRSAUP = '" + table.Rows[i - 1]["ELRSAUP"].ToString() + "' ";

                dELRPAYTOTAL += Convert.ToDouble(table.Compute("Sum(ELRPAYTOTAL)", sELRJKCDNM).ToString());
                // 전일 원화잔액
                table.Rows[i]["ELRPAYTOTAL"] = table.Compute("Sum(ELRPAYTOTAL)", sELRJKCDNM).ToString();


                row = table.NewRow();
                table.Rows.InsertAt(row, i + 1);

                table.Rows[i + 1]["ELRJKCD"] = "[총     계]";

                sELRJKCDNM = "";

                table.Rows[i + 1]["ELRPAYTOTAL"] = dELRPAYTOTAL;
            }

            return table;
        }
        #endregion
    }
}
