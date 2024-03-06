using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;
using TY.Service.Library.Controls.TYSpreadCellType;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;

namespace TY.ER.US00
{
    /// <summary>
    /// 선급자재 관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2013.02.19 09:59
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_32J79125 : 선급자재 미생성 조회
    ///  TY_P_MR_32J7A126 : 선급자재 생성 조회
    ///  TY_P_MR_32J7A127 : 선급자재 DETAIL 조회
    ///  TY_P_MR_32J7A128 : 선급자재 DETAIL 하위 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_32J7C129 : 선급자재 생성 조회
    ///  TY_S_MR_32J7M130 : 선급자재 DETAIL 조회
    ///  TY_S_US_92LDO842 : 선급자재 DETAIL 하위 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CANCEL : 취소
    ///  CREATE : 생성
    ///  INQ : 조회
    ///  JASAN_CRE : 자산생성
    ///  JASAN_DEL : 자산삭제
    ///  JPNO_CRE : 전표생성
    ///  JPNO_DEL : 전표삭제
    ///  FXDDPMK : 귀속부서
    ///  FXDSAUP : 선급사업부
    ///  FXDGETDATE : 취득일
    ///  GCDACGHAP : 계정총액
    ///  GDAESANGHAP : 대상총액
    ///  GJANGHAP : 잔액
    /// </summary>
    public partial class TYUSME020I : TYBase
    {
        private string fsGUBUN   = string.Empty;

        private string fsUTWNJPNO  = string.Empty;
        private string fsUTJPDATE  = string.Empty;
        private string fsUTAMT1    = string.Empty;
        private string fsUTAMT2    = string.Empty;
        private string fsUTTAXCODE = string.Empty;

        #region Description : 페이지 로드
        public TYUSME020I()
        {
            InitializeComponent();
        }

        private void TYUSME020I_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_US_92LDO842.Initialize();

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            UP_BTN_DISPLAY("");

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            fsGUBUN = "";

            DataTable dt = new DataTable();

            this.FPS91_TY_S_US_92LDO842.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_92LDN841",
                Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                this.CBH01_SHWAJU.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_92LDO842.SetValue(dt);


            UP_BTN_DISPLAY("");
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            fsGUBUN = "NEW";

            UP_Field_ReadOnly(false,true);

            UP_Field_Clear();

            UP_BTN_DISPLAY(fsGUBUN);

            this.DTP01_UTJPDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            SetFocus(this.CBO01_UTMAECH);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            UP_Save();
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            UP_Del();
        }
        #endregion

        #region Description : 저장 메소드
        private void UP_Save()
        {
            if (fsGUBUN == "NEW")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92MFT879",
                                        Get_Date(this.DTP01_UTDATE.GetValue().ToString()).Trim(),
                                        Get_Date(this.DTP01_UTMCYYMM.GetValue().ToString()).Trim(),
                                        this.CBH01_UTHANGCHA.GetValue().ToString().Trim(),
                                        Get_Numeric(this.TXT01_UTSEQ.GetValue().ToString()).Trim(),
                                        this.CBH01_UTGOKJONG.GetValue().ToString().Trim(),
                                        this.CBH01_UTHWAJU.GetValue().ToString().Trim(),
                                        this.TXT01_UTBLMSN.GetValue().ToString().Trim(),
                                        this.TXT01_UTBLHSN.GetValue().ToString().Trim(),
                                        this.CBH01_UTYDHWAJU.GetValue().ToString().Trim(),
                                        this.CBH01_UTYSHWAJU.GetValue().ToString().Trim(),
                                        this.CBH01_UTCHHWAJU.GetValue().ToString().Trim(),
                                        Get_Date(this.MTB01_UTYSDATE.GetValue().ToString()).Trim(),
                                        Get_Numeric(this.TXT01_UTYSSEQ.GetValue().ToString()).Trim(),
                                        Get_Numeric(this.TXT01_UTYDSEQ.GetValue().ToString()).Trim(),
                                        Get_Numeric(this.TXT01_UTNUM.GetValue().ToString()).Trim(),
                                        this.CBO01_UTMAECH.GetValue().ToString().Trim(),
                                        this.TXT01_UTWNJPNO.GetValue().ToString().Trim(),
                                        this.DTP01_UTJPDATE.GetValue().ToString().Trim(),
                                        this.TXT01_UTJPNO.GetValue().ToString().Trim(),
                                        Get_Numeric(this.TXT01_UTAMT1.GetValue().ToString()).Trim(),
                                        Get_Numeric(this.TXT01_UTVAT1.GetValue().ToString()).Trim(),
                                        Get_Numeric(this.TXT01_UTAMT2.GetValue().ToString()).Trim(),
                                        Get_Numeric(this.TXT01_UTVAT2.GetValue().ToString()).Trim(),
                                        this.CBO01_UTTAXCODE.GetValue().ToString().Trim(),
                                        this.TXT01_UTRKAC.GetValue().ToString().Trim(),
                                        this.TXT01_UTBIGO.GetValue().ToString().Trim(),
                                        this.CBO01_UTSAYU.GetValue().ToString().Trim(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper()
                                        );

                this.DbConnector.ExecuteNonQuery();
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92MFV880",
                                        this.TXT01_UTWNJPNO.GetValue().ToString().Trim(),
                                        this.DTP01_UTJPDATE.GetValue().ToString().Trim(),
                                        this.TXT01_UTJPNO.GetValue().ToString().Trim(),
                                        Get_Numeric(this.TXT01_UTAMT1.GetValue().ToString()).Trim(),
                                        Get_Numeric(this.TXT01_UTVAT1.GetValue().ToString()).Trim(),
                                        Get_Numeric(this.TXT01_UTAMT2.GetValue().ToString()).Trim(),
                                        Get_Numeric(this.TXT01_UTVAT2.GetValue().ToString()).Trim(),
                                        this.CBO01_UTTAXCODE.GetValue().ToString().Trim(),
                                        this.TXT01_UTRKAC.GetValue().ToString().Trim(),
                                        this.TXT01_UTBIGO.GetValue().ToString().Trim(),
                                        this.CBO01_UTSAYU.GetValue().ToString().Trim(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper().Trim(),
                                        Get_Date(this.DTP01_UTDATE.GetValue().ToString()).Trim(),
                                        Get_Date(this.DTP01_UTMCYYMM.GetValue().ToString()).Trim(),
                                        this.CBH01_UTHANGCHA.GetValue().ToString().Trim(),
                                        Get_Numeric(this.TXT01_UTSEQ.GetValue().ToString()).Trim(),
                                        this.CBH01_UTGOKJONG.GetValue().ToString().Trim(),
                                        this.CBH01_UTHWAJU.GetValue().ToString().Trim(),
                                        this.TXT01_UTBLMSN.GetValue().ToString().Trim(),
                                        this.TXT01_UTBLHSN.GetValue().ToString().Trim(),
                                        this.CBH01_UTYDHWAJU.GetValue().ToString().Trim(),
                                        this.CBH01_UTYSHWAJU.GetValue().ToString().Trim(),
                                        this.CBH01_UTCHHWAJU.GetValue().ToString().Trim(),
                                        Get_Date(this.MTB01_UTYSDATE.GetValue().ToString()).Trim(),
                                        Get_Numeric(this.TXT01_UTYSSEQ.GetValue().ToString()).Trim(),
                                        Get_Numeric(this.TXT01_UTYDSEQ.GetValue().ToString()).Trim(),
                                        Get_Numeric(this.TXT01_UTNUM.GetValue().ToString()).Trim(),
                                        this.CBO01_UTMAECH.GetValue().ToString().Trim()
                                        );

                this.DbConnector.ExecuteNonQuery();
            }

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 삭제 메소드
        private void UP_Del()
        {
            // 삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_92MFV881",
                                    Get_Date(this.DTP01_UTDATE.GetValue().ToString()).Trim(),
                                    Get_Date(this.DTP01_UTMCYYMM.GetValue().ToString()).Trim(),
                                    this.CBH01_UTHANGCHA.GetValue().ToString().Trim(),
                                    Get_Numeric(this.TXT01_UTSEQ.GetValue().ToString()).Trim(),
                                    this.CBH01_UTGOKJONG.GetValue().ToString().Trim(),
                                    this.CBH01_UTHWAJU.GetValue().ToString().Trim(),
                                    this.TXT01_UTBLMSN.GetValue().ToString().Trim(),
                                    this.TXT01_UTBLHSN.GetValue().ToString().Trim(),
                                    this.CBH01_UTYDHWAJU.GetValue().ToString().Trim(),
                                    this.CBH01_UTYSHWAJU.GetValue().ToString().Trim(),
                                    this.CBH01_UTCHHWAJU.GetValue().ToString().Trim(),
                                    Get_Date(this.MTB01_UTYSDATE.GetValue().ToString()).Trim(),
                                    Get_Numeric(this.TXT01_UTYSSEQ.GetValue().ToString()).Trim(),
                                    Get_Numeric(this.TXT01_UTYDSEQ.GetValue().ToString()).Trim(),
                                    Get_Numeric(this.TXT01_UTNUM.GetValue().ToString()).Trim(),
                                    this.CBO01_UTMAECH.GetValue().ToString().Trim()
                                    );

