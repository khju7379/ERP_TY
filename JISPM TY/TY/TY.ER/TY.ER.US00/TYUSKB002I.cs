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
    /// 거래처관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2019.02.25 14:13
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_92PHJ894 : 거래처관리 등록
    ///  TY_P_US_92PHL895 : 거래처관리 수정
    ///  TY_P_US_92PHM896 : 거래처관리 삭제
    ///  TY_P_US_92SGJ966 : 거래처관리 삭제 - 재고테이블 체크
    ///  TY_P_US_92SGN967 : 거래처관리 삭제 - 미승인전표 체크
    ///  TY_P_US_935AG979 : 거래처관리 확인
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_43C9G671 : 삭제 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  REM : 삭제
    ///  SAV : 저장
    ///  VNSOSOK : 소속협회
    ///  VNGUBUN : 구분
    ///  VNCODE : 거래처코드
    ///  VNIRUM : 대표자명
    ///  VNNEWADD : 신주소
    ///  VNREDPMK : 접안료-부서명
    ///  VNRELJUSO : 관련업체주소
    ///  VNRELSANG : 관련업체상호
    ///  VNRELTEL : 관련업체전화번호
    ///  VNRELUPYUN : 관련업체우편번호
    ///  VNREMAIL : 접안료-담당메일
    ///  VNREMAIL1 : 접안료-담당메일1
    ///  VNRENAME : 접안료-담당자명
    ///  VNRETEL : 접안료-담당전화
    ///  VNREUSCK : 전자사용구분
    ///  VNSANGHO : 거래처명
    ///  VNSANGHO1 : 거래처명-영문
    ///  VNSAUPJA : 사업자등록번호
    ///  VNTAXJUSO : 주소-세금
    ///  VNTAXTEL : 전화번호-세금
    ///  VNTAXUPYUN : 우편번호-세금
    ///  VNTEL : 전화번호
    ///  VNUPJONG : 종목
    ///  VNUPTAE : 업태
    /// </summary>
    public partial class TYUSKB002I : TYBase
    {
        private string fsVNCODE;
        private string fsVNSAUPJA;
        private string fsVNBLCLAIM;

        #region Description : 폼 로드
        public TYUSKB002I(string sVNCODE)
        {
            InitializeComponent();

            fsVNCODE = sVNCODE;
        }

        private void TYUSKB002I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            if (string.IsNullOrEmpty(this.fsVNCODE))
            {
                this.TXT01_VNCODE.SetReadOnly(false);
                this.BTN61_VNSAUPNO3.Visible = true;

                UP_FieldClear();

                SetStartingFocus(this.TXT01_VNCODE);
            }
            else
            {
                this.TXT01_VNCODE.SetReadOnly(true);
                this.BTN61_VNSAUPNO3.Visible = false;

                UP_Run();

                SetStartingFocus(this.TXT01_VNSAUPJA);
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

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {   
            if (string.IsNullOrEmpty(this.fsVNCODE))
            {
                // 등록
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_BA7EQ600",
                                        TXT01_VNCODE.GetValue().ToString(),
                                        TXT01_VNSAUPJA.GetValue().ToString(),
                                        CBO01_VNGUBUN.GetValue().ToString(),
                                        TXT01_VNSANGHO.GetValue().ToString(),
                                        TXT01_VNSANGHO1.GetValue().ToString(),
                                        TXT01_VNIRUM.GetValue().ToString(),
                                        TXT01_VNUPJONG.GetValue().ToString(),
                                        TXT01_VNUPTAE.GetValue().ToString(),
                                        TXT01_VNTEL.GetValue().ToString(),
                                        TXT01_VNTAXTEL.GetValue().ToString(),
                                        TXT01_VNTAXUPYUN.GetValue().ToString() + TXT01_VNTAXUPYUN1.GetValue().ToString(),
                                        TXT01_VNTAXJUSO.GetValue().ToString(),
                                        TXT01_VNRELSANG.GetValue().ToString(),
                                        TXT01_VNRELTEL.GetValue().ToString(),
                                        TXT01_VNRELUPYUN.GetValue().ToString() + TXT01_VNRELUPYUN1.GetValue().ToString(),
                                        TXT01_VNRELJUSO.GetValue().ToString(),
                                        CBH01_VNSOSOK.GetValue().ToString(),
                                        TXT01_VNREDPMK.GetValue().ToString(),
                                        TXT01_VNRENAME.GetValue().ToString(),
                                        TXT01_VNRETEL.GetValue().ToString(),
                                        TXT01_VNREMAIL.GetValue().ToString(),
                                        TXT01_VNREMAIL1.GetValue().ToString(),
                                        CBO01_VNREUSCK.GetValue().ToString(),
                                        TXT01_VNNEWADD.GetValue().ToString(),
                                        CBH01_VNRPCODE.GetValue().ToString(),
                                        CBO01_VNBLCLAIM.GetValue().ToString(),
                                        TXT01_VNPERCNT.GetValue().ToString(),
                                        TXT01_VNJGSPNO.GetValue().ToString(),
                                        TYUserInfo.EmpNo.ToString().Trim()
                                        );

                this.DbConnector.ExecuteNonQuery();
            }
            else
            {
                // 수정
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_BA7EQ601",
                                        TXT01_VNSAUPJA.GetValue().ToString(),
                                        CBO01_VNGUBUN.GetValue().ToString(),
                                        TXT01_VNSANGHO.GetValue().ToString(),
                                        TXT01_VNSANGHO1.GetValue().ToString(),
                                        TXT01_VNIRUM.GetValue().ToString(),
                                        TXT01_VNUPJONG.GetValue().ToString(),
                                        TXT01_VNUPTAE.GetValue().ToString(),
                                        TXT01_VNTEL.GetValue().ToString(),
                                        TXT01_VNTAXTEL.GetValue().ToString(),
                                        TXT01_VNTAXUPYUN.GetValue().ToString() + TXT01_VNTAXUPYUN1.GetValue().ToString(),
                                        TXT01_VNTAXJUSO.GetValue().ToString(),
                                        TXT01_VNRELSANG.GetValue().ToString(),
                                        TXT01_VNRELTEL.GetValue().ToString(),
                                        TXT01_VNRELUPYUN.GetValue().ToString() + TXT01_VNRELUPYUN1.GetValue().ToString(),
                                        TXT01_VNRELJUSO.GetValue().ToString(),
                                        CBH01_VNSOSOK.GetValue().ToString(),
                                        TXT01_VNREDPMK.GetValue().ToString(),
                                        TXT01_VNRENAME.GetValue().ToString(),
                                        TXT01_VNRETEL.GetValue().ToString(),
                                        TXT01_VNREMAIL.GetValue().ToString(),
                                        TXT01_VNREMAIL1.GetValue().ToString(),
                                        CBO01_VNREUSCK.GetValue().ToString(),
                                        TXT01_VNNEWADD.GetValue().ToString(),
                                        CBH01_VNRPCODE.GetValue().ToString(),
                                        CBO01_VNBLCLAIM.GetValue().ToString(),
                                        TXT01_VNPERCNT.GetValue().ToString(),
                                        TXT01_VNJGSPNO.GetValue().ToString(),
                                        TYUserInfo.EmpNo.ToString().Trim(),
                                        TXT01_VNCODE.GetValue().ToString()
                                        );

                this.DbConnector.ExecuteNonQuery();
            }
            if (this.CBO01_VNREUSCK.GetValue().ToString() == "Y")
            {
                // 회계 거래처 신 주소 업데이트
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_936GJ008",
                                        TXT01_VNNEWADD.GetValue().ToString(),
                                        TXT01_VNSAUPJA.GetValue().ToString()
                                        );

                this.DbConnector.ExecuteNonQuery();
            }
            fsVNCODE    = TXT01_VNCODE.GetValue().ToString();
            fsVNSAUPJA  = TXT01_VNSAUPJA.GetValue().ToString();
            
            fsVNBLCLAIM = CBO01_VNBLCLAIM.GetValue().ToString();

            this.BTN61_VNSAUPNO3.Visible = false;
            UP_Run();

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (TXT01_VNTAXUPYUN.GetValue().ToString() != "" || TXT01_VNTAXUPYUN1.GetValue().ToString() != "")
            {
                if (TXT01_VNTAXUPYUN.GetValue().ToString().Length != 3 || TXT01_VNTAXUPYUN1.GetValue().ToString().Length != 3)
                {
                    this.ShowMessage("TY_M_UT_66DGC161");
                    e.Successed = false;
                    this.TXT01_VNTAXUPYUN.Focus();
                    return;
                }
            }

            if (TXT01_VNRELUPYUN.GetValue().ToString() != "" || TXT01_VNRELUPYUN1.GetValue().ToString() != "")
            {
                if (TXT01_VNRELUPYUN.GetValue().ToString().Length != 3 || TXT01_VNRELUPYUN1.GetValue().ToString().Length != 3)
                {
                    this.ShowMessage("TY_M_UT_66DGC161");
                    e.Successed = false;
                    this.TXT01_VNRELUPYUN.Focus();
                    return;
                }
            }

            if (this.CBO01_VNREUSCK.GetValue().ToString() == "Y")
            {
                if (this.TXT01_VNREMAIL.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_UT_66DGE165");
                    e.Successed = false;
                    this.TXT01_VNREMAIL.Focus();
                    return;
                }
            }

            /******************************************************************
			 * 1. 등록, 수정시 동일한 사업자 번호는 안들어가짐.               *
			 * 2. 사업자번호 업데이트 안됨.                                   *
			 *    2-1. 수정시 한번이라도 거래된 경우(재고테이블)              *
			 *    2-2. 수정시 한번이라도 전표가 발행된 경우                   *
             *    2-3. 회계에 존재하는 사업자일경우                           * 
			 * 3. 삭제 불가                                                   *
			 *    3-1. 한번이라도 거래된 경우(재고테이블)                     *
			 *    3-2. 한번이라도 전표가 발행된 경우                          *
             *    3-3. 회계에 존재하는 사업자일경우                           * 
			******************************************************************/

            // 등록체크
            if (string.IsNullOrEmpty(this.fsVNCODE))
            {
                
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                        "TY_P_US_936D3004",
                                        this.TXT01_VNCODE.GetValue().ToString()
                                        );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_2CE3N200");
                    e.Successed = false;
                    this.TXT01_VNCODE.Focus();
                    return;
                }


                if (this.CBO01_VNBLCLAIM.GetValue().ToString() == "N")
                {
                    #region Description : 1 경우
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach(
                                           "TY_P_US_935E7987",
                                           this.TXT01_VNSAUPJA.GetValue().ToString()
                                           );

                    if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_AC_2445G439");
                        e.Successed = false;
                        this.TXT01_VNSAUPJA.Focus();
                        return;
                    }
                    #endregion
                }
            }
            // 수정체크
            else
            {
                if (fsVNBLCLAIM.ToString() != this.CBO01_VNBLCLAIM.GetValue().ToString())
                {
                    this.ShowMessage("TY_M_US_B1BEG289");
                    e.Successed = false;
                    this.CBO01_VNBLCLAIM.Focus();
                    return;
                }

                // 사업자번호를 변경하는 경우 체크
                if (this.TXT01_VNSAUPJA.GetValue().ToString() != fsVNSAUPJA)
                {
                    #region Description : 2-1경우

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach(
                                           "TY_P_US_92SGJ966",
                                           this.TXT01_VNCODE.GetValue().ToString()
                                           );

                    if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                    {
                        this.TXT01_VNSAUPJA.SetValue(fsVNSAUPJA);
                    }

                    #endregion

                    #region Description : 2-2경우

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach(
                                           "TY_P_US_92SGN967",
                                           this.TXT01_VNCODE.GetValue().ToString()
                                           );

                    if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                    {
                        this.TXT01_VNSAUPJA.SetValue(fsVNSAUPJA);
                    }

                    #endregion

                    #region Dsecription : 2-3경우

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach(
                                           "TY_P_US_935EB990",
                                           this.TXT01_VNCODE.GetValue().ToString()
                                           );

                    if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                    {
                        this.TXT01_VNSAUPJA.SetValue(fsVNSAUPJA);
                    }

                    #endregion
                }
            }

            // 회계거래처파일 사업자번호 체크 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach(
                                   "TY_P_US_935E1989",
                                   this.TXT01_VNSAUPJA.GetValue().ToString()
                                   );

            if (this.DbConnector.ExecuteDataTable().Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_66DGE163");
                e.Successed = false;
                this.TXT01_VNSAUPJA.Focus();
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Descriptoin : 확인 메소드
        private void UP_Run()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_935AG979", this.fsVNCODE);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                TXT01_VNCODE.SetValue(dt.Rows[0]["VNCODE"].ToString());
                TXT01_VNSAUPJA.SetValue(dt.Rows[0]["VNSAUPJA"].ToString());
                TXT01_VNJGSPNO.SetValue(dt.Rows[0]["VNJGSPNO"].ToString());
                CBO01_VNGUBUN.SetValue(dt.Rows[0]["VNGUBUN"].ToString());
                TXT01_VNSANGHO.SetValue(dt.Rows[0]["VNSANGHO"].ToString());
                TXT01_VNSANGHO1.SetValue(dt.Rows[0]["VNSANGHO1"].ToString());
                TXT01_VNIRUM.SetValue(dt.Rows[0]["VNIRUM"].ToString());
                TXT01_VNTEL.SetValue(dt.Rows[0]["VNTEL"].ToString());
                TXT01_VNUPJONG.SetValue(dt.Rows[0]["VNUPJONG"].ToString());
                TXT01_VNUPTAE.SetValue(dt.Rows[0]["VNUPTAE"].ToString());
                CBH01_VNSOSOK.SetValueText(dt.Rows[0]["VNSOSOK"].ToString(), dt.Rows[0]["SOSOKNM"].ToString());
                TXT01_VNTAXTEL.SetValue(dt.Rows[0]["VNTAXTEL"].ToString());

                if(dt.Rows[0]["VNTAXUPYUN"].ToString() != "")
                {
                    TXT01_VNTAXUPYUN.SetValue(dt.Rows[0]["VNTAXUPYUN"].ToString().Substring(0, 3));
                    TXT01_VNTAXUPYUN1.SetValue(dt.Rows[0]["VNTAXUPYUN"].ToString().Substring(3, 3));
                }

                TXT01_VNTAXJUSO.SetValue(dt.Rows[0]["VNTAXJUSO"].ToString());
                TXT01_VNNEWADD.SetValue(dt.Rows[0]["VNNEWADD"].ToString());
                TXT01_VNRELTEL.SetValue(dt.Rows[0]["VNRELTEL"].ToString());

                if(dt.Rows[0]["VNRELUPYUN"].ToString() != "")
                {
                    TXT01_VNRELUPYUN.SetValue(dt.Rows[0]["VNRELUPYUN"].ToString().Substring(0, 3));
                    TXT01_VNRELUPYUN1.SetValue(dt.Rows[0]["VNRELUPYUN"].ToString().Substring(3, 3));
                }

                TXT01_VNRELSANG.SetValue(dt.Rows[0]["VNRELSANG"].ToString());
                TXT01_VNRELJUSO.SetValue(dt.Rows[0]["VNRELJUSO"].ToString());
                CBO01_VNREUSCK.SetValue(dt.Rows[0]["VNREUSCK"].ToString());
                TXT01_VNREDPMK.SetValue(dt.Rows[0]["VNREDPMK"].ToString());
                TXT01_VNRENAME.SetValue(dt.Rows[0]["VNRENAME"].ToString());
                TXT01_VNRETEL.SetValue(dt.Rows[0]["VNRETEL"].ToString());
                TXT01_VNREMAIL.SetValue(dt.Rows[0]["VNREMAIL"].ToString());
                TXT01_VNREMAIL1.SetValue(dt.Rows[0]["VNREMAIL1"].ToString());
                CBH01_VNRPCODE.SetValue(dt.Rows[0]["VNRPCODE"].ToString());
                this.CBO01_VNBLCLAIM.SetValue(dt.Rows[0]["VNBLCLAIM"].ToString());
                TXT01_VNPERCNT.SetValue(dt.Rows[0]["VNPERCNT"].ToString());

                fsVNSAUPJA  = dt.Rows[0]["VNSAUPJA"].ToString();

                fsVNBLCLAIM = dt.Rows[0]["VNBLCLAIM"].ToString();
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_935BZ983", this.fsVNCODE);
            this.FPS91_TY_S_US_935BZ984.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Descriptoin : 필드 초기화
        private void UP_FieldClear()
        {
            this.TXT01_VNCODE.SetReadOnly(false);
            this.TXT01_VNCODE.SetValue("");
            this.TXT01_VNSAUPJA.SetValue("");
            this.CBO01_VNGUBUN.SetValue("1");
            this.TXT01_VNSANGHO.SetValue("");
            this.TXT01_VNSANGHO1.SetValue("");
            this.TXT01_VNIRUM.SetValue("");
            this.TXT01_VNTEL.SetValue("");
            this.TXT01_VNUPJONG.SetValue("");
            this.TXT01_VNUPTAE.SetValue("");
            this.CBH01_VNSOSOK.SetValueText("", "");
            this.TXT01_VNTAXTEL.SetValue("");
            this.TXT01_VNTAXUPYUN.SetValue("");
            this.TXT01_VNTAXUPYUN1.SetValue("");
            this.TXT01_VNTAXJUSO.SetValue("");
            this.TXT01_VNNEWADD.SetValue("");
            this.TXT01_VNRELTEL.SetValue("");
            this.TXT01_VNRELUPYUN.SetValue("");
            this.TXT01_VNRELUPYUN1.SetValue("");
            this.TXT01_VNRELSANG.SetValue("");
            this.TXT01_VNRELJUSO.SetValue("");
            this.CBO01_VNREUSCK.SetValue("N");
            this.TXT01_VNREDPMK.SetValue("");
            this.TXT01_VNRENAME.SetValue("");
            this.TXT01_VNRETEL.SetValue("");
            this.TXT01_VNREMAIL.SetValue("");
            this.TXT01_VNREMAIL1.SetValue("");
            this.TXT01_VNPERCNT.SetValue("1");

            this.FPS91_TY_S_US_935BZ984.Initialize();
        }
        #endregion

        #region Description : 회계처래처 코드박스 버튼
        private void BTN61_VNSAUPNO3_Click(object sender, EventArgs e)
        {
            TYUSGB002S popup = new TYUSGB002S();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_VNSAUPJA.SetValue(popup.fsVNSAUPNO);

                this.TXT01_VNSANGHO.SetValue(popup.fsVNSANGHO);
            }
        }
        #endregion
    }
}
