using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 급여대상자관리 팝업 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2014.12.18 17:03
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4CJDS888 : 급여대상자관리 삭제
    ///  TY_P_HR_4CJDT890 : 급여대상자관리 등록
    ///  TY_P_HR_4CJDT891 : 급여대상자관리 수정
    ///  TY_P_HR_4CJDX894 : 급여대상자관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_51CEW099 : 급여대상자관리 조회
    ///  TY_S_HR_51CEQ098 : 급여대상자 리스트 조회
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
    ///  PTGUBN : 급여구분
    ///  PTJIDATE : 지급일자
    ///  PTYYMM : 급여년월
    /// </summary>
    public partial class TYHRFR001B : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRFR001B()
        {
            InitializeComponent();
        }

        private void TYHRFR001B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            //this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.BTN62_INQ.ProcessCheck += new TButton.CheckHandler(BTN62_INQ_ProcessCheck);

            this.DTP01_COYYMM.SetReadOnly(true);
            this.CBH01_COGUBN.SetReadOnly(true);
            this.DTP01_COJIDATE.SetReadOnly(true);

            this.FPS91_TY_S_HR_51CEQ098.Initialize();
            this.FPS91_TY_S_HR_51CEW099.Initialize();

            this.BTN62_INQ.SetValue(">>");

            this.BTN61_INQ_Click(null, null);

            this.CBH01_COSABUN.CodeText.Focus();
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.UP_GetSABUNLIST();
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region  Description : 인사기본 리스트
        private void UP_GetSABUNLIST()
        {
            // 기본인사관리 퇴사자 외 리스트 가져오기
            this.FPS91_TY_S_HR_51CEQ098.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_51CEK097", this.CBH01_COSABUN.GetValue().ToString());
            this.FPS91_TY_S_HR_51CEQ098.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        //#region  Description : 삭제 버튼 이벤트
        //private void BTN61_REM_Click(object sender, EventArgs e)
        //{
        //    int iRowIndex = this.FPS91_TY_S_HR_51CEW099.ActiveSheet.Rows.Count;

        //    for (int i = 0; i < iRowIndex; i++)
        //    {
        //        iRowIndex = iRowIndex + 1;

        //        if (this.FPS91_TY_S_HR_51CEW099.ActiveSheet.RowHeader.Cells[iRowIndex - 1, 0].Text == "D")
        //        {
        //            this.FPS91_TY_S_HR_51CEW099.ActiveSheet.Rows[i].Remove();
        //        }
        //    }
        //}

        //private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        //{
        //    DataTable dt = this.FPS91_TY_S_HR_51CEW099.GetDataSourceInclude(TSpread.TActionType.Remove, "COSABUN");

        //    if (dt.Rows.Count == 0)
        //    {
        //        this.ShowMessage("TY_M_GB_23NAD870");
        //        e.Successed = false;
        //        return;
        //    }

        //    if (!this.ShowMessage("TY_M_GB_23NAD872"))
        //    {
        //        e.Successed = false;
        //        return;
        //    }
        //}
        //#endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            this.DataTableColumnAdd(ds.Tables[0], "COYYMM",    this.DTP01_COYYMM.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "COGUBN",    this.CBH01_COGUBN.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "COJIDATE",  this.DTP01_COJIDATE.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "COINGUBN",  this.CBH01_COINGUBN.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "COFLAG",    this.CBO01_COFLAG.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "COAMT",     this.TXT01_COAMT.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "COINSABUN", this.CBH01_COINSABUN.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "COINCDBK",  this.CBH01_COINCDBK.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "COINNOAC",  this.TXT01_COINNOAC.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "COBIGO",    this.TXT01_COBIGO.GetValue().ToString());
            this.DataTableColumnAdd(ds.Tables[0], "COHISAB",   TYUserInfo.EmpNo);

            this.DbConnector.Attach("TY_P_HR_51C94086", ds.Tables[0]);
            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);
            
            this.ShowMessage("TY_M_GB_23NAD873");
        }
        
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_HR_51CEW099.GetDataSourceInclude(TSpread.TActionType.New, "COSABUN"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            // 급여이체 자료 존재 체크
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_51CHQ112", this.DTP01_COYYMM.GetValue().ToString(),
                                                            this.CBH01_COGUBN.GetValue().ToString(),
                                                            this.DTP01_COJIDATE.GetValue().ToString()
                                                            );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_HR_51CIB113");
                    e.Successed = false;
                    return;
                }

                // 급여징수관리 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_51C9G088", this.DTP01_COYYMM.GetValue().ToString(),
                                                            this.CBH01_COGUBN.GetValue().ToString(),
                                                            this.DTP01_COJIDATE.GetValue().ToString(),
                                                            ds.Tables[0].Rows[i]["COSABUN"].ToString(),
                                                            this.CBH01_COINGUBN.GetValue().ToString()
                                                            );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowCustomMessage(ds.Tables[0].Rows[i]["COSABUN"].ToString() + "사번은 이미 급여징수 대상자에 등록되어 있습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
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

        #region  Description : 선택 버튼 이벤트
        private void BTN62_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_51CG3102");
            this.FPS91_TY_S_HR_51CEW099.SetValue(this.DbConnector.ExecuteDataTable());

            this.FPS91_TY_S_HR_51CEW099.Initialize();

            int iRowIndex = 0;
            
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            if (dt.Rows.Count > 0)
            {
                iRowIndex = this.FPS91_TY_S_HR_51CEW099.ActiveSheet.Rows.Count;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    iRowIndex = iRowIndex + 1;

                    this.FPS91_TY_S_HR_51CEW099.ActiveSheet.AddRows(iRowIndex -1, 1);
                    this.FPS91_TY_S_HR_51CEW099.ActiveSheet.RowHeader.Cells[iRowIndex-1, 0].Text = "N";

                    this.FPS91_TY_S_HR_51CEW099.ActiveSheet.Cells[iRowIndex - 1, 0].Text = dt.Rows[i]["KBSABUN"].ToString();
                    this.FPS91_TY_S_HR_51CEW099.ActiveSheet.Cells[iRowIndex - 1, 1].Text = dt.Rows[i]["KBHANGL"].ToString();
                    this.FPS91_TY_S_HR_51CEW099.ActiveSheet.Cells[iRowIndex - 1, 2].Text = dt.Rows[i]["KBBUSEO"].ToString();
                    this.FPS91_TY_S_HR_51CEW099.ActiveSheet.Cells[iRowIndex - 1, 3].Text = dt.Rows[i]["KBBUSEONM"].ToString();
                }
            }

            this.BTN61_INQ_Click(null, null);
        }

        private void BTN62_INQ_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_51CEQ098.GetDataSourceInclude(TSpread.TActionType.Select, "KBSABUN", "KBHANGL", "KBBUSEO", "KBBUSEONM");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_25F59464");
                e.Successed = false;
                return;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < this.FPS91_TY_S_HR_51CEW099.ActiveSheet.Rows.Count; j++)
                {
                    if (dt.Rows[i]["KBSABUN"].ToString() == this.FPS91_TY_S_HR_51CEW099.ActiveSheet.Cells[j, 0].Text.Trim())
                    {
                        this.ShowCustomMessage("급여징수 대상자에 등록되어 있습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (!this.ShowMessage("TY_M_HR_51CFF100"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region Description : 선택버튼 이벤트
        private void BTN61_SEL_Click(object sender, EventArgs e)
        {
            TYHRPY006P popup = new TYHRPY006P();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DTP01_COYYMM.SetValue(popup.fsPAYYYMM);
                this.CBH01_COGUBN.SetValue(popup.fsPAYGUBN);
                this.DTP01_COJIDATE.SetValue(popup.fsPAYJIDATE);

                this.CBH01_COINGUBN.CodeText.Focus();
            }
        }
        #endregion

        #region Description : 사번 이벤트
        private void CBH01_COSABUN_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.BTN61_INQ);
            }
        }
        #endregion
    }
}