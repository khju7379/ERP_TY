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
    /// 원천세 전산매체자료 생성 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2014.10.24 08:42
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_3CQ5Y873 : 부가세 신고 거래처코드 구하기
    ///  TY_P_AC_42B67344 : 전산매체 신고용 부가세 제출자 인적사항
    ///  TY_P_AC_42B8L317 : 부가세 마감 체크
    ///  TY_P_AC_42H9G397 : 전자매체 옵션관리(총괄납부구분) 조회
    ///  TY_P_AC_4AUA3274 : 원천세 전산매체 생성 (환급세액)
    ///  TY_P_AC_4AUA5275 : 원천세 전산매체 생성 (세부)
    ///  TY_P_AC_4AUA6276 : 원천세 전산매체 생성 (HEAD)
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2B77B165 : 파일을 다운 작업을 하시겠습니까?
    ///  TY_M_AC_3CR9M876 : 부가세 옵션 자료가 없습니다.
    ///  TY_M_GB_25UAA711 : 파일을 다운로드하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  DWN : 다운
    ///  WABRANCH : 지점구분
    ///  EMYYMM : 기준년월
    ///  WREYYMM : 귀속년월
    /// </summary>
    public partial class TYACTP018B : TYBase
    {
        public TYACTP018B()
        {
            InitializeComponent();
        }

        #region Description : Page_Load
        private void TYACTP018B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_DWN.ProcessCheck += new TButton.CheckHandler(BTN61_DWN_ProcessCheck);

            this.DTP01_EMYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            this.DTP01_WREYYMM.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"));

            this.SetStartingFocus(this.DTP01_EMYYMM);
        }
        #endregion

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : DOWN 버튼 이벤트
        private void BTN61_DWN_Click(object sender, EventArgs e)
        {
            UP_TAXFile_Create();
        }
        #endregion


        #region Description : 파일 다운 ProcessCheck 이벤트
        private void BTN61_DWN_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            // 1생성 체크  WSUMMARYTF
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_4B3GF298", "1", this.DTP01_WREYYMM.GetValue().ToString());

            if (this.DbConnector.ExecuteDataTable().Rows.Count == 0)
            {
                this.ShowCustomMessage("신고서 자료가 존재하지 않습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                this.SetFocus(this.DTP01_EMYYMM);
                return;
            }

            //// 2.제출자 인적사항 체크 
            // this.DbConnector.CommandClear();
            // this.DbConnector.Attach
            //     (
            //     "TY_P_AC_42B67344",  // AVSUBMITMF 
            //     this.DTP01_WREYYMM.GetValue().ToString(),     // 귀속년월
            //     this.CBO01_WABRANCH.GetValue().ToString()     // 사업장(1본점, 2지점)
            //     );

            // DataTable dt_tae = this.DbConnector.ExecuteDataTable();

            // if (dt_tae.Rows.Count == 0)
            // {
            //     this.ShowCustomMessage("제출자 인적사항 미등록 확인 하세요 ", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //     e.Successed = false;
            //     this.SetFocus(this.DTP01_WREYYMM);
            //     return;
            // }

            // if (!this.ShowMessage("TY_M_AC_2B77B165"))
            // {
            //     e.Successed = false;
            //     return;
            // }

        }
        #endregion

        #region Description : 전자세금계산서 File 처리 로직
        private void UP_TAXFile_Create()
        {

            string sFile_Path = "C:\\ERSDATA\\";
            if (System.IO.Directory.Exists(sFile_Path) == false)
            {
                System.IO.Directory.CreateDirectory(sFile_Path);
            }

            // 21.11.23일 원본 소스
            //// 구분(1.본점, 2.지점)에 따라 파일이름을 각각 설정한다.
            //string sFile_Name = string.Empty;
            //string sPath = string.Empty;
            
            //if (this.CBO01_WABRANCH.GetValue().ToString() == "1")
            //{
            //    sFile_Name = "C:\\ERSDATA\\"+DateTime.Now.ToString("yyyyMMdd") + ".201";

            //    if (File.Exists(sFile_Name))
            //    {
            //        File.Delete(sFile_Name);
            //    }
            //}
            //else
            //{
            //    sFile_Name = "C:\\ERSDATA\\" + DateTime.Now.ToString("yyyyMMdd") + ".201";

            //    if (File.Exists(sFile_Name))
            //    {
            //        File.Delete(sFile_Name);
            //    }
            //}

            string sFile_Name = string.Empty;
            string sPath = string.Empty;

            sFile_Name = "C:\\ERSDATA\\" + DateTime.Now.ToString("yyyyMMdd") + ".201";

            if (File.Exists(sFile_Name))
            {
                File.Delete(sFile_Name);
            }


            StreamWriter sw = new StreamWriter(sFile_Name, false, Encoding.Default);

            string sRecord = string.Empty;

            // 서식코드 O201 : 원천징수 이행상황 신고서 레코드    (1. 원천징수이행상황신고서-Header)                     -- [길이 : 400]
            // 21 : O201
            UP_TAX_Create_HEAD_O201(sw);

            // 서식코드 O201 : 원천징수 이행상황 신고서 레코드    (2. 원천징수이행상황신고서_환급세액 조정)              -- [길이 : 200]
            //  27 : O201
            UP_TAX_Create_O201(sw);

            // 서식코드 O201 : 원천징수이행상황 신고서 세부 레코드(3. 원천징수이행상황신고서_원천징수 명세 및 납부세액)  -- [길이 : 150]
            //  28 : O201
            UP_TAX_Create_SO201(sw);

            sw.Close();


            this.ShowMessage("TY_M_GB_25UAA711");
        }
        #endregion

        // --- 생성 시작--- //
        #region Description : 일반과세자 신고서 레코드 생성 (HEAD O201) -- UP_TAX_Create_HEAD_O201() -- 세무프로그램 코드
        private void UP_TAX_Create_HEAD_O201(StreamWriter sw)
        {
            string sYEAR    = string.Empty;
            string sData    = string.Empty;
            string sStrTemp = string.Empty;
            string sFill    = string.Empty;

            string sSAUPNO    = string.Empty; // 사업자등록번호
            string sSANGHO    = string.Empty; // 상호명
            string sNAMENM    = string.Empty; // 대표자이름
            string sCORPNO    = string.Empty; // 법인번호
            string sUPTAE     = string.Empty; // 업태
            string sEVENT     = string.Empty; // 종목
            string sTELNUM    = string.Empty; // 전화번호
            string sVNADDRS   = string.Empty; // 사업장주소
            string sTAXAREA   = string.Empty; // 관할세무소
            string sBUSTYPE   = string.Empty; // 업종코드
            string sWRETURNGB = string.Empty; // 18 원천신고구분(1.매월, 2.반기)
            string sWDETAILGB = string.Empty; // 21 부표
            string sWYEARTXGB = string.Empty; // 22 연말정산
            string sWDISOISGB = string.Empty; // 26 소득처분
            string sWREQUESGB = string.Empty; // 27 환급신청
            string sWSUCCESGB = string.Empty; // 30 차월이월환급세액 승계명세여부

            sYEAR = this.DTP01_WREYYMM.GetValue().ToString().Substring(0, 4);

            /*
            struct_HO201 HDO201 = new struct_HO201();

            // 제출자 인적사항 조회 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_4AUA6276",  // AVSUBMITMF , WSUMMARYTF
                 this.CBO01_WABRANCH.GetValue().ToString(),   // 사업장(1본점, 2지점)
                 this.DTP01_WREYYMM.GetValue().ToString() // 년월
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

                sWRETURNGB = dt_tae.Rows[0]["WRETURNGB"].ToString(); // 18  원천신고구분(1.매월, 2.반기)
                sWDETAILGB = dt_tae.Rows[0]["WDETAILGB"].ToString(); // 21  부표
                sWYEARTXGB = dt_tae.Rows[0]["WYEARTXGB"].ToString(); // 22  연말정산
                sWDISOISGB = dt_tae.Rows[0]["WDISOISGB"].ToString(); // 26  소득처분
                sWREQUESGB = dt_tae.Rows[0]["WREQUESGB"].ToString(); // 27  환급신청
                sWSUCCESGB = dt_tae.Rows[0]["WREQUESGB"].ToString(); // 30  차월이월환급세액 승계명세여부
            }

            HDO201.HO201_DT01 = "21";                  //  2자리 : 자료구분 ==> (11)
            HDO201.HO201_DT02 = "O201";                //  4자리 : 서식코드 ==> (V101)

            HDO201.HO201_DT03 = sSAUPNO.PadRight(13);  // 13자리 : 납세자ID --> 사업자등록번호(사업자번호 생성규칙과 무세적 체크를 함) 
            HDO201.HO201_DT04 = "14";                  //  2자리 : 세목구분코드 ‘14’ 
            HDO201.HO201_DT05 = "1";                   //  1자리  신고구분 : ‘1’ 정기, ’2’ 수정, ‘5’ 기한 후 
            HDO201.HO201_DT06 = "8";                   //  1자리  납세자구분: 법인
            HDO201.HO201_DT07 = this.DTP01_EMYYMM.GetString().ToString().Substring(0, 6);   // 6자리 제출연월
            HDO201.HO201_DT08 = this.DTP01_WREYYMM.GetString().ToString().Substring(0, 6);  // 6자리 지급연월
            HDO201.HO201_DT09 = this.DTP01_WREYYMM.GetString().ToString().Substring(0, 6);  // 6자리 귀속연월
            HDO201.HO201_DT10 = "TYC2921";              // 20자리 사용자 ID (본점-TYC2921 , 본점-TYC2922) (재정리 됨)
            HDO201.HO201_DT11 = sFill.PadRight(30);     // 30자리 세무대리인성명 (공백)
            HDO201.HO201_DT12 = sFill.PadRight(06);     //  6자리 세무대리인관리번호 (공백)
            HDO201.HO201_DT13 = sFill.PadRight(14);     // 14자리 세무대리인전화번호1 (공백) 
            HDO201.HO201_DT14 = sSANGHO;                // 30자리 상호명 (재정리 됨) 
            HDO201.HO201_DT15 = sVNADDRS;// sFill.PadRight(70);     // 70자리 사업장소재지 (공백)
            HDO201.HO201_DT16 = sTELNUM; // sFill.PadRight(14);     // 14자리 사업장전화번호 (공백)
            HDO201.HO201_DT17 = sNAMENM;                // 30자리 대표자명 (재정리 됨)
            HDO201.HO201_DT18 = sWRETURNGB;             //  1자리 원천신고구분
            HDO201.HO201_DT19 = sFill.PadRight(10);     // 10자리 세무대리인사업자번호
            if (sWDETAILGB == "1")
            {
                HDO201.HO201_DT20 = sYEAR;              //  4자리 귀속년도
                HDO201.HO201_DT21 = sWDETAILGB;         //  1자리 신고서부표여부 (신고서(뒤쪽)작성여부)
            }
            else
            {
                HDO201.HO201_DT20 = sFill.PadRight(04); //  4자리 귀속년도
                HDO201.HO201_DT21 = sFill.PadRight(01); //  1자리 신고서부표여부 (신고서(뒤쪽)작성여부)
            }
            HDO201.HO201_DT22 = sWYEARTXGB;             //  1자리 연말정산여부
            HDO201.HO201_DT23 = DateTime.Now.ToString("yyyyMMdd");     // 8자리 작성일자
            HDO201.HO201_DT24 = "9000";                 //  4자리 세무프로그램코드
            HDO201.HO201_DT25 = sFill.PadRight(50);     // 50자리 사업장소재지 (공백)

            HDO201.HO201_DT26 = sWDISOISGB;             //  1자리 소득처분여부
            HDO201.HO201_DT27 = sWREQUESGB;             //  1자리 환급신청여부
            HDO201.HO201_DT28 = sFill.PadRight(03);     //  3자리 예입처(은행코드)
            HDO201.HO201_DT29 = sFill.PadRight(20);     // 20자리 계좌번호
            HDO201.HO201_DT30 = sWSUCCESGB;             //  1자리 차기월환급세액 승계명세서
            HDO201.HO201_DT31 = "N";                    //  1자리 기납부세액명세서
            HDO201.HO201_DT32 = sFill.PadRight(19);     // 19자리 공란

            // 레코드 세팅 작업(자리수)
            sData = HDO201.HO201_DT01;
            sData += HDO201.HO201_DT02;
            sData += HDO201.HO201_DT03;
            sData += HDO201.HO201_DT04;
            sData += HDO201.HO201_DT05;
            sData += HDO201.HO201_DT06;
            sData += HDO201.HO201_DT07;
            sData += HDO201.HO201_DT08;
            sData += HDO201.HO201_DT09;

            sStrTemp = HDO201.HO201_DT10.Trim(); // 10 사용자ID :  20자리 
            sStrTemp += new String(Convert.ToChar(" "), (20 - Encoding.Default.GetByteCount(HDO201.HO201_DT10.Trim())));
            sData += sStrTemp;

            sData += HDO201.HO201_DT11;
            sData += HDO201.HO201_DT12;
            sData += HDO201.HO201_DT13;

            sStrTemp = HDO201.HO201_DT14.Trim(); // 14 상호(법인명) : 30자리
            sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(HDO201.HO201_DT14.Trim())));
            sData += sStrTemp;

            sStrTemp = HDO201.HO201_DT15.Trim(); // 15 사업장소재지 : 70자리
            sStrTemp += new String(Convert.ToChar(" "), (70 - Encoding.Default.GetByteCount(HDO201.HO201_DT15.Trim())));
            sData += sStrTemp;

            sStrTemp = HDO201.HO201_DT16.Trim(); // 16 사업장전화번호 : 14자리
            sStrTemp += new String(Convert.ToChar(" "), (14 - Encoding.Default.GetByteCount(HDO201.HO201_DT16.Trim())));
            sData += sStrTemp;

            sStrTemp = HDO201.HO201_DT17.Trim(); // 17 성명(대표자명) : 30자리
            sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(HDO201.HO201_DT17.Trim())));
            sData += sStrTemp;

            sData += HDO201.HO201_DT18;  // 18 원천신고구분 : 1자리 
            sData += HDO201.HO201_DT19;
            sData += HDO201.HO201_DT20;
            sData += HDO201.HO201_DT21;
            sData += HDO201.HO201_DT22;
            sData += HDO201.HO201_DT23;
            sData += HDO201.HO201_DT24;
            sData += HDO201.HO201_DT25;
            sData += HDO201.HO201_DT26;
            sData += HDO201.HO201_DT27;
            sData += HDO201.HO201_DT28;
            sData += HDO201.HO201_DT29;
            sData += HDO201.HO201_DT30;
            sData += HDO201.HO201_DT31;
            sData += HDO201.HO201_DT32;

            sw.WriteLine(sData); */


           /*
           //2015년 변경
           struct_HO201 HDO201 = new struct_HO201();

           // 제출자 인적사항 조회 
           this.DbConnector.CommandClear();
           this.DbConnector.Attach
               (
               "TY_P_AC_4AUA6276",  // AVSUBMITMF , WSUMMARYTF
                this.CBO01_WABRANCH.GetValue().ToString(),   // 사업장(1본점, 2지점)
                this.DTP01_WREYYMM.GetValue().ToString() // 년월
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

               sWRETURNGB = dt_tae.Rows[0]["WRETURNGB"].ToString(); // 18  원천신고구분(1.매월, 2.반기)
               sWDETAILGB = dt_tae.Rows[0]["WDETAILGB"].ToString(); // 21  부표
               sWYEARTXGB = dt_tae.Rows[0]["WYEARTXGB"].ToString(); // 22  연말정산
               sWDISOISGB = dt_tae.Rows[0]["WDISOISGB"].ToString(); // 26  소득처분
               sWREQUESGB = dt_tae.Rows[0]["WREQUESGB"].ToString(); // 27  환급신청
               sWSUCCESGB = dt_tae.Rows[0]["WREQUESGB"].ToString(); // 30  차월이월환급세액 승계명세여부
           }

           HDO201.HO201_DT01 = "21";                  //  2자리 : 자료구분 ==> (21)
           HDO201.HO201_DT02 = "C103900";                //  7자리 : 서식코드 ==> (C103900)

           HDO201.HO201_DT03 = sSAUPNO.PadRight(13);  // 13자리 : 납세자ID --> 사업자등록번호(사업자번호 생성규칙과 무세적 체크를 함) 
           HDO201.HO201_DT04 = "14";                  //  2자리 : 세목구분코드 ‘14’ 
           HDO201.HO201_DT05 = "01";                   // 2자리  신고구분 : ‘01’ 정기
           HDO201.HO201_DT06 = "01";                   // 2자리  신고구분상세코드 : ‘01’ 정기, ’02’ 수정, ‘05’ 기한 후 
           HDO201.HO201_DT07 = "F01";                   // 3자리  신고서종류코드

           HDO201.HO201_DT08 = this.DTP01_WREYYMM.GetString().ToString().Substring(0, 6);  // 6자리 귀속연월
           HDO201.HO201_DT09 = this.DTP01_WREYYMM.GetString().ToString().Substring(0, 6);  // 6자리 지급연월
           HDO201.HO201_DT10 = this.DTP01_EMYYMM.GetString().ToString().Substring(0, 6);   // 6자리 제출연월          
           
           HDO201.HO201_DT11 = "tyc2921";              // 20자리 사용자 ID (본점-TYC2921 , 본점-TYC2922) (재정리 됨)
           HDO201.HO201_DT12 = "FF001";                // 5자리 민원종류코드 FF001:원천징수이행상황신고서 FF002:원천징수이행상황 수정신고서 
           HDO201.HO201_DT13 = sFill.PadRight(10);     // 10자리 세무대리인사업자번호
           HDO201.HO201_DT14 = sFill.PadRight(30);     // 30자리 세무대리인성명 (공백)
           HDO201.HO201_DT15 = sFill.PadRight(06);     //  6자리 세무대리인관리번호 (공백)
           HDO201.HO201_DT16 = sFill.PadRight(14);     // 14자리 세무대리인전화번호1 (공백) 
           HDO201.HO201_DT17 = sSANGHO;                // 30자리 상호명 (재정리 됨) 
           HDO201.HO201_DT18 = sVNADDRS;// sFill.PadRight(70);     // 70자리 사업장소재지 (공백)
           HDO201.HO201_DT19 = sTELNUM; // sFill.PadRight(14);     // 14자리 사업장전화번호 (공백)
           HDO201.HO201_DT20 = sFill.PadRight(50);     // 50자리 전자메일 (공백)
           HDO201.HO201_DT21 = sNAMENM;                // 30자리 대표자명 (재정리 됨)
           HDO201.HO201_DT22 = Set_Fill2(sWRETURNGB);             //  2자리 원천신고구분

           HDO201.HO201_DT23 = sWYEARTXGB;             //  1자리 연말정산여부
           HDO201.HO201_DT24 = sWDISOISGB;             //  1자리 소득처분여부
           HDO201.HO201_DT25 = sWREQUESGB;             //  1자리 환급신청여부
           HDO201.HO201_DT26 = "N";                    //  1자리 일괄납부여부
           HDO201.HO201_DT27 = "N";                    //  1자리 사업자단위과세여부

           HDO201.HO201_DT28 = sWDETAILGB;         //  1자리 신고서부표여부 (신고서(뒤쪽)작성여부)
           
           HDO201.HO201_DT29 = sWSUCCESGB;             //  1자리 차기월환급세액 승계명세서
           HDO201.HO201_DT30 = "N";                    //  1자리 기납부세액명세서 
           HDO201.HO201_DT31 = sFill.PadRight(03);     //  3자리 예입처(은행코드)
           HDO201.HO201_DT32 = sFill.PadRight(20);     // 20자리 계좌번호
           HDO201.HO201_DT33 = DateTime.Now.ToString("yyyyMMdd");     // 8자리 작성일자
           //HDO201.HO201_DT34 = "9000";                 //  4자리 세무프로그램코드          
           if (this.CBO01_WABRANCH.GetValue().ToString() == "1")
           {
               HDO201.HO201_DT34 = "1074";                 //  4자리 세무프로그램코드          
           }
           else
           {
               HDO201.HO201_DT34 = "1075";                 //  4자리 세무프로그램코드          
           }
           HDO201.HO201_DT35 = sFill.PadRight(27);     // 19자리 공란

           // 레코드 세팅 작업(자리수)
           sData = HDO201.HO201_DT01;
           sData += HDO201.HO201_DT02;
           sData += HDO201.HO201_DT03;
           sData += HDO201.HO201_DT04;
           sData += HDO201.HO201_DT05;
           sData += HDO201.HO201_DT06;
           sData += HDO201.HO201_DT07;
           sData += HDO201.HO201_DT08;
           sData += HDO201.HO201_DT09;
           sData += HDO201.HO201_DT10;

           int dd2 = sData.Length;

           sStrTemp = HDO201.HO201_DT11.Trim(); // 11 사용자ID :  20자리 
           sStrTemp += new String(Convert.ToChar(" "), (20 - Encoding.Default.GetByteCount(HDO201.HO201_DT11.Trim())));
           sData += sStrTemp;

           int kk = sStrTemp.Length;

           sData += HDO201.HO201_DT12;
           sData += HDO201.HO201_DT13;

           sStrTemp = HDO201.HO201_DT14.Trim(); // 14 세무대리인성명 : 30자리
           sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(HDO201.HO201_DT14.Trim())));
           sData += sStrTemp;

           sData += HDO201.HO201_DT15;
           sData += HDO201.HO201_DT16;

           sStrTemp = HDO201.HO201_DT17.Trim(); // 17 상호(법인명) : 30자리
           sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(HDO201.HO201_DT17.Trim())));
           sData += sStrTemp;

           int dd3 = sData.Length;

           sStrTemp = HDO201.HO201_DT18.Trim(); // 18 사업장소재지 : 70자리
           sStrTemp += new String(Convert.ToChar(" "), (70 - Encoding.Default.GetByteCount(HDO201.HO201_DT18.Trim())));
           sData += sStrTemp;

           sStrTemp = HDO201.HO201_DT19.Trim(); // 19 사업장전화번호 : 14자리
           sStrTemp += new String(Convert.ToChar(" "), (14 - Encoding.Default.GetByteCount(HDO201.HO201_DT19.Trim())));
           sData += sStrTemp;

           sData += HDO201.HO201_DT20;

           sStrTemp = HDO201.HO201_DT21.Trim(); // 21 성명(대표자명) : 30자리
           sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(HDO201.HO201_DT21.Trim())));
           sData += sStrTemp;


           int dd6 = sData.Length;

           sData += HDO201.HO201_DT22;
           sData += HDO201.HO201_DT23;
           sData += HDO201.HO201_DT24;
           sData += HDO201.HO201_DT25;
           sData += HDO201.HO201_DT26;
           sData += HDO201.HO201_DT27;
           sData += HDO201.HO201_DT28;
           sData += HDO201.HO201_DT29;
           sData += HDO201.HO201_DT30;
           sData += HDO201.HO201_DT31;
           sData += HDO201.HO201_DT32;
           sData += HDO201.HO201_DT33;
           sData += HDO201.HO201_DT34;

           int dd1 = sData.Length;

           sData += HDO201.HO201_DT35;

           sw.WriteLine(sData); 
           
           */

            // 2021년 11월 변경
            string sWREYYMM = string.Empty;
            string sEMYYMM  = string.Empty;

            string sBRANCH = string.Empty;

            sBRANCH = "1";

            struct_HO201 HDO201 = new struct_HO201();

            // 제출자 인적사항 조회 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_4AUA6276",   // AVSUBMITMF , WSUMMARYTF
                 sBRANCH.ToString(),  // 사업장(1본점, 2지점)
                 this.DTP01_WREYYMM.GetValue().ToString() // 년월
                );

            DataTable dt_tae = this.DbConnector.ExecuteDataTable();

            if (dt_tae.Rows.Count > 0)
            {
                sSAUPNO    = dt_tae.Rows[0]["ASMSAUPNO"].ToString(); // 사업자등록번호
                sSANGHO    = dt_tae.Rows[0]["ASMSANGHO"].ToString(); // 상호명
                sNAMENM    = dt_tae.Rows[0]["ASMNAMENM"].ToString(); // 대표자이름
                sCORPNO    = dt_tae.Rows[0]["ASMCORPNO"].ToString(); // 법인번호
                sUPTAE     = dt_tae.Rows[0]["ASMUPTAE"].ToString(); // 업태
                sEVENT     = dt_tae.Rows[0]["ASMEVENT"].ToString(); // 종목
                sTELNUM    = Up_Telnum_Convert(dt_tae.Rows[0]["ASMTELNUM"].ToString().Trim()); // 전화번호
                sVNADDRS   = dt_tae.Rows[0]["ASMVNADDRS"].ToString(); // 사업장주소
                sTAXAREA   = dt_tae.Rows[0]["ASMTAXAREA"].ToString(); // 관할세무소
                sBUSTYPE   = dt_tae.Rows[0]["ASMBUSTYPE"].ToString(); // 업종코드

                sWRETURNGB = dt_tae.Rows[0]["WRETURNGB"].ToString(); // 18  원천신고구분(1.매월, 2.반기)
                sWDETAILGB = dt_tae.Rows[0]["WDETAILGB"].ToString(); // 21  부표
                sWYEARTXGB = dt_tae.Rows[0]["WYEARTXGB"].ToString(); // 22  연말정산
                sWDISOISGB = dt_tae.Rows[0]["WDISOISGB"].ToString(); // 26  소득처분
                sWREQUESGB = dt_tae.Rows[0]["WREQUESGB"].ToString(); // 27  환급신청
                sWSUCCESGB = dt_tae.Rows[0]["WREQUESGB"].ToString(); // 30  차월이월환급세액 승계명세여부
            }

            sWREYYMM = this.DTP01_WREYYMM.GetString().ToString().Substring(0, 6);
            sEMYYMM = this.DTP01_EMYYMM.GetString().ToString().Substring(0, 6);

            HDO201.HO201_DT01 = "21";                                //  2자리 : 자료구분 ==> (21)
            HDO201.HO201_DT02 = "C103900";                           //  7자리 : 서식코드 ==> (C103900)

            HDO201.HO201_DT03 = sSAUPNO.PadRight(13);                // 13자리 : 납세자ID --> 사업자등록번호(사업자번호 생성규칙과 무세적 체크를 함) 
            HDO201.HO201_DT04 = "14";                                //  2자리 : 세목구분코드 ‘14’ 
            HDO201.HO201_DT05 = "01";                                //  2자리 : 신고구분 : ‘01’ 정기
            HDO201.HO201_DT06 = "01";                                //  2자리 : 신고구분상세코드 : ‘01’ 정기, ’02’ 수정, ‘05’ 기한 후 
            HDO201.HO201_DT07 = "F01";                               //  3자리 : 신고서종류코드

            HDO201.HO201_DT08 = sWREYYMM.ToString();                 //  6자리 : 귀속연월
            HDO201.HO201_DT09 = sWREYYMM.ToString();                 //  6자리 : 지급연월
            HDO201.HO201_DT10 = sEMYYMM.ToString();                  //  6자리 : 제출연월          

            HDO201.HO201_DT11 = "tyc2921";                           // 20자리 : 사용자 ID (본점-TYC2921 , 본점-TYC2922) (재정리 됨)
            HDO201.HO201_DT12 = "FF001";                             //  5자리 : 민원종류코드 FF001:원천징수이행상황신고서 FF002:원천징수이행상황 수정신고서 
            HDO201.HO201_DT13 = sFill.PadRight(10);                  // 10자리 : 세무대리인사업자번호
            HDO201.HO201_DT14 = sFill.PadRight(30);                  // 30자리 : 세무대리인성명 (공백)
            HDO201.HO201_DT15 = sFill.PadRight(06);                  //  6자리 : 세무대리인관리번호 (공백)
            HDO201.HO201_DT16 = sFill.PadRight(14);                  // 14자리 : 세무대리인전화번호1 (공백) 
            HDO201.HO201_DT17 = sSANGHO;                             // 30자리 : 상호명 (재정리 됨) 
            HDO201.HO201_DT18 = sVNADDRS;                            // 70자리 : 사업장소재지 (공백)
            HDO201.HO201_DT19 = sTELNUM;                             // 14자리 : 사업장전화번호 (공백)
            HDO201.HO201_DT20 = sFill.PadRight(50);                  // 50자리 : 전자메일 (공백)
            HDO201.HO201_DT21 = sNAMENM;                             // 30자리 : 대표자명 (재정리 됨)
            HDO201.HO201_DT22 = Set_Fill2(sWRETURNGB);               //  2자리 : 원천신고구분

            HDO201.HO201_DT23 = sWYEARTXGB;                          //  1자리 : 연말정산여부
            HDO201.HO201_DT24 = sWDISOISGB;                          //  1자리 : 소득처분여부
            HDO201.HO201_DT25 = sWREQUESGB;                          //  1자리 : 환급신청여부
            HDO201.HO201_DT26 = "N";                                 //  1자리 : 일괄납부여부
            HDO201.HO201_DT27 = "N";                                 //  1자리 : 사업자단위과세여부

            HDO201.HO201_DT28 = sWDETAILGB;                          //  1자리 : 신고서부표여부 (신고서(뒤쪽)작성여부)

            HDO201.HO201_DT29 = sWSUCCESGB;                          //  1자리 : 차기월환급세액 승계명세서
            HDO201.HO201_DT30 = "N";                                 //  1자리 : 기납부세액명세서 
            HDO201.HO201_DT31 = sFill.PadRight(03);                  //  3자리 : 예입처(은행코드)
            HDO201.HO201_DT32 = sFill.PadRight(20);                  // 20자리 : 계좌번호
            HDO201.HO201_DT33 = DateTime.Now.ToString("yyyyMMdd");   //  8자리 : 작성일자

            // 21.11.23일 수정 소스
            HDO201.HO201_DT34 = "1074";                              //  4자리 : 세무프로그램코드
            HDO201.HO201_DT35 = sFill.PadRight(27);                  // 27자리 : 공란

            //HDO201.HO201_DT34 = "9000";                            //  4자리 : 세무프로그램코드    

            // 21.11.23일 원본 소스
            //if (this.CBO01_WABRANCH.GetValue().ToString() == "1")
            //{
            //    HDO201.HO201_DT34 = "1074";                        //  4자리 세무프로그램코드          
            //}
            //else
            //{
            //    HDO201.HO201_DT34 = "1075";                        //  4자리 세무프로그램코드          
            //}

            

            // 레코드 세팅 작업(자리수)
            sData  = HDO201.HO201_DT01;
            sData += HDO201.HO201_DT02;
            sData += HDO201.HO201_DT03;
            sData += HDO201.HO201_DT04;
            sData += HDO201.HO201_DT05;
            sData += HDO201.HO201_DT06;
            sData += HDO201.HO201_DT07;
            sData += HDO201.HO201_DT08;
            sData += HDO201.HO201_DT09;
            sData += HDO201.HO201_DT10;

            int dd2 = sData.Length;

            sStrTemp = HDO201.HO201_DT11.Trim(); // 11 사용자ID :  20자리 
            sStrTemp += new String(Convert.ToChar(" "), (20 - Encoding.Default.GetByteCount(HDO201.HO201_DT11.Trim())));
            sData += sStrTemp;

            int kk = sStrTemp.Length;

            sData += HDO201.HO201_DT12;
            sData += HDO201.HO201_DT13;

            sStrTemp = HDO201.HO201_DT14.Trim(); // 14 세무대리인성명 : 30자리
            sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(HDO201.HO201_DT14.Trim())));
            sData += sStrTemp;

            sData += HDO201.HO201_DT15;
            sData += HDO201.HO201_DT16;

            sStrTemp = HDO201.HO201_DT17.Trim(); // 17 상호(법인명) : 30자리
            sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(HDO201.HO201_DT17.Trim())));
            sData += sStrTemp;

            int dd3 = sData.Length;

            sStrTemp = HDO201.HO201_DT18.Trim(); // 18 사업장소재지 : 70자리
            sStrTemp += new String(Convert.ToChar(" "), (70 - Encoding.Default.GetByteCount(HDO201.HO201_DT18.Trim())));
            sData += sStrTemp;

            sStrTemp = HDO201.HO201_DT19.Trim(); // 19 사업장전화번호 : 14자리
            sStrTemp += new String(Convert.ToChar(" "), (14 - Encoding.Default.GetByteCount(HDO201.HO201_DT19.Trim())));
            sData += sStrTemp;

            sData += HDO201.HO201_DT20;

            sStrTemp = HDO201.HO201_DT21.Trim(); // 21 성명(대표자명) : 30자리
            sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(HDO201.HO201_DT21.Trim())));
            sData += sStrTemp;


            int dd6 = sData.Length;

            sData += HDO201.HO201_DT22;
            sData += HDO201.HO201_DT23;
            sData += HDO201.HO201_DT24;
            sData += HDO201.HO201_DT25;
            sData += HDO201.HO201_DT26;
            sData += HDO201.HO201_DT27;
            sData += HDO201.HO201_DT28;
            sData += HDO201.HO201_DT29;
            sData += HDO201.HO201_DT30;
            sData += HDO201.HO201_DT31;
            sData += HDO201.HO201_DT32;
            sData += HDO201.HO201_DT33;
            sData += HDO201.HO201_DT34;

            int dd1 = sData.Length;

            sData += HDO201.HO201_DT35;

            sw.WriteLine(sData); 

        }
        #endregion

        #region Description : 원천징수이행상황 신고서 레코드 생성 (O201) -- UP_TAX_Create_O201()
        private void UP_TAX_Create_O201(StreamWriter sw)
        {
            string sStrTemp = string.Empty;
            string sData = string.Empty;
            string sFill = string.Empty;
            /*
            struct_DO201 D201 = new struct_DO201();

            // 신고서
            this.DbConnector.CommandClear(); // WSUMMARYTF 
            this.DbConnector.Attach("TY_P_AC_4AUA3274",
                 this.CBO01_WABRANCH.GetValue().ToString(),   // 사업장(1본점, 2지점)
                 this.DTP01_WREYYMM.GetValue().ToString() // 년월
                                    );
            DataTable dt_201 = this.DbConnector.ExecuteDataTable();

            if (dt_201.Rows.Count > 0)
            {
                D201.DO201_DT01 = dt_201.Rows[0]["VSDT01"].ToString(); // 2자리 (문자) : 자료구분  ==> (27)
                D201.DO201_DT02 = dt_201.Rows[0]["VSDT02"].ToString(); // 4자리 (문자) : 서식코드  ==> (O201)
                D201.DO201_DT03 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT03"].ToString().Trim(), 15);    // 15자리 ○12전월미환급세액
                D201.DO201_DT04 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT04"].ToString().Trim(), 15);    // 15자리 ○13기환급신청세액
                D201.DO201_DT05 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT05"].ToString().Trim(), 15);    // 15자리 ○14차감후잔액(원천)
                D201.DO201_DT06 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT06"].ToString().Trim(), 15);    // 15자리 ○15일반환급세액
                D201.DO201_DT07 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT07"].ToString().Trim(), 15);    // 15자리 ○16신탁재산세액
                D201.DO201_DT08 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT08"].ToString().Trim(), 15);    // 15자리 ○18조정대상환급세액
                D201.DO201_DT09 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT09"].ToString().Trim(), 15);    // 15자리 ○19당월조정환급세액
                D201.DO201_DT10 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT10"].ToString().Trim(), 15);    // 15자리 ○20차월이월환급세액
                D201.DO201_DT11 = dt_201.Rows[0]["VSDT11"].ToString().Trim();                            //  1자리 일괄납부여부
                D201.DO201_DT12 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT12"].ToString().Trim(), 15);    // 15자리 ○17 그밖의 환급세액금융회사 등
                D201.DO201_DT13 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT13"].ToString().Trim(), 15);    // 15자리 ○21환급신청액
                D201.DO201_DT14 = dt_201.Rows[0]["VSDT14"].ToString().Trim();                            //  1자리 사업자단위과세여부
                D201.DO201_DT15 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT15"].ToString().Trim(), 15);    // 15자리 ○17 그밖의 환급세액합병 등
                D201.DO201_DT16 = sFill.PadRight(7);    //  7자리 공란

                // 레코드 세팅 작업(자리수)
                sData = D201.DO201_DT01;
                sData += D201.DO201_DT02;
                sData += D201.DO201_DT03;
                sData += D201.DO201_DT04;
                sData += D201.DO201_DT05;
                sData += D201.DO201_DT06;
                sData += D201.DO201_DT07;
                sData += D201.DO201_DT08;
                sData += D201.DO201_DT09;
                sData += D201.DO201_DT10;
                sData += D201.DO201_DT11;
                sData += D201.DO201_DT12;
                sData += D201.DO201_DT13;
                sData += D201.DO201_DT14;
                sData += D201.DO201_DT15;
                sData += D201.DO201_DT16;

                sw.WriteLine(sData);

            }; */

            /*
            //2015년 변경
            struct_DO201 D201 = new struct_DO201();

            // 신고서
            this.DbConnector.CommandClear(); // WSUMMARYTF 
            this.DbConnector.Attach("TY_P_AC_4AUA3274",
                 this.CBO01_WABRANCH.GetValue().ToString(),   // 사업장(1본점, 2지점)
                 this.DTP01_WREYYMM.GetValue().ToString() // 년월
                                    );
            DataTable dt_201 = this.DbConnector.ExecuteDataTable();

            if (dt_201.Rows.Count > 0)
            {
                D201.DO201_DT01 = dt_201.Rows[0]["VSDT01"].ToString(); // 2자리 (문자) : 자료구분  ==> (22)
                D201.DO201_DT02 = dt_201.Rows[0]["VSDT02"].ToString(); // 7자리 (문자) : 서식코드  ==> (C103900)
                D201.DO201_DT03 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT03"].ToString().Trim(), 15);    // 15자리 ○12전월미환급세액
                D201.DO201_DT04 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT04"].ToString().Trim(), 15);    // 15자리 ○13기환급신청세액
                D201.DO201_DT05 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT05"].ToString().Trim(), 15);    // 15자리 ○14차감후잔액(원천)
                D201.DO201_DT06 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT06"].ToString().Trim(), 15);    // 15자리 ○15일반환급세액
                D201.DO201_DT07 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT07"].ToString().Trim(), 15);    // 15자리 ○16신탁재산세액
                D201.DO201_DT08 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT12"].ToString().Trim(), 15);    // 15자리 ○17 그밖의 환급세액금융회사 등
                D201.DO201_DT09 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT15"].ToString().Trim(), 15);    // 15자리 ○17 그밖의 환급세액합병 등
                D201.DO201_DT10 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT08"].ToString().Trim(), 15);    // 15자리 ○18조정대상환급세액
                D201.DO201_DT11 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT09"].ToString().Trim(), 15);    // 15자리 ○19당월조정환급세액
                D201.DO201_DT12 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT10"].ToString().Trim(), 15);    // 15자리 ○20차월이월환급세액
                D201.DO201_DT13 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT13"].ToString().Trim(), 15);    // 15자리 ○21환급신청액                

                D201.DO201_DT14 = UP_Minus_Conv_Fill("0", 15);                                           // 15자리 승계대상합계 차월이월환급액
                
                D201.DO201_DT15 = sFill.PadRight(11);    //  11자리 공란

                // 레코드 세팅 작업(자리수)
                sData = D201.DO201_DT01;
                sData += D201.DO201_DT02;
                sData += D201.DO201_DT03;
                sData += D201.DO201_DT04;
                sData += D201.DO201_DT05;
                sData += D201.DO201_DT06;
                sData += D201.DO201_DT07;
                sData += D201.DO201_DT08;
                sData += D201.DO201_DT09;
                sData += D201.DO201_DT10;
                sData += D201.DO201_DT11;
                sData += D201.DO201_DT12;
                sData += D201.DO201_DT13;
                sData += D201.DO201_DT14;
                sData += D201.DO201_DT15;

                sw.WriteLine(sData);

            }
            */

            // 2021년 11월 변경
            string sBRANCH = string.Empty;

            sBRANCH = "1";

            struct_DO201 D201 = new struct_DO201();

            // 신고서
            this.DbConnector.CommandClear(); // WSUMMARYTF 
            this.DbConnector.Attach("TY_P_AC_BBJH0766", sBRANCH.ToString(),                        // 사업장(1본점, 2지점)
                                                        this.DTP01_WREYYMM.GetValue().ToString()   // 년월
                                                        );
            DataTable dt_201 = this.DbConnector.ExecuteDataTable();

            if (dt_201.Rows.Count > 0)
            {
                D201.DO201_DT01 = "22";                                                                  //  2자리 : 자료구분  ==> (22)
                D201.DO201_DT02 = "C103900";                                                             //  7자리 : 서식코드  ==> (C103900)

                D201.DO201_DT03 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT03"].ToString().Trim(), 15);    // 15자리 : ○12전월미환급세액
                D201.DO201_DT04 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT04"].ToString().Trim(), 15);    // 15자리 : ○13기환급신청세액
                D201.DO201_DT05 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT05"].ToString().Trim(), 15);    // 15자리 : ○14차감후잔액(원천)
                D201.DO201_DT06 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT06"].ToString().Trim(), 15);    // 15자리 : ○15일반환급세액
                D201.DO201_DT07 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT07"].ToString().Trim(), 15);    // 15자리 : ○16신탁재산세액
                D201.DO201_DT08 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT12"].ToString().Trim(), 15);    // 15자리 : ○17 그밖의 환급세액금융회사 등
                D201.DO201_DT09 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT15"].ToString().Trim(), 15);    // 15자리 : ○17 그밖의 환급세액합병 등
                D201.DO201_DT10 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT08"].ToString().Trim(), 15);    // 15자리 : ○18조정대상환급세액
                D201.DO201_DT11 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT09"].ToString().Trim(), 15);    // 15자리 : ○19당월조정환급세액
                D201.DO201_DT12 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT10"].ToString().Trim(), 15);    // 15자리 : ○20차월이월환급세액
                D201.DO201_DT13 = UP_Minus_Conv_Fill(dt_201.Rows[0]["VSDT13"].ToString().Trim(), 15);    // 15자리 : ○21환급신청액
                D201.DO201_DT14 = UP_Minus_Conv_Fill("0", 15);                                           // 15자리 : 승계대상합계 차월이월환급액

                D201.DO201_DT15 = sFill.PadRight(11);                                                    // 11자리 : 공란

                // 레코드 세팅 작업(자리수)
                sData  = D201.DO201_DT01;
                sData += D201.DO201_DT02;
                sData += D201.DO201_DT03;
                sData += D201.DO201_DT04;
                sData += D201.DO201_DT05;
                sData += D201.DO201_DT06;
                sData += D201.DO201_DT07;
                sData += D201.DO201_DT08;
                sData += D201.DO201_DT09;
                sData += D201.DO201_DT10;
                sData += D201.DO201_DT11;
                sData += D201.DO201_DT12;
                sData += D201.DO201_DT13;
                sData += D201.DO201_DT14;
                sData += D201.DO201_DT15;

                sw.WriteLine(sData);

            }
        }
        #endregion 

        #region Description : 원천징수이행상황 신고서 세부 레코드 생성 (O201) -- UP_TAX_Create_SO201()
        private void UP_TAX_Create_SO201(StreamWriter sw)
        {
            string sStrTemp = string.Empty;
            string sData = string.Empty;
            string sFill = string.Empty;

            /*
            struct_SO201 SD201 = new struct_SO201();

            //// 신고서
            this.DbConnector.CommandClear(); // WSUMMARYTF 
            this.DbConnector.Attach("TY_P_AC_4AUA5275",
                 this.CBO01_WABRANCH.GetValue().ToString(),   // 사업장(1본점, 2지점)
                 this.DTP01_WREYYMM.GetValue().ToString() // 년월
                                    );
            DataTable dt_201 = this.DbConnector.ExecuteDataTable();

            if (dt_201.Rows.Count > 0)
            {
                for (int i = 0; i < dt_201.Rows.Count; i++)
                {
                    SD201.SO201_DT01 = dt_201.Rows[i]["VSDT01"].ToString(); // 2자리 (문자) : 자료구분  ==> (28)
                    SD201.SO201_DT02 = dt_201.Rows[i]["VSDT02"].ToString(); // 4자리 (문자) : 서식코드  ==> (O201)
                    SD201.SO201_DT03 = dt_201.Rows[i]["VSDT03"].ToString().Trim();    // 3자리 원천세목코드
                    SD201.SO201_DT04 = UP_Minus_Conv_Fill(dt_201.Rows[i]["VSDT04"].ToString().Trim(), 15);    // 15자리 인원
                    SD201.SO201_DT05 = UP_Minus_Conv_Fill(dt_201.Rows[i]["VSDT05"].ToString().Trim(), 15);    // 15자리 총지급액
                    SD201.SO201_DT06 = UP_Minus_Conv_Fill(dt_201.Rows[i]["VSDT06"].ToString().Trim(), 15);    // 15자리 징수세액(소득세 등)
                    SD201.SO201_DT07 = UP_Minus_Conv_Fill(dt_201.Rows[i]["VSDT07"].ToString().Trim(), 15);    // 15자리 징수세액(농특세)
                    SD201.SO201_DT08 = UP_Minus_Conv_Fill(dt_201.Rows[i]["VSDT08"].ToString().Trim(), 15);    // 15자리 가산세
                    SD201.SO201_DT09 = UP_Minus_Conv_Fill(dt_201.Rows[i]["VSDT09"].ToString().Trim(), 15);    // 15자리 당월조정환급세액
                    SD201.SO201_DT10 = UP_Minus_Conv_Fill(dt_201.Rows[i]["VSDT10"].ToString().Trim(), 15);    // 15자리 납부세액(소득세 등)
                    SD201.SO201_DT11 = UP_Minus_Conv_Fill(dt_201.Rows[i]["VSDT11"].ToString().Trim(), 15);    // 15자리 납부세액(농특세)
                    SD201.SO201_DT12 = sFill.PadRight(21);    //  21자리 공란

                    // 레코드 세팅 작업(자리수)
                    sData = SD201.SO201_DT01;
                    sData += SD201.SO201_DT02;
                    sData += SD201.SO201_DT03;
                    sData += SD201.SO201_DT04;
                    sData += SD201.SO201_DT05;
                    sData += SD201.SO201_DT06;
                    sData += SD201.SO201_DT07;
                    sData += SD201.SO201_DT08;
                    sData += SD201.SO201_DT09;
                    sData += SD201.SO201_DT10;
                    sData += SD201.SO201_DT11;
                    sData += SD201.SO201_DT12;

                    sw.WriteLine(sData);
                }

            }; */

            /*
            //2015년 변경
            struct_SO201 SD201 = new struct_SO201();

            //// 신고서
            this.DbConnector.CommandClear(); // WSUMMARYTF 
            this.DbConnector.Attach("TY_P_AC_4AUA5275",
                 this.CBO01_WABRANCH.GetValue().ToString(),   // 사업장(1본점, 2지점)
                 this.DTP01_WREYYMM.GetValue().ToString() // 년월
                                    );
            DataTable dt_201 = this.DbConnector.ExecuteDataTable();

            if (dt_201.Rows.Count > 0)
            {
                for (int i = 0; i < dt_201.Rows.Count; i++)
                {
                    SD201.SO201_DT01 = dt_201.Rows[i]["VSDT01"].ToString(); // 2자리 (문자) : 자료구분  ==> (23)
                    SD201.SO201_DT02 = dt_201.Rows[i]["VSDT02"].ToString(); // 7자리 (문자) : 서식코드  ==> (C103900)
                    SD201.SO201_DT03 = dt_201.Rows[i]["VSDT03"].ToString().Trim();    // 3자리 원천세목코드

                   
                        SD201.SO201_DT04 = UP_Minus_Conv_Fill(dt_201.Rows[i]["VSDT04"].ToString().Trim(), 15);    // 15자리 인원
                        SD201.SO201_DT05 = UP_Minus_Conv_Fill(dt_201.Rows[i]["VSDT05"].ToString().Trim(), 15);    // 15자리 총지급액
                        SD201.SO201_DT06 = UP_Minus_Conv_Fill(dt_201.Rows[i]["VSDT06"].ToString().Trim(), 15);    // 15자리 징수세액(소득세 등)
                        SD201.SO201_DT07 = UP_Minus_Conv_Fill(dt_201.Rows[i]["VSDT07"].ToString().Trim(), 15);    // 15자리 징수세액(농특세)
                        SD201.SO201_DT08 = UP_Minus_Conv_Fill(dt_201.Rows[i]["VSDT08"].ToString().Trim(), 15);    // 15자리 가산세
                        SD201.SO201_DT09 = UP_Minus_Conv_Fill(dt_201.Rows[i]["VSDT09"].ToString().Trim(), 15);    // 15자리 당월조정환급세액
                        SD201.SO201_DT10 = UP_Minus_Conv_Fill(dt_201.Rows[i]["VSDT10"].ToString().Trim(), 15);    // 15자리 납부세액(소득세 등)
                        SD201.SO201_DT11 = UP_Minus_Conv_Fill(dt_201.Rows[i]["VSDT11"].ToString().Trim(), 15);    // 15자리 납부세액(농특세)
                        SD201.SO201_DT12 = sFill.PadRight(18);    //  18자리 공란
                    
                    

                    // 레코드 세팅 작업(자리수)
                    sData = SD201.SO201_DT01;
                    sData += SD201.SO201_DT02;
                    sData += SD201.SO201_DT03;
                    sData += SD201.SO201_DT04;
                    sData += SD201.SO201_DT05;
                    sData += SD201.SO201_DT06;
                    sData += SD201.SO201_DT07;
                    sData += SD201.SO201_DT08;
                    sData += SD201.SO201_DT09;
                    sData += SD201.SO201_DT10;
                    sData += SD201.SO201_DT11;
                    sData += SD201.SO201_DT12;

                    sw.WriteLine(sData);
                }

            };
            */

            // 2021년 11월 변경
            string sBRANCH = string.Empty;

            sBRANCH = "1";

            struct_SO201 SD201 = new struct_SO201();

            //// 신고서
            this.DbConnector.CommandClear(); // WSUMMARYTF 
            this.DbConnector.Attach("TY_P_AC_BBJH9767", sBRANCH.ToString(),                      // 사업장(1본점, 2지점)
                                                        this.DTP01_WREYYMM.GetValue().ToString() // 년월
                                                        );
            DataTable dt_201 = this.DbConnector.ExecuteDataTable();

            if (dt_201.Rows.Count > 0)
            {
                for (int i = 0; i < dt_201.Rows.Count; i++)
                {
                    SD201.SO201_DT01 = dt_201.Rows[i]["VSDT01"].ToString();                                   //  2자리 : (문자) : 자료구분  ==> (23)
                    SD201.SO201_DT02 = dt_201.Rows[i]["VSDT02"].ToString();                                   //  7자리 : (문자) : 서식코드  ==> (C103900)
                    SD201.SO201_DT03 = dt_201.Rows[i]["VSDT03"].ToString().Trim();                            //  3자리 : 원천징수소득코드

                    SD201.SO201_DT04 = UP_Minus_Conv_Fill(dt_201.Rows[i]["VSDT04"].ToString().Trim(), 15);    // 15자리 : 인원
                    SD201.SO201_DT05 = UP_Minus_Conv_Fill(dt_201.Rows[i]["VSDT05"].ToString().Trim(), 15);    // 15자리 : 총지급액
                    SD201.SO201_DT06 = UP_Minus_Conv_Fill(dt_201.Rows[i]["VSDT06"].ToString().Trim(), 15);    // 15자리 : 징수세액(소득세 등)
                    SD201.SO201_DT07 = UP_Minus_Conv_Fill(dt_201.Rows[i]["VSDT07"].ToString().Trim(), 15);    // 15자리 : 징수세액(농특세)
                    SD201.SO201_DT08 = UP_Minus_Conv_Fill(dt_201.Rows[i]["VSDT08"].ToString().Trim(), 15);    // 15자리 : 가산세
                    SD201.SO201_DT09 = UP_Minus_Conv_Fill(dt_201.Rows[i]["VSDT09"].ToString().Trim(), 15);    // 15자리 : 당월조정환급세액
                    SD201.SO201_DT10 = UP_Minus_Conv_Fill(dt_201.Rows[i]["VSDT10"].ToString().Trim(), 15);    // 15자리 : 납부세액(소득세 등)
                    SD201.SO201_DT11 = UP_Minus_Conv_Fill(dt_201.Rows[i]["VSDT11"].ToString().Trim(), 15);    // 15자리 : 납부세액(농특세)

                    SD201.SO201_DT12 = sFill.PadRight(18);                                                    // 18자리 : 공란

                    // 레코드 세팅 작업(자리수)
                    sData  = SD201.SO201_DT01;
                    sData += SD201.SO201_DT02;
                    sData += SD201.SO201_DT03;
                    sData += SD201.SO201_DT04;
                    sData += SD201.SO201_DT05;
                    sData += SD201.SO201_DT06;
                    sData += SD201.SO201_DT07;
                    sData += SD201.SO201_DT08;
                    sData += SD201.SO201_DT09;
                    sData += SD201.SO201_DT10;
                    sData += SD201.SO201_DT11;
                    sData += SD201.SO201_DT12;

                    sw.WriteLine(sData);
                }
            }
        }
        #endregion 

        // --- 생성 끝 --- //

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

        //---- 구조 정보 선언 ----//
        #region Description : 신고서 구조 정보 선언 (HEAD)
        public struct struct_HO201
        {
            // 원천징수이행상황 신고서
            // 1) 신고서 Head 레코드
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
            public string HO201_DT16;          //1자리
            public string HO201_DT17;          //1자리
            public string HO201_DT18;          //1자리
            public string HO201_DT19;          //1자리
            public string HO201_DT20;          //1자리
            public string HO201_DT21;          //1자리
            public string HO201_DT22;          //1자리
            public string HO201_DT23;          //1자리
            public string HO201_DT24;          //1자리
            public string HO201_DT25;          //1자리
            public string HO201_DT26;          //1자리
            public string HO201_DT27;          //1자리
            public string HO201_DT28;          //1자리
            public string HO201_DT29;          //1자리
            public string HO201_DT30;          //1자리
            public string HO201_DT31;          //1자리
            public string HO201_DT32;
            public string HO201_DT33;
            public string HO201_DT34;
            public string HO201_DT35;          

        }
        #endregion

        #region Description : 원천징수이행상황 신고서 구조 정보 선언 (DO201)
        public struct struct_DO201
        {
            // 2) 원천징수이행상황 신고서 레코드(수정신고서의 원천징수이행상황 신고서레코드_당초)
            public string DO201_DT01;          // 2자리
            public string DO201_DT02;          // 4자리
            public string DO201_DT03;          //15자리
            public string DO201_DT04;          //15자리
            public string DO201_DT05;          //15자리
            public string DO201_DT06;          //15자리
            public string DO201_DT07;          //15자리
            public string DO201_DT08;          //15자리
            public string DO201_DT09;          //15자리
            public string DO201_DT10;          //15자리
            public string DO201_DT11;          // 1자리
            public string DO201_DT12;          //15자리
            public string DO201_DT13;          //15자리
            public string DO201_DT14;          // 1자리
            public string DO201_DT15;          //15자리
            public string DO201_DT16;          // 7자리 : 공란
        }
        #endregion

        #region Description : 원천징수이행상황 신고서 세부 구조 정보 선언 (SO201)
        public struct struct_SO201
        {
            // 3)  원천징수이행상황 신고서 세부 레코드(수정신고서의 원천징수이행상황 신고서 세부 레코드_당초)
            public string SO201_DT01;          //   2자리
            public string SO201_DT02;          //   4자리
            public string SO201_DT03;          //   3자리
            public string SO201_DT04;          //  15자리
            public string SO201_DT05;          //  15자리
            public string SO201_DT06;          //  15자리
            public string SO201_DT07;          //  15자리
            public string SO201_DT08;          //  15자리 
            public string SO201_DT09;          //  15자리
            public string SO201_DT10;          //  15자리
            public string SO201_DT11;          //  15자리
            public string SO201_DT12;          //  21자리 : 공란

        }
        #endregion

    }
}