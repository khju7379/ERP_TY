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
    /// 유형자산 폐기전표관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2018.07.18 09:54
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
    ///  TY_P_AC_3423Y416 : 고정자산 이력 전표번호 셋팅
    ///  TY_P_AC_34N55557 : 고정자산 이력 전표번호 세팅(합필,분할)
    ///  TY_P_AC_35N8Z728 : 유형자산 증감삭제[자산이력]
    ///  TY_P_AC_44TEA300 : 구자산번호 가져오기
    ///  TY_P_GB_4CVJ7024 : 사용자 부서 정보
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_87IAE418 : 유형자산 폐기자산 조회(전표발행용)
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  JPNO_CRE : 전표생성
    ///  JPNO_DEL : 전표삭제
    ///  KBSABUN : 사번
    ///  ARJUNNO :  전표번호
    /// </summary>
    public partial class TYACHF015I : TYBase
    {
        private DataSet fds;
        private string fsWkGubn;

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

        private TYData DAT03_FXLYEAR;
        private TYData DAT03_FXLSEQ;
        private TYData DAT03_FXLSUBNUM;
        private TYData DAT03_FXLSETGN;
        private TYData DAT03_FXLSETDATE;
        private TYData DAT03_FXLNOLN;


        System.Collections.Generic.List<object[]> fdatas = new System.Collections.Generic.List<object[]>();
        System.Collections.Generic.List<object[]> fdatalog = new System.Collections.Generic.List<object[]>();

        #region  Description : 폼 로드 이벤트
        public TYACHF015I(DataSet ds, string sWkGubn)
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

            this.DAT03_FXLYEAR = new TYData("DAT03_FXLYEAR", null);
            this.DAT03_FXLSEQ = new TYData("DAT03_FXLSEQ", null);
            this.DAT03_FXLSUBNUM = new TYData("DAT03_FXLSUBNUM", null);            
            this.DAT03_FXLSETDATE = new TYData("DAT03_FXLSETDATE", null);
            this.DAT03_FXLSETGN = new TYData("DAT03_FXLSETGN", null);
            this.DAT03_FXLNOLN = new TYData("DAT03_FXLNOLN", null);

            this.SetPopupStyle();

            fds = ds;
            fsWkGubn = sWkGubn;
        }

        private void TYACHF015I_Load(object sender, System.EventArgs e)
        {
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

            this.ControlFactory.Add(this.DAT03_FXLYEAR);
            this.ControlFactory.Add(this.DAT03_FXLSEQ);
            this.ControlFactory.Add(this.DAT03_FXLSUBNUM);
            this.ControlFactory.Add(this.DAT03_FXLSETDATE);
            this.ControlFactory.Add(this.DAT03_FXLSETGN);            
            this.ControlFactory.Add(this.DAT03_FXLNOLN);

            this.BTN61_JPNO_CRE.ProcessCheck += new TButton.CheckHandler(BTN61_JPNO_CRE_ProcessCheck);
            this.BTN61_JPNO_DEL.ProcessCheck += new TButton.CheckHandler(BTN61_JPNO_DEL_ProcessCheck);

            this.UP_DataBinding();

            this.CBH01_KBSABUN.SetReadOnly(true);

            if (fsWkGubn == "A")
            {
                this.DTP01_B2DTMK.SetValue(fds.Tables[0].Rows[0]["FXLSETDATE"].ToString());
            }
            else
            {
                this.DTP01_B2DTMK.SetValue(fds.Tables[0].Rows[0]["FXLJPNODATE"].ToString());
                this.TXT01_ARJUNNO.SetValue(fds.Tables[0].Rows[0]["FXLUWJPNO"].ToString().Substring(0, 17).Substring(0, 6) + "-" + fds.Tables[0].Rows[0]["FXLUWJPNO"].ToString().Substring(0, 17).Substring(6, 8) + "-" + fds.Tables[0].Rows[0]["FXLUWJPNO"].ToString().Substring(0, 17).Substring(14, 3));
            }            

            if (fsWkGubn == "A")
            {
                BTN61_JPNO_CRE.Visible = true;
                BTN61_JPNO_DEL.Visible = false;
                
                this.CBH01_KBSABUN.SetValue(TYUserInfo.EmpNo);
            }
            else
            {
                this.DTP01_B2DTMK.SetReadOnly(true);

                BTN61_JPNO_CRE.Visible = false;
                BTN61_JPNO_DEL.Visible = true;

                this.CBH01_KBSABUN.SetValue(fds.Tables[0].Rows[0]["B1HISAB"].ToString());
            }
         
        }
        #endregion       

        #region  Description : UP_DataBinding 이벤트
        private void UP_DataBinding()
        {
            FPS91_TY_S_AC_87IAE418.Initialize();
            FPS91_TY_S_AC_87IAE418.SetValue(UP_ConvertDt(fds.Tables[0]));

            if (this.FPS91_TY_S_AC_87IAE418.CurrentRowCount > 0)
            {
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_87IAE418, "FXLSETTEXT", "[합  계]", SumRowType.Sum);
            }

        }
        #endregion

        #region  Description : 전표발행 버튼 이벤트
        private void BTN61_JPNO_CRE_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            string sB2SSID = string.Empty;
            string sW2DPMK = string.Empty;
            string sW2RKAC = string.Empty;        
            int iCnt = 0;

            bool bJunPyoFlag = false;

            fdatas.Clear();

            if (fds.Tables[0].Rows.Count > 0)
            {
                //BATID번호 부여
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29C7M958");
                decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());
                sB2SSID = this.IPAdresss + CBH01_KBSABUN.GetValue().ToString() + dAutoSeq.ToString();

                //사번 조회
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_GB_4CVJ7024", this.DTP01_B2DTMK.GetString().ToString(), CBH01_KBSABUN.GetValue().ToString().Trim());  //INKIBNMF
                DataTable dt_sabun = this.DbConnector.ExecuteDataTable();
                if (dt_sabun.Rows.Count != 0)
                { 
                    sW2DPMK = dt_sabun.Rows[0]["KBBUSEO"].ToString().Trim(); 
                }

                for (int i = 0; i < fds.Tables[0].Rows.Count; i++)
                {
                    if (fds.Tables[0].Rows[i]["FXLYEAR"].ToString() != "")
                    {
                        //라인번호
                        iCnt = iCnt + 1;

                        //자산이력 전표번호 UPDATE용 번호 저장
                        this.DAT03_FXLYEAR.SetValue(fds.Tables[0].Rows[i]["FXLYEAR"].ToString());
                        this.DAT03_FXLSEQ.SetValue(fds.Tables[0].Rows[i]["FXLSEQ"].ToString());
                        this.DAT03_FXLSUBNUM.SetValue(fds.Tables[0].Rows[i]["FXLSUBNUM"].ToString());
                        this.DAT03_FXLSETGN.SetValue(fds.Tables[0].Rows[i]["FXLSETGN"].ToString());
                        this.DAT03_FXLSETDATE.SetValue(fds.Tables[0].Rows[i]["FXLSETDATE"].ToString().Replace("-", "").ToString());
                        this.DAT03_FXLNOLN.SetValue(Set_Fill2(iCnt.ToString()));

                        UP_LogGenericListAdd();

                        //차변 첫번째라인 
                        this.DAT02_W2SSID.SetValue(sB2SSID);
                        this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                        this.DAT02_W2DTMK.SetValue(this.DTP01_B2DTMK.GetString().ToString()); // 작성일자
                        this.DAT02_W2NOSQ.SetValue("0");
                        this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                        this.DAT02_W2IDJP.SetValue("3");
                        this.DAT02_W2NOJP.SetValue("");
                        this.DAT02_W2CDAC.SetValue(UP_Get_AccountReturn(fds.Tables[0].Rows[i]["FXSCLASS"].ToString().Substring(0, 1), 1));
                        this.DAT02_W2DTAC.SetValue("");
                        this.DAT02_W2DTLI.SetValue("");
                        this.DAT02_W2DPAC.SetValue(UP_Set_DPAC(fds.Tables[0].Rows[i]["FXSAUP"].ToString())); // 귀속부서

                        //관리항목 
                        UP_Set_AccManageItem(fds.Tables[0].Rows[i]["FXSCLASS"].ToString().Substring(0, 1), 1);

                        sW2RKAC = fds.Tables[0].Rows[i]["FXSNAME"].ToString().Trim() + "손망실(" + fds.Tables[0].Rows[i]["FXLNUM"].ToString().Trim() + ")";

                        this.DAT02_W2AMDR.SetValue(fds.Tables[0].Rows[i]["FXLDWAMOUNT"].ToString().Trim()); // 차변금액 (충당금감소소액 필더)
                        this.DAT02_W2AMCR.SetValue("0");

                        this.DAT02_W2CDFD.SetValue("");
                        this.DAT02_W2AMFD.SetValue("0");
                        this.DAT02_W2RKAC.SetValue(sW2RKAC);
                        this.DAT02_W2RKCU.SetValue("");
                        this.DAT02_W2WCJP.SetValue("");
                        this.DAT02_W2PRGB.SetValue("");
                        this.DAT02_W2HIGB.SetValue("A");
                        this.DAT02_W2HISAB.SetValue(CBH01_KBSABUN.GetValue().ToString());
                        this.DAT02_W2GUBUN.SetValue("");
                        this.DAT02_W2TXAMT.SetValue("0");
                        this.DAT02_W2TXVAT.SetValue("0");
                        this.DAT02_W2HWAJU.SetValue("");

                        UP_GenericListAdd();

                        iCnt = iCnt + 1;

                        //차변 두번째라인 
                        this.DAT02_W2SSID.SetValue(sB2SSID);
                        this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                        this.DAT02_W2DTMK.SetValue(this.DTP01_B2DTMK.GetString().ToString()); // 작성일자
                        this.DAT02_W2NOSQ.SetValue("0");
                        this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                        this.DAT02_W2IDJP.SetValue("3");
                        this.DAT02_W2NOJP.SetValue("");
                        this.DAT02_W2CDAC.SetValue(UP_Get_AccountReturn(fds.Tables[0].Rows[i]["FXSCLASS"].ToString().Substring(0, 1), 2));
                        this.DAT02_W2DTAC.SetValue("");
                        this.DAT02_W2DTLI.SetValue("");
                        this.DAT02_W2DPAC.SetValue(UP_Set_DPAC(fds.Tables[0].Rows[i]["FXSAUP"].ToString())); // 귀속부서

                        //관리항목 
                        UP_Set_AccManageItem(fds.Tables[0].Rows[i]["FXSCLASS"].ToString().Substring(0, 1), 2);

                        sW2RKAC = fds.Tables[0].Rows[i]["FXSNAME"].ToString().Trim() + "손망실(" + fds.Tables[0].Rows[i]["FXLNUM"].ToString().Trim() + ")";

                        this.DAT02_W2AMDR.SetValue(Convert.ToString(Convert.ToDouble(fds.Tables[0].Rows[i]["FXLAMOUNT"].ToString().Trim()) - Convert.ToDouble(fds.Tables[0].Rows[i]["FXLDWAMOUNT"].ToString().Trim()))); // 차변금액 (충당금감소소액 필더)
                        this.DAT02_W2AMCR.SetValue("0");

                        this.DAT02_W2CDFD.SetValue("");
                        this.DAT02_W2AMFD.SetValue("0");
                        this.DAT02_W2RKAC.SetValue(sW2RKAC);
                        this.DAT02_W2RKCU.SetValue("");
                        this.DAT02_W2WCJP.SetValue("");
                        this.DAT02_W2PRGB.SetValue("");
                        this.DAT02_W2HIGB.SetValue("A");
                        this.DAT02_W2HISAB.SetValue(CBH01_KBSABUN.GetValue().ToString());
                        this.DAT02_W2GUBUN.SetValue("");
                        this.DAT02_W2TXAMT.SetValue("0");
                        this.DAT02_W2TXVAT.SetValue("0");
                        this.DAT02_W2HWAJU.SetValue("");

                        UP_GenericListAdd();

                        iCnt = iCnt + 1;

                        //대변 세번째라인 
                        this.DAT02_W2SSID.SetValue(sB2SSID);
                        this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                        this.DAT02_W2DTMK.SetValue(this.DTP01_B2DTMK.GetString().ToString()); // 작성일자
                        this.DAT02_W2NOSQ.SetValue("0");
                        this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                        this.DAT02_W2IDJP.SetValue("3");
                        this.DAT02_W2NOJP.SetValue("");
                        this.DAT02_W2CDAC.SetValue(UP_Get_AccountReturn(fds.Tables[0].Rows[i]["FXSCLASS"].ToString().Substring(0, 1), 3));
                        this.DAT02_W2DTAC.SetValue("");
                        this.DAT02_W2DTLI.SetValue("");
                        this.DAT02_W2DPAC.SetValue(UP_Set_DPAC(fds.Tables[0].Rows[i]["FXSAUP"].ToString())); // 귀속부서

                        //관리항목 
                        UP_Set_AccManageItem(fds.Tables[0].Rows[i]["FXSCLASS"].ToString().Substring(0, 1), 3);

                        if (this.DAT02_W2CDMI1.GetValue().ToString() == "38")
                        {
                            this.DAT02_W2VLMI1.SetValue(fds.Tables[0].Rows[i]["FXLNUM"].ToString().Trim().Replace("-", ""));
                        }

                        sW2RKAC = fds.Tables[0].Rows[i]["FXSNAME"].ToString().Trim() + "손망실(" + fds.Tables[0].Rows[i]["FXLNUM"].ToString().Trim() + ")";

                        this.DAT02_W2AMDR.SetValue("0"); // 대변금액
                        this.DAT02_W2AMCR.SetValue(fds.Tables[0].Rows[i]["FXLAMOUNT"].ToString().Trim());

                        this.DAT02_W2CDFD.SetValue("");
                        this.DAT02_W2AMFD.SetValue("0");
                        this.DAT02_W2RKAC.SetValue(sW2RKAC);
                        this.DAT02_W2RKCU.SetValue("");
                        this.DAT02_W2WCJP.SetValue("");
                        this.DAT02_W2PRGB.SetValue("");
                        this.DAT02_W2HIGB.SetValue("A");
                        this.DAT02_W2HISAB.SetValue(CBH01_KBSABUN.GetValue().ToString());
                        this.DAT02_W2GUBUN.SetValue("");
                        this.DAT02_W2TXAMT.SetValue("0");
                        this.DAT02_W2TXVAT.SetValue("0");
                        this.DAT02_W2HWAJU.SetValue("");

                        UP_GenericListAdd();
                    }

                }
            }

            if (fdatas.Count > 0)
            {
                this.DbConnector.CommandClear();
                foreach (object[] data in fdatas)
                {
                    this.DbConnector.Attach("TY_P_AC_29DA5966", data);
                }
            }

            //미승인 SP호출 파일 입력
            this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, CBH01_KBSABUN.GetValue().ToString() , "A",
                                                            sW2DPMK, this.DTP01_B2DTMK.GetString().ToString(), "", "",
                                                            "", "", CBH01_KBSABUN.GetValue().ToString());
            this.DbConnector.ExecuteTranQueryList();

            //전표 생성 함수 호출
            bJunPyoFlag = false;
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C80960", "A", sB2SSID, "");
            string sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
            if (sOUTMSG.Substring(0, 2) == "ER")
            {
                this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            else
            {
                bJunPyoFlag = true;
                //this.ShowCustomMessage(sOUTMSG, "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.None);
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
                            this.TXT01_ARJUNNO.SetValue(sJpno);
                            UP_Set_JunPyoNumber(sJpno,"A");
                        }
                    }
                }
            }

            BTN61_JPNO_CRE.Visible = false;
            BTN61_JPNO_DEL.Visible = true;

            this.ShowMessage("TY_M_AC_25O8K620");
        }
        private void BTN61_JPNO_CRE_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (fds.Tables[0].Rows[0]["FXLSETDATE"].ToString().Substring(0, 4) != DTP01_B2DTMK.GetString().ToString().Substring(0, 4))
            {
                this.ShowCustomMessage("폐기 설정년도와 전표년도는 같아야 합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_AC_25O8J618"))
            {
                e.Successed = false;
                return;
            }
        } 
        #endregion

        #region  Description : 전표취소 버튼 이벤트
        private void BTN61_JPNO_DEL_Click(object sender, EventArgs e)
        {
            string sB2SSID = string.Empty;
            string sW2DPMK = string.Empty;

            if (this.TXT01_ARJUNNO.GetValue().ToString() != "")
            {
                //BATID번호 부여
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29C7M958");
                decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());
                sB2SSID = this.IPAdresss + CBH01_KBSABUN.GetValue().ToString() + dAutoSeq.ToString();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_GB_4CVJ7024", this.DTP01_B2DTMK.GetString().ToString(), CBH01_KBSABUN.GetValue().ToString().Trim());  //INKIBNMF
                DataTable dt_sabun = this.DbConnector.ExecuteDataTable();
                if (dt_sabun.Rows.Count != 0)
                {
                    sW2DPMK = dt_sabun.Rows[0]["KBBUSEO"].ToString().Trim();
                }

                //미승인전표 -> 임시파일 입력
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29C7K957", sB2SSID, this.TXT01_ARJUNNO.GetValue().ToString().Replace("-", "").Substring(0, 6), this.TXT01_ARJUNNO.GetValue().ToString().Replace("-", "").Substring(6, 8), this.TXT01_ARJUNNO.GetValue().ToString().Replace("-", "").Substring(14, 3));
                //미승인 SP호출 파일 입력
                this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, CBH01_KBSABUN.GetValue().ToString(), "D",
                                                                sW2DPMK, this.DTP01_B2DTMK.GetString().ToString(), "", "",
                                                                "", "", CBH01_KBSABUN.GetValue().ToString());

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
                    //자산이력관리 전표번호 삭제
                    UP_Set_JunPyoNumber("", "D");
                    
                    this.TXT01_ARJUNNO.SetValue("");

                    this.DTP01_B2DTMK.SetReadOnly(false);

                    BTN61_JPNO_CRE.Visible = true;
                    BTN61_JPNO_DEL.Visible = false;

                    this.ShowMessage("TY_M_MR_3194D581");
                }

                this.BTN61_CLO_Click(null, null);
            }

        }
        private void BTN61_JPNO_DEL_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int iCnt = 0;

            if (this.TXT01_ARJUNNO.GetValue().ToString() == "")
            {
                this.ShowCustomMessage("미승인전표가 존재 하지 않습니다.!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            //승인전표 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2B7BT153", this.TXT01_ARJUNNO.GetValue().ToString().Replace("-", "").Substring(0, 6), this.TXT01_ARJUNNO.GetValue().ToString().Replace("-", "").Substring(6, 8), this.TXT01_ARJUNNO.GetValue().ToString().Replace("-","").Substring(14, 3)); // ADSLGLF
            DataTable dt_adsl = this.DbConnector.ExecuteDataTable();
            if (dt_adsl.Rows.Count == 0)
            {
                this.ShowCustomMessage("미승인전표가 존재 하지 않습니다.!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }
            else
            {
                if (dt_adsl.Rows[0]["B2NOJP"].ToString().Trim() != "")
                {
                    this.ShowCustomMessage("승인된 전표이므로 삭제 할수 없음!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                };
            }

            if (fds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < fds.Tables[0].Rows.Count; i++)
                {
                    //라인번호
                    iCnt = iCnt + 1;

                    //자산이력 전표번호 UPDATE용 번호 저장
                    this.DAT03_FXLYEAR.SetValue(fds.Tables[0].Rows[i]["FXLYEAR"].ToString());
                    this.DAT03_FXLSEQ.SetValue(fds.Tables[0].Rows[i]["FXLSEQ"].ToString());
                    this.DAT03_FXLSUBNUM.SetValue(fds.Tables[0].Rows[i]["FXLSUBNUM"].ToString());
                    this.DAT03_FXLSETGN.SetValue(fds.Tables[0].Rows[i]["FXLSETGN"].ToString());
                    this.DAT03_FXLSETDATE.SetValue(fds.Tables[0].Rows[i]["FXLSETDATE"].ToString());
                    this.DAT03_FXLNOLN.SetValue(Set_Fill2(iCnt.ToString()));

                    UP_LogGenericListAdd();
                }
            }

            if (!this.ShowMessage("TY_M_AC_25O8K619"))
            {
                e.Successed = false;
                return;
            }
        } 
        #endregion

        #region  Description : 관리항목 적용 함수
        private void UP_Set_AccManageItem(string sFXSCLASS, int iNum)
        {            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_23N3M888", UP_Get_AccountReturn(sFXSCLASS, iNum), "");
            DataTable dt = this.DbConnector.ExecuteDataTable();
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

            if (iNum == 1 || iNum == 2)
            {
                this.DAT02_W2VLMI1.SetValue("");
                this.DAT02_W2VLMI2.SetValue("");
                this.DAT02_W2VLMI3.SetValue("");
                this.DAT02_W2VLMI4.SetValue("");
                this.DAT02_W2VLMI5.SetValue("");
                this.DAT02_W2VLMI6.SetValue("");
            }

            if (iNum == 3)
            {
                this.DAT02_W2VLMI1.SetValue("");
                this.DAT02_W2VLMI2.SetValue("");
                this.DAT02_W2VLMI3.SetValue("");
                this.DAT02_W2VLMI4.SetValue("");
                this.DAT02_W2VLMI5.SetValue("");
                this.DAT02_W2VLMI6.SetValue("");
            }
        }
        #endregion

        #region  Description : 계정과목 선택 함수
        private string UP_Get_AccountReturn(string sASCLASS, int iNum)
        {
            string sValue = string.Empty;

            switch (sASCLASS)
            {
                case "2":
                    if (iNum == 1)
                    {
                        sValue = "12200299";
                    }
                    else if (iNum == 2)
                    {
                        sValue = "52000702";
                    }
                    else
                    {
                        sValue = "12200200";
                    }
                    break;
                case "3":
                    if (iNum == 1)
                    {
                        sValue = "12200399";
                    }
                    else if (iNum == 2)
                    {
                        sValue = "52000703";
                    }
                    else
                    {
                        sValue = "12200300";
                    }
                    break;                  
                case "4":
                     if (iNum == 1)
                    {
                        sValue = "12200499";
                    }
                    else if (iNum == 2)
                    {
                        sValue = "52000704";
                    }
                    else
                    {
                        sValue = "12200400";
                    }
                    break;          
                case "5":
                    if (iNum == 1)
                    {
                        sValue = "12200599";
                    }
                    else if (iNum == 2)
                    {
                        sValue = "52000788";
                    }
                    else
                    {
                        sValue = "12200500";
                    }
                    break;  
                case "6":
                    if (iNum == 1)
                    {
                        sValue = "12200699";
                    }
                    else if (iNum == 2)
                    {
                        sValue = "52000788";
                    }
                    else
                    {
                        sValue = "12200600";
                    }
                    break;  
                case "7":
                     if (iNum == 1)
                    {
                        sValue = "12200799";
                    }
                    else if (iNum == 2)
                    {
                        sValue = "52000788";
                    }
                    else
                    {
                        sValue = "12200700";
                    }
                    break;  
                case "8":
                    if (iNum == 1)
                    {
                        sValue = "12200899";
                    }
                    else if (iNum == 2)
                    {
                        sValue = "52000788";
                    }
                    else
                    {
                        sValue = "12200800";
                    }
                    break;
                case "9":
                    if (iNum == 1)
                    {
                        sValue = "12200999";
                    }
                    else if (iNum == 2)
                    {
                        sValue = "52000705";
                    }
                    else
                    {
                        sValue = "12200900";
                    }
                    break;
                default:
                    sValue = "";
                    break;
            }

            return sValue;
        }
        #endregion

        #region  Description : GenericListAdd 함수
        private void UP_GenericListAdd()
        {
            fdatas.Add(new object[] {   this.DAT02_W2SSID.GetValue().ToString(),
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
        #endregion

        #region  Description : LogGenericListAdd 함수
        private void UP_LogGenericListAdd()
        {
            fdatalog.Add(new object[] { this.DAT03_FXLYEAR.GetValue().ToString(),
                                        this.DAT03_FXLSEQ.GetValue().ToString(),
                                        this.DAT03_FXLSUBNUM.GetValue().ToString(),
                                        this.DAT03_FXLSETGN.GetValue().ToString(),
                                        this.DAT03_FXLSETDATE.GetValue().ToString(),
                                        this.DAT03_FXLNOLN.GetValue().ToString()
                           });
        }
        #endregion

        #region Description : 귀속부서 세팅
        private string UP_Set_DPAC(string sDPAC)
        {
            string sValue = "";
            
            switch (sDPAC.Substring(0, 2))
            {
                case "G1":
                case "A1":
                    return "A10000";

                case "C1":
                    return "C10000";

                case "A5":
                case "B8":
                case "B7":
                case "B6":
                case "B1":
                case "O1":
                case "O2":
                case "O3":
                case "O4":
                    return "A50000";

                case "S1":
                    return "S10000";
                case "S3":
                    return "S40000";

                case "T1":
                    return "T10000";
                case "T4":
                    return "T40000";

                default:
                    sValue = sDPAC;
                    break;
            }

            return sValue;
        }
        #endregion

        #region Description : 자산이력관리 전표번호 셋팅
        private void UP_Set_JunPyoNumber(string sJpno, string sGubn)
        {           

            this.DbConnector.CommandClear();
            foreach (object[] data in fdatalog)
            { 
                if (sGubn == "A")
                {
                    this.DbConnector.Attach("TY_P_AC_3423Y416", sJpno.Replace("-", "") + data[5].ToString(),
                                                                sJpno.Replace("-", "").ToString().Substring(6,8),
                                                                TYUserInfo.EmpNo,
                                                                data[0].ToString(),
                                                                data[1].ToString(),
                                                                data[2].ToString(),
                                                                data[4].ToString(),
                                                                data[3].ToString()                                                                
                                                                );
                }
                else
                {
                    this.DbConnector.Attach("TY_P_AC_3423Y416", "",
                                                                "",
                                                                TYUserInfo.EmpNo,
                                                                data[0].ToString(),
                                                                data[1].ToString(),
                                                                data[2].ToString(),
                                                                data[4].ToString(),
                                                                data[3].ToString()
                                                                );

                }
            }            
            
            if (this.DbConnector.CommandCount > 0)
            {
                this.DbConnector.ExecuteTranQueryList();
            }
        }
        #endregion

        #region Description : 데이터테이블 컨버젼
        private DataTable UP_ConvertDt(DataTable dt)
        {
            int i = 0;

            double dFXLDWAMOUNT = 0;
            double dFXSGETAMOUNT = 0;

            string sFilter = string.Empty;

            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;

            for (i = 0; i < nNum; i++)
            {               
                dFXLDWAMOUNT = dFXLDWAMOUNT + Convert.ToDouble(table.Rows[i]["FXLDWAMOUNT"].ToString());
                dFXSGETAMOUNT = dFXSGETAMOUNT + Convert.ToDouble(table.Rows[i]["FXSGETAMOUNT"].ToString());                
            }
           
            if (nNum > 0)
            {             
                /******** 총계를 위한 Row 생성 **************/
                row = table.NewRow();
                table.Rows.InsertAt(row, nNum);

                // 합 계 이름 넣기
                table.Rows[nNum]["FXLSETTEXT"] = "[합  계]";
                table.Rows[nNum]["FXLDWAMOUNT"] = string.Format("{0:#,##0}", dFXLDWAMOUNT);
                table.Rows[nNum]["FXSGETAMOUNT"] = string.Format("{0:#,##0}", dFXSGETAMOUNT);             
            }

            return table;
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
