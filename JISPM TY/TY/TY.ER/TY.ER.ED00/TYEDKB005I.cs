using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Text;
using Microsoft.VisualBasic;
using System.Xml;


namespace TY.ER.ED00
{
    /// <summary>
    /// 반출승인정보 수신/변환 관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.04.06 09:53
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  EDIREJBGB : 결과통보코드
    /// </summary>
    public partial class TYEDKB005I : TYBase
    {
        
        private string fsTEMP_COL1;

        private string fsEDIGJ;
        private string fsEDIHMNO1;
        private string fsEDIHMNO2;

        private string fsEDINO1;
        private string fsEDINO2;
        private string fsEDINO3;

        private string fsAUT0101;
        private string fsBGM0101;
        private string fsBGM0102;
        private string fsCTA0101;
        private string fsCST0101;
        private string fsCST0102;
        private string fsDTM0101;
        private string fsDTM0201;
        private string fsDTM0301;
        private string fsDTM0401;
        private string fsDMS0101;
        private string fsDOC0101;
        private string fsDOC0201;
        private string fsDOC0202;
        private string fsLOC0101;
        private string fsLOC0102;
        private string fsLOC0201;
        private string fsLOC0301;
        private string fsGIS0101;
        private string fsGIS0201;
        private string fsMOA0101;
        private string fsMOA0201;
        private string fsRFF0101;
        private string fsRFF0102;
        private string fsRFF0103;
        private string fsFTX0101;
        private string fsFTX0102;
        private string fsFTX0103;
        private string fsFTX0104;
        private string fsFTX0201;
        private string fsFTX0202;
        private string fsFTX0203;
        private string fsFTX0301;
        private string fsTDT0101;
        private string fsTDT0102;
        private string fsNAD0101;
        private string fsNAD0102;
        private string fsNAD0103;
        private string fsNAD0104;
        private string fsNAD02011;
        private string fsNAD02012;
        private string fsNAD0201;
        private string fsNAD0202;
        private string fsNAD0203;
        private string fsNAD0204;
        private string fsNAD0301;
        private string fsNAD0302;
        private string fsNAD0303;
        private string fsNAD0304;
        private string fsNAD0401;
        private string fsMEA0101;
        private string fsMEA0102;
        private string fsMEA0201;
        private string fsMEA0202;
        private string fsCNT0101;
        private string fsCNT0102;
        private string fsCNT0201;
        private string fsPAC0101;
        private string fsPAC0102;
        private string fsEQD0101;
        private string fsCOM0101;
        private string fsCOM0201;
        private string fsCOM0301;
        private string fsSEL0101;
        private string fsSEL0201;
        private string fsSEL0301;
        private string fsERP1101;
        private string fsERP1102;
        private string fsERP2101;
        private string fsERP2102;
        private string fsERP3101;
        private string fsERP3102;
        private string fsFTX1101;
        private string fsFTX1102;
        private string fsFTX2101;
        private string fsFTX2102;
        private string fsFTX3101;
        private string fsFTX3102;
        private string fsFTX1201;
        private string fsFTX2201;
        private string fsFTX3201;
        private string fsTOD0101;
        private string fsUNH0101;

        private int fiERRCNT;

        private string[] _NodeValueList;
        private string   _NodeString;

        private string fsFullXPath;
        

        //private string fsWinmatePath = "C:\\WINMATE";

        #region  Description : 폼 로드 이벤트
        public TYEDKB005I()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }

        private void TYEDKB005I_Load(object sender, System.EventArgs e)
        {           

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.BTN61_SAV.IsAsynchronous = true;

            LBL51_EDIREJBGB.Text = "반입예정";
            LBL52_EDIREJBGB.Text = "반출승인";
            LBL53_EDIREJBGB.Text = "접수통보";
            LBL54_EDIREJBGB.Text = "정정결과";
            LBL55_EDIREJBGB.Text = "오류통보";

            //변환내역 표시
            TXT01_EDIREJBGB.SetValue("0");
            TXT02_EDIREJBGB.SetValue("0");
            TXT03_EDIREJBGB.SetValue("0");
            TXT04_EDIREJBGB.SetValue("0");
            TXT05_EDIREJBGB.SetValue("0");

            pgBar.Visible = false;                        
        }
        #endregion

        #region  Description : 수신/변환 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            //try
            //{

            //    this.UP_TYEDI_CALL();

            //    string sDirDate = DateTime.Now.ToString("yyyyMMdd");
            //    string[] Getfiles = System.IO.Directory.GetFiles(TYBase.ConstKCSAPIPath + "\\" + sDirDate + "\\Rcv\\", "*.txt");

            //    foreach (string file in Getfiles)
            //    {
            //        //파일존재 체크
            //        if (System.IO.File.Exists(file))
            //        {
            //            TXT01_AFFILENAME.SetValue(file);

            //            //수신파일 변환
            //            if (TXT01_AFFILENAME.GetValue().ToString() != "")
            //            {
            //                this.UP_KCSAPI4XmlDocment();
            //            }
            //        }
            //    }

            //    this.UP_DirFileMove_KCSAPI4();

            //    //수신문서 목록 조회
            //    //this.UP_KCSAPI4Recive_Docment();

            //    ////WINMATE 수신
            //    //this.UP_WinmateRecive();

            //    //System.Threading.Thread.Sleep(3000);

            //    ////파일 읽기 및 변환
            //    //this.UP_Get_FileOpenRead();

            //    ////파일이동
            //    //this.UP_FileToMove();
            //}
            //catch (Exception ex)
            //{

            //    //파일이동
            //    this.UP_FileToMove();

            //    this.ShowMessage(ex.Message);
            //    return;
            //}
            //finally
            //{
            //    //파일이동
            //    this.UP_FileToMove();
            //}

            //this.ShowMessage("TY_M_UT_74CGE268");

        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            //변환내역 표시
            TXT01_EDIREJBGB.SetValue("0");
            TXT02_EDIREJBGB.SetValue("0");
            TXT03_EDIREJBGB.SetValue("0");
            TXT04_EDIREJBGB.SetValue("0");
            TXT05_EDIREJBGB.SetValue("0");

            //KCSAPI4 설치 여부 판단
            if (!System.IO.Directory.Exists(ConstKCSAPIPath) || !System.IO.File.Exists(ConstKCSAPIPath + "\\KCSAPI4.dll"))
            {
                this.ShowCustomMessage("KCSAPI4 모듈이 설치되어 있지않습니다! 전산실에 문의하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            //다운로드 경로에 파일 삭제
            string sFileName = string.Empty;
            string sDirDate = DateTime.Now.ToString("yyyyMMdd");

            if (System.IO.Directory.Exists(ConstKCSAPIPath + "\\" + sDirDate) == true)
            {
                string[] _DriList = System.IO.Directory.GetDirectories(ConstKCSAPIPath + "\\" + sDirDate);

                if (_DriList.Length > 0)
                {

                    string[] _FileList = System.IO.Directory.GetFiles(ConstKCSAPIPath + "\\" + sDirDate + "\\Rcv\\", "*.*");

                    if (_FileList.Length > 0)
                    {
                        for (int i = 0; i < _FileList.Length; i++)
                        {
                            sFileName = _FileList[i].ToString();

                            System.IO.File.Delete(sFileName);
                        }
                    }
                }
            }
            

            if (!this.ShowMessage("TY_M_UT_75I8G556"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : Winmate 수신 이벤트
        private void UP_WinmateRecive()
        {

            System.Diagnostics.Process ps = new System.Diagnostics.Process();

            ps.StartInfo.FileName = "hermes.exe"; // 프로그램 파일명            
            ps.StartInfo.Arguments = "-m3";  // 넘길 파라미터
            ps.StartInfo.UseShellExecute = true;
            ps.StartInfo.WorkingDirectory = Get_WinmdatePath() + "\\BIN\\"; //프로그램이 있는 디렉토리 위치
            ps.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            //실행에 필요한 설정 후 외부 프로그램을 실행.
            ps.Start();

            return;
        }
        #endregion

        #region  Description : 전송 모듈 호출 이벤트
        private void UP_KCSAPIModulCall()
        {
            
            ProcessStartInfo ps = new ProcessStartInfo();

            ps.FileName = "TY_KCSAPI.exe"; // 프로그램 파일명                            
            ps.UseShellExecute = false;
            ps.RedirectStandardOutput = true;
            ps.CreateNoWindow = true;
            ps.Arguments = "TY,TX,"+ Get_KCSAPI4LoginId()+","+ Get_KCSAPI4DocUserId();  // 회사구분, 호출구분(전송(RX),수신(TX))

            //ps.WorkingDirectory = ConstKCSAPIPath + "\\"; //프로그램이 있는 디렉토리 위치
            ps.WorkingDirectory = Application.StartupPath + "\\"; //프로그램이 있는 디렉토리 위치
            ps.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;

            using (Process process = Process.Start(ps))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string sArguments = reader.ReadToEnd();
                }
            }

            return;
        }
        #endregion       

        #region  Description : Winmate 폴더 오픈 및 파일 읽기
        private void UP_Get_FileOpenRead()
        {
            string[] Getfiles = System.IO.Directory.GetFiles(Get_WinmdatePath() + "\\IN", "*.*");

            if (Getfiles.Length > 0)
            {
                foreach (string file in Getfiles)
                {
                    //파일존재 체크
                    if (System.IO.File.Exists(file))
                    {
                        UP_DataReceiveToConvert(file);
                    }
                }
            }
            else
            {
                 this.ShowCustomMessage("수신된 파일이 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                 return;
            }
        }
        #endregion

        #region  Description : 변환 이벤트
        private void UP_DataReceiveToConvert(string sFileName)
        {
            string sRevDataA;
            string sRevDataB;
            string sRevDataC;

            string sRCVDAT;

            Int16 iLenRevA = 0;
            Int16 iLenRevB = 0;
            Int16 iLenRevC = 0;
            Int16 iLenRevD = 0;
            Int16 iCount = 0;
            int iSW = 0;

            int iStartIndex = 0;

            int i반입예정 = 0;
            int i반출승인 = 0;
            int i접수 = 0;
            int i정정결과 = 0;
            int i오류 = 0;

            bool gbFlag = false;

            TXT01_AFFILENAME.SetValue(sFileName);

            //수신파일 변환
            if (TXT01_AFFILENAME.GetValue().ToString() != "")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_74B9D224");
                this.DbConnector.ExecuteTranQuery();

                //파일 읽기
                StreamReader file = new StreamReader(TXT01_AFFILENAME.GetValue().ToString(), Encoding.Default);
                sRevDataA = file.ReadLine();
                file.Close();
                if (sRevDataA.Length > 0)
                {

                    this.DbConnector.CommandClear();

                    iLenRevA = 1;

                    for (; ; )
                    {

                        iLenRevA = Convert.ToInt16(sRevDataA.IndexOf("UNB", 0).ToString());
                        iLenRevB = Convert.ToInt16(sRevDataA.IndexOf("UNB", iLenRevA + 1).ToString());

                        if (iLenRevB <= 0)
                        {
                            sRevDataB = sRevDataA.Substring(iLenRevA, sRevDataA.Length);
                        }
                        else
                        {

                            sRevDataB = sRevDataA.Substring(iLenRevA, iLenRevB - iLenRevA);
                            sRevDataA = sRevDataA.Substring(iLenRevB, sRevDataA.Length - (iLenRevB - iLenRevA));
                        }

                        iLenRevC = Convert.ToInt16(sRevDataB.IndexOf("UNH", 0).ToString());
                        iLenRevD = Convert.ToInt16(sRevDataB.IndexOf("UNZ", iLenRevC + 1).ToString());

                        sRevDataB = sRevDataB.Substring(iLenRevC, iLenRevD - iLenRevC);


                        while (gbFlag == false)
                        {
                            iLenRevC = Convert.ToInt16(sRevDataB.IndexOf("'").ToString());
                            sRevDataC = sRevDataB.Substring(0, iLenRevC + 1);
                            sRevDataB = sRevDataB.Substring(iLenRevC + 1, sRevDataB.Length - (iLenRevC + 1));

                            iCount += 1;

                            this.DbConnector.Attach("TY_P_UT_74B9E225", iCount.ToString(), sRevDataC);

                            if (sRevDataB == "")
                            {
                                gbFlag = true;
                            }
                        }

                        if (iLenRevB <= 0)
                        {
                            gbFlag = true;
                            break;
                        }

                        gbFlag = false;
                    }

                    if (this.DbConnector.CommandCount > 0)
                    {
                        this.DbConnector.ExecuteTranQueryList();
                    }
                }
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_74B9M226");
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                iStartIndex = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sRCVDAT = dt.Rows[i]["RCVDAT"].ToString();

                    //문서 종류 정리
                    if (sRCVDAT.Substring(0, 3) == "BGM")
                    {
                        iSW = UP_Get_DocumentSelect(sRCVDAT);
                    }

                    if (sRCVDAT.Substring(0, 3) == "UNT")
                    {
                        switch (iSW)
                        {
                            case 0:
                                iStartIndex = i + 1;
                                break;
                            case 1:
                                iStartIndex = i + 1;
                                break;
                            case 2:
                                //반입예정정보
                                iStartIndex = i + 1;
                                i반입예정 += 1;
                                break;
                            case 3:
                                //반출승인내역
                                UP_Set_EDICUSCRA(dt, iStartIndex, i);
                                iStartIndex = i + 1;
                                i반출승인 += 1;
                                break;
                            case 4:
                                //접수통보
                                UP_Set_EDICUSRES(dt, iStartIndex, i);
                                iStartIndex = i + 1;
                                i접수 += 1;
                                break;
                            case 5:
                                //오류통보
                                UP_Set_EDICUSAPE(dt, iStartIndex, i);
                                iStartIndex = i + 1;
                                i오류 += 1;
                                break;
                            case 6:
                                iStartIndex = i + 1;
                                break;
                            case 7:
                                //반출기간연장 결과통보
                                UP_Set_EDICUSRES_5HN(dt, iStartIndex, i);
                                iStartIndex = i + 1;
                                i접수 += 1;
                                break;
                        }
                    }
                }
            }
            //변환내역 표시
            TXT01_EDIREJBGB.SetValue(i반입예정.ToString());
            TXT02_EDIREJBGB.SetValue(i반출승인.ToString());
            TXT03_EDIREJBGB.SetValue(i접수.ToString());
            TXT04_EDIREJBGB.SetValue(i정정결과.ToString());
            TXT05_EDIREJBGB.SetValue(i오류.ToString());
        }
        #endregion

        #region  Description :  접수 처리 이벤트
        private void UP_Set_EDICUSRES(DataTable dt, int iStart, int iEnd)
        {
            //예 
            string sRCVDAT = string.Empty;

            UP_Set_FiledClear();

            for (int i = iStart; i <= iEnd; i++)
            {
                sRCVDAT = dt.Rows[i]["RCVDAT"].ToString();

                switch (sRCVDAT.Substring(0, 3))
                {
                    case "UNH":

                        UP_Set_EDICUSRES_UNH(sRCVDAT);
                        break;
                    case "BGM":
                        UP_Set_EDICUSRES_BGM(sRCVDAT);
                        break;
                    case "NAD":
                        UP_Set_EDICUSRES_NAD(sRCVDAT);
                        break;
                    case "LOC":
                        UP_Set_EDICUSRES_LOC(sRCVDAT);
                        break;
                    case "DTM":
                        UP_Set_EDICUSRES_DTM(sRCVDAT);
                        break;
                    case "GIS":
                        UP_Set_EDICUSRES_GIS(sRCVDAT);
                        break;
                    case "FTX":
                        UP_Set_EDICUSRES_FTX(sRCVDAT);
                        break;
                    case "RFF":
                        UP_Set_EDICUSRES_RFF(sRCVDAT);
                        break;
                    case "DOC":
                        UP_Set_EDICUSRES_DOC(sRCVDAT);
                        break;
                    case "AUT":
                        UP_Set_EDICUSRES_AUT(sRCVDAT);
                        break;
                    case "UNT":
                        UP_Set_EDICUSRES_UNT(sRCVDAT);
                        break;
                }
            }
        }

        private void UP_Set_EDICUSRES_UNH(string sStr)
        {
            string sRevDataA = sStr;
            string sData = string.Empty;
            Int16 iLenRevA = 0;
            Int16 iLenRevB = 0;

            iLenRevA = Convert.ToInt16(sRevDataA.IndexOf("+", 0).ToString());

            iLenRevB = Convert.ToInt16(sRevDataA.IndexOf("+", iLenRevA + 1).ToString());

            sData = sRevDataA.Substring(iLenRevA + 1, iLenRevB - iLenRevA - 1);

            fsUNH0101 = sData;
        }

        //응답형태구분
        //제출번호
        private void UP_Set_EDICUSRES_BGM(string sStr)
        {
            string sRevDataA = sStr;
            string sData = string.Empty;
            Int16 iLenRevA = 0;
            Int16 iLenRevB = 0;

            iLenRevA = Convert.ToInt16(sRevDataA.IndexOf("+", 0).ToString());

            iLenRevB = Convert.ToInt16(sRevDataA.IndexOf("+", iLenRevA + 1).ToString());

            sData = sRevDataA.Substring(iLenRevB + 1, sRevDataA.Length - (iLenRevB + 1) - 1);

            fsBGM0101 = sData;
        }

        //정정처리담당자
        private void UP_Set_EDICUSRES_NAD(string sStr)
        {
            string sRevDataA = sStr;
            string sData = string.Empty;

            sData = sRevDataA.Substring(8, (sRevDataA.Length - 1) - 9);

            fsNAD0101 = sData;
        }

        //신고세관 및 과부호 기재
        private void UP_Set_EDICUSRES_LOC(string sStr)
        {
            string sRevDataA = sStr;

            fsLOC0101 = sRevDataA.Substring(7, 3);
            fsLOC0102 = sRevDataA.Substring(19, 2);
        }
        private void UP_Set_EDICUSRES_DTM(string sStr)
        {
            string sRevDataA = sStr;

            switch (sRevDataA.Substring(4, 3))
            {
                case "137":
                    fsDTM0101 = sRevDataA.Substring(8, 12);
                    break;
                case "148":
                    fsDTM0201 = sRevDataA.Substring(8, 12);
                    break;
                case "218":
                    fsDTM0301 = sRevDataA.Substring(8, 12);
                    break;
                case "187":
                    fsDTM0101 = sRevDataA.Substring(8, 12);
                    break;
                case "184":
                    fsDTM0201 = sRevDataA.Substring(8, 12);
                    break;
            }
        }
        private void UP_Set_EDICUSRES_GIS(string sStr)
        {
            fsGIS0101 = sStr.Substring(4, 1);
        }
        private void UP_Set_EDICUSRES_FTX(string sStr)
        {
            switch (sStr.Substring(4, 3))
            {
                case "AAI":
                    fsFTX0101 = sStr.Substring(10, ((sStr.Length - 1) - 11) + 1);
                    break;
                case "ACD":
                    fsFTX0201 = sStr.Substring(10, ((sStr.Length - 1) - 11) + 1);
                    break;
            }
        }
        private void UP_Set_EDICUSRES_RFF(string sStr)
        {
            string sRevDataA = sStr;
            string sData = string.Empty;
            Int16 iLenRevA = 0;

            iLenRevA = Convert.ToInt16(sRevDataA.IndexOf(":", 0).ToString());

            sData = sRevDataA.Substring(iLenRevA + 1, sRevDataA.Length - (iLenRevA + 1) - 1);

            fsRFF0101 = sData;
        }
        private void UP_Set_EDICUSRES_DOC(string sStr)
        {
            string sRevDataA = sStr;
            string sData = string.Empty;
            Int16 iLenRevA = 0;

            iLenRevA = Convert.ToInt16(sRevDataA.IndexOf("+", 0).ToString());

            sData = sRevDataA.Substring(iLenRevA + 1, sRevDataA.Length - (iLenRevA + 1) - 1);

            fsDOC0101 = sData;
        }
        private void UP_Set_EDICUSRES_AUT(string sStr)
        {
            string sRevDataA = sStr;
            string sData = string.Empty;
            Int16 iLenRevA = 0;

            iLenRevA = Convert.ToInt16(sRevDataA.IndexOf("+", 0).ToString());

            sData = sRevDataA.Substring(iLenRevA + 1, sRevDataA.Length - (iLenRevA + 1) - 1);

            fsAUT0101 = sData;
        }
        private void UP_Set_EDICUSRES_UNT(string sStr)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_74BE2227", fsUNH0101, fsBGM0101);
            this.DbConnector.ExecuteTranQuery();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_74BE5228", fsUNH0101,
                                                        fsBGM0101,
                                                        fsLOC0101,
                                                        fsLOC0102,
                                                        fsDTM0101,
                                                        fsDTM0201,
                                                        fsGIS0101,
                                                        fsRFF0101,
                                                        fsDOC0101,
                                                        fsAUT0101,
                                                        fsDTM0301,
                                                        TYUserInfo.EmpNo
                                                        );
            this.DbConnector.ExecuteTranQuery();

