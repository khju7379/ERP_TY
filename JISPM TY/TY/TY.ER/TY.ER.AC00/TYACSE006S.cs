using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TY.Service.Library;
using TY.ER.GB00;
using GrapeCity.ActiveReports;
using Shoveling2010.SmartClient.SystemUtility.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 채권채무 조회 명세서 조회 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2014.06.25 15:11
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_46PDD884 : 채무 조회
    ///  TY_P_AC_46PDD885 : 채권 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_46PDA883 : 채권 채무 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  GSTDATE : 시작일자
    ///  AHBLIGCD : 채권채무코드
    /// </summary>
    public partial class TYACSE006S : TYBase
    {
  
        #region Description : 페이지 로드
        public TYACSE006S()
        {
            InitializeComponent();
        }

        private void TYACSE006S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_INQ.ProcessCheck += new TButton.CheckHandler(BTN61_INQ_ProcessCheck);

            this.DTP01_GSTDATE.Focus();

        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sProcedureID = string.Empty;

            this.FPS91_TY_S_AC_46PDA883.Initialize();

            if (this.CBO01_AHBLIGCD.GetValue().ToString() == "A")
            {
                sProcedureID = "TY_P_AC_46PDD885";  // 채권
                UP_Spread_Title_1();
            }
            else
            {
                sProcedureID = "TY_P_AC_46PDD884";  // 채무
                UP_Spread_Title_2();
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach(sProcedureID, this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 6));

            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_46PDA883.SetValue(dt);

                this.SpreadSumRowAdd(this.FPS91_TY_S_AC_46PDA883, "VNSANGHO", " [ 합       계 ]", SumRowType.Total, "TOTAMT01", "TOTAMT02", "TOTAMT03", "TOTAMT04");
            }
        }
        #endregion

        #region Description : 조회 CHECK
        private void BTN61_INQ_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sProcedureID = string.Empty;

            if (this.CBO01_AHBLIGCD.GetValue().ToString() == "A")
            {
                sProcedureID = "TY_P_AC_46PDD885";  // 채권
            }
            else
            {
                sProcedureID = "TY_P_AC_46PDD884";  // 채무
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach(sProcedureID, this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 6));
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count == 0)
            {
                this.ShowCustomMessage("자료가 존재하지 않습니다.", "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 스프레드 타이틀 변경 (채권)
        private void UP_Spread_Title_1()
        {

            this.FPS91_TY_S_AC_46PDA883_Sheet1.ColumnHeaderRowCount = 1;
            this.FPS91_TY_S_AC_46PDA883_Sheet1.RowHeaderColumnCount = 1;


            this.FPS91_TY_S_AC_46PDA883_Sheet1.ColumnHeader.Cells[0, 0].Value = "기준년월";
            this.FPS91_TY_S_AC_46PDA883_Sheet1.ColumnHeader.Cells[0, 1].Value = "거래처";
            this.FPS91_TY_S_AC_46PDA883_Sheet1.ColumnHeader.Cells[0, 2].Value = "거래처명";
            this.FPS91_TY_S_AC_46PDA883_Sheet1.ColumnHeader.Cells[0, 3].Value = "외상매출금";
            this.FPS91_TY_S_AC_46PDA883_Sheet1.ColumnHeader.Cells[0, 4].Value = "받을어음";
            this.FPS91_TY_S_AC_46PDA883_Sheet1.ColumnHeader.Cells[0, 5].Value = "미수금";
            this.FPS91_TY_S_AC_46PDA883_Sheet1.ColumnHeader.Cells[0, 6].Value = "합      계";

            this.FPS91_TY_S_AC_46PDA883_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;


            if (this.FPS91_TY_S_AC_46PDA883_Sheet1.AlternatingRows.Count > 0)
                this.FPS91_TY_S_AC_46PDA883_Sheet1.AlternatingRows[0].BackColor = Color.White;

        }
        #endregion

        #region Description : 스프레드 타이틀 변경 (채무)
        private void UP_Spread_Title_2()
        {

            this.FPS91_TY_S_AC_46PDA883_Sheet1.ColumnHeaderRowCount = 1;
            this.FPS91_TY_S_AC_46PDA883_Sheet1.RowHeaderColumnCount = 1;


            this.FPS91_TY_S_AC_46PDA883_Sheet1.ColumnHeader.Cells[0, 0].Value = "기준년월";
            this.FPS91_TY_S_AC_46PDA883_Sheet1.ColumnHeader.Cells[0, 1].Value = "거래처";
            this.FPS91_TY_S_AC_46PDA883_Sheet1.ColumnHeader.Cells[0, 2].Value = "거래처명";
            this.FPS91_TY_S_AC_46PDA883_Sheet1.ColumnHeader.Cells[0, 3].Value = "외상매입금";
            this.FPS91_TY_S_AC_46PDA883_Sheet1.ColumnHeader.Cells[0, 4].Value = "미지급금";
            this.FPS91_TY_S_AC_46PDA883_Sheet1.ColumnHeader.Cells[0, 5].Value = "지급어음";
            this.FPS91_TY_S_AC_46PDA883_Sheet1.ColumnHeader.Cells[0, 6].Value = "합      계";

            this.FPS91_TY_S_AC_46PDA883_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;


            if (this.FPS91_TY_S_AC_46PDA883_Sheet1.AlternatingRows.Count > 0)
                this.FPS91_TY_S_AC_46PDA883_Sheet1.AlternatingRows[0].BackColor = Color.White;

        }
        #endregion
    }
}