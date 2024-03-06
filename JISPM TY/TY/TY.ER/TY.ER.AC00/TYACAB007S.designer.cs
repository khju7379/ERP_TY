namespace TY.ER.AC00
{
    partial class TYACAB007S
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
            this.TXT01_CDCODE = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_CDCODE = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_CDDESC1 = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_CDDESC1 = new TY.Service.Library.Controls.TYLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.FPS91_TY_S_AC_243B8324 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_AC_243B8324_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_243B8324)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_243B8324_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // BTN61_INQ
            // 
            this.BTN61_INQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_INQ.FactoryID = "";
            this.BTN61_INQ.FactoryName = null;
            this.BTN61_INQ.Location = new System.Drawing.Point(932, 12);
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
            this.BTN61_NEW.Location = new System.Drawing.Point(1013, 12);
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
            this.BTN61_REM.Location = new System.Drawing.Point(1094, 12);
            this.BTN61_REM.Name = "BTN61_REM";
            this.BTN61_REM.Size = new System.Drawing.Size(75, 21);
            this.BTN61_REM.TabIndex = 2;
            this.BTN61_REM.Text = "삭제";
            this.BTN61_REM.UseVisualStyleBackColor = true;
            this.BTN61_REM.Click += new System.EventHandler(this.BTN61_REM_Click);
            // 
            // TXT01_CDCODE
            // 
            this.TXT01_CDCODE.FactoryID = "";
            this.TXT01_CDCODE.FactoryName = null;
            this.TXT01_CDCODE.Location = new System.Drawing.Point(111, 11);
            this.TXT01_CDCODE.MinLength = 0;
            this.TXT01_CDCODE.Name = "TXT01_CDCODE";
            this.TXT01_CDCODE.Size = new System.Drawing.Size(100, 21);
            this.TXT01_CDCODE.TabIndex = 3;
            // 
            // LBL51_CDCODE
            // 
            this.LBL51_CDCODE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_CDCODE.FactoryID = "";
            this.LBL51_CDCODE.FactoryName = null;
            this.LBL51_CDCODE.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.LBL51_CDCODE.ForeColor = System.Drawing.Color.White;
            this.LBL51_CDCODE.Location = new System.Drawing.Point(5, 12);
            this.LBL51_CDCODE.Name = "LBL51_CDCODE";
            this.LBL51_CDCODE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_CDCODE.TabIndex = 4;
            this.LBL51_CDCODE.Text = "CODE";
            this.LBL51_CDCODE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_CDDESC1
            // 
            this.TXT01_CDDESC1.FactoryID = "";
            this.TXT01_CDDESC1.FactoryName = null;
            this.TXT01_CDDESC1.Location = new System.Drawing.Point(341, 12);
            this.TXT01_CDDESC1.MinLength = 0;
            this.TXT01_CDDESC1.Name = "TXT01_CDDESC1";
            this.TXT01_CDDESC1.Size = new System.Drawing.Size(259, 21);
            this.TXT01_CDDESC1.TabIndex = 5;
            // 
            // LBL51_CDDESC1
            // 
            this.LBL51_CDDESC1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_CDDESC1.FactoryID = "";
            this.LBL51_CDDESC1.FactoryName = null;
            this.LBL51_CDDESC1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.LBL51_CDDESC1.ForeColor = System.Drawing.Color.White;
            this.LBL51_CDDESC1.Location = new System.Drawing.Point(235, 12);
            this.LBL51_CDDESC1.Name = "LBL51_CDDESC1";
            this.LBL51_CDDESC1.Size = new System.Drawing.Size(100, 21);
            this.LBL51_CDDESC1.TabIndex = 6;
            this.LBL51_CDDESC1.Text = "내용1";
            this.LBL51_CDDESC1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.FPS91_TY_S_AC_243B8324);
            this.groupBox1.Controls.Add(this.TXT01_CDCODE);
            this.groupBox1.Controls.Add(this.BTN61_NEW);
            this.groupBox1.Controls.Add(this.BTN61_INQ);
            this.groupBox1.Controls.Add(this.LBL51_CDCODE);
            this.groupBox1.Controls.Add(this.BTN61_REM);
            this.groupBox1.Controls.Add(this.TXT01_CDDESC1);
            this.groupBox1.Controls.Add(this.LBL51_CDDESC1);
            this.groupBox1.Location = new System.Drawing.Point(2, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1175, 860);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // FPS91_TY_S_AC_243B8324
            // 
            this.FPS91_TY_S_AC_243B8324.AccessibleDescription = "";
            this.FPS91_TY_S_AC_243B8324.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_AC_243B8324.FactoryID = "";
            this.FPS91_TY_S_AC_243B8324.FactoryName = null;
            this.FPS91_TY_S_AC_243B8324.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.FPS91_TY_S_AC_243B8324.Location = new System.Drawing.Point(1, 45);
            this.FPS91_TY_S_AC_243B8324.Name = "FPS91_TY_S_AC_243B8324";
            this.FPS91_TY_S_AC_243B8324.PopMenuVisible = false;
            this.FPS91_TY_S_AC_243B8324.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_AC_243B8324.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_AC_243B8324_Sheet1});
            this.FPS91_TY_S_AC_243B8324.Size = new System.Drawing.Size(1172, 815);
            this.FPS91_TY_S_AC_243B8324.TabIndex = 8;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_AC_243B8324.TextTipAppearance = tipAppearance1;
            this.FPS91_TY_S_AC_243B8324.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.FPS91_TY_S_AC_243B8324_CellDoubleClick);
            // 
            // FPS91_TY_S_AC_243B8324_Sheet1
            // 
            this.FPS91_TY_S_AC_243B8324_Sheet1.Reset();
            this.FPS91_TY_S_AC_243B8324_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_AC_243B8324_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_AC_243B8324_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_AC_243B8324_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // TYACAB007S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.groupBox1);
            this.Name = "TYACAB007S";
            this.Load += new System.EventHandler(this.TYACAB007S_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_243B8324)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_243B8324_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYButton BTN61_NEW;
        private TY.Service.Library.Controls.TYButton BTN61_REM;
        private TY.Service.Library.Controls.TYTextBox TXT01_CDCODE;
        private TY.Service.Library.Controls.TYLabel LBL51_CDCODE;
        private TY.Service.Library.Controls.TYTextBox TXT01_CDDESC1;
        private TY.Service.Library.Controls.TYLabel LBL51_CDDESC1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Service.Library.Controls.TYSpread FPS91_TY_S_AC_243B8324;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_AC_243B8324_Sheet1;
    }
}