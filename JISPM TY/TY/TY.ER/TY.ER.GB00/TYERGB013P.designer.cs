namespace TY.ER.AC00
{
    partial class TYERGB013P
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
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.webB1 = new System.Windows.Forms.WebBrowser();
            this.GBX80_CONTROLS.SuspendLayout();
            this.SuspendLayout();
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Controls.Add(this.webB1);
            this.GBX80_CONTROLS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(0, 0);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(811, 364);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // webB1
            // 
            this.webB1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webB1.Location = new System.Drawing.Point(3, 17);
            this.webB1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webB1.Name = "webB1";
            this.webB1.Size = new System.Drawing.Size(805, 344);
            this.webB1.TabIndex = 3;
            // 
            // TYERGB013P
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 364);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYERGB013P";
            this.Load += new System.EventHandler(this.TYERGB013P_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TYERGB013P_FormClosed);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private System.Windows.Forms.WebBrowser webB1;
    }
}