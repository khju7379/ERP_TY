using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;

namespace TY.Service.Library.Controls
{
    /// <summary>
    /// 태영 전용 체크콤보박스
    /// </summary>
    public class TYCheckComboBox : TYComboBox, IControlFactory
    {
        #region internal class CheckedListItem - CheckedListBox 바인딩에 사용할 Item 클래스
        /// <summary>
        /// CheckedListBox 바인딩에 사용할 Item 클래스
        /// </summary>
        internal class CheckedListItem
        {
            private string _text;
            private object _value;

            /// <summary>
            /// CheckedListBox 바인딩에 사용할 Item 클래스 인스턴스 초기화
            /// </summary>
            /// <param name="text">표시할 텍스트</param>
            /// <param name="value">값</param>
            public CheckedListItem(string text, object value)
            {
                this._text = text;
                this._value = value;
            }

            /// <summary>
            /// 표시할 텍스트
            /// </summary>
            public string Text
            {
                get { return this._text; }
                set { this._text = value; }
            }

            /// <summary>
            /// 값
            /// </summary>
            public object Value
            {
                get { return this._value; }
                set { this._value = value; }
            }

            /// <summary>
            /// 현재 CheckedListItem를 나타내는 System.String을 반환. CheckedListBox.Items에 Add 시 화면 표현 사용되므로 Text로 정의.
            /// </summary>
            /// <returns>현재 CheckedListItem를 나타내는 System.String</returns>
            public override string ToString()
            {
                return this._text;
            }
        } 
        #endregion

        /*******************************************
         * 0. 전역변수 설정
         *******************************************/
        private bool _isFormShown;
        private Form _form;
        private CheckedListBox _checkedListBox;
        private int _dropDownHeight;
        private int _dropDownWidth;
        private string _separator;
        private List<object> _selectedValues;

        /*******************************************
         * 1. 생성자
         *******************************************/
        public TYCheckComboBox()
            : base()
        {
            this._isFormShown = false;
            this._dropDownHeight = 0;
            this._dropDownWidth = 0;
            this._separator = ",";
            this._selectedValues = new List<object>();
            this.KeyDown += new KeyEventHandler(TYCheckComboBox_KeyDown);
        }

        /*******************************************
         * 1. 오버라이딩
         *******************************************/
        protected override void OnDropDown(EventArgs e)
        {
            if (this._form == null)
                this.CreateCheckedListForm();

            Rectangle rect = RectangleToScreen(this.ClientRectangle);
            this._form.Location = new Point(rect.X, rect.Y + this.Size.Height);
            this._isFormShown = true;
            this._form.Show();
            this._form.Activate();
            this.SetCheckedListItems();
        }

        protected override void OnDropDownClosed(EventArgs e)
        {
            if (e != null)
                return;

            this._isFormShown = false;
            this._form.Hide();
            base.OnDropDownClosed(e);
        }

        #region IControlFactory 재정의

        new public void ControlSetting()
        {
            base.ControlSetting();
            
            if (this.Option.ContainsKey("C66"))
                this.SetValue(this.Option["C66"]);
        }

        new public string FactoryID
        {
            get { return base.FactoryID; }
            set { base.FactoryID = value; }
        }

        new public string FactoryName
        {
            get { return base.FactoryName; }
            set { base.FactoryName = value; }
        }

        new public string FactoryOption
        {
            set { base.FactoryOption = value; }
        }

        new public void GenerateFactoryID()
        {
            base.GenerateFactoryID();
        }

        new public object GetValue()
        {
            return this.GetValueIncludeSingleQuot();
        }

        new public string GroupNo
        {
            get { return base.GroupNo; }
        }

        public override void Initialize()
        {
            string value = "";
            if (this.Option.ContainsKey("C14"))
                value = this.Option["C14"];

            if (this.Option.ContainsKey("C66"))
                value = this.Option["C66"];

            this.SetValue(value);
        }

        new public bool IsCreated
        {
            get { return base.IsCreated; }
        }

        new public bool IsEmpty
        {
            get { return base.IsEmpty; }
        }

        new public bool IsReadOnly
        {
            get { return base.IsReadOnly; }
        }

        new public OptionDictionary Option
        {
            get { return base.Option; }
        }

        new public void SetReadOnly(bool read)
        {
            base.SetReadOnly(read);
        }

