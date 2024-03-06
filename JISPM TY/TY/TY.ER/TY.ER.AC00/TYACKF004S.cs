using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 자금계획 조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.12.27 14:07
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2CR5J383 : 자금계획 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2CSA2412 : 자금계획 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  A3CDFD : 자금항목코드
    ///  GCDAC : 계정코드
    ///  DPAC : 귀속부서
    ///  F3CDFD : 자금항목코드
    ///  GPRTGN : 출력구분
    ///  TWGUBN : 외화구분
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    /// </summary>
    public partial class TYACKF004S : TYBase
    {
        #region Description : 페이지 로드
        public TYACKF004S()
        {
            InitializeComponent();
        }

        private void TYACKF004S_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.DTP01_GSTDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_3139H459",
                this.DTP01_GSTDATE.GetString(),
                TYUserInfo.EmpNo
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["KBJKCD"].ToString() != "01")
                {
                    if (dt.Rows[0]["KBBUSEO"].ToString().Substring(0, 1) != "A")
                    {
                        if (dt.Rows[0]["KBBUSEO"].ToString().Substring(0, 1) == "A" || dt.Rows[0]["KBBUSEO"].ToString().Substring(0, 1) == "C")
                        {
                            this.CBO01_DPAC.SetValue("A");
                        }
                        else if (dt.Rows[0]["KBBUSEO"].ToString().Substring(0, 1) == "T")
                        {
                            this.CBO01_DPAC.SetValue("T");
                        }
                        else if (dt.Rows[0]["KBBUSEO"].ToString().Substring(0, 1) == "S")
                        {
                            this.CBO01_DPAC.SetValue("S");
                        }
                        else if (dt.Rows[0]["KBBUSEO"].ToString().Substring(0, 4) == "B101")
                        {
                            this.CBO01_DPAC.SetValue("B801");
                        }
                        else if (dt.Rows[0]["KBBUSEO"].ToString().Substring(0, 4) == "B102")
                        {
                            this.CBO01_DPAC.SetValue("B802");
                        }
                        else if (dt.Rows[0]["KBBUSEO"].ToString().Substring(0, 4) == "B201")
                        {
                            this.CBO01_DPAC.SetValue("B803");
                        }
                    }
                }
            }


            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_2CR5J383",
                this.DTP01_GSTDATE.GetValue(),
                this.DTP01_GEDDATE.GetValue(),
                this.CBO01_DPAC.GetValue().ToString(),
                this.CBH01_GCDAC.GetValue(),
                this.CBH01_A3CDFD.GetValue(),
                this.CBO01_F3CDFD.GetValue().ToString(),
                this.CBO01_TWGUBN.GetValue().ToString(),
                "1",
                this.CBO01_GPRTGN.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_2CSA2412.SetValue(UP_AddSumRow(dt));

                this.SetSpreadSumRow(this.FPS91_TY_S_AC_2CSA2412, "DTCDDESC1", "[합     계]", SumRowType.Total);
            }
            else
            {
                this.FPS91_TY_S_AC_2CSA2412.SetValue(dt);
            }
        }
        #endregion

        #region Description : UP_AddSum()
        public DataTable UP_AddSumRow(DataTable dt)
        {
            // 합계를 보여주기 위한 빈 로우 하나 생성
            DataTable table = new DataTable();
            table = dt;

            DataRow row;

            string sPHINAMT = table.Compute("Sum(PHINAMT)", "").ToString();   //원화(수입)
            string sPHOUTAMT = table.Compute("Sum(PHOUTAMT)", "").ToString(); //원화(지출)

            string sPHAIAMT_IN = table.Compute("Sum(PHAIAMT_IN)", "").ToString();   //외화(수입)
            string sPHAIAMT_OUT = table.Compute("Sum(PHAIAMT_OUT)", "").ToString(); //외화(지출)

            string s과부족원화 = Convert.ToString(double.Parse(sPHINAMT) - double.Parse(sPHOUTAMT));
            string s과부족외화 = Convert.ToString(double.Parse(sPHAIAMT_IN) - double.Parse(sPHAIAMT_OUT));

            string sFilter = string.Empty;
            string s일수입원화 = string.Empty;
            string s일지출원화 = string.Empty;
            string s일수입외화 = string.Empty;
            string s일지출외화 = string.Empty;

            string s일과부족원화 = string.Empty;
            string s일과부족외화 = string.Empty;

            try
            {

                for (int i = 1; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i]["PHIPDATE"].ToString() != table.Rows[i - 1]["PHIPDATE"].ToString())
                    {

                        sFilter = " PHIPDATE = '" + table.Rows[i - 1]["PHIPDATE"].ToString() + "'";

                        s일수입원화 = Convert.ToString(Get_Numeric(table.Compute("SUM(PHINAMT)", sFilter).ToString()));
                        s일지출원화 = Convert.ToString(Get_Numeric(table.Compute("SUM(PHOUTAMT)", sFilter).ToString()));
                        s일수입외화 = Convert.ToString(Get_Numeric(table.Compute("SUM(PHAIAMT_IN)", sFilter).ToString()));
                        s일지출외화 = Convert.ToString(Get_Numeric(table.Compute("SUM(PHAIAMT_OUT)", sFilter).ToString()));

                        s일과부족원화 = Convert.ToString(Convert.ToDouble(Get_Numeric(s일과부족원화)) +
                                        Convert.ToDouble(Get_Numeric(s일수입원화)) -
                                        Convert.ToDouble(Get_Numeric(s일지출원화)));

                        s일과부족외화 = Convert.ToString(Convert.ToDouble(Get_Numeric(s일과부족외화)) +
                                        Convert.ToDouble(Get_Numeric(s일수입외화)) -
                                        Convert.ToDouble(Get_Numeric(s일지출외화)));

                        table.Rows[i - 1]["POVERAMT"] = String.Format("{0,9:N0}", double.Parse(SetDefaultValue(s일과부족원화.ToString()))).Trim();

                    }
                }

                // 마지막 일자 계산
                int nNum = table.Rows.Count;

                sFilter = " PHIPDATE = '" + table.Rows[nNum - 1]["PHIPDATE"].ToString() + "'";

                s일수입원화 = Convert.ToString(Get_Numeric(table.Compute("SUM(PHINAMT)", sFilter).ToString()));
                s일지출원화 = Convert.ToString(Get_Numeric(table.Compute("SUM(PHOUTAMT)", sFilter).ToString()));
                s일수입외화 = Convert.ToString(Get_Numeric(table.Compute("SUM(PHAIAMT_IN)", sFilter).ToString()));
                s일지출외화 = Convert.ToString(Get_Numeric(table.Compute("SUM(PHAIAMT_OUT)", sFilter).ToString()));

                s일과부족원화 = Convert.ToString(Convert.ToDouble(Get_Numeric(s일과부족원화)) +
                                                  Convert.ToDouble(Get_Numeric(s일수입원화)) -
                                                  Convert.ToDouble(Get_Numeric(s일지출원화)));

                s일과부족외화 = Convert.ToString(Convert.ToDouble(Get_Numeric(s일과부족외화)) +
                                                  Convert.ToDouble(Get_Numeric(s일수입외화)) -
                                                  Convert.ToDouble(Get_Numeric(s일지출외화)));

                table.Rows[nNum - 1]["POVERAMT"] = String.Format("{0,9:N0}", double.Parse(SetDefaultValue(s일과부족원화.ToString()))).Trim();

                row = table.NewRow();
                table.Rows.InsertAt(row, nNum);

                table.Rows[nNum]["DTCDDESC1"] = "[합     계]";

                table.Rows[nNum]["PHINAMT"]     = double.Parse(SetDefaultValue(sPHINAMT.ToString()));
                table.Rows[nNum]["PHOUTAMT"]    = String.Format("{0,9:N0}", double.Parse(SetDefaultValue(sPHOUTAMT.ToString()))).Trim();
                table.Rows[nNum]["POVERAMT"]    = String.Format("{0,9:N0}", double.Parse(sPHINAMT) - double.Parse(sPHOUTAMT)).Trim();
                table.Rows[nNum]["PHAIAMT_IN"]  = String.Format("{0,9:N2}", double.Parse(SetDefaultValue(sPHAIAMT_IN.ToString()))).Trim();
                table.Rows[nNum]["PHAIAMT_OUT"] = String.Format("{0,9:N2}", double.Parse(SetDefaultValue(sPHAIAMT_OUT.ToString()))).Trim();
                table.Rows[nNum]["PHRKAC"]      = String.Format("{0,9:N2}", double.Parse(sPHAIAMT_IN) - double.Parse(sPHAIAMT_OUT)).Trim();
            }
            catch (Exception e)
            {
                string sMessage = string.Empty;

                sMessage = e.ToString();
            }

            return table;
        }
        #endregion
    }
}
