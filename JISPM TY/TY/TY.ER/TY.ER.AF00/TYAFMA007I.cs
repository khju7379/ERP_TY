using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.Drawing; 

namespace TY.ER.AF00
{
    /// <summary>
    /// EIS 계열사 계획금액 관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2013.10.07 11:12
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_3A719972 : EIS 마감 조회(최종 마감월)
    ///  
    ///  TY_P_AC_3A72E973 : EIS 계열사 계획금액 수정
    ///  TY_P_AC_3AF3H056 : EIS 계열사 계획금액 수정 (TG)
    ///  
    ///  TY_P_AC_3A72I974 : EIS 계열사 계획금액 확정처리
    ///  TY_P_AC_3AF3J057 : EIS 계열사 계획금액 확정처리(TG)
    ///  
    ///  TY_P_AC_3A7A1970 : EIS 계열사 계획금액 조회
    ///  TY_P_AC_3AF3F055 : EIS 계열사 계획금액 조회 (TG)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_3A7A0971 : EIS 계열사 계획금액 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_AC_25F59464 : 선택한 자료가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    ///  TY_M_MR_2BD3Y285 : 수정하시겠습니까?
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  SAV : 저장
    ///  ESMCUST : 계열사구분
    ///  ESMYYHD : 처리년
    /// </summary>
    public partial class TYAFMA007I : TYBase
    {
        private string fsCompanyCode;

        #region  Description : 폼 로드 이벤트
        public TYAFMA007I()
        {
            InitializeComponent();
        }

        private void TYAFMA007I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.TXT01_ESMYYHD.SetValue(DateTime.Now.ToString("yyyy"));

            switch( TYUserInfo.EmpNo.Substring(0,2) )
            {
                case "HT":
                    fsCompanyCode = "TH";
                    break;
                case "TG":
                    fsCompanyCode = "TG";
                    break;
                case "TS":
                    fsCompanyCode = "TS";
                    break;
                case "TL":
                    fsCompanyCode = "TL";
                    break;
                default:
                    fsCompanyCode = "";  
                    break;  
            }

            if (fsCompanyCode != "")
            {
                CBH01_ESMCUST.SetValue(fsCompanyCode);
                CBH01_ESMCUST.SetReadOnly(true);                 
            }

            if (fsCompanyCode != "")
            {
                this.SetStartingFocus(this.TXT01_ESMYYHD);
            }
            else
            {
                this.SetStartingFocus(this.CBH01_ESMCUST.CodeText);
            }
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            int iMonth = 5;
            int j = 0;

            this.FPS91_TY_S_AC_3A7A0971.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            if (this.CBH01_ESMCUST.GetValue().ToString() == "TG")
            {
                this.DbConnector.Attach("TY_P_AC_3AF3F055", this.CBH01_ESMCUST.GetValue(), this.TXT01_ESMYYHD.GetValue()); // 그레인터미널
            }
            else
            {
                this.DbConnector.Attach("TY_P_AC_3A7A1970", this.CBH01_ESMCUST.GetValue(), this.TXT01_ESMYYHD.GetValue());
            }

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_3A7A0971.SetValue(dt);

            // 마감 월 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_3A719972", this.TXT01_ESMYYHD.GetValue());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                iMonth = int.Parse(dt.Rows[0]["ECMONTH"].ToString()) + 5;
            }

            for (int i = 0; i < FPS91_TY_S_AC_3A7A0971.CurrentRowCount; i++)
            {
                // TAG = 'Y'이면 잠금
                if (this.FPS91_TY_S_AC_3A7A0971.GetValue(i, "EPCTAG02").ToString() == "Y")
                {
                    // 마감 월 잠금
                    for (j = iMonth; j <= 16; j++)
                    {
                        this.FPS91_TY_S_AC_3A7A0971_Sheet1.Cells[i, j].Locked = false;
                    }
                }
                else
                {
                    this.FPS91_TY_S_AC_3A7A0971.ActiveSheet.Rows[i].Locked = true;
                }
            }

            // 마감 월 잠금
            for (j = 5; j < iMonth; j++)
            {
                this.FPS91_TY_S_AC_3A7A0971.ActiveSheet.Columns[j].BackColor = Color.FromArgb(218, 239, 194);
            }

            this.FPS91_TY_S_AC_3A7A0971.ActiveSheet.Columns["HAP"].BackColor = Color.FromArgb(254, 209, 164);

