namespace TY.ER.GB00
{
    partial class TYERGB001P
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
            this.AVW01_REPORT = new GrapeCity.ActiveReports.Viewer.Win.Viewer();
            this.GBX01_REPORT = new System.Windows.Forms.GroupBox();
            this.BTN61_CLO = new TY.Service.Library.Controls.TYButton();
            this.GBX01_REPORT.SuspendLayout();
            this.SuspendLayout();
            // 
            // AVW01_REPORT
            // 
            this.AVW01_REPORT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AVW01_REPORT.BackColor = System.Drawing.SystemColors.Control;
            this.AVW01_REPORT.CurrentPage = 0;
            this.AVW01_REPORT.Location = new System.Drawing.Point(6, 48);
            this.AVW01_REPORT.Name = "AVW01_REPORT";
            this.AVW01_REPORT.PreviewPages = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            this.AVW01_REPORT.Sidebar.ParametersPanel.ContextMenu = null;
            this.AVW01_REPORT.Sidebar.ParametersPanel.Width = 200;
            // 
            // 
            // 
            this.AVW01_REPORT.Sidebar.SearchPanel.ContextMenu = null;
            this.AVW01_REPORT.Sidebar.SearchPanel.Width = 200;
            // 
            // 
            // 
            this.AVW01_REPORT.Sidebar.ThumbnailsPanel.ContextMenu = null;
            this.AVW01_REPORT.Sidebar.ThumbnailsPanel.Width = 200;
            this.AVW01_REPORT.Sidebar.ThumbnailsPanel.Zoom = 0.1D;
            // 
            // 
            // 
            this.AVW01_REPORT.Sidebar.TocPanel.ContextMenu = null;
            this.AVW01_REPORT.Sidebar.TocPanel.Expanded = true;
            this.AVW01_REPORT.Sidebar.TocPanel.Text = "Table Of Contents";
            this.AVW01_REPORT.Sidebar.TocPanel.Width = 200;
            this.AVW01_REPORT.Sidebar.Width = 200;
            this.AVW01_REPORT.Size = new System.Drawing.Size(1147, 583);
            this.AVW01_REPORT.TabIndex = 0;
            // 
            // GBX01_REPORT
            // 
            this.GBX01_REPORT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX01_REPORT.Controls.Add(this.BTN61_CLO);
            this.GBX01_REPORT.Controls.Add(this.AVW01_REPORT);
            this.GBX01_REPORT.Location = new System.Drawing.Point(13, 13);
            this.GBX01_REPORT.Name = "GBX01_REPORT";
            this.GBX01_REPORT.Size = new System.Drawing.Size(1159, 637);
            this.GBX01_REPORT.TabIndex = 1;
            this.GBX01_REPORT.TabStop = false;
            this.GBX01_REPORT.Text = "레포트";
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(1078, 20);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 22);
            this.BTN61_CLO.TabIndex = 2;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // TYERGB001P
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 662);
            this.Controls.Add(this.GBX01_REPORT);
            this.Name = "TYERGB001P";
            this.Text = "TYERGB001P";
            this.GBX01_REPORT.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GrapeCity.ActiveReports.Viewer.Win.Viewer AVW01_REPORT;
        private System.Windows.Forms.GroupBox GBX01_REPORT;
        private Service.Library.Controls.TYButton BTN61_CLO;
    }
}