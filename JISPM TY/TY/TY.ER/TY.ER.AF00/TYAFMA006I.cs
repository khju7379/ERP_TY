using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.Drawing; 

namespace TY.ER.AF00
{
    /// <summary>
    /// EIS 계열사 자금수지 관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.10.01 11:12
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_3A12K936 : EIS 계열사 자금수지 등록
    ///  TY_P_AC_3A12L937 : EIS 계열사 자금수지 수정
    ///  TY_P_AC_3A1BD933 : EIS 계열사 자금수지 조회[항목]
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_3A112935 : EIS 계열사 자금수지 관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  INQ : 조회
    ///  SAV : 저장
    ///  EFSUBGN : 계열사구분
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYAFMA006I : TYBase
    {
        private string fsCompanyCode = string.Empty;

        #region  Description : 폼 로드 이벤트
        public TYAFMA006I()
        {
            InitializeComponent();
        }

        private void TYAFMA006I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.BTN61_BATCH.Visible = false;
            this.BTN61_EDIT.Visible = false;

            this.BTN61_INQ.Location = new System.Drawing.Point(1013, 12); // 조회 버튼 위치변경
            this.BTN61_SAV.Location = new System.Drawing.Point(1094, 12); // 저장 버튼 위치변경

            switch( TYUserInfo.EmpNo.Substring(0,2) )
            {
                case "HT":
                    fsCompanyCode = "TH";
                    break;
                case "TG":
                    fsCompanyCode = "TG";
                    break;
                case "TS":
                    fsCompanyCode = "TS";
                    break;
                case "TL":
                    fsCompanyCode = "TL";
                    break;
                default:
                    fsCompanyCode = "";  
                    break;  
            }

            if (fsCompanyCode != "")
            {
                CBH01_EFSUBGN.SetValue(fsCompanyCode);
                CBH01_EFSUBGN.SetReadOnly(true);                 
            }

            this.SetStartingFocus(this.DTP01_GSTYYMM);

            //this.DTP01_GSTYYMM.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"));

            UP_start_dsp_month();
        }
        #endregion

        #region  Description : 확정 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYAFMA006P(CBH01_EFSUBGN.GetValue().ToString())) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_3A13B938", this.CBH01_EFSUBGN.GetValue(), this.DTP01_GSTYYMM.GetString().Substring(0,6));

            Int32 iCnt = Convert.ToInt32(this.DbConnector.ExecuteScalar());

            if (CBO01_INQOPTION.GetValue().ToString() == "1")  //미확정분
            {
                this.BTN61_SAV.Visible = true;  

                if (iCnt > 0)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_3A15U946", this.CBH01_EFSUBGN.GetValue(), this.DTP01_GSTYYMM.GetString().Substring(0, 6));

                    this.FPS91_TY_S_AC_3A112935.SetValue(this.DbConnector.ExecuteDataTable());
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_3A1BD933", this.CBH01_EFSUBGN.GetValue(), this.DTP01_GSTYYMM.GetString().Substring(0, 6));
                    this.FPS91_TY_S_AC_3A112935.SetValue(this.DbConnector.ExecuteDataTable());
                }

