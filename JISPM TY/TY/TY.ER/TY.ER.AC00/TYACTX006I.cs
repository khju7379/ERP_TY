using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 부가세코드 옵션 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2013.11.27 16:51
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_3BS33480 : 부가세코드 옵션 수정
    ///  TY_P_AC_3BS3T481 : 부가세코드 옵션 유효성
    ///  TY_P_AC_3BS3V482 : 부가세코드 옵션 등록
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_MR_2BD3Y285 : 수정하시겠습니까?
    ///  TY_M_MR_2BD3Z286 : 수정하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  EDIT : 수정
    ///  SAV : 저장
    ///  ATCASHGB : 현금과세
    ///  ATCREDIGB : 신용카드
    ///  ATEXCLGB : 불공제
    ///  ATZEROGB : 영세율
    ///  MET2040 : 부가세코드
    /// </summary>
    public partial class TYACTX006I : TYBase
    {
        #region Description : 페이지 로드
        private TYData DAT01_MAHISAB;
        public string fsATTAXCODE = string.Empty;

        public TYACTX006I(string sATTAXCODE)
        {
            InitializeComponent();
            this.fsATTAXCODE = sATTAXCODE;

            this.DAT01_MAHISAB = new TYData("DAT01_MAHISAB", TYUserInfo.EmpNo);
        }
        
        private void TYACTX006I_Load(object sender, System.EventArgs e)
        {
            if (fsATTAXCODE != "")
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                (
                "TY_P_AC_3BR4V468",
                fsATTAXCODE
                );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                this.CBH01_OPM1150.SetValue(dt.Rows[0]["ATTAXCODE"].ToString());
                this.CBO01_ATCREDIGB.SetValue(dt.Rows[0]["ATCREDIGB"].ToString());
                this.CBO01_ATCASHGB.SetValue(dt.Rows[0]["ATCASHGB"].ToString());
                this.CBO01_ATEXCLGB.SetValue(dt.Rows[0]["ATEXCLGB"].ToString());
                this.CBO01_ATEXEMGB.SetValue(dt.Rows[0]["ATEXEMGB"].ToString());
                this.CBO01_ATZEROGB.SetValue(dt.Rows[0]["ATZEROGB"].ToString());

                // 매입매출
                if (dt.Rows[0]["ATTAXGUBN"].ToString() == "1") this.RB_ATTAXGUBN1.Checked = true;
                else if (dt.Rows[0]["ATTAXGUBN"].ToString() == "2") this.RB_ATTAXGUBN2.Checked = true;
                // 전자.일반
                if (dt.Rows[0]["ATELECTGB"].ToString() == "1") this.RB_ATELECTGB1.Checked = true;
                else if (dt.Rows[0]["ATELECTGB"].ToString() == "2") this.RB_ATELECTGB2.Checked = true;
                // 계산서
                if (dt.Rows[0]["ATBILLGB"].ToString() == "1") this.RB_ATBILLGB1.Checked = true;
                else if (dt.Rows[0]["ATBILLGB"].ToString() == "2") this.RB_ATBILLGB2.Checked = true;
                // 불공제내역
                if (dt.Rows[0]["ATEXGUBN"].ToString() == "1") this.RB_ATEXGUBN1.Checked = true;
                else if (dt.Rows[0]["ATEXGUBN"].ToString() == "2") this.RB_ATEXGUBN2.Checked = true;

                this.CBH01_OPM1150.SetReadOnly(true);
                this.SetStartingFocus(this.CBO01_ATCREDIGB);
                this.CBH01_OPM1150.Visible = true;
                this.CBH01_FXYSVATGB.Visible = false;
            }
            else
            {
                this.SetStartingFocus(this.CBH01_FXYSVATGB.CodeText);
                this.CBH01_OPM1150.Visible = false;
                this.CBH01_FXYSVATGB.Visible = true;
            }
        }
        #endregion

        #region Description : 닫기버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 저장버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            try
            {
                string sATTAXGUBN = "";
                string sATELECTGB = "";
                string sATBILLGB = "";
                string sATEXGUBN = "";


                

                    // 라디오버튼 선택 값 가져오기
                    // 매입매출
                    if (this.RB_ATTAXGUBN1.Checked == true) sATTAXGUBN = "1";
                    else if (this.RB_ATTAXGUBN2.Checked == true) sATTAXGUBN = "2";
                    // 전자.일반
                    if (this.RB_ATELECTGB1.Checked == true) sATELECTGB = "1";
                    else if (this.RB_ATELECTGB2.Checked == true) sATELECTGB = "2";
                    // 계산서
                    if (this.RB_ATBILLGB1.Checked == true) sATBILLGB = "1";
                    else if (this.RB_ATBILLGB2.Checked == true) sATBILLGB = "2";
                    // 불공제내역
                    if (this.CBO01_ATEXCLGB.GetValue().ToString() == "Y")
                    {
                        if (this.RB_ATEXGUBN1.Checked == true) sATEXGUBN = "1";
                        else if (this.RB_ATEXGUBN2.Checked == true) sATEXGUBN = "2";
                    }

                    if (this.ShowMessage("TY_M_GB_23NAD871"))
                    {
                        if (string.IsNullOrEmpty(this.fsATTAXCODE))
                        {
                            if (UP_ATTAXCODE_Check(this.CBH01_FXYSVATGB.GetValue().ToString()))
                            {
                                this.DbConnector.CommandClear();

                                this.DbConnector.Attach
                                    (
                                    "TY_P_AC_3BS3V482",
                                    this.CBH01_FXYSVATGB.GetValue(),
                                    sATTAXGUBN,
                                    this.CBO01_ATCREDIGB.GetValue(),
                                    sATELECTGB,
                                    sATBILLGB,
                                    this.CBO01_ATCASHGB.GetValue(),
                                    this.CBO01_ATEXCLGB.GetValue(),
                                    sATEXGUBN,
                                    this.CBO01_ATEXEMGB.GetValue(),
                                    this.CBO01_ATZEROGB.GetValue()
                                    );
                                this.DbConnector.ExecuteTranQueryList();
                                this.ShowMessage("TY_M_GB_23NAD873");
                                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                                this.Close();
                            }
                            else
                            {
                                this.ShowMessage("TY_M_MR_34568457");
                            }
                        }
                        else
                        {
                            this.DbConnector.CommandClear();

                            this.DbConnector.Attach
                                (
                                "TY_P_AC_3BS33480",
                                sATTAXGUBN,
                                this.CBO01_ATCREDIGB.GetValue(),
                                sATELECTGB,
                                sATBILLGB,
                                this.CBO01_ATCASHGB.GetValue(),
                                this.CBO01_ATEXCLGB.GetValue(),
                                sATEXGUBN,
                                this.CBO01_ATEXEMGB.GetValue(),
                                this.CBO01_ATZEROGB.GetValue(),
                                this.CBH01_OPM1150.GetValue()
                                );
                            this.DbConnector.ExecuteTranQueryList();

                            this.ShowMessage("TY_M_GB_23NAD873");
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                            this.Close();
                        }
                    }
                
            }
            catch
            {
                this.ShowMessage("TY_M_MR_34568457");
            }
        }
        #endregion

        #region Description : 부가세 코드 유효성 체크
        private bool UP_ATTAXCODE_Check(string sATTAXCODE)
        {
            this.DbConnector.Attach
                (
                "TY_P_AC_3BS3T481",
                sATTAXCODE
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                return true;
            }

            return false;
        }
        #endregion

        #region Description : 불공제 구분(활성화/비활성화)
        private void CBO01_ATEXCLGB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBO01_ATEXCLGB.GetValue().ToString() == "N")
            {   
                this.groupBox5.Visible = false;
            }
            else
            {
                this.groupBox5.Visible = true;
            }

        }
        #endregion

    }
}
