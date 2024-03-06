using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.BS00
{
    /// <summary>
    /// 영업계획(매출액,취급량) 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.08.08 10:04
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_788A4373 : 사업계획-영업계획(매출액,취급량) 자료 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_788A2372 : 영업계획(매출액,취급량) 자료 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  INQOPTION : 조회구분
    ///  INQOPTION2 : 조회구분
    ///  BSYEAR : 년도
    /// </summary>
    public partial class TYBSUP002S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYBSUP002S()
        {
            InitializeComponent();
        }

        private void TYBSUP002S_Load(object sender, System.EventArgs e)
        {
            this.TXT01_BSYEAR.SetValue(DateTime.Now.AddYears(1).ToString("yyyy"));

            this.SetStartingFocus(this.TXT01_BSYEAR);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_788A2372.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_788A4373", CBO01_INQOPTION2.GetValue().ToString(), TXT01_BSYEAR.GetValue().ToString(), CBO01_INQOPTION.GetValue().ToString() == "A" ? "" : CBO01_INQOPTION.GetValue().ToString());
            this.FPS91_TY_S_AC_788A2372.SetValue(UP_Set_SumRowAdd(this.DbConnector.ExecuteDataTable()));

            if (this.FPS91_TY_S_AC_788A2372.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_AC_788A2372.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_788A2372.GetValue(i, "BSHMCDNM").ToString() == "[ 소 계 ]")
                    {
                        this.FPS91_TY_S_AC_788A2372.ActiveSheet.Rows[i].BackColor = Color.FromArgb(228, 242, 194);
                    }
                    else if (this.FPS91_TY_S_AC_788A2372.GetValue(i, "BSVNCDNM").ToString() == "[ 합 계 ]")
                    {
                        this.FPS91_TY_S_AC_788A2372.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    }
                }
            }
        }
        #endregion

        #region Description : 소계,합계 Row 추가 함수
        private DataTable UP_Set_SumRowAdd(DataTable dt)
        {
            int i = 0;

            string sFilter = string.Empty;

            double dBSMONAMT01 = 0;
            double dBSMONAMT02 = 0;
            double dBSMONAMT03 = 0;
            double dBSMONAMT04 = 0;
            double dBSMONAMT05 = 0;
            double dBSMONAMT06 = 0;
            double dBSMONAMT07 = 0;
            double dBSMONAMT08 = 0;
            double dBSMONAMT09 = 0;
            double dBSMONAMT10 = 0;
            double dBSMONAMT11 = 0;
            double dBSMONAMT12 = 0;
            double dBSMONTOTAL = 0;

            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;

            for (i = 1; i < nNum; i++)
            {
                if (table.Rows[i - 1]["BSDPAC"].ToString() != table.Rows[i]["BSDPAC"].ToString()  )
                {
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    // 소 계 이름 넣기
                    table.Rows[i]["BSHMCDNM"] = "[ 소 계 ]";

                    //  귀속부서
                    sFilter = "  BSDPAC  = '" + table.Rows[i - 1]["BSDPAC"].ToString() + "'";
                                        
                    table.Rows[i]["BSMONAMT01"] = table.Compute("SUM(BSMONAMT01)", sFilter).ToString();
                    table.Rows[i]["BSMONAMT02"] = table.Compute("SUM(BSMONAMT02)", sFilter).ToString();
                    table.Rows[i]["BSMONAMT03"] = table.Compute("SUM(BSMONAMT03)", sFilter).ToString();
                    table.Rows[i]["BSMONAMT04"] = table.Compute("SUM(BSMONAMT04)", sFilter).ToString();
                    table.Rows[i]["BSMONAMT05"] = table.Compute("SUM(BSMONAMT05)", sFilter).ToString();
                    table.Rows[i]["BSMONAMT06"] = table.Compute("SUM(BSMONAMT06)", sFilter).ToString();
                    table.Rows[i]["BSMONAMT07"] = table.Compute("SUM(BSMONAMT07)", sFilter).ToString();
                    table.Rows[i]["BSMONAMT08"] = table.Compute("SUM(BSMONAMT08)", sFilter).ToString();
                    table.Rows[i]["BSMONAMT09"] = table.Compute("SUM(BSMONAMT09)", sFilter).ToString();
                    table.Rows[i]["BSMONAMT10"] = table.Compute("SUM(BSMONAMT10)", sFilter).ToString();
                    table.Rows[i]["BSMONAMT11"] = table.Compute("SUM(BSMONAMT11)", sFilter).ToString();
                    table.Rows[i]["BSMONAMT12"] = table.Compute("SUM(BSMONAMT12)", sFilter).ToString();
                    table.Rows[i]["BSMONTOTAL"] = table.Compute("SUM(BSMONTOTAL)", sFilter).ToString();

                    dBSMONAMT01 = dBSMONAMT01 + Convert.ToDouble(table.Rows[i]["BSMONAMT01"].ToString());
                    dBSMONAMT02 = dBSMONAMT02 + Convert.ToDouble(table.Rows[i]["BSMONAMT02"].ToString());
                    dBSMONAMT03 = dBSMONAMT03 + Convert.ToDouble(table.Rows[i]["BSMONAMT03"].ToString());
                    dBSMONAMT04 = dBSMONAMT04 + Convert.ToDouble(table.Rows[i]["BSMONAMT04"].ToString());
                    dBSMONAMT05 = dBSMONAMT05 + Convert.ToDouble(table.Rows[i]["BSMONAMT05"].ToString());
                    dBSMONAMT06 = dBSMONAMT06 + Convert.ToDouble(table.Rows[i]["BSMONAMT06"].ToString());
                    dBSMONAMT07 = dBSMONAMT07 + Convert.ToDouble(table.Rows[i]["BSMONAMT07"].ToString());
                    dBSMONAMT08 = dBSMONAMT08 + Convert.ToDouble(table.Rows[i]["BSMONAMT08"].ToString());
                    dBSMONAMT09 = dBSMONAMT09 + Convert.ToDouble(table.Rows[i]["BSMONAMT09"].ToString());
                    dBSMONAMT10 = dBSMONAMT10 + Convert.ToDouble(table.Rows[i]["BSMONAMT10"].ToString());
                    dBSMONAMT11 = dBSMONAMT11 + Convert.ToDouble(table.Rows[i]["BSMONAMT11"].ToString());
                    dBSMONAMT12 = dBSMONAMT12 + Convert.ToDouble(table.Rows[i]["BSMONAMT12"].ToString());
                    dBSMONTOTAL = dBSMONTOTAL + Convert.ToDouble(table.Rows[i]["BSMONTOTAL"].ToString());

                    nNum = nNum + 1;

                    i = i + 1;
                }
            }

            if (nNum > 0)
            {
                /******* 마지막 거래처의 대한 로우 생성*****/
                row = table.NewRow();
                table.Rows.InsertAt(row, i);

                table.Rows[i]["BSHMCDNM"] = "[ 소 계 ]";

                //  귀속부서
                sFilter = "  BSDPAC  = '" + table.Rows[i - 1]["BSDPAC"].ToString() + "'";

                table.Rows[i]["BSMONAMT01"] = table.Compute("SUM(BSMONAMT01)", sFilter).ToString();
                table.Rows[i]["BSMONAMT02"] = table.Compute("SUM(BSMONAMT02)", sFilter).ToString();
                table.Rows[i]["BSMONAMT03"] = table.Compute("SUM(BSMONAMT03)", sFilter).ToString();
                table.Rows[i]["BSMONAMT04"] = table.Compute("SUM(BSMONAMT04)", sFilter).ToString();
                table.Rows[i]["BSMONAMT05"] = table.Compute("SUM(BSMONAMT05)", sFilter).ToString();
                table.Rows[i]["BSMONAMT06"] = table.Compute("SUM(BSMONAMT06)", sFilter).ToString();
                table.Rows[i]["BSMONAMT07"] = table.Compute("SUM(BSMONAMT07)", sFilter).ToString();
                table.Rows[i]["BSMONAMT08"] = table.Compute("SUM(BSMONAMT08)", sFilter).ToString();
                table.Rows[i]["BSMONAMT09"] = table.Compute("SUM(BSMONAMT09)", sFilter).ToString();
                table.Rows[i]["BSMONAMT10"] = table.Compute("SUM(BSMONAMT10)", sFilter).ToString();
                table.Rows[i]["BSMONAMT11"] = table.Compute("SUM(BSMONAMT11)", sFilter).ToString();
                table.Rows[i]["BSMONAMT12"] = table.Compute("SUM(BSMONAMT12)", sFilter).ToString();
                table.Rows[i]["BSMONTOTAL"] = table.Compute("SUM(BSMONTOTAL)", sFilter).ToString();

                dBSMONAMT01 = dBSMONAMT01 + Convert.ToDouble(table.Rows[i]["BSMONAMT01"].ToString());
                dBSMONAMT02 = dBSMONAMT02 + Convert.ToDouble(table.Rows[i]["BSMONAMT02"].ToString());
                dBSMONAMT03 = dBSMONAMT03 + Convert.ToDouble(table.Rows[i]["BSMONAMT03"].ToString());
                dBSMONAMT04 = dBSMONAMT04 + Convert.ToDouble(table.Rows[i]["BSMONAMT04"].ToString());
                dBSMONAMT05 = dBSMONAMT05 + Convert.ToDouble(table.Rows[i]["BSMONAMT05"].ToString());
                dBSMONAMT06 = dBSMONAMT06 + Convert.ToDouble(table.Rows[i]["BSMONAMT06"].ToString());
                dBSMONAMT07 = dBSMONAMT07 + Convert.ToDouble(table.Rows[i]["BSMONAMT07"].ToString());
                dBSMONAMT08 = dBSMONAMT08 + Convert.ToDouble(table.Rows[i]["BSMONAMT08"].ToString());
                dBSMONAMT09 = dBSMONAMT09 + Convert.ToDouble(table.Rows[i]["BSMONAMT09"].ToString());
                dBSMONAMT10 = dBSMONAMT10 + Convert.ToDouble(table.Rows[i]["BSMONAMT10"].ToString());
                dBSMONAMT11 = dBSMONAMT11 + Convert.ToDouble(table.Rows[i]["BSMONAMT11"].ToString());
                dBSMONAMT12 = dBSMONAMT12 + Convert.ToDouble(table.Rows[i]["BSMONAMT12"].ToString());
                dBSMONTOTAL = dBSMONTOTAL + Convert.ToDouble(table.Rows[i]["BSMONTOTAL"].ToString());

                /******** 총계를 위한 Row 생성 **************/
                row = table.NewRow();
                table.Rows.InsertAt(row, i + 1);

                // 합 계 이름 넣기
                table.Rows[i + 1]["BSVNCDNM"] = "[ 합 계 ]";

                table.Rows[i + 1]["BSMONAMT01"] = string.Format("{0:#,##0}", dBSMONAMT01);
                table.Rows[i + 1]["BSMONAMT02"] = string.Format("{0:#,##0}", dBSMONAMT02);
                table.Rows[i + 1]["BSMONAMT03"] = string.Format("{0:#,##0}", dBSMONAMT03);
                table.Rows[i + 1]["BSMONAMT04"] = string.Format("{0:#,##0}", dBSMONAMT04);
                table.Rows[i + 1]["BSMONAMT05"] = string.Format("{0:#,##0}", dBSMONAMT05);
                table.Rows[i + 1]["BSMONAMT06"] = string.Format("{0:#,##0}", dBSMONAMT06);
                table.Rows[i + 1]["BSMONAMT07"] = string.Format("{0:#,##0}", dBSMONAMT07);
                table.Rows[i + 1]["BSMONAMT08"] = string.Format("{0:#,##0}", dBSMONAMT08);
                table.Rows[i + 1]["BSMONAMT09"] = string.Format("{0:#,##0}", dBSMONAMT09);
                table.Rows[i + 1]["BSMONAMT10"] = string.Format("{0:#,##0}", dBSMONAMT10);
                table.Rows[i + 1]["BSMONAMT11"] = string.Format("{0:#,##0}", dBSMONAMT11);
                table.Rows[i + 1]["BSMONAMT12"] = string.Format("{0:#,##0}", dBSMONAMT12);
                table.Rows[i + 1]["BSMONTOTAL"] = string.Format("{0:#,##0}", dBSMONTOTAL);

            }

            return table;
        }
        #endregion   

    }
}
