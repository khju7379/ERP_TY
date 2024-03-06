namespace TY.ER.UT00
{
    /// <summary>
    /// Summary description for TYUTIL014R4.
    /// </summary>
    partial class TYUTIL014R4
    {
        private GrapeCity.ActiveReports.SectionReportModel.Detail detail;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }

        #region ActiveReport Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TYUTIL014R4));
            this.detail = new GrapeCity.ActiveReports.SectionReportModel.Detail();
            this.label6 = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.label7 = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.label8 = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.JLQTY = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            this.JLDANGA = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.JLQTY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.JLDANGA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new GrapeCity.ActiveReports.SectionReportModel.ARControl[] {
            this.label6,
            this.label7,
            this.label8,
            this.JLQTY,
            this.JLDANGA});
            this.detail.Height = 0.25F;
            this.detail.Name = "detail";
            this.detail.Format += new System.EventHandler(this.detail_Format);
            // 
            // label6
            // 
            this.label6.Height = 0.197F;
            this.label6.HyperLink = null;
            this.label6.Left = 0.026F;
            this.label6.Name = "label6";
            this.label6.Style = "font-family: 굴림체; font-size: 9.75pt; font-weight: normal; text-align: center; tex" +
                "t-decoration: none; vertical-align: top";
            this.label6.Text = "(";
            this.label6.Top = 0.035F;
            this.label6.Width = 0.1460002F;
            // 
            // label7
            // 
            this.label7.Height = 0.197F;
            this.label7.HyperLink = null;
            this.label7.Left = 1.308F;
            this.label7.Name = "label7";
            this.label7.Style = "font-family: 굴림체; font-size: 9.75pt; font-weight: normal; text-align: left; text-" +
                "decoration: none; vertical-align: top";
            this.label7.Text = ")  N㎥ * (";
            this.label7.Top = 0.035F;
            this.label7.Width = 0.74F;
            // 
            // label8
            // 
            this.label8.Height = 0.197F;
            this.label8.HyperLink = null;
            this.label8.Left = 2.849F;
            this.label8.Name = "label8";
            this.label8.Style = "font-family: 굴림체; font-size: 9.75pt; font-weight: normal; text-align: left; text-" +
                "decoration: none; vertical-align: top";
            this.label8.Text = ")  \\/N㎥";
            this.label8.Top = 0.035F;
            this.label8.Width = 0.74F;
            // 
            // JLQTY
            // 
            this.JLQTY.DataField = "JLQTY";
            this.JLQTY.Height = 0.197F;
            this.JLQTY.Left = 0.228F;
            this.JLQTY.Name = "JLQTY";
            this.JLQTY.OutputFormat = resources.GetString("JLQTY.OutputFormat");
            this.JLQTY.Style = "font-family: 바탕; font-size: 9.75pt; text-align: right";
            this.JLQTY.Text = "JLQTY";
            this.JLQTY.Top = 0.035F;
            this.JLQTY.Width = 1.022F;
            // 
            // JLDANGA
            // 
            this.JLDANGA.DataField = "JLDANGA";
            this.JLDANGA.Height = 0.197F;
            this.JLDANGA.Left = 2.082F;
            this.JLDANGA.Name = "JLDANGA";
            this.JLDANGA.OutputFormat = resources.GetString("JLDANGA.OutputFormat");
            this.JLDANGA.Style = "font-family: 바탕; font-size: 9.75pt; text-align: right";
            this.JLDANGA.Text = "JLDANGA";
            this.JLDANGA.Top = 0.035F;
            this.JLDANGA.Width = 0.729F;
            // 
            // TYUTIL014R4
            // 
            this.MasterReport = false;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 4.5F;
            this.Sections.Add(this.detail);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
                        "l; font-size: 10pt; color: Black", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
                        "lic", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"));
            ((System.ComponentModel.ISupportInitialize)(this.label6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.JLQTY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.JLDANGA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private GrapeCity.ActiveReports.SectionReportModel.Label label6;
        private GrapeCity.ActiveReports.SectionReportModel.Label label7;
        private GrapeCity.ActiveReports.SectionReportModel.Label label8;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox JLQTY;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox JLDANGA;
    }
}
