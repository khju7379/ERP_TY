using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.IO;
using System.Text;

namespace TY.ER.AC00
{
    /// <summary>
    /// 지급명세서 전산매체 생성 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2014.12.11 17:57
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_49IF2933 : 원천세 연말정산 세부내역 생성 조회
    ///  TY_P_AC_49NH1052 : 원천세 연말정산 세부내역 등록
    ///  TY_P_AC_49O9H055 : 원천세 연말정산 세부내역 삭제
    ///  TY_P_AC_4AEAA177 : 원천세 일용항운노조 명부 조회(자동)
    ///  TY_P_AC_4CBHH729 : 원천세 명세서 일용근로소득 생성
    ///  TY_P_AC_4CBHK732 : 원천세 명세서 일용근로소득 삭제
    ///  TY_P_AC_4CMDT923 : 지급명세서 제출자 인적사항 조회(전산매체)
    ///  TY_P_AC_4CMI2931 : 명세서 일용자 지급자별 집계 레코드(전산매체)
    ///  TY_P_AC_4CNDB942 : 명세서 일용자 지급자별 세부 레코드(전산매체)
    ///  TY_P_AC_4COEN963 : 명세서 이자.배당소득 집계 레코드(전산매체)
    ///  TY_P_AC_4CQAN982 : 명세서 이자.배당소득 세부 레코드(전산매체)
    ///  TY_P_AC_4CTB3987 : 명세서 기타소득 집계 레코드(전산매체)
    ///  TY_P_AC_4CTB4988 : 명세서 기타소득 상세 레코드(전산매체)
    ///  TY_P_AC_51JEI177 : 명세서 사업소득 상세 레코드(전산매체)
    ///  TY_P_AC_51JEK178 : 명세서 사업소득 집계 레코드(전산매체)
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  VNGUBUN : 구분
    ///  WABRANCH : 지점구분
    ///  S1CHK12 : 전체
    ///  W1CHK1 : 근로소득
    ///  W1CHK2 : 퇴직소득
    ///  W1CHK3 : 사업소득
    ///  W1CHK4 : 기타소득
    ///  W1CHK5 : 이자소득
    ///  W1CHK6 : 배당소득
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    ///  WREYYMM : 귀속년월
    /// </summary>
    public partial class TYACTP012B : TYBase
    {
        public TYACTP012B()
        {
            InitializeComponent();
        }
     
        #region Description : Page_Load
        private void TYACTP012B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);
            this.DTP01_WREYYMM.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_GSTYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            this.DTP01_GEDYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.SetStartingFocus(this.DTP01_GSTYYMM);

            this.TXT01_W1CHK1.ReadOnly = true;
            this.TXT01_W1CHK2.ReadOnly = true;
            this.TXT01_W1CHK3.ReadOnly = true;
            this.TXT01_W1CHK4.ReadOnly = true;
            this.TXT01_W1CHK5.ReadOnly = true;
            this.TXT01_W1CHK6.ReadOnly = true;
            this.TXT01_W1CHK7.ReadOnly = true;

