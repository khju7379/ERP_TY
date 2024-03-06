using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.ED00
{
    /// <summary>
    /// 반출입 정정신고관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.03.30 15:47
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_73UGF165 : 반출입 정정신고관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_73UGG166 : 반출입 정정신고관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYEDKB010S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYEDKB010S()
        {
            InitializeComponent();
        }

        private void TYEDKB010S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_SDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            UP_SetLockCheck();

            this.SetStartingFocus(this.DTP01_SDATE);

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_73UGG166.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_73UGF165", this.CBO01_EDIRECHIP.GetValue(), this.CBO01_EDIGJ.GetValue().ToString(), this.DTP01_SDATE.GetString(), this.DTP01_EDATE.GetString() );
            this.FPS91_TY_S_UT_73UGG166.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_UT_73UGG166.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_UT_73UGG166.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_UT_73UGG166.GetValue(i, "EDIRERCVGB").ToString() == "Y")
                    {
                        this.FPS91_TY_S_UT_73UGG166_Sheet1.Rows[i].ForeColor = Color.Blue;
                    }
                    else if (this.FPS91_TY_S_UT_73UGG166.GetValue(i, "EDIRERCVGB").ToString() == "N")
                    {
                        this.FPS91_TY_S_UT_73UGG166_Sheet1.Rows[i].ForeColor = Color.Red;
                    }
                }
            }
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_UT_744HT204", dt.Rows[i]["EDIGJ"].ToString(),
                                                            dt.Rows[i]["EDIRENO1"].ToString(),
                                                            dt.Rows[i]["EDIRENO2"].ToString(),
                                                            dt.Rows[i]["EDIRENO3"].ToString(),
                                                            dt.Rows[i]["EDIRECHASU"].ToString()
                                                            );
                this.DbConnector.Attach("TY_P_UT_744HT205", dt.Rows[i]["EDIGJ"].ToString(),
                                                            dt.Rows[i]["EDIRENO1"].ToString(),
                                                            dt.Rows[i]["EDIRENO2"].ToString(),
                                                            dt.Rows[i]["EDIRENO3"].ToString(),
                                                            dt.Rows[i]["EDIRECHASU"].ToString()
                                                            );
            }

            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD874");

        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_UT_73UGG166.GetDataSourceInclude(TSpread.TActionType.Remove, "EDIGJ", "EDIRENO1", "EDIRENO2", "EDIRENO3", "EDIRECHASU", "EDIRERCVGB");

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
                    if (dt.Rows[i]["EDIRERCVGB"].ToString() == "Y")
                    {
                        this.ShowCustomMessage("접수 완료된 자료는 삭제할수 없습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
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

        #region  Description : 조회 버튼 이벤트
        private void FPS91_TY_S_UT_73UGG166_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYEDKB010I(this.FPS91_TY_S_UT_73UGG166.GetValue("EDIGJ").ToString(),
                                   this.FPS91_TY_S_UT_73UGG166.GetValue("EDIRENONUM").ToString().Substring(0, 8),
                                   this.FPS91_TY_S_UT_73UGG166.GetValue("EDIRENONUM").ToString().Substring(8, 4),
                                   this.FPS91_TY_S_UT_73UGG166.GetValue("EDIRENONUM").ToString().Substring(12, 8),
                                   this.FPS91_TY_S_UT_73UGG166.GetValue("EDIRECHASU").ToString(),
                                   this.FPS91_TY_S_UT_73UGG166.GetValue("EDIRECHIP").ToString(),
                                   "EDIT"
                             )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : Lock Check
        private void UP_SetLockCheck()
        {
            if (TYUserInfo.DeptCode.Substring(0, 1) == "S")
            {
                CBO01_EDIGJ.SetValue("S");
            }
            else
            {
                CBO01_EDIGJ.SetValue("T");
            }

            if (TYUserInfo.DeptCode.Substring(0, 6) != "A10300")
            {
                CBO01_EDIGJ.SetReadOnly(true);
            }
        }
        #endregion

       
    }
}
