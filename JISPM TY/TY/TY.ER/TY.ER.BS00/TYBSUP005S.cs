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
    public partial class TYBSUP005S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYBSUP005S()
        {
            InitializeComponent();
        }

        private void TYBSUP005S_Load(object sender, System.EventArgs e)
        {
            this.TXT01_BVYEAR.SetValue(DateTime.Now.AddYears(1).ToString("yyyy"));

            CBH01_BVDPMK.DummyValue = this.TXT01_BVYEAR.GetValue().ToString() + "0101";
            CBH01_BVDPAC.DummyValue = this.TXT01_BVYEAR.GetValue().ToString() + "0101";

            this.SetStartingFocus(this.TXT01_BVYEAR);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_78A9B409.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_78A92408", CBO01_INQOPTION2.GetValue().ToString(), TXT01_BVYEAR.GetValue().ToString(), CBH01_BVDPMK.GetValue(), CBH01_BVDPAC.GetValue());
            this.FPS91_TY_S_AC_78A9B409.SetValue(UP_Set_SumRowAdd(this.DbConnector.ExecuteDataTable()));

            if (this.FPS91_TY_S_AC_78A9B409.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_AC_78A9B409.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_78A9B409.GetValue(i, "BVASETGNNM").ToString() == "[ 소 계 ]")
                    {
                        this.FPS91_TY_S_AC_78A9B409.ActiveSheet.Rows[i].BackColor = Color.FromArgb(228, 242, 194);
                    }
                    if (this.FPS91_TY_S_AC_78A9B409.GetValue(i, "BVDPACNM").ToString() == "[ 합 계 ]")
                    {
                        this.FPS91_TY_S_AC_78A9B409.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
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

            double dBVMONAMT01 = 0;
            double dBVMONAMT02 = 0;
            double dBVMONAMT03 = 0;
            double dBVMONAMT04 = 0;
            double dBVMONAMT05 = 0;
            double dBVMONAMT06 = 0;
            double dBVMONAMT07 = 0;
            double dBVMONAMT08 = 0;
            double dBVMONAMT09 = 0;
            double dBVMONAMT10 = 0;
            double dBVMONAMT11 = 0;
            double dBVMONAMT12 = 0;
            double dBVMONTOTAL = 0;

            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;

            for (i = 1; i < nNum; i++)
            {

                if (table.Rows[i - 1]["BVASETGN"].ToString() != table.Rows[i]["BVASETGN"].ToString() )                    
                {
                        row = table.NewRow();
                        table.Rows.InsertAt(row, i);

                        // 소 계 이름 넣기
                        table.Rows[i]["BVASETGNNM"] = "[ 소 계 ]";

                        // 투자구분
                        sFilter = "  BVASETGN  = '" + table.Rows[i - 1]["BVASETGN"].ToString() + "' ";
                        
                    

                        table.Rows[i]["BVMONAMT01"] = table.Compute("SUM(BVMONAMT01)", sFilter).ToString();
                        table.Rows[i]["BVMONAMT02"] = table.Compute("SUM(BVMONAMT02)", sFilter).ToString();
                        table.Rows[i]["BVMONAMT03"] = table.Compute("SUM(BVMONAMT03)", sFilter).ToString();
                        table.Rows[i]["BVMONAMT04"] = table.Compute("SUM(BVMONAMT04)", sFilter).ToString();
                        table.Rows[i]["BVMONAMT05"] = table.Compute("SUM(BVMONAMT05)", sFilter).ToString();
                        table.Rows[i]["BVMONAMT06"] = table.Compute("SUM(BVMONAMT06)", sFilter).ToString();
                        table.Rows[i]["BVMONAMT07"] = table.Compute("SUM(BVMONAMT07)", sFilter).ToString();
                        table.Rows[i]["BVMONAMT08"] = table.Compute("SUM(BVMONAMT08)", sFilter).ToString();
                        table.Rows[i]["BVMONAMT09"] = table.Compute("SUM(BVMONAMT09)", sFilter).ToString();
                        table.Rows[i]["BVMONAMT10"] = table.Compute("SUM(BVMONAMT10)", sFilter).ToString();
                        table.Rows[i]["BVMONAMT11"] = table.Compute("SUM(BVMONAMT11)", sFilter).ToString();
                        table.Rows[i]["BVMONAMT12"] = table.Compute("SUM(BVMONAMT12)", sFilter).ToString();
                        table.Rows[i]["BVMONTOTAL"] = table.Compute("SUM(BVMONTOTAL)", sFilter).ToString();


                        dBVMONAMT01 = dBVMONAMT01 + Convert.ToDouble(dt.Rows[i]["BVMONAMT01"].ToString());
                        dBVMONAMT02 = dBVMONAMT02 + Convert.ToDouble(dt.Rows[i]["BVMONAMT02"].ToString());
                        dBVMONAMT03 = dBVMONAMT03 + Convert.ToDouble(dt.Rows[i]["BVMONAMT03"].ToString());
                        dBVMONAMT04 = dBVMONAMT04 + Convert.ToDouble(dt.Rows[i]["BVMONAMT04"].ToString());
                        dBVMONAMT05 = dBVMONAMT05 + Convert.ToDouble(dt.Rows[i]["BVMONAMT05"].ToString());
                        dBVMONAMT06 = dBVMONAMT06 + Convert.ToDouble(dt.Rows[i]["BVMONAMT06"].ToString());
                        dBVMONAMT07 = dBVMONAMT07 + Convert.ToDouble(dt.Rows[i]["BVMONAMT07"].ToString());
                        dBVMONAMT08 = dBVMONAMT08 + Convert.ToDouble(dt.Rows[i]["BVMONAMT08"].ToString());
                        dBVMONAMT09 = dBVMONAMT09 + Convert.ToDouble(dt.Rows[i]["BVMONAMT09"].ToString());
                        dBVMONAMT10 = dBVMONAMT10 + Convert.ToDouble(dt.Rows[i]["BVMONAMT10"].ToString());
                        dBVMONAMT11 = dBVMONAMT11 + Convert.ToDouble(dt.Rows[i]["BVMONAMT11"].ToString());
                        dBVMONAMT12 = dBVMONAMT12 + Convert.ToDouble(dt.Rows[i]["BVMONAMT12"].ToString());
                        dBVMONTOTAL = dBVMONTOTAL + Convert.ToDouble(dt.Rows[i]["BVMONTOTAL"].ToString());
                    
                    nNum = nNum + 1;

                    i = i + 1;
                }
            }

            if (nNum > 0)
            {
                /******* 마지막 거래처의 대한 로우 생성*****/
                row = table.NewRow();
                table.Rows.InsertAt(row, i);

                table.Rows[i]["BVASETGNNM"] = "[ 소 계 ]";

                //  투자구분
                sFilter = "  BVASETGN  = '" + table.Rows[i - 1]["BVASETGN"].ToString() + "' ";
                

                table.Rows[i]["BVMONAMT01"] = table.Compute("SUM(BVMONAMT01)", sFilter).ToString();
                table.Rows[i]["BVMONAMT02"] = table.Compute("SUM(BVMONAMT02)", sFilter).ToString();
                table.Rows[i]["BVMONAMT03"] = table.Compute("SUM(BVMONAMT03)", sFilter).ToString();
                table.Rows[i]["BVMONAMT04"] = table.Compute("SUM(BVMONAMT04)", sFilter).ToString();
                table.Rows[i]["BVMONAMT05"] = table.Compute("SUM(BVMONAMT05)", sFilter).ToString();
                table.Rows[i]["BVMONAMT06"] = table.Compute("SUM(BVMONAMT06)", sFilter).ToString();
                table.Rows[i]["BVMONAMT07"] = table.Compute("SUM(BVMONAMT07)", sFilter).ToString();
                table.Rows[i]["BVMONAMT08"] = table.Compute("SUM(BVMONAMT08)", sFilter).ToString();
                table.Rows[i]["BVMONAMT09"] = table.Compute("SUM(BVMONAMT09)", sFilter).ToString();
                table.Rows[i]["BVMONAMT10"] = table.Compute("SUM(BVMONAMT10)", sFilter).ToString();
                table.Rows[i]["BVMONAMT11"] = table.Compute("SUM(BVMONAMT11)", sFilter).ToString();
                table.Rows[i]["BVMONAMT12"] = table.Compute("SUM(BVMONAMT12)", sFilter).ToString();
                table.Rows[i]["BVMONTOTAL"] = table.Compute("SUM(BVMONTOTAL)", sFilter).ToString();


                dBVMONAMT01 = dBVMONAMT01 + Convert.ToDouble(dt.Rows[i]["BVMONAMT01"].ToString());
                dBVMONAMT02 = dBVMONAMT02 + Convert.ToDouble(dt.Rows[i]["BVMONAMT02"].ToString());
                dBVMONAMT03 = dBVMONAMT03 + Convert.ToDouble(dt.Rows[i]["BVMONAMT03"].ToString());
                dBVMONAMT04 = dBVMONAMT04 + Convert.ToDouble(dt.Rows[i]["BVMONAMT04"].ToString());
                dBVMONAMT05 = dBVMONAMT05 + Convert.ToDouble(dt.Rows[i]["BVMONAMT05"].ToString());
                dBVMONAMT06 = dBVMONAMT06 + Convert.ToDouble(dt.Rows[i]["BVMONAMT06"].ToString());
                dBVMONAMT07 = dBVMONAMT07 + Convert.ToDouble(dt.Rows[i]["BVMONAMT07"].ToString());
                dBVMONAMT08 = dBVMONAMT08 + Convert.ToDouble(dt.Rows[i]["BVMONAMT08"].ToString());
                dBVMONAMT09 = dBVMONAMT09 + Convert.ToDouble(dt.Rows[i]["BVMONAMT09"].ToString());
                dBVMONAMT10 = dBVMONAMT10 + Convert.ToDouble(dt.Rows[i]["BVMONAMT10"].ToString());
                dBVMONAMT11 = dBVMONAMT11 + Convert.ToDouble(dt.Rows[i]["BVMONAMT11"].ToString());
                dBVMONAMT12 = dBVMONAMT12 + Convert.ToDouble(dt.Rows[i]["BVMONAMT12"].ToString());
                dBVMONTOTAL = dBVMONTOTAL + Convert.ToDouble(dt.Rows[i]["BVMONTOTAL"].ToString());

                /******** 총계를 위한 Row 생성 **************/
                row = table.NewRow();
                table.Rows.InsertAt(row, i + 1);

                // 합 계 이름 넣기
                table.Rows[i + 1]["BVDPACNM"] = "[ 합 계 ]";

                table.Rows[i + 1]["BVMONAMT01"] = string.Format("{0:#,##0}", dBVMONAMT01);
                table.Rows[i + 1]["BVMONAMT02"] = string.Format("{0:#,##0}", dBVMONAMT02);
                table.Rows[i + 1]["BVMONAMT03"] = string.Format("{0:#,##0}", dBVMONAMT03);
                table.Rows[i + 1]["BVMONAMT04"] = string.Format("{0:#,##0}", dBVMONAMT04);
                table.Rows[i + 1]["BVMONAMT05"] = string.Format("{0:#,##0}", dBVMONAMT05);
                table.Rows[i + 1]["BVMONAMT06"] = string.Format("{0:#,##0}", dBVMONAMT06);
                table.Rows[i + 1]["BVMONAMT07"] = string.Format("{0:#,##0}", dBVMONAMT07);
                table.Rows[i + 1]["BVMONAMT08"] = string.Format("{0:#,##0}", dBVMONAMT08);
                table.Rows[i + 1]["BVMONAMT09"] = string.Format("{0:#,##0}", dBVMONAMT09);
                table.Rows[i + 1]["BVMONAMT10"] = string.Format("{0:#,##0}", dBVMONAMT10);
                table.Rows[i + 1]["BVMONAMT11"] = string.Format("{0:#,##0}", dBVMONAMT11);
                table.Rows[i + 1]["BVMONAMT12"] = string.Format("{0:#,##0}", dBVMONAMT12);
                table.Rows[i + 1]["BVMONTOTAL"] = string.Format("{0:#,##0}", dBVMONTOTAL);

            }

            return table;

            return table;
        }
        #endregion   

    }
}
