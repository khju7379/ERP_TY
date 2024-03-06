using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;


namespace TY.ER.HR00
{
    /// <summary>
    /// 급여대상자관리 팝업 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2014.12.18 17:03
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4CJDS888 : 급여대상자관리 삭제
    ///  TY_P_HR_4CJDT890 : 급여대상자관리 등록
    ///  TY_P_HR_4CJDT891 : 급여대상자관리 수정
    ///  TY_P_HR_4CJDX894 : 급여대상자관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_4CJDZ899 : 급여대상자관리 조회
    ///  TY_S_HR_4CJE3900 : 급여대상자 리스트 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  PTGUBN : 급여구분
    ///  PTJIDATE : 지급일자
    ///  PTYYMM : 급여년월
    /// </summary>
    public partial class TYHRPY06C1 : TYBase
    {
        private string fsPTGUBN = string.Empty;
        private string fsPTYYMM = string.Empty;
        private string fsPTJIDATE = string.Empty;

        #region  Description : 폼 로드 이벤트
        public TYHRPY06C1(string sPTGUBN, string sPTYYMM, string sPTJIDATE)
        {
            InitializeComponent();

            this.fsPTGUBN = sPTGUBN;
            this.fsPTYYMM = sPTYYMM;
            this.fsPTJIDATE = sPTJIDATE;

        }

