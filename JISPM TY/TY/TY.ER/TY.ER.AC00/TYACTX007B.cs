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
    /// 부가세 부속서류 생성 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2013.12.03 15:13
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_3CQ4A870 : 부가세 부속서류 생성(SP)
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  INQOPTION : 조회구분
    ///  INQOPTION2 : 조회구분
    ///  VNGUBUN : 구분
    ///  S1CHK1 : 세금계산서 합계표
    ///  S1CHK2 : 매출계산서 합계표
    ///  S1CHK3 : 건물등감가상각자산취득명세서
    ///  S1CHK4 : 신용카드매출전표등수취명세서
    ///  S1CHK5 : 공제받지못할 매입세액명세서
    ///  S1CHK6 : 전자세금계산서 발급세액세서
    ///  S1CHK7 : 수출실적명세서
    ///  S1CHK8 : 영세율첨부서류명세서
    ///  S1CHK9 : 사업장별부가가치세과세표준및납부세액명세서
    ///  ELXYYMM : 기준년도
    /// </summary>
    public partial class TYACTX007B : TYBase
    {
        public TYACTX007B()
        {
            InitializeComponent();
        }

        #region Description : Page_Load
        private void TYACTX007B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            //this.CKB01_S1CHK1.Visible = false;
            //this.CKB01_S1CHK2.Visible = false;
            //this.CKB01_S1CHK3.Visible = false;
            //this.CKB01_S1CHK4.Visible = false;
            //this.CKB01_S1CHK5.Visible = false;
            //this.CKB01_S1CHK7.Visible = false;
            //this.CKB01_S1CHK8.Visible = false;
            //this.CKB01_S1CHK9.Visible = false;

            this.SetStartingFocus(this.DTP01_ELXYYMM);

            UP_Cookie_Load();

            this.RB_ATTAXGUBN1.Checked = true;
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
                                    this.DTP01_ELXYYMM.GetValue().ToString(),
                                    this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)   // 확정구분(1.예정, 2.확정)
                                    );
            DataTable dt = this.DbConnector.ExecuteDataTable();

            sVNGUBUN = dt.Rows[0]["AOVNEDCD"].ToString();

            this.TXT01_S1CHK1.SetValue(""); // s세금계산서
            this.TXT01_S1CHK2.SetValue(""); // s매출계산서
            this.TXT01_S1CHK3.SetValue(""); // s건물취득
            this.TXT01_S1CHK4.SetValue(""); // s신용카드
            this.TXT01_S1CHK5.SetValue(""); // s공제받지못할
            //this.TXT01_S1CHK6.SetValue(""); // s전자세금계산서
            this.TXT01_S1CHK7.SetValue(""); // s수출실적명세
            this.TXT01_S1CHK8.SetValue(""); // s영세율첨부

            string s세금 = this.CKB01_S1CHK1.GetValue().ToString();  // s세금계산서
            string s매출 = this.CKB01_S1CHK2.GetValue().ToString();  // s매출계산서
            string s건물 = this.CKB01_S1CHK3.GetValue().ToString();  // s건물취득
            string s신용 = this.CKB01_S1CHK4.GetValue().ToString();  // s신용카드
            string s공제 = this.CKB01_S1CHK5.GetValue().ToString();  // s공제받지못할
            //string s전자 = this.CKB01_S1CHK6.GetValue().ToString();  // s전자세금계산서
            string s수출 = this.CKB01_S1CHK7.GetValue().ToString();  // s수출실적명세
            string s영세 = this.CKB01_S1CHK8.GetValue().ToString();  // s영세율첨부

            string s전체처리구분 = string.Empty;

            if (this.RB_ATTAXGUBN1.Checked == true)
            {
                s전체처리구분 = "Y"; //생성
            }
            else
            {
                s전체처리구분 = "N"; //취소
            };

            if (s세금 == "Y") { iCCNT = iCCNT + 1; };
            if (s매출 == "Y") { iCCNT = iCCNT + 1; };
            if (s건물 == "Y") { iCCNT = iCCNT + 1; };
            if (s신용 == "Y") { iCCNT = iCCNT + 1; };
            if (s공제 == "Y") { iCCNT = iCCNT + 1; };
            //if (s전자 == "Y") { iCCNT = iCCNT + 1; };
            if (s수출 == "Y") { iCCNT = iCCNT + 1; };
            if (s영세 == "Y") { iCCNT = iCCNT + 1; };

            string sYEAR = string.Empty;
            string s시작년월 = string.Empty;
            string s종료년월 = string.Empty;

            sYEAR = this.DTP01_ELXYYMM.GetValue().ToString().Substring(0, 4);

            if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1) == "1") // 신고구분(1기, 2기)
            {
                if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2) == "1") // 확정구분(1.예정, 2.확정)
                {
                    s시작년월 = sYEAR + "01";
                    s종료년월 = sYEAR + "03";
                }
                else
                {
                    s시작년월 = sYEAR + "04";
                    s종료년월 = sYEAR + "06";
                }
            }
            else
            {
                if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2) == "1")
                {
                    s시작년월 = sYEAR + "07";
                    s종료년월 = sYEAR + "09";
                }
                else
                {
                    s시작년월 = sYEAR + "10";
                    s종료년월 = sYEAR + "12";
                }
            }

            if (s전체처리구분 == "Y")
            {
                //IN_ATCRGB 처리구분
                //          1.세금계산서
                //          2.계산서 
                //          3.건물감가상각취득명세서
                //          4.신용카드매출전표등 수취명세서(갑)
                //          5.공제받지못할 매입세액 명세서
                //          6.수출실적명세서 
                //          7.영세율 첨부서류 제출명세서

                #region Description : 1.세금계산서 생성 처리 (SP) --> 매입,매출 한번에 생성
                if (s세금 == "Y")
                {
                    // 세금계산서 - 생성 (SP)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        ("TY_P_AC_3CQ4A870",
                        "1",  // 1.세금계산서
                        sYEAR,
                        this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                        sVNGUBUN,                                    // 사업장코드
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2), // 확정구분(1.예정, 2.확정)
                        "", // 계산서구분(1.매입, 2.매출)
                        s시작년월,
                        s종료년월,
                        sOUTMSG.ToString()
                        );

                    sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                    if (sOUTMSG.ToString().Substring(0, 2) == "OK")
                    {
                        this.TXT01_S1CHK1.Text = sOUTMSG;
                    }
                    else
                    {
                        this.TXT01_S1CHK1.Text = "ER-처리중 ERROR 발생";
                    }

                }
                #endregion

                #region Description : 2.계산서(매출만) 생성 처리 (SP) --> 매입계산서 별도 생성
                if (s매출 == "Y")
                {
                    // 계산서(매출) - 생성 (SP)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        ("TY_P_AC_3CQ4A870",
                        "2",  // 2.계산서
                        sYEAR,
                        this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                        sVNGUBUN,                                    // 사업장코드
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2), // 확정구분(1.예정, 2.확정)
                        "2", // 계산서구분(1.매입, 2.매출)
                        s시작년월,
                        s종료년월,
                        sOUTMSG.ToString()
                        );

                    sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                    if (sOUTMSG.ToString().Substring(0, 2) == "OK")
                    {
                        this.TXT01_S1CHK2.Text = sOUTMSG;
                    }
                    else
                    {
                        this.TXT01_S1CHK2.Text = "ER-처리중 ERROR 발생";
                    }

                }
                #endregion

                #region Description : 3.건물감가상각취득명세서(매입만) 생성 처리 (SP)
                if (s건물 == "Y")
                {
                    // 건물감가상각취득명세서(매입) - 생성 (SP)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        ("TY_P_AC_3CQ4A870",
                        "3",  // 3.건물감가상각취득명세서
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
                        this.TXT01_S1CHK3.Text = sOUTMSG;
                    }
                    else
                    {
                        this.TXT01_S1CHK3.Text = "ER-처리중 ERROR 발생";
                    }

                }
                #endregion

                #region Description : 4.신용카드매출전표등 수취명세서(갑)(매입만) 생성 처리 (SP)
                if (s신용 == "Y")
                {
                    // 신용카드매출전표등 수취명세서(갑)(매입) - 생성 (SP)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        ("TY_P_AC_3CQ4A870",
                        "4",  // 4.신용카드매출전표등 수취명세서(갑)
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
                        this.TXT01_S1CHK4.Text = sOUTMSG;
                    }
                    else
                    {
                        this.TXT01_S1CHK4.Text = "ER-처리중 ERROR 발생";
                    }

                }
                #endregion

                #region Description : 5.공제받지못할 매입세액 명세서(매입만) 생성 처리 (SP)
                if (s공제 == "Y")
                {
                    // 공제받지못할 매입세액 명세서(매입) - 생성 (SP)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        ("TY_P_AC_3CQ4A870",
                        "5",  // 5.공제받지못할 매입세액 명세서
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
                        this.TXT01_S1CHK5.Text = sOUTMSG;
                    }
                    else
                    {
                        this.TXT01_S1CHK5.Text = "ER-처리중 ERROR 발생";
                    }

                }
                #endregion

                #region Description : 6.수출실적명세서(매출만) 생성 처리 (SP)
                if (s수출 == "Y")
                {
                    // 6.수출실적명세서(매출) - 생성 (SP)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        ("TY_P_AC_3CQ4A870",
                        "6",  // 6.수출실적명세서
                        sYEAR,
                        this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                        sVNGUBUN,                                    // 사업장코드
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2), // 확정구분(1.예정, 2.확정)
                        "2", // 계산서구분(1.매입, 2.매출)
                        s시작년월,
                        s종료년월,
                        sOUTMSG.ToString()
                        );

                    sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                    if (sOUTMSG.ToString().Substring(0, 2) == "OK")
                    {
                        this.TXT01_S1CHK7.Text = sOUTMSG;
                    }
                    else
                    {
                        this.TXT01_S1CHK7.Text = "ER-처리중 ERROR 발생";
                    }

                }
                #endregion

                #region Description : 7.영세율 첨부서류 제출명세서(매출만) 생성 처리 (SP)
                if (s영세 == "Y")
                {
                    // 7.영세율 첨부서류 제출명세서(매출) - 생성 (SP)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        ("TY_P_AC_3CQ4A870",
                        "7",  //  7.영세율 첨부서류 제출명세서
                        sYEAR,
                        this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                        sVNGUBUN,                                    // 사업장코드
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2), // 확정구분(1.예정, 2.확정)
                        "2", // 계산서구분(1.매입, 2.매출)
                        s시작년월,
                        s종료년월,
                        sOUTMSG.ToString()
                        );

                    sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                    if (sOUTMSG.ToString().Substring(0, 2) == "OK")
                    {
                        this.TXT01_S1CHK8.Text = sOUTMSG;
                    }
                    else
                    {
                        this.TXT01_S1CHK8.Text = "ER-처리중 ERROR 발생";
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
                    this.ShowCustomMessage("처리할구분을 선택하세요.", "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }

            }
            else // 삭제
            {
                #region Description : 1.세금계산서 생성 처리 (SP) --> 매입,매출 한번에 생성
                if (s세금 == "Y")
                {
                    // 세금계산서 - 생성 (SP)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        ("TY_P_AC_3CRAY881",
                        "1",  // 1.세금계산서
                        sYEAR,
                        this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                        sVNGUBUN,                                    // 사업장코드
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2), // 확정구분(1.예정, 2.확정)
                        "", // 계산서구분(1.매입, 2.매출)
                        s시작년월,
                        s종료년월,
                        sOUTMSG.ToString()
                        );

                    sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                    if (sOUTMSG.ToString().Substring(0, 2) == "OK")
                    {
                        this.TXT01_S1CHK1.Text = sOUTMSG;
                    }
                    else
                    {
                        this.TXT01_S1CHK1.Text = "ER-처리중 ERROR 발생";
                    }

                }
                #endregion

                #region Description : 2.계산서(매출만) 생성 처리 (SP) --> 매입계산서 별도 생성
                if (s매출 == "Y")
                {
                    // 계산서(매출) - 생성 (SP)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        ("TY_P_AC_3CRAY881",
                        "2",  // 2.계산서
                        sYEAR,
                        this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                        sVNGUBUN,                                    // 사업장코드
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2), // 확정구분(1.예정, 2.확정)
                        "2", // 계산서구분(1.매입, 2.매출)
                        s시작년월,
                        s종료년월,
                        sOUTMSG.ToString()
                        );

                    sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                    if (sOUTMSG.ToString().Substring(0, 2) == "OK")
                    {
                        this.TXT01_S1CHK2.Text = sOUTMSG;
                    }
                    else
                    {
                        this.TXT01_S1CHK2.Text = "ER-처리중 ERROR 발생";
                    }

                }
                #endregion

                #region Description : 3.건물감가상각취득명세서(매입만) 생성 처리 (SP)
                if (s건물 == "Y")
                {
                    // 건물감가상각취득명세서(매입) - 생성 (SP)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        ("TY_P_AC_3CRAY881",
                        "3",  // 3.건물감가상각취득명세서
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
                        this.TXT01_S1CHK3.Text = sOUTMSG;
                    }
                    else
                    {
                        this.TXT01_S1CHK3.Text = "ER-처리중 ERROR 발생";
                    }

                }
                #endregion

                #region Description : 4.신용카드매출전표등 수취명세서(갑)(매입만) 생성 처리 (SP)
                if (s신용 == "Y")
                {
                    // 신용카드매출전표등 수취명세서(갑)(매입) - 생성 (SP)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        ("TY_P_AC_3CRAY881",
                        "4",  // 4.신용카드매출전표등 수취명세서(갑)
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
                        this.TXT01_S1CHK4.Text = sOUTMSG;
                    }
                    else
                    {
                        this.TXT01_S1CHK4.Text = "ER-처리중 ERROR 발생";
                    }

                }
                #endregion

                #region Description : 5.공제받지못할 매입세액 명세서(매입만) 생성 처리 (SP)
                if (s공제 == "Y")
                {
                    // 공제받지못할 매입세액 명세서(매입) - 생성 (SP)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        ("TY_P_AC_3CRAY881",
                        "5",  // 5.공제받지못할 매입세액 명세서
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
                        this.TXT01_S1CHK5.Text = sOUTMSG;
                    }
                    else
                    {
                        this.TXT01_S1CHK5.Text = "ER-처리중 ERROR 발생";
                    }

                }
                #endregion

                #region Description : 6.수출실적명세서(매출만) 생성 처리 (SP)
                if (s수출 == "Y")
                {
                    // 6.수출실적명세서(매출) - 생성 (SP)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        ("TY_P_AC_3CRAY881",
                        "6",  // 6.수출실적명세서
                        sYEAR,
                        this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                        sVNGUBUN,                                    // 사업장코드
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2), // 확정구분(1.예정, 2.확정)
                        "2", // 계산서구분(1.매입, 2.매출)
                        s시작년월,
                        s종료년월,
                        sOUTMSG.ToString()
                        );

                    sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                    if (sOUTMSG.ToString().Substring(0, 2) == "OK")
                    {
                        this.TXT01_S1CHK7.Text = sOUTMSG;
                    }
                    else
                    {
                        this.TXT01_S1CHK7.Text = "ER-처리중 ERROR 발생";
                    }

                }
                #endregion

                #region Description : 7.영세율 첨부서류 제출명세서(매출만) 생성 처리 (SP)
                if (s영세 == "Y")
                {
                    // 7.영세율 첨부서류 제출명세서(매출) - 생성 (SP)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        ("TY_P_AC_3CRAY881",
                        "7",  //  7.영세율 첨부서류 제출명세서
                        sYEAR,
                        this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                        sVNGUBUN,                                    // 사업장코드
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2), // 확정구분(1.예정, 2.확정)
                        "2", // 계산서구분(1.매입, 2.매출)
                        s시작년월,
                        s종료년월,
                        sOUTMSG.ToString()
                        );

                    sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                    if (sOUTMSG.ToString().Substring(0, 2) == "OK")
                    {
                        this.TXT01_S1CHK8.Text = sOUTMSG;
                    }
                    else
                    {
                        this.TXT01_S1CHK8.Text = "ER-처리중 ERROR 발생";
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
                    this.ShowCustomMessage("처리할구분을 선택하세요.", "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
            }

            UP_Cookie_Save();
        }
        #endregion

        #region Description : 처리 CHECK
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int iCCNT = 0;

            this.TXT01_S1CHK1.SetValue(""); // s세금계산서
            this.TXT01_S1CHK2.SetValue(""); // s매출계산서
            this.TXT01_S1CHK3.SetValue(""); // s건물취득
            this.TXT01_S1CHK4.SetValue(""); // s신용카드
            this.TXT01_S1CHK5.SetValue(""); // s공제받지못할
            //this.TXT01_S1CHK6.SetValue(""); // s전자세금계산서
            this.TXT01_S1CHK7.SetValue(""); // s수출실적명세
            this.TXT01_S1CHK8.SetValue(""); // s영세율첨부

             // 1.부가세 OPTION TABLE 존재 유무 확인
            this.DbConnector.CommandClear(); // AVOPTIONMF
            this.DbConnector.Attach("TY_P_AC_3CQ5Y873", 
                                    this.DTP01_ELXYYMM.GetValue().ToString(),
                                    this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)  // 확정구분(1.예정, 2.확정)
                                    );
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_3CR9M876"); 
                e.Successed = false;
                this.SetFocus(this.CBO01_VNGUBUN);
                return;
            }

            string s세금 = this.CKB01_S1CHK1.GetValue().ToString();  // s세금계산서
            string s매출 = this.CKB01_S1CHK2.GetValue().ToString();  // s매출계산서
            string s건물 = this.CKB01_S1CHK3.GetValue().ToString();  // s건물취득
            string s신용 = this.CKB01_S1CHK4.GetValue().ToString();  // s신용카드
            string s공제 = this.CKB01_S1CHK5.GetValue().ToString();  // s공제받지못할
            //string s전자 = this.CKB01_S1CHK6.GetValue().ToString();  // s전자세금계산서
            string s수출 = this.CKB01_S1CHK7.GetValue().ToString();  // s수출실적명세
            string s영세 = this.CKB01_S1CHK8.GetValue().ToString();  // s영세율첨부

            if (s세금 == "Y") { iCCNT = iCCNT + 1; };
            if (s매출 == "Y") { iCCNT = iCCNT + 1; };
            if (s건물 == "Y") { iCCNT = iCCNT + 1; };
            if (s신용 == "Y") { iCCNT = iCCNT + 1; };
            if (s공제 == "Y") { iCCNT = iCCNT + 1; };
            //if (s전자 == "Y") { iCCNT = iCCNT + 1; };
            if (s수출 == "Y") { iCCNT = iCCNT + 1; };
            if (s영세 == "Y") { iCCNT = iCCNT + 1; };

            if (iCCNT == 0)
            {
                this.ShowCustomMessage("처리할구분을 선택하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                this.SetFocus(this.DTP01_ELXYYMM);
                return;
            }

            // 마감 체크 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_42B8L317",
                this.DTP01_ELXYYMM.GetValue().ToString(),
                this.CBO01_VNGUBUN.GetValue().ToString(),                   // 사업장(1본점, 2지점)
                getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),   // 신고구분(1기, 2기)
                getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)   // 확정구분(1.예정, 2.확정)
                );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                this.ShowCustomMessage("신고서 마감 완료 되었습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                this.SetFocus(this.DTP01_ELXYYMM);
                return;
            }
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

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            UP_Cookie_Save();

            this.Close();
        }
        #endregion

        #region Description : 쿠키 불러오기
        private void UP_Cookie_Load()
        {
            if (TYCookie.Chk == "Cookie")
            {
                this.DTP01_ELXYYMM.SetValue(TYCookie.Year);
                this.CBO01_VNGUBUN.SetValue(TYCookie.Branch);
                //this.CBO01_INQOPTION.SetValue(TYCookie.RptGubn);
                this.CBO01_INQOPTION.SetValue(TYCookie.Confgb);
            }
            else
            {
                this.DTP01_ELXYYMM.SetValue(DateTime.Now.ToString("yyyy"));
            }
        }
        #endregion

        #region Description : 쿠키 저장
        private void UP_Cookie_Save()
        {
            TYCookie.Save(this.DTP01_ELXYYMM.GetValue().ToString(), this.CBO01_VNGUBUN.GetValue().ToString(), this.CBO01_INQOPTION.GetValue().ToString());
        }
        #endregion

        #region Description : 체크박스 이벤트
        private void CKB01_S1CHK12_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_S1CHK12.Checked == true)
            {
                CKB01_S1CHK1.Checked = true;
                CKB01_S1CHK2.Checked = true;
                CKB01_S1CHK3.Checked = true;
                CKB01_S1CHK4.Checked = true;
                CKB01_S1CHK5.Checked = true;
                CKB01_S1CHK7.Checked = true;
                CKB01_S1CHK8.Checked = true;
            }
            else if (CKB01_S1CHK1.Checked == true && CKB01_S1CHK2.Checked == true && CKB01_S1CHK3.Checked == true && CKB01_S1CHK4.Checked == true && 
                     CKB01_S1CHK5.Checked == true && CKB01_S1CHK7.Checked == true && CKB01_S1CHK8.Checked == true )
            {
                CKB01_S1CHK1.Checked = false;
                CKB01_S1CHK2.Checked = false;
                CKB01_S1CHK3.Checked = false;
                CKB01_S1CHK4.Checked = false;
                CKB01_S1CHK5.Checked = false;
                CKB01_S1CHK7.Checked = false;
                CKB01_S1CHK8.Checked = false;
            }
        }

        private void CKB01_S1CHK1_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_S1CHK1.Checked == false)
            {
                this.CKB01_S1CHK12.Checked = false;
            }
        }

        private void CKB01_S1CHK3_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_S1CHK3.Checked == false)
            {
                this.CKB01_S1CHK12.Checked = false;
            }
        }

        private void CKB01_S1CHK4_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_S1CHK4.Checked == false)
            {
                this.CKB01_S1CHK12.Checked = false;
            }
        }

        private void CKB01_S1CHK5_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_S1CHK5.Checked == false)
            {
                this.CKB01_S1CHK12.Checked = false;
            }
        }

        private void CKB01_S1CHK7_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_S1CHK7.Checked == false)
            {
                this.CKB01_S1CHK12.Checked = false;
            }
        }

        private void CKB01_S1CHK8_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_S1CHK8.Checked == false)
            {
                this.CKB01_S1CHK12.Checked = false;
            }

        }

        private void CKB01_S1CHK2_CheckedChanged(object sender, EventArgs e)
        {
            if (CKB01_S1CHK2.Checked == false)
            {
                this.CKB01_S1CHK12.Checked = false;
            }
        }
        #endregion
    }
}
