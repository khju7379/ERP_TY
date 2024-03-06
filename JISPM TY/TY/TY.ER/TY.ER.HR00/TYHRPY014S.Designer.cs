namespace TY.ER.HR00
{
    partial class TYHRPY014S
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
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.BTN61_INQ = new TY.Service.Library.Controls.TYButton();
            this.BTN61_SEL = new TY.Service.Library.Controls.TYButton();
            this.CBH01_PAYGUBN = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_PAYGUBN = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_PAYJIDATE = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_PAYYYMM = new TY.Service.Library.Controls.TYLabel();
            this.FPS91_TY_S_HR_53PBY887 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_HR_53PBY887_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.DTP01_PAYJIDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.DTP01_PAYYYMM = new TY.Service.Library.Controls.TYDatePicker();
            this.BTN61_INQOPTION = new TY.Service.Library.Controls.TYButton();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_53PBY887)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_53PBY887_Sheet1)).BeginInit();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_INQ
            // 
            this.BTN61_INQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_INQ.FactoryID = "";
            this.BTN61_INQ.FactoryName = null;
            this.BTN61_INQ.Location = new System.Drawing.Point(1013, 12);
            this.BTN61_INQ.Name = "BTN61_INQ";
            this.BTN61_INQ.Size = new System.Drawing.Size(75, 21);
            this.BTN61_INQ.TabIndex = 0;
            this.BTN61_INQ.Text = "조회";
            this.BTN61_INQ.UseVisualStyleBackColor = true;
            this.BTN61_INQ.Click += new System.EventHandler(this.BTN61_INQ_Click);
            // 
            // BTN61_SEL
            // 
            this.BTN61_SEL.FactoryID = "";
            this.BTN61_SEL.FactoryName = null;
            this.BTN61_SEL.Location = new System.Drawing.Point(651, 12);
            this.BTN61_SEL.Name = "BTN61_SEL";
            this.BTN61_SEL.Size = new System.Drawing.Size(75, 21);
            this.BTN61_SEL.TabIndex = 1;
            this.BTN61_SEL.Text = "선택";
            this.BTN61_SEL.UseVisualStyleBackColor = true;
            this.BTN61_SEL.Click += new System.EventHandler(this.BTN61_SEL_Click);
            // 
            // CBH01_PAYGUBN
            // 
            this.CBH01_PAYGUBN.BindedDataRow = null;
            this.CBH01_PAYGUBN.CodeBoxWidth = 0;
            this.CBH01_PAYGUBN.DummyValue = null;
            this.CBH01_PAYGUBN.FactoryID = "";
            this.CBH01_PAYGUBN.FactoryName = null;
            this.CBH01_PAYGUBN.Location = new System.Drawing.Point(111, 12);
            this.CBH01_PAYGUBN.MinLength = 0;
            this.CBH01_PAYGUBN.Name = "CBH01_PAYGUBN";
            this.CBH01_PAYGUBN.Size = new System.Drawing.Size(106, 20);
            this.CBH01_PAYGUBN.TabIndex = 2;
            // 
            // LBL51_PAYGUBN
            // 
            this.LBL51_PAYGUBN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_PAYGUBN.FactoryID = "";
            this.LBL51_PAYGUBN.FactoryName = null;
            this.LBL51_PAYGUBN.IsCreated = false;
            this.LBL51_PAYGUBN.Location = new System.Drawing.Point(5, 12);
            this.LBL51_PAYGUBN.Name = "LBL51_PAYGUBN";
            this.LBL51_PAYGUBN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_PAYGUBN.TabIndex = 3;
            this.LBL51_PAYGUBN.Text = "급여구분";
            this.LBL51_PAYGUBN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_PAYJIDATE
            // 
            this.LBL51_PAYJIDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_PAYJIDATE.FactoryID = "";
            this.LBL51_PAYJIDATE.FactoryName = null;
            this.LBL51_PAYJIDATE.IsCreated = false;
            this.LBL51_PAYJIDATE.Location = new System.Drawing.Point(425, 12);
            this.LBL51_PAYJIDATE.Name = "LBL51_PAYJIDATE";
            this.LBL51_PAYJIDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_PAYJIDATE.TabIndex = 5;
            this.LBL51_PAYJIDATE.Text = "지급일자";
            this.LBL51_PAYJIDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_PAYYYMM
            // 
            this.LBL51_PAYYYMM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_PAYYYMM.FactoryID = "";
            this.LBL51_PAYYYMM.FactoryName = null;
            this.LBL51_PAYYYMM.IsCreated = false;
            this.LBL51_PAYYYMM.Location = new System.Drawing.Point(223, 12);
            this.LBL51_PAYYYMM.Name = "LBL51_PAYYYMM";
            this.LBL51_PAYYYMM.Size = new System.Drawing.Size(100, 21);
            this.LBL51_PAYYYMM.TabIndex = 7;
            this.LBL51_PAYYYMM.Text = "급여년월";
            this.LBL51_PAYYYMM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FPS91_TY_S_HR_53PBY887
            // 
            this.FPS91_TY_S_HR_53PBY887.AccessibleDescription = "";
            this.FPS91_TY_S_HR_53PBY887.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_HR_53PBY887.FactoryID = "";
            this.FPS91_TY_S_HR_53PBY887.FactoryName = null;
            this.FPS91_TY_S_HR_53PBY887.Location = new System.Drawing.Point(1, 45);
            this.FPS91_TY_S_HR_53PBY887.Name = "FPS91_TY_S_HR_53PBY887";
            this.FPS91_TY_S_HR_53PBY887.PopMenuVisible = false;
            this.FPS91_TY_S_HR_53PBY887.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_HR_53PBY887.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_HR_53PBY887_Sheet1});
            this.FPS91_TY_S_HR_53PBY887.Size = new System.Drawing.Size(1173, 812);
            this.FPS91_TY_S_HR_53PBY887.TabIndex = 8;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_HR_53PBY887.TextTipAppearance = tipAppearance2;
            this.FPS91_TY_S_HR_53PBY887.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.FPS91_TY_S_HR_53PBY887_ButtonClicked);
            // 
            // FPS91_TY_S_HR_53PBY887_Sheet1
            // 
            this.FPS91_TY_S_HR_53PBY887_Sheet1.Reset();
            this.FPS91_TY_S_HR_53PBY887_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_HR_53PBY887_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_HR_53PBY887_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_HR_53PBY887_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_INQOPTION);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_PAYJIDATE);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_PAYYYMM);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_INQ);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_SEL);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_PAYGUBN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_PAYGUBN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_PAYJIDATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_PAYYYMM);
            this.GBX80_CONTROLS.Controls.Add(this.FPS91_TY_S_HR_53PBY887);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1175, 860);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // DTP01_PAYJIDATE
            // 
            this.DTP01_PAYJIDATE.FactoryID = "";
            this.DTP01_PAYJIDATE.FactoryName = null;
            this.DTP01_PAYJIDATE.Location = new System.Drawing.Point(531, 12);
            this.DTP01_PAYJIDATE.Name = "DTP01_PAYJIDATE";
            this.DTP01_PAYJIDATE.Size = new System.Drawing.Size(114, 21);
            this.DTP01_PAYJIDATE.TabIndex = 10;
            // 
            // DTP01_PAYYYMM
            // 
            this.DTP01_PAYYYMM.FactoryID = "";
            this.DTP01_PAYYYMM.FactoryName = null;
            this.DTP01_PAYYYMM.Location = new System.Drawing.Point(329, 12);
            this.DTP01_PAYYYMM.Name = "DTP01_PAYYYMM";
            this.DTP01_PAYYYMM.Size = new System.Drawing.Size(90, 21);
            this.DTP01_PAYYYMM.TabIndex = 9;
            // 
            // BTN61_INQOPTION
            // 
            this.BTN61_INQOPTION.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_INQOPTION.FactoryID = "";
            this.BTN61_INQOPTION.FactoryName = null;
            this.BTN61_INQOPTION.Location = new System.Drawing.Point(1094, 12);
            this.BTN61_INQOPTION.Name = "BTN61_INQOPTION";
            this.BTN61_INQOPTION.Size = new System.Drawing.Size(75, 21);
            this.BTN61_INQOPTION.TabIndex = 11;
            this.BTN61_INQOPTION.Text = "옵션";
            this.BTN61_INQOPTION.UseVisualStyleBackColor = true;
            this.BTN61_INQOPTION.Click += new System.EventHandler(this.BTN61_INQOPTION_Click);
            // 
            // TYHRPY014S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRPY014S";
            this.Load += new System.EventHandler(this.TYHRPY014S_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_53PBY887)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_HR_53PBY887_Sheet1)).EndInit();
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYButton BTN61_SEL;
        private TY.Service.Library.Controls.TYCodeBox CBH01_PAYGUBN;
        private TY.Service.Library.Controls.TYLabel LBL51_PAYGUBN;
        private TY.Service.Library.Controls.TYLabel LBL51_PAYJIDATE;
        private TY.Service.Library.Controls.TYLabel LBL51_PAYYYMM;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_HR_53PBY887;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_HR_53PBY887_Sheet1;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYDatePicker DTP01_PAYYYMM;
        private Service.Library.Controls.TYDatePicker DTP01_PAYJIDATE;
        private Service.Library.Controls.TYButton BTN61_INQOPTION;
    }
}