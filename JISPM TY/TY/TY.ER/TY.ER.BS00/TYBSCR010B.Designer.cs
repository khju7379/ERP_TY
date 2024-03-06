namespace TY.ER.BS00
{
    partial class TYBSCR010B
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
            this.BTN61_BATCH = new TY.Service.Library.Controls.TYButton();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.TXT01_SDATE = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_SDATE = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(194, 193);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 0;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // BTN61_BATCH
            // 
            this.BTN61_BATCH.FactoryID = "";
            this.BTN61_BATCH.FactoryName = null;
            this.BTN61_BATCH.Location = new System.Drawing.Point(113, 193);
            this.BTN61_BATCH.Name = "BTN61_BATCH";
            this.BTN61_BATCH.Size = new System.Drawing.Size(75, 21);
            this.BTN61_BATCH.TabIndex = 1;
            this.BTN61_BATCH.Text = "전송";
            this.BTN61_BATCH.UseVisualStyleBackColor = true;
            this.BTN61_BATCH.Click += new System.EventHandler(this.BTN61_BATCH_Click);
            this.BTN61_BATCH.InvokerStart += new Shoveling2010.SmartClient.SystemUtility.Controls.TButton.CheckHandler(this.BTN61_BATCH_InvokerStart);
            this.BTN61_BATCH.InvokerEnd += new Shoveling2010.SmartClient.SystemUtility.Controls.TButton.CheckHandler(this.BTN61_BATCH_InvokerEnd);
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_SDATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_SDATE);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_BATCH);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(407, 246);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // TXT01_SDATE
            // 
            this.TXT01_SDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_SDATE.FactoryID = "";
            this.TXT01_SDATE.FactoryName = null;
            this.TXT01_SDATE.Location = new System.Drawing.Point(211, 91);
            this.TXT01_SDATE.MinLength = 0;
            this.TXT01_SDATE.Name = "TXT01_SDATE";
            this.TXT01_SDATE.Size = new System.Drawing.Size(58, 21);
            this.TXT01_SDATE.TabIndex = 8;
            this.TXT01_SDATE.TabIndexCustom = false;
            // 
            // LBL51_SDATE
            // 
            this.LBL51_SDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_SDATE.FactoryID = "";
            this.LBL51_SDATE.FactoryName = null;
            this.LBL51_SDATE.IsCreated = false;
            this.LBL51_SDATE.Location = new System.Drawing.Point(105, 91);
            this.LBL51_SDATE.Name = "LBL51_SDATE";
            this.LBL51_SDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SDATE.TabIndex = 7;
            this.LBL51_SDATE.Text = "사업년도";
            this.LBL51_SDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TYBSCR010B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 252);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYBSCR010B";
            this.Load += new System.EventHandler(this.TYBSCR010B_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYButton BTN61_BATCH;
        private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYTextBox TXT01_SDATE;
        private Service.Library.Controls.TYLabel LBL51_SDATE;
    }
}