using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.UT00
{
    /// <summary>
    /// 코드박스 - 장기계약 조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.11.08 10:56
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_2B8C1196 : 코드박스 - 장기계약 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_2B84W204 : 코드박스-장기계약 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  OPM1020 : 계약업체
    ///  OPM1000 : 계약년도
    ///  OPM1040 : 계약내용
    ///  PRM1020 : 년월
    /// </summary>
    public partial class TYUTIN030I : TYBase
    {
        public string fsIPHANG = string.Empty;
        public string fsBONSUN = string.Empty;
        public string fsHWAJU  = string.Empty;
        public string fsHWAMUL = string.Empty;
        public string fsMSN    = string.Empty;

        #region Description : 페이지 로드
        public TYUTIN030I()
        {
            InitializeComponent();
            this.SetPopupStyle();
        }

        public TYUTIN030I(string sIPHANG, string sBONSUN, string sHWAJU,
                          string sHWAMUL, string sMSN)
        {
            InitializeComponent();
            this.SetPopupStyle();

            fsIPHANG = sIPHANG.ToString();
            fsBONSUN = sBONSUN.ToString();
            fsHWAJU  = sHWAJU.ToString();
            fsHWAMUL = sHWAMUL.ToString();
            fsMSN    = sMSN.ToString();
        }

        private void TYUTIN030I_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_UT_67JHK874.Initialize();

            this.DTP01_BSIPHANG.SetReadOnly(true);
            this.CBH01_BSBONSUN.SetReadOnly(true);
            this.CBH01_BSHWAJU.SetReadOnly(true);
            this.CBH01_BSHWAMUL.SetReadOnly(true);

            this.DTP01_BSIPHANG.SetValue(fsIPHANG.ToString());
            this.CBH01_BSBONSUN.SetValue(fsBONSUN.ToString());
            this.CBH01_BSHWAJU.SetValue(fsHWAJU.ToString());
            this.CBH01_BSHWAMUL.SetValue(fsHWAMUL.ToString());
            this.TXT01_BSBLMSN.SetValue(fsMSN.ToString().Trim());

            this.BTN61_INQ_Click(null, null);

            UP_BTN_DISPLAY("");
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
               (
               "TY_P_UT_67JHB868",
               this.DTP01_BSIPHANG.GetValue().ToString(),
               this.CBH01_BSBONSUN.GetValue().ToString(),
               this.CBH01_BSHWAJU.GetValue().ToString(),
               this.CBH01_BSHWAMUL.GetValue().ToString(),
               this.TXT01_BSBLMSN.GetValue().ToString().Trim()
               );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_67JHK874.SetValue(dt);
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            UP_FieldClear();

            UP_BTN_DISPLAY("NEW");
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            UP_SAV();

            this.BTN61_INQ_Click(null, null);

            UP_BTN_DISPLAY("");
        }
        #endregion

        #region Description : 수정 버튼
        private void BTN61_EDIT_Click(object sender, EventArgs e)
        {
            UP_UPT();

            this.BTN61_INQ_Click(null, null);

            UP_BTN_DISPLAY("");
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            UP_DEL();

            this.BTN61_INQ_Click(null, null);

            UP_BTN_DISPLAY("");
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 확인 메소드
        private void UP_RUN()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_67JHB869",
                this.DTP01_BSIPHANG.GetValue().ToString(),
                this.CBH01_BSBONSUN.GetValue().ToString(),
                this.CBH01_BSHWAJU.GetValue().ToString(),
                this.CBH01_BSHWAMUL.GetValue().ToString(),
                this.TXT01_BSBLMSN.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");

                UP_BTN_DISPLAY("UPT");
            }
        }
        #endregion

        #region Description : 저장 메소드
        private void UP_SAV()
        {
            this.DbConnector.CommandClear();


            this.DbConnector.Attach("TY_P_UT_B53D7286",
                                    this.DTP01_BSIPHANG.GetValue().ToString(),
                                    this.CBH01_BSBONSUN.GetValue().ToString(),
                                    this.CBH01_BSHWAJU.GetValue().ToString(),
                                    this.CBH01_BSHWAMUL.GetValue().ToString(),
                                    this.TXT01_BSBLMSN.GetValue().ToString().Trim(),
                                    this.TXT01_BSBOSAE.GetValue().ToString().ToUpper(),        // 보세운송번호
                                    this.CBH01_BSBOBANG.GetValue().ToString().ToUpper(),       // 보세운송방법
                                    this.TXT01_BSBOCOMP.GetValue().ToString().ToUpper(),       // 보세운송회사
                                    Get_Date(this.DTP01_BSBODATE.GetValue().ToString()),       // 반입일자
                                    TYUserInfo.EmpNo.ToString().Trim().ToUpper()     
                                    );

            //this.DbConnector.Attach("TY_P_UT_67JHC870",
            //                        this.DTP01_BSIPHANG.GetValue().ToString(),
            //                        this.CBH01_BSBONSUN.GetValue().ToString(),
            //                        this.CBH01_BSHWAJU.GetValue().ToString(),
            //                        this.CBH01_BSHWAMUL.GetValue().ToString(),
            //                        this.TXT01_BSBLMSN.GetValue().ToString().Trim(),
            //                        this.TXT01_BSBOSAE.GetValue().ToString().ToUpper(),        // 보세운송번호
            //                        this.CBH01_BSBOBANG.GetValue().ToString().ToUpper(),       // 보세운송방법
            //                        this.TXT01_BSBOCOMP.GetValue().ToString().ToUpper(),       // 보세운송회사
            //                        TYUserInfo.EmpNo.ToString().Trim().ToUpper()     
            //                        );

            this.DbConnector.ExecuteNonQuery();

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 수정 메소드
        private void UP_UPT()
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_UT_B53D8287",
                                    this.TXT01_BSBOSAE.GetValue().ToString().ToUpper(),        // 보세운송번호
                                    this.CBH01_BSBOBANG.GetValue().ToString().ToUpper(),       // 보세운송방법
                                    this.TXT01_BSBOCOMP.GetValue().ToString().ToUpper(),       // 보세운송회사
                                    Get_Date(this.DTP01_BSBODATE.GetValue().ToString()),       // 반입일자
                                    TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                    this.DTP01_BSIPHANG.GetValue().ToString(),
                                    this.CBH01_BSBONSUN.GetValue().ToString(),
                                    this.CBH01_BSHWAJU.GetValue().ToString(),
                                    this.CBH01_BSHWAMUL.GetValue().ToString(),
                                    this.TXT01_BSBLMSN.GetValue().ToString().Trim()
                                    );

            //this.DbConnector.Attach("TY_P_UT_67JHC872",
            //                        this.TXT01_BSBOSAE.GetValue().ToString().ToUpper(),        // 보세운송번호
            //                        this.CBH01_BSBOBANG.GetValue().ToString().ToUpper(),       // 보세운송방법
            //                        this.TXT01_BSBOCOMP.GetValue().ToString().ToUpper(),       // 보세운송회사
            //                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
            //                        this.DTP01_BSIPHANG.GetValue().ToString(),
            //                        this.CBH01_BSBONSUN.GetValue().ToString(),
            //                        this.CBH01_BSHWAJU.GetValue().ToString(),
            //                        this.CBH01_BSHWAMUL.GetValue().ToString(),
            //                        this.TXT01_BSBLMSN.GetValue().ToString().Trim()
            //                        );

            this.DbConnector.ExecuteNonQuery();

            this.ShowMessage("TY_M_MR_2BD3Z286");
        }
        #endregion

        #region Description : 삭제 메소드
        private void UP_DEL()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_67JHC873",
                                    this.DTP01_BSIPHANG.GetValue().ToString(),
                                    this.CBH01_BSBONSUN.GetValue().ToString(),
                                    this.CBH01_BSHWAJU.GetValue().ToString(),
                                    this.CBH01_BSHWAMUL.GetValue().ToString(),
                                    this.TXT01_BSBLMSN.GetValue().ToString().Trim()
                                    );

            this.DbConnector.ExecuteNonQuery();

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : FieldClear
        private void UP_FieldClear()
        {
            // 보세운송번호
            this.TXT01_BSBOSAE.SetValue("");
            // 보세운송방법코드
            this.CBH01_BSBOBANG.SetValue("");
            // 보세운송회사
            this.TXT01_BSBOCOMP.SetValue("");
            // 반입일자
            this.DTP01_BSBODATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_UT_67JHK874_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.TXT01_BSBLMSN.SetValue(this.FPS91_TY_S_UT_67JHK874.GetValue("BSBLMSN").ToString());

            UP_RUN();
        }
        #endregion

        #region Description : 버튼 디스플레이
        private void UP_BTN_DISPLAY(string sGUBUN)
        {
            if (sGUBUN == "NEW")
            {
                this.BTN61_SAV.Visible = true;
                this.BTN61_EDIT.Visible = false;
                this.BTN61_REM.Visible = false;
            }
            else if (sGUBUN == "UPT")
            {
                this.BTN61_SAV.Visible = false;
                this.BTN61_EDIT.Visible = true;
                this.BTN61_REM.Visible = true;
            }
            else
            {
                this.BTN61_SAV.Visible = false;
                this.BTN61_EDIT.Visible = false;
                this.BTN61_REM.Visible = false;
            }
        }
        #endregion
    }
}