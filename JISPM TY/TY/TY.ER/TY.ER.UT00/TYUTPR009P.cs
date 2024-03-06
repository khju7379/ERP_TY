using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.UT00
{
    /// <summary>
    /// 화물 비중 TABLE1 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.04.05 14:50
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_745ET213 : 화물 비중 TABLE1 조회
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PRT : 출력
    /// </summary>
    public partial class TYUTPR009P : TYBase
    {
        #region Descriptino : 폼 로드
        public TYUTPR009P()
        {
            InitializeComponent();
        }

        private void TYUTPR009P_Load(object sender, System.EventArgs e)
        {
        }
        #endregion

        #region Descriptino : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Descriptino : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_745ET213");

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYUTPR009R();
                // 가로 출력
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, QueryDataSetReport(dt))).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Description : 데이터셋 변경
        private DataTable QueryDataSetReport(DataTable dt)
        {
            int iT1GROUP = 0;
            int iCOUNT = 0;
            string sT1SEQ = string.Empty;

            DataTable retDt = new DataTable();

            retDt.Columns.Add("T1GROUP", typeof(System.String));
            retDt.Columns.Add("T1SEQ", typeof(System.String));
            retDt.Columns.Add("T1FAC01", typeof(System.String));
            retDt.Columns.Add("T1FAC02", typeof(System.String));
            retDt.Columns.Add("T1FAC03", typeof(System.String));
            retDt.Columns.Add("T1FAC04", typeof(System.String));
            retDt.Columns.Add("T1FAC05", typeof(System.String));
            retDt.Columns.Add("T1FAC06", typeof(System.String));
            retDt.Columns.Add("T1FAC07", typeof(System.String));
            retDt.Columns.Add("T1FAC08", typeof(System.String));
            retDt.Columns.Add("T1FAC09", typeof(System.String));
            retDt.Columns.Add("T1FAC10", typeof(System.String));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = retDt.NewRow();

                iCOUNT = iCOUNT + 1;
                if (iCOUNT > 15)
                {
                    iCOUNT = 0;
                    iCOUNT = iCOUNT + 1;
                    iT1GROUP = iT1GROUP + 1;
                }

                if (i == 0)
                {
                    iT1GROUP = 1;
                    sT1SEQ = "-25.0";
                }

                row["T1GROUP"] = iT1GROUP;
                row["T1SEQ"] = SetDefaultValue(dt.Rows[i][0].ToString()).Trim();
                row["T1FAC01"] = String.Format("{0:N4}", double.Parse(SetDefaultValue(dt.Rows[i][1].ToString()))).Trim();
                row["T1FAC02"] = String.Format("{0:N4}", double.Parse(SetDefaultValue(dt.Rows[i][2].ToString()))).Trim();
                row["T1FAC03"] = String.Format("{0:N4}", double.Parse(SetDefaultValue(dt.Rows[i][3].ToString()))).Trim();
                row["T1FAC04"] = String.Format("{0:N4}", double.Parse(SetDefaultValue(dt.Rows[i][4].ToString()))).Trim();
                row["T1FAC05"] = String.Format("{0:N4}", double.Parse(SetDefaultValue(dt.Rows[i][5].ToString()))).Trim();
                row["T1FAC06"] = String.Format("{0:N4}", double.Parse(SetDefaultValue(dt.Rows[i][6].ToString()))).Trim();
                row["T1FAC07"] = String.Format("{0:N4}", double.Parse(SetDefaultValue(dt.Rows[i][7].ToString()))).Trim();
                row["T1FAC08"] = String.Format("{0:N4}", double.Parse(SetDefaultValue(dt.Rows[i][8].ToString()))).Trim();
                row["T1FAC09"] = String.Format("{0:N4}", double.Parse(SetDefaultValue(dt.Rows[i][9].ToString()))).Trim();
                row["T1FAC10"] = String.Format("{0:N4}", double.Parse(SetDefaultValue(dt.Rows[i][10].ToString()))).Trim();
                retDt.Rows.Add(row);

                DataRow row1 = retDt.NewRow();
                row1["T1GROUP"] = iT1GROUP;
                row1["T1SEQ"] = Convert.ToString(Double.Parse(sT1SEQ.ToString()) + (i * 0.5));
                row1["T1FAC01"] = String.Format("{0:N4}", double.Parse(SetDefaultValue(dt.Rows[i][11].ToString()))).Trim();
                row1["T1FAC02"] = String.Format("{0:N4}", double.Parse(SetDefaultValue(dt.Rows[i][12].ToString()))).Trim();
                row1["T1FAC03"] = String.Format("{0:N4}", double.Parse(SetDefaultValue(dt.Rows[i][13].ToString()))).Trim();
                row1["T1FAC04"] = String.Format("{0:N4}", double.Parse(SetDefaultValue(dt.Rows[i][14].ToString()))).Trim();
                row1["T1FAC05"] = String.Format("{0:N4}", double.Parse(SetDefaultValue(dt.Rows[i][15].ToString()))).Trim();
                row1["T1FAC06"] = String.Format("{0:N4}", double.Parse(SetDefaultValue(dt.Rows[i][16].ToString()))).Trim();
                row1["T1FAC07"] = String.Format("{0:N4}", double.Parse(SetDefaultValue(dt.Rows[i][17].ToString()))).Trim();
                row1["T1FAC08"] = String.Format("{0:N4}", double.Parse(SetDefaultValue(dt.Rows[i][18].ToString()))).Trim();
                row1["T1FAC09"] = String.Format("{0:N4}", double.Parse(SetDefaultValue(dt.Rows[i][19].ToString()))).Trim();
                row1["T1FAC10"] = String.Format("{0:N4}", double.Parse(SetDefaultValue(dt.Rows[i][20].ToString()))).Trim();
                retDt.Rows.Add(row1);
            }
            return retDt;
        }
        #endregion
    }
}
