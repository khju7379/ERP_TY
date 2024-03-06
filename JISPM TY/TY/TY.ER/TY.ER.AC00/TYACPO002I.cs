using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 자금항목 등록 팝업 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.03.30 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_23U3Z210 : 자금항목코드 등록
    ///  TY_P_AC_23U40211 : 자금항목코드 수정
    ///  TY_P_AC_23U43214 : 자금항목코드 확인
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  A3FDHL1 : 상위항목코드１
    ///  A3FDHL2 : 상위항목코드２
    ///  A3FDHL3 : 상위항목코드３
    ///  A3IDPL  : LEVEL
    ///  A3YNSL  : 전표발생단위Y/N
    ///  A3ABFD  : 자금항목약명
    ///  A3CDFD  : 자금항목코드
    ///  A3HISAB : 작성사번
    ///  A3NMFD  : 자금항목명
    /// </summary>
    public partial class TYACPO002I : TYBase
    {
        private string fsEDCYYMM;
        private string fsEDCCDDP;
        private string fsEDCVEND;

        #region Description : 페이지 로드
        public TYACPO002I(string sEDCYYMM, string sEDCCDDP, string sEDCVEND)
        {
            InitializeComponent();

            this.SetPopupStyle();

            // 파라미터값 가져오기
            this.fsEDCYYMM = sEDCYYMM;
            this.fsEDCCDDP = sEDCCDDP;
            this.fsEDCVEND = sEDCVEND;
        }

        private void TYACPO002I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.DTP01_EDCYYMM.SetReadOnly(true);
            this.CBH01_EDCCDDP.SetReadOnly(true);

            this.DTP01_EDCYYMM.SetValue(fsEDCYYMM.ToString());

            // 사업부
            this.CBH01_EDCCDDP.DummyValue = this.DTP01_EDCYYMM.GetString().ToString().Substring(0, 6) + "01";


            // 화물
            this.CBH01_EDCHWAMUL1.DummyValue = fsEDCCDDP.ToString();
            this.CBH01_EDCHWAMUL2.DummyValue = fsEDCCDDP.ToString();
            this.CBH01_EDCHWAMUL3.DummyValue = fsEDCCDDP.ToString();

            // 현업거래처
            this.CBH01_EDCMAEVN1.DummyValue = fsEDCCDDP.ToString();
            this.CBH01_EDCMAEVN2.DummyValue = fsEDCCDDP.ToString();
            this.CBH01_EDCMAEVN3.DummyValue = fsEDCCDDP.ToString();
            this.CBH01_EDCMAEVN4.DummyValue = fsEDCCDDP.ToString();
            this.CBH01_EDCMAEVN5.DummyValue = fsEDCCDDP.ToString();
            this.CBH01_EDCMAEVN6.DummyValue = fsEDCCDDP.ToString();
            this.CBH01_EDCMAEVN7.DummyValue = fsEDCCDDP.ToString();
            this.CBH01_EDCMAEVN8.DummyValue = fsEDCCDDP.ToString();
            this.CBH01_EDCMAEVN9.DummyValue = fsEDCCDDP.ToString();
            this.CBH01_EDCMAEVNT.DummyValue = fsEDCCDDP.ToString();
            
            this.CBH01_EDCCDDP.SetValue(fsEDCCDDP.ToString());
            this.CBH01_EDCVEND.SetValue(fsEDCVEND.ToString());

            if (string.IsNullOrEmpty(this.fsEDCVEND)) // 등록
            {
                this.CBH01_EDCVEND.SetReadOnly(false);

                SetStartingFocus(this.CBH01_EDCVEND.CodeText);
            }
            else // 수정
            {
                this.CBH01_EDCVEND.SetReadOnly(true);

                SetStartingFocus(this.CBH01_EDCHWAMUL1.CodeText);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_38627341",
                    this.DTP01_EDCYYMM.GetValue().ToString(),
                    this.DTP01_EDCYYMM.GetValue().ToString(),
                    this.CBH01_EDCCDDP.GetValue().ToString(),
                    this.CBH01_EDCVEND.GetValue().ToString()
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                    this.CurrentDataTableRowMapping(dt, "01");
            }
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            if (string.IsNullOrEmpty(this.fsEDCVEND))
                // 등록
                this.DbConnector.Attach("TY_P_AC_387AD355", this.ControlFactory, "01");
            else
                // 수정
                this.DbConnector.Attach("TY_P_AC_387AE356", this.ControlFactory, "01");

            this.DbConnector.ExecuteNonQuery();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ShowMessage("TY_M_GB_23NAD873");
            this.Close();
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            if (string.IsNullOrEmpty(this.fsEDCVEND))
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_38627341",
                    this.DTP01_EDCYYMM.GetValue().ToString(),
                    this.DTP01_EDCYYMM.GetValue().ToString(),
                    this.CBH01_EDCCDDP.GetValue().ToString(),
                    this.CBH01_EDCVEND.GetValue().ToString()
                    );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_387AG357");
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3871W361",
                    this.CBH01_EDCCDDP.GetValue().ToString(),
                    this.CBH01_EDCVEND.GetValue().ToString(),
                    this.DTP01_EDCYYMM.GetValue().ToString()                    
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    //if (double.Parse(dt.Rows[0]["EDCVOLME"].ToString()) != 0 ||
                    //    double.Parse(dt.Rows[0]["EDCMAEAMT"].ToString()) != 0)
                    //{
                    //    this.ShowMessage("TY_M_AC_3871Y362");
                    //    e.Successed = false;
                    //    return;
                    //}
                }
            }

            if (this.CBH01_EDCHWAMUL1.GetValue().ToString() == "" &&
                this.CBH01_EDCHWAMUL2.GetValue().ToString() == "" &&
                this.CBH01_EDCHWAMUL3.GetValue().ToString() == ""
                )
            {
                this.ShowMessage("TY_M_AC_387AH358");
                e.Successed = false;
                return;
            }

            if (this.CBH01_EDCMAEVN1.GetValue().ToString() == "" &&
                this.CBH01_EDCMAEVN2.GetValue().ToString() == "" &&
                this.CBH01_EDCMAEVN3.GetValue().ToString() == "" &&
                this.CBH01_EDCMAEVN4.GetValue().ToString() == "" &&
                this.CBH01_EDCMAEVN5.GetValue().ToString() == "" &&
                this.CBH01_EDCMAEVN6.GetValue().ToString() == "" &&
                this.CBH01_EDCMAEVN7.GetValue().ToString() == "" &&
                this.CBH01_EDCMAEVN8.GetValue().ToString() == "" &&
                this.CBH01_EDCMAEVN9.GetValue().ToString() == "" &&
                this.CBH01_EDCMAEVNT.GetValue().ToString() == ""
                )
            {
                this.ShowMessage("TY_M_AC_387AH359");
                e.Successed = false;
                return;
            }

            if (this.CBH01_EDCMAEVNT.GetValue().ToString() != "")
            {
                if (this.CBH01_EDCMAEVN9.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_388CU372");
                    e.Successed = false;
                    return;
                }
            }
            
            if (this.CBH01_EDCMAEVN9.GetValue().ToString() != "")
            {
                if (this.CBH01_EDCMAEVN8.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_388CU372");
                    e.Successed = false;
                    return;
                }
            }
            
            if (this.CBH01_EDCMAEVN8.GetValue().ToString() != "")
            {
                if (this.CBH01_EDCMAEVN7.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_388CU372");
                    e.Successed = false;
                    return;
                }
            }
            
            if (this.CBH01_EDCMAEVN7.GetValue().ToString() != "")
            {
                if (this.CBH01_EDCMAEVN6.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_388CU372");
                    e.Successed = false;
                    return;
                }
            }
            
            if (this.CBH01_EDCMAEVN6.GetValue().ToString() != "")
            {
                if (this.CBH01_EDCMAEVN5.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_388CU372");
                    e.Successed = false;
                    return;
                }
            }
            
            if (this.CBH01_EDCMAEVN5.GetValue().ToString() != "")
            {
                if (this.CBH01_EDCMAEVN4.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_388CU372");
                    e.Successed = false;
                    return;
                }
            }
            
            if (this.CBH01_EDCMAEVN4.GetValue().ToString() != "")
            {
                if (this.CBH01_EDCMAEVN3.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_388CU372");
                    e.Successed = false;
                    return;
                }
            }
            
            if (this.CBH01_EDCMAEVN3.GetValue().ToString() != "")
            {
                if (this.CBH01_EDCMAEVN2.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_388CU372");
                    e.Successed = false;
                    return;
                }
            }
            
            if (this.CBH01_EDCMAEVN2.GetValue().ToString() != "")
            {
                if (this.CBH01_EDCMAEVN1.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_388CU372");
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

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}