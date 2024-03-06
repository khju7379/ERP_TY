using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;
using GrapeCity.ActiveReports;
using TY.ER.GB00;

namespace TY.ER.HR00
{
    /// <summary>
    /// 연말정산 제출자료 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.06.15 15:49
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
    ///  INQ : 조회
    ///  SAV : 저장
    /// </summary>
    public partial class TYHRNT001I : TYBase
    {
        private string fsComPany = string.Empty;
        private string fsYEAR = string.Empty;
        private string fsSABUN = string.Empty;

        private string fsFixGubn = string.Empty;




        #region  Description : 폼 로드 이벤트
        public TYHRNT001I(string sComPany, string sYear, string sSABUN)
        {
            InitializeComponent();
            fsComPany = sComPany;
            fsYEAR = sYear;
            fsSABUN = sSABUN;

        }

        private void TYHRNT001I_Load(object sender, System.EventArgs e)
        {


            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.BTN61_SEND.ProcessCheck += new TButton.CheckHandler(BTN61_SEND_ProcessCheck);
            this.BTN61_CANCEL.ProcessCheck += new TButton.CheckHandler(BTN61_CANCEL_ProcessCheck);


            BTN61_TAXRESULT.Visible = false;
            BTN62_TAXRESULT.Visible = false;

            DTP01_KBIDATE.SetReadOnly(true);

            //this.TXT01_SDATE.SetValue(DateTime.Now.AddYears(-1).ToString("yyyy"));

            this.TXT01_SDATE.SetValue(fsYEAR);

            this.CBH01_KBBSTEAM.DummyValue = this.TXT01_SDATE.GetValue().ToString() + "0101";

            this.CBH01_KBSABUN.SetValue(fsSABUN);

            UP_Spread_PayItemTitle();

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 연말정산 제출 상태
        private void UP_TaxDataSubMit_Status()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77LD4260", fsComPany, TXT01_SDATE.GetValue().ToString().Substring(0, 4), this.CBH01_KBSABUN.GetValue().ToString(), "", "", "", "");
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                BTN61_INQ_ETC.Visible = true;
                BTN61_INQ_PWKCOMPY.Visible = true;

                TXT01_TAXSTATUS.SetValue(dt.Rows[0]["TAXSTATUS"].ToString());

                switch (dt.Rows[0]["TAXSTATUS"].ToString())
                {
                    case "작성중":
                        TXT01_TAXSTATUS.ForeColor = System.Drawing.Color.Black;
                        break;
                    case "신고서제출":
                        TXT01_TAXSTATUS.ForeColor = System.Drawing.Color.Black;
                        break;
                    case "신고서마감":
                        TXT01_TAXSTATUS.ForeColor = System.Drawing.Color.Blue;
                        break;
                    case "정산완료":
                        TXT01_TAXSTATUS.ForeColor = System.Drawing.Color.Red;
                        break;
                    default:
                        break;
                }

                fsFixGubn = dt.Rows[0]["ADSUBMIT"].ToString();

                //개인확정 Y , 공제신고서 마감 Y
                if (dt.Rows[0]["ADSUBMIT"].ToString() == "Y" && dt.Rows[0]["ADDEDDOC"].ToString() == "Y")
                {
                    UP_Set_ButtonDisPlay(false, false, false, false, false, true);
                }
                else if (dt.Rows[0]["ADSUBMIT"].ToString() == "Y" && dt.Rows[0]["ADDEDDOC"].ToString() == "N")
                {
                    //개인확정 Y , 공제신고서 마감 N
                    UP_Set_ButtonDisPlay(false, false, false, false, true, true);
                }
                else
                {
                    UP_Set_ButtonDisPlay(true, true, true, true, false, false);
                }

                //정산결과버튼
                if (dt.Rows[0]["ADDEDTAX"].ToString() == "Y")
                {
                    BTN61_TAXRESULT.Visible = true;
                    BTN62_TAXRESULT.Text = "정산출력";
                    BTN62_TAXRESULT.Visible = true;
                }

            }
            else
            {
                BTN61_INQ_ETC.Visible = false;
                BTN61_INQ_PWKCOMPY.Visible = false;
                UP_Set_ButtonDisPlay(false, false, false, false, false, false);
                TXT01_TAXSTATUS.SetValue("");
                this.ShowCustomMessage("연말정산 대상자가 아닙니다! 대상자를 먼저 생성하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
        }
        #endregion        

        #region  Description : 개인 기본정보 이벤트
        private void UP_DataBing()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BBGV367", "", this.CBH01_KBSABUN.GetValue().ToString(), TYUserInfo.SecureKey, "Y");
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                CBH01_KBBSTEAM.SetValue(dt.Rows[0]["KBBSTEAM"].ToString());
                CBH01_KBJKCD.SetValue(dt.Rows[0]["KBJKCD"].ToString());
                DTP01_KBIDATE.SetValue(dt.Rows[0]["KBIDATE"].ToString());
                TXT01_KBJUSO.SetValue(dt.Rows[0]["KBJUSO"].ToString());
            }
        }
        #endregion        

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            UP_DataBing();

