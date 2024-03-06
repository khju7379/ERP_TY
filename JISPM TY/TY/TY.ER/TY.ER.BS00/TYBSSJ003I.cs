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
    public partial class TYBSSJ003I : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYBSSJ003I()
        {
            InitializeComponent();
        }

        private void TYBSSJ003I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            CBH01_BVJDPAC.DummyValue = DateTime.Now.ToString("yyyyMMdd");

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
            UP_Set_TitleSetting();

            this.FPS91_TY_S_AC_79JBK616.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_79JBI615", this.CBH01_BSJYYMM.GetValue().ToString(), CBH01_BVJDPAC.GetValue(), CBO01_INQOPTION.GetValue().ToString());
            this.FPS91_TY_S_AC_79JBK616.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_79JBK616.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_AC_79JBK616.CurrentRowCount; i++)
                {
                    //수선비는 실적월까지 수정할수 없다
                    if (this.FPS91_TY_S_AC_79JBK616.GetValue(i, "BVJTYPE").ToString() == "B" && this.FPS91_TY_S_AC_79JBK616.GetValue(i, "BVJCDAC").ToString() != "99999999" 
                        && this.FPS91_TY_S_AC_79JBK616.GetValue(i, "BVJSEQ").ToString() != "0")
                    {
                        int iAddMonth = Convert.ToInt16(this.CBH01_BSJYYMM.GetValue().ToString().Substring(4, 2));

                        if (iAddMonth > 9)
                        {
                            iAddMonth = iAddMonth + 1;
                        }

                        for (int j = 22; j < (22+iAddMonth); j++)
                        {
                            this.FPS91_TY_S_AC_79JBK616_Sheet1.Cells[i, j].ForeColor = Color.Black;
                            this.FPS91_TY_S_AC_79JBK616_Sheet1.Cells[i, j].Locked = true;
                        }

                        for (int j = (22 + iAddMonth); j < (22 + iAddMonth) + (this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Columns.Count - (22 + iAddMonth)); j++)
                        {
                            if (this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[1, j].Value.ToString() != "소 계" && this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[1, j].Value.ToString() != "합 계")
                            {
                                this.FPS91_TY_S_AC_79JBK616_Sheet1.Cells[i, j].Font = new Font("굴림", 9, FontStyle.Bold);
                            }
                        }
                    }

                    //투자는 전월을 다 수정 할수 있다
                    if (this.FPS91_TY_S_AC_79JBK616.GetValue(i, "BVJTYPE").ToString() == "A" && this.FPS91_TY_S_AC_79JBK616.GetValue(i, "BVJCDAC").ToString() != "99999999"
                        && this.FPS91_TY_S_AC_79JBK616.GetValue(i, "BVJSEQ").ToString() != "0")
                    {
                        int iAddMonth = 13;

                        for (int j = 22; j < (22 + iAddMonth); j++)
                        {
                            if (this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[1, j].Value.ToString() != "소 계" && this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[1, j].Value.ToString() != "합 계")
                            {
                                this.FPS91_TY_S_AC_79JBK616_Sheet1.Cells[i, j].Font = new Font("굴림", 9, FontStyle.Bold);
                            }
                        }
                    }

                    //소계라인
                    if (this.FPS91_TY_S_AC_79JBK616.GetValue(i, "BVJITEM").ToString() == "[소 계]")
                    {                        
                        for (int j = 19; j < 35 ; j++)
                        {
                            this.FPS91_TY_S_AC_79JBK616_Sheet1.Cells[i, j].BackColor = Color.FromArgb(218, 239, 244);
                        }

                        for (int j = 21; j < 35; j++)
                        {
                            this.FPS91_TY_S_AC_79JBK616_Sheet1.Cells[i, j].ForeColor = Color.Black;
                            this.FPS91_TY_S_AC_79JBK616_Sheet1.Cells[i, j].Locked = true;
                        }

                    }
                    else if (this.FPS91_TY_S_AC_79JBK616.GetValue(i, "BVJSEQ").ToString() == "999" ) //유형별 합계라인
                    {

                        for (int j = 8; j < 35; j++)
                        {
                            this.FPS91_TY_S_AC_79JBK616_Sheet1.Cells[i, j].BackColor = Color.FromArgb(228, 242, 194);
                        }

                        for (int j = 21; j < 35; j++)
                        {
                            this.FPS91_TY_S_AC_79JBK616_Sheet1.Cells[i, j].ForeColor = Color.Black;
                            this.FPS91_TY_S_AC_79JBK616_Sheet1.Cells[i, j].Locked = true;
                        }
                    }
                    else if (    this.FPS91_TY_S_AC_79JBK616.GetValue(i, "BVJSEQ").ToString() == "9999")  //자산별 합계
                    {
                        this.FPS91_TY_S_AC_79JBK616.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);

                        for (int j = 21; j < 35; j++)
                        {
                            this.FPS91_TY_S_AC_79JBK616_Sheet1.Cells[i, j].ForeColor = Color.Red;
                            this.FPS91_TY_S_AC_79JBK616_Sheet1.Cells[i, j].Locked = true;
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
                if (ds.Tables[0].Rows[i]["BVJSEQ"].ToString() != "0" && ds.Tables[0].Rows[i]["BVJSEQ"].ToString() != "999" && ds.Tables[0].Rows[i]["BVJSEQ"].ToString() != "9999")
                {
                    this.DbConnector.Attach("TY_P_AC_79K8W634", ds.Tables[0].Rows[i]["BVJMONAMT01"].ToString(),
                                                                ds.Tables[0].Rows[i]["BVJMONAMT02"].ToString(),
                                                                ds.Tables[0].Rows[i]["BVJMONAMT03"].ToString(),
                                                                ds.Tables[0].Rows[i]["BVJMONAMT04"].ToString(),
                                                                ds.Tables[0].Rows[i]["BVJMONAMT05"].ToString(),
                                                                ds.Tables[0].Rows[i]["BVJMONAMT06"].ToString(),
                                                                ds.Tables[0].Rows[i]["BVJMONAMT07"].ToString(),
                                                                ds.Tables[0].Rows[i]["BVJMONAMT08"].ToString(),
                                                                ds.Tables[0].Rows[i]["BVJMONAMT09"].ToString(),
                                                                ds.Tables[0].Rows[i]["BVJMONAMT10"].ToString(),
                                                                ds.Tables[0].Rows[i]["BVJMONAMT11"].ToString(),
                                                                ds.Tables[0].Rows[i]["BVJMONAMT12"].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[0].Rows[i]["BVJYYMM"].ToString(),
                                                                ds.Tables[0].Rows[i]["BVJFORM"].ToString(),
                                                                ds.Tables[0].Rows[i]["BVJYEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["BVJDPAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["BVJTYPE"].ToString(),
                                                                ds.Tables[0].Rows[i]["BVJASETGN"].ToString(),
                                                                ds.Tables[0].Rows[i]["BVJCDAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["BVJSEQ"].ToString()
                                                                );
                }


            }
            if (this.DbConnector.CommandCount > 0)
                this.DbConnector.ExecuteTranQueryList();

            //합계 정리
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_79K8Y635", TYUserInfo.EmpNo, CBH01_BSJYYMM.GetValue().ToString());
            this.DbConnector.ExecuteTranQuery();

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD873");
        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_AC_79JBK616.GetDataSourceInclude(TSpread.TActionType.Update, "BVJYYMM", "BVJFORM", "BVJYEAR", "BVJDPAC", "BVJASETGN", "BVJTYPE", "BVJCDAC","BVJSEQ","BVJMONAMT01","BVJMONAMT02","BVJMONAMT03","BVJMONAMT04","BVJMONAMT05","BVJMONAMT06","BVJMONAMT07","BVJMONAMT08","BVJMONAMT09","BVJMONAMT10","BVJMONAMT11","BVJMONAMT12"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
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

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYBSSJ003P(CBH01_BSJYYMM.GetValue().ToString(), CBH01_BVJDPAC.GetValue().ToString() )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
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
                this.DbConnector.Attach("TY_P_AC_79MEM657", dt.Rows[i]["BVJYYMM"].ToString(),
                                                            dt.Rows[i]["BVJFORM"].ToString(),
                                                            dt.Rows[i]["BVJYEAR"].ToString(),
                                                            dt.Rows[i]["BVJDPAC"].ToString(),
                                                            dt.Rows[i]["BVJTYPE"].ToString(),
                                                            dt.Rows[i]["BVJASETGN"].ToString(),
                                                            dt.Rows[i]["BVJCDAC"].ToString(),
                                                            dt.Rows[i]["BVJSEQ"].ToString()
                                                            );
            }
            if( this.DbConnector.CommandCount > 0 )
               this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);

        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_79JBK616.GetDataSourceInclude(TSpread.TActionType.Remove, "BVJYYMM", "BVJFORM", "BVJYEAR", "BVJDPAC", "BVJASETGN", "BVJTYPE", "BVJCDAC","BVJSEQ", "BVJDATAGN");

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

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["BVJDATAGN"].ToString() != "N")
                    {
                        this.ShowCustomMessage("삭제할수 없는 자료 입니다! ", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region Description :  그리드 타이트 셋팅 함수
        private void UP_Set_TitleSetting()
        {
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_79JBK616_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_79JBK616_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_AC_79JBK616_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_AC_79JBK616_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);
            this.FPS91_TY_S_AC_79JBK616_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);
            this.FPS91_TY_S_AC_79JBK616_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1);
            this.FPS91_TY_S_AC_79JBK616_Sheet1.AddColumnHeaderSpanCell(0, 5, 2, 1);
            this.FPS91_TY_S_AC_79JBK616_Sheet1.AddColumnHeaderSpanCell(0, 6, 2, 1);
            this.FPS91_TY_S_AC_79JBK616_Sheet1.AddColumnHeaderSpanCell(0, 7, 2, 1);
            this.FPS91_TY_S_AC_79JBK616_Sheet1.AddColumnHeaderSpanCell(0, 8, 2, 1);
            this.FPS91_TY_S_AC_79JBK616_Sheet1.AddColumnHeaderSpanCell(0, 9, 2, 1);
            this.FPS91_TY_S_AC_79JBK616_Sheet1.AddColumnHeaderSpanCell(0, 10, 2, 1);
            this.FPS91_TY_S_AC_79JBK616_Sheet1.AddColumnHeaderSpanCell(0, 11, 2, 1);
            this.FPS91_TY_S_AC_79JBK616_Sheet1.AddColumnHeaderSpanCell(0, 12, 2, 1);
            this.FPS91_TY_S_AC_79JBK616_Sheet1.AddColumnHeaderSpanCell(0, 13, 2, 1);
            this.FPS91_TY_S_AC_79JBK616_Sheet1.AddColumnHeaderSpanCell(0, 14, 2, 1);
            this.FPS91_TY_S_AC_79JBK616_Sheet1.AddColumnHeaderSpanCell(0, 15, 2, 1);
            this.FPS91_TY_S_AC_79JBK616_Sheet1.AddColumnHeaderSpanCell(0, 16, 2, 1);
            this.FPS91_TY_S_AC_79JBK616_Sheet1.AddColumnHeaderSpanCell(0, 17, 2, 1);
            this.FPS91_TY_S_AC_79JBK616_Sheet1.AddColumnHeaderSpanCell(0, 18, 2, 1);
            this.FPS91_TY_S_AC_79JBK616_Sheet1.AddColumnHeaderSpanCell(0, 19, 2, 1);
            this.FPS91_TY_S_AC_79JBK616_Sheet1.AddColumnHeaderSpanCell(0, 20, 2, 1);
            this.FPS91_TY_S_AC_79JBK616_Sheet1.AddColumnHeaderSpanCell(0, 21, 2, 1);

            this.FPS91_TY_S_AC_79JBK616_Sheet1.AddColumnHeaderSpanCell(0, 36, 2, 1);
            this.FPS91_TY_S_AC_79JBK616_Sheet1.AddColumnHeaderSpanCell(0, 37, 2, 1);
            

            this.FPS91_TY_S_AC_79JBK616_Sheet1.AddColumnHeaderSpanCell(0, 22, 1, 10);  //1월 ~ 9월            
            this.FPS91_TY_S_AC_79JBK616_Sheet1.AddColumnHeaderSpanCell(0, 32, 1, 4);  //10월 ~ 12월            

            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[0, 0].Value = "실적생성년월";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[0, 1].Value = "양식구분";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[0, 2].Value = "사업년도";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[0, 3].Value = "년 도";

            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[0, 4].Value = "귀속부서";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[0, 5].Value = "귀속부서명";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[0, 6].Value = "귀속부서";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[0, 7].Value = "귀속부서명";

            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[0, 8].Value = "자산코드";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[0, 9].Value = "자산명";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[0, 10].Value = "유형코드";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[0, 11].Value = "유     형";

            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[0, 12].Value = "자산코드";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[0, 13].Value = "자산명";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[0, 14].Value = "유형코드";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[0, 15].Value = "유     형";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[0, 16].Value = "계정과목";

            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[0, 17].Value = "계 정 명";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[0, 18].Value = "순번";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[0, 19].Value = "항목코드";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[0, 20].Value = "항 목 명";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[0, 21].Value = "삭제가능구분";

            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[0, 22].Value = this.CBH01_BSJYYMM.GetValue().ToString().Substring(0, 4) + "년 1월 ~ 9월";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[1, 22].Value = "1월";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[1, 23].Value = "2월";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[1, 24].Value = "3월";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[1, 25].Value = "4월";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[1, 26].Value = "5월";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[1, 27].Value = "6월";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[1, 28].Value = "7월";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[1, 29].Value = "8월";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[1, 30].Value = "9월";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[1, 31].Value = "소 계";

            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[0, 32].Value = this.CBH01_BSJYYMM.GetValue().ToString().Substring(0, 4) + "년 10월 ~ 12월";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[1, 32].Value = "10월";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[1, 33].Value = "11월";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[1, 34].Value = "12월";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[1, 35].Value = "소 계";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[1, 36].Value = "합 계";
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[1, 37].Value = "수정사유";

            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[0, 22].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_79JBK616_Sheet1.ColumnHeader.Cells[0, 32].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            

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
                sYN = dt.Rows[0]["BLJCHKIN"].ToString();
            }

            return sYN;
        }
        #endregion
    }
}
