using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.MR00
{
    /// <summary>
    /// 품목 코드 관리(팝업) 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.11.06 13:01
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_2B62Y135 : 품목코드 등록
    ///  TY_P_MR_2B62Z136 : 품목코드 수정
    ///  TY_P_MR_2B631138 : 품목코드 확인
    ///  TY_P_MR_2B68N147 : 품목코드 순번 가져오기
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  Z105035 : 처리구분
    ///  Z105037 : 구매방법
    ///  Z105068 : 거래처
    ///  Z105000 : 대분류코드
    ///  Z105001 : 중분류코드
    ///  Z105002 : 소분류코드
    ///  Z105003 : 품목순번
    ///  Z105004 : HS-CODE
    ///  Z105013 : 자재명１
    ///  Z105015 : 자재명２
    ///  Z105023 : 단위
    ///  Z105025 : 포장단위
    ///  Z105029 : 규격1
    ///  Z105030 : 규격2
    ///  Z105038 : 용도구분
    ///  Z105039 : 구매소요일
    ///  Z105049 : 제작회사
    ///  Z105057 : 최종구매일
    ///  Z105059 : 최종구매단가
    ///  Z105061 : 최종출고일
    ///  Z105065 : 최소재고
    ///  Z105067 : 최대재고
    ///  Z105998 : 비품 (Y)
    /// </summary>
    public partial class TYMRCO006I : TYBase
    {
        private string fsZ105000;
        private string fsZ105001;
        private string fsZ105002;
        public  string fsZ105003;

        private string fsLMDESC;
        private string fsMMDESC;
        private string fsSMDESC;

        #region Description : 페이지 로드
        public TYMRCO006I(string sZ105000, string sZ105001, string sZ105002, string sZ105003, string sLMDESC, string sMMDESC, string sSMDESC)
        {
            InitializeComponent();

            this.SetPopupStyle();

            // 파라미터값 가져오기
            this.fsZ105000 = sZ105000;
            this.fsZ105001 = sZ105001;
            this.fsZ105002 = sZ105002;
            this.fsZ105003 = sZ105003;

            this.fsLMDESC = sLMDESC;
            this.fsMMDESC = sMMDESC;
            this.fsSMDESC = sSMDESC;

            this.TXT01_Z105000.SetValue(fsZ105000);
            this.TXT01_Z105001.SetValue(fsZ105001);
            this.TXT01_Z105002.SetValue(fsZ105002);
            this.TXT01_Z105003.SetValue(fsZ105003);
        }

        private void TYMRCO006I_Load(object sender, System.EventArgs e)
        {
            this.TXT01_Z105000.SetReadOnly(true);
            this.TXT01_Z105001.SetReadOnly(true);
            this.TXT01_Z105002.SetReadOnly(true);
            this.TXT01_Z105003.SetReadOnly(true);

            this.TXT01_Z105061.SetReadOnly(true);

            this.CBO01_Z105000.SetReadOnly(true);
            this.CBO01_Z105001.SetReadOnly(true);
            this.CBO01_Z105002.SetReadOnly(true);

            this.TXT01_Z105061.SetReadOnly(true);
            this.TXT01_Z105065.SetReadOnly(true);
            this.TXT01_Z105067.SetReadOnly(true);
            this.TXT01_Z105057.SetReadOnly(true);
            this.TXT01_Z105059.SetReadOnly(true);

            this.TXT01_Z106000.SetReadOnly(true);

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            UP_SetZ105000();
            this.CBO01_Z105000.SetValue(fsZ105000);
            this.CBO01_Z105001.SetValue(fsZ105001);
            this.CBO01_Z105002.SetValue(fsZ105002);

            if (string.IsNullOrEmpty(this.fsZ105003)) // 등록
            {
                if (string.IsNullOrEmpty(this.fsZ105000) && string.IsNullOrEmpty(this.fsZ105001) && string.IsNullOrEmpty(this.fsZ105002))
                {
                    this.CBO01_Z105000.SetReadOnly(false);
                    this.CBO01_Z105001.SetReadOnly(false);
                    this.CBO01_Z105002.SetReadOnly(false);
                }
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2B631138",
                    this.TXT01_Z105000.GetValue(),
                    this.TXT01_Z105001.GetValue(),
                    this.TXT01_Z105002.GetValue(),
                    this.TXT01_Z105003.GetValue()
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                    this.CurrentDataTableRowMapping(dt, "01");
            }
            SetStartingFocus(this.TXT01_Z105013);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            if (string.IsNullOrEmpty(this.fsZ105003)) // 등록
            {
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2B62Y135",
                    this.TXT01_Z105000.GetValue(),
                    this.TXT01_Z105001.GetValue(),
                    this.TXT01_Z105002.GetValue(),
                    this.TXT01_Z105003.GetValue(),
                    this.TXT01_Z105004.GetValue(),
                    this.TXT01_Z105013.GetValue(),
                    this.TXT01_Z105015.GetValue(),
                    this.CBH01_Z105023.GetValue(),
                    this.TXT01_Z105025.GetValue(),
                    this.TXT01_Z105029.GetValue(),
                    this.TXT01_Z105030.GetValue(),
                    this.CBH01_Z105035.GetValue(),
                    this.CBH01_Z105037.GetValue(),
                    this.TXT01_Z105038.GetValue(),
                    this.TXT01_Z105039.GetValue(),
                    this.TXT01_Z105049.GetValue(),
                    this.TXT01_Z105057.GetValue().ToString().Replace("-", ""),
                    this.TXT01_Z105059.GetValue(),
                    //this.TXT01_Z105060, // 화폐 단위
                    this.TXT01_Z105061.GetValue().ToString().Replace("-", ""),
                    this.TXT01_Z105065.GetValue(),
                    this.TXT01_Z105067.GetValue(),
                    this.CBH01_Z105068.GetValue(),
                    TYUserInfo.EmpNo.ToString(),
                    DateTime.Now.ToString("HHmmss").ToString(),
                    this.CBO01_Z105998.GetValue()
                    );
            }
            else // 수정
            {
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2B62Z136",
                    this.TXT01_Z105004.GetValue(),
                    this.TXT01_Z105013.GetValue(),
                    this.TXT01_Z105015.GetValue(),
                    this.CBH01_Z105023.GetValue(),
                    this.TXT01_Z105025.GetValue(),
                    this.TXT01_Z105029.GetValue(),
                    this.TXT01_Z105030.GetValue(),
                    this.CBH01_Z105035.GetValue(),
                    this.CBH01_Z105037.GetValue(),
                    this.TXT01_Z105038.GetValue(),
                    this.TXT01_Z105039.GetValue(),
                    this.TXT01_Z105049.GetValue(),
                    this.TXT01_Z105057.GetValue().ToString().Replace("-",""),
                    this.TXT01_Z105059.GetValue(),
                    //this.TXT01_Z105060, // 화폐 단위
                    this.TXT01_Z105061.GetValue().ToString().Replace("-", ""),
                    this.TXT01_Z105065.GetValue(),
                    this.TXT01_Z105067.GetValue(),
                    this.CBH01_Z105068.GetValue(),
                    TYUserInfo.EmpNo.ToString(),
                    DateTime.Now.ToString("HHmmss").ToString(),
                    this.CBO01_Z105998.GetValue(),
                    this.TXT01_Z105000.GetValue(),
                    this.TXT01_Z105001.GetValue(),
                    this.TXT01_Z105002.GetValue(),
                    this.TXT01_Z105003.GetValue()
                    );
            }

            this.DbConnector.ExecuteNonQuery();

            this.ShowMessage("TY_M_GB_23NAD873");
            this.Close();
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (string.IsNullOrEmpty(this.fsZ105003)) // 등록
            {
                // 품목 순번 가져오기(MAX(Z105003) + 1)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2B68N147",
                    TXT01_Z105000.GetValue().ToString(),
                    TXT01_Z105001.GetValue().ToString(),
                    TXT01_Z105002.GetValue().ToString()
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_Z105003.SetValue(Set_Fill5(dt.Rows[0]["Z105003"].ToString()));
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 품목사진보기
        private void BTN61_SEARCH_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.fsZ105003)) // 등록
            {
                // 품목 순번 가져오기(MAX(Z105003) + 1)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2B68N147",
                    TXT01_Z105000.GetValue().ToString(),
                    TXT01_Z105001.GetValue().ToString(),
                    TXT01_Z105002.GetValue().ToString()
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    fsZ105003 = Set_Fill5(dt.Rows[0]["Z105003"].ToString());
                    this.TXT01_Z105003.SetValue(Set_Fill5(dt.Rows[0]["Z105003"].ToString()));
                }

                // 등록
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2B62Y135",
                    this.TXT01_Z105000.GetValue(),
                    this.TXT01_Z105001.GetValue(),
                    this.TXT01_Z105002.GetValue(),
                    this.TXT01_Z105003.GetValue(),
                    this.TXT01_Z105004.GetValue(),
                    this.TXT01_Z105013.GetValue(),
                    this.TXT01_Z105015.GetValue(),
                    this.CBH01_Z105023.GetValue(),
                    this.TXT01_Z105025.GetValue(),
                    this.TXT01_Z105029.GetValue(),
                    this.TXT01_Z105030.GetValue(),
                    this.CBH01_Z105035.GetValue(),
                    this.CBH01_Z105037.GetValue(),
                    this.TXT01_Z105038.GetValue(),
                    this.TXT01_Z105039.GetValue(),
                    this.TXT01_Z105049.GetValue(),
                    this.TXT01_Z105057.GetValue().ToString().Replace("-", ""),
                    this.TXT01_Z105059.GetValue(),
                    //this.TXT01_Z105060, // 화폐 단위
                    this.TXT01_Z105061.GetValue().ToString().Replace("-", ""),
                    this.TXT01_Z105065.GetValue(),
                    this.TXT01_Z105067.GetValue(),
                    this.CBH01_Z105068.GetValue(),
                    TYUserInfo.EmpNo.ToString(),
                    DateTime.Now.ToString("HHmmss").ToString(),
                    this.CBO01_Z105998.GetValue()
                    );

                this.DbConnector.ExecuteNonQuery();
            }

            string sJPCODE = string.Empty;

            // 제품코드
            sJPCODE = this.TXT01_Z105000.GetValue().ToString() + this.TXT01_Z105001.GetValue().ToString() + this.TXT01_Z105002.GetValue().ToString() + this.TXT01_Z105003.GetValue().ToString();

            if ((new TYMRCO007I(sJPCODE.ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2B631138",
                    this.TXT01_Z105000.GetValue(),
                    this.TXT01_Z105001.GetValue(),
                    this.TXT01_Z105002.GetValue(),
                    this.TXT01_Z105003.GetValue()
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                    this.CurrentDataTableRowMapping(dt, "01");
            }
        }
        #endregion

        #region Description : 대분류코드 데이터 조회
        private void UP_SetZ105000()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2B24C040"
                );

            dt = this.DbConnector.ExecuteDataTable();
            this.CBO01_Z105000.DataBind(dt, true);
        }
        #endregion

        #region Descriptoin : 대분류코드 이벤트
        private void CBO01_Z105000_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2B24D041",
                this.CBO01_Z105000.GetValue()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.CBO01_Z105001.DataBind(dt, true);
            this.TXT01_Z105000.SetValue(this.CBO01_Z105000.GetValue());
        }
        #endregion

        #region Descriptoin : 중분류코드 이벤트
        private void CBO01_Z105001_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2B24D042",
                this.CBO01_Z105000.GetValue(),
                this.CBO01_Z105001.GetValue()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.CBO01_Z105002.DataBind(dt, true);
            this.TXT01_Z105001.SetValue(this.CBO01_Z105001.GetValue());
        }
        #endregion

        #region Descriptoin : 소분류코드 이벤트
        private void CBO01_Z105002_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.TXT01_Z105002.SetValue(this.CBO01_Z105002.GetValue());
        }
        #endregion
    }
}