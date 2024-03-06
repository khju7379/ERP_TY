using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 일별근무자관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.12.08 14:08
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4C8F0663 : 일별근무자관리 수정
    ///  TY_P_HR_4C8F1664 : 일별근무자관리 삭제
    ///  TY_P_HR_4C8F6662 : 일별근무자관리 등록
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  REM : 삭제
    ///  SAV : 저장
    ///  KBSABUN : 사번
    ///  ROTGROUP : 교대조
    ///  AERDATE : 기준일자
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    ///  SYYOILCD : 요일
    /// </summary>
    public partial class TYHRGT002I : TYBase
    {
        string fsGDDATE = string.Empty;
        string fsGDGROUP = string.Empty;
        string fsGDSABUN = string.Empty;
        string fsGDROTGN = string.Empty;

        #region Description : 페이지 로드
        public TYHRGT002I(string GDDATE, string GDGROUP, string GDSABUN, string GDROTGN)
        {
            fsGDDATE = GDDATE;
            fsGDGROUP = GDGROUP;
            fsGDSABUN = GDSABUN;
            fsGDROTGN = GDROTGN;
            
            InitializeComponent();
        }

        private void TYHRGT002I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.CBH01_KBSABUN.DummyValue = "A";
            if (string.IsNullOrEmpty(fsGDGROUP))
            {
                DTP01_AERDATE.SetValue(fsGDDATE);
                SetStartingFocus(CBO01_GJGUBUN);
                BTN61_REM.Visible = false;
            }
            else
            {   
                UP_Select();

                DTP01_AERDATE.SetReadOnly(true);
                //CBO01_GJGUBUN.SetReadOnly(true);
                CBH01_KBSABUN.SetReadOnly(true);
                //CBH01_ROTGROUP.SetReadOnly(true);
                SetStartingFocus(MTB02_STDATE);
                BTN61_REM.Visible = true;
            }
            TXT01_SYYOILCD.SetReadOnly(true);
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;
            this.DbConnector.CommandClear();
            
            this.DbConnector.Attach("TY_P_HR_4C8F1664", DTP01_AERDATE.GetValue().ToString(),
                                                        CBO01_GJGUBUN.GetValue().ToString(),
                                                        CBH01_KBSABUN.GetValue().ToString(),
                                                        CBH01_ROTGROUP.GetValue().ToString());
            
            this.DbConnector.ExecuteNonQueryList();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지
            this.Close();
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            if (string.IsNullOrEmpty(fsGDGROUP))   //등록
            {
                this.DbConnector.Attach("TY_P_HR_4C8F6662", DTP01_AERDATE.GetValue().ToString(),
                                                            CBO01_GJGUBUN.GetValue().ToString(),
                                                            CBH01_KBSABUN.GetValue().ToString(),
                                                            CBH01_ROTGROUP.GetValue().ToString(),
                                                            "",
                                                            MTB02_STDATE.GetValue().ToString().Replace(":",""),
                                                            MTB02_EDDATE.GetValue().ToString().Replace(":", ""),
                                                            TXT01_NOTE.GetValue().ToString(),
                                                            TYUserInfo.EmpNo
                                                            );
            }
            else                                    // 수정
            {               
                //삭제후 다시 등록
                this.DbConnector.Attach("TY_P_HR_4C8F1664", fsGDDATE,
                                                            fsGDGROUP,
                                                            fsGDSABUN,
                                                            fsGDROTGN);

                this.DbConnector.Attach("TY_P_HR_4C8F6662",   DTP01_AERDATE.GetValue().ToString(),
                                                              CBO01_GJGUBUN.GetValue().ToString(),
                                                              CBH01_KBSABUN.GetValue().ToString(),
                                                              CBH01_ROTGROUP.GetValue().ToString(),
                                                              "",
                                                              MTB02_STDATE.GetValue().ToString().Replace(":", ""),
                                                              MTB02_EDDATE.GetValue().ToString().Replace(":", ""),
                                                              TXT01_NOTE.GetValue().ToString(),
                                                              TYUserInfo.EmpNo
                                                              );

                //this.DbConnector.Attach("TY_P_HR_4C8F0663", "",
                //                                            MTB02_STDATE.GetValue().ToString().Replace(":", ""),
                //                                            MTB02_EDDATE.GetValue().ToString().Replace(":", ""),
                //                                            TXT01_NOTE.GetValue().ToString(),
                //                                            TYUserInfo.EmpNo,
                //                                            DTP01_AERDATE.GetValue().ToString(),
                //                                            CBO01_GJGUBUN.GetValue().ToString(),
                //                                            CBH01_KBSABUN.GetValue().ToString(),
                //                                            CBH01_ROTGROUP.GetValue().ToString()
                //                                            );
            }
            this.DbConnector.ExecuteTranQueryList();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ShowMessage("TY_M_GB_23NAD873");
            this.Close();
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            //if (string.IsNullOrEmpty(this.fsGDGROUP))
            //{
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_HR_4C5EL649",
                    DTP01_AERDATE.GetString(),
                    CBO01_GJGUBUN.GetValue().ToString(),
                    CBH01_KBSABUN.GetValue().ToString(),
                    CBH01_ROTGROUP.GetValue().ToString()
                    );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_3219C986");
                    e.Successed = false;
                    return;
                }
            //}

            //교대조 사번 확인
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_HR_9178Z414",                
                CBO01_GJGUBUN.GetValue().ToString(),
                CBH01_KBSABUN.GetValue().ToString()                
                );
            int iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());
            if( iCnt <= 0 )
            {
                this.ShowCustomMessage("교대조 사번을 확인하세요.!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 데이터 조회
        private void UP_Select()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4C5EL649", fsGDDATE,
                                                        fsGDGROUP,
                                                        fsGDSABUN,
                                                        fsGDROTGN
                                                        );
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                DTP01_AERDATE.SetValue(fsGDDATE);
                CBO01_GJGUBUN.SetValue(fsGDGROUP);
                CBH01_ROTGROUP.SetValue(fsGDROTGN);
                CBH01_KBSABUN.SetValue(fsGDSABUN);
                MTB02_STDATE.SetValue(dt.Rows[0]["GDINTIME"].ToString());
                MTB02_EDDATE.SetValue(dt.Rows[0]["GDOUTTIME"].ToString());
                TXT01_NOTE.SetValue(dt.Rows[0]["GDBIGO"].ToString());
            }
        }
        #endregion

        #region Description : 일자선택 이벤트
        private void DTP01_AERDATE_ValueChanged(object sender, EventArgs e)
        {
            string sDate = DTP01_AERDATE.GetString();

            DateTime dateValue = new DateTime(Convert.ToInt16(sDate.Substring(0, 4)), Convert.ToInt16(sDate.Substring(4, 2)), Convert.ToInt16(sDate.Substring(6, 2)));

            if (dateValue.ToString("ddd") == "일")
            {
                this.TXT01_SYYOILCD.SetValue("일요일");
            }
            else if (dateValue.ToString("ddd") == "월")
            {
                this.TXT01_SYYOILCD.SetValue("월요일");
            }
            else if (dateValue.ToString("ddd") == "화")
            {
                this.TXT01_SYYOILCD.SetValue("화요일");
            }
            else if (dateValue.ToString("ddd") == "수")
            {
                this.TXT01_SYYOILCD.SetValue("수요일");
            }
            else if (dateValue.ToString("ddd") == "목")
            {
                this.TXT01_SYYOILCD.SetValue("목요일");
            }
            else if (dateValue.ToString("ddd") == "금")
            {
                this.TXT01_SYYOILCD.SetValue("금요일");
            }
            else if (dateValue.ToString("ddd") == "토")
            {
                this.TXT01_SYYOILCD.SetValue("토요일");
            }
        }
        #endregion

        #region Description : 교대구분 선택 이벤트
        private void CBH01_ROTGROUP_TextChanged(object sender, EventArgs e)
        {
            string sROTGROUP = CBH01_ROTGROUP.GetValue().ToString();

            if (sROTGROUP == "1")
            {
                MTB02_STDATE.SetValue("0700");
                MTB02_EDDATE.SetValue("1500");
            }
            else if (sROTGROUP == "2")
            {
                MTB02_STDATE.SetValue("1500");
                MTB02_EDDATE.SetValue("2300");
            }
            else if (sROTGROUP == "3")
            {
                MTB02_STDATE.SetValue("2300");
                MTB02_EDDATE.SetValue("0700");
            }
            else if (sROTGROUP == "4")
            {
                MTB02_STDATE.SetValue("");
                MTB02_EDDATE.SetValue("");
            }
            else if (sROTGROUP == "5")
            {
                MTB02_STDATE.SetValue("");
                MTB02_EDDATE.SetValue("");
            }
        }
        #endregion

        #region Description : 교대조 선택 이벤트
        private void CBO01_GJGUBUN_SelectedValueChanged(object sender, EventArgs e)
        {
            this.CBH01_KBSABUN.DummyValue = CBO01_GJGUBUN.GetValue().ToString();
        }
        #endregion
    }
}
