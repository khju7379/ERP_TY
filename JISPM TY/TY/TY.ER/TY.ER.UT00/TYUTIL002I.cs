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
    public partial class TYUTIL002I : TYBase
    {
        private string fsDNYYMM = string.Empty;

        public TYUTIL002I(string sDNYYMM)
        {
            InitializeComponent();

            fsDNYYMM = sDNYYMM;
        }

        #region Description : 페이지 로드
        private void TYUTIL002I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            UP_Init();

            if (string.IsNullOrEmpty(fsDNYYMM))
            {
                this.DTP01_DNYYMM.SetReadOnly(false);

                this.DTP01_DNYYMM.SetValue(System.DateTime.Now.ToString("yyyyMM"));

                SetStartingFocus(this.DTP01_DNYYMM);
            }
            else
            {
                this.DTP01_DNYYMM.SetReadOnly(true);

                UP_SetText(fsDNYYMM);

                SetStartingFocus(this.TXT01_DNKYUNG);
            }
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            try
            {
                string sGASTAMT = string.Empty;
                string sDNYYMMDD = string.Empty;


                if (Convert.ToDouble(Get_Numeric(this.DTP01_DNYYMM.GetString().Substring(0, 6))) >= 202002)
                {
                    sDNYYMMDD = this.DTP01_DNYYMM.GetString().Substring(0, 6) + "20";
                }
                else
                {
                    sDNYYMMDD = this.DTP01_DNYYMM.GetString().Substring(0, 6) + "25";
                }

                // 가열료 등록
                if (this.TXT01_DNSKSTEAM.GetValue().ToString() != "")
                {
                    if (Convert.ToDouble(this.TXT01_DNSKSTEAM.GetValue().ToString()) != 0)
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_674FJ555", "1",
                                                                    sDNYYMMDD);

                        DataTable dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            this.DbConnector.CommandClear();

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (Convert.ToDouble(dt.Rows[i]["GASTTIME"].ToString()) > 0)
                                {
                                    sGASTAMT = Convert.ToString(Convert.ToDouble(Get_Numeric(dt.Rows[i]["GASTQTY"].ToString())) * Convert.ToDouble(this.TXT01_DNSKSTEAM.GetValue().ToString()) + 0.5);
                                }
                                else
                                {
                                    sGASTAMT = "0";
                                }

                                this.DbConnector.Attach("TY_P_UT_674FM556", "1",
                                                                            sDNYYMMDD,
                                                                            dt.Rows[i]["GAHWAJU"].ToString(),
                                                                            dt.Rows[i]["GAHWAMUL"].ToString(),
                                                                            sGASTAMT,
                                                                            "C",
                                                                            TYUserInfo.EmpNo
                                                                            );


                            }
                            this.DbConnector.ExecuteTranQueryList();
                        }
                    }
                }
                // 수정
                if (UP_KEY_Check())
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_UT_674AN537", this.TXT01_DNKYUNG.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_DNBKCU.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_DNELECT.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_DNMOTER2.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_DNYUL.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_DNSELECT.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_DNMOTER1.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_DNSELDANGA.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_DNSELTIM.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_DNSELAMT.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_DNSTTIM.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_DNSTAMT.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_DNSTDANGA.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_DNJILSO.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_DNJILSOQTY.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_DNSKSTEAM.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_DNSKTAMT.GetValue().ToString().Replace(",", ""),
                                                                "C",
                                                                TYUserInfo.EmpNo,
                                                                this.DTP01_DNYYMM.GetString().Substring(0, 6)
                                                                );

                    this.DbConnector.ExecuteTranQuery();
                }
                // 신규 등록
                else
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_UT_674AM536", this.DTP01_DNYYMM.GetString().Substring(0, 6),
                                                                this.TXT01_DNELECT.GetValue().ToString().Replace(",", ""),                                                                                                        
                                                                this.TXT01_DNBKCU.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_DNKYUNG.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_DNMOTER1.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_DNMOTER2.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_DNYUL.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_DNJILSO.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_DNJILSOQTY.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_DNSKSTEAM.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_DNSKTAMT.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_DNSELECT.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_DNSELTIM.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_DNSELDANGA.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_DNSELAMT.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_DNSTTIM.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_DNSTDANGA.GetValue().ToString().Replace(",", ""),
                                                                this.TXT01_DNSTAMT.GetValue().ToString().Replace(",", ""),
                                                                TYUserInfo.EmpNo
                                                                );

                    this.DbConnector.ExecuteTranQuery();
                }
                UP_SetText(this.DTP01_DNYYMM.GetString().Substring(0, 6));
                this.DTP01_DNYYMM.SetReadOnly(true);
                this.ShowMessage("TY_M_GB_23NAD873");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch
            {
                this.ShowMessage("TY_M_AC_246A2488");
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

        #region Description : 키 체크
        private bool UP_KEY_Check()
        {
            bool bRst = false;

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_674DE549", this.DTP01_DNYYMM.GetString().Substring(0, 6));

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                bRst = true;
            }

            return bRst;
        }
        #endregion

        #region Description : 필드 값 입력
        private void UP_SetText(string sNDYYMM)
        {
            try
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_674DE549", sNDYYMM);

                DataTable dt = this.DbConnector.ExecuteDataTable();

                this.DTP01_DNYYMM.SetValue(dt.Rows[0]["DNYYMM"].ToString());

                this.TXT01_DNKYUNG.Text = string.Format("{0:#,##0.00}", double.Parse(dt.Rows[0]["DNKYUNG"].ToString()));
                this.TXT01_DNBKCU.Text = string.Format("{0:#,##0.00}", double.Parse(dt.Rows[0]["DNBKCU"].ToString()));
                this.TXT01_DNELECT.Text = string.Format("{0:#,##0.00}", double.Parse(dt.Rows[0]["DNELECT"].ToString()));
                this.TXT01_DNMOTER2.Text = string.Format("{0:#,##0.0}", double.Parse(dt.Rows[0]["DNMOTER2"].ToString()));
                this.TXT01_DNYUL.Text = string.Format("{0:#,##0.00}", double.Parse(dt.Rows[0]["DNYUL"].ToString()));

                this.TXT01_DNSELECT.Text = string.Format("{0:#,##0.00}", double.Parse(dt.Rows[0]["DNSELECT"].ToString()));
                this.TXT01_DNMOTER1.Text = string.Format("{0:#,##0.0}", double.Parse(dt.Rows[0]["DNMOTER1"].ToString()));
                this.TXT01_DNSELDANGA.Text = string.Format("{0:#,###}", double.Parse(dt.Rows[0]["DNSELDANGA"].ToString()));
                this.TXT01_DNSELTIM.Text = string.Format("{0:#,##0.00}", double.Parse(dt.Rows[0]["DNSELTIM"].ToString()));
                this.TXT01_DNSELAMT.Text = string.Format("{0:#,###}", double.Parse(dt.Rows[0]["DNSELAMT"].ToString()));

                this.TXT01_DNSTTIM.Text = string.Format("{0:#,##0.00}", double.Parse(dt.Rows[0]["DNSTTIM"].ToString()));
                this.TXT01_DNSTAMT.Text = string.Format("{0:#,###}", double.Parse(dt.Rows[0]["DNSTAMT"].ToString()));
                this.TXT01_DNSTDANGA.Text = string.Format("{0:#,###}", double.Parse(dt.Rows[0]["DNSTDANGA"].ToString()));

                this.TXT01_DNJILSO.Text = string.Format("{0:#,###}", double.Parse(dt.Rows[0]["DNJILSO"].ToString()));
                this.TXT01_DNJILSOQTY.Text = string.Format("{0:#,###}", double.Parse(dt.Rows[0]["DNJILSOQTY"].ToString()));

                this.TXT01_DNSKSTEAM.Text = string.Format("{0:#,##0.00}", double.Parse(dt.Rows[0]["DNSKSTEAM"].ToString()));
                this.TXT01_DNSKTAMT.Text = string.Format("{0:#,###}", double.Parse(dt.Rows[0]["DNSKTAMT"].ToString()));
            }
            catch
            {
            }
        }
        #endregion

        #region Description : 필드 초기화
        private void UP_Init()
        {
            this.DTP01_DNYYMM.SetValue("");

            this.TXT01_DNKYUNG.Text = "";
            this.TXT01_DNBKCU.Text = "";
            this.TXT01_DNYUL.Text = "";
            this.TXT01_DNELECT.Text = "";
            this.TXT01_DNMOTER2.Text = "";

            this.TXT01_DNSELECT.Text = "";
            this.TXT01_DNMOTER1.Text = "";
            this.TXT01_DNSELDANGA.Text = "";
            this.TXT01_DNSELTIM.Text = "";
            this.TXT01_DNSELAMT.Text = "";

            this.TXT01_DNSTTIM.Text = "";
            this.TXT01_DNSTAMT.Text = "";
            this.TXT01_DNSTDANGA.Text = "";

            this.TXT01_DNJILSO.Text = "";

            this.TXT01_DNSKSTEAM.Text = "";
            this.TXT01_DNSKTAMT.Text = "";
        }
        #endregion

        #region Description : 엔터키 이벤트(포커스 이동)
        private void DTP01_DNYYMM_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.TXT01_DNKYUNG);
            }
        }
        #endregion
    }
}
