using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 개인국민연금총계 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2015.01.02 09:37
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_5129P029 : 개인국민연금총계 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_5129P031 : 개인국민연금총계 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  KBSABUN : 사번
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYHRPY013S : TYBase
    {
        #region Description : 페이지 로드
        public TYHRPY013S()
        {
            InitializeComponent();
        }

        private void TYHRPY013S_Load(object sender, System.EventArgs e)
        {
            BTN61_INQ_Click(null, null);
            SetStartingFocus(DTP01_GSTYYMM);
        }
        #endregion

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_5129P031.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_HR_5129P029",
                this.DTP01_GSTYYMM.GetString().Substring(0, 6),
                this.DTP01_GEDYYMM.GetString().Substring(0, 6),
                this.CBH01_KBSABUN.GetValue().ToString()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_HR_5129P031.SetValue(UP_ChangeDataTable(dt));
            }
            else
            {
                this.FPS91_TY_S_HR_5129P031.SetValue(dt);
            }

            this.SetSpreadSumRow(this.FPS91_TY_S_HR_5129P031, "PNNAME", "소 계", SumRowType.SubTotal);
            this.SetSpreadSumRow(this.FPS91_TY_S_HR_5129P031, "PNSABUN", "합 계", SumRowType.Total);            
        }
        #endregion

        private DataTable UP_ChangeDataTable(DataTable dt)
        {
            DataTable rtnDt = new DataTable();

            rtnDt.Columns.Add("PNCOMPANY", typeof(System.String));
            rtnDt.Columns.Add("PNSABUN", typeof(System.String));
            rtnDt.Columns.Add("PNNAME", typeof(System.String));
            rtnDt.Columns.Add("PNYYMM", typeof(System.String));
            rtnDt.Columns.Add("PNJIKGUB", typeof(System.String));
            rtnDt.Columns.Add("PNDEPT", typeof(System.String));
            rtnDt.Columns.Add("PNYUNDG", typeof(System.String));
            rtnDt.Columns.Add("PNNYUNGUM", typeof(System.String));

            string sPNSABUN = string.Empty;
            double dPNNYUNGUM = 0;
            double dPNNYUNGUMTOTAL = 0;

            DataRow row;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row = rtnDt.NewRow();

                row["PNCOMPANY"] = dt.Rows[i]["PNCOMPANY"].ToString();
                row["PNSABUN"] = dt.Rows[i]["PNSABUN"].ToString();
                row["PNNAME"] = dt.Rows[i]["PNNAME"].ToString();
                row["PNYYMM"] = dt.Rows[i]["PNYYMM"].ToString();
                row["PNJIKGUB"] = dt.Rows[i]["PNJIKGUB"].ToString();
                row["PNDEPT"] = dt.Rows[i]["PNDEPT"].ToString();
                row["PNYUNDG"] = dt.Rows[i]["PNYUNDG"].ToString();
                row["PNNYUNGUM"] = dt.Rows[i]["PNNYUNGUM"].ToString();

                rtnDt.Rows.Add(row);

                dPNNYUNGUM += Convert.ToDouble(dt.Rows[i]["PNNYUNGUM"]);
                dPNNYUNGUMTOTAL += Convert.ToDouble(dt.Rows[i]["PNNYUNGUM"]);

                if (i > 0)
                {
                    if (dt.Rows[i - 1]["PNSABUN"].ToString() != dt.Rows[i]["PNSABUN"].ToString())
                    {
                        row = rtnDt.NewRow();

                        row["PNCOMPANY"] = DBNull.Value;
                        row["PNSABUN"] = DBNull.Value;
                        row["PNNAME"] = "소 계"; 
                        row["PNYYMM"] = DBNull.Value;
                        row["PNJIKGUB"] = DBNull.Value;
                        row["PNDEPT"] = DBNull.Value;
                        row["PNYUNDG"] = DBNull.Value;
                        row["PNNYUNGUM"] = string.Format("{0:#,###}", dPNNYUNGUM.ToString());

                        rtnDt.Rows.Add(row);

                        dPNNYUNGUM = 0;
                    }
                }
            }

            row = rtnDt.NewRow();

            row["PNCOMPANY"] = DBNull.Value; 
            row["PNSABUN"] = "합 계"; 
            row["PNNAME"] = DBNull.Value;
            row["PNYYMM"] = DBNull.Value;
            row["PNJIKGUB"] = DBNull.Value;
            row["PNDEPT"] = DBNull.Value;
            row["PNYUNDG"] = DBNull.Value;
            row["PNNYUNGUM"] = string.Format("{0:#,###}", dPNNYUNGUMTOTAL.ToString());

            rtnDt.Rows.Add(row);

            return rtnDt;
        }
    }
}
