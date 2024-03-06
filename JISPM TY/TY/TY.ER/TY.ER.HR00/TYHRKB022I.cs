using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 상해보험 지급내역관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.03.21 13:14
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_73LDC033 : 상해보험 지급내역 등록
    ///  TY_P_HR_73LDE034 : 상해보험 지급내역 수정
    ///  TY_P_HR_73LDE036 : 상해보험 지급내역 확인
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  INMSABUN : 계약자
    ///  INMDATE : 접수일자
    ///  INMPYDATE : 지급일자
    ///  INMREQDATE : 청구일자
    ///  INMBIGO : 비고
    ///  INMDISNAME : 진단명
    ///  INMHOSP : 치료병원
    ///  INMPJUSO : 주소
    ///  INMPNAME : 피보험자
    ///  INMPTEL : 연락처
    ///  INMPYAMOUNT : 지급금액
    ///  INMREQAMOUNT : 청구금액
    /// </summary>
    public partial class TYHRKB022I : TYBase
    {
        private string fsINMDATE;
        private string fsINMSABUN;

        #region  Description : 폼 로드 이벤트
        public TYHRKB022I(string sINMDATE, string sINMSABUN)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsINMDATE = sINMDATE;
            fsINMSABUN = sINMSABUN;

        }

        private void TYHRKB022I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            if (string.IsNullOrEmpty(fsINMDATE))
            {
                DTP01_INMDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
                DTP01_INMREQDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
                DTP01_INMPYDATE.SetValue("");

                this.SetStartingFocus(DTP01_INMDATE);
            }
            else
            {
                DTP01_INMDATE.SetValue(fsINMDATE);
                CBH01_INMSABUN.SetValue(fsINMSABUN);

                UP_Run();

                this.SetStartingFocus(TXT01_INMPNAME);
            }
        }
        #endregion

        #region  Description : 확인 함수 이벤트
        private void UP_Run()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_73LDE036", TYUserInfo.SecureKey, TYUserInfo.PerAuth, DTP01_INMDATE.GetString().ToString(), CBH01_INMSABUN.GetValue());
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");
            }
        }
        #endregion


        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            if (string.IsNullOrEmpty(fsINMDATE))
            {
                this.DbConnector.Attach("TY_P_HR_73LDC033", DTP01_INMDATE.GetString().ToString(), 
                                                            CBH01_INMSABUN.GetValue(),
                                                            TXT01_INMPNAME.GetValue(),
                                                            MTB01_INMPJUMIN.GetValue().ToString().Replace("-",""),
                                                            TYUserInfo.SecureKey,
                                                            TXT01_INMPJUSO.GetValue(),
                                                            TXT01_INMPTEL.GetValue(),
                                                            TXT01_INMHOSP.GetValue(),
                                                            TXT01_INMDISNAME.GetValue(),
                                                            DTP01_INMREQDATE.GetString().ToString().Replace("19000101",""),
                                                            Get_Numeric(TXT01_INMREQAMOUNT.GetValue().ToString()),
                                                            DTP01_INMPYDATE.GetString().ToString().Replace("19000101",""),
                                                            Get_Numeric(TXT01_INMPYAMOUNT.GetValue().ToString()),
                                                            TXT01_INMBIGO.GetValue().ToString(),
                                                            TYUserInfo.EmpNo
                                                            );
            }
            else
            {
                this.DbConnector.Attach("TY_P_HR_73LDE034", TXT01_INMPNAME.GetValue(),
                                                            MTB01_INMPJUMIN.GetValue().ToString().Replace("-", ""),
                                                            TYUserInfo.SecureKey,
                                                            TXT01_INMPJUSO.GetValue(),
                                                            TXT01_INMPTEL.GetValue(),
                                                            TXT01_INMHOSP.GetValue(),
                                                            TXT01_INMDISNAME.GetValue(),
                                                            DTP01_INMREQDATE.GetString().ToString().Replace("19000101", ""),
                                                            Get_Numeric(TXT01_INMREQAMOUNT.GetValue().ToString()),
                                                            DTP01_INMPYDATE.GetString().ToString().Replace("19000101", ""),
                                                            Get_Numeric(TXT01_INMPYAMOUNT.GetValue().ToString()),
                                                            TXT01_INMBIGO.GetValue().ToString(),
                                                            TYUserInfo.EmpNo,
                                                            DTP01_INMDATE.GetString().ToString(),
                                                            CBH01_INMSABUN.GetValue()
                                                            );
            }
            this.DbConnector.ExecuteTranQuery();

            this.ShowMessage("TY_M_GB_23NAD873");

            BTN61_CLO_Click(null, null);
        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
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
