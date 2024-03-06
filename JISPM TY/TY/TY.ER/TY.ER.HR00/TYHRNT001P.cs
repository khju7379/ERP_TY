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
    /// 연말정산 제출자료 관리(사용자별) 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.07.07 16:09
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_777GA046 : 연말정산 영수증 보험료 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_777GB048 : 연말정산 영수증 보험료 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  REM : 삭제
    ///  KBSABUN : 사번
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRNT001P : TYBase
    {
        private string fsComPy;
        private string fsYEAR;
        private string fsKBSABUN;
        private string fsFixGubn;

        private object _TXT01_COMPY_Value;
        private object _TXT01_SDATE_Value;
        private object _CBH01_KBSABUN_Value;
        private object _Login_Value;

        #region  Description : 폼 로드 이벤트
        public TYHRNT001P(string sComPy, string sYEAR, string sKBSABUN, string sFixGubn)
        {
            InitializeComponent();

            fsComPy = sComPy;
            fsYEAR = sYEAR;
            fsKBSABUN = sKBSABUN;
            fsFixGubn = sFixGubn;

        }

        private void TYHRNT001P_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.BTN61_BATCH.IsAsynchronous = true;
            this.BTN61_REM.IsAsynchronous = true;

            this.TXT01_SDATE.SetValue(fsYEAR);
            this.CBH01_KBSABUN.SetValue(fsKBSABUN);

            if (fsFixGubn == "Y")
            {
                BTN61_BATCH.Visible = false;
                BTN61_REM.Visible = false;
                BTN61_SAV.Visible = false;
            }

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            //보험료 
            this.UP_DataBing_A102Y();
            //의료비
            this.UP_DataBing_B101Y();
            //실손보험
            this.UP_DataBing_B201Y();
            //교육비
            this.UP_DataBing_C102Y();
            //직업훈련비
            this.UP_DataBing_C202Y();
            //교복구입비
            this.UP_DataBing_C301Y();
            //학자금대출
            this.UP_DataBing_C401Y();
            //개인연금저축
            this.UP_DataBing_D101Y();
            //연금저축
            this.UP_DataBing_E102Y();
            //퇴직연금
            this.UP_DataBing_F102Y();
            //신용카드, 직불카드, 제로페이
            this.UP_DataBing_G106Y();
            //현금영수증
            this.UP_DataBing_G206M();
            //주택임차차입금원리상환액
            this.UP_DataBing_J101Y();
            //장기주택저당차입금 이자상환액
            this.UP_DataBing_J203Y();
            //주택마련저축
            this.UP_DataBing_J301Y();
            //기부금
            this.UP_DataBing_L102Y();
            //장기집합투자증권처축
            this.UP_DataBing_N101Y();
            //건강보험료
            this.UP_DataBing_O101Y();
            //국민연금
            this.UP_DataBing_P102M();
            //월세액
            this.UP_DataBing_J501Y();

        }
        #endregion

        #region  Description : 보험료 영수증 자료 조회
        private void UP_DataBing_A102Y()
        {
            this.FPS91_TY_S_HR_777GB048.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_777GA046", TYUserInfo.SecureKey, "Y", fsComPy, fsYEAR, fsKBSABUN);
            this.FPS91_TY_S_HR_777GB048.SetValue(this.DbConnector.ExecuteDataTable());

          

        }
        #endregion

        #region  Description : 의료비 영수증 자료 조회
        private void UP_DataBing_B101Y()
        {
            this.FPS91_TY_S_HR_777H4051.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_777GY049", TYUserInfo.SecureKey, "Y", fsComPy, fsYEAR, fsKBSABUN);
            this.FPS91_TY_S_HR_777H4051.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region  Description : 실손보험 영수증 자료 조회
        private void UP_DataBing_B201Y()
        {
            this.FPS91_TY_S_HR_ACOAH238.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_ACOAG237", TYUserInfo.SecureKey, "Y", fsComPy, fsYEAR, fsKBSABUN);
            this.FPS91_TY_S_HR_ACOAH238.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region  Description : 교육비 영수증 자료 조회
        private void UP_DataBing_C102Y()
        {
            this.FPS91_TY_S_HR_777HG056.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_777HG055", TYUserInfo.SecureKey, "Y", fsComPy, fsYEAR, fsKBSABUN);
            this.FPS91_TY_S_HR_777HG056.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region  Description : 직업훈련비 영수증 자료 조회
        private void UP_DataBing_C202Y()
        {
            this.FPS91_TY_S_HR_777HQ058.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_777HQ057", TYUserInfo.SecureKey, "Y", fsComPy, fsYEAR, fsKBSABUN);
            this.FPS91_TY_S_HR_777HQ058.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region  Description : 교복구입비 영수증 자료 조회
        private void UP_DataBing_C301Y()
        {
            this.FPS91_TY_S_HR_777HT060.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_777HT059", TYUserInfo.SecureKey, "Y", fsComPy, fsYEAR, fsKBSABUN);
            this.FPS91_TY_S_HR_777HT060.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region  Description : 학자금대출 영수증 자료 조회
        private void UP_DataBing_C401Y()
        {
            this.FPS91_TY_S_HR_7C5BN177.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_7C5BM176", TYUserInfo.SecureKey, "Y", fsComPy, fsYEAR, fsKBSABUN);
            this.FPS91_TY_S_HR_7C5BN177.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region  Description : 개인연금저축 영수증 자료 조회
        private void UP_DataBing_D101Y()
        {
            this.FPS91_TY_S_HR_777I3065.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_777HW061", TYUserInfo.SecureKey, "Y",  fsComPy, fsYEAR, fsKBSABUN);
            this.FPS91_TY_S_HR_777I3065.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region  Description : 연금저축 영수증 자료 조회
        private void UP_DataBing_E102Y()
        {
            this.FPS91_TY_S_HR_77A8T068.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77A8T067", TYUserInfo.SecureKey, "Y", fsComPy, fsYEAR, fsKBSABUN);
            this.FPS91_TY_S_HR_77A8T068.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region  Description : 퇴직저축 영수증 자료 조회
        private void UP_DataBing_F102Y()
        {
            this.FPS91_TY_S_HR_77AAT070.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77AAT069", TYUserInfo.SecureKey, "Y", fsComPy, fsYEAR, fsKBSABUN);
            this.FPS91_TY_S_HR_77AAT070.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region  Description : 신용카드, 직불카드 자료 조회
        private void UP_DataBing_G106Y()
        {
            if (Convert.ToInt16(fsYEAR) >= 2022)
            {
                //신용카드
                this.FPS91_TY_S_HR_77AB3074.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_CCLGF410", TYUserInfo.SecureKey, "Y", fsComPy, fsYEAR, fsKBSABUN, "G110Y");
                this.FPS91_TY_S_HR_77AB3074.SetValue(this.DbConnector.ExecuteDataTable());

                //직불카드
                this.FPS91_TY_S_HR_77ABJ076.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_CCLGF410", TYUserInfo.SecureKey, "Y", fsComPy, fsYEAR, fsKBSABUN, "G310Y");
                this.FPS91_TY_S_HR_77ABJ076.SetValue(this.DbConnector.ExecuteDataTable());

                //제로페이
                this.FPS92_TY_S_HR_77ABJ076.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_CCLGF410", TYUserInfo.SecureKey, "Y", fsComPy, fsYEAR, fsKBSABUN, "G410Y");
                this.FPS92_TY_S_HR_77ABJ076.SetValue(this.DbConnector.ExecuteDataTable());
            }
            else if (Convert.ToInt16(fsYEAR) == 2021)
            {
                //신용카드
                this.FPS91_TY_S_HR_77AB3074.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_BCSDE955", TYUserInfo.SecureKey, "Y", fsComPy, fsYEAR, fsKBSABUN, "G109Y");
                this.FPS91_TY_S_HR_77AB3074.SetValue(this.DbConnector.ExecuteDataTable());

                //직불카드
                this.FPS91_TY_S_HR_77ABJ076.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_BCSDE955", TYUserInfo.SecureKey, "Y", fsComPy, fsYEAR, fsKBSABUN, "G309Y");
                this.FPS91_TY_S_HR_77ABJ076.SetValue(this.DbConnector.ExecuteDataTable());

                //제로페이
                this.FPS92_TY_S_HR_77ABJ076.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_BCSDE955", TYUserInfo.SecureKey, "Y", fsComPy, fsYEAR, fsKBSABUN, "G409Y");
                this.FPS92_TY_S_HR_77ABJ076.SetValue(this.DbConnector.ExecuteDataTable());
            }
            else if (Convert.ToInt16(fsYEAR) == 2020)
            {
                //신용카드
                this.FPS91_TY_S_HR_77AB3074.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_ACMIZ225", TYUserInfo.SecureKey, "Y", fsComPy, fsYEAR, fsKBSABUN, "G108M");
                this.FPS91_TY_S_HR_77AB3074.SetValue(this.DbConnector.ExecuteDataTable());

                //직불카드
                this.FPS91_TY_S_HR_77ABJ076.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_ACMIZ225", TYUserInfo.SecureKey, "Y", fsComPy, fsYEAR, fsKBSABUN, "G308M");
                this.FPS91_TY_S_HR_77ABJ076.SetValue(this.DbConnector.ExecuteDataTable());

                //제로페이
                this.FPS92_TY_S_HR_77ABJ076.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_ACMIZ225", TYUserInfo.SecureKey, "Y", fsComPy, fsYEAR, fsKBSABUN, "G408M");
                this.FPS92_TY_S_HR_77ABJ076.SetValue(this.DbConnector.ExecuteDataTable());
            }
            else
            {
                //신용카드
                this.FPS91_TY_S_HR_77AB3074.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_77AAZ071", TYUserInfo.SecureKey, "Y", fsComPy, fsYEAR, fsKBSABUN, Convert.ToInt16(fsYEAR) > 2017 ? "G107Y" : "G106Y");
                this.FPS91_TY_S_HR_77AB3074.SetValue(this.DbConnector.ExecuteDataTable());

                //직불카드
                this.FPS91_TY_S_HR_77ABJ076.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_77AAZ071", TYUserInfo.SecureKey, "Y", fsComPy, fsYEAR, fsKBSABUN, Convert.ToInt16(fsYEAR) > 2017 ? "G307Y" : "G306Y");
                this.FPS91_TY_S_HR_77ABJ076.SetValue(this.DbConnector.ExecuteDataTable());

                //제로페이
                this.FPS92_TY_S_HR_77ABJ076.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_77AAZ071", TYUserInfo.SecureKey, "Y", fsComPy, fsYEAR, fsKBSABUN, Convert.ToInt16(fsYEAR) > 2017 ? "G407Y" : "G406Y");
                this.FPS92_TY_S_HR_77ABJ076.SetValue(this.DbConnector.ExecuteDataTable());
            }

            
        }
        #endregion

        #region  Description : 현금영수증 자료 조회
        private void UP_DataBing_G206M()
        {
            this.FPS91_TY_S_HR_77ABS077.Initialize();

            if (Convert.ToInt16(fsYEAR) >= 2022)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_CCLGH411", fsComPy, fsYEAR, fsKBSABUN, TYUserInfo.SecureKey, "Y");
                this.FPS91_TY_S_HR_77ABS077.SetValue(this.DbConnector.ExecuteDataTable());
            }
            else if (Convert.ToInt16(fsYEAR) == 2021)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_BCSDG956", fsComPy, fsYEAR, fsKBSABUN, TYUserInfo.SecureKey, "Y");
                this.FPS91_TY_S_HR_77ABS077.SetValue(this.DbConnector.ExecuteDataTable());
            }
            else if (Convert.ToInt16(fsYEAR) == 2020)
            {

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_B1FAF320", fsComPy, fsYEAR, fsKBSABUN, TYUserInfo.SecureKey, "Y");
                this.FPS91_TY_S_HR_77ABS077.SetValue(this.DbConnector.ExecuteDataTable());
            }
            else
            {

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_77AFN097", fsComPy, fsYEAR, fsKBSABUN, TYUserInfo.SecureKey, "Y");
                this.FPS91_TY_S_HR_77ABS077.SetValue(this.DbConnector.ExecuteDataTable());
            }

        }
        #endregion

        #region  Description :주택임차차입금 원리금상환액 자료 조회
        private void UP_DataBing_J101Y()
        {
            this.FPS91_TY_S_HR_77ABV079.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77ABV078", TYUserInfo.SecureKey, "Y", fsComPy, fsYEAR, fsKBSABUN);
            this.FPS91_TY_S_HR_77ABV079.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region  Description :장기주택저당차입금 이자상환액 자료 조회
        private void UP_DataBing_J203Y()
        {
            this.FPS91_TY_S_HR_77ABZ081.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77ABY080", TYUserInfo.SecureKey, "Y", fsComPy, fsYEAR, fsKBSABUN);
            this.FPS91_TY_S_HR_77ABZ081.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region  Description :주택마련저축 자료 조회
        private void UP_DataBing_J301Y()
        {
            this.FPS91_TY_S_HR_77AD5088.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77AD2085", TYUserInfo.SecureKey, "Y", fsComPy, fsYEAR, fsKBSABUN);
            this.FPS91_TY_S_HR_77AD5088.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region  Description :기부금 자료 조회
        private void UP_DataBing_L102Y()
        {
            this.FPS91_TY_S_HR_77ADE090.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77ADE089", TYUserInfo.SecureKey, "Y", fsComPy, fsYEAR, fsKBSABUN);
            this.FPS91_TY_S_HR_77ADE090.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region  Description :장기집합추자증권저축 자료 조회
        private void UP_DataBing_N101Y()
        {
            this.FPS91_TY_S_HR_77ADJ092.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77ADI091", TYUserInfo.SecureKey, "Y", fsComPy, fsYEAR, fsKBSABUN);
            this.FPS91_TY_S_HR_77ADJ092.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region  Description :건강보험료 자료 조회
        private void UP_DataBing_O101Y()
        {
            this.FPS91_TY_S_HR_77ADM094.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77ADM093", TYUserInfo.SecureKey, "Y", fsComPy, fsYEAR, fsKBSABUN);
            this.FPS91_TY_S_HR_77ADM094.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region  Description :국민연금 자료 조회
        private void UP_DataBing_P102M()
        {
            this.FPS91_TY_S_HR_77ADP096.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77ADP095", TYUserInfo.SecureKey, "Y", fsComPy, fsYEAR, fsKBSABUN);
            this.FPS91_TY_S_HR_77ADP096.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description :월세액 자료 조회
        private void UP_DataBing_J501Y()
        {
            this.FPS91_TY_S_HR_ACMIS222.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_ACMIV224", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", fsComPy, fsYEAR, fsKBSABUN);
            this.FPS91_TY_S_HR_ACMIS222.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion


        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            _TXT01_COMPY_Value = fsComPy;
            _TXT01_SDATE_Value = TXT01_SDATE.GetValue();
            _CBH01_KBSABUN_Value = CBH01_KBSABUN.GetValue().ToString();
            _Login_Value = TYUserInfo.EmpNo;

            if (ds.Tables.Count > 0)
            {
                this.DbConnector.CommandClear();
                //보장성보험
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_7BLAM066", ds.Tables[0].Rows[i]["NSCOMPANY"].ToString(),
                                                                ds.Tables[0].Rows[i]["NSYEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["NSSABUN"].ToString(),
                                                                ds.Tables[0].Rows[i]["NSFORM_CD"].ToString(),
                                                                ds.Tables[0].Rows[i]["NSDAT_CD"].ToString(),
                                                                TYUserInfo.SecureKey, "Y", 
                                                                ds.Tables[0].Rows[i]["NSRESID"].ToString().Replace("-",""),
                                                                ds.Tables[0].Rows[i]["NSBUSNID"].ToString().Replace("-",""),
                                                                ds.Tables[0].Rows[i]["NSACC_NO"].ToString()
                                                                ); 
                }
                //의료비
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_7BLAQ067", ds.Tables[1].Rows[i]["NSCOMPANY"].ToString(),
                                                                ds.Tables[1].Rows[i]["NSYEAR"].ToString(),
                                                                ds.Tables[1].Rows[i]["NSSABUN"].ToString(),
                                                                ds.Tables[1].Rows[i]["NSFORM_CD"].ToString(),
                                                                ds.Tables[1].Rows[i]["NSDAT_CD"].ToString(),
                                                                TYUserInfo.SecureKey, "Y",
                                                                ds.Tables[1].Rows[i]["NSRESID"].ToString().Replace("-",""),
                                                                ds.Tables[1].Rows[i]["NSBUSNID"].ToString().Replace("-","")
                                                                );
                }
                //교육비
                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_7BLAT068", ds.Tables[2].Rows[i]["NSCOMPANY"].ToString(),
                                                                ds.Tables[2].Rows[i]["NSYEAR"].ToString(),
                                                                ds.Tables[2].Rows[i]["NSSABUN"].ToString(),
                                                                ds.Tables[2].Rows[i]["NSFORM_CD"].ToString(),
                                                                ds.Tables[2].Rows[i]["NSDAT_CD"].ToString(),
                                                                ds.Tables[2].Rows[i]["NSRESID"].ToString().Replace("-",""),
                                                                ds.Tables[2].Rows[i]["NSBUSNID"].ToString().Replace("-", ""),
                                                                ds.Tables[2].Rows[i]["NSEDU_TP"].ToString()
                                                                );
                }
                //직업훈련비 3
                for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_7BLAV069", ds.Tables[3].Rows[i]["NSCOMPANY"].ToString(),
                                                                ds.Tables[3].Rows[i]["NSYEAR"].ToString(),
                                                                ds.Tables[3].Rows[i]["NSSABUN"].ToString(),
                                                                ds.Tables[3].Rows[i]["NSFORM_CD"].ToString(),
                                                                ds.Tables[3].Rows[i]["NSDAT_CD"].ToString(),
                                                                TYUserInfo.SecureKey, "Y",
                                                                ds.Tables[3].Rows[i]["NSRESID"].ToString().Replace("-",""),
                                                                ds.Tables[3].Rows[i]["NSBUSNID"].ToString().Replace("-", ""),
                                                                ds.Tables[3].Rows[i]["NSCOURSE_CD"].ToString()
                                                                );
                }
                //교복구입비 4
                for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_7BLAY070", ds.Tables[4].Rows[i]["NSCOMPANY"].ToString(),
                                                                ds.Tables[4].Rows[i]["NSYEAR"].ToString(),
                                                                ds.Tables[4].Rows[i]["NSSABUN"].ToString(),
                                                                ds.Tables[4].Rows[i]["NSFORM_CD"].ToString(),
                                                                ds.Tables[4].Rows[i]["NSDAT_CD"].ToString(),
                                                                TYUserInfo.SecureKey, "Y",
                                                                ds.Tables[4].Rows[i]["NSRESID"].ToString().Replace("-",""),
                                                                ds.Tables[4].Rows[i]["NSBUSNID"].ToString().Replace("-","")
                                                                );
                }

                //개인연금저축 5
                for (int i = 0; i < ds.Tables[5].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_7BLB0071", ds.Tables[5].Rows[i]["NSCOMPANY"].ToString(),
                                                                ds.Tables[5].Rows[i]["NSYEAR"].ToString(),
                                                                ds.Tables[5].Rows[i]["NSSABUN"].ToString(),
                                                                ds.Tables[5].Rows[i]["NSFORM_CD"].ToString(),
                                                                ds.Tables[5].Rows[i]["NSDAT_CD"].ToString(),
                                                                TYUserInfo.SecureKey, "Y",
                                                                ds.Tables[5].Rows[i]["NSRESID"].ToString().Replace("-",""),
                                                                ds.Tables[5].Rows[i]["NSBUSNID"].ToString().Replace("-", ""),
                                                                ds.Tables[5].Rows[i]["NSACC_NO"].ToString()
                                                                );
                }
                //연금저축 6
                for (int i = 0; i < ds.Tables[6].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_7BLB3072", ds.Tables[6].Rows[i]["NSCOMPANY"].ToString(),
                                                                ds.Tables[6].Rows[i]["NSYEAR"].ToString(),
                                                                ds.Tables[6].Rows[i]["NSSABUN"].ToString(),
                                                                ds.Tables[6].Rows[i]["NSFORM_CD"].ToString(),
                                                                ds.Tables[6].Rows[i]["NSDAT_CD"].ToString(),
                                                                TYUserInfo.SecureKey, "Y",
                                                                ds.Tables[6].Rows[i]["NSRESID"].ToString().Replace("-",""),
                                                                ds.Tables[6].Rows[i]["NSBUSNID"].ToString().Replace("-", ""),
                                                                ds.Tables[6].Rows[i]["NSACC_NO"].ToString()
                                                                );
                }
                //퇴직연금 7
                for (int i = 0; i < ds.Tables[7].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_7BLBA073", ds.Tables[7].Rows[i]["NSCOMPANY"].ToString(),
                                                                ds.Tables[7].Rows[i]["NSYEAR"].ToString(),
                                                                ds.Tables[7].Rows[i]["NSSABUN"].ToString(),
                                                                ds.Tables[7].Rows[i]["NSFORM_CD"].ToString(),
                                                                ds.Tables[7].Rows[i]["NSDAT_CD"].ToString(),
                                                                TYUserInfo.SecureKey, "Y",
                                                                ds.Tables[7].Rows[i]["NSRESID"].ToString().Replace("-",""),
                                                                ds.Tables[7].Rows[i]["NSBUSNID"].ToString().Replace("-", ""),
                                                                ds.Tables[7].Rows[i]["NSACC_NO"].ToString()
                                                                );
                }
                //신용카드 8
                for (int i = 0; i < ds.Tables[8].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_7BLBG074", ds.Tables[8].Rows[i]["NSCOMPANY"].ToString(),
                                                                ds.Tables[8].Rows[i]["NSYEAR"].ToString(),
                                                                ds.Tables[8].Rows[i]["NSSABUN"].ToString(),
                                                                ds.Tables[8].Rows[i]["NSFORM_CD"].ToString(),
                                                                ds.Tables[8].Rows[i]["NSDAT_CD"].ToString(),
                                                                TYUserInfo.SecureKey, "Y",
                                                                ds.Tables[8].Rows[i]["NSRESID"].ToString().Replace("-",""),
                                                                ds.Tables[8].Rows[i]["NSBUSNID"].ToString().Replace("-", ""),
                                                                ds.Tables[8].Rows[i]["NSUSE_PLACE_CD"].ToString()
                                                                );
                }
                //현금영수증 9
                for (int i = 0; i < ds.Tables[9].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_7BLBL075", ds.Tables[9].Rows[i]["NSCOMPANY"].ToString(),
                                                                ds.Tables[9].Rows[i]["NSYEAR"].ToString(),
                                                                ds.Tables[9].Rows[i]["NSSABUN"].ToString(),
                                                                ds.Tables[9].Rows[i]["NSFORM_CD"].ToString(),
                                                                ds.Tables[9].Rows[i]["NSDAT_CD"].ToString(),
                                                                TYUserInfo.SecureKey, "Y", 
                                                                ds.Tables[9].Rows[i]["NSRESID"].ToString().Replace("-", ""),
                                                                ds.Tables[9].Rows[i]["NSUSE_PLACE_CD"].ToString()
                                                                );
                }
                //직불카드 10
                for (int i = 0; i < ds.Tables[10].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_7BLBG074", ds.Tables[10].Rows[i]["NSCOMPANY"].ToString(),
                                                                ds.Tables[10].Rows[i]["NSYEAR"].ToString(),
                                                                ds.Tables[10].Rows[i]["NSSABUN"].ToString(),
                                                                ds.Tables[10].Rows[i]["NSFORM_CD"].ToString(),
                                                                ds.Tables[10].Rows[i]["NSDAT_CD"].ToString(),
                                                                TYUserInfo.SecureKey, "Y",
                                                                ds.Tables[10].Rows[i]["NSRESID"].ToString().Replace("-",""),
                                                                ds.Tables[10].Rows[i]["NSBUSNID"].ToString().Replace("-", ""),
                                                                ds.Tables[10].Rows[i]["NSUSE_PLACE_CD"].ToString()
                                                                );
                }
                //주택임차원리금상환액 11
                for (int i = 0; i < ds.Tables[11].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_7BLBO076", ds.Tables[11].Rows[i]["NSCOMPANY"].ToString(),
                                                                ds.Tables[11].Rows[i]["NSYEAR"].ToString(),
                                                                ds.Tables[11].Rows[i]["NSSABUN"].ToString(),
                                                                ds.Tables[11].Rows[i]["NSFORM_CD"].ToString(),
                                                                ds.Tables[11].Rows[i]["NSDAT_CD"].ToString(),
                                                                TYUserInfo.SecureKey, "Y",
                                                                ds.Tables[11].Rows[i]["NSRESID"].ToString().Replace("-",""),
                                                                ds.Tables[11].Rows[i]["NSBUSNID"].ToString().Replace("-", ""),
                                                                ds.Tables[11].Rows[i]["NSACC_NO"].ToString()
                                                                );
                }
                //장기주택저당차입금 12
                for (int i = 0; i < ds.Tables[12].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_7BLBP077", ds.Tables[12].Rows[i]["NSCOMPANY"].ToString(),
                                                                ds.Tables[12].Rows[i]["NSYEAR"].ToString(),
                                                                ds.Tables[12].Rows[i]["NSSABUN"].ToString(),
                                                                ds.Tables[12].Rows[i]["NSFORM_CD"].ToString(),
                                                                ds.Tables[12].Rows[i]["NSDAT_CD"].ToString(),
                                                                TYUserInfo.SecureKey, "Y",
                                                                ds.Tables[12].Rows[i]["NSRESID"].ToString().Replace("-",""),
                                                                ds.Tables[12].Rows[i]["NSBUSNID"].ToString().Replace("-", ""),
                                                                ds.Tables[12].Rows[i]["NSACC_NO"].ToString(),
                                                                ds.Tables[12].Rows[i]["NSLEND_KD"].ToString()
                                                                );
                }
                //주택마련저축 13
                for (int i = 0; i < ds.Tables[13].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_7BLBV078", ds.Tables[13].Rows[i]["NSCOMPANY"].ToString(),
                                                                ds.Tables[13].Rows[i]["NSYEAR"].ToString(),
                                                                ds.Tables[13].Rows[i]["NSSABUN"].ToString(),
                                                                ds.Tables[13].Rows[i]["NSFORM_CD"].ToString(),
                                                                ds.Tables[13].Rows[i]["NSDAT_CD"].ToString(),
                                                                 TYUserInfo.SecureKey, "Y",
                                                                ds.Tables[13].Rows[i]["NSRESID"].ToString().Replace("-",""),
                                                                ds.Tables[13].Rows[i]["NSBUSNID"].ToString().Replace("-", ""),
                                                                ds.Tables[13].Rows[i]["NSACC_NO"].ToString(),
                                                                ds.Tables[13].Rows[i]["NSSAVING_GUBN"].ToString()
                                                                );
                }
                //기부금 14
                for (int i = 0; i < ds.Tables[14].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_7BLC5079", ds.Tables[14].Rows[i]["NSCOMPANY"].ToString(),
                                                                ds.Tables[14].Rows[i]["NSYEAR"].ToString(),
                                                                ds.Tables[14].Rows[i]["NSSABUN"].ToString(),
                                                                ds.Tables[14].Rows[i]["NSFORM_CD"].ToString(),
                                                                ds.Tables[14].Rows[i]["NSDAT_CD"].ToString(),
                                                                TYUserInfo.SecureKey, "Y",
                                                                ds.Tables[14].Rows[i]["NSRESID"].ToString().Replace("-",""),
                                                                ds.Tables[14].Rows[i]["NSBUSNID"].ToString().Replace("-", ""),
                                                                ds.Tables[14].Rows[i]["NSDONATION_CD"].ToString()
                                                                );
                }

                //장기집합투자 15
                for (int i = 0; i < ds.Tables[15].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_7BLH1081", ds.Tables[15].Rows[i]["NSCOMPANY"].ToString(),
                                                                ds.Tables[15].Rows[i]["NSYEAR"].ToString(),
                                                                ds.Tables[15].Rows[i]["NSSABUN"].ToString(),
                                                                ds.Tables[15].Rows[i]["NSFORM_CD"].ToString(),
                                                                ds.Tables[15].Rows[i]["NSDAT_CD"].ToString(),
                                                                TYUserInfo.SecureKey, "Y",
                                                                ds.Tables[15].Rows[i]["NSRESID"].ToString().Replace("-",""),
                                                                ds.Tables[15].Rows[i]["NSBUSNID"].ToString().Replace("-", ""),
                                                                ds.Tables[15].Rows[i]["NSSECU_NO"].ToString()
                                                                );
                }

                //교육비(학자금대출) 16
                for (int i = 0; i < ds.Tables[16].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_7C5DV178", ds.Tables[16].Rows[i]["NSCOMPANY"].ToString(),
                                                                ds.Tables[16].Rows[i]["NSYEAR"].ToString(),
                                                                ds.Tables[16].Rows[i]["NSSABUN"].ToString(),
                                                                ds.Tables[16].Rows[i]["NSFORM_CD"].ToString(),
                                                                ds.Tables[16].Rows[i]["NSDAT_CD"].ToString(),
                                                                TYUserInfo.SecureKey, "Y",
                                                                ds.Tables[16].Rows[i]["NSRESID"].ToString().Replace("-",""),
                                                                ds.Tables[16].Rows[i]["NSBUSNID"].ToString().Replace("-","")                                                                
                                                                );
                }
                //제로페이 17
                for (int i = 0; i < ds.Tables[17].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_7BLBG074", ds.Tables[17].Rows[i]["NSCOMPANY"].ToString(),
                                                                ds.Tables[17].Rows[i]["NSYEAR"].ToString(),
                                                                ds.Tables[17].Rows[i]["NSSABUN"].ToString(),
                                                                ds.Tables[17].Rows[i]["NSFORM_CD"].ToString(),
                                                                ds.Tables[17].Rows[i]["NSDAT_CD"].ToString(),
                                                                TYUserInfo.SecureKey, "Y",
                                                                ds.Tables[17].Rows[i]["NSRESID"].ToString().Replace("-", ""),
                                                                ds.Tables[17].Rows[i]["NSBUSNID"].ToString().Replace("-", ""),
                                                                ds.Tables[17].Rows[i]["NSUSE_PLACE_CD"].ToString()
                                                                );
                }
                //실손보험 18
                for (int i = 0; i < ds.Tables[18].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_D1HC0503", ds.Tables[18].Rows[i]["NSCOMPANY"].ToString(),
                                                                ds.Tables[18].Rows[i]["NSYEAR"].ToString(),
                                                                ds.Tables[18].Rows[i]["NSSABUN"].ToString(),
                                                                ds.Tables[18].Rows[i]["NSFORM_CD"].ToString(),
                                                                ds.Tables[18].Rows[i]["NSDAT_CD"].ToString(),
                                                                TYUserInfo.SecureKey, "Y",
                                                                ds.Tables[18].Rows[i]["NSRESID"].ToString().Replace("-", ""),
                                                                ds.Tables[18].Rows[i]["NSBUSNID"].ToString().Replace("-", ""),
                                                                ds.Tables[18].Rows[i]["NSACC_NO"].ToString()
                                                                );
                }


            }
            if (this.DbConnector.CommandCount > 0)
            {
                this.DbConnector.ExecuteTranQueryList();
            }

            this.BTN61_INQ_Click(null, null);            
            
        }
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            //보장성보험 0
            ds.Tables.Add(this.FPS91_TY_S_HR_777GB048.GetDataSourceInclude(TSpread.TActionType.Select, "NSCOMPANY", "NSYEAR", "NSSABUN", "NSFORM_CD", "NSDAT_CD", "NSRESID", "NSBUSNID","NSACC_NO"));
            //의료비 1
            ds.Tables.Add(this.FPS91_TY_S_HR_777H4051.GetDataSourceInclude(TSpread.TActionType.Select, "NSCOMPANY", "NSYEAR", "NSSABUN", "NSFORM_CD", "NSDAT_CD", "NSRESID", "NSBUSNID"));
            //교육비 2
            ds.Tables.Add(this.FPS91_TY_S_HR_777HG056.GetDataSourceInclude(TSpread.TActionType.Select, "NSCOMPANY", "NSYEAR", "NSSABUN", "NSFORM_CD", "NSDAT_CD", "NSRESID", "NSEDU_TP", "NSBUSNID"));
            //직업훈련비 3
            ds.Tables.Add(this.FPS91_TY_S_HR_777HQ058.GetDataSourceInclude(TSpread.TActionType.Select, "NSCOMPANY", "NSYEAR", "NSSABUN", "NSFORM_CD", "NSDAT_CD", "NSRESID", "NSBUSNID", "NSCOURSE_CD"));
            //교복구입비 4
            ds.Tables.Add(this.FPS91_TY_S_HR_777HT060.GetDataSourceInclude(TSpread.TActionType.Select, "NSCOMPANY", "NSYEAR", "NSSABUN", "NSFORM_CD", "NSDAT_CD", "NSRESID", "NSBUSNID"));
            //개인연금저축 5
            ds.Tables.Add(this.FPS91_TY_S_HR_777I3065.GetDataSourceInclude(TSpread.TActionType.Select, "NSCOMPANY", "NSYEAR", "NSSABUN", "NSFORM_CD", "NSDAT_CD", "NSRESID", "NSBUSNID","NSACC_NO"));
            //연금저축 6
            ds.Tables.Add(this.FPS91_TY_S_HR_77A8T068.GetDataSourceInclude(TSpread.TActionType.Select, "NSCOMPANY", "NSYEAR", "NSSABUN", "NSFORM_CD", "NSDAT_CD", "NSRESID", "NSBUSNID", "NSACC_NO"));
            //퇴직연금 7
            ds.Tables.Add(this.FPS91_TY_S_HR_77AAT070.GetDataSourceInclude(TSpread.TActionType.Select, "NSCOMPANY", "NSYEAR", "NSSABUN", "NSFORM_CD", "NSDAT_CD", "NSRESID", "NSBUSNID", "NSACC_NO"));
            //신용카드 8
            ds.Tables.Add(this.FPS91_TY_S_HR_77AB3074.GetDataSourceInclude(TSpread.TActionType.Select, "NSCOMPANY", "NSYEAR", "NSSABUN", "NSFORM_CD", "NSDAT_CD", "NSRESID", "NSBUSNID", "NSUSE_PLACE_CD"));
            //현금영수증 9
            ds.Tables.Add(this.FPS91_TY_S_HR_77ABS077.GetDataSourceInclude(TSpread.TActionType.Select, "NSCOMPANY", "NSYEAR", "NSSABUN", "NSFORM_CD", "NSDAT_CD", "NSRESID", "NSUSE_PLACE_CD"));
            //직불카드 10
            ds.Tables.Add(this.FPS91_TY_S_HR_77ABJ076.GetDataSourceInclude(TSpread.TActionType.Select, "NSCOMPANY", "NSYEAR", "NSSABUN", "NSFORM_CD", "NSDAT_CD", "NSRESID", "NSBUSNID", "NSUSE_PLACE_CD"));
            //주택임차원리금상환액 11
            ds.Tables.Add(this.FPS91_TY_S_HR_77ABV079.GetDataSourceInclude(TSpread.TActionType.Select, "NSCOMPANY", "NSYEAR", "NSSABUN", "NSFORM_CD", "NSDAT_CD", "NSRESID", "NSBUSNID", "NSACC_NO"));
            //장기주택저당차입금 12
            ds.Tables.Add(this.FPS91_TY_S_HR_77ABZ081.GetDataSourceInclude(TSpread.TActionType.Select, "NSCOMPANY", "NSYEAR", "NSSABUN", "NSFORM_CD", "NSDAT_CD", "NSRESID", "NSBUSNID", "NSACC_NO","NSLEND_KD"));
            //주택마련저축 13
            ds.Tables.Add(this.FPS91_TY_S_HR_77AD5088.GetDataSourceInclude(TSpread.TActionType.Select, "NSCOMPANY", "NSYEAR", "NSSABUN", "NSFORM_CD", "NSDAT_CD", "NSRESID", "NSBUSNID", "NSACC_NO", "NSSAVING_GUBN"));
            //기부금 14
            ds.Tables.Add(this.FPS91_TY_S_HR_77ADE090.GetDataSourceInclude(TSpread.TActionType.Select, "NSCOMPANY", "NSYEAR", "NSSABUN", "NSFORM_CD", "NSDAT_CD", "NSRESID", "NSBUSNID", "NSDONATION_CD"));
            //장기집합투자 15
            ds.Tables.Add(this.FPS91_TY_S_HR_77ADJ092.GetDataSourceInclude(TSpread.TActionType.Select, "NSCOMPANY", "NSYEAR", "NSSABUN", "NSFORM_CD", "NSDAT_CD", "NSRESID", "NSBUSNID", "NSSECU_NO"));
            //교육비(학자금대출) 16
            ds.Tables.Add(this.FPS91_TY_S_HR_7C5BN177.GetDataSourceInclude(TSpread.TActionType.Select, "NSCOMPANY", "NSYEAR", "NSSABUN", "NSFORM_CD", "NSDAT_CD", "NSRESID", "NSBUSNID"));
            //제로페이 17
            ds.Tables.Add(this.FPS92_TY_S_HR_77ABJ076.GetDataSourceInclude(TSpread.TActionType.Select, "NSCOMPANY", "NSYEAR", "NSSABUN", "NSFORM_CD", "NSDAT_CD", "NSRESID", "NSBUSNID", "NSUSE_PLACE_CD"));
            //실손보험 18
            ds.Tables.Add(this.FPS91_TY_S_HR_ACOAH238.GetDataSourceInclude(TSpread.TActionType.Select, "NSCOMPANY", "NSYEAR", "NSSABUN", "NSFORM_CD", "NSDAT_CD", "NSRESID", "NSBUSNID", "NSACC_NO"));

            if ( ds.Tables[0].Rows.Count == 0  &&
                 ds.Tables[1].Rows.Count == 0  &&
                 ds.Tables[2].Rows.Count == 0  &&
                 ds.Tables[3].Rows.Count == 0  &&
                ds.Tables[4].Rows.Count == 0  &&
                ds.Tables[5].Rows.Count == 0  &&
                ds.Tables[6].Rows.Count == 0  &&
                ds.Tables[7].Rows.Count == 0  &&
                ds.Tables[8].Rows.Count == 0  &&
                ds.Tables[9].Rows.Count == 0  &&
                ds.Tables[10].Rows.Count == 0  &&
                ds.Tables[11].Rows.Count == 0  &&
                ds.Tables[12].Rows.Count == 0  &&
                ds.Tables[13].Rows.Count == 0  &&
                ds.Tables[14].Rows.Count == 0  &&
                ds.Tables[15].Rows.Count == 0  &&
                ds.Tables[16].Rows.Count == 0  &&
                ds.Tables[17].Rows.Count == 0  &&
                ds.Tables[18].Rows.Count == 0 
                )
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

        private void BTN61_REM_InvokerStart(object sender, TButton.ClickEventCheckArgs e)
        {
            e.DbConnector.CommandClear();
            e.DbConnector.Attach("TY_P_HR_77JDB223", _TXT01_COMPY_Value, _TXT01_SDATE_Value, _CBH01_KBSABUN_Value, _Login_Value, TYUserInfo.SecureKey, "Y", "");
            e.DbConnector.ExecuteScalar();
        }

        private void BTN61_REM_InvokerEnd(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = e.ArgData as DataSet;

            if (ds.Tables[0].Rows[0][0].ToString().Substring(0, 2) != "OK")
            {
                this.ShowCustomMessage(ds.Tables[0].Rows[0][0].ToString(), "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            else
            {
                this.ShowMessage("TY_M_GB_23NAD874");
            }
        }
        #endregion

        #region  Description : 제출자료 등록 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            if ((new TYHRNT001B(fsComPy, fsYEAR, CBH01_KBSABUN.GetValue().ToString(), fsFixGubn, "2")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 확정 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            _TXT01_COMPY_Value = fsComPy;
            _TXT01_SDATE_Value = TXT01_SDATE.GetValue();
            _CBH01_KBSABUN_Value = CBH01_KBSABUN.GetValue().ToString();
            _Login_Value = TYUserInfo.EmpNo;
        }

        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            if (!this.ShowMessage("TY_M_HR_77JD4222"))
            {
                e.Successed = false;
                return;
            }
        }        

        private void BTN61_BATCH_InvokerStart(object sender, TButton.ClickEventCheckArgs e)
        {
            e.DbConnector.CommandClear();
            e.DbConnector.Attach("TY_P_HR_77JDB223", _TXT01_COMPY_Value, _TXT01_SDATE_Value, _CBH01_KBSABUN_Value, _Login_Value, TYUserInfo.SecureKey, "Y", "");
            e.DbConnector.ExecuteScalar();
        }

        private void BTN61_BATCH_InvokerEnd(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = e.ArgData as DataSet;

            if (ds.Tables[0].Rows[0][0].ToString().Substring(0, 2) != "OK")
            {
                this.ShowCustomMessage(ds.Tables[0].Rows[0][0].ToString(), "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            else
            {
               this.ShowMessage("TY_M_GB_3A81B997");
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

   

    }
}
