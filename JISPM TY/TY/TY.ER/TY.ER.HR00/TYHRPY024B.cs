using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.IO;

namespace TY.ER.HR00
{
    /// <summary>
    /// 근로소득간이지급명세서 전산매체 생성 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.05.09 15:02
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_959B4519 : 근로소득간이지급명세서 급여 조회
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  INQOPTION : 조회구분
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRPY024B : TYBase
    {
        private string fsSAUPNO = string.Empty;
        private string fsSANGHO = string.Empty;
        private string fsDPMK = string.Empty;
        private string fsDPMKNAME = string.Empty;
        private string fsDPTEL = string.Empty;
        private string fsHOMETAXID = string.Empty;
        private string fsCEONAME = string.Empty;

        #region  Description : 폼 로드 이벤트
        public TYHRPY024B()
        {
            InitializeComponent();
        }

        private void TYHRPY024B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            TXT01_SDATE.SetValue(DateTime.Now.ToString("yyyy"));
            DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.SetStartingFocus(TXT01_SDATE);
        }
        #endregion

        #region  Description : 파일 생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            try
            {
                // 2021.07.28일 수정후
                fsSAUPNO = "6108110449";
                fsSANGHO = "(주)태영인더스트리";
                fsDPMK = "관리팀";
                fsDPMKNAME = "조영래";
                fsDPTEL = "052-228-3311";
                fsHOMETAXID = "TYC2921";
                fsCEONAME = "정세진";

                //파일 생성 경로
                //string sFilePath = "C:\\Temp\\";
                string sFileName = "";

                sFileName = "SC1058516.181";

                this.saveFileDialog.FileName = sFileName;
                if (this.saveFileDialog.ShowDialog() == DialogResult.Cancel) return;
                sFileName = this.saveFileDialog.FileName;

                if (File.Exists(sFileName))
                {
                    File.Delete(sFileName);
                }

                StreamWriter StrWrReCode = new StreamWriter(sFileName, false, Encoding.Default);

                this.UP_Get_HomeTaxFileCreate(StrWrReCode);

                StrWrReCode.Close();
                

                // 원본소스 2021.07.28일 수정전
                //for (int i = 1; i < 3; i++)
                //{
                //    //본지점 선택
                //    if (i != 1)
                //    {
                //        fsSAUPNO = "1058516181";
                //        fsSANGHO = "(주)태영인더스트리서울지점";
                //        fsDPMK = "관리팀";
                //        fsDPMKNAME = "이성재";
                //        fsDPTEL = "02-2090-2619";
                //        fsHOMETAXID = "TYC2922";
                //        fsCEONAME = "정세진";

                //    }
                //    else
                //    {
                //        fsSAUPNO = "6108110449";
                //        fsSANGHO = "(주)태영인더스트리";
                //        fsDPMK = "관리팀";
                //        fsDPMKNAME = "조영래";
                //        fsDPTEL = "052-228-3311";
                //        fsHOMETAXID = "TYC2921";
                //        fsCEONAME = "정세진";
                //    }

                //    //파일 생성 경로
                //    //string sFilePath = "C:\\Temp\\";
                //    string sFileName = "";

                //    sFileName = i != 1 ? "SC1058516.181" : "SC6108110.449";

                //    this.saveFileDialog.FileName = sFileName;
                //    if (this.saveFileDialog.ShowDialog() == DialogResult.Cancel) return;
                //    sFileName = this.saveFileDialog.FileName;

                //    if (File.Exists(sFileName))
                //    {
                //        File.Delete(sFileName);
                //    }

                //    StreamWriter StrWrReCode = new StreamWriter(sFileName, false, Encoding.Default);

                //    this.UP_Get_HomeTaxFileCreate(i.ToString(), StrWrReCode);

                //    StrWrReCode.Close();
                //}

                this.ShowMessage("TY_M_GB_25UAA711");
            }
            catch (Exception ex)
            {
                this.ShowCustomMessage(ex.Message, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {

            }
        }
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_959B4519", this.TXT01_SDATE.GetValue(), "", CBO01_INQOPTION.GetValue().ToString(), TYUserInfo.SecureKey, "Y" );
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowCustomMessage("신고자료가 존재하지 않습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["KBMOBILE"].ToString().Trim() == "" && dt.Rows[i]["KBTELNO"].ToString().Trim() == "")
                    {
                        this.ShowCustomMessage(dt.Rows[i]["KBSABUN"].ToString().Trim() + ":" + dt.Rows[i]["KBHANGL"].ToString().Trim() + " 전화번호가 없습니다!\r\n인사기본사항에 전화번호를 등록하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (!this.ShowMessage("TY_M_AC_2B77B165"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : 파일 생성 함수
        private void UP_Get_HomeTaxFileCreate(string sGubn, StreamWriter StrWrReCode)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_959B4519", this.TXT01_SDATE.GetValue(), sGubn, CBO01_INQOPTION.GetValue().ToString(), TYUserInfo.SecureKey, "Y");
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                //A레코드 제출자 레코드
                this.UP_Made_RecordA(StrWrReCode);

                //B레코드 원천징수의무자별 집계레코드
                this.UP_Made_RecordB(dt, StrWrReCode);

                //C레코드 소득자
                this.UP_Made_RecordC(dt, StrWrReCode);
            }
        }

        private void UP_Get_HomeTaxFileCreate(StreamWriter StrWrReCode)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_B7SDO483", this.TXT01_SDATE.GetValue(), CBO01_INQOPTION.GetValue().ToString(), TYUserInfo.SecureKey, "Y");
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                //A레코드 제출자 레코드
                this.UP_Made_RecordA(StrWrReCode);

                //B레코드 원천징수의무자별 집계레코드
                this.UP_Made_RecordB(dt, StrWrReCode);

                //C레코드 소득자
                this.UP_Made_RecordC(dt, StrWrReCode);
            }
        }
        #endregion

