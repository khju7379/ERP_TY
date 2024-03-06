using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;
using FarPoint.Win.Spread.CellType;
using GrapeCity.ActiveReports;
using TY.ER.GB00;

namespace TY.ER.AC00
{
    /// <summary>
    /// 월상각 전표생성 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2013.06.10 10:00
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_23N3M888 : 계정 과목 코드 조회
    ///  TY_P_AC_29C7K957 : 미승인전표 임시파일 입력
    ///  TY_P_AC_29C7M958 : 자동순번 가져오기
    ///  TY_P_AC_29C7O959 : 미승인전표 SP호출 이력 저장
    ///  TY_P_AC_29C80960 : 미승인전표 SP 호출
    ///  TY_P_AC_29D5B004 : 전표호출 파라메타 파일 조회
    ///  TY_P_AC_29DA5966 : 미승인전표 임시파일 등록(TMAC1102)
    ///  TY_P_AC_2AU2M916 : 미승인 전표 출력
    ///  TY_P_AC_36A6F823 : 월상각 전표생성 조회
    ///  TY_P_AC_36BAS831 : 월상각전표 번호 세팅
    ///  TY_P_AC_36BAZ832 : 월상각 전표 대변 값가져오기
    ///  TY_P_AC_36BB0833 : 월상각 전표 차변 생성
    ///  TY_P_AC_36BBA834 : 월상각 전표 차변 값가져오기
    ///  TY_P_GB_24G9S659 : 사용자 부서 정보
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_36A6G825 : 월상각 전표생성 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_AC_25O8K620 : 전표처리가  완료되었습니다!
    ///  TY_M_GB_2BF7Y364 : 조회가 완료되었습니다.
    ///  INQ : 조회
    ///  PRYEAR : 년도
    /// </summary>
    public partial class TYACHF011S : TYBase
    {

        #region Description : 미승인 자료 처리 (TMAC1102F)
        private TYData DAT02_W2SSID;
        private TYData DAT02_W2DPMK;
        private TYData DAT02_W2DTMK;
        private TYData DAT02_W2NOSQ;
        private TYData DAT02_W2NOLN;
        private TYData DAT02_W2IDJP;
        private TYData DAT02_W2NOJP;
        private TYData DAT02_W2CDAC;
        private TYData DAT02_W2DTAC;
        private TYData DAT02_W2DTLI;
        private TYData DAT02_W2DPAC;
        private TYData DAT02_W2CDMI1;
        private TYData DAT02_W2VLMI1;
        private TYData DAT02_W2CDMI2;
        private TYData DAT02_W2VLMI2;
        private TYData DAT02_W2CDMI3;
        private TYData DAT02_W2VLMI3;
        private TYData DAT02_W2CDMI4;
        private TYData DAT02_W2VLMI4;
        private TYData DAT02_W2CDMI5;
        private TYData DAT02_W2VLMI5;
        private TYData DAT02_W2CDMI6;
        private TYData DAT02_W2VLMI6;
        private TYData DAT02_W2AMDR;
        private TYData DAT02_W2AMCR;
        private TYData DAT02_W2CDFD;
        private TYData DAT02_W2AMFD;
        private TYData DAT02_W2RKAC;
        private TYData DAT02_W2RKCU;
        private TYData DAT02_W2WCJP;
        private TYData DAT02_W2PRGB;
        private TYData DAT02_W2HIGB;
        //private TYData DAT02_W2HIDAT;
        //private TYData DAT02_W2HITIM;
        private TYData DAT02_W2HISAB;
        private TYData DAT02_W2GUBUN;
        private TYData DAT02_W2TXAMT;
        private TYData DAT02_W2TXVAT;
        private TYData DAT02_W2HWAJU;
        #endregion

