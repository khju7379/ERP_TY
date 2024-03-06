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
    /// 사업계획 항운노조 부대비용 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.09.08 09:29
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_7989J557 : 사업계획 항운노조 부대비용 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_798A1558 : 사업계획 항운노조 부대비용 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_26E2Z874 : 생성하시겠습니까?
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    ///  TY_M_GB_26E31876 : 생성 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  INQ : 조회
    ///  HUYEAR : 년도
    /// </summary>
    public partial class TYBSHU002S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYBSHU002S()
        {
            InitializeComponent();
        }

        private void TYBSHU002S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.TXT01_HUYEAR.SetValue(DateTime.Now.ToString("yyyy"));

            this.CBH01_HUDPAC.DummyValue = this.TXT01_HUYEAR.GetValue().ToString() + "0101";

            this.SetStartingFocus(TXT01_HUYEAR);

        }
        #endregion

        #region  Description : 생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sOUT_MSG = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_797F1554", this.TXT01_HUYEAR.GetValue().ToString(), TYUserInfo.EmpNo, sOUT_MSG);           

            sOUT_MSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUT_MSG.Substring(0, 2).ToString() == "OK")
            {
                this.ShowMessage("TY_M_GB_26E30875");
            }
            else
            {
                this.ShowMessage("TY_M_GB_26E31876");
            }
        }
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            this.FPS91_TY_S_AC_798A1558.Initialize();

            if (!this.ShowMessage("TY_M_GB_26E2Z874"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_798A1558.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_7989J557", this.TXT01_HUYEAR.GetValue().ToString(), this.CBH01_HUDPAC.GetValue() );
            this.FPS91_TY_S_AC_798A1558.SetValue(UP_RowSumAddDt(this.DbConnector.ExecuteDataTable()));

            if (this.FPS91_TY_S_AC_798A1558.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_AC_798A1558.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_798A1558.GetValue(i, "HUCDACNM").ToString() == "[소 계]")
                    {
                        this.FPS91_TY_S_AC_798A1558.ActiveSheet.Rows[i].BackColor = Color.FromArgb(228, 242, 194);
                    }
                    if (this.FPS91_TY_S_AC_798A1558.GetValue(i, "HUADACNM").ToString() == "[합 계]")
                    {
                        this.FPS91_TY_S_AC_798A1558.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
                    }
                }
            }

        }
        #endregion

        #region Description : 소계,합계 추가
        private DataTable UP_RowSumAddDt(DataTable dt)
        {
            int i = 0;

            string sFilter = string.Empty;

            double dHUMONAMT01 = 0;
            double dHUMONAMT02 = 0;
            double dHUMONAMT03 = 0;
            double dHUMONAMT04 = 0;
            double dHUMONAMT05 = 0;
            double dHUMONAMT06 = 0;
            double dHUMONAMT07 = 0;
            double dHUMONAMT08 = 0;
            double dHUMONAMT09 = 0;
            double dHUMONAMT10 = 0;
            double dHUMONAMT11 = 0;
            double dHUMONAMT12 = 0;
            double dHUMONTOTAL = 0;

            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;

            for (i = 1; i < nNum; i++)
            {
                if (table.Rows[i - 1]["HUCDAC"].ToString() != table.Rows[i]["HUCDAC"].ToString()  )
                {
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    // 소 계 이름 넣기
                    table.Rows[i]["HUCDACNM"] = "[소 계]";

                    //  년월, 거래처
                    sFilter = "  HUCDAC  = '" + table.Rows[i - 1]["HUCDAC"].ToString() + "'";


                    table.Rows[i]["HUMONAMT01"] = table.Compute("SUM(HUMONAMT01)", sFilter).ToString();
                    table.Rows[i]["HUMONAMT02"] = table.Compute("SUM(HUMONAMT02)", sFilter).ToString();
                    table.Rows[i]["HUMONAMT03"] = table.Compute("SUM(HUMONAMT03)", sFilter).ToString();
                    table.Rows[i]["HUMONAMT04"] = table.Compute("SUM(HUMONAMT04)", sFilter).ToString();
                    table.Rows[i]["HUMONAMT05"] = table.Compute("SUM(HUMONAMT05)", sFilter).ToString();
                    table.Rows[i]["HUMONAMT06"] = table.Compute("SUM(HUMONAMT06)", sFilter).ToString();
                    table.Rows[i]["HUMONAMT07"] = table.Compute("SUM(HUMONAMT07)", sFilter).ToString();
                    table.Rows[i]["HUMONAMT08"] = table.Compute("SUM(HUMONAMT08)", sFilter).ToString();
                    table.Rows[i]["HUMONAMT09"] = table.Compute("SUM(HUMONAMT09)", sFilter).ToString();
                    table.Rows[i]["HUMONAMT10"] = table.Compute("SUM(HUMONAMT10)", sFilter).ToString();
                    table.Rows[i]["HUMONAMT11"] = table.Compute("SUM(HUMONAMT11)", sFilter).ToString();
                    table.Rows[i]["HUMONAMT12"] = table.Compute("SUM(HUMONAMT12)", sFilter).ToString();
                    table.Rows[i]["HUMONTOTAL"] = table.Compute("SUM(HUMONTOTAL)", sFilter).ToString();

                    dHUMONAMT01 = dHUMONAMT01 + Convert.ToDouble(table.Rows[i]["HUMONAMT01"].ToString());
                    dHUMONAMT02 = dHUMONAMT02 + Convert.ToDouble(table.Rows[i]["HUMONAMT02"].ToString());
                    dHUMONAMT03 = dHUMONAMT03 + Convert.ToDouble(table.Rows[i]["HUMONAMT03"].ToString());
                    dHUMONAMT04 = dHUMONAMT04 + Convert.ToDouble(table.Rows[i]["HUMONAMT04"].ToString());
                    dHUMONAMT05 = dHUMONAMT05 + Convert.ToDouble(table.Rows[i]["HUMONAMT05"].ToString());
                    dHUMONAMT06 = dHUMONAMT06 + Convert.ToDouble(table.Rows[i]["HUMONAMT06"].ToString());
                    dHUMONAMT07 = dHUMONAMT07 + Convert.ToDouble(table.Rows[i]["HUMONAMT07"].ToString());
                    dHUMONAMT08 = dHUMONAMT08 + Convert.ToDouble(table.Rows[i]["HUMONAMT08"].ToString());
                    dHUMONAMT09 = dHUMONAMT09 + Convert.ToDouble(table.Rows[i]["HUMONAMT09"].ToString());
                    dHUMONAMT10 = dHUMONAMT10 + Convert.ToDouble(table.Rows[i]["HUMONAMT10"].ToString());
                    dHUMONAMT11 = dHUMONAMT11 + Convert.ToDouble(table.Rows[i]["HUMONAMT11"].ToString());
                    dHUMONAMT12 = dHUMONAMT12 + Convert.ToDouble(table.Rows[i]["HUMONAMT12"].ToString());
                    dHUMONTOTAL = dHUMONTOTAL + Convert.ToDouble(table.Rows[i]["HUMONTOTAL"].ToString());


                    nNum = nNum + 1;

                    i = i + 1;
                }
            }

            if (nNum > 0)
            {
                /******* 마지막 거래처의 대한 로우 생성*****/
                row = table.NewRow();
                table.Rows.InsertAt(row, i);

                // 소 계 이름 넣기
                table.Rows[i]["HUCDACNM"] = "[소 계]";

                //  년월, 거래처
                sFilter = "  HUCDAC  = '" + table.Rows[i - 1]["HUCDAC"].ToString() + "'";

                table.Rows[i]["HUMONAMT01"] = table.Compute("SUM(HUMONAMT01)", sFilter).ToString();
                table.Rows[i]["HUMONAMT02"] = table.Compute("SUM(HUMONAMT02)", sFilter).ToString();
                table.Rows[i]["HUMONAMT03"] = table.Compute("SUM(HUMONAMT03)", sFilter).ToString();
                table.Rows[i]["HUMONAMT04"] = table.Compute("SUM(HUMONAMT04)", sFilter).ToString();
                table.Rows[i]["HUMONAMT05"] = table.Compute("SUM(HUMONAMT05)", sFilter).ToString();
                table.Rows[i]["HUMONAMT06"] = table.Compute("SUM(HUMONAMT06)", sFilter).ToString();
                table.Rows[i]["HUMONAMT07"] = table.Compute("SUM(HUMONAMT07)", sFilter).ToString();
                table.Rows[i]["HUMONAMT08"] = table.Compute("SUM(HUMONAMT08)", sFilter).ToString();
                table.Rows[i]["HUMONAMT09"] = table.Compute("SUM(HUMONAMT09)", sFilter).ToString();
                table.Rows[i]["HUMONAMT10"] = table.Compute("SUM(HUMONAMT10)", sFilter).ToString();
                table.Rows[i]["HUMONAMT11"] = table.Compute("SUM(HUMONAMT11)", sFilter).ToString();
                table.Rows[i]["HUMONAMT12"] = table.Compute("SUM(HUMONAMT12)", sFilter).ToString();
                table.Rows[i]["HUMONTOTAL"] = table.Compute("SUM(HUMONTOTAL)", sFilter).ToString();

                dHUMONAMT01 = dHUMONAMT01 + Convert.ToDouble(table.Rows[i]["HUMONAMT01"].ToString());
                dHUMONAMT02 = dHUMONAMT02 + Convert.ToDouble(table.Rows[i]["HUMONAMT02"].ToString());
                dHUMONAMT03 = dHUMONAMT03 + Convert.ToDouble(table.Rows[i]["HUMONAMT03"].ToString());
                dHUMONAMT04 = dHUMONAMT04 + Convert.ToDouble(table.Rows[i]["HUMONAMT04"].ToString());
                dHUMONAMT05 = dHUMONAMT05 + Convert.ToDouble(table.Rows[i]["HUMONAMT05"].ToString());
                dHUMONAMT06 = dHUMONAMT06 + Convert.ToDouble(table.Rows[i]["HUMONAMT06"].ToString());
                dHUMONAMT07 = dHUMONAMT07 + Convert.ToDouble(table.Rows[i]["HUMONAMT07"].ToString());
                dHUMONAMT08 = dHUMONAMT08 + Convert.ToDouble(table.Rows[i]["HUMONAMT08"].ToString());
                dHUMONAMT09 = dHUMONAMT09 + Convert.ToDouble(table.Rows[i]["HUMONAMT09"].ToString());
                dHUMONAMT10 = dHUMONAMT10 + Convert.ToDouble(table.Rows[i]["HUMONAMT10"].ToString());
                dHUMONAMT11 = dHUMONAMT11 + Convert.ToDouble(table.Rows[i]["HUMONAMT11"].ToString());
                dHUMONAMT12 = dHUMONAMT12 + Convert.ToDouble(table.Rows[i]["HUMONAMT12"].ToString());
                dHUMONTOTAL = dHUMONTOTAL + Convert.ToDouble(table.Rows[i]["HUMONTOTAL"].ToString());

                /******** 총계를 위한 Row 생성 **************/
                row = table.NewRow();
                table.Rows.InsertAt(row, i + 1);

                // 합 계 이름 넣기
                table.Rows[i + 1]["HUADACNM"] = "[합 계]";

                table.Rows[i + 1]["HUMONAMT01"] = string.Format("{0:#,##0}", dHUMONAMT01);
                table.Rows[i + 1]["HUMONAMT02"] = string.Format("{0:#,##0}", dHUMONAMT02);
                table.Rows[i + 1]["HUMONAMT03"] = string.Format("{0:#,##0}", dHUMONAMT03);
                table.Rows[i + 1]["HUMONAMT04"] = string.Format("{0:#,##0}", dHUMONAMT04);
                table.Rows[i + 1]["HUMONAMT05"] = string.Format("{0:#,##0}", dHUMONAMT05);
                table.Rows[i + 1]["HUMONAMT06"] = string.Format("{0:#,##0}", dHUMONAMT06);
                table.Rows[i + 1]["HUMONAMT07"] = string.Format("{0:#,##0}", dHUMONAMT07);
                table.Rows[i + 1]["HUMONAMT08"] = string.Format("{0:#,##0}", dHUMONAMT08);
                table.Rows[i + 1]["HUMONAMT09"] = string.Format("{0:#,##0}", dHUMONAMT09);
                table.Rows[i + 1]["HUMONAMT10"] = string.Format("{0:#,##0}", dHUMONAMT10);
                table.Rows[i + 1]["HUMONAMT11"] = string.Format("{0:#,##0}", dHUMONAMT11);
                table.Rows[i + 1]["HUMONAMT12"] = string.Format("{0:#,##0}", dHUMONAMT12);
                table.Rows[i + 1]["HUMONTOTAL"] = string.Format("{0:#,##0}", dHUMONTOTAL);
                
            }

            return table;
        }
        #endregion   

        #region Description : 소계,합계 추가
        private void TXT01_HUYEAR_TextChanged(object sender, EventArgs e)
        {
            this.CBH01_HUDPAC.DummyValue = this.TXT01_HUYEAR.GetValue().ToString() + "0101";
        }
        #endregion
    }
}
