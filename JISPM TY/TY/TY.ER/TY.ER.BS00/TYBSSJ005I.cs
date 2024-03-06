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
    /// 당기실적 영업비용 관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.09.15 11:56
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_79FED601 : 당기실적 영업비용 자료 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_79FEE602 : 당기실적 영업비용관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  SAV : 저장
    ///  BCJADAC : 계정과목
    ///  BCJDPAC : 귀속부서
    ///  BSJYYMM : 실적생성년월
    /// </summary>
    public partial class TYBSSJ005I : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYBSSJ005I()
        {
            InitializeComponent();
        }

        private void TYBSSJ005I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            CBH01_BCJDPAC.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            CBH01_BSJYYMM.SetValue(UP_Get_LastSJYYMM());

            if (UP_Get_SJCheckClose() == "Y")
            {
                this.BTN61_NEW.SetReadOnly(true);
                this.BTN61_SAV.SetReadOnly(true);
                this.BTN61_REM.SetReadOnly(true);
            } 

            this.SetStartingFocus(CBH01_BSJYYMM);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            
            this.UP_Set_TitleSetting();

            this.FPS91_TY_S_AC_79FEE602.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_79FED601", this.CBH01_BSJYYMM.GetValue().ToString(), CBH01_BCJDPAC.GetValue(), CBH01_BCJADAC.GetValue(), "PR");
            this.FPS91_TY_S_AC_79FEE602.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_79FEE602.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_AC_79FEE602.CurrentRowCount; i++)
                {
                    //귀속부서 총 합계라인
                    if (this.FPS91_TY_S_AC_79FEE602.GetValue(i, "BCJLNGUBN").ToString() == "0" && this.FPS91_TY_S_AC_79FEE602.GetValue(i, "BCGWCODE").ToString() == "0")
                    {
                        this.FPS91_TY_S_AC_79FEE602.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);

                        for (int j = 19; j < 32; j++)
                        {
                            this.FPS91_TY_S_AC_79FEE602_Sheet1.Cells[i, j].ForeColor = Color.Black;
                            this.FPS91_TY_S_AC_79FEE602_Sheet1.Cells[i, j].Locked = true;
                        }
                    }
                    else if (this.FPS91_TY_S_AC_79FEE602.GetValue(i, "BCJLNGUBN").ToString() == "1" &&  this.FPS91_TY_S_AC_79FEE602.GetValue(i, "BCGWCODE").ToString() == "1" )  //계정과목 합계 라인
                    {
                        this.FPS91_TY_S_AC_79FEE602.ActiveSheet.Rows[i].BackColor = Color.FromArgb(228, 242, 194);

                        for (int j = 19; j < 32; j++)
                        {
                            this.FPS91_TY_S_AC_79FEE602_Sheet1.Cells[i, j].ForeColor = Color.Black;
                            this.FPS91_TY_S_AC_79FEE602_Sheet1.Cells[i, j].Locked = true;
                        }

                    }
                    else if (this.FPS91_TY_S_AC_79FEE602.GetValue(i, "BCJLNGUBN").ToString() == "1" && this.FPS91_TY_S_AC_79FEE602.GetValue(i, "BCGWCODE").ToString() == "S")  //계정과목 합계 라인
                    {
                        for (int j = 13; j < 34; j++)
                        {
                            this.FPS91_TY_S_AC_79FEE602_Sheet1.Cells[i, j].BackColor = Color.FromArgb(218, 239, 244);
                        }

                        for (int j = 19; j < 32; j++)
                        {
                            this.FPS91_TY_S_AC_79FEE602_Sheet1.Cells[i, j].ForeColor = Color.Black;
                            this.FPS91_TY_S_AC_79FEE602_Sheet1.Cells[i, j].Locked = true;
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

                        for (int j = 19; j < (19 + iAddMonth); j++)
                        {
                            this.FPS91_TY_S_AC_79FEE602_Sheet1.Cells[i, j].ForeColor = Color.Black;
                            this.FPS91_TY_S_AC_79FEE602_Sheet1.Cells[i, j].Locked = true;
                        }

                        //수정가능월 글자 bold 처리
                        for (int j = (19 + iAddMonth); j < (19 + iAddMonth) + (this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Columns.Count - (19 + iAddMonth)); j++)
                        {
                            if (this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[1, j].Value.ToString() != "소 계" && this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[1, j].Value.ToString() != "합 계")
                            {
                                this.FPS91_TY_S_AC_79FEE602_Sheet1.Cells[i, j].Font = new Font("굴림", 9, FontStyle.Bold);
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
                if (ds.Tables[0].Rows[i]["BCJADAC"].ToString() != "")
                {
                    this.DbConnector.Attach("TY_P_AC_79FHT610", ds.Tables[0].Rows[i]["BCJMONAMT01"].ToString(),
                                                                ds.Tables[0].Rows[i]["BCJMONAMT02"].ToString(),
                                                                ds.Tables[0].Rows[i]["BCJMONAMT03"].ToString(),
                                                                ds.Tables[0].Rows[i]["BCJMONAMT04"].ToString(),
                                                                ds.Tables[0].Rows[i]["BCJMONAMT05"].ToString(),
                                                                ds.Tables[0].Rows[i]["BCJMONAMT06"].ToString(),
                                                                ds.Tables[0].Rows[i]["BCJMONAMT07"].ToString(),
                                                                ds.Tables[0].Rows[i]["BCJMONAMT08"].ToString(),
                                                                ds.Tables[0].Rows[i]["BCJMONAMT09"].ToString(),
                                                                ds.Tables[0].Rows[i]["BCJMONAMT10"].ToString(),
                                                                ds.Tables[0].Rows[i]["BCJMONAMT11"].ToString(),
                                                                ds.Tables[0].Rows[i]["BCJMONAMT12"].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[0].Rows[i]["BCJYYMM"].ToString(),
                                                                ds.Tables[0].Rows[i]["BCJFORM"].ToString(),
                                                                ds.Tables[0].Rows[i]["BCJYEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["BCJDPAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["BCJADAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["BCJCDAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["BCJITEM"].ToString(),
                                                                ds.Tables[0].Rows[i]["BCJLNGUBN"].ToString()
                                                                );
                }


            }
            if (this.DbConnector.CommandCount > 0)
                this.DbConnector.ExecuteTranQueryList();

            this.DbConnector.CommandClear();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //소계,합계라인은 제외
                if (ds.Tables[0].Rows[i]["BCJADAC"].ToString() != "")
                {

                    //항목하위 내역이 있으면 항목 집계 UPDATE
                    if (ds.Tables[0].Rows[i]["BCJLNGUBN"].ToString() == "2")
                    {
                        this.DbConnector.Attach("TY_P_AC_7BEBF988",
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[0].Rows[i]["BCJYYMM"].ToString(),
                                                                ds.Tables[0].Rows[i]["BCJFORM"].ToString(),
                                                                ds.Tables[0].Rows[i]["BCJYEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["BCJDPAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["BCJADAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["BCJCDAC"].ToString(),
                                                                "1");
                    }
                }


            }
            if (this.DbConnector.CommandCount > 0)
                this.DbConnector.ExecuteTranQueryList();

            //합계 정리
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_79FHT611", TYUserInfo.EmpNo, CBH01_BSJYYMM.GetValue().ToString());
            this.DbConnector.ExecuteTranQuery();

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD873");
        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_79FEE602.GetDataSourceInclude(TSpread.TActionType.Update, "BCJYYMM", "BCJFORM", "BCJYEAR", "BCJDPAC", "BCJADAC", "BCJCDAC","BCJITEM", "BCJLNGUBN", "BCJMONAMT01", "BCJMONAMT02", "BCJMONAMT03", "BCJMONAMT04", "BCJMONAMT05", "BCJMONAMT06", "BCJMONAMT07", "BCJMONAMT08", "BCJMONAMT09", "BCJMONAMT10", "BCJMONAMT11", "BCJMONAMT12"));

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

        #region  Description :  CBH01_BSJYYMM_CodeBoxDataBinded 이벤트
        private void CBH01_BSJYYMM_CodeBoxDataBinded(object sender, EventArgs e)
        {
            CBH01_BCJDPAC.DummyValue = CBH01_BSJYYMM.GetValue().ToString() + "01";
        }
        #endregion

        #region Description :  그리드 타이트 셋팅 함수
        private void UP_Set_TitleSetting()
        {
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_79FEE602_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_79FEE602_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_AC_79FEE602_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_AC_79FEE602_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);
            this.FPS91_TY_S_AC_79FEE602_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);
            this.FPS91_TY_S_AC_79FEE602_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1);
            this.FPS91_TY_S_AC_79FEE602_Sheet1.AddColumnHeaderSpanCell(0, 5, 2, 1);
            this.FPS91_TY_S_AC_79FEE602_Sheet1.AddColumnHeaderSpanCell(0, 6, 2, 1);
            this.FPS91_TY_S_AC_79FEE602_Sheet1.AddColumnHeaderSpanCell(0, 7, 2, 1);
            this.FPS91_TY_S_AC_79FEE602_Sheet1.AddColumnHeaderSpanCell(0, 8, 2, 1);
            this.FPS91_TY_S_AC_79FEE602_Sheet1.AddColumnHeaderSpanCell(0, 9, 2, 1);
            this.FPS91_TY_S_AC_79FEE602_Sheet1.AddColumnHeaderSpanCell(0, 10, 2, 1);
            this.FPS91_TY_S_AC_79FEE602_Sheet1.AddColumnHeaderSpanCell(0, 11, 2, 1);
            this.FPS91_TY_S_AC_79FEE602_Sheet1.AddColumnHeaderSpanCell(0, 12, 2, 1);
            this.FPS91_TY_S_AC_79FEE602_Sheet1.AddColumnHeaderSpanCell(0, 13, 2, 1);
            this.FPS91_TY_S_AC_79FEE602_Sheet1.AddColumnHeaderSpanCell(0, 14, 2, 1);
            this.FPS91_TY_S_AC_79FEE602_Sheet1.AddColumnHeaderSpanCell(0, 15, 2, 1);
            this.FPS91_TY_S_AC_79FEE602_Sheet1.AddColumnHeaderSpanCell(0, 16, 2, 1);
            this.FPS91_TY_S_AC_79FEE602_Sheet1.AddColumnHeaderSpanCell(0, 17, 2, 1);
            this.FPS91_TY_S_AC_79FEE602_Sheet1.AddColumnHeaderSpanCell(0, 18, 2, 1);

            this.FPS91_TY_S_AC_79FEE602_Sheet1.AddColumnHeaderSpanCell(0, 33, 2, 1);            

            this.FPS91_TY_S_AC_79FEE602_Sheet1.AddColumnHeaderSpanCell(0, 19, 1, 10);  //1월 ~ 9월
            this.FPS91_TY_S_AC_79FEE602_Sheet1.AddColumnHeaderSpanCell(0, 29, 1, 4);  //10월 ~ 12월

            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[0, 0].Value = "실적생성년월";
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[0, 1].Value = "양식구분";
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[0, 2].Value = "사업년도";
            
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[0, 3].Value = "귀속부서";
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[0, 4].Value = "귀속부서";
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[0, 5].Value = "계정과목";
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[0, 6].Value = "계정과목";
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[0, 7].Value = "계정세목";
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[0, 8].Value = "계정세목";

            //그리드 표현용 타이틀
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[0, 9].Value = "귀속부서";
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[0, 10].Value = "귀속부서";
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[0, 11].Value = "계정과목";
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[0, 12].Value = "계정과목";
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[0, 13].Value = "계정세목";
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[0, 14].Value = "계정세목";

            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[0, 15].Value = "항 목";
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[0, 16].Value = "항 목 명";
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[0, 17].Value = "라인구분";
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[0, 18].Value = "구분";

            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[0, 19].Value = this.CBH01_BSJYYMM.GetValue().ToString().Substring(0, 4) + "년 1월 ~ 9월";
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[1, 19].Value = "1월";
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[1, 20].Value = "2월";
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[1, 21].Value = "3월";
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[1, 22].Value = "4월";
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[1, 23].Value = "5월";
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[1, 24].Value = "6월";
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[1, 25].Value = "7월";
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[1, 26].Value = "8월";
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[1, 27].Value = "9월";
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[1, 28].Value = "소 계";

            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[0, 29].Value = this.CBH01_BSJYYMM.GetValue().ToString().Substring(0, 4) + "년 10월 ~ 12월";
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[1, 29].Value = "10월";
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[1, 30].Value = "11월";
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[1, 31].Value = "12월";
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[1, 32].Value = "소 계";

            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[1, 33].Value = "합 계";

            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[0, 19].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_79FEE602_Sheet1.ColumnHeader.Cells[0, 29].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

        }
        #endregion

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYBSSJ004P(CBH01_BSJYYMM.GetValue().ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_7ANGY873", dt.Rows[i]["BCJYYMM"].ToString(),
                                                            dt.Rows[i]["BCJFORM"].ToString(),
                                                            dt.Rows[i]["BCJYEAR"].ToString(),
                                                            dt.Rows[i]["BCJDPAC"].ToString(),
                                                            dt.Rows[i]["BCJADAC"].ToString(),
                                                            dt.Rows[i]["BCJCDAC"].ToString(),
                                                            dt.Rows[i]["BCJITEM"].ToString(),
                                                            dt.Rows[i]["BCJLNGUBN"].ToString()
                                                            );
            }
            if (this.DbConnector.CommandCount > 0)
                this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);
        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            
            DataTable dt = this.FPS91_TY_S_AC_79FEE602.GetDataSourceInclude(TSpread.TActionType.Remove, "BCJYYMM", "BCJFORM", "BCJYEAR", "BCJDPAC", "BCJADAC", "BCJCDAC", "BCJITEM", "BCJLNGUBN");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (UP_Get_SJCheckClose() == "Y")
            {
                this.ShowCustomMessage("당기실적이 마감되었습니다! 수정할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region Description : 최종 실적년월 가져오기
        private string UP_Get_LastSJYYMM()
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
            this.DbConnector.Attach("TY_P_AC_7ANBD869", CBH01_BSJYYMM.GetValue().ToString(), CBH01_BSJYYMM.GetValue().ToString().Substring(0, 4));
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sYN = dt.Rows[0]["BLJCHKPR"].ToString();
            }

            return sYN;
        }
        #endregion
    }
}
