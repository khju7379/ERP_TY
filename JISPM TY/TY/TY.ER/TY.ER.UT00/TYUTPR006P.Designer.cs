namespace TY.ER.UT00
{
    partial class TYUTPR006P
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
            this.TXT01_TNTANKNO = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_TNTANKNO = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.FPS91_TY_S_UT_A41ED179 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_UT_A41ED179_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GBX80_CONTROLS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_A41ED179)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_A41ED179_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // BTN61_INQ
            // 
            this.BTN61_INQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_INQ.FactoryID = "";
            this.BTN61_INQ.FactoryName = null;
            this.BTN61_INQ.Location = new System.Drawing.Point(1014, 12);
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
            this.BTN61_PRT.Location = new System.Drawing.Point(1095, 12);
            this.BTN61_PRT.Name = "BTN61_PRT";
            this.BTN61_PRT.Size = new System.Drawing.Size(75, 21);
            this.BTN61_PRT.TabIndex = 1;
            this.BTN61_PRT.Text = "출력";
            this.BTN61_PRT.UseVisualStyleBackColor = true;
            this.BTN61_PRT.Click += new System.EventHandler(this.BTN61_PRT_Click);
            // 
            // TXT01_TNTANKNO
            // 
            this.TXT01_TNTANKNO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_TNTANKNO.FactoryID = "";
            this.TXT01_TNTANKNO.FactoryName = null;
            this.TXT01_TNTANKNO.Location = new System.Drawing.Point(111, 12);
            this.TXT01_TNTANKNO.MinLength = 0;
            this.TXT01_TNTANKNO.Name = "TXT01_TNTANKNO";
            this.TXT01_TNTANKNO.Size = new System.Drawing.Size(80, 21);
            this.TXT01_TNTANKNO.TabIndex = 2;
            this.TXT01_TNTANKNO.TabIndexCustom = false;
            // 
            // LBL51_TNTANKNO
            // 
            this.LBL51_TNTANKNO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_TNTANKNO.FactoryID = "";
            this.LBL51_TNTANKNO.FactoryName = null;
            this.LBL51_TNTANKNO.IsCreated = false;
            this.LBL51_TNTANKNO.Location = new System.Drawing.Point(5, 12);
            this.LBL51_TNTANKNO.Name = "LBL51_TNTANKNO";
            this.LBL51_TNTANKNO.Size = new System.Drawing.Size(100, 21);
            this.LBL51_TNTANKNO.TabIndex = 3;
            this.LBL51_TNTANKNO.Text = "TANK번호";
            this.LBL51_TNTANKNO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.FPS91_TY_S_UT_A41ED179);
            this.GBX80_CONTROLS.Controls.Add(this.label7);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_INQ);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_PRT);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_TNTANKNO);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_TNTANKNO);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1175, 860);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(197, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(237, 13);
            this.label7.TabIndex = 155;
            this.label7.Text = "전체 출력시 탱크번호를 넣지 마세요";
            // 
            // FPS91_TY_S_UT_A41ED179
            // 
            this.FPS91_TY_S_UT_A41ED179.AccessibleDescription = "";
            this.FPS91_TY_S_UT_A41ED179.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_UT_A41ED179.FactoryID = "";
            this.FPS91_TY_S_UT_A41ED179.FactoryName = null;
            this.FPS91_TY_S_UT_A41ED179.Location = new System.Drawing.Point(1, 45);
            this.FPS91_TY_S_UT_A41ED179.Name = "FPS91_TY_S_UT_A41ED179";
            this.FPS91_TY_S_UT_A41ED179.PopMenuVisible = false;
            this.FPS91_TY_S_UT_A41ED179.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_UT_A41ED179.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_UT_A41ED179_Sheet1});
            this.FPS91_TY_S_UT_A41ED179.Size = new System.Drawing.Size(1174, 815);
            this.FPS91_TY_S_UT_A41ED179.TabIndex = 315;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_UT_A41ED179.TextTipAppearance = tipAppearance1;
            // 
            // FPS91_TY_S_UT_A41ED179_Sheet1
            // 
            this.FPS91_TY_S_UT_A41ED179_Sheet1.Reset();
            this.FPS91_TY_S_UT_A41ED179_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_UT_A41ED179_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_UT_A41ED179_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_UT_A41ED179_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // TYUTPR006P
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 861);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYUTPR006P";
            this.Load += new System.EventHandler(this.TYUTPR006P_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_A41ED179)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_A41ED179_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYButton BTN61_PRT;
        private TY.Service.Library.Controls.TYTextBox TXT01_TNTANKNO;
        private TY.Service.Library.Controls.TYLabel LBL51_TNTANKNO;
        private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private System.Windows.Forms.Label label7;
        private Service.Library.Controls.TYSpread FPS91_TY_S_UT_A41ED179;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_UT_A41ED179_Sheet1;
    }
}