namespace TY.ER.UT00
{
    partial class TYUTME034P
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
            this.CBH01_CHHWAJU = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_CHHWAJU = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_UTDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_UTDATE = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_PRT
            // 
            this.BTN61_PRT.FactoryID = "";
            this.BTN61_PRT.FactoryName = null;
            this.BTN61_PRT.Location = new System.Drawing.Point(512, 373);
            this.BTN61_PRT.Name = "BTN61_PRT";
            this.BTN61_PRT.Size = new System.Drawing.Size(75, 21);
            this.BTN61_PRT.TabIndex = 1;
            this.BTN61_PRT.Text = "출력";
            this.BTN61_PRT.UseVisualStyleBackColor = true;
            this.BTN61_PRT.Click += new System.EventHandler(this.BTN61_PRT_Click);
            // 
            // CBH01_CHHWAJU
            // 
            this.CBH01_CHHWAJU.BindedDataRow = null;
            this.CBH01_CHHWAJU.CodeBoxWidth = 0;
            this.CBH01_CHHWAJU.DummyValue = null;
            this.CBH01_CHHWAJU.FactoryID = "";
            this.CBH01_CHHWAJU.FactoryName = null;
            this.CBH01_CHHWAJU.Location = new System.Drawing.Point(512, 342);
            this.CBH01_CHHWAJU.MinLength = 0;
            this.CBH01_CHHWAJU.Name = "CBH01_CHHWAJU";
            this.CBH01_CHHWAJU.Size = new System.Drawing.Size(250, 20);
            this.CBH01_CHHWAJU.TabIndex = 2;
            // 
            // LBL51_CHHWAJU
            // 
            this.LBL51_CHHWAJU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_CHHWAJU.FactoryID = "";
            this.LBL51_CHHWAJU.FactoryName = null;
            this.LBL51_CHHWAJU.IsCreated = false;
            this.LBL51_CHHWAJU.Location = new System.Drawing.Point(406, 342);
            this.LBL51_CHHWAJU.Name = "LBL51_CHHWAJU";
            this.LBL51_CHHWAJU.Size = new System.Drawing.Size(100, 21);
            this.LBL51_CHHWAJU.TabIndex = 3;
            this.LBL51_CHHWAJU.Text = "화주";
            this.LBL51_CHHWAJU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_UTDATE
            // 
            this.DTP01_UTDATE.FactoryID = "";
            this.DTP01_UTDATE.FactoryName = null;
            this.DTP01_UTDATE.Location = new System.Drawing.Point(512, 315);
            this.DTP01_UTDATE.Name = "DTP01_UTDATE";
            this.DTP01_UTDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_UTDATE.TabIndex = 8;
            // 
            // LBL51_UTDATE
            // 
            this.LBL51_UTDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_UTDATE.FactoryID = "";
            this.LBL51_UTDATE.FactoryName = null;
            this.LBL51_UTDATE.IsCreated = false;
            this.LBL51_UTDATE.Location = new System.Drawing.Point(406, 315);
            this.LBL51_UTDATE.Name = "LBL51_UTDATE";
            this.LBL51_UTDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_UTDATE.TabIndex = 9;
            this.LBL51_UTDATE.Text = "매출일자";
            this.LBL51_UTDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_PRT);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_CHHWAJU);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_CHHWAJU);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_UTDATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_UTDATE);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1175, 860);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // TYUTME034P
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYUTME034P";
            this.Load += new System.EventHandler(this.TYUTME034P_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_PRT;
        private TY.Service.Library.Controls.TYCodeBox CBH01_CHHWAJU;
        private TY.Service.Library.Controls.TYLabel LBL51_CHHWAJU;
        private TY.Service.Library.Controls.TYDatePicker DTP01_UTDATE;
        private TY.Service.Library.Controls.TYLabel LBL51_UTDATE;
        private System.Windows.Forms.GroupBox GBX80_CONTROLS;
    }
}