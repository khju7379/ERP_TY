using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.ER.GB00;

namespace TY.ER.US00
{
    /// <summary>
    /// 수동 BIN 출고지시 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2019.08.13 16:46
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_98DEQ099 : 수동 BIN 출고지시 조회
    ///  TY_P_US_98DET100 : 수동 BIN 출고지시 등록
    ///  TY_P_US_98DEX101 : 수동 BIN 충고지시 수정
    ///  TY_P_US_98DEY102 : 수동 BIN 출고지시 삭제
    ///  TY_P_US_98DF3103 : BIN 상태관리 업데이트(수동 BIN 출고지시)
    ///  TY_P_US_98DFA104 : 수동 BIN 출고지시 확인
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_98DFN106 : 수동 BIN 출고지시 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_43C9G671 : 삭제 작업을 실패했습니다.
    ///  TY_M_US_91SEN618 : 이미 등록 된 자료입니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  BNGOKJONG : 곡종
    ///  BNHANGCHA : 항차
    ///  BNSOSOK : 협회
    ///  BNTJHJ : 화주
    ///  BNWONSAN : 원산지
    ///  BNCHULIL : 출고일자
    ///  STDATE : 시작일자
    ///  BINO : BIN
    ///  BNBINNO : BIN
    ///  BNCHGN : 출고가능
    ///  BNGUBN : 특기사항
    ///  BNHJEGO : 재고량
    ///  BSSEQ : 차량대수
    /// </summary>
    public partial class TYUSGA002I : TYBase
    {
        private string fsGUBUN = string.Empty;

        #region Description : 폼 로드
        public TYUSGA002I()
        {
            InitializeComponent();
        }

