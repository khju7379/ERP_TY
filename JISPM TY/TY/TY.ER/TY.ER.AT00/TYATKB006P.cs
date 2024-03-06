using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.AT00
{
    /// <summary>
    /// 세대별 고지서 발행 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2018.10.04 13:53
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_8AAI4899 : 세대별 고지서 마스타 출력
    ///  TY_P_HR_8AAIA900 : 세대별 고지서 내역 출력
    ///  TY_P_HR_8AAIA901 : 세대별 고지서 공지사항 출력
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  AMRYYMM : 작업년월
    ///  AMRHOSU : 호 수
    /// </summary>
    public partial class TYATKB006P : TYBase
    {
        #region Description : 폼 로드
        public TYATKB006P()
        {
            InitializeComponent();
        }

        private void TYATKB006P_Load(object sender, System.EventArgs e)
        {
            this.DTP01_AMRYYMM.SetValue(System.DateTime.Now.ToString("yyyy-MM"));

            SetStartingFocus(this.DTP01_AMRYYMM);
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            try
            {
                // 마스타 조회
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_HR_8AAI4899", this.DTP01_AMRYYMM.GetString().Substring(0, 6),
                                                            this.TXT01_AMRHOSU.GetValue().ToString());

                DataTable dtMst = this.DbConnector.ExecuteDataTable();

                DataTable[] dtDet = new DataTable[dtMst.Rows.Count];

                // 내역 조회
                for (int i = 0; i < dtMst.Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_HR_8AAIA900", dtMst.Rows[i]["AMRYYMM"].ToString(),
                                                                dtMst.Rows[i]["AMRHOSU"].ToString());

                    dtDet[i] = this.DbConnector.ExecuteDataTable();
                }
                // 공지사항 조회
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_HR_8AAIA901", this.DTP01_AMRYYMM.GetString().Substring(0, 6));

                DataTable dtOre = this.DbConnector.ExecuteDataTable();

                SectionReport rpt = new TYATKB006R1(dtDet, dtOre);
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                (new TYERGB001P(rpt, dtMst)).ShowDialog();
            }
            catch
            {
            }
        }
        #endregion
    }
}
