namespace TY.ER.US00
{
    partial class TYUSAU005I
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
            this.BTN61_SAV = new TY.Service.Library.Controls.TYButton();
            this.CBO01_GIGAGUBN = new TY.Service.Library.Controls.TYComboBox();
            this.LBL51_GIGAGUBN = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(225, 162);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 0;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // BTN61_SAV
            // 
            this.BTN61_SAV.FactoryID = "";
            this.BTN61_SAV.FactoryName = null;
            this.BTN61_SAV.Location = new System.Drawing.Point(144, 162);
            this.BTN61_SAV.Name = "BTN61_SAV";
            this.BTN61_SAV.Size = new System.Drawing.Size(75, 21);
            this.BTN61_SAV.TabIndex = 1;
            this.BTN61_SAV.Text = "저장";
            this.BTN61_SAV.UseVisualStyleBackColor = true;
            this.BTN61_SAV.Click += new System.EventHandler(this.BTN61_SAV_Click);
            // 
            // CBO01_GIGAGUBN
            // 
            this.CBO01_GIGAGUBN.FactoryID = "";
            this.CBO01_GIGAGUBN.FactoryName = null;
            this.CBO01_GIGAGUBN.Location = new System.Drawing.Point(225, 117);
            this.CBO01_GIGAGUBN.Name = "CBO01_GIGAGUBN";
            this.CBO01_GIGAGUBN.Size = new System.Drawing.Size(100, 20);
            this.CBO01_GIGAGUBN.TabIndex = 2;
            // 
            // LBL51_GIGAGUBN
            // 
            this.LBL51_GIGAGUBN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_GIGAGUBN.FactoryID = "";
            this.LBL51_GIGAGUBN.FactoryName = null;
            this.LBL51_GIGAGUBN.IsCreated = false;
            this.LBL51_GIGAGUBN.Location = new System.Drawing.Point(119, 117);
            this.LBL51_GIGAGUBN.Name = "LBL51_GIGAGUBN";
            this.LBL51_GIGAGUBN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_GIGAGUBN.TabIndex = 3;
            this.LBL51_GIGAGUBN.Text = "무인계근설정";
            this.LBL51_GIGAGUBN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_SAV);
            this.GBX80_CONTROLS.Controls.Add(this.CBO01_GIGAGUBN);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_GIGAGUBN);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(445, 300);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // TYUSAU005I
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 304);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYUSAU005I";
            this.Load += new System.EventHandler(this.TYUSAU005I_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYButton BTN61_SAV;
        private TY.Service.Library.Controls.TYComboBox CBO01_GIGAGUBN;
        private TY.Service.Library.Controls.TYLabel LBL51_GIGAGUBN;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
    }
}