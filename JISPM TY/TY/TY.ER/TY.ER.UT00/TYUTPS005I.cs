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
    public partial class TYUTPS005I : TYBase
    {
        private string fsDICODE = string.Empty;

        public TYUTPS005I(string sDICODE)
        {
            InitializeComponent();

            fsDICODE = sDICODE;
        }

        #region Description : 페이지 로드
        private void TYUTPS005I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            UP_Initialize();

            if (string.IsNullOrEmpty(fsDICODE))
            {
                this.CBH01_DICODE.SetReadOnly(false);

                SetStartingFocus(this.CBH01_DICODE.CodeText);
            }
            else
            {
                this.CBH01_DICODE.SetReadOnly(true);

                UP_RUN(fsDICODE);

                SetStartingFocus(this.TXT01_DIHWAMULNM);
            }
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            fsDICODE = "";

            UP_Initialize();
            this.CBH01_DICODE.SetReadOnly(false);

            this.SetFocus(this.CBH01_DICODE.CodeText);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(fsDICODE))
                {
                    fsDICODE = this.CBH01_DICODE.GetValue().ToString();
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_B3CEG936", this.CBH01_DICODE.GetValue().ToString().Trim(),
                                                            this.TXT01_DIHWAMULNM.GetValue().ToString(),
                                                            this.TXT01_DICAPA.GetValue().ToString(),
                                                            Get_Numeric(this.TXT01_DIDIA.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_DIHIGH.GetValue().ToString()),
                                                            this.TXT01_DIOPPRESS.GetValue().ToString(),
                                                            this.TXT01_DIPLPRESS.GetValue().ToString(),
                                                            this.TXT01_DIOPTEMP.GetValue().ToString(),
                                                            this.TXT01_DIPLTEMP.GetValue().ToString(),
                                                            this.CBH01_DIMATERIAL.GetValue().ToString(),
                                                            this.TXT01_DINOZZEL.GetValue().ToString(),
                                                            this.TXT01_DIGASKET.GetValue().ToString(),
                                                            Get_Numeric(this.TXT01_DIWELDYUL.GetValue().ToString()),
                                                            this.TXT01_DICAWIDTH.GetValue().ToString(),
                                                            this.TXT01_DICORROA.GetValue().ToString(),
                                                            this.TXT01_DICORROB.GetValue().ToString(),
                                                            this.TXT01_DICORROC.GetValue().ToString(),
                                                            this.TXT01_DICORROD.GetValue().ToString(),
                                                            this.TXT01_DIUSWIDTHA.GetValue().ToString(),
                                                            this.TXT01_DIUSWIDTHB.GetValue().ToString(),
                                                            this.TXT01_DIUSWIDTHC.GetValue().ToString(),
                                                            this.TXT01_DIUSWIDTHD.GetValue().ToString(),
                                                            this.TXT01_DIBACKROW.GetValue().ToString(),
                                                            this.TXT01_DINONINSP.GetValue().ToString(),
                                                            this.CKB01_DITANKGUBN.GetValue().ToString(),
                                                            this.CBO01_DIUSEGUBN.GetValue().ToString(),
                                                            this.TXT01_DIBIGO.GetValue().ToString(),
                                                            TYUserInfo.EmpNo,
                                                            this.CBH01_DICODE.GetValue().ToString().Trim(),
                                                            this.TXT01_DIHWAMULNM.GetValue().ToString(),
                                                            this.TXT01_DICAPA.GetValue().ToString(),
                                                            Get_Numeric(this.TXT01_DIDIA.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_DIHIGH.GetValue().ToString()),
                                                            this.TXT01_DIOPPRESS.GetValue().ToString(),
                                                            this.TXT01_DIPLPRESS.GetValue().ToString(),
                                                            this.TXT01_DIOPTEMP.GetValue().ToString(),
                                                            this.TXT01_DIPLTEMP.GetValue().ToString(),
                                                            this.CBH01_DIMATERIAL.GetValue().ToString(),
                                                            this.TXT01_DINOZZEL.GetValue().ToString(),
                                                            this.TXT01_DIGASKET.GetValue().ToString(),
                                                            Get_Numeric(this.TXT01_DIWELDYUL.GetValue().ToString()),
                                                            this.TXT01_DICAWIDTH.GetValue().ToString(),
                                                            this.TXT01_DICORROA.GetValue().ToString(),
                                                            this.TXT01_DICORROB.GetValue().ToString(),
                                                            this.TXT01_DICORROC.GetValue().ToString(),
                                                            this.TXT01_DICORROD.GetValue().ToString(),
                                                            this.TXT01_DIUSWIDTHA.GetValue().ToString(),
                                                            this.TXT01_DIUSWIDTHB.GetValue().ToString(),
                                                            this.TXT01_DIUSWIDTHC.GetValue().ToString(),
                                                            this.TXT01_DIUSWIDTHD.GetValue().ToString(),
                                                            this.TXT01_DIBACKROW.GetValue().ToString(),
                                                            this.TXT01_DINONINSP.GetValue().ToString(),
                                                            this.CKB01_DITANKGUBN.GetValue().ToString(),
                                                            this.CBO01_DIUSEGUBN.GetValue().ToString(),
                                                            this.TXT01_DIBIGO.GetValue().ToString(),
                                                            TYUserInfo.EmpNo
                                                            );

                this.DbConnector.ExecuteNonQuery();

                UP_RUN(fsDICODE);

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
            if (this.CKB01_DITANKGUBN.Checked == true)
            {
                int iLength = this.CBH01_DICODE.GetValue().ToString().Trim().Length;

                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_6AQKH593", this.CBH01_DICODE.GetValue().ToString().Substring(1, iLength - 1));

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_DIHWAMULNM.SetValue(dt.Rows[0]["HMDESC1"].ToString());
                    this.TXT01_DICAPA.SetValue(dt.Rows[0]["TNCAPA"].ToString());
                    this.TXT01_DIDIA.SetValue(dt.Rows[0]["TNDIA"].ToString());
                    this.TXT01_DIHIGH.SetValue(dt.Rows[0]["TNHIGH"].ToString());
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
        private void UP_RUN(string sDICODE)
        {
            try
            {
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_B3CDH934", sDICODE.ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.CurrentDataTableRowMapping(dt, "01");

                    this.SetFocus(this.TXT01_DIHWAMULNM);

                    this.CBH01_DICODE.SetReadOnly(true);
                }
                else
                {
                    this.CBH01_DICODE.SetReadOnly(false);
                }
            }
            catch(Exception ex)
            {
                string a = string.Empty;

                a = ex.ToString();
            }
        }
        #endregion

        #region Description : 필드 초기화
        private void UP_Initialize()
        {
            this.TXT01_DIHWAMULNM.SetValue("");
            this.TXT01_DICAPA.SetValue("");
            this.TXT01_DIDIA.SetValue("");
            this.TXT01_DIHIGH.SetValue("");
            this.TXT01_DIOPPRESS.SetValue("");
            this.TXT01_DIPLPRESS.SetValue("");
            this.TXT01_DIOPTEMP.SetValue("");
            this.TXT01_DIPLTEMP.SetValue("");
            this.CBH01_DIMATERIAL.SetValue("");
            this.TXT01_DIGASKET.SetValue("");
            this.TXT01_DIWELDYUL.SetValue("");
            this.TXT01_DICAWIDTH.SetValue("");
            this.TXT01_DICORROA.SetValue("");
            this.TXT01_DICORROB.SetValue("");
            this.TXT01_DICORROC.SetValue("");
            this.TXT01_DICORROD.SetValue("");
            this.TXT01_DIUSWIDTHA.SetValue("");
            this.TXT01_DIUSWIDTHB.SetValue("");
            this.TXT01_DIUSWIDTHC.SetValue("");
            this.TXT01_DIUSWIDTHD.SetValue("");
            this.TXT01_DIBACKROW.SetValue("");
            this.TXT01_DINONINSP.SetValue("");
            this.CKB01_DITANKGUBN.SetValue("");
            this.CBO01_DIUSEGUBN.SetValue("Y");
            this.TXT01_DIBIGO.SetValue("");
        }
        #endregion

        #region Description : 엔터키 이벤트(포커스 이동)
        private void CBH01_DICODE_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.TXT01_DIHWAMULNM);
            }
        }

        private void TXT01_DIUSWIDTHD_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.TXT01_DIBACKROW);
            }
        }

        private void TXT01_DIBIGO_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.BTN61_SAV);
            }
        }
        #endregion

        private void CKB01_DITANKGUBN_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CKB01_DITANKGUBN.Checked == true)
            {
                int iLength = this.CBH01_DICODE.GetValue().ToString().Trim().Length;

                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_6AQKH593", this.CBH01_DICODE.GetValue().ToString().Substring(1, iLength - 1));

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_DIHWAMULNM.SetValue(dt.Rows[0]["HMDESC1"].ToString());
                    this.TXT01_DICAPA.SetValue(dt.Rows[0]["TNCAPA"].ToString());
                    this.TXT01_DIDIA.SetValue(dt.Rows[0]["TNDIA"].ToString());
                    this.TXT01_DIHIGH.SetValue(dt.Rows[0]["TNHIGH"].ToString());
                }
            }
            else
            {
                this.TXT01_DIHWAMULNM.SetValue("");
                this.TXT01_DICAPA.SetValue("");
                this.TXT01_DIDIA.SetValue("");
                this.TXT01_DIHIGH.SetValue("");
            }
        }
    }
}