        public TYACHF011S()
        {
            InitializeComponent();

            #region Description : 미승인 자료 처리
            this.DAT02_W2SSID = new TYData("DAT02_W2SSID", null);
            this.DAT02_W2DPMK = new TYData("DAT02_W2DPMK", null);
            this.DAT02_W2DTMK = new TYData("DAT02_W2DTMK", null);
            this.DAT02_W2NOSQ = new TYData("DAT02_W2NOSQ", null);
            this.DAT02_W2NOLN = new TYData("DAT02_W2NOLN", null);
            this.DAT02_W2IDJP = new TYData("DAT02_W2IDJP", null);
            this.DAT02_W2NOJP = new TYData("DAT02_W2NOJP", null);
            this.DAT02_W2CDAC = new TYData("DAT02_W2CDAC", null);
            this.DAT02_W2DTAC = new TYData("DAT02_W2DTAC", null);
            this.DAT02_W2DTLI = new TYData("DAT02_W2DTLI", null);
            this.DAT02_W2DPAC = new TYData("DAT02_W2DPAC", null);
            this.DAT02_W2CDMI1 = new TYData("DAT02_W2CDMI1", null);
            this.DAT02_W2VLMI1 = new TYData("DAT02_W2VLMI1", null);
            this.DAT02_W2CDMI2 = new TYData("DAT02_W2CDMI2", null);
            this.DAT02_W2VLMI2 = new TYData("DAT02_W2VLMI2", null);
            this.DAT02_W2CDMI3 = new TYData("DAT02_W2CDMI3", null);
            this.DAT02_W2VLMI3 = new TYData("DAT02_W2VLMI3", null);
            this.DAT02_W2CDMI4 = new TYData("DAT02_W2CDMI4", null);
            this.DAT02_W2VLMI4 = new TYData("DAT02_W2VLMI4", null);
            this.DAT02_W2CDMI5 = new TYData("DAT02_W2CDMI5", null);
            this.DAT02_W2VLMI5 = new TYData("DAT02_W2VLMI5", null);
            this.DAT02_W2CDMI6 = new TYData("DAT02_W2CDMI6", null);
            this.DAT02_W2VLMI6 = new TYData("DAT02_W2VLMI6", null);
            this.DAT02_W2AMDR = new TYData("DAT02_W2AMDR", null);
            this.DAT02_W2AMCR = new TYData("DAT02_W2AMCR", null);
            this.DAT02_W2CDFD = new TYData("DAT02_W2CDFD", null);
            this.DAT02_W2AMFD = new TYData("DAT02_W2AMFD", null);
            this.DAT02_W2RKAC = new TYData("DAT02_W2RKAC", null);
            this.DAT02_W2RKCU = new TYData("DAT02_W2RKCU", null);
            this.DAT02_W2WCJP = new TYData("DAT02_W2WCJP", null);
            this.DAT02_W2PRGB = new TYData("DAT02_W2PRGB", null);
            this.DAT02_W2HIGB = new TYData("DAT02_W2HIGB", null);
            //this.DAT02_W2HIDAT = new TYData("DAT02_W2HIDAT", null);
            //this.DAT02_W2HITIM = new TYData("DAT02_W2HITIM", null);
            this.DAT02_W2HISAB = new TYData("DAT02_W2HISAB", null);
            this.DAT02_W2GUBUN = new TYData("DAT02_W2GUBUN", null);
            this.DAT02_W2TXAMT = new TYData("DAT02_W2TXAMT", null);
            this.DAT02_W2TXVAT = new TYData("DAT02_W2TXVAT", null);
            this.DAT02_W2HWAJU = new TYData("DAT02_W2HWAJU", null);
            #endregion
        }

        #region Description : Page_Load
        private void TYACHF011S_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_AC_36A6G825.Initialize();

            #region Description :미승인 처리
            this.ControlFactory.Add(this.DAT02_W2SSID);
            this.ControlFactory.Add(this.DAT02_W2DPMK);
            this.ControlFactory.Add(this.DAT02_W2DTMK);
            this.ControlFactory.Add(this.DAT02_W2NOSQ);
            this.ControlFactory.Add(this.DAT02_W2NOLN);
            this.ControlFactory.Add(this.DAT02_W2IDJP);
            this.ControlFactory.Add(this.DAT02_W2NOJP);
            this.ControlFactory.Add(this.DAT02_W2CDAC);
            this.ControlFactory.Add(this.DAT02_W2DTAC);
            this.ControlFactory.Add(this.DAT02_W2DTLI);
            this.ControlFactory.Add(this.DAT02_W2DPAC);
            this.ControlFactory.Add(this.DAT02_W2CDMI1);
            this.ControlFactory.Add(this.DAT02_W2VLMI1);
            this.ControlFactory.Add(this.DAT02_W2CDMI2);
            this.ControlFactory.Add(this.DAT02_W2VLMI2);
            this.ControlFactory.Add(this.DAT02_W2CDMI3);
            this.ControlFactory.Add(this.DAT02_W2VLMI3);
            this.ControlFactory.Add(this.DAT02_W2CDMI4);
            this.ControlFactory.Add(this.DAT02_W2VLMI4);
            this.ControlFactory.Add(this.DAT02_W2CDMI5);
            this.ControlFactory.Add(this.DAT02_W2VLMI5);
            this.ControlFactory.Add(this.DAT02_W2CDMI6);
            this.ControlFactory.Add(this.DAT02_W2VLMI6);
            this.ControlFactory.Add(this.DAT02_W2AMDR);
            this.ControlFactory.Add(this.DAT02_W2AMCR);
            this.ControlFactory.Add(this.DAT02_W2CDFD);
            this.ControlFactory.Add(this.DAT02_W2AMFD);
            this.ControlFactory.Add(this.DAT02_W2RKAC);
            this.ControlFactory.Add(this.DAT02_W2RKCU);
            this.ControlFactory.Add(this.DAT02_W2WCJP);
            this.ControlFactory.Add(this.DAT02_W2PRGB);
            this.ControlFactory.Add(this.DAT02_W2HIGB);
            //this.ControlFactory.Add(this.DAT02_W2HIDAT);
            //this.ControlFactory.Add(this.DAT02_W2HITIM);
            this.ControlFactory.Add(this.DAT02_W2HISAB);
            this.ControlFactory.Add(this.DAT02_W2GUBUN);
            this.ControlFactory.Add(this.DAT02_W2TXAMT);
            this.ControlFactory.Add(this.DAT02_W2TXVAT);
            this.ControlFactory.Add(this.DAT02_W2HWAJU);
            #endregion

