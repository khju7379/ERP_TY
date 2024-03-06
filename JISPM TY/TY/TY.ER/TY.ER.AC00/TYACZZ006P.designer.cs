namespace TY.ER.AC00
{
    partial class TYACZZ006P
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
            this.webB1 = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(468, 12);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(159, 42);
            this.BTN61_CLO.TabIndex = 6;
            this.BTN61_CLO.Text = "´Ý±â";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // webB1
            // 
            this.webB1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webB1.Location = new System.Drawing.Point(0, 63);
            this.webB1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webB1.Name = "webB1";
            this.webB1.Size = new System.Drawing.Size(1200, 831);
            this.webB1.TabIndex = 5;
            this.webB1.Visible = false;
            this.webB1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webB1_DocumentCompleted);
            // 
            // TYACZZ006P
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 900);
            this.Controls.Add(this.webB1);
            this.Controls.Add(this.BTN61_CLO);
            this.Name = "TYACZZ006P";
            this.Load += new System.EventHandler(this.TYACZZ006P_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Service.Library.Controls.TYButton BTN61_CLO;
        private System.Windows.Forms.WebBrowser webB1;


    }
}