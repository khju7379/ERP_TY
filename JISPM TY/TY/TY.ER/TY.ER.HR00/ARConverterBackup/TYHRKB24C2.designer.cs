namespace TY.ER.HR00
{
    partial class TYHRKB24C2
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
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.RDBPRTCHECK2 = new System.Windows.Forms.RadioButton();
            this.RDBPRTCHECK1 = new System.Windows.Forms.RadioButton();
            this.BTN61_PRT = new TY.Service.Library.Controls.TYButton();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(177, 74);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 0;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Controls.Add(this.RDBPRTCHECK2);
            this.GBX80_CONTROLS.Controls.Add(this.RDBPRTCHECK1);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_PRT);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(359, 131);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // RDBPRTCHECK2
            // 
            this.RDBPRTCHECK2.AutoSize = true;
            this.RDBPRTCHECK2.Location = new System.Drawing.Point(177, 30);
            this.RDBPRTCHECK2.Name = "RDBPRTCHECK2";
            this.RDBPRTCHECK2.Size = new System.Drawing.Size(131, 16);
            this.RDBPRTCHECK2.TabIndex = 6;
            this.RDBPRTCHECK2.TabStop = true;
            this.RDBPRTCHECK2.Text = "퇴직소득원천영수증";
            this.RDBPRTCHECK2.UseVisualStyleBackColor = true;
            // 
            // RDBPRTCHECK1
            // 
            this.RDBPRTCHECK1.AutoSize = true;
            this.RDBPRTCHECK1.Location = new System.Drawing.Point(76, 30);
            this.RDBPRTCHECK1.Name = "RDBPRTCHECK1";
            this.RDBPRTCHECK1.Size = new System.Drawing.Size(95, 16);
            this.RDBPRTCHECK1.TabIndex = 5;
            this.RDBPRTCHECK1.TabStop = true;
            this.RDBPRTCHECK1.Text = "퇴직금내역서";
            this.RDBPRTCHECK1.UseVisualStyleBackColor = true;
            // 
            // BTN61_PRT
            // 
            this.BTN61_PRT.FactoryID = "";
            this.BTN61_PRT.FactoryName = null;
            this.BTN61_PRT.Location = new System.Drawing.Point(96, 74);
            this.BTN61_PRT.Name = "BTN61_PRT";
            this.BTN61_PRT.Size = new System.Drawing.Size(75, 21);
            this.BTN61_PRT.TabIndex = 4;
            this.BTN61_PRT.Text = "출력";
            this.BTN61_PRT.UseVisualStyleBackColor = true;
            this.BTN61_PRT.Click += new System.EventHandler(this.BTN61_PRT_Click);
            // 
            // TYHRKB24C2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 135);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYHRKB24C2";
            this.Load += new System.EventHandler(this.TYHRKB24C2_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TY.Service.Library.Controls.TYButton BTN61_CLO;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private Service.Library.Controls.TYButton BTN61_PRT;
        private System.Windows.Forms.RadioButton RDBPRTCHECK2;
        private System.Windows.Forms.RadioButton RDBPRTCHECK1;
    }
}