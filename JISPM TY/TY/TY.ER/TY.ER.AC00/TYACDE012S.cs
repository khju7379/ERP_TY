using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 은행별 외화잔액 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.01.14 08:52
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_31EBJ709 : 은행별 외화잔액 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_31EBK711 : 은행별 외화잔액 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  Z105060 : 화폐단위
    ///  GSTDATE : 시작일자
    /// </summary>
    public partial class TYACDE012S : TYBase
    {
        #region  Description : 폼로드 이벤트
        public TYACDE012S()
        {
            InitializeComponent();
        }

        private void TYACDE012S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_GSTDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));

            this.SetStartingFocus(this.DTP01_GSTDATE);
 
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DateTime dt_Start = new DateTime();
            DateTime dt_End = new DateTime();

            string sParamDate = this.DTP01_GSTDATE.GetString().ToString().Substring(0, 4) + "-" +
                                this.DTP01_GSTDATE.GetString().ToString().Substring(4, 2) + "-" +
                                this.DTP01_GSTDATE.GetString().ToString().Substring(6, 2);


            dt_End = Convert.ToDateTime(sParamDate);
            dt_Start = dt_End.AddDays(+1);

            string sENDDATE = Convert.ToString(dt_Start.Year) + Set_Fill2(Convert.ToString(dt_Start.Month)) + Set_Fill2(Convert.ToString(dt_Start.Day));

            this.FPS91_TY_S_AC_31EBK711.Initialize(); 
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_31EBJ709", this.DTP01_GSTDATE.GetValue().ToString(), sENDDATE, this.CBH01_Z105060.GetValue());
            this.FPS91_TY_S_AC_31EBK711.SetValue(UP_SumData(this.DbConnector.ExecuteDataTable()));
            if (this.FPS91_TY_S_AC_31EBK711.CurrentRowCount > 0)
            {
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_31EBK711, "IHGUJA", "[소   계]", SumRowType.SubTotal);
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_31EBK711, "IHGUJA", "[총   계]", SumRowType.Total);
            }

        }
        #endregion

        #region Description : 소계 및 총계 처리
        public DataTable UP_SumData(DataTable ds)
        {
            

            /* 소계처리 */
            int nNum = ds.Rows.Count;

            double dIHapAmt = 0;
            double dWHapAmt = 0;

            DataTable oTable = new DataTable();
            DataRow dtRow;

            oTable = ds;

            // 데이터가 없을시 리턴

            if (nNum > 0)
            {

                string sCond = "";

                /* 소 계 */
                int i = 0;
                for (i = 1; i < nNum; i++)
                {
                    if (oTable.Rows[i - 1]["BANK"].ToString() != oTable.Rows[i]["BANK"].ToString())
                    {
                        dtRow = oTable.NewRow();
                        oTable.Rows.InsertAt(dtRow, i);
                        // 합 계 이름 넣기
                        oTable.Rows[i]["IHGUJA"] = "[소   계]";

                        // 조건
                        sCond = "BANK = '" + oTable.Rows[i - 1]["BANK"].ToString().Trim() + "' ";

                        // 외화잔액금액
                        oTable.Rows[i]["ISHNJAN"] = double.Parse(Get_Numeric(oTable.Compute("Sum(ISHNJAN)", sCond).ToString()));
                        dIHapAmt = dIHapAmt + double.Parse(Get_Numeric(oTable.Compute("Sum(ISHNJAN)", sCond).ToString()));

                        // 원화잔액금액
                        oTable.Rows[i]["WANJAN"] = System.Int64.Parse(Get_Numeric(oTable.Compute("Sum(WANJAN)", sCond).ToString()));
                        dWHapAmt = dWHapAmt + System.Int64.Parse(Get_Numeric(oTable.Compute("Sum(WANJAN)", sCond).ToString()));


                        nNum = nNum + 1;
                        i = i + 1;
                    }
                }

                /******* 마지막 거래처의 대한 로우 생성*****/
                dtRow = oTable.NewRow();
                oTable.Rows.InsertAt(dtRow, i);
                // 합 계 이름 넣기
                oTable.Rows[i]["IHGUJA"] = "[소   계]";

                // 상위업체 조건
                sCond = "BANK = '" + oTable.Rows[i - 1]["BANK"].ToString().Trim() + "' ";

                // 외화잔액금액
                oTable.Rows[i]["ISHNJAN"] = double.Parse(Get_Numeric(oTable.Compute("Sum(ISHNJAN)", sCond).ToString()));
                dIHapAmt = dIHapAmt + double.Parse(Get_Numeric(oTable.Compute("Sum(ISHNJAN)", sCond).ToString()));

                // 원화잔액금액
                oTable.Rows[i]["WANJAN"] = System.Int64.Parse(Get_Numeric(oTable.Compute("Sum(WANJAN)", sCond).ToString()));
                dWHapAmt = dWHapAmt + System.Int64.Parse(Get_Numeric(oTable.Compute("Sum(WANJAN)", sCond).ToString()));

                /******** 총계를 위한 Row 생성 **************/
                dtRow = oTable.NewRow();
                oTable.Rows.InsertAt(dtRow, i + 1);

                // 합 계 이름 넣기
                oTable.Rows[i + 1]["IHGUJA"] = "[총   계]";
                // 외화잔액 총잔액 
                oTable.Rows[i + 1]["ISHNJAN"] = dIHapAmt;
                // 외화잔액 총잔액 
                oTable.Rows[i + 1]["WANJAN"] = dWHapAmt;
            }

            return oTable;
        }
        #endregion
    }
}
