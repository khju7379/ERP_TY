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
    public partial class TYHRNT003I : TYBase
    {
        private string fsWICOMPANY;
        private string fsWIYEAR;
        private string fsWISABUN;

        #region  Description : 폼 로드 이벤트
        public TYHRNT003I(string sWICOMPANY, string sWIYEAR, string sWISABUN)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsWICOMPANY = sWICOMPANY;
            fsWIYEAR = sWIYEAR;
            fsWISABUN = sWISABUN;

        }

        private void TYHRNT003I_Load(object sender, System.EventArgs e)
        {
            this.TXT01_WNYEAR.SetValue(fsWIYEAR);
            this.CBH01_WNSABUN.SetValue(fsWISABUN);

            UP_Spread_Title();

            if (Convert.ToInt16(this.TXT01_WNYEAR.GetValue().ToString()) > 2017)
            {
                this.FPS91_TY_S_HR_919EY467.Visible = true;
                this.FPS91_TY_S_HR_7BUDB143.Visible = false;
            }
            else
            {
                this.FPS91_TY_S_HR_919EY467.Visible = false;
                this.FPS91_TY_S_HR_7BUDB143.Visible = true;
            }

            this.UP_Get_ItemDataBainding();


            //this.SetStartingFocus(TXT01_WIPE_PUBOFFICIAL);

        }
        #endregion

        #region Description : 스프레드 타이틀&내용 (소득공제)
        private void UP_Spread_Title()
        {
            this.FPS91_TY_S_HR_7B7ID954_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_HR_7B7ID954_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_HR_7B7ID954_Sheet1.AddColumnHeaderSpanCell(0, 0, 1, 4);

            this.FPS91_TY_S_HR_7B7ID954_Sheet1.ColumnHeader.Cells[0, 0].Value = "소득내역";

            this.FPS91_TY_S_HR_7B7ID954_Sheet1.AddColumnHeaderSpanCell(0, 4, 1, 5);

            this.FPS91_TY_S_HR_7B7ID954_Sheet1.ColumnHeader.Cells[0, 4].Value = "세액내역";

            this.FPS91_TY_S_HR_7B7ID954_Sheet1.ColumnHeader.Cells[1, 0].Value = "소득구분";
            this.FPS91_TY_S_HR_7B7ID954_Sheet1.ColumnHeader.Cells[1, 1].Value = "현 근무지";
            this.FPS91_TY_S_HR_7B7ID954_Sheet1.ColumnHeader.Cells[1, 2].Value = "전 근무지";
            this.FPS91_TY_S_HR_7B7ID954_Sheet1.ColumnHeader.Cells[1, 3].Value = "소득합계";

            this.FPS91_TY_S_HR_7B7ID954_Sheet1.ColumnHeader.Cells[1, 4].Value = "세액구분";
            this.FPS91_TY_S_HR_7B7ID954_Sheet1.ColumnHeader.Cells[1, 5].Value = "소득세";
            this.FPS91_TY_S_HR_7B7ID954_Sheet1.ColumnHeader.Cells[1, 6].Value = "주민세";
            this.FPS91_TY_S_HR_7B7ID954_Sheet1.ColumnHeader.Cells[1, 7].Value = "농어촌특별세";
            this.FPS91_TY_S_HR_7B7ID954_Sheet1.ColumnHeader.Cells[1, 8].Value = "세액합계";

            this.FPS91_TY_S_HR_7B7ID954_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_7B7ID954_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

        }
        #endregion

        #region  Description : 연말정산 기초자료 DataBinding 버튼 이벤트
        private void UP_Get_ItemDataBainding()
        {
            int iEmpyCell = 0;
            int iStCell = 0;
            bool bCheck = false;

            FPS91_TY_S_HR_7B7ID954.Initialize();
            FPS91_TY_S_HR_7BUDB143.Initialize();

            //소득공제 집계표
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_7B7J0955", fsWICOMPANY, TXT01_WNYEAR.GetValue(), CBH01_WNSABUN.GetValue());
            FPS91_TY_S_HR_7B7ID954.SetValue(this.DbConnector.ExecuteDataTable());

            if (FPS91_TY_S_HR_7B7ID954.CurrentRowCount > 0)
            {
                this.FPS91_TY_S_HR_7B7ID954.ActiveSheet.Rows[FPS91_TY_S_HR_7B7ID954.CurrentRowCount - 1].BackColor = Color.FromArgb(228, 242, 194);

                this.FPS91_TY_S_HR_7B7ID954_Sheet1.Cells[FPS91_TY_S_HR_7B7ID954.CurrentRowCount - 1, 3].ForeColor = Color.Red;

                if (Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_HR_7B7ID954.GetValue(FPS91_TY_S_HR_7B7ID954.CurrentRowCount - 1, "TAXINCOM").ToString())) < 0)
                {
                    //환급
                    for (int j = 5; j < 9; j++)
                    {
                        this.FPS91_TY_S_HR_7B7ID954_Sheet1.Cells[FPS91_TY_S_HR_7B7ID954.CurrentRowCount - 1, j].ForeColor = Color.Red;
                        //this.FPS91_TY_S_HR_7B7ID954_Sheet1.Cells[FPS91_TY_S_HR_7B7ID954.CurrentRowCount - 1, j].Font = new Font("굴림", 9, FontStyle.Bold);
                    }
                }
                else
                {
                    //추징
                    for (int j = 5; j < 9; j++)
                    {
                        this.FPS91_TY_S_HR_7B7ID954_Sheet1.Cells[FPS91_TY_S_HR_7B7ID954.CurrentRowCount - 1, j].ForeColor = Color.Blue;
                        //this.FPS91_TY_S_HR_7B7ID954_Sheet1.Cells[FPS91_TY_S_HR_7B7ID954.CurrentRowCount - 1, j].Font = new Font("굴림", 9, FontStyle.Bold);
                    }
                }
            }


            if (Convert.ToInt16(this.TXT01_WNYEAR.GetValue().ToString()) > 2017)
            {
                #region  Description : 2018년도 이후 처리 로직

                DataTable dt_Master = UP_Set_DataTable();

                //기본공제 영역
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_919ER465", fsWICOMPANY, TXT01_WNYEAR.GetValue(), CBH01_WNSABUN.GetValue());
                DataTable dt_Base = this.DbConnector.ExecuteDataTable();

                //특별공제 영역
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_919ES466", fsWICOMPANY, TXT01_WNYEAR.GetValue(), CBH01_WNSABUN.GetValue());
                DataTable dt_Spc = this.DbConnector.ExecuteDataTable();

                dt_Master = dt_Base;

                foreach (DataRow dr in dt_Spc.Select("", ""))
                {
                    dt_Master.Rows.Add(dr.ItemArray);
                }

                FPS91_TY_S_HR_919EY467.SetValue(dt_Master);

                if (FPS91_TY_S_HR_919EY467.CurrentRowCount > 0)
                {
                    for (int i = 0; i < this.FPS91_TY_S_HR_919EY467.CurrentRowCount; i++)
                    {
                        //소득공제
                        bCheck = false;
                        //Cell Span 시작지점 선택
                        if (this.FPS91_TY_S_HR_919EY467.GetValue(i, "ITEM1").ToString() != "" && bCheck != true)
                        {
                            iStCell = 0;
                        }
                        else
                        {
                            bCheck = true;
                        }
                        if (this.FPS91_TY_S_HR_919EY467.GetValue(i, "ITEM2").ToString() != "" && bCheck != true)
                        {
                            iStCell = 1;
                        }
                        else
                        {
                            bCheck = true;
                        }
                        if (this.FPS91_TY_S_HR_919EY467.GetValue(i, "ITEM3").ToString() != "" && bCheck != true)
                        {
                            iStCell = 2;
                        }
                        else
                        {
                            bCheck = true;
                        }
                        if (this.FPS91_TY_S_HR_919EY467.GetValue(i, "ITEM4").ToString() != "" && bCheck != true)
                        {
                            iStCell = 3;
                        }
                        else
                        {
                            bCheck = true;
                        }

                        //Cell Span 종료지점 선택
                        if (this.FPS91_TY_S_HR_919EY467.GetValue(i, "ITEM1").ToString() == "")
                        {
                            iEmpyCell = 0;
                        }
                        if (this.FPS91_TY_S_HR_919EY467.GetValue(i, "ITEM2").ToString() == "")
                        {
                            iEmpyCell = 1;
                        }
                        if (this.FPS91_TY_S_HR_919EY467.GetValue(i, "ITEM3").ToString() == "")
                        {
                            iEmpyCell = 2;
                        }
                        if (this.FPS91_TY_S_HR_919EY467.GetValue(i, "ITEM4").ToString() == "")
                        {
                            iEmpyCell = 3;
                        }

                        if (iStCell < 3)
                        {
                            this.FPS91_TY_S_HR_919EY467_Sheet1.AddSpanCell(i, iStCell, 1, (iEmpyCell - iStCell) + 1);
                        }

                        //세액공제
                        bCheck = false;
                        iStCell = 6;
                        //Cell Span 시작지점 선택
                        if (this.FPS91_TY_S_HR_919EY467.GetValue(i, "TAXITEM1").ToString() != "" && bCheck != true)
                        {
                            iStCell = 6;
                        }
                        else
                        {
                            bCheck = true;
                        }
                        if (this.FPS91_TY_S_HR_919EY467.GetValue(i, "TAXITEM2").ToString() != "" && bCheck != true)
                        {
                            iStCell = 7;
                        }
                        else
                        {
                            bCheck = true;
                        }
                        if (this.FPS91_TY_S_HR_919EY467.GetValue(i, "TAXITEM3").ToString() != "" && bCheck != true)
                        {
                            iStCell = 8;
                        }
                        else
                        {
                            bCheck = true;
                        }
                        if (this.FPS91_TY_S_HR_919EY467.GetValue(i, "TAXITEM4").ToString() != "" && bCheck != true)
                        {
                            iStCell = 9;
                        }
                        else
                        {
                            bCheck = true;
                        }
                        if (this.FPS91_TY_S_HR_919EY467.GetValue(i, "TAXITEM5").ToString() != "" && bCheck != true)
                        {
                            iStCell = 10;
                        }
                        else
                        {
                            bCheck = true;
                        }

                        //Cell Span 종료지점 선택
                        if (this.FPS91_TY_S_HR_919EY467.GetValue(i, "TAXITEM1").ToString() == "")
                        {
                            iEmpyCell = 6;
                        }
                        if (this.FPS91_TY_S_HR_919EY467.GetValue(i, "TAXITEM2").ToString() == "")
                        {
                            iEmpyCell = 7;
                        }
                        if (this.FPS91_TY_S_HR_919EY467.GetValue(i, "TAXITEM3").ToString() == "")
                        {
                            iEmpyCell = 8;
                        }
                        if (this.FPS91_TY_S_HR_919EY467.GetValue(i, "TAXITEM4").ToString() == "")
                        {
                            iEmpyCell = 9;
                        }
                        if (this.FPS91_TY_S_HR_919EY467.GetValue(i, "TAXITEM5").ToString() == "")
                        {
                            iEmpyCell = 10;
                        }

                        if (iStCell < 10)
                        {
                            this.FPS91_TY_S_HR_919EY467_Sheet1.AddSpanCell(i, iStCell, 1, (iEmpyCell - iStCell) + 1);
                        }

                    }

                    //rowSpan
                    //국민연금공제
                    this.FPS91_TY_S_HR_919EY467_Sheet1.AddSpanCell(10, 1, 2, 2);
                    //과학기술인공제
                    this.FPS91_TY_S_HR_919EY467_Sheet1.AddSpanCell(10, 8, 2, 2);
                    //퇴직연금
                    this.FPS91_TY_S_HR_919EY467_Sheet1.AddSpanCell(12, 8, 2, 2);
                    //연금저축
                    this.FPS91_TY_S_HR_919EY467_Sheet1.AddSpanCell(14, 8, 2, 2);
                    //ISA
                    this.FPS91_TY_S_HR_919EY467_Sheet1.AddSpanCell(16, 8, 2, 2);
                    //의료비
                    this.FPS91_TY_S_HR_919EY467_Sheet1.AddSpanCell(22, 8, 2, 2);
                    //교육비
                    this.FPS91_TY_S_HR_919EY467_Sheet1.AddSpanCell(24, 8, 2, 2);
                    //월세액
                    this.FPS91_TY_S_HR_919EY467_Sheet1.AddSpanCell(42, 7, 2, 3);

                    //중간라인 공백처리
                    this.FPS91_TY_S_HR_919EY467_Sheet1.AddSpanCell(0, 5, FPS91_TY_S_HR_919EY467.CurrentRowCount, 1);
                }

                #endregion
            }
            else
            {
                #region  Description : 2017년도이전 처리 로직
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_7BUD5142", fsWICOMPANY, TXT01_WNYEAR.GetValue(), CBH01_WNSABUN.GetValue());
                FPS91_TY_S_HR_7BUDB143.SetValue(this.DbConnector.ExecuteDataTable());

                if (FPS91_TY_S_HR_7BUDB143.CurrentRowCount > 0)
                {
                    for (int i = 0; i < this.FPS91_TY_S_HR_7BUDB143.CurrentRowCount; i++)
                    {
                        //소득공제
                        bCheck = false;
                        //Cell Span 시작지점 선택
                        if (this.FPS91_TY_S_HR_7BUDB143.GetValue(i, "ITEM1").ToString() != "" && bCheck != true)
                        {
                            iStCell = 0;
                        }
                        else
                        {
                            bCheck = true;
                        }
                        if (this.FPS91_TY_S_HR_7BUDB143.GetValue(i, "ITEM2").ToString() != "" && bCheck != true)
                        {
                            iStCell = 1;
                        }
                        else
                        {
                            bCheck = true;
                        }
                        if (this.FPS91_TY_S_HR_7BUDB143.GetValue(i, "ITEM3").ToString() != "" && bCheck != true)
                        {
                            iStCell = 2;
                        }
                        else
                        {
                            bCheck = true;
                        }

                        //Cell Span 종료지점 선택
                        if (this.FPS91_TY_S_HR_7BUDB143.GetValue(i, "ITEM1").ToString() == "")
                        {
                            iEmpyCell = 0;
                        }
                        if (this.FPS91_TY_S_HR_7BUDB143.GetValue(i, "ITEM2").ToString() == "")
                        {
                            iEmpyCell = 1;
                        }
                        if (this.FPS91_TY_S_HR_7BUDB143.GetValue(i, "ITEM3").ToString() == "")
                        {
                            iEmpyCell = 2;
                        }

                        if (iStCell < 2)
                        {
                            this.FPS91_TY_S_HR_7BUDB143_Sheet1.AddSpanCell(i, iStCell, 1, (iEmpyCell - iStCell) + 1);
                        }

                        //세액공제
                        bCheck = false;
                        //Cell Span 시작지점 선택
                        if (this.FPS91_TY_S_HR_7BUDB143.GetValue(i, "TAXITEM1").ToString() != "" && bCheck != true)
                        {
                            iStCell = 5;
                        }
                        else
                        {
                            bCheck = true;
                        }
                        if (this.FPS91_TY_S_HR_7BUDB143.GetValue(i, "TAXITEM2").ToString() != "" && bCheck != true)
                        {
                            iStCell = 6;
                        }
                        else
                        {
                            bCheck = true;
                        }
                        if (this.FPS91_TY_S_HR_7BUDB143.GetValue(i, "TAXITEM3").ToString() != "" && bCheck != true)
                        {
                            iStCell = 7;
                        }
                        else
                        {
                            bCheck = true;
                        }
                        if (this.FPS91_TY_S_HR_7BUDB143.GetValue(i, "TAXITEM4").ToString() != "" && bCheck != true)
                        {
                            iStCell = 8;
                        }
                        else
                        {
                            bCheck = true;
                        }
                        if (this.FPS91_TY_S_HR_7BUDB143.GetValue(i, "TAXITEM5").ToString() != "" && bCheck != true)
                        {
                            iStCell = 9;
                        }
                        else
                        {
                            bCheck = true;
                        }

                        //Cell Span 종료지점 선택
                        if (this.FPS91_TY_S_HR_7BUDB143.GetValue(i, "TAXITEM1").ToString() == "")
                        {
                            iEmpyCell = 5;
                        }
                        if (this.FPS91_TY_S_HR_7BUDB143.GetValue(i, "TAXITEM2").ToString() == "")
                        {
                            iEmpyCell = 6;
                        }
                        if (this.FPS91_TY_S_HR_7BUDB143.GetValue(i, "TAXITEM3").ToString() == "")
                        {
                            iEmpyCell = 7;
                        }
                        if (this.FPS91_TY_S_HR_7BUDB143.GetValue(i, "TAXITEM4").ToString() == "")
                        {
                            iEmpyCell = 8;
                        }
                        if (this.FPS91_TY_S_HR_7BUDB143.GetValue(i, "TAXITEM5").ToString() == "")
                        {
                            iEmpyCell = 9;
                        }

                        if (iStCell < 9)
                        {
                            this.FPS91_TY_S_HR_7BUDB143_Sheet1.AddSpanCell(i, iStCell, 1, (iEmpyCell - iStCell) + 1);
                        }
                    }

                    //rowSpan
                    //과학기술인공제
                    this.FPS91_TY_S_HR_7BUDB143_Sheet1.AddSpanCell(11, 7, 2, 2);
                    //퇴직연금
                    this.FPS91_TY_S_HR_7BUDB143_Sheet1.AddSpanCell(13, 7, 2, 2);
                    //연금저축
                    this.FPS91_TY_S_HR_7BUDB143_Sheet1.AddSpanCell(15, 7, 2, 2);
                    //의료비
                    this.FPS91_TY_S_HR_7BUDB143_Sheet1.AddSpanCell(21, 7, 2, 2);
                    //교육비
                    this.FPS91_TY_S_HR_7BUDB143_Sheet1.AddSpanCell(23, 7, 2, 2);
                    //월세액
                    this.FPS91_TY_S_HR_7BUDB143_Sheet1.AddSpanCell(41, 6, 2, 3);

                    //중간라인 공백처리
                    this.FPS91_TY_S_HR_7BUDB143_Sheet1.AddSpanCell(0, 4, FPS91_TY_S_HR_7BUDB143.CurrentRowCount, 1);
                }
                #endregion
            }

        }
        #endregion

        #region  Description : 원천징수영수증 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            SectionReport rpt;

            this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_HR_7CT9B380", fsWICOMPANY, TXT01_WNYEAR.GetValue().ToString(), this.CBH01_WNSABUN.GetValue().ToString(), "", "");
            this.DbConnector.Attach("TY_P_HR_7CT9B380", fsWICOMPANY, TXT01_WNYEAR.GetValue().ToString(), this.CBH01_WNSABUN.GetValue().ToString(), "", TYUserInfo.SecureKey, "Y");
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (Convert.ToInt16(TXT01_WNYEAR.GetValue().ToString()) > 2017)
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

        #region  Description : DataTable 만들기
        private DataTable UP_Set_DataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ROWNUM", typeof(System.Int32));
            dt.Columns.Add("ITEM1", typeof(System.String));
            dt.Columns.Add("ITEM2", typeof(System.String));
            dt.Columns.Add("ITEM3", typeof(System.String));
            dt.Columns.Add("ITEM4", typeof(System.String));
            dt.Columns.Add("AMOUNT", typeof(System.Double));
            dt.Columns.Add("LINE", typeof(System.String));
            dt.Columns.Add("TAXITEM1", typeof(System.String));
            dt.Columns.Add("TAXITEM2", typeof(System.String));
            dt.Columns.Add("TAXITEM3", typeof(System.String));
            dt.Columns.Add("TAXITEM4", typeof(System.String));
            dt.Columns.Add("TAXITEM5", typeof(System.String));
            dt.Columns.Add("TAXAMOUNT", typeof(System.Double));

            dt.TableName = "TableNames";

            return dt;
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

