using System;
using System.Data;
using System.Windows.Forms;
using TY.Service.Library;

namespace TY.ER.GB99
{
    public partial class TYERGB995S : TYBase
    {
        private DataTable _dataSource;
        
        public TYERGB995S()
        {
            InitializeComponent();

            this._dataSource = new DataTable();
            this._dataSource.Columns.Add("CODE");
            this._dataSource.Columns.Add("CODE_NAME");
            this._dataSource.Columns.Add("PARENT_CODE");

            this._dataSource.Rows.Add("A", "A", "ROOT");
            this._dataSource.Rows.Add("AA", "AA", "A");
            this._dataSource.Rows.Add("AB", "AB", "A");
            this._dataSource.Rows.Add("AC", "AC", "A");
            this._dataSource.Rows.Add("B", "B", "ROOT");
            this._dataSource.Rows.Add("BA", "BA", "B");
            this._dataSource.Rows.Add("BB", "BB", "B");
            this._dataSource.Rows.Add("BC", "BC", "B");
        }

        private void TYERGB995S_Load(object sender, EventArgs e)
        {
            this.SetStartingFocus(this.TXT01_CODE);
            this.TRV01_MAIN.SetValue(new object[] {"루트", "ROOT", this._dataSource });
        }

        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            //this._dataSource.Rows.Add(this.TXT01_CODE.GetValue(), this.TXT01_CODE_NAME.GetValue(), "ROOT");
            //this.TRV01_MAIN.SetValue(new object[] { "루트", "ROOT", this._dataSource });
            this.TXT01_CODE.SetReadOnly(!this.TXT01_CODE.IsReadOnly);
        }

        private void TRV01_MAIN_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.ShowCustomMessage(this.TRV01_MAIN.SelectedNodeName + " " + this.TRV01_MAIN.SelectedNodeText + this.MTB01_YYYYMM.GetValue(), "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }



        #region 키 이벤트 순서
        /// <summary>
        /// 1번째
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TXT01_CODE_NAME_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //소용 없음
            if (e.KeyCode == Keys.Enter)
                this.SetFocus(this.TXT01_CODE);

        } 

        /// <summary>
        /// 2번째, 실행 후 프레임워크의 KeyDown을 다시 타므로 이벤트 효과 없음
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TXT01_CODE_NAME_KeyDown(object sender, KeyEventArgs e)
        {
            //소용 없음
            if (e.KeyCode == Keys.Enter)
                this.SetFocus(this.TXT01_CODE);
        }

        /// <summary>
        /// 3번째, 강제로 포커스 변경을 원할 경우 여기에 넣어야 함
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TXT01_CODE_NAME_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == '\r')
                this.SetFocus(this.TXT01_CODE);
        }
        #endregion
    }
}
