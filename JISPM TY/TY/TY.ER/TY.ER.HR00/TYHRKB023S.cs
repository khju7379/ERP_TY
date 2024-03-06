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
    /// 퇴직금 중도정산 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.03.22 11:06
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_73MD9076 : 퇴직금 중간정산 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_73MD0079 : 퇴직금 중간정산 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  EDATE : 종료일자
    ///  MDBUSEO : 부서
    ///  MDCHASU : 정산차수
    ///  MDJKCD : 직급
    ///  MDSABUN : 사번
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRKB023S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRKB023S()
        {
            InitializeComponent();
        }

        private void TYHRKB023S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_SDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.SetStartingFocus(this.DTP01_SDATE);

        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_73MD0079.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_73MD9076", this.DTP01_SDATE.GetString(), DTP01_EDATE.GetString(), CBH01_MDSABUN.GetValue(), CBH01_MDJKCD.GetValue(), CBH01_MDBUSEO.GetValue(), TXT01_MDCHASU.GetValue() );
            this.FPS91_TY_S_HR_73MD0079.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {

            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_73MEC084", dt);
            this.DbConnector.ExecuteTranQueryList();

            //인사기본 중도정산일자 update
            string sLastDate = string.Empty;
            
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_73MD9076", "19900101", "20301231", dt.Rows[i]["MDSABUN"].ToString(), "", "", "");
                    DataTable dm = this.DbConnector.ExecuteDataTable();
                    if (dm.Rows.Count > 0)
                    {
                        sLastDate = dm.Rows[0]["MDACCEDATEADD"].ToString();
                    }

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_73MB3075", sLastDate,
                                                    TYUserInfo.EmpNo,
                                                    "2",
                                                     dt.Rows[i]["MDSABUN"].ToString()
                                                   );
                    this.DbConnector.ExecuteTranQuery();
                }
            }

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);

        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            DataTable dt = this.FPS91_TY_S_HR_73MD0079.GetDataSourceInclude(TSpread.TActionType.Remove, "MDDATE", "MDSABUN" );

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

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYHRKB023I(string.Empty, string.Empty)) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : FPS91_TY_S_HR_73MD0079_CellDoubleClick 이벤트
        private void FPS91_TY_S_HR_73MD0079_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.OpenModalPopup(new TYHRKB023I(this.FPS91_TY_S_HR_73MD0079.GetValue("MDDATE").ToString(),
                                                   this.FPS91_TY_S_HR_73MD0079.GetValue("MDSABUN").ToString()
                                                  )) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}
