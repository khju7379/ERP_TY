using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.UT00
{
    /// <summary>
    /// UTILITY 단가 등록 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.07.04 10:30
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_674AM536 : UTILITY 단가 등록
    ///  TY_P_UT_674AN537 : UTILITY 단가 수정
    ///  TY_P_UT_674DE549 : UTILITY 단가 확인
    ///  TY_P_UT_674FJ555 : 가열료 조회(UTILITY 단가 등록)
    ///  TY_P_UT_674FM556 : SK 가열료 수정(UTILITY 단가 등록)
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2BF7Y364 : 조회가 완료되었습니다.
    ///  TY_M_GB_43C9G671 : 삭제 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  DNYYMM : 년월
    ///  DNBKCU : 벙커C유
    ///  DNELECT : 전기료
    ///  DNJILSO : 질소사용료
    ///  DNKYUNG : 경유
    ///  DNMOTER1 : 모터용량
    ///  DNMOTER2 : 모터용량
    ///  DNSELAMT : 전기총사용금액
    ///  DNSELDANGA : 전기사용단가
    ///  DNSELECT : 전기료
    ///  DNSELTIM : 전기총사용시간
    ///  DNSKSTEAM : SK스팀
    ///  DNSKTAMT : SK스팀총사용금액
    ///  DNSTAMT : 스팀총금액
    ///  DNSTDANGA : 스팀단가
    ///  DNSTTIM : 스팀총사용시간
    ///  DNYUL : 효율
    /// </summary>
    public partial class TYUTPS006I : TYBase
    {
        private string fsDTNCODE   = string.Empty;
        private string fsWK_GUBUN1 = string.Empty;
        private string fsWK_GUBUN2 = string.Empty;

        public TYUTPS006I(string sDTNCODE)
        {
            InitializeComponent();

            fsDTNCODE = sDTNCODE;
        }

        #region Description : 페이지 로드
        private void TYUTPS006I_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_B3CGG940, "DTNMODDATE");

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.CBH01_DTNCODE.SetValue(fsDTNCODE.ToString());

            this.CBH01_DTNCODE.SetReadOnly(true);

            UP_RUN();
        }
        #endregion

        #region Description : 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            try
            {
                DataTable dt = new DataTable();

                // 등록
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string sDTNNUM = string.Empty;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        // 순번 가져오기
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_B3CGZ941", this.CBH01_DTNCODE.GetValue().ToString().Trim(),
                                                                    Get_Date(ds.Tables[0].Rows[i]["DTNMODDATE"].ToString()));

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            sDTNNUM = dt.Rows[0]["DTNNUM"].ToString();
                        }

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_B3CH3942", this.CBH01_DTNCODE.GetValue().ToString().Trim(),
                                                                    Get_Date(ds.Tables[0].Rows[i]["DTNMODDATE"].ToString()),
                                                                    sDTNNUM.ToString(),
                                                                    ds.Tables[0].Rows[i]["DTNMODYDESC"].ToString(),
                                                                    ds.Tables[0].Rows[i]["DTNMODYTEAM"].ToString(),
                                                                    Get_Numeric(ds.Tables[0].Rows[i]["DTNAMOUNT"].ToString()),
                                                                    ds.Tables[0].Rows[i]["DTNBIGO"].ToString(),
                                                                    TYUserInfo.EmpNo
                                                                    );
                        this.DbConnector.ExecuteTranQuery();
                    }
                }

                // 수정
                if (ds.Tables[1].Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();

                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        this.DbConnector.Attach("TY_P_UT_B3CH4943", ds.Tables[1].Rows[i]["DTNMODYDESC"].ToString(),
                                                                    ds.Tables[1].Rows[i]["DTNMODYTEAM"].ToString(),
                                                                    Get_Numeric(ds.Tables[1].Rows[i]["DTNAMOUNT"].ToString()),
                                                                    ds.Tables[1].Rows[i]["DTNBIGO"].ToString(),
                                                                    TYUserInfo.EmpNo,
                                                                    this.CBH01_DTNCODE.GetValue().ToString().Trim(),
                                                                    Get_Date(ds.Tables[1].Rows[i]["DTNMODDATE"].ToString()),
                                                                    ds.Tables[1].Rows[i]["DTNNUM"].ToString()
                                                                    );
                    }

                    this.DbConnector.ExecuteTranQueryList();
                }

                // 삭제
                if (ds.Tables[2].Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();

                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                    {
                        this.DbConnector.Attach("TY_P_UT_B3CH4944", this.CBH01_DTNCODE.GetValue().ToString().Trim(),
                                                                    Get_Date(ds.Tables[2].Rows[i]["DTNMODDATE"].ToString()),
                                                                    ds.Tables[2].Rows[i]["DTNNUM"].ToString()
                                                                    );
                    }

                    this.DbConnector.ExecuteTranQueryList();
                }

                this.ShowMessage("TY_M_MR_2BF50354");

                // 확인
                UP_RUN();
            }
            catch (Exception ex)
            {
                this.ShowMessage("TY_M_UT_71BDP399");
            }
        }
        #endregion

        #region Description : 처리 체크
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_UT_B3CGG940.GetDataSourceInclude(TSpread.TActionType.New,    "DTNMODDATE", "DTNNUM", "DTNMODYDESC", "DTNMODYTEAM", "DTNAMOUNT", "DTNBIGO"));
            ds.Tables.Add(this.FPS91_TY_S_UT_B3CGG940.GetDataSourceInclude(TSpread.TActionType.Update, "DTNMODDATE", "DTNNUM", "DTNMODYDESC", "DTNMODYTEAM", "DTNAMOUNT", "DTNBIGO"));
            ds.Tables.Add(this.FPS91_TY_S_UT_B3CGG940.GetDataSourceInclude(TSpread.TActionType.Remove, "DTNMODDATE", "DTNNUM"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2CV43442");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_MR_2BF50353"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 확인
        private void UP_RUN()
        {
            try
            {
                this.FPS91_TY_S_UT_B3CGG940.Initialize();

                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_B3CGD939", this.CBH01_DTNCODE.GetValue().ToString());
                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.CurrentDataTableRowMapping(dt, "01");

                    this.FPS91_TY_S_UT_B3CGG940.SetValue(dt);
                }
                else
                {
                    int iLength = 0;

                    iLength = this.CBH01_DTNCODE.GetValue().ToString().Length;

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_B1PB3451", this.CBH01_DTNCODE.GetValue().ToString().Substring(1, iLength - 1));
                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.CurrentDataTableRowMapping(dt, "01");
                    }
                }
            }
            catch (Exception ex)
            {
                string a = string.Empty;

                a = ex.ToString();
            }
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion
    }
}
