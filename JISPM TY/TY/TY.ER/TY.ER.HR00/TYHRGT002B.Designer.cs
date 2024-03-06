namespace TY.ER.HR00
{
    partial class TYHRGT002B
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
            this.BTN61_CREATE = new TY.Service.Library.Controls.TYButton();
            this.DTP01_GEDYYMM = new TY.Service.Library.Controls.TYDatePicker();
            this.DTP01_GSTYYMM = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_GSTYYMM = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(231, 116);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 0;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // BTN61_CREATE
            // 
            this.BTN61_CREATE.FactoryID = "";
            this.BTN61_CREATE.FactoryName = null;
            this.BTN61_CREATE.Location = new System.Drawing.Point(150, 116);
            this.BTN61_CREATE.Name = "BTN61_CREATE";
            this.BTN61_CREATE.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CREATE.TabIndex = 1;
            this.BTN61_CREATE.Text = "생성";
            this.BTN61_CREATE.UseVisualStyleBackColor = true;
            this.BTN61_CREATE.Click += new System.EventHandler(this.BTN61_CREATE_Click);
            // 
            // DTP01_GEDYYMM
            // 
            this.DTP01_GEDYYMM.FactoryID = "";
            this.DTP01_GEDYYMM.FactoryName = null;
            this.DTP01_GEDYYMM.Location = new System.Drawing.Point(319, 59);
            this.DTP01_GEDYYMM.Name = "DTP01_GEDYYMM";
            this.DTP01_GEDYYMM.Size = new System.Drawing.Size(114, 21);
            this.DTP01_GEDYYMM.TabIndex = 2;
            // 
            // DTP01_GSTYYMM
            // 
            this.DTP01_GSTYYMM.FactoryID = "";
            this.DTP01_GSTYYMM.FactoryName = null;
            this.DTP01_GSTYYMM.Location = new System.Drawing.Point(180, 59);
            this.DTP01_GSTYYMM.Name = "DTP01_GSTYYMM";
            this.DTP01_GSTYYMM.Size = new System.Drawing.Size(112, 21);
            this.DTP01_GSTYYMM.TabIndex = 4;
            // 
            // LBL51_GSTYYMM
            // 
            this.LBL51_GSTYYMM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GSTYYMM.FactoryID = "";
            this.LBL51_GSTYYMM.FactoryName = null;
            this.LBL51_GSTYYMM.IsCreated = false;
            this.LBL51_GSTYYMM.Location = new System.Drawing.Point(74, 59);
            this.LBL51_GSTYYMM.Name = "LBL51_GSTYYMM";
            this.LBL51_GSTYYMM.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GSTYYMM.TabIndex = 5;
            this.LBL51_GSTYYMM.Text = "생성일자";
            this.LBL51_GSTYYMM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.label1);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CREATE);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_GEDYYMM);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_GSTYYMM);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GSTYYMM);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(461, 185);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(298, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 12);
            this.label1.TabIndex = 20;
            this.label1.Text = "~";
            // 
            // TYHRGT002B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 187);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRGT002B";
            this.Load += new System.EventHandler(this.TYHRGT002B_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYButton BTN61_CREATE;
        private TY.Service.Library.Controls.TYDatePicker DTP01_GEDYYMM;
        private TY.Service.Library.Controls.TYDatePicker DTP01_GSTYYMM;
        private TY.Service.Library.Controls.TYLabel LBL51_GSTYYMM;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private System.Windows.Forms.Label label1;
    }
}