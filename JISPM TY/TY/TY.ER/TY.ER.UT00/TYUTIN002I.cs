using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.UT00
{
    /// <summary>
    /// 거래처관리 등록 팝업 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.04.02 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// 
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2454Y465 : 사업자번호 체크(등록)
    ///  TY_P_AC_24550471 : 사업자번호 체크(수정)
    ///  TY_P_AC_245BX447 : 거래처코드 가져오는 SP
    ///  TY_P_AC_2444X432 : 거래처관리 등록
    ///  TY_P_AC_2444Y433 : 거래처관리 수정
    ///  TY_P_AC_2445D438 : 거래처관리 확인
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2445M440 : 은행코드를 입력하세요.
    ///  TY_M_AC_2445M441 : 계좌번호를 입력하세요.
    ///  TY_M_AC_2454S464 : 사업자 번호 또는 주민등록번호중 한가지만 입력이 가능 합니다.
    ///  TY_M_AC_2443N422 : 해당거래처 코드는 사용내용이 존재하여 작업이 불가합니다.
    ///  TY_M_AC_2445G439 : 동일 사업자 번호가 존재합니다.
    ///  TY_M_AC_2445M440 : 은행코드를 입력하세요.
    ///  TY_M_AC_2445M441 : 계좌번호를 입력하세요.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  VNCDBK : 은행
    ///  VNGUBUN : 구분
    ///  VNJJGUB : 자재사용구분
    ///  VNPGUBN : 거래처구분
    ///  VNBKYN : 전자계좌계설
    ///  VNBIGO : 비고
    ///  VNCODE : 거래처코드
    ///  VNHIDAT : 작성일자
    ///  VNIRUM : 대표자명
    ///  VNJUSO : 주소
    ///  VNNOAC : 계좌번호
    ///  VNSANGHO : 거래처명
    ///  VNSAUPNO1 : 사업자등록번호1
    ///  VNSAUPNO2 : 사업자등록번호2
    ///  VNSAUPNO3 : 사업자등록번호3
    ///  VNTEL : 전화번호
    ///  VNUPJONG : 업종
    ///  VNUPTE : 업태
    ///  VNUPYUN1 : 우편번호1
    ///  VNUPYUN2 : 우편번호2
    /// </summary>
    public partial class TYUTIN002I : TYBase
    {
        private string fsVNCODE;
        private string fsVNSAUPNO;
        private TYData DAT01_VNHISAB;
        private TYData DAT01_VNSJGB;

        #region Description : 페이지 로드
        public TYUTIN002I(string VNCODE)
        {
            InitializeComponent();

            this.SetPopupStyle();

            // 파라미터값 가져오기 
            this.fsVNCODE = VNCODE;
        }

        private void TYUTIN002I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            if (string.IsNullOrEmpty(this.fsVNCODE))
            {
                this.TXT01_VNCODE.SetReadOnly(false);

                this.TXT01_VNSAUPNO.SetReadOnly(false);
                this.BTN61_VNSAUPNO3.Visible = true;

                SetStartingFocus(this.TXT01_VNCODE);
            }
            else
            {
                this.TXT01_VNCODE.SetReadOnly(true);

                this.TXT01_VNSAUPNO.SetReadOnly(true);
                this.BTN61_VNSAUPNO3.Visible = false;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_66DFG160", this.fsVNCODE);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.CurrentDataTableRowMapping(dt, "01");

                    this.fsVNSAUPNO = dt.Rows[0]["VNSAUPNO"].ToString();
                }

                SetStartingFocus(this.TXT01_VNSAUPNO);
            }
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sVNUPYUN = string.Empty;

            sVNUPYUN = this.TXT01_VNUPYUN1.GetValue().ToString().Trim() + this.TXT01_VNUPYUN2.GetValue().ToString().Trim();

            if (string.IsNullOrEmpty(this.fsVNCODE))
            {
                // 등록
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_BA7FH607",
                                        this.TXT01_VNCODE.GetValue().ToString(),
                                        this.TXT01_VNSAUPNO.GetValue().ToString(),
                                        this.CBO01_VNGUBUN.GetValue().ToString().Trim(),
                                        this.TXT01_VNSANGHO.GetValue().ToString().Trim(),
                                        this.TXT01_VNIRUM.GetValue().ToString().Trim(),
                                        this.TXT01_VNUPJONG.GetValue().ToString().Trim(),
                                        this.TXT01_VNUPTE.GetValue().ToString().Trim(),
                                        this.TXT01_VNTEL.GetValue().ToString().Trim(),
                                        sVNUPYUN.ToString(),
                                        this.TXT01_VNJUSO.GetValue().ToString().Trim(),
                                        this.TXT01_VNBIGO.GetValue().ToString().Trim(),
                                        this.TXT01_VNREDPMK.GetValue().ToString().Trim(),
                                        this.TXT01_VNRENAME.GetValue().ToString().Trim(),
                                        this.TXT01_VNRETEL.GetValue().ToString().Trim(),
                                        this.TXT01_VNREMAIL.GetValue().ToString().Trim(),
                                        this.TXT01_VNREMAIL1.GetValue().ToString().Trim(),
                                        this.CBO01_VNREUSCK.GetValue().ToString(),
                                        "",
                                        "",
                                        "",
                                        this.TXT01_VNVSNAME.GetValue().ToString().Trim(),
                                        this.TXT01_VNVSTEL.GetValue().ToString().Trim().Replace("-",""),
                                        this.TXT01_VNVSMAIL.GetValue().ToString().Trim(),
                                        this.TXT01_VNNEWADD.GetValue().ToString().Trim(),
                                        this.TXT01_VNJGSPNO.GetValue().ToString().Trim(),
                                        this.CBH01_VNRPCODE.GetValue().ToString().Trim(),
                                        this.TXT01_VNHYDPMK.GetValue().ToString().Trim(),
                                        this.TXT01_VNHYNAME.GetValue().ToString().Trim(),
                                        this.TXT01_VNHYTEL.GetValue().ToString().Trim(),
                                        this.TXT01_VNHYMAIL.GetValue().ToString().Trim(),
                                        this.TXT01_VNHYMAIL1.GetValue().ToString().Trim(),
                                        this.TXT01_VNMCDPMK.GetValue().ToString().Trim(),
                                        this.TXT01_VNMCNAME.GetValue().ToString().Trim(),
                                        this.TXT01_VNMCTEL.GetValue().ToString().Trim(),
                                        this.TXT01_VNMCMAIL.GetValue().ToString().Trim(),
                                        this.TXT01_VNMCMAIL1.GetValue().ToString().Trim(),
                                        this.CBO01_VNHMTAXGN.GetValue().ToString().Trim(),
                                        this.CBO01_VNCLGUBN.GetValue().ToString().Trim(),
                                        this.TXT01_VNPERCNT.GetValue().ToString().Trim(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper()
                                        );

                this.DbConnector.ExecuteNonQuery();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_83JGV717", "HJ", this.TXT01_VNCODE.GetValue().ToString());

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    // 코드관리 등록
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_668DE089",
                                            "HJ",
                                            this.TXT01_VNCODE.GetValue().ToString(),
                                            this.TXT01_VNSANGHO.GetValue().ToString().Trim(),
                                            "",
                                            "",
                                            TYUserInfo.EmpNo.ToString().Trim().ToUpper()
                                            );

                    this.DbConnector.ExecuteNonQuery();
                }
                else
                {
                    // 코드관리 수정
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_668DG090",
                                            this.TXT01_VNSANGHO.GetValue().ToString().Trim(),
                                            "",
                                            "",
                                            TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                            "HJ",
                                            this.TXT01_VNCODE.GetValue().ToString()
                                            );

                    this.DbConnector.ExecuteNonQuery();
                }
            }
            else
            {
                // 수정
                this.DbConnector.Attach("TY_P_UT_BA7FH608",
                                        this.TXT01_VNSAUPNO.GetValue().ToString(),
                                        this.CBO01_VNGUBUN.GetValue().ToString().Trim(),
                                        this.TXT01_VNSANGHO.GetValue().ToString().Trim(),
                                        this.TXT01_VNIRUM.GetValue().ToString().Trim(),
                                        this.TXT01_VNUPJONG.GetValue().ToString().Trim(),
                                        this.TXT01_VNUPTE.GetValue().ToString().Trim(),
                                        this.TXT01_VNTEL.GetValue().ToString().Trim(),
                                        sVNUPYUN.ToString(),
                                        this.TXT01_VNJUSO.GetValue().ToString().Trim(),
                                        this.TXT01_VNBIGO.GetValue().ToString().Trim(),
                                        this.TXT01_VNREDPMK.GetValue().ToString().Trim(),
                                        this.TXT01_VNRENAME.GetValue().ToString().Trim(),
                                        this.TXT01_VNRETEL.GetValue().ToString().Trim(),
                                        this.TXT01_VNREMAIL.GetValue().ToString().Trim(),
                                        this.TXT01_VNREMAIL1.GetValue().ToString().Trim(),
                                        this.CBO01_VNREUSCK.GetValue().ToString(),
                                        "",
                                        "",
                                        "",
                                        this.TXT01_VNVSNAME.GetValue().ToString().Trim(),
                                        this.TXT01_VNVSTEL.GetValue().ToString().Trim().Replace("-", ""),
                                        this.TXT01_VNVSMAIL.GetValue().ToString().Trim(),
                                        this.TXT01_VNNEWADD.GetValue().ToString().Trim(),
                                        this.TXT01_VNJGSPNO.GetValue().ToString().Trim(),
                                        this.CBH01_VNRPCODE.GetValue().ToString().Trim(),
                                        this.TXT01_VNHYDPMK.GetValue().ToString().Trim(),
                                        this.TXT01_VNHYNAME.GetValue().ToString().Trim(),
                                        this.TXT01_VNHYTEL.GetValue().ToString().Trim(),
                                        this.TXT01_VNHYMAIL.GetValue().ToString().Trim(),
                                        this.TXT01_VNHYMAIL1.GetValue().ToString().Trim(),
                                        this.TXT01_VNMCDPMK.GetValue().ToString().Trim(),
                                        this.TXT01_VNMCNAME.GetValue().ToString().Trim(),
                                        this.TXT01_VNMCTEL.GetValue().ToString().Trim(),
                                        this.TXT01_VNMCMAIL.GetValue().ToString().Trim(),
                                        this.TXT01_VNMCMAIL1.GetValue().ToString().Trim(),
                                        this.CBO01_VNHMTAXGN.GetValue().ToString().Trim(),
                                        this.CBO01_VNCLGUBN.GetValue().ToString().Trim(),
                                        this.TXT01_VNPERCNT.GetValue().ToString().Trim(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                        this.TXT01_VNCODE.GetValue().ToString()
                                        );

                this.DbConnector.ExecuteNonQuery();
            }


            if (this.CBO01_VNREUSCK.GetValue().ToString() == "Y")
            {
                // 회계 거래처 신주소 업데이트
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_66DI7174",
                                        this.TXT01_VNNEWADD.GetValue().ToString().Trim(),
                                        this.TXT01_VNSAUPNO.GetValue().ToString()
                                        );

                this.DbConnector.ExecuteNonQuery();
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ShowMessage("TY_M_GB_23NAD873");
            this.Close();
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            /******************************************************************
			 * 1. 등록, 수정시 동일한 사업자 번호는 안들어가짐.               *
			 * 2. 사업자번호 업데이트 안됨.                                   *
			 *    2-1. 수정시 한번이라도 거래된 경우(재고테이블)              *
			 *    2-2. 수정시 한번이라도 전표가 발행된 경우                   *
			 * 3. 삭제 불가                                                   *
			 *    3-1. 한번이라도 거래된 경우(재고테이블)                     *
			 *    3-1. 한번이라도 전표가 발행된 경우                          *
			 ******************************************************************/

            string sVNCOE = string.Empty;

            DataTable dt = new DataTable();

            if (this.fsVNCODE == "")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_66DFG160", this.TXT01_VNCODE.GetValue().ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_736F3857");
                    e.Successed = false;
                    return;
                }
            }

            if (this.TXT01_VNUPYUN1.GetValue().ToString() != "")
            {
                if (this.TXT01_VNUPYUN1.GetValue().ToString().Length < 3)
                {
                    this.ShowMessage("TY_M_UT_66DGC161");
                    e.Successed = false;
                    return;
                }

                if (this.TXT01_VNUPYUN2.GetValue().ToString().Length < 3)
                {
                    this.ShowMessage("TY_M_UT_66DGC161");
                    e.Successed = false;
                    return;
                }
            }

            // 신규 등록일 경우에만 사업자번호를 체크 함
            if (this.fsVNCODE == "")
            {
                if (TXT01_VNSAUPNO.GetValue().ToString() != "")
                {
                    // 사업자 번호 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_2454Y465", this.TXT01_VNSAUPNO.GetValue());
                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_UT_66DGE163");
                        e.Successed = false;
                        return;
                    }

                    // 1경우
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_66DGJ166", this.TXT01_VNSAUPNO.GetValue(), this.TXT01_VNCODE.GetValue().ToString());
                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        if (this.fsVNCODE == "")
                        {
                            this.ShowMessage("TY_M_UT_66DGE164");
                            e.Successed = false;
                            return;
                        }
                        else
                        {
                            if (this.TXT01_VNCODE.GetValue().ToString() != dt.Rows[0][0].ToString())
                            {
                                this.ShowMessage("TY_M_UT_66DGE164");
                                e.Successed = false;
                                return;
                            }
                        }
                    }

                    // 2. 한번이라도 거래된 경우 수정시 사업자번호 기존 사업자 번호 그대로 가져감.
                    if (this.fsVNCODE != "")
                    {
                        #region Description : 2-1경우

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach(
                                               "TY_P_UT_669HK122",
                                               this.TXT01_VNCODE.GetValue().ToString()
                                               );

                        if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                        {
                            this.TXT01_VNSAUPNO.SetValue(this.fsVNSAUPNO.ToString());
                        }

                        #endregion

                        #region Description : 2-2경우

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach(
                                               "TY_P_UT_669HN123",
                                               this.TXT01_VNCODE.GetValue().ToString()
                                               );

                        if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                        {
                            this.TXT01_VNSAUPNO.SetValue(this.fsVNSAUPNO.ToString());
                        }

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach(
                                               "TY_P_UT_669HN124",
                                               this.TXT01_VNCODE.GetValue().ToString()
                                               );

                        if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                        {
                            this.TXT01_VNSAUPNO.SetValue(this.fsVNSAUPNO.ToString());
                        }

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach(
                                               "TY_P_UT_669HO125",
                                               this.TXT01_VNCODE.GetValue().ToString()
                                               );

                        if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                        {
                            this.TXT01_VNSAUPNO.SetValue(this.fsVNSAUPNO.ToString());
                        }

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach(
                                               "TY_P_UT_669HO127",
                                               this.TXT01_VNCODE.GetValue().ToString()
                                               );

                        if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                        {
                            this.TXT01_VNSAUPNO.SetValue(this.fsVNSAUPNO.ToString());
                        }

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach(
                                               "TY_P_UT_669HO128",
                                               this.TXT01_VNCODE.GetValue().ToString()
                                               );

                        if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                        {
                            this.TXT01_VNSAUPNO.SetValue(this.fsVNSAUPNO.ToString());
                        }

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach(
                                               "TY_P_UT_669HP129",
                                               this.TXT01_VNCODE.GetValue().ToString()
                                               );

                        if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                        {
                            this.TXT01_VNSAUPNO.SetValue(this.fsVNSAUPNO.ToString());
                        }

                        #endregion
                    }
                }
            }

            if (this.CBO01_VNREUSCK.GetValue().ToString() == "Y")
            {
                if (this.TXT01_VNREMAIL.GetValue().ToString() == "" && this.TXT01_VNHYMAIL.GetValue().ToString() == "" && this.TXT01_VNMCMAIL.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_UT_66DGE165");
                    e.Successed = false;
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

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 사업자번호
        private void BTN61_VNSAUPNO3_Click(object sender, EventArgs e)
        {
            TYUTGB001S popup = new TYUTGB001S();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_VNSAUPNO.SetValue(popup.fsVNSAUPNO);

                this.TXT01_VNSANGHO.SetValue(popup.fsVNSANGHO);
            }
        }
        #endregion
    }
}