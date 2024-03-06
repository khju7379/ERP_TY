namespace TY.ER.AC00
{
    partial class TYACLO01C2
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
            this.BTN61_CLO = new TY.Service.Library.Controls.TYButton();
            this.BTN61_INQ = new TY.Service.Library.Controls.TYButton();
            this.FPS91_TY_S_AC_87HH5416 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_P_AC_87HGZ415_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.LBL51_STYEAR = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_EDYEAR = new TY.Service.Library.Controls.TYTextBox();
            this.TXT01_STYEAR = new TY.Service.Library.Controls.TYTextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_87HH5416)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_P_AC_87HGZ415_Sheet1)).BeginInit();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(1295, 12);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 0;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // BTN61_INQ
            // 
            this.BTN61_INQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_INQ.FactoryID = "";
            this.BTN61_INQ.FactoryName = null;
            this.BTN61_INQ.Location = new System.Drawing.Point(1214, 12);
            this.BTN61_INQ.Name = "BTN61_INQ";
            this.BTN61_INQ.Size = new System.Drawing.Size(75, 21);
            this.BTN61_INQ.TabIndex = 1;
            this.BTN61_INQ.Text = "조회";
            this.BTN61_INQ.UseVisualStyleBackColor = true;
            this.BTN61_INQ.Click += new System.EventHandler(this.BTN61_INQ_Click);
            // 
            // FPS91_TY_S_AC_87HH5416
            // 
            this.FPS91_TY_S_AC_87HH5416.AccessibleDescription = "";
            this.FPS91_TY_S_AC_87HH5416.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_AC_87HH5416.FactoryID = "";
            this.FPS91_TY_S_AC_87HH5416.FactoryName = null;
            this.FPS91_TY_S_AC_87HH5416.Location = new System.Drawing.Point(1, 45);
            this.FPS91_TY_S_AC_87HH5416.Name = "FPS91_TY_S_AC_87HH5416";
            this.FPS91_TY_S_AC_87HH5416.PopMenuVisible = false;
            this.FPS91_TY_S_AC_87HH5416.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_AC_87HH5416.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_P_AC_87HGZ415_Sheet1});
            this.FPS91_TY_S_AC_87HH5416.Size = new System.Drawing.Size(1377, 405);
            this.FPS91_TY_S_AC_87HH5416.TabIndex = 4;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_AC_87HH5416.TextTipAppearance = tipAppearance1;
            this.FPS91_TY_S_AC_87HH5416.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.FPS91_TY_P_AC_87HGZ415_CellDoubleClick);
            this.FPS91_TY_S_AC_87HH5416.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FPS91_TY_P_AC_87HGZ415_KeyPress);
            // 
            // FPS91_TY_P_AC_87HGZ415_Sheet1
            // 
            this.FPS91_TY_P_AC_87HGZ415_Sheet1.Reset();
            this.FPS91_TY_P_AC_87HGZ415_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_P_AC_87HGZ415_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_P_AC_87HGZ415_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_P_AC_87HGZ415_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_EDYEAR);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_STYEAR);
            this.GBX80_CONTROLS.Controls.Add(this.label1);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_STYEAR);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_INQ);
            this.GBX80_CONTROLS.Controls.Add(this.FPS91_TY_S_AC_87HH5416);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1378, 450);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // LBL51_STYEAR
            // 
            this.LBL51_STYEAR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_STYEAR.FactoryID = "";
            this.LBL51_STYEAR.FactoryName = null;
            this.LBL51_STYEAR.ForeColor = System.Drawing.Color.White;
            this.LBL51_STYEAR.IsCreated = false;
            this.LBL51_STYEAR.Location = new System.Drawing.Point(5, 12);
            this.LBL51_STYEAR.Name = "LBL51_STYEAR";
            this.LBL51_STYEAR.Size = new System.Drawing.Size(100, 21);
            this.LBL51_STYEAR.TabIndex = 214;
            this.LBL51_STYEAR.Text = "계약년도";
            this.LBL51_STYEAR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_EDYEAR
            // 
            this.TXT01_EDYEAR.AcceptsReturn = true;
            this.TXT01_EDYEAR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_EDYEAR.FactoryID = "";
            this.TXT01_EDYEAR.FactoryName = null;
            this.TXT01_EDYEAR.Font = new System.Drawing.Font("굴림", 9F);
            this.TXT01_EDYEAR.Location = new System.Drawing.Point(183, 12);
            this.TXT01_EDYEAR.MinLength = 0;
            this.TXT01_EDYEAR.Name = "TXT01_EDYEAR";
            this.TXT01_EDYEAR.Size = new System.Drawing.Size(50, 21);
            this.TXT01_EDYEAR.TabIndex = 217;
            this.TXT01_EDYEAR.TabIndexCustom = false;
            // 
            // TXT01_STYEAR
            // 
            this.TXT01_STYEAR.AcceptsReturn = true;
            this.TXT01_STYEAR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_STYEAR.FactoryID = "";
            this.TXT01_STYEAR.FactoryName = null;
            this.TXT01_STYEAR.Font = new System.Drawing.Font("굴림", 9F);
            this.TXT01_STYEAR.Location = new System.Drawing.Point(111, 12);
            this.TXT01_STYEAR.MinLength = 0;
            this.TXT01_STYEAR.Name = "TXT01_STYEAR";
            this.TXT01_STYEAR.Size = new System.Drawing.Size(50, 21);
            this.TXT01_STYEAR.TabIndex = 216;
            this.TXT01_STYEAR.TabIndexCustom = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(166, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 215;
            this.label1.Text = "-";
            // 
            // TYACLO01C2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 457);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYACLO01C2";
            this.Load += new System.EventHandler(this.TYACLO01C2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_87HH5416)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_P_AC_87HGZ415_Sheet1)).EndInit();
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_AC_87HH5416;
        private FarPoint.Win.Spread.SheetView FPS91_TY_P_AC_87HGZ415_Sheet1;
        private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYLabel LBL51_STYEAR;
        private Service.Library.Controls.TYTextBox TXT01_EDYEAR;
        private Service.Library.Controls.TYTextBox TXT01_STYEAR;
        private System.Windows.Forms.Label label1;
    }
}