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
    /// 당기실적 매출액&물량 관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.09.14 09:56
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_79EA5588 : 당기실적 매출액&물량 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_79EA8589 : 당기실적 매출액&물량 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  SAV : 저장
    ///  INQOPTION2 : 조회구분
    ///  BSJYYMM : 실적생성년월
    ///  INQOPTION : 조회구분
    /// </summary>
    public partial class TYBSSJ002I : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYBSSJ002I()
        {
            InitializeComponent();
        }

        private void TYBSSJ002I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            CBH01_BSJYYMM.SetValue(UP_Get_LastSJYYMM());

            if (UP_Get_SJCheckClose() == "Y")
            {
                this.BTN61_SAV.SetReadOnly(true);
            }

            this.SetStartingFocus(CBH01_BSJYYMM);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            UP_Set_TitleSetting();

            this.FPS91_TY_S_AC_79EA8589.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_79EA5588", this.CBH01_BSJYYMM.GetValue().ToString(), CBO01_INQOPTION.GetValue());
            this.FPS91_TY_S_AC_79EA8589.SetValue(UP_Set_SumRowAdd(this.DbConnector.ExecuteDataTable()));

            if (this.FPS91_TY_S_AC_79EA8589.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_AC_79EA8589.CurrentRowCount; i++)
                {

                    if (this.FPS91_TY_S_AC_79EA8589.GetValue(i, "BSJGUBNNM").ToString() == "[ 매출소계 ]")
                    {
                        this.FPS91_TY_S_AC_79EA8589.ActiveSheet.Rows[i].BackColor = Color.FromArgb(228, 242, 194);

                        for (int j = 7; j < 21; j++)
                        {
                            this.FPS91_TY_S_AC_79EA8589_Sheet1.Cells[i, j].ForeColor = Color.Black;
                            this.FPS91_TY_S_AC_79EA8589_Sheet1.Cells[i, j].Locked = true;
                        }                                                                   
                    }
                    else if (this.FPS91_TY_S_AC_79EA8589.GetValue(i, "BSJDPAC").ToString() == "[ 매출합계 ]")
                    {
                        this.FPS91_TY_S_AC_79EA8589.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);

                        for (int j = 7; j < 21; j++)
                        {
                            this.FPS91_TY_S_AC_79EA8589_Sheet1.Cells[i, j].ForeColor = Color.Black;
                            this.FPS91_TY_S_AC_79EA8589_Sheet1.Cells[i, j].Locked = true;
                        }
                    }
                    else
                    {
                        //수선비는 실적월까지 수정할수 없다
                        int iAddMonth = Convert.ToInt16(this.CBH01_BSJYYMM.GetValue().ToString().Substring(4, 2));

                        if (iAddMonth > 9)
                        {
                            iAddMonth = iAddMonth + 1;
                        }

                        for (int j = 7; j < (7 + iAddMonth); j++)
                        {
                            this.FPS91_TY_S_AC_79EA8589_Sheet1.Cells[i, j].ForeColor = Color.Black;
                            this.FPS91_TY_S_AC_79EA8589_Sheet1.Cells[i, j].Locked = true;
                        }

                        //수정가능월 글자 bold 처리
                        for (int j = (7 + iAddMonth); j < (7 + iAddMonth) + (this.FPS91_TY_S_AC_79EA8589_Sheet1.ColumnHeader.Columns.Count - (7 + iAddMonth)); j++)
                        {
                            if (this.FPS91_TY_S_AC_79EA8589_Sheet1.ColumnHeader.Cells[1, j].Value.ToString() != "소 계" && this.FPS91_TY_S_AC_79EA8589_Sheet1.ColumnHeader.Cells[1, j].Value.ToString() != "합 계")
                            {
                                this.FPS91_TY_S_AC_79EA8589_Sheet1.Cells[i, j].Font = new Font("굴림", 9, FontStyle.Bold);
                            }
                        }


                    }
                }
            }
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //소계,합계라인은 제외
                if (ds.Tables[0].Rows[i]["BSJYYMM"].ToString() != "")
                {
                    this.DbConnector.Attach("TY_P_AC_79EEC595", ds.Tables[0].Rows[i]["BSJMONAMT01"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSJMONAMT02"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSJMONAMT03"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSJMONAMT04"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSJMONAMT05"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSJMONAMT06"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSJMONAMT07"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSJMONAMT08"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSJMONAMT09"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSJMONAMT10"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSJMONAMT11"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSJMONAMT12"].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[0].Rows[i]["BSJYYMM"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSJFORM"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSJYEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSJDPAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSJGUBN"].ToString());
                }

             
            }
            if (this.DbConnector.CommandCount > 0)
                this.DbConnector.ExecuteTranQueryList();

            //합계 정리
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_79EG5599", TYUserInfo.EmpNo, CBH01_BSJYYMM.GetValue().ToString());
            this.DbConnector.ExecuteTranQuery();
            
            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_79EA8589.GetDataSourceInclude(TSpread.TActionType.Update, "BSJYYMM", "BSJFORM", "BSJYEAR", "BSJDPAC", "BSJGUBN", "BSJMONAMT01","BSJMONAMT02","BSJMONAMT03","BSJMONAMT04","BSJMONAMT05","BSJMONAMT06","BSJMONAMT07","BSJMONAMT08","BSJMONAMT09","BSJMONAMT10","BSJMONAMT11","BSJMONAMT12"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            if (UP_Get_SJCheckClose() == "Y")
            {
                this.ShowCustomMessage("당기실적이 마감되었습니다! 수정할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return; 
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description :  그리드 타이트 셋팅 함수
        private void UP_Set_TitleSetting()
        {
            this.FPS91_TY_S_AC_79EA8589_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_79EA8589_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_79EA8589_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_AC_79EA8589_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_AC_79EA8589_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);
            this.FPS91_TY_S_AC_79EA8589_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);
            this.FPS91_TY_S_AC_79EA8589_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1);
            this.FPS91_TY_S_AC_79EA8589_Sheet1.AddColumnHeaderSpanCell(0, 5, 2, 1);
            this.FPS91_TY_S_AC_79EA8589_Sheet1.AddColumnHeaderSpanCell(0, 6, 2, 1);

            this.FPS91_TY_S_AC_79EA8589_Sheet1.AddColumnHeaderSpanCell(0, 21, 2, 1);

            this.FPS91_TY_S_AC_79EA8589_Sheet1.AddColumnHeaderSpanCell(0, 7, 1, 10);  //1월 ~ 9월
            this.FPS91_TY_S_AC_79EA8589_Sheet1.AddColumnHeaderSpanCell(0, 17, 1, 4);  //10월 ~ 12월

            this.FPS91_TY_S_AC_79EA8589_Sheet1.ColumnHeader.Cells[0, 0].Value = "실적생성년월";
            this.FPS91_TY_S_AC_79EA8589_Sheet1.ColumnHeader.Cells[0, 1].Value = "구분";
            this.FPS91_TY_S_AC_79EA8589_Sheet1.ColumnHeader.Cells[0, 2].Value = "사업년도";
            this.FPS91_TY_S_AC_79EA8589_Sheet1.ColumnHeader.Cells[0, 3].Value = "귀속부서";
            this.FPS91_TY_S_AC_79EA8589_Sheet1.ColumnHeader.Cells[0, 4].Value = "귀속부서";
            this.FPS91_TY_S_AC_79EA8589_Sheet1.ColumnHeader.Cells[0, 5].Value = "구 분";
            this.FPS91_TY_S_AC_79EA8589_Sheet1.ColumnHeader.Cells[0, 6].Value = "구 분";

            this.FPS91_TY_S_AC_79EA8589_Sheet1.ColumnHeader.Cells[0, 7].Value = this.CBH01_BSJYYMM.GetValue().ToString().Substring(0,4) + "년 1월 ~ 9월";
            this.FPS91_TY_S_AC_79EA8589_Sheet1.ColumnHeader.Cells[1, 7].Value = "1월";
            this.FPS91_TY_S_AC_79EA8589_Sheet1.ColumnHeader.Cells[1, 8].Value = "2월";
            this.FPS91_TY_S_AC_79EA8589_Sheet1.ColumnHeader.Cells[1, 9].Value = "3월";
            this.FPS91_TY_S_AC_79EA8589_Sheet1.ColumnHeader.Cells[1, 10].Value = "4월";
            this.FPS91_TY_S_AC_79EA8589_Sheet1.ColumnHeader.Cells[1, 11].Value = "5월";
            this.FPS91_TY_S_AC_79EA8589_Sheet1.ColumnHeader.Cells[1, 12].Value = "6월";
            this.FPS91_TY_S_AC_79EA8589_Sheet1.ColumnHeader.Cells[1, 13].Value = "7월";
            this.FPS91_TY_S_AC_79EA8589_Sheet1.ColumnHeader.Cells[1, 14].Value = "8월";
            this.FPS91_TY_S_AC_79EA8589_Sheet1.ColumnHeader.Cells[1, 15].Value = "9월";
            this.FPS91_TY_S_AC_79EA8589_Sheet1.ColumnHeader.Cells[1, 16].Value = "소 계";

            this.FPS91_TY_S_AC_79EA8589_Sheet1.ColumnHeader.Cells[0, 17].Value = this.CBH01_BSJYYMM.GetValue().ToString().Substring(0, 4) + "년 10월 ~ 12월";
            this.FPS91_TY_S_AC_79EA8589_Sheet1.ColumnHeader.Cells[1, 17].Value = "10월";
            this.FPS91_TY_S_AC_79EA8589_Sheet1.ColumnHeader.Cells[1, 18].Value = "11월";
            this.FPS91_TY_S_AC_79EA8589_Sheet1.ColumnHeader.Cells[1, 19].Value = "12월";
            this.FPS91_TY_S_AC_79EA8589_Sheet1.ColumnHeader.Cells[1, 20].Value = "소 계";

            this.FPS91_TY_S_AC_79EA8589_Sheet1.ColumnHeader.Cells[1, 21].Value = "합 계";

            this.FPS91_TY_S_AC_79EA8589_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_79EA8589_Sheet1.ColumnHeader.Cells[0, 17].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

        }
        #endregion

        #region Description : 소계,합계 Row 추가 함수
        private DataTable UP_Set_SumRowAdd(DataTable dt)
        {
            int i = 0;

            string sFilter = string.Empty;

            double dBSJMONAMT01 = 0;
            double dBSJMONAMT02 = 0;
            double dBSJMONAMT03 = 0;
            double dBSJMONAMT04 = 0;
            double dBSJMONAMT05 = 0;
            double dBSJMONAMT06 = 0;
            double dBSJMONAMT07 = 0;
            double dBSJMONAMT08 = 0;
            double dBSJMONAMT09 = 0;
            double dBSJMONAMT10 = 0;
            double dBSJMONAMT11 = 0;
            double dBSJMONAMT12 = 0;
            double dBSJHFTOTAL = 0;
            double dBSJAFTOTAL = 0;

            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;

            for (i = 1; i < nNum; i++)
            {
                if (table.Rows[i - 1]["BSJDPAC"].ToString() != table.Rows[i]["BSJDPAC"].ToString())
                {
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    // 소 계 이름 넣기
                    table.Rows[i]["BSJGUBNNM"] = "[ 매출소계 ]";

                    //  귀속부서
                    sFilter = "  BSJDPAC  = '" + table.Rows[i - 1]["BSJDPAC"].ToString() + "'";
                    sFilter = sFilter + "  AND BSJFORM  = 'MA' ";

                    table.Rows[i]["BSJMONAMT01"] = table.Compute("SUM(BSJMONAMT01)", sFilter).ToString();
                    table.Rows[i]["BSJMONAMT02"] = table.Compute("SUM(BSJMONAMT02)", sFilter).ToString();
                    table.Rows[i]["BSJMONAMT03"] = table.Compute("SUM(BSJMONAMT03)", sFilter).ToString();
                    table.Rows[i]["BSJMONAMT04"] = table.Compute("SUM(BSJMONAMT04)", sFilter).ToString();
                    table.Rows[i]["BSJMONAMT05"] = table.Compute("SUM(BSJMONAMT05)", sFilter).ToString();
                    table.Rows[i]["BSJMONAMT06"] = table.Compute("SUM(BSJMONAMT06)", sFilter).ToString();
                    table.Rows[i]["BSJMONAMT07"] = table.Compute("SUM(BSJMONAMT07)", sFilter).ToString();
                    table.Rows[i]["BSJMONAMT08"] = table.Compute("SUM(BSJMONAMT08)", sFilter).ToString();
                    table.Rows[i]["BSJMONAMT09"] = table.Compute("SUM(BSJMONAMT09)", sFilter).ToString();
                    table.Rows[i]["BSJMONAMT10"] = table.Compute("SUM(BSJMONAMT10)", sFilter).ToString();
                    table.Rows[i]["BSJMONAMT11"] = table.Compute("SUM(BSJMONAMT11)", sFilter).ToString();
                    table.Rows[i]["BSJMONAMT12"] = table.Compute("SUM(BSJMONAMT12)", sFilter).ToString();
                    table.Rows[i]["BSJHFTOTAL"] = table.Compute("SUM(BSJHFTOTAL)", sFilter).ToString();
                    table.Rows[i]["BSJAFTOTAL"] = table.Compute("SUM(BSJAFTOTAL)", sFilter).ToString();
                    table.Rows[i]["BSJMONTOTAL"] = table.Compute("SUM(BSJMONTOTAL)", sFilter).ToString();

                    dBSJMONAMT01 = dBSJMONAMT01 + Convert.ToDouble(table.Rows[i]["BSJMONAMT01"].ToString());
                    dBSJMONAMT02 = dBSJMONAMT02 + Convert.ToDouble(table.Rows[i]["BSJMONAMT02"].ToString());
                    dBSJMONAMT03 = dBSJMONAMT03 + Convert.ToDouble(table.Rows[i]["BSJMONAMT03"].ToString());
                    dBSJMONAMT04 = dBSJMONAMT04 + Convert.ToDouble(table.Rows[i]["BSJMONAMT04"].ToString());
                    dBSJMONAMT05 = dBSJMONAMT05 + Convert.ToDouble(table.Rows[i]["BSJMONAMT05"].ToString());
                    dBSJMONAMT06 = dBSJMONAMT06 + Convert.ToDouble(table.Rows[i]["BSJMONAMT06"].ToString());
                    dBSJMONAMT07 = dBSJMONAMT07 + Convert.ToDouble(table.Rows[i]["BSJMONAMT07"].ToString());
                    dBSJMONAMT08 = dBSJMONAMT08 + Convert.ToDouble(table.Rows[i]["BSJMONAMT08"].ToString());
                    dBSJMONAMT09 = dBSJMONAMT09 + Convert.ToDouble(table.Rows[i]["BSJMONAMT09"].ToString());
                    dBSJMONAMT10 = dBSJMONAMT10 + Convert.ToDouble(table.Rows[i]["BSJMONAMT10"].ToString());
                    dBSJMONAMT11 = dBSJMONAMT11 + Convert.ToDouble(table.Rows[i]["BSJMONAMT11"].ToString());
                    dBSJMONAMT12 = dBSJMONAMT12 + Convert.ToDouble(table.Rows[i]["BSJMONAMT12"].ToString());
                    dBSJHFTOTAL = dBSJHFTOTAL + Convert.ToDouble(table.Rows[i]["BSJHFTOTAL"].ToString());
                    dBSJAFTOTAL = dBSJAFTOTAL + Convert.ToDouble(table.Rows[i]["BSJAFTOTAL"].ToString());

                    nNum = nNum + 1;

                    i = i + 1;
                }
            }

            if (nNum > 0)
            {
                /******* 마지막 거래처의 대한 로우 생성*****/
                row = table.NewRow();
                table.Rows.InsertAt(row, i);

                table.Rows[i]["BSJGUBNNM"] = "[ 매출소계 ]";

                //  귀속부서
                sFilter = "  BSJDPAC  = '" + table.Rows[i - 1]["BSJDPAC"].ToString() + "'";
                sFilter = sFilter + "  AND BSJFORM  = 'MA' ";

                table.Rows[i]["BSJMONAMT01"] = table.Compute("SUM(BSJMONAMT01)", sFilter).ToString();
                table.Rows[i]["BSJMONAMT02"] = table.Compute("SUM(BSJMONAMT02)", sFilter).ToString();
                table.Rows[i]["BSJMONAMT03"] = table.Compute("SUM(BSJMONAMT03)", sFilter).ToString();
                table.Rows[i]["BSJMONAMT04"] = table.Compute("SUM(BSJMONAMT04)", sFilter).ToString();
                table.Rows[i]["BSJMONAMT05"] = table.Compute("SUM(BSJMONAMT05)", sFilter).ToString();
                table.Rows[i]["BSJMONAMT06"] = table.Compute("SUM(BSJMONAMT06)", sFilter).ToString();
                table.Rows[i]["BSJMONAMT07"] = table.Compute("SUM(BSJMONAMT07)", sFilter).ToString();
                table.Rows[i]["BSJMONAMT08"] = table.Compute("SUM(BSJMONAMT08)", sFilter).ToString();
                table.Rows[i]["BSJMONAMT09"] = table.Compute("SUM(BSJMONAMT09)", sFilter).ToString();
                table.Rows[i]["BSJMONAMT10"] = table.Compute("SUM(BSJMONAMT10)", sFilter).ToString();
                table.Rows[i]["BSJMONAMT11"] = table.Compute("SUM(BSJMONAMT11)", sFilter).ToString();
                table.Rows[i]["BSJMONAMT12"] = table.Compute("SUM(BSJMONAMT12)", sFilter).ToString();
                table.Rows[i]["BSJHFTOTAL"] = table.Compute("SUM(BSJHFTOTAL)", sFilter).ToString();
                table.Rows[i]["BSJAFTOTAL"] = table.Compute("SUM(BSJAFTOTAL)", sFilter).ToString();
                table.Rows[i]["BSJMONTOTAL"] = table.Compute("SUM(BSJMONTOTAL)", sFilter).ToString();

                dBSJMONAMT01 = dBSJMONAMT01 + Convert.ToDouble(table.Rows[i]["BSJMONAMT01"].ToString());
                dBSJMONAMT02 = dBSJMONAMT02 + Convert.ToDouble(table.Rows[i]["BSJMONAMT02"].ToString());
                dBSJMONAMT03 = dBSJMONAMT03 + Convert.ToDouble(table.Rows[i]["BSJMONAMT03"].ToString());
                dBSJMONAMT04 = dBSJMONAMT04 + Convert.ToDouble(table.Rows[i]["BSJMONAMT04"].ToString());
                dBSJMONAMT05 = dBSJMONAMT05 + Convert.ToDouble(table.Rows[i]["BSJMONAMT05"].ToString());
                dBSJMONAMT06 = dBSJMONAMT06 + Convert.ToDouble(table.Rows[i]["BSJMONAMT06"].ToString());
                dBSJMONAMT07 = dBSJMONAMT07 + Convert.ToDouble(table.Rows[i]["BSJMONAMT07"].ToString());
                dBSJMONAMT08 = dBSJMONAMT08 + Convert.ToDouble(table.Rows[i]["BSJMONAMT08"].ToString());
                dBSJMONAMT09 = dBSJMONAMT09 + Convert.ToDouble(table.Rows[i]["BSJMONAMT09"].ToString());
                dBSJMONAMT10 = dBSJMONAMT10 + Convert.ToDouble(table.Rows[i]["BSJMONAMT10"].ToString());
                dBSJMONAMT11 = dBSJMONAMT11 + Convert.ToDouble(table.Rows[i]["BSJMONAMT11"].ToString());
                dBSJMONAMT12 = dBSJMONAMT12 + Convert.ToDouble(table.Rows[i]["BSJMONAMT12"].ToString());
                dBSJHFTOTAL = dBSJHFTOTAL + Convert.ToDouble(table.Rows[i]["BSJHFTOTAL"].ToString());
                dBSJAFTOTAL = dBSJAFTOTAL + Convert.ToDouble(table.Rows[i]["BSJAFTOTAL"].ToString());

                /******** 총계를 위한 Row 생성 **************/
                row = table.NewRow();
                table.Rows.InsertAt(row, i + 1);

                // 합 계 이름 넣기
                table.Rows[i + 1]["BSJDPAC"] = "[ 매출합계 ]";

                table.Rows[i + 1]["BSJMONAMT01"] = string.Format("{0:#,##0}", dBSJMONAMT01);
                table.Rows[i + 1]["BSJMONAMT02"] = string.Format("{0:#,##0}", dBSJMONAMT02);
                table.Rows[i + 1]["BSJMONAMT03"] = string.Format("{0:#,##0}", dBSJMONAMT03);
                table.Rows[i + 1]["BSJMONAMT04"] = string.Format("{0:#,##0}", dBSJMONAMT04);
                table.Rows[i + 1]["BSJMONAMT05"] = string.Format("{0:#,##0}", dBSJMONAMT05);
                table.Rows[i + 1]["BSJMONAMT06"] = string.Format("{0:#,##0}", dBSJMONAMT06);
                table.Rows[i + 1]["BSJMONAMT07"] = string.Format("{0:#,##0}", dBSJMONAMT07);
                table.Rows[i + 1]["BSJMONAMT08"] = string.Format("{0:#,##0}", dBSJMONAMT08);
                table.Rows[i + 1]["BSJMONAMT09"] = string.Format("{0:#,##0}", dBSJMONAMT09);
                table.Rows[i + 1]["BSJMONAMT10"] = string.Format("{0:#,##0}", dBSJMONAMT10);
                table.Rows[i + 1]["BSJMONAMT11"] = string.Format("{0:#,##0}", dBSJMONAMT11);
                table.Rows[i + 1]["BSJMONAMT12"] = string.Format("{0:#,##0}", dBSJMONAMT12);
                table.Rows[i + 1]["BSJHFTOTAL"] = string.Format("{0:#,##0}", dBSJHFTOTAL);
                table.Rows[i + 1]["BSJAFTOTAL"] = string.Format("{0:#,##0}", dBSJAFTOTAL);
                table.Rows[i + 1]["BSJMONTOTAL"] = string.Format("{0:#,##0}", dBSJHFTOTAL+dBSJAFTOTAL);

            }

            return table;
        }
        #endregion   

        #region Description : 최종 실적년월 가져오기
        private string  UP_Get_LastSJYYMM()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_7AKAW859");
            string sYYMM = this.DbConnector.ExecuteScalar().ToString();

            return sYYMM;
        }
        #endregion

        #region Description : 실적마감 체크
        private string UP_Get_SJCheckClose()
        {
            string sYN = string.Empty;

            sYN = "N";

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_7ANBD869", CBH01_BSJYYMM.GetValue().ToString(), CBH01_BSJYYMM.GetValue().ToString().Substring(0,4));
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sYN = dt.Rows[0]["BLJCHKMC"].ToString();
            }

            return sYN;
        }
        #endregion
    }
}



