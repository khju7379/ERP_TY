namespace TY.ER.HR00
{
    partial class TYHRPY05C2
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
            this.BTN61_SAV_BATCH = new TY.Service.Library.Controls.TYButton();
            this.LBL51_EDATE = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.DTP01_EDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(437, 12);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 0;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // BTN61_SAV_BATCH
            // 
            this.BTN61_SAV_BATCH.FactoryID = "";
            this.BTN61_SAV_BATCH.FactoryName = null;
            this.BTN61_SAV_BATCH.Location = new System.Drawing.Point(356, 12);
            this.BTN61_SAV_BATCH.Name = "BTN61_SAV_BATCH";
            this.BTN61_SAV_BATCH.Size = new System.Drawing.Size(75, 21);
            this.BTN61_SAV_BATCH.TabIndex = 1;
            this.BTN61_SAV_BATCH.Text = "일괄등록";
            this.BTN61_SAV_BATCH.UseVisualStyleBackColor = true;
            this.BTN61_SAV_BATCH.Click += new System.EventHandler(this.BTN61_SAV_BATCH_Click);
            // 
            // LBL51_EDATE
            // 
            this.LBL51_EDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_EDATE.FactoryID = "";
            this.LBL51_EDATE.FactoryName = null;
            this.LBL51_EDATE.IsCreated = false;
            this.LBL51_EDATE.Location = new System.Drawing.Point(5, 12);
            this.LBL51_EDATE.Name = "LBL51_EDATE";
            this.LBL51_EDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_EDATE.TabIndex = 3;
            this.LBL51_EDATE.Text = "종료일자";
            this.LBL51_EDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_EDATE);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_SAV_BATCH);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_EDATE);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(518, 48);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // DTP01_EDATE
            // 
            this.DTP01_EDATE.FactoryID = "";
            this.DTP01_EDATE.FactoryName = null;
            this.DTP01_EDATE.Location = new System.Drawing.Point(111, 12);
            this.DTP01_EDATE.Name = "DTP01_EDATE";
            this.DTP01_EDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_EDATE.TabIndex = 16;
            // 
            // TYHRPY05C2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 55);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRPY05C2";
            this.Load += new System.EventHandler(this.TYHRPY05C2_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYButton BTN61_SAV_BATCH;
        private TY.Service.Library.Controls.TYLabel LBL51_EDATE;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYDatePicker DTP01_EDATE;
    }
}