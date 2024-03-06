using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using GrapeCity.ActiveReports;
using TY.ER.GB00;


namespace TY.ER.AC00
{
    /// <summary>
    /// 받을어음 보관 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.08.14 17:14
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_28E5X397 : 받을어음 보관 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_28E67400 : 받을어음 보관 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYACEI003S : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYACEI003S()
        {
            InitializeComponent();
        }

        private void TYACEI003S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));

            this.DTP01_EDDATE.SetValue(DateTime.Now.AddMonths(1).ToString("yyyyMMdd"));

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_28E67400.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_28E5X397", this.DTP01_STDATE.GetString(), this.DTP01_EDDATE.GetString());
            this.FPS91_TY_S_AC_28E67400.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_28E5X397", this.DTP01_STDATE.GetString(), this.DTP01_EDDATE.GetString());
            ds = this.DbConnector.ExecuteDataSet();

            if (ds.Tables[0].Rows.Count > 0)
            {
                SectionReport rptMaster = null;
                rptMaster = new TYACEI003R();
                rptMaster.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rptMaster, ds.Tables[0])).ShowDialog();
            }
        }
        #endregion
    }
}
