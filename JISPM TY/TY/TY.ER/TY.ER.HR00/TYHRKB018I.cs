using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 퇴충금 옵션관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.02.20 13:33
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_72KDR716 : 퇴충금옵션관리 MAST 등록
    ///  TY_P_HR_72KDS717 : 퇴충금옵션관리 MAST 수정
    ///  TY_P_HR_72KDS718 : 퇴충금옵션관리 MAST 삭제
    ///  TY_P_HR_72KDT719 : 퇴충금옵션관리 DETAIL  등록
    ///  TY_P_HR_72KDW720 : 퇴충금옵션관리 DETAIL  수정
    ///  TY_P_HR_72KDW721 : 퇴충금옵션관리 DETAIL  삭제
    ///  TY_P_HR_72KDX722 : 퇴충금옵션관리 임원배수관리 등록
    ///  TY_P_HR_72KDY723 : 퇴충금옵션관리 임원배수관리 수정
    ///  TY_P_HR_72KE0724 : 퇴충금옵션관리 임원배수관리 삭제
    ///  TY_P_HR_72KE1728 : 퇴충금옵션관리 DETAIL 조회
    ///  TY_P_HR_72KE3725 : 퇴충금옵션관리 직급별인상율관리 등록
    ///  TY_P_HR_72KE3729 : 퇴충금옵션관리 임원배수관리 조회
    ///  TY_P_HR_72KE5730 : 퇴충금옵션관리 직급별인상율관리 조회
    ///  TY_P_HR_72KE8726 : 퇴충금옵션관리 직급별인상율관리 수정
    ///  TY_P_HR_72KE9727 : 퇴충금옵션관리 직급별인상율관리 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_72KEF731 : 퇴충금옵션관리 DETAIL 조회
    ///  TY_S_HR_72KEG732 : 퇴충금옵션관리 임원배수관리 조회
    ///  TY_S_HR_72KEG733 : 퇴충금옵션관리 직급별인상율관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
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
    ///  TOCOMGUBN : 구분(실적,예상)
    ///  TOKIJUNDATE : 퇴충금기준일자
    ///  TOOTDATEE : OT적용종료년월
    ///  TOOTDATES : OT적용시작년월
    ///  TOOTGUBN : 구분(실적,예상)
    ///  TOSEQ : 순번
    ///  TOYEAR : 년도
    /// </summary>
    public partial class TYHRKB018I : TYBase
    {
        public string fsTOYEARNUM = string.Empty;
        private string fsDBOption = string.Empty;

        #region  Description : 폼 로드 이벤트
        public TYHRKB018I()
        {
            InitializeComponent();

            this.SetPopupStyle();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_72KEG732, "TFSABUN", "TFSABUNNM", "TFSABUN");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_72KEG733, "TKJKCD", "TKJKCDNM", "TKJKCD");  
        }

        private void TYHRKB018I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            ToolStripMenuItem reateCOPY = new ToolStripMenuItem("옵션복사");
            reateCOPY.Click += new EventHandler(COPY_ToolStripMenuItem_Click);

            this.FPS91_TY_S_HR_72KH2735.CurrentContextMenu.Items.AddRange(
                new System.Windows.Forms.ToolStripItem[] { new ToolStripSeparator(), reateCOPY });


            UP_Set_BTNDsp("1");

            fsDBOption = "ADD";

            TXT02_TOYEAR.SetReadOnly(false);

            TXT01_TOYEAR.SetValue(DateTime.Now.ToString("yyyy"));

            DTP01_TOKIJUNDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            DTP01_TOPYMDATE1.SetValue(DateTime.Now.ToString("yyyy-MM"));
            DTP01_TOPYMDATE2.SetValue(DateTime.Now.ToString("yyyy-MM"));
            DTP01_TOPYMDATE3.SetValue(DateTime.Now.ToString("yyyy-MM"));

            DTP01_TOPYSDATE_FROM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            DTP01_TOPYSDATE_TO.SetValue(DateTime.Now.ToString("yyyy-MM"));

            DTP01_TOYCHADATE.SetValue(DateTime.Now.ToString("yyyy-MM"));

            DTP01_TOOTDATES.SetValue(DateTime.Now.ToString("yyyy-MM"));
            DTP01_TOOTDATEE.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.BTN61_INQ_Click(null, null);

            this.SetStartingFocus(this.DTP01_TOKIJUNDATE);
        }
        #endregion


        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_72KH2735.Initialize();
            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_72KH2734", this.TXT02_TOYEAR.GetValue().ToString());
            this.FPS91_TY_S_HR_72KH2735.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            fsDBOption = "ADD";

            TXT01_TOYEAR.SetValue(DateTime.Now.ToString("yyyy"));
            TXT01_TOSEQ.SetValue("");

            DTP01_TOKIJUNDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            DTP01_TOPYMDATE1.SetValue(DateTime.Now.ToString("yyyy-MM"));
            DTP01_TOPYMDATE2.SetValue(DateTime.Now.ToString("yyyy-MM"));
            DTP01_TOPYMDATE3.SetValue(DateTime.Now.ToString("yyyy-MM"));

            DTP01_TOPYSDATE_FROM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            DTP01_TOPYSDATE_TO.SetValue(DateTime.Now.ToString("yyyy-MM"));

            DTP01_TOYCHADATE.SetValue(DateTime.Now.ToString("yyyy-MM"));

            CBO01_TOCOMGUBN.SetValue("1");
            CBO01_TOOTGUBN.SetValue("1");

            DTP01_TOOTDATES.SetValue(DateTime.Now.ToString("yyyy-MM"));
            DTP01_TOOTDATEE.SetValue(DateTime.Now.ToString("yyyy-MM"));

            RTB01_TOMEMO.SetValue("");
            
            this.FPS91_TY_S_HR_72KEG732.Initialize();
            this.FPS91_TY_S_HR_72KEG733.Initialize();

            UP_Set_BTNDsp("2");

        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            //퇴충금옵션관리 MAST 삭제            
            this.DbConnector.Attach("TY_P_HR_72KDS718", this.TXT01_TOYEAR.GetValue().ToString(), this.TXT01_TOSEQ.GetValue().ToString());

            //퇴충금옵션관리 직급별인상율관리 삭제            
            this.DbConnector.Attach("TY_P_HR_72KE9727",
                                                      TXT01_TOYEAR.GetValue().ToString(),
                                                      TXT01_TOSEQ.GetValue().ToString(),
                                                      ""
                                                      );
            //퇴충금옵션관리 임원배수관리 삭제            
            this.DbConnector.Attach("TY_P_HR_72KE0724",
                                                        TXT01_TOYEAR.GetValue().ToString(),
                                                        TXT01_TOSEQ.GetValue().ToString(),
                                                        ""
                                                      );
            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);

            BTN61_NEW_Click(null, null);

            this.UP_Run();

            UP_Set_BTNDsp("1");

            this.ShowMessage("TY_M_GB_23NAD874");
        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : FPS91_TY_S_HR_72KH2735_CellDoubleClick 이벤트
        private void FPS91_TY_S_HR_72KH2735_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            fsDBOption = "EDIT";

            this.TXT01_TOYEAR.SetValue(this.FPS91_TY_S_HR_72KH2735.GetValue("YEARNO").ToString().Substring(0,4));
            this.TXT01_TOSEQ.SetValue(this.FPS91_TY_S_HR_72KH2735.GetValue("YEARNO").ToString().Substring(5,3));

            this.UP_Run();

            UP_Set_BTNDsp("2");
        }
        #endregion

        #region  Description : UP_Run 이벤트
        private void UP_Run()
        {
            //퇴충금옵션관리 MAST 확인
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_72LA0740", this.TXT01_TOYEAR.GetValue().ToString(), this.TXT01_TOSEQ.GetValue().ToString());
            DataTable  dt = this.DbConnector.ExecuteDataTable();
            this.CurrentDataTableRowMapping(dt, "01");

            //퇴충금옵션관리 임원배수관리
            this.FPS91_TY_S_HR_72KEG732.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_72LAA742", this.TXT01_TOYEAR.GetValue().ToString(), this.TXT01_TOSEQ.GetValue().ToString());
            this.FPS91_TY_S_HR_72KEG732.SetValue(this.DbConnector.ExecuteDataTable());            

            //퇴충금옵션관리 직급별인상율관리
            this.FPS91_TY_S_HR_72KEG733.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_72LAB743", this.TXT01_TOYEAR.GetValue().ToString(), this.TXT01_TOSEQ.GetValue().ToString());
            this.FPS91_TY_S_HR_72KEG733.SetValue(this.DbConnector.ExecuteDataTable());                        

        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            if (fsDBOption == "ADD")
            {
                //번호 생성
                TXT01_TOSEQ.SetValue( Set_Fill3(UP_AutoNum(TXT01_TOYEAR.GetValue().ToString())));
                //퇴충금옵션관리 MAST 확인
                
                this.DbConnector.Attach("TY_P_HR_72KDR716", TXT01_TOYEAR.GetValue().ToString(),
                                                            TXT01_TOSEQ.GetValue().ToString(),
                                                            DTP01_TOKIJUNDATE.GetString().ToString(),
                                                            DTP01_TOPYMDATE1.GetString().ToString(),
                                                            DTP01_TOPYMDATE2.GetString().ToString(),
                                                            DTP01_TOPYMDATE3.GetString().ToString(),
                                                            DTP01_TOPYSDATE_FROM.GetString().ToString(),
                                                            DTP01_TOPYSDATE_TO.GetString().ToString(),
                                                            DTP01_TOYCHADATE.GetString().ToString(),
                                                            CBO01_TOCOMGUBN.GetValue().ToString(),
                                                            CBO01_TOOTGUBN.GetValue().ToString(),
                                                            DTP01_TOOTDATES.GetString().ToString().Substring(0,6),
                                                            DTP01_TOOTDATEE.GetString().ToString().Substring(0, 6),
                                                            RTB01_TOMEMO.GetValue().ToString(),
                                                            TYUserInfo.EmpNo
                                                            );                

            }

            if (fsDBOption == "EDIT")
            {
                this.DbConnector.Attach("TY_P_HR_72KDS717",
                                                           DTP01_TOKIJUNDATE.GetString().ToString(),
                                                           DTP01_TOPYMDATE1.GetString().ToString(),
                                                           DTP01_TOPYMDATE2.GetString().ToString(),
                                                           DTP01_TOPYMDATE3.GetString().ToString(),
                                                           DTP01_TOPYSDATE_FROM.GetString().ToString(),
                                                           DTP01_TOPYSDATE_TO.GetString().ToString(),
                                                           DTP01_TOYCHADATE.GetString().ToString(),
                                                           CBO01_TOCOMGUBN.GetValue().ToString(),
                                                           CBO01_TOOTGUBN.GetValue().ToString(),
                                                           DTP01_TOOTDATES.GetString().ToString().Substring(0, 6),
                                                           DTP01_TOOTDATEE.GetString().ToString().Substring(0, 6),
                                                           RTB01_TOMEMO.GetValue().ToString(),
                                                           TYUserInfo.EmpNo,
                                                           TXT01_TOYEAR.GetValue().ToString(),
                                                           TXT01_TOSEQ.GetValue().ToString()
                                                           );                
            }

            //퇴충금옵션관리 직급별인상율관리
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //등록
                this.DbConnector.Attach("TY_P_HR_72KE3725",TXT01_TOYEAR.GetValue().ToString(),
                                                           TXT01_TOSEQ.GetValue().ToString(),
                                                           ds.Tables[0].Rows[i]["TKJKCD"].ToString(),
                                                           ds.Tables[0].Rows[i]["TKPAYGN"].ToString(),
                                                           ds.Tables[0].Rows[i]["TKINRATE1"].ToString(),
                                                           ds.Tables[0].Rows[i]["TKINAMT1"].ToString(),
                                                           ds.Tables[0].Rows[i]["TKINRATE2"].ToString(),
                                                           ds.Tables[0].Rows[i]["TKINAMT2"].ToString(),
                                                           TYUserInfo.EmpNo
                                                           );          

                   
            }

            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                //수정
                this.DbConnector.Attach("TY_P_HR_72KE8726",                                                           
                                                          ds.Tables[1].Rows[i]["TKPAYGN"].ToString(),
                                                          ds.Tables[1].Rows[i]["TKINRATE1"].ToString(),
                                                          ds.Tables[1].Rows[i]["TKINAMT1"].ToString(),
                                                          ds.Tables[1].Rows[i]["TKINRATE2"].ToString(),
                                                          ds.Tables[1].Rows[i]["TKINAMT2"].ToString(),
                                                          TYUserInfo.EmpNo,
                                                          TXT01_TOYEAR.GetValue().ToString(),
                                                          TXT01_TOSEQ.GetValue().ToString(),
                                                          ds.Tables[1].Rows[i]["TKJKCD"].ToString()
                                                          ); 
            }

            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
            {
                //삭제
                this.DbConnector.Attach("TY_P_HR_72KE9727",
                                                          TXT01_TOYEAR.GetValue().ToString(),
                                                          TXT01_TOSEQ.GetValue().ToString(),
                                                          ds.Tables[2].Rows[i]["TKJKCD"].ToString()
                                                          );
            }

            //퇴충금옵션관리 임원배수관리
            for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
            {
                //등록
                this.DbConnector.Attach("TY_P_HR_72KDX722",
                                                          TXT01_TOYEAR.GetValue().ToString(),
                                                          TXT01_TOSEQ.GetValue().ToString(),
                                                          ds.Tables[3].Rows[i]["TFSABUN"].ToString(),
                                                          ds.Tables[3].Rows[i]["TFBAESU"].ToString(),
                                                          TYUserInfo.EmpNo
                                                          );
            }

            for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
            {
                //수정
                this.DbConnector.Attach("TY_P_HR_72KDY723", ds.Tables[4].Rows[i]["TFBAESU"].ToString(),                                                          
                                                            TYUserInfo.EmpNo,
                                                            TXT01_TOYEAR.GetValue().ToString(),
                                                            TXT01_TOSEQ.GetValue().ToString(),
                                                            ds.Tables[4].Rows[i]["TFSABUN"].ToString()                                                          
                                                          );
            }
            
            for (int i = 0; i < ds.Tables[5].Rows.Count; i++)
            {
                //삭제
                this.DbConnector.Attach("TY_P_HR_72KE0724", 
                                                            TXT01_TOYEAR.GetValue().ToString(),
                                                            TXT01_TOSEQ.GetValue().ToString(),
                                                            ds.Tables[5].Rows[i]["TFSABUN"].ToString()
                                                          );
            }
            
            if (this.DbConnector.CommandCount > 0)
            {
                this.DbConnector.ExecuteTranQueryList();
            }

            this.BTN61_INQ_Click(null, null);

            this.UP_Run();

            this.ShowMessage("TY_M_GB_23NAD873");

        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            DataSet ds = new DataSet();

            //직급별인상액관리
            ds.Tables.Add(this.FPS91_TY_S_HR_72KEG733.GetDataSourceInclude(TSpread.TActionType.New, "TKYEAR", "TKSEQ", "TKJKCD", "TKPAYGN", "TKINRATE1", "TKINAMT1", "TKINRATE2", "TKINAMT2"));
            ds.Tables.Add(this.FPS91_TY_S_HR_72KEG733.GetDataSourceInclude(TSpread.TActionType.Update, "TKYEAR", "TKSEQ", "TKJKCD", "TKPAYGN", "TKINRATE1", "TKINAMT1", "TKINRATE2", "TKINAMT2"));
            ds.Tables.Add(this.FPS91_TY_S_HR_72KEG733.GetDataSourceInclude(TSpread.TActionType.Remove, "TKYEAR", "TKSEQ", "TKJKCD", "TKPAYGN", "TKINRATE1", "TKINAMT1", "TKINRATE2", "TKINAMT2"));

            //임원배수관리
            ds.Tables.Add(this.FPS91_TY_S_HR_72KEG732.GetDataSourceInclude(TSpread.TActionType.New, "TFYEAR", "TFSEQ", "TFSABUN", "TFBAESU"));
            ds.Tables.Add(this.FPS91_TY_S_HR_72KEG732.GetDataSourceInclude(TSpread.TActionType.Update, "TFYEAR", "TFSEQ", "TFSABUN", "TFBAESU"));
            ds.Tables.Add(this.FPS91_TY_S_HR_72KEG732.GetDataSourceInclude(TSpread.TActionType.Remove, "TFYEAR", "TFSEQ", "TFSABUN", "TFBAESU"));

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region  Description : UP_AutoNum 이벤트
        private string UP_AutoNum(string sYear)
        {
            string sNum = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_72LAZ744", sYear);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                sNum = dt.Rows[0][0].ToString();
            }

            return sNum;
        }
        #endregion

        #region  Description : FPS91_TY_S_HR_72KEG732_RowInserted 이벤트
        private void FPS91_TY_S_HR_72KEG732_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_HR_72KEG732.SetValue(e.RowIndex, "TFYEAR", this.TXT01_TOYEAR.GetValue());
            this.FPS91_TY_S_HR_72KEG732.SetValue(e.RowIndex, "TFSEQ", this.TXT01_TOSEQ.GetValue());
        }
        #endregion

        #region  Description : FPS91_TY_S_HR_72KEG732_RowInserted 이벤트
        private void FPS91_TY_S_HR_72KEG733_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_HR_72KEG733.SetValue(e.RowIndex, "TKYEAR", this.TXT01_TOYEAR.GetValue());
            this.FPS91_TY_S_HR_72KEG733.SetValue(e.RowIndex, "TKSEQ", this.TXT01_TOSEQ.GetValue());
        }
        #endregion

        #region  Description : 선택 버튼 이벤트
        private void BTN61_SEL_Click(object sender, EventArgs e)
        {
            if (TXT01_TOSEQ.GetValue().ToString() != "")
            {
                fsTOYEARNUM = TXT01_TOYEAR.GetValue().ToString() + "-" + Set_Fill3(TXT01_TOSEQ.GetValue().ToString());
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }
        #endregion

        #region  Description : 버튼 표시 이벤트
        private void UP_Set_BTNDsp( string sDspGubn  )
        {
            if (sDspGubn == "1")
            {
                BTN61_SEL.Visible = false;
                BTN61_NEW.Visible = true;
                BTN61_SAV.Visible = false;
                BTN61_REM.Visible = false;
            }
            else if (sDspGubn == "2")
            {
                BTN61_SEL.Visible = true;
                BTN61_NEW.Visible = true;
                BTN61_SAV.Visible = true;
                BTN61_REM.Visible = true;
            }
        }
        #endregion

        #region  Description : 옵션 복사 처리 팝업 이벤트
        private void COPY_ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string sYear = this.FPS91_TY_S_HR_72KH2735.GetValue("YEARNO").ToString();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_84QEW899", sYear.Substring(0, 4), sYear.Substring(5, 3));  //퇴충금옵션관리 MAST
            this.DbConnector.Attach("TY_P_HR_84QEZ900", sYear.Substring(0, 4), sYear.Substring(5, 3));  //임원배수관리 
            this.DbConnector.Attach("TY_P_HR_84QEZ901", sYear.Substring(0, 4), sYear.Substring(5, 3));  //직급별인상율관리
            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_AC_27J83134");
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        
       

      

     

      

        

        


    }
}
