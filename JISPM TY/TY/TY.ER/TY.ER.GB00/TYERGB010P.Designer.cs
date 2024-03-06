namespace TY.ER.GB00
{
    partial class TYERGB010P
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
            this.GBX01_CLAIM = new System.Windows.Forms.GroupBox();
            this.GBX01_WORK = new System.Windows.Forms.GroupBox();
            this.TXT01_CLAIM_NO = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_CLAIM_NO = new TY.Service.Library.Controls.TYLabel();
            this.BTN61_CLO = new TY.Service.Library.Controls.TYButton();
            this.TXT01_REG_DTM = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_PROGRAM_NAME = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_PROGRAM_NAME = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_REG_DTM = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_REQ_COMMENT = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_REQ_COMMENT = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_NOTE = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_NOTE = new TY.Service.Library.Controls.TYLabel();
            this.LBL51_ATTACH_FILENAME = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_ATTACH_FILENAME = new TY.Service.Library.Controls.TYTextBox();
            this.BTN61_DWN = new TY.Service.Library.Controls.TYButton();
            this.LBL51_DEV_NAME = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_DEV_NAME = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_WORK_START_DTM = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_WORK_START_DTM = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_WORK_END_DTM = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_WORK_END_DTM = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_PROGRAM_NO = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_PROGRAM_NO = new TY.Service.Library.Controls.TYTextBox();
            this.LBL51_DISPOSAL_YN = new TY.Service.Library.Controls.TYLabel();
            this.CKB01_DISPOSAL_YN = new TY.Service.Library.Controls.TYCheckBox();
            this.LBL51_WORK_COMMENT = new TY.Service.Library.Controls.TYLabel();
            this.TXT01_WORK_COMMENT = new TY.Service.Library.Controls.TYTextBox();
            this.sfd01_FILE = new System.Windows.Forms.SaveFileDialog();
            this.GBX01_CLAIM.SuspendLayout();
            this.GBX01_WORK.SuspendLayout();
            this.SuspendLayout();
            // 
            // GBX01_CLAIM
            // 
            this.GBX01_CLAIM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX01_CLAIM.Controls.Add(this.BTN61_DWN);
            this.GBX01_CLAIM.Controls.Add(this.TXT01_NOTE);
            this.GBX01_CLAIM.Controls.Add(this.LBL51_NOTE);
            this.GBX01_CLAIM.Controls.Add(this.TXT01_REQ_COMMENT);
            this.GBX01_CLAIM.Controls.Add(this.TXT01_ATTACH_FILENAME);
            this.GBX01_CLAIM.Controls.Add(this.TXT01_PROGRAM_NAME);
            this.GBX01_CLAIM.Controls.Add(this.TXT01_REG_DTM);
            this.GBX01_CLAIM.Controls.Add(this.LBL51_REG_DTM);
            this.GBX01_CLAIM.Controls.Add(this.LBL51_ATTACH_FILENAME);
            this.GBX01_CLAIM.Controls.Add(this.LBL51_REQ_COMMENT);
            this.GBX01_CLAIM.Controls.Add(this.LBL51_PROGRAM_NAME);
            this.GBX01_CLAIM.Controls.Add(this.BTN61_CLO);
            this.GBX01_CLAIM.Controls.Add(this.TXT01_PROGRAM_NO);
            this.GBX01_CLAIM.Controls.Add(this.LBL51_PROGRAM_NO);
            this.GBX01_CLAIM.Controls.Add(this.TXT01_CLAIM_NO);
            this.GBX01_CLAIM.Controls.Add(this.LBL51_CLAIM_NO);
            this.GBX01_CLAIM.Location = new System.Drawing.Point(2, 2);
            this.GBX01_CLAIM.Name = "GBX01_CLAIM";
            this.GBX01_CLAIM.Size = new System.Drawing.Size(1021, 324);
            this.GBX01_CLAIM.TabIndex = 1;
            this.GBX01_CLAIM.TabStop = false;
            this.GBX01_CLAIM.Text = "요구사항";
            // 
            // GBX01_WORK
            // 
            this.GBX01_WORK.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBX01_WORK.Controls.Add(this.CKB01_DISPOSAL_YN);
            this.GBX01_WORK.Controls.Add(this.TXT01_WORK_END_DTM);
            this.GBX01_WORK.Controls.Add(this.LBL51_DISPOSAL_YN);
            this.GBX01_WORK.Controls.Add(this.TXT01_WORK_COMMENT);
            this.GBX01_WORK.Controls.Add(this.LBL51_WORK_END_DTM);
            this.GBX01_WORK.Controls.Add(this.TXT01_WORK_START_DTM);
            this.GBX01_WORK.Controls.Add(this.LBL51_WORK_START_DTM);
            this.GBX01_WORK.Controls.Add(this.TXT01_DEV_NAME);
            this.GBX01_WORK.Controls.Add(this.LBL51_DEV_NAME);
            this.GBX01_WORK.Controls.Add(this.LBL51_WORK_COMMENT);
            this.GBX01_WORK.Location = new System.Drawing.Point(2, 332);
            this.GBX01_WORK.Name = "GBX01_WORK";
            this.GBX01_WORK.Size = new System.Drawing.Size(1021, 267);
            this.GBX01_WORK.TabIndex = 2;
            this.GBX01_WORK.TabStop = false;
            this.GBX01_WORK.Text = "처리현황";
            // 
            // TXT01_CLAIM_NO
            // 
            this.TXT01_CLAIM_NO.FactoryID = "";
            this.TXT01_CLAIM_NO.FactoryName = null;
            this.TXT01_CLAIM_NO.Location = new System.Drawing.Point(116, 20);
            this.TXT01_CLAIM_NO.MinLength = 0;
            this.TXT01_CLAIM_NO.Name = "TXT01_CLAIM_NO";
            this.TXT01_CLAIM_NO.Size = new System.Drawing.Size(120, 21);
            this.TXT01_CLAIM_NO.TabIndex = 6;
            // 
            // LBL51_CLAIM_NO
            // 
            this.LBL51_CLAIM_NO.BackColor = System.Drawing.SystemColors.Control;
            this.LBL51_CLAIM_NO.FactoryID = "";
            this.LBL51_CLAIM_NO.FactoryName = null;
            this.LBL51_CLAIM_NO.Location = new System.Drawing.Point(10, 20);
            this.LBL51_CLAIM_NO.Name = "LBL51_CLAIM_NO";
            this.LBL51_CLAIM_NO.Size = new System.Drawing.Size(100, 21);
            this.LBL51_CLAIM_NO.TabIndex = 5;
            this.LBL51_CLAIM_NO.Text = "tyLabel1";
            this.LBL51_CLAIM_NO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BTN61_CLO
            // 
            this.BTN61_CLO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_CLO.FactoryID = "";
            this.BTN61_CLO.FactoryName = null;
            this.BTN61_CLO.Location = new System.Drawing.Point(936, 19);
            this.BTN61_CLO.Name = "BTN61_CLO";
            this.BTN61_CLO.Size = new System.Drawing.Size(75, 23);
            this.BTN61_CLO.TabIndex = 7;
            this.BTN61_CLO.Text = "tyButton1";
            this.BTN61_CLO.UseVisualStyleBackColor = true;
            this.BTN61_CLO.Click += new System.EventHandler(this.BTN61_CLO_Click);
            // 
            // TXT01_REG_DTM
            // 
            this.TXT01_REG_DTM.FactoryID = "";
            this.TXT01_REG_DTM.FactoryName = null;
            this.TXT01_REG_DTM.Location = new System.Drawing.Point(348, 20);
            this.TXT01_REG_DTM.MinLength = 0;
            this.TXT01_REG_DTM.Name = "TXT01_REG_DTM";
            this.TXT01_REG_DTM.Size = new System.Drawing.Size(120, 21);
            this.TXT01_REG_DTM.TabIndex = 9;
            // 
            // LBL51_PROGRAM_NAME
            // 
            this.LBL51_PROGRAM_NAME.BackColor = System.Drawing.SystemColors.Control;
            this.LBL51_PROGRAM_NAME.FactoryID = "";
            this.LBL51_PROGRAM_NAME.FactoryName = null;
            this.LBL51_PROGRAM_NAME.Location = new System.Drawing.Point(242, 46);
            this.LBL51_PROGRAM_NAME.Name = "LBL51_PROGRAM_NAME";
            this.LBL51_PROGRAM_NAME.Size = new System.Drawing.Size(100, 21);
            this.LBL51_PROGRAM_NAME.TabIndex = 8;
            this.LBL51_PROGRAM_NAME.Text = "tyLabel1";
            this.LBL51_PROGRAM_NAME.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_PROGRAM_NAME
            // 
            this.TXT01_PROGRAM_NAME.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TXT01_PROGRAM_NAME.FactoryID = "";
            this.TXT01_PROGRAM_NAME.FactoryName = null;
            this.TXT01_PROGRAM_NAME.Location = new System.Drawing.Point(348, 47);
            this.TXT01_PROGRAM_NAME.MinLength = 0;
            this.TXT01_PROGRAM_NAME.Name = "TXT01_PROGRAM_NAME";
            this.TXT01_PROGRAM_NAME.Size = new System.Drawing.Size(663, 21);
            this.TXT01_PROGRAM_NAME.TabIndex = 11;
            // 
            // LBL51_REG_DTM
            // 
            this.LBL51_REG_DTM.BackColor = System.Drawing.SystemColors.Control;
            this.LBL51_REG_DTM.FactoryID = "";
            this.LBL51_REG_DTM.FactoryName = null;
            this.LBL51_REG_DTM.Location = new System.Drawing.Point(242, 20);
            this.LBL51_REG_DTM.Name = "LBL51_REG_DTM";
            this.LBL51_REG_DTM.Size = new System.Drawing.Size(100, 21);
            this.LBL51_REG_DTM.TabIndex = 8;
            this.LBL51_REG_DTM.Text = "tyLabel1";
            this.LBL51_REG_DTM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_REQ_COMMENT
            // 
            this.TXT01_REQ_COMMENT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TXT01_REQ_COMMENT.FactoryID = "";
            this.TXT01_REQ_COMMENT.FactoryName = null;
            this.TXT01_REQ_COMMENT.Location = new System.Drawing.Point(116, 74);
            this.TXT01_REQ_COMMENT.MinLength = 0;
            this.TXT01_REQ_COMMENT.Multiline = true;
            this.TXT01_REQ_COMMENT.Name = "TXT01_REQ_COMMENT";
            this.TXT01_REQ_COMMENT.Size = new System.Drawing.Size(895, 150);
            this.TXT01_REQ_COMMENT.TabIndex = 11;
            // 
            // LBL51_REQ_COMMENT
            // 
            this.LBL51_REQ_COMMENT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.LBL51_REQ_COMMENT.BackColor = System.Drawing.SystemColors.Control;
            this.LBL51_REQ_COMMENT.FactoryID = "";
            this.LBL51_REQ_COMMENT.FactoryName = null;
            this.LBL51_REQ_COMMENT.Location = new System.Drawing.Point(10, 74);
            this.LBL51_REQ_COMMENT.Name = "LBL51_REQ_COMMENT";
            this.LBL51_REQ_COMMENT.Size = new System.Drawing.Size(100, 150);
            this.LBL51_REQ_COMMENT.TabIndex = 8;
            this.LBL51_REQ_COMMENT.Text = "tyLabel1";
            this.LBL51_REQ_COMMENT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_NOTE
            // 
            this.TXT01_NOTE.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TXT01_NOTE.FactoryID = "";
            this.TXT01_NOTE.FactoryName = null;
            this.TXT01_NOTE.Location = new System.Drawing.Point(116, 230);
            this.TXT01_NOTE.MinLength = 0;
            this.TXT01_NOTE.Multiline = true;
            this.TXT01_NOTE.Name = "TXT01_NOTE";
            this.TXT01_NOTE.Size = new System.Drawing.Size(895, 60);
            this.TXT01_NOTE.TabIndex = 13;
            // 
            // LBL51_NOTE
            // 
            this.LBL51_NOTE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LBL51_NOTE.BackColor = System.Drawing.SystemColors.Control;
            this.LBL51_NOTE.FactoryID = "";
            this.LBL51_NOTE.FactoryName = null;
            this.LBL51_NOTE.Location = new System.Drawing.Point(10, 230);
            this.LBL51_NOTE.Name = "LBL51_NOTE";
            this.LBL51_NOTE.Size = new System.Drawing.Size(100, 60);
            this.LBL51_NOTE.TabIndex = 12;
            this.LBL51_NOTE.Text = "tyLabel1";
            this.LBL51_NOTE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL51_ATTACH_FILENAME
            // 
            this.LBL51_ATTACH_FILENAME.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LBL51_ATTACH_FILENAME.BackColor = System.Drawing.SystemColors.Control;
            this.LBL51_ATTACH_FILENAME.FactoryID = "";
            this.LBL51_ATTACH_FILENAME.FactoryName = null;
            this.LBL51_ATTACH_FILENAME.Location = new System.Drawing.Point(10, 296);
            this.LBL51_ATTACH_FILENAME.Name = "LBL51_ATTACH_FILENAME";
            this.LBL51_ATTACH_FILENAME.Size = new System.Drawing.Size(100, 21);
            this.LBL51_ATTACH_FILENAME.TabIndex = 8;
            this.LBL51_ATTACH_FILENAME.Text = "tyLabel1";
            this.LBL51_ATTACH_FILENAME.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_ATTACH_FILENAME
            // 
            this.TXT01_ATTACH_FILENAME.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TXT01_ATTACH_FILENAME.FactoryID = "";
            this.TXT01_ATTACH_FILENAME.FactoryName = null;
            this.TXT01_ATTACH_FILENAME.Location = new System.Drawing.Point(116, 296);
            this.TXT01_ATTACH_FILENAME.MinLength = 0;
            this.TXT01_ATTACH_FILENAME.Name = "TXT01_ATTACH_FILENAME";
            this.TXT01_ATTACH_FILENAME.Size = new System.Drawing.Size(814, 21);
            this.TXT01_ATTACH_FILENAME.TabIndex = 11;
            // 
            // BTN61_DWN
            // 
            this.BTN61_DWN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN61_DWN.FactoryID = "";
            this.BTN61_DWN.FactoryName = null;
            this.BTN61_DWN.Location = new System.Drawing.Point(936, 294);
            this.BTN61_DWN.Name = "BTN61_DWN";
            this.BTN61_DWN.Size = new System.Drawing.Size(75, 23);
            this.BTN61_DWN.TabIndex = 17;
            this.BTN61_DWN.Text = "tyButton1";
            this.BTN61_DWN.UseVisualStyleBackColor = true;
            this.BTN61_DWN.Click += new System.EventHandler(this.BTN61_DWN_Click);
            // 
            // LBL51_DEV_NAME
            // 
            this.LBL51_DEV_NAME.BackColor = System.Drawing.SystemColors.Control;
            this.LBL51_DEV_NAME.FactoryID = "";
            this.LBL51_DEV_NAME.FactoryName = null;
            this.LBL51_DEV_NAME.Location = new System.Drawing.Point(10, 20);
            this.LBL51_DEV_NAME.Name = "LBL51_DEV_NAME";
            this.LBL51_DEV_NAME.Size = new System.Drawing.Size(100, 21);
            this.LBL51_DEV_NAME.TabIndex = 5;
            this.LBL51_DEV_NAME.Text = "tyLabel1";
            this.LBL51_DEV_NAME.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_DEV_NAME
            // 
            this.TXT01_DEV_NAME.FactoryID = "";
            this.TXT01_DEV_NAME.FactoryName = null;
            this.TXT01_DEV_NAME.Location = new System.Drawing.Point(116, 20);
            this.TXT01_DEV_NAME.MinLength = 0;
            this.TXT01_DEV_NAME.Name = "TXT01_DEV_NAME";
            this.TXT01_DEV_NAME.Size = new System.Drawing.Size(120, 21);
            this.TXT01_DEV_NAME.TabIndex = 6;
            // 
            // LBL51_WORK_START_DTM
            // 
            this.LBL51_WORK_START_DTM.BackColor = System.Drawing.SystemColors.Control;
            this.LBL51_WORK_START_DTM.FactoryID = "";
            this.LBL51_WORK_START_DTM.FactoryName = null;
            this.LBL51_WORK_START_DTM.Location = new System.Drawing.Point(242, 20);
            this.LBL51_WORK_START_DTM.Name = "LBL51_WORK_START_DTM";
            this.LBL51_WORK_START_DTM.Size = new System.Drawing.Size(100, 21);
            this.LBL51_WORK_START_DTM.TabIndex = 5;
            this.LBL51_WORK_START_DTM.Text = "tyLabel1";
            this.LBL51_WORK_START_DTM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_WORK_START_DTM
            // 
            this.TXT01_WORK_START_DTM.FactoryID = "";
            this.TXT01_WORK_START_DTM.FactoryName = null;
            this.TXT01_WORK_START_DTM.Location = new System.Drawing.Point(348, 20);
            this.TXT01_WORK_START_DTM.MinLength = 0;
            this.TXT01_WORK_START_DTM.Name = "TXT01_WORK_START_DTM";
            this.TXT01_WORK_START_DTM.Size = new System.Drawing.Size(120, 21);
            this.TXT01_WORK_START_DTM.TabIndex = 6;
            // 
            // LBL51_WORK_END_DTM
            // 
            this.LBL51_WORK_END_DTM.BackColor = System.Drawing.SystemColors.Control;
            this.LBL51_WORK_END_DTM.FactoryID = "";
            this.LBL51_WORK_END_DTM.FactoryName = null;
            this.LBL51_WORK_END_DTM.Location = new System.Drawing.Point(474, 20);
            this.LBL51_WORK_END_DTM.Name = "LBL51_WORK_END_DTM";
            this.LBL51_WORK_END_DTM.Size = new System.Drawing.Size(100, 21);
            this.LBL51_WORK_END_DTM.TabIndex = 5;
            this.LBL51_WORK_END_DTM.Text = "tyLabel1";
            this.LBL51_WORK_END_DTM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_WORK_END_DTM
            // 
            this.TXT01_WORK_END_DTM.FactoryID = "";
            this.TXT01_WORK_END_DTM.FactoryName = null;
            this.TXT01_WORK_END_DTM.Location = new System.Drawing.Point(580, 20);
            this.TXT01_WORK_END_DTM.MinLength = 0;
            this.TXT01_WORK_END_DTM.Name = "TXT01_WORK_END_DTM";
            this.TXT01_WORK_END_DTM.Size = new System.Drawing.Size(120, 21);
            this.TXT01_WORK_END_DTM.TabIndex = 6;
            // 
            // LBL51_PROGRAM_NO
            // 
            this.LBL51_PROGRAM_NO.BackColor = System.Drawing.SystemColors.Control;
            this.LBL51_PROGRAM_NO.FactoryID = "";
            this.LBL51_PROGRAM_NO.FactoryName = null;
            this.LBL51_PROGRAM_NO.Location = new System.Drawing.Point(10, 47);
            this.LBL51_PROGRAM_NO.Name = "LBL51_PROGRAM_NO";
            this.LBL51_PROGRAM_NO.Size = new System.Drawing.Size(100, 21);
            this.LBL51_PROGRAM_NO.TabIndex = 5;
            this.LBL51_PROGRAM_NO.Text = "tyLabel1";
            this.LBL51_PROGRAM_NO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_PROGRAM_NO
            // 
            this.TXT01_PROGRAM_NO.FactoryID = "";
            this.TXT01_PROGRAM_NO.FactoryName = null;
            this.TXT01_PROGRAM_NO.Location = new System.Drawing.Point(116, 47);
            this.TXT01_PROGRAM_NO.MinLength = 0;
            this.TXT01_PROGRAM_NO.Name = "TXT01_PROGRAM_NO";
            this.TXT01_PROGRAM_NO.Size = new System.Drawing.Size(120, 21);
            this.TXT01_PROGRAM_NO.TabIndex = 6;
            // 
            // LBL51_DISPOSAL_YN
            // 
            this.LBL51_DISPOSAL_YN.BackColor = System.Drawing.SystemColors.Control;
            this.LBL51_DISPOSAL_YN.FactoryID = "";
            this.LBL51_DISPOSAL_YN.FactoryName = null;
            this.LBL51_DISPOSAL_YN.Location = new System.Drawing.Point(706, 20);
            this.LBL51_DISPOSAL_YN.Name = "LBL51_DISPOSAL_YN";
            this.LBL51_DISPOSAL_YN.Size = new System.Drawing.Size(100, 21);
            this.LBL51_DISPOSAL_YN.TabIndex = 5;
            this.LBL51_DISPOSAL_YN.Text = "tyLabel1";
            this.LBL51_DISPOSAL_YN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CKB01_DISPOSAL_YN
            // 
            this.CKB01_DISPOSAL_YN.AutoSize = true;
            this.CKB01_DISPOSAL_YN.FactoryID = "";
            this.CKB01_DISPOSAL_YN.FactoryName = null;
            this.CKB01_DISPOSAL_YN.Location = new System.Drawing.Point(812, 23);
            this.CKB01_DISPOSAL_YN.Name = "CKB01_DISPOSAL_YN";
            this.CKB01_DISPOSAL_YN.Size = new System.Drawing.Size(15, 14);
            this.CKB01_DISPOSAL_YN.TabIndex = 7;
            this.CKB01_DISPOSAL_YN.UseVisualStyleBackColor = true;
            // 
            // LBL51_WORK_COMMENT
            // 
            this.LBL51_WORK_COMMENT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.LBL51_WORK_COMMENT.BackColor = System.Drawing.SystemColors.Control;
            this.LBL51_WORK_COMMENT.FactoryID = "";
            this.LBL51_WORK_COMMENT.FactoryName = null;
            this.LBL51_WORK_COMMENT.Location = new System.Drawing.Point(10, 47);
            this.LBL51_WORK_COMMENT.Name = "LBL51_WORK_COMMENT";
            this.LBL51_WORK_COMMENT.Size = new System.Drawing.Size(100, 210);
            this.LBL51_WORK_COMMENT.TabIndex = 8;
            this.LBL51_WORK_COMMENT.Text = "tyLabel1";
            this.LBL51_WORK_COMMENT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT01_WORK_COMMENT
            // 
            this.TXT01_WORK_COMMENT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TXT01_WORK_COMMENT.FactoryID = "";
            this.TXT01_WORK_COMMENT.FactoryName = null;
            this.TXT01_WORK_COMMENT.Location = new System.Drawing.Point(116, 47);
            this.TXT01_WORK_COMMENT.MinLength = 0;
            this.TXT01_WORK_COMMENT.Multiline = true;
            this.TXT01_WORK_COMMENT.Name = "TXT01_WORK_COMMENT";
            this.TXT01_WORK_COMMENT.Size = new System.Drawing.Size(895, 210);
            this.TXT01_WORK_COMMENT.TabIndex = 11;
            // 
            // TYERGB010P
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 601);
            this.Controls.Add(this.GBX01_WORK);
            this.Controls.Add(this.GBX01_CLAIM);
            this.Name = "TYERGB010P";
            this.Text = "TYERGB009P";
            this.Load += new System.EventHandler(this.TYERGB010P_Load);
            this.GBX01_CLAIM.ResumeLayout(false);
            this.GBX01_CLAIM.PerformLayout();
            this.GBX01_WORK.ResumeLayout(false);
            this.GBX01_WORK.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GBX01_CLAIM;
        private System.Windows.Forms.GroupBox GBX01_WORK;
        private Service.Library.Controls.TYTextBox TXT01_CLAIM_NO;
        private Service.Library.Controls.TYLabel LBL51_CLAIM_NO;
        private Service.Library.Controls.TYButton BTN61_CLO;
        private Service.Library.Controls.TYTextBox TXT01_PROGRAM_NAME;
        private Service.Library.Controls.TYTextBox TXT01_REG_DTM;
        private Service.Library.Controls.TYLabel LBL51_PROGRAM_NAME;
        private Service.Library.Controls.TYTextBox TXT01_NOTE;
        private Service.Library.Controls.TYLabel LBL51_NOTE;
        private Service.Library.Controls.TYTextBox TXT01_REQ_COMMENT;
        private Service.Library.Controls.TYLabel LBL51_REG_DTM;
        private Service.Library.Controls.TYLabel LBL51_REQ_COMMENT;
        private Service.Library.Controls.TYTextBox TXT01_ATTACH_FILENAME;
        private Service.Library.Controls.TYLabel LBL51_ATTACH_FILENAME;
        private Service.Library.Controls.TYButton BTN61_DWN;
        private Service.Library.Controls.TYTextBox TXT01_PROGRAM_NO;
        private Service.Library.Controls.TYLabel LBL51_PROGRAM_NO;
        private Service.Library.Controls.TYTextBox TXT01_WORK_END_DTM;
        private Service.Library.Controls.TYLabel LBL51_WORK_END_DTM;
        private Service.Library.Controls.TYTextBox TXT01_WORK_START_DTM;
        private Service.Library.Controls.TYLabel LBL51_WORK_START_DTM;
        private Service.Library.Controls.TYTextBox TXT01_DEV_NAME;
        private Service.Library.Controls.TYLabel LBL51_DEV_NAME;
        private Service.Library.Controls.TYCheckBox CKB01_DISPOSAL_YN;
        private Service.Library.Controls.TYLabel LBL51_DISPOSAL_YN;
        private Service.Library.Controls.TYTextBox TXT01_WORK_COMMENT;
        private Service.Library.Controls.TYLabel LBL51_WORK_COMMENT;
        private System.Windows.Forms.SaveFileDialog sfd01_FILE;
    }
}