namespace TY.ER.GB99
{
    partial class TYERGB996S
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.FPS91_TY24U1P923 = new TY.Service.Library.Controls.TYSpread();
            this.FPS91_TY24U1P923_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY24U1P923)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY24U1P923_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.FPS91_TY24U1P923);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1023, 638);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // FPS91_TY24U1P923
            // 
            this.FPS91_TY24U1P923.AccessibleDescription = "";
            this.FPS91_TY24U1P923.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FPS91_TY24U1P923.FactoryID = "";
            this.FPS91_TY24U1P923.FactoryName = null;
            this.FPS91_TY24U1P923.Location = new System.Drawing.Point(3, 17);
            this.FPS91_TY24U1P923.Name = "FPS91_TY24U1P923";
            this.FPS91_TY24U1P923.PopMenuVisible = false;
            this.FPS91_TY24U1P923.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.FPS91_TY24U1P923.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.FPS91_TY24U1P923_Sheet1});
            this.FPS91_TY24U1P923.Size = new System.Drawing.Size(1017, 618);
            this.FPS91_TY24U1P923.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FPS91_TY24U1P923.TextTipAppearance = tipAppearance1;
            // 
            // FPS91_TY24U1P923_Sheet1
            // 
            this.FPS91_TY24U1P923_Sheet1.Reset();
            this.FPS91_TY24U1P923_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.FPS91_TY24U1P923_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.FPS91_TY24U1P923_Sheet1.AutoUpdateNotes = true;
            this.FPS91_TY24U1P923_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // TYERGB996S
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1047, 662);
            this.Controls.Add(this.groupBox1);
            this.Name = "TYERGB996S";
            this.Text = "TYERGB996S";
            this.Load += new System.EventHandler(this.TYERGB996S_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY24U1P923)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FPS91_TY24U1P923_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private Service.Library.Controls.TYSpread FPS91_TY24U1P923;
        private FarPoint.Win.Spread.SheetView FPS91_TY24U1P923_Sheet1;
    }
}