namespace TY.ER.UT00
{
    partial class TYUTME005B
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
            this.BTN61_CREATE = new TY.Service.Library.Controls.TYButton();
            this.BTN61_CLO = new TY.Service.Library.Controls.TYButton();
            this.LBL51_M2DATE = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.LBL51_GGUBUN = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_M2DATE = new TY.Service.Library.Controls.TYDatePicker();
            this.CBO01_GGUBUN = new TY.Service.Library.Controls.TYComboBox();
            this.BTN61_PRT = new TY.Service.Library.Controls.TYButton();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CREATE
            // 
            this.BTN61_CREATE.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BTN61_CREATE.FactoryID = "";
            this.BTN61_CREATE.FactoryName = null;
            this.BTN61_CREATE.Location = new System.Drawing.Point(85, 73);
            this.BTN61_CREATE.Name = "BTN61_CREATE";
            this.BTN61_CREATE.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CREATE.TabIndex = 1;
            this.BTN61_CREATE.Text = "전표생성";
            this.BTN61_CREATE.UseVisualStyleBackColor = true;
            this.BTN61_CREATE.Click += new System.EventHandler(this.BTN61_CREATE_Click);
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(247, 73);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 2;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // LBL51_M2DATE
            // 
            this.LBL51_M2DATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_M2DATE.FactoryID = "";
            this.LBL51_M2DATE.FactoryName = null;
            this.LBL51_M2DATE.IsCreated = false;
            this.LBL51_M2DATE.Location = new System.Drawing.Point(5, 12);
            this.LBL51_M2DATE.Name = "LBL51_M2DATE";
            this.LBL51_M2DATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_M2DATE.TabIndex = 4;
            this.LBL51_M2DATE.Text = "기준일자";
            this.LBL51_M2DATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_PRT);
            this.GBX80_CONTROLS.Controls.Add(this.CBO01_GGUBUN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GGUBUN);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_M2DATE);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CREATE);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_M2DATE);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(399, 106);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // LBL51_GGUBUN
            // 
            this.LBL51_GGUBUN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GGUBUN.FactoryID = "";
            this.LBL51_GGUBUN.FactoryName = null;
            this.LBL51_GGUBUN.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LBL51_GGUBUN.IsCreated = false;
            this.LBL51_GGUBUN.Location = new System.Drawing.Point(5, 39);
            this.LBL51_GGUBUN.Name = "LBL51_GGUBUN";
            this.LBL51_GGUBUN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GGUBUN.TabIndex = 310;
            this.LBL51_GGUBUN.Text = "생성구분";
            this.LBL51_GGUBUN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_M2DATE
            // 
            this.DTP01_M2DATE.FactoryID = "";
            this.DTP01_M2DATE.FactoryName = null;
            this.DTP01_M2DATE.Location = new System.Drawing.Point(111, 12);
            this.DTP01_M2DATE.Name = "DTP01_M2DATE";
            this.DTP01_M2DATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_M2DATE.TabIndex = 156;
            // 
            // CBO01_GGUBUN
            // 
            this.CBO01_GGUBUN.FactoryID = "";
            this.CBO01_GGUBUN.FactoryName = null;
            this.CBO01_GGUBUN.Font = new System.Drawing.Font("굴림", 9F);
            this.CBO01_GGUBUN.Location = new System.Drawing.Point(111, 40);
            this.CBO01_GGUBUN.Name = "CBO01_GGUBUN";
            this.CBO01_GGUBUN.Size = new System.Drawing.Size(100, 20);
            this.CBO01_GGUBUN.TabIndex = 311;
            // 
            // BTN61_PRT
            // 
            this.BTN61_PRT.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BTN61_PRT.FactoryID = "";
            this.BTN61_PRT.FactoryName = null;
            this.BTN61_PRT.Location = new System.Drawing.Point(166, 73);
            this.BTN61_PRT.Name = "BTN61_PRT";
            this.BTN61_PRT.Size = new System.Drawing.Size(75, 21);
            this.BTN61_PRT.TabIndex = 312;
            this.BTN61_PRT.Text = "전표출력";
            this.BTN61_PRT.UseVisualStyleBackColor = true;
            this.BTN61_PRT.Click += new System.EventHandler(this.BTN61_PRT_Click);
            // 
            // TYUTME005B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 108);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYUTME005B";
            this.Load += new System.EventHandler(this.TYUTME005B_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_CREATE;
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYLabel LBL51_M2DATE;
        private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYDatePicker DTP01_M2DATE;
        private Service.Library.Controls.TYLabel LBL51_GGUBUN;
        private Service.Library.Controls.TYComboBox CBO01_GGUBUN;
        private Service.Library.Controls.TYButton BTN61_PRT;
    }
}