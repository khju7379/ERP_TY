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
    /// 선박사양관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.11.08 15:28
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_6BOFD848 : 선박사양관리 확인
    ///  TY_P_UT_6BOFF849 : 선박사양관리 등록
    ///  TY_P_UT_6BOFG850 : 선박사양관리 수정
    ///  TY_P_UT_6BOFI851 : 선박사양관리 삭제
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_43C9G671 : 삭제 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  NEW : 신규
    ///  REM : 삭제
    ///  SAV : 저장
    ///  VESLAJET : 대리점
    ///  VESLCODE : 선박코드
    ///  VESLFLAG : 선박국적
    ///  VEBONSON : 선박번호
    ///  VEHOSENO : 선박호스
    ///  VEMANIFD : 본선구경
    ///  VEPMCAPA : 펌프용량
    ///  VEPMTYPE : 펌프형태
    ///  VERATE : 시간당용량
    ///  VESLCALL : 호출번호
    ///  VESLGLOS : G/T
    ///  VESLLOGN : 선박길이
    /// </summary>
    public partial class TYUTVS002I : TYBase
    {
        private string fsVESLCODE = string.Empty;


        #region Description : 페이지 로드
        public TYUTVS002I(string sVESLCODE)
        {
            InitializeComponent();

            fsVESLCODE = sVESLCODE;
        }

        private void TYUTVS002I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            if (string.IsNullOrEmpty(fsVESLCODE))
            {
                this.CBH01_VESLCODE.SetReadOnly(false);

                SetStartingFocus(this.CBH01_VESLCODE.CodeText);
            }
            else
            {

                this.CBH01_VESLCODE.SetReadOnly(true);

                this.CBH01_VESLCODE.SetValue(this.fsVESLCODE.ToString().Trim());

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_6BOFD848", fsVESLCODE);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.CBH01_VESLAJET.SetValue(dt.Rows[0]["VESLAJET"].ToString());
                    this.TXT01_VESLGLOS.Text = dt.Rows[0]["VESLGLOS"].ToString();
                    this.TXT01_VESLLOGN.Text = dt.Rows[0]["VESLLOGN"].ToString();
                    this.CBH01_VESLFLAG.SetValue(dt.Rows[0]["VESLFLAG"].ToString());
                    this.TXT01_VESLCALL.Text = dt.Rows[0]["VESLCALL"].ToString();
                    this.TXT01_VEBONSON.Text = dt.Rows[0]["VEBONSON"].ToString();
                    this.TXT01_VEMANIFD.Text = dt.Rows[0]["VEMANIFD"].ToString();
                    this.TXT01_VEHOSENO.Text = dt.Rows[0]["VEHOSENO"].ToString();
                    this.TXT01_VEPMTYPE.Text = dt.Rows[0]["VEPMTYPE"].ToString();
                    this.TXT01_VEPMCAPA.Text = dt.Rows[0]["VEPMCAPA"].ToString();
                    this.TXT01_VERATE.Text = dt.Rows[0]["VERATE"].ToString();

                    this.CKB01_VEWHARF1.SetValue(dt.Rows[0]["VEWHARF1"].ToString());
                    this.CKB01_VEWHARF2.SetValue(dt.Rows[0]["VEWHARF2"].ToString());
                    this.CKB01_VEWHARF3.SetValue(dt.Rows[0]["VEWHARF3"].ToString());
                    this.CKB01_VEWHARF4.SetValue(dt.Rows[0]["VEWHARF4"].ToString());
                }

                SetStartingFocus(this.CBH01_VESLAJET.CodeText);
            }
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            // 등록
            if (string.IsNullOrEmpty(fsVESLCODE))
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_B179A274", this.CBH01_VESLCODE.GetValue().ToString(),
                                                            this.CBH01_VESLAJET.GetValue().ToString(),
                                                            this.TXT01_VESLGLOS.GetValue().ToString(),
                                                            this.TXT01_VESLLOGN.GetValue().ToString(),
                                                            this.CBH01_VESLFLAG.GetValue().ToString(),
                                                            this.TXT01_VESLCALL.GetValue().ToString(),
                                                            this.TXT01_VEMANIFD.GetValue().ToString(),
                                                            this.TXT01_VEHOSENO.GetValue().ToString(),
                                                            this.TXT01_VEPMTYPE.GetValue().ToString(),
                                                            this.TXT01_VEPMCAPA.GetValue().ToString(),
                                                            this.TXT01_VERATE.GetValue().ToString(),
                                                            this.TXT01_VEBONSON.GetValue().ToString(),
                                                            this.CKB01_VEWHARF1.GetValue().ToString(),
                                                            this.CKB01_VEWHARF2.GetValue().ToString(),
                                                            this.CKB01_VEWHARF3.GetValue().ToString(),
                                                            this.CKB01_VEWHARF4.GetValue().ToString(),
                                                            "A",
                                                            TYUserInfo.EmpNo                                               
                                                            ); 

                this.DbConnector.ExecuteNonQuery();
            }
            // 수정
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_B179B275", this.CBH01_VESLAJET.GetValue().ToString(),
                                                            this.TXT01_VESLGLOS.GetValue().ToString(),
                                                            this.TXT01_VESLLOGN.GetValue().ToString(),
                                                            this.CBH01_VESLFLAG.GetValue().ToString(),
                                                            this.TXT01_VESLCALL.GetValue().ToString(),
                                                            this.TXT01_VEMANIFD.GetValue().ToString(),
                                                            this.TXT01_VEHOSENO.GetValue().ToString(),
                                                            this.TXT01_VEPMTYPE.GetValue().ToString(),
                                                            this.TXT01_VEPMCAPA.GetValue().ToString(),
                                                            this.TXT01_VERATE.GetValue().ToString(),
                                                            this.TXT01_VEBONSON.GetValue().ToString(),
                                                            this.CKB01_VEWHARF1.GetValue().ToString(),
                                                            this.CKB01_VEWHARF2.GetValue().ToString(),
                                                            this.CKB01_VEWHARF3.GetValue().ToString(),
                                                            this.CKB01_VEWHARF4.GetValue().ToString(),
                                                            "C",
                                                            TYUserInfo.EmpNo,
                                                            this.CBH01_VESLCODE.GetValue().ToString()
                                                            );

                this.DbConnector.ExecuteNonQuery();
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.CBH01_VESLCODE.GetValue().ToString() == "TK" || this.CBH01_VESLCODE.GetValue().ToString() == "PP")
            {
            }
            else
            {
                if (string.IsNullOrEmpty(fsVESLCODE))
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_6BOFD848", CBH01_VESLCODE.GetValue().ToString());
                    DataTable dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowCustomMessage("이미 등록된 자료입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(CBH01_VESLCODE.CodeText);
                        e.Successed = false;
                        return;
                    }
                }


                // G/T
                if (this.TXT01_VESLGLOS.GetValue().ToString() == "")
                {
                    this.ShowCustomMessage("G/T를 입력하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(TXT01_VESLGLOS);
                    e.Successed = false;
                    return;
                }
                // 선박길이
                if (this.TXT01_VESLLOGN.GetValue().ToString() == "")
                {
                    this.ShowCustomMessage("선박길이를 입력하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(TXT01_VESLLOGN);
                    e.Successed = false;
                    return;
                }
                // 호출번호
                if (this.TXT01_VESLCALL.GetValue().ToString() == "")
                {
                    this.ShowCustomMessage("호출번호를 입력하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(TXT01_VESLCALL);
                    e.Successed = false;
                    return;
                }
                // 본선구경
                if (this.TXT01_VEMANIFD.GetValue().ToString() == "")
                {
                    this.ShowCustomMessage("본선구경을 입력하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(TXT01_VEMANIFD);
                    e.Successed = false;
                    return;
                }
                // 선박호스
                if (this.TXT01_VEHOSENO.GetValue().ToString() == "")
                {
                    this.ShowCustomMessage("선박호스를 입력하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(TXT01_VEHOSENO);
                    e.Successed = false;
                    return;
                }
                // 펌프형태
                if (this.TXT01_VEPMTYPE.GetValue().ToString() == "")
                {
                    this.ShowCustomMessage("펌프형태를 입력하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(TXT01_VEPMTYPE);
                    e.Successed = false;
                    return;
                }
                // 펌프용량
                if (this.TXT01_VEPMCAPA.GetValue().ToString() == "")
                {
                    this.ShowCustomMessage("펌프용량을 입력하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(TXT01_VEPMCAPA);
                    e.Successed = false;
                    return;
                }
                // 시간당용량
                if (this.TXT01_VERATE.GetValue().ToString() == "")
                {
                    this.ShowCustomMessage("시간당용량을 입력하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(TXT01_VERATE);
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
    }
}
