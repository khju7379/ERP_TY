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
    /// 부가세신고자료수정 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.06.14 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_26E5W882 : 부가세신고자료수정 조회
    ///  TY_P_AC_26FBO890 : 부가세신고자료 수정
    ///  TY_P_AC_26FBN889 : 부가세신고자료 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_26E5W883 : 부가세신고자료수정 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  V1GUBN : 매입．매출구분
    ///  V1YYMM : 발생년월
    /// </summary>
    public partial class TYACGT002I : TYBase
    {
        private TYData DAT03_V1HIGB;
        private TYData DAT03_V1HISAB;

        #region Description : 페이지 로드
        public TYACGT002I()
        {
            InitializeComponent();

            this.DAT03_V1HIGB = new TYData("DAT03_V1HIGB", null);
            this.DAT03_V1HISAB = new TYData("DAT03_V1HISAB", null);
        }

        private void TYACGT002I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.ControlFactory.Add(this.DAT03_V1HIGB);
            this.ControlFactory.Add(this.DAT03_V1HISAB);

            SetStartingFocus(this.TXT01_V1YYMM);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_26E5W882",
                this.TXT01_V1YYMM.GetValue().ToString(),
                this.CBO01_V1GUBN.GetValue().ToString()
                );

            this.FPS91_TY_S_AC_26E5W883.SetValue(this.DbConnector.ExecuteDataTable());

            // 스프레드 합계 색깔
            // 기본적으로 SQL문에서 합계를 가져와야 함
            this.SetSpreadSumRow(this.FPS91_TY_S_AC_26E5W883, "V1YYMM", "합 계", SumRowType.Total);

            // 마지막 ROW 잠금
            this.FPS91_TY_S_AC_26E5W883.ActiveSheet.Rows[this.FPS91_TY_S_AC_26E5W883.ActiveSheet.Rows.Count - 1].Locked = true;
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DataTableColumnAdd(ds.Tables[0], "V1HIGB", "C");
            //this.DataTableColumnAdd(ds.Tables[0], "V1HISAB", "0311-M");
            this.DataTableColumnAdd(ds.Tables[0], "V1HISAB", TYUserInfo.EmpNo);

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_26FBO890",
                ds.Tables[0]
                );

            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_26FBN889", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_26E5W883.GetDataSourceInclude(TSpread.TActionType.Update, "V1YYMM", "V1CDAC", "V1VEND", "V1GUBN", "V1JPNO", "V1AMT", "V1VAT"));

            if (ds.Tables[0].Rows.Count == 0)
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

            e.ArgData = ds;
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_26E5W883.GetDataSourceInclude(TSpread.TActionType.Remove, "V1YYMM", "V1CDAC", "V1VEND", "V1GUBN", "V1JPNO");

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