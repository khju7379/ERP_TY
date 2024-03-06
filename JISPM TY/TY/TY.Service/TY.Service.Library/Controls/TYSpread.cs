using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.Model;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library.Controls.TYSpreadCellType;

namespace TY.Service.Library.Controls
{
    public class TYSpread : TSpread
    {
        private const string DEFAULT_PATH = @"C:\Temp";
        private SaveFileDialog _SaveExcelDialog;
        private bool _autoResizeRate;
        private Dictionary<string, int> _colOriginalWidthes;
        private List<string> _colFixedWidthes;
        private Dictionary<string, ColCodeBoxInfo> _colCodeBoxInfos = null;
        private List<string> _defaultColSeq;
        private string _lastSortedColumn = string.Empty;
        private string _lastSortedDirection = string.Empty;

        internal Color NewRowColor = Color.FromArgb(209, 249, 185);//D1F9B9
        internal Color UpdateRowColor = Color.FromArgb(209, 236, 240);//D1ECF0
        internal Color DeleteRowColor = Color.FromArgb(254, 174, 152);//FEAE98

        #region struct ColCodeBoxInfo
        /// <summary>
        /// 코드헬퍼 사용 컬럼 정보
        /// </summary>
        public struct ColCodeBoxInfo
        {
            /// <summary>
            /// 코드헬퍼 사용 컬럼 정보
            /// </summary>
            /// <param name="applyCodeCol">코드 컬럼</param>
            /// <param name="applyNameCol">코드명 컬럼</param>
            /// <param name="codeBox">코드박스</param>
            public ColCodeBoxInfo(string applyCodeCol, string applyNameCol, TYCodeBox codeBox)
            {
                this.ApplyCodeCol = applyCodeCol;
                this.ApplyNameCol = applyNameCol;
                this.CodeBox = codeBox;
            }

            /// <summary>
            /// 코드 컬럼
            /// </summary>
            public string ApplyCodeCol;
            /// <summary>
            /// 코드명 컬럼
            /// </summary>
            public string ApplyNameCol;
            /// <summary>
            /// 코드박스
            /// </summary>
            public TYCodeBox CodeBox;
        }
        #endregion

        public TYSpread()
            : base()
        {
            this.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            //this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this._SaveExcelDialog = new SaveFileDialog();
            this._SaveExcelDialog.FileOk += new System.ComponentModel.CancelEventHandler(_SaveExcelDialog_FileOk);

            //this.EditModePermanent = true;

            this._autoResizeRate = true;
            this.AutoResize = false;
            this._colOriginalWidthes = new Dictionary<string, int>();
            this._colFixedWidthes = new List<string>();
            this.DataColumnConfigure += new DataColumnConfigureEventHandler(TYSpread_DataColumnConfigure);
            this.RowInserted += new TRowEventHandler(TYSpread_RowInserted);
            this.RowDeleted += new TRowEventHandler(TYSpread_RowDeleted);

            this._colCodeBoxInfos = new Dictionary<string, ColCodeBoxInfo>();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                e.SuppressKeyPress = false;
                return;
            }

            //전체선택
            if (e.Control && e.KeyCode == System.Windows.Forms.Keys.A &&
                this.ActiveSheet != null &&
                (this.ActiveSheet.OperationMode == OperationMode.ExtendedSelect || this.ActiveSheet.OperationMode == OperationMode.MultiSelect || this.ActiveSheet.OperationMode == OperationMode.Normal) &&
                (this.ActiveSheet.SelectionPolicy == SelectionPolicy.MultiRange || this.ActiveSheet.SelectionPolicy == SelectionPolicy.Range))
                this.ActiveSheet.AddSelection(0, 0, this.ActiveSheet.RowCount, this.ActiveSheet.ColumnCount);

            //코드박스형태 수정모드
            if (e.KeyCode == Keys.F1 &&
                this.ActiveSheet != null &&
                this.ActiveSheet.ActiveColumn != null &&
                (!this.ActiveSheet.ActiveColumn.Locked || this.ActiveSheet.RowHeader.Cells[this.ActiveSheet.ActiveRowIndex, 0].Text.Equals(TSpread.FLAG_N)) &&
                this._colCodeBoxInfos.ContainsKey(Convert.ToString(this.ActiveSheet.ActiveColumn.Tag)))
            {
                ColCodeBoxInfo tmpInfo = this._colCodeBoxInfos[Convert.ToString(this.ActiveSheet.ActiveColumn.Tag)];
                //tmpInfo.CodeBox.CodeText.Text = string.Empty;
                //tmpInfo.CodeBox.SetText(string.Empty);
                tmpInfo.CodeBox.SetValueText(
                    Convert.ToString(this.GetValue(this.ActiveSheet.ActiveRowIndex, tmpInfo.ApplyCodeCol)),
                    Convert.ToString(this.GetValue(this.ActiveSheet.ActiveRowIndex, tmpInfo.ApplyNameCol)));
                tmpInfo.CodeBox.ShowPopupHelper();
            }

