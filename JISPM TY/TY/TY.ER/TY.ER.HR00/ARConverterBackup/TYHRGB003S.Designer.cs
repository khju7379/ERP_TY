namespace TY.ER.HR00
{
    partial class TYHRGB003S
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
            this.BTN61_REM = new TY.Service.Library.Controls.TYButton();
            this.FPS91_TY_S_HR_858BC961 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_HR_858BC961_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.BTN61_PRT = new TY.Service.Library.Controls.TYButton();
            this.LBL51_BMSABUN = new TY.Service.Library.Controls.TYLabel();
            this.CBH01_BMSABUN = new TY.Service.Library.Controls.TYCodeBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DTP01_EDDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.DTP01_STDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_STDATE = new TY.Service.Library.Controls.TYLabel();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_858BC961)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_858BC961_Sheet1)).BeginInit();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_INQ
            // 
            this.BTN61_INQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_INQ.FactoryID = "";
            this.BTN61_INQ.FactoryName = null;
            this.BTN61_INQ.Location = new System.Drawing.Point(933, 12);
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
            this.BTN61_NEW.Location = new System.Drawing.Point(1014, 12);
            this.BTN61_NEW.Name = "BTN61_NEW";
            this.BTN61_NEW.Size = new System.Drawing.Size(75, 21);
            this.BTN61_NEW.TabIndex = 1;
            this.BTN61_NEW.Text = "신규";
            this.BTN61_NEW.UseVisualStyleBackColor = true;
            this.BTN61_NEW.Click += new System.EventHandler(this.BTN61_NEW_Click);
            // 
            // BTN61_REM
            // 
            this.BTN61_REM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_REM.FactoryID = "";
            this.BTN61_REM.FactoryName = null;
            this.BTN61_REM.Location = new System.Drawing.Point(1095, 12);
            this.BTN61_REM.Name = "BTN61_REM";
            this.BTN61_REM.Size = new System.Drawing.Size(75, 21);
            this.BTN61_REM.TabIndex = 2;
            this.BTN61_REM.Text = "삭제";
            this.BTN61_REM.UseVisualStyleBackColor = true;
            this.BTN61_REM.Click += new System.EventHandler(this.BTN61_REM_Click);
            // 
            // FPS91_TY_S_HR_858BC961
            // 
            this.FPS91_TY_S_HR_858BC961.AccessibleDescription = "";
            this.FPS91_TY_S_HR_858BC961.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_HR_858BC961.FactoryID = "";
            this.FPS91_TY_S_HR_858BC961.FactoryName = null;
            this.FPS91_TY_S_HR_858BC961.Location = new System.Drawing.Point(1, 45);
            this.FPS91_TY_S_HR_858BC961.Name = "FPS91_TY_S_HR_858BC961";
            this.FPS91_TY_S_HR_858BC961.PopMenuVisible = false;
            this.FPS91_TY_S_HR_858BC961.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_HR_858BC961.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_HR_858BC961_Sheet1});
            this.FPS91_TY_S_HR_858BC961.Size = new System.Drawing.Size(1174, 715);
            this.FPS91_TY_S_HR_858BC961.TabIndex = 9;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_HR_858BC961.TextTipAppearance = tipAppearance1;
            this.FPS91_TY_S_HR_858BC961.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.FPS91_TY_S_HR_858BC961_CellDoubleClick);
            this.FPS91_TY_S_HR_858BC961.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.FPS91_TY_S_HR_858BC961_ButtonClicked);
            // 
            // FPS91_TY_S_HR_858BC961_Sheet1
            // 
            this.FPS91_TY_S_HR_858BC961_Sheet1.Reset();
            this.FPS91_TY_S_HR_858BC961_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_HR_858BC961_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_HR_858BC961_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_HR_858BC961_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_PRT);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_BMSABUN);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_BMSABUN);
            this.GBX80_CONTROLS.Controls.Add(this.label1);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_EDDATE);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_STDATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_STDATE);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_INQ);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_NEW);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_REM);
            this.GBX80_CONTROLS.Controls.Add(this.FPS91_TY_S_HR_858BC961);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1175, 760);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // BTN61_PRT
            // 
            this.BTN61_PRT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_PRT.FactoryID = "";
            this.BTN61_PRT.FactoryName = null;
            this.BTN61_PRT.Location = new System.Drawing.Point(827, 12);
            this.BTN61_PRT.Name = "BTN61_PRT";
            this.BTN61_PRT.Size = new System.Drawing.Size(100, 21);
            this.BTN61_PRT.TabIndex = 100;
            this.BTN61_PRT.Text = "방문대장출력";
            this.BTN61_PRT.UseVisualStyleBackColor = true;
            this.BTN61_PRT.Click += new System.EventHandler(this.BTN61_PRT_Click);
            // 
            // LBL51_BMSABUN
            // 
            this.LBL51_BMSABUN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_BMSABUN.FactoryID = "";
            this.LBL51_BMSABUN.FactoryName = null;
            this.LBL51_BMSABUN.IsCreated = false;
            this.LBL51_BMSABUN.Location = new System.Drawing.Point(340, 12);
            this.LBL51_BMSABUN.Name = "LBL51_BMSABUN";
            this.LBL51_BMSABUN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_BMSABUN.TabIndex = 99;
            this.LBL51_BMSABUN.Text = "방문 사번";
            this.LBL51_BMSABUN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CBH01_BMSABUN
            // 
            this.CBH01_BMSABUN.BindedDataRow = null;
            this.CBH01_BMSABUN.CodeBoxWidth = 0;
            this.CBH01_BMSABUN.DummyValue = null;
            this.CBH01_BMSABUN.FactoryID = "";
            this.CBH01_BMSABUN.FactoryName = null;
            this.CBH01_BMSABUN.Location = new System.Drawing.Point(446, 12);
            this.CBH01_BMSABUN.MinLength = 0;
            this.CBH01_BMSABUN.Name = "CBH01_BMSABUN";
            this.CBH01_BMSABUN.Size = new System.Drawing.Size(161, 20);
            this.CBH01_BMSABUN.TabIndex = 98;
            this.CBH01_BMSABUN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CBH01_BMSABUN_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(217, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 97;
            this.label1.Text = "-";
            // 
            // DTP01_EDDATE
            // 
            this.DTP01_EDDATE.FactoryID = "";
            this.DTP01_EDDATE.FactoryName = null;
            this.DTP01_EDDATE.Location = new System.Drawing.Point(234, 12);
            this.DTP01_EDDATE.Name = "DTP01_EDDATE";
            this.DTP01_EDDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_EDDATE.TabIndex = 96;
            // 
            // DTP01_STDATE
            // 
            this.DTP01_STDATE.FactoryID = "";
            this.DTP01_STDATE.FactoryName = null;
            this.DTP01_STDATE.Location = new System.Drawing.Point(111, 12);
            this.DTP01_STDATE.Name = "DTP01_STDATE";
            this.DTP01_STDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_STDATE.TabIndex = 95;
            // 
            // LBL51_STDATE
            // 
            this.LBL51_STDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_STDATE.FactoryID = "";
            this.LBL51_STDATE.FactoryName = null;
            this.LBL51_STDATE.ForeColor = System.Drawing.Color.Black;
            this.LBL51_STDATE.IsCreated = false;
            this.LBL51_STDATE.Location = new System.Drawing.Point(5, 12);
            this.LBL51_STDATE.Name = "LBL51_STDATE";
            this.LBL51_STDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_STDATE.TabIndex = 94;
            this.LBL51_STDATE.Text = "방문 일자";
            this.LBL51_STDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TYHRGB003S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 762);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRGB003S";
            this.Load += new System.EventHandler(this.TYHRGB003S_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_858BC961)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_858BC961_Sheet1)).EndInit();
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYButton BTN61_NEW;
        private TY.Service.Library.Controls.TYButton BTN61_REM;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_HR_858BC961;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_HR_858BC961_Sheet1;
        private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private System.Windows.Forms.Label label1;
        private Service.Library.Controls.TYDatePicker DTP01_EDDATE;
        private Service.Library.Controls.TYDatePicker DTP01_STDATE;
        private Service.Library.Controls.TYLabel LBL51_STDATE;
        private Service.Library.Controls.TYLabel LBL51_BMSABUN;
        private Service.Library.Controls.TYCodeBox CBH01_BMSABUN;
        private Service.Library.Controls.TYButton BTN61_PRT;
    }
}