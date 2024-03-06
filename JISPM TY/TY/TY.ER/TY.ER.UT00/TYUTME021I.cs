using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;
using TY.Service.Library.Controls.TYSpreadCellType;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;

namespace TY.ER.UT00
{
    /// <summary>
    /// 선급자재 관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2013.02.19 09:59
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_32J79125 : 선급자재 미생성 조회
    ///  TY_P_MR_32J7A126 : 선급자재 생성 조회
    ///  TY_P_MR_32J7A127 : 선급자재 DETAIL 조회
    ///  TY_P_MR_32J7A128 : 선급자재 DETAIL 하위 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_32J7C129 : 선급자재 생성 조회
    ///  TY_S_MR_32J7M130 : 선급자재 DETAIL 조회
    ///  TY_S_UT_74QI2391 : 선급자재 DETAIL 하위 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CANCEL : 취소
    ///  CREATE : 생성
    ///  INQ : 조회
    ///  JASAN_CRE : 자산생성
    ///  JASAN_DEL : 자산삭제
    ///  JPNO_CRE : 전표생성
    ///  JPNO_DEL : 전표삭제
    ///  FXDDPMK : 귀속부서
    ///  FXDSAUP : 선급사업부
    ///  FXDGETDATE : 취득일
    ///  GCDACGHAP : 계정총액
    ///  GDAESANGHAP : 대상총액
    ///  GJANGHAP : 잔액
    /// </summary>
    public partial class TYUTME021I : TYBase
    {
        private string fsGUBUN   = string.Empty;
        private string fsVSJUBAN = string.Empty;
        private string fsVSGROSS = string.Empty;

        private string fsMCENBOHP   = string.Empty;
        private string fsMCENIPHP   = string.Empty;
        private string fsMCENCHHP   = string.Empty;
        private string fsMCENISHP   = string.Empty;
        private string fsMCENCANHP  = string.Empty;
        private string fsMCENDRHP   = string.Empty;
        private string fsMCENBOJHP  = string.Empty;
        private string fsMCENIPOVHP = string.Empty;
        private string fsMCENCHOVHP = string.Empty;

        #region Description : 페이지 로드
        public TYUTME021I()
        {
            InitializeComponent();
        }

        private void TYUTME021I_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_UT_74QI2391.Initialize();

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.BTN61_DETAIL_DEL.ProcessCheck += new TButton.CheckHandler(BTN61_DETAIL_DEL_ProcessCheck);

            UP_BTN_DISPLAY("");

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            fsGUBUN = "";

            DataTable dt = new DataTable();

            this.FPS91_TY_S_UT_74QI2391.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_74QI3389",
                Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                this.CBH01_SHWAJU.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_74QI2391.SetValue(dt);


            UP_BTN_DISPLAY("");
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            fsGUBUN = "NEW";

            UP_Field_ReadOnly(false);

            UP_Field_Clear();

            UP_BTN_DISPLAY("NEW");

            SetFocus(this.DTP01_MCDATE);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            UP_Save();
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            UP_Del();
        }
        #endregion