        new public void SetValue(object value)
        {
            this._selectedValues.Clear();
            List<object> tmpValues = new List<object>();

            if (value is object[])
                tmpValues.AddRange((object[])value);
            else if (value is string)
                tmpValues.AddRange(((string)value).Split(new string[] { this._separator }, StringSplitOptions.None));
            else
                tmpValues.Add(value);

            foreach (object tmpValue in tmpValues)
                foreach (object tmpItem in this.Items)
                {
                    if (tmpItem is DataRowView)
                    {
                        if (!tmpValue.Equals(((DataRowView)tmpItem)[this.ValueMember]))
                            continue;
                    }
                    else
                    {
                        if (!tmpValue.Equals(tmpItem) && 
                            !Convert.ToString(tmpValue).Equals(Convert.ToString(tmpItem)))
                            continue;
                    }

                    this._selectedValues.Add(tmpValue);
                    break;
                }

            this.SetTextBySelectedValues();
        }

        #endregion

        /*******************************************
         * 2. 속성
         *******************************************/
        /// <summary>
        /// Value 간 구분자
        /// </summary>
        [Localizable(true), DefaultValue(",")]
        public string Separator
        {
            get { return this._separator; }
            set { this._separator = value; }
        }

        /*******************************************
         * 3. 메서드
         *******************************************/
        /// <summary>
        /// 선택된 값을 배열 형태로 반환
        /// </summary>
        /// <returns>선택된 값의 배열</returns>
        public object[] GetValueArray()
        {
            return this._selectedValues.ToArray();
        }

        /// <summary>
        /// 선택된 값 중 첫번째 값을 가져옴
        /// </summary>
        /// <returns>선택된 값 중 첫번째 값</returns>
        public object GetFirstValue()
        {
            return (this._selectedValues.Count > 0 ? this._selectedValues[0] : null);
        }

        /// <summary>
        /// 선택된 값 중 마지막 값을 가져옴
        /// </summary>
        /// <returns>선택된 값 중 마지막 값</returns>
        public object GetLastValue()
        {
            return (this._selectedValues.Count > 0 ? this._selectedValues[this._selectedValues.Count - 1] : null);
        }

        /// <summary>
        /// 선택된 값 중 첫번째 텍스트를 가져옴
        /// </summary>
        /// <returns>선택된 값 중 첫번째 텍스트</returns>
        public string GetFirstText()
        {
            return (this._selectedValues.Count > 0 ? this.GetTextByValue(this.GetFirstValue()) : null);
        }

        /// <summary>
        /// 선택된 값 중 마지막 텍스트를 가져옴
        /// </summary>
        /// <returns>선택된 값 중 마지막 텍스트</returns>
        public string GetLastText()
        {
            return (this._selectedValues.Count > 0 ? this.GetTextByValue(this.GetLastValue()) : null);
        }

        /// <summary>
        /// 선택된 값을 구분자로 구분한 string으로 반환, Single Quotation 제외
        /// </summary>
        /// <returns>선택된 값을 구분자로 구분한 string</returns>
        public string GetValueExceptSingleQuot()
        {
            List<string> rtnValue = new List<string>();
            foreach (object selectedValue in this._selectedValues)
                rtnValue.Add(Convert.ToString(selectedValue));

            return string.Join(this._separator, rtnValue.ToArray());
        }

        /// <summary>
        /// 선택된 값을 구분자로 구분한 string으로 반환, Single Quotation 포함
        /// </summary>
        /// <returns>선택된 값을 구분자로 구분한 string</returns>
        public string GetValueIncludeSingleQuot()
        {
            List<string> rtnValue = new List<string>();
            foreach (object selectedValue in this._selectedValues)
                rtnValue.Add("'" + Convert.ToString(selectedValue) + "'");

            return string.Join(this._separator, rtnValue.ToArray());
        }

        /*******************************************
         * 3. 이벤트 정의
         *******************************************/
        /// <summary>
        /// TYCheckComboBox KeyDown 이벤트
        /// </summary>
        private void TYCheckComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
                this.OnDropDown(null);

            e.Handled = true;
        }

