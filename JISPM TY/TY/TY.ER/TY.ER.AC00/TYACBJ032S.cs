using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using GrapeCity.ActiveReports;
using TY.ER.GB00;
using System.Windows.Forms;

namespace TY.ER.AC00
{
    /// <summary>
    /// 이자비용 명세서 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.11.28 14:09
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2BS3H723 : 이자비용 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2BS3I725 : 이자비용 명세 조회(은행기준)
    ///  TY_S_AC_2BS3L726 : 이자비용 명세 조회(이자율기준)
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  INQOPTION : 조회구분
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    /// </summary>
    public partial class TYACBJ032S : TYBase
    {
        #region 화면 펑션키 정의 ---> ProcessCmdKey()
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F5)
            {                
                this.BTN61_INQ_Click(null, null);

                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        public TYACBJ032S()
        {
            InitializeComponent();
        }

        private void TYACBJ032S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_GSTDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            this.DTP01_GEDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.SetStartingFocus(DTP01_GSTDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_2BS3I725.Initialize();
            this.FPS91_TY_S_AC_2BS3L726.Initialize(); 
            
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_2BS3H723", this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString(), this.CBO01_INQOPTION.GetValue());

            if (CBO01_INQOPTION.GetValue().ToString() == "1")
            {
                this.FPS91_TY_S_AC_2BS3I725.Visible = true;
                this.FPS91_TY_S_AC_2BS3L726.Visible = false;
 
                this.FPS91_TY_S_AC_2BS3I725.SetValue(UP_SuTotal_ds(this.DbConnector.ExecuteDataSet(), CBO01_INQOPTION.GetValue().ToString()));  //은행

                if (this.FPS91_TY_S_AC_2BS3I725.CurrentRowCount > 0)
                {
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_2BS3I725, "ACJPNO", "[소     계]", SumRowType.SubTotal);
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_2BS3I725, "ACJPNO", "[총     계]", SumRowType.Total);
                }
            }
            else
            {
                this.FPS91_TY_S_AC_2BS3I725.Visible = false;
                this.FPS91_TY_S_AC_2BS3L726.Visible = true;

                this.FPS91_TY_S_AC_2BS3L726.SetValue(UP_SuTotal_ds(this.DbConnector.ExecuteDataSet(), CBO01_INQOPTION.GetValue().ToString()));  //할인율
                if (this.FPS91_TY_S_AC_2BS3L726.CurrentRowCount > 0)
                {
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_2BS3L726, "ACJPNO", "[소     계]", SumRowType.SubTotal);
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_2BS3L726, "ACJPNO", "[총     계]", SumRowType.Total);
                }

            }

            


        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_2BS3H723", this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString(), this.CBO01_INQOPTION.GetValue());

            dt = this.DbConnector.ExecuteDataTable();
            
            SectionReport rpt = null;

            if (CBO01_INQOPTION.GetValue().ToString() == "1")
            {                
                rpt = new TYACBJ032R();
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                rpt = new TYACBJ032R1();
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                (new TYERGB001P(rpt, dt)).ShowDialog();                
            }            
        }
        #endregion

        #region Description : 소계 내기
        private DataTable UP_SuTotal_ds(DataSet ds, string sOpt)
        {
            string sB4VLMINM = string.Empty;
            double dB4AMDRTOTAL = 0;
            int i = 0;

            // 합계를 보여주기 위한 빈 로우 하나 생성
            DataTable table = new DataTable();
            table = ds.Tables[0];

            DataRow row;
            int nNum = table.Rows.Count;

            if (nNum > 0)
            {
                if (sOpt == "1")
                {
                    for (i = 1; i < nNum; i++)
                    {
                        if (table.Rows[i - 1]["B4VLMI1"].ToString() != table.Rows[i]["B4VLMI1"].ToString())
                        {
                            row = table.NewRow();
                            table.Rows.InsertAt(row, i);

                            table.Rows[i]["ACJPNO"] = "[소     계]";

                            sB4VLMINM = "B4VLMI1 = '" + table.Rows[i - 1]["B4VLMI1"].ToString() + "' ";

                            // 전일 원화잔액
                            dB4AMDRTOTAL += Convert.ToDouble(table.Compute("Sum(B4AMDR)", sB4VLMINM).ToString());

                            table.Rows[i]["B4AMDR"] = table.Compute("Sum(B4AMDR)", sB4VLMINM).ToString();

                            nNum = nNum + 1;

                            i = i + 1;
                        }
                    }

                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    table.Rows[i]["ACJPNO"] = "[소     계]";

                    sB4VLMINM = "B4VLMI1 = '" + table.Rows[i - 1]["B4VLMI1"].ToString() + "' ";

                    dB4AMDRTOTAL += Convert.ToDouble(table.Compute("Sum(B4AMDR)", sB4VLMINM).ToString());

                    // 전일 원화잔액
                    table.Rows[i]["B4AMDR"] = table.Compute("Sum(B4AMDR)", sB4VLMINM).ToString();

                    row = table.NewRow();
                    table.Rows.InsertAt(row, i + 1);

                    table.Rows[i + 1]["ACJPNO"] = "[총     계]";

                    table.Rows[i + 1]["B4AMDR"] = dB4AMDRTOTAL;
                }
                else
                {
                    for (i = 1; i < nNum; i++)
                    {
                        if (table.Rows[i - 1]["B4VLMI2"].ToString() != table.Rows[i]["B4VLMI2"].ToString())
                        {
                            row = table.NewRow();
                            table.Rows.InsertAt(row, i);

                            table.Rows[i]["ACJPNO"] = "[소     계]";

                            sB4VLMINM = "B4VLMI2 = '" + table.Rows[i - 1]["B4VLMI2"].ToString() + "' ";

                            // 전일 원화잔액
                            dB4AMDRTOTAL += Convert.ToDouble(table.Compute("Sum(B4AMDR)", sB4VLMINM).ToString());

                            table.Rows[i]["B4AMDR"] = table.Compute("Sum(B4AMDR)", sB4VLMINM).ToString();

                            nNum = nNum + 1;

                            i = i + 1;
                        }
                    }

                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    table.Rows[i]["ACJPNO"] = "[소     계]";

                    sB4VLMINM = "B4VLMI2 = '" + table.Rows[i - 1]["B4VLMI2"].ToString() + "' ";

                    dB4AMDRTOTAL += Convert.ToDouble(table.Compute("Sum(B4AMDR)", sB4VLMINM).ToString());

                    // 전일 원화잔액
                    table.Rows[i]["B4AMDR"] = table.Compute("Sum(B4AMDR)", sB4VLMINM).ToString();

                    row = table.NewRow();
                    table.Rows.InsertAt(row, i + 1);

                    table.Rows[i + 1]["ACJPNO"] = "[총     계]";

                    table.Rows[i + 1]["B4AMDR"] = dB4AMDRTOTAL;
                }
            }

            return table;
        }
        #endregion
    }
}
