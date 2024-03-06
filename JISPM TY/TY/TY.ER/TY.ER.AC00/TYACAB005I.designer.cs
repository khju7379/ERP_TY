namespace TY.ER.AC00
{
    partial class TYACAB005I
    {
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
            this.BTN61_CLO = new TY.Service.Library.Controls.TYButton();
            this.BTN61_SAV = new TY.Service.Library.Controls.TYButton();
            this.TXT01_A3CDFD = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_A3CDFD = new TY.Service.Library.Controls.TYLabel();
            this.GRP01_SEARCH = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TXT01_A3NMFD = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_A3NMFD = new TY.Service.Library.Controls.TYLabel();
            this.CBH01_A3FDHL1 = new TY.Service.Library.Controls.TYCodeBox();
            this.TXT01_A3ABFD = new TY.Service.Library.Controls.TYTextBox();
            this.CBH01_A3FDHL2 = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_A3FDHL3 = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_A3FDHL1 = new TY.Service.Library.Controls.TYLabel();
            this.CBH01_A3FDHL3 = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_A3FDHL2 = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_A3ABFD = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_A3YNSL = new TY.Service.Library.Controls.TYLabel();
            this.CKB01_A3YNSL = new TY.Service.Library.Controls.TYCheckBox();
            this.CBO01_A3IDPL = new TY.Service.Library.Controls.TYComboBox();
            this.LBL51_A3IDPL = new TY.Service.Library.Controls.TYLabel();
            this.GRP01_SEARCH.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(453, 12);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 0;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // BTN61_SAV
            // 
            this.BTN61_SAV.FactoryID = "";
            this.BTN61_SAV.FactoryName = null;
            this.BTN61_SAV.Location = new System.Drawing.Point(372, 12);
            this.BTN61_SAV.Name = "BTN61_SAV";
            this.BTN61_SAV.Size = new System.Drawing.Size(75, 21);
            this.BTN61_SAV.TabIndex = 1;
            this.BTN61_SAV.Text = "저장";
            this.BTN61_SAV.UseVisualStyleBackColor = true;
            this.BTN61_SAV.Click += new System.EventHandler(this.BTN61_SAV_Click);
            // 
            // TXT01_A3CDFD
            // 
            this.TXT01_A3CDFD.FactoryID = "";
            this.TXT01_A3CDFD.FactoryName = null;
            this.TXT01_A3CDFD.Location = new System.Drawing.Point(111, 12);
            this.TXT01_A3CDFD.MaxLength = 5;
            this.TXT01_A3CDFD.MinLength = 0;
            this.TXT01_A3CDFD.Name = "TXT01_A3CDFD";
            this.TXT01_A3CDFD.Size = new System.Drawing.Size(100, 21);
            this.TXT01_A3CDFD.TabIndex = 14;
            // 
            // LBL51_A3CDFD
            // 
            this.LBL51_A3CDFD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_A3CDFD.FactoryID = "";
            this.LBL51_A3CDFD.FactoryName = null;
            this.LBL51_A3CDFD.ForeColor = System.Drawing.Color.White;
            this.LBL51_A3CDFD.Location = new System.Drawing.Point(5, 12);
            this.LBL51_A3CDFD.Name = "LBL51_A3CDFD";
            this.LBL51_A3CDFD.Size = new System.Drawing.Size(100, 21);
            this.LBL51_A3CDFD.TabIndex = 15;
            this.LBL51_A3CDFD.Text = "자금항목코드";
            this.LBL51_A3CDFD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GRP01_SEARCH
            // 
            this.GRP01_SEARCH.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GRP01_SEARCH.Controls.Add(this.panel1);
            this.GRP01_SEARCH.Controls.Add(this.BTN61_CLO);
            this.GRP01_SEARCH.Controls.Add(this.BTN61_SAV);
            this.GRP01_SEARCH.Controls.Add(this.TXT01_A3CDFD);
            this.GRP01_SEARCH.Controls.Add(this.LBL51_A3CDFD);
            this.GRP01_SEARCH.Location = new System.Drawing.Point(2, 1);
            this.GRP01_SEARCH.Name = "GRP01_SEARCH";
            this.GRP01_SEARCH.Size = new System.Drawing.Size(537, 238);
            this.GRP01_SEARCH.TabIndex = 20;
            this.GRP01_SEARCH.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.TXT01_A3NMFD);
            this.panel1.Controls.Add(this.LBL51_A3NMFD);
            this.panel1.Controls.Add(this.CBH01_A3FDHL1);
            this.panel1.Controls.Add(this.TXT01_A3ABFD);
            this.panel1.Controls.Add(this.CBH01_A3FDHL2);
            this.panel1.Controls.Add(this.LBL51_A3FDHL3);
            this.panel1.Controls.Add(this.LBL51_A3FDHL1);
            this.panel1.Controls.Add(this.CBH01_A3FDHL3);
            this.panel1.Controls.Add(this.LBL51_A3FDHL2);
            this.panel1.Controls.Add(this.LBL51_A3ABFD);
            this.panel1.Controls.Add(this.LBL51_A3YNSL);
            this.panel1.Controls.Add(this.CKB01_A3YNSL);
            this.panel1.Controls.Add(this.CBO01_A3IDPL);
            this.panel1.Controls.Add(this.LBL51_A3IDPL);
            this.panel1.Location = new System.Drawing.Point(5, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(526, 187);
            this.panel1.TabIndex = 16;
            // 
            // TXT01_A3NMFD
            // 
            this.TXT01_A3NMFD.FactoryID = "";
            this.TXT01_A3NMFD.FactoryName = null;
            this.TXT01_A3NMFD.Location = new System.Drawing.Point(106, 0);
            this.TXT01_A3NMFD.MaxLength = 40;
            this.TXT01_A3NMFD.MinLength = 0;
            this.TXT01_A3NMFD.Name = "TXT01_A3NMFD";
            this.TXT01_A3NMFD.Size = new System.Drawing.Size(300, 21);
            this.TXT01_A3NMFD.TabIndex = 46;
            // 
            // LBL51_A3NMFD
            // 
            this.LBL51_A3NMFD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_A3NMFD.FactoryID = "";
            this.LBL51_A3NMFD.FactoryName = null;
            this.LBL51_A3NMFD.ForeColor = System.Drawing.Color.White;
            this.LBL51_A3NMFD.Location = new System.Drawing.Point(0, 0);
            this.LBL51_A3NMFD.Name = "LBL51_A3NMFD";
            this.LBL51_A3NMFD.Size = new System.Drawing.Size(100, 21);
            this.LBL51_A3NMFD.TabIndex = 47;
            this.LBL51_A3NMFD.Text = "자금항목명";
            this.LBL51_A3NMFD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBH01_A3FDHL1
            // 
            this.CBH01_A3FDHL1.BindedDataRow = null;
            this.CBH01_A3FDHL1.CodeBoxWidth = 0;
            this.CBH01_A3FDHL1.DummyValue = null;
            this.CBH01_A3FDHL1.FactoryID = "";
            this.CBH01_A3FDHL1.FactoryName = null;
            this.CBH01_A3FDHL1.Location = new System.Drawing.Point(106, 107);
            this.CBH01_A3FDHL1.MaxLength = 5;
            this.CBH01_A3FDHL1.MinLength = 0;
            this.CBH01_A3FDHL1.Name = "CBH01_A3FDHL1";
            this.CBH01_A3FDHL1.Size = new System.Drawing.Size(200, 20);
            this.CBH01_A3FDHL1.TabIndex = 34;
            // 
            // TXT01_A3ABFD
            // 
            this.TXT01_A3ABFD.FactoryID = "";
            this.TXT01_A3ABFD.FactoryName = null;
            this.TXT01_A3ABFD.Location = new System.Drawing.Point(106, 27);
            this.TXT01_A3ABFD.MaxLength = 20;
            this.TXT01_A3ABFD.MinLength = 0;
            this.TXT01_A3ABFD.Name = "TXT01_A3ABFD";
            this.TXT01_A3ABFD.Size = new System.Drawing.Size(200, 21);
            this.TXT01_A3ABFD.TabIndex = 44;
            // 
            // CBH01_A3FDHL2
            // 
            this.CBH01_A3FDHL2.BindedDataRow = null;
            this.CBH01_A3FDHL2.CodeBoxWidth = 0;
            this.CBH01_A3FDHL2.DummyValue = null;
            this.CBH01_A3FDHL2.FactoryID = "";
            this.CBH01_A3FDHL2.FactoryName = null;
            this.CBH01_A3FDHL2.Location = new System.Drawing.Point(106, 133);
            this.CBH01_A3FDHL2.MaxLength = 5;
            this.CBH01_A3FDHL2.MinLength = 0;
            this.CBH01_A3FDHL2.Name = "CBH01_A3FDHL2";
            this.CBH01_A3FDHL2.Size = new System.Drawing.Size(200, 20);
            this.CBH01_A3FDHL2.TabIndex = 36;
            // 
            // LBL51_A3FDHL3
            // 
            this.LBL51_A3FDHL3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_A3FDHL3.FactoryID = "";
            this.LBL51_A3FDHL3.FactoryName = null;
            this.LBL51_A3FDHL3.ForeColor = System.Drawing.Color.White;
            this.LBL51_A3FDHL3.Location = new System.Drawing.Point(0, 159);
            this.LBL51_A3FDHL3.Name = "LBL51_A3FDHL3";
            this.LBL51_A3FDHL3.Size = new System.Drawing.Size(100, 21);
            this.LBL51_A3FDHL3.TabIndex = 39;
            this.LBL51_A3FDHL3.Text = "상위항목코드３";
            this.LBL51_A3FDHL3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_A3FDHL1
            // 
            this.LBL51_A3FDHL1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_A3FDHL1.FactoryID = "";
            this.LBL51_A3FDHL1.FactoryName = null;
            this.LBL51_A3FDHL1.ForeColor = System.Drawing.Color.White;
            this.LBL51_A3FDHL1.Location = new System.Drawing.Point(0, 107);
            this.LBL51_A3FDHL1.Name = "LBL51_A3FDHL1";
            this.LBL51_A3FDHL1.Size = new System.Drawing.Size(100, 21);
            this.LBL51_A3FDHL1.TabIndex = 35;
            this.LBL51_A3FDHL1.Text = "상위항목코드１";
            this.LBL51_A3FDHL1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBH01_A3FDHL3
            // 
            this.CBH01_A3FDHL3.BindedDataRow = null;
            this.CBH01_A3FDHL3.CodeBoxWidth = 0;
            this.CBH01_A3FDHL3.DummyValue = null;
            this.CBH01_A3FDHL3.FactoryID = "";
            this.CBH01_A3FDHL3.FactoryName = null;
            this.CBH01_A3FDHL3.Location = new System.Drawing.Point(106, 159);
            this.CBH01_A3FDHL3.MaxLength = 5;
            this.CBH01_A3FDHL3.MinLength = 0;
            this.CBH01_A3FDHL3.Name = "CBH01_A3FDHL3";
            this.CBH01_A3FDHL3.Size = new System.Drawing.Size(200, 20);
            this.CBH01_A3FDHL3.TabIndex = 38;
            // 
            // LBL51_A3FDHL2
            // 
            this.LBL51_A3FDHL2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_A3FDHL2.FactoryID = "";
            this.LBL51_A3FDHL2.FactoryName = null;
            this.LBL51_A3FDHL2.ForeColor = System.Drawing.Color.White;
            this.LBL51_A3FDHL2.Location = new System.Drawing.Point(0, 133);
            this.LBL51_A3FDHL2.Name = "LBL51_A3FDHL2";
            this.LBL51_A3FDHL2.Size = new System.Drawing.Size(100, 21);
            this.LBL51_A3FDHL2.TabIndex = 37;
            this.LBL51_A3FDHL2.Text = "상위항목코드２";
            this.LBL51_A3FDHL2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_A3ABFD
            // 
            this.LBL51_A3ABFD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_A3ABFD.FactoryID = "";
            this.LBL51_A3ABFD.FactoryName = null;
            this.LBL51_A3ABFD.ForeColor = System.Drawing.Color.White;
            this.LBL51_A3ABFD.Location = new System.Drawing.Point(0, 27);
            this.LBL51_A3ABFD.Name = "LBL51_A3ABFD";
            this.LBL51_A3ABFD.Size = new System.Drawing.Size(100, 21);
            this.LBL51_A3ABFD.TabIndex = 45;
            this.LBL51_A3ABFD.Text = "자금항목약명";
            this.LBL51_A3ABFD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_A3YNSL
            // 
            this.LBL51_A3YNSL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_A3YNSL.FactoryID = "";
            this.LBL51_A3YNSL.FactoryName = null;
            this.LBL51_A3YNSL.ForeColor = System.Drawing.Color.White;
            this.LBL51_A3YNSL.Location = new System.Drawing.Point(0, 54);
            this.LBL51_A3YNSL.Name = "LBL51_A3YNSL";
            this.LBL51_A3YNSL.Size = new System.Drawing.Size(100, 21);
            this.LBL51_A3YNSL.TabIndex = 43;
            this.LBL51_A3YNSL.Text = "전표발생단위Y/N";
            this.LBL51_A3YNSL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CKB01_A3YNSL
            // 
            this.CKB01_A3YNSL.FactoryID = "";
            this.CKB01_A3YNSL.FactoryName = null;
            this.CKB01_A3YNSL.Location = new System.Drawing.Point(106, 54);
            this.CKB01_A3YNSL.Name = "CKB01_A3YNSL";
            this.CKB01_A3YNSL.Size = new System.Drawing.Size(100, 21);
            this.CKB01_A3YNSL.TabIndex = 42;
            // 
            // CBO01_A3IDPL
            // 
            this.CBO01_A3IDPL.FactoryID = "";
            this.CBO01_A3IDPL.FactoryName = null;
            this.CBO01_A3IDPL.Location = new System.Drawing.Point(106, 81);
            this.CBO01_A3IDPL.Name = "CBO01_A3IDPL";
            this.CBO01_A3IDPL.Size = new System.Drawing.Size(100, 20);
            this.CBO01_A3IDPL.TabIndex = 40;
            // 
            // LBL51_A3IDPL
            // 
            this.LBL51_A3IDPL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_A3IDPL.FactoryID = "";
            this.LBL51_A3IDPL.FactoryName = null;
            this.LBL51_A3IDPL.ForeColor = System.Drawing.Color.White;
            this.LBL51_A3IDPL.Location = new System.Drawing.Point(0, 81);
            this.LBL51_A3IDPL.Name = "LBL51_A3IDPL";
            this.LBL51_A3IDPL.Size = new System.Drawing.Size(100, 21);
            this.LBL51_A3IDPL.TabIndex = 41;
            this.LBL51_A3IDPL.Text = "LEVEL";
            this.LBL51_A3IDPL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TYACAB005I
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 242);
            this.Controls.Add(this.GRP01_SEARCH);
            this.Name = "TYACAB005I";
            this.Load += new System.EventHandler(this.TYACAB005I_Load);
            this.GRP01_SEARCH.ResumeLayout(false);
            this.GRP01_SEARCH.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYButton BTN61_SAV;
        private TY.Service.Library.Controls.TYTextBox TXT01_A3CDFD;
        private TY.Service.Library.Controls.TYLabel LBL51_A3CDFD;
        private System.Windows.Forms.GroupBox GRP01_SEARCH;
        private System.Windows.Forms.Panel panel1;
        private Service.Library.Controls.TYTextBox TXT01_A3NMFD;
        private Service.Library.Controls.TYLabel LBL51_A3NMFD;
        private Service.Library.Controls.TYCodeBox CBH01_A3FDHL1;
        private Service.Library.Controls.TYTextBox TXT01_A3ABFD;
        private Service.Library.Controls.TYCodeBox CBH01_A3FDHL2;
        private Service.Library.Controls.TYLabel LBL51_A3FDHL3;
        private Service.Library.Controls.TYLabel LBL51_A3FDHL1;
        private Service.Library.Controls.TYCodeBox CBH01_A3FDHL3;
        private Service.Library.Controls.TYLabel LBL51_A3FDHL2;
        private Service.Library.Controls.TYLabel LBL51_A3ABFD;
        private Service.Library.Controls.TYLabel LBL51_A3YNSL;
        private Service.Library.Controls.TYCheckBox CKB01_A3YNSL;
        private Service.Library.Controls.TYComboBox CBO01_A3IDPL;
        private Service.Library.Controls.TYLabel LBL51_A3IDPL;
    }
}