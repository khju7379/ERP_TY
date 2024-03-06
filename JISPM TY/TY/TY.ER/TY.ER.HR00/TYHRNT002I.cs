using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using TY.Service.Library;
using TY.Service.Library.Controls;

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
    public partial class TYHRNT002I : TYBase
    {
        private string fsWICOMPANY;
        private string fsWIYEAR;
        private string fsWISABUN;
        private string fsFixGubn;

        #region  Description : 폼 로드 이벤트
        public TYHRNT002I(string sWICOMPANY, string sWIYEAR, string sWISABUN, string sFixGubn)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsWICOMPANY = sWICOMPANY;
            fsWIYEAR = sWIYEAR;
            fsWISABUN = sWISABUN;
            fsFixGubn = sFixGubn;

        }

        private void TYHRNT002I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            (this.FPS91_TY_S_HR_7B6C2944.Sheets[0].Columns[7].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.btn_member_search;
            (this.FPS91_TY_S_HR_7B6C2944.Sheets[0].Columns[16].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.btn_member_search;
            //(this.FPS92_TY_S_HR_7B6C2944.Sheets[0].Columns[6].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.btn_member_search;            

            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_7B6C2944, "LBTN");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_7B6C2944, "RBTN");
            //this.SetSpreadFixedWidthColumn(this.FPS92_TY_S_HR_7B6C2944, "BTN");

            this.TXT01_WIYEAR.SetValue(fsWIYEAR);
            this.CBH01_WISABUN.SetValue(fsWISABUN);

            if (fsFixGubn == "Y")
            {
                BTN61_SAV.Visible = false;
            }

            UP_Spread_Title();

            this.UP_Get_ItemDataBainding();


           //this.SetStartingFocus(TXT01_WIPE_PUBOFFICIAL);

        }
        #endregion

        #region Description : 스프레드 타이틀&내용 (소득공제)
        private void UP_Spread_Title()
        {
            this.FPS91_TY_S_HR_7B6C2944_Sheet1.ColumnHeaderRowCount = 1;
            this.FPS91_TY_S_HR_7B6C2944_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_HR_7B6C2944_Sheet1.AddColumnHeaderSpanCell(0, 0, 1, 2);

            this.FPS91_TY_S_HR_7B6C2944_Sheet1.ColumnHeader.Cells[0, 0].Value = "항   목";
            this.FPS91_TY_S_HR_7B6C2944_Sheet1.ColumnHeader.Cells[0, 1].Value = "";
            
            this.FPS91_TY_S_HR_7B6C2944_Sheet1.ColumnHeader.Cells[0, 2].Value = "금   액";

            this.FPS91_TY_S_HR_7B6C2944_Sheet1.AddColumnHeaderSpanCell(0, 6, 1, 2);
            this.FPS91_TY_S_HR_7B6C2944_Sheet1.ColumnHeader.Cells[0, 6].Value = "";
            this.FPS91_TY_S_HR_7B6C2944_Sheet1.ColumnHeader.Cells[0, 7].Value = "";

            this.FPS91_TY_S_HR_7B6C2944_Sheet1.AddColumnHeaderSpanCell(0, 9, 1, 2);

            this.FPS91_TY_S_HR_7B6C2944_Sheet1.ColumnHeader.Cells[0, 9].Value = "항   목";
            this.FPS91_TY_S_HR_7B6C2944_Sheet1.ColumnHeader.Cells[0, 10].Value = "";

            this.FPS91_TY_S_HR_7B6C2944_Sheet1.ColumnHeader.Cells[0, 11].Value = "금   액";
            this.FPS91_TY_S_HR_7B6C2944_Sheet1.ColumnHeader.Cells[0, 15].Value = "";
            
            this.FPS91_TY_S_HR_7B6C2944_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_7B6C2944_Sheet1.ColumnHeader.Cells[0, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

            this.FPS91_TY_S_HR_7B6C2944_Sheet1.ColumnHeader.Cells[0, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_7B6C2944_Sheet1.ColumnHeader.Cells[0, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

        }
        #endregion


        #region  Description : 연말정산 기초자료 DataBinding 버튼 이벤트
        private void UP_Get_ItemDataBainding()
        {
            FPS91_TY_S_HR_7B6C2944.Initialize();
            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_7B6FC946", fsWICOMPANY,TXT01_WIYEAR.GetValue(), CBH01_WISABUN.GetValue()  );           

            FPS91_TY_S_HR_7B6C2944.SetValue(this.DbConnector.ExecuteDataTable());

            if (FPS91_TY_S_HR_7B6C2944.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_HR_7B6C2944.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "LLOCKGN").ToString() == "Y")
                    {
                        this.FPS91_TY_S_HR_7B6C2944_Sheet1.Cells[i, 2].Locked = true;
                        this.FPS91_TY_S_HR_7B6C2944_Sheet1.Cells[i, 2].ForeColor = Color.Red;
                        this.FPS91_TY_S_HR_7B6C2944_Sheet1.Cells[i, 2].Font = new Font("굴림", 9);
                    }

                    //국세청이외 자료가 있으면 파란색 표시
                    if (this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "LNOTNTSYN").ToString() == "Y")
                    {
                        this.FPS91_TY_S_HR_7B6C2944_Sheet1.Cells[i, 2].ForeColor = Color.Blue;
                        this.FPS91_TY_S_HR_7B6C2944_Sheet1.Cells[i, 2].Font = new Font("굴림", 9);
                    }

                    //팝업버튼 활성화 판단
                    if (this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "LPOPUP").ToString() != "Y")
                    {
                        this.FPS91_TY_S_HR_7B6C2944_Sheet1.Cells[i, 7].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    }

                    if (this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "RLOCKGN").ToString() == "Y")
                    {
                        this.FPS91_TY_S_HR_7B6C2944_Sheet1.Cells[i, 11].Locked = true;
                        this.FPS91_TY_S_HR_7B6C2944_Sheet1.Cells[i, 11].ForeColor = Color.Red;
                        this.FPS91_TY_S_HR_7B6C2944_Sheet1.Cells[i, 11].Font = new Font("굴림", 9);
                    }

                    //국세청이외 자료가 있으면 파란색 표시
                    if (this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "RNOTNTSYN").ToString() == "Y")
                    {
                        this.FPS91_TY_S_HR_7B6C2944_Sheet1.Cells[i, 11].ForeColor = Color.Blue;
                        this.FPS91_TY_S_HR_7B6C2944_Sheet1.Cells[i, 11].Font = new Font("굴림", 9);
                    }
                    //else
                    //{
                    //    this.FPS91_TY_S_HR_7B6C2944_Sheet1.Cells[i, 10].ForeColor = Color.Red;
                    //    this.FPS91_TY_S_HR_7B6C2944_Sheet1.Cells[i, 10].Font = new Font("굴림", 9, FontStyle.Underline);
                    //}
                    //팝업버튼 활성화 판단
                    if (this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "RPOPUP").ToString() != "Y")
                    {
                        this.FPS91_TY_S_HR_7B6C2944_Sheet1.Cells[i, 16].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    }

                    if (this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "RITEM1").ToString() == "의료비" && this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "RITEM2").ToString() == "제외금액(실손보험금)")
                    {
                        this.FPS91_TY_S_HR_7B6C2944_Sheet1.Cells[i, 11].ForeColor = Color.Maroon;
                    }

                }

                //버튼 필드 Row Merge

                this.FPS91_TY_S_HR_7B6C2944_Sheet1.AddSpanCell(0, 7, 4, 1); //연금보험
                this.FPS91_TY_S_HR_7B6C2944_Sheet1.AddSpanCell(4, 7, 2, 1); //주택임차
                this.FPS91_TY_S_HR_7B6C2944_Sheet1.AddSpanCell(6, 7, 9, 1); //장기주택
                this.FPS91_TY_S_HR_7B6C2944_Sheet1.AddSpanCell(17, 7, 3, 1); //주택마련
                this.FPS91_TY_S_HR_7B6C2944_Sheet1.AddSpanCell(21, 7, 10, 1); //신용카드

                this.FPS91_TY_S_HR_7B6C2944_Sheet1.AddSpanCell(26, 7, 9, 1); //그밖의공제

                this.FPS91_TY_S_HR_7B6C2944_Sheet1.AddSpanCell(1, 16, 3, 1); //연금계좌
                this.FPS91_TY_S_HR_7B6C2944_Sheet1.AddSpanCell(7, 16, 4, 1); //의료비

                this.FPS91_TY_S_HR_7B6C2944_Sheet1.AddSpanCell(12, 16, 4, 1); //교육비
                this.FPS91_TY_S_HR_7B6C2944_Sheet1.AddSpanCell(17, 16, 4, 1); //기부금

                //공백처리
                this.FPS91_TY_S_HR_7B6C2944_Sheet1.AddSpanCell(22, 9, 18, 8);               


            }

          

        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            string sWIPE_PubOfficial  =  "0";     //공적연금보험료: 공무원
            string sWIPE_Army         = "0";    //공적연금보험료: 군인연금
            string sWIPE_PriSchool    = "0";    //공적연금보험료: 사립학교
            string sWIPE_PostOffice   = "0";    //공적연금보험료: 벌정우체국연금
            /* 특별공제 */
            string sWISP_House_Lender   =  "0"; //주택임차차입금원리금: 대출기관
            string sWISP_House_Resident =  "0"; //주택임차차입금원리금: 거주자

            string sWISP_House11_15year  = "0";//2011이전차입분 : 15년미만
            string sWISP_House11_29year  = "0"; //2011이전차입분 : 15년~29년
            string sWISP_House11_30year  = "0"; //2011이전차입분 : 30년이상
            string sWISP_House12_Fixed   = "0";//2012이후차입분 : 고정금리.비거치
            string sWISP_House12_Etc     = "0";//2012이후차입분 : 기타대출
            string sWISP_House15_15yearAndFIX  = "0";//2015이후차입분 : 15년이상고정금리이면서비거치
            string sWISP_House15_15yearOrFIX   = "0";//2015이후차입분 : 15년이상고정금리이거나비거치
            string sWISP_House15_15yearEtc     = "0";//2015이후차입분 : 15년이상그밖의대출
            string sWISP_House15_10To15yearFix = "0";//2015이후차입분 : 10년~15년고정금리이거축비거치

            string sWIET_SmallTradeAmount     = "0"; //소기업소상공인 공제부금소득공제 납입액            
            string sWIET_PpreAscInvestAmount  = "0"; //투자조합출자 조합 전전년도 납입금액
            string sWIET_PpreVntInvestAmount  = "0";//투자조합출자 벤처 전전년도 납입금액
            string sWIET_PreAscInvestAmount   = "0";  //투자조합출자 조합 전년도 납입금액
            string sWIET_PreVntInvestAmount   = "0";  //투자조합출자 벤처 전년도 납입금액
            string sWIET_AscInvestAmount      = "0";  //투자조합출자 조합 당해년도 납입금액
            string sWIET_VntInvestAmount      = "0"; //투자조합출자 벤처 당해년도 납입금액
            string sWIET_OwnerContAmount      = "0"; //우리사주조합출연금
            string sWIET_SmallCompanyWorker = "0";  //고용유지 중소기업 근로자

            string sItem = string.Empty;                 

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < FPS91_TY_S_HR_7B6C2944.CurrentRowCount; i++)
                {
                    sItem = this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "LITEM2").ToString();

                    switch (sItem)
                    {
                        case "공무원연금":
                            sWIPE_PubOfficial = this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "LAMOUNT").ToString();
                            break;
                        case "군인연금":
                            sWIPE_Army = this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "LAMOUNT").ToString();
                            break;
                        case "사립학교직원연금":
                            sWIPE_PriSchool = this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "LAMOUNT").ToString();
                            break;
                        case "별정우체국연금":
                            sWIPE_PostOffice = this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "LAMOUNT").ToString();
                            break;
                        case "대출기관":
                            sWISP_House_Lender = this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "LAMOUNT").ToString();
                            break;
                        case "거주자":
                            sWISP_House_Resident = this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "LAMOUNT").ToString();
                            break;
                        case "2011년이전 15년미만":
                            sWISP_House11_15year = this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "LAMOUNT").ToString();
                            break;
                        case "2011년이전 15년~29년":
                            sWISP_House11_29year = this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "LAMOUNT").ToString();
                            break;
                        case "2011년이전 30년이상":
                            sWISP_House11_30year = this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "LAMOUNT").ToString();
                            break;
                        case "2012년이후 고정이면서 비거치":
                            sWISP_House12_Fixed = this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "LAMOUNT").ToString();
                            break;
                        case "2012년이후 기타대출":
                            sWISP_House12_Etc = this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "LAMOUNT").ToString();
                            break;
                        case "2015년이후 15년이상 고정이면서 비거치":
                            sWISP_House15_15yearAndFIX = this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "LAMOUNT").ToString();
                            break;
                        case "2015년이후 15년이상 고정이거나 비거치":
                            sWISP_House15_15yearOrFIX = this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "LAMOUNT").ToString();
                            break;
                        case "2015년이후 15년이상 그밖의대출":
                            sWISP_House15_15yearEtc = this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "LAMOUNT").ToString();
                            break;
                        case "2015년이후 10~15년 고정이거나 비거치":
                            sWISP_House15_10To15yearFix = this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "LAMOUNT").ToString();
                            break;
                        case "소상공인공제부금":
                            sWIET_SmallTradeAmount = this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "LAMOUNT").ToString();
                            break;
                        case "투자조합출자금 2015년 조합투자":
                            sWIET_PpreAscInvestAmount = this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "LAMOUNT").ToString();
                            break;
                        case "투자조합출자금 2015년 벤처투자":
                            sWIET_PpreVntInvestAmount = this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "LAMOUNT").ToString();
                            break;
                        case "투자조합출자금 2016년 조합투자":
                            sWIET_PreAscInvestAmount = this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "LAMOUNT").ToString();
                            break;
                        case "투자조합출자금 2016년 벤처투자":
                            sWIET_PreVntInvestAmount = this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "LAMOUNT").ToString();
                            break;
                        case "투자조합출자금 2017년 조합투자":
                            sWIET_AscInvestAmount = this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "LAMOUNT").ToString();
                            break;
                        case "투자조합출자금 2017년 벤처투자":
                            sWIET_VntInvestAmount = this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "LAMOUNT").ToString();
                            break;
                        case "우리사주조합출연금":
                            sWIET_OwnerContAmount = this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "LAMOUNT").ToString();
                            break;
                        case "고용유지중소기업근로자 공제":
                            sWIET_SmallCompanyWorker = this.FPS91_TY_S_HR_7B6C2944.GetValue(i, "LAMOUNT").ToString();
                            break;
                        default:
                            break;
                    }
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_7BKHF059",  sWIPE_PubOfficial  ,     //공적연금보험료: 공무원
                                                             sWIPE_Army         ,    //공적연금보험료: 군인연금
                                                             sWIPE_PriSchool    ,    //공적연금보험료: 사립학교
                                                             sWIPE_PostOffice   ,    //공적연금보험료: 벌정우체국연금
                                                            /* 특별공제 */
                                                             sWISP_House_Lender   , //주택임차차입금원리금: 대출기관
                                                             sWISP_House_Resident , //주택임차차입금원리금: 거주자
                                                             sWISP_House11_15year  ,//2011이전차입분 : 15년미만
                                                             sWISP_House11_29year  , //2011이전차입분 : 15년~29년
                                                             sWISP_House11_30year  , //2011이전차입분 : 30년이상
                                                             sWISP_House12_Fixed   ,//2012이후차입분 : 고정금리.비거치
                                                             sWISP_House12_Etc     ,//2012이후차입분 : 기타대출
                                                             sWISP_House15_15yearAndFIX  ,//2015이후차입분 : 15년이상고정금리이면서비거치
                                                             sWISP_House15_15yearOrFIX   ,//2015이후차입분 : 15년이상고정금리이거나비거치
                                                             sWISP_House15_15yearEtc     ,//2015이후차입분 : 15년이상그밖의대출
                                                             sWISP_House15_10To15yearFix ,//2015이후차입분 : 10년~15년고정금리이거축비거치
                                                             sWIET_SmallTradeAmount     , //소기업소상공인 공제부금소득공제 납입액            
                                                             sWIET_PpreAscInvestAmount  , //투자조합출자 조합 전전년도 납입금액
                                                             sWIET_PpreVntInvestAmount  ,//투자조합출자 벤처 전전년도 납입금액
                                                             sWIET_PreAscInvestAmount   ,  //투자조합출자 조합 전년도 납입금액
                                                             sWIET_PreVntInvestAmount   ,  //투자조합출자 벤처 전년도 납입금액
                                                             sWIET_AscInvestAmount      ,  //투자조합출자 조합 당해년도 납입금액
                                                             sWIET_VntInvestAmount      , //투자조합출자 벤처 당해년도 납입금액
                                                             sWIET_OwnerContAmount      , //우리사주조합출연금
                                                             sWIET_SmallCompanyWorker ,  //고용유지 중소기업 근로자
                                                             TYUserInfo.EmpNo,
                                                            fsWICOMPANY,
                                                            TXT01_WIYEAR.GetValue(),
                                                            CBH01_WISABUN.GetValue()
                    );
                this.DbConnector.ExecuteTranQuery();
            }

            UP_Get_ItemDataBainding();

            this.ShowMessage("TY_M_GB_23NAD873");
        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_HR_7B6C2944.GetDataSourceInclude(TSpread.TActionType.Update, "LITEM2", "LAMOUNT","RITEM2", "RAMOUNT"));

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

        #region  Description : FPS91_TY_S_HR_7B6C2944_ButtonClicked 이벤트
        private void FPS91_TY_S_HR_7B6C2944_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            string sLPopUpPgid = string.Empty;
            string sRPopUpPgid = string.Empty;

            if (e.Column == 7)
            {

                sLPopUpPgid = this.FPS91_TY_S_HR_7B6C2944.GetValue("LPOPPGID").ToString();


                if (sLPopUpPgid != "")
                {
                    switch (sLPopUpPgid)
                    {
                        case "TYHRNT01C5":  //연금.저축명세
                            if (this.OpenModalPopup(new TYHRNT01C5("TY", TXT01_WIYEAR.GetValue().ToString(), CBH01_WISABUN.GetValue().ToString(), fsFixGubn)) == System.Windows.Forms.DialogResult.OK)
                            {
                                UP_Get_ItemDataBainding();
                            }
                            break;
                        case "TYHRNT01C3":  //의료비
                            if (this.OpenModalPopup(new TYHRNT01C3("TY", TXT01_WIYEAR.GetValue().ToString(), CBH01_WISABUN.GetValue().ToString(), fsFixGubn)) == System.Windows.Forms.DialogResult.OK)
                            {
                                UP_Get_ItemDataBainding();
                            }
                            break;
                        case "TYHRNT01C4": //기부금
                            if (this.OpenModalPopup(new TYHRNT01C4("TY", TXT01_WIYEAR.GetValue().ToString(), CBH01_WISABUN.GetValue().ToString(), fsFixGubn)) == System.Windows.Forms.DialogResult.OK)
                            {
                                UP_Get_ItemDataBainding();
                            }
                            break;
                        case "TYHRNT01C6": //교육비,보험료
                            if (this.OpenModalPopup(new TYHRNT01C6("TY", TXT01_WIYEAR.GetValue().ToString(), CBH01_WISABUN.GetValue().ToString(), fsFixGubn)) == System.Windows.Forms.DialogResult.OK)
                            {
                                UP_Get_ItemDataBainding();
                            }
                            break;
                        case "TYHRNT01C7": //월세액
                            if (this.OpenModalPopup(new TYHRNT01C7("TY", TXT01_WIYEAR.GetValue().ToString(), CBH01_WISABUN.GetValue().ToString(), fsFixGubn)) == System.Windows.Forms.DialogResult.OK)
                            {
                                UP_Get_ItemDataBainding();
                            }
                            break;
                        case "TYHRNT01C8": //신용카드(2020년)
                            if (this.OpenModalPopup(new TYHRNT01C8("TY", TXT01_WIYEAR.GetValue().ToString(), CBH01_WISABUN.GetValue().ToString(), fsFixGubn)) == System.Windows.Forms.DialogResult.OK)
                            {
                                UP_Get_ItemDataBainding();
                            }
                            break;
                        case "TYHRNT01C9": //신용카드(2021년)
                            if (this.OpenModalPopup(new TYHRNT01C9("TY", TXT01_WIYEAR.GetValue().ToString(), CBH01_WISABUN.GetValue().ToString(), fsFixGubn)) == System.Windows.Forms.DialogResult.OK)
                            {
                                UP_Get_ItemDataBainding();
                            }
                            break;
                        case "TYHRNT02C4": //신용카드(2022년)
                            if (this.OpenModalPopup(new TYHRNT02C4("TY", TXT01_WIYEAR.GetValue().ToString(), CBH01_WISABUN.GetValue().ToString(), fsFixGubn)) == System.Windows.Forms.DialogResult.OK)
                            {
                                UP_Get_ItemDataBainding();
                            }
                            break;
                    }
                }
            }

            if (e.Column == 16)
            {
                sRPopUpPgid = this.FPS91_TY_S_HR_7B6C2944.GetValue("RPOPPGID").ToString();
                if (sRPopUpPgid != "")
                {
                    switch (sRPopUpPgid)
                    {
                        case "TYHRNT01C5":  //연금.저축명세
                            if (this.OpenModalPopup(new TYHRNT01C5("TY", TXT01_WIYEAR.GetValue().ToString(), CBH01_WISABUN.GetValue().ToString(), fsFixGubn)) == System.Windows.Forms.DialogResult.OK)
                            {
                                UP_Get_ItemDataBainding();
                            }
                            break;
                        case "TYHRNT01C3":  //의료비
                            if (this.OpenModalPopup(new TYHRNT01C3("TY", TXT01_WIYEAR.GetValue().ToString(), CBH01_WISABUN.GetValue().ToString(), fsFixGubn)) == System.Windows.Forms.DialogResult.OK)
                            {
                                UP_Get_ItemDataBainding();
                            }
                            break;
                        case "TYHRNT01C4": //기부금
                            if (this.OpenModalPopup(new TYHRNT01C4("TY", TXT01_WIYEAR.GetValue().ToString(), CBH01_WISABUN.GetValue().ToString(), fsFixGubn)) == System.Windows.Forms.DialogResult.OK)
                            {
                                UP_Get_ItemDataBainding();
                            }
                            break;
                        case "TYHRNT01C6": //교육비,보험료
                            if (this.OpenModalPopup(new TYHRNT01C6("TY", TXT01_WIYEAR.GetValue().ToString(), CBH01_WISABUN.GetValue().ToString(), fsFixGubn)) == System.Windows.Forms.DialogResult.OK)
                            {
                                UP_Get_ItemDataBainding();
                            }
                            break;
                        case "TYHRNT01C7": //월세액
                            if (this.OpenModalPopup(new TYHRNT01C7("TY", TXT01_WIYEAR.GetValue().ToString(), CBH01_WISABUN.GetValue().ToString(), fsFixGubn)) == System.Windows.Forms.DialogResult.OK)
                            {
                                UP_Get_ItemDataBainding();
                            }
                            break;
                        case "TYHRNT01C8": //신용카드(2020년)
                            if (this.OpenModalPopup(new TYHRNT01C8("TY", TXT01_WIYEAR.GetValue().ToString(), CBH01_WISABUN.GetValue().ToString(), fsFixGubn)) == System.Windows.Forms.DialogResult.OK)
                            {
                                UP_Get_ItemDataBainding();
                            }
                            break;
                        case "TYHRNT01C9": //신용카드(2021년)
                            if (this.OpenModalPopup(new TYHRNT01C9("TY", TXT01_WIYEAR.GetValue().ToString(), CBH01_WISABUN.GetValue().ToString(), fsFixGubn)) == System.Windows.Forms.DialogResult.OK)
                            {
                                UP_Get_ItemDataBainding();
                            }
                            break;
                        case "TYHRNT02C4": //신용카드(2021년)
                            if (this.OpenModalPopup(new TYHRNT02C4("TY", TXT01_WIYEAR.GetValue().ToString(), CBH01_WISABUN.GetValue().ToString(), fsFixGubn)) == System.Windows.Forms.DialogResult.OK)
                            {
                                UP_Get_ItemDataBainding();
                            }
                            break;
                    }
                }
            }
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

