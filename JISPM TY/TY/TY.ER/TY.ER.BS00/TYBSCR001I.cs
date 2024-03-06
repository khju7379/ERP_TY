using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.BS00
{
    /// <summary>
    /// 자금수지 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.08.09 13:08
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_789DC396 : 자금수지 관리 조회
    ///  TY_P_AC_789DE397 : 자금수지 관리 등록
    ///  TY_P_AC_789DG398 : 자금수지 관리 수정
    ///  TY_P_AC_789DG399 : 자금수지 관리 삭제
    ///  TY_P_AC_789DH400 : 자금수지 관리 확인
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_789EK402 : 자금수지 관리
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  VSYEAR : 기준년도
    /// </summary>
    public partial class TYBSCR001I : TYBase
    {
        #region Description : 폼 로드
        public TYBSCR001I()
        {
            InitializeComponent();
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_789EK402, "BFCDAC", "A1NMAC", "BFCDAC");
        }

        private void TYBSCR001I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_789EK402, "BFYEAR", "BFCDAC");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.TXT01_VSYEAR.Text = System.DateTime.Now.ToString("yyyy");

            BTN61_INQ_Click(null, null);

            SetStartingFocus(this.TXT01_VSYEAR);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            try
            {
                this.FPS91_TY_S_AC_789EK402.Initialize();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_789DC396", this.TXT01_VSYEAR.GetValue().ToString(), "");
                DataTable dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_AC_789EK402.SetValue(dt);
            }
            catch
            {

            }
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_789DG399", dt);
                this.DbConnector.ExecuteNonQuery();

                BTN61_INQ_Click(null, null);

                this.ShowMessage("TY_M_GB_23NAD874");
            }
            catch
            {
                this.ShowMessage("TY_M_GB_43C9G671");
            }
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_789EK402.GetDataSourceInclude(TSpread.TActionType.Remove, "BFYEAR", "BFCDAC");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
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

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            DataTable dt = new DataTable();

             

            try
            {
                double[] dBFMONTOTAL0 = new double[ds.Tables[0].Rows.Count];
                double[] dBFMONTOTAL1 = new double[ds.Tables[1].Rows.Count];
                dBFMONTOTAL0 = UP_GetBFMONTOTAL(ds.Tables[0]);
                dBFMONTOTAL1 = UP_GetBFMONTOTAL(ds.Tables[1]);

                //신규등록
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_AC_789DE397", ds.Tables[0].Rows[i]["BFYEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["BFCDAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["BFMONAMT01"].ToString(),
                                                                ds.Tables[0].Rows[i]["BFMONAMT02"].ToString(),
                                                                ds.Tables[0].Rows[i]["BFMONAMT03"].ToString(),
                                                                ds.Tables[0].Rows[i]["BFMONAMT04"].ToString(),
                                                                ds.Tables[0].Rows[i]["BFMONAMT05"].ToString(),
                                                                ds.Tables[0].Rows[i]["BFMONAMT06"].ToString(),
                                                                ds.Tables[0].Rows[i]["BFMONAMT07"].ToString(),
                                                                ds.Tables[0].Rows[i]["BFMONAMT08"].ToString(),
                                                                ds.Tables[0].Rows[i]["BFMONAMT09"].ToString(),
                                                                ds.Tables[0].Rows[i]["BFMONAMT10"].ToString(),
                                                                ds.Tables[0].Rows[i]["BFMONAMT11"].ToString(),
                                                                ds.Tables[0].Rows[i]["BFMONAMT12"].ToString(),
                                                                dBFMONTOTAL0[i],
                                                                ds.Tables[0].Rows[i]["BFMEMO"].ToString(),
                                                                TYUserInfo.EmpNo
                                                                );
                    this.DbConnector.ExecuteTranQuery();
                }
                //수정
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_AC_789DG398", ds.Tables[1].Rows[i]["BFMONAMT01"].ToString(),
                                                                ds.Tables[1].Rows[i]["BFMONAMT02"].ToString(),
                                                                ds.Tables[1].Rows[i]["BFMONAMT03"].ToString(),
                                                                ds.Tables[1].Rows[i]["BFMONAMT04"].ToString(),
                                                                ds.Tables[1].Rows[i]["BFMONAMT05"].ToString(),
                                                                ds.Tables[1].Rows[i]["BFMONAMT06"].ToString(),
                                                                ds.Tables[1].Rows[i]["BFMONAMT07"].ToString(),
                                                                ds.Tables[1].Rows[i]["BFMONAMT08"].ToString(),
                                                                ds.Tables[1].Rows[i]["BFMONAMT09"].ToString(),
                                                                ds.Tables[1].Rows[i]["BFMONAMT10"].ToString(),
                                                                ds.Tables[1].Rows[i]["BFMONAMT11"].ToString(),
                                                                ds.Tables[1].Rows[i]["BFMONAMT12"].ToString(),
                                                                dBFMONTOTAL1[i],
                                                                ds.Tables[1].Rows[i]["BFMEMO"].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["BFYEAR"].ToString(),
                                                                ds.Tables[1].Rows[i]["BFCDAC"].ToString()
                                                                );
                    this.DbConnector.ExecuteTranQuery();
                }

                BTN61_INQ_Click(null, null);

                this.ShowMessage("TY_M_GB_23NAD873");
            }
            catch
            {
                this.ShowMessage("TY_M_AC_246A2488");
            }
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_789EK402.GetDataSourceInclude(TSpread.TActionType.New, "BFYEAR", "BFCDAC", "BFMONAMT01", "BFMONAMT02", "BFMONAMT03", "BFMONAMT04", "BFMONAMT05", "BFMONAMT06",
                                                                                                    "BFMONAMT07", "BFMONAMT08", "BFMONAMT09", "BFMONAMT10", "BFMONAMT11", "BFMONAMT12", "BFMONTOTAL", "BFMEMO"));

            ds.Tables.Add(this.FPS91_TY_S_AC_789EK402.GetDataSourceInclude(TSpread.TActionType.Update, "BFYEAR", "BFCDAC", "BFMONAMT01", "BFMONAMT02", "BFMONAMT03", "BFMONAMT04", "BFMONAMT05", "BFMONAMT06",
                                                                                                       "BFMONAMT07", "BFMONAMT08", "BFMONAMT09", "BFMONAMT10", "BFMONAMT11", "BFMONAMT12", "BFMONTOTAL", "BFMEMO"));

            // 저장 체크
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_789DH400", ds.Tables[0].Rows[i]["BFYEAR"].ToString(),
                                                            ds.Tables[0].Rows[i]["BFCDAC"].ToString()
                                                            );

                DataTable dtTmp = this.DbConnector.ExecuteDataTable();

                if (dtTmp.Rows.Count > 0)
                {
                    this.ShowCustomMessage("이미 등록된 항목입니다.[" + ds.Tables[0].Rows[i]["BFCDAC"].ToString() + "]",
                                            "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }
            }

            // 수정 체크
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {

            }

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
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

        #region Description : row 추가
        private void FPS91_TY_S_AC_789EK402_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_789EK402.SetValue(e.RowIndex, "BFYEAR", TXT01_VSYEAR.GetValue().ToString());
        }
        #endregion

        private double[] UP_GetBFMONTOTAL(DataTable dt)
        {
            double[] dBFMONTOTAL = new double[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["BFMONAMT01"].ToString() != "")
                {
                    dBFMONTOTAL[i] += Convert.ToDouble(dt.Rows[i]["BFMONAMT01"].ToString());
                }
                if (dt.Rows[i]["BFMONAMT02"].ToString() != "")
                {
                    dBFMONTOTAL[i] += Convert.ToDouble(dt.Rows[i]["BFMONAMT02"].ToString());
                }
                if (dt.Rows[i]["BFMONAMT03"].ToString() != "")
                {
                    dBFMONTOTAL[i] += Convert.ToDouble(dt.Rows[i]["BFMONAMT03"].ToString());
                }
                if (dt.Rows[i]["BFMONAMT04"].ToString() != "")
                {
                    dBFMONTOTAL[i] += Convert.ToDouble(dt.Rows[i]["BFMONAMT04"].ToString());
                }
                if (dt.Rows[i]["BFMONAMT05"].ToString() != "")
                {
                    dBFMONTOTAL[i] += Convert.ToDouble(dt.Rows[i]["BFMONAMT05"].ToString());
                }
                if (dt.Rows[i]["BFMONAMT06"].ToString() != "")
                {
                    dBFMONTOTAL[i] += Convert.ToDouble(dt.Rows[i]["BFMONAMT06"].ToString());
                }
                if (dt.Rows[i]["BFMONAMT07"].ToString() != "")
                {
                    dBFMONTOTAL[i] += Convert.ToDouble(dt.Rows[i]["BFMONAMT07"].ToString());
                }
                if (dt.Rows[i]["BFMONAMT08"].ToString() != "")
                {
                    dBFMONTOTAL[i] += Convert.ToDouble(dt.Rows[i]["BFMONAMT08"].ToString());
                }
                if (dt.Rows[i]["BFMONAMT09"].ToString() != "")
                {
                    dBFMONTOTAL[i] += Convert.ToDouble(dt.Rows[i]["BFMONAMT09"].ToString());
                }
                if (dt.Rows[i]["BFMONAMT10"].ToString() != "")
                {
                    dBFMONTOTAL[i] += Convert.ToDouble(dt.Rows[i]["BFMONAMT10"].ToString());
                }
                if (dt.Rows[i]["BFMONAMT11"].ToString() != "")
                {
                    dBFMONTOTAL[i] += Convert.ToDouble(dt.Rows[i]["BFMONAMT11"].ToString());
                }
                if (dt.Rows[i]["BFMONAMT12"].ToString() != "")
                {
                    dBFMONTOTAL[i] += Convert.ToDouble(dt.Rows[i]["BFMONAMT12"].ToString());
                }
            }
            return dBFMONTOTAL;
        }
    }
}
