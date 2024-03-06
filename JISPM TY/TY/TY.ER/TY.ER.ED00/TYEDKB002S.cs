using System;
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
    /// 반입보고서 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.02.27 11:54
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_72RDC787 : 반입보고서 조회
    ///  TY_P_UT_72RDE788 : 내국화물 반입보고서 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_72RDH792 : 반입보고서 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  GOKCR : 생성구분
    ///  EDIDATE : 반입일자
    ///  EDIJUKHA : 적하목록
    /// </summary>
    public partial class TYEDKB002S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYEDKB002S()
        {
            InitializeComponent();
        }

        private void TYEDKB002S_Load(object sender, System.EventArgs e)
        {
            ToolStripMenuItem reateEdit = new ToolStripMenuItem("정정신고");
            reateEdit.Click += new EventHandler(reateEdit_ToolStripMenuItem_Click);

            this.FPS91_TY_S_UT_72RDH792.CurrentContextMenu.Items.AddRange(
                new System.Windows.Forms.ToolStripItem[] { new ToolStripSeparator(), reateEdit });

            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            RDB01_CHK1.Checked = true;
            RDB01_CHK2.Checked = false;

            UP_SetLockCheck();
            
            this.DTP01_EDIDATE.SetValue(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"));

            this.BTN61_INQ_Click(null, null);

            this.SetStartingFocus(DTP01_EDIDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sProcId = string.Empty;

            sProcId = RDB01_CHK1.Checked == true ? "TY_P_UT_72RDC787" : "TY_P_UT_72RDE788";

            this.FPS91_TY_S_UT_72RDH792.Initialize();
            this.DbConnector.CommandClear();
            if (RDB01_CHK1.Checked == true )
            {
                this.DbConnector.Attach(sProcId, CBO01_EDIGJ.GetValue(), DTP01_EDIDATE.GetString().ToString(), TXT01_EDIJUKHA.GetValue().ToString());
            }
            else
            {
                this.DbConnector.Attach(sProcId, CBO01_EDIGJ.GetValue(), DTP01_EDIDATE.GetString().ToString().Substring(0,6), TXT01_EDIJUKHA.GetValue().ToString());
            }
            this.FPS91_TY_S_UT_72RDH792.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_UT_72RDH792.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_UT_72RDH792.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_UT_72RDH792.GetValue(i, "EDIRCVGB").ToString() == "Y")
                    {
                        this.FPS91_TY_S_UT_72RDH792_Sheet1.Rows[i].ForeColor = Color.Blue; 
                    }
                    else if (this.FPS91_TY_S_UT_72RDH792.GetValue(i, "EDIRCVGB").ToString() == "E")
                    {
                        this.FPS91_TY_S_UT_72RDH792_Sheet1.Rows[i].ForeColor = Color.Red; 
                    }
                }
            }

        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            if (dt.Rows.Count > 0)
            {
                if (RDB01_CHK1.Checked == true)
                {
                    this.DbConnector.CommandClear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.DbConnector.Attach("TY_P_UT_72S9Z810", dt.Rows[i]["EDIGJ"].ToString(),
                                                                    dt.Rows[i]["EDIDATE"].ToString(),
                                                                    dt.Rows[i]["EDISINGONUM"].ToString());
                    }
                    this.DbConnector.ExecuteNonQueryList();
                }
                else
                {
                    this.DbConnector.CommandClear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.DbConnector.Attach("TY_P_UT_72SA4812", dt.Rows[i]["EDIGJ"].ToString(),
                                                                    dt.Rows[i]["EDIDATE"].ToString(),
                                                                    dt.Rows[i]["EDISINGONUM"].ToString());
                    }
                    this.DbConnector.ExecuteNonQueryList();
                }
            }

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);
        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_UT_72RDH792.GetDataSourceInclude(TSpread.TActionType.Remove, "EDIGJ", "EDIDATE", "EDISINGONUM", "EDIRCVGB");

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
                    if (dt.Rows[i]["EDIRCVGB"].ToString() == "Y")
                    {

                        this.ShowMessage("TY_M_UT_72SA1811");
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

        #region  Description : 정정신고 처리 팝업 이벤트
        private void reateEdit_ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (this.FPS91_TY_S_UT_72RDH792.GetValue("EDISINGONUM").ToString() != "" && this.FPS91_TY_S_UT_72RDH792.GetValue("EDIRCVGB").ToString() == "Y")
            {
                if ((new TYEDKB010I(this.FPS91_TY_S_UT_72RDH792.GetValue("EDIGJ").ToString(),
                                    this.FPS91_TY_S_UT_72RDH792.GetValue("EDISINGONUM").ToString().Substring(0, 8),
                                    this.FPS91_TY_S_UT_72RDH792.GetValue("EDISINGONUM").ToString().Substring(8, 4),
                                    this.FPS91_TY_S_UT_72RDH792.GetValue("EDISINGONUM").ToString().Substring(12, 8),
                                    "",
                                    RDB01_CHK1.Checked == true ?  "5LC" : "004",
                                    "NEW"
                              )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    this.BTN61_INQ_Click(null, null);
            }
            else
            {
                this.ShowCustomMessage("접수가 처리된 자료만 정정신고가 가능합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
        }
        #endregion

    }
}
