namespace TY.ER.UT00
{
    /// <summary>
    /// Summary description for TYUTME034R.
    /// </summary>
    partial class TYUTME034R
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TYUTME034R));
            this.pageHeader = new DataDynamics.ActiveReports.PageHeader();
            this.detail = new DataDynamics.ActiveReports.Detail();
            this.shape1 = new DataDynamics.ActiveReports.Shape();
            this.label1 = new DataDynamics.ActiveReports.Label();
            this.label2 = new DataDynamics.ActiveReports.Label();
            this.label4 = new DataDynamics.ActiveReports.Label();
            this.label5 = new DataDynamics.ActiveReports.Label();
            this.line1 = new DataDynamics.ActiveReports.Line();
            this.line2 = new DataDynamics.ActiveReports.Line();
            this.label6 = new DataDynamics.ActiveReports.Label();
            this.picture1 = new DataDynamics.ActiveReports.Picture();
            this.DATE2 = new DataDynamics.ActiveReports.TextBox();
            this.DATE1 = new DataDynamics.ActiveReports.TextBox();
            this.VNSANGHO = new DataDynamics.ActiveReports.TextBox();
            this.COUNT = new DataDynamics.ActiveReports.TextBox();
            this.M3ENHMAM = new DataDynamics.ActiveReports.TextBox();
            this.pageFooter = new DataDynamics.ActiveReports.PageFooter();
            ((System.ComponentModel.ISupportInitialize)(this.label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DATE2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DATE1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VNSANGHO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.COUNT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.M3ENHMAM)).BeginInit();
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
            this.shape1,
            this.label1,
            this.label2,
            this.label4,
            this.label5,
            this.line1,
            this.line2,
            this.label6,
            this.picture1,
            this.DATE2,
            this.DATE1,
            this.VNSANGHO,
            this.COUNT,
            this.M3ENHMAM});
            this.detail.Height = 4.979167F;
            this.detail.Name = "detail";
            this.detail.Format += new System.EventHandler(this.detail_Format);
            // 
            // shape1
            // 
            this.shape1.Height = 4.5F;
            this.shape1.Left = 1.25F;
            this.shape1.LineWeight = 4F;
            this.shape1.Name = "shape1";
            this.shape1.RoundingRadius = 9.999999F;
            this.shape1.Top = 0.27F;
            this.shape1.Width = 5.27F;
            // 
            // label1
            // 
            this.label1.Height = 0.281F;
            this.label1.HyperLink = null;
            this.label1.Left = 3.131999F;
            this.label1.Name = "label1";
            this.label1.Style = "font-family: 굴림; font-size: 15pt; font-weight: bold; text-align: left; ddo-char-s" +
    "et: 0";
            this.label1.Text = "월분 화물료 영수증";
            this.label1.Top = 0.4699999F;
            this.label1.Width = 1.857F;
            // 
            // label2
            // 
            this.label2.Height = 0.208F;
            this.label2.HyperLink = null;
            this.label2.Left = 2.171999F;
            this.label2.Name = "label2";
            this.label2.Style = "font-family: 굴림; font-size: 12pt; font-weight: bold; ddo-char-set: 1";
            this.label2.Text = "화   주   명";
            this.label2.Top = 1.399F;
            this.label2.Width = 0.99F;
            // 
            // label4
            // 
            this.label4.Height = 0.208F;
            this.label4.HyperLink = null;
            this.label4.Left = 2.171999F;
            this.label4.Name = "label4";
            this.label4.Style = "font-family: 굴림; font-size: 12pt; font-weight: bold; ddo-char-set: 1";
            this.label4.Text = "건         수";
            this.label4.Top = 2.143F;
            this.label4.Width = 0.9899997F;
            // 
            // label5
            // 
            this.label5.Height = 0.208F;
            this.label5.HyperLink = null;
            this.label5.Left = 2.171999F;
            this.label5.Name = "label5";
            this.label5.Style = "font-family: 굴림; font-size: 12pt; font-weight: bold; ddo-char-set: 1";
            this.label5.Text = "금         액";
            this.label5.Top = 2.903F;
            this.label5.Width = 0.9899997F;
            // 
            // line1
            // 
            this.line1.Height = 0.0001664162F;
            this.line1.Left = 2.704999F;
            this.line1.LineWeight = 2F;
            this.line1.Name = "line1";
            this.line1.Top = 0.7638334F;
            this.line1.Width = 2.390021F;
            this.line1.X1 = 2.704999F;
            this.line1.X2 = 5.09502F;
            this.line1.Y1 = 0.7639998F;
            this.line1.Y2 = 0.7638334F;
            // 
            // line2
            // 
            this.line2.Height = 0.0001659393F;
            this.line2.Left = 1.999999F;
            this.line2.LineWeight = 2F;
            this.line2.Name = "line2";
            this.line2.Top = 3.106834F;
            this.line2.Width = 3.760029F;
            this.line2.X1 = 1.999999F;
            this.line2.X2 = 5.760028F;
            this.line2.Y1 = 3.107F;
            this.line2.Y2 = 3.106834F;
            // 
            // label6
            // 
            this.label6.Height = 0.208F;
            this.label6.HyperLink = null;
            this.label6.Left = 3.328999F;
            this.label6.Name = "label6";
            this.label6.Style = "font-family: 굴림; font-size: 12pt; font-weight: bold; ddo-char-set: 1";
            this.label6.Text = "￦";
            this.label6.Top = 2.903F;
            this.label6.Width = 0.1900001F;
            // 
            // picture1
            // 
            this.picture1.Height = 0.752F;
            this.picture1.HyperLink = null;
            this.picture1.ImageData = ((System.IO.Stream)(resources.GetObject("picture1.ImageData")));
            this.picture1.Left = 2.911999F;
            this.picture1.Name = "picture1";
            this.picture1.SizeMode = DataDynamics.ActiveReports.SizeModes.Stretch;
            this.picture1.Top = 3.622F;
            this.picture1.Width = 1.962F;
            // 
            // DATE2
            // 
            this.DATE2.DataField = "DATE2";
            this.DATE2.Height = 0.2809998F;
            this.DATE2.Left = 2.796999F;
            this.DATE2.Name = "DATE2";
            this.DATE2.Style = "font-family: 굴림; font-size: 15pt; font-style: normal; font-weight: bold; text-ali" +
    "gn: right; ddo-char-set: 129";
            this.DATE2.Text = "DATE2";
            this.DATE2.Top = 0.4699999F;
            this.DATE2.Width = 0.318F;
            // 
            // DATE1
            // 
            this.DATE1.DataField = "DATE1";
            this.DATE1.Height = 0.198F;
            this.DATE1.Left = 2.341999F;
            this.DATE1.Name = "DATE1";
            this.DATE1.Style = "font-family: 굴림; font-size: 15pt; font-style: normal; font-weight: bold; text-ali" +
    "gn: center; ddo-char-set: 129";
            this.DATE1.Text = "DATE1";
            this.DATE1.Top = 0.8189998F;
            this.DATE1.Width = 3.01F;
            // 
            // VNSANGHO
            // 
            this.VNSANGHO.DataField = "VNSANGHO";
            this.VNSANGHO.Height = 0.208F;
            this.VNSANGHO.Left = 3.328999F;
            this.VNSANGHO.Name = "VNSANGHO";
            this.VNSANGHO.Style = "font-family: 굴림; font-size: 12pt; font-style: normal; font-weight: bold; ddo-char" +
    "-set: 1";
            this.VNSANGHO.Text = "VNSANGHO";
            this.VNSANGHO.Top = 1.399F;
            this.VNSANGHO.Width = 2.23F;
            // 
            // COUNT
            // 
            this.COUNT.DataField = "COUNT";
            this.COUNT.Height = 0.208F;
            this.COUNT.Left = 3.328999F;
            this.COUNT.Name = "COUNT";
            this.COUNT.Style = "font-family: 굴림; font-size: 12pt; font-style: normal; font-weight: bold; ddo-char" +
    "-set: 1";
            this.COUNT.Text = "COUNT";
            this.COUNT.Top = 2.143F;
            this.COUNT.Width = 2.23F;
            // 
            // M3ENHMAM
            // 
            this.M3ENHMAM.DataField = "M3ENHMAM";
            this.M3ENHMAM.Height = 0.208F;
            this.M3ENHMAM.Left = 3.556999F;
            this.M3ENHMAM.Name = "M3ENHMAM";
            this.M3ENHMAM.OutputFormat = resources.GetString("M3ENHMAM.OutputFormat");
            this.M3ENHMAM.Style = "font-family: 굴림; font-size: 12pt; font-style: normal; font-weight: bold; text-ali" +
    "gn: left; ddo-char-set: 1";
            this.M3ENHMAM.Text = "M3ENHMAM";
            this.M3ENHMAM.Top = 2.903F;
            this.M3ENHMAM.Width = 1.709F;
            // 
            // pageFooter
            // 
            this.pageFooter.Height = 0F;
            this.pageFooter.Name = "pageFooter";
            // 
            // TYUTME034R
            // 
            this.MasterReport = false;
            this.PageSettings.Margins.Bottom = 0.5F;
            this.PageSettings.Margins.Left = 0.2F;
            this.PageSettings.Margins.Right = 0F;
            this.PageSettings.Margins.Top = 0.75F;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 7.842916F;
            this.Sections.Add(this.pageHeader);
            this.Sections.Add(this.detail);
            this.Sections.Add(this.pageFooter);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
            "l; font-size: 10pt; color: Black", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
            "lic", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"));
            ((System.ComponentModel.ISupportInitialize)(this.label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DATE2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DATE1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VNSANGHO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.COUNT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.M3ENHMAM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private DataDynamics.ActiveReports.Shape shape1;
        private DataDynamics.ActiveReports.Label label1;
        private DataDynamics.ActiveReports.Label label2;
        private DataDynamics.ActiveReports.Label label4;
        private DataDynamics.ActiveReports.Label label5;
        private DataDynamics.ActiveReports.Line line1;
        private DataDynamics.ActiveReports.Line line2;
        private DataDynamics.ActiveReports.Label label6;
        private DataDynamics.ActiveReports.Picture picture1;
        private DataDynamics.ActiveReports.TextBox DATE2;
        private DataDynamics.ActiveReports.TextBox DATE1;
        private DataDynamics.ActiveReports.TextBox VNSANGHO;
        private DataDynamics.ActiveReports.TextBox COUNT;
        private DataDynamics.ActiveReports.TextBox M3ENHMAM;
    }
}
