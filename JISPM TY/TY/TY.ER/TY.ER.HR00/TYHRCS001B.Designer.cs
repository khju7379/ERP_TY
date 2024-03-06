namespace TY.ER.HR00
{
    partial class TYHRCS001B
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
            this.BTN61_BATCH = new TY.Service.Library.Controls.TYButton();
            this.BTN61_CLO = new TY.Service.Library.Controls.TYButton();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.LBL51_GGUBUN = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_G2GUBN = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_STDATE = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_EDDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.DTP01_STDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.CBO01_GGUBUN = new TY.Service.Library.Controls.TYComboBox();
            this.CBO01_G2GUBN = new TY.Service.Library.Controls.TYComboBox();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_BATCH
            // 
            this.BTN61_BATCH.FactoryID = "";
            this.BTN61_BATCH.FactoryName = null;
            this.BTN61_BATCH.Location = new System.Drawing.Point(137, 121);
            this.BTN61_BATCH.Name = "BTN61_BATCH";
            this.BTN61_BATCH.Size = new System.Drawing.Size(75, 21);
            this.BTN61_BATCH.TabIndex = 0;
            this.BTN61_BATCH.Text = "처리";
            this.BTN61_BATCH.UseVisualStyleBackColor = true;
            this.BTN61_BATCH.Click += new System.EventHandler(this.BTN61_BATCH_Click);
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(218, 121);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 1;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.CBO01_G2GUBN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GGUBUN);
            this.GBX80_CONTROLS.Controls.Add(this.CBO01_GGUBUN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_G2GUBN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_STDATE);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_EDDATE);
            this.GBX80_CONTROLS.Controls.Add(this.label2);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_STDATE);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_BATCH);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(431, 171);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // LBL51_GGUBUN
            // 
            this.LBL51_GGUBUN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GGUBUN.FactoryID = "";
            this.LBL51_GGUBUN.FactoryName = null;
            this.LBL51_GGUBUN.IsCreated = false;
            this.LBL51_GGUBUN.Location = new System.Drawing.Point(21, 85);
            this.LBL51_GGUBUN.Name = "LBL51_GGUBUN";
            this.LBL51_GGUBUN.Size = new System.Drawing.Size(110, 21);
            this.LBL51_GGUBUN.TabIndex = 50;
            this.LBL51_GGUBUN.Text = "작업 구분";
            this.LBL51_GGUBUN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_G2GUBN
            // 
            this.LBL51_G2GUBN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_G2GUBN.FactoryID = "";
            this.LBL51_G2GUBN.FactoryName = null;
            this.LBL51_G2GUBN.IsCreated = false;
            this.LBL51_G2GUBN.Location = new System.Drawing.Point(21, 58);
            this.LBL51_G2GUBN.Name = "LBL51_G2GUBN";
            this.LBL51_G2GUBN.Size = new System.Drawing.Size(110, 21);
            this.LBL51_G2GUBN.TabIndex = 28;
            this.LBL51_G2GUBN.Text = "항목 구분";
            this.LBL51_G2GUBN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_STDATE
            // 
            this.LBL51_STDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_STDATE.FactoryID = "";
            this.LBL51_STDATE.FactoryName = null;
            this.LBL51_STDATE.IsCreated = false;
            this.LBL51_STDATE.Location = new System.Drawing.Point(21, 32);
            this.LBL51_STDATE.Name = "LBL51_STDATE";
            this.LBL51_STDATE.Size = new System.Drawing.Size(110, 21);
            this.LBL51_STDATE.TabIndex = 25;
            this.LBL51_STDATE.Text = "작업 일자";
            this.LBL51_STDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_EDDATE
            // 
            this.DTP01_EDDATE.FactoryID = "";
            this.DTP01_EDDATE.FactoryName = null;
            this.DTP01_EDDATE.Location = new System.Drawing.Point(263, 32);
            this.DTP01_EDDATE.Name = "DTP01_EDDATE";
            this.DTP01_EDDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_EDDATE.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(243, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 12);
            this.label2.TabIndex = 23;
            this.label2.Text = "~";
            // 
            // DTP01_STDATE
            // 
            this.DTP01_STDATE.FactoryID = "";
            this.DTP01_STDATE.FactoryName = null;
            this.DTP01_STDATE.Location = new System.Drawing.Point(137, 32);
            this.DTP01_STDATE.Name = "DTP01_STDATE";
            this.DTP01_STDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_STDATE.TabIndex = 22;
            // 
            // CBO01_GGUBUN
            // 
            this.CBO01_GGUBUN.FactoryID = "";
            this.CBO01_GGUBUN.FactoryName = null;
            this.CBO01_GGUBUN.Location = new System.Drawing.Point(137, 85);
            this.CBO01_GGUBUN.Name = "CBO01_GGUBUN";
            this.CBO01_GGUBUN.Size = new System.Drawing.Size(150, 20);
            this.CBO01_GGUBUN.TabIndex = 49;
            // 
            // CBO01_G2GUBN
            // 
            this.CBO01_G2GUBN.FactoryID = "";
            this.CBO01_G2GUBN.FactoryName = null;
            this.CBO01_G2GUBN.Location = new System.Drawing.Point(137, 59);
            this.CBO01_G2GUBN.Name = "CBO01_G2GUBN";
            this.CBO01_G2GUBN.Size = new System.Drawing.Size(150, 20);
            this.CBO01_G2GUBN.TabIndex = 51;
            // 
            // TYHRCS001B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 173);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRCS001B";
            this.Load += new System.EventHandler(this.TYHRCS001B_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_BATCH;
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYLabel LBL51_STDATE;
        private Service.Library.Controls.TYDatePicker DTP01_EDDATE;
        private System.Windows.Forms.Label label2;
        private Service.Library.Controls.TYDatePicker DTP01_STDATE;
        private Service.Library.Controls.TYLabel LBL51_G2GUBN;
        private Service.Library.Controls.TYLabel LBL51_GGUBUN;
        private Service.Library.Controls.TYComboBox CBO01_G2GUBN;
        private Service.Library.Controls.TYComboBox CBO01_GGUBUN;
    }
}