            base.OnKeyDown(e);
        }

        protected override void OnChange(ChangeEventArgs e)
        {
            if (this._colCodeBoxInfos.ContainsKey(Convert.ToString(this.ActiveSheet.Columns[e.Column].Tag)))
            {
                ColCodeBoxInfo tmpInfo = this._colCodeBoxInfos[Convert.ToString(this.ActiveSheet.Columns[e.Column].Tag)];
                Cell cell = this.ActiveSheet.Cells[e.Row, e.Column];
                tmpInfo.CodeBox.SetText(string.Empty);
                tmpInfo.CodeBox.CodeText.Text = Convert.ToString(cell.Value);
                tmpInfo.CodeBox.SetValue(cell.Value ?? string.Empty);
                if (string.IsNullOrEmpty(tmpInfo.CodeBox.CodeText.Text) && !string.IsNullOrEmpty(tmpInfo.ApplyNameCol))
                    this.SetValue(e.Row, tmpInfo.ApplyNameCol, string.Empty);
            }
            else
            {
                foreach (ColCodeBoxInfo colCodeBoxInfo in this._colCodeBoxInfos.Values)
                {
                    if (colCodeBoxInfo.ApplyNameCol == Convert.ToString(this.ActiveSheet.Columns[e.Column].Tag))
                    {
                        Cell cell = this.ActiveSheet.Cells[e.Row, e.Column];
                        colCodeBoxInfo.CodeBox.Initialize();
                        colCodeBoxInfo.CodeBox.SetText(Convert.ToString(cell.Value));
                        colCodeBoxInfo.CodeBox.CodeTextValidationCheck();
                        if (!string.IsNullOrEmpty(colCodeBoxInfo.ApplyCodeCol))
                            this.SetValue(e.Row, colCodeBoxInfo.ApplyCodeCol, colCodeBoxInfo.CodeBox.GetValue());
                        if (!string.IsNullOrEmpty(colCodeBoxInfo.ApplyNameCol))
                            this.SetValue(e.Row, colCodeBoxInfo.ApplyNameCol, colCodeBoxInfo.CodeBox.GetText());
                    }
                }
            }

            if (!this.ActiveSheet.RowHeader.Cells[e.Row, 0].Text.Equals(FLAG_N) && !this.ActiveSheet.RowHeader.Cells[e.Row, 0].Text.Equals(FLAG_D))
                this.ActiveSheet.Rows[e.Row].BackColor = this.UpdateRowColor;

            base.OnChange(e);
        }

        protected override void OnCellClick(CellClickEventArgs e)
        {
            if (!e.ColumnHeader || !e.RowHeader)
                base.OnCellClick(e);
            else
            {

                base.OnCellClick(e);

                if (this.ActiveSheet.SheetCornerStyle.CellType is TCheckBoxCellType)
                {
                    bool rowSelected;
                    for (int i = 1; i < this.ActiveSheet.RowHeader.Rows.Count; i++)
                    {
                        rowSelected = false;
                        if (this.ActiveSheet.RowHeader.Cells[i, 0].Value is bool)
                            rowSelected = (bool)this.ActiveSheet.RowHeader.Cells[i, 0].Value;

                        this.ActiveSheet.RowHeader.Cells[i, 0].Value = !rowSelected;
                    }
                }
            }

            if (e.ColumnHeader && !e.RowHeader)
                if (this.ActiveSheet.Columns.Get(e.Column).AllowAutoSort)
                {
                    this._lastSortedColumn = Convert.ToString(this.ActiveSheet.Columns[e.Column].Tag);
                    this._lastSortedDirection =
                        (this.ActiveSheet.Columns[e.Column].SortIndicator == SortIndicator.Ascending ? "A" : "D");
                }
        }

        /// <summary>
        /// 비율로 자동 Resizing 여부
        /// </summary>
        [
        Localizable(true),
        DefaultValue(true)
        ]
        public bool AutoResizeRate
        {
            get { return this._autoResizeRate; }
            set
            {
                this._autoResizeRate = value;
                this.AutoResize = !this._autoResizeRate;
            }
        }

