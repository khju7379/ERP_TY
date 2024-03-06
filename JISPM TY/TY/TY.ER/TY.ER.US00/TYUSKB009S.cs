using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.US00
{
    /// <summary>
    /// BL재고관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.04.02 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_92FGG782 : B/L 재고관리 조회    
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_P_US_8BJHK186 : B/L 재고관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    /// </summary>
    public partial class TYUSKB009S : TYBase
    {
        #region Description : 페이지 로드
        public TYUSKB009S()
        {
            InitializeComponent();
        }

        private void TYUSKB009S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.DTP01_SDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_92FHQ787.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                 "TY_P_US_92FHP786",
                 this.DTP01_SDATE.GetString().ToString(),
                 this.DTP01_EDATE.GetString().ToString(),
                 this.CBH01_CHHANGCHA.GetValue().ToString()
                );

            this.FPS91_TY_S_US_92FHQ787.SetValue(UP_SumRowAdd(this.DbConnector.ExecuteDataTable()));

            if (this.FPS91_TY_S_US_92FHQ787.CurrentRowCount > 0)
            {
                this.SetSpreadSumRow(this.FPS91_TY_S_US_92FHQ787, "HJDESC1", "[월  계]", SumRowType.SubTotal);
                this.SetSpreadSumRow(this.FPS91_TY_S_US_92FHQ787, "VSDESC1", "[합  계]", SumRowType.Total);
            }
        }
        #endregion        

        #region Description : 소계, 합계 넣는 함수
        private DataTable UP_SumRowAdd(DataTable dt)
        {
            int i = 0;

            string sFilter = string.Empty;

            
            double dCHYNCHQTYTotal = 0;

            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;

            for (i = 1; i < nNum; i++)
            {
                if (table.Rows[i - 1]["CHYYMM"].ToString() != table.Rows[i]["CHYYMM"].ToString()
                   )
                {
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    
                    table.Rows[i]["HJDESC1"] = "[소  계]";

                    sFilter = " CHYYMM = '" + table.Rows[i - 1]["CHYYMM"].ToString() + "'";

                    //공급가액
                    table.Rows[i]["CHYNCHQTY"] = table.Compute("SUM(CHYNCHQTY)", sFilter).ToString();

                    dCHYNCHQTYTotal = dCHYNCHQTYTotal + Convert.ToDouble(table.Rows[i]["CHYNCHQTY"].ToString());

                    nNum = nNum + 1;

                    i = i + 1;
                }
            }

            if (nNum > 0)
            {
                /******* 마지막 거래처의 대한 로우 생성*****/
                row = table.NewRow();
                table.Rows.InsertAt(row, i);

                table.Rows[i]["HJDESC1"] = "[소  계]";

                //  년월, 거래처
                sFilter = "  CHYYMM = '" + table.Rows[i - 1]["CHYYMM"].ToString().Substring(0, 6) + "'";

                table.Rows[i]["CHYNCHQTY"] = table.Compute("SUM(CHYNCHQTY)", sFilter).ToString();

                dCHYNCHQTYTotal = dCHYNCHQTYTotal + Convert.ToDouble(table.Rows[i]["CHYNCHQTY"].ToString());

                /******** 총계를 위한 Row 생성 **************/
                row = table.NewRow();
                table.Rows.InsertAt(row, i + 1);

                // 합 계 이름 넣기
                table.Rows[i + 1]["VSDESC1"] = "[합  계]";

                table.Rows[i + 1]["CHYNCHQTY"] = string.Format("{0:#,##0.000}", dCHYNCHQTYTotal);
            }

            return table;
        }
        #endregion 
    }
}