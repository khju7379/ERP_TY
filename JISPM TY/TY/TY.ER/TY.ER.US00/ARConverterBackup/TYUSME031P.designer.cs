namespace TY.ER.US00
{
    partial class TYUSME031P
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
            this.BTN61_PRT = new TY.Service.Library.Controls.TYButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BTN61_CLO = new TY.Service.Library.Controls.TYButton();
            this.LBL51_GDATE = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_GDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_GHWAJU = new TY.Service.Library.Controls.TYLabel();
            this.CBH01_GHWAJU = new TY.Service.Library.Controls.TYCodeBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_PRT
            // 
            this.BTN61_PRT.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BTN61_PRT.FactoryID = "";
            this.BTN61_PRT.FactoryName = null;
            this.BTN61_PRT.Location = new System.Drawing.Point(111, 74);
            this.BTN61_PRT.Name = "BTN61_PRT";
            this.BTN61_PRT.Size = new System.Drawing.Size(75, 21);
            this.BTN61_PRT.TabIndex = 2;
            this.BTN61_PRT.Text = "출력";
            this.BTN61_PRT.UseVisualStyleBackColor = true;
            this.BTN61_PRT.Click += new System.EventHandler(this.BTN61_PRT_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.LBL51_GHWAJU);
            this.groupBox1.Controls.Add(this.CBH01_GHWAJU);
            this.groupBox1.Controls.Add(this.BTN61_CLO);
            this.groupBox1.Controls.Add(this.LBL51_GDATE);
            this.groupBox1.Controls.Add(this.DTP01_GDATE);
            this.groupBox1.Controls.Add(this.BTN61_PRT);
            this.groupBox1.Location = new System.Drawing.Point(2, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(381, 113);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(192, 74);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 367;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // LBL51_GDATE
            // 
            this.LBL51_GDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_GDATE.FactoryID = "";
            this.LBL51_GDATE.FactoryName = null;
            this.LBL51_GDATE.ForeColor = System.Drawing.Color.White;
            this.LBL51_GDATE.IsCreated = false;
            this.LBL51_GDATE.Location = new System.Drawing.Point(5, 12);
            this.LBL51_GDATE.Name = "LBL51_GDATE";
            this.LBL51_GDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GDATE.TabIndex = 223;
            this.LBL51_GDATE.Text = "기준일자";
            this.LBL51_GDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_GDATE
            // 
            this.DTP01_GDATE.FactoryID = "";
            this.DTP01_GDATE.FactoryName = null;
            this.DTP01_GDATE.Location = new System.Drawing.Point(111, 12);
            this.DTP01_GDATE.Name = "DTP01_GDATE";
            this.DTP01_GDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_GDATE.TabIndex = 221;
            // 
            // LBL51_GHWAJU
            // 
            this.LBL51_GHWAJU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_GHWAJU.FactoryID = "";
            this.LBL51_GHWAJU.FactoryName = null;
            this.LBL51_GHWAJU.ForeColor = System.Drawing.Color.White;
            this.LBL51_GHWAJU.IsCreated = false;
            this.LBL51_GHWAJU.Location = new System.Drawing.Point(5, 39);
            this.LBL51_GHWAJU.Name = "LBL51_GHWAJU";
            this.LBL51_GHWAJU.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GHWAJU.TabIndex = 379;
            this.LBL51_GHWAJU.Text = "화주";
            this.LBL51_GHWAJU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBH01_GHWAJU
            // 
            this.CBH01_GHWAJU.BindedDataRow = null;
            this.CBH01_GHWAJU.CodeBoxWidth = 0;
            this.CBH01_GHWAJU.DummyValue = null;
            this.CBH01_GHWAJU.FactoryID = "";
            this.CBH01_GHWAJU.FactoryName = null;
            this.CBH01_GHWAJU.Location = new System.Drawing.Point(111, 39);
            this.CBH01_GHWAJU.MinLength = 0;
            this.CBH01_GHWAJU.Name = "CBH01_GHWAJU";
            this.CBH01_GHWAJU.Size = new System.Drawing.Size(250, 20);
            this.CBH01_GHWAJU.TabIndex = 378;
            // 
            // TYUSME031P
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 115);
            this.Controls.Add(this.groupBox1);
            this.Name = "TYUSME031P";
            this.Load += new System.EventHandler(this.TYUSME031P_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_PRT;
        private System.Windows.Forms.GroupBox groupBox1;
        private Service.Library.Controls.TYDatePicker DTP01_GDATE;
        private Service.Library.Controls.TYLabel LBL51_GDATE;
        private Service.Library.Controls.TYButton BTN61_CLO;
        private Service.Library.Controls.TYLabel LBL51_GHWAJU;
        private Service.Library.Controls.TYCodeBox CBH01_GHWAJU;
    }
}