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
    public partial class TYBSUP003S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYBSUP003S()
        {
            InitializeComponent();
        }

        private void TYBSUP003S_Load(object sender, System.EventArgs e)
        {
            this.TXT01_BCYEAR.SetValue(DateTime.Now.AddYears(1).ToString("yyyy"));
            this.TXT02_BCYEAR.SetValue(DateTime.Now.AddYears(1).ToString("yyyy"));

            CBH01_BCDPMK.DummyValue = this.TXT01_BCYEAR.GetValue().ToString() + "0101";
            CBH01_BCDPAC.DummyValue = this.TXT01_BCYEAR.GetValue().ToString() + "0101";

            CBH02_BCDPAC.DummyValue = this.TXT01_BCYEAR.GetValue().ToString() + "0101";

            this.SetStartingFocus(this.TXT01_BCYEAR);
        }
        #endregion

        #region  Description : 조회(담당부서별) 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sGubn = string.Empty;
            string sSubChk = "N";

            if (CBO01_INQOPTION2.GetValue().ToString() == "ALL")
            {
                sGubn = "CM,SE";
            }
            else
            {
                sGubn = CBO01_INQOPTION2.GetValue().ToString();
            }

            if (CKB01_BPCHK_ALL.Checked == true)
            {
                sSubChk = "Y";
            }

            this.FPS91_TY_S_AC_788BE383.Initialize();
            this.DbConnector.CommandClear();
            if (CBO01_INQOPTION2.GetValue().ToString() == "ALL")
            {
                this.DbConnector.Attach(CBO01_INQOPTION.GetValue().ToString() == "S" ? "TY_P_AC_78MFH476" : "TY_P_AC_78MFR477", TXT01_BCYEAR.GetValue().ToString(), CBH01_BCDPMK.GetValue(), CBH01_BCDPAC.GetValue(), sGubn, sSubChk);
            }
            else
            {
                this.DbConnector.Attach(CBO01_INQOPTION.GetValue().ToString() == "S" ? "TY_P_AC_79SDT701" : "TY_P_AC_78MFR477", TXT01_BCYEAR.GetValue().ToString(), CBH01_BCDPMK.GetValue(), CBH01_BCDPAC.GetValue(), sGubn, sSubChk);
            }
            this.FPS91_TY_S_AC_788BE383.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_788BE383.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_AC_788BE383.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_788BE383.GetValue(i, "DATAGN").ToString() == "HAP")
                    {
                        this.FPS91_TY_S_AC_788BE383.ActiveSheet.Rows[i].BackColor = Color.FromArgb(228, 242, 194);

                        for (int j = 19; j < 32; j++)
                        {
                            this.FPS91_TY_S_AC_788BE383_Sheet1.Cells[i, j].ForeColor = Color.Red;
                        }

                    }

                    if (this.FPS91_TY_S_AC_788BE383.GetValue(i, "DATAGN").ToString() == "SUB")
                    {
                        for (int j = 11; j < 32; j++)
                        {
                            this.FPS91_TY_S_AC_788BE383.ActiveSheet.Cells[i, j].BackColor = Color.FromArgb(218, 239, 244);
                        }

                        for (int j = 19; j < 32; j++)
                        {
                            this.FPS91_TY_S_AC_788BE383_Sheet1.Cells[i, j].ForeColor = Color.Red;
                        }

                    }
                    
                    if (this.FPS91_TY_S_AC_788BE383.GetValue(i, "DATAGN").ToString() == "TOT")
                    {
                        this.FPS91_TY_S_AC_788BE383.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);

                        for (int j = 19; j < 32; j++)
                        {
                            this.FPS91_TY_S_AC_788BE383_Sheet1.Cells[i, j].ForeColor = Color.Red;
                        }

                    }
                }
            }
        }
        #endregion

        #region  Description : 조회(귀속부서별) 버튼 이벤트
        private void BTN62_INQ_Click(object sender, EventArgs e)
        {
            this.UP_Set_TitleSetting();

            this.FPS91_TY_S_AC_79CD4563.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach(CBO02_INQOPTION.GetValue().ToString() == "S" ? "TY_P_AC_79CDI564" : "TY_P_AC_79CI5565", TXT02_BCYEAR.GetValue().ToString(), CBH02_BCDPAC.GetValue(), CBH02_BCADAC.GetValue());
            this.FPS91_TY_S_AC_79CD4563.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_79CD4563.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_AC_79CD4563.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_79CD4563.GetValue(i, "BCCDAC").ToString() == "")
                    {
                        this.FPS91_TY_S_AC_79CD4563.ActiveSheet.Rows[i].BackColor = Color.FromArgb(228, 242, 194);

                        for (int j = 11; j < 29; j++)
                        {
                            this.FPS91_TY_S_AC_79CD4563_Sheet1.Cells[i, j].ForeColor = Color.Red;
                        }
                    }
                }
            }

        }
        #endregion

        #region Description :  조회(귀속부서별) 그리드 타이트 조정 함수
        private void UP_Set_TitleSetting()
        {
            this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_79CD4563_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_79CD4563_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_AC_79CD4563_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_AC_79CD4563_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);
            this.FPS91_TY_S_AC_79CD4563_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);
            this.FPS91_TY_S_AC_79CD4563_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1);
            this.FPS91_TY_S_AC_79CD4563_Sheet1.AddColumnHeaderSpanCell(0, 5, 2, 1);
            this.FPS91_TY_S_AC_79CD4563_Sheet1.AddColumnHeaderSpanCell(0, 6, 2, 1);
            this.FPS91_TY_S_AC_79CD4563_Sheet1.AddColumnHeaderSpanCell(0, 7, 2, 1);
            this.FPS91_TY_S_AC_79CD4563_Sheet1.AddColumnHeaderSpanCell(0, 8, 2, 1);
            this.FPS91_TY_S_AC_79CD4563_Sheet1.AddColumnHeaderSpanCell(0, 9, 2, 1);
            this.FPS91_TY_S_AC_79CD4563_Sheet1.AddColumnHeaderSpanCell(0, 10, 2, 1);
            
            this.FPS91_TY_S_AC_79CD4563_Sheet1.AddColumnHeaderSpanCell(0, 11, 1, 13);

            this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[0, 0].Value = "년 도";
            this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[0, 1].Value = "귀속부서";
            this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[0, 2].Value = "귀 속 명";
            this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[0, 3].Value = "계정과목";
            this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[0, 4].Value = "계정과목명";
            this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[0, 5].Value = "계정과목";
            this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[0, 6].Value = "계정과목명";
            this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[0, 7].Value = "계정세목";
            this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[0, 8].Value = "계정세목명";
            this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[0, 9].Value = "항목코드";
            this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[0, 10].Value = "항 목 명";

            this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[0, 11].Value = TXT02_BCYEAR.GetValue().ToString()+"년" ;
            this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[1, 11].Value = "1월";
            this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[1, 12].Value = "2월";
            this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[1, 13].Value = "3월";
            this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[1, 14].Value = "4월";
            this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[1, 15].Value = "5월";
            this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[1, 16].Value = "6월";
            this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[1, 17].Value = "7월";
            this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[1, 18].Value = "8월";
            this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[1, 19].Value = "9월";
            this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[1, 20].Value = "10월";
            this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[1, 21].Value = "11월";
            this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[1, 22].Value = "12월";
            this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[1, 23].Value = "합 계";

            this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[0, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            
            this.FPS91_TY_S_AC_79CD4563_Sheet1.AddColumnHeaderSpanCell(0, 24, 2, 1);

            this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[1, 24].Value = "공통배부";

            this.FPS91_TY_S_AC_79CD4563_Sheet1.AddColumnHeaderSpanCell(0, 25, 1, 4);                       

            this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[0, 25].Value = "자  체";

            if (CBH02_BCDPAC.GetValue().ToString().Substring(0, 2) == "T1" || CBH02_BCDPAC.GetValue().ToString().Substring(0, 2) == "S1")
            {
                this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[1, 25].Value = "운영";
                this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[1, 26].Value = "공무";
                this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[1, 27].Value = "안전";
                this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[1, 28].Value = "영업";
            }
            else if (CBH02_BCDPAC.GetValue().ToString().Substring(0, 2) == "A1")
            {
                this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[1, 25].Value = "총무";
                this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[1, 26].Value = "회계";
                this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[1, 27].Value = "전산";
                this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[1, 28].Value = "";
            }
            else
            {
                this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[1, 25].Value = "자체예산";
                this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[1, 26].Value = "";
                this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[1, 27].Value = "";
                this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[1, 28].Value = "";
            }

            this.FPS91_TY_S_AC_79CD4563_Sheet1.ColumnHeader.Cells[0, 25].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;


        }
        #endregion

        #region Description : 소계,합계 Row 추가 함수
        private DataTable UP_Set_SumRowAdd(DataTable dt)
        {
            int i = 0;            

            string sFilter = string.Empty;

            string sBCADAC_Now = string.Empty;
            string sBCADAC_Old = string.Empty;

            double dBCMONAMT01 = 0;
            double dBCMONAMT02 = 0;
            double dBCMONAMT03 = 0;
            double dBCMONAMT04 = 0;
            double dBCMONAMT05 = 0;
            double dBCMONAMT06 = 0;
            double dBCMONAMT07 = 0;
            double dBCMONAMT08 = 0;
            double dBCMONAMT09 = 0;
            double dBCMONAMT10 = 0;
            double dBCMONAMT11 = 0;
            double dBCMONAMT12 = 0;
            double dBCMONTOTAL = 0;

            DataTable table = new DataTable();           
            
            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;

            for (i = 1; i < nNum; i++)
            {

                if (table.Rows[i - 1]["BCADAC"].ToString() != table.Rows[i]["BCADAC"].ToString() || i == 1
                    )                   
                {
                    if (i == 1)
                    {
                        row = table.NewRow();
                        table.Rows.InsertAt(row, 0);

                        // 소 계 이름 넣기
                        table.Rows[0]["BCFORM"] = table.Rows[1]["BCFORM"].ToString();
                        table.Rows[0]["BCYEAR"] = table.Rows[1]["BCYEAR"].ToString();
                        table.Rows[0]["BCDPMK"] = table.Rows[1]["BCDPMK"].ToString();
                        table.Rows[0]["BCDPMKNM"] = table.Rows[1]["BCDPMKNM"].ToString();
                        //table.Rows[0]["BCADAC"] = table.Rows[1]["BCADAC"].ToString();
                        //table.Rows[0]["BCADACNM"] = table.Rows[1]["BCADACNM"].ToString();
                        table.Rows[0]["BCCDACNM"] = "[ 계정 소계 ]";

                        //  귀속부서
                        sFilter = "  BCADAC  = '" + table.Rows[1]["BCADAC"].ToString() + "' ";

                    }
                    else
                    {
                        row = table.NewRow();
                        table.Rows.InsertAt(row, i);

                        // 소 계 이름 넣기
                        table.Rows[i]["BCFORM"] = table.Rows[i+1]["BCFORM"].ToString();
                        table.Rows[i]["BCYEAR"] = table.Rows[i + 1]["BCYEAR"].ToString();
                        table.Rows[i]["BCDPMK"] = table.Rows[i + 1]["BCDPMK"].ToString();
                        table.Rows[i]["BCDPMKNM"] = table.Rows[i + 1]["BCDPMKNM"].ToString();
                        //table.Rows[i]["BCADAC"] = table.Rows[i + 1]["BCADAC"].ToString();
                        //table.Rows[i]["BCADACNM"] = table.Rows[i + 1]["BCADACNM"].ToString();
                        table.Rows[i]["BCCDACNM"] = "[ 계정 소계 ]";

                        //  귀속부서
                        sFilter = "  BCADAC  = '" + table.Rows[i + 1]["BCADAC"].ToString() + "' ";
                    }

                    if (i == 1)
                    {
                        table.Rows[0]["BCMONAMT01"] = table.Compute("SUM(BCMONAMT01)", sFilter).ToString();
                        table.Rows[0]["BCMONAMT02"] = table.Compute("SUM(BCMONAMT02)", sFilter).ToString();
                        table.Rows[0]["BCMONAMT03"] = table.Compute("SUM(BCMONAMT03)", sFilter).ToString();
                        table.Rows[0]["BCMONAMT04"] = table.Compute("SUM(BCMONAMT04)", sFilter).ToString();
                        table.Rows[0]["BCMONAMT05"] = table.Compute("SUM(BCMONAMT05)", sFilter).ToString();
                        table.Rows[0]["BCMONAMT06"] = table.Compute("SUM(BCMONAMT06)", sFilter).ToString();
                        table.Rows[0]["BCMONAMT07"] = table.Compute("SUM(BCMONAMT07)", sFilter).ToString();
                        table.Rows[0]["BCMONAMT08"] = table.Compute("SUM(BCMONAMT08)", sFilter).ToString();
                        table.Rows[0]["BCMONAMT09"] = table.Compute("SUM(BCMONAMT09)", sFilter).ToString();
                        table.Rows[0]["BCMONAMT10"] = table.Compute("SUM(BCMONAMT10)", sFilter).ToString();
                        table.Rows[0]["BCMONAMT11"] = table.Compute("SUM(BCMONAMT11)", sFilter).ToString();
                        table.Rows[0]["BCMONAMT12"] = table.Compute("SUM(BCMONAMT12)", sFilter).ToString();
                        table.Rows[0]["BCMONTOTAL"] = table.Compute("SUM(BCMONTOTAL)", sFilter).ToString();


                        dBCMONAMT01 = dBCMONAMT01 + Convert.ToDouble(table.Rows[0]["BCMONAMT01"].ToString());
                        dBCMONAMT02 = dBCMONAMT02 + Convert.ToDouble(table.Rows[0]["BCMONAMT02"].ToString());
                        dBCMONAMT03 = dBCMONAMT03 + Convert.ToDouble(table.Rows[0]["BCMONAMT03"].ToString());
                        dBCMONAMT04 = dBCMONAMT04 + Convert.ToDouble(table.Rows[0]["BCMONAMT04"].ToString());
                        dBCMONAMT05 = dBCMONAMT05 + Convert.ToDouble(table.Rows[0]["BCMONAMT05"].ToString());
                        dBCMONAMT06 = dBCMONAMT06 + Convert.ToDouble(table.Rows[0]["BCMONAMT06"].ToString());
                        dBCMONAMT07 = dBCMONAMT07 + Convert.ToDouble(table.Rows[0]["BCMONAMT07"].ToString());
                        dBCMONAMT08 = dBCMONAMT08 + Convert.ToDouble(table.Rows[0]["BCMONAMT08"].ToString());
                        dBCMONAMT09 = dBCMONAMT09 + Convert.ToDouble(table.Rows[0]["BCMONAMT09"].ToString());
                        dBCMONAMT10 = dBCMONAMT10 + Convert.ToDouble(table.Rows[0]["BCMONAMT10"].ToString());
                        dBCMONAMT11 = dBCMONAMT11 + Convert.ToDouble(table.Rows[0]["BCMONAMT11"].ToString());
                        dBCMONAMT12 = dBCMONAMT12 + Convert.ToDouble(table.Rows[0]["BCMONAMT12"].ToString());
                        dBCMONTOTAL = dBCMONTOTAL + Convert.ToDouble(table.Rows[0]["BCMONTOTAL"].ToString());
                    }
                    else
                    {
                        table.Rows[i]["BCMONAMT01"] = table.Compute("SUM(BCMONAMT01)", sFilter).ToString();
                        table.Rows[i]["BCMONAMT02"] = table.Compute("SUM(BCMONAMT02)", sFilter).ToString();
                        table.Rows[i]["BCMONAMT03"] = table.Compute("SUM(BCMONAMT03)", sFilter).ToString();
                        table.Rows[i]["BCMONAMT04"] = table.Compute("SUM(BCMONAMT04)", sFilter).ToString();
                        table.Rows[i]["BCMONAMT05"] = table.Compute("SUM(BCMONAMT05)", sFilter).ToString();
                        table.Rows[i]["BCMONAMT06"] = table.Compute("SUM(BCMONAMT06)", sFilter).ToString();
                        table.Rows[i]["BCMONAMT07"] = table.Compute("SUM(BCMONAMT07)", sFilter).ToString();
                        table.Rows[i]["BCMONAMT08"] = table.Compute("SUM(BCMONAMT08)", sFilter).ToString();
                        table.Rows[i]["BCMONAMT09"] = table.Compute("SUM(BCMONAMT09)", sFilter).ToString();
                        table.Rows[i]["BCMONAMT10"] = table.Compute("SUM(BCMONAMT10)", sFilter).ToString();
                        table.Rows[i]["BCMONAMT11"] = table.Compute("SUM(BCMONAMT11)", sFilter).ToString();
                        table.Rows[i]["BCMONAMT12"] = table.Compute("SUM(BCMONAMT12)", sFilter).ToString();
                        table.Rows[i]["BCMONTOTAL"] = table.Compute("SUM(BCMONTOTAL)", sFilter).ToString();


                        dBCMONAMT01 = dBCMONAMT01 + Convert.ToDouble(dt.Rows[i]["BCMONAMT01"].ToString());
                        dBCMONAMT02 = dBCMONAMT02 + Convert.ToDouble(dt.Rows[i]["BCMONAMT02"].ToString());
                        dBCMONAMT03 = dBCMONAMT03 + Convert.ToDouble(dt.Rows[i]["BCMONAMT03"].ToString());
                        dBCMONAMT04 = dBCMONAMT04 + Convert.ToDouble(dt.Rows[i]["BCMONAMT04"].ToString());
                        dBCMONAMT05 = dBCMONAMT05 + Convert.ToDouble(dt.Rows[i]["BCMONAMT05"].ToString());
                        dBCMONAMT06 = dBCMONAMT06 + Convert.ToDouble(dt.Rows[i]["BCMONAMT06"].ToString());
                        dBCMONAMT07 = dBCMONAMT07 + Convert.ToDouble(dt.Rows[i]["BCMONAMT07"].ToString());
                        dBCMONAMT08 = dBCMONAMT08 + Convert.ToDouble(dt.Rows[i]["BCMONAMT08"].ToString());
                        dBCMONAMT09 = dBCMONAMT09 + Convert.ToDouble(dt.Rows[i]["BCMONAMT09"].ToString());
                        dBCMONAMT10 = dBCMONAMT10 + Convert.ToDouble(dt.Rows[i]["BCMONAMT10"].ToString());
                        dBCMONAMT11 = dBCMONAMT11 + Convert.ToDouble(dt.Rows[i]["BCMONAMT11"].ToString());
                        dBCMONAMT12 = dBCMONAMT12 + Convert.ToDouble(dt.Rows[i]["BCMONAMT12"].ToString());
                        dBCMONTOTAL = dBCMONTOTAL + Convert.ToDouble(dt.Rows[i]["BCMONTOTAL"].ToString());
                    }

                    nNum = nNum + 1;

                    i = i + 1;
                }                
            }           

            return table;
        }
        #endregion   

        

       

    }
}
