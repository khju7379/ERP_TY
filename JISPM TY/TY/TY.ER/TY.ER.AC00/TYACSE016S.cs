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
    /// 결산 손익계산서 조회-일반관리비 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2014.06.17 15:11
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_46IB1825 : 전년대비 손익계산서-일반관리비 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_46IB4826 : 결산 손익조회_일반관리비
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    /// </summary>
    public partial class TYACSE016S : TYBase
    {
  
        #region Description : 페이지 로드
        public TYACSE016S()
        {
            InitializeComponent();
        }

        private void TYACSE016S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_INQ.ProcessCheck += new TButton.CheckHandler(BTN61_INQ_ProcessCheck);
            UP_Spread_Title();

            this.DTP01_GEDDATE.SetReadOnly(true);

            this.DTP01_GSTDATE.Focus();

        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sProced_id  = "TY_P_AC_46IB1825";
            string s시작계정 = "44210100";
            string s종료계정 = "44214500";

            this.DbConnector.CommandClear();
            this.DbConnector.Attach(sProced_id, s시작계정, s종료계정, this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 6));

            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                this.DTP01_GEDDATE.SetValue(dt.Rows[0]["ANYYMMCM"].ToString().Trim());

                this.FPS91_TY_S_AC_46IB4826.SetValue(dt);
            }
        }
        #endregion

        #region Description : 조회 CHECK
        private void BTN61_INQ_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sProced_id = "TY_P_AC_46IB1825";
            string s시작계정 = "44210100";
            string s종료계정 = "44214500";

            this.DbConnector.CommandClear();
            this.DbConnector.Attach(sProced_id, s시작계정, s종료계정, this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 6));
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count == 0)
            {
                this.ShowCustomMessage("자료가 존재하지 않습니다.", "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

        }
        #endregion

        #region Description : 스프레드 타이틀 변경(일반관리비)
        private void UP_Spread_Title()
        {
            this.FPS91_TY_S_AC_46IB4826_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_46IB4826_Sheet1.RowHeaderColumnCount = 1;

            //(현재ROW, 현재COLUMN, 묶을ROW수, 묶을 COLUMN수)
            this.FPS91_TY_S_AC_46IB4826_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1); // 계정코드
            this.FPS91_TY_S_AC_46IB4826_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1); // 계정명

            this.FPS91_TY_S_AC_46IB4826_Sheet1.AddColumnHeaderSpanCell(0, 4, 1, 6); // 당기금액
            this.FPS91_TY_S_AC_46IB4826_Sheet1.AddColumnHeaderSpanCell(0, 10, 1, 6); // 전기금액
            this.FPS91_TY_S_AC_46IB4826_Sheet1.AddColumnHeaderSpanCell(0, 16, 1, 6); // 전년대비(감액)

            this.FPS91_TY_S_AC_46IB4826_Sheet1.ColumnHeader.Cells[0, 2].Value = "계정코드";
            this.FPS91_TY_S_AC_46IB4826_Sheet1.ColumnHeader.Cells[0, 3].Value = "계정명";

            this.FPS91_TY_S_AC_46IB4826_Sheet1.ColumnHeader.Cells[0, 4].Value = "당기금액";
            this.FPS91_TY_S_AC_46IB4826_Sheet1.ColumnHeader.Cells[0, 10].Value = "전기금액";
            this.FPS91_TY_S_AC_46IB4826_Sheet1.ColumnHeader.Cells[0, 16].Value = "전년대비 증(감)액";


            this.FPS91_TY_S_AC_46IB4826_Sheet1.ColumnHeader.Cells[1, 2].Value = "";
            this.FPS91_TY_S_AC_46IB4826_Sheet1.ColumnHeader.Cells[1, 3].Value = "";
            this.FPS91_TY_S_AC_46IB4826_Sheet1.ColumnHeader.Cells[1, 4].Value = "경영지원";
            this.FPS91_TY_S_AC_46IB4826_Sheet1.ColumnHeader.Cells[1, 5].Value = "기획재무";
            this.FPS91_TY_S_AC_46IB4826_Sheet1.ColumnHeader.Cells[1, 6].Value = "울산공통";
            this.FPS91_TY_S_AC_46IB4826_Sheet1.ColumnHeader.Cells[1, 7].Value = "기획공통";
            this.FPS91_TY_S_AC_46IB4826_Sheet1.ColumnHeader.Cells[1, 8].Value = "공    통";
            this.FPS91_TY_S_AC_46IB4826_Sheet1.ColumnHeader.Cells[1, 9].Value = "계";

            this.FPS91_TY_S_AC_46IB4826_Sheet1.ColumnHeader.Cells[1, 10].Value = "경영지원";
            this.FPS91_TY_S_AC_46IB4826_Sheet1.ColumnHeader.Cells[1, 11].Value = "기획재무";
            this.FPS91_TY_S_AC_46IB4826_Sheet1.ColumnHeader.Cells[1, 12].Value = "울산공통";
            this.FPS91_TY_S_AC_46IB4826_Sheet1.ColumnHeader.Cells[1, 13].Value = "기획공통";
            this.FPS91_TY_S_AC_46IB4826_Sheet1.ColumnHeader.Cells[1, 14].Value = "공    통";
            this.FPS91_TY_S_AC_46IB4826_Sheet1.ColumnHeader.Cells[1, 15].Value = "계";

            this.FPS91_TY_S_AC_46IB4826_Sheet1.ColumnHeader.Cells[1, 16].Value = "경영지원";
            this.FPS91_TY_S_AC_46IB4826_Sheet1.ColumnHeader.Cells[1, 17].Value = "기획재무";
            this.FPS91_TY_S_AC_46IB4826_Sheet1.ColumnHeader.Cells[1, 18].Value = "울산공통";
            this.FPS91_TY_S_AC_46IB4826_Sheet1.ColumnHeader.Cells[1, 19].Value = "기획공통";
            this.FPS91_TY_S_AC_46IB4826_Sheet1.ColumnHeader.Cells[1, 20].Value = "공    통";
            this.FPS91_TY_S_AC_46IB4826_Sheet1.ColumnHeader.Cells[1, 21].Value = "계";

            if (this.FPS91_TY_S_AC_46IB4826_Sheet1.AlternatingRows.Count > 0)
                this.FPS91_TY_S_AC_46IB4826_Sheet1.AlternatingRows[0].BackColor = Color.White;


            this.FPS91_TY_S_AC_46IB4826_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_46IB4826_Sheet1.ColumnHeader.Cells[0, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_46IB4826_Sheet1.ColumnHeader.Cells[0, 16].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion
    }
}