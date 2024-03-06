using System;
using System.Data;
using System.Drawing;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.Windows.Forms;

namespace TY.ER.HR00
{
    /// <summary>
    /// 승호관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2015.02.03 16:53
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_522EE251 : 승호파일 삭제
    ///  TY_P_HR_523BJ258 : 승호파일 수정
    ///  TY_P_HR_523C3260 : 승호파일 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_5B3FM089 : 승호관리
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    ///  TY_M_GB_43C9G671 : 삭제 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  KBJKCD : 직급
    ///  KBSABUN : 사번
    ///  YYYYMM : 기준 년월
    /// </summary>
    public partial class TYHRKB017S : TYBase
    {
        #region Description : 폼 로드
        public TYHRKB017S()
        {
            InitializeComponent();
        }

        private void TYHRKB017S_Load(object sender, System.EventArgs e)
        {
            ToolStripMenuItem reateAdd = new ToolStripMenuItem("중도정산등록");
            reateAdd.Click += new EventHandler(Add_ToolStripMenuItem_Click);            

            this.FPS91_TY_S_HR_5B3FM089.CurrentContextMenu.Items.AddRange(
                new System.Windows.Forms.ToolStripItem[] { new ToolStripSeparator(), reateAdd });

            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));
            SetStartingFocus(this.DTP01_SDATE);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sSDATE = string.Empty;
            string sEDATE = string.Empty;

            DataTable dt = new DataTable();

            sSDATE = this.DTP01_SDATE.GetString();
            sEDATE = this.DTP01_EDATE.GetString();

            if (this.DTP01_SDATE.GetString() == "")
            {
                sSDATE = "19900101";
            }

            if (this.DTP01_EDATE.GetString() == "")
            {
                sEDATE = "22001231";
            }

            this.DbConnector.Attach
            (
            "TY_P_HR_5B3FP090",
            sSDATE.ToString(),
            sEDATE.ToString(),
            this.CBH01_PSSABUN.GetValue().ToString()
            );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }

            this.FPS91_TY_S_HR_5B3FM089.SetValue(dt);
        }
        #endregion

        //#region Description : 신규 버튼 이벤트
        //private void BTN61_NEW_Click(object sender, EventArgs e)
        //{
        //    //if ((new TYMRPR001I("", "P", "", "")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    //    BTN61_INQ_Click(null, null);
        //}
        //#endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_5B3G1092", Get_Numeric(ds.Tables[0].Rows[i]["PSSABUN"].ToString()),
                                                                Get_Numeric(ds.Tables[0].Rows[i]["PSYDATE"].ToString())
                                                                );

                    this.DbConnector.Attach("TY_P_HR_5B3G1091", Get_Numeric(ds.Tables[0].Rows[i]["PSSABUN"].ToString()),
                                                                Get_Numeric(ds.Tables[0].Rows[i]["PSYDATE"].ToString())
                                                                );
                }
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 ProcessCheck
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();

            ds.Tables.Add(this.FPS91_TY_S_HR_5B3FM089.GetDataSourceInclude(TSpread.TActionType.Remove, "PSSABUN", "PSYDATE"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }
            else
            {
                for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    // 급여 파일 존재 유무 체크
                    this.DbConnector.Attach
                        (
                        "TY_P_HR_5B3BR081",
                        ds.Tables[0].Rows[i]["PSSABUN"].ToString(),
                        ds.Tables[0].Rows[i]["PSYDATE"].ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_HR_5B3BS083");
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

            e.ArgData = ds;
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_HR_5B3FM089_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            // 파라미터값 보내기
            if ((new TYHRKB017I(this.FPS91_TY_S_HR_5B3FM089.GetValue("PSSABUN").ToString(), this.FPS91_TY_S_HR_5B3FM089.GetValue("PSYDATE").ToString(),
                                this.FPS91_TY_S_HR_5B3FM089.GetValue("PSGUBN").ToString(), this.FPS91_TY_S_HR_5B3FM089.GetValue("PSWKSDATE").ToString(),
                                this.FPS91_TY_S_HR_5B3FM089.GetValue("PSWKEDATE").ToString(), this.FPS91_TY_S_HR_5B3FM089.GetValue("PSJPNO").ToString(),
                                this.FPS91_TY_S_HR_5B3FM089.GetValue("PSTYPE").ToString()
                                )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 중도정산 등록 이벤트
        private void Add_ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (this.FPS91_TY_S_HR_5B3FM089.GetValue("PSGUBN").ToString() != "1")
            {
                this.ShowCustomMessage("중간정산 자료만 등록할수 있습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
            (
            "TY_P_HR_5B9DS130",
            this.FPS91_TY_S_HR_5B3FM089.GetValue("PSSABUN").ToString(),
            this.FPS91_TY_S_HR_5B3FM089.GetValue("PSYDATE").ToString(),
            this.FPS91_TY_S_HR_5B3FM089.GetValue("PSGUBN").ToString()
            );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_73TD1143", dt.Rows[0]["PSPAYDATE"].ToString(),
                                                            dt.Rows[0]["PSSABUN"].ToString(),
                                                            dt.Rows[0]["KBJKCD"].ToString(),
                                                            dt.Rows[0]["PSDEPT"].ToString(),
                                                            dt.Rows[0]["PSWKSDATE"].ToString(),
                                                            dt.Rows[0]["PSWKEDATE"].ToString(),
                                                            dt.Rows[0]["PSYEAR"].ToString(),
                                                            dt.Rows[0]["PSMONTH"].ToString(),
                                                            dt.Rows[0]["PSDAY"].ToString(),
                                                            Convert.ToString(Convert.ToInt16(Get_Numeric(dt.Rows[0]["CHASU"].ToString())) + 1),
                                                            dt.Rows[0]["PSSERVPAYAMT"].ToString(),
                                                            dt.Rows[0]["PSINCOMTAX"].ToString(),
                                                            dt.Rows[0]["PSJUMINTAX"].ToString(),
                                                            dt.Rows[0]["PSCHAINAMT"].ToString(),
                                                            "N",
                                                            TYUserInfo.EmpNo
                                                           );
                //인사기본사항에 중도정산 일자  UPDATE
                //this.DbConnector.Attach("TY_P_HR_73MB3075", dt.Rows[0]["PSWKEDATEADD"].ToString(),
                //                                            TYUserInfo.EmpNo,
                //                                            "2",
                //                                            dt.Rows[0]["PSSABUN"].ToString()                                                            
                //                                           );
                this.DbConnector.ExecuteTranQueryList();

                this.ShowCustomMessage("중간정산 자료 등록이 완료되었습니다 ", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            else
            {
                this.ShowCustomMessage("중간정산 자료가 존재하지않습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
            
        }
        #endregion
    }
}