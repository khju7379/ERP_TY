using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.IO;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;


namespace TY.ER.AC00
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
    public partial class TYACAB006I : TYBase
    {
        private string _VNCODE;
        private TYData DAT01_VNHISAB;
        private TYData DAT01_VNSJGB;

        #region Description : 페이지 로드
        public TYACAB006I(string VNCODE)
        {
            InitializeComponent();

            this.SetPopupStyle();

            // 파라미터값 가져오기 
            this._VNCODE = VNCODE;

            // 로그인 사번 가져오기
            this.DAT01_VNHISAB = new TYData("DAT01_VNHISAB", TYUserInfo.EmpNo);
            // 사업 & 주민 구분
            this.DAT01_VNSJGB = new TYData("DAT01_VNSJGB", "");
        }

        private void TYACAB006I_Load(object sender, System.EventArgs e)
        {
            (this.FPS91_TY_S_AC_667AT055.Sheets[0].Columns[7].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.Download;

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.ControlFactory.Add(this.DAT01_VNHISAB);
            this.ControlFactory.Add(this.DAT01_VNSJGB);

            if (string.IsNullOrEmpty(this._VNCODE))
            {
                this.TXT01_VNCODE.SetReadOnly(true);
            }
            else
            {
                this.TXT01_VNCODE.SetReadOnly(true);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2445D438", this._VNCODE);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                    this.CurrentDataTableRowMapping(dt, "01");                
            }

            UP_FileDataBinding();

            SetStartingFocus(this.TXT01_VNSAUPNO1);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sVNSJGB     = string.Empty;
            string sVNSAUPNO   = string.Empty;
            string sVNSANGHO   = string.Empty;
            string sCB_CUSTGBN = string.Empty;
            string sVNUPYUN    = string.Empty;
            string sCB_CREDIT  = string.Empty;

            string sRIUM = this.TXT01_VNIRUMB.GetValue().ToString().Trim();

            if (this.TXT01_VNUGUBN.GetValue().ToString() != "Y")
            {
                this.TXT01_VNUGUBN.SetValue("");
            }

            if (this.CBO01_VNPGUBN.GetValue().ToString() != "2" || this.CBO01_VNPGUBN.GetValue().ToString() != "4")
            {
                if (this.TXT01_VNSAUPNO1.GetValue().ToString() != "" && this.TXT01_VNSAUPNO2.GetValue().ToString() != "" && this.TXT01_VNSAUPNO3.GetValue().ToString() != "")
                {
                    this.DAT01_VNSJGB.SetValue("1");

                    sVNSAUPNO = this.TXT01_VNSAUPNO1.GetValue().ToString() + this.TXT01_VNSAUPNO2.GetValue().ToString() + this.TXT01_VNSAUPNO3.GetValue().ToString();
                }
                else
                {
                    this.DAT01_VNSJGB.SetValue("2");
                    sVNSAUPNO = this.TXT01_VNJUMIN1.GetValue().ToString() + this.TXT01_VNJUMIN2.GetValue().ToString();
                }
            }
             
            this.DbConnector.CommandClear();

            if (string.IsNullOrEmpty(this._VNCODE))
            {
                // INDEX 순번 가져오는 SP
                this.DbConnector.Attach("TY_P_AC_245BX447", "");
                // SP의 OUTPUT 값 가져오는 부분
                this.TXT01_VNCODE.SetValue(Convert.ToString(this.DbConnector.ExecuteScalar()).PadLeft(6, '0'));

                this.ControlFactory.Remove(this.TXT01_VNHIDAT);
                 
                // 등록
                this.DbConnector.Attach("TY_P_AC_2444X432",
                                        this.TXT01_VNCODE.GetValue().ToString().Trim(),
                                        sVNSAUPNO,
                                        this.DAT01_VNSJGB.GetValue().ToString().Trim(),
                                        this.CBO01_VNGUBUN.GetValue().ToString().Trim(),
                                        this.TXT01_VNSANGHO.GetValue().ToString().Trim(),
                                        this.TXT01_VNIRUM.GetValue().ToString().Trim(),
                                        this.TXT01_VNUPJONG.GetValue().ToString().Trim(),
                                        this.TXT01_VNUPTE.GetValue().ToString().Trim(),
                                        this.TXT01_VNTEL.GetValue().ToString().Trim(),
                                        this.TXT01_VNUPYUN1.GetValue().ToString().Trim(), this.TXT01_VNUPYUN2.GetValue().ToString().Trim(),
                                        this.TXT01_VNJUSO.GetValue().ToString().Trim(),
                                        this.TXT01_VNNEWADD.GetValue().ToString().Trim(),
                                        this.TXT01_VNBIGO.GetValue().ToString().Trim(),
                                        this.CBH01_VNCDBK.GetValue().ToString().Trim(),
                                        this.TXT01_VNNOAC.GetValue().ToString().Trim(),
                                        this.CBO01_VNJJGUB.GetValue().ToString().Trim(),
                                        this.CKB01_VNBKYN.GetValue().ToString().Trim(),
                                        this.CBO01_VNPGUBN.GetValue().ToString().Trim(),
                                        this.TXT01_VNUGUBN.GetValue().ToString().Trim(),
                                        this.TXT01_VNPOSTB1.GetValue().ToString().Trim(), this.TXT01_VNPOSTB2.GetValue().ToString().Trim(),
                                        this.TXT01_VNADDRB.GetValue().ToString().Trim(),
                                        sRIUM, // this.TXT01_VNIRUM_.GetValue().ToString().Trim(),
                                        this.TXT01_VNDISCB.GetValue().ToString().Trim(),
                                        this.TXT01_VNBOTEL.GetValue().ToString().Trim(),
                                        this.TXT01_VNBOFAX.GetValue().ToString().Trim(),
                                        this.CKB01_VNCOGUBN.GetValue().ToString().Trim(),
                                        this.CBH01_VNCOCDBK.GetValue().ToString().Trim(),
                                        this.TXT01_VNCONOAC.GetValue().ToString().Trim(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper()
                                        );


                //구무역 거래처관리 등록
                this.DbConnector.Attach("TY_P_AC_33T14389", this.TXT01_VNCODE.GetValue().ToString(),
                                                            sVNSAUPNO,
                                                            this.TXT01_VNSANGHO.GetValue().ToString(),
                                                            this.TXT01_VNIRUM.GetValue().ToString(),
                                                            "",
                                                            "",
                                                            "",
                                                            this.TXT01_VNTEL.GetValue().ToString(),
                                                            "",
                                                            this.TXT01_VNUPTE.GetValue().ToString(),
                                                            this.TXT01_VNUPJONG.GetValue().ToString(),
                                                            "",
                                                            this.TXT01_VNJUSO.GetValue().ToString(),
                                                            "");
                // 괄호안의 숫자는 수행 순서임
                this.DbConnector.ExecuteNonQuery(0);
                this.DbConnector.ExecuteNonQuery(1);
                this.DbConnector.ExecuteNonQuery(2);
            }
            else
            {
                this.ControlFactory.Remove(this.TXT01_VNHIDAT);

                // 수정
                this.DbConnector.Attach("TY_P_AC_2444Y433",
                                        this.DAT01_VNSJGB.GetValue().ToString().Trim(),
                                        sVNSAUPNO,
                                        this.CBO01_VNGUBUN.GetValue().ToString().Trim(),
                                        this.TXT01_VNSANGHO.GetValue().ToString().Trim(),
                                        this.TXT01_VNIRUM.GetValue().ToString().Trim(),
                                        this.TXT01_VNUPJONG.GetValue().ToString().Trim(),
                                        this.TXT01_VNUPTE.GetValue().ToString().Trim(),
                                        this.TXT01_VNTEL.GetValue().ToString().Trim(),
                                        this.TXT01_VNUPYUN1.GetValue().ToString().Trim().PadLeft(3, ' ') + this.TXT01_VNUPYUN2.GetValue().ToString().Trim().PadLeft(3, ' '),
                                        this.TXT01_VNJUSO.GetValue().ToString().Trim(),
                                        this.TXT01_VNNEWADD.GetValue().ToString().Trim(),
                                        this.TXT01_VNBIGO.GetValue().ToString().Trim(),
                                        this.CBH01_VNCDBK.GetValue().ToString().Trim(),
                                        this.TXT01_VNNOAC.GetValue().ToString().Trim(),
                                        this.CBO01_VNJJGUB.GetValue().ToString().Trim(),
                                        this.CKB01_VNBKYN.GetValue().ToString().Trim(),
                                        this.CBO01_VNPGUBN.GetValue().ToString().Trim(),
                                        this.TXT01_VNUGUBN.GetValue().ToString().Trim(),
                                        this.TXT01_VNPOSTB1.GetValue().ToString().Trim().PadLeft(3, ' ') + this.TXT01_VNPOSTB2.GetValue().ToString().Trim().PadLeft(3, ' '),
                                        this.TXT01_VNADDRB.GetValue().ToString().Trim(),
                                        sRIUM,
                                        this.TXT01_VNDISCB.GetValue().ToString().Trim(),
                                        this.TXT01_VNBOTEL.GetValue().ToString().Trim(),
                                        this.TXT01_VNBOFAX.GetValue().ToString().Trim(),
                                        this.CKB01_VNCOGUBN.GetValue().ToString().Trim(),
                                        this.CBH01_VNCOCDBK.GetValue().ToString().Trim(),
                                        this.TXT01_VNCONOAC.GetValue().ToString().Trim(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                        this.TXT01_VNCODE.GetValue().ToString().Trim()
                    );

                this.DbConnector.Attach("TY_P_AC_33T24390", sVNSAUPNO,
                                                                            this.TXT01_VNSANGHO.GetValue().ToString(),
                                                                            this.TXT01_VNIRUM.GetValue().ToString(),
                                                                            "",
                                                                            "",
                                                                            "",
                                                                            this.TXT01_VNTEL.GetValue().ToString(),
                                                                            "",
                                                                            this.TXT01_VNUPTE.GetValue().ToString(),
                                                                            this.TXT01_VNUPJONG.GetValue().ToString(),
                                                                            "",
                                                                            this.TXT01_VNJUSO.GetValue().ToString(),
                                                                            "",
                                                                            this.TXT01_VNCODE.GetValue().ToString());

                // 괄호안의 숫자는 수행 순서임
                this.DbConnector.ExecuteNonQuery(0);
                this.DbConnector.ExecuteNonQuery(1);
            }
             
            if (this.CBO01_VNPGUBN.GetValue().ToString() == "1" || this.CBO01_VNPGUBN.GetValue().ToString() == "2")
            {
                sVNSAUPNO = "";
                sVNSAUPNO = this.TXT01_VNSAUPNO1.GetValue().ToString() + this.TXT01_VNSAUPNO2.GetValue().ToString() + this.TXT01_VNSAUPNO3.GetValue().ToString();
                sVNSANGHO = this.TXT01_VNSANGHO.GetValue().ToString().Replace("'", "''''");

                if (this.CBO01_VNPGUBN.GetValue().ToString() == "1")
                {
                    sCB_CUSTGBN = "02";
                }
                else if (this.CBO01_VNPGUBN.GetValue().ToString() == "2")
                {
                    sCB_CUSTGBN = "01";
                }

                sVNUPYUN = TXT01_VNUPYUN1.GetValue().ToString().Trim() + TXT01_VNUPYUN2.GetValue().ToString().Trim();

                sCB_CREDIT = TXT01_VNNOAC.GetValue().ToString().Trim().Replace("-", "");

                this.DbConnector.CommandClear();

                // 오라클 업데이트
                this.DbConnector.Attach
                    (
                    "TY_P_AC_2424D265",
                    "",
                    "0",
                    TXT01_VNCODE.GetValue().ToString().Trim(),
                    TXT01_VNCODE.GetValue().ToString().Trim(),
                    sVNSAUPNO.ToString().Trim(),
                    sCB_CUSTGBN.ToString().Trim(),
                    sVNSANGHO.ToString().Trim(),
                    "",
                    "",
                    "",
                    sVNSANGHO.ToString().Trim(),
                    TXT01_VNIRUM.GetValue().ToString().Trim(),
                    "",
                    TXT01_VNUPJONG.GetValue().ToString().Trim(),
                    TXT01_VNUPTE.GetValue().ToString().Trim(),
                    "",
                    "",
                    TXT01_VNTEL.GetValue().ToString().Trim(),
                    sVNUPYUN.ToString().Trim(),
                    sVNUPYUN.ToString().Trim(),
                    TXT01_VNJUSO.GetValue().ToString().Trim(),
                    "",
                    "",
                    "",
                    TXT01_VNJUSO.GetValue().ToString().Trim(),
                    "",
                    "",
                    "",
                    TXT01_VNBIGO.GetValue().ToString().Trim(),
                    CBH01_VNCDBK.GetValue().ToString().Trim(), // 은행코드
                    CBH01_VNCDBK.GetText().Trim(),             // 은행코드명
                    sCB_CREDIT.ToString(),
                    "1",
                    DateTime.Now.ToString("yyyyMMdd"),
                    DateTime.Now.ToString("yyyyMMdd"),
                    DAT01_VNHISAB.GetValue().ToString(),
                    DAT01_VNHISAB.GetValue().ToString()
                    );

                // 단일의 insert,update, delete SP를 수행 할때 사용(OUTPUT값을 받아올때)
                string sMessage = this.DbConnector.ExecuteScalar(0).ToString();

                if (sMessage.ToString().Substring(0, 1) == "I")
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.ShowMessage("TY_M_GB_23NAD873");
                    this.Close();
                }
                else
                {
                    //this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    //this.ShowMessage("TY_M_AC_246A2488");
                    //this.Close();
                    throw new Exception(sMessage);
                }
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.ShowMessage("TY_M_GB_23NAD873");
                this.Close();
            }

            //// 다중의 insert,update, delete SP를 수행 할때 사용
            //this.DbConnector.ExecuteNonQueryList();

            //// 단일의 insert,update, delete SP를 수행 할때 사용
            //this.DbConnector.ExecuteNonQuery();
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            if (this.CBO01_VNPGUBN.GetValue().ToString() != "2" && this.CBO01_VNPGUBN.GetValue().ToString() != "4")
            {
                if ((this.TXT01_VNSAUPNO1.GetValue().ToString() == "" && this.TXT01_VNJUMIN1.GetValue().ToString() == "") ||
                    (this.TXT01_VNSAUPNO1.GetValue().ToString() != "" && this.TXT01_VNJUMIN1.GetValue().ToString() != "")
                   )
                {
                    this.ShowMessage("TY_M_AC_2454S464");
                    e.Successed = false;
                    return;
                }
            }

            // 등록
            if (string.IsNullOrEmpty(this._VNCODE))
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_2CE3M197",
                    this.TXT01_VNCODE.GetValue().ToString()
                    );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_2CE3N200");
                    e.Successed = false;
                    return;
                }
            }

            // 사업자번호 중복 체크
            if (TXT01_VNSAUPNO1.GetValue().ToString() != "")
            {
                string sVNSAUPNO = string.Empty;

                sVNSAUPNO = this.TXT01_VNSAUPNO1.GetValue().ToString() + this.TXT01_VNSAUPNO2.GetValue().ToString() + this.TXT01_VNSAUPNO3.GetValue().ToString();
                // 등록
                if (string.IsNullOrEmpty(this._VNCODE))
                {
                    // 사업자 번호 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_2454Y465", sVNSAUPNO.ToString());
                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_AC_2445G439");
                        e.Successed = false;
                        return;
                    }
                }
                else // 수정
                {
                    // 사업자 번호 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_24550471", sVNSAUPNO.ToString(), this.TXT01_VNCODE.GetValue().ToString());
                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_AC_2445G439");
                        e.Successed = false;
                        return;
                    }

                    // 미승인 전표 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach(
                                           "TY_P_AC_24433418",
                                           this.TXT01_VNCODE.GetValue().ToString(),
                                           this.TXT01_VNCODE.GetValue().ToString(),
                                           this.TXT01_VNCODE.GetValue().ToString(),
                                           this.TXT01_VNCODE.GetValue().ToString(),
                                           this.TXT01_VNCODE.GetValue().ToString(),
                                           this.TXT01_VNCODE.GetValue().ToString()
                                           );

                    if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                    {
                        sVNSAUPNO = this.TXT01_VNSAUPNO1.GetValue().ToString() + this.TXT01_VNSAUPNO2.GetValue().ToString() + this.TXT01_VNSAUPNO3.GetValue().ToString();

                        if (sVNSAUPNO != "0000000000")
                        {
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_AC_31I9L799", this.TXT01_VNCODE.GetValue().ToString());

                            dt = this.DbConnector.ExecuteDataTable();

                            if (dt.Rows.Count > 0)
                            {
                                if (dt.Rows[0]["VNSAUPNO"].ToString() != sVNSAUPNO.ToString())
                                {
                                    this.ShowMessage("TY_M_AC_31I9O800");
                                    e.Successed = false;
                                    return;
                                }
                            }
                        }
                    }
                }
            }

            // 계좌번호 체크
            if (this.CBH01_VNCDBK.GetValue().ToString() != "" && this.TXT01_VNNOAC.GetValue().ToString() == "")
            {
                this.ShowMessage("TY_M_AC_2445M441");

                this.TXT01_VNNOAC.Focus();

                e.Successed = false;
                return;
            }

            // 은행코드 체크
            if (this.CBH01_VNCDBK.GetValue().ToString() == "" && this.TXT01_VNNOAC.GetValue().ToString() != "")
            {
                this.ShowMessage("TY_M_AC_2445M440");
                e.Successed = false;
                return;
            }

            //은행코드중 외환은행(105)는 사용할수 없다.
            if (this.CBH01_VNCDBK.GetValue().ToString() == "105" )
            {
                this.ShowCustomMessage("외환은행코드는 사용할수 없습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                this.CBH01_VNCDBK.Focus();

                e.Successed = false;
                return;
            }

            if (this.CKB01_VNCOGUBN.Checked == true)
            {
                //계좌개설이 늦을수도 있어 체크 기능을 제외한다. 2020.03.27 강경석 요청
                //if (this.CBH01_VNCOCDBK.GetValue().ToString() == "")
                //{
                //    this.ShowMessage("TY_M_AC_2445M440");
                //    e.Successed = false;

                //    SetFocus(this.CBH01_VNCOCDBK.CodeText);

                //    return;
                //}

                //if (this.TXT01_VNCONOAC.GetValue().ToString() == "")
                //{
                //    this.ShowMessage("TY_M_AC_2445M441");

                //    this.TXT01_VNCONOAC.Focus();

                //    e.Successed = false;
                //    return;
                //}
            }
            else
            {
                // 사용했다가 사용안할경우 은행 및 계좌번호 클리어 안되도록 요청. 2020.05.11 강경석 요청
                //this.CBH01_VNCOCDBK.SetValue("");
                //this.TXT01_VNCONOAC.SetValue("");
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

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYACAB06C1(this._VNCODE, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                UP_FileDataBinding();
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_667AY058", dt);
            this.DbConnector.ExecuteNonQueryList();

            UP_FileDataBinding();

            this.ShowMessage("TY_M_GB_23NAD874");            
        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_667AT055.GetDataSourceInclude(TSpread.TActionType.Remove, "AFVNCODE", "AFSEQ");

            if (dt.Rows.Count == 0)
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

            e.ArgData = dt;
        }
        #endregion

        #region Description : 첨부파일 조회
        private void UP_FileDataBinding()
        {
            FPS91_TY_S_AC_667AT055.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_667AS052", this._VNCODE);
            FPS91_TY_S_AC_667AT055.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : FPS91_TY_S_AC_667AT055_CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_667AT055_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYACAB06C1(this.FPS91_TY_S_AC_667AT055.GetValue("AFVNCODE").ToString(), this.FPS91_TY_S_AC_667AT055.GetValue("AFSEQ").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                UP_FileDataBinding();
        }
        #endregion

        #region Description : FPS91_TY_S_AC_667AT055_ButtonClicked 이벤트
        private void FPS91_TY_S_AC_667AT055_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "7")
            {
                this.UP_AttachFileDown(this.FPS91_TY_S_AC_667AT055.GetValue("AFVNCODE").ToString(),
                                       this.FPS91_TY_S_AC_667AT055.GetValue("AFSEQ").ToString()
                                       );
            }
        }
        #endregion

        
        #region Description : 첨부관리 다운로드 이벤트
        private void UP_AttachFileDown(string sAFVNCODE, string sAFSEQ)
        {
            FileStream stream = null;
            int iArraySize = 0;
            byte[] _AttachFile = null;
            string sAFFILENAME = string.Empty;

            try
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_667AY059", sAFVNCODE, sAFSEQ);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sAFFILENAME = dt.Rows[0]["AFFILENAME"].ToString();
                    _AttachFile = dt.Rows[0]["AFFILEBYTE"] as byte[];
                    iArraySize = _AttachFile.GetUpperBound(0);
                }

                this.saveFileDialog.FileName = sAFFILENAME;
                if (this.saveFileDialog.ShowDialog() == DialogResult.Cancel) return;
                string fileName = this.saveFileDialog.FileName;

                stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                stream.Write(_AttachFile, 0, iArraySize + 1);

                this.ShowMessage("TY_M_GB_25UAA711");

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }
        #endregion

        #region Description : 노무비 닷컴 이벤트
        private void CKB01_VNCOGUBN_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CKB01_VNCOGUBN.Checked == true)
            {
                this.CBH01_VNCOCDBK.Enabled = true;
                this.TXT01_VNCONOAC.Enabled = true;
            }
            else
            {
                //this.CBH01_VNCOCDBK.SetValue("");
                //this.TXT01_VNCONOAC.SetValue("");

                this.CBH01_VNCOCDBK.Enabled = false;
                this.TXT01_VNCONOAC.Enabled = false;
            }
        }
        #endregion




        //private void CBH01_VNCDBK_CodeBoxDataBinded(object sender, EventArgs e)
        //{
        //    this.Session.SetValue("key", new object[] { this.CBH01_VNCDBK.GetValue(), this.TXT01_VNNOAC.GetValue()});
        //}
    }
}