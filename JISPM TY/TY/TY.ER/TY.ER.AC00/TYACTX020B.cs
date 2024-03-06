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
    /// 부가세 전산신고 생성 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2014.01.20 16:13
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_3CQ5Y873 : 부가세 신고 거래처코드 구하기
    ///  TY_P_AC_41MB3140 : 신고서 전산매체 생성_101
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2B77B165 : 파일을 다운 작업을 하시겠습니까?
    ///  TY_M_AC_3CR9M876 : 부가세 옵션 자료가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  DWN : 다운
    ///  INQOPTION : 조회구분
    ///  INQOPTION2 : 조회구분
    ///  VNGUBUN : 구분
    ///  ELXYYMM : 기준년도
    /// </summary>
    public partial class TYACTX020B : TYBase
    {
        public TYACTX020B()
        {
            InitializeComponent();
        }

        #region Description : Page_Load
        private void TYACTX020B_Load(object sender, System.EventArgs e)
        {

            this.BTN61_DWN.ProcessCheck += new TButton.CheckHandler(BTN61_DWN_ProcessCheck);

            this.DTP01_ELXYYMM.SetValue(DateTime.Now.ToString("yyyy"));

            UP_Cookie_Load();
            
        }
        #endregion

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            UP_Cookie_Save();

            this.Close();
        }
        #endregion

        #region Description : DOWN 버튼 이벤트
        private void BTN61_DWN_Click(object sender, EventArgs e)
        {
            UP_TAXFile_Create();

            UP_Cookie_Save();
        }
        #endregion


        #region Description : 파일 다운 ProcessCheck 이벤트
        private void BTN61_DWN_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

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

            // 2.마감 체크 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_42B8L317", // AVDECLMF
                this.DTP01_ELXYYMM.GetValue().ToString(),
                this.CBO01_VNGUBUN.GetValue().ToString(),
                getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),
                getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)
                );

            if (this.DbConnector.ExecuteDataTable().Rows.Count == 0)
            {
                this.ShowCustomMessage("신고서 마감 미확정 되었습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                this.SetFocus(this.DTP01_ELXYYMM);
                return;
            }


           // 3.제출자 인적사항 체크 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_42B67344",  // AVSUBMITMF 
                this.DTP01_ELXYYMM.GetValue().ToString(),
                getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  //신고구분(1기, 2기)
                getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),  // 확정구분(1.예정, 2.확정)
                this.CBO01_VNGUBUN.GetValue().ToString()   // 사업장(1본점, 2지점)
                );

            DataTable dt_tae = this.DbConnector.ExecuteDataTable();

            if (dt_tae.Rows.Count == 0)
            {
                this.ShowCustomMessage("제출자 인적사항 미등록 확인 하세요 ", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                this.SetFocus(this.DTP01_ELXYYMM);
                return;
            }

            if (!this.ShowMessage("TY_M_AC_2B77B165"))
            {
                e.Successed = false;
                return;
            }

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

            // 구분(1.본점, 2.지점)에 따라 파일이름을 각각 설정한다.
            string sFile_Name = string.Empty;
            string sPath = string.Empty;
            if (this.CBO01_VNGUBUN.GetValue().ToString() == "1")
            {
                sPath = @"C:\ERSDATA\6108110449.101";
                sFile_Name = "6108110449.101";

                if (File.Exists("C:\\ERSDATA\\6108110449.101"))
                {
                    File.Delete("C:\\ERSDATA\\6108110449.101");
                }
            }
            else
            {
                sPath = @"C:\ERSDATA\1058516181.101";
                sFile_Name = "1058516181.101";

                if (File.Exists("C:\\ERSDATA\\1058516181.101"))
                {
                    File.Delete("C:\\ERSDATA\\1058516181.101");
                }
            };

            StreamWriter sw = new StreamWriter(sPath, false, Encoding.Default);

            string sRecord = string.Empty;

            // 서식코드 V101 : 일반과세자 신고서 레코드(HEAD) -- [길이 : 600]
            // 11 : V101
            UP_TAX_Create_HEAD_V101(sw);

            // 서식코드 V101 : 일반과세자 신고서 레코드(수정신고.경정청구의 일반과세자 시고서 레코드_당초)   -- [길이 : 1900]
            //  17 : V101
            UP_TAX_Create_V101(sw);

            // 서식코드 V101 : 부가가치세수입금액등(과세표준명세, 면세수입금액)  -- 여러개 처리 -- [길이 : 150] 
            // 15 : V101
            UP_TAX_Create_CV101(sw);

            // 서식코드 V101 : 부가가치세 공제감면 신고서(2015년 신규)   -- 여러개 처리 -- [길이 : 100] 
            // 14 : I103200
            UP_TAX_Create_CV102(sw);

            // 서식코드 V106 : 영세율 첨부서류제출 명세서(여러개 처리)  -- [길이 : 250]
            // 17 : V106 
            UP_TAX_Create_V106(sw);

            // 서식코드 V115 : 사업장별 부가가체세 과세표준 및 납부세액(환급세액)신고명세서  -- [길이 : 300,300] ----> 총괄납부사업자가 아닌경우 생성안함
            // 17 : V115
            UP_TAX_Create_V115(sw);

            // 서식코드 - V164 : 신용카드매출전표등수취명세서(갑,을) -- 여러개 처리 -- [길이 : HL:40 , DL:140 , TL:140]
            // HL:40 , DL:140 , TL:140
            UP_TAX_Create_V164(sw);

            // 서식코드 - V153 : 공제받지못할매입세액 명세서 -- [길이 : 200,100,100,100,100]
            // 17 : V153
            // 18 : V153
            UP_TAX_Create_V153(sw);

            // 세금계산서합계표
            // 서식코드 - V104 , V105 : 매출처별 세금계산서 합계표(갑,을) , 매입처별 세금계산서 합계표(갑,을) - (여러개 처리)  -- [길이 : 170]
            // 7 , 5, 2, 4 , 6
            UP_TAX_Create_V104(sw);

            // 계산서합계표
            // 서식코드 - V110 , V109 : 매출처별계산서 합계표 , 매입처별계산서 합계표  - (여러개 처리) -- [길이 : 230]
            // A ,B ,C ,D ,E
            UP_TAX_Create_V109(sw);

            // 서식코드 - V141 : 수출실적명세서 -- [길이 : A:180 , B:180 , C:180]
            // A ,B ,C
            UP_TAX_Create_V141(sw);

            // 서식코드 - V149 : 건물 등 감가삼각자산 취득명세서 -- [길이 : 200]
            // 17 : V149
            UP_TAX_Create_V149(sw);

            // 서식코드 - V177 : 영세율매출명세서 -- [길이 : 400]
            // 17 : V177
            // 2013.7.1 이후 과세기간 신고분부터 적용
            if (Convert.ToDouble(this.DTP01_ELXYYMM.GetValue().ToString().Substring(0, 4)) > 2013)
            {
                UP_TAX_Create_V177(sw);
            }
            else
            {
                if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1) == "2")  // 2013.7.1 이후 과세기간 신고분부터 적용
                {
                    UP_TAX_Create_V177(sw);
                }
            }

            sw.Close();


            this.ShowMessage("TY_M_GB_25UAA711");
        }
        #endregion

        // --- 생성 시작--- //

        #region Description : 일반과세자 신고서 레코드 생성 (HEAD V101) -- UP_TAX_Create_HEAD_V101() -- 세무프로그램 코드 (국세청 문의후 수정)
        private void UP_TAX_Create_HEAD_V101(StreamWriter sw)
        {
            string sYEAR = string.Empty;
            string s시작년월일 = string.Empty;
            string s종료년월일 = string.Empty;
            string sData = string.Empty;
            string sStrTemp = string.Empty;
            string sFill = string.Empty;

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

            sYEAR = this.DTP01_ELXYYMM.GetValue().ToString().Substring(0, 4);

            struct_HV101 HDV101 = new struct_HV101();

            // 제출자 인적사항 조회 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_42B67344",  // AVSUBMITMF 
                this.DTP01_ELXYYMM.GetValue().ToString(),
                getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  //신고구분(1기, 2기)
                getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),  // 확정구분(1.예정, 2.확정)
                this.CBO01_VNGUBUN.GetValue().ToString()   // 사업장(1본점, 2지점)
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

            if (this.CBO01_VNGUBUN.GetValue().ToString() == "1")
            {
                /*
                HDV101.HV101_DT01 = "11";                  //  2자리 : 자료구분 ==> (11)
                HDV101.HV101_DT02 = "V101";                //  4자리 : 서식코드 ==> (V101)
                HDV101.HV101_DT03 = sSAUPNO.PadRight(13);  // 13자리 : 납세자ID --> 사업자등록번호(사업자번호 생성규칙과 무세적 체크를 함) 
                HDV101.HV101_DT04 = "41";                  //  2자리 : 세목구분코드 ‘41’ 부가가치세
                if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2) == "1") // 확정구분(1.예정, 2.확정)
                {
                    HDV101.HV101_DT05 = "3";          // 1자리
                }
                else
                {
                    HDV101.HV101_DT05 = "1";          // 1자리
                };

                HDV101.HV101_DT06 = "8";          // 1자리 -- 납세자구분

                if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1) == "1") // 신고구분(1기, 2기)
                {
                    HDV101.HV101_DT07 = this.DTP01_ELXYYMM.GetString().ToString().Substring(0, 4) + "01";  // 6자리
                }
                else
                {
                    HDV101.HV101_DT07 = this.DTP01_ELXYYMM.GetString().ToString().Substring(0, 4) + "02";  // 6자리
                }

                // 신고차수
                if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1) == "1") // 신고구분(1기, 2기)
                {
                    if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2) == "1") // 확정구분(1.예정, 2.확정)
                    {
                        HDV101.HV101_DT08 = "0003";          // 4자리
                    }
                    else
                    {
                        HDV101.HV101_DT08 = "0006";          // 4자리
                    };
                }
                else
                {
                    if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2) == "1")
                    {
                        HDV101.HV101_DT08 = "0003";          // 4자리
                    }
                    else
                    {
                        HDV101.HV101_DT08 = "0006";          // 4자리
                    };
                }

                HDV101.HV101_DT09 = "0001";          // 4자리 (태영인더스트리 현재 수정신고를 하지 않음 - 수정신고 개발시 수정해야함)
                HDV101.HV101_DT10 = "TYC2921";       // 20자리 사용자 ID (본점-TYC2921 , 본점-TYC2922) (재정리 됨)
                HDV101.HV101_DT11 = sCORPNO.PadRight(13);     // 13자리 납세자번호 :  법인등록번호(법인)
                HDV101.HV101_DT12 = sFill.PadRight(30);       // 30자리 세무대리인성명 (공백)
                HDV101.HV101_DT13 = sFill.PadRight(06);       //  6자리 세무대리인관리번호 (공백)
                HDV101.HV101_DT14 = sFill.PadRight(04);       //  4자리 세무대리인전화번호1 (공백)
                HDV101.HV101_DT15 = sFill.PadRight(05);       //  5자리 세무대리인전화번호2 (공백)
                HDV101.HV101_DT16 = sFill.PadRight(05);       //  5자리 세무대리인전화번호3 (공백)
                HDV101.HV101_DT17 = sSANGHO ;      //30자리 상호명 (재정리 됨)
                HDV101.HV101_DT18 = sNAMENM ;     // 30자리 대표자명 (재정리 됨)
                HDV101.HV101_DT19 = sVNADDRS;     // 70자리 사업장소재지 (재정리 됨)
                HDV101.HV101_DT20 = sTELNUM ;     // 14자리 사업장전화번호 (재정리 됨)
                HDV101.HV101_DT21 = sVNADDRS;     // 70자리 사업자주소 (재정리 됨)
                HDV101.HV101_DT22 = sTELNUM ;     // 14자리 사업자전화번호 (재정리 됨)
                HDV101.HV101_DT23 = sUPTAE  ;     // 30자리 업태명 (재정리 됨)
                HDV101.HV101_DT24 = sEVENT  ;     // 50자리 종목명 (재정리 됨)
                HDV101.HV101_DT25 = sBUSTYPE.PadRight(07);     //  7자리 업종코드

                if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1) == "1") // 신고구분(1기, 2기)
                {
                    if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2) == "1") // 확정구분(1.예정, 2.확정)
                    {
                        s시작년월일 = sYEAR + "0101";
                        s종료년월일 = sYEAR + "0331";
                    }
                    else
                    {
                        s시작년월일 = sYEAR + "0401";
                        s종료년월일 = sYEAR + "0630";
                    };
                }
                else
                {
                    if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2) == "1")
                    {
                        s시작년월일 = sYEAR + "0701";
                        s종료년월일 = sYEAR + "0930";
                    }
                    else
                    {
                        s시작년월일 = sYEAR + "1001";
                        s종료년월일 = sYEAR + "1231";
                    };
                }

                HDV101.HV101_DT26 = s시작년월일;  // 8자리 과세기간(시작일)
                HDV101.HV101_DT27 = s종료년월일;  // 8자리 과세기간(종료일)
                HDV101.HV101_DT28 = s종료년월일;  // 8자리 작성일자 (해당기수의 마지막 일자를 기재)
                HDV101.HV101_DT29 = "19900508";  // 8자리 개업년월일
                HDV101.HV101_DT30 = "N";         // 1자리 보정신고구분
                HDV101.HV101_DT31 = sFill.PadRight(14);         // 14자리 사업자휴대전화
                HDV101.HV101_DT32 = "9000";                     //  4자리 세무프로그램코드 (9000)
                HDV101.HV101_DT33 = sFill.PadRight(10);         // 10자리 세무대리인사업자번호
                HDV101.HV101_DT34 = sFill.PadRight(50);         // 50자리 전자메일주소
                HDV101.HV101_DT35 = "100";                      // 3자리 신고종류구분코드
                HDV101.HV101_DT36 = sFill.PadRight(51);         // 51자리 공란

                // 레코드 세팅 작업(자리수)
                sData = HDV101.HV101_DT01;
                sData += HDV101.HV101_DT02;
                sData += HDV101.HV101_DT03;
                sData += HDV101.HV101_DT04;
                sData += HDV101.HV101_DT05;
                sData += HDV101.HV101_DT06;
                sData += HDV101.HV101_DT07;
                sData += HDV101.HV101_DT08;
                sData += HDV101.HV101_DT09;

                sStrTemp = HDV101.HV101_DT10.Trim(); // 10 사용자ID :  20자리 
                sStrTemp += new String(Convert.ToChar(" "), (20 - Encoding.Default.GetByteCount(HDV101.HV101_DT10.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT11.Trim(); // 11 납세자번호 :  13자리 
                sStrTemp += new String(Convert.ToChar(" "), (13 - Encoding.Default.GetByteCount(HDV101.HV101_DT11.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT12.Trim(); // 12 세무대리인성명 :  30자리 
                sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(HDV101.HV101_DT12.Trim())));
                sData += sStrTemp;

                sData += HDV101.HV101_DT13;
                sData += HDV101.HV101_DT14;
                sData += HDV101.HV101_DT15;
                sData += HDV101.HV101_DT16;

                sStrTemp = HDV101.HV101_DT17.Trim(); // 17 상호(법인명) : 30자리
                sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(HDV101.HV101_DT17.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT18.Trim(); // 18 성명(대표자명) : 30자리
                sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(HDV101.HV101_DT18.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT19.Trim(); // 19 사업장소재지 : 70자리
                sStrTemp += new String(Convert.ToChar(" "), (70 - Encoding.Default.GetByteCount(HDV101.HV101_DT19.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT20.Trim(); // 20 사업장전화번호 : 14자리
                sStrTemp += new String(Convert.ToChar(" "), (14 - Encoding.Default.GetByteCount(HDV101.HV101_DT20.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT21.Trim();// 21 사업장주소 : 70자리
                sStrTemp += new String(Convert.ToChar(" "), (70 - Encoding.Default.GetByteCount(HDV101.HV101_DT21.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT22.Trim(); // 22 사업자전화번호 : 14자리
                sStrTemp += new String(Convert.ToChar(" "), (14 - Encoding.Default.GetByteCount(HDV101.HV101_DT22.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT23.Trim(); // 23 업태명 : 30자리
                sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(HDV101.HV101_DT23.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT24.Trim(); // 24 종목명 : 50자리
                sStrTemp += new String(Convert.ToChar(" "), (50 - Encoding.Default.GetByteCount(HDV101.HV101_DT24.Trim())));
                sData += sStrTemp;

                sData += HDV101.HV101_DT25;  // 25 업종코드 : 7자리 
                sData += HDV101.HV101_DT26;
                sData += HDV101.HV101_DT27;
                sData += HDV101.HV101_DT28;
                sData += HDV101.HV101_DT29;
                sData += HDV101.HV101_DT30;
                sData += HDV101.HV101_DT31;
                sData += HDV101.HV101_DT32;
                sData += HDV101.HV101_DT33;
                sData += HDV101.HV101_DT34;
                sData += HDV101.HV101_DT35;
                sData += HDV101.HV101_DT36;
                */

                //2015.04월 변경
                HDV101.HV101_DT01 = "11";                  //  2자리 : 자료구분 ==> (11)
                HDV101.HV101_DT02 = "I103200";             //  7자리 : 서식코드 ==> (I103200)
                HDV101.HV101_DT03 = sSAUPNO.PadRight(13);  // 13자리 : 납세자ID --> 사업자등록번호(사업자번호 생성규칙과 무세적 체크를 함) 
                HDV101.HV101_DT04 = "41";                  //  2자리 : 세목구분코드 ‘41’ 부가가치세

                if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2) == "1") // 확정구분(1.예정, 2.확정)
                {
                    HDV101.HV101_DT05 = "03";          // 2자리
                }
                else
                {
                    HDV101.HV101_DT05 = "01";          // 2자리
                };

                HDV101.HV101_DT06 = "01";          // 2자리 -- 신고구분상세코드(01-정기신고 02-수정신고 03-기한후신고 04-경정청구

                if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1) == "1") // 신고구분(1기, 2기)
                {
                    HDV101.HV101_DT07 = this.DTP01_ELXYYMM.GetString().ToString().Substring(0, 4) + "01";  // 6자리
                }
                else
                {
                    HDV101.HV101_DT07 = this.DTP01_ELXYYMM.GetString().ToString().Substring(0, 4) + "02";  // 6자리
                }

                //신고서종류코드
                if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2) == "1") // 확정구분(1.예정, 2.확정)
                {
                    HDV101.HV101_DT08 = "C17";          // 3자리
                }
                else
                {
                    HDV101.HV101_DT08 = "C07";          // 3자리
                };


                HDV101.HV101_DT09 = "tyc2921";       // 20자리 사용자 ID (본점-TYC2921 , 본점-TYC2922) (재정리 됨)
                HDV101.HV101_DT10 = sCORPNO.PadRight(13);     // 13자리 납세자번호 :  법인등록번호(법인)
                HDV101.HV101_DT11 = sFill.PadRight(30);       // 30자리 세무대리인성명 (공백)
                HDV101.HV101_DT12 = sFill.PadRight(04);       //  4자리 세무대리인전화번호1 (공백)
                HDV101.HV101_DT13 = sFill.PadRight(05);       //  5자리 세무대리인전화번호2 (공백)
                HDV101.HV101_DT14 = sFill.PadRight(05);       //  5자리 세무대리인전화번호3 (공백)
                HDV101.HV101_DT15 = sSANGHO;      //30자리 상호명 (재정리 됨)
                HDV101.HV101_DT16 = sNAMENM;     // 30자리 대표자명 (재정리 됨)
                HDV101.HV101_DT17 = sVNADDRS;     // 70자리 사업장소재지 (재정리 됨)
                HDV101.HV101_DT18 = sTELNUM;     // 14자리 사업장전화번호 (재정리 됨)
                HDV101.HV101_DT19 = sVNADDRS;     // 70자리 사업자주소 (재정리 됨)
                HDV101.HV101_DT20 = sTELNUM;     // 14자리 사업자전화번호 (재정리 됨)
                HDV101.HV101_DT21 = sUPTAE;     // 30자리 업태명 (재정리 됨)
                HDV101.HV101_DT22 = sEVENT;     // 50자리 종목명 (재정리 됨)
                HDV101.HV101_DT23 = sBUSTYPE.PadRight(07);     //  7자리 업종코드                                

                if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1) == "1") // 신고구분(1기, 2기)
                {
                    if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2) == "1") // 확정구분(1.예정, 2.확정)
                    {
                        s시작년월일 = sYEAR + "0101";
                        s종료년월일 = sYEAR + "0331";
                    }
                    else
                    {
                        s시작년월일 = sYEAR + "0401";
                        s종료년월일 = sYEAR + "0630";
                    };
                }
                else
                {
                    if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2) == "1")
                    {
                        s시작년월일 = sYEAR + "0701";
                        s종료년월일 = sYEAR + "0930";
                    }
                    else
                    {
                        s시작년월일 = sYEAR + "1001";
                        s종료년월일 = sYEAR + "1231";
                    };
                }

                HDV101.HV101_DT24 = s시작년월일;  // 8자리 과세기간(시작일)
                HDV101.HV101_DT25 = s종료년월일;  // 8자리 과세기간(종료일)
                HDV101.HV101_DT26 = s종료년월일;  // 8자리 작성일자 (해당기수의 마지막 일자를 기재)
                HDV101.HV101_DT27 = "N";         // 1자리 보정신고구분
                HDV101.HV101_DT28 = sFill.PadRight(14);         // 14자리 사업자휴대전화
                //HDV101.HV101_DT29 = "9000";                     //  4자리 세무프로그램코드 (9000)
                if (this.CBO01_VNGUBUN.GetValue().ToString() == "1")
                {
                    HDV101.HV101_DT29 = "1074";                     //  4자리 세무프로그램코드 (9000)
                }
                else
                {
                    HDV101.HV101_DT29 = "1075";                     //  4자리 세무프로그램코드 (9000)
                }
                HDV101.HV101_DT30 = sFill.PadRight(13);         // 10자리 세무대리인사업자번호
                HDV101.HV101_DT31 = sFill.PadRight(50);         // 50자리 전자메일주소                
                HDV101.HV101_DT32 = sFill.PadRight(65);         // 51자리 공란

                // 레코드 세팅 작업(자리수)
                sData = HDV101.HV101_DT01;
                sData += HDV101.HV101_DT02;
                sData += HDV101.HV101_DT03;
                sData += HDV101.HV101_DT04;
                sData += HDV101.HV101_DT05;
                sData += HDV101.HV101_DT06;
                sData += HDV101.HV101_DT07;
                sData += HDV101.HV101_DT08;

                sStrTemp = HDV101.HV101_DT09.Trim(); // 9 사용자ID :  20자리 
                sStrTemp += new String(Convert.ToChar(" "), (20 - Encoding.Default.GetByteCount(HDV101.HV101_DT09.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT10.Trim(); // 10 납세자번호 :  13자리 
                sStrTemp += new String(Convert.ToChar(" "), (13 - Encoding.Default.GetByteCount(HDV101.HV101_DT10.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT11.Trim(); // 11 세무대리인성명 :  30자리 
                sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(HDV101.HV101_DT11.Trim())));
                sData += sStrTemp;

                sData += HDV101.HV101_DT12;
                sData += HDV101.HV101_DT13;
                sData += HDV101.HV101_DT14;                

                sStrTemp = HDV101.HV101_DT15.Trim(); // 15 상호(법인명) : 30자리
                sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(HDV101.HV101_DT15.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT16.Trim(); // 16 성명(대표자명) : 30자리
                sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(HDV101.HV101_DT16.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT17.Trim(); // 17 사업장소재지 : 70자리
                sStrTemp += new String(Convert.ToChar(" "), (70 - Encoding.Default.GetByteCount(HDV101.HV101_DT17.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT18.Trim(); // 18 사업장전화번호 : 14자리
                sStrTemp += new String(Convert.ToChar(" "), (14 - Encoding.Default.GetByteCount(HDV101.HV101_DT18.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT19.Trim();// 19 사업장주소 : 70자리
                sStrTemp += new String(Convert.ToChar(" "), (70 - Encoding.Default.GetByteCount(HDV101.HV101_DT19.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT20.Trim(); // 20 사업자전화번호 : 14자리
                sStrTemp += new String(Convert.ToChar(" "), (14 - Encoding.Default.GetByteCount(HDV101.HV101_DT20.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT21.Trim(); // 21 업태명 : 30자리
                sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(HDV101.HV101_DT21.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT22.Trim(); // 22 종목명 : 50자리
                sStrTemp += new String(Convert.ToChar(" "), (50 - Encoding.Default.GetByteCount(HDV101.HV101_DT22.Trim())));
                sData += sStrTemp;

                sData += HDV101.HV101_DT23;  // 25 업종코드 : 7자리 
                sData += HDV101.HV101_DT24;
                sData += HDV101.HV101_DT25;
                sData += HDV101.HV101_DT26;
                sData += HDV101.HV101_DT27;
                sData += HDV101.HV101_DT28;
                sData += HDV101.HV101_DT29;
                sData += HDV101.HV101_DT30;
                sData += HDV101.HV101_DT31;
                sData += HDV101.HV101_DT32;

                sw.WriteLine(sData);
            }
            else // 지점 (서울사무소 처리)
            {
                /*
                HDV101.HV101_DT01 = "11";                 //  2자리 : 자료구분
                HDV101.HV101_DT02 = "V101";               //  4자리 : 서식코드
                HDV101.HV101_DT03 = sSAUPNO.PadRight(13); // 13자리 : 납세자ID --> 사업자등록번호(사업자번호 생성규칙과 무세적 체크를 함) 
                HDV101.HV101_DT04 = "41";                 //  2자리 : 세목구분코드 ‘41’ 부가가치세
                if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2) == "1") // 확정구분(1.예정, 2.확정)
                {
                    HDV101.HV101_DT05 = "3";          // 1자리
                }
                else
                {
                    HDV101.HV101_DT05 = "1";          // 1자리
                };

                HDV101.HV101_DT06 = "8";          // 1자리 -- 납세자구분

                if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1) == "1") // 신고구분(1기, 2기)
                {
                    HDV101.HV101_DT07 = this.DTP01_ELXYYMM.GetString().ToString().Substring(0, 4) + "01";  // 6자리
                }
                else
                {
                    HDV101.HV101_DT07 = this.DTP01_ELXYYMM.GetString().ToString().Substring(0, 4) + "02";  // 6자리
                }

                // 신고차수
                if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1) == "1") // 신고구분(1기, 2기)
                {
                    if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2) == "1") // 확정구분(1.예정, 2.확정)
                    {
                        HDV101.HV101_DT08 = "0003";          // 4자리
                    }
                    else
                    {
                        HDV101.HV101_DT08 = "0006";          // 4자리
                    };
                }
                else
                {
                    if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2) == "1")
                    {
                        HDV101.HV101_DT08 = "0003";          // 4자리
                    }
                    else
                    {
                        HDV101.HV101_DT08 = "0006";          // 4자리
                    };
                }

                HDV101.HV101_DT09 = "0001";                    // 4자리 (태영인더스트리 현재 수정신고를 하지 않음 - 수정신고 개발시 수정해야함)
                HDV101.HV101_DT10 = "TYC2922";                // 20자리 사용자 ID (본점-TYC2921 , 본점-TYC2922) (재정리 됨)
                HDV101.HV101_DT11 = sCORPNO.PadRight(13);     // 13자리 납세자번호 :  법인등록번호(법인)
                HDV101.HV101_DT12 = sFill.PadRight(30);       // 30자리 세무대리인성명 (공백)
                HDV101.HV101_DT13 = sFill.PadRight(06);       //  6자리 세무대리인관리번호 (공백)
                HDV101.HV101_DT14 = sFill.PadRight(04);       //  4자리 세무대리인전화번호1 (공백)
                HDV101.HV101_DT15 = sFill.PadRight(05);       //  5자리 세무대리인전화번호2 (공백)
                HDV101.HV101_DT16 = sFill.PadRight(05);       //  5자리 세무대리인전화번호3 (공백)
                HDV101.HV101_DT17 = sSANGHO;      // 30자리 상호명 (재정리 됨)
                HDV101.HV101_DT18 = sNAMENM;      // 30자리 대표자명 (재정리 됨)
                HDV101.HV101_DT19 = sVNADDRS;     // 70자리 사업장소재지 (재정리 됨)
                HDV101.HV101_DT20 = sTELNUM;      // 14자리 사업장전화번호 (재정리 됨)
                HDV101.HV101_DT21 = sVNADDRS;     // 70자리 사업자주소 (재정리 됨)
                HDV101.HV101_DT22 = sTELNUM;      // 14자리 사업자전화번호 (재정리 됨)
                HDV101.HV101_DT23 = sUPTAE;       // 30자리 업태명 (재정리 됨)
                HDV101.HV101_DT24 = sEVENT;       // 50자리 종목명 (재정리 됨)
                HDV101.HV101_DT25 = sBUSTYPE.PadRight(07);     //  7자리 업종코드

                if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1) == "1") // 신고구분(1기, 2기)
                {
                    if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2) == "1") // 확정구분(1.예정, 2.확정)
                    {
                        s시작년월일 = sYEAR + "0101";
                        s종료년월일 = sYEAR + "0331";
                    }
                    else
                    {
                        s시작년월일 = sYEAR + "0401";
                        s종료년월일 = sYEAR + "0630";
                    };
                }
                else
                {
                    if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2) == "1")
                    {
                        s시작년월일 = sYEAR + "0701";
                        s종료년월일 = sYEAR + "0930";
                    }
                    else
                    {
                        s시작년월일 = sYEAR + "1001";
                        s종료년월일 = sYEAR + "1231";
                    };
                }

                HDV101.HV101_DT26 = s시작년월일;  // 8자리 과세기간(시작일)
                HDV101.HV101_DT27 = s종료년월일;  // 8자리 과세기간(종료일)
                HDV101.HV101_DT28 = s종료년월일;  // 8자리 작성일자 (해당기수의 마지막 일자를 기재)
                HDV101.HV101_DT29 = "20020221";  // 8자리 개업년월일
                HDV101.HV101_DT30 = "N";         // 1자리 보정신고구분
                HDV101.HV101_DT31 = sFill.PadRight(14);    // 14자리 사업자휴대전화 (공백)
                HDV101.HV101_DT32 = "9000";                //  4자리 세무프로그램코드 (9000)
                HDV101.HV101_DT33 = sFill.PadRight(10);    // 10자리 세무대리인사업자번호 (공백)
                HDV101.HV101_DT34 = sFill.PadRight(50);    // 50자리 전자메일주소 (공백)
                HDV101.HV101_DT35 = "100";                 //  3자리 신고종류구분코드
                HDV101.HV101_DT36 = sFill.PadRight(51);    // 51자리 공란 (공백)

                // 레코드 세팅 작업(자리수)
                sData = HDV101.HV101_DT01;
                sData += HDV101.HV101_DT02;
                sData += HDV101.HV101_DT03;
                sData += HDV101.HV101_DT04;
                sData += HDV101.HV101_DT05;
                sData += HDV101.HV101_DT06;
                sData += HDV101.HV101_DT07;
                sData += HDV101.HV101_DT08;
                sData += HDV101.HV101_DT09;

                sStrTemp = HDV101.HV101_DT10.Trim(); // 10 사용자ID :  20자리 
                sStrTemp += new String(Convert.ToChar(" "), (20 - Encoding.Default.GetByteCount(HDV101.HV101_DT10.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT11.Trim(); // 11 납세자번호 :  13자리 
                sStrTemp += new String(Convert.ToChar(" "), (13 - Encoding.Default.GetByteCount(HDV101.HV101_DT11.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT12.Trim(); // 12 세무대리인성명 :  30자리 
                sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(HDV101.HV101_DT12.Trim())));
                sData += sStrTemp;

                sData += HDV101.HV101_DT13;
                sData += HDV101.HV101_DT14;
                sData += HDV101.HV101_DT15;
                sData += HDV101.HV101_DT16;

                sStrTemp = HDV101.HV101_DT17.Trim(); // 17 상호(법인명) : 30자리
                sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(HDV101.HV101_DT17.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT18.Trim(); // 18 성명(대표자명) : 30자리
                sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(HDV101.HV101_DT18.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT19.Trim(); // 19 사업장소재지 : 70자리
                sStrTemp += new String(Convert.ToChar(" "), (70 - Encoding.Default.GetByteCount(HDV101.HV101_DT19.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT20.Trim(); // 20 사업장전화번호 : 14자리
                sStrTemp += new String(Convert.ToChar(" "), (14 - Encoding.Default.GetByteCount(HDV101.HV101_DT20.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT21.Trim();// 21 사업장주소 : 70자리
                sStrTemp += new String(Convert.ToChar(" "), (70 - Encoding.Default.GetByteCount(HDV101.HV101_DT21.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT22.Trim(); // 22 사업자전화번호 : 14자리
                sStrTemp += new String(Convert.ToChar(" "), (14 - Encoding.Default.GetByteCount(HDV101.HV101_DT22.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT23.Trim(); // 23 업태명 : 30자리
                sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(HDV101.HV101_DT23.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT24.Trim(); // 24 종목명 : 50자리
                sStrTemp += new String(Convert.ToChar(" "), (50 - Encoding.Default.GetByteCount(HDV101.HV101_DT24.Trim())));
                sData += sStrTemp;

                sData += HDV101.HV101_DT25;  // 25 업종코드 : 7자리 
                sData += HDV101.HV101_DT26;
                sData += HDV101.HV101_DT27;
                sData += HDV101.HV101_DT28;
                sData += HDV101.HV101_DT29;
                sData += HDV101.HV101_DT30;
                sData += HDV101.HV101_DT31;
                sData += HDV101.HV101_DT32;
                sData += HDV101.HV101_DT33;
                sData += HDV101.HV101_DT34;
                sData += HDV101.HV101_DT35;
                sData += HDV101.HV101_DT36;
                
                sw.WriteLine(sData);
                */


                //2015.04월 변경
                HDV101.HV101_DT01 = "11";                  //  2자리 : 자료구분 ==> (11)
                HDV101.HV101_DT02 = "I103200";             //  7자리 : 서식코드 ==> (I103200)
                HDV101.HV101_DT03 = sSAUPNO.PadRight(13);  // 13자리 : 납세자ID --> 사업자등록번호(사업자번호 생성규칙과 무세적 체크를 함) 
                HDV101.HV101_DT04 = "41";                  //  2자리 : 세목구분코드 ‘41’ 부가가치세

                if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2) == "1") // 확정구분(1.예정, 2.확정)
                {
                    HDV101.HV101_DT05 = "03";          // 2자리
                }
                else
                {
                    HDV101.HV101_DT05 = "01";          // 2자리
                };

                HDV101.HV101_DT06 = "01";          // 2자리 -- 신고구분상세코드(01-정기신고 02-수정신고 03-기한후신고 04-경정청구

                if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1) == "1") // 신고구분(1기, 2기)
                {
                    HDV101.HV101_DT07 = this.DTP01_ELXYYMM.GetString().ToString().Substring(0, 4) + "01";  // 6자리
                }
                else
                {
                    HDV101.HV101_DT07 = this.DTP01_ELXYYMM.GetString().ToString().Substring(0, 4) + "02";  // 6자리
                }

                //신고서종류코드
                if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2) == "1") // 확정구분(1.예정, 2.확정)
                {
                    HDV101.HV101_DT08 = "C17";          // 3자리
                }
                else
                {
                    HDV101.HV101_DT08 = "C07";          // 3자리
                };


                HDV101.HV101_DT09 = "tyc2922";       // 20자리 사용자 ID (본점-TYC2921 , 본점-TYC2922) (재정리 됨)
                HDV101.HV101_DT10 = sCORPNO.PadRight(13);     // 13자리 납세자번호 :  법인등록번호(법인)
                HDV101.HV101_DT11 = sFill.PadRight(30);       // 30자리 세무대리인성명 (공백)
                HDV101.HV101_DT12 = sFill.PadRight(04);       //  4자리 세무대리인전화번호1 (공백)
                HDV101.HV101_DT13 = sFill.PadRight(05);       //  5자리 세무대리인전화번호2 (공백)
                HDV101.HV101_DT14 = sFill.PadRight(05);       //  5자리 세무대리인전화번호3 (공백)
                HDV101.HV101_DT15 = sSANGHO;      //30자리 상호명 (재정리 됨)
                HDV101.HV101_DT16 = sNAMENM;     // 30자리 대표자명 (재정리 됨)
                HDV101.HV101_DT17 = sVNADDRS;     // 70자리 사업장소재지 (재정리 됨)
                HDV101.HV101_DT18 = sTELNUM;     // 14자리 사업장전화번호 (재정리 됨)
                HDV101.HV101_DT19 = sVNADDRS;     // 70자리 사업자주소 (재정리 됨)
                HDV101.HV101_DT20 = sTELNUM;     // 14자리 사업자전화번호 (재정리 됨)
                HDV101.HV101_DT21 = sUPTAE;     // 30자리 업태명 (재정리 됨)
                HDV101.HV101_DT22 = sEVENT;     // 50자리 종목명 (재정리 됨)
                HDV101.HV101_DT23 = sBUSTYPE.PadRight(07);     //  7자리 업종코드                                

                if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1) == "1") // 신고구분(1기, 2기)
                {
                    if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2) == "1") // 확정구분(1.예정, 2.확정)
                    {
                        s시작년월일 = sYEAR + "0101";
                        s종료년월일 = sYEAR + "0331";
                    }
                    else
                    {
                        s시작년월일 = sYEAR + "0401";
                        s종료년월일 = sYEAR + "0630";
                    };
                }
                else
                {
                    if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2) == "1")
                    {
                        s시작년월일 = sYEAR + "0701";
                        s종료년월일 = sYEAR + "0930";
                    }
                    else
                    {
                        s시작년월일 = sYEAR + "1001";
                        s종료년월일 = sYEAR + "1231";
                    };
                }

                HDV101.HV101_DT24 = s시작년월일;  // 8자리 과세기간(시작일)
                HDV101.HV101_DT25 = s종료년월일;  // 8자리 과세기간(종료일)
                HDV101.HV101_DT26 = s종료년월일;  // 8자리 작성일자 (해당기수의 마지막 일자를 기재)
                HDV101.HV101_DT27 = "N";         // 1자리 보정신고구분
                HDV101.HV101_DT28 = sFill.PadRight(14);         // 14자리 사업자휴대전화
                //HDV101.HV101_DT29 = "9000";                     //  4자리 세무프로그램코드 (9000)
                if (this.CBO01_VNGUBUN.GetValue().ToString() == "1")
                {
                    HDV101.HV101_DT29 = "1074";                     //  4자리 세무프로그램코드 (9000)
                }
                else
                {
                    HDV101.HV101_DT29 = "1075";                     //  4자리 세무프로그램코드 (9000)
                }
                HDV101.HV101_DT30 = sFill.PadRight(13);         // 10자리 세무대리인사업자번호
                HDV101.HV101_DT31 = sFill.PadRight(50);         // 50자리 전자메일주소                
                HDV101.HV101_DT32 = sFill.PadRight(65);         // 51자리 공란

                // 레코드 세팅 작업(자리수)
                sData = HDV101.HV101_DT01;
                sData += HDV101.HV101_DT02;
                sData += HDV101.HV101_DT03;
                sData += HDV101.HV101_DT04;
                sData += HDV101.HV101_DT05;
                sData += HDV101.HV101_DT06;
                sData += HDV101.HV101_DT07;
                sData += HDV101.HV101_DT08;

                sStrTemp = HDV101.HV101_DT09.Trim(); // 9 사용자ID :  20자리 
                sStrTemp += new String(Convert.ToChar(" "), (20 - Encoding.Default.GetByteCount(HDV101.HV101_DT09.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT10.Trim(); // 10 납세자번호 :  13자리 
                sStrTemp += new String(Convert.ToChar(" "), (13 - Encoding.Default.GetByteCount(HDV101.HV101_DT10.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT11.Trim(); // 11 세무대리인성명 :  30자리 
                sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(HDV101.HV101_DT11.Trim())));
                sData += sStrTemp;

                sData += HDV101.HV101_DT12;
                sData += HDV101.HV101_DT13;
                sData += HDV101.HV101_DT14;

                sStrTemp = HDV101.HV101_DT15.Trim(); // 15 상호(법인명) : 30자리

                int kk = Encoding.Default.GetByteCount(HDV101.HV101_DT15.Trim());

                sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(HDV101.HV101_DT15.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT16.Trim(); // 16 성명(대표자명) : 30자리
                sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(HDV101.HV101_DT16.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT17.Trim(); // 17 사업장소재지 : 70자리
                sStrTemp += new String(Convert.ToChar(" "), (70 - Encoding.Default.GetByteCount(HDV101.HV101_DT17.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT18.Trim(); // 18 사업장전화번호 : 14자리
                sStrTemp += new String(Convert.ToChar(" "), (14 - Encoding.Default.GetByteCount(HDV101.HV101_DT18.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT19.Trim();// 19 사업장주소 : 70자리
                sStrTemp += new String(Convert.ToChar(" "), (70 - Encoding.Default.GetByteCount(HDV101.HV101_DT19.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT20.Trim(); // 20 사업자전화번호 : 14자리
                sStrTemp += new String(Convert.ToChar(" "), (14 - Encoding.Default.GetByteCount(HDV101.HV101_DT20.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT21.Trim(); // 21 업태명 : 30자리
                sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(HDV101.HV101_DT21.Trim())));
                sData += sStrTemp;

                sStrTemp = HDV101.HV101_DT22.Trim(); // 22 종목명 : 50자리
                sStrTemp += new String(Convert.ToChar(" "), (50 - Encoding.Default.GetByteCount(HDV101.HV101_DT22.Trim())));
                sData += sStrTemp;

                sData += HDV101.HV101_DT23;  // 25 업종코드 : 7자리 
                sData += HDV101.HV101_DT24;
                sData += HDV101.HV101_DT25;
                sData += HDV101.HV101_DT26;
                sData += HDV101.HV101_DT27;
                sData += HDV101.HV101_DT28;
                sData += HDV101.HV101_DT29;
                sData += HDV101.HV101_DT30;
                sData += HDV101.HV101_DT31;
                sData += HDV101.HV101_DT32;

                Int16 dd = Convert.ToInt16(sData.Length);

                sw.WriteLine(sData);
            }

        }
        #endregion

        #region Description : 일반과세자 신고서 레코드 생성 (V101) -- UP_TAX_Create_V101() 
        private void UP_TAX_Create_V101(StreamWriter sw)
        {
            string sStrTemp = string.Empty;
            string sData = string.Empty;
            string sFill = string.Empty;
                       

            // 신고서
            /*
            struct_DV101 V101 = new struct_DV101(); 
            this.DbConnector.CommandClear(); // AVSURTAXF 
            this.DbConnector.Attach("TY_P_AC_41MB3140",
                                    this.DTP01_ELXYYMM.GetValue().ToString(),
                                    this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)  // 확정구분(1.예정, 2.확정)
                                    );
            DataTable dt_101 = this.DbConnector.ExecuteDataTable();

            if (dt_101.Rows.Count > 0)
            {
                V101.DV101_DT01 = dt_101.Rows[0]["VSDT01"].ToString(); // 2자리 (문자) : 자료구분  ==> (17)
                V101.DV101_DT02 = dt_101.Rows[0]["VSDT02"].ToString(); // 4자리 (문자) : 서식코드  ==> (V101)
                V101.DV101_DT03 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT03"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT04 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT04"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT05 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT05"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT06 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT06"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT07 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT07"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT08 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT08"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT09 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT09"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT10 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT10"].ToString().Trim(), 13);    // 13자리

                V101.DV101_DT11 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT11"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT12 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT12"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT13 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT13"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT14 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT14"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT15 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT15"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT16 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT16"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT17 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT17"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT18 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT18"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT19 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT19"].ToString().Trim(), 13);    // 13자리

                V101.DV101_DT20 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT20"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT21 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT21"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT22 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT22"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT23 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT23"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT24 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT24"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT25 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT25"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT26 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT26"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT27 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT27"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT28 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT28"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT29 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT29"].ToString().Trim(), 13);    // 13자리

                V101.DV101_DT30 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT30"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT31 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT31"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT32 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT32"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT33 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT33"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT34 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT34"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT35 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT35"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT36 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT36"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT37 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT37"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT38 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT38"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT39 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT39"].ToString().Trim(), 13);    // 13자리

                V101.DV101_DT40 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT40"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT41 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT41"].ToString().Trim(), 13);    // 13자리

                V101.DV101_DT42 = "0";    // 13자리(신고불성실가산금액) -- 재계산됨
                V101.DV101_DT43 = "0";    // 13자리(신고불성실가산세액) -- 재계산됨

                V101.DV101_DT44 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT44"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT45 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT45"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT46 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT46"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT47 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT47"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT48 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT48"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT49 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT49"].ToString().Trim(), 13);    // 13자리

                V101.DV101_DT50 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT50"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT51 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT51"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT52 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT52"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT53 = dt_101.Rows[0]["VSDT53"].ToString().PadRight(01);                      // 1자리 (문자) : 환급구분 (" " : 환급 없음 , 1 : 일반환급 , 2 :영세율 환급 , 3: 시설투자 환급)
                V101.DV101_DT54 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT54"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT55 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT55"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT56 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT56"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT57 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT57"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT58 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT58"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT59 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT59"].ToString().Trim(), 13);    // 13자리

                V101.DV101_DT60 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT60"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT61 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT61"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT62 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT62"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT63 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT63"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT64 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT64"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT65 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT65"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT66 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT66"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT67 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT67"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT68 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT68"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT69 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT69"].ToString().Trim(), 13);    // 13자리

                V101.DV101_DT70 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT70"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT71 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT71"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT72 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT72"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT73 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT73"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT74 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT74"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT75 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT75"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT76 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT76"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT77 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT77"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT78 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT78"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT79 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT79"].ToString().Trim(), 15);    // 15자리

                V101.DV101_DT80 = dt_101.Rows[0]["VSDT80"].ToString().PadRight(03); //  3자리 (문자) : 은행코드(국세환급금)
                V101.DV101_DT81 = dt_101.Rows[0]["VSDT81"].ToString().PadRight(20); // 20자리 (문자) : 계좌번호(국세환급금)
                V101.DV101_DT82 = dt_101.Rows[0]["VSDT82"].ToString().PadRight(07); //  7자리 (문자) : 총괄납부승인번호
                V101.DV101_DT83 = dt_101.Rows[0]["VSDT83"].ToString();              // 30자리 (문자) : 은행지점명(한글)
                V101.DV101_DT84 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT84"].ToString().Trim(), 15);    // 15자리 산출세액
                V101.DV101_DT85 = dt_101.Rows[0]["VSDT85"].ToString().PadRight(08); //  8자리 (문자) : 폐업일자
                V101.DV101_DT86 = dt_101.Rows[0]["VSDT86"].ToString().PadRight(03); //  3자리 (문자) : 폐업사유
                V101.DV101_DT87 = dt_101.Rows[0]["VSDT87"].ToString().PadRight(01); //  1자리 (문자) : 기한후(과세표준)여부
                V101.DV101_DT88 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT88"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT89 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT89"].ToString().Trim(), 15);    // 15자리

                V101.DV101_DT90 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT90"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT91 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT91"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT92 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT92"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT93 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT93"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT94 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT94"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT95 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT95"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT96 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT96"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT97 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT97"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT98 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT98"].ToString().Trim(), 13);    // 13자리 : 기타경감공제세액명세기타세액 : 정기신고,기한후신고,수정신고분 모두 0으로 입력합니다
                V101.DV101_DT99 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT99"].ToString().Trim(), 13);    // 13자리

                V101.DV101_DT100 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT100"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT101 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT101"].ToString().Trim(), 15);    // 15자리 : 실차감납부할세액
                V101.DV101_DT102 = dt_101.Rows[0]["VSDT102"].ToString().PadRight(01); //  1자리 (문자) : 일반과세자구분
                V101.DV101_DT103 = dt_101.Rows[0]["VSDT103"].ToString().PadRight(01); //  1자리 (문자) : 조기환급취소구분
                V101.DV101_DT104 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT104"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT105 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT105"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT106 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT106"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT107 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT107"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT108 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT108"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT109 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT109"].ToString().Trim(), 13);    // 13자리

                V101.DV101_DT110 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT110"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT111 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT111"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT112 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT112"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT113 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT113"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT114 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT114"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT115 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT115"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT116 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT116"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT117 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT117"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT118 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT118"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT119 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT119"].ToString().Trim(), 15);    // 15자리

                V101.DV101_DT120 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT120"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT121 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT121"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT122 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT122"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT123 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT123"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT124 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT124"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT125 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT125"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT126 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT126"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT127 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT127"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT128 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT128"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT129 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT129"].ToString().Trim(), 13);    // 13자리

                V101.DV101_DT130 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT130"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT131 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT131"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT132 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT132"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT133 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT133"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT134 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT134"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT135 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT135"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT136 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT136"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT137 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT137"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT138 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT138"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT139 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT139"].ToString().Trim(), 15);    // 15자리

                V101.DV101_DT140 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT140"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT141 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT141"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT142 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT142"].ToString().Trim(), 15);    // 15자리

                // 추가 (2014.02.22)
                //-- 사업양수자의 대리납부기납부세액
                //-- 외국인관광객에대한환급세액
                //-- 매입자납부특례거래계좌미사용
                //-- 매입자납부특례거래계좌지연입금

                V101.DV101_DT143 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT143"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT144 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT144"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT145 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT145"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT146 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT146"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT147 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT147"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT148 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT148"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT149 = sFill.PadRight(81); // 81자리 공백수정 (2014.02.22)

                // 신고불성실가산금액 : 신고불성실 각 항목(133,135,137,139)의 금액의 합을 기재한다
                string sDT42 = Convert.ToString(Convert.ToDouble(V101.DV101_DT133) + Convert.ToDouble(V101.DV101_DT135) + Convert.ToDouble(V101.DV101_DT137) + Convert.ToDouble(V101.DV101_DT139));
                V101.DV101_DT42 = UP_Minus_Conv_Fill(sDT42, 13);    // 13자리(신고불성실가산금액)

                // 신고불성실가산세액 : 신고불성실 각 항목(134,136,138,140)의 세액의 합을 기재한다.
                string sDT43 = Convert.ToString(Convert.ToDouble(V101.DV101_DT134) + Convert.ToDouble(V101.DV101_DT136) + Convert.ToDouble(V101.DV101_DT138) + Convert.ToDouble(V101.DV101_DT140));
                V101.DV101_DT43 = UP_Minus_Conv_Fill(sDT43, 13);    // 13자리(신고불성실가산세액)


                // 레코드 세팅 작업(자리수)
                sData = V101.DV101_DT01;
                sData += V101.DV101_DT02;
                sData += V101.DV101_DT03;
                sData += V101.DV101_DT04;
                sData += V101.DV101_DT05;
                sData += V101.DV101_DT06;
                sData += V101.DV101_DT07;
                sData += V101.DV101_DT08;
                sData += V101.DV101_DT09;

                sData += V101.DV101_DT10;
                sData += V101.DV101_DT11;
                sData += V101.DV101_DT12;
                sData += V101.DV101_DT13;
                sData += V101.DV101_DT14;
                sData += V101.DV101_DT15;
                sData += V101.DV101_DT16;
                sData += V101.DV101_DT17;
                sData += V101.DV101_DT18;
                sData += V101.DV101_DT19;

                sData += V101.DV101_DT20;
                sData += V101.DV101_DT21;
                sData += V101.DV101_DT22;
                sData += V101.DV101_DT23;
                sData += V101.DV101_DT24;
                sData += V101.DV101_DT25;
                sData += V101.DV101_DT26;
                sData += V101.DV101_DT27;
                sData += V101.DV101_DT28;
                sData += V101.DV101_DT29;

                sData += V101.DV101_DT30;
                sData += V101.DV101_DT31;
                sData += V101.DV101_DT32;
                sData += V101.DV101_DT33;
                sData += V101.DV101_DT34;
                sData += V101.DV101_DT35;
                sData += V101.DV101_DT36;
                sData += V101.DV101_DT37;
                sData += V101.DV101_DT38;
                sData += V101.DV101_DT39;

                sData += V101.DV101_DT40;
                sData += V101.DV101_DT41;
                sData += V101.DV101_DT42;
                sData += V101.DV101_DT43;
                sData += V101.DV101_DT44;
                sData += V101.DV101_DT45;
                sData += V101.DV101_DT46;
                sData += V101.DV101_DT47;
                sData += V101.DV101_DT48;
                sData += V101.DV101_DT49;

                sData += V101.DV101_DT50;
                sData += V101.DV101_DT51;
                sData += V101.DV101_DT52;
                sData += V101.DV101_DT53;  // 53: 문자 1자리 : 환급구분
                sData += V101.DV101_DT54;
                sData += V101.DV101_DT55;
                sData += V101.DV101_DT56;
                sData += V101.DV101_DT57;
                sData += V101.DV101_DT58;
                sData += V101.DV101_DT59;

                sData += V101.DV101_DT60;
                sData += V101.DV101_DT61;
                sData += V101.DV101_DT62;
                sData += V101.DV101_DT63;
                sData += V101.DV101_DT64;
                sData += V101.DV101_DT65;
                sData += V101.DV101_DT66;
                sData += V101.DV101_DT67;
                sData += V101.DV101_DT68;
                sData += V101.DV101_DT69;
                sData += V101.DV101_DT70;

                sData += V101.DV101_DT71;
                sData += V101.DV101_DT72;
                sData += V101.DV101_DT73;
                sData += V101.DV101_DT74;
                sData += V101.DV101_DT75;
                sData += V101.DV101_DT76;
                sData += V101.DV101_DT77;
                sData += V101.DV101_DT78;
                sData += V101.DV101_DT79;

                sData += V101.DV101_DT80; // 80 : 문자  3자리 : 은행코드(국세환급금)
                sData += V101.DV101_DT81; // 81 : 문자 20자리 : 계좌번호(국세환급금)
                sData += V101.DV101_DT82; // 82 : 문자  7자리 : 총괄납부승인번호
                sStrTemp = V101.DV101_DT83.Trim();  //  83: 문자 30자리 : 은행지점명
                sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(V101.DV101_DT83.Trim())));
                sData += sStrTemp;
                sData += V101.DV101_DT84;
                sData += V101.DV101_DT85; //  85: 문자 8자리 : 폐업일자
                sData += V101.DV101_DT86; //  86: 문자 3자리 : 폐업사유
                sData += V101.DV101_DT87; //  87: 문자 1자리 : 기한후(과세표준)여부
                sData += V101.DV101_DT88;
                sData += V101.DV101_DT89;
                sData += V101.DV101_DT90;

                sData += V101.DV101_DT91;
                sData += V101.DV101_DT92;
                sData += V101.DV101_DT93;
                sData += V101.DV101_DT94;
                sData += V101.DV101_DT95;
                sData += V101.DV101_DT96;
                sData += V101.DV101_DT97;
                sData += V101.DV101_DT98;
                sData += V101.DV101_DT99;

                sData += V101.DV101_DT100;
                sData += V101.DV101_DT101; // 101: 숫자 15자리 : 실차감납부할세액
                sData += V101.DV101_DT102; // 102: 문자  1자리 : 일반과세자구분
                sData += V101.DV101_DT103; // 103: 문자  1자리 : 조기환급취소구분
                sData += V101.DV101_DT104;
                sData += V101.DV101_DT105;
                sData += V101.DV101_DT106;
                sData += V101.DV101_DT107;
                sData += V101.DV101_DT108;
                sData += V101.DV101_DT109;

                sData += V101.DV101_DT110;
                sData += V101.DV101_DT111;
                sData += V101.DV101_DT112;
                sData += V101.DV101_DT113;
                sData += V101.DV101_DT114;
                sData += V101.DV101_DT115;
                sData += V101.DV101_DT116;
                sData += V101.DV101_DT117;
                sData += V101.DV101_DT118;
                sData += V101.DV101_DT119;

                sData += V101.DV101_DT120;
                sData += V101.DV101_DT121;
                sData += V101.DV101_DT122;
                sData += V101.DV101_DT123;
                sData += V101.DV101_DT124;
                sData += V101.DV101_DT125;
                sData += V101.DV101_DT126;
                sData += V101.DV101_DT127;
                sData += V101.DV101_DT128;
                sData += V101.DV101_DT129;

                sData += V101.DV101_DT130;
                sData += V101.DV101_DT131;
                sData += V101.DV101_DT132;
                sData += V101.DV101_DT133;
                sData += V101.DV101_DT134;
                sData += V101.DV101_DT135;
                sData += V101.DV101_DT136;
                sData += V101.DV101_DT137;
                sData += V101.DV101_DT138;
                sData += V101.DV101_DT139;

                sData += V101.DV101_DT140;
                sData += V101.DV101_DT141;
                sData += V101.DV101_DT142;

                // 추가 (2014.02.22) 
                sData += V101.DV101_DT143; // 사업양수자의 대리납부기납부세
                sData += V101.DV101_DT144; // 외국인관광객에대한환급세액
                sData += V101.DV101_DT145; // 매입자납부특례거래계좌미사용
                sData += V101.DV101_DT146; // 매입자납부특례거래계좌미사용
                sData += V101.DV101_DT147; // 매입자납부특례거래계좌지연입금
                sData += V101.DV101_DT148; // 매입자납부특례거래계좌지연입금
                sData += V101.DV101_DT149; // 문자 81자리 (공란)

                sw.WriteLine(sData);
                */

            // 2015년 변경 신고서
            struct_DV101 V101 = new struct_DV101(); 
            this.DbConnector.CommandClear(); // AVSURTAXF 
            this.DbConnector.Attach("TY_P_AC_54L83243",
                                    this.DTP01_ELXYYMM.GetValue().ToString(),
                                    this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)  // 확정구분(1.예정, 2.확정)
                                    );
            DataTable dt_101 = this.DbConnector.ExecuteDataTable();

            if (dt_101.Rows.Count > 0)
            {
                V101.DV101_DT01 = dt_101.Rows[0]["VSDT01"].ToString(); // 2자리 (문자) : 자료구분  ==> (17)
                V101.DV101_DT02 = dt_101.Rows[0]["VSDT02"].ToString(); // 7자리 (문자) : 서식코드  ==> (I103200)
                V101.DV101_DT03 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT03"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT04 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT04"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT05 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT05"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT06 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT06"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT07 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT07"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT08 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT08"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT09 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT09"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT10 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT10"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT11 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT11"].ToString().Trim(), 13);    // 13자리

                V101.DV101_DT12 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT12"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT13 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT13"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT14 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT14"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT15 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT15"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT16 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT16"].ToString().Trim(), 13);    // 15자리
                V101.DV101_DT17 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT17"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT18 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT18"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT19 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT19"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT20 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT20"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT21 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT21"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT22 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT22"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT23 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT23"].ToString().Trim(), 13);    // 13자리

                V101.DV101_DT24 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT24"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT25 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT25"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT26 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT26"].ToString().Trim(), 15);    // 15자리

                V101.DV101_DT27 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT27"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT28 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT28"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT29 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT29"].ToString().Trim(), 13);    // 13자리

                V101.DV101_DT30 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT30"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT31 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT31"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT32 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT32"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT33 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT33"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT34 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT34"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT35 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT35"].ToString().Trim(), 13);    // 15자리
                V101.DV101_DT36 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT36"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT37 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT37"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT38 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT38"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT39 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT39"].ToString().Trim(), 13);    // 13자리

                V101.DV101_DT40 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT40"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT41 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT41"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT42 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT42"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT43 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT43"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT44 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT44"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT45 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT45"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT46 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT46"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT47 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT47"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT48 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT48"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT49 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT49"].ToString().Trim(), 13);    // 13자리

                V101.DV101_DT50 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT50"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT51 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT51"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT52 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT52"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT53 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT53"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT54 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT54"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT55 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT55"].ToString().Trim(), 13);    // 13자리

                V101.DV101_DT56 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT56"].ToString().Trim(), 15);    // 15자리

                V101.DV101_DT57 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT57"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT58 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT58"].ToString().Trim(), 13);    // 13자리

                V101.DV101_DT59 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT59"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT60 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT60"].ToString().Trim(), 15);    // 15자리

                V101.DV101_DT61 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT61"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT62 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT62"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT63 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT63"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT64 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT64"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT65 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT65"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT66 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT66"].ToString().Trim(), 13);    // 13자리

                V101.DV101_DT67 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT67"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT68 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT68"].ToString().Trim(), 13);    // 13자리
                V101.DV101_DT69 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT69"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT70 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT70"].ToString().Trim(), 13);    // 13자리

                V101.DV101_DT71 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT71"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT72 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT72"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT73 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT73"].ToString().Trim(), 15);    // 15자리

                V101.DV101_DT74 = dt_101.Rows[0]["VSDT74"].ToString().PadRight(02); //  2자리 (문자) : 환급구분코드

                V101.DV101_DT75 = dt_101.Rows[0]["VSDT75"].ToString().PadRight(03); //  3자리 (문자) : 은행코드(국세환급금)
                V101.DV101_DT76 = dt_101.Rows[0]["VSDT76"].ToString().PadRight(20); // 20자리 (문자) : 계좌번호(국세환급금)
                V101.DV101_DT77 = dt_101.Rows[0]["VSDT82"].ToString().PadRight(09); //  9자리 (문자) : 총괄납부승인번호
                V101.DV101_DT78 = dt_101.Rows[0]["VSDT78"].ToString();              // 30자리 (문자) : 은행지점명(한글)
                                
                V101.DV101_DT79 = dt_101.Rows[0]["VSDT79"].ToString().PadRight(08); //  8자리 (문자) : 폐업일자
                V101.DV101_DT80 = dt_101.Rows[0]["VSDT80"].ToString().PadRight(03); //  3자리 (문자) : 폐업사유

                V101.DV101_DT81 = dt_101.Rows[0]["VSDT81"].ToString().PadRight(01); //  1자리 (문자) : 기한후(과세표준)여부
                V101.DV101_DT82 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT82"].ToString().Trim(), 15);    // 15자리
                V101.DV101_DT83 = dt_101.Rows[0]["VSDT83"].ToString().PadRight(01); //  1자리 (문자) : 일반과세자구분
                V101.DV101_DT84 = dt_101.Rows[0]["VSDT84"].ToString().PadRight(01); //  1자리 (문자) : 일반과세자구분

                V101.DV101_DT85 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT85"].ToString().Trim(), 15);    // 15자리 수출기업 수입 납부유예(2016년 적용)

                V101.DV101_DT86 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT86"].ToString().Trim(), 13);    // 13자리 신용카드업자의 대리납부 기납부세액(2019년 적용)

                V101.DV101_DT87 = UP_Minus_Conv_Fill(dt_101.Rows[0]["VSDT87"].ToString().Trim(), 13);    // 13자리 소규모 개인사업자 부가가치세 감면세액(2020년 1기 확정부터 적용)

                V101.DV101_DT88 = sFill.PadRight(2); // 2자리 공백(2019년 변경)

                // 레코드 세팅 작업(자리수)
                sData = V101.DV101_DT01;
                sData += V101.DV101_DT02;
                sData += V101.DV101_DT03;
                sData += V101.DV101_DT04;
                sData += V101.DV101_DT05;
                sData += V101.DV101_DT06;
                sData += V101.DV101_DT07;
                sData += V101.DV101_DT08;
                sData += V101.DV101_DT09;

                sData += V101.DV101_DT10;
                sData += V101.DV101_DT11;
                sData += V101.DV101_DT12;
                sData += V101.DV101_DT13;
                sData += V101.DV101_DT14;
                sData += V101.DV101_DT15;
                sData += V101.DV101_DT16;
                sData += V101.DV101_DT17;
                sData += V101.DV101_DT18;
                sData += V101.DV101_DT19;

                sData += V101.DV101_DT20;
                sData += V101.DV101_DT21;
                sData += V101.DV101_DT22;
                sData += V101.DV101_DT23;
                sData += V101.DV101_DT24;
                sData += V101.DV101_DT25;
                sData += V101.DV101_DT26;                

                sData += V101.DV101_DT27;
                sData += V101.DV101_DT28;
                sData += V101.DV101_DT29;

                sData += V101.DV101_DT30;
                sData += V101.DV101_DT31;
                sData += V101.DV101_DT32;
                sData += V101.DV101_DT33;
                sData += V101.DV101_DT34;
                sData += V101.DV101_DT35;
                sData += V101.DV101_DT36;
                sData += V101.DV101_DT37;
                sData += V101.DV101_DT38;
                sData += V101.DV101_DT39;
                
                sData += V101.DV101_DT40;
                sData += V101.DV101_DT41;
                sData += V101.DV101_DT42;
                sData += V101.DV101_DT43;
                sData += V101.DV101_DT44;
                
                sData += V101.DV101_DT45;
                sData += V101.DV101_DT46;
                sData += V101.DV101_DT47;
                sData += V101.DV101_DT48;
                sData += V101.DV101_DT49;
                               

                sData += V101.DV101_DT50;
                sData += V101.DV101_DT51;
                sData += V101.DV101_DT52;
                sData += V101.DV101_DT53;  
                sData += V101.DV101_DT54;
                sData += V101.DV101_DT55;
                sData += V101.DV101_DT56;
                sData += V101.DV101_DT57;
                sData += V101.DV101_DT58;
                sData += V101.DV101_DT59;
                                
                sData += V101.DV101_DT60;
                sData += V101.DV101_DT61;
                sData += V101.DV101_DT62;
                sData += V101.DV101_DT63;
                sData += V101.DV101_DT64;
                sData += V101.DV101_DT65;
                sData += V101.DV101_DT66;
                sData += V101.DV101_DT67;
                sData += V101.DV101_DT68;
                sData += V101.DV101_DT69;
                sData += V101.DV101_DT70;
                               

                sData += V101.DV101_DT71;
                sData += V101.DV101_DT72;
                sData += V101.DV101_DT73;
                sData += V101.DV101_DT74;
                sData += V101.DV101_DT75;
                sData += V101.DV101_DT76;
                sData += V101.DV101_DT77;
                
                sStrTemp = V101.DV101_DT78.Trim();  //  83: 문자 30자리 : 은행지점명
                sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(V101.DV101_DT78.Trim())));
                sData += sStrTemp;

                sData += V101.DV101_DT79;
                sData += V101.DV101_DT80; 
                sData += V101.DV101_DT81; 
                sData += V101.DV101_DT82;
                sData += V101.DV101_DT83;
                sData += V101.DV101_DT84;
                sData += V101.DV101_DT85;
                sData += V101.DV101_DT86;
                sData += V101.DV101_DT87;
                sData += V101.DV101_DT88;

                sw.WriteLine(sData);

            };

        }
        #endregion 

        #region Description : 부가가치세수입금액등(과세표준명세, 면세수입금액) 레코드 생성 (V101) -- UP_TAX_Create_CV101()
        private void UP_TAX_Create_CV101(StreamWriter sw)
        {
            string sStrTemp = string.Empty;
            string sData = string.Empty;
            string sFill = string.Empty;


            /*
             struct_CV101 C101 = new struct_CV101();
            // 신고서
            this.DbConnector.CommandClear(); // AVSURTAXF 
            this.DbConnector.Attach("TY_P_AC_42437183",
                                    this.DTP01_ELXYYMM.GetValue().ToString(),
                                    this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1), // 신고구분(1기, 2기)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)  // 확정구분(1.예정, 2.확정)
                                    );
            DataTable dt_101 = this.DbConnector.ExecuteDataTable();

            if (dt_101.Rows.Count > 0)
            {
                //  수입구분 1번 체크 사항 (과세표준명세 ( 일반과세자 부가가치세신고서의  (26)번, (27), (28)번 항목)
                // ----------------------------------------- 수입구분 [ 1 - 최대 2개 ] ------------------------------------
                #region Description : 수입구분 [ 1 ] -- 26,27,28 번 항목 체크
                if (Convert.ToInt64(dt_101.Rows[0]["VS05EV26AMT"].ToString()) > 0)
                {
                    C101.CV101_DT01 = "15";    // 2자리 : 자료구분 ==> (15)
                    C101.CV101_DT02 = "V101";  // 4자리 : 서식코드 ==> (V101)
                    C101.CV101_DT03 = "1";     // 1자리 : 수입금액종류구분
                    C101.CV101_DT04 = dt_101.Rows[0]["VS05EV26BUS"].ToString().Trim();   //  30자리 : 업태명 (재정리)
                    C101.CV101_DT05 = dt_101.Rows[0]["VS05EV26NAM"].ToString().Trim();   //  50자리 : 종목명 (재정리)
                    C101.CV101_DT06 = dt_101.Rows[0]["VS05EV26COD"].ToString().Trim().PadRight(07);   //  7자리  : 주 업종코드
                    C101.CV101_DT07 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS05EV26AMT"].ToString()));  // 15자리  : 수입금액
                    C101.CV101_DT08 = sFill.PadRight(41);  // 41자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = C101.CV101_DT01;           //  2자리 : 자료구분
                    sData += C101.CV101_DT02;          //  4자리 : 서식코드
                    sData += C101.CV101_DT03;          //  1자리 : 수입금액종류구분
                    sStrTemp = C101.CV101_DT04.Trim(); // 30자리 : 업태명 (한글)
                    sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(C101.CV101_DT04.Trim())));
                    sData += sStrTemp;
                    sStrTemp = C101.CV101_DT05.Trim(); // 50자리 : 종목명 (한글)
                    sStrTemp += new String(Convert.ToChar(" "), (50 - Encoding.Default.GetByteCount(C101.CV101_DT05.Trim())));
                    sData += sStrTemp;
                    sData += C101.CV101_DT06; //  7자리  : 업종코드
                    sData += C101.CV101_DT07; // 15자리  : 수입금액
                    sData += C101.CV101_DT08; // 41자리 : (공란)

                    sw.WriteLine(sData);

                    sData = "";
                    C101.CV101_DT01 = "";  //   2자리 : 자료구분
                    C101.CV101_DT02 = "";  //   4자리 : 서식코드
                    C101.CV101_DT03 = "";  //   1자리 : 수입금액종류구분
                    C101.CV101_DT04 = "";  //  30자리 : 업태명 (재정리)
                    C101.CV101_DT05 = "";  //  50자리 : 종목명 (재정리)
                    C101.CV101_DT06 = "";  //   7자리 : 주 업종코드 (재정리)
                    C101.CV101_DT07 = "";  //  15자리 : 수입금액
                    C101.CV101_DT08 = "";  //  41자리 : 공백
                }

                //  수입구분 1번 체크 사항 (과표표준명세) -- 일반과세자는 2개까지 가능 (27번 항목 처리)
                if (Convert.ToInt64(dt_101.Rows[0]["VS05EV27AMT"].ToString()) > 0)
                {
                    C101.CV101_DT01 = "15";    // 2자리 : 자료구분
                    C101.CV101_DT02 = "V101";  // 4자리 : 서식코드
                    C101.CV101_DT03 = "1";     // 1자리 : 수입금액종류구분
                    C101.CV101_DT04 = dt_101.Rows[0]["VS05EV27BUS"].ToString().Trim();   //  30자리 : 업태명 (재정리)
                    C101.CV101_DT05 = dt_101.Rows[0]["VS05EV27NAM"].ToString().Trim();   //  50자리 : 종목명 (재정리)
                    C101.CV101_DT06 = dt_101.Rows[0]["VS05EV27COD"].ToString().Trim().PadRight(07);   //  7자리  : 업종코드
                    C101.CV101_DT07 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS05EV27AMT"].ToString()));  // 15자리  : 수입금액
                    C101.CV101_DT08 = sFill.PadRight(41);  // 41자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = C101.CV101_DT01;           //  2자리 : 자료구분
                    sData += C101.CV101_DT02;          //  4자리 : 서식코드
                    sData += C101.CV101_DT03;          //  1자리 : 수입금액종류구분
                    sStrTemp = C101.CV101_DT04.Trim(); // 30자리 : 업태명 (한글)
                    sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(C101.CV101_DT04.Trim())));
                    sData += sStrTemp;
                    sStrTemp = C101.CV101_DT05.Trim(); // 50자리 : 종목명 (한글)
                    sStrTemp += new String(Convert.ToChar(" "), (50 - Encoding.Default.GetByteCount(C101.CV101_DT05.Trim())));
                    sData += sStrTemp;
                    sData += C101.CV101_DT06; // 7자리  : 업종코드 
                    sData += C101.CV101_DT07; // 15자리  : 수입금액
                    sData += C101.CV101_DT08; // 41자리  : (공란)

                    sw.WriteLine(sData);

                    sData = "";
                    C101.CV101_DT01 = "";  //   2자리 : 자료구분
                    C101.CV101_DT02 = "";  //   4자리 : 서식코드
                    C101.CV101_DT03 = "";  //   1자리 : 수입금액종류구분
                    C101.CV101_DT04 = "";  //  30자리 : 업태명 (재정리)
                    C101.CV101_DT05 = "";  //  50자리 : 종목명 (재정리)
                    C101.CV101_DT06 = "";  //   7자리 : 주 업종코드 (재정리)
                    C101.CV101_DT07 = "";  //  15자리 : 수입금액
                    C101.CV101_DT08 = "";  //  41자리 : 공백
                }

                //  수입구분 1번 체크 사항 (과표표준명세) -- 일반과세자는 2개까지 가능 (28번 항목 처리)
                if (Convert.ToInt64(dt_101.Rows[0]["VS05EV28AMT"].ToString()) > 0)
                {
                    C101.CV101_DT01 = "15";    // 2자리 : 자료구분
                    C101.CV101_DT02 = "V101";  // 4자리 : 서식코드
                    C101.CV101_DT03 = "1";     // 1자리 : 수입금액종류구분
                    C101.CV101_DT04 = dt_101.Rows[0]["VS05EV28BUS"].ToString().Trim();   //  30자리 : 업태명 (재정리)
                    C101.CV101_DT05 = dt_101.Rows[0]["VS05EV28NAM"].ToString().Trim();   //  50자리 : 종목명 (재정리)
                    C101.CV101_DT06 = dt_101.Rows[0]["VS05EV28COD"].ToString().Trim().PadRight(07);   //  7자리  : 업종코드 (재정리)
                    C101.CV101_DT07 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS05EV28AMT"].ToString()));  // 15자리  : 수입금액
                    C101.CV101_DT08 = sFill.PadRight(41);  // 41자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = C101.CV101_DT01;           //  2자리 : 자료구분
                    sData += C101.CV101_DT02;          //  4자리 : 서식코드
                    sData += C101.CV101_DT03;          //  1자리 : 수입금액종류구분
                    sStrTemp = C101.CV101_DT04.Trim(); // 30자리 : 업태명 
                    sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(C101.CV101_DT04.Trim())));
                    sData += sStrTemp;
                    sStrTemp = C101.CV101_DT05.Trim(); // 50자리 : 종목명 
                    sStrTemp += new String(Convert.ToChar(" "), (50 - Encoding.Default.GetByteCount(C101.CV101_DT05.Trim())));
                    sData += sStrTemp;
                    sData += C101.CV101_DT06; // 7자리  : 업종코드
                    sData += C101.CV101_DT07; // 15자리  : 수입금액
                    sData += C101.CV101_DT08; // 41자리 : (공란)

                    sw.WriteLine(sData);

                    sData = "";
                    C101.CV101_DT01 = "";  //   2자리 : 자료구분
                    C101.CV101_DT02 = "";  //   4자리 : 서식코드
                    C101.CV101_DT03 = "";  //   1자리 : 수입금액종류구분
                    C101.CV101_DT04 = "";  //  30자리 : 업태명 (재정리)
                    C101.CV101_DT05 = "";  //  50자리 : 종목명 (재정리)
                    C101.CV101_DT06 = "";  //   7자리 : 주 업종코드 (재정리)
                    C101.CV101_DT07 = "";  //  15자리 : 수입금액
                    C101.CV101_DT08 = "";  //  41자리 : 공백
                } 
                #endregion

                // ----------------------------------------- 수입구분 [ 2 ] -----------------------------------------------
                //  수입구분 2번 체크 사항 수입금액제외 (일반과세자 부가가치세신고서의 (29)번 항목)
                #region Description : 수입구분 [ 2 ] -- 29 번 항목 체크
                if (Convert.ToInt64(dt_101.Rows[0]["VS05EV29AMT"].ToString()) > 0)
                {
                    C101.CV101_DT01 = "15";    // 2자리 : 자료구분
                    C101.CV101_DT02 = "V101";  // 4자리 : 서식코드
                    C101.CV101_DT03 = "2";     // 1자리 : 수입금액종류구분
                    C101.CV101_DT04 = dt_101.Rows[0]["VS05EV26BUS"].ToString().Trim();   //  30자리 : 주 업태명 (재정리)
                    C101.CV101_DT05 = dt_101.Rows[0]["VS05EV26NAM"].ToString().Trim();   //  50자리 : 주 종목명 (재정리)
                    C101.CV101_DT06 = dt_101.Rows[0]["VS05EV26COD"].ToString().Trim().PadRight(07);   //  7자리  : 주 업종코드 (재정리)
                    C101.CV101_DT07 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS05EV29AMT"].ToString()));  // 15자리  : 수입금액
                    C101.CV101_DT08 = sFill.PadRight(41);  // 41자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = C101.CV101_DT01;           //  2자리 : 자료구분
                    sData += C101.CV101_DT02;          //  4자리 : 서식코드
                    sData += C101.CV101_DT03;          //  1자리 : 수입금액종류구분
                    sStrTemp = C101.CV101_DT04.Trim(); // 30자리 : 업태명 
                    sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(C101.CV101_DT04.Trim())));
                    sData += sStrTemp;
                    sStrTemp = C101.CV101_DT05.Trim(); // 50자리 : 종목명 
                    sStrTemp += new String(Convert.ToChar(" "), (50 - Encoding.Default.GetByteCount(C101.CV101_DT05.Trim())));
                    sData += sStrTemp;
                    sData += C101.CV101_DT06; // 7자리  : 업종코드 
                    sData += C101.CV101_DT07; // 15자리  : 수입금액
                    sData += C101.CV101_DT08; // 41자리 : (공란)

                    sw.WriteLine(sData);

                    sData = "";
                    C101.CV101_DT01 = "";  //   2자리 : 자료구분
                    C101.CV101_DT02 = "";  //   4자리 : 서식코드
                    C101.CV101_DT03 = "";  //   1자리 : 수입금액종류구분
                    C101.CV101_DT04 = "";  //  30자리 : 업태명 (재정리)
                    C101.CV101_DT05 = "";  //  50자리 : 종목명 (재정리)
                    C101.CV101_DT06 = "";  //   7자리 : 주 업종코드 (재정리)
                    C101.CV101_DT07 = "";  //  15자리 : 수입금액
                    C101.CV101_DT08 = "";  //  41자리 : 공백
                } 
                #endregion

                // ----------------------------------------- 수입구분 [ 4 ] -----------------------------------------------
                //  수입구분 4번 체크 사항  ( 신용카드발행공제세액 (일반과세자 부가가치세신고서의 (19)번 항목 )
                #region Description : 수입구분 [ 4 ] -- 19 번 항목 체크
                if (Convert.ToInt64(dt_101.Rows[0]["VS03RE19TAX"].ToString()) > 0)
                {
                    C101.CV101_DT01 = "15";    // 2자리 : 자료구분
                    C101.CV101_DT02 = "V101";  // 4자리 : 서식코드
                    C101.CV101_DT03 = "4";     // 1자리 : 수입금액종류구분
                    C101.CV101_DT04 = dt_101.Rows[0]["VS05EV26BUS"].ToString().Trim();   //  30자리 : 주 업태명 (재정리)
                    C101.CV101_DT05 = dt_101.Rows[0]["VS05EV26NAM"].ToString().Trim();   //  50자리 : 주 종목명 (재정리)
                    C101.CV101_DT06 = dt_101.Rows[0]["VS05EV26COD"].ToString().Trim().PadRight(07);   //  7자리  : 주 업종코드 (재정리)
                    C101.CV101_DT07 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS03RE19TAX"].ToString()));  // 15자리  : 금액
                    C101.CV101_DT08 = sFill.PadRight(41);  // 41자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = C101.CV101_DT01;           //  2자리 : 자료구분
                    sData += C101.CV101_DT02;          //  4자리 : 서식코드
                    sData += C101.CV101_DT03;          //  1자리 : 수입금액종류구분
                    sStrTemp = C101.CV101_DT04.Trim(); // 30자리 : 업태명 
                    sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(C101.CV101_DT04.Trim())));
                    sData += sStrTemp;
                    sStrTemp = C101.CV101_DT05.Trim(); // 50자리 : 종목명 
                    sStrTemp += new String(Convert.ToChar(" "), (50 - Encoding.Default.GetByteCount(C101.CV101_DT05.Trim())));
                    sData += sStrTemp;
                    sData += C101.CV101_DT06; // 7자리  : 업종코드 
                    sData += C101.CV101_DT07; // 15자리  : 수입금액
                    sData += C101.CV101_DT08; // 41자리 : (공란)

                    sw.WriteLine(sData);

                    sData = "";
                    C101.CV101_DT01 = "";  //   2자리 : 자료구분
                    C101.CV101_DT02 = "";  //   4자리 : 서식코드
                    C101.CV101_DT03 = "";  //   1자리 : 수입금액종류구분
                    C101.CV101_DT04 = "";  //  30자리 : 업태명 (재정리)
                    C101.CV101_DT05 = "";  //  50자리 : 종목명 (재정리)
                    C101.CV101_DT06 = "";  //   7자리 : 주 업종코드 (재정리)
                    C101.CV101_DT07 = "";  //  15자리 : 수입금액
                    C101.CV101_DT08 = "";  //  41자리 : 공백
                } 
                #endregion

                // ----------------------------------------- 수입구분 [ 7 ] -----------------------------------------------
                //  수입구분 7번 체크 사항 ( 기타 경감•공제세액( 일반과세자부가가치세신고서의 (18)번 항목 )
                #region Description : 수입구분 [ 7 ] -- 18 번 항목 체크
                if (Convert.ToInt64(dt_101.Rows[0]["VS03RE18TAX"].ToString()) > 0)
                {
                    C101.CV101_DT01 = "15";    // 2자리 : 자료구분
                    C101.CV101_DT02 = "V101";  // 4자리 : 서식코드
                    C101.CV101_DT03 = "7";     // 1자리 : 수입금액종류구분
                    C101.CV101_DT04 = dt_101.Rows[0]["VS05EV26BUS"].ToString().Trim();   //  30자리 : 주 업태명 (재정리)
                    C101.CV101_DT05 = dt_101.Rows[0]["VS05EV26NAM"].ToString().Trim();   //  50자리 : 주 종목명 (재정리)
                    C101.CV101_DT06 = dt_101.Rows[0]["VS05EV26COD"].ToString().Trim().PadRight(07);   //  7자리  : 주 업종코드 (재정리)
                    C101.CV101_DT07 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS03RE18TAX"].ToString()));  // 15자리  : 금액
                    C101.CV101_DT08 = sFill.PadRight(41);  // 41자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = C101.CV101_DT01;           //  2자리 : 자료구분
                    sData += C101.CV101_DT02;          //  4자리 : 서식코드
                    sData += C101.CV101_DT03;          //  1자리 : 수입금액종류구분
                    sStrTemp = C101.CV101_DT04.Trim(); // 30자리 : 업태명 (한글)
                    sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(C101.CV101_DT04.Trim())));
                    sData += sStrTemp;
                    sStrTemp = C101.CV101_DT05.Trim(); // 50자리 : 종목명 (한글)
                    sStrTemp += new String(Convert.ToChar(" "), (50 - Encoding.Default.GetByteCount(C101.CV101_DT05.Trim())));
                    sData += sStrTemp;
                    sData += C101.CV101_DT06; // 7자리  : 업종코드
                    sData += C101.CV101_DT07; // 15자리  : 수입금액
                    sData += C101.CV101_DT08; // 41자리 : (공란)

                    sw.WriteLine(sData);

                    sData = "";
                    C101.CV101_DT01 = "";  //   2자리 : 자료구분
                    C101.CV101_DT02 = "";  //   4자리 : 서식코드
                    C101.CV101_DT03 = "";  //   1자리 : 수입금액종류구분
                    C101.CV101_DT04 = "";  //  30자리 : 업태명 (재정리)
                    C101.CV101_DT05 = "";  //  50자리 : 종목명 (재정리)
                    C101.CV101_DT06 = "";  //   7자리 : 주 업종코드 (재정리)
                    C101.CV101_DT07 = "";  //  15자리 : 수입금액
                    C101.CV101_DT08 = "";  //  41자리 : 공백
                } 
                #endregion

                // ----------------------------------------- 수입구분 [ 8 - 최대 2개 ] ------------------------------------
                //  수입구분 8번 체크 사항 ( 면세사업수입금액 ( 일반과세자 부가가치세신고서의  (76)번,(77) 항목 -- 주 업중 외 2개까지 처리 )
                #region Description : 수입구분 [ 8 ] -- 76 , 77 번 항목 체크
                if (Convert.ToInt64(dt_101.Rows[0]["VS25EX76AMT"].ToString()) > 0)
                {
                    C101.CV101_DT01 = "15";    // 2자리 : 자료구분
                    C101.CV101_DT02 = "V101";  // 4자리 : 서식코드
                    C101.CV101_DT03 = "8";     // 1자리 : 수입금액종류구분
                    C101.CV101_DT04 = dt_101.Rows[0]["VS25EX76BUS"].ToString().Trim();   //  30자리 : 업태명 (재정리)
                    C101.CV101_DT05 = dt_101.Rows[0]["VS25EX76NAM"].ToString().Trim();   //  50자리 : 종목명 (재정리)
                    C101.CV101_DT06 = dt_101.Rows[0]["VS25EX76COD"].ToString().Trim().PadRight(07);   //  7자리  : 업종코드 (재정리)
                    C101.CV101_DT07 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS25EX76AMT"].ToString()));  // 15자리  : 금액
                    C101.CV101_DT08 = sFill.PadRight(41);  // 41자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = C101.CV101_DT01;           //  2자리 : 자료구분
                    sData += C101.CV101_DT02;          //  4자리 : 서식코드
                    sData += C101.CV101_DT03;          //  1자리 : 수입금액종류구분
                    sStrTemp = C101.CV101_DT04.Trim(); // 30자리 : 업태명 (한글)
                    sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(C101.CV101_DT04.Trim())));
                    sData += sStrTemp;
                    sStrTemp = C101.CV101_DT05.Trim(); // 50자리 : 종목명 (한글)
                    sStrTemp += new String(Convert.ToChar(" "), (50 - Encoding.Default.GetByteCount(C101.CV101_DT05.Trim())));
                    sData += sStrTemp;
                    sData += C101.CV101_DT06; // 7자리  : 업종코드
                    sData += C101.CV101_DT07; // 15자리  : 수입금액
                    sData += C101.CV101_DT08; // 41자리 : (공란)

                    sw.WriteLine(sData);

                    sData = "";
                    C101.CV101_DT01 = "";  //   2자리 : 자료구분
                    C101.CV101_DT02 = "";  //   4자리 : 서식코드
                    C101.CV101_DT03 = "";  //   1자리 : 수입금액종류구분
                    C101.CV101_DT04 = "";  //  30자리 : 업태명 (재정리)
                    C101.CV101_DT05 = "";  //  50자리 : 종목명 (재정리)
                    C101.CV101_DT06 = "";  //   7자리 : 주 업종코드 (재정리)
                    C101.CV101_DT07 = "";  //  15자리 : 수입금액
                    C101.CV101_DT08 = "";  //  41자리 : 공백
                }
                // ------------------------------------------------------------------------------------------------------------------
                // ----------------------------------------- 수입구분 [ 8 - 최대 2개 ] -----------------------------------------------
                //  수입구분 8번 체크 사항 ( 면세사업수입금액 ( 일반과세자 부가가치세신고서의  (76)번,(77) 항목 -- 주 업중 외 2개까지 처리 )
                if (Convert.ToInt64(dt_101.Rows[0]["VS25EX77AMT"].ToString()) > 0)
                {
                    C101.CV101_DT01 = "15";    // 2자리 : 자료구분
                    C101.CV101_DT02 = "V101";  // 4자리 : 서식코드
                    C101.CV101_DT03 = "8";     // 1자리 : 수입금액종류구분
                    C101.CV101_DT04 = dt_101.Rows[0]["VS25EX77BUS"].ToString().Trim();   //  30자리 : 업태명 (재정리)
                    C101.CV101_DT05 = dt_101.Rows[0]["VS25EX77NAM"].ToString().Trim();   //  50자리 : 종목명 (재정리)
                    C101.CV101_DT06 = dt_101.Rows[0]["VS25EX77COD"].ToString().Trim().PadRight(07);   //  7자리  : 업종코드 (재정리)
                    C101.CV101_DT07 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS25EX77AMT"].ToString()));  // 15자리  : 금액
                    C101.CV101_DT08 = sFill.PadRight(41);  // 41자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = C101.CV101_DT01;           //  2자리 : 자료구분
                    sData += C101.CV101_DT02;          //  4자리 : 서식코드
                    sData += C101.CV101_DT03;          //  1자리 : 수입금액종류구분
                    sStrTemp = C101.CV101_DT04.Trim(); // 30자리 : 업태명 (한글)
                    sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(C101.CV101_DT04.Trim())));
                    sData += sStrTemp;
                    sStrTemp = C101.CV101_DT05.Trim(); // 50자리 : 종목명 (한글)
                    sStrTemp += new String(Convert.ToChar(" "), (50 - Encoding.Default.GetByteCount(C101.CV101_DT05.Trim())));
                    sData += sStrTemp;
                    sData += C101.CV101_DT06; // 7자리  : 업종코드 
                    sData += C101.CV101_DT07; // 15자리  : 수입금액
                    sData += C101.CV101_DT08; // 41자리 : (공란)

                    sw.WriteLine(sData);

                    sData = "";
                    C101.CV101_DT01 = "";  //   2자리 : 자료구분
                    C101.CV101_DT02 = "";  //   4자리 : 서식코드
                    C101.CV101_DT03 = "";  //   1자리 : 수입금액종류구분
                    C101.CV101_DT04 = "";  //  30자리 : 업태명 (재정리)
                    C101.CV101_DT05 = "";  //  50자리 : 종목명 (재정리)
                    C101.CV101_DT06 = "";  //   7자리 : 주 업종코드 (재정리)
                    C101.CV101_DT07 = "";  //  15자리 : 수입금액
                    C101.CV101_DT08 = "";  //  41자리 : 공백
                } 
                #endregion

                // ----------------------------------------- 수입구분 [ E ] -----------------------------------------------
                //  수입구분 E번 체크 사항 ( 면세사업수입금액 ( 면세수입금액제외(일반과세자 부가가치세신고서의 (78)번 항목 )
                #region Description : 수입구분 [ E ] -- 78번 항목 체크
                if (Convert.ToInt64(dt_101.Rows[0]["VS25EX78AMT"].ToString()) > 0)
                {
                    C101.CV101_DT01 = "15";    // 2자리 : 자료구분
                    C101.CV101_DT02 = "V101";  // 4자리 : 서식코드
                    C101.CV101_DT03 = "8";     // 1자리 : 수입금액종류구분
                    C101.CV101_DT04 = dt_101.Rows[0]["VS05EV26BUS"].ToString().Trim();   //  30자리 : 주 업태명 (재정리)
                    C101.CV101_DT05 = dt_101.Rows[0]["VS05EV26NAM"].ToString().Trim();   //  50자리 : 주 종목명 (재정리)
                    C101.CV101_DT06 = dt_101.Rows[0]["VS05EV26COD"].ToString().Trim().PadRight(07);   //  7자리  : 주 업종코드 (재정리)
                    C101.CV101_DT07 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS25EX78AMT"].ToString()));  // 15자리  : 금액
                    C101.CV101_DT08 = sFill.PadRight(41);  // 41자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = C101.CV101_DT01;           //  2자리 : 자료구분
                    sData += C101.CV101_DT02;          //  4자리 : 서식코드
                    sData += C101.CV101_DT03;          //  1자리 : 수입금액종류구분
                    sStrTemp = C101.CV101_DT04.Trim(); // 30자리 : 업태명 (한글)
                    sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(C101.CV101_DT04.Trim())));
                    sData += sStrTemp;
                    sStrTemp = C101.CV101_DT05.Trim(); // 50자리 : 종목명 (한글)
                    sStrTemp += new String(Convert.ToChar(" "), (50 - Encoding.Default.GetByteCount(C101.CV101_DT05.Trim())));
                    sData += sStrTemp;
                    sData += C101.CV101_DT06; // 7자리  : 업종코드
                    sData += C101.CV101_DT07; // 15자리  : 수입금액
                    sData += C101.CV101_DT08; // 41자리 : (공란)

                    sw.WriteLine(sData);

                } 
                #endregion

            } */

            struct_CV101 C101 = new struct_CV101();

            // 신고서
            this.DbConnector.CommandClear(); // AVSURTAXF 
            this.DbConnector.Attach("TY_P_AC_42437183",
                                    this.DTP01_ELXYYMM.GetValue().ToString(),
                                    this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1), // 신고구분(1기, 2기)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)  // 확정구분(1.예정, 2.확정)
                                    );
            DataTable dt_101 = this.DbConnector.ExecuteDataTable();

            if (dt_101.Rows.Count > 0)
            {
                //  수입구분 1번 체크 사항 (과세표준명세 ( 일반과세자 부가가치세신고서의  (26)번, (27), (28)번 항목)
                // ----------------------------------------- 수입구분 [ 1 - 최대 2개 ] ------------------------------------
                #region Description : 수입구분 [ 1 ] -- 26,27,28 번 항목 체크
                if (Convert.ToInt64(dt_101.Rows[0]["VS05EV26AMT"].ToString()) > 0)
                {
                    C101.CV101_DT01 = "15";    // 2자리 : 자료구분 ==> (15)
                    C101.CV101_DT02 = "I103200";  // 7자리 : 서식코드 ==> (I103200)
                    C101.CV101_DT03 = "01";     // 2자리 : 수입금액종류구분
                    C101.CV101_DT04 = dt_101.Rows[0]["VS05EV26BUS"].ToString().Trim();   //  30자리 : 업태명 (재정리)
                    C101.CV101_DT05 = dt_101.Rows[0]["VS05EV26NAM"].ToString().Trim();   //  50자리 : 종목명 (재정리)
                    C101.CV101_DT06 = dt_101.Rows[0]["VS05EV26COD"].ToString().Trim().PadRight(07);   //  7자리  : 주 업종코드
                    C101.CV101_DT07 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS05EV26AMT"].ToString()));  // 15자리  : 수입금액
                    C101.CV101_DT08 = sFill.PadRight(37);  // 37자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = C101.CV101_DT01;           //  2자리 : 자료구분
                    sData += C101.CV101_DT02;          //  4자리 : 서식코드
                    sData += C101.CV101_DT03;          //  1자리 : 수입금액종류구분
                    sStrTemp = C101.CV101_DT04.Trim(); // 30자리 : 업태명 (한글)
                    sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(C101.CV101_DT04.Trim())));
                    sData += sStrTemp;
                    sStrTemp = C101.CV101_DT05.Trim(); // 50자리 : 종목명 (한글)
                    sStrTemp += new String(Convert.ToChar(" "), (50 - Encoding.Default.GetByteCount(C101.CV101_DT05.Trim())));
                    sData += sStrTemp;
                    sData += C101.CV101_DT06; //  7자리  : 업종코드
                    sData += C101.CV101_DT07; // 15자리  : 수입금액
                    sData += C101.CV101_DT08; // 41자리 : (공란)

                    sw.WriteLine(sData);

                    sData = "";
                    C101.CV101_DT01 = "";  //   2자리 : 자료구분
                    C101.CV101_DT02 = "";  //   4자리 : 서식코드
                    C101.CV101_DT03 = "";  //   1자리 : 수입금액종류구분
                    C101.CV101_DT04 = "";  //  30자리 : 업태명 (재정리)
                    C101.CV101_DT05 = "";  //  50자리 : 종목명 (재정리)
                    C101.CV101_DT06 = "";  //   7자리 : 주 업종코드 (재정리)
                    C101.CV101_DT07 = "";  //  15자리 : 수입금액
                    C101.CV101_DT08 = "";  //  41자리 : 공백
                }

                //  수입구분 1번 체크 사항 (과표표준명세) -- 일반과세자는 2개까지 가능 (27번 항목 처리)
                if (Convert.ToInt64(dt_101.Rows[0]["VS05EV27AMT"].ToString()) > 0)
                {
                    C101.CV101_DT01 = "15";    // 2자리 : 자료구분
                    C101.CV101_DT02 = "I103200";  // 7자리 : 서식코드 ==> (I103200)
                    C101.CV101_DT03 = "01";     // 2자리 : 수입금액종류구분
                    C101.CV101_DT04 = dt_101.Rows[0]["VS05EV27BUS"].ToString().Trim();   //  30자리 : 업태명 (재정리)
                    C101.CV101_DT05 = dt_101.Rows[0]["VS05EV27NAM"].ToString().Trim();   //  50자리 : 종목명 (재정리)
                    C101.CV101_DT06 = dt_101.Rows[0]["VS05EV27COD"].ToString().Trim().PadRight(07);   //  7자리  : 업종코드
                    C101.CV101_DT07 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS05EV27AMT"].ToString()));  // 15자리  : 수입금액
                    C101.CV101_DT08 = sFill.PadRight(37);  // 41자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = C101.CV101_DT01;           //  2자리 : 자료구분
                    sData += C101.CV101_DT02;          //  4자리 : 서식코드
                    sData += C101.CV101_DT03;          //  1자리 : 수입금액종류구분
                    sStrTemp = C101.CV101_DT04.Trim(); // 30자리 : 업태명 (한글)
                    sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(C101.CV101_DT04.Trim())));
                    sData += sStrTemp;
                    sStrTemp = C101.CV101_DT05.Trim(); // 50자리 : 종목명 (한글)
                    sStrTemp += new String(Convert.ToChar(" "), (50 - Encoding.Default.GetByteCount(C101.CV101_DT05.Trim())));
                    sData += sStrTemp;
                    sData += C101.CV101_DT06; // 7자리  : 업종코드 
                    sData += C101.CV101_DT07; // 15자리  : 수입금액
                    sData += C101.CV101_DT08; // 41자리  : (공란)

                    sw.WriteLine(sData);

                    sData = "";
                    C101.CV101_DT01 = "";  //   2자리 : 자료구분
                    C101.CV101_DT02 = "";  //   4자리 : 서식코드
                    C101.CV101_DT03 = "";  //   1자리 : 수입금액종류구분
                    C101.CV101_DT04 = "";  //  30자리 : 업태명 (재정리)
                    C101.CV101_DT05 = "";  //  50자리 : 종목명 (재정리)
                    C101.CV101_DT06 = "";  //   7자리 : 주 업종코드 (재정리)
                    C101.CV101_DT07 = "";  //  15자리 : 수입금액
                    C101.CV101_DT08 = "";  //  41자리 : 공백
                }

                //  수입구분 1번 체크 사항 (과표표준명세) -- 일반과세자는 2개까지 가능 (28번 항목 처리)
                if (Convert.ToInt64(dt_101.Rows[0]["VS05EV28AMT"].ToString()) > 0)
                {
                    C101.CV101_DT01 = "15";    // 2자리 : 자료구분
                    C101.CV101_DT02 = "I103200";  // 7자리 : 서식코드 ==> (I103200)
                    C101.CV101_DT03 = "01";     // 2자리 : 수입금액종류구분
                    C101.CV101_DT04 = dt_101.Rows[0]["VS05EV28BUS"].ToString().Trim();   //  30자리 : 업태명 (재정리)
                    C101.CV101_DT05 = dt_101.Rows[0]["VS05EV28NAM"].ToString().Trim();   //  50자리 : 종목명 (재정리)
                    C101.CV101_DT06 = dt_101.Rows[0]["VS05EV28COD"].ToString().Trim().PadRight(07);   //  7자리  : 업종코드 (재정리)
                    C101.CV101_DT07 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS05EV28AMT"].ToString()));  // 15자리  : 수입금액
                    C101.CV101_DT08 = sFill.PadRight(37);  // 41자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = C101.CV101_DT01;           //  2자리 : 자료구분
                    sData += C101.CV101_DT02;          //  4자리 : 서식코드
                    sData += C101.CV101_DT03;          //  1자리 : 수입금액종류구분
                    sStrTemp = C101.CV101_DT04.Trim(); // 30자리 : 업태명 
                    sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(C101.CV101_DT04.Trim())));
                    sData += sStrTemp;
                    sStrTemp = C101.CV101_DT05.Trim(); // 50자리 : 종목명 
                    sStrTemp += new String(Convert.ToChar(" "), (50 - Encoding.Default.GetByteCount(C101.CV101_DT05.Trim())));
                    sData += sStrTemp;
                    sData += C101.CV101_DT06; // 7자리  : 업종코드
                    sData += C101.CV101_DT07; // 15자리  : 수입금액
                    sData += C101.CV101_DT08; // 41자리 : (공란)

                    sw.WriteLine(sData);

                    sData = "";
                    C101.CV101_DT01 = "";  //   2자리 : 자료구분
                    C101.CV101_DT02 = "";  //   4자리 : 서식코드
                    C101.CV101_DT03 = "";  //   1자리 : 수입금액종류구분
                    C101.CV101_DT04 = "";  //  30자리 : 업태명 (재정리)
                    C101.CV101_DT05 = "";  //  50자리 : 종목명 (재정리)
                    C101.CV101_DT06 = "";  //   7자리 : 주 업종코드 (재정리)
                    C101.CV101_DT07 = "";  //  15자리 : 수입금액
                    C101.CV101_DT08 = "";  //  41자리 : 공백
                }
                #endregion

                // ----------------------------------------- 수입구분 [ 2 ] -----------------------------------------------
                //  수입구분 2번 체크 사항 수입금액제외 (일반과세자 부가가치세신고서의 (29)번 항목)
                #region Description : 수입구분 [ 2 ] -- 29 번 항목 체크
                if (Convert.ToInt64(dt_101.Rows[0]["VS05EV29AMT"].ToString()) > 0)
                {
                    C101.CV101_DT01 = "15";    // 2자리 : 자료구분
                    C101.CV101_DT02 = "I103200";  // 7자리 : 서식코드 ==> (I103200)
                    C101.CV101_DT03 = "02";     // 1자리 : 수입금액종류구분
                    C101.CV101_DT04 = dt_101.Rows[0]["VS05EV26BUS"].ToString().Trim();   //  30자리 : 주 업태명 (재정리)
                    C101.CV101_DT05 = dt_101.Rows[0]["VS05EV26NAM"].ToString().Trim();   //  50자리 : 주 종목명 (재정리)
                    C101.CV101_DT06 = dt_101.Rows[0]["VS05EV26COD"].ToString().Trim().PadRight(07);   //  7자리  : 주 업종코드 (재정리)
                    C101.CV101_DT07 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS05EV29AMT"].ToString()));  // 15자리  : 수입금액
                    C101.CV101_DT08 = sFill.PadRight(37);  // 41자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = C101.CV101_DT01;           //  2자리 : 자료구분
                    sData += C101.CV101_DT02;          //  4자리 : 서식코드
                    sData += C101.CV101_DT03;          //  1자리 : 수입금액종류구분
                    sStrTemp = C101.CV101_DT04.Trim(); // 30자리 : 업태명 
                    sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(C101.CV101_DT04.Trim())));
                    sData += sStrTemp;
                    sStrTemp = C101.CV101_DT05.Trim(); // 50자리 : 종목명 
                    sStrTemp += new String(Convert.ToChar(" "), (50 - Encoding.Default.GetByteCount(C101.CV101_DT05.Trim())));
                    sData += sStrTemp;
                    sData += C101.CV101_DT06; // 7자리  : 업종코드 
                    sData += C101.CV101_DT07; // 15자리  : 수입금액
                    sData += C101.CV101_DT08; // 41자리 : (공란)

                    sw.WriteLine(sData);

                    sData = "";
                    C101.CV101_DT01 = "";  //   2자리 : 자료구분
                    C101.CV101_DT02 = "";  //   4자리 : 서식코드
                    C101.CV101_DT03 = "";  //   1자리 : 수입금액종류구분
                    C101.CV101_DT04 = "";  //  30자리 : 업태명 (재정리)
                    C101.CV101_DT05 = "";  //  50자리 : 종목명 (재정리)
                    C101.CV101_DT06 = "";  //   7자리 : 주 업종코드 (재정리)
                    C101.CV101_DT07 = "";  //  15자리 : 수입금액
                    C101.CV101_DT08 = "";  //  41자리 : 공백
                }
                #endregion

                // ----------------------------------------- 수입구분 [ 4 ] -----------------------------------------------
                //  수입구분 4번 체크 사항  ( 신용카드발행공제세액 (일반과세자 부가가치세신고서의 (19)번 항목 )
                #region Description : 수입구분 [ 4 ] -- 19 번 항목 체크
                if (Convert.ToInt64(dt_101.Rows[0]["VS03RE19TAX"].ToString()) > 0)
                {
                    C101.CV101_DT01 = "15";    // 2자리 : 자료구분
                    C101.CV101_DT02 = "I103200";  // 7자리 : 서식코드(I103200)
                    C101.CV101_DT03 = "04";     // 1자리 : 수입금액종류구분
                    C101.CV101_DT04 = dt_101.Rows[0]["VS05EV26BUS"].ToString().Trim();   //  30자리 : 주 업태명 (재정리)
                    C101.CV101_DT05 = dt_101.Rows[0]["VS05EV26NAM"].ToString().Trim();   //  50자리 : 주 종목명 (재정리)
                    C101.CV101_DT06 = dt_101.Rows[0]["VS05EV26COD"].ToString().Trim().PadRight(07);   //  7자리  : 주 업종코드 (재정리)
                    C101.CV101_DT07 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS03RE19TAX"].ToString()));  // 15자리  : 금액
                    C101.CV101_DT08 = sFill.PadRight(37);  // 41자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = C101.CV101_DT01;           //  2자리 : 자료구분
                    sData += C101.CV101_DT02;          //  4자리 : 서식코드
                    sData += C101.CV101_DT03;          //  1자리 : 수입금액종류구분
                    sStrTemp = C101.CV101_DT04.Trim(); // 30자리 : 업태명 
                    sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(C101.CV101_DT04.Trim())));
                    sData += sStrTemp;
                    sStrTemp = C101.CV101_DT05.Trim(); // 50자리 : 종목명 
                    sStrTemp += new String(Convert.ToChar(" "), (50 - Encoding.Default.GetByteCount(C101.CV101_DT05.Trim())));
                    sData += sStrTemp;
                    sData += C101.CV101_DT06; // 7자리  : 업종코드 
                    sData += C101.CV101_DT07; // 15자리  : 수입금액
                    sData += C101.CV101_DT08; // 41자리 : (공란)

                    sw.WriteLine(sData);

                    sData = "";
                    C101.CV101_DT01 = "";  //   2자리 : 자료구분
                    C101.CV101_DT02 = "";  //   4자리 : 서식코드
                    C101.CV101_DT03 = "";  //   1자리 : 수입금액종류구분
                    C101.CV101_DT04 = "";  //  30자리 : 업태명 (재정리)
                    C101.CV101_DT05 = "";  //  50자리 : 종목명 (재정리)
                    C101.CV101_DT06 = "";  //   7자리 : 주 업종코드 (재정리)
                    C101.CV101_DT07 = "";  //  15자리 : 수입금액
                    C101.CV101_DT08 = "";  //  41자리 : 공백
                }
                #endregion

                // ----------------------------------------- 수입구분 [ 7 ] -----------------------------------------------
                //  수입구분 7번 체크 사항 ( 기타 경감•공제세액( 일반과세자부가가치세신고서의 (18)번 항목 )
                #region Description : 수입구분 [ 7 ] -- 18 번 항목 체크
                if (Convert.ToInt64(dt_101.Rows[0]["VS03RE18TAX"].ToString()) > 0)
                {
                    C101.CV101_DT01 = "15";    // 2자리 : 자료구분
                    C101.CV101_DT02 = "I103200";  // 7자리 : 서식코드(I103200)
                    C101.CV101_DT03 = "07";     // 1자리 : 수입금액종류구분
                    C101.CV101_DT04 = dt_101.Rows[0]["VS05EV26BUS"].ToString().Trim();   //  30자리 : 주 업태명 (재정리)
                    C101.CV101_DT05 = dt_101.Rows[0]["VS05EV26NAM"].ToString().Trim();   //  50자리 : 주 종목명 (재정리)
                    C101.CV101_DT06 = dt_101.Rows[0]["VS05EV26COD"].ToString().Trim().PadRight(07);   //  7자리  : 주 업종코드 (재정리)
                    C101.CV101_DT07 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS03RE18TAX"].ToString()));  // 15자리  : 금액
                    C101.CV101_DT08 = sFill.PadRight(37);  // 41자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = C101.CV101_DT01;           //  2자리 : 자료구분
                    sData += C101.CV101_DT02;          //  4자리 : 서식코드
                    sData += C101.CV101_DT03;          //  1자리 : 수입금액종류구분
                    sStrTemp = C101.CV101_DT04.Trim(); // 30자리 : 업태명 (한글)
                    sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(C101.CV101_DT04.Trim())));
                    sData += sStrTemp;
                    sStrTemp = C101.CV101_DT05.Trim(); // 50자리 : 종목명 (한글)
                    sStrTemp += new String(Convert.ToChar(" "), (50 - Encoding.Default.GetByteCount(C101.CV101_DT05.Trim())));
                    sData += sStrTemp;
                    sData += C101.CV101_DT06; // 7자리  : 업종코드
                    sData += C101.CV101_DT07; // 15자리  : 수입금액
                    sData += C101.CV101_DT08; // 41자리 : (공란)

                    sw.WriteLine(sData);

                    sData = "";
                    C101.CV101_DT01 = "";  //   2자리 : 자료구분
                    C101.CV101_DT02 = "";  //   4자리 : 서식코드
                    C101.CV101_DT03 = "";  //   1자리 : 수입금액종류구분
                    C101.CV101_DT04 = "";  //  30자리 : 업태명 (재정리)
                    C101.CV101_DT05 = "";  //  50자리 : 종목명 (재정리)
                    C101.CV101_DT06 = "";  //   7자리 : 주 업종코드 (재정리)
                    C101.CV101_DT07 = "";  //  15자리 : 수입금액
                    C101.CV101_DT08 = "";  //  41자리 : 공백
                }
                #endregion

                // ----------------------------------------- 수입구분 [ 8 - 최대 2개 ] ------------------------------------
                //  수입구분 8번 체크 사항 ( 면세사업수입금액 ( 일반과세자 부가가치세신고서의  (76)번,(77) 항목 -- 주 업중 외 2개까지 처리 )
                #region Description : 수입구분 [ 8 ] -- 76 , 77 번 항목 체크
                if (Convert.ToInt64(dt_101.Rows[0]["VS25EX76AMT"].ToString()) > 0)
                {
                    C101.CV101_DT01 = "15";    // 2자리 : 자료구분
                    C101.CV101_DT02 = "I103200";  // 7자리 : 서식코드(I103200)
                    C101.CV101_DT03 = "08";     // 1자리 : 수입금액종류구분
                    C101.CV101_DT04 = dt_101.Rows[0]["VS25EX76BUS"].ToString().Trim();   //  30자리 : 업태명 (재정리)
                    C101.CV101_DT05 = dt_101.Rows[0]["VS25EX76NAM"].ToString().Trim();   //  50자리 : 종목명 (재정리)
                    C101.CV101_DT06 = dt_101.Rows[0]["VS25EX76COD"].ToString().Trim().PadRight(07);   //  7자리  : 업종코드 (재정리)
                    C101.CV101_DT07 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS25EX76AMT"].ToString()));  // 15자리  : 금액
                    C101.CV101_DT08 = sFill.PadRight(37);  // 41자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = C101.CV101_DT01;           //  2자리 : 자료구분
                    sData += C101.CV101_DT02;          //  4자리 : 서식코드
                    sData += C101.CV101_DT03;          //  1자리 : 수입금액종류구분
                    sStrTemp = C101.CV101_DT04.Trim(); // 30자리 : 업태명 (한글)
                    sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(C101.CV101_DT04.Trim())));
                    sData += sStrTemp;
                    sStrTemp = C101.CV101_DT05.Trim(); // 50자리 : 종목명 (한글)
                    sStrTemp += new String(Convert.ToChar(" "), (50 - Encoding.Default.GetByteCount(C101.CV101_DT05.Trim())));
                    sData += sStrTemp;
                    sData += C101.CV101_DT06; // 7자리  : 업종코드
                    sData += C101.CV101_DT07; // 15자리  : 수입금액
                    sData += C101.CV101_DT08; // 41자리 : (공란)

                    sw.WriteLine(sData);

                    sData = "";
                    C101.CV101_DT01 = "";  //   2자리 : 자료구분
                    C101.CV101_DT02 = "";  //   4자리 : 서식코드
                    C101.CV101_DT03 = "";  //   1자리 : 수입금액종류구분
                    C101.CV101_DT04 = "";  //  30자리 : 업태명 (재정리)
                    C101.CV101_DT05 = "";  //  50자리 : 종목명 (재정리)
                    C101.CV101_DT06 = "";  //   7자리 : 주 업종코드 (재정리)
                    C101.CV101_DT07 = "";  //  15자리 : 수입금액
                    C101.CV101_DT08 = "";  //  41자리 : 공백
                }
                // ------------------------------------------------------------------------------------------------------------------
                // ----------------------------------------- 수입구분 [ 8 - 최대 2개 ] -----------------------------------------------
                //  수입구분 8번 체크 사항 ( 면세사업수입금액 ( 일반과세자 부가가치세신고서의  (76)번,(77) 항목 -- 주 업중 외 2개까지 처리 )
                if (Convert.ToInt64(dt_101.Rows[0]["VS25EX77AMT"].ToString()) > 0)
                {
                    C101.CV101_DT01 = "15";    // 2자리 : 자료구분
                    C101.CV101_DT02 = "I103200";  // 7자리 : 서식코드(I103200)
                    C101.CV101_DT03 = "08";     // 1자리 : 수입금액종류구분
                    C101.CV101_DT04 = dt_101.Rows[0]["VS25EX77BUS"].ToString().Trim();   //  30자리 : 업태명 (재정리)
                    C101.CV101_DT05 = dt_101.Rows[0]["VS25EX77NAM"].ToString().Trim();   //  50자리 : 종목명 (재정리)
                    C101.CV101_DT06 = dt_101.Rows[0]["VS25EX77COD"].ToString().Trim().PadRight(07);   //  7자리  : 업종코드 (재정리)
                    C101.CV101_DT07 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS25EX77AMT"].ToString()));  // 15자리  : 금액
                    C101.CV101_DT08 = sFill.PadRight(37);  // 41자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = C101.CV101_DT01;           //  2자리 : 자료구분
                    sData += C101.CV101_DT02;          //  4자리 : 서식코드
                    sData += C101.CV101_DT03;          //  1자리 : 수입금액종류구분
                    sStrTemp = C101.CV101_DT04.Trim(); // 30자리 : 업태명 (한글)
                    sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(C101.CV101_DT04.Trim())));
                    sData += sStrTemp;
                    sStrTemp = C101.CV101_DT05.Trim(); // 50자리 : 종목명 (한글)
                    sStrTemp += new String(Convert.ToChar(" "), (50 - Encoding.Default.GetByteCount(C101.CV101_DT05.Trim())));
                    sData += sStrTemp;
                    sData += C101.CV101_DT06; // 7자리  : 업종코드 
                    sData += C101.CV101_DT07; // 15자리  : 수입금액
                    sData += C101.CV101_DT08; // 41자리 : (공란)

                    sw.WriteLine(sData);

                    sData = "";
                    C101.CV101_DT01 = "";  //   2자리 : 자료구분
                    C101.CV101_DT02 = "";  //   4자리 : 서식코드
                    C101.CV101_DT03 = "";  //   1자리 : 수입금액종류구분
                    C101.CV101_DT04 = "";  //  30자리 : 업태명 (재정리)
                    C101.CV101_DT05 = "";  //  50자리 : 종목명 (재정리)
                    C101.CV101_DT06 = "";  //   7자리 : 주 업종코드 (재정리)
                    C101.CV101_DT07 = "";  //  15자리 : 수입금액
                    C101.CV101_DT08 = "";  //  41자리 : 공백
                }
                #endregion

                // ----------------------------------------- 수입구분 [ E ] -----------------------------------------------
                //  수입구분 E번 체크 사항 ( 면세사업수입금액 ( 면세수입금액제외(일반과세자 부가가치세신고서의 (78)번 항목 )
                #region Description : 수입구분 [ E ] -- 78번 항목 체크
                if (Convert.ToInt64(dt_101.Rows[0]["VS25EX78AMT"].ToString()) > 0)
                {
                    C101.CV101_DT01 = "15";    // 2자리 : 자료구분
                    C101.CV101_DT02 = "I103200";  // 7자리 : 서식코드(I103200)
                    C101.CV101_DT03 = "08";     // 1자리 : 수입금액종류구분
                    C101.CV101_DT04 = dt_101.Rows[0]["VS05EV26BUS"].ToString().Trim();   //  30자리 : 주 업태명 (재정리)
                    C101.CV101_DT05 = dt_101.Rows[0]["VS05EV26NAM"].ToString().Trim();   //  50자리 : 주 종목명 (재정리)
                    C101.CV101_DT06 = dt_101.Rows[0]["VS05EV26COD"].ToString().Trim().PadRight(07);   //  7자리  : 주 업종코드 (재정리)
                    C101.CV101_DT07 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS25EX78AMT"].ToString()));  // 15자리  : 금액
                    C101.CV101_DT08 = sFill.PadRight(37);  // 41자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = C101.CV101_DT01;           //  2자리 : 자료구분
                    sData += C101.CV101_DT02;          //  4자리 : 서식코드
                    sData += C101.CV101_DT03;          //  1자리 : 수입금액종류구분
                    sStrTemp = C101.CV101_DT04.Trim(); // 30자리 : 업태명 (한글)
                    sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(C101.CV101_DT04.Trim())));
                    sData += sStrTemp;
                    sStrTemp = C101.CV101_DT05.Trim(); // 50자리 : 종목명 (한글)
                    sStrTemp += new String(Convert.ToChar(" "), (50 - Encoding.Default.GetByteCount(C101.CV101_DT05.Trim())));
                    sData += sStrTemp;
                    sData += C101.CV101_DT06; // 7자리  : 업종코드
                    sData += C101.CV101_DT07; // 15자리  : 수입금액
                    sData += C101.CV101_DT08; // 41자리 : (공란)

                    sw.WriteLine(sData);

                }
                #endregion

            }

        }
        #endregion

        #region Description : 부가가치세 공제감면 신고서 레코드  -- UP_TAX_Create_CV102()
        private void UP_TAX_Create_CV102(StreamWriter sw)
        {
            string sStrTemp = string.Empty;
            string sData = string.Empty;
            string sFill = string.Empty;


            struct_CV102 C102 = new struct_CV102();

            // 신고서
            this.DbConnector.CommandClear(); // AVSURTAXF 
            this.DbConnector.Attach("TY_P_AC_54NG0260",
                                    this.DTP01_ELXYYMM.GetValue().ToString(),
                                    this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1), // 신고구분(1기, 2기)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)  // 확정구분(1.예정, 2.확정)
                                    );
            DataTable dt_101 = this.DbConnector.ExecuteDataTable();

            if (dt_101.Rows.Count > 0)
            {
                // (14) 그 밖의 공제 매입세액 명세
                #region Description : 일반_과세표준신고서 40번 항목(신용카드 매출전표등 수령명세서 제출분 일반매입)
                if (Convert.ToInt64(dt_101.Rows[0]["VS14ET39AMT"].ToString()) > 0)
                {
                    C102.CV102_DT01 = "14";    // 2자리 : 자료구분 ==> (14)
                    C102.CV102_DT02 = "I103200";  // 7자리 : 서식코드 ==> (I103200)
                    C102.CV102_DT03 = "211";     // 3자리 : 공제감면코드
                    C102.CV102_DT04 = "000000000001";  //12자리 '1' 고정
                    C102.CV102_DT05 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS14ET39AMT"].ToString()));  // 15자리  : 공제감면금액
                    C102.CV102_DT06 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS14ET39TAX"].ToString()));  // 15자리  : 공제감면세액
                    C102.CV102_DT07 = sFill.PadRight(46);  // 46자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = C102.CV102_DT01;    
                    sData += C102.CV102_DT02;   
                    sData += C102.CV102_DT03;   
                    sData += C102.CV102_DT04; 
                    sData += C102.CV102_DT05; 
                    sData += C102.CV102_DT06;
                    sData += C102.CV102_DT07;

                    sw.WriteLine(sData);

                    sData = "";
                    C102.CV102_DT01 = "";  //   2자리 : 자료구분
                    C102.CV102_DT02 = "";  //   7자리 : 서식코드
                    C102.CV102_DT03 = "";  //   3자리 : 공제감면코드
                    C102.CV102_DT04 = "";  //  12자리 : 등록일련번호
                    C102.CV102_DT05 = "";  //  15자리 : 공제감면금액
                    C102.CV102_DT06 = "";  //  15자리 : 공제감면세액
                    C102.CV102_DT07 = "";  //  46자리 : 공란(46)                    
                }              
                #endregion

                #region Description : 일반_과세표준신고서 41번 항목(신용카드 매출전표등 수령명세서 제출분 고정자산매입)
                if (Convert.ToInt64(dt_101.Rows[0]["VS14ET40AMT"].ToString()) > 0)
                {
                    C102.CV102_DT01 = "14";    // 2자리 : 자료구분 ==> (14)
                    C102.CV102_DT02 = "I103200";  // 7자리 : 서식코드 ==> (I103200)
                    C102.CV102_DT03 = "212";     // 3자리 : 공제감면코드
                    C102.CV102_DT04 = "000000000001";  //12자리 '1' 고정
                    C102.CV102_DT05 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS14ET40AMT"].ToString()));  // 15자리  : 공제감면금액
                    C102.CV102_DT06 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS14ET40TAX"].ToString()));  // 15자리  : 공제감면세액
                    C102.CV102_DT07 = sFill.PadRight(46);  // 46자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = C102.CV102_DT01;
                    sData += C102.CV102_DT02;
                    sData += C102.CV102_DT03;
                    sData += C102.CV102_DT04;
                    sData += C102.CV102_DT05;
                    sData += C102.CV102_DT06;
                    sData += C102.CV102_DT07;

                    sw.WriteLine(sData);

                    sData = "";
                    C102.CV102_DT01 = "";  //   2자리 : 자료구분
                    C102.CV102_DT02 = "";  //   7자리 : 서식코드
                    C102.CV102_DT03 = "";  //   3자리 : 공제감면코드
                    C102.CV102_DT04 = "";  //  12자리 : 등록일련번호
                    C102.CV102_DT05 = "";  //  15자리 : 공제감면금액
                    C102.CV102_DT06 = "";  //  15자리 : 공제감면세액
                    C102.CV102_DT07 = "";  //  46자리 : 공란(46)                    
                }
                #endregion

                #region Description : 일반_과세표준신고서 42번 항목(의제매입 세액 )
                if (Convert.ToInt64(dt_101.Rows[0]["VS14ET41AMT"].ToString()) > 0)
                {
                    C102.CV102_DT01 = "14";    // 2자리 : 자료구분 ==> (14)
                    C102.CV102_DT02 = "I103200";  // 7자리 : 서식코드 ==> (I103200)
                    C102.CV102_DT03 = "230";     // 3자리 : 공제감면코드
                    C102.CV102_DT04 = "000000000001";  //12자리 '1' 고정
                    C102.CV102_DT05 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS14ET41AMT"].ToString()));  // 15자리  : 공제감면금액
                    C102.CV102_DT06 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS14ET41TAX"].ToString()));  // 15자리  : 공제감면세액
                    C102.CV102_DT07 = sFill.PadRight(46);  // 46자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = C102.CV102_DT01;
                    sData += C102.CV102_DT02;
                    sData += C102.CV102_DT03;
                    sData += C102.CV102_DT04;
                    sData += C102.CV102_DT05;
                    sData += C102.CV102_DT06;
                    sData += C102.CV102_DT07;

                    sw.WriteLine(sData);

                    sData = "";
                    C102.CV102_DT01 = "";  //   2자리 : 자료구분
                    C102.CV102_DT02 = "";  //   7자리 : 서식코드
                    C102.CV102_DT03 = "";  //   3자리 : 공제감면코드
                    C102.CV102_DT04 = "";  //  12자리 : 등록일련번호
                    C102.CV102_DT05 = "";  //  15자리 : 공제감면금액
                    C102.CV102_DT06 = "";  //  15자리 : 공제감면세액
                    C102.CV102_DT07 = "";  //  46자리 : 공란(46)                    
                }
                #endregion

                #region Description : 일반_과세표준신고서 43번 항목(재활용 페자원등 매입세액 )
                if (Convert.ToInt64(dt_101.Rows[0]["VS14ET42AMT"].ToString()) > 0)
                {
                    C102.CV102_DT01 = "14";    // 2자리 : 자료구분 ==> (14)
                    C102.CV102_DT02 = "I103200";  // 7자리 : 서식코드 ==> (I103200)
                    C102.CV102_DT03 = "270";     // 3자리 : 공제감면코드
                    C102.CV102_DT04 = "000000000001";  //12자리 '1' 고정
                    C102.CV102_DT05 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS14ET42AMT"].ToString()));  // 15자리  : 공제감면금액
                    C102.CV102_DT06 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS14ET42TAX"].ToString()));  // 15자리  : 공제감면세액
                    C102.CV102_DT07 = sFill.PadRight(46);  // 46자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = C102.CV102_DT01;
                    sData += C102.CV102_DT02;
                    sData += C102.CV102_DT03;
                    sData += C102.CV102_DT04;
                    sData += C102.CV102_DT05;
                    sData += C102.CV102_DT06;
                    sData += C102.CV102_DT07;

                    sw.WriteLine(sData);

                    sData = "";
                    C102.CV102_DT01 = "";  //   2자리 : 자료구분
                    C102.CV102_DT02 = "";  //   7자리 : 서식코드
                    C102.CV102_DT03 = "";  //   3자리 : 공제감면코드
                    C102.CV102_DT04 = "";  //  12자리 : 등록일련번호
                    C102.CV102_DT05 = "";  //  15자리 : 공제감면금액
                    C102.CV102_DT06 = "";  //  15자리 : 공제감면세액
                    C102.CV102_DT07 = "";  //  46자리 : 공란(46)                    
                }
                #endregion

                #region Description : 일반_과세표준신고서 44번 항목(과세사업전환 매입세액)
                if (Convert.ToInt64(dt_101.Rows[0]["VS14ET44TAX"].ToString()) > 0)
                {
                    C102.CV102_DT01 = "14";    // 2자리 : 자료구분 ==> (14)
                    C102.CV102_DT02 = "I103200";  // 7자리 : 서식코드 ==> (I103200)
                    C102.CV102_DT03 = "291";     // 3자리 : 공제감면코드
                    C102.CV102_DT04 = "000000000001";  //12자리 '1' 고정
                    C102.CV102_DT05 = "000000000000000";  // 15자리  : 공제감면금액
                    C102.CV102_DT06 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS14ET44TAX"].ToString()));  // 15자리  : 공제감면세액
                    C102.CV102_DT07 = sFill.PadRight(46);  // 46자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = C102.CV102_DT01;
                    sData += C102.CV102_DT02;
                    sData += C102.CV102_DT03;
                    sData += C102.CV102_DT04;
                    sData += C102.CV102_DT05;
                    sData += C102.CV102_DT06;
                    sData += C102.CV102_DT07;

                    sw.WriteLine(sData);

                    sData = "";
                    C102.CV102_DT01 = "";  //   2자리 : 자료구분
                    C102.CV102_DT02 = "";  //   7자리 : 서식코드
                    C102.CV102_DT03 = "";  //   3자리 : 공제감면코드
                    C102.CV102_DT04 = "";  //  12자리 : 등록일련번호
                    C102.CV102_DT05 = "";  //  15자리 : 공제감면금액
                    C102.CV102_DT06 = "";  //  15자리 : 공제감면세액
                    C102.CV102_DT07 = "";  //  46자리 : 공란(46)                    
                }
                #endregion

                #region Description : 일반_과세표준신고서 45번 항목(재고매입세액)
                if (Convert.ToInt64(dt_101.Rows[0]["VS14ET45TAX"].ToString()) > 0)
                {
                    C102.CV102_DT01 = "14";    // 2자리 : 자료구분 ==> (14)
                    C102.CV102_DT02 = "I103200";  // 7자리 : 서식코드 ==> (I103200)
                    C102.CV102_DT03 = "292";     // 3자리 : 공제감면코드
                    C102.CV102_DT04 = "000000000001";  //12자리 '1' 고정
                    C102.CV102_DT05 = "000000000000000";  // 15자리  : 공제감면금액
                    C102.CV102_DT06 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS14ET45TAX"].ToString()));  // 15자리  : 공제감면세액
                    C102.CV102_DT07 = sFill.PadRight(46);  // 46자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = C102.CV102_DT01;
                    sData += C102.CV102_DT02;
                    sData += C102.CV102_DT03;
                    sData += C102.CV102_DT04;
                    sData += C102.CV102_DT05;
                    sData += C102.CV102_DT06;
                    sData += C102.CV102_DT07;

                    sw.WriteLine(sData);

                    sData = "";
                    C102.CV102_DT01 = "";  //   2자리 : 자료구분
                    C102.CV102_DT02 = "";  //   7자리 : 서식코드
                    C102.CV102_DT03 = "";  //   3자리 : 공제감면코드
                    C102.CV102_DT04 = "";  //  12자리 : 등록일련번호
                    C102.CV102_DT05 = "";  //  15자리 : 공제감면금액
                    C102.CV102_DT06 = "";  //  15자리 : 공제감면세액
                    C102.CV102_DT07 = "";  //  46자리 : 공란(46)                    
                }
                #endregion

                #region Description : 일반_과세표준신고서 46번 항목(변재대손세액)
                if (Convert.ToInt64(dt_101.Rows[0]["VS14ET46TAX"].ToString()) > 0)
                {
                    C102.CV102_DT01 = "14";    // 2자리 : 자료구분 ==> (14)
                    C102.CV102_DT02 = "I103200";  // 7자리 : 서식코드 ==> (I103200)
                    C102.CV102_DT03 = "293";     // 3자리 : 공제감면코드
                    C102.CV102_DT04 = "000000000001";  //12자리 '1' 고정
                    C102.CV102_DT05 = "000000000000000";  // 15자리  : 공제감면금액
                    C102.CV102_DT06 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS14ET46TAX"].ToString()));  // 15자리  : 공제감면세액
                    C102.CV102_DT07 = sFill.PadRight(46);  // 46자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = C102.CV102_DT01;
                    sData += C102.CV102_DT02;
                    sData += C102.CV102_DT03;
                    sData += C102.CV102_DT04;
                    sData += C102.CV102_DT05;
                    sData += C102.CV102_DT06;
                    sData += C102.CV102_DT07;

                    sw.WriteLine(sData);

                    sData = "";
                    C102.CV102_DT01 = "";  //   2자리 : 자료구분
                    C102.CV102_DT02 = "";  //   7자리 : 서식코드
                    C102.CV102_DT03 = "";  //   3자리 : 공제감면코드
                    C102.CV102_DT04 = "";  //  12자리 : 등록일련번호
                    C102.CV102_DT05 = "";  //  15자리 : 공제감면금액
                    C102.CV102_DT06 = "";  //  15자리 : 공제감면세액
                    C102.CV102_DT07 = "";  //  46자리 : 공란(46)                    
                }
                #endregion

                #region Description : 일반_과세표준신고서 47번 항목(외국인관광객에 대한 환급세액)
                if (Convert.ToInt64(dt_101.Rows[0]["VS14NT47TAX"].ToString()) > 0)
                {
                    C102.CV102_DT01 = "14";    // 2자리 : 자료구분 ==> (14)
                    C102.CV102_DT02 = "I103200";  // 7자리 : 서식코드 ==> (I103200)
                    C102.CV102_DT03 = "294";     // 3자리 : 공제감면코드
                    C102.CV102_DT04 = "000000000001";  //12자리 '1' 고정
                    C102.CV102_DT05 = "000000000000000";  // 15자리  : 공제감면금액
                    C102.CV102_DT06 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS14NT47TAX"].ToString()));  // 15자리  : 공제감면세액
                    C102.CV102_DT07 = sFill.PadRight(46);  // 46자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = C102.CV102_DT01;
                    sData += C102.CV102_DT02;
                    sData += C102.CV102_DT03;
                    sData += C102.CV102_DT04;
                    sData += C102.CV102_DT05;
                    sData += C102.CV102_DT06;
                    sData += C102.CV102_DT07;

                    sw.WriteLine(sData);

                    sData = "";
                    C102.CV102_DT01 = "";  //   2자리 : 자료구분
                    C102.CV102_DT02 = "";  //   7자리 : 서식코드
                    C102.CV102_DT03 = "";  //   3자리 : 공제감면코드
                    C102.CV102_DT04 = "";  //  12자리 : 등록일련번호
                    C102.CV102_DT05 = "";  //  15자리 : 공제감면금액
                    C102.CV102_DT06 = "";  //  15자리 : 공제감면세액
                    C102.CV102_DT07 = "";  //  46자리 : 공란(46)                    
                }
                #endregion

                // (18) 그 밖의 경감.공제 세액 명세
                #region Description : 일반_과세표준신고서 53번 항목(전자신고 세액 공제)
                if (Convert.ToInt64(dt_101.Rows[0]["VS18BE52TAX"].ToString()) > 0)
                {
                    C102.CV102_DT01 = "14";    // 2자리 : 자료구분 ==> (14)
                    C102.CV102_DT02 = "I103200";  // 7자리 : 서식코드 ==> (I103200)
                    C102.CV102_DT03 = "310";     // 3자리 : 공제감면코드
                    C102.CV102_DT04 = "000000000001";  //12자리 '1' 고정
                    C102.CV102_DT05 = "000000000000000";  // 15자리  : 공제감면금액
                    C102.CV102_DT06 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS18BE52TAX"].ToString()));  // 15자리  : 공제감면세액
                    C102.CV102_DT07 = sFill.PadRight(46);  // 46자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = C102.CV102_DT01;
                    sData += C102.CV102_DT02;
                    sData += C102.CV102_DT03;
                    sData += C102.CV102_DT04;
                    sData += C102.CV102_DT05;
                    sData += C102.CV102_DT06;
                    sData += C102.CV102_DT07;

                    sw.WriteLine(sData);

                    sData = "";
                    C102.CV102_DT01 = "";  //   2자리 : 자료구분
                    C102.CV102_DT02 = "";  //   7자리 : 서식코드
                    C102.CV102_DT03 = "";  //   3자리 : 공제감면코드
                    C102.CV102_DT04 = "";  //  12자리 : 등록일련번호
                    C102.CV102_DT05 = "";  //  15자리 : 공제감면금액
                    C102.CV102_DT06 = "";  //  15자리 : 공제감면세액
                    C102.CV102_DT07 = "";  //  46자리 : 공란(46)                    
                }
                #endregion

                #region Description : 일반_과세표준신고서 54번 항목(전자세금계산서 발급세액)
                if (Convert.ToInt64(dt_101.Rows[0]["VS18BE53TAX"].ToString()) > 0)
                {
                    C102.CV102_DT01 = "14";    // 2자리 : 자료구분 ==> (14)
                    C102.CV102_DT02 = "I103200";  // 7자리 : 서식코드 ==> (I103200)
                    C102.CV102_DT03 = "321";     // 3자리 : 공제감면코드
                    C102.CV102_DT04 = "000000000001";  //12자리 '1' 고정
                    C102.CV102_DT05 = "000000000000000";  // 15자리  : 공제감면금액
                    C102.CV102_DT06 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS18BE53TAX"].ToString()));  // 15자리  : 공제감면세액
                    C102.CV102_DT07 = sFill.PadRight(46);  // 46자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = C102.CV102_DT01;
                    sData += C102.CV102_DT02;
                    sData += C102.CV102_DT03;
                    sData += C102.CV102_DT04;
                    sData += C102.CV102_DT05;
                    sData += C102.CV102_DT06;
                    sData += C102.CV102_DT07;

                    sw.WriteLine(sData);

                    sData = "";
                    C102.CV102_DT01 = "";  //   2자리 : 자료구분
                    C102.CV102_DT02 = "";  //   7자리 : 서식코드
                    C102.CV102_DT03 = "";  //   3자리 : 공제감면코드
                    C102.CV102_DT04 = "";  //  12자리 : 등록일련번호
                    C102.CV102_DT05 = "";  //  15자리 : 공제감면금액
                    C102.CV102_DT06 = "";  //  15자리 : 공제감면세액
                    C102.CV102_DT07 = "";  //  46자리 : 공란(46)                    
                }
                #endregion

                #region Description : 일반_과세표준신고서 55번 항목(택시운송사업자 경감세액)
                if (Convert.ToInt64(dt_101.Rows[0]["VS18BE54TAX"].ToString()) > 0)
                {
                    C102.CV102_DT01 = "14";    // 2자리 : 자료구분 ==> (14)
                    C102.CV102_DT02 = "I103200";  // 7자리 : 서식코드 ==> (I103200)
                    C102.CV102_DT03 = "331";     // 3자리 : 공제감면코드
                    C102.CV102_DT04 = "000000000001";  //12자리 '1' 고정
                    C102.CV102_DT05 = "000000000000000";  // 15자리  : 공제감면금액
                    C102.CV102_DT06 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS18BE54TAX"].ToString()));  // 15자리  : 공제감면세액
                    C102.CV102_DT07 = sFill.PadRight(46);  // 46자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = C102.CV102_DT01;
                    sData += C102.CV102_DT02;
                    sData += C102.CV102_DT03;
                    sData += C102.CV102_DT04;
                    sData += C102.CV102_DT05;
                    sData += C102.CV102_DT06;
                    sData += C102.CV102_DT07;

                    sw.WriteLine(sData);

                    sData = "";
                    C102.CV102_DT01 = "";  //   2자리 : 자료구분
                    C102.CV102_DT02 = "";  //   7자리 : 서식코드
                    C102.CV102_DT03 = "";  //   3자리 : 공제감면코드
                    C102.CV102_DT04 = "";  //  12자리 : 등록일련번호
                    C102.CV102_DT05 = "";  //  15자리 : 공제감면금액
                    C102.CV102_DT06 = "";  //  15자리 : 공제감면세액
                    C102.CV102_DT07 = "";  //  46자리 : 공란(46)                    
                }
                #endregion

                #region Description : 일반_과세표준신고서 56번 항목(현금영수증 사업자 세액)
                if (Convert.ToInt64(dt_101.Rows[0]["VS18BE56TAX"].ToString()) > 0)
                {
                    C102.CV102_DT01 = "14";    // 2자리 : 자료구분 ==> (14)
                    C102.CV102_DT02 = "I103200";  // 7자리 : 서식코드 ==> (I103200)
                    C102.CV102_DT03 = "351";     // 3자리 : 공제감면코드
                    C102.CV102_DT04 = "000000000001";  //12자리 '1' 고정
                    C102.CV102_DT05 = "000000000000000";  // 15자리  : 공제감면금액
                    C102.CV102_DT06 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS18BE56TAX"].ToString()));  // 15자리  : 공제감면세액
                    C102.CV102_DT07 = sFill.PadRight(46);  // 46자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = C102.CV102_DT01;
                    sData += C102.CV102_DT02;
                    sData += C102.CV102_DT03;
                    sData += C102.CV102_DT04;
                    sData += C102.CV102_DT05;
                    sData += C102.CV102_DT06;
                    sData += C102.CV102_DT07;

                    sw.WriteLine(sData);

                    sData = "";
                    C102.CV102_DT01 = "";  //   2자리 : 자료구분
                    C102.CV102_DT02 = "";  //   7자리 : 서식코드
                    C102.CV102_DT03 = "";  //   3자리 : 공제감면코드
                    C102.CV102_DT04 = "";  //  12자리 : 등록일련번호
                    C102.CV102_DT05 = "";  //  15자리 : 공제감면금액
                    C102.CV102_DT06 = "";  //  15자리 : 공제감면세액
                    C102.CV102_DT07 = "";  //  46자리 : 공란(46)                    
                }
                #endregion

                #region Description : 일반_과세표준신고서 57번 항목(기타공제)
                if (Convert.ToInt64(dt_101.Rows[0]["VS18BE57TAX"].ToString()) > 0)
                {
                    C102.CV102_DT01 = "14";    // 2자리 : 자료구분 ==> (14)
                    C102.CV102_DT02 = "I103200";  // 7자리 : 서식코드 ==> (I103200)
                    C102.CV102_DT03 = "361";     // 3자리 : 공제감면코드
                    C102.CV102_DT04 = "000000000001";  //12자리 '1' 고정
                    C102.CV102_DT05 = "000000000000000";  // 15자리  : 공제감면금액
                    C102.CV102_DT06 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS18BE57TAX"].ToString()));  // 15자리  : 공제감면세액
                    C102.CV102_DT07 = sFill.PadRight(46);  // 46자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = C102.CV102_DT01;
                    sData += C102.CV102_DT02;
                    sData += C102.CV102_DT03;
                    sData += C102.CV102_DT04;
                    sData += C102.CV102_DT05;
                    sData += C102.CV102_DT06;
                    sData += C102.CV102_DT07;

                    sw.WriteLine(sData);

                    sData = "";
                    C102.CV102_DT01 = "";  //   2자리 : 자료구분
                    C102.CV102_DT02 = "";  //   7자리 : 서식코드
                    C102.CV102_DT03 = "";  //   3자리 : 공제감면코드
                    C102.CV102_DT04 = "";  //  12자리 : 등록일련번호
                    C102.CV102_DT05 = "";  //  15자리 : 공제감면금액
                    C102.CV102_DT06 = "";  //  15자리 : 공제감면세액
                    C102.CV102_DT07 = "";  //  46자리 : 공란(46)                    
                }
                #endregion

                #region Description : 일반_과세표준신고서 19번 항목(신용카드 매출전표등 발행공제 등)
                if (Convert.ToInt64(dt_101.Rows[0]["VS03RE19TAX"].ToString()) > 0)
                {
                    C102.CV102_DT01 = "14";    // 2자리 : 자료구분 ==> (14)
                    C102.CV102_DT02 = "I103200";  // 7자리 : 서식코드 ==> (I103200)
                    C102.CV102_DT03 = "410";     // 3자리 : 공제감면코드
                    C102.CV102_DT04 = "000000000001";  //12자리 '1' 고정
                    C102.CV102_DT05 = "000000000000000";  // 15자리  : 공제감면금액
                    C102.CV102_DT06 = string.Format("{0:D15}", Convert.ToInt64(dt_101.Rows[0]["VS03RE19TAX"].ToString()));  // 15자리  : 공제감면세액
                    C102.CV102_DT07 = sFill.PadRight(46);  // 46자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = C102.CV102_DT01;
                    sData += C102.CV102_DT02;
                    sData += C102.CV102_DT03;
                    sData += C102.CV102_DT04;
                    sData += C102.CV102_DT05;
                    sData += C102.CV102_DT06;
                    sData += C102.CV102_DT07;

                    sw.WriteLine(sData);

                    sData = "";
                    C102.CV102_DT01 = "";  //   2자리 : 자료구분
                    C102.CV102_DT02 = "";  //   7자리 : 서식코드
                    C102.CV102_DT03 = "";  //   3자리 : 공제감면코드
                    C102.CV102_DT04 = "";  //  12자리 : 등록일련번호
                    C102.CV102_DT05 = "";  //  15자리 : 공제감면금액
                    C102.CV102_DT06 = "";  //  15자리 : 공제감면세액
                    C102.CV102_DT07 = "";  //  46자리 : 공란(46)                    
                }
                #endregion

            }

        }
        #endregion

        #region Description : 영세율첨부서류 제출 명세서 레코드 생성 (V106) -- UP_TAX_Create_V106()
        private void UP_TAX_Create_V106(StreamWriter sw)
        {
            string sStrTemp = string.Empty;
            string sData = string.Empty;
            string sFill = string.Empty;

            /*
            struct_DV106 V101 = new struct_DV106();

            this.DbConnector.CommandClear(); // AVZEROTAXF
            this.DbConnector.Attach("TY_P_AC_41M64145",
                                    this.DTP01_ELXYYMM.GetValue().ToString(),
                                    this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)   // 확정구분(1.예정, 2.확정)
                                    );
            DataTable dt_101 = this.DbConnector.ExecuteDataTable();

            if (dt_101.Rows.Count > 0)
            {
                for (int i = 0; i < dt_101.Rows.Count; i++)
                {
                    V101.DV106_DT01 = dt_101.Rows[i]["VSDT01"].ToString(); //  2자리 (문자) : 자료구분 ==> (17)
                    V101.DV106_DT02 = dt_101.Rows[i]["VSDT02"].ToString(); //  4자리 (문자) : 서식코드 ==> (V106)
                    V101.DV106_DT03 = dt_101.Rows[i]["VSDT03"].ToString().PadRight(02); //  2자리 (문자) : 제출사유코드
                    V101.DV106_DT04 = dt_101.Rows[i]["VSDT04"].ToString(); // 60자리 (문자) : 제출사유
                    V101.DV106_DT05 = dt_101.Rows[i]["VSDT05"].ToString(); //  6자리 (문자) : 일련번호
                    V101.DV106_DT06 = dt_101.Rows[i]["VSDT06"].ToString(); // 40자리 (문자) : 서류명
                    V101.DV106_DT07 = dt_101.Rows[i]["VSDT07"].ToString(); // 20자리 (문자) : 발급자
                    V101.DV106_DT08 = dt_101.Rows[i]["VSDT08"].ToString().PadRight(08); //  8자리 (문자) : 발급일자
                    V101.DV106_DT09 = dt_101.Rows[i]["VSDT09"].ToString().PadRight(08); //  8자리 (문자) : 선적일자
                    V101.DV106_DT10 = dt_101.Rows[i]["VSDT10"].ToString().PadRight(03); //  3자리 (문자) : 통화코드

                    V101.DV106_DT11 = dt_101.Rows[i]["VSDT11"].ToString();    // 9.4자리 : 환율
                    V101.DV106_DT12 = dt_101.Rows[i]["VSDT12"].ToString();    // 15.2자리 : 당기체출금액(외화)
                    V101.DV106_DT13 = UP_Minus_Conv_Fill(dt_101.Rows[i]["VSDT13"].ToString().Trim(), 15);    // 15자리 : 당기제출금액(원화)
                    V101.DV106_DT14 = dt_101.Rows[i]["VSDT14"].ToString();    // 15.2자리 : 당기신고해당분(외화)
                    V101.DV106_DT15 = UP_Minus_Conv_Fill(dt_101.Rows[i]["VSDT15"].ToString().Trim(), 15);    // 15자리 : 당기신고해당분(원화)
                    V101.DV106_DT16 = sFill.PadRight(28);  // 28자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = V101.DV106_DT01;
                    sData += V101.DV106_DT02;
                    sData += V101.DV106_DT03;

                    sStrTemp = V101.DV106_DT04.Trim(); // 04: 60자리 (문자) : 제출사유
                    sStrTemp += new String(Convert.ToChar(" "), (60 - Encoding.Default.GetByteCount(V101.DV106_DT04.Trim())));
                    sData += sStrTemp;

                    sStrTemp = V101.DV106_DT05.Trim(); // 05: 6자리 (문자) : 일련번호
                    sStrTemp += new String(Convert.ToChar(" "), (6 - Encoding.Default.GetByteCount(V101.DV106_DT05.Trim())));
                    sData += sStrTemp;

                    sStrTemp = V101.DV106_DT06.Trim(); // 06: 40자리 (문자) : 서류명
                    sStrTemp += new String(Convert.ToChar(" "), (40 - Encoding.Default.GetByteCount(V101.DV106_DT06.Trim())));
                    sData += sStrTemp;

                    sStrTemp = V101.DV106_DT07.Trim(); // 07: 20자리 (문자) : 발급자
                    sStrTemp += new String(Convert.ToChar(" "), (20 - Encoding.Default.GetByteCount(V101.DV106_DT07.Trim())));
                    sData += sStrTemp;

                    sData += V101.DV106_DT08;
                    sData += V101.DV106_DT09;
                    sData += V101.DV106_DT10;

                    sData += V101.DV106_DT11;
                    sData += V101.DV106_DT12;
                    sData += V101.DV106_DT13;
                    sData += V101.DV106_DT14;
                    sData += V101.DV106_DT15;
                    sData += V101.DV106_DT16; // 16 : 28자리 공백

                    sw.WriteLine(sData);

                }; //for..end
            } */

            //2015년 변경
            struct_DV106 V101 = new struct_DV106();

            this.DbConnector.CommandClear(); // AVZEROTAXF
            this.DbConnector.Attach("TY_P_AC_41M64145",
                                    this.DTP01_ELXYYMM.GetValue().ToString(),
                                    this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)   // 확정구분(1.예정, 2.확정)
                                    );
            DataTable dt_101 = this.DbConnector.ExecuteDataTable();

            if (dt_101.Rows.Count > 0)
            {
                for (int i = 0; i < dt_101.Rows.Count; i++)
                {
                    V101.DV106_DT01 = dt_101.Rows[i]["VSDT01"].ToString(); //  2자리 (문자) : 자료구분 ==> (17)
                    V101.DV106_DT02 = dt_101.Rows[i]["VSDT02"].ToString(); //  7자리 (문자) : 서식코드 ==> (I105800)
                    V101.DV106_DT03 = dt_101.Rows[i]["VSDT03"].ToString().PadRight(02); //  2자리 (문자) : 제출사유코드
                    V101.DV106_DT04 = dt_101.Rows[i]["VSDT04"].ToString(); // 60자리 (문자) : 제출사유
                    V101.DV106_DT05 = dt_101.Rows[i]["VSDT05"].ToString(); //  6자리 (문자) : 일련번호
                    V101.DV106_DT06 = dt_101.Rows[i]["VSDT06"].ToString(); // 40자리 (문자) : 서류명
                    V101.DV106_DT07 = dt_101.Rows[i]["VSDT07"].ToString(); // 20자리 (문자) : 발급자
                    V101.DV106_DT08 = dt_101.Rows[i]["VSDT08"].ToString().PadRight(08); //  8자리 (문자) : 발급일자
                    V101.DV106_DT09 = dt_101.Rows[i]["VSDT09"].ToString().PadRight(08); //  8자리 (문자) : 선적일자
                    V101.DV106_DT10 = dt_101.Rows[i]["VSDT10"].ToString().PadRight(03); //  3자리 (문자) : 통화코드

                    V101.DV106_DT11 = dt_101.Rows[i]["VSDT11"].ToString();    // 9.4자리 : 환율
                    V101.DV106_DT12 = dt_101.Rows[i]["VSDT12"].ToString();    // 15.2자리 : 당기체출금액(외화)
                    V101.DV106_DT13 = UP_Minus_Conv_Fill(dt_101.Rows[i]["VSDT13"].ToString().Trim(), 15);    // 15자리 : 당기제출금액(원화)
                    V101.DV106_DT14 = dt_101.Rows[i]["VSDT14"].ToString();    // 15.2자리 : 당기신고해당분(외화)
                    V101.DV106_DT15 = UP_Minus_Conv_Fill(dt_101.Rows[i]["VSDT15"].ToString().Trim(), 15);    // 15자리 : 당기신고해당분(원화)
                    V101.DV106_DT16 = sFill.PadRight(25);  // 25자리 : 공백

                    // 레코드 세팅 작업(자리수)
                    sData = V101.DV106_DT01;
                    sData += V101.DV106_DT02;
                    sData += V101.DV106_DT03;

                    sStrTemp = V101.DV106_DT04.Trim(); // 04: 60자리 (문자) : 제출사유
                    sStrTemp += new String(Convert.ToChar(" "), (60 - Encoding.Default.GetByteCount(V101.DV106_DT04.Trim())));
                    sData += sStrTemp;

                    sStrTemp = V101.DV106_DT05.Trim(); // 05: 6자리 (문자) : 일련번호
                    sStrTemp += new String(Convert.ToChar(" "), (6 - Encoding.Default.GetByteCount(V101.DV106_DT05.Trim())));
                    sData += sStrTemp;

                    sStrTemp = V101.DV106_DT06.Trim(); // 06: 40자리 (문자) : 서류명
                    sStrTemp += new String(Convert.ToChar(" "), (40 - Encoding.Default.GetByteCount(V101.DV106_DT06.Trim())));
                    sData += sStrTemp;

                    sStrTemp = V101.DV106_DT07.Trim(); // 07: 20자리 (문자) : 발급자
                    sStrTemp += new String(Convert.ToChar(" "), (20 - Encoding.Default.GetByteCount(V101.DV106_DT07.Trim())));
                    sData += sStrTemp;

                    sData += V101.DV106_DT08;
                    sData += V101.DV106_DT09;
                    sData += V101.DV106_DT10;

                    sData += V101.DV106_DT11;
                    sData += V101.DV106_DT12;
                    sData += V101.DV106_DT13;
                    sData += V101.DV106_DT14;
                    sData += V101.DV106_DT15;
                    sData += V101.DV106_DT16; // 16 : 28자리 공백

                    sw.WriteLine(sData);

                }; //for..end
            }

        }
        #endregion

        #region Description : 사업장별 부가가체세 과세표준 및 납부세액(환급세액)신고명세서 레코드 생성 (V115)  -- UP_TAX_Create_V115()
        private void UP_TAX_Create_V115(StreamWriter sw)
        {
            string sStrTemp = string.Empty;
            string sData = string.Empty;
            string sFill = string.Empty;
            string sAOGENCHK = string.Empty;

            struct_DV115_H V115_H = new struct_DV115_H();
            struct_DV115_D V115_D = new struct_DV115_D();

            string s시작년월일 = string.Empty;
            string s종료년월일 = string.Empty;
            string sYEAR = this.DTP01_ELXYYMM.GetValue().ToString().Substring(0, 4);

            // 옵션관리(총괄납부구분) 확인

            this.DbConnector.CommandClear(); // AVOPTIONMF 
            this.DbConnector.Attach("TY_P_AC_42H9G397",
                                    this.DTP01_ELXYYMM.GetValue().ToString(),    // 년월
                                    this.CBO01_VNGUBUN.GetValue().ToString(),   // 사업장(1본점, 2지점)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)   // 확정구분(1.예정, 2.확정)
                                    );
            DataTable dt_chk = this.DbConnector.ExecuteDataTable();

            // 일관과세자구분(2. 총괄납부자 3. 총괄사업자 종사업자 ,  5 : 사업자단위과세적용사업자 )
            if (dt_chk.Rows.Count > 0)
            {
                sAOGENCHK = dt_chk.Rows[0]["AOGENCHK"].ToString(); // 
            }
            // -------------------------------------------------------------------------------------- //
            // 총괄납부자가 아닐경우 생성 처리 안함                                                     //
            // -------------------------------------------------------------------------------------- //
            if (sAOGENCHK == "2")
            {
                // 사업장별부가가치세과세표준 및 납부세액(환급세액)신고명세서 (집계)
                this.DbConnector.CommandClear(); // AVSURTAXF 
                this.DbConnector.Attach("TY_P_AC_41T14178",
                                        this.DTP01_ELXYYMM.GetValue().ToString(),    // 년월
                                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)   // 확정구분(1.예정, 2.확정)
                                        );
                DataTable dt_115_h = this.DbConnector.ExecuteDataTable();

                if (dt_115_h.Rows.Count > 0)
                {
                    V115_H.DV115_H_DT01 = dt_115_h.Rows[0]["DTGB"].ToString(); // 1자리 (문자) : 자료구분  ==> (17)

                    //V115_H.DV115_H_DT02 = dt_115_h.Rows[0]["DOCGB"].ToString(); // 4자리 (문자) : 서식코드 ==> (V115) 

                    V115_H.DV115_H_DT02 = dt_115_h.Rows[0]["DOCGB"].ToString(); // 7자리 (문자) : 서식코드 ==> (I104500) 

                    //V115_H.DV115_H_DT03 = string.Format("{0:D15}", Convert.ToInt64(dt_115_h.Rows[0]["MAECHUL_AMT"].ToString()));  // 15자리 매출과세금액합계
                    //V115_H.DV115_H_DT04 = string.Format("{0:D15}", Convert.ToInt64(dt_115_h.Rows[0]["MAECHUL_TAX"].ToString()));  // 15자리 매출과세세액합계
                    //V115_H.DV115_H_DT05 = string.Format("{0:D15}", Convert.ToInt64(dt_115_h.Rows[0]["YOUNGSEYUL"].ToString()));  // 15자리 매출영세금액합계
                    //V115_H.DV115_H_DT06 = string.Format("{0:D15}", 0);  // 15자리 매출영세세액합계
                    //V115_H.DV115_H_DT07 = string.Format("{0:D15}", Convert.ToInt64(dt_115_h.Rows[0]["MAEIP_AMT"].ToString()));  // 15자리 매입과세금액합계
                    //V115_H.DV115_H_DT08 = string.Format("{0:D15}", Convert.ToInt64(dt_115_h.Rows[0]["MAEIP_TAX"].ToString()));  // 15자리 매입과세세액합계
                    //V115_H.DV115_H_DT09 = string.Format("{0:D15}", 0);  // 15자리 매입의제금액합계
                    //V115_H.DV115_H_DT10 = string.Format("{0:D15}", 0);  // 15자리 매입의제세액합계
                    //V115_H.DV115_H_DT11 = string.Format("{0:D15}", Convert.ToInt64(dt_115_h.Rows[0]["GASANSE"].ToString()));  // 15자리 가산세합계
                    //V115_H.DV115_H_DT12 = string.Format("{0:D15}", Convert.ToInt64(dt_115_h.Rows[0]["GONGJAESE"].ToString()));  // 15자리 공제세액합계
                    //V115_H.DV115_H_DT13 = string.Format("{0:D15}", Convert.ToInt64(dt_115_h.Rows[0]["NAPBUSE"].ToString()));  // 15자리  납부(환급)세액합계
                    //V115_H.DV115_H_DT14 = string.Format("{0:D15}", 0);  // 15자리 내부거래(판매목적)반출액합계
                    //V115_H.DV115_H_DT15 = string.Format("{0:D15}", 0);  // 15자리 내부거래(판매목적)반입액합계

                    V115_H.DV115_H_DT03 = UP_Minus_Conv_Fill(dt_115_h.Rows[0]["MAECHUL_AMT"].ToString().Trim(), 15);  // 15자리 매출과세금액합계
                    V115_H.DV115_H_DT04 = UP_Minus_Conv_Fill(dt_115_h.Rows[0]["MAECHUL_TAX"].ToString().Trim(), 15);;  // 15자리 매출과세세액합계
                    V115_H.DV115_H_DT05 = UP_Minus_Conv_Fill(dt_115_h.Rows[0]["YOUNGSEYUL"].ToString().Trim(), 15); // 15자리 매출영세금액합계
                    V115_H.DV115_H_DT06 = string.Format("{0:D15}", 0);  // 15자리 매출영세세액합계
                    V115_H.DV115_H_DT07 = UP_Minus_Conv_Fill(dt_115_h.Rows[0]["MAEIP_AMT"].ToString().Trim(), 15);  // 15자리 매입과세금액합계
                    V115_H.DV115_H_DT08 = UP_Minus_Conv_Fill(dt_115_h.Rows[0]["MAEIP_TAX"].ToString().Trim(), 15);;  // 15자리 매입과세세액합계
                    V115_H.DV115_H_DT09 = string.Format("{0:D15}", 0);  // 15자리 매입의제금액합계
                    V115_H.DV115_H_DT10 = string.Format("{0:D15}", 0);  // 15자리 매입의제세액합계
                    V115_H.DV115_H_DT11 = UP_Minus_Conv_Fill(dt_115_h.Rows[0]["GASANSE"].ToString().Trim(), 15);  // 15자리 가산세합계
                    V115_H.DV115_H_DT12 = UP_Minus_Conv_Fill(dt_115_h.Rows[0]["GONGJAESE"].ToString().Trim(), 15);  // 15자리 공제세액합계
                    V115_H.DV115_H_DT13 = UP_Minus_Conv_Fill(dt_115_h.Rows[0]["NAPBUSE"].ToString().Trim(), 15);  // 15자리  납부(환급)세액합계
                    V115_H.DV115_H_DT14 = string.Format("{0:D15}", 0);  // 15자리 내부거래(판매목적)반출액합계
                    V115_H.DV115_H_DT15 = string.Format("{0:D15}", 0);  // 15자리 내부거래(판매목적)반입액합계

                    // ---------------------------------------------------------------------------------------------------------------- //
                    //V115_H.DV115_H_DT16 = sFill.PadRight(99);  // 99자리 공백

                    V115_H.DV115_H_DT16 = sFill.PadRight(96);  // 96자리 공백


                    // 레코드 세팅 작업(자리수)
                    sData = V115_H.DV115_H_DT01;
                    sData += V115_H.DV115_H_DT02;
                    sData += V115_H.DV115_H_DT03;   // 15자리
                    sData += V115_H.DV115_H_DT04;   // 15자리
                    sData += V115_H.DV115_H_DT05;   // 15자리
                    sData += V115_H.DV115_H_DT06;   // 15자리
                    sData += V115_H.DV115_H_DT07;   // 15자리
                    sData += V115_H.DV115_H_DT08;   // 15자리
                    sData += V115_H.DV115_H_DT09;   // 15자리
                    sData += V115_H.DV115_H_DT10;   // 15자리
                    sData += V115_H.DV115_H_DT11;   // 15자리
                    sData += V115_H.DV115_H_DT12;   // 15자리
                    sData += V115_H.DV115_H_DT13;   // 15자리
                    sData += V115_H.DV115_H_DT14;   // 15자리
                    sData += V115_H.DV115_H_DT15;   // 15자리
                    sData += V115_H.DV115_H_DT16;   // 99자리 공백

                };

                sw.WriteLine(sData);


                // --------------------------------------------------------------------------------------------------------------- //
                //                 전산매체 사업장별부가가치세과세표준 및 납부세액(환급세액)신고명세서 상세생성                         // 
                // --------------------------                내              역                             ---------------------- //

                this.DbConnector.CommandClear(); // AVSURTAXF 
                this.DbConnector.Attach("TY_P_AC_41TBF177",
                                        this.DTP01_ELXYYMM.GetValue().ToString(),    // 년월
                                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)   // 확정구분(1.예정, 2.확정)
                                        );
                DataTable dt_115_d = this.DbConnector.ExecuteDataTable();

                if (dt_115_d.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_115_d.Rows.Count; i++)
                    {
                        if (dt_115_d.Rows[i]["VSVENDCD"].ToString() != "999999")
                        {
                            V115_D.DV115_D_DT01 = "18";   // 1자리 (문자) : 자료구분 
                            //V115_D.DV115_D_DT02 = "V115"; // 4자리 (문자) : 서식코드 
                            V115_D.DV115_D_DT02 = "I104500"; // 7자리 (문자) : 서식코드
                            V115_D.DV115_D_DT03 = dt_115_d.Rows[i]["SAUPNO"].ToString().PadRight(10); // 10자리 사업자등록번호
                            V115_D.DV115_D_DT04 = dt_115_d.Rows[i]["JUSO"].ToString();   // 70자리 사업장소재지

                            //V115_H.DV115_H_DT13 = UP_Minus_Conv_Fill(dt_115_h.Rows[0]["NAPBUSE"].ToString().Trim(), 15);  // 15자리  납부(환급)세액합계

                            //V115_D.DV115_D_DT05 = string.Format("{0:D15}", Convert.ToInt64(dt_115_d.Rows[i]["MAECHUL_AMT"].ToString()));  // 15자리 매출과세금액합계
                            //V115_D.DV115_D_DT06 = string.Format("{0:D13}", Convert.ToInt64(dt_115_d.Rows[i]["MAECHUL_TAX"].ToString()));  // 13자리 매출과세세액합계
                            //V115_D.DV115_D_DT07 = string.Format("{0:D15}", Convert.ToInt64(dt_115_d.Rows[i]["YOUNGSEYUL"].ToString()));  // 15자리 매출영세금액합계
                            //V115_D.DV115_D_DT08 = string.Format("{0:D13}", 0);  // 13자리 매출영세세액합계
                            //V115_D.DV115_D_DT09 = string.Format("{0:D15}", Convert.ToInt64(dt_115_d.Rows[i]["MAEIP_AMT"].ToString()));  // 15자리 매입과세금액합계
                            //V115_D.DV115_D_DT10 = string.Format("{0:D13}", Convert.ToInt64(dt_115_d.Rows[i]["MAEIP_TAX"].ToString()));  // 13자리 매입과세세액합계
                            //V115_D.DV115_D_DT11 = string.Format("{0:D15}", 0);  // 15자리 매입의제금액합계
                            //V115_D.DV115_D_DT12 = string.Format("{0:D13}", 0);  // 13자리 매입의제세액합계
                            //V115_D.DV115_D_DT13 = string.Format("{0:D13}", Convert.ToInt64(dt_115_d.Rows[i]["GASANSE"].ToString()));  // 13자리 가산세합계
                            //V115_D.DV115_D_DT14 = string.Format("{0:D15}", Convert.ToInt64(dt_115_d.Rows[i]["GONGJAESE"].ToString()));  // 15자리 공제세액합계
                            //V115_D.DV115_D_DT15 = string.Format("{0:D15}", Convert.ToInt64(dt_115_d.Rows[i]["NAPBUSE"].ToString()));  // 15자리  납부(환급)세액합계
                            //V115_D.DV115_D_DT16 = string.Format("{0:D15}", 0);  // 15자리 내부거래(판매목적)반출액합계
                            //V115_D.DV115_D_DT17 = string.Format("{0:D15}", 0);  // 15자리 내부거래(판매목적)반입액합계

                            V115_D.DV115_D_DT05 = UP_Minus_Conv_Fill(dt_115_d.Rows[i]["MAECHUL_AMT"].ToString().Trim(), 15);  // 15자리 매출과세금액합계
                            V115_D.DV115_D_DT06 = UP_Minus_Conv_Fill(dt_115_d.Rows[i]["MAECHUL_TAX"].ToString().Trim(), 13);  // 13자리 매출과세세액합계
                            V115_D.DV115_D_DT07 = UP_Minus_Conv_Fill(dt_115_d.Rows[i]["YOUNGSEYUL"].ToString().Trim(), 15); // 15자리 매출영세금액합계
                            V115_D.DV115_D_DT08 = string.Format("{0:D13}", 0);  // 13자리 매출영세세액합계
                            V115_D.DV115_D_DT09 = UP_Minus_Conv_Fill(dt_115_d.Rows[i]["MAEIP_AMT"].ToString().Trim(), 15);  // 15자리 매입과세금액합계
                            V115_D.DV115_D_DT10 = UP_Minus_Conv_Fill(dt_115_d.Rows[i]["MAEIP_TAX"].ToString().Trim(), 13);  // 13자리 매입과세세액합계
                            V115_D.DV115_D_DT11 = string.Format("{0:D15}", 0);  // 15자리 매입의제금액합계
                            V115_D.DV115_D_DT12 = string.Format("{0:D13}", 0);  // 13자리 매입의제세액합계
                            V115_D.DV115_D_DT13 = UP_Minus_Conv_Fill(dt_115_d.Rows[i]["GASANSE"].ToString().Trim(), 13);  // 13자리 가산세합계
                            V115_D.DV115_D_DT14 = UP_Minus_Conv_Fill(dt_115_d.Rows[i]["GONGJAESE"].ToString().Trim(), 15);;  // 15자리 공제세액합계
                            V115_D.DV115_D_DT15 = UP_Minus_Conv_Fill(dt_115_d.Rows[i]["NAPBUSE"].ToString().Trim(), 15);  // 15자리  납부(환급)세액합계
                            V115_D.DV115_D_DT16 = string.Format("{0:D15}", 0);  // 15자리 내부거래(판매목적)반출액합계
                            V115_D.DV115_D_DT17 = string.Format("{0:D15}", 0);  // 15자리 내부거래(판매목적)반입액합계
                            // ---------------------------------------------------------------------------------------------------------------- //
                            //V115_D.DV115_D_DT18 = sFill.PadRight(29);  // 29자리 공백
                            V115_D.DV115_D_DT18 = sFill.PadRight(26);  // 29자리 공백


                            // 레코드 세팅 작업(자리수)
                            sData = V115_D.DV115_D_DT01;
                            sData += V115_D.DV115_D_DT02;
                            sData += V115_D.DV115_D_DT03;  // 10자리 사업자등록번호

                            sStrTemp = V115_D.DV115_D_DT04.Trim(); // 70자리 사업장소재지
                            sStrTemp += new String(Convert.ToChar(" "), (70 - Encoding.Default.GetByteCount(V115_D.DV115_D_DT04.Trim())));
                            sData += sStrTemp;

                            sData += V115_D.DV115_D_DT05;   // 15자리
                            sData += V115_D.DV115_D_DT06;   // 13자리
                            sData += V115_D.DV115_D_DT07;   // 15자리
                            sData += V115_D.DV115_D_DT08;   // 13자리
                            sData += V115_D.DV115_D_DT09;   // 15자리
                            sData += V115_D.DV115_D_DT10;   // 13자리
                            sData += V115_D.DV115_D_DT11;   // 15자리
                            sData += V115_D.DV115_D_DT12;   // 13자리
                            sData += V115_D.DV115_D_DT13;   // 13자리
                            sData += V115_D.DV115_D_DT14;   // 15자리
                            sData += V115_D.DV115_D_DT15;   // 15자리
                            sData += V115_D.DV115_D_DT16;   // 15자리
                            sData += V115_D.DV115_D_DT17;   // 15자리
                            sData += V115_D.DV115_D_DT18;   // 29자리 공백

                            sw.WriteLine(sData);
                        }

                    } // End ... For
                };
            }

        }
        #endregion

        #region Description : 신용카드매출전표등 수치명세서(갑,을) -(V164) -- UP_TAX_Create_V164()
        private void UP_TAX_Create_V164(StreamWriter sw)
        {

            string sFill = string.Empty;

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

            // 제출자 인적사항 조회 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_42B67344", // AVSUBMITMF 
                this.DTP01_ELXYYMM.GetValue().ToString(),
                getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1), //신고구분(1기, 2기)
                getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),   // 확정구분(1.예정, 2.확정)
                this.CBO01_VNGUBUN.GetValue().ToString()   // 사업장(1본점, 2지점)
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

            struct_HR HEADER = new struct_HR();
            struct_DR DataRec = new struct_DR();
            struct_TR TailRec = new struct_TR();

            DataSet ds_chk = new DataSet();

            this.DbConnector.CommandClear(); // AVCREDITMF 
            this.DbConnector.Attach("TY_P_AC_41N5F148",
                                    this.DTP01_ELXYYMM.GetValue().ToString(),    // 년도
                                    this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1), // 신고구분(1기, 2기)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),  // 확정구분(1.예정, 2.확정)
                                    "57,58"                                         // 계산서구분(1.매입, 2.매출)
                                    );
            ds_chk = this.DbConnector.ExecuteDataSet();

            if (ds_chk.Tables[0].Rows.Count > 0)
            {

                string s시작년월일 = string.Empty;
                string s종료년월일 = string.Empty;
                string sYEAR = this.DTP01_ELXYYMM.GetValue().ToString().Substring(0, 4);
                string s순번 = string.Empty;

                if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1) == "1") // 신고구분(1기, 2기)
                {
                    if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2) == "1") // 확정구분(1.예정, 2.확정)
                    {
                        s시작년월일 = sYEAR + "0101";
                        s종료년월일 = sYEAR + "0331";
                        s순번 = "3";
                    }
                    else
                    {
                        s시작년월일 = sYEAR + "0401";
                        s종료년월일 = sYEAR + "0630";
                        s순번 = "6";
                    };
                }
                else
                {
                    if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2) == "1")
                    {
                        s시작년월일 = sYEAR + "0701";
                        s종료년월일 = sYEAR + "0930";
                        s순번 = "3";
                    }
                    else
                    {
                        s시작년월일 = sYEAR + "1001";
                        s종료년월일 = sYEAR + "1231";
                        s순번 = "6";
                    };
                }

                string sStrTemp = string.Empty;
                string sData = string.Empty;
                Int32 iCnt = 0;

                // ------------------------------------------------------------------------------------------- //
                // 제출자 인적사항(Header Record)
                // ------------------------------------------------------------------------------------------- //

                if (this.CBO01_VNGUBUN.GetValue().ToString() == "1") //본점
                {
                    HEADER.HR_DT01 = "HL";                  // 2자리 레코드구분
                    HEADER.HR_DT02 = sYEAR;                 // 4자리  귀속년도
                    HEADER.HR_DT03 = getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1); // 1자리  반기구분
                    HEADER.HR_DT04 = s순번;                 // 1자리 반기내월순번
                    HEADER.HR_DT05 = sSAUPNO;              // 10자리 수취자(제출자)사업자등록번호
                    HEADER.HR_DT06 = sSANGHO;              // 60자리 상호(법인명) (재 정리)
                    HEADER.HR_DT07 = sNAMENM;              // 30자리 성명(대표자) (재 정리)
                    HEADER.HR_DT08 = sCORPNO;              // 13자리 주민(법인)등록번호
                    HEADER.HR_DT09 = s종료년월일;           // 08자리 제출일자
                    HEADER.HR_DT10 = sFill.PadRight(11);   // 11자리 공란

                }
                else //지점
                {
                    HEADER.HR_DT01 = "HL";                  // 2자리 레코드구분
                    HEADER.HR_DT02 = sYEAR;                 // 4자리  귀속년도
                    HEADER.HR_DT03 = getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1); // 1자리  반기구분
                    HEADER.HR_DT04 = s순번;                 // 1자리 반기내월순번
                    HEADER.HR_DT05 = sSAUPNO;              // 10자리 수취자(제출자)사업자등록번호
                    HEADER.HR_DT06 = sSANGHO;              // 60자리 상호(법인명) (재 정리)
                    HEADER.HR_DT07 = sNAMENM;              // 30자리 성명(대표자) (재 정리)
                    HEADER.HR_DT08 = sCORPNO;              // 13자리 주민(법인)등록번호
                    HEADER.HR_DT09 = s종료년월일;           // 08자리 제출일자
                    HEADER.HR_DT10 = sFill.PadRight(11);   // 11자리 공란
                }

                sData = HEADER.HR_DT01;
                sData += HEADER.HR_DT02;
                sData += HEADER.HR_DT03;
                sData += HEADER.HR_DT04;

                sStrTemp = HEADER.HR_DT05.Trim(); // 10자리 사업자등록번호
                sStrTemp += new String(Convert.ToChar(" "), (10 - Encoding.Default.GetByteCount(HEADER.HR_DT05.Trim())));
                sData += sStrTemp;

                sStrTemp = HEADER.HR_DT06.Trim(); // 60자리 상호(법인명)
                sStrTemp += new String(Convert.ToChar(" "), (60 - Encoding.Default.GetByteCount(HEADER.HR_DT06.Trim())));
                sData += sStrTemp;

                sStrTemp = HEADER.HR_DT07.Trim(); // 30자리 성명(대표자)
                sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(HEADER.HR_DT07.Trim())));
                sData += sStrTemp;

                sData += HEADER.HR_DT08;
                sData += HEADER.HR_DT09;
                sData += HEADER.HR_DT10;

                sw.WriteLine(sData);

                // ------------------------------------------------------ //
                //                      신용카드매출전표                   //
                // ------------------------------------------------------ //
                // 신용•직불카드 및 기명식선불카드 매출전표 수취명세(Data Record) //
                iCnt = 0;

                DataSet ds = new DataSet();

                this.DbConnector.CommandClear(); // AVCREDITMF 
                this.DbConnector.Attach("TY_P_AC_41N5F148",
                                        this.DTP01_ELXYYMM.GetValue().ToString(),    // 년도
                                        this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1), // 신고구분(1기, 2기)
                                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),  // 확정구분(1.예정, 2.확정)
                                        "57,58"                                         // 계산서구분(1.매입, 2.매출)
                                        );
                ds = this.DbConnector.ExecuteDataSet();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        iCnt = iCnt + 1;

                        DataRec.DR_DT01 = "DL";                  // 2자리 레코드구분
                        DataRec.DR_DT02 = sYEAR;                 // 4자리  귀속년도
                        DataRec.DR_DT03 = getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1); // 1자리  반기구분
                        DataRec.DR_DT04 = s순번;                 // 1자리 반기내월순번

                        if (this.CBO01_VNGUBUN.GetValue().ToString() == "1") //본점
                        {
                            DataRec.DR_DT05 = sSAUPNO;        // 10자리 수취자(제출자)사업자등록번호 울산
                        }
                        else
                        {
                            DataRec.DR_DT05 = sSAUPNO;        // 10자리 수취자(제출자)사업자등록번호 서울
                        };

                        DataRec.DR_DT06 = ds.Tables[0].Rows[i]["VSDT06"].ToString();    // 1자리 카드구분
                        DataRec.DR_DT07 = ds.Tables[0].Rows[i]["VSDT07"].ToString().PadRight(20);    // 20자리 카드번호
                        DataRec.DR_DT08 = ds.Tables[0].Rows[i]["VSDT08"].ToString().PadRight(10);    // 10자리 공급자(가맹점)사업자 등록번호
                        DataRec.DR_DT09 = string.Format("{0:D9}", Convert.ToInt64(ds.Tables[0].Rows[i]["VSDT09"].ToString()));    // 9자리  거래건수
                        if (Convert.ToInt64(ds.Tables[0].Rows[i]["VSDT11"].ToString().Trim()) < 0)
                        {
                            DataRec.DR_DT10 = "-";    // 1자리  음수표시
                        }
                        else
                        {
                            DataRec.DR_DT10 = " ";    // 1자리  음수표시
                        };

                        DataRec.DR_DT11 = ds.Tables[0].Rows[i]["VSDT11"].ToString().Replace("-", "").Trim();    // 13자리 공급가액

                        if (Convert.ToInt64(ds.Tables[0].Rows[i]["VSDT13"].ToString().Trim()) < 0)
                        {
                            DataRec.DR_DT12 = "-";    // 1자리  음수표시
                        }
                        else
                        {
                            DataRec.DR_DT12 = " ";    // 1자리  음수표시
                        };
                        DataRec.DR_DT13 = ds.Tables[0].Rows[i]["VSDT13"].ToString().Replace("-","").Trim();    // 13자리 세액
                        // ---------------------------------------------------------------------------------------------------------------- //
                        DataRec.DR_DT14 = sFill.PadRight(54);   // 54자리 공란

                        sData = DataRec.DR_DT01;
                        sData += DataRec.DR_DT02;
                        sData += DataRec.DR_DT03;
                        sData += DataRec.DR_DT04;
                        sData += DataRec.DR_DT05;
                        sData += DataRec.DR_DT06;
                        sData += DataRec.DR_DT07; // 20자리 카드번호
                        sData += DataRec.DR_DT08; // 10자리 공급자(가맹점)사업자 등록번호
                        sData += DataRec.DR_DT09;
                        sData += DataRec.DR_DT10;
                        sData += string.Format("{0:D13}", Convert.ToInt64(DataRec.DR_DT11));     //  13자리 공급가액
                        sData += DataRec.DR_DT12;
                        sData += string.Format("{0:D13}", Convert.ToInt64(DataRec.DR_DT13));     //  13자리 세액
                        sData += DataRec.DR_DT14;

                        sw.WriteLine(sData);

                        // --------------------------------------------------------------------------------------- //
                        // ----------------------  신용카드등 매입내용 합계(Tail Record)  -------------------------- //
                        // --------------------------------------------------------------------------------------- //
                        TailRec.TR_DT01 = "TL";                  // 2자리 레코드구분
                        TailRec.TR_DT02 = sYEAR;                 // 4자리  귀속년도
                        TailRec.TR_DT03 = getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1); // 1자리  반기구분
                        TailRec.TR_DT04 = s순번;                 // 1자리 반기내월순번

                        if (this.CBO01_VNGUBUN.GetValue().ToString() == "1") //본점
                        {
                            TailRec.TR_DT05 = sSAUPNO;        // 10자리 수취자(제출자)사업자등록번호 울산
                        }
                        else
                        {
                            TailRec.TR_DT05 = sSAUPNO;        // 10자리 수취자(제출자)사업자등록번호 서울
                        };

                        // 합계자료 구하기
                        TailRec.TR_DT06 = Convert.ToString(iCnt);
                        TailRec.TR_DT07 = Convert.ToString(Convert.ToDouble(TailRec.TR_DT07) +
                                          Convert.ToDouble(ds.Tables[0].Rows[i]["VSDT09"].ToString().Trim()));

                        TailRec.TR_DT09 = Convert.ToString(Convert.ToDouble(TailRec.TR_DT09) +
                                          Convert.ToDouble(ds.Tables[0].Rows[i]["VSDT11"].ToString().Trim()));
                        TailRec.TR_DT11 = Convert.ToString(Convert.ToDouble(TailRec.TR_DT11) +
                                          Convert.ToDouble(ds.Tables[0].Rows[i]["VSDT13"].ToString().Trim()));

                    }//for..end


                    // -------------------------------------------------------------------------------------------- // 
                    //  신용카드등 매입내용 합계(Tail Record) (TOTAL RECORD)
                    TailRec.TR_DT12 = sFill.PadRight(74);   // 74자리 공란

                    sData = TailRec.TR_DT01;          // 2자리 레코드구분
                    sData += TailRec.TR_DT02;        //  4자리 귀속년도
                    sData += TailRec.TR_DT03;        //  1자리 반기구분
                    sData += TailRec.TR_DT04;        //  1자리 반기내월순번
                    sData += TailRec.TR_DT05;         // 10자리 수취자(제출자)사업자등록번호
                    sData += string.Format("{0:D7}", Convert.ToInt64(TailRec.TR_DT06));        // DATA건수     :   7자리
                    sData += string.Format("{0:D9}", Convert.ToInt64(TailRec.TR_DT07));        // 총거래건수   :   9자리

                    if (Convert.ToInt64(TailRec.TR_DT09.ToString().Trim()) < 0)  // 1자리  음수표시
                    { TailRec.TR_DT08 = "-"; }
                    else { TailRec.TR_DT08 = " "; };
                    sData += TailRec.TR_DT08;

                    sData += string.Format("{0:D15}", Convert.ToInt64(TailRec.TR_DT09));        // 총공급가액   :   15자리
                    if (Convert.ToInt64(TailRec.TR_DT11.ToString().Trim()) < 0) // 1자리  음수표시
                    { TailRec.TR_DT10 = "-"; }
                    else
                    { TailRec.TR_DT10 = " "; };
                    sData += TailRec.TR_DT10;

                    sData += string.Format("{0:D15}", Convert.ToInt64(TailRec.TR_DT11));        // 총세액   :   15자리
                    sData += TailRec.TR_DT12;        //74자리

                    sw.WriteLine(sData);
                }

            } // 존재시 사용


        }
        #endregion

        #region Description : 공제받지못할매입세액명세서 레코드 생성 (V153)  -- UP_TAX_Create_V153()
        private void UP_TAX_Create_V153(StreamWriter sw)
        {
            string sStrTemp = string.Empty;
            string sData = string.Empty;
            string sFill = string.Empty;

            struct_DV153 V153 = new struct_DV153();

            // 공제받지못할매입세액명세서
            this.DbConnector.CommandClear(); // AVDEDUCTMF 
            this.DbConnector.Attach("TY_P_AC_41R9Y159",
                                    this.DTP01_ELXYYMM.GetValue().ToString(),    // 년월
                                    this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)   // 확정구분(1.예정, 2.확정)
                                    );
            DataTable dt_153 = this.DbConnector.ExecuteDataTable();

            if (dt_153.Rows.Count > 0)
            {
                if (Convert.ToInt64(dt_153.Rows[0]["CNT01"].ToString().Trim()) > 0) // 건수_합계_고정자산 없어면 처리 안함
                {
                    V153.DV153_DT01 = dt_153.Rows[0]["DTGB"].ToString(); // 2자리 (문자) : 자료구분 --> (17)
                    //V153.DV153_DT02 = dt_153.Rows[0]["DOCGB"].ToString(); // 4자리 (문자) : 서식코드 --> (153)
                    V153.DV153_DT02 = dt_153.Rows[0]["DOCGB"].ToString(); // 7자리 (문자) : 서식코드 --> (I103300)

                    // 매수합계_세금계산서 //
                    //V153.DV153_DT03 = dt_153.Rows[0]["CNT01"].ToString().Trim();    // 11자리
                    V153.DV153_DT03 = string.Format("{0:D11}", Convert.ToInt64(dt_153.Rows[0]["CNT01"].ToString().Trim()));        // 건수_합계_고정자산 :   11자리
                    V153.DV153_DT04 = UP_Minus_Conv_Fill(dt_153.Rows[0]["AMT01"].ToString().Trim(), 15);    // 15자리
                    V153.DV153_DT05 = UP_Minus_Conv_Fill(dt_153.Rows[0]["VAT01"].ToString().Trim(), 15);    // 15자리
                    // 공통매입공급가액합계_안분계산 //
                    V153.DV153_DT06 = UP_Minus_Conv_Fill(dt_153.Rows[0]["AMT02"].ToString().Trim(), 15);    // 15자리
                    V153.DV153_DT07 = UP_Minus_Conv_Fill(dt_153.Rows[0]["VAT02"].ToString().Trim(), 15);    // 15자리
                    // 불공제매입세액합계_안분계산 //
                    V153.DV153_DT08 = UP_Minus_Conv_Fill(dt_153.Rows[0]["AMT03"].ToString().Trim(), 15);    // 15자리
                    V153.DV153_DT09 = UP_Minus_Conv_Fill(dt_153.Rows[0]["VAT03"].ToString().Trim(), 15);    // 15자리
                    // 기불공제매입세액합계_정산내역 //
                    V153.DV153_DT10 = UP_Minus_Conv_Fill(dt_153.Rows[0]["AMT04"].ToString().Trim(), 15);    // 15자리
                    // 가산•공제매입세액합계_정산내역 //
                    V153.DV153_DT11 = UP_Minus_Conv_Fill(dt_153.Rows[0]["AMT05"].ToString().Trim(), 15);    // 13자리
                    V153.DV153_DT12 = UP_Minus_Conv_Fill(dt_153.Rows[0]["AMT05"].ToString().Trim(), 15);    // 13자리
                    // ---------------------------------------------------------------------------------------------------------------- //
                    //V153.DV153_DT13 = sFill.PadRight(48);   // 48자리 공백
                    V153.DV153_DT13 = sFill.PadRight(45);   // 48자리 공백

                    // 레코드 세팅 작업(자리수)
                    sData = V153.DV153_DT01;
                    sData += V153.DV153_DT02;

                    sData += string.Format("{0:D11}", Convert.ToInt64(V153.DV153_DT03));        // 건수_합계_고정자산 :   11자리
                    sData += V153.DV153_DT04;
                    sData += V153.DV153_DT05;

                    sData += V153.DV153_DT06;
                    sData += V153.DV153_DT07;

                    sData += V153.DV153_DT08;
                    sData += V153.DV153_DT09;

                    sData += V153.DV153_DT10;

                    sData += V153.DV153_DT11;
                    sData += V153.DV153_DT12;
                    sData += V153.DV153_DT13;  // 48자리 공백

                    sw.WriteLine(sData);
                }
            };

            // ----------------------------------------------------------------------- //
            //                        공제받지못할매입세액명세서                         // 
            // --------------------------     내         역     ---------------------- //
            struct_DV153_D V153_D = new struct_DV153_D();

            this.DbConnector.CommandClear(); // AVDEDUCTMF 
            this.DbConnector.Attach("TY_P_AC_41RA6160",
                                    this.DTP01_ELXYYMM.GetValue().ToString(),    // 년월
                                    this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)   // 확정구분(1.예정, 2.확정)
                                    );
            DataTable dt_153_d = this.DbConnector.ExecuteDataTable();

            if (dt_153_d.Rows.Count > 0)
            {
                for (int i = 0; i < dt_153_d.Rows.Count; i++)
                {
                    V153_D.DV153_D_DT01 = dt_153_d.Rows[i]["DTGB"].ToString();  // 2자리 (문자) : 자료구분 --> (18)
                    //V153_D.DV153_D_DT02 = dt_153_d.Rows[i]["DOCGB"].ToString(); // 4자리 (문자) : 서식코드 --> (V153)
                    V153_D.DV153_D_DT02 = dt_153_d.Rows[i]["DOCGB"].ToString(); // 7자리 (문자) : 서식코드 --> (I103300)
                    V153_D.DV153_D_DT03 = dt_153_d.Rows[i]["SAYU"].ToString();  // 2자리 (문자) : 사유구분

                    V153_D.DV153_D_DT04 = string.Format("{0:D11}", Convert.ToInt64(dt_153_d.Rows[i]["CNT01"].ToString().Trim())); // 세금계산서매수 :   11자리
                    V153_D.DV153_D_DT05 = UP_Minus_Conv_Fill(dt_153_d.Rows[i]["AMT01"].ToString().Trim(), 13);    // 13자리
                    V153_D.DV153_D_DT06 = UP_Minus_Conv_Fill(dt_153_d.Rows[i]["VAT01"].ToString().Trim(), 13);    // 13자리
                    // ---------------------------------------------------------------------------------------------------------------- //
                    //V153_D.DV153_D_DT07 = sFill.PadRight(55);   // 55자리 공백
                    V153_D.DV153_D_DT07 = sFill.PadRight(52);   // 52자리 공백

                    // 레코드 세팅 작업(자리수)
                    sData = V153_D.DV153_D_DT01;
                    sData += V153_D.DV153_D_DT02;
                    sData += V153_D.DV153_D_DT03;
                    sData += V153_D.DV153_D_DT04;        //  11자리
                    sData += V153_D.DV153_D_DT05;        //  13자리
                    sData += V153_D.DV153_D_DT06;        //  13자리
                    sData += V153_D.DV153_D_DT07;        //  55자리 공백

                    sw.WriteLine(sData);
                }
            };

        }
        #endregion

        #region Description : 매출처별 세금계산서 합계표(갑,을)-(V104)  : 매입처별 세금계산서 합계표(갑,을) - (V105) - [Multi-Key] -- UP_TAX_Create_V104()
        private void UP_TAX_Create_V104(StreamWriter sw)
        {
            struct_HEADER HEADER = new struct_HEADER();
            struct_MaeChul Maechul = new struct_MaeChul();
            struct_MaeChulTotal MaechulTotal = new struct_MaeChulTotal();
            struct_MaeIpTotal MaeIpTotal = new struct_MaeIpTotal();

            string sFill = string.Empty;

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

            // 제출자 인적사항 조회 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_42B67344", // AVSUBMITMF 
                this.DTP01_ELXYYMM.GetValue().ToString(),
                getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1), //신고구분(1기, 2기)
                getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),   // 확정구분(1.예정, 2.확정)
                this.CBO01_VNGUBUN.GetValue().ToString()   // 사업장(1본점, 2지점)
                );

            DataTable dt_tae = this.DbConnector.ExecuteDataTable();

            if (dt_tae.Rows.Count > 0)
            {
                sSAUPNO = dt_tae.Rows[0]["ASMSAUPNO"].ToString(); // 사업자등록번호(10자리)
                sSANGHO = dt_tae.Rows[0]["ASMSANGHO"].ToString(); // 상호명
                sNAMENM = dt_tae.Rows[0]["ASMNAMENM"].ToString(); // 대표자이름
                sCORPNO = dt_tae.Rows[0]["ASMCORPNO"].ToString(); // 법인번호 (13자리)
                sUPTAE = dt_tae.Rows[0]["ASMUPTAE"].ToString(); // 업태
                sEVENT = dt_tae.Rows[0]["ASMEVENT"].ToString(); // 종목
                sTELNUM = Up_Telnum_Convert(dt_tae.Rows[0]["ASMTELNUM"].ToString().Trim()); // 전화번호
                sVNADDRS = dt_tae.Rows[0]["ASMVNADDRS"].ToString(); // 사업장주소
                sTAXAREA = dt_tae.Rows[0]["ASMTAXAREA"].ToString(); // 관할세무소
                sBUSTYPE = dt_tae.Rows[0]["ASMBUSTYPE"].ToString(); // 업종코드 (7자리)
            }


            string s시작년월일 = string.Empty;
            string s종료년월일 = string.Empty;
            string sYEAR = this.DTP01_ELXYYMM.GetValue().ToString().Substring(0, 4);

            if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1) == "1") // 신고구분(1기, 2기)
            {
                if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2) == "1") // 확정구분(1.예정, 2.확정)
                {
                    s시작년월일 = sYEAR + "0101";
                    s종료년월일 = sYEAR + "0331";
                }
                else
                {
                    s시작년월일 = sYEAR + "0401";
                    s종료년월일 = sYEAR + "0630";
                };
            }
            else
            {
                if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2) == "1")
                {
                    s시작년월일 = sYEAR + "0701";
                    s종료년월일 = sYEAR + "0930";
                }
                else
                {
                    s시작년월일 = sYEAR + "1001";
                    s종료년월일 = sYEAR + "1231";
                };
            }

            string sStrTemp = string.Empty;
            string sData = string.Empty;
            Int32 iCnt = 0;

            // -----------------------  부가세 헤더  ------------------------
            #region Description : 세금계산서 부가세 HAED  작성

            if (this.CBO01_VNGUBUN.GetValue().ToString() == "1") //본점
            {
                HEADER.HEADER_GUBN = "7";              // 1자리
                HEADER.HEADER_SAUPNO = sSAUPNO;        // 10자리 사업자등록번호
                HEADER.HEADER_SANGHO = sSANGHO;        // 30자리 상호명 (재 정리)
                HEADER.HEADER_IRUM = sNAMENM;          // 15자리 대표자이름 (재 정리)
                HEADER.HEADER_JUSO = sVNADDRS;         // 45자리 사업장주소 (재 정리)
                HEADER.HEADER_UPTE = sFill.PadRight(17);     // 17자리 (재 정리) -- 2014년 부터 공백처리 지침
                HEADER.HEADER_UPJONG = sFill.PadRight(25);   // 25자리 (재 정리) -- 2014년 부터 공백처리 지침
                HEADER.HEADER_STDATE = s시작년월일.Substring(2, 6);  // 6자리(거래기간)
                HEADER.HEADER_EDDATE = s종료년월일.Substring(2, 6);  // 6자리(거래기간)
                HEADER.HEADER_DATE = s종료년월일.Substring(2, 6);  // 6자리
                HEADER.HEADER_FILLER = sFill.PadRight(09);        // 9자리
            }
            else //지점
            {
                HEADER.HEADER_GUBN = "7";                // 1자리
                HEADER.HEADER_SAUPNO = sSAUPNO;        // 10자리 사업자등록번호
                HEADER.HEADER_SANGHO = sSANGHO;        // 30자리 상호명 (재 정리)
                HEADER.HEADER_IRUM = sNAMENM;          // 15자리 대표자이름 (재 정리)
                HEADER.HEADER_JUSO = "서울시 영등포구 여의공원로 111";         // 45자리 사업장주소 (재 정리)
                HEADER.HEADER_UPTE = sFill.PadRight(17);     // 17자리 (재 정리) -- 2014년 부터 공백처리 지침
                HEADER.HEADER_UPJONG = sFill.PadRight(25);   // 25자리 (재 정리) -- 2014년 부터 공백처리 지침
                HEADER.HEADER_STDATE = s시작년월일.Substring(2, 6);  // 6자리(거래기간)
                HEADER.HEADER_EDDATE = s종료년월일.Substring(2, 6);   // 6자리(거래기간)
                HEADER.HEADER_DATE = s종료년월일.Substring(2, 6);        // 6자리
                HEADER.HEADER_FILLER = sFill.PadRight(09);       // 9자리					
            }

            sData = HEADER.HEADER_GUBN;
            sData += HEADER.HEADER_SAUPNO;

            sStrTemp = HEADER.HEADER_SANGHO.Trim();  // 30자리 상호명 (재 정리)
            sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(HEADER.HEADER_SANGHO.Trim())));
            sData += sStrTemp;

            sStrTemp = HEADER.HEADER_IRUM.Trim();    // 15자리 대표자이름 (재 정리)
            sStrTemp += new String(Convert.ToChar(" "), (15 - Encoding.Default.GetByteCount(HEADER.HEADER_IRUM.Trim())));
            sData += sStrTemp;

            sStrTemp = HEADER.HEADER_JUSO.Trim();    // 45자리 사업장주소 (재 정리)
            sStrTemp += new String(Convert.ToChar(" "), (45 - Encoding.Default.GetByteCount(HEADER.HEADER_JUSO.Trim())));
            sData += sStrTemp;

            sStrTemp = HEADER.HEADER_UPTE.Trim();   // 17자리 (재 정리) -- 2014년 부터 공백처리 지침
            sStrTemp += new String(Convert.ToChar(" "), (17 - Encoding.Default.GetByteCount(HEADER.HEADER_UPTE.Trim())));
            sData += sStrTemp;

            sStrTemp = HEADER.HEADER_UPJONG.Trim(); // 25자리 (재 정리) -- 2014년 부터 공백처리 지침
            sStrTemp += new String(Convert.ToChar(" "), (25 - Encoding.Default.GetByteCount(HEADER.HEADER_UPJONG.Trim())));
            sData += sStrTemp;

            sData += HEADER.HEADER_STDATE;
            sData += HEADER.HEADER_EDDATE;
            sData += HEADER.HEADER_DATE;
            sData += HEADER.HEADER_FILLER;

            sw.WriteLine(sData); // 표지(Head Record)  

            #endregion

            // ---------------   매  출  세  금  계  산  서   ---------------
            #region Description : 매출생성

            iCnt = 0;
            DataSet ds = new DataSet();

            MaechulTotal.T1GUBN = "";          //1자리
            MaechulTotal.T1SAUPNO = "";        //10자리
            MaechulTotal.T1SEQ = "";           //7자리
            MaechulTotal.T1CNT = "";           //7자리
            MaechulTotal.T1AMT = "";           //15자리
            MaechulTotal.T1VAT = "";           //14자리
            MaechulTotal.T1SASEQ = "";         //7자리
            MaechulTotal.T1SACNT = "";         //7자리
            MaechulTotal.T1SAAMT = "";         //15자리
            MaechulTotal.T1SAVAT = "";         //14자리
            MaechulTotal.T1JUSEQ = "";         //7자리
            MaechulTotal.T1JUCNT = "";         //7자리
            MaechulTotal.T1JUAMT = "";         //15자리
            MaechulTotal.T1JUVAT = "";         //14자리
            MaechulTotal.FILLER = "";          //28자리

            // -------------------------------------------------------------------------------------------------- //
            //                               전자세금계산서 이외분 매출 합계 처리 (1,3)  (NOT IN)                   //
            // -------------------------------------------------------------------------------------------------- //
            this.DbConnector.CommandClear(); // AVSUMTABMF 
            this.DbConnector.Attach("TY_P_AC_41N3Q146",
                                    this.DTP01_ELXYYMM.GetValue().ToString(),    // 년도
                                    this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1), // 신고구분(1기, 2기)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),  // 확정구분(1.예정, 2.확정)
                                    "2"                                          // 계산서구분(1.매입, 2.매출)
                                    );
            ds = this.DbConnector.ExecuteDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    iCnt = iCnt + 1;

                    Maechul.GUBN = "1";                              // 자료구분 : 1자리
                    Maechul.SEQ = string.Format("{0:D4}", iCnt);     // 일련번호 : 4자리

                    if (this.CBO01_VNGUBUN.GetValue().ToString() == "1") //본점
                    {
                        Maechul.SAUPNO = sSAUPNO;     // 제출자 사업자등록번호 : 10자리
                        Maechul.CODE = sTAXAREA;      // 관할세무소 : 3자리
                    }
                    else
                    {
                        Maechul.SAUPNO = sSAUPNO;     // 제출자 사업자등록번호 : 10자리
                        Maechul.CODE = sTAXAREA;      // 관할세무소 : 3자리
                    }

                    if (ds.Tables[0].Rows[i]["VNSJGB"].ToString() == "1")
                    {
                        Maechul.SAUPNO1 = ds.Tables[0].Rows[i]["S1SAUPNO"].ToString().Substring(0, 10);
                    }
                    else
                    {
                        Maechul.SAUPNO1 = "8888888888";
                        Maechul.GUBN = "9";
                    }

                    sStrTemp = " ";
                    sStrTemp += ds.Tables[0].Rows[i]["VNSANGHO"].ToString();
                    sStrTemp += new String(Convert.ToChar(" "), (29 - Encoding.Default.GetByteCount(ds.Tables[0].Rows[i]["VNSANGHO"].ToString().Trim())));
                    Maechul.SANGHO = sStrTemp;

                    sStrTemp = "";
                    sStrTemp += ds.Tables[0].Rows[i]["VNUPTE"].ToString();
                    sStrTemp += new String(Convert.ToChar(" "), (17 - Encoding.Default.GetByteCount(ds.Tables[0].Rows[i]["VNUPTE"].ToString().Trim())));
                    Maechul.UPTE = sStrTemp;

                    sStrTemp = "";
                    sStrTemp += ds.Tables[0].Rows[i]["VNUPJONG"].ToString();
                    sStrTemp += new String(Convert.ToChar(" "), (24 - Encoding.Default.GetByteCount(ds.Tables[0].Rows[i]["VNUPJONG"].ToString().Trim())));
                    Maechul.UPJONG = sStrTemp;

                    Maechul.CNT = string.Format("{0:D7}", Convert.ToInt32(ds.Tables[0].Rows[i]["CNT"].ToString().Trim()));


                    Maechul.AMT = UP_Multi_Key_Fill(Get_Numeric(ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim()), 14); // 14자리 :  공급가액
                    Maechul.GONG = "00";                        //  2자리 : 삭제항목으로 0으로 입력 [2014.01월 개정됨] - 공란수
                    Maechul.VAT = UP_Multi_Key_Fill(Get_Numeric(ds.Tables[0].Rows[i]["V1VAT"].ToString().Trim()), 13); // 13자리 :  세액

                    //if (Convert.ToInt64(ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim()) < 0)
                    //{
                    //    string sTempAmt = string.Format("{0:D14}", Convert.ToInt64(ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim()) * -1);

                    //    string sALPHAValue = UP_Minus_ALPHA(sTempAmt.Substring(13, 1));

                    //    Maechul.AMT = sTempAmt.Substring(0, 13) + sALPHAValue;

                    //    Maechul.GONG = string.Format("{0:D2}", Convert.ToInt64(Convert.ToString(14 - ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim().Length + 1)));
                    //}
                    //else
                    //{
                    //    Maechul.AMT = string.Format("{0:D14}", Convert.ToInt64(ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim()));

                    //    Maechul.GONG = string.Format("{0:D2}", Convert.ToInt64(Convert.ToString(14 - ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim().Length)));
                    //}

                    //// 13자리 :  세액
                    //if (Convert.ToInt64(ds.Tables[0].Rows[i]["V1VAT"].ToString().Trim()) < 0)
                    //{
                    //    string sTempAmt = string.Format("{0:D13}", Convert.ToInt64(ds.Tables[0].Rows[i]["V1VAT"].ToString().Trim()) * -1);

                    //    string sALPHAValue = UP_Minus_ALPHA(sTempAmt.Substring(12, 1));

                    //    Maechul.VAT = sTempAmt.Substring(0, 12) + sALPHAValue;
                    //}
                    //else
                    //{
                    //    Maechul.VAT = string.Format("{0:D13}", Convert.ToInt64(ds.Tables[0].Rows[i]["V1VAT"].ToString().Trim()));
                    //}

                    Maechul.UPTE = sFill.PadRight(17);     // 17자리 : 삭제항목으로 SPACE 으로 입력 [2014.01월 개정됨] - 거래자업태
                    Maechul.UPJONG = sFill.PadRight(25); // 25자리 : 삭제항목으로 SPACE 으로 입력 [2014.01월 개정됨] - 거래자종목
                    Maechul.GONG = "00";                        //  2자리 : 삭제항목으로 0으로 입력 [2014.01월 개정됨] - 공란수
                    Maechul.DOCODE = "0";
                    Maechul.SOCODE = "0";
                    Maechul.BOOKNO = "7501";  // 신고서 에서 변경됨 9001
                    Maechul.FILLER = sFill.PadRight(28); // 28자리 : 공백

                    sData = Maechul.GUBN;
                    sData += Maechul.SAUPNO;
                    sData += Maechul.SEQ;
                    sData += Maechul.SAUPNO1;
                    sData += Maechul.SANGHO;
                    sData += Maechul.UPTE;
                    sData += Maechul.UPJONG;
                    sData += Maechul.CNT;
                    sData += Maechul.GONG;
                    sData += Maechul.AMT;
                    sData += Maechul.VAT;
                    sData += Maechul.DOCODE;
                    sData += Maechul.SOCODE;
                    sData += Maechul.BOOKNO;
                    sData += Maechul.CODE;
                    sData += Maechul.FILLER;

                    sw.WriteLine(sData);

                    MaechulTotal.T1GUBN = "3";

                    if (this.CBO01_VNGUBUN.GetValue().ToString() == "1") //본점
                    {
                        MaechulTotal.T1SAUPNO = sSAUPNO;
                    }
                    else
                    {
                        MaechulTotal.T1SAUPNO = sSAUPNO;
                    }

                    MaechulTotal.T1SEQ = Convert.ToString(iCnt);
                    MaechulTotal.T1CNT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaechulTotal.T1CNT)) +
                                         Convert.ToDouble(ds.Tables[0].Rows[i]["CNT"].ToString().Trim()));
                    MaechulTotal.T1AMT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaechulTotal.T1AMT)) +
                                         Convert.ToDouble(ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim()));
                    MaechulTotal.T1VAT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaechulTotal.T1VAT)) +
                                         Convert.ToDouble(ds.Tables[0].Rows[i]["V1VAT"].ToString().Trim()));

                    if (ds.Tables[0].Rows[i]["VNSJGB"].ToString() == "1")
                    {
                        MaechulTotal.T1SASEQ = Convert.ToString(Convert.ToInt32(Get_Numeric(MaechulTotal.T1SASEQ)) + 1);
                        MaechulTotal.T1SACNT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaechulTotal.T1SACNT)) +
                                               Convert.ToDouble(ds.Tables[0].Rows[i]["CNT"].ToString().Trim()));
                        MaechulTotal.T1SAAMT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaechulTotal.T1SAAMT)) +
                                               Convert.ToDouble(ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim()));
                        MaechulTotal.T1SAVAT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaechulTotal.T1SAVAT)) +
                                               Convert.ToDouble(ds.Tables[0].Rows[i]["V1VAT"].ToString().Trim()));
                    }
                    else
                    {
                        MaechulTotal.T1JUSEQ = Convert.ToString(Convert.ToInt32(Get_Numeric(MaechulTotal.T1JUSEQ)) + 1);
                        MaechulTotal.T1JUCNT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaechulTotal.T1JUCNT)) +
                                               Convert.ToDouble(ds.Tables[0].Rows[i]["CNT"].ToString().Trim()));
                        MaechulTotal.T1JUAMT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaechulTotal.T1JUAMT)) +
                                               Convert.ToDouble(ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim()));
                        MaechulTotal.T1JUVAT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaechulTotal.T1JUVAT)) +
                                               Convert.ToDouble(ds.Tables[0].Rows[i]["V1VAT"].ToString().Trim()));
                    }

                }//for..end

                // (전자세금계산서 이외분) 매출합계 (TOTAL RECORD)
                MaechulTotal.FILLER = sFill.PadRight(30); // 30자리 : 공백

                MaechulTotal.T1AMT = UP_Multi_Key_Fill(Get_Numeric(MaechulTotal.T1AMT.ToString().Trim()), 15); // 15자리 :  공급가액
                MaechulTotal.T1VAT = UP_Multi_Key_Fill(Get_Numeric(MaechulTotal.T1VAT.ToString().Trim()), 14); // 14자리 :  세액

                MaechulTotal.T1SAAMT = UP_Multi_Key_Fill(Get_Numeric(MaechulTotal.T1SAAMT.ToString().Trim()), 15); // 15자리 :  공급가액
                MaechulTotal.T1SAVAT = UP_Multi_Key_Fill(Get_Numeric(MaechulTotal.T1SAVAT.ToString().Trim()), 14); // 14자리 :  세액

                MaechulTotal.T1JUAMT = UP_Multi_Key_Fill(Get_Numeric(MaechulTotal.T1JUAMT.ToString().Trim()), 15); // 15자리 :  공급가액
                MaechulTotal.T1JUVAT = UP_Multi_Key_Fill(Get_Numeric(MaechulTotal.T1JUVAT.ToString().Trim()), 14); // 14자리 :  세액


                sData = MaechulTotal.T1GUBN;          // 자료구분 : 1자리
                sData += MaechulTotal.T1SAUPNO;        // 제출자 사업자등록번호 : 10자리
                // 합계분
                sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaechulTotal.T1SEQ)));        // 거래처수    :   7자리
                sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaechulTotal.T1CNT)));        // 계산서 매수 :   7자리
                sData += MaechulTotal.T1AMT;       // 공급가액    :  15자리
                sData += MaechulTotal.T1VAT;       // 세    액    :  14자리

                //sData += string.Format("{0:D15}", Convert.ToInt64(MaechulTotal.T1AMT));       // 공급가액    :  15자리
                //sData += string.Format("{0:D14}", Convert.ToInt64(MaechulTotal.T1VAT));       // 세    액    :  14자리
                // 사업자등록번호 발행분
                sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaechulTotal.T1SASEQ)));      // 거래처수    :   7자리
                sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaechulTotal.T1SACNT)));      // 계산서 매수 :   7자리
                sData += MaechulTotal.T1SAAMT;     // 공급가액    :  15자리
                sData += MaechulTotal.T1SAVAT;     // 세    액    :  14자리

                //sData += string.Format("{0:D15}", Convert.ToInt64(MaechulTotal.T1SAAMT));     // 공급가액    :  15자리
                //sData += string.Format("{0:D14}", Convert.ToInt64(MaechulTotal.T1SAVAT));     // 세    액    :  14자리
                // 주민등록번호 발행분
                sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaechulTotal.T1JUSEQ)));      // 거래처수    :   7자리
                sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaechulTotal.T1JUCNT)));      // 계산서 매수 :   7자리
                sData += MaechulTotal.T1JUAMT;     // 공급가액    :  15자리
                sData += MaechulTotal.T1JUVAT;     // 세    액    :  14자리			

                //sData += string.Format("{0:D15}", Convert.ToInt64(MaechulTotal.T1JUAMT));     // 공급가액    :  15자리
                //sData += string.Format("{0:D14}", Convert.ToInt64(MaechulTotal.T1JUVAT));     // 세    액    :  14자리			
                sData += MaechulTotal.FILLER;        //30자리

                sw.WriteLine(sData);

            }

            // -------------------------------------------------------------------------------------------------- //
            //                                 전자세금계산서분 매출 합계 처리 (5)  (IN)                            //
            // -------------------------------------------------------------------------------------------------- //

            ds.Clear();

            MaechulTotal.T1GUBN = "";          //1자리
            MaechulTotal.T1SAUPNO = "";        //10자리
            MaechulTotal.T1SEQ = "";           //7자리
            MaechulTotal.T1CNT = "";           //7자리
            MaechulTotal.T1AMT = "";           //15자리
            MaechulTotal.T1VAT = "";           //14자리
            MaechulTotal.T1SASEQ = "";         //7자리
            MaechulTotal.T1SACNT = "";         //7자리
            MaechulTotal.T1SAAMT = "";         //15자리
            MaechulTotal.T1SAVAT = "";         //14자리
            MaechulTotal.T1JUSEQ = "";         //7자리
            MaechulTotal.T1JUCNT = "";         //7자리
            MaechulTotal.T1JUAMT = "";         //15자리
            MaechulTotal.T1JUVAT = "";         //14자리
            MaechulTotal.FILLER = "";          //30자리

            iCnt = 0;

            this.DbConnector.CommandClear(); // AVSUMTABMF 
            this.DbConnector.Attach("TY_P_AC_41N3Q147",
                                    this.DTP01_ELXYYMM.GetValue().ToString(),    // 년도
                                    this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1), // 신고구분(1기, 2기)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),  // 확정구분(1.예정, 2.확정)
                                    "2"                                          // 계산서구분(1.매입, 2.매출)
                                    );
            ds = this.DbConnector.ExecuteDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    iCnt = iCnt + 1;

                    MaechulTotal.T1GUBN = "5";

                    if (this.CBO01_VNGUBUN.GetValue().ToString() == "1") //본점
                    {
                        MaechulTotal.T1SAUPNO = sSAUPNO;
                    }
                    else
                    {
                        MaechulTotal.T1SAUPNO = sSAUPNO;
                    }

                    MaechulTotal.T1SEQ = Convert.ToString(iCnt);
                    MaechulTotal.T1CNT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaechulTotal.T1CNT)) +
                                         Convert.ToDouble(ds.Tables[0].Rows[i]["CNT"].ToString().Trim()));
                    MaechulTotal.T1AMT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaechulTotal.T1AMT)) +
                                         Convert.ToDouble(ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim()));
                    MaechulTotal.T1VAT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaechulTotal.T1VAT)) +
                                         Convert.ToDouble(ds.Tables[0].Rows[i]["V1VAT"].ToString().Trim()));

                    if (ds.Tables[0].Rows[i]["VNSJGB"].ToString() == "1")  // 사업자등록분
                    {
                        MaechulTotal.T1SASEQ = Convert.ToString(Convert.ToInt32(Get_Numeric(MaechulTotal.T1SASEQ)) + 1);
                        MaechulTotal.T1SACNT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaechulTotal.T1SACNT)) +
                                               Convert.ToDouble(ds.Tables[0].Rows[i]["CNT"].ToString().Trim()));
                        MaechulTotal.T1SAAMT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaechulTotal.T1SAAMT)) +
                                               Convert.ToDouble(ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim()));
                        MaechulTotal.T1SAVAT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaechulTotal.T1SAVAT)) +
                                               Convert.ToDouble(ds.Tables[0].Rows[i]["V1VAT"].ToString().Trim()));
                    }
                    else  // 주민등록번호 발행분
                    {
                        MaechulTotal.T1JUSEQ = Convert.ToString(Convert.ToInt32(Get_Numeric(MaechulTotal.T1JUSEQ)) + 1);
                        MaechulTotal.T1JUCNT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaechulTotal.T1JUCNT)) +
                                               Convert.ToDouble(ds.Tables[0].Rows[i]["CNT"].ToString().Trim()));
                        MaechulTotal.T1JUAMT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaechulTotal.T1JUAMT)) +
                                               Convert.ToDouble(ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim()));
                        MaechulTotal.T1JUVAT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaechulTotal.T1JUVAT)) +
                                               Convert.ToDouble(ds.Tables[0].Rows[i]["V1VAT"].ToString().Trim()));
                    }

                }//for..end

                // (전자세금계산분) 매출합계 (TOTAL RECORD)
                MaechulTotal.FILLER = sFill.PadRight(30); // 30자리 : 공백

                MaechulTotal.T1AMT = UP_Multi_Key_Fill(Get_Numeric(MaechulTotal.T1AMT.ToString().Trim()), 15); // 15자리 :  공급가액
                MaechulTotal.T1VAT = UP_Multi_Key_Fill(Get_Numeric(MaechulTotal.T1VAT.ToString().Trim()), 14); // 14자리 :  세액

                MaechulTotal.T1SAAMT = UP_Multi_Key_Fill(Get_Numeric(MaechulTotal.T1SAAMT.ToString().Trim()), 15); // 15자리 :  공급가액
                MaechulTotal.T1SAVAT = UP_Multi_Key_Fill(Get_Numeric(MaechulTotal.T1SAVAT.ToString().Trim()), 14); // 14자리 :  세액

                MaechulTotal.T1JUAMT = UP_Multi_Key_Fill(Get_Numeric(MaechulTotal.T1JUAMT.ToString().Trim()), 15); // 15자리 :  공급가액
                MaechulTotal.T1JUVAT = UP_Multi_Key_Fill(Get_Numeric(MaechulTotal.T1JUVAT.ToString().Trim()), 14); // 14자리 :  세액


                sData = MaechulTotal.T1GUBN;                                            // 자료구분 : 1자리
                sData += MaechulTotal.T1SAUPNO;                                         // 제출자 사업자등록번호 : 10자리
                // 합계분
                sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaechulTotal.T1SEQ)));    // 거래처수    :   7자리
                sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaechulTotal.T1CNT)));    // 계산서 매수 :   7자리
                sData += MaechulTotal.T1AMT;   // 공급가액    :  15자리
                sData += MaechulTotal.T1VAT;   // 세    액    :  14자리

                //sData += string.Format("{0:D15}", Convert.ToInt64(Get_Numeric(MaechulTotal.T1AMT)));   // 공급가액    :  15자리
                //sData += string.Format("{0:D14}", Convert.ToInt64(Get_Numeric(MaechulTotal.T1VAT)));   // 세    액    :  14자리
                // 사업자등록번호 발행분
                sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaechulTotal.T1SASEQ)));  // 거래처수    :   7자리
                sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaechulTotal.T1SACNT)));  // 계산서 매수 :   7자리
                sData += MaechulTotal.T1SAAMT; // 공급가액    :  15자리
                sData += MaechulTotal.T1SAVAT; // 세    액    :  14자리

                //sData += string.Format("{0:D15}", Convert.ToInt64(Get_Numeric(MaechulTotal.T1SAAMT))); // 공급가액    :  15자리
                //sData += string.Format("{0:D14}", Convert.ToInt64(Get_Numeric(MaechulTotal.T1SAVAT))); // 세    액    :  14자리
                // 주민등록번호 발행분
                sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaechulTotal.T1JUSEQ)));  // 거래처수    :   7자리
                sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaechulTotal.T1JUCNT)));  // 계산서 매수 :   7자리
                sData += MaechulTotal.T1JUAMT; // 공급가액    :  15자리
                sData += MaechulTotal.T1JUVAT; // 세    액    :  14자리			

                //sData += string.Format("{0:D15}", Convert.ToInt64(Get_Numeric(MaechulTotal.T1JUAMT))); // 공급가액    :  15자리
                //sData += string.Format("{0:D14}", Convert.ToInt64(Get_Numeric(MaechulTotal.T1JUVAT))); // 세    액    :  14자리			
                sData += MaechulTotal.FILLER;        //30자리

                sw.WriteLine(sData);

            }
            
            #endregion

            // ---------------   매  입  세  금  계  산  서   ---------------
            #region Description : 매입생성

            ds.Clear();
            iCnt = 0;

            MaeIpTotal.T2GUBN = "";          //1자리
            MaeIpTotal.T2SAUPNO = "";        //10자리
            MaeIpTotal.T2CNT = "";           //7자리
            MaeIpTotal.T2AMT = "";           //15자리
            MaeIpTotal.T2VAT = "";           //14자리
            MaeIpTotal.T2SASEQ = "";          //7자리
            MaeIpTotal.T2SACNT = "";          //7자리
            MaeIpTotal.T2SAAMT = "";          //15자리
            MaeIpTotal.T2SAVAT = "";          //14자리
            MaeIpTotal.T2JUSEQ = "";          //7자리
            MaeIpTotal.T2JUCNT = "";          //7자리
            MaeIpTotal.T2JUAMT = "";          //15자리
            MaeIpTotal.T2JUVAT = "";          //14자리
            MaeIpTotal.FILLER = "";          //28자리

            // -------------------------------------------------------------------------------------------------- //
            //                               전자세금계산서 이외분 매입 합계 처리 (2,5)  (NOT IN)                   //
            // -------------------------------------------------------------------------------------------------- //
            this.DbConnector.CommandClear(); // AVSUMTABMF 
            this.DbConnector.Attach("TY_P_AC_41N3Q146",
                                    this.DTP01_ELXYYMM.GetValue().ToString(),    // 년도
                                    this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1), // 신고구분(1기, 2기)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),  // 확정구분(1.예정, 2.확정)
                                    "1"                                          // 계산서구분(1.매입, 2.매출)
                                    );
            ds = this.DbConnector.ExecuteDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    iCnt = iCnt + 1;

                    Maechul.GUBN = "2";
                    Maechul.SEQ = string.Format("{0:D4}", iCnt);

                    if (this.CBO01_VNGUBUN.GetValue().ToString() == "1") //본점
                    {
                        Maechul.SAUPNO = sSAUPNO; // 사업자등록번호(10자리)
                        Maechul.CODE = sTAXAREA;  // 관할세무소(3자리)
                    }
                    else
                    {
                        Maechul.SAUPNO = sSAUPNO; // 사업자등록번호(10자리)
                        Maechul.CODE = sTAXAREA;   // 관할세무소(3자리)
                    }

                    if (ds.Tables[0].Rows[i]["VNSJGB"].ToString() == "1")
                    {
                        Maechul.SAUPNO1 = ds.Tables[0].Rows[i]["S1SAUPNO"].ToString().Substring(0, 10);
                    }
                    else
                    {
                        Maechul.SAUPNO1 = "8888888888";
                        Maechul.GUBN = "9";
                    }

                    sStrTemp = " ";
                    sStrTemp += ds.Tables[0].Rows[i]["VNSANGHO"].ToString();
                    sStrTemp += new String(Convert.ToChar(" "), (29 - Encoding.Default.GetByteCount(ds.Tables[0].Rows[i]["VNSANGHO"].ToString().Trim())));
                    Maechul.SANGHO = sStrTemp;

                    sStrTemp = "";
                    sStrTemp += ds.Tables[0].Rows[i]["VNUPTE"].ToString();
                    sStrTemp += new String(Convert.ToChar(" "), (17 - Encoding.Default.GetByteCount(ds.Tables[0].Rows[i]["VNUPTE"].ToString().Trim())));
                    Maechul.UPTE = sStrTemp;

                    sStrTemp = "";
                    sStrTemp += ds.Tables[0].Rows[i]["VNUPJONG"].ToString();
                    sStrTemp += new String(Convert.ToChar(" "), (24 - Encoding.Default.GetByteCount(ds.Tables[0].Rows[i]["VNUPJONG"].ToString().Trim())));
                    Maechul.UPJONG = sStrTemp;

                    Maechul.CNT = string.Format("{0:D7}", Convert.ToInt64(ds.Tables[0].Rows[i]["CNT"].ToString().Trim()));

                    Maechul.AMT = UP_Multi_Key_Fill(Get_Numeric(ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim()), 14); // 14자리 :  공급가액
                    Maechul.GONG = "00";                        //  2자리 : 삭제항목으로 0으로 입력 [2014.01월 개정됨] - 공란수
                    Maechul.VAT = UP_Multi_Key_Fill(Get_Numeric(ds.Tables[0].Rows[i]["V1VAT"].ToString().Trim()), 13); // 13자리 :  세액


                    //if (Convert.ToInt64(ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim()) < 0)
                    //{
                    //    string sTempAmt = string.Format("{0:D14}", Convert.ToInt64(ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim()) * -1);

                    //    Maechul.AMT = sTempAmt.Substring(0, 13) + "}";

                    //    Maechul.GONG = string.Format("{0:D2}", Convert.ToInt64(Convert.ToString(14 - ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim().Length + 1)));
                    //}
                    //else
                    //{
                    //    Maechul.AMT = string.Format("{0:D14}", Convert.ToInt64(ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim()));

                    //    Maechul.GONG = string.Format("{0:D2}", Convert.ToInt64(Convert.ToString(14 - ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim().Length)));
                    //}

                    //if (Convert.ToInt64(ds.Tables[0].Rows[i]["V1VAT"].ToString().Trim()) < 0)
                    //{
                    //    string sTempAmt = string.Format("{0:D13}", Convert.ToInt64(ds.Tables[0].Rows[i]["V1VAT"].ToString().Trim()) * -1);

                    //    Maechul.VAT = sTempAmt.Substring(0, 12) + "}";
                    //}
                    //else
                    //{
                    //    Maechul.VAT = string.Format("{0:D13}", Convert.ToInt64(ds.Tables[0].Rows[i]["V1VAT"].ToString().Trim()));
                    //}

                    Maechul.UPTE = sFill.PadRight(17);         // 17자리 : 삭제항목으로 SPACE 으로 입력 [2014.01월 개정됨] - 거래자업태
                    Maechul.UPJONG = sFill.PadRight(25);       // 25자리 : 삭제항목으로 SPACE 으로 입력 [2014.01월 개정됨] - 거래자종목
                    Maechul.GONG = "00";                       //  2자리 : 삭제항목으로 0으로 입력 [2014.01월 개정됨] - 공란수
                    Maechul.DOCODE = "0";
                    Maechul.SOCODE = "0";
                    Maechul.BOOKNO = "8501"; // 신고서 에서 변경됨 9501
                    Maechul.FILLER = sFill.PadRight(28);         // 28자리 : 공백

                    sData = Maechul.GUBN;      // 1자리 :자료구분
                    sData += Maechul.SAUPNO;   // 10자리 :보고자등록번호
                    sData += Maechul.SEQ;      // 4자리 :일련번호 
                    sData += Maechul.SAUPNO1;  // 10자리 :거래자등록번호
                    sData += Maechul.SANGHO;   // 30자리 :거래자상호
                    sData += Maechul.UPTE;     // 17자리 :거래자업태
                    sData += Maechul.UPJONG;   // 25자리 :거래자종목
                    sData += Maechul.CNT;      // 7자리 :세금계산서매수
                    sData += Maechul.GONG;     // 2자리 :공란수
                    sData += Maechul.AMT;      // 14자리 :공급가액
                    sData += Maechul.VAT;      // 13자리 :세액
                    sData += Maechul.DOCODE;   // 1자리 :주류여부
                    sData += Maechul.SOCODE;   // 1자리 :주류코드(소매)
                    sData += Maechul.BOOKNO;   // 4자리 :권번호
                    sData += Maechul.CODE;     // 3자리 :제출서
                    sData += Maechul.FILLER;   // 28자리 :공백

                    sw.WriteLine(sData); //  전자세금계산서 이외분)매입자료(Data Record) 저장

                    MaeIpTotal.T2GUBN = "4";

                    if (this.CBO01_VNGUBUN.GetValue().ToString() == "1") //본점
                    {
                        MaeIpTotal.T2SAUPNO = sSAUPNO; // 사업자등록번호(10자리)
                    }
                    else
                    {
                        MaeIpTotal.T2SAUPNO = sSAUPNO; // 사업자등록번호(10자리)
                    }

                    MaeIpTotal.T2CNT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaeIpTotal.T2CNT)) +
                                       Convert.ToDouble(ds.Tables[0].Rows[i]["CNT"].ToString().Trim()));
                    MaeIpTotal.T2AMT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaeIpTotal.T2AMT)) +
                                       Convert.ToDouble(ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim()));
                    MaeIpTotal.T2VAT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaeIpTotal.T2VAT)) +
                                       Convert.ToDouble(ds.Tables[0].Rows[i]["V1VAT"].ToString().Trim()));

                    if (ds.Tables[0].Rows[i]["VNSJGB"].ToString() == "1")
                    {
                        MaeIpTotal.T2SASEQ = Convert.ToString(Convert.ToInt32(Get_Numeric(MaeIpTotal.T2SASEQ)) + 1);
                        MaeIpTotal.T2SACNT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaeIpTotal.T2SACNT)) +
                                             Convert.ToDouble(ds.Tables[0].Rows[i]["CNT"].ToString().Trim()));
                        MaeIpTotal.T2SAAMT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaeIpTotal.T2SAAMT)) +
                                             Convert.ToDouble(ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim()));
                        MaeIpTotal.T2SAVAT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaeIpTotal.T2SAVAT)) +
                                             Convert.ToDouble(ds.Tables[0].Rows[i]["V1VAT"].ToString().Trim()));
                    }
                    else
                    {
                        MaeIpTotal.T2JUSEQ = Convert.ToString(Convert.ToInt32(Get_Numeric(MaeIpTotal.T2JUSEQ)) + 1);
                        MaeIpTotal.T2JUCNT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaeIpTotal.T2JUCNT)) +
                                             Convert.ToDouble(ds.Tables[0].Rows[i]["CNT"].ToString().Trim()));
                        MaeIpTotal.T2JUAMT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaeIpTotal.T2JUAMT)) +
                                             Convert.ToDouble(ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim()));
                        MaeIpTotal.T2JUVAT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaeIpTotal.T2JUVAT)) +
                                             Convert.ToDouble(ds.Tables[0].Rows[i]["V1VAT"].ToString().Trim()));
                    }

                }//for..end

                MaeIpTotal.FILLER = sFill.PadRight(30);         // 30자리 공백 (2010년 4월 22일추가)

                MaeIpTotal.T2SEQ = string.Format("{0:D4}", iCnt);

                sData = MaeIpTotal.T2GUBN;           // 자료구분 : 1자리
                sData += MaeIpTotal.T2SAUPNO;         // 제출자 사업자등록번호 : 10자리

                // 합계분
                MaeIpTotal.T2AMT = UP_Multi_Key_Fill(Get_Numeric(MaeIpTotal.T2AMT.ToString().Trim()), 15); // 15자리 :  공급가액
                MaeIpTotal.T2VAT = UP_Multi_Key_Fill(Get_Numeric(MaeIpTotal.T2VAT.ToString().Trim()), 14); // 14자리 :  세액
                sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaeIpTotal.T2SEQ)));       // 거래처수    :   7자리
                sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaeIpTotal.T2CNT)));       // 계산서 매수 :   7자리
                sData += MaeIpTotal.T2AMT;      // 공급가액    :  15자리
                sData += MaeIpTotal.T2VAT;      // 세    액    :  14자리
                //sData += string.Format("{0:D15}", Convert.ToInt64(MaeIpTotal.T2AMT));      // 공급가액    :  15자리
                //sData += string.Format("{0:D14}", Convert.ToInt64(MaeIpTotal.T2VAT));      // 세    액    :  14자리

                // 시업자등록번호 수취분
                MaeIpTotal.T2SAAMT = UP_Multi_Key_Fill(Get_Numeric(MaeIpTotal.T2SAAMT.ToString().Trim()), 15); // 15자리 :  공급가액
                MaeIpTotal.T2SAVAT = UP_Multi_Key_Fill(Get_Numeric(MaeIpTotal.T2SAVAT.ToString().Trim()), 14); // 14자리 :  세액
                sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaeIpTotal.T2SASEQ)));     // 거래처수    :   7자리
                sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaeIpTotal.T2SACNT)));     // 계산서 매수 :   7자리
                sData += MaeIpTotal.T2SAAMT;    // 공급가액    :  15자리
                sData += MaeIpTotal.T2SAVAT;    // 세    액    :  14자리
                //sData += string.Format("{0:D15}", Convert.ToInt64(MaeIpTotal.T2SAAMT));    // 공급가액    :  15자리
                //sData += string.Format("{0:D14}", Convert.ToInt64(MaeIpTotal.T2SAVAT));    // 세    액    :  14자리

                // 주민등록번호 수취분		
                MaeIpTotal.T2JUAMT = UP_Multi_Key_Fill(Get_Numeric(MaeIpTotal.T2JUAMT.ToString().Trim()), 15); // 15자리 :  공급가액
                MaeIpTotal.T2JUVAT = UP_Multi_Key_Fill(Get_Numeric(MaeIpTotal.T2JUVAT.ToString().Trim()), 14); // 14자리 :  세액
                sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaeIpTotal.T2JUSEQ)));     // 거래처수    :   7자리
                sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaeIpTotal.T2JUCNT)));     // 계산서 매수 :   7자리
                sData += MaeIpTotal.T2JUAMT;    // 공급가액    :  15자리
                sData += MaeIpTotal.T2JUVAT;    // 세    액    :  14자리
                //sData += string.Format("{0:D15}", Convert.ToInt64(MaeIpTotal.T2JUAMT));    // 공급가액    :  15자리
                //sData += string.Format("{0:D14}", Convert.ToInt64(MaeIpTotal.T2JUVAT));    // 세    액    :  14자리
                sData += MaeIpTotal.FILLER;        //30자리

                sw.WriteLine(sData);
            }

            // --------------------------------------------------------------------------------- //
            // --------------------------------------------------------------------------------- //
            // 전자세금계산서분 매입합계 ( TOTAL RECORD ) 

            ds.Clear();

            MaeIpTotal.T2GUBN = "";          //1자리
            MaeIpTotal.T2SAUPNO = "";        //10자리
            MaeIpTotal.T2CNT = "";           //7자리
            MaeIpTotal.T2AMT = "";           //15자리
            MaeIpTotal.T2VAT = "";           //14자리
            MaeIpTotal.T2SASEQ = "";          //7자리
            MaeIpTotal.T2SACNT = "";          //7자리
            MaeIpTotal.T2SAAMT = "";          //15자리
            MaeIpTotal.T2SAVAT = "";          //14자리
            MaeIpTotal.T2JUSEQ = "";          //7자리
            MaeIpTotal.T2JUCNT = "";          //7자리
            MaeIpTotal.T2JUAMT = "";          //15자리
            MaeIpTotal.T2JUVAT = "";          //14자리
            MaeIpTotal.FILLER = "";          //28자리

            iCnt = 0;

            this.DbConnector.CommandClear(); // AVSUMTABMF 
            this.DbConnector.Attach("TY_P_AC_41N3Q147",
                                    this.DTP01_ELXYYMM.GetValue().ToString(),    // 년도
                                    this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2), // 확정구분(1.예정, 2.확정)
                                    "1"                                          // 계산서구분(1.매입, 2.매출)
                                    );
            ds = this.DbConnector.ExecuteDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    iCnt = iCnt + 1;

                    MaeIpTotal.T2GUBN = "6";

                    if (this.CBO01_VNGUBUN.GetValue().ToString() == "1") //본점
                    {
                        MaeIpTotal.T2SAUPNO = sSAUPNO; // 사업자등록번호(10자리)
                    }
                    else
                    {
                        MaeIpTotal.T2SAUPNO = sSAUPNO; // 사업자등록번호(10자리)
                    }

                    MaeIpTotal.T2CNT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaeIpTotal.T2CNT)) +
                                       Convert.ToDouble(ds.Tables[0].Rows[i]["CNT"].ToString().Trim()));
                    MaeIpTotal.T2AMT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaeIpTotal.T2AMT)) +
                                       Convert.ToDouble(ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim()));
                    MaeIpTotal.T2VAT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaeIpTotal.T2VAT)) +
                                       Convert.ToDouble(ds.Tables[0].Rows[i]["V1VAT"].ToString().Trim()));

                    if (ds.Tables[0].Rows[i]["VNSJGB"].ToString() == "1")
                    {
                        MaeIpTotal.T2SASEQ = Convert.ToString(Convert.ToInt32(Get_Numeric(MaeIpTotal.T2SASEQ)) + 1);
                        MaeIpTotal.T2SACNT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaeIpTotal.T2SACNT)) +
                                             Convert.ToDouble(ds.Tables[0].Rows[i]["CNT"].ToString().Trim()));
                        MaeIpTotal.T2SAAMT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaeIpTotal.T2SAAMT)) +
                                             Convert.ToDouble(ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim()));
                        MaeIpTotal.T2SAVAT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaeIpTotal.T2SAVAT)) +
                                             Convert.ToDouble(ds.Tables[0].Rows[i]["V1VAT"].ToString().Trim()));
                    }
                    else
                    {
                        MaeIpTotal.T2JUSEQ = Convert.ToString(Convert.ToInt32(Get_Numeric(MaeIpTotal.T2JUSEQ)) + 1);
                        MaeIpTotal.T2JUCNT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaeIpTotal.T2JUCNT)) +
                                             Convert.ToDouble(ds.Tables[0].Rows[i]["CNT"].ToString().Trim()));
                        MaeIpTotal.T2JUAMT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaeIpTotal.T2JUAMT)) +
                                             Convert.ToDouble(ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim()));
                        MaeIpTotal.T2JUVAT = Convert.ToString(Convert.ToDouble(Get_Numeric(MaeIpTotal.T2JUVAT)) +
                                             Convert.ToDouble(ds.Tables[0].Rows[i]["V1VAT"].ToString().Trim()));
                    }

                } //for..end

                MaeIpTotal.FILLER = sFill.PadRight(30);         // 30자리 공백 (2010년 4월 22일추가)
                MaeIpTotal.T2SEQ = string.Format("{0:D4}", iCnt);

                sData = MaeIpTotal.T2GUBN;           // 자료구분 : 1자리
                sData += MaeIpTotal.T2SAUPNO;         // 제출자 사업자등록번호 : 10자리
                // 합계분
                MaeIpTotal.T2AMT = UP_Multi_Key_Fill(Get_Numeric(MaeIpTotal.T2AMT.ToString().Trim()), 15); // 15자리 :  공급가액
                MaeIpTotal.T2VAT = UP_Multi_Key_Fill(Get_Numeric(MaeIpTotal.T2VAT.ToString().Trim()), 14); // 14자리 :  세액
                sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaeIpTotal.T2SEQ)));       // 거래처수    :   7자리
                sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaeIpTotal.T2CNT)));       // 계산서 매수 :   7자리
                sData += MaeIpTotal.T2AMT;      // 공급가액    :  15자리
                sData += MaeIpTotal.T2VAT;      // 세    액    :  14자리
                //sData += string.Format("{0:D15}", Convert.ToInt64(Get_Numeric(MaeIpTotal.T2AMT)));      // 공급가액    :  15자리
                //sData += string.Format("{0:D14}", Convert.ToInt64(Get_Numeric(MaeIpTotal.T2VAT)));      // 세    액    :  14자리

                // 사업자등록번호 수취분
                MaeIpTotal.T2SAAMT = UP_Multi_Key_Fill(Get_Numeric(MaeIpTotal.T2SAAMT.ToString().Trim()), 15); // 15자리 :  공급가액
                MaeIpTotal.T2SAVAT = UP_Multi_Key_Fill(Get_Numeric(MaeIpTotal.T2SAVAT.ToString().Trim()), 14); // 14자리 :  세액
                sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaeIpTotal.T2SASEQ)));     // 거래처수    :   7자리
                sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaeIpTotal.T2SACNT)));     // 계산서 매수 :   7자리
                sData += MaeIpTotal.T2SAAMT;    // 공급가액    :  15자리
                sData += MaeIpTotal.T2SAVAT;    // 세    액    :  14자리
                //sData += string.Format("{0:D15}", Convert.ToInt64(Get_Numeric(MaeIpTotal.T2SAAMT)));    // 공급가액    :  15자리
                //sData += string.Format("{0:D14}", Convert.ToInt64(Get_Numeric(MaeIpTotal.T2SAVAT)));    // 세    액    :  14자리

                // 주민등록번호 수취분	
                MaeIpTotal.T2JUAMT = UP_Multi_Key_Fill(Get_Numeric(MaeIpTotal.T2JUAMT.ToString().Trim()), 15); // 15자리 :  공급가액
                MaeIpTotal.T2JUVAT = UP_Multi_Key_Fill(Get_Numeric(MaeIpTotal.T2JUVAT.ToString().Trim()), 14); // 14자리 :  세액
                sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaeIpTotal.T2JUSEQ)));     // 거래처수    :   7자리
                sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaeIpTotal.T2JUCNT)));     // 계산서 매수 :   7자리
                sData += MaeIpTotal.T2JUAMT;    // 공급가액    :  15자리
                sData += MaeIpTotal.T2JUVAT;    // 세    액    :  14자리
                //sData += string.Format("{0:D15}", Convert.ToInt64(Get_Numeric(MaeIpTotal.T2JUAMT)));    // 공급가액    :  15자리
                //sData += string.Format("{0:D14}", Convert.ToInt64(Get_Numeric(MaeIpTotal.T2JUVAT)));    // 세    액    :  14자리

                sData += MaeIpTotal.FILLER;        //30자리

                sw.WriteLine(sData);

            }
            
            #endregion

        }
        #endregion

        #region Description : 매출처별 계산서 합계표(갑,을)-(V109)  : 매입처별 계산서 합계표(갑,을) - (V110)  -- UP_TAX_Create_V109()
        private void UP_TAX_Create_V109(StreamWriter sw)
        {

            string s시작년월일 = string.Empty;
            string s종료년월일 = string.Empty;

            string s매입시작년월일 = string.Empty;
            string s매입종료년월일 = string.Empty;

            string sFill = string.Empty;
            string sYEAR = this.DTP01_ELXYYMM.GetValue().ToString().Substring(0, 4);

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


            // 제출자 인적사항 조회 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_42B67344", // AVSUBMITMF 
                this.DTP01_ELXYYMM.GetValue().ToString(),
                getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  //신고구분(1기, 2기)
                getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),  // 확정구분(1.예정, 2.확정)
                this.CBO01_VNGUBUN.GetValue().ToString()   // 사업장(1본점, 2지점)
                );

            DataTable dt_tae = this.DbConnector.ExecuteDataTable();

            if (dt_tae.Rows.Count > 0)
            {
                sSAUPNO = dt_tae.Rows[0]["ASMSAUPNO"].ToString(); // 사업자등록번호(10자리)
                sSANGHO = dt_tae.Rows[0]["ASMSANGHO"].ToString(); // 상호명
                sNAMENM = dt_tae.Rows[0]["ASMNAMENM"].ToString(); // 대표자이름
                sCORPNO = dt_tae.Rows[0]["ASMCORPNO"].ToString(); // 법인번호 (13자리)
                sUPTAE = dt_tae.Rows[0]["ASMUPTAE"].ToString(); // 업태
                sEVENT = dt_tae.Rows[0]["ASMEVENT"].ToString(); // 종목
                sTELNUM = Up_Telnum_Convert(dt_tae.Rows[0]["ASMTELNUM"].ToString().Trim()); // 전화번호
                sVNADDRS = dt_tae.Rows[0]["ASMVNADDRS"].ToString(); // 사업장주소
                sTAXAREA = dt_tae.Rows[0]["ASMTAXAREA"].ToString(); // 관할세무소(3자리)
                sBUSTYPE = dt_tae.Rows[0]["ASMBUSTYPE"].ToString(); // 업종코드 (7자리)
            }

            struct_DV109_A DV109_A = new struct_DV109_A(); // 제출자
            struct_DV109_B DV109_B = new struct_DV109_B(); // 제출의무자인적사항

            struct_DV109_C DV109_C = new struct_DV109_C(); // 제출의무자별집계레코드(매출)
            struct_DV109_D DV109_D = new struct_DV109_D(); // 매출처별거래명세레코드
            struct_DV109_E DV109_E = new struct_DV109_E(); // 전자계산서 매출집계레코드(매출)

            struct_DV110_C DV110_C = new struct_DV110_C(); // 제출의무자별집계레코드(매입)
            struct_DV110_D DV110_D = new struct_DV110_D(); // 매입처별거래명세레코드
            struct_DV110_E DV110_E = new struct_DV110_E(); // 전자계산서 매출집계레코드(매입)

            // --------------------------------- 매출계산서 작업년월일 가져 오기  ---------------------------------

            DataTable dt_Maechul = new DataTable();

            string sCHKTAXCDGN = string.Empty;


            if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1) == "1") // 신고구분(1기, 2기)
            {
                if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2) == "1") // 확정구분(1.예정, 2.확정)
                {
                    s시작년월일 = sYEAR + "0101";
                    s종료년월일 = sYEAR + "0331";
                }
                else
                {
                    s시작년월일 = sYEAR + "0401";
                    s종료년월일 = sYEAR + "0630";
                };
            }
            else
            {
                if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2) == "1")
                {
                    s시작년월일 = sYEAR + "0701";
                    s종료년월일 = sYEAR + "0930";
                }
                else
                {
                    s시작년월일 = sYEAR + "1001";
                    s종료년월일 = sYEAR + "1231";
                };
            }

            sCHKTAXCDGN = "22,66"; // 매출

            this.DbConnector.CommandClear(); // AVTAXDETAF (내역)
            this.DbConnector.Attach("TY_P_AC_42327179",
                                    this.DTP01_ELXYYMM.GetValue().ToString(),    // 년도
                                    this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2), // 확정구분(1.예정, 2.확정)
                                    "2",                                         // 계산서구분(1.매입, 2.매출)
                                    sCHKTAXCDGN
                                    );

            dt_Maechul = this.DbConnector.ExecuteDataTable();

            // --------------------------------- 매입계산서 작업년월일 가져 오기  ---------------------------------

            DataTable dt_MaeIp = new DataTable();
            DataTable dt_Opt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_42B5G343",  // AVTAXOPTF(매입계산서 관련 옵션 자료)
                this.DTP01_ELXYYMM.GetValue().ToString().Substring(0, 4),     // 년도
                this.CBO01_VNGUBUN.GetValue().ToString(),   // 사업장(1본점, 2지점)
                getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),   // 확정구분(1.예정, 2.확정)
                "1"   // 계산서구분(1.매입, 2.매출)
                );

            dt_Opt = this.DbConnector.ExecuteDataTable();

            if (dt_Opt.Rows.Count > 0)
            {
                s매입시작년월일 = dt_Opt.Rows[0]["O1STYYMM"].ToString().Trim();
                s매입종료년월일 = dt_Opt.Rows[0]["O1EDYYMM"].ToString().Trim();
            }

            //-- 자료 세부자료 존재 확인

            sCHKTAXCDGN = "59,79"; // 매입

            this.DbConnector.CommandClear(); // AVTAXDETAF (내역)
            this.DbConnector.Attach("TY_P_AC_42327179",
                                    this.DTP01_ELXYYMM.GetValue().ToString(),    // 년도
                                    this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2), // 확정구분(1.예정, 2.확정)
                                    "1",                                         // 계산서구분(1.매입, 2.매출)
                                    sCHKTAXCDGN
                                    );
            dt_MaeIp = this.DbConnector.ExecuteDataTable();


            string sStrTemp = string.Empty;
            string sData = string.Empty;
            string sTAXCDGN_HAP = string.Empty;
            Int32 iCnt = 0;

            //매입계산서 관련 기초 자료가 존재 할경우 만 실행)
            if (dt_Maechul.Rows.Count > 0 || dt_MaeIp.Rows.Count > 0)
            {
                #region Description : 제출자 HEAD 정보
                //부가세 헤더 작성
                if (this.CBO01_VNGUBUN.GetValue().ToString() == "1") //본점
                {
                    DV109_A.DV109_A_DT01 = "A";                        //  1자리 : 자료구분
                    DV109_A.DV109_A_DT02 = "610";                      //  3자리 : 세무서 코드
                    DV109_A.DV109_A_DT03 = s종료년월일;                 //  8자리 : 제출일자
                    DV109_A.DV109_A_DT04 = "2";                        //  1자리 : 제출자구분
                    DV109_A.DV109_A_DT05 = "      ";                   //  6자리 : 세무대리인관리번호
                    DV109_A.DV109_A_DT06 = sSAUPNO;                    // 10자리 : 사업자등록번호
                    DV109_A.DV109_A_DT07 = sSANGHO;                    // 40자리 : 법인명(상호)  (재 정리)
                    DV109_A.DV109_A_DT08 = sCORPNO;                    // 13자리 : 주민(법인)등록번호  (재 정리)
                    DV109_A.DV109_A_DT09 = sNAMENM;                    // 30자리 : 대표자(성명)  (재 정리)
                    DV109_A.DV109_A_DT10 = "680070";                   // 10자리 : 소재지(우편번호)법정동코드  (재 정리)
                    DV109_A.DV109_A_DT11 = sVNADDRS;                   // 70자리 : 소재지(주소)  (재 정리)
                    DV109_A.DV109_A_DT12 = sTELNUM;                    // 15자리 : 전화번호  (재 정리)
                    DV109_A.DV109_A_DT13 = "00001";                    //  5자리 : 제출건수계
                    DV109_A.DV109_A_DT14 = "101";                      //  3자리 : 사용한한글코드종류
                    DV109_A.DV109_A_DT15 = sFill.PadRight(15);         // 15자리  :공란

                    DV109_B.DV109_B_DT01 = "B";                        //  1자리  : 자료구분
                    DV109_B.DV109_B_DT02 = "610";                      //  3자리  : 세무서 코드
                    DV109_B.DV109_B_DT03 = "000001";                   //  6자리  : 일련번호
                    DV109_B.DV109_B_DT04 = sSAUPNO;                    // 10자리  : 사업자등록번호  (재 정리)
                    DV109_B.DV109_B_DT05 = sSANGHO;                    // 40자리  : 법인명(상호)  (재 정리)
                    DV109_B.DV109_B_DT06 = sNAMENM;                    // 30자리  : 대표자(성명)  (재 정리)
                    DV109_B.DV109_B_DT07 = "680070";                   // 10자리  : 소재지(우편번호)법정동코드  (재 정리)
                    DV109_B.DV109_B_DT08 = sVNADDRS;                   // 70자리  : 소재지(주소)  (재 정리)
                    DV109_B.DV109_B_DT09 = sFill.PadRight(60);         // 60자리  :공란
                }
                else //지점
                {
                    DV109_A.DV109_A_DT01 = "A";                        //  1자리 : 자료구분
                    DV109_A.DV109_A_DT02 = "107";                      //  3자리 : 세무서 코드
                    DV109_A.DV109_A_DT03 = s종료년월일;                 //  8자리 : 제출일자
                    DV109_A.DV109_A_DT04 = "2";                        //  1자리 : 제출자구분
                    DV109_A.DV109_A_DT05 = "      ";                   //  6자리 : 세무대리인관리번호
                    DV109_A.DV109_A_DT06 = sSAUPNO;                    // 10자리 : 사업자등록번호
                    DV109_A.DV109_A_DT07 = sSANGHO;                    // 40자리 :  법인명(상호) (재 정리)
                    DV109_A.DV109_A_DT08 = sCORPNO;                    // 13자리 : 주민(법인)등록번호  (재 정리)
                    DV109_A.DV109_A_DT09 = sNAMENM;                    // 30자리 : 대표자(성명)  (재 정리)
                    DV109_A.DV109_A_DT10 = "150777";                   // 10자리 : 소재지(우편번호)법정동코드  (재 정리)
                    DV109_A.DV109_A_DT11 = sVNADDRS;                   // 70자리 : 소재지(주소)  (재 정리)
                    DV109_A.DV109_A_DT12 = sTELNUM;                    // 15자리 : 전화번호  (재 정리)
                    DV109_A.DV109_A_DT13 = "00001";                    //  5자리 : 제출건수계
                    DV109_A.DV109_A_DT14 = "101";                      //  3자리 : 사용한한글코드종류
                    DV109_A.DV109_A_DT15 = sFill.PadRight(15);         // 15자리  :공란

                    DV109_B.DV109_B_DT01 = "B";                        //  1자리 : 자료구분
                    DV109_B.DV109_B_DT02 = "107";                      //  3자리 : 세무서 코드
                    DV109_B.DV109_B_DT03 = "000001";                   //  6자리 : 일련번호
                    DV109_B.DV109_B_DT04 = sSAUPNO;                    // 10자리 : 사업자등록번호
                    DV109_B.DV109_B_DT05 = sSANGHO;                    // 40자리 : 법인명(상호)  (재 정리)
                    DV109_B.DV109_B_DT06 = sNAMENM;                    // 30자리 : 대표자(성명)  (재 정리)
                    DV109_B.DV109_B_DT07 = "150777";                   // 10자리 : 소재지(우편번호)법정동코드  (재 정리)
                    DV109_B.DV109_B_DT08 = sVNADDRS;                   // 70자리 : 소재지(주소)  (재 정리)
                    DV109_B.DV109_B_DT09 = sFill.PadRight(60);         // 60자리  :공란
                }

                sData = DV109_A.DV109_A_DT01;
                sData += DV109_A.DV109_A_DT02;
                sData += DV109_A.DV109_A_DT03;
                sData += DV109_A.DV109_A_DT04;
                sData += DV109_A.DV109_A_DT05;
                sData += DV109_A.DV109_A_DT06;

                sStrTemp = DV109_A.DV109_A_DT07.Trim(); // 40자리  법인명(상호)
                sStrTemp += new String(Convert.ToChar(" "), (40 - Encoding.Default.GetByteCount(DV109_A.DV109_A_DT07.Trim())));
                sData += sStrTemp;

                sStrTemp = DV109_A.DV109_A_DT08.Trim(); // 13자리  주민(법인)등록번호
                sStrTemp += new String(Convert.ToChar(" "), (13 - Encoding.Default.GetByteCount(DV109_A.DV109_A_DT08.Trim())));
                sData += sStrTemp;

                sStrTemp = DV109_A.DV109_A_DT09.Trim(); // 30자리 대표자(성명)
                sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(DV109_A.DV109_A_DT09.Trim())));
                sData += sStrTemp;

                sStrTemp = DV109_A.DV109_A_DT10.Trim(); // 10자리 소재지(우편번호)법정동코드
                sStrTemp += new String(Convert.ToChar(" "), (10 - Encoding.Default.GetByteCount(DV109_A.DV109_A_DT10.Trim())));
                sData += sStrTemp;

                sStrTemp = DV109_A.DV109_A_DT11.Trim(); // 70자리  소재지(주소)
                sStrTemp += new String(Convert.ToChar(" "), (70 - Encoding.Default.GetByteCount(DV109_A.DV109_A_DT11.Trim())));
                sData += sStrTemp;

                sStrTemp = DV109_A.DV109_A_DT12.Trim(); // 15자리 전화번호
                sStrTemp += new String(Convert.ToChar(" "), (15 - Encoding.Default.GetByteCount(DV109_A.DV109_A_DT12.Trim())));
                sData += sStrTemp;

                sData += DV109_A.DV109_A_DT13;
                sData += DV109_A.DV109_A_DT14;
                sData += DV109_A.DV109_A_DT15;

                sw.WriteLine(sData);  // A   RECORD 

                // --------------   B   RECORD   ------------------- //
                sData = DV109_B.DV109_B_DT01;
                sData += DV109_B.DV109_B_DT02;
                sData += DV109_B.DV109_B_DT03;
                sData += DV109_B.DV109_B_DT04;

                sStrTemp = DV109_B.DV109_B_DT05.Trim(); // 40자리  법인명(상호)
                sStrTemp += new String(Convert.ToChar(" "), (40 - Encoding.Default.GetByteCount(DV109_B.DV109_B_DT05.Trim())));
                sData += sStrTemp;

                sStrTemp = DV109_B.DV109_B_DT06.Trim(); // 30자리  대표자(성명)
                sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(DV109_B.DV109_B_DT06.Trim())));
                sData += sStrTemp;

                sStrTemp = DV109_B.DV109_B_DT07.Trim(); // 10자리 사업장(우편번호)법정동코드
                sStrTemp += new String(Convert.ToChar(" "), (10 - Encoding.Default.GetByteCount(DV109_B.DV109_B_DT07.Trim())));
                sData += sStrTemp;

                sStrTemp = DV109_B.DV109_B_DT08.Trim(); // 70자리 사업장소재지(주소)
                sStrTemp += new String(Convert.ToChar(" "), (70 - Encoding.Default.GetByteCount(DV109_B.DV109_B_DT08.Trim())));
                sData += sStrTemp;

                sData += DV109_B.DV109_B_DT09;

                sw.WriteLine(sData);   //  B   RECORD

                #endregion
            }

            // -------------------------             매    출   계   산   서              ------------------------
            if (dt_Maechul.Rows.Count > 0)
            {
                #region Description :   매    출   계   산   서

                iCnt = 0;
                sTAXCDGN_HAP = "22"; // 매출

                DataSet ds = new DataSet();
                DataSet ds_109 = new DataSet();

                // -------------------------------------------------------------------------------------------------- //
                //                               전자계산서 이외분 매출 합계 처리                                       //
                // -------------------------------------------------------------------------------------------------- //

                this.DbConnector.CommandClear(); // AVTAXDETAF (내역)
                this.DbConnector.Attach("TY_P_AC_42327179",
                                        this.DTP01_ELXYYMM.GetValue().ToString(),    // 년도
                                        this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2), // 확정구분(1.예정, 2.확정)
                                        "2",                                         // 계산서구분(1.매입, 2.매출)
                                        sTAXCDGN_HAP
                                        );
                ds = this.DbConnector.ExecuteDataSet();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.DbConnector.CommandClear(); // AVTAXDETAF  (집계)
                    this.DbConnector.Attach("TY_P_AC_4236D180",
                                            this.DTP01_ELXYYMM.GetValue().ToString(),    // 년도
                                            this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                                            getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                                            getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2), // 확정구분(1.예정, 2.확정)
                                            "2",                                         // 계산서구분(1.매입, 2.매출)
                                            sTAXCDGN_HAP
                                            );
                    ds_109 = this.DbConnector.ExecuteDataSet();

                    // ------------------------   C 레코드 생성 처리(매출계산서 집계)   ---------------------------------  //
                    DV109_C.DV109_C_DT01 = "C";                              // 레코드구분 : 1자리
                    DV109_C.DV109_C_DT02 = "17";                             // 자료구분 : 2자리
                    DV109_C.DV109_C_DT03 = getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1); // 기구분 : 1자리 (1년에 한번이면 2 )
                    DV109_C.DV109_C_DT04 = getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2); // 신고구분 : 1자리 (1년에 한번이면 2 )
                    if (this.CBO01_VNGUBUN.GetValue().ToString() == "1") //본점
                    {
                        DV109_C.DV109_C_DT05 = sTAXAREA;              // 세무서 코드 : 3자리
                        DV109_C.DV109_C_DT06 = "000001";              // 일련번호 : 6자리
                        DV109_C.DV109_C_DT07 = sSAUPNO;               // 제출자 사업자등록번호 : 10자리
                    }
                    else
                    {
                        DV109_C.DV109_C_DT05 = sTAXAREA;             // 세무서 코드 : 3자리
                        DV109_C.DV109_C_DT06 = "000001";             // 일련번호 : 6자리
                        DV109_C.DV109_C_DT07 = sSAUPNO;              // 제출자 사업자등록번호 : 10자리
                    }

                    DV109_C.DV109_C_DT08 = this.DTP01_ELXYYMM.GetValue().ToString().Substring(0, 4);  // 귀속년도   : 4자리
                    DV109_C.DV109_C_DT09 = s시작년월일;                                // 거래시작일 : 8자리  (확인요망)
                    DV109_C.DV109_C_DT10 = s종료년월일;                                // 거래종료일 : 8자리  (확인요망)
                    DV109_C.DV109_C_DT11 = s종료년월일;                                // 작성일자   : 8자리

                    DV109_C.DV109_C_DT12 = ds_109.Tables[0].Rows[0]["DT_CNT"].ToString().Trim(); // 건수
                    DV109_C.DV109_C_DT13 = ds_109.Tables[0].Rows[0]["DT_MCNT"].ToString().Trim(); // 매수
                    DV109_C.DV109_C_DT15 = ds_109.Tables[0].Rows[0]["DT_AMT"].ToString().Trim(); // 매출금액

                    DV109_C.DV109_C_DT16 = ds_109.Tables[0].Rows[0]["DS_CNT"].ToString().Trim(); // 건수
                    DV109_C.DV109_C_DT17 = ds_109.Tables[0].Rows[0]["DS_MCNT"].ToString().Trim(); // 매수
                    DV109_C.DV109_C_DT19 = ds_109.Tables[0].Rows[0]["DS_AMT"].ToString().Trim(); // 매출금액

                    DV109_C.DV109_C_DT20 = ds_109.Tables[0].Rows[0]["DJ_CNT"].ToString().Trim(); // 건수
                    DV109_C.DV109_C_DT21 = ds_109.Tables[0].Rows[0]["DJ_MCNT"].ToString().Trim(); // 매수
                    DV109_C.DV109_C_DT23 = ds_109.Tables[0].Rows[0]["DJ_AMT"].ToString().Trim(); // 매출금액
                    DV109_C.DV109_C_DT24 = sFill.PadRight(97);                                   // 97자리 공란

                    sData = DV109_C.DV109_C_DT01;         //  1자리  : 레코드구분
                    sData += DV109_C.DV109_C_DT02;        //  2자리  : 자료구분  
                    sData += DV109_C.DV109_C_DT03;        //  1자리  : 기구분    
                    sData += DV109_C.DV109_C_DT04;        //  1자리  : 신고구분  
                    sData += DV109_C.DV109_C_DT05;        //  3자리  : 세무서    
                    sData += DV109_C.DV109_C_DT06;        //  6자리  : 일련번호  
                    sData += DV109_C.DV109_C_DT07;        // 10자리  :  제출자 사업자등록번호
                    sData += DV109_C.DV109_C_DT08;        //  4자리  : 귀속년도
                    sData += DV109_C.DV109_C_DT09;        //  8자리  : 거래기간시작년월일 
                    sData += DV109_C.DV109_C_DT10;        //  8자리  : 거래기간종료년월일 
                    sData += DV109_C.DV109_C_DT11;        //  8자리  : 작성일자

                    // 합계분
                    sData += string.Format("{0:D6}", Convert.ToInt64(DV109_C.DV109_C_DT12));        // 거래처수    :   6자리
                    sData += string.Format("{0:D6}", Convert.ToInt64(DV109_C.DV109_C_DT13));        // 계산서 매수 :   6자리
                    if (Convert.ToInt64(DV109_C.DV109_C_DT15) < 0)
                    {
                        string sTempAmt = string.Format("{0:D14}", Convert.ToInt64(DV109_C.DV109_C_DT15) * -1);
                        DV109_C.DV109_C_DT14 = "1";  // 음수금액 표시 : 1자리
                        DV109_C.DV109_C_DT15 = sStrTemp; // 매출금액 : 14자리
                    }
                    else
                    {
                        DV109_C.DV109_C_DT14 = "0";  // 음수금액 표시 : 1자리
                        DV109_C.DV109_C_DT15 = string.Format("{0:D14}", Convert.ToInt64(DV109_C.DV109_C_DT15));  // 매출금액 : 14자리
                    }
                    sData += DV109_C.DV109_C_DT14;        // 음수금액 표시 : 1자리
                    sData += string.Format("{0:D14}", Convert.ToInt64(DV109_C.DV109_C_DT15));       // 공급가액    :  14자리

                    // 사업자등록번호 발행분
                    sData += string.Format("{0:D6}", Convert.ToInt64(DV109_C.DV109_C_DT16));      // 거래처수    :   6자리
                    sData += string.Format("{0:D6}", Convert.ToInt64(DV109_C.DV109_C_DT17));      // 계산서 매수 :   6자리

                    if (Convert.ToInt64(DV109_C.DV109_C_DT19) < 0)
                    {
                        string sTempAmt = string.Format("{0:D14}", Convert.ToInt64(DV109_C.DV109_C_DT19) * -1);
                        DV109_C.DV109_C_DT18 = "1";  // 음수금액 표시 : 1자리
                        DV109_C.DV109_C_DT19 = sStrTemp; // 매출금액 : 14자리
                    }
                    else
                    {
                        DV109_C.DV109_C_DT18 = "0";  // 음수금액 표시 : 1자리
                        DV109_C.DV109_C_DT19 = string.Format("{0:D14}", Convert.ToInt64(DV109_C.DV109_C_DT19));  // 매출금액 : 14자리
                    }
                    sData += string.Format("{0:D14}", Convert.ToInt64(DV109_C.DV109_C_DT19));     // 공급가액    :  14자리

                    // 주민등록번호 발행분
                    sData += string.Format("{0:D6}", Convert.ToInt64(DV109_C.DV109_C_DT20));      // 거래처수    :   6자리
                    sData += string.Format("{0:D6}", Convert.ToInt64(DV109_C.DV109_C_DT21));      // 계산서 매수 :   6자리
                    if (Convert.ToInt64(DV109_C.DV109_C_DT23) < 0)
                    {
                        string sTempAmt = string.Format("{0:D14}", Convert.ToInt64(DV109_C.DV109_C_DT23) * -1);
                        DV109_C.DV109_C_DT22 = "1";  // 음수금액 표시 : 1자리
                        DV109_C.DV109_C_DT23 = sStrTemp; // 매출금액 : 14자리
                    }
                    else
                    {
                        DV109_C.DV109_C_DT22 = "0";  // 음수금액 표시 : 1자리
                        DV109_C.DV109_C_DT23 = string.Format("{0:D14}", Convert.ToInt64(DV109_C.DV109_C_DT23));  // 매출금액 : 14자리
                    }

                    sData += string.Format("{0:D14}", Convert.ToInt64(DV109_C.DV109_C_DT23));     // 공급가액    :  14자리
                    sData += DV109_C.DV109_C_DT24;        // 79자리 : 공백

                    sw.WriteLine(sData); // C RECOED 저장(집계)

                    // ----------------------------------------------------------------------------------------------  //
                    // ------------------------   D 레코드 매출 거래명세서 생성 처리  ---------------------------------  //
                    // ----------------------------------------------------------------------------------------------  //

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        iCnt = iCnt + 1;

                        DV109_D.DV109_D_DT01 = "D";                              // 레코드구분 : 1자리
                        DV109_D.DV109_D_DT02 = "17";                             // 자료구분 : 2자리
                        DV109_D.DV109_D_DT03 = getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1); // 기구분 : 1자리 (1년에 한번이면 2 )
                        DV109_D.DV109_D_DT04 = getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2); // 신고구분 : 1자리 (1년에 한번이면 2 )
                        if (this.CBO01_VNGUBUN.GetValue().ToString() == "1") //본점
                        {
                            DV109_D.DV109_D_DT05 = sTAXAREA;                      //  세무서 코드 : 3자리
                            DV109_D.DV109_D_DT06 = string.Format("{0:D6}", iCnt); // 일련번호 : 6자리
                            DV109_D.DV109_D_DT07 = sSAUPNO;               // 제출자 사업자등록번호 : 10자리
                        }
                        else
                        {
                            DV109_D.DV109_D_DT05 = sTAXAREA;                      //  세무서 코드 : 3자리
                            DV109_D.DV109_D_DT06 = string.Format("{0:D6}", iCnt); // 일련번호 : 6자리
                            DV109_D.DV109_D_DT07 = sSAUPNO;
                        }

                        sStrTemp = "";
                        sStrTemp += ds.Tables[0].Rows[i]["S2SAUPNO"].ToString();  // 매출처 사업자등록번호 : 10자리
                        sStrTemp += new String(Convert.ToChar(" "), (10 - Encoding.Default.GetByteCount(ds.Tables[0].Rows[i]["S2SAUPNO"].ToString().Trim())));
                        DV109_D.DV109_D_DT08 = sStrTemp;

                        sStrTemp = " ";
                        sStrTemp += ds.Tables[0].Rows[i]["VNSANGHO"].ToString();  // 매출처 상호 : 40자리
                        sStrTemp += new String(Convert.ToChar(" "), (39 - Encoding.Default.GetByteCount(ds.Tables[0].Rows[i]["VNSANGHO"].ToString().Trim())));
                        DV109_D.DV109_D_DT09 = sStrTemp;

                        DV109_D.DV109_D_DT10 = string.Format("{0:D5}", Convert.ToInt32(ds.Tables[0].Rows[i]["MAESU_CNT"].ToString().Trim())); // 계산서 매수 : 5자리

                        if (Convert.ToInt64(ds.Tables[0].Rows[i]["HAP_AMT"].ToString().Trim()) < 0)
                        {
                            string sTempAmt = string.Format("{0:D14}", Convert.ToInt64(ds.Tables[0].Rows[i]["HAP_AMT"].ToString().Trim()) * -1);

                            DV109_D.DV109_D_DT11 = "1";  // 음수금액 표시 : 1자리
                            DV109_D.DV109_D_DT12 = sStrTemp; // 매출금액 : 14자리
                        }
                        else
                        {
                            DV109_D.DV109_D_DT11 = "0";  // 음수금액 표시 : 1자리
                            DV109_D.DV109_D_DT12 = string.Format("{0:D14}", Convert.ToInt64(ds.Tables[0].Rows[i]["HAP_AMT"].ToString().Trim()));  // 매출금액 : 14자리
                        }

                        DV109_D.DV109_D_DT13 = sFill.PadRight(136); // 136자리 공란

                        sData = DV109_D.DV109_D_DT01;
                        sData += DV109_D.DV109_D_DT02;
                        sData += DV109_D.DV109_D_DT03;
                        sData += DV109_D.DV109_D_DT04;
                        sData += DV109_D.DV109_D_DT05;
                        sData += DV109_D.DV109_D_DT06;
                        sData += DV109_D.DV109_D_DT07;
                        sData += DV109_D.DV109_D_DT08;
                        sData += DV109_D.DV109_D_DT09;
                        sData += DV109_D.DV109_D_DT10;
                        sData += DV109_D.DV109_D_DT11;
                        sData += DV109_D.DV109_D_DT12;
                        sData += DV109_D.DV109_D_DT13;

                        sw.WriteLine(sData);  // D RECORD 저장(거래명세서)

                    } //for..end

                } // End .. 전자계산서 이외분 매출

                // -------------------------------------------------------------------------------------------------- //
                //                                 전자계산서분 매출 합계 처리                                         //
                // -------------------------------------------------------------------------------------------------- //

                ds.Clear();
                ds_109.Clear();

                sTAXCDGN_HAP = "66"; // 매출(전자신고)

                this.DbConnector.CommandClear(); // AVTAXDETAF (내역)
                this.DbConnector.Attach("TY_P_AC_42327179",
                                        this.DTP01_ELXYYMM.GetValue().ToString(),    // 년도
                                        this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2), // 확정구분(1.예정, 2.확정)
                                        "2",                                         // 계산서구분(1.매입, 2.매출)
                                        sTAXCDGN_HAP
                                        );
                ds = this.DbConnector.ExecuteDataSet();

                if (ds.Tables[0].Rows.Count > 0)
                {

                    this.DbConnector.CommandClear(); // AVTAXDETAF  (집계)
                    this.DbConnector.Attach("TY_P_AC_4236D180",
                                            this.DTP01_ELXYYMM.GetValue().ToString(),    // 년도
                                            this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                                            getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                                            getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2), // 확정구분(1.예정, 2.확정)
                                            "2",                                         // 계산서구분(1.매입, 2.매출)
                                            sTAXCDGN_HAP
                                            );
                    ds_109 = this.DbConnector.ExecuteDataSet();

                    // ------------------------   E 레코드 생성 처리(매출계산서 집계)   ---------------------------------  //
                    DV109_E.DV109_E_DT01 = "E";                              // 레코드구분 : 1자리
                    DV109_E.DV109_E_DT02 = "17";                             // 자료구분 : 2자리
                    DV109_E.DV109_E_DT03 = getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1); // 기구분 : 1자리 (1년에 한번이면 2 )
                    DV109_E.DV109_E_DT04 = getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2); // 신고구분 : 1자리 (1년에 한번이면 2 )
                    if (this.CBO01_VNGUBUN.GetValue().ToString() == "1") //본점
                    {
                        DV109_E.DV109_E_DT05 = sTAXAREA;       // 세무서 코드 : 3자리
                        DV109_E.DV109_E_DT06 = "000001";       // 일련번호 : 6자리
                        DV109_E.DV109_E_DT07 = sSAUPNO;        // 제출자 사업자등록번호 : 10자리
                    }
                    else
                    {
                        DV109_E.DV109_E_DT05 = sTAXAREA;      // 세무서 코드 : 3자리
                        DV109_E.DV109_E_DT06 = "000001";      // 일련번호 : 6자리
                        DV109_E.DV109_E_DT07 = sSAUPNO;       // 제출자 사업자등록번호 : 10자리
                    }

                    DV109_E.DV109_E_DT08 = this.DTP01_ELXYYMM.GetValue().ToString().Substring(0, 4);  // 귀속년도   : 4자리
                    DV109_E.DV109_E_DT09 = s시작년월일;                                // 거래시작일 : 8자리 (확인요망)
                    DV109_E.DV109_E_DT10 = s종료년월일;                                // 거래종료일 : 8자리 (확인요망)
                    DV109_E.DV109_E_DT11 = s종료년월일;                                // 작성일자   : 8자리

                    DV109_E.DV109_E_DT12 = ds_109.Tables[0].Rows[0]["DT_CNT"].ToString().Trim(); // 건수
                    DV109_E.DV109_E_DT13 = ds_109.Tables[0].Rows[0]["DT_MCNT"].ToString().Trim(); // 매수
                    DV109_E.DV109_E_DT15 = ds_109.Tables[0].Rows[0]["DT_AMT"].ToString().Trim(); // 매출금액

                    DV109_E.DV109_E_DT16 = ds_109.Tables[0].Rows[0]["DS_CNT"].ToString().Trim(); // 건수
                    DV109_E.DV109_E_DT17 = ds_109.Tables[0].Rows[0]["DS_MCNT"].ToString().Trim(); // 매수
                    DV109_E.DV109_E_DT19 = ds_109.Tables[0].Rows[0]["DS_AMT"].ToString().Trim(); // 매출금액

                    DV109_E.DV109_E_DT20 = ds_109.Tables[0].Rows[0]["DJ_CNT"].ToString().Trim(); // 건수
                    DV109_E.DV109_E_DT21 = ds_109.Tables[0].Rows[0]["DJ_MCNT"].ToString().Trim(); // 매수
                    DV109_E.DV109_E_DT23 = ds_109.Tables[0].Rows[0]["DJ_AMT"].ToString().Trim(); // 매출금액
                    DV109_E.DV109_E_DT24 = sFill.PadRight(97); // 97자리 공란

                    sData = DV109_E.DV109_E_DT01;         //  1자리  : 레코드구분
                    sData += DV109_E.DV109_E_DT02;        //  2자리  : 자료구분  
                    sData += DV109_E.DV109_E_DT03;        //  1자리  : 기구분    
                    sData += DV109_E.DV109_E_DT04;        //  1자리  : 신고구분  
                    sData += DV109_E.DV109_E_DT05;        //  3자리  : 세무서    
                    sData += DV109_E.DV109_E_DT06;        //  6자리  : 일련번호  
                    sData += DV109_E.DV109_E_DT07;        // 10자리  :  제출자 사업자등록번호
                    sData += DV109_E.DV109_E_DT08;        //  4자리  : 귀속년도
                    sData += DV109_E.DV109_E_DT09;        //  8자리  : 거래기간시작년월일 
                    sData += DV109_E.DV109_E_DT10;        //  8자리  : 거래기간종료년월일 
                    sData += DV109_E.DV109_E_DT11;        //  8자리  : 작성일자

                    // 합계분
                    sData += string.Format("{0:D6}", Convert.ToInt64(DV109_E.DV109_E_DT12));        // 6자리 : 거래처수     
                    sData += string.Format("{0:D6}", Convert.ToInt64(DV109_E.DV109_E_DT13));        // 6자리 : 계산서 매수  
                    if (Convert.ToInt64(DV109_E.DV109_E_DT15) < 0)
                    {
                        string sTempAmt = string.Format("{0:D14}", Convert.ToInt64(DV109_E.DV109_E_DT15) * -1);
                        DV109_E.DV109_E_DT14 = "1";      //  1자리 : 음수금액 표시
                        DV109_E.DV109_E_DT15 = sStrTemp; // 14자리 : 매출금액
                    }
                    else
                    {
                        DV109_E.DV109_E_DT14 = "0";  // 음수금액 표시 : 1자리
                        DV109_E.DV109_E_DT15 = string.Format("{0:D14}", Convert.ToInt64(DV109_E.DV109_E_DT15));  // 매출금액 : 14자리
                    }
                    sData += DV109_E.DV109_E_DT14;        // 제출자 사업자등록번호 : 10자리
                    sData += string.Format("{0:D14}", Convert.ToInt64(DV109_E.DV109_E_DT15));       // 공급가액    :  14자리

                    // 사업자등록번호 발행분
                    sData += string.Format("{0:D6}", Convert.ToInt64(DV109_E.DV109_E_DT16));      // 거래처수    :   6자리
                    sData += string.Format("{0:D6}", Convert.ToInt64(DV109_E.DV109_E_DT17));      // 계산서 매수 :   6자리

                    if (Convert.ToInt64(DV109_E.DV109_E_DT19) < 0)
                    {
                        string sTempAmt = string.Format("{0:D14}", Convert.ToInt64(DV109_E.DV109_E_DT19) * -1);
                        DV109_E.DV109_E_DT18 = "1";  // 음수금액 표시 : 1자리
                        DV109_E.DV109_E_DT19 = sStrTemp; // 매출금액 : 14자리
                    }
                    else
                    {
                        DV109_E.DV109_E_DT18 = "0";  // 음수금액 표시 : 1자리
                        DV109_E.DV109_E_DT19 = string.Format("{0:D14}", Convert.ToInt64(DV109_E.DV109_E_DT19));  // 매출금액 : 14자리
                    }
                    sData += DV109_E.DV109_E_DT18;   // 음수금액 표시 : 1자리
                    sData += string.Format("{0:D14}", Convert.ToInt64(DV109_E.DV109_E_DT19));     // 공급가액    :  14자리

                    // 주민등록번호 발행분
                    sData += string.Format("{0:D6}", Convert.ToInt64(DV109_E.DV109_E_DT20));      // 거래처수    :   6자리
                    sData += string.Format("{0:D6}", Convert.ToInt64(DV109_E.DV109_E_DT21));      // 계산서 매수 :   6자리
                    if (Convert.ToInt64(DV109_E.DV109_E_DT23) < 0)
                    {
                        string sTempAmt = string.Format("{0:D14}", Convert.ToInt64(DV109_E.DV109_E_DT23) * -1);
                        DV109_E.DV109_E_DT22 = "1";  // 음수금액 표시 : 1자리
                        DV109_E.DV109_E_DT23 = sStrTemp; // 매출금액 : 14자리
                    }
                    else
                    {
                        DV109_E.DV109_E_DT22 = "0";  // 음수금액 표시 : 1자리
                        DV109_E.DV109_E_DT23 = string.Format("{0:D14}", Convert.ToInt64(DV109_E.DV109_E_DT23));  // 매출금액 : 14자리
                    }
                    sData += DV109_E.DV109_E_DT22;   // 음수금액 표시 : 1자리
                    sData += string.Format("{0:D14}", Convert.ToInt64(DV109_E.DV109_E_DT23));     // 공급가액  :  14자리
                    sData += DV109_E.DV109_E_DT24;        // 97자리 : 공백

                    sw.WriteLine(sData); // E RECORD 저장(집계)

                } // End .. 전자계산서분 매출 합계 
                #endregion

            } // End ... If (매출계산서 관련 기초 자료가 존재 할경우 만 실행)

            // -------------------------             매    입   계   산   서              ------------------------
            if (dt_MaeIp.Rows.Count > 0)
            {
                #region Description :  매    입   계   산   서

                iCnt = 0;
                sTAXCDGN_HAP = "59"; // 매입

                DataSet ds_110 = new DataSet();
                DataSet ds = new DataSet();

                // -------------------------------------------------------------------------------------------------- //
                //                               전자세금계산서 이외분 매입 합계 처리 (2,5)  (NOT IN)                   //
                // -------------------------------------------------------------------------------------------------- //

                this.DbConnector.CommandClear(); // AVTAXDETAF (내역)
                this.DbConnector.Attach("TY_P_AC_42327179",
                                        this.DTP01_ELXYYMM.GetValue().ToString(),    // 년도
                                        this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2), // 확정구분(1.예정, 2.확정)
                                        "1",                                         // 계산서구분(1.매입, 2.매출)
                                        sTAXCDGN_HAP
                                        );
                ds = this.DbConnector.ExecuteDataSet();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.DbConnector.CommandClear(); // AVTAXDETAF  (집계)
                    this.DbConnector.Attach("TY_P_AC_4236D180",
                                            this.DTP01_ELXYYMM.GetValue().ToString(),    // 년도
                                            this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                                            getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                                            getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2), // 확정구분(1.예정, 2.확정)
                                            "1",                                         // 계산서구분(1.매입, 2.매출)
                                            sTAXCDGN_HAP
                                            );
                    ds_110 = this.DbConnector.ExecuteDataSet();

                    // ------------------------   C 레코드 생성 처리(매입계산서 집계)   ---------------------------------  //
                    DV110_C.DV110_C_DT01 = "C";                        // 1자리 : 레코드구분
                    DV110_C.DV110_C_DT02 = "18";                       // 2자리 : 자료구분
                    DV110_C.DV110_C_DT03 = getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1);  // 1자리 : 기구분    (1년에 한번이면 2 )
                    DV110_C.DV110_C_DT04 = getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2); // 1자리 : 신고구분  (1년에 한번이면 2 )
                    if (this.CBO01_VNGUBUN.GetValue().ToString() == "1") //본점
                    {
                        DV110_C.DV110_C_DT05 = sTAXAREA;              // 세무서 코드 : 3자리
                        DV110_C.DV110_C_DT06 = "000001";              // 일련번호 : 6자리
                        DV110_C.DV110_C_DT07 = sSAUPNO;               // 제출자 사업자등록번호 : 10자리
                    }
                    else
                    {
                        DV110_C.DV110_C_DT05 = sTAXAREA;             // 세무서 코드 : 3자리
                        DV110_C.DV110_C_DT06 = "000001";             // 일련번호 : 6자리
                        DV110_C.DV110_C_DT07 = sSAUPNO;              // 제출자 사업자등록번호 : 10자리
                    }

                    DV110_C.DV110_C_DT08 = this.DTP01_ELXYYMM.GetValue().ToString().Substring(0, 4);  // 4자리 : 귀속년도  
                    DV110_C.DV110_C_DT09 = s매입시작년월일;                                // 8자리 : 거래시작일 (확인요망)
                    DV110_C.DV110_C_DT10 = s매입종료년월일;                                // 8자리 : 거래종료일 (확인요망)
                    DV110_C.DV110_C_DT11 = s매입종료년월일;                                // 8자리 : 작성일자

                    DV110_C.DV110_C_DT12 = ds_110.Tables[0].Rows[0]["DT_CNT"].ToString().Trim(); // 건수
                    DV110_C.DV110_C_DT13 = ds_110.Tables[0].Rows[0]["DT_MCNT"].ToString().Trim(); // 매수
                    DV110_C.DV110_C_DT15 = ds_110.Tables[0].Rows[0]["DT_AMT"].ToString().Trim(); // 매출금액
                    DV110_C.DV110_C_DT16 = sFill.PadRight(151); // 151자리 공란

                    sData = DV110_C.DV110_C_DT01;         //  1자리  : 레코드구분
                    sData += DV110_C.DV110_C_DT02;        //  2자리  : 자료구분  
                    sData += DV110_C.DV110_C_DT03;        //  1자리  : 기구분    
                    sData += DV110_C.DV110_C_DT04;        //  1자리  : 신고구분  
                    sData += DV110_C.DV110_C_DT05;        //  3자리  : 세무서    
                    sData += DV110_C.DV110_C_DT06;        //  6자리  : 일련번호  
                    sData += DV110_C.DV110_C_DT07;        // 10자리  : 제출자 사업자등록번호
                    sData += DV110_C.DV110_C_DT08;        //  4자리  : 귀속년도
                    sData += DV110_C.DV110_C_DT09;        //  8자리  : 거래기간시작년월일 
                    sData += DV110_C.DV110_C_DT10;        //  8자리  : 거래기간종료년월일 
                    sData += DV110_C.DV110_C_DT11;        //  8자리  : 작성일자

                    // 합계분
                    sData += string.Format("{0:D6}", Convert.ToInt64(DV110_C.DV110_C_DT12));        // 거래처수    :   6자리
                    sData += string.Format("{0:D6}", Convert.ToInt64(DV110_C.DV110_C_DT13));        // 계산서 매수 :   6자리
                    if (Convert.ToInt64(DV110_C.DV110_C_DT15) < 0)
                    {
                        string sTempAmt = string.Format("{0:D14}", Convert.ToInt64(DV110_C.DV110_C_DT15) * -1);
                        DV110_C.DV110_C_DT14 = "1";      //  1자리 : 음수금액 표시
                        DV110_C.DV110_C_DT15 = sStrTemp; // 14자리 : 매입금액
                    }
                    else
                    {
                        DV110_C.DV110_C_DT14 = "0";      // 1자리 : 음수금액 표시
                        DV110_C.DV110_C_DT15 = string.Format("{0:D14}", Convert.ToInt64(DV110_C.DV110_C_DT15));  // 14자리 : 매입금액
                    }
                    sData += DV110_C.DV110_C_DT14;      //  1자리 : 음수금액 표시
                    sData += string.Format("{0:D14}", Convert.ToInt64(DV110_C.DV110_C_DT15));       // 14자리 : 매입금액
                    sData += DV110_C.DV110_C_DT16;      //  151자리:  공란

                    sw.WriteLine(sData); // C RECORD 저장(집계)

                    // -----------------------------------------------------------------------------------------  //
                    // ------------------------   D 레코드 거래명세서 생성 처리  ---------------------------------  //
                    // -----------------------------------------------------------------------------------------  //

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        iCnt = iCnt + 1;

                        DV110_D.DV110_D_DT01 = "D";                           // 1자리 : 레코드구분
                        DV110_D.DV110_D_DT02 = "18";                          // 2자리 : 자료구분
                        DV110_D.DV110_D_DT03 = getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1); // 1자리 : 기구분    (1년에 한번이면 2 )
                        DV110_D.DV110_D_DT04 = getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2); // 1자리 : 신고구분  (1년에 한번이면 2 )
                        if (this.CBO01_VNGUBUN.GetValue().ToString() == "1") //본점
                        {
                            DV110_D.DV110_D_DT05 = sTAXAREA;                      // 3자리 : 세무서 코드
                            DV110_D.DV110_D_DT06 = string.Format("{0:D6}", iCnt); // 6자리 : 일련번호
                            DV110_D.DV110_D_DT07 = sSAUPNO;                       // 10자리 : 제출자 사업자등록번호
                        }
                        else
                        {
                            DV110_D.DV110_D_DT05 = sTAXAREA;                      // 3자리 : 세무서 코드
                            DV110_D.DV110_D_DT06 = string.Format("{0:D6}", iCnt); // 6자리 : 일련번호
                            DV110_D.DV110_D_DT07 = sSAUPNO;                       // 10자리 : 제출자 사업자등록번호
                        }

                        sStrTemp = "";
                        sStrTemp += ds.Tables[0].Rows[i]["S2SAUPNO"].ToString();  // 10자리 : 매입처 사업자등록번호
                        sStrTemp += new String(Convert.ToChar(" "), (10 - Encoding.Default.GetByteCount(ds.Tables[0].Rows[i]["S2SAUPNO"].ToString().Trim())));
                        DV110_D.DV110_D_DT08 = sStrTemp;

                        sStrTemp = " ";
                        sStrTemp += ds.Tables[0].Rows[i]["VNSANGHO"].ToString();  // 40자리 : 매입처 상호
                        sStrTemp += new String(Convert.ToChar(" "), (39 - Encoding.Default.GetByteCount(ds.Tables[0].Rows[i]["VNSANGHO"].ToString().Trim())));
                        DV110_D.DV110_D_DT09 = sStrTemp;

                        DV110_D.DV110_D_DT10 = string.Format("{0:D5}", Convert.ToInt32(ds.Tables[0].Rows[i]["MAESU_CNT"].ToString().Trim())); // 5자리 : 계산서 매수

                        if (Convert.ToInt64(ds.Tables[0].Rows[i]["HAP_AMT"].ToString().Trim()) < 0)
                        {
                            string sTempAmt = string.Format("{0:D14}", Convert.ToInt64(ds.Tables[0].Rows[i]["HAP_AMT"].ToString().Trim()) * -1);
                            DV110_D.DV110_D_DT11 = "1";      //  1자리 : 음수금액 표시
                            DV110_D.DV110_D_DT12 = sStrTemp; // 14자리 : 매입금액
                        }
                        else
                        {
                            DV110_D.DV110_D_DT11 = "0";  //  1자리 : 음수금액 표시
                            DV110_D.DV110_D_DT12 = string.Format("{0:D14}", Convert.ToInt64(ds.Tables[0].Rows[i]["HAP_AMT"].ToString().Trim()));  // 14자리 : 매입금액
                        }
                        DV110_D.DV110_D_DT13 = sFill.PadRight(136); // 136 자리 공란

                        sData = DV110_D.DV110_D_DT01;
                        sData += DV110_D.DV110_D_DT02;
                        sData += DV110_D.DV110_D_DT03;
                        sData += DV110_D.DV110_D_DT04;
                        sData += DV110_D.DV110_D_DT05;
                        sData += DV110_D.DV110_D_DT06;
                        sData += DV110_D.DV110_D_DT07;
                        sData += DV110_D.DV110_D_DT08;
                        sData += DV110_D.DV110_D_DT09;
                        sData += DV110_D.DV110_D_DT10;
                        sData += DV110_D.DV110_D_DT11;
                        sData += DV110_D.DV110_D_DT12;
                        sData += DV110_D.DV110_D_DT13;

                        sw.WriteLine(sData);  // D RECORD 저장(거래명세서)

                    } //for..end

                } // End .. 전자계산서 이외분 매입처별

                // -------------------------------------------------------------------------------------------------- //
                //                                 전자계산서분 매입 합계 처리 (5)  (IN)                               //
                // -------------------------------------------------------------------------------------------------- //

                ds.Clear();
                ds_110.Clear();

                sTAXCDGN_HAP = "79"; // 매입(전자신고)

                this.DbConnector.CommandClear(); // AVTAXDETAF (내역)
                this.DbConnector.Attach("TY_P_AC_42327179",
                                        this.DTP01_ELXYYMM.GetValue().ToString(),    // 년도
                                        this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2), // 확정구분(1.예정, 2.확정)
                                        "1",                                         // 계산서구분(1.매입, 2.매출)
                                        sTAXCDGN_HAP
                                        );
                ds = this.DbConnector.ExecuteDataSet();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.DbConnector.CommandClear(); // AVTAXDETAF  (집계)
                    this.DbConnector.Attach("TY_P_AC_4236D180",
                                            this.DTP01_ELXYYMM.GetValue().ToString(),    // 년도
                                            this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                                            getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                                            getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2), // 확정구분(1.예정, 2.확정)
                                            "1",                                         // 계산서구분(1.매입, 2.매출)
                                            sTAXCDGN_HAP
                                            );
                    ds_110 = this.DbConnector.ExecuteDataSet();

                    // ------------------------   E 레코드 생성 처리(매입계산서 집계)   ---------------------------------  //
                    DV110_E.DV110_E_DT01 = "E";                        //  1자리 : 레코드구분
                    DV110_E.DV110_E_DT02 = "18";                       //  2자리 : 자료구분
                    DV110_E.DV110_E_DT03 = getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1);  // 1자리 : 기구분 (1년에 한번이면 2 )
                    DV110_E.DV110_E_DT04 = getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2); // 1자리 : 신고구분 (1년에 한번이면 2 )
                    if (this.CBO01_VNGUBUN.GetValue().ToString() == "1") //본점
                    {
                        DV110_E.DV110_E_DT05 = sTAXAREA;              //  3자리 : 세무서 코드
                        DV110_E.DV110_E_DT06 = "000001";              //  6자리 : 일련번호
                        DV110_E.DV110_E_DT07 = sSAUPNO;               // 10자리 : 제출자 사업자등록번호
                    }
                    else
                    {
                        DV110_E.DV110_E_DT05 = sTAXAREA;              //  3자리 : 세무서 코드
                        DV110_E.DV110_E_DT06 = "000001";              //  6자리 : 일련번호
                        DV110_E.DV110_E_DT07 = sSAUPNO;               // 10자리 : 제출자 사업자등록번호
                    }

                    DV110_E.DV110_E_DT08 = this.DTP01_ELXYYMM.GetValue().ToString().Substring(0, 4);  // 4자리 : 귀속년도  
                    DV110_E.DV110_E_DT09 = s매입시작년월일;                               // 8자리 : 거래시작일 (확인요망)
                    DV110_E.DV110_E_DT10 = s매입종료년월일;                               // 8자리 : 거래종료일 (확인요망)
                    DV110_E.DV110_E_DT11 = s매입종료년월일;                               // 8자리 : 작성일자

                    DV110_E.DV110_E_DT12 = ds_110.Tables[0].Rows[0]["DT_CNT"].ToString().Trim(); // 건수
                    DV110_E.DV110_E_DT13 = ds_110.Tables[0].Rows[0]["DT_MCNT"].ToString().Trim(); // 매수
                    DV110_E.DV110_E_DT15 = ds_110.Tables[0].Rows[0]["DT_AMT"].ToString().Trim(); // 매출금액
                    DV110_E.DV110_E_DT16 = sFill.PadRight(151); // 151 자리 공란

                    sData = DV110_E.DV110_E_DT01;         //  1자리  : 레코드구분
                    sData += DV110_E.DV110_E_DT02;        //  2자리  : 자료구분  
                    sData += DV110_E.DV110_E_DT03;        //  1자리  : 기구분    
                    sData += DV110_E.DV110_E_DT04;        //  1자리  : 신고구분  
                    sData += DV110_E.DV110_E_DT05;        //  3자리  : 세무서    
                    sData += DV110_E.DV110_E_DT06;        //  6자리  : 일련번호  
                    sData += DV110_E.DV110_E_DT07;        // 10자리  :  제출자 사업자등록번호
                    sData += DV110_E.DV110_E_DT08;        //  4자리  : 귀속년도
                    sData += DV110_E.DV110_E_DT09;        //  8자리  : 거래기간시작년월일 
                    sData += DV110_E.DV110_E_DT10;        //  8자리  : 거래기간종료년월일 
                    sData += DV110_E.DV110_E_DT11;        //  8자리  : 작성일자

                    // 합계분
                    sData += string.Format("{0:D6}", Convert.ToInt64(DV110_E.DV110_E_DT12));        // 6자리 : 거래처수     
                    sData += string.Format("{0:D6}", Convert.ToInt64(DV110_E.DV110_E_DT13));        // 6자리 : 계산서 매수  
                    if (Convert.ToInt64(DV110_E.DV110_E_DT15) < 0)
                    {
                        string sTempAmt = string.Format("{0:D14}", Convert.ToInt64(DV110_E.DV110_E_DT15) * -1);
                        DV110_E.DV110_E_DT14 = "1";      //  1자리 : 음수금액 표시
                        DV110_E.DV110_E_DT15 = sStrTemp; // 14자리 : 매입금액
                    }
                    else
                    {
                        DV110_E.DV110_E_DT14 = "0";  //  1자리 : 음수금액 표시
                        DV110_E.DV110_E_DT15 = string.Format("{0:D14}", Convert.ToInt64(DV110_E.DV110_E_DT15));  // 14자리 : 매입금액
                    }
                    sData += DV110_E.DV110_E_DT14;        //   1자리 : 음수금액 표시
                    sData += string.Format("{0:D14}", Convert.ToInt64(DV110_E.DV110_E_DT15));       // 14자리 : 매입금액
                    sData += DV110_E.DV110_E_DT16;        // 151자리 : 공백

                    sw.WriteLine(sData); // E RECOED 저장(집계)

                } // End .. 전자계산서분 매입 합계
                
                #endregion

            } // End ... If (매입계산서 관련 기초 자료가 존재 할경우 만 실행)

        }
        #endregion

        #region Description : 수출실적 명세서 레코드 생성 (V141) - [Multi-Key]  -- UP_TAX_Create_V141()
        private void UP_TAX_Create_V141(StreamWriter sw)
        {
            string sStrTemp = string.Empty;
            string sData = string.Empty;
            string sFill = string.Empty;

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

            // 제출자 인적사항 조회 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_42B67344",  // AVSUBMITMF 
                this.DTP01_ELXYYMM.GetValue().ToString(),
                getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  //신고구분(1기, 2기)
                getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2),  // 확정구분(1.예정, 2.확정)
                this.CBO01_VNGUBUN.GetValue().ToString()   // 사업장(1본점, 2지점)
                );

            DataTable dt_tae = this.DbConnector.ExecuteDataTable();

            if (dt_tae.Rows.Count > 0)
            {
                sSAUPNO = dt_tae.Rows[0]["ASMSAUPNO"].ToString(); // 사업자등록번호(10자리)
                sSANGHO = dt_tae.Rows[0]["ASMSANGHO"].ToString(); // 상호명
                sNAMENM = dt_tae.Rows[0]["ASMNAMENM"].ToString(); // 대표자이름
                sCORPNO = dt_tae.Rows[0]["ASMCORPNO"].ToString(); // 법인번호 (13자리)
                sUPTAE = dt_tae.Rows[0]["ASMUPTAE"].ToString(); // 업태
                sEVENT = dt_tae.Rows[0]["ASMEVENT"].ToString(); // 종목
                sTELNUM = Up_Telnum_Convert(dt_tae.Rows[0]["ASMTELNUM"].ToString().Trim()); // 전화번호
                sVNADDRS = dt_tae.Rows[0]["ASMVNADDRS"].ToString(); // 사업장주소
                sTAXAREA = dt_tae.Rows[0]["ASMTAXAREA"].ToString(); // 관할세무소(3자리)
                sBUSTYPE = dt_tae.Rows[0]["ASMBUSTYPE"].ToString(); // 업종코드 (7자리)
            }

            struct_DV141_A V141_A = new struct_DV141_A();

            string s시작년월일 = string.Empty;
            string s종료년월일 = string.Empty;
            string sYEAR = this.DTP01_ELXYYMM.GetValue().ToString().Substring(0, 4);

            if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1) == "1") // 신고구분(1기, 2기)
            {
                if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2) == "1") // 확정구분(1.예정, 2.확정)
                {
                    s시작년월일 = sYEAR + "0101";
                    s종료년월일 = sYEAR + "0331";
                }
                else
                {
                    s시작년월일 = sYEAR + "0401";
                    s종료년월일 = sYEAR + "0630";
                };
            }
            else
            {
                if (getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2) == "1")
                {
                    s시작년월일 = sYEAR + "0701";
                    s종료년월일 = sYEAR + "0930";
                }
                else
                {
                    s시작년월일 = sYEAR + "1001";
                    s종료년월일 = sYEAR + "1231";
                };
            }

            Int32 iCnt = 0;

            // 헤더 작성
            if (this.CBO01_VNGUBUN.GetValue().ToString() == "1") //본점
            {
                V141_A.DV141_A_DT01 = "A";                 // 1자리
                V141_A.DV141_A_DT02 = s종료년월일.Substring(0, 6);  // 6자리 : 수출신고년월(귀속년월)
                V141_A.DV141_A_DT03 = "3";                // 1자리 : 신고구분
                V141_A.DV141_A_DT04 = sSAUPNO;            // 10자리 : 사업자등록번호
                V141_A.DV141_A_DT05 = sSANGHO;            // 30자리 : 상호명 (재정리)
                V141_A.DV141_A_DT06 = sNAMENM;            // 15자리 : 대표자이름 (재정리)
                V141_A.DV141_A_DT07 = sVNADDRS;           // 45자리 : 사업장주소 (재정리)
                V141_A.DV141_A_DT08 = sUPTAE;             // 17자리 : 업태 (재정리)
                V141_A.DV141_A_DT09 = sEVENT;             // 25자리 : 종목 (재정리)
                V141_A.DV141_A_DT10 = s시작년월일.Substring(0, 8) + s종료년월일.Substring(0, 8);  // 16자리(거래기간)
                V141_A.DV141_A_DT11 = s종료년월일.Substring(0, 8);  // 8자리
                V141_A.DV141_A_DT12 = sFill.PadRight(06); // 6자리
            }
            else //지점
            {
                V141_A.DV141_A_DT01 = "A";                 // 1자리
                V141_A.DV141_A_DT02 = s종료년월일.Substring(0, 6);  // 6자리 : 수출신고년월(귀속년월)
                V141_A.DV141_A_DT03 = "3";                //  1자리 : 신고구분
                V141_A.DV141_A_DT04 = sSAUPNO;            // 10자리 : 사업자등록번호
                V141_A.DV141_A_DT05 = sSANGHO;            // 30자리 : 상호명 (재정리)
                V141_A.DV141_A_DT06 = sNAMENM;            // 15자리 : 대표자이름 (재정리)
                V141_A.DV141_A_DT07 = "서울시 영등포구 여의공원로 111";           // 45자리 : 사업장주소 (재정리)
                V141_A.DV141_A_DT08 = sUPTAE;             // 17자리 : 업태 (재정리)
                V141_A.DV141_A_DT09 = sEVENT;             // 25자리 : 종목 (재정리)
                V141_A.DV141_A_DT10 = s시작년월일.Substring(0, 8) + s종료년월일.Substring(0, 8);  // 16자리(거래기간)
                V141_A.DV141_A_DT11 = s종료년월일.Substring(0, 8);  // 8자리
                V141_A.DV141_A_DT12 = sFill.PadRight(06); // 6자리
            }

            sData = V141_A.DV141_A_DT01;

            sStrTemp = V141_A.DV141_A_DT02.Trim(); // 6자리 : 수출신고년월(귀속년월) -종료년월수록
            sStrTemp += new String(Convert.ToChar(" "), (6 - Encoding.Default.GetByteCount(V141_A.DV141_A_DT02.Trim())));
            sData += sStrTemp;

            sData += V141_A.DV141_A_DT03;
            sData += V141_A.DV141_A_DT04;

            sStrTemp = V141_A.DV141_A_DT05.Trim();   // 30자리 : 상호명 (재정리)
            sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(V141_A.DV141_A_DT05.Trim())));
            sData += sStrTemp;

            sStrTemp = V141_A.DV141_A_DT06.Trim();    // 15자리 : 대표자이름 (재정리)
            sStrTemp += new String(Convert.ToChar(" "), (15 - Encoding.Default.GetByteCount(V141_A.DV141_A_DT06.Trim())));
            sData += sStrTemp;

            sStrTemp = V141_A.DV141_A_DT07.Trim();    // 45자리 : 사업장주소 (재정리)
            sStrTemp += new String(Convert.ToChar(" "), (45 - Encoding.Default.GetByteCount(V141_A.DV141_A_DT07.Trim())));
            sData += sStrTemp;

            sStrTemp = V141_A.DV141_A_DT08.Trim();    // 17자리 : 업태 (재정리)
            sStrTemp += new String(Convert.ToChar(" "), (17 - Encoding.Default.GetByteCount(V141_A.DV141_A_DT08.Trim())));
            sData += sStrTemp;

            sStrTemp = V141_A.DV141_A_DT09.Trim();    // 25자리 : 종목 (재정리)
            sStrTemp += new String(Convert.ToChar(" "), (25 - Encoding.Default.GetByteCount(V141_A.DV141_A_DT09.Trim())));
            sData += sStrTemp;

            sData += V141_A.DV141_A_DT10;
            sData += V141_A.DV141_A_DT11;
            sData += V141_A.DV141_A_DT12;


            // 수출실적 명세서 (집계)

            struct_DV141_B V141_B = new struct_DV141_B();

            this.DbConnector.CommandClear(); // AVEXPORTMF 
            this.DbConnector.Attach("TY_P_AC_41R2Y163",
                                    this.DTP01_ELXYYMM.GetValue().ToString(),    // 년월
                                    this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)   // 확정구분(1.예정, 2.확정)
                                    );
            DataTable dt_141_b = this.DbConnector.ExecuteDataTable();

            if ( Convert.ToInt64(dt_141_b.Rows[0]["CNT00"].ToString()) > 0 ) // 전체 건수가 없을경우 생성 안함
            {
                sw.WriteLine(sData);   // ---------- [[ 수출실적 헤더 RECORD 저장 (전체 건수가 없을경우엔 HEAD 화일도 생성하지 않음 ]]---------------


                V141_B.DV141_B_DT01 = dt_141_b.Rows[0]["DTGB"].ToString(); // 1자리 (문자) : 자료구분
                V141_B.DV141_B_DT02 = s종료년월일.Substring(0, 6); // 6자리 (문자) : 수출신고년월(귀속년월)
                V141_B.DV141_B_DT03 = "3"; // 1자리 (문자) : 신고구분

                if (this.CBO01_VNGUBUN.GetValue().ToString() == "1") //본점
                {
                    V141_B.DV141_B_DT04 = sSAUPNO; // 10자리 (문자) : 사업자등록번호
                }
                else
                {
                    V141_B.DV141_B_DT04 = sSAUPNO; // 10자리 (문자) : 사업자등록번호
                };

                // 합계 //
                // ---------------------------------------------------------------------------------------------------------------- //
                V141_B.DV141_B_DT05 = string.Format("{0:D7}", Convert.ToInt64(dt_141_b.Rows[0]["CNT00"].ToString()));    // 7자리  건수합계_수출
                // 외화
                if (Convert.ToDouble(dt_141_b.Rows[0]["AMT00"].ToString().Trim()) < 0)  // 15.2자리 Multi-Key
                {
                    string sTempAmt = string.Format("{0:D15}", Convert.ToInt64((Convert.ToDouble(dt_141_b.Rows[0]["AMT00"].ToString().Trim()) * -1) * 100));
                    string sALPHAValue = UP_Minus_ALPHA(sTempAmt.Substring(14, 1));
                    V141_B.DV141_B_DT06 = sTempAmt.Substring(0, 14) + sALPHAValue;
                }
                else
                {
                    V141_B.DV141_B_DT06 = string.Format("{0:D15}", Convert.ToInt64(Convert.ToDouble(dt_141_b.Rows[0]["AMT00"].ToString().Trim()) * 100));
                };
                // 원화
                if (Convert.ToInt64(dt_141_b.Rows[0]["HAP00"].ToString().Trim()) < 0)  // 15자리   Multi-Key
                {
                    string sTempAmt = string.Format("{0:D15}", Convert.ToInt64(dt_141_b.Rows[0]["HAP00"].ToString().Trim()) * -1);
                    string sALPHAValue = UP_Minus_ALPHA(sTempAmt.Substring(14, 1));
                    V141_B.DV141_B_DT07 = sTempAmt.Substring(0, 14) + sALPHAValue;
                }
                else
                {
                    V141_B.DV141_B_DT07 = string.Format("{0:D15}", Convert.ToInt64(dt_141_b.Rows[0]["HAP00"].ToString().Trim()));
                };


                // ------ [ 재화 ] -----
                // ---------------------------------------------------------------------------------------------------------------- //

                V141_B.DV141_B_DT08 = string.Format("{0:D7}", Convert.ToInt64(dt_141_b.Rows[0]["CNT01"].ToString()));    // 7자리  건수_재화
                // 외화
                if (Convert.ToDouble(dt_141_b.Rows[0]["AMT01"].ToString().Trim()) < 0)  // 15.2자리 Multi-Key
                {
                    string sTempAmt = string.Format("{0:D15}", Convert.ToInt64((Convert.ToDouble(dt_141_b.Rows[0]["AMT01"].ToString().Trim()) * -1) * 100));
                    string sALPHAValue = UP_Minus_ALPHA(sTempAmt.Substring(14, 1));
                    V141_B.DV141_B_DT09 = sTempAmt.Substring(0, 14) + sALPHAValue;
                }
                else
                {
                    V141_B.DV141_B_DT09 = string.Format("{0:D15}", Convert.ToInt64(Convert.ToDouble(dt_141_b.Rows[0]["AMT01"].ToString().Trim()) * 100));
                };

                // 원화
                if (Convert.ToInt64(dt_141_b.Rows[0]["HAP00"].ToString().Trim()) < 0)  // 15자리   Multi-Key
                {
                    string sTempAmt = string.Format("{0:D15}", Convert.ToInt64(dt_141_b.Rows[0]["HAP01"].ToString().Trim()) * -1);
                    string sALPHAValue = UP_Minus_ALPHA(sTempAmt.Substring(14, 1));
                    V141_B.DV141_B_DT10 = sTempAmt.Substring(0, 14) + sALPHAValue;
                }
                else
                {
                    V141_B.DV141_B_DT10 = string.Format("{0:D15}", Convert.ToInt64(dt_141_b.Rows[0]["HAP01"].ToString().Trim()));
                };

                // 기타
                // ---------------------------------------------------------------------------------------------------------------- //
                V141_B.DV141_B_DT11 = string.Format("{0:D7}", Convert.ToInt64(dt_141_b.Rows[0]["CNT02"].ToString()));    // 7자리  건수_기타
                // 외화
                if (Convert.ToDouble(dt_141_b.Rows[0]["AMT02"].ToString().Trim()) < 0)  // 15.2자리 Multi-Key
                {
                    string sTempAmt = string.Format("{0:D15}", Convert.ToInt64((Convert.ToDouble(dt_141_b.Rows[0]["AMT02"].ToString().Trim()) * -1) * 100));
                    string sALPHAValue = UP_Minus_ALPHA(sTempAmt.Substring(14, 1));
                    V141_B.DV141_B_DT12 = sTempAmt.Substring(0, 14) + sALPHAValue;
                }
                else
                {
                    V141_B.DV141_B_DT12 = string.Format("{0:D15}", Convert.ToInt64(Convert.ToDouble(dt_141_b.Rows[0]["AMT02"].ToString().Trim()) * 100));
                };

                // 원화
                if (Convert.ToInt64(dt_141_b.Rows[0]["HAP00"].ToString().Trim()) < 0)  // 15자리   Multi-Key
                {
                    string sTempAmt = string.Format("{0:D15}", Convert.ToInt64(dt_141_b.Rows[0]["HAP02"].ToString().Trim()) * -1);
                    string sALPHAValue = UP_Minus_ALPHA(sTempAmt.Substring(14, 1));
                    V141_B.DV141_B_DT13 = sTempAmt.Substring(0, 14) + sALPHAValue;
                }
                else
                {
                    V141_B.DV141_B_DT13 = string.Format("{0:D15}", Convert.ToInt64(dt_141_b.Rows[0]["HAP02"].ToString().Trim()));
                };

                // ---------------------------------------------------------------------------------------------------------------- //
                V141_B.DV141_B_DT14 = sFill.PadRight(51); // 51자리 공백

                // 레코드 세팅 작업(자리수)
                sData = V141_B.DV141_B_DT01;
                sData += V141_B.DV141_B_DT02;
                sData += V141_B.DV141_B_DT03;
                sData += V141_B.DV141_B_DT04;
                sData += V141_B.DV141_B_DT05;   // 7자리  건수합계_수출
                sData += V141_B.DV141_B_DT06;   // 15.2자리
                sData += V141_B.DV141_B_DT07;   // 15자리
                sData += V141_B.DV141_B_DT08;   // 7자리  건수_재화
                sData += V141_B.DV141_B_DT09;   // 15.2자리
                sData += V141_B.DV141_B_DT10;   // 15자리
                sData += V141_B.DV141_B_DT11;   // 7자리  건수_기타
                sData += V141_B.DV141_B_DT12;   // 15.2자리
                sData += V141_B.DV141_B_DT13;   // 15자리
                sData += V141_B.DV141_B_DT14;   // 51자리 공백

                sw.WriteLine(sData);   // 수출실적 명세서 (집계) RECORD 저장

            }; // End ... If (Convert.ToInt64(dt_141_b.Rows[0]["CNT00"].ToString()) > 0) 


            // ----------------------------------------------------------------------- //
            //                 전산매체 수출실적 명세서 상세생성                         // 
            // --------------------------     내         역     ---------------------- //

            // 수출 내역이 존재 하지 않은 경우엔 내역을 생성 하지 아니함
            if (Convert.ToDouble(V141_B.DV141_B_DT08) > 0)
            {
                struct_DV141_C V141_C = new struct_DV141_C();

                this.DbConnector.CommandClear(); // AVEXPORTMF 
                this.DbConnector.Attach("TY_P_AC_41R2Z165",
                                        this.DTP01_ELXYYMM.GetValue().ToString(),    // 년월
                                        this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                                        getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)   // 확정구분(1.예정, 2.확정)
                                        );
                DataTable dt_141_c = this.DbConnector.ExecuteDataTable();

                if (dt_141_c.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_141_c.Rows.Count; i++)
                    {
                        iCnt = iCnt + 1;

                        V141_C.DV141_C_DT01 = dt_141_c.Rows[i]["DTGB"].ToString(); // 2자리 (문자) : 자료구분

                        V141_C.DV141_C_DT02 = s종료년월일.Substring(0, 6); // 6자리 (문자) : 서식코드
                        V141_C.DV141_C_DT03 = "3"; // 1자리 (문자) : 신고구분

                        if (this.CBO01_VNGUBUN.GetValue().ToString() == "1") //본점
                        {
                            V141_C.DV141_C_DT04 = sSAUPNO; // 10자리 (문자) : 사업자등록번호
                        }
                        else
                        {
                            V141_C.DV141_C_DT04 = sSAUPNO; // 10자리 (문자) : 사업자등록번호
                        };

                        V141_C.DV141_C_DT05 = string.Format("{0:D7}", Convert.ToInt64(dt_141_c.Rows[i]["SEQNUM"].ToString())); // 7자리 (문자) : 수출일련번호
                        V141_C.DV141_C_DT06 = dt_141_c.Rows[i]["S7SINNO"].ToString().Trim();    // 15자리 수출신고번호
                        V141_C.DV141_C_DT07 = dt_141_c.Rows[i]["S7SHIPDT"].ToString().Trim();    // 8자리 선적(기)일자
                        V141_C.DV141_C_DT08 = dt_141_c.Rows[i]["S7CURRCD"].ToString().Trim();    // 3자리 수출통화코드
                        V141_C.DV141_C_DT09 = string.Format("{0:D9}", Convert.ToInt64(dt_141_c.Rows[i]["CONV_EXCHAN"].ToString().Trim()));  // 9.4 자리 환    율

                        //  외화금액
                        if (Convert.ToDouble(dt_141_c.Rows[i]["S7FORGIAMT"].ToString().Trim()) < 0)  // 15.2자리   Multi-Key 외화금액
                        {
                            string sTempAmt = string.Format("{0:D15}", Convert.ToInt64((Convert.ToDouble(dt_141_c.Rows[i]["S7FORGIAMT"].ToString().Trim()) * -1) * 100));
                            string sALPHAValue = UP_Minus_ALPHA(sTempAmt.Substring(14, 1));
                            V141_C.DV141_C_DT10 = sTempAmt.Substring(0, 14) + sALPHAValue;
                        }
                        else
                        {
                            V141_C.DV141_C_DT10 = string.Format("{0:D15}", Convert.ToInt64(Convert.ToDouble(dt_141_c.Rows[i]["S7FORGIAMT"].ToString().Trim()) * 100));
                        };

                        // 원화금액
                        if (Convert.ToInt64(dt_141_c.Rows[i]["S7WONHAAMT"].ToString().Trim()) < 0)  // 15자리   Multi-Key 원화금액
                        {
                            string sTempAmt = string.Format("{0:D15}", Convert.ToInt64(dt_141_c.Rows[i]["S7WONHAAMT"].ToString().Trim()) * -1);
                            string sALPHAValue = UP_Minus_ALPHA(sTempAmt.Substring(14, 1));
                            V141_C.DV141_C_DT11 = sTempAmt.Substring(0, 14) + sALPHAValue;
                        }
                        else
                        {
                            V141_C.DV141_C_DT11 = string.Format("{0:D15}", Convert.ToInt64(dt_141_c.Rows[i]["S7WONHAAMT"].ToString().Trim()));
                        };

                        V141_C.DV141_C_DT12 = sFill.PadRight(90); // 90자리 공백

                        // 레코드 세팅 작업(자리수)
                        sData = V141_C.DV141_C_DT01;
                        sData += V141_C.DV141_C_DT02;
                        sData += V141_C.DV141_C_DT03;
                        sData += V141_C.DV141_C_DT04;
                        sData += V141_C.DV141_C_DT05;

                        sStrTemp = V141_C.DV141_C_DT06.Trim(); // 15자리 수출신고번호
                        sStrTemp += new String(Convert.ToChar(" "), (15 - Encoding.Default.GetByteCount(V141_C.DV141_C_DT06.Trim())));
                        sData += sStrTemp;

                        sStrTemp = V141_C.DV141_C_DT07.Trim(); // 8자리 선적일자
                        sStrTemp += new String(Convert.ToChar(" "), (8 - Encoding.Default.GetByteCount(V141_C.DV141_C_DT07.Trim())));
                        sData += sStrTemp;

                        sStrTemp = V141_C.DV141_C_DT08.Trim(); // 3자리 수출통화코드 
                        sStrTemp += new String(Convert.ToChar(" "), (3 - Encoding.Default.GetByteCount(V141_C.DV141_C_DT08.Trim())));
                        sData += sStrTemp;

                        sData += V141_C.DV141_C_DT09;
                        sData += V141_C.DV141_C_DT10;
                        sData += V141_C.DV141_C_DT11;
                        sData += V141_C.DV141_C_DT12;

                        sw.WriteLine(sData);

                    }; // End .. For
                };
            } // End .. if ( Convert.ToDouble(V141_B.DV141_B_DT08) > 0 )
        }
        #endregion

        #region Description : 건물등 감가상각자산 취득명세서 레코드 생성 (V149) -- UP_TAX_Create_V149()
        private void UP_TAX_Create_V149(StreamWriter sw)
        {
            string sStrTemp = string.Empty;
            string sData = string.Empty;
            string sFill = string.Empty;

            struct_DV149 V149 = new struct_DV149();

            // 건물등 감가상각자산 취득명세서
            this.DbConnector.CommandClear(); // AVASSETMF 
            this.DbConnector.Attach("TY_P_AC_41OBJ151",
                                    this.DTP01_ELXYYMM.GetValue().ToString(),    // 년월
                                    this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)   // 확정구분(1.예정, 2.확정)
                                    );
            DataTable dt_149 = this.DbConnector.ExecuteDataTable();

            if (dt_149.Rows.Count > 0)
            {
                if (Convert.ToDouble(dt_149.Rows[0]["CNT00"].ToString().Trim()) > 0) // 건수가 없어면 처리 안함
                {
                    V149.DV149_DT01 = dt_149.Rows[0]["DTGB"].ToString(); // 2자리 (문자) : 자료구분  --> (17)
                    //V149.DV149_DT02 = dt_149.Rows[0]["DOCGB"].ToString(); // 4자리 (문자) : 서식코드 --> (V149)

                    V149.DV149_DT02 = dt_149.Rows[0]["DOCGB"].ToString(); // 7자리 (문자) : 서식코드 --> (I103800)

                    // 전체 - 고정자산 //
                    V149.DV149_DT03 = dt_149.Rows[0]["CNT00"].ToString().Trim();    // 11자리
                    V149.DV149_DT04 = UP_Minus_Conv_Fill(dt_149.Rows[0]["AMT00"].ToString().Trim(), 13);    // 13자리
                    V149.DV149_DT05 = UP_Minus_Conv_Fill(dt_149.Rows[0]["HAP00"].ToString().Trim(), 13);    // 13자리
                    // 건물 //
                    V149.DV149_DT06 = dt_149.Rows[0]["CNT01"].ToString().Trim();    // 11자리
                    V149.DV149_DT07 = UP_Minus_Conv_Fill(dt_149.Rows[0]["AMT01"].ToString().Trim(), 13);    // 13자리
                    V149.DV149_DT08 = UP_Minus_Conv_Fill(dt_149.Rows[0]["HAP01"].ToString().Trim(), 13);    // 13자리
                    // 기계장치 //
                    V149.DV149_DT09 = dt_149.Rows[0]["CNT02"].ToString().Trim();    // 11자리
                    V149.DV149_DT10 = UP_Minus_Conv_Fill(dt_149.Rows[0]["AMT02"].ToString().Trim(), 13);    // 13자리
                    V149.DV149_DT11 = UP_Minus_Conv_Fill(dt_149.Rows[0]["HAP02"].ToString().Trim(), 13);    // 13자리
                    // 차량운반구 //
                    V149.DV149_DT12 = dt_149.Rows[0]["CNT03"].ToString().Trim();    // 11자리
                    V149.DV149_DT13 = UP_Minus_Conv_Fill(dt_149.Rows[0]["AMT03"].ToString().Trim(), 13);    // 13자리
                    V149.DV149_DT14 = UP_Minus_Conv_Fill(dt_149.Rows[0]["HAP03"].ToString().Trim(), 13);    // 13자리
                    // 기타감가상각 //
                    V149.DV149_DT15 = dt_149.Rows[0]["CNT04"].ToString().Trim();    // 11자리
                    V149.DV149_DT16 = UP_Minus_Conv_Fill(dt_149.Rows[0]["AMT04"].ToString().Trim(), 13);    // 13자리
                    V149.DV149_DT17 = UP_Minus_Conv_Fill(dt_149.Rows[0]["HAP04"].ToString().Trim(), 13);    // 13자리
                    //V149.DV149_DT18 = sFill.PadRight(09); // 9자리 공백
                    V149.DV149_DT18 = sFill.PadRight(06); // 9자리 공백

                    // 레코드 세팅 작업(자리수)
                    sData = V149.DV149_DT01;
                    sData += V149.DV149_DT02;

                    sData += string.Format("{0:D11}", Convert.ToInt64(V149.DV149_DT03));        // 건수_합계_고정자산 :   11자리
                    sData += V149.DV149_DT04;
                    sData += V149.DV149_DT05;

                    sData += string.Format("{0:D11}", Convert.ToInt64(V149.DV149_DT06));        // 건수_건물_구축물  :   11자리
                    sData += V149.DV149_DT07;
                    sData += V149.DV149_DT08;

                    sData += string.Format("{0:D11}", Convert.ToInt64(V149.DV149_DT09));        // 건수_기계장치  :   11자리
                    sData += V149.DV149_DT10;
                    sData += V149.DV149_DT11;

                    sData += string.Format("{0:D11}", Convert.ToInt64(V149.DV149_DT12));        // 건수_차량운반구  :   11자리
                    sData += V149.DV149_DT13;
                    sData += V149.DV149_DT14;

                    sData += string.Format("{0:D11}", Convert.ToInt64(V149.DV149_DT15));        // 건수_기타감가상각  :   11자리
                    sData += V149.DV149_DT16;
                    sData += V149.DV149_DT17;

                    sData += V149.DV149_DT18; // 9자리 공백

                    sw.WriteLine(sData);
                }

            };

            
        }
        #endregion

        #region Description : 영세율매출명세서 레코드 생성 (V177)  -- UP_TAX_Create_V177()
        private void UP_TAX_Create_V177(StreamWriter sw)
        {
            string sStrTemp = string.Empty;
            string sData = string.Empty;
            string sFill = string.Empty;

            struct_DV177 V177 = new struct_DV177();

            // 영세율매출명세서
            this.DbConnector.CommandClear(); // AVEXPORTMF , AVZEROTAXF 
            this.DbConnector.Attach("TY_P_AC_41S4H167",
                                    this.DTP01_ELXYYMM.GetValue().ToString(),    // 년월
                                    this.CBO01_VNGUBUN.GetValue().ToString(),    // 사업장(1본점, 2지점)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 1),  // 신고구분(1기, 2기)
                                    getCONFGB(this.CBO01_INQOPTION.GetValue().ToString(), 2)   // 확정구분(1.예정, 2.확정)
                                    );
            DataTable dt_177 = this.DbConnector.ExecuteDataTable();

            if (dt_177.Rows.Count > 0)
            {
                if (Convert.ToDouble(dt_177.Rows[6]["AMT"].ToString().Trim()) > 0) // 전체 총금액이 0 이면 처리 안함
                {
                    V177.DV177_DT01 = "17"; // 2자리 (문자) : 자료구분 --> (17)
                    //V177.DV177_DT02 = "V177"; // 4자리 (문자) : 서식코드--> (V177)
                    V177.DV177_DT02 = "I104000"; // 7자리 (문자) : 서식코드--> (I104000)                    
                    V177.DV177_DT03 = dt_177.Rows[0]["AMT"].ToString();
                    V177.DV177_DT04 = "0";
                    V177.DV177_DT05 = dt_177.Rows[1]["AMT"].ToString();
                    V177.DV177_DT06 = "0";
                    V177.DV177_DT07 = "0";
                    V177.DV177_DT08 = "0";
                    V177.DV177_DT09 = dt_177.Rows[2]["AMT"].ToString();
                    V177.DV177_DT10 = "0";
                    V177.DV177_DT11 = dt_177.Rows[3]["AMT"].ToString();
                    V177.DV177_DT12 = "0";
                    V177.DV177_DT13 = "0";
                    V177.DV177_DT14 = "0";
                    V177.DV177_DT15 = "0";
                    V177.DV177_DT16 = "0";
                    V177.DV177_DT17 = "0";
                    V177.DV177_DT18 = "0";
                    V177.DV177_DT19 = dt_177.Rows[5]["AMT"].ToString();
                    V177.DV177_DT20 = "0";
                    V177.DV177_DT21 = "0";
                    V177.DV177_DT22 = "0";
                    V177.DV177_DT23 = "0";
                    V177.DV177_DT24 = dt_177.Rows[4]["AMT"].ToString();
                    V177.DV177_DT25 = "0";
                    V177.DV177_DT26 = "0";
                    V177.DV177_DT27 = dt_177.Rows[4]["AMT"].ToString();
                    V177.DV177_DT28 = dt_177.Rows[6]["AMT"].ToString();
                    //V177.DV177_DT29 = sFill.PadRight(04); // 4자리 공백
                    //V177.DV177_DT29 = sFill.PadRight(01); // 4자리 공백
                    V177.DV177_DT29 = "0";
                    V177.DV177_DT30 = "0";
                    V177.DV177_DT31 = sFill.PadRight(21); // 21자리 공백

                    // 레코드 세팅 작업(자리수)
                    sData = V177.DV177_DT01;
                    sData += V177.DV177_DT02;

                    sData += string.Format("{0:D15}", Convert.ToInt64(V177.DV177_DT03));        // 15자리 직접수출
                    sData += string.Format("{0:D15}", Convert.ToInt64(V177.DV177_DT04));        // 15자리
                    sData += string.Format("{0:D15}", Convert.ToInt64(V177.DV177_DT05));        // 15자리
                    sData += string.Format("{0:D15}", Convert.ToInt64(V177.DV177_DT06));        // 15자리 내국신용구매확인
                    sData += string.Format("{0:D15}", Convert.ToInt64(V177.DV177_DT07));        // 15자리
                    sData += string.Format("{0:D15}", Convert.ToInt64(V177.DV177_DT08));        // 15자리
                    sData += string.Format("{0:D15}", Convert.ToInt64(V177.DV177_DT09));        // 15자리 선박항공기외국항행
                    sData += string.Format("{0:D15}", Convert.ToInt64(V177.DV177_DT10));        // 15자리
                    sData += string.Format("{0:D15}", Convert.ToInt64(V177.DV177_DT11));        // 15자리 국내비거주외국법인
                    sData += string.Format("{0:D15}", Convert.ToInt64(V177.DV177_DT12));        // 15자리
                    sData += string.Format("{0:D15}", Convert.ToInt64(V177.DV177_DT13));        // 15자리
                    sData += string.Format("{0:D15}", Convert.ToInt64(V177.DV177_DT14));        // 15자리
                    sData += string.Format("{0:D15}", Convert.ToInt64(V177.DV177_DT15));        // 15자리
                    sData += string.Format("{0:D15}", Convert.ToInt64(V177.DV177_DT16));        // 15자리
                    sData += string.Format("{0:D15}", Convert.ToInt64(V177.DV177_DT17));        // 15자리
                    sData += string.Format("{0:D15}", Convert.ToInt64(V177.DV177_DT18));        // 15자리
                    sData += string.Format("{0:D15}", Convert.ToInt64(V177.DV177_DT19));        // 15자리 부가세법에 따른 합계
                    sData += string.Format("{0:D15}", Convert.ToInt64(V177.DV177_DT20));        // 15자리
                    sData += string.Format("{0:D15}", Convert.ToInt64(V177.DV177_DT21));        // 15자리
                    sData += string.Format("{0:D15}", Convert.ToInt64(V177.DV177_DT22));        // 15자리
                    sData += string.Format("{0:D15}", Convert.ToInt64(V177.DV177_DT23));        // 15자리
                    sData += string.Format("{0:D15}", Convert.ToInt64(V177.DV177_DT24));        // 15자리 농어민등에게공급하는농축임어업용기자재
                    sData += string.Format("{0:D15}", Convert.ToInt64(V177.DV177_DT25));        // 15자리
                    sData += string.Format("{0:D15}", Convert.ToInt64(V177.DV177_DT26));        // 15자리
                    sData += string.Format("{0:D15}", Convert.ToInt64(V177.DV177_DT27));        // 15자리 조특법및그밖의법률에 따른 합계
                    sData += string.Format("{0:D15}", Convert.ToInt64(V177.DV177_DT28));        // 15자리 영세율적용공급실적총합계

                    //2016년 1분기 변경사항
                    sData += string.Format("{0:D15}", Convert.ToInt64(V177.DV177_DT29));        // 15자리 군부대공급석유류
                    sData += string.Format("{0:D15}", Convert.ToInt64(V177.DV177_DT30));        // 15자리 어민에게공급하는어업용기자재

                    sData += V177.DV177_DT31; // 4자리 공백
                }

            };

            if (sData != "")
            {
                sw.WriteLine(sData); //  RECOED 저장(집계)
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
        #region Description : 신고서 구조 정보 선언 (HEAD V101)
        public struct struct_HV101
        {
            // 부가세가치세 일반 및 간이 신고서
            // 1) 신고서 Head 레코드
            public string HV101_DT01;          //1자리
            public string HV101_DT02;          //1자리
            public string HV101_DT03;          //1자리
            public string HV101_DT04;          //1자리
            public string HV101_DT05;          //1자리
            public string HV101_DT06;          //1자리
            public string HV101_DT07;          //1자리
            public string HV101_DT08;          //1자리
            public string HV101_DT09;          //1자리
            public string HV101_DT10;          //1자리
            public string HV101_DT11;          //1자리
            public string HV101_DT12;          //1자리
            public string HV101_DT13;          //1자리
            public string HV101_DT14;          //1자리
            public string HV101_DT15;          //1자리
            public string HV101_DT16;          //1자리
            public string HV101_DT17;          //1자리
            public string HV101_DT18;          //1자리
            public string HV101_DT19;          //1자리
            public string HV101_DT20;          //1자리
            public string HV101_DT21;          //1자리
            public string HV101_DT22;          //1자리
            public string HV101_DT23;          //1자리
            public string HV101_DT24;          //1자리
            public string HV101_DT25;          //1자리
            public string HV101_DT26;          //1자리
            public string HV101_DT27;          //1자리
            public string HV101_DT28;          //1자리
            public string HV101_DT29;          //1자리
            public string HV101_DT30;          //1자리
            public string HV101_DT31;          //1자리
            public string HV101_DT32;          //1자리
            public string HV101_DT33;          //1자리
            public string HV101_DT34;          //1자리
            public string HV101_DT35;          //1자리
            public string HV101_DT36;          //51자리
        }
        #endregion

        #region Description : 신고서 구조 정보 선언 (V101)
        public struct struct_DV101
        {
            // 2) 일반과세자 신고서 레코드(수정신.경정청구의 일반과세자신고서레코드_당초)
            public string DV101_DT01;          //1자리
            public string DV101_DT02;          //1자리
            public string DV101_DT03;          //1자리
            public string DV101_DT04;          //1자리
            public string DV101_DT05;          //1자리
            public string DV101_DT06;          //1자리
            public string DV101_DT07;          //1자리
            public string DV101_DT08;          //1자리
            public string DV101_DT09;          //1자리
            public string DV101_DT10;          //1자리
            public string DV101_DT11;          //1자리
            public string DV101_DT12;          //1자리
            public string DV101_DT13;          //1자리
            public string DV101_DT14;          //1자리
            public string DV101_DT15;          //1자리
            public string DV101_DT16;          //1자리
            public string DV101_DT17;          //1자리
            public string DV101_DT18;          //1자리
            public string DV101_DT19;          //1자리
            public string DV101_DT20;          //1자리
            public string DV101_DT21;          //1자리
            public string DV101_DT22;          //1자리
            public string DV101_DT23;          //1자리
            public string DV101_DT24;          //1자리
            public string DV101_DT25;          //1자리
            public string DV101_DT26;          //1자리
            public string DV101_DT27;          //1자리
            public string DV101_DT28;          //1자리
            public string DV101_DT29;          //1자리
            public string DV101_DT30;          //1자리
            public string DV101_DT31;          //1자리
            public string DV101_DT32;          //1자리
            public string DV101_DT33;          //1자리
            public string DV101_DT34;          //1자리
            public string DV101_DT35;          //1자리
            public string DV101_DT36;          //1자리
            public string DV101_DT37;          //1자리
            public string DV101_DT38;          //1자리
            public string DV101_DT39;          //1자리
            public string DV101_DT40;          //1자리
            public string DV101_DT41;          //1자리
            public string DV101_DT42;          //1자리
            public string DV101_DT43;          //1자리
            public string DV101_DT44;          //1자리
            public string DV101_DT45;          //1자리
            public string DV101_DT46;          //1자리
            public string DV101_DT47;          //1자리
            public string DV101_DT48;          //1자리
            public string DV101_DT49;          //1자리
            public string DV101_DT50;          //1자리
            public string DV101_DT51;          //1자리
            public string DV101_DT52;          //1자리
            public string DV101_DT53;          //1자리
            public string DV101_DT54;          //1자리
            public string DV101_DT55;          //1자리
            public string DV101_DT56;          //1자리
            public string DV101_DT57;          //1자리
            public string DV101_DT58;          //1자리
            public string DV101_DT59;          //1자리
            public string DV101_DT60;          //1자리
            public string DV101_DT61;          //1자리
            public string DV101_DT62;          //1자리
            public string DV101_DT63;          //1자리
            public string DV101_DT64;          //1자리
            public string DV101_DT65;          //1자리
            public string DV101_DT66;          //1자리
            public string DV101_DT67;          //1자리
            public string DV101_DT68;          //1자리
            public string DV101_DT69;          //1자리
            public string DV101_DT70;          //1자리
            public string DV101_DT71;          //1자리
            public string DV101_DT72;          //1자리
            public string DV101_DT73;          //1자리
            public string DV101_DT74;          //1자리
            public string DV101_DT75;          //1자리
            public string DV101_DT76;          //1자리
            public string DV101_DT77;          //1자리
            public string DV101_DT78;          //1자리
            public string DV101_DT79;          //1자리
            public string DV101_DT80;          //1자리
            public string DV101_DT81;          //1자리
            public string DV101_DT82;          //1자리
            public string DV101_DT83;          //1자리
            public string DV101_DT84;          //1자리
            public string DV101_DT85;          //1자리
            public string DV101_DT86;          //1자리
            public string DV101_DT87;          //1자리
            public string DV101_DT88;          //1자리
            public string DV101_DT89;          //1자리
            public string DV101_DT90;          //1자리
            public string DV101_DT91;          //1자리
            public string DV101_DT92;          //1자리
            public string DV101_DT93;          //1자리
            public string DV101_DT94;          //1자리
            public string DV101_DT95;          //1자리
            public string DV101_DT96;          //1자리
            public string DV101_DT97;          //1자리
            public string DV101_DT98;          //1자리
            public string DV101_DT99;          //1자리
            public string DV101_DT100;          //1자리
            public string DV101_DT101;          //1자리
            public string DV101_DT102;          //1자리
            public string DV101_DT103;          //1자리
            public string DV101_DT104;          //1자리
            public string DV101_DT105;          //1자리
            public string DV101_DT106;          //1자리
            public string DV101_DT107;          //1자리
            public string DV101_DT108;          //1자리
            public string DV101_DT109;          //1자리
            public string DV101_DT110;          //1자리
            public string DV101_DT111;          //1자리
            public string DV101_DT112;          //1자리
            public string DV101_DT113;          //1자리
            public string DV101_DT114;          //1자리
            public string DV101_DT115;          //1자리
            public string DV101_DT116;          //1자리
            public string DV101_DT117;          //1자리
            public string DV101_DT118;          //1자리
            public string DV101_DT119;          //1자리
            public string DV101_DT120;          //1자리
            public string DV101_DT121;          //1자리
            public string DV101_DT122;          //1자리
            public string DV101_DT123;          //1자리
            public string DV101_DT124;          //1자리
            public string DV101_DT125;          //1자리
            public string DV101_DT126;          //1자리
            public string DV101_DT127;          //1자리
            public string DV101_DT128;          //1자리
            public string DV101_DT129;          //1자리
            public string DV101_DT130;          //1자리
            public string DV101_DT131;          //1자리
            public string DV101_DT132;          //1자리
            public string DV101_DT133;          //1자리
            public string DV101_DT134;          //1자리
            public string DV101_DT135;          //1자리
            public string DV101_DT136;          //1자리
            public string DV101_DT137;          //1자리
            public string DV101_DT138;          //1자리
            public string DV101_DT139;          //1자리
            public string DV101_DT140;          //1자리
            public string DV101_DT141;          //1자리
            public string DV101_DT142;          //1자리
            public string DV101_DT143;          //추가 (2014.02.22)
            public string DV101_DT144;          //추가 (2014.02.22)
            public string DV101_DT145;          //추가 (2014.02.22)
            public string DV101_DT146;          //추가 (2014.02.22)
            public string DV101_DT147;          //추가 (2014.02.22)
            public string DV101_DT148;          //추가 (2014.02.22)
            public string DV101_DT149;          //81자리 (공란)
        }
        #endregion

        #region Description : 부가가치세수입금액등 정보 선언 (V101)
        public struct struct_CV101
        {
            // 3) 부가가치세수입금액등
            public string CV101_DT01;          //   2자리
            public string CV101_DT02;          //   4자리
            public string CV101_DT03;          //   1자리
            public string CV101_DT04;          //  30자리
            public string CV101_DT05;          //  50자리
            public string CV101_DT06;          //   7자리
            public string CV101_DT07;          //  15자리
            public string CV101_DT08;          // 150자리 : 공란

        }
        #endregion

        #region Description : 부가가치세 공제감면  정보 선언 (V101)
        public struct struct_CV102
        {
            // 3) 부가가치세공제감면
            public string CV102_DT01;      
            public string CV102_DT02;      
            public string CV102_DT03;      
            public string CV102_DT04;      
            public string CV102_DT05;      
            public string CV102_DT06;      
            public string CV102_DT07;                

        }
        #endregion


        #region Description : 영세율첨부서류 제출 구조 정보 선언 (V106)
        public struct struct_DV106
        {
            public string DV106_DT01;          //1자리
            public string DV106_DT02;          //1자리
            public string DV106_DT03;          //1자리
            public string DV106_DT04;          //1자리
            public string DV106_DT05;          //1자리
            public string DV106_DT06;          //1자리
            public string DV106_DT07;          //1자리
            public string DV106_DT08;          //1자리
            public string DV106_DT09;          //1자리
            public string DV106_DT10;          //1자리
            public string DV106_DT11;          //1자리
            public string DV106_DT12;          //1자리
            public string DV106_DT13;          //1자리
            public string DV106_DT14;          //1자리
            public string DV106_DT15;          //1자리
            public string DV106_DT16;          //28자리 공란
        }
        #endregion

        #region Description : 사업장별 부가가치세 과세표준 및 납부세액(환급세액) 신고명세서 구조 정보 선언  (V115)
        public struct struct_DV115_17
        {
            public string DV115_17_DT01;          //1자리
            public string DV115_17_DT02;          //1자리
            public string DV115_17_DT03;          //1자리
            public string DV115_17_DT04;          //1자리
            public string DV115_17_DT05;          //1자리
            public string DV115_17_DT06;          //1자리
            public string DV115_17_DT07;          //1자리
            public string DV115_17_DT08;          //1자리
            public string DV115_17_DT09;          //1자리
            public string DV115_17_DT10;          //1자리
            public string DV115_17_DT11;          //1자리
            public string DV115_17_DT12;          //1자리
            public string DV115_17_DT13;          //1자리
            public string DV115_17_DT14;          //1자리
            public string DV115_17_DT15;          //1자리
            public string DV115_17_DT16;          //99자리 공란
        }

        public struct struct_DV115_18
        {
            public string DV115_18_DT01;          //1자리
            public string DV115_18_DT02;          //1자리
            public string DV115_18_DT03;          //1자리
            public string DV115_18_DT04;          //1자리
            public string DV115_18_DT05;          //1자리
            public string DV115_18_DT06;          //1자리
            public string DV115_18_DT07;          //1자리
            public string DV115_18_DT08;          //1자리
            public string DV115_18_DT09;          //1자리
            public string DV115_18_DT10;          //1자리
            public string DV115_18_DT11;          //1자리
            public string DV115_18_DT12;          //1자리
            public string DV115_18_DT13;          //1자리
            public string DV115_18_DT14;          //1자리
            public string DV115_18_DT15;          //1자리
            public string DV115_18_DT16;          //1자리
            public string DV115_18_DT17;          //1자리
            public string DV115_18_DT18;          //29자리 공란
        }

        #endregion

        #region Description : 세금계산서 매입,매출 구조 정보 선언
        public struct struct_HEADER
        {
            public string HEADER_GUBN;          //1자리
            public string HEADER_SAUPNO;        //10자리
            public string HEADER_SANGHO;        //30자리
            public string HEADER_IRUM;          //15자리
            public string HEADER_JUSO;          //45자리
            public string HEADER_UPTE;          //17자리
            public string HEADER_UPJONG;        //25자리
            public string HEADER_STDATE;        //6자리
            public string HEADER_EDDATE;        //6자리
            public string HEADER_DATE;          //6자리
            public string HEADER_FILLER;        //9자리
        }

        public struct struct_MaeChul
        {
            public string GUBN;          //1자리
            public string SAUPNO;        //10자리
            public string SEQ;           //4자리
            public string SAUPNO1;       //10자리
            public string SANGHO;        //30자리
            public string UPTE;          //17자리
            public string UPJONG;        //25자리
            public string CNT;           //7자리
            public string GONG;          //2자리
            public string AMT;           //14자리
            public string VAT;           //13자리
            public string DOCODE;        //1자리
            public string SOCODE;        //1자리
            public string BOOKNO;        //4자리
            public string CODE;          //3자리
            public string FILLER;        //28자리
        }

        public struct struct_MaeChulTotal
        {
            public string T1GUBN;          //1자리
            public string T1SAUPNO;        //10자리
            public string T1SEQ;           //7자리
            public string T1CNT;           //7자리
            public string T1AMT;           //15자리
            public string T1VAT;           //14자리
            public string T1SASEQ;         //7자리
            public string T1SACNT;         //7자리
            public string T1SAAMT;         //15자리
            public string T1SAVAT;         //14자리
            public string T1JUSEQ;         //7자리
            public string T1JUCNT;         //7자리
            public string T1JUAMT;         //15자리
            public string T1JUVAT;         //14자리			
            public string FILLER;          //28자리
        }

        public struct struct_MaeIpTotal
        {
            public string T2GUBN;          //1자리
            public string T2SAUPNO;        //10자리

            public string T2SEQ;           //7자리
            public string T2CNT;           //7자리
            public string T2AMT;           //15자리
            public string T2VAT;           //14자리
            // 2010 년 4월 22일추가 
            public string T2SASEQ;          //7자리
            public string T2SACNT;          //7자리
            public string T2SAAMT;          //15자리
            public string T2SAVAT;          //14자리
            public string T2JUSEQ;          //7자리
            public string T2JUCNT;          //7자리
            public string T2JUAMT;          //15자리
            public string T2JUVAT;          //14자리
            // 2010 년 4월 22일추가 끝
            public string FILLER;           //30자리
        }
        #endregion

        #region Description : 신용카드매출전표등 수치명세서(갑,을) 구조 정보 선언 (V164)

        public struct struct_HR
        {
            public string HR_DT01;          //  2자리
            public string HR_DT02;          //  2자리
            public string HR_DT03;          //  2자리
            public string HR_DT04;          //  2자리
            public string HR_DT05;          //  2자리
            public string HR_DT06;          //  2자리
            public string HR_DT07;          //  2자리
            public string HR_DT08;          //  2자리
            public string HR_DT09;          //  2자리
            public string HR_DT10;          //  2자리
        }

        public struct struct_DR
        {
            public string DR_DT01;          //  2자리
            public string DR_DT02;          //  2자리
            public string DR_DT03;          //  2자리
            public string DR_DT04;          //  2자리
            public string DR_DT05;          //  2자리
            public string DR_DT06;          //  2자리
            public string DR_DT07;          //  2자리
            public string DR_DT08;          //  2자리
            public string DR_DT09;          //  2자리
            public string DR_DT10;          //  2자리
            public string DR_DT11;          //  2자리
            public string DR_DT12;          //  2자리
            public string DR_DT13;          //  2자리
            public string DR_DT14;          //  2자리
        }

        public struct struct_TR
        {
            public string TR_DT01;          //  2자리
            public string TR_DT02;          //  2자리
            public string TR_DT03;          //  2자리
            public string TR_DT04;          //  2자리
            public string TR_DT05;          //  2자리
            public string TR_DT06;          //  2자리
            public string TR_DT07;          //  2자리
            public string TR_DT08;          //  2자리
            public string TR_DT09;          //  2자리
            public string TR_DT10;          //  2자리
            public string TR_DT11;          //  2자리
            public string TR_DT12;          //  2자리
        }
        #endregion

        #region Description : 건물등 감가상각자산 취득명세서 구조 정보 선언 (V149)
        public struct struct_DV149
        {
            public string DV149_DT01;          //1자리
            public string DV149_DT02;          //1자리
            public string DV149_DT03;          //1자리
            public string DV149_DT04;          //1자리
            public string DV149_DT05;          //1자리
            public string DV149_DT06;          //1자리
            public string DV149_DT07;          //1자리
            public string DV149_DT08;          //1자리
            public string DV149_DT09;          //1자리
            public string DV149_DT10;          //1자리
            public string DV149_DT11;          //1자리
            public string DV149_DT12;          //1자리
            public string DV149_DT13;          //1자리
            public string DV149_DT14;          //1자리
            public string DV149_DT15;          //1자리
            public string DV149_DT16;          //1자리
            public string DV149_DT17;          //1자리
            public string DV149_DT18;          //1자리
        }
        #endregion

        #region Description : 공제받지못할매입세액명세서 구조 정보 선언 (V153)
        public struct struct_DV153
        {
            public string DV153_DT01;          //1자리
            public string DV153_DT02;          //1자리
            public string DV153_DT03;          //1자리
            public string DV153_DT04;          //1자리
            public string DV153_DT05;          //1자리
            public string DV153_DT06;          //1자리
            public string DV153_DT07;          //1자리
            public string DV153_DT08;          //1자리
            public string DV153_DT09;          //1자리
            public string DV153_DT10;          //1자리
            public string DV153_DT11;          //1자리
            public string DV153_DT12;          //1자리
            public string DV153_DT13;          //1자리
        }

        public struct struct_DV153_D
        {
            public string DV153_D_DT01;          //1자리
            public string DV153_D_DT02;          //1자리
            public string DV153_D_DT03;          //1자리
            public string DV153_D_DT04;          //1자리
            public string DV153_D_DT05;          //1자리
            public string DV153_D_DT06;          //1자리
            public string DV153_D_DT07;          //1자리
        }
        #endregion

        #region Description : 수출실적 명세서 구조 정보 선언 (V141)
        public struct struct_DV141_A
        {
            public string DV141_A_DT01;          //1자리
            public string DV141_A_DT02;          //1자리
            public string DV141_A_DT03;          //1자리
            public string DV141_A_DT04;          //1자리
            public string DV141_A_DT05;          //1자리
            public string DV141_A_DT06;          //1자리
            public string DV141_A_DT07;          //1자리
            public string DV141_A_DT08;          //1자리
            public string DV141_A_DT09;          //1자리
            public string DV141_A_DT10;          //1자리
            public string DV141_A_DT11;          //1자리
            public string DV141_A_DT12;          //1자리
         }

        public struct struct_DV141_B
        {
            public string DV141_B_DT01;          //1자리
            public string DV141_B_DT02;          //1자리
            public string DV141_B_DT03;          //1자리
            public string DV141_B_DT04;          //1자리
            public string DV141_B_DT05;          //1자리
            public string DV141_B_DT06;          //1자리
            public string DV141_B_DT07;          //1자리
            public string DV141_B_DT08;          //1자리
            public string DV141_B_DT09;          //1자리
            public string DV141_B_DT10;          //1자리
            public string DV141_B_DT11;          //1자리
            public string DV141_B_DT12;          //1자리
            public string DV141_B_DT13;          //1자리
            public string DV141_B_DT14;          //1자리
        }

        public struct struct_DV141_C
        {
            public string DV141_C_DT01;          //1자리
            public string DV141_C_DT02;          //1자리
            public string DV141_C_DT03;          //1자리
            public string DV141_C_DT04;          //1자리
            public string DV141_C_DT05;          //1자리
            public string DV141_C_DT06;          //1자리
            public string DV141_C_DT07;          //1자리
            public string DV141_C_DT08;          //1자리
            public string DV141_C_DT09;          //1자리
            public string DV141_C_DT10;          //1자리
            public string DV141_C_DT11;          //1자리
            public string DV141_C_DT12;          //1자리
        }
        #endregion

        #region Description : 영세율매출명세서  구조 정보 선언 (V177)
        public struct struct_DV177
        {
            public string DV177_DT01;          //1자리
            public string DV177_DT02;          //1자리
            public string DV177_DT03;          //1자리
            public string DV177_DT04;          //1자리
            public string DV177_DT05;          //1자리
            public string DV177_DT06;          //1자리
            public string DV177_DT07;          //1자리
            public string DV177_DT08;          //1자리
            public string DV177_DT09;          //1자리
            public string DV177_DT10;          //1자리
            public string DV177_DT11;          //1자리
            public string DV177_DT12;          //1자리
            public string DV177_DT13;          //1자리
            public string DV177_DT14;          //1자리
            public string DV177_DT15;          //1자리
            public string DV177_DT16;          //1자리
            public string DV177_DT17;          //1자리
            public string DV177_DT18;          //1자리
            public string DV177_DT19;          //1자리
            public string DV177_DT20;          //1자리
            public string DV177_DT21;          //1자리
            public string DV177_DT22;          //1자리
            public string DV177_DT23;          //1자리
            public string DV177_DT24;          //1자리
            public string DV177_DT25;          //1자리
            public string DV177_DT26;          //1자리
            public string DV177_DT27;          //1자리
            public string DV177_DT28;          //1자리
            public string DV177_DT29;          //1자리
            public string DV177_DT30;          //1자리
            public string DV177_DT31;          //1자리
        }
        #endregion

        #region Description : 사업장별부가가치세과세표준 및 납부세액(환급세액)신고명세서 구조 정보 선언 (V115)
        public struct struct_DV115_H
        {
            public string DV115_H_DT01;          //1자리
            public string DV115_H_DT02;          //1자리
            public string DV115_H_DT03;          //1자리
            public string DV115_H_DT04;          //1자리
            public string DV115_H_DT05;          //1자리
            public string DV115_H_DT06;          //1자리
            public string DV115_H_DT07;          //1자리
            public string DV115_H_DT08;          //1자리
            public string DV115_H_DT09;          //1자리
            public string DV115_H_DT10;          //1자리
            public string DV115_H_DT11;          //1자리
            public string DV115_H_DT12;          //1자리
            public string DV115_H_DT13;          //1자리
            public string DV115_H_DT14;          //1자리
            public string DV115_H_DT15;          //1자리
            public string DV115_H_DT16;          //99자리

        }

        public struct struct_DV115_D
        {
            public string DV115_D_DT01;          //1자리
            public string DV115_D_DT02;          //1자리
            public string DV115_D_DT03;          //1자리
            public string DV115_D_DT04;          //1자리
            public string DV115_D_DT05;          //1자리
            public string DV115_D_DT06;          //1자리
            public string DV115_D_DT07;          //1자리
            public string DV115_D_DT08;          //1자리
            public string DV115_D_DT09;          //1자리
            public string DV115_D_DT10;          //1자리
            public string DV115_D_DT11;          //1자리
            public string DV115_D_DT12;          //1자리
            public string DV115_D_DT13;          //1자리
            public string DV115_D_DT14;          //1자리
            public string DV115_D_DT15;          //1자리
            public string DV115_D_DT16;          //1자리
            public string DV115_D_DT17;          //1자리
            public string DV115_D_DT18;          //29자리

        }
        #endregion

        #region Description : 계산서 합계표 구조 정보 선언 (V109)

        // 제출자(대리인)레코드 (A)
        public struct struct_DV109_A
        {
            public string DV109_A_DT01;          //1자리
            public string DV109_A_DT02;          //1자리
            public string DV109_A_DT03;          //1자리
            public string DV109_A_DT04;          //1자리
            public string DV109_A_DT05;          //1자리
            public string DV109_A_DT06;          //1자리
            public string DV109_A_DT07;          //1자리
            public string DV109_A_DT08;          //1자리
            public string DV109_A_DT09;          //1자리
            public string DV109_A_DT10;          //1자리
            public string DV109_A_DT11;          //1자리
            public string DV109_A_DT12;          //1자리
            public string DV109_A_DT13;          //1자리
            public string DV109_A_DT14;          //1자리
            public string DV109_A_DT15;          //15자리 공란
        }

        // 제출의무자인적사항레코드 (B)
        public struct struct_DV109_B
        {
            public string DV109_B_DT01;          //1자리
            public string DV109_B_DT02;          //1자리
            public string DV109_B_DT03;          //1자리
            public string DV109_B_DT04;          //1자리
            public string DV109_B_DT05;          //1자리
            public string DV109_B_DT06;          //1자리
            public string DV109_B_DT07;          //1자리
            public string DV109_B_DT08;          //1자리
            public string DV109_B_DT09;          //60자리 공란
        }

        // 제출의무자별집계레코드(매출)  (C)
        public struct struct_DV109_C
        {
            public string DV109_C_DT01;          //1자리
            public string DV109_C_DT02;          //1자리
            public string DV109_C_DT03;          //1자리
            public string DV109_C_DT04;          //1자리
            public string DV109_C_DT05;          //1자리
            public string DV109_C_DT06;          //1자리
            public string DV109_C_DT07;          //1자리
            public string DV109_C_DT08;          //1자리
            public string DV109_C_DT09;          //1자리
            public string DV109_C_DT10;          //1자리
            public string DV109_C_DT11;          //1자리
            public string DV109_C_DT12;          //1자리
            public string DV109_C_DT13;          //1자리
            public string DV109_C_DT14;          //1자리
            public string DV109_C_DT15;          //1자리
            public string DV109_C_DT16;          //1자리
            public string DV109_C_DT17;          //1자리
            public string DV109_C_DT18;          //1자리
            public string DV109_C_DT19;          //1자리
            public string DV109_C_DT20;          //1자리
            public string DV109_C_DT21;          //1자리
            public string DV109_C_DT22;          //1자리
            public string DV109_C_DT23;          //1자리
            public string DV109_C_DT24;          //97자리 공란
        }

        //  매출처별거래명세레코드 (D)
        public struct struct_DV109_D
        {
            public string DV109_D_DT01;          //1자리
            public string DV109_D_DT02;          //1자리
            public string DV109_D_DT03;          //1자리
            public string DV109_D_DT04;          //1자리
            public string DV109_D_DT05;          //1자리
            public string DV109_D_DT06;          //1자리
            public string DV109_D_DT07;          //1자리
            public string DV109_D_DT08;          //1자리
            public string DV109_D_DT09;          //1자리
            public string DV109_D_DT10;          //1자리
            public string DV109_D_DT11;          //1자리
            public string DV109_D_DT12;          //1자리
            public string DV109_D_DT13;          //136자리 공란
        }

        // 전자계산서 매출집계레코드(매출) (E)
        public struct struct_DV109_E
        {
            public string DV109_E_DT01;          //1자리
            public string DV109_E_DT02;          //1자리
            public string DV109_E_DT03;          //1자리
            public string DV109_E_DT04;          //1자리
            public string DV109_E_DT05;          //1자리
            public string DV109_E_DT06;          //1자리
            public string DV109_E_DT07;          //1자리
            public string DV109_E_DT08;          //1자리
            public string DV109_E_DT09;          //1자리
            public string DV109_E_DT10;          //1자리
            public string DV109_E_DT11;          //1자리
            public string DV109_E_DT12;          //1자리
            public string DV109_E_DT13;          //1자리
            public string DV109_E_DT14;          //1자리
            public string DV109_E_DT15;          //1자리
            public string DV109_E_DT16;          //1자리
            public string DV109_E_DT17;          //1자리
            public string DV109_E_DT18;          //1자리
            public string DV109_E_DT19;          //1자리
            public string DV109_E_DT20;          //1자리
            public string DV109_E_DT21;          //1자리
            public string DV109_E_DT22;          //1자리
            public string DV109_E_DT23;          //1자리
            public string DV109_E_DT24;          //97자리 공란
        }

        // ----------------------------    매   입    ----------------------------
        // 제출의무자별집계레코드(매입) (C)
        public struct struct_DV110_C
        {
            public string DV110_C_DT01;          //1자리
            public string DV110_C_DT02;          //1자리
            public string DV110_C_DT03;          //1자리
            public string DV110_C_DT04;          //1자리
            public string DV110_C_DT05;          //1자리
            public string DV110_C_DT06;          //1자리
            public string DV110_C_DT07;          //1자리
            public string DV110_C_DT08;          //1자리
            public string DV110_C_DT09;          //1자리
            public string DV110_C_DT10;          //1자리
            public string DV110_C_DT11;          //1자리
            public string DV110_C_DT12;          //1자리
            public string DV110_C_DT13;          //1자리
            public string DV110_C_DT14;          //1자리
            public string DV110_C_DT15;          //1자리
            public string DV110_C_DT16;          //151자리 공란
        }

        // 매입처별거래명세레코드 (D)
        public struct struct_DV110_D
        {
            public string DV110_D_DT01;          //1자리
            public string DV110_D_DT02;          //1자리
            public string DV110_D_DT03;          //1자리
            public string DV110_D_DT04;          //1자리
            public string DV110_D_DT05;          //1자리
            public string DV110_D_DT06;          //1자리
            public string DV110_D_DT07;          //1자리
            public string DV110_D_DT08;          //1자리
            public string DV110_D_DT09;          //1자리
            public string DV110_D_DT10;          //1자리
            public string DV110_D_DT11;          //1자리
            public string DV110_D_DT12;          //1자리
            public string DV110_D_DT13;          //136자리 공란
        }

        // 전자계산서 매입집계레코드 (E)
        public struct struct_DV110_E
        {
            public string DV110_E_DT01;          //1자리
            public string DV110_E_DT02;          //1자리
            public string DV110_E_DT03;          //1자리
            public string DV110_E_DT04;          //1자리
            public string DV110_E_DT05;          //1자리
            public string DV110_E_DT06;          //1자리
            public string DV110_E_DT07;          //1자리
            public string DV110_E_DT08;          //1자리
            public string DV110_E_DT09;          //1자리
            public string DV110_E_DT10;          //1자리
            public string DV110_E_DT11;          //1자리
            public string DV110_E_DT12;          //1자리
            public string DV110_E_DT13;          //1자리
            public string DV110_E_DT14;          //1자리
            public string DV110_E_DT15;          //1자리
            public string DV110_E_DT16;          //151자리 공란
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
    }
}