using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.MR00
{
    /// <summary>
    /// 구매발주 관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.11.19 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_2BD2L282 : 구매요청 마스터(확인) - 팝업
    ///  TY_P_MR_2BJ11461 : 구매발주 - 결재완료 체크
    ///  TY_P_MR_2BJ19460 : 구매발주 - 입고번호체크
    ///  TY_P_MR_2BJ1A466 : 구매발주 내역사항 조회 - 팝업
    ///  TY_P_MR_2BJ1A467 : 구매발주 특기사항 조회 - 팝업
    ///  TY_P_MR_2BJAF449 : 구매발주 마스터 조회
    ///  TY_P_MR_2BJAJ450 : 구매발주 일괄삭제 - 마스터, 내역, 특기(삭제)
    ///  TY_P_MR_2BJAT455 : 구매발주 일괄삭제 - 요청예산 업데이트(플러스)
    ///  TY_P_MR_2BJAZ456 : 구매발주 일괄삭제 - 발주예산 업데이트(마이너스)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_2BJ1F468 : 구매발주 마스터 조회
    ///  TY_S_MR_74HGZ300 : 구매요청 마스터 조회(미발주)
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_MR_2BC51262 : 결재 완료 된 문서가 아닙니다.
    ///  TY_M_MR_2BC59266 : 결재 완료 된 자료이므로 작업이 불가합니다.
    ///  TY_M_MR_2BE4Z311 : 특기 사항이 존재합니다.
    ///  TY_M_MR_2BE4Z312 : 내역 사항이 존재 합니다.
    ///  TY_M_MR_2BJ4K470 : 입고자료에 발주번호가 존재합니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  POM1000 : 사업부
    ///  POM1010 : 발주구분
    ///  POM1020 : 년월
    ///  POM1180 : 공사및구매명
    /// </summary>
    public partial class TYMRPO001S : TYBase
    {
        private string fsPOM1000 = string.Empty;
        private string fsPOM1010 = string.Empty;
        private string fsPOM1020 = string.Empty;
        private string fsPOM1030 = string.Empty;

        #region Description : 페이지 로드
        public TYMRPO001S()
        {
            InitializeComponent();
        }

        private void TYMRPO001S_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_MR_2BJ1F468.Initialize();

            UP_SET_BUSEO();

            this.TXT01_POM1010.SetValue("O");
            this.TXT01_POM1010.SetReadOnly(true);

            SetStartingFocus(TXT01_POM1000);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_MR_2BJ1F468.Initialize();
            this.FPS91_TY_S_MR_74HGZ300.Initialize();

            this.DbConnector.CommandClear();

            if (this.CBO01_POGUBN.GetValue().ToString() == "1")         //전체
            {
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BJAF449",
                    this.TXT01_POM1000.GetValue(),
                    this.TXT01_POM1010.GetValue(),
                    this.TXT01_POM1020.GetValue(),
                    this.TXT01_POM1180.GetValue(),
                    this.CBH01_GCDDP.GetValue()
                    );
            }
            else if (this.CBO01_POGUBN.GetValue().ToString() == "2")         //미발주
            {
                this.DbConnector.Attach
                    (
                    "TY_P_MR_74EIJ288",
                    this.TXT01_POM1000.GetValue(),
                    "P",
                    this.TXT01_POM1020.GetValue(),
                    this.TXT01_POM1180.GetValue(),
                    this.CBH01_GCDDP.GetValue()
                    );
            }
            else if (this.CBO01_POGUBN.GetValue().ToString() == "3")         //결재중
            {
                this.DbConnector.Attach
                    (
                    "TY_P_MR_74HGD293",
                    this.TXT01_POM1000.GetValue(),
                    this.TXT01_POM1010.GetValue(),
                    this.TXT01_POM1020.GetValue(),
                    this.TXT01_POM1180.GetValue(),
                    this.CBH01_GCDDP.GetValue()
                    );
            }
            else if (this.CBO01_POGUBN.GetValue().ToString() == "4")         //결재완료
            {
                this.DbConnector.Attach
                    (
                    "TY_P_MR_74HGE296",
                    this.TXT01_POM1000.GetValue(),
                    this.TXT01_POM1010.GetValue(),
                    this.TXT01_POM1020.GetValue(),
                    this.TXT01_POM1180.GetValue(),
                    this.CBH01_GCDDP.GetValue()
                    );
            }
            else if (this.CBO01_POGUBN.GetValue().ToString() == "5")         //입고중
            {
                this.DbConnector.Attach
                    (
                    "TY_P_MR_74JF3347",
                    this.TXT01_POM1000.GetValue(),
                    this.TXT01_POM1010.GetValue(),
                    this.TXT01_POM1020.GetValue(),
                    this.TXT01_POM1180.GetValue(),
                    this.CBH01_GCDDP.GetValue()
                    );
            }
            else if (this.CBO01_POGUBN.GetValue().ToString() == "6")         //입고완료
            {
                this.DbConnector.Attach
                    (
                    "TY_P_MR_74HGK298",
                    this.TXT01_POM1000.GetValue(),
                    this.TXT01_POM1010.GetValue(),
                    this.TXT01_POM1020.GetValue(),
                    this.TXT01_POM1180.GetValue(),
                    this.CBH01_GCDDP.GetValue()
                    );
            }

            if (this.CBO01_POGUBN.GetValue().ToString() == "2")
            {
                this.FPS91_TY_S_MR_74HGZ300.SetValue(this.DbConnector.ExecuteDataTable());

                this.FPS91_TY_S_MR_2BJ1F468.Visible = false;
                this.FPS91_TY_S_MR_74HGZ300.Visible = true;
            }
            else
            {
                this.FPS91_TY_S_MR_2BJ1F468.SetValue(this.DbConnector.ExecuteDataTable());

                this.FPS91_TY_S_MR_2BJ1F468.Visible = true;
                this.FPS91_TY_S_MR_74HGZ300.Visible = false;
            }

            for (int i = 0; i < this.FPS91_TY_S_MR_2BJ1F468.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_MR_2BJ1F468.GetValue(i, "POM1230").ToString() == "")
                {
                    this.FPS91_TY_S_MR_2BJ1F468_Sheet1.Cells[i, 12].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                }
            }
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYMRPO001I("", "O", "", "")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 스프레드 클릭 이벤트
        private void FPS91_TY_S_MR_2BJ1F468_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            // 파라미터값 보내기
            if ((new TYMRPO001I(this.FPS91_TY_S_MR_2BJ1F468.GetValue("POM1000").ToString(), this.FPS91_TY_S_MR_2BJ1F468.GetValue("POM1010").ToString(),
                                this.FPS91_TY_S_MR_2BJ1F468.GetValue("POM1020").ToString(), this.FPS91_TY_S_MR_2BJ1F468.GetValue("POM1030").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 구매발주 마스터 그리드 이벤트(그룹웨어 문서 바로가기)
        private void FPS91_TY_S_MR_2BJ1F468_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "12") // (그룹웨어 발주문서 바로가기)
            {
                if (this.FPS91_TY_S_MR_2BJ1F468.GetValue("POM1230").ToString() != "")
                {
                    if ((new TYMRPR005S(this.FPS91_TY_S_MR_2BJ1F468.GetValue("POM1230").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                    }
                }
                else
                {
                    this.ShowMessage("TY_M_MR_2BC51262");
                    return;
                }
            }
        }
        #endregion

        #region Description : 부서코드 가져오기
        private void UP_SET_BUSEO()
        {
            // 부서코드
            this.CBH01_GCDDP.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BEBB293",
                DateTime.Now.ToString("yyyyMMdd"),
                TYUserInfo.EmpNo
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                // 부서코드
                this.CBH01_GCDDP.SetValue(dt.Rows[0]["KBBUSEO"].ToString());
            }

            // 사업부
            this.TXT01_POM1000.SetValue(this.CBH01_GCDDP.GetValue().ToString().Substring(0, 1));

            // 발주년월
            this.TXT01_POM1020.SetValue(DateTime.Now.ToString("yyyyMMdd").Substring(0, 6));
        }
        #endregion

        #region Description : 발주년월 이벤트
        private void TXT01_POM1020_TextChanged(object sender, EventArgs e)
        {
            if (this.TXT01_POM1020.GetValue().ToString().Length == 6)
            {
                // 부서코드
                this.CBH01_GCDDP.DummyValue = this.TXT01_POM1020.GetValue().ToString() + "01";
            }
        }
        #endregion

        #region Description : 스프레드 클릭 이벤트(미발주)
        private void FPS91_TY_S_MR_74HGZ300_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            // 파라미터값 보내기
            if ((new TYMRPO001I(this.FPS91_TY_S_MR_74HGZ300.GetValue("PRNUM").ToString().Substring(0, 1), this.FPS91_TY_S_MR_74HGZ300.GetValue("PRNUM").ToString().Substring(2, 1),
                                this.FPS91_TY_S_MR_74HGZ300.GetValue("PRNUM").ToString().Substring(4, 6), this.FPS91_TY_S_MR_74HGZ300.GetValue("PRNUM").ToString().Substring(11, 4))).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}