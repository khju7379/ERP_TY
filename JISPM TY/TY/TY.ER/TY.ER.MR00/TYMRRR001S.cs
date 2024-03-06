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
    ///  TY_P_MR_2BQBW631 : 구매입고 마스터 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_2BQBY632 : 구매발주 마스터 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_MR_2BC51262 : 결재 완료 된 문서가 아닙니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  RRM1000 : 사업부
    ///  RRM1010 : 발주구분
    ///  RRM1020 : 년월
    ///  POM1180 : 공사및구매명
    /// </summary>
    public partial class TYMRRR001S : TYBase
    {
        private string fsPOM1000 = string.Empty;
        private string fsPOM1010 = string.Empty;
        private string fsPOM1020 = string.Empty;
        private string fsPOM1030 = string.Empty;

        #region Description : 페이지 로드
        public TYMRRR001S()
        {
            InitializeComponent();
        }

        private void TYMRRR001S_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_MR_2BQBY632.Initialize();

            UP_SET_BUSEO();

            this.TXT01_RRM1010.SetValue("R");
            this.TXT01_RRM1010.SetReadOnly(true);

            SetStartingFocus(TXT01_RRM1000);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_MR_2BQBY632.Initialize();

            this.DbConnector.CommandClear();

            if (this.CBO01_RRGUBN.GetValue().ToString() == "1")         //전체
            {

                this.DbConnector.Attach
                    (
                    "TY_P_MR_2BQBW631",
                    this.TXT01_RRM1000.GetValue(),
                    this.TXT01_RRM1010.GetValue(),
                    this.TXT01_RRM1020.GetValue(),
                    this.TXT01_POM1180.GetValue(),
                    this.CBH01_GCDDP.GetValue()
                    );
            }
            else if (this.CBO01_RRGUBN.GetValue().ToString() == "2")         //미입고
            {
                this.DbConnector.Attach
                    (
                    "TY_P_MR_74J9B321",
                    this.TXT01_RRM1000.GetValue(),
                    "O",
                    this.TXT01_RRM1020.GetValue(),
                    this.TXT01_POM1180.GetValue(),
                    this.CBH01_GCDDP.GetValue()
                    );
            }
            else if (this.CBO01_RRGUBN.GetValue().ToString() == "3")         //입고중
            {
                this.DbConnector.Attach
                    (
                    "TY_P_MR_74J9O324",
                    this.TXT01_RRM1000.GetValue(),
                    this.TXT01_RRM1010.GetValue(),
                    this.TXT01_RRM1020.GetValue(),
                    this.TXT01_POM1180.GetValue(),
                    this.CBH01_GCDDP.GetValue()
                    );
            }
            else if (this.CBO01_RRGUBN.GetValue().ToString() == "4")         //입고완료
            {
                this.DbConnector.Attach
                    (
                    "TY_P_MR_74J9N323",
                    this.TXT01_RRM1000.GetValue(),
                    this.TXT01_RRM1010.GetValue(),
                    this.TXT01_RRM1020.GetValue(),
                    this.TXT01_POM1180.GetValue(),
                    this.CBH01_GCDDP.GetValue()
                    );
            }

            if (this.CBO01_RRGUBN.GetValue().ToString() == "2")
            {
                this.FPS91_TY_S_MR_74J9P325.SetValue(this.DbConnector.ExecuteDataTable());

                this.FPS91_TY_S_MR_2BQBY632.Visible = false;
                this.FPS91_TY_S_MR_74J9P325.Visible = true;
            }
            else
            {
                this.FPS91_TY_S_MR_2BQBY632.SetValue(this.DbConnector.ExecuteDataTable());

                this.FPS91_TY_S_MR_2BQBY632.Visible = true;
                this.FPS91_TY_S_MR_74J9P325.Visible = false;
            }

            this.FPS91_TY_S_MR_2BQBY632.SetValue(this.DbConnector.ExecuteDataTable());

            for (int i = 0; i < this.FPS91_TY_S_MR_2BQBY632.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_MR_2BQBY632.GetValue(i, "RRM1460").ToString() == "")
                {
                    this.FPS91_TY_S_MR_2BQBY632_Sheet1.Cells[i, 11].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                }
            }
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYMRRR001I("", "R", "", "")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 스프레드 클릭 이벤트
        private void FPS91_TY_S_MR_2BQBY632_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            // 파라미터값 보내기
            if ((new TYMRRR001I(this.FPS91_TY_S_MR_2BQBY632.GetValue("RRM1000").ToString(), this.FPS91_TY_S_MR_2BQBY632.GetValue("RRM1010").ToString(),
                                this.FPS91_TY_S_MR_2BQBY632.GetValue("RRM1020").ToString(), this.FPS91_TY_S_MR_2BQBY632.GetValue("RRM1030").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 구매발주 마스터 그리드 이벤트(그룹웨어 문서 바로가기)
        private void FPS91_TY_S_MR_2BQBY632_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "11") // (그룹웨어 입고문서 바로가기)
            {
                if (this.FPS91_TY_S_MR_2BQBY632.GetValue("RRM1460").ToString() != "")
                {
                    if ((new TYMRPR005S(this.FPS91_TY_S_MR_2BQBY632.GetValue("RRM1460").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
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
            this.TXT01_RRM1000.SetValue(this.CBH01_GCDDP.GetValue().ToString().Substring(0, 1));

            // 입고년월
            this.TXT01_RRM1020.SetValue(DateTime.Now.ToString("yyyyMMdd").Substring(0, 6));
        }
        #endregion

        #region Description : 입고년월 이벤트
        private void TXT01_RRM1020_TextChanged(object sender, EventArgs e)
        {
            if (this.TXT01_RRM1020.GetValue().ToString().Length == 6)
            {
                // 부서코드
                this.CBH01_GCDDP.DummyValue = this.TXT01_RRM1020.GetValue().ToString() + "01";
            }
        }
        #endregion

        #region Description : 스프레드 클릭 이벤트(미입고)
        private void FPS91_TY_S_MR_74J9P325_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            // 파라미터값 보내기
            if ((new TYMRRR001I(this.FPS91_TY_S_MR_74J9P325.GetValue("PONUM").ToString().Substring(0, 1), this.FPS91_TY_S_MR_74J9P325.GetValue("PONUM").ToString().Substring(2, 1),
                                this.FPS91_TY_S_MR_74J9P325.GetValue("PONUM").ToString().Substring(4, 6), this.FPS91_TY_S_MR_74J9P325.GetValue("PONUM").ToString().Substring(11, 4))).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}