            UP_Family_DataBinding();

            UP_FamilyInComeList_DataBinding();

            UP_TaxDataSubMit_Status();
        }
        #endregion

        #region  Description : 홈택스 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {

            //국세청 자료 제출 이력이 있으면 조회 화면으로 바로 간다.
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77S8Z291", fsComPany, TXT01_SDATE.GetValue(), CBH01_KBSABUN.GetValue());
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                if ((new TYHRNT001P(fsComPany, TXT01_SDATE.GetValue().ToString(), this.CBH01_KBSABUN.GetValue().ToString(), fsFixGubn)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
            }
            else
            {
                if ((new TYHRNT001B(fsComPany, TXT01_SDATE.GetValue().ToString(), CBH01_KBSABUN.GetValue().ToString(), fsFixGubn, "1")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);


            }

        }
        #endregion

        #region  Description : 부양가족 자료 조회
        private void UP_Family_DataBinding()
        {
            this.FPS91_TY_S_HR_77BD2109.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77BB4098", fsComPany, TXT01_SDATE.GetValue(), CBH01_KBSABUN.GetValue(), TYUserInfo.SecureKey, "Y");
            this.FPS91_TY_S_HR_77BD2109.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 소득자명세서 자료 조회
        private void UP_FamilyInComeList_DataBinding()
        {
            this.FPS91_TY_S_HR_77BFG112.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_7CBD4221", fsComPany, TXT01_SDATE.GetValue(), CBH01_KBSABUN.GetValue(), TYUserInfo.SecureKey, "Y");
            this.FPS91_TY_S_HR_77BFG112.SetValue(this.DbConnector.ExecuteDataTable());
            if (this.FPS91_TY_S_HR_77BFG112.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_HR_77BFG112.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_HR_77BFG112.GetValue(i, "WFNAME").ToString() == "[합 계]")
                    {
                        this.FPS91_TY_S_HR_77BFG112.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);

                        this.FPS91_TY_S_HR_77BFG112.ActiveSheet.Rows[i].Font = new Font("맑은 고딕", 9, FontStyle.Bold);
                    }
                }
            }
        }
        #endregion

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYHRNT01C1(fsComPany, TXT01_SDATE.GetValue().ToString(), CBH01_KBSABUN.GetValue().ToString(), string.Empty, fsFixGubn)) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description :  FPS91_TY_S_HR_77BD2109_CellDoubleClick 이벤트
        private void FPS91_TY_S_HR_77BD2109_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.OpenModalPopup(new TYHRNT01C1(this.FPS91_TY_S_HR_77BD2109.GetValue("WFCOMPANY").ToString(),
                                                   this.FPS91_TY_S_HR_77BD2109.GetValue("WFYEAR").ToString(),
                                                   this.FPS91_TY_S_HR_77BD2109.GetValue("WFSABUN").ToString(),
                                                   this.FPS91_TY_S_HR_77BD2109.GetValue("WFJUMIN").ToString(),
                                                   fsFixGubn)) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {

            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            if (dt.Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_77BBH101", dt.Rows[i]["WFCOMPANY"].ToString(), dt.Rows[i]["WFYEAR"].ToString(), dt.Rows[i]["WFSABUN"].ToString(),
                                                                TYUserInfo.SecureKey, "Y", dt.Rows[i]["WFJUMIN"].ToString().Replace("-", ""));
                }
                this.DbConnector.ExecuteTranQueryList();

                UP_ProCedure_FixCall();
            }

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);
        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            bool bTaxCheck = false;

            DataTable dt = this.FPS91_TY_S_HR_77BD2109.GetDataSourceInclude(TSpread.TActionType.Remove, "WFCOMPANY", "WFYEAR", "WFSABUN", "WFJUMIN", "WFGUBUN");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            //소득명세자료가 존재하는지 체크
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["WFGUBUN"].ToString() == "0")
                    {
                        this.ShowCustomMessage("소득자본인은 삭제가 불가합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_7C4HA168", dt.Rows[i]["WFCOMPANY"].ToString(), dt.Rows[i]["WFYEAR"].ToString(), dt.Rows[i]["WFSABUN"].ToString(),
                                                                dt.Rows[i]["WFJUMIN"].ToString().Replace("-", ""), TYUserInfo.SecureKey, "Y");
                    DataTable dk = this.DbConnector.ExecuteDataTable();
                    if (dk.Rows.Count > 0)
                    {
                        if (Convert.ToDouble(dk.Rows[0]["TAXAMT"].ToString()) + Convert.ToDouble(dk.Rows[0]["NOTTAXAMT"].ToString()) > 0)
                        {
                            bTaxCheck = true;
                        }
                    }
                }
            }

            if (bTaxCheck)
            {
                if (!this.ShowMessage("TY_M_HR_7C4HE169"))
                {
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (!this.ShowMessage("TY_M_GB_23NAD872"))
                {
                    e.Successed = false;
                    return;
                }
            }

            e.ArgData = dt;
        }
        #endregion

        #region  Description : 종전근무지 등록 버튼 이벤트
        private void BTN61_INQ_PWKCOMPY_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYHRNT01C2(fsComPany, TXT01_SDATE.GetValue().ToString(), CBH01_KBSABUN.GetValue().ToString(), fsFixGubn)) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion     

        #region  Description : 정산명세관리 버튼 이벤트
        private void BTN61_INQ_ETC_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYHRNT002I(fsComPany, TXT01_SDATE.GetValue().ToString(), CBH01_KBSABUN.GetValue().ToString(), fsFixGubn)) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion      

        #region Description : 소득명세 스프레드 타이틀 변경
        private void UP_Spread_PayItemTitle()
        {
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_HR_77BFG112_Sheet1.RowHeaderColumnCount = 1;

            for (int i = 0; i < 23; i++)
            {
                this.FPS91_TY_S_HR_77BFG112_Sheet1.AddColumnHeaderSpanCell(0, i, 2, 1);
            }

            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 0].Value = "회사구분";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 1].Value = "귀속년도";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 2].Value = "사 번";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 3].Value = "성 명";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 4].Value = "주민등록번호";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 5].Value = "가족코드";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 6].Value = "가족구분";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 7].Value = "가족관계";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 8].Value = "가족관계";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 9].Value = "교육구분";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 10].Value = "교육구분";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 11].Value = "기본공제";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 12].Value = "장애인";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 13].Value = "부녀자";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 14].Value = "6세이하";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 15].Value = "출산.입양자";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 16].Value = "한부모";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 17].Value = "국적";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 18].Value = "국적";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 19].Value = "자료구분";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 20].Value = "자료구분";

            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 21].Value = "건강보험";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 22].Value = "고용보험";

            this.FPS91_TY_S_HR_77BFG112_Sheet1.AddColumnHeaderSpanCell(0, 23, 1, 2);

            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[0, 23].Value = "보험료";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 23].Value = "보장성";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 24].Value = "장애인";

            this.FPS91_TY_S_HR_77BFG112_Sheet1.AddColumnHeaderSpanCell(0, 25, 1, 4);
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 25].Value = "의료비";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 25].Value = "일반";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 26].Value = "난임.선천";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 27].Value = "경로장애";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 28].Value = "제외금액";

            this.FPS91_TY_S_HR_77BFG112_Sheet1.AddColumnHeaderSpanCell(0, 29, 1, 2);
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 29].Value = "교육비";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 29].Value = "일반";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 30].Value = "장애인";

            this.FPS91_TY_S_HR_77BFG112_Sheet1.AddColumnHeaderSpanCell(0, 31, 1, 8);
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[0, 31].Value = "신용카드등 사용액";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 31].Value = "신용카드";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 32].Value = "직불카드";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 33].Value = "현   금";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 34].Value = "도서공연";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 35].Value = "대중교통";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 36].Value = "전통시장";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 37].Value = "전년합계";
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 38].Value = "당해합계";


            this.FPS91_TY_S_HR_77BFG112_Sheet1.AddColumnHeaderSpanCell(0, 39, 2, 1);
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[1, 39].Value = "기부금";

            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[0, 23].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[0, 25].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[0, 29].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_77BFG112_Sheet1.ColumnHeader.Cells[0, 31].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion

        #region  Description : 제출확정, 제출취소 버튼 이벤트
        private void BTN61_SEND_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_7C5E6181", "Y", TYUserInfo.EmpNo, fsComPany, TXT01_SDATE.GetValue().ToString(), this.CBH01_KBSABUN.GetValue().ToString());
            //인사기본 주소 UPDATE
            this.DbConnector.Attach("TY_P_HR_81PDZ537", TXT01_KBJUSO.GetValue().ToString().Trim(), TYUserInfo.EmpNo, this.CBH01_KBSABUN.GetValue().ToString());
            this.DbConnector.ExecuteTranQueryList();

            this.UP_ProCedure_FixCall();

            UP_TaxDataSubMit_Status();

            this.ShowMessage("TY_M_GB_3A81B997");
        }
        private void BTN61_SEND_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            double dEduAmount = 0;

            //교육비 금액이 있는데 기본사항에 교육비구분이 설정되어 있지 않으면 메세지 출력
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77BB4098", fsComPany, TXT01_SDATE.GetValue(), CBH01_KBSABUN.GetValue(), TYUserInfo.SecureKey, "Y");
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dEduAmount = Convert.ToDouble(dt.Rows[i]["WFTAXGYOUK"].ToString()) + Convert.ToDouble(dt.Rows[i]["WFTAXGYOUKJANG"].ToString()) +
                                 Convert.ToDouble(dt.Rows[i]["WFGYOUK"].ToString()) + Convert.ToDouble(dt.Rows[i]["WFGYOUKJANG"].ToString());

                    if (dEduAmount > 0 && dt.Rows[i]["WFEDUGN"].ToString() == "")
                    {
                        this.ShowCustomMessage(dt.Rows[i]["WFNAME"].ToString() + ": 교육비 금액이 존재합니다. 교육구분 선택을 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (!this.ShowMessage("TY_M_HR_7C5E2179"))
            {
                e.Successed = false;
                return;
            }
        }

        private void BTN61_CANCEL_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_7C5E6181", "N", TYUserInfo.EmpNo, fsComPany, TXT01_SDATE.GetValue().ToString(), this.CBH01_KBSABUN.GetValue().ToString());
            this.DbConnector.ExecuteTranQuery();

            UP_TaxDataSubMit_Status();

            this.ShowMessage("TY_M_AC_2CDB1167");
        }
        private void BTN61_CANCEL_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_HR_7C5E4180"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : 연말정산 정산결과 버튼 이벤트 
        private void BTN61_TAXRESULT_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYHRNT003I(fsComPany, TXT01_SDATE.GetValue().ToString(), CBH01_KBSABUN.GetValue().ToString())) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);

        }
        #endregion

        #region Description : 공제신고서 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            //DataSet ds = new DataSet();

            //공제신고서
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_7CLI5349", TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", fsComPany, TXT01_SDATE.GetValue().ToString(), this.CBH01_KBSABUN.GetValue().ToString());
            DataTable dMaster = this.DbConnector.ExecuteDataTable();

            //부양가족
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_7CREZ376", fsComPany, TXT01_SDATE.GetValue().ToString(), this.CBH01_KBSABUN.GetValue().ToString(), TYUserInfo.SecureKey, "Y");
            DataTable dFamy = this.DbConnector.ExecuteDataTable();

            SectionReport rpt = new TYHRNT002R1(dFamy);

            rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Default;

            (new TYERGB001P(rpt, dMaster)).ShowDialog();
        }
        #endregion

        #region Description : 버튼 표시 
        private void UP_Set_ButtonDisPlay(bool bNew, bool bSave, bool bRem, bool bSend, bool bCancel, bool bPrt)
        {
            BTN61_NEW.Visible = bNew;
            BTN61_SAV.Visible = bSave;
            BTN61_REM.Visible = bRem;
            BTN61_SEND.Visible = bSend;
            BTN61_CANCEL.Visible = bCancel;
            BTN61_PRT.Visible = bPrt;

        }
        #endregion

        #region  Description : 연말정산 국세청 확정 프로시저 호출 함수
        private void UP_ProCedure_FixCall()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77JDB223", fsComPany, TXT01_SDATE.GetValue().ToString(), CBH01_KBSABUN.GetValue(), TYUserInfo.EmpNo, TYUserInfo.SecureKey, "Y", "");
            this.DbConnector.ExecuteScalar();
        }
        #endregion       

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 정산결과 출력
        private void BTN62_TAXRESULT_Click(object sender, EventArgs e)
        {
            SectionReport rpt;

            this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_HR_7CT9B380", fsComPany, TXT01_SDATE.GetValue().ToString(), this.CBH01_KBSABUN.GetValue().ToString(), "", "");
            this.DbConnector.Attach("TY_P_HR_7CT9B380", fsComPany, TXT01_SDATE.GetValue().ToString(), this.CBH01_KBSABUN.GetValue().ToString(), "", TYUserInfo.SecureKey, "Y");
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (Convert.ToInt16(TXT01_SDATE.GetValue().ToString()) > 2017)
            {
                rpt = new TYHRNT003R4();
            }
            else
            {
                rpt = new TYHRNT003R1();
            }

            rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Default;

            (new TYERGB001P(rpt, dt)).ShowDialog();
        }
        #endregion

        #region Description : 부양가족 빈 row 추가

        #endregion





    }
}
