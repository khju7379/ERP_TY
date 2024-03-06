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
    /// 받을어음 보관관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.08.21 17:10
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_25L45582 : 받을어음 수정
    ///  TY_P_AC_28K5C448 : 받을어음기타관리 삭제
    ///  TY_P_AC_28L4O467 : 보관어음관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_28L5E478 : 보관어음 관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  E6CDCL : 거래처코드
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    /// </summary>
    public partial class TYACEI002S : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYACEI002S()
        {
            InitializeComponent();
        }

        private void TYACEI002S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_GSTDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_GEDDATE.SetValue(DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd"));

            this.SetStartingFocus(DTP01_GSTDATE);

            
        }
        #endregion

        #region Description : 조회 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_28L4O467", this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString(),this.CBO01_E6PRGN.GetValue(), this.CBH01_E6CDCL.GetValue());

            this.FPS91_TY_S_AC_28L5E478.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 삭제 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            for (int i = 0 ; i < dt.Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_28K5C448", dt.Rows[i]["E6NONR"].ToString(), dt.Rows[i]["E6IDBG"].ToString(), dt.Rows[i]["E6DTBG"].ToString());

                this.DbConnector.Attach("TY_P_AC_25L45582", dt.Rows[i]["E7HIDBG"].ToString(), dt.Rows[i]["E7HDTBG"].ToString(),
                                                            dt.Rows[i]["E7HCDGL"].ToString(), dt.Rows[i]["E6NONR"].ToString());
            }
            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 저장 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            if ((new TYACEI002I(ds)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);

            
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_AC_28L5E478.GetDataSourceInclude(TSpread.TActionType.Select, "E6NONR", "E6DTCO", "E6IDBG", "E6IDBGNM", "E6BODATE", "E6CDCL", "E6CDCLNM", "E6AMNR", "E6DTED", "E7HIDBG", "E7HDTBG", "E7HCDGL"));           
            
            e.ArgData = ds;
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.FPS91_TY_S_AC_28L5E478.CurrentRowCount == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            DataTable dt = this.FPS91_TY_S_AC_28L5E478.GetDataSourceInclude(TSpread.TActionType.Select, "E6NONR", "E6IDBG", "E6DTBG", "E7HIDBG", "E7HDTBG", "E7HCDGL");

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
    }
}