            this.CKB01_W1CHK2.Visible = false;
            this.CKB01_W1CHK6.Visible = false;
            this.CKB01_W1CHK7.Visible = false;

        }
        #endregion

        #region Description : 처리버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sBRANCH   = string.Empty;

            string s일용근로 = this.CKB01_W1CHK1.GetValue().ToString();  // s일용근로
            string s퇴직소득 = this.CKB01_W1CHK2.GetValue().ToString();  // s퇴직소득
            string s사업소득 = this.CKB01_W1CHK3.GetValue().ToString();  // s사업소득
            string s기타소득 = this.CKB01_W1CHK4.GetValue().ToString();  // s기타소득
            string s이자배당 = this.CKB01_W1CHK5.GetValue().ToString();  // s이자배당
            string s의료비   = this.CKB01_W1CHK6.GetValue().ToString();    // s의료비
            string s기부금   = this.CKB01_W1CHK7.GetValue().ToString();    // s기부금


            this.TXT01_W1CHK1.TextAlign = HorizontalAlignment.Center;
            this.TXT01_W1CHK2.TextAlign = HorizontalAlignment.Center;
            this.TXT01_W1CHK3.TextAlign = HorizontalAlignment.Center;
            this.TXT01_W1CHK4.TextAlign = HorizontalAlignment.Center;
            this.TXT01_W1CHK5.TextAlign = HorizontalAlignment.Center;
            this.TXT01_W1CHK6.TextAlign = HorizontalAlignment.Center;
            this.TXT01_W1CHK7.TextAlign = HorizontalAlignment.Center;

            sBRANCH = "1";

            // 일용근로
            if (sBRANCH.ToString() == "1" && s일용근로 == "Y")
            {
                // 일용근로 레코드
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_4CNDB942",
                     sBRANCH.ToString(),
                     this.DTP01_WREYYMM.GetValue().ToString().Replace("-", "").Substring(0, 8),
                     this.DTP01_GSTYYMM.GetValue().ToString().Replace("-", "").Substring(0, 6),
                     this.DTP01_GEDYYMM.GetValue().ToString().Replace("-", "").Substring(0, 6),
                     TYUserInfo.SecureKey, "Y"
                    );

                DataTable dt_bdata = this.DbConnector.ExecuteDataTable();
                if (dt_bdata.Rows.Count > 0)
                {
                    UP_Daily_Labor_File_Create();
                    this.TXT01_W1CHK1.SetValue("○");
                }
                else
                {
                    this.TXT01_W1CHK1.SetValue("");
                }
            }

            // 이자배당
            if (s이자배당 == "Y")
            {
                // 이자․배당소득 집계
                int icnt = 0;
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_4COEN963", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", sBRANCH.ToString(), this.DTP01_GSTYYMM.GetValue().ToString(), this.DTP01_GEDYYMM.GetValue().ToString()); // WIINCOMEF 
                DataTable dt_nae = this.DbConnector.ExecuteDataTable();
                if (dt_nae.Rows.Count > 0)
                {
                    icnt = Convert.ToInt16(Get_Numeric(SetDefaultValue(dt_nae.Rows[0]["DT11"].ToString())));
                }
                if (icnt > 0)
                {
                    UP_Interest_Dividends_File_Create();
                    this.TXT01_W1CHK5.SetValue("○");
                }
                else
                {
                    this.TXT01_W1CHK5.SetValue("");
                }
            }

            // 기타소득
            if (s기타소득 == "Y")
            {
                int icnt = 0;
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_4CTB3987", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", sBRANCH.ToString(), this.DTP01_GSTYYMM.GetValue().ToString(), this.DTP01_GEDYYMM.GetValue().ToString()); // WIINCOMEF 
                DataTable dt_nae = this.DbConnector.ExecuteDataTable();
                if (dt_nae.Rows.Count > 0)
                {
                    icnt = Convert.ToInt16(Get_Numeric(SetDefaultValue(dt_nae.Rows[0]["B08"].ToString())));
                }
                if (icnt > 0)
                {
                    UP_Other_Income_File_Create();
                    this.TXT01_W1CHK4.SetValue("○");
                }
                else
                {
                    this.TXT01_W1CHK4.SetValue("");
                }
            }

            if (s사업소득 == "Y")
            {
                int icnt = 0;
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_51JEK178", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", sBRANCH.ToString(), this.DTP01_GSTYYMM.GetValue().ToString(), this.DTP01_GEDYYMM.GetValue().ToString()); // WIINCOMEF 
                DataTable dt_nae = this.DbConnector.ExecuteDataTable();
                if (dt_nae.Rows.Count > 0)
                {
                    icnt = Convert.ToInt16(Get_Numeric(SetDefaultValue(dt_nae.Rows[0]["B07"].ToString())));
                }
                if (icnt > 0)
                {
                    UP_Business_Income_File_Create();
                    this.TXT01_W1CHK3.SetValue("○");
                }
                else
                {
                    this.TXT01_W1CHK3.SetValue("");
                }
            }

            #region Description :  의료비 (신인사 개발완료후 개발 진행)
            if (s의료비 == "Y")
            {
                // 신인사 개발완료후 개발 진행
                //int icnt = 0;
                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach("TY_P_AC_51JEK178", this.CBO01_WABRANCH.GetValue().ToString(), this.DTP01_GSTYYMM.GetValue().ToString(), this.DTP01_GEDYYMM.GetValue().ToString()); // WIINCOMEF 
                //DataTable dt_nae = this.DbConnector.ExecuteDataTable();
                //if (dt_nae.Rows.Count > 0)
                //{
                //    icnt = Convert.ToInt16(Get_Numeric(SetDefaultValue(dt_nae.Rows[0]["B07"].ToString())));
                //}
                //if (icnt > 0)
                //{
                //    UP_Medical_Expenses_File_Create();
                //    this.TXT01_W1CHK6.SetValue("○");
                //}
                //else
                //{
                //    this.TXT01_W1CHK6.SetValue("");
                //}
            } 
            #endregion

            #region Description :  기부금 (신인사 개발완료후 개발 진행)
            if (s기부금 == "Y")
            {
                // 신인사 개발완료후 개발 진행
                //int icnt = 0;
                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach("TY_P_AC_51JEK178", this.CBO01_WABRANCH.GetValue().ToString(), this.DTP01_GSTYYMM.GetValue().ToString(), this.DTP01_GEDYYMM.GetValue().ToString()); // WIINCOMEF 
                //DataTable dt_nae = this.DbConnector.ExecuteDataTable();
                //if (dt_nae.Rows.Count > 0)
                //{
                //    icnt = Convert.ToInt16(Get_Numeric(SetDefaultValue(dt_nae.Rows[0]["B07"].ToString())));
                //}
                //if (icnt > 0)
                //{
                //    UP_Contribution_File_Create();
                //    this.TXT01_W1CHK7.SetValue("○");
                //}
                //else
                //{
                //    this.TXT01_W1CHK7.SetValue("");
                //}
            }
            #endregion

            this.ShowMessage("TY_M_GB_26E30875");

        }
        #endregion

        #region Description : 처리 CHECK
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            this.TXT01_W1CHK1.SetValue(""); // s일용근로
            this.TXT01_W1CHK2.SetValue(""); // s퇴직소득
            this.TXT01_W1CHK3.SetValue(""); // s사업소득
            this.TXT01_W1CHK4.SetValue(""); // s기타소득
            this.TXT01_W1CHK5.SetValue(""); // s이자배당
            this.TXT01_W1CHK6.SetValue(""); // s의료비
            this.TXT01_W1CHK7.SetValue(""); // s기부금

            string sBRANCH = string.Empty;

            sBRANCH = "1";

            if ( Convert.ToInt16(this.DTP01_GSTYYMM.GetValue().ToString().Replace("-", "").Substring(0, 4)) != Convert.ToInt16(this.DTP01_GEDYYMM.GetValue().ToString().Replace("-", "").Substring(0, 4)) )
            {
                this.ShowCustomMessage("기간년을 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                this.SetFocus(this.DTP01_GEDYYMM);
                return;
            }

            if (this.CKB01_W1CHK1.GetValue().ToString() != "Y" && this.CKB01_W1CHK2.GetValue().ToString() != "Y" &&
                this.CKB01_W1CHK3.GetValue().ToString() != "Y" && this.CKB01_W1CHK4.GetValue().ToString() != "Y" &&
                this.CKB01_W1CHK5.GetValue().ToString() != "Y" && this.CKB01_W1CHK6.GetValue().ToString() != "Y" && 
                this.CKB01_W1CHK7.GetValue().ToString() != "Y")
            {
                this.ShowCustomMessage("생성할 구분을 선택하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                this.SetFocus(this.DTP01_GSTYYMM);
                return;
            }

            // 퇴직소득 미작업
            if (this.CKB01_W1CHK2.GetValue().ToString() == "Y" )
            {
                this.ShowCustomMessage("작업 불가", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                this.SetFocus(this.CKB01_W1CHK2);
                return;
            }

            // 의료비 미작업
            if (this.CKB01_W1CHK6.GetValue().ToString() == "Y" )
            {
                this.ShowCustomMessage("작업 불가", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                this.SetFocus(this.CKB01_W1CHK6);
                return;
            }

            // 기부금 미작업
            if (this.CKB01_W1CHK7.GetValue().ToString() == "Y")
            {
                this.ShowCustomMessage("작업 불가", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                this.SetFocus(this.CKB01_W1CHK7);
                return;
            }

            // 제출자 인적사항 조회 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_4CMDT923", sBRANCH.ToString());  // AVSUBMITMF 
            if (this.DbConnector.ExecuteDataTable().Rows.Count == 0)
            {
                this.ShowCustomMessage("제출자 인적사항 미존재", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                this.SetFocus(this.DTP01_GSTYYMM);
                return;
            }

            // 일용근로 (본점만)
            if (sBRANCH.ToString() == "1" && this.CKB01_W1CHK1.GetValue().ToString() == "Y")
            {
                // 소득자 레코드
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_4CNDB942",
                     sBRANCH.ToString(),
                     this.DTP01_WREYYMM.GetValue().ToString().Replace("-", "").Substring(0, 8),
                     this.DTP01_GSTYYMM.GetValue().ToString().Replace("-", "").Substring(0, 6),
                     this.DTP01_GEDYYMM.GetValue().ToString().Replace("-", "").Substring(0, 6),
                     TYUserInfo.SecureKey, "Y"
                    );

                if (this.DbConnector.ExecuteDataTable().Rows.Count == 0)
                {
                    this.ShowCustomMessage("일용근로자 자료가 없습니다. 기간을 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    this.SetFocus(this.DTP01_GSTYYMM);
                    return;
                }
            }

            // 이자․배당소득 집계
            if (this.CKB01_W1CHK5.GetValue().ToString() == "Y")
            {
                int icnt = 0;
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_4COEN963", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", sBRANCH.ToString(), this.DTP01_GSTYYMM.GetValue().ToString(), this.DTP01_GEDYYMM.GetValue().ToString()); // WIINCOMEF 
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count == 0)
                {
                    this.ShowCustomMessage("이자․배당소득 자료가 없습니다. 기간을 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    this.SetFocus(this.DTP01_GSTYYMM);
                    return;
                }

                if (dt.Rows.Count > 0)
                {
                    icnt = Convert.ToInt16(Get_Numeric(SetDefaultValue(dt.Rows[0]["DT11"].ToString())));
                    if (icnt <= 0)
                    {
                        this.ShowCustomMessage("이자․배당소득 자료가 없습니다. 기간을 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        this.SetFocus(this.DTP01_GSTYYMM);
                        return;
                    }
                }
            }

            // 기타소득 집계
            if (this.CKB01_W1CHK4.GetValue().ToString() == "Y")
            {
                int icnt = 0;
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_4CTB3987", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", sBRANCH.ToString(), this.DTP01_GSTYYMM.GetValue().ToString(), this.DTP01_GEDYYMM.GetValue().ToString()); // WIINCOMEF 
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    icnt = Convert.ToInt16(Get_Numeric(SetDefaultValue(dt.Rows[0]["B08"].ToString())));
                    if (icnt <= 0)
                    {
                        this.ShowCustomMessage("기타소득 자료가 없습니다. 기간을 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        this.SetFocus(this.DTP01_GSTYYMM);
                        return;
                    }
                }
            }

            // 사업소득 집계
            if (this.CKB01_W1CHK3.GetValue().ToString() == "Y")
            {
                int icnt = 0;
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_51JEK178", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", sBRANCH.ToString(), this.DTP01_GSTYYMM.GetValue().ToString(), this.DTP01_GEDYYMM.GetValue().ToString()); // WIINCOMEF 
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    icnt = Convert.ToInt16(Get_Numeric(SetDefaultValue(dt.Rows[0]["B07"].ToString())));
                    if (icnt <= 0)
                    {
                        this.ShowCustomMessage("사업소득 자료가 없습니다. 기간을 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        this.SetFocus(this.DTP01_GSTYYMM);
                        return;
                    }
                }
            }

        }
        #endregion


        #region Description : 일용근로 명세서 File 처리 로직
        private void UP_Daily_Labor_File_Create()
        {
            string sFile_Path = "C:\\eosdata\\";
            if (System.IO.Directory.Exists(sFile_Path) == false)
            {
                System.IO.Directory.CreateDirectory(sFile_Path);
            }

            // 구분(1.본점, 2.지점)에 따라 파일이름을 각각 설정한다.
            string sFile_Name = string.Empty;
            string sPath = string.Empty;
            
            sFile_Name = "C:\\eosdata\\" + "I6108110.449";

            if (File.Exists(sFile_Name))
            {
                File.Delete(sFile_Name);
            }

            //if (this.CBO01_WABRANCH.GetValue().ToString() == "1")
            //{
            //    sFile_Name = "C:\\eosdata\\" + "I6108110.449";

            //    if (File.Exists(sFile_Name))
            //    {
            //        File.Delete(sFile_Name);
            //    }
            //}
            //else
            //{
            //    sFile_Name = "C:\\eosdata\\" + "I1058516.181";

            //    if (File.Exists(sFile_Name))
            //    {
            //        File.Delete(sFile_Name);
            //    }
            //}

            StreamWriter sw = new StreamWriter(sFile_Name, false, Encoding.Default);

            string sRecord = string.Empty;
            // 서식코드 A : A레코드[제출자(대리인)레코드] (HEAD) -- [길이 : 240]
            // A:28
            UP_Daily_Labor_HEAD_A(sw);
            // 서식코드 B : B레코드[지급자별 집계 레코드] -- [길이 : 240]
            // B:28
            UP_Daily_Labor_TOTA_B(sw);
            // 서식코드 C : C레코드[소득자 레코드] -- [길이 : 240]
            // C:28
            UP_Daily_Labor_Details_C(sw);

            sw.Close();
        }
        #endregion
        #region Description : 일용근로 - A레코드[제출자(대리인)레코드
        private void UP_Daily_Labor_HEAD_A(StreamWriter sw)
        {
            string sYEAR = string.Empty;
            string sData = string.Empty;
            string sStrTemp = string.Empty;
            string sFill = string.Empty;

            string sSAUPNO = string.Empty; // 사업자등록번호
            string sSANGHO = string.Empty; // 상호명
            string sNAMENM = string.Empty; // 대표자이름
            string sCORPNO = string.Empty; // 법인번호
            string sUPTAE = string.Empty; // 업태
            string sEVENT = string.Empty; // 종목
            string sTELNUM = string.Empty; // 전화번호
            string sVNADDRS = string.Empty; // 사업장주소
            string sTAXAREA = string.Empty; // 관할세무소
            string sBUSTYPE = string.Empty; // 업종코드
            string sBUNGI = string.Empty; //제출대상기간코드

            string sBRANCH = string.Empty;

            sBRANCH = "1";


            // 폐업에 의한 수시 제출분 : “5”
            if (this.DTP01_GEDYYMM.GetValue().ToString().Substring(4, 2) == "03")
            {
                sBUNGI = "1";
            }
            else if (this.DTP01_GEDYYMM.GetValue().ToString().Substring(4, 2) == "06")
            {
                sBUNGI = "2";
            }
            else if (this.DTP01_GEDYYMM.GetValue().ToString().Substring(4, 2) == "09")
            {
                sBUNGI = "3";
            }
            else if (this.DTP01_GEDYYMM.GetValue().ToString().Substring(4, 2) == "12")
            {
                sBUNGI = "4";
            }


            sYEAR = this.DTP01_WREYYMM.GetValue().ToString().Substring(0, 4); // 체출년도

            struct_HO201 HDO201 = new struct_HO201();



            // 제출자 인적사항 조회 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_4CMDT923",  // AVSUBMITMF 
                sBRANCH.ToString()
                );

            DataTable dt_tae = this.DbConnector.ExecuteDataTable();

            if (dt_tae.Rows.Count > 0)
            {
                sSAUPNO = dt_tae.Rows[0]["ASMSAUPNO"].ToString(); // 사업자등록번호
                sSANGHO = dt_tae.Rows[0]["ASMSANGHO"].ToString(); // 상호명
                sNAMENM = dt_tae.Rows[0]["ASMNAMENM"].ToString(); // 대표자이름
                sCORPNO = dt_tae.Rows[0]["ASMCORPNO"].ToString(); // 법인번호
                sUPTAE = dt_tae.Rows[0]["ASMUPTAE"].ToString(); // 업태
                sEVENT = dt_tae.Rows[0]["ASMEVENT"].ToString(); // 종목
                sTELNUM = Up_Telnum_Convert(dt_tae.Rows[0]["ASMTELNUM"].ToString().Trim()); // 전화번호
                sVNADDRS = dt_tae.Rows[0]["ASMVNADDRS"].ToString(); // 사업장주소
                sTAXAREA = dt_tae.Rows[0]["ASMTAXAREA"].ToString(); // 관할세무소
                sBUSTYPE = dt_tae.Rows[0]["ASMBUSTYPE"].ToString(); // 업종코드
            }

            HDO201.HO201_DT01 = "A";                  //  1자리 : 자료구분 ==> “A”
            HDO201.HO201_DT02 = "28";                 //  2자리 : 서식코드 ==>  “28”
            HDO201.HO201_DT03 = sTAXAREA.PadRight(3); //  3자리 : 제출자 소재지관할 세무서 코드
            HDO201.HO201_DT04 = this.DTP01_WREYYMM.GetString().ToString().Replace("-", ""); //  8자리 : 자료를 세무서에 제출하는 연월일을 수록
            HDO201.HO201_DT05 = "2";// sFill.PadRight(1);    //  1자리  제출자(대리인) :
            HDO201.HO201_DT06 = sFill.PadRight(6);    //  6자리  세무대리인 관리번호

            HDO201.HO201_DT07 = "tyc2921";            // 20자리 사용자 ID (본점-TYC2921 , 본점-TYC2922) (재정리 됨)
            HDO201.HO201_DT08 = "1074";               //  4자리 세무프로그램코드

            //if (this.CBO01_WABRANCH.GetValue().ToString() == "1")
            //{
            //    HDO201.HO201_DT07 = "tyc2921";        // 20자리 사용자 ID (본점-TYC2921 , 본점-TYC2922) (재정리 됨)
            //    HDO201.HO201_DT08 = "1074";               //  4자리 세무프로그램코드
            //}
            //else
            //{
            //    HDO201.HO201_DT07 = "tyc2922";        // 20자리 사용자 ID (본점-TYC2921 , 본점-TYC2922) (재정리 됨)
            //    HDO201.HO201_DT08 = "1075";               //  4자리 세무프로그램코드
            //}
            //HDO201.HO201_DT08 = "9000";               //  4자리 세무프로그램코드
            
            HDO201.HO201_DT09 = sSAUPNO.PadRight(10); // 10자리 : 납세자ID --> 사업자등록번호(사업자번호 생성규칙과 무세적 체크를 함) 
            HDO201.HO201_DT10 = sSANGHO;              // 40자리 : 상호명 (재정리 됨) 
            HDO201.HO201_DT11 = "회계팀";             // 30자리 : 담당자 부서 (재정리 됨) 
            HDO201.HO201_DT12 = "황성환";             // 30자리 : 담당자 성명 (재정리 됨) 
            HDO201.HO201_DT13 = "0522283314";         // 15자리 : 담당자 전화번호 (재정리 됨) 

            HDO201.HO201_DT14 = "00001";               //  5자리 : 신고의무자수 원천징수의무자(B레코드) 수
            HDO201.HO201_DT15 = sBUNGI.PadRight(01);   //  1자리 : 신고구분 : ‘제출대상기간코드
            HDO201.HO201_DT16 = sFill.PadRight(64);    // 64자리 공란

            // 레코드 세팅 작업(자리수)
            sData = HDO201.HO201_DT01;
            sData += HDO201.HO201_DT02;
            sData += HDO201.HO201_DT03;
            sData += HDO201.HO201_DT04;
            sData += HDO201.HO201_DT05;
            sData += HDO201.HO201_DT06;
            sStrTemp = HDO201.HO201_DT07.Trim(); // 7 사용자ID :  20자리 
            sStrTemp += new String(Convert.ToChar(" "), (20 - Encoding.Default.GetByteCount(HDO201.HO201_DT07.Trim())));
            sData += sStrTemp;

            sData += HDO201.HO201_DT08;
            sData += HDO201.HO201_DT09;
            sStrTemp = HDO201.HO201_DT10.Trim(); // 10 상호(법인명) : 40자리
            sStrTemp += new String(Convert.ToChar(" "), (40 - Encoding.Default.GetByteCount(HDO201.HO201_DT10.Trim())));
            sData += sStrTemp;

            sStrTemp = HDO201.HO201_DT11.Trim(); // 11 담당자 부서 : 30자리
            sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(HDO201.HO201_DT11.Trim())));
            sData += sStrTemp;

            sStrTemp = HDO201.HO201_DT12.Trim(); // 12 담당자 성명 : 30자리
            sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(HDO201.HO201_DT12.Trim())));
            sData += sStrTemp;

            sStrTemp = HDO201.HO201_DT13.Trim(); // 13 담당자 전화번호 : 15자리
            sStrTemp += new String(Convert.ToChar(" "), (15 - Encoding.Default.GetByteCount(HDO201.HO201_DT13.Trim())));
            sData += sStrTemp;

            sData += HDO201.HO201_DT14;
            sData += HDO201.HO201_DT15;
            sData += HDO201.HO201_DT16;

            sw.WriteLine(sData);
        }
        #endregion
        #region Description : 일용근로 - B레코드[지급자별 집계 레코드]
        private void UP_Daily_Labor_TOTA_B(StreamWriter sw)
        {
            string sYEAR = string.Empty;
            string sData = string.Empty;
            string sStrTemp = string.Empty;
            string sFill = string.Empty;

            string sSAUPNO  = string.Empty; // 사업자등록번호
            string sSANGHO  = string.Empty; // 상호명
            string sNAMENM  = string.Empty; // 대표자이름
            string sCORPNO  = string.Empty; // 법인번호
            string sUPTAE   = string.Empty; // 업태
            string sEVENT   = string.Empty; // 종목
            string sTELNUM  = string.Empty; // 전화번호
            string sVNADDRS = string.Empty; // 사업장주소
            string sTAXAREA = string.Empty; // 관할세무소
            string sBUSTYPE = string.Empty; // 업종코드
            string sBUNGI   = string.Empty; //제출대상기간코드

            string sBRANCH  = string.Empty;

            sBRANCH = "1";

            if (this.DTP01_GEDYYMM.GetValue().ToString().Substring(4, 2) == "03")
            {
                sBUNGI = "1";
            }
            else if (this.DTP01_GEDYYMM.GetValue().ToString().Substring(4, 2) == "06")
            {
                sBUNGI = "2";
            }
            else if (this.DTP01_GEDYYMM.GetValue().ToString().Substring(4, 2) == "09")
            {
                sBUNGI = "3";
            }
            else if (this.DTP01_GEDYYMM.GetValue().ToString().Substring(4, 2) == "12")
            {
                sBUNGI = "4";
            }


            sYEAR = this.DTP01_WREYYMM.GetValue().ToString().Substring(0, 4);

            struct_BO201 BDO201 = new struct_BO201();

            // 제출자 인적사항 조회 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_4CMDT923",  // AVSUBMITMF 
                sBRANCH.ToString()
                );

            DataTable dt_tae = this.DbConnector.ExecuteDataTable();

            if (dt_tae.Rows.Count > 0)
            {
                sSAUPNO = dt_tae.Rows[0]["ASMSAUPNO"].ToString(); // 사업자등록번호
                sSANGHO = dt_tae.Rows[0]["ASMSANGHO"].ToString(); // 상호명
                sNAMENM = dt_tae.Rows[0]["ASMNAMENM"].ToString(); // 대표자이름
                sCORPNO = dt_tae.Rows[0]["ASMCORPNO"].ToString(); // 법인번호
                sUPTAE = dt_tae.Rows[0]["ASMUPTAE"].ToString(); // 업태
                sEVENT = dt_tae.Rows[0]["ASMEVENT"].ToString(); // 종목
                sTELNUM = dt_tae.Rows[0]["ASMTELNUM"].ToString().Trim().Replace("-",""); // 전화번호
                sVNADDRS = dt_tae.Rows[0]["ASMVNADDRS"].ToString(); // 사업장주소
                sTAXAREA = dt_tae.Rows[0]["ASMTAXAREA"].ToString(); // 관할세무소
                sBUSTYPE = dt_tae.Rows[0]["ASMBUSTYPE"].ToString(); // 업종코드
            }

            BDO201.BO201_DT01 = "B";                  //  1자리 : 자료구분 ==> “B”
            BDO201.BO201_DT02 = "28";                 //  2자리 : 서식코드 ==>  “28”
            BDO201.BO201_DT03 = sTAXAREA.PadRight(3); //  3자리 : 제출자 소재지관할 세무서 코드
            BDO201.BO201_DT04 = "000001";             //  6자리 : 일련번호
            BDO201.BO201_DT05 = sSAUPNO.PadRight(10); // 10자리 : 납세자ID --> 사업자등록번호(사업자번호 생성규칙과 무세적 체크를 함) 
            BDO201.BO201_DT06 = sSANGHO;              // 40자리 : 상호명 (재정리 됨) 
            BDO201.BO201_DT07 = sNAMENM;              // 30자리 : 대표자(성명) (재정리 됨) 
            BDO201.BO201_DT08 = sCORPNO.PadRight(13); // 13자리 : 주민(법인)등록번호

            BDO201.BO201_DT09 = sTELNUM.PadRight(15); // 15자리 : 전화번호 
            BDO201.BO201_DT10 = sFill.PadRight(40);   // 40자리 : E-MAIL 

            // 지급자별 집계 레코드 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_4CMI2931",  //  지급자별 집계
                 sBRANCH.ToString(),
                 this.DTP01_WREYYMM.GetValue().ToString().Replace("-","").Substring(0,8),
                 this.DTP01_GSTYYMM.GetValue().ToString().Replace("-","").Substring(0,6),
                 this.DTP01_GEDYYMM.GetValue().ToString().Replace("-","").Substring(0,6),
                 TYUserInfo.SecureKey, "Y"
                );

            DataTable dt_bdata = this.DbConnector.ExecuteDataTable();

            if (dt_tae.Rows.Count > 0)
            {
                BDO201.BO201_DT11 = dt_bdata.Rows[0]["G1REVYYMM"].ToString();   //  4자리 :귀속년도 
                BDO201.BO201_DT12 = dt_bdata.Rows[0]["BUNGE"].ToString();       //  1자리 : 신고구분 : ‘제출대상기간코드
                BDO201.BO201_DT13 = string.Format("{0:D6}", Convert.ToInt64(dt_bdata.Rows[0]["CNTGB"].ToString()));    //  6자리 : 일용근로자수
                BDO201.BO201_DT14 = string.Format("{0:D6}", Convert.ToInt64(dt_bdata.Rows[0]["RECOCNT"].ToString()));  //  6자리 : 제출자료건수
                BDO201.BO201_DT15 = UP_Minus_Conv_Fill(dt_bdata.Rows[0]["G1PAYAMT"].ToString().Trim(), 15); // 15자리 : 총지급액계
                BDO201.BO201_DT16 = UP_Minus_Conv_Fill(dt_bdata.Rows[0]["G1NONTAX"].ToString().Trim(), 15); // 15자리 : 비과세소득합계
                BDO201.BO201_DT17 = UP_Minus_Conv_Fill(dt_bdata.Rows[0]["G1INCTAX"].ToString().Trim(), 15); // 15자리 : 원천징수세액합계_소득세
                BDO201.BO201_DT18 = UP_Minus_Conv_Fill(dt_bdata.Rows[0]["G1ARETAX"].ToString().Trim(), 15); // 15자리 : 원천징수세액합계_지방소득세
            }

            BDO201.BO201_DT19 = sFill.PadRight(03);    //  3자리 : 공란

            // 레코드 세팅 작업(자리수)
            sData = BDO201.BO201_DT01;
            sData += BDO201.BO201_DT02;
            sData += BDO201.BO201_DT03;
            sData += BDO201.BO201_DT04;
            sData += BDO201.BO201_DT05;

            sStrTemp = BDO201.BO201_DT06.Trim(); // 상호명 : 40자리
            sStrTemp += new String(Convert.ToChar(" "), (40 - Encoding.Default.GetByteCount(BDO201.BO201_DT06.Trim())));
            sData += sStrTemp;

            sStrTemp = BDO201.BO201_DT07.Trim(); // 대표자(성명) : 30자리
            sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(BDO201.BO201_DT07.Trim())));
            sData += sStrTemp;

            sData += BDO201.BO201_DT08;
            sData += BDO201.BO201_DT09;
            sData += BDO201.BO201_DT10;
            sData += BDO201.BO201_DT11;
            sData += BDO201.BO201_DT12;  

            sData += BDO201.BO201_DT13;  // 일용근로자수
            sData += BDO201.BO201_DT14;  // 제출자료건수
            sData += BDO201.BO201_DT15;  // 총지급액계
            sData += BDO201.BO201_DT16;  // 비과세소득합계
            sData += BDO201.BO201_DT17;  // 원천징수세액합계_소득세
            sData += BDO201.BO201_DT18;  // 원천징수세액합계_지방소득세

            sData += BDO201.BO201_DT19; // 공란

            sw.WriteLine(sData);
        }
        #endregion
        #region Description : 일용근로 - C레코드[소득자 레코드]
        private void UP_Daily_Labor_Details_C(StreamWriter sw)
        {
            string sYEAR    = string.Empty;
            string sData    = string.Empty;
            string sStrTemp = string.Empty;
            string sFill    = string.Empty;

            string sBRANCH  = string.Empty;

            sBRANCH = "1";

            int iSEQ = 0;

            string sSAUPNO = string.Empty; // 사업자등록번호
            string sSANGHO = string.Empty; // 상호명
            string sNAMENM = string.Empty; // 대표자이름
            string sCORPNO = string.Empty; ; // 법인번호
            string sUPTAE = string.Empty; // 업태
            string sEVENT = string.Empty; // 종목
            string sTELNUM = string.Empty; // 전화번호
            string sVNADDRS = string.Empty; // 사업장주소
            string sTAXAREA = string.Empty; // 관할세무소
            string sBUSTYPE = string.Empty; // 업종코드
            string sBUNGI = string.Empty; //제출대상기간코드

            sYEAR = this.DTP01_WREYYMM.GetValue().ToString().Substring(0, 4);

            struct_CO201 CDO201 = new struct_CO201();

            // 제출자 인적사항 조회 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_4CMDT923",  // AVSUBMITMF 
                sBRANCH.ToString()
                );

            DataTable dt_tae = this.DbConnector.ExecuteDataTable();

            if (dt_tae.Rows.Count > 0)
            {
                sSAUPNO = dt_tae.Rows[0]["ASMSAUPNO"].ToString(); // 사업자등록번호
                sSANGHO = dt_tae.Rows[0]["ASMSANGHO"].ToString(); // 상호명
                sNAMENM = dt_tae.Rows[0]["ASMNAMENM"].ToString(); // 대표자이름
                sCORPNO = dt_tae.Rows[0]["ASMCORPNO"].ToString(); // 법인번호
                sUPTAE = dt_tae.Rows[0]["ASMUPTAE"].ToString(); // 업태
                sEVENT = dt_tae.Rows[0]["ASMEVENT"].ToString(); // 종목
                sTELNUM = Up_Telnum_Convert(dt_tae.Rows[0]["ASMTELNUM"].ToString().Trim()); // 전화번호
                sVNADDRS = dt_tae.Rows[0]["ASMVNADDRS"].ToString(); // 사업장주소
                sTAXAREA = dt_tae.Rows[0]["ASMTAXAREA"].ToString(); // 관할세무소
                sBUSTYPE = dt_tae.Rows[0]["ASMBUSTYPE"].ToString(); // 업종코드
            }

            // 소득자 레코드
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_4CNDB942",
                 sBRANCH.ToString(),
                 this.DTP01_WREYYMM.GetValue().ToString().Replace("-", "").Substring(0, 8),
                 this.DTP01_GSTYYMM.GetValue().ToString().Replace("-", "").Substring(0, 6),
                 this.DTP01_GEDYYMM.GetValue().ToString().Replace("-", "").Substring(0, 6),
                 TYUserInfo.SecureKey, "Y"
                );

            DataTable dt_bdata = this.DbConnector.ExecuteDataTable();

            if (dt_bdata.Rows.Count > 0)
            {
                for (int i = 0; i < dt_bdata.Rows.Count; i++)
                {
                    iSEQ = iSEQ + 1;

                    CDO201.CO201_DT01 = "C";                  //  1자리 : 자료구분 ==> “C”
                    CDO201.CO201_DT02 = "28";                 //  2자리 : 서식코드 ==>  “28”
                    CDO201.CO201_DT03 = sTAXAREA.PadRight(3); //  3자리 : 제출자 소재지관할 세무서 코드
                    CDO201.CO201_DT04 = string.Format("{0:D6}", iSEQ);  // 6자리 : 일련번호
                    CDO201.CO201_DT05 = sSAUPNO.PadRight(10); // 10자리 : 납세자ID --> 사업자등록번호(사업자번호 생성규칙과 무세적 체크를 함) 

                    // 【소득자】
                    CDO201.CO201_DT06 = dt_bdata.Rows[i]["JUMIN"].ToString();     // 13자리 : 주민등록번호 
                    CDO201.CO201_DT07 = dt_bdata.Rows[i]["NAME"].ToString();      // 30자리 : 소득자 성명 (재정리 됨) 
                    CDO201.CO201_DT08 = dt_bdata.Rows[i]["WNATIVEGB"].ToString(); //  1자리 : 내․외국인구분코드
                    CDO201.CO201_DT09 = dt_bdata.Rows[i]["TELNUM"].ToString().PadRight(15);    // 15자리 : 전화번호 
                    // 【소득지급명세】
                    CDO201.CO201_DT10 = Set_Fill2(dt_bdata.Rows[i]["WRMM"].ToString());      //  2자리 : 지급월 
                    CDO201.CO201_DT11 = Set_Fill2(dt_bdata.Rows[i]["GUMM"].ToString());      //  2자리 : 근무월 
                    CDO201.CO201_DT12 = Set_Fill2(dt_bdata.Rows[i]["WDAYWORK"].ToString());  //  2자리 : 근무일수
                    // 【소득지급명세】
                    CDO201.CO201_DT13 = UP_Minus_Conv_Fill(dt_bdata.Rows[i]["PAYAMOUNT"].ToString().Trim(), 13); // 13자리 : 총지급액계
                    CDO201.CO201_DT14 = UP_Minus_Conv_Fill(dt_bdata.Rows[i]["NONTAX"].ToString().Trim(), 13); // 13자리 : 비과세소득합계
                    CDO201.CO201_DT15 = UP_Minus_Conv_Fill(dt_bdata.Rows[i]["INCTAX"].ToString().Trim(), 13); // 13자리 : 원천징수세액합계_소득세
                    CDO201.CO201_DT16 = UP_Minus_Conv_Fill(dt_bdata.Rows[i]["ARETAX"].ToString().Trim(), 13); // 13자리 : 원천징수세액합계_지방소득세
                    CDO201.CO201_DT17 = sFill.PadRight(101);    //  101자리 : 공란

                    // 레코드 세팅 작업(자리수)
                    sData = CDO201.CO201_DT01;
                    sData += CDO201.CO201_DT02;
                    sData += CDO201.CO201_DT03;
                    sData += CDO201.CO201_DT04;
                    sData += CDO201.CO201_DT05;
                    sData += CDO201.CO201_DT06;
                    sStrTemp = CDO201.CO201_DT07.Trim(); // 소득자 성명 : 30자리
                    sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(CDO201.CO201_DT07.Trim())));
                    sData += sStrTemp;

                    sData += CDO201.CO201_DT08;
                    sData += CDO201.CO201_DT09;
                    sData += CDO201.CO201_DT10;
                    sData += CDO201.CO201_DT11;
                    sData += CDO201.CO201_DT12;
                    sData += CDO201.CO201_DT13;  // 일용근로자수
                    sData += CDO201.CO201_DT14;  // 제출자료건수
                    sData += CDO201.CO201_DT15;  // 총지급액계
                    sData += CDO201.CO201_DT16;  // 비과세소득합계
                    sData += CDO201.CO201_DT17;  // 공란

                    sw.WriteLine(sData);
                }
            }
        }
        #endregion

        #region Description : 이자․배당(금융)소득 명세서 File 처리 로직
        private void UP_Interest_Dividends_File_Create()
        {
            string sFile_Path = "C:\\eosdata\\";
            if (System.IO.Directory.Exists(sFile_Path) == false)
            {
                System.IO.Directory.CreateDirectory(sFile_Path);
            }

            // 구분(1.본점, 2.지점)에 따라 파일이름을 각각 설정한다.
            string sFile_Name = string.Empty;
            string sPath = string.Empty;

            sFile_Name = "C:\\eosdata\\" + "B6108110.449";

            if (File.Exists(sFile_Name))
            {
                File.Delete(sFile_Name);
            }

            //if (this.CBO01_WABRANCH.GetValue().ToString() == "1")
            //{
            //    sFile_Name = "C:\\eosdata\\" + "B6108110.449";

            //    if (File.Exists(sFile_Name))
            //    {
            //        File.Delete(sFile_Name);
            //    }
            //}
            //else
            //{
            //    sFile_Name = "C:\\eosdata\\" + "B1058516.181";

            //    if (File.Exists(sFile_Name))
            //    {
            //        File.Delete(sFile_Name);
            //    }
            //}

            StreamWriter sw = new StreamWriter(sFile_Name, false, Encoding.Default);
            string sRecord = string.Empty;

            // 서식코드 A : A레코드(본점(제출자) 레코드) (HEAD) -- [길이 : 480]
            // A:40
            UP_Interest_Dividends_HEAD_A(sw);
            // 서식코드 B : B레코드 (지점(원천징수의무자) 레코드) -- [길이 : 480]
            // B:40
            UP_Interest_Dividends_TOTA_B(sw);
            // 서식코드 C : C레코드 (지급명세 레코드) -- [길이 : 480]
            // C:40
            UP_Interest_Dividends_C(sw);

            sw.Close();
        }
        #endregion
        #region Description : 이자․배당(금융)소득 - A레코드 (제출자) 레코드
        private void UP_Interest_Dividends_HEAD_A(StreamWriter sw)
        {
            string sYEAR = string.Empty;
            string sData = string.Empty;
            string sStrTemp = string.Empty;
            string sFill = string.Empty;

            string sSAUPNO  = string.Empty; // 사업자등록번호
            string sSANGHO  = string.Empty; // 상호명
            string sNAMENM  = string.Empty; // 대표자이름
            string sCORPNO  = string.Empty; ; // 법인번호
            string sUPTAE   = string.Empty; // 업태
            string sEVENT   = string.Empty; // 종목
            string sTELNUM  = string.Empty; // 전화번호
            string sVNADDRS = string.Empty; // 사업장주소
            string sTAXAREA = string.Empty; // 관할세무소
            string sBUSTYPE = string.Empty; // 업종코드
            string sBUNGI   = string.Empty; //제출대상기간코드

            string sDT11 = string.Empty; 
            string sDT12 = string.Empty;
            string sDT13 = string.Empty;
            string sDT14 = string.Empty;

            string sBRANCH = string.Empty;
            sBRANCH = "1";

            sYEAR = this.DTP01_WREYYMM.GetValue().ToString().Substring(0, 4); // 체출년도

            struct_AO101 ADO101 = new struct_AO101();

            // 제출자 인적사항 조회 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_4CMDT923",  // AVSUBMITMF 
                sBRANCH.ToString()
                );

            DataTable dt_tae = this.DbConnector.ExecuteDataTable();

            if (dt_tae.Rows.Count > 0)
            {
                sSAUPNO = dt_tae.Rows[0]["ASMSAUPNO"].ToString(); // 사업자등록번호
                sSANGHO = dt_tae.Rows[0]["ASMSANGHO"].ToString(); // 상호명
                sNAMENM = dt_tae.Rows[0]["ASMNAMENM"].ToString(); // 대표자이름
                sCORPNO = dt_tae.Rows[0]["ASMCORPNO"].ToString(); // 법인번호
                sUPTAE = dt_tae.Rows[0]["ASMUPTAE"].ToString(); // 업태
                sEVENT = dt_tae.Rows[0]["ASMEVENT"].ToString(); // 종목
                sTELNUM = Up_Telnum_Convert(dt_tae.Rows[0]["ASMTELNUM"].ToString().Trim()); // 전화번호
                sVNADDRS = dt_tae.Rows[0]["ASMVNADDRS"].ToString(); // 사업장주소
                sTAXAREA = dt_tae.Rows[0]["ASMTAXAREA"].ToString(); // 관할세무소
                sBUSTYPE = dt_tae.Rows[0]["ASMBUSTYPE"].ToString(); // 업종코드
            }

            // 이자․배당소득 집계
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_4COEN963", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", sBRANCH.ToString(), this.DTP01_GSTYYMM.GetValue().ToString(), this.DTP01_GEDYYMM.GetValue().ToString()); // WIINCOMEF 

            DataTable dt_nae = this.DbConnector.ExecuteDataTable();

            if (dt_nae.Rows.Count > 0)
            {
                sDT11 = dt_nae.Rows[0]["DT11"].ToString();
                sDT12 = dt_nae.Rows[0]["DT12"].ToString();
                sDT13 = dt_nae.Rows[0]["DT13"].ToString();
                sDT14 = dt_nae.Rows[0]["DT14"].ToString();
            }

            ADO101.AO101_DT01 = "A";                  //  1자리 : 자료구분 ==> “A”
            ADO101.AO101_DT02 = "40";                 //  2자리 : 서식코드 ==>  "40"
            ADO101.AO101_DT03 = sTAXAREA.PadRight(3); //  3자리 : 제출자 소재지관할 세무서 코드
            ADO101.AO101_DT04 = this.DTP01_WREYYMM.GetString().ToString().Replace("-", "").Substring(0,6); //  6자리 : 자료를 세무서에 제출하는 연월일을 수록

            // 【본점기본사항】
            ADO101.AO101_DT05 = sSAUPNO.PadRight(13); //  13자리 : 본점(제출자)의 사업자 등록번호
            ADO101.AO101_DT06 = sFill.PadRight(22);   //  22자리 : 공란
            ADO101.AO101_DT07 = sSANGHO;              //  40자리 : 상호명 (재정리 됨) 

            ADO101.AO101_DT08 = "tyc2921";            // 20자리 사용자 ID (본점-TYC2921 , 본점-TYC2922) (재정리 됨)
            ADO101.AO101_DT09 = "1074";               //  4자리 세무프로그램코드

            //if (this.CBO01_WABRANCH.GetValue().ToString() == "1")
            //{
            //    ADO101.AO101_DT08 = "tyc2921";       // 20자리 사용자 ID (본점-TYC2921 , 본점-TYC2922) (재정리 됨)
            //    ADO101.AO101_DT09 = "1074";               //  4자리 세무프로그램코드
            //}
            //else
            //{
            //    ADO101.AO101_DT08 = "tyc2922";       // 20자리 사용자 ID (본점-TYC2921 , 본점-TYC2922) (재정리 됨)
            //    ADO101.AO101_DT09 = "1075";               //  4자리 세무프로그램코드
            //}
            //ADO101.AO101_DT09 = "9000";               //  4자리 세무프로그램코드
            

            //【제출기관 담당자】
            ADO101.AO101_DT10 = "회계팀";             // 20자리 : 담당자 부서 (재정리 됨) 
            ADO101.AO101_DT11 = "황성환";             // 20자리 : 담당자 성명 (재정리 됨) 
            ADO101.AO101_DT12 = "052-228-3314";       // 20자리 : 담당자 전화번호 (재정리 됨) 

            //【제출기관 통보내역】
            ADO101.AO101_DT13 = "00001";               //  5자리  : 원천징수의무자수 --> 지점수 (B레코드수)

            ADO101.AO101_DT14 = string.Format("{0:D10}", Convert.ToInt64(sDT11));    //  10자리 : 총 제출건수 --> 모든 C레코드 건수
            ADO101.AO101_DT15 = string.Format("{0:D16}", Convert.ToInt64(sDT12));    //  16자리 : 소득금액합계 -> 모든 C레코드 소득금액의 합계금액
            ADO101.AO101_DT16 = string.Format("{0:D15}", Convert.ToInt64(sDT13));    //  15자리 : 소득세액합계 -> 모든 C레코드 소득세액의 합계금액
            ADO101.AO101_DT17 = string.Format("{0:D15}", Convert.ToInt64(sDT14));    //  15자리 : 법인세액합계 -> 모든 C레코드 법인세액의 합계금액

            //ADO101.AO101_DT18 = string.Format("{0:D10}", 0);  //  10자리 : 2014 귀속당초 제출건수
            //ADO101.AO101_DT19 = string.Format("{0:D16}", 0);  //  16자리 : 2014 귀속당초 소득금액 합계
            ADO101.AO101_DT18 = string.Format("{0:D10}", Convert.ToInt64(sDT11));    //  10자리 : 2014 귀속당초 제출건수
            ADO101.AO101_DT19 = string.Format("{0:D16}", Convert.ToInt64(sDT12));    //  16자리 : 2014 귀속당초 소득금액 합계
            ADO101.AO101_DT20 = string.Format("{0:D10}", 0);  //  10자리 : 2014 귀속삭제 제출건수
            ADO101.AO101_DT21 = string.Format("{0:D16}", 0);  //  16자리 : 2014 귀속삭제 소득금액 합계
            ADO101.AO101_DT22 = string.Format("{0:D10}", 0);  //  10자리 : 2014 귀속수정 제출건수
            ADO101.AO101_DT23 = string.Format("{0:D16}", 0);  //  16자리 : 2014 귀속수정 소득금액 합계
            ADO101.AO101_DT24 = sFill.PadRight(170);          // 170자리 : 공란

            // 레코드 세팅 작업(자리수)
            sData = ADO101.AO101_DT01;
            sData += ADO101.AO101_DT02;
            sData += ADO101.AO101_DT03;
            sData += ADO101.AO101_DT04;
            sData += ADO101.AO101_DT05;
            sData += ADO101.AO101_DT06;
            sStrTemp = ADO101.AO101_DT07.Trim(); // 7 상호(법인명) : 40자리 
            sStrTemp += new String(Convert.ToChar(" "), (40 - Encoding.Default.GetByteCount(ADO101.AO101_DT07.Trim())));
            sData += sStrTemp;

            sStrTemp = ADO101.AO101_DT08.Trim(); // 8 홈택스 ID : 20 
            sStrTemp += new String(Convert.ToChar(" "), (20 - Encoding.Default.GetByteCount(ADO101.AO101_DT08.Trim())));
            sData += sStrTemp;

            sData += ADO101.AO101_DT09;
            sStrTemp = ADO101.AO101_DT10.Trim(); // 10 담당부서 : 20자리
            sStrTemp += new String(Convert.ToChar(" "), (20 - Encoding.Default.GetByteCount(ADO101.AO101_DT10.Trim())));
            sData += sStrTemp;

            sStrTemp = ADO101.AO101_DT11.Trim(); // 11 담당자성명 : 20자리
            sStrTemp += new String(Convert.ToChar(" "), (20 - Encoding.Default.GetByteCount(ADO101.AO101_DT11.Trim())));
            sData += sStrTemp;

            sStrTemp = ADO101.AO101_DT12.Trim(); // 12 담당자연락처 : 20자리
            sStrTemp += new String(Convert.ToChar(" "), (20 - Encoding.Default.GetByteCount(ADO101.AO101_DT12.Trim())));
            sData += sStrTemp;

            sData += ADO101.AO101_DT13;
            sData += ADO101.AO101_DT14;
            sData += ADO101.AO101_DT15;
            sData += ADO101.AO101_DT16;
            sData += ADO101.AO101_DT17;
            sData += ADO101.AO101_DT18;
            sData += ADO101.AO101_DT19;
            sData += ADO101.AO101_DT20;
            sData += ADO101.AO101_DT21;
            sData += ADO101.AO101_DT22;
            sData += ADO101.AO101_DT23;
            sData += ADO101.AO101_DT24; // 공란

            sw.WriteLine(sData);
        }
        #endregion
        #region Description : 이자․배당(금융)소득 - B레코드【원천징수의무자 별 집계 레코드】
        private void UP_Interest_Dividends_TOTA_B(StreamWriter sw)
        {
            string sYEAR = string.Empty;
            string sData = string.Empty;
            string sStrTemp = string.Empty;
            string sFill = string.Empty;

            string sSAUPNO  = string.Empty; // 사업자등록번호
            string sSANGHO  = string.Empty; // 상호명
            string sNAMENM  = string.Empty; // 대표자이름
            string sCORPNO  = string.Empty; // 법인번호
            string sUPTAE   = string.Empty; // 업태
            string sEVENT   = string.Empty; // 종목
            string sTELNUM  = string.Empty; // 전화번호
            string sVNADDRS = string.Empty; // 사업장주소
            string sTAXAREA = string.Empty; // 관할세무소
            string sBUSTYPE = string.Empty; // 업종코드
            string sBUNGI   = string.Empty; //제출대상기간코드

            string sDT11 = string.Empty;
            string sDT12 = string.Empty;
            string sDT13 = string.Empty;
            string sDT14 = string.Empty;

            string sYCHK = string.Empty;

            string sBRANCH = string.Empty;
            sBRANCH = "1";

            int iMM = 1;
            for (int i = Convert.ToInt16(this.DTP01_GSTYYMM.GetValue().ToString().Replace("-", "").Substring(4, 2)); i < Convert.ToInt16(this.DTP01_GEDYYMM.GetValue().ToString().Replace("-", "").Substring(4, 2)); i++)
            {
                iMM = iMM + 1;
            }
            
            if (iMM == 12)
            { sYCHK = "1" ;
            } else{ sYCHK = "3";}

            
            sYEAR = this.DTP01_WREYYMM.GetValue().ToString().Substring(0, 4);

            struct_BO101 BDO101 = new struct_BO101();

            // 제출자 인적사항 조회 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_4CMDT923",  // AVSUBMITMF 
                sBRANCH.ToString()
                );

            DataTable dt_tae = this.DbConnector.ExecuteDataTable();

            if (dt_tae.Rows.Count > 0)
            {
                sSAUPNO = dt_tae.Rows[0]["ASMSAUPNO"].ToString(); // 사업자등록번호
                sSANGHO = dt_tae.Rows[0]["ASMSANGHO"].ToString(); // 상호명
                sNAMENM = dt_tae.Rows[0]["ASMNAMENM"].ToString(); // 대표자이름
                sCORPNO = dt_tae.Rows[0]["ASMCORPNO"].ToString(); // 법인번호
                sUPTAE = dt_tae.Rows[0]["ASMUPTAE"].ToString(); // 업태
                sEVENT = dt_tae.Rows[0]["ASMEVENT"].ToString(); // 종목
                sTELNUM = Up_Telnum_Convert(dt_tae.Rows[0]["ASMTELNUM"].ToString().Trim()); // 전화번호
                sVNADDRS = dt_tae.Rows[0]["ASMVNADDRS"].ToString(); // 사업장주소
                sTAXAREA = dt_tae.Rows[0]["ASMTAXAREA"].ToString(); // 관할세무소
                sBUSTYPE = dt_tae.Rows[0]["ASMBUSTYPE"].ToString(); // 업종코드
            }

            BDO101.BO101_DT01 = "B";                  //  1자리 : 자료구분 ==> “B”
            BDO101.BO101_DT02 = "40";                 //  2자리 : 서식코드 ==>  "40"
            BDO101.BO101_DT03 = sTAXAREA.PadRight(3); //  3자리 : 제출자 소재지관할 세무서 코드
            BDO101.BO101_DT04 = this.DTP01_WREYYMM.GetString().ToString().Replace("-", "").Substring(0, 6); //  6자리 : 자료를 세무서에 제출하는 연월일을 수록
            BDO101.BO101_DT05 = sSAUPNO.PadRight(13); // 13자리 : 본점(제출자)사업자등록번호 

            //【지급기관 기본사항】
            BDO101.BO101_DT06 = sSAUPNO.PadRight(13); // 13자리 : 지점(원천징수의무자)사업자등록번호 
            BDO101.BO101_DT07 = sFill.PadRight(12);   // 12자리 : 공란 
            BDO101.BO101_DT08 = sSANGHO;              // 40자리 : 상호명 (재정리 됨) 
            BDO101.BO101_DT09 = sFill.PadRight(70);   // 70자리 : 지점명칭-영문명 
            BDO101.BO101_DT10 = sFill.PadRight(140);  //140자리 : 징수의무자 소재지

            //【지급기관 제출내역】
            // 이자․배당소득 집계
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_4COEN963", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", sBRANCH.ToString(), this.DTP01_GSTYYMM.GetValue().ToString(), this.DTP01_GEDYYMM.GetValue().ToString()); // WIINCOMEF 

            DataTable dt_nae = this.DbConnector.ExecuteDataTable();

            if (dt_nae.Rows.Count > 0)
            {
                sDT11 = dt_nae.Rows[0]["DT11"].ToString();
                sDT12 = dt_nae.Rows[0]["DT12"].ToString();
                sDT13 = dt_nae.Rows[0]["DT13"].ToString();
                sDT14 = dt_nae.Rows[0]["DT14"].ToString();
            }
            BDO101.BO101_DT11 = string.Format("{0:D10}", Convert.ToInt64(sDT11));  //  10자리 : 총 제출건수 --> 모든 C레코드 건수
            BDO101.BO101_DT12 = string.Format("{0:D16}", Convert.ToInt64(sDT12));  //  16자리 : 소득금액합계 -> 모든 C레코드 소득금액의 합계금액
            BDO101.BO101_DT13 = string.Format("{0:D15}", Convert.ToInt64(sDT13));  //  15자리 : 소득세액합계 -> 모든 C레코드 소득세액의 합계금액
            BDO101.BO101_DT14 = string.Format("{0:D15}", Convert.ToInt64(sDT14));  //  15자리 : 법인세액합계 -> 모든 C레코드 법인세액의 합계금액

            //BDO101.BO101_DT15 = string.Format("{0:D10}", 0);  //  10자리 : 2014 귀속당초 제출건수
            //BDO101.BO101_DT16 = string.Format("{0:D16}", 0);  //  16자리 : 2014 귀속당초 소득금액 합계
            BDO101.BO101_DT15 = string.Format("{0:D10}", Convert.ToInt64(sDT11));  //  10자리 : 2014 귀속당초 제출건수
            BDO101.BO101_DT16 = string.Format("{0:D16}", Convert.ToInt64(sDT12));  //  16자리 : 2014 귀속당초 소득금액 합계
            BDO101.BO101_DT17 = string.Format("{0:D10}", 0);  //  10자리 : 2014 귀속삭제 제출건수
            BDO101.BO101_DT18 = string.Format("{0:D16}", 0);  //  16자리 : 2014 귀속삭제 소득금액 합계
            BDO101.BO101_DT19 = string.Format("{0:D10}", 0);  //  10자리 : 2014 귀속수정 제출건수
            BDO101.BO101_DT20 = string.Format("{0:D16}", 0);  //  16자리 : 2014 귀속수정 소득금액 합계
            BDO101.BO101_DT21 = sFill.PadRight(45);    //  45자리 : 공란

            BDO101.BO101_DT22 = sYCHK;          // 1자리 : 연간 합산제출 -> 1 , 휴․폐업에 의한 수시제출-> 2 ,수시 분할제출 -> 3

            // 레코드 세팅 작업(자리수)
            sData = BDO101.BO101_DT01;
            sData += BDO101.BO101_DT02;
            sData += BDO101.BO101_DT03;
            sData += BDO101.BO101_DT04;
            sData += BDO101.BO101_DT05;
            sData += BDO101.BO101_DT06;
            sData += BDO101.BO101_DT07;

            sStrTemp = BDO101.BO101_DT08.Trim(); // 상호명 : 40자리
            sStrTemp += new String(Convert.ToChar(" "), (40 - Encoding.Default.GetByteCount(BDO101.BO101_DT08.Trim())));
            sData += sStrTemp;

            sData += BDO101.BO101_DT09;
            sData += BDO101.BO101_DT10;
            sData += BDO101.BO101_DT11;
            sData += BDO101.BO101_DT12;
            sData += BDO101.BO101_DT13; 
            sData += BDO101.BO101_DT14; 
            sData += BDO101.BO101_DT15; 
            sData += BDO101.BO101_DT16; 
            sData += BDO101.BO101_DT17; 
            sData += BDO101.BO101_DT18; 
            sData += BDO101.BO101_DT19;
            sData += BDO101.BO101_DT20;
            sData += BDO101.BO101_DT21;
            sData += BDO101.BO101_DT22; 

            sw.WriteLine(sData);
        }
        #endregion
        #region Description : 이자․배당(금융)소득 - C레코드【지급명세 레코드】
        private void UP_Interest_Dividends_C(StreamWriter sw)
        {
            string sYEAR = string.Empty;
            string sData = string.Empty;
            string sStrTemp = string.Empty;
            string sFill = string.Empty;

            int iSEQ = 0;

            string sSAUPNO  = string.Empty; // 사업자등록번호
            string sSANGHO  = string.Empty; // 상호명
            string sNAMENM  = string.Empty; // 대표자이름
            string sCORPNO  = string.Empty; ; // 법인번호
            string sUPTAE   = string.Empty; // 업태
            string sEVENT   = string.Empty; // 종목
            string sTELNUM  = string.Empty; // 전화번호
            string sVNADDRS = string.Empty; // 사업장주소
            string sTAXAREA = string.Empty; // 관할세무소
            string sBUSTYPE = string.Empty; // 업종코드
            string sBUNGI   = string.Empty; //제출대상기간코드

            string sBRANCH  = string.Empty;

            sBRANCH = "1";

            sYEAR = this.DTP01_WREYYMM.GetValue().ToString().Substring(0, 4);

            struct_CO101 CDO101 = new struct_CO101();

            // 제출자 인적사항 조회 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_4CMDT923",  // AVSUBMITMF 
                sBRANCH.ToString()
                );

            DataTable dt_tae = this.DbConnector.ExecuteDataTable();

            if (dt_tae.Rows.Count > 0)
            {
                sSAUPNO = dt_tae.Rows[0]["ASMSAUPNO"].ToString(); // 사업자등록번호
                sSANGHO = dt_tae.Rows[0]["ASMSANGHO"].ToString(); // 상호명
                sNAMENM = dt_tae.Rows[0]["ASMNAMENM"].ToString(); // 대표자이름
                sCORPNO = dt_tae.Rows[0]["ASMCORPNO"].ToString(); // 법인번호
                sUPTAE = dt_tae.Rows[0]["ASMUPTAE"].ToString(); // 업태
                sEVENT = dt_tae.Rows[0]["ASMEVENT"].ToString(); // 종목
                sTELNUM = Up_Telnum_Convert(dt_tae.Rows[0]["ASMTELNUM"].ToString().Trim()); // 전화번호
                sVNADDRS = dt_tae.Rows[0]["ASMVNADDRS"].ToString(); // 사업장주소
                sTAXAREA = dt_tae.Rows[0]["ASMTAXAREA"].ToString(); // 관할세무소
                sBUSTYPE = dt_tae.Rows[0]["ASMBUSTYPE"].ToString(); // 업종코드
            }

            //【지급기관 제출내역】
            // 이자․배당소득 집계
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_4CQAN982", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", sBRANCH.ToString(), this.DTP01_GSTYYMM.GetValue().ToString(), this.DTP01_GEDYYMM.GetValue().ToString()); // WIINCOMEF 

            DataTable dt_nae = this.DbConnector.ExecuteDataTable();
  
            if (dt_nae.Rows.Count > 0)
            {
                for (int i = 0; i < dt_nae.Rows.Count; i++)
                {
                    iSEQ = iSEQ + 1;

                    CDO101.CO101_DT01 = "C";                  //  1자리 : 자료구분 ==> “C”
                    CDO101.CO101_DT02 = "40";                 //  2자리 : 서식코드 ==>  "40"
                    CDO101.CO101_DT03 = sTAXAREA.PadRight(3); //  3자리 : 제출자 소재지관할 세무서 코드
                    CDO101.CO101_DT04 = this.DTP01_WREYYMM.GetString().ToString().Replace("-", "").Substring(0, 6); //  6자리 : 자료를 세무서에 제출하는 연월일을 수록
                    CDO101.CO101_DT05 = sSAUPNO.PadRight(13); // 13자리 : 본점(제출자)사업자등록번호 
                    CDO101.CO101_DT06 = sSAUPNO.PadRight(13); // 13자리 : 지점(징수의무자) 사업자등록번호 
                    CDO101.CO101_DT07 = string.Format("{0:D8}", iSEQ);  // 8자리 : 일련번호

                    //【소득자 기본사항】
                    CDO101.CO101_DT08 = dt_nae.Rows[i]["DT08"].ToString();     // 70자리 : 성명 (재정리) 
                    CDO101.CO101_DT09 = dt_nae.Rows[i]["DT09"].ToString().PadRight(20);     // 20자리 : 주민(사업자)등록번호 및 거주지국의 인식번호 등 
                    CDO101.CO101_DT10 = dt_nae.Rows[i]["DT10"].ToString().PadRight(08);     //  8자리 : 생년월일 
                    CDO101.CO101_DT11 = dt_nae.Rows[i]["DT11"].ToString().PadRight(03);     //  3자리 : 소득자구분코드 
                    CDO101.CO101_DT12 = dt_nae.Rows[i]["DT12"].ToString().PadRight(140);    // 140자리 : 주소(소재지) 
                    CDO101.CO101_DT13 = dt_nae.Rows[i]["DT13"].ToString().PadRight(01);     //  1자리 : 거주구분 
                    CDO101.CO101_DT14 = dt_nae.Rows[i]["DT14"].ToString().PadRight(02);     //  2자리 : 거주지국 코드 
                    CDO101.CO101_DT15 = dt_nae.Rows[i]["DT15"].ToString().PadRight(20);     // 20자리 : 계좌번호 또는 증서번호 
                    CDO101.CO101_DT16 = dt_nae.Rows[i]["DT16"].ToString().PadRight(01);     // 1자리 : 신탁이익여부 
                    CDO101.CO101_DT17 = dt_nae.Rows[i]["DT17"].ToString().PadRight(08);     // 8자리 : 지급일자 
                    CDO101.CO101_DT18 = dt_nae.Rows[i]["DT18"].ToString().PadRight(06);     // 6자리 : 소득귀속연월 
                    CDO101.CO101_DT19 = dt_nae.Rows[i]["DT19"].ToString().PadRight(03);     // 3자리 : 과세구분 
                    CDO101.CO101_DT20 = dt_nae.Rows[i]["DT20"].ToString().PadRight(02);     // 2자리 : 소득의종류 
                    CDO101.CO101_DT21 = dt_nae.Rows[i]["DT21"].ToString().PadRight(02);     // 2자리 : 조세특례등 
                    CDO101.CO101_DT22 = dt_nae.Rows[i]["DT22"].ToString().PadRight(03);     // 3자리 : 금융상품코드 
                    CDO101.CO101_DT23 = dt_nae.Rows[i]["DT23"].ToString().PadRight(12);     // 12자리 : 유가증권표준코드 (사업자등록번호) 
                    CDO101.CO101_DT24 = dt_nae.Rows[i]["DT24"].ToString().PadRight(02);     // 2자리 : 채권이자구분 
                    CDO101.CO101_DT25 = dt_nae.Rows[i]["DT25"].ToString().PadRight(16);     // 16자리 : 지급대상기간 
                    CDO101.CO101_DT26 = dt_nae.Rows[i]["DT26"].ToString().PadRight(10);     // 10자리 : 이자율 등 
                    CDO101.CO101_DT27 = UP_Minus_Conv_Fill(dt_nae.Rows[i]["DT27"].ToString().Trim(), 13);  // 13자리 : 소득금액 
                    CDO101.CO101_DT28 = dt_nae.Rows[i]["DT28"].ToString().PadRight(06);     // 6자리 : 세율 
                    // 【원천징수내역】
                    CDO101.CO101_DT29 = UP_Minus_Conv_Fill(dt_nae.Rows[i]["DT29"].ToString().Trim(), 13);     // 13자리 : 소득세액 
                    CDO101.CO101_DT30 = UP_Minus_Conv_Fill(dt_nae.Rows[i]["DT30"].ToString().Trim(), 13);     // 13자리 : 법인세액 
                    CDO101.CO101_DT31 = UP_Minus_Conv_Fill(dt_nae.Rows[i]["DT31"].ToString().Trim(), 13);     // 13자리 : 지방소득세액 
                    CDO101.CO101_DT32 = UP_Minus_Conv_Fill(dt_nae.Rows[i]["DT32"].ToString().Trim(), 13);     // 13자리 : 농어촌특별세 

                    CDO101.CO101_DT33 = sFill.PadRight(33);     // 33자리 : 공란 
                    CDO101.CO101_DT34 = dt_nae.Rows[i]["DT34"].ToString().PadRight(1);     // 1자리 : 변동자료구분코드 

                 
                    // 레코드 세팅 작업(자리수)
                    sData = CDO101.CO101_DT01;
                    sData += CDO101.CO101_DT02;
                    sData += CDO101.CO101_DT03;
                    sData += CDO101.CO101_DT04;
                    sData += CDO101.CO101_DT05;
                    sData += CDO101.CO101_DT06;
                    sData += CDO101.CO101_DT07;
                    sStrTemp = CDO101.CO101_DT08.Trim(); // 소득자 성명 : 70자리
                    sStrTemp += new String(Convert.ToChar(" "), (70 - Encoding.Default.GetByteCount(CDO101.CO101_DT08.Trim())));
                    sData += sStrTemp;
                    sData += CDO101.CO101_DT09;
                    sData += CDO101.CO101_DT10;
                    sData += CDO101.CO101_DT11;
                    sData += CDO101.CO101_DT12;
                    sData += CDO101.CO101_DT13;  
                    sData += CDO101.CO101_DT14;  
                    sData += CDO101.CO101_DT15;  
                    sData += CDO101.CO101_DT16;
                    sData += CDO101.CO101_DT17;
                    sData += CDO101.CO101_DT18;
                    sData += CDO101.CO101_DT19;
                    sData += CDO101.CO101_DT20;
                    sData += CDO101.CO101_DT21;
                    sData += CDO101.CO101_DT22;
                    sData += CDO101.CO101_DT23;
                    sData += CDO101.CO101_DT24;
                    sData += CDO101.CO101_DT25;
                    sData += CDO101.CO101_DT26;
                    sData += CDO101.CO101_DT27;
                    sData += CDO101.CO101_DT28;
                    sData += CDO101.CO101_DT29;
                    sData += CDO101.CO101_DT30;
                    sData += CDO101.CO101_DT31;
                    sData += CDO101.CO101_DT32;
                    sData += CDO101.CO101_DT33; // 공란
                    sData += CDO101.CO101_DT34;  

                    sw.WriteLine(sData);
                }
            }
        }
        #endregion

        #region Description : 기타소득 명세서 File 처리 로직
        private void UP_Other_Income_File_Create()
        {
            string sFile_Path = "C:\\eosdata\\";
            if (System.IO.Directory.Exists(sFile_Path) == false)
            {
                System.IO.Directory.CreateDirectory(sFile_Path);
            }

            // 구분(1.본점, 2.지점)에 따라 파일이름을 각각 설정한다.
            string sFile_Name = string.Empty;
            string sPath = string.Empty;

            sFile_Name = "C:\\eosdata\\" + "G6108110.449";

            if (File.Exists(sFile_Name))
            {
                File.Delete(sFile_Name);
            }

            //if (this.CBO01_WABRANCH.GetValue().ToString() == "1")
            //{
            //    sFile_Name = "C:\\eosdata\\" + "G6108110.449";

            //    if (File.Exists(sFile_Name))
            //    {
            //        File.Delete(sFile_Name);
            //    }
            //}
            //else
            //{
            //    sFile_Name = "C:\\eosdata\\" + "G1058516.181";

            //    if (File.Exists(sFile_Name))
            //    {
            //        File.Delete(sFile_Name);
            //    }
            //}

            StreamWriter sw = new StreamWriter(sFile_Name, false, Encoding.Default);
            string sRecord = string.Empty;

            // 서식코드 A : A레코드(본점(제출자) 레코드) (HEAD) -- [길이 : 170]
            // A:23
            UP_Other_Income_HEAD_A(sw);
            // 서식코드 B : B레코드 (원천징수의무자별 집계 레코드) -- [길이 : 170]
            // B:23
            UP_Other_Income_TOTA_B(sw);
            // 서식코드 C : C레코드 (기타소득자 레코드) -- [길이 : 170]
            // C:23
            UP_Other_Income_C(sw);

            sw.Close();
        }
        #endregion
        #region Description : 기타소득 - A레코드 (제출자) 레코드
        private void UP_Other_Income_HEAD_A(StreamWriter sw)
        {
            string sData = string.Empty;
            string sStrTemp = string.Empty;
            string sFill = string.Empty;

            string sSAUPNO = string.Empty;  // 사업자등록번호
            string sSANGHO = string.Empty;  // 상호명
            string sNAMENM = string.Empty;  // 대표자이름
            string sCORPNO = string.Empty;  // 법인번호
            string sUPTAE = string.Empty;   // 업태
            string sEVENT = string.Empty;   // 종목
            string sTELNUM = string.Empty;  // 전화번호
            string sVNADDRS = string.Empty; // 사업장주소
            string sTAXAREA = string.Empty; // 관할세무소
            string sBUSTYPE = string.Empty; // 업종코드
            string sBUNGI = string.Empty;   // 제출대상기간코드

            string sBRANCH = string.Empty;

            sBRANCH = "1";

            struct_EA101 EA101 = new struct_EA101();

            // 제출자 인적사항 조회 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_4CMDT923",  // AVSUBMITMF 
                sBRANCH.ToString()
                );

            DataTable dt_tae = this.DbConnector.ExecuteDataTable();

            if (dt_tae.Rows.Count > 0)
            {
                sSAUPNO = dt_tae.Rows[0]["ASMSAUPNO"].ToString(); // 사업자등록번호
                sSANGHO = dt_tae.Rows[0]["ASMSANGHO"].ToString(); // 상호명
                sNAMENM = dt_tae.Rows[0]["ASMNAMENM"].ToString(); // 대표자이름
                sCORPNO = dt_tae.Rows[0]["ASMCORPNO"].ToString(); // 법인번호
                sUPTAE = dt_tae.Rows[0]["ASMUPTAE"].ToString(); // 업태
                sEVENT = dt_tae.Rows[0]["ASMEVENT"].ToString(); // 종목
                sTELNUM = Up_Telnum_Convert(dt_tae.Rows[0]["ASMTELNUM"].ToString().Trim()); // 전화번호
                sVNADDRS = dt_tae.Rows[0]["ASMVNADDRS"].ToString(); // 사업장주소
                sTAXAREA = dt_tae.Rows[0]["ASMTAXAREA"].ToString(); // 관할세무소
                sBUSTYPE = dt_tae.Rows[0]["ASMBUSTYPE"].ToString(); // 업종코드
            }

            EA101.EA101_DT01 = "A";                  //  1자리 : 자료구분 ==> “A”
            EA101.EA101_DT02 = "23";                 //  2자리 : 서식코드 ==>  "23"
            EA101.EA101_DT03 = sTAXAREA.PadRight(3); //  3자리 : 제출자 소재지관할 세무서 코드
            EA101.EA101_DT04 = this.DTP01_WREYYMM.GetString().ToString(); //  8자리 : 자료를 세무서에 제출하는 연월일을 수록

            // 【제출자】
            EA101.EA101_DT05 = "2";                  //  1자리 : 제출자 구분  (1:세무대리인/ 2:법인/ 3:개인 중)
            EA101.EA101_DT06 = sFill.PadRight(06);   //  6자리 : 세무대리인 관리번호
            EA101.EA101_DT07 = "tyc2921";            // 20자리 사용자 ID (본점-TYC2921 , 본점-TYC2922) (재정리 됨)
            EA101.EA101_DT08 = "1074";               //  4자리 세무프로그램코드

            //if (this.CBO01_WABRANCH.GetValue().ToString() == "1")
            //{
            //    EA101.EA101_DT07 = "tyc2921";       // 20자리 사용자 ID (본점-TYC2921 , 본점-TYC2922) (재정리 됨)
            //    EA101.EA101_DT08 = "1074";               //  4자리 세무프로그램코드
            //}
            //else
            //{
            //    EA101.EA101_DT07 = "tyc2922";       // 20자리 사용자 ID (본점-TYC2921 , 본점-TYC2922) (재정리 됨)
            //    EA101.EA101_DT08 = "1075";               //  4자리 세무프로그램코드
            //}
            //EA101.EA101_DT08 = "9000";               //  4자리 세무프로그램코드
            EA101.EA101_DT09 = sSAUPNO.Replace("-", "").PadRight(10); //  10자리 : 자료제출자 사업자등록번호
            EA101.EA101_DT10 = sSANGHO;              // 30자리 : 상호명 (재정리 됨) 
            EA101.EA101_DT11 = "회계파트";           // 30자리 : 담당자 부서 (재정리 됨) 
            EA101.EA101_DT12 = "황성환";             // 30자리 : 담당자 성명 (재정리 됨) 
            EA101.EA101_DT13 = "052-228-3314";       // 15자리 : 담당자 전화번호 (재정리 됨) 

            // 【제출내역】
            EA101.EA101_DT14 = "00001";               //  5자리 : 신고의무자수 원천징수의무자(B레코드) 수
            EA101.EA101_DT15 = sFill.PadRight(5);     //  5자리 : 공란

            // 레코드 세팅 작업(자리수)
            sData = EA101.EA101_DT01;
            sData += EA101.EA101_DT02;
            sData += EA101.EA101_DT03;
            sData += EA101.EA101_DT04;
            sData += EA101.EA101_DT05;
            sData += EA101.EA101_DT06;
            sStrTemp = EA101.EA101_DT07.Trim(); // 7 홈택스 ID : 20 자리 
            sStrTemp += new String(Convert.ToChar(" "), (20 - Encoding.Default.GetByteCount(EA101.EA101_DT07.Trim())));
            sData += sStrTemp;

            sData += EA101.EA101_DT08;
            sData += EA101.EA101_DT09;

            sStrTemp = EA101.EA101_DT10.Trim(); // 10 법인명(상호) : 30 자리
            sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(EA101.EA101_DT10.Trim())));
            sData += sStrTemp;

            sStrTemp = EA101.EA101_DT11.Trim(); // 10 담당부서 : 30자리
            sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(EA101.EA101_DT11.Trim())));
            sData += sStrTemp;

            sStrTemp = EA101.EA101_DT12.Trim(); // 11 담당자성명 : 30자리
            sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(EA101.EA101_DT12.Trim())));
            sData += sStrTemp;

            sStrTemp = EA101.EA101_DT13.Trim(); // 12 담당자연락처 : 15자리
            sStrTemp += new String(Convert.ToChar(" "), (15 - Encoding.Default.GetByteCount(EA101.EA101_DT13.Trim())));
            sData += sStrTemp;

            sData += EA101.EA101_DT14;
            sData += EA101.EA101_DT15; // 공란

            sw.WriteLine(sData);
        }
        #endregion
        #region Description : 기타소득 - B레코드【원천징수의무자별 집계 레코드】
        private void UP_Other_Income_TOTA_B(StreamWriter sw)
        {
            string sData = string.Empty;
            string sStrTemp = string.Empty;
            string sFill = string.Empty;

            string sSAUPNO  = string.Empty; // 사업자등록번호
            string sSANGHO  = string.Empty; // 상호명
            string sNAMENM  = string.Empty; // 대표자이름
            string sCORPNO  = string.Empty; // 법인번호
            string sUPTAE   = string.Empty; // 업태
            string sEVENT   = string.Empty; // 종목
            string sTELNUM  = string.Empty; // 전화번호
            string sVNADDRS = string.Empty; // 사업장주소
            string sTAXAREA = string.Empty; // 관할세무소
            string sBUSTYPE = string.Empty; // 업종코드
            string sBUNGI   = string.Empty; //제출대상기간코드

            string sWB07 = string.Empty;
            string sWB08 = string.Empty;
            string sWB09 = string.Empty;
            string sWB10 = string.Empty;
            string sWB11 = string.Empty;
            string sWB12 = string.Empty;
            string sWB13 = string.Empty;

            string sYCHK = string.Empty;

            string sBRANCH = string.Empty;

            sBRANCH = "1";

            int iMM = 1;
            for (int i = Convert.ToInt16(this.DTP01_GSTYYMM.GetValue().ToString().Replace("-", "").Substring(4, 2)); i < Convert.ToInt16(this.DTP01_GEDYYMM.GetValue().ToString().Replace("-", "").Substring(4, 2)); i++)
            {
                iMM = iMM + 1;
            }

            if (iMM == 12)
            {
                sYCHK = "1";
            }
            else { sYCHK = "3"; }

            struct_EB101 EBD101 = new struct_EB101();
  
            // 제출자 인적사항 조회 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_4CMDT923",  // AVSUBMITMF 
                sBRANCH.ToString()
                );

            DataTable dt_tae = this.DbConnector.ExecuteDataTable();

            if (dt_tae.Rows.Count > 0)
            {
                sSAUPNO = dt_tae.Rows[0]["ASMSAUPNO"].ToString(); // 사업자등록번호
                sSANGHO = dt_tae.Rows[0]["ASMSANGHO"].ToString(); // 상호명
                sNAMENM = dt_tae.Rows[0]["ASMNAMENM"].ToString(); // 대표자이름
                sCORPNO = dt_tae.Rows[0]["ASMCORPNO"].ToString(); // 법인번호
                sUPTAE = dt_tae.Rows[0]["ASMUPTAE"].ToString(); // 업태
                sEVENT = dt_tae.Rows[0]["ASMEVENT"].ToString(); // 종목
                sTELNUM = Up_Telnum_Convert(dt_tae.Rows[0]["ASMTELNUM"].ToString().Trim()); // 전화번호
                sVNADDRS = dt_tae.Rows[0]["ASMVNADDRS"].ToString(); // 사업장주소
                sTAXAREA = dt_tae.Rows[0]["ASMTAXAREA"].ToString(); // 관할세무소
                sBUSTYPE = dt_tae.Rows[0]["ASMBUSTYPE"].ToString(); // 업종코드
            }

            EBD101.EB101_DT01 = "B";                  //  1자리 : 자료구분 ==> “B”
            EBD101.EB101_DT02 = "23";                 //  2자리 : 서식코드 ==>  "23"
            EBD101.EB101_DT03 = sTAXAREA.PadRight(3); //  3자리 : 제출자 소재지관할 세무서 코드
            EBD101.EB101_DT04 = "000001";             //  6자리 : 신고의무자수 원천징수의무자(B레코드) 수

            // 【원천징수의무자】
            EBD101.EB101_DT05 = sSAUPNO.Replace("-","").PadRight(10); // 10자리 : 본점(제출자)사업자등록번호 
            EBD101.EB101_DT06 = sSANGHO;              // 30자리 : 상호명 (재정리 됨) 

            //【제출내역】
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_4CTB3987", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", sBRANCH.ToString(), this.DTP01_GSTYYMM.GetValue().ToString(), this.DTP01_GEDYYMM.GetValue().ToString()); // WIINCOMEF 

            DataTable dt_nae = this.DbConnector.ExecuteDataTable();
            if (dt_nae.Rows.Count > 0)
            {
                sWB07 = dt_nae.Rows[0]["B07"].ToString();
                sWB08 = dt_nae.Rows[0]["B08"].ToString();
                sWB09 = dt_nae.Rows[0]["B09"].ToString();
                if (Convert.ToDouble(Get_Numeric(dt_nae.Rows[0]["B10"].ToString())) > 0)
                {  sWB10 = dt_nae.Rows[0]["B10"].ToString(); }
                else
                {  sWB10 = "0";};

                if (Convert.ToDouble(Get_Numeric(dt_nae.Rows[0]["B11"].ToString())) > 0)
                { sWB11 = dt_nae.Rows[0]["B11"].ToString(); }
                else
                { sWB11 = "0";};

                if (Convert.ToDouble(Get_Numeric(dt_nae.Rows[0]["B12"].ToString())) > 0)
                {  sWB12 = dt_nae.Rows[0]["B12"].ToString(); }
                else
                {  sWB12 = "0";};

                if (Convert.ToDouble(Get_Numeric(dt_nae.Rows[0]["B13"].ToString())) > 0)
                {  sWB13 = dt_nae.Rows[0]["B13"].ToString(); }
                else
                {  sWB13 = "0";};
            }

            EBD101.EB101_DT07 = string.Format("{0:D06}", Convert.ToInt64(sWB07));    //  10자리 : 연간소득인원수
            EBD101.EB101_DT08 = string.Format("{0:D10}", Convert.ToInt64(sWB08));    //  16자리 : 연간총지급건수 -> C레코드의 지급건수(항목13)의 합계
            EBD101.EB101_DT09 = string.Format("{0:D15}", Convert.ToInt64(sWB09));    //  15자리 : 연간총지급액계 -> C레코드의 연간지급총액(항목14)의 합계
            EBD101.EB101_DT10 = string.Format("{0:D15}", Convert.ToInt64(sWB10));    //  15자리 : 연간소득금액합계 -> C레코드의 소득금액(항목16)의 합계
            EBD101.EB101_DT11 = string.Format("{0:D15}", Convert.ToInt64(sWB11));    //  15자리 : 소득세합계 -> C레코드의 소득세(항목18)의 합계
            EBD101.EB101_DT12 = string.Format("{0:D15}", Convert.ToInt64(sWB12));    //  15자리 : 지방소득세합계 -> C레코드의 지방소득세(항목19)의 합계
            EBD101.EB101_DT13 = string.Format("{0:D15}", Convert.ToInt64(sWB13));    //  15자리 : 원천징수액합계 -> C레코드의 원천징수액(항목20)의 합계
            EBD101.EB101_DT14 = sYCHK;               //  1자리 : 연간 합산제출 -> 1 , 휴․폐업에 의한 수시제출-> 2 ,수시 분할제출 -> 3
            EBD101.EB101_DT15 = sFill.PadRight(26); //  26자리 : 공란

            // 레코드 세팅 작업(자리수)
            sData = EBD101.EB101_DT01;
            sData += EBD101.EB101_DT02;
            sData += EBD101.EB101_DT03;
            sData += EBD101.EB101_DT04;
            sData += EBD101.EB101_DT05;

            sStrTemp = EBD101.EB101_DT06.Trim(); // 상호명 : 30자리
            sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(EBD101.EB101_DT06.Trim())));
            sData += sStrTemp;

            sData += EBD101.EB101_DT07;
            sData += EBD101.EB101_DT08;
            sData += EBD101.EB101_DT09;
            sData += EBD101.EB101_DT10;
            sData += EBD101.EB101_DT11;
            sData += EBD101.EB101_DT12;
            sData += EBD101.EB101_DT13;
            sData += EBD101.EB101_DT14;
            sData += EBD101.EB101_DT15;

            sw.WriteLine(sData);
        }
        #endregion
        #region Description : 기타소득 - C레코드【기타소득자 레코드】
        private void UP_Other_Income_C(StreamWriter sw)
        {
            string sData = string.Empty;
            string sStrTemp = string.Empty;
            string sFill = string.Empty;

            int iSEQ = 0;

            string sSAUPNO  = string.Empty; // 사업자등록번호
            string sSANGHO  = string.Empty; // 상호명
            string sNAMENM  = string.Empty; // 대표자이름
            string sCORPNO  = string.Empty; ; // 법인번호
            string sUPTAE   = string.Empty; // 업태
            string sEVENT   = string.Empty; // 종목
            string sTELNUM  = string.Empty; // 전화번호
            string sVNADDRS = string.Empty; // 사업장주소
            string sTAXAREA = string.Empty; // 관할세무소
            string sBUSTYPE = string.Empty; // 업종코드
            string sBUNGI   = string.Empty; //제출대상기간코드

            string sBRANCH  = string.Empty;

            sBRANCH = "1";

            struct_EC101 ECD101 = new struct_EC101();

            // 제출자 인적사항 조회 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_4CMDT923",  // AVSUBMITMF 
                sBRANCH.ToString()
                );

            DataTable dt_tae = this.DbConnector.ExecuteDataTable();

            if (dt_tae.Rows.Count > 0)
            {
                sSAUPNO = dt_tae.Rows[0]["ASMSAUPNO"].ToString(); // 사업자등록번호
                sSANGHO = dt_tae.Rows[0]["ASMSANGHO"].ToString(); // 상호명
                sNAMENM = dt_tae.Rows[0]["ASMNAMENM"].ToString(); // 대표자이름
                sCORPNO = dt_tae.Rows[0]["ASMCORPNO"].ToString(); // 법인번호
                sUPTAE = dt_tae.Rows[0]["ASMUPTAE"].ToString(); // 업태
                sEVENT = dt_tae.Rows[0]["ASMEVENT"].ToString(); // 종목
                sTELNUM = Up_Telnum_Convert(dt_tae.Rows[0]["ASMTELNUM"].ToString().Trim()); // 전화번호
                sVNADDRS = dt_tae.Rows[0]["ASMVNADDRS"].ToString(); // 사업장주소
                sTAXAREA = dt_tae.Rows[0]["ASMTAXAREA"].ToString(); // 관할세무소
                sBUSTYPE = dt_tae.Rows[0]["ASMBUSTYPE"].ToString(); // 업종코드
            }

            //【자료관리번호】
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_4CTB4988", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", sBRANCH.ToString(), this.DTP01_GSTYYMM.GetValue().ToString(), this.DTP01_GEDYYMM.GetValue().ToString()); // WIINCOMEF 

            DataTable dt_nae = this.DbConnector.ExecuteDataTable();

            if (dt_nae.Rows.Count > 0)
            {
                for (int i = 0; i < dt_nae.Rows.Count; i++)
                {
                    iSEQ = iSEQ + 1;

                    ECD101.EC101_DT01 = "C";                  //  1자리 : 자료구분 ==> “C”
                    ECD101.EC101_DT02 = "23";                 //  2자리 : 서식코드 ==>  "23"
                    ECD101.EC101_DT03 = sTAXAREA.PadRight(3); //  3자리 : 제출자 소재지관할 세무서 코드
                    ECD101.EC101_DT04 = string.Format("{0:D6}", iSEQ);  // 6자리 : 일련번호
                    // 【원천징수의무자】
                    ECD101.EC101_DT05 = sSAUPNO.Replace("-", "").PadRight(10); // 10자리 : 지점(징수의무자) 사업자등록번호 
                    // 【소득자】
                    ECD101.EC101_DT06 = dt_nae.Rows[i]["C06"].ToString().PadRight(13);     // 13자리 : 소득자의 주민등록번호
                    ECD101.EC101_DT07 = dt_nae.Rows[i]["C07"].ToString();                  // 30자리 : 소득자의 성명(재정리) 
                    ECD101.EC101_DT08 = dt_nae.Rows[i]["C08"].ToString().PadRight(01);     //  1자리 : 거주구분
                    ECD101.EC101_DT09 = dt_nae.Rows[i]["C09"].ToString().PadRight(01);     //  1자리 : 내.외국인구분 
                    ECD101.EC101_DT10 = dt_nae.Rows[i]["C10"].ToString().PadRight(02);     //  2자리 : 소득구분코드
                    //【소득지급명세】
                    ECD101.EC101_DT11 = dt_nae.Rows[i]["C11"].ToString().PadRight(04);     //  4자리 : 소득귀속연도 
                    ECD101.EC101_DT12 = dt_nae.Rows[i]["C12"].ToString().PadRight(04);     //  4자리 : 지급연도 
                    ECD101.EC101_DT13 = string.Format("{0:D04}", Convert.ToInt64(dt_nae.Rows[i]["C13"].ToString()));     //  4자리 : 지급건수 
                    ECD101.EC101_DT14 = UP_ETC_Minus_Conv_Fill(dt_nae.Rows[i]["C14"].ToString(), 13);     // 14자리 : 연간지급총액(음수표시 1자리 ,  연간지급총액 13자리)
                    ECD101.EC101_DT15 = UP_ETC_Minus_Conv_Fill(dt_nae.Rows[i]["C15"].ToString(), 13);     // 14자리 : 필요경비(음수표시 1자리 ,  필요경비 13자리) 
                    ECD101.EC101_DT16 = UP_ETC_Minus_Conv_Fill(dt_nae.Rows[i]["C16"].ToString(), 13);     // 14자리 : 소득금액(음수표시 1자리 ,  소득금액 13자리) 
                    ECD101.EC101_DT17 = string.Format("{0:D02}", Convert.ToInt64(UP_DotDelete(dt_nae.Rows[i]["C17"].ToString())));     // 2자리  : 세율 
                    // 【원천징수액】
                    ECD101.EC101_DT18 = UP_ETC_Minus_Conv_Fill(dt_nae.Rows[i]["C18"].ToString(), 13);     // 14자리 : 소득세(음수표시 1자리 ,  소득세 13자리) 
                    ECD101.EC101_DT19 = UP_ETC_Minus_Conv_Fill(dt_nae.Rows[i]["C19"].ToString(), 13);     // 14자리 : 지방소득세(음수표시 1자리 ,  지방소득세 13자리) 
                    ECD101.EC101_DT20 = UP_ETC_Minus_Conv_Fill(dt_nae.Rows[i]["C20"].ToString(), 13);     // 14자리 : 계(음수표시 1자리 ,  계 13자리)) 

                    ECD101.EC101_DT21 = sFill.PadRight(03);     // 3자리 : 공란 

                    // 레코드 세팅 작업(자리수)
                    sData = ECD101.EC101_DT01;
                    sData += ECD101.EC101_DT02;
                    sData += ECD101.EC101_DT03;
                    sData += ECD101.EC101_DT04;
                    sData += ECD101.EC101_DT05;
                    sData += ECD101.EC101_DT06;

                    sStrTemp = ECD101.EC101_DT07.Trim(); // 소득자 성명 : 30자리
                    sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(ECD101.EC101_DT07.Trim())));
                    sData += sStrTemp;

                    sData += ECD101.EC101_DT08;
                    sData += ECD101.EC101_DT09;
                    sData += ECD101.EC101_DT10;
                    sData += ECD101.EC101_DT11;
                    sData += ECD101.EC101_DT12;
                    sData += ECD101.EC101_DT13;
                    sData += ECD101.EC101_DT14;
                    sData += ECD101.EC101_DT15;
                    sData += ECD101.EC101_DT16;
                    sData += ECD101.EC101_DT17;
                    sData += ECD101.EC101_DT18;
                    sData += ECD101.EC101_DT19;
                    sData += ECD101.EC101_DT20;
                    sData += ECD101.EC101_DT21; // 공란

                    sw.WriteLine(sData);
                }
            }
        }
        #endregion

        #region Description : 사업소득 명세서 File 처리 로직
        private void UP_Business_Income_File_Create()
        {
            string sFile_Path = "C:\\eosdata\\";
            if (System.IO.Directory.Exists(sFile_Path) == false)
            {
                System.IO.Directory.CreateDirectory(sFile_Path);
            }

            // 구분(1.본점, 2.지점)에 따라 파일이름을 각각 설정한다.
            string sFile_Name = string.Empty;
            string sPath = string.Empty;

            sFile_Name = "C:\\eosdata\\" + "F6108110.449";

            if (File.Exists(sFile_Name))
            {
                File.Delete(sFile_Name);
            }

            //if (this.CBO01_WABRANCH.GetValue().ToString() == "1")
            //{
            //    sFile_Name = "C:\\eosdata\\" + "F6108110.449";

            //    if (File.Exists(sFile_Name))
            //    {
            //        File.Delete(sFile_Name);
            //    }
            //}
            //else
            //{
            //    sFile_Name = "C:\\eosdata\\" + "F1058516.181";

            //    if (File.Exists(sFile_Name))
            //    {
            //        File.Delete(sFile_Name);
            //    }
            //}

            StreamWriter sw = new StreamWriter(sFile_Name, false, Encoding.Default);
            string sRecord = string.Empty;

            // 서식코드 A : A레코드(본점(제출자) 레코드) (HEAD) -- [길이 : 190]
            // A:24
            UP_Business_Income_HEAD_A(sw);
            // 서식코드 B : B레코드 (원천징수의무자별 집계 레코드) -- [길이 : 190]
            // B:24
            UP_Business_Income_TOTA_B(sw);
            // 서식코드 C : C레코드 (사업소득자 레코드) -- [길이 : 190]
            // C:24
            UP_Business_Income_C(sw);

            sw.Close();
        }
        #endregion
        #region Description : 사업소득 - A레코드 (제출자) 레코드
        private void UP_Business_Income_HEAD_A(StreamWriter sw)
        {
            string sData = string.Empty;
            string sStrTemp = string.Empty;
            string sFill = string.Empty;

            string sSAUPNO  = string.Empty;  // 사업자등록번호
            string sSANGHO  = string.Empty;  // 상호명
            string sNAMENM  = string.Empty;  // 대표자이름
            string sCORPNO  = string.Empty;  // 법인번호
            string sUPTAE   = string.Empty;   // 업태
            string sEVENT   = string.Empty;   // 종목
            string sTELNUM  = string.Empty;  // 전화번호
            string sVNADDRS = string.Empty; // 사업장주소
            string sTAXAREA = string.Empty; // 관할세무소
            string sBUSTYPE = string.Empty; // 업종코드
            string sBUNGI   = string.Empty;   // 제출대상기간코드

            string sBRANCH  = string.Empty;

            sBRANCH = "1";

            struct_CA101 CA101 = new struct_CA101();

            // 제출자 인적사항 조회 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_4CMDT923",  // AVSUBMITMF 
                sBRANCH.ToString()
                );

            DataTable dt_tae = this.DbConnector.ExecuteDataTable();

            if (dt_tae.Rows.Count > 0)
            {
                sSAUPNO  = dt_tae.Rows[0]["ASMSAUPNO"].ToString(); // 사업자등록번호
                sSANGHO  = dt_tae.Rows[0]["ASMSANGHO"].ToString(); // 상호명
                sNAMENM  = dt_tae.Rows[0]["ASMNAMENM"].ToString(); // 대표자이름
                sCORPNO  = dt_tae.Rows[0]["ASMCORPNO"].ToString(); // 법인번호
                sUPTAE   = dt_tae.Rows[0]["ASMUPTAE"].ToString(); // 업태
                sEVENT   = dt_tae.Rows[0]["ASMEVENT"].ToString(); // 종목
                sTELNUM  = Up_Telnum_Convert(dt_tae.Rows[0]["ASMTELNUM"].ToString().Trim()); // 전화번호
                sVNADDRS = dt_tae.Rows[0]["ASMVNADDRS"].ToString(); // 사업장주소
                sTAXAREA = dt_tae.Rows[0]["ASMTAXAREA"].ToString(); // 관할세무소
                sBUSTYPE = dt_tae.Rows[0]["ASMBUSTYPE"].ToString(); // 업종코드
            }

            CA101.CA101_DT01 = "A";                  //  1자리 : 자료구분 ==> “A”
            CA101.CA101_DT02 = "24";                 //  2자리 : 서식코드 ==>  "24"
            CA101.CA101_DT03 = sTAXAREA.PadRight(3); //  3자리 : 제출자 소재지관할 세무서 코드
            CA101.CA101_DT04 = this.DTP01_WREYYMM.GetString().ToString(); //  8자리 : 자료를 세무서에 제출하는 연월일을 수록

            // 【제출자】
            CA101.CA101_DT05 = "2";                  //  1자리 : 제출자 구분 ( 1:세무대리인/ 2:법인/ 3:개인 중)
            CA101.CA101_DT06 = sFill.PadRight(06);   //  6자리 : 세무대리인 관리번호
            CA101.CA101_DT07 = "tyc2921";            // 20자리 사용자 ID (본점-TYC2921 , 본점-TYC2922) (재정리 됨)
            CA101.CA101_DT08 = "1074";               //  4자리 세무프로그램코드

            //if (this.CBO01_WABRANCH.GetValue().ToString() == "1")
            //{
            //    CA101.CA101_DT07 = "tyc2921";       // 20자리 사용자 ID (본점-TYC2921 , 본점-TYC2922) (재정리 됨)
            //    CA101.CA101_DT08 = "1074";               //  4자리 세무프로그램코드
            //}
            //else
            //{
            //    CA101.CA101_DT07 = "tyc2922";       // 20자리 사용자 ID (본점-TYC2921 , 본점-TYC2922) (재정리 됨)
            //    CA101.CA101_DT08 = "1075";               //  4자리 세무프로그램코드
            //}
            //CA101.CA101_DT08 = "9000";               //  4자리 세무프로그램코드
            CA101.CA101_DT09 = sSAUPNO.Replace("-", "").PadRight(10); //  10자리 : 자료제출자 사업자등록번호
            CA101.CA101_DT10 = sSANGHO;              // 30자리 : 상호명 (재정리 됨) 
            CA101.CA101_DT11 = "회계파트";           // 30자리 : 담당자 부서 (재정리 됨) 
            CA101.CA101_DT12 = "황성환";             // 30자리 : 담당자 성명 (재정리 됨) 
            CA101.CA101_DT13 = "052-228-3314";       // 15자리 : 담당자 전화번호 (재정리 됨) 

            // 【제출내역】
            CA101.CA101_DT14 = "00001";               //  5자리 : 신고의무자수 원천징수의무자(B레코드) 수
            CA101.CA101_DT15 = sFill.PadRight(25);    //  25자리 : 공란

            // 레코드 세팅 작업(자리수)
            sData = CA101.CA101_DT01;
            sData += CA101.CA101_DT02;
            sData += CA101.CA101_DT03;
            sData += CA101.CA101_DT04;
            sData += CA101.CA101_DT05;
            sData += CA101.CA101_DT06;
            sStrTemp = CA101.CA101_DT07.Trim(); // 7 홈택스 ID : 20 자리 
            sStrTemp += new String(Convert.ToChar(" "), (20 - Encoding.Default.GetByteCount(CA101.CA101_DT07.Trim())));
            sData += sStrTemp;

            sData += CA101.CA101_DT08;
            sData += CA101.CA101_DT09;

            sStrTemp = CA101.CA101_DT10.Trim(); // 10 법인명(상호) : 30 자리
            sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(CA101.CA101_DT10.Trim())));
            sData += sStrTemp;

            sStrTemp = CA101.CA101_DT11.Trim(); // 10 담당부서 : 30자리
            sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(CA101.CA101_DT11.Trim())));
            sData += sStrTemp;

            sStrTemp = CA101.CA101_DT12.Trim(); // 11 담당자성명 : 30자리
            sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(CA101.CA101_DT12.Trim())));
            sData += sStrTemp;

            sStrTemp = CA101.CA101_DT13.Trim(); // 12 담당자연락처 : 15자리
            sStrTemp += new String(Convert.ToChar(" "), (15 - Encoding.Default.GetByteCount(CA101.CA101_DT13.Trim())));
            sData += sStrTemp;

            sData += CA101.CA101_DT14;
            sData += CA101.CA101_DT15; // 공란

            sw.WriteLine(sData);
        }
        #endregion
        #region Description : 사업소득 - B레코드【원천징수의무자별 집계 레코드】
        private void UP_Business_Income_TOTA_B(StreamWriter sw)
        {
            string sData = string.Empty;
            string sStrTemp = string.Empty;
            string sFill = string.Empty;

            string sSAUPNO  = string.Empty; // 사업자등록번호
            string sSANGHO  = string.Empty; // 상호명
            string sNAMENM  = string.Empty; // 대표자이름
            string sCORPNO  = string.Empty; // 법인번호
            string sUPTAE   = string.Empty; // 업태
            string sEVENT   = string.Empty; // 종목
            string sTELNUM  = string.Empty; // 전화번호
            string sVNADDRS = string.Empty; // 사업장주소
            string sTAXAREA = string.Empty; // 관할세무소
            string sBUSTYPE = string.Empty; // 업종코드
            string sBUNGI   = string.Empty; // 제출대상기간코드

            string sWB07 = string.Empty;  // 연간소득인원
            string sWB08 = string.Empty;
            string sWB09 = string.Empty;
            string sWB10 = string.Empty;  // 소득세합계
            string sWB11 = string.Empty;  // 지방소득세합계
            string sWB12 = string.Empty;  // 원천징수액합계
            string sWB13 = string.Empty;  // 소액부징수 연간건수합계
            string sWB14 = string.Empty;  // 소액부징수 연간지급액합계

            string sYCHK = string.Empty;

            string sBRANCH = string.Empty;

            sBRANCH = "1";

            int iMM = 1;
            for (int i = Convert.ToInt16(this.DTP01_GSTYYMM.GetValue().ToString().Replace("-", "").Substring(4, 2)); i < Convert.ToInt16(this.DTP01_GEDYYMM.GetValue().ToString().Replace("-", "").Substring(4, 2)); i++)
            {
                iMM = iMM + 1;
            }

            if (iMM == 12)
            {
                sYCHK = "1";
            }
            else { sYCHK = "3"; }

            struct_CB101 CBD101 = new struct_CB101();

            // 제출자 인적사항 조회 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_4CMDT923",  // AVSUBMITMF 
                sBRANCH.ToString()
                );

            DataTable dt_tae = this.DbConnector.ExecuteDataTable();

            if (dt_tae.Rows.Count > 0)
            {
                sSAUPNO = dt_tae.Rows[0]["ASMSAUPNO"].ToString(); // 사업자등록번호
                sSANGHO = dt_tae.Rows[0]["ASMSANGHO"].ToString(); // 상호명
                sNAMENM = dt_tae.Rows[0]["ASMNAMENM"].ToString(); // 대표자이름
                sCORPNO = dt_tae.Rows[0]["ASMCORPNO"].ToString(); // 법인번호
                sUPTAE = dt_tae.Rows[0]["ASMUPTAE"].ToString(); // 업태
                sEVENT = dt_tae.Rows[0]["ASMEVENT"].ToString(); // 종목
                sTELNUM = Up_Telnum_Convert(dt_tae.Rows[0]["ASMTELNUM"].ToString().Trim()); // 전화번호
                sVNADDRS = dt_tae.Rows[0]["ASMVNADDRS"].ToString(); // 사업장주소
                sTAXAREA = dt_tae.Rows[0]["ASMTAXAREA"].ToString(); // 관할세무소
                sBUSTYPE = dt_tae.Rows[0]["ASMBUSTYPE"].ToString(); // 업종코드
            }

            CBD101.CB101_DT01 = "B";                  //  1자리 : 자료구분 ==> “B”
            CBD101.CB101_DT02 = "24";                 //  2자리 : 서식코드 ==>  "24"
            CBD101.CB101_DT03 = sTAXAREA.PadRight(3); //  3자리 : 제출자 소재지관할 세무서 코드
            CBD101.CB101_DT04 = "000001";             //  6자리 : 신고의무자수 원천징수의무자(B레코드) 수

            // 【원천징수의무자】
            CBD101.CB101_DT05 = sSAUPNO.Replace("-", "").PadRight(10); // 10자리 : 본점(제출자)사업자등록번호 
            CBD101.CB101_DT06 = sSANGHO;              // 30자리 : 상호명 (재정리 됨) 

            //【제출내역】
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_51JEK178", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", sBRANCH.ToString(), this.DTP01_GSTYYMM.GetValue().ToString(), this.DTP01_GEDYYMM.GetValue().ToString()); // WIINCOMEF 

            DataTable dt_nae = this.DbConnector.ExecuteDataTable();
            if (dt_nae.Rows.Count > 0)
            {
                sWB07 = dt_nae.Rows[0]["B07"].ToString();
                sWB08 = dt_nae.Rows[0]["B08"].ToString();

                if (Convert.ToDouble(Get_Numeric(dt_nae.Rows[0]["B09"].ToString())) > 0)
                { sWB09 = dt_nae.Rows[0]["B09"].ToString(); }
                else
                { sWB09 = "0"; };

                if (Convert.ToDouble(Get_Numeric(dt_nae.Rows[0]["B10"].ToString())) > 0)
                { sWB10 = dt_nae.Rows[0]["B10"].ToString(); }
                else
                { sWB10 = "0"; };

                if (Convert.ToDouble(Get_Numeric(dt_nae.Rows[0]["B11"].ToString())) > 0)
                { sWB11 = dt_nae.Rows[0]["B11"].ToString(); }
                else
                { sWB11 = "0"; };

                if (Convert.ToDouble(Get_Numeric(dt_nae.Rows[0]["B12"].ToString())) > 0)
                { sWB12 = dt_nae.Rows[0]["B12"].ToString(); }
                else
                { sWB12 = "0"; };

                if (Convert.ToDouble(Get_Numeric(dt_nae.Rows[0]["B13"].ToString())) > 0)
                { sWB13 = dt_nae.Rows[0]["B13"].ToString(); }
                else
                { sWB13 = "0"; };

                if (Convert.ToDouble(Get_Numeric(dt_nae.Rows[0]["B13"].ToString())) > 0)
                { sWB14 = dt_nae.Rows[0]["B14"].ToString(); }
                else
                { sWB14 = "0"; };
            }

            CBD101.CB101_DT07 = string.Format("{0:D06}", Convert.ToInt64(sWB07));    //   6자리 : 연간소득인원수
            CBD101.CB101_DT08 = string.Format("{0:D10}", Convert.ToInt64(sWB08));    //  10자리 : 연간총지급건수 -> C레코드의 지급건수(항목15)의 합계
            CBD101.CB101_DT09 = string.Format("{0:D15}", Convert.ToInt64(sWB09));    //  15자리 : 연간총지급액계 -> C레코드의 연간지급총액(항목16))의 합계
            CBD101.CB101_DT10 = string.Format("{0:D15}", Convert.ToInt64(sWB10));    //  15자리 : 소득세합계 -> C레코드의 소득세(항목18)의 합계
            CBD101.CB101_DT11 = string.Format("{0:D15}", Convert.ToInt64(sWB11));    //  15자리 : 지방소득세합계 -> C레코드의 지방소득세(항목19)의 합계
            CBD101.CB101_DT12 = string.Format("{0:D15}", Convert.ToInt64(sWB12));    //  15자리 : 원천징수액합계 --> C레코드의 원천징수액(항목20)의 합계
            CBD101.CB101_DT13 = string.Format("{0:D10}", Convert.ToInt64(sWB13));    //  10자리 : 소액부징수 연간건수합계  -> 소액부징수된 자료의 연간 건수 수록
            CBD101.CB101_DT14 = string.Format("{0:D15}", Convert.ToInt64(sWB14));    //  15자리 : 소액부징수 연간지급액합계 -> 소액부징수된 자료의 연간 지급액 수록
            CBD101.CB101_DT15 = sYCHK;              //  1자리 : 연간 합산제출 -> 1 , 휴․폐업에 의한 수시제출-> 2 ,수시 분할제출 -> 3
            CBD101.CB101_DT16 = sFill.PadRight(36); //  36자리 : 공란

            // 레코드 세팅 작업(자리수)
            sData = CBD101.CB101_DT01;
            sData += CBD101.CB101_DT02;
            sData += CBD101.CB101_DT03;
            sData += CBD101.CB101_DT04;
            sData += CBD101.CB101_DT05;

            sStrTemp = CBD101.CB101_DT06.Trim(); // 상호명 : 30자리
            sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(CBD101.CB101_DT06.Trim())));
            sData += sStrTemp;

            sData += CBD101.CB101_DT07;
            sData += CBD101.CB101_DT08;
            sData += CBD101.CB101_DT09;
            sData += CBD101.CB101_DT10;
            sData += CBD101.CB101_DT11;
            sData += CBD101.CB101_DT12;
            sData += CBD101.CB101_DT13;
            sData += CBD101.CB101_DT14;
            sData += CBD101.CB101_DT15;
            sData += CBD101.CB101_DT16;

            sw.WriteLine(sData);
        }
        #endregion
        #region Description : 사업소득 - C레코드【사업소득자 레코드】
        private void UP_Business_Income_C(StreamWriter sw)
        {
            string sData = string.Empty;
            string sStrTemp = string.Empty;
            string sFill = string.Empty;

            int iSEQ = 0;

            string sSAUPNO  = string.Empty; // 사업자등록번호
            string sSANGHO  = string.Empty; // 상호명
            string sNAMENM  = string.Empty; // 대표자이름
            string sCORPNO  = string.Empty; // 법인번호
            string sUPTAE   = string.Empty; // 업태
            string sEVENT   = string.Empty; // 종목
            string sTELNUM  = string.Empty; // 전화번호
            string sVNADDRS = string.Empty; // 사업장주소
            string sTAXAREA = string.Empty; // 관할세무소
            string sBUSTYPE = string.Empty; // 업종코드
            string sBUNGI   = string.Empty; //제출대상기간코드

            string sBRANCH  = string.Empty;

            sBRANCH = "1";

            struct_CC101 CCD101 = new struct_CC101();

            // 제출자 인적사항 조회 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_4CMDT923",  // AVSUBMITMF 
                sBRANCH.ToString()
                );

            DataTable dt_tae = this.DbConnector.ExecuteDataTable();

            if (dt_tae.Rows.Count > 0)
            {
                sSAUPNO  = dt_tae.Rows[0]["ASMSAUPNO"].ToString(); // 사업자등록번호
                sSANGHO  = dt_tae.Rows[0]["ASMSANGHO"].ToString(); // 상호명
                sNAMENM  = dt_tae.Rows[0]["ASMNAMENM"].ToString(); // 대표자이름
                sCORPNO  = dt_tae.Rows[0]["ASMCORPNO"].ToString(); // 법인번호
                sUPTAE   = dt_tae.Rows[0]["ASMUPTAE"].ToString(); // 업태
                sEVENT   = dt_tae.Rows[0]["ASMEVENT"].ToString(); // 종목
                sTELNUM  = Up_Telnum_Convert(dt_tae.Rows[0]["ASMTELNUM"].ToString().Trim()); // 전화번호
                sVNADDRS = dt_tae.Rows[0]["ASMVNADDRS"].ToString(); // 사업장주소
                sTAXAREA = dt_tae.Rows[0]["ASMTAXAREA"].ToString(); // 관할세무소
                sBUSTYPE = dt_tae.Rows[0]["ASMBUSTYPE"].ToString(); // 업종코드
            }

            //【자료관리번호】
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_51JEI177", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", sBRANCH.ToString(), this.DTP01_GSTYYMM.GetValue().ToString(), this.DTP01_GEDYYMM.GetValue().ToString()); // WIINCOMEF 

            DataTable dt_nae = this.DbConnector.ExecuteDataTable();

            if (dt_nae.Rows.Count > 0)
            {
                for (int i = 0; i < dt_nae.Rows.Count; i++)
                {
                    iSEQ = iSEQ + 1;

                    CCD101.CC101_DT01 = "C";                            //  1자리 : 자료구분 ==> “C”
                    CCD101.CC101_DT02 = "24";                           //  2자리 : 서식코드 ==>  "24"
                    CCD101.CC101_DT03 = sTAXAREA.PadRight(3);           //  3자리 : 제출자 소재지관할 세무서 코드
                    CCD101.CC101_DT04 = string.Format("{0:D7}", iSEQ);  //  7자리 : 일련번호
                    // 【원천징수의무자】
                    CCD101.CC101_DT05 = sSAUPNO.Replace("-", "").PadRight(10); // 10자리 : 지점(징수의무자) 사업자등록번호 
                    // 【소득자】
                    CCD101.CC101_DT06 = dt_nae.Rows[i]["C06"].ToString().PadRight(13);     // 13자리 : 소득자의 주민등록번호
                    CCD101.CC101_DT07 = dt_nae.Rows[i]["C07"].ToString();                  // 30자리 : 소득자의 성명(재정리) 
                    CCD101.CC101_DT08 = dt_nae.Rows[i]["C08"].ToString().Replace("-", "").PadRight(10);     // 10자리 : 사업자등록번호
                    CCD101.CC101_DT09 = dt_nae.Rows[i]["C09"].ToString().PadRight(30);     // 30자리 : 상호(재정리)
                    CCD101.CC101_DT10 = dt_nae.Rows[i]["C10"].ToString().PadRight(01);     //  1자리 : 거주구분
                    CCD101.CC101_DT11 = dt_nae.Rows[i]["C11"].ToString().PadRight(01);     //  1자리 : 내.외국인구분 
                    CCD101.CC101_DT12 = dt_nae.Rows[i]["C12"].ToString().PadRight(06);     //  6자리 : 업종구분코드
                    //【소득지급명세】
                    CCD101.CC101_DT13 = dt_nae.Rows[i]["C13"].ToString().PadRight(04);     //  4자리 : 소득귀속연도 
                    CCD101.CC101_DT14 = dt_nae.Rows[i]["C14"].ToString().PadRight(04);     //  4자리 : 지급연도 
                    CCD101.CC101_DT15 = string.Format("{0:D04}", Convert.ToInt64(dt_nae.Rows[i]["C15"].ToString()));     //  4자리 : 지급건수 
                    CCD101.CC101_DT16 = UP_ETC_Minus_Conv_Fill(dt_nae.Rows[i]["C16"].ToString(), 13);     // 14자리 : 연간지급총액(음수표시 1자리 ,  연간지급총액 13자리)
                    CCD101.CC101_DT17 = string.Format("{0:D02}", Convert.ToInt64(UP_DotDelete(dt_nae.Rows[i]["C17"].ToString())));     // 2자리  : 세율 
                    // 【원천징수액】
                    CCD101.CC101_DT18 = UP_ETC_Minus_Conv_Fill(dt_nae.Rows[i]["C18"].ToString(), 13);     // 14자리 : 소득세(음수표시 1자리 ,  소득세 13자리) 
                    CCD101.CC101_DT19 = UP_ETC_Minus_Conv_Fill(dt_nae.Rows[i]["C19"].ToString(), 13);     // 14자리 : 지방소득세(음수표시 1자리 ,  지방소득세 13자리) 
                    CCD101.CC101_DT20 = UP_ETC_Minus_Conv_Fill(dt_nae.Rows[i]["C20"].ToString(), 13);     // 14자리 : 계(음수표시 1자리 ,  계 13자리)) 
                    CCD101.CC101_DT21 = sFill.PadRight(06);     // 6자리 : 공란 

                    // 레코드 세팅 작업(자리수)
                    sData = CCD101.CC101_DT01;
                    sData += CCD101.CC101_DT02;
                    sData += CCD101.CC101_DT03;
                    sData += CCD101.CC101_DT04;
                    sData += CCD101.CC101_DT05;
                    sData += CCD101.CC101_DT06;

                    sStrTemp = CCD101.CC101_DT07.Trim(); // 소득자 성명 : 30자리
                    sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(CCD101.CC101_DT07.Trim())));
                    sData += sStrTemp;

                    sData += CCD101.CC101_DT08;

                    sStrTemp = CCD101.CC101_DT09.Trim(); // 상호 : 30자리
                    sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(CCD101.CC101_DT09.Trim())));
                    sData += sStrTemp;

                    sData += CCD101.CC101_DT10;
                    sData += CCD101.CC101_DT11;
                    sData += CCD101.CC101_DT12;
                    sData += CCD101.CC101_DT13;
                    sData += CCD101.CC101_DT14;
                    sData += CCD101.CC101_DT15;
                    sData += CCD101.CC101_DT16;
                    sData += CCD101.CC101_DT17;
                    sData += CCD101.CC101_DT18;
                    sData += CCD101.CC101_DT19;
                    sData += CCD101.CC101_DT20;
                    sData += CCD101.CC101_DT21; // 공란

                    sw.WriteLine(sData);
                }
            }
        }
        #endregion


        #region Description : 의료비 명세서 File 처리 로직
        private void UP_Medical_Expenses_File_Create()
        {
            string sFile_Path = "C:\\eosdata\\";
            if (System.IO.Directory.Exists(sFile_Path) == false)
            {
                System.IO.Directory.CreateDirectory(sFile_Path);
            }

            // 구분(1.본점, 2.지점)에 따라 파일이름을 각각 설정한다.
            string sFile_Name = string.Empty;
            string sPath = string.Empty;

            sFile_Name = "C:\\eosdata\\" + "CA6108110.449";

            if (File.Exists(sFile_Name))
            {
                File.Delete(sFile_Name);
            }

            //if (this.CBO01_WABRANCH.GetValue().ToString() == "1")
            //{
            //    sFile_Name = "C:\\eosdata\\" + "CA6108110.449";

            //    if (File.Exists(sFile_Name))
            //    {
            //        File.Delete(sFile_Name);
            //    }
            //}
            //else
            //{
            //    sFile_Name = "C:\\eosdata\\" + "CA1058516.181";

            //    if (File.Exists(sFile_Name))
            //    {
            //        File.Delete(sFile_Name);
            //    }
            //}

            StreamWriter sw = new StreamWriter(sFile_Name, false, Encoding.Default);
            string sRecord = string.Empty;

            //// 서식코드 A : A레코드(의료비지급명세서 레코드) -- [길이 : 250]
            //// A:26
            //UP_Medical_Expenses_LIST_A(sw);

            sw.Close();
        }
        #endregion

        #region Description : 기부금 명세서 File 처리 로직
        private void UP_Contribution_File_Create()
        {
            string sFile_Path = "C:\\eosdata\\";
            if (System.IO.Directory.Exists(sFile_Path) == false)
            {
                System.IO.Directory.CreateDirectory(sFile_Path);
            }

            // 구분(1.본점, 2.지점)에 따라 파일이름을 각각 설정한다.
            string sFile_Name = string.Empty;
            string sPath = string.Empty;

            sFile_Name = "C:\\eosdata\\" + "H6108110.449";

            if (File.Exists(sFile_Name))
            {
                File.Delete(sFile_Name);
            }

            //if (this.CBO01_WABRANCH.GetValue().ToString() == "1")
            //{
            //    sFile_Name = "C:\\eosdata\\" + "H6108110.449";

            //    if (File.Exists(sFile_Name))
            //    {
            //        File.Delete(sFile_Name);
            //    }
            //}
            //else
            //{
            //    sFile_Name = "C:\\eosdata\\" + "H1058516.181";

            //    if (File.Exists(sFile_Name))
            //    {
            //        File.Delete(sFile_Name);
            //    }
            //}

            StreamWriter sw = new StreamWriter(sFile_Name, false, Encoding.Default);
            string sRecord = string.Empty;

            //// 서식코드 A : A레코드(본점(제출자) 레코드) (HEAD) -- [길이 : 180]
            //// A:27
            //UP_Contribution_HEAD_A(sw);

            //// 서식코드 B : B레코드【원천징수의무자별 집계 레코드】 -- [길이 : 180]
            //// B:27
            //UP_Contribution_TOTAL_B(sw);

            //// 서식코드 C : C레코드【기부금 조정명세 레코드】 -- [길이 : 180]
            //// C:27
            //UP_Contribution_ADJUST_C(sw);

            //// 서식코드 D : D레코드【해당연도 기부명세 레코드】 -- [길이 : 180]
            //// D:27
            //UP_Contribution_LIST_D(sw);

            sw.Close();
        }
        #endregion

        //---- 구조 정보 선언 ----//
        #region Description : 신고서 구조 정보 선언 -  일용근로
        // 일용근로
        public struct struct_HO201
        {
            // 일용근로소득 명세서
            // 1) 일용근로소득 Head 레코드
            public string HO201_DT01;          //1자리
            public string HO201_DT02;          //1자리
            public string HO201_DT03;          //1자리
            public string HO201_DT04;          //1자리
            public string HO201_DT05;          //1자리
            public string HO201_DT06;          //1자리
            public string HO201_DT07;          //1자리
            public string HO201_DT08;          //1자리
            public string HO201_DT09;          //1자리
            public string HO201_DT10;          //1자리
            public string HO201_DT11;          //1자리
            public string HO201_DT12;          //1자리
            public string HO201_DT13;          //1자리
            public string HO201_DT14;          //1자리
            public string HO201_DT15;          //1자리
            public string HO201_DT16;          //64자리 : 공란
        }
        
        // 지급자별 집계 레코드
        public struct struct_BO201
        {
            public string BO201_DT01;         
            public string BO201_DT02;         
            public string BO201_DT03;         
            public string BO201_DT04;         
            public string BO201_DT05;         
            public string BO201_DT06;         
            public string BO201_DT07;         
            public string BO201_DT08;         
            public string BO201_DT09;         
            public string BO201_DT10;         
            public string BO201_DT11;         
            public string BO201_DT12;         
            public string BO201_DT13;         
            public string BO201_DT14;         
            public string BO201_DT15;         
            public string BO201_DT16;         
            public string BO201_DT17;         
            public string BO201_DT18;         
            public string BO201_DT19;         
        }
        
        // 소득자 레코드
        public struct struct_CO201
        {
            // 소득자 레코드
            public string CO201_DT01;
            public string CO201_DT02;
            public string CO201_DT03;
            public string CO201_DT04;
            public string CO201_DT05;
            public string CO201_DT06;
            public string CO201_DT07;
            public string CO201_DT08;
            public string CO201_DT09;
            public string CO201_DT10;
            public string CO201_DT11;
            public string CO201_DT12;
            public string CO201_DT13;
            public string CO201_DT14;
            public string CO201_DT15;
            public string CO201_DT16;
            public string CO201_DT17;
        }
        #endregion
        #region Description : 신고서 구조 정보 선언 -  이자․배당(금융)소득
        public struct struct_AO101
        {
            //  이자․배당(금융)소득 명세서
            // 1)  이자․배당(금융)소득 Head 레코드
            public string AO101_DT01;      //
            public string AO101_DT02;      //
            public string AO101_DT03;      //
            public string AO101_DT04;      //
            public string AO101_DT05;      //
            public string AO101_DT06;      //
            public string AO101_DT07;      //
            public string AO101_DT08;      //
            public string AO101_DT09;      //
            public string AO101_DT10;      //
            public string AO101_DT11;      //
            public string AO101_DT12;      //
            public string AO101_DT13;      //
            public string AO101_DT14;      //
            public string AO101_DT15;      //
            public string AO101_DT16;      //
            public string AO101_DT17;      //
            public string AO101_DT18;      //
            public string AO101_DT19;      //
            public string AO101_DT20;      //
            public string AO101_DT21;      //
            public string AO101_DT22;      //
            public string AO101_DT23;      //
            public string AO101_DT24;      //
            public string AO101_DT25;      //
            public string AO101_DT26;      //170자리 : 공란
        }


        public struct struct_BO101
        {
            //  원천징수의무자 별 집계 레코드
            public string BO101_DT01;      //
            public string BO101_DT02;      //
            public string BO101_DT03;      //
            public string BO101_DT04;      //
            public string BO101_DT05;      //
            public string BO101_DT06;      //
            public string BO101_DT07;      //
            public string BO101_DT08;      //
            public string BO101_DT09;      //
            public string BO101_DT10;      //
            public string BO101_DT11;      //
            public string BO101_DT12;      //
            public string BO101_DT13;      //
            public string BO101_DT14;      //
            public string BO101_DT15;      //
            public string BO101_DT16;      //
            public string BO101_DT17;      //
            public string BO101_DT18;      //
            public string BO101_DT19;      //
            public string BO101_DT20;      //
            public string BO101_DT21;      // 45자리 : 공란
            public string BO101_DT22;      // 
        }

        public struct struct_CO101
        {
            // 지급명세 레코드
            public string CO101_DT01;      
            public string CO101_DT02;      
            public string CO101_DT03;      
            public string CO101_DT04;      
            public string CO101_DT05;      
            public string CO101_DT06;      
            public string CO101_DT07;      
            public string CO101_DT08;      
            public string CO101_DT09;      
            public string CO101_DT10;      
            public string CO101_DT11;      
            public string CO101_DT12;      
            public string CO101_DT13;      
            public string CO101_DT14;      
            public string CO101_DT15;      
            public string CO101_DT16;      
            public string CO101_DT17;      
            public string CO101_DT18;      
            public string CO101_DT19;      
            public string CO101_DT20;      
            public string CO101_DT21;      
            public string CO101_DT22;      
            public string CO101_DT23;      
            public string CO101_DT24;      
            public string CO101_DT25;      
            public string CO101_DT26;      
            public string CO101_DT27;      
            public string CO101_DT28;      
            public string CO101_DT29;      
            public string CO101_DT30;      
            public string CO101_DT31;      
            public string CO101_DT32;      
            public string CO101_DT33;      
            public string CO101_DT34;      
        }

        #endregion
        #region Description : 신고서 구조 정보 선언 -  기타소득
        public struct struct_EA101
        {
            //  기타소득 명세서
            // 1)  기타소득 Head 레코드
            public string EA101_DT01;      //
            public string EA101_DT02;      //
            public string EA101_DT03;      //
            public string EA101_DT04;      //
            public string EA101_DT05;      //
            public string EA101_DT06;      //
            public string EA101_DT07;      //
            public string EA101_DT08;      //
            public string EA101_DT09;      //
            public string EA101_DT10;      //
            public string EA101_DT11;      //
            public string EA101_DT12;      //
            public string EA101_DT13;      //
            public string EA101_DT14;      //
            public string EA101_DT15;      // 5자리 : 공란
        }

        public struct struct_EB101
        {
            //  원천징수의무자 별 집계 레코드
            public string EB101_DT01;      //
            public string EB101_DT02;      //
            public string EB101_DT03;      //
            public string EB101_DT04;      //
            public string EB101_DT05;      //
            public string EB101_DT06;      //
            public string EB101_DT07;      //
            public string EB101_DT08;      //
            public string EB101_DT09;      //
            public string EB101_DT10;      //
            public string EB101_DT11;      //
            public string EB101_DT12;      //
            public string EB101_DT13;      //
            public string EB101_DT14;      //
            public string EB101_DT15;      // 26자리 : 공란
        }

        public struct struct_EC101
        {
            // C레코드【기타소득자 레코드】
            public string EC101_DT01;
            public string EC101_DT02;
            public string EC101_DT03;
            public string EC101_DT04;
            public string EC101_DT05;
            public string EC101_DT06;
            public string EC101_DT07;
            public string EC101_DT08;
            public string EC101_DT09;
            public string EC101_DT10;
            public string EC101_DT11;
            public string EC101_DT12;
            public string EC101_DT13;
            public string EC101_DT14;
            public string EC101_DT15;
            public string EC101_DT16;
            public string EC101_DT17;
            public string EC101_DT18;
            public string EC101_DT19;
            public string EC101_DT20;
            public string EC101_DT21; // 3자리 : 공란
        }


        #endregion
        #region Description : 신고서 구조 정보 선언 -  사업소득
        public struct struct_CA101
        {
            //  사업소득 명세서
            // 1)  사업소득 Head 레코드
            public string CA101_DT01;      //
            public string CA101_DT02;      //
            public string CA101_DT03;      //
            public string CA101_DT04;      //
            public string CA101_DT05;      //
            public string CA101_DT06;      //
            public string CA101_DT07;      //
            public string CA101_DT08;      //
            public string CA101_DT09;      //
            public string CA101_DT10;      //
            public string CA101_DT11;      //
            public string CA101_DT12;      //
            public string CA101_DT13;      //
            public string CA101_DT14;      //
            public string CA101_DT15;      // 25자리 : 공란
        }

        public struct struct_CB101
        {
            //   원천징수의무자 별 집계 레코드
            public string CB101_DT01;      //
            public string CB101_DT02;      //
            public string CB101_DT03;      //
            public string CB101_DT04;      //
            public string CB101_DT05;      //
            public string CB101_DT06;      //
            public string CB101_DT07;      //
            public string CB101_DT08;      //
            public string CB101_DT09;      //
            public string CB101_DT10;      //
            public string CB101_DT11;      //
            public string CB101_DT12;      //
            public string CB101_DT13;      //
            public string CB101_DT14;      //
            public string CB101_DT15;      //
            public string CB101_DT16;      // 36자리 : 공란
        }

        public struct struct_CC101
        {
            // C레코드【사업소득자 레코드】
            public string CC101_DT01;
            public string CC101_DT02;
            public string CC101_DT03;
            public string CC101_DT04;
            public string CC101_DT05;
            public string CC101_DT06;
            public string CC101_DT07;
            public string CC101_DT08;
            public string CC101_DT09;
            public string CC101_DT10;
            public string CC101_DT11;
            public string CC101_DT12;
            public string CC101_DT13;
            public string CC101_DT14;
            public string CC101_DT15;
            public string CC101_DT16;
            public string CC101_DT17;
            public string CC101_DT18;
            public string CC101_DT19;
            public string CC101_DT20;
            public string CC101_DT21; // 6자리 : 공란
        }
        #endregion
        #region Description : 신고서 구조 정보 선언 -  의료비지급 명세서
        public struct struct_MA101
        {
            //  의료비지급 명세서
            // 1)  의료비지급 명세서 레코드
            public string MA101_DT01;      //
            public string MA101_DT02;      //
            public string MA101_DT03;      //
            public string MA101_DT04;      //
            public string MA101_DT05;      //
            public string MA101_DT06;      //
            public string MA101_DT07;      //
            public string MA101_DT08;      //
            public string MA101_DT09;      //
            public string MA101_DT10;      //
            public string MA101_DT11;      //
            public string MA101_DT12;      //
            public string MA101_DT13;      //
            public string MA101_DT14;      //
            public string MA101_DT15;      //
            public string MA101_DT16;      //
            public string MA101_DT17;      //
            public string MA101_DT18;      //
            public string MA101_DT19;      //
            public string MA101_DT20;      //
            public string MA101_DT21;      //
            public string MA101_DT22;      //
            public string MA101_DT23;      // 19자리 : 공란
        }
        #endregion
        #region Description : 신고서 구조 정보 선언 -  기부금 지급명세서
        public struct struct_GA101
        {
            //  기부금 지급명세서
            // 1) A: 기부금 지급명세서 제출자 레코드
            public string GA101_DT01;      //
            public string GA101_DT02;      //
            public string GA101_DT03;      //
            public string GA101_DT04;      //
            public string GA101_DT05;      //
            public string GA101_DT06;      //
            public string GA101_DT07;      //
            public string GA101_DT08;      //
            public string GA101_DT09;      //
            public string GA101_DT10;      //
            public string GA101_DT11;      //
            public string GA101_DT12;      //
            public string GA101_DT13;      //
            public string GA101_DT14;      //
            public string GA101_DT15;      //
            public string GA101_DT16;      // 02자리 : 공란
        }

        public struct struct_GB101
        {
            //  기부금 지급명세서
            // 2)  B: 원천징수의무자별 집계 레코드
            public string GB101_DT01;      //
            public string GB101_DT02;      //
            public string GB101_DT03;      //
            public string GB101_DT04;      //
            public string GB101_DT05;      //
            public string GB101_DT06;      //
            public string GB101_DT07;      //
            public string GB101_DT08;      //
            public string GB101_DT09;      //
            public string GB101_DT10;      //
            public string GB101_DT11;      //
            public string GB101_DT12;      // 77자리 : 공란
        }

        public struct struct_GC101
        {
            //  기부금 지급명세서
            // 3) C: 기부금 조정명세 레코드
            public string GC101_DT01;      //
            public string GC101_DT02;      //
            public string GC101_DT03;      //
            public string GC101_DT04;      //
            public string GC101_DT05;      //
            public string GC101_DT06;      //
            public string GC101_DT07;      //
            public string GC101_DT08;      //
            public string GC101_DT09;      //
            public string GC101_DT10;      //
            public string GC101_DT11;      //
            public string GC101_DT12;      //
            public string GC101_DT13;      //
            public string GC101_DT14;      //
            public string GC101_DT15;      //
            public string GC101_DT16;      //
            public string GC101_DT17;      //
            public string GC101_DT18;      // 25자리 : 공란
        }

        public struct struct_GD101
        {
            //  기부금 지급명세서
            // 4) D: 해당연도 기부명세 레코드
            public string GD101_DT01;      //
            public string GD101_DT02;      //
            public string GD101_DT03;      //
            public string GD101_DT04;      //
            public string GD101_DT05;      //
            public string GD101_DT06;      //
            public string GD101_DT07;      //
            public string GD101_DT08;      //
            public string GD101_DT09;      //
            public string GD101_DT10;      //
            public string GD101_DT11;      //
            public string GD101_DT12;      //
            public string GD101_DT13;      //
            public string GD101_DT14;      //
            public string GD101_DT15;      //
            public string GD101_DT16;      //
            public string GD101_DT17;      // 42자리 : 공란
        }
        #endregion

        #region Description : 숫자 자리수 처리(-) 처리 병행
        private string UP_Minus_Conv_Fill(string sNum, int iLenth)
        {
            string sReturnValue = string.Empty;

            if (iLenth == 13) // 길이 13 자리
            {
                if (Convert.ToInt64(sNum.ToString().Trim()) < 0)
                {
                    sReturnValue = string.Format("{0:D12}", Convert.ToInt64(Get_Numeric(sNum.ToString().Trim())));    // 공급가액    :  13자리
                }
                else
                {
                    sReturnValue = string.Format("{0:D13}", Convert.ToInt64(Get_Numeric(sNum.ToString().Trim())));    // 공급가액    :  13자리
                }
            }
            else // 길이 15 자리
            {
                if (Convert.ToInt64(sNum.ToString().Trim()) < 0)
                {
                    sReturnValue = string.Format("{0:D14}", Convert.ToInt64(Get_Numeric(sNum.ToString().Trim())));    // 공급가액    :  15자리
                }
                else
                {
                    sReturnValue = string.Format("{0:D15}", Convert.ToInt64(Get_Numeric(sNum.ToString().Trim())));    // 공급가액    :  15자리
                }
            };

            return sReturnValue;
        }
        #endregion
        #region Description : 기타,사업소득 숫자 자리수 처리(-) 처리 병행 (14자리 변경)
        private string UP_ETC_Minus_Conv_Fill(string sNum, int iLenth)
        {
            string sReturnValue = string.Empty;

            if (iLenth == 13) // 길이 13 자리
            {
                if (Convert.ToInt64(sNum.ToString().Trim()) < 0)
                {
                    sReturnValue = "1" + string.Format("{0:D13}", Convert.ToInt64(Get_Numeric(sNum.ToString().Trim()))*0.1 );  // 공급가액    :  13자리
                }
                else
                {
                    sReturnValue = "0" + string.Format("{0:D13}", Convert.ToInt64(Get_Numeric(sNum.ToString().Trim())));    // 공급가액    :  13자리
                }
            }
            return sReturnValue;
        }
        #endregion

        #region Description : 전화번호 처리
        private string Up_Telnum_Convert(string sTelnum)
        {
            string sReTelnum = string.Empty;
            string[] sTel = new string[3];
            string[] strArray = sTelnum.Split('-');

            if (strArray.Length == 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    sTel[i] = strArray[i].PadRight(04);
                    sReTelnum = sReTelnum + sTel[i];
                }
            }
            else
            {
                sReTelnum = sTelnum;
            }

            return sReTelnum;
        }
        #endregion

        #region Description : 멀티키
        private string UP_Multi_Key_Fill(string sNum, int iLen)
        {
            string sReturnValue = "";
            string sValue = string.Empty;

            if (sNum == null)
            {
                sNum = "0";
            }

            if (iLen == 13)
            {
                if (Convert.ToInt64(sNum.ToString().Trim()) < 0)
                {
                    string sTempAmt = string.Format("{0:D13}", Convert.ToInt64(sNum.ToString().Trim()) * -1);
                    string sALPHAValue = UP_Minus_ALPHA(sTempAmt.Substring(12, 1));
                    sValue = sTempAmt.Substring(0, 12) + sALPHAValue;
                }
                else
                {
                    sValue = string.Format("{0:D13}", Convert.ToInt64(sNum.ToString().Trim()));
                }
            }

            if (iLen == 14)
            {
                if (Convert.ToInt64(sNum.ToString().Trim()) < 0)
                {
                    string sTempAmt = string.Format("{0:D14}", Convert.ToInt64(sNum.ToString().Trim()) * -1);
                    string sALPHAValue = UP_Minus_ALPHA(sTempAmt.Substring(13, 1));
                    sValue = sTempAmt.Substring(0, 13) + sALPHAValue;
                }
                else
                {
                    sValue = string.Format("{0:D14}", Convert.ToInt64(sNum.ToString().Trim()));
                }
            }

            if (iLen == 15)
            {
                if (Convert.ToInt64(sNum.ToString().Trim()) < 0)
                {
                    string sTempAmt = string.Format("{0:D15}", Convert.ToInt64(sNum.ToString().Trim()) * -1);
                    string sALPHAValue = UP_Minus_ALPHA(sTempAmt.Substring(14, 1));
                    sValue = sTempAmt.Substring(0, 14) + sALPHAValue;
                }
                else
                {
                    sValue = string.Format("{0:D15}", Convert.ToInt64(sNum.ToString().Trim()));
                }

            }

            sReturnValue = sValue;
            return sReturnValue;
        }

        private string UP_Minus_ALPHA(string sNum)
        {
            string sReturnValue = "";

            switch (sNum)
            {
                case "0":
                    sReturnValue = "}";
                    break;
                case "1":
                    sReturnValue = "J";
                    break;
                case "2":
                    sReturnValue = "K";
                    break;
                case "3":
                    sReturnValue = "L";
                    break;
                case "4":
                    sReturnValue = "M";
                    break;
                case "5":
                    sReturnValue = "N";
                    break;
                case "6":
                    sReturnValue = "O";
                    break;
                case "7":
                    sReturnValue = "P";
                    break;
                case "8":
                    sReturnValue = "Q";
                    break;
                case "9":
                    sReturnValue = "R";
                    break;
            }

            return sReturnValue;
        }
        #endregion


        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