        private void TYHRPY06C1_Load(object sender, System.EventArgs e)
        {
            ToolStripMenuItem reateRATE = new ToolStripMenuItem("지급율일괄등록");
            reateRATE.Click += new EventHandler(UpdateRate_ToolStripMenuItem_Click);

            this.FPS91_TY_S_HR_4CJDZ899.CurrentContextMenu.Items.AddRange(
                new System.Windows.Forms.ToolStripItem[] { new ToolStripSeparator(), reateRATE });


            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.BTN62_INQ.ProcessCheck += new TButton.CheckHandler(BTN62_INQ_ProcessCheck);

            this.CBH01_PTGUBN.SetValue(fsPTGUBN);
            this.DTP01_PTYYMM.SetValue(fsPTYYMM);
            this.DTP01_PTJIDATE.SetValue(fsPTJIDATE);

            this.BTN62_INQ.Text = ">>";

            this.BTN61_INQ_Click(null, null);
            this.BTN63_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 급여대상자 리스트
        private void UP_GetSABUNLIST()
        {           
            //급여대상자 리스트
            this.FPS91_TY_S_HR_4CJE3900.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4CJEM901", this.CBH01_PTGUBN.GetValue().ToString(), this.DTP01_PTYYMM.GetString().Substring(0, 6), this.DTP01_PTJIDATE.GetString(), this.CBH01_KBSABUN.GetValue() );
            this.FPS91_TY_S_HR_4CJE3900.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region  Description : 급여대상자 조회 함수
        private void UP_GetDataBinding()
        {                        

                this.FPS91_TY_S_HR_4CJDZ899.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4CJDX894", this.CBH01_PTGUBN.GetValue().ToString(), this.DTP01_PTYYMM.GetString().Substring(0, 6), this.DTP01_PTJIDATE.GetString());
                DataTable dt = this.DbConnector.ExecuteDataTable();
                this.FPS91_TY_S_HR_4CJDZ899.SetValue(dt);

                if (this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.RowCount <= 0)
                {
                    if (this.CBH01_PTGUBN.GetValue().ToString().Substring(0, 1) != "Y")
                    {
                        this.UP_GetLastMonthDataBinding(this.CBH01_PTGUBN.GetValue().ToString(), DTP01_PTJIDATE.GetString().ToString(), this.CBH01_PTGUBN.GetValue().ToString());                        
                    }
                    else if (this.CBH01_PTGUBN.GetValue().ToString().Substring(0, 1) == "Y")
                    {
                        this.UP_GetYunChaDataBinding(this.CBH01_PTGUBN.GetValue().ToString(), this.DTP01_PTYYMM.GetString().Substring(0, 6), this.DTP01_PTJIDATE.GetString());
                    }
                }                

                if (dt.Rows.Count > 0)
                {
                    //if (Convert.ToInt16(dt.Rows[0]["PAYCREATECNT"].ToString()) > 0)
                    //{
                    //    this.BTN61_REM.Visible = false;
                    //    this.BTN61_SAV.Visible = false;
                    //}
                }

                ////급여지급완료시 버튼 잠금
                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach("TY_P_HR_535G1512", this.CBH01_PTGUBN.GetValue().ToString(), this.DTP01_PTYYMM.GetString().Substring(0, 6), this.DTP01_PTJIDATE.GetString());
                //Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                //if (iCnt > 0)
                //{
                //    this.BTN61_REM.SetReadOnly(true);
                //    this.BTN61_SAV.SetReadOnly(true);
                //}                

                this.UP_PayListCount(Convert.ToInt16(dt.Rows.Count));

        }
        #endregion

        #region  Description : 전월 급여대상자 
        private void UP_GetLastMonthDataBinding(string sPYGUBN1, string sPYJIDATE, string sPYGUBN2)
        {
            int iRowIndex = 0;

            sPYGUBN1 = sPYGUBN1 == "S1" ? sPYGUBN1 : "M1";
            sPYGUBN2 = sPYGUBN2 == "S1" ? sPYGUBN2 : "M1";
            
            this.FPS91_TY_S_HR_4CJDZ899.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4CJFI902", sPYGUBN1, sPYJIDATE, sPYGUBN2);

            DataTable dt = this.DbConnector.ExecuteDataTable();

            //this.FPS91_TY_S_HR_4CJDZ899.SetValue(this.DbConnector.ExecuteDataTable());
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                iRowIndex = iRowIndex + 1;

                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.AddRows(iRowIndex - 1, 1);
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.RowHeader.Cells[iRowIndex - 1, 0].Text = "N";
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 0].Text = CBH01_PTGUBN.GetValue().ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 1].Text = DTP01_PTYYMM.GetString().Substring(0, 6);
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 2].Text = DTP01_PTJIDATE.GetString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 3].Text = dt.Rows[i]["PTSABUN"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 4].Text = dt.Rows[i]["PTSABUNNM"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 5].Text = dt.Rows[i]["PTSAUP"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 6].Text = dt.Rows[i]["PTSAUPNM"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 7].Text = dt.Rows[i]["PTBUSEO"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 8].Text = dt.Rows[i]["PTBUSEONM"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 9].Text = dt.Rows[i]["PTTEAM"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 10].Text = dt.Rows[i]["PTTEAMNM"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 11].Text = dt.Rows[i]["PTJJCD"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 12].Text = dt.Rows[i]["PTJJCDNM"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 13].Text = dt.Rows[i]["PTJKCD"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 14].Text = dt.Rows[i]["PTJKCDNM"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 15].Text = dt.Rows[i]["PTHOBN"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 16].Text = dt.Rows[i]["PTIDATE"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 17].Text = dt.Rows[i]["PTBALCD"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 18].Text = dt.Rows[i]["PTBALCDNM"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 19].Text = dt.Rows[i]["PTBDATE"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 20].Text = CBH01_PTGUBN.GetValue().ToString() != "S2" ? dt.Rows[i]["PTPAYRATE"].ToString() : "50";
            }
        }
        #endregion

        #region  Description : 당해 년차대상자
        private void UP_GetYunChaDataBinding(string sPYGUBN, string sPYYYMM, string sPYJIDATE)
        {
            int iRowIndex = 0;

            this.FPS91_TY_S_HR_4CJDZ899.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_5CUIW345", sPYGUBN, sPYYYMM, sPYJIDATE);

            DataTable dt = this.DbConnector.ExecuteDataTable();

            //this.FPS91_TY_S_HR_4CJDZ899.SetValue(this.DbConnector.ExecuteDataTable());

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                iRowIndex = iRowIndex + 1;

                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.AddRows(iRowIndex - 1, 1);
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.RowHeader.Cells[iRowIndex - 1, 0].Text = "N";
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 0].Text = CBH01_PTGUBN.GetValue().ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 1].Text = DTP01_PTYYMM.GetString().Substring(0, 6);
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 2].Text = DTP01_PTJIDATE.GetString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 3].Text = dt.Rows[i]["PTSABUN"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 4].Text = dt.Rows[i]["PTSABUNNM"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 5].Text = dt.Rows[i]["PTSAUP"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 6].Text = dt.Rows[i]["PTSAUPNM"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 7].Text = dt.Rows[i]["PTBUSEO"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 8].Text = dt.Rows[i]["PTBUSEONM"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 9].Text = dt.Rows[i]["PTTEAM"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 10].Text = dt.Rows[i]["PTTEAMNM"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 11].Text = dt.Rows[i]["PTJJCD"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 12].Text = dt.Rows[i]["PTJJCDNM"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 13].Text = dt.Rows[i]["PTJKCD"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 14].Text = dt.Rows[i]["PTJKCDNM"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 15].Text = dt.Rows[i]["PTHOBN"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 16].Text = dt.Rows[i]["PTIDATE"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 17].Text = dt.Rows[i]["PTBALCD"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 18].Text = dt.Rows[i]["PTBALCDNM"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 19].Text = dt.Rows[i]["PTBDATE"].ToString();
                this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 20].Text = dt.Rows[i]["PTPAYRATE"].ToString();
            }
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            //this.UP_GetSABUNLIST();

            this.UP_GetDataBinding();
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4CJDS888", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD874");
        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_4CJDZ899.GetDataSourceInclude(TSpread.TActionType.Remove, "PTGUBN", "PTYYMM", "PTJIDATE", "PTSABUN");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }          

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;

        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            this.DataTableColumnAdd(ds.Tables[0], "PTHISAB", TYUserInfo.EmpNo);
            this.DataTableColumnAdd(ds.Tables[1], "PTHISAB", TYUserInfo.EmpNo);

            this.DbConnector.Attach("TY_P_HR_4CJDT890", ds.Tables[0]);
            this.DbConnector.Attach("TY_P_HR_4CJDT891", ds.Tables[1]);
            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);
            
            this.ShowMessage("TY_M_GB_23NAD873");
        }
        
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_HR_4CJDZ899.GetDataSourceInclude(TSpread.TActionType.New, "PTGUBN", "PTYYMM", "PTJIDATE", "PTSABUN", "PTSAUP", "PTBUSEO", "PTTEAM", "PTJJCD", "PTJKCD", "PTHOBN", "PTIDATE", "PTBALCD", "PTBDATE", "PTPAYRATE"));
            ds.Tables.Add(this.FPS91_TY_S_HR_4CJDZ899.GetDataSourceInclude(TSpread.TActionType.Update, "PTGUBN", "PTYYMM", "PTJIDATE", "PTSABUN", "PTSAUP", "PTBUSEO", "PTTEAM", "PTJJCD", "PTJKCD", "PTHOBN", "PTIDATE", "PTBALCD", "PTBDATE", "PTPAYRATE"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
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
        #endregion

        #region  Description : 선택 버튼 이벤트
        private void BTN62_INQ_Click(object sender, EventArgs e)
        {
            int iRowIndex = 0;
            
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            if (dt.Rows.Count > 0)
            {
                iRowIndex = this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Rows.Count;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    iRowIndex = iRowIndex + 1;

                    this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.AddRows(iRowIndex -1, 1);
                    this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.RowHeader.Cells[iRowIndex-1, 0].Text = "N";

                    this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex-1, 0].Text = this.CBH01_PTGUBN.GetValue().ToString();
                    this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex-1, 1].Text = this.DTP01_PTYYMM.GetString().Substring(0, 6);
                    this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex-1, 2].Text = this.DTP01_PTJIDATE.GetString();
                    this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex-1, 3].Text = dt.Rows[i]["KBSABUN"].ToString();
                    this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex-1, 4].Text = dt.Rows[i]["KBHANGL"].ToString();
                    this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex-1, 5].Text = dt.Rows[i]["KBSOSOK"].ToString();
                    this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex-1, 6].Text = dt.Rows[i]["KBSOSOKNM"].ToString();
                    this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex-1, 7].Text = dt.Rows[i]["KBBUSEO"].ToString();
                    this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex-1, 8].Text = dt.Rows[i]["KBBUSEONM"].ToString();
                    this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex-1, 9].Text = dt.Rows[i]["KBBSTEAM"].ToString();
                    this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex-1, 10].Text = dt.Rows[i]["KBBSTEAMNM"].ToString();
                    this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex-1, 11].Text = dt.Rows[i]["KBJJCD"].ToString();
                    this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex-1, 12].Text = dt.Rows[i]["KBJJCDNM"].ToString();
                    this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex-1, 13].Text = dt.Rows[i]["KBJKCD"].ToString();
                    this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex-1, 14].Text = dt.Rows[i]["KBJKCDNM"].ToString();
                    this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex-1, 15].Text = dt.Rows[i]["KBHOBN"].ToString();
                    this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex-1, 16].Text = dt.Rows[i]["KBIDATE"].ToString();
                    this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex-1, 17].Text = dt.Rows[i]["KBBALCD"].ToString();
                    this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex-1, 18].Text = dt.Rows[i]["KBBALCDNM"].ToString();
                    this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex-1, 19].Text = dt.Rows[i]["KBBDATE"].ToString();
                    if(CBH01_PTGUBN.GetValue().ToString() != "S1" )
                    {
                        this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 20].Text = dt.Rows[i]["PAYRATE"].ToString();
                    }
                    else
                    {
                        this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 20].Text = "50";
                    }
                    
                }

                this.UP_PayListCount(Convert.ToInt16(this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Rows.Count));
            }

            this.UP_GetSABUNLIST();
        }

        private void BTN62_INQ_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_4CJE3900.GetDataSourceInclude(TSpread.TActionType.Select, "KBSABUN", "KBHANGL", "KBJJCD", "KBJJCDNM", "KBJKCD", "KBJKCDNM", "KBSOSOK", "KBSOSOKNM", "KBBUSEO", "KBBUSEONM", "KBBSTEAM", "KBBSTEAMNM", "KBHOBN", "KBIDATE", "KBBALCD", "KBBALCDNM", "KBBDATE","PAYRATE");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_25F59464");
                e.Successed = false;
                return;
            }

            //급여대상자에 있는지 체크
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Rows.Count; j++)
                {
                    if (dt.Rows[i]["KBSABUN"].ToString() == this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[j, 3].Text.Trim())
                    {
                        this.ShowCustomMessage("급여대상자에 등록되어 있습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }            

            if (!this.ShowMessage("TY_M_HR_4CJGE904"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;

        }
        #endregion

        #region  Description : 대상자 표시
        private void UP_PayListCount(Int16 iCnt)
        {
            TXT01_ESCEMPCNT.SetValue(iCnt.ToString());
        }
        #endregion

        #region  Description : 지급율 등록 팝업 이벤트
        private void UpdateRate_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TYHRPY06C4 popup = new TYHRPY06C4();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Int16 iPayRate =  Convert.ToInt16(Get_Numeric(popup.fiPayRate.ToString()));

                if (iPayRate > 0)
                {
                    int iRowIndex = 0;

                    for (int i = 0; i < this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Rows.Count; i++)
                    {
                        iRowIndex = iRowIndex + 1;

                        if (this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.RowHeader.Cells[iRowIndex-1, 0].Text != "N")
                        {
                            this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.RowHeader.Cells[iRowIndex - 1, 0].Text = "U";
                        }                       

                        this.FPS91_TY_S_HR_4CJDZ899.ActiveSheet.Cells[iRowIndex - 1, 20].Text = iPayRate.ToString();
                    }
                }
            }

        }
        #endregion

        #region  Description : 급여대상자 조회 버튼 이벤트
        private void BTN63_INQ_Click(object sender, EventArgs e)
        {
            UP_GetSABUNLIST();
        }
        #endregion

    }
}
