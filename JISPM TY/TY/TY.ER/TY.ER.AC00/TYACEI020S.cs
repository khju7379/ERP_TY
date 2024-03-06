using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 거래처별받을어음 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.03.13 17:03
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_33D51308 : 거래처별받을어음 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_33D55310 : 거래처별받을어음 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  B2DPMK : 작성부서
    ///  E6CDCL : 거래처코드
    ///  INQOPTION : 조회구분
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    /// </summary>
    public partial class TYACEI020S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYACEI020S()
        {
            InitializeComponent();
        }

        private void TYACEI020S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_GSTDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyyMMdd"));
            this.DTP01_GEDDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));

            this.CBH01_B2DPMK.DummyValue = this.DTP01_GSTDATE.GetString().ToString();  

            this.SetStartingFocus(this.DTP01_GSTDATE);

        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_33D55310.Initialize();
 
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_33D51308", this.CBO01_INQOPTION.GetValue(), 
                                                        this.DTP01_GSTDATE.GetString().ToString(),
                                                        this.DTP01_GEDDATE.GetString().ToString(),
                                                        this.CBH01_E6CDCL.GetValue(),
                                                        this.CBH01_B2DPMK.GetValue());
            this.FPS91_TY_S_AC_33D55310.SetValue(UP_SuTotal_ds(this.DbConnector.ExecuteDataSet()));

            if (this.FPS91_TY_S_AC_33D55310.CurrentRowCount > 0)
            {
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_33D55310, "E6NONR", "[소     계]", SumRowType.SubTotal);
            }

        }
        #endregion

        #region Description : 소계 내기
        private DataTable UP_SuTotal_ds(DataSet ds)
        {
            string sFilter = string.Empty;
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
                    if (table.Rows[i - 1]["E6CDCL"].ToString() != table.Rows[i]["E6CDCL"].ToString())
                    {
                        row = table.NewRow();
                        table.Rows.InsertAt(row, i);

                        table.Rows[i]["E6NONR"] = "[소     계]";

                        sFilter = "E6CDCL = '" + table.Rows[i - 1]["E6CDCL"].ToString() + "' ";

                        
                        table.Rows[i]["E6AMNR"] = table.Compute("Sum(E6AMNR)", sFilter).ToString();

                        nNum = nNum + 1;

                        i = i + 1;
                    }
                }

                row = table.NewRow();
                table.Rows.InsertAt(row, i);

                table.Rows[i]["E6NONR"] = "[소     계]";

                sFilter = "E6CDCL = '" + table.Rows[i - 1]["E6CDCL"].ToString() + "' ";

                
                table.Rows[i]["E6AMNR"] = table.Compute("Sum(E6AMNR)", sFilter).ToString();
            }

            return table;
        }
        #endregion

        #region Description : DTP01_GSTDATE_ValueChanged
        private void DTP01_GSTDATE_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_B2DPMK.DummyValue = this.DTP01_GSTDATE.GetString().ToString();
        }
        #endregion
    }
}
