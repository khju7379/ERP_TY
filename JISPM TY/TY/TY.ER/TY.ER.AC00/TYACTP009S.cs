using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using GrapeCity.ActiveReports;
using TY.ER.AC00;
using TY.ER.GB00;
using System.Collections;

namespace TY.ER.AC00
{
    /// <summary>
    /// 원천징수이행상황신고서 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.10.16 11:32
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_3BK25384 : 제출자정보 조회
    ///  TY_P_AC_4AGGN186 : 원천징수이행상황신고서 조회(환급세액 조정)
    ///  TY_P_AC_4AGHS187 : 원천징수이행상황신고서 조회(원천징수영세 및 납부세액)
    ///  TY_P_AC_4ARGY260 : 원천징수이행상황신고서 조회(부표1)
    ///  TY_P_AC_4ARGZ264 : 원천징수이행상황신고서 조회(부표2)
    ///  TY_P_AC_4AVF2288 : 원천징수이행상황신고서 부표존재여부
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_4AKGG206 : 원천징수이행상황신고서(환급세액 조정)
    ///  TY_S_AC_4AKGG208 : 원천징수이행상황신고서(원천징수명세 및 납부세액)
    ///  TY_S_AC_4ARF4256 : 원천징수이행상황신고서 부표1
    ///  TY_S_AC_4ARF5257 : 원천징수이행상황신고서 부표2
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  WJBRANCH : 지점구분
    ///  WREYYMM : 귀속년월
    /// </summary>
    public partial class TYACTP009S : TYBase
    {
        string _sGubn = "N";
        public TYACTP009S()
        {
            InitializeComponent();
        }

        private void TYACTP009S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_WREYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            UP_Spread_Title();

            for (int i = 0; i < 2; i++)
            {
                tabControl1.Controls.RemoveAt(1);
            }
        }

