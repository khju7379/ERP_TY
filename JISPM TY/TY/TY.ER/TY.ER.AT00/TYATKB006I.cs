using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AT00
{
    /// <summary>
    /// 세대별 월별요금관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2018.09.20 14:15
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_89KFB733 : 세대별 월별요금관리 조회
    ///  TY_P_HR_89RI7747 : 사택 세대별요금관리 내역 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_89RI8748 : 세대별요금관리 내역 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  REM : 삭제
    ///  SAV : 저장
    ///  AMRBILDATE : 발행일자
    ///  AMRNILDATE : 납기일자
    ///  AMRYYMM : 작업년월
    ///  AMRHOSU : 호 수
    ///  AMRLATEAMT : 연체료
    ///  AMRNABGIGN : 납기내구분
    ///  AMRSUNAPCHK : 수납구분
    ///  AMRTOTALAMT : 합계금액
    /// </summary>
    public partial class TYATKB006I : TYBase
    {
        private string fsAMRYYMM;
        private string fsAMRHOSU;


        #region  Description : 폼 로드 이벤트
        public TYATKB006I(string sAMRYYMM, string sAMRHOSU)
        {
            InitializeComponent();

            fsAMRYYMM = sAMRYYMM;
            fsAMRHOSU = sAMRHOSU;

            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_89RI8748, "ASRCODE", "ASRCODENM", "ASRCODE");  
        }

        private void TYATKB006I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_89RI8748, "ASRCODE");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_89RI8748, "ASRCODENM");

            this.DTP01_AMRYYMM.SetValue(fsAMRYYMM);
            this.TXT01_AMRHOSU.SetValue(fsAMRHOSU);


            this.DTP01_AMRYYMM.SetReadOnly(true);
            //this.DTP01_AMRBILDATE.SetReadOnly(true);
            //this.DTP01_AMRNILDATE.SetReadOnly(true);
            
            this.UP_DataBinding();
        }
        #endregion

        #region  Description : UP_DataBinding 이벤트
        private void UP_DataBinding()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_89KFB733", this.DTP01_AMRYYMM.GetString().ToString().Substring(0, 6), this.TXT01_AMRHOSU.GetValue().ToString() , "");
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                this.DTP01_AMRYYMM.SetValue(dt.Rows[0]["AMRYYMM"].ToString());
                this.TXT01_AMRHOSU.SetValue(dt.Rows[0]["AMRHOSU"].ToString());
                this.DTP01_AMRBILDATE.SetValue(dt.Rows[0]["AMRBILDATE"].ToString());
                this.DTP01_AMRNILDATE.SetValue(dt.Rows[0]["AMRNILDATE"].ToString());
                this.TXT01_AMRTOTALAMT.SetValue(dt.Rows[0]["AMRTOTALAMT"].ToString());
                this.TXT01_AMRLATEAMT.SetValue(dt.Rows[0]["AMRLATEAMT"].ToString());
                this.CBO01_AMRNABGIGN.SetValue(dt.Rows[0]["AMRNABGIGN"].ToString());
                this.CBO01_AMRSUNAPCHK.SetValue(dt.Rows[0]["AMRSUNAPCHK"].ToString());
                if (dt.Rows[0]["AMRSUNAPCHK"].ToString() == "Y")
                {
                    this.DTP01_AMRSUNAPDATE.SetValue(dt.Rows[0]["AMRSUNAPDATE"].ToString());
                }
                else
                {
                    this.DTP01_AMRSUNAPDATE.SetValue("");
                }
            }

            this.FPS91_TY_S_HR_89RI8748.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_89RI7747", this.DTP01_AMRYYMM.GetString().ToString().Substring(0, 6), this.TXT01_AMRHOSU.GetValue().ToString());
            this.FPS91_TY_S_HR_89RI8748.SetValue(this.DbConnector.ExecuteDataTable());
            if (this.FPS91_TY_S_HR_89RI8748.CurrentRowCount > 0)
            {

                this.SpreadSumRowAdd(this.FPS91_TY_S_HR_89RI8748, "ASRCODENM", "합   계", SumRowType.Sum, "ASRCODEAMT");
            }               


        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.Attach
                    (
                    "TY_P_HR_89SBB753",
                    ds.Tables[0].Rows[i]["ASRYYMM"].ToString(),
                    ds.Tables[0].Rows[i]["ASRHOSU"].ToString(),
                    ds.Tables[0].Rows[i]["ASRCODE"].ToString()
                    );
            }

            this.DbConnector.ExecuteTranQueryList();

            UP_DataBinding();

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            // 스프레드에서 삭제 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_HR_89RI8748.GetDataSourceInclude(TSpread.TActionType.Remove, "ASRYYMM", "ASRHOSU", "ASRCODE"));

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

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // 신규 등록
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_89SB1751", ds.Tables[0].Rows[i]["ASRYYMM"].ToString(),
                                                                ds.Tables[0].Rows[i]["ASRHOSU"].ToString(),
                                                                ds.Tables[0].Rows[i]["ASRCODE"].ToString(),
                                                                Get_Numeric(ds.Tables[0].Rows[i]["ASRCODEAMT"].ToString()),
                                                                ds.Tables[0].Rows[i]["ASRCALBASE"].ToString(),
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
                    this.DbConnector.Attach("TY_P_HR_89SB2752", Get_Numeric(ds.Tables[1].Rows[i]["ASRCODEAMT"].ToString()),
                                                                ds.Tables[1].Rows[i]["ASRCALBASE"].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["ASRYYMM"].ToString(),
                                                                ds.Tables[1].Rows[i]["ASRHOSU"].ToString(),
                                                                ds.Tables[1].Rows[i]["ASRCODE"].ToString()
                                                                );
                }

                this.DbConnector.ExecuteTranQueryList();
                
            }

            this.UP_Set_MasterUpadte();

            this.UP_DataBinding();

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_8AFAM948", this.TXT01_AMRHOSU.GetValue().ToString());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowCustomMessage("존재하지않는 호수 입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            DataSet ds = new DataSet();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_HR_89RI8748.GetDataSourceInclude(TSpread.TActionType.New, "ASRYYMM", "ASRHOSU", "ASRCODE", "ASRCODEAMT", "ASRCALBASE"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_HR_89RI8748.GetDataSourceInclude(TSpread.TActionType.Update, "ASRYYMM", "ASRHOSU", "ASRCODE", "ASRCODEAMT", "ASRCALBASE"));

            //if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            //{
            //    this.ShowMessage("TY_M_GB_2452W459");
            //    e.Successed = false;
            //    return;
            //}

            //수납구분이 Y 이면 
            //if (this.CBO01_AMRSUNAPCHK.GetValue().ToString() == "Y" && this.DTP01_AMRSUNAPDATE.GetString().ToString().Trim() == "19000101")
            //{
            //    this.SetFocus(this.DTP01_AMRSUNAPDATE);
            //    this.ShowCustomMessage("수납일자를 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //    e.Successed = false;
            //    return; 
            //}

            if (this.CBO01_AMRSUNAPCHK.GetValue().ToString() != "Y")
            {
                this.DTP01_AMRSUNAPDATE.SetValue("");
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;          
        }
        #endregion

        #region  Description : 세대별요금관리 마스타 금액 UPDATE
        private void UP_Set_MasterUpadte()
        {
            string sAMRSUNAPDATE = string.Empty;

            if (DTP01_AMRSUNAPDATE.GetValue().ToString() == "1900-01-01" || DTP01_AMRSUNAPDATE.GetValue().ToString() == "")
            {
                sAMRSUNAPDATE = "";
            }
            else
            {
                sAMRSUNAPDATE = DTP01_AMRSUNAPDATE.GetString();
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_89SGP763", this.CBO01_AMRNABGIGN.GetValue().ToString(),
                                                        this.CBO01_AMRSUNAPCHK.GetValue().ToString(),
                                                        this.CBO01_AMRSUNAPCHK.GetValue().ToString() == "Y" ? sAMRSUNAPDATE : "",  
                                                        this.DTP01_AMRBILDATE.GetString(),
                                                        this.DTP01_AMRNILDATE.GetString(),
                                                        TYUserInfo.EmpNo, 
                                                        this.DTP01_AMRYYMM.GetString().ToString().Substring(0, 6), 
                                                        this.TXT01_AMRHOSU.GetValue().ToString());
            this.DbConnector.ExecuteTranQuery();
        }
        #endregion

        #region  Description : FPS91_TY_S_HR_89RI8748_RowInserted 이벤트
        private void FPS91_TY_S_HR_89RI8748_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_HR_89RI8748.SetValue(e.RowIndex, "ASRYYMM", this.DTP01_AMRYYMM.GetString().ToString().Substring(0, 6));
            this.FPS91_TY_S_HR_89RI8748.SetValue(e.RowIndex, "ASRHOSU", this.TXT01_AMRHOSU.GetValue().ToString());
        }
        #endregion

        #region  Description : CBO01_AMRSUNAPCHK_SelectedValueChanged 이벤트
        private void CBO01_AMRSUNAPCHK_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.CBO01_AMRSUNAPCHK.GetValue().ToString() == "Y")
            {
                DTP01_AMRSUNAPDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            }
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

    }
}
