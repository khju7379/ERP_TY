using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 직급별 급여총액 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.09.22 15:33
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_59MFK891 : 직급별 급여총액 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_59MFL892 : 직급별 급여총액 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  KBJKCD : 직급
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRPY018S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRPY018S()
        {
            InitializeComponent();
        }

        private void TYHRPY018S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_INQ.ProcessCheck += new TButton.CheckHandler(BTN61_INQ_ProcessCheck);

            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));            

            this.SetStartingFocus(this.DTP01_SDATE);

        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_59MFL892.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_59MFK891", this.DTP01_SDATE.GetString().ToString().Substring(0, 6),
                                                            this.DTP01_EDATE.GetString().ToString().Substring(0, 6),
                                                            this.CBH01_KBJKCD.GetValue()
                                   );
            this.FPS91_TY_S_HR_59MFL892.SetValue(UP_DataTableConvert(this.DbConnector.ExecuteDataTable()));

            if (this.FPS91_TY_S_HR_59MFL892.CurrentRowCount > 0)
            {
                this.SetSpreadSumRow(this.FPS91_TY_S_HR_59MFL892, "KBSABUN", "", SumRowType.SubTotal);
            }

        }

        private void BTN61_INQ_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (Convert.ToInt32(this.DTP01_SDATE.GetString().ToString().Substring(0, 6)) > Convert.ToInt32((this.DTP01_EDATE.GetString().ToString().Substring(0, 6))))
            {
                this.ShowCustomMessage("시작일자가 종료일자보다 클수 없습니다", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                this.DTP01_SDATE.Focus();
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : DataTable Convert
        private DataTable UP_DataTableConvert(DataTable dt)
        {
            int i = 0;
            int iJKCnt = 0;
            string sKBJKCD = string.Empty;

            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;
            iJKCnt = 1;

            for (i = 1; i < nNum; i++)
            {
                if (table.Rows[i - 1]["KBJKCD"].ToString() != table.Rows[i]["KBJKCD"].ToString())
                {
                    // 소계
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    // 소 계 이름 넣기
                    table.Rows[i]["KBHANGL"] = iJKCnt.ToString() + "명";

                    sKBJKCD = "KBJKCD = '" + table.Rows[i - 1]["KBJKCD"].ToString() + "' ";

                    table.Rows[i]["PAYMAMT"] = table.Compute("SUM(PAYMAMT)", sKBJKCD).ToString();
                    table.Rows[i]["PAYSAMT"] = table.Compute("SUM(PAYSAMT)", sKBJKCD).ToString();
                    table.Rows[i]["PAYOTAMT"] = table.Compute("SUM(PAYOTAMT)", sKBJKCD).ToString();
                    table.Rows[i]["PAYYUNCHAAMT"] = table.Compute("SUM(PAYYUNCHAAMT)", sKBJKCD).ToString();
                    table.Rows[i]["PAYHAPAMT"] = table.Compute("SUM(PAYHAPAMT)", sKBJKCD).ToString();
                    table.Rows[i]["PAYBONUSAMT"] = table.Compute("SUM(PAYBONUSAMT)", sKBJKCD).ToString();
                    table.Rows[i]["PAYTOTAL"] = table.Compute("SUM(PAYTOTAL)", sKBJKCD).ToString();

                    nNum = nNum + 1;

                    i = i + 1;

                    iJKCnt = 0;
                }
                iJKCnt = iJKCnt + 1;
            }

            if (nNum > 0)
            {
                // 소계
                row = table.NewRow();
                table.Rows.InsertAt(row, i);

                table.Rows[i]["KBHANGL"] = iJKCnt.ToString() + "명";

                sKBJKCD = "KBJKCD = '" + table.Rows[i - 1]["KBJKCD"].ToString() + "' ";

                table.Rows[i]["PAYMAMT"] = table.Compute("SUM(PAYMAMT)", sKBJKCD).ToString();
                table.Rows[i]["PAYSAMT"] = table.Compute("SUM(PAYSAMT)", sKBJKCD).ToString();
                table.Rows[i]["PAYOTAMT"] = table.Compute("SUM(PAYOTAMT)", sKBJKCD).ToString();
                table.Rows[i]["PAYYUNCHAAMT"] = table.Compute("SUM(PAYYUNCHAAMT)", sKBJKCD).ToString();
                table.Rows[i]["PAYHAPAMT"] = table.Compute("SUM(PAYHAPAMT)", sKBJKCD).ToString();
                table.Rows[i]["PAYBONUSAMT"] = table.Compute("SUM(PAYBONUSAMT)", sKBJKCD).ToString();
                table.Rows[i]["PAYTOTAL"] = table.Compute("SUM(PAYTOTAL)", sKBJKCD).ToString();
               
            }

            return table;
        }
        #endregion
    }
}
