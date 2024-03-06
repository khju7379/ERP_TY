using System;
using System.Data;
using System.Drawing;
using FarPoint.Win.Spread.CellType;
using TY.Service.Library;

namespace TY.ER.GB99
{
    public partial class TYERGB996S : TYBase
    {
        public TYERGB996S()
        {
            InitializeComponent();
        }

        private void TYERGB996S_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("A");
            dt.Columns.Add("B");
            dt.Columns.Add("C");
            dt.Rows.Add("1", "20120206", "국내법인카드");
            dt.Rows.Add("", "6108110449", "우리BC");
            dt.Rows.Add("", "거래처축하난발송", "");
            dt.Rows.Add("2", "201200220", "국내법인카드");
            dt.Rows.Add("", "6108500459", "외환VISA");
            dt.Rows.Add("", "거래처접대비", "");
            dt.Rows.Add("3", "20120224", "국내법인카드");
            dt.Rows.Add("", "0", "외환VISA");
            dt.Rows.Add("", "거래처접대비", "");
            this.FPS91_TY24U1P923.SetValue(dt);

            //this.FPS91_TY24U1P923.ActiveSheet
            this.FPS91_TY24U1P923_Sheet1.AddColumnHeaderSpanCell(0, 0, 3, 1);
            this.FPS91_TY24U1P923_Sheet1.AddColumnHeaderSpanCell(2, 1, 1, 2);
            this.FPS91_TY24U1P923_Sheet1.ColumnHeader.Cells[0, 0].Value = "일련번호";
            this.FPS91_TY24U1P923_Sheet1.ColumnHeader.Cells[0, 1].Value = "발행일";
            this.FPS91_TY24U1P923_Sheet1.ColumnHeader.Cells[0, 2].Value = "거래구분 <사용처>";
            this.FPS91_TY24U1P923_Sheet1.ColumnHeader.Cells[1, 1].Value = "사용자번호";
            this.FPS91_TY24U1P923_Sheet1.ColumnHeader.Cells[1, 2].Value = "카드명";
            this.FPS91_TY24U1P923_Sheet1.ColumnHeader.Cells[2, 1].Value = "사용자 내역";
            for (int i = 1; i < this.FPS91_TY24U1P923_Sheet1.ColumnHeader.RowCount; i++)
                this.FPS91_TY24U1P923_Sheet1.ColumnHeader.Rows[i].Height = this.FPS91_TY24U1P923_Sheet1.ColumnHeader.Rows[0].Height;
            
            for (int i = 0; i < this.FPS91_TY24U1P923_Sheet1.RowCount; i++)
            {
                if (i % 3 == 0)
                {
                    this.FPS91_TY24U1P923_Sheet1.AddRowHeaderSpanCell(i, 0, 3, 1);
                    this.FPS91_TY24U1P923_Sheet1.AddSpanCell(i, 0, 3, 1);
                }
                else if (i % 3 == 1)
                {
                    //TNumberCellType tmpCellType = new TNumberCellType(2, 99999999999999, -99999999999999);
                    //tmpCellType.ShowSeparator = true;
                    //tmpCellType.LeadingZero = FarPoint.Win.Spread.CellType.LeadingZero.No;
                    GeneralCellType tmpCellType = new GeneralCellType();
                    tmpCellType.FormatString = "#,###.##";
                    this.FPS91_TY24U1P923_Sheet1.Cells[i, 1].CellType = tmpCellType;
                    this.FPS91_TY24U1P923_Sheet1.Cells[i, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                }
                else if (i % 3 == 2)
                    this.FPS91_TY24U1P923_Sheet1.AddSpanCell(i, 1, 1, 2);
            }

            if (this.FPS91_TY24U1P923_Sheet1.AlternatingRows.Count > 0)
                this.FPS91_TY24U1P923_Sheet1.AlternatingRows[0].BackColor = Color.White;
        }
    }
}
