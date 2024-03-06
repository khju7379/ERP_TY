using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 반제 등록 팝업(미승인 등록) 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2012.11.07 09:03
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_23N3L884 : 계정 과목 코드 상세
    ///  TY_P_AC_29DA5966 : 미승인전표 임시파일 등록(TMAC1102)
    ///  TY_P_AC_2AI7H749 : 미승인전표 반제 처리(차변-미승인기준)
    ///  TY_P_AC_2AI7J750 : 미승인전표 반제 처리(대변-미승인기준)
    ///  TY_P_AC_2AI7K755 : 미승인전표 반제 처리(차변-임시자료기준)
    ///  TY_P_AC_2AI7L756 : 미승인전표 반제 처리(대변-임시자료기준)
    ///  TY_P_AC_2AO4D818 : 미승인 임시화일 라인번호 구하기(미승인 등록)
    ///  TY_P_AC_2B77E166 : 반제 팝업등록 내역 조회 (미승인 등록)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2B77G167 : 반제 팝업등록 내역 조회 (미승인 등록)
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  SAV : 저장
    ///  B7CDAC : 계정코드
    ///  B7CDBK : 은행
    ///  B7CDSB : 사번
    ///  B7VEND : 거래처
    /// </summary>
    public partial class TYAZBJ01C4 : TYBase
    {
        private string fsVEND = string.Empty, fsCDBK = string.Empty, fsCDAC = string.Empty, fsCDSB = string.Empty;

        private string fsDPMK = string.Empty, fsDTMK = string.Empty, fsNOSQ = string.Empty, fsNOLN = string.Empty;
        private string fsIDJP = string.Empty;
        private string fsHISAB = string.Empty;

        private string fsSessionId = string.Empty;

        private string sVB7WCJP = string.Empty, sVB7AMJN = string.Empty, sVA1TAG01 = string.Empty, sVB2AMDR = string.Empty, sVB2AMCR = string.Empty, sVB2RKAC = string.Empty ,sVB2RKCU = string.Empty;

        private string sVB2CDAC = string.Empty, sVB2DPAC = string.Empty;
        private string sVA1VLMI1 = string.Empty, sVA1VLMI2 = string.Empty, sVA1VLMI3 = string.Empty, sVA1VLMI4 = string.Empty, sVA1VLMI5 = string.Empty, sVA1VLMI6 = string.Empty;
        private string fsA1CDMI1 = string.Empty;
        private string fsA1VLMI1 = string.Empty;
        private string fsA1CDMI2 = string.Empty;
        private string fsA1VLMI2 = string.Empty;
        private string fsA1CDMI3 = string.Empty;
        private string fsA1VLMI3 = string.Empty;
        private string fsA1CDMI4 = string.Empty;
        private string fsA1VLMI4 = string.Empty;
        private string fsA1CDMI5 = string.Empty;
        private string fsA1VLMI5 = string.Empty;
        private string fsA1CDMI6 = string.Empty;
        private string fsA1VLMI6 = string.Empty;       


        //public string fsWCJP = string.Empty;
        //public string fsAMDR = string.Empty;
        //public string fsJunno = string.Empty;
        //public string fsRKAC = string.Empty;

        #region Descriontion : 미승인 전표 Data 변수 정의
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

        //private TYData DAT02_W2HANG;
        //private TYData DAT02_W2SAPP; 
        #endregion

        public TYAZBJ01C4(string sSessionId, string sDPMK, string sDTMK, string sNOSQ, string sNOLN, string sIDJP, string sHISAB, string sVEND, string sCDBK, string sCDAC, string sCDSB)
        {
            InitializeComponent();
            this.SetPopupStyle();

            #region Description : 미승인 저장 변수 정의
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

            fsSessionId = sSessionId ;
            fsDPMK = sDPMK ;
            fsDTMK = sDTMK;
            fsNOSQ = sNOSQ;
            fsNOLN = sNOLN;
            fsIDJP = sIDJP;
            fsHISAB = sHISAB;

            fsVEND = sVEND;
            fsCDBK = sCDBK;
            fsCDAC = sCDAC;
            fsCDSB = sCDSB;
        }

        #region Description : Page_Load
        private void TYAZBJ01C4_Load(object sender, System.EventArgs e)
        {
            #region Description : 미승인전표 ControlFactory ADD  정의
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

            this.CBH01_B7VEND.SetValue(fsVEND);  //거래처
            this.CBH01_B7CDBK.SetValue(fsCDBK);  //은행
            this.CBH01_B7CDAC.SetValue(fsCDAC);  //계정과목
            this.CBH01_B7CDSB.SetValue(fsCDSB);  //사번

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(this.BTN61_SAV_ProcessCheck);

            this.BTN61_INQ_Click(null, null);
        } 
        #endregion


        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {

            double dAMATAmt = 0;
            double dAMBJAmt = 0;
            double dAMJNAmt = 0;
            

            this.DbConnector.CommandClear();  // ABANJMF ( 거래처,계정과목,은행,사번 )
            //this.DbConnector.Attach("TY_P_AC_2B77E166", this.CBH01_B7VEND.GetValue().ToString().Trim(), this.CBH01_B7CDAC.GetValue().ToString().Trim(),this.CBH01_B7CDBK.GetValue().ToString().Trim(),this.CBH01_B7CDSB.GetValue().ToString().Trim());

            this.DbConnector.Attach("TY_P_AC_A19GS722", this.CBH01_B7VEND.GetValue().ToString().Trim(), this.CBH01_B7CDAC.GetValue().ToString().Trim(), this.CBH01_B7CDBK.GetValue().ToString().Trim(), this.CBH01_B7CDSB.GetValue().ToString().Trim(),
                                                         fsSessionId, fsDPMK, fsDTMK, fsNOSQ, fsNOLN
                                                          );
            this.FPS91_TY_S_AC_2B77G167.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_2B77G167.CurrentRowCount > 0)
            {
                // 합계구하기
                for (int i = 0; i < this.FPS91_TY_S_AC_2B77G167.CurrentRowCount; i++)
                {
                    dAMATAmt = dAMATAmt + Convert.ToDouble(this.FPS91_TY_S_AC_2B77G167.GetValue(i, "B7AMAT").ToString());
                    dAMBJAmt = dAMBJAmt + Convert.ToDouble(this.FPS91_TY_S_AC_2B77G167.GetValue(i, "B7AMBJ").ToString());
                    dAMJNAmt = dAMJNAmt + Convert.ToDouble(this.FPS91_TY_S_AC_2B77G167.GetValue(i, "B7AMJN").ToString());
                }
                this.TXT01_BANAMAT.SetValue(dAMATAmt);
                this.TXT01_BANAMBJ.SetValue(dAMBJAmt);
                this.TXT01_BANAMJAN.SetValue(dAMJNAmt);
            }

        } 
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            //버튼 잠금
            this.BTN61_SAV.Visible = false; // 라인저장
            this.BTN61_INQ.Visible = false; // 라인조회
        }

        #endregion

        #region Description : 저장처리 전 처리 
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            bool bRetrun = true;

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_2B77G167.GetDataSourceInclude(TSpread.TActionType.Select, "BANCDAC", "JUNNO", "B7AMAT", "B7AMBJ", "B7AMJN", "B2RKAC", "B7VEND", "B7CDBK", "B7CDSB", "B7CDAS",
                          "A1TAG01", "A1CDMI1", "A1CDMI2", "A1CDMI3", "A1CDMI4", "A1CDMI5", "A1CDMI6", "A1OTMI1", "A1OTMI2", "A1OTMI3", "A1OTMI4", "A1OTMI5", "A1OTMI6"));


            Int32 iB2NOLN = 0;
            int iRowCnt = 0;

            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

            // 임시화일에 마직막 전표 번호를 가지고 옮
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2AO4D818", fsSessionId, fsDPMK, fsDTMK, fsNOSQ); // TMAC1102F
            iB2NOLN = Convert.ToInt32(this.DbConnector.ExecuteScalar().ToString());

            if (ds.Tables[0].Rows.Count != 0)
            {
                // 체크 부분
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //설정번호 
                        sVB7WCJP = ds.Tables[0].Rows[i]["JUNNO"].ToString().Trim();
                        //반제잔액
                        sVB7AMJN = Convert.ToString(Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i]["B7AMAT"].ToString().Trim())) -
                                                    Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i]["B7AMBJ"].ToString().Trim())));

                        //차대구분
                        sVA1TAG01 = SetDefaultValue(ds.Tables[0].Rows[i]["A1TAG01"].ToString().Trim());

                        /* 임시 테이블 입력전 필드 값 체크 (반제정리 Over 체크 함수) */
                        bRetrun = UP_FieldValueCheck();

                        if (bRetrun == false)
                        {
                            this.SetFocus(this.BTN61_SAV);
                            e.Successed = false;
                            return;
                        }

                        //설정전표의 자료 가져오기
                        bRetrun = UP_WonChunJunPyo(SetDefaultValue(ds.Tables[0].Rows[i]["JUNNO"].ToString().Trim())); // 원천번호(설정번호)

                        if (bRetrun == false)
                        {
                            this.SetFocus(this.BTN61_SAV);
                            e.Successed = false;
                            return;
                        }

                        /* 임시 테이블 입력전 필드 값 체크 (미수금 화일 CHECK)  */
                        bRetrun = UP_MESUCheck();
                        if (bRetrun == false)
                        {
                            this.SetFocus(this.BTN61_SAV);
                            e.Successed = false;
                            return;
                        }

                        iRowCnt = iRowCnt + 1;
                    }
                }

                if (iB2NOLN + iRowCnt > 98)
                {
                    this.ShowCustomMessage("전표의 라인수가 98라인을 초과합니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.BTN61_SAV);
                    e.Successed = false;
                    return;
                }

                // 생성부분
                DataTable dt = new DataTable();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        dt.Clear();

                        //설정전표의 자료 가져오기
                        bRetrun = UP_WonChunJunPyo(SetDefaultValue(ds.Tables[0].Rows[i]["JUNNO"].ToString().Trim()));

                        if (bRetrun == false)
                        {
                            return;
                        }

                        //설정번호 
                        sVB7WCJP = ds.Tables[0].Rows[i]["JUNNO"].ToString().Trim();
                        //반제잔액
                        sVB7AMJN = Convert.ToString(Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i]["B7AMAT"].ToString().Trim())) -
                                                    Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i]["B7AMBJ"].ToString().Trim())));
                        

                        switch (sVB2CDAC.Substring(0, 6))
                        {
                            case "111005":
                                sVB2RKAC = "어음만기입금";
                                break;
                            case "111004":
                                sVB2RKAC = "매출수금";
                                break;
                            default:
                                //적요
                                sVB2RKAC = ds.Tables[0].Rows[i]["B2RKAC"].ToString().Trim();
                                break;
                        }


                        //차대구분
                        sVA1TAG01 = SetDefaultValue(ds.Tables[0].Rows[i]["A1TAG01"].ToString().Trim());

                        fsA1CDMI1 = SetDefaultValue(ds.Tables[0].Rows[i]["A1CDMI1"].ToString().Trim());
                        fsA1CDMI2 = SetDefaultValue(ds.Tables[0].Rows[i]["A1CDMI2"].ToString().Trim());
                        fsA1CDMI3 = SetDefaultValue(ds.Tables[0].Rows[i]["A1CDMI3"].ToString().Trim());
                        fsA1CDMI4 = SetDefaultValue(ds.Tables[0].Rows[i]["A1CDMI4"].ToString().Trim());
                        fsA1CDMI5 = SetDefaultValue(ds.Tables[0].Rows[i]["A1CDMI5"].ToString().Trim());
                        fsA1CDMI6 = SetDefaultValue(ds.Tables[0].Rows[i]["A1CDMI6"].ToString().Trim());

                        if (SetDefaultValue(sVA1TAG01) == "D")
                        {
                            sVB2AMCR = sVB7AMJN;
                            sVB2AMDR = "0";

                            if (SetDefaultValue(ds.Tables[0].Rows[i]["A1OTMI1"].ToString().Trim()) == "D")
                            {
                                sVA1VLMI1 = "";
                            }
                            if (SetDefaultValue(ds.Tables[0].Rows[i]["A1OTMI2"].ToString().Trim()) == "D")
                            {
                                sVA1VLMI2 = "";
                            }
                            if (SetDefaultValue(ds.Tables[0].Rows[i]["A1OTMI3"].ToString().Trim()) == "D")
                            {
                                sVA1VLMI3 = "";
                            }
                            if (SetDefaultValue(ds.Tables[0].Rows[i]["A1OTMI4"].ToString().Trim()) == "D")
                            {
                                sVA1VLMI4 = "";
                            }
                            if (SetDefaultValue(ds.Tables[0].Rows[i]["A1OTMI5"].ToString().Trim()) == "D")
                            {
                                sVA1VLMI5 = "";
                            }
                            if (SetDefaultValue(ds.Tables[0].Rows[i]["A1OTMI6"].ToString().Trim()) == "D")
                            {
                                sVA1VLMI6 = "";
                            }
                        }
                        else
                        {
                            sVB2AMDR = sVB7AMJN;
                            sVB2AMCR = "0";

                            if (SetDefaultValue(ds.Tables[0].Rows[i]["A1OTMI1"].ToString().Trim()) == "C")
                            {
                                sVA1VLMI1 = "";
                            }
                            if (SetDefaultValue(ds.Tables[0].Rows[i]["A1OTMI2"].ToString().Trim()) == "C")
                            {
                                sVA1VLMI2 = "";
                            }
                            if (SetDefaultValue(ds.Tables[0].Rows[i]["A1OTMI3"].ToString().Trim()) == "C")
                            {
                                sVA1VLMI3 = "";
                            }
                            if (SetDefaultValue(ds.Tables[0].Rows[i]["A1OTMI4"].ToString().Trim()) == "C")
                            {
                                sVA1VLMI4 = "";
                            }
                            if (SetDefaultValue(ds.Tables[0].Rows[i]["A1OTMI5"].ToString().Trim()) == "C")
                            {
                                sVA1VLMI5 = "";
                            }
                            if (SetDefaultValue(ds.Tables[0].Rows[i]["A1OTMI6"].ToString().Trim()) == "C")
                            {
                                sVA1VLMI6 = "";
                            }
                        }


                        //상대처 조회 
                        string sVENDCODE = string.Empty;

                        sVENDCODE = UP_CDMIToVLMI_VEND("01", fsA1CDMI1, fsA1CDMI2, fsA1CDMI3, fsA1CDMI4, fsA1CDMI5, fsA1CDMI6,
                                                             sVA1VLMI1, sVA1VLMI2, sVA1VLMI3, sVA1VLMI4, sVA1VLMI5, sVA1VLMI6);

                        if (sVENDCODE != "")
                        {

                            sVENDCODE = sVENDCODE.Replace("-", "").Substring(0,6);

                            this.DbConnector.CommandClear(); // AVENDMF
                            this.DbConnector.Attach("TY_P_AC_2445D438", sVENDCODE);
                            DataTable dt_vedn = this.DbConnector.ExecuteDataTable();
                            if (dt_vedn.Rows.Count > 0)
                            {
                                sVB2RKCU = StringTransfer(dt_vedn.Rows[0]["VNSANGHO"].ToString().Trim(), 20);
                            }
                            else
                            {
                                sVB2RKCU = "";
                            }
                        }
                        else
                        {
                            sVB2RKCU = "";
                        }


                        iB2NOLN = iB2NOLN + 1;

                        this.DAT02_W2SSID.SetValue(fsSessionId);
                        this.DAT02_W2DPMK.SetValue(fsDPMK);
                        this.DAT02_W2DTMK.SetValue(fsDTMK);
                        this.DAT02_W2NOSQ.SetValue(fsNOSQ);

                        this.DAT02_W2NOLN.SetValue(Set_Fill2(iB2NOLN.ToString()));
                        this.DAT02_W2IDJP.SetValue(fsIDJP);
                        this.DAT02_W2NOJP.SetValue("");

                        this.DAT02_W2CDAC.SetValue(sVB2CDAC);  // 계정과목
                        this.DAT02_W2DTAC.SetValue("");
                        this.DAT02_W2DTLI.SetValue("");
                        this.DAT02_W2DPAC.SetValue(sVB2DPAC);  // 귀속부서
                        this.DAT02_W2CDMI1.SetValue(fsA1CDMI1);
                        this.DAT02_W2VLMI1.SetValue(sVA1VLMI1);
                        this.DAT02_W2CDMI2.SetValue(fsA1CDMI2);
                        this.DAT02_W2VLMI2.SetValue(sVA1VLMI2);
                        this.DAT02_W2CDMI3.SetValue(fsA1CDMI3);
                        this.DAT02_W2VLMI3.SetValue(sVA1VLMI3);
                        this.DAT02_W2CDMI4.SetValue(fsA1CDMI4);
                        this.DAT02_W2VLMI4.SetValue(sVA1VLMI4);
                        this.DAT02_W2CDMI5.SetValue(fsA1CDMI5);
                        this.DAT02_W2VLMI5.SetValue(sVA1VLMI5);
                        this.DAT02_W2CDMI6.SetValue(fsA1CDMI6);
                        this.DAT02_W2VLMI6.SetValue(sVA1VLMI6);
                        this.DAT02_W2AMDR.SetValue(sVB2AMDR);
                        this.DAT02_W2AMCR.SetValue(sVB2AMCR);

                        this.DAT02_W2CDFD.SetValue("");
                        this.DAT02_W2AMFD.SetValue("0");
                        this.DAT02_W2RKAC.SetValue(sVB2RKAC);
                        this.DAT02_W2RKCU.SetValue(sVB2RKCU); 
                        this.DAT02_W2WCJP.SetValue(sVB7WCJP);
                        this.DAT02_W2PRGB.SetValue("");
                        this.DAT02_W2HIGB.SetValue("A");
                        this.DAT02_W2HISAB.SetValue(fsHISAB);
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

                    if (datas.Count > 0)
                    {
                        this.DbConnector.CommandClear();
                        foreach (object[] data in datas)
                        {
                            this.DbConnector.Attach("TY_P_AC_29DA5966", data);
                        }

                        this.DbConnector.ExecuteNonQueryList();

                        string sCount = Convert.ToString(datas.Count) + " 건의 반제정리 자료가 등록되었습니다";

                        this.ShowCustomMessage(sCount, "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        this.SetFocus(this.BTN61_CLO);
                        e.Successed = true;
                        return ;
                    }
                }
            }
            else
            {
                this.ShowCustomMessage("선택된 자료가 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.SetFocus(this.BTN61_INQ);
                e.Successed = false;
                return;
            }
        }
        #endregion


        #region Description : 반제정리 Over 체크 함수
        private bool UP_FieldValueCheck()
        {
            string sWK_BanAMT = "";
            string sWK_BanAMBJ = "";
            string sWK_BanAMJN = "";

            if (SetDefaultValue(sVA1TAG01) == "D")
            {
                sVB2AMCR = sVB7AMJN;
                sVB2AMDR = "0";
            }
            else
            {
                sVB2AMDR = sVB7AMJN;
                sVB2AMCR = "0";
            }

            /* 36- 반제 정리전표인경우 반제금액 over 확인 */
            string sWK_WCDate = "";

            /*  작성일자 보다 원천번호의 작성일자가 늦으면 전표처리가 안됨.  */
            //원천번호의 작성일자 
            sWK_WCDate = sVB7WCJP.Trim().Substring(6, 8);
            if (Convert.ToInt32(sWK_WCDate) > Convert.ToInt32(fsDTMK))
            {
                this.ShowCustomMessage("원천번호의 일자가 작성일자보다 큽니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2AI5O748", sVB7WCJP.Trim().Substring(0, 6), sVB7WCJP.Trim().Substring(6, 8), sVB7WCJP.Trim().Substring(14, 3), sVB7WCJP.Trim().Substring(17, 2)); // 반제설정 조회
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                sWK_BanAMT = dt.Rows[0]["B7AMAT"].ToString();  // 발생금액
                sWK_BanAMBJ = dt.Rows[0]["B7AMBJ"].ToString(); // 정리금액
                sWK_BanAMJN = dt.Rows[0]["B7AMJN"].ToString(); // 잔액		
            }
            else
            {
                this.ShowCustomMessage("미등록 원천번호 입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }


            //<< 수정전 미승인전표의 정리 금액을 차감(전표발행전 상태로 금액수정) >>
            string sWK_B2AMBJ = "0";
            if (SetDefaultValue(sVA1TAG01) == "D")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2AI7H749", fsDPMK, fsDTMK, fsNOSQ, sVB7WCJP.Trim(), fsDPMK, fsDTMK, fsNOSQ, sVB7WCJP.Trim());
                DataTable dt_tm_1 = this.DbConnector.ExecuteDataTable();
                if (dt_tm_1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_tm_1.Rows.Count; i++)
                    {
                        sWK_B2AMBJ = dt_tm_1.Rows[0]["B2AMBJ"].ToString();
                    }
                }
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2AI7J750", fsDPMK, fsDTMK, fsNOSQ, sVB7WCJP.Trim(), fsDPMK, fsDTMK, fsNOSQ, sVB7WCJP.Trim());
                DataTable dt_tm_2 = this.DbConnector.ExecuteDataTable();
                if (dt_tm_2.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_tm_2.Rows.Count; i++)
                    {
                        sWK_B2AMBJ = dt_tm_2.Rows[0]["B2AMBJ"].ToString();
                    }
                }
            }

            sWK_BanAMBJ = Convert.ToString(Convert.ToDouble(Get_Numeric(sWK_BanAMBJ)) - Convert.ToDouble(Get_Numeric(sWK_B2AMBJ)));

            ///* 37- 수정후 임시화일의 정리금액을 더함 */
            string sWK_W2AM = "0";
            if (SetDefaultValue(sVA1TAG01) == "D")
            {
                this.DbConnector.CommandClear(); // TMAC1102F
                this.DbConnector.Attach("TY_P_AC_2AI7K755", fsSessionId, fsDPMK, fsDTMK, fsNOSQ, fsNOLN, sVB7WCJP,
                                                            fsSessionId, fsDPMK, fsDTMK, fsNOSQ, fsNOLN, sVB7WCJP);
                DataTable dt_tm_1 = this.DbConnector.ExecuteDataTable();
                if (dt_tm_1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_tm_1.Rows.Count; i++)
                    {
                        sWK_W2AM = dt_tm_1.Rows[0]["W2AM"].ToString();
                    }
                }
            }
            else
            {
                this.DbConnector.CommandClear(); // TY_P_AC_2AI7L756
                this.DbConnector.Attach("TY_P_AC_2AI7L756", fsSessionId, fsDPMK, fsDTMK, fsNOSQ, fsNOLN, sVB7WCJP,
                                                            fsSessionId, fsDPMK, fsDTMK, fsNOSQ, fsNOLN, sVB7WCJP);
                DataTable dt_tm_1 = this.DbConnector.ExecuteDataTable();
                if (dt_tm_1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_tm_1.Rows.Count; i++)
                    {
                        sWK_W2AM = dt_tm_1.Rows[0]["W2AM"].ToString();
                    }
                }
            }

            sWK_BanAMBJ = Convert.ToString(Convert.ToDouble(Get_Numeric(sWK_BanAMBJ)) + Convert.ToDouble(Get_Numeric(sWK_W2AM)));

            // ----------------------------------------------------------------------------------------------------------------- //

            //<< 현재 작업중인 레코드에대한 처리 >>
            if (SetDefaultValue(sVA1TAG01) == "D")
            {
                if (Convert.ToDouble(sVB2AMDR) < 0)
                {
                    sWK_BanAMBJ = Convert.ToString(Convert.ToDouble(sWK_BanAMBJ) +
                        (Convert.ToDouble(sVB2AMDR) * -1));
                }
                else
                {
                    sWK_BanAMBJ = Convert.ToString(Convert.ToDouble(sWK_BanAMBJ) +
                        Convert.ToDouble(sVB2AMCR));
                }
            }
            else
            {
                if (Convert.ToDouble(sVB2AMCR) < 0)
                {
                    sWK_BanAMBJ = Convert.ToString(Convert.ToDouble(sWK_BanAMBJ) +
                        (Convert.ToDouble(sVB2AMCR) * -1));
                }
                else
                {
                    sWK_BanAMBJ = Convert.ToString(Convert.ToDouble(sWK_BanAMBJ) +
                        Convert.ToDouble(sVB2AMDR));
                }
            }

            //<< 반재잔액 계산 >>
            sWK_BanAMJN = Convert.ToString(Convert.ToDouble(sWK_BanAMT) - Convert.ToDouble(sWK_BanAMBJ));
            
            if (Convert.ToDouble(sWK_BanAMJN) < 0)
            {
                this.ShowCustomMessage("정리금액이 설정금액을 초과하였습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }

            return true;

        }
        #endregion


        #region Description : 원천전표 내용 가져오기 --->  UP_WonChunJunPyo()
        private bool UP_WonChunJunPyo(string sB2WCJP)
        {
           

            this.DbConnector.CommandClear(); // ADSLGLF
            this.DbConnector.Attach("TY_P_AC_2B79W189", sB2WCJP.ToString().Substring(0, 6), sB2WCJP.ToString().Substring(6, 8) ,sB2WCJP.ToString().Substring(14, 3) ,sB2WCJP.ToString().Substring(17, 2));
            DataTable dt_adsl = this.DbConnector.ExecuteDataTable();
            if (dt_adsl.Rows.Count > 0)
            {
                sVB2CDAC = SetDefaultValue(dt_adsl.Rows[0]["B2CDAC"].ToString().Trim());

                //귀속부서
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_GB_4CVJ7024", fsDTMK, fsHISAB);
                DataTable dt_sabun = this.DbConnector.ExecuteDataTable();
                if (dt_sabun.Rows.Count != 0)
                {
                    sVB2DPAC = SetDefaultValue(dt_sabun.Rows[0]["KBBUSEO"].ToString().Trim());
                }

                // 2015년 조직개편 후 귀속부서 사용 강제세팅( 공무계전팀-E10000 ) -적용안함
                if (Convert.ToDouble(fsDTMK.Substring(0, 4)) > 2014 && sVB2DPAC.Substring(0, 1) == "E")
                {
                    if (sVB2DPAC == "E10200")
                    {
                        sVB2DPAC = "S10000";
                    }
                    if (sVB2DPAC == "E10100")
                    {
                        sVB2DPAC = "T10000";
                    }
                }

                sVA1VLMI1 = SetDefaultValue(dt_adsl.Rows[0]["B2VLMI1"].ToString().Trim());
                sVA1VLMI2 = SetDefaultValue(dt_adsl.Rows[0]["B2VLMI2"].ToString().Trim());
                sVA1VLMI3 = SetDefaultValue(dt_adsl.Rows[0]["B2VLMI3"].ToString().Trim());
                sVA1VLMI4 = SetDefaultValue(dt_adsl.Rows[0]["B2VLMI4"].ToString().Trim());
                sVA1VLMI5 = SetDefaultValue(dt_adsl.Rows[0]["B2VLMI5"].ToString().Trim());
                sVA1VLMI6 = SetDefaultValue(dt_adsl.Rows[0]["B2VLMI6"].ToString().Trim());
            }
            else
            {
                this.ShowCustomMessage("미등록 원천번호 입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }

            return true;

        }
        #endregion

        //* 임시 테이블 입력전 필드 값 체크 (미수금 화일 CHECK)  */
        #region Description : 미수금 이월확인 체크 함수   ---> UP_MESUCheck()
        private bool UP_MESUCheck()
        {
            if (sVB2CDAC == "11100401" || sVB2CDAC == "11100402" || sVB2CDAC == "11100403" || sVB2CDAC == "11100404" ||
                sVB2CDAC == "11100405" || sVB2CDAC == "11100411" || sVB2CDAC == "11100412" || sVB2CDAC == "11100413" ||
                sVB2CDAC == "11100414" || sVB2CDAC == "11100415" || sVB2CDAC == "11100416" || sVB2CDAC == "11100417" ||
                sVB2CDAC == "11100418" || sVB2CDAC == "11100419" || sVB2CDAC == "11100420" || sVB2CDAC == "11100421" ||
                sVB2CDAC == "11100422" || sVB2CDAC == "11101002") 
            {
                if (sVB2DPAC.Substring(0, 1) == "T")  //T00000 - UTT
                {
                    if (this.CBH01_B7VEND.GetValue().ToString().Trim() != "")
                    {
                        string sMIYYMM = "";
                        string sMIHWAJU = "";
                        string sMIMAECH = "";

                        //일자
                        sMIYYMM = fsDTMK.Trim().Substring(0, 6);

                        // 현업 거래처 거래처 구하기
                        // (현업 거래처 코드를 가지고 옮) 반제TABLE 에 있는것을 우선처리 
                        if (sVB7WCJP.Trim() != "")
                        {
                            sMIHWAJU = UP_Get_VendCode_HWAJU_ABANJMF(sVB7WCJP.Trim().ToUpper().Substring(0, 19));
                        }

                        if (sMIHWAJU == "")
                        {
                            sMIHWAJU = UP_Get_VendCode(this.CBH01_B7VEND.GetValue().ToString().Trim(), "T");
                        }

                        if (sMIHWAJU == "")
                        {
                            this.ShowCustomMessage("UTT 미수금 거래처가 올바르지 않습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            return false;
                        }
                        switch (sVB2CDAC)
                        {
                            case "11100412":
                                sMIMAECH = "01"; //접  안  료  11100506
                                break;
                            case "11101002":
                                sMIMAECH = "02"; //보  험  료  11300101
                                break;
                            case "11100413":
                                sMIMAECH = "03"; //하  역  료  11100507
                                break;
                            case "11100411":
                                sMIMAECH = "04"; //보  관  료  11100505
                                break;
                            case "11100414":
                                sMIMAECH = "05"; //취  급  료   11100508
                                break;
                        }

                        int iCount = 0;
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_2AN4N806", sMIYYMM, sMIHWAJU, sMIMAECH); // UTIMISUF
                        iCount = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                        if (iCount == 0)
                        {
                            this.ShowCustomMessage("UTT 미수금 파일(UTIMISUF)에 자료가 존재하지 않습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }

                //S00000 - SILO

                if (sVB2DPAC.Substring(0, 1) == "S")
                {
                    if (this.CBH01_B7VEND.GetValue().ToString().Trim() != "")
                    {
                        string sMIYYMM = "";
                        string sMIHWAJU = "";
                        string sMIMAECH = "";

                        //일자
                        sMIYYMM = fsDTMK.Trim().Substring(0, 6);
                        //현업 거래처 거래처 구하기

                        // (현업 거래처 코드를 가지고 옮) 반제TABLE 에 있는것을 우선처리 
                        if (sVB7WCJP.Trim() != "")
                        {
                            sMIHWAJU = UP_Get_VendCode_HWAJU_ABANJMF(sVB7WCJP.Trim().ToUpper().Substring(0, 19));
                        }

                        if (sMIHWAJU == "")
                        {
                            sMIHWAJU = UP_Get_VendCode(this.CBH01_B7VEND.GetValue().ToString().Trim(), "S");
                        }

                        if (sMIHWAJU == "")
                        {
                            this.ShowCustomMessage("SILO 미수금 거래처가 올바르지 않습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            return false;
                        }

                        switch (sVB2CDAC)
                        {
                            case "11100422":
                                sMIMAECH = "10"; //훈증 소득료   11100512
                                break;
                            case "11100412":
                                sMIMAECH = "11"; //접  안  료  11100506
                                break;
                            case "11100415":
                                sMIMAECH = "12"; //시설 사용료   11100509
                                break;
                            case "11100413":
                                sMIMAECH = "13"; //하 역 료     11100507
                                break;
                            case "11100411":
                                sMIMAECH = "14"; //보 관 료     11100505
                                break;
                            case "11100416":
                                sMIMAECH = "15"; // 조 출 료    11100511
                                break;
                            case "11100419":
                                sMIMAECH = "17"; //포대 개포작업비   11100513
                                break;
                            case "11100421":
                                sMIMAECH = "18"; //훈증 시설사용료   11100510
                                break;
                            case "11100417":
                                sMIMAECH = "19"; //화  물  료   11100514
                                break;
                            case "11100418":
                                sMIMAECH = "20"; //현대화 기금   11100516
                                break;
                            case "11100414":
                                sMIMAECH = "21"; //취  급  료   11100508
                                break;
                            case "11100420":
                                sMIMAECH = "22"; //중개수수료   11100524
                                break;
                        }

                        int iCount = 0;
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_2AN4N807", sMIYYMM, sMIHWAJU, sMIMAECH); // USIMISUF
                        iCount = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                        if (iCount == 0)
                        {
                            this.ShowCustomMessage("SILO 미수금 파일(USIMISUF)에 자료가 존재하지 않습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        #endregion

        #region Description : 매출입금 관련 사업장별 거래처 코드 조회 함수  ---> UP_Get_VendCode()
        private string UP_Get_VendCode(string sVNCODE, string sGUBN)
        {
            string sReSultVNCODE = "";

            if (sGUBN == "S")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2AN4G802", sVNCODE);  //USIVENDF
                DataTable dt_S = this.DbConnector.ExecuteDataTable();
                if (dt_S.Rows.Count > 0)
                {
                    sReSultVNCODE = dt_S.Rows[0]["VNCODE"].ToString().Trim();
                }
            }
            else if (sGUBN == "T")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2AN4H803", sVNCODE);  //UTIVENDF
                DataTable dt_T = this.DbConnector.ExecuteDataTable();
                if (dt_T.Rows.Count > 0)
                {
                    sReSultVNCODE = dt_T.Rows[0]["VNCODE"].ToString().Trim();
                }
            }
            else if (sGUBN == "B")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2AN4H804", sVNCODE);  //TRVENDF
                DataTable dt_B = this.DbConnector.ExecuteDataTable();
                if (dt_B.Rows.Count > 0)
                {
                    sReSultVNCODE = dt_B.Rows[0]["VNCODE"].ToString().Trim();
                }
            }

            return sReSultVNCODE;
        }
        #endregion

        #region Description : 매출입금 관련 현업 거래처 코드 조회 함수(외상매출금)  ---> UP_Get_VendCode_HWAJU_ABANJMF()
        private string UP_Get_VendCode_HWAJU_ABANJMF(string sWNJPNO)
        {
            string sReSultVNCODE = "";
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2AN4D801", sWNJPNO);  //ABANJMF
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                sReSultVNCODE = dt.Rows[0]["VNCODE"].ToString().Trim();
            }

            return sReSultVNCODE;
        }

        #endregion


        #region Description : 관리항목 코드 -> Value 리턴 ---> UP_CDMIToVLMI()
        private string UP_CDMIToVLMI_VEND(string sCDMIValue, string sCDMI1, string sCDMI2, string sCDMI3, string sCDMI4, string sCDMI5, string sCDMI6,
                             string sVLMI1, string sVLMI2, string sVLMI3, string sVLMI4, string sVLMI5, string sVLMI6)
        {
            string sVlMI = "";

            if (sCDMIValue == "35") sVlMI = "false";

            if (sCDMIValue == sCDMI1) sVlMI = sVLMI1;
            if (sCDMIValue == sCDMI2) sVlMI = sVLMI2;
            if (sCDMIValue == sCDMI3) sVlMI = sVLMI3;
            if (sCDMIValue == sCDMI4) sVlMI = sVLMI4;
            if (sCDMIValue == sCDMI5) sVlMI = sVLMI5;
            if (sCDMIValue == sCDMI6) sVlMI = sVLMI6;

            return sVlMI;
        }
        #endregion

    }
}
