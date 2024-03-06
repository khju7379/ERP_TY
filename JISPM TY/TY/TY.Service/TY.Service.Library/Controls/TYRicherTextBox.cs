using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using System.Text;
using System.Text.RegularExpressions;
using Shoveling2010.SmartClient.SystemUtility.Controls;

namespace TY.Service.Library.Controls
{
    /// <summary>
    /// CodeProject의 RicherTextBox를 정리하여 IControlFactory를 구현한 클래스입니다.
    /// 
    /// 작성자 : 김영우
    /// 작성일 : 2012년 05월 31일
    /// </summary>
    public class TYRicherTextBox : UserControl, IControlFactory
    {
        public const string PREFIX = "RTB";

        private bool _IsCreated;
        private string _GroupNo;
        private string _FactoryID;
        private string _FactoryName;
        private OptionDictionary _OptionDictionary;
        private string _RegexPattern;
        private int _MinLength;

        public TYRicherTextBox()
            : base()
        {
            InitializeComponent();

            _IsCreated = false;
            _GroupNo = "";
            _FactoryID = "";
            _RegexPattern = "";
            _MinLength = 0;
            _OptionDictionary = new OptionDictionary();
        }

        [DefaultValue(0)]
        public int MinLength
        {
            get { return _MinLength; }
            set { _MinLength = value; }
        }

        public int ByteLength
        {
            get { return Encoding.Default.GetByteCount(this.GetValue().ToString()); }
        }

        /*******************************************
         * 2. IControlFactory 관련 정의
         *******************************************/
        #region Description: IControlFactory 멤버

        public bool IsCreated
        {
            get { return _IsCreated; }
        }

        public bool IsEmpty
        {
            get { return this.ByteLength > 0 ? false : true; }
        }

        public bool IsReadOnly
        {
            get { return (!this.Enabled || this.ReadOnly) ? true : false; }
        }

        public string FactoryID
        {
            get { return _FactoryID; }
            set { _FactoryID = value; }
        }

        public string FactoryName
        {
            get { return _FactoryName; }
            set { _FactoryName = value; }
        }

        public string FactoryOption
        {
            set { _OptionDictionary = StaticCommon.CreateOptionDictionary(value); }
        }

        public string GroupNo
        {
            get { return _GroupNo; }
        }

        public OptionDictionary Option
        {
            get { return _OptionDictionary; }
        }

        public void GenerateFactoryID()
        {
            if (_FactoryID.Length == 0)
                StaticCommon.ControlNamingRuleCheck(this.Name, PREFIX, ref _FactoryID, ref _GroupNo);
        }

        public object GetValue()
        {
            return this.Text.Trim();
        }

        public void SetValue(object value)
        {
            this.Text = value.ToString();
        }

        public void SetReadOnly(bool read)
        {
            this.ReadOnly = read;
        }

        public void ControlSetting()
        {
            if (_IsCreated) return;

            this.rtbDocument.KeyPress += new KeyPressEventHandler(TTextBox_KeyPress);
            this.rtbDocument.KeyDown += new KeyEventHandler(TTextBox_KeyDown);
            this.rtbDocument.Leave += new EventHandler(TTextBox_Leave);
            this.rtbDocument.Enter += new System.EventHandler(this.rtbDocument_Enter);
            this.rtbDocument.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtbDocument_KeyDown);

            if (_OptionDictionary.Count > 0)
            {
                StaticCommon.ControlDefaultSetting(this, _OptionDictionary);

                if (_OptionDictionary.ContainsKey("C01")) StaticCommon.SetFromColor(this.BackColor, _OptionDictionary["C01"]);
                if (_OptionDictionary.ContainsKey("C02")) StaticCommon.SetFromColor(this.ForeColor, _OptionDictionary["C02"]);
                if (_OptionDictionary.ContainsKey("C03")) StaticCommon.SetFromPoint(this.Location, _OptionDictionary["C03"]);
                if (_OptionDictionary.ContainsKey("C04")) StaticCommon.SetFromSize(this.Size, _OptionDictionary["C04"]);
                if (_OptionDictionary.ContainsKey("C05")) this.Visible = StaticCommon.GetFromBool(_OptionDictionary["C05"]);
                if (_OptionDictionary.ContainsKey("C06")) this.Enabled = StaticCommon.GetFromBool(_OptionDictionary["C06"]);

                if (_OptionDictionary.ContainsKey("C21"))
                {
                    this.ImeMode = ImeMode.Disable;
                    switch (_OptionDictionary["C21"])
                    {
                        case "02": // AlphabetOnly
                            _RegexPattern = @"[a-zA-Z0-9\b]";
                            break;
                        case "03": // Hangul
                            this.ImeMode = ImeMode.Hangul;
                            break;
                        case "04": // Number
                            _RegexPattern = @"[0-9]";
                            break;
                        case "05": // Number2
                            _RegexPattern = @"[0-9\.]";
                            break;
                        case "06": // Regular
                            if (_OptionDictionary.ContainsKey("C29"))
                                _RegexPattern = _OptionDictionary["C29"];
                            break;
                    }
                }

                if (_OptionDictionary.ContainsKey("C12"))
                {
                    int mod = int.Parse(_OptionDictionary["C12"].ToString()) % 3;
                    if (mod == 0) mod = 3;
                }
                if (_OptionDictionary.ContainsKey("C23")) this.MaxLength = StaticCommon.GetFromInt(_OptionDictionary["C23"]);
                if (_OptionDictionary.ContainsKey("C24")) this.MinLength = StaticCommon.GetFromInt(_OptionDictionary["C24"]);
                if (_OptionDictionary.ContainsKey("C27")) this.ReadOnly = StaticCommon.GetFromBool(_OptionDictionary["C27"]);
                if (_OptionDictionary.ContainsKey("C28")) this.Multiline = StaticCommon.GetFromBool(_OptionDictionary["C28"]);
                if (_OptionDictionary.ContainsKey("C13"))
                    _FactoryName = _OptionDictionary["C13"];

                if (this.Multiline)
                {
                    this.ScrollBars = RichTextBoxScrollBars.Vertical;
                    this.AcceptsTab = true;
                }
            }

            _IsCreated = true;
        }

        public void Initialize()
        {
            this.SetValue("");
            this.Rtf = "";
        }

        #endregion

        #region Description: TTextBox events

