using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using System.Drawing;

namespace TY.ER.HR00
{
    /// <summary>
    /// 근로소득간이지급명세서 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.05.09 11:41
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_959B4519 : 근로소득간이지급명세서 급여 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_959B7520 : 근로소득간이지급명세서 급여 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  INQ : 조회
    ///  INQOPTION : 조회구분
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRPY024S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRPY024S()
        {
            InitializeComponent();
        }

        private void TYHRPY024S_Load(object sender, System.EventArgs e)
        {

            TXT01_SDATE.SetValue(DateTime.Now.ToString("yyyy"));

            this.SetStartingFocus(TXT01_SDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_959B7520.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_A1U9A766", this.TXT01_SDATE.GetValue(), this.CBO01_INQOPTION.GetValue().ToString(), TYUserInfo.SecureKey, "Y" );
            this.FPS91_TY_S_HR_959B7520.SetValue(UP_InsertSumRow(this.DbConnector.ExecuteDataTable()));

            if (this.FPS91_TY_S_HR_959B7520.CurrentRowCount > 0)
            {
                
                this.SetSpreadSumRow(this.FPS91_TY_S_HR_959B7520, "KBHANGL", "[소 계]", SumRowType.SubTotal);
                this.SetSpreadSumRow(this.FPS91_TY_S_HR_959B7520, "KBSABUN", "[합 계]", SumRowType.Total);

                for (int i = 0; i < this.FPS91_TY_S_HR_959B7520.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_HR_959B7520.GetValue(i, "KBHANGL").ToString() == "[소 계]")
                    {
                        this.FPS91_TY_S_HR_959B7520_Sheet1.Cells[i, 8].ForeColor = Color.Red;
                        this.FPS91_TY_S_HR_959B7520_Sheet1.Cells[i, 9].ForeColor = Color.Red;
                        this.FPS91_TY_S_HR_959B7520_Sheet1.Cells[i, 10].ForeColor = Color.Red;
                    }
                    if (this.FPS91_TY_S_HR_959B7520.GetValue(i, "KBSABUN").ToString() == "[합 계]")
                    {
                        this.FPS91_TY_S_HR_959B7520_Sheet1.Cells[i, 8].ForeColor = Color.Red;
                        this.FPS91_TY_S_HR_959B7520_Sheet1.Cells[i, 8].Font = new Font("굴림", 9, FontStyle.Bold);
                        this.FPS91_TY_S_HR_959B7520_Sheet1.Cells[i, 9].ForeColor = Color.Red;
                        this.FPS91_TY_S_HR_959B7520_Sheet1.Cells[i, 9].Font = new Font("굴림", 9, FontStyle.Bold);
                        this.FPS91_TY_S_HR_959B7520_Sheet1.Cells[i, 10].ForeColor = Color.Red;
                        this.FPS91_TY_S_HR_959B7520_Sheet1.Cells[i, 10].Font = new Font("굴림", 9, FontStyle.Bold);
                    }
                }
            }
        }
        #endregion             

        #region  Description : 전산매체 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            if ((new TYHRPY024B()).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 합계 라인 추가 함수
        private DataTable UP_InsertSumRow(DataTable dt)
        {
            int i = 0;

            int iManCnt = 1;
            int iTotalCnt = 0;

            string sFilter = string.Empty;

            double dFHALFAMOUNT = 0;
            double dSHALFAMOUNT = 0;
            double dTOTALAMOUNT = 0;

            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;

            for (i = 1; i < nNum; i++)
            {
                if (table.Rows[i - 1]["KBGUNMU"].ToString() != table.Rows[i]["KBGUNMU"].ToString()  )
                {
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    // 합 계 이름 넣기
                    table.Rows[i]["KBHANGL"] = "[소 계]";
                    table.Rows[i]["KBBSTEAMNM"] = "[ " +iManCnt.ToString() + " 명 ]";                    

                    sFilter = "  KBGUNMU  = '" + table.Rows[i - 1]["KBGUNMU"].ToString() + "'";

                    //상반기
                    table.Rows[i]["FHALFAMOUNT"] = table.Compute("SUM(FHALFAMOUNT)", sFilter).ToString();
                    //하반기
                    table.Rows[i]["SHALFAMOUNT"] = table.Compute("SUM(SHALFAMOUNT)", sFilter).ToString();
                    //총액
                    table.Rows[i]["TOTALAMOUNT"] = table.Compute("SUM(TOTALAMOUNT)", sFilter).ToString();

                    dFHALFAMOUNT = dFHALFAMOUNT + Convert.ToDouble(table.Rows[i]["FHALFAMOUNT"].ToString());
                    dSHALFAMOUNT = dSHALFAMOUNT + Convert.ToDouble(table.Rows[i]["SHALFAMOUNT"].ToString());
                    dTOTALAMOUNT = dTOTALAMOUNT + Convert.ToDouble(table.Rows[i]["TOTALAMOUNT"].ToString());

                    nNum = nNum + 1;

                    i = i + 1;

                    iTotalCnt = iTotalCnt + iManCnt;

                    iManCnt = 0;
                }

                iManCnt = iManCnt + 1;
            }

            if (nNum > 0)
            {
                /******* 마지막 거래처의 대한 로우 생성*****/
                row = table.NewRow();
                table.Rows.InsertAt(row, i);

                table.Rows[i]["KBHANGL"] = "[소 계]";
                table.Rows[i]["KBBSTEAMNM"] = "[ " + iManCnt.ToString() + " 명 ]";

                iTotalCnt = iTotalCnt + iManCnt;

                sFilter = "  KBGUNMU  = '" + table.Rows[i - 1]["KBGUNMU"].ToString() + "'";

                //상반기
                table.Rows[i]["FHALFAMOUNT"] = table.Compute("SUM(FHALFAMOUNT)", sFilter).ToString();
                //하반기
                table.Rows[i]["SHALFAMOUNT"] = table.Compute("SUM(SHALFAMOUNT)", sFilter).ToString();
                //총액
                table.Rows[i]["TOTALAMOUNT"] = table.Compute("SUM(TOTALAMOUNT)", sFilter).ToString();

                dFHALFAMOUNT = dFHALFAMOUNT + Convert.ToDouble(table.Rows[i]["FHALFAMOUNT"].ToString());
                dSHALFAMOUNT = dSHALFAMOUNT + Convert.ToDouble(table.Rows[i]["SHALFAMOUNT"].ToString());
                dTOTALAMOUNT = dTOTALAMOUNT + Convert.ToDouble(table.Rows[i]["TOTALAMOUNT"].ToString());


                /******** 총계를 위한 Row 생성 **************/
                row = table.NewRow();
                table.Rows.InsertAt(row, i + 1);

                table.Rows[i+1]["KBSABUN"] = "[합 계]";
                table.Rows[i + 1]["KBBSTEAMNM"] = "[ " + iTotalCnt.ToString() + " 명 ]";

                sFilter = "  KBGUNMU <> ''";

                table.Rows[i + 1]["FHALFAMOUNT"] = string.Format("{0:#,##0}", Convert.ToDouble(table.Compute("SUM(FHALFAMOUNT)", sFilter).ToString()));
                table.Rows[i + 1]["SHALFAMOUNT"] = string.Format("{0:#,##0}", Convert.ToDouble(table.Compute("SUM(SHALFAMOUNT)", sFilter).ToString()));
                table.Rows[i + 1]["TOTALAMOUNT"] = string.Format("{0:#,##0}", Convert.ToDouble(table.Compute("SUM(TOTALAMOUNT)", sFilter).ToString()));
            }

            return table;
        }
        #endregion   

    }
}
