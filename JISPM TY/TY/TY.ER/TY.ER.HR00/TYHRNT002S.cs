using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;

namespace TY.ER.HR00
{
    /// <summary>
    /// 연말정산 기초자료관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.07.19 11:12
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_77LD4260 : 연말정산 대상자 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_77LD0261 : 연말정산 대상자 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  SAV : 저장
    ///  WISABUN : 사 번
    ///  WICOMPANY : WICOMPANY
    ///  WIET_ADDDEDUCTAMOUNT : 상반기 본인의 추가공제율 사용액
    ///  WIET_CASHAMOUNT : 현금영수증
    ///  WIET_CREDITCARDAMOUNT : 신용카드
    ///  WIET_DEBITCARDAMOUNT : 직불카드
    ///  WIET_FTYRADDDEDUCTAMOUNT : 전전년도 본인의 추가공제율 사용액
    ///  WIET_FTYRCARDAMOUNT : 전전년도 본인 신용카드등 사용액
    ///  WIET_HOUSESAV_2014TOTALSUB : 주택청약종합저축(14년이전)
    ///  WIET_HOUSESAV_2015TOTALSUB : 주택청약종합저축(15년이후)
    ///  WIET_HOUSESAV_SUB : 청약저축
    ///  WIET_HOUSESAV_WORKERSUB : 근로자주택마련저축
    ///  WIET_INDPENSION : 개인연금저축
    ///  WIET_INVESTAMOUNT : 투자조합출자등
    ///  WIET_INVESTSTOCKSAVING : 장기집합투자증권저축
    ///  WIET_MARKETAMOUNT : 전통시장
    ///  WIET_OWNERCONTAMOUNT : 우리사주조합출연금
    ///  WIET_PRECARDAMOUNT : 전년도 본인 신용카드등 사용액
    ///  WIET_PUBTRADEAMOUNT : 대중교통
    ///  WIET_SMALLCOMPANYWORKER : 고용유지 중소기업
    ///  WIET_SMALLTRADEAMOUNT : 소기업소상공인
    ///  WIGB_OTHERINCOME : 기타소득액
    ///  WIGB_OVER_DONATION : 기부금이월분
    ///  WIPE_ARMY : 군인연금
    ///  WIPE_POSTOFFICE : 벌정우체국연금
    ///  WIPE_PRISCHOOL : 사립학교
    ///  WIPE_PUBOFFICIAL : 공무원
    ///  WISP_HOUSE11_15YEAR : 15년미만
    ///  WISP_HOUSE11_29YEAR : 15년~29년
    ///  WISP_HOUSE11_30YEAR : 30년이상
    ///  WISP_HOUSE12_ETC : 기타대출
    ///  WISP_HOUSE12_FIXED : 고정금리.비거치
    ///  WISP_HOUSE15_10TO15YEARFIX : 10년~15년고정금리이거축비거치
    ///  WISP_HOUSE15_15YEARANDFIX : 15년이상고정금리이면서비거치
    ///  WISP_HOUSE15_15YEARETC : 15년이상그밖의대출
    ///  WISP_HOUSE15_15YEARORFIX : 15년이상고정금리이거나비거치
    ///  WISP_HOUSE_LENDER : 대출기관
    ///  WISP_HOUSE_RESIDENT : 거주자
    ///  WITX_CON_FIXCONGROUP : 지정기부금(종교단체)
    ///  WITX_CON_FIXCONGROUPOUT : 지정기부금(종교단체외)
    ///  WITX_CON_LAWCON : 법정기부금
    ///  WITX_CON_POFUND10DOWN : 정치자금 10만원이하
    ///  WITX_CON_POFUND10UP : 정치자금 10만원초과
    ///  WITX_EDUFAMILYAMOUNT : 직계교육비
    ///  WITX_EDUOBJAMOUNT : 장애자
    ///  WITX_EDUOWNAMOUNT : 본 인
    ///  WITX_EDUWIFEAMOUNT : 배우자
    ///  WITX_INSURAMOUNT : 보장성
    ///  WITX_MEDGENERALAMOUNT : 일반의료비
    ///  WITX_MEDOBJAMOUNT : 장애인의료비
    ///  WITX_MEDOWNAMOUNT : 본인.경로
    ///  WITX_MEDPRSDDCTRGTAMOUNT : 난임시술비
    ///  WITX_MONTHRENT : 월세액
    ///  WITX_OBSINSURAMOUNT : 장애인
    ///  WITX_PENSIONSAVAMOUNT : 연금저축
    ///  WITX_RETIREAMOUNT : 퇴직연금
    ///  WITX_SCIENAMOUNT : 과학기술인공제
    ///  WIYEAR : WIYEAR
    /// </summary>
    public partial class TYHRNT002S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRNT002S()
        {
            InitializeComponent();
        }

