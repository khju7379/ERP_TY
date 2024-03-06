using System;
using System.Data;
using System.Drawing;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;

namespace TY.ER.AC00
{
    /// <summary>
    /// 여비교통비세목관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.06.15 13:05
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_26F2Y897 : 여비교통비 세목예산 조회
    ///  TY_P_AC_26F31898 : 여비교통비 세목 월예산 조회
    ///  TY_P_AC_26F4M901 : 여비교통비 세목예산 등록
    ///  TY_P_AC_26F4O902 : 여비교통비 세목예산 수정
    ///  TY_P_AC_26F4P903 : 여비교통비 세목예산 삭제
    ///  TY_P_AC_26F4Q904 : 여비교통비 세목 월예산 등록
    ///  TY_P_AC_26F4R905 : 여비교통비 세목 월예산 수정
    ///  TY_P_AC_26F4S906 : 여비교통비 세목 월예산 삭제
    ///  TY_P_AC_26I1H913 : 여비교통비 세목예산 순번 부여
    ///  TY_P_AC_26I1Y914 : 여비교통비 세목예산 순번 등록
    ///  TY_P_AC_26I1Z915 : 여비교통비 세목예산 순번 수정
    ///  TY_P_AC_2745Y963 : 본예산 등록 - 여비교통비
    ///  TY_P_AC_2734L950 : 본예산 업데이트 - 여비교통비
    ///  TY_P_AC_27353951 : 본예산 업데이트 - 세목 삭제 또는 월 예산 저장 및 수정시
    ///  TY_P_AC_27354952 : 여비교통비 세목 월예산 등록 및 수정, 본예산 업데이트
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_26F22893 : 여비교통비 세목예산관리 조회
    ///  TY_S_AC_26F20892 : 여비교통비 세목 월예산 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_AC_24C3F612 : 집행금액 존재합니다. 삭제할수 없습니다.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    ///  TY_M_GB_26I24916 : 일자를 확인하세요.
    ///  TY_M_AC_2732T947 : 업무내용을 입력하세요.
    ///  TY_M_AC_2733V948 : 입력하신 계정코드는 등록이 불가한 코드입니다.
    ///  TY_M_AC_2733W949 : 입력하신 부서코드는 등록이 불가한 코드입니다.
    ///  TY_M_AC_24C3F612 : 집행금액 존재합니다. 삭제할수 없습니다.
    ///  TY_M_AC_27477965 : 등록시 본예산 또는 수정예산을 입력하세요.
    ///  TY_M_AC_2C37F812 : 출장 일수를 입력하세요.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  T1CDAC : 계정
    ///  T1CDDP : 팀코드
    ///  T2CDAC : 계정
    ///  T2CDDP : 팀코드
    ///  T2CDSB : 사번
    ///  T1RKAC : 내용
    ///  T1YEAR : 예산년도
    ///  T2SEQ : 순번
    ///  T2YEAR : 예산년도
    /// </summary>
    public partial class TYACLB005I : TYBase
    {
        #region Description : 페이지 로드
        public TYACLB005I()
        {
            InitializeComponent();

            // 스프레드에서 코드헬프 사용
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_26F22893, "T1CDDP", "DTDESC",  "T1CDDP");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_26F22893, "T1CDAC", "A1NMAC",  "T1CDAC");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_26F22893, "T1CDSB", "KBHANGL", "T1CDSB");
        }

        private void TYACLB005I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_26F22893, "T1YEAR");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_26F22893, "T1CDDP");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_26F22893, "DTDESC");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_26F22893, "T1CDAC");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_26F22893, "A1NMAC");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_26F22893, "T1CDSB");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_26F22893, "KBHANGL");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_26F22893, "T1SEQ");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.BTN62_SAV.ProcessCheck += new TButton.CheckHandler(BTN62_SAV_ProcessCheck);

            this.CBH01_T1CDDP.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            UP_SetReadOnly(false);

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.TXT01_T1YEAR);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_26F2Y897",
                this.TXT01_T1YEAR.GetValue().ToString(),
                this.CBH01_T1CDDP.GetValue().ToString(),
                this.CBH01_T1CDAC.GetValue().ToString()
                );

            this.FPS91_TY_S_AC_26F22893.SetValue(this.DbConnector.ExecuteDataTable());

            this.FPS91_TY_S_AC_26F20892.Initialize();

            this.TXT02_T2YEAR.SetValue("");
            this.CBH02_T2CDDP.SetValue("");
            this.CBH02_T2CDAC.SetValue("");
            this.CBH02_T2CDSB.SetValue("");
            this.TXT02_T2SEQ.SetValue("");
            this.TXT01_T1RKAC.SetValue("");
        }
        #endregion

        #region Description : 여비교통비 세목예산 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            int i = 0;
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // 여비교통비 세목코드(APPTCDF) 저장 및 수정
            this.DbConnector.CommandClear();

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_26F4M901", ds.Tables[0].Rows[i]["T1YEAR"].ToString(),
                                                            ds.Tables[0].Rows[i]["T1CDDP"].ToString(),
                                                            ds.Tables[0].Rows[i]["T1CDAC"].ToString(),
                                                            ds.Tables[0].Rows[i]["T1CDSB"].ToString(),
                                                            ds.Tables[0].Rows[i]["T1SEQ"].ToString(),
                                                            ds.Tables[0].Rows[i]["T1RKAC"].ToString(),
                                                            ds.Tables[0].Rows[i]["T1CNT"].ToString()
                                                            //ds.Tables[0].Rows[i]["T1DTST"].ToString(),
                                                            //ds.Tables[0].Rows[i]["T1DTED"].ToString()
                                                            );
            }

            for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_26F4O902", ds.Tables[1].Rows[i]["T1RKAC"].ToString(),
                                                            ds.Tables[1].Rows[i]["T1CNT"].ToString(),
                                                            //ds.Tables[1].Rows[i]["T1DTST"].ToString(),
                                                            //ds.Tables[1].Rows[i]["T1DTED"].ToString(),
                                                            ds.Tables[1].Rows[i]["T1YEAR"].ToString(),
                                                            ds.Tables[1].Rows[i]["T1CDDP"].ToString(),
                                                            ds.Tables[1].Rows[i]["T1CDAC"].ToString(),
                                                            ds.Tables[1].Rows[i]["T1CDSB"].ToString(),
                                                            ds.Tables[1].Rows[i]["T1SEQ"].ToString()
                                                            );
            }

            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");
            this.BTN61_INQ_Click(null, null);

            this.FPS91_TY_S_AC_26F20892.Initialize();

            //this.TXT02_T2YEAR.SetValue(ds.Tables[0].Rows[0]["T1YEAR"].ToString());
            //this.CBH02_T2CDDP.SetValue(ds.Tables[0].Rows[0]["T1CDDP"].ToString());
            //this.CBH02_T2CDAC.SetValue(ds.Tables[0].Rows[0]["T1CDAC"].ToString());
            //this.CBH02_T2CDSB.SetValue(ds.Tables[0].Rows[0]["T1CDSB"].ToString());
            //this.TXT02_T2SEQ.SetValue(ds.Tables[0].Rows[0]["T1SEQ"].ToString());
            //this.TXT01_T1RKAC.SetValue(ds.Tables[0].Rows[0]["T1RKAC"].ToString());

            //UP_SetReadOnly(true);

            //UP_SetWoldMaster();
        }
        #endregion

        #region Description : 여비교통비 세목예산 및 월예산 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            string sMMHIGB = string.Empty;
            int i = 0;

            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();

            // 본예산 업데이트
            for (i = 0; i < dt.Rows.Count; i++)
            {
                sMMHIGB = "";

                if (dt.Rows[i]["T1CDDP"].ToString().Substring(0, 2) == "T1" || dt.Rows[i]["T1CDDP"].ToString().Substring(0, 2) == "S1")
                {
                    sMMHIGB = "U";
                }

                // SP 호출
                this.DbConnector.Attach
                (
                "TY_P_AC_27353951",
                dt.Rows[i]["T1YEAR"].ToString(),
                dt.Rows[i]["T1CDDP"].ToString(),
                dt.Rows[i]["T1CDAC"].ToString(),
                dt.Rows[i]["T1CDSB"].ToString(),
                dt.Rows[i]["T1SEQ"].ToString(),
                sMMHIGB.ToString()
                );

                // 여비교통비 세목예산
                this.DbConnector.Attach("TY_P_AC_26F4P903", dt.Rows[i]["T1YEAR"].ToString(),
                                                            dt.Rows[i]["T1CDDP"].ToString(),
                                                            dt.Rows[i]["T1CDAC"].ToString(),
                                                            dt.Rows[i]["T1CDSB"].ToString(),
                                                            dt.Rows[i]["T1SEQ"].ToString());

                // 여비교통비 세목 월예산
                this.DbConnector.Attach("TY_P_AC_26F4S906", dt.Rows[i]["T1YEAR"].ToString(),
                                                            dt.Rows[i]["T1CDDP"].ToString(),
                                                            dt.Rows[i]["T1CDAC"].ToString(),
                                                            dt.Rows[i]["T1CDSB"].ToString(),
                                                            dt.Rows[i]["T1SEQ"].ToString());
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지
            this.BTN61_INQ_Click(null, null);

            this.FPS91_TY_S_AC_26F20892.Initialize();

            this.TXT02_T2YEAR.SetValue("");
            this.CBH02_T2CDDP.SetValue("");
            this.CBH02_T2CDAC.SetValue("");
            this.CBH02_T2CDSB.SetValue("");
            this.TXT02_T2SEQ.SetValue("");
            this.TXT01_T1RKAC.SetValue("");

            // 원본 소스
            //string sGUBUN  = string.Empty;
            //string sMMHIGB = string.Empty;

            //double dMMCRAMT = 0;
            //double dMMPLAMT = 0;

            //double dT2CRAMT = 0;
            //double dT2PLAMT = 0;

            //int i = 0;
            //int j = 1;

            //DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            //// 본예산 업데이트
            //for (i = 0; i < dt.Rows.Count; i++)
            //{
            //    for (j = 1; j < 13; j++)
            //    {
            //        sGUBUN  = "";
            //        sMMHIGB = "";

            //        dMMCRAMT = 0;
            //        dMMPLAMT = 0;

            //        dT2CRAMT = 0;
            //        dT2PLAMT = 0;

            //        this.DbConnector.CommandClear();

            //        this.DbConnector.Attach
            //            (
            //            "TY_P_AC_27353951",
            //            dt.Rows[i]["T1YEAR"].ToString(),
            //            j.ToString("00"),
            //            dt.Rows[i]["T1CDDP"].ToString(),
            //            dt.Rows[i]["T1CDAC"].ToString()
            //            );

            //        DataTable dz = this.DbConnector.ExecuteDataTable();

            //        if (dz.Rows.Count > 0)
            //        {
            //            dMMCRAMT = double.Parse(dz.Rows[0]["MMCRAMT"].ToString());
            //            dMMPLAMT = double.Parse(dz.Rows[0]["MMPLAMT"].ToString());
            //        }
            //        else
            //        {
            //            sGUBUN = "INS";
            //        }

            //        this.DbConnector.CommandClear();

            //        this.DbConnector.Attach
            //            (
            //            "TY_P_AC_27354952",
            //            dt.Rows[i]["T1YEAR"].ToString(),
            //            dt.Rows[i]["T1CDDP"].ToString(),
            //            dt.Rows[i]["T1CDAC"].ToString(),
            //            dt.Rows[i]["T1CDSB"].ToString(),
            //            dt.Rows[i]["T1SEQ"].ToString(),
            //            j.ToString("00")
            //            );

            //        DataTable dy = this.DbConnector.ExecuteDataTable();

            //        if (dy.Rows.Count > 0)
            //        {
            //            // 본예산_OLD
            //            dT2CRAMT = double.Parse(dy.Rows[0]["T2CRAMT"].ToString());
            //            // 수정예산_OLD
            //            dT2PLAMT = double.Parse(dy.Rows[0]["T2PLAMT"].ToString());
            //        }

            //        if (dt.Rows[i]["T1CDDP"].ToString().Substring(0, 2) == "T1" || dt.Rows[i]["T1CDDP"].ToString().Substring(0, 2) == "S1")
            //        {
            //            sMMHIGB = "U";
            //        }

            //        this.DbConnector.CommandClear();

            //        // 본예산 업데이트
            //        this.DbConnector.Attach("TY_P_AC_2734L950", Convert.ToString(dMMCRAMT - dT2CRAMT),
            //                                                    Convert.ToString(dMMPLAMT - dT2PLAMT),
            //                                                    sMMHIGB.ToString(),
            //                                                    dt.Rows[i]["T1YEAR"].ToString(),
            //                                                    j.ToString("00"),
            //                                                    dt.Rows[i]["T1CDDP"].ToString(),
            //                                                    dt.Rows[i]["T1CDAC"].ToString()
            //                                                    );

            //        this.DbConnector.ExecuteNonQuery();
            //    }
            //}

            //this.DbConnector.CommandClear();
            //// 여비교통비 세목예산
            //this.DbConnector.Attach("TY_P_AC_26F4P903", dt);

            //// 여비교통비 세목 월예산
            //this.DbConnector.Attach("TY_P_AC_26F4S906", dt);

            //this.DbConnector.ExecuteTranQueryList();

            //this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지
            //this.BTN61_INQ_Click(null, null);

            //this.FPS91_TY_S_AC_26F20892.Initialize();

            //this.TXT02_T2YEAR.SetValue("");
            //this.CBH02_T2CDDP.SetValue("");
            //this.CBH02_T2CDAC.SetValue("");
            //this.CBH02_T2CDSB.SetValue("");
            //this.TXT02_T2SEQ.SetValue("");
            //this.TXT01_T1RKAC.SetValue("");
        }
        #endregion

        #region Description : 여비교통비 세목 월예산 저장 버튼
        private void BTN62_SAV_Click(object sender, EventArgs e)
        {
            string sMMHIGB = string.Empty;
            string sT2CDDP = string.Empty;

            double dT2CRAMT = 0;
            double dT2PLAMT = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            for (int i = 0; i < 12; i++)
            {
                dT2CRAMT = double.Parse(Get_Numeric(this.FPS91_TY_S_AC_26F20892.GetValue(i, "T2CRAMT").ToString()));
                dT2PLAMT = double.Parse(Get_Numeric(this.FPS91_TY_S_AC_26F20892.GetValue(i, "T2PLAMT").ToString()));

                sT2CDDP = this.CBH02_T2CDDP.GetValue().ToString().Substring(0, 2);

                if (sT2CDDP.ToString() == "T1" || sT2CDDP.ToString() == "S1" )
                {
                    sMMHIGB = "U";
                }

                this.DbConnector.Attach
                    (
                    "TY_P_AC_27354952",
                    this.TXT02_T2YEAR.GetValue().ToString(),
                    this.CBH02_T2CDDP.GetValue().ToString(),
                    this.CBH02_T2CDAC.GetValue().ToString(),
                    this.CBH02_T2CDSB.GetValue().ToString(),
                    this.TXT02_T2SEQ.GetValue().ToString(),
                    (i + 1).ToString("00"),
                    dT2CRAMT,
                    dT2PLAMT,
                    sMMHIGB.ToString(),
                    TYUserInfo.EmpNo
                    );

                // 원본 소스
                //this.DbConnector.CommandClear();

                //this.DbConnector.Attach
                //    (
                //    "TY_P_AC_27353951",
                //    this.TXT02_T2YEAR.GetValue().ToString(),
                //    (i+1).ToString("00"),
                //    this.CBH02_T2CDDP.GetValue().ToString(),
                //    this.CBH02_T2CDAC.GetValue().ToString()
                //    );

                //DataTable dz = this.DbConnector.ExecuteDataTable();

                //if (dz.Rows.Count > 0)
                //{
                //    dMMCRAMT = double.Parse(dz.Rows[0]["MMCRAMT"].ToString());
                //    dMMPLAMT = double.Parse(dz.Rows[0]["MMPLAMT"].ToString());
                //}

                //this.DbConnector.CommandClear();

                //this.DbConnector.Attach
                //    (
                //    "TY_P_AC_27354952",
                //    this.TXT02_T2YEAR.GetValue().ToString(),
                //    this.CBH02_T2CDDP.GetValue().ToString(),
                //    this.CBH02_T2CDAC.GetValue().ToString(),
                //    this.CBH02_T2CDSB.GetValue().ToString(),
                //    this.TXT02_T2SEQ.GetValue().ToString(),
                //    (i + 1).ToString("00")
                //    );

                //DataTable dy = this.DbConnector.ExecuteDataTable();

                //if (dy.Rows.Count > 0)
                //{
                //    // 본예산_OLD
                //    dOLDT2CRAMT = double.Parse(dy.Rows[0]["T2CRAMT"].ToString());
                //    // 수정예산_OLD
                //    dOLDT2PLAMT = double.Parse(dy.Rows[0]["T2PLAMT"].ToString());
                //}

                //dT2CRAMT = double.Parse(Get_Numeric(this.FPS91_TY_S_AC_26F20892.GetValue(i, "T2CRAMT").ToString()));
                //dT2PLAMT = double.Parse(Get_Numeric(this.FPS91_TY_S_AC_26F20892.GetValue(i, "T2PLAMT").ToString()));

                //sT2CDDP = this.CBH02_T2CDDP.GetValue().ToString().Substring(0, 2);

                //if (sT2CDDP.ToString() == "T1" || sT2CDDP.ToString() == "S1")
                //{
                //    sMMHIGB = "U";
                //}

                //this.DbConnector.CommandClear();

                //// 본예산 등록
                //this.DbConnector.Attach("TY_P_AC_2745Y963", this.TXT02_T2YEAR.GetValue().ToString(),
                //                                            (i + 1).ToString("00"),
                //                                            this.CBH02_T2CDDP.GetValue().ToString(),
                //                                            this.CBH02_T2CDAC.GetValue().ToString(),
                //                                            Get_Numeric(this.FPS91_TY_S_AC_26F20892.GetValue(i, "T2CRAMT").ToString()),
                //                                            Get_Numeric(this.FPS91_TY_S_AC_26F20892.GetValue(i, "T2PLAMT").ToString()),
                //                                            TYUserInfo.UserID,
                //                                            this.TXT02_T2YEAR.GetValue().ToString(),
                //                                            (i + 1).ToString("00"),
                //                                            this.CBH02_T2CDDP.GetValue().ToString(),
                //                                            this.CBH02_T2CDAC.GetValue().ToString());
                //// 본예산 수정
                //this.DbConnector.Attach("TY_P_AC_2734L950", Convert.ToString(dMMCRAMT - dOLDT2CRAMT + dT2CRAMT),
                //                                            Convert.ToString(dMMPLAMT - dOLDT2PLAMT + dT2PLAMT),
                //                                            "C",
                //                                            this.TXT02_T2YEAR.GetValue().ToString(),
                //                                            (i + 1).ToString("00"),
                //                                            this.CBH02_T2CDDP.GetValue().ToString(),
                //                                            this.CBH02_T2CDAC.GetValue().ToString());

                //this.DbConnector.ExecuteTranQueryList();

                //this.DbConnector.CommandClear();

                //// 월 예산 등록 및 수정
                //if (this.FPS91_TY_S_AC_26F20892.GetValue(i, "T2FLAG").ToString() == "A")
                //{
                //    this.DbConnector.Attach("TY_P_AC_26F4Q904", this.TXT02_T2YEAR.GetValue().ToString(),
                //                                                this.CBH02_T2CDDP.GetValue().ToString(),
                //                                                this.CBH02_T2CDAC.GetValue().ToString(),
                //                                                this.CBH02_T2CDSB.GetValue().ToString(),
                //                                                this.TXT02_T2SEQ.GetValue().ToString(),
                //                                                (i + 1).ToString("00"),
                //                                                Get_Numeric(this.FPS91_TY_S_AC_26F20892.GetValue(i, "T2CRAMT").ToString()),
                //                                                Get_Numeric(this.FPS91_TY_S_AC_26F20892.GetValue(i, "T2PLAMT").ToString())); // 저장(월예산관리)
                //}
                //else
                //{
                //    this.DbConnector.Attach("TY_P_AC_26F4R905", Get_Numeric(this.FPS91_TY_S_AC_26F20892.GetValue(i, "T2CRAMT").ToString()),
                //                                                Get_Numeric(this.FPS91_TY_S_AC_26F20892.GetValue(i, "T2PLAMT").ToString()),
                //                                                this.TXT02_T2YEAR.GetValue().ToString(),
                //                                                this.CBH02_T2CDDP.GetValue().ToString(),
                //                                                this.CBH02_T2CDAC.GetValue().ToString(),
                //                                                this.CBH02_T2CDSB.GetValue().ToString(),
                //                                                this.TXT02_T2SEQ.GetValue().ToString(),
                //                                                (i + 1).ToString("00")
                //                                                ); // 수정(월예산관리)
                //}

                //this.DbConnector.ExecuteNonQuery();
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");
            this.BTN61_INQ_Click(null, null);

            this.TXT02_T2YEAR.SetValue("");
            this.CBH02_T2CDDP.SetValue("");
            this.CBH02_T2CDAC.SetValue("");
            this.CBH02_T2CDSB.SetValue("");
            this.TXT02_T2SEQ.SetValue("");
            this.TXT01_T1RKAC.SetValue("");
        }
        #endregion

        #region Description : 여비교통비 세목예산 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;
            int j = 0;
            string sFrom = string.Empty;
            string sTo   = string.Empty;

            DataSet ds = new DataSet();

            // 등록 및 수정을 동시에 할 경우
            // 등록이 먼저이면 Tables[0]
            // 수정이 나중이면 Tables[1]임
            // 저장
            ds.Tables.Add(this.FPS91_TY_S_AC_26F22893.GetDataSourceInclude(TSpread.TActionType.New, "T1YEAR", "T1CDDP", "T1CDAC", "T1CDSB", "T1SEQ", "T1RKAC", "T1CNT"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_AC_26F22893.GetDataSourceInclude(TSpread.TActionType.Update, "T1YEAR", "T1CDDP", "T1CDAC", "T1CDSB", "T1SEQ", "T1RKAC", "T1CNT"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            // 신규 순번 부여
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // 순번 부여
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_26I1H913", ds.Tables[0].Rows[i]["T1YEAR"].ToString(),
                                                            ds.Tables[0].Rows[i]["T1CDDP"].ToString(),
                                                            ds.Tables[0].Rows[i]["T1CDAC"].ToString(),
                                                            ds.Tables[0].Rows[i]["T1CDSB"].ToString());

                ds.Tables[0].Rows[i]["T1SEQ"] = this.DbConnector.ExecuteScalar();

                if (Convert.ToInt16(Get_Numeric(ds.Tables[0].Rows[i]["T1SEQ"].ToString())) <= 1)
                {
                    //순번 입력
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_26I1Y914", ds.Tables[0].Rows[i]["T1YEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["T1CDDP"].ToString(),
                                                                ds.Tables[0].Rows[i]["T1CDAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["T1CDSB"].ToString(),
                                                                ds.Tables[0].Rows[i]["T1SEQ"].ToString()
                                                                );
                    this.DbConnector.ExecuteTranQuery();
                }
                else
                {
                    //순번 수정
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_26I1Z915", ds.Tables[0].Rows[i]["T1SEQ"].ToString(),
                                                                ds.Tables[0].Rows[i]["T1YEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["T1CDDP"].ToString(),
                                                                ds.Tables[0].Rows[i]["T1CDAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["T1CDSB"].ToString());
                    this.DbConnector.ExecuteTranQuery();
                }

                if (int.Parse(Get_Numeric(ds.Tables[0].Rows[i]["T1CNT"].ToString())) == 0)
                {
                    this.ShowMessage("TY_M_AC_2C37F812");
                    e.Successed = false;
                    return;
                }


                //if (ds.Tables[0].Rows[i]["T1DTST"].ToString() == "" && ds.Tables[0].Rows[i]["T1DTED"].ToString() == "")
                //{
                //    ds.Tables[0].Rows[i]["T1CNT"] = "0";
                //}
                //else
                //{
                //    if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["T1DTST"].ToString())) > double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["T1DTED"].ToString())))
                //    {
                //        this.ShowMessage("TY_M_GB_26I24916");
                //        e.Successed = false;
                //        return;
                //    }
                //    else
                //    {
                //        sFrom = ds.Tables[0].Rows[i]["T1DTST"].ToString();
                //        sTo   = ds.Tables[0].Rows[i]["T1DTED"].ToString();

                //        DateTime dt1 = new DateTime(int.Parse(sFrom.Substring(0, 4)), int.Parse(sFrom.Substring(4, 2)), int.Parse(sFrom.Substring(6, 2)));
                //        DateTime dt2 = new DateTime(int.Parse(sTo.Substring(0, 4)), int.Parse(sTo.Substring(4, 2)), int.Parse(sTo.Substring(6, 2)));
                //        TimeSpan dt3 = dt2 - dt1;

                //        ds.Tables[0].Rows[i]["T1CNT"] = dt3.Days.ToString();
                //    }
                //}

                if (ds.Tables[0].Rows[i]["T1RKAC"].ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_2732T947");
                    e.Successed = false;
                    return;
                }

                if (ds.Tables[0].Rows[i]["T1CDDP"].ToString() != "A10000" &&
                    ds.Tables[0].Rows[i]["T1CDDP"].ToString() != "A50000" &&
                    ds.Tables[0].Rows[i]["T1CDDP"].ToString() != "A80000" &&
                    ds.Tables[0].Rows[i]["T1CDDP"].ToString() != "A90000" &&
                    ds.Tables[0].Rows[i]["T1CDDP"].ToString() != "T10000" &&
                    ds.Tables[0].Rows[i]["T1CDDP"].ToString() != "T40000" &&
                    ds.Tables[0].Rows[i]["T1CDDP"].ToString() != "S10000" &&
                    ds.Tables[0].Rows[i]["T1CDDP"].ToString() != "S30200" &&
                    ds.Tables[0].Rows[i]["T1CDDP"].ToString() != "S40000" &&
                    ds.Tables[0].Rows[i]["T1CDDP"].ToString() != "C10000" &&
                    ds.Tables[0].Rows[i]["T1CDDP"].ToString() != "G10000" &&
                    ds.Tables[0].Rows[i]["T1CDDP"].ToString() != "O30000" &&
                    ds.Tables[0].Rows[i]["T1CDDP"].ToString() != "O40000" &&
                    ds.Tables[0].Rows[i]["T1CDDP"].ToString() != "O50000" &&
                    ds.Tables[0].Rows[i]["T1CDDP"].ToString() != "O60000" &&
                    ds.Tables[0].Rows[i]["T1CDDP"].ToString() != "B10000" &&
                    ds.Tables[0].Rows[i]["T1CDDP"].ToString() != "B20000" &&
                    ds.Tables[0].Rows[i]["T1CDDP"].ToString() != "B30000" &&
                    ds.Tables[0].Rows[i]["T1CDDP"].ToString() != "B40000" &&
                    ds.Tables[0].Rows[i]["T1CDDP"].ToString() != "B50000" &&
                    ds.Tables[0].Rows[i]["T1CDDP"].ToString() != "B60000" &&
                    ds.Tables[0].Rows[i]["T1CDDP"].ToString() != "B60100" &&
                    ds.Tables[0].Rows[i]["T1CDDP"].ToString() != "B70000" &&
                    ds.Tables[0].Rows[i]["T1CDDP"].ToString() != "B70100" &&
                    ds.Tables[0].Rows[i]["T1CDDP"].ToString() != "B10100" &&
                    ds.Tables[0].Rows[i]["T1CDDP"].ToString() != "B10200" &&
                    ds.Tables[0].Rows[i]["T1CDDP"].ToString() != "B20100" &&
                    ds.Tables[0].Rows[i]["T1CDDP"].ToString() != "B50100" &&
                    ds.Tables[0].Rows[i]["T1CDDP"].ToString() != "B80100" &&
                    ds.Tables[0].Rows[i]["T1CDDP"].ToString() != "B80200" &&
                    ds.Tables[0].Rows[i]["T1CDDP"].ToString() != "B80300"
                    )
                {
                    this.ShowMessage("TY_M_AC_2733W949");
                    e.Successed = false;
                    return;
                }

                if (ds.Tables[0].Rows[i]["T1CDAC"].ToString() != "42411201" &&
                    ds.Tables[0].Rows[i]["T1CDAC"].ToString() != "42411202" &&
                    ds.Tables[0].Rows[i]["T1CDAC"].ToString() != "44121201" &&
                    ds.Tables[0].Rows[i]["T1CDAC"].ToString() != "44121202" &&
                    ds.Tables[0].Rows[i]["T1CDAC"].ToString() != "44130000" &&
                    ds.Tables[0].Rows[i]["T1CDAC"].ToString() != "44111201" &&
                    ds.Tables[0].Rows[i]["T1CDAC"].ToString() != "44111202" &&
                    ds.Tables[0].Rows[i]["T1CDAC"].ToString() != "44211201" &&
                    ds.Tables[0].Rows[i]["T1CDAC"].ToString() != "44211202"
                    )
                {
                    this.ShowMessage("TY_M_AC_2733V948");
                    e.Successed = false;
                    return;
                }
            }

            //// 스프레드에서 수정 할 항목들
            //ds.Tables.Add(this.FPS91_TY_S_AC_26F22893.GetDataSourceInclude(TSpread.TActionType.Update, "T1YEAR", "T1CDDP", "T1CDAC", "T1CDSB", "T1SEQ", "T1RKAC", "T1CNT", "T1DTST", "T1DTED"));

            for (j = 0; j < ds.Tables[1].Rows.Count; j++)
            {
                //if (ds.Tables[1].Rows[j]["T1DTST"].ToString() == "" && ds.Tables[1].Rows[j]["T1DTED"].ToString() == "")
                //{
                //    ds.Tables[1].Rows[j]["T1CNT"] = "0";
                //}
                //else
                //{
                //    if (double.Parse(Get_Numeric(ds.Tables[1].Rows[j]["T1DTST"].ToString())) > double.Parse(Get_Numeric(ds.Tables[1].Rows[j]["T1DTED"].ToString())))
                //    {
                //        this.ShowMessage("TY_M_GB_26I24916");
                //        e.Successed = false;
                //        return;
                //    }
                //    else
                //    {
                //        sFrom = ds.Tables[1].Rows[j]["T1DTST"].ToString();
                //        sTo   = ds.Tables[1].Rows[j]["T1DTED"].ToString();

                //        DateTime dt5 = new DateTime(int.Parse(sFrom.Substring(0, 4)), int.Parse(sFrom.Substring(4, 2)), int.Parse(sFrom.Substring(6, 2)));
                //        DateTime dt6 = new DateTime(int.Parse(sTo.Substring(0, 4)), int.Parse(sTo.Substring(4, 2)), int.Parse(sTo.Substring(6, 2)));
                //        TimeSpan dt7 = dt6 - dt5;

                //        ds.Tables[1].Rows[j]["T1CNT"] = dt7.Days.ToString();
                //    }
                //}

                if (int.Parse(Get_Numeric(ds.Tables[1].Rows[j]["T1CNT"].ToString())) == 0)
                {
                    this.ShowMessage("TY_M_AC_2C37F812");
                    e.Successed = false;
                    return;
                }

                if (ds.Tables[1].Rows[j]["T1RKAC"].ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_2732T947");
                    e.Successed = false;
                    return;
                }
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 여비교통비 세목예산 및 월예산 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            double dWkAMT = 0;

            DataTable dt = this.FPS91_TY_S_AC_26F22893.GetDataSourceInclude(TSpread.TActionType.Remove, "T1YEAR", "T1CDDP", "T1CDAC", "T1CDSB", "T1SEQ", "T2USAMT");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            //집행금액 체크
            //집행금액이 있으면 삭제안됨
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                dWkAMT = Convert.ToDouble(dt.Rows[j]["T2USAMT"].ToString());

                if (dWkAMT > 0)
                {
                    this.ShowMessage("TY_M_AC_24C3F612");
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region Description : 여비교통비 세목 월예산 저장 ProcessCheck 이벤트
        private void BTN62_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_26F20892.GetDataSourceInclude(TSpread.TActionType.Update, "T2YEAR", "T2CDDP", "T2CDAC", "T2CDSB", "T2SEQ", "T2MONTH", "T2CRAMT", "T2PLAMT", "T2FLAG"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }
            else
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["T2CRAMT"].ToString() == "" && ds.Tables[0].Rows[i]["T2PLAMT"].ToString() == "")
                    {
                        this.ShowMessage("TY_M_AC_27477965");
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 사용자 정의 함수
        private void UP_SetReadOnly(bool bTrueAndFalse)
        {
            this.TXT02_T2YEAR.SetReadOnly(bTrueAndFalse);
            this.CBH02_T2CDDP.SetReadOnly(bTrueAndFalse);
            this.CBH02_T2CDAC.SetReadOnly(bTrueAndFalse);
            this.CBH02_T2CDSB.SetReadOnly(bTrueAndFalse);
            this.TXT02_T2SEQ.SetReadOnly(bTrueAndFalse);
            this.TXT01_T1RKAC.SetReadOnly(bTrueAndFalse);
        }
        #endregion

        #region Description : 예산년도 이벤트
        private void TXT01_T1YEAR_TextChanged(object sender, EventArgs e)
        {
            if (TXT01_T1YEAR.GetValue().ToString() != "")
            {
                this.CBH01_T1CDDP.DummyValue = TXT01_T1YEAR.GetValue() + "0101";
            }
            else
            {
                this.CBH01_T1CDDP.DummyValue = DateTime.Now.ToString("yyyyMMdd");
            }
        }
        #endregion

        #region Description : 여비교통비 세목예산 스프레드 이벤트
        private void FPS91_TY_S_AC_26F22893_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            if (e.Column != 1)
                return;

            // 부서명을 가져오기 위해서 스프레드의 예산년도에 파라미터 날짜를 넣음.
            string year = FPS91_TY_S_AC_26F22893.GetValue(e.Row, "T1YEAR").ToString() + "0101";
            //((TCodeBoxCellType)FPS91_TY_S_AC_24917510.ActiveSheet.Columns["P1CDDP"].CellType).DummyValue = year;
            TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_26F22893, "T1CDDP");
            if (tyCodeBox != null)
                tyCodeBox.DummyValue = year;
        }
        #endregion

        #region Description : 여비교통비 세목예산 스프레드 클릭 이벤트
        private void FPS91_TY_S_AC_26F22893_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.TXT02_T2YEAR.SetValue(this.FPS91_TY_S_AC_26F22893.GetValue("T1YEAR").ToString());
            this.CBH02_T2CDDP.DummyValue = this.TXT02_T2YEAR.GetValue().ToString() + "0101";
            this.CBH02_T2CDDP.SetValue(this.FPS91_TY_S_AC_26F22893.GetValue("T1CDDP").ToString());
            this.CBH02_T2CDAC.SetValue(this.FPS91_TY_S_AC_26F22893.GetValue("T1CDAC").ToString());
            this.CBH02_T2CDSB.SetValue(this.FPS91_TY_S_AC_26F22893.GetValue("T1CDSB").ToString());
            this.TXT02_T2SEQ.SetValue(this.FPS91_TY_S_AC_26F22893.GetValue("T1SEQ").ToString());
            this.TXT01_T1RKAC.SetValue(this.FPS91_TY_S_AC_26F22893.GetValue("T1RKAC").ToString());

            UP_SetReadOnly(true);

            UP_SetWoldMaster();
        }
        #endregion

        #region Description : 여비교통비 세목여비교통비 세목 월예산 조회
        private void UP_SetWoldMaster()
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_26F31898", 
                this.TXT02_T2YEAR.GetValue(),
                this.CBH02_T2CDDP.GetValue(),
                this.CBH02_T2CDAC.GetValue(),
                this.CBH02_T2CDSB.GetValue(),
                this.TXT02_T2SEQ.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "02");

                if (dt.Rows.Count > 0)
                {
                    this.FPS91_TY_S_AC_26F20892.SetValue(UP_MonRowAdd(dt));
                }
                else
                {
                    this.FPS91_TY_S_AC_26F20892.SetValue(UP_NewRowAdd(dt));

                    for (int i = 0; i < this.FPS91_TY_S_AC_26F20892.CurrentRowCount; i++)
                    {
                        this.FPS91_TY_S_AC_26F20892.ActiveSheet.RowHeader.Cells[i, 0].Text = "U";
                    }
                }
            }
            else
            {
                this.FPS91_TY_S_AC_26F20892.SetValue(UP_NewRowAdd(dt));

                for (int i = 0; i < this.FPS91_TY_S_AC_26F20892.CurrentRowCount; i++)
                {
                    this.FPS91_TY_S_AC_26F20892.ActiveSheet.RowHeader.Cells[i, 0].Text = "U";
                }
            }

            UP_SumRowAdd();

            // 마지막 ROW 잠금
            this.FPS91_TY_S_AC_26F20892.ActiveSheet.Rows[this.FPS91_TY_S_AC_26F20892.ActiveSheet.Rows.Count - 1].Locked = true;
        }
        #endregion

        #region Description : 여비교통비 세목여비교통비 세목 월예산 합계
        private DataTable UP_MonRowAdd(DataTable dt)
        {
            DataTable Rowdt = new DataTable();
            DataRow rw;

            Rowdt = dt.Clone();

            for (int i = 0; i < 12; i++)
            {
                rw = Rowdt.NewRow();
                rw["T2YEAR"]  = this.TXT02_T2YEAR.GetValue();
                rw["T2CDDP"]  = this.CBH02_T2CDDP.GetValue();
                rw["T2CDAC"]  = this.CBH02_T2CDAC.GetValue();
                rw["T2CDSB"]  = this.CBH02_T2CDSB.GetValue();
                rw["T2SEQ"]   = this.TXT02_T2SEQ.GetValue();
                rw["T2MONTH"] = (i + 1).ToString("00");
                rw["T2CRAMT"] = dt.Rows[i]["T2CRAMT"].ToString();
                rw["T2PLAMT"] = dt.Rows[i]["T2PLAMT"].ToString();
                rw["T2USAMT"] = dt.Rows[i]["T2USAMT"].ToString();
                rw["T2JAMT"]  = dt.Rows[i]["T2JAMT"].ToString();
                rw["T2FLAG"]  = "C";
                
                Rowdt.Rows.Add(rw);
            }

            return Rowdt;
        }

        private DataTable UP_NewRowAdd(DataTable dt)
        {
            DataTable Rowdt = new DataTable();
            DataRow rw;
            Rowdt = dt.Clone();
            for (int i = 1; i < 13; i++)
            {
                rw = Rowdt.NewRow();
                rw["T2YEAR"]  = this.TXT02_T2YEAR.GetValue();
                rw["T2CDDP"]  = this.CBH02_T2CDDP.GetValue();
                rw["T2CDAC"]  = this.CBH02_T2CDAC.GetValue();
                rw["T2CDSB"]  = this.CBH02_T2CDSB.GetValue();
                rw["T2SEQ"]   = this.TXT02_T2SEQ.GetValue();
                rw["T2MONTH"] = i.ToString("00");
                rw["T2CRAMT"] = 0;
                rw["T2PLAMT"] = 0;
                rw["T2USAMT"] = 0;
                rw["T2JAMT"]  = 0;
                rw["T2FLAG"] = "A";
                Rowdt.Rows.Add(rw);
            }
            return Rowdt;
        }

        private void UP_SumRowAdd()
        {
            this.SpreadSumRowAdd(this.FPS91_TY_S_AC_26F20892, "T2MONTH", "합 계", Color.Yellow);
            this.FPS91_TY_S_AC_26F20892_Sheet1.SetFormula(
                FPS91_TY_S_AC_26F20892_Sheet1.RowCount - 1,
                FPS91_TY_S_AC_26F20892_Sheet1.ColumnCount - 3,
                " R[0]C[-3] + R[0]C[-2] - R[0]C[-1]"); //잔액 구하기        
        }
        #endregion
    }
}