using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.AC00
{
    /// <summary>
    /// 수입이자명세서 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.08.23 15:11
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_28N33522 : 수입이자명세서
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_28N3J525 : 수입이자명세서
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    /// </summary>
    public partial class TYACBJ031S : TYBase
    {
        #region Description : 페이지 로드
        public TYACBJ031S()
        {
            InitializeComponent();
        }

        private void TYACBJ031S_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.DTP01_GSTDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sSTDATE = string.Empty;
            string sEDDATE = string.Empty;

            sSTDATE = this.DTP01_GSTDATE.GetValue() + "01";
            sEDDATE = this.DTP01_GEDDATE.GetValue() + "31";

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_28N33522",
                sSTDATE.ToString(),
                sEDDATE.ToString()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_28N3J525.SetValue(UP_ConvertDt(dt));

                // 특정 ROW 색깔 입히기
                for (int i = 0; i < this.FPS91_TY_S_AC_28N3J525.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_28N3J525.GetValue(i, "CDDESC2").ToString() == "합   계")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_28N3J525.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    }
                    else if (this.FPS91_TY_S_AC_28N3J525.GetValue(i, "CDDESC2").ToString() == "총   계")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_28N3J525.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
                    }
                }
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sSTDATE = string.Empty;
            string sEDDATE = string.Empty;

            sSTDATE = this.DTP01_GSTDATE.GetValue() + "01";
            sEDDATE = this.DTP01_GEDDATE.GetValue() + "31";

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_28N33522",
                sSTDATE.ToString(),
                sEDDATE.ToString()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYACBJ031R();

                // 가로 출력
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, UP_ConvertDt(dt))).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        }
        #endregion

        #region Description : 데이터테이블 컨버젼
        private DataTable UP_ConvertDt(DataTable dt)
        {
            string sNEWCDDESC2 = string.Empty;
            string sOLDCDDESC2 = string.Empty;

            DataTable Condt = new DataTable();

            DataRow row;

            Condt.Columns.Add("CDDESC2", typeof(System.String));
            Condt.Columns.Add("CDCODE",  typeof(System.String));
            //Condt.Columns.Add("NUM1",    typeof(System.String));
            Condt.Columns.Add("NUM2",    typeof(System.String));
            Condt.Columns.Add("B4RKAC",  typeof(System.String));
            Condt.Columns.Add("B4CDAC",  typeof(System.String));
            Condt.Columns.Add("A1ABAC",  typeof(System.String));
            Condt.Columns.Add("B4AMCR",  typeof(System.String));
            Condt.Columns.Add("DATE",    typeof(System.String));

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                row = Condt.NewRow();

                sNEWCDDESC2 = dt.Rows[i]["CDDESC2"].ToString();

                if (sOLDCDDESC2 == "")
                {
                    row["CDDESC2"] = dt.Rows[i]["CDDESC2"].ToString();

                    sOLDCDDESC2 = sNEWCDDESC2.ToString();
                }
                else
                {
                    if (sNEWCDDESC2 == sOLDCDDESC2)
                    {
                        row["CDDESC2"] = "";
                        row["CDCODE"]  = "";
                    }
                    else
                    {
                        row["CDDESC2"] = dt.Rows[i]["CDDESC2"].ToString();
                        row["CDCODE"] = dt.Rows[i]["CDCODE"].ToString();

                        sOLDCDDESC2 = sNEWCDDESC2.ToString();
                    }
                }

                //row["NUM1"]   = dt.Rows[i]["NUM1"].ToString();
                row["NUM2"]   = dt.Rows[i]["NUM2"].ToString();
                row["B4RKAC"] = dt.Rows[i]["B4RKAC"].ToString();
                row["A1ABAC"] = dt.Rows[i]["A1ABAC"].ToString();
                row["B4AMCR"] = dt.Rows[i]["B4AMCR"].ToString();
                row["DATE"]   = dt.Rows[i]["DATE"].ToString();

                Condt.Rows.Add(row);
            }

            return Condt;
        }
        #endregion
    }
}