        private void TYHRNT002S_Load(object sender, System.EventArgs e)
        {
            
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            (this.FPS91_TY_S_HR_77LD0261.Sheets[0].Columns[19].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.btn_member_search;
            (this.FPS91_TY_S_HR_77LD0261.Sheets[0].Columns[20].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.btn_member_search;
            (this.FPS91_TY_S_HR_77LD0261.Sheets[0].Columns[21].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.pdf;        

            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_77LD0261, "BTNDATA");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_77LD0261, "BTNRESULT");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_77LD0261, "BTNFILE");
            
            ToolStripMenuItem reportFix = new ToolStripMenuItem("공제신고서 마감");
            reportFix.Click += new EventHandler(reportFix_ToolStripMenuItem_Click);

            ToolStripMenuItem reportCancel = new ToolStripMenuItem("공제신고서 마감취소");
            reportCancel.Click += new EventHandler(reportCancel_ToolStripMenuItem_Click);

            ToolStripMenuItem TaxAdjustFix = new ToolStripMenuItem("정산 마감");
            TaxAdjustFix.Click += new EventHandler(TaxAdjustFix_ToolStripMenuItem_Click);

            ToolStripMenuItem TaxAdjustCancel = new ToolStripMenuItem("정산 마감취소");
            TaxAdjustCancel.Click += new EventHandler(TaxAdjustCancel_ToolStripMenuItem_Click);

            this.FPS91_TY_S_HR_77LD0261.CurrentContextMenu.Items.AddRange(
                          new System.Windows.Forms.ToolStripItem[] { new ToolStripSeparator(), reportFix, reportCancel, TaxAdjustFix, TaxAdjustCancel });

            this.TXT01_WIYEAR.SetValue(DateTime.Now.ToString("yyyy"));
            
            this.SetStartingFocus(TXT01_WIYEAR);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_77LD0261.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77LD4260", "TY", TXT01_WIYEAR.GetValue(), CBH01_WISABUN.GetValue(), CBO01_INQOPTION.GetValue().ToString(), 
                                                        CBO01_ADSUBMIT.GetValue().ToString(), CBO01_ADDEDDOC.GetValue().ToString(), CBO01_KBGUNMU.GetValue().ToString() );
            this.FPS91_TY_S_HR_77LD0261.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_HR_77LD0261.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_HR_77LD0261.CurrentRowCount; i++)
                {
                    //국세청외 자료가 존재하는 사람 표시
                    if (this.FPS91_TY_S_HR_77LD0261.GetValue(i, "NOTNTSAMTYN").ToString() == "Y")
                    {
                        this.FPS91_TY_S_HR_77LD0261.ActiveSheet.Rows[i].ForeColor = Color.Blue;
                    }

                    //정산기초자료
                    //if (this.FPS91_TY_S_HR_77LD0261.GetValue(i, "BASECNT").ToString() != "Y")
                    //{
                    //    this.FPS91_TY_S_HR_77LD0261_Sheet1.Cells[i, 19].CellType = new FarPoint.Win.Spread.CellType.TextCellType();                        
                    // }

                    //정산결과자료
                    if (this.FPS91_TY_S_HR_77LD0261.GetValue(i, "ADJUSTCNT").ToString() != "Y")
                    {
                        this.FPS91_TY_S_HR_77LD0261_Sheet1.Cells[i, 20].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    }
                    //첨부파일
                    if (this.FPS91_TY_S_HR_77LD0261.GetValue(i, "FILECNT").ToString() != "Y")
                    {
                        this.FPS91_TY_S_HR_77LD0261_Sheet1.Cells[i, 21].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    }
                }
            }
        }
        #endregion

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            string sMsg = string.Empty;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //연말정산 첨부파일 삭제
                    this.DbConnector.Attach("TY_P_HR_81FFR454",
                                                                ds.Tables[0].Rows[i]["ADCOMPANY"].ToString(),
                                                                ds.Tables[0].Rows[i]["ADYEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["ADSABUN"].ToString());

                    //연말정산 대상자 삭제
                    this.DbConnector.Attach("TY_P_HR_7BMDK088",
                                                                ds.Tables[0].Rows[i]["ADCOMPANY"].ToString(),
                                                                ds.Tables[0].Rows[i]["ADYEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["ADSABUN"].ToString());
                    //부양가족사항 삭제
                    this.DbConnector.Attach("TY_P_HR_7CDDU262",
                                                                ds.Tables[0].Rows[i]["ADCOMPANY"].ToString(),
                                                                ds.Tables[0].Rows[i]["ADYEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["ADSABUN"].ToString());
                    //연말정산 기초자료 삭제
                    this.DbConnector.Attach("TY_P_HR_7CDFV264",
                                                                ds.Tables[0].Rows[i]["ADCOMPANY"].ToString(),
                                                                ds.Tables[0].Rows[i]["ADYEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["ADSABUN"].ToString());
                    //연말정산 정산결과 삭제
                    this.DbConnector.Attach("TY_P_HR_7CDFW265",
                                                                ds.Tables[0].Rows[i]["ADCOMPANY"].ToString(),
                                                                ds.Tables[0].Rows[i]["ADYEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["ADSABUN"].ToString());

                }
                if (this.DbConnector.CommandCount > 0)
                {
                    this.DbConnector.ExecuteTranQueryList();
                }

                //국세청 영수증  및 명세관리 테이블 자료 삭제
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_7CDF1263",
                                                                ds.Tables[0].Rows[i]["ADCOMPANY"].ToString(),
                                                                ds.Tables[0].Rows[i]["ADYEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["ADSABUN"].ToString(), "2", "");
                    sMsg = this.DbConnector.ExecuteScalar().ToString();
                }
            }

            BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_HR_77LD0261.GetDataSourceInclude(TSpread.TActionType.Select, "ADCOMPANY", "ADYEAR", "ADSABUN"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_HR_7CDFY266"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion              

        #region  Description : FPS91_TY_S_HR_77LD0261_CellDoubleClick 버튼 이벤트
        private void FPS91_TY_S_HR_77LD0261_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {

            //정산기초자료 조회
            if (e.Column.ToString() == "19")
            {
                //if (this.OpenModalPopup(new TYHRNT002I(this.FPS91_TY_S_HR_77LD0261.GetValue("ADCOMPANY").ToString(),
                //                                       this.FPS91_TY_S_HR_77LD0261.GetValue("ADYEAR").ToString(),
                //                                       this.FPS91_TY_S_HR_77LD0261.GetValue("ADSABUN").ToString(),
                //                                       "Y")) == System.Windows.Forms.DialogResult.OK)
                //this.BTN61_INQ_Click(null, null);

                if (this.OpenModalPopup(new TYHRNT001I(this.FPS91_TY_S_HR_77LD0261.GetValue("ADCOMPANY").ToString(),
                                                    this.FPS91_TY_S_HR_77LD0261.GetValue("ADYEAR").ToString(),
                                                    this.FPS91_TY_S_HR_77LD0261.GetValue("ADSABUN").ToString()                                                    
                                                    )) == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
            }

            //정산결과 조회
            if (e.Column.ToString() == "20")
            {
                if (this.OpenModalPopup(new TYHRNT003I(this.FPS91_TY_S_HR_77LD0261.GetValue("ADCOMPANY").ToString(),
                                                      this.FPS91_TY_S_HR_77LD0261.GetValue("ADYEAR").ToString(),
                                                      this.FPS91_TY_S_HR_77LD0261.GetValue("ADSABUN").ToString())) == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
            }


            //첨부파일 조회
            if (e.Column.ToString() == "21")
            {
                if (this.OpenModalPopup(new TYHRNT02C1(this.FPS91_TY_S_HR_77LD0261.GetValue("ADCOMPANY").ToString(),
                                                       this.FPS91_TY_S_HR_77LD0261.GetValue("ADYEAR").ToString(),
                                                       this.FPS91_TY_S_HR_77LD0261.GetValue("ADSABUN").ToString())) == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region  Description : 연말정산 대상자 생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYHRNT02C2("TY"                                                      
                                                   )) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 연말정산 정산계산 버튼 이벤트
        private void BTN61_CREATE_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYHRNT003B("TY"
                                                   )) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 공제신고서 마감 처리 이벤트
        private void reportFix_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_HR_77LD0261.GetDataSourceInclude(TSpread.TActionType.Select, "ADCOMPANY", "ADYEAR", "ADSABUN", "ADDEDDOC", "ADDEDTAX" ));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowCustomMessage("선택된 자료가 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return;
            }

            if (!this.ShowMessage("TY_M_HR_7C1F0160"))
            {
                return;
            }
            else
            {
                this.DbConnector.CommandClear();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_7C1EW158", "Y",
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[0].Rows[i]["ADCOMPANY"].ToString(),
                                                                ds.Tables[0].Rows[i]["ADYEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["ADSABUN"].ToString());
                }
                if (this.DbConnector.CommandCount > 0)
                {
                    this.DbConnector.ExecuteTranQueryList();
                }

                this.ShowMessage("TY_M_AC_2CECE180");
            }

            BTN61_INQ_Click(null, null);

        }

        private void reportCancel_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_HR_77LD0261.GetDataSourceInclude(TSpread.TActionType.Select, "ADCOMPANY", "ADYEAR", "ADSABUN", "ADDEDDOC", "ADDEDTAX"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowCustomMessage("선택된 자료가 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return;
            }

            if (!this.ShowMessage("TY_M_HR_7C1F0161"))
            {
                return;
            }
            else
            {
                this.DbConnector.CommandClear();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_7C1EW158", "N",
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[0].Rows[i]["ADCOMPANY"].ToString(),
                                                                ds.Tables[0].Rows[i]["ADYEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["ADSABUN"].ToString());
                }
                if (this.DbConnector.CommandCount > 0)
                {
                    this.DbConnector.ExecuteTranQueryList();
                }

                this.ShowMessage("TY_M_MR_35O2G735");
            }
            BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 정산 마감 처리 이벤트
        private void TaxAdjustFix_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_HR_77LD0261.GetDataSourceInclude(TSpread.TActionType.Select, "ADCOMPANY", "ADYEAR", "ADSABUN", "ADDEDDOC", "ADDEDTAX"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowCustomMessage("선택된 자료가 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return;
            }

            if (!this.ShowMessage("TY_M_HR_7C1F1162"))
            {
                return;
            }
            else
            {
                this.DbConnector.CommandClear();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_7C1EW159", "Y",
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[0].Rows[i]["ADCOMPANY"].ToString(),
                                                                ds.Tables[0].Rows[i]["ADYEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["ADSABUN"].ToString());
                }
                if (this.DbConnector.CommandCount > 0)
                {
                    this.DbConnector.ExecuteTranQueryList();
                }

                this.ShowMessage("TY_M_AC_2CECE180");
            }
            BTN61_INQ_Click(null, null);
        }

        private void TaxAdjustCancel_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_HR_77LD0261.GetDataSourceInclude(TSpread.TActionType.Select, "ADCOMPANY", "ADYEAR", "ADSABUN", "ADDEDDOC", "ADDEDTAX"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowCustomMessage("선택된 자료가 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return;
            }

            if (!this.ShowMessage("TY_M_HR_7C1F2163"))
            {
                return;
            }
            else
            {
                this.DbConnector.CommandClear();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_7C1EW159", "N",
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[0].Rows[i]["ADCOMPANY"].ToString(),
                                                                ds.Tables[0].Rows[i]["ADYEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["ADSABUN"].ToString());
                }
                if (this.DbConnector.CommandCount > 0)
                {
                    this.DbConnector.ExecuteTranQueryList();
                }

                this.ShowMessage("TY_M_MR_35O2G735");
            }
            BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 양식 출력 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYHRNT02C3("TY"
                                                   )) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion



    }
}