            switch (fsDOC0101)
            {
                case "632":
                    //세관 반입보고 누적  FILE
                    UP_Set_EDICUSRES_EDIIPGOF(fsBGM0101, "Y", "");
                    break;
                case "6NB":
                    //세관 반출보고 누적  FILE                    
                    UP_Set_EDICUSRES_EDICHULF(fsBGM0101, "Y", "");
                    break;
                case "5II":
                    //세관체화보고누적화일
                    //Call fSub_EDICUSRES_EDICHEWF
                    break;
                case "5LC":                    
                    //세관 반출입 정정신고 통보
                    UP_Set_EDICUSRES_EDIREIPCHF(fsBGM0101, "Y", "");
                    break;
                case "004":
                    //세관 반출입 정정신고 통보
                    UP_Set_EDICUSRES_EDIREIPCHF(fsBGM0101, "Y", "");
                    break;
                case "5LD":
                    //'세관 반출입 정정신고 통보
                    UP_Set_EDICUSRES_EDIREIPCHF(fsBGM0101, "Y", "");
                    break;
                case "005":
                    //'세관 반출입 정정신고 통보
                    UP_Set_EDICUSRES_EDIREIPCHF(fsBGM0101, "Y", "");
                    break;
                case "5LG":
                    //'세관 반출입 정정결과 통보
                    UP_Set_EDICUSRES_EDIREIPCHF(fsBGM0101, "Y", "");
                    break;
                case "5HA":
                    //'세관 내국반입 결과 통보
                    UP_Set_EDICUSRES_EDIHAIPGOF(fsBGM0101, "Y", "");
                    break;
                case "5HB":
                    //'세관 내국반출 결과 통보
                    UP_Set_EDICUSRES_EDIHBCHULF(fsBGM0101, "Y", "");
                    break;
                case "5HM":
                    //'세관 반출기간연장 통보                    
                    UP_Set_EDICUSRES_EDICHEXTENDF(fsBGM0101, "Y", "");
                    break;

            }