        private void TYUSGA002I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyyMMdd"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyyMMdd"));

            SetStartingFocus(this.DTP01_STDATE);
            UP_FiledClear();
            UP_SetFiled("NEW");

            BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_98DEQ099",
                this.DTP01_STDATE.GetString(),
                this.DTP01_EDDATE.GetString(),
                this.TXT01_BINO.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_98DFN106.SetValue(dt);
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {   
            UP_FiledClear();

            this.DTP01_BNCHULIL.Focus();
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            try
            {
                this.DbConnector.CommandClear();

                if (fsGUBUN == "NEW")
                {
                    this.DbConnector.Attach("TY_P_US_98DET100",
                                            this.DTP01_BNCHULIL.GetString(),
                                            this.TXT01_BNBINNO.GetValue().ToString(),
                                            this.CBH01_BNGOKJONG.GetValue().ToString(),
                                            this.CBH01_BNWONSAN.GetValue().ToString(),
                                            this.CBH01_BNHANGCHA.GetValue().ToString(),
                                            this.TXT01_BNHJEGO.GetValue().ToString(),
                                            this.TXT01_BNCHGN.GetValue().ToString().ToUpper(),
                                            this.TXT01_BNGUBN.GetValue().ToString(),
                                            this.CBH01_BNTJHJ.GetValue().ToString(),
                                            this.CBH01_BNSOSOK.GetValue().ToString(),
                                            TYUserInfo.EmpNo);
                }
                else if (fsGUBUN == "UPT")
                {
                    this.DbConnector.Attach("TY_P_US_98DEX101",
                                            this.CBH01_BNGOKJONG.GetValue().ToString(),
                                            this.CBH01_BNWONSAN.GetValue().ToString(),
                                            this.CBH01_BNHANGCHA.GetValue().ToString(),
                                            this.TXT01_BNHJEGO.GetValue().ToString(),
                                            this.TXT01_BNCHGN.GetValue().ToString().ToUpper(),
                                            this.TXT01_BNGUBN.GetValue().ToString(),
                                            this.CBH01_BNTJHJ.GetValue().ToString(),
                                            this.CBH01_BNSOSOK.GetValue().ToString(),
                                            TYUserInfo.EmpNo,
                                            this.DTP01_BNCHULIL.GetString(),
                                            this.TXT01_BNBINNO.GetValue().ToString());
                }
                UP_BIN_STATUSMF_Update();

                this.DbConnector.ExecuteTranQueryList();

                this.ShowMessage("TY_M_GB_23NAD873");
                UP_Run(this.DTP01_BNCHULIL.GetString(),
                       this.TXT01_BNBINNO.GetValue().ToString());
                BTN61_INQ_Click(null, null);
            }
            catch
            {
                this.ShowMessage("TY_M_AC_246A2488");
            }
        }
        #endregion

        #region Description : 저장 ProcessCheck
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            // BIN 번호 유효성 체크
            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_98DJW109",
                                    this.TXT01_BNBINNO.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowCustomMessage("BIN 번호를 확인하세요.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                e.Successed = false;
                this.TXT01_BNBINNO.Focus();
                return;
            }


            // 화주, 협회 둘 중 하나만 입력가능
            if (this.CBH01_BNTJHJ.GetValue().ToString() != "" && this.CBH01_BNSOSOK.GetValue().ToString() != "")
            {
                this.ShowCustomMessage("화주, 협회 둘 중 하나만 입력 가능합니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                e.Successed = false;
                this.CBH01_BNTJHJ.CodeText.Focus();
                return;
            }
            // 출고가능 Y 또는 공백만 입력가능
            if (this.TXT01_BNCHGN.GetValue().ToString() != "" && this.TXT01_BNCHGN.GetValue().ToString().ToUpper() != "Y")
            {
                this.ShowCustomMessage("출고가능은 공백 또는 'Y'만 입력가능합니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                e.Successed = false;
                this.TXT01_BNCHGN.Focus();
                return;
            }

            if (fsGUBUN == "NEW")
            {
                // 데이터 존재유무 체크 (신규, 수정 체크)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_98DFA104",
                                        this.DTP01_BNCHULIL.GetString(),
                                        this.TXT01_BINO.GetValue().ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_US_91SEN618");
                    e.Successed = false;
                    this.DTP01_BNCHULIL.Focus();
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            try
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_US_98DEY102",
                                        this.DTP01_BNCHULIL.GetString(),
                                        this.TXT01_BNBINNO.GetValue().ToString());

                this.DbConnector.ExecuteTranQueryList();

                this.ShowMessage("TY_M_GB_23NAD874");


                this.BTN61_INQ_Click(null, null);

                UP_FiledClear();
            }
            catch
            {
                this.ShowMessage("TY_M_GB_43C9G671");
            }
        }
        #endregion

        #region Description : 삭제 ProcessCheck
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {   
            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 그리드 더블클릭
        private void FPS91_TY_S_US_98DFN106_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            UP_Run(this.FPS91_TY_S_US_98DFN106.GetValue("BNCHULIL").ToString(), this.FPS91_TY_S_US_98DFN106.GetValue("BNBINNO").ToString());
        }
        #endregion

        #region Description : 데이터 조회
        private void UP_Run(string sBNCHULIL, string sBNBINNO)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_98DFA104",
                                    sBNCHULIL,
                                    sBNBINNO);

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.DTP01_BNCHULIL.SetValue(dt.Rows[0]["BNCHULIL"].ToString());
                this.TXT01_BNBINNO.SetValue(dt.Rows[0]["BNBINNO"].ToString());

                this.CBH01_BNGOKJONG.SetValue(dt.Rows[0]["BNGOKJONG"].ToString());
                this.CBH01_BNWONSAN.SetValue(dt.Rows[0]["BNWONSAN"].ToString());
                this.CBH01_BNHANGCHA.SetValue(dt.Rows[0]["BNHANGCHA"].ToString());
                this.TXT01_BNHJEGO.SetValue(dt.Rows[0]["BNHJEGO"].ToString());
                this.TXT01_BNCHGN.SetValue(dt.Rows[0]["BNCHGN"].ToString());
                this.CBH01_BNTJHJ.SetValue(dt.Rows[0]["BNTJHJ"].ToString());
                this.CBH01_BNSOSOK.SetValue(dt.Rows[0]["BNSOSOK"].ToString(), dt.Rows[0]["BNSOSOKNM"].ToString());
                this.TXT01_BSSEQ.SetValue(dt.Rows[0]["BSSEQ"].ToString());
                this.TXT01_BNGUBN.SetValue(dt.Rows[0]["BNGUBN"].ToString());

                fsGUBUN = "UPT";

                UP_SetFiled("UPT");
            }
        }
        #endregion

        #region Desscription : 필드 초기화
        private void UP_FiledClear()
        {
            this.DTP01_BNCHULIL.SetValue(System.DateTime.Now.ToString("yyyyMMdd"));
            this.TXT01_BNBINNO.SetValue("");

            this.CBH01_BNGOKJONG.SetValue("");
            this.CBH01_BNWONSAN.SetValue("");
            this.CBH01_BNHANGCHA.SetValue("");
            this.TXT01_BNHJEGO.SetValue("");
            this.TXT01_BNCHGN.SetValue("");
            this.CBH01_BNTJHJ.SetValue("");
            this.CBH01_BNSOSOK.SetValue("");
            this.TXT01_BSSEQ.SetValue("");
            this.TXT01_BNGUBN.SetValue("");

            fsGUBUN = "NEW";

            UP_SetFiled("NEW");
        }
        #endregion

        #region Description : BIN 상태관리 업데이트
        private void UP_BIN_STATUSMF_Update()
        {
            this.DbConnector.Attach("TY_P_US_98DF3103",
                                    this.CBH01_BNGOKJONG.GetValue().ToString(),
                                    this.CBH01_BNWONSAN.GetValue().ToString(),
                                    this.TXT01_BNCHGN.GetValue().ToString().ToUpper(),
                                    this.CBH01_BNTJHJ.GetValue().ToString(),
                                    this.CBH01_BNSOSOK.GetValue().ToString(),
                                    this.TXT01_BNGUBN.GetValue().ToString(),
                                    this.CBH01_BNHANGCHA.GetValue().ToString(),
                                    TYUserInfo.EmpNo,
                                    this.DTP01_BNCHULIL.GetString(),
                                    this.TXT01_BNBINNO.GetValue().ToString());
        }
        #endregion

        #region Description : 버튼, 키 필드 잠금/해제
        private void UP_SetFiled(string sGUBUN)
        {
            if (sGUBUN == "NEW")
            {
                this.DTP01_BNCHULIL.SetReadOnly(false);
                this.TXT01_BNBINNO.SetReadOnly(false);

                this.BTN61_REM.Visible = false;
            }
            else if (sGUBUN == "UPT")
            {
                this.DTP01_BNCHULIL.SetReadOnly(true);
                this.TXT01_BNBINNO.SetReadOnly(true);

                this.BTN61_REM.Visible = true;
            }
        }
        #endregion

        private void TXT01_BINO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.BTN61_INQ.Focus();
            }
        }

        private void TXT01_BNGUBN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.BTN61_SAV.Focus();
            }
        }
    }
}
