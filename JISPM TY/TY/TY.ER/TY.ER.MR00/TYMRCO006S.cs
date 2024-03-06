using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.MR00
{
    /// <summary>
    /// 품목 코드 관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.11.06 13:01
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_2B62S132 : 품목코드 조회
    ///  TY_P_MR_2B62Y135 : 품목코드 등록
    ///  TY_P_MR_2B62Z136 : 품목코드 수정
    ///  TY_P_MR_2B62Z137 : 품목코드 삭제
    ///  TY_P_MR_2B631138 : 품목코드 확인
    ///  TY_P_MR_2B24D041 : 품목 중분류조회(콤보박스)
    ///  TY_P_MR_2B24D042 : 품목 소분류조회(콤보박스)
    ///  TY_P_MR_2B634139 : 품목코드-요청내역 품목 삭제체크
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_2B635140 : 품목코드 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_MR_2B63K141 : 요청내역에 품목코드가 존재합니다.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_MR_2B550096 : 품목 중분류코드를 선택하세요.
    ///  TY_M_MR_2B551097 : 품목 소분류코드를 선택하세요.
    ///  TY_M_MR_2B559095 : 품목 대분류코드를 선택하세요.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  Z105000 : 대분류코드
    ///  Z105001 : 중분류코드
    ///  Z105002 : 소분류코드
    ///  Z105013 : 자재명１
    /// </summary>
    public partial class TYMRCO006S : TYBase
    {
        #region Description : 페이지 로드
        public TYMRCO006S()
        {
            InitializeComponent();
        }

        private void TYMRCO006S_Load(object sender, System.EventArgs e)
        {
            // 삭제 체크
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            SetStartingFocus(this.CBO01_Z105000);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_MR_2B62S132",
                this.CBO01_Z105000.GetValue(),
                this.CBO01_Z105001.GetValue(),
                this.CBO01_Z105002.GetValue(),
                this.TXT01_Z105013.GetValue(),
                this.TXT01_Z105029.GetValue()
                );

            this.FPS91_TY_S_MR_2B635140.SetValue(this.DbConnector.ExecuteDataTable());

            for (int i = 0; i < this.FPS91_TY_S_MR_2B635140.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_MR_2B635140.GetValue(i, "Z106000").ToString() == "")
                {
                    this.FPS91_TY_S_MR_2B635140_Sheet1.Cells[i, 13].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                }
            }

            this.CBO01_Z105000.Focus();
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYMRCO006I(this.CBO01_Z105000.GetValue().ToString(), this.CBO01_Z105001.GetValue().ToString(),
                                this.CBO01_Z105002.GetValue().ToString(), "",
                                this.CBO01_Z105000.GetText().ToString(), this.CBO01_Z105001.GetText().ToString(),
                                this.CBO01_Z105002.GetText().ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)

                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            string sFIJPCODE = string.Empty;

            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_MR_2B62Z137", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.DbConnector.CommandClear();
            // 품목사진 삭제
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // 품목코드
                sFIJPCODE = dt.Rows[i]["Z105000"].ToString() + dt.Rows[i]["Z105001"].ToString() + dt.Rows[i]["Z105002"].ToString() + Set_Fill5(dt.Rows[i]["Z105003"].ToString());

                this.DbConnector.Attach("TY_P_MR_34493433", sFIJPCODE.ToString());
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sPRN1050 = string.Empty;

            DataTable dt = this.FPS91_TY_S_MR_2B635140.GetDataSourceInclude(TSpread.TActionType.Remove, "Z105000", "Z105001", "Z105002", "Z105003");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // 품목 코드
                sPRN1050 = dt.Rows[i]["Z105000"].ToString() + dt.Rows[i]["Z105001"].ToString() + dt.Rows[i]["Z105002"].ToString() + dt.Rows[i]["Z105003"].ToString();

                // 요청내역 품목 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_MR_2B634139",
                                       sPRN1050.ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_MR_2B63K141");
                    e.Successed = false;
                    return;
                }
            }

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region Description : 대분류코드 이벤트
        private void CBO01_Z105000_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2B24D041",
                this.CBO01_Z105000.GetValue()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.CBO01_Z105001.DataBind(dt, true);
        }
        #endregion

        #region Description : 중분류코드 이벤트
        private void CBO01_Z105001_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2B24D042",
                this.CBO01_Z105000.GetValue(),
                this.CBO01_Z105001.GetValue()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.CBO01_Z105002.DataBind(dt, true);
        }
        #endregion

        #region Description : 스프레드 클릭 이벤트
        private void FPS91_TY_S_MR_2B635140_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            // 파라미터값 보내기
            if ((new TYMRCO006I(this.FPS91_TY_S_MR_2B635140.GetValue("Z105000").ToString(), this.FPS91_TY_S_MR_2B635140.GetValue("Z105001").ToString(),
                                this.FPS91_TY_S_MR_2B635140.GetValue("Z105002").ToString(), this.FPS91_TY_S_MR_2B635140.GetValue("Z105003").ToString(),
                                this.FPS91_TY_S_MR_2B635140.GetValue("LMDESC").ToString(),  this.FPS91_TY_S_MR_2B635140.GetValue("MMDESC").ToString(),
                                this.FPS91_TY_S_MR_2B635140.GetValue("SMDESC").ToString()
                               )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        private void FPS91_TY_S_MR_2B635140_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "13") // (그룹웨어 요청문서 바로가기)
            {
                if (this.FPS91_TY_S_MR_2B635140.GetValue("Z106000").ToString() != "")
                {
                    if ((new TYMRCO007I(this.FPS91_TY_S_MR_2B635140.GetValue("JEPUM").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        this.BTN61_INQ_Click(null, null);
                    }
                }
                else
                {
                    this.ShowMessage("TY_M_MR_2BC51262");
                    return;
                }
            }
        }
    }
}