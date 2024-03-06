namespace TY.ER.AT00
{
    /// <summary>
    /// Summary description for TYATKB006R3.
    /// </summary>
    partial class TYATKB006R3
    {
        private DataDynamics.ActiveReports.PageHeader pageHeader;
        private DataDynamics.ActiveReports.Detail detail;
        private DataDynamics.ActiveReports.PageFooter pageFooter;

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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TYATKB006R3));
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.APDESC1 = new DataDynamics.ActiveReports.TextBox();
            this.ASRCALBASE = new DataDynamics.ActiveReports.TextBox();
            this.ASRCODEAMT = new DataDynamics.ActiveReports.TextBox();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            ((System.ComponentModel.ISupportInitialize)(this.APDESC1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ASRCALBASE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ASRCODEAMT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Height = 0F;
            this.pageHeader.Name = "pageHeader";
            // 
            // detail
            // 
            this.detail.ColumnSpacing = 0F;
            this.detail.Controls.AddRange(new DataDynamics.ActiveReports.ARControl[] {
            this.APDESC1,
            this.ASRCALBASE,
            this.ASRCODEAMT});
            this.detail.Height = 0.1669999F;
            this.detail.Name = "detail";
            // 
            // APDESC1
            // 
            this.APDESC1.DataField = "APDESC1";
            this.APDESC1.Height = 0.1669999F;
            this.APDESC1.Left = 0.04F;
            this.APDESC1.Name = "APDESC1";
            this.APDESC1.Style = "font-family: 돋움; font-size: 9pt; text-align: left; vertical-align: middle";
            this.APDESC1.Text = "APDESC1";
            this.APDESC1.Top = 0F;
            this.APDESC1.Width = 1.46F;
            // 
            // ASRCALBASE
            // 
            this.ASRCALBASE.DataField = "ASRCALBASE";
            this.ASRCALBASE.Height = 0.1669999F;
            this.ASRCALBASE.Left = 1.58F;
            this.ASRCALBASE.Name = "ASRCALBASE";
            this.ASRCALBASE.Style = "font-family: 돋움; font-size: 9pt; text-align: left; vertical-align: middle";
            this.ASRCALBASE.Text = "ASRCALBASE";
            this.ASRCALBASE.Top = 0F;
            this.ASRCALBASE.Width = 3.346001F;
            // 
            // ASRCODEAMT
            // 
            this.ASRCODEAMT.DataField = "ASRCODEAMT";
            this.ASRCODEAMT.Height = 0.1669999F;
            this.ASRCODEAMT.Left = 5.017F;
            this.ASRCODEAMT.Name = "ASRCODEAMT";
            this.ASRCODEAMT.OutputFormat = resources.GetString("ASRCODEAMT.OutputFormat");
            this.ASRCODEAMT.Style = "font-family: 돋움; font-size: 9pt; text-align: right; vertical-align: middle";
            this.ASRCODEAMT.Text = "ASRCODEAMT";
            this.ASRCODEAMT.Top = 0F;
            this.ASRCODEAMT.Width = 1.231F;
            // 
            // pageFooter
            // 
            this.pageFooter.Height = 0F;
            this.pageFooter.Name = "pageFooter";
            // 
            // TYATKB006R3
            // 
            this.MasterReport = false;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 6.248F;
            this.Sections.Add(this.pageHeader);
            this.Sections.Add(this.detail);
            this.Sections.Add(this.pageFooter);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
                        "l; font-size: 10pt; color: Black", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
                        "lic", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"));
            ((System.ComponentModel.ISupportInitialize)(this.APDESC1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ASRCALBASE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ASRCODEAMT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.TextBox APDESC1;
        private DataDynamics.ActiveReports.TextBox ASRCALBASE;
        private DataDynamics.ActiveReports.TextBox ASRCODEAMT;
    }
}