        #region Descripgion : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            if (_sGubn == "Y")
            {
                for (int i = 0; i < 2; i++)
                {
                    tabControl1.Controls.RemoveAt(1);
                }
            }
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_4AGHS187", this.DTP01_WREYYMM.GetValue().ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_4AKGG208.SetValue(dt);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_4AVF2288",
                                        this.DTP01_WREYYMM.GetValue().ToString());

                DataTable dtTemp = this.DbConnector.ExecuteDataTable();

                if (dtTemp.Rows[0]["WDETAILGB"].ToString() == "Y")
                {
                    tabControl1.Controls.Add(tabPage2);
                    tabControl1.Controls.Add(tabPage3);
                    
                    UP_Spread_Title_Sub();

                    _sGubn = "Y";
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_4ARGY260", this.DTP01_WREYYMM.GetValue().ToString());

                    DataTable dt1 = this.DbConnector.ExecuteDataTable();

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_4ARGZ264", this.DTP01_WREYYMM.GetValue().ToString());

                    DataTable dt2 = this.DbConnector.ExecuteDataTable();

                    this.FPS91_TY_S_AC_4ARF4256.SetValue(dt1);
                    this.FPS91_TY_S_AC_4ARF5257.SetValue(dt2);
                    
                    UP_Spread_Desc_Sub();
                }
                else
                {
                    _sGubn = "N";
                }
            }
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_4AGGN186",
                                    this.DTP01_WREYYMM.GetValue().ToString(),
                                    "1");

            dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_4AKGG206.SetValue(dt);
                
            }
            //else
            //{
            //    this.ShowMessage("TY_M_AC_2422N250");
            //}
            UP_Spread_Desc();
        }
        #endregion

        #region Description : 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sBRANCH = string.Empty;

            sBRANCH = "1";

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_4AGHS187", this.DTP01_WREYYMM.GetValue().ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();
            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_4AGGN186",
                                    this.DTP01_WREYYMM.GetValue().ToString(),
                                    "1");

            DataTable dt2 = this.DbConnector.ExecuteDataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_3BK25384",
                                    sBRANCH.ToString(),
                                    "");

            DataTable dt3 = this.DbConnector.ExecuteDataTable();
            

            SectionReport rpt = new TYACTP009R1(dt2, dt3);
            (new TYERGB001P(rpt, dt)).ShowDialog();

            //SectionReport rpt2 = new TYACTP009R2();
            //(new TYERGB001P(rpt2, dt)).ShowDialog();
        }
        #endregion

        #region Description : 스프레드 타이틀 변경
        private void UP_Spread_Title()
        {
            //-----------------------원천징수명세 및 납부세액-----------------------------------------

            this.FPS91_TY_S_AC_4AKGG208_Sheet1.ColumnHeaderRowCount = 3;
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_4AKGG208_Sheet1.AddColumnHeaderSpanCell(0, 0, 3, 5); //구분
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.AddColumnHeaderSpanCell(0, 5, 3, 1); //코드
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.AddColumnHeaderSpanCell(0, 6, 1, 7); //원천징수내역
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.AddColumnHeaderSpanCell(0, 13, 3, 1); //당월조정환급세액
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.AddColumnHeaderSpanCell(0, 14, 1, 5); //납부세액

            this.FPS91_TY_S_AC_4AKGG208_Sheet1.AddColumnHeaderSpanCell(1, 6, 1, 2); //소득지급
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.AddColumnHeaderSpanCell(1, 8, 1, 5); //징수세액
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.AddColumnHeaderSpanCell(1, 15, 2, 1); //소득세등
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.AddColumnHeaderSpanCell(1, 17, 2, 1); //농어촌특별세
            

            this.FPS91_TY_S_AC_4AKGG208_Sheet1.ColumnHeader.Cells[0, 0].Value = "구    분";
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.ColumnHeader.Cells[0, 5].Value = "코드";
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.ColumnHeader.Cells[0, 6].Value = "원천징수내역";
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.ColumnHeader.Cells[0, 13].Value = "당월조정환급세액";
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.ColumnHeader.Cells[0, 14].Value = "납부세액";

            this.FPS91_TY_S_AC_4AKGG208_Sheet1.ColumnHeader.Cells[1, 6].Value = "소득지급";
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.ColumnHeader.Cells[1, 8].Value = "징수세액";
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.ColumnHeader.Cells[1, 15].Value = "소득세등";
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.ColumnHeader.Cells[1, 17].Value = "농어촌특별세";

            this.FPS91_TY_S_AC_4AKGG208_Sheet1.ColumnHeader.Cells[2, 6].Value = "인원";
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.ColumnHeader.Cells[2, 7].Value = "총지급액";
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.ColumnHeader.Cells[2, 8].Value = "소득세등";
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.ColumnHeader.Cells[2, 10].Value = "농어촌특별세";
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.ColumnHeader.Cells[2, 11].Value = "가산세";

            this.FPS91_TY_S_AC_4AKGG208_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.ColumnHeader.Cells[0, 13].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.ColumnHeader.Cells[0, 14].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_4AKGG208_Sheet1.ColumnHeader.Cells[1, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.ColumnHeader.Cells[1, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.ColumnHeader.Cells[1, 15].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.ColumnHeader.Cells[1, 17].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            //-----------------------------------환급세액 조정------------------------------------------

            this.FPS91_TY_S_AC_4AKGG206_Sheet1.ColumnHeaderRowCount = 3;
            this.FPS91_TY_S_AC_4AKGG206_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_4AKGG206_Sheet1.AddColumnHeaderSpanCell(0, 0, 1, 6); //전월 미환급 세액의 계산
            this.FPS91_TY_S_AC_4AKGG206_Sheet1.AddColumnHeaderSpanCell(0, 6, 1, 4); //당월발생 환급세액
            this.FPS91_TY_S_AC_4AKGG206_Sheet1.AddColumnHeaderSpanCell(0, 10, 3, 1); //조정대상 환급세액
            this.FPS91_TY_S_AC_4AKGG206_Sheet1.AddColumnHeaderSpanCell(0, 11, 3, 1); //당월조정 환급세액계
            this.FPS91_TY_S_AC_4AKGG206_Sheet1.AddColumnHeaderSpanCell(0, 12, 3, 1); //차월이월 환급세액
            this.FPS91_TY_S_AC_4AKGG206_Sheet1.AddColumnHeaderSpanCell(0, 13, 3, 1); //환급 신청액

            this.FPS91_TY_S_AC_4AKGG206_Sheet1.AddColumnHeaderSpanCell(1, 3, 2, 1); //전월 미환급세액
            this.FPS91_TY_S_AC_4AKGG206_Sheet1.AddColumnHeaderSpanCell(1, 4, 2, 1); //기환급 신청세액
            this.FPS91_TY_S_AC_4AKGG206_Sheet1.AddColumnHeaderSpanCell(1, 5, 2, 1); //차감잔액
            this.FPS91_TY_S_AC_4AKGG206_Sheet1.AddColumnHeaderSpanCell(1, 6, 2, 1); //일반환급
            this.FPS91_TY_S_AC_4AKGG206_Sheet1.AddColumnHeaderSpanCell(1, 7, 2, 1); //신탁재산
            this.FPS91_TY_S_AC_4AKGG206_Sheet1.AddColumnHeaderSpanCell(1, 8, 1, 2); //그 밖의 환급세액

            this.FPS91_TY_S_AC_4AKGG206_Sheet1.ColumnHeader.Cells[0, 0].Value = "전월 미환급 세액의 계산";
            this.FPS91_TY_S_AC_4AKGG206_Sheet1.ColumnHeader.Cells[0, 6].Value = "당월발생 환급세액";
            this.FPS91_TY_S_AC_4AKGG206_Sheet1.ColumnHeader.Cells[0, 10].Value = "조정대상 환급세액";
            this.FPS91_TY_S_AC_4AKGG206_Sheet1.ColumnHeader.Cells[0, 11].Value = "당월조정 환급세액계";
            this.FPS91_TY_S_AC_4AKGG206_Sheet1.ColumnHeader.Cells[0, 12].Value = "차월이월 환급세액";
            this.FPS91_TY_S_AC_4AKGG206_Sheet1.ColumnHeader.Cells[0, 13].Value = "환급 신청액";

            this.FPS91_TY_S_AC_4AKGG206_Sheet1.ColumnHeader.Cells[1, 3].Value = "전월 미환급세액";
            this.FPS91_TY_S_AC_4AKGG206_Sheet1.ColumnHeader.Cells[1, 4].Value = "기환급 신청세액";
            this.FPS91_TY_S_AC_4AKGG206_Sheet1.ColumnHeader.Cells[1, 5].Value = "차감잔액";
            this.FPS91_TY_S_AC_4AKGG206_Sheet1.ColumnHeader.Cells[1, 6].Value = "일반환급";
            this.FPS91_TY_S_AC_4AKGG206_Sheet1.ColumnHeader.Cells[1, 7].Value = "신탁재산";
            this.FPS91_TY_S_AC_4AKGG206_Sheet1.ColumnHeader.Cells[1, 8].Value = "그 밖의 환급세액";

            this.FPS91_TY_S_AC_4AKGG206_Sheet1.ColumnHeader.Cells[2, 8].Value = "금융회사";
            this.FPS91_TY_S_AC_4AKGG206_Sheet1.ColumnHeader.Cells[2, 9].Value = "합병";
            

            this.FPS91_TY_S_AC_4AKGG206_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AKGG206_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AKGG206_Sheet1.ColumnHeader.Cells[0, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AKGG206_Sheet1.ColumnHeader.Cells[0, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AKGG206_Sheet1.ColumnHeader.Cells[0, 12].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AKGG206_Sheet1.ColumnHeader.Cells[0, 13].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_4AKGG206_Sheet1.ColumnHeader.Cells[1, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AKGG206_Sheet1.ColumnHeader.Cells[1, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AKGG206_Sheet1.ColumnHeader.Cells[1, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AKGG206_Sheet1.ColumnHeader.Cells[1, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AKGG206_Sheet1.ColumnHeader.Cells[1, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AKGG206_Sheet1.ColumnHeader.Cells[1, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion

        #region Description : 스프레트 틀 만들기
        private void UP_Spread_Desc()
        {
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.ColumnCount = 19;
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.RowCount = 25;

            this.FPS91_TY_S_AC_4AKGG208_Sheet1.AddSpanCell(0, 0, 5, 1); // 근로소득
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.AddSpanCell(5, 0, 3, 1); // 퇴직소득
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.AddSpanCell(8, 0, 3, 1); // 사업소득
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.AddSpanCell(11, 0, 3, 1); // 기타소득
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.AddSpanCell(14, 0, 4, 1); // 연금소득

            this.FPS91_TY_S_AC_4AKGG208_Sheet1.AddSpanCell(18, 0, 1, 5); // 이자소득
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.AddSpanCell(19, 0, 1, 5); // 배당소득
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.AddSpanCell(20, 0, 1, 5); // 저축해지 추징세액
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.AddSpanCell(21, 0, 1, 5); // 비거주자양도소득
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.AddSpanCell(22, 0, 1, 5); // 법인원천
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.AddSpanCell(23, 0, 1, 5); // 수정신고
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.AddSpanCell(24, 0, 1, 5); // 총합계

            this.FPS91_TY_S_AC_4AKGG208_Sheet1.Cells[0, 0].Value = "근로소득";
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.Cells[5, 0].Value = "퇴직소득";
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.Cells[8, 0].Value = "사업소득";
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.Cells[11, 0].Value = "기타소득";
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.Cells[14, 0].Value = "연금소득";

            this.FPS91_TY_S_AC_4AKGG208_Sheet1.Cells[18, 0].Value = "이자소득";
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.Cells[19, 0].Value = "배당소득";
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.Cells[20, 0].Value = "저축해지 추징세액 등";
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.Cells[21, 0].Value = "비거주자양도소득";
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.Cells[22, 0].Value = "법인원천";
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.Cells[23, 0].Value = "수정신고(세액)";
            this.FPS91_TY_S_AC_4AKGG208_Sheet1.Cells[24, 0].Value = "총 합 계";
        }
        #endregion

        #region Description : 부표 스프레드 타이틀 변경
        private void UP_Spread_Title_Sub()
        {
            //-----------------------원천징수명세 및 납부세액-----------------------------------------

            this.FPS91_TY_S_AC_4ARF4256_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_4ARF4256_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 6); //구분
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.AddColumnHeaderSpanCell(0, 6, 2, 1); //코드
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.AddColumnHeaderSpanCell(0, 7, 1, 2); //소득지급
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.AddColumnHeaderSpanCell(0, 9, 1, 5); //징수세액
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.AddColumnHeaderSpanCell(0, 14, 2, 1); //조정환급세액
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.AddColumnHeaderSpanCell(0, 15, 1, 5); //납부세액

            this.FPS91_TY_S_AC_4ARF4256_Sheet1.ColumnHeader.Cells[0, 0].Value = "소득자 소득구분";
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.ColumnHeader.Cells[0, 6].Value = "코드";
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.ColumnHeader.Cells[0, 7].Value = "소득지급";
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.ColumnHeader.Cells[0, 9].Value = "징수세액";
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.ColumnHeader.Cells[0, 14].Value = "조정환급세액";
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.ColumnHeader.Cells[0, 15].Value = "납부세액";

            this.FPS91_TY_S_AC_4ARF4256_Sheet1.ColumnHeader.Cells[1, 7].Value = "인원";
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.ColumnHeader.Cells[1, 8].Value = "총지급액";
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.ColumnHeader.Cells[1, 9].Value = "소득세등";
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.ColumnHeader.Cells[1, 11].Value = "농어촌특별세";
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.ColumnHeader.Cells[1, 12].Value = "가산세";
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.ColumnHeader.Cells[1, 16].Value = "소득세등(가산세)";
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.ColumnHeader.Cells[1, 18].Value = "농어촌특별세";

            this.FPS91_TY_S_AC_4ARF4256_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.ColumnHeader.Cells[0, 9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.ColumnHeader.Cells[0, 14].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.ColumnHeader.Cells[0, 15].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            //-----------------------------------환급세액 조정------------------------------------------

            this.FPS91_TY_S_AC_4ARF5257_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_4ARF5257_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 5); //구분
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.AddColumnHeaderSpanCell(0, 5, 2, 1); //코드
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.AddColumnHeaderSpanCell(0, 6, 1, 2); //소득지급
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.AddColumnHeaderSpanCell(0, 8, 1, 5); //징수세액
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.AddColumnHeaderSpanCell(0, 13, 2, 1); //조정환급세액
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.AddColumnHeaderSpanCell(0, 14, 1, 5); //납부세액

            this.FPS91_TY_S_AC_4ARF5257_Sheet1.ColumnHeader.Cells[0, 0].Value = "소득자 소득구분";
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.ColumnHeader.Cells[0, 5].Value = "코드";
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.ColumnHeader.Cells[0, 6].Value = "소득지급";
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.ColumnHeader.Cells[0, 8].Value = "징수세액";
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.ColumnHeader.Cells[0, 13].Value = "조정환급세액";
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.ColumnHeader.Cells[0, 14].Value = "납부세액";

            this.FPS91_TY_S_AC_4ARF5257_Sheet1.ColumnHeader.Cells[1, 6].Value = "인원";
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.ColumnHeader.Cells[1, 7].Value = "총지급액";
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.ColumnHeader.Cells[1, 8].Value = "소득세등";
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.ColumnHeader.Cells[1, 10].Value = "농어촌특별세";
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.ColumnHeader.Cells[1, 11].Value = "가산세";
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.ColumnHeader.Cells[1, 15].Value = "소득세등(가산세)";
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.ColumnHeader.Cells[1, 17].Value = "농어촌특별세";

            this.FPS91_TY_S_AC_4ARF5257_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.ColumnHeader.Cells[0, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.ColumnHeader.Cells[0, 13].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.ColumnHeader.Cells[0, 14].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            
        }
        #endregion

        #region Description : 부표 스프레트 틀 만들기
        private void UP_Spread_Desc_Sub()
        {
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.ColumnCount = 20;
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.RowCount = 46;

            this.FPS91_TY_S_AC_4ARF4256_Sheet1.AddSpanCell(0, 0, 39, 1); // 이자배당소득
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.AddSpanCell(39, 0, 7, 1); // 이자배당소득
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.AddSpanCell(0, 1, 14, 1); // 비과세소득
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.AddSpanCell(14, 1, 8, 1); // 세금특례
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.AddSpanCell(22, 1, 2, 1); // 세금우대
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.AddSpanCell(24, 1, 8, 1); // 일반세율분리과세
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.AddSpanCell(32, 1, 2, 1); // 일반과세
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.AddSpanCell(34, 1, 2, 1); // 비실명소득
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.AddSpanCell(36, 1, 1, 5); // 비영업대금이익
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.AddSpanCell(37, 1, 1, 5); // 출자공동사업자
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.AddSpanCell(38, 1, 1, 5); // 이자배다소득계
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.AddSpanCell(39, 1, 1, 5); // 장기주식형저축
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.AddSpanCell(40, 1, 1, 5); // 장기주택마련저축
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.AddSpanCell(41, 1, 1, 5); // 연금저축
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.AddSpanCell(42, 1, 1, 5); // 소기업소상공인공제부금
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.AddSpanCell(43, 1, 1, 5); // 주택청약종합저축
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.AddSpanCell(44, 1, 1, 5); // 장기집합투자증권저축
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.AddSpanCell(45, 1, 1, 5); // 해지추징계

            this.FPS91_TY_S_AC_4ARF4256_Sheet1.Cells[0, 0].Value = "이자ㆍ\n배당소득";
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.Cells[39, 0].Value = "해지추징\n세액 등";

            this.FPS91_TY_S_AC_4ARF4256_Sheet1.Cells[0, 1].Value = "비과세 소득";
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.Cells[14, 1].Value = "세금특례";
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.Cells[22, 1].Value = "세금우대";
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.Cells[24, 1].Value = "일반세율분리과세";
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.Cells[32, 1].Value = "일반과세";
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.Cells[34, 1].Value = "비실명 소득";

            this.FPS91_TY_S_AC_4ARF4256_Sheet1.Cells[36, 1].Value = "비영업대금이익";
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.Cells[37, 1].Value = "출자공동사업자";
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.Cells[38, 1].Value = "이자ㆍ배당소득계";
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.Cells[39, 1].Value = "장기주식형저축";
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.Cells[40, 1].Value = "장기주택마련저축";
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.Cells[41, 1].Value = "연금저축";
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.Cells[42, 1].Value = "소기업ㆍ소상공인공제부금";
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.Cells[43, 1].Value = "주택청약종합저축";
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.Cells[44, 1].Value = "장기집합투자증권저축";
            this.FPS91_TY_S_AC_4ARF4256_Sheet1.Cells[45, 1].Value = "해지추징계";

            //-------------------------------------------------------------------------------

            this.FPS91_TY_S_AC_4ARF5257_Sheet1.ColumnCount = 19;
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.RowCount = 24;

            this.FPS91_TY_S_AC_4ARF5257_Sheet1.AddSpanCell(0, 0, 1, 5); // 이자
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.AddSpanCell(1, 0, 1, 5); // 배당
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.AddSpanCell(2, 0, 3, 1); // 사업
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.AddSpanCell(5, 0, 2, 1); // 양도
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.AddSpanCell(7, 0, 1, 5); // 기타
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.AddSpanCell(8, 0, 1, 5); // 비거주자 계
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.AddSpanCell(9, 0, 6, 1); // 내국법인
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.AddSpanCell(15, 0, 8, 1); // 외국법인
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.AddSpanCell(23, 0, 1, 5); // 법인세 계

            this.FPS91_TY_S_AC_4ARF5257_Sheet1.Cells[0, 0].Value = "이자";
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.Cells[1, 0].Value = "배당";
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.Cells[2, 0].Value = "사업";
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.Cells[5, 0].Value = "양도";
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.Cells[7, 0].Value = "기타";
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.Cells[8, 0].Value = "비거주자 계";
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.Cells[9, 0].Value = "내국법인";
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.Cells[15, 0].Value = "외국법인";
            this.FPS91_TY_S_AC_4ARF5257_Sheet1.Cells[23, 0].Value = "법인세 계";
        }
        #endregion
        
    }
}
