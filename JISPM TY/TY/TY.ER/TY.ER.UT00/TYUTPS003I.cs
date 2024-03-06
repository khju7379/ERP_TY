using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.UT00
{
    /// <summary>
    /// UTILITY 단가 등록 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.07.04 10:30
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_674AM536 : UTILITY 단가 등록
    ///  TY_P_UT_674AN537 : UTILITY 단가 수정
    ///  TY_P_UT_674DE549 : UTILITY 단가 확인
    ///  TY_P_UT_674FJ555 : 가열료 조회(UTILITY 단가 등록)
    ///  TY_P_UT_674FM556 : SK 가열료 수정(UTILITY 단가 등록)
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2BF7Y364 : 조회가 완료되었습니다.
    ///  TY_M_GB_43C9G671 : 삭제 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  DNYYMM : 년월
    ///  DNBKCU : 벙커C유
    ///  DNELECT : 전기료
    ///  DNJILSO : 질소사용료
    ///  DNKYUNG : 경유
    ///  DNMOTER1 : 모터용량
    ///  DNMOTER2 : 모터용량
    ///  DNSELAMT : 전기총사용금액
    ///  DNSELDANGA : 전기사용단가
    ///  DNSELECT : 전기료
    ///  DNSELTIM : 전기총사용시간
    ///  DNSKSTEAM : SK스팀
    ///  DNSKTAMT : SK스팀총사용금액
    ///  DNSTAMT : 스팀총금액
    ///  DNSTDANGA : 스팀단가
    ///  DNSTTIM : 스팀총사용시간
    ///  DNYUL : 효율
    /// </summary>
    public partial class TYUTPS003I : TYBase
    {
        private string fsDMCODE    = string.Empty;
        private string fsWK_GUBUN1 = string.Empty;
        private string fsWK_GUBUN2 = string.Empty;

        public TYUTPS003I(string sDMCODE)
        {
            InitializeComponent();

            fsDMCODE   = sDMCODE;
        }

        #region Description : 페이지 로드
        private void TYUTPS003I_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_B2PBG729, "DPNMODDATE");

            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_B2PBO733, "DMNMODDATE");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.BTN63_SAV.ProcessCheck += new TButton.CheckHandler(BTN63_SAV_ProcessCheck);
            this.BTN63_REM.ProcessCheck += new TButton.CheckHandler(BTN63_REM_ProcessCheck);
            this.BTN63_BATCH.ProcessCheck += new TButton.CheckHandler(BTN63_BATCH_ProcessCheck);

            UP_PUMP_Initialize();
            UP_MOTOR_Initialize();

            fsWK_GUBUN1 = "";
            fsWK_GUBUN2 = "";

            this.CBH01_DMPCODE.SetValue(fsDMCODE.ToString());

            this.CBH01_DMPCODE.SetReadOnly(true);

            // PUMP 이력 조회
            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.CBH01_DMPPUMPCD.CodeText);
        }
        #endregion

        #region Description : PUMP 정비 이력

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            fsWK_GUBUN1 = "NEW";

            UP_PUMP_Initialize();

            this.SetFocus(this.CBH01_DMPPUMPCD.CodeText);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            try
            {
                string sDMPHIGB = string.Empty;

                if (fsWK_GUBUN1.ToString() == "NEW")
                {
                    sDMPHIGB = "A";

                    DataTable dt = new DataTable();

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_B33AK773", this.CBH01_DMPCODE.GetValue().ToString().Trim(), this.CBH01_DMPPUMPCD.GetValue().ToString().Trim());

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.TXT01_DMPSEQ.SetValue(dt.Rows[0]["DMPSEQ"].ToString());
                    }
                }
                else
                {
                    if (Get_Numeric(this.TXT01_DMPSEQ.GetValue().ToString()) == "0")
                    {
                        sDMPHIGB = "A";

                        DataTable dt = new DataTable();

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_B33AK773", this.CBH01_DMPCODE.GetValue().ToString().Trim(), this.CBH01_DMPPUMPCD.GetValue().ToString().Trim());

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            this.TXT01_DMPSEQ.SetValue(dt.Rows[0]["DMPSEQ"].ToString());
                        }
                    }
                    else
                    {
                        sDMPHIGB = "C";
                    }
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_B33AD772", this.CBH01_DMPCODE.GetValue().ToString().Trim(),
                                                            this.CBH01_DMPPUMPCD.GetValue().ToString().Trim(),
                                                            Get_Numeric(this.TXT01_DMPSEQ.GetValue().ToString().Trim()),
                                                            Get_Date(this.DTP01_DMPBUYDATE.GetValue().ToString().Trim()),
                                                            this.TXT01_DMPMODEL.GetValue().ToString().Trim(),
                                                            this.CBH01_DMPMKCORP.GetValue().ToString().Trim(),
                                                            Get_Date(this.DTP01_DMPREGDATE.GetValue().ToString().Trim()),
                                                            this.TXT01_DMMREGDEV.GetValue().ToString().Trim(),
                                                            this.TXT01_DMPSEAL.GetValue().ToString().Trim(),
                                                            this.TXT01_DMPBEARAGO.GetValue().ToString().Trim(),
                                                            this.TXT01_DMPBEARAFT.GetValue().ToString().Trim(),
                                                            this.TXT01_DMPBAESQTY.GetValue().ToString().Trim(),
                                                            this.TXT01_DMPBUNHO.GetValue().ToString().Trim(),
                                                            sDMPHIGB.ToString(),
                                                            TYUserInfo.EmpNo,
                                                            this.CBH01_DMPCODE.GetValue().ToString().Trim(),
                                                            this.CBH01_DMPPUMPCD.GetValue().ToString().Trim(),
                                                            Get_Numeric(this.TXT01_DMPSEQ.GetValue().ToString().Trim()),
                                                            Get_Date(this.DTP01_DMPBUYDATE.GetValue().ToString().Trim()),
                                                            this.TXT01_DMPMODEL.GetValue().ToString().Trim(),
                                                            this.CBH01_DMPMKCORP.GetValue().ToString().Trim(),
                                                            Get_Date(this.DTP01_DMPREGDATE.GetValue().ToString().Trim()),
                                                            this.TXT01_DMMREGDEV.GetValue().ToString().Trim(),
                                                            this.TXT01_DMPSEAL.GetValue().ToString().Trim(),
                                                            this.TXT01_DMPBEARAGO.GetValue().ToString().Trim(),
                                                            this.TXT01_DMPBEARAFT.GetValue().ToString().Trim(),
                                                            this.TXT01_DMPBAESQTY.GetValue().ToString().Trim(),
                                                            this.TXT01_DMPBUNHO.GetValue().ToString().Trim(),
                                                            sDMPHIGB.ToString(),
                                                            TYUserInfo.EmpNo
                                                            );

                this.DbConnector.ExecuteNonQuery();

                this.ShowMessage("TY_M_GB_23NAD873");
            }
            catch (Exception ex)
            {
                string smsg = ex.ToString();
                this.ShowMessage("TY_M_AC_246A2488");
            }
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            try
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_B33BM774", this.CBH01_DMPCODE.GetValue().ToString().Trim(),
                                                            this.CBH01_DMPPUMPCD.GetValue().ToString().Trim(),
                                                            Get_Numeric(this.TXT01_DMPSEQ.GetValue().ToString().Trim()));

                this.DbConnector.ExecuteNonQuery();

                this.BTN61_INQ_Click(null, null);

                this.ShowMessage("TY_M_GB_23NAD874");
            }
            catch (Exception ex)
            {
                string smsg = ex.ToString();
                this.ShowMessage("TY_M_GB_43C9G671");
            }
        }
        #endregion

        #region Description : 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            try
            {
                DataTable dt = new DataTable();

                // 등록
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string sDPNNUM = string.Empty;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        // 순번 가져오기
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_B33DW784", this.CBH01_DMPCODE.GetValue().ToString().Trim(),
                                                                    this.CBH01_DMPPUMPCD.GetValue().ToString().Trim(),
                                                                    Get_Numeric(this.TXT01_DMPSEQ.GetValue().ToString().Trim()),
                                                                    Get_Date(ds.Tables[0].Rows[i]["DPNMODDATE"].ToString()));

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            sDPNNUM = dt.Rows[0]["DPNNUM"].ToString();
                        }


                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_B33DS783", this.CBH01_DMPCODE.GetValue().ToString().Trim(),
                                                                    this.CBH01_DMPPUMPCD.GetValue().ToString().Trim(),
                                                                    Get_Numeric(this.TXT01_DMPSEQ.GetValue().ToString().Trim()),
                                                                    Get_Date(ds.Tables[0].Rows[i]["DPNMODDATE"].ToString()),
                                                                    sDPNNUM.ToString(),
                                                                    ds.Tables[0].Rows[i]["DPNMODYDESC"].ToString(),
                                                                    ds.Tables[0].Rows[i]["DPNMODYTEAM"].ToString(),
                                                                    Get_Numeric(ds.Tables[0].Rows[i]["DPNAMOUNT"].ToString()),
                                                                    ds.Tables[0].Rows[i]["DPNBIGO"].ToString(),
                                                                    TYUserInfo.EmpNo
                                                                    );
                        this.DbConnector.ExecuteTranQuery();
                    }
                }

                // 수정
                if (ds.Tables[1].Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();

                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        this.DbConnector.Attach("TY_P_UT_B33DZ785", ds.Tables[1].Rows[i]["DPNMODYDESC"].ToString(),
                                                                    ds.Tables[1].Rows[i]["DPNMODYTEAM"].ToString(),
                                                                    Get_Numeric(ds.Tables[1].Rows[i]["DPNAMOUNT"].ToString()),
                                                                    ds.Tables[1].Rows[i]["DPNBIGO"].ToString(),
                                                                    TYUserInfo.EmpNo,
                                                                    this.CBH01_DMPCODE.GetValue().ToString().Trim(),
                                                                    this.CBH01_DMPPUMPCD.GetValue().ToString().Trim(),
                                                                    Get_Numeric(this.TXT01_DMPSEQ.GetValue().ToString().Trim()),
                                                                    Get_Date(ds.Tables[1].Rows[i]["DPNMODDATE"].ToString()),
                                                                    ds.Tables[1].Rows[i]["DPNNUM"].ToString()
                                                                    );
                    }

                    this.DbConnector.ExecuteTranQueryList();
                }

                // 삭제
                if (ds.Tables[2].Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();

                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                    {
                        this.DbConnector.Attach("TY_P_UT_B33E0786", this.CBH01_DMPCODE.GetValue().ToString().Trim(),
                                                                    this.CBH01_DMPPUMPCD.GetValue().ToString().Trim(),
                                                                    Get_Numeric(this.TXT01_DMPSEQ.GetValue().ToString().Trim()),
                                                                    Get_Date(ds.Tables[2].Rows[i]["DPNMODDATE"].ToString()),
                                                                    ds.Tables[2].Rows[i]["DPNNUM"].ToString()
                                                                    );
                    }

                    this.DbConnector.ExecuteTranQueryList();
                }

                this.ShowMessage("TY_M_MR_2BF50354");

                // PUMP 정비이력 조회
                UP_PUMP_MODIFY_SEARCH(this.CBH01_DMPCODE.GetValue().ToString().Trim(),
                                      this.CBH01_DMPPUMPCD.GetValue().ToString().Trim(),
                                      Get_Numeric(this.TXT01_DMPSEQ.GetValue().ToString().Trim()));
            }
            catch (Exception ex)
            {
                this.ShowMessage("TY_M_UT_71BDP399");
            }
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_B2PBG728", this.CBH01_DMPCODE.GetValue().ToString().Trim(),
                                                        this.CBH01_DMPPUMPCD.GetValue().ToString().Trim(),
                                                        Get_Numeric(this.TXT01_DMPSEQ.GetValue().ToString().Trim()));

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_UT_B33BN775");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 처리 체크
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_UT_B2PBG729.GetDataSourceInclude(TSpread.TActionType.New,    "DPNMODDATE", "DPNNUM", "DPNMODYDESC", "DPNMODYTEAM", "DPNAMOUNT", "DPNBIGO"));
            ds.Tables.Add(this.FPS91_TY_S_UT_B2PBG729.GetDataSourceInclude(TSpread.TActionType.Update, "DPNMODDATE", "DPNNUM", "DPNMODYDESC", "DPNMODYTEAM", "DPNAMOUNT", "DPNBIGO"));
            ds.Tables.Add(this.FPS91_TY_S_UT_B2PBG729.GetDataSourceInclude(TSpread.TActionType.Remove, "DPNMODDATE", "DPNNUM"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2CV43442");
                e.Successed = false;
                return;
            }
            else
            {
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_B339Q767", this.CBH01_DMPCODE.GetValue().ToString().Trim(),
                                                            this.CBH01_DMPPUMPCD.GetValue().ToString().Trim(),
                                                            Get_Numeric(this.TXT01_DMPSEQ.GetValue().ToString().Trim()));

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count < 0)
                {
                    this.ShowMessage("TY_M_UT_B33E3787");
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_MR_2BF50353"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : PUMP 조회
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_B2PBC726.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_B2PBC727", this.CBH01_DMPCODE.GetValue().ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_B2PBC726.SetValue(dt);
        }
        #endregion

        #region Description : PUMP 확인
        private void UP_PUMP_RUN(string sDMPCODE, string sDMPPUMPCD, string sDMPSEQ)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_B339Q767", sDMPCODE.ToString(), sDMPPUMPCD.ToString(), sDMPSEQ.ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsWK_GUBUN1 = "UPT";

                this.CurrentDataTableRowMapping(dt, "01");

                // PUMP 정비이력 조회
                UP_PUMP_MODIFY_SEARCH(sDMPCODE.ToString(), sDMPPUMPCD.ToString(), sDMPSEQ.ToString());

                this.CBH01_DMPPUMPCD.SetReadOnly(true);
                this.TXT01_DMPSEQ.SetReadOnly(true);

                this.SetFocus(this.TXT01_DMPMODEL);
            }
            else
            {
                this.CBH01_DMPPUMPCD.SetReadOnly(false);
                this.TXT01_DMPSEQ.SetReadOnly(false);
            }
        }
        #endregion

        #region Description : PUMP 정비이력 조회
        private void UP_PUMP_MODIFY_SEARCH(string sDPNCODE, string sDPNPUMPCD, string sDPNSEQ)
        {
            this.FPS91_TY_S_UT_B2PBG729.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_B2PBG728", sDPNCODE.ToString(), sDPNPUMPCD.ToString(), sDPNSEQ.ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_B2PBG729.SetValue(dt);
        }
        #endregion

        #region Description : PUMP 스프레드 이벤트
        private void FPS91_TY_S_UT_B2PBC726_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.CBH01_DMPCODE.SetValue(this.FPS91_TY_S_UT_B2PBC726.GetValue("DMPCODE").ToString());
            this.CBH01_DMPPUMPCD.SetValue(this.FPS91_TY_S_UT_B2PBC726.GetValue("DMPPUMPCD").ToString());
            this.TXT01_DMPSEQ.SetValue(this.FPS91_TY_S_UT_B2PBC726.GetValue("DMPSEQ").ToString());

            UP_PUMP_RUN(this.FPS91_TY_S_UT_B2PBC726.GetValue("DMPCODE").ToString(),
                        this.FPS91_TY_S_UT_B2PBC726.GetValue("DMPPUMPCD").ToString(),
                        this.FPS91_TY_S_UT_B2PBC726.GetValue("DMPSEQ").ToString());

            UP_PUMP_MODIFY_SEARCH(this.FPS91_TY_S_UT_B2PBC726.GetValue("DMPCODE").ToString(),
                                  this.FPS91_TY_S_UT_B2PBC726.GetValue("DMPPUMPCD").ToString(),
                                  this.FPS91_TY_S_UT_B2PBC726.GetValue("DMPSEQ").ToString());
        }
        #endregion

        #region Description : PUMP 필드 초기화
        private void UP_PUMP_Initialize()
        {
            this.FPS91_TY_S_UT_B2PBC726.Initialize();
            this.FPS91_TY_S_UT_B2PBG729.Initialize();

            this.TXT01_DMFLOW.SetValue("");
            this.TXT01_DMHEAD.SetValue("");
            this.TXT01_DMRPM.SetValue("");
            this.TXT01_DMTYPE.SetValue("");
            this.CBH01_DMCASING.SetValue("");
            this.CBH01_DMIMPELLER.SetValue("");
            this.CBH01_DMSHAFT.SetValue("");
            this.TXT01_DMMTCAPA.SetValue("");
            this.TXT01_DMPTDESC.SetValue("");
            this.TXT01_DMBIGO.SetValue("");

            this.CBH01_DMPPUMPCD.SetValue("");
            this.TXT01_DMPSEQ.SetValue("");
            this.DTP01_DMPBUYDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.TXT01_DMPMODEL.SetValue("");
            this.CBH01_DMPMKCORP.SetValue("");
            this.DTP01_DMPREGDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.TXT01_DMMREGDEV.SetValue("");
            this.TXT01_DMPSEAL.SetValue("");
            this.TXT01_DMPBEARAGO.SetValue("");
            this.TXT01_DMPBEARAFT.SetValue("");
            this.TXT01_DMPBAESQTY.SetValue("");
            this.TXT01_DMPTDESC.SetValue("");
            this.TXT01_DMPBUNHO.SetValue("");

            this.CBH01_DMPPUMPCD.SetReadOnly(false);
            this.TXT01_DMPSEQ.SetReadOnly(false);
        }
        #endregion

        #endregion







        #region Description : MOTOR 정비 이력

        #region Description : 신규 버튼
        private void BTN63_NEW_Click(object sender, EventArgs e)
        {
            fsWK_GUBUN2 = "NEW";

            UP_MOTOR_Initialize();

            this.SetFocus(this.CBH01_DMMMOTORCD.CodeText);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN63_SAV_Click(object sender, EventArgs e)
        {
            try
            {
                string sDMMHIGB = string.Empty;

                if (fsWK_GUBUN2.ToString() == "NEW")
                {
                    sDMMHIGB = "A";

                    DataTable dt = new DataTable();

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_B33G4792", this.CBH01_DMPCODE.GetValue().ToString().Trim(), this.CBH01_DMMMOTORCD.GetValue().ToString().Trim());

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.TXT01_DMMSEQ.SetValue(dt.Rows[0]["DMMSEQ"].ToString());
                    }
                }
                else
                {
                    if (Get_Numeric(this.TXT01_DMMSEQ.GetValue().ToString()) == "0")
                    {
                        sDMMHIGB = "A";

                        DataTable dt = new DataTable();

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_B33G4792", this.CBH01_DMPCODE.GetValue().ToString().Trim(), this.CBH01_DMMMOTORCD.GetValue().ToString().Trim());

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            this.TXT01_DMMSEQ.SetValue(dt.Rows[0]["DMMSEQ"].ToString());
                        }
                    }
                    else
                    {
                        sDMMHIGB = "C";
                    }
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_B33GC793", this.CBH01_DMPCODE.GetValue().ToString().Trim(),
                                                            this.CBH01_DMMMOTORCD.GetValue().ToString().Trim(),
                                                            Get_Numeric(this.TXT01_DMMSEQ.GetValue().ToString().Trim()),
                                                            this.TXT01_DMMMODEL.GetValue().ToString().Trim(),
                                                            Get_Date(this.DTP01_DMMBUYDATE.GetValue().ToString().Trim()),
                                                            this.CBH01_DMMMKCORP.GetValue().ToString().Trim(),
                                                            Get_Date(this.DTP01_DMMREGDATE.GetValue().ToString().Trim()),
                                                            this.TXT01_DMMREGDEV.GetValue().ToString().Trim(),
                                                            this.TXT01_DMMVOLTAGE.GetValue().ToString().Trim(),
                                                            this.TXT01_DMMFREQ.GetValue().ToString().Trim(),
                                                            this.TXT01_DMMPOLE.GetValue().ToString().Trim(),
                                                            this.TXT01_DMMRPM.GetValue().ToString().Trim(),
                                                            this.TXT01_DMMBEARAGO.GetValue().ToString().Trim(),
                                                            this.TXT01_DMMBEARAFT.GetValue().ToString().Trim(),
                                                            this.TXT01_DMMQUALITY.GetValue().ToString().Trim(),
                                                            this.TXT01_DMMOUTPUT.GetValue().ToString().Trim(),
                                                            sDMMHIGB.ToString(),
                                                            TYUserInfo.EmpNo,
                                                            this.CBH01_DMPCODE.GetValue().ToString().Trim(),
                                                            this.CBH01_DMMMOTORCD.GetValue().ToString().Trim(),
                                                            Get_Numeric(this.TXT01_DMMSEQ.GetValue().ToString().Trim()),
                                                            this.TXT01_DMMMODEL.GetValue().ToString().Trim(),
                                                            Get_Date(this.DTP01_DMMBUYDATE.GetValue().ToString().Trim()),
                                                            this.CBH01_DMMMKCORP.GetValue().ToString().Trim(),
                                                            Get_Date(this.DTP01_DMMREGDATE.GetValue().ToString().Trim()),
                                                            this.TXT01_DMMREGDEV.GetValue().ToString().Trim(),
                                                            this.TXT01_DMMVOLTAGE.GetValue().ToString().Trim(),
                                                            this.TXT01_DMMFREQ.GetValue().ToString().Trim(),
                                                            this.TXT01_DMMPOLE.GetValue().ToString().Trim(),
                                                            this.TXT01_DMMRPM.GetValue().ToString().Trim(),
                                                            this.TXT01_DMMBEARAGO.GetValue().ToString().Trim(),
                                                            this.TXT01_DMMBEARAFT.GetValue().ToString().Trim(),
                                                            this.TXT01_DMMQUALITY.GetValue().ToString().Trim(),
                                                            this.TXT01_DMMOUTPUT.GetValue().ToString().Trim(),
                                                            sDMMHIGB.ToString(),
                                                            TYUserInfo.EmpNo
                                                            );

                this.DbConnector.ExecuteNonQuery();

                this.ShowMessage("TY_M_GB_23NAD873");
            }
            catch (Exception ex)
            {
                string smsg = ex.ToString();
                this.ShowMessage("TY_M_AC_246A2488");
            }
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN63_REM_Click(object sender, EventArgs e)
        {
            try
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_B33GF794", this.CBH01_DMPCODE.GetValue().ToString().Trim(),
                                                            this.CBH01_DMMMOTORCD.GetValue().ToString().Trim(),
                                                            Get_Numeric(this.TXT01_DMMSEQ.GetValue().ToString().Trim()));

                this.DbConnector.ExecuteNonQuery();

                this.BTN63_INQ_Click(null, null);

                this.ShowMessage("TY_M_GB_23NAD874");
            }
            catch (Exception ex)
            {
                string smsg = ex.ToString();
                this.ShowMessage("TY_M_GB_43C9G671");
            }
        }
        #endregion

        #region Description : 처리 버튼
        private void BTN63_BATCH_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            try
            {
                DataTable dt = new DataTable();

                // 등록
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string sDMNNUM = string.Empty;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        // 순번 가져오기
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_B33GJ796", this.CBH01_DMPCODE.GetValue().ToString().Trim(),
                                                                    this.CBH01_DMMMOTORCD.GetValue().ToString().Trim(),
                                                                    Get_Numeric(this.TXT01_DMMSEQ.GetValue().ToString().Trim()),
                                                                    Get_Date(ds.Tables[0].Rows[i]["DMNMODDATE"].ToString()));

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            sDMNNUM = dt.Rows[0]["DMNNUM"].ToString();
                        }

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_B33HA797", this.CBH01_DMPCODE.GetValue().ToString().Trim(),
                                                                    this.CBH01_DMMMOTORCD.GetValue().ToString().Trim(),
                                                                    Get_Numeric(this.TXT01_DMMSEQ.GetValue().ToString().Trim()),
                                                                    Get_Date(ds.Tables[0].Rows[i]["DMNMODDATE"].ToString()),
                                                                    sDMNNUM.ToString(),
                                                                    ds.Tables[0].Rows[i]["DMNMODYDESC"].ToString(),
                                                                    ds.Tables[0].Rows[i]["DMNMODYTEAM"].ToString(),
                                                                    Get_Numeric(ds.Tables[0].Rows[i]["DMNAMOUNT"].ToString()),
                                                                    ds.Tables[0].Rows[i]["DMNBIGO"].ToString(),
                                                                    TYUserInfo.EmpNo
                                                                    );
                        this.DbConnector.ExecuteTranQuery();
                    }
                }

                // 수정
                if (ds.Tables[1].Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();

                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        this.DbConnector.Attach("TY_P_UT_B33HC798", ds.Tables[1].Rows[i]["DMNMODYDESC"].ToString(),
                                                                    ds.Tables[1].Rows[i]["DMNMODYTEAM"].ToString(),
                                                                    Get_Numeric(ds.Tables[1].Rows[i]["DMNAMOUNT"].ToString()),
                                                                    ds.Tables[1].Rows[i]["DMNBIGO"].ToString(),
                                                                    TYUserInfo.EmpNo,
                                                                    this.CBH01_DMPCODE.GetValue().ToString().Trim(),
                                                                    this.CBH01_DMMMOTORCD.GetValue().ToString().Trim(),
                                                                    Get_Numeric(this.TXT01_DMMSEQ.GetValue().ToString().Trim()),
                                                                    Get_Date(ds.Tables[1].Rows[i]["DMNMODDATE"].ToString()),
                                                                    ds.Tables[1].Rows[i]["DMNNUM"].ToString()
                                                                    );
                    }

                    this.DbConnector.ExecuteTranQueryList();
                }

                // 삭제
                if (ds.Tables[2].Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();

                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                    {
                        this.DbConnector.Attach("TY_P_UT_B33HD799", this.CBH01_DMPCODE.GetValue().ToString().Trim(),
                                                                    this.CBH01_DMMMOTORCD.GetValue().ToString().Trim(),
                                                                    Get_Numeric(this.TXT01_DMMSEQ.GetValue().ToString().Trim()),
                                                                    Get_Date(ds.Tables[2].Rows[i]["DMNMODDATE"].ToString()),
                                                                    ds.Tables[2].Rows[i]["DMNNUM"].ToString()
                                                                    );
                    }

                    this.DbConnector.ExecuteTranQueryList();
                }

                this.ShowMessage("TY_M_MR_2BF50354");

                // MOTOR 정비이력 조회
                UP_MOTOR_MODIFY_SEARCH();
            }
            catch (Exception ex)
            {
                this.ShowMessage("TY_M_UT_71BDP399");
            }
        }
        #endregion

        #region Description : 저장 체크
        private void BTN63_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN63_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            // 동력기계 MOTOR 정비 이력 확인
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_B2PBN732", this.CBH01_DMPCODE.GetValue().ToString().Trim(),
                                                        this.CBH01_DMMMOTORCD.GetValue().ToString().Trim(),
                                                        Get_Numeric(this.TXT01_DMMSEQ.GetValue().ToString().Trim()));

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_UT_B33BN775");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 처리 체크
        private void BTN63_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_UT_B2PBO733.GetDataSourceInclude(TSpread.TActionType.New,    "DMNMODDATE", "DMNNUM", "DMNMODYDESC", "DMNMODYTEAM", "DMNAMOUNT", "DMNBIGO"));
            ds.Tables.Add(this.FPS91_TY_S_UT_B2PBO733.GetDataSourceInclude(TSpread.TActionType.Update, "DMNMODDATE", "DMNNUM", "DMNMODYDESC", "DMNMODYTEAM", "DMNAMOUNT", "DMNBIGO"));
            ds.Tables.Add(this.FPS91_TY_S_UT_B2PBO733.GetDataSourceInclude(TSpread.TActionType.Remove, "DMNMODDATE", "DMNNUM"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2CV43442");
                e.Successed = false;
                return;
            }
            else
            {
                DataTable dt = new DataTable();

                // 동력기계 MOTOR 확인
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_B33FN791", this.CBH01_DMPCODE.GetValue().ToString().Trim(),
                                                            this.CBH01_DMMMOTORCD.GetValue().ToString().Trim(),
                                                            Get_Numeric(this.TXT01_DMMSEQ.GetValue().ToString().Trim()));

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count < 0)
                {
                    this.ShowMessage("TY_M_UT_B33E3787");
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_MR_2BF50353"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : MOTOR 조회
        private void BTN63_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_B2PBK731.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_B2PBK730", this.CBH01_DMPCODE.GetValue().ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_B2PBK731.SetValue(dt);
        }
        #endregion

        #region Description : MOTOR 확인
        private void UP_MOTOR_RUN()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_B33FN791", this.CBH01_DMPCODE.GetValue().ToString(),
                                                        this.CBH01_DMMMOTORCD.GetValue().ToString(),
                                                        this.TXT01_DMMSEQ.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsWK_GUBUN2 = "UPT";

                this.CurrentDataTableRowMapping(dt, "01");

                // MOTOR 정비이력 조회
                UP_MOTOR_MODIFY_SEARCH();

                this.CBH01_DMMMOTORCD.SetReadOnly(true);
                this.TXT01_DMMSEQ.SetReadOnly(true);

                this.SetFocus(this.TXT01_DMMMODEL);
            }
            else
            {
                this.CBH01_DMMMOTORCD.SetReadOnly(false);
                this.TXT01_DMMSEQ.SetReadOnly(false);
            }
        }
        #endregion

        #region Description : MOTOR 정비이력 조회
        private void UP_MOTOR_MODIFY_SEARCH()
        {
            this.FPS91_TY_S_UT_B2PBO733.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_B2PBN732", this.CBH01_DMPCODE.GetValue().ToString(),
                                                        this.CBH01_DMMMOTORCD.GetValue().ToString(),
                                                        this.TXT01_DMMSEQ.GetValue().ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_B2PBO733.SetValue(dt);
        }
        #endregion

        #region Description : MOTOR 스프레드 이벤트
        private void FPS91_TY_S_UT_B2PBK731_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.CBH01_DMPCODE.SetValue(this.FPS91_TY_S_UT_B2PBK731.GetValue("DMMCODE").ToString());
            this.CBH01_DMMMOTORCD.SetValue(this.FPS91_TY_S_UT_B2PBK731.GetValue("DMMMOTORCD").ToString());
            this.TXT01_DMMSEQ.SetValue(this.FPS91_TY_S_UT_B2PBK731.GetValue("DMMSEQ").ToString());

            UP_MOTOR_RUN();

            UP_MOTOR_MODIFY_SEARCH();
        }
        #endregion

        #region Description : MOTOR 필드 초기화
        private void UP_MOTOR_Initialize()
        {
            this.FPS91_TY_S_UT_B2PBK731.Initialize();
            this.FPS91_TY_S_UT_B2PBO733.Initialize();

            this.CBH01_DMMMOTORCD.SetValue("");
            this.TXT01_DMMSEQ.SetValue("");
            this.TXT01_DMMMODEL.SetValue("");
            this.DTP01_DMMBUYDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.CBH01_DMMMKCORP.SetValue("");
            this.DTP01_DMMREGDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.TXT01_DMMREGDEV.SetValue("");
            this.TXT01_DMMVOLTAGE.SetValue("");
            this.TXT01_DMMFREQ.SetValue("");
            this.TXT01_DMMPOLE.SetValue("");
            this.TXT01_DMMBEARAGO.SetValue("");
            this.TXT01_DMMBEARAFT.SetValue("");
            this.TXT01_DMMRPM.SetValue("");
            this.TXT01_DMMQUALITY.SetValue("");
            this.TXT01_DMMOUTPUT.SetValue("");

            this.CBH01_DMMMOTORCD.SetReadOnly(false);
            this.TXT01_DMMSEQ.SetReadOnly(false);
        }
        #endregion

        #endregion

        #region Description : 엔터키 이벤트(포커스 이동)
        private void TXT01_DMBIGO_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.BTN61_SAV);
            }
        }

        private void TXT01_DMMOUTPUT_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.BTN63_SAV);
            }
        }
        #endregion

        #region Description : 탭 이벤트
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)      // 펌프 정비이력
            {
                UP_PUMP_Initialize();

                this.BTN61_INQ_Click(null, null);
            }
            else if (tabControl1.SelectedIndex == 1) // 모터 정비이력
            {
                UP_MOTOR_Initialize();

                this.BTN63_INQ_Click(null, null);
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
    }
}
