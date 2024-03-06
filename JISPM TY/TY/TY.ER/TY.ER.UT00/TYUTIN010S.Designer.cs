namespace TY.ER.UT00
{
    partial class TYUTIN010S
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
            this.TXT01_TNTANKNO = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_TNTANKNO = new TY.Service.Library.Controls.TYLabel();
            this.GRP01_SEARCH = new System.Windows.Forms.GroupBox();
            this.FPS91_TY_S_UT_6AQGV566 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_P_UT_6AQGV565_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GRP01_SEARCH.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_6AQGV566)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_P_UT_6AQGV565_Sheet1)).BeginInit();
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
            // TXT01_TNTANKNO
            // 
            this.TXT01_TNTANKNO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_TNTANKNO.FactoryID = "";
            this.TXT01_TNTANKNO.FactoryName = null;
            this.TXT01_TNTANKNO.Location = new System.Drawing.Point(111, 12);
            this.TXT01_TNTANKNO.MinLength = 0;
            this.TXT01_TNTANKNO.Name = "TXT01_TNTANKNO";
            this.TXT01_TNTANKNO.Size = new System.Drawing.Size(70, 21);
            this.TXT01_TNTANKNO.TabIndex = 5;
            this.TXT01_TNTANKNO.TabIndexCustom = false;
            // 
            // LBL51_TNTANKNO
            // 
            this.LBL51_TNTANKNO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(152)))), ((int)(((byte)(135)))));
            this.LBL51_TNTANKNO.FactoryID = "";
            this.LBL51_TNTANKNO.FactoryName = null;
            this.LBL51_TNTANKNO.ForeColor = System.Drawing.Color.White;
            this.LBL51_TNTANKNO.IsCreated = false;
            this.LBL51_TNTANKNO.Location = new System.Drawing.Point(5, 12);
            this.LBL51_TNTANKNO.Name = "LBL51_TNTANKNO";
            this.LBL51_TNTANKNO.Size = new System.Drawing.Size(100, 21);
            this.LBL51_TNTANKNO.TabIndex = 6;
            this.LBL51_TNTANKNO.Text = "TANK번호";
            this.LBL51_TNTANKNO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GRP01_SEARCH
            // 
            this.GRP01_SEARCH.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GRP01_SEARCH.Controls.Add(this.FPS91_TY_S_UT_6AQGV566);
            this.GRP01_SEARCH.Controls.Add(this.BTN61_INQ);
            this.GRP01_SEARCH.Controls.Add(this.BTN61_NEW);
            this.GRP01_SEARCH.Controls.Add(this.BTN61_REM);
            this.GRP01_SEARCH.Controls.Add(this.LBL51_TNTANKNO);
            this.GRP01_SEARCH.Controls.Add(this.TXT01_TNTANKNO);
            this.GRP01_SEARCH.Location = new System.Drawing.Point(2, 1);
            this.GRP01_SEARCH.Name = "GRP01_SEARCH";
            this.GRP01_SEARCH.Size = new System.Drawing.Size(1175, 860);
            this.GRP01_SEARCH.TabIndex = 8;
            this.GRP01_SEARCH.TabStop = false;
            // 
            // FPS91_TY_S_UT_6AQGV566
            // 
            this.FPS91_TY_S_UT_6AQGV566.AccessibleDescription = "";
            this.FPS91_TY_S_UT_6AQGV566.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_UT_6AQGV566.FactoryID = "";
            this.FPS91_TY_S_UT_6AQGV566.FactoryName = null;
            this.FPS91_TY_S_UT_6AQGV566.Location = new System.Drawing.Point(1, 45);
            this.FPS91_TY_S_UT_6AQGV566.Name = "FPS91_TY_S_UT_6AQGV566";
            this.FPS91_TY_S_UT_6AQGV566.PopMenuVisible = false;
            this.FPS91_TY_S_UT_6AQGV566.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_UT_6AQGV566.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_P_UT_6AQGV565_Sheet1});
            this.FPS91_TY_S_UT_6AQGV566.Size = new System.Drawing.Size(1175, 815);
            this.FPS91_TY_S_UT_6AQGV566.TabIndex = 47;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_UT_6AQGV566.TextTipAppearance = tipAppearance1;
            this.FPS91_TY_S_UT_6AQGV566.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.FPS91_TY_P_UT_6AQGV565_CellDoubleClick);
            // 
            // FPS91_TY_P_UT_6AQGV565_Sheet1
            // 
            this.FPS91_TY_P_UT_6AQGV565_Sheet1.Reset();
            this.FPS91_TY_P_UT_6AQGV565_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_P_UT_6AQGV565_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_P_UT_6AQGV565_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_P_UT_6AQGV565_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // TYUTIN010S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.GRP01_SEARCH);
            this.Name = "TYUTIN010S";
            this.Load += new System.EventHandler(this.TYUTIN010S_Load);
            this.GRP01_SEARCH.ResumeLayout(false);
            this.GRP01_SEARCH.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_UT_6AQGV566)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_P_UT_6AQGV565_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYButton BTN61_NEW;
        private TY.Service.Library.Controls.TYButton BTN61_REM;
        private TY.Service.Library.Controls.TYTextBox TXT01_TNTANKNO;
        private TY.Service.Library.Controls.TYLabel LBL51_TNTANKNO;
        private System.Windows.Forms.GroupBox GRP01_SEARCH;
        private Service.Library.Controls.TYSpread FPS91_TY_S_UT_6AQGV566;
        private FarPoint.Win.Spread.SheetView FPS91_TY_P_UT_6AQGV565_Sheet1;
    }
}