        /// <summary>
        /// CheckedListBox KeyDown 이벤트
        /// </summary>
        private void _checkedListBox_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Escape)
                this.CloseForm();
            else if (e.KeyCode == Keys.Enter)
            {
                this.SetSeletedValueByChekedList();
                this.SetTextBySelectedValues();
                this.CloseForm();
            }
        }

        /// <summary>
        /// Form(this._form) Deactivate 이벤트
        /// </summary>
        private void _form_Deactivate(object sender, EventArgs e)
        {
            if (!this._isFormShown)
                return;

            this.SetSeletedValueByChekedList();
            this.SetTextBySelectedValues();
            this.CloseForm();
        }

        /*******************************************
         * 4. private 함수
         *******************************************/
        /// <summary>
        /// CheckedListBox가 있는 폼 생성
        /// </summary>
        private void CreateCheckedListForm()
        {
            this._form = new Form();
            this._checkedListBox = new CheckedListBox();
            this._dropDownHeight = (this.DropDownHeight > 0 ? this.DropDownHeight : this._dropDownHeight);
            this._dropDownWidth = (this.DropDownWidth > 0 ? this.DropDownWidth : this._dropDownWidth);
            this.DropDownHeight = 1;
            this.DropDownWidth = 1;

            this._form.SuspendLayout();

            this._checkedListBox.BorderStyle = BorderStyle.None;
            this._checkedListBox.Dock = DockStyle.Fill;
            this._checkedListBox.FormattingEnabled = true;
            this._checkedListBox.Location = new Point(0, 0);
            this._checkedListBox.Size = new Size(this._dropDownWidth, this._dropDownHeight);
            this._checkedListBox.TabIndex = 0;
            this._checkedListBox.CheckOnClick = true;
            this._checkedListBox.HorizontalScrollbar = false;
            this._checkedListBox.KeyDown += new KeyEventHandler(_checkedListBox_KeyDown);

            this._form.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this._form.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this._form.BackColor = SystemColors.Menu;
            this._form.ClientSize = new Size(this._dropDownWidth, this._dropDownHeight);
            this._form.ControlBox = false;
            this._form.Controls.Add(this._checkedListBox);
            this._form.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this._form.MinimizeBox = false;
            this._form.ShowInTaskbar = false;
            this._form.StartPosition = FormStartPosition.Manual;
            this._form.Deactivate += new EventHandler(_form_Deactivate);

            this._form.ResumeLayout(false);
        }

        /// <summary>
        /// CheckedListBox Item 설정(체크 여부 포함)
        /// </summary>
        private void SetCheckedListItems()
        {
            if (this._checkedListBox == null)
                return;

            this._checkedListBox.Items.Clear();
            this._form.Activate();
            this._form.ActiveControl = this._checkedListBox;
            this._checkedListBox.Focus();

            if (this.Items.Count == 0)
                return;

            CheckedListItem checkedListItem;
            foreach (object item in this.Items)
            {
                if (item is DataRowView)
                    checkedListItem = new CheckedListItem(Convert.ToString(((DataRowView)item)[this.DisplayMember]), ((DataRowView)item)[this.ValueMember]);
                else
                    checkedListItem = new CheckedListItem(Convert.ToString(item), Convert.ToString(item));
                this._checkedListBox.Items.Add(checkedListItem);
                if (this._selectedValues.Contains(checkedListItem.Value))
                    this._checkedListBox.SetItemChecked(this._checkedListBox.Items.Count - 1, true);
                else
                    foreach (object selectedValue in this._selectedValues)
                    {
                        if (!selectedValue.Equals(checkedListItem.Value))
                            continue;
                        else
                            this._checkedListBox.SetItemChecked(this._checkedListBox.Items.Count - 1, true);
                    }
            }
        }

        /// <summary>
        /// ChekedListBox(this._checkedListBox)의 CheckedItems로 SelectedValues(this._selectedValues) 설정
        /// </summary>
        private void SetSeletedValueByChekedList()
        {
            this._selectedValues.Clear();
            foreach (CheckedListItem item in this._checkedListBox.CheckedItems)
                this._selectedValues.Add(item.Value);
        }

        /// <summary>
        /// SelectedValues(this._selectedValues)로 Text 설정
        /// </summary>
        private void SetTextBySelectedValues()
        {
            string selectedText;
            List<string> selectedTexts = new List<string>();

            foreach (object selectedValue in this._selectedValues)
            {
                selectedText = this.GetTextByValue(selectedValue);
                if(!string.IsNullOrEmpty(selectedText))
                    selectedTexts.Add(selectedText);
            }

            this.Text = string.Join(this._separator, selectedTexts.ToArray());
            this.OnSelectedValueChanged(new EventArgs());
        }

        private string GetTextByValue(object value)
        {
            string rtnValue = null;
            string itemText;
            object itemValue;

            foreach (object item in this.Items)
            {
                if (item is DataRowView)
                {
                    itemText = Convert.ToString(((DataRowView)item)[this.DisplayMember]);
                    itemValue = ((DataRowView)item)[this.ValueMember];
                }
                else
                {
                    itemText = Convert.ToString(item);
                    itemValue = Convert.ToString(item);
                }

                if (!value.Equals(itemValue))
                    continue;

                rtnValue = itemText;
                break;
            }

            return rtnValue;
        }

        /// <summary>
        /// Form(this._form) 닫기
        /// </summary>
        private void CloseForm()
        {
            this._isFormShown = false;
            this._form.Hide();
            this.OnDropDownClosed(null);
        }
    }
}
