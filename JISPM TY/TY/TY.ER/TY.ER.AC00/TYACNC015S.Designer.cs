namespace TY.ER.AC00
{
    partial class TYACNC015S
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
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            this.BTN61_INQ = new TY.Service.Library.Controls.TYButton();
            this.BTN61_NEW = new TY.Service.Library.Controls.TYButton();
            this.LBL51_SDATE = new TY.Service.Library.Controls.TYLabel();
            this.FPS91_TY_S_AC_AB3FY106 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_AC_AB3FY106_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.DTP01_EDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.DTP01_SDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BTN61_REM = new TY.Service.Library.Controls.TYButton();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_AB3FY106)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_AB3FY106_Sheet1)).BeginInit();
            this.GBX80_CONTROLS.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_INQ
            // 
            this.BTN61_INQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_INQ.FactoryID = "";
            this.BTN61_INQ.FactoryName = null;
            this.BTN61_INQ.Location = new System.Drawing.Point(923, 12);
            this.BTN61_INQ.Name = "BTN61_INQ";
            this.BTN61_INQ.Size = new System.Drawing.Size(75, 21);
            this.BTN61_INQ.TabIndex = 0;
            this.BTN61_INQ.Text = "조회";
            this.BTN61_INQ.UseVisualStyleBackColor = true;
            this.BTN61_INQ.Click += new System.EventHandler(this.BTN61_INQ_Click);
            // 
            // BTN61_NEW
            // 
            this.BTN61_NEW.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_NEW.FactoryID = "";
            this.BTN61_NEW.FactoryName = null;
            this.BTN61_NEW.Location = new System.Drawing.Point(1004, 12);
            this.BTN61_NEW.Name = "BTN61_NEW";
            this.BTN61_NEW.Size = new System.Drawing.Size(75, 21);
            this.BTN61_NEW.TabIndex = 1;
            this.BTN61_NEW.Text = "신규";
            this.BTN61_NEW.UseVisualStyleBackColor = true;
            this.BTN61_NEW.Click += new System.EventHandler(this.BTN61_NEW_Click);
            // 
            // LBL51_SDATE
            // 
            this.LBL51_SDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_SDATE.FactoryID = "";
            this.LBL51_SDATE.FactoryName = null;
            this.LBL51_SDATE.IsCreated = false;
            this.LBL51_SDATE.Location = new System.Drawing.Point(5, 12);
            this.LBL51_SDATE.Name = "LBL51_SDATE";
            this.LBL51_SDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_SDATE.TabIndex = 5;
            this.LBL51_SDATE.Text = "시작일자";
            this.LBL51_SDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FPS91_TY_S_AC_AB3FY106
            // 
            this.FPS91_TY_S_AC_AB3FY106.AccessibleDescription = "";
            this.FPS91_TY_S_AC_AB3FY106.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FPS91_TY_S_AC_AB3FY106.FactoryID = "";
            this.FPS91_TY_S_AC_AB3FY106.FactoryName = null;
            this.FPS91_TY_S_AC_AB3FY106.Location = new System.Drawing.Point(3, 17);
            this.FPS91_TY_S_AC_AB3FY106.Name = "FPS91_TY_S_AC_AB3FY106";
            this.FPS91_TY_S_AC_AB3FY106.PopMenuVisible = false;
            this.FPS91_TY_S_AC_AB3FY106.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_AC_AB3FY106.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_AC_AB3FY106_Sheet1});
            this.FPS91_TY_S_AC_AB3FY106.Size = new System.Drawing.Size(1154, 759);
            this.FPS91_TY_S_AC_AB3FY106.TabIndex = 6;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_AC_AB3FY106.TextTipAppearance = tipAppearance1;
            this.FPS91_TY_S_AC_AB3FY106.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.FPS91_TY_S_AC_AB3FY106_CellDoubleClick);
            this.FPS91_TY_S_AC_AB3FY106.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.FPS91_TY_S_AC_AB3FY106_CellClick);
            // 
            // FPS91_TY_S_AC_AB3FY106_Sheet1
            // 
            this.FPS91_TY_S_AC_AB3FY106_Sheet1.Reset();
            this.FPS91_TY_S_AC_AB3FY106_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_AC_AB3FY106_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_AC_AB3FY106_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_AC_AB3FY106_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.tabControl1);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1270, 1000);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(6, 11);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1174, 847);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.BTN61_REM);
            this.tabPage1.Controls.Add(this.DTP01_EDATE);
            this.tabPage1.Controls.Add(this.DTP01_SDATE);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.BTN61_NEW);
            this.tabPage1.Controls.Add(this.BTN61_INQ);
            this.tabPage1.Controls.Add(this.LBL51_SDATE);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1166, 821);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "프로젝트 이자관리";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // DTP01_EDATE
            // 
            this.DTP01_EDATE.FactoryID = "";
            this.DTP01_EDATE.FactoryName = null;
            this.DTP01_EDATE.Location = new System.Drawing.Point(238, 12);
            this.DTP01_EDATE.Name = "DTP01_EDATE";
            this.DTP01_EDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_EDATE.TabIndex = 11;
            // 
            // DTP01_SDATE
            // 
            this.DTP01_SDATE.FactoryID = "";
            this.DTP01_SDATE.FactoryName = null;
            this.DTP01_SDATE.Location = new System.Drawing.Point(112, 12);
            this.DTP01_SDATE.Name = "DTP01_SDATE";
            this.DTP01_SDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_SDATE.TabIndex = 12;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.FPS91_TY_S_AC_AB3FY106);
            this.groupBox2.Location = new System.Drawing.Point(3, 39);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1160, 779);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "프로젝트관리";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(218, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "~";
            // 
            // BTN61_REM
            // 
            this.BTN61_REM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_REM.FactoryID = "";
            this.BTN61_REM.FactoryName = null;
            this.BTN61_REM.Location = new System.Drawing.Point(1085, 12);
            this.BTN61_REM.Name = "BTN61_REM";
            this.BTN61_REM.Size = new System.Drawing.Size(75, 21);
            this.BTN61_REM.TabIndex = 13;
            this.BTN61_REM.Text = "삭제";
            this.BTN61_REM.UseVisualStyleBackColor = true;
            this.BTN61_REM.Click += new System.EventHandler(this.BTN61_REM_Click);
            // 
            // TYACNC015S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 861);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYACNC015S";
            this.Load += new System.EventHandler(this.TYACNC015S_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_AB3FY106)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_AB3FY106_Sheet1)).EndInit();
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYButton BTN61_NEW;
        private TY.Service.Library.Controls.TYLabel LBL51_SDATE;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_AC_AB3FY106;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_AC_AB3FY106_Sheet1;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Service.Library.Controls.TYDatePicker DTP01_EDATE;
        private Service.Library.Controls.TYDatePicker DTP01_SDATE;
        private Service.Library.Controls.TYButton BTN61_REM;
    }
}