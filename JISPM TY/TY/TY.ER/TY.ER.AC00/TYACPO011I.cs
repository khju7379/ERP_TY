using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 계열사 인원주주현황 관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.07.18 17:05
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_37I51156 : EIS 계열사 임원현황 수정
    ///  TY_P_AC_37I52157 : EIS 계열사 임원현황 삭제
    ///  TY_P_AC_37I54158 : EIS 계열사 주주현황 등록
    ///  TY_P_AC_37I58155 : EIS 계열사 임원현황 등록
    ///  TY_P_AC_37I5A159 : EIS 계열사 주주현황 수정
    ///  TY_P_AC_37I5B160 : EIS 계열사 주주현황 삭제
    ///  TY_P_AC_37I5C161 : EIS 계열사 임원현황 조회
    ///  TY_P_AC_37I5D162 : EIS 계열사 주주현황 조회
    ///  TY_P_AC_37JBF165 : EIS 계열사 인원현황 조회(태영GLS)
    ///  TY_P_AC_37JBG166 : EIS 계열사 인원현황 조회(태영그레인)
    ///  TY_P_AC_37JBG167 : EIS 계열사 인원현황 조회(태영호라이즌)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_37JBP168 : EIS 계열사 인원현황 조회
    ///  TY_S_AC_37JBQ169 : EIS 계열사 임원현황 조회
    ///  TY_S_AC_37JBR170 : EIS 계열사 주주현황 조회
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
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  ESPLCMPY : 계열사구분
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACPO011I : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYACPO011I()
        {
            InitializeComponent();
        }

        private void TYACPO011I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            
            this.DTP01_GSTYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            this.SetStartingFocus(this.DTP01_GSTYYMM);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            //인원현황
            this.FPS91_TY_S_AC_37JBP168.Initialize(); 
            this.DbConnector.CommandClear();

            if (this.CBH01_ESPLCMPY.GetValue().ToString() == "TH")
            {
                this.DbConnector.Attach("TY_P_AC_37JBG167", this.DTP01_GSTYYMM.GetString().ToString().Substring(0,6)   );
            }
            else if (this.CBH01_ESPLCMPY.GetValue().ToString() == "TG")
            {
                this.DbConnector.Attach("TY_P_AC_37JBG166", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
            }
            else
            {
                this.DbConnector.Attach("TY_P_AC_37JBF165", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
            }
            
            this.FPS91_TY_S_AC_37JBP168.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_37JBP168.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_AC_37JBP168, "JKCD", "합   계", SumRowType.Sum, "OPERTEAM", "SALESTEAM", "MANAGERTEAM", "SAFETEAM", "ORGTOTAL");
            }
            //임원현황
            this.FPS91_TY_S_AC_37JBQ169.Initialize(); 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_37I5C161", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6),CBH01_ESPLCMPY.GetValue());
            this.FPS91_TY_S_AC_37JBQ169.SetValue(this.DbConnector.ExecuteDataTable());

            //주주현황
            this.FPS91_TY_S_AC_37JBR170.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_37I5D162", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), CBH01_ESPLCMPY.GetValue());
            this.FPS91_TY_S_AC_37JBR170.SetValue(this.DbConnector.ExecuteDataTable());
            if (this.FPS91_TY_S_AC_37JBR170.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_AC_37JBR170, "ESBIRTH", "합   계", SumRowType.Sum, "ESSTOCKCNT", "ESSTOCKAMOUNT", "ESSTOCKRATE");
            }
            
        }
        #endregion

        #region Description : 삭제 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_37I52157", ds.Tables[0] );
            this.DbConnector.Attach("TY_P_AC_37I5B160", ds.Tables[1]);
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");

        }
        #endregion

        #region Description : 저장 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            //임원현황
            
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_37I58155", ds.Tables[0].Rows[i]["EFYYMM"].ToString(),
                                                            ds.Tables[0].Rows[i]["EFSUBGN"].ToString(), 
                                                            ds.Tables[0].Rows[i]["EFYYMM"].ToString(), 
                                                            ds.Tables[0].Rows[i]["EFSUBGN"].ToString(), 
                                                            ds.Tables[0].Rows[i]["EFJKCD"].ToString(), 
                                                            ds.Tables[0].Rows[i]["EFNAME"].ToString(), 
                                                            ds.Tables[0].Rows[i]["EFBIRTHDY"].ToString(), 
                                                            ds.Tables[0].Rows[i]["EFHOLDJOB"].ToString(), 
                                                            ds.Tables[0].Rows[i]["EFCAREER"].ToString(), 
                                                            TYUserInfo.EmpNo  
                                                            ); //저장          
            }
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_37I51156", ds.Tables[1].Rows[i]["EFJKCD"].ToString(),
                                                            ds.Tables[1].Rows[i]["EFNAME"].ToString(),
                                                            ds.Tables[1].Rows[i]["EFBIRTHDY"].ToString(),
                                                            ds.Tables[1].Rows[i]["EFHOLDJOB"].ToString(),
                                                            ds.Tables[1].Rows[i]["EFCAREER"].ToString(),
                                                            TYUserInfo.EmpNo,
                                                            ds.Tables[1].Rows[i]["EFYYMM"].ToString(), 
                                                            ds.Tables[1].Rows[i]["EFSUBGN"].ToString(), 
                                                            ds.Tables[1].Rows[i]["EFSEQ"].ToString() 
                                                            ); //수정      
            }
            
            //주주현황            
            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_37I54158", ds.Tables[2].Rows[i]["ESYYMM"].ToString(),
                                                            ds.Tables[2].Rows[i]["ESSUBGN"].ToString(),
                                                            ds.Tables[2].Rows[i]["ESYYMM"].ToString(),
                                                            ds.Tables[2].Rows[i]["ESSUBGN"].ToString(),
                                                            ds.Tables[2].Rows[i]["ESHOLDNAME"].ToString(),
                                                            ds.Tables[2].Rows[i]["ESBIRTH"].ToString(),
                                                            ds.Tables[2].Rows[i]["ESSTOCKCNT"].ToString(),
                                                            ds.Tables[2].Rows[i]["ESSTOCKPER"].ToString(),
                                                            ds.Tables[2].Rows[i]["ESSTOCKAMOUNT"].ToString(),
                                                            ds.Tables[2].Rows[i]["ESSTOCKRATE"].ToString(),
                                                            TYUserInfo.EmpNo
                                                            ); //저장          
            }            

            for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_37I5A159", ds.Tables[3].Rows[i]["ESHOLDNAME"].ToString(),
                                                            ds.Tables[3].Rows[i]["ESBIRTH"].ToString(),
                                                            ds.Tables[3].Rows[i]["ESSTOCKCNT"].ToString(),
                                                            ds.Tables[3].Rows[i]["ESSTOCKPER"].ToString(),
                                                            ds.Tables[3].Rows[i]["ESSTOCKAMOUNT"].ToString(),
                                                            ds.Tables[3].Rows[i]["ESSTOCKRATE"].ToString(),
                                                            TYUserInfo.EmpNo,
                                                            ds.Tables[3].Rows[i]["ESYYMM"].ToString(),
                                                            ds.Tables[3].Rows[i]["ESSUBGN"].ToString(),
                                                            ds.Tables[3].Rows[i]["ESSEQ"].ToString()
                                                            ); //저장          
            }


            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD873");   
        }
        #endregion


        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            //임원현황 
            ds.Tables.Add(this.FPS91_TY_S_AC_37JBQ169.GetDataSourceInclude(TSpread.TActionType.New, "EFYYMM", "EFSUBGN", "EFSEQ", "EFJKCD", "EFNAME", "EFBIRTHDY", "EFHOLDJOB", "EFCAREER" ));
            ds.Tables.Add(this.FPS91_TY_S_AC_37JBQ169.GetDataSourceInclude(TSpread.TActionType.Update, "EFYYMM", "EFSUBGN", "EFSEQ", "EFJKCD", "EFNAME", "EFBIRTHDY", "EFHOLDJOB", "EFCAREER"));
            //주주현황
            ds.Tables.Add(this.FPS91_TY_S_AC_37JBR170.GetDataSourceInclude(TSpread.TActionType.New, "ESYYMM", "ESSUBGN", "ESSEQ", "ESHOLDNAME", "ESBIRTH", "ESSTOCKCNT", "ESSTOCKPER", "ESSTOCKAMOUNT", "ESSTOCKRATE"));
            ds.Tables.Add(this.FPS91_TY_S_AC_37JBR170.GetDataSourceInclude(TSpread.TActionType.Update, "ESYYMM", "ESSUBGN", "ESSEQ", "ESHOLDNAME", "ESBIRTH", "ESSTOCKCNT", "ESSTOCKPER", "ESSTOCKAMOUNT", "ESSTOCKRATE"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0 && ds.Tables[3].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_25F59464");
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

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add( this.FPS91_TY_S_AC_37JBQ169.GetDataSourceInclude(TSpread.TActionType.Remove, "EFYYMM", "EFSUBGN", "EFSEQ") );
            ds.Tables.Add(this.FPS91_TY_S_AC_37JBR170.GetDataSourceInclude(TSpread.TActionType.Remove, "ESYYMM", "ESSUBGN", "ESSEQ"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 )
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

            e.ArgData = ds;

        }
        #endregion

        #region Description : FPS91_TY_S_AC_37JBQ169_RowInserted 이벤트
        private void FPS91_TY_S_AC_37JBQ169_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_37JBQ169.SetValue(e.RowIndex, "EFYYMM", this.DTP01_GSTYYMM.GetString().ToString().Substring(0,6));
            this.FPS91_TY_S_AC_37JBQ169.SetValue(e.RowIndex, "EFSUBGN", this.CBH01_ESPLCMPY.GetValue().ToString());
        }
        #endregion

        #region Description : FPS91_TY_S_AC_37JBR170_RowInserted 이벤트
        private void FPS91_TY_S_AC_37JBR170_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_37JBR170.SetValue(e.RowIndex, "ESYYMM", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
            this.FPS91_TY_S_AC_37JBR170.SetValue(e.RowIndex, "ESSUBGN", this.CBH01_ESPLCMPY.GetValue().ToString());
        }
        #endregion

        #region Description : 복사 버튼 이벤트
        private void BTN61_COPY_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYACPO011B()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}