            UP_Set_FiledClear();
        }
        #endregion

        #region  Description :  오류 처리 이벤트
        private void UP_Set_EDICUSAPE(DataTable dt, int iStart, int iEnd)
        {
            string sRCVDAT = string.Empty;

            UP_Set_FiledClear();

            fiERRCNT = 0;

            for (int i = iStart; i <= iEnd; i++)
            {
                sRCVDAT = dt.Rows[i]["RCVDAT"].ToString();

                switch (sRCVDAT.Substring(0, 3))
                {
                    case "UNH":
                        UP_Set_EDICUSAPE_UNH(sRCVDAT);
                        break;
                    case "BGM":
                        UP_Set_EDICUSAPE_BGM(sRCVDAT);
                        break;
                    case "DTM":
                        UP_Set_EDICUSAPE_DTM(sRCVDAT);
                        break;
                    case "CNT":
                        UP_Set_EDICUSAPE_CNT(sRCVDAT);
                        break;
                    case "NAD":
                        UP_Set_EDICUSAPE_NAD(sRCVDAT);
                        break;
                    case "CTA":
                        UP_Set_EDICUSAPE_CTA(sRCVDAT);
                        break;
                    case "ERP":
                        UP_Set_EDICUSAPE_ERP(sRCVDAT);
                        break;
                    case "FTX":
                        UP_Set_EDICUSAPE_FTX(sRCVDAT);
                        break;
                    case "DOC":
                        UP_Set_EDICUSAPE_DOC(sRCVDAT);
                        break;
                    case "UNT":
                        UP_Set_EDICUSAPE_UNT(sRCVDAT);
                        break;
                }
            }
        }

        private void UP_Set_EDICUSAPE_UNH(string sStr)
        {

            //예) UNH+1+CUSAPE:S:93A:KE'

            string sRevDataA = sStr;
            string sData = string.Empty;
            Int16 iLenRevA = 0;
            Int16 iLenRevB = 0;

            iLenRevA = Convert.ToInt16(sRevDataA.IndexOf("+", 0).ToString());

            iLenRevB = Convert.ToInt16(sRevDataA.IndexOf("+", iLenRevA + 1).ToString());

            sData = sRevDataA.Substring(iLenRevA + 1, iLenRevB - iLenRevA - 1);

            fsUNH0101 = sData;
        }

        //응답형태구분
        //제출번호
        private void UP_Set_EDICUSAPE_BGM(string sStr)
        {
            //예) BGM+R20+110110551700002046'

            string sRevDataA = sStr;
            string sData = string.Empty;
            Int16 iLenRevA = 0;
            Int16 iLenRevB = 0;

            iLenRevA = Convert.ToInt16(sRevDataA.IndexOf("+", 0).ToString());

            iLenRevB = Convert.ToInt16(sRevDataA.IndexOf("+", iLenRevA + 1).ToString());

            sData = sRevDataA.Substring(iLenRevB + 1, sRevDataA.Length - (iLenRevB + 1) - 1);

            fsBGM0101 = sData;
        }

        private void UP_Set_EDICUSAPE_DTM(string sStr)
        {
            //예) DTM+137:20170322162430:204'
            //    DTM+148:20170322162430:204'


            string sRevDataA = sStr;

            switch (sRevDataA.Substring(4, 3))
            {
                case "137":
                    fsDTM0101 = sRevDataA.Substring(8, 12);
                    break;
                case "148":
                    fsDTM0201 = sRevDataA.Substring(8, 12);
                    break;
                case "218":
                    fsDTM0301 = sRevDataA.Substring(8, 12);
                    break;
                case "187":
                    fsDTM0101 = sRevDataA.Substring(8, 12);
                    break;
                case "184":
                    fsDTM0201 = sRevDataA.Substring(8, 12);
                    break;
            }
        }

        private void UP_Set_EDICUSAPE_CNT(string sStr)
        {

            string sRevDataA = sStr;
            string sData = string.Empty;
            Int16 iLenRevA = 0;

            iLenRevA = Convert.ToInt16(sRevDataA.IndexOf(":", 0).ToString());

            sData = sRevDataA.Substring(iLenRevA + 1, sRevDataA.Length - (iLenRevA + 1) - 1);

            fsCNT0101 = sData.Length == 0 ? "0" : sData;
        }

        //통보세관
        private void UP_Set_EDICUSAPE_NAD(string sStr)
        {
            //예) NAD+CM+110:113:KCS'

            string sData = string.Empty;

            sData = sStr.Substring(7, 3);

            fsNAD0101 = sData;
        }
        //과부호
        private void UP_Set_EDICUSAPE_CTA(string sStr)
        {
            //예) CTA++10'

            string sData = string.Empty;

            sData = sStr.Substring(5, 2);

            fsCTA0101 = sData;
        }

        //오류발생위치
        private void UP_Set_EDICUSAPE_ERP(string sStr)
        {
            //예) ERP+1'

            int iCnt = 0;

            for (int i = 0; i < sStr.Length - 1; i++)
            {
                if (sStr.Substring(i, 1) == "+")
                {
                    iCnt += 1;
                }

                if (iCnt == 2 && sStr.Substring(i, 1) != "+")
                {
                    fsTEMP_COL1 = fsTEMP_COL1 + sStr.Substring(i, 1);
                }
            }

            fiERRCNT += 1;

            switch (fiERRCNT)
            {
                case 1:
                    fsERP1101 = sStr.Substring(4, 1);
                    fsERP1102 = fsTEMP_COL1;
                    break;
                case 2:
                    fsERP2101 = sStr.Substring(4, 1);
                    fsERP2102 = fsTEMP_COL1;
                    break;
                case 3:
                    fsERP3101 = sStr.Substring(4, 1);
                    fsERP3102 = fsTEMP_COL1;
                    break;
            }
        }

        private void UP_Set_EDICUSAPE_FTX(string sStr)
        {
            //예) FTX+AAE+++110110551700002046'
            //    FTX+AAO+++해당 화물의 재고 내역이(가) 존재하지 않습니다.'

            string sTEMP1 = string.Empty;
            string sTEMP2 = string.Empty;

            string sRevDataA = sStr;
            string sData = string.Empty;
            Int16 iLenRevA = 0;
            Int16 iLenRevB = 0;

            iLenRevA = Convert.ToInt16(sRevDataA.IndexOf("AAE", 0).ToString());

            if (iLenRevA > 0)
            {
                iLenRevB = Convert.ToInt16(sRevDataA.IndexOf("+++", 0).ToString());

                sData = sRevDataA.Substring(iLenRevB + 3, sRevDataA.Length - (iLenRevB + 2) - 1);

                switch (fiERRCNT)
                {
                    case 1:
                        fsFTX1101 = sData;
                        fsFTX1102 = "";
                        break;
                    case 2:
                        fsFTX2101 = sData;
                        fsFTX2102 = "";
                        break;
                    case 3:
                        fsFTX3101 = sData;
                        fsFTX3102 = "";
                        break;
                }
            }

            iLenRevA = Convert.ToInt16(sRevDataA.IndexOf("AAO", 0).ToString());

            if (iLenRevA > 0)
            {
                iLenRevB = Convert.ToInt16(sRevDataA.IndexOf("+++", 0).ToString());

                sData = sRevDataA.Substring(iLenRevB + 3, sRevDataA.Length - (iLenRevB + 2) - 1);

                switch (fiERRCNT)
                {
                    case 1:
                        fsFTX1201 = sData;
                        break;
                    case 2:
                        fsFTX2201 = sData;
                        break;
                    case 3:
                        fsFTX3201 = sData;
                        break;
                }
            }

        }

        private void UP_Set_EDICUSAPE_DOC(string sStr)
        {
            string sRevDataA = sStr;
            string sData = string.Empty;
            Int16 iLenRevA = 0;

            iLenRevA = Convert.ToInt16(sRevDataA.IndexOf("+", 0).ToString());

            sData = sRevDataA.Substring(iLenRevA + 1, sRevDataA.Length - (iLenRevA + 1) - 1);

            fsDOC0101 = sData;
        }

        private void UP_Set_EDICUSAPE_UNT(string sStr)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_74C8O260", fsUNH0101, fsBGM0101);
            this.DbConnector.ExecuteTranQuery();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_74C8P261", fsUNH0101,
                                                        fsBGM0101,
                                                        fsDTM0101,
                                                        fsDTM0201,
                                                        fsNAD0101,
                                                        fsCTA0101,
                                                        fiERRCNT.ToString(),
                                                        fsERP1101,
                                                        fsERP1102,
                                                        fsFTX1101,
                                                        fsFTX1102,
                                                        fsFTX1201,
                                                        fsERP2101,
                                                        fsERP2102,
                                                        fsFTX2101,
                                                        fsFTX2102,
                                                        fsFTX2201,
                                                        fsERP3101,
                                                        fsERP3102,
                                                        fsFTX3101,
                                                        fsFTX3102,
                                                        fsFTX3201,
                                                        fsDOC0101,
                                                        TYUserInfo.EmpNo
                                                        );
            this.DbConnector.ExecuteTranQuery();

            switch (fsDOC0101)
            {
                case "632":
                    //세관 반입보고 누적  FILE
                    UP_Set_EDICUSRES_EDIIPGOF(fsBGM0101, "E", fsFTX1201);
                    break;
                case "6NB":
                    //세관 반출보고 누적  FILE                    
                    UP_Set_EDICUSRES_EDICHULF(fsBGM0101, "E", fsFTX1201);
                    break;
                case "5II":
                    //세관체화보고누적화일
                    //Call fSub_EDICUSRES_EDICHEWF
                    break;
                case "5LC":
                    //세관 반출입 정정신고 통보
                    UP_Set_EDICUSRES_EDIREIPCHF(fsBGM0101, "E", fsFTX1201);
                    break;
                case "5LD":
                    //'세관 반출입 정정신고 통보
                    UP_Set_EDICUSRES_EDIREIPCHF(fsBGM0101, "E", fsFTX1201);
                    break;
                case "5HA":
                    //'세관 내국반입 결과 통보
                    UP_Set_EDICUSRES_EDIHAIPGOF(fsBGM0101, "E", fsFTX1201);
                    break;
                case "5HB":
                    //'세관 내국반출 결과 통보
                    UP_Set_EDICUSRES_EDIHBCHULF(fsBGM0101, "E", fsFTX1201);
                    break;
                case "5HM":
                    //'세관 반출기간연장 결과 통보
                    UP_Set_EDICUSRES_EDICHEXTENDF(fsBGM0101, "E", fsFTX1201);
                    break;
            }

            UP_Set_FiledClear();
        }
        #endregion

        #region  Description :  반출승인내역 처리 이벤트
        private void UP_Set_EDICUSCRA(DataTable dt, int iStart, int iEnd)
        {
            string sRCVDAT = string.Empty;

            UP_Set_FiledClear();

            fiERRCNT = 0;

            for (int i = iStart; i <= iEnd; i++)
            {
                sRCVDAT = dt.Rows[i]["RCVDAT"].ToString();

                switch (sRCVDAT.Substring(0, 3))
                {
                    case "UNH":
                        UP_Set_EDICUSCRA_UNH(sRCVDAT);
                        break;
                    case "BGM":
                        UP_Set_EDICUSCRA_BGM(sRCVDAT);
                        break;
                    case "DTM":
                        UP_Set_EDICUSCRA_DTM(sRCVDAT);
                        break;
                    case "LOC":
                        UP_Set_EDICUSCRA_LOC(sRCVDAT);
                        break;
                    case "GIS":
                        UP_Set_EDICUSCRA_GIS(sRCVDAT);
                        break;
                    case "DOC":
                        UP_Set_EDICUSCRA_DOC(sRCVDAT);
                        break;
                    case "MOA":
                        UP_Set_EDICUSCRA_MOA(sRCVDAT);
                        break;
                    case "RFF":
                        UP_Set_EDICUSCRA_RFF(sRCVDAT);
                        break;
                    case "TOD":
                        UP_Set_EDICUSCRA_TOD(sRCVDAT);
                        break;
                    case "DMS":
                        UP_Set_EDICUSCRA_DMS(sRCVDAT);
                        break;
                    case "TDT":
                        UP_Set_EDICUSCRA_TDT(sRCVDAT);
                        break;
                    case "NAD":
                        UP_Set_EDICUSCRA_NAD(sRCVDAT);
                        break;
                    case "CST":
                        UP_Set_EDICUSCRA_CST(sRCVDAT);
                        break;
                    case "FTX":
                        UP_Set_EDICUSCRA_FTX(sRCVDAT);
                        break;
                    //case "MEA":
                    //    UP_Set_EDICUSCRA_MEA(sRCVDAT);
                    //    break;
                    case "CNT":
                        UP_Set_EDICUSCRA_CNT(sRCVDAT);
                        break;
                    case "UNT":
                        UP_Set_EDICUSCRA_UNT(sRCVDAT);
                        break;
                }
            }
        }

        private void UP_Set_EDICUSCRA_UNH(string sStr)
        {
            string sRevDataA = sStr;
            string sData = string.Empty;
            Int16 iLenRevA = 0;
            Int16 iLenRevB = 0;

            iLenRevA = Convert.ToInt16(sRevDataA.IndexOf("+", 0).ToString());

            iLenRevB = Convert.ToInt16(sRevDataA.IndexOf("+", iLenRevA + 1).ToString());

            sData = sRevDataA.Substring(iLenRevA + 1, iLenRevB - iLenRevA - 1);

            fsUNH0101 = sData;
        }

        //응답형태구분
        //제출번호
        private void UP_Set_EDICUSCRA_BGM(string sStr)
        {
            string sRevDataA = sStr;
            string sData = string.Empty;
            Int16 iLenRevA = 0;
            Int16 iLenRevB = 0;
            Int16 iLenRevC = 0;

            iLenRevA = Convert.ToInt16(sRevDataA.IndexOf("+", 0).ToString());

            iLenRevB = Convert.ToInt16(sRevDataA.IndexOf("+", iLenRevA + 1).ToString());

            iLenRevC = Convert.ToInt16(sRevDataA.IndexOf("+", iLenRevB + 1).ToString());

            sData = sRevDataA.Substring(iLenRevB + 1, iLenRevC - (iLenRevB + 1));

            fsBGM0101 = sData;

            iLenRevC = Convert.ToInt16(sRevDataA.IndexOf("+", iLenRevB + 1).ToString());

            sData = sRevDataA.Substring(iLenRevC + 1, sRevDataA.Length - (iLenRevC + 1) - 1);

            fsBGM0102 = sData;
        }

        private void UP_Set_EDICUSCRA_DTM(string sStr)
        {
            string sRevDataA = sStr;

            switch (sRevDataA.Substring(4, 3))
            {
                case "137":
                    fsDTM0101 = sRevDataA.Substring(8, 12);
                    break;
                case "204":
                    fsDTM0201 = sRevDataA.Substring(8, 8);
                    break;
                case "178":
                    fsDTM0301 = sRevDataA.Substring(8, 8);
                    break;
            }
        }

        private void UP_Set_EDICUSCRA_LOC(string sStr)
        {
            //예) LOC+14+11011055:129:KCS'            
            string sRevDataA = sStr;
            string sData = string.Empty;
            Int16 iLenRevA = 0;
            Int16 iLenRevB = 0;
            Int16 iLenRevC = 0;

            iLenRevA = Convert.ToInt16(sRevDataA.IndexOf("+", 0).ToString());

            iLenRevB = Convert.ToInt16(sRevDataA.IndexOf("+", iLenRevA + 1).ToString());

            iLenRevC = Convert.ToInt16(sRevDataA.IndexOf(":", iLenRevB + 1).ToString());

            sData = sRevDataA.Substring(iLenRevB + 1, iLenRevC - (iLenRevB + 1));

            fsLOC0101 = sData;
        }

        private void UP_Set_EDICUSCRA_GIS(string sStr)
        {
            //예) GIS+50:148:KCS'
            string sRevDataA = sStr;
            string sData = string.Empty;

            if (sRevDataA.Substring(7, 3) == "148")
            {
                sData = sRevDataA.Substring(4, 2);
                fsGIS0101 = sData;
            }
            else if (sRevDataA.Substring(7, 3) == "5EA")
            {
                sRevDataA.Substring(4, 1);
                fsGIS0201 = sData;
            }
        }

        //화물관리번호
        private void UP_Set_EDICUSCRA_DOC(string sStr)
        {
            //예) DOC+85+17POSLU009I'
            //    DOC+705+0001::0001'
            string sRevDataA = sStr;
            string sData = string.Empty;
            Int16 iLenRevA = 0;
            Int16 iLenRevB = 0;
            Int16 iLenRevC = 0;

            iLenRevA = Convert.ToInt16(sRevDataA.IndexOf("85", 0).ToString());

            if (iLenRevA > 0)
            {
                //적하목록번호
                iLenRevA = Convert.ToInt16(sRevDataA.IndexOf("+", 0).ToString());

                iLenRevB = Convert.ToInt16(sRevDataA.IndexOf("+", iLenRevA + 1).ToString());

                sData = sRevDataA.Substring(iLenRevB + 1, sRevDataA.Length - (iLenRevB + 1) - 1);

                fsDOC0101 = sData;
            }


            iLenRevA = Convert.ToInt16(sRevDataA.IndexOf("705", 0).ToString());

            if (iLenRevA > 0)
            {
                //MSN
                iLenRevA = Convert.ToInt16(sRevDataA.IndexOf("+", 0).ToString());

                iLenRevB = Convert.ToInt16(sRevDataA.IndexOf("+", iLenRevA + 1).ToString());

                iLenRevC = Convert.ToInt16(sRevDataA.IndexOf(":", iLenRevB + 1).ToString());

                if (iLenRevC > 0) //hsn이 있는경우
                {
                    sData = sRevDataA.Substring(iLenRevB + 1, iLenRevC - iLenRevB -1);
                    fsDOC0201 = sData;

                    //HSN
                    iLenRevA = Convert.ToInt16(sRevDataA.IndexOf(":", 0).ToString());

                    iLenRevB = Convert.ToInt16(sRevDataA.IndexOf(":", iLenRevA + 1).ToString());

                    sData = sRevDataA.Substring(iLenRevB + 1, sRevDataA.Length - (iLenRevB + 1) - 1);

                    fsDOC0202 = sData;
                }
                else
                {
                    sData = sRevDataA.Substring(iLenRevB + 1, sRevDataA.Length - (iLenRevB + 1) - 1);
                    fsDOC0201 = sData;
                }
            }
        }

        //관세액
        private void UP_Set_EDICUSCRA_MOA(string sStr)
        {
            //예) MOA+55:0:KRW'
            //    MOA+43:882415333:KRW' 
            string sRevDataA = sStr;
            string sData = string.Empty;
            Int16 iLenRevA = 0;
            Int16 iLenRevB = 0;

            iLenRevA = Convert.ToInt16(sRevDataA.IndexOf("55", 0).ToString());

            if (iLenRevA > 0)
            {
                //관세액
                iLenRevA = Convert.ToInt16(sRevDataA.IndexOf(":", 0).ToString());

                iLenRevB = Convert.ToInt16(sRevDataA.IndexOf(":", iLenRevA + 1).ToString());

                sData = sRevDataA.Substring(iLenRevA + 1, iLenRevB - (iLenRevA + 1));

                fsMOA0101 = sData;
            }


            iLenRevA = Convert.ToInt16(sRevDataA.IndexOf("43", 0).ToString());

            if (iLenRevA > 0)
            {
                //관세가격합계
                iLenRevA = Convert.ToInt16(sRevDataA.IndexOf(":", 0).ToString());

                iLenRevB = Convert.ToInt16(sRevDataA.IndexOf(":", iLenRevA + 1).ToString());

                sData = sRevDataA.Substring(iLenRevA + 1, iLenRevB - (iLenRevA + 1));

                fsMOA0201 = sData;
            }

        }

        //장치확인번호
        private void UP_Set_EDICUSCRA_RFF(string sStr)
        {
            //예) RFF+CKN:17D0000080'

            string sRevDataA = sStr;
            string sData = string.Empty;
            Int16 iLenRevA = 0;

            iLenRevA = Convert.ToInt16(sRevDataA.IndexOf(":", 0).ToString());

            if (iLenRevA > 0)
            {
                sData = sRevDataA.Substring(iLenRevA + 1, sRevDataA.Length - (iLenRevA + 1) - 1);    
            }            

            fsRFF0101 = sData;
        }

        //인도조건
        private void UP_Set_EDICUSCRA_TOD(string sStr)
        {
            //예) TOD+++CFR'

            string sRevDataA = sStr;
            string sData = string.Empty;

            sData = sRevDataA.Substring(6, 3);

            fsTOD0101 = sData;
        }

        //BL번호
        private void UP_Set_EDICUSCRA_DMS(string sStr)
        {
            //예) DMS+TEM1701201+705'

            string sRevDataA = sStr;
            string sData = string.Empty;
            Int16 iLenRevA = 0;
            Int16 iLenRevB = 0;

            iLenRevA = Convert.ToInt16(sRevDataA.IndexOf("+", 0).ToString());

            iLenRevB = Convert.ToInt16(sRevDataA.IndexOf("+", iLenRevA + 1).ToString());

            sData = sRevDataA.Substring(iLenRevA + 1, iLenRevB - (iLenRevA + 1));

            fsDMS0101 = sData;
        }

        //선명
        private void UP_Set_EDICUSCRA_TDT(string sStr)
        {
            //예) TDT+20++++:::CHRYSSA K'

            string sRevDataA = sStr;
            string sData = string.Empty;
            Int16 iLenRevA = 0;

            iLenRevA = Convert.ToInt16(sRevDataA.IndexOf(":::", 0).ToString());

            sData = sRevDataA.Substring(iLenRevA + 3, sRevDataA.Length - (iLenRevA + 3) - 1);

            fsTDT0101 = sData;
        }

        //납세의무자 - 상호 및 사업자
        private void UP_Set_EDICUSCRA_NAD(string sStr)
        {
            //예) NAD+PR+5088505374:58:KTX+(주)농협사료경북지사+이진홍'

            string sRevDataA = sStr;
            string sData = string.Empty;
            Int16 iLenRevA = 0;
            Int16 iLenRevB = 0;
            Int16 iLenRevC = 0;
            Int16 iLenRevD = 0;

            iLenRevA = Convert.ToInt16(sRevDataA.IndexOf("+", 0).ToString());

            iLenRevB = Convert.ToInt16(sRevDataA.IndexOf("+", iLenRevA + 1).ToString());

            iLenRevC = Convert.ToInt16(sRevDataA.IndexOf(":", iLenRevB + 1).ToString());

            //사업자번호
            sData = sRevDataA.Substring(iLenRevB + 1, iLenRevC - (iLenRevB + 1));
            fsNAD0101 = sData;

            //사업자구분
            iLenRevA = Convert.ToInt16(sRevDataA.IndexOf(":", 0).ToString());
            iLenRevB = Convert.ToInt16(sRevDataA.IndexOf(":", iLenRevA + 1).ToString());
            sData = sRevDataA.Substring(iLenRevA + 1, iLenRevB - (iLenRevA + 1));
            fsNAD0102 = sData;

            //상호
            iLenRevA = Convert.ToInt16(sRevDataA.IndexOf("+", 0).ToString());

            iLenRevB = Convert.ToInt16(sRevDataA.IndexOf("+", iLenRevA + 1).ToString());

            iLenRevC = Convert.ToInt16(sRevDataA.IndexOf("+", iLenRevB + 1).ToString());

            iLenRevD = Convert.ToInt16(sRevDataA.IndexOf("+", iLenRevC + 1).ToString());

            sData = sRevDataA.Substring(iLenRevC + 1, iLenRevD - (iLenRevC + 1));


            //fsNAD0103 = StringTransfer(sData, 20);

            fsNAD0103 = sData;

            //대표자
            sData = sRevDataA.Substring(iLenRevD + 1, sRevDataA.Length - (iLenRevD + 1) - 1);
            fsNAD0104 =   StringTransfer(sData.Replace(",","").Trim(),  8);

        }

        //란번호
        //보류/수리구분 
        private void UP_Set_EDICUSCRA_CST(string sStr)
        {
            string sRevDataA = sStr;
            string sData = string.Empty;
            Int16 iLenRevA = 0;
            Int16 iLenRevB = 0;

            iLenRevA = Convert.ToInt16(sRevDataA.IndexOf("+", 0).ToString());

            iLenRevB = Convert.ToInt16(sRevDataA.IndexOf("+", iLenRevA + 1).ToString());

            sData = sRevDataA.Substring(iLenRevA + 1, iLenRevB - iLenRevA);

            fsCST0101 = sData.Length == 0 ? "0" : sData;

            sData = sRevDataA.Substring(iLenRevB + 1, sRevDataA.Length - (iLenRevB + 1) - 1);

            fsCST0102 = sData;
        }

        //품명 및 규격
        private void UP_Set_EDICUSCRA_FTX(string sStr)
        {
            string sRevDataA = sStr;
            string sData1 = string.Empty;
            string sData2 = string.Empty;

            Int16 iLenRevA = 0;
            Int16 iLenRevB = 0;
            Int16 iLenRevC = 0;
            Int16 iLenRevD = 0;
            Int16 iLenRevE = 0;

            iLenRevA = Convert.ToInt16(sRevDataA.IndexOf("+", 0).ToString());

            iLenRevB = Convert.ToInt16(sRevDataA.IndexOf("+", iLenRevA + 1).ToString());

            iLenRevC = Convert.ToInt16(sRevDataA.IndexOf("+", iLenRevB + 1).ToString());

            iLenRevD = Convert.ToInt16(sRevDataA.IndexOf("+", iLenRevC + 1).ToString());

            iLenRevE = Convert.ToInt16(sRevDataA.IndexOf(":", 0).ToString());

            sData1 = sRevDataA.Substring(iLenRevD + 1, iLenRevE - iLenRevD);

            sData2 = sRevDataA.Substring(iLenRevD + 1, sRevDataA.Length - (iLenRevD + 1) - 1);

            iLenRevA = Convert.ToInt16(sRevDataA.IndexOf("AAA", 0).ToString());

            if (iLenRevA > 0)
            {
                fsFTX0101 = sData1;
                fsFTX0102 = sData1;
            }
        }

        private void UP_Set_EDICUSCRA_MEA(string sStr)
        {

        }

        //반출승인갯수
        private void UP_Set_EDICUSCRA_CNT(string sStr)
        {
            //예) CNT+11:0:VR'
            //    CNT+7:4000000:KG'

            string sRevDataA = sStr;
            string sData = string.Empty;
            Int16 iLenRevA = 0;
            Int16 iLenRevB = 0;

            iLenRevA = Convert.ToInt16(sRevDataA.IndexOf("11", 0).ToString());

            if (iLenRevA > 0)
            {

                iLenRevA = Convert.ToInt16(sRevDataA.IndexOf(":", 0).ToString());

                iLenRevB = Convert.ToInt16(sRevDataA.IndexOf(":", iLenRevA + 1).ToString());

                sData = sRevDataA.Substring(iLenRevA + 1, iLenRevB - (iLenRevA + 1));

                fsCNT0101 = sData;

                sData = sRevDataA.Substring(iLenRevB + 1, sRevDataA.Length - (iLenRevB + 1) - 1);

                fsCNT0102 = sData;
            }

            iLenRevA = Convert.ToInt16(sRevDataA.IndexOf("7", 0).ToString());

            if (iLenRevA > 0)
            {

                iLenRevA = Convert.ToInt16(sRevDataA.IndexOf(":", 0).ToString());

                iLenRevB = Convert.ToInt16(sRevDataA.IndexOf(":", iLenRevA + 1).ToString());

                sData = sRevDataA.Substring(iLenRevA + 1, iLenRevB - (iLenRevA + 1));

                fsCNT0201 = sData.Length == 0 ? "0" : sData;
            }

        }

        private void UP_Set_EDICUSCRA_UNT(string sStr)
        {
            string sEDIJUKHA = string.Empty;
            string sEDIBLMSN = string.Empty;
            string sEDIBLHSN = string.Empty;
            string sCRANEW = string.Empty;

            try
            {
                fsEDIHMNO2 = "";

                //장치장 구분 찾기
                fsEDINO1 = fsLOC0101;
                fsEDIHMNO1 = "20";
                fsEDINO2 = "20";
                fsEDINO3 = "";

                if (fsRFF0101.Trim() != "")
                {
                    fsEDIHMNO1 = fsEDIHMNO1 + fsRFF0101.Substring(0, 2);
                    fsEDINO2 = fsEDINO2 + fsRFF0101.Substring(0, 2);
                }

                if (fsRFF0101.Length >= 10)
                {
                    fsEDIHMNO2 = fsRFF0101.Substring(2, 8);
                }

                if (fsEDIHMNO2.Length == 0)
                {
                    fsEDIHMNO2 = "0";
                }
                if (fsRFF0101.Length >= 10)
                {
                    fsEDINO3 = fsRFF0101.Substring(2, 8);
                }

                sEDIJUKHA = fsDOC0101;
                sEDIBLMSN = fsDOC0201;
                sEDIBLHSN = fsDOC0202 == "" ? "0" : fsDOC0202;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_74CBT265", sEDIJUKHA,
                                                            sEDIBLMSN,
                                                            fsEDINO1,
                                                            fsEDINO2,
                                                            fsEDINO3
                                                           );
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    sCRANEW = "N";
                    fsEDIGJ = dt.Rows[0]["EDIGJ"].ToString();
                }
                else
                {
                    //반입보고서에 없으면 적하목록, MSN 으로 입고관리를 찾는다
                    //SILO
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_74CF4266", sEDIJUKHA,
                                                                sEDIBLMSN,
                                                                sEDIBLHSN
                                                               );
                    DataTable dsilo = this.DbConnector.ExecuteDataTable();
                    if (dsilo.Rows.Count > 0)
                    {
                        fsEDIGJ = "S";
                    }
                    else
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_74CFB267", sEDIJUKHA,
                                                                    sEDIBLMSN,
                                                                    sEDIBLHSN
                                                                   );
                        DataTable duTT = this.DbConnector.ExecuteDataTable();
                        if (duTT.Rows.Count > 0)
                        {
                            fsEDIGJ = "T";
                            if (fsEDIHMNO2 == "0")
                            {
                                fsEDIHMNO1 = duTT.Rows[0]["IPSINOYY"].ToString();
                                fsEDIHMNO2 = duTT.Rows[0]["IPSINO"].ToString();
                            }
                        }
                        else
                        {
                            fsEDIGJ = "S";
                        }
                    }
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_74CBL263", fsBGM0101);
                this.DbConnector.ExecuteTranQuery();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_74CBO264",   fsEDIGJ,
                                                              fsEDIHMNO1,
                                                              fsEDIHMNO2.Replace("D", ""),
                                                              fsUNH0101,
                                                              fsBGM0101,
                                                              fsBGM0102,
                                                              fsDTM0101,
                                                              fsDTM0201,
                                                              fsLOC0101,
                                                              fsGIS0101,
                                                              fsGIS0201,
                                                              fsDOC0101,
                                                              fsDOC0201,
                                                              fsDOC0202,
                                                              fsMOA0101,
                                                              fsRFF0101,
                                                              fsTOD0101,
                                                              fsMOA0201,
                                                              fsDMS0101,
                                                              fsTDT0101,
                                                              fsDTM0301,
                                                              fsNAD0101,
                                                              fsNAD0102,
                                                              fsNAD0103,
                                                              fsNAD0104,
                                                              fsCST0101,
                                                              fsCST0102,
                                                              fsFTX0101,
                                                              fsFTX0102,
                                                              fsMEA0101,
                                                              fsMEA0102,
                                                              fsMEA0201,
                                                              fsMEA0202,
                                                              fsCNT0101,
                                                              fsCNT0102,
                                                              fsCNT0201,
                                                              sCRANEW,
                                                              TYUserInfo.EmpNo,
                                                              ""
                                                            );
                this.DbConnector.ExecuteTranQuery();

                //silo 통관분일경우 구 edi 테이블에 넣어준다
                if (fsEDIGJ == "S")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_81AAS426", fsBGM0101);
                    this.DbConnector.ExecuteNonQuery();

                    fsNAD0103 = StringTransfer(fsNAD0103, 20);

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_818LS416", fsEDIGJ,
                                                                  fsEDIHMNO1,
                                                                  fsEDIHMNO2.Replace("D", ""),
                                                                  fsUNH0101,
                                                                  fsBGM0101,
                                                                  fsBGM0102,
                                                                  fsDTM0101,
                                                                  fsDTM0201,
                                                                  fsLOC0101,
                                                                  fsGIS0101,
                                                                  fsGIS0201,
                                                                  fsDOC0101,
                                                                  fsDOC0201,
                                                                  fsDOC0202,
                                                                  fsMOA0101,
                                                                  fsRFF0101,
                                                                  fsTOD0101,
                                                                  fsMOA0201,
                                                                  fsDMS0101,
                                                                  fsTDT0101,
                                                                  fsDTM0301,
                                                                  fsNAD0101,
                                                                  fsNAD0102,
                                                                  fsNAD0103,
                                                                  fsNAD0104,
                                                                  fsCST0101,
                                                                  fsCST0102,
                                                                  fsFTX0101,
                                                                  fsFTX0102,
                                                                  fsMEA0101,
                                                                  fsMEA0102,
                                                                  fsMEA0201,
                                                                  fsMEA0202,
                                                                  fsCNT0101,
                                                                  fsCNT0102,
                                                                  fsCNT0201,
                                                                  sCRANEW,
                                                                  TYUserInfo.EmpNo,
                                                                  ""
                                                                );
                    this.DbConnector.ExecuteNonQuery();
                }
            }           
            finally
            {                
            }
        }
        #endregion

        #region  Description :  반출기간연장 결과통보 처리 이벤트
        private void UP_Set_EDICUSRES_5HN(DataTable dt, int iStart, int iEnd)
        {
            //예 
            string sRCVDAT = string.Empty;

            UP_Set_FiledClear();

            for (int i = iStart; i <= iEnd; i++)
            {
                sRCVDAT = dt.Rows[i]["RCVDAT"].ToString();

                switch (sRCVDAT.Substring(0, 3))
                {
                    case "UNH":
                        UP_Set_EDICUSRES5HN_UNH(sRCVDAT);
                        break;
                    case "BGM":
                        UP_Set_EDICUSRES5HN_BGM(sRCVDAT);
                        break;
                    case "DTM":
                        UP_Set_EDICUSRES5HN_DTM(sRCVDAT);
                        break;
                    case "GIS":
                        UP_Set_EDICUSRES5HN_GIS(sRCVDAT);
                        break;
                    case "FTX":
                        UP_Set_EDICUSRES5HN_FTX(sRCVDAT);
                        break;
                    case "RFF":
                        UP_Set_EDICUSRES5HN_RFF(sRCVDAT);
                        break;
                    case "UNT":
                        UP_Set_EDICUSRES5HN_UNT(sRCVDAT);
                        break;
                }
            }
        }

        private void UP_Set_EDICUSRES5HN_UNH(string sStr)
        {
            string sRevDataA = sStr;
            string sData = string.Empty;
            Int16 iLenRevA = 0;
            Int16 iLenRevB = 0;

            iLenRevA = Convert.ToInt16(sRevDataA.IndexOf("+", 0).ToString());

            iLenRevB = Convert.ToInt16(sRevDataA.IndexOf("+", iLenRevA + 1).ToString());

            sData = sRevDataA.Substring(iLenRevA + 1, iLenRevB - iLenRevA - 1);

            fsUNH0101 = sData;
        }

        //응답형태구분
        //제출번호
        private void UP_Set_EDICUSRES5HN_BGM(string sStr)
        {
            string sRevDataA = sStr;
            string sData = string.Empty;
            Int16 iLenRevA = 0;
            Int16 iLenRevB = 0;

            iLenRevA = Convert.ToInt16(sRevDataA.IndexOf("+", 0).ToString());

            iLenRevB = Convert.ToInt16(sRevDataA.IndexOf("+", iLenRevA + 1).ToString());

            sData = sRevDataA.Substring(iLenRevB + 1, sRevDataA.Length - (iLenRevB + 1) - 1);

            fsBGM0101 = sData;
        }

        private void UP_Set_EDICUSRES5HN_DTM(string sStr)
        {
            //승인일자, 통보일시, 연장기간시작일,  연장기간종료일
            string sRevDataA = sStr;

            switch (sRevDataA.Substring(4, 3))
            {
                case "504":
                    fsDTM0301 = sRevDataA.Substring(8, 8);  //연장기간시작일
                    break;
                case "505":
                    fsDTM0401 = sRevDataA.Substring(8, 8);  //연장기간종료일
                    break;
                case "187":
                    fsDTM0101 = sRevDataA.Substring(8, 8);  //승인일자
                    break;
                case "184":
                    fsDTM0201 = sRevDataA.Substring(8, 12);  //통보일시
                    break;
            }
        }
        private void UP_Set_EDICUSRES5HN_GIS(string sStr)
        {
            //연장처리구분

            fsGIS0101 = sStr.Substring(4, 1);

        }
        private void UP_Set_EDICUSRES5HN_FTX(string sStr)
        {

            switch (sStr.Substring(4, 3))
            {
                case "ACD":
                    fsFTX0201 = sStr.Substring(10, ((sStr.Length - 1) - 11) + 1);  //승인거부사유
                    break;
            }
        }
        private void UP_Set_EDICUSRES5HN_RFF(string sStr)
        {
            //기간연장승인번호

            string sRevDataA = sStr;
            string sData = string.Empty;
            Int16 iLenRevA = 0;

            iLenRevA = Convert.ToInt16(sRevDataA.IndexOf(":", 0).ToString());

            sData = sRevDataA.Substring(iLenRevA + 1, sRevDataA.Length - (iLenRevA + 1) - 1);

            fsRFF0101 = sData;
        }

        private void UP_Set_EDICUSRES5HN_UNT(string sStr)
        {
            if (fsGIS0101 == "1")
            {
                //승인
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_868FM194", "Y", "", TYUserInfo.EmpNo, "T", fsBGM0101);
                this.DbConnector.ExecuteTranQuery();
            }
            else
            {
                //거부
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_868FM194", "E", "", TYUserInfo.EmpNo, "T", fsBGM0101);
                this.DbConnector.ExecuteTranQuery();
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_868G4196", fsBGM0101);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if( dt.Rows.Count > 0 )
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_868FP195",  dt.Rows[0]["EDIGJ"].ToString(),
                                                             fsDTM0101,  //승인일자
                                                             dt.Rows[0]["EDINO1"].ToString(),
                                                             dt.Rows[0]["EDINO2"].ToString(),
                                                             dt.Rows[0]["EDINO3"].ToString(),
                                                             fsDTM0201,  //통보일시
                                                             fsDTM0301,  //연장시작일자
                                                             fsDTM0401,  //연장종료일자
                                                             fsGIS0101,  //연장처리구분
                                                             fsFTX0201,  //승인거부사유
                                                             fsRFF0101,  //기간연장승인번호
                                                             TYUserInfo.EmpNo   
                                                            );
                this.DbConnector.ExecuteTranQuery();
            }
          
            UP_Set_FiledClear();
        }
        #endregion

        #region  Description :  Database 처리 이벤트
        //반입보고서
        private void UP_Set_EDICUSRES_EDIIPGOF(string sDocSubmitNum, string sEDIRCVGB, string sEDIMSG)
        {
            //신EDI 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_74BE3229", sDocSubmitNum.Substring(0, 8),
                                                        "20" + sDocSubmitNum.Substring(8, 2),
                                                        sDocSubmitNum.Substring(10, 8)
                                                       );
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                //접수,오류사항 기록
                if (dt.Rows[0]["EDIRCVGB"].ToString() != "Y")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_74BEB230", sEDIRCVGB,
                                                                sEDIMSG,
                                                                TYUserInfo.EmpNo,
                                                                dt.Rows[0]["EDIGJ"].ToString(),
                                                                dt.Rows[0]["EDINO1"].ToString(),
                                                                dt.Rows[0]["EDINO2"].ToString(),
                                                                dt.Rows[0]["EDINO3"].ToString()
                                                               );
                    this.DbConnector.ExecuteTranQuery();
                }
            }

            //구EDI
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_818LY417", sDocSubmitNum.Substring(0, 8),
                                                        "20" + sDocSubmitNum.Substring(8, 2),
                                                        sDocSubmitNum.Substring(10, 8)
                                                       );
            DataTable dk = this.DbConnector.ExecuteDataTable();
            if (dk.Rows.Count > 0)
            {
                //접수,오류사항 기록
                if (dk.Rows[0]["EDIRCVGB"].ToString() != "Y")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_818LZ418", sEDIRCVGB,
                                                                sEDIMSG,                                                                
                                                                dk.Rows[0]["EDIGJ"].ToString(),
                                                                dk.Rows[0]["EDINO1"].ToString(),
                                                                dk.Rows[0]["EDINO2"].ToString(),
                                                                dk.Rows[0]["EDINO3"].ToString()
                                                               );
                    this.DbConnector.ExecuteNonQuery();
                }
            }

        }

        //반출보고서
        private void UP_Set_EDICUSRES_EDICHULF(string sDocSubmitNum, string sEDIRCVGB, string sEDIMSG)
        {
            //신 EDI
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_74BEO231", sDocSubmitNum.Substring(0, 8),
                                                        "20" + sDocSubmitNum.Substring(8, 2),
                                                        sDocSubmitNum.Substring(10, 8)
                                                       );
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                //접수사항 기록
                if (dt.Rows[0]["EDIRCVGB"].ToString() != "Y")
                {
                    this.DbConnector.CommandClear();
                    //EDICHULF
                    this.DbConnector.Attach("TY_P_UT_74BEP232", sEDIRCVGB, sEDIMSG,
                                                                TYUserInfo.EmpNo,
                                                                dt.Rows[0]["EDIGJ"].ToString(),
                                                                dt.Rows[0]["EDINO1"].ToString(),
                                                                dt.Rows[0]["EDINO2"].ToString(),
                                                                dt.Rows[0]["EDINO3"].ToString()
                                                               );
                    //EDICHULMUF
                    this.DbConnector.Attach("TY_P_UT_A4EB5268", sEDIRCVGB, sEDIMSG,
                                                                TYUserInfo.EmpNo,
                                                                dt.Rows[0]["EDIGJ"].ToString(),
                                                                dt.Rows[0]["EDINO1"].ToString(),
                                                                dt.Rows[0]["EDINO2"].ToString(),
                                                                dt.Rows[0]["EDINO3"].ToString()
                                                               );
                    this.DbConnector.ExecuteTranQueryList();
                }
            }

            //구 EDI
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_818M1419", sDocSubmitNum.Substring(0, 8),
                                                        "20" + sDocSubmitNum.Substring(8, 2),
                                                        sDocSubmitNum.Substring(10, 8)
                                                       );
            DataTable dk = this.DbConnector.ExecuteDataTable();
            if (dk.Rows.Count > 0)
            {
                //접수사항 기록
                if (dk.Rows[0]["EDIRCVGB"].ToString() != "Y")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_818M4420", sEDIRCVGB, sEDIMSG,                                                                
                                                                dk.Rows[0]["EDIGJ"].ToString(),
                                                                dk.Rows[0]["EDINO1"].ToString(),
                                                                dk.Rows[0]["EDINO2"].ToString(),
                                                                dk.Rows[0]["EDINO3"].ToString()
                                                               );
                    this.DbConnector.ExecuteNonQuery();
                }
            }

        }

        //체화예정보고서
        private void UP_Set_EDICUSRES_EDICHEWF(string sEDIRCVGB, string sEDIMSG)
        {

        }

        //정정신고통보/정정결과통보
        private void UP_Set_EDICUSRES_EDIREIPCHF(string sDocSubmitNum, string sEDIRCVGB, string sEDIMSG)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_74BEQ233", sDocSubmitNum.Substring(0, 8),
                                                        "20" + sDocSubmitNum.Substring(8, 2),
                                                        sDocSubmitNum.Substring(10, 8)
                                                       );
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                //접수사항 기록
                this.DbConnector.CommandClear();
                if (fsDOC0101 != "5LG")
                {
                    if (dt.Rows[0]["EDIRERCVGB"].ToString() != "Y")
                    {
                        this.DbConnector.Attach("TY_P_UT_74BES234", sEDIRCVGB, sEDIMSG,
                                                                    "",
                                                                    "",
                                                                    TYUserInfo.EmpNo,
                                                                    dt.Rows[0]["EDIGJ"].ToString(),
                                                                    dt.Rows[0]["EDIRENO1"].ToString(),
                                                                    dt.Rows[0]["EDIRENO2"].ToString(),
                                                                    dt.Rows[0]["EDIRENO3"].ToString(),
                                                                    dt.Rows[0]["EDIRECHASU"].ToString()
                                                                   );
                    }
                }
                else
                {
                    this.DbConnector.Attach("TY_P_UT_74BES234", "",
                                                                "",
                                                                fsGIS0101,
                                                                fsFTX0201,
                                                                TYUserInfo.EmpNo,
                                                                dt.Rows[0]["EDIGJ"].ToString(),
                                                                dt.Rows[0]["EDIRENO1"].ToString(),
                                                                dt.Rows[0]["EDIRENO2"].ToString(),
                                                                dt.Rows[0]["EDIRENO3"].ToString(),
                                                                dt.Rows[0]["EDIRECHASU"].ToString()
                                                                );
                }
                this.DbConnector.ExecuteTranQuery();
            }
        }

        //내국물품 반입결과 통보
        private void UP_Set_EDICUSRES_EDIHAIPGOF(string sDocSubmitNum, string sEDIRCVGB, string sEDIMSG)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_74BEW235", sDocSubmitNum.Substring(0, 8),
                                                        "20" + sDocSubmitNum.Substring(8, 2),
                                                        sDocSubmitNum.Substring(10, 8)
                                                       );
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                //접수사항 기록
                if (dt.Rows[0]["EDIRCVGB"].ToString() != "Y")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_74BEX236", sEDIRCVGB, sEDIMSG,
                                                                TYUserInfo.EmpNo,
                                                                dt.Rows[0]["EDIGJ"].ToString(),
                                                                dt.Rows[0]["EDINO1"].ToString(),
                                                                dt.Rows[0]["EDINO2"].ToString(),
                                                                dt.Rows[0]["EDINO3"].ToString()
                                                               );
                    this.DbConnector.ExecuteTranQuery();
                }
            }
        }

        //내국물품 반출결과 통보
        private void UP_Set_EDICUSRES_EDIHBCHULF(string sDocSubmitNum, string sEDIRCVGB, string sEDIMSG)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_74BEZ238", sDocSubmitNum.Substring(0, 8),
                                                        "20" + sDocSubmitNum.Substring(8, 2),
                                                        sDocSubmitNum.Substring(10, 8)
                                                       );
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                //접수사항 기록
                if (dt.Rows[0]["EDIRCVGB"].ToString() != "Y")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_74BEY237", sEDIRCVGB, sEDIMSG,
                                                                TYUserInfo.EmpNo,
                                                                dt.Rows[0]["EDIGJ"].ToString(),
                                                                dt.Rows[0]["EDINO1"].ToString(),
                                                                dt.Rows[0]["EDINO2"].ToString(),
                                                                dt.Rows[0]["EDINO3"].ToString()
                                                               );
                    this.DbConnector.Attach("TY_P_UT_A4KH4300", sEDIRCVGB, sEDIMSG,
                                                                TYUserInfo.EmpNo,
                                                                dt.Rows[0]["EDIGJ"].ToString(),
                                                                dt.Rows[0]["EDINO1"].ToString(),
                                                                dt.Rows[0]["EDINO2"].ToString(),
                                                                dt.Rows[0]["EDINO3"].ToString()
                                                               );
                    this.DbConnector.ExecuteTranQueryList();
                }
            }
        }

        //반출기간연장 통보
        private void UP_Set_EDICUSRES_EDICHEXTENDF(string sDocSubmitNum, string sEDIRCVGB, string sEDIMSG)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_868G4196", sDocSubmitNum);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                //접수사항 기록
                if (dt.Rows[0]["EDIRCVGB"].ToString() != "Y")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_868FM194", sEDIRCVGB, sEDIMSG, TYUserInfo.EmpNo, "T", sDocSubmitNum);
                    this.DbConnector.ExecuteTranQuery();
                }
            }            
        }

        //반출통고목록보고서 통보
        private void UP_Set_EDICUSRES_EDICHNOTEMF(string sDocSubmitNum, string sEDIRCVGB, string sEDIMSG)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_A6N9E666", sDocSubmitNum);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                //접수사항 기록
                if (dt.Rows[0]["EDNRCVGB"].ToString() != "Y")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_A6N9G667", sEDIRCVGB, sEDIMSG, TYUserInfo.EmpNo, sDocSubmitNum);
                    this.DbConnector.ExecuteTranQuery();
                }
            }
        }
        #endregion

        #region  Description :  파일이동 이벤트
        private void UP_FileToMove()
        {
            int i = 0;
            int iPoint = 0;
            int iIndex = 0;

            string sPathFileName = string.Empty;
            string sFileName = string.Empty;

            sPathFileName = TXT01_AFFILENAME.GetValue().ToString();

            if (sPathFileName != "")
            {

                for (; ; )
                {
                    i = Convert.ToInt16(sPathFileName.IndexOf("\\", iPoint).ToString());

                    if (i < 0)
                    {
                        break;
                    }
                    else
                    {
                        iPoint = i + 1;
                        iIndex = i;
                    }
                }

                sFileName = sPathFileName.Substring(iIndex + 1, sPathFileName.Length - (iIndex + 1));

                if (System.IO.File.Exists(sPathFileName))
                {
                    System.IO.File.Delete(Get_WinmdatePath() + "\\INSAVE\\" + sFileName);

                    System.IO.File.Move(TXT01_AFFILENAME.GetValue().ToString(), Get_WinmdatePath() + "\\INSAVE\\" + sFileName);
                }
            }
        }
        #endregion

        #region  Description :  변수 클리어 이벤트
        private void UP_Set_FiledClear()
        {
         
            fsTEMP_COL1 = "";

            fsEDIGJ = "";
            fsEDIHMNO1 = "";
            fsEDIHMNO2 = "";
            fsAUT0101 = "";
            fsBGM0101 = "";
            fsBGM0102 = "";
            fsCTA0101 = "";
            fsCST0101 = "0";
            fsCST0102 = "";
            fsDTM0101 = "";
            fsDTM0201 = "";
            fsDTM0301 = "";
            fsDTM0401 = "";
            fsDMS0101 = "";
            fsDOC0101 = "";
            fsDOC0201 = "";
            fsDOC0202 = "";
            fsLOC0101 = "";
            fsLOC0102 = "";
            fsLOC0201 = "";
            fsLOC0301 = "";
            fsGIS0101 = "";
            fsGIS0201 = "";
            fsMOA0101 = "0";
            fsMOA0201 = "0";
            fsRFF0101 = "";
            fsRFF0102 = "";
            fsRFF0103 = "";
            fsFTX0101 = "";
            fsFTX0102 = "";
            fsFTX0103 = "";
            fsFTX0104 = "";
            fsFTX0201 = "";
            fsFTX0202 = "";
            fsFTX0203 = "";
            fsFTX0301 = "";
            fsTDT0101 = "";
            fsTDT0102 = "";
            fsNAD0101 = "";
            fsNAD0102 = "";
            fsNAD0103 = "";
            fsNAD0104 = "";
            fsNAD02011 = "";
            fsNAD02012 = "";
            fsNAD0201 = "";
            fsNAD0202 = "";
            fsNAD0203 = "";
            fsNAD0204 = "";
            fsNAD0301 = "";
            fsNAD0302 = "";
            fsNAD0303 = "";
            fsNAD0304 = "";
            fsNAD0401 = "";
            fsMEA0101 = "";
            fsMEA0102 = "0";
            fsMEA0201 = "";
            fsMEA0202 = "0";
            fsCNT0101 = "0";
            fsCNT0102 = "";
            fsCNT0201 = "0";
            fsPAC0101 = "";
            fsPAC0102 = "";
            fsEQD0101 = "";
            fsCOM0101 = "";
            fsCOM0201 = "";
            fsCOM0301 = "";
            fsSEL0101 = "";
            fsSEL0201 = "";
            fsSEL0301 = "";
            fsERP1101 = "";
            fsERP1102 = "";
            fsERP2101 = "";
            fsERP2102 = "";
            fsERP3101 = "";
            fsERP3102 = "";
            fsFTX1101 = "";
            fsFTX1102 = "";
            fsFTX2101 = "";
            fsFTX2102 = "";
            fsFTX3101 = "";
            fsFTX3102 = "";
            fsFTX1201 = "";
            fsFTX2201 = "";
            fsFTX3201 = "";
            fsTOD0101 = "";
            fsUNH0101 = "";


         TYEdiLayout.wco_Response_IssueDateTime = "";  //통보일시        
         TYEdiLayout.wco_Response_TypeCode = "";  //!--문서형태구분--
         TYEdiLayout.wco_AcceptanceDateTime = "";  //수신일시
         TYEdiLayout.wco_Declaration_ID = "";     //문서번호(제출번호)
         TYEdiLayout.wco_Declaration_TypeCode = ""; //문서구분        
         TYEdiLayout.wco_Error_Declaration = ""; //!--오류내역--


        /* --------------- 반출승인내역통보서 ---------------------------------------------------------------- */        
         TYEdiLayout.wco_Response_ID = "";						  //반출승인근거번호      
         TYEdiLayout.wco_Response_FunctionCode = "";					  //반출승인취하구분              
         TYEdiLayout.wco_Response_Declaration_AcceptanceDateTime = "";		  //반출승인취하일자      
         TYEdiLayout.wco_Response_Declaration_Consignment_Warehouse_ID = "";		  //보세구역              
         TYEdiLayout.wco_Response_AdditionalInformation_StatementTypeCode = "";	  //반출승인유형          
         TYEdiLayout.wco_Response_Declaration_ExaminationIndicatorCode = "";		  //검사대상여부  
         TYEdiLayout.wco_TransportContractDocument_ID = "";                     //적하목록 관리번호    
         TYEdiLayout.wco_TransportContractDocument_MasterBLSequenceID = "";     //MSN
         TYEdiLayout.wco_TransportContractDocument_HouseBLSequenceID = "";      //HSN

         TYEdiLayout.wco_Payment_TaxAssessedAmount = "";                        //관세액
         TYEdiLayout.wco_AdditionalDocument_ID = "";                 //장치확인번호
         TYEdiLayout.wco_TradeTerms_ConditionCode = "";              //인도조건

         TYEdiLayout.wco_DutyTaxFee_TypeCode = "";                   //세액구분 (관세:CUD,과세가격합계:5CZ)-->
         TYEdiLayout.wco_DutyTaxFee_Payment_TaxAssessedAmount = "0";             //과세가격

         TYEdiLayout.wco_DutyTaxFee_TypeCode_Total = "";                   //세액구분 (관세:CUD,과세가격합계:5CZ)-->
         TYEdiLayout.wco_DutyTaxFee_Payment_TaxAssessedAmount_Total = "0";             //과세가격 합계

         TYEdiLayout.wco_Consignment_TransportContractDocument_ID = "";          //B/L (AWB) 번호
         TYEdiLayout.wco_BorderTransportMeans_Name = "";             //선(기)명
         TYEdiLayout.wco_BorderTransportMeans_ArrivalDateTime = "";  //입항일자

         TYEdiLayout.wco_Payer_ID = "";                  //납세의무자 사업자등록번호          
         TYEdiLayout.wco_Payer_RoleCode = "";            //납세의무자 구분
         TYEdiLayout.wco_Payer_Name = "";                //납세의무자 상호
         TYEdiLayout.wco_Payer_Contact_Name = "";        //납세의무자 성명

         TYEdiLayout.wco_Commodity_SequenceNumeric = "0";                           //란번호              
         TYEdiLayout.wco_AdditionalInformation_StatementCode = "";		      //보류/수리구분       
         TYEdiLayout.wco_Commodity_CargoDescription = "";			      //품명                
         TYEdiLayout.wco_Commodity_Description = "";				      //규격                
         TYEdiLayout.wco_GoodsMeasure_NetNetWeightMeasure_kcsUnitCode = "";	      //순중량(단위)        
         TYEdiLayout.wco_GoodsMeasure_NetNetWeightMeasure = "0";		      //순중량              
         TYEdiLayout.wco_Commodity_CountQuantity_kcsUnitCode = "";		      //수량(단위)          
         TYEdiLayout.wco_Commodity_CountQuantity = "0";			      //수량                
         TYEdiLayout.wco_AdditionalInformation_ApprovalCountQuantity = "";	      //반출승인개수        
         TYEdiLayout.wco_Packaging_TypeCode = "";				      //포장종류            
         TYEdiLayout.wco_AdditionalInformation_ApprovalWeightMeasure = "";	      //반출승인중량   

         TYEdiLayout.kcs_Reason = "";

        }
        #endregion

        #region  Description :  수신문서 구분 이벤트
        private int UP_Get_DocumentSelect(string sStr)
        {
            int iSW = 0;

            if (sStr.Substring(0, 3) == "BGM")
            {
                switch (sStr.Substring(4, 3))
                {
                    case "5CO":
                        iSW = 1;
                        break;
                    case "5IJ":
                        iSW = 2;
                        break;
                    case "099":
                        iSW = 3;
                        break;
                    case "5IO":
                        iSW = 4;
                        break;
                    case "5IM":
                        iSW = 4;
                        break;
                    case "5LG":
                        iSW = 4;
                        break;
                    case "RAQ":
                        iSW = 4;
                        break;
                    case "5IP":
                        iSW = 5;
                        break;
                    case "R20":
                        iSW = 5;
                        break;
                    case "5IN":
                        iSW = 6;
                        break;
                    case "5HN":  //반출기간연장결과통보
                        iSW = 7;
                        break;
                    case "5HF":
                        iSW = 0;
                        break;
                    default:
                        break;
                }
            }

            return iSW;
        }
        #endregion
        
        #region  Description : KCSAPI4 모듈 사용 함수 전체 모음

        #region  Description : KCSAPI4 수신 이벤트
        private void UP_KCSAPI4Recive_Docment()
        {

            //공인인증서 로그인
            string Login = LoginSecuMdle(Get_KCSAPI4LoginId(), Get_KCSAPI4DocUserId());

            if (Login.Trim().Substring(0, 4) == "C200")
            {
                //통관관련 목록 수신
                string ret = RcpnDocLstCscl(Get_KCSAPI4LoginId(), Get_KCSAPI4DocUserId());

                if (ret != "" && ret.Substring(0, 4) != "C200")
                {
                    this.ShowCustomMessage(ret, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }

                //KCSAPI4 폴더 오픈 및 파일 읽기
                this.UP_Get_KCSAPI4FileOpenRead();
            }           
        }
        #endregion

        #region  Description : KCSAPI4 폴더 오픈 및 파일 읽기
        private void UP_Get_KCSAPI4FileOpenRead()
        {
            try
            {
                string sDirDate = DateTime.Now.ToString("yyyyMMdd");

                string[] Getfiles = System.IO.Directory.GetFiles(ConstKCSAPIPath + "\\" + sDirDate + "\\Rcv\\", "*.txt");

                if (Getfiles.Length > 0)
                {
                    pgBar.Visible = true;

                    pgBar.Minimum = 0;
                    pgBar.Maximum = 0;
                    pgBar.Value = 0;                    
                }
                else
                {
                    this.ShowCustomMessage("수신 할 자료가 존재하지 않습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    return;
                }

                foreach (string file in Getfiles)
                {
                    //파일존재 체크
                    if (System.IO.File.Exists(file))
                    {
                        UP_KCSAPI4DataReceiveToConvert(file);
                    }
                }
            }
            catch
            {
            }
            finally
            {

                string LoginOut = LogoutSecuMdle();

                pgBar.Visible = false;

                UP_DirFileMove_KCSAPI4();
            }
        }
        #endregion

        #region  Description : KCSAPI4 변환 이벤트
        private void UP_KCSAPI4DataReceiveToConvert(string sFileName)
        {
            string sRevDataA = string.Empty;
            string sConversationID = string.Empty;
            string sDocCode = string.Empty;
            string[] arrayResult;
            string ret = string.Empty;

            TXT01_AFFILENAME.SetValue(sFileName);

            //수신파일 변환
            if (TXT01_AFFILENAME.GetValue().ToString() != "")
            {
                //파일 읽기
                StreamReader file = new StreamReader(TXT01_AFFILENAME.GetValue().ToString(), Encoding.Default);
                sRevDataA = file.ReadToEnd();
                file.Close();

                if (sRevDataA.Length > 0)
                {                   
                    arrayResult = sRevDataA.Split('\n');
                    if (arrayResult.Length > 0)
                    {
                        pgBar.Maximum = arrayResult.Length;

                        for (int i = 0; i < arrayResult.Length - 1; i++)
                        {
                            string[] arrayDoc = arrayResult[i].ToString().Split(',');
                            //xml문서번호
                            sConversationID = arrayDoc[0].ToString();
                            //문서코드
                            sDocCode = arrayDoc[1].ToString();                            

                            //문서번호에 해당하는 XML파일 수신
                            ret = RcpnDocCscl(Get_KCSAPI4LoginId(), Get_KCSAPI4DocUserId(), sDocCode, sConversationID);

                        }
                    } //if (arrayResult.Length > 0)..end

                    this.UP_KCSAPI4XmlDocment();
                }
            }
        }
        #endregion

        #region  Description : KCSAPI4 xml 문서 처리
        private void UP_KCSAPI4XmlDocment()
        {
            Int16 iReciveCnt = 0;
            string sDocCode = string.Empty;

            string sNodeName = string.Empty;
            string sNodeValue = string.Empty;
            string sParentNodeName = string.Empty;
            string sAttName = string.Empty;
            string sAttValue = string.Empty;

            string sDirDate = DateTime.Now.ToString("yyyyMMdd");

            string[] Getfiles = System.IO.Directory.GetFiles(ConstKCSAPIPath + "\\" + sDirDate + "\\Rcv\\", "*.xml");

            foreach (string file in Getfiles)
            {
                //파일존재 체크
                if (System.IO.File.Exists(file))
                {

                    UP_Set_FiledClear();

                    //XML 문서 파싱
                    UP_ParseXml(file);
                    //문서종류
                    sDocCode = UP_Get_XmlToDocCode(file);

                    if (sDocCode == "GOVCBRRAQ" || sDocCode == "GOVCBRR20" || sDocCode == "GOVCBR099" || sDocCode == "GOVCBR5HN" )
                    {
                        if (_NodeValueList.Length > 0)
                        {
                            for (int i = 0; i < _NodeValueList.Length; i++)
                            {
                                string[] sXmlElement = _NodeValueList[i].Split(',');

                                if (sXmlElement.Length > 2)
                                {
                                    //노드명
                                    sNodeName = sXmlElement[0].ToString();
                                    //노드값
                                    sNodeValue = sXmlElement[1].ToString();
                                    //x-path
                                    sParentNodeName = sXmlElement[2].ToString();
                                    //속성명
                                    sAttName = sXmlElement[3].ToString();
                                    //속성값
                                    sAttValue = sXmlElement[4].ToString();

                                    if (sDocCode == "GOVCBR099")  //반출승인통보
                                    {
                                        #region 반출승인통보 Xml Case
                                        switch (sParentNodeName)
                                        {
                                            case "wco:Response/wco:IssueDateTime":  //통보일시
                                                TYEdiLayout.wco_Response_IssueDateTime = sNodeValue;
                                                break;
                                            case "wco:Response/wco:FunctionCode":  //반출승인취하구분(31: 승인 1: 취하)
                                                TYEdiLayout.wco_Response_FunctionCode = sNodeValue;
                                                break;
                                            case "wco:Response/wco:TypeCode":  //문서형태구분
                                                TYEdiLayout.wco_Response_TypeCode = sNodeValue;
                                                break;
                                            case "wco:Response/wco:ID":  //문서번호(제출번호)
                                                TYEdiLayout.wco_Response_ID = sNodeValue;
                                                break;
                                            case "wco:Response/wco:AdditionalInformation/wco:StatementTypeCode":  //반출승인유형
                                                TYEdiLayout.wco_Response_AdditionalInformation_StatementTypeCode = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/wco:AcceptanceDateTime":  //반출승인취하일자(CCYYMMDD)
                                                TYEdiLayout.wco_Response_Declaration_AcceptanceDateTime = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/kcs:ExaminationIndicatorCode":  //검사대상여부(Y: 검사대상)
                                                TYEdiLayout.wco_Response_Declaration_ExaminationIndicatorCode = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/wco:AdditionalDocument/wco:ID":  //장치확인번호-->
                                                TYEdiLayout.wco_AdditionalDocument_ID = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/wco:BorderTransportMeans/wco:ArrivalDateTime":  //입항일자(CCYYMMDD)
                                                TYEdiLayout.wco_BorderTransportMeans_ArrivalDateTime = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/wco:BorderTransportMeans/wco:Name":  //선(기)명-->
                                                TYEdiLayout.wco_BorderTransportMeans_Name = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/wco:Consignment/wco:AdditionalInformation/kcs:ApprovalCountQuantity":  //!--반출승인개수--
                                                TYEdiLayout.wco_AdditionalInformation_ApprovalCountQuantity = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/wco:Consignment/wco:AdditionalInformation/kcs:ApprovalWeightMeasure":  //!--반출승인중량--
                                                TYEdiLayout.wco_AdditionalInformation_ApprovalWeightMeasure = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/wco:Consignment/wco:ConsignmentItem/wco:AdditionalInformation/wco:StatementCode":  //보류/수리 구분(P :보류 C : 수리)
                                                TYEdiLayout.wco_AdditionalInformation_StatementCode = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/wco:Consignment/wco:ConsignmentItem/wco:Commodity/wco:SequenceNumeric":  // <!--란번호-->
                                                TYEdiLayout.wco_Commodity_SequenceNumeric = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/wco:Consignment/wco:ConsignmentItem/wco:Commodity/wco:CargoDescription":  //<!--품명-->
                                                TYEdiLayout.wco_Commodity_CargoDescription = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/wco:Consignment/wco:ConsignmentItem/wco:Commodity/wco:CountQuantity":  //수량(단위)-->
                                                if (sAttName != "")
                                                {
                                                    TYEdiLayout.wco_Commodity_CountQuantity_kcsUnitCode = sAttValue;  //수량(단위)-->
                                                }
                                                TYEdiLayout.wco_Commodity_CountQuantity = sNodeValue;  //수량-->
                                                break;
                                            case "wco:Response/wco:Declaration/wco:Consignment/wco:ConsignmentItem/wco:Commodity/wco:Description":  //규격
                                                TYEdiLayout.wco_Commodity_Description = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/wco:Consignment/wco:ConsignmentItem/wco:GoodsMeasure/wco:NetNetWeightMeasure":  //<!--순중량(단위)-->
                                                if (sAttName != "")
                                                {
                                                    TYEdiLayout.wco_GoodsMeasure_NetNetWeightMeasure_kcsUnitCode = sAttValue;
                                                }
                                                TYEdiLayout.wco_GoodsMeasure_NetNetWeightMeasure = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/wco:Consignment/wco:Packaging/wco:TypeCode":  //!--포장종류--
                                                TYEdiLayout.wco_Packaging_TypeCode = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/wco:Consignment/wco:TradeTerms/wco:ConditionCode":  //인도조건
                                                TYEdiLayout.wco_TradeTerms_ConditionCode = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/wco:Consignment/wco:TransportContractDocument/wco:ID":  //B/L (AWB) 번호-->
                                                TYEdiLayout.wco_Consignment_TransportContractDocument_ID = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/wco:Consignment/wco:Warehouse/wco:ID":  //<!--보세구역-->
                                                TYEdiLayout.wco_Response_Declaration_Consignment_Warehouse_ID = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/wco:DutyTaxFee/wco:TypeCode":  //세액구분 (관세:CUD,과세가격합계:5CZ)-
                                                if (sNodeValue == "CUD")
                                                {
                                                    TYEdiLayout.wco_DutyTaxFee_TypeCode = sNodeValue;
                                                }
                                                else
                                                {
                                                    TYEdiLayout.wco_DutyTaxFee_TypeCode_Total = sNodeValue;
                                                }
                                                break;
                                            case "wco:Response/wco:Declaration/wco:DutyTaxFee/wco:Payment/wco:TaxAssessedAmount":  //!--세액--
                                                if (TYEdiLayout.wco_DutyTaxFee_TypeCode_Total == "5CZ")
                                                {
                                                    if (TYEdiLayout.wco_DutyTaxFee_TypeCode == "CUD")
                                                    {
                                                        TYEdiLayout.wco_DutyTaxFee_Payment_TaxAssessedAmount = sNodeValue;
                                                    }
                                                    else
                                                    {
                                                        TYEdiLayout.wco_DutyTaxFee_Payment_TaxAssessedAmount_Total = sNodeValue;
                                                    }
                                                }
                                                else
                                                {
                                                    TYEdiLayout.wco_DutyTaxFee_Payment_TaxAssessedAmount = sNodeValue;
                                                }
                                                break;
                                            case "wco:Response/wco:Declaration/wco:Payer/wco:ID":  //납세의무자 사업자등록번호-->
                                                TYEdiLayout.wco_Payer_ID = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/wco:Payer/wco:Name":  //!--납세의무자 상호-->
                                                TYEdiLayout.wco_Payer_Name = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/wco:Payer/wco:RoleCode":  //<!--납세의무자 구분-->
                                                TYEdiLayout.wco_Payer_RoleCode = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/wco:Payer/wco:Contact/wco:Name":  //<!--납세의무자 성명-->
                                                TYEdiLayout.wco_Payer_Contact_Name = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/wco:TransportContractDocument/wco:ID":  //<!--적하목록 관리번호-->
                                                TYEdiLayout.wco_TransportContractDocument_ID = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/wco:TransportContractDocument/kcs:MasterBLSequenceID":  //<!--msn-->
                                                TYEdiLayout.wco_TransportContractDocument_MasterBLSequenceID = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/wco:TransportContractDocument/kcs:HouseBLSequenceID":  //<!--hsn->
                                                TYEdiLayout.wco_TransportContractDocument_HouseBLSequenceID = sNodeValue;
                                                break;
                                        }
                                        #endregion
                                    }
                                    else if (sDocCode == "GOVCBR5HN")  //반출기간연장결과통보
                                    {
                                        #region 반출기간연장결과통보 Xml Case
                                        switch (sParentNodeName)
                                        {
                                            case "wco:Response/wco:IssueDateTime":  //통보일시
                                                TYEdiLayout.wco_Response_IssueDateTime = sNodeValue;
                                                break;
                                            case "wco:Response/wco:TypeCode":  //문서형태구분
                                                //문서종류 판단
                                                TYEdiLayout.wco_Response_TypeCode = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/wco:ID":  //문서번호(제출번호)
                                                TYEdiLayout.wco_Declaration_ID = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/kcs:AuthenticationDateTime":  //승인일자
                                                TYEdiLayout.kcs_AuthenticationDateTime = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/kcs:Reason":  //승인거부사유
                                                TYEdiLayout.kcs_Reason = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/wco:AdditionalDocument/wco:ID":  //기간연장승인번호
                                                TYEdiLayout.wco_AdditionalDocument_ID = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/wco:AdditionalInformation/wco:StatementCode":  //연장처리구분
                                                TYEdiLayout.wco_AdditionalInformation_StatementCode = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/wco:AdditionalInformation/kcs:BeginningDateTime":  //연장기간시작일
                                                TYEdiLayout.wco_AdditionalInformation_BeginningDateTime = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/wco:AdditionalInformation/kcs:EndingDateTime":  //연장기간종료일
                                                TYEdiLayout.wco_AdditionalInformation_EndingDateTime = sNodeValue;
                                                break;
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        // 접수통보, 오류통보
                                        #region 접수통보, 오류통보 Xml Case
                                        switch (sParentNodeName)
                                        {
                                            case "wco:Response/wco:IssueDateTime":  //통보일시
                                                TYEdiLayout.wco_Response_IssueDateTime = sNodeValue;
                                                break;
                                            case "wco:Response/wco:TypeCode":  //문서형태구분
                                                //문서종류 판단
                                                TYEdiLayout.wco_Response_TypeCode = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/wco:TypeCode":  //접수문서형태구분
                                                TYEdiLayout.wco_Declaration_TypeCode = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/wco:AcceptanceDateTime":  //수신일시
                                                TYEdiLayout.wco_AcceptanceDateTime = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Declaration/wco:ID":  //문서번호(제출번호)
                                                TYEdiLayout.wco_Declaration_ID = sNodeValue;
                                                break;
                                            case "wco:Response/wco:Error/kcs:Description":  //오류내역
                                                TYEdiLayout.wco_Error_Declaration = sNodeValue;
                                                break;
                                        }
                                        #endregion
                                    }

                                }

                            } //for (int i = 0; i < _NodeValueList.Length; i++)...end

                            if (TYEdiLayout.wco_Response_TypeCode != "")
                            {

                                switch (TYEdiLayout.wco_Response_TypeCode)
                                {
                                    case "GOVCBRRAQ":  //접수통보
                                        this.UP_Set_KCSAPI4ReciveDocUpdate(TYEdiLayout.wco_Declaration_TypeCode, TYEdiLayout.wco_Declaration_ID, "Y", "");
                                        iReciveCnt = Convert.ToInt16(TXT03_EDIREJBGB.GetValue().ToString());
                                        iReciveCnt += 1;
                                        TXT03_EDIREJBGB.SetValue(iReciveCnt.ToString());
                                        break;
                                    case "GOVCBRR20":  //오류통보
                                        this.UP_Set_KCSAPI4ReciveDocUpdate(TYEdiLayout.wco_Declaration_TypeCode, TYEdiLayout.wco_Declaration_ID, "E", TYEdiLayout.wco_Error_Declaration);
                                        iReciveCnt = Convert.ToInt16(TXT05_EDIREJBGB.GetValue().ToString());
                                        iReciveCnt += 1;
                                        TXT05_EDIREJBGB.SetValue(iReciveCnt.ToString());
                                        break;
                                    case "GOVCBR099":  //반출승인통보                                    
                                        this.UP_Set_KCSAPI4ReciveEDICUSCRA099();
                                        iReciveCnt = Convert.ToInt16(TXT02_EDIREJBGB.GetValue().ToString());
                                        iReciveCnt += 1;
                                        TXT02_EDIREJBGB.SetValue(iReciveCnt.ToString());
                                        break;
                                    case "GOVCBR5HN":  //반출기간연장결과통보
                                        this.UP_Set_KCSAPI4ReciveEDICUSCRA5HN();
                                        iReciveCnt = Convert.ToInt16(TXT03_EDIREJBGB.GetValue().ToString());
                                        iReciveCnt += 1;
                                        TXT03_EDIREJBGB.SetValue(iReciveCnt.ToString());
                                        break;
                                }

                                UP_Set_FiledClear();
                            }
                        } //if (_NodeValueList.Length > 0)..end
                    }

                    pgBar.Value = pgBar.Value + 1;
                    pgBar.Refresh();

                } //if (System.IO.File.Exists(file))...end

            } //foreach (string file in Getfiles)..end

        }
        #endregion

        #region  Description : 문서 수신 처리
        private void UP_Set_KCSAPI4ReciveDocUpdate(string sDoc, string sSinno, string sGubn, string sMsg)
        {
            switch (sDoc)
            {
                case "GOVCBR632":
                    //세관 반입보고 누적  FILE
                    UP_Set_EDICUSRES_EDIIPGOF(sSinno, sGubn, sMsg);
                    break;
                case "GOVCBR6NB":
                    //세관 반출보고 누적  FILE                    
                    UP_Set_EDICUSRES_EDICHULF(sSinno, sGubn, sMsg);
                    break;
                case "GOVCBR5LC":
                    //세관 반출입 정정신고 통보
                    UP_Set_EDICUSRES_EDIREIPCHF(sSinno, sGubn, sMsg);
                    break;
                case "GOVCBR004":
                    //세관 반출입 정정신고 통보
                    UP_Set_EDICUSRES_EDIREIPCHF(sSinno, sGubn, sMsg);
                    break;
                case "GOVCBR5LD":
                    //'세관 반출입 정정신고 통보
                    UP_Set_EDICUSRES_EDIREIPCHF(sSinno, sGubn, sMsg);
                    break;
                case "GOVCBR005":
                    //'세관 반출입 정정신고 통보
                    UP_Set_EDICUSRES_EDIREIPCHF(sSinno, sGubn, sMsg);
                    break;
                case "GOVCBR5HA":
                    //'세관 내국반입 결과 통보
                    UP_Set_EDICUSRES_EDIHAIPGOF(sSinno, sGubn, sMsg);
                    break;
                case "GOVCBR5HB":
                    //'세관 내국반출 결과 통보
                    UP_Set_EDICUSRES_EDIHBCHULF(sSinno, sGubn, sMsg);
                    break;
                case "GOVCBR5HM":
                    //'반출기간연장 결과 통보
                    UP_Set_EDICUSRES_EDICHEXTENDF(sSinno, sGubn, sMsg);
                    break;
                case "GOVCBR5II":
                    //'반출통고목록보고서 통보
                    UP_Set_EDICUSRES_EDICHNOTEMF(sSinno, sGubn, sMsg);
                    break;
            }

        }

        //반출승인통보
        private void UP_Set_KCSAPI4ReciveEDICUSCRA099()
        {
            string sEDIJUKHA = string.Empty;
            string sEDIBLMSN = string.Empty;
            string sEDIBLHSN = string.Empty;
            string sCRANEW = string.Empty;

            try
            {
                //반출승인근거번호
                fsBGM0101 = TYEdiLayout.wco_Response_ID;
                //반출승인취하구분
                fsBGM0102 = TYEdiLayout.wco_Response_FunctionCode;
                //관세청 통보일시(CCYYMMDDHHMM
                fsDTM0101 = TYEdiLayout.wco_Response_IssueDateTime.Substring(0,12);
                //반출승인취하일자
                fsDTM0201 = TYEdiLayout.wco_Response_Declaration_AcceptanceDateTime;
                //장치장확인번호
                fsLOC0101 = TYEdiLayout.wco_Response_Declaration_Consignment_Warehouse_ID;

                //반출승인유형-->
                fsGIS0101 = TYEdiLayout.wco_Response_AdditionalInformation_StatementTypeCode;
                //검사대상여부
                fsGIS0201 = TYEdiLayout.wco_Response_Declaration_ExaminationIndicatorCode;
                //적하목록
                fsDOC0101 = TYEdiLayout.wco_TransportContractDocument_ID;
                //MSN
                fsDOC0201 = TYEdiLayout.wco_TransportContractDocument_MasterBLSequenceID;
                //HSN
                fsDOC0202 = TYEdiLayout.wco_TransportContractDocument_HouseBLSequenceID;
                //관세액
                fsMOA0101 = TYEdiLayout.wco_DutyTaxFee_Payment_TaxAssessedAmount;

                //장치장위치
                fsRFF0101 = TYEdiLayout.wco_AdditionalDocument_ID.Replace("D","").Trim();
                //인도조건
                fsTOD0101 = TYEdiLayout.wco_TradeTerms_ConditionCode;
                //관세합계
                fsMOA0201 = TYEdiLayout.wco_DutyTaxFee_Payment_TaxAssessedAmount_Total;
                //bl번호
                fsDMS0101 = TYEdiLayout.wco_Consignment_TransportContractDocument_ID;
                //선명 
                fsTDT0101 = TYEdiLayout.wco_BorderTransportMeans_Name;
                //입항일자
                fsDTM0301 = TYEdiLayout.wco_BorderTransportMeans_ArrivalDateTime;

                //사업자번호
                fsNAD0101 = TYEdiLayout.wco_Payer_ID;
                //사업자구분
                fsNAD0102 = TYEdiLayout.wco_Payer_RoleCode;
                //상호
                fsNAD0103 = TYEdiLayout.wco_Payer_Name;
                //성명
                fsNAD0104 = TYEdiLayout.wco_Payer_Contact_Name;

                //란번호
                fsCST0101 = TYEdiLayout.wco_Commodity_SequenceNumeric;

                //란구분
                fsCST0102 = "";

                //품명
                fsFTX0101 = TYEdiLayout.wco_Commodity_CargoDescription;
                //규격
                fsFTX0102 = TYEdiLayout.wco_Commodity_Description;

                //순중량단위
                fsMEA0101 = TYEdiLayout.wco_GoodsMeasure_NetNetWeightMeasure_kcsUnitCode;
                //순중량 
                fsMEA0102 = TYEdiLayout.wco_GoodsMeasure_NetNetWeightMeasure;

                //수량단위
                fsMEA0201 = TYEdiLayout.wco_Commodity_CountQuantity_kcsUnitCode;
                //수량
                fsMEA0202 = TYEdiLayout.wco_Commodity_CountQuantity;
                //반출승인갯수
                fsCNT0101 = TYEdiLayout.wco_AdditionalInformation_ApprovalCountQuantity;
                //포장종류
                fsCNT0102 = TYEdiLayout.wco_Packaging_TypeCode;
                //반출승인중량
                fsCNT0201 = TYEdiLayout.wco_AdditionalInformation_ApprovalWeightMeasure;


                //적하목록
                sEDIJUKHA = TYEdiLayout.wco_TransportContractDocument_ID;
                //MSN
                sEDIBLMSN = TYEdiLayout.wco_TransportContractDocument_MasterBLSequenceID;
                //HSN
                sEDIBLHSN = Get_Numeric(TYEdiLayout.wco_TransportContractDocument_HouseBLSequenceID);

                //반입보고서에 없으면 적하목록, MSN 으로 입고관리를 찾는다
                //SILO
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_74CF4266", sEDIJUKHA,
                                                            sEDIBLMSN,
                                                            sEDIBLHSN
                                                            );
                DataTable dsilo = this.DbConnector.ExecuteDataTable();
                if (dsilo.Rows.Count > 0)
                {
                    fsEDIGJ = "S";

                    for (int i = 0; i < dsilo.Rows.Count; i++)
                    {
                        if (fsRFF0101.Trim() != "")
                        {
                            string sImEDIHMNO1 = "20" + TYEdiLayout.wco_AdditionalDocument_ID.Replace("D", "").Trim().Substring(0, 2);
                            string sImEDIHMNO2 = TYEdiLayout.wco_AdditionalDocument_ID.Replace("D", "").Trim().Substring(2, TYEdiLayout.wco_AdditionalDocument_ID.Replace("D", "").Trim().Length - 2);

                            if (sImEDIHMNO1 != "" && sImEDIHMNO2 != "")
                            {
                                if (sImEDIHMNO1 == dsilo.Rows[i]["IBHMNO1"].ToString() &&
                                    Convert.ToInt32(sImEDIHMNO2) == Convert.ToInt32(dsilo.Rows[i]["IBHMNO2"].ToString())
                                    )
                                {
                                    fsEDIHMNO1 = dsilo.Rows[i]["IBHMNO1"].ToString();
                                    fsEDIHMNO2 = dsilo.Rows[i]["IBHMNO2"].ToString();
                                    break;
                                }
                            }
                        }
                    }

                    //번호에 맞는 BL을 못찾으면 재고남은 msn에 배당한다.
                    if (fsEDIHMNO1 == "" && fsEDIHMNO2 == "")
                    {
                        for (int i = 0; i < dsilo.Rows.Count; i++)
                        {
                            if (Convert.ToDouble(dsilo.Rows[i]["CSJANQTY"].ToString()) > 0)
                            {
                                fsEDIHMNO1 = dsilo.Rows[i]["IBHMNO1"].ToString();
                                fsEDIHMNO2 = dsilo.Rows[i]["IBHMNO2"].ToString();
                                break;
                            }
                        }
                    }

                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_74CFB267", sEDIJUKHA,
                                                                sEDIBLMSN,
                                                                sEDIBLHSN
                                                                );
                    DataTable duTT = this.DbConnector.ExecuteDataTable();
                    if (duTT.Rows.Count > 0)
                    {
                        fsEDIGJ = "T";

                        fsEDIHMNO1 = duTT.Rows[0]["IPSINOYY"].ToString();
                        fsEDIHMNO2 = duTT.Rows[0]["IPSINO"].ToString();
                    }
                    else
                    {                       
                        //BL분할된 건일 경우 분할전 bl을 찾는다
                        if (Convert.ToInt16(sEDIBLHSN.Trim()) > 0)
                        {
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_UT_74CFB267", sEDIJUKHA,
                                                                        sEDIBLMSN,
                                                                        "0"
                                                                        );
                            DataTable dBuhal = this.DbConnector.ExecuteDataTable();
                            if (dBuhal.Rows.Count > 0)
                            {
                                fsEDIGJ = "T";

                                fsEDIHMNO1 = "20" + TYEdiLayout.wco_AdditionalDocument_ID.Replace("D", "").Trim().Substring(0, 2);
                                fsEDIHMNO2 = TYEdiLayout.wco_AdditionalDocument_ID.Replace("D", "").Trim().Substring(2, TYEdiLayout.wco_AdditionalDocument_ID.Replace("D", "").Trim().Length - 2);
                            }
                            else
                            {
                                fsEDIGJ = "S";

                                fsEDIHMNO1 = "20" + TYEdiLayout.wco_AdditionalDocument_ID.Replace("D", "").Trim().Substring(0, 2);
                                fsEDIHMNO2 = TYEdiLayout.wco_AdditionalDocument_ID.Replace("D", "").Trim().Substring(2, TYEdiLayout.wco_AdditionalDocument_ID.Replace("D", "").Trim().Length - 2);
                            }
                        }
                        else
                        {
                            fsEDIGJ = "S";

                            fsEDIHMNO1 = "20" + TYEdiLayout.wco_AdditionalDocument_ID.Replace("D", "").Trim().Substring(0, 2);
                            fsEDIHMNO2 = TYEdiLayout.wco_AdditionalDocument_ID.Replace("D", "").Trim().Substring(2, TYEdiLayout.wco_AdditionalDocument_ID.Replace("D", "").Trim().Length - 2);
                        }
                    }                   
                }

                if (fsEDIHMNO1 != "" && fsEDIHMNO2 != "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_74CBL263", fsBGM0101);
                    this.DbConnector.ExecuteTranQuery();

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_74CBO264", fsEDIGJ,
                                                                  fsEDIHMNO1,
                                                                  fsEDIHMNO2.Replace("D", ""),
                                                                  fsUNH0101,
                                                                  fsBGM0101,
                                                                  fsBGM0102,
                                                                  fsDTM0101,
                                                                  fsDTM0201,
                                                                  fsLOC0101,
                                                                  fsGIS0101,
                                                                  fsGIS0201,
                                                                  fsDOC0101,
                                                                  fsDOC0201,
                                                                  fsDOC0202,
                                                                  fsMOA0101,
                                                                  fsRFF0101,
                                                                  fsTOD0101,
                                                                  fsMOA0201,
                                                                  fsDMS0101,
                                                                  fsTDT0101,
                                                                  fsDTM0301,
                                                                  fsNAD0101,
                                                                  fsNAD0102,
                                                                  fsNAD0103,
                                                                  fsNAD0104,
                                                                  fsCST0101,
                                                                  fsCST0102,
                                                                  fsFTX0101,
                                                                  fsFTX0102,
                                                                  fsMEA0101,
                                                                  fsMEA0102,
                                                                  fsMEA0201,
                                                                  fsMEA0202,
                                                                  fsCNT0101,
                                                                  fsCNT0102,
                                                                  fsCNT0201,
                                                                  sCRANEW,
                                                                  TYUserInfo.EmpNo,
                                                                  ""
                                                                );
                    this.DbConnector.ExecuteTranQuery();
                }

                /*
                //silo 통관분일경우 구 edi 테이블에 넣어준다
                if (fsEDIGJ == "S")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_81AAS426", fsBGM0101);
                    this.DbConnector.ExecuteNonQuery();

                    fsNAD0103 = StringTransfer(fsNAD0103, 20);

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_818LS416", fsEDIGJ,
                                                                  fsEDIHMNO1,
                                                                  fsEDIHMNO2.Replace("D", ""),
                                                                  fsUNH0101,
                                                                  fsBGM0101,
                                                                  fsBGM0102,
                                                                  fsDTM0101,
                                                                  fsDTM0201,
                                                                  fsLOC0101,
                                                                  fsGIS0101,
                                                                  fsGIS0201,
                                                                  fsDOC0101,
                                                                  fsDOC0201,
                                                                  fsDOC0202,
                                                                  fsMOA0101,
                                                                  fsRFF0101,
                                                                  fsTOD0101,
                                                                  fsMOA0201,
                                                                  fsDMS0101,
                                                                  fsTDT0101,
                                                                  fsDTM0301,
                                                                  fsNAD0101,
                                                                  fsNAD0102,
                                                                  fsNAD0103,
                                                                  fsNAD0104,
                                                                  fsCST0101,
                                                                  fsCST0102,
                                                                  fsFTX0101,
                                                                  fsFTX0102,
                                                                  fsMEA0101,
                                                                  fsMEA0102,
                                                                  fsMEA0201,
                                                                  fsMEA0202,
                                                                  fsCNT0101,
                                                                  fsCNT0102,
                                                                  fsCNT0201,
                                                                  sCRANEW,
                                                                  TYUserInfo.EmpNo,
                                                                  ""
                                                                );
                    this.DbConnector.ExecuteNonQuery();
                }
                */

            }
            catch (Exception ex)
            {
                string dd = ex.Message;
            }
            finally
            {
            }
        }

        //반출기간연장결과 통보
        private void UP_Set_KCSAPI4ReciveEDICUSCRA5HN()
        {           
            if (TYEdiLayout.wco_Response_TypeCode == "GOVCBR5HN")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_868G4196", TYEdiLayout.wco_Declaration_ID);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_868FP195",  dt.Rows[0]["EDIGJ"].ToString(),
                                                                 TYEdiLayout.kcs_AuthenticationDateTime,  //승인일자
                                                                 dt.Rows[0]["EDINO1"].ToString(),
                                                                 dt.Rows[0]["EDINO2"].ToString(),
                                                                 dt.Rows[0]["EDINO3"].ToString(),
                                                                 TYEdiLayout.wco_Response_IssueDateTime,  //통보일시
                                                                 TYEdiLayout.wco_AdditionalInformation_BeginningDateTime,  //연장시작일자
                                                                 TYEdiLayout.wco_AdditionalInformation_EndingDateTime,  //연장종료일자
                                                                 TYEdiLayout.wco_AdditionalInformation_StatementCode,  //연장처리구분
                                                                 //TYEdiLayout.kcs_Reason,  //승인거부사유
                                                                 "",
                                                                 TYEdiLayout.wco_AdditionalDocument_ID,  //기간연장승인번호
                                                                 TYUserInfo.EmpNo
                                                                );
                    this.DbConnector.ExecuteTranQuery();

                    //접수처리
                    UP_Set_EDICUSRES_EDICHEXTENDF(TYEdiLayout.wco_Declaration_ID, "Y", "");
                }
            }
        }
        #endregion

        #region  Description : xml 문서 파싱 함수
        private void UP_ParseXml(String path)
        {

            _NodeString = "";

            // Xml 작업을 하기 위한 Xml 문서 생성
            XmlDocument xmlDoc = new XmlDocument();

            // Xml 파일을 불러옵니다.
            xmlDoc.Load(path);

            // 자식 노드를 모두 순환합니다.
            foreach (XmlNode n in xmlDoc.ChildNodes)
            {               
                FetchNodes(n, false);
            }

            _NodeValueList = _NodeString.Split(';');
        }
        private void FetchNodes(XmlNode n, bool isChild)
        {         
            // 노드가 null 인 경우, 함수 실행을 종료한다.
            if (n == null) return;

            // 하위 노드를 다 순환한다.
            foreach (XmlNode n2 in n.ChildNodes)
            {                
                // Xml 요소가 아닌 Text 등의 값이면 순환하지 않는다.
                if (n2.GetType() != typeof(XmlElement)) continue;                

                // 순환하는 모든 노드는 자식으로 처리
                FetchNodes(n2, true);
            }

            // 자식이고, 자식 갯수가 하나면서, Xml 요소가 아닌 경우엔 요소 이름과 값 출력
            if (isChild && n.ChildNodes.Count == 1 && n.ChildNodes[0].GetType() != typeof(XmlElement))
            {
                // Console.WriteLine("요소: {0} / 값: {1}", n.Name, n.InnerText);
                UP_Xml_XPath(n);

                string sNodeAttributes = UP_Get_NodeAttributes(n, n.Name.ToString(), "kcsUnitCode");

                fsFullXPath = fsFullXPath.Substring(1, fsFullXPath.Length - 1) + "/" + n.Name;

                _NodeString += n.Name + "," + n.InnerText + "," + fsFullXPath + "," + sNodeAttributes + ";";                

                fsFullXPath = "";

            }

        }

        private void UP_Xml_XPath(XmlNode n)
        {
            
            try
            {
                if (n.ParentNode.Name != null)
                {
                    if (n.ParentNode.Name.ToString().Substring(0, 3) == "wco")
                    {
                        fsFullXPath = "/"+ n.ParentNode.Name + fsFullXPath;
                        UP_Xml_XPath(n.ParentNode);
                    }
                    
                }
            }
            catch
            {
                return;
            }
        }

        private string UP_Get_NodeAttributes(XmlNode XNode, string NodeName, string AttName)
        {
            string sNodeAttributesList = string.Empty;            

            if (XNode.Attributes.Count > 0)
            {
                for (int i = 0; i < XNode.Attributes.Count; i++)
                {
                    try
                    {
                        if (XNode.Attributes[i].Name == AttName)
                        {
                            sNodeAttributesList = XNode.Attributes[i].Name + "," + XNode.Attributes[i].Value;
                            break;
                        }
                    }
                    catch (NullReferenceException e)
                    {

                    }
                }
            }
            else
            {
                sNodeAttributesList = ",";
            }
            return sNodeAttributesList;
        }
        #endregion

        #region  Description : KCSAPI4 upload 파일 일괄 삭제 및 이동 이벤트
        private void UP_DirFileMove_KCSAPI4()
        {
            int j = 0;
            int iPoint = 0;
            int iIndex = 0;
            string sPathFileName = string.Empty;
            string sFileName = string.Empty;

            string sDirDate = DateTime.Now.ToString("yyyyMMdd");

            string[] _FileList = System.IO.Directory.GetFiles(ConstKCSAPIPath + "\\" + sDirDate + "\\Rcv\\");

            if (System.IO.Directory.Exists(ConstKCSAPIPath + "\\insave") != true)
            {
                System.IO.Directory.CreateDirectory(ConstKCSAPIPath + "\\insave");
            }

            if (_FileList.Length > 0)
            {
                for (int i = 0; i < _FileList.Length; i++)
                {
                    sFileName = _FileList[i].ToString();
                    sPathFileName = sFileName;

                    for (; ; )
                    {
                        j = Convert.ToInt16(sPathFileName.IndexOf("\\", iPoint).ToString());

                        if (j < 0)
                        {
                            break;
                        }
                        else
                        {
                            iPoint = j + 1;
                            iIndex = j;
                        }
                    }

                    sFileName = sPathFileName.Substring(iIndex + 1, sPathFileName.Length - (iIndex + 1));

                    System.IO.File.Delete(ConstKCSAPIPath + "\\insave\\" + sFileName);

                    System.IO.File.Move(sPathFileName, ConstKCSAPIPath + "\\insave\\" + sFileName);
                }
            }

        }
        #endregion


        #endregion       

        #region  Description : BTN61_SAV_InvokerStart 이벤트
        private void BTN61_SAV_InvokerStart(object sender, TButton.ClickEventCheckArgs e)
        {

            try
            {

                this.UP_KCSAPIModulCall();
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message);
                return;
            }
            finally
            {

                //종료시점을 알리기 위해 가상으로 db 호출 한다.
                e.DbConnector.CommandClear();
                e.DbConnector.Attach("TY_P_UT_74B9M226");
                e.DbConnector.ExecuteScalar();
            }
            

        }
        #endregion

        #region  Description : BTN61_SAV_InvokerEnd 이벤트
        private void BTN61_SAV_InvokerEnd(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = e.ArgData as DataSet;

            try
            {                

                string sDirDate = DateTime.Now.ToString("yyyyMMdd");
                string[] Getfiles = System.IO.Directory.GetFiles(TYBase.ConstKCSAPIPath + "\\" + sDirDate + "\\Rcv\\", "*.txt");

                foreach (string file in Getfiles)
                {
                    //파일존재 체크
                    if (System.IO.File.Exists(file))
                    {
                        TXT01_AFFILENAME.SetValue(file);

                        //수신파일 변환
                        if (TXT01_AFFILENAME.GetValue().ToString() != "")
                        {
                            this.UP_KCSAPI4XmlDocment();
                        }
                    }
                }

                this.UP_DirFileMove_KCSAPI4();             
            }
            catch (Exception ex)
            {

                //파일이동
                //this.UP_FileToMove();

                this.ShowMessage(ex.Message);
                return;
            }
            finally
            {
                //파일이동
                //this.UP_FileToMove();
            }

            this.ShowMessage("TY_M_UT_74CGE268");
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion
    }
}