                // 특정 ROW 잠금
                for (int i = 0; i < FPS91_TY_S_AC_3A112935.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_3A112935.GetValue(i, "EAFLEVE").ToString().Substring(0, 1) == "1" || this.FPS91_TY_S_AC_3A112935.GetValue(i, "EAFLEVE").ToString().Substring(0, 1) == "2")
                    {
                        // 특정 칼럼 색깔 입히기
                        //this.FPS91_TY_S_AC_3A112935.ActiveSheet.Rows[i].BackColor = Color.SkyBlue;

                        if (this.FPS91_TY_S_AC_3A112935.GetValue(i, "EAFSEQN").ToString().Trim() == "9999")
                        {
                            this.FPS91_TY_S_AC_3A112935.ActiveSheet.Rows[i].ForeColor = Color.Red;

                            Font nFont = new Font("굴림", 11, FontStyle.Bold);

                            this.FPS91_TY_S_AC_3A112935.ActiveSheet.Rows[i].Font = nFont;

                            this.SetSpreadSumRow(this.FPS91_TY_S_AC_3A112935, "EAFSEQN", this.FPS91_TY_S_AC_3A112935.GetValue(i, "EAFSEQN").ToString(), SumRowType.Total);
                        }
                        else
                        {
                            this.FPS91_TY_S_AC_3A112935.ActiveSheet.Rows[i].ForeColor = Color.Blue;

                            Font nFont = new Font("굴림", 10, FontStyle.Bold);

                            this.FPS91_TY_S_AC_3A112935.ActiveSheet.Rows[i].Font = nFont;

                            this.SetSpreadSumRow(this.FPS91_TY_S_AC_3A112935, "EAFSEQN", this.FPS91_TY_S_AC_3A112935.GetValue(i, "EAFSEQN").ToString(), SumRowType.SubTotal);
                        }

                        if (this.FPS91_TY_S_AC_3A112935.GetValue(i, "EAFSEQN").ToString().Trim() != "1000")
                        {
                            this.FPS91_TY_S_AC_3A112935.ActiveSheet.Rows[i].Locked = true;
                        }
                    }
                }

                UP_SumRowAdd();
            }
            else //확정분
            {
                this.BTN61_SAV.Visible = false;  

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_3A812994", this.CBH01_EFSUBGN.GetValue(), this.DTP01_GSTYYMM.GetString().Substring(0, 6));

                this.FPS91_TY_S_AC_3A112935.SetValue(this.DbConnector.ExecuteDataTable());

                for (int i = 0; i < FPS91_TY_S_AC_3A112935.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_3A112935.GetValue(i, "EAFLEVE").ToString().Substring(0, 1) == "1" || this.FPS91_TY_S_AC_3A112935.GetValue(i, "EAFLEVE").ToString().Substring(0, 1) == "2")
                    {
                        // 특정 칼럼 색깔 입히기
                        //this.FPS91_TY_S_AC_3A112935.ActiveSheet.Rows[i].BackColor = Color.SkyBlue;

                        if (this.FPS91_TY_S_AC_3A112935.GetValue(i, "EAFSEQN").ToString().Trim() == "9999")
                        {
                            this.FPS91_TY_S_AC_3A112935.ActiveSheet.Rows[i].ForeColor = Color.Red;

                            Font nFont = new Font("굴림", 11, FontStyle.Bold);

                            this.FPS91_TY_S_AC_3A112935.ActiveSheet.Rows[i].Font = nFont;

                            this.SetSpreadSumRow(this.FPS91_TY_S_AC_3A112935, "EAFSEQN", this.FPS91_TY_S_AC_3A112935.GetValue(i, "EAFSEQN").ToString(), SumRowType.Total);
                        }
                        else
                        {
                            this.FPS91_TY_S_AC_3A112935.ActiveSheet.Rows[i].ForeColor = Color.Blue;

                            Font nFont = new Font("굴림", 10, FontStyle.Bold);

                            this.FPS91_TY_S_AC_3A112935.ActiveSheet.Rows[i].Font = nFont;

                            this.SetSpreadSumRow(this.FPS91_TY_S_AC_3A112935, "EAFSEQN", this.FPS91_TY_S_AC_3A112935.GetValue(i, "EAFSEQN").ToString(), SumRowType.SubTotal);
                        }
                    }

                    this.FPS91_TY_S_AC_3A112935.ActiveSheet.Rows[i].Locked = true;
                }
            }

            if (CBO01_INQOPTION.GetValue().ToString() == "1")  //미확정분
            {
                for (int i = 0; i < this.FPS91_TY_S_AC_3A112935.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_3A112935.GetValue(i, "EAFLEVE").ToString() != "3" || Convert.ToDouble(this.FPS91_TY_S_AC_3A112935.GetValue(i, "EAFSAMM").ToString()) <= 0 )
                    {
                        this.FPS91_TY_S_AC_3A112935_Sheet1.Cells[i, 7].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    }
                }
            }
            else
            {
                for (int i = 0; i < this.FPS91_TY_S_AC_3A112935.ActiveSheet.RowCount; i++)
                {
                    this.FPS91_TY_S_AC_3A112935_Sheet1.Cells[i, 7].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                }
            }            
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            // 삭제
            this.DbConnector.Attach
                (
                "TY_P_AC_3A13T941",
                this.CBH01_EFSUBGN.GetValue(), this.DTP01_GSTYYMM.GetString().Substring(0, 6)
                );

            for (int i = 0; i < this.FPS91_TY_S_AC_3A112935.CurrentRowCount-1; i++)
            {
               
                    this.DbConnector.Attach("TY_P_AC_3A12K936", this.FPS91_TY_S_AC_3A112935.GetValue(i, "EAFSUBGN").ToString(),
                                                                this.FPS91_TY_S_AC_3A112935.GetValue(i, "EAFYYMM").ToString(),
                                                                this.FPS91_TY_S_AC_3A112935.GetValue(i, "EAFSEQN").ToString(),
                                                                this.FPS91_TY_S_AC_3A112935.GetValue(i, "EAFTINM").ToString().Trim(),
                                                                this.FPS91_TY_S_AC_3A112935.GetValue(i, "EAFLEVE").ToString(),
                                                                this.FPS91_TY_S_AC_3A112935.GetValue(i, "EAFSAMM").ToString(),
                                                                this.FPS91_TY_S_AC_3A112935.GetValue(i, "EAFNEMM").ToString(),
                                                                TYUserInfo.EmpNo.ToString()); // 저장               
            }

            this.DbConnector.ExecuteTranQueryList();

            // 저장 메세지
            this.ShowMessage("TY_M_GB_23NAD873");
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            //해당월에 확정분 존재체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach( "TY_P_AC_3A812994", this.CBH01_EFSUBGN.GetValue(), this.DTP01_GSTYYMM.GetString().Substring(0, 6));
            if( this.DbConnector.ExecuteDataTable().Rows.Count > 0  )
            {
                this.ShowMessage("TY_M_GB_3A82W005"); 
                e.Successed = false;
                return;
            }

            // 마감 완료 CHECK 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3C92V659", // TY_P_AC_27H64059
                this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4),
                this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2)
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                e.Successed = false;
                return;
            }
            else
            {
                if (dt.Rows[0]["ECSBBUN"].ToString() == "Z")
                {
                    this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }           

        }
        #endregion

        #region Description : 특정 Row와 Column 값 변경
        private void UP_SumRowAdd()
        {
            this.SpreadSumRowAdd(this.FPS91_TY_S_AC_3A112935, "EAFTINM", "합 계", Color.Yellow);
            
            #region Description : 당월 실적액

            this.FPS91_TY_S_AC_3A112935_Sheet1.SetFormula(
                5,
                6,
                "R[-1]C[0] + R[-2]C[0]+ R[-3]C[0]+ R[-4]C[0]"); // 수입

            this.FPS91_TY_S_AC_3A112935_Sheet1.SetFormula(
                15,
                6,
                "R[-1]C[0] + R[-2]C[0]+ R[-3]C[0]+ R[-4]C[0]+ R[-5]C[0]+ R[-6]C[0]+ R[-7]C[0]+ R[-8]C[0]+ R[-9]C[0]"); // 지출

            this.FPS91_TY_S_AC_3A112935_Sheet1.SetFormula(
                16,
                6,
                "R[-16]C[0] + R[-11]C[0] - R[-1]C[0]"); // 과부족

            this.FPS91_TY_S_AC_3A112935_Sheet1.SetFormula(
                22,
                6,
                "R[-6]C[0] - (R[-4]C[0] + R[-5]C[0]) + R[-2]C[0]+ R[-3]C[0] + R[-1]C[0]"); // 차월이월

            #endregion

            #region Description : 익월 실적액

            this.FPS91_TY_S_AC_3A112935_Sheet1.SetFormula(
               5,
               8,
               "R[-1]C[0] + R[-2]C[0]+ R[-3]C[0]+ R[-4]C[0]"); // 수입

            this.FPS91_TY_S_AC_3A112935_Sheet1.SetFormula(
                15,
                8,
                "R[-1]C[0] + R[-2]C[0]+ R[-3]C[0]+ R[-4]C[0]+ R[-5]C[0]+ R[-6]C[0]+ R[-7]C[0]+ R[-8]C[0]+ R[-9]C[0]"); // 지출

            this.FPS91_TY_S_AC_3A112935_Sheet1.SetFormula(
                16,
                8,
                "R[-16]C[0] + R[-11]C[0] - R[-1]C[0]"); // 과부족

            this.FPS91_TY_S_AC_3A112935_Sheet1.SetFormula(
                22,
                8,
                "R[-6]C[0] - (R[-4]C[0] + R[-5]C[0]) + R[-2]C[0]+ R[-3]C[0] + R[-1]C[0]"); // 차월이월

            
            #endregion

            this.FPS91_TY_S_AC_3A112935.ActiveSheet.Rows[FPS91_TY_S_AC_3A112935.CurrentRowCount-1].Visible = false;
        }
        #endregion

        #region Description : 자금생성 버튼 이벤트
        private void BTN61_EDIT_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYAFMA006B(CBH01_EFSUBGN.GetValue().ToString())) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 보기 버튼 이벤트
        private void FPS91_TY_S_AC_3A112935_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "7")
            {
                string sCode = this.FPS91_TY_S_AC_3A112935.GetValue("EAFSEQN").ToString().Substring(0, 4).Trim();

                if (this.OpenModalPopup(new TYAFMA006S(CBH01_EFSUBGN.GetValue().ToString(), this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 6),sCode )) == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region Description : EIS 계열사 최종 마감 월 조회
        private void UP_start_dsp_month()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_3C94Q662");
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count == 0)
            {
                this.DTP01_GSTYYMM.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"));
            }
            else
            {
                this.DTP01_GSTYYMM.SetValue(dt.Rows[0]["YYMM"].ToString());
            }
        }
        #endregion
    }
}