            this.DbConnector.ExecuteNonQuery();

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 확인 메소드
        private void UP_RUN()
        {
            this.TXT01_UTAMT1.SetReadOnly(false);
            this.TXT01_UTAMT2.SetReadOnly(false);

            this.BTN61_JUNPYO_OK.Visible = false;
            this.BTN61_GBJUNPYO.Visible = false;

            this.BTN61_BTNJUNPYO.Visible = false;


            fsUTWNJPNO  = "";
            fsUTJPDATE  = "";
            fsUTAMT1    = "0";
            fsUTAMT2    = "0";
            fsUTTAXCODE = "";

            fsGUBUN = "";

            this.TXT01_UTAMT1.SetReadOnly(false);
            this.TXT01_UTAMT2.SetReadOnly(false);

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_92LEC843",
                Get_Date(this.DTP01_UTDATE.GetValue().ToString()).Trim(),
                Get_Date(this.DTP01_UTMCYYMM.GetValue().ToString()).Trim(),
                this.CBH01_UTHANGCHA.GetValue().ToString().Trim(),
                Get_Numeric(this.TXT01_UTSEQ.GetValue().ToString()).Trim(),
                this.CBH01_UTGOKJONG.GetValue().ToString().Trim(),
                this.CBH01_UTHWAJU.GetValue().ToString().Trim(),
                this.TXT01_UTBLMSN.GetValue().ToString().Trim(),
                this.TXT01_UTBLHSN.GetValue().ToString().Trim(),
                this.CBH01_UTYDHWAJU.GetValue().ToString().Trim(),
                this.CBH01_UTYSHWAJU.GetValue().ToString().Trim(),
                this.CBH01_UTCHHWAJU.GetValue().ToString().Trim(),
                Get_Date(this.MTB01_UTYSDATE.GetValue().ToString()).Trim(),
                Get_Numeric(this.TXT01_UTYSSEQ.GetValue().ToString()).Trim(),
                Get_Numeric(this.TXT01_UTYDSEQ.GetValue().ToString()).Trim(),
                Get_Numeric(this.TXT01_UTNUM.GetValue().ToString()).Trim(),
                this.CBO01_UTMAECH.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");

                fsUTWNJPNO  = dt.Rows[0]["UTWNJPNO"].ToString();
                fsUTJPDATE  = dt.Rows[0]["UTJPDATE"].ToString();
                fsUTAMT1    = dt.Rows[0]["UTAMT1"].ToString();
                fsUTAMT2    = dt.Rows[0]["UTAMT2"].ToString();
                fsUTTAXCODE = dt.Rows[0]["UTTAXCODE"].ToString();

                fsGUBUN = "UPT";

                if (this.TXT01_UTJPNO.GetValue().ToString() != "")
                {
                    this.TXT01_UTAMT1.SetReadOnly(true);
                    this.TXT01_UTAMT2.SetReadOnly(true);

                    this.BTN61_JUNPYO_OK.Visible = false;
                    this.BTN61_GBJUNPYO.Visible = true;

                    this.BTN61_BTNJUNPYO.Visible = false;
                }
                else
                {
                    this.TXT01_UTAMT1.SetReadOnly(false);
                    this.TXT01_UTAMT2.SetReadOnly(false);

                    this.BTN61_JUNPYO_OK.Visible = true;
                    this.BTN61_GBJUNPYO.Visible = false;

                    this.BTN61_BTNJUNPYO.Visible = true;
                }

                UP_Field_ReadOnly(true, true);
            }
            else
            {
                fsGUBUN = "NEW";
            }

            this.LBL51_UTAMT1.SetValue("공급가1");
            this.LBL51_UTAMT2.SetValue("공급가2");

