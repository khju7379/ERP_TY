using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.HR00
{
    /// <summary>
    /// 주52시간 개인별 근태집계 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.08.12 15:31
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_98L92133 : 주52시간 달력 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_98L93134 : 주52시간 달력 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  KBSABUN : 사번
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRGT012S : TYBase
    {        

        #region  Description : 폼 로드 이벤트
        public TYHRGT012S()
        {
            InitializeComponent();
        }

        private void TYHRGT012S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_INQ.ProcessCheck += new TButton.CheckHandler(BTN61_INQ_ProcessCheck);

            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));
            this.DTP01_GIDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP02_GIDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.CBH01_KBBSTEAM.DummyValue = this.DTP01_SDATE.GetString();

            UP_Set_TitleSetting("1");
            UP_Set_TitleSetting("2");

            this.SetStartingFocus(DTP01_SDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            if (tabControl_INQ.SelectedIndex != 2)
            {
                tabControl_INQ.SelectedIndex = 0;

                this.UP_Set_TitleSetting("1");

                //집계 조회
                this.FPS91_TY_S_HR_98R9C142.Initialize();
                this.FPS91_TY_S_HR_98L93134.Initialize();
                this.FPS91_TY_S_HR_98RDP145.Initialize();
                CBH01_GISABUN.Initialize();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_98R9B141", this.DTP01_SDATE.GetString().ToString().Substring(0, 6) + "01", this.DTP01_SDATE.GetString().ToString().Substring(0, 6) + "01", CBH01_KBSABUN.GetValue().ToString(),
                                                            this.CBH01_KBBSTEAM.GetValue(), this.CBH01_KBJKCD.GetValue(), this.CBO01_INQOPTION.GetValue());
                this.FPS91_TY_S_HR_98R9C142.SetValue(this.DbConnector.ExecuteDataTable());

                ////개인별 달력 조회
                //if (CBH01_KBSABUN.GetValue().ToString() != "")
                //{
                //    this.UP_Set_TitleSetting("2");

                //    UP_Set_CalendarDataBinding(CBH01_KBSABUN.GetValue().ToString());               
                //}
            }

            if (tabControl_INQ.SelectedIndex == 2)
            {
                UP_Set_DayilDataBinding(DTP01_GIDATE.GetString().ToString(), DTP02_GIDATE.GetString().ToString(), CBH01_GISABUN.GetValue().ToString());
            }

        }
        private void BTN61_INQ_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (tabControl_INQ.TabIndex == 2)
            {
                if (DTP01_GIDATE.GetString().ToString() == "" )
                {
                    this.SetFocus(DTP01_GIDATE);
                    this.ShowCustomMessage("근태일자를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }

                if (DTP02_GIDATE.GetString().ToString() == "" )
                {
                    this.SetFocus(DTP02_GIDATE);
                    this.ShowCustomMessage("근태일자를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }

                if (CBH01_GISABUN.GetValue().ToString() == "")
                {
                    this.SetFocus(CBH01_GISABUN);
                    this.ShowCustomMessage("사번을 입력하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }
        }
        #endregion

        #region  Description : DataTable Merge 함수
        private DataTable UP_Set_DataMerge(DataTable dt)
        {
            DataTable dtMegre = UP_SetDataTable();

            DataRow row;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row = dtMegre.NewRow();
                row["GUBN"] = "DAY"; //날짜
                row["WEEK"] = i + 1;
                
                row["MON"] = dt.Rows[i]["MON"].ToString();
                row["TUE"] = dt.Rows[i]["TUE"].ToString();
                row["WED"] = dt.Rows[i]["WED"].ToString();
                row["THU"] = dt.Rows[i]["THU"].ToString();
                row["FRI"] = dt.Rows[i]["FRI"].ToString();
                row["SAT"] = dt.Rows[i]["SAT"].ToString();
                row["SUN"] = dt.Rows[i]["SUN"].ToString();
                row["TIMETOTAL"] = "";
                
                row["MONDAY"] = dt.Rows[i]["MONDAY"].ToString();
                row["TUEDAY"] = dt.Rows[i]["TUEDAY"].ToString();
                row["WEDDAY"] = dt.Rows[i]["WEDDAY"].ToString();
                row["THUDAY"] = dt.Rows[i]["THUDAY"].ToString();
                row["FRIDAY"] = dt.Rows[i]["FRIDAY"].ToString();
                row["SATDAY"] = dt.Rows[i]["SATDAY"].ToString();
                row["SUNDAY"] = dt.Rows[i]["SUNDAY"].ToString();

                
                row["MONDATE"] = dt.Rows[i]["MONDATE"].ToString();
                row["TUEDATE"] = dt.Rows[i]["TUEDATE"].ToString();
                row["WEDDATE"] = dt.Rows[i]["WEDDATE"].ToString();
                row["THUDATE"] = dt.Rows[i]["THUDATE"].ToString();
                row["FRIDATE"] = dt.Rows[i]["FRIDATE"].ToString();
                row["SATDATE"] = dt.Rows[i]["SATDATE"].ToString();
                row["SUNDATE"] = dt.Rows[i]["SUNDATE"].ToString();

                dtMegre.Rows.Add(row);

                row = dtMegre.NewRow();
                row["GUBN"] = "TIME"; //시간
                row["WEEK"] = i + 1;
                
                row["MON"] = dt.Rows[i]["TIMEMON"].ToString();
                row["TUE"] = dt.Rows[i]["TIMETUE"].ToString();
                row["WED"] = dt.Rows[i]["TIMEWED"].ToString();
                row["THU"] = dt.Rows[i]["TIMETHU"].ToString();
                row["FRI"] = dt.Rows[i]["TIMEFRI"].ToString();
                row["SAT"] = dt.Rows[i]["TIMESAT"].ToString();
                row["SUN"] = dt.Rows[i]["TIMESUN"].ToString();
                row["TIMETOTAL"] = dt.Rows[i]["TIMETOTAL"].ToString();                
                
                row["MONDAY"] = dt.Rows[i]["MONDAY"].ToString();
                row["TUEDAY"] = dt.Rows[i]["TUEDAY"].ToString();
                row["WEDDAY"] = dt.Rows[i]["WEDDAY"].ToString();
                row["THUDAY"] = dt.Rows[i]["THUDAY"].ToString();
                row["FRIDAY"] = dt.Rows[i]["FRIDAY"].ToString();
                row["SATDAY"] = dt.Rows[i]["SATDAY"].ToString();
                row["SUNDAY"] = dt.Rows[i]["SUNDAY"].ToString();
                
                row["MONDATE"] = dt.Rows[i]["MONDATE"].ToString();
                row["TUEDATE"] = dt.Rows[i]["TUEDATE"].ToString();
                row["WEDDATE"] = dt.Rows[i]["WEDDATE"].ToString();
                row["THUDATE"] = dt.Rows[i]["THUDATE"].ToString();
                row["FRIDATE"] = dt.Rows[i]["FRIDATE"].ToString();
                row["SATDATE"] = dt.Rows[i]["SATDATE"].ToString();
                row["SUNDATE"] = dt.Rows[i]["SUNDATE"].ToString();

                dtMegre.Rows.Add(row);                
            }

            return dtMegre;
        }
        #endregion

        #region  Description : DataTable 만들기
        private DataTable UP_SetDataTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("GUBN", typeof(System.String));
            dt.Columns.Add("WEEK", typeof(System.Int16));
            dt.Columns.Add("SUN", typeof(System.String));
            dt.Columns.Add("MON", typeof(System.String));
            dt.Columns.Add("TUE", typeof(System.String));
            dt.Columns.Add("WED", typeof(System.String));
            dt.Columns.Add("THU", typeof(System.String));
            dt.Columns.Add("FRI", typeof(System.String));
            dt.Columns.Add("SAT", typeof(System.String));
            dt.Columns.Add("TIMETOTAL", typeof(System.String));

            dt.Columns.Add("SUNDAY", typeof(System.String));
            dt.Columns.Add("MONDAY", typeof(System.String));
            dt.Columns.Add("TUEDAY", typeof(System.String));
            dt.Columns.Add("WEDDAY", typeof(System.String));
            dt.Columns.Add("THUDAY", typeof(System.String));
            dt.Columns.Add("FRIDAY", typeof(System.String));
            dt.Columns.Add("SATDAY", typeof(System.String));

            dt.Columns.Add("SUNDATE", typeof(System.String));
            dt.Columns.Add("MONDATE", typeof(System.String));
            dt.Columns.Add("TUEDATE", typeof(System.String));
            dt.Columns.Add("WEDDATE", typeof(System.String));
            dt.Columns.Add("THUDATE", typeof(System.String));
            dt.Columns.Add("FRIDATE", typeof(System.String));
            dt.Columns.Add("SATDATE", typeof(System.String));

            dt.TableName = "TableNames";

            return dt;
        }
        #endregion

        #region Description :  그리드 타이트 셋팅 함수
        private void UP_Set_TitleSetting(string Gubn)
        {

            if (Gubn == "1")
            {
                this.FPS91_TY_S_HR_98R9C142_Sheet1.ColumnHeaderRowCount = 2;
                this.FPS91_TY_S_HR_98R9C142_Sheet1.RowHeaderColumnCount = 1;

                this.FPS91_TY_S_HR_98R9C142_Sheet1.AddColumnHeaderSpanCell(0, 0, 1, 11);

                this.FPS91_TY_S_HR_98R9C142_Sheet1.ColumnHeader.Cells[0, 0].Value = DTP01_SDATE.GetString().ToString().Substring(0, 4) + "년 " + DTP01_SDATE.GetString().ToString().Substring(4, 2) + "월";

                this.FPS91_TY_S_HR_98R9C142_Sheet1.ColumnHeader.Cells[1, 0].Value = "키";
                this.FPS91_TY_S_HR_98R9C142_Sheet1.ColumnHeader.Cells[1, 1].Value = "부 서";
                this.FPS91_TY_S_HR_98R9C142_Sheet1.ColumnHeader.Cells[1, 2].Value = "부서명";
                this.FPS91_TY_S_HR_98R9C142_Sheet1.ColumnHeader.Cells[1, 3].Value = "사 번";
                this.FPS91_TY_S_HR_98R9C142_Sheet1.ColumnHeader.Cells[1, 4].Value = "이 름";
                this.FPS91_TY_S_HR_98R9C142_Sheet1.ColumnHeader.Cells[1, 5].Value = "직 급";
                this.FPS91_TY_S_HR_98R9C142_Sheet1.ColumnHeader.Cells[1, 6].Value = "직 급";
                this.FPS91_TY_S_HR_98R9C142_Sheet1.ColumnHeader.Cells[1, 7].Value = "주 차";
                this.FPS91_TY_S_HR_98R9C142_Sheet1.ColumnHeader.Cells[1, 8].Value = "주 시작일";
                this.FPS91_TY_S_HR_98R9C142_Sheet1.ColumnHeader.Cells[1, 9].Value = "주 종료일";
                this.FPS91_TY_S_HR_98R9C142_Sheet1.ColumnHeader.Cells[1, 10].Value = "주 근무시간";

                this.FPS91_TY_S_HR_98R9C142_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            }

            if (Gubn == "2")
            {
                this.FPS91_TY_S_HR_98L93134_Sheet1.ColumnHeaderRowCount = 2;
                this.FPS91_TY_S_HR_98L93134_Sheet1.RowHeaderColumnCount = 1;

                this.FPS91_TY_S_HR_98L93134_Sheet1.AddColumnHeaderSpanCell(0, 0, 1, 16);

                this.FPS91_TY_S_HR_98L93134_Sheet1.ColumnHeader.Cells[0, 0].Value = DTP01_SDATE.GetString().ToString().Substring(0, 4) + "년 " + DTP01_SDATE.GetString().ToString().Substring(4, 2) + "월" +" : " +
                                                                                    CBH01_KBSABUN.GetValue().ToString() + "  " +CBH01_KBSABUN.GetText().ToString();

                
                this.FPS91_TY_S_HR_98L93134_Sheet1.ColumnHeader.Cells[1, 2].Value = "월요일(MON)";
                this.FPS91_TY_S_HR_98L93134_Sheet1.ColumnHeader.Cells[1, 3].Value = "화요일(TUE)";
                this.FPS91_TY_S_HR_98L93134_Sheet1.ColumnHeader.Cells[1, 4].Value = "수요일(WED)";
                this.FPS91_TY_S_HR_98L93134_Sheet1.ColumnHeader.Cells[1, 5].Value = "목요일(THU)";
                this.FPS91_TY_S_HR_98L93134_Sheet1.ColumnHeader.Cells[1, 6].Value = "금요일(FRI)";
                this.FPS91_TY_S_HR_98L93134_Sheet1.ColumnHeader.Cells[1, 7].Value = "토요일(SAT)";
                this.FPS91_TY_S_HR_98L93134_Sheet1.ColumnHeader.Cells[1, 8].Value = "일요일(SUN)";
                this.FPS91_TY_S_HR_98L93134_Sheet1.ColumnHeader.Cells[1, 9].Value = "총근무시간";

                this.FPS91_TY_S_HR_98L93134_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            }

        }
        #endregion

        #region Description :  FPS91_TY_S_HR_98R9C142_CellDoubleClick 이벤트
        private void FPS91_TY_S_HR_98R9C142_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            CBH01_KBSABUN.SetValue(this.FPS91_TY_S_HR_98R9C142.GetValue("KBSABUN").ToString());

            UP_Set_CalendarDataBinding(this.FPS91_TY_S_HR_98R9C142.GetValue("KBSABUN").ToString());

            tabControl_INQ.SelectedIndex = 1;            
        }
        #endregion

        #region Description :  UP_Set_CalendarDataBinding 이벤트
        private void UP_Set_CalendarDataBinding(string sKBSABUN)
        {
            int iHeight = 0;
            int iFontSize = 0;

            UP_Set_TitleSetting("2");

            this.FPS91_TY_S_HR_98L93134.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_98L92133", this.DTP01_SDATE.GetString().ToString().Substring(0, 6) + "01", this.DTP01_SDATE.GetString().ToString().Substring(0, 6) + "01", sKBSABUN);
            this.FPS91_TY_S_HR_98L93134.SetValue(UP_Set_DataMerge(this.DbConnector.ExecuteDataTable()));

            if (this.FPS91_TY_S_HR_98L93134.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_HR_98L93134.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_HR_98L93134.GetValue(i, "GUBN").ToString() == "DAY")
                    {
                        iHeight = 80;
                        iFontSize = 15;
                    }
                    else
                    {
                        iHeight = 30;
                        iFontSize = 12;
                    }

                    this.FPS91_TY_S_HR_98L93134.ActiveSheet.Rows[i].Height = iHeight;

                    for (int j = 0; j < 10; j++)
                    {
                        if (this.FPS91_TY_S_HR_98L93134.GetValue(i, "GUBN").ToString() == "DAY")
                        {
                            if (this.FPS91_TY_S_HR_98L93134.GetValue(i, "WEEK").ToString() == "1" && (j >= 2 && j <= 8))
                            {
                                //전월 표시
                                if (Convert.ToInt16(this.FPS91_TY_S_HR_98L93134_Sheet1.Cells[i, j].Text.Trim()) > 9)
                                {
                                    this.FPS91_TY_S_HR_98L93134_Sheet1.Cells[i, j].Font = new Font("굴림체", 13, FontStyle.Bold);
                                    this.FPS91_TY_S_HR_98L93134_Sheet1.Cells[i, j].ForeColor = Color.Gray;
                                }
                                else
                                {
                                    this.FPS91_TY_S_HR_98L93134_Sheet1.Cells[i, j].Font = new Font("굴림체", iFontSize, FontStyle.Bold);
                                }
                            }
                            else
                            {
                                //익월 표시 
                                if (i > 3 && (j >= 2 && j <= 8) && Convert.ToInt16(this.FPS91_TY_S_HR_98L93134_Sheet1.Cells[i, j].Text.Trim()) < 9)
                                {
                                    this.FPS91_TY_S_HR_98L93134_Sheet1.Cells[i, j].Font = new Font("굴림체", 13, FontStyle.Bold);
                                    this.FPS91_TY_S_HR_98L93134_Sheet1.Cells[i, j].ForeColor = Color.Gray;
                                }
                                else
                                {
                                    this.FPS91_TY_S_HR_98L93134_Sheet1.Cells[i, j].Font = new Font("굴림체", iFontSize, FontStyle.Bold);
                                }
                            }

                            //휴일코드가 있으면 날짜를 붉은색으로 한다.
                            if (j >= 2 && j <= 8)
                            {
                                if (this.FPS91_TY_S_HR_98L93134_Sheet1.Cells[i, j + 8].Text.Trim() != "")
                                {
                                    this.FPS91_TY_S_HR_98L93134_Sheet1.Cells[i, j].ForeColor = Color.Red;
                                }
                            }
                        }
                        else
                        {
                            this.FPS91_TY_S_HR_98L93134_Sheet1.Cells[i, j].Font = new Font("굴림체", iFontSize);
                            this.FPS91_TY_S_HR_98L93134_Sheet1.Cells[i, j].ForeColor = Color.Crimson;
                        }
                    }
                }
            }
        }
        #endregion

        #region Description :  FPS91_TY_S_HR_98L93134_CellDoubleClick 이벤트
        private void FPS91_TY_S_HR_98L93134_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sEnDate = string.Empty;

            if (this.FPS91_TY_S_HR_98L93134.GetValue("MONDATE").ToString() == "")
            {
                sEnDate = this.FPS91_TY_S_HR_98L93134.GetValue("SUNDATE").ToString();
            }
            else if (this.FPS91_TY_S_HR_98L93134.GetValue("TUEDATE").ToString() == "")
            {
                sEnDate = this.FPS91_TY_S_HR_98L93134.GetValue("MONDATE").ToString();
            }
            else if (this.FPS91_TY_S_HR_98L93134.GetValue("WEDDATE").ToString() == "")
            {
                sEnDate = this.FPS91_TY_S_HR_98L93134.GetValue("TUEDATE").ToString();
            }
            else if (this.FPS91_TY_S_HR_98L93134.GetValue("THUDATE").ToString() == "")
            {
                sEnDate = this.FPS91_TY_S_HR_98L93134.GetValue("WEDDATE").ToString();
            }
            else if (this.FPS91_TY_S_HR_98L93134.GetValue("FRIDATE").ToString() == "")
            {
                sEnDate = this.FPS91_TY_S_HR_98L93134.GetValue("THUDATE").ToString();
            }
            else if (this.FPS91_TY_S_HR_98L93134.GetValue("SATDATE").ToString() == "")
            {
                sEnDate = this.FPS91_TY_S_HR_98L93134.GetValue("FRIDATE").ToString();
            }
            else
            {
                sEnDate = this.FPS91_TY_S_HR_98L93134.GetValue("SATDATE").ToString();
            }

            UP_Set_DayilDataBinding(this.FPS91_TY_S_HR_98L93134.GetValue("MONDATE").ToString(), this.FPS91_TY_S_HR_98L93134.GetValue("SUNDATE").ToString(), this.FPS91_TY_S_HR_98R9C142.GetValue("KBSABUN").ToString());
        }
        #endregion

        #region Description :  UP_Set_DayilDataBinding 이벤트
        private void UP_Set_DayilDataBinding(string sSDATE, string sEDATE, string sKBSABUN)
        {
            tabControl_INQ.SelectedIndex = 2;

            DTP01_GIDATE.SetValue(sSDATE);
            DTP02_GIDATE.SetValue(sEDATE);

            CBH01_GISABUN.SetValue(sKBSABUN);

            this.FPS91_TY_S_HR_98RDP145.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_98RDO144", sSDATE, sEDATE, sKBSABUN);
            this.FPS91_TY_S_HR_98RDP145.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_HR_98RDP145.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_HR_98RDP145, "KBHANGL", "합  계", SumRowType.Sum, "VW_TOTALTIME");
            }
        }
        #endregion
    }
}
