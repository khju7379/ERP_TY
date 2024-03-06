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
    public partial class TYHRNT014S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRNT014S()
        {
            InitializeComponent();
        }

        private void TYHRNT014S_Load(object sender, System.EventArgs e)
        {            
            this.TXT01_SDATE.SetValue(DateTime.Now.ToString("yyyy"));

            this.UP_Set_JuminAuthCheck(CBO01_INQ_AUTH);

            this.UP_Set_CheckBoxTextChange();
            
            this.SetStartingFocus(TXT01_SDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            UP_Spread_Title();

            this.FPS91_TY_S_HR_81HA4476.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_81HA2475", "TY", TXT01_SDATE.GetValue(), CBH01_KBSABUN.GetValue(), CBO01_WFGUBUN.GetValue(), CBO01_WFEDUGN.GetValue(),
                                       CKB01_WFKIBON.Checked == true ? "Y": "",
                                       CKB01_WFJANG.Checked == true ? "Y" : "",
                                       CKB01_WFBUNYO.Checked == true ? "Y" : "",                                        
                                       CKB01_WFJANYE.Checked == true ? "Y" : "",
                                       CKB01_WFCHULSAN.Checked == true ? "Y" : "",                                      
                                       CKB01_WFSPARENT.Checked == true ? "Y" : "",
                                       TYUserInfo.SecureKey, CBO01_INQ_AUTH.GetValue().ToString()
                                       );
            this.FPS91_TY_S_HR_81HA4476.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_HR_81HA4476.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_HR_81HA4476.CurrentRowCount; i++)
                {
                    //전년도 비교하여 변동사항이 있으면 색깔 변경
                    if (this.FPS91_TY_S_HR_81HA4476.GetValue(i, "CHANGEYN").ToString() == "Y")
                    {
                        this.FPS91_TY_S_HR_81HA4476.ActiveSheet.Rows[i].ForeColor = Color.Red;
                    }                  
                }
            }
        }
        #endregion

        #region Description : 스프레드 타이틀 변경
        private void UP_Spread_Title()
        {
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_HR_81HA4476_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_HR_81HA4476_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1 );
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[0, 0].Value = "회 사";
            this.FPS91_TY_S_HR_81HA4476_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[0, 1].Value = "귀속년도";
            this.FPS91_TY_S_HR_81HA4476_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[0, 2].Value = "귀속사번";
            this.FPS91_TY_S_HR_81HA4476_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[0, 3].Value = "귀속성명";
            this.FPS91_TY_S_HR_81HA4476_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1);
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[0, 4].Value = "성   명";
            this.FPS91_TY_S_HR_81HA4476_Sheet1.AddColumnHeaderSpanCell(0, 5, 2, 1);
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[0, 5].Value = "주민등록번호";

            this.FPS91_TY_S_HR_81HA4476_Sheet1.AddColumnHeaderSpanCell(0, 6, 1, 13 );
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[0, 6].Value = TXT01_SDATE.GetValue().ToString()+"년";

            this.FPS91_TY_S_HR_81HA4476_Sheet1.AddColumnHeaderSpanCell(0, 19, 1, 13);
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[0, 19].Value = (Convert.ToInt16(TXT01_SDATE.GetValue().ToString()) - 1).ToString() + "년";

            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[1, 6 ].Value = "나 이";
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[1, 7].Value = "가족코드";
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[1, 8].Value = "가족코드";
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[1, 9].Value = "가족관계";
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[1, 10].Value = "가족관계";
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[1, 11].Value = "교육구분";
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[1, 12].Value = "교육구분";
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[1, 13].Value = "기본공제";
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[1, 14].Value = "장애인";
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[1, 15].Value = "부녀자";
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[1, 16].Value = Convert.ToInt16(TXT01_SDATE.GetValue().ToString()) > 2017 ? "자녀공제" : "6세이하";                
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[1, 17].Value = "출 산";
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[1, 18].Value = "한부모";

            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[1, 19].Value = "나 이";
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[1, 20].Value = "가족코드";
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[1, 21].Value = "가족코드";
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[1, 22].Value = "가족관계";
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[1, 23].Value = "가족관계";
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[1, 24].Value = "교육구분";
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[1, 25].Value = "교육구분";
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[1, 26].Value = "기본공제";
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[1, 27].Value = "장애인";
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[1, 28].Value = "부녀자";
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[1, 29].Value = (Convert.ToInt16(TXT01_SDATE.GetValue().ToString()) - 1) > 2017 ? "자녀공제" : "6세이하";
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[1, 30].Value = "출 산";
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[1, 31].Value = "한부모";

            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;            
            this.FPS91_TY_S_HR_81HA4476_Sheet1.ColumnHeader.Cells[0, 19].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;            

        }
        #endregion

        #region Description : FPS91_TY_S_HR_81HA4476_CellDoubleClick
        private void FPS91_TY_S_HR_81HA4476_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.OpenModalPopup(new TYHRNT01C1(this.FPS91_TY_S_HR_81HA4476.GetValue("WFCOMPANY").ToString(),
                                                   this.FPS91_TY_S_HR_81HA4476.GetValue("WFYEAR").ToString(),
                                                   this.FPS91_TY_S_HR_81HA4476.GetValue("WFSABUN").ToString(),
                                                   this.FPS91_TY_S_HR_81HA4476.GetValue("WFJUMIN").ToString(),
                                                   "N")) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : TXT01_SDATE_TextChanged
        private void TXT01_SDATE_TextChanged(object sender, EventArgs e)
        {
            this.UP_Set_CheckBoxTextChange();
        }
        #endregion

        #region Description : TXT01_SDATE_TextChanged
        private void UP_Set_CheckBoxTextChange()
        {
            if (Convert.ToInt16(Get_Numeric(this.TXT01_SDATE.GetValue().ToString())) > 2017)
            {
                CKB01_WFJANYE.Text = "자녀공제";
            }
            else
            {
                CKB01_WFJANYE.Text = "6세이하";
            }
        }
        #endregion

    }
}
