using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.ER.GB00;
using System.Drawing;

namespace TY.ER.AT00
{
    /// <summary>
    /// 세대별 사용량 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2018.09.04 15:07
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_894ET686 : 세대별 사용량 조회
    ///  TY_P_HR_894EZ687 : 세대별 사용량 등록
    ///  TY_P_HR_894F0688 : 세대별 사용량 수정
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_894F1689 : 세대별 사용량 관리
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  SAV : 저장
    ///  APUYYMM : 년월
    /// </summary>
    public partial class TYATKB004I : TYBase
    {
        #region Description : 폼 로드
        public TYATKB004I()
        {
            InitializeComponent();
        }

        private void TYATKB004I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_APUYYMM.SetValue(System.DateTime.Now.ToString("yyyy-MM"));

            SetStartingFocus(this.DTP01_APUYYMM);

            BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_HR_894ET686", this.DTP01_APUYYMM.GetString().Substring(0, 6),
                                                        this.DTP01_APUYYMM.GetString().Substring(0, 6));

            this.FPS91_TY_S_HR_894F1689.SetValue(this.DbConnector.ExecuteDataTable());

            UP_ChangeSpread();
        }
        #endregion

        private void UP_ChangeSpread()
        {
            for (int i = 0; i < this.FPS91_TY_S_HR_894F1689.CurrentRowCount; i++)
            {
                if (this.FPS91_TY_S_HR_894F1689.GetValue(i, "GUBN").ToString() == "Y")
                {
                    if (this.FPS91_TY_S_HR_894F1689.GetValue(i, "APTCHECK").ToString() == "Y")
                    {
                        this.FPS91_TY_S_HR_894F1689.ActiveSheet.RowHeader.Cells[i, 0].Text = "N";
                    }
                    else
                    {
                        // 필드 잠금 , 배경색 변경
                        this.FPS91_TY_S_HR_894F1689.ActiveSheet.Rows[i].BackColor = Color.SkyBlue;
                        this.FPS91_TY_S_HR_894F1689.ActiveSheet.Rows[i].Locked = true;
                    }
                }
            }
        }

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

                // 신규 등록
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string sAPUSPCOLYN = ds.Tables[0].Rows[i]["APUSPCOLYN"].ToString();
                        if (sAPUSPCOLYN == "")
                        {
                            sAPUSPCOLYN = "N";
                        }

                        this.DbConnector.Attach("TY_P_HR_894EZ687", ds.Tables[0].Rows[i]["APUYYMM"].ToString(),
                                                                    ds.Tables[0].Rows[i]["APUHOSU"].ToString(),
                                                                    ds.Tables[0].Rows[i]["APUHAQTY"].ToString(),
                                                                    ds.Tables[0].Rows[i]["APUGASQTY"].ToString(),
                                                                    sAPUSPCOLYN,
                                                                    TYUserInfo.EmpNo
                                                                    );
                    }

                    this.DbConnector.ExecuteTranQueryList();
                }
                // 수정
                if (ds.Tables[1].Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();

                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        string sAPUSPCOLYN = ds.Tables[1].Rows[i]["APUSPCOLYN"].ToString();
                        if (sAPUSPCOLYN == "")
                        {
                            sAPUSPCOLYN = "N";
                        }

                        this.DbConnector.Attach("TY_P_HR_894F0688", ds.Tables[1].Rows[i]["APUHAQTY"].ToString(),
                                                                    ds.Tables[1].Rows[i]["APUGASQTY"].ToString(),
                                                                    sAPUSPCOLYN,
                                                                    TYUserInfo.EmpNo,
                                                                    ds.Tables[1].Rows[i]["APUYYMM"].ToString(),
                                                                    ds.Tables[1].Rows[i]["APUHOSU"].ToString()
                                                                    );
                    }

                    this.DbConnector.ExecuteTranQueryList();
                }
                this.ShowMessage("TY_M_GB_23NAD873");
                BTN61_INQ_Click(null, null);
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

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_HR_894F1689.GetDataSourceInclude(TSpread.TActionType.New, "APUYYMM", "APUHOSU", "APUHAQTY", "APUGASQTY", "APUSPCOLYN"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_HR_894F1689.GetDataSourceInclude(TSpread.TActionType.Update, "APUYYMM", "APUHOSU", "APUHAQTY", "APUGASQTY", "APUSPCOLYN"));

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

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

                this.DbConnector.CommandClear();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach
                        (
                        "TY_P_HR_895I3693",
                        ds.Tables[0].Rows[i]["APUYYMM"].ToString(),
                        ds.Tables[0].Rows[i]["APUHOSU"].ToString()
                        );
                }

                this.DbConnector.ExecuteTranQueryList();

                this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지

                BTN61_INQ_Click(null, null);
            }
            catch
            {
            }
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            // 스프레드에서 삭제 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_HR_894F1689.GetDataSourceInclude(TSpread.TActionType.Remove, "APUYYMM", "APUHOSU"));
            
            if (ds.Tables[0].Rows.Count == 0)
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

            e.ArgData = ds;
        }
        #endregion
    }
}