        private void TTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!_OptionDictionary.ContainsKey("C21") ||
                 _OptionDictionary["C21"].Equals("03")) return;

            Regex regex = new Regex(_RegexPattern);
            string charText = e.KeyChar.ToString();
            if (_OptionDictionary.ContainsKey("C22"))
                switch (_OptionDictionary["C22"])
                {
                    case "01": e.KeyChar = charText.ToLower()[0]; break;
                    case "02": e.KeyChar = charText.ToUpper()[0]; break;
                }

            if (e.KeyChar == (char)1 ||
                e.KeyChar == (char)3 ||
                e.KeyChar == (char)22) { }
            else
            {
                if (regex.IsMatch(charText))
                    if (_OptionDictionary["C21"].Equals("05") ||
                        _OptionDictionary["C21"].Equals("08"))
                        if (charText.Equals("."))
                        {
                            if (this.GetValue().ToString().IndexOf('.') > -1)
                                e.KeyChar = ""[0];
                        }
            }
        }

        private void TTextBox_Leave(object sender, EventArgs e)
        {
            string inputText = this.GetValue().ToString();
            if (inputText.Length > 0)
            {
                int byteLength = Encoding.Default.GetByteCount(inputText);
                if (this.MinLength > byteLength)
                {
                    MessageBox.Show(String.Format("'{0}' 항목은 {1} 바이트 이상 입력되어야 합니다.", _FactoryName, this.MinLength));
                    this.Focus();
                    return;
                }

                if (this.MaxLength < byteLength && this.MaxLength > 0)
                {
                    MessageBox.Show(String.Format("'{0}' 항목은 {1} 바이트 이상 입력할 수 없습니다.", _FactoryName, this.MaxLength));
                    this.Focus();
                    return;
                }
            }
        }

        private void TTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                this.SelectAll();
        }

        #endregion

        #region Description: Redefine Properties

        public CustomImeModeType CustomImeMode
        {
            set
            {
                if (_OptionDictionary.ContainsKey("C21"))
                    _OptionDictionary.Remove("C21");

                _OptionDictionary.Add("C21",
                    ((int)value).ToString().PadLeft(2, '0'));

                this.ImeMode = ImeMode.Disable;
                if (_OptionDictionary["C21"].Equals("04") ||
                    _OptionDictionary["C21"].Equals("05") ||
                    _OptionDictionary["C21"].Equals("07") ||
                    _OptionDictionary["C21"].Equals("08"))
                {
                    _RegexPattern = @"[0-9\b]";

                    if (_OptionDictionary["C21"].Equals("05") ||
                        _OptionDictionary["C21"].Equals("08"))
                        _RegexPattern = @"[0-9\.\b]";
                }
                else
                    switch (_OptionDictionary["C21"])
                    {
                        case "02": _RegexPattern = @"[a-zA-Z0-9\b]"; break;
                        case "03": this.ImeMode = ImeMode.Hangul; break;
                    }
            }
        }

        public CustomCharacterCasingType CustomCharacterCasing
        {
            set
            {
                if (_OptionDictionary.ContainsKey("C22"))
                    _OptionDictionary.Remove("C22");

                _OptionDictionary.Add("C22",
                    ((int)value).ToString().PadLeft(2, '0'));
            }
        }

        public string CustomRegexPattern
        {
            set
            {
                _RegexPattern = value;
            }
        }

        #endregion

        /*******************************************
         * 3. TRichTextBox - RicherTextBox 연결용 멤버 정의
         *******************************************/
        [DefaultValue(false)]
        public bool ReadOnly
        {
            get { return this.rtbDocument.ReadOnly; }
            set
            {
                this.rtbDocument.ReadOnly = value;
                this.tsbtnOpen.Enabled = !value;
                this.tscmbFont.Enabled = !value;
                this.tscmbFontSize.Enabled = !value;
                this.tsbtnChooseFont.Enabled = !value;
                this.tsbtnBold.Enabled = !value;
                this.tsbtnItalic.Enabled = !value;
                this.tsbtnUnderline.Enabled = !value;
                this.tsbtnAlignLeft.Enabled = !value;
                this.tsbtnAlignCenter.Enabled = !value;
                this.tsbtnAlignRight.Enabled = !value;
                this.tsbtnFontColor.Enabled = !value;
                this.tsbtnWordWrap.Enabled = !value;
                this.tsbtnIndent.Enabled = !value;
                this.tsbtnOutdent.Enabled = !value;
                this.tsbtnBullets.Enabled = !value;
                this.tsbtnInsertPicture.Enabled = !value;
                this.tsbtnReplace.Enabled = !value;
            }
        }

        [DefaultValue(2147483647)]
        public int MaxLength
        {
            get { return this.rtbDocument.MaxLength; }
            set { this.rtbDocument.MaxLength = value; }
        }

        [DefaultValue(true)]
        public bool Multiline
        {
            get { return this.rtbDocument.Multiline; }
            set { this.rtbDocument.Multiline = value; }
        }

        [DefaultValue(RichTextBoxScrollBars.Both)]
        public RichTextBoxScrollBars ScrollBars
        {
            get { return this.rtbDocument.ScrollBars; }
            set { this.rtbDocument.ScrollBars = value; }
        }

        [DefaultValue(true)]
        public bool AcceptsTab
        {
            get { return this.rtbDocument.AcceptsTab; }
            set { this.rtbDocument.AcceptsTab = value; }
        }

        public void SelectAll()
        {
            this.rtbDocument.SelectAll();
        }

        private void rtbDocument_Enter(object sender, EventArgs e)
        {
            base.OnEnter(e);
        }

        private void rtbDocument_KeyDown(object sender, KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            this.rtbDocument.BackColor = this.BackColor;
        }

        /*******************************************
         * z. RicherTextBox 클래스 정의
         *******************************************/
        #region enum RicherTextBoxToolStripGroups
        public enum RicherTextBoxToolStripGroups
        {
            SaveAndLoad = 0x1,
            FontNameAndSize = 0x2,
            BoldUnderlineItalic = 0x4,
            Alignment = 0x8,
            FontColor = 0x10,
            IndentationAndBullets = 0x20,
            Insert = 0x40,
            Zoom = 0x80
        }
        #endregion

        #region class FindForm
        private class FindForm : Form
        {
            int lastFound = 0;
            RichTextBox rtbInstance = null;
            public RichTextBox RtbInstance
            {
                set { rtbInstance = value; }
                get { return rtbInstance; }
            }

            public string InitialText
            {
                set { txtSearchText.Text = value; }
                get { return txtSearchText.Text; }
            }

            public FindForm()
            {
                InitializeComponent();
                this.TopMost = true;
            }

            void rtbInstance_SelectionChanged(object sender, EventArgs e)
            {
                lastFound = rtbInstance.SelectionStart;
            }

            private void btnDone_Click(object sender, EventArgs e)
            {
                this.rtbInstance.SelectionChanged -= rtbInstance_SelectionChanged;
                this.Close();
            }

            private void btnFindNext_Click(object sender, EventArgs e)
            {
                RichTextBoxFinds options = RichTextBoxFinds.None;
                if (chkMatchCase.Checked) options |= RichTextBoxFinds.MatchCase;
                if (chkWholeWord.Checked) options |= RichTextBoxFinds.WholeWord;

                int index = rtbInstance.Find(txtSearchText.Text, lastFound, options);
                lastFound += txtSearchText.Text.Length;
                if (index >= 0)
                {
                    rtbInstance.Parent.Focus();
                }
                else
                {
                    MessageBox.Show("Search string not found", "Find", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lastFound = 0;
                }

            }

            private void FindForm_Load(object sender, EventArgs e)
            {
                if (rtbInstance != null)
                    this.rtbInstance.SelectionChanged += new EventHandler(rtbInstance_SelectionChanged);
            }

            /// <summary>
            /// Required designer variable.
            /// </summary>
            private System.ComponentModel.IContainer components = null;

            /// <summary>
            /// Clean up any resources being used.
            /// </summary>
            /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
            protected override void Dispose(bool disposing)
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }

            #region Windows Form Designer generated code

            /// <summary>
            /// Required method for Designer support - do not modify
            /// the contents of this method with the code editor.
            /// </summary>
            private void InitializeComponent()
            {
                this.label1 = new System.Windows.Forms.Label();
                this.txtSearchText = new System.Windows.Forms.TextBox();
                this.btnFindNext = new System.Windows.Forms.Button();
                this.btnDone = new System.Windows.Forms.Button();
                this.chkMatchCase = new System.Windows.Forms.CheckBox();
                this.chkWholeWord = new System.Windows.Forms.CheckBox();
                this.SuspendLayout();
                // 
                // label1
                // 
                this.label1.AutoSize = true;
                this.label1.Location = new System.Drawing.Point(12, 16);
                this.label1.Name = "label1";
                this.label1.Size = new System.Drawing.Size(68, 13);
                this.label1.TabIndex = 0;
                this.label1.Text = "Search Text:";
                // 
                // txtSearchText
                // 
                this.txtSearchText.Location = new System.Drawing.Point(87, 13);
                this.txtSearchText.Name = "txtSearchText";
                this.txtSearchText.Size = new System.Drawing.Size(166, 20);
                this.txtSearchText.TabIndex = 1;
                // 
                // btnFindNext
                // 
                this.btnFindNext.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.btnFindNext.Location = new System.Drawing.Point(178, 39);
                this.btnFindNext.Name = "btnFindNext";
                this.btnFindNext.Size = new System.Drawing.Size(75, 23);
                this.btnFindNext.TabIndex = 4;
                this.btnFindNext.Text = "&Find Next";
                this.btnFindNext.UseVisualStyleBackColor = true;
                this.btnFindNext.Click += new System.EventHandler(this.btnFindNext_Click);
                // 
                // btnDone
                // 
                this.btnDone.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.btnDone.Location = new System.Drawing.Point(178, 68);
                this.btnDone.Name = "btnDone";
                this.btnDone.Size = new System.Drawing.Size(75, 23);
                this.btnDone.TabIndex = 5;
                this.btnDone.Text = "Close";
                this.btnDone.UseVisualStyleBackColor = true;
                this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
                // 
                // chkMatchCase
                // 
                this.chkMatchCase.AutoSize = true;
                this.chkMatchCase.Location = new System.Drawing.Point(15, 43);
                this.chkMatchCase.Name = "chkMatchCase";
                this.chkMatchCase.Size = new System.Drawing.Size(82, 17);
                this.chkMatchCase.TabIndex = 2;
                this.chkMatchCase.Text = "Match case";
                this.chkMatchCase.UseVisualStyleBackColor = true;
                // 
                // chkWholeWord
                // 
                this.chkWholeWord.AutoSize = true;
                this.chkWholeWord.Location = new System.Drawing.Point(15, 66);
                this.chkWholeWord.Name = "chkWholeWord";
                this.chkWholeWord.Size = new System.Drawing.Size(86, 17);
                this.chkWholeWord.TabIndex = 3;
                this.chkWholeWord.Text = "Whole Word";
                this.chkWholeWord.UseVisualStyleBackColor = true;
                // 
                // FindForm
                // 
                this.AcceptButton = this.btnFindNext;
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.CancelButton = this.btnDone;
                this.ClientSize = new System.Drawing.Size(274, 114);
                this.Controls.Add(this.chkWholeWord);
                this.Controls.Add(this.chkMatchCase);
                this.Controls.Add(this.btnDone);
                this.Controls.Add(this.btnFindNext);
                this.Controls.Add(this.txtSearchText);
                this.Controls.Add(this.label1);
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.Name = "FindForm";
                this.ShowIcon = false;
                this.ShowInTaskbar = false;
                this.Text = "Find";
                this.Load += new System.EventHandler(this.FindForm_Load);
                this.ResumeLayout(false);
                this.PerformLayout();

            }

            #endregion

            private System.Windows.Forms.Label label1;
            private System.Windows.Forms.TextBox txtSearchText;
            private System.Windows.Forms.Button btnFindNext;
            private System.Windows.Forms.Button btnDone;
            private System.Windows.Forms.CheckBox chkMatchCase;
            private System.Windows.Forms.CheckBox chkWholeWord;
        }
        #endregion

        #region class ReplaceForm
        private class ReplaceForm : FindForm
        {
            public new RichTextBox RtbInstance
            {
                set
                {
                    base.RtbInstance = value;
                }
                get
                {
                    return base.RtbInstance;
                }
            }

            public new string InitialText
            {
                set
                {
                    base.InitialText = value;
                }
            }


            public ReplaceForm()
            {
                InitializeComponent();
            }

            private void btnReplace_Click(object sender, EventArgs e)
            {
                if (RtbInstance.SelectionLength > 0)
                {
                    int start = RtbInstance.SelectionStart;
                    int len = RtbInstance.SelectionLength;
                    RtbInstance.Text = RtbInstance.Text.Remove(start, len);
                    RtbInstance.Text = RtbInstance.Text.Insert(start, txtReplace.Text);
                    RtbInstance.Focus();
                }
            }

            /// <summary>
            /// Required designer variable.
            /// </summary>
            private System.ComponentModel.IContainer components = null;

            /// <summary>
            /// Clean up any resources being used.
            /// </summary>
            /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
            protected override void Dispose(bool disposing)
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }

            #region Windows Form Designer generated code

            /// <summary>
            /// Required method for Designer support - do not modify
            /// the contents of this method with the code editor.
            /// </summary>
            private void InitializeComponent()
            {
                this.label2 = new System.Windows.Forms.Label();
                this.txtReplace = new System.Windows.Forms.TextBox();
                this.btnReplace = new System.Windows.Forms.Button();
                this.SuspendLayout();
                // 
                // label2
                // 
                this.label2.AutoSize = true;
                this.label2.Location = new System.Drawing.Point(12, 116);
                this.label2.Name = "label2";
                this.label2.Size = new System.Drawing.Size(72, 13);
                this.label2.TabIndex = 2;
                this.label2.Text = "Replace with:";
                // 
                // txtReplace
                // 
                this.txtReplace.Location = new System.Drawing.Point(87, 113);
                this.txtReplace.Name = "txtReplace";
                this.txtReplace.Size = new System.Drawing.Size(166, 20);
                this.txtReplace.TabIndex = 3;
                // 
                // btnReplace
                // 
                this.btnReplace.Location = new System.Drawing.Point(178, 139);
                this.btnReplace.Name = "btnReplace";
                this.btnReplace.Size = new System.Drawing.Size(75, 23);
                this.btnReplace.TabIndex = 4;
                this.btnReplace.Text = "Replace";
                this.btnReplace.UseVisualStyleBackColor = true;
                this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
                // 
                // ReplaceForm
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.ClientSize = new System.Drawing.Size(274, 179);
                this.Controls.Add(this.label2);
                this.Controls.Add(this.txtReplace);
                this.Controls.Add(this.btnReplace);
                this.Name = "ReplaceForm";
                this.Controls.SetChildIndex(this.btnReplace, 0);
                this.Controls.SetChildIndex(this.txtReplace, 0);
                this.Controls.SetChildIndex(this.label2, 0);
                this.ResumeLayout(false);
                this.PerformLayout();

            }

            #endregion

            private System.Windows.Forms.Label label2;
            private System.Windows.Forms.TextBox txtReplace;
            private System.Windows.Forms.Button btnReplace;
        }
        #endregion


        #region Settings
        private int indent = 10;
        [Category("Settings")]
        [Description("Value indicating the number of characters used for indentation")]
        [DefaultValue(10)]
        public int INDENT
        {
            get { return indent; }
            set { indent = value; }
        }
        #endregion

        #region Properties for toolstrip items visibility
        [Category("Toolstip items visibility")]
        [DefaultValue(true)]
        public bool GroupSaveAndLoadVisible
        {
            get { return IsGroupVisible(RicherTextBoxToolStripGroups.SaveAndLoad); }
            set { HideToolstripItemsByGroup(RicherTextBoxToolStripGroups.SaveAndLoad, value); }
        }
        [Category("Toolstip items visibility")]
        [DefaultValue(true)]
        public bool GroupFontNameAndSizeVisible
        {
            get { return IsGroupVisible(RicherTextBoxToolStripGroups.FontNameAndSize); }
            set { HideToolstripItemsByGroup(RicherTextBoxToolStripGroups.FontNameAndSize, value); }
        }
        [Category("Toolstip items visibility")]
        [DefaultValue(true)]
        public bool GroupBoldUnderlineItalicVisible
        {
            get { return IsGroupVisible(RicherTextBoxToolStripGroups.BoldUnderlineItalic); }
            set { HideToolstripItemsByGroup(RicherTextBoxToolStripGroups.BoldUnderlineItalic, value); }
        }
        [Category("Toolstip items visibility")]
        [DefaultValue(true)]
        public bool GroupAlignmentVisible
        {
            get { return IsGroupVisible(RicherTextBoxToolStripGroups.Alignment); }
            set { HideToolstripItemsByGroup(RicherTextBoxToolStripGroups.Alignment, value); }
        }
        [Category("Toolstip items visibility")]
        [DefaultValue(true)]
        public bool GroupFontColorVisible
        {
            get { return IsGroupVisible(RicherTextBoxToolStripGroups.FontColor); }
            set { HideToolstripItemsByGroup(RicherTextBoxToolStripGroups.FontColor, value); }
        }
        [Category("Toolstip items visibility")]
        [DefaultValue(true)]
        public bool GroupIndentationAndBulletsVisible
        {
            get { return IsGroupVisible(RicherTextBoxToolStripGroups.IndentationAndBullets); }
            set { HideToolstripItemsByGroup(RicherTextBoxToolStripGroups.IndentationAndBullets, value); }
        }
        [Category("Toolstip items visibility")]
        [DefaultValue(true)]
        public bool GroupInsertVisible
        {
            get { return IsGroupVisible(RicherTextBoxToolStripGroups.Insert); }
            set { HideToolstripItemsByGroup(RicherTextBoxToolStripGroups.Insert, value); }
        }
        [Category("Toolstip items visibility")]
        [DefaultValue(true)]
        public bool GroupZoomVisible
        {
            get { return IsGroupVisible(RicherTextBoxToolStripGroups.Zoom); }
            set { HideToolstripItemsByGroup(RicherTextBoxToolStripGroups.Zoom, value); }
        }
        [Category("Toolstip items visibility")]
        [DefaultValue(true)]
        public bool ToolStripVisible
        {
            get { return toolStrip1.Visible; }
            set { toolStrip1.Visible = value; }
        }
        [Category("Toolstip items visibility")]
        [DefaultValue(true)]
        public bool FindReplaceVisible
        {
            get { return toolStripFindReplace.Visible; }
            set { toolStripFindReplace.Visible = value; }
        }

        [Category("Toolstrip single items visibility")]
        [DefaultValue(true)]
        public bool SaveVisible
        {
            get { return tsbtnSave.Visible; }
            set { tsbtnSave.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        [DefaultValue(true)]
        public bool LoadVisible
        {
            get { return tsbtnOpen.Visible; }
            set { tsbtnOpen.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        [DefaultValue(true)]
        public bool SeparatorSaveLoadVisible
        {
            get { return toolStripSeparator6.Visible; }
            set { toolStripSeparator6.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        [DefaultValue(true)]
        public bool FontFamilyVisible
        {
            get { return tscmbFont.Visible; }
            set { tscmbFont.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        [DefaultValue(true)]
        public bool FontSizeVisible
        {
            get { return tscmbFontSize.Visible; }
            set { tscmbFontSize.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        [DefaultValue(true)]
        public bool ChooseFontVisible
        {
            get { return tsbtnChooseFont.Visible; }
            set { tsbtnChooseFont.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        [DefaultValue(true)]
        public bool SeparatorFontVisible
        {
            get { return toolStripSeparator1.Visible; }
            set { toolStripSeparator1.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        [DefaultValue(true)]
        public bool BoldVisible
        {
            get { return tsbtnBold.Visible; }
            set { tsbtnBold.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        [DefaultValue(true)]
        public bool ItalicVisible
        {
            get { return tsbtnItalic.Visible; }
            set { tsbtnItalic.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        [DefaultValue(true)]
        public bool UnderlineVisible
        {
            get { return tsbtnUnderline.Visible; }
            set { tsbtnUnderline.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        [DefaultValue(true)]
        public bool SeparatorBoldUnderlineItalicVisible
        {
            get { return toolStripSeparator2.Visible; }
            set { toolStripSeparator2.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        [DefaultValue(true)]
        public bool AlignLeftVisible
        {
            get { return tsbtnAlignLeft.Visible; }
            set { tsbtnAlignLeft.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        [DefaultValue(true)]
        public bool AlignRightVisible
        {
            get { return tsbtnAlignRight.Visible; }
            set { tsbtnAlignRight.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        [DefaultValue(true)]
        public bool AlignCenterVisible
        {
            get { return tsbtnAlignCenter.Visible; }
            set { tsbtnAlignCenter.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        [DefaultValue(true)]
        public bool SeparatorAlignVisible
        {
            get { return toolStripSeparator3.Visible; }
            set { toolStripSeparator3.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        [DefaultValue(true)]
        public bool FontColorVisible
        {
            get { return tsbtnFontColor.Visible; }
            set { tsbtnFontColor.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        [DefaultValue(true)]
        public bool WordWrapVisible
        {
            get { return tsbtnWordWrap.Visible; }
            set { tsbtnWordWrap.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        [DefaultValue(true)]
        public bool SeparatorFontColorVisible
        {
            get { return toolStripSeparator4.Visible; }
            set { toolStripSeparator4.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        [DefaultValue(true)]
        public bool IndentVisible
        {
            get { return tsbtnIndent.Visible; }
            set { tsbtnIndent.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        [DefaultValue(true)]
        public bool OutdentVisible
        {
            get { return tsbtnOutdent.Visible; }
            set { tsbtnOutdent.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        [DefaultValue(true)]
        public bool BulletsVisible
        {
            get { return tsbtnBullets.Visible; }
            set { tsbtnBullets.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        [DefaultValue(true)]
        public bool SeparatorIndentAndBulletsVisible
        {
            get { return toolStripSeparator5.Visible; }
            set { toolStripSeparator5.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        [DefaultValue(true)]
        public bool InsertPictureVisible
        {
            get { return tsbtnInsertPicture.Visible; }
            set { tsbtnInsertPicture.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        [DefaultValue(true)]
        public bool SeparatorInsertVisible
        {
            get { return toolStripSeparator7.Visible; }
            set { toolStripSeparator7.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        [DefaultValue(true)]
        public bool ZoomInVisible
        {
            get { return tsbtnZoomIn.Visible; }
            set { tsbtnZoomIn.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        [DefaultValue(true)]
        public bool ZoomOutVisible
        {
            get { return tsbtnZoomOut.Visible; }
            set { tsbtnZoomOut.Visible = value; }
        }
        [Category("Toolstrip single items visibility")]
        [DefaultValue(true)]
        public bool ZoomFactorTextVisible
        {
            get { return tstxtZoomFactor.Visible; }
            set { tstxtZoomFactor.Visible = value; }
        }

        #endregion

        #region data properties
        [Category("Document data")]
        [Description("RicherTextBox content in plain text")]
        [Browsable(true)]
        public override string Text
        {
            get { return rtbDocument.Text; }
            set { rtbDocument.Text = value; }
        }
        [Category("Document data")]
        [Description("RicherTextBox content in rich-text format")]
        public string Rtf
        {
            get { return rtbDocument.Rtf; }
            set { try { rtbDocument.Rtf = value; } catch (ArgumentException) { rtbDocument.Text = value; } }
        }

        #endregion

        #region Construction and initial loading

        private void RicherTextBox_Load(object sender, EventArgs e)
        {
            // load system fonts
            foreach (FontFamily family in FontFamily.Families)
            {
                tscmbFont.Items.Add(family.Name);
            }
            tscmbFont.SelectedItem = "Microsoft Sans Serif";

            tscmbFontSize.SelectedItem = "9";

            tstxtZoomFactor.Text = Convert.ToString(rtbDocument.ZoomFactor * 100);
            tsbtnWordWrap.Checked = rtbDocument.WordWrap;
        }

        #endregion

        #region Toolstrip items handling

        private void tsbtnBIU_Click(object sender, EventArgs e)
        {
            // bold, italic, underline
            try
            {
                if (!(rtbDocument.SelectionFont == null))
                {
                    Font currentFont = rtbDocument.SelectionFont;
                    FontStyle newFontStyle = rtbDocument.SelectionFont.Style;
                    string txt = (sender as ToolStripButton).Name;
                    if (txt.IndexOf("Bold") >= 0)
                        newFontStyle = rtbDocument.SelectionFont.Style ^ FontStyle.Bold;
                    else if (txt.IndexOf("Italic") >= 0)
                        newFontStyle = rtbDocument.SelectionFont.Style ^ FontStyle.Italic;
                    else if (txt.IndexOf("Underline") >= 0)
                        newFontStyle = rtbDocument.SelectionFont.Style ^ FontStyle.Underline;

                    rtbDocument.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void rtbDocument_SelectionChanged(object sender, EventArgs e)
        {
            if (rtbDocument.SelectionFont != null)
            {
                tsbtnBold.Checked = rtbDocument.SelectionFont.Bold;
                tsbtnItalic.Checked = rtbDocument.SelectionFont.Italic;
                tsbtnUnderline.Checked = rtbDocument.SelectionFont.Underline;

                boldToolStripMenuItem.Checked = rtbDocument.SelectionFont.Bold;
                italicToolStripMenuItem.Checked = rtbDocument.SelectionFont.Italic;
                underlineToolStripMenuItem.Checked = rtbDocument.SelectionFont.Underline;

                switch (rtbDocument.SelectionAlignment)
                {
                    case HorizontalAlignment.Left:
                        tsbtnAlignLeft.Checked = true;
                        tsbtnAlignCenter.Checked = false;
                        tsbtnAlignRight.Checked = false;

                        leftToolStripMenuItem.Checked = true;
                        centerToolStripMenuItem.Checked = false;
                        rightToolStripMenuItem.Checked = false;
                        break;

                    case HorizontalAlignment.Center:
                        tsbtnAlignLeft.Checked = false;
                        tsbtnAlignCenter.Checked = true;
                        tsbtnAlignRight.Checked = false;

                        leftToolStripMenuItem.Checked = false;
                        centerToolStripMenuItem.Checked = true;
                        rightToolStripMenuItem.Checked = false;
                        break;

                    case HorizontalAlignment.Right:
                        tsbtnAlignLeft.Checked = false;
                        tsbtnAlignCenter.Checked = false;
                        tsbtnAlignRight.Checked = true;

                        leftToolStripMenuItem.Checked = false;
                        centerToolStripMenuItem.Checked = false;
                        rightToolStripMenuItem.Checked = true;
                        break;
                }

                tsbtnBullets.Checked = rtbDocument.SelectionBullet;
                bulletsToolStripMenuItem.Checked = rtbDocument.SelectionBullet;

                tscmbFont.SelectedItem = rtbDocument.SelectionFont.FontFamily.Name;
                tscmbFontSize.SelectedItem = rtbDocument.SelectionFont.Size.ToString();
            }
        }

        private void tsbtnAlignment_Click(object sender, EventArgs e)
        {
            // alignment: left, center, right
            try
            {
                string txt = (sender as ToolStripButton).Name;
                if (txt.IndexOf("Left") >= 0)
                {
                    rtbDocument.SelectionAlignment = HorizontalAlignment.Left;
                    tsbtnAlignLeft.Checked = true;
                    tsbtnAlignCenter.Checked = false;
                    tsbtnAlignRight.Checked = false;
                }
                else if (txt.IndexOf("Center") >= 0)
                {
                    rtbDocument.SelectionAlignment = HorizontalAlignment.Center;
                    tsbtnAlignLeft.Checked = false;
                    tsbtnAlignCenter.Checked = true;
                    tsbtnAlignRight.Checked = false;
                }
                else if (txt.IndexOf("Right") >= 0)
                {
                    rtbDocument.SelectionAlignment = HorizontalAlignment.Right;
                    tsbtnAlignLeft.Checked = false;
                    tsbtnAlignCenter.Checked = false;
                    tsbtnAlignRight.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void tsbtnFontColor_Click(object sender, EventArgs e)
        {
            // font color
            try
            {
                using (ColorDialog dlg = new ColorDialog())
                {
                    dlg.Color = rtbDocument.SelectionColor;
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        rtbDocument.SelectionColor = dlg.Color;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void tsbtnBulletsAndNumbering_Click(object sender, EventArgs e)
        {
            // bullets, indentation
            try
            {
                string name = (sender as ToolStripButton).Name;
                if (name.IndexOf("Bullets") >= 0)
                    rtbDocument.SelectionBullet = tsbtnBullets.Checked;
                else if (name.IndexOf("Indent") >= 0)
                    rtbDocument.SelectionIndent += INDENT;
                else if (name.IndexOf("Outdent") >= 0)
                    rtbDocument.SelectionIndent -= INDENT;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void tscmbFontSize_Click(object sender, EventArgs e)
        {
            // font size
            try
            {
                if (!(rtbDocument.SelectionFont == null))
                {
                    Font currentFont = rtbDocument.SelectionFont;
                    float newSize = Convert.ToSingle(tscmbFontSize.SelectedItem.ToString());
                    rtbDocument.SelectionFont = new Font(currentFont.FontFamily, newSize, currentFont.Style);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }


        private void tscmbFontSize_TextChanged(object sender, EventArgs e)
        {
            // font size custom
            try
            {
                if (!(rtbDocument.SelectionFont == null))
                {
                    Font currentFont = rtbDocument.SelectionFont;
                    float newSize = Convert.ToSingle(tscmbFontSize.Text);
                    rtbDocument.SelectionFont = new Font(currentFont.FontFamily, newSize, currentFont.Style);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void tscmbFont_Click(object sender, EventArgs e)
        {
            // font
            try
            {
                if (!(rtbDocument.SelectionFont == null))
                {
                    Font currentFont = rtbDocument.SelectionFont;
                    FontFamily newFamily = new FontFamily(tscmbFont.SelectedItem.ToString());
                    rtbDocument.SelectionFont = new Font(newFamily, currentFont.Size, currentFont.Style);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void btnChooseFont_Click(object sender, EventArgs e)
        {
            using (FontDialog dlg = new FontDialog())
            {
                if (rtbDocument.SelectionFont != null) dlg.Font = rtbDocument.SelectionFont;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    rtbDocument.SelectionFont = dlg.Font;
                }
            }
        }

        private void tsbtnInsertPicture_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Insert picture";
                dlg.DefaultExt = "jpg";
                dlg.Filter = "Bitmap Files|*.bmp|JPEG Files|*.jpg|GIF Files|*.gif|All files|*.*";
                dlg.FilterIndex = 1;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string strImagePath = dlg.FileName;
                        Image img = Image.FromFile(strImagePath);
                        Clipboard.SetDataObject(img);
                        DataFormats.Format df;
                        df = DataFormats.GetFormat(DataFormats.Bitmap);
                        if (this.rtbDocument.CanPaste(df))
                        {
                            this.rtbDocument.Paste(df);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Unable to insert image.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void tsbtnSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "Rich text format|*.rtf";
                dlg.FilterIndex = 0;
                dlg.OverwritePrompt = true;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        rtbDocument.SaveFile(dlg.FileName, RichTextBoxStreamType.RichText);
                    }
                    catch (IOException exc)
                    {
                        MessageBox.Show("Error writing file: \n" + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (ArgumentException exc_a)
                    {
                        MessageBox.Show("Error writing file: \n" + exc_a.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void tsbtnOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Rich text format|*.rtf";
                dlg.FilterIndex = 0;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        rtbDocument.LoadFile(dlg.FileName, RichTextBoxStreamType.RichText);
                    }
                    catch (IOException exc)
                    {
                        MessageBox.Show("Error reading file: \n" + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (ArgumentException exc_a)
                    {
                        MessageBox.Show("Error reading file: \n" + exc_a.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (rtbDocument.ZoomFactor < 64.0f - 0.20f)
            {
                rtbDocument.ZoomFactor += 0.20f;
                tstxtZoomFactor.Text = String.Format("{0:F0}", rtbDocument.ZoomFactor * 100);
            }
        }

        private void tsbtnZoomOut_Click(object sender, EventArgs e)
        {
            if (rtbDocument.ZoomFactor > 0.16f + 0.20f)
            {
                rtbDocument.ZoomFactor -= 0.20f;
                tstxtZoomFactor.Text = String.Format("{0:F0}", rtbDocument.ZoomFactor * 100);
            }
        }

        private void tstxtZoomFactor_Leave(object sender, EventArgs e)
        {
            try
            {
                rtbDocument.ZoomFactor = Convert.ToSingle(tstxtZoomFactor.Text) / 100;
            }
            catch (FormatException)
            {
                MessageBox.Show("Enter valid number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tstxtZoomFactor.Focus();
                tstxtZoomFactor.SelectAll();
            }
            catch (OverflowException)
            {
                MessageBox.Show("Enter valid number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tstxtZoomFactor.Focus();
                tstxtZoomFactor.SelectAll();
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Zoom factor should be between 20% and 6400%", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tstxtZoomFactor.Focus();
                tstxtZoomFactor.SelectAll();
            }
        }


        private void tsbtnWordWrap_Click(object sender, EventArgs e)
        {
            rtbDocument.WordWrap = tsbtnWordWrap.Checked;
        }

        #endregion

        #region Changing visibility of toolstrip items

        public void HideToolstripItemsByGroup(RicherTextBoxToolStripGroups group, bool visible)
        {
            if ((group & RicherTextBoxToolStripGroups.SaveAndLoad) != 0)
            {
                tsbtnSave.Visible = visible;
                tsbtnOpen.Visible = visible;
                toolStripSeparator6.Visible = visible;
            }
            if ((group & RicherTextBoxToolStripGroups.FontNameAndSize) != 0)
            {
                tscmbFont.Visible = visible;
                tscmbFontSize.Visible = visible;
                tsbtnChooseFont.Visible = visible;
                toolStripSeparator1.Visible = visible;
            }
            if ((group & RicherTextBoxToolStripGroups.BoldUnderlineItalic) != 0)
            {
                tsbtnBold.Visible = visible;
                tsbtnItalic.Visible = visible;
                tsbtnUnderline.Visible = visible;
                toolStripSeparator2.Visible = visible;
            }
            if ((group & RicherTextBoxToolStripGroups.Alignment) != 0)
            {
                tsbtnAlignLeft.Visible = visible;
                tsbtnAlignRight.Visible = visible;
                tsbtnAlignCenter.Visible = visible;
                toolStripSeparator3.Visible = visible;
            }
            if ((group & RicherTextBoxToolStripGroups.FontColor) != 0)
            {
                tsbtnFontColor.Visible = visible;
                tsbtnWordWrap.Visible = visible;
                toolStripSeparator4.Visible = visible;
            }
            if ((group & RicherTextBoxToolStripGroups.IndentationAndBullets) != 0)
            {
                tsbtnIndent.Visible = visible;
                tsbtnOutdent.Visible = visible;
                tsbtnBullets.Visible = visible;
                toolStripSeparator5.Visible = visible;
            }
            if ((group & RicherTextBoxToolStripGroups.Insert) != 0)
            {
                tsbtnInsertPicture.Visible = visible;
                toolStripSeparator7.Visible = visible;
            }
            if ((group & RicherTextBoxToolStripGroups.Zoom) != 0)
            {
                tsbtnZoomOut.Visible = visible;
                tsbtnZoomIn.Visible = visible;
                tstxtZoomFactor.Visible = visible;
            }
        }

        public bool IsGroupVisible(RicherTextBoxToolStripGroups group)
        {
            switch (group)
            {
                case RicherTextBoxToolStripGroups.SaveAndLoad:
                    return tsbtnSave.Visible && tsbtnOpen.Visible && toolStripSeparator6.Visible;

                case RicherTextBoxToolStripGroups.FontNameAndSize:
                    return tscmbFont.Visible && tscmbFontSize.Visible && tsbtnChooseFont.Visible && toolStripSeparator1.Visible;

                case RicherTextBoxToolStripGroups.BoldUnderlineItalic:
                    return tsbtnBold.Visible && tsbtnItalic.Visible && tsbtnUnderline.Visible && toolStripSeparator2.Visible;

                case RicherTextBoxToolStripGroups.Alignment:
                    return tsbtnAlignLeft.Visible && tsbtnAlignRight.Visible && tsbtnAlignCenter.Visible && toolStripSeparator3.Visible;

                case RicherTextBoxToolStripGroups.FontColor:
                    return tsbtnFontColor.Visible && tsbtnWordWrap.Visible && toolStripSeparator4.Visible;

                case RicherTextBoxToolStripGroups.IndentationAndBullets:
                    return tsbtnIndent.Visible && tsbtnOutdent.Visible && tsbtnBullets.Visible && toolStripSeparator5.Visible;

                case RicherTextBoxToolStripGroups.Insert:
                    return tsbtnInsertPicture.Visible && toolStripSeparator7.Visible;

                case RicherTextBoxToolStripGroups.Zoom:
                    return tsbtnZoomOut.Visible && tsbtnZoomIn.Visible && tstxtZoomFactor.Visible;

                default:
                    return false;
            }
        }
        #endregion

        #region Public methods for accessing the functionality of the RicherTextBox

        public void SetFontFamily(FontFamily family)
        {
            if (family != null)
            {
                tscmbFont.SelectedItem = family.Name;
            }
        }

        public void SetFontSize(float newSize)
        {
            tscmbFontSize.Text = newSize.ToString();
        }

        public void ToggleBold()
        {
            tsbtnBold.PerformClick();
        }

        public void ToggleItalic()
        {
            tsbtnItalic.PerformClick();
        }

        public void ToggleUnderline()
        {
            tsbtnUnderline.PerformClick();
        }

        public void SetAlign(HorizontalAlignment alignment)
        {
            switch (alignment)
            {
                case HorizontalAlignment.Center:
                    tsbtnAlignCenter.PerformClick();
                    break;

                case HorizontalAlignment.Left:
                    tsbtnAlignLeft.PerformClick();
                    break;

                case HorizontalAlignment.Right:
                    tsbtnAlignRight.PerformClick();
                    break;
            }
        }

        public void Indent()
        {
            tsbtnIndent.PerformClick();
        }

        public void Outdent()
        {
            tsbtnOutdent.PerformClick();
        }

        public void ToggleBullets()
        {
            tsbtnBullets.PerformClick();
        }

        public void ZoomIn()
        {
            tsbtnZoomIn.PerformClick();
        }

        public void ZoomOut()
        {
            tsbtnZoomOut.PerformClick();
        }

        public void ZoomTo(float factor)
        {
            rtbDocument.ZoomFactor = factor;
        }

        public void SetWordWrap(bool activated)
        {
            rtbDocument.WordWrap = activated;
        }

        #endregion


        #region Context menu handlers

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbDocument.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbDocument.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbDocument.Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbDocument.Clear();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbDocument.SelectAll();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbDocument.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbDocument.Redo();
        }

        private void leftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsbtnAlignLeft.PerformClick();

            leftToolStripMenuItem.Checked = true;
            centerToolStripMenuItem.Checked = false;
            rightToolStripMenuItem.Checked = false;


        }

        private void centerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsbtnAlignCenter.PerformClick();

            leftToolStripMenuItem.Checked = false;
            centerToolStripMenuItem.Checked = true;
            rightToolStripMenuItem.Checked = false;
        }

        private void rightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsbtnAlignRight.PerformClick();

            leftToolStripMenuItem.Checked = false;
            centerToolStripMenuItem.Checked = false;
            rightToolStripMenuItem.Checked = true;
        }

        private void boldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsbtnBold.PerformClick();
        }

        private void italicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsbtnItalic.PerformClick();
        }

        private void underlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsbtnUnderline.PerformClick();
        }

        private void increaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsbtnIndent.PerformClick();
        }

        private void decreaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsbtnOutdent.PerformClick();
        }

        private void bulletsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsbtnBullets.PerformClick();
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsbtnZoomIn.PerformClick();
        }

        private void zoomOuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsbtnZoomOut.PerformClick();
        }

        private void insertPictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsbtnInsertPicture.PerformClick();
        }

        #endregion

        #region Find and Replace

        private void tsbtnFind_Click(object sender, EventArgs e)
        {
            FindForm findForm = new FindForm();
            findForm.RtbInstance = this.rtbDocument;
            findForm.InitialText = this.tstxtSearchText.Text;
            findForm.Show();
        }

        private void tsbtnReplace_Click(object sender, EventArgs e)
        {
            ReplaceForm replaceForm = new ReplaceForm();
            replaceForm.RtbInstance = this.rtbDocument;
            replaceForm.InitialText = this.tstxtSearchText.Text;
            replaceForm.Show();
        }

        #endregion

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TYRicherTextBox));
            this.rtbDocument = new System.Windows.Forms.RichTextBox();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.alignmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.centerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.styleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.boldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.italicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.underlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.increaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decreaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bulletsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.insertPictureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.zoomInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomOuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnSave = new System.Windows.Forms.ToolStripButton();
            this.tsbtnOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tscmbFont = new System.Windows.Forms.ToolStripComboBox();
            this.tscmbFontSize = new System.Windows.Forms.ToolStripComboBox();
            this.tsbtnChooseFont = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnBold = new System.Windows.Forms.ToolStripButton();
            this.tsbtnItalic = new System.Windows.Forms.ToolStripButton();
            this.tsbtnUnderline = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnAlignLeft = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAlignCenter = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAlignRight = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnFontColor = new System.Windows.Forms.ToolStripButton();
            this.tsbtnWordWrap = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnIndent = new System.Windows.Forms.ToolStripButton();
            this.tsbtnOutdent = new System.Windows.Forms.ToolStripButton();
            this.tsbtnBullets = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnInsertPicture = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.tsbtnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.tstxtZoomFactor = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripFindReplace = new System.Windows.Forms.ToolStrip();
            this.tstxtSearchText = new System.Windows.Forms.ToolStripTextBox();
            this.tsbtnFind = new System.Windows.Forms.ToolStripButton();
            this.tsbtnReplace = new System.Windows.Forms.ToolStripButton();
            this.contextMenu.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStripFindReplace.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbDocument
            // 
            this.rtbDocument.AcceptsTab = true;
            this.rtbDocument.ContextMenuStrip = this.contextMenu;
            this.rtbDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbDocument.EnableAutoDragDrop = true;
            this.rtbDocument.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rtbDocument.HideSelection = false;
            this.rtbDocument.Location = new System.Drawing.Point(0, 51);
            this.rtbDocument.Name = "rtbDocument";
            this.rtbDocument.Size = new System.Drawing.Size(778, 238);
            this.rtbDocument.TabIndex = 0;
            this.rtbDocument.Text = "";
            this.rtbDocument.SelectionChanged += new System.EventHandler(this.rtbDocument_SelectionChanged);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.selectAllToolStripMenuItem,
            this.toolStripMenuItem1,
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripMenuItem2,
            this.alignmentToolStripMenuItem,
            this.styleToolStripMenuItem,
            this.indentationToolStripMenuItem,
            this.toolStripMenuItem3,
            this.insertPictureToolStripMenuItem,
            this.toolStripMenuItem4,
            this.zoomInToolStripMenuItem,
            this.zoomOuToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(151, 314);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripMenuItem.Image")));
            this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
            this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
            this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripMenuItem.Image")));
            this.deleteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.deleteToolStripMenuItem.Text = "Clear";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("selectAllToolStripMenuItem.Image")));
            this.selectAllToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(147, 6);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("undoToolStripMenuItem.Image")));
            this.undoToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("redoToolStripMenuItem.Image")));
            this.redoToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(147, 6);
            // 
            // alignmentToolStripMenuItem
            // 
            this.alignmentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.leftToolStripMenuItem,
            this.centerToolStripMenuItem,
            this.rightToolStripMenuItem});
            this.alignmentToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("alignmentToolStripMenuItem.Image")));
            this.alignmentToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.alignmentToolStripMenuItem.Name = "alignmentToolStripMenuItem";
            this.alignmentToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.alignmentToolStripMenuItem.Text = "Alignment";
            // 
            // leftToolStripMenuItem
            // 
            this.leftToolStripMenuItem.CheckOnClick = true;
            this.leftToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("leftToolStripMenuItem.Image")));
            this.leftToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.leftToolStripMenuItem.Name = "leftToolStripMenuItem";
            this.leftToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.leftToolStripMenuItem.Text = "Left";
            this.leftToolStripMenuItem.Click += new System.EventHandler(this.leftToolStripMenuItem_Click);
            // 
            // centerToolStripMenuItem
            // 
            this.centerToolStripMenuItem.CheckOnClick = true;
            this.centerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("centerToolStripMenuItem.Image")));
            this.centerToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.centerToolStripMenuItem.Name = "centerToolStripMenuItem";
            this.centerToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.centerToolStripMenuItem.Text = "Center";
            this.centerToolStripMenuItem.Click += new System.EventHandler(this.centerToolStripMenuItem_Click);
            // 
            // rightToolStripMenuItem
            // 
            this.rightToolStripMenuItem.CheckOnClick = true;
            this.rightToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("rightToolStripMenuItem.Image")));
            this.rightToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.rightToolStripMenuItem.Name = "rightToolStripMenuItem";
            this.rightToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.rightToolStripMenuItem.Text = "Right";
            this.rightToolStripMenuItem.Click += new System.EventHandler(this.rightToolStripMenuItem_Click);
            // 
            // styleToolStripMenuItem
            // 
            this.styleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.boldToolStripMenuItem,
            this.italicToolStripMenuItem,
            this.underlineToolStripMenuItem});
            this.styleToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.styleToolStripMenuItem.Name = "styleToolStripMenuItem";
            this.styleToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.styleToolStripMenuItem.Text = "Style";
            // 
            // boldToolStripMenuItem
            // 
            this.boldToolStripMenuItem.CheckOnClick = true;
            this.boldToolStripMenuItem.Name = "boldToolStripMenuItem";
            this.boldToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.boldToolStripMenuItem.Text = "Bold";
            this.boldToolStripMenuItem.Click += new System.EventHandler(this.boldToolStripMenuItem_Click);
            // 
            // italicToolStripMenuItem
            // 
            this.italicToolStripMenuItem.CheckOnClick = true;
            this.italicToolStripMenuItem.Name = "italicToolStripMenuItem";
            this.italicToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.italicToolStripMenuItem.Text = "Italic";
            this.italicToolStripMenuItem.Click += new System.EventHandler(this.italicToolStripMenuItem_Click);
            // 
            // underlineToolStripMenuItem
            // 
            this.underlineToolStripMenuItem.CheckOnClick = true;
            this.underlineToolStripMenuItem.Name = "underlineToolStripMenuItem";
            this.underlineToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.underlineToolStripMenuItem.Text = "Underline";
            this.underlineToolStripMenuItem.Click += new System.EventHandler(this.underlineToolStripMenuItem_Click);
            // 
            // indentationToolStripMenuItem
            // 
            this.indentationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.increaseToolStripMenuItem,
            this.decreaseToolStripMenuItem,
            this.bulletsToolStripMenuItem});
            this.indentationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("indentationToolStripMenuItem.Image")));
            this.indentationToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.indentationToolStripMenuItem.Name = "indentationToolStripMenuItem";
            this.indentationToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.indentationToolStripMenuItem.Text = "Indentation";
            // 
            // increaseToolStripMenuItem
            // 
            this.increaseToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("increaseToolStripMenuItem.Image")));
            this.increaseToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.increaseToolStripMenuItem.Name = "increaseToolStripMenuItem";
            this.increaseToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.increaseToolStripMenuItem.Text = "Increase";
            this.increaseToolStripMenuItem.Click += new System.EventHandler(this.increaseToolStripMenuItem_Click);
            // 
            // decreaseToolStripMenuItem
            // 
            this.decreaseToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("decreaseToolStripMenuItem.Image")));
            this.decreaseToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.decreaseToolStripMenuItem.Name = "decreaseToolStripMenuItem";
            this.decreaseToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.decreaseToolStripMenuItem.Text = "Decrease";
            this.decreaseToolStripMenuItem.Click += new System.EventHandler(this.decreaseToolStripMenuItem_Click);
            // 
            // bulletsToolStripMenuItem
            // 
            this.bulletsToolStripMenuItem.CheckOnClick = true;
            this.bulletsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("bulletsToolStripMenuItem.Image")));
            this.bulletsToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.bulletsToolStripMenuItem.Name = "bulletsToolStripMenuItem";
            this.bulletsToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.bulletsToolStripMenuItem.Text = "Bullets";
            this.bulletsToolStripMenuItem.Click += new System.EventHandler(this.bulletsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(147, 6);
            // 
            // insertPictureToolStripMenuItem
            // 
            this.insertPictureToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("insertPictureToolStripMenuItem.Image")));
            this.insertPictureToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.insertPictureToolStripMenuItem.Name = "insertPictureToolStripMenuItem";
            this.insertPictureToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.insertPictureToolStripMenuItem.Text = "Insert Picture";
            this.insertPictureToolStripMenuItem.Click += new System.EventHandler(this.insertPictureToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(147, 6);
            // 
            // zoomInToolStripMenuItem
            // 
            this.zoomInToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("zoomInToolStripMenuItem.Image")));
            this.zoomInToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
            this.zoomInToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.zoomInToolStripMenuItem.Text = "Zoom In";
            this.zoomInToolStripMenuItem.Click += new System.EventHandler(this.zoomInToolStripMenuItem_Click);
            // 
            // zoomOuToolStripMenuItem
            // 
            this.zoomOuToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("zoomOuToolStripMenuItem.Image")));
            this.zoomOuToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.zoomOuToolStripMenuItem.Name = "zoomOuToolStripMenuItem";
            this.zoomOuToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.zoomOuToolStripMenuItem.Text = "Zoom Out";
            this.zoomOuToolStripMenuItem.Click += new System.EventHandler(this.zoomOuToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnSave,
            this.tsbtnOpen,
            this.toolStripSeparator6,
            this.tscmbFont,
            this.tscmbFontSize,
            this.tsbtnChooseFont,
            this.toolStripSeparator1,
            this.tsbtnBold,
            this.tsbtnItalic,
            this.tsbtnUnderline,
            this.toolStripSeparator2,
            this.tsbtnAlignLeft,
            this.tsbtnAlignCenter,
            this.tsbtnAlignRight,
            this.toolStripSeparator3,
            this.tsbtnFontColor,
            this.tsbtnWordWrap,
            this.toolStripSeparator4,
            this.tsbtnIndent,
            this.tsbtnOutdent,
            this.tsbtnBullets,
            this.toolStripSeparator5,
            this.tsbtnInsertPicture,
            this.toolStripSeparator7,
            this.tsbtnZoomIn,
            this.tsbtnZoomOut,
            this.tstxtZoomFactor});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(778, 26);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtnSave
            // 
            this.tsbtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSave.Image")));
            this.tsbtnSave.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnSave.Name = "tsbtnSave";
            this.tsbtnSave.Size = new System.Drawing.Size(23, 23);
            this.tsbtnSave.Text = "toolStripButton1";
            this.tsbtnSave.ToolTipText = "Save Document";
            this.tsbtnSave.Click += new System.EventHandler(this.tsbtnSave_Click);
            // 
            // tsbtnOpen
            // 
            this.tsbtnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnOpen.Image")));
            this.tsbtnOpen.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnOpen.Name = "tsbtnOpen";
            this.tsbtnOpen.Size = new System.Drawing.Size(23, 23);
            this.tsbtnOpen.Text = "toolStripButton2";
            this.tsbtnOpen.ToolTipText = "Load Document";
            this.tsbtnOpen.Click += new System.EventHandler(this.tsbtnOpen_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 26);
            // 
            // tscmbFont
            // 
            this.tscmbFont.Name = "tscmbFont";
            this.tscmbFont.Size = new System.Drawing.Size(140, 26);
            this.tscmbFont.SelectedIndexChanged += new System.EventHandler(this.tscmbFont_Click);
            // 
            // tscmbFontSize
            // 
            this.tscmbFontSize.AutoSize = false;
            this.tscmbFontSize.Items.AddRange(new object[] {
            "8",
            "9",
            "10",
            "11",
            "12",
            "14",
            "16",
            "18",
            "20",
            "22",
            "24",
            "26",
            "28",
            "36",
            "48",
            "72"});
            this.tscmbFontSize.Name = "tscmbFontSize";
            this.tscmbFontSize.Size = new System.Drawing.Size(46, 23);
            this.tscmbFontSize.SelectedIndexChanged += new System.EventHandler(this.tscmbFontSize_Click);
            this.tscmbFontSize.TextChanged += new System.EventHandler(this.tscmbFontSize_TextChanged);
            // 
            // tsbtnChooseFont
            // 
            this.tsbtnChooseFont.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnChooseFont.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnChooseFont.Image")));
            this.tsbtnChooseFont.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnChooseFont.Name = "tsbtnChooseFont";
            this.tsbtnChooseFont.Size = new System.Drawing.Size(23, 23);
            this.tsbtnChooseFont.Text = "toolStripButton1";
            this.tsbtnChooseFont.ToolTipText = "Select Font";
            this.tsbtnChooseFont.Click += new System.EventHandler(this.btnChooseFont_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 26);
            // 
            // tsbtnBold
            // 
            this.tsbtnBold.CheckOnClick = true;
            this.tsbtnBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnBold.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tsbtnBold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnBold.Name = "tsbtnBold";
            this.tsbtnBold.Size = new System.Drawing.Size(24, 23);
            this.tsbtnBold.Text = "B";
            this.tsbtnBold.ToolTipText = "Toggle Bold";
            this.tsbtnBold.Click += new System.EventHandler(this.tsbtnBIU_Click);
            // 
            // tsbtnItalic
            // 
            this.tsbtnItalic.CheckOnClick = true;
            this.tsbtnItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnItalic.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic);
            this.tsbtnItalic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnItalic.Name = "tsbtnItalic";
            this.tsbtnItalic.Size = new System.Drawing.Size(23, 23);
            this.tsbtnItalic.Text = "I";
            this.tsbtnItalic.ToolTipText = "Toggle Italic";
            this.tsbtnItalic.Click += new System.EventHandler(this.tsbtnBIU_Click);
            // 
            // tsbtnUnderline
            // 
            this.tsbtnUnderline.CheckOnClick = true;
            this.tsbtnUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnUnderline.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Underline);
            this.tsbtnUnderline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnUnderline.Name = "tsbtnUnderline";
            this.tsbtnUnderline.Size = new System.Drawing.Size(24, 23);
            this.tsbtnUnderline.Text = "U";
            this.tsbtnUnderline.ToolTipText = "Toggle Underline";
            this.tsbtnUnderline.Click += new System.EventHandler(this.tsbtnBIU_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 26);
            // 
            // tsbtnAlignLeft
            // 
            this.tsbtnAlignLeft.CheckOnClick = true;
            this.tsbtnAlignLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAlignLeft.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAlignLeft.Image")));
            this.tsbtnAlignLeft.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnAlignLeft.Name = "tsbtnAlignLeft";
            this.tsbtnAlignLeft.Size = new System.Drawing.Size(23, 23);
            this.tsbtnAlignLeft.Text = "toolStripButton1";
            this.tsbtnAlignLeft.ToolTipText = "Align Left";
            this.tsbtnAlignLeft.Click += new System.EventHandler(this.tsbtnAlignment_Click);
            // 
            // tsbtnAlignCenter
            // 
            this.tsbtnAlignCenter.CheckOnClick = true;
            this.tsbtnAlignCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAlignCenter.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAlignCenter.Image")));
            this.tsbtnAlignCenter.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnAlignCenter.Name = "tsbtnAlignCenter";
            this.tsbtnAlignCenter.Size = new System.Drawing.Size(23, 23);
            this.tsbtnAlignCenter.Text = "toolStripButton2";
            this.tsbtnAlignCenter.ToolTipText = "Align Center";
            this.tsbtnAlignCenter.Click += new System.EventHandler(this.tsbtnAlignment_Click);
            // 
            // tsbtnAlignRight
            // 
            this.tsbtnAlignRight.CheckOnClick = true;
            this.tsbtnAlignRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAlignRight.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAlignRight.Image")));
            this.tsbtnAlignRight.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnAlignRight.Name = "tsbtnAlignRight";
            this.tsbtnAlignRight.Size = new System.Drawing.Size(23, 23);
            this.tsbtnAlignRight.Text = "toolStripButton3";
            this.tsbtnAlignRight.ToolTipText = "Align Right";
            this.tsbtnAlignRight.Click += new System.EventHandler(this.tsbtnAlignment_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 26);
            // 
            // tsbtnFontColor
            // 
            this.tsbtnFontColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnFontColor.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnFontColor.Image")));
            this.tsbtnFontColor.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnFontColor.Name = "tsbtnFontColor";
            this.tsbtnFontColor.Size = new System.Drawing.Size(23, 23);
            this.tsbtnFontColor.Text = "toolStripButton4";
            this.tsbtnFontColor.ToolTipText = "Pick Font Color";
            this.tsbtnFontColor.Click += new System.EventHandler(this.tsbtnFontColor_Click);
            // 
            // tsbtnWordWrap
            // 
            this.tsbtnWordWrap.CheckOnClick = true;
            this.tsbtnWordWrap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnWordWrap.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnWordWrap.Image")));
            this.tsbtnWordWrap.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnWordWrap.Name = "tsbtnWordWrap";
            this.tsbtnWordWrap.Size = new System.Drawing.Size(23, 23);
            this.tsbtnWordWrap.Text = "toolStripButton1";
            this.tsbtnWordWrap.ToolTipText = "Word Wrap";
            this.tsbtnWordWrap.Click += new System.EventHandler(this.tsbtnWordWrap_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 26);
            // 
            // tsbtnIndent
            // 
            this.tsbtnIndent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnIndent.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnIndent.Image")));
            this.tsbtnIndent.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnIndent.Name = "tsbtnIndent";
            this.tsbtnIndent.Size = new System.Drawing.Size(23, 23);
            this.tsbtnIndent.Text = "toolStripButton1";
            this.tsbtnIndent.ToolTipText = "Indent";
            this.tsbtnIndent.Click += new System.EventHandler(this.tsbtnBulletsAndNumbering_Click);
            // 
            // tsbtnOutdent
            // 
            this.tsbtnOutdent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnOutdent.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnOutdent.Image")));
            this.tsbtnOutdent.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnOutdent.Name = "tsbtnOutdent";
            this.tsbtnOutdent.Size = new System.Drawing.Size(23, 23);
            this.tsbtnOutdent.Text = "toolStripButton3";
            this.tsbtnOutdent.ToolTipText = "Outdent";
            this.tsbtnOutdent.Click += new System.EventHandler(this.tsbtnBulletsAndNumbering_Click);
            // 
            // tsbtnBullets
            // 
            this.tsbtnBullets.CheckOnClick = true;
            this.tsbtnBullets.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnBullets.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnBullets.Image")));
            this.tsbtnBullets.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnBullets.Name = "tsbtnBullets";
            this.tsbtnBullets.Size = new System.Drawing.Size(23, 23);
            this.tsbtnBullets.Text = "toolStripButton2";
            this.tsbtnBullets.ToolTipText = "Toggle Bullets";
            this.tsbtnBullets.Click += new System.EventHandler(this.tsbtnBulletsAndNumbering_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 26);
            // 
            // tsbtnInsertPicture
            // 
            this.tsbtnInsertPicture.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnInsertPicture.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnInsertPicture.Image")));
            this.tsbtnInsertPicture.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnInsertPicture.Name = "tsbtnInsertPicture";
            this.tsbtnInsertPicture.Size = new System.Drawing.Size(23, 23);
            this.tsbtnInsertPicture.Text = "toolStripButton1";
            this.tsbtnInsertPicture.ToolTipText = "Insert Picture";
            this.tsbtnInsertPicture.Click += new System.EventHandler(this.tsbtnInsertPicture_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 26);
            // 
            // tsbtnZoomIn
            // 
            this.tsbtnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnZoomIn.Image")));
            this.tsbtnZoomIn.ImageTransparentColor = System.Drawing.Color.White;
            this.tsbtnZoomIn.Name = "tsbtnZoomIn";
            this.tsbtnZoomIn.Size = new System.Drawing.Size(23, 23);
            this.tsbtnZoomIn.Text = "toolStripButton1";
            this.tsbtnZoomIn.ToolTipText = "Zoom In";
            this.tsbtnZoomIn.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tsbtnZoomOut
            // 
            this.tsbtnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnZoomOut.Image")));
            this.tsbtnZoomOut.ImageTransparentColor = System.Drawing.Color.White;
            this.tsbtnZoomOut.Name = "tsbtnZoomOut";
            this.tsbtnZoomOut.Size = new System.Drawing.Size(23, 23);
            this.tsbtnZoomOut.Text = "toolStripButton2";
            this.tsbtnZoomOut.ToolTipText = "Zoom Out";
            this.tsbtnZoomOut.Click += new System.EventHandler(this.tsbtnZoomOut_Click);
            // 
            // tstxtZoomFactor
            // 
            this.tstxtZoomFactor.Name = "tstxtZoomFactor";
            this.tstxtZoomFactor.Size = new System.Drawing.Size(34, 26);
            this.tstxtZoomFactor.Leave += new System.EventHandler(this.tstxtZoomFactor_Leave);
            // 
            // toolStripFindReplace
            // 
            this.toolStripFindReplace.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripFindReplace.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tstxtSearchText,
            this.tsbtnFind,
            this.tsbtnReplace});
            this.toolStripFindReplace.Location = new System.Drawing.Point(0, 26);
            this.toolStripFindReplace.Name = "toolStripFindReplace";
            this.toolStripFindReplace.Size = new System.Drawing.Size(778, 25);
            this.toolStripFindReplace.TabIndex = 1;
            this.toolStripFindReplace.Text = "toolStrip2";
            // 
            // tstxtSearchText
            // 
            this.tstxtSearchText.Name = "tstxtSearchText";
            this.tstxtSearchText.Size = new System.Drawing.Size(116, 25);
            // 
            // tsbtnFind
            // 
            this.tsbtnFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnFind.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnFind.Image")));
            this.tsbtnFind.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnFind.Name = "tsbtnFind";
            this.tsbtnFind.Size = new System.Drawing.Size(23, 22);
            this.tsbtnFind.Text = "toolStripButton1";
            this.tsbtnFind.ToolTipText = "Find";
            this.tsbtnFind.Click += new System.EventHandler(this.tsbtnFind_Click);
            // 
            // tsbtnReplace
            // 
            this.tsbtnReplace.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnReplace.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnReplace.Image")));
            this.tsbtnReplace.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbtnReplace.Name = "tsbtnReplace";
            this.tsbtnReplace.Size = new System.Drawing.Size(23, 22);
            this.tsbtnReplace.Text = "toolStripButton2";
            this.tsbtnReplace.ToolTipText = "Replace";
            this.tsbtnReplace.Click += new System.EventHandler(this.tsbtnReplace_Click);
            // 
            // TYRicherTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rtbDocument);
            this.Controls.Add(this.toolStripFindReplace);
            this.Controls.Add(this.toolStrip1);
            this.Name = "TYRicherTextBox";
            this.Size = new System.Drawing.Size(778, 289);
            this.Load += new System.EventHandler(this.RicherTextBox_Load);
            this.contextMenu.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStripFindReplace.ResumeLayout(false);
            this.toolStripFindReplace.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbDocument;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox tscmbFont;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbtnBold;
        private System.Windows.Forms.ToolStripButton tsbtnItalic;
        private System.Windows.Forms.ToolStripButton tsbtnUnderline;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbtnAlignLeft;
        private System.Windows.Forms.ToolStripButton tsbtnAlignCenter;
        private System.Windows.Forms.ToolStripButton tsbtnAlignRight;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbtnFontColor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbtnIndent;
        private System.Windows.Forms.ToolStripButton tsbtnBullets;
        private System.Windows.Forms.ToolStripButton tsbtnOutdent;
        private System.Windows.Forms.ToolStripComboBox tscmbFontSize;
        private System.Windows.Forms.ToolStripButton tsbtnChooseFont;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbtnInsertPicture;
        private System.Windows.Forms.ToolStripButton tsbtnSave;
        private System.Windows.Forms.ToolStripButton tsbtnOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton tsbtnZoomIn;
        private System.Windows.Forms.ToolStripButton tsbtnZoomOut;
        private System.Windows.Forms.ToolStripTextBox tstxtZoomFactor;
        private System.Windows.Forms.ToolStripButton tsbtnWordWrap;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem alignmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem centerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem styleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem boldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem italicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem underlineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indentationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem increaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem decreaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bulletsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem insertPictureToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem zoomInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomOuToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStripFindReplace;
        private System.Windows.Forms.ToolStripTextBox tstxtSearchText;
        private System.Windows.Forms.ToolStripButton tsbtnFind;
        private System.Windows.Forms.ToolStripButton tsbtnReplace;
    }
}