        #region  Description : 근로소득간이지급명세서 A 레코드 생성
        private void UP_Made_RecordA(StreamWriter StrWrReCode)
        {
            string sStrRecord = string.Empty;
            string sStrTemp = string.Empty;

            //A1 레코드구분 X(1)
            sStrRecord = "A";
            //A2 자료구분 9(2)
            sStrRecord = sStrRecord + "77";
            //A3 세무서코드 X(3))
            sStrRecord = sStrRecord + fsSAUPNO.Substring(0, 3);
            //A4 제출연월일 9(8)
            sStrRecord = sStrRecord + DTP01_EDATE.GetString().ToString();
            //A5 제출자구분 9(1) 1-세무대리인 2-법인 3-개인
            sStrRecord = sStrRecord + "2";
            //A6 세무대리인관리번호 X(6)
            sStrRecord = sStrRecord + "      ";
            //A7 홈택스ID X(20)
            sStrRecord = sStrRecord + string.Format("{0,-20:G}", fsHOMETAXID);
            //A8 세무프로그램코드 X(4)
            sStrRecord = sStrRecord + "9000";
            //A9 사업자번호 X(10)
            sStrRecord = sStrRecord + fsSAUPNO;
            //A10 법인명 X(30)
            sStrTemp = "";
            sStrTemp = fsSANGHO;
            sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(fsSANGHO)));
            sStrRecord = sStrRecord + sStrTemp;
            //A11 담당자부서 X(30)
            sStrTemp = "";
            sStrTemp = fsDPMK;
            sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(fsDPMK)));
            sStrRecord = sStrRecord + sStrTemp;

            //A12 담당자성명 X(30)
            sStrTemp = "";
            sStrTemp = fsDPMKNAME;
            sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(fsDPMKNAME)));
            sStrRecord = sStrRecord + sStrTemp;

            //A13 담당자 전화번호 X(15)
            sStrRecord = sStrRecord + string.Format("{0,-15:G}", fsDPTEL);
            //A14 신고의무자수 9(5)
            sStrRecord = sStrRecord + "00001";
            //A15 공란 X(25)
            sStrRecord = sStrRecord + string.Format("{0,-115:G}", "");

            StrWrReCode.WriteLine(sStrRecord);
        }
        #endregion

        #region  Description : 근로소득간이지급명세서 B 레코드 생성
        private void UP_Made_RecordB(DataTable dt, StreamWriter StrWrReCode)
        {
            string sStrRecord = string.Empty;
            string sStrTemp = string.Empty;
            //B1 레코드구분 X(1)
            sStrRecord = "B";
            //B2 자료구분 9(2)
            sStrRecord = sStrRecord + "77";
            //B3 세무서코드 X(3))
            sStrRecord = sStrRecord + fsSAUPNO.Substring(0, 3);
            //B4 일련번호 9(6)
            sStrRecord = sStrRecord + "000001";

            //B5 법인명 X(40)
            sStrTemp = "";
            sStrTemp = fsSANGHO;
            sStrTemp += new String(Convert.ToChar(" "), (40 - Encoding.Default.GetByteCount(fsSANGHO)));
            sStrRecord = sStrRecord + sStrTemp;

            //B6 대표자 X(30)
            sStrTemp = "";
            sStrTemp = fsCEONAME;
            sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(fsCEONAME)));
            sStrRecord = sStrRecord + sStrTemp;

            //B7 사업자등록번호 X(10)
            sStrRecord = sStrRecord + fsSAUPNO;

            //B8 주민번호(법인등록번호) X(13)
            sStrRecord = sStrRecord + "1812110012745";

            //B9 귀속년도 9(4)
            sStrRecord = sStrRecord + TXT01_SDATE.GetValue().ToString();

            //B10 근무시기 9(1) 1 상반기 2 하반기
            sStrRecord = sStrRecord + CBO01_INQOPTION.GetValue().ToString();

            //B11 근로자수 9(10)
            sStrRecord = sStrRecord + String.Format("{0:D10}", Convert.ToInt64(dt.Rows.Count));

            //B12 과세소득합계(급여) 9(13)
            if (CBO01_INQOPTION.GetValue().ToString() == "1")  //상반기
            {
                sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Compute("SUM(FHALFAMOUNT)", "").ToString()));
            }
            else  //하반기
            {
                sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Compute("SUM(SHALFAMOUNT)", "").ToString()));
            }

            //B13 과세소득합계(상여) 9(13)
            sStrRecord = sStrRecord + "0000000000000";
            
            //B14 공란 X(44)
            sStrRecord = sStrRecord + string.Format("{0,-134:G}", "");

            StrWrReCode.WriteLine(sStrRecord);
        }
        #endregion

        #region  Description : 근로소득간이지급명세서 C 레코드 생성
        private void UP_Made_RecordC(DataTable dt, StreamWriter StrWrReCode)
        {
            string sStrRecord = string.Empty;
            string sStrTemp = string.Empty;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //C1 레코드 구분 X(1) 1 영문 대문자 “C” 영문 대문자 ‘C’ 아니면 오류
                sStrRecord = "C";
                //C2 자료구분 9(2) 3 근로소득 - 숫자 “77” ‘77’이 아니면 오류
                sStrRecord = sStrRecord + "77";
                //C3 세무서코드 X(3) 6 원천징수의무자의 납세지관할 세무서코드 ※B레코드의 세무서코드와 항상 일치 [C3] ≠ [B3]이면 오류
                sStrRecord = sStrRecord + fsSAUPNO.Substring(0, 3);
                //C4 일련번호 9(7) 12 원천징수의무자별로 1부터 순차 부여
                sStrRecord = sStrRecord + String.Format("{0:D7}", i+1);

                //C5 ③사업자등록번호 X(10) 22 원천징수의무자의 사업자등록번호 ※B레코드의 사업자등록번호와 항상 일치 1.[C5] ≠ [B5]이면 오류 2.잘못된 사업자등록번호 입력 시 오류
                sStrRecord = sStrRecord + fsSAUPNO;
              
                //C6 ⑦주민등록번호 X(13)
                sStrRecord = sStrRecord + dt.Rows[i]["KBJUMIN"].ToString().Trim().Replace("-", "");

                //C7 ⑨성명 X(30) 127
                sStrTemp = "";
                sStrTemp = dt.Rows[i]["KBHANGL"].ToString().Trim();
                sStrTemp += new String(Convert.ToChar(" "), (30 - Encoding.Default.GetByteCount(dt.Rows[i]["KBHANGL"].ToString().Trim())));
                sStrRecord = sStrRecord + sStrTemp;

                //C8 전화번호 X20
                string sTelno = dt.Rows[i]["KBMOBILE"].ToString().Trim().Replace("-", "") != "" ? dt.Rows[i]["KBMOBILE"].ToString().Trim().Replace("-", "") : dt.Rows[i]["KBTELNO"].ToString().Trim().Replace("-", "");
                sStrRecord = sStrRecord + string.Format("{0,-20:G}", sTelno);

                //C9 내·외국인 9(1) 1:내국인 2:외국인
                sStrRecord = sStrRecord + "1";

                //C10 거주자구분 9(1) 1:거주자 2:비거주자
                sStrRecord = sStrRecord + "1";

                //C11 거주지국코드 X(2) 공란
                sStrRecord = sStrRecord + string.Format("{0,-2:G}", "");

                //C12 근무기간시작년월일
                if (CBO01_INQOPTION.GetValue().ToString() == "1")  //상반기
                {
                    sStrRecord = sStrRecord + TXT01_SDATE.GetValue().ToString() + "0101";
                }
                else
                {
                    sStrRecord = sStrRecord + TXT01_SDATE.GetValue().ToString() + "0701";
                }

                //C13 근무기간종료년월일
                if (CBO01_INQOPTION.GetValue().ToString() == "1")  //상반기
                {
                    sStrRecord = sStrRecord + TXT01_SDATE.GetValue().ToString() + "0630";
                }
                else
                {
                    sStrRecord = sStrRecord + TXT01_SDATE.GetValue().ToString() + "1231";
                }

                //C14 과세소득 9(13)
                if (CBO01_INQOPTION.GetValue().ToString() == "1")  //상반기
                {
                    //⑱급여 등(1월/7월)
                    sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Rows[i]["FHALFAMOUNT01"].ToString().Trim()));
                    //⑲인정상여(1월/7월)
                    sStrRecord = sStrRecord + "0000000000000";
                    //⑱급여 등(2월/8월)
                    sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Rows[i]["FHALFAMOUNT02"].ToString().Trim()));
                    //⑲인정상여(2월/8월)
                    sStrRecord = sStrRecord + "0000000000000";
                    //⑱급여 등(3월/9월)
                    sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Rows[i]["FHALFAMOUNT03"].ToString().Trim()));
                    //⑲인정상여(3월/9월)
                    sStrRecord = sStrRecord + "0000000000000";
                    //⑱급여 등(4월/10월)
                    sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Rows[i]["FHALFAMOUNT04"].ToString().Trim()));
                    //⑲인정상여(4월/10월)
                    sStrRecord = sStrRecord + "0000000000000";
                    //⑱급여 등(5월/11월)
                    sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Rows[i]["FHALFAMOUNT05"].ToString().Trim()));
                    //⑲인정상여(5월/11월)
                    sStrRecord = sStrRecord + "0000000000000";
                    //⑱급여 등(6월/12월)
                    sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Rows[i]["FHALFAMOUNT06"].ToString().Trim()));
                    //⑲인정상여(6월/12월)
                    sStrRecord = sStrRecord + "0000000000000";

                    
                }
                else
                {
                    //⑱급여 등(1월/7월)
                    sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Rows[i]["FHALFAMOUNT07"].ToString().Trim()));
                    //⑲인정상여(1월/7월)
                    sStrRecord = sStrRecord + "0000000000000";
                    //⑱급여 등(2월/8월)
                    sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Rows[i]["FHALFAMOUNT08"].ToString().Trim()));
                    //⑲인정상여(2월/8월)
                    sStrRecord = sStrRecord + "0000000000000";
                    //⑱급여 등(3월/9월)
                    sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Rows[i]["FHALFAMOUNT09"].ToString().Trim()));
                    //⑲인정상여(3월/9월)
                    sStrRecord = sStrRecord + "0000000000000";
                    //⑱급여 등(4월/10월)
                    sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Rows[i]["FHALFAMOUNT10"].ToString().Trim()));
                    //⑲인정상여(4월/10월)
                    sStrRecord = sStrRecord + "0000000000000";
                    //⑱급여 등(5월/11월)
                    sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Rows[i]["FHALFAMOUNT11"].ToString().Trim()));
                    //⑲인정상여(5월/11월)
                    sStrRecord = sStrRecord + "0000000000000";
                    //⑱급여 등(6월/12월)
                    sStrRecord = sStrRecord + String.Format("{0:D13}", Convert.ToInt64(dt.Rows[i]["FHALFAMOUNT12"].ToString().Trim()));
                    //⑲인정상여(6월/12월)
                    sStrRecord = sStrRecord + "0000000000000";
                }                

                // C16 공란
                sStrRecord = sStrRecord + string.Format("{0,-18:G}", "");

                StrWrReCode.WriteLine(sStrRecord);
            }
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
