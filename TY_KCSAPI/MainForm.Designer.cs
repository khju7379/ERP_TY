
namespace TY_KCSAPI
{
    partial class MainForm
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
            this.Btn_Send = new System.Windows.Forms.Button();
            this.Btn_Recive = new System.Windows.Forms.Button();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.TXT01_AFFILENAME = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Btn_Send
            // 
            this.Btn_Send.Location = new System.Drawing.Point(22, 12);
            this.Btn_Send.Name = "Btn_Send";
            this.Btn_Send.Size = new System.Drawing.Size(77, 31);
            this.Btn_Send.TabIndex = 0;
            this.Btn_Send.Text = "전 송";
            this.Btn_Send.UseVisualStyleBackColor = true;
            this.Btn_Send.Click += new System.EventHandler(this.Btn_Send_Click);
            // 
            // Btn_Recive
            // 
            this.Btn_Recive.Location = new System.Drawing.Point(105, 12);
            this.Btn_Recive.Name = "Btn_Recive";
            this.Btn_Recive.Size = new System.Drawing.Size(77, 31);
            this.Btn_Recive.TabIndex = 1;
            this.Btn_Recive.Text = "수 신";
            this.Btn_Recive.UseVisualStyleBackColor = true;
            this.Btn_Recive.Click += new System.EventHandler(this.Btn_Recive_Click);
            // 
            // pgBar
            // 
            this.pgBar.Location = new System.Drawing.Point(22, 76);
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(161, 10);
            this.pgBar.TabIndex = 2;
            // 
            // TXT01_AFFILENAME
            // 
            this.TXT01_AFFILENAME.Location = new System.Drawing.Point(22, 49);
            this.TXT01_AFFILENAME.Name = "TXT01_AFFILENAME";
            this.TXT01_AFFILENAME.Size = new System.Drawing.Size(160, 21);
            this.TXT01_AFFILENAME.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(196, 93);
            this.Controls.Add(this.TXT01_AFFILENAME);
            this.Controls.Add(this.pgBar);
            this.Controls.Add(this.Btn_Recive);
            this.Controls.Add(this.Btn_Send);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "세관EDI 전송모듈";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_Send;
        private System.Windows.Forms.Button Btn_Recive;
        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.TextBox TXT01_AFFILENAME;
    }
}