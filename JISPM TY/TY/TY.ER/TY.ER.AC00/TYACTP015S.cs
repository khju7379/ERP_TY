using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.AC00
{
    /// <summary>
    /// 명세서 집계 조회 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2014.12.12 17:57
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_4CJBY884 : 지급 명세서 집계조회 (집계)
    ///  TY_P_AC_4CJDY895 : 지급 명세서 집계조회 (사업,기타,이자,배당-상세)
    ///  TY_P_AC_4CJDY896 : 지급 명세서 집계조회 (퇴직소득-상세)
    ///  TY_P_AC_4CJDZ897 : 지급 명세서 집계조회 (근로소득(중도퇴사)-상세)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_4CCB0775 : 명세서 집계표 조회(집계)
    ///  TY_S_AC_4CCB1776 : 명세서 집계표 조회(상세)
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  WABRANCH : 지점구분
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACTP015S : TYBase
    {

        #region Description : 페이지 로드
        public TYACTP015S()
        {
            InitializeComponent();
        }

        private void TYACTP015S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_INQ.ProcessCheck += new TButton.CheckHandler(BTN61_INQ_ProcessCheck);

            this.DTP01_GSTYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            this.DTP01_GEDYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.BTN61_INQ_Click(null, null);
            this.FPS91_TY_S_AC_4CCB1776.Initialize();

            SetStartingFocus(this.CBO01_WRGUNMU);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sYEYYMM = string.Empty;

            if (this.DTP01_GEDYYMM.GetValue().ToString().Substring(4, 2) == "12")
            {
                sYEYYMM = Convert.ToString(Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 4)) + 1 ) + "02";
            }else
            {
                sYEYYMM = Convert.ToString(Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 4)) + 1) + "00";
            }

            this.FPS91_TY_S_AC_4CCB0775.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_4CJBY884", this.CBO01_WRGUNMU.GetValue().ToString(), this.DTP01_GSTYYMM.GetValue().ToString(), this.DTP01_GEDYYMM.GetValue().ToString(), sYEYYMM, TYUserInfo.SecureKey, "Y");
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_4CCB0775.SetValue(dt);

                this.FPS91_TY_S_AC_4CCB0775_Sheet1.Cells[0, 1].BackColor = Color.Yellow;
                this.FPS91_TY_S_AC_4CCB0775_Sheet1.Cells[1, 1].BackColor = Color.Yellow;
                this.FPS91_TY_S_AC_4CCB0775_Sheet1.Cells[2, 1].BackColor = Color.Yellow;
                this.FPS91_TY_S_AC_4CCB0775_Sheet1.Cells[3, 1].BackColor = Color.Yellow;
                this.FPS91_TY_S_AC_4CCB0775_Sheet1.Cells[4, 1].BackColor = Color.Yellow;
                this.FPS91_TY_S_AC_4CCB0775_Sheet1.Cells[5, 1].BackColor = Color.Yellow;
            }
        }
        #endregion

        #region Description : 조회 CHECK
        private void BTN61_INQ_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sYEYYMM = string.Empty;
            if (this.DTP01_GEDYYMM.GetValue().ToString().Substring(4, 2) == "12")
            {
                sYEYYMM = Convert.ToString(Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 4)) + 1) + "02";
            }
            else
            {
                sYEYYMM = Convert.ToString(Convert.ToDouble(this.DTP01_GEDYYMM.GetValue().ToString().Substring(0, 4)) + 1) + "00";
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_4CJBY884", this.CBO01_WRGUNMU.GetValue().ToString(), this.DTP01_GSTYYMM.GetValue().ToString(), this.DTP01_GEDYYMM.GetValue().ToString(), sYEYYMM, TYUserInfo.SecureKey, "Y");
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count == 0)
            {
                this.ShowCustomMessage("자료가 존재하지 않습니다.", "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }
        }
        #endregion
       
        #region Description : 명세서 집계표 스프레드 클릭 이벤트
        private void FPS91_TY_S_AC_4CCB0775_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            UP_Content_Display(e.Row, e.Column);
        }
        #endregion

        #region Description : 세부내역 보여주기
        private void UP_Content_Display(int iRow, int iCol)
        {
            switch (iRow)
            {
                case 0:
                    if (iCol == 1)
                    {
                        UP_Details_display("A25"); // 사업소득 A25
                    }
                    break;

                case 1:
                    if (iCol == 1)
                    {
                        UP_Details_display("A42");  // 기타소득 A42
                    }
                    break;

                case 2:
                    if (iCol == 1)
                    {
                        UP_Details_display("A50"); // 이자소득 A50
                    }
                    break;

                case 3:
                    if (iCol == 1)
                    {
                        UP_Details_display("A60"); // 배당소득 A60
                    }
                    break;

                case 4:
                    if (iCol == 1)
                    {
                        UP_Retirement_display_One("S05"); // 퇴직소득 
                    }
                    break;

                case 5:

                    if (iCol == 1)
                    {
                        UP_Details_display_two("S06"); // 근로소득(중도퇴사)
                    }
                    break;
            }
        }
        #endregion


        #region Description :  집계표 상세 내역 - 1
        private void UP_Details_display(string sINCOME)
        {
            this.FPS91_TY_S_AC_4CCB1776.Initialize();
            UP_Spread_Daily_Title(sINCOME);

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_4CJDY895", this.CBO01_WRGUNMU.GetValue().ToString(), this.DTP01_GSTYYMM.GetValue().ToString(), this.DTP01_GEDYYMM.GetValue().ToString(), sINCOME, TYUserInfo.SecureKey, "Y"  );
            this.FPS91_TY_S_AC_4CCB1776.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description :  집계표 상세 내역 - 퇴직소득
        private void UP_Retirement_display_One(string sINCOME)
        {
            this.FPS91_TY_S_AC_4CCB1776.Initialize();
            UP_Spread_Daily_Title(sINCOME);

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_4CJDY896", this.CBO01_WRGUNMU.GetValue().ToString(), this.DTP01_GSTYYMM.GetValue().ToString(), this.DTP01_GEDYYMM.GetValue().ToString(), sINCOME, TYUserInfo.SecureKey, "Y");
            this.FPS91_TY_S_AC_4CCB1776.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description :  집계표 상세 내역 - 중도퇴사
        private void UP_Details_display_two(string sINCOME)
        {
            this.FPS91_TY_S_AC_4CCB1776.Initialize();
            UP_Spread_Daily_Title(sINCOME);

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_4CJDZ897", this.CBO01_WRGUNMU.GetValue().ToString(), this.DTP01_GSTYYMM.GetValue().ToString(), this.DTP01_GEDYYMM.GetValue().ToString(), sINCOME, TYUserInfo.SecureKey, "Y" );
            this.FPS91_TY_S_AC_4CCB1776.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 스프레드 타이틀 변경
        private void UP_Spread_Daily_Title(string sINCOME)
        {
            this.FPS91_TY_S_AC_4CCB1776_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_4CCB1776_Sheet1.RowHeaderColumnCount = 1;

            //(현재ROW, 현재COLUMN, 묶을ROW수, 묶을 COLUMN수)
            this.FPS91_TY_S_AC_4CCB1776_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1); // 귀속년월
            this.FPS91_TY_S_AC_4CCB1776_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1); // 주민번호
            this.FPS91_TY_S_AC_4CCB1776_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1); // 성명
            this.FPS91_TY_S_AC_4CCB1776_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1); // 지급년월일
            this.FPS91_TY_S_AC_4CCB1776_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1); // 총지급액
            this.FPS91_TY_S_AC_4CCB1776_Sheet1.AddColumnHeaderSpanCell(0, 5, 1, 4); // 원천징수세액
            this.FPS91_TY_S_AC_4CCB1776_Sheet1.AddColumnHeaderSpanCell(0, 9, 2, 1); // 비 고
            this.FPS91_TY_S_AC_4CCB1776_Sheet1.AddColumnHeaderSpanCell(0, 10, 2, 1); // 출력

            this.FPS91_TY_S_AC_4CCB1776_Sheet1.ColumnHeader.Cells[0, 0].Value = "귀속년월";
            this.FPS91_TY_S_AC_4CCB1776_Sheet1.ColumnHeader.Cells[0, 1].Value = "주민번호";
            this.FPS91_TY_S_AC_4CCB1776_Sheet1.ColumnHeader.Cells[0, 2].Value = "성명";
            this.FPS91_TY_S_AC_4CCB1776_Sheet1.ColumnHeader.Cells[0, 3].Value = "지급년월일";
            this.FPS91_TY_S_AC_4CCB1776_Sheet1.ColumnHeader.Cells[0, 4].Value = "총지급액";
            if (sINCOME == "S06")
            {
                this.FPS91_TY_S_AC_4CCB1776_Sheet1.ColumnHeader.Cells[0, 5].Value = "차가감징수세액";
            }
            else
            {
                this.FPS91_TY_S_AC_4CCB1776_Sheet1.ColumnHeader.Cells[0, 5].Value = "원천징수세액";
            }
            this.FPS91_TY_S_AC_4CCB1776_Sheet1.ColumnHeader.Cells[0, 9].Value = "비 고";
            this.FPS91_TY_S_AC_4CCB1776_Sheet1.ColumnHeader.Cells[0, 10].Value = "출력";

            this.FPS91_TY_S_AC_4CCB1776_Sheet1.ColumnHeader.Cells[1, 0].Value = "";
            this.FPS91_TY_S_AC_4CCB1776_Sheet1.ColumnHeader.Cells[1, 1].Value = "";
            this.FPS91_TY_S_AC_4CCB1776_Sheet1.ColumnHeader.Cells[1, 2].Value = "";
            this.FPS91_TY_S_AC_4CCB1776_Sheet1.ColumnHeader.Cells[1, 3].Value = "";
            this.FPS91_TY_S_AC_4CCB1776_Sheet1.ColumnHeader.Cells[1, 4].Value = "";
            this.FPS91_TY_S_AC_4CCB1776_Sheet1.ColumnHeader.Cells[1, 5].Value = "소득세";
            this.FPS91_TY_S_AC_4CCB1776_Sheet1.ColumnHeader.Cells[1, 6].Value = "지방소득세";
            this.FPS91_TY_S_AC_4CCB1776_Sheet1.ColumnHeader.Cells[1, 7].Value = "농특세";
            this.FPS91_TY_S_AC_4CCB1776_Sheet1.ColumnHeader.Cells[1, 8].Value = "세액계";
            this.FPS91_TY_S_AC_4CCB1776_Sheet1.ColumnHeader.Cells[1, 9].Value = "";
            this.FPS91_TY_S_AC_4CCB1776_Sheet1.ColumnHeader.Cells[1, 10].Value = "";

            if (this.FPS91_TY_S_AC_4CCB1776_Sheet1.AlternatingRows.Count > 0)
                this.FPS91_TY_S_AC_4CCB1776_Sheet1.AlternatingRows[0].BackColor = Color.White;
            this.FPS91_TY_S_AC_4CCB1776_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

        }
        #endregion
    }
}