            SetStartingFocus(this.TXT01_PRYEAR);

            //this.BTN61_INQ_Click(null, null);
        } 
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_36A6G825.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_36A6F823", this.TXT01_PRYEAR.GetValue().ToString().Trim());
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_36A6G825.SetValue(dt);

                for (int i = 0; i < this.FPS91_TY_S_AC_36A6G825.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_36A6G825.GetValue(i, "FXMREPJPNO").ToString() == "")
                    {
                        this.FPS91_TY_S_AC_36A6G825_Sheet1.Cells[i, 4].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                        //this.FPS91_TY_S_AC_36A6G825_Sheet1.Cells[i, 3].Text = "전표취소";
                    }
                    else
                    {
                        //this.FPS91_TY_S_AC_36A6G825_Sheet1.Cells[i, 4].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                        //this.FPS91_TY_S_AC_36A6G825_Sheet1.Cells[i, 3].Text = "전표생성";
                    }
                }

                this.ShowMessage("TY_M_GB_2BF7Y364");
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        } 
        #endregion

        #region Description :전표 처리 버턴
        private void FPS91_TY_S_AC_36A6G825_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {

            string sB2SSID = "";
            string sW2RKAC = "";

            string sW2DPMK = string.Empty;
            string sW2DPAC = string.Empty;
            string sW2CDAC_1 = string.Empty;
            string sW2CDAC_2 = string.Empty;

            string sFXMYYMM = string.Empty;
            string sDATE = string.Empty;
            string sYear = string.Empty;
            string sMonth = string.Empty;
            int iDD = 0;
            string sPRDATE = string.Empty;
            string sMSGYYMM = string.Empty;

            int iCnt = 0;

            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

            DataTable dt = new DataTable();

            //BATID번호 부여
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7M958");
            decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());
            sB2SSID = this.IPAdresss + Employer.EmpNo + dAutoSeq.ToString();

            //사번 조회--> 생성부서
            this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_GB_24G9S659", Employer.EmpNo.ToString().Trim());  //INKIBNMF
            this.DbConnector.Attach("TY_P_GB_4CVJ7024",DateTime.Now.ToString("yyyyMMdd"), Employer.EmpNo.ToString().Trim());  //INKIBNMF
            DataTable dt_sabun = this.DbConnector.ExecuteDataTable();
            if (dt_sabun.Rows.Count != 0)
            { sW2DPMK = dt_sabun.Rows[0]["KBBUSEO"].ToString().Trim(); }

            if (e.Column.ToString() == "3")
            {
                sFXMYYMM = this.FPS91_TY_S_AC_36A6G825.GetValue("FXMYYMM").ToString();

                if (this.FPS91_TY_S_AC_36A6G825.GetValue("FXMREPJPNO").ToString() != "")
                {
                    string sB2DPMK = this.FPS91_TY_S_AC_36A6G825.GetValue("FXMREPJPNO").ToString().Substring(0, 6);
                    string sB2DTMK = this.FPS91_TY_S_AC_36A6G825.GetValue("FXMREPJPNO").ToString().Substring(7, 8);
                    string sB2NOSQ = this.FPS91_TY_S_AC_36A6G825.GetValue("FXMREPJPNO").ToString().Substring(16, 3);

                    #region Description : 전표 취소

                    //미승인전표 -> 임시파일 입력 (전표삭제)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_29C7K957", sB2SSID, sB2DPMK, sB2DTMK, sB2NOSQ);
                    //this.DbConnector.ExecuteTranQueryList();
                    ////미승인 SP호출 파일 입력
                    //this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, Employer.EmpNo, "D",
                                                     sW2DPMK, sB2DTMK, "", "",
                                                     "", "", Employer.EmpNo);
                    this.DbConnector.ExecuteTranQueryList();

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_29C80960", "A", sB2SSID, ""); // SP CALL
                    string sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar()); // SP의 OUTPUT 값 가져오는 부분
                    if (sOUTMSG.Substring(0, 2) == "ER")
                    {
                        this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                    else
                    {
                        this.DbConnector.CommandClear(); // 전표번호 세팅 
                        this.DbConnector.Attach("TY_P_AC_36BAS831", "", sB2DTMK.Substring(0, 6));
                        this.DbConnector.ExecuteTranQueryList();

                        this.ShowMessage("TY_M_AC_25O8K620");

                        this.BTN61_INQ_Click(null, null);
                    }

                    #endregion
                }
                else
                {
                    #region Description : 전표생성
                    sYear = sFXMYYMM.ToString().Substring(0, 4);
                    sMonth = sFXMYYMM.ToString().Substring(4, 2);

                    sMSGYYMM = sYear + "년" + Set_Fill2(sMonth) + "월 ";

                    // 해당월 마지막 일자 가져오기
                    iDD = DateTime.DaysInMonth(int.Parse(sYear.ToString()), int.Parse(sMonth.ToString()));
                    // 기준년월
                    sPRDATE = sFXMYYMM.ToString().Trim() + Set_Fill2(Convert.ToString(iDD));

                    this.DbConnector.CommandClear(); // 생성 (ACFIXIDVSF)
                    this.DbConnector.Attach("TY_P_AC_36BB0833", sFXMYYMM, Employer.EmpNo.ToString().Trim(), ""); // 월상각 차변 자료 생성(년월,사번,OUTMSG)
                    string sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar()); // SP의 OUTPUT 값 가져오는 부분
                    if (sOUTMSG.Substring(0, 2) == "ER")
                    {
                        this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }

                    if (sOUTMSG.Substring(0, 2) == "OK")
                    {
                        // ------------------------------------ Line 번호 : 1  (차변 생성)
                        #region Description : 차변

                        DataSet ds = new DataSet();
                        this.DbConnector.CommandClear(); // ACFIXIDVSF
                        this.DbConnector.Attach("TY_P_AC_36BBA834", sFXMYYMM); // 차변 값가져오기 

                        ds = this.DbConnector.ExecuteDataSet();
                        if (ds.Tables[0].Rows.Count != 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                iCnt = iCnt + 1;

                                dt.Clear();

                                if (ds.Tables[0].Rows[i]["ACFSDPAC"].ToString().Trim() == "T10000" || ds.Tables[0].Rows[i]["ACFSDPAC"].ToString().Trim() == "S10000")
                                {
                                    sW2CDAC_1 = "42411700";

                                    if (ds.Tables[0].Rows[i]["ACFSDPAC"].ToString().Trim() == "T10000")
                                    {
                                        sW2RKAC = sMSGYYMM + "유형자산상각(UTT)-월차결산";
                                    }
                                    else
                                    {
                                        sW2RKAC = sMSGYYMM + "유형자산상각(SILO)-월차결산";
                                    };


                                }
                                else
                                    if (ds.Tables[0].Rows[i]["ACFSDPAC"].ToString().Trim().Substring(0, 1) == "B")
                                    {
                                        sW2CDAC_1 = "44121700";
                                        if (ds.Tables[0].Rows[i]["ACFSDPAC"].ToString().Trim() == "B80100")
                                        {
                                            sW2RKAC = sMSGYYMM + "유형자산상각(석유화학)-월차결산";
                                        }
                                        else
                                        {
                                            sW2RKAC = sMSGYYMM + "유형자산상각(농업자원)-월차결산";
                                        };
                                    }
                                    else
                                        if (ds.Tables[0].Rows[i]["ACFSDPAC"].ToString().Trim().Substring(0, 1) == "A")
                                        {
                                            sW2CDAC_1 = "44211700";
                                            if (ds.Tables[0].Rows[i]["ACFSDPAC"].ToString().Trim() == "A10000")
                                            {
                                                sW2RKAC = sMSGYYMM + "유형자산상각(경영지원)-월차결산";
                                            }
                                            else
                                            {
                                                sW2RKAC = sMSGYYMM + "유형자산상각(기획재무)-월차결산";
                                            };
                                        };

                                sW2DPAC = ds.Tables[0].Rows[i]["ACFSDPAC"].ToString().Trim();

                                this.DAT02_W2SSID.SetValue(sB2SSID);
                                this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                                this.DAT02_W2DTMK.SetValue(sPRDATE); // 작성일자
                                this.DAT02_W2NOSQ.SetValue("0");
                                this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                                this.DAT02_W2IDJP.SetValue("3");
                                this.DAT02_W2NOJP.SetValue("");
                                this.DAT02_W2CDAC.SetValue(sW2CDAC_1);
                                this.DAT02_W2DTAC.SetValue("");
                                this.DAT02_W2DTLI.SetValue("");
                                this.DAT02_W2DPAC.SetValue(sW2DPAC); // 귀속부서

                                //관리항목 
                                this.DbConnector.CommandClear();
                                this.DbConnector.Attach("TY_P_AC_23N3M888", sW2CDAC_1, "");
                                dt = this.DbConnector.ExecuteDataTable();
                                if (dt.Rows.Count > 0)
                                {
                                    if (dt.Rows[0]["A1CDMI1"].ToString().Length > 0)
                                    { this.DAT02_W2CDMI1.SetValue(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2)); }
                                    else
                                    { this.DAT02_W2CDMI1.SetValue(""); }
                                    if (dt.Rows[0]["A1CDMI2"].ToString().Length > 0)
                                    { this.DAT02_W2CDMI2.SetValue(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2)); }
                                    else
                                    { this.DAT02_W2CDMI2.SetValue(""); }
                                    if (dt.Rows[0]["A1CDMI3"].ToString().Length > 0)
                                    { this.DAT02_W2CDMI3.SetValue(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2)); }
                                    else
                                    { this.DAT02_W2CDMI3.SetValue(""); }
                                    if (dt.Rows[0]["A1CDMI4"].ToString().Length > 0)
                                    { this.DAT02_W2CDMI4.SetValue(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2)); }
                                    else
                                    { this.DAT02_W2CDMI4.SetValue(""); }
                                    if (dt.Rows[0]["A1CDMI5"].ToString().Length > 0)
                                    { this.DAT02_W2CDMI5.SetValue(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2)); }
                                    else
                                    { this.DAT02_W2CDMI5.SetValue(""); }
                                    if (dt.Rows[0]["A1CDMI6"].ToString().Length > 0)
                                    { this.DAT02_W2CDMI6.SetValue(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2)); }
                                    else
                                    { this.DAT02_W2CDMI6.SetValue(""); }
                                }

                                this.DAT02_W2VLMI1.SetValue("");
                                this.DAT02_W2VLMI2.SetValue("");
                                this.DAT02_W2VLMI3.SetValue("");
                                this.DAT02_W2VLMI4.SetValue("");
                                this.DAT02_W2VLMI5.SetValue("");
                                this.DAT02_W2VLMI6.SetValue("");


                                this.DAT02_W2AMDR.SetValue(ds.Tables[0].Rows[i]["ACFSYAMT"].ToString().Trim()); // 차변금액 
                                this.DAT02_W2AMCR.SetValue("0");// 대변금액 

                                this.DAT02_W2CDFD.SetValue("");
                                this.DAT02_W2AMFD.SetValue("0");
                                this.DAT02_W2RKAC.SetValue(sW2RKAC);
                                this.DAT02_W2RKCU.SetValue("");
                                this.DAT02_W2WCJP.SetValue("");
                                this.DAT02_W2PRGB.SetValue("");
                                this.DAT02_W2HIGB.SetValue("A");
                                this.DAT02_W2HISAB.SetValue(Employer.EmpNo);
                                this.DAT02_W2GUBUN.SetValue("");
                                this.DAT02_W2TXAMT.SetValue("0");
                                this.DAT02_W2TXVAT.SetValue("0");
                                this.DAT02_W2HWAJU.SetValue("");

                                datas.Add(new object[] {this.DAT02_W2SSID.GetValue().ToString(),
                                        this.DAT02_W2DPMK.GetValue().ToString(),
                                        this.DAT02_W2DTMK.GetValue().ToString(),
                                        this.DAT02_W2NOSQ.GetValue().ToString(),
                                        this.DAT02_W2NOLN.GetValue().ToString(),
                                        this.DAT02_W2IDJP.GetValue().ToString(),
                                        this.DAT02_W2NOJP.GetValue().ToString(),
                                        this.DAT02_W2CDAC.GetValue().ToString(),
                                        this.DAT02_W2DTAC.GetValue().ToString(),
                                        this.DAT02_W2DTLI.GetValue().ToString(),
                                        this.DAT02_W2DPAC.GetValue().ToString(),
                                        this.DAT02_W2CDMI1.GetValue().ToString(),
                                        this.DAT02_W2VLMI1.GetValue().ToString(),
                                        this.DAT02_W2CDMI2.GetValue().ToString(),
                                        this.DAT02_W2VLMI2.GetValue().ToString(),
                                        this.DAT02_W2CDMI3.GetValue().ToString(),
                                        this.DAT02_W2VLMI3.GetValue().ToString(),
                                        this.DAT02_W2CDMI4.GetValue().ToString(),
                                        this.DAT02_W2VLMI4.GetValue().ToString(),
                                        this.DAT02_W2CDMI5.GetValue().ToString(),
                                        this.DAT02_W2VLMI5.GetValue().ToString(),
                                        this.DAT02_W2CDMI6.GetValue().ToString(),
                                        this.DAT02_W2VLMI6.GetValue().ToString(),
                                        this.DAT02_W2AMDR.GetValue().ToString(),
                                        this.DAT02_W2AMCR.GetValue().ToString(),
                                        this.DAT02_W2CDFD.GetValue().ToString(),
                                        this.DAT02_W2AMFD.GetValue().ToString(),
                                        this.DAT02_W2RKAC.GetValue().ToString(),
                                        this.DAT02_W2RKCU.GetValue().ToString(),
                                        this.DAT02_W2WCJP.GetValue().ToString(),
                                        this.DAT02_W2PRGB.GetValue().ToString(),
                                        this.DAT02_W2HIGB.GetValue().ToString(),
                                        this.DAT02_W2HISAB.GetValue().ToString(),
                                        this.DAT02_W2GUBUN.GetValue().ToString(),
                                        this.DAT02_W2TXAMT.GetValue().ToString(),
                                        this.DAT02_W2TXVAT.GetValue().ToString(),
                                        this.DAT02_W2HWAJU.GetValue().ToString()});
                            }

                        }
                        #endregion

                        // ------------------------------------ Line 번호 : 1  (대변 생성)
                        #region Description : 대변
                        DataSet ds_cr = new DataSet();
                        this.DbConnector.CommandClear(); // ACFIXREPAYMMF
                        this.DbConnector.Attach("TY_P_AC_36BAZ832", sFXMYYMM); // 대변 값가져오기 

                        ds_cr = this.DbConnector.ExecuteDataSet();
                        if (ds_cr.Tables[0].Rows.Count != 0)
                        {
                            for (int i = 0; i < ds_cr.Tables[0].Rows.Count; i++)
                            {
                                iCnt = iCnt + 1;

                                dt.Clear();

                                sW2CDAC_2 = ds_cr.Tables[0].Rows[i]["CDAC"].ToString().Trim();

                                this.DAT02_W2SSID.SetValue(sB2SSID);
                                this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                                this.DAT02_W2DTMK.SetValue(sPRDATE); // 작성일자
                                this.DAT02_W2NOSQ.SetValue("0");
                                this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                                this.DAT02_W2IDJP.SetValue("3");
                                this.DAT02_W2NOJP.SetValue("");
                                this.DAT02_W2CDAC.SetValue(sW2CDAC_2);
                                this.DAT02_W2DTAC.SetValue("");
                                this.DAT02_W2DTLI.SetValue("");
                                this.DAT02_W2DPAC.SetValue(sW2DPMK); // 귀속부서

                                //관리항목 
                                this.DbConnector.CommandClear();
                                this.DbConnector.Attach("TY_P_AC_23N3M888", sW2CDAC_2, "");
                                dt = this.DbConnector.ExecuteDataTable();
                                if (dt.Rows.Count > 0)
                                {
                                    if (dt.Rows[0]["A1CDMI1"].ToString().Length > 0)
                                    { this.DAT02_W2CDMI1.SetValue(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2)); }
                                    else
                                    { this.DAT02_W2CDMI1.SetValue(""); }
                                    if (dt.Rows[0]["A1CDMI2"].ToString().Length > 0)
                                    { this.DAT02_W2CDMI2.SetValue(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2)); }
                                    else
                                    { this.DAT02_W2CDMI2.SetValue(""); }
                                    if (dt.Rows[0]["A1CDMI3"].ToString().Length > 0)
                                    { this.DAT02_W2CDMI3.SetValue(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2)); }
                                    else
                                    { this.DAT02_W2CDMI3.SetValue(""); }
                                    if (dt.Rows[0]["A1CDMI4"].ToString().Length > 0)
                                    { this.DAT02_W2CDMI4.SetValue(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2)); }
                                    else
                                    { this.DAT02_W2CDMI4.SetValue(""); }
                                    if (dt.Rows[0]["A1CDMI5"].ToString().Length > 0)
                                    { this.DAT02_W2CDMI5.SetValue(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2)); }
                                    else
                                    { this.DAT02_W2CDMI5.SetValue(""); }
                                    if (dt.Rows[0]["A1CDMI6"].ToString().Length > 0)
                                    { this.DAT02_W2CDMI6.SetValue(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2)); }
                                    else
                                    { this.DAT02_W2CDMI6.SetValue(""); }
                                }

                                this.DAT02_W2VLMI1.SetValue("");
                                this.DAT02_W2VLMI2.SetValue("");
                                this.DAT02_W2VLMI3.SetValue("");
                                this.DAT02_W2VLMI4.SetValue("");
                                this.DAT02_W2VLMI5.SetValue("");
                                this.DAT02_W2VLMI6.SetValue("");

                                if (sW2CDAC_2 == "12200299")
                                {
                                    sW2RKAC = sMSGYYMM + "건물유형자산상각-월차결산";
                                }
                                else
                                if (sW2CDAC_2 == "12200399")
                                {
                                    sW2RKAC = sMSGYYMM + "구축물유형자산상각-월차결산";
                                }
                                else
                                if (sW2CDAC_2 == "12200499")
                                {
                                    sW2RKAC = sMSGYYMM + "기계장치유형자산상각-월차결산";
                                }
                                else
                                if (sW2CDAC_2 == "12200599")
                                {
                                    sW2RKAC = sMSGYYMM + "중기유형자산상각-월차결산";
                                }
                                else
                                if (sW2CDAC_2 == "12200699")
                                {
                                    sW2RKAC = sMSGYYMM + "차량운반구유형자산상각-월차결산";
                                }
                                else
                                if (sW2CDAC_2 == "12200799")
                                {
                                    sW2RKAC = sMSGYYMM + "공구와기구유형자산상각-월차결산";
                                }
                                else
                                if (sW2CDAC_2 == "12200899")
                                {
                                    sW2RKAC = sMSGYYMM + "비품유형자산상각-월차결산";
                                }
                                else
                                {
                                    sW2RKAC = sMSGYYMM + "유형자산상각-월차결산";
                                };



                                this.DAT02_W2AMDR.SetValue("0"); // 차변금액 
                                this.DAT02_W2AMCR.SetValue(ds_cr.Tables[0].Rows[i]["SUMAMT"].ToString().Trim());// 대변금액 

                                this.DAT02_W2CDFD.SetValue("");
                                this.DAT02_W2AMFD.SetValue("0");
                                this.DAT02_W2RKAC.SetValue(sW2RKAC);
                                this.DAT02_W2RKCU.SetValue("");
                                this.DAT02_W2WCJP.SetValue("");
                                this.DAT02_W2PRGB.SetValue("");
                                this.DAT02_W2HIGB.SetValue("A");
                                this.DAT02_W2HISAB.SetValue(Employer.EmpNo);
                                this.DAT02_W2GUBUN.SetValue("");
                                this.DAT02_W2TXAMT.SetValue("0");
                                this.DAT02_W2TXVAT.SetValue("0");
                                this.DAT02_W2HWAJU.SetValue("");

                                datas.Add(new object[] {this.DAT02_W2SSID.GetValue().ToString(),
                                        this.DAT02_W2DPMK.GetValue().ToString(),
                                        this.DAT02_W2DTMK.GetValue().ToString(),
                                        this.DAT02_W2NOSQ.GetValue().ToString(),
                                        this.DAT02_W2NOLN.GetValue().ToString(),
                                        this.DAT02_W2IDJP.GetValue().ToString(),
                                        this.DAT02_W2NOJP.GetValue().ToString(),
                                        this.DAT02_W2CDAC.GetValue().ToString(),
                                        this.DAT02_W2DTAC.GetValue().ToString(),
                                        this.DAT02_W2DTLI.GetValue().ToString(),
                                        this.DAT02_W2DPAC.GetValue().ToString(),
                                        this.DAT02_W2CDMI1.GetValue().ToString(),
                                        this.DAT02_W2VLMI1.GetValue().ToString(),
                                        this.DAT02_W2CDMI2.GetValue().ToString(),
                                        this.DAT02_W2VLMI2.GetValue().ToString(),
                                        this.DAT02_W2CDMI3.GetValue().ToString(),
                                        this.DAT02_W2VLMI3.GetValue().ToString(),
                                        this.DAT02_W2CDMI4.GetValue().ToString(),
                                        this.DAT02_W2VLMI4.GetValue().ToString(),
                                        this.DAT02_W2CDMI5.GetValue().ToString(),
                                        this.DAT02_W2VLMI5.GetValue().ToString(),
                                        this.DAT02_W2CDMI6.GetValue().ToString(),
                                        this.DAT02_W2VLMI6.GetValue().ToString(),
                                        this.DAT02_W2AMDR.GetValue().ToString(),
                                        this.DAT02_W2AMCR.GetValue().ToString(),
                                        this.DAT02_W2CDFD.GetValue().ToString(),
                                        this.DAT02_W2AMFD.GetValue().ToString(),
                                        this.DAT02_W2RKAC.GetValue().ToString(),
                                        this.DAT02_W2RKCU.GetValue().ToString(),
                                        this.DAT02_W2WCJP.GetValue().ToString(),
                                        this.DAT02_W2PRGB.GetValue().ToString(),
                                        this.DAT02_W2HIGB.GetValue().ToString(),
                                        this.DAT02_W2HISAB.GetValue().ToString(),
                                        this.DAT02_W2GUBUN.GetValue().ToString(),
                                        this.DAT02_W2TXAMT.GetValue().ToString(),
                                        this.DAT02_W2TXVAT.GetValue().ToString(),
                                        this.DAT02_W2HWAJU.GetValue().ToString()});
                            }

                        }
                        #endregion

                        #region Description : 전표 처리
                        if (datas.Count > 0)
                        {
                            this.DbConnector.CommandClear();
                            foreach (object[] data in datas)
                            {
                                this.DbConnector.Attach("TY_P_AC_29DA5966", data);
                            }
                        }

                        //미승인 SP호출 파일 입력
                        this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, Employer.EmpNo, "A",
                                                                    sW2DPMK, sPRDATE, "", "",
                                                                    "", "", Employer.EmpNo);
                        this.DbConnector.ExecuteTranQueryList();

                        //전표 생성 함수 호출
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_29C80960", "A", sB2SSID, "");
                        string sOUTMSG_1 = Convert.ToString(this.DbConnector.ExecuteScalar());
                        if (sOUTMSG_1.Substring(0, 2) == "ER")
                        {
                            this.ShowCustomMessage(sOUTMSG_1, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        }
                        else
                        {
                            //this.ShowCustomMessage(sOUTMSG_1, "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.None);
                            this.ShowMessage("TY_M_AC_25O8K620");
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_AC_29D5B004", sB2SSID);
                            DataTable dtresult = this.DbConnector.ExecuteDataTable();
                            if (dtresult.Rows.Count > 0)
                            {
                                if (dtresult.Rows[0]["AGRESULTCODE"].ToString().Trim() == "OK")
                                {
                                    //전표번호 받아오기
                                    string sJpno = dtresult.Rows[0]["AGOUT_PARAM"].ToString();
                                    if (sJpno.Trim() != "")
                                    {
                                        this.DbConnector.CommandClear(); // 전표번호 셋팅
                                        this.DbConnector.Attach("TY_P_AC_36BAS831", sJpno, sFXMYYMM);
                                        this.DbConnector.ExecuteTranQueryList();

                                        this.BTN61_INQ_Click(null, null);

                                    }
                                }
                            }
                        }
                        #endregion

                    }
                    #endregion
                }
            }

            // 전표 출력 처리

            #region Description : 전표 출력 처리
            if (e.Column.ToString() == "4")
            {
                if (this.FPS91_TY_S_AC_36A6G825.GetValue("FXMREPJPNO").ToString() != "")
                {
                    string sB2DPMK = this.FPS91_TY_S_AC_36A6G825.GetValue("FXMREPJPNO").ToString().Substring(0, 6);
                    string sB2DTMK = this.FPS91_TY_S_AC_36A6G825.GetValue("FXMREPJPNO").ToString().Substring(7, 8);
                    string sB2NOSQ = this.FPS91_TY_S_AC_36A6G825.GetValue("FXMREPJPNO").ToString().Substring(16, 3);

                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach
                        (
                        "TY_P_AC_2AU2M916",
                        sB2DPMK,
                        sB2DTMK,
                        sB2NOSQ, // 시작 번호
                        sB2NOSQ  // 종료 번호
                        );

                    if (Convert.ToDouble(sB2DTMK.Substring(0, 4)) > 2014)
                    {
                        SectionReport rpt = new TYACBJ0012R();
                        // 세로 출력
                        rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                        dt.Clear();
                        dt = this.DbConnector.ExecuteDataTable();
                        if (dt.Rows.Count > 0)
                        {
                            (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                        }
                    }
                    else
                    {
                        SectionReport rpt = new TYACBJ001R();
                        // 세로 출력
                        rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                        dt.Clear();
                        dt = this.DbConnector.ExecuteDataTable();
                        if (dt.Rows.Count > 0)
                        {
                            (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                        }
                    }


                } 
            #endregion
            }

        } 
        #endregion


    }
}
