namespace TY.ER.AC00
{
    partial class TYACZZ001S
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
            this.FPS91_TY_S_AC_29I4Z177 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY_S_AC_29I4Z177_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_29I4Z177)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_29I4Z177_Sheet1)).BeginInit();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_INQ
            // 
            this.BTN61_INQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_INQ.FactoryID = "";
            this.BTN61_INQ.FactoryName = null;
            this.BTN61_INQ.Location = new System.Drawing.Point(1094, 12);
            this.BTN61_INQ.Name = "BTN61_INQ";
            this.BTN61_INQ.Size = new System.Drawing.Size(75, 21);
            this.BTN61_INQ.TabIndex = 0;
            this.BTN61_INQ.Text = "조회";
            this.BTN61_INQ.UseVisualStyleBackColor = true;
            this.BTN61_INQ.Click += new System.EventHandler(this.BTN61_INQ_Click);
            // 
            // FPS91_TY_S_AC_29I4Z177
            // 
            this.FPS91_TY_S_AC_29I4Z177.AccessibleDescription = "";
            this.FPS91_TY_S_AC_29I4Z177.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FPS91_TY_S_AC_29I4Z177.FactoryID = "";
            this.FPS91_TY_S_AC_29I4Z177.FactoryName = null;
            this.FPS91_TY_S_AC_29I4Z177.Location = new System.Drawing.Point(1, 39);
            this.FPS91_TY_S_AC_29I4Z177.Name = "FPS91_TY_S_AC_29I4Z177";
            this.FPS91_TY_S_AC_29I4Z177.PopMenuVisible = false;
            this.FPS91_TY_S_AC_29I4Z177.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY_S_AC_29I4Z177.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY_S_AC_29I4Z177_Sheet1});
            this.FPS91_TY_S_AC_29I4Z177.Size = new System.Drawing.Size(1174, 815);
            this.FPS91_TY_S_AC_29I4Z177.TabIndex = 1;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY_S_AC_29I4Z177.TextTipAppearance = tipAppearance1;
            // 
            // FPS91_TY_S_AC_29I4Z177_Sheet1
            // 
            this.FPS91_TY_S_AC_29I4Z177_Sheet1.Reset();
            this.FPS91_TY_S_AC_29I4Z177_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY_S_AC_29I4Z177_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY_S_AC_29I4Z177_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY_S_AC_29I4Z177_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_INQ);
            this.GBX80_CONTROLS.Controls.Add(this.FPS91_TY_S_AC_29I4Z177);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1175, 860);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // TYACZZ001S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 862);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYACZZ001S";
            this.Load += new System.EventHandler(this.TYACZZ001S_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_29I4Z177)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY_S_AC_29I4Z177_Sheet1)).EndInit();
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_INQ;
        private TY.Service.Library.Controls.TYSpread FPS91_TY_S_AC_29I4Z177;
        private FarPoint.Win.Spread.SheetView FPS91_TY_S_AC_29I4Z177_Sheet1;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
    }
}