        #region Description : 일괄 삭제 버튼
        private void BTN61_DETAIL_DEL_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            if (dt.Rows.Count > 0)
            {
                // 보관료 삭제
                this.DbConnector.CommandClear();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_UT_75GJQ538",
                                            Get_Date(dt.Rows[i]["MCDATE"].ToString()),
                                            dt.Rows[i]["MCHWAJU"].ToString(),
                                            dt.Rows[i]["MCSEQ"].ToString(),
                                            dt.Rows[i]["MCHWAMUL"].ToString(),
                                            dt.Rows[i]["MCTANKNO"].ToString().Trim(),
                                            dt.Rows[i]["MCCONTNO"].ToString()
                                            );
                }

                this.DbConnector.ExecuteTranQueryList();
            }

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 저장 메소드
        private void UP_Save()
        {
            if (fsGUBUN == "NEW")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_C4DDA246",
                                        Get_Date(this.DTP01_MCDATE.GetValue().ToString()),
                                        this.CBH01_MCHWAJU.GetValue().ToString().ToUpper(),
                                        this.TXT01_MCSEQ.GetValue().ToString(),
                                        this.CBH01_MCHWAMUL.GetValue().ToString().ToUpper(),
                                        Set_TankNo(this.TXT01_MCTANKNO.GetValue().ToString().Trim()),
                                        this.TXT01_MCCONTNO1.GetValue().ToString() + "-" + Set_Fill3(this.TXT01_MCCONTNO2.GetValue().ToString()),
                                        this.CBO01_MCREQGB.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_MCENRATE.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENBOAMT.GetValue().ToString()),
                                        fsMCENBOHP.ToString(),
                                        Get_Numeric(this.TXT01_MCENIPQTY.GetValue().ToString()),
                                        this.CBO01_MCENIPDAN.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_MCENIPRAT.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENIPAMT.GetValue().ToString()),
                                        fsMCENIPHP.ToString(),
                                        Get_Numeric(this.TXT01_MCENCHQTY.GetValue().ToString()),
                                        this.CBO01_MCENCHDAN.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_MCENCHRAT.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENCHAMT.GetValue().ToString()),
                                        fsMCENCHHP.ToString(),
                                        Get_Numeric(this.TXT01_MCENISQTY.GetValue().ToString()),
                                        this.CBO01_MCENISDAN.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_MCENISRAT.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENISAMT.GetValue().ToString()),
                                        fsMCENISHP.ToString(),
                                        Get_Numeric(this.TXT01_MCENCANQTY.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENCANRAT.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENCANAMT.GetValue().ToString()),
                                        fsMCENCANHP.ToString(),
                                        Get_Numeric(this.TXT01_MCENDRQTY.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENDRRAT.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENDRAMT.GetValue().ToString()),
                                        fsMCENDRHP.ToString(),
                                        Get_Numeric(this.TXT01_MCENBOJQTY.GetValue().ToString()),
                                        this.CBO01_MCENBOJDAN.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_MCENBOJRAT.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENBOJAMT.GetValue().ToString()),
                                        fsMCENBOJHP.ToString(),
                                        Get_Numeric(this.TXT01_MCENIPOVQT.GetValue().ToString()),
                                        this.CBO01_MCENIPOVDA.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_MCENIPOVRA.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENIPOVAM.GetValue().ToString()),
                                        fsMCENIPOVHP.ToString(),
                                        Get_Numeric(this.TXT01_MCENCHOVQT.GetValue().ToString()),
                                        this.CBO01_MCENCHOVDA.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_MCENCHOVRA.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENCHOVAM.GetValue().ToString()),
                                        fsMCENCHOVHP.ToString(),
                                        Get_Numeric(this.TXT01_MCENBUDUAM.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENTOJIAM.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENJILQTY.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENJILRAT.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENJILAMT.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENGAAMT.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENCLEAMT.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENVOCAMT.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENLPGAMT.GetValue().ToString()),
                                        this.CBO01_MCVATGB.GetValue().ToString().Trim(),
                                        Get_Numeric(this.TXT01_MCENBODAL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENIPDAL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENCHDAL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENISDAL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENCANDAL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENDRDAL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENBOJDAL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENIPOVDL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENCHOVDL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENBUDUDL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENTOJIDL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENJILDAL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENGADAL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENCLEDAL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENVOCDAL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENLPGDAL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENETCWAMT.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENETCDALAMT.GetValue().ToString().Trim()),
                                        this.CBH01_MCCURRCD.GetValue().ToString().Trim(),
                                        this.CBH01_MCETCCODE.GetValue().ToString(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper()                             // 작성사번
                                        );

                this.DbConnector.ExecuteNonQuery();
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_C4DDV247",
                                        this.CBO01_MCREQGB.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_MCENRATE.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENBOAMT.GetValue().ToString()),
                                        fsMCENBOHP.ToString(),
                                        Get_Numeric(this.TXT01_MCENIPQTY.GetValue().ToString()),
                                        this.CBO01_MCENIPDAN.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_MCENIPRAT.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENIPAMT.GetValue().ToString()),
                                        fsMCENIPHP.ToString(),
                                        Get_Numeric(this.TXT01_MCENCHQTY.GetValue().ToString()),
                                        this.CBO01_MCENCHDAN.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_MCENCHRAT.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENCHAMT.GetValue().ToString()),
                                        fsMCENCHHP.ToString(),
                                        Get_Numeric(this.TXT01_MCENISQTY.GetValue().ToString()),
                                        this.CBO01_MCENISDAN.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_MCENISRAT.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENISAMT.GetValue().ToString()),
                                        fsMCENISHP.ToString(),
                                        Get_Numeric(this.TXT01_MCENCANQTY.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENCANRAT.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENCANAMT.GetValue().ToString()),
                                        fsMCENCANHP.ToString(),
                                        Get_Numeric(this.TXT01_MCENDRQTY.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENDRRAT.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENDRAMT.GetValue().ToString()),
                                        fsMCENDRHP.ToString(),
                                        Get_Numeric(this.TXT01_MCENBOJQTY.GetValue().ToString()),
                                        this.CBO01_MCENBOJDAN.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_MCENBOJRAT.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENBOJAMT.GetValue().ToString()),
                                        fsMCENBOJHP.ToString(),
                                        Get_Numeric(this.TXT01_MCENIPOVQT.GetValue().ToString()),
                                        this.CBO01_MCENIPOVDA.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_MCENIPOVRA.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENIPOVAM.GetValue().ToString()),
                                        fsMCENIPOVHP.ToString(),
                                        Get_Numeric(this.TXT01_MCENCHOVQT.GetValue().ToString()),
                                        this.CBO01_MCENCHOVDA.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_MCENCHOVRA.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENCHOVAM.GetValue().ToString()),
                                        fsMCENCHOVHP.ToString(),
                                        Get_Numeric(this.TXT01_MCENBUDUAM.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENTOJIAM.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENJILQTY.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENJILRAT.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENJILAMT.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENGAAMT.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENCLEAMT.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENVOCAMT.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_MCENLPGAMT.GetValue().ToString()),
                                        this.CBO01_MCVATGB.GetValue().ToString().Trim(),
                                        Get_Numeric(this.TXT01_MCENBODAL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENIPDAL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENCHDAL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENISDAL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENCANDAL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENDRDAL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENBOJDAL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENIPOVDL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENCHOVDL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENBUDUDL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENTOJIDL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENJILDAL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENGADAL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENCLEDAL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENVOCDAL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENLPGDAL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENETCWAMT.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_MCENETCDALAMT.GetValue().ToString().Trim()),
                                        this.CBH01_MCCURRCD.GetValue().ToString().Trim(),
                                        this.CBH01_MCETCCODE.GetValue().ToString(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),                            // 작성사번
                                        Get_Date(this.DTP01_MCDATE.GetValue().ToString()),
                                        this.CBH01_MCHWAJU.GetValue().ToString().ToUpper(),
                                        this.TXT01_MCSEQ.GetValue().ToString(),
                                        this.CBH01_MCHWAMUL.GetValue().ToString().ToUpper(),
                                        this.TXT01_MCTANKNO.GetValue().ToString().Trim(),
                                        this.TXT01_MCCONTNO1.GetValue().ToString() + "-" + Set_Fill3(this.TXT01_MCCONTNO2.GetValue().ToString())
                                        );

                this.DbConnector.ExecuteNonQuery();
            }

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 삭제 메소드
        private void UP_Del()
        {
            string sMCCONTNO = string.Empty;

            sMCCONTNO = this.TXT01_MCCONTNO1.GetValue().ToString() + "-" + Set_Fill3(this.TXT01_MCCONTNO2.GetValue().ToString());

            // 보관료 삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75GJQ538",
                                    Get_Date(this.DTP01_MCDATE.GetValue().ToString()),
                                    this.CBH01_MCHWAJU.GetValue().ToString(),
                                    this.TXT01_MCSEQ.GetValue().ToString(),
                                    this.CBH01_MCHWAMUL.GetValue().ToString(),
                                    this.TXT01_MCTANKNO.GetValue().ToString().Trim(),
                                    sMCCONTNO.ToString()
                                    );

            this.DbConnector.ExecuteNonQuery();

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 확인 메소드
        private void UP_RUN()
        {
            string sMCCONTNO = string.Empty;

            UP_Field_Clear();

            sMCCONTNO = this.TXT01_MCCONTNO1.GetValue().ToString() + "-" + Set_Fill3(this.TXT01_MCCONTNO2.GetValue().ToString());

            fsGUBUN = "";

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_75BGG454",
                Get_Date(this.DTP01_MCDATE.GetValue().ToString()),
                this.CBH01_MCHWAJU.GetValue().ToString(),
                this.TXT01_MCSEQ.GetValue().ToString(),
                this.CBH01_MCHWAMUL.GetValue().ToString(),
                this.TXT01_MCTANKNO.GetValue().ToString().Trim(),
                sMCCONTNO.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");

                fsGUBUN = "UPT";

                if (this.TXT01_MCJPNO.GetValue().ToString() != "")
                {
                    fsGUBUN = "";
                }

                // 원화, 외화 계산(소계, 총계)
                UP_COMPUTE_DAL();

                UP_Field_ReadOnly(true);
            }
            else
            {
                fsGUBUN = "NEW";
            }

            UP_BTN_DISPLAY(fsGUBUN);

            SetFocus(this.CBO01_MCREQGB);
        }
        #endregion

        #region Description : 원화, 외화 계산(소계, 총계)
        private void UP_COMPUTE_DAL()
        {
            if (this.CBO01_MCREQGB.GetValue().ToString() == "1") // 원화
            {
                // 보관료 원화금액
                decimal dWON_MCENBOAMT = 0;
                decimal dWON_MCENBUDUAM = 0;
                decimal dWON_MCENTOJIAM = 0;
                decimal dWON_MCENBOAMT_HAP = 0;

                dWON_MCENBOAMT = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENBOAMT.GetValue().ToString().Trim())));
                dWON_MCENBUDUAM = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENBUDUAM.GetValue().ToString().Trim())));
                dWON_MCENTOJIAM = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENTOJIAM.GetValue().ToString().Trim())));

                // 보관료 원화금액
                dWON_MCENBOAMT_HAP = dWON_MCENBOAMT + dWON_MCENBUDUAM + dWON_MCENTOJIAM;
                dWON_MCENBOAMT_HAP = decimal.Parse(UP_DotDelete(Convert.ToString(dWON_MCENBOAMT_HAP)));
                this.TXT01_MCENBOAMT_HAP.SetValue(String.Format("{0,9:N3}", dWON_MCENBOAMT_HAP));

                // 취급료 원화금액
                decimal dWON_MCENIPAMT = 0;
                decimal dWON_MCENCHAMT = 0;
                decimal dWON_MCENISAMT = 0;
                decimal dWON_MCENCANAMT = 0;
                decimal dWON_MCENDRAMT = 0;
                decimal dWON_MCENBOJAMT = 0;
                decimal dWON_MCENETCWAMT = 0;
                decimal dCHO_TOT_WON = 0;

                dWON_MCENIPAMT = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENIPAMT.GetValue().ToString().Trim())));
                dWON_MCENCHAMT = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENCHAMT.GetValue().ToString().Trim())));
                dWON_MCENISAMT = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENISAMT.GetValue().ToString().Trim())));
                dWON_MCENCANAMT = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENCANAMT.GetValue().ToString().Trim())));
                dWON_MCENDRAMT = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENDRAMT.GetValue().ToString().Trim())));
                dWON_MCENBOJAMT = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENBOJAMT.GetValue().ToString().Trim())));
                dWON_MCENETCWAMT = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENETCWAMT.GetValue().ToString().Trim())));

                // 취급료 원화금액
                dCHO_TOT_WON = dWON_MCENIPAMT + dWON_MCENCHAMT + dWON_MCENISAMT + dWON_MCENCANAMT + dWON_MCENDRAMT + dWON_MCENBOJAMT + dWON_MCENETCWAMT;
                this.TXT01_CHO_TOT_WON.SetValue(String.Format("{0,9:N3}", dCHO_TOT_WON));

                // 할증료 원화금액
                decimal dMCENIPOVAM = 0;
                decimal dMCENCHOVAM = 0;
                decimal dCHOV_TOT_WON = 0;

                dMCENIPOVAM = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENIPOVAM.GetValue().ToString().Trim())));
                dMCENCHOVAM = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENCHOVAM.GetValue().ToString().Trim())));

                // 할증료 원화금액
                dCHOV_TOT_WON = dMCENIPOVAM + dMCENCHOVAM;
                this.TXT01_CHOV_TOT_WON.SetValue(String.Format("{0,9:N3}", dCHOV_TOT_WON));

                // UTILITY 원화금액
                decimal dMCENCLEAMT = 0;
                decimal dMCENGAAMT = 0;
                decimal dMCENJILAMT = 0;
                decimal dMCENVOCAMT = 0;
                decimal dMCENLPGAMT = 0;
                decimal dUTI_TOT_WON = 0;

                dMCENCLEAMT = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENCLEAMT.GetValue().ToString().Trim())));
                dMCENGAAMT = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENGAAMT.GetValue().ToString().Trim())));
                dMCENJILAMT = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENJILAMT.GetValue().ToString().Trim())));
                dMCENVOCAMT = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENVOCAMT.GetValue().ToString().Trim())));
                dMCENLPGAMT = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENLPGAMT.GetValue().ToString().Trim())));

                // UTILITY 원화금액
                dUTI_TOT_WON = dMCENCLEAMT + dMCENGAAMT + dMCENJILAMT + dMCENVOCAMT + dMCENLPGAMT;
                this.TXT01_UTI_TOT_WON.SetValue(String.Format("{0,9:N3}", dUTI_TOT_WON));

                // 매출 총원화금액
                decimal dTOT_WON = 0;

                // 매출 총원화금액
                dTOT_WON = dWON_MCENBOAMT_HAP + dCHO_TOT_WON + dCHOV_TOT_WON + dUTI_TOT_WON;
                dTOT_WON = decimal.Parse(UP_DotDelete(Convert.ToString(dTOT_WON)));
                this.TXT01_TOT_WON.SetValue(String.Format("{0,9:N3}", dTOT_WON));
            }
            else // 외화
            {
                // 보관료 원화금액
                decimal dWON_MCENBOAMT = 0;
                decimal dWON_MCENBUDUAM = 0;
                decimal dWON_MCENTOJIAM = 0;
                decimal dWON_MCENBOAMT_HAP = 0;

                dWON_MCENBOAMT = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENBOAMT.GetValue().ToString().Trim())));
                dWON_MCENBUDUAM = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENBUDUAM.GetValue().ToString().Trim())));
                dWON_MCENTOJIAM = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENTOJIAM.GetValue().ToString().Trim())));

                // 보관료 원화금액
                dWON_MCENBOAMT_HAP = dWON_MCENBOAMT + dWON_MCENBUDUAM + dWON_MCENTOJIAM;
                dWON_MCENBOAMT_HAP = decimal.Parse(UP_DotDelete(Convert.ToString(dWON_MCENBOAMT_HAP)));
                this.TXT01_MCENBOAMT_HAP.SetValue(String.Format("{0,9:N3}", dWON_MCENBOAMT_HAP));

                // 취급료 원화금액
                decimal dWON_MCENIPAMT = 0;
                decimal dWON_MCENCHAMT = 0;
                decimal dWON_MCENISAMT = 0;
                decimal dWON_MCENCANAMT = 0;
                decimal dWON_MCENDRAMT = 0;
                decimal dWON_MCENBOJAMT = 0;
                decimal dCHO_TOT_WON = 0;

                dWON_MCENIPAMT = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENIPAMT.GetValue().ToString().Trim())));
                dWON_MCENCHAMT = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENCHAMT.GetValue().ToString().Trim())));
                dWON_MCENISAMT = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENISAMT.GetValue().ToString().Trim())));
                dWON_MCENCANAMT = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENCANAMT.GetValue().ToString().Trim())));
                dWON_MCENDRAMT = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENDRAMT.GetValue().ToString().Trim())));
                dWON_MCENBOJAMT = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENBOJAMT.GetValue().ToString().Trim())));

                // 취급료 원화금액
                dCHO_TOT_WON = dWON_MCENIPAMT + dWON_MCENCHAMT + dWON_MCENISAMT + dWON_MCENCANAMT + dWON_MCENDRAMT + dWON_MCENBOJAMT;
                this.TXT01_CHO_TOT_WON.SetValue(String.Format("{0,9:N3}", dCHO_TOT_WON));

                // 할증료 원화금액
                decimal dMCENIPOVAM = 0;
                decimal dMCENCHOVAM = 0;
                decimal dCHOV_TOT_WON = 0;

                dMCENIPOVAM = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENIPOVAM.GetValue().ToString().Trim())));
                dMCENCHOVAM = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENCHOVAM.GetValue().ToString().Trim())));

                // 할증료 원화금액
                dCHOV_TOT_WON = dMCENIPOVAM + dMCENCHOVAM;
                this.TXT01_CHOV_TOT_WON.SetValue(String.Format("{0,9:N3}", dCHOV_TOT_WON));

                // UTILITY 원화금액
                decimal dMCENCLEAMT = 0;
                decimal dMCENGAAMT = 0;
                decimal dMCENJILAMT = 0;
                decimal dMCENVOCAMT = 0;
                decimal dMCENLPGAMT = 0;
                decimal dUTI_TOT_WON = 0;

                dMCENCLEAMT = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENCLEAMT.GetValue().ToString().Trim())));
                dMCENGAAMT = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENGAAMT.GetValue().ToString().Trim())));
                dMCENJILAMT = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENJILAMT.GetValue().ToString().Trim())));
                dMCENVOCAMT = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENVOCAMT.GetValue().ToString().Trim())));
                dMCENLPGAMT = decimal.Parse(UP_DotDelete(Get_Numeric(this.TXT01_MCENLPGAMT.GetValue().ToString().Trim())));

                // UTILITY 원화금액
                dUTI_TOT_WON = dMCENCLEAMT + dMCENGAAMT + dMCENJILAMT + dMCENVOCAMT + dMCENLPGAMT;
                this.TXT01_UTI_TOT_WON.SetValue(String.Format("{0,9:N3}", dUTI_TOT_WON));

                // 매출 총원화금액
                decimal dTOT_WON = 0;

                // 매출 총원화금액
                dTOT_WON = dWON_MCENBOAMT_HAP + dCHO_TOT_WON + dCHOV_TOT_WON + dUTI_TOT_WON;
                dTOT_WON = decimal.Parse(UP_DotDelete(Convert.ToString(dTOT_WON)));
                this.TXT01_TOT_WON.SetValue(String.Format("{0,9:N3}", dTOT_WON));



                // 보관료 외화금액
                decimal dDAL_MCENBOAMT = 0;
                decimal dDAL_MCENBUDUAM = 0;
                decimal dDAL_MCENTOJIAM = 0;
                decimal dDAL_MCENBOAMT_HAP = 0;

                dDAL_MCENBOAMT = decimal.Parse(Get_Numeric(this.TXT01_MCENBODAL.GetValue().ToString().Trim()));
                dDAL_MCENBUDUAM = decimal.Parse(Get_Numeric(this.TXT01_MCENBUDUDL.GetValue().ToString().Trim()));
                dDAL_MCENTOJIAM = decimal.Parse(Get_Numeric(this.TXT01_MCENTOJIDL.GetValue().ToString().Trim()));

                // 보관료 외화금액
                dDAL_MCENBOAMT_HAP = dDAL_MCENBOAMT + dDAL_MCENBUDUAM + dDAL_MCENTOJIAM;
                this.TXT01_MCENBODAL_HAP.SetValue(String.Format("{0,9:N3}", dDAL_MCENBOAMT_HAP));

                // 취급료 외화금액
                decimal dDAL_MCENIPAMT = 0;
                decimal dDAL_MCENCHAMT = 0;
                decimal dDAL_MCENISAMT = 0;
                decimal dDAL_MCENCANAMT = 0;
                decimal dDAL_MCENDRAMT = 0;
                decimal dDAL_MCENBOJAMT = 0;
                decimal dDAL_MCENETCDALAMT = 0;
                decimal dCHO_TOT_DAL = 0;

                dDAL_MCENIPAMT = decimal.Parse(Get_Numeric(this.TXT01_MCENIPDAL.GetValue().ToString().Trim()));
                dDAL_MCENCHAMT = decimal.Parse(Get_Numeric(this.TXT01_MCENCHDAL.GetValue().ToString().Trim()));
                dDAL_MCENISAMT = decimal.Parse(Get_Numeric(this.TXT01_MCENISDAL.GetValue().ToString().Trim()));
                dDAL_MCENCANAMT = decimal.Parse(Get_Numeric(this.TXT01_MCENCANDAL.GetValue().ToString().Trim()));
                dDAL_MCENDRAMT = decimal.Parse(Get_Numeric(this.TXT01_MCENDRDAL.GetValue().ToString().Trim()));
                dDAL_MCENBOJAMT = decimal.Parse(Get_Numeric(this.TXT01_MCENBOJDAL.GetValue().ToString().Trim()));
                dDAL_MCENETCDALAMT = decimal.Parse(Get_Numeric(this.TXT01_MCENETCDALAMT.GetValue().ToString().Trim()));

                // 취급료 외화금액
                dCHO_TOT_DAL = dDAL_MCENIPAMT + dDAL_MCENCHAMT + dDAL_MCENISAMT + dDAL_MCENCANAMT + dDAL_MCENDRAMT + dDAL_MCENBOJAMT + dDAL_MCENETCDALAMT;
                this.TXT01_CHO_TOT_DAL.SetValue(String.Format("{0,9:N3}", dCHO_TOT_DAL));

                // 할증료 외화금액
                decimal dMCENIPOVDL = 0;
                decimal dMCENCHOVDL = 0;
                decimal dCHOV_TOT_DAL = 0;

                dMCENIPOVDL = decimal.Parse(Get_Numeric(this.TXT01_MCENIPOVDL.GetValue().ToString().Trim()));
                dMCENCHOVDL = decimal.Parse(Get_Numeric(this.TXT01_MCENCHOVDL.GetValue().ToString().Trim()));

                // 할증료 외화금액
                dCHOV_TOT_DAL = dMCENIPOVDL + dMCENCHOVDL;
                this.TXT01_CHOV_TOT_DAL.SetValue(String.Format("{0,9:N3}", dCHOV_TOT_DAL));

                // UTILITY 외화금액
                decimal dMCENCLEDAL = 0;
                decimal dMCENGADAL = 0;
                decimal dMCENJILDAL = 0;
                decimal dMCENVOCDAL = 0;
                decimal dMCENLPGDAL = 0;
                decimal dUTI_TOT_DAL = 0;

                dMCENCLEDAL = decimal.Parse(Get_Numeric(this.TXT01_MCENCLEDAL.GetValue().ToString().Trim()));
                dMCENGADAL = decimal.Parse(Get_Numeric(this.TXT01_MCENGADAL.GetValue().ToString().Trim()));
                dMCENJILDAL = decimal.Parse(Get_Numeric(this.TXT01_MCENJILDAL.GetValue().ToString().Trim()));
                dMCENVOCDAL = decimal.Parse(Get_Numeric(this.TXT01_MCENVOCDAL.GetValue().ToString().Trim()));
                dMCENLPGDAL = decimal.Parse(Get_Numeric(this.TXT01_MCENLPGDAL.GetValue().ToString().Trim()));

                // UTILITY 외화금액
                dUTI_TOT_DAL = dMCENCLEDAL + dMCENGADAL + dMCENJILDAL + dMCENVOCDAL + dMCENLPGDAL;
                this.TXT01_UTI_TOT_DAL.SetValue(String.Format("{0,9:N3}", dUTI_TOT_DAL));

                // 매출 총외화금액
                decimal dTOT_DAL = 0;

                // 매출 총외화금액
                dTOT_DAL = dDAL_MCENBOAMT_HAP + dCHO_TOT_DAL + dCHOV_TOT_DAL + dUTI_TOT_DAL;
                this.TXT01_TOT_DAL.SetValue(String.Format("{0,9:N3}", dTOT_DAL));

            }
        }
        #endregion

        #region Description : 저장 ProcessCheck
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            string sMCCONTNO = string.Empty;

            sMCCONTNO = this.TXT01_MCCONTNO1.GetValue().ToString() + "-" + Set_Fill3(this.TXT01_MCCONTNO2.GetValue().ToString());


            // 계약번호
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_676G5597", sMCCONTNO);

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_6CMGS176");

                SetFocus(this.TXT01_MCCONTNO1);

                e.Successed = false;
                return;
            }

            if (fsGUBUN == "UPT") // 수정
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_75BGG454",
                    Get_Date(this.DTP01_MCDATE.GetValue().ToString()),
                    this.CBH01_MCHWAJU.GetValue().ToString(),
                    this.TXT01_MCSEQ.GetValue().ToString(),
                    this.CBH01_MCHWAMUL.GetValue().ToString(),
                    this.TXT01_MCTANKNO.GetValue().ToString().Trim(),
                    sMCCONTNO.ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["MCJPNO"].ToString() != "")
                    {
                        this.ShowMessage("TY_M_UT_73K9X969");

                        SetFocus(this.TXT01_MCCONTNO1);

                        e.Successed = false;
                        return;
                    }
                }
            }

            // 화주
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_736DD855", this.CBH01_MCHWAJU.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["VNPGUBN"].ToString() == "6")
                {
                    if (this.CBO01_MCREQGB.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_UT_66FHW237");

                        SetFocus(this.CBO01_MCREQGB);

                        e.Successed = false;
                        return;
                    }

                    if (double.Parse(Get_Numeric(this.TXT01_MCENRATE.GetValue().ToString())) == 0)
                    {
                        this.ShowMessage("TY_M_UT_736D8847");

                        SetFocus(this.TXT01_MCENRATE);

                        e.Successed = false;
                        return;
                    }

                    if (this.CBH01_MCCURRCD.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_UT_736D8848");

                        SetFocus(this.CBH01_MCCURRCD.CodeText);

                        e.Successed = false;
                        return;
                    }
                }                
            }

            // 유틸리티 질소금액 존재 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_75GJK532",
                Get_Date(this.DTP01_MCDATE.GetValue().ToString()),
                this.CBH01_MCHWAJU.GetValue().ToString(),
                this.CBH01_MCHWAMUL.GetValue().ToString(),
                this.TXT01_MCTANKNO.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (fsGUBUN == "NEW")
                {
                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_UT_75GJL533");

                        SetFocus(this.CBH01_MCHWAJU.CodeText);

                        e.Successed = false;
                        return;
                    }
                }
                else
                {
                    if (dt.Rows.Count > 1)
                    {
                        this.ShowMessage("TY_M_UT_75GJL533");

                        SetFocus(this.CBH01_MCHWAJU.CodeText);

                        e.Successed = false;
                        return;
                    }
                }
            }

            // 유틸리티 가열료 금액 존재 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_75GJM536",
                Get_Date(this.DTP01_MCDATE.GetValue().ToString()),
                this.CBH01_MCHWAJU.GetValue().ToString(),
                this.CBH01_MCHWAMUL.GetValue().ToString(),
                this.TXT01_MCTANKNO.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (fsGUBUN == "NEW")
                {
                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_UT_75GJL534");

                        SetFocus(this.CBH01_MCHWAJU.CodeText);

                        e.Successed = false;
                        return;
                    }
                }
                else
                {
                    if (dt.Rows.Count > 1)
                    {
                        this.ShowMessage("TY_M_UT_75GJL534");

                        SetFocus(this.CBH01_MCHWAJU.CodeText);

                        e.Successed = false;
                        return;
                    }
                }
            }

            // 유틸리티 탱크세척 금액 존재 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_75GJN537",
                Get_Date(this.DTP01_MCDATE.GetValue().ToString()),
                this.CBH01_MCHWAJU.GetValue().ToString(),
                this.CBH01_MCHWAMUL.GetValue().ToString(),
                this.TXT01_MCTANKNO.GetValue().ToString().Trim()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (fsGUBUN == "NEW")
                {
                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_UT_75GJM535");

                        SetFocus(this.CBH01_MCHWAJU.CodeText);

                        e.Successed = false;
                        return;
                    }
                }
                else
                {
                    if (dt.Rows.Count > 1)
                    {
                        this.ShowMessage("TY_M_UT_75GJM535");

                        SetFocus(this.CBH01_MCHWAJU.CodeText);

                        e.Successed = false;
                        return;
                    }
                }
            }

            if (this.CBO01_MCREQGB.GetValue().ToString() == "1")
            {
                fsMCENBOHP   = "1";
                fsMCENIPHP   = "1";
                fsMCENCHHP   = "1";
                fsMCENISHP   = "1";
                fsMCENCANHP  = "1";
                fsMCENDRHP   = "1";
                fsMCENBOJHP  = "1";
                fsMCENIPOVHP = "1";
                fsMCENCHOVHP = "1";

                this.TXT01_MCENBODAL.SetValue("0");
                this.TXT01_MCENIPDAL.SetValue("0");
                this.TXT01_MCENCHDAL.SetValue("0");
                this.TXT01_MCENISDAL.SetValue("0");
                this.TXT01_MCENCANDAL.SetValue("0");
                this.TXT01_MCENDRDAL.SetValue("0");
                this.TXT01_MCENBOJDAL.SetValue("0");
                this.TXT01_MCENIPOVDL.SetValue("0");
                this.TXT01_MCENCHOVDL.SetValue("0");
                this.TXT01_MCENBUDUDL.SetValue("0");
                this.TXT01_MCENTOJIDL.SetValue("0");
                this.TXT01_MCENJILDAL.SetValue("0");
                this.TXT01_MCENGADAL.SetValue("0");
                this.TXT01_MCENCLEDAL.SetValue("0");
                this.TXT01_MCENVOCDAL.SetValue("0");
                this.TXT01_MCENLPGDAL.SetValue("0");
                this.TXT01_CHO_TOT_WON.SetValue("0");
                this.TXT01_CHO_TOT_DAL.SetValue("0");
                this.TXT01_CHOV_TOT_WON.SetValue("0");
                this.TXT01_CHOV_TOT_DAL.SetValue("0");
                this.TXT01_UTI_TOT_WON.SetValue("0");
                this.TXT01_UTI_TOT_DAL.SetValue("0");
                this.TXT01_TOT_WON.SetValue("0");
                this.TXT01_TOT_DAL.SetValue("0");
                this.TXT01_MCENBOAMT_HAP.SetValue("0");
                this.TXT01_MCENBODAL_HAP.SetValue("0");
            }
            else
            {
                fsMCENBOHP   = "2";
                fsMCENIPHP   = "2";
                fsMCENCHHP   = "2";
                fsMCENISHP   = "2";
                fsMCENCANHP  = "2";
                fsMCENDRHP   = "2";
                fsMCENBOJHP  = "2";
                fsMCENIPOVHP = "2";
                fsMCENCHOVHP = "2";
            }

            if (this.CBO01_MCREQGB.GetValue().ToString() == "2" && Get_Numeric(this.TXT01_MCENRATE.GetValue().ToString()) != "0")
            {
                if (this.CBO01_MCVATGB.GetValue().ToString() == "11" || this.CBO01_MCVATGB.GetValue().ToString() == "61")
                {
                    this.ShowMessage("TY_M_UT_73KF0992");

                    SetFocus(this.CBO01_MCREQGB);

                    e.Successed = false;
                    return;
                }
            }


            double dFieldCompute = 0;


            #region Description : 보관료

            if (this.CBO01_MCREQGB.GetValue().ToString() == "2" && double.Parse(Get_Numeric(this.TXT01_MCENBOAMT.GetValue().ToString())) != 0)
            {
                // 외화
                dFieldCompute = ((Convert.ToDouble(Get_Numeric(this.TXT01_MCENBOAMT.GetValue().ToString())) / Convert.ToDouble(Get_Numeric(this.TXT01_MCENRATE.GetValue().ToString()))) * 100);
                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));
                dFieldCompute = (dFieldCompute / 100);
                this.TXT01_MCENBODAL.SetValue(String.Format("{0:#,0.000}", dFieldCompute));
            }

            #endregion


            if (this.CBO01_MCREQGB.GetValue().ToString() != "1" && this.CBO01_MCREQGB.GetValue().ToString() != "2")
            {
                this.ShowMessage("TY_M_UT_66FHW237");

                SetFocus(this.CBO01_MCREQGB);

                e.Successed = false;
                return;
            }

            this.TXT01_MCENRATE.SetValue(Convert.ToString(Double.Parse(Get_Numeric(this.TXT01_MCENRATE.GetValue().ToString().Trim())).ToString("#0.000")));
            this.TXT01_MCENBOAMT.SetValue(Convert.ToString(Double.Parse(Get_Numeric(this.TXT01_MCENBOAMT.GetValue().ToString())).ToString("#0.00")));

            this.TXT01_MCENIPQTY.SetValue(Convert.ToString(Double.Parse(Get_Numeric(this.TXT01_MCENIPQTY.GetValue().ToString().Trim())).ToString("#0.000")));   //13P 3       COLHDG('입고량')
            if (Convert.ToDouble(this.TXT01_MCENIPQTY.GetValue().ToString()) > 0)
            {
                if (this.CBO01_MCENIPDAN.GetValue().ToString().Trim() == "")
                {
                    this.ShowMessage("TY_M_UT_75CEM458");

                    SetFocus(this.CBO01_MCENIPDAN);

                    e.Successed = false;
                    return;
                }
            }

            //취급료 요율								   
            if (Convert.ToDouble(Get_Numeric(this.TXT01_MCENIPQTY.GetValue().ToString().Trim())) > 0)
            {
                this.TXT01_MCENIPRAT.SetValue(Convert.ToString(Double.Parse(Get_Numeric(this.TXT01_MCENIPRAT.GetValue().ToString().Trim())).ToString("#0.000")));

                if (Convert.ToDouble(Get_Numeric(this.TXT01_MCENIPRAT.GetValue().ToString().Trim())) <= 0)
                {
                    this.ShowMessage("TY_M_UT_75CEN459");

                    SetFocus(this.TXT01_MCENIPRAT);

                    e.Successed = false;
                    return;
                }
            }
            else
            {
                this.TXT01_MCENIPRAT.SetValue("0");
            }

            // 기타매출코드 체크
            if (double.Parse(Get_Numeric(this.TXT01_MCENETCWAMT.GetValue().ToString().Trim())) != 0 || double.Parse(Get_Numeric(this.TXT01_MCENETCDALAMT.GetValue().ToString().Trim())) != 0)
            {
                if (this.CBH01_MCETCCODE.GetValue().ToString() == "")
                {
                    // 기타매출 코드를 입력하세요.
                    this.ShowMessage("TY_M_UT_C4EDE249");

                    SetFocus(this.CBH01_MCETCCODE.CodeText);

                    e.Successed = false;
                    return;
                }
            }
            else if (double.Parse(Get_Numeric(this.TXT01_MCENETCWAMT.GetValue().ToString().Trim())) == 0 && double.Parse(Get_Numeric(this.TXT01_MCENETCDALAMT.GetValue().ToString().Trim())) == 0)
            {
                if (this.CBH01_MCETCCODE.GetValue().ToString() != "")
                {
                    this.CBH01_MCETCCODE.SetValue("");
                }
            }

            #region Description : 입고 취급료

            this.TXT01_MCENIPAMT.SetValue("0");

            if (this.CBO01_MCREQGB.GetValue().ToString() == "1")
            {
                dFieldCompute = ((Convert.ToDouble(Get_Numeric(this.TXT01_MCENIPQTY.GetValue().ToString())) * Convert.ToDouble(Get_Numeric(this.TXT01_MCENIPRAT.GetValue().ToString()))) / 10);

                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));

                this.TXT01_MCENIPAMT.SetValue(Convert.ToString(Double.Parse(Convert.ToString(dFieldCompute * 10)).ToString("#,0.000")));
            }
            else if (this.CBO01_MCREQGB.GetValue().ToString() == "2")
            {
                // 외화
                dFieldCompute = (((Convert.ToDouble(Get_Numeric(this.TXT01_MCENIPQTY.GetValue().ToString())) * Convert.ToDouble(Get_Numeric(this.TXT01_MCENIPRAT.GetValue().ToString())))) / Convert.ToDouble(Get_Numeric(this.TXT01_MCENRATE.GetValue().ToString())) * 100);
                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));
                dFieldCompute = (dFieldCompute / 100);

                this.TXT01_MCENIPDAL.SetValue(String.Format("{0:#,0.000}", dFieldCompute));

                // 원화 수정
                dFieldCompute = ((Convert.ToDouble(Get_Numeric(this.TXT01_MCENIPQTY.GetValue().ToString())) * Convert.ToDouble(Get_Numeric(this.TXT01_MCENIPRAT.GetValue().ToString()))) / 10);

                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));

                this.TXT01_MCENIPAMT.SetValue(Convert.ToString(Double.Parse(Convert.ToString(dFieldCompute * 10)).ToString("#,0.000")));
            }

            // 원화 수정
            dFieldCompute = (Convert.ToDouble(Get_Numeric(this.TXT01_MCENIPAMT.GetValue().ToString())) / 10);

            dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));

            this.TXT01_MCENIPAMT.SetValue(Convert.ToString(Double.Parse(Convert.ToString(dFieldCompute * 10)).ToString("#0.000")));

            #endregion

            #region Description : 출고 취급료

            this.TXT01_MCENCHQTY.SetValue(Convert.ToString(Double.Parse(Get_Numeric(this.TXT01_MCENCHQTY.GetValue().ToString().Trim())).ToString("#0.000")));

            if (Convert.ToDouble(Get_Numeric(this.TXT01_MCENCHQTY.GetValue().ToString().Trim())) > 0)
            {
                if (this.CBO01_MCENCHDAN.GetValue().ToString().Trim() == "")
                {
                    this.ShowMessage("TY_M_UT_75CES462");

                    SetFocus(this.CBO01_MCENCHDAN);

                    e.Successed = false;
                    return;
                }

                if (Convert.ToDouble(Get_Numeric(this.TXT01_MCENCHRAT.GetValue().ToString().Trim())) <= 0)
                {
                    this.ShowMessage("TY_M_UT_75CEN459");

                    SetFocus(this.TXT01_MCENCHRAT);

                    e.Successed = false;
                    return;
                }
            }

            this.TXT01_MCENCHRAT.SetValue(Convert.ToString(Double.Parse(Get_Numeric(this.TXT01_MCENCHRAT.GetValue().ToString().Trim())).ToString("#0.000")));

            this.TXT01_MCENCHAMT.SetValue(Convert.ToString(Double.Parse(Convert.ToString(Convert.ToDouble(Get_Numeric(this.TXT01_MCENCHQTY.GetValue().ToString())) *
                                          Convert.ToDouble(Get_Numeric(this.TXT01_MCENCHRAT.GetValue().ToString())))).ToString("#0.000")));

            if (this.CBO01_MCREQGB.GetValue().ToString() == "1")
            {
                dFieldCompute = (Convert.ToDouble(Get_Numeric(this.TXT01_MCENCHAMT.GetValue().ToString())) / 10);

                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));

                this.TXT01_MCENCHAMT.SetValue(Convert.ToString(dFieldCompute * 10));
            }
            else if (this.CBO01_MCREQGB.GetValue().ToString() == "2")
            {
                // 외화
                dFieldCompute = (Convert.ToDouble(Get_Numeric(this.TXT01_MCENCHQTY.GetValue().ToString())) * Convert.ToDouble(Get_Numeric(this.TXT01_MCENCHRAT.GetValue().ToString())))
                    / Convert.ToDouble(Get_Numeric(this.TXT01_MCENRATE.GetValue().ToString())) * 100;
                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));
                dFieldCompute = (dFieldCompute / 100);

                this.TXT01_MCENCHDAL.SetValue(String.Format("{0:#,0.000}", dFieldCompute));

                // 원화 수정
                dFieldCompute = (Convert.ToDouble(Get_Numeric(this.TXT01_MCENCHAMT.GetValue().ToString())) / 10);

                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));

                this.TXT01_MCENCHAMT.SetValue(Convert.ToString(dFieldCompute * 10));
            }

            #endregion

            #region Description : 이송료

            this.TXT01_MCENISQTY.SetValue(Convert.ToString(Double.Parse(Get_Numeric(this.TXT01_MCENISQTY.GetValue().ToString().Trim())).ToString("#0.000")));

            //이송 취급료
            if (Convert.ToDouble(Get_Numeric(this.TXT01_MCENISQTY.GetValue().ToString().Trim())) > 0)
            {
                if (this.CBO01_MCENISDAN.GetValue().ToString().Trim() == "")
                {
                    this.ShowMessage("TY_M_UT_75CEV463");

                    SetFocus(this.CBO01_MCENISDAN);

                    e.Successed = false;
                    return;
                }

                if (Convert.ToDouble(Get_Numeric(this.TXT01_MCENISRAT.GetValue().ToString().Trim())) <= 0)
                {
                    this.ShowMessage("TY_M_UT_75CEN459");

                    SetFocus(this.TXT01_MCENISRAT);

                    e.Successed = false;
                    return;
                }
            }

            this.TXT01_MCENISRAT.SetValue(Convert.ToString(Double.Parse(Get_Numeric(this.TXT01_MCENISRAT.GetValue().ToString().Trim())).ToString("#0.000")));

            if (fsGUBUN == "NEW") //등록
            {
                this.TXT01_MCENISAMT.SetValue(Convert.ToString(Double.Parse(Convert.ToString(Convert.ToDouble(Get_Numeric(this.TXT01_MCENISQTY.GetValue().ToString())) * Convert.ToDouble(Get_Numeric(this.TXT01_MCENISRAT.GetValue().ToString())))).ToString("#,0.000")));
            }
            else //수정
            {
                this.TXT01_MCENISAMT.SetValue(Convert.ToString(Double.Parse(Convert.ToString(Convert.ToDouble(Get_Numeric(this.TXT01_MCENISQTY.GetValue().ToString())) * Convert.ToDouble(Get_Numeric(this.TXT01_MCENISRAT.GetValue().ToString())))).ToString("#,0.000")));
            }


            if (this.CBO01_MCREQGB.GetValue().ToString() == "1")
            {
                dFieldCompute = (Convert.ToDouble(Get_Numeric(this.TXT01_MCENISAMT.GetValue().ToString())) / 10);

                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));

                this.TXT01_MCENISAMT.SetValue( Convert.ToString(dFieldCompute * 10));
            }
            else if (this.CBO01_MCREQGB.GetValue().ToString() == "2")
            {
                // 외화
                dFieldCompute = (Convert.ToDouble(Get_Numeric(this.TXT01_MCENISQTY.GetValue().ToString())) * Convert.ToDouble(Get_Numeric(this.TXT01_MCENISRAT.GetValue().ToString())))
                               / Convert.ToDouble(Get_Numeric(this.TXT01_MCENRATE.GetValue().ToString())) * 100;
                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));
                dFieldCompute = (dFieldCompute / 100);

                this.TXT01_MCENISDAL.SetValue(String.Format("{0:#,0.000}", dFieldCompute));

                // 원화 수정
                dFieldCompute = (Convert.ToDouble(Get_Numeric(this.TXT01_MCENISAMT.GetValue().ToString())) / 10);

                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));

                this.TXT01_MCENISAMT.SetValue(Convert.ToString(dFieldCompute * 10));
            }

            #endregion

            #region Description : CAN

            this.TXT01_MCENCANQTY.SetValue(Convert.ToString(Double.Parse(Get_Numeric(this.TXT01_MCENCANQTY.GetValue().ToString().Trim())).ToString("#0.000")));
            this.TXT01_MCENCANRAT.SetValue(Convert.ToString(Double.Parse(Get_Numeric(this.TXT01_MCENCANRAT.GetValue().ToString().Trim())).ToString("#,0.000")));
            this.TXT01_MCENCANAMT.SetValue(Convert.ToString(Double.Parse(Convert.ToString(Convert.ToDouble(Get_Numeric(this.TXT01_MCENCANQTY.GetValue().ToString().Trim())) *
                                           Convert.ToDouble(Get_Numeric(this.TXT01_MCENCANRAT.GetValue().ToString().Trim())))).ToString("#,0.000")));

            if (this.CBO01_MCREQGB.GetValue().ToString() == "1")
            {
                dFieldCompute = (Convert.ToDouble(Get_Numeric(this.TXT01_MCENCANAMT.GetValue().ToString())) / 10);
                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));
                this.TXT01_MCENCANAMT.SetValue(Convert.ToString(dFieldCompute * 10));
            }
            else if (this.CBO01_MCREQGB.GetValue().ToString() == "2")
            {
                // 외화
                dFieldCompute = (Convert.ToDouble(Get_Numeric(this.TXT01_MCENCANQTY.GetValue().ToString())) * Convert.ToDouble(Get_Numeric(this.TXT01_MCENCANRAT.GetValue().ToString())))
                               / Convert.ToDouble(Get_Numeric(this.TXT01_MCENRATE.GetValue().ToString())) * 100;
                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));
                dFieldCompute = (dFieldCompute / 100);

                this.TXT01_MCENCANDAL.SetValue(String.Format("{0:#,0.000}", dFieldCompute));

                // 원화 수정
                dFieldCompute = (Convert.ToDouble(Get_Numeric(this.TXT01_MCENCANAMT.GetValue().ToString())) / 10);
                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));
                this.TXT01_MCENCANAMT.SetValue(Convert.ToString(dFieldCompute * 10));
            }

            #endregion

            #region Description : DRUM

            this.TXT01_MCENDRQTY.SetValue(Convert.ToString(Double.Parse(Get_Numeric(this.TXT01_MCENDRQTY.GetValue().ToString().Trim())).ToString("#0.000")));
            this.TXT01_MCENDRRAT.SetValue(Convert.ToString(Double.Parse(Get_Numeric(this.TXT01_MCENDRRAT.GetValue().ToString().Trim())).ToString("#0.000")));
            this.TXT01_MCENDRAMT.SetValue(Convert.ToString(Double.Parse(Convert.ToString(Convert.ToDouble(Get_Numeric(this.TXT01_MCENDRQTY.GetValue().ToString().Trim())) *
                                          Convert.ToDouble(Get_Numeric(this.TXT01_MCENDRRAT.GetValue().ToString().Trim())))).ToString("#0.000")));

            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            if (this.CBO01_MCREQGB.GetValue().ToString() == "1")
            {
                dFieldCompute = (Convert.ToDouble(this.TXT01_MCENDRAMT.GetValue().ToString()) / 10);
                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));
                this.TXT01_MCENDRAMT.SetValue(Convert.ToString(dFieldCompute * 10));
            }
            else if (this.CBO01_MCREQGB.GetValue().ToString() == "2")
            {
                // 외화
                dFieldCompute = (Convert.ToDouble(Get_Numeric(this.TXT01_MCENDRQTY.GetValue().ToString())) * Convert.ToDouble(Get_Numeric(this.TXT01_MCENDRRAT.GetValue().ToString())))
                    / Convert.ToDouble(Get_Numeric(this.TXT01_MCENRATE.GetValue().ToString())) * 100;
                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));
                dFieldCompute = (dFieldCompute / 100);

                this.TXT01_MCENDRDAL.SetValue(String.Format("{0:#,0.000}", dFieldCompute));

                // 원화 수정
                dFieldCompute = (Convert.ToDouble(this.TXT01_MCENDRAMT.GetValue().ToString()) / 10);
                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));
                this.TXT01_MCENDRAMT.SetValue(Convert.ToString(dFieldCompute * 10));
            }

            #endregion

            #region Description : 보장물동량

            this.TXT01_MCENBOJQTY.SetValue(Convert.ToString(Double.Parse(Get_Numeric(this.TXT01_MCENBOJQTY.GetValue().ToString().Trim())).ToString("#,0.000")));

            if (Convert.ToDouble(Get_Numeric(this.TXT01_MCENBOJQTY.GetValue().ToString().Trim())) > 0)
            {
                if (this.CBO01_MCENBOJDAN.GetValue().ToString().Trim() == "")
                {
                    this.ShowMessage("TY_M_UT_75CF2467");

                    SetFocus(this.CBO01_MCENBOJDAN);

                    e.Successed = false;
                    return;
                }
            }

            this.TXT01_MCENBOJRAT.SetValue(Convert.ToString(Double.Parse(Get_Numeric(this.TXT01_MCENBOJRAT.GetValue().ToString().Trim())).ToString("#,0.000")));
            this.TXT01_MCENBOJAMT.SetValue(Convert.ToString(Double.Parse(Convert.ToString(Convert.ToDouble(Get_Numeric(this.TXT01_MCENBOJQTY.GetValue().ToString().Trim())) *
                                           Convert.ToDouble(Get_Numeric(this.TXT01_MCENBOJRAT.GetValue().ToString().Trim())))).ToString("#,0.000")));

            

            if (this.CBO01_MCREQGB.GetValue().ToString() == "1")
            {
                dFieldCompute = (Convert.ToDouble(Get_Numeric(this.TXT01_MCENBOJAMT.GetValue().ToString())) / 10);
                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));
                this.TXT01_MCENBOJAMT.SetValue(Convert.ToString(dFieldCompute * 10));
            }
            else if (this.CBO01_MCREQGB.GetValue().ToString() == "2")
            {
                // 외화
                dFieldCompute = (Convert.ToDouble(Get_Numeric(this.TXT01_MCENBOJQTY.GetValue().ToString())) * Convert.ToDouble(Get_Numeric(this.TXT01_MCENBOJRAT.GetValue().ToString())))
                               / Convert.ToDouble(Get_Numeric(this.TXT01_MCENRATE.GetValue().ToString())) * 100;
                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));
                dFieldCompute = (dFieldCompute / 100);

                this.TXT01_MCENBOJDAL.SetValue(String.Format("{0:#,0.000}", dFieldCompute));

                // 원화 수정
                dFieldCompute = (Convert.ToDouble(Get_Numeric(this.TXT01_MCENBOJAMT.GetValue().ToString())) / 10);
                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));
                this.TXT01_MCENBOJAMT.SetValue(Convert.ToString(dFieldCompute * 10));
            }

            #endregion

            #region Description : 입고 할증료

            this.TXT01_MCENIPOVQT.SetValue(Convert.ToString(Double.Parse(Get_Numeric(this.TXT01_MCENIPOVQT.GetValue().ToString().Trim())).ToString("#0.000")));

            if (Convert.ToDouble(Get_Numeric(this.TXT01_MCENIPOVQT.GetValue().ToString().Trim())) > 0)
            {
                if (this.CBO01_MCENIPOVDA.GetValue().ToString().Trim() == "")
                {
                    this.ShowMessage("TY_M_UT_75CF5468");

                    SetFocus(this.CBO01_MCENIPOVDA);

                    e.Successed = false;
                    return;
                }
                if (Convert.ToDouble(Get_Numeric(this.TXT01_MCENIPOVRA.GetValue().ToString().Trim())) <= 0)
                {
                    this.ShowMessage("TY_M_UT_75CEN459");

                    SetFocus(this.TXT01_MCENIPOVRA);

                    e.Successed = false;
                    return;
                }
            }

            this.TXT01_MCENIPOVRA.SetValue(Convert.ToString(Double.Parse(Get_Numeric(this.TXT01_MCENIPOVRA.GetValue().ToString().Trim())).ToString("#,0.000")));

            dFieldCompute = Math.Round(((Convert.ToDouble(Get_Numeric(this.TXT01_MCENIPOVQT.GetValue().ToString().Trim())) * Convert.ToDouble(Get_Numeric(this.TXT01_MCENIPOVRA.GetValue().ToString().Trim())))
                            / 10) - 0.5);
            this.TXT01_MCENIPOVAM.SetValue(Convert.ToString(Double.Parse(Convert.ToString(dFieldCompute * 10)).ToString("#,0.000")));

            if (this.CBO01_MCREQGB.GetValue().ToString() == "1")
            {
                dFieldCompute = (Convert.ToDouble(Get_Numeric(this.TXT01_MCENIPOVAM.GetValue().ToString())) / 10);
                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));
                this.TXT01_MCENIPOVAM.SetValue(Convert.ToString(dFieldCompute * 10));
            }
            else if (this.CBO01_MCREQGB.GetValue().ToString() == "2")
            {
                // 외화
                dFieldCompute = (Convert.ToDouble(Get_Numeric(this.TXT01_MCENIPOVQT.GetValue().ToString())) * Convert.ToDouble(Get_Numeric(this.TXT01_MCENIPOVRA.GetValue().ToString())))
                    / Convert.ToDouble(Get_Numeric(this.TXT01_MCENRATE.GetValue().ToString())) * 100;
                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));
                dFieldCompute = (dFieldCompute / 100);

                this.TXT01_MCENIPOVDL.SetValue(String.Format("{0:#,0.000}", dFieldCompute));

                // 원화 수정
                dFieldCompute = (Convert.ToDouble(Get_Numeric(this.TXT01_MCENIPOVAM.GetValue().ToString())) / 10);
                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));
                this.TXT01_MCENIPOVAM.SetValue(Convert.ToString(dFieldCompute * 10));
            }

            #endregion

            #region Description : 출고 할증료

            this.TXT01_MCENCHOVQT.SetValue(Convert.ToString(Double.Parse(Get_Numeric(this.TXT01_MCENCHOVQT.GetValue().ToString())).ToString("#0.000")));

            if (Convert.ToDouble(Get_Numeric(this.TXT01_MCENCHOVQT.GetValue().ToString().Trim())) > 0)
            {
                if (this.CBO01_MCENCHOVDA.GetValue().ToString().Trim() == "")
                {
                    this.ShowMessage("TY_M_UT_75CFO476");

                    SetFocus(this.CBO01_MCENCHOVDA);

                    e.Successed = false;
                    return;
                }
                if (Convert.ToDouble(Get_Numeric(this.TXT01_MCENCHOVRA.GetValue().ToString().Trim())) <= 0)
                {
                    this.ShowMessage("TY_M_UT_75CEN459");

                    SetFocus(this.TXT01_MCENCHOVRA);

                    e.Successed = false;
                    return;
                }
            }

            this.TXT01_MCENCHOVRA.SetValue(Convert.ToString(Double.Parse(Get_Numeric(this.TXT01_MCENCHOVRA.GetValue().ToString().Trim())).ToString("#,0.000")));

            //dFieldCompute = Math.Round(((Convert.ToDouble(Get_Numeric(this.TXT01_MCENCHOVQT.GetValue().ToString().Trim())) * Convert.ToDouble(Get_Numeric(this.TXT01_MCENCHOVRA.GetValue().ToString().Trim()))) / 10) - 0.5);
            dFieldCompute = Math.Truncate(((Convert.ToDouble(Get_Numeric(this.TXT01_MCENCHOVQT.GetValue().ToString().Trim())) * Convert.ToDouble(Get_Numeric(this.TXT01_MCENCHOVRA.GetValue().ToString().Trim()))) / 10));

            this.TXT01_MCENCHOVAM.SetValue(Convert.ToString(Double.Parse(Convert.ToString(dFieldCompute * 10)).ToString("#,0.000")));

            
            if (this.CBO01_MCREQGB.GetValue().ToString() == "1")
            {
                dFieldCompute = (Convert.ToDouble(Get_Numeric(this.TXT01_MCENCHOVAM.GetValue().ToString())) / 10);
                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));
                this.TXT01_MCENCHOVAM.SetValue(Convert.ToString((Convert.ToDouble(this.TXT01_MCENCHOVAM.GetValue().ToString()) / 10) * 10));
            }
            else
            {
                // 외화
                dFieldCompute = (Convert.ToDouble(Get_Numeric(this.TXT01_MCENCHOVQT.GetValue().ToString())) * Convert.ToDouble(Get_Numeric(this.TXT01_MCENCHOVRA.GetValue().ToString())))
                    / Convert.ToDouble(Get_Numeric(this.TXT01_MCENRATE.GetValue().ToString())) * 100;
                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));
                dFieldCompute = (dFieldCompute / 100);

                this.TXT01_MCENCHOVDL.SetValue(String.Format("{0:#,0.000}", dFieldCompute));

                // 원화 수정
                dFieldCompute = (Convert.ToDouble(Get_Numeric(this.TXT01_MCENCHOVAM.GetValue().ToString())) / 10);
                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));
                this.TXT01_MCENCHOVAM.SetValue(Convert.ToString((Convert.ToDouble(this.TXT01_MCENCHOVAM.GetValue().ToString()) / 10) * 10));
            }

            #endregion

            #region Description : 질소료

            dFieldCompute = ((Convert.ToDouble(Get_Numeric(this.TXT01_MCENJILQTY.GetValue().ToString())) * Convert.ToDouble(Get_Numeric(this.TXT01_MCENJILRAT.GetValue().ToString()))) / 10);

            dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));

            this.TXT01_MCENJILAMT.SetValue(Convert.ToString(Double.Parse(Convert.ToString(dFieldCompute * 10)).ToString("#,0.000")));

            if (this.CBO01_MCREQGB.GetValue().ToString() == "1")
            {
                dFieldCompute = ((Convert.ToDouble(Get_Numeric(this.TXT01_MCENJILQTY.GetValue().ToString())) * Convert.ToDouble(Get_Numeric(this.TXT01_MCENJILRAT.GetValue().ToString())))
                    / 10);
                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));
                this.TXT01_MCENJILAMT.SetValue(Convert.ToString(Double.Parse(Convert.ToString(dFieldCompute * 10)).ToString("#,0.000")));
            }
            else
            {
                // 외화
                dFieldCompute = (Convert.ToDouble(Get_Numeric(this.TXT01_MCENJILQTY.GetValue().ToString())) * Convert.ToDouble(Get_Numeric(this.TXT01_MCENJILRAT.GetValue().ToString())))
                    / Convert.ToDouble(Get_Numeric(this.TXT01_MCENRATE.GetValue().ToString())) * 100;
                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));
                dFieldCompute = (dFieldCompute / 100);

                this.TXT01_MCENJILDAL.SetValue(String.Format("{0:#,0.000}", dFieldCompute));

                // 원화 수정
                dFieldCompute = ((Convert.ToDouble(Get_Numeric(this.TXT01_MCENJILQTY.GetValue().ToString())) * Convert.ToDouble(Get_Numeric(this.TXT01_MCENJILRAT.GetValue().ToString())))
                    / 10);
                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));
                this.TXT01_MCENJILAMT.SetValue(Convert.ToString(Double.Parse(Convert.ToString(dFieldCompute * 10)).ToString("#,0.000")));
            }

            #endregion






            // 취급료 입고
            if (double.Parse(Get_Numeric(this.TXT01_MCENIPQTY.GetValue().ToString().Trim())) == 0 || double.Parse(Get_Numeric(this.TXT01_MCENIPRAT.GetValue().ToString().Trim())) == 0)
            {
                this.TXT01_MCENIPAMT.SetValue("0.000");
                fsMCENIPHP = "";
                this.TXT01_MCENIPDAL.SetValue("0.000");
            }

            // 취급료 출고
            if (double.Parse(Get_Numeric(this.TXT01_MCENCHQTY.GetValue().ToString().Trim())) == 0 || double.Parse(Get_Numeric(this.TXT01_MCENCHRAT.GetValue().ToString().Trim())) == 0)
            {
                this.TXT01_MCENCHAMT.SetValue("0.000");
                fsMCENCHHP = "";
                this.TXT01_MCENCHDAL.SetValue("0.000");
            }

            // 취급료 이송
            if (double.Parse(Get_Numeric(this.TXT01_MCENISQTY.GetValue().ToString().Trim())) == 0 || double.Parse(Get_Numeric(this.TXT01_MCENISRAT.GetValue().ToString().Trim())) == 0)
            {
                this.TXT01_MCENISAMT.SetValue("0.000");
                fsMCENISHP = "";
                this.TXT01_MCENISDAL.SetValue("0.000");
            }

            // 취급료 CAN
            if (double.Parse(Get_Numeric(this.TXT01_MCENCANQTY.GetValue().ToString().Trim())) == 0 || double.Parse(Get_Numeric(this.TXT01_MCENCANRAT.GetValue().ToString().Trim())) == 0)
            {
                this.TXT01_MCENCANAMT.SetValue("0.000");
                fsMCENCANHP = "";
                this.TXT01_MCENCANDAL.SetValue("0.000");
            }

            // 취급료 DRUM
            if (double.Parse(Get_Numeric(this.TXT01_MCENDRQTY.GetValue().ToString().Trim())) == 0 || double.Parse(Get_Numeric(this.TXT01_MCENDRRAT.GetValue().ToString().Trim())) == 0)
            {
                this.TXT01_MCENDRAMT.SetValue("0.000");
                fsMCENDRHP = "";
                this.TXT01_MCENDRDAL.SetValue("0.000");
            }

            // 취급료 보장물동량
            if (double.Parse(Get_Numeric(this.TXT01_MCENBOJQTY.GetValue().ToString().Trim())) == 0 || double.Parse(Get_Numeric(this.TXT01_MCENBOJRAT.GetValue().ToString().Trim())) == 0)
            {
                this.TXT01_MCENBOJAMT.SetValue("0.000");
                fsMCENBOJHP = "";
                this.TXT01_MCENBOJDAL.SetValue("0.000");
            }

            // 할증료 입고
            if (double.Parse(Get_Numeric(this.TXT01_MCENIPOVQT.GetValue().ToString().Trim())) == 0 || double.Parse(Get_Numeric(this.TXT01_MCENIPOVRA.GetValue().ToString().Trim())) == 0)
            {
                this.TXT01_MCENIPOVAM.SetValue("0.000");
                fsMCENIPOVHP = "";
                this.TXT01_MCENIPOVDL.SetValue("0.000");
            }

            // 할증료 출고
            if (double.Parse(Get_Numeric(this.TXT01_MCENCHOVQT.GetValue().ToString().Trim())) == 0 || double.Parse(Get_Numeric(this.TXT01_MCENCHOVRA.GetValue().ToString().Trim())) == 0)
            {
                this.TXT01_MCENCHOVAM.SetValue("0.000");
                fsMCENCHOVHP = "";
                this.TXT01_MCENCHOVDL.SetValue("0.000");
            }

            // 질소료
            if (double.Parse(Get_Numeric(this.TXT01_MCENJILQTY.GetValue().ToString().Trim())) == 0 || double.Parse(Get_Numeric(this.TXT01_MCENJILRAT.GetValue().ToString().Trim())) == 0)
            {
                this.TXT01_MCENJILAMT.SetValue("0.000");
                this.TXT01_MCENJILDAL.SetValue("0.000");
            }

            /* 추가(2008.09.29) 끝 */

            this.TXT01_MCENBUDUAM.SetValue(Convert.ToString(Double.Parse(Get_Numeric(this.TXT01_MCENBUDUAM.GetValue().ToString().Trim())).ToString("#,0.00")));    //13P 2       COLHDG('부두사용료')
            this.TXT01_MCENTOJIAM.SetValue(Convert.ToString(Double.Parse(Get_Numeric(this.TXT01_MCENTOJIAM.GetValue().ToString().Trim())).ToString("#,0.00")));    //13P 2       COLHDG('토지사용료')
            this.TXT01_MCENJILQTY.SetValue(Convert.ToString(Double.Parse(Get_Numeric(this.TXT01_MCENJILQTY.GetValue().ToString().Trim())).ToString("#,0.000")));   //13P 3       COLHDG('질소사용량')
            this.TXT01_MCENJILRAT.SetValue(Convert.ToString(Double.Parse(Get_Numeric(this.TXT01_MCENJILRAT.GetValue().ToString().Trim())).ToString("#,0.000")));   //13P 3       COLHDG('질소　요율')
            this.TXT01_MCENJILAMT.SetValue(Convert.ToString(Double.Parse(Get_Numeric(this.TXT01_MCENJILAMT.GetValue().ToString().Trim())).ToString("#,0.000")));   //13P 3       COLHDG('질소사용료')
            this.TXT01_MCENGAAMT.SetValue(Convert.ToString(Double.Parse(Get_Numeric(this.TXT01_MCENGAAMT.GetValue().ToString().Trim())).ToString("#,0.000")));     //13P 2       COLHDG('가　열　료')
            this.TXT01_MCENCLEAMT.SetValue(Convert.ToString(Double.Parse(Get_Numeric(this.TXT01_MCENCLEAMT.GetValue().ToString().Trim())).ToString("#,0.00")));    //13P 2       COLHDG('세　척　료')
            this.TXT01_MCENVOCAMT.SetValue(Convert.ToString(Double.Parse(Get_Numeric(this.TXT01_MCENVOCAMT.GetValue().ToString().Trim())).ToString("#,0.00")));    //13P 2       COLHDG('Ｖ　Ｏ　Ｃ')
            this.TXT01_MCENLPGAMT.SetValue(Convert.ToString(Double.Parse(Get_Numeric(this.TXT01_MCENLPGAMT.GetValue().ToString().Trim())).ToString("#,0.00")));    //13P 2       COLHDG('LPG')

            #region Description : UTILITY

            if (this.CBO01_MCREQGB.GetValue().ToString() == "2")
            {
                // 외화
                dFieldCompute = (Convert.ToDouble(Get_Numeric(this.TXT01_MCENBUDUAM.GetValue().ToString())) / Convert.ToDouble(Get_Numeric(this.TXT01_MCENRATE.GetValue().ToString())) * 100);
                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));
                dFieldCompute = (dFieldCompute / 100);

                this.TXT01_MCENBUDUDL.SetValue(String.Format("{0:#,0.00}", dFieldCompute));
            }

            if (this.CBO01_MCREQGB.GetValue().ToString() == "2")
            {
                // 외화
                dFieldCompute = (Convert.ToDouble(Get_Numeric(this.TXT01_MCENTOJIAM.GetValue().ToString())) / Convert.ToDouble(Get_Numeric(this.TXT01_MCENRATE.GetValue().ToString())) * 100);
                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));
                dFieldCompute = (dFieldCompute / 100);

                this.TXT01_MCENTOJIDL.SetValue(String.Format("{0:#,0.00}", dFieldCompute));
            }

            if (this.CBO01_MCREQGB.GetValue().ToString() == "2")
            {
                // 외화
                dFieldCompute = (Convert.ToDouble(Get_Numeric(this.TXT01_MCENGAAMT.GetValue().ToString())) / Convert.ToDouble(Get_Numeric(this.TXT01_MCENRATE.GetValue().ToString())) * 100);
                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));
                dFieldCompute = (dFieldCompute / 100);

                this.TXT01_MCENGADAL.SetValue(String.Format("{0:#,0.00}", dFieldCompute));
            }

            if (this.CBO01_MCREQGB.GetValue().ToString() == "2")
            {
                // 외화
                dFieldCompute = (Convert.ToDouble(Get_Numeric(this.TXT01_MCENCLEAMT.GetValue().ToString())) / Convert.ToDouble(Get_Numeric(this.TXT01_MCENRATE.GetValue().ToString())) * 100);
                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));
                dFieldCompute = (dFieldCompute / 100);

                this.TXT01_MCENCLEDAL.SetValue(String.Format("{0:#,0.00}", dFieldCompute));
            }

            if (this.CBO01_MCREQGB.GetValue().ToString() == "2")
            {
                // 외화
                dFieldCompute = (Convert.ToDouble(Get_Numeric(this.TXT01_MCENVOCAMT.GetValue().ToString())) / Convert.ToDouble(Get_Numeric(this.TXT01_MCENRATE.GetValue().ToString())) * 100);
                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));
                dFieldCompute = (dFieldCompute / 100);

                this.TXT01_MCENVOCDAL.SetValue(String.Format("{0:#,0.00}", dFieldCompute));
            }

            if (this.CBO01_MCREQGB.GetValue().ToString() == "2")
            {
                // 외화
                dFieldCompute = (Convert.ToDouble(Get_Numeric(this.TXT01_MCENLPGAMT.GetValue().ToString())) / Convert.ToDouble(Get_Numeric(this.TXT01_MCENRATE.GetValue().ToString())) * 100);
                dFieldCompute = Convert.ToDouble(UP_DotDelete(Convert.ToString(dFieldCompute)));
                dFieldCompute = (dFieldCompute / 100);

                this.TXT01_MCENLPGDAL.SetValue(String.Format("{0:#,0.00}", dFieldCompute));
            }

            #endregion

            // 매출별 총계(원화, 외화)
            UP_COMPUTE_DAL();

            // 저장하시겠습니까?
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 삭제 ProcessCheck
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sMCCONTNO = string.Empty;

            sMCCONTNO = this.TXT01_MCCONTNO1.GetValue().ToString() + "-" + Set_Fill3(this.TXT01_MCCONTNO2.GetValue().ToString());

            DataTable dt = new DataTable();
            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_75BGG454",
                Get_Date(this.DTP01_MCDATE.GetValue().ToString()),
                this.CBH01_MCHWAJU.GetValue().ToString(),
                this.TXT01_MCSEQ.GetValue().ToString(),
                this.CBH01_MCHWAMUL.GetValue().ToString(),
                this.TXT01_MCTANKNO.GetValue().ToString().Trim(),
                sMCCONTNO.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["MCJPNO"].ToString() != "")
                {
                    this.ShowMessage("TY_M_UT_73K9X969");

                    SetFocus(this.TXT01_MCCONTNO1);

                    e.Successed = false;
                    return;
                }
            }

            // 삭제 하시겠습니까?
            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 일괄삭제 ProcessCheck
        private void BTN61_DETAIL_DEL_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_UT_74QI2391.GetDataSourceInclude(TSpread.TActionType.Remove, "MCDATE", "MCHWAJU", "MCSEQ", "MCHWAMUL", "MCTANKNO", "MCCONTNO");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            DataTable dt1 = new DataTable();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_75BGG454",
                    dt.Rows[i]["MCDATE"].ToString(),
                    dt.Rows[i]["MCHWAJU"].ToString(),
                    dt.Rows[i]["MCSEQ"].ToString(),
                    dt.Rows[i]["MCHWAMUL"].ToString(),
                    dt.Rows[i]["MCTANKNO"].ToString().Trim(),
                    dt.Rows[i]["MCCONTNO"].ToString()
                    );

                dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count > 0)
                {
                    if (dt1.Rows[0]["MCJPNO"].ToString() != "")
                    {
                        this.ShowMessage("TY_M_UT_73K9X969");

                        SetFocus(this.TXT01_MCCONTNO1);

                        e.Successed = false;
                        return;
                    }
                }
            }

            // 삭제 하시겠습니까?
            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            // 스프레드 칼럼 데이터 넘겨주는 부분
            e.ArgData = dt;
        }
        #endregion

        #region Description : 스프레드 더블클릭
        private void FPS91_TY_S_UT_74QI2391_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DTP01_MCDATE.SetValue(this.FPS91_TY_S_UT_74QI2391.GetValue("MCDATE").ToString());
            this.CBH01_MCHWAJU.SetValue(this.FPS91_TY_S_UT_74QI2391.GetValue("MCHWAJU").ToString());
            this.TXT01_MCSEQ.SetValue(this.FPS91_TY_S_UT_74QI2391.GetValue("MCSEQ").ToString());
            this.CBH01_MCHWAMUL.SetValue(this.FPS91_TY_S_UT_74QI2391.GetValue("MCHWAMUL").ToString());
            this.TXT01_MCTANKNO.SetValue(this.FPS91_TY_S_UT_74QI2391.GetValue("MCTANKNO").ToString());
            this.TXT01_MCCONTNO1.SetValue(this.FPS91_TY_S_UT_74QI2391.GetValue("MCCONTNO").ToString().Substring(0,4));
            this.TXT01_MCCONTNO2.SetValue(this.FPS91_TY_S_UT_74QI2391.GetValue("MCCONTNO").ToString().Substring(5,3));

            UP_RUN();
        }
        #endregion

        #region Description : 필드 ReadOnly
        private void UP_Field_ReadOnly(bool boolean)
        {
            //this.DTP01_MCDATE.SetReadOnly(boolean);
            //this.DTP01_M1IPHANG.SetReadOnly(boolean);
            //this.CBH01_M1BONSUN.SetReadOnly(boolean);
            //this.CBH01_MCHWAJU.SetReadOnly(boolean);
            //this.CBH01_M1HWAMUL.SetReadOnly(boolean);
        }
        #endregion

        #region Description : 버튼 디스플레이
        private void UP_BTN_DISPLAY(string sGUBUN)
        {
            if (sGUBUN == "NEW")
            {
                this.BTN61_SAV.Visible = true;
                this.BTN61_REM.Visible = false;
            }
            else if (sGUBUN == "UPT")
            {
                this.BTN61_SAV.Visible = true;
                this.BTN61_REM.Visible = true;
            }
            else
            {
                this.BTN61_SAV.Visible = false;
                this.BTN61_REM.Visible = false;
            }
        }
        #endregion

        #region Description : 필드 클리어
        private void UP_Field_Clear()
        {
            this.TXT01_MCENIPAMT.SetValue("0");
            this.TXT01_MCENCHAMT.SetValue("0");
            this.TXT01_MCENISAMT.SetValue("0");
            this.TXT01_MCENCANAMT.SetValue("0");
            this.TXT01_MCENDRAMT.SetValue("0");
            this.TXT01_MCENBOJAMT.SetValue("0");
            this.TXT01_MCENIPOVAM.SetValue("0");
            this.TXT01_MCENCHOVAM.SetValue("0");
            this.TXT01_MCJPNO.SetValue("");

            this.TXT01_MCENIPDAL.SetValue("0");
            this.TXT01_MCENCHDAL.SetValue("0");
            this.TXT01_MCENISDAL.SetValue("0");
            this.TXT01_MCENCANDAL.SetValue("0");
            this.TXT01_MCENDRDAL.SetValue("0");
            this.TXT01_MCENBOJDAL.SetValue("0");
            this.TXT01_MCENIPOVDL.SetValue("0");
            this.TXT01_MCENCHOVDL.SetValue("0");
            this.TXT01_MCENBUDUDL.SetValue("0");
            this.TXT01_MCENTOJIDL.SetValue("0");
            this.TXT01_MCENJILDAL.SetValue("0");
            this.TXT01_MCENGADAL.SetValue("0");
            this.TXT01_MCENCLEDAL.SetValue("0");
            this.TXT01_MCENVOCDAL.SetValue("0");
            this.TXT01_MCENLPGDAL.SetValue("0");
            this.TXT01_CHO_TOT_WON.SetValue("0");
            this.TXT01_CHO_TOT_DAL.SetValue("0");
            this.TXT01_CHOV_TOT_WON.SetValue("0");
            this.TXT01_CHOV_TOT_DAL.SetValue("0");
            this.TXT01_UTI_TOT_WON.SetValue("0");
            this.TXT01_UTI_TOT_DAL.SetValue("0");
            this.TXT01_TOT_WON.SetValue("0");
            this.TXT01_TOT_DAL.SetValue("0");
            this.TXT01_MCENBOAMT_HAP.SetValue("0");
            this.TXT01_MCENBODAL_HAP.SetValue("0");
        }
        #endregion

        #region Description : 날짜 이벤트
        private void DTP01_EDDATE_KeyPress(object sender, KeyPressEventArgs e)
        {
            SetFocus(this.CBH01_SHWAJU.CodeText);
        }
        #endregion

        #region Description : 화물 이벤트
        private void CBH01_M1HWAMUL_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    SetFocus(this.TXT01_MCREQGB);
            //}
        }
        #endregion

        #region Description : 화폐 이벤트
        private void CBH01_M1CURRCD_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == '\r')
            //{
            //    SetFocus(this.TXT01_M1RATE);
            //}
        }
        #endregion

        #region Description : 외화 금액 이벤트
        private void TXT01_M1EDDOLLAR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetFocus(this.BTN61_SAV);
            }
        }
        #endregion

        #region Description : 유틸리티 값 가져오기
        private void BTN61_GET_Click(object sender, EventArgs e)
        {
            string sYEAR   = string.Empty;
            string sMONTH  = string.Empty;
            string sSTDATE = string.Empty;
            string sEDDATE = string.Empty;


            if (Get_Date(this.DTP01_MCDATE.GetValue().ToString()).Substring(4, 2) == "01")
            {
                sYEAR  = Convert.ToString(int.Parse(Get_Date(this.DTP01_MCDATE.GetValue().ToString()).Substring(0, 4)) - 1);
                sMONTH = "12";
            }
            else
            {
                sYEAR  = Get_Date(this.DTP01_MCDATE.GetValue().ToString()).Substring(0, 4);
                sMONTH = Set_Fill2(Convert.ToString(int.Parse(Get_Date(this.DTP01_MCDATE.GetValue().ToString()).Substring(4, 2)) - 1));
            }

            sSTDATE = sYEAR + sMONTH + "26";
            sEDDATE = Get_Date(this.DTP01_MCDATE.GetValue().ToString()).Substring(0, 6) + "25";


            DataTable dt = new DataTable();

            // 탱크 세척료 데이터 가져오기
            this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_UT_75GJ2524", sSTDATE.ToString(),
            //                                            sEDDATE.ToString(),
            //                                            this.CBH01_MCHWAJU.GetValue().ToString(),
            //                                            this.CBH01_MCHWAMUL.GetValue().ToString(),
            //                                            this.TXT01_MCTANKNO.GetValue().ToString().Trim());

            if (int.Parse(this.DTP01_MCDATE.GetString()) >= 20220301)
            {
                this.DbConnector.Attach("TY_P_UT_C2LBW088", Get_Date(this.DTP01_MCDATE.GetValue().ToString()).Substring(0, 6),
                                                            this.CBH01_MCHWAJU.GetValue().ToString(),
                                                            this.CBH01_MCHWAMUL.GetValue().ToString(),
                                                            this.TXT01_MCTANKNO.GetValue().ToString().Trim());
            }
            else
            {
                this.DbConnector.Attach("TY_P_UT_B4S8I269", Get_Date(this.DTP01_MCDATE.GetValue().ToString()).Substring(0, 6),
                                                            this.CBH01_MCHWAJU.GetValue().ToString(),
                                                            this.CBH01_MCHWAMUL.GetValue().ToString(),
                                                            this.TXT01_MCTANKNO.GetValue().ToString().Trim());
            }

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_MCENCLEAMT.SetValue(dt.Rows[0]["CLAMT"].ToString());
            }

            // 가열료 데이터 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75GJ2525", sSTDATE.ToString(),
                                                        sEDDATE.ToString(),
                                                        this.CBH01_MCHWAJU.GetValue().ToString(),
                                                        this.CBH01_MCHWAMUL.GetValue().ToString(),
                                                        this.TXT01_MCTANKNO.GetValue().ToString().Trim());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_MCENGAAMT.SetValue(dt.Rows[0]["GAAMT"].ToString());
            }

            // 질소 데이터 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_75GJ4526", Get_Date(this.DTP01_MCDATE.GetValue().ToString()).Substring(0, 6),
                                                        this.CBH01_MCHWAJU.GetValue().ToString(),
                                                        this.CBH01_MCHWAMUL.GetValue().ToString(),
                                                        this.TXT01_MCTANKNO.GetValue().ToString().Trim());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                // 질소 수량
                this.TXT01_MCENJILQTY.SetValue(dt.Rows[0]["JLQTY"].ToString());

                // 질소 단가
                this.TXT01_MCENJILRAT.SetValue(dt.Rows[0]["JLDANGA"].ToString());

                // 질소 금액
                this.TXT01_MCENJILAMT.SetValue(dt.Rows[0]["JLAMT"].ToString());
            }

        }
        #endregion

        #region Description : 계약번호 이벤트
        private void TXT01_MCCONTNO2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.CBO01_MCREQGB);
            }
        }
        #endregion

        #region Description : VOC 운영비 이벤트
        private void TXT01_MCENVOCAMT_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == '\r')
            //{
            //    if (this.BTN61_SAV.Visible == true)
            //    {
            //        SetFocus(this.BTN61_SAV);
            //    }
            //}
        }
        #endregion

        #region Description : 화주 이벤트
        private void CBH01_SHWAJU_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.BTN61_INQ);
            }

        }

        private void CBH01_SHWAJU_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetFocus(this.BTN61_INQ);
            }
        }
        #endregion

        #region Description : LPG 이벤트
        private void TXT01_MCENLPGAMT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.BTN61_SAV.Visible == true)
                {
                    SetFocus(this.BTN61_SAV);
                }
            }
        }
        #endregion
    }
}