using System;
using System.Data;
using System.Drawing;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using DataDynamics.ActiveReports;

namespace TY.ER.UT00
{
    /// <summary>
    /// 미수금 현황 출력 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.10.20 17:17
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_66FD4200 : 대표 거래처 코드 조회
    ///  TY_P_UT_6AKH0466 : 미수금 현황 출력(화주O)
    ///  TY_P_UT_6AKH0467 : 미수금 현황 출력(화주X)
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  CMHWAJU : 화주
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYUTSU002P : TYBase
    {
        #region Description : 페이지 로드
        public TYUTSU002P()
        {
            InitializeComponent();
        }

        private void TYUTSU002P_Load(object sender, System.EventArgs e)
        {
            this.DTP01_SDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            this.BTN61_INQ.ProcessCheck += new TButton.CheckHandler(BTN61_INQ_ProcessCheck);
            this.BTN61_PRT.ProcessCheck += new TButton.CheckHandler(BTN61_PRT_ProcessCheck);

            this.FPS91_TY_S_UT_81OCK527.Initialize();

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.DTP01_SDATE);
        }
        #endregion

        #region Desription : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sVNCODE = this.CBH01_CMHWAJU.GetValue().ToString();
            DataTable dt = new DataTable();

            this.FPS91_TY_S_UT_81OCK527.Initialize();

            if (sVNCODE != "")
            {
                sVNCODE = Get_VNCODE(sVNCODE);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_81OB2526", this.DTP01_EDATE.GetValue().ToString(),
                                                            sVNCODE);
                dt = this.DbConnector.ExecuteDataTable();
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_81OCP528", this.DTP01_EDATE.GetValue().ToString());

