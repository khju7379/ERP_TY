using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.ER.GB00;
using DataDynamics.ActiveReports;

namespace TY.ER.UT00
{
    /// <summary>
    /// 출고증 출력(당사 보관용) 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2020.09.03 15:45
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_A93F2964 : 잔량처리여부 체크
    ///  TY_P_UT_A93FQ965 : 잔량처리 티켓번호 조회
    ///  TY_P_UT_A93G2966 : 출고증 출력(잔량처리)
    ///  TY_P_UT_A93G5967 : 출고증 출력
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_9BIG4521 : 출력 할 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  CHCHULIL : 출고일자
    ///  EDSEQ : 발행순번
    ///  STSEQ : 발행순번
    /// </summary>
    public partial class TYUTPR033P : TYBase
    {
        string[] fsCHTKNO;      // TKNO
        string[] fsCHTKNO_PRT;  // TKNO 출력용
        string[] fsCHCHULIL;    // 출고일자 출력용
        string[] fsCHJGTKNO;    // 잔량연결 TKNO
        string[] fsCHJGCHDATE;  // 잔량출고일자

        #region Description : 폼 로드
        public TYUTPR033P()
        {
            InitializeComponent();
        }

        private void TYUTPR033P_Load(object sender, System.EventArgs e)
        {
            this.BTN61_PRT.ProcessCheck += new TButton.CheckHandler(BTN61_PRT_ProcessCheck);

            this.DTP01_CHCHULIL.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_CHCHULIL);
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {   
            DataTable dt = new DataTable();

            if(this.TXT01_STSEQ.GetValue().ToString() != "" && this.TXT01_EDSEQ.GetValue().ToString() == "")
            {
                fsCHTKNO = new string[1];
                fsCHTKNO_PRT = new string[1];
                fsCHCHULIL = new string[1];
                fsCHJGTKNO = new string[1];
                fsCHJGCHDATE = new string[1];

                fsCHTKNO[0] = this.TXT01_STSEQ.GetValue().ToString();
            }
            else if (this.TXT01_STSEQ.GetValue().ToString() != "" && this.TXT01_EDSEQ.GetValue().ToString() != "")
            {

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_A98AJ981",
                    Get_Date(this.DTP01_CHCHULIL.GetValue().ToString()),
                    this.TXT01_STSEQ.GetValue().ToString(),
                    this.TXT01_EDSEQ.GetValue().ToString()
                    );
                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    fsCHTKNO = new string[dt.Rows.Count];
                    fsCHTKNO_PRT = new string[dt.Rows.Count];
                    fsCHCHULIL = new string[dt.Rows.Count];
                    fsCHJGTKNO = new string[dt.Rows.Count];
                    fsCHJGCHDATE = new string[dt.Rows.Count];

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        fsCHTKNO[i] = dt.Rows[i]["CHTKNO"].ToString();
                    }
                }
                else
                {
                    fsCHTKNO = new string[1];
                    fsCHTKNO_PRT = new string[1];
                    fsCHCHULIL = new string[1];
                    fsCHJGTKNO = new string[1];
                    fsCHJGCHDATE = new string[1];
                    fsCHTKNO[0] = this.TXT01_STSEQ.GetValue().ToString();
                }
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_A98DG982",
                    Get_Date(this.DTP01_CHCHULIL.GetValue().ToString()),
                    this.TXT01_JIGSPINO.GetValue().ToString()
                    );
                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    fsCHTKNO = new string[dt.Rows.Count];
                    fsCHTKNO_PRT = new string[dt.Rows.Count];
                    fsCHCHULIL = new string[dt.Rows.Count];
                    fsCHJGTKNO = new string[dt.Rows.Count];
                    fsCHJGCHDATE = new string[dt.Rows.Count];

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        fsCHTKNO[i] = dt.Rows[i]["CHTKNO"].ToString();
                    }
                }
                else
                {
                    fsCHTKNO = new string[1];
                    fsCHTKNO_PRT = new string[1];
                    fsCHCHULIL = new string[1];
                    fsCHJGTKNO = new string[1];
                    fsCHJGCHDATE = new string[1];
                    fsCHTKNO[0] = this.TXT01_STSEQ.GetValue().ToString();
                }
            }
            // 잔량처리여부 조회
            for (int i = 0; i < fsCHTKNO_PRT.Length; i++)
            {
                UP_Get_CHJGTKNO(i);

                UP_Get_CHTKNO(i);
            }

            UP_Print();
        }
        #endregion

        #region Description : 출력 ProcessCheck
        private void BTN61_PRT_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.TXT01_EDSEQ.GetValue().ToString() != "")
            {
                if (this.TXT01_STSEQ.GetValue().ToString() == "")
                {
                    this.ShowCustomMessage("시작출고번호를 입력하세요.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }

                if (Convert.ToDouble(Get_Numeric(this.TXT01_STSEQ.GetValue().ToString())) > Convert.ToDouble(Get_Numeric(this.TXT01_EDSEQ.GetValue().ToString())))
                {
                    this.ShowCustomMessage("시작출고번호가 종료출고번호보다 큽니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }
                
            }
            
            if (this.TXT01_STSEQ.GetValue().ToString() == "" && this.TXT01_EDSEQ.GetValue().ToString() == "" && this.TXT01_JIGSPINO.GetValue().ToString() == "")
            {
                this.ShowCustomMessage("출고번호와 GS-PI NO 중 한 가지는 입력하여야 합니다 .", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                e.Successed = false;
                return;
            }
            if (this.TXT01_STSEQ.GetValue().ToString() != "" && this.TXT01_JIGSPINO.GetValue().ToString() != "")
            {
                this.ShowCustomMessage("출고번호와 GS-PI NO 중 한 가지만 입력하여야 합니다 .", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                e.Successed = false;
                return;
            }
            
        }
        #endregion

        #region Description : 잔량처리여부 조회
        private void UP_Get_CHJGTKNO(int i)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_UT_A93F2964",
                Get_Date(this.DTP01_CHCHULIL.GetValue().ToString()),
                fsCHTKNO[i]
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsCHJGCHDATE[i] = dt.Rows[0]["CHJGCHDATE"].ToString();
                fsCHJGTKNO[i] = dt.Rows[0]["CHJGTKNO"].ToString();
            }
            else
            {
                fsCHJGCHDATE[i] = "0";
                fsCHJGTKNO[i] = "0";
            }
        }
        #endregion

        #region Description : 티켓번호 연결
        private void UP_Get_CHTKNO(int i)
        {
            DataTable dt = new DataTable();

            if (fsCHJGCHDATE[i] != "0" && fsCHJGTKNO[i] != "0")
            {

                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                    (
                    "TY_P_UT_A93FQ965",
                    Get_Date(fsCHJGCHDATE[i]),
                    fsCHJGTKNO[i]
                    );

                dt = this.DbConnector.ExecuteDataTable();

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    fsCHCHULIL[i] = dt.Rows[j]["CHCHULIL"].ToString();

                    if (fsCHTKNO_PRT[i] == "")
                    {
                        fsCHTKNO_PRT[i] = dt.Rows[j]["CHTKNO"].ToString();
                    }
                    else
                    {
                        fsCHTKNO_PRT[i] = fsCHTKNO_PRT[i] + "," + dt.Rows[j]["CHTKNO"].ToString();
                    }
                }
            }
        }
        #endregion

        #region Description : 출고 데이터 조회
        private void UP_Print()
        {
            string sTKNO;
            string sCARNO;
            string sSOSOK;
            string sCHULHA;
            string sGAERYUNG;
            string sGROSS;
            string sTARE;
            string sNET;
            string sHWAJU;
            string sHWAMUL;
            string sDOCHAK;
            string sCHULTIME;
            string sCHULHA1;
            string sJISITANK;
            string sCHJGHWAJU;
            string sCHWKTYPE;
            string sCHWKTYPENM;
            string sTitle;
            string sCHSILNUM;
            string sCHCONTNUM;
            string sJIGSPINO;
            string sCHCHTANK;
            string sCHCHSTR;
            string sCHCHEND; 
            string sDRMtQty;

            DataTable dtPrt = new DataTable();
            DataTable dt = new DataTable();
            DataRow row = dtPrt.NewRow();

            int iCnt = 0;

            dtPrt.Columns.Add("WKTYPE1", typeof(System.String));
            dtPrt.Columns.Add("TKNO1", typeof(System.String));
            dtPrt.Columns.Add("TITLE1", typeof(System.String));
            dtPrt.Columns.Add("CARNO1", typeof(System.String));
            dtPrt.Columns.Add("SOSOK1", typeof(System.String));
            dtPrt.Columns.Add("CHULHA1", typeof(System.String));
            dtPrt.Columns.Add("GAERYUNG1", typeof(System.String));
            dtPrt.Columns.Add("GROSS1", typeof(System.String));
            dtPrt.Columns.Add("TARE1", typeof(System.String));
            dtPrt.Columns.Add("NET1", typeof(System.String));
            dtPrt.Columns.Add("HWAJU1", typeof(System.String));
            dtPrt.Columns.Add("HWAMUL1", typeof(System.String));
            dtPrt.Columns.Add("DOCHAK1", typeof(System.String));
            dtPrt.Columns.Add("CHULTIME1", typeof(System.String));
            dtPrt.Columns.Add("SEALNUM1", typeof(System.String));
            dtPrt.Columns.Add("JIGSPINO1", typeof(System.String));

            dtPrt.Columns.Add("WKTYPE2", typeof(System.String));
            dtPrt.Columns.Add("TKNO2", typeof(System.String));
            dtPrt.Columns.Add("TITLE2", typeof(System.String));
            dtPrt.Columns.Add("CARNO2", typeof(System.String));
            dtPrt.Columns.Add("SOSOK2", typeof(System.String));
            dtPrt.Columns.Add("CHULHA2", typeof(System.String));
            dtPrt.Columns.Add("GAERYUNG2", typeof(System.String));
            dtPrt.Columns.Add("GROSS2", typeof(System.String));
            dtPrt.Columns.Add("TARE2", typeof(System.String));
            dtPrt.Columns.Add("NET2", typeof(System.String));
            dtPrt.Columns.Add("HWAJU2", typeof(System.String));
            dtPrt.Columns.Add("HWAMUL2", typeof(System.String));
            dtPrt.Columns.Add("DOCHAK2", typeof(System.String));
            dtPrt.Columns.Add("CHULTIME2", typeof(System.String));
            dtPrt.Columns.Add("SEALNUM2", typeof(System.String));
            dtPrt.Columns.Add("JIGSPINO2", typeof(System.String));

            dtPrt.Columns.Add("WKTYPE3", typeof(System.String));
            dtPrt.Columns.Add("TKNO3", typeof(System.String));
            dtPrt.Columns.Add("TITLE3", typeof(System.String));
            dtPrt.Columns.Add("CARNO3", typeof(System.String));
            dtPrt.Columns.Add("SOSOK3", typeof(System.String));
            dtPrt.Columns.Add("CHULHA3", typeof(System.String));
            dtPrt.Columns.Add("GAERYUNG3", typeof(System.String));
            dtPrt.Columns.Add("GROSS3", typeof(System.String));
            dtPrt.Columns.Add("TARE3", typeof(System.String));
            dtPrt.Columns.Add("NET3", typeof(System.String));
            dtPrt.Columns.Add("HWAJU3", typeof(System.String));
            dtPrt.Columns.Add("HWAMUL3", typeof(System.String));
            dtPrt.Columns.Add("DOCHAK3", typeof(System.String));
            dtPrt.Columns.Add("CHULTIME3", typeof(System.String));
            dtPrt.Columns.Add("SEALNUM3", typeof(System.String));
            dtPrt.Columns.Add("JIGSPINO3", typeof(System.String));

            for (int i = 0; i < fsCHTKNO_PRT.Length; i++)
            {
                sTKNO = string.Empty;
                sCARNO = string.Empty;
                sSOSOK = string.Empty;
                sCHULHA = string.Empty;
                sGAERYUNG = string.Empty;
                sGROSS = string.Empty;
                sTARE = string.Empty;
                sNET = string.Empty;
                sHWAJU = string.Empty;
                sHWAMUL = string.Empty;
                sDOCHAK = string.Empty;
                sCHULTIME = string.Empty;
                sCHULHA1 = string.Empty;
                sJISITANK = string.Empty;
                sCHJGHWAJU = string.Empty;
                sCHWKTYPE = string.Empty;
                sCHWKTYPENM = string.Empty;
                sTitle = string.Empty;
                sCHSILNUM = string.Empty;
                sCHCONTNUM = string.Empty;
                sJIGSPINO = string.Empty;
                sCHCHTANK = string.Empty;
                sCHCHSTR = string.Empty;
                sCHCHEND = string.Empty;
                sDRMtQty = string.Empty;

                if (fsCHJGCHDATE[i] != "0" && fsCHJGTKNO[i] != "0")
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach
                        (
                        "TY_P_UT_A93G2966",
                        Get_Date(fsCHJGCHDATE[i]),
                        fsCHJGTKNO[i]
                        );

                    dt = this.DbConnector.ExecuteDataTable();
                }
                else
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach
                        (
                        "TY_P_UT_A93G5967",
                        Get_Date(this.DTP01_CHCHULIL.GetValue().ToString()),
                        fsCHTKNO[i]
                        );

                    dt = this.DbConnector.ExecuteDataTable();
                }

                if (dt.Rows.Count > 0)
                {
                    // << NO >>
                    if (fsCHJGCHDATE[i] != "0" && fsCHJGTKNO[i] != "0")
                    {
                        sTKNO = fsCHTKNO_PRT[i] + "   " + fsCHCHULIL[i].Substring(0,4) + "/" + fsCHCHULIL[i].Substring(4,2) + "/" + fsCHCHULIL[i].Substring(6,2) + "/";
                    }
                    else{
                        sTKNO = dt.Rows[0]["CHTKNO"].ToString() + "   " + dt.Rows[0]["CHCHULIL"].ToString();
                    }
        
                    // << 출고유형 >>
                    sCHWKTYPE = dt.Rows[0]["CHWKTYPE"].ToString();
        
                    if (sCHWKTYPE == "01")
                    {
                        sCHWKTYPENM = "(TANK LORRY)";
                    }
                    else if (sCHWKTYPE == "02")
                    {
                       sCHWKTYPENM = "(ISO CONTAINER)";
                    }
                    else if (sCHWKTYPE == "03")
                    {
                       sCHWKTYPENM = "(FLEXI BAG)";
                    }
                    else
                    {
                       sCHWKTYPENM = "(TANK LORRY)";
                    }
        
                    // << 차량번호(FULL) >>
                    if (sCHWKTYPE == "01")
                    {
                       sCARNO = dt.Rows[0]["CHCARNO"].ToString().Trim();
                    }
                    else if (sCHWKTYPE == "02")
                    {
                       sCARNO = dt.Rows[0]["CHCONTNUM"].ToString().Trim() + "          " + dt.Rows[0]["CHCARNO"].ToString().Trim().Substring(dt.Rows[0]["CHCARNO"].ToString().Trim().Length -4, 4);
                    }
                    else if (sCHWKTYPE == "03")
                    {
                       sCARNO = dt.Rows[0]["CHCONTNUM"].ToString().Trim() + "          " + dt.Rows[0]["CHCARNO"].ToString().Trim().Substring(dt.Rows[0]["CHCARNO"].ToString().Trim().Length -4, 4);
                    }
                    else
                    {
                        sCARNO = dt.Rows[0]["CHCARNO"].ToString().Trim();
                    }
        
        
                    // << TITLE >>
                    if (sCHWKTYPE == "01")
                    {
                       sTitle = "차량번호";
                    }
                    else if (sCHWKTYPE == "02")
                    { 
                       sTitle = "CON NO.";
                    }
                    else if (sCHWKTYPE == "03")
                    {
                       sTitle = "FLEXI NO.";
                    }
                    else
                    {
                       sTitle = "차량번호";
                    }
        
                    // << 소속 >>
                    sSOSOK = dt.Rows[0]["CHSOSOKNM"].ToString().Trim();
        
                    // << 재고화주 >>
                    sCHJGHWAJU = dt.Rows[0]["CHJGHWAJU"].ToString().Trim();
        
                    // << 출고탱크 >>
                    sCHCHTANK = "T-" + dt.Rows[0]["CHCHTANK"].ToString().Trim();
        
                    // << 출하원 >>
                    if (sCHJGHWAJU == "GSG")
                    {
                        if(dt.Rows[0]["CHCHHASAB"].ToString().Trim() != "")
                        {
                            sCHULHA = dt.Rows[0]["CHCHHASABNM"].ToString().Trim() + "  (" + sCHCHTANK + ")";
                        }
                        else{
                            sCHULHA = dt.Rows[0]["OUDESC1"].ToString().Trim() + "  (" + sCHCHTANK + ")";
                        }
                    }
                    else{
        
                       if(dt.Rows[0]["CHCHHASAB"].ToString().Trim() != "")
                        {
                            sCHULHA = dt.Rows[0]["CHCHHASABNM"].ToString().Trim();
                        }
                        else{
                            sCHULHA = dt.Rows[0]["OUDESC1"].ToString().Trim();
                        }
                    }
        
                    // << 지시 탱크 >>
                    if (sCHJGHWAJU != "GSG")
                    {
                        if (sCHJGHWAJU == "HHK")
                        {
                            sJISITANK = dt.Rows[0]["CHJISITANK"].ToString().Trim();

                            if (sJISITANK != "")
                            {
                                sCHULHA1 = sCHULHA + "  (T-" + sJISITANK + ", " + sCHCHTANK + ")";
                            }
                            else
                            {
                                sCHULHA1 = sCHULHA + "  (" + sCHCHTANK + ")";
                            }
                        }
                        else
                        {
                            sCHULHA1 = sCHULHA + "  (" + sCHCHTANK + ")";
                        }
                    }
                    else
                    {
                        sCHULHA1 = sCHULHA;
                    }
        
                    // << 계량자 >>
                    if (sCHJGHWAJU == "GSG")
                    {
                        // 20180130 박선형 주임 요청
                        // GSG일 경우 탱크로리일때만 비중 나오게 함
                        if(sCHWKTYPE == "01")
                        {
                            sGAERYUNG = dt.Rows[0]["CHCHSABNM"].ToString().Trim() + "  (비중:" + string.Format("{0:#,###.0000}", double.Parse(dt.Rows[0]["CHBIJUNG"].ToString().Trim())) + ")";
                        }
                        else{
                            sGAERYUNG = dt.Rows[0]["CHCHSABNM"].ToString().Trim();
                        }
                    }
                    else{
                        sGAERYUNG = dt.Rows[0]["CHCHSABNM"].ToString().Trim();
                    }

                    //<< GROSS(실  차) >>
                    sGROSS = string.Format("{0:#,###.000}", double.Parse(dt.Rows[0]["CHTOTAL"].ToString().Trim())) + "톤";
        
                    //<< TARE(공  차) >>
                    sTARE = string.Format("{0:#,###.000}", double.Parse(dt.Rows[0]["CHEMPTY"].ToString().Trim())) + "톤";
        
                    sDRMtQty = string.Format("{0:#,###.000}", double.Parse(dt.Rows[0]["CHTOTAL"].ToString().Trim()) - double.Parse(dt.Rows[0]["CHEMPTY"].ToString().Trim()));
        
                    // << NET(출고량) >>
                    sNET = string.Format("{0:#,###.000}", double.Parse(dt.Rows[0]["CHMTQTY"].ToString().Trim())) + "톤";
        
                    // << 재고화주명 >>
                    sHWAJU = dt.Rows[0]["VNSANGHO"].ToString().Trim();
        
                    // << 화물명 >>
                    // GS칼텍스 화주만 온도를 찍어준다
                    if (sCHJGHWAJU == "GSG")
                    {
                        sHWAMUL = dt.Rows[0]["CHHWAMULNM"].ToString().Trim() + "  (온도:" + string.Format("{0:#,###.0}", double.Parse(dt.Rows[0]["CHTEMP"].ToString().Trim())) + ")";
                    }
                    else{
                        sHWAMUL = dt.Rows[0]["CHHWAMULNM"].ToString().Trim();
                    }
        
                    // << 도착지 >>
                    if (sCHWKTYPE == "01")
                    {
                       sDOCHAK = dt.Rows[0]["CHCHHJNM"].ToString().Trim();
                    }
                    else{
                        sDOCHAK = dt.Rows[0]["CHCHHJNM"].ToString().Trim() + "(" + dt.Rows[0]["KJDESC"].ToString().Trim() + ")";
                    }
        
                    // << 출하시간 >>
                    sCHCHSTR = Set_Fill4(dt.Rows[0]["CHCHSTR"].ToString().Trim());
                    sCHCHEND = Set_Fill4(dt.Rows[0]["CHCHEND"].ToString().Trim());
                    sCHULTIME = sCHCHSTR.Substring(0,2) + ":" + sCHCHSTR.Substring(2,2) + " - " + sCHCHEND.Substring(0,2) + ":" + sCHCHEND.Substring(2,2);
        
                    // << 씰번호 >>
                    sCHSILNUM = dt.Rows[0]["CHSILNUM"].ToString().Trim();
        
                    // << GS-PINO >>
                    sJIGSPINO = dt.Rows[0]["JIGSPINO"].ToString().Trim();

                    if (iCnt % 3 == 0)
                    {
                        row = dtPrt.NewRow();

                        row["WKTYPE1"] = sCHWKTYPENM;
                        row["TKNO1"] = sTKNO;
                        row["TITLE1"] = sTitle;
                        row["CARNO1"] = sCARNO;
                        row["SOSOK1"] = sSOSOK;
                        row["CHULHA1"] = sCHULHA1;
                        row["GAERYUNG1"] = sGAERYUNG;
                        row["GROSS1"] = sGROSS;
                        row["TARE1"] = sTARE;
                        row["NET1"] = sNET;
                        row["HWAJU1"] = sHWAJU;
                        row["HWAMUL1"] = sHWAMUL;
                        row["DOCHAK1"] = sDOCHAK;
                        row["CHULTIME1"] = sCHULTIME;
                        row["SEALNUM1"] = sCHSILNUM;
                        row["JIGSPINO1"] = sJIGSPINO;

                        if (i + 1 == fsCHTKNO_PRT.Length)
                        {
                            dtPrt.Rows.Add(row);
                        }
                    }
                    else if (iCnt % 3 == 1)
                    {
                        row["WKTYPE2"] = sCHWKTYPENM;
                        row["TKNO2"] = sTKNO;
                        row["TITLE2"] = sTitle;
                        row["CARNO2"] = sCARNO;
                        row["SOSOK2"] = sSOSOK;
                        row["CHULHA2"] = sCHULHA1;
                        row["GAERYUNG2"] = sGAERYUNG;
                        row["GROSS2"] = sGROSS;
                        row["TARE2"] = sTARE;
                        row["NET2"] = sNET;
                        row["HWAJU2"] = sHWAJU;
                        row["HWAMUL2"] = sHWAMUL;
                        row["DOCHAK2"] = sDOCHAK;
                        row["CHULTIME2"] = sCHULTIME;
                        row["SEALNUM2"] = sCHSILNUM;
                        row["JIGSPINO2"] = sJIGSPINO;

                        if (i + 1 == fsCHTKNO_PRT.Length)
                        {
                            dtPrt.Rows.Add(row);
                        }
                    }
                    else if (iCnt % 3 == 2)
                    {
                        row["WKTYPE3"] = sCHWKTYPENM;
                        row["TKNO3"] = sTKNO;
                        row["TITLE3"] = sTitle;
                        row["CARNO3"] = sCARNO;
                        row["SOSOK3"] = sSOSOK;
                        row["CHULHA3"] = sCHULHA1;
                        row["GAERYUNG3"] = sGAERYUNG;
                        row["GROSS3"] = sGROSS;
                        row["TARE3"] = sTARE;
                        row["NET3"] = sNET;
                        row["HWAJU3"] = sHWAJU;
                        row["HWAMUL3"] = sHWAMUL;
                        row["DOCHAK3"] = sDOCHAK;
                        row["CHULTIME3"] = sCHULTIME;
                        row["SEALNUM3"] = sCHSILNUM;
                        row["JIGSPINO3"] = sJIGSPINO;
                        dtPrt.Rows.Add(row);
                    }
                    iCnt++;
                }
            }

            if (dtPrt.Rows.Count > 0)
            {
                ActiveReport rpt = new TYUTPR033R();

                rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dtPrt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_GB_9BIG4521");
            }
        }
        #endregion
    }
}
