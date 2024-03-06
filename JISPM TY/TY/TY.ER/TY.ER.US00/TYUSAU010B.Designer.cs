namespace TY.ER.US00
{
    partial class TYUSAU010B
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
            this.BTN61_CLO = new TY.Service.Library.Controls.TYButton();
            this.BTN61_BATCH = new TY.Service.Library.Controls.TYButton();
            this.GRP01_SEARCH = new System.Windows.Forms.GroupBox();
            this.LBL51_TGJUNGRY = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_TGJUNGRY = new TY.Service.Library.Controls.TYTextBox();
            this.GRP01_SEARCH.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(126, 57);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 0;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // BTN61_BATCH
            // 
            this.BTN61_BATCH.FactoryID = "";
            this.BTN61_BATCH.FactoryName = null;
            this.BTN61_BATCH.Location = new System.Drawing.Point(45, 57);
            this.BTN61_BATCH.Name = "BTN61_BATCH";
            this.BTN61_BATCH.Size = new System.Drawing.Size(75, 21);
            this.BTN61_BATCH.TabIndex = 1;
            this.BTN61_BATCH.Text = "처리";
            this.BTN61_BATCH.UseVisualStyleBackColor = true;
            this.BTN61_BATCH.Click += new System.EventHandler(this.BTN61_BATCH_Click);
            // 
            // GRP01_SEARCH
            // 
            this.GRP01_SEARCH.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GRP01_SEARCH.Controls.Add(this.LBL51_TGJUNGRY);
            this.GRP01_SEARCH.Controls.Add(this.TXT01_TGJUNGRY);
            this.GRP01_SEARCH.Controls.Add(this.BTN61_CLO);
            this.GRP01_SEARCH.Controls.Add(this.BTN61_BATCH);
            this.GRP01_SEARCH.Location = new System.Drawing.Point(2, 1);
            this.GRP01_SEARCH.Name = "GRP01_SEARCH";
            this.GRP01_SEARCH.Size = new System.Drawing.Size(243, 90);
            this.GRP01_SEARCH.TabIndex = 1;
            this.GRP01_SEARCH.TabStop = false;
            // 
            // LBL51_TGJUNGRY
            // 
            this.LBL51_TGJUNGRY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_TGJUNGRY.FactoryID = "";
            this.LBL51_TGJUNGRY.FactoryName = null;
            this.LBL51_TGJUNGRY.IsCreated = false;
            this.LBL51_TGJUNGRY.Location = new System.Drawing.Point(20, 30);
            this.LBL51_TGJUNGRY.Name = "LBL51_TGJUNGRY";
            this.LBL51_TGJUNGRY.Size = new System.Drawing.Size(100, 21);
            this.LBL51_TGJUNGRY.TabIndex = 94;
            this.LBL51_TGJUNGRY.Text = "차량중량";
            this.LBL51_TGJUNGRY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_TGJUNGRY
            // 
            this.TXT01_TGJUNGRY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_TGJUNGRY.FactoryID = "";
            this.TXT01_TGJUNGRY.FactoryName = null;
            this.TXT01_TGJUNGRY.Location = new System.Drawing.Point(126, 30);
            this.TXT01_TGJUNGRY.MinLength = 0;
            this.TXT01_TGJUNGRY.Name = "TXT01_TGJUNGRY";
            this.TXT01_TGJUNGRY.Size = new System.Drawing.Size(100, 21);
            this.TXT01_TGJUNGRY.TabIndex = 92;
            this.TXT01_TGJUNGRY.TabIndexCustom = false;
            // 
            // TYUSAU010B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 92);
            this.Controls.Add(this.GRP01_SEARCH);
            this.Name = "TYUSAU010B";
            this.Load += new System.EventHandler(this.TYUSAU010B_Load);
            this.GRP01_SEARCH.ResumeLayout(false);
            this.GRP01_SEARCH.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYButton BTN61_BATCH;
		private System.Windows.Forms.GroupBox GRP01_SEARCH;
        private Service.Library.Controls.TYLabel LBL51_TGJUNGRY;
        private Service.Library.Controls.TYTextBox TXT01_TGJUNGRY;
    }
}