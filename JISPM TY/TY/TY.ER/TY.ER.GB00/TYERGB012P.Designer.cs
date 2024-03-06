namespace TY.ER.GB00
{
    partial class TYERGB012P
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
            this.webB1 = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webB1
            // 
            this.webB1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webB1.Location = new System.Drawing.Point(0, 0);
            this.webB1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webB1.Name = "webB1";
            this.webB1.Size = new System.Drawing.Size(1184, 662);
            this.webB1.TabIndex = 2;
            // 
            // TYERGB012P
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 662);
            this.Controls.Add(this.webB1);
            this.Name = "TYERGB012P";
            this.Text = "TYERGB012P";
            this.Load += new System.EventHandler(this.TYERGB012P_Load);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.WebBrowser webB1;

       
    }
}