            UP_SumRowAdd();
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            int i = 0;
            string sOUTMSG = string.Empty;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            // 임원 겸직 및 경력 현황
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                if (this.CBH01_ESMCUST.GetValue().ToString() == "TG")  // 그레인터미널
                {
                    this.DbConnector.Attach("TY_P_AC_3AF3H056", ds.Tables[0].Rows[i]["ESMPL01"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESMPL02"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESMPL03"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESMPL04"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESMPL05"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESMPL06"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESMPL07"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESMPL08"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESMPL09"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESMPL10"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESMPL11"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESMPL12"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESMCUST"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESMYYHD"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESMCDAC"].ToString()
                                                                );
                }
                else
                {
                    this.DbConnector.Attach("TY_P_AC_3A72E973", ds.Tables[0].Rows[i]["ESMPL01"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESMPL02"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESMPL03"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESMPL04"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESMPL05"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESMPL06"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESMPL07"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESMPL08"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESMPL09"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESMPL10"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESMPL11"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESMPL12"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESMCUST"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESMYYHD"].ToString(),
                                                                ds.Tables[0].Rows[i]["ESMCDAC"].ToString()
                                                                );
                }
            }

            this.DbConnector.ExecuteTranQueryList();

            // 상위계정 계산

            if (this.CBH01_ESMCUST.GetValue().ToString() == "TG")  // 그레인터미널
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3AF3J057",
                    this.TXT01_ESMYYHD.GetValue(),
                    this.CBH01_ESMCUST.GetValue(),
                    "PL",
                    sOUTMSG.ToString()
                    );
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3A72I974",
                    this.TXT01_ESMYYHD.GetValue(),
                    this.CBH01_ESMCUST.GetValue(),
                    "PL",
                    sOUTMSG.ToString()
                    );
            }

            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUTMSG.ToString() != "")
            {
                if (sOUTMSG.Substring(0, 2) == "OK")
                {
                    // 수정 메세지
                    this.ShowMessage("TY_M_GB_23NAD873");
                    this.BTN61_INQ_Click(null, null);
                }
                else
                {
                    this.ShowMessage("TY_M_AC_246A2488");
                }
            }
            else
            {
                this.ShowMessage("TY_M_AC_246A2488");
            }
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            // ------------------------   마감 완료 CHECK 시작  ------------------------------------------ //

            //this.DbConnector.CommandClear(); // TY_P_AC_27H64059
            //this.DbConnector.Attach("TY_P_AC_3C92V659", this.TXT01_ESMYYHD.GetValue().ToString().Substring(0, 4), "01");
            //DataTable dt1 = this.DbConnector.ExecuteDataTable();

            //if (dt1.Rows.Count == 0)
            //{
            //    this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
            //    e.Successed = false;
            //    return;
            //}
            //else
            //{
            //    if (dt1.Rows[0]["ECSBBUN"].ToString() == "Z")
            //    {
            //        this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
            //        e.Successed = false;
            //        return;
            //    }
            //}

            // ------------------------   마감 완료 CHECK 끝 ------------------------------------------ //

            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_AC_3A7A0971.GetDataSourceInclude(TSpread.TActionType.Update, "ESMCUST", "ESMYYHD", "ESMCDAC", "ESMPL01", "ESMPL02", "ESMPL03", "ESMPL04", "ESMPL05", "ESMPL06", "ESMPL07", "ESMPL08", "ESMPL09", "ESMPL10", "ESMPL11", "ESMPL12"));

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

        #region Description : 특정 Row와 Column 값 변경
        private void UP_SumRowAdd()
        {
            int i = 0;

            this.SpreadSumRowAdd(this.FPS91_TY_S_AC_3A7A0971, "ESMYYHD", "합 계", Color.Yellow);

            for (i = 0; i < this.FPS91_TY_S_AC_3A7A0971.ActiveSheet.RowCount; i++)
            {
                this.FPS91_TY_S_AC_3A7A0971_Sheet1.SetFormula(
                    i, // row
                    17, // column
                    "R[0]C[-1] + R[0]C[-2] + R[0]C[-3] + R[0]C[-4] + R[0]C[-5] + R[0]C[-6] + R[0]C[-7] + R[0]C[-8] + R[0]C[-9] + R[0]C[-10] + R[0]C[-11] + R[0]C[-12]"); //
            }

            this.FPS91_TY_S_AC_3A7A0971.ActiveSheet.Rows[i - 1].Visible = false;
        }
        #endregion
    }
}
