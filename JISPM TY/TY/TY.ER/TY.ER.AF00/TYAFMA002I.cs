using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AF00
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
    public partial class TYAFMA002I : TYBase
    {
        private string sGUBUN = string.Empty;
        private string fsCompanyCode;

        #region  Description : 폼 로드 이벤트
        public TYAFMA002I()
        {
            InitializeComponent();
        }

        private void TYAFMA002I_Load(object sender, System.EventArgs e)
        {
            this.DTP01_EHYYMM.SetReadOnly(true);
            this.CBH01_EHSUBGN.SetReadOnlyCode(true);
            this.CBH01_EHSUBGN.SetReadOnlyText(true);
            this.TXT01_EHSEQ.SetReadOnly(true);

            // Key필드 수정모드시 잠금
            //this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_396C0566, "EFYYMM");
            //this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_396C0566, "EFSUBGN");
            //this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_396C0566, "EFSEQ");

            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_3961E571, "EHNUM");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.BTN62_SAV.ProcessCheck += new TButton.CheckHandler(BTN62_SAV_ProcessCheck);
            this.BTN62_REM.ProcessCheck += new TButton.CheckHandler(BTN62_REM_ProcessCheck);


            switch (TYUserInfo.EmpNo.Substring(0, 2))
            {
                case "HT":
                    fsCompanyCode = "TH";
                    break;
                case "TG":
                    fsCompanyCode = "TG";
                    break;
                case "TS":
                    fsCompanyCode = "TS";
                    break;
                case "TL":
                    fsCompanyCode = "TL";
                    break;
                default:
                    fsCompanyCode = "";
                    break;
            }

            if (fsCompanyCode != "")
            {
                this.CBH01_EFSUBGN.SetValue(fsCompanyCode);
                this.CBH01_EFSUBGN.SetReadOnly(true);
            }

            // 임원현황 탭 이벤트
            UP_Btn_Enable(true, false);

            sGUBUN = "Tab1";

            //this.DTP01_EFYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            //this.DTP01_EFYYMM.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"));

            UP_start_dsp_month();

            if (fsCompanyCode == "TL")
            {
                this.FPS91_TY_S_AC_3B55E199.Visible = true;
                this.FPS91_TY_S_AC_39625584.Visible = false; 

            }
            else
            {
                this.FPS91_TY_S_AC_39625584.Visible = true;
                this.FPS91_TY_S_AC_3B55E199.Visible = false; 
            }

            if (fsCompanyCode != "")
            {
                this.SetStartingFocus(this.DTP01_EFYYMM);
            }
            else
            {
                this.SetStartingFocus(this.CBH01_EFSUBGN.CodeText);
            }

            this.BTN62_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 임원 및 주주현황 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_39625584.Initialize();
            this.FPS91_TY_S_AC_396C0566.Initialize();
            this.FPS91_TY_S_AC_37JBR170.Initialize();

            this.FPS91_TY_S_AC_3961E571.Initialize();

            //임원현황
            if (sGUBUN.ToString() == "Tab2")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_396BZ564", this.DTP01_EFYYMM.GetString().ToString().Substring(0, 6), CBH01_EFSUBGN.GetValue());
                this.FPS91_TY_S_AC_396C0566.SetValue(this.DbConnector.ExecuteDataTable());
            }

            //주주현황
            if (sGUBUN.ToString() == "Tab3")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_37I5D162", this.DTP01_EFYYMM.GetString().ToString().Substring(0, 6), CBH01_EFSUBGN.GetValue());
                this.FPS91_TY_S_AC_37JBR170.SetValue(this.DbConnector.ExecuteDataTable());
                if (this.FPS91_TY_S_AC_37JBR170.CurrentRowCount > 0)
                {
                    this.SpreadSumRowAdd(this.FPS91_TY_S_AC_37JBR170, "ESBIRTH", "합   계", SumRowType.Sum, "ESSTOCKCNT", "ESSTOCKAMOUNT", "ESSTOCKRATE");
                }
            }
        }
        #endregion

        #region  Description : 임원 및 주주현황 버튼 이벤트
        private void BTN62_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_396C0566.Initialize();
            this.FPS91_TY_S_AC_37JBR170.Initialize();
            this.FPS91_TY_S_AC_39625584.Initialize();
            this.FPS91_TY_S_AC_3B55E199.Initialize();


            if (this.CBH01_EFSUBGN.GetValue().ToString() != "TL")
            {
                this.FPS91_TY_S_AC_3B55E199.Visible = false;
                this.FPS91_TY_S_AC_39625584.Visible = true; 
            }
            else
            {
                this.FPS91_TY_S_AC_3B55E199.Visible = true;
                this.FPS91_TY_S_AC_39625584.Visible = false;
            }

            // 인원현황
            this.DbConnector.CommandClear();

            if (this.CBH01_EFSUBGN.GetValue().ToString() == "TH")
            {
                this.DbConnector.Attach("TY_P_AC_39629574", this.DTP01_EFYYMM.GetString().ToString().Substring(0, 6));
            }
            else if (this.CBH01_EFSUBGN.GetValue().ToString() == "TG")
            {
                this.DbConnector.Attach("TY_P_AC_39620579", this.DTP01_EFYYMM.GetString().ToString().Substring(0, 6));
            }
            else if (this.CBH01_EFSUBGN.GetValue().ToString() == "TS")
            {
                this.DbConnector.Attach("TY_P_AC_39621580", this.DTP01_EFYYMM.GetString().ToString().Substring(0, 6));
            }
            else
            {                
                this.DbConnector.Attach("TY_P_AC_3B54J196", this.DTP01_EFYYMM.GetString().ToString().Substring(0, 6));
            }

            if (this.CBH01_EFSUBGN.GetValue().ToString() != "TL")
            {
                this.FPS91_TY_S_AC_39625584.SetValue(this.DbConnector.ExecuteDataTable());
                if (this.FPS91_TY_S_AC_39625584.CurrentRowCount > 0)
                {
                    this.SpreadSumRowAdd(this.FPS91_TY_S_AC_39625584, "BUSEONM", "합   계", SumRowType.Sum, "IMWON", "BUJANG", "CHAJANG", "GWAJANG", "DAELI", "JUIM", "SAWON", "HAP");
                }
            }
            else
            {
                this.FPS91_TY_S_AC_3B55E199.SetValue(this.DbConnector.ExecuteDataTable());
                if (this.FPS91_TY_S_AC_3B55E199.CurrentRowCount > 0)
                {
                    this.SpreadSumRowAdd(this.FPS91_TY_S_AC_3B55E199, "BUSEONM", "합   계", SumRowType.Sum, "IMWON", "BUJANG", "CHAJANG", "GWAJANG", "DAELI", "JUIM", "SAWON", "HAP");
                }               
            }
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            if (tabControl1.SelectedIndex == 1) // 임원현황
            {
                // 임원현황
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_396BF554", ds.Tables[0].Rows[i]["EFYYMM"].ToString(),
                                                                ds.Tables[0].Rows[i]["EFSUBGN"].ToString(),
                                                                ds.Tables[0].Rows[i]["EFYYMM"].ToString(),
                                                                ds.Tables[0].Rows[i]["EFSUBGN"].ToString(),
                                                                ds.Tables[0].Rows[i]["EFJKCD"].ToString(),
                                                                ds.Tables[0].Rows[i]["EFNAME"].ToString(),
                                                                ds.Tables[0].Rows[i]["EFBIRTHDY"].ToString(),
                                                                TYUserInfo.EmpNo
                                                                ); //저장          
                }

                for (i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_396BG555", ds.Tables[1].Rows[i]["EFJKCD"].ToString(),
                                                                ds.Tables[1].Rows[i]["EFNAME"].ToString(),
                                                                ds.Tables[1].Rows[i]["EFBIRTHDY"].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["EFYYMM"].ToString(),
                                                                ds.Tables[1].Rows[i]["EFSUBGN"].ToString(),
                                                                ds.Tables[1].Rows[i]["EFSEQ"].ToString()
                                                                ); //수정
                }
            }
            else if (tabControl1.SelectedIndex == 2) // 주주현황
            {
                //주주현황
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_37I54158", ds.Tables[0].Rows[i]["ESYYMM"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESSUBGN"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESYYMM"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESSUBGN"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESHOLDNAME"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESBIRTH"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESSTOCKCNT"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESSTOCKPER"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESSTOCKAMOUNT"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESSTOCKRATE"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESJUSO"].ToString(),
                                                                TYUserInfo.EmpNo
                                                                ); //저장          
                }

                for (i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_37I5A159", ds.Tables[1].Rows[i]["ESHOLDNAME"].ToString(),
                                                                ds.Tables[1].Rows[i]["ESBIRTH"].ToString(),
                                                                ds.Tables[1].Rows[i]["ESSTOCKCNT"].ToString(),
                                                                ds.Tables[1].Rows[i]["ESSTOCKPER"].ToString(),
                                                                ds.Tables[1].Rows[i]["ESSTOCKAMOUNT"].ToString(),
                                                                ds.Tables[1].Rows[i]["ESSTOCKRATE"].ToString(),
                                                                ds.Tables[1].Rows[i]["ESJUSO"].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["ESYYMM"].ToString(),
                                                                ds.Tables[1].Rows[i]["ESSUBGN"].ToString(),
                                                                ds.Tables[1].Rows[i]["ESSEQ"].ToString()
                                                                ); //수정
                }
            }

            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            if (tabControl1.SelectedIndex == 1) // 임원현황
            {
                this.DbConnector.Attach("TY_P_AC_396BG556", ds.Tables[0]); // 임원현황

                int i = 0;

                // 임원 겸직 및 경력 현황
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_3965X596", ds.Tables[0].Rows[i]["EFYYMM"].ToString(),
                                                                ds.Tables[0].Rows[i]["EFSUBGN"].ToString(),
                                                                ds.Tables[0].Rows[i]["EFSEQ"].ToString()
                                                                );
                }
            }
            else if (tabControl1.SelectedIndex == 2) // 주주현황
            {
                this.DbConnector.Attach("TY_P_AC_37I5B160", ds.Tables[0]);
            }

            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");

            if (tabControl1.SelectedIndex == 1) // 임원현황
            {
                this.FPS91_TY_S_AC_3961E571.Initialize();
            }
        }
        #endregion

        #region Description : 임원 겸직 및 경력 저장 버튼
        private void BTN62_SAV_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            //임원현황
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_396BG557", ds.Tables[0].Rows[i]["EHYYMM"].ToString(),
                                                            ds.Tables[0].Rows[i]["EHSUBGN"].ToString(),
                                                            ds.Tables[0].Rows[i]["EHSEQ"].ToString(),
                                                            ds.Tables[0].Rows[i]["EHNUM"].ToString(),
                                                            ds.Tables[0].Rows[i]["EHHOLDJOB"].ToString(),
                                                            ds.Tables[0].Rows[i]["EHCAREEF"].ToString(),
                                                            TYUserInfo.EmpNo
                                                            ); //저장
            }

            for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_396BG558", ds.Tables[1].Rows[i]["EHHOLDJOB"].ToString(),
                                                            ds.Tables[1].Rows[i]["EHCAREEF"].ToString(),
                                                            TYUserInfo.EmpNo,
                                                            ds.Tables[1].Rows[i]["EHYYMM"].ToString(),
                                                            ds.Tables[1].Rows[i]["EHSUBGN"].ToString(),
                                                            ds.Tables[1].Rows[i]["EHSEQ"].ToString(),
                                                            ds.Tables[1].Rows[i]["EHNUM"].ToString()
                                                            ); //수정
            }

            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD873");

            UP_Set_HOLDF();
        }
        #endregion

        #region Description : 임원 겸직 및 경력 삭제 버튼
        private void BTN62_REM_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_396BH559", ds.Tables[0]);
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");

            UP_Set_HOLDF();
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // ------------------------   마감 완료 CHECK 시작  ------------------------------------------ //

            this.DbConnector.CommandClear(); //TY_P_AC_27H64059
            this.DbConnector.Attach("TY_P_AC_3C92V659", this.DTP01_EFYYMM.GetValue().ToString().Substring(0, 4), this.DTP01_EFYYMM.GetValue().ToString().Substring(4, 2));
            DataTable dt1 = this.DbConnector.ExecuteDataTable();

            if (dt1.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                e.Successed = false;
                return;
            }
            else
            {
                if (dt1.Rows[0]["ECSBBUN"].ToString() == "Z")
                {
                    this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                    e.Successed = false;
                    return;
                }
            }

            // ------------------------   마감 완료 CHECK 끝 ------------------------------------------ //

            if (TYUserInfo.EmpNo.Substring(0, 1).ToString() == "0" || TYUserInfo.EmpNo.Substring(0, 1).ToString() == "C")
            {
                this.ShowMessage("TY_M_AC_3992B618");
                e.Successed = false;
                return;
            }

            DataSet ds = new DataSet();
            if (tabControl1.SelectedIndex == 1) // 임원현황
            {
                //임원현황
                ds.Tables.Add(this.FPS91_TY_S_AC_396C0566.GetDataSourceInclude(TSpread.TActionType.New,    "EFYYMM", "EFSUBGN", "EFSEQ", "EFJKCD", "EFNAME", "EFBIRTHDY"));
                ds.Tables.Add(this.FPS91_TY_S_AC_396C0566.GetDataSourceInclude(TSpread.TActionType.Update, "EFYYMM", "EFSUBGN", "EFSEQ", "EFJKCD", "EFNAME", "EFBIRTHDY"));
            }
            else if (tabControl1.SelectedIndex == 2) // 주주현황
            {
                //주주현황
                ds.Tables.Add(this.FPS91_TY_S_AC_37JBR170.GetDataSourceInclude(TSpread.TActionType.New,    "ESYYMM", "ESSUBGN", "ESSEQ", "ESHOLDNAME", "ESBIRTH", "ESSTOCKCNT", "ESSTOCKPER", "ESSTOCKAMOUNT", "ESSTOCKRATE", "ESJUSO"));
                ds.Tables.Add(this.FPS91_TY_S_AC_37JBR170.GetDataSourceInclude(TSpread.TActionType.Update, "ESYYMM", "ESSUBGN", "ESSEQ", "ESHOLDNAME", "ESBIRTH", "ESSTOCKCNT", "ESSTOCKPER", "ESSTOCKAMOUNT", "ESSTOCKRATE", "ESJUSO"));
            }

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
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
            // ------------------------   마감 완료 CHECK 시작  ------------------------------------------ //

            this.DbConnector.CommandClear(); // TY_P_AC_27H64059
            this.DbConnector.Attach("TY_P_AC_3C92V659", this.DTP01_EFYYMM.GetValue().ToString().Substring(0, 4), this.DTP01_EFYYMM.GetValue().ToString().Substring(4, 2));
            DataTable dt1 = this.DbConnector.ExecuteDataTable();

            if (dt1.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                e.Successed = false;
                return;
            }
            else
            {
                if (dt1.Rows[0]["ECSBBUN"].ToString() == "Z")
                {
                    this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                    e.Successed = false;
                    return;
                }
            }

            // ------------------------   마감 완료 CHECK 끝 ------------------------------------------ //

            if (TYUserInfo.EmpNo.Substring(0, 1).ToString() == "0" || TYUserInfo.EmpNo.Substring(0, 1).ToString() == "C")
            {
                this.ShowMessage("TY_M_AC_3992B618");
                e.Successed = false;
                return;
            }

            int i = 0;

            DataSet ds = new DataSet();

            if (tabControl1.SelectedIndex == 1) // 임원현황
            {
                ds.Tables.Add(this.FPS91_TY_S_AC_396C0566.GetDataSourceInclude(TSpread.TActionType.Remove, "EFYYMM", "EFSUBGN", "EFSEQ"));
            }
            else if (tabControl1.SelectedIndex == 2) // 주주현황
            {
                ds.Tables.Add(this.FPS91_TY_S_AC_37JBR170.GetDataSourceInclude(TSpread.TActionType.Remove, "ESYYMM", "ESSUBGN", "ESSEQ"));
            }

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach
                //    (
                //    "TY_P_AC_396BZ565",
                //    ds.Tables[0].Rows[i]["EFYYMM"].ToString(),
                //    ds.Tables[0].Rows[i]["EFSUBGN"].ToString(),
                //    ds.Tables[0].Rows[i]["EFSEQ"].ToString()
                //    );

                //DataTable dt = this.DbConnector.ExecuteDataTable();

                //if (dt.Rows.Count > 0)
                //{
                //    this.ShowMessage("TY_M_AC_3965T595");
                //    e.Successed = false;
                //    return; 
                //}
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 임원 겸직 및 경력 저장 ProcessCheck 이벤트
        private void BTN62_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // ------------------------   마감 완료 CHECK 시작  ------------------------------------------ //

            this.DbConnector.CommandClear(); // TY_P_AC_27H64059
            this.DbConnector.Attach("TY_P_AC_3C92V659", this.DTP01_EFYYMM.GetValue().ToString().Substring(0, 4), this.DTP01_EFYYMM.GetValue().ToString().Substring(4, 2));
            DataTable dt1 = this.DbConnector.ExecuteDataTable();

            if (dt1.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                e.Successed = false;
                return;
            }
            else
            {
                if (dt1.Rows[0]["ECSBBUN"].ToString() == "Z")
                {
                    this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                    e.Successed = false;
                    return;
                }
            }

            // ------------------------   마감 완료 CHECK 끝 ------------------------------------------ //

            if (TYUserInfo.EmpNo.Substring(0, 1).ToString() == "0" || TYUserInfo.EmpNo.Substring(0, 1).ToString() == "C")
            {
                this.ShowMessage("TY_M_AC_3992B618");
                e.Successed = false;
                return;
            }

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_3961E571.GetDataSourceInclude(TSpread.TActionType.New, "EHYYMM", "EHSUBGN", "EHSEQ", "EHNUM", "EHHOLDJOB", "EHCAREEF"));
            ds.Tables.Add(this.FPS91_TY_S_AC_3961E571.GetDataSourceInclude(TSpread.TActionType.Update, "EHYYMM", "EHSUBGN", "EHSEQ", "EHNUM", "EHHOLDJOB", "EHCAREEF"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
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

        #region Description : 임원 겸직 및 경력 삭제 ProcessCheck 이벤트
        private void BTN62_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // ------------------------   마감 완료 CHECK 시작  ------------------------------------------ //

            this.DbConnector.CommandClear(); // TY_P_AC_27H64059
            this.DbConnector.Attach("TY_P_AC_3C92V659", this.DTP01_EFYYMM.GetValue().ToString().Substring(0, 4), this.DTP01_EFYYMM.GetValue().ToString().Substring(4, 2));
            DataTable dt1 = this.DbConnector.ExecuteDataTable();

            if (dt1.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                e.Successed = false;
                return;
            }
            else
            {
                if (dt1.Rows[0]["ECSBBUN"].ToString() == "Z")
                {
                    this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                    e.Successed = false;
                    return;
                }
            }

            // ------------------------   마감 완료 CHECK 끝 ------------------------------------------ //

            if (TYUserInfo.EmpNo.Substring(0, 1).ToString() == "0" || TYUserInfo.EmpNo.Substring(0, 1).ToString() == "C")
            {
                this.ShowMessage("TY_M_AC_3992B618");
                e.Successed = false;
                return;
            }

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_3961E571.GetDataSourceInclude(TSpread.TActionType.Remove, "EHYYMM", "EHSUBGN", "EHSEQ", "EHNUM"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
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

        #region Description : 복사 버튼 이벤트
        private void BTN61_COPY_Click(object sender, EventArgs e)
        {
            string sEFSUBGN = string.Empty;

            if (TYUserInfo.EmpNo.Substring(0, 1).ToString() == "0" || TYUserInfo.EmpNo.Substring(0, 1).ToString() == "C")
            {
                this.ShowMessage("TY_M_AC_3992B618");
                return;
            }
            else
            {
                if (TYUserInfo.EmpNo.Substring(0, 2).ToString() != "HT")
                {
                    sEFSUBGN = TYUserInfo.EmpNo.Substring(0, 2).ToString();
                }
                else
                {
                    sEFSUBGN = "TH";
                }
            }

            if (this.OpenModalPopup(new TYAFMA002B(sEFSUBGN)) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 임원현황 겸직 및 경력 조회
        private void UP_Set_HOLDF()
        {
            this.FPS91_TY_S_AC_3961E571.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_396BZ565",
                this.DTP01_EHYYMM.GetValue(),
                this.CBH01_EHSUBGN.GetValue(),
                this.TXT01_EHSEQ.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_3961E571.SetValue(dt);
        }
        #endregion

        #region Description : 임원현황 스프레드 더블 클릭
        private void FPS91_TY_S_AC_396C0566_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DTP01_EHYYMM.SetValue(this.FPS91_TY_S_AC_396C0566.GetValue("EFYYMM").ToString());
            this.CBH01_EHSUBGN.SetValue(this.FPS91_TY_S_AC_396C0566.GetValue("EFSUBGN").ToString());
            this.TXT01_EHSEQ.SetValue(this.FPS91_TY_S_AC_396C0566.GetValue("EFSEQ").ToString());

            UP_Set_HOLDF();
        }
        #endregion

        #region Description : 임원현황 스프레드 추가 이벤트
        private void FPS91_TY_S_AC_396C0566_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_396C0566.SetValue(e.RowIndex, "EFYYMM",  this.DTP01_EFYYMM.GetString().ToString().Substring(0, 6));
            this.FPS91_TY_S_AC_396C0566.SetValue(e.RowIndex, "EFSUBGN", this.CBH01_EFSUBGN.GetValue().ToString());
        }
        #endregion

        #region Description : 임원 겸직 및 경력 스프레드 추가 이벤트
        private void FPS91_TY_S_AC_3961E571_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_3961E571.SetValue(e.RowIndex, "EHYYMM",  this.DTP01_EHYYMM.GetString().ToString().Substring(0, 6));
            this.FPS91_TY_S_AC_3961E571.SetValue(e.RowIndex, "EHSUBGN", this.CBH01_EHSUBGN.GetValue().ToString());
            this.FPS91_TY_S_AC_3961E571.SetValue(e.RowIndex, "EHSEQ",   this.TXT01_EHSEQ.GetValue().ToString());
        }
        #endregion

        #region Description : 주주현황 스프레드 추가 이벤트
        private void FPS91_TY_S_AC_37JBR170_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_37JBR170.SetValue(e.RowIndex, "ESYYMM",  this.DTP01_EFYYMM.GetString().ToString().Substring(0, 6));
            this.FPS91_TY_S_AC_37JBR170.SetValue(e.RowIndex, "ESSUBGN", this.CBH01_EFSUBGN.GetValue().ToString());
        }
        #endregion

        #region Description : 탭 이벤트
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0) // 인원현황
            {
                sGUBUN = "Tab1";
                UP_Btn_Enable(true, false);

                this.BTN62_INQ_Click(null, null);
            }
            else if (tabControl1.SelectedIndex == 1) // 임원현황
            {
                sGUBUN = "Tab2";
                UP_Btn_Enable(false, true);

                this.BTN61_INQ_Click(null, null);
            }
            else if (tabControl1.SelectedIndex == 2) // 주주현황
            {
                sGUBUN = "Tab3";
                UP_Btn_Enable(false, true);

                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region Description : 버튼 활성환
        private void UP_Btn_Enable(bool bResult1, bool bResult2)
        {
            this.BTN62_INQ.Visible   = bResult1;
            this.BTN62_BATCH.Visible = bResult1;

            this.BTN61_INQ.Visible  = bResult2;
            this.BTN61_SAV.Visible  = bResult2;
            this.BTN61_REM.Visible  = bResult2;
            this.BTN61_COPY.Visible = bResult2;
        }
        #endregion

        #region Description : 인원현황 처리 버튼
        private void BTN62_BATCH_Click(object sender, EventArgs e)
        {
            if (this.CBH01_EFSUBGN.GetValue().ToString() == "TY")
            {
                UP_TY_ORGCD();
            }

            if (this.CBH01_EFSUBGN.GetValue().ToString() == "TG")
            {
                UP_TG_ORGCD();
            }

            if (this.CBH01_EFSUBGN.GetValue().ToString() == "TS")
            {
                UP_TS_ORGCD();
            }

            if (this.CBH01_EFSUBGN.GetValue().ToString() == "TH")
            {
                UP_TH_ORGCD();
            }

            if (this.CBH01_EFSUBGN.GetValue().ToString() == "TL")
            {
                UP_TL_ORGCD();
            }

            this.BTN62_INQ_Click(null, null);
        }
        #endregion

        #region Description : 인원생성 함수
        private void UP_TY_ORGCD()  //태영
        {
            string sTEPPRIOR_ORG_CD = "";
            string sTEPPRIOR_ORG_CDNM = "";
            Int16 iTEPLIST01 = 0;
            Int16 iTEPLIST02 = 0;
            Int16 iTEPLIST03 = 0;
            Int16 iTEPLIST04 = 0;
            Int16 iTEPLIST05 = 0;
            Int16 iTEPLIST06 = 0;
            Int16 iTEPLIST07 = 0;
            Int16 iTEPLIST08 = 0;
            Int16 iTEPLIST09 = 0;
            Int16 iTEPLIST10 = 0;
            Int16 iTEPLIST11 = 0;
            Int16 iTEPLIST12 = 0;

            string sSEPPRIOR_ORG_CD = "";
            string sSEPPRIOR_ORG_CDNM = "";
            Int16 iSEPLIST01 = 0;
            Int16 iSEPLIST02 = 0;
            Int16 iSEPLIST03 = 0;
            Int16 iSEPLIST04 = 0;
            Int16 iSEPLIST05 = 0;
            Int16 iSEPLIST06 = 0;
            Int16 iSEPLIST07 = 0;
            Int16 iSEPLIST08 = 0;
            Int16 iSEPLIST09 = 0;
            Int16 iSEPLIST10 = 0;
            Int16 iSEPLIST11 = 0;
            Int16 iSEPLIST12 = 0;

            string sBEPPRIOR_ORG_CD = "";
            string sBEPPRIOR_ORG_CDNM = "";
            Int16 iBEPLIST01 = 0;
            Int16 iBEPLIST02 = 0;
            Int16 iBEPLIST03 = 0;
            Int16 iBEPLIST04 = 0;
            Int16 iBEPLIST05 = 0;
            Int16 iBEPLIST06 = 0;
            Int16 iBEPLIST07 = 0;
            Int16 iBEPLIST08 = 0;
            Int16 iBEPLIST09 = 0;
            Int16 iBEPLIST10 = 0;
            Int16 iBEPLIST11 = 0;
            Int16 iBEPLIST12 = 0;

            string sAEPPRIOR_ORG_CD = "";
            string sAEPPRIOR_ORG_CDNM = "";
            Int16 iAEPLIST01 = 0;
            Int16 iAEPLIST02 = 0;
            Int16 iAEPLIST03 = 0;
            Int16 iAEPLIST04 = 0;
            Int16 iAEPLIST05 = 0;
            Int16 iAEPLIST06 = 0;
            Int16 iAEPLIST07 = 0;
            Int16 iAEPLIST08 = 0;
            Int16 iAEPLIST09 = 0;
            Int16 iAEPLIST10 = 0;
            Int16 iAEPLIST11 = 0;
            Int16 iAEPLIST12 = 0;

            string sA5EPPRIOR_ORG_CD = "";
            string sA5EPPRIOR_ORG_CDNM = "";
            Int16 iA5EPLIST01 = 0;
            Int16 iA5EPLIST02 = 0;
            Int16 iA5EPLIST03 = 0;
            Int16 iA5EPLIST04 = 0;
            Int16 iA5EPLIST05 = 0;
            Int16 iA5EPLIST06 = 0;
            Int16 iA5EPLIST07 = 0;
            Int16 iA5EPLIST08 = 0;
            Int16 iA5EPLIST09 = 0;
            Int16 iA5EPLIST10 = 0;
            Int16 iA5EPLIST11 = 0;
            Int16 iA5EPLIST12 = 0;


            string sZ0EPPRIOR_ORG_CD = "";
            string sZ0EPPRIOR_ORG_CDNM = "";
            Int16 iZ0EPLIST01 = 0;

            string sZ1EPPRIOR_ORG_CD = "";
            string sZ1EPPRIOR_ORG_CDNM = "";
            Int16 iZ1EPLIST01 = 0;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_28V6H709", this.DTP01_EFYYMM.GetValue());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //사업부
                    string sSaupCode = dt.Rows[i]["KBBUSEO"].ToString().Substring(0, 2);
                    string sSABUN = dt.Rows[i]["KBSABUN"].ToString();
                    string sJKCD = dt.Rows[i]["KBJKCD"].ToString();
                    string sJCCD = dt.Rows[i]["KBJCCD"].ToString();

                    if (sSaupCode == "T0")
                    {
                        sTEPPRIOR_ORG_CD = "T00000";
                        sTEPPRIOR_ORG_CDNM = "UTT";
                        //임원
                        if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                        {
                            iTEPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iTEPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iTEPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iTEPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iTEPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iTEPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")  //사원
                        {
                            iTEPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iTEPLIST12 += 1;
                        }
                        //운영직
                        if (sJKCD == "3C" && sJCCD == "15")  //운영대리
                        {
                            iTEPLIST06 += 1;
                        }
                        if (sJKCD == "3C" && sJCCD == "16")  //운영주임
                        {
                            iTEPLIST08 += 1;
                        }
                        if (sJKCD == "3D" && sJCCD == "17")  //부주임
                        {
                            iTEPLIST09 += 1;
                        }
                        if (sJKCD == "3D" && sJCCD == "18")  //운영사원
                        {
                            iTEPLIST11 += 1;
                        }
                    }
                    else if (sSaupCode == "S0")
                    {
                        sSEPPRIOR_ORG_CD = "S00000";
                        sSEPPRIOR_ORG_CDNM = "SILO";
                        //임원
                        if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                        {
                            iSEPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iSEPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iSEPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iSEPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iSEPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iSEPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")  //사원
                        {
                            iSEPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iSEPLIST12 += 1;
                        }
                        //운영직
                        if (sJKCD == "3C" && sJCCD == "15")  //운영대리
                        {
                            iSEPLIST06 += 1;
                        }
                        if (sJKCD == "3C" && sJCCD == "16")  //운영주임
                        {
                            iSEPLIST08 += 1;
                        }
                        if (sJKCD == "3D" && sJCCD == "17")  //부주임
                        {
                            iSEPLIST09 += 1;
                        }
                        if (sJKCD == "3D" && sJCCD == "18")  //운영사원
                        {
                            iSEPLIST11 += 1;
                        }
                    }
                    else if (sSaupCode == "B0")
                    {
                        sBEPPRIOR_ORG_CD = "B00000";
                        sBEPPRIOR_ORG_CDNM = "무  역";
                        //임원
                        if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                        {
                            iBEPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iBEPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iBEPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iBEPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iBEPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iBEPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")  //사원
                        {
                            iBEPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iBEPLIST12 += 1;
                        }
                        //운영직
                        if (sJKCD == "3C" && sJCCD == "15")  //운영대리
                        {
                            iBEPLIST06 += 1;
                        }
                        if (sJKCD == "3C" && sJCCD == "16")  //운영주임
                        {
                            iBEPLIST08 += 1;
                        }
                        if (sJKCD == "3D" && sJCCD == "17")  //부주임
                        {
                            iBEPLIST09 += 1;
                        }
                        if (sJKCD == "3D" && sJCCD == "18")  //운영사원
                        {
                            iBEPLIST11 += 1;
                        }
                    }
                    else if (sSaupCode == "A1")
                    {
                        sAEPPRIOR_ORG_CD = "A10000";
                        sAEPPRIOR_ORG_CDNM = "경영지원";

                        //임원
                        if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                        {
                            iAEPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iAEPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iAEPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iAEPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iAEPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iAEPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")  //사원
                        {
                            iAEPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iAEPLIST12 += 1;
                        }
                        //운영직
                        if (sJKCD == "3C" && sJCCD == "15")  //운영대리
                        {
                            iAEPLIST06 += 1;
                        }
                        if (sJKCD == "3C" && sJCCD == "16")  //운영주임
                        {
                            iAEPLIST08 += 1;
                        }
                        if (sJKCD == "3D" && sJCCD == "17")  //부주임
                        {
                            iAEPLIST09 += 1;
                        }
                        if (sJKCD == "3D" && sJCCD == "18")  //운영사원
                        {
                            iAEPLIST11 += 1;
                        }
                    }
                    else if (sSaupCode == "A5" || sSaupCode == "C0")
                    {
                        sA5EPPRIOR_ORG_CD = "A50000";
                        sA5EPPRIOR_ORG_CDNM = "기획재무";
                        //임원
                        if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                        {
                            iA5EPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iA5EPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iA5EPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iA5EPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iA5EPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iA5EPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")  //사원
                        {
                            iA5EPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iA5EPLIST12 += 1;
                        }
                        //운영직
                        if (sJKCD == "3C" && sJCCD == "15")  //운영대리
                        {
                            iA5EPLIST06 += 1;
                        }
                        if (sJKCD == "3C" && sJCCD == "16")  //운영주임
                        {
                            iA5EPLIST08 += 1;
                        }
                        if (sJKCD == "3D" && sJCCD == "17")  //부주임
                        {
                            iA5EPLIST09 += 1;
                        }
                        if (sJKCD == "3D" && sJCCD == "18")  //운영사원
                        {
                            iA5EPLIST11 += 1;
                        }
                    }


                    //경영진
                    if (sJKCD == "01" && (sJCCD == "01" || sJCCD == "02" || sJCCD == "03" || sJCCD == "04"))
                    {
                        sZ0EPPRIOR_ORG_CD = "Z00000";
                        sZ0EPPRIOR_ORG_CDNM = "경영진";

                        iZ0EPLIST01 += 1;
                    }
                    //감사, 고문
                    if (sJKCD == "01" && (sJCCD == "80" || sJCCD == "90"))
                    {
                        if (sSABUN == "0016-M")
                        {
                            sZ0EPPRIOR_ORG_CD = "Z00000";
                            sZ0EPPRIOR_ORG_CDNM = "경영진";

                            iZ0EPLIST01 += 1;
                        }
                        else
                        {
                            sZ1EPPRIOR_ORG_CD = "Z10000";
                            sZ1EPPRIOR_ORG_CDNM = "비상근";

                            iZ1EPLIST01 += 1;
                        }
                    }


                }
            } //if (dt.Rows.Count > 0)...end

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_HR_2934W732", this.DTP01_EFYYMM.GetValue(), this.CBH01_EFSUBGN.GetValue());

            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_EFYYMM.GetValue(), this.CBH01_EFSUBGN.GetValue(), sTEPPRIOR_ORG_CD, sTEPPRIOR_ORG_CDNM, iTEPLIST01, iTEPLIST02, iTEPLIST03, iTEPLIST04,
                                                        iTEPLIST05, iTEPLIST06, iTEPLIST07, iTEPLIST08, iTEPLIST09, iTEPLIST10, iTEPLIST11, iTEPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_EFYYMM.GetValue(), this.CBH01_EFSUBGN.GetValue(), sSEPPRIOR_ORG_CD, sSEPPRIOR_ORG_CDNM, iSEPLIST01, iSEPLIST02, iSEPLIST03, iSEPLIST04,
                                                        iSEPLIST05, iSEPLIST06, iSEPLIST07, iSEPLIST08, iSEPLIST09, iSEPLIST10, iSEPLIST11, iSEPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_EFYYMM.GetValue(), this.CBH01_EFSUBGN.GetValue(), sBEPPRIOR_ORG_CD, sBEPPRIOR_ORG_CDNM, iBEPLIST01, iBEPLIST02, iBEPLIST03, iBEPLIST04,
                                                        iBEPLIST05, iBEPLIST06, iBEPLIST07, iBEPLIST08, iBEPLIST09, iBEPLIST10, iBEPLIST11, iBEPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_EFYYMM.GetValue(), this.CBH01_EFSUBGN.GetValue(), sAEPPRIOR_ORG_CD, sAEPPRIOR_ORG_CDNM, iAEPLIST01, iAEPLIST02, iAEPLIST03, iAEPLIST04,
                                                       iAEPLIST05, iAEPLIST06, iAEPLIST07, iAEPLIST08, iAEPLIST09, iAEPLIST10, iAEPLIST11, iAEPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_EFYYMM.GetValue(), this.CBH01_EFSUBGN.GetValue(), sA5EPPRIOR_ORG_CD, sA5EPPRIOR_ORG_CDNM, iA5EPLIST01, iA5EPLIST02, iA5EPLIST03, iA5EPLIST04,
                                                       iA5EPLIST05, iA5EPLIST06, iA5EPLIST07, iA5EPLIST08, iA5EPLIST09, iA5EPLIST10, iA5EPLIST11, iA5EPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_EFYYMM.GetValue(), this.CBH01_EFSUBGN.GetValue(), sZ0EPPRIOR_ORG_CD, sZ0EPPRIOR_ORG_CDNM, iZ0EPLIST01, 0, 0, 0,
                                                       0, 0, 0, 0, 0, 0, 0, 0, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_EFYYMM.GetValue(), this.CBH01_EFSUBGN.GetValue(), sZ1EPPRIOR_ORG_CD, sZ1EPPRIOR_ORG_CDNM, iZ1EPLIST01, 0, 0, 0,
                                                       0, 0, 0, 0, 0, 0, 0, 0, Employer.EmpNo);
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_26E30875");

            this.BTN61_INQ_Click(null, null);
        }

        private void UP_TH_ORGCD() //태영호라이즌
        {

            string sTEPPRIOR_ORG_CD = "";
            string sTEPPRIOR_ORG_CDNM = "";
            Int16 iTEPLIST01 = 0;
            Int16 iTEPLIST02 = 0;
            Int16 iTEPLIST03 = 0;
            Int16 iTEPLIST04 = 0;
            Int16 iTEPLIST05 = 0;
            Int16 iTEPLIST06 = 0;
            Int16 iTEPLIST07 = 0;
            Int16 iTEPLIST08 = 0;
            Int16 iTEPLIST09 = 0;
            Int16 iTEPLIST10 = 0;
            Int16 iTEPLIST11 = 0;
            Int16 iTEPLIST12 = 0;

            string sSEPPRIOR_ORG_CD = "";
            string sSEPPRIOR_ORG_CDNM = "";
            Int16 iSEPLIST01 = 0;
            Int16 iSEPLIST02 = 0;
            Int16 iSEPLIST03 = 0;
            Int16 iSEPLIST04 = 0;
            Int16 iSEPLIST05 = 0;
            Int16 iSEPLIST06 = 0;
            Int16 iSEPLIST07 = 0;
            Int16 iSEPLIST08 = 0;
            Int16 iSEPLIST09 = 0;
            Int16 iSEPLIST10 = 0;
            Int16 iSEPLIST11 = 0;
            Int16 iSEPLIST12 = 0;

            string sAEPPRIOR_ORG_CD = "";
            string sAEPPRIOR_ORG_CDNM = "";
            Int16 iAEPLIST01 = 0;
            Int16 iAEPLIST02 = 0;
            Int16 iAEPLIST03 = 0;
            Int16 iAEPLIST04 = 0;
            Int16 iAEPLIST05 = 0;
            Int16 iAEPLIST06 = 0;
            Int16 iAEPLIST07 = 0;
            Int16 iAEPLIST08 = 0;
            Int16 iAEPLIST09 = 0;
            Int16 iAEPLIST10 = 0;
            Int16 iAEPLIST11 = 0;
            Int16 iAEPLIST12 = 0;

            string sA5EPPRIOR_ORG_CD = "";
            string sA5EPPRIOR_ORG_CDNM = "";
            Int16 iA5EPLIST01 = 0;
            Int16 iA5EPLIST02 = 0;
            Int16 iA5EPLIST03 = 0;
            Int16 iA5EPLIST04 = 0;
            Int16 iA5EPLIST05 = 0;
            Int16 iA5EPLIST06 = 0;
            Int16 iA5EPLIST07 = 0;
            Int16 iA5EPLIST08 = 0;
            Int16 iA5EPLIST09 = 0;
            Int16 iA5EPLIST10 = 0;
            Int16 iA5EPLIST11 = 0;
            Int16 iA5EPLIST12 = 0;


            string sZ0EPPRIOR_ORG_CD = "";
            string sZ0EPPRIOR_ORG_CDNM = "";
            Int16 iZ0EPLIST01 = 0;

            string sZ1EPPRIOR_ORG_CD = "";
            string sZ1EPPRIOR_ORG_CDNM = "";
            Int16 iZ1EPLIST01 = 0;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_34F4H503", this.DTP01_EFYYMM.GetValue());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //사업부
                    string sSaupCode = dt.Rows[i]["KBBUSEO"].ToString().Substring(0, 6);
                    string sSABUN = dt.Rows[i]["KBSABUN"].ToString();
                    string sJKCD = dt.Rows[i]["KBJKCD"].ToString();
                    string sJCCD = dt.Rows[i]["KBJCCD"].ToString();

                    if (sSaupCode == "300000")
                    {
                        sTEPPRIOR_ORG_CD = "300000";
                        sTEPPRIOR_ORG_CDNM = "운영팀";
                        //임원
                        if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                        {
                            iTEPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iTEPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iTEPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iTEPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iTEPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iTEPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A" || sJKCD == "3C" || sJKCD == "3D")  //사원
                        {
                            iTEPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iTEPLIST12 += 1;
                        }

                    }
                    else if (sSaupCode == "303000")
                    {
                        sSEPPRIOR_ORG_CD = "303000";
                        sSEPPRIOR_ORG_CDNM = "영업팀";
                        //임원
                        if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                        {
                            iSEPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iSEPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iSEPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iSEPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iSEPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iSEPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A" || sJKCD == "3C" || sJKCD == "3D")  //사원
                        {
                            iSEPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iSEPLIST12 += 1;
                        }

                    }
                    else if (sSaupCode == "200000")
                    {
                        sAEPPRIOR_ORG_CD = "200000";
                        sAEPPRIOR_ORG_CDNM = "관리팀";

                        //임원
                        if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                        {
                            iAEPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iAEPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iAEPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iAEPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iAEPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iAEPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A" || sJKCD == "3C" || sJKCD == "3D")  //사원
                        {
                            iAEPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iAEPLIST12 += 1;
                        }
                    }
                    else if (sSaupCode == "304000")
                    {
                        sA5EPPRIOR_ORG_CD = "304000";
                        sA5EPPRIOR_ORG_CDNM = "공무안전팀";
                        //임원
                        if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                        {
                            iA5EPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iA5EPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iA5EPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iA5EPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iA5EPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iA5EPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A" || sJKCD == "3C" || sJKCD == "3D")  //사원
                        {
                            iA5EPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iA5EPLIST12 += 1;
                        }
                    }


                    //경영진
                    if (sJKCD == "01" && (sJCCD == "01" || sJCCD == "02" || sJCCD == "03" || sJCCD == "04"))
                    {
                        sZ0EPPRIOR_ORG_CD = "Z00000";
                        sZ0EPPRIOR_ORG_CDNM = "경영진";

                        iZ0EPLIST01 += 1;
                    }
                    //감사, 고문
                    if (sJKCD == "01" && (sJCCD == "80" || sJCCD == "90"))
                    {

                        sZ1EPPRIOR_ORG_CD = "Z10000";
                        sZ1EPPRIOR_ORG_CDNM = "비상근";

                        iZ1EPLIST01 += 1;
                    }


                }
            } //if (dt.Rows.Count > 0)...end

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_HR_2934W732", this.DTP01_EFYYMM.GetValue(), this.CBH01_EFSUBGN.GetValue());

            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_EFYYMM.GetValue(), this.CBH01_EFSUBGN.GetValue(), sTEPPRIOR_ORG_CD, sTEPPRIOR_ORG_CDNM, iTEPLIST01, iTEPLIST02, iTEPLIST03, iTEPLIST04,
                                                        iTEPLIST05, iTEPLIST06, iTEPLIST07, iTEPLIST08, iTEPLIST09, iTEPLIST10, iTEPLIST11, iTEPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_EFYYMM.GetValue(), this.CBH01_EFSUBGN.GetValue(), sSEPPRIOR_ORG_CD, sSEPPRIOR_ORG_CDNM, iSEPLIST01, iSEPLIST02, iSEPLIST03, iSEPLIST04,
                                                        iSEPLIST05, iSEPLIST06, iSEPLIST07, iSEPLIST08, iSEPLIST09, iSEPLIST10, iSEPLIST11, iSEPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_EFYYMM.GetValue(), this.CBH01_EFSUBGN.GetValue(), sAEPPRIOR_ORG_CD, sAEPPRIOR_ORG_CDNM, iAEPLIST01, iAEPLIST02, iAEPLIST03, iAEPLIST04,
                                                       iAEPLIST05, iAEPLIST06, iAEPLIST07, iAEPLIST08, iAEPLIST09, iAEPLIST10, iAEPLIST11, iAEPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_EFYYMM.GetValue(), this.CBH01_EFSUBGN.GetValue(), sA5EPPRIOR_ORG_CD, sA5EPPRIOR_ORG_CDNM, iA5EPLIST01, iA5EPLIST02, iA5EPLIST03, iA5EPLIST04,
                                                       iA5EPLIST05, iA5EPLIST06, iA5EPLIST07, iA5EPLIST08, iA5EPLIST09, iA5EPLIST10, iA5EPLIST11, iA5EPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_EFYYMM.GetValue(), this.CBH01_EFSUBGN.GetValue(), sZ0EPPRIOR_ORG_CD, sZ0EPPRIOR_ORG_CDNM, iZ0EPLIST01, 0, 0, 0,
                                                       0, 0, 0, 0, 0, 0, 0, 0, Employer.EmpNo);
            if (iZ1EPLIST01 > 0)
            {
                this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_EFYYMM.GetValue(), this.CBH01_EFSUBGN.GetValue(), sZ1EPPRIOR_ORG_CD, sZ1EPPRIOR_ORG_CDNM, iZ1EPLIST01, 0, 0, 0,
                                                           0, 0, 0, 0, 0, 0, 0, 0, Employer.EmpNo);
            }
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_26E30875");

            this.BTN61_INQ_Click(null, null);

        }

        private void UP_TS_ORGCD() //태영GLS
        {
            string sTEPPRIOR_ORG_CD = "";
            string sTEPPRIOR_ORG_CDNM = "";
            Int16 iTEPLIST01 = 0;
            Int16 iTEPLIST02 = 0;
            Int16 iTEPLIST03 = 0;
            Int16 iTEPLIST04 = 0;
            Int16 iTEPLIST05 = 0;
            Int16 iTEPLIST06 = 0;
            Int16 iTEPLIST07 = 0;
            Int16 iTEPLIST08 = 0;
            Int16 iTEPLIST09 = 0;
            Int16 iTEPLIST10 = 0;
            Int16 iTEPLIST11 = 0;
            Int16 iTEPLIST12 = 0;

            string sSEPPRIOR_ORG_CD = "";
            string sSEPPRIOR_ORG_CDNM = "";
            Int16 iSEPLIST01 = 0;
            Int16 iSEPLIST02 = 0;
            Int16 iSEPLIST03 = 0;
            Int16 iSEPLIST04 = 0;
            Int16 iSEPLIST05 = 0;
            Int16 iSEPLIST06 = 0;
            Int16 iSEPLIST07 = 0;
            Int16 iSEPLIST08 = 0;
            Int16 iSEPLIST09 = 0;
            Int16 iSEPLIST10 = 0;
            Int16 iSEPLIST11 = 0;
            Int16 iSEPLIST12 = 0;

            string sAEPPRIOR_ORG_CD = "";
            string sAEPPRIOR_ORG_CDNM = "";
            Int16 iAEPLIST01 = 0;
            Int16 iAEPLIST02 = 0;
            Int16 iAEPLIST03 = 0;
            Int16 iAEPLIST04 = 0;
            Int16 iAEPLIST05 = 0;
            Int16 iAEPLIST06 = 0;
            Int16 iAEPLIST07 = 0;
            Int16 iAEPLIST08 = 0;
            Int16 iAEPLIST09 = 0;
            Int16 iAEPLIST10 = 0;
            Int16 iAEPLIST11 = 0;
            Int16 iAEPLIST12 = 0;

            string sA5EPPRIOR_ORG_CD = "";
            string sA5EPPRIOR_ORG_CDNM = "";
            Int16 iA5EPLIST01 = 0;
            Int16 iA5EPLIST02 = 0;
            Int16 iA5EPLIST03 = 0;
            Int16 iA5EPLIST04 = 0;
            Int16 iA5EPLIST05 = 0;
            Int16 iA5EPLIST06 = 0;
            Int16 iA5EPLIST07 = 0;
            Int16 iA5EPLIST08 = 0;
            Int16 iA5EPLIST09 = 0;
            Int16 iA5EPLIST10 = 0;
            Int16 iA5EPLIST11 = 0;
            Int16 iA5EPLIST12 = 0;


            string sZ0EPPRIOR_ORG_CD = "";
            string sZ0EPPRIOR_ORG_CDNM = "";
            Int16 iZ0EPLIST01 = 0;

            string sZ1EPPRIOR_ORG_CD = "";
            string sZ1EPPRIOR_ORG_CDNM = "";
            Int16 iZ1EPLIST01 = 0;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_34F49502", this.DTP01_EFYYMM.GetValue());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //사업부
                    string sSaupCode = dt.Rows[i]["KBBUSEO"].ToString().Substring(0, 1);
                    string sSABUN = dt.Rows[i]["KBSABUN"].ToString();
                    string sJKCD = dt.Rows[i]["KBJKCD"].ToString();
                    string sJCCD = dt.Rows[i]["KBJCCD"].ToString();

                    if (sSaupCode == "3")
                    {
                        sTEPPRIOR_ORG_CD = "300000";
                        sTEPPRIOR_ORG_CDNM = "운영팀";
                        //임원
                        if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                        {
                            iTEPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iTEPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iTEPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iTEPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iTEPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iTEPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")  //사원
                        {
                            iTEPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iTEPLIST12 += 1;
                        }

                    }
                    else if (sSaupCode == "4")
                    {
                        sSEPPRIOR_ORG_CD = "400000";
                        sSEPPRIOR_ORG_CDNM = "영업팀";
                        //임원
                        if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                        {
                            iSEPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iSEPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iSEPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iSEPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iSEPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iSEPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")  //사원
                        {
                            iSEPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iSEPLIST12 += 1;
                        }

                    }
                    else if (sSaupCode == "2" || sSaupCode == "1")
                    {
                        sAEPPRIOR_ORG_CD = "200000";
                        sAEPPRIOR_ORG_CDNM = "관리팀";

                        //임원
                        if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                        {
                            iAEPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iAEPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iAEPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iAEPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iAEPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iAEPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")  //사원
                        {
                            iAEPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iAEPLIST12 += 1;
                        }
                    }


                    //경영진
                    if (sJKCD == "01" && (sJCCD == "01" || sJCCD == "02" || sJCCD == "03" || sJCCD == "04"))
                    {
                        sZ0EPPRIOR_ORG_CD = "Z00000";
                        sZ0EPPRIOR_ORG_CDNM = "경영진";

                        iZ0EPLIST01 += 1;
                    }
                    //감사, 고문
                    if (sJKCD == "01" && (sJCCD == "80" || sJCCD == "90"))
                    {

                        sZ1EPPRIOR_ORG_CD = "Z10000";
                        sZ1EPPRIOR_ORG_CDNM = "비상근";

                        iZ1EPLIST01 += 1;
                    }


                }
            } //if (dt.Rows.Count > 0)...end

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_HR_2934W732", this.DTP01_EFYYMM.GetValue(), this.CBH01_EFSUBGN.GetValue());

            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_EFYYMM.GetValue(), this.CBH01_EFSUBGN.GetValue(), sTEPPRIOR_ORG_CD, sTEPPRIOR_ORG_CDNM, iTEPLIST01, iTEPLIST02, iTEPLIST03, iTEPLIST04,
                                                        iTEPLIST05, iTEPLIST06, iTEPLIST07, iTEPLIST08, iTEPLIST09, iTEPLIST10, iTEPLIST11, iTEPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_EFYYMM.GetValue(), this.CBH01_EFSUBGN.GetValue(), sSEPPRIOR_ORG_CD, sSEPPRIOR_ORG_CDNM, iSEPLIST01, iSEPLIST02, iSEPLIST03, iSEPLIST04,
                                                        iSEPLIST05, iSEPLIST06, iSEPLIST07, iSEPLIST08, iSEPLIST09, iSEPLIST10, iSEPLIST11, iSEPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_EFYYMM.GetValue(), this.CBH01_EFSUBGN.GetValue(), sAEPPRIOR_ORG_CD, sAEPPRIOR_ORG_CDNM, iAEPLIST01, iAEPLIST02, iAEPLIST03, iAEPLIST04,
                                                       iAEPLIST05, iAEPLIST06, iAEPLIST07, iAEPLIST08, iAEPLIST09, iAEPLIST10, iAEPLIST11, iAEPLIST12, Employer.EmpNo);

            //this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_EFYYMM.GetValue(), this.CBH01_EFSUBGN.GetValue(), sA5EPPRIOR_ORG_CD, sA5EPPRIOR_ORG_CDNM, iA5EPLIST01, iA5EPLIST02, iA5EPLIST03, iA5EPLIST04,
            //                                           iA5EPLIST05, iA5EPLIST06, iA5EPLIST07, iA5EPLIST08, iA5EPLIST09, iA5EPLIST10, iA5EPLIST11, iA5EPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_EFYYMM.GetValue(), this.CBH01_EFSUBGN.GetValue(), sZ0EPPRIOR_ORG_CD, sZ0EPPRIOR_ORG_CDNM, iZ0EPLIST01, 0, 0, 0,
                                                       0, 0, 0, 0, 0, 0, 0, 0, Employer.EmpNo);
            if (iZ1EPLIST01 > 0)
            {
                this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_EFYYMM.GetValue(), this.CBH01_EFSUBGN.GetValue(), sZ1EPPRIOR_ORG_CD, sZ1EPPRIOR_ORG_CDNM, iZ1EPLIST01, 0, 0, 0,
                                                           0, 0, 0, 0, 0, 0, 0, 0, Employer.EmpNo);
            }
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_26E30875");

            this.BTN61_INQ_Click(null, null);



        }

        private void UP_TG_ORGCD() //태영그레인
        {

            string sTEPPRIOR_ORG_CD = "";
            string sTEPPRIOR_ORG_CDNM = "";
            Int16 iTEPLIST01 = 0;
            Int16 iTEPLIST02 = 0;
            Int16 iTEPLIST03 = 0;
            Int16 iTEPLIST04 = 0;
            Int16 iTEPLIST05 = 0;
            Int16 iTEPLIST06 = 0;
            Int16 iTEPLIST07 = 0;
            Int16 iTEPLIST08 = 0;
            Int16 iTEPLIST09 = 0;
            Int16 iTEPLIST10 = 0;
            Int16 iTEPLIST11 = 0;
            Int16 iTEPLIST12 = 0;

            string sSEPPRIOR_ORG_CD = "";
            string sSEPPRIOR_ORG_CDNM = "";
            Int16 iSEPLIST01 = 0;
            Int16 iSEPLIST02 = 0;
            Int16 iSEPLIST03 = 0;
            Int16 iSEPLIST04 = 0;
            Int16 iSEPLIST05 = 0;
            Int16 iSEPLIST06 = 0;
            Int16 iSEPLIST07 = 0;
            Int16 iSEPLIST08 = 0;
            Int16 iSEPLIST09 = 0;
            Int16 iSEPLIST10 = 0;
            Int16 iSEPLIST11 = 0;
            Int16 iSEPLIST12 = 0;

            string sBEPPRIOR_ORG_CD = "";
            string sBEPPRIOR_ORG_CDNM = "";
            Int16 iBEPLIST01 = 0;
            Int16 iBEPLIST02 = 0;
            Int16 iBEPLIST03 = 0;
            Int16 iBEPLIST04 = 0;
            Int16 iBEPLIST05 = 0;
            Int16 iBEPLIST06 = 0;
            Int16 iBEPLIST07 = 0;
            Int16 iBEPLIST08 = 0;
            Int16 iBEPLIST09 = 0;
            Int16 iBEPLIST10 = 0;
            Int16 iBEPLIST11 = 0;
            Int16 iBEPLIST12 = 0;

            string sAEPPRIOR_ORG_CD = "";
            string sAEPPRIOR_ORG_CDNM = "";
            Int16 iAEPLIST01 = 0;
            Int16 iAEPLIST02 = 0;
            Int16 iAEPLIST03 = 0;
            Int16 iAEPLIST04 = 0;
            Int16 iAEPLIST05 = 0;
            Int16 iAEPLIST06 = 0;
            Int16 iAEPLIST07 = 0;
            Int16 iAEPLIST08 = 0;
            Int16 iAEPLIST09 = 0;
            Int16 iAEPLIST10 = 0;
            Int16 iAEPLIST11 = 0;
            Int16 iAEPLIST12 = 0;

            string sA5EPPRIOR_ORG_CD = "";
            string sA5EPPRIOR_ORG_CDNM = "";
            Int16 iA5EPLIST01 = 0;
            Int16 iA5EPLIST02 = 0;
            Int16 iA5EPLIST03 = 0;
            Int16 iA5EPLIST04 = 0;
            Int16 iA5EPLIST05 = 0;
            Int16 iA5EPLIST06 = 0;
            Int16 iA5EPLIST07 = 0;
            Int16 iA5EPLIST08 = 0;
            Int16 iA5EPLIST09 = 0;
            Int16 iA5EPLIST10 = 0;
            Int16 iA5EPLIST11 = 0;
            Int16 iA5EPLIST12 = 0;


            string sZ0EPPRIOR_ORG_CD = "";
            string sZ0EPPRIOR_ORG_CDNM = "";
            Int16 iZ0EPLIST01 = 0;

            string sZ1EPPRIOR_ORG_CD = "";
            string sZ1EPPRIOR_ORG_CDNM = "";
            Int16 iZ1EPLIST01 = 0;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_34F34500", this.DTP01_EFYYMM.GetValue());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //사업부
                    string sSaupCode = dt.Rows[i]["KBBUSEO"].ToString().Substring(0, 1);
                    string sSABUN = dt.Rows[i]["KBSABUN"].ToString();
                    string sJKCD = dt.Rows[i]["KBJKCD"].ToString();
                    string sJCCD = dt.Rows[i]["KBJCCD"].ToString();

                    if (sSaupCode == "3")
                    {
                        sTEPPRIOR_ORG_CD = "300000";
                        sTEPPRIOR_ORG_CDNM = "운영부";
                        //임원
                        if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                        {
                            iTEPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iTEPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iTEPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iTEPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iTEPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iTEPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")  //사원
                        {
                            iTEPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iTEPLIST12 += 1;
                        }

                    }
                    else if (sSaupCode == "2")
                    {
                        sSEPPRIOR_ORG_CD = "200000";
                        sSEPPRIOR_ORG_CDNM = "영업부";
                        //임원
                        if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                        {
                            iSEPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iSEPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iSEPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iSEPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iSEPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iSEPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")  //사원
                        {
                            iSEPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iSEPLIST12 += 1;
                        }

                    }
                    else if (sSaupCode == "1")
                    {
                        sAEPPRIOR_ORG_CD = "100000";
                        sAEPPRIOR_ORG_CDNM = "관리부";

                        //임원
                        if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                        {
                            iAEPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iAEPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iAEPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iAEPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iAEPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iAEPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")  //사원
                        {
                            iAEPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iAEPLIST12 += 1;
                        }
                    }
                    else if (sSaupCode == "4")
                    {
                        sA5EPPRIOR_ORG_CD = "400000";
                        sA5EPPRIOR_ORG_CDNM = "안전관리부";
                        //임원
                        if (sJKCD == "01" && (sJCCD != "01" && sJCCD != "02" && sJCCD != "03" && sJCCD != "04" && sJCCD != "80" && sJCCD != "90"))
                        {
                            iA5EPLIST01 += 1;
                        }
                        if (sJKCD == "1A") //부장
                        {
                            iA5EPLIST02 += 1;
                        }
                        if (sJKCD == "1B") //차장
                        {
                            iA5EPLIST03 += 1;
                        }
                        if (sJKCD == "2A") //과장
                        {
                            iA5EPLIST04 += 1;
                        }
                        if (sJKCD == "2B") //대리
                        {
                            iA5EPLIST05 += 1;
                        }
                        if (sJKCD == "2C") //주임
                        {
                            iA5EPLIST07 += 1;
                        }
                        if (sJKCD == "3A" || sJKCD == "3B" || sJKCD == "4A")  //사원
                        {
                            iA5EPLIST10 += 1;
                        }
                        if (sJKCD == "6C")  //계약직
                        {
                            iA5EPLIST12 += 1;
                        }
                    }


                    //경영진
                    if (sJKCD == "01" && (sJCCD == "01" || sJCCD == "02" || sJCCD == "03" || sJCCD == "04"))
                    {
                        sZ0EPPRIOR_ORG_CD = "Z00000";
                        sZ0EPPRIOR_ORG_CDNM = "경영진";

                        iZ0EPLIST01 += 1;
                    }
                    //감사, 고문
                    if (sJKCD == "01" && (sJCCD == "80" || sJCCD == "90"))
                    {

                        sZ1EPPRIOR_ORG_CD = "Z10000";
                        sZ1EPPRIOR_ORG_CDNM = "비상근";

                        iZ1EPLIST01 += 1;
                    }


                }
            } //if (dt.Rows.Count > 0)...end

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_HR_2934W732", this.DTP01_EFYYMM.GetValue(), this.CBH01_EFSUBGN.GetValue());

            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_EFYYMM.GetValue(), this.CBH01_EFSUBGN.GetValue(), sTEPPRIOR_ORG_CD, sTEPPRIOR_ORG_CDNM, iTEPLIST01, iTEPLIST02, iTEPLIST03, iTEPLIST04,
                                                        iTEPLIST05, iTEPLIST06, iTEPLIST07, iTEPLIST08, iTEPLIST09, iTEPLIST10, iTEPLIST11, iTEPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_EFYYMM.GetValue(), this.CBH01_EFSUBGN.GetValue(), sSEPPRIOR_ORG_CD, sSEPPRIOR_ORG_CDNM, iSEPLIST01, iSEPLIST02, iSEPLIST03, iSEPLIST04,
                                                        iSEPLIST05, iSEPLIST06, iSEPLIST07, iSEPLIST08, iSEPLIST09, iSEPLIST10, iSEPLIST11, iSEPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_EFYYMM.GetValue(), this.CBH01_EFSUBGN.GetValue(), sAEPPRIOR_ORG_CD, sAEPPRIOR_ORG_CDNM, iAEPLIST01, iAEPLIST02, iAEPLIST03, iAEPLIST04,
                                                       iAEPLIST05, iAEPLIST06, iAEPLIST07, iAEPLIST08, iAEPLIST09, iAEPLIST10, iAEPLIST11, iAEPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_EFYYMM.GetValue(), this.CBH01_EFSUBGN.GetValue(), sA5EPPRIOR_ORG_CD, sA5EPPRIOR_ORG_CDNM, iA5EPLIST01, iA5EPLIST02, iA5EPLIST03, iA5EPLIST04,
                                                       iA5EPLIST05, iA5EPLIST06, iA5EPLIST07, iA5EPLIST08, iA5EPLIST09, iA5EPLIST10, iA5EPLIST11, iA5EPLIST12, Employer.EmpNo);
            this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_EFYYMM.GetValue(), this.CBH01_EFSUBGN.GetValue(), sZ0EPPRIOR_ORG_CD, sZ0EPPRIOR_ORG_CDNM, iZ0EPLIST01, 0, 0, 0,
                                                       0, 0, 0, 0, 0, 0, 0, 0, Employer.EmpNo);
            if (iZ1EPLIST01 > 0)
            {
                this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_EFYYMM.GetValue(), this.CBH01_EFSUBGN.GetValue(), sZ1EPPRIOR_ORG_CD, sZ1EPPRIOR_ORG_CDNM, iZ1EPLIST01, 0, 0, 0,
                                                           0, 0, 0, 0, 0, 0, 0, 0, Employer.EmpNo);
            }
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_26E30875");

            this.BTN61_INQ_Click(null, null);

        }


        private void UP_TL_ORGCD() //티와이스틸
        {
            string sTEPPRIOR_ORG_CD = "";
            string sTEPPRIOR_ORG_CDNM = "";

            Int16 iTEPLIST01 = 0;
            Int16 iTEPLIST02 = 0;
            Int16 iTEPLIST03 = 0;
            Int16 iTEPLIST04 = 0;
            Int16 iTEPLIST05 = 0;
            Int16 iTEPLIST06 = 0;
            Int16 iTEPLIST07 = 0;
            Int16 iTEPLIST08 = 0;
            Int16 iTEPLIST09 = 0;
            Int16 iTEPLIST10 = 0;
            Int16 iTEPLIST11 = 0;
            Int16 iTEPLIST12 = 0;
            

            if (this.FPS91_TY_S_AC_3B55E199.CurrentRowCount > 0)
            {
                iTEPLIST01 = 0;
                iTEPLIST02 = 0;
                iTEPLIST03 = 0;
                iTEPLIST04 = 0;
                iTEPLIST05 = 0;
                iTEPLIST06 = 0;
                iTEPLIST07 = 0;
                iTEPLIST08 = 0;
                iTEPLIST09 = 0;
                iTEPLIST10 = 0;
                iTEPLIST11 = 0;
                iTEPLIST12 = 0;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_2934W732", this.DTP01_EFYYMM.GetValue(), this.CBH01_EFSUBGN.GetValue());

                for (int i = 0; i < this.FPS91_TY_S_AC_3B55E199.CurrentRowCount-1; i++)
                {
                    //사업부
                    string sSaupCode = this.FPS91_TY_S_AC_3B55E199.GetValue(i, "EPPRIOR_ORG_CD").ToString();

                    if (sSaupCode.Substring(0,1)  == "2")
                    {
                        sTEPPRIOR_ORG_CD = "200000";
                        sTEPPRIOR_ORG_CDNM = "관리팀";
                    }
                    else if (sSaupCode.Substring(0, 1) == "4")
                    {
                        sTEPPRIOR_ORG_CD = "400000";
                        sTEPPRIOR_ORG_CDNM = "영업팀";
                    }
                    else if (sSaupCode.Substring(0, 1) == "5")
                    {
                        sTEPPRIOR_ORG_CD = "500000";
                        sTEPPRIOR_ORG_CDNM = "화학팀";
                    }
                    else
                    {
                        sTEPPRIOR_ORG_CD = "Z00000";
                        sTEPPRIOR_ORG_CDNM = "경영진";
                    }
  
                    //임원
                    iTEPLIST01 +=  Convert.ToInt16(this.FPS91_TY_S_AC_3B55E199.GetValue(i, "IMWON").ToString()); 
                        
                    //부장
                    iTEPLIST02 += Convert.ToInt16(this.FPS91_TY_S_AC_3B55E199.GetValue(i, "BUJANG").ToString()); 
                        
                    //차장
                    iTEPLIST03 += Convert.ToInt16(this.FPS91_TY_S_AC_3B55E199.GetValue(i, "CHAJANG").ToString()); 
                        
                    //과장
                    iTEPLIST04 += Convert.ToInt16(this.FPS91_TY_S_AC_3B55E199.GetValue(i, "GWAJANG").ToString()); 
                        
                    //대리
                    iTEPLIST05 += Convert.ToInt16(this.FPS91_TY_S_AC_3B55E199.GetValue(i, "DAELI").ToString()); 
                        
                    //주임
                    iTEPLIST07 += Convert.ToInt16(this.FPS91_TY_S_AC_3B55E199.GetValue(i, "JUIM").ToString()); 
                        
                    //사원
                    iTEPLIST10 += Convert.ToInt16(this.FPS91_TY_S_AC_3B55E199.GetValue(i, "SAWON").ToString());

                    this.DbConnector.Attach("TY_P_HR_2931H723", this.DTP01_EFYYMM.GetValue(), this.CBH01_EFSUBGN.GetValue(), sTEPPRIOR_ORG_CD, sTEPPRIOR_ORG_CDNM, iTEPLIST01, iTEPLIST02, iTEPLIST03, iTEPLIST04,
                                                                iTEPLIST05, iTEPLIST06, iTEPLIST07, iTEPLIST08, iTEPLIST09, iTEPLIST10, iTEPLIST11, iTEPLIST12, "");

                    iTEPLIST01 = 0;
                    iTEPLIST02 = 0;
                    iTEPLIST03 = 0;
                    iTEPLIST04 = 0;
                    iTEPLIST05 = 0;
                    iTEPLIST06 = 0;
                    iTEPLIST07 = 0;
                    iTEPLIST08 = 0;
                    iTEPLIST09 = 0;
                    iTEPLIST10 = 0;
                    iTEPLIST11 = 0;
                    iTEPLIST12 = 0;
                }

            } //if (dt.Rows.Count > 0)...end
            
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_26E30875");

            this.BTN61_INQ_Click(null, null);            
        }
        #endregion

        #region Description : EIS 계열사 최종 마감 월 조회
        private void UP_start_dsp_month()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_3C94Q662");
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count == 0)
            {
                this.DTP01_EFYYMM.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"));
            }
            else
            {
                this.DTP01_EFYYMM.SetValue(dt.Rows[0]["YYMM"].ToString());
            }
        }
        #endregion
    }
}