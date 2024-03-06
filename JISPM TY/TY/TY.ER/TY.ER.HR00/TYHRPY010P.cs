using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 고용.산재보험 요율관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.10.19 19:22
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_5AJJE005 : 고용산재보험 요율 등록
    ///  TY_P_HR_5AJJF006 : 고용산재보험 요율 수정
    ///  TY_P_HR_5AJJG007 : 고용.산재보험 요율 삭제
    ///  TY_P_HR_5AJJJ008 : 고용.산재보험 요율 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_5AK8O010 : 고용.산재보험 요율관리 조회
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
    ///  REM : 삭제
    ///  SAV : 저장
    /// </summary>
    public partial class TYHRPY010P : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRPY010P()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_5AK8O010, "EIRTEAM", "EIRTEAMNM", "EIRTEAM");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_5AK8O010, "EIRCDAC", "EIRCDACNM", "EIRCDAC");

        }

        private void TYHRPY010P_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_5AK8O010, "EIRGUBN");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_5AK8O010, "EIRTEAM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_5AK8O010, "EIRTEAMNM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_5AK8O010, "EIRCDAC");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_5AK8O010, "EIRCDACNM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_5AK8O010, "EIRSDATE");
            

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            
            this.SetStartingFocus(this.CBO01_EIRGUBN);

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_5AK8O010.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_5AJJJ008", this.CBO01_EIRGUBN.GetValue().ToString(), this.CBO01_INQOPTION.GetValue().ToString());
            this.FPS91_TY_S_HR_5AK8O010.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_5AJJG007", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);
        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            
            DataTable dt = this.FPS91_TY_S_HR_5AK8O010.GetDataSourceInclude(TSpread.TActionType.Remove, "EIRGUBN", "EIRTEAM", "EIRCDAC", "EIRSDATE");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }
            else
            {
                //급여전표가 있는지 체크
                Int16 iCnt = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_5AKDB012", "1", "8", dt.Rows[i]["EIRSDATE"].ToString());
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());
                    if (iCnt > 0)
                    {
                        this.ShowCustomMessage("4대보험전표가 존재합니다! 삭제할수 없습니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            try
            {
                this.DbConnector.CommandClear();
                //저장
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        this.DbConnector.Attach("TY_P_HR_5AJJE005", ds.Tables[0].Rows[i]["EIRGUBN"].ToString(),
                                                                    ds.Tables[0].Rows[i]["EIRTEAM"].ToString(),
                                                                    ds.Tables[0].Rows[i]["EIRCDAC"].ToString(),
                                                                    ds.Tables[0].Rows[i]["EIRSDATE"].ToString().Replace("19000101", "").ToString(),
                                                                    ds.Tables[0].Rows[i]["EIRRATE"].ToString(),
                                                                    ds.Tables[0].Rows[i]["EIREDATE"].ToString().Replace("19000101", "").ToString(),
                                                                    ds.Tables[0].Rows[i]["EIRBIGO"].ToString(),
                                                                    TYUserInfo.EmpNo
                                                                    );
                    }
                }
                //수정
                if (ds.Tables[1].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        this.DbConnector.Attach("TY_P_HR_5AJJF006", ds.Tables[1].Rows[i]["EIRRATE"].ToString(),
                                                                    ds.Tables[1].Rows[i]["EIREDATE"].ToString().Replace("19000101", "").ToString(),
                                                                    ds.Tables[1].Rows[i]["EIRBIGO"].ToString(),
                                                                    TYUserInfo.EmpNo,
                                                                    ds.Tables[1].Rows[i]["EIRGUBN"].ToString(),
                                                                    ds.Tables[1].Rows[i]["EIRTEAM"].ToString(),
                                                                    ds.Tables[1].Rows[i]["EIRCDAC"].ToString(),
                                                                    ds.Tables[1].Rows[i]["EIRSDATE"].ToString().Replace("19000101", "").ToString()
                                                                    );
                    }
                }

                this.DbConnector.ExecuteTranQueryList();

                this.ShowMessage("TY_M_GB_23NAD873");

                this.BTN61_INQ_Click(null, null);
            }
            catch
            {

            }
        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_HR_5AK8O010.GetDataSourceInclude(TSpread.TActionType.New, "EIRGUBN", "EIRTEAM", "EIRTEAMNM", "EIRCDAC", "EIRCDACNM", "EIRSDATE", "EIRRATE", "EIREDATE", "EIRBIGO"));
            ds.Tables.Add(this.FPS91_TY_S_HR_5AK8O010.GetDataSourceInclude(TSpread.TActionType.Update, "EIRGUBN", "EIRTEAM", "EIRTEAMNM", "EIRCDAC", "EIRCDACNM", "EIRSDATE", "EIRRATE", "EIREDATE", "EIRBIGO"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            //종료되지 않은 자료가 있으면 등록 불가
            Int16 iCnt = 0;            
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_5AKDI016", ds.Tables[0].Rows[i]["EIRGUBN"].ToString(), ds.Tables[0].Rows[i]["EIRTEAM"].ToString(), ds.Tables[0].Rows[i]["EIRCDAC"].ToString());
                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());
                    if (iCnt > 0)
                    {
                        this.ShowCustomMessage("진행중인 자료가 존재합니다! 종료처리후 등록하세요.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                }               
            }
           

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region  Description : FPS91_TY_S_HR_5AK8O010_EnterCell 이벤트
        private void FPS91_TY_S_HR_5AK8O010_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            if (e.Column != 2)
                return;

            string year = FPS91_TY_S_HR_5AK8O010.GetValue(e.Row, "EIRSDATE").ToString();

            year = year == "19000101" ? "" : year;

            if (year == "")
            {
                this.FPS91_TY_S_HR_5AK8O010.SetValue(e.Row, "EIRSDATE","");
                this.ShowCustomMessage("시작일자를 먼저입력하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_HR_5AK8O010, "EIRTEAM");
            if (tyCodeBox != null)
                tyCodeBox.DummyValue = year;
        }
        #endregion

        #region  Description : CBO01_INQOPTION_SelectedIndexChanged 이벤트
        private void CBO01_INQOPTION_SelectedIndexChanged(object sender, EventArgs e)
        {
            BTN61_INQ_Click(null, null);
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
