using System;
using System.Data;
using FarPoint.Win.Spread;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using TY.Service.Library.Controls.TYSpreadCellType;
using System.IO;
using System.Windows.Forms;


namespace TY.ER.GB99
{
    /// <summary>
    /// 스프레드 테스트 프로그램입니다.
    /// 
    /// 작성자 : 김영우
    /// 작성일 : 2012.03.30 13:03
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_GB_23U16190 : 스프레트 테스트
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    /// </summary>
    public partial class TYERGB998S : TYBase
    {
        TYCodeBox CBH21_CBHTEST_YWKIM1;
        TYTextButtonBox TXT21_TXTTEST;
        TYTextButtonBox TXT22_TXTTEST;

        private TYData DAT30_AFSABUN;
        private TYData DAT30_AFSEQ;
        private TYData DAT30_AFFILEGUBN;
        private TYData DAT30_AFDESC;
        private TYData DAT30_AFFILENAME;
        private TYData DAT30_AFFILESIZE;
        private TYData DAT30_AFFILEBYTE;
        private TYData DAT30_AFHISAB;

        private byte[] _fbAttachFile;


        private DataTable _dt;

        public TYERGB998S()
        {
            InitializeComponent();

            //세션 이벤트 테스트
            this.Session.SessionValueChanged += new TYSession.SessionValueChangedEventHandler(Session_SessionValueChanged);

            //스프레드 코드박스 테스트
            this.SetSpreadCodeHelper(this.FPS91_TY_S_GB_23U16190, "F", "G", "P1CDDP");
            TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_GB_23U16190, "F");
            if (tyCodeBox != null)
                tyCodeBox.DummyValue = "20120101";

            //TYItemBox 테스트
            this.CBH21_CBHTEST_YWKIM1 = new TYCodeBox();
            this.CBH21_CBHTEST_YWKIM1.Name = "CBH11_CBHTEST_YWKIM1";
            this.PAN11_TEST.AddControl("01", "테스트1", this.CBH21_CBHTEST_YWKIM1);

            //TYTextButtonBox 테스트
            this.TXT21_TXTTEST = new TYTextButtonBox();
            this.TXT21_TXTTEST.Name = "TXT21_TXTTEST";
            this.TXT21_TXTTEST.Button.Click += new EventHandler(Button_Click);
            this.TXT21_TXTTEST.TextChanged += new EventHandler(TXT21_TXTTEST_TextChanged);
            this.TXT21_TXTTEST.TextBoxVisible = false;
            this.PAN11_TEST.AddControl("02", "테스트2", this.TXT21_TXTTEST);
            this.TXT22_TXTTEST = new TYTextButtonBox();
            this.TXT22_TXTTEST.Name = "TXT22_TXTTEST";
            this.TXT22_TXTTEST.TextBoxVisible = true;
            this.PAN11_TEST.AddControl("03", "테스트3", this.TXT22_TXTTEST);

            //텍스트박스 ReadOnly 테스트
            this.TXT13_TXTTEST.SetReadOnly(true);
            this.TXT13_TXTTEST.SetValue("생성자");

            this.DAT30_AFSABUN = new TYData("DAT30_AFSABUN", null);
            this.DAT30_AFSEQ = new TYData("DAT30_AFSEQ", null);
            this.DAT30_AFFILEGUBN = new TYData("DAT30_AFFILEGUBN", null);
            this.DAT30_AFDESC = new TYData("DAT30_AFDESC", null);
            this.DAT30_AFFILENAME = new TYData("DAT30_AFFILENAME", null);
            this.DAT30_AFFILESIZE = new TYData("DAT30_AFFILESIZE", null);
            this.DAT30_AFFILEBYTE = new TYData("DAT30_AFFILEBYTE", null);
            this.DAT30_AFHISAB = new TYData("DAT30_AFHISAB", null);

            
        }

