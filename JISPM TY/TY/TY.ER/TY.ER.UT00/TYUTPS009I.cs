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
    public partial class TYUTPS009I : TYBase
    {
        private string fsSVREDATE = string.Empty;
        private string fsSVCODE   = string.Empty;


        public TYUTPS009I(string sSVREDATE, string sSVCODE)
        {
            InitializeComponent();

            fsSVREDATE  = sSVREDATE;
            fsSVCODE  = sSVCODE;
        }

        #region Description : 페이지 로드
        private void TYUTPS009I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            UP_Initialize();

            if (string.IsNullOrEmpty(fsSVREDATE) && string.IsNullOrEmpty(fsSVCODE))
            {
                this.DTP01_SVREDATE.SetReadOnly(false);
                this.CBH01_SVCODE.SetReadOnly(false);

                this.DTP01_SVREDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

                SetStartingFocus(this.DTP01_SVREDATE);
            }
            else
            {

                this.DTP01_SVREDATE.SetReadOnly(true);
                this.CBH01_SVCODE.SetReadOnly(true);

                this.DTP01_SVREDATE.SetValue(Set_Date(fsSVREDATE.ToString()));
                this.CBH01_SVCODE.SetValue(fsSVCODE.ToString());                

                UP_RUN(fsSVREDATE, fsSVCODE);

                SetStartingFocus(this.CBH01_SVPROTDEV.CodeText);
            }
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            fsSVREDATE = "";
            fsSVCODE = "";
            

            this.DTP01_SVREDATE.SetReadOnly(false);
            this.CBH01_SVCODE.SetReadOnly(false);

            UP_Initialize();

            SetFocus(this.DTP01_SVREDATE);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            try
            {
                string sSVHIGB = string.Empty;

                if (string.IsNullOrEmpty(fsSVREDATE) && string.IsNullOrEmpty(fsSVCODE))
                {
                    fsSVREDATE = Get_Date(this.DTP01_SVREDATE.GetValue().ToString());
                    fsSVCODE = this.CBH01_SVCODE.GetValue().ToString().Trim();
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_B3UDO083", Get_Date(this.DTP01_SVREDATE.GetValue().ToString().Trim()),
                                                            this.CBH01_SVCODE.GetValue().ToString().Trim(),
                                                            this.TXT01_SVHWAMULNM.GetValue().ToString().Trim(),
                                                            this.TXT01_SVSTATUS.GetValue().ToString().Trim(),
                                                            Get_Numeric(this.TXT01_SVOUTCAPA.GetValue().ToString().Trim()),
                                                            Get_Numeric(this.TXT01_SVRATECAPA.GetValue().ToString().Trim()),
                                                            this.TXT01_SVNOZZLEIN.GetValue().ToString().Trim(),
                                                            this.TXT01_SVNOZZLEOU.GetValue().ToString().Trim(),
                                                            this.CBH01_SVPROTDEV.GetValue().ToString().Trim(),
                                                            Get_Numeric(this.TXT01_SVPROTOPERA.GetValue().ToString().Trim()),
                                                            Get_Numeric(this.TXT01_SVPROTPLAN.GetValue().ToString().Trim()),
                                                            Get_Numeric(this.TXT01_SVPIPESET.GetValue().ToString().Trim()),
                                                            this.CBH01_SVPIPECODE.GetValue().ToString().Trim(),
                                                            this.CBH01_SVPIPETRIM.GetValue().ToString().Trim(),
                                                            this.TXT01_SVPRECSION.GetValue().ToString().Trim(),
                                                            this.TXT01_SVOUTCONNECT.GetValue().ToString().Trim(),
                                                            this.TXT01_SVOUTREASION.GetValue().ToString().Trim(),
                                                            this.TXT01_SVFORMAT.GetValue().ToString().Trim(),
                                                            this.CKB01_SVTANKGUBN.GetValue().ToString().Trim(),
                                                            TYUserInfo.EmpNo,
                                                            Get_Date(this.DTP01_SVREDATE.GetValue().ToString().Trim()),
                                                            this.CBH01_SVCODE.GetValue().ToString().Trim(),
                                                            this.TXT01_SVHWAMULNM.GetValue().ToString().Trim(),
                                                            this.TXT01_SVSTATUS.GetValue().ToString().Trim(),
                                                            Get_Numeric(this.TXT01_SVOUTCAPA.GetValue().ToString().Trim()),
                                                            Get_Numeric(this.TXT01_SVRATECAPA.GetValue().ToString().Trim()),
                                                            this.TXT01_SVNOZZLEIN.GetValue().ToString().Trim(),
                                                            this.TXT01_SVNOZZLEOU.GetValue().ToString().Trim(),
                                                            this.CBH01_SVPROTDEV.GetValue().ToString().Trim(),
                                                            Get_Numeric(this.TXT01_SVPROTOPERA.GetValue().ToString().Trim()),
                                                            Get_Numeric(this.TXT01_SVPROTPLAN.GetValue().ToString().Trim()),
                                                            Get_Numeric(this.TXT01_SVPIPESET.GetValue().ToString().Trim()),
                                                            this.CBH01_SVPIPECODE.GetValue().ToString().Trim(),
                                                            this.CBH01_SVPIPETRIM.GetValue().ToString().Trim(),
                                                            this.TXT01_SVPRECSION.GetValue().ToString().Trim(),
                                                            this.TXT01_SVOUTCONNECT.GetValue().ToString().Trim(),
                                                            this.TXT01_SVOUTREASION.GetValue().ToString().Trim(),
                                                            this.TXT01_SVFORMAT.GetValue().ToString().Trim(),
                                                            this.CKB01_SVTANKGUBN.GetValue().ToString().Trim(),
                                                            TYUserInfo.EmpNo
                                                            );

                this.DbConnector.ExecuteNonQuery();

                UP_RUN(fsSVREDATE, fsSVCODE);

                this.ShowMessage("TY_M_GB_23NAD873");
            }
            catch(Exception ex)
            {
                string smsg = ex.ToString();
                this.ShowMessage("TY_M_AC_246A2488");
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

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.CKB01_SVTANKGUBN.Checked == true)
            {
                if (this.TXT01_SVOUTCONNECT.GetValue().ToString().Trim() != "")
                {
                    int iLength = this.TXT01_SVOUTCONNECT.GetValue().ToString().Trim().Length;

                    DataTable dt = new DataTable();

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_6AQKH593", this.TXT01_SVOUTCONNECT.GetValue().ToString().Substring(2, iLength - 2));

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.TXT01_SVHWAMULNM.SetValue(dt.Rows[0]["HMDESC1"].ToString());
                    }
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 필드 값 입력
        private void UP_RUN(string sSVREDATE, string sSVCODE)
        {
            try
            {
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_B3UD5081", sSVREDATE.ToString(), sSVCODE.ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.CurrentDataTableRowMapping(dt, "01");

                    this.DTP01_SVREDATE.SetReadOnly(true);
                    this.CBH01_SVCODE.SetReadOnly(true);

                    this.SetFocus(this.TXT01_SVHWAMULNM);
                }
                else
                {
                    this.DTP01_SVREDATE.SetReadOnly(false);
                    this.CBH01_SVCODE.SetReadOnly(false);

                    this.SetFocus(this.DTP01_SVREDATE);
                }
            }
            catch
            {
            }
        }
        #endregion

        #region Description : 필드 초기화
        private void UP_Initialize()
        {
            this.DTP01_SVREDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.CBH01_SVCODE.SetValue("");

            this.TXT01_SVHWAMULNM.SetValue("");
            this.TXT01_SVSTATUS.SetValue("");
            this.TXT01_SVOUTCAPA.SetValue("");
            this.TXT01_SVRATECAPA.SetValue("");
            this.TXT01_SVNOZZLEIN.SetValue("");
            this.TXT01_SVNOZZLEOU.SetValue("");

            this.CBH01_SVPROTDEV.SetValue("");
            this.TXT01_SVPROTOPERA.SetValue("");
            this.TXT01_SVPROTPLAN.SetValue("");
            this.TXT01_SVPIPESET.SetValue("");
            this.CBH01_SVPIPECODE.SetValue("");
            this.CBH01_SVPIPETRIM.SetValue("");
            this.TXT01_SVPRECSION.SetValue("");

            this.TXT01_SVOUTCONNECT.SetValue("");
            this.TXT01_SVOUTREASION.SetValue("");
            this.TXT01_SVFORMAT.SetValue("");
            this.CKB01_SVTANKGUBN.SetValue("N");
        }
        #endregion

        #region Description : 엔터키 이벤트(포커스 이동)
        private void TXT01_SVFORMAT_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.BTN61_SAV);
            }
        }
        #endregion

        private void CKB01_SVTANKGUBN_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CKB01_SVTANKGUBN.Checked == true)
            {
                if (this.TXT01_SVOUTCONNECT.GetValue().ToString().Trim() != "")
                {
                    int iLength = this.TXT01_SVOUTCONNECT.GetValue().ToString().Trim().Length;

                    DataTable dt = new DataTable();

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_6AQKH593", this.TXT01_SVOUTCONNECT.GetValue().ToString().Substring(2, iLength - 2));

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.TXT01_SVHWAMULNM.SetValue(dt.Rows[0]["HMDESC1"].ToString());
                    }
                }
            }
            else
            {
                this.TXT01_SVHWAMULNM.SetValue("");
            }
        }
    }
}
