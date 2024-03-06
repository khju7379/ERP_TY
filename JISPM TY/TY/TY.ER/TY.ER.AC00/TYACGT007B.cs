using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.IO;
using System.Text;

namespace TY.ER.AC00
{
    /// <summary>
    /// 부가세신고 파일생성 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.11.08 16:13
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2B82S200 : 부가세 자료 조회
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2B77B165 : 파일을 다운 작업을 하시겠습니까?
    ///  TY_M_GB_25UAA711 : 파일을 다운로드하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  DWN : 다운
    ///  GOKCR : 생성구분
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    /// </summary>
    public partial class TYACGT007B : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYACGT007B()
        {
            InitializeComponent();
        }

        private void TYACGT007B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_DWN.ProcessCheck += new TButton.CheckHandler(BTN61_DWN_ProcessCheck);

            this.DTP01_GSTDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_GEDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
        }
        #endregion

        #region  Description : 파일다운 버튼 이벤트
        private void BTN61_DWN_Click(object sender, EventArgs e)
        {
            UP_TAXFile_Dwon();
        }
        #endregion

        #region Description : 파일 다운 ProcessCheck 이벤트
        private void BTN61_DWN_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            Int16 iCnt1 = 0;
            Int16 iCnt2 = 0;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2B84W203", this.DTP01_GSTDATE.GetString().Substring(0, 6), this.DTP01_GEDDATE.GetString().Substring(0, 6), "2");
            iCnt1 = Convert.ToInt16(this.DbConnector.ExecuteScalar());

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2B84W203", this.DTP01_GSTDATE.GetString().Substring(0, 6), this.DTP01_GEDDATE.GetString().Substring(0, 6), "1");
            iCnt2 = Convert.ToInt16(this.DbConnector.ExecuteScalar());

            if (iCnt1 <= 0 && iCnt2 <= 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
                e.Successed = false;
                return;
            }


            if (!this.ShowMessage("TY_M_AC_2B77B165"))
            {
                e.Successed = false;
                return;
            }


        }
        #endregion

        #region Description : 전자세금계산서 PC DownLoad 로직 - 2010.11.05 추가
        private void UP_TAXFile_Dwon()
        {
            string sFile_Path = "C:\\EDI\\OUT\\";

            struct_HEADER HEADER = new struct_HEADER();
            struct_MaeChul Maechul = new struct_MaeChul();
            struct_MaeChulTotal MaechulTotal = new struct_MaeChulTotal();
            struct_MaeIpTotal MaeIpTotal = new struct_MaeIpTotal();

            if (System.IO.Directory.Exists(sFile_Path) == false)
            {
                System.IO.Directory.CreateDirectory(sFile_Path);
            }        

            try
            {

                string sInCDAC = "";
                string sOutCDAC = "";
                string sStrTemp = "";                
                string sFile_Name = "";

                Int32 iCnt = 0;

                // 구분(1.본점, 2.지점)에 따라 파일이름을 각각 설정한다.
                string sPath = "";
                if ( this.CBO01_GOKCR.GetValue().ToString() == "1")
                {
                    sPath = @"C:\EDI\OUT\K6108110.449";

                    sFile_Name = "K6108110.449";

                    sInCDAC = "11103101";
                    sOutCDAC = "21103101";

                    if (File.Exists("C:\\EDI\\OUT\\K6108110.449"))
                    {
                        File.Delete("C:\\EDI\\OUT\\K6108110.449");
                    }
                }
                else
                {
                    sPath = @"C:\EDI\OUT\K1058516.181";
                    sFile_Name = "K1058516.181";

                    sInCDAC = "11103102";
                    sOutCDAC = "21103102";

                    if (File.Exists("C:\\EDI\\OUT\\K1058516.181"))
                    {
                        File.Delete("C:\\EDI\\OUT\\K1058516.181");
                    }

                }

                StreamWriter sw = new StreamWriter(sPath, false, Encoding.Default);

                //StreamWriter sw = File.AppendText(sPath);                

                string sData = "";

                //부가세 헤더 작성
                if (this.CBO01_GOKCR.GetValue().ToString() == "1") //본점
                {
                    HEADER.HEADER_GUBN = "7";                 // 1자리
                    HEADER.HEADER_SAUPNO = "6108110449";        // 10자리
                    HEADER.HEADER_SANGHO = " 주식회사태영인더스트리";    // 30자리
                    HEADER.HEADER_IRUM = "정세진,윤석민";           // 15자리
                    HEADER.HEADER_JUSO = "울산시 남구 용잠로 459 (용잠동)";        // 45자리
                    HEADER.HEADER_UPTE = "운보";             // 17자리
                    HEADER.HEADER_UPJONG = "하역및보관업외";   // 25자리
                    HEADER.HEADER_STDATE = DTP01_GSTDATE.GetString().ToString().Substring(2,6);  // 6자리
                    HEADER.HEADER_EDDATE = DTP01_GEDDATE.GetString().ToString().Substring(2, 6);   // 6자리
                    HEADER.HEADER_DATE = DateTime.Now.ToString("yyyyMMdd").Substring(2, 6);        // 6자리
                    HEADER.HEADER_FILLER = "         ";        // 9자리
                }
                else //지점
                {
                    HEADER.HEADER_GUBN = "7";                // 1자리
                    HEADER.HEADER_SAUPNO = "1058516181";       // 10자리
                    HEADER.HEADER_SANGHO = " 주식회사태영인더스트리서울지";  // 30자리
                    HEADER.HEADER_IRUM = "정세진,윤석민";           // 15자리
                    HEADER.HEADER_JUSO = "서울시 영등포구 여의공원로 111";  // 45자리
                    HEADER.HEADER_UPTE = "도매";            // 17자리
                    HEADER.HEADER_UPJONG = "무역";            // 25자리
                    HEADER.HEADER_STDATE = DTP01_GSTDATE.GetString().ToString().Substring(2, 6);  // 6자리
                    HEADER.HEADER_EDDATE = DTP01_GEDDATE.GetString().ToString().Substring(2, 6);   // 6자리
                    HEADER.HEADER_DATE = DateTime.Now.ToString("yyyyMMdd").Substring(2, 6);        // 6자리
                    HEADER.HEADER_FILLER = "         ";       // 9자리					
                }

                sData = HEADER.HEADER_GUBN;
                sData += HEADER.HEADER_SAUPNO;

                sStrTemp = HEADER.HEADER_SANGHO.Trim();
                sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(HEADER.HEADER_SANGHO.Trim())));
                sData += sStrTemp;

                sStrTemp = HEADER.HEADER_IRUM.Trim();
                sStrTemp += new String(Convert.ToChar(" "), (15 - Encoding.Default.GetByteCount(HEADER.HEADER_IRUM.Trim())));
                sData += sStrTemp;

                sStrTemp = HEADER.HEADER_JUSO.Trim();
                sStrTemp += new String(Convert.ToChar(" "), (45 - Encoding.Default.GetByteCount(HEADER.HEADER_JUSO.Trim())));
                sData += sStrTemp;

                sStrTemp = HEADER.HEADER_UPTE.Trim();
                sStrTemp += new String(Convert.ToChar(" "), (17 - Encoding.Default.GetByteCount(HEADER.HEADER_UPTE.Trim())));
                sData += sStrTemp;

                sStrTemp = HEADER.HEADER_UPJONG.Trim();
                sStrTemp += new String(Convert.ToChar(" "), (25 - Encoding.Default.GetByteCount(HEADER.HEADER_UPJONG.Trim())));
                sData += sStrTemp;

                sData += HEADER.HEADER_STDATE;
                sData += HEADER.HEADER_EDDATE;
                sData += HEADER.HEADER_DATE;
                sData += HEADER.HEADER_FILLER;

                sw.WriteLine(sData);

                // ------------------------- 매출자료 ---------------------------
                iCnt = 0;

                DataSet ds = new DataSet();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2B82S200", this.DTP01_GSTDATE.GetString().Substring(0, 6), this.DTP01_GEDDATE.GetString().Substring(0, 6), "2", sOutCDAC);
                ds = this.DbConnector.ExecuteDataSet();  

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        iCnt = iCnt + 1;

                        Maechul.GUBN = "1";                              // 자료구분 : 1자리
                        Maechul.SEQ = string.Format("{0:D4}", iCnt);     // 일련번호 : 4자리

                        if (this.CBO01_GOKCR.GetValue().ToString() == "1") //본점
                        {
                            Maechul.SAUPNO = "6108110449";               // 제출자 사업자등록번호 : 10자리
                            Maechul.CODE = "610";
                        }
                        else
                        {
                            Maechul.SAUPNO = "1058516181";
                            Maechul.CODE = "105";
                        }

                        if (ds.Tables[0].Rows[i]["VNSJGB"].ToString() == "1")
                        {
                            Maechul.SAUPNO1 = ds.Tables[0].Rows[i]["VNSAUPNO"].ToString().Substring(0, 10);
                        }
                        else
                        {
                            Maechul.SAUPNO1 = "8888888888";
                            Maechul.GUBN = "9";
                        }

                        sStrTemp = " ";
                        sStrTemp += ds.Tables[0].Rows[i]["VNSANGHO"].ToString();
                        sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(ds.Tables[0].Rows[i]["VNSANGHO"].ToString().Trim())));                        
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

                        if (Convert.ToInt64(ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim()) < 0)
                        {
                            string sTempAmt = string.Format("{0:D14}", Convert.ToInt64(ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim()) * -1);

                            string sALPHAValue = UP_Minus_ALPHA(sTempAmt.Substring(13, 1));

                            Maechul.AMT = sTempAmt.Substring(0, 13) + sALPHAValue;

                            Maechul.GONG = string.Format("{0:D2}", Convert.ToInt64(Convert.ToString(14 - ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim().Length + 1)));
                        }
                        else
                        {
                            Maechul.AMT = string.Format("{0:D14}", Convert.ToInt64(ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim()));

                            Maechul.GONG = string.Format("{0:D2}", Convert.ToInt64(Convert.ToString(14 - ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim().Length)));
                        }

                        if (Convert.ToInt64(ds.Tables[0].Rows[i]["V1VAT"].ToString().Trim()) < 0)
                        {
                            string sTempAmt = string.Format("{0:D13}", Convert.ToInt64(ds.Tables[0].Rows[i]["V1VAT"].ToString().Trim()) * -1);

                            string sALPHAValue = UP_Minus_ALPHA(sTempAmt.Substring(12, 1));

                            Maechul.VAT = sTempAmt.Substring(0, 12) + sALPHAValue;
                        }
                        else
                        {
                            Maechul.VAT = string.Format("{0:D13}", Convert.ToInt64(ds.Tables[0].Rows[i]["V1VAT"].ToString().Trim()));
                        }

                        Maechul.DOCODE = "0";
                        Maechul.SOCODE = "0";

                        Maechul.BOOKNO = "9001";
                        Maechul.FILLER = "                            ";

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

                        if (this.CBO01_GOKCR.GetValue().ToString() == "1") //본점
                        {
                            MaechulTotal.T1SAUPNO = "6108110449";
                        }
                        else
                        {
                            MaechulTotal.T1SAUPNO = "1058516181";
                        }

                        MaechulTotal.T1SEQ = Convert.ToString(iCnt);
                        MaechulTotal.T1CNT = Convert.ToString(Convert.ToDouble(MaechulTotal.T1CNT) +
                                             Convert.ToDouble(ds.Tables[0].Rows[i]["CNT"].ToString().Trim()));
                        MaechulTotal.T1AMT = Convert.ToString(Convert.ToDouble(MaechulTotal.T1AMT) +
                                             Convert.ToDouble(ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim()));
                        MaechulTotal.T1VAT = Convert.ToString(Convert.ToDouble(MaechulTotal.T1VAT) +
                                             Convert.ToDouble(ds.Tables[0].Rows[i]["V1VAT"].ToString().Trim()));

                        if (ds.Tables[0].Rows[i]["VNSJGB"].ToString() == "1")
                        {
                            MaechulTotal.T1SASEQ = Convert.ToString(Convert.ToInt32(MaechulTotal.T1SASEQ) + 1);
                            MaechulTotal.T1SACNT = Convert.ToString(Convert.ToDouble(MaechulTotal.T1SACNT) +
                                                   Convert.ToDouble(ds.Tables[0].Rows[i]["CNT"].ToString().Trim()));
                            MaechulTotal.T1SAAMT = Convert.ToString(Convert.ToDouble(MaechulTotal.T1SAAMT) +
                                                   Convert.ToDouble(ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim()));
                            MaechulTotal.T1SAVAT = Convert.ToString(Convert.ToDouble(MaechulTotal.T1SAVAT) +
                                                   Convert.ToDouble(ds.Tables[0].Rows[i]["V1VAT"].ToString().Trim()));
                        }
                        else
                        {
                            MaechulTotal.T1JUSEQ = Convert.ToString(Convert.ToInt32(MaechulTotal.T1JUSEQ) + 1);
                            MaechulTotal.T1JUCNT = Convert.ToString(Convert.ToDouble(MaechulTotal.T1JUCNT) +
                                                   Convert.ToDouble(ds.Tables[0].Rows[i]["CNT"].ToString().Trim()));
                            MaechulTotal.T1JUAMT = Convert.ToString(Convert.ToDouble(MaechulTotal.T1JUAMT) +
                                                   Convert.ToDouble(ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim()));
                            MaechulTotal.T1JUVAT = Convert.ToString(Convert.ToDouble(MaechulTotal.T1JUVAT) +
                                                   Convert.ToDouble(ds.Tables[0].Rows[i]["V1VAT"].ToString().Trim()));
                        }


                    }//for..end

                    // (전자세금계산서 이외분) 매출합계 (TOTAL RECORD)
                    MaechulTotal.FILLER = "                            ";

                    sData = MaechulTotal.T1GUBN;          // 자료구분 : 1자리
                    sData += MaechulTotal.T1SAUPNO;        // 제출자 사업자등록번호 : 10자리
                    // 합계분
                    sData += string.Format("{0:D7}", Convert.ToInt64(MaechulTotal.T1SEQ));        // 거래처수    :   7자리
                    sData += string.Format("{0:D7}", Convert.ToInt64(MaechulTotal.T1CNT));        // 계산서 매수 :   7자리
                    sData += string.Format("{0:D15}", Convert.ToInt64(MaechulTotal.T1AMT));       // 공급가액    :  15자리
                    sData += string.Format("{0:D14}", Convert.ToInt64(MaechulTotal.T1VAT));       // 세    액    :  14자리
                    // 사업자등록번호 발행분
                    sData += string.Format("{0:D7}", Convert.ToInt64(MaechulTotal.T1SASEQ));      // 거래처수    :   7자리
                    sData += string.Format("{0:D7}", Convert.ToInt64(MaechulTotal.T1SACNT));      // 계산서 매수 :   7자리
                    sData += string.Format("{0:D15}", Convert.ToInt64(MaechulTotal.T1SAAMT));     // 공급가액    :  15자리
                    sData += string.Format("{0:D14}", Convert.ToInt64(MaechulTotal.T1SAVAT));     // 세    액    :  14자리
                    // 주민등록번호 발행분
                    sData += string.Format("{0:D7}", Convert.ToInt64(MaechulTotal.T1JUSEQ));      // 거래처수    :   7자리
                    sData += string.Format("{0:D7}", Convert.ToInt64(MaechulTotal.T1JUCNT));      // 계산서 매수 :   7자리
                    sData += string.Format("{0:D15}", Convert.ToInt64(MaechulTotal.T1JUAMT));     // 공급가액    :  15자리
                    sData += string.Format("{0:D14}", Convert.ToInt64(MaechulTotal.T1JUVAT));     // 세    액    :  14자리			
                    sData += MaechulTotal.FILLER;        //28자리

                    sw.WriteLine(sData);

                }

                // 전자세금계산서분 매출 합계 처리

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
                MaechulTotal.FILLER = "";          //28자리


                iCnt = 0;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2B87J208", this.DTP01_GSTDATE.GetString().Substring(0, 6), this.DTP01_GEDDATE.GetString().Substring(0, 6), "2", sOutCDAC);
                ds = this.DbConnector.ExecuteDataSet();  

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        iCnt = iCnt + 1;

                        MaechulTotal.T1GUBN = "5";

                        if (this.CBO01_GOKCR.GetValue().ToString() == "1") //본점
                        {
                            MaechulTotal.T1SAUPNO = "6108110449";
                        }
                        else
                        {
                            MaechulTotal.T1SAUPNO = "1058516181";
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
                    MaechulTotal.FILLER = "                            ";

                    sData = MaechulTotal.T1GUBN;                                            // 자료구분 : 1자리
                    sData += MaechulTotal.T1SAUPNO;                                          // 제출자 사업자등록번호 : 10자리
                    // 합계분
                    sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaechulTotal.T1SEQ)));    // 거래처수    :   7자리
                    sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaechulTotal.T1CNT)));    // 계산서 매수 :   7자리
                    sData += string.Format("{0:D15}", Convert.ToInt64(Get_Numeric(MaechulTotal.T1AMT)));   // 공급가액    :  15자리
                    sData += string.Format("{0:D14}", Convert.ToInt64(Get_Numeric(MaechulTotal.T1VAT)));   // 세    액    :  14자리
                    // 사업자등록번호 발행분
                    sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaechulTotal.T1SASEQ)));  // 거래처수    :   7자리
                    sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaechulTotal.T1SACNT)));  // 계산서 매수 :   7자리
                    sData += string.Format("{0:D15}", Convert.ToInt64(Get_Numeric(MaechulTotal.T1SAAMT))); // 공급가액    :  15자리
                    sData += string.Format("{0:D14}", Convert.ToInt64(Get_Numeric(MaechulTotal.T1SAVAT))); // 세    액    :  14자리
                    // 주민등록번호 발행분
                    sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaechulTotal.T1JUSEQ)));  // 거래처수    :   7자리
                    sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaechulTotal.T1JUCNT)));  // 계산서 매수 :   7자리
                    sData += string.Format("{0:D15}", Convert.ToInt64(Get_Numeric(MaechulTotal.T1JUAMT))); // 공급가액    :  15자리
                    sData += string.Format("{0:D14}", Convert.ToInt64(Get_Numeric(MaechulTotal.T1JUVAT))); // 세    액    :  14자리			
                    sData += MaechulTotal.FILLER;        //28자리

                    sw.WriteLine(sData);

                }


                // --------------------------------------------------------------
                // ------------------------- 매입자료 ---------------------------
                // --------------------------------------------------------------

                ds.Clear();

                iCnt = 0;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2B82S200", this.DTP01_GSTDATE.GetString().Substring(0, 6), this.DTP01_GEDDATE.GetString().Substring(0, 6), "1", sInCDAC);
                ds = this.DbConnector.ExecuteDataSet();  

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        iCnt = iCnt + 1;

                        Maechul.GUBN = "2";
                        Maechul.SEQ = string.Format("{0:D4}", iCnt);

                        if (this.CBO01_GOKCR.GetValue().ToString() == "1") //본점
                        {
                            Maechul.SAUPNO = "6108110449";
                            Maechul.CODE = "610";
                        }
                        else
                        {
                            Maechul.SAUPNO = "1058516181";
                            Maechul.CODE = "105";
                        }

                        if (ds.Tables[0].Rows[i]["VNSJGB"].ToString() == "1")
                        {
                            Maechul.SAUPNO1 = ds.Tables[0].Rows[i]["VNSAUPNO"].ToString().Substring(0, 10);
                        }
                        else
                        {
                            Maechul.SAUPNO1 = "8888888888";
                            Maechul.GUBN = "9";
                        }

                        sStrTemp = " ";
                        sStrTemp += ds.Tables[0].Rows[i]["VNSANGHO"].ToString();
                        sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(ds.Tables[0].Rows[i]["VNSANGHO"].ToString().Trim())));
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

                        if (Convert.ToInt64(ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim()) < 0)
                        {
                            string sTempAmt = string.Format("{0:D14}", Convert.ToInt64(ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim()) * -1);

                            Maechul.AMT = sTempAmt.Substring(0, 13) + "}";

                            Maechul.GONG = string.Format("{0:D2}", Convert.ToInt64(Convert.ToString(14 - ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim().Length + 1)));
                        }
                        else
                        {
                            Maechul.AMT = string.Format("{0:D14}", Convert.ToInt64(ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim()));

                            Maechul.GONG = string.Format("{0:D2}", Convert.ToInt64(Convert.ToString(14 - ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim().Length)));
                        }

                        if (Convert.ToInt64(ds.Tables[0].Rows[i]["V1VAT"].ToString().Trim()) < 0)
                        {
                            string sTempAmt = string.Format("{0:D13}", Convert.ToInt64(ds.Tables[0].Rows[i]["V1VAT"].ToString().Trim()) * -1);

                            Maechul.VAT = sTempAmt.Substring(0, 12) + "}";
                        }
                        else
                        {
                            Maechul.VAT = string.Format("{0:D13}", Convert.ToInt64(ds.Tables[0].Rows[i]["V1VAT"].ToString().Trim()));
                        }

                        Maechul.DOCODE = "0";
                        Maechul.SOCODE = "0";

                        Maechul.BOOKNO = "9501";
                        Maechul.FILLER = "                            ";

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

                        MaeIpTotal.T2GUBN = "4";

                        if(this.CBO01_GOKCR.GetValue().ToString() == "1") //본점
                        {
                            MaeIpTotal.T2SAUPNO = "6108110449";
                        }
                        else
                        {
                            MaeIpTotal.T2SAUPNO = "1058516181";
                        }

                        MaeIpTotal.T2CNT = Convert.ToString(Convert.ToDouble(MaeIpTotal.T2CNT) +
                                           Convert.ToDouble(ds.Tables[0].Rows[i]["CNT"].ToString().Trim()));
                        MaeIpTotal.T2AMT = Convert.ToString(Convert.ToDouble(MaeIpTotal.T2AMT) +
                                           Convert.ToDouble(ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim()));
                        MaeIpTotal.T2VAT = Convert.ToString(Convert.ToDouble(MaeIpTotal.T2VAT) +
                                           Convert.ToDouble(ds.Tables[0].Rows[i]["V1VAT"].ToString().Trim()));

                        if (ds.Tables[0].Rows[i]["VNSJGB"].ToString() == "1")
                        {
                            MaeIpTotal.T2SASEQ = Convert.ToString(Convert.ToInt32(MaeIpTotal.T2SASEQ) + 1);
                            MaeIpTotal.T2SACNT = Convert.ToString(Convert.ToDouble(MaeIpTotal.T2SACNT) +
                                                 Convert.ToDouble(ds.Tables[0].Rows[i]["CNT"].ToString().Trim()));
                            MaeIpTotal.T2SAAMT = Convert.ToString(Convert.ToDouble(MaeIpTotal.T2SAAMT) +
                                                 Convert.ToDouble(ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim()));
                            MaeIpTotal.T2SAVAT = Convert.ToString(Convert.ToDouble(MaeIpTotal.T2SAVAT) +
                                                 Convert.ToDouble(ds.Tables[0].Rows[i]["V1VAT"].ToString().Trim()));
                        }
                        else
                        {
                            MaeIpTotal.T2JUSEQ = Convert.ToString(Convert.ToInt32(MaeIpTotal.T2JUSEQ) + 1);
                            MaeIpTotal.T2JUCNT = Convert.ToString(Convert.ToDouble(MaeIpTotal.T2JUCNT) +
                                                 Convert.ToDouble(ds.Tables[0].Rows[i]["CNT"].ToString().Trim()));
                            MaeIpTotal.T2JUAMT = Convert.ToString(Convert.ToDouble(MaeIpTotal.T2JUAMT) +
                                                 Convert.ToDouble(ds.Tables[0].Rows[i]["V1AMT"].ToString().Trim()));
                            MaeIpTotal.T2JUVAT = Convert.ToString(Convert.ToDouble(MaeIpTotal.T2JUVAT) +
                                                 Convert.ToDouble(ds.Tables[0].Rows[i]["V1VAT"].ToString().Trim()));
                        }

                    }//for..end

                    MaeIpTotal.FILLER = "                              "; // 30자리 공백 (2010년 4월 22일추가)

                    MaeIpTotal.T2SEQ = string.Format("{0:D4}", iCnt);

                    sData = MaeIpTotal.T2GUBN;           // 자료구분 : 1자리
                    sData += MaeIpTotal.T2SAUPNO;         // 제출자 사업자등록번호 : 10자리
                    // 합계분
                    sData += string.Format("{0:D7}", Convert.ToInt64(MaeIpTotal.T2SEQ));       // 거래처수    :   7자리
                    sData += string.Format("{0:D7}", Convert.ToInt64(MaeIpTotal.T2CNT));       // 계산서 매수 :   7자리
                    sData += string.Format("{0:D15}", Convert.ToInt64(MaeIpTotal.T2AMT));      // 공급가액    :  15자리
                    sData += string.Format("{0:D14}", Convert.ToInt64(MaeIpTotal.T2VAT));      // 세    액    :  14자리
                    // 시업자등록번호 수취분
                    sData += string.Format("{0:D7}", Convert.ToInt64(MaeIpTotal.T2SASEQ));     // 거래처수    :   7자리
                    sData += string.Format("{0:D7}", Convert.ToInt64(MaeIpTotal.T2SACNT));     // 계산서 매수 :   7자리
                    sData += string.Format("{0:D15}", Convert.ToInt64(MaeIpTotal.T2SAAMT));    // 공급가액    :  15자리
                    sData += string.Format("{0:D14}", Convert.ToInt64(MaeIpTotal.T2SAVAT));    // 세    액    :  14자리
                    // 주민등록번호 수취분												
                    sData += string.Format("{0:D7}", Convert.ToInt64(MaeIpTotal.T2JUSEQ));     // 거래처수    :   7자리
                    sData += string.Format("{0:D7}", Convert.ToInt64(MaeIpTotal.T2JUCNT));     // 계산서 매수 :   7자리
                    sData += string.Format("{0:D15}", Convert.ToInt64(MaeIpTotal.T2JUAMT));    // 공급가액    :  15자리
                    sData += string.Format("{0:D14}", Convert.ToInt64(MaeIpTotal.T2JUVAT));    // 세    액    :  14자리
                    sData += MaeIpTotal.FILLER;        //30자리

                    sw.WriteLine(sData);
                }


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

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2B87J208", this.DTP01_GSTDATE.GetString().Substring(0, 6), this.DTP01_GEDDATE.GetString().Substring(0, 6), "1", sInCDAC);
                ds = this.DbConnector.ExecuteDataSet();  

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        iCnt = iCnt + 1;

                        MaeIpTotal.T2GUBN = "6";

                        if (this.CBO01_GOKCR.GetValue().ToString() == "1") //본점
                        {
                            MaeIpTotal.T2SAUPNO = "6108110449";
                        }
                        else
                        {
                            MaeIpTotal.T2SAUPNO = "1058516181";
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

                    MaeIpTotal.FILLER = "                              "; // 30자리 공백 (2010년 4월 22일추가)

                    MaeIpTotal.T2SEQ = string.Format("{0:D4}", iCnt);

                    sData = MaeIpTotal.T2GUBN;           // 자료구분 : 1자리
                    sData += MaeIpTotal.T2SAUPNO;         // 제출자 사업자등록번호 : 10자리
                    // 합계분
                    sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaeIpTotal.T2SEQ)));       // 거래처수    :   7자리
                    sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaeIpTotal.T2CNT)));       // 계산서 매수 :   7자리
                    sData += string.Format("{0:D15}", Convert.ToInt64(Get_Numeric(MaeIpTotal.T2AMT)));      // 공급가액    :  15자리
                    sData += string.Format("{0:D14}", Convert.ToInt64(Get_Numeric(MaeIpTotal.T2VAT)));      // 세    액    :  14자리
                    // 사업자등록번호 수취분
                    sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaeIpTotal.T2SASEQ)));     // 거래처수    :   7자리
                    sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaeIpTotal.T2SACNT)));     // 계산서 매수 :   7자리
                    sData += string.Format("{0:D15}", Convert.ToInt64(Get_Numeric(MaeIpTotal.T2SAAMT)));    // 공급가액    :  15자리
                    sData += string.Format("{0:D14}", Convert.ToInt64(Get_Numeric(MaeIpTotal.T2SAVAT)));    // 세    액    :  14자리
                    // 주민등록번호 수취분												
                    sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaeIpTotal.T2JUSEQ)));     // 거래처수    :   7자리
                    sData += string.Format("{0:D7}", Convert.ToInt64(Get_Numeric(MaeIpTotal.T2JUCNT)));     // 계산서 매수 :   7자리
                    sData += string.Format("{0:D15}", Convert.ToInt64(Get_Numeric(MaeIpTotal.T2JUAMT)));    // 공급가액    :  15자리
                    sData += string.Format("{0:D14}", Convert.ToInt64(Get_Numeric(MaeIpTotal.T2JUVAT)));    // 세    액    :  14자리
                    sData += MaeIpTotal.FILLER;        //30자리

                    sw.WriteLine(sData);

                }

                sw.Close();
                ds.Dispose();
            }
            catch
            {
                
            }

            this.ShowMessage("TY_M_GB_25UAA711");
        }
        #endregion	

        #region Description : 음수 표시 알파벳
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

        #region Description :  부가세 정보 선언
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

        #region  Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();  
        }
        #endregion

    }
}
