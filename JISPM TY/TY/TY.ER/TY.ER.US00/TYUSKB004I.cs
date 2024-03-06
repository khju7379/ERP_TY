using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.ER.GB00;

namespace TY.ER.US00
{
    /// <summary>
    /// 적하목록관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2019.04.04 15:18
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_944FC234 : 적하목록 조회
    ///  TY_P_US_944FD235 : 적하목록 확인
    ///  TY_P_US_944FE236 : 적하목록 등록
    ///  TY_P_US_944FG237 : 적하목록 수정
    ///  TY_P_US_944FG238 : 적하목록 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_944FN239 : 적하목록관리
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
    ///  JKHANGCHA : 항차
    ///  IHJUKHANO : 적하목록번호
    ///  JKBEJNQTY : 배정량
    ///  JKHWAKQTY : 확정량
    /// </summary>
    public partial class TYUSKB004I : TYBase
    {
        private string fsIHHANGCHA = string.Empty;
        private string fsIHJUKHANO = string.Empty;

        #region Description : 폼 로드
        public TYUSKB004I(string sIHHANGCHA, string sIHJUKHANO)
        {
            InitializeComponent();

            fsIHHANGCHA = sIHHANGCHA;
            fsIHJUKHANO = sIHJUKHANO;

            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_944FN239, "JKHANGCHA", "HANGCHANM", "JKHANGCHA");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_944FN239, "JKHWAJU", "HAWJUNM", "JKHWAJU");
        }

        private void TYUSKB004I_Load(object sender, System.EventArgs e)
        {   
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_944FN239, "JKBLMSN");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.CBH01_JKHANGCHA.SetValue(fsIHHANGCHA);
            this.TXT01_IHJUKHANO.SetValue(fsIHJUKHANO);

            this.CBH01_JKHANGCHA.SetReadOnly(true);
            this.TXT01_IHJUKHANO.SetReadOnly(true);

            BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_944FN239.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_US_944FC234", this.CBH01_JKHANGCHA.GetValue().ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_944FN239.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                this.TXT01_JKBEJNQTY.SetValue(dt.Compute("SUM(JKBEJNQTY)", null).ToString());
                this.TXT01_JKHWAKQTY.SetValue(dt.Compute("SUM(JKHWAKQTY)", null).ToString());
            }
            else
            {
                this.TXT01_JKBEJNQTY.SetValue("0");
                this.TXT01_JKHWAKQTY.SetValue("0");
            }
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_944FG238", dt);
            this.DbConnector.ExecuteNonQueryList();

            BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_US_944FN239.GetDataSourceInclude(TSpread.TActionType.Remove, "JKHANGCHA", "JKBLMSN");
            DataTable dtTemp = this.FPS91_TY_S_US_944FN239.GetDataSourceInclude(TSpread.TActionType.Remove, "JKHANGCHA", "JKBLMSN", "JKHWAKQTY");

            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                if (Convert.ToDouble(Get_Numeric(dtTemp.Rows[i]["JKHWAKQTY"].ToString())) > 0)
                {
                    this.ShowCustomMessage("입고내역이 존재하여 삭제가 불가합니다. .", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

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

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_US_944FE236", ds.Tables[0].Rows[i]["JKHANGCHA"].ToString(),
                                                                ds.Tables[0].Rows[i]["JKBLMSN"].ToString(),
                                                                ds.Tables[0].Rows[i]["JKHWAJU"].ToString(),
                                                                ds.Tables[0].Rows[i]["JKBEJNQTY"].ToString(),
                                                                TYUserInfo.EmpNo
                                                                ); //저장
                }
                this.DbConnector.ExecuteNonQueryList();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_US_944FG237", ds.Tables[1].Rows[i]["JKHWAJU"].ToString(),
                                                                ds.Tables[1].Rows[i]["JKBEJNQTY"].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["JKHANGCHA"].ToString(),
                                                                ds.Tables[1].Rows[i]["JKBLMSN"].ToString()
                                                                ); //수정
                }
                this.DbConnector.ExecuteNonQueryList();
            }

            BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD873"); // 저장 메세지
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            DataSet dsChk = new DataSet();
            DataTable dt = new DataTable();

            double dJKBEJNQTY_TOT = 0;
            double dIHBLQTY = 0;

            int iRowEqual = 0;

            string sJKBLMSN = string.Empty;

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_US_944FN239.GetDataSourceInclude(TSpread.TActionType.New, "JKHANGCHA", "JKBLMSN", "JKHWAJU", "JKBEJNQTY", "PREJKBEJNQTY"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_US_944FN239.GetDataSourceInclude(TSpread.TActionType.Update, "JKHANGCHA", "JKBLMSN", "JKHWAJU", "JKBEJNQTY", "PREJKBEJNQTY"));

            dsChk = ds.Copy();

            // 키 중복 체크
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                iRowEqual = 0;

                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_US_944FD235", ds.Tables[0].Rows[i]["JKHANGCHA"].ToString(),
                                                            ds.Tables[0].Rows[i]["JKBLMSN"].ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowCustomMessage("[MSN:" + ds.Tables[0].Rows[i]["JKBLMSN"].ToString() + "] 이미 등록된 자료입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }

                for (int j = 0; j < dsChk.Tables[0].Rows.Count; j++)
                {
                    if (ds.Tables[0].Rows[i]["JKBLMSN"].ToString() == dsChk.Tables[0].Rows[j]["JKBLMSN"].ToString())
                    {
                        iRowEqual = iRowEqual + 1;
                    }
                }

                if (iRowEqual > 1)
                {
                    this.ShowCustomMessage("동일한 MSN은 등록할 수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            // 배정량이 입항관리의 총 B/L 량을 초과하는지 체크
            // 입항관리 총 B/L량 조회
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_US_941ET214", this.CBH01_JKHANGCHA.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                dIHBLQTY = Convert.ToDouble(Get_Numeric(dt.Rows[0]["IHBLQTY_HAP"].ToString()));
            }

            // 적하목록관리 배정량 합계 계산
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_US_944FC234", this.CBH01_JKHANGCHA.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                dJKBEJNQTY_TOT = Convert.ToDouble(dt.Compute("SUM(JKBEJNQTY)", null).ToString());
            }
            
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dJKBEJNQTY_TOT = double.Parse(String.Format("{0,9:N3}", dJKBEJNQTY_TOT + Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i]["JKBEJNQTY"].ToString()))));
            }

            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                dJKBEJNQTY_TOT = double.Parse(String.Format("{0,9:N3}", dJKBEJNQTY_TOT + Convert.ToDouble(Get_Numeric(ds.Tables[1].Rows[i]["JKBEJNQTY"].ToString())) - Convert.ToDouble(Get_Numeric(ds.Tables[1].Rows[i]["PREJKBEJNQTY"].ToString()))));
            }

            if (dJKBEJNQTY_TOT > dIHBLQTY)
            {
                this.ShowCustomMessage("배정량 합계가 총B/L량을 초과하였습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
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

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion

        #region Description : Row 추가 이벤트
        private void FPS91_TY_S_US_944FN239_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_US_944FN239.SetValue(e.RowIndex, "JKHANGCHA", this.CBH01_JKHANGCHA.GetValue().ToString());
            this.FPS91_TY_S_US_944FN239.SetValue(e.RowIndex, "HANGCHANM", this.CBH01_JKHANGCHA.GetText().ToString());
        }
        #endregion
    }
}
