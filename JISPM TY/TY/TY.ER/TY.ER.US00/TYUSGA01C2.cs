using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.ER.GB00;
using System.Drawing;

namespace TY.ER.US00
{
    /// <summary>
    /// 계근 현황 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2019.10.01 11:23
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_9A1A8271 : 계근 현황 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_9A1A1272 : 계근 현황 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    /// </summary>
    public partial class TYUSGA01C2 : TYBase
    {
        #region Description : 폼 로드
        public TYUSGA01C2()
        {
            InitializeComponent();
        }

        private void TYUSGA01C2_Load(object sender, System.EventArgs e)
        {
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            // A 계근대 조회
            DataTable dt = new DataTable();

            this.FPS91_TY_S_US_9A1A1272.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9A1A8271");
            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_9A1A1272.SetValue(UP_DataTableChange(dt));

            // B 계근대 조회
            this.FPS91_TY_S_US_9A1AE275.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_9A1AE273");
            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_9A1AE275.SetValue(UP_DataTableChange(dt));

            UP_Spread_Title();
        }
        #endregion

        #region Descriptoin : 스프레드 타이블 변경
        private void UP_Spread_Title()
        {
            this.FPS91_TY_S_US_9A1A1272_Sheet1.AddColumnHeaderSpanCell(0, 0, 1, 2);
            this.FPS91_TY_S_US_9A1A1272_Sheet1.ColumnHeader.Cells[0, 0].Value = "A 계근대";
            this.FPS91_TY_S_US_9A1A1272_Sheet1.ColumnHeader.Rows[0].Height = 45;
            this.FPS91_TY_S_US_9A1A1272_Sheet1.ColumnHeader.Cells[0, 0].Font = new Font("굴림", 14, FontStyle.Bold);

            this.FPS91_TY_S_US_9A1AE275_Sheet1.AddColumnHeaderSpanCell(0, 0, 1, 2);
            this.FPS91_TY_S_US_9A1AE275_Sheet1.ColumnHeader.Cells[0, 0].Value = "B 계근대";
            this.FPS91_TY_S_US_9A1AE275_Sheet1.ColumnHeader.Rows[0].Height = 45;
            this.FPS91_TY_S_US_9A1AE275_Sheet1.ColumnHeader.Cells[0, 0].Font = new Font("굴림", 14, FontStyle.Bold);

            for (int i = 0; i < this.FPS91_TY_S_US_9A1A1272.ActiveSheet.RowCount; i++)
            {
                this.FPS91_TY_S_US_9A1A1272.ActiveSheet.Rows[i].Height = 45;
                this.FPS91_TY_S_US_9A1A1272.ActiveSheet.Rows[i].Font = new Font("굴림", 14, FontStyle.Bold);
            }
            for (int i = 0; i < this.FPS91_TY_S_US_9A1AE275.ActiveSheet.RowCount; i++)
            {
                this.FPS91_TY_S_US_9A1AE275.ActiveSheet.Rows[i].Height = 45;
                this.FPS91_TY_S_US_9A1AE275.ActiveSheet.Rows[i].Font = new Font("굴림", 14, FontStyle.Bold);
            }
        }
        #endregion

        #region Description : 데이터 셋 변경
        private DataTable UP_DataTableChange(DataTable dt)
        {
            DataTable dtRtn = new DataTable();

            DataRow row;

            dtRtn.Columns.Add("TITLE", typeof(System.String));
            dtRtn.Columns.Add("CONTENT", typeof(System.String));

            if (dt.Rows.Count > 0)
            {
                row = dtRtn.NewRow();
                row["TITLE"] = "중     량";
                row["CONTENT"] = string.Format("{0:#,##0.00}", Convert.ToDouble(Get_Numeric(dt.Rows[0]["CHMTQTY"].ToString())));
                dtRtn.Rows.Add(row);
                row = dtRtn.NewRow();
                row["TITLE"] = "구     분";
                row["CONTENT"] = dt.Rows[0]["CHGEGUN"].ToString();
                dtRtn.Rows.Add(row);
                row = dtRtn.NewRow();
                row["TITLE"] = "차량번호";
                row["CONTENT"] = dt.Rows[0]["CHNUMBER"].ToString();
                dtRtn.Rows.Add(row);
                row = dtRtn.NewRow();
                row["TITLE"] = "화     주";
                row["CONTENT"] = dt.Rows[0]["CHHWAJU"].ToString();
                dtRtn.Rows.Add(row);
                row = dtRtn.NewRow();
                row["TITLE"] = "곡     종";
                row["CONTENT"] = dt.Rows[0]["CHGOKJONG"].ToString();
                dtRtn.Rows.Add(row);
                row = dtRtn.NewRow();
                row["TITLE"] = "원 산 지";
                row["CONTENT"] = dt.Rows[0]["CHWONSAN"].ToString();
                dtRtn.Rows.Add(row);
                row = dtRtn.NewRow();
                row["TITLE"] = "출고 BIN";
                row["CONTENT"] = dt.Rows[0]["CHBINNO"].ToString();
                dtRtn.Rows.Add(row);
                row = dtRtn.NewRow();
                row["TITLE"] = "MESSAGE";
                row["CONTENT"] = dt.Rows[0]["CHMESSAGE"].ToString();
                dtRtn.Rows.Add(row);
            }

            return dtRtn;
        }
        #endregion
    }
}
