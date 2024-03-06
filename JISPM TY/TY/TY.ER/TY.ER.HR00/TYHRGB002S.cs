using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.Drawing;

namespace TY.ER.HR00
{
    /// <summary>
    /// 용역직 인사기본사항 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2015.01.16 17:38
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_51GAM167 : 용역직 인사기본사항 조회(그리드)
    ///  TY_P_HR_51JHE187 : 용역직 인사기본사항 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_8539A917 : 용역직 인사기본사항 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  EDDATE : 종료일자
    ///  KBHANGL : 한글이름
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYHRGB002S : TYBase
    {
        #region Description : 폼 로드
        public TYHRGB002S()
        {
            InitializeComponent();
        }

        private void TYHRGB002S_Load(object sender, System.EventArgs e)
        {
            // 삭제 체크
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_8539A917.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_852G3904", this.TXT01_BANAME.GetValue().ToString(),
                                                        this.TXT01_BAVEND.GetValue().ToString(),
                                                        this.CBO01_BASTOPGN.GetValue().ToString()
                                                        );

            this.FPS91_TY_S_HR_8539A917.SetValue(this.DbConnector.ExecuteDataTable());

            if(this.FPS91_TY_S_HR_8539A917.CurrentRowCount > 0 )
            {
                for (int i = 0; i < FPS91_TY_S_HR_8539A917.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_HR_8539A917.GetValue(i, "BASTOPGN").ToString() == "Y")
                    {
                        this.FPS91_TY_S_HR_8539A917.ActiveSheet.Rows[i].ForeColor = Color.Red;
                    }
                }

            }

        }
        #endregion

        #region Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYHRGB002I(string.Empty, string.Empty, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null,null);
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_HR_8539N921", ds.Tables[0].Rows[i]["BASEQ"],
                                                            ds.Tables[0].Rows[i]["BANAME"],
                                                            ds.Tables[0].Rows[i]["BAJUMIN"]);
            }
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_HR_8539A917.GetDataSourceInclude(TSpread.TActionType.Remove, "BASEQ", "BANAME", "BAJUMIN"));

            if (ds.Tables[0].Rows.Count == 0)
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

            e.ArgData = ds;
        }
        #endregion

        #region Description : 그리드 더블클릭 이벤트
        private void FPS91_TY_S_HR_8539A917_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYHRGB002I(this.FPS91_TY_S_HR_8539A917.GetValue("BASEQ").ToString(), this.FPS91_TY_S_HR_8539A917.GetValue("BANAME").ToString(),
                                this.FPS91_TY_S_HR_8539A917.GetValue("BAJUMIN").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion
    }
}