        [
        Localizable(true),
        DefaultValue(false)
        ]
        new private bool AutoResize
        {
            set { base.AutoResize = value; }
        }

        protected override void OnResize(EventArgs e)
        {
            this.AutoSpreadResize(this.ActiveSheet);
            base.OnResize(e);
        }

        private void TYSpread_DataColumnConfigure(object sender, DataColumnConfigureEventArgs e)
        {
            this.AutoSpreadResize(e.Sheet);
        }

        private void TYSpread_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.ActiveSheet.Rows[e.RowIndex, e.RowIndex + e.RowCount - 1].BackColor = this.NewRowColor;

            this.AutoSpreadResize(this.ActiveSheet);
        }

        private void TYSpread_RowDeleted(object sender, TSpread.TAlterEventRow e)
        {
            this.AutoSpreadResize(this.ActiveSheet);
        }

        /// <summary>
        /// 해당 SheetView를 비율로 Resizing
        /// </summary>
        /// <param name="view"></param>
        private void AutoSpreadResize(SheetView view)
        {
            if (!this.IsCreated || this.DesignMode || view == null || !this._autoResizeRate)
                return;

            this.SuspendLayout();

            if (view.ColumnCount < 100 && this.HorizontalScrollBarPolicy != ScrollBarPolicy.Always)
            {
                int totalWidth = 0;
                int spreadContentsWidth = this.Width;
                int totalSetingWidth = 0;

                foreach (Column col in view.Columns)
                {
                    if (!col.Visible)
                        continue;

                    if (!this._colOriginalWidthes.ContainsKey(Convert.ToString(col.Tag)))
                        this._colOriginalWidthes.Add(Convert.ToString(col.Tag), Convert.ToInt32(col.Width));
                }

                foreach (int width in this._colOriginalWidthes.Values)
                    totalWidth += width;

                if (this.ActiveSheet.RowHeaderVisible)
                    foreach (Column col in this.ActiveSheet.RowHeader.Columns)
                        spreadContentsWidth -= Convert.ToInt32(col.Width);

                if (this.VerticalScrollBarPolicy != ScrollBarPolicy.Never)
                {
                    int colHeadHeight = 0;
                    int totalHeight = 0;

                    if (this.ActiveSheet.RowHeaderVisible)
                        for (int i = 0; i < view.ColumnHeader.Rows.Count; i++)
                            colHeadHeight += view.Models.ColumnHeaderRowAxis.GetSize(i);

                    if (view.Rows.Count > 0)
                        totalHeight = colHeadHeight + (Convert.ToInt32(view.Rows[0].Height) * view.Rows.Count);

                    if (totalHeight + 4 > this.Height || this.VerticalScrollBarPolicy == ScrollBarPolicy.Always)
                        spreadContentsWidth -= (this.VerticalScrollBarWidth > 0) ? this.VerticalScrollBarWidth : 17;
                }

                if (this.BorderStyle == System.Windows.Forms.BorderStyle.Fixed3D)
                    spreadContentsWidth -= 3;

                foreach (Column col in view.Columns)
                {
                    if (!col.Visible)
                        continue;

                    if (this._colFixedWidthes.Contains(Convert.ToString(col.Tag)))
                    {
                        totalWidth -= this._colOriginalWidthes[Convert.ToString(col.Tag)];
                        spreadContentsWidth -= this._colOriginalWidthes[Convert.ToString(col.Tag)];
                    }
                }

                foreach (Column col in view.Columns)
                {
                    if (!col.Visible)
                        continue;

                    if (this._colFixedWidthes.Contains(Convert.ToString(col.Tag)))
                        col.Width = this._colOriginalWidthes[Convert.ToString(col.Tag)];
                    else
                    {
                        col.Width = this._colOriginalWidthes[Convert.ToString(col.Tag)] * spreadContentsWidth / totalWidth;
                        totalSetingWidth += this._colOriginalWidthes[Convert.ToString(col.Tag)] * spreadContentsWidth / totalWidth;
                    }
                }

                foreach (Column col in view.Columns)
                {
                    if (!col.Visible)
                        continue;

                    if (!this._colFixedWidthes.Contains(Convert.ToString(col.Tag)))
                    {
                        col.Width = col.Width + spreadContentsWidth - totalSetingWidth - 1;
                        break;
                    }
                }
            }

            this.ResumeLayout();
        }

        /// <summary>
        /// 컬럼 설정
        /// </summary>
        internal void SetTYColumns()
        {
            if (this.ActiveSheet == null)
                return;

            foreach (Column column in this.ActiveSheet.Columns)
            {
                if (column.CellType is TTextCellType)
                {
                    TTextCellType tTextCellType = (TTextCellType)column.CellType;
                    TYTextCellType tyTextCellType = new TYTextCellType();
                    tyTextCellType.StringTrim = StringTrimming.EllipsisCharacter;

                    tyTextCellType.CharacterSet = tTextCellType.CharacterSet;
                    tyTextCellType.CharacterCasing = tTextCellType.CharacterCasing;
                    tyTextCellType.MaxLength = tTextCellType.MaxLength;
                    tyTextCellType.TextOrientation = tTextCellType.TextOrientation;
                    tyTextCellType.PasswordChar = tTextCellType.PasswordChar;
                    tyTextCellType.StringTrim = tTextCellType.StringTrim;
                    tyTextCellType.ButtonAlign = tTextCellType.ButtonAlign;
                    tyTextCellType.Static = tTextCellType.Static;
                    tyTextCellType.WordWrap = tTextCellType.WordWrap;
                    tyTextCellType.DropDownButton = tTextCellType.DropDownButton;
                    tyTextCellType.ReadOnly = tTextCellType.ReadOnly;
                    tyTextCellType.Multiline = tTextCellType.Multiline;

                    column.CellType = tyTextCellType;
                }

                /***********컬럼헤더 Align 시작***********/
                if (column.Visible)
                    this.ActiveSheet.ColumnHeader.Columns[column.Index].HorizontalAlignment = column.HorizontalAlignment;
                /***********컬럼헤더 Align 끝***********/
            }

            bool showSaveColumnState = this.AllowColumnMove;
            this._defaultColSeq = new List<string>();
            foreach (Column col in this.ActiveSheet.Columns)
            {
                this._defaultColSeq.Add(Convert.ToString(col.Tag));

                if (!col.ShowSortIndicator)
                    continue;

                showSaveColumnState = true;
            }

            if (showSaveColumnState)
            {
                ToolStripMenuItem saveColumnState_Item = new ToolStripMenuItem("타이틀 저장");
                ToolStripMenuItem initColumnState_Item = new ToolStripMenuItem("타이틀 초기화");
                saveColumnState_Item.Click += new EventHandler(saveColumnState_Item_Click);
                initColumnState_Item.Click += new EventHandler(initColumnState_Item_Click);
                if (this.CurrentContextMenu.Items.Count > 0)
                    this.CurrentContextMenu.Items.Add(new ToolStripSeparator());
                this.CurrentContextMenu.Items.Add(saveColumnState_Item);
                this.CurrentContextMenu.Items.Add(initColumnState_Item);

                TYBase form = this.FindForm() as TYBase;
                if (form != null)
                {
                    string colStates = null;
                    try
                    {
                        colStates = RegisteryKey.GetValue(form.Name + "|" + this.Name);
                    }
                    catch { }
                    if (!string.IsNullOrEmpty(colStates) && colStates.Split('!').Length > 1)
                    {
                        string colSeq = colStates.Split('!')[0];
                        string colSort = colStates.Split('!')[1];
                        this.SetColSeq(new List<string>(colSeq.Split('|')));
                        if (colSort.Split('|').Length > 1)
                        {
                            this._lastSortedColumn = colSort.Split('|')[0];
                            this._lastSortedDirection = colSort.Split('|')[1];
                        }
                    }
                }
            }
        }

        private void SetColSeq(List<string> colSeq)
        {
            if (colSeq == null)
                return;

            Column col;
            string key;
            string tmpDataField;

            for (int i = 0; i < colSeq.Count; i++)
            {
                key = colSeq[i];

                for (int j = i; j < this.ActiveSheet.Columns.Count; j++)
                {
                    col = this.ActiveSheet.Columns[j];
                    if (key == Convert.ToString(col.Tag))
                    {
                        /* vs 2010 소스
                        tmpDataField = col.DataField;
                        this.ActiveSheet.MoveColumn(j, i, true);
                        this.ActiveSheet.Columns[i].DataField = tmpDataField;
                        break;
                        */

                        /* 스프레드 버전 변경으로 소스 수정 2022.05.10 */
                        tmpDataField = Convert.ToString(col.Tag);
                        this.ActiveSheet.MoveColumn(j, i, true);
                        this.ActiveSheet.Columns[i].DataField = tmpDataField;
                        this.ActiveSheet.Columns[i].Tag = tmpDataField;
                        break;
                    }
                }
            }
        }

        private void SetColSort(string colTag, string direction)
        {
            Column col;
            for (int i = 0; i < this.ActiveSheet.Columns.Count; i++)
            {
                col = this.ActiveSheet.Columns[i];

                if (!col.AllowAutoSort)
                    continue;

                this.ActiveSheet.SetColumnSortIndicator(i, SortIndicator.None);

                if (Convert.ToString(this.ActiveSheet.Columns[i].Tag) == colTag && !string.IsNullOrEmpty(colTag))
                    this.ActiveSheet.SortRows(i, direction == "A", true);
            }
        }

        private void initColumnState_Item_Click(object sender, EventArgs e)
        {
            this._lastSortedColumn = string.Empty;
            this._lastSortedDirection = string.Empty;

            this.SetColSeq(this._defaultColSeq);
            this.SetColSort(this._lastSortedColumn, this._lastSortedDirection);

            TYBase form = this.FindForm() as TYBase;
            if (form == null)
                return;

            try
            {
                if (!string.IsNullOrEmpty(RegisteryKey.GetValue(form.Name + "|" + this.Name)))
                    RegisteryKey.Remove(form.Name + "|" + this.Name);
            }
            catch { }
        }

        private void saveColumnState_Item_Click(object sender, EventArgs e)
        {
            TYBase form = this.FindForm() as TYBase;
            if (form == null)
                return;
            List<string> colSeq = new List<string>();
            foreach (Column col in this.ActiveSheet.Columns)
                colSeq.Add(Convert.ToString(col.Tag));

            try
            {
                RegisteryKey.SetValue(form.Name + "|" + this.Name,
                    string.Join("|", colSeq.ToArray()) + "!" +
                    this._lastSortedColumn + "|" + this._lastSortedDirection
                    );
            }
            catch { }
        }

        public override void ControlSetting()
        {
            if (!this.Option.ContainsKey("S02"))    //Selection BackColor
                this.Option.Add("S02", "252,236,187");  //FCECBB
            if (!this.Option.ContainsKey("S05"))    //Selection Style
                this.Option.Add("S05", "03");

            base.ControlSetting();

            if (this.ActiveSheet != null)
            {
                foreach (Column column in this.ActiveSheet.Columns)
                {
                    /***********컬럼헤더 Align 시작***********/
                    if (column.Visible)
                        this.ActiveSheet.ColumnHeader.Columns[column.Index].HorizontalAlignment = column.HorizontalAlignment;
                    /***********컬럼헤더 Align 끝***********/

                    if (!(column.CellType is TTextCellType))
                        continue;

                    TTextCellType tTextCellType = (TTextCellType)column.CellType;
                    TYTextCellType tyTextCellType = new TYTextCellType();
                    tyTextCellType.StringTrim = StringTrimming.EllipsisCharacter;

                    tyTextCellType.CharacterSet = tTextCellType.CharacterSet;
                    tyTextCellType.CharacterCasing = tTextCellType.CharacterCasing;
                    tyTextCellType.MaxLength = tTextCellType.MaxLength;
                    tyTextCellType.TextOrientation = tTextCellType.TextOrientation;
                    tyTextCellType.PasswordChar = tTextCellType.PasswordChar;
                    tyTextCellType.StringTrim = tTextCellType.StringTrim;
                    tyTextCellType.ButtonAlign = tTextCellType.ButtonAlign;
                    tyTextCellType.Static = tTextCellType.Static;
                    tyTextCellType.WordWrap = tTextCellType.WordWrap;
                    tyTextCellType.DropDownButton = tTextCellType.DropDownButton;
                    tyTextCellType.ReadOnly = tTextCellType.ReadOnly;
                    tyTextCellType.Multiline = tTextCellType.Multiline;

                    column.CellType = tyTextCellType;
                }

                //스프레드 버전 변경에 따른 추가 코드---시작
                this.PaintSelectionHeader = false;//선택 셀의 Col/Row 헤더 색상 표시여부
                //this.PaintSelectionBorder = false;
                this.VisualStyles = FarPoint.Win.VisualStyles.Off;
                //this.FocusRenderer = new FarPoint.Win.Spread.DefaultFocusIndicatorRenderer();
                this.Skin = FarPoint.Win.Spread.DefaultSpreadSkins.Classic;

                //-----ActiveSheet 스타일 설정-----
                //this.ActiveSheet.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Classic1;
                this.ActiveSheet.GrayAreaBackColor = System.Drawing.SystemColors.Menu;//시트 빈 공간 색상

                //스프레드 스크롤 타입 & 색상 지정 //

                FarPoint.Win.Spread.FlatScrollBarRenderer render = new FarPoint.Win.Spread.FlatScrollBarRenderer();

                //render.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(121)))), ((int)(((byte)(121)))));
                //render.BackColor = System.Drawing.SystemColors.ControlLight;
                //render.BorderActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
                //render.TrackBarBackColor = System.Drawing.SystemColors.ButtonFace;

                this.VerticalScrollBar.Renderer = render;
                this.HorizontalScrollBar.Renderer = render;

                //스프레드 버전 변경에 따른 추가 코드---종료

                FarPoint.Win.Spread.StyleInfo tmpStyle = new FarPoint.Win.Spread.StyleInfo();
                tmpStyle.BackColor = Color.FromArgb(229, 229, 229);     //E5E5E5
                //tmpStyle.ForeColor = Color.FromArgb(255, 255, 255);
                //tmpStyle.Font = new System.Drawing.Font("굴림", 9, FontStyle.Bold);

                this.ActiveSheet.ColumnHeader.DefaultStyle = tmpStyle;
                this.ActiveSheet.RowHeader.DefaultStyle = tmpStyle;
                this.ActiveSheet.SheetCornerStyle.BackColor = Color.FromArgb(229, 229, 229);     //E5E5E5
                if (this.ActiveSheet.AlternatingRows.Count > 0)
                    this.ActiveSheet.AlternatingRows[0].BackColor = Color.FromArgb(249, 249, 249);  //F9F9F9
            }

            string option = null;
            if (this.Option.TryGetValue("F19", out option))
            {
                ToolStripMenuItem delete_Item = new ToolStripMenuItem("행 삭제");
                ToolStripMenuItem cancel_Item = new ToolStripMenuItem("행 취소");
                ToolStripMenuItem viewer_Item = new ToolStripMenuItem("엑셀 바로보기");
                ToolStripMenuItem excel_Item = new ToolStripMenuItem("엑셀 내보내기");
                delete_Item.Click += new EventHandler(delete_Item_Click);
                cancel_Item.Click += new EventHandler(cancel_Item_Click);
                viewer_Item.Click += new EventHandler(viewer_Item_Click);
                excel_Item.Click += new EventHandler(excel_Item_Click);

                ToolStripItem tmpItem;
                for (int i = 0; i < this.CurrentContextMenu.Items.Count; i++)
                {
                    tmpItem = this.CurrentContextMenu.Items[i];
                    if (option.Substring(1, 1).Equals("Y") && tmpItem.Text == "행 삭제")
                    {
                        this.CurrentContextMenu.Items.Insert(this.CurrentContextMenu.Items.IndexOf(tmpItem), delete_Item);
                        this.CurrentContextMenu.Items.Remove(tmpItem);
                    }
                    if (option.Substring(2, 1).Equals("Y") && tmpItem.Text == "행 취소")
                    {
                        this.CurrentContextMenu.Items.Insert(this.CurrentContextMenu.Items.IndexOf(tmpItem), cancel_Item);
                        this.CurrentContextMenu.Items.Remove(tmpItem);
                    }
                    if (option.Substring(3, 1).Equals("Y") && tmpItem.Text == "행 선택")
                    {
                        TCheckBoxCellType tCheckBoxCellType = new TCheckBoxCellType("Y");
                        this.ActiveSheet.SheetCornerStyle.CellType = tCheckBoxCellType;
                    }
                    if (option.Substring(4, 1).Equals("Y") && tmpItem.Text == "엑셀 바로보기")
                    {
                        this.CurrentContextMenu.Items.Insert(this.CurrentContextMenu.Items.IndexOf(tmpItem), viewer_Item);
                        this.CurrentContextMenu.Items.Remove(tmpItem);
                    }
                    if (option.Substring(5, 1).Equals("Y") && tmpItem.Text == "엑셀 내보내기")
                    {
                        this.CurrentContextMenu.Items.Insert(this.CurrentContextMenu.Items.IndexOf(tmpItem), excel_Item);
                        this.CurrentContextMenu.Items.Remove(tmpItem);
                    }
                }
            }
        }

        new public void SetValue(object value)
        {
            base.SetValue(value);

            //this.SetColSort(this._lastSortedColumn, this._lastSortedDirection);
        }

        private void delete_Item_Click(object sender, EventArgs e)
        {
            ArrayList selections = this.GetSelectionList();
            if (selections.Count == 0)
            {
                MessageBox.Show("삭제할 행이 선택되지 않았습니다.");
                return;
            }

            for (int i = selections.Count - 1; i >= 0; i--)
            {
                int row = (int)selections[i];
                switch (this.ActiveSheet.RowHeader.Cells[row, 0].Text)
                {
                    case FLAG_N:
                        this.ActiveSheet.RemoveRows(row, 1);
                        this.AutoSpreadResize(this.ActiveSheet);
                        break;
                    default:
                        this.ActiveSheet.RowHeader.Cells[row, 0].Text = FLAG_D;
                        this.ActiveSheet.Rows[row].BackColor = this.DeleteRowColor;
                        break;
                }
            }
        }

        private void cancel_Item_Click(object sender, EventArgs e)
        {
            ArrayList selections = this.GetSelectionList();
            if (selections.Count == 0)
            {
                MessageBox.Show("취소할 행이 선택되지 않았습니다.");
                return;
            }

            for (int i = selections.Count - 1; i >= 0; i--)
            {
                int row = (int)selections[i];
                switch (this.Sheets[0].RowHeader.Cells[row, 0].Text)
                {
                    case FLAG_N:
                        this.Sheets[0].RemoveRows(row, 1);
                        this.AutoSpreadResize(this.ActiveSheet);
                        break;
                    case FLAG_U:
                        if (this.ActiveSheet.DataSource != null)
                            ((DataTable)this.Sheets[0].DataSource).Rows[row].RejectChanges();
                        this.ActiveSheet.RowHeader.Cells[row, 0].Text = (row + 1).ToString();
                        this.ActiveSheet.Rows[row].BackColor = Color.Empty;
                        break;
                    case FLAG_D:
                        this.ActiveSheet.RowHeader.Cells[row, 0].Text = (row + 1).ToString();
                        this.ActiveSheet.Rows[row].BackColor = Color.Empty;
                        break;
                    case FLAG_T:
                        this.Sheets[0].RowHeader.Cells[row, 0].Value = false;
                        break;
                }
            }
        }

        private ArrayList GetSelectionList()
        {
            CellRange[] selections = this.ActiveSheet.GetSelections();
            if (selections.Length == 0)
                selections = new CellRange[] { new CellRange(this.ActiveSheet.ActiveRowIndex, -1, 1, -1) };

            ArrayList selectedlist = new ArrayList();
            if (this.Sheets[0].Rows.Count == 0)
                return selectedlist;

            for (int i = 0; i < selections.Length; i++)
            {
                CellRange selected = selections[i];
                int start = selected.Row;
                int end = selected.Row + selected.RowCount;
                for (int j = start; j < end; j++)
                    selectedlist.Add(j);
            }

            selectedlist.Sort();
            return selectedlist;
        }

        #region 엑셀 Export 관련

        private void viewer_Item_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(DEFAULT_PATH))
                Directory.CreateDirectory(DEFAULT_PATH);

            string excelFile = String.Format(@"{0}\{1}.xls", DEFAULT_PATH, Guid.NewGuid());
            Thread.Sleep(this.SaveExcelAsHTML(excelFile) / 100000 + 500);

            Process.Start(excelFile);
        }

        private void excel_Item_Click(object sender, EventArgs e)
        {
            _SaveExcelDialog.Title = "엑셀 내보내기";
            _SaveExcelDialog.OverwritePrompt = true;
            _SaveExcelDialog.AddExtension = true;
            _SaveExcelDialog.CheckPathExists = true;
            _SaveExcelDialog.DefaultExt = "xls";
            _SaveExcelDialog.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";

            _SaveExcelDialog.ShowDialog();
        }

        private void _SaveExcelDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //this.SaveExcel(_SaveExcelDialog.FileName);
            Thread.Sleep(this.SaveExcelAsHTML(_SaveExcelDialog.FileName) / 100000 + 500);
            MessageBox.Show("스프레드 정보를 모두 엑셀 내보내었습니다.");
        }

        private int SaveExcelAsHTML(string fileName)
        {
            StringBuilder htmlContents = new StringBuilder();
            Cell[,] tmpCells = new Cell[this.ActiveSheet.ColumnHeader.Rows.Count, this.ActiveSheet.ColumnHeader.Columns.Count];

            //헤더
            for (int i = 0; i < this.ActiveSheet.ColumnHeader.Rows.Count; i++)
                for (int j = 0; j < this.ActiveSheet.ColumnHeader.Columns.Count; j++)
                    if (this.ActiveSheet.ColumnHeader.Rows[i].Visible && this.ActiveSheet.ColumnHeader.Columns[j].Visible)
                        tmpCells[i, j] = this.ActiveSheet.ColumnHeader.Cells[i, j];

            for (int i = 0; i < this.ActiveSheet.ColumnHeader.Rows.Count; i++)
                for (int j = 0; j < this.ActiveSheet.ColumnHeader.Columns.Count; j++)
                {
                    if (tmpCells[i, j] == null)
                        continue;

                    for (int k = 0; k < tmpCells[i, j].RowSpan; k++)
                        for (int m = 0; m < tmpCells[i, j].ColumnSpan; m++)
                        {
                            if (k + m == 0)
                                continue;

                            tmpCells[i + k, j + m] = null;
                        }
                }

            htmlContents.AppendLine("<table border=\"1\">");
            htmlContents.AppendLine("<thead>");
            for (int i = 0; i < this.ActiveSheet.ColumnHeader.Rows.Count; i++)
            {
                htmlContents.AppendLine(string.Format("\t<tr style=\"background-color:#{0:X}{1:X}{2:X}\">",
                    this.ActiveSheet.ColumnHeader.DefaultStyle.BackColor.R,
                    this.ActiveSheet.ColumnHeader.DefaultStyle.BackColor.G,
                    this.ActiveSheet.ColumnHeader.DefaultStyle.BackColor.B));

                for (int j = 0; j < this.ActiveSheet.ColumnHeader.Columns.Count; j++)
                {
                    if (tmpCells[i, j] == null)
                        continue;

                    htmlContents.AppendLine(string.Format("\t\t<td rowspan=\"{1}\" colspan=\"{2}\" style=\"text-align:center;\">{0}</td>", tmpCells[i, j].Value, tmpCells[i, j].RowSpan, tmpCells[i, j].ColumnSpan));
                }
                htmlContents.AppendLine("\t</tr>");
            }
            htmlContents.AppendLine("</thead>");

            //본문
            htmlContents.AppendLine("<tbody>");
            for (int i = 0; i < this.ActiveSheet.Rows.Count; i++)
            {
                htmlContents.AppendLine("\t<tr>");

                for (int j = 0; j < this.ActiveSheet.Columns.Count; j++)
                    if (this.ActiveSheet.Rows[i].Visible && this.ActiveSheet.Columns[j].Visible)
                        htmlContents.AppendLine(string.Format("\t\t<td>{0}</td>", this.ActiveSheet.Cells[i, j].Value));

                htmlContents.AppendLine("\t</tr>");
            }
            htmlContents.AppendLine("</tbody>");
            htmlContents.AppendLine("</table>");

            File.WriteAllText(fileName, htmlContents.ToString());

            return htmlContents.Length;
        }

        #endregion

        /// <summary>
        /// 비율로 Resizing 시 컬럼 폭 고정 여부 설정
        /// </summary>
        /// <param name="columnName">컬럼 명</param>
        /// <param name="isFixedWidth">컬럼 폭 고정 여부</param>
        public void SetFixedWidthColumnInAutoResizeRate(string columnName, bool isFixedWidth)
        {
            if (isFixedWidth && !this._colFixedWidthes.Contains(columnName))
                this._colFixedWidthes.Add(columnName);

            if (!isFixedWidth && this._colFixedWidthes.Contains(columnName))
                this._colFixedWidthes.Remove(columnName);
        }

        /// <summary>
        /// CodeBox 컬럼 정보
        /// </summary>
        public Dictionary<string, ColCodeBoxInfo> CodeBoxColumnInfos
        {
            get { return this._colCodeBoxInfos; }
        }

        #region Description: GetDataSource

        new public DataTable GetDataSource(TActionType action)
        {
            return this.GetReversedData(base.GetDataSource(action));
        }

        new public DataTable GetDataSource(TActionType action, params string[] excludeColumnNames)
        {
            return this.GetReversedData(base.GetDataSource(action, excludeColumnNames));
        }

        new public DataTable GetDataSourceInclude(TActionType action)
        {

            return this.GetReversedData(base.GetDataSourceInclude(action));
        }

        new public DataTable GetDataSourceInclude(TActionType action, params string[] includeColumnNames)
        {
            DataTable rtnValue = (DataTable)this.GetValue();

            if (rtnValue == null)
                rtnValue = new DataTable();
            else
                rtnValue = this.GetReversedData(base.GetDataSourceInclude(action, includeColumnNames));

            return rtnValue;
        }

        private DataTable GetReversedData(DataTable dataTable)
        {
            DataTable rtnValue = null;
            if (dataTable != null)
            {
                rtnValue = dataTable.Clone();
                for (int i = dataTable.Rows.Count - 1; i > -1; i--)
                {
                    rtnValue.Rows.Add(dataTable.Rows[i].ItemArray);
                }
            }
            return rtnValue;
        }

        #endregion
    }
}
