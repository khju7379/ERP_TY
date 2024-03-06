using System;
using System.Net;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using System.Security.Cryptography;

using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

using TY.Service.Library;
using TY.Service.Library.Controls;

using Shoveling2010.SmartClient.SystemUtility;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Component;
using Shoveling2010.SmartClient.SystemUtility.Controls.SystemForm;

namespace TY.Service.Library
{
    public class TYBase : FormBase
    {
        /********************************************************************
         * 0. private 변수
         ********************************************************************/
        private const string xlsConnectionString =
            "Provider=Microsoft.Jet.OLEDB.4.0;" +
            "Data Source=\"{0}\";" +
            "Mode=ReadWrite|Share Deny None;" +
            "Extended Properties='Excel 8.0; HDR={1}; IMEX={2}';" +
            "Persist Security Info=False";
        private const string xlsxConnectionString =
            "Provider=Microsoft.ACE.OLEDB.12.0;" +
            "Data Source=\"{0}\";" +
            "Mode=ReadWrite|Share Deny None;" +
            "Extended Properties='Excel 12.0; HDR={1}; IMEX={2}';" +
            "Persist Security Info=False";

        private static TYSession _session;
        private Dictionary<string, List<string>> _KeyColumns = new Dictionary<string, List<string>>();
        private Panel PNL99_HIDDENCODEBOXES = null;
        private Dictionary<TYCodeBox, SpreadCodeHelperInfos> _spreadCodeHelperInfos = null;
        private Control _startingControl = null;
        private bool _buttonTabIndexLast = true;


        /********************************************************************
         * 연말정산 사용 dll
         ********************************************************************/

        [DllImport("ExportCustomFile.dll")]
        public static extern int NTS_GetFileSize([MarshalAs(UnmanagedType.LPStr)]string szIn, [MarshalAs(UnmanagedType.LPStr)]string szPassword, [MarshalAs(UnmanagedType.LPStr)]string szName, int bAnsi);

        [DllImport("ExportCustomFile.dll")]
        public static extern int NTS_GetFileBuf([MarshalAs(UnmanagedType.LPStr)]string szIn, [MarshalAs(UnmanagedType.LPStr)]string szPassword, [MarshalAs(UnmanagedType.LPStr)]string szName, [In, Out]byte[] pcBuffer, int bAnsi);

        [DllImport("DSTSPDFSig_C.dll")]        
        public static extern int DSTSPdfSigVerifyF([MarshalAs(UnmanagedType.LPStr)]string pram1, byte[] pram2, byte[] pram3, byte[] pram4, byte[] pram5);

        /********************************************************************
         * 세관EDI 전자문서 지원 KCSAPI4 Dll
         ********************************************************************/
        //관세청 서버의 URL정보 조회
        [DllImport(@"C:\\KCSAPI4\\KCSAPI4.dll")]
        public static extern string GetSrvrInfo(string USERID, string FromCbtID);

        //통관관련 문서 송신
        [DllImport(@"C:\\KCSAPI4\\KCSAPI4.dll")]
        public static extern string TrsmDocCscl(string USERID, string FromCbtID, string DocCode, string ConversationID, string Payload);

        //요건신청문서 송신
        [DllImport(@"C:\\KCSAPI4\\KCSAPI4.dll")]
        public static extern string TrsmDocReqApre(string USERID, string FromCbtID, string ToCbtID, string DocCode, string ConversationID, string Payload);

        //통관관련 목록 수신
        [DllImport(@"C:\\KCSAPI4\\KCSAPI4.dll")]
        public static extern string RcpnDocLstCscl(string USERID, string FromCbtID);

        //문서번호에 해당하는 XML파일 수신
        [DllImport(@"C:\\KCSAPI4\\KCSAPI4.dll")]
        public static extern string RcpnDocCscl(string USERID, string FromCbtID, string DocCode, string ConversationID);

        //요건확인 목록 수신
        [DllImport(@"C:\\KCSAPI4\\KCSAPI4.dll")]
        public static extern string RcpnDocLstReqApre(string USERID, string FromCbtID);

        //문서번호에 해당하는 XML파일 수신
        [DllImport(@"C:\\KCSAPI4\\KCSAPI4.dll")]
        public static extern string RcpnDocReqApre(string USERID, string FromCbtID, string DocCode, string ConversationID);

        //다중문서 송신(TrsmMltDoc)으로 다량의 통보서가 발생한 경우에만 사용
        [DllImport(@"C:\\KCSAPI4\\KCSAPI4.dll")]
        public static extern string RcpnMltDoc(string USERID, string FromCbtID);
        
        //인증서 설정
        [DllImport(@"C:\\KCSAPI4\\KCSAPI4.dll")]
        public static extern string LoginSecuMdle(string USERID, string FromCbtID);

        //인증서 해지
        [DllImport(@"C:\\KCSAPI4\\KCSAPI4.dll")]
        public static extern string LogoutSecuMdle();

        public static string ConstKCSAPIPath = "C:\\KCSAPI4";

        /********************************************************************
         * 1. 구조체 및 enum 정의
         ********************************************************************/
        #region struct SpreadCodeHelperInfos
        private struct SpreadCodeHelperInfos
        {
            public SpreadCodeHelperInfos(TYSpread spread, string applyCodeCol, string applyNameCol)
            {
                this.Spread = spread;
                this.ApplyCodeCol = applyCodeCol;
                this.ApplyNameCol = applyNameCol;
            }

            public TYSpread Spread;
            public string ApplyCodeCol;
            public string ApplyNameCol;
        }
        #endregion

        #region enum SumRowType
        /// <summary>
        /// 합계 행 종류
        /// </summary>
        protected enum SumRowType
        {
            /// <summary>
            /// 소계
            /// </summary>
            SubTotal = 0,
            /// <summary>
            /// 누계
            /// </summary>
            Sum = 1,
            /// <summary>
            /// 합계
            /// </summary>
            Total = 2
        }
        #endregion

        #region enum ExcelImportHDRType
        /// <summary>
        /// OleDB Excel Import 시 Excel의 첫번째 줄의 데이터를 Field 명으로 인식 할 것인지 여부
        /// </summary>
        public enum ExcelImportHDRType
        {
            /// <summary>
            /// Excel의 첫번째 줄의 데이터를 Field 명으로 인식
            /// </summary>
            Yes = 0,
            /// <summary>
            /// Excel의 첫번째 줄의 데이터를 Field 명으로 인식하지 않음
            /// </summary>
            No = 1
        }
        #endregion

        #region enum ExcelImportIMEXType
        /// <summary>
        /// OleDB Excel Import 시 OleDB IMEX 값
        /// </summary>
        public enum ExcelImportIMEXType
        {
            /// <summary>
            /// Export mode
            /// </summary>
            IMEX0 = 0,
            /// <summary>
            /// Import mode
            /// </summary>
            IMEX1 = 1,
            /// <summary>
            /// Linked mode (full update capabilities)
            /// </summary>
            IMEX2 = 2
        }
        #endregion

        #region enum ExcelImportType
        /// <summary>
        /// Excel Import 시 사용할 Type
        /// </summary>
        public enum ExcelImportType
        {
            /// <summary>
            /// Automation(Microsoft.Office.Interop.Excel.dll, v2.0.50727)
            /// </summary>
            Automation = 0,
            /// <summary>
            /// OleDB
            /// </summary>
            OleDB = 1,
            /// <summary>
            /// Automation(Microsoft.Office.Interop.Excel.dll, v2.0.50727) 오류 시 OleDB 사용, OleDB 오류 시 오류 발생
            /// </summary>
            Dynamic = 2
        } 
        #endregion

        /********************************************************************
         * 2. 생성자
         ********************************************************************/
        public TYBase()
            : base()
        {
            _session = _session ?? new TYSession();
            this.Load += new EventHandler(TYBase_Load);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TYBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.Name = "TYBase";
            this.ResumeLayout(false);

        }

        /********************************************************************
         * 3. 이벤트 처리
         ********************************************************************/
        private void TYBase_Load(object sender, EventArgs e)
        {
            foreach (Control control in this.ControlFactory)
            {
                if (!(control is TYSpread))
                    continue;

                ((TYSpread)control).SetTYColumns();
            }

            this.BackColor = Color.FromArgb(252, 252, 252);
        }

        /********************************************************************
         * 4. 오버라이딩
         ********************************************************************/
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (this.DesignMode)
                return;

            this.IControlFactoryCollectionTabIndexSorting();

            if (this._buttonTabIndexLast)
            {
                List<Control> buttons = this.ControlSequenceList.FindAll(new Predicate<Control>(IsControlButton));
                this.ControlSequenceList.RemoveAll(new Predicate<Control>(IsControlButton));
                this.ControlSequenceList.InsertRange(this.ControlSequenceList.Count, buttons);
            }

            if (this._startingControl != null && this._startingControl.CanFocus)
            {
                if (this.ControlSequenceList.Contains(this._startingControl))
                    this.SetControlFocus(this._startingControl, 0);
                else
                    this._startingControl.Focus();
            }
        }

        private bool IsControlButton(Control control)
        {
            return control is Button;
        }

        private void IControlFactoryCollectionTabIndexSorting()
        {
            ArrayList newFactorySortingList = new ArrayList();
            for (int i = 0; i < this.ControlFactory.Count; i++)
            {
                Control control = (Control)this.ControlFactory[i];
                newFactorySortingList.Add(String.Format("{0}#{1}",
                    this.GetControlAllSumPoint(control), control.Name));
            }

            newFactorySortingList.Sort();
            int beforeSeqIndex = -1;
            for (int i = 0; i < newFactorySortingList.Count; i++)
            {
                string[] item = newFactorySortingList[i].ToString().Split('#');
                string seqText = item[1];
                Control control = this.ControlFactory.GetControl(item[2]);

                if (control != null)
                {
                    IControlFactory factory = (IControlFactory)control;
                    if (!seqText.Equals("9999-9999"))
                    {
                        if (control is TYMaskedTextBox) //TYRicherTextBox 엔터키 입력 문제로 제외
                        {
                            control.KeyDown += new KeyEventHandler(control_KeyDown);
                            control.Enter += new EventHandler(control_Enter);
                            this.ControlSequenceList.Add(control);
                            this.ControlSequenceList.Remove(control);
                            this.ControlSequenceList.Insert(beforeSeqIndex + 1, control);
                        }
                        else if (control is TYTextButtonBox)
                        {
                            TYTextButtonBox tmpBox = control as TYTextButtonBox;
                            if (tmpBox.TextBoxVisible)
                            {
                                tmpBox.TextBox.KeyDown += new KeyEventHandler(control_KeyDown);
                                tmpBox.TextBox.Enter += new EventHandler(control_Enter);
                                this.ControlSequenceList.Add(tmpBox.TextBox);
                                this.ControlSequenceList.Remove(tmpBox.TextBox);
                                this.ControlSequenceList.Insert(beforeSeqIndex + 2, tmpBox.TextBox);
                            }
                        }

                        beforeSeqIndex = this.ControlSequenceList.IndexOf(control);
                    }
                }
            }
        }

        private string GetControlAllSumPoint(Control control)
        {
            Point point = new Point(9999, 9999);
            string group = "9999-9999#9999-9999";
            if (control is TButton ||
                control is TCheckBox ||
                control is TComboBox ||
                control is TCodeBox ||
                control is TDatePicker ||
                control is TTextBox ||
                control is TTrackBar ||
                control is TRichTextBox ||
                control is TYMaskedTextBox) //TYRicherTextBox 엔터키 입력 문제로 제외
            {
                point = control.Location;

                if (control is TButton)
                    point.Y = point.Y + 5000;

                group = String.Format("#{0:0000}-{1:0000}",
                    Math.Ceiling(point.Y * 0.1),
                    Math.Ceiling(point.X * 0.1));

                while (control.Parent != null)
                {
                    control = (Control)control.Parent;
                    point = control.Location;
                    group = String.Format("{0:0000}-{1:0000}-{2}",
                                Math.Ceiling(point.Y * 0.1),
                                Math.Ceiling(point.X * 0.1), group);
                }
            }

            return group;
        }