            if (this.CBO01_UTMAECH.GetText().ToString() == "하역료" || this.CBO01_UTMAECH.GetText().ToString() == "보관료")
            {
                // 별첨화주 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92LGY845", this.CBH01_UTHWAJU.GetValue().ToString().Trim());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.LBL51_UTAMT1.SetValue("보관료");
                    this.LBL51_UTAMT2.SetValue("하역료");
                }
            }

            UP_BTN_DISPLAY(fsGUBUN);

            SetFocus(this.CBO01_UTSAYU);
        }
        #endregion

        #region Description : 저장 ProcessCheck
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt  = new DataTable();
            DataTable dt1 = new DataTable();

            if (this.CBO01_UTMAECH.GetText().ToString() == "하역료" || this.CBO01_UTMAECH.GetText().ToString() == "보관료")
            {
                // 별첨화주 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92LGY845", this.CBH01_UTHWAJU.GetValue().ToString().Trim());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.LBL51_UTAMT1.SetValue("보관료");
                    this.LBL51_UTAMT2.SetValue("하역료");
                }
            }

            if (double.Parse(Get_Numeric(this.TXT01_UTAMT1.GetValue().ToString())) != 0 && double.Parse(Get_Numeric(this.TXT01_UTVAT1.GetValue().ToString())) == 0)
            {
                this.ShowMessage("TY_M_US_937F4019");

                SetFocus(this.TXT01_UTVAT1);

                e.Successed = false;
                return;
            }

            if ((double.Parse(Get_Numeric(this.TXT01_UTAMT1.GetValue().ToString())) < 0 && double.Parse(Get_Numeric(this.TXT01_UTVAT1.GetValue().ToString())) > 0) ||
                (double.Parse(Get_Numeric(this.TXT01_UTAMT1.GetValue().ToString())) > 0 && double.Parse(Get_Numeric(this.TXT01_UTVAT1.GetValue().ToString())) < 0))
            {
                this.ShowMessage("TY_M_US_937FJ020");

                SetFocus(this.TXT01_UTVAT1);

                e.Successed = false;
                return;
            }



            if (double.Parse(Get_Numeric(this.TXT01_UTAMT2.GetValue().ToString())) != 0 && double.Parse(Get_Numeric(this.TXT01_UTVAT2.GetValue().ToString())) == 0)
            {
                this.ShowMessage("TY_M_US_937F4019");

                SetFocus(this.TXT01_UTVAT2);

                e.Successed = false;
                return;
            }

            if ((double.Parse(Get_Numeric(this.TXT01_UTAMT2.GetValue().ToString())) < 0 && double.Parse(Get_Numeric(this.TXT01_UTVAT2.GetValue().ToString())) > 0) ||
                (double.Parse(Get_Numeric(this.TXT01_UTAMT2.GetValue().ToString())) > 0 && double.Parse(Get_Numeric(this.TXT01_UTVAT2.GetValue().ToString())) < 0))
            {
                this.ShowMessage("TY_M_US_937FJ020");

                SetFocus(this.TXT01_UTVAT2);

                e.Successed = false;
                return;
            }


            if (this.CBO01_UTMAECH.GetText().ToString() == "시설사용료")
            {
                if (this.TXT01_UTSEQ.GetValue().ToString().Trim() == "")
                {
                    this.ShowMessage("TY_M_US_92LGM844");

                    SetFocus(this.TXT01_UTSEQ);

                    e.Successed = false;
                    return;
                }

                if (this.CBH01_UTGOKJONG.GetValue().ToString().Trim() == "")
                {
                    this.ShowMessage("TY_M_US_92LH1846");

                    SetFocus(this.CBH01_UTGOKJONG.CodeText);

                    e.Successed = false;
                    return;
                }

                // 매출 전표 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92M8K847", Get_Date(this.DTP01_UTDATE.GetValue().ToString()).Trim(),
                                                            Get_Date(this.DTP01_UTMCYYMM.GetValue().ToString()).Trim(),
                                                            this.CBH01_UTHANGCHA.GetValue().ToString().Trim(),
                                                            this.CBH01_UTGOKJONG.GetValue().ToString().Trim(),
                                                            this.CBH01_UTHWAJU.GetValue().ToString().Trim(),
                                                            this.TXT01_UTSEQ.GetValue().ToString().Trim()
                                                            );

                dt = this.DbConnector.ExecuteDataTable();
                
            }
            else if (this.CBO01_UTMAECH.GetText().ToString() == "하역료")
            {
                if (this.CBH01_UTGOKJONG.GetValue().ToString().Trim() == "")
                {
                    this.ShowMessage("TY_M_US_92LH1846");

                    SetFocus(this.CBH01_UTGOKJONG.CodeText);

                    e.Successed = false;
                    return;
                }

                // 매출 전표 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92M8W848", Get_Date(this.DTP01_UTDATE.GetValue().ToString()).Trim(),
                                                            Get_Date(this.DTP01_UTMCYYMM.GetValue().ToString()).Trim(),
                                                            this.CBH01_UTHANGCHA.GetValue().ToString().Trim(),
                                                            this.CBH01_UTGOKJONG.GetValue().ToString().Trim(),
                                                            this.CBH01_UTHWAJU.GetValue().ToString().Trim()
                                                            );

                dt = this.DbConnector.ExecuteDataTable();
            }
            else if (this.CBO01_UTMAECH.GetText().ToString() == "보관료")
            {
                if (this.CBH01_UTGOKJONG.GetValue().ToString().Trim() == "")
                {
                    this.ShowMessage("TY_M_US_92LH1846");

                    SetFocus(this.CBH01_UTGOKJONG.CodeText);

                    e.Successed = false;
                    return;
                }

                // 매출 전표 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92M8X849", Get_Date(this.DTP01_UTDATE.GetValue().ToString()).Trim(),
                                                            Get_Date(this.DTP01_UTMCYYMM.GetValue().ToString()).Trim(),
                                                            this.CBH01_UTHANGCHA.GetValue().ToString().Trim(),
                                                            this.CBH01_UTGOKJONG.GetValue().ToString().Trim(),
                                                            this.CBH01_UTHWAJU.GetValue().ToString().Trim(),
                                                            this.TXT01_UTBLMSN.GetValue().ToString().Trim(),
                                                            this.TXT01_UTBLHSN.GetValue().ToString().Trim(),
                                                            this.CBH01_UTYDHWAJU.GetValue().ToString().Trim(),
                                                            this.CBH01_UTYSHWAJU.GetValue().ToString().Trim(),
                                                            this.CBH01_UTCHHWAJU.GetValue().ToString().Trim(),
                                                            Get_Date(this.MTB01_UTYSDATE.GetValue().ToString()).Trim(),
                                                            this.TXT01_UTYSSEQ.GetValue().ToString().Trim(),
                                                            this.TXT01_UTYDSEQ.GetValue().ToString().Trim()
                                                            );

                dt = this.DbConnector.ExecuteDataTable();
            }
            else if (this.CBO01_UTMAECH.GetText().ToString() == "조출료")
            {
                if (this.CBH01_UTGOKJONG.GetValue().ToString().Trim() == "")
                {
                    this.ShowMessage("TY_M_US_92LH1846");

                    SetFocus(this.CBH01_UTGOKJONG.CodeText);

                    e.Successed = false;
                    return;
                }

                // 매출 전표 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92M95850", Get_Date(this.DTP01_UTDATE.GetValue().ToString()),
                                                            Get_Date(this.DTP01_UTMCYYMM.GetValue().ToString()),
                                                            this.CBH01_UTHANGCHA.GetValue().ToString(),
                                                            this.CBH01_UTGOKJONG.GetValue().ToString(),
                                                            this.CBH01_UTHWAJU.GetValue().ToString()
                                                            );

                dt = this.DbConnector.ExecuteDataTable();
            }
            else if (this.CBO01_UTMAECH.GetText().ToString() == "난작업비")
            {
                if (this.CBH01_UTGOKJONG.GetValue().ToString().Trim() == "")
                {
                    this.ShowMessage("TY_M_US_92LH1846");

                    SetFocus(this.CBH01_UTGOKJONG.CodeText);

                    e.Successed = false;
                    return;
                }

                // 매출 전표 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92M9C851", Get_Date(this.DTP01_UTDATE.GetValue().ToString()),
                                                            Get_Date(this.DTP01_UTMCYYMM.GetValue().ToString()),
                                                            this.CBH01_UTHANGCHA.GetValue().ToString(),
                                                            this.CBH01_UTGOKJONG.GetValue().ToString(),
                                                            this.CBH01_UTHWAJU.GetValue().ToString()
                                                            );

                dt = this.DbConnector.ExecuteDataTable();
            }
            else if (this.CBO01_UTMAECH.GetText().ToString() == "이송료")
            {
                if (this.CBH01_UTGOKJONG.GetValue().ToString().Trim() == "")
                {
                    this.ShowMessage("TY_M_US_92LH1846");

                    SetFocus(this.CBH01_UTGOKJONG.CodeText);

                    e.Successed = false;
                    return;
                }

                // 매출 전표 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92M9D852", Get_Date(this.DTP01_UTDATE.GetValue().ToString()),
                                                            Get_Date(this.DTP01_UTMCYYMM.GetValue().ToString()),
                                                            this.CBH01_UTHANGCHA.GetValue().ToString(),
                                                            this.CBH01_UTGOKJONG.GetValue().ToString(),
                                                            this.CBH01_UTHWAJU.GetValue().ToString()
                                                            );

                dt = this.DbConnector.ExecuteDataTable();
            }
            else if (this.CBO01_UTMAECH.GetText().ToString() == "소급분-시설사용료")
            {
                if (this.CBH01_UTGOKJONG.GetValue().ToString().Trim() == "")
                {
                    this.ShowMessage("TY_M_US_92LH1846");

                    SetFocus(this.CBH01_UTGOKJONG.CodeText);

                    e.Successed = false;
                    return;
                }

                if (this.TXT01_UTSEQ.GetValue().ToString().Trim() == "")
                {
                    this.ShowMessage("TY_M_US_92LGM844");

                    SetFocus(this.TXT01_UTSEQ);

                    e.Successed = false;
                    return;
                }

                // 매출 전표 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92M9G853", Get_Date(this.DTP01_UTDATE.GetValue().ToString()),
                                                            this.CBH01_UTHANGCHA.GetValue().ToString(),
                                                            this.CBH01_UTGOKJONG.GetValue().ToString(),
                                                            this.CBH01_UTHWAJU.GetValue().ToString(),
                                                            this.TXT01_UTSEQ.GetValue().ToString(),
                                                            "12"
                                                            );

                dt = this.DbConnector.ExecuteDataTable();
            }
            else if (this.CBO01_UTMAECH.GetText().ToString() == "소급분-하역료")
            {
                if (this.CBH01_UTGOKJONG.GetValue().ToString().Trim() == "")
                {
                    this.ShowMessage("TY_M_US_92LH1846");

                    SetFocus(this.CBH01_UTGOKJONG.CodeText);

                    e.Successed = false;
                    return;
                }

                if (this.TXT01_UTSEQ.GetValue().ToString().Trim() == "")
                {
                    this.ShowMessage("TY_M_US_92LGM844");

                    SetFocus(this.TXT01_UTSEQ);

                    e.Successed = false;
                    return;
                }

                // 매출 전표 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92M9G853", Get_Date(this.DTP01_UTDATE.GetValue().ToString()),
                                                            this.CBH01_UTHANGCHA.GetValue().ToString(),
                                                            this.CBH01_UTGOKJONG.GetValue().ToString(),
                                                            this.CBH01_UTHWAJU.GetValue().ToString(),
                                                            this.TXT01_UTSEQ.GetValue().ToString(),
                                                            "13"
                                                            );

                dt = this.DbConnector.ExecuteDataTable();
            }

            // 매출자료 존재 체크
            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_75VEA678");

                SetFocus(this.CBO01_UTMAECH);

                e.Successed = false;
                return;
            }
            else
            {
                // 마이너스 금액을 경우만 원천 전표 번호 체크 함.
                if (double.Parse(Get_Numeric(this.TXT01_UTAMT1.GetValue().ToString().Trim())) < 0 || double.Parse(Get_Numeric(this.TXT01_UTAMT2.GetValue().ToString().Trim())) < 0)
                {
                    if (this.TXT01_UTWNJPNO.GetValue().ToString().Trim() != SetDefaultValue(dt.Rows[0]["JPNO"].ToString()))
                    {
                        this.ShowMessage("TY_M_UT_75VEM682");

                        SetFocus(this.BTN61_BTNJUNPYO);

                        e.Successed = false;
                        return;
                    }

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_92MET870", this.TXT01_UTWNJPNO.GetValue().ToString().Trim().Substring(0, 6),
                                                                this.TXT01_UTWNJPNO.GetValue().ToString().Trim().Substring(6, 8),
                                                                this.TXT01_UTWNJPNO.GetValue().ToString().Trim().Substring(14, 3));

                    dt1= this.DbConnector.ExecuteDataTable();

                    if (dt1.Rows.Count > 0)
                    {
                        double dAMT1 = 0;
                        double dAMT2 = 0;

                        if (double.Parse(Get_Numeric(this.TXT01_UTAMT1.GetValue().ToString().Trim())) < 0)
                        {
                            dAMT1 = double.Parse(Get_Numeric(this.TXT01_UTAMT1.GetValue().ToString().Trim()));
                        }

                        if (double.Parse(Get_Numeric(this.TXT01_UTAMT2.GetValue().ToString().Trim())) < 0)
                        {
                            dAMT2 = double.Parse(Get_Numeric(this.TXT01_UTAMT2.GetValue().ToString().Trim()));
                        }

                        if (dAMT1 + dAMT2 + double.Parse(SetDefaultValue(dt1.Rows[0]["B7AMJN"].ToString())) < 0)
                        {
                            this.ShowMessage("TY_M_UT_75VEP684");

                            SetFocus(this.BTN61_BTNJUNPYO);

                            e.Successed = false;
                            return;
                        }
                    }
                }
                else
                {
                    if (this.TXT01_UTWNJPNO.GetValue().ToString().Trim() != "")
                    {
                        if (this.TXT01_UTWNJPNO.GetValue().ToString().Trim() != SetDefaultValue(dt.Rows[0]["JPNO"].ToString()))
                        {
                            this.ShowMessage("TY_M_UT_75VEM682");

                            SetFocus(this.BTN61_BTNJUNPYO);

                            e.Successed = false;
                            return;
                        }
                    }
                }
            }

            if (double.Parse(Get_Numeric(this.TXT01_UTAMT1.GetValue().ToString().Trim())) < 0 || double.Parse(Get_Numeric(this.TXT01_UTAMT2.GetValue().ToString().Trim())) < 0)
            {
                if (this.CBO01_UTSAYU.GetValue().ToString() == "05")
                {
                    if (Get_Date(this.DTP01_UTJPDATE.GetValue().ToString().Trim()) != Get_Date(this.DTP01_UTDATE.GetValue().ToString().Trim()))
                    {
                        this.ShowMessage("TY_M_UT_75VER685");

                        SetFocus(this.DTP01_UTJPDATE);

                        e.Successed = false;
                        return;
                    }
                }
            }






            // 미승인 전표 원천전표번호 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_92MEY871", this.TXT01_UTWNJPNO.GetValue().ToString().Trim().Substring(0, 6),
                                                        this.TXT01_UTWNJPNO.GetValue().ToString().Trim().Substring(6, 8),
                                                        this.TXT01_UTWNJPNO.GetValue().ToString().Trim().Substring(14, 3));

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_75VEM682");

                SetFocus(this.BTN61_BTNJUNPYO);

                e.Successed = false;
                return;
            }

            string sB2CDAC   = string.Empty;
            string sCDGUBUN1 = string.Empty;

            if (this.CBO01_UTMAECH.GetText().ToString() == "하역료" || this.CBO01_UTMAECH.GetText().ToString() == "보관료")
            {
                // 별첨화주 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92LGY845", this.CBH01_UTHWAJU.GetValue().ToString().Trim());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sCDGUBUN1 = "M";

                    // 공급가 체크
                    // (원금액 - 공급가) > 0 보다 커야 한다.
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_92MF0873", this.TXT01_UTWNJPNO.GetValue().ToString().Trim().Substring(0, 6),
                                                                this.TXT01_UTWNJPNO.GetValue().ToString().Trim().Substring(6, 8),
                                                                this.TXT01_UTWNJPNO.GetValue().ToString().Trim().Substring(14, 3));
                }
            }

            if (sCDGUBUN1.ToString() == "") // 일반화주(별첨화주 아님
            {
                if (this.CBO01_UTMAECH.GetText().ToString() == "접안료")
                {
                    sB2CDAC = "41200200";
                }
                else if (this.CBO01_UTMAECH.GetText().ToString() == "시설사용료")
                {
                    sB2CDAC = "41200500";
                }
                else if (this.CBO01_UTMAECH.GetText().ToString() == "하역료")
                {
                    sB2CDAC = "41200300";
                }
                else if (this.CBO01_UTMAECH.GetText().ToString() == "보관료")
                {
                    sB2CDAC = "41200100";
                }
                else if (this.CBO01_UTMAECH.GetText().ToString() == "조출료")
                {
                    sB2CDAC = "41200600";
                }
                else if (this.CBO01_UTMAECH.GetText().ToString() == "난작업비")
                {
                    sB2CDAC = "41201000";
                }
                else if (this.CBO01_UTMAECH.GetText().ToString() == "이송료")
                {
                    sB2CDAC = "41200400";
                }
                else if (this.CBO01_UTMAECH.GetText().ToString() == "소급분-시설사용료")
                {
                    sB2CDAC = "41200500";
                }
                else if (this.CBO01_UTMAECH.GetText().ToString() == "소급분-하역료")
                {
                    sB2CDAC = "41200300";
                }

                // 공급가 체크
                // (원금액 - 공급가) > 0 보다 커야 한다.
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92MF8872", this.TXT01_UTWNJPNO.GetValue().ToString().Trim().Substring(0, 6),
                                                            this.TXT01_UTWNJPNO.GetValue().ToString().Trim().Substring(6, 8),
                                                            this.TXT01_UTWNJPNO.GetValue().ToString().Trim().Substring(14, 3),
                                                            sB2CDAC.ToString());
            }


            // 공급가 체크
            // (원금액 - 공급가) > 0 보다 커야 한다.
            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        // 별첨화주일 경우 보관료
                        // 그외는 모든 매출
                        if (double.Parse(Get_Numeric(this.TXT01_UTAMT1.GetValue().ToString().Trim())) < 0)
                        {
                            if (double.Parse(dt.Rows[i]["B2AMCR"].ToString()) + double.Parse(Get_Numeric(this.TXT01_UTAMT1.GetValue().ToString().Trim())) < 0)
                            {
                                this.ShowMessage("TY_M_UT_75VIQ692");

                                SetFocus(this.TXT01_UTAMT1);

                                e.Successed = false;
                                return;
                            }
                        }
                    }
                    else
                    {
                        // 별첨화주일 경우 하역료
                        // 별첨화주만 해당 됨
                        if (double.Parse(Get_Numeric(this.TXT01_UTAMT2.GetValue().ToString().Trim())) < 0)
                        {
                            if (double.Parse(dt.Rows[i]["B2AMCR"].ToString()) + double.Parse(Get_Numeric(this.TXT01_UTAMT2.GetValue().ToString().Trim())) < 0)
                            {
                                this.ShowMessage("TY_M_UT_75VIQ692");

                                SetFocus(this.TXT01_UTAMT2);

                                e.Successed = false;
                                return;
                            }
                        }
                    }
                }
            }

            if (sCDGUBUN1.ToString() == "")
            {
                if (double.Parse(Get_Numeric(this.TXT01_UTAMT1.GetValue().ToString().Trim())) == 0)
                {
                    this.ShowMessage("TY_M_US_91SEY621");

                    SetFocus(this.TXT01_UTAMT1);

                    e.Successed = false;
                    return;
                }

                this.TXT01_UTAMT2.SetValue("0");
            }

            if (this.CBO01_UTTAXCODE.GetValue().ToString().Trim() != "61" && this.CBO01_UTTAXCODE.GetValue().ToString().Trim() != "62" && this.CBO01_UTTAXCODE.GetValue().ToString().Trim() != "13")
            {
                this.ShowMessage("TY_M_MR_34568457");

                SetFocus(this.CBO01_UTTAXCODE);

                e.Successed = false;
                return;
            }

            if (double.Parse(Get_Numeric(this.TXT01_UTAMT1.GetValue().ToString().Trim())) + double.Parse(Get_Numeric(this.TXT01_UTAMT2.GetValue().ToString().Trim())) == 0)
            {
                this.ShowMessage("TY_M_US_91SEY621");

                SetFocus(this.TXT01_UTAMT1);

                e.Successed = false;
                return;
            }

            if (fsGUBUN != "NEW")
            {
                if (fsGUBUN == "UPT")
                {
                    if (this.TXT01_UTJPNO.GetValue().ToString().Trim() != "")
                    {
                        this.ShowMessage("TY_M_US_7CIDF300");

                        SetFocus(this.TXT01_UTJPNO);

                        e.Successed = false;
                        return;
                    }
                }

                if (this.TXT01_UTJPNO.GetValue().ToString().Trim() != "")
                {
                    if (this.TXT01_UTJPNO.GetValue().ToString().Trim() != fsUTWNJPNO.ToString())
                    {
                        this.ShowMessage("TY_M_UT_75VEM682");

                        SetFocus(this.CBO01_UTMAECH);

                        e.Successed = false;
                        return;
                    }

                    if (double.Parse(Get_Numeric(this.TXT01_UTAMT1.GetValue().ToString().Trim())) != double.Parse(fsUTAMT1.ToString()))
                    {
                        this.ShowMessage("TY_M_UT_75VIV694");

                        SetFocus(this.TXT01_UTAMT1);

                        e.Successed = false;
                        return;
                    }

                    if (double.Parse(Get_Numeric(this.TXT01_UTAMT2.GetValue().ToString().Trim())) != double.Parse(fsUTAMT2.ToString()))
                    {
                        this.ShowMessage("TY_M_UT_75VIV694");

                        SetFocus(this.TXT01_UTAMT2);

                        e.Successed = false;
                        return;
                    }

                    if (Get_Date(this.DTP01_UTJPDATE.GetValue().ToString().Trim()) != fsUTJPDATE.ToString())
                    {
                        this.ShowMessage("TY_M_UT_75VIW695");

                        SetFocus(this.DTP01_UTJPDATE);

                        e.Successed = false;
                        return;
                    }

                    if (this.CBO01_UTTAXCODE.GetValue().ToString().Trim() != fsUTTAXCODE.ToString())
                    {
                        this.ShowMessage("TY_M_UT_75VIW696");

                        SetFocus(this.CBO01_UTTAXCODE);

                        e.Successed = false;
                        return;
                    }
                }
            }

            if (fsGUBUN == "NEW")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92MFG877", Get_Date(this.DTP01_UTDATE.GetValue().ToString()).Trim(),
                                                            Get_Date(this.DTP01_UTMCYYMM.GetValue().ToString()).Trim(),
                                                            this.CBH01_UTHANGCHA.GetValue().ToString().Trim(),
                                                            Get_Numeric(this.TXT01_UTSEQ.GetValue().ToString()).Trim(),
                                                            this.CBH01_UTGOKJONG.GetValue().ToString().Trim(),
                                                            this.CBH01_UTHWAJU.GetValue().ToString().Trim(),
                                                            this.TXT01_UTBLMSN.GetValue().ToString().Trim(),
                                                            this.TXT01_UTBLHSN.GetValue().ToString().Trim(),
                                                            this.CBH01_UTYDHWAJU.GetValue().ToString().Trim(),
                                                            this.CBH01_UTYSHWAJU.GetValue().ToString().Trim(),
                                                            this.CBH01_UTCHHWAJU.GetValue().ToString().Trim(),
                                                            Get_Date(this.MTB01_UTYSDATE.GetValue().ToString()).Trim(),
                                                            Get_Numeric(this.TXT01_UTYSSEQ.GetValue().ToString()).Trim(),
                                                            Get_Numeric(this.TXT01_UTYDSEQ.GetValue().ToString()).Trim(),
                                                            this.CBO01_UTMAECH.GetValue().ToString().Trim()
                                                            );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_UTNUM.SetValue(SetDefaultValue(dt.Rows[0]["NUM"].ToString()));
                }
            }

            // 저장하시겠습니까?
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 삭제 ProcessCheck
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sUTCONTNO = string.Empty;

            sUTCONTNO = this.TXT01_UTBLMSN.GetValue().ToString() + "-" + this.TXT01_UTBLHSN.GetValue().ToString();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_92MFJ878",
                Get_Date(this.DTP01_UTDATE.GetValue().ToString()).Trim(),
                Get_Date(this.DTP01_UTMCYYMM.GetValue().ToString()).Trim(),
                this.CBH01_UTHANGCHA.GetValue().ToString().Trim(),
                Get_Numeric(this.TXT01_UTSEQ.GetValue().ToString()).Trim(),
                this.CBH01_UTGOKJONG.GetValue().ToString().Trim(),
                this.CBH01_UTHWAJU.GetValue().ToString().Trim(),
                this.TXT01_UTBLMSN.GetValue().ToString().Trim(),
                this.TXT01_UTBLHSN.GetValue().ToString().Trim(),
                this.CBH01_UTYDHWAJU.GetValue().ToString().Trim(),
                this.CBH01_UTYSHWAJU.GetValue().ToString().Trim(),
                this.CBH01_UTCHHWAJU.GetValue().ToString().Trim(),
                Get_Date(this.MTB01_UTYSDATE.GetValue().ToString()).Trim(),
                Get_Numeric(this.TXT01_UTYSSEQ.GetValue().ToString()).Trim(),
                Get_Numeric(this.TXT01_UTYDSEQ.GetValue().ToString()).Trim(),
                Get_Numeric(this.TXT01_UTNUM.GetValue().ToString()).Trim(),
                this.CBO01_UTMAECH.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["UTJPNO"].ToString() != "")
                {
                    this.ShowMessage("TY_M_US_7CIDF300");

                    SetFocus(this.TXT01_UTBLMSN);

                    e.Successed = false;
                    return;
                }
            }

            // 삭제 하시겠습니까?
            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 스프레드 더블클릭
        private void FPS91_TY_S_US_92LDO842_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sUTMAECH = string.Empty;

            sUTMAECH = this.FPS91_TY_S_US_92LDO842.GetValue("UTMAECH").ToString().Substring(0, 2);

            this.CBO01_UTMAECH.SetValue("");
            this.DTP01_UTDATE.SetValue("");
            this.CBH01_UTGOKJONG.SetValue("");
            this.CBH01_UTHWAJU.SetValue("");
            this.CBH01_UTYDHWAJU.SetValue("");
            this.TXT01_UTYSSEQ.SetValue("");
            this.TXT01_UTBLMSN.SetValue("");
            this.TXT01_UTBLHSN.SetValue("");
            this.TXT01_UTSEQ.SetValue("");
            this.TXT01_UTNUM.SetValue("");

            UP_Field_Clear();

            this.CBO01_UTMAECH.SetValue(this.FPS91_TY_S_US_92LDO842.GetValue("UTMAECH").ToString().Substring(0, 2));

            this.DTP01_UTDATE.SetValue(this.FPS91_TY_S_US_92LDO842.GetValue("UTDATE").ToString());
            this.DTP01_UTMCYYMM.SetValue(this.FPS91_TY_S_US_92LDO842.GetValue("UTMCYYMM").ToString());

            this.CBH01_UTHANGCHA.SetValue(this.FPS91_TY_S_US_92LDO842.GetValue("UTHANGCHA").ToString());
            this.TXT01_UTSEQ.SetValue(this.FPS91_TY_S_US_92LDO842.GetValue("UTSEQ").ToString());
            this.CBH01_UTGOKJONG.SetValue(this.FPS91_TY_S_US_92LDO842.GetValue("UTGOKJONG").ToString());
            this.CBH01_UTHWAJU.SetValue(this.FPS91_TY_S_US_92LDO842.GetValue("UTHWAJU").ToString());
            this.TXT01_UTBLMSN.SetValue(this.FPS91_TY_S_US_92LDO842.GetValue("UTBLMSN").ToString());
            this.TXT01_UTBLHSN.SetValue(this.FPS91_TY_S_US_92LDO842.GetValue("UTBLHSN").ToString());
            this.CBH01_UTYDHWAJU.SetValue(this.FPS91_TY_S_US_92LDO842.GetValue("UTYDHWAJU").ToString());
            this.CBH01_UTYSHWAJU.SetValue(this.FPS91_TY_S_US_92LDO842.GetValue("UTYSHWAJU").ToString());
            this.CBH01_UTCHHWAJU.SetValue(this.FPS91_TY_S_US_92LDO842.GetValue("UTCHHWAJU").ToString());
            this.MTB01_UTYSDATE.SetValue(this.FPS91_TY_S_US_92LDO842.GetValue("UTYSDATE").ToString());
            this.TXT01_UTYSSEQ.SetValue(this.FPS91_TY_S_US_92LDO842.GetValue("UTYSSEQ").ToString());
            this.TXT01_UTYDSEQ.SetValue(this.FPS91_TY_S_US_92LDO842.GetValue("UTYDSEQ").ToString());

            this.TXT01_UTNUM.SetValue(this.FPS91_TY_S_US_92LDO842.GetValue("UTNUM").ToString());

            UP_RUN();
        }
        #endregion

        #region Description : 필드 ReadOnly
        private void UP_Field_ReadOnly(bool boolean1, bool boolean2)
        {
            this.CBO01_UTMAECH.SetReadOnly(boolean1);

            this.DTP01_UTDATE.SetReadOnly(boolean2);
            this.DTP01_UTMCYYMM.SetReadOnly(boolean2);
            this.CBH01_UTHANGCHA.SetReadOnly(boolean2);
            this.TXT01_UTSEQ.SetReadOnly(boolean2);
            this.CBH01_UTGOKJONG.SetReadOnly(boolean2);
            this.CBH01_UTHWAJU.SetReadOnly(boolean2);
            this.TXT01_UTBLMSN.SetReadOnly(boolean2);
            this.TXT01_UTBLHSN.SetReadOnly(boolean2);
            this.CBH01_UTYDHWAJU.SetReadOnly(boolean2);
            this.CBH01_UTYSHWAJU.SetReadOnly(boolean2);
            this.CBH01_UTCHHWAJU.SetReadOnly(boolean2);
            this.MTB01_UTYSDATE.SetReadOnly(boolean2);
            this.TXT01_UTYSSEQ.SetReadOnly(boolean2);
            this.TXT01_UTYDSEQ.SetReadOnly(boolean2);
            this.TXT01_UTNUM.SetReadOnly(boolean2);

            this.TXT01_UTAMT1.SetReadOnly(boolean1);
            this.TXT01_UTVAT1.SetReadOnly(boolean1);
            this.TXT01_UTAMT2.SetReadOnly(boolean1);
            this.TXT01_UTVAT2.SetReadOnly(boolean1);
        }
        #endregion

        #region Description : 버튼 디스플레이
        private void UP_BTN_DISPLAY(string sGUBUN)
        {
            if (sGUBUN == "NEW")
            {
                this.BTN61_SAV.Visible = true;
                this.BTN61_REM.Visible = false;

                this.BTN61_CODEHELP.Visible = true;
                this.BTN61_BTNJUNPYO.Visible    = true;

                this.BTN61_JUNPYO_OK.Visible    = false;
                this.BTN61_GBJUNPYO.Visible     = false;
            }
            else if (sGUBUN == "UPT")
            {
                this.BTN61_SAV.Visible = true;
                this.BTN61_REM.Visible = true;

                this.BTN61_CODEHELP.Visible = false;
            }
            else
            {
                this.BTN61_SAV.Visible = false;
                this.BTN61_REM.Visible = false;

                this.BTN61_CODEHELP.Visible = false;

                this.BTN61_BTNJUNPYO.Visible    = false;
                this.BTN61_JUNPYO_OK.Visible    = false;
                this.BTN61_GBJUNPYO.Visible     = false;
            }
        }
        #endregion

        #region Description : 필드 클리어
        private void UP_Field_Clear()
        {
            this.DTP01_UTDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_UTMCYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            this.CBH01_UTHANGCHA.SetValue("");
            this.TXT01_UTSEQ.SetValue("");
            this.CBH01_UTGOKJONG.SetValue("");
            this.CBH01_UTHWAJU.SetValue("");
            this.TXT01_UTBLMSN.SetValue("");
            this.TXT01_UTBLHSN.SetValue("");
            this.CBH01_UTYDHWAJU.SetValue("");
            this.CBH01_UTYSHWAJU.SetValue("");
            this.CBH01_UTCHHWAJU.SetValue("");
            this.MTB01_UTYSDATE.SetValue("");
            this.TXT01_UTYSSEQ.SetValue("");
            this.TXT01_UTYDSEQ.SetValue("");
            this.TXT01_UTNUM.SetValue("");

            this.TXT01_UTWNJPNO.SetValue("");
            this.TXT01_UTAMT1.SetValue("");
            this.TXT01_UTVAT1.SetValue("");
            this.TXT01_UTAMT2.SetValue("");
            this.TXT01_UTVAT2.SetValue("");
            this.TXT01_UTRKAC.SetValue("");
            this.TXT01_UTJPNO.SetValue("");
            this.TXT01_UTBIGO.SetValue("");

            this.CBO01_UTSAYU.SetValue("01");
            this.CBO01_UTTAXCODE.SetValue("61");
        }
        #endregion

        #region Description : 날짜 이벤트
        private void DTP01_EDDATE_KeyPress(object sender, KeyPressEventArgs e)
        {
            SetFocus(this.CBH01_SHWAJU.CodeText);
        }
        #endregion

        #region Description : 사유 이벤트
        private void TXT01_UTBIGO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.BTN61_SAV);
            }
        }
        #endregion

        #region Description : 원천전표번호 이벤트
        private void TXT01_UTWNJPNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                if (this.CBH01_UTHWAJU.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_UT_75THQ667");
                    SetFocus(this.CBO01_UTMAECH);

                    return;
                }

                TYUSME20C2 popup = new TYUSME20C2(this.CBO01_UTMAECH.GetText().ToString(), Get_Date(this.DTP01_UTDATE.GetValue().ToString()), this.CBH01_UTHWAJU.GetValue().ToString());

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.TXT01_UTWNJPNO.SetValue(popup.fsUTWNJPNO);

                    SetFocus(this.DTP01_UTJPDATE);
                }
            }
        }
        #endregion

        #region Description : 화주 이벤트
        private void CBH01_SHWAJU_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.BTN61_INQ);
            }

        }
        #endregion

        #region Description : 매출조회 코드헬프
        private void BTN61_CODEHELP_Click(object sender, EventArgs e)
        {
            TYUSME20C1 popup = new TYUSME20C1(this.CBO01_UTMAECH.GetText().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DTP01_UTDATE.SetValue(popup.fsUTDATE);
                this.DTP01_UTMCYYMM.SetValue(popup.fsUTMCYYMM);

                this.CBH01_UTHANGCHA.SetValue(popup.fsUTHANGCHA);
	            this.TXT01_UTSEQ.SetValue(popup.fsUTSEQ);
	            this.CBH01_UTGOKJONG.SetValue(popup.fsUTGOKJONG);
	            this.CBH01_UTHWAJU.SetValue(popup.fsUTHWAJU);
	            this.TXT01_UTBLMSN.SetValue(popup.fsUTBLMSN);
	            this.TXT01_UTBLHSN.SetValue(popup.fsUTBLHSN);
	            this.CBH01_UTYDHWAJU.SetValue(popup.fsUTYDHWAJU);
	            this.CBH01_UTYSHWAJU.SetValue(popup.fsUTYSHWAJU);
	            this.CBH01_UTCHHWAJU.SetValue(popup.fsUTCHHWAJU);
	            this.MTB01_UTYSDATE.SetValue(popup.fsUTYSDATE);
	            this.TXT01_UTYSSEQ.SetValue(popup.fsUTYSSEQ);
                this.TXT01_UTYDSEQ.SetValue(popup.fsUTYDSEQ);

                SetFocus(this.CBO01_UTSAYU);

                this.LBL51_UTAMT1.SetValue("공급가1");
                this.LBL51_UTAMT2.SetValue("공급가2");
            }
        }
        #endregion

        #region Description : 전표조회 코드헬프
        private void BTN61_GET_Click(object sender, EventArgs e)
        {
            if (this.CBH01_UTHWAJU.GetValue().ToString() == "")
            {
                this.ShowMessage("TY_M_UT_75THQ667");
                SetFocus(this.CBO01_UTMAECH);
                
                return;
            }

            TYUSME20C2 popup = new TYUSME20C2(this.CBO01_UTMAECH.GetText().ToString(), Get_Date(this.DTP01_UTDATE.GetValue().ToString()), this.CBH01_UTHWAJU.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_UTWNJPNO.SetValue(popup.fsUTWNJPNO);

                SetFocus(this.DTP01_UTJPDATE);
            }
        }
        #endregion

        #region Description : 전표발행 버튼
        private void BTN61_JUNPYO_OK_Click(object sender, EventArgs e)
        {
            string sB2DPMK = string.Empty;
            string sB2DTMK = string.Empty;
            string sB2NOSQ = string.Empty;

            string sUTCONTNO = string.Empty;

            sUTCONTNO = this.TXT01_UTBLMSN.GetValue().ToString() + "-" + this.TXT01_UTBLHSN.GetValue().ToString();

            DataTable dt = new DataTable();



            if (this.CBO01_UTMAECH.GetText().ToString() == "시설사용료")
            {
                // 매출 전표 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92M8K847", Get_Date(this.DTP01_UTDATE.GetValue().ToString()).Trim(),
                                                            Get_Date(this.DTP01_UTMCYYMM.GetValue().ToString()).Trim(),
                                                            this.CBH01_UTHANGCHA.GetValue().ToString().Trim(),
                                                            this.CBH01_UTGOKJONG.GetValue().ToString().Trim(),
                                                            this.CBH01_UTHWAJU.GetValue().ToString().Trim(),
                                                            this.TXT01_UTSEQ.GetValue().ToString().Trim()
                                                            );

                dt = this.DbConnector.ExecuteDataTable();

            }
            else if (this.CBO01_UTMAECH.GetText().ToString() == "하역료")
            {
                // 매출 전표 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92M8W848", Get_Date(this.DTP01_UTDATE.GetValue().ToString()).Trim(),
                                                            Get_Date(this.DTP01_UTMCYYMM.GetValue().ToString()).Trim(),
                                                            this.CBH01_UTHANGCHA.GetValue().ToString().Trim(),
                                                            this.CBH01_UTGOKJONG.GetValue().ToString().Trim(),
                                                            this.CBH01_UTHWAJU.GetValue().ToString().Trim()
                                                            );

                dt = this.DbConnector.ExecuteDataTable();
            }
            else if (this.CBO01_UTMAECH.GetText().ToString() == "보관료")
            {
                // 매출 전표 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92M8X849", Get_Date(this.DTP01_UTDATE.GetValue().ToString()).Trim(),
                                                            Get_Date(this.DTP01_UTMCYYMM.GetValue().ToString()).Trim(),
                                                            this.CBH01_UTHANGCHA.GetValue().ToString().Trim(),
                                                            this.CBH01_UTGOKJONG.GetValue().ToString().Trim(),
                                                            this.CBH01_UTHWAJU.GetValue().ToString().Trim(),
                                                            this.TXT01_UTBLMSN.GetValue().ToString().Trim(),
                                                            this.TXT01_UTBLHSN.GetValue().ToString().Trim(),
                                                            this.CBH01_UTYDHWAJU.GetValue().ToString().Trim(),
                                                            this.CBH01_UTYSHWAJU.GetValue().ToString().Trim(),
                                                            this.CBH01_UTCHHWAJU.GetValue().ToString().Trim(),
                                                            Get_Date(this.MTB01_UTYSDATE.GetValue().ToString()).Trim(),
                                                            this.TXT01_UTYSSEQ.GetValue().ToString().Trim(),
                                                            this.TXT01_UTYDSEQ.GetValue().ToString().Trim()
                                                            );

                dt = this.DbConnector.ExecuteDataTable();
            }
            else if (this.CBO01_UTMAECH.GetText().ToString() == "조출료")
            {
                // 매출 전표 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92M95850", Get_Date(this.DTP01_UTDATE.GetValue().ToString()),
                                                            Get_Date(this.DTP01_UTMCYYMM.GetValue().ToString()),
                                                            this.CBH01_UTHANGCHA.GetValue().ToString(),
                                                            this.CBH01_UTGOKJONG.GetValue().ToString(),
                                                            this.CBH01_UTHWAJU.GetValue().ToString()
                                                            );

                dt = this.DbConnector.ExecuteDataTable();
            }
            else if (this.CBO01_UTMAECH.GetText().ToString() == "난작업비")
            {
                // 매출 전표 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92M9C851", Get_Date(this.DTP01_UTDATE.GetValue().ToString()),
                                                            Get_Date(this.DTP01_UTMCYYMM.GetValue().ToString()),
                                                            this.CBH01_UTHANGCHA.GetValue().ToString(),
                                                            this.CBH01_UTGOKJONG.GetValue().ToString(),
                                                            this.CBH01_UTHWAJU.GetValue().ToString()
                                                            );

                dt = this.DbConnector.ExecuteDataTable();
            }
            else if (this.CBO01_UTMAECH.GetText().ToString() == "이송료")
            {
                // 매출 전표 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92M9D852", Get_Date(this.DTP01_UTDATE.GetValue().ToString()),
                                                            Get_Date(this.DTP01_UTMCYYMM.GetValue().ToString()),
                                                            this.CBH01_UTHANGCHA.GetValue().ToString(),
                                                            this.CBH01_UTGOKJONG.GetValue().ToString(),
                                                            this.CBH01_UTHWAJU.GetValue().ToString()
                                                            );

                dt = this.DbConnector.ExecuteDataTable();
            }
            else if (this.CBO01_UTMAECH.GetText().ToString() == "소급분-시설사용료")
            {
                // 매출 전표 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92M9G853", Get_Date(this.DTP01_UTDATE.GetValue().ToString()),
                                                            this.CBH01_UTHANGCHA.GetValue().ToString(),
                                                            this.CBH01_UTGOKJONG.GetValue().ToString(),
                                                            this.CBH01_UTHWAJU.GetValue().ToString(),
                                                            this.TXT01_UTSEQ.GetValue().ToString(),
                                                            "12"
                                                            );

                dt = this.DbConnector.ExecuteDataTable();
            }
            else if (this.CBO01_UTMAECH.GetText().ToString() == "소급분-하역료")
            {
                // 매출 전표 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92M9G853", Get_Date(this.DTP01_UTDATE.GetValue().ToString()),
                                                            this.CBH01_UTHANGCHA.GetValue().ToString(),
                                                            this.CBH01_UTGOKJONG.GetValue().ToString(),
                                                            this.CBH01_UTHWAJU.GetValue().ToString(),
                                                            this.TXT01_UTSEQ.GetValue().ToString(),
                                                            "13"
                                                            );

                dt = this.DbConnector.ExecuteDataTable();
            }

            // 매출별 전표번호 체크
            if (dt.Rows.Count > 0)
            {
                sB2DPMK = SetDefaultValue(dt.Rows[0]["JPNO"].ToString().Substring(0, 6));
                sB2DTMK = SetDefaultValue(dt.Rows[0]["JPNO"].ToString().Substring(6, 8));
                sB2NOSQ = SetDefaultValue(dt.Rows[0]["JPNO"].ToString().Substring(14, 3));

                // 마이너스 금액을 경우만 원천 전표 번호 체크 함.
                if (double.Parse(Get_Numeric(this.TXT01_UTAMT1.GetValue().ToString().Trim())) < 0 || double.Parse(Get_Numeric(this.TXT01_UTAMT2.GetValue().ToString().Trim())) < 0)
                {
                    if (this.TXT01_UTWNJPNO.GetValue().ToString().Trim() != SetDefaultValue(dt.Rows[0]["JPNO"].ToString()))
                    {
                        this.ShowMessage("TY_M_UT_75VEM682");

                        SetFocus(this.BTN61_BTNJUNPYO);
                        return;
                    }
                }
                else
                {
                    if (this.TXT01_UTWNJPNO.GetValue().ToString().Trim() != "")
                    {
                        if (this.TXT01_UTWNJPNO.GetValue().ToString().Trim() != SetDefaultValue(dt.Rows[0]["JPNO"].ToString()))
                        {
                            this.ShowMessage("TY_M_UT_75VEM682");
                            SetFocus(this.BTN61_BTNJUNPYO);

                            return;
                        }
                    }
                }
            }

            if (double.Parse(Get_Numeric(this.TXT01_UTAMT1.GetValue().ToString().Trim())) < 0 || double.Parse(Get_Numeric(this.TXT01_UTAMT2.GetValue().ToString().Trim())) < 0)
            {
                if (this.CBO01_UTSAYU.GetValue().ToString() == "05")
                {
                    if (Get_Date(this.DTP01_UTJPDATE.GetValue().ToString().Trim()) != Get_Date(this.DTP01_UTDATE.GetValue().ToString().Trim()))
                    {
                        this.ShowMessage("TY_M_UT_75VER685");
                        SetFocus(this.DTP01_UTJPDATE);

                        return;
                    }
                }
            }


            string sCDGUBUN1 = string.Empty;

            if (this.CBO01_UTMAECH.GetText().ToString() == "하역료" || this.CBO01_UTMAECH.GetText().ToString() == "보관료")
            {
                // 별첨화주 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92LGY845", this.CBH01_UTHWAJU.GetValue().ToString().Trim());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sCDGUBUN1 = "M";
                }
            }

            if (sCDGUBUN1.ToString() == "")
            {
                if (double.Parse(Get_Numeric(this.TXT01_UTAMT1.GetValue().ToString().Trim())) == 0)
                {
                    this.ShowMessage("TY_M_US_91SEY621");
                    SetFocus(this.TXT01_UTAMT1);

                    return;
                }

                this.TXT01_UTAMT2.SetValue("0");
            }



            string sCDAC01 = string.Empty;
            string sCDAC02 = string.Empty;
            string sCDAC03 = string.Empty;


            if (this.CBO01_UTMAECH.GetText().ToString() == "접안료")
            {
                sCDAC01 = "41200200";
            }
            else if (this.CBO01_UTMAECH.GetText().ToString() == "시설사용료")
            {
                sCDAC01 = "41200500";
            }
            else if (this.CBO01_UTMAECH.GetText().ToString() == "하역료")
            {
                if (sCDGUBUN1.ToString() == "M")
                {
                    if (double.Parse(Get_Numeric(this.TXT01_UTAMT1.GetValue().ToString().Trim())) != 0)
                    {
                        sCDAC01 = "41200100";
                    }

                    if (double.Parse(Get_Numeric(this.TXT01_UTAMT1.GetValue().ToString().Trim())) != 0)
                    {
                        sCDAC02 = "41200300";
                    }
                }
                else
                {
                    sCDAC01 = "41200300";
                }
            }
            else if (this.CBO01_UTMAECH.GetText().ToString() == "보관료")
            {
                if (sCDGUBUN1.ToString() == "M")
                {
                    if (double.Parse(Get_Numeric(this.TXT01_UTAMT1.GetValue().ToString().Trim())) != 0)
                    {
                        sCDAC01 = "41200100";
                    }

                    if (double.Parse(Get_Numeric(this.TXT01_UTAMT1.GetValue().ToString().Trim())) != 0)
                    {
                        sCDAC02 = "41200300";
                    }
                }
                else
                {
                    sCDAC01 = "41200100";
                }
            }
            else if (this.CBO01_UTMAECH.GetText().ToString() == "조출료")
            {
                sCDAC01 = "41200600";
            }
            else if (this.CBO01_UTMAECH.GetText().ToString() == "난작업비")
            {
                sCDAC01 = "41201000";
            }
            else if (this.CBO01_UTMAECH.GetText().ToString() == "이송료")
            {
                sCDAC01 = "41200400";
            }
            else if (this.CBO01_UTMAECH.GetText().ToString() == "소급분-시설사용료")
            {
                sCDAC01 = "41200500";
            }
            else if (this.CBO01_UTMAECH.GetText().ToString() == "소급분-하역료")
            {
                sCDAC01 = "41200300";
            }


            // 전표발행 SP
            string sOUTMSG = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_92PD0886", sB2DPMK.ToString(),
                                                        sB2DTMK.ToString(),
                                                        sB2NOSQ.ToString(),
                                                        "",
                                                        "",
                                                        "0",
                                                        sCDAC01.ToString(),
                                                        Get_Numeric(this.TXT01_UTAMT1.GetValue().ToString().Trim()),
                                                        Get_Numeric(this.TXT01_UTVAT1.GetValue().ToString().Trim()),
                                                        sCDAC02.ToString(),
                                                        Get_Numeric(this.TXT01_UTAMT2.GetValue().ToString().Trim()),
                                                        Get_Numeric(this.TXT01_UTVAT2.GetValue().ToString().Trim()),
                                                        sCDAC03.ToString(),
                                                        "0",
                                                        "0",
                                                        this.TXT01_UTRKAC.GetValue().ToString().Trim(),
                                                        this.CBH01_UTHWAJU.GetValue().ToString().Trim(),
                                                        this.CBO01_UTTAXCODE.GetValue().ToString(),
                                                        "1",
                                                        Get_Date(this.DTP01_UTJPDATE.GetValue().ToString()),
				                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
				                                        "A",
                                                        sOUTMSG.ToString()
                                                        );

            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUTMSG.Substring(0, 2) != "OK")
            {
                this.ShowMessage("TY_M_UT_76GG3847");
                SetFocus(this.TXT01_UTBIGO);

                return;
            }
            else
            {
                this.TXT01_UTJPNO.SetValue(sOUTMSG.ToString().Substring(3, 17));

                // 전표번호 업데이트
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92PE7888",
                                        this.TXT01_UTJPNO.GetValue().ToString().Trim(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper().Trim(),
                                        Get_Date(this.DTP01_UTDATE.GetValue().ToString()).Trim(),
                                        Get_Date(this.DTP01_UTMCYYMM.GetValue().ToString()).Trim(),
                                        this.CBH01_UTHANGCHA.GetValue().ToString().Trim(),
                                        Get_Numeric(this.TXT01_UTSEQ.GetValue().ToString()).Trim(),
                                        this.CBH01_UTGOKJONG.GetValue().ToString().Trim(),
                                        this.CBH01_UTHWAJU.GetValue().ToString().Trim(),
                                        this.TXT01_UTBLMSN.GetValue().ToString().Trim(),
                                        this.TXT01_UTBLHSN.GetValue().ToString().Trim(),
                                        this.CBH01_UTYDHWAJU.GetValue().ToString().Trim(),
                                        this.CBH01_UTYSHWAJU.GetValue().ToString().Trim(),
                                        this.CBH01_UTCHHWAJU.GetValue().ToString().Trim(),
                                        Get_Date(this.MTB01_UTYSDATE.GetValue().ToString()).Trim(),
                                        Get_Numeric(this.TXT01_UTYSSEQ.GetValue().ToString()).Trim(),
                                        Get_Numeric(this.TXT01_UTYDSEQ.GetValue().ToString()).Trim(),
                                        Get_Numeric(this.TXT01_UTNUM.GetValue().ToString()).Trim(),
                                        this.CBO01_UTMAECH.GetValue().ToString().Trim()
                                        );

                this.DbConnector.ExecuteNonQuery();

                this.BTN61_INQ_Click(null, null);

                this.ShowMessage("TY_M_UT_76GG4848");
            }
        }
        #endregion

        #region Description : 전표취소 버튼
        private void BTN61_GBJUNPYO_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            if (this.TXT01_UTJPNO.GetValue().ToString() == "")
            {
                this.ShowMessage("TY_M_UT_73K9X970");
                SetFocus(this.TXT01_UTBIGO);

                return;
            }

            if (this.TXT01_UTWNJPNO.GetValue().ToString().Trim() != fsUTWNJPNO.ToString())
            {
                this.ShowMessage("TY_M_UT_75VEM682");
                SetFocus(this.TXT01_UTWNJPNO);

                return;
            }

            if (double.Parse(Get_Numeric(this.TXT01_UTAMT1.GetValue().ToString().Trim())) != double.Parse(fsUTAMT1.ToString()))
            {
                this.ShowMessage("TY_M_UT_75VIV694");
                SetFocus(this.TXT01_UTAMT1);

                return;
            }

            if (double.Parse(Get_Numeric(this.TXT01_UTAMT2.GetValue().ToString().Trim())) != double.Parse(fsUTAMT2.ToString()))
            {
                this.ShowMessage("TY_M_UT_75VIV694");
                SetFocus(this.TXT01_UTAMT2);

                return;
            }

            if (Get_Date(this.DTP01_UTJPDATE.GetValue().ToString().Trim()) != fsUTJPDATE.ToString())
            {
                this.ShowMessage("TY_M_UT_75VIW695");
                SetFocus(this.DTP01_UTJPDATE);

                return;
            }

            if (this.CBO01_UTTAXCODE.GetValue().ToString().Trim() != fsUTTAXCODE.ToString())
            {
                this.ShowMessage("TY_M_UT_75VIW696");
                SetFocus(this.CBO01_UTTAXCODE);

                return;
            }


            string sCDGUBUN1 = string.Empty;

            if (this.CBO01_UTMAECH.GetText().ToString() == "하역료" || this.CBO01_UTMAECH.GetText().ToString() == "보관료")
            {
                // 별첨화주 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92LGY845", this.CBH01_UTHWAJU.GetValue().ToString().Trim());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sCDGUBUN1 = "M";
                }
            }

            if (sCDGUBUN1.ToString() == "")
            {
                if (double.Parse(Get_Numeric(this.TXT01_UTAMT1.GetValue().ToString().Trim())) == 0)
                {
                    this.ShowMessage("TY_M_US_91SEY621");
                    SetFocus(this.TXT01_UTAMT1);

                    return;
                }

                this.TXT01_UTAMT2.SetValue("0");
            }
            

            // 전표취소 SP

            // 미승인 전표 취소시 전자세금계산서에 자료 존재 유무 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_C3SBF197", "6108110449", this.TXT01_UTJPNO.GetValue().ToString().Trim());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_UT_73KHQ009");
                SetFocus(this.TXT01_UTBIGO);

                return;
            }

            string sB2DPMK = string.Empty;
            string sB2DTMK = string.Empty;
            string sB2NOSQ = string.Empty;

            string sJPDPMK = string.Empty;
            string sJPDTMK = string.Empty;
            string sJPNOSQ = string.Empty;

            sB2DPMK = SetDefaultValue(fsUTWNJPNO.ToString()).Substring(0, 6);
            sB2DTMK = SetDefaultValue(fsUTWNJPNO.ToString()).Substring(6, 8);
            sB2NOSQ = SetDefaultValue(fsUTWNJPNO.ToString()).Substring(14, 3);

            sJPDPMK = SetDefaultValue(this.TXT01_UTJPNO.GetValue().ToString().Trim()).Substring(0, 6);
            sJPDTMK = SetDefaultValue(this.TXT01_UTJPNO.GetValue().ToString().Trim()).Substring(6, 8);
            sJPNOSQ = SetDefaultValue(this.TXT01_UTJPNO.GetValue().ToString().Trim()).Substring(14, 3);

            string sCDAC01 = string.Empty;
            string sCDAC02 = string.Empty;
            string sCDAC03 = string.Empty;

            if (this.CBO01_UTMAECH.GetText().ToString() == "접안료")
            {
                sCDAC01 = "41200200";
            }
            else if (this.CBO01_UTMAECH.GetText().ToString() == "시설사용료")
            {
                sCDAC01 = "41200500";
            }
            else if (this.CBO01_UTMAECH.GetText().ToString() == "하역료")
            {
                if (sCDGUBUN1.ToString() == "M")
                {
                    if (double.Parse(Get_Numeric(this.TXT01_UTAMT1.GetValue().ToString().Trim())) != 0)
                    {
                        sCDAC01 = "41200100";
                    }

                    if (double.Parse(Get_Numeric(this.TXT01_UTAMT1.GetValue().ToString().Trim())) != 0)
                    {
                        sCDAC02 = "41200300";
                    }
                }
                else
                {
                    sCDAC01 = "41200300";
                }
            }
            else if (this.CBO01_UTMAECH.GetText().ToString() == "보관료")
            {
                if (sCDGUBUN1.ToString() == "M")
                {
                    if (double.Parse(Get_Numeric(this.TXT01_UTAMT1.GetValue().ToString().Trim())) != 0)
                    {
                        sCDAC01 = "41200100";
                    }

                    if (double.Parse(Get_Numeric(this.TXT01_UTAMT1.GetValue().ToString().Trim())) != 0)
                    {
                        sCDAC02 = "41200300";
                    }
                }
                else
                {
                    sCDAC01 = "41200100";
                }
            }
            else if (this.CBO01_UTMAECH.GetText().ToString() == "조출료")
            {
                sCDAC01 = "41200600";
            }
            else if (this.CBO01_UTMAECH.GetText().ToString() == "난작업비")
            {
                sCDAC01 = "41201000";
            }
            else if (this.CBO01_UTMAECH.GetText().ToString() == "이송료")
            {
                sCDAC01 = "41200400";
            }
            else if (this.CBO01_UTMAECH.GetText().ToString() == "소급분-시설사용료")
            {
                sCDAC01 = "41200500";
            }
            else if (this.CBO01_UTMAECH.GetText().ToString() == "소급분-하역료")
            {
                sCDAC01 = "41200300";
            }

            // 전표취소 SP
            string sOUTMSG = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_92PD0886", sB2DPMK.ToString(),
                                                        sB2DTMK.ToString(),
                                                        sB2NOSQ.ToString(),
                                                        sJPDPMK.ToString(),
                                                        sJPDTMK.ToString(),
                                                        sJPNOSQ.ToString(),
                                                        sCDAC01.ToString(),
                                                        Get_Numeric(this.TXT01_UTAMT1.GetValue().ToString().Trim()),
                                                        Get_Numeric(this.TXT01_UTVAT1.GetValue().ToString().Trim()),
                                                        sCDAC02.ToString(),
                                                        Get_Numeric(this.TXT01_UTAMT2.GetValue().ToString().Trim()),
                                                        Get_Numeric(this.TXT01_UTVAT2.GetValue().ToString().Trim()),
                                                        sCDAC03.ToString(),
                                                        "0",
                                                        "0",
                                                        this.TXT01_UTRKAC.GetValue().ToString().Trim(),
                                                        this.CBH01_UTHWAJU.GetValue().ToString().Trim(),
                                                        this.CBO01_UTTAXCODE.GetValue().ToString(),
                                                        "1",
                                                        Get_Date(this.DTP01_UTJPDATE.GetValue().ToString()),
                                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                                        "D",
                                                        sOUTMSG.ToString()
                                                        );

            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUTMSG.Substring(0, 2) != "OK")
            {
                this.ShowMessage("TY_M_UT_76GGA849");
                SetFocus(this.TXT01_UTBIGO);

                return;
            }
            else
            {
                this.TXT01_UTJPNO.SetValue("");

                // 전표번호 업데이트
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92PE7888",
                                        this.TXT01_UTJPNO.GetValue().ToString().Trim(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper().Trim(),
                                        Get_Date(this.DTP01_UTDATE.GetValue().ToString()).Trim(),
                                        Get_Date(this.DTP01_UTMCYYMM.GetValue().ToString()).Trim(),
                                        this.CBH01_UTHANGCHA.GetValue().ToString().Trim(),
                                        Get_Numeric(this.TXT01_UTSEQ.GetValue().ToString()).Trim(),
                                        this.CBH01_UTGOKJONG.GetValue().ToString().Trim(),
                                        this.CBH01_UTHWAJU.GetValue().ToString().Trim(),
                                        this.TXT01_UTBLMSN.GetValue().ToString().Trim(),
                                        this.TXT01_UTBLHSN.GetValue().ToString().Trim(),
                                        this.CBH01_UTYDHWAJU.GetValue().ToString().Trim(),
                                        this.CBH01_UTYSHWAJU.GetValue().ToString().Trim(),
                                        this.CBH01_UTCHHWAJU.GetValue().ToString().Trim(),
                                        Get_Date(this.MTB01_UTYSDATE.GetValue().ToString()).Trim(),
                                        Get_Numeric(this.TXT01_UTYSSEQ.GetValue().ToString()).Trim(),
                                        Get_Numeric(this.TXT01_UTYDSEQ.GetValue().ToString()).Trim(),
                                        Get_Numeric(this.TXT01_UTNUM.GetValue().ToString()).Trim(),
                                        this.CBO01_UTMAECH.GetValue().ToString().Trim()
                                        );

                this.DbConnector.ExecuteNonQuery();

                this.BTN61_INQ_Click(null, null);

                this.ShowMessage("TY_M_UT_76GGB850");
            }
        }
        #endregion

        private void CBO01_UTMAECH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                TYUSME20C1 popup = new TYUSME20C1(this.CBO01_UTMAECH.GetText().ToString());

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.DTP01_UTDATE.SetValue(popup.fsUTDATE);
                    this.DTP01_UTMCYYMM.SetValue(popup.fsUTMCYYMM);

                    this.CBH01_UTHANGCHA.SetValue(popup.fsUTHANGCHA);
                    this.TXT01_UTSEQ.SetValue(popup.fsUTSEQ);
                    this.CBH01_UTGOKJONG.SetValue(popup.fsUTGOKJONG);
                    this.CBH01_UTHWAJU.SetValue(popup.fsUTHWAJU);
                    this.TXT01_UTBLMSN.SetValue(popup.fsUTBLMSN);
                    this.TXT01_UTBLHSN.SetValue(popup.fsUTBLHSN);
                    this.CBH01_UTYDHWAJU.SetValue(popup.fsUTYDHWAJU);
                    this.CBH01_UTYSHWAJU.SetValue(popup.fsUTYSHWAJU);
                    this.CBH01_UTCHHWAJU.SetValue(popup.fsUTCHHWAJU);
                    this.MTB01_UTYSDATE.SetValue(popup.fsUTYSDATE);
                    this.TXT01_UTYSSEQ.SetValue(popup.fsUTYSSEQ);
                    this.TXT01_UTYDSEQ.SetValue(popup.fsUTYDSEQ);

                    SetFocus(this.CBO01_UTSAYU);

                    //this.LBL51_UTAMT1.SetValue("공급가1");
                    //this.LBL51_UTAMT2.SetValue("공급가2");

                    //if (this.CBO01_UTMAECH.GetValue().ToString() == "05") // 보관취급료
                    //{
                    //    this.LBL51_UTAMT1.SetValue("보관료");
                    //    this.LBL51_UTAMT2.SetValue("취급료");
                    //}
                }
            }
        }
    }
}