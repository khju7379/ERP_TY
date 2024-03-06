namespace TY.ER.HR00
{
    partial class TYHRBS01B1
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
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.CBH01_BPMSABUN = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_BPMSABUN = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_BPMYEAR = new TY.Service.Library.Controls.TYTextBox();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_BATCH
            // 
            this.BTN61_BATCH.FactoryID = "";
            this.BTN61_BATCH.FactoryName = null;
            this.BTN61_BATCH.Location = new System.Drawing.Point(111, 82);
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
            this.BTN61_CLO.Location = new System.Drawing.Point(192, 82);
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
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_BPMSABUN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_BPMSABUN);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_BPMYEAR);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_BATCH);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_BPMYEAR);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(358, 133);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // CBH01_BPMSABUN
            // 
            this.CBH01_BPMSABUN.BindedDataRow = null;
            this.CBH01_BPMSABUN.CodeBoxWidth = 0;
            this.CBH01_BPMSABUN.DummyValue = null;
            this.CBH01_BPMSABUN.FactoryID = "";
            this.CBH01_BPMSABUN.FactoryName = null;
            this.CBH01_BPMSABUN.Location = new System.Drawing.Point(111, 39);
            this.CBH01_BPMSABUN.MinLength = 0;
            this.CBH01_BPMSABUN.Name = "CBH01_BPMSABUN";
            this.CBH01_BPMSABUN.Size = new System.Drawing.Size(161, 20);
            this.CBH01_BPMSABUN.TabIndex = 347;
            // 
            // LBL51_BPMSABUN
            // 
            this.LBL51_BPMSABUN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_BPMSABUN.FactoryID = "";
            this.LBL51_BPMSABUN.FactoryName = null;
            this.LBL51_BPMSABUN.IsCreated = false;
            this.LBL51_BPMSABUN.Location = new System.Drawing.Point(5, 38);
            this.LBL51_BPMSABUN.Name = "LBL51_BPMSABUN";
            this.LBL51_BPMSABUN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_BPMSABUN.TabIndex = 346;
            this.LBL51_BPMSABUN.Text = "사 번";
            this.LBL51_BPMSABUN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // TYHRBS01B1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 137);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRBS01B1";
            this.Load += new System.EventHandler(this.TYHRBS01B1_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_BATCH;
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYLabel LBL51_BPMYEAR;
        private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYTextBox TXT01_BPMYEAR;
        private Service.Library.Controls.TYCodeBox CBH01_BPMSABUN;
        private Service.Library.Controls.TYLabel LBL51_BPMSABUN;
    }
}