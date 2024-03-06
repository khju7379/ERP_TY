namespace TY.ER.UT00
{
    partial class TYUTME24C3
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.PDF81_AcroPDF = new Shoveling2010.SmartClient.SystemUtility.Component.AcrobatPDF();
            this.BTN61_CLO = new TY.Service.Library.Controls.TYButton();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.GBX80_CONTROLS.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.groupBox2);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(3, 2);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1179, 758);
            this.GBX80_CONTROLS.TabIndex = 2;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.PDF81_AcroPDF);
            this.groupBox2.Location = new System.Drawing.Point(0, 39);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1175, 714);
            this.groupBox2.TabIndex = 39;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PDF 상세정보";
            // 
            // PDF81_AcroPDF
            // 
            this.PDF81_AcroPDF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PDF81_AcroPDF.Location = new System.Drawing.Point(3, 17);
            this.PDF81_AcroPDF.Name = "PDF81_AcroPDF";
            this.PDF81_AcroPDF.Size = new System.Drawing.Size(1169, 694);
            this.PDF81_AcroPDF.TabIndex = 37;
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(1098, 12);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 0;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // TYUTME24C3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 762);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYUTME24C3";
            this.Load += new System.EventHandler(this.TYUTME24C3_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private System.Windows.Forms.GroupBox groupBox2;
        private Shoveling2010.SmartClient.SystemUtility.Component.AcrobatPDF PDF81_AcroPDF;
        private Service.Library.Controls.TYButton BTN61_CLO;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;

    }
}