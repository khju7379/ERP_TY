using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.ER.GB00;

namespace TY.ER.AT00
{
    /// <summary>
    /// 아파트 공지사항 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2018.08.22 14:02
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_88ME0609 : 아파트 공지사항 등록
    ///  TY_P_HR_88ME0610 : 아파트 공지사항 수정
    ///  TY_P_HR_88ME1611 : 아파트 공지사항 삭제
    ///  TY_P_HR_88ME1612 : 아파트 공지사항 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_89QG0739 : 아파트 공지사항 관리
    /// 
    ///  # 알림문자 정보 ####
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
    ///  APUYYMM : 년월
    /// </summary>
    public partial class TYATKB03C1 : TYBase
    {
        #region : Description : 폼 로드
        public TYATKB03C1()
        {
            InitializeComponent();
        }

        private void TYATKB03C1_Load(object sender, System.EventArgs e)
        {

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_89QG0739, "APOYYMM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_89QG0739, "APOSEQ");

            this.DTP01_APUYYMM.SetValue(System.DateTime.Now.ToString("yyyy-MM"));

            SetStartingFocus(this.DTP01_APUYYMM);

            BTN61_INQ_Click(null, null);
        }
        #endregion

        #region : Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_HR_88ME1612", this.DTP01_APUYYMM.GetString().Substring(0, 6));

            this.FPS91_TY_S_HR_89QG0739.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region : Description : 삭제 버튼
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
                        "TY_P_HR_88ME1611",
                        ds.Tables[0].Rows[i]["APOYYMM"].ToString(),
                        ds.Tables[0].Rows[i]["APOSEQ"].ToString()
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
            ds.Tables.Add(this.FPS91_TY_S_HR_89QG0739.GetDataSourceInclude(TSpread.TActionType.Remove, "APOYYMM", "APOSEQ"));

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

        #region : Description : 저장 버튼
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
                        this.DbConnector.Attach("TY_P_HR_88ME0609", ds.Tables[0].Rows[i]["APOYYMM"].ToString(),
                                                                    ds.Tables[0].Rows[i]["APOSEQ"].ToString(),
                                                                    ds.Tables[0].Rows[i]["APOMEMO"].ToString(),
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
                        this.DbConnector.Attach("TY_P_HR_88ME0610", ds.Tables[1].Rows[i]["APOMEMO"].ToString(),
                                                                    TYUserInfo.EmpNo,
                                                                    ds.Tables[1].Rows[i]["APOYYMM"].ToString(),
                                                                    ds.Tables[1].Rows[i]["APOSEQ"].ToString()
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
            ds.Tables.Add(this.FPS91_TY_S_HR_89QG0739.GetDataSourceInclude(TSpread.TActionType.New, "APOYYMM", "APOSEQ", "APOMEMO"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_HR_89QG0739.GetDataSourceInclude(TSpread.TActionType.Update, "APOYYMM", "APOSEQ", "APOMEMO"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string sAPOYYMM = ds.Tables[0].Rows[i]["APOYYMM"].ToString();
                string sAPOSEQ = ds.Tables[0].Rows[i]["APOSEQ"].ToString();
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_HR_89QH0743", ds.Tables[0].Rows[i]["APOYYMM"].ToString(),
                                                            ds.Tables[0].Rows[i]["APOSEQ"].ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowCustomMessage("이미 등록된 자료입니다.[" + ds.Tables[0].Rows[i]["APOYYMM"].ToString() + "-" + Set_Fill3(ds.Tables[0].Rows[i]["APOSEQ"].ToString()) + "]", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                    e.Successed = false;
                    return;
                }

                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    if (i != j)
                    {
                        if (sAPOYYMM + sAPOSEQ == ds.Tables[0].Rows[j]["APOYYMM"].ToString() + ds.Tables[0].Rows[j]["APOSEQ"].ToString())
                        {
                            this.ShowCustomMessage("중복되는 자료입니다.[" + sAPOYYMM + "-" + Set_Fill3(sAPOSEQ) + "]", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                            e.Successed = false;
                            return;
                        }
                    }
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        private void FPS91_TY_S_HR_89QG0739_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_HR_89QG0739.SetValue(e.RowIndex, "APOYYMM", this.DTP01_APUYYMM.GetString().Substring(0, 6));
        }
    }
}