        private void control_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) ||
                (e.KeyCode == Keys.Enter && (e.Control | e.Shift)))
                this.ControlSequenceList.SetControlFocus((Control)sender, (e.Shift) ? -1 : 1);
        }

        private void control_Enter(object sender, EventArgs e)
        {
            this.ControlSequenceList.SetControlFocus((Control)sender, 0);
        }

        /********************************************************************
         * 5. internal 함수
         ********************************************************************/
        internal void SetControlFocus(Control control, int move)
        {
            if (this.ControlSequenceList.Contains(control))
                this.ControlSequenceList.SetControlFocus(control, move);
        }

        /********************************************************************
         * 6. 멤버 변수
         ********************************************************************/
        #region Session - 웹의 세션처럼 프로그램 구동 동안 전역으로 사용할 수 있는 클래스
        /// <summary>
        /// 웹의 세션처럼 프로그램 구동 동안 전역으로 사용할 수 있는 클래스
        /// </summary>
        public TYSession Session
        {
            get
            {
                _session.CurrentForm = this;
                return _session;
            }
        }
        #endregion

        #region ButtonTabIndexLast - 버튼의 탭 순서를 맨 마지막으로 할 지 여부
        /// <summary>
        /// 버튼의 탭 순서를 맨 마지막으로 할 지 여부
        /// </summary>
        public bool ButtonTabIndexLast
        {
            get { return this._buttonTabIndexLast; }
            set { this._buttonTabIndexLast = value; }
        } 
        #endregion

        /********************************************************************
         * z. 사용자 정의 함수
         ********************************************************************/
        //-----TYBase 생성자에서 실행해야하는 함수-----
        #region SetSpreadCodeHelper - 해당 스프레드에 코드헬퍼 컬럼 기능 설정, TYBase 생성자에 위치
        /// <summary>
        /// 해당 스프레드에 코드헬퍼 컬럼 기능 설정, TYBase 생성자에 위치
        /// </summary>
        /// <param name="spread">스프레드</param>
        /// <param name="applyCodeCol">코드 컬럼</param>
        /// <param name="applyFieldNo">코드헬퍼 적용 필드 번호</param>
        protected void SetSpreadCodeHelper(TYSpread spread, string applyCodeCol, string applyFieldNo)
        {
            this.SetSpreadCodeHelper(spread, applyCodeCol, string.Empty, applyFieldNo);
        }

        /// <summary>
        /// 해당 스프레드에 코드헬퍼 컬럼 기능 설정, TYBase 생성자에 위치
        /// </summary>
        /// <param name="spread">스프레드</param>
        /// <param name="applyCodeCol">코드 컬럼</param>
        /// <param name="applyNameCol">코드명 컬럼</param>
        /// <param name="applyFieldNo">코드헬퍼 적용 필드 번호</param>
        protected void SetSpreadCodeHelper(TYSpread spread, string applyCodeCol, string applyNameCol, string applyFieldNo)
        {
            if (this.PNL99_HIDDENCODEBOXES == null)
            {
                this.PNL99_HIDDENCODEBOXES = new Panel();
                this.PNL99_HIDDENCODEBOXES.Visible = false;
                this.Controls.Add(this.PNL99_HIDDENCODEBOXES);
            }
            this._spreadCodeHelperInfos = this._spreadCodeHelperInfos ?? new Dictionary<TYCodeBox, SpreadCodeHelperInfos>();

            TYCodeBox tyCodeBox = new TYCodeBox();
            tyCodeBox.IsSpreadCodeHelper = true;
            for (int i = 160; i < 256; i++)
            {
                tyCodeBox.Name = string.Format("CBH{0:X}_{1}", i, applyFieldNo);
                if (this.PNL99_HIDDENCODEBOXES.Controls.Find(tyCodeBox.Name, true).Length > 0)
                    continue;

                tyCodeBox.CodeBoxDataBinded += new TCodeBox.TCodeBoxEventHandler(tyCodeBox_CodeBoxDataBinded);
                spread.CodeBoxColumnInfos.Add(applyCodeCol, new TYSpread.ColCodeBoxInfo(applyCodeCol, applyNameCol, tyCodeBox));                
                this._spreadCodeHelperInfos.Add(tyCodeBox, new SpreadCodeHelperInfos(spread, applyCodeCol, applyNameCol));
                break;
            }

            this.PNL99_HIDDENCODEBOXES.Controls.Add(tyCodeBox);
        }

        /// <summary>
        /// SetCodeBoxCellType 동작 관련 이벤트 private 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tyCodeBox_CodeBoxDataBinded(object sender, EventArgs e)
        {
            TYCodeBox tyCodeBox;
            TYSpread tySpread;
            tyCodeBox = (TYCodeBox)sender;

            if (tyCodeBox == null)
                return;

            if (!this._spreadCodeHelperInfos.ContainsKey(tyCodeBox))
                return;

            tySpread = this._spreadCodeHelperInfos[tyCodeBox].Spread;

            if (!string.IsNullOrEmpty(this._spreadCodeHelperInfos[tyCodeBox].ApplyCodeCol))
                tySpread.SetValue(tySpread.ActiveRowIndex, this._spreadCodeHelperInfos[tyCodeBox].ApplyCodeCol, tyCodeBox.GetValue());
            if (!string.IsNullOrEmpty(this._spreadCodeHelperInfos[tyCodeBox].ApplyNameCol))
                tySpread.SetValue(tySpread.ActiveRowIndex, this._spreadCodeHelperInfos[tyCodeBox].ApplyNameCol, tyCodeBox.GetText());

            if (!tySpread.ActiveSheet.RowHeader.Cells[tySpread.ActiveRowIndex, 0].Text.Equals(TSpread.FLAG_N) &&
                !tySpread.ActiveSheet.RowHeader.Cells[tySpread.ActiveRowIndex, 0].Text.Equals(TSpread.FLAG_D))
            {
                tySpread.ActiveSheet.Rows[tySpread.ActiveRowIndex].BackColor = tySpread.UpdateRowColor;
                tySpread.ActiveSheet.RowHeader.Cells[tySpread.ActiveRowIndex, 0].Text = TSpread.FLAG_U;
            }
        }
        #endregion

        //-----TYBase Load에서 실행해야하는 함수-----
        #region SetSpreadKeyColumn - 스프레드에 키 컬럼 설정(키 컬럼은 신규행인 경우만 수정 가능), TYBase Load에 위치

        /// <summary>
        /// 스프레드에 키 컬럼 설정(키 컬럼은 신규행인 경우만 수정 가능), TYBase Load에 위치
        /// </summary>
        /// <param name="spread">스프레드</param>
        /// <param name="keyColumnNames">키 컬럼 명</param>
        protected void SetSpreadKeyColumn(TYSpread spread, params string[] keyColumnNames)
        {
            foreach (string keyColumnName in keyColumnNames)
                this.SetSpreadKeyColumn(spread, keyColumnName);
        }

        private void SetSpreadKeyColumn(TYSpread spread, string keyColumnName)
        {
            spread.ActiveSheet.Columns[keyColumnName].Locked = true;
            if (!this._KeyColumns.ContainsKey(spread.FactoryID))
            {
                spread.RowInserted += new TSpread.TRowEventHandler(spread_RowInserted);
                this._KeyColumns.Add(spread.FactoryID, new List<string>());
            }
            if (!this._KeyColumns[spread.FactoryID].Contains(keyColumnName))
                this._KeyColumns[spread.FactoryID].Add(keyColumnName);
        }

        private void spread_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            TYSpread spread = (TYSpread)sender;

            if (spread.ActiveSheet.Rows.Count > e.RowIndex) //스프레드의 RowInserted 이벤트에서 Validation의 이유로 추가된 행이 사라진 경우 오류를 막기 위함
                foreach (string keyColumnName in this._KeyColumns[spread.FactoryID])
                    spread.ActiveSheet.Cells[e.RowIndex, spread.ActiveSheet.Columns[keyColumnName].Index].Locked = false;
        }

        #endregion

        #region SetStartingFocus - 화면 로딩 후 최초 포커스할 컨트롤을 설정, TYBase Load에 위치해야 함
        /// <summary>
        /// 화면 로딩 후 최초 포커스할 컨트롤을 설정, TYBase Load에 위치해야 함
        /// </summary>
        /// <param name="control">컨트롤</param>
        protected void SetStartingFocus(Control control)
        {
            this._startingControl = control;
        }
        #endregion

        #region SetSpreadFixedWidthColumn - 해당 스프레드를 비율로 Resizing 시 컬럼 폭의 고정 여부 설정, TYBase Load에 위치
        /// <summary>
        /// 해당 스프레드를 비율로 Resizing 시 컬럼 폭의 고정 여부 설정, TYBase Load에 위치
        /// </summary>
        /// <param name="spread">스프레드</param>
        /// <param name="columnName">컬럼 명</param>
        /// <param name="isFixedWidth">폭 고정 여부</param>
        protected void SetSpreadFixedWidthColumn(TYSpread spread, string columnName, bool isFixedWidth)
        {
            if (spread == null)
                return;

            spread.SetFixedWidthColumnInAutoResizeRate(columnName, isFixedWidth);
        }

        /// <summary>
        /// 해당 스프레드를 비율로 Resizing 시 컬럼 폭의 고정 여부 설정, TYBase Load에 위치
        /// </summary>
        /// <param name="spread">스프레드</param>
        /// <param name="columnNames">컬럼 명</param>
        protected void SetSpreadFixedWidthColumn(TYSpread spread, params string[] columnNames)
        {
            foreach (string columnName in columnNames)
                this.SetSpreadFixedWidthColumn(spread, columnName, true);
        }
        #endregion

        //-----기타 특정위치 함수-----
        #region SetSpreadSumRow - 스프레드에 합계 행 설정. 해당 컬럼의 값이 합계 명인 경우, 스프레드 바인딩 시 마다 위치
        /// <summary>
        /// 스프레드에 합계 행 설정. 해당 컬럼의 값이 합계 명인 경우, 스프레드 바인딩 시 마다 위치
        /// </summary>
        /// <param name="spread">스프레드</param>
        /// <param name="sumStringColumnName">함계 문자 표시 컬럼 명</param>
        /// <param name="sumString">함계 문자</param>
        /// <param name="sumRowType">합계 행 종류</param>
        protected void SetSpreadSumRow(TYSpread spread, string sumStringColumnName, string sumString, SumRowType sumRowType)
        {
            switch (sumRowType)
            {
                case SumRowType.SubTotal:
                    this.SetSpreadSumRow(spread, sumStringColumnName, sumString, Color.FromArgb(228, 242, 194));    //E4F2C2
                    break;
                case SumRowType.Sum:
                    this.SetSpreadSumRow(spread, sumStringColumnName, sumString, Color.FromArgb(218, 239, 244));    //DAEFF4
                    break;
                default:
                    this.SetSpreadSumRow(spread, sumStringColumnName, sumString, Color.FromArgb(254, 209, 164));    //FED1A4
                    break;
            }
        }

        /// <summary>
        /// 스프레드에 합계 행 설정. 해당 컬럼의 값이 합계 명인 경우, 스프레드 바인딩 시 마다 위치
        /// </summary>
        /// <param name="spread">스프레드</param>
        /// <param name="sumStringColumnName">함계 문자 표시 컬럼 명</param>
        /// <param name="sumString">함계 문자</param>
        /// <param name="sumRowColor">합계 행 색</param>
        protected void SetSpreadSumRow(TYSpread spread, string sumStringColumnName, string sumString, System.Drawing.Color sumRowColor)
        {
            for (int i = 0; i < spread.ActiveSheet.Rows.Count; i++)
                if (Convert.ToString(spread.ActiveSheet.Cells[i, spread.ActiveSheet.Columns[sumStringColumnName].Index].Value) == sumString)
                    spread.ActiveSheet.Rows[i].BackColor = sumRowColor;
        }
        #endregion

        #region SpreadSumRowAdd - 스프레드에 합계 행 추가, 스프레드 바인딩 시 마다 위치
        /// <summary>
        /// 스프레드에 합계 행 추가, 스프레드 바인딩 시 마다 위치
        /// </summary>
        /// <param name="spread">스프레드</param>
        /// <param name="sumStringColumnName">합계 문자 표시 컬럼 명</param>
        /// <param name="sumString">합계 문자</param>
        /// <param name="sumRowType">합계 행 종류</param>
        protected void SpreadSumRowAdd(TYSpread spread, string sumStringColumnName, string sumString, SumRowType sumRowType)
        {
            switch (sumRowType)
            {
                case SumRowType.SubTotal:
                    this.SpreadSumRowAdd(spread, sumStringColumnName, sumString, Color.FromArgb(228, 242, 194));    //E4F2C2
                    break;
                case SumRowType.Sum:
                    this.SpreadSumRowAdd(spread, sumStringColumnName, sumString, Color.FromArgb(218, 239, 244));    //DAEFF4
                    break;
                default:
                    this.SpreadSumRowAdd(spread, sumStringColumnName, sumString, Color.FromArgb(254, 209, 164));    //FED1A4
                    break;
            }
        }

        /// <summary>
        /// 스프레드에 합계 행 추가, 스프레드 바인딩 시 마다 위치
        /// </summary>
        /// <param name="spread">스프레드</param>
        /// <param name="sumStringColumnName">합계 문자 표시 컬럼 명</param>
        /// <param name="sumString">합계 문자</param>
        /// <param name="sumRowColor">합계 행 색</param>
        protected void SpreadSumRowAdd(TYSpread spread, string sumStringColumnName, string sumString, System.Drawing.Color sumRowColor)
        {
            List<string> columnDataFields = new List<string>();
            for (int i = 0; i < spread.Sheets[0].Columns.Count; i++)
            {
                columnDataFields.Add(spread.Sheets[0].Columns[i].DataField);
            }
            this.SpreadSumRowAdd(spread, sumStringColumnName, sumString, sumRowColor, columnDataFields.ToArray());
        }

        /// <summary>
        /// 스프레드에 합계 행 추가, 스프레드 바인딩 시 마다 위치
        /// </summary>
        /// <param name="spread">스프레드</param>
        /// <param name="sumStringColumnName">합계 문자 표시 컬럼 명</param>
        /// <param name="sumString">합계 문자</param>
        /// <param name="sumRowType">합계 행 종류</param>
        /// <param name="sumColumns">합계를 낼 컬럼 명</param>
        protected void SpreadSumRowAdd(TYSpread spread, string sumStringColumnName, string sumString, SumRowType sumRowType, params string[] sumColumns)
        {
            switch (sumRowType)
            {
                case SumRowType.SubTotal:
                    this.SpreadSumRowAdd(spread, sumStringColumnName, sumString, Color.FromArgb(228, 242, 194), sumColumns);    //E4F2C2
                    break;
                case SumRowType.Sum:
                    this.SpreadSumRowAdd(spread, sumStringColumnName, sumString, Color.FromArgb(218, 239, 244), sumColumns);    //DAEFF4
                    break;
                default:
                    this.SpreadSumRowAdd(spread, sumStringColumnName, sumString, Color.FromArgb(254, 209, 164), sumColumns);    //FED1A4
                    break;
            }
        }

        /// <summary>
        /// 스프레드에 합계 행 추가, 스프레드 바인딩 시 마다 위치
        /// </summary>
        /// <param name="spread">스프레드</param>
        /// <param name="sumStringColumnName">합계 문자 표시 컬럼 명</param>
        /// <param name="sumString">합계 문자</param>
        /// <param name="sumRowColor">합계 행 색</param>
        /// <param name="sumColumns">합계를 낼 컬럼 명</param>
        protected void SpreadSumRowAdd(TYSpread spread, string sumStringColumnName, string sumString, System.Drawing.Color sumRowColor, params string[] sumColumns)
        {
            List<string> listSumColumns = new List<string>(sumColumns);
            spread.Sheets[0].AddRows(spread.Sheets[0].Rows.Count, 1);
            spread.Sheets[0].ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            spread.Sheets[0].AutoCalculation = true;
            spread.Sheets[0].Rows[spread.Sheets[0].Rows.Count - 1].BackColor = sumRowColor;
            for (int i = 0; i < spread.Sheets[0].Columns.Count; i++)
            {
                if (spread.Sheets[0].Columns[i].DataField == sumStringColumnName)
                    spread.Sheets[0].SetValue(spread.Sheets[0].Rows.Count - 1, i, sumString);
                else if (listSumColumns.IndexOf(spread.Sheets[0].Columns[i].DataField) > -1)
                {
                    if (spread.Sheets[0].Rows.Count == 1)
                        spread.Sheets[0].Cells[0, i].Value = 0;
                    else
                        spread.Sheets[0].SetFormula(spread.Sheets[0].Rows.Count - 1, i, string.Format("SUM(R1C[0]:R{0}C[0])", spread.Sheets[0].Rows.Count - 1));
                }
            }
        }
        #endregion

        //-----위치 무관 함수-----
        #region OpenPopup - 해당 폼을 띄움
        /// <summary>
        /// 해당 폼을 띄움
        /// </summary>
        /// <param name="form">폼</param>
        public void OpenPopup(Form form)
        {
            if (form is TYBase)
                ((TYBase)form).OwnerMDI = this.OwnerMDI;
            form.Show();
        } 
        #endregion

        #region OpenModalPopup - 해당 폼을 모달 팝업으로 띄움
        /// <summary>
        /// 해당 폼을 모달 팝업으로 띄움
        /// </summary>
        /// <param name="form">폼</param>
        /// <returns>폼의 DialogResult</returns>
        public DialogResult OpenModalPopup(Form form)
        {
            DialogResult rtnValue = System.Windows.Forms.DialogResult.Cancel;
            Form tmpForm = this;
            if (form is TYBase)
                ((TYBase)form).OwnerMDI = this.OwnerMDI;
            if (this.OwnerMDI != null && this.OwnerMDI.ParentForm != null)
                tmpForm = this.OwnerMDI.ParentForm;
            tmpForm.Opacity = 0.8;
            rtnValue = form.ShowDialog();
            tmpForm.Opacity = 1.0;
            return rtnValue;
        } 
        #endregion

        #region SetPopupStyle - 모달 팝업 스타일로 설정
        /// <summary>
        /// 모달 팝업 스타일로 설정
        /// </summary>
        public void SetPopupStyle()
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        #endregion

        #region GetSpreadCodeHelper - 해당 스프레드 컬럼에 설정된 코드박스 반환
        /// <summary>
        /// 해당 스프레드 컬럼에 설정된 코드박스 반환
        /// </summary>
        /// <param name="spread">스프레드</param>
        /// <param name="applyCodeCol">코드 컬럼</param>
        /// <returns>코드박스</returns>
        protected TYCodeBox GetSpreadCodeHelper(TYSpread spread, string applyCodeCol)
        {
            TYSpread.ColCodeBoxInfo tmpInfos;
            return spread.CodeBoxColumnInfos.TryGetValue(applyCodeCol, out tmpInfos) ? (tmpInfos.CodeBox ?? null) : null;
        }
        #endregion

        #region SetFocus - 해당 컨트롤로 포커스 이동
        /// <summary>
        /// 해당 컨트롤로 포커스 이동
        /// </summary>
        /// <param name="control">컨트롤</param>
        protected void SetFocus(Control control)
        {
            if (control != null && control.CanFocus)
            {
                if (this.ControlSequenceList.Contains(control))
                    this.SetControlFocus(control, 0);
                else
                    control.Focus();
            }
        }
        #endregion

        #region ShowCustomMessage - 커스텀 메세지박스를 표시
        /// <summary>
        /// 커스텀 메세지박스를 표시
        /// </summary>
        /// <param name="text">메시지 내용</param>
        /// <param name="caption">캡션</param>
        /// <param name="messageBoxButtons">메세지박스 표시 버튼</param>
        /// <param name="messageBoxIcon">메세지박스 아이콘</param>
        /// <returns>OK나 Yes 클릭시만 True</returns>
        protected bool ShowCustomMessage(string text, string caption, MessageBoxButtons messageBoxButtons, MessageBoxIcon messageBoxIcon)
        {
            bool rtnValue = false;

            DialogResult result = MessageBox.Show(text, caption, messageBoxButtons, messageBoxIcon);
            if (result == DialogResult.OK || result == DialogResult.Yes)
                rtnValue = true;

            return rtnValue;
        }
        #endregion

        #region OpenExcelDialogAndGetData - OpenFileDialog를 띄워 선택한 엑셀파일의 데이터를 DataSet 형태로 가져옴
        /// <summary>
        /// OpenFileDialog를 띄워 선택한 엑셀파일의 데이터를 DataSet 형태로 가져옴
        /// </summary>
        /// <param name="importType">사용할 ExcelImportType</param>
        /// <returns>DataSet 형태의 선택한 엑셀파일 데이터</returns>
        protected DataSet OpenExcelDialogAndGetData(ExcelImportType importType)
        {
            ExcelImportType usingImportType;
            return this.OpenExcelDialogAndGetData(importType, ExcelImportHDRType.Yes, ExcelImportIMEXType.IMEX2, out usingImportType);
        }

        /// <summary>
        /// OpenFileDialog를 띄워 선택한 엑셀파일의 데이터를 DataSet 형태로 가져옴
        /// </summary>
        /// <param name="usingImportType">사용된 ExcelImportType</param>
        /// <returns>DataSet 형태의 선택한 엑셀파일 데이터</returns>
        protected DataSet OpenExcelDialogAndGetData(out ExcelImportType usingImportType)
        {
            return this.OpenExcelDialogAndGetData(ExcelImportType.Dynamic, ExcelImportHDRType.Yes, ExcelImportIMEXType.IMEX2, out usingImportType);
        }

        /// <summary>
        /// OpenFileDialog를 띄워 선택한 엑셀파일의 데이터를 DataSet 형태로 가져옴
        /// </summary>
        /// <param name="importType">사용할 ExcelImportType</param>
        /// <param name="hdr">OleDB Excel Import 시 Excel의 첫번째 줄의 데이터를 Field 명으로 인식 할 것인지 여부. ExcelImportType이 OleDB인 경우만 사용</param>
        /// <param name="imex">OleDB Excel Import 시 OleDB IMEX 값. ExcelImportType이 OleDB인 경우만 사용</param>
        /// <returns>DataSet 형태의 선택한 엑셀파일 데이터</returns>
        protected DataSet OpenExcelDialogAndGetData(ExcelImportType importType, ExcelImportHDRType hdr, ExcelImportIMEXType imex)
        {
            ExcelImportType usingImportType;
            return this.OpenExcelDialogAndGetData(importType, hdr, imex, out usingImportType);
        }

        /// <summary>
        /// OpenFileDialog를 띄워 선택한 엑셀파일의 데이터를 DataSet 형태로 가져옴
        /// </summary>
        /// <param name="hdr">OleDB Excel Import 시 Excel의 첫번째 줄의 데이터를 Field 명으로 인식 할 것인지 여부. ExcelImportType이 OleDB인 경우만 사용</param>
        /// <param name="imex">OleDB Excel Import 시 OleDB IMEX 값. ExcelImportType이 OleDB인 경우만 사용</param>
        /// <param name="usingImportType">사용된 ExcelImportType</param>
        /// <returns>DataSet 형태의 선택한 엑셀파일 데이터</returns>
        protected DataSet OpenExcelDialogAndGetData(ExcelImportHDRType hdr, ExcelImportIMEXType imex, out ExcelImportType usingImportType)
        {
            return this.OpenExcelDialogAndGetData(ExcelImportType.Dynamic, hdr, imex, out usingImportType);
        }

        /// <summary>
        /// OpenFileDialog를 띄워 선택한 엑셀파일의 데이터를 DataSet 형태로 가져옴
        /// </summary>
        /// <param name="importType">사용할 ExcelImportType</param>
        /// <param name="hdr">OleDB Excel Import 시 Excel의 첫번째 줄의 데이터를 Field 명으로 인식 할 것인지 여부. ExcelImportType이 OleDB인 경우만 사용</param>
        /// <param name="imex">OleDB Excel Import 시 OleDB IMEX 값. ExcelImportType이 OleDB인 경우만 사용</param>
        /// <param name="usingImportType">사용된 ExcelImportType</param>
        /// <returns>DataSet 형태의 선택한 엑셀파일 데이터</returns>
        private DataSet OpenExcelDialogAndGetData(ExcelImportType importType, ExcelImportHDRType hdr, ExcelImportIMEXType imex, out ExcelImportType usingImportType)
        {
            DataSet rtnValue = null;
            usingImportType = ExcelImportType.Automation;

            string filePath = this.OpenExcelDialog();

            if (!string.IsNullOrEmpty(filePath))
            {
                switch (importType)
                {
                    case ExcelImportType.Automation:
                        rtnValue = this.GetExcelDataByAutomation(filePath);
                        break;
                    case ExcelImportType.OleDB:
                        rtnValue = this.GetExcelDataByOleDB(filePath, hdr, imex);
                        usingImportType = ExcelImportType.OleDB;
                        break;
                    default:
                        try
                        {
                            rtnValue = this.GetExcelDataByAutomation(filePath);
                        }
                        catch
                        {
                            rtnValue = this.GetExcelDataByOleDB(filePath, hdr, imex);
                            usingImportType = ExcelImportType.OleDB;
                        }
                        break;
                }
            }

            return rtnValue;
        }
        #endregion

        #region OpenExcelDialog - Excel 파일을 선택할 수 있는 File Dialog를 띄움
        /// <summary>
        /// Excel 파일을 선택할 수 있는 File Dialog를 띄움
        /// </summary>
        /// <returns>선택한 Excel 파일</returns>
        protected string OpenExcelDialog()
        {
            string rtnValue = null;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "엑셀 가져오기";
                openFileDialog.CheckFileExists = true;
                openFileDialog.DefaultExt = "xls";
                openFileDialog.DereferenceLinks = true;
                openFileDialog.Filter = "Excel 97 - 2003 통합 문서 (*.xls)|*.xls|Excel 통합 문서 (*.xlsx)|*.xlsx";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                    rtnValue = openFileDialog.FileName;
            }

            return rtnValue;
        }
        #endregion

        #region GetExcelDataByOleDB - OleDB를 이용하여 해당 경로의 엑셀파일의 데이터를 DataSet 형태로 가져옴
        /// <summary>
        /// OleDB를 이용하여 해당 경로의 엑셀파일의 데이터를 DataSet 형태로 가져옴
        /// </summary>
        /// <param name="filePath">파일 경로</param>
        /// <param name="hdr">OleDB Excel Import 시 Excel의 첫번째 줄의 데이터를 Field 명으로 인식 할 것인지 여부</param>
        /// <param name="imex">OleDB Excel Import 시 OleDB IMEX 값</param>
        /// <returns>DataSet 형태의 해당 경로 엑셀파일 데이터</returns>
        protected DataSet GetExcelDataByOleDB(string filePath, ExcelImportHDRType hdr, ExcelImportIMEXType imex)
        {
            DataSet rtnValue = null;

            string szConn = null;
            string hdrString = (hdr == ExcelImportHDRType.Yes ? "Yes" : "No");
            string imexString = (imex == ExcelImportIMEXType.IMEX0 ? "0" : (imex == ExcelImportIMEXType.IMEX1 ? "1" : "2"));

            switch (this.GetExcelFileType(filePath))
            {
                case -2:
                    szConn = null;
                    break;
                case -1:
                    szConn = null;
                    break;
                case 0:
                    szConn = string.Format(xlsConnectionString, filePath, hdrString, imexString);
                    break;
                case 1:
                    szConn = string.Format(xlsxConnectionString, filePath, hdrString, imexString);
                    break;
            }

            if (!string.IsNullOrEmpty(szConn))
            {
                using (OleDbConnection conn = new OleDbConnection(szConn))
                {
                    try
                    {
                        OleDbCommand cmd;
                        OleDbDataAdapter adpt;
                        string sheetTableName;

                        conn.Open();

                        using (DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null))
                        {
                            if (dt != null)
                            {
                                rtnValue = new DataSet();

                                foreach (DataRow dr in dt.Rows)
                                {
                                    sheetTableName = Convert.ToString(dr["TABLE_NAME"]);

                                    using (cmd = new OleDbCommand(@"SELECT * FROM [" + sheetTableName + "]", conn))
                                    using (adpt = new OleDbDataAdapter(cmd))
                                    {
                                        rtnValue.Tables.Add(sheetTableName);
                                        adpt.Fill(rtnValue, sheetTableName);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        rtnValue = null;
                        throw exc;
                    }
                    finally
                    {
                        if (conn != null)
                            conn.Close();
                    }
                }
            }

            return rtnValue;
        }
        #endregion

        #region GetExcelDataByAutomation - Microsoft.Office.Interop.Excel.dll(v2.0.50727)을 이용하여 해당 경로의 엑셀파일의 데이터를 DataSet 형태로 가져옴
        /// <summary>
        /// Microsoft.Office.Interop.Excel.dll(v2.0.50727)을 이용하여 해당 경로의 엑셀파일의 데이터를 DataSet 형태로 가져옴 
        /// </summary>
        /// <param name="filePath">파일 경로</param>
        /// <returns>DataSet 형태의 해당 경로 엑셀파일 데이터</returns>
        protected DataSet GetExcelDataByAutomation(string filePath)
        {
            DataSet rtnValue = null;

            Microsoft.Office.Interop.Excel.Application excelApp = null;
            Microsoft.Office.Interop.Excel.Workbook wb = null;

            try
            {
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                wb = excelApp.Workbooks.Open(filePath);
                DataTable sheetData;
                DataRow rowData;
                int columnCount;
                int rowCount;
                Microsoft.Office.Interop.Excel.Range range;

                rtnValue = new DataSet();
                foreach (Microsoft.Office.Interop.Excel.Worksheet ws in wb.Sheets)
                {
                    columnCount = ws.UsedRange.Cells.Columns.Count;
                    rowCount = ws.UsedRange.Cells.Rows.Count;

                    sheetData = new DataTable(ws.Name);

                    for (int i = 1; i <= columnCount; i++)
                        sheetData.Columns.Add(this.GetExcelColumnName(i - 1), typeof(string));

                    for (int i = 1; i <= rowCount; i++)
                    {
                        rowData = sheetData.NewRow();

                        for (int j = 1; j <= columnCount; j++)
                        {
                            range = (Microsoft.Office.Interop.Excel.Range)(ws.Cells[i, j]);
                            rowData[this.GetExcelColumnName(j - 1)] = range.Text;
                        }

                        sheetData.Rows.Add(rowData);
                    }

                    rtnValue.Tables.Add(sheetData);
                    this.ReleaseExcelObject(ws);
                }
            }
            catch (Exception exc)
            {
                rtnValue = null;
                throw exc;
            }
            finally
            {
                // Clean up
                if (wb != null)
                    wb.Close();
                this.ReleaseExcelObject(wb);
                this.ReleaseExcelObject(excelApp);
            }

            return rtnValue;
        }

        private string GetExcelColumnName(int columnIndex)
        {
            string rtnValue = string.Empty;
            string upperAlphatbet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            for (; ; )
            {
                rtnValue = upperAlphatbet.Substring(columnIndex % 26, 1) + rtnValue;
                if (columnIndex / 26 > 0)
                    columnIndex = columnIndex / 26;
                else
                    break;
            }

            return rtnValue;
        }

        private void ReleaseExcelObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }
        #endregion

        #region GetExcelFileType - 해당 경로의 엑셀 파일의 종류를 확인
        /// <summary>
        /// 해당 경로의 엑셀 파일의 종류를 확인
        /// </summary>
        /// <param name="filePath">엑셀 파일 경로</param>
        /// <returns>-2=error, -1=not excel , 0=xls , 1=xlsx</returns>
        protected int GetExcelFileType(string filePath)
        {
            // 요거이 비교할 파일 데이터 입니다.
            byte[,] ExcelHeader = {
                    { 0xD0, 0xCF, 0x11, 0xE0, 0xA1 }, // XLS  File Header
                    { 0x50, 0x4B, 0x03, 0x04, 0x14 }  // XLSX File Header
                };

            // result -2=error, -1=not excel , 0=xls , 1=xlsx
            int result = -1;

            FileInfo FI = new FileInfo(filePath);
            FileStream FS = FI.Open(FileMode.Open);

            try
            {
                byte[] FH = new byte[5];

                FS.Read(FH, 0, 5);

                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (FH[j] != ExcelHeader[i, j]) break;
                        else if (j == 4) result = i;
                    }
                    if (result >= 0) break;
                }
            }
            catch
            {
                result = (-2);
                //throw e;
            }
            finally
            {
                FS.Close();
            }
            return result;
        }
        #endregion

        #region SetCodeBoxApplyProcedure - 해당 코드박스의 프로시져를 설정
        /// <summary>
        /// 해당 코드박스의 프로시져를 설정
        /// </summary>
        /// <param name="codeBox">코드박스</param>
        /// <param name="procedureNo">프로시져No</param>
        protected void SetCodeBoxApplyProcedure(TYCodeBox codeBox, string procedureNo)
        {
            codeBox.Option.Remove("C36");
            codeBox.Option.Remove("C37");
            codeBox.Option.Remove("C67");
            codeBox.Option["C39"] = "TY.ER.GB00.TYERGB003P";
            codeBox.Option.Add("C36", procedureNo);
            codeBox.SetIPopupHelper(new TYCodeBoxPopup());
        } 
        #endregion

        #region SetCodeBoxApplyCode - 해당 코드박스의 코드를 설정
        /// <summary>
        /// 해당 코드박스의 코드를 설정
        /// </summary>
        /// <param name="codeBox">코드박스</param>
        /// <param name="code">코드</param>
        protected void SetCodeBoxApplyCode(TYCodeBox codeBox, string code)
        {
            codeBox.Option.Remove("C36");
            codeBox.Option.Remove("C37");
            codeBox.Option.Remove("C67");
            codeBox.Option["C39"] = "TY.ER.GB00.TYERGB003P";
            codeBox.Option.Add("C37", code);
            codeBox.SetIPopupHelper(new TYCodeBoxPopup());
        } 
        #endregion

        #region 숫자 입력 텍스트박스는  000,000,000 형식을 000000000형태의 decimal 로 바꿔주는 메서드
        protected string Get_Numeric(string sStr)
        {
            if (sStr == "") return "0";
            else return sStr.Replace(",", "");
        }
        #endregion

        #region Description : string 값을 입력받아서 2자리로 바꿔주는 메소드
        protected string Set_Fill2(string sFirst)
        {
            if (sFirst.Length == 1)
            {
                sFirst = "0" + sFirst;
            }
            else if (sFirst.Length == 2)
            {
                sFirst = sFirst;
            }
            else sFirst = "00";

            return sFirst;
        }
        #endregion

        #region Description : string 값을 입력받아서 3자리로 변형해서 Return 해준다
        protected string Set_Fill3(string sFirst)
        {
            if (sFirst.Length == 1)
            {
                sFirst = "00" + sFirst;
            }
            else if (sFirst.Length == 2)
            {
                sFirst = "0" + sFirst;
            }
            else if (sFirst.Length == 3)
            {
                sFirst = sFirst;
            }
            else sFirst = "000";

            return sFirst;
        }
        #endregion

        #region Description : string 값을 입력받아서 4자리로 변형해서 Return 해준다
        protected string Set_Fill4(string sFirst)
        {
            if (sFirst.Length == 1)
            {
                sFirst = "000" + sFirst;
            }
            else if (sFirst.Length == 2)
            {
                sFirst = "00" + sFirst;
            }
            else if (sFirst.Length == 3)
            {
                sFirst = "0" + sFirst;
            }
            else if (sFirst.Length == 4)
            {
                sFirst = sFirst;
            }
            else sFirst = "0000";

            return sFirst;
        }
        #endregion

        #region Description : string 값을 입력받아서 5자리로 변형해서 Return 해준다
        //==================================================================================================
        // string 값을 입력받아서 5자리로 변형해서 Return 해준다.
        // 2002-11-20 추가 
        //==================================================================================================		
        protected string Set_Fill5(string sFirst)
        {
            if (sFirst.Length == 1)
            {
                sFirst = "0000" + sFirst;
            }
            else if (sFirst.Length == 2)
            {
                sFirst = "000" + sFirst;
            }
            else if (sFirst.Length == 3)
            {
                sFirst = "00" + sFirst;
            }
            else if (sFirst.Length == 4)
            {
                sFirst = "0" + sFirst;
            }

            return sFirst;
        } 
        #endregion		


        #region Description  : 날짜 유효성 검사
        protected bool dateValidateCheck(string syymmdd)
        {
            int i_year, i_month, i_day;
            i_year = i_month = i_day = 0;

            string sDate = syymmdd.Trim().Replace("-", "");
            string year = sDate.Trim().Substring(0, 4);
            string month = sDate.Trim().Substring(4, 2);
            string day = sDate.Trim().Substring(6, 2);

            // Year(4) + Month(2) + Day(2)  = 8
            if (sDate.Length == 8)
            {
                // 문자열을 정수로 변환
                i_year = Convert.ToInt32(year);
                i_month = Convert.ToInt32(month);
                i_day = Convert.ToInt32(day);
                // 월 확인
                if ((i_month <= 0) || (12 < i_month)) // Check 01 ~ 12
                { // 음수 이거나 12보다 크다면 잘못된 날짜
                    return false;
                }
                // 일 확인
                if ((i_month == 1) || (i_month == 3) || (i_month == 5) || (i_month == 7) || (i_month == 8) || (i_month == 10) || (i_month == 12))
                { // 1, 3, 5, 7, 8, 10, 12월은 31일까지
                    if ((i_day <= 0) || (31 < i_day))
                    { // 1 ~ 31이 아니면 잘못된 날짜
                        return false;
                    }
                }
                else if (i_month == 2)
                { // 2월인 경우 28일, 윤년은 29일
                    if ((i_year % 4) == 0) // 윤년 Check(4로 나누어서 떨어지면 윤년)
                    {
                        if ((i_year % 100) == 0) // 평년 Check(100으로도 나누어 떨어지면
                        {
                            if ((i_year % 400) == 0) // 윤년 Check(400으로 나누어 떨어지면
                            {
                                if ((i_day <= 0) || (29 < i_day)) // 윤년인 경우
                                {
                                    return false;
                                }
                            }
                            else // 평년인 경우
                            {
                                if ((i_day <= 0) || (28 < i_day))
                                {
                                    return false;
                                }
                            }
                        }
                        else // 윤년인 경우
                        {
                            if ((i_day <= 0) || (29 < i_day))
                            {
                                return false;
                            }
                        }
                    }
                    else // 평년인 경우
                    {
                        if ((i_day <= 0) || (28 < i_day))
                        {
                            return false;
                        }
                    }
                }
                else
                { // 4, 6, 9, 11월은 30일까지
                    if ((i_day <= 0) || (30 < i_day))
                    { // 1 ~ 30이 아니면 잘못된 날짜
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }
        #endregion


        #region PC IP정보 가져오기
        public string IPAdresss
        {
            get
            {
                IPHostEntry IPHost = Dns.GetHostByName(Dns.GetHostName());

                return IPHost.AddressList[0].ToString();
            }
        }
        #endregion

        #region Description : 소수점 이하 절삭하는 함수
        protected string UP_DotDelete(string sStr)
        {
            string sValue = "";
            for (int i = 0; i < sStr.Length; i++)
            {
                if (sStr.Substring(i, 1) != ".")
                {
                    sValue = sValue + sStr.Substring(i, 1);
                }
                else
                {
                    break;
                }
            }
            return sValue;
        } 
        #endregion


        #region Descrioption : 소수점 둘째 자리 절삭하는 함수
        protected string UP_DotDelete2(string sStr)
        {
            string sValue = "";
            int j = 0;
            for (int i = 0; i < sStr.Length; i++)
            {
                if (j == 0)
                {
                    if (sStr.Substring(i, 1) != ".")
                    {
                        sValue = sValue + sStr.Substring(i, 1);
                    }
                    else
                    {
                        j = j + 1;
                        sValue = sValue + sStr.Substring(i, 1);
                    }
                }
                else
                {
                    if (j > 2)
                    {
                        break;
                    }
                    else
                    {
                        j = j + 1;
                        sValue = sValue + sStr.Substring(i, 1);
                    }
                }
            }
            return sValue;
        } 
        #endregion

        #region 자리수 만큼 짤라주는 함수
        protected  string StringTransfer(string inputString, int stringLength)
        {
            int ihangulStart = 0;

            inputString = inputString.Replace("（", "(");
            inputString = inputString.Replace("）", ")");

            int strLen = inputString.Length;

            int sByteTotal_Len = 0;

            string sByteStr = "";

            for (int i = 0; i < strLen; i++)
            {
                string sValue = inputString.Substring(i, 1);

                int sByteLen = ASCIIEncoding.Default.GetByteCount(sValue);

                // 한글인지 체크
                if (sByteLen == 2)
                {
                    //이전문자도 한글인지 체크
                    if (i != 0 && ihangulStart == 2)
                    {
                        //이전문자도 한글이면 byte수 증가 안시킴

                    }
                    else
                    {
                        //한글이면 시작으로( oe of ) 보고 2byte ++
                        sByteTotal_Len = sByteTotal_Len + 2;
                    }
                }

                sByteTotal_Len = sByteTotal_Len + sByteLen;

                if (sByteTotal_Len <= stringLength)
                {
                    sByteStr = sByteStr + sValue;
                }

                //현재문자 byte수 기록   
                ihangulStart = sByteLen;

            }

            sByteStr = sByteStr.Replace("'", "''");

            return sByteStr;

        }
        #endregion
        //==================================================================================================						
        // Description : "&nbsp;" 이면 빈공백을 리턴함.
        //==================================================================================================		
        protected string SetDefaultValue(string sStr)
        {
            if (sStr.Equals("&nbsp;")) return "";
            else return sStr;
        }

        #region Description : 전표 데이터셋 변환
        protected DataTable UP_ConvertJunPyo(DataTable dt)
        {
            string sTMB2DPMK     = string.Empty;
            string sTMB2DTMK     = string.Empty;
            string sTMB2NOSQ     = string.Empty;

            string sTMB2DPMKOld  = string.Empty;
            string sTMB2DTMKOld  = string.Empty;
            string sTMB2NOSQOld  = string.Empty;

            string sTMB2DPNMOld  = string.Empty;
            string sTMB2HISABOld = string.Empty;
            string sDATEOld      = string.Empty;
            string sTMB2IDJPOld  = string.Empty;
            string sTMB2NOLNOld  = string.Empty;
            string sTMB2VLMI6Old = string.Empty;

            string sFilter       = string.Empty;

            int iCount        = 0;
            int iBLANK        = 0;

            int iTOTALPAGEOld = 0;
            int iNOWPAGEOld   = 0;

            // 페이지계
            double dTMB2AMDR  = 0;
            double dTMB2AMCR  = 0;

            // 합계
            double dTMB2DRHAP = 0;
            double dTMB2CRHAP = 0;

            int i = 0;
            int j = 0;

            sTMB2DPMK     = "";
            sTMB2DTMK     = "";
            sTMB2NOSQ     = "";

            sTMB2DPMKOld  = "";
            sTMB2DTMKOld  = "";
            sTMB2NOSQOld  = "";

            sTMB2DPNMOld  = "";
            sTMB2HISABOld = "";
            sDATEOld      = "";
            sTMB2IDJPOld  = "";
            sTMB2NOLNOld  = "";
            sTMB2VLMI6Old = "";

            sFilter       = "";

            iTOTALPAGEOld = 0;
            iNOWPAGEOld   = 0;
            // 페이지계
            dTMB2AMDR     = 0;
            dTMB2AMCR     = 0;
            // 합계
            dTMB2DRHAP    = 0;
            dTMB2CRHAP    = 0;

            DataTable Retdt = new DataTable();

            if (dt != null)
            {
                iCount = 0;

                DataRow row;

                Retdt.Columns.Add("TMB2DPNM",   typeof(System.String));
                Retdt.Columns.Add("TMB2DPMK",   typeof(System.String));
                Retdt.Columns.Add("DATE",       typeof(System.String));
                Retdt.Columns.Add("TMB2HISAB",  typeof(System.String));
                Retdt.Columns.Add("TMB2DTMK",   typeof(System.String));
                Retdt.Columns.Add("TMB2NOSQ",   typeof(System.String));
                Retdt.Columns.Add("TMB2IDJP",   typeof(System.String));
                Retdt.Columns.Add("TMB2CDNM",   typeof(System.String));
                Retdt.Columns.Add("TMB2VLMI1",  typeof(System.String));
                Retdt.Columns.Add("TMB2VLMI2",  typeof(System.String));
                Retdt.Columns.Add("TMB2VLMI3",  typeof(System.String));
                Retdt.Columns.Add("TMB2VLMI4",  typeof(System.String));
                Retdt.Columns.Add("TMB2VLMI5",  typeof(System.String));
                Retdt.Columns.Add("TMB2VLMI6",  typeof(System.String));
                Retdt.Columns.Add("TMB2ACNM",   typeof(System.String));
                Retdt.Columns.Add("TMB2RKAC",   typeof(System.String));
                Retdt.Columns.Add("TMB2AMDR",   typeof(System.String));
                Retdt.Columns.Add("TMB2AMCR",   typeof(System.String));
                Retdt.Columns.Add("TMB2NOLN",   typeof(System.String));
                Retdt.Columns.Add("TMB2BCDNM",  typeof(System.String));
                Retdt.Columns.Add("TMNOWDRHAP", typeof(System.String));
                Retdt.Columns.Add("TMNOWCRHAP", typeof(System.String));
                Retdt.Columns.Add("TMB2DRHAP",  typeof(System.String));
                Retdt.Columns.Add("TMB2CRHAP",  typeof(System.String));
                Retdt.Columns.Add("NOWPAGE",    typeof(System.String));
                Retdt.Columns.Add("TOTALPAGE",  typeof(System.String));

                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    sTMB2DPMK = dt.Rows[i]["TMB2DPMK"].ToString();
                    sTMB2DTMK = dt.Rows[i]["TMB2DTMK"].ToString();
                    sTMB2NOSQ = dt.Rows[i]["TMB2NOSQ"].ToString();

                    if (i == 0)
                    {
                        sTMB2DPMKOld = sTMB2DPMK;
                        sTMB2DTMKOld = sTMB2DTMK;
                        sTMB2NOSQOld = sTMB2NOSQ;
                    }

                    if ((sTMB2DPMK != sTMB2DPMKOld || sTMB2DTMK != sTMB2DTMKOld ||
                         sTMB2NOSQ != sTMB2NOSQOld) && sTMB2DPMKOld != "")
                    {
                        // 페이지계
                        sFilter = "";
                        sFilter = sFilter + " TMB2DPNM = '" + sTMB2DPNMOld.ToString() + "' AND  ";
                        sFilter = sFilter + " TMB2DTMK = '" + sTMB2DTMKOld.ToString() + "' AND  ";
                        sFilter = sFilter + " TMB2NOSQ =  " + sTMB2NOSQOld.ToString() + "  AND  ";
                        sFilter = sFilter + " NOWPAGE  =  " + Convert.ToString(iNOWPAGEOld) + " ";

                        // 페이지계
                        dTMB2AMDR = Convert.ToDouble(dt.Compute("SUM(TMB2AMDR)", sFilter).ToString());
                        dTMB2AMCR = Convert.ToDouble(dt.Compute("SUM(TMB2AMCR)", sFilter).ToString());

                        // 합계
                        sFilter = "";
                        sFilter = sFilter + " TMB2DPNM = '" + sTMB2DPNMOld.ToString() + "' AND ";
                        sFilter = sFilter + " TMB2DTMK = '" + sTMB2DTMKOld.ToString() + "' AND ";
                        sFilter = sFilter + " TMB2NOSQ =  " + sTMB2NOSQOld.ToString() + "      ";

                        // 합계
                        dTMB2DRHAP = Convert.ToDouble(dt.Compute("SUM(TMB2AMDR)", sFilter).ToString());
                        dTMB2CRHAP = Convert.ToDouble(dt.Compute("SUM(TMB2AMCR)", sFilter).ToString());

                        iBLANK = 0;
                        iBLANK = iTOTALPAGEOld * 6 - iCount;

                        for (j = 1; j <= iBLANK; j++)
                        {
                            row = Retdt.NewRow();

                            row["TMB2DPNM"]   = sTMB2DPNMOld.ToString();
                            row["TMB2DPMK"]   = sTMB2DPMKOld.ToString();
                            row["DATE"]       = sDATEOld.ToString();
                            row["TMB2HISAB"]  = sTMB2HISABOld.ToString();
                            row["TMB2DTMK"]   = sTMB2DTMKOld.ToString();
                            row["TMB2NOSQ"]   = sTMB2NOSQOld.ToString();
                            row["TMB2IDJP"]   = sTMB2IDJPOld.ToString();
                            row["TMB2CDNM"]   = "";
                            row["TMB2VLMI1"]  = "";
                            row["TMB2VLMI2"]  = "";
                            row["TMB2VLMI3"]  = "";
                            row["TMB2VLMI4"]  = "";
                            row["TMB2VLMI5"]  = "";
                            row["TMB2VLMI6"]  = sTMB2VLMI6Old.ToString();
                            row["TMB2ACNM"]   = "";
                            row["TMB2RKAC"]   = "";
                            row["TMB2AMDR"]   = 0;
                            row["TMB2AMCR"]   = 0;
                            row["TMB2NOLN"]   = Convert.ToString(int.Parse(sTMB2NOLNOld.ToString()) + j);
                            row["TMB2BCDNM"]  = "";
                            row["TMNOWDRHAP"] = string.Format("{0:#,###}", Convert.ToString(dTMB2AMDR));
                            row["TMNOWCRHAP"] = string.Format("{0:#,###}", Convert.ToString(dTMB2AMCR));
                            row["TMB2DRHAP"]  = string.Format("{0:#,###}", Convert.ToString(dTMB2DRHAP));
                            row["TMB2CRHAP"]  = string.Format("{0:#,###}", Convert.ToString(dTMB2CRHAP));

                            row["NOWPAGE"]    = Convert.ToString(iNOWPAGEOld);
                            row["TOTALPAGE"]  = Convert.ToString(iTOTALPAGEOld);

                            Retdt.Rows.Add(row);
                        }

                        iCount = 0;
                    }

                    // 페이지계
                    sFilter = "";
                    sFilter = sFilter + " TMB2DPNM = '" + dt.Rows[i]["TMB2DPNM"].ToString() + "' AND ";
                    sFilter = sFilter + " TMB2DTMK = '" + dt.Rows[i]["TMB2DTMK"].ToString() + "' AND ";
                    sFilter = sFilter + " TMB2NOSQ =  " + dt.Rows[i]["TMB2NOSQ"].ToString() + "  AND ";
                    sFilter = sFilter + " NOWPAGE  =  " + dt.Rows[i]["NOWPAGE"].ToString() + "      ";

                    // 페이지계
                    dTMB2AMDR = Convert.ToDouble(dt.Compute("SUM(TMB2AMDR)", sFilter).ToString());
                    dTMB2AMCR = Convert.ToDouble(dt.Compute("SUM(TMB2AMCR)", sFilter).ToString());

                    // 합계
                    sFilter = "";
                    sFilter = sFilter + " TMB2DPNM = '" + dt.Rows[i]["TMB2DPNM"].ToString() + "' AND ";
                    sFilter = sFilter + " TMB2DTMK = '" + dt.Rows[i]["TMB2DTMK"].ToString() + "' AND ";
                    sFilter = sFilter + " TMB2NOSQ =  " + dt.Rows[i]["TMB2NOSQ"].ToString() + "      ";

                    // 합계
                    dTMB2DRHAP = Convert.ToDouble(dt.Compute("SUM(TMB2AMDR)", sFilter).ToString());
                    dTMB2CRHAP = Convert.ToDouble(dt.Compute("SUM(TMB2AMCR)", sFilter).ToString());

                    row = Retdt.NewRow();

                    row["TMB2DPNM"]   = dt.Rows[i]["TMB2DPNM"].ToString();
                    row["TMB2DPMK"]   = dt.Rows[i]["TMB2DPMK"].ToString();
                    row["DATE"]       = dt.Rows[i]["DATE"].ToString();
                    row["TMB2HISAB"]  = dt.Rows[i]["TMB2HISAB"].ToString();
                    row["TMB2DTMK"]   = dt.Rows[i]["TMB2DTMK"].ToString();
                    row["TMB2NOSQ"]   = dt.Rows[i]["TMB2NOSQ"].ToString();
                    row["TMB2IDJP"]   = dt.Rows[i]["TMB2IDJP"].ToString();
                    row["TMB2CDNM"]   = dt.Rows[i]["TMB2CDNM"].ToString();
                    if (dt.Rows[i]["B2CDMI1"].ToString() == "06" || dt.Rows[i]["B2CDMI1"].ToString() == "14")
                    {
                        row["TMB2VLMI1"] = string.Format("{0:#,###}", double.Parse(Get_Numeric(dt.Rows[i]["TMB2VLMI1"].ToString())));
                    }
                    else
                    {
                        row["TMB2VLMI1"] = dt.Rows[i]["TMB2VLMI1"].ToString();
                    }
                    
                    if (dt.Rows[i]["B2CDMI2"].ToString() == "06" || dt.Rows[i]["B2CDMI2"].ToString() == "14")
                    {
                        row["TMB2VLMI2"] = string.Format("{0:#,###}", double.Parse(Get_Numeric(dt.Rows[i]["TMB2VLMI2"].ToString())));
                    }
                    else
                    {
                        row["TMB2VLMI2"] = dt.Rows[i]["TMB2VLMI2"].ToString();
                    }

                    if (dt.Rows[i]["B2CDMI3"].ToString() == "06" || dt.Rows[i]["B2CDMI3"].ToString() == "14")
                    {
                        row["TMB2VLMI3"] = string.Format("{0:#,###}", double.Parse(Get_Numeric(dt.Rows[i]["TMB2VLMI3"].ToString())));
                    }
                    else
                    {
                        row["TMB2VLMI3"] = dt.Rows[i]["TMB2VLMI3"].ToString();
                    }

                    if (dt.Rows[i]["B2CDMI4"].ToString() == "06" || dt.Rows[i]["B2CDMI4"].ToString() == "14")
                    {
                        row["TMB2VLMI4"] = string.Format("{0:#,###}", double.Parse(Get_Numeric(dt.Rows[i]["TMB2VLMI4"].ToString())));
                    }
                    else
                    {
                        row["TMB2VLMI4"] = dt.Rows[i]["TMB2VLMI4"].ToString();
                    }

                    if (dt.Rows[i]["B2CDMI5"].ToString() == "06" || dt.Rows[i]["B2CDMI5"].ToString() == "14")
                    {
                        row["TMB2VLMI5"] = string.Format("{0:#,###}", double.Parse(Get_Numeric(dt.Rows[i]["TMB2VLMI5"].ToString())));
                    }
                    else
                    {
                        row["TMB2VLMI5"] = dt.Rows[i]["TMB2VLMI5"].ToString();
                    }

                    row["TMB2VLMI6"]  = dt.Rows[i]["TMB2VLMI6"].ToString();
                    row["TMB2ACNM"]   = dt.Rows[i]["TMB2ACNM"].ToString();
                    row["TMB2RKAC"]   = dt.Rows[i]["TMB2RKAC"].ToString();
                    row["TMB2AMDR"]   = dt.Rows[i]["TMB2AMDR"].ToString();
                    row["TMB2AMCR"]   = dt.Rows[i]["TMB2AMCR"].ToString();
                    row["TMB2NOLN"]   = dt.Rows[i]["TMB2NOLN"].ToString();
                    row["TMB2BCDNM"]  = dt.Rows[i]["TMB2BCDNM"].ToString();
                    row["TMNOWDRHAP"] = string.Format("{0:#,###}", Convert.ToString(dTMB2AMDR));
                    row["TMNOWCRHAP"] = string.Format("{0:#,###}", Convert.ToString(dTMB2AMCR));

                    if (dt.Rows[i]["TOTALPAGE"].ToString() == dt.Rows[i]["NOWPAGE"].ToString())
                    {
                        row["TMB2DRHAP"] = string.Format("{0:#,###}", Convert.ToString(dTMB2DRHAP));
                        row["TMB2CRHAP"] = string.Format("{0:#,###}", Convert.ToString(dTMB2CRHAP));
                    }
                    else
                    {
                        row["TMB2DRHAP"] = "0";
                        row["TMB2CRHAP"] = "0";
                    }

                    row["NOWPAGE"]    = dt.Rows[i]["NOWPAGE"].ToString();
                    row["TOTALPAGE"]  = dt.Rows[i]["TOTALPAGE"].ToString();

                    Retdt.Rows.Add(row);

                    iCount++;

                    iTOTALPAGEOld = int.Parse(dt.Rows[i]["TOTALPAGE"].ToString());
                    iNOWPAGEOld   = int.Parse(dt.Rows[i]["NOWPAGE"].ToString());

                    sTMB2DPMKOld  = sTMB2DPMK;
                    sTMB2DTMKOld  = sTMB2DTMK;
                    sTMB2NOSQOld  = sTMB2NOSQ;

                    sTMB2DPNMOld  = dt.Rows[i]["TMB2DPNM"].ToString();
                    sTMB2HISABOld = dt.Rows[i]["TMB2HISAB"].ToString();
                    sDATEOld      = dt.Rows[i]["DATE"].ToString();
                    sTMB2NOLNOld  = dt.Rows[i]["TMB2NOLN"].ToString();
                    sTMB2IDJPOld  = dt.Rows[i]["TMB2IDJP"].ToString();
                    sTMB2VLMI6Old = dt.Rows[i]["TMB2VLMI6"].ToString();
                }

                // 페이지계
                sFilter = "";
                sFilter = sFilter + " TMB2DPNM = '" + sTMB2DPNMOld.ToString() + "' AND  ";
                sFilter = sFilter + " TMB2DTMK = '" + sTMB2DTMKOld.ToString() + "' AND  ";
                sFilter = sFilter + " TMB2NOSQ =  " + sTMB2NOSQOld.ToString() + "  AND  ";
                sFilter = sFilter + " NOWPAGE  =  " + Convert.ToString(iNOWPAGEOld) + " ";

                // 페이지계
                dTMB2AMDR = Convert.ToDouble(dt.Compute("SUM(TMB2AMDR)", sFilter).ToString());
                dTMB2AMCR = Convert.ToDouble(dt.Compute("SUM(TMB2AMCR)", sFilter).ToString());

                // 합계
                sFilter = "";
                sFilter = sFilter + " TMB2DPNM = '" + sTMB2DPNMOld.ToString() + "' AND ";
                sFilter = sFilter + " TMB2DTMK = '" + sTMB2DTMKOld.ToString() + "' AND ";
                sFilter = sFilter + " TMB2NOSQ =  " + sTMB2NOSQOld.ToString() + "      ";

                // 합계
                dTMB2DRHAP = Convert.ToDouble(dt.Compute("SUM(TMB2AMDR)", sFilter).ToString());
                dTMB2CRHAP = Convert.ToDouble(dt.Compute("SUM(TMB2AMCR)", sFilter).ToString());

                iBLANK = 0;
                iBLANK = iTOTALPAGEOld * 6 - iCount;

                int iSeq = int.Parse(sTMB2NOSQOld.ToString());

                for (j = 1; j <= iBLANK; j++)
                {
                    row = Retdt.NewRow();

                    row["TMB2DPNM"]   = sTMB2DPNMOld.ToString();
                    row["TMB2DPMK"]   = sTMB2DPMKOld.ToString();
                    row["DATE"]       = sDATEOld.ToString();
                    row["TMB2HISAB"]  = sTMB2HISABOld.ToString();
                    row["TMB2DTMK"]   = sTMB2DTMKOld.ToString();
                    row["TMB2NOSQ"]   = sTMB2NOSQOld.ToString();
                    row["TMB2IDJP"]   = sTMB2IDJPOld.ToString();
                    row["TMB2CDNM"]   = "";
                    row["TMB2VLMI1"]  = "";
                    row["TMB2VLMI2"]  = "";
                    row["TMB2VLMI3"]  = "";
                    row["TMB2VLMI4"]  = "";
                    row["TMB2VLMI5"]  = "";
                    row["TMB2VLMI6"]  = sTMB2VLMI6Old.ToString();
                    row["TMB2ACNM"]   = "";
                    row["TMB2RKAC"]   = "";
                    row["TMB2AMDR"]   = 0;
                    row["TMB2AMCR"]   = 0;
                    row["TMB2NOLN"]   = Convert.ToString(int.Parse(sTMB2NOLNOld.ToString()) + j);
                    row["TMB2BCDNM"]  = "";
                    row["TMNOWDRHAP"] = string.Format("{0:#,###}", Convert.ToString(dTMB2AMDR));
                    row["TMNOWCRHAP"] = string.Format("{0:#,###}", Convert.ToString(dTMB2AMCR));
                    row["TMB2DRHAP"]  = string.Format("{0:#,###}", Convert.ToString(dTMB2DRHAP));
                    row["TMB2CRHAP"]  = string.Format("{0:#,###}", Convert.ToString(dTMB2CRHAP));

                    row["NOWPAGE"]    = Convert.ToString(iNOWPAGEOld);
                    row["TOTALPAGE"]  = Convert.ToString(iTOTALPAGEOld);

                    Retdt.Rows.Add(row);
                }
            }

            return Retdt;
        }
        #endregion

        #region 콤보박스에 값 셋팅하기
        protected void SetComboBox(TComboBox cmbBox, DataTable dt, int index)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                int count = dt.Rows.Count;

                for (int i = 0; i < count; i++)
                {
                    cmbBox.Items.Add(dt.Rows[i][index].ToString());
                }
                if (count > 0)
                {
                    cmbBox.SelectedIndex = 0;
                }
            }
        }
        #endregion

        #region Description : 구 인사 코드 변환
        protected string UP_Get_OldInsaCode(string sGubn, string sCode)
        {
            string sReturnCode;

            //입사구분   
            if (sGubn == "1")
            {
                switch (sCode)
                {
                    case "100":
                        sCode = "10";
                        break;
                    case "110":
                        sCode = "11";
                        break;
                    case "120":
                        sCode = "12";
                        break;
                    case "140":
                        sCode = "13";
                        break;
                    case "130":
                        sCode = "14";
                        break;
                    case "150":
                        sCode = "15";
                        break;
                    case "380":
                        sCode = "16";
                        break;
                    case "160":
                        sCode = "20";
                        break;
                    case "170":
                        sCode = "21";
                        break;
                    case "180":
                        sCode = "22";
                        break;
                    case "190":
                        sCode = "23";
                        break;
                    case "200":
                        sCode = "25";
                        break;
                    case "210":
                        sCode = "26";
                        break;
                    case "220":
                        sCode = "27";
                        break;
                    case "230":
                        sCode = "29";
                        break;
                    case "240":
                        sCode = "30";
                        break;
                    case "250":
                        sCode = "31";
                        break;
                    case "260":
                        sCode = "40";
                        break;
                    case "280":
                        sCode = "41";
                        break;
                    case "350":
                        sCode = "42";
                        break;
                    case "360":
                        sCode = "43";
                        break;
                    case "370":
                        sCode = "44";
                        break;
                    case "390":
                        sCode = "45";
                        break;
                    case "300":
                        sCode = "50";
                        break;
                    case "310":
                        sCode = "51";
                        break;
                    case "270":
                        sCode = "52";
                        break;
                    case "320":
                        sCode = "55";
                        break;
                    case "290":
                        sCode = "56";
                        break;
                    case "470":
                        sCode = "60";
                        break;
                    case "480":
                        sCode = "61";
                        break;
                    case "400":
                        sCode = "67";
                        break;
                    case "410":
                        sCode = "68";
                        break;
                    case "420":
                        sCode = "69";
                        break;
                    case "430":
                        sCode = "70";
                        break;
                    case "450":
                        sCode = "71";
                        break;
                    case "440":
                        sCode = "72";
                        break;
                    case "460":
                        sCode = "79";
                        break;
                    case "490":
                        sCode = "80";
                        break;
                    case "500":
                        sCode = "81";
                        break;
                    case "510":
                        sCode = "82";
                        break;
                    case "530":
                        sCode = "83";
                        break;
                    case "330":
                        sCode = "84";
                        break;
                    case "340":
                        sCode = "85";
                        break;
                    case "550":
                        sCode = "88";
                        break;
                    case "580":
                        sCode = "89";
                        break;
                    case "900":
                        sCode = "90";
                        break;
                    case "560":
                        sCode = "91";
                        break;
                    case "520":
                        sCode = "92";
                        break;
                    case "540":
                        sCode = "93";
                        break;
                    case "570":
                        sCode = "94";
                        break;
                    default:
                        sCode = "";
                        break;
                }
            }
            else if (sGubn == "2")
            {
                switch (sCode)
                {
                    case "010": sCode = "01"; break;
                    case "020": sCode = "02"; break;
                    case "030": sCode = "03"; break;
                    case "040": sCode = "04"; break;
                    case "050": sCode = "05"; break;
                    case "060": sCode = "06"; break;
                    case "070": sCode = "07"; break;
                    case "080": sCode = "08"; break;
                    case "090": sCode = "09"; break;
                    case "100": sCode = "10"; break;
                    case "110": sCode = "11"; break;
                    case "120": sCode = "12"; break;
                    case "130": sCode = "13"; break;
                    case "140": sCode = "14"; break;
                    case "150": sCode = "15"; break;
                    case "160": sCode = "16"; break;
                    case "170": sCode = "17"; break;
                    case "180": sCode = "18"; break;
                    case "190": sCode = "20"; break;
                    case "200": sCode = "21"; break;
                    case "210": sCode = "22"; break;
                    case "220": sCode = "23"; break;
                    case "230": sCode = "24"; break;
                    case "240": sCode = "30"; break;
                    case "260": sCode = "80"; break;
                    case "270": sCode = "90"; break;
                    case "300": sCode = "91"; break;
                    case "280": sCode = "92"; break;
                    case "250": sCode = "93"; break;
                    case "290": sCode = "94"; break;
                    case "310": sCode = "95"; break;
                    default:
                        sCode = "";
                        break;
                }
            }
            else if (sGubn == "3")
            {
                switch (sCode)
                {
                    case "010": sCode = "05"; break;
                    case "050": sCode = "06"; break;
                    case "020": sCode = "07"; break;
                    case "040": sCode = "08"; break;
                    case "060": sCode = "09"; break;
                    case "030": sCode = "10"; break;
                    case "070": sCode = "15"; break;
                    case "080": sCode = "18"; break;
                    case "090": sCode = "20"; break;
                    case "100": sCode = "30"; break;
                    case "110": sCode = "35"; break;
                    default:
                        sCode = "";
                        break;
                }
            }
            else if (sGubn == "4")
            {
                switch (sCode)
                {                   
                    case  "100":    sCode = "10"; break;
                     case  "110":    sCode = "11"; break;
                     case  "120":    sCode = "12"; break;
                     case  "140":    sCode = "13"; break;
                     case  "130":    sCode = "14"; break;
                     case  "150":    sCode = "15"; break;
                     case  "380":    sCode = "16"; break;
                     case  "160":    sCode = "20"; break;
                     case  "170":    sCode = "21"; break;
                     case  "180":    sCode = "22"; break;
                     case  "190":    sCode = "23"; break;
                     case  "200":    sCode = "25"; break;
                     case  "210":    sCode = "26"; break;
                     case  "220":    sCode = "27"; break;
                     case  "230":    sCode = "29"; break;
                     case  "240":    sCode = "30"; break;
                     case  "250":    sCode = "31"; break;
                     case  "260":    sCode = "40"; break;
                     case  "280":    sCode = "41"; break;
                     case  "350":    sCode = "42"; break;
                     case  "360":    sCode = "43"; break;
                     case  "370":    sCode = "44"; break;
                     case  "390":    sCode = "45"; break;
                     case  "300":    sCode = "50"; break;
                     case  "310":    sCode = "51"; break;
                     case  "270":    sCode = "52"; break;
                     case  "320":    sCode = "55"; break;
                     case  "290":    sCode = "56"; break;
                     case  "470":    sCode = "60"; break;
                     case  "480":    sCode = "61"; break;
                     case  "400":    sCode = "67"; break;
                     case  "410":    sCode = "68"; break;
                     case  "420":    sCode = "69"; break;
                     case  "430":    sCode = "70"; break;
                     case  "450":    sCode = "71"; break;
                     case  "440":    sCode = "72"; break;
                     case  "460":    sCode = "79"; break;
                     case  "490":    sCode = "80"; break;
                     case  "500":    sCode = "81"; break;
                     case  "510":    sCode = "82"; break;
                     case  "530":    sCode = "83"; break;
                     case  "330":    sCode = "84"; break;
                     case  "340":    sCode = "85"; break;
                     case  "550":    sCode = "88"; break;
                     case  "580":    sCode = "89"; break;
                     case  "900":    sCode = "90"; break;
                     case  "560":    sCode = "91"; break;
                     case  "520":    sCode = "92"; break;
                     case  "540":    sCode = "93"; break;
                     case "570":     sCode = "94"; break;
                    default:
                        sCode = "";
                        break;
                }
            }

            sReturnCode = sCode;

            return sReturnCode;
        }
        #endregion

        protected string[] getCONFGB(string sStr){

            string[] sRtn = new string[2];

            if (sStr != "")
            {
                sRtn[0] = sStr.Substring(0, 1);
                sRtn[1] = sStr.Substring(1, 1);
            }
            else
            {
                sRtn[0] = "";
                sRtn[1] = "";
            }
            return sRtn;
        }

        protected string getCONFGB(string sStr, int i)
        {
            string sReturn = string.Empty;

            if (sStr == "")
            {
                sReturn = "";
            }
            else
            {
                sReturn = sStr.Substring(i - 1, 1).ToString();
            }

            return sReturn;
        }

        protected string Set_Date(string sStr)
        {
            if (sStr.Length == 8)
            {
                sStr = sStr.Substring(0, 4) + "-" + sStr.Substring(4, 2) + "-" + sStr.Substring(6, 2);
            }
            else
            {
                sStr = "";
            }
            return sStr;
        }

        protected bool ChkIsNum(string s)
        {
            bool CheckVal = false;

            for (int i = 0; i < s.Length; i++)
            {
                if (System.Char.IsNumber(s, i))
                    CheckVal = true;
                else
                {
                    if (s.Substring(i, 1) == ".")
                        CheckVal = true;
                    else
                    {
                        CheckVal = false;
                        break;
                    } //end of IF
                } //end of IF
            } //end of FOR

            return CheckVal;
        }

        protected string Get_Date(string sStr)
        {
            if (sStr == "") return "";
            else return sStr.Replace("-", "");
        }

        //==================================================================================================
        // 탱크번호 4자리로 포멧형식 바꿈
        //==================================================================================================		
        protected string Set_TankNo(string sStr)
        {
            if (sStr.Length == 3) return sStr = " " + sStr.ToString();
            else return sStr;
        }

        protected string Set_Numeric(string sStr)
        {
            if (sStr.Trim() != "" && sStr.Trim() != "0")
            {
                sStr = decimal.Parse(sStr).ToString();

                int iPositionDot = sStr.IndexOf(".");
                int iFullStringLength = sStr.Length;

                // 전체길이 - 쩜의위치 - 1 하면 소수점 이하 갯수가 나온다.
                int iSubDotNum = iFullStringLength - iPositionDot - 1; 


                string sFormat = "#,0";
                if (iPositionDot != -1 && iSubDotNum > 0)
                {
                    sFormat += ".";
                    for (int i = 0; i < iSubDotNum; i++)
                    {
                        sFormat += "0";
                    }
                }
                return sStr = decimal.Parse(sStr).ToString(sFormat);
            }
            return "0";
        }

        protected string Set_Numeric2(string sStr, int iJariSu)
        {
            if (sStr.Trim() != "" && sStr.Trim() != "0")
            {
                sStr = decimal.Parse(sStr).ToString();


                int iPositionDot = sStr.IndexOf(".");
                int iFullStringLength = sStr.Length;

                // 전체길이 - 쩜의위치 - 1 하면 소수점 이하 갯수가 나온다.
                int iSubDotNum = iFullStringLength - iPositionDot - 1;

                string sFormat = "#,0";

                if (iSubDotNum < iJariSu)
                {
                    if (iJariSu == 0)
                    {
                        if (sStr != "" && sStr != "0") return sStr = double.Parse(sStr).ToString("#,#");
                    }
                    else if (iJariSu == 1)
                    {
                        if (sStr != "" && sStr != "0") return sStr = double.Parse(sStr).ToString("#,#.0");
                    }
                    else if (iJariSu == 2)
                    {
                        if (sStr != "" && sStr != "0") return sStr = double.Parse(sStr).ToString("#,#.00");
                    }
                    else if (iJariSu == 3)
                    {
                        if (sStr != "" && sStr != "0") return sStr = double.Parse(sStr).ToString("#,#.000");
                    }
                    else if (iJariSu == 4)
                    {
                        if (sStr != "" && sStr != "0") return sStr = double.Parse(sStr).ToString("#,#.0000");
                    }
                    else if (iJariSu == 5)
                    {
                        if (sStr != "" && sStr != "0") return sStr = double.Parse(sStr).ToString("#,#.00000");
                    }
                    else if (iJariSu == 6)
                    {
                        if (sStr != "" && sStr != "0") return sStr = double.Parse(sStr).ToString("#,#.000000");
                    }
                    else if (iJariSu == 7)
                    {
                        if (sStr != "" && sStr != "0") return sStr = double.Parse(sStr).ToString("#,#.0000000");
                    }
                    else if (iJariSu == 8)
                    {
                        if (sStr != "" && sStr != "0") return sStr = double.Parse(sStr).ToString("#,#.00000000");
                    }
                    return "0";
                }
                else if (iPositionDot != -1 && iSubDotNum > 0)
                {
                    sFormat += ".";
                    for (int i = 0; i < iSubDotNum; i++)
                    {
                        sFormat += "0";
                    }
                }
                return sStr = decimal.Parse(sStr).ToString(sFormat);
            }
            return "0";
        }

        #region Description : 대표거래처 코드 가져오기
        protected string Get_VNCODE(string sHWAJU)
        {
            string sVNRPCODE = string.Empty;

            sVNRPCODE = sHWAJU.ToString();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_66FD4200", sHWAJU.ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        sVNRPCODE = dt.Rows[i]["VNCODE"].ToString();
                    }
                    else
                    {
                        sVNRPCODE = sVNRPCODE + "," + dt.Rows[i]["VNCODE"].ToString();
                    }
                }
            }

            if (sVNRPCODE.ToString() == "")
            {
                sVNRPCODE = sHWAJU.ToString();
            }

            return sVNRPCODE;
        }
        #endregion

        #region Description : WinMate 경로 가져오기
        public string Get_WinmdatePath()
        {
            return "C:\\WINMATE";
        }
        #endregion

        #region  Description : UP_Set_Sha256 암호화 이벤트
        public string UP_Set_Sha256(string sPassWord)
        {
            string sSecurityPass;

            byte[] PassWordbyte = Encoding.UTF8.GetBytes(sPassWord);

            SHA256Managed sha256Managed = new SHA256Managed();

            byte[] encryptBytes = sha256Managed.ComputeHash(PassWordbyte);

            //base64 
            //sSecurityPass = Convert.ToBase64String(encryptBytes);

            //hex 16진수
            sSecurityPass = BitConverter.ToString(encryptBytes).Replace("-", "").ToString().ToLower();

            return sSecurityPass;
        }
        #endregion

        #region  Description : 파일 byte 변환 함수
        // Description :  파일 byte 변환 함수
        public static byte[] UP_Get_Byte(string filePath)
        {
            FileInfo file = new FileInfo(filePath);

            FileStream stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 2000);

            byte[] rawAssembly = new byte[(int)stream.Length];
            stream.Read(rawAssembly, 0, rawAssembly.Length);
            return rawAssembly; // <= byte[] 임
        }
        #endregion

        #region Description : 탭 메뉴를 열어주는 소스 이벤트 소스
        // 파라미터 넘겨주스 예제(form(form으로 넘겨주는 파라미터), 메뉴의 id, 탭 이름, 프로그램id)
        //TabPage_Add(new TYUTIN005I(sCSIPHANG, sCSBONSUN, sCSHWAJU, sCSHWAMUL, sCSBLNO, sCSMSNSEQ, sCSHSNSEQ, sCSCUSTIL, sCSCHASU, "EDI"), "TY66HF4298", "입항 및 통관관리", "TYUTIN005I");
        protected void TabPage_Add(Form form, string menuNo, string menuName, string programNo)
        {
            try
            {
                // 메뉴 NO
                TTabPage page = this.TabPage_GetTTabPage(form, menuNo);

                if (page == null)
                {
                    // 탭 페이지 생성
                    TabPage_Create(form, menuNo, menuName, programNo);
                }
                else
                {
                    // 탭 페이지 제거
                    this.TabPage_Remove(menuNo);

                    // 탭 페이지 생성
                    TabPage_Create(form, menuNo, menuName, programNo);
                }

                // 탭 페이지 선택
                this.TabPage_Select(form, menuNo);
            }
            catch (Exception e)
            {
                LocalCapturer.ExceptionCatch(e);
            }
        }

        protected TTabPage TabPage_GetTTabPage(Form form, string menuNo)
        {
            string sEXISTS = string.Empty;

            Form tmpForm = this.FindForm();
            if (form is TYBase)
            {
                ((TYBase)form).OwnerMDI = this.OwnerMDI;
            }

            int i = 0;

            for (i = 0; i < this.OwnerMDI.UCMM_TabControls.TabPages.Count; i++)
            {
                TTabPage page = (TTabPage)this.OwnerMDI.UCMM_TabControls.TabPages[i];
                if (page.MenuNo.Equals(menuNo))
                {
                    return page;
                }
            }

            return null;
        }

        protected void TabPage_Select(Form form, string menuNo)
        {
            try
            {
                if (form is TYBase)
                {
                    ((TYBase)form).OwnerMDI = this.OwnerMDI;
                }

                string programNo = "";
                for (int i = 0; i < this.OwnerMDI.UCMM_TabControls.TabPages.Count; i++)
                {
                    TTabPage page = (TTabPage)this.OwnerMDI.UCMM_TabControls.TabPages[i];
                    if (page.MenuNo.Equals(menuNo))
                    {
                        this.OwnerMDI.UCMM_TabControls.SelectedIndex = i;
                        programNo = page.ProgramNo;
                        break;
                    }
                }

                for (int i = 0; i < this.OwnerMDI.UCMM_MDIBody.Controls.Count; i++)
                {
                    string findno = (this.OwnerMDI.UCMM_MDIBody.Controls[i] is FormBase) ?
                        ((FormBase)this.OwnerMDI.UCMM_MDIBody.Controls[i]).ProgramNo :
                        ((InternetViewer)this.OwnerMDI.UCMM_MDIBody.Controls[i]).ProgramNo;

                    Form form1 = (Form)this.OwnerMDI.UCMM_MDIBody.Controls[i];
                    if (findno.Equals(programNo))
                    {
                        form1.FormBorderStyle = FormBorderStyle.None;
                        form1.Dock = DockStyle.Fill;
                        form1.Show();
                    }
                    else
                    {
                        form1.Hide();
                    }
                }
            }
            catch (Exception e)
            {
                LocalCapturer.ExceptionCatch(e);
            }
        }

        protected void TabPage_Create(Form form, string menuNo, string menuName, string programNo)
        {
            TTabPage page = this.TabPage_GetTTabPage(form, menuNo);

            Form tmpForm = this.FindForm();
            if (form is TYBase)
            {
                ((TYBase)form).OwnerMDI = this.OwnerMDI;
            }

            form.TopLevel = false;

            // 탭 이름
            form.Text = menuName;

            // 메뉴 NO(menuNo), 탭 이름(menuName), 프로그램 ID(programNo)
            page = new TTabPage(menuNo, menuName, programNo);
            page.BackColor = Color.FromArgb(240, 240, 240);

            this.OwnerMDI.UCMM_TabControls.TabPages.Add(page);

            this.OwnerMDI.UCMM_MDIBody.Controls.Add(form);
        }

        protected void TabPage_Remove(string menuNo)
        {
            try
            {
                for (int i = this.OwnerMDI.UCMM_TabControls.TabPages.Count - 1; i >= 0; i--)
                {
                    TTabPage page = (TTabPage)this.OwnerMDI.UCMM_TabControls.TabPages[i];
                    if (page.MenuNo.Equals(menuNo))
                    {
                        for (int j = this.OwnerMDI.UCMM_MDIBody.Controls.Count - 1; j >= 0; j--)
                        {
                            string findno = (this.OwnerMDI.UCMM_MDIBody.Controls[i] is FormBase) ?
                                ((FormBase)this.OwnerMDI.UCMM_MDIBody.Controls[i]).ProgramNo :
                                ((InternetViewer)this.OwnerMDI.UCMM_MDIBody.Controls[i]).ProgramNo;

                            Form form = (Form)this.OwnerMDI.UCMM_MDIBody.Controls[i];
                            if (findno.Equals(page.ProgramNo))
                            {
                                form.Close();
                                form = null;
                                this.OwnerMDI.UCMM_MDIBody.Controls.Remove(form);
                                break;
                            }
                        }

                        this.OwnerMDI.UCMM_TabControls.TabPages.RemoveAt(i);
                    }
                }
            }
            catch (Exception e)
            {
                LocalCapturer.ExceptionCatch(e);
            }
        }
        #endregion

        #region Description : 관세청 등록 사용자 
        public string Get_KCSAPI4LoginId()
        {
            return "taeyoungin";
        }
        #endregion

        #region Description : 관세청 문서 사용자
        public string Get_KCSAPI4DocUserId()
        {
            return "VC610811044901";
        }
        #endregion

        #region  Description : KCSAPI4 Xml 문서코드 반환 함수
        public string UP_Get_XmlToDocCode(string path)
        {
            string sDocCode = string.Empty;

            // Xml 작업을 하기 위한 Xml 문서 생성
            XmlDocument xmlDoc = new XmlDocument();

            // Xml 파일을 불러옵니다.
            xmlDoc.Load(path);

            XmlNodeList elemList = xmlDoc.GetElementsByTagName("wco:TypeCode");

            if (elemList.Count > 0)
            {
                sDocCode = elemList[0].InnerText;
            }

            return sDocCode;
        }
        #endregion

        #region  Description : 주민번호 조회 권한 체크
        protected void UP_Set_JuminAuthCheck(TYComboBox CboCheck)
        {
            if (TYUserInfo.PerAuth != "Y")
            {
                CboCheck.SetValue("N");
                CboCheck.Enabled = false;
            }
        }
        #endregion

    }
}