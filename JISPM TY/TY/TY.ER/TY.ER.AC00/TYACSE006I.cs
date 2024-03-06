using System;
using System.Data;
using System.Drawing;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.AC00
{
    public partial class TYACSE006I : TYBase
    {
        #region Description : 페이지 로드
        public TYACSE006I()
        {
            InitializeComponent();
        }

        private void TYACSE006I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_PRT.ProcessCheck += new TButton.CheckHandler(BTN61_PRT_ProcessCheck);

            UP_Spread_Title();

            SetStartingFocus(this.DTP01_AOCRYYMM);
        }
        #endregion

        #region Description : 스프레드 타이틀
        private void UP_Spread_Title()
        {
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_43OBZ956_Sheet1.AddColumnHeaderSpanCell(0,  5, 1, 2); // 외화 외상매출금
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.AddColumnHeaderSpanCell(0, 13, 1, 2); // 외화 외상매입금
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.AddColumnHeaderSpanCell(0, 20, 1, 4); // 채권채무 담당자

            this.FPS91_TY_S_AC_43OBZ956_Sheet1.AddColumnHeaderSpanCell(0,  0, 2, 1);
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.AddColumnHeaderSpanCell(0,  1, 2, 1);
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.AddColumnHeaderSpanCell(0,  2, 2, 1);
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.AddColumnHeaderSpanCell(0,  3, 2, 1);
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.AddColumnHeaderSpanCell(0,  4, 2, 1);

            this.FPS91_TY_S_AC_43OBZ956_Sheet1.AddColumnHeaderSpanCell(0,  7, 2, 1);
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.AddColumnHeaderSpanCell(0,  8, 2, 1);
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.AddColumnHeaderSpanCell(0,  9, 2, 1);
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.AddColumnHeaderSpanCell(0,  10, 2, 1);

            this.FPS91_TY_S_AC_43OBZ956_Sheet1.AddColumnHeaderSpanCell(0, 11, 2, 1);
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.AddColumnHeaderSpanCell(0, 12, 2, 1);
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.AddColumnHeaderSpanCell(0, 15, 2, 1);
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.AddColumnHeaderSpanCell(0, 16, 2, 1);
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.AddColumnHeaderSpanCell(0, 17, 2, 1);
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.AddColumnHeaderSpanCell(0, 18, 2, 1);
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.AddColumnHeaderSpanCell(0, 19, 2, 1);

            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 0].Value  = "출  력";
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 1].Value  = "거래처";
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 2].Value  = "거래처명";
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 3].Value  = "채권계(원화)";
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 4].Value  = "원화외상매출금";
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 5].Value  = "외화외상매출금";
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 7].Value  = "미청구외상매출금";
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 8].Value  = "받을어음";
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 9].Value  = "선급금";
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 10].Value = "미수금";
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 11].Value = "채무계(원화)";
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 12].Value = "원화외상매입금";
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 13].Value = "외화외상매입금";
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 15].Value = "미지급금";
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 16].Value = "선 수 금";
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 17].Value = "지급어음";
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 18].Value = "우편번호";
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 19].Value = "주    소";
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 20].Value = "채권채무 담당자";

            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[1,  5].Value  = "외화금액";
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[1,  6].Value  = "원화금액";
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[1, 13].Value  = "외화금액";
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[1, 14].Value  = "원화금액";
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[1, 20].Value  = "직    책";
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[1, 21].Value  = "성    명";
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[1, 22].Value  = "전화번호";
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[1, 23].Value  = "팩스번호";

            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 2].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 3].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 8].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 9].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 12].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 13].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 15].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 16].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 17].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 18].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 19].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[0, 20].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[1, 5].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[1, 6].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[1, 13].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[1, 14].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[1, 20].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[1, 21].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[1, 22].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_43OBZ956_Sheet1.ColumnHeader.Cells[1, 23].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_43OBV954",
                this.DTP01_AOCRYYMM.GetValue().ToString(),
                this.CBH01_E6CDCL.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();
            this.FPS91_TY_S_AC_43OBZ956.SetValue(dt);
        }
        #endregion

        #region Description : 채권채무 MAST 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            int i = 0;
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_43OGK961", ds.Tables[0].Rows[i]["AOA2USDAMT"].ToString(),
                                                                ds.Tables[0].Rows[i]["AOB2USDAMT"].ToString(),
                                                                this.DTP01_AOCRYYMM.GetValue().ToString(),
                                                                ds.Tables[0].Rows[i]["AOCRCUST"].ToString()
                                                                );
                }

                this.DbConnector.ExecuteTranQueryList();
            }

            this.ShowMessage("TY_M_GB_23NAD873");
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();

                int i = 0;
                DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

                this.DbConnector.CommandClear();

                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_44GDR184",
                        this.DTP01_AOCRYYMM.GetValue().ToString(),
                        //this.FPS91_TY_S_AC_43OBZ956.GetValue("AOCRCUST").ToString()
                        ds.Tables[0].Rows[i]["AOCRCUST"].ToString()
                        //"361390"
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        SectionReport rpt = new TYACSE006R();

                        (new TYERGB001P(rpt, dt)).ShowDialog();
                    }
                }
            }
            catch
            {
            }
        }
        #endregion

        #region Description : 채권채무 MAST 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataSet ds = new DataSet();

            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_43OBZ956.GetDataSourceInclude(TSpread.TActionType.Update, "AOA2USDAMT", "AOA2WONAMT", "AOB2USDAMT", "AOB2WONAMT", "AOCRCUST"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
                e.Successed = false;
                return;
            }

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["AOA2WONAMT"].ToString())) == 0 &&
                    double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["AOA2USDAMT"].ToString())) != 0)
                {
                    this.ShowMessage("TY_M_AC_43P8Z962");
                    e.Successed = false;
                    return;
                }

                if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["AOA2WONAMT"].ToString())) != 0 &&
                    double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["AOA2USDAMT"].ToString())) == 0)
                {
                    this.ShowMessage("TY_M_AC_43P8Z962");
                    e.Successed = false;
                    return;
                }

                if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["AOB2WONAMT"].ToString())) == 0 &&
                    double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["AOB2USDAMT"].ToString())) != 0)
                {
                    this.ShowMessage("TY_M_AC_43P8Z962");
                    e.Successed = false;
                    return;
                }

                if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["AOB2WONAMT"].ToString())) != 0 &&
                    double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["AOB2USDAMT"].ToString())) == 0)
                {
                    this.ShowMessage("TY_M_AC_43P8Z962");
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 채권채무 MAST 저장 ProcessCheck 이벤트
        private void BTN61_PRT_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataSet ds = new DataSet();

            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_43OBZ956.GetDataSourceInclude(TSpread.TActionType.Update, "AOCRCUST"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 채권채무 내역 조회
        private void UP_Set_Detail(string sAOCRCUST)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_43OBY955",
                this.DTP01_AOCRYYMM.GetValue().ToString(),
                sAOCRCUST.ToString(),
                this.DTP01_AOCRYYMM.GetValue().ToString(),
                sAOCRCUST.ToString(),
                this.DTP01_AOCRYYMM.GetValue().ToString(),
                sAOCRCUST.ToString(),
                this.DTP01_AOCRYYMM.GetValue().ToString(),
                sAOCRCUST.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_43OFT957.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_AC_43OFT957.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_43OFT957.GetValue(i, "AHBLIGGB").ToString() == "채권계" ||
                    this.FPS91_TY_S_AC_43OFT957.GetValue(i, "AHBLIGGB").ToString() == "채무계")
                {
                    this.FPS91_TY_S_AC_43OFT957.ActiveSheet.Rows[i].ForeColor = Color.Red;
                    this.FPS91_TY_S_AC_43OFT957.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                }

                if (this.FPS91_TY_S_AC_43OFT957.GetValue(i, "AHBLIGGB").ToString() == "합  계")
                {
                    this.FPS91_TY_S_AC_43OFT957.ActiveSheet.Rows[i].ForeColor = Color.Blue;
                    this.FPS91_TY_S_AC_43OFT957.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
                }
            }
        }
        #endregion

        #region Description : 채권채무 MAST 스프레드 이벤트
        private void FPS91_TY_S_AC_43OBZ956_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            UP_Set_Detail(this.FPS91_TY_S_AC_43OBZ956.GetValue("AOCRCUST").ToString());
        }
        #endregion

        #region Description : 내역 스프레드 이벤트
        private void FPS91_TY_S_AC_43OFT957_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column.ToString() == "5")
            {
                if (this.FPS91_TY_S_AC_43OFT957.GetValue("AHBLIGGB").ToString() == "채권계" ||
                    this.FPS91_TY_S_AC_43OFT957.GetValue("AHBLIGGB").ToString() == "채무계" ||
                    this.FPS91_TY_S_AC_43OFT957.GetValue("AHBLIGGB").ToString() == "합  계")
                {
                    this.ShowMessage("TY_M_MR_2BF8A365");
                }
                else
                {
                    string sB2DPMK = this.FPS91_TY_S_AC_43OFT957.GetValue("AHJPNO").ToString().Substring(0, 6);
                    string sB2DTMK = this.FPS91_TY_S_AC_43OFT957.GetValue("AHJPNO").ToString().Substring(6, 8);
                    string sB2NOSQ = this.FPS91_TY_S_AC_43OFT957.GetValue("AHJPNO").ToString().Substring(14, 3);

                    if (this.OpenModalPopup(new TYACBJ001I(sB2DPMK, sB2DTMK, sB2NOSQ)) == System.Windows.Forms.DialogResult.OK)
                    {
                    }
                }
            }
        }
        #endregion
    }
}