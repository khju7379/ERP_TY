namespace TY.ER.HR00
{
    partial class TYHRSJ002B
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
            this.DTP01_GDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_GDATE = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.CBO01_GGUBUN = new TY.Service.Library.Controls.TYComboBox();
            this.LBL51_GGUBUN = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_BATCH
            // 
            this.BTN61_BATCH.FactoryID = "";
            this.BTN61_BATCH.FactoryName = null;
            this.BTN61_BATCH.Location = new System.Drawing.Point(133, 101);
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
            this.BTN61_CLO.Location = new System.Drawing.Point(214, 101);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 1;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // DTP01_GDATE
            // 
            this.DTP01_GDATE.FactoryID = "";
            this.DTP01_GDATE.FactoryName = null;
            this.DTP01_GDATE.Location = new System.Drawing.Point(127, 34);
            this.DTP01_GDATE.Name = "DTP01_GDATE";
            this.DTP01_GDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_GDATE.TabIndex = 4;
            // 
            // LBL51_GDATE
            // 
            this.LBL51_GDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GDATE.FactoryID = "";
            this.LBL51_GDATE.FactoryName = null;
            this.LBL51_GDATE.IsCreated = false;
            this.LBL51_GDATE.Location = new System.Drawing.Point(21, 34);
            this.LBL51_GDATE.Name = "LBL51_GDATE";
            this.LBL51_GDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GDATE.TabIndex = 5;
            this.LBL51_GDATE.Text = "작업 년월";
            this.LBL51_GDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.CBO01_GGUBUN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GGUBUN);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_BATCH);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_GDATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GDATE);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(431, 143);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // CBO01_GGUBUN
            // 
            this.CBO01_GGUBUN.FactoryID = "";
            this.CBO01_GGUBUN.FactoryName = null;
            this.CBO01_GGUBUN.Location = new System.Drawing.Point(127, 61);
            this.CBO01_GGUBUN.Name = "CBO01_GGUBUN";
            this.CBO01_GGUBUN.Size = new System.Drawing.Size(140, 20);
            this.CBO01_GGUBUN.TabIndex = 52;
            // 
            // LBL51_GGUBUN
            // 
            this.LBL51_GGUBUN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GGUBUN.FactoryID = "";
            this.LBL51_GGUBUN.FactoryName = null;
            this.LBL51_GGUBUN.IsCreated = false;
            this.LBL51_GGUBUN.Location = new System.Drawing.Point(21, 61);
            this.LBL51_GGUBUN.Name = "LBL51_GGUBUN";
            this.LBL51_GGUBUN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GGUBUN.TabIndex = 51;
            this.LBL51_GGUBUN.Text = "작업 구분";
            this.LBL51_GGUBUN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TYHRSJ002B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 145);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRSJ002B";
            this.Load += new System.EventHandler(this.TYHRSJ002B_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_BATCH;
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYDatePicker DTP01_GDATE;
        private TY.Service.Library.Controls.TYLabel LBL51_GDATE;
        private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYLabel LBL51_GGUBUN;
        private Service.Library.Controls.TYComboBox CBO01_GGUBUN;
    }
}