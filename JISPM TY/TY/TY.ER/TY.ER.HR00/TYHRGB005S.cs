using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.Service.Library.Controls.TYSpreadCellType;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using GrapeCity.ActiveReports;
using TY.ER.GB00;

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
    ///  TY_S_HR_85BC1011 : 용역직 인사기본사항 조회
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
    public partial class TYHRGB005S : TYBase
    {
        #region Description : 폼 로드
        public TYHRGB005S()
        {
            InitializeComponent();
        }

        private void TYHRGB005S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyyMMdd"));

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_85BC1011.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_85BC0010", Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                                                        Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                                                        this.CBO01_CIGUBUN.GetValue().ToString(),
                                                        this.CBH01_CIVEND.GetValue().ToString(),
                                                        this.TXT01_CIWORK.GetValue().ToString()
                                                        );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_HR_85BC1011.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_HR_85BC1011.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_HR_85BC1011.GetValue(i, "VNSANGHO").ToString() == "")
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_2445D438", this.FPS91_TY_S_HR_85BC1011.GetValue(i, "CIVEND").ToString()
                                                                    );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            this.FPS91_TY_S_HR_85BC1011.ActiveSheet.Cells[i, 3].Text = dt.Rows[0]["VNSANGHO"].ToString();
                        }
                    }
                }
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_HR_85BC0010", Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                                                        Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                                                        this.CBO01_CIGUBUN.GetValue().ToString(),
                                                        this.CBH01_CIVEND.GetValue().ToString(),
                                                        this.TXT01_CIWORK.GetValue().ToString()
                                                        );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            SectionReport rpt = new TYHRGB005R(this.DTP01_STDATE.GetValue().ToString(), this.DTP01_EDDATE.GetValue().ToString());

            // 가로 출력
            rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

            if (dt.Rows.Count > 0)
            {
                (new TYERGB001P(rpt, UP_DataTableChange(dt))).ShowDialog();
            }
        }
        #endregion

        #region Description : 출력 데이터셋 변경
        private DataTable UP_DataTableChange(DataTable dt)
        {
            DataTable dtRtn = dt;
            DataTable dtVend = new DataTable();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2445D438", this.FPS91_TY_S_HR_85BC1011.GetValue(i, "CIVEND").ToString()
                                                            );

                dtVend = this.DbConnector.ExecuteDataTable();

                if (dtVend.Rows.Count > 0)
                {
                    dtRtn.Rows[i]["VNSANGHO"] = dtVend.Rows[0]["VNSANGHO"].ToString();
                }
            }


            return dtRtn;
        }
        #endregion
    }
}
