using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;

namespace TY.ER.AC00
{
    /// <summary>
    /// 반제잔액조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.08.24 15:11
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_28O5S550 : 반제잔액조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_28R9I554 : 반제잔액조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_AC_2BK6A524 : 계정과목과 거래처 둘중 한가지는 입력해야 합니다.
    ///  TY_M_MR_2BGA1379 : 계정 코드를 입력하세요.
    ///  TY_M_MR_2BGA5395 : 거래처를 입력하세요.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  GCDDP : 사업장코드
    ///  GEDCDAC : 계정코드
    ///  GSTCDAC : 계정코드
    ///  GVEND : 거래처
    ///  GDATE : 일자
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    /// </summary>
    public partial class TYACFP007S : TYBase
    {
        #region Description : 페이지 로드
        public TYACFP007S()
        {
            InitializeComponent();
        }

        private void TYACFP007S_Load(object sender, System.EventArgs e)
        {
            (this.FPS91_TY_S_AC_28R9I554.Sheets[0].Columns[5].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.magnifier;
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_28R9I554, "BTN");

            this.DTP01_GSTDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));
            this.DTP01_GEDDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));
            this.DTP01_GDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));

            this.CBH01_GCDDP.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            

            SetStartingFocus(this.DTP01_GSTDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            if (this.CBH01_GSTCDAC.GetValue().ToString() == "" && this.CBH01_GEDCDAC.GetValue().ToString() == "" && this.CBH01_GVEND.GetValue().ToString() == "")
            {
                this.ShowMessage("TY_M_AC_2BK6A524");

                this.CBH01_GSTCDAC.CodeText.Focus();

                return;
            }

            if (this.CBH01_GSTCDAC.GetValue().ToString() == "" && this.CBH01_GEDCDAC.GetValue().ToString() != "")
            {
                this.ShowMessage("TY_M_MR_2BGA1379");

                this.CBH01_GSTCDAC.CodeText.Focus();

                return;
            }

            if (this.CBH01_GSTCDAC.GetValue().ToString() != "" && this.CBH01_GEDCDAC.GetValue().ToString() == "")
            {
                this.ShowMessage("TY_M_MR_2BGA1379");

                this.CBH01_GEDCDAC.CodeText.Focus();

                return;
            }


            this.FPS91_TY_S_AC_28R9I554.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_28O5S550",
                this.DTP01_GSTDATE.GetValue(),
                this.DTP01_GEDDATE.GetValue(),
                this.CBH01_GSTCDAC.GetValue(),
                this.CBH01_GEDCDAC.GetValue(),
                this.DTP01_GDATE.GetValue(),
                this.CBH01_GVEND.GetValue(),
                this.CBH01_GCDDP.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_28R9I554.SetValue(UP_ConvertDt(dt, "SEL"));

                // 특정 ROW 색깔 입히기
                for (int i = 0; i < this.FPS91_TY_S_AC_28R9I554.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_28R9I554.GetValue(i, "VNSANGHO").ToString() == "*** 합 계 ***")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_28R9I554.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                        this.FPS91_TY_S_AC_28R9I554_Sheet1.Cells[i, 5].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    }
                    else if (this.FPS91_TY_S_AC_28R9I554.GetValue(i, "VNSANGHO").ToString() == "*** 총합계 ***")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_28R9I554.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
                        this.FPS91_TY_S_AC_28R9I554_Sheet1.Cells[i, 5].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    }
                }
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            if (this.CBH01_GSTCDAC.GetValue().ToString() == "" && this.CBH01_GEDCDAC.GetValue().ToString() == "" && this.CBH01_GVEND.GetValue().ToString() == "")
            {
                this.ShowMessage("TY_M_AC_2BK6A524");

                this.CBH01_GSTCDAC.CodeText.Focus();

                return;
            }

            if (this.CBH01_GSTCDAC.GetValue().ToString() == "" && this.CBH01_GEDCDAC.GetValue().ToString() != "")
            {
                this.ShowMessage("TY_M_MR_2BGA1379");

                this.CBH01_GSTCDAC.CodeText.Focus();

                return;
            }

            if (this.CBH01_GSTCDAC.GetValue().ToString() != "" && this.CBH01_GEDCDAC.GetValue().ToString() == "")
            {
                this.ShowMessage("TY_M_MR_2BGA1379");

                this.CBH01_GEDCDAC.CodeText.Focus();

                return;
            }

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_28O5S550",
                this.DTP01_GSTDATE.GetValue(),
                this.DTP01_GEDDATE.GetValue(),
                this.CBH01_GSTCDAC.GetValue(),
                this.CBH01_GEDCDAC.GetValue(),
                this.DTP01_GDATE.GetValue(),
                this.CBH01_GVEND.GetValue(),
                this.CBH01_GCDDP.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYACFP007R();

                // 가로 출력
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, UP_ConvertDt(dt,"PRT"))).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        }
        #endregion

        #region Description : 데이터테이블 컨버젼
        private DataTable UP_ConvertDt(DataTable dt, string sGUBUN)
        {
            int i = 0;

            string sV1VEND = string.Empty;

            double dTotalAmt = 0;
            
            DataTable table  = new DataTable();

            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;

            for (i = 1; i < nNum; i++)
            {
                if (table.Rows[i - 1]["B7VEND"].ToString() != table.Rows[i]["B7VEND"].ToString())
                {
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    if (sGUBUN == "PRT")
                    {
                        table.Rows[i]["GIJUNDATE"] = table.Rows[i - 1]["GIJUNDATE"].ToString();
                        table.Rows[i]["DATE"]      = table.Rows[i - 1]["DATE"].ToString();
                        table.Rows[i]["CDACNM"]    = table.Rows[i - 1]["CDACNM"].ToString();
                        table.Rows[i]["B7VEND1"]   = table.Rows[i - 1]["B7VEND1"].ToString();
                    }
                    // 합 계 이름 넣기
                    table.Rows[i]["VNSANGHO"] = "*** 합 계 ***";

                    // 거래처번호
                    sV1VEND = "B7VEND = '" + table.Rows[i - 1]["B7VEND"].ToString() + "' ";

                    // 잔액
                    table.Rows[i]["JAN"] = table.Compute("SUM(JAN)", sV1VEND).ToString();

                    dTotalAmt = dTotalAmt + Convert.ToDouble(table.Rows[i]["JAN"].ToString());

                    nNum = nNum + 1;

                    i = i + 1;
                }
            }

            /******* 마지막 거래처의 대한 로우 생성*****/
            row = table.NewRow();
            table.Rows.InsertAt(row, i);
            // 합 계 이름 넣기
            if (sGUBUN == "PRT")
            {
                table.Rows[i]["GIJUNDATE"] = table.Rows[i - 1]["GIJUNDATE"].ToString();
                table.Rows[i]["DATE"]      = table.Rows[i - 1]["DATE"].ToString();
                table.Rows[i]["CDACNM"]    = table.Rows[i - 1]["CDACNM"].ToString();
                table.Rows[i]["B7VEND1"]   = table.Rows[i - 1]["B7VEND1"].ToString();
            }
            table.Rows[i]["VNSANGHO"] = "*** 합 계 ***";
            // 거래처번호
            sV1VEND = "B7VEND = '" + table.Rows[i - 1]["B7VEND"].ToString() + "' ";
            // 잔액
            table.Rows[i]["JAN"] = table.Compute("SUM(JAN)", sV1VEND).ToString();

            dTotalAmt = dTotalAmt + Convert.ToDouble(table.Rows[i]["JAN"].ToString());

            /******** 총계를 위한 Row 생성 **************/
            row = table.NewRow();
            table.Rows.InsertAt(row, i + 1);
            if (sGUBUN == "PRT")
            {
                if (nNum == 1)
                {
                    table.Rows[i + 1]["GIJUNDATE"] = table.Rows[i - 1]["GIJUNDATE"].ToString();
                    table.Rows[i + 1]["DATE"]      = table.Rows[i - 1]["DATE"].ToString();
                    table.Rows[i + 1]["CDACNM"]    = table.Rows[i - 1]["CDACNM"].ToString();
                    table.Rows[i + 1]["B7VEND1"]   = table.Rows[i - 1]["B7VEND1"].ToString();
                }
                else
                {
                    table.Rows[i + 1]["GIJUNDATE"] = table.Rows[i - 2]["GIJUNDATE"].ToString();
                    table.Rows[i + 1]["DATE"]      = table.Rows[i - 2]["DATE"].ToString();
                    table.Rows[i + 1]["CDACNM"]    = table.Rows[i - 2]["CDACNM"].ToString();
                    table.Rows[i + 1]["B7VEND1"]   = table.Rows[i - 2]["B7VEND1"].ToString();
                }
            }
            // 합 계 이름 넣기
            table.Rows[i + 1]["VNSANGHO"] = "*** 총합계 ***";

            table.Rows[i + 1]["JAN"] = string.Format("{0:#,##0}", dTotalAmt);

            return table;
        }
        #endregion

        #region Description : 기준일자 이벤트
        private void DTP01_GDATE_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_GCDDP.DummyValue = this.DTP01_GDATE.GetValue();
        }
        #endregion

        #region Description : 그리드 버튼 클릭 이벤트
        private void FPS91_TY_S_AC_28R9I554_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "5")
            {
                if (this.FPS91_TY_S_AC_28R9I554.GetValue("JPNO").ToString() != "")
                {
                    string sB2DPMK = this.FPS91_TY_S_AC_28R9I554.GetValue("JPNO").ToString().Substring(0, 6);
                    string sB2DTMK = this.FPS91_TY_S_AC_28R9I554.GetValue("JPNO").ToString().Substring(7, 8);
                    string sB2NOSQ = this.FPS91_TY_S_AC_28R9I554.GetValue("JPNO").ToString().Substring(16, 3);

                    if (this.OpenModalPopup(new TYACBJ001I(sB2DPMK, sB2DTMK, sB2NOSQ)) == System.Windows.Forms.DialogResult.OK)
                        this.BTN61_INQ_Click(null, null);
                }
            }
        }
        #endregion

    }
}