        /// <summary>
        /// TYTextButtonBox 이벤트 테스트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TXT21_TXTTEST_TextChanged(object sender, EventArgs e)
        {
            this.ShowCustomMessage("TXT21_TXTTEST_TextChanged", "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }

        /// <summary>
        /// TYTextButtonBox 이벤트 테스트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, EventArgs e)
        {
            this.ShowCustomMessage("TXT21_TXTTEST의 Button_Click", "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }

        private void TYERGB998S_Load(object sender, System.EventArgs e)
        {
            this.ControlFactory.Add(this.DAT30_AFSABUN);
            this.ControlFactory.Add(this.DAT30_AFSEQ);
            this.ControlFactory.Add(this.DAT30_AFFILEGUBN);
            this.ControlFactory.Add(this.DAT30_AFDESC);
            this.ControlFactory.Add(this.DAT30_AFFILENAME);
            this.ControlFactory.Add(this.DAT30_AFFILESIZE);
            this.ControlFactory.Add(this.DAT30_AFFILEBYTE);
            this.ControlFactory.Add(this.DAT30_AFHISAB);

            //거래처 팝업
            this.CBH01_CBHTEST_VNCODE.SetIPopupHelper(new TYERGB011P());

            //콤보박스 상위 하위 테스트
            this.CBH01_CBHTEST_YWKIM1.CodeBoxDataBinded += new TCodeBox.TCodeBoxEventHandler(CBH01_CBHTEST_YWKIM1_CodeBoxDataBinded);
            this.CBH01_CBHTEST_YWKIM2.SetReadOnly(true);

            //스프레드 키컬럼 신규때만 Locked false 테스트
            this.SetSpreadKeyColumn(this.FPS91_TY_S_GB_23U16190, "A", "B");

            //편집모드 이벤트
            this.FPS91_TY_S_GB_23U16190.EnterCell += new FarPoint.Win.Spread.EnterCellEventHandler(FPS91_TY_S_GB_23U16190_EnterCell);

            //스프레드 고정 폭 컬럼 테스트
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_GB_23U16190, "A", "B", "C", "D", "E");

            //스프레드 버튼컬럼 클릭 이벤트 테스트
            this.FPS91_TY_S_GB_23U16190.ButtonClicked += new EditorNotifyEventHandler(FPS91_TY_S_GB_23U16190_ButtonClicked);

            //TYTextButtonBox 테스트
            this.TXT21_TXTTEST.Enabled = true;
            this.TXT22_TXTTEST.Enabled = true;

            //텍스트박스 ReadOnly 테스트
            this.TXT14_TXTTEST.SetReadOnly(true);
            this.TXT14_TXTTEST.SetValue("Load");

            //스프레드 바인딩
            _dt = new DataTable();
            _dt.Columns.Add("A");
            _dt.Columns.Add("B");
            _dt.Columns.Add("C");
            _dt.Columns.Add("D");
            for (int i = 0; i < 100; i++)
                _dt.Rows.Add("A" + i, "B" + i, "C" + i, (i % 2 == 0 ? "Y" : "N"));

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.BTN61_INQ_Click(null, null);
        }

        /// <summary>
        /// 스프레드 버튼셀 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FPS91_TY_S_GB_23U16190_ButtonClicked(object sender, EditorNotifyEventArgs e)
        {
            this.TXT01_TXTTEST.Text = string.Format("{0}-{1}", e.Column, e.Row);
        }

        /// <summary>
        /// 스프레드 편집모드 이벤트 테스트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FPS91_TY_S_GB_23U16190_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            //if (e.Column != 3)
            //    return;

            //string dummyValue = this.FPS91_TY_S_GB_23U16190_Sheet1.Cells[e.Row, this.FPS91_TY_S_GB_23U16190_Sheet1.Columns["A"].Index].Value.ToString();
            //((TCodeBoxCellType)this.FPS91_TY_S_GB_23U16190_Sheet1.Columns["D"].CellType).DummyValue = "20120101";
        }

        /// <summary>
        /// 세션 변경 이벤트 테스트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Session_SessionValueChanged(object sender, TYSession.SessionValueChangedEventArgs e)
        {
            //세션 값 변경 이벤트
            if (!this.IsDisposed && e.EventForm.ProgramNo == "TYERGB999S" && e.Key == "key")
            {
                object value = e.Value;
                value = this.Session.GetValue(e.Key);
            }
        }

        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_GB_23U16190.SetValue(this._dt.Copy());
        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            DataTable tmpDT1 = this.FPS91_TY_S_GB_23U16190.GetDataSourceInclude(TSpread.TActionType.New, "A", "B", "C");
            DataTable tmpDT2 = this.FPS91_TY_S_GB_23U16190.GetDataSourceInclude(TSpread.TActionType.Update, "A", "B", "C");
            ds.Tables.Add(tmpDT1);
            ds.Tables.Add(tmpDT2);

            if (tmpDT1.Rows.Count == 0 && tmpDT2.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }

        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                this._dt.Rows.Add(dr.ItemArray);
            }
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                foreach (DataRow seletedRow in this._dt.Select(string.Format("A = '{0}'", dr["A"])))
                {
                    seletedRow["A"] = dr["A"];
                    seletedRow["B"] = dr["B"];
                    seletedRow["C"] = dr["C"];
                }
            }

