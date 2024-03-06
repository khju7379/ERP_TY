using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using DataDynamics.ActiveReports;

namespace TY.ER.UT00
{
    /// <summary>
    /// 탱크별 재고현황 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.05.10 15:25
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_75AFF429 : 탱크별 재고현황 출력
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  DATE : 일자
    /// </summary>
    public partial class TYUTPR013P : TYBase
    {
        #region Description : 폼 로드
        public TYUTPR013P()
        {
            InitializeComponent();
        }

        private void TYUTPR013P_Load(object sender, System.EventArgs e)
        {   
            this.DTP01_DATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_DATE);
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75AFF429");

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                ActiveReport rpt = new TYUTPR013R();
                // 가로 출력
                rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75AFF429");

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_9758W010.SetValue(UP_Change_DataTable(dt));

            if (this.FPS91_TY_S_UT_9758W010.CurrentRowCount > 0)
            {
                this.SetSpreadSumRow(this.FPS91_TY_S_UT_9758W010, "VNSANGHO", "[탱 크 합 계]", SumRowType.SubTotal);
                this.SetSpreadSumRow(this.FPS91_TY_S_UT_9758W010, "VNSANGHO", "[전 체 합 계]", SumRowType.Total);
            }
        }
        #endregion

        #region Description : DataTable 변경
        private DataTable UP_Change_DataTable(DataTable dt)
        {
            DataTable dtRtn = new DataTable();
            DataRow row;

            dtRtn.Columns.Add("SVTANKNO", typeof(System.String));
            dtRtn.Columns.Add("HWAMUL", typeof(System.String));
            dtRtn.Columns.Add("SVIPHANG", typeof(System.String));
            dtRtn.Columns.Add("BONSUNNM", typeof(System.String));
            dtRtn.Columns.Add("VNSANGHO", typeof(System.String));
            dtRtn.Columns.Add("SVMTQTY", typeof(System.String));
            dtRtn.Columns.Add("SVCHULQTY", typeof(System.String));
            dtRtn.Columns.Add("JEGO", typeof(System.String));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row = dtRtn.NewRow();

                row["SVTANKNO"] = dt.Rows[i]["SVTANKNO"].ToString();
                row["HWAMUL"] = dt.Rows[i]["HWAMUL"].ToString();
                row["SVIPHANG"] = dt.Rows[i]["SVIPHANG"].ToString();
                row["BONSUNNM"] = dt.Rows[i]["BONSUNNM"].ToString();
                row["VNSANGHO"] = dt.Rows[i]["VNSANGHO"].ToString();
                row["SVMTQTY"] = dt.Rows[i]["SVMTQTY"].ToString();
                row["SVCHULQTY"] = dt.Rows[i]["SVCHULQTY"].ToString();
                row["JEGO"] = dt.Rows[i]["JEGO"].ToString();

                dtRtn.Rows.Add(row);

                if (i + 1 != dt.Rows.Count)
                {
                    if (i != 0)
                    {
                        if (dt.Rows[i]["SVTANKNO"].ToString() != dt.Rows[i + 1]["SVTANKNO"].ToString())
                        {
                            row = dtRtn.NewRow();

                            row["SVTANKNO"] = "";
                            row["HWAMUL"] = "";
                            row["SVIPHANG"] = "";
                            row["BONSUNNM"] = "";
                            row["VNSANGHO"] = "[탱 크 합 계]";
                            row["SVMTQTY"] = string.Format("{0:#,###}", Convert.ToString(dt.Compute("Sum(SVMTQTY)", "SVTANKNO=" + dt.Rows[i]["SVTANKNO"].ToString()).ToString()));
                            row["SVCHULQTY"] = string.Format("{0:#,###}", Convert.ToString(dt.Compute("Sum(SVCHULQTY)", "SVTANKNO=" + dt.Rows[i]["SVTANKNO"].ToString()).ToString()));
                            row["JEGO"] = string.Format("{0:#,###}", Convert.ToString(dt.Compute("Sum(JEGO)", "SVTANKNO=" + dt.Rows[i]["SVTANKNO"].ToString()).ToString()));

                            dtRtn.Rows.Add(row);
                        }
                    }
                }
                else
                {
                    row = dtRtn.NewRow();

                    row["SVTANKNO"] = "";
                    row["HWAMUL"] = "";
                    row["SVIPHANG"] = "";
                    row["BONSUNNM"] = "";
                    row["VNSANGHO"] = "[탱 크 합 계]";
                    row["SVMTQTY"] = string.Format("{0:#,###}", Convert.ToString(dt.Compute("Sum(SVMTQTY)", "SVTANKNO=" + dt.Rows[i]["SVTANKNO"].ToString()).ToString()));
                    row["SVCHULQTY"] = string.Format("{0:#,###}", Convert.ToString(dt.Compute("Sum(SVCHULQTY)", "SVTANKNO=" + dt.Rows[i]["SVTANKNO"].ToString()).ToString()));
                    row["JEGO"] = string.Format("{0:#,###}", Convert.ToString(dt.Compute("Sum(JEGO)", "SVTANKNO=" + dt.Rows[i]["SVTANKNO"].ToString()).ToString()));

                    dtRtn.Rows.Add(row);

                    row = dtRtn.NewRow();

                    row["SVTANKNO"] = "";
                    row["HWAMUL"] = "";
                    row["SVIPHANG"] = "";
                    row["BONSUNNM"] = "";
                    row["VNSANGHO"] = "[전 체 합 계]";
                    row["SVMTQTY"] = string.Format("{0:#,###}", Convert.ToString(dt.Compute("Sum(SVMTQTY)", null).ToString()));
                    row["SVCHULQTY"] = string.Format("{0:#,###}", Convert.ToString(dt.Compute("Sum(SVCHULQTY)", null).ToString()));
                    row["JEGO"] = string.Format("{0:#,###}", Convert.ToString(dt.Compute("Sum(JEGO)", null).ToString()));

                    dtRtn.Rows.Add(row);
                }
            }

            return dtRtn;
        }
        #endregion
    }
}
