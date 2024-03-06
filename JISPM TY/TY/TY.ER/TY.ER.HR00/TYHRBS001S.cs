using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using System.Drawing;

namespace TY.ER.HR00
{
    /// <summary>
    /// 개인 급여 통합 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.12.23 10:10
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4CNJ0949 : 개인 급여 통합 조회(급여지급관리)
    ///  TY_P_HR_4CNJ2950 : 개인 급여 통합 조회(급여결과마스타)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_4CNJE951 : 개인 급여 통합 조회(급여지급관리)
    ///  TY_S_HR_85EEK026 : 개인 급여 통합 조회(급여결과마스타)
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  INQ_FXM : 조회
    ///  KBBUSEO : 부서
    ///  KBJKCD : 직급
    ///  KBSABUN : 사번
    ///  PAYGUBN : 급여구분
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYHRBS001S : TYBase
    {
        string fsGUBN = string.Empty;

        #region Description : 페이지 로드
        public TYHRBS001S()
        {
            InitializeComponent();

            this.CBH01_BPMDPAC.DummyValue = DateTime.Now.ToString("yyyyMMdd");
        }

        private void TYHRBS001S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            UP_Spread_Title("LOAD");

            this.TXT01_BPMYEAR.SetValue(DateTime.Now.ToString("yyyyMMdd").Substring(0, 4));

            this.SetStartingFocus(this.TXT01_BPMYEAR);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_85EEK026.Initialize();

            string sJKCD  = string.Empty;
            string sBUSEO = string.Empty;

            if (this.CBO01_INQOPTION.GetValue().ToString() == "1") // 임원
            {
                sJKCD = "01";
            }
            else if (this.CBO01_INQOPTION.GetValue().ToString() == "2") // 연봉직
            {
                sJKCD = "1A,1B,2A,2B";
            }
            else if (this.CBO01_INQOPTION.GetValue().ToString() == "3") // 사무직
            {
                sJKCD = "3B,3A,2C,6C";
            }
            else if (this.CBO01_INQOPTION.GetValue().ToString() == "4") // 운영
            {
                sJKCD = "3C,3D";
            }

            if (this.CBH01_BPMDPAC.GetValue().ToString() != "")
            {
                sBUSEO = this.CBH01_BPMDPAC.GetValue().ToString().Substring(0, 2).ToString();
            }

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_85FEJ039", sJKCD.ToString(),
                                                        this.TXT01_BPMYEAR.GetValue().ToString(),
                                                        sBUSEO.ToString(),
                                                        this.CBH01_BPMSABUN.GetValue().ToString()
                                                        );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_8BDID123", sJKCD.ToString(),
                                                            this.TXT01_BPMYEAR.GetValue().ToString(),
                                                            sBUSEO.ToString(),
                                                            this.CBH01_BPMSABUN.GetValue().ToString()
                                                            );
                Int16 iRate = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());

                UP_Spread_Title(iRate.ToString());
            }

            this.FPS91_TY_S_HR_85EEK026.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_HR_85EEK026.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_HR_85EEK026.GetValue(i, "GUBUN").ToString() == "HAP")
                    {
                        // 특정 칼럼 색깔 입히기
                        this.FPS91_TY_S_HR_85EEK026.ActiveSheet.Rows[i].BackColor = Color.SkyBlue;
                        this.FPS91_TY_S_HR_85EEK026.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);
                    }
                }
            }
        }
        #endregion

        #region Description : 신규버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYHRBS001I("", "", "")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // 마스터 삭제
                this.DbConnector.Attach("TY_P_HR_85NI2085", Get_Numeric(this.TXT01_BPMYEAR.GetValue().ToString()),
                                                            dt.Rows[i]["BPMSEQ"].ToString(),
                                                            dt.Rows[i]["BPMSABUN"].ToString()
                                                            );

                // 내역 삭제
                this.DbConnector.Attach("TY_P_HR_85NIB086", Get_Numeric(this.TXT01_BPMYEAR.GetValue().ToString()),
                                                            dt.Rows[i]["BPMSEQ"].ToString(),
                                                            dt.Rows[i]["BPMSABUN"].ToString()
                                                            );
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 기초자료 생성 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            TYHRBS001B popup = new TYHRBS001B();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (popup.fsBPMYEAR.ToString() != "")
                {
                    this.TXT01_BPMYEAR.SetValue(popup.fsBPMYEAR.ToString());

                    this.BTN61_INQ_Click(null, null);
                }
            }
        }
        #endregion

        #region Description : 인상율 생성 버튼
        private void BTN61_BATCH_JASAN_Click(object sender, EventArgs e)
        {
            TYHRBS01B1 popup = new TYHRBS01B1();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (popup.fsBPMYEAR.ToString() != "")
                {
                    this.TXT01_BPMYEAR.SetValue(popup.fsBPMYEAR.ToString());

                    this.BTN61_INQ_Click(null, null);
                }
            }
        }
        #endregion

        #region Description : 그리드 더블클릭 이벤트
        private void FPS91_TY_S_HR_85EEK026_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.FPS91_TY_S_HR_85EEK026.GetValue("GUBUN").ToString() == "HAP")
            {
                ShowMessage("TY_M_MR_2BF8A365");
            }
            else
            {
                string sBPMYEAR  = string.Empty;
                string sBPMSEQ   = string.Empty;
                string sBPMSABUN = string.Empty;

                sBPMYEAR  = this.FPS91_TY_S_HR_85EEK026.GetValue("BPMYEAR").ToString();
                sBPMSEQ   = this.FPS91_TY_S_HR_85EEK026.GetValue("BPMSEQ").ToString();
                sBPMSABUN = this.FPS91_TY_S_HR_85EEK026.GetValue("BPMSABUN").ToString();

                if ((new TYHRBS001I(sBPMYEAR.ToString(), sBPMSEQ.ToString(), sBPMSABUN.ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region Description : 스프레드 타이틀 변경
        private void UP_Spread_Title(string sGUBUN)
        {
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_HR_85EEK026_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_HR_85EEK026_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_HR_85EEK026_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_HR_85EEK026_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);

            this.FPS91_TY_S_HR_85EEK026_Sheet1.AddColumnHeaderSpanCell(0, 3, 1, 9);

            this.FPS91_TY_S_HR_85EEK026_Sheet1.AddColumnHeaderSpanCell(0, 3, 1, 9);

            this.FPS91_TY_S_HR_85EEK026_Sheet1.AddColumnHeaderSpanCell(0, 12, 1, 4);

            this.FPS91_TY_S_HR_85EEK026_Sheet1.AddColumnHeaderSpanCell(0, 16, 1, 4);

            this.FPS91_TY_S_HR_85EEK026_Sheet1.AddColumnHeaderSpanCell(0, 20, 2, 1);


            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[0, 0].Value = "NO";
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[0, 1].Value = "성  명";
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[0, 2].Value = "직  급";

            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[0, 3].Value  = "현  행";

            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[0, 12].Value = "정기승호";
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[0, 16].Value = "인상율" + "(" + sGUBUN + "%)";

            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[0, 20].Value = "연차수당";

            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 3].Value = "기본급";
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 4].Value = "공장";
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 5].Value = "관리";
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 6].Value = "제수당";
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 7].Value = "월급여";
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 8].Value = "월상여";
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 9].Value = "OT";
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 10].Value = "월차보전";
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 11].Value = "OT보전";
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 12].Value = "호봉";

            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 13].Value = "월급여";
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 14].Value = "월상여";
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 15].Value = "OT";

            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 16].Value = sGUBUN + "%";
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 17].Value = "월급여";
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 18].Value = "월상여";
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 19].Value = "OT";            

            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[0, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[0, 12].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[0, 16].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[0, 20].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 13].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 14].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 15].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 16].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 17].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 18].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.FPS91_TY_S_HR_85EEK026_Sheet1.ColumnHeader.Cells[1, 19].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
        }
        #endregion

        #region Description : 삭제 ProcessCheck
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_85EEK026.GetDataSourceInclude(TSpread.TActionType.Remove, "BPMSEQ", "BPMSABUN", "GUBUN");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["GUBUN"].ToString() == "HAP")
                {
                    ShowMessage("TY_M_MR_2BF8A365");
                    e.Successed = false;
                    return;
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_85SDA117",
                                        TXT01_BPMYEAR.GetValue().ToString()
                                        );
                int iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());
                if (iCnt > 0)
                {
                    this.ShowCustomMessage("인건비 예산 자료가 이미 사업계획으로 전송되었습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }          
            
            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region Description : 조회구분 이벤트
        private void CBO01_INQOPTION_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SetFocus(this.BTN61_INQ);
            }

        }
        #endregion
    }
}
