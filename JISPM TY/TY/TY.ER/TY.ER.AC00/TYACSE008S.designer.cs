namespace TY.ER.AC00
{
    partial class TYACSE008S
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
            FarPoint.Win.Spread.TipAppearance tipAppearance3 = new FarPoint.Win.Spread.TipAppearance();
            this.BTN61_INQ = new TY.Service.Library.Controls.TYButton();
            this.DTP01_GSTDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_GSTDATE = new TY.Service.Library.Controls.TYLabel();
            this.FPS91_TY_S_AC_46JHV845 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_AC_46JHV845_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CBO01_BSCDAC = new TY.Service.Library.Controls.TYComboBox();
            this.LBL51_BSCDAC = new TY.Service.Library.Controls.TYLabel();
            this.BTN61_PRT = new TY.Service.Library.Controls.TYButton();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_46JHV845)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_46JHV845_Sheet1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_INQ
            // 
            this.BTN61_INQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_INQ.FactoryID = "";
            this.BTN61_INQ.FactoryName = null;
            this.BTN61_INQ.Location = new System.Drawing.Point(1014, 13);
            this.BTN61_INQ.Name = "BTN61_INQ";
            this.BTN61_INQ.Size = new System.Drawing.Size(75, 21);
            this.BTN61_INQ.TabIndex = 0;
            this.BTN61_INQ.Text = "조회";
            this.BTN61_INQ.UseVisualStyleBackColor = true;
            this.BTN61_INQ.Click += new System.EventHandler(this.BTN61_INQ_Click);
            // 
            // DTP01_GSTDATE
            // 
            this.DTP01_GSTDATE.FactoryID = "";
            this.DTP01_GSTDATE.FactoryName = null;
            this.DTP01_GSTDATE.Location = new System.Drawing.Point(113, 11);
            this.DTP01_GSTDATE.Name = "DTP01_GSTDATE";
            this.DTP01_GSTDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_GSTDATE.TabIndex = 8;
            // 
            // LBL51_GSTDATE
            // 
            this.LBL51_GSTDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GSTDATE.FactoryID = "";
            this.LBL51_GSTDATE.FactoryName = null;
            this.LBL51_GSTDATE.IsCreated = false;
            this.LBL51_GSTDATE.Location = new System.Drawing.Point(7, 11);
            this.LBL51_GSTDATE.Name = "LBL51_GSTDATE";
            this.LBL51_GSTDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GSTDATE.TabIndex = 13;
            this.LBL51_GSTDATE.Text = "기준년월";
            this.LBL51_GSTDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FPS91_TY_S_AC_46JHV845
            // 
            this.FPS91_TY_S_AC_46JHV845.AccessibleDescription = "";
            this.FPS91_TY_S_AC_46JHV845.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_AC_46JHV845.FactoryID = "";
            this.FPS91_TY_S_AC_46JHV845.FactoryName = null;
            this.FPS91_TY_S_AC_46JHV845.Location = new System.Drawing.Point(1, 42);
            this.FPS91_TY_S_AC_46JHV845.Name = "FPS91_TY_S_AC_46JHV845";
            this.FPS91_TY_S_AC_46JHV845.PopMenuVisible = false;
            this.FPS91_TY_S_AC_46JHV845.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_AC_46JHV845.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_AC_46JHV845_Sheet1});
            this.FPS91_TY_S_AC_46JHV845.Size = new System.Drawing.Size(1175, 818);
            this.FPS91_TY_S_AC_46JHV845.TabIndex = 14;
            tipAppearance3.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_AC_46JHV845.TextTipAppearance = tipAppearance3;
            // 
            // FPS91_TY_S_AC_46JHV845_Sheet1
            // 
            this.FPS91_TY_S_AC_46JHV845_Sheet1.Reset();
            this.FPS91_TY_S_AC_46JHV845_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_AC_46JHV845_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_AC_46JHV845_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_AC_46JHV845_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.BTN61_PRT);
            this.groupBox1.Controls.Add(this.CBO01_BSCDAC);
            this.groupBox1.Controls.Add(this.LBL51_BSCDAC);
            this.groupBox1.Controls.Add(this.BTN61_INQ);
            this.groupBox1.Controls.Add(this.LBL51_GSTDATE);
            this.groupBox1.Controls.Add(this.DTP01_GSTDATE);
            this.groupBox1.Controls.Add(this.FPS91_TY_S_AC_46JHV845);
            this.groupBox1.Location = new System.Drawing.Point(2, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1175, 860);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // CBO01_BSCDAC
            // 
            this.CBO01_BSCDAC.FactoryID = "";
            this.CBO01_BSCDAC.FactoryName = null;
            this.CBO01_BSCDAC.Location = new System.Drawing.Point(574, 13);
            this.CBO01_BSCDAC.Name = "CBO01_BSCDAC";
            this.CBO01_BSCDAC.Size = new System.Drawing.Size(164, 20);
            this.CBO01_BSCDAC.TabIndex = 64;
            // 
            // LBL51_BSCDAC
            // 
            this.LBL51_BSCDAC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_BSCDAC.FactoryID = "";
            this.LBL51_BSCDAC.FactoryName = null;
            this.LBL51_BSCDAC.IsCreated = false;
            this.LBL51_BSCDAC.Location = new System.Drawing.Point(468, 13);
            this.LBL51_BSCDAC.Name = "LBL51_BSCDAC";
            this.LBL51_BSCDAC.Size = new System.Drawing.Size(100, 21);
            this.LBL51_BSCDAC.TabIndex = 65;
            this.LBL51_BSCDAC.Text = "계정과목";
            this.LBL51_BSCDAC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BTN61_PRT
            // 
            this.BTN61_PRT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_PRT.FactoryID = "";
            this.BTN61_PRT.FactoryName = null;
            this.BTN61_PRT.Location = new System.Drawing.Point(1094, 12);
            this.BTN61_PRT.Name = "BTN61_PRT";
            this.BTN61_PRT.Size = new System.Drawing.Size(75, 21);
            this.BTN61_PRT.TabIndex = 66;
            this.BTN61_PRT.Text = "출력";
            this.BTN61_PRT.UseVisualStyleBackColor = true;
            this.BTN61_PRT.Click += new System.EventHandler(this.BTN61_PRT_Click);
            // 
            // TYACSE008S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.groupBox1);
            this.Name = "TYACSE008S";
            this.Load += new System.EventHandler(this.TYACSE008S_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_46JHV845)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_46JHV845_Sheet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYDatePicker DTP01_GSTDATE;
        private TY.Service.Library.Controls.TYLabel LBL51_GSTDATE;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_AC_46JHV845;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_AC_46JHV845_Sheet1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Service.Library.Controls.TYComboBox CBO01_BSCDAC;
        private Service.Library.Controls.TYLabel LBL51_BSCDAC;
        private Service.Library.Controls.TYButton BTN61_PRT;
    }
}