            this.BTN61_INQ_Click(null, null);
        }

        /// <summary>
        /// 대분류/소분류 개념 코드박스 사용 시 대분류의 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CBH01_CBHTEST_YWKIM1_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH01_CBHTEST_YWKIM1.GetValue().ToString();
            this.CBH01_CBHTEST_YWKIM2.DummyValue = groupCode;
            this.CBH01_CBHTEST_YWKIM2.SetReadOnly(string.IsNullOrEmpty(groupCode));
            this.CBH01_CBHTEST_YWKIM2.Initialize();
        }

        private void FPS91_TY_S_GB_23U16190_CellClick(object sender, CellClickEventArgs e)
        {
            //CellClick 이벤트 뒤에 Selection이 Change되므로 CellClick 이벤트에서 Selection으로 값을 가져오면 안됨
            string eRowData = this.FPS91_TY_S_GB_23U16190.GetValue(e.Row, "A").ToString();
            string sRowData = this.FPS91_TY_S_GB_23U16190.GetValue("A").ToString();

            if (eRowData != sRowData) //값이 다름
            {

            }
        }

        /// <summary>
        /// 엑셀 업로드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTN62_INQ_Click(object sender, EventArgs e)
        {
            ExcelImportType usingImportType;
            DataSet ds = this.OpenExcelDialogAndGetData(out usingImportType);

            if (ds == null)
                return;

            switch (usingImportType)
            {
                case ExcelImportType.Automation:
                    break;
                case ExcelImportType.OleDB:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 탭 패널 동적 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTN64_INQ_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.TabPages.Contains(this.tabPage2))
                this.tabControl1.TabPages.Remove(this.tabPage2);
            else
                this.tabControl1.TabPages.Add(this.tabPage2);
            
            if (this.tabControl1.TabPages.Contains(this.tabPage3))
                this.tabControl1.TabPages.Remove(this.tabPage3);
            else
                this.tabControl1.TabPages.Add(this.tabPage3);
        }

        /// <summary>
        /// 셀의 CellType 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTN65_INQ_Click(object sender, EventArgs e)
        {
            if (this.FPS91_TY_S_GB_23U16190.ActiveSheet.Cells[0, this.FPS91_TY_S_GB_23U16190.ActiveSheet.Columns["E"].Index].CellType == null ||    //컬럼에만 CellType이 지정된 경우 초기 값은 null
                this.FPS91_TY_S_GB_23U16190.ActiveSheet.Cells[0, this.FPS91_TY_S_GB_23U16190.ActiveSheet.Columns["E"].Index].CellType is TButtonCellType
                )
            {
                TYTextCellType tyTextCellType = new TYTextCellType();
                this.FPS91_TY_S_GB_23U16190.ActiveSheet.Cells[0, this.FPS91_TY_S_GB_23U16190.ActiveSheet.Columns["E"].Index].CellType = tyTextCellType;
                this.FPS91_TY_S_GB_23U16190.ActiveSheet.Cells[0, this.FPS91_TY_S_GB_23U16190.ActiveSheet.Columns["E"].Index].Locked = true;
            }
            else
            {
                TButtonCellType tButtonCellType = new TButtonCellType();
                tButtonCellType.Text = "테스트";
                tButtonCellType.TextAlign = FarPoint.Win.ButtonTextAlign.TextTopPictBottom;
                tButtonCellType.TextOrientation = FarPoint.Win.TextOrientation.TextHorizontal;
                this.FPS91_TY_S_GB_23U16190.ActiveSheet.Cells[0, this.FPS91_TY_S_GB_23U16190.ActiveSheet.Columns["E"].Index].CellType = tButtonCellType;
                this.FPS91_TY_S_GB_23U16190.ActiveSheet.Cells[0, this.FPS91_TY_S_GB_23U16190.ActiveSheet.Columns["E"].Index].Locked = false;
            }
        }

        /// <summary>
        /// TextBox Enable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTN62_SAV_Click(object sender, EventArgs e)
        {
            this.TXT01_TXTTEST.Enabled = !this.TXT01_TXTTEST.Enabled;
        }

        /// <summary>
        /// tabpage2 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTN71_INQ_Click(object sender, EventArgs e)
        {
            //콤보 초기화
            this.CBO11_AO1OTMI1.Initialize();

            //TYTextButtonBox 테스트
            this.TXT11_TXTTEST.Enabled = !this.TXT11_TXTTEST.Enabled;

            //TYItemPanel - TYTextButtonBox 연동 테스트
            if (this.PAN11_TEST.GetCurCode() == "01")
                this.PAN11_TEST.SetCurCode("02");
            else if (this.PAN11_TEST.GetCurCode() == "02")
                this.PAN11_TEST.SetCurCode("03");
            else
                this.PAN11_TEST.SetCurCode("01");
        }

        private void BTN72_INQ_Click(object sender, EventArgs e)
        {
            //TYTextButtonBox TextBoxVisible 테스트
            this.TXT22_TXTTEST.TextBoxVisible = !this.TXT22_TXTTEST.TextBoxVisible;
        }

        private void BTN73_INQ_Click(object sender, EventArgs e)
        {
            this.TXT13_TXTTEST.SetReadOnly(!this.TXT13_TXTTEST.IsReadOnly);
            this.TXT14_TXTTEST.SetReadOnly(!this.TXT14_TXTTEST.IsReadOnly);
        }

        /// <summary>
        /// 텍스트박스 ESC 키 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TXT13_TXTTEST_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Escape)
            {
                this.TXT13_TXTTEST.Initialize();
            }
        }

        /// <summary>
        /// 콤보박스 ESC 키 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CBO11_AO1OTMI1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Escape)
            {
                this.CBO11_AO1OTMI1.Initialize();
            }
        }

        private void tyButton1_Click(object sender, EventArgs e)
        {
            byte[] _AttachFile = null;

            object _objAttachFile = null;

            string sSABUN = "";

            for (int i = 269; i < 500; i++)
            {
                sSABUN = Set_Fill4(i.ToString())+"-M";

                string filePath = "C:\\Users\\Administrator\\Downloads\\직원증명사진\\EisEmpImg\\" + sSABUN + ".jpg";

                bool bcheck = File.Exists(filePath);

                if (bcheck == true)
                {
                    _AttachFile = UP_Get_Byte(filePath);

                    _objAttachFile = _AttachFile;

                    int ArraySize = _AttachFile.GetUpperBound(0);

                    this.UP_Save_Seq(sSABUN);

                    string sAFFILESIZE = ArraySize.ToString();
                    string sAFFILENAME = UP_Set_FileName(filePath);

                    this.DAT30_AFSABUN.SetValue(sSABUN);
                    this.DAT30_AFSEQ.SetValue(this.TXT01_AFSEQ.GetValue());
                    this.DAT30_AFFILEGUBN.SetValue("1");
                    this.DAT30_AFDESC.SetValue("증명사진");
                    this.DAT30_AFFILENAME.SetValue(sAFFILENAME);
                    this.DAT30_AFFILESIZE.SetValue(sAFFILESIZE);
                    this.DAT30_AFFILEBYTE.SetValue(_objAttachFile);
                    this.DAT30_AFHISAB.SetValue(TYUserInfo.EmpNo);

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_4BHFB430", this.ControlFactory, "30");
                    this.DbConnector.ExecuteTranQuery();
                }

                
            }

            this.ShowMessage("TY_M_GB_23NAD873");

        }

        #region Description : UP_Save_Seq(순번생성) 이벤트
        private void UP_Save_Seq(string SABUN)
        {
            // 순번 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_HR_4BHGE435",
                SABUN,
                "2"
                );

            Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

            this.TXT01_AFSEQ.SetValue(Set_Fill3(iCnt.ToString()));

        }
        #endregion

        #region Descrioption : 파일 이름 가져오기
        protected string UP_Set_FileName(string sStr)
        {
            string sValue = "";
            int i = 0;
            int iPoint = 0;
            for (i = 0; i < sStr.Length; i++)
            {
                if (sStr.Substring(i, 1) == "\\")
                {
                    iPoint = i;
                }
            }

            for (i = iPoint + 1; i < sStr.Length; i++)
            {
                sValue = sValue + sStr.Substring(i, 1);
            }

            return sValue;
        }
        #endregion

        #region Description : 첨부파일 byte 변환
        public static byte[] UP_Get_Byte(string filePath)
        {

                FileInfo file = new FileInfo(filePath);
                //FileStream stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                FileStream stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 2000);

                byte[] rawAssembly = new byte[(int)stream.Length];
                stream.Read(rawAssembly, 0, rawAssembly.Length);
                return rawAssembly; // <= byte[] 임
            
        }
        #endregion
    }
}
