using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// EIS 사업부별 인건비 추이 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.11.21 18:52
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_2BL85543 : EIS 사업부별 인건비 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_2BL86544 : EIS 사업부별 인건비 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  INQ : 조회
    ///  NEW : 신규
    ///  ELSSAUP : ELSSAUP
    ///  ELSYYMM : 년월
    /// </summary>
    public partial class TYHRES007S : TYBase
    {
        public TYHRES007S()
        {
            InitializeComponent();
        }

        private void TYHRES007S_Load(object sender, System.EventArgs e)
        {
            this.MTB01_ELSYYMM.SetValue(DateTime.Now.ToString("yyyy"));

            this.CBH01_ELSSAUP.DummyValue = this.MTB01_ELSYYMM.GetValue().ToString().Substring(0, 4) + "0101";  

            this.SetStartingFocus(this.MTB01_ELSYYMM);
        }

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_2BL86544.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_2BL85543", this.MTB01_ELSYYMM.GetValue().ToString().Substring(0,4),this.CBO01_INQOPTION.GetValue(), this.CBH01_ELSSAUP.GetValue());
            this.FPS91_TY_S_HR_2BL86544.SetValue(UP_SuTotal_ds(this.DbConnector.ExecuteDataSet()));

            if (this.FPS91_TY_S_HR_2BL86544.CurrentRowCount > 0)
            {
                this.SetSpreadSumRow(this.FPS91_TY_S_HR_2BL86544, "ELSSAUPNM", "[소     계]", SumRowType.SubTotal);
                this.SetSpreadSumRow(this.FPS91_TY_S_HR_2BL86544, "ELSSAUP", "[총     계]", SumRowType.Total);
            }
        }
        #endregion

        #region  Description : 생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYHRES007B()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 인건비 등록 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYHRES007I()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion      

        #region Description : 소계 내기
        private DataTable UP_SuTotal_ds(DataSet ds)
        {
            string sELSSAUP = string.Empty;

            double dELSPAYTOTAL = 0;
            double dELSBONUS = 0;
            double dELSRETIREFUND = 0;
            double dELSINSURANCE = 0;
            double dTOTAL = 0;
            
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
                    if (table.Rows[i - 1]["ELSSAUP"].ToString() != table.Rows[i]["ELSSAUP"].ToString())
                    {
                        row = table.NewRow();
                        table.Rows.InsertAt(row, i);

                        table.Rows[i]["ELSSAUPNM"] = "[소     계]";

                        sELSSAUP = "ELSSAUP = '" + table.Rows[i - 1]["ELSSAUP"].ToString() + "' ";

                        dELSPAYTOTAL += Convert.ToDouble(table.Compute("Sum(ELSPAYTOTAL)", sELSSAUP).ToString());
                        dELSBONUS += Convert.ToDouble(table.Compute("Sum(ELSBONUS)", sELSSAUP).ToString());
                        dELSRETIREFUND += Convert.ToDouble(table.Compute("Sum(ELSRETIREFUND)", sELSSAUP).ToString());
                        dELSINSURANCE += Convert.ToDouble(table.Compute("Sum(ELSINSURANCE)", sELSSAUP).ToString());
                        dTOTAL += Convert.ToDouble(table.Compute("Sum(TOTAL)", sELSSAUP).ToString());

                        table.Rows[i]["ELSPAYTOTAL"] = table.Compute("Sum(ELSPAYTOTAL)", sELSSAUP).ToString();
                        table.Rows[i]["ELSBONUS"] = table.Compute("Sum(ELSBONUS)", sELSSAUP).ToString();
                        table.Rows[i]["ELSRETIREFUND"] = table.Compute("Sum(ELSRETIREFUND)", sELSSAUP).ToString();
                        table.Rows[i]["ELSINSURANCE"] = table.Compute("Sum(ELSINSURANCE)", sELSSAUP).ToString();
                        table.Rows[i]["TOTAL"] = table.Compute("Sum(TOTAL)", sELSSAUP).ToString();

                        nNum = nNum + 1;

                        i = i + 1;
                    }
                }

                row = table.NewRow();
                table.Rows.InsertAt(row, i);

                table.Rows[i]["ELSSAUPNM"] = "[소     계]";

                sELSSAUP = "ELSSAUP = '" + table.Rows[i - 1]["ELSSAUP"].ToString() + "'";

                dELSPAYTOTAL += Convert.ToDouble(table.Compute("Sum(ELSPAYTOTAL)", sELSSAUP).ToString());
                dELSBONUS += Convert.ToDouble(table.Compute("Sum(ELSBONUS)", sELSSAUP).ToString());
                dELSRETIREFUND += Convert.ToDouble(table.Compute("Sum(ELSRETIREFUND)", sELSSAUP).ToString());
                dELSINSURANCE += Convert.ToDouble(table.Compute("Sum(ELSINSURANCE)", sELSSAUP).ToString());
                dTOTAL += Convert.ToDouble(table.Compute("Sum(TOTAL)", sELSSAUP).ToString());

                table.Rows[i]["ELSPAYTOTAL"] = table.Compute("Sum(ELSPAYTOTAL)", sELSSAUP).ToString();
                table.Rows[i]["ELSBONUS"] = table.Compute("Sum(ELSBONUS)", sELSSAUP).ToString();
                table.Rows[i]["ELSRETIREFUND"] = table.Compute("Sum(ELSRETIREFUND)", sELSSAUP).ToString();
                table.Rows[i]["ELSINSURANCE"] = table.Compute("Sum(ELSINSURANCE)", sELSSAUP).ToString();
                table.Rows[i]["TOTAL"] = table.Compute("Sum(TOTAL)", sELSSAUP).ToString();


                row = table.NewRow();
                table.Rows.InsertAt(row, i + 1);

                table.Rows[i + 1]["ELSSAUP"] = "[총     계]";

                sELSSAUP = "";

                table.Rows[i + 1]["ELSPAYTOTAL"] = dELSPAYTOTAL;
                table.Rows[i + 1]["ELSBONUS"] = dELSBONUS;
                table.Rows[i + 1]["ELSRETIREFUND"] = dELSRETIREFUND;
                table.Rows[i + 1]["ELSINSURANCE"] = dELSINSURANCE;
                table.Rows[i + 1]["TOTAL"] = dTOTAL;
            }
            return table;
        }
        #endregion

        #region  Description : MTB01_ELSYYMM_TextChanged 이벤트
        private void MTB01_ELSYYMM_TextChanged(object sender, EventArgs e)
        {
            if (this.MTB01_ELSYYMM.GetValue().ToString().Length >= 4)
            {
                this.CBH01_ELSSAUP.DummyValue = this.MTB01_ELSYYMM.GetValue().ToString().Substring(0, 4) + "0101";
            }
        }
        #endregion
    }
}
