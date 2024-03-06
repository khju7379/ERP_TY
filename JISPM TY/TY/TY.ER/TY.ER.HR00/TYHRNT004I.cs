using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 연말정산 추가소득관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.12.12 10:54
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_7CCB0233 : 연말정산 추가소득관리 삭제
    ///  TY_P_HR_7CCB1234 : 연말정산 추가소득관리 수정
    ///  TY_P_HR_7CCB6230 : 연말정산 추가소득관리 조회
    ///  TY_P_HR_7CCB8231 : 연말정산 추가소득관리 확인
    ///  TY_P_HR_7CCB8232 : 연말정산 추가소득관리 등록
    /// 
    ///  # 스프레드 정보 ####
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
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  KBSABUN : 사번
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRNT004I : TYBase
    {

        #region  Description : 폼 로드 이벤트
        public TYHRNT004I()
        {
            InitializeComponent();

            this.SetPopupStyle();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_7CCBJ235, "ACSABUN", "ACSABUNNM", "ACSABUN");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_7CCBJ235, "ACITEM", "ACITEMNM", "ACITEM");  

        }

        private void TYHRNT004I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_7CCBJ235, "ACSABUN");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_7CCBJ235, "ACITEM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_7CCBJ235, "ACYYMM");

            TXT01_SDATE.SetValue(DateTime.Now.ToString("yyyy"));            

            this.UP_Grid_DataBinding();
        }
        #endregion

        #region  Description : 그리드 데이타 바인딩 이벤트
        private void UP_Grid_DataBinding()
        {
            this.FPS91_TY_S_HR_7CCBJ235.Initialize();            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_7CCB6230", "TY", TXT01_SDATE.GetValue(), CBH01_KBSABUN.GetValue());
            this.FPS91_TY_S_HR_7CCBJ235.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion       

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            //등록
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                this.DbConnector.Attach("TY_P_HR_7CCB8232", ds.Tables[0].Rows[i]["ACCOMPANY"].ToString(),
                                                            ds.Tables[0].Rows[i]["ACYEAR"].ToString(),
                                                            ds.Tables[0].Rows[i]["ACSABUN"].ToString(),
                                                            ds.Tables[0].Rows[i]["ACITEM"].ToString(),
                                                            ds.Tables[0].Rows[i]["ACYYMM"].ToString(),
                                                            ds.Tables[0].Rows[i]["ACEXTRAINCOME"].ToString(),
                                                            ds.Tables[0].Rows[i]["ACMEMO"].ToString(),
                                                            TYUserInfo.EmpNo
                                                           );
            }
            //수정
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {

                this.DbConnector.Attach("TY_P_HR_7CCB1234", ds.Tables[1].Rows[i]["ACEXTRAINCOME"].ToString(),
                                                            ds.Tables[1].Rows[i]["ACMEMO"].ToString(),
                                                            TYUserInfo.EmpNo,
                                                            ds.Tables[1].Rows[i]["ACCOMPANY"].ToString(),
                                                            ds.Tables[1].Rows[i]["ACYEAR"].ToString(),
                                                            ds.Tables[1].Rows[i]["ACSABUN"].ToString(),
                                                            ds.Tables[1].Rows[i]["ACITEM"].ToString(),
                                                            ds.Tables[1].Rows[i]["ACYYMM"].ToString()
                                                           );
            }


            if (this.DbConnector.CommandCount > 0)
            {
                this.DbConnector.ExecuteTranQueryList();
            }           
            
            this.UP_Grid_DataBinding();            

            this.ShowMessage("TY_M_GB_23NAD873");
        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_HR_7CCBJ235.GetDataSourceInclude(TSpread.TActionType.New, "ACCOMPANY", "ACYEAR", "ACSABUN", "ACITEM", "ACYYMM", "ACEXTRAINCOME", "ACMEMO"));
            ds.Tables.Add(this.FPS91_TY_S_HR_7CCBJ235.GetDataSourceInclude(TSpread.TActionType.Update, "ACCOMPANY", "ACYEAR", "ACSABUN", "ACITEM", "ACYYMM", "ACEXTRAINCOME", "ACMEMO"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (TXT01_SDATE.GetValue().ToString() != ds.Tables[0].Rows[i]["ACYYMM"].ToString().Substring(0, 4))
                    {
                        this.ShowCustomMessage("귀속년도와 소득년월의 년도는 같아야 합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_77LD4260", ds.Tables[0].Rows[i]["ACCOMPANY"].ToString(), ds.Tables[0].Rows[i]["ACYEAR"].ToString(), ds.Tables[0].Rows[i]["ACSABUN"].ToString(), "", "", "", "");
                    DataTable dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["ADDEDTAX"].ToString() == "Y")
                        {
                            this.ShowCustomMessage("연말정산 정산작업이 완료되었습니다. 등록할 수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            e.Successed = false;
                            return;
                        }
                    }
                }
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_77LD4260", ds.Tables[1].Rows[i]["ACCOMPANY"].ToString(), ds.Tables[1].Rows[i]["ACYEAR"].ToString(), ds.Tables[1].Rows[i]["ACSABUN"].ToString(), "", "", "", "");
                    DataTable dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["ADDEDTAX"].ToString() == "Y")
                        {
                            this.ShowCustomMessage("연말정산 정산작업이 완료되었습니다. 수정할 수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            e.Successed = false;
                            return;
                        }
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

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            UP_Grid_DataBinding();
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_7CCB0233", dt);
            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            DataTable dt = this.FPS91_TY_S_HR_7CCBJ235.GetDataSourceInclude(TSpread.TActionType.Remove, "ACCOMPANY", "ACYEAR", "ACSABUN", "ACITEM", "ACYYMM");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_77LD4260", dt.Rows[i]["ACCOMPANY"].ToString(), dt.Rows[i]["ACYEAR"].ToString(), dt.Rows[i]["ACSABUN"].ToString(), "", "", "", "");
                    DataTable del = this.DbConnector.ExecuteDataTable();
                    if (del.Rows.Count > 0)
                    {
                        if (del.Rows[0]["ADDEDTAX"].ToString() == "Y")
                        {
                            this.ShowCustomMessage("연말정산 정산작업이 완료되었습니다. 삭제할 수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            e.Successed = false;
                            return;
                        }
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

        #region  Description : 복사 버튼 이벤트
        private void BTN61_COPY_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYHRNT04C1()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : FPS91_TY_S_HR_7CCBJ235_RowInserted 이벤트
        private void FPS91_TY_S_HR_7CCBJ235_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_HR_7CCBJ235.SetValue(e.RowIndex, "ACCOMPANY", "TY");
            this.FPS91_TY_S_HR_7CCBJ235.SetValue(e.RowIndex, "ACYEAR", TXT01_SDATE.GetValue().ToString());
            this.FPS91_TY_S_HR_7CCBJ235.SetValue(e.RowIndex, "ACYYMM", TXT01_SDATE.GetValue().ToString()+"12");            
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
