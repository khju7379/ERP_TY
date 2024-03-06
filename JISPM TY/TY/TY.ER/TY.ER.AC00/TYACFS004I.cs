using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 개별분석 충당금 설정관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.07.23 18:24
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_27N6V215 : 개별분석 충당금 설정관리 조회
    ///  TY_P_AC_27N6W217 : 개별분석 충당금 설정관리 등록
    ///  TY_P_AC_27N6X218 : 개별분석 충당금 설정관리 수정
    ///  TY_P_AC_27N6X221 : 개별분석 충당금 설정관리 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_27N7D230 : 개별분석 충당금 설정관리 조회
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
    ///  COPY : 복사
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  BYCODE : 개별분석코드
    ///  BYYYMM : 기준년월
    /// </summary>
    public partial class TYACFS004I : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYACFS004I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_27N7D230, "BYCODE", "BYCODENM", "BYCODE");
        }

        private void TYACFS004I_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27N7D230, "BYYYMM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27N7D230, "BYCODE");

            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_27N7D230, "BYYYMM");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_27N7D230, "BYCODE");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_27N7D230, "BYCODENM");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_27N7D230, "BYRATE");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_BYYYMM.SetValue(DateTime.Now.ToString("yyyyMM"));

            SetStartingFocus(DTP01_BYYYMM);

            this.BTN61_INQ_Click(null, null);  
        }
        #endregion

        #region Description : 복사 버튼 이벤트
        private void BTN61_COPY_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYACFS004B()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_27N6V215", this.ControlFactory, "01");
            this.FPS91_TY_S_AC_27N7D230.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DataTableColumnAdd(ds.Tables[0], "BYHISAB", TYUserInfo.EmpNo);
            this.DataTableColumnAdd(ds.Tables[1], "BYHISAB", TYUserInfo.EmpNo);

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_27N6W217", ds.Tables[0]); //ADD
            this.DbConnector.Attach("TY_P_AC_27N6X218", ds.Tables[1]); //UPDATE

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_27N6X221", dt);

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");
            this.BTN61_INQ_Click(null, null);

        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int iRowCnt = 0;
            int iRowEqual = 0;

            DataSet ds = new DataSet();

            // 저장
            ds.Tables.Add(this.FPS91_TY_S_AC_27N7D230.GetDataSourceInclude(TSpread.TActionType.New, "BYYYMM", "BYCODE", "BYRATE"));

            // 수정
            ds.Tables.Add(this.FPS91_TY_S_AC_27N7D230.GetDataSourceInclude(TSpread.TActionType.Update, "BYYYMM", "BYCODE", "BYRATE"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
                e.Successed = false;
                return;
            }

            // 저장 체크
            DataSet dsChk = ds.Copy();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                iRowEqual = 0;

                if (ds.Tables[0].Rows[i]["BYYYMM"].ToString() != "" && ds.Tables[0].Rows[i]["BYCODE"].ToString() != "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_27N7P236", ds.Tables[0].Rows[i]["BYYYMM"].ToString(),
                                                                ds.Tables[0].Rows[i]["BYCODE"].ToString());
                    iRowCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                    if (iRowCnt > 0)
                    {
                        this.ShowMessage("TY_M_GB_23S40973");
                        e.Successed = false;
                        return;
                    }
                }

                for (int j = 0; j < dsChk.Tables[0].Rows.Count; j++)
                {
                    if (ds.Tables[0].Rows[i]["BYYYMM"].ToString() == dsChk.Tables[0].Rows[j]["BYYYMM"].ToString() &&
                         ds.Tables[0].Rows[i]["BYCODE"].ToString() == dsChk.Tables[0].Rows[j]["BYCODE"].ToString())
                    {
                        iRowEqual = iRowEqual + 1;
                    }
                }

                if (iRowEqual > 1)
                {
                    this.ShowMessage("TY_M_GB_23S40973");
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            dsChk.Dispose(); 

            e.ArgData = ds;
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_27N7D230.GetDataSourceInclude(TSpread.TActionType.Remove, "BYYYMM", "BYCODE");

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

        #region Description : FPS91_TY_S_AC_27N7D230_RowInserted 이벤트
        private void FPS91_TY_S_AC_27N7D230_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_27N7D230.SetValue(e.RowIndex, "BYYYMM", DateTime.Now.ToString("yyyy-MM"));
        }
        #endregion
    }
}
