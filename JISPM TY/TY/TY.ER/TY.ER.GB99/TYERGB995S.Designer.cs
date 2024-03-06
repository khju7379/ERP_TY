namespace TY.ER.GB99
{
    partial class TYERGB995S
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.MTB01_YYYYMM = new TY.Service.Library.Controls.TYMaskedTextBox();
            this.LBL51_CODE = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_CODE_NAME = new TY.Service.Library.Controls.TYLabel();
            this.BTN61_SAV = new TY.Service.Library.Controls.TYButton();
            this.TXT01_CODE_NAME = new TY.Service.Library.Controls.TYTextBox();
            this.TXT01_CODE = new TY.Service.Library.Controls.TYTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TRV01_MAIN = new TY.Service.Library.Controls.TYTreeView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.MTB01_YYYYMM);
            this.groupBox1.Controls.Add(this.LBL51_CODE);
            this.groupBox1.Controls.Add(this.LBL51_CODE_NAME);
            this.groupBox1.Controls.Add(this.BTN61_SAV);
            this.groupBox1.Controls.Add(this.TXT01_CODE_NAME);
            this.groupBox1.Controls.Add(this.TXT01_CODE);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1060, 57);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // MTB01_YYYYMM
            // 
            this.MTB01_YYYYMM.FactoryID = "";
            this.MTB01_YYYYMM.FactoryName = null;
            this.MTB01_YYYYMM.Location = new System.Drawing.Point(511, 20);
            this.MTB01_YYYYMM.Mask = "0000-00-00";
            this.MTB01_YYYYMM.Name = "MTB01_YYYYMM";
            this.MTB01_YYYYMM.Size = new System.Drawing.Size(100, 21);
            this.MTB01_YYYYMM.TabIndex = 5;
            this.MTB01_YYYYMM.ValidatingType = typeof(System.DateTime);
            // 
            // LBL51_CODE
            // 
            this.LBL51_CODE.BackColor = System.Drawing.Color.Silver;
            this.LBL51_CODE.FactoryID = "";
            this.LBL51_CODE.FactoryName = null;
            this.LBL51_CODE.Location = new System.Drawing.Point(6, 20);
            this.LBL51_CODE.Name = "LBL51_CODE";
            this.LBL51_CODE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_CODE.TabIndex = 4;
            this.LBL51_CODE.Text = "tyLabel2";
            // 
            // LBL51_CODE_NAME
            // 
            this.LBL51_CODE_NAME.BackColor = System.Drawing.Color.Silver;
            this.LBL51_CODE_NAME.FactoryID = "";
            this.LBL51_CODE_NAME.FactoryName = null;
            this.LBL51_CODE_NAME.Location = new System.Drawing.Point(218, 20);
            this.LBL51_CODE_NAME.Name = "LBL51_CODE_NAME";
            this.LBL51_CODE_NAME.Size = new System.Drawing.Size(100, 21);
            this.LBL51_CODE_NAME.TabIndex = 3;
            this.LBL51_CODE_NAME.Text = "tyLabel1";
            // 
            // BTN61_SAV
            // 
            this.BTN61_SAV.FactoryID = "";
            this.BTN61_SAV.FactoryName = null;
            this.BTN61_SAV.Location = new System.Drawing.Point(430, 18);
            this.BTN61_SAV.Name = "BTN61_SAV";
            this.BTN61_SAV.Size = new System.Drawing.Size(75, 23);
            this.BTN61_SAV.TabIndex = 2;
            this.BTN61_SAV.Text = "tyButton1";
            this.BTN61_SAV.UseVisualStyleBackColor = true;
            this.BTN61_SAV.Click += new System.EventHandler(this.BTN61_SAV_Click);
            // 
            // TXT01_CODE_NAME
            // 
            this.TXT01_CODE_NAME.FactoryID = "";
            this.TXT01_CODE_NAME.FactoryName = null;
            this.TXT01_CODE_NAME.Location = new System.Drawing.Point(324, 20);
            this.TXT01_CODE_NAME.MinLength = 0;
            this.TXT01_CODE_NAME.Name = "TXT01_CODE_NAME";
            this.TXT01_CODE_NAME.Size = new System.Drawing.Size(100, 21);
            this.TXT01_CODE_NAME.TabIndex = 1;
            this.TXT01_CODE_NAME.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TXT01_CODE_NAME_KeyDown);
            this.TXT01_CODE_NAME.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TXT01_CODE_NAME_KeyPress);
            this.TXT01_CODE_NAME.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.TXT01_CODE_NAME_PreviewKeyDown);
            // 
            // TXT01_CODE
            // 
            this.TXT01_CODE.FactoryID = "";
            this.TXT01_CODE.FactoryName = null;
            this.TXT01_CODE.Location = new System.Drawing.Point(112, 20);
            this.TXT01_CODE.MinLength = 0;
            this.TXT01_CODE.Name = "TXT01_CODE";
            this.TXT01_CODE.Size = new System.Drawing.Size(100, 21);
            this.TXT01_CODE.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.TRV01_MAIN);
            this.groupBox2.Location = new System.Drawing.Point(12, 75);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1060, 572);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // TRV01_MAIN
            // 
            this.TRV01_MAIN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TRV01_MAIN.FactoryID = "";
            this.TRV01_MAIN.FactoryName = null;
            this.TRV01_MAIN.ImageIndex = 0;
            this.TRV01_MAIN.Location = new System.Drawing.Point(3, 17);
            this.TRV01_MAIN.Name = "TRV01_MAIN";
            this.TRV01_MAIN.SelectedImageIndex = 0;
            this.TRV01_MAIN.Size = new System.Drawing.Size(1054, 552);
            this.TRV01_MAIN.TabIndex = 0;
            this.TRV01_MAIN.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TRV01_MAIN_MouseDoubleClick);
            // 
            // TYERGB995S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 659);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "TYERGB995S";
            this.Text = "TYERGB995S";
            this.Load += new System.EventHandler(this.TYERGB995S_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Service.Library.Controls.TYButton BTN61_SAV;
        private Service.Library.Controls.TYTextBox TXT01_CODE_NAME;
        private Service.Library.Controls.TYTextBox TXT01_CODE;
        private Service.Library.Controls.TYTreeView TRV01_MAIN;
        private Service.Library.Controls.TYLabel LBL51_CODE;
        private Service.Library.Controls.TYLabel LBL51_CODE_NAME;
        private Service.Library.Controls.TYMaskedTextBox MTB01_YYYYMM;
    }
}