namespace TY.ER.HR00
{
    partial class TYHRBS001B
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
            this.LBL51_BPMYEAR = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_BPMSTYYMM = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.TXT01_BPMNYRATE = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_BPMNYRATE = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_BPMDYRATE = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_BPMDYRATE = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_BPMYEAR = new TY.Service.Library.Controls.TYTextBox();
            this.DTP01_BPMSTYYMM = new TY.Service.Library.Controls.TYDatePicker();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_BATCH
            // 
            this.BTN61_BATCH.FactoryID = "";
            this.BTN61_BATCH.FactoryName = null;
            this.BTN61_BATCH.Location = new System.Drawing.Point(111, 131);
            this.BTN61_BATCH.Name = "BTN61_BATCH";
            this.BTN61_BATCH.Size = new System.Drawing.Size(75, 21);
            this.BTN61_BATCH.TabIndex = 0;
            this.BTN61_BATCH.Text = "처리";
            this.BTN61_BATCH.UseVisualStyleBackColor = true;
            this.BTN61_BATCH.Click += new System.EventHandler(this.BTN61_BATCH_Click);
            this.BTN61_BATCH.InvokerStart += new Shoveling2010.SmartClient.SystemUtility.Controls.TButton.CheckHandler(this.BTN61_BATCH_InvokerStart);
            this.BTN61_BATCH.InvokerEnd += new Shoveling2010.SmartClient.SystemUtility.Controls.TButton.CheckHandler(this.BTN61_BATCH_InvokerEnd);
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(192, 131);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 1;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // LBL51_BPMYEAR
            // 
            this.LBL51_BPMYEAR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_BPMYEAR.FactoryID = "";
            this.LBL51_BPMYEAR.FactoryName = null;
            this.LBL51_BPMYEAR.IsCreated = false;
            this.LBL51_BPMYEAR.Location = new System.Drawing.Point(5, 12);
            this.LBL51_BPMYEAR.Name = "LBL51_BPMYEAR";
            this.LBL51_BPMYEAR.Size = new System.Drawing.Size(100, 21);
            this.LBL51_BPMYEAR.TabIndex = 4;
            this.LBL51_BPMYEAR.Text = "년 도";
            this.LBL51_BPMYEAR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_BPMSTYYMM
            // 
            this.LBL51_BPMSTYYMM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_BPMSTYYMM.FactoryID = "";
            this.LBL51_BPMSTYYMM.FactoryName = null;
            this.LBL51_BPMSTYYMM.IsCreated = false;
            this.LBL51_BPMSTYYMM.Location = new System.Drawing.Point(5, 40);
            this.LBL51_BPMSTYYMM.Name = "LBL51_BPMSTYYMM";
            this.LBL51_BPMSTYYMM.Size = new System.Drawing.Size(100, 21);
            this.LBL51_BPMSTYYMM.TabIndex = 22;
            this.LBL51_BPMSTYYMM.Text = "기준급여년월";
            this.LBL51_BPMSTYYMM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_BPMNYRATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_BPMNYRATE);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_BPMDYRATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_BPMDYRATE);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_BPMYEAR);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_BPMSTYYMM);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_BATCH);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_BPMYEAR);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_BPMSTYYMM);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(358, 165);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // TXT01_BPMNYRATE
            // 
            this.TXT01_BPMNYRATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_BPMNYRATE.FactoryID = "";
            this.TXT01_BPMNYRATE.FactoryName = null;
            this.TXT01_BPMNYRATE.Location = new System.Drawing.Point(111, 94);
            this.TXT01_BPMNYRATE.MinLength = 0;
            this.TXT01_BPMNYRATE.Name = "TXT01_BPMNYRATE";
            this.TXT01_BPMNYRATE.Size = new System.Drawing.Size(50, 21);
            this.TXT01_BPMNYRATE.TabIndex = 46;
            this.TXT01_BPMNYRATE.TabIndexCustom = false;
            // 
            // LBL51_BPMNYRATE
            // 
            this.LBL51_BPMNYRATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_BPMNYRATE.FactoryID = "";
            this.LBL51_BPMNYRATE.FactoryName = null;
            this.LBL51_BPMNYRATE.IsCreated = false;
            this.LBL51_BPMNYRATE.Location = new System.Drawing.Point(5, 94);
            this.LBL51_BPMNYRATE.Name = "LBL51_BPMNYRATE";
            this.LBL51_BPMNYRATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_BPMNYRATE.TabIndex = 45;
            this.LBL51_BPMNYRATE.Text = "내년 인상율";
            this.LBL51_BPMNYRATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_BPMDYRATE
            // 
            this.TXT01_BPMDYRATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_BPMDYRATE.FactoryID = "";
            this.TXT01_BPMDYRATE.FactoryName = null;
            this.TXT01_BPMDYRATE.Location = new System.Drawing.Point(111, 67);
            this.TXT01_BPMDYRATE.MinLength = 0;
            this.TXT01_BPMDYRATE.Name = "TXT01_BPMDYRATE";
            this.TXT01_BPMDYRATE.Size = new System.Drawing.Size(50, 21);
            this.TXT01_BPMDYRATE.TabIndex = 44;
            this.TXT01_BPMDYRATE.TabIndexCustom = false;
            // 
            // LBL51_BPMDYRATE
            // 
            this.LBL51_BPMDYRATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_BPMDYRATE.FactoryID = "";
            this.LBL51_BPMDYRATE.FactoryName = null;
            this.LBL51_BPMDYRATE.IsCreated = false;
            this.LBL51_BPMDYRATE.Location = new System.Drawing.Point(5, 67);
            this.LBL51_BPMDYRATE.Name = "LBL51_BPMDYRATE";
            this.LBL51_BPMDYRATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_BPMDYRATE.TabIndex = 43;
            this.LBL51_BPMDYRATE.Text = "당해 인상율";
            this.LBL51_BPMDYRATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_BPMYEAR
            // 
            this.TXT01_BPMYEAR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_BPMYEAR.FactoryID = "";
            this.TXT01_BPMYEAR.FactoryName = null;
            this.TXT01_BPMYEAR.Location = new System.Drawing.Point(111, 12);
            this.TXT01_BPMYEAR.MinLength = 0;
            this.TXT01_BPMYEAR.Name = "TXT01_BPMYEAR";
            this.TXT01_BPMYEAR.Size = new System.Drawing.Size(50, 21);
            this.TXT01_BPMYEAR.TabIndex = 41;
            this.TXT01_BPMYEAR.TabIndexCustom = false;
            // 
            // DTP01_BPMSTYYMM
            // 
            this.DTP01_BPMSTYYMM.FactoryID = "";
            this.DTP01_BPMSTYYMM.FactoryName = null;
            this.DTP01_BPMSTYYMM.Location = new System.Drawing.Point(111, 40);
            this.DTP01_BPMSTYYMM.Name = "DTP01_BPMSTYYMM";
            this.DTP01_BPMSTYYMM.Size = new System.Drawing.Size(79, 21);
            this.DTP01_BPMSTYYMM.TabIndex = 34;
            // 
            // TYHRBS001B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 169);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRBS001B";
            this.Load += new System.EventHandler(this.TYHRBS001B_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_BATCH;
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYLabel LBL51_BPMYEAR;
        private TY.Service.Library.Controls.TYLabel LBL51_BPMSTYYMM;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYDatePicker DTP01_BPMSTYYMM;
        private Service.Library.Controls.TYTextBox TXT01_BPMYEAR;
        private Service.Library.Controls.TYTextBox TXT01_BPMDYRATE;
        private Service.Library.Controls.TYLabel LBL51_BPMDYRATE;
        private Service.Library.Controls.TYTextBox TXT01_BPMNYRATE;
        private Service.Library.Controls.TYLabel LBL51_BPMNYRATE;
    }
}