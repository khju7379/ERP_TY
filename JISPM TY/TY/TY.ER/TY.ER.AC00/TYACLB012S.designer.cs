namespace TY.ER.AC00
{
    partial class TYACLB012S
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
            this.BTN61_PRT = new TY.Service.Library.Controls.TYButton();
            this.CBH01_GCDDP = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_GCDDP = new TY.Service.Library.Controls.TYLabel();
            this.DTP01_GEDYYMM = new TY.Service.Library.Controls.TYDatePicker();
            this.DTP01_GSTYYMM = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_GSTYYMM = new TY.Service.Library.Controls.TYLabel();
            this.FPS91_TY_S_AC_28T3B629 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_AC_28T3B629_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_28T3B629)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_28T3B629_Sheet1)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            // BTN61_PRT
            // 
            this.BTN61_PRT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_PRT.FactoryID = "";
            this.BTN61_PRT.FactoryName = null;
            this.BTN61_PRT.Location = new System.Drawing.Point(1094, 12);
            this.BTN61_PRT.Name = "BTN61_PRT";
            this.BTN61_PRT.Size = new System.Drawing.Size(75, 21);
            this.BTN61_PRT.TabIndex = 1;
            this.BTN61_PRT.Text = "출력";
            this.BTN61_PRT.UseVisualStyleBackColor = true;
            this.BTN61_PRT.Click += new System.EventHandler(this.BTN61_PRT_Click);
            // 
            // CBH01_GCDDP
            // 
            this.CBH01_GCDDP.BindedDataRow = null;
            this.CBH01_GCDDP.CodeBoxWidth = 0;
            this.CBH01_GCDDP.DummyValue = null;
            this.CBH01_GCDDP.FactoryID = "";
            this.CBH01_GCDDP.FactoryName = null;
            this.CBH01_GCDDP.Location = new System.Drawing.Point(446, 12);
            this.CBH01_GCDDP.MinLength = 0;
            this.CBH01_GCDDP.Name = "CBH01_GCDDP";
            this.CBH01_GCDDP.Size = new System.Drawing.Size(165, 20);
            this.CBH01_GCDDP.TabIndex = 2;
            // 
            // LBL51_GCDDP
            // 
            this.LBL51_GCDDP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GCDDP.FactoryID = "";
            this.LBL51_GCDDP.FactoryName = null;
            this.LBL51_GCDDP.Location = new System.Drawing.Point(340, 12);
            this.LBL51_GCDDP.Name = "LBL51_GCDDP";
            this.LBL51_GCDDP.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GCDDP.TabIndex = 3;
            this.LBL51_GCDDP.Text = "사업장코드";
            this.LBL51_GCDDP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DTP01_GEDYYMM
            // 
            this.DTP01_GEDYYMM.FactoryID = "";
            this.DTP01_GEDYYMM.FactoryName = null;
            this.DTP01_GEDYYMM.Location = new System.Drawing.Point(234, 12);
            this.DTP01_GEDYYMM.Name = "DTP01_GEDYYMM";
            this.DTP01_GEDYYMM.Size = new System.Drawing.Size(100, 21);
            this.DTP01_GEDYYMM.TabIndex = 4;
            // 
            // DTP01_GSTYYMM
            // 
            this.DTP01_GSTYYMM.FactoryID = "";
            this.DTP01_GSTYYMM.FactoryName = null;
            this.DTP01_GSTYYMM.Location = new System.Drawing.Point(111, 12);
            this.DTP01_GSTYYMM.Name = "DTP01_GSTYYMM";
            this.DTP01_GSTYYMM.Size = new System.Drawing.Size(100, 21);
            this.DTP01_GSTYYMM.TabIndex = 6;
            this.DTP01_GSTYYMM.ValueChanged += new System.EventHandler(this.DTP01_GSTYYMM_ValueChanged);
            // 
            // LBL51_GSTYYMM
            // 
            this.LBL51_GSTYYMM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GSTYYMM.FactoryID = "";
            this.LBL51_GSTYYMM.FactoryName = null;
            this.LBL51_GSTYYMM.Location = new System.Drawing.Point(5, 12);
            this.LBL51_GSTYYMM.Name = "LBL51_GSTYYMM";
            this.LBL51_GSTYYMM.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GSTYYMM.TabIndex = 7;
            this.LBL51_GSTYYMM.Text = "시작년월";
            this.LBL51_GSTYYMM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FPS91_TY_S_AC_28T3B629
            // 
            this.FPS91_TY_S_AC_28T3B629.AccessibleDescription = "";
            this.FPS91_TY_S_AC_28T3B629.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_AC_28T3B629.FactoryID = "";
            this.FPS91_TY_S_AC_28T3B629.FactoryName = null;
            this.FPS91_TY_S_AC_28T3B629.Location = new System.Drawing.Point(1, 45);
            this.FPS91_TY_S_AC_28T3B629.Name = "FPS91_TY_S_AC_28T3B629";
            this.FPS91_TY_S_AC_28T3B629.PopMenuVisible = false;
            this.FPS91_TY_S_AC_28T3B629.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_AC_28T3B629.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_AC_28T3B629_Sheet1});
            this.FPS91_TY_S_AC_28T3B629.Size = new System.Drawing.Size(1175, 815);
            this.FPS91_TY_S_AC_28T3B629.TabIndex = 8;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_AC_28T3B629.TextTipAppearance = tipAppearance1;
            // 
            // FPS91_TY_S_AC_28T3B629_Sheet1
            // 
            this.FPS91_TY_S_AC_28T3B629_Sheet1.Reset();
            this.FPS91_TY_S_AC_28T3B629_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_AC_28T3B629_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_AC_28T3B629_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_AC_28T3B629_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.BTN61_INQ);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.BTN61_PRT);
            this.groupBox1.Controls.Add(this.LBL51_GSTYYMM);
            this.groupBox1.Controls.Add(this.CBH01_GCDDP);
            this.groupBox1.Controls.Add(this.DTP01_GSTYYMM);
            this.groupBox1.Controls.Add(this.LBL51_GCDDP);
            this.groupBox1.Controls.Add(this.DTP01_GEDYYMM);
            this.groupBox1.Location = new System.Drawing.Point(2, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1175, 860);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(217, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 29;
            this.label1.Text = "-";
            // 
            // TYACLB012S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.FPS91_TY_S_AC_28T3B629);
            this.Controls.Add(this.groupBox1);
            this.Name = "TYACLB012S";
            this.Load += new System.EventHandler(this.TYACLB012S_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_28T3B629)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_28T3B629_Sheet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYButton BTN61_PRT;
        private TY.Service.Library.Controls.TYCodeBox CBH01_GCDDP;
        private TY.Service.Library.Controls.TYLabel LBL51_GCDDP;
        private TY.Service.Library.Controls.TYDatePicker DTP01_GEDYYMM;
        private TY.Service.Library.Controls.TYDatePicker DTP01_GSTYYMM;
        private TY.Service.Library.Controls.TYLabel LBL51_GSTYYMM;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_AC_28T3B629;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_AC_28T3B629_Sheet1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
    }
}