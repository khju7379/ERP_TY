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
    public partial class TYUTPS007I : TYBase
    {
        private string fsGMREDATE  = string.Empty;
        private string fsGMTANKNO  = string.Empty;
        private string fsGMSECTION = string.Empty;


        public TYUTPS007I(string sGMREDATE, string sGMTANKNO, string sGMSECTION)
        {
            InitializeComponent();

            fsGMREDATE  = sGMREDATE;
            fsGMTANKNO  = sGMTANKNO;
            fsGMSECTION = sGMSECTION;
        }

        #region Description : 페이지 로드
        private void TYUTPS007I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            UP_Initialize();

            if (string.IsNullOrEmpty(fsGMREDATE) && string.IsNullOrEmpty(fsGMTANKNO) && string.IsNullOrEmpty(fsGMSECTION))
            {
                this.DTP01_GMREDATE.SetReadOnly(false);
                this.TXT01_GMTANKNO.SetReadOnly(false);
                this.CBO01_GMSECTION.SetReadOnly(false);

                this.DTP01_GMREDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

                SetStartingFocus(this.DTP01_GMREDATE);
            }
            else
            {

                this.DTP01_GMREDATE.SetReadOnly(true);
                this.TXT01_GMTANKNO.SetReadOnly(true);
                this.CBO01_GMSECTION.SetReadOnly(true);

                this.DTP01_GMREDATE.SetValue(Set_Date(fsGMREDATE.ToString()));
                this.TXT01_GMTANKNO.SetValue(fsGMTANKNO.ToString());
                this.CBO01_GMSECTION.SetValue(fsGMSECTION.ToString());

                UP_RUN(fsGMREDATE, fsGMTANKNO, fsGMSECTION);

                SetStartingFocus(this.CBH01_GMPIPE.CodeText);
            }
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            fsGMREDATE = "";
            fsGMTANKNO = "";
            fsGMSECTION = "";

            this.DTP01_GMREDATE.SetReadOnly(false);
            this.TXT01_GMTANKNO.SetReadOnly(false);
            this.CBO01_GMSECTION.SetReadOnly(false);

            UP_Initialize();

            SetFocus(this.DTP01_GMREDATE);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            try
            {
                string sGMHIGB = string.Empty;

                if (string.IsNullOrEmpty(fsGMREDATE) && string.IsNullOrEmpty(fsGMTANKNO) && string.IsNullOrEmpty(fsGMSECTION))
                {
                    sGMHIGB = "A";

                    fsGMREDATE = Get_Date(this.DTP01_GMREDATE.GetValue().ToString());
                    fsGMTANKNO = this.TXT01_GMTANKNO.GetValue().ToString().Trim();
                    fsGMSECTION = this.CBO01_GMSECTION.GetValue().ToString();
                }
                else
                {
                    sGMHIGB = "C";
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_B3JEM011", Get_Date(this.DTP01_GMREDATE.GetValue().ToString().Trim()),
                                                            this.TXT01_GMTANKNO.GetValue().ToString().Trim(),
                                                            this.CBO01_GMSECTION.GetValue().ToString().Trim(),
                                                            this.CBH01_GMPIPE.GetValue().ToString().Trim(),
                                                            this.CBH01_GMHWAMUL.GetValue().ToString().Trim(),
                                                            this.TXT01_GMTEMP.GetValue().ToString().Trim(),
                                                            this.TXT01_GMPRESS.GetValue().ToString().Trim(),
                                                            this.CBH01_GMMATERIAL.GetValue().ToString().Trim(),
                                                            this.TXT01_GMGASKETMA.GetValue().ToString().Trim(),
                                                            this.TXT01_GMNONINSP.GetValue().ToString().Trim(),
                                                            this.TXT01_GMBACKROW.GetValue().ToString().Trim(),
                                                            this.TXT01_GMBIGO.GetValue().ToString().Trim(), 
                                                            TYUserInfo.EmpNo,
                                                            Get_Date(this.DTP01_GMREDATE.GetValue().ToString().Trim()),
                                                            this.TXT01_GMTANKNO.GetValue().ToString().Trim(),
                                                            this.CBO01_GMSECTION.GetValue().ToString().Trim(),
                                                            this.CBH01_GMPIPE.GetValue().ToString().Trim(),
                                                            this.CBH01_GMHWAMUL.GetValue().ToString().Trim(),
                                                            this.TXT01_GMTEMP.GetValue().ToString().Trim(),
                                                            this.TXT01_GMPRESS.GetValue().ToString().Trim(),
                                                            this.CBH01_GMMATERIAL.GetValue().ToString().Trim(),
                                                            this.TXT01_GMGASKETMA.GetValue().ToString().Trim(),
                                                            this.TXT01_GMNONINSP.GetValue().ToString().Trim(),
                                                            this.TXT01_GMBACKROW.GetValue().ToString().Trim(),
                                                            this.TXT01_GMBIGO.GetValue().ToString().Trim(), 
                                                            TYUserInfo.EmpNo
                                                            );

                this.DbConnector.ExecuteNonQuery();

                UP_RUN(fsGMREDATE, fsGMTANKNO, fsGMSECTION);

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
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 필드 값 입력
        private void UP_RUN(string sGMREDATE, string sGMTANKNO, string sGMSECTION)
        {
            try
            {
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_B3JED010", sGMREDATE.ToString(), sGMTANKNO.ToString(), sGMSECTION.ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.CurrentDataTableRowMapping(dt, "01");

                    this.SetFocus(this.CBH01_GMPIPE.CodeText);

                    this.DTP01_GMREDATE.SetReadOnly(true);
                    this.TXT01_GMTANKNO.SetReadOnly(true);
                    this.CBO01_GMSECTION.SetReadOnly(true);
                }
                else
                {
                    this.DTP01_GMREDATE.SetReadOnly(false);
                    this.TXT01_GMTANKNO.SetReadOnly(false);
                    this.CBO01_GMSECTION.SetReadOnly(false);
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
            this.DTP01_GMREDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.TXT01_GMTANKNO.SetValue("");
            this.CBO01_GMSECTION.SetValue("I");

            this.CBH01_GMPIPE.SetValue("");
            this.CBH01_GMHWAMUL.SetValue("");
            this.TXT01_GMTEMP.SetValue("");
            this.TXT01_GMPRESS.SetValue("");
            this.CBH01_GMMATERIAL.SetValue("");
            this.TXT01_GMGASKETMA.SetValue("");
            this.TXT01_GMNONINSP.SetValue("");
            this.TXT01_GMBACKROW.SetValue("");
            this.TXT01_GMBIGO.SetValue("");
        }
        #endregion

        #region Description : 엔터키 이벤트(포커스 이동)
        private void TXT01_GMBIGO_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.BTN61_SAV);
            }
        }
        #endregion
    }
}
