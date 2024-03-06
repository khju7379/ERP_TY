using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AF00
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
    public partial class TYAFMA003I : TYBase
    {
        private string fsESCYYMM;
        private string fsESCVEND;

        #region Description : 페이지 로드
        public TYAFMA003I(string sESCYYMM, string sESCVEND)
        {
            InitializeComponent();

            this.SetPopupStyle();

            // 파라미터값 가져오기
            this.fsESCYYMM = sESCYYMM;
            this.fsESCVEND = sESCVEND;
        }

        private void TYAFMA003I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.DTP01_ESCYYMM.SetReadOnly(true);
            
            this.DTP01_ESCYYMM.SetValue(fsESCYYMM.ToString());
            this.CBH01_ESCVEND.SetValue(fsESCVEND.ToString());

            if (string.IsNullOrEmpty(this.fsESCVEND)) // 등록
            {
                this.CBH01_ESCVEND.SetReadOnly(false);

                SetStartingFocus(this.CBH01_ESCVEND.CodeText);
            }
            else // 수정
            {
                this.CBH01_ESCVEND.SetReadOnly(true);

                SetStartingFocus(this.CBH01_ESCHWAMUL1.CodeText);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_39B3M667",
                    this.DTP01_ESCYYMM.GetValue().ToString(),
                    this.CBH01_ESCVEND.GetValue().ToString()
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
            if (string.IsNullOrEmpty(this.fsESCVEND))
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3C222500",
                    this.DTP01_ESCYYMM.GetValue().ToString(),
                    this.CBH01_ESCVEND.GetValue().ToString()
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_3C224502");
                    return;
                }
                else
                {
                    // 등록(태영)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_39B3N668", this.ControlFactory, "01");
                    this.DbConnector.ExecuteNonQuery();
                }

                // 등록(그레인)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_39B3O669", this.ControlFactory, "01");
                this.DbConnector.ExecuteNonQuery();
            }
            else
            {
                // 수정(태영)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_39B3V671", this.ControlFactory, "01");
                this.DbConnector.ExecuteNonQuery();

                // 수정(그레인)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_39B3V672", this.ControlFactory, "01");
                this.DbConnector.ExecuteNonQuery();
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ShowMessage("TY_M_GB_23NAD873");
            this.Close();
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // ------------------------   마감 완료 CHECK 시작  ------------------------------------------ //

            this.DbConnector.CommandClear(); // TY_P_AC_27H64059
            this.DbConnector.Attach("TY_P_AC_3C92V659", this.DTP01_ESCYYMM.GetValue().ToString().Substring(0, 4), this.DTP01_ESCYYMM.GetValue().ToString().Substring(4, 2));
            DataTable dt1 = this.DbConnector.ExecuteDataTable();

            if (dt1.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                e.Successed = false;
                return;
            }
            else
            {
                if (dt1.Rows[0]["ECSBBUN"].ToString() == "Z")
                {
                    this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                    e.Successed = false;
                    return;
                }
            }

            // ------------------------   마감 완료 CHECK 끝 ------------------------------------------ //

            DataTable dt = new DataTable();

            if (string.IsNullOrEmpty(this.fsESCVEND))
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_39B3M667",
                    this.DTP01_ESCYYMM.GetValue().ToString(),
                    this.CBH01_ESCVEND.GetValue().ToString()
                    );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_387AG357");
                    e.Successed = false;
                    return;
                }
            }

            if (this.CBH01_ESCHWAMUL1.GetValue().ToString() == "" &&
                this.CBH01_ESCHWAMUL2.GetValue().ToString() == "" &&
                this.CBH01_ESCHWAMUL3.GetValue().ToString() == ""
                )
            {
                this.ShowMessage("TY_M_AC_387AH358");
                e.Successed = false;
                return;
            }

            if (this.CBH01_ESCMAEVN1.GetValue().ToString() == "" &&
                this.CBH01_ESCMAEVN2.GetValue().ToString() == "" &&
                this.CBH01_ESCMAEVN3.GetValue().ToString() == "" &&
                this.CBH01_ESCMAEVN4.GetValue().ToString() == "" &&
                this.CBH01_ESCMAEVN5.GetValue().ToString() == "" &&
                this.CBH01_ESCMAEVN6.GetValue().ToString() == "" &&
                this.CBH01_ESCMAEVN7.GetValue().ToString() == "" &&
                this.CBH01_ESCMAEVN8.GetValue().ToString() == "" &&
                this.CBH01_ESCMAEVN9.GetValue().ToString() == "" &&
                this.CBH01_ESCMAEVNT.GetValue().ToString() == ""
                )
            {
                this.ShowMessage("TY_M_AC_387AH359");
                e.Successed = false;
                return;
            }

            if (this.CBH01_ESCMAEVNT.GetValue().ToString() != "")
            {
                if (this.CBH01_ESCMAEVN9.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_388CU372");
                    e.Successed = false;
                    return;
                }
            }
            
            if (this.CBH01_ESCMAEVN9.GetValue().ToString() != "")
            {
                if (this.CBH01_ESCMAEVN8.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_388CU372");
                    e.Successed = false;
                    return;
                }
            }
            
            if (this.CBH01_ESCMAEVN8.GetValue().ToString() != "")
            {
                if (this.CBH01_ESCMAEVN7.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_388CU372");
                    e.Successed = false;
                    return;
                }
            }
            
            if (this.CBH01_ESCMAEVN7.GetValue().ToString() != "")
            {
                if (this.CBH01_ESCMAEVN6.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_388CU372");
                    e.Successed = false;
                    return;
                }
            }
            
            if (this.CBH01_ESCMAEVN6.GetValue().ToString() != "")
            {
                if (this.CBH01_ESCMAEVN5.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_388CU372");
                    e.Successed = false;
                    return;
                }
            }
            
            if (this.CBH01_ESCMAEVN5.GetValue().ToString() != "")
            {
                if (this.CBH01_ESCMAEVN4.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_388CU372");
                    e.Successed = false;
                    return;
                }
            }
            
            if (this.CBH01_ESCMAEVN4.GetValue().ToString() != "")
            {
                if (this.CBH01_ESCMAEVN3.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_388CU372");
                    e.Successed = false;
                    return;
                }
            }
            
            if (this.CBH01_ESCMAEVN3.GetValue().ToString() != "")
            {
                if (this.CBH01_ESCMAEVN2.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_388CU372");
                    e.Successed = false;
                    return;
                }
            }
            
            if (this.CBH01_ESCMAEVN2.GetValue().ToString() != "")
            {
                if (this.CBH01_ESCMAEVN1.GetValue().ToString() == "")
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