using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.ER.GB00;
using System.Drawing;

namespace TY.ER.US00
{
    /// <summary>
    /// 수동 출고화주 순위 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2019.08.28 16:46
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_98THD156 : 수동 출고화주 순위 조회
    ///  TY_P_US_98UAC159 : 수동 출고화주 순위 수정
    ///  TY_P_US_98UAP160 : 통관일별 재고 로그 등록
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_98THD158 : 수동 출고화주 순위 관리
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  SAV : 저장
    ///  JCGOKJONG : 곡종
    ///  JCHWAJU : 화주
    /// </summary>
    public partial class TYUSGA003I : TYBase
    {
        string fsGUBUN = string.Empty;

        #region Description : 폼 로드
        public TYUSGA003I()
        {
            InitializeComponent();
        }

        public TYUSGA003I(string sGUBUN)
        {
            InitializeComponent();

            fsGUBUN = sGUBUN;
        }

        private void TYUSGA003I_Load(object sender, System.EventArgs e)
        {
            // 저장 체크
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            BTN61_INQ_Click(null, null);
            SetStartingFocus(this.CBH01_JCHWAJU.CodeText);

            UP_Spread_Load();

            if (fsGUBUN != "")
            {
                this.BTN61_CLO.Visible = true;
            }
            else
            {
                this.BTN61_CLO.Visible = false;

                this.BTN61_SAV.Location = new System.Drawing.Point(1095, 12);
                this.BTN61_INQ.Location = new System.Drawing.Point(1014, 12);
            }
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_98THD158.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_US_98THD156",
                                    this.CBH01_JCHWAJU.GetValue().ToString(),
                                    this.CBH01_JCGOKJONG.GetValue().ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            dt = UP_DataTableChange(dt);

            this.FPS91_TY_S_US_98THD158.SetValue(dt);
            
            // 컬럼 색깔 및 텍스트 정렬
            this.FPS91_TY_S_US_98THD158.ActiveSheet.Columns["JCHWAJU"].Font = new Font("굴림", 9, FontStyle.Bold);

            int i = 0;

            // 스프레드 특정 칼럼 병합
            for (i = 0; i < FPS91_TY_S_US_98THD158.ActiveSheet.RowCount; i++)
            {
                if (i % 2 == 0)
                {
                    this.FPS91_TY_S_US_98THD158.ActiveSheet.AddSpanCell(i, 4, 2, 1);
                }
            }

            for (i = 0; i < dt.Rows.Count;i++)
            {
                if (i % 2 == 1)
                {
                    this.FPS91_TY_S_US_98THD158.ActiveSheet.Cells[i, 5].Font = new Font("굴림", 10);
                    this.FPS91_TY_S_US_98THD158.ActiveSheet.Cells[i, 6].Font = new Font("굴림", 10);
                    this.FPS91_TY_S_US_98THD158.ActiveSheet.Cells[i, 10].Font = new Font("굴림", 10, FontStyle.Bold);
                    this.FPS91_TY_S_US_98THD158.ActiveSheet.Cells[i, 10].ForeColor = Color.Blue;

                    this.FPS91_TY_S_US_98THD158.ActiveSheet.Cells[i, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_US_98THD158.ActiveSheet.Cells[i, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_US_98THD158.ActiveSheet.Cells[i, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_US_98THD158.ActiveSheet.Cells[i, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_US_98THD158.ActiveSheet.Cells[i, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    this.FPS91_TY_S_US_98THD158.ActiveSheet.Cells[i, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

                    this.FPS91_TY_S_US_98THD158.ActiveSheet.Rows[i].ForeColor = Color.Black;
                    this.FPS91_TY_S_US_98THD158.ActiveSheet.Rows[i].BackColor = Color.White;
                    this.FPS91_TY_S_US_98THD158.ActiveSheet.Rows[i].Locked = true;
                }
                else
                {
                    this.FPS91_TY_S_US_98THD158.ActiveSheet.Cells[i, 2].Font = new Font("굴림", 10, FontStyle.Bold);
                    this.FPS91_TY_S_US_98THD158.ActiveSheet.Cells[i, 5].Font = new Font("굴림", 10, FontStyle.Bold);
                    this.FPS91_TY_S_US_98THD158.ActiveSheet.Cells[i, 6].Font = new Font("굴림", 10, FontStyle.Bold);
                    this.FPS91_TY_S_US_98THD158.ActiveSheet.Cells[i, 10].Font = new Font("굴림", 10);

                    this.FPS91_TY_S_US_98THD158.ActiveSheet.Rows[i].BackColor = Color.FromArgb(240,240,240);
                }

                this.FPS91_TY_S_US_98THD158.ActiveSheet.Rows[i].Height = 30;
            }
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        // USIJECSNF 출고순위 수정
                        this.DbConnector.Attach("TY_P_US_98UAC159", ds.Tables[0].Rows[i]["JCNUMBER"].ToString(),    // 출고순위
                                                                    TYUserInfo.EmpNo,
                                                                    ds.Tables[0].Rows[i]["JCHANGCHA"].ToString(),   // 항차
                                                                    ds.Tables[0].Rows[i]["JCGOKJONG"].ToString(),   // 곡종
                                                                    ds.Tables[0].Rows[i]["JCHWAJU"].ToString(),     // 화주
                                                                    ds.Tables[0].Rows[i]["JCBLNO"].ToString(),      // B/L 번호
                                                                    ds.Tables[0].Rows[i]["JCBLMSN"].ToString(),     // MSN
                                                                    ds.Tables[0].Rows[i]["JCBLHSN"].ToString(),     // HSN
                                                                    ds.Tables[0].Rows[i]["JCCUSTIL"].ToString(),    // 통관일자
                                                                    ds.Tables[0].Rows[i]["JCSEQ"].ToString(),       // 통관차수
                                                                    ds.Tables[0].Rows[i]["JCYDHWAJU"].ToString(),   // 양도화주
                                                                    ds.Tables[0].Rows[i]["JCYSDATE"].ToString(),    // 양수일자
                                                                    ds.Tables[0].Rows[i]["JCYDSEQ"].ToString(),     // 양도차수
                                                                    ds.Tables[0].Rows[i]["JCYSSEQ"].ToString()      // 양수순번
                                                                    );

                        // USIJECSLGF 등록
                        this.DbConnector.Attach("TY_P_US_98UAP160", ds.Tables[0].Rows[i]["JCHANGCHA"].ToString(),   // 항차
                                                                    ds.Tables[0].Rows[i]["JCGOKJONG"].ToString(),   // 곡종
                                                                    ds.Tables[0].Rows[i]["JCHWAJU"].ToString(),     // 화주
                                                                    ds.Tables[0].Rows[i]["JCBLNO"].ToString(),      // B/L 번호
                                                                    ds.Tables[0].Rows[i]["JCBLMSN"].ToString(),     // MSN
                                                                    ds.Tables[0].Rows[i]["JCBLHSN"].ToString(),     // HSN
                                                                    ds.Tables[0].Rows[i]["JCCUSTIL"].ToString(),    // 통관일자
                                                                    ds.Tables[0].Rows[i]["JCSEQ"].ToString(),       // 통관차수
                                                                    ds.Tables[0].Rows[i]["JCYDHWAJU"].ToString(),   // 양도화주
                                                                    ds.Tables[0].Rows[i]["JCYSDATE"].ToString(),    // 양수일자
                                                                    ds.Tables[0].Rows[i]["JCYDSEQ"].ToString(),     // 양도차수
                                                                    ds.Tables[0].Rows[i]["JCYSSEQ"].ToString(),     // 양수순번
                                                                    ds.Tables[0].Rows[i]["JCNUMBER"].ToString(),    // 출고순위
                                                                    TYUserInfo.EmpNo
                                                                    );
                    }
                    this.DbConnector.ExecuteTranQueryList();
                }

                // 저장되었습니다.
                this.ShowMessage("TY_M_GB_23NAD873");
                BTN61_INQ_Click(null, null);
            }
            catch
            {
                this.ShowMessage("TY_M_AC_246A2488");
            }
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dtChk = new DataTable();
            DataTable dt = new DataTable();

            int iRowEqual = 0;

            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_US_98THD158.GetDataSourceInclude(TSpread.TActionType.Update, "JCHANGCHA", "JCGOKJONG", "JCHWAJU", "JCBLNO", "JCBLMSN", "JCBLHSN", "JCCUSTIL",
                                                                                                       "JCSEQ", "JCYDHWAJU", "JCYSDATE", "JCYDSEQ", "JCYSSEQ", "JCNUMBER", "JCWONSAN"));
            dtChk = (DataTable)FPS91_TY_S_US_98THD158.DataSource; ;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["JCNUMBER"].ToString() != "50" && ds.Tables[0].Rows[i]["JCNUMBER"].ToString() != "80" && ds.Tables[0].Rows[i]["JCNUMBER"].ToString() != "99")
                {
                    iRowEqual = 0;

                    for (int j = 0; j < dtChk.Rows.Count; j++)
                    {
                        if (ds.Tables[0].Rows[i]["JCHWAJU"].ToString() == dtChk.Rows[j]["JCHWAJU"].ToString() &&
                            ds.Tables[0].Rows[i]["JCGOKJONG"].ToString() == dtChk.Rows[j]["JCGOKJONG"].ToString() &&
                            ds.Tables[0].Rows[i]["JCWONSAN"].ToString() == dtChk.Rows[j]["JCWONSAN"].ToString() &&
                            ds.Tables[0].Rows[i]["JCNUMBER"].ToString() == dtChk.Rows[j]["JCNUMBER"].ToString())
                        {
                            iRowEqual = iRowEqual + 1;
                        }
                    }

                    if (iRowEqual > 1)
                    {
                        this.ShowCustomMessage("화주,곡종 및 원산지가 같은 경우 우선순위가 동일할 수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 스프레드 타이틀 변경
        private void UP_Spread_Load()
        {
            this.FPS91_TY_S_US_98THD158_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_US_98THD158_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_US_98THD158_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_US_98THD158_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_US_98THD158_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1);

            this.FPS91_TY_S_US_98THD158_Sheet1.ColumnHeader.Cells[1, 2].Value = "원산지";
            this.FPS91_TY_S_US_98THD158_Sheet1.ColumnHeader.Cells[1, 3].Value = "통관량";
            this.FPS91_TY_S_US_98THD158_Sheet1.ColumnHeader.Cells[1, 5].Value = "양도량";
            this.FPS91_TY_S_US_98THD158_Sheet1.ColumnHeader.Cells[1, 6].Value = "양수량";
            this.FPS91_TY_S_US_98THD158_Sheet1.ColumnHeader.Cells[1, 7].Value = "양수양도";
            this.FPS91_TY_S_US_98THD158_Sheet1.ColumnHeader.Cells[1, 8].Value = "양수출고";
            this.FPS91_TY_S_US_98THD158_Sheet1.ColumnHeader.Cells[1, 9].Value = "출고량";
            this.FPS91_TY_S_US_98THD158_Sheet1.ColumnHeader.Cells[1, 10].Value = "재고량";


            this.FPS91_TY_S_US_98THD158_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_US_98THD158_Sheet1.ColumnHeader.Cells[0, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_US_98THD158_Sheet1.ColumnHeader.Cells[0, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_US_98THD158_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_US_98THD158_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_US_98THD158_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_US_98THD158_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_US_98THD158_Sheet1.ColumnHeader.Cells[0, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_US_98THD158_Sheet1.ColumnHeader.Cells[0, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_US_98THD158_Sheet1.ColumnHeader.Cells[0, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            
            this.FPS91_TY_S_US_98THD158_Sheet1.ColumnHeader.Cells[1, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_US_98THD158_Sheet1.ColumnHeader.Cells[1, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_US_98THD158_Sheet1.ColumnHeader.Cells[1, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_US_98THD158_Sheet1.ColumnHeader.Cells[1, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_US_98THD158_Sheet1.ColumnHeader.Cells[1, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_US_98THD158_Sheet1.ColumnHeader.Cells[1, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_US_98THD158_Sheet1.ColumnHeader.Cells[1, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_US_98THD158_Sheet1.ColumnHeader.Rows[0].Height = 30;
            this.FPS91_TY_S_US_98THD158_Sheet1.ColumnHeader.Rows[1].Height = 30;
        }
        #endregion

        #region Description : 데이터 셋 변경
        private DataTable UP_DataTableChange(DataTable dt)
        {
            DataTable dtRtn = new DataTable();

            DataRow row;
            // 화주, 곡종, 순위, 통관일자, 차수, 항차, 모선명,MSN-HSN, 양도화주, 양수일자, 양도차수
            // 화주, 곡종, 원산지, 통관량, 차수, 양도량,양수량,양수양도,양수출고,출고량,재고량
            dtRtn.Columns.Add("JCHWAJU", typeof(System.String));
            dtRtn.Columns.Add("JCGOKJONG", typeof(System.String));
            dtRtn.Columns.Add("JCNUMBER", typeof(System.String));
            dtRtn.Columns.Add("JCCUSTIL", typeof(System.String));
            dtRtn.Columns.Add("JCSEQ", typeof(System.String));
            dtRtn.Columns.Add("JCHANGCHA", typeof(System.String));
            dtRtn.Columns.Add("JCHANGCHANM", typeof(System.String));
            dtRtn.Columns.Add("JCBLMSNHSN", typeof(System.String));
            dtRtn.Columns.Add("JCYDHWAJU", typeof(System.String));
            dtRtn.Columns.Add("JCYSDATE", typeof(System.String));
            dtRtn.Columns.Add("JCYDSEQ", typeof(System.String));
            dtRtn.Columns.Add("JCBLNO", typeof(System.String));
            dtRtn.Columns.Add("JCBLMSN", typeof(System.String));
            dtRtn.Columns.Add("JCBLHSN", typeof(System.String));
            dtRtn.Columns.Add("JCYSSEQ", typeof(System.String));

            dtRtn.Columns.Add("JCWONSAN", typeof(System.String));
            

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // 1번 ROW
                row = dtRtn.NewRow();
                row["JCHWAJU"] = dt.Rows[i]["JCHWAJU"].ToString();      // 화주
                row["JCGOKJONG"] = dt.Rows[i]["JCGOKJONG"].ToString();  // 곡종
                row["JCNUMBER"] = dt.Rows[i]["JCNUMBER"].ToString();    // 순위
                row["JCCUSTIL"] = dt.Rows[i]["JCCUSTIL"].ToString();    // 통관일자
                row["JCSEQ"] = dt.Rows[i]["JCSEQ"].ToString();          // 통관차수
                row["JCHANGCHA"] = dt.Rows[i]["JCHANGCHA"].ToString();  // 항차
                row["JCHANGCHANM"] = dt.Rows[i]["JCHANGCHANM"].ToString();  // 모선명
                row["JCBLMSNHSN"] = dt.Rows[i]["JCBLMSN"].ToString() + "-" + dt.Rows[i]["JCBLHSN"].ToString(); // MSN-HSN
                if (dt.Rows[i]["JCYDHWAJU"].ToString() == "")
                {
                    row["JCYDHWAJU"] = "";  // 양도화주
                    row["JCYSDATE"] = "";    // 양수일자
                    row["JCYDSEQ"] = "";      // 양도차수
                }
                else{
                    row["JCYDHWAJU"] = dt.Rows[i]["JCYDHWAJU"].ToString();  // 양도화주
                    row["JCYSDATE"] = dt.Rows[i]["JCYSDATE"].ToString();    // 양수일자
                    row["JCYDSEQ"] = dt.Rows[i]["JCYDSEQ"].ToString();      // 양도차수
                }
                row["JCBLNO"] = dt.Rows[i]["JCBLNO"].ToString();            // B/L 번호
                row["JCBLMSN"] = dt.Rows[i]["JCBLMSN"].ToString();          // MSN
                row["JCBLHSN"] = dt.Rows[i]["JCBLHSN"].ToString();          // HSN
                row["JCYSSEQ"] = dt.Rows[i]["JCYSSEQ"].ToString();          // 양수순번

                row["JCWONSAN"] = dt.Rows[i]["JCWONSAN"].ToString();        // 원산지

                dtRtn.Rows.Add(row);

                // 2번 ROW
                row = dtRtn.NewRow();
                row["JCHWAJU"] = dt.Rows[i]["JCHWAJU"].ToString();      // 화주
                row["JCGOKJONG"] = dt.Rows[i]["JCGOKJONG"].ToString();  // 곡종
                row["JCNUMBER"] = dt.Rows[i]["JCWONSAN"].ToString();    // 원산지
                if (Convert.ToDouble(Get_Numeric(dt.Rows[i]["JCCSQTY"].ToString())) <= 0)
                {
                    row["JCCUSTIL"] = "";    // 통관량
                }
                else{
                    row["JCCUSTIL"] = string.Format("{0:#,##0.000}", Convert.ToDouble(Get_Numeric(dt.Rows[i]["JCCSQTY"].ToString())));    // 통관량
                }
                row["JCSEQ"] = dt.Rows[i]["JCSEQ"].ToString();          // 통관차수
                if (Convert.ToDouble(Get_Numeric(dt.Rows[i]["JCYDQTY"].ToString())) <= 0)
                {
                    row["JCHANGCHA"] = "";    // 양도량
                }
                else
                {
                    row["JCHANGCHA"] = string.Format("{0:#,##0.000}", Convert.ToDouble(Get_Numeric(dt.Rows[i]["JCYDQTY"].ToString())));  // 양도량
                }
                if (Convert.ToDouble(Get_Numeric(dt.Rows[i]["JCYSQTY"].ToString())) <= 0)
                {
                    row["JCHANGCHANM"] = "";    // 양수량
                }
                else
                {
                    row["JCHANGCHANM"] = string.Format("{0:#,##0.000}", Convert.ToDouble(Get_Numeric(dt.Rows[i]["JCYSQTY"].ToString())));  // 양수량
                }
                if (Convert.ToDouble(Get_Numeric(dt.Rows[i]["JCYSYDQTY"].ToString())) <= 0)
                {
                    row["JCBLMSNHSN"] = "";    // 양수양도
                }
                else
                {
                    row["JCBLMSNHSN"] = string.Format("{0:#,##0.000}", Convert.ToDouble(Get_Numeric(dt.Rows[i]["JCYSYDQTY"].ToString()))); // 양수양도
                }
                if (Convert.ToDouble(Get_Numeric(dt.Rows[i]["JCYSCHQTY"].ToString())) <= 0)
                {
                    row["JCYDHWAJU"] = "";    // 양수출고
                }
                else
                {
                    row["JCYDHWAJU"] = string.Format("{0:#,##0.000}", Convert.ToDouble(Get_Numeric(dt.Rows[i]["JCYSCHQTY"].ToString())));  // 양수출고
                }
                if (Convert.ToDouble(Get_Numeric(dt.Rows[i]["JCCHQTY"].ToString())) <= 0)
                {
                    row["JCYSDATE"] = "";    // 출고량
                }
                else
                {
                    row["JCYSDATE"] = string.Format("{0:#,##0.000}", Convert.ToDouble(Get_Numeric(dt.Rows[i]["JCCHQTY"].ToString())));    // 출고량
                }
                if (Convert.ToDouble(Get_Numeric(dt.Rows[i]["JCJEGOQTY"].ToString())) <= 0)
                {
                    row["JCYDSEQ"] = "";    // 재고량
                }
                else
                {
                    row["JCYDSEQ"] = string.Format("{0:#,##0.000}", Convert.ToDouble(Get_Numeric(dt.Rows[i]["JCJEGOQTY"].ToString())));      // 재고량
                }
                row["JCBLNO"] = dt.Rows[i]["JCBLNO"].ToString();            // B/L 번호
                row["JCBLMSN"] = dt.Rows[i]["JCBLMSN"].ToString();          // MSN
                row["JCBLHSN"] = dt.Rows[i]["JCBLHSN"].ToString();          // HSN
                row["JCYSSEQ"] = dt.Rows[i]["JCYSSEQ"].ToString();          // 양수순번

                row["JCWONSAN"] = dt.Rows[i]["JCWONSAN"].ToString();        // 원산지

                dtRtn.Rows.Add(row);
            }

            return dtRtn;

        }
        #endregion

        #region Description : 스프레드 쉬프트키 입력
        private void FPS91_TY_S_US_98THD158_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
            {
                TButton.ClickEventCheckArgs args = new TButton.ClickEventCheckArgs(true);

                this.BTN61_SAV_ProcessCheck(this.BTN61_SAV, args);

                if (args.Successed == true)
                {
                    this.BTN61_SAV_Click(this.BTN61_SAV, args);
                }
            }
        }
        #endregion
    }
}
