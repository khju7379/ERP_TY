using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.ED00
{
    /// <summary>
    /// 반출통고목록보고서 등록 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2020.05.25 13:06
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_A5QDE558 : 반출통고목록보고서 등록
    ///  TY_P_UT_A5QDE559 : 반출통고목록보고서 수정
    ///  TY_P_UT_A5QDH561 : 반출통고목록보고서 확인
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
    ///  EDNADDR : 기본주소
    ///  EDNBLHSN : HSN
    ///  EDNBLMSN : MSN
    ///  EDNBLNO : BL
    ///  EDNBOLOC : 반입장소
    ///  EDNBUILDNUM : 건물관리번호
    ///  EDNCHDATE : 반출통고일자
    ///  EDNCNT : 반입개수
    ///  EDNDATE : 등록일자
    ///  EDNDHDATE : 체화예정일자
    ///  EDNGJ : 공장
    ///  EDNHJBIRTH : 화주생년월일
    ///  EDNHWAJU : 화주
    ///  EDNHWAJUNM : 화주명
    ///  EDNHWAMUL : 화물
    ///  EDNIPHANG : 입항일
    ///  EDNIPQTY : 반입중량
    ///  EDNJSGB : 전송구분
    ///  EDNJUKHA : 적하목록
    ///  EDNNO1 : NO1
    ///  EDNNO2 : NO2
    ///  EDNNO3 : NO3
    ///  EDNPOST : 우편번호
    ///  EDNSEQ : 순번
    ///  EDNTEL : 연락처
    /// </summary>
    public partial class TYEDKB015I : TYBase
    {
        private string fsEDNDATE;
        private string fsEDNSEQ;
        private string fsEDNJSGB;

        #region  Description : 폼 로드 이벤트
        public TYEDKB015I(string sEDNDATE, string sEDNSEQ, string sEDNJSGB)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsEDNDATE = sEDNDATE;
            fsEDNSEQ = sEDNSEQ;
            fsEDNJSGB = sEDNJSGB;
        }

        private void TYEDKB015I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            BTN62_INQOPTION.Image = global::TY.Service.Library.Properties.Resources.magnifier;
            BTN62_INQOPTION.Text = "";

            UP_SetLockCheck();

            DTP01_EDNIPHANG.SetReadOnly(true);
            CBO01_EDNJSGB.SetReadOnly(true);

            if (string.IsNullOrEmpty(this.fsEDNDATE))
            {
                DTP01_EDNDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
                DTP01_EDNCHDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
                DTP01_EDNDHDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            }
            else
            {
                DTP01_EDNDATE.SetValue(fsEDNDATE);
                TXT01_EDNSEQ.SetValue(fsEDNSEQ);
                CBO01_EDNJSGB.SetValue(fsEDNJSGB);

                DTP01_EDNDATE.SetReadOnly(true);
                

                UP_DataBinding();
            }

        }
        #endregion

        #region  Description : 데이터 바인딩 이벤트
        private void UP_DataBinding()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_A5QDH561", DTP01_EDNDATE.GetString(), TXT01_EDNSEQ.GetValue(), CBO01_EDNJSGB.GetValue());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
                    this.CurrentDataTableRowMapping(dt, "01");

            if (dt.Rows[0]["EDNRCVGB"].ToString() == "Y")
            {
                this.BTN61_SAV.Visible = false;
            }

        }        
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {

            this.DbConnector.CommandClear();
            if (string.IsNullOrEmpty(this.fsEDNDATE))
            {

                this.DbConnector.Attach("TY_P_UT_A5QDE558", DTP01_EDNDATE.GetString().ToString(),
                                                            TXT01_EDNSEQ.GetValue().ToString(),
                                                            CBO01_EDNJSGB.GetValue(),
                                                            CBO01_EDNGJ.GetValue(),
                                                            TXT01_EDNJUKHA.GetValue(),
                                                            TXT01_EDNBLMSN.GetValue(),
                                                            TXT01_EDNBLHSN.GetValue(),
                                                            DTP01_EDNIPHANG.GetValue(),
                                                            CBH01_EDNBONSUN.GetValue(),
                                                            CBH01_EDNHWAJU.GetValue(),
                                                            CBH01_EDNHWAMUL.GetValue(),
                                                            TXT01_EDNBLNO.GetValue(),                                                            
                                                            DTP01_EDNCHDATE.GetString(),
                                                            DTP01_EDNDHDATE.GetString(),
                                                            Get_Numeric(TXT01_EDNCNT.GetValue().ToString()),
                                                            Get_Numeric(TXT01_EDNIPQTY.GetValue().ToString()),
                                                            TXT01_EDNBOLOC.GetValue(),
                                                            TXT01_EDNHWAJUNM.GetValue(),
                                                            MTB01_EDNHJBIRTH.GetValue().ToString().Replace("-",""),
                                                            TXT01_EDNTEL.GetValue(),
                                                            TXT01_EDNPOST.GetValue(),
                                                            TXT01_EDNADDR.GetValue(),
                                                            "",
                                                            "",
                                                            "",
                                                            "",
                                                            "",
                                                            TYUserInfo.EmpNo
                    );

            }
            else
            {
                this.DbConnector.Attach("TY_P_UT_A5QDE559", CBO01_EDNGJ.GetValue(),
                                                            TXT01_EDNJUKHA.GetValue(),
                                                            TXT01_EDNBLMSN.GetValue(),
                                                            TXT01_EDNBLHSN.GetValue(),
                                                            DTP01_EDNIPHANG.GetValue(),
                                                            CBH01_EDNBONSUN.GetValue(),
                                                            CBH01_EDNHWAJU.GetValue(),
                                                            CBH01_EDNHWAMUL.GetValue(),
                                                            TXT01_EDNBLNO.GetValue(),                                                            
                                                            DTP01_EDNCHDATE.GetString(),
                                                            DTP01_EDNDHDATE.GetString(),
                                                            Get_Numeric(TXT01_EDNCNT.GetValue().ToString()),
                                                            Get_Numeric(TXT01_EDNIPQTY.GetValue().ToString()),
                                                            TXT01_EDNBOLOC.GetValue(),
                                                            TXT01_EDNHWAJUNM.GetValue(),
                                                            MTB01_EDNHJBIRTH.GetValue().ToString().Replace("-", ""),
                                                            TXT01_EDNTEL.GetValue(),
                                                            TXT01_EDNPOST.GetValue(),
                                                            TXT01_EDNADDR.GetValue(),
                                                            "",
                                                            "",
                                                            "",
                                                            TYUserInfo.EmpNo,
                                                            DTP01_EDNDATE.GetString().ToString(),
                                                            TXT01_EDNSEQ.GetValue().ToString(),
                                                            CBO01_EDNJSGB.GetValue()

                    );
            }
            this.DbConnector.ExecuteTranQuery();

            UP_DataBinding();

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (string.IsNullOrEmpty(this.fsEDNDATE))
            { 
                //순번 
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_A5SAD589", DTP01_EDNDATE.GetString().ToString());
                TXT01_EDNSEQ.SetValue(Set_Fill3(this.DbConnector.ExecuteScalar().ToString()));
            }            

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

        #region  Description : 재고선택 버튼 이벤트
        private void BTN61_INQOPTION_Click(object sender, EventArgs e)
        {
            TYEDKB15C1 popup = new TYEDKB15C1();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {                
                TXT01_EDNJUKHA.SetValue(popup.fsEDNJUKHA);
                TXT01_EDNBLMSN.SetValue(popup.fsEDNBLMSN);
                TXT01_EDNBLHSN.SetValue(popup.fsEDNBLHSN);
                DTP01_EDNIPHANG.SetValue(popup.fsEDNIPHANG);
                CBH01_EDNBONSUN.SetValue(popup.fsEDNBONSUN);
                CBH01_EDNHWAJU.SetValue(popup.fsEDNHWAJU);
                TXT01_EDNHWAJUNM.SetValue(popup.fsEDNHWAJUNAME);
                CBH01_EDNHWAMUL.SetValue(popup.fsEDNHWAMUL);
                TXT01_EDNBLNO.SetValue(popup.fsEDNBLNO);
                TXT01_EDNIPQTY.SetValue(popup.fsEDNIPQTY);
                TXT01_EDNADDR.SetValue(popup.fsEDNADDR);     
 
                //체화예정일자 기본셋팅
                DateTime ChehwaDate = Convert.ToDateTime(popup.fsEDNIPHANG.Substring(0, 4) + "-" + popup.fsEDNIPHANG.Substring(4, 2) + "-" + popup.fsEDNIPHANG.Substring(6, 2));

                ChehwaDate = ChehwaDate.AddMonths(6).AddDays(1);

                string sChehwaDate = Convert.ToString(ChehwaDate.Year) + Set_Fill2(ChehwaDate.Month.ToString()) + Set_Fill2(ChehwaDate.Day.ToString());

                DTP01_EDNDHDATE.SetValue(sChehwaDate);
            }
        }
        #endregion

        #region  Description : Lock Check
        private void UP_SetLockCheck()
        {
            if (TYUserInfo.DeptCode.Substring(0, 1) == "S")
            {
                CBO01_EDNGJ.SetValue("S");
            }
            else
            {
                CBO01_EDNGJ.SetValue("T");
            }

            if (TYUserInfo.DeptCode.Substring(0, 6) != "A10300")
            {
                CBO01_EDNGJ.SetReadOnly(true);
            }
        }
        #endregion

        #region  Description : 우편번호 검색
        private void BTN62_INQOPTION_Click(object sender, EventArgs e)
        {
            TYEDKB15C2 popup = new TYEDKB15C2();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (popup.fsPOSTNUM != "" && popup.fsPOSTNUM != null )
                {
                    TXT01_EDNPOST.SetValue(popup.fsPOSTNUM);
                }
            }
        }
        #endregion

    }
}
