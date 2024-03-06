﻿using System;
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
    /// 재무상태표 현금성자산 조회 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2014.06.19 15:11
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_46JHU844 : 결산 재무상태표 부속서류 현금성자산 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_46JHV845 : 결산 재무상태표 부속서류 현금성자산 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  BSCDAC : 계정과목
    ///  GSTDATE : 시작일자
    /// </summary>
    public partial class TYACSE008S : TYBase
    {
  
        #region Description : 페이지 로드
        public TYACSE008S()
        {
            InitializeComponent();
        }

        private void TYACSE008S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_INQ.ProcessCheck += new TButton.CheckHandler(BTN61_INQ_ProcessCheck);
           // UP_Spread_Title();

            this.DTP01_GSTDATE.Focus();

        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sCDAC = string.Empty;
            sCDAC = this.CBO01_BSCDAC.GetValue().ToString();

            if (sCDAC == "00000000")
            {
                sCDAC = "";
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_46JHU844", sCDAC , this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 6));

            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_46JHV845.SetValue(dt);
            }
        }
        #endregion

        #region Description : 조회 CHECK
        private void BTN61_INQ_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sCDAC = string.Empty;
            sCDAC = this.CBO01_BSCDAC.GetValue().ToString();

            if (sCDAC == "00000000")
            {
                sCDAC = "";
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_46JHU844", sCDAC, this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 6));
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count == 0)
            {
                this.ShowCustomMessage("자료가 존재하지 않습니다.", "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_479DT990", this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 6));

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYACSE010R1();

                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
        }
        #endregion

        #region Description : 스프레드 타이틀 변경
        //private void UP_Spread_Title()
        //{
            //this.FPS91_TY_S_AC_46JHV845_Sheet1.ColumnHeaderRowCount = 2;
            //this.FPS91_TY_S_AC_46JHV845_Sheet1.RowHeaderColumnCount = 1;

            ////(현재ROW, 현재COLUMN, 묶을ROW수, 묶을 COLUMN수)
            //this.FPS91_TY_S_AC_46JHV845_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1); // 계정코드
            //this.FPS91_TY_S_AC_46JHV845_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1); // 계정명
            //this.FPS91_TY_S_AC_46JHV845_Sheet1.AddColumnHeaderSpanCell(0, 8, 2, 1); // 전년대비(감액)

            //this.FPS91_TY_S_AC_46JHV845_Sheet1.AddColumnHeaderSpanCell(0, 4, 1, 2); // 당기금액
            //this.FPS91_TY_S_AC_46JHV845_Sheet1.AddColumnHeaderSpanCell(0, 6, 1, 2); // 전기금액

            //this.FPS91_TY_S_AC_46JHV845_Sheet1.ColumnHeader.Cells[0, 2].Value = "계정코드";
            //this.FPS91_TY_S_AC_46JHV845_Sheet1.ColumnHeader.Cells[0, 3].Value = "계정명";

            //this.FPS91_TY_S_AC_46JHV845_Sheet1.ColumnHeader.Cells[0, 4].Value = "당기금액";
            //this.FPS91_TY_S_AC_46JHV845_Sheet1.ColumnHeader.Cells[0, 6].Value = "전기금액";
            //this.FPS91_TY_S_AC_46JHV845_Sheet1.ColumnHeader.Cells[0, 8].Value = "전년대비 증(감)액";


            //this.FPS91_TY_S_AC_46JHV845_Sheet1.ColumnHeader.Cells[1, 2].Value = "";
            //this.FPS91_TY_S_AC_46JHV845_Sheet1.ColumnHeader.Cells[1, 3].Value = "";
            //this.FPS91_TY_S_AC_46JHV845_Sheet1.ColumnHeader.Cells[1, 4].Value = "차변";
            //this.FPS91_TY_S_AC_46JHV845_Sheet1.ColumnHeader.Cells[1, 5].Value = "대변";
            //this.FPS91_TY_S_AC_46JHV845_Sheet1.ColumnHeader.Cells[1, 6].Value = "차변";
            //this.FPS91_TY_S_AC_46JHV845_Sheet1.ColumnHeader.Cells[1, 7].Value = "대변";

            //if (this.FPS91_TY_S_AC_46JHV845_Sheet1.AlternatingRows.Count > 0)
            //    this.FPS91_TY_S_AC_46JHV845_Sheet1.AlternatingRows[0].BackColor = Color.White;

            //this.FPS91_TY_S_AC_46JHV845_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.FPS91_TY_S_AC_46JHV845_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //this.FPS91_TY_S_AC_46JHV845_Sheet1.ColumnHeader.Cells[0, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        //}
        #endregion
    }
}