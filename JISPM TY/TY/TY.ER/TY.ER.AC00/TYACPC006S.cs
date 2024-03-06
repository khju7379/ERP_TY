using System;
using System.Data;
using System.Drawing;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 투하자산 받을어음 관리 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2012.07.18 10:52
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_27H64059 : EIS 마감 CHECK  확인
    ///  TY_P_AC_27I30089 : EIS 투하자산 받을어음 삭제
    ///  TY_P_AC_27I38086 : EIS 투하자산 받을어음 조회
    ///  TY_P_AC_27I39087 : EIS 투하자산 받을어음 등록
    ///  TY_P_AC_27I39088 : EIS 투하자산 받을어음 수정
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_27I31090 : EIS 투하자산 받을어음 관리
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_27H6I062 : EIS 마감 년월이 존재 하지 않습니다.
    ///  TY_M_AC_27H6I063 : EIS 적용 완료상태 입니다. (처리 불가)
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_23S40973 : 동일한 코드가 존재합니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  EIBYYMM : 년월
    /// </summary>
    public partial class TYACPC006S : TYBase
    {
        #region Description : Page_Load
        public TYACPC006S()
        {
            InitializeComponent();
        }

        private void TYACPC006S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            //키필드는 신규일때만 수정된다.
            //this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_27I31090, "EIBYYMM");

            this.TXT01_EIBYYMM.Focus(); 

            this.BTN61_INQ_Click(null, null);
        } 
        #endregion

        #region Description : 조회
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_27I38086", this.ControlFactory, "01");
            this.FPS91_TY_S_AC_27I31090.SetValue(this.DbConnector.ExecuteDataTable());

            UP_SumRowAdd();
        } 
        #endregion

        #region Description : 삭제
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_27I30089", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        } 
        #endregion

        #region Description : 저장
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_27I39087", ds.Tables[0]); //저장
            this.DbConnector.Attach("TY_P_AC_27I39088", ds.Tables[1]); //수정
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD873"); 
        } 
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            UP_SumRowAdd();

            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_AC_27I31090.GetDataSourceInclude(TSpread.TActionType.New, "EIBYYMM", "EIBDRTT", "EIBDRSS", "EIBDRO1", "EIBDRO2", "EIBDRTO"));
            ds.Tables.Add(this.FPS91_TY_S_AC_27I31090.GetDataSourceInclude(TSpread.TActionType.Update, "EIBYYMM", "EIBDRTT", "EIBDRSS", "EIBDRO1", "EIBDRO2", "EIBDRTO"));

            //신규
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_27I38086", ds.Tables[0].Rows[i]["EIBYYMM"].ToString());
                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_GB_23S40973");
                    e.Successed = false;
                    return;
                }

                // 마감 완료 CHECK 
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_27H64059", ds.Tables[0].Rows[i]["EIBYYMM"].ToString().Substring(0, 4), ds.Tables[0].Rows[i]["EIBYYMM"].ToString().Substring(4, 2));
                DataTable dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count == 0)
                {
                    this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                    e.Successed = false;
                    return;
                }
                else
                {
                    if (dt1.Rows[0]["ECGUBUN"].ToString() == "Y")
                    {
                        this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                        e.Successed = false;
                        return;
                    }
                }
            }

            //수정시 CHECK
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                // 마감 완료 CHECK 
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_27H64059", ds.Tables[1].Rows[i]["EIBYYMM"].ToString().Substring(0, 4), ds.Tables[1].Rows[i]["EIBYYMM"].ToString().Substring(4, 2));
                DataTable dt2 = this.DbConnector.ExecuteDataTable();

                if (dt2.Rows.Count == 0)
                {
                    this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                    e.Successed = false;
                    return;
                }
                else
                {
                    if (dt2.Rows[0]["ECGUBUN"].ToString() == "Y")
                    {
                        this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_27I31090.GetDataSourceInclude(TSpread.TActionType.Remove, "EIBYYMM");


            //삭제시 CHECK
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // 마감 완료 CHECK 
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_27H64059", dt.Rows[i]["EIBYYMM"].ToString().Substring(0, 4), dt.Rows[i]["EIBYYMM"].ToString().Substring(4, 2));
                DataTable dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count == 0)
                {
                    this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                    e.Successed = false;
                    return;
                }
                else
                {
                    if (dt1.Rows[0]["ECGUBUN"].ToString() == "Y")
                    {
                        this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                        e.Successed = false;
                        return;
                    }
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


        #region Description : 특정 Row와 Column 값 변경
        private void UP_SumRowAdd()
        {
            int i = 0;

            this.SpreadSumRowAdd(this.FPS91_TY_S_AC_27I31090, "EIBYYMM", "합 계", Color.Yellow);

            for (i = 0; i < this.FPS91_TY_S_AC_27I31090.ActiveSheet.RowCount; i++)
            {
                this.FPS91_TY_S_AC_27I31090_Sheet1.SetFormula(
                    i, // row
                    5, // column
                    "R[0]C[-1] + R[0]C[-2] + R[0]C[-3] + R[0]C[-4]"); //
            }

            this.FPS91_TY_S_AC_27I31090.ActiveSheet.Rows[i - 1].Visible = false;
        }
        #endregion


    }
}
