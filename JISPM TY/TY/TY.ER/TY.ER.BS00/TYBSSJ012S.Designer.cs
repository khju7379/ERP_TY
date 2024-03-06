namespace TY.ER.BS00
{
    partial class TYBSSJ012S
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
            this.BTN61_PRT = new TY.Service.Library.Controls.TYButton();
            this.CBH01_BCJDPAC = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_BCJDPAC = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_BCJYYMM = new TY.Service.Library.Controls.TYLabel();
            this.FPS91_TY_S_AC_7AGG2801 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_AC_7AGG2801_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TXT01_BCJYYMM = new TY.Service.Library.Controls.TYTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_7AGG2801)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_7AGG2801_Sheet1)).BeginInit();
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
            // CBH01_BCJDPAC
            // 
            this.CBH01_BCJDPAC.BindedDataRow = null;
            this.CBH01_BCJDPAC.CodeBoxWidth = 0;
            this.CBH01_BCJDPAC.DummyValue = null;
            this.CBH01_BCJDPAC.FactoryID = "";
            this.CBH01_BCJDPAC.FactoryName = null;
            this.CBH01_BCJDPAC.Location = new System.Drawing.Point(278, 12);
            this.CBH01_BCJDPAC.MinLength = 0;
            this.CBH01_BCJDPAC.Name = "CBH01_BCJDPAC";
            this.CBH01_BCJDPAC.Size = new System.Drawing.Size(196, 20);
            this.CBH01_BCJDPAC.TabIndex = 2;
            // 
            // LBL51_BCJDPAC
            // 
            this.LBL51_BCJDPAC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_BCJDPAC.FactoryID = "";
            this.LBL51_BCJDPAC.FactoryName = null;
            this.LBL51_BCJDPAC.IsCreated = false;
            this.LBL51_BCJDPAC.Location = new System.Drawing.Point(172, 12);
            this.LBL51_BCJDPAC.Name = "LBL51_BCJDPAC";
            this.LBL51_BCJDPAC.Size = new System.Drawing.Size(100, 21);
            this.LBL51_BCJDPAC.TabIndex = 3;
            this.LBL51_BCJDPAC.Text = "귀속부서";
            this.LBL51_BCJDPAC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_BCJYYMM
            // 
            this.LBL51_BCJYYMM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_BCJYYMM.FactoryID = "";
            this.LBL51_BCJYYMM.FactoryName = null;
            this.LBL51_BCJYYMM.IsCreated = false;
            this.LBL51_BCJYYMM.Location = new System.Drawing.Point(5, 12);
            this.LBL51_BCJYYMM.Name = "LBL51_BCJYYMM";
            this.LBL51_BCJYYMM.Size = new System.Drawing.Size(100, 21);
            this.LBL51_BCJYYMM.TabIndex = 5;
            this.LBL51_BCJYYMM.Text = "사업년도";
            this.LBL51_BCJYYMM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FPS91_TY_S_AC_7AGG2801
            // 
            this.FPS91_TY_S_AC_7AGG2801.AccessibleDescription = "";
            this.FPS91_TY_S_AC_7AGG2801.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_AC_7AGG2801.FactoryID = "";
            this.FPS91_TY_S_AC_7AGG2801.FactoryName = null;
            this.FPS91_TY_S_AC_7AGG2801.Location = new System.Drawing.Point(1, 57);
            this.FPS91_TY_S_AC_7AGG2801.Name = "FPS91_TY_S_AC_7AGG2801";
            this.FPS91_TY_S_AC_7AGG2801.PopMenuVisible = false;
            this.FPS91_TY_S_AC_7AGG2801.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_AC_7AGG2801.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_AC_7AGG2801_Sheet1});
            this.FPS91_TY_S_AC_7AGG2801.Size = new System.Drawing.Size(1175, 799);
            this.FPS91_TY_S_AC_7AGG2801.TabIndex = 6;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_AC_7AGG2801.TextTipAppearance = tipAppearance2;
            // 
            // FPS91_TY_S_AC_7AGG2801_Sheet1
            // 
            this.FPS91_TY_S_AC_7AGG2801_Sheet1.Reset();
            this.FPS91_TY_S_AC_7AGG2801_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_AC_7AGG2801_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_AC_7AGG2801_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_BCJYYMM);
            this.GBX80_CONTROLS.Controls.Add(this.label1);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_INQ);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_PRT);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_BCJDPAC);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_BCJDPAC);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_BCJYYMM);
            this.GBX80_CONTROLS.Controls.Add(this.FPS91_TY_S_AC_7AGG2801);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1175, 860);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1096, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "[단위: 천원]";
            // 
            // TXT01_BCJYYMM
            // 
            this.TXT01_BCJYYMM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_BCJYYMM.FactoryID = "";
            this.TXT01_BCJYYMM.FactoryName = null;
            this.TXT01_BCJYYMM.Location = new System.Drawing.Point(111, 12);
            this.TXT01_BCJYYMM.MinLength = 0;
            this.TXT01_BCJYYMM.Name = "TXT01_BCJYYMM";
            this.TXT01_BCJYYMM.Size = new System.Drawing.Size(55, 21);
            this.TXT01_BCJYYMM.TabIndex = 15;
            this.TXT01_BCJYYMM.TabIndexCustom = false;
            // 
            // TYBSSJ012S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYBSSJ012S";
            this.Load += new System.EventHandler(this.TYBSSJ012S_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_7AGG2801)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_7AGG2801_Sheet1)).EndInit();
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYButton BTN61_PRT;
        private TY.Service.Library.Controls.TYCodeBox CBH01_BCJDPAC;
        private TY.Service.Library.Controls.TYLabel LBL51_BCJDPAC;
        private TY.Service.Library.Controls.TYLabel LBL51_BCJYYMM;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_AC_7AGG2801;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_AC_7AGG2801_Sheet1;
        private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private System.Windows.Forms.Label label1;
        private Service.Library.Controls.TYTextBox TXT01_BCJYYMM;
    }
}