                dt = this.DbConnector.ExecuteDataTable();
            }

            this.FPS91_TY_S_UT_81OCK527.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_UT_81OCK527, "VNSANGHO", "합  계", SumRowType.Sum, "BOKAMT", "HANDAMT", "HAYAMT", "JUBAMT", "MAECHUL", "INSRAMT", "HMAMT", "TOTAL");

                for (int i = 0; i < this.FPS91_TY_S_UT_81OCK527.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_UT_81OCK527.GetValue(i, "VNSANGHO").ToString() == "거래처별 소계")
                    {
                        //this.FPS91_TY_S_UT_81OCK527.ActiveSheet.Rows[i].BackColor = Color.LightSeaGreen;

                        //this.FPS91_TY_S_UT_81OCK527_Sheet1.Cells[i, 0].Font = new Font("굴림체", 9, FontStyle.Bold);

                        //this.FPS91_TY_S_UT_81OCK527_Sheet1.Cells[i, 2].Font = new Font("굴림체", 9, FontStyle.Bold);
                        //this.FPS91_TY_S_UT_81OCK527_Sheet1.Cells[i, 3].Font = new Font("굴림체", 9, FontStyle.Bold);
                        //this.FPS91_TY_S_UT_81OCK527_Sheet1.Cells[i, 4].Font = new Font("굴림체", 9, FontStyle.Bold);
                        //this.FPS91_TY_S_UT_81OCK527_Sheet1.Cells[i, 5].Font = new Font("굴림체", 9, FontStyle.Bold);
                        //this.FPS91_TY_S_UT_81OCK527_Sheet1.Cells[i, 6].Font = new Font("굴림체", 9, FontStyle.Bold);
                        //this.FPS91_TY_S_UT_81OCK527_Sheet1.Cells[i, 7].Font = new Font("굴림체", 9, FontStyle.Bold);
                        //this.FPS91_TY_S_UT_81OCK527_Sheet1.Cells[i, 8].Font = new Font("굴림체", 9, FontStyle.Bold);
                        //this.FPS91_TY_S_UT_81OCK527_Sheet1.Cells[i, 9].Font = new Font("굴림체", 9, FontStyle.Bold);

                        this.FPS91_TY_S_UT_81OCK527_Sheet1.Cells[i, 0].ForeColor = Color.Red;
                        this.FPS91_TY_S_UT_81OCK527_Sheet1.Cells[i, 2].ForeColor = Color.Red;
                        this.FPS91_TY_S_UT_81OCK527_Sheet1.Cells[i, 3].ForeColor = Color.Red;
                        this.FPS91_TY_S_UT_81OCK527_Sheet1.Cells[i, 4].ForeColor = Color.Red;
                        this.FPS91_TY_S_UT_81OCK527_Sheet1.Cells[i, 5].ForeColor = Color.Red;
                        this.FPS91_TY_S_UT_81OCK527_Sheet1.Cells[i, 6].ForeColor = Color.Red;
                        this.FPS91_TY_S_UT_81OCK527_Sheet1.Cells[i, 7].ForeColor = Color.Red;
                        this.FPS91_TY_S_UT_81OCK527_Sheet1.Cells[i, 8].ForeColor = Color.Red;
                        this.FPS91_TY_S_UT_81OCK527_Sheet1.Cells[i, 9].ForeColor = Color.Red;

                        
                    }
                }
            }
        }

        private void BTN61_INQ_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string STDATE = this.DTP01_SDATE.GetString();
            string EDDATE = this.DTP01_EDATE.GetString();

            if (Convert.ToInt32(STDATE) > Convert.ToInt32(EDDATE))
            {
                this.DTP01_SDATE.Focus();
                this.ShowCustomMessage("시작일자가 종료일자보다 클수 없습니다 .", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sVNCODE = this.CBH01_CMHWAJU.GetValue().ToString();
            DataTable dt = new DataTable();

            // 20180123 윤현진 주임 선급화물료도 나오도록 요청
            // 원본 - 20180123
            //if (sVNCODE != "")
            //{
            //    sVNCODE = Get_VNCODE(sVNCODE);

            //    this.DbConnector.CommandClear();
            //    this.DbConnector.Attach("TY_P_UT_6AKH0466", this.DTP01_EDATE.GetString().Substring(4, 2),
            //                                                this.DTP01_EDATE.GetString(),
            //                                                sVNCODE);
            //    dt = this.DbConnector.ExecuteDataTable();
            //}
            //else
            //{
            //    this.DbConnector.CommandClear();
            //    this.DbConnector.Attach("TY_P_UT_6AKH0467", this.DTP01_EDATE.GetString().Substring(4, 2),
            //                                                this.DTP01_EDATE.GetString());
            //    dt = this.DbConnector.ExecuteDataTable();
            //}

            //if (dt.Rows.Count > 0)
            //{
            //    ActiveReport rpt = new TYUTSU002R();
            //    // 가로 출력
            //    rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

            //    (new TYERGB001P(rpt, dt)).ShowDialog();
            //}
            //else
            //{
            //    this.ShowMessage("TY_M_AC_2422N250");
            //}


            // 수정 - 20180123
            if (sVNCODE != "")
            {
                sVNCODE = Get_VNCODE(sVNCODE);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_81NI0522", this.DTP01_EDATE.GetString().Substring(4, 2),
                                                            this.DTP01_EDATE.GetString(),
                                                            sVNCODE);
                dt = this.DbConnector.ExecuteDataTable();
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_81NI2523", this.DTP01_EDATE.GetString().Substring(4, 2),
                                                            this.DTP01_EDATE.GetString());
                dt = this.DbConnector.ExecuteDataTable();
            }

            if (dt.Rows.Count > 0)
            {
                ActiveReport rpt = new TYUTSU002R1();
                // 가로 출력
                rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }

        private void BTN61_PRT_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string STDATE = this.DTP01_SDATE.GetString();
            string EDDATE = this.DTP01_EDATE.GetString();

            if (Convert.ToInt32(STDATE) > Convert.ToInt32(EDDATE))
            {
                this.DTP01_SDATE.Focus();
                this.ShowCustomMessage("시작일자가 종료일자보다 클수 없습니다 .", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }
        }
        #endregion
    }
}
