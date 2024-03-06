using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.ED00
{
    /// <summary>
    /// 반출입 정정신고관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.03.31 10:18
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_73VB3170 : 반출입 정정신고관리 등록
    ///  TY_P_UT_73VBG171 : 반출입 정정신고관리 수정
    ///  TY_P_UT_73VBH172 : 반출입 정정신고관리 확인
    ///  TY_P_UT_73VBJ173 : 반출입 정정신고관리내역 확인
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_73VBJ174 : 반출입 정정신고관리내역 확인
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  REM : 삭제
    ///  SAV : 저장
    ///  EDIGJ : 공장
    ///  EDIRECHIP : 반입반출구분
    ///  EDIREBLHSN : HSN
    ///  EDIREBLMSN : MSN
    ///  EDIRECHASU : 정정차수
    ///  EDIREDATE : 신청일자
    ///  EDIREFTX : 정정사유
    ///  EDIREGIS : 신청구분
    ///  EDIREJSGB : 전송구분
    ///  EDIREJUKHA : 적하목록
    ///  EDIREKWA : 과
    ///  EDIRELOC : 세과
    ///  EDIREMSG : MSG
    ///  EDIRENAD : 신청자성명
    ///  EDIRENO1 : 제출번호
    ///  EDIRENO2 : MSN
    ///  EDIRENO3 : HSN
    ///  EDIRERCVGB : 접수구분
    /// </summary>
    public partial class TYEDKB010I : TYBase
    {
        private string fsEDIGJ;
        private string fsEDIRENO1;
        private string fsEDIRENO2;
        private string fsEDIRENO3;
        private string fsEDIRECHASU;
        private string fsEDIRECHIP;
        private string fsGubn;
             

        #region  Description : 폼 로드 이벤트
        public TYEDKB010I(string sEDIGJ, string sEDIRENO1, string sEDIRENO2, string sEDIRENO3, string sEDIRECHASU, string sEDIRECHIP, string sGubn)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsEDIGJ = sEDIGJ;
            fsEDIRENO1 = sEDIRENO1;
            fsEDIRENO2 = sEDIRENO2;
            fsEDIRENO3 = sEDIRENO3;
            fsEDIRECHASU = sEDIRECHASU;
            fsEDIRECHIP = sEDIRECHIP;
            fsGubn = sGubn;

            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_73VBJ174, "EDISRELIN", "EDISRELINNM", "EDISRELIN");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_7459E206, "EDISRELINBI", "EDISRELINNM", "EDISRELINBI");  

        }

        private void TYEDKB010I_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_73VBJ174, "EDISGJ");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_73VBJ174, "EDISRENO1");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_73VBJ174, "EDISRENO2");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_73VBJ174, "EDISRENO3");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_73VBJ174, "EDISRECHASU");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_73VBJ174, "EDISSEQ");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_73VBJ174, "EDISRELIN");

            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_UT_73VBJ174, "EDISSEQ");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_UT_73VBJ174, "EDISRELIN");

            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_7459E206, "EDISGJ");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_7459E206, "EDISRENO1");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_7459E206, "EDISRENO2");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_7459E206, "EDISRENO3");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_7459E206, "EDISRECHASU");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_7459E206, "EDISSEQ");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_7459E206, "EDISRELINBI");

            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_UT_7459E206, "EDISSEQ");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_UT_7459E206, "EDISRELINBI");


            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_EDIREDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            UP_SetLockCheck();

            this.CBO01_EDIGJ.SetValue(fsEDIGJ);
            this.TXT01_EDIRENO1.SetValue(fsEDIRENO1);
            this.TXT01_EDIRENO2.SetValue(fsEDIRENO2);
            this.TXT01_EDIRENO3.SetValue(fsEDIRENO3);
            this.TXT01_EDIRECHASU.SetValue(fsEDIRECHASU);
            this.CBO01_EDIRECHIP.SetValue(fsEDIRECHIP);

            UP_DataBinding_Load();
        }
        #endregion

        #region  Description : UP_DataBinding 이벤트
        private void UP_DataBinding_New()
        {
            if (fsEDIRECHIP == "5LD")
            {
                //반출
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_744BS200", this.CBO01_EDIGJ.GetValue().ToString(),
                                                            this.TXT01_EDIRENO1.GetValue(),
                                                            this.TXT01_EDIRENO2.GetValue(),
                                                            this.TXT01_EDIRENO3.GetValue()                                                            
                                                            );
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    TXT01_EDIREJUKHA.SetValue(dt.Rows[0]["EDIJUKHA"].ToString());
                    TXT01_EDIREBLMSN.SetValue(dt.Rows[0]["EDIBLMSN"].ToString());
                    TXT01_EDIREBLHSN.SetValue(dt.Rows[0]["EDIBLHSN"].ToString());
                }
            }
            else
            {
                //반입
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_744BW202", this.CBO01_EDIGJ.GetValue().ToString(),
                                                            this.TXT01_EDIRENO1.GetValue(),
                                                            this.TXT01_EDIRENO2.GetValue(),
                                                            this.TXT01_EDIRENO3.GetValue()
                                                            );
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    TXT01_EDIREJUKHA.SetValue(dt.Rows[0]["EDIJUKHA"].ToString());
                    TXT01_EDIREBLMSN.SetValue(dt.Rows[0]["EDIBLMSN"].ToString());
                    TXT01_EDIREBLHSN.SetValue(dt.Rows[0]["EDIBLHSN"].ToString());
                }
            }

            //기본정보 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_744BV201", this.TXT01_EDIRENO1.GetValue());
            DataTable dst = this.DbConnector.ExecuteDataTable();
            if (dst.Rows.Count > 0)
            {
                TXT01_EDIRELOC.SetValue(dst.Rows[0]["EDNCUSTLOC"].ToString());
                TXT01_EDIREKWA.SetValue(dst.Rows[0]["EDNCUSTKWA"].ToString());
                TXT01_EDIRENAD.SetValue(dst.Rows[0]["EDNCEONAME"].ToString());
            }

        }
        #endregion

        #region  Description : UP_DataBinding_Edit 이벤트
        private void UP_DataBinding_Edit()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_73VBH172", this.CBO01_EDIGJ.GetValue().ToString(), 
                                                        this.TXT01_EDIRENO1.GetValue(),
                                                        this.TXT01_EDIRENO2.GetValue(), 
                                                        this.TXT01_EDIRENO3.GetValue(), 
                                                        this.TXT01_EDIRECHASU.GetValue() 
                                                        );
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");

                if (dt.Rows[0]["EDIRERCVGB"].ToString() == "Y")
                {
                    this.BTN61_SAV.Visible = false;
                    this.BTN61_REM.Visible = false;
                }
            }         
        }
        #endregion

        #region  Description : UP_DataBinding_Detail 이벤트
        private void UP_DataBinding_Detail()
        {
            TYSpread TySpy = new TYSpread();

            TySpy = this.CBO01_EDIRECHIP.GetValue().ToString() == "5LC" ? this.FPS91_TY_S_UT_7459E206 : this.FPS91_TY_S_UT_73VBJ174;

            if (this.CBO01_EDIRECHIP.GetValue().ToString() == "5LC")
            {
                this.FPS91_TY_S_UT_73VBJ174.Visible = false;
            }
            else
            {
                this.FPS91_TY_S_UT_7459E206.Visible = false;
            }
                      
            TySpy.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_73VBJ173", this.CBO01_EDIGJ.GetValue().ToString(),
                                                        this.TXT01_EDIRENO1.GetValue(),
                                                        this.TXT01_EDIRENO2.GetValue(),
                                                        this.TXT01_EDIRENO3.GetValue(),
                                                        this.TXT01_EDIRECHASU.GetValue()
                                                        );
            TySpy.SetValue(this.DbConnector.ExecuteDataTable());            
        }
        #endregion

        #region  Description : UP_AutoSeq 이벤트
        private void UP_AutoSeq()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_744D7203", this.CBO01_EDIGJ.GetValue().ToString(),
                                                        this.TXT01_EDIRENO1.GetValue(),
                                                        this.TXT01_EDIRENO2.GetValue(),
                                                        this.TXT01_EDIRENO3.GetValue()                                                        
                                                        );
            Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

            this.TXT01_EDIRECHASU.SetValue(Set_Fill2(iCnt.ToString()));

        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_73VF0176", dt);
            this.DbConnector.ExecuteTranQueryList();

            fsGubn = "EDIT";

            UP_DataBinding_Load();

            this.ShowMessage("TY_M_GB_23NAD874");
        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            if (this.CBO01_EDIRECHIP.GetValue().ToString() == "5LC")
            {
                dt = this.FPS91_TY_S_UT_7459E206.GetDataSourceInclude(TSpread.TActionType.Remove, "EDISGJ", "EDISRENO1", "EDISRENO2", "EDISRENO3", "EDISRECHASU", "EDISSEQ");
            }
            else
            {
                dt = this.FPS91_TY_S_UT_73VBJ174.GetDataSourceInclude(TSpread.TActionType.Remove, "EDISGJ", "EDISRENO1", "EDISRENO2", "EDISRENO3", "EDISRECHASU", "EDISSEQ");
            }

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (fsGubn == "NEW")
            {
                this.ShowCustomMessage("저장후 삭제 할수 있습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
            if (fsGubn == "NEW")
            {
                this.DbConnector.Attach("TY_P_UT_73VB3170", this.CBO01_EDIGJ.GetValue().ToString(),
                                                            this.TXT01_EDIRENO1.GetValue(),
                                                            this.TXT01_EDIRENO2.GetValue(),
                                                            this.TXT01_EDIRENO3.GetValue(),
                                                            this.TXT01_EDIRECHASU.GetValue(),
                                                            CBO01_EDIRECHIP.GetValue(),
                                                            DTP01_EDIREDATE.GetString().ToString(),
                                                            this.TXT01_EDIRELOC.GetValue(),
                                                            this.TXT01_EDIREKWA.GetValue(),
                                                            this.TXT01_EDIRENAD.GetValue(),
                                                            this.TXT01_EDIREJUKHA.GetValue(),
                                                            this.TXT01_EDIREBLMSN.GetValue(),
                                                            this.TXT01_EDIREBLHSN.GetValue(),
                                                            this.CBO01_EDIREGIS.GetValue(),
                                                            this.TXT01_EDIREFTX.GetValue(),
                                                             "9",
                                                             "",
                                                             "",
                                                             "",
                                                             "",
                                                             TYUserInfo.EmpNo,
                                                             TYUserInfo.EmpNo
                                                           );
            }
            else
            {
                this.DbConnector.Attach("TY_P_UT_73VBG171",  this.CBO01_EDIREGIS.GetValue(),
                                                             this.TXT01_EDIREFTX.GetValue(),
                                                             TYUserInfo.EmpNo,
                                                             TYUserInfo.EmpNo,
                                                             this.CBO01_EDIGJ.GetValue().ToString(),
                                                             this.TXT01_EDIRENO1.GetValue(),
                                                             this.TXT01_EDIRENO2.GetValue(),
                                                             this.TXT01_EDIRENO3.GetValue(),
                                                             this.TXT01_EDIRECHASU.GetValue()
                                                            );
            }            
            
            //항목정정일 경우만 내역을 처리한다.
            if (this.CBO01_EDIREGIS.GetValue().ToString() == "01")
            {
               
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {

                            this.DbConnector.Attach("TY_P_UT_73VH7188", ds.Tables[0].Rows[i]["EDISGJ"].ToString(),
                                                                         ds.Tables[0].Rows[i]["EDISRENO1"].ToString(),
                                                                         ds.Tables[0].Rows[i]["EDISRENO2"].ToString(),
                                                                         ds.Tables[0].Rows[i]["EDISRENO3"].ToString(),
                                                                         ds.Tables[0].Rows[i]["EDISRECHASU"].ToString(),
                                                                         ds.Tables[0].Rows[i]["EDISSEQ"].ToString(),                                                                         
                                                                         this.CBO01_EDIRECHIP.GetValue().ToString() == "5LC" ? ds.Tables[0].Rows[i]["EDISRELINBI"].ToString() : ds.Tables[0].Rows[i]["EDISRELIN"].ToString(),
                                                                         ds.Tables[0].Rows[i]["EDISREERRFT"].ToString(),
                                                                         ds.Tables[0].Rows[i]["EDISRECHAFT"].ToString(),
                                                                         TYUserInfo.EmpNo
                                                                         );
                        }
                    }

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            this.DbConnector.Attach("TY_P_UT_73VGZ187", ds.Tables[1].Rows[i]["EDISRELIN"].ToString(),
                                                                             ds.Tables[1].Rows[i]["EDISREERRFT"].ToString(),
                                                                             ds.Tables[1].Rows[i]["EDISRECHAFT"].ToString(),
                                                                             TYUserInfo.EmpNo,
                                                                             ds.Tables[1].Rows[i]["EDISGJ"].ToString(),
                                                                             ds.Tables[1].Rows[i]["EDISRENO1"].ToString(),
                                                                             ds.Tables[1].Rows[i]["EDISRENO2"].ToString(),
                                                                             ds.Tables[1].Rows[i]["EDISRENO3"].ToString(),
                                                                             ds.Tables[1].Rows[i]["EDISRECHASU"].ToString(),
                                                                             ds.Tables[1].Rows[i]["EDISSEQ"].ToString()
                                                                             );
                        }
                    }
                
            }

            if (this.DbConnector.CommandCount > 0)
            {
                this.DbConnector.ExecuteTranQueryList();
            }

            fsGubn = "EDIT";

            UP_DataBinding_Load();

            this.ShowMessage("TY_M_GB_23NAD873");

        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            if (this.CBO01_EDIRECHIP.GetValue().ToString() == "5LC")
            {
                ds.Tables.Add(this.FPS91_TY_S_UT_7459E206.GetDataSourceInclude(TSpread.TActionType.New, "EDISGJ", "EDISRENO1", "EDISRENO2", "EDISRENO3", "EDISRECHASU", "EDISSEQ", "EDISRELINBI", "EDISREERRFT", "EDISRECHAFT"));
                ds.Tables.Add(this.FPS91_TY_S_UT_7459E206.GetDataSourceInclude(TSpread.TActionType.Update, "EDISGJ", "EDISRENO1", "EDISRENO2", "EDISRENO3", "EDISRECHASU", "EDISSEQ", "EDISRELINBI", "EDISREERRFT", "EDISRECHAFT"));
            }
            else
            {
                ds.Tables.Add(this.FPS91_TY_S_UT_73VBJ174.GetDataSourceInclude(TSpread.TActionType.New, "EDISGJ", "EDISRENO1", "EDISRENO2", "EDISRENO3", "EDISRECHASU", "EDISSEQ", "EDISRELIN", "EDISREERRFT", "EDISRECHAFT"));
                ds.Tables.Add(this.FPS91_TY_S_UT_73VBJ174.GetDataSourceInclude(TSpread.TActionType.Update, "EDISGJ", "EDISRENO1", "EDISRENO2", "EDISRENO3", "EDISRECHASU", "EDISSEQ", "EDISRELIN", "EDISREERRFT", "EDISRECHAFT"));
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region  Description : UP_SetFieldLock
        private void UP_SetFieldLock()
        {
            this.CBO01_EDIGJ.SetReadOnly(true);
            this.TXT01_EDIRENO1.SetReadOnly(true);
            this.TXT01_EDIRENO2.SetReadOnly(true);
            this.TXT01_EDIRENO3.SetReadOnly(true);
            this.TXT01_EDIRECHASU.SetReadOnly(true);
            this.DTP01_EDIREDATE.SetReadOnly(true);
            this.CBO01_EDIREGIS.SetReadOnly(true);
        }
        #endregion

        #region  Description : Lock Check
        private void UP_SetLockCheck()
        {
            if (TYUserInfo.DeptCode.Substring(0, 1) == "S")
            {
                CBO01_EDIGJ.SetValue("S");
            }
            else
            {
                CBO01_EDIGJ.SetValue("T");
            }

            if (TYUserInfo.DeptCode.Substring(0, 6) != "A10300")
            {
                CBO01_EDIGJ.SetReadOnly(true);
            }
        }
        #endregion

        #region  Description : FPS91_TY_S_UT_73VBJ174_RowInserted
        private void FPS91_TY_S_UT_73VBJ174_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_UT_73VBJ174.SetValue(e.RowIndex, "EDISGJ", CBO01_EDIGJ.GetValue().ToString() );
            this.FPS91_TY_S_UT_73VBJ174.SetValue(e.RowIndex, "EDISRENO1", TXT01_EDIRENO1.GetValue().ToString() );
            this.FPS91_TY_S_UT_73VBJ174.SetValue(e.RowIndex, "EDISRENO2", TXT01_EDIRENO2.GetValue().ToString());
            this.FPS91_TY_S_UT_73VBJ174.SetValue(e.RowIndex, "EDISRENO3", TXT01_EDIRENO3.GetValue().ToString());
            this.FPS91_TY_S_UT_73VBJ174.SetValue(e.RowIndex, "EDISRECHASU", Set_Fill2(TXT01_EDIRECHASU.GetValue().ToString()));

            this.FPS91_TY_S_UT_73VBJ174.SetValue(e.RowIndex, "EDISSEQ", UP_Get_SeqDetail());

        }
        #endregion

        #region  Description : FPS91_TY_S_UT_7459E206_RowInserted
        private void FPS91_TY_S_UT_7459E206_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_UT_7459E206.SetValue(e.RowIndex, "EDISGJ", CBO01_EDIGJ.GetValue().ToString());
            this.FPS91_TY_S_UT_7459E206.SetValue(e.RowIndex, "EDISRENO1", TXT01_EDIRENO1.GetValue().ToString());
            this.FPS91_TY_S_UT_7459E206.SetValue(e.RowIndex, "EDISRENO2", TXT01_EDIRENO2.GetValue().ToString());
            this.FPS91_TY_S_UT_7459E206.SetValue(e.RowIndex, "EDISRENO3", TXT01_EDIRENO3.GetValue().ToString());
            this.FPS91_TY_S_UT_7459E206.SetValue(e.RowIndex, "EDISRECHASU", Set_Fill2(TXT01_EDIRECHASU.GetValue().ToString()));

            this.FPS91_TY_S_UT_7459E206.SetValue(e.RowIndex, "EDISSEQ", UP_Get_SeqDetail());
        }
        #endregion

        #region  Description : UP_Get_SeqDetail
        private string UP_Get_SeqDetail()
        {
            Int16 iRowMaxValue = 0;
            string sCnt = string.Empty;

            sCnt = "1";

            TYSpread TySpy = new TYSpread();

            TySpy = this.CBO01_EDIRECHIP.GetValue().ToString() == "5LC" ? this.FPS91_TY_S_UT_7459E206 : this.FPS91_TY_S_UT_73VBJ174;


                if (TySpy.CurrentRowCount > 0)
                {
                    for (int i = 0; i < TySpy.CurrentRowCount; i++)
                    {
                        if (i == 0)
                        {
                            iRowMaxValue = Convert.ToInt16(Get_Numeric(TySpy.GetValue(i, "EDISSEQ").ToString()));
                        }

                        if (i != 0 && Convert.ToInt16(Get_Numeric(TySpy.GetValue(i, "EDISSEQ").ToString())) > iRowMaxValue)
                        {
                            iRowMaxValue = Convert.ToInt16(Get_Numeric(TySpy.GetValue(i, "EDISSEQ").ToString()));
                        }
                    }

                    iRowMaxValue += 1;

                    sCnt = iRowMaxValue.ToString();
                }
          

            return sCnt;
        }
        #endregion

        #region  Description : UP_DataBinding_Load
        private void UP_DataBinding_Load()
        {
            if (fsGubn == "NEW")
            {
                UP_DataBinding_New();

                UP_AutoSeq();

                this.SetStartingFocus(this.DTP01_EDIREDATE);
            }
            else
            {
                UP_DataBinding_Edit();

                UP_SetFieldLock();

                this.SetStartingFocus(this.TXT01_EDIREFTX);
            }

            UP_DataBinding_Detail();
        }
        #endregion

       
    }
}
