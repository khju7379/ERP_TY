namespace TY.ER.AC00
{
    partial class TYACAB007I
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
            this.CBH01_CDCODE1 = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_CDCODE1 = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_CDCODE2 = new TY.Service.Library.Controls.TYTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TXT01_CDDESC1 = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_CDDESC1 = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_CDDESC2 = new TY.Service.Library.Controls.TYTextBox();
            this.TXT01_CDBIGO = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_CDDESC2 = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_CDBIGO = new TY.Service.Library.Controls.TYLabel();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(497, 12);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 0;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // BTN61_SAV
            // 
            this.BTN61_SAV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_SAV.FactoryID = "";
            this.BTN61_SAV.FactoryName = null;
            this.BTN61_SAV.Location = new System.Drawing.Point(416, 12);
            this.BTN61_SAV.Name = "BTN61_SAV";
            this.BTN61_SAV.Size = new System.Drawing.Size(75, 21);
            this.BTN61_SAV.TabIndex = 1;
            this.BTN61_SAV.Text = "저장";
            this.BTN61_SAV.UseVisualStyleBackColor = true;
            this.BTN61_SAV.Click += new System.EventHandler(this.BTN61_SAV_Click);
            // 
            // CBH01_CDCODE1
            // 
            this.CBH01_CDCODE1.BindedDataRow = null;
            this.CBH01_CDCODE1.CodeBoxWidth = 0;
            this.CBH01_CDCODE1.DummyValue = null;
            this.CBH01_CDCODE1.FactoryID = "";
            this.CBH01_CDCODE1.FactoryName = null;
            this.CBH01_CDCODE1.Location = new System.Drawing.Point(111, 12);
            this.CBH01_CDCODE1.MinLength = 0;
            this.CBH01_CDCODE1.Name = "CBH01_CDCODE1";
            this.CBH01_CDCODE1.Size = new System.Drawing.Size(230, 20);
            this.CBH01_CDCODE1.TabIndex = 2;
            // 
            // LBL51_CDCODE1
            // 
            this.LBL51_CDCODE1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_CDCODE1.FactoryID = "";
            this.LBL51_CDCODE1.FactoryName = null;
            this.LBL51_CDCODE1.ForeColor = System.Drawing.Color.White;
            this.LBL51_CDCODE1.Location = new System.Drawing.Point(5, 12);
            this.LBL51_CDCODE1.Name = "LBL51_CDCODE1";
            this.LBL51_CDCODE1.Size = new System.Drawing.Size(100, 21);
            this.LBL51_CDCODE1.TabIndex = 3;
            this.LBL51_CDCODE1.Text = "코드1";
            this.LBL51_CDCODE1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_CDCODE2
            // 
            this.TXT01_CDCODE2.FactoryID = "";
            this.TXT01_CDCODE2.FactoryName = null;
            this.TXT01_CDCODE2.Location = new System.Drawing.Point(357, 12);
            this.TXT01_CDCODE2.MinLength = 0;
            this.TXT01_CDCODE2.Name = "TXT01_CDCODE2";
            this.TXT01_CDCODE2.Size = new System.Drawing.Size(52, 21);
            this.TXT01_CDCODE2.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.BTN61_CLO);
            this.groupBox1.Controls.Add(this.BTN61_SAV);
            this.groupBox1.Controls.Add(this.CBH01_CDCODE1);
            this.groupBox1.Controls.Add(this.LBL51_CDCODE1);
            this.groupBox1.Controls.Add(this.TXT01_CDCODE2);
            this.groupBox1.Location = new System.Drawing.Point(2, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(578, 134);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.TXT01_CDDESC1);
            this.panel1.Controls.Add(this.LBL51_CDDESC1);
            this.panel1.Controls.Add(this.TXT01_CDDESC2);
            this.panel1.Controls.Add(this.TXT01_CDBIGO);
            this.panel1.Controls.Add(this.LBL51_CDDESC2);
            this.panel1.Controls.Add(this.LBL51_CDBIGO);
            this.panel1.Location = new System.Drawing.Point(5, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(567, 83);
            this.panel1.TabIndex = 9;
            // 
            // TXT01_CDDESC1
            // 
            this.TXT01_CDDESC1.FactoryID = "";
            this.TXT01_CDDESC1.FactoryName = null;
            this.TXT01_CDDESC1.Location = new System.Drawing.Point(106, 0);
            this.TXT01_CDDESC1.MinLength = 0;
            this.TXT01_CDDESC1.Name = "TXT01_CDDESC1";
            this.TXT01_CDDESC1.Size = new System.Drawing.Size(450, 21);
            this.TXT01_CDDESC1.TabIndex = 16;
            // 
            // LBL51_CDDESC1
            // 
            this.LBL51_CDDESC1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_CDDESC1.FactoryID = "";
            this.LBL51_CDDESC1.FactoryName = null;
            this.LBL51_CDDESC1.ForeColor = System.Drawing.Color.White;
            this.LBL51_CDDESC1.Location = new System.Drawing.Point(0, 0);
            this.LBL51_CDDESC1.Name = "LBL51_CDDESC1";
            this.LBL51_CDDESC1.Size = new System.Drawing.Size(100, 21);
            this.LBL51_CDDESC1.TabIndex = 17;
            this.LBL51_CDDESC1.Text = "내용1";
            this.LBL51_CDDESC1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_CDDESC2
            // 
            this.TXT01_CDDESC2.FactoryID = "";
            this.TXT01_CDDESC2.FactoryName = null;
            this.TXT01_CDDESC2.Location = new System.Drawing.Point(106, 27);
            this.TXT01_CDDESC2.MinLength = 0;
            this.TXT01_CDDESC2.Name = "TXT01_CDDESC2";
            this.TXT01_CDDESC2.Size = new System.Drawing.Size(450, 21);
            this.TXT01_CDDESC2.TabIndex = 18;
            // 
            // TXT01_CDBIGO
            // 
            this.TXT01_CDBIGO.FactoryID = "";
            this.TXT01_CDBIGO.FactoryName = null;
            this.TXT01_CDBIGO.Location = new System.Drawing.Point(106, 54);
            this.TXT01_CDBIGO.MinLength = 0;
            this.TXT01_CDBIGO.Name = "TXT01_CDBIGO";
            this.TXT01_CDBIGO.Size = new System.Drawing.Size(450, 21);
            this.TXT01_CDBIGO.TabIndex = 14;
            // 
            // LBL51_CDDESC2
            // 
            this.LBL51_CDDESC2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_CDDESC2.FactoryID = "";
            this.LBL51_CDDESC2.FactoryName = null;
            this.LBL51_CDDESC2.ForeColor = System.Drawing.Color.White;
            this.LBL51_CDDESC2.Location = new System.Drawing.Point(0, 27);
            this.LBL51_CDDESC2.Name = "LBL51_CDDESC2";
            this.LBL51_CDDESC2.Size = new System.Drawing.Size(100, 21);
            this.LBL51_CDDESC2.TabIndex = 19;
            this.LBL51_CDDESC2.Text = "내용2";
            this.LBL51_CDDESC2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_CDBIGO
            // 
            this.LBL51_CDBIGO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_CDBIGO.FactoryID = "";
            this.LBL51_CDBIGO.FactoryName = null;
            this.LBL51_CDBIGO.ForeColor = System.Drawing.Color.White;
            this.LBL51_CDBIGO.Location = new System.Drawing.Point(0, 54);
            this.LBL51_CDBIGO.Name = "LBL51_CDBIGO";
            this.LBL51_CDBIGO.Size = new System.Drawing.Size(100, 21);
            this.LBL51_CDBIGO.TabIndex = 15;
            this.LBL51_CDBIGO.Text = "비고";
            this.LBL51_CDBIGO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TYACAB007I
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 148);
            this.Controls.Add(this.groupBox1);
            this.Name = "TYACAB007I";
            this.Load += new System.EventHandler(this.TYACAB007I_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYButton BTN61_SAV;
        private TY.Service.Library.Controls.TYCodeBox CBH01_CDCODE1;
        private TY.Service.Library.Controls.TYLabel LBL51_CDCODE1;
        private TY.Service.Library.Controls.TYTextBox TXT01_CDCODE2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private Service.Library.Controls.TYTextBox TXT01_CDDESC1;
        private Service.Library.Controls.TYLabel LBL51_CDDESC1;
        private Service.Library.Controls.TYTextBox TXT01_CDDESC2;
        private Service.Library.Controls.TYTextBox TXT01_CDBIGO;
        private Service.Library.Controls.TYLabel LBL51_CDDESC2;
        private Service.Library.Controls.TYLabel LBL51_CDBIGO;
    }
}