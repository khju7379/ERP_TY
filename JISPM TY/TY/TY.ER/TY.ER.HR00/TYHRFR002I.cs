using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace TY.ER.HR00
{
    /// <summary>
    /// 이체자료 생성관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.03.18 14:07
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_53IEH701 : 급여이체자료 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_53IEI702 : 급여이체자료 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PAYGUBN : 급여구분
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRFR002I : TYBase
    {
        #region Description :  폼 Load 이벤트
        public TYHRFR002I()
        {
            InitializeComponent();
        }

        private void TYHRFR002I_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_53IEI702, "PAYGUBN");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_53IEI702, "PAYGUBNNM");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_53IEI702, "PAYYYMM");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_53IEI702, "PAYJIDATE");

            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_53IEI702, "TRANSCHECK");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_53IEI702, "TRANSFILECHECK");

            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_53IEI702, "BTNBATCH");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_53IEI702, "BTNDOWN");

            (this.FPS91_TY_S_HR_53IEI702.Sheets[0].Columns[12].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.create;
            (this.FPS91_TY_S_HR_53IEI702.Sheets[0].Columns[13].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.Download;

            this.DTP01_SDATE.SetValue(DateTime.Now.AddMonths(-2).ToString("yyyy-MM"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.SetStartingFocus(this.DTP01_SDATE);

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description :  조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_53IEI702.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_53IEH701", this.DTP01_SDATE.GetString().ToString().Substring(0, 6), this.DTP01_EDATE.GetString().ToString().Substring(0, 6), this.CBH01_PAYGUBN.GetValue().ToString());
            DataTable dt = this.DbConnector.ExecuteDataTable();
            this.FPS91_TY_S_HR_53IEI702.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_HR_53IEI702.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_HR_53IEI702.GetValue(i, "TRANSCHECK").ToString() == "N")
                {
                    this.FPS91_TY_S_HR_53IEI702.ActiveSheet.Cells[i, this.FPS91_TY_S_HR_53IEI702.ActiveSheet.Columns["BTNBATCH"].Index].CellType = UP_GetButtonType("생성", "1", "A");
                }
                else
                {
                    this.FPS91_TY_S_HR_53IEI702.ActiveSheet.Cells[i, this.FPS91_TY_S_HR_53IEI702.ActiveSheet.Columns["BTNBATCH"].Index].CellType = UP_GetButtonType("취소", "1", "D");

                }
                if (this.FPS91_TY_S_HR_53IEI702.GetValue(i, "TRANSFILECHECK").ToString() == "N")
                {
                    this.FPS91_TY_S_HR_53IEI702.ActiveSheet.Cells[i, this.FPS91_TY_S_HR_53IEI702.ActiveSheet.Columns["BTNDOWN"].Index].CellType = UP_GetButtonType("다운", "2", "A");
                }
                else
                {
                    this.FPS91_TY_S_HR_53IEI702.ActiveSheet.Cells[i, this.FPS91_TY_S_HR_53IEI702.ActiveSheet.Columns["BTNDOWN"].Index].CellType = UP_GetButtonType("취소", "2",  "D");
                    
                }
            }

        }
        #endregion

        #region Description :  FPS91_TY_S_HR_53IEI702_ButtonClicked 버튼 이벤트
        private void FPS91_TY_S_HR_53IEI702_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            //처리 버튼
            if (e.Column.ToString() == "12")
            {
                if (FPS91_TY_S_HR_53IEI702.GetValue(e.Row, "TRANSCHECK").ToString() == "N")  //생성
                {
                    //전표발행 유무 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_5AJFI004",
                                            FPS91_TY_S_HR_53IEI702.GetValue(e.Row, "PAYYYMM").ToString(),
                                            "1",
                                            FPS91_TY_S_HR_53IEI702.GetValue(e.Row, "PAYGUBN").ToString(),
                                            "1",
                                            FPS91_TY_S_HR_53IEI702.GetValue(e.Row, "PAYJIDATE").ToString());
                    DataTable djp = this.DbConnector.ExecuteDataTable();
                    if (djp.Rows.Count > 0)
                    {
                        if (djp.Rows[0]["APMJPNO"].ToString() == "")
                        {
                            this.ShowCustomMessage("급여전표가 발행되지않습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        this.ShowCustomMessage("급여전표가 발행되지않습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }

                    //파일다운 이력 체크
                    if (FPS91_TY_S_HR_53IEI702.GetValue(e.Row, "TRANSFILECHECK").ToString() == "Y")
                    {
                        this.ShowCustomMessage("파일다운 자료를 먼저 취소해야합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }

                    //은행코드, 계좌체크
                    //징수관리 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_53JDY729", FPS91_TY_S_HR_53IEI702.GetValue(e.Row, "PAYGUBN").ToString(),
                                                                FPS91_TY_S_HR_53IEI702.GetValue(e.Row, "PAYJIDATE").ToString(),
                                                                FPS91_TY_S_HR_53IEI702.GetValue(e.Row, "PAYGUBN").ToString(),
                                                                FPS91_TY_S_HR_53IEI702.GetValue(e.Row, "PAYYYMM").ToString(),
                                                                FPS91_TY_S_HR_53IEI702.GetValue(e.Row, "PAYJIDATE").ToString());
                    DataTable dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["COINCDBK"].ToString() == "" || dt.Rows[i]["COINNOAC"].ToString() == "")
                            {
                                this.ShowCustomMessage(dt.Rows[i]["COSABUNNM"].ToString() +"("+dt.Rows[i]["COSABUN"].ToString()+")"+
                                                       "급여징수관리 이체계좌를 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }                    
                    //급여이체자료 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_53KAH754", FPS91_TY_S_HR_53IEI702.GetValue(e.Row, "PAYJIDATE").ToString(),
                                                                FPS91_TY_S_HR_53IEI702.GetValue(e.Row, "PAYGUBN").ToString(),
                                                                FPS91_TY_S_HR_53IEI702.GetValue(e.Row, "PAYYYMM").ToString(),
                                                                FPS91_TY_S_HR_53IEI702.GetValue(e.Row, "PAYJIDATE").ToString());
                    DataTable dk = this.DbConnector.ExecuteDataTable();
                    if (dk.Rows.Count > 0)
                    {
                        for (int i = 0; i < dk.Rows.Count; i++)
                        {
                            if (dk.Rows[i]["TCBANKCODE"].ToString() == "" || dk.Rows[i]["TCACCOUNT"].ToString() == "")
                            {
                                this.ShowCustomMessage(dk.Rows[i]["KBHANGL"].ToString() + "(" + dk.Rows[i]["PMSABUN"].ToString() + ")" +
                                                       "   급여 이체계좌를 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }

                    if (this.ShowMessage("TY_M_GB_26E2Z874"))
                    {
                        this.UP_DataCreate(FPS91_TY_S_HR_53IEI702.GetValue(e.Row, "PAYGUBN").ToString(),
                                           FPS91_TY_S_HR_53IEI702.GetValue(e.Row, "PAYYYMM").ToString(),
                                           FPS91_TY_S_HR_53IEI702.GetValue(e.Row, "PAYJIDATE").ToString());
                    }
                }
                else //취소
                {
                    //파일다운 이력 체크
                    if (FPS91_TY_S_HR_53IEI702.GetValue(e.Row, "TRANSFILECHECK").ToString() == "Y")
                    {
                        this.ShowCustomMessage("파일다운 자료를 먼저 취소해야합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }

                    if (this.ShowMessage("TY_M_AC_2CDB0166"))
                    {
                        this.UP_DataDelete(FPS91_TY_S_HR_53IEI702.GetValue(e.Row, "PAYGUBN").ToString(),
                                           FPS91_TY_S_HR_53IEI702.GetValue(e.Row, "PAYYYMM").ToString(),
                                           FPS91_TY_S_HR_53IEI702.GetValue(e.Row, "PAYJIDATE").ToString());
                    }
                }
            }

            //파일다운 버튼
            if (e.Column.ToString() == "13")
            {
                if (FPS91_TY_S_HR_53IEI702.GetValue(e.Row, "TRANSFILECHECK").ToString() == "N")  //생성
                {
                    //파일다운 이력 체크
                    if (FPS91_TY_S_HR_53IEI702.GetValue(e.Row, "TRANSCHECK").ToString() != "Y")
                    {
                        this.ShowCustomMessage("이체자료 생성되지 않았습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }

                    if (this.ShowMessage("TY_M_AC_2B77B165"))
                    {
                        this.UP_FileDown(FPS91_TY_S_HR_53IEI702.GetValue(e.Row, "PAYGUBN").ToString(),
                                           FPS91_TY_S_HR_53IEI702.GetValue(e.Row, "PAYYYMM").ToString(),
                                           FPS91_TY_S_HR_53IEI702.GetValue(e.Row, "PAYJIDATE").ToString());
                    }
                }
                else
                {
                    if (this.ShowMessage("TY_M_AC_2CDB0166"))
                    {
                        this.UP_FileDownCancel(FPS91_TY_S_HR_53IEI702.GetValue(e.Row, "PAYGUBN").ToString(),
                                               FPS91_TY_S_HR_53IEI702.GetValue(e.Row, "PAYYYMM").ToString(),
                                               FPS91_TY_S_HR_53IEI702.GetValue(e.Row, "PAYJIDATE").ToString());
                    }
                }
            }
        }
        #endregion

        #region Description :  이체자료 생성 및 취소
        private void UP_DataCreate(string sPAYGUBN, string sPAYYYMM, string sPAYJIDATE)
        {
            //펌뱅킹 징수관리 등록
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_53JDY729", sPAYGUBN, sPAYJIDATE, sPAYGUBN, sPAYYYMM, sPAYJIDATE);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_53JE0730", dt.Rows[i]["COSABUN"].ToString(),
                                                                dt.Rows[i]["COGUBN"].ToString(),
                                                                dt.Rows[i]["COSDATE"].ToString(),
                                                                sPAYYYMM,
                                                                sPAYGUBN,
                                                                sPAYJIDATE,
                                                                dt.Rows[i]["COINSABUN"].ToString(),
                                                                dt.Rows[i]["COINCDBK"].ToString(),
                                                                dt.Rows[i]["COINNOAC"].ToString(),
                                                                (dt.Rows[i]["COFLAG"].ToString() == "1" ? dt.Rows[i]["COAMT"].ToString(): dt.Rows[i]["MWONUNITAMT"].ToString()),
                                                                TYUserInfo.EmpNo
                                                                );
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            //급여이체자료 등록
            this.DbConnector.CommandClear();
            //급여자료
            this.DbConnector.Attach("TY_P_HR_53JET735", "00", TYUserInfo.EmpNo, sPAYGUBN, sPAYYYMM, sPAYJIDATE);
            //급여징수관리 내역자료 급여이체자료에 추가 등록
            this.DbConnector.Attach("TY_P_HR_53JF2737", TYUserInfo.EmpNo,sPAYYYMM, sPAYGUBN,sPAYJIDATE);
            this.DbConnector.ExecuteTranQueryList();            
            
            this.ShowMessage("TY_M_GB_26E30875");

            if (this.OpenModalPopup(new TYHRFR02C1(sPAYYYMM, sPAYGUBN, sPAYJIDATE)) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);            
        }

        private void UP_DataDelete(string sPAYGUBN, string sPAYYYMM, string sPAYJIDATE)
        {           
            this.DbConnector.CommandClear();
            //펌뱅킹 징수관리 삭제
            this.DbConnector.Attach("TY_P_HR_53JG9743", sPAYYYMM, sPAYGUBN,sPAYJIDATE);
            //급여이체자료 삭제
            this.DbConnector.Attach("TY_P_HR_53JG1744",sPAYYYMM, sPAYGUBN, sPAYJIDATE);
            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_AC_2CDB1167");
        }
        #endregion

        #region Description :  파일다운 
        private void UP_FileDown(string sPAYGUBN, string sPAYYYMM, string sPAYJIDATE)
        {
            string sData      = string.Empty;

            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Title = "펌뱅킹 이체자료 다운로드";
                dlg.Filter = "Text (*.txt)|*.txt";
                dlg.FilterIndex = 0;
                dlg.OverwritePrompt = true;                
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter sw = new StreamWriter(dlg.FileName, false, Encoding.Default);

                    //StreamWriter sw = File.AppendText(dlg.FileName);

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_53K94751", sPAYYYMM, sPAYGUBN, sPAYJIDATE);
                    DataTable dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            
                            sData = string.Format("{0,-5:G}", dt.Rows[i]["E2CDBK"].ToString())+",";  //5자리 은행코드
                            sData += string.Format("{0,-20:G}", dt.Rows[i]["E2NOAC"].ToString()) + ",";  //20자리 계좌번호
                            sData += string.Format("{0,-15:G}", dt.Rows[i]["E2EAMT"].ToString()) + ",";  //15자리 이체금액
                            sData += string.Format("{0,-6:G}", dt.Rows[i]["E2SABUN"].ToString().Substring(0, 4)) + ",";  //6자리  사번                            
                            sData += "태영인더스트리급여";

                            sw.WriteLine(sData);
                        }
                        sw.Close();

                        //다운로드 표시하기
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_HR_53KA7753", "*", sPAYYYMM, sPAYGUBN, sPAYJIDATE);
                        this.DbConnector.ExecuteTranQuery();
                    }
                    this.BTN61_INQ_Click(null, null);
                    this.ShowMessage("TY_M_GB_25UAA711");
                }
            }                                    
            
        }

        private void UP_FileDownCancel(string sPAYGUBN, string sPAYYYMM, string sPAYJIDATE)
        {
            //다운로드 표시하기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_53KA7753", "", sPAYYYMM, sPAYGUBN, sPAYJIDATE);
            this.DbConnector.ExecuteTranQuery();
                    
            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_AC_2CDB1167");
        }
        #endregion

        #region Description :  버튼 타입 변경
        private TButtonCellType UP_GetButtonType(string sButtonText, string sImageCheck, string sGubn)
        {
            TButtonCellType tButtonCellType = new TButtonCellType();
            if (sImageCheck == "1")
            {
                if (sGubn == "A")
                    tButtonCellType.Picture = global::TY.Service.Library.Properties.Resources.create;
                else
                    tButtonCellType.Picture = global::TY.Service.Library.Properties.Resources.cross; 
            }
            else
            {
                if (sGubn == "A")
                    tButtonCellType.Picture = global::TY.Service.Library.Properties.Resources.Download;
                else
                    tButtonCellType.Picture = global::TY.Service.Library.Properties.Resources.cross;  
            }
            tButtonCellType.Text = sButtonText;
            tButtonCellType.TextAlign = FarPoint.Win.ButtonTextAlign.TextRightPictLeft;
            tButtonCellType.TextOrientation = FarPoint.Win.TextOrientation.TextHorizontal;

            return tButtonCellType;
        }
        #endregion

    }
}
