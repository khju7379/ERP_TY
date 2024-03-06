using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.ER.GB00;

using DataDynamics.ActiveReports;

namespace TY.ER.AT00
{
    /// <summary>
    /// 사택기본사항 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2018.08.14 18:00
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_88GGS585 : 아파트 기본사항 조회(전체)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_88GGS586 : 아파트 기본사항 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  APHOSU : 호수
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYATKB002S : TYBase
    {
        #region Description : 폼 로드
        public TYATKB002S()
        {
            InitializeComponent();
        }

        private void TYATKB002S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(System.DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            this.DTP01_STDATE.SetReadOnly(true);
            this.DTP01_EDDATE.SetReadOnly(true);

            SetStartingFocus(this.TXT01_APHOSU);

            BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sSTDATE = this.DTP01_STDATE.GetString();
            string sEDDATE = this.DTP01_EDDATE.GetString();

            if (sSTDATE == "19000101" || sSTDATE == "")
            {
                sSTDATE = "19000101";
            }
            if (sEDDATE == "19000101" || sEDDATE == "")
            {
                sEDDATE = "99991231";
            }

            this.FPS91_TY_S_HR_88GGS586.Initialize();

            this.DbConnector.CommandClear();

            if (this.CBO01_ATGUBN.GetValue().ToString() == "1")
            {
                this.DbConnector.Attach
                    (
                    "TY_P_HR_88KEF603",
                    this.TXT01_APHOSU.GetValue().ToString()
                    );
            }
            else
            {
                this.DbConnector.Attach
                    (
                    "TY_P_HR_88GGS585",
                    sSTDATE,
                    sEDDATE,
                    this.TXT01_APHOSU.GetValue().ToString()
                    );
            }

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_HR_88GGS586.SetValue(dt);
            
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYATKB002I(string.Empty, string.Empty)) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);

            //DataTable dt1 = new DataTable();

            //dt1.Columns.Add("APDESC1", typeof(System.String));
            //dt1.Columns.Add("CONTENT", typeof(System.String));
            //dt1.Columns.Add("ASRCODEAMT", typeof(System.String));

            //string[] s1 = { "공동전기료", "관리인급여", "관리인상여", "상수도료", "하수도료", "가스사용료", "분리수거비", "운영비", "적립금", "음식물처리비", "정화조청소비", "저수조청소비", "기타","미납료" };
            //string[] s2 = { "120,000 / 40 = 3,000", "1,000,000 / 40 = 25,000", "200,000 / 40 = 5,000", "20 * 400 = 8,000", "12.5 * 400 = 5,000", "10 * 1,000 = 10,000", "", "40,000 / 40 = 1,000", "", "40,000 / 40 = 1,000", "", "", "400,000 / 40 = 10,000","2018.6월미납" };
            //string[] s3 = { "3000", "25000", "5000", "8000", "5000", "10000", "10000", "1000", "0", "1000", "0", "0", "10000","65000" };

            //for (int i = 0; i < s1.Length; i++)
            //{
            //    DataRow row = dt1.NewRow();

            //    row["APDESC1"] = s1[i];
            //    row["CONTENT"] = s2[i];
            //    row["ASRCODEAMT"] = s3[i];

            //    dt1.Rows.Add(row);
            //}

            //DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            //DataTable Rptdt = new DataTable();

            //ActiveReport rpt = new TYATKB006R1(dt1);
            //rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;
            //(new TYERGB001P(rpt, Rptdt)).ShowDialog();
            
        }
        #endregion
        
        #region Description : 그리드 더블클릭
        private void FPS91_TY_S_HR_88GGS586_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.OpenModalPopup(new TYATKB002I(this.FPS91_TY_S_HR_88GGS586.GetValue("APTHOSU").ToString(),
                                                   this.FPS91_TY_S_HR_88GGS586.GetValue("APIDATE").ToString().Replace("-",""))) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회구분 선택 이벤트
        private void CBO01_ATGUBN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CBO01_ATGUBN.GetValue().ToString() == "1")
            {
                //현재
                this.DTP01_STDATE.SetReadOnly(true);
                this.DTP01_EDDATE.SetReadOnly(true);
            }
            else
            {
                //전체
                this.DTP01_STDATE.SetReadOnly(false);
                this.DTP01_EDDATE.SetReadOnly(false);
            }
        }
        #endregion
    }
}
