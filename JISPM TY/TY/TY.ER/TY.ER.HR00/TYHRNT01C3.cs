using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 연말정산 의료비 등록 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.07.19 17:16
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_77JC6219 : 연말정산 소득자공제명세서 조회
    ///  TY_P_HR_77JHK227 : 연말정산 영수증 의료비 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_77JHH226 : 연말정산 부양가족명세 조회
    ///  TY_S_HR_77JHL228 : 연말정산 영수증 의료비 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  NEW : 신규
    ///  SAV : 저장
    ///  NSBUSNID : 사업자번호
    ///  NSDAT_CD : 자료코드
    ///  NSFORM_CD : 서식코드
    ///  NSSUM : 납입금액계
    ///  NSTRADE_NM : 상    호
    ///  WFCODE : 가족코드
    ///  WFJUMIN : 주민번호
    ///  WFNAME : 가족이름
    ///  WFSABUN : 사　　번
    ///  WFYEAR : 년    도
    /// </summary>
    public partial class TYHRNT01C3 : TYBase
    {
        private string fsWKCOMPANY;
        private string fsWKYEAR;
        private string fsWKSABUN;
        private string fsFixGubn;

        #region  Description : 폼 로드 이벤트
        public TYHRNT01C3(string sWKCOMPANY, string sWKYEAR, string sWKSABUN, string sFixGubn)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsWKCOMPANY = sWKCOMPANY;
            fsWKYEAR = sWKYEAR;
            fsWKSABUN = sWKSABUN;
            fsFixGubn = sFixGubn;
        }

        private void TYHRNT01C3_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            TXT01_WFYEAR.SetValue(fsWKYEAR);
            CBH01_WFSABUN.SetValue(fsWKSABUN);

            if (fsFixGubn == "Y")
            {
                BTN61_NEW.Visible = false;
                BTN61_SAV.Visible = false;
                BTN61_REM.Visible = false;
            }

            UP_Set_ButtonStatus(false, false, false);

            this.UP_LeftGrid_DataBinding();

        }
        #endregion

        #region  Description : 좌측 그리드 데이타 바인딩 이벤트
        private void UP_LeftGrid_DataBinding()
        {
            this.FPS91_TY_S_HR_77JHH226.Initialize();            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77JC6219", fsWKCOMPANY, TXT01_WFYEAR.GetValue(), CBH01_WFSABUN.GetValue(), TYUserInfo.SecureKey, "Y");
            this.FPS91_TY_S_HR_77JHH226.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 우측 그리드 데이타 바인딩 이벤트
        private void UP_RightGrid_DataBinding(string sNSCOMPANY, string sNSYEAR, string sNSSABUN,  string sNSresid)
        {
            this.FPS91_TY_S_HR_77JHL228.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_7BKB5052",  TYUserInfo.SecureKey, "Y", sNSCOMPANY, sNSYEAR, sNSSABUN,  TYUserInfo.SecureKey, "Y", sNSresid
                                                         );
            this.FPS91_TY_S_HR_77JHL228.SetValue(this.DbConnector.ExecuteDataTable());           

            this.SpreadSumRowAdd(this.FPS91_TY_S_HR_77JHL228, "MDTRADE_NM", "합  계", SumRowType.Sum, "MDMEDAMOUNT", "NOTMDMEDAMOUNT");
        }
        #endregion


        #region  Description :  FPS91_TY_S_HR_77JHH226_CellDoubleClick 이벤트
        private void FPS91_TY_S_HR_77JHH226_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.UP_RightGrid_DataBinding(this.FPS91_TY_S_HR_77JHH226.GetValue("WFCOMPANY").ToString(),
                                          this.FPS91_TY_S_HR_77JHH226.GetValue("WFYEAR").ToString(),
                                          this.FPS91_TY_S_HR_77JHH226.GetValue("WFSABUN").ToString(),                                        
                                          this.FPS91_TY_S_HR_77JHH226.GetValue("WFJUMIN").ToString()
                                        );
            MTB01_WFJUMIN.SetValue(this.FPS91_TY_S_HR_77JHH226.GetValue("WFJUMIN").ToString());
            TXT01_WFNAME.SetValue(this.FPS91_TY_S_HR_77JHH226.GetValue("WFNAME").ToString());
            TXT01_WFCODE.SetValue(this.FPS91_TY_S_HR_77JHH226.GetValue("WFCODE").ToString());

            CBO01_MDKINGCD.SetValue(this.FPS91_TY_S_HR_77JHH226.GetValue("MDKIND").ToString());

            CBO01_MDMDCODE.SetValue("1");
            MTB01_MDBUSNID.SetValue("");
            TXT01_MDTRADE_NM.SetValue("");
            TXT01_MDCOUNT.SetValue("0");
            CBO01_MDXPRSGN.SetValue("0");
            TXT01_MDMEDAMOUNT.SetValue("0");

            this.CBO01_MDMDCODE.SetReadOnly(false);
            this.MTB01_MDBUSNID.SetReadOnly(false);

            if (fsFixGubn != "Y")
            {
                UP_Set_ButtonStatus(true, true, false);
            }

            this.SetFocus(CBO01_MDMDCODE);
        }
        #endregion

        #region  Description :  FPS91_TY_S_HR_77JHL228_CellDoubleClick 이벤트
        private void FPS91_TY_S_HR_77JHL228_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77KAM237", this.FPS91_TY_S_HR_77JHL228.GetValue("MDCOMPANY").ToString(),
                                                        this.FPS91_TY_S_HR_77JHL228.GetValue("MDYEAR").ToString(),
                                                        this.FPS91_TY_S_HR_77JHL228.GetValue("MDSABUN").ToString(),
                                                        this.FPS91_TY_S_HR_77JHL228.GetValue("MDRESID").ToString(),
                                                        this.FPS91_TY_S_HR_77JHL228.GetValue("MDBUSNID").ToString().Replace("-","").ToString(),
                                                        this.FPS91_TY_S_HR_77JHL228.GetValue("MDMDCODE").ToString(),
                                                        TYUserInfo.SecureKey, "Y"
                                                         );
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["MDNTSGN"].ToString() == "Y")
                {
                    this.ShowCustomMessage("국세청자료는 수정할수 없습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    if (fsFixGubn != "Y")
                    {
                        UP_Set_ButtonStatus(true, false, false);
                    }
                }
                else
                {
                    if (fsFixGubn != "Y")
                    {
                        UP_Set_ButtonStatus(true, true, true);
                    }
                }

                this.CBO01_MDMDCODE.SetReadOnly(true);
                this.MTB01_MDBUSNID.SetReadOnly(true);

                this.CurrentDataTableRowMapping(dt, "01");

                this.SetFocus(TXT01_MDTRADE_NM);
            }
        }
        #endregion

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            CBO01_MDMDCODE.SetValue("1");
            MTB01_MDBUSNID.SetValue("");
            TXT01_MDTRADE_NM.SetValue("");            
            TXT01_MDCOUNT.SetValue("0");
            CBO01_MDXPRSGN.SelectedIndex = 0;
            TXT01_MDMEDAMOUNT.SetValue("0");
            CBO01_MDXPRSGN.SetValue("0");

            this.CBO01_MDMDCODE.SetReadOnly(false);
            this.MTB01_MDBUSNID.SetReadOnly(false);

            UP_Set_ButtonStatus(true, true, false);

            this.SetFocus(CBO01_MDMDCODE);
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77KAM237", fsWKCOMPANY,
                                                        TXT01_WFYEAR.GetValue(),
                                                        CBH01_WFSABUN.GetValue(),
                                                        MTB01_WFJUMIN.GetValue().ToString().Replace("-", ""),
                                                        MTB01_MDBUSNID.GetValue().ToString().Replace("-", ""),
                                                        CBO01_MDMDCODE.GetValue().ToString(),
                                                        TYUserInfo.SecureKey, "Y"
                                                         );
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                //수정
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_77KBA238", TXT01_MDTRADE_NM.GetValue().ToString(),
                                                            TXT01_MDCOUNT.GetValue(),
                                                            TXT01_MDMEDAMOUNT.GetValue(),
                                                            CBO01_MDXPRSGN.GetValue(),
                                                            CBO01_MDKINGCD.GetValue(),
                                                            TYUserInfo.EmpNo,
                                                            fsWKCOMPANY,
                                                            TXT01_WFYEAR.GetValue(),
                                                            CBH01_WFSABUN.GetValue(),
                                                            TYUserInfo.SecureKey, "Y",
                                                            MTB01_WFJUMIN.GetValue().ToString().Replace("-", ""),
                                                            MTB01_MDBUSNID.GetValue().ToString().Replace("-", ""),
                                                            CBO01_MDMDCODE.GetValue().ToString()
                                                             );

            }
            else
            {
                //등록
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_7BKAZ051", fsWKCOMPANY,
                                                            TXT01_WFYEAR.GetValue(),
                                                            CBH01_WFSABUN.GetValue(),
                                                            MTB01_WFJUMIN.GetValue().ToString().Replace("-", ""),
                                                            TYUserInfo.SecureKey, 
                                                            MTB01_MDBUSNID.GetValue().ToString().Replace("-", ""),
                                                            CBO01_MDMDCODE.GetValue().ToString(),
                                                            TXT01_MDTRADE_NM.GetValue(),
                                                            TXT01_MDCOUNT.GetValue(),
                                                            TXT01_MDMEDAMOUNT.GetValue(),
                                                            CBO01_MDXPRSGN.GetValue(),
                                                            CBO01_MDKINGCD.GetValue(),
                                                            "N",
                                                            TYUserInfo.EmpNo
                                                             );
            }
            this.DbConnector.ExecuteTranQuery();

            UP_ProCedure_FixCall();

            this.UP_LeftGrid_DataBinding();
            this.UP_RightGrid_DataBinding(fsWKCOMPANY, TXT01_WFYEAR.GetValue().ToString(), CBH01_WFSABUN.GetValue().ToString(), MTB01_WFJUMIN.GetValue().ToString().Replace("-", ""));

            UP_Set_ButtonStatus(true, true, false);

            this.ShowMessage("TY_M_GB_23NAD873");
        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            //사업자번호 체크
            if (this.CBO01_MDMDCODE.GetValue().ToString() != "6")
            {

                if (this.MTB01_MDBUSNID.GetValue().ToString().Replace("-", "").Trim().Length < 10)
                {
                    this.SetFocus(MTB01_MDBUSNID);
                    this.ShowCustomMessage("사업자번호를 확인하세요!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    e.Successed = false;
                    return;
                }


                if (Convert.ToInt16(Get_Numeric(TXT01_MDCOUNT.GetValue().ToString())) <= 0 && CBO01_MDMDCODE.GetValue().ToString() != "1")
                {
                    this.SetFocus(TXT01_MDCOUNT);
                    this.ShowCustomMessage("연간 의료비 지급건수를 입력하세요!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    e.Successed = false;
                    return;
                }
            }

            

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77KBQ239",
                                                            fsWKCOMPANY,
                                                            TXT01_WFYEAR.GetValue(),
                                                            CBH01_WFSABUN.GetValue(),
                                                             TYUserInfo.SecureKey, "Y",
                                                            MTB01_WFJUMIN.GetValue().ToString().Replace("-", ""),
                                                            MTB01_MDBUSNID.GetValue().ToString().Replace("-", ""),
                                                            CBO01_MDMDCODE.GetValue().ToString()
                                                        );
            this.DbConnector.ExecuteTranQuery();

            UP_ProCedure_FixCall();

            this.UP_LeftGrid_DataBinding();
            this.UP_RightGrid_DataBinding(fsWKCOMPANY, TXT01_WFYEAR.GetValue().ToString(), CBH01_WFSABUN.GetValue().ToString(), MTB01_WFJUMIN.GetValue().ToString().Replace("-", ""));

            this.BTN61_NEW_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD874");
        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_GB_23NAD872"))
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

        #region  Description : 버튼 Visible 타입 이벤트
        private void UP_Set_ButtonStatus(bool bNew, bool bSav, bool bRem )
        {
            this.BTN61_NEW.Visible = bNew;
            this.BTN61_SAV.Visible = bSav;
            this.BTN61_REM.Visible = bRem;
        }
        #endregion
        
        #region  Description : 연말정산 국세청 확정 프로시저 호출 함수
        private void UP_ProCedure_FixCall()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_77JDB223", fsWKCOMPANY, TXT01_WFYEAR.GetValue() , CBH01_WFSABUN.GetValue(), TYUserInfo.EmpNo , TYUserInfo.SecureKey, "Y",  "");
            this.DbConnector.ExecuteScalar();
        }
        #endregion

        #region  Description : TXT01 KeyPress 함수
        private void TXT01_MDTRADE_NM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.SetFocus(this.TXT01_MDCOUNT);                
            }
        }        

        private void TXT01_MDCOUNT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.SetFocus(this.TXT01_MDMEDAMOUNT);
            }
        }

        private void TXT01_MDMEDAMOUNT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.SetFocus(this.CBO01_MDXPRSGN);
            }
        }
        #endregion

        #region  Description : CBO01_MDMDCODE_SelectedIndexChanged 함수
        private void CBO01_MDMDCODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CBO01_MDMDCODE.GetValue().ToString() == "6")
            {
                MTB01_MDBUSNID.Enabled = false;
                TXT01_MDTRADE_NM.SetReadOnly(true);
                TXT01_MDCOUNT.SetReadOnly(true);
                CBO01_MDXPRSGN.SetReadOnly(true);
            }
            else
            {
                MTB01_MDBUSNID.Enabled = true;
                TXT01_MDTRADE_NM.SetReadOnly(false);
                TXT01_MDCOUNT.SetReadOnly(false);
                CBO01_MDXPRSGN.SetReadOnly(false);
            }
        }
        #endregion



    }
}
