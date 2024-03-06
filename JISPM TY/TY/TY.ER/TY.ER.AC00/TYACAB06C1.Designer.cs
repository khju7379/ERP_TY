namespace TY.ER.AC00
{
    partial class TYACAB06C1
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
            this.BTN61_DWN = new TY.Service.Library.Controls.TYButton();
            this.BTN61_SAV = new TY.Service.Library.Controls.TYButton();
            this.BTN61_SEARCH = new TY.Service.Library.Controls.TYButton();
            this.CBH01_AFVNCODE = new TY.Service.Library.Controls.TYCodeBox();
            this.LBL51_AFVNCODE = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_AFDESC = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_AFDESC = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_AFFILENAME = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_AFFILENAME = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_AFFILESIZE = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_AFFILESIZE = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_AFSEQ = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_AFSEQ = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_ATTACH_FILENAME = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_ATTACH_FILENAME = new TY.Service.Library.Controls.TYLabel();
            this.GBX80_CONTROLS = new System.Windows.Forms.GroupBox();
            this.DTP01_AFDATE = new TY.Service.Library.Controls.TYDatePicker();
            this.LBL51_AFDATE = new TY.Service.Library.Controls.TYLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PDF81_AcroPDF = new Shoveling2010.SmartClient.SystemUtility.Component.AcrobatPDF();
            this.GBX80_CONTROLS.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(1001, 12);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 21);
            this.BTN61_CLO.TabIndex = 0;
            this.BTN61_CLO.Text = "닫기";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // BTN61_DWN
            // 
            this.BTN61_DWN.FactoryID = "";
            this.BTN61_DWN.FactoryName = null;
            this.BTN61_DWN.Location = new System.Drawing.Point(467, 93);
            this.BTN61_DWN.Name = "BTN61_DWN";
            this.BTN61_DWN.Size = new System.Drawing.Size(75, 21);
            this.BTN61_DWN.TabIndex = 1;
            this.BTN61_DWN.Text = "다운";
            this.BTN61_DWN.UseVisualStyleBackColor = true;
            this.BTN61_DWN.Click += new System.EventHandler(this.BTN61_DWN_Click);
            // 
            // BTN61_SAV
            // 
            this.BTN61_SAV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_SAV.FactoryID = "";
            this.BTN61_SAV.FactoryName = null;
            this.BTN61_SAV.Location = new System.Drawing.Point(920, 12);
            this.BTN61_SAV.Name = "BTN61_SAV";
            this.BTN61_SAV.Size = new System.Drawing.Size(75, 21);
            this.BTN61_SAV.TabIndex = 3;
            this.BTN61_SAV.Text = "저장";
            this.BTN61_SAV.UseVisualStyleBackColor = true;
            this.BTN61_SAV.Click += new System.EventHandler(this.BTN61_SAV_Click);
            // 
            // BTN61_SEARCH
            // 
            this.BTN61_SEARCH.FactoryID = "";
            this.BTN61_SEARCH.FactoryName = null;
            this.BTN61_SEARCH.Location = new System.Drawing.Point(467, 66);
            this.BTN61_SEARCH.Name = "BTN61_SEARCH";
            this.BTN61_SEARCH.Size = new System.Drawing.Size(75, 21);
            this.BTN61_SEARCH.TabIndex = 4;
            this.BTN61_SEARCH.Text = "찾아보기";
            this.BTN61_SEARCH.UseVisualStyleBackColor = true;
            this.BTN61_SEARCH.Click += new System.EventHandler(this.BTN61_SEARCH_Click);
            // 
            // CBH01_AFVNCODE
            // 
            this.CBH01_AFVNCODE.BindedDataRow = null;
            this.CBH01_AFVNCODE.CodeBoxWidth = 0;
            this.CBH01_AFVNCODE.DummyValue = null;
            this.CBH01_AFVNCODE.FactoryID = "";
            this.CBH01_AFVNCODE.FactoryName = null;
            this.CBH01_AFVNCODE.Location = new System.Drawing.Point(111, 12);
            this.CBH01_AFVNCODE.MinLength = 0;
            this.CBH01_AFVNCODE.Name = "CBH01_AFVNCODE";
            this.CBH01_AFVNCODE.Size = new System.Drawing.Size(242, 20);
            this.CBH01_AFVNCODE.TabIndex = 5;
            // 
            // LBL51_AFVNCODE
            // 
            this.LBL51_AFVNCODE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_AFVNCODE.FactoryID = "";
            this.LBL51_AFVNCODE.FactoryName = null;
            this.LBL51_AFVNCODE.IsCreated = false;
            this.LBL51_AFVNCODE.Location = new System.Drawing.Point(5, 12);
            this.LBL51_AFVNCODE.Name = "LBL51_AFVNCODE";
            this.LBL51_AFVNCODE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_AFVNCODE.TabIndex = 6;
            this.LBL51_AFVNCODE.Text = "거래처";
            this.LBL51_AFVNCODE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_AFDESC
            // 
            this.TXT01_AFDESC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_AFDESC.FactoryID = "";
            this.TXT01_AFDESC.FactoryName = null;
            this.TXT01_AFDESC.Location = new System.Drawing.Point(111, 120);
            this.TXT01_AFDESC.MinLength = 0;
            this.TXT01_AFDESC.Name = "TXT01_AFDESC";
            this.TXT01_AFDESC.Size = new System.Drawing.Size(350, 21);
            this.TXT01_AFDESC.TabIndex = 9;
            this.TXT01_AFDESC.TabIndexCustom = false;
            // 
            // LBL51_AFDESC
            // 
            this.LBL51_AFDESC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_AFDESC.FactoryID = "";
            this.LBL51_AFDESC.FactoryName = null;
            this.LBL51_AFDESC.IsCreated = false;
            this.LBL51_AFDESC.Location = new System.Drawing.Point(5, 120);
            this.LBL51_AFDESC.Name = "LBL51_AFDESC";
            this.LBL51_AFDESC.Size = new System.Drawing.Size(100, 21);
            this.LBL51_AFDESC.TabIndex = 10;
            this.LBL51_AFDESC.Text = "파일내용";
            this.LBL51_AFDESC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_AFFILENAME
            // 
            this.TXT01_AFFILENAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_AFFILENAME.FactoryID = "";
            this.TXT01_AFFILENAME.FactoryName = null;
            this.TXT01_AFFILENAME.Location = new System.Drawing.Point(111, 93);
            this.TXT01_AFFILENAME.MinLength = 0;
            this.TXT01_AFFILENAME.Name = "TXT01_AFFILENAME";
            this.TXT01_AFFILENAME.Size = new System.Drawing.Size(350, 21);
            this.TXT01_AFFILENAME.TabIndex = 11;
            this.TXT01_AFFILENAME.TabIndexCustom = false;
            // 
            // LBL51_AFFILENAME
            // 
            this.LBL51_AFFILENAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_AFFILENAME.FactoryID = "";
            this.LBL51_AFFILENAME.FactoryName = null;
            this.LBL51_AFFILENAME.IsCreated = false;
            this.LBL51_AFFILENAME.Location = new System.Drawing.Point(5, 93);
            this.LBL51_AFFILENAME.Name = "LBL51_AFFILENAME";
            this.LBL51_AFFILENAME.Size = new System.Drawing.Size(100, 21);
            this.LBL51_AFFILENAME.TabIndex = 12;
            this.LBL51_AFFILENAME.Text = "파일명";
            this.LBL51_AFFILENAME.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_AFFILESIZE
            // 
            this.TXT01_AFFILESIZE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_AFFILESIZE.FactoryID = "";
            this.TXT01_AFFILESIZE.FactoryName = null;
            this.TXT01_AFFILESIZE.Location = new System.Drawing.Point(111, 147);
            this.TXT01_AFFILESIZE.MinLength = 0;
            this.TXT01_AFFILESIZE.Name = "TXT01_AFFILESIZE";
            this.TXT01_AFFILESIZE.Size = new System.Drawing.Size(100, 21);
            this.TXT01_AFFILESIZE.TabIndex = 13;
            this.TXT01_AFFILESIZE.TabIndexCustom = false;
            // 
            // LBL51_AFFILESIZE
            // 
            this.LBL51_AFFILESIZE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_AFFILESIZE.FactoryID = "";
            this.LBL51_AFFILESIZE.FactoryName = null;
            this.LBL51_AFFILESIZE.IsCreated = false;
            this.LBL51_AFFILESIZE.Location = new System.Drawing.Point(5, 147);
            this.LBL51_AFFILESIZE.Name = "LBL51_AFFILESIZE";
            this.LBL51_AFFILESIZE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_AFFILESIZE.TabIndex = 14;
            this.LBL51_AFFILESIZE.Text = "파일용량";
            this.LBL51_AFFILESIZE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_AFSEQ
            // 
            this.TXT01_AFSEQ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_AFSEQ.FactoryID = "";
            this.TXT01_AFSEQ.FactoryName = null;
            this.TXT01_AFSEQ.Location = new System.Drawing.Point(465, 12);
            this.TXT01_AFSEQ.MinLength = 0;
            this.TXT01_AFSEQ.Name = "TXT01_AFSEQ";
            this.TXT01_AFSEQ.Size = new System.Drawing.Size(50, 21);
            this.TXT01_AFSEQ.TabIndex = 15;
            this.TXT01_AFSEQ.TabIndexCustom = false;
            // 
            // LBL51_AFSEQ
            // 
            this.LBL51_AFSEQ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_AFSEQ.FactoryID = "";
            this.LBL51_AFSEQ.FactoryName = null;
            this.LBL51_AFSEQ.IsCreated = false;
            this.LBL51_AFSEQ.Location = new System.Drawing.Point(359, 12);
            this.LBL51_AFSEQ.Name = "LBL51_AFSEQ";
            this.LBL51_AFSEQ.Size = new System.Drawing.Size(100, 21);
            this.LBL51_AFSEQ.TabIndex = 16;
            this.LBL51_AFSEQ.Text = "순번";
            this.LBL51_AFSEQ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_ATTACH_FILENAME
            // 
            this.TXT01_ATTACH_FILENAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TXT01_ATTACH_FILENAME.FactoryID = "";
            this.TXT01_ATTACH_FILENAME.FactoryName = null;
            this.TXT01_ATTACH_FILENAME.Location = new System.Drawing.Point(111, 66);
            this.TXT01_ATTACH_FILENAME.MinLength = 0;
            this.TXT01_ATTACH_FILENAME.Name = "TXT01_ATTACH_FILENAME";
            this.TXT01_ATTACH_FILENAME.Size = new System.Drawing.Size(350, 21);
            this.TXT01_ATTACH_FILENAME.TabIndex = 17;
            this.TXT01_ATTACH_FILENAME.TabIndexCustom = false;
            // 
            // LBL51_ATTACH_FILENAME
            // 
            this.LBL51_ATTACH_FILENAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_ATTACH_FILENAME.FactoryID = "";
            this.LBL51_ATTACH_FILENAME.FactoryName = null;
            this.LBL51_ATTACH_FILENAME.IsCreated = false;
            this.LBL51_ATTACH_FILENAME.Location = new System.Drawing.Point(5, 66);
            this.LBL51_ATTACH_FILENAME.Name = "LBL51_ATTACH_FILENAME";
            this.LBL51_ATTACH_FILENAME.Size = new System.Drawing.Size(100, 21);
            this.LBL51_ATTACH_FILENAME.TabIndex = 18;
            this.LBL51_ATTACH_FILENAME.Text = "파일경로";
            this.LBL51_ATTACH_FILENAME.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GBX80_CONTROLS
            // 
            this.GBX80_CONTROLS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX80_CONTROLS.Controls.Add(this.groupBox1);
            this.GBX80_CONTROLS.Controls.Add(this.DTP01_AFDATE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_AFDATE);
            this.GBX80_CONTROLS.Controls.Add(this.label1);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_CLO);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_DWN);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_SAV);
            this.GBX80_CONTROLS.Controls.Add(this.BTN61_SEARCH);
            this.GBX80_CONTROLS.Controls.Add(this.CBH01_AFVNCODE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_AFVNCODE);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_AFDESC);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_AFDESC);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_AFFILENAME);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_AFFILENAME);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_AFFILESIZE);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_AFFILESIZE);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_AFSEQ);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_AFSEQ);
            this.GBX80_CONTROLS.Controls.Add(this.TXT01_ATTACH_FILENAME);
            this.GBX80_CONTROLS.Controls.Add(this.LBL51_ATTACH_FILENAME);
            this.GBX80_CONTROLS.Location = new System.Drawing.Point(2, 1);
            this.GBX80_CONTROLS.Name = "GBX80_CONTROLS";
            this.GBX80_CONTROLS.Size = new System.Drawing.Size(1082, 758);
            this.GBX80_CONTROLS.TabIndex = 1;
            this.GBX80_CONTROLS.TabStop = false;
            // 
            // DTP01_AFDATE
            // 
            this.DTP01_AFDATE.FactoryID = "";
            this.DTP01_AFDATE.FactoryName = null;
            this.DTP01_AFDATE.Location = new System.Drawing.Point(111, 38);
            this.DTP01_AFDATE.Name = "DTP01_AFDATE";
            this.DTP01_AFDATE.Size = new System.Drawing.Size(100, 21);
            this.DTP01_AFDATE.TabIndex = 174;
            // 
            // LBL51_AFDATE
            // 
            this.LBL51_AFDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(198)))), ((int)(((byte)(215)))));
            this.LBL51_AFDATE.FactoryID = "";
            this.LBL51_AFDATE.FactoryName = null;
            this.LBL51_AFDATE.IsCreated = false;
            this.LBL51_AFDATE.Location = new System.Drawing.Point(5, 38);
            this.LBL51_AFDATE.Name = "LBL51_AFDATE";
            this.LBL51_AFDATE.Size = new System.Drawing.Size(100, 21);
            this.LBL51_AFDATE.TabIndex = 173;
            this.LBL51_AFDATE.Text = "등록일자";
            this.LBL51_AFDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(214, 152);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 12);
            this.label1.TabIndex = 172;
            this.label1.Text = "(Byte)";
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileName = "OpenFileDialog";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.PDF81_AcroPDF);
            this.groupBox1.Location = new System.Drawing.Point(2, 179);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1077, 574);
            this.groupBox1.TabIndex = 175;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "미리보기";
            // 
            // PDF81_AcroPDF
            // 
            this.PDF81_AcroPDF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PDF81_AcroPDF.Location = new System.Drawing.Point(3, 17);
            this.PDF81_AcroPDF.Name = "PDF81_AcroPDF";
            this.PDF81_AcroPDF.Size = new System.Drawing.Size(1071, 554);
            this.PDF81_AcroPDF.TabIndex = 38;
            // 
            // TYACAB06C1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1091, 762);
            this.Controls.Add(this.GBX80_CONTROLS);
            this.Name = "TYACAB06C1";
            this.Load += new System.EventHandler(this.TYHRKB03C2_Load);
            this.GBX80_CONTROLS.ResumeLayout(false);
            this.GBX80_CONTROLS.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        
        private TY.Service.Library.Controls.TYButton BTN61_CLO;
        private TY.Service.Library.Controls.TYButton BTN61_DWN;
        private TY.Service.Library.Controls.TYButton BTN61_SAV;
        private TY.Service.Library.Controls.TYButton BTN61_SEARCH;
        private TY.Service.Library.Controls.TYCodeBox CBH01_AFVNCODE;
        private TY.Service.Library.Controls.TYLabel LBL51_AFVNCODE;
        private TY.Service.Library.Controls.TYTextBox TXT01_AFDESC;
        private TY.Service.Library.Controls.TYLabel LBL51_AFDESC;
        private TY.Service.Library.Controls.TYTextBox TXT01_AFFILENAME;
        private TY.Service.Library.Controls.TYLabel LBL51_AFFILENAME;
        private TY.Service.Library.Controls.TYTextBox TXT01_AFFILESIZE;
        private TY.Service.Library.Controls.TYLabel LBL51_AFFILESIZE;
        private TY.Service.Library.Controls.TYTextBox TXT01_AFSEQ;
        private TY.Service.Library.Controls.TYLabel LBL51_AFSEQ;
        private TY.Service.Library.Controls.TYTextBox TXT01_ATTACH_FILENAME;
        private TY.Service.Library.Controls.TYLabel LBL51_ATTACH_FILENAME;
		private System.Windows.Forms.GroupBox GBX80_CONTROLS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private Service.Library.Controls.TYLabel LBL51_AFDATE;
        private Service.Library.Controls.TYDatePicker DTP01_AFDATE;
        private System.Windows.Forms.GroupBox groupBox1;
        private Shoveling2010.SmartClient.SystemUtility.Component.AcrobatPDF PDF81_AcroPDF;
    }
}