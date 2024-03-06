using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.AC00
{
    /// <summary>
    /// 프로젝트 이자관리 등록 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2020.11.04 10:03
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_AB4A8116 : 프로젝트관리 Master 등록
    ///  TY_P_AC_AB4A9117 : 프로젝트관리 Master 수정
    ///  TY_P_AC_AB4A9118 : 프로젝트관리 Master 삭제
    ///  TY_P_AC_AB4AT119 : 프로젝트 차입이자율 확인
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_AB4AU120 : 프로젝트 차입이자율 확인
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  AJNDPAC : 귀속부서
    ///  AJNPJGB : 프로젝트구분
    ///  AJNSTATUS : 종료구분
    ///  AJNDATE : 기준일자
    ///  AJNCLDATE : 종료년월
    ///  AJNINAMT : 투자금액
    ///  AJNNOTE : 비  고
    /// </summary>
    public partial class TYACNC015I : TYBase
    {
        private string fsAJNPJGB;
        private string fsAJNDATE;
        private string fsAJNDPAC;


        #region  Description : 폼 로드 이벤트
        public TYACNC015I(string sAJNPJGB, string sAJNDATE, string sAJNDPAC)
        {
            InitializeComponent();

            fsAJNPJGB = sAJNPJGB;
            fsAJNDATE = sAJNDATE;
            fsAJNDPAC = sAJNDPAC;

            this.SetPopupStyle();
        }

        private void TYACNC015I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            LBL52_AJNNOTE.SetValue("차입이자율");

            CBH01_AJNDPAC.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            if (string.IsNullOrEmpty(this.fsAJNPJGB))
            {

                CBH01_AJNDPAC.SetValue("A10000");
                DTP01_AJNDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));
                
                this.SetStartingFocus(CBH01_AJNPJGB.CodeText); 
            }
            else
            {
               
                UP_DataBinding();

                this.SetStartingFocus(TXT01_AJNINAMT);
            }

            
        }
        #endregion

        #region  Description : UP_DataBinding 이벤트
        private void UP_DataBinding()
        {
            CBH01_AJNPJGB.SetValue(fsAJNPJGB);
            DTP01_AJNDATE.SetValue(fsAJNDATE);
            CBH01_AJNDPAC.SetValue(fsAJNDPAC);

            CBH01_AJNPJGB.SetReadOnly(true);
            DTP01_AJNDATE.SetReadOnly(true);
            CBH01_AJNDPAC.SetReadOnly(true);

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_AB4GZ127", CBH01_AJNPJGB.GetValue(), DTP01_AJNDATE.GetString().Substring(0, 6), CBH01_AJNDPAC.GetValue());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
                this.CurrentDataTableRowMapping(dt, "01");


            this.FPS91_TY_S_AC_AB4AU120.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_AB4AT119", CBH01_AJNPJGB.GetValue(), DTP01_AJNDATE.GetString().Substring(0, 6), CBH01_AJNDPAC.GetValue());
            this.FPS91_TY_S_AC_AB4AU120.SetValue(this.DbConnector.ExecuteDataTable());            
            
        }
        #endregion


        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            if (string.IsNullOrEmpty(this.fsAJNPJGB))
            {
                this.DbConnector.Attach("TY_P_AC_AB4A8116",
                                        this.CBH01_AJNPJGB.GetValue().ToString().Trim(),
                                        DTP01_AJNDATE.GetString().Substring(0, 6),
                                        CBH01_AJNDPAC.GetValue().ToString(),
                                        TXT01_AJNINAMT.GetValue().ToString(),
                                        TXT01_AJNNOTE.GetValue().ToString(),
                                        CBO01_AJNSTATUS.GetValue().ToString(),
                                        MTB01_AJNCLDATE.GetValue().ToString().Replace("-",""),                                        
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper()
                                        );

            }
            else
            {
                this.DbConnector.Attach("TY_P_AC_AB4A9117",                                       
                                       TXT01_AJNINAMT.GetValue().ToString(),
                                       TXT01_AJNNOTE.GetValue().ToString(),
                                       CBO01_AJNSTATUS.GetValue().ToString(),
                                       MTB01_AJNCLDATE.GetValue().ToString().Replace("-", ""),
                                       TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                       this.CBH01_AJNPJGB.GetValue().ToString().Trim(),
                                       DTP01_AJNDATE.GetString().Substring(0, 6),
                                       CBH01_AJNDPAC.GetValue().ToString()
                                       );
            }
            

            if (ds.Tables[0].Rows.Count > 0)  //등록
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_AB4HE128",
                                             ds.Tables[0].Rows[i]["AJRPJGB"].ToString(),
                                             ds.Tables[0].Rows[i]["AJRDATE"].ToString(),
                                             ds.Tables[0].Rows[i]["AJRDPAC"].ToString(),
                                             ds.Tables[0].Rows[i]["AJRYYMM"].ToString(),
                                             ds.Tables[0].Rows[i]["AJRRATE"].ToString(),
                                             TYUserInfo.EmpNo.ToString().Trim().ToUpper()
                                            );
                }
            }

            if (ds.Tables[1].Rows.Count > 0)  //수정
            {
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_AB4HE129",
                                             ds.Tables[1].Rows[i]["AJRRATE"].ToString(),
                                             TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                             ds.Tables[1].Rows[i]["AJRPJGB"].ToString(),
                                             ds.Tables[1].Rows[i]["AJRDATE"].ToString(),
                                             ds.Tables[1].Rows[i]["AJRDPAC"].ToString(),
                                             ds.Tables[1].Rows[i]["AJRYYMM"].ToString()                                             
                                            );

                }
            }

            if (ds.Tables[2].Rows.Count > 0)  //삭제
            {
                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_AB4HE130",
                                             ds.Tables[2].Rows[i]["AJRPJGB"].ToString(),
                                             ds.Tables[2].Rows[i]["AJRDATE"].ToString(),
                                             ds.Tables[2].Rows[i]["AJRDPAC"].ToString(),
                                             ds.Tables[2].Rows[i]["AJRYYMM"].ToString()                                             
                                            );

                }
            }

            this.DbConnector.ExecuteTranQueryList();

            fsAJNPJGB = CBH01_AJNPJGB.GetValue().ToString();
            fsAJNDATE = DTP01_AJNDATE.GetString().Substring(0, 6);
            fsAJNDPAC = CBH01_AJNDPAC.GetValue().ToString();

            UP_DataBinding();

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_AB4AU120.GetDataSourceInclude(TSpread.TActionType.New, "AJRPJGB", "AJRDATE", "AJRDPAC", "AJRYYMM", "AJRRATE"));
            ds.Tables.Add(this.FPS91_TY_S_AC_AB4AU120.GetDataSourceInclude(TSpread.TActionType.Update, "AJRPJGB", "AJRDATE", "AJRDPAC", "AJRYYMM", "AJRRATE"));
            ds.Tables.Add(this.FPS91_TY_S_AC_AB4AU120.GetDataSourceInclude(TSpread.TActionType.Remove, "AJRPJGB", "AJRDATE", "AJRDPAC", "AJRYYMM", "AJRRATE"));


            if (CBO01_AJNSTATUS.GetValue().ToString() != "N" &&  MTB01_AJNCLDATE.GetValue().ToString().Replace("-","").Trim() == "" )
            {
                this.SetFocus(MTB01_AJNCLDATE);
                this.ShowCustomMessage("종료년월을 입력하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (ds.Tables[0].Rows.Count > 0 )
            {                
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_AB594131", ds.Tables[0].Rows[i]["AJRPJGB"].ToString(),
                                                                ds.Tables[0].Rows[i]["AJRDATE"].ToString(),
                                                                ds.Tables[0].Rows[i]["AJRDPAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["AJRYYMM"].ToString());
                    DataTable dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        this.ShowCustomMessage("적용년월 이후 자료가 존재합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_AB6FL146", ds.Tables[2].Rows[i]["AJRPJGB"].ToString(),
                                                                ds.Tables[2].Rows[i]["AJRDATE"].ToString(),
                                                                ds.Tables[2].Rows[i]["AJRDPAC"].ToString(),
                                                                ds.Tables[2].Rows[i]["AJRYYMM"].ToString());
                    DataTable dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        this.ShowCustomMessage("적용년월 이후 자료가 존재합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : FPS91_TY_S_AC_AB4AU120_RowInserted 이벤트
        private void FPS91_TY_S_AC_AB4AU120_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            if (this.CBH01_AJNPJGB.GetValue().ToString() != "" && this.DTP01_AJNDATE.GetValue().ToString() != "" && this.CBH01_AJNDPAC.GetValue().ToString() != "" )
            {
                this.FPS91_TY_S_AC_AB4AU120.SetValue(e.RowIndex, "AJRPJGB", this.CBH01_AJNPJGB.GetValue());
                this.FPS91_TY_S_AC_AB4AU120.SetValue(e.RowIndex, "AJRDATE", this.DTP01_AJNDATE.GetValue());
                this.FPS91_TY_S_AC_AB4AU120.SetValue(e.RowIndex, "AJRDPAC", this.CBH01_AJNDPAC.GetValue());
                this.FPS91_TY_S_AC_AB4AU120.SetValue(e.RowIndex, "AJRYYMM", DateTime.Now.ToString("yyyy-MM") );
            }
            else
            {
                this.ShowCustomMessage("프로젝트 구분이 먼저 선택되어야 합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region  Description : DTP01_AJNDATE_ValueChanged 이벤트
        private void DTP01_AJNDATE_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_AJNDPAC.DummyValue = this.DTP01_AJNDATE.GetString().ToString();
        }
        #endregion


    }
}
