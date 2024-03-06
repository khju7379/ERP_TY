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
    /// 당기실적 투자.수선 관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.09.19 08:35
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_79JBI615 : 당기실적 투자.수선 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_79JBK616 : 당기실적 투자.수선관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  SAV : 저장
    ///  BSJYYMM : 실적생성년월
    ///  BVJCDAC : 계정과목
    ///  BVJDPAC : 귀속부서
    ///  INQOPTION : 조회구분
    /// </summary>
    public partial class TYBSSJ009I : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYBSSJ009I()
        {
            InitializeComponent();
        }

        private void TYBSSJ009I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            CBH01_BIJYYMM.SetValue(UP_Get_LastSJYYMM());
            
            this.SetStartingFocus(CBH01_BIJYYMM);

        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            UP_Set_TitleSetting();

            this.FPS91_TY_S_AC_79PHS670.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_79PHQ669", this.CBH01_BIJYYMM.GetValue().ToString() );
            this.FPS91_TY_S_AC_79PHS670.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_79PHS670.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_AC_79PHS670.CurrentRowCount; i++)
                {
                    //수선비는 실적월까지 수정할수 없다
                    if (this.FPS91_TY_S_AC_79PHS670.GetValue(i, "SORTGN").ToString() != "2")
                    {
                        int iAddMonth = Convert.ToInt16(this.CBH01_BIJYYMM.GetValue().ToString().Substring(4, 2));

                        if (iAddMonth > 9)
                        {
                            iAddMonth = iAddMonth + 1;
                        }

                        for (int j = 7; j < (7 + iAddMonth); j++)
                        {
                            this.FPS91_TY_S_AC_79PHS670_Sheet1.Cells[i, j].ForeColor = Color.Black;
                            this.FPS91_TY_S_AC_79PHS670_Sheet1.Cells[i, j].Locked = true;
                        }

                        //수정가능월 글자 bold 처리
                        for (int j = (7 + iAddMonth); j < (7 + iAddMonth) + (this.FPS91_TY_S_AC_79PHS670_Sheet1.ColumnHeader.Columns.Count - (7 + iAddMonth)); j++)
                        {
                            if (this.FPS91_TY_S_AC_79PHS670_Sheet1.ColumnHeader.Cells[0, j].Value.ToString() != "소 계" && this.FPS91_TY_S_AC_79PHS670_Sheet1.ColumnHeader.Cells[0, j].Value.ToString() != "합 계")
                            {
                                this.FPS91_TY_S_AC_79PHS670_Sheet1.Cells[i, j].Font = new Font("굴림", 9, FontStyle.Bold);
                            }
                        }
                    }

                    if (this.FPS91_TY_S_AC_79PHS670.GetValue(i, "SORTGN").ToString() == "2")
                    {
                        for (int j = 6; j < 22; j++)
                        {
                            this.FPS91_TY_S_AC_79PHS670_Sheet1.Cells[i, j].BackColor = Color.FromArgb(218, 239, 244);
                        }

                        for (int j = 7; j < 22; j++)
                        {
                            this.FPS91_TY_S_AC_79PHS670_Sheet1.Cells[i, j].ForeColor = Color.Black;
                            this.FPS91_TY_S_AC_79PHS670_Sheet1.Cells[i, j].Locked = true;
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
                //소계,합계,총계 라인은 제외
                if (ds.Tables[0].Rows[i]["SORTGN"].ToString() != "0" )
                {
                    this.DbConnector.Attach("TY_P_AC_79QGB674", ds.Tables[0].Rows[i]["BIJMONAMT01"].ToString(),
                                                                ds.Tables[0].Rows[i]["BIJMONAMT02"].ToString(),
                                                                ds.Tables[0].Rows[i]["BIJMONAMT03"].ToString(),
                                                                ds.Tables[0].Rows[i]["BIJMONAMT04"].ToString(),
                                                                ds.Tables[0].Rows[i]["BIJMONAMT05"].ToString(),
                                                                ds.Tables[0].Rows[i]["BIJMONAMT06"].ToString(),
                                                                ds.Tables[0].Rows[i]["BIJMONAMT07"].ToString(),
                                                                ds.Tables[0].Rows[i]["BIJMONAMT08"].ToString(),
                                                                ds.Tables[0].Rows[i]["BIJMONAMT09"].ToString(),
                                                                ds.Tables[0].Rows[i]["BIJMONAMT10"].ToString(),
                                                                ds.Tables[0].Rows[i]["BIJMONAMT11"].ToString(),
                                                                ds.Tables[0].Rows[i]["BIJMONAMT12"].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[0].Rows[i]["BIJYYMM"].ToString(),
                                                                ds.Tables[0].Rows[i]["BIJYEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["BIJCDAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["BIJDPAC"].ToString()
                                                                );
                }


            }
            if (this.DbConnector.CommandCount > 0)
                this.DbConnector.ExecuteTranQueryList();

            //합계 정리
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_79QGC675", TYUserInfo.EmpNo, CBH01_BIJYYMM.GetValue().ToString());
            this.DbConnector.ExecuteTranQuery();

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD873");
        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_AC_79PHS670.GetDataSourceInclude(TSpread.TActionType.Update, "BIJYYMM", "BIJYEAR", "BIJCDAC", "BIJDPAC", "BIJMONAMT01", "BIJMONAMT02", "BIJMONAMT03", "BIJMONAMT04", "BIJMONAMT05", "BIJMONAMT06", "BIJMONAMT07", "BIJMONAMT08", "BIJMONAMT09", "BIJMONAMT10", "BIJMONAMT11", "BIJMONAMT12","SORTGN"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
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
            this.FPS91_TY_S_AC_79PHS670_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_79PHS670_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_79PHS670_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_AC_79PHS670_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_AC_79PHS670_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);
            this.FPS91_TY_S_AC_79PHS670_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);
            this.FPS91_TY_S_AC_79PHS670_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1);
            this.FPS91_TY_S_AC_79PHS670_Sheet1.AddColumnHeaderSpanCell(0, 5, 2, 1);
            this.FPS91_TY_S_AC_79PHS670_Sheet1.AddColumnHeaderSpanCell(0, 6, 2, 1);

            this.FPS91_TY_S_AC_79PHS670_Sheet1.AddColumnHeaderSpanCell(0, 21, 2, 1);           
            
            this.FPS91_TY_S_AC_79PHS670_Sheet1.ColumnHeader.Cells[0, 0].Value = "실적생성년월";
            this.FPS91_TY_S_AC_79PHS670_Sheet1.ColumnHeader.Cells[0, 1].Value = "귀속년도";
            this.FPS91_TY_S_AC_79PHS670_Sheet1.ColumnHeader.Cells[0, 2].Value = "계정과목";
            this.FPS91_TY_S_AC_79PHS670_Sheet1.ColumnHeader.Cells[0, 3].Value = "계정과목";
            this.FPS91_TY_S_AC_79PHS670_Sheet1.ColumnHeader.Cells[0, 4].Value = "계 정 명";
            this.FPS91_TY_S_AC_79PHS670_Sheet1.ColumnHeader.Cells[0, 5].Value = "귀속부서";
            this.FPS91_TY_S_AC_79PHS670_Sheet1.ColumnHeader.Cells[0, 6].Value = "귀속부서";


            this.FPS91_TY_S_AC_79PHS670_Sheet1.AddColumnHeaderSpanCell(0, 7, 1, 10);  //1월 ~ 9월            
            this.FPS91_TY_S_AC_79PHS670_Sheet1.AddColumnHeaderSpanCell(0, 17, 1, 4);  //10월 ~ 12월            

            this.FPS91_TY_S_AC_79PHS670_Sheet1.ColumnHeader.Cells[0, 7].Value = this.CBH01_BIJYYMM.GetValue().ToString().Substring(0, 4) + "년 1월 ~ 9월";
            this.FPS91_TY_S_AC_79PHS670_Sheet1.ColumnHeader.Cells[1, 7].Value = "1월";
            this.FPS91_TY_S_AC_79PHS670_Sheet1.ColumnHeader.Cells[1, 8].Value = "2월";
            this.FPS91_TY_S_AC_79PHS670_Sheet1.ColumnHeader.Cells[1, 9].Value = "3월";
            this.FPS91_TY_S_AC_79PHS670_Sheet1.ColumnHeader.Cells[1, 10].Value = "4월";
            this.FPS91_TY_S_AC_79PHS670_Sheet1.ColumnHeader.Cells[1, 11].Value = "5월";
            this.FPS91_TY_S_AC_79PHS670_Sheet1.ColumnHeader.Cells[1, 12].Value = "6월";
            this.FPS91_TY_S_AC_79PHS670_Sheet1.ColumnHeader.Cells[1, 13].Value = "7월";
            this.FPS91_TY_S_AC_79PHS670_Sheet1.ColumnHeader.Cells[1, 14].Value = "8월";
            this.FPS91_TY_S_AC_79PHS670_Sheet1.ColumnHeader.Cells[1, 15].Value = "9월";
            this.FPS91_TY_S_AC_79PHS670_Sheet1.ColumnHeader.Cells[1, 16].Value = "소 계";
            this.FPS91_TY_S_AC_79PHS670_Sheet1.ColumnHeader.Cells[0, 17].Value = this.CBH01_BIJYYMM.GetValue().ToString().Substring(0, 4) + "년 10월 ~ 12월";
            this.FPS91_TY_S_AC_79PHS670_Sheet1.ColumnHeader.Cells[1, 17].Value = "10월";
            this.FPS91_TY_S_AC_79PHS670_Sheet1.ColumnHeader.Cells[1, 18].Value = "11월";
            this.FPS91_TY_S_AC_79PHS670_Sheet1.ColumnHeader.Cells[1, 19].Value = "12월";
            this.FPS91_TY_S_AC_79PHS670_Sheet1.ColumnHeader.Cells[1, 20].Value = "소 계";
            this.FPS91_TY_S_AC_79PHS670_Sheet1.ColumnHeader.Cells[1, 21].Value = "합 계";
            
            this.FPS91_TY_S_AC_79PHS670_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_79PHS670_Sheet1.ColumnHeader.Cells[0, 17].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            

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
    }
}
