using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 부가세 부속서류 매입계산서 생성 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2013.12.27 15:13
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_3CQ4A870 : 부가세 부속서류 생성(SP)
    ///  TY_P_AC_3CQ5Y873 : 부가세 신고 거래처코드 구하기
    ///  TY_P_AC_3CRAY881 : 부가세 부속서류 삭제(SP)
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_3CR9M876 : 부가세 옵션 자료가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  INQOPTION : 조회구분
    ///  INQOPTION2 : 조회구분
    ///  VNGUBUN : 구분
    ///  S1CHK15 : 매입계산서 합계표
    ///  ELXYYMM : 기준년도
    ///  GBPRYYMM : 처리년월
    /// </summary>
    public partial class TYACTX008B : TYBase
    {
        public TYACTX008B()
        {
            InitializeComponent();
        }

        #region Description : Page_Load
        private void TYACTX008B_Load(object sender, System.EventArgs e)
        {
            this.DTP01_ELXYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            this.DTP01_GBPRYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.TXT01_S2YEAR.SetValue(DateTime.Now.ToString("yyyy")); // 기준년도 

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.CKB01_S1CHK15.Visible = false;

            this.SetStartingFocus(this.DTP01_ELXYYMM);

            this.RB_ATTAXGUBN1.Checked = true;

            Set_Start_yymm();

        }
        #endregion

        #region Description : 처리버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            int iCCNT = 0;
            string sOUTMSG = string.Empty;
            string sVNGUBUN = string.Empty;

            // 부가세 OPTION TABLE 사업장코드 가져오기
            this.DbConnector.CommandClear(); // AVOPTIONMF
            this.DbConnector.Attach("TY_P_AC_3CQ5Y873",
                                    this.TXT01_S2YEAR.GetValue().ToString() ,  // 기준년도 
                                    //this.DTP01_ELXYYMM.GetValue().ToString().Substring(0,4),
                                    this.CBO01_VNGUBUN.GetValue().ToString(),      // 사업장(1본점, 2지점)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)  // 확정구분(1.예정, 2.확정)
                                    );
            DataTable dt = this.DbConnector.ExecuteDataTable();
            sVNGUBUN = dt.Rows[0]["AOVNEDCD"].ToString();

            this.TXT01_S1CHK15.SetValue(""); // s매입계산서

            string s매입 = "Y";  // s매입계산서

            string s전체처리구분 = string.Empty;

            if (this.RB_ATTAXGUBN1.Checked == true)
            {
                s전체처리구분 = "Y"; //생성
            }
            else
            {
                s전체처리구분 = "N"; //취소
            };

            if (s매입 == "Y") { iCCNT = iCCNT + 1; };

            string sYEAR = string.Empty;
            string s시작년월 = string.Empty;
            string s종료년월 = string.Empty;

            //sYEAR = this.DTP01_ELXYYMM.GetValue().ToString().Substring(0, 4);

            sYEAR = this.TXT01_S2YEAR.GetValue().ToString();  // 기준년도 

            s시작년월 = this.DTP01_ELXYYMM.GetValue().ToString();
            s종료년월 = this.DTP01_GBPRYYMM.GetValue().ToString();

            if (s전체처리구분 == "Y")
            {
                //IN_ATCRGB 처리구분
                //          2.계산서 
                #region Description : 2.계산서(매입만) 생성 처리 (SP)
                if (s매입 == "Y")
                {
                    // 계산서(매입) - 생성 (SP)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        ("TY_P_AC_3CQ4A870",
                        "2",  // 2.계산서
                        sYEAR,
                        this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                        sVNGUBUN,                                    // 사업장코드
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2), // 확정구분(1.예정, 2.확정)
                        "1", // 계산서구분(1.매입, 2.매출)
                        s시작년월,
                        s종료년월,
                        sOUTMSG.ToString()
                        );

                    sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                    if (sOUTMSG.ToString().Substring(0, 2) == "OK")
                    {
                        this.TXT01_S1CHK15.Text = sOUTMSG;
                    }
                    else
                    {
                        this.TXT01_S1CHK15.Text = "ER-처리중 ERROR 발생";
                    }
                }
                #endregion

                if (iCCNT > 0)
                {
                    if ((new TYACTX019S(sYEAR, this.CBO01_VNGUBUN.GetValue().ToString(), this.CBO01_INQOPTION.GetValue().ToString(), "POP")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        this.ShowCustomMessage("처리완료 되었습니다.", "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
                else
                {
                    this.ShowCustomMessage("처리할구분을 선택하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            else // 삭제
            {
                #region Description : 2.계산서(매입만) 생성 처리 (SP)
                if (s매입 == "Y")
                {
                    // 계산서(매입) - 생성 (SP)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        ("TY_P_AC_3CRAY881",
                        "2",  // 2.계산서
                        sYEAR,
                        this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                        sVNGUBUN,                                    // 사업장코드
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2), // 확정구분(1.예정, 2.확정)
                        "1", // 계산서구분(1.매입, 2.매출)
                        s시작년월,
                        s종료년월,
                        sOUTMSG.ToString()
                        );

                    sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                    if (sOUTMSG.ToString().Substring(0, 2) == "OK")
                    {
                        this.TXT01_S1CHK15.Text = sOUTMSG;
                    }
                    else
                    {
                        this.TXT01_S1CHK15.Text = "ER-처리중 ERROR 발생";
                    }

                }
                #endregion

                if (iCCNT > 0)
                {
                    if ((new TYACTX019S(sYEAR, this.CBO01_VNGUBUN.GetValue().ToString(), this.CBO01_INQOPTION.GetValue().ToString(), "POP")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        this.ShowCustomMessage("처리완료 되었습니다.", "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
                else
                {
                    this.ShowCustomMessage("처리할구분을 선택하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            };

            // TYACTX019S() -- 신고서 계산 로직  (string sVSYEAR, string sVSBRANCH,         string sVSRPTGUBN,  string sPOPUP )
            //                                            년도      사업장(1본점, 2지점) ,   (신고구분+확정구분),         "POP"
        }
        #endregion

        #region Description : 처리 CHECK
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            //int iCCNT = 0;

            this.TXT01_S1CHK15.SetValue(""); // s매입계산서


            if (this.TXT01_S2YEAR.GetValue().ToString() == "")
            {
                this.ShowCustomMessage("귀속 년도를 입력하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                this.SetFocus(this.TXT01_S2YEAR);
                return;
            };


            if (this.DTP01_ELXYYMM.GetValue().ToString().Substring(0, 4) != this.DTP01_GBPRYYMM.GetValue().ToString().Substring(0, 4))
            {
                this.ShowMessage("TY_M_AC_3CRBV882");  // 기준년월 범위 지정이 잘못 되었습니다.
                e.Successed = false;
                this.SetFocus(this.DTP01_ELXYYMM);
                return;
            };

            // 시작년월이 종료년월 
            if (Int32.Parse(this.DTP01_ELXYYMM.GetString().ToString().Trim()) > Int32.Parse(this.DTP01_GBPRYYMM.GetString().ToString().Trim()))
            {
                this.ShowMessage("TY_M_AC_3CRBZ883");  // 시작년월이 종료년월 보다 작습니다
                this.SetFocus(this.DTP01_GBPRYYMM);
                e.Successed = false;
                return;
            }

            // 1.부가세 OPTION TABLE 존재 유무 확인
            this.DbConnector.CommandClear(); // AVOPTIONMF
            this.DbConnector.Attach("TY_P_AC_3CQ5Y873",
                                    this.TXT01_S2YEAR.GetValue().ToString(),  // 기준년도
                                    //this.DTP01_ELXYYMM.GetValue().ToString().Substring(0,4),
                                    this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)  // 확정구분(1.예정, 2.확정)
                                    );
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_3CR9M876");  // 부가세 옵션 자료가 없습니다.
                this.SetFocus(this.CBO01_VNGUBUN);
                e.Successed = false;
                return;
            }

            
            ////string s매입 = "Y";  // s매입계산서

            ////if (s매입 == "Y") { iCCNT = iCCNT + 1; };

            ////if (iCCNT == 0)
            ////{
            ////    this.ShowCustomMessage("처리할구분을 선택하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            ////    e.Successed = false;
            ////    this.SetFocus(this.DTP01_ELXYYMM);
            ////    return;
            ////}

            // 마감 체크 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_42B8L317",  // AVDECLMF
                this.TXT01_S2YEAR.GetValue().ToString(),  // 기준년도 
                //this.DTP01_ELXYYMM.GetValue().ToString().Substring(0, 4),
                this.CBO01_VNGUBUN.GetValue().ToString(),     // 사업장(1본점, 2지점)
                getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)  // 확정구분(1.예정, 2.확정)
                );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                this.ShowCustomMessage("신고서 마감 완료 되었습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                this.SetFocus(this.DTP01_ELXYYMM);
                return;
            }

            // 매입합계표에서 생성된 자료가 존재하는지 체크함 (삭제시만)
            if (this.RB_ATTAXGUBN2.Checked == true)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3CAB6679",  // AVTAXOPTF
                    this.TXT01_S2YEAR.GetValue().ToString(),  // 기준년도
                    //this.DTP01_ELXYYMM.GetValue().ToString().Substring(0, 4),
                    this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),  // 확정구분(1.예정, 2.확정)
                    "1" // 1. 매입
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count == 0)
                {
                    this.ShowCustomMessage("해당 기 생성된자료가 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.CBO01_INQOPTION);
                    e.Successed = false;
                    return;
                }
                else
                {
                    this.DTP01_ELXYYMM.SetValue(dt.Rows[0]["O1STYYMM"].ToString());
                    this.DTP01_GBPRYYMM.SetValue(dt.Rows[0]["O1EDYYMM"].ToString());
                }
            }
            else  //------------------- 생성 ------------------
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3CAB6679",  // AVTAXOPTF
                    this.TXT01_S2YEAR.GetValue().ToString(),  // 기준년도
                    //this.DTP01_ELXYYMM.GetValue().ToString().Substring(0, 4),
                    this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),  // 확정구분(1.예정, 2.확정)
                    "1" // 1. 매입
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count != 0)
                {
                    Set_Start_yymm(); // 생성시 마지막 생성월을 가지고 옮
                }
            }
        }
        #endregion

        #region Description : 닫기버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 라디오버튼 이벤트
        private void RB_ATTAXGUBN1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RB_ATTAXGUBN1.Checked == true)
            {
                this.RB_ATTAXGUBN2.Checked = false;
            }
        }

        private void RB_ATTAXGUBN2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RB_ATTAXGUBN2.Checked == true)
            {
                this.RB_ATTAXGUBN1.Checked = false;
            }
        }
        #endregion

        #region Description : 매입합계표 마지막 생성 년월 가지고 오기
        private void Set_Start_yymm()
        {
            if (this.RB_ATTAXGUBN1.Checked == true)
            {
                this.DbConnector.CommandClear(); // AVTAXOPTF
                this.DbConnector.Attach("TY_P_AC_42H17403",
                                        this.TXT01_S2YEAR.GetValue().ToString(),  // 기준년도 
                                      //this.DTP01_ELXYYMM.GetValue().ToString().Substring(0, 4),
                                        this.CBO01_VNGUBUN.GetValue().ToString()     // 사업장(1본점, 2지점)
                                        );
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    if (this.RB_ATTAXGUBN1.Checked == true)
                    {
                        this.DTP01_ELXYYMM.SetValue(dt.Rows[0]["O1EDYYMM"].ToString().Substring(0, 6));  //생성 (시작월을 재 세팅함)
                    }
                }
            }

        }
        #endregion


    }
}