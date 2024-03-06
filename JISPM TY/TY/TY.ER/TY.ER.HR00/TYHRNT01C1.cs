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
    /// 연말정산 부양가족 관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.07.11 11:20
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_77BBG099 : 연말정산 부양가족 등록
    ///  TY_P_HR_77BBG100 : 연말정산 부양가족 확인
    ///  TY_P_HR_77BBI102 : 연말정산 부양가족 수정
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
    ///  WFCODE : 가족코드
    ///  WFSABUN : 사　　번
    ///  WFGUBUN : 가족관계
    ///  WFBUBU : 맞벌이
    ///  WFBUNYO : 부녀자
    ///  WFCHULSAN : 출산,입양자
    ///  WFJANG : 장애인
    ///  WFJANYE : 자녀양육
    ///  WFKIBON : 기본공제자
    ///  WFJUMIN : 주민번호
    ///  WFNAME : 가족이름
    ///  WFNATION : 국적
    ///  WFSPARENT :  한부모
    ///  WFYEAR : 년    도
    /// </summary>
    public partial class TYHRNT01C1 : TYBase
    {
        private string fsWFCOMPANY;
        private string fsWFYEAR;
        private string fsWFSABUN;
        private string fsWFJUMIN;
        private string fsFixGubn;

        #region  Description : 폼 로드 이벤트
        public TYHRNT01C1(string sWFCOMPANY, string sWFYEAR, string sWFSABUN, string sWFJUMIN, string sFixGubn)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsWFCOMPANY = sWFCOMPANY;
            fsWFYEAR = sWFYEAR;
            fsWFSABUN = sWFSABUN;
            fsWFJUMIN = sWFJUMIN;
            fsFixGubn = sFixGubn;

        }

        private void TYHRNT01C1_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            TXT01_WFYEAR.SetValue(fsWFYEAR);
            CBH01_WFSABUN.SetValue(fsWFSABUN);

            if (fsFixGubn == "Y")
            {
                BTN61_SAV.Visible = false;                
            }

            if (string.IsNullOrEmpty(this.fsWFJUMIN))
            {
                UP_Set_CheckBoxEnable(CBO01_WFGUBUN.GetValue().ToString());

                this.SetStartingFocus(MTB01_WFJUMIN);
            }
            else
            {
                MTB01_WFJUMIN.SetValue(fsWFJUMIN);
                MTB01_WFJUMIN.SetReadOnly(true);
                TXT01_WFNAME.SetReadOnly(true);
                CBH01_WFCODE.SetReadOnly(true);


                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_77BBG100", fsWFCOMPANY, TXT01_WFYEAR.GetValue(), CBH01_WFSABUN.GetValue(), TYUserInfo.SecureKey, "Y", MTB01_WFJUMIN.GetValue().ToString().Replace("-","") );
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.CurrentDataTableRowMapping(dt, "01");

                    if (this.CBO01_WFGUBUN.GetValue().ToString() != "0")
                    {
                        this.CKB01_WFBUNYO.Enabled = false;
                        this.CKB01_WFSPARENT.Enabled = false;
                    }
                }

                this.SetFocus(CBO01_WFGUBUN);
            }

        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            if (string.IsNullOrEmpty(this.fsWFJUMIN))
            {
                this.DbConnector.Attach("TY_P_HR_77BBG099", fsWFCOMPANY, 
                                                            TXT01_WFYEAR.GetValue(), 
                                                            CBH01_WFSABUN.GetValue(), 
                                                            MTB01_WFJUMIN.GetValue().ToString().Replace("-",""),
                                                            TYUserInfo.SecureKey,
                                                            CBH01_WFCODE.GetValue(),
                                                            TXT01_WFNAME.GetValue(),
                                                            CBO01_WFGUBUN.GetValue(),
                                                            CBO01_WFEDUGN.GetValue(),
                                                            CKB01_WFKIBON.GetValue(),
                                                            CKB01_WFJANG.GetValue(),
                                                            CKB01_WFBUNYO.GetValue(),                                                            
                                                            CKB01_WFJANYE.GetValue(),
                                                            CKB01_WFCHULSAN.GetValue(),
                                                            CBO01_WFNATION.GetValue(),
                                                            CKB01_WFSPARENT.GetValue(),
                                                            TYUserInfo.EmpNo
                                                            );
            }
            else
            {
                this.DbConnector.Attach("TY_P_HR_77BBI102", CBH01_WFCODE.GetValue(),
                                                            TXT01_WFNAME.GetValue(),
                                                            CBO01_WFGUBUN.GetValue(),
                                                            CBO01_WFEDUGN.GetValue(),
                                                            CKB01_WFKIBON.GetValue(),
                                                            CKB01_WFJANG.GetValue(),
                                                            CKB01_WFBUNYO.GetValue(),
                                                            CKB01_WFJANYE.GetValue(),
                                                            CKB01_WFCHULSAN.GetValue(),
                                                            CBO01_WFNATION.GetValue(),
                                                            CKB01_WFSPARENT.GetValue(),
                                                            TYUserInfo.EmpNo,
                                                            fsWFCOMPANY, 
                                                            TXT01_WFYEAR.GetValue(), 
                                                            CBH01_WFSABUN.GetValue(),
                                                            TYUserInfo.SecureKey,
                                                            "Y",
                                                            MTB01_WFJUMIN.GetValue().ToString().Replace("-", "")
                    );
            }
            this.DbConnector.ExecuteTranQuery();

            this.ShowMessage("TY_M_GB_23NAD873");

            this.BTN61_CLO_Click(null, null);
        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            Int16 iAge = 0;
            
            //주민번호 체크

            if (MTB01_WFJUMIN.GetValue().ToString().Replace("-", "").Substring(6, 1) != "3" && MTB01_WFJUMIN.GetValue().ToString().Replace("-", "").Substring(6, 1) != "4")
            {

                bool bresult = validateRRN(MTB01_WFJUMIN.GetValue().ToString());

                if (bresult != true)
                {
                    this.ShowCustomMessage("주민등록번호를 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }
            
            //나이계산
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_7BGBZ014", TXT01_WFYEAR.GetValue(), MTB01_WFJUMIN.GetValue().ToString().Replace("-", ""));
            iAge = Convert.ToInt16(this.DbConnector.ExecuteScalar());

            //귀속년도이후 주민번호 체크
            if (MTB01_WFJUMIN.GetValue().ToString().Replace("-", "").Substring(6, 1) == "3" || MTB01_WFJUMIN.GetValue().ToString().Replace("-", "").Substring(6, 1) == "4")
            {
                if (Convert.ToInt16("20" + MTB01_WFJUMIN.GetValue().ToString().Replace("-", "").Substring(0, 2)) > Convert.ToInt16(TXT01_WFYEAR.GetValue().ToString()))
                {
                    this.ShowCustomMessage("귀속년도이후 주민등록번호는 등록할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }            

            //소득자본인이 있는지 체크
            if (string.IsNullOrEmpty(this.fsWFJUMIN))
            {
                if (CBO01_WFGUBUN.GetValue().ToString() == "0")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_7BGBS013", fsWFCOMPANY, TXT01_WFYEAR.GetValue(), CBH01_WFSABUN.GetValue());
                    Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                    if (iCnt > 0)
                    {
                        this.ShowCustomMessage("소득자 본인은 이미 등록되어 있습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (CBH01_WFCODE.GetValue().ToString() != "10")
            {
                if (CBO01_WFGUBUN.GetValue().ToString() == "0")
                {
                    this.ShowCustomMessage("가족관계를 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }

                if (iAge <= 20 && (CBO01_WFGUBUN.GetValue().ToString() == "1" || CBO01_WFGUBUN.GetValue().ToString() == "2" || CBO01_WFGUBUN.GetValue().ToString() == "3" ))
                {
                    this.ShowCustomMessage("가족관계를 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            //부녀자 체크
            //남자는 부녀자 체크 불가능
            if (MTB01_WFJUMIN.GetValue().ToString().Substring(6, 1) == "1" || MTB01_WFJUMIN.GetValue().ToString().Substring(6, 1) == "3")
            {
                if (CKB01_WFBUNYO.Checked == true)
                {
                    this.ShowCustomMessage("남자는 부녀자 선택이 불가능합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            if (Convert.ToInt16(TXT01_WFYEAR.GetValue().ToString()) > 2017)
            {
                //2018년부터 자녀공제 체크
                if (CKB01_WFJANYE.Checked == true)
                {
                    //자녀공제이면 기본공제 and 가족관계(4,8) 이여야 함
                    if (CKB01_WFKIBON.Checked != true)
                    {
                        CKB01_WFJANYE.Checked = false;
                        this.ShowCustomMessage("자녀공제이면 기본공제가 체크 되어야 합니다! ", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }

                    if (CBO01_WFGUBUN.GetValue().ToString() != "4" && CBO01_WFGUBUN.GetValue().ToString() != "8")
                    {
                        CKB01_WFJANYE.Checked = false;
                        this.ShowCustomMessage("자녀공제는 가족관계코드(4,8)만 가능합니다! ", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }

                    if (iAge < 7 || iAge > 20)
                    {
                        CKB01_WFJANYE.Checked = false;
                        this.ShowCustomMessage("자녀공제는 7세이상 20세이하 자녀만 가능합니다! ", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }

                if (CKB01_WFKIBON.Checked == true && (CBO01_WFGUBUN.GetValue().ToString() == "4" || CBO01_WFGUBUN.GetValue().ToString() == "8") )
                {
                    if (iAge >= 7 && iAge <= 20)
                    {
                        if (CKB01_WFJANYE.Checked != true)
                        {
                            this.ShowCustomMessage("자녀공제를 체크하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            e.Successed = false;
                            return;
                        }
                    }
                }
            }
            else
            {
                //6세이하 체크
                if (CKB01_WFJANYE.Checked == true)
                {
                    if (iAge > 6)
                    {
                        CKB01_WFJANYE.Checked = false;
                        this.ShowCustomMessage("6세이하 자녀가 아닙니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
                else
                {
                    if (CKB01_WFKIBON.Checked == true || CBO01_WFGUBUN.GetValue().ToString() == "4")
                    {
                        if (iAge <= 6)
                        {
                            CKB01_WFJANYE.Checked = true;
                        }
                    }
                }
            }

            //한부모 조건 체크
            if (CKB01_WFSPARENT.Checked == true)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_82198565", fsWFCOMPANY, TXT01_WFYEAR.GetValue(), CBH01_WFSABUN.GetValue());
                if (Convert.ToInt16(this.DbConnector.ExecuteScalar()) > 0)
                {
                    this.ShowCustomMessage("배우자가 존재합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
                else
                {
                    //자녀중에 기본공제대상 자녀가 있어야 한다.
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_8218Y564", fsWFCOMPANY, TXT01_WFYEAR.GetValue(), CBH01_WFSABUN.GetValue());
                    Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                    if (iCnt <= 0)
                    {
                        this.ShowCustomMessage("한부모는 기본공제대상 자녀가 1명이상 있어야 합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }

            //60세미만 소득자직계, 배우자직계는 기본공제대상 제외
            if ((iAge < 60) && (CBO01_WFGUBUN.GetValue().ToString() == "1" || CBO01_WFGUBUN.GetValue().ToString() == "2"))
            {
                CKB01_WFKIBON.Checked = false;
            }

            //장애자 체크시 기본공제 자동 처리
            if (CKB01_WFJANG.Checked == true)
            {
                CKB01_WFKIBON.Checked = true;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : CBO01_WFGUBUN_SelectedIndexChanged 이벤트
        private void CBO01_WFGUBUN_SelectedIndexChanged(object sender, EventArgs e)
        {
            UP_Set_CheckBoxEnable(CBO01_WFGUBUN.GetValue().ToString());

        }
        #endregion
        
        #region  Description : 체크박스 사용 잠금 해제
        private void UP_Set_CheckBoxEnable(string svalue)
        {
            this.CKB01_WFKIBON.Checked  = false;
            this.CKB01_WFBUNYO.Checked = false;
            this.CKB01_WFSPARENT.Checked = false;
            this.CKB01_WFJANG.Checked = false;
            this.CKB01_WFCHULSAN.Checked = false;
            this.CKB01_WFJANYE.Checked = false;
            
            //본인
            if (svalue == "0")
            {
                this.CKB01_WFKIBON.Enabled = true;
                this.CKB01_WFBUNYO.Enabled = true;
                this.CKB01_WFSPARENT.Enabled = true;
                this.CKB01_WFJANG.Enabled = true;
                this.CKB01_WFCHULSAN.Enabled = false;
                this.CKB01_WFJANYE.Enabled = false;                
            }
            //소득자직계존속, 배우자직계존속, 배우자
            if (svalue == "1" || svalue == "2" || svalue == "3")
            {
                this.CKB01_WFKIBON.Enabled = true;
                this.CKB01_WFBUNYO.Enabled = false;
                this.CKB01_WFSPARENT.Enabled = false;
                this.CKB01_WFJANG.Enabled = true;
                this.CKB01_WFCHULSAN.Enabled = false;
                this.CKB01_WFJANYE.Enabled = false;
            }
            //직계비속
            if (svalue == "4" )
            {
                this.CKB01_WFKIBON.Enabled = true;
                this.CKB01_WFBUNYO.Enabled = false;
                this.CKB01_WFSPARENT.Enabled = false;
                this.CKB01_WFJANG.Enabled = true;
                this.CKB01_WFCHULSAN.Enabled = true;
                this.CKB01_WFJANYE.Enabled = true;
            }

            //형제자매, 기타
            if (svalue == "5" || svalue == "6")
            {
                this.CKB01_WFKIBON.Enabled = true;
                this.CKB01_WFBUNYO.Enabled = false;
                this.CKB01_WFSPARENT.Enabled = false;
                this.CKB01_WFJANG.Enabled = true;
                this.CKB01_WFCHULSAN.Enabled = false;
                this.CKB01_WFJANYE.Enabled = false;
            }
        }
        #endregion

        #region  Description : 주민번호 체크 함수
        private bool validateRRN(string number)
        {
           number = number.Replace("-", "");

           //길이 검사 (13자)
           if (number.Length != 13) return false;

           int sum = 0;

           //모두 숫자인지 검사
           int len = number.Length;
           for (int i = 0; i < len-1; i++)
           {
            char c = number[i];

            if (!char.IsNumber(c)) 
            {
                return false;
            }
            else
            {
             //마지막은 계산 안함
             if (i < len) sum += int.Parse(c.ToString()) * ((i % 8) + 2);
            }
           }
   
           //결과 값이 주민등록번호의 마지막과 같은지 확인
           if (((11 - (sum % 11)) % 10).ToString() == (number[number.Length - 1]).ToString())
           {
               return true;
           }

           return false;
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
