using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 연장관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2014.11.25 16:53
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BPJQ529 : 연장관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_P_HR_616BB408 : 연장관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  GYSABUN : 사  번
    ///  GYGUBN : 신청형태
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRCS002I : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRCS002I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_616E9414, "FOSABUN", "KBHANGL", "FOSABUN");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_616E9414, "FODEPT", "DPDESC", "FODEPT");
        }

        private void TYHRCS002I_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_616E9414, "FOSABUN");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_616E9414, "FODEPT");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_STDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_FODATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 좌측 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            bool returnValue = UP_SearchCheck();

            if (returnValue != true)
            {
                return;
            }

            string sArea = string.Empty;

            if(this.CBO01_KBGUNMU.GetValue().ToString() == "1")
            {
                if(this.CBO01_GGUBUN.GetValue().ToString() == "2")
                {
                    sArea = "2";
                }
                else if (this.CBO01_GGUBUN.GetValue().ToString() == "3")
                {
                    sArea = "3";
                }
            }

            this.FPS91_TY_S_HR_616BU409.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_616BB408", this.DTP01_STDATE.GetValue(), this.DTP01_EDDATE.GetValue(), this.CBO01_KBGUNMU.GetValue(), sArea.ToString());            
            this.FPS91_TY_S_HR_616BU409.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 우측 조회 버튼 이벤트
        private void BTN61_INQ_FXM_Click(object sender, EventArgs e)
        {
            string sArea = string.Empty;

            if (this.CBO01_KBGUNMU.GetValue().ToString() == "1")
            {
                if (this.CBO01_GGUBUN.GetValue().ToString() == "2")
                {
                    sArea = "2";
                }
                else if (this.CBO01_GGUBUN.GetValue().ToString() == "3")
                {
                    sArea = "3";
                }
            }

            this.FPS91_TY_S_HR_616E9414.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_616E8413", this.DTP01_FODATE.GetValue(), this.CBO01_FOGUBUN.GetValue(), this.CBO01_KBGUNMU.GetValue(), sArea.ToString());
            this.FPS91_TY_S_HR_616E9414.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 그리드 더블 클릭 이벤트
        private void FPS91_TY_P_HR_616BB408_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DTP01_FODATE.SetValue(this.FPS91_TY_S_HR_616BU409.GetValue("FODATE").ToString());
            this.CBO01_FOGUBUN.SetValue(this.FPS91_TY_S_HR_616BU409.GetValue("FOGUBUN").ToString());

            this.BTN61_INQ_FXM_Click(null, null);
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_HR_616GU419", this.DTP01_FODATE.GetValue(),
                                                            this.CBO01_FOGUBUN.GetValue(),
                                                            dt.Rows[i]["FOSABUN"].ToString(),
                                                            dt.Rows[i]["FODEPT"].ToString(),
                                                            TYUserInfo.EmpNo
                                                            );
            }

            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);
            this.BTN61_INQ_FXM_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_HR_616GU418", this.DTP01_FODATE.GetValue(),
                                                            this.CBO01_FOGUBUN.GetValue(),
                                                            dt.Rows[i]["FOSABUN"].ToString()
                                                            );
            }

            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);
            this.BTN61_INQ_FXM_Click(null, null);
                       
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_616E9414.GetDataSourceInclude(TSpread.TActionType.New, "FOSABUN", "FODEPT");

            if (dt.Rows.Count == 0)
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

            e.ArgData = dt;
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_616E9414.GetDataSourceInclude(TSpread.TActionType.Remove, "FOSABUN");

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

        #region  Description : 조회 체크
        private bool UP_SearchCheck()
        {
            //if (Convert.ToInt32(this.TXT01_GSTYYMM.GetValue().ToString()) > Convert.ToInt32(this.TXT01_GEDYYMM.GetValue().ToString()))
            //{
            //    this.ShowCustomMessage("시작일자는 종료일자보다 이전이어야 합니다. ", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            //    return false;
            //}


            //if (this.TXT01_GDATE.GetValue().ToString() != "" && int.Parse(this.TXT01_GSTYYMM.GetValue().ToString()) < int.Parse(this.TXT01_GDATE.GetValue().ToString().Substring(0, 4)) && int.Parse(this.TXT01_GEDYYMM.GetValue().ToString()) < int.Parse(this.TXT01_GDATE.GetValue().ToString().Substring(0, 4)))
            //{
            //    this.ShowCustomMessage("기준년월을 년도 범위내에서 지정해주세요!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            //    return false;
            //}
            //if (this.TXT01_GDATE.GetValue().ToString() != "" && int.Parse(this.TXT01_GSTYYMM.GetValue().ToString()) > int.Parse(this.TXT01_GDATE.GetValue().ToString().Substring(0, 4)))
            //{
            //    this.ShowCustomMessage("기준년월을 년도 범위내에서 지정해주세요!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            //    return false;
            //}
            //if (this.TXT01_GDATE.GetValue().ToString() != "" && int.Parse(this.TXT01_GEDYYMM.GetValue().ToString()) < int.Parse(this.TXT01_GDATE.GetValue().ToString().Substring(0, 4)))
            //{
            //    this.ShowCustomMessage("기준년월을 년도 범위내에서 지정해주세요!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            //    return false;
            //}
            //if (this.TXT01_GDATE.GetValue().ToString() == "" && CBO01_KBGUNMU.GetValue().ToString() != "3")
            //{
            //    this.ShowCustomMessage("진행구분이 전체가 아닌경우에는 기준년월을 입력하세요!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            //    return false;
            //}

            return true;
        }
        #endregion

        #region Description : 진행구분 이벤트
        private void CBO01_GGUBUN_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            SetFocus(this.BTN61_INQ);
        }
        #endregion

        private void FPS91_TY_S_HR_616E9414_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            if (e.Column != 2)
                return;

            // 부서명을 가져오기 위해서 스프레드의 예산년도에 파라미터 날짜를 넣음.
            string year = this.DTP01_FODATE.GetValue().ToString();
            TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_HR_616E9414, "FODEPT");
            if (tyCodeBox != null)
                tyCodeBox.DummyValue = year;
        }        
    }
}