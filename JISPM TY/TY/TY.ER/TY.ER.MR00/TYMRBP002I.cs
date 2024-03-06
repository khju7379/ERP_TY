using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.MR00
{
    /// <summary>
    /// 비품이동 관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.12.11 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_2CB7L079 : 비품 이동 관리 - 비품 마스터 조회
    ///  TY_P_MR_2CB82082 : 비품 이동 조회
    ///  TY_P_MR_2CBC0055 : 비품 이동 등록
    ///  TY_P_MR_2CBC1056 : 비품 이동 수정
    ///  TY_P_MR_2CBC2057 : 비품 이동 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_2CB84083 : 비품 이동 조회
    ///  TY_S_MR_2CB94087 : 비품 이동 관리 - 비품 마스터 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    ///  TY_M_MR_2CB90088 : 매각 금액을 입력하세요.
    ///  TY_M_MR_2CB90089 : 매각 업체를 입력하세요.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  MABPCODE : 자산분류코드
    ///  MABUSEO : 관리부서
    ///  MAFRBUSEO : 사용부서
    ///  MAFRWICHI : 설치위치
    ///  MOBUSEO : 관리부서
    ///  MOCHBUSEO : 변경부서
    ///  MOCHCODE : 변경코드
    ///  MOCHWICHI : 변경위치
    ///  MOVEND : 매각업체
    ///  MABPDESC : 비품명
    ///  MASEQ : 순번
    ///  MAYYMM : 관리년월
    /// </summary>
    public partial class TYMRBP002I : TYBase
    {
        #region Description : 페이지 로드
        public TYMRBP002I()
        {
            InitializeComponent();

            // 스프레드에서 코드헬프 사용
            this.SetSpreadCodeHelper(this.FPS91_TY_S_MR_2CB84083, "MOCHCODE", "REDESC", "MOCHCODE");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_MR_2CB84083, "MOCHBUSEO", "CHDESC", "MOCHBUSEO");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_MR_2CB84083, "MOCHWICHI", "CHDESC1", "MOCHWICHI");

            this.SetSpreadCodeHelper(this.FPS91_TY_S_MR_2CB84083, "MOVEND", "VNSANGHO", "MOVEND");
        }
        
        private void TYMRBP002I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_MR_2CB84083, "MODATE");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.CBH02_MABUSEO.DummyValue   = DateTime.Now.ToString("yyyyMMdd");
            this.CBH02_MAFRBUSEO.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            UP_SetReadOnly(true);

            SetStartingFocus(this.TXT01_MAYYMM);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_MR_2CB7L079",
                this.TXT01_MAYYMM.GetValue().ToString(),
                this.TXT01_MASEQ.GetValue().ToString(),
                this.TXT01_MABPDESC.GetValue().ToString()
                );

            this.FPS91_TY_S_MR_2CB94087.SetValue(this.DbConnector.ExecuteDataTable());

            this.TXT02_MAYYMM.SetValue("");
            this.TXT02_MASEQ.SetValue("");
            this.CBH02_MABUSEO.SetValue("");
            this.CBH02_MABPCODE.SetValue("");
            this.DTP02_MABUYDATE.SetValue("");
            this.CBH02_MAVEND.SetValue("");
            this.CBH02_MAFRBUSEO.SetValue("");
            this.CBH02_MAFRWICHI.SetValue("");
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sMOCHDATE  = string.Empty;
            string sMOFRBUSEO = string.Empty;
            string sMOFRWICHI = string.Empty;

            int i = 0;
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            DataTable dt = new DataTable();

            // 등록
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sMOCHDATE = "";

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2CC5K119",
                    this.TXT02_MAYYMM.GetValue().ToString(),
                    this.TXT02_MASEQ.GetValue().ToString(),
                    ds.Tables[0].Rows[i]["MODATE"].ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    // 비품이동의 변경부서,위치 -> 비품이동 현재부서,위치로 업데이트
                    sMOFRBUSEO = dt.Rows[0]["MOCHBUSEO"].ToString();
                    sMOFRWICHI = dt.Rows[0]["MOCHWICHI"].ToString();

                    sMOCHDATE = dt.Rows[0]["MODATE"].ToString();
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2CC5L120",
                        this.TXT02_MAYYMM.GetValue().ToString(),
                        this.TXT02_MASEQ.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        // 비품마스터의 사용부서,사용위치 -> 비품이동 현재부서,위치로 업데이트
                        sMOFRBUSEO = dt.Rows[0]["MAFRBUSEO"].ToString();
                        sMOFRWICHI = dt.Rows[0]["MAFRWICHI"].ToString();

                        sMOCHDATE = dt.Rows[0]["MABUYDATE"].ToString();
                    }
                }

                if (int.Parse(sMOCHDATE.ToString()) > int.Parse(ds.Tables[0].Rows[i]["MODATE"].ToString()))
                {
                    this.ShowMessage("TY_M_MR_2CDA2125");
                    return;
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2CBC0055",
                    this.TXT02_MAYYMM.GetValue().ToString(),
                    this.TXT02_MASEQ.GetValue().ToString(),
                    ds.Tables[0].Rows[i]["MODATE"].ToString(),
                    this.CBH02_MABUSEO.GetValue().ToString(),
                    sMOFRBUSEO.ToString(),
                    sMOFRWICHI.ToString(),
                    ds.Tables[0].Rows[i]["MOCHCODE"].ToString(),
                    ds.Tables[0].Rows[i]["MOCHBUSEO"].ToString(),
                    ds.Tables[0].Rows[i]["MOCHWICHI"].ToString(),
                    ds.Tables[0].Rows[i]["MODESC"].ToString(),
                    ds.Tables[0].Rows[i]["MOAMOUNT"].ToString(),
                    ds.Tables[0].Rows[i]["MOVEND"].ToString(),
                    ds.Tables[0].Rows[i]["MOGIVER"].ToString(),
                    ds.Tables[0].Rows[i]["MODONOR"].ToString(),
                    "",
                    "",
                    "",
                    TYUserInfo.EmpNo
                    );

                // 비품마스터 변경에 대한 부분 업데이트
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2CD94121",
                    ds.Tables[0].Rows[i]["MODATE"].ToString(),
                    ds.Tables[0].Rows[i]["MOCHCODE"].ToString(),
                    ds.Tables[0].Rows[i]["MODESC"].ToString(),
                    ds.Tables[0].Rows[i]["MOCHBUSEO"].ToString(),
                    ds.Tables[0].Rows[i]["MOCHWICHI"].ToString(),
                    TYUserInfo.EmpNo,
                    this.TXT02_MAYYMM.GetValue().ToString(),
                    this.TXT02_MASEQ.GetValue().ToString()
                    );

                this.DbConnector.ExecuteTranQueryList();
            }

            for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2CC5K119",
                    this.TXT02_MAYYMM.GetValue().ToString(),
                    this.TXT02_MASEQ.GetValue().ToString(),
                    ds.Tables[1].Rows[i]["MODATE"].ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    // 비품이동의 변경부서,위치 -> 비품이동 현재부서,위치로 업데이트
                    sMOFRBUSEO = dt.Rows[0]["MOCHBUSEO"].ToString();
                    sMOFRWICHI = dt.Rows[0]["MOCHWICHI"].ToString();
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_2CC5L120",
                        this.TXT02_MAYYMM.GetValue().ToString(),
                        this.TXT02_MASEQ.GetValue().ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        // 비품마스터의 사용부서,사용위치 -> 비품이동 현재부서,위치로 업데이트
                        sMOFRBUSEO = dt.Rows[0]["MAFRBUSEO"].ToString();
                        sMOFRWICHI = dt.Rows[0]["MAFRWICHI"].ToString();
                    }
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2CBC1056",
                    this.CBH02_MABUSEO.GetValue().ToString(),
                    sMOFRBUSEO.ToString(),
                    sMOFRWICHI.ToString(),
                    ds.Tables[1].Rows[i]["MOCHCODE"].ToString(),
                    ds.Tables[1].Rows[i]["MOCHBUSEO"].ToString(),
                    ds.Tables[1].Rows[i]["MOCHWICHI"].ToString(),
                    ds.Tables[1].Rows[i]["MODESC"].ToString(),
                    ds.Tables[1].Rows[i]["MOAMOUNT"].ToString(),
                    ds.Tables[1].Rows[i]["MOVEND"].ToString(),
                    ds.Tables[1].Rows[i]["MOGIVER"].ToString(),
                    ds.Tables[1].Rows[i]["MODONOR"].ToString(),
                    "",
                    "",
                    "",
                    TYUserInfo.EmpNo,
                    this.TXT02_MAYYMM.GetValue().ToString(),
                    this.TXT02_MASEQ.GetValue().ToString(),
                    ds.Tables[1].Rows[i]["MODATE"].ToString()
                    );

                // 비품마스터 변경에 대한 부분 업데이트
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2CD94121",
                    ds.Tables[1].Rows[i]["MODATE"].ToString(),
                    ds.Tables[1].Rows[i]["MOCHCODE"].ToString(),
                    ds.Tables[1].Rows[i]["MODESC"].ToString(),
                    ds.Tables[1].Rows[i]["MOCHBUSEO"].ToString(),
                    ds.Tables[1].Rows[i]["MOCHWICHI"].ToString(),
                    TYUserInfo.EmpNo,
                    this.TXT02_MAYYMM.GetValue().ToString(),
                    this.TXT02_MASEQ.GetValue().ToString()
                    );

                this.DbConnector.ExecuteTranQueryList();
            }

            this.ShowMessage("TY_M_GB_23NAD873");

            UP_SEL_EQUBPMOMF();
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            for (i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_2CBC2057",
                    this.TXT02_MAYYMM.GetValue().ToString(),
                    this.TXT02_MASEQ.GetValue().ToString(),
                    dt.Rows[i]["MODATE"].ToString()
                    );

                this.DbConnector.ExecuteTranQueryList();
            }

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지

            UP_SEL_EQUBPMOMF();
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2CC5L120",
                this.TXT02_MAYYMM.GetValue().ToString(),
                this.TXT02_MASEQ.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_MR_2CD9X124");
                e.Successed = false;
                return;
            }

            DataSet ds = new DataSet();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_MR_2CB84083.GetDataSourceInclude(TSpread.TActionType.New, "MODATE", "MOCHCODE", "MOCHBUSEO", "MOCHWICHI", "MODESC", "MOAMOUNT", "MOVEND", "MOGIVER", "MODONOR"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_MR_2CB84083.GetDataSourceInclude(TSpread.TActionType.Update, "MODATE", "MOCHCODE", "MOCHBUSEO", "MOCHWICHI", "MODESC", "MOAMOUNT", "MOVEND", "MOGIVER", "MODONOR", "MOSIGN", "MOGWBUNHO", "MOJPNO"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }
            else
            {
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach(
                                           "TY_P_MR_2CEC1175",
                                           this.TXT02_MAYYMM.GetValue().ToString(),
                                           this.TXT02_MASEQ.GetValue().ToString(),
                                           ds.Tables[0].Rows[i]["MODATE"].ToString()
                                           );

                    if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_MR_2CEC3176");
                        e.Successed = false;
                        return;
                    }

                    // 변경일자 이후 자료 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach(
                                           "TY_P_MR_2CEBT208",
                                           this.TXT02_MAYYMM.GetValue().ToString(),
                                           this.TXT02_MASEQ.GetValue().ToString(),
                                           ds.Tables[0].Rows[i]["MODATE"].ToString()
                                           );

                    if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_MR_2CEBV210");
                        e.Successed = false;
                        return;
                    }

                    if (ds.Tables[0].Rows[i]["MOCHCODE"].ToString() == "90")
                    {
                        if (int.Parse(Get_Numeric(ds.Tables[0].Rows[i]["MOAMOUNT"].ToString())) == 0 &&
                            ds.Tables[0].Rows[i]["MOVEND"].ToString() == "")
                        {
                            this.ShowMessage("TY_M_MR_2CB90088");
                            e.Successed = false;
                            return;
                        }
                    }

                    if (int.Parse(Get_Numeric(ds.Tables[0].Rows[i]["MOAMOUNT"].ToString())) == 0)
                    {
                        if (ds.Tables[0].Rows[i]["MOVEND"].ToString() != "")
                        {
                            this.ShowMessage("TY_M_MR_2CB90088");
                            e.Successed = false;
                            return;
                        }
                    }
                    else
                    {
                        if (ds.Tables[0].Rows[i]["MOVEND"].ToString() == "")
                        {
                            this.ShowMessage("TY_M_MR_2CB90089");
                            e.Successed = false;
                            return;
                        }
                    }
                }

                for (i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    if (ds.Tables[1].Rows[i]["MOSIGN"].ToString() == "Y")
                    {
                        this.ShowMessage("TY_M_MR_2CC54111");
                        e.Successed = false;
                        return;
                    }

                    if (ds.Tables[1].Rows[i]["MOGWBUNHO"].ToString() != "")
                    {
                        this.ShowMessage("TY_M_MR_2CC54111");
                        e.Successed = false;
                        return;
                    }

                    if (ds.Tables[1].Rows[i]["MOJPNO"].ToString() != "")
                    {
                        this.ShowMessage("TY_M_MR_2CD9V123");
                        e.Successed = false;
                        return;
                    }

                    if (ds.Tables[1].Rows[i]["MOCHCODE"].ToString() == "90")
                    {
                        if (int.Parse(Get_Numeric(ds.Tables[1].Rows[i]["MOAMOUNT"].ToString())) == 0 &&
                            ds.Tables[1].Rows[i]["MOVEND"].ToString() == "")
                        {
                            this.ShowMessage("TY_M_MR_2CB90088");
                            e.Successed = false;
                            return;
                        }
                    }

                    if (int.Parse(Get_Numeric(ds.Tables[1].Rows[i]["MOAMOUNT"].ToString())) == 0)
                    {
                        if (ds.Tables[1].Rows[i]["MOVEND"].ToString() != "")
                        {
                            this.ShowMessage("TY_M_MR_2CB90088");
                            e.Successed = false;
                            return;
                        }
                    }
                    else
                    {
                        if (ds.Tables[1].Rows[i]["MOVEND"].ToString() == "")
                        {
                            this.ShowMessage("TY_M_MR_2CB90089");
                            e.Successed = false;
                            return;
                        }
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

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataTable dz = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2CC5L120",
                this.TXT02_MAYYMM.GetValue().ToString(),
                this.TXT02_MASEQ.GetValue().ToString()
                );

            dz = this.DbConnector.ExecuteDataTable();

            if (dz.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_MR_2CD9X124");
                e.Successed = false;
                return;
            }

            DataTable dt = this.FPS91_TY_S_MR_2CB84083.GetDataSourceInclude(TSpread.TActionType.Remove, "MODATE", "MOSIGN", "MOGWBUNHO", "MOJPNO");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }


            for (i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["MOSIGN"].ToString() == "Y")
                {
                    this.ShowMessage("TY_M_MR_2CC54111");
                    e.Successed = false;
                    return;
                }

                if (dt.Rows[i]["MOGWBUNHO"].ToString() != "")
                {
                    this.ShowMessage("TY_M_MR_2CC54111");
                    e.Successed = false;
                    return;
                }

                if (dt.Rows[i]["MOJPNO"].ToString() != "")
                {
                    this.ShowMessage("TY_M_MR_2CD9V123");
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

        #region Description : 사용자 정의 함수
        private void UP_SetReadOnly(bool bTrueAndFalse)
        {
            this.TXT02_MAYYMM.SetReadOnly(bTrueAndFalse);
            this.TXT02_MASEQ.SetReadOnly(bTrueAndFalse);
            this.CBH02_MABUSEO.SetReadOnly(bTrueAndFalse);
            this.CBH02_MABPCODE.SetReadOnly(bTrueAndFalse);
            this.DTP02_MABUYDATE.SetReadOnly(bTrueAndFalse);
            this.CBH02_MAVEND.SetReadOnly(bTrueAndFalse);
            this.CBH02_MAFRBUSEO.SetReadOnly(bTrueAndFalse);
            this.CBH02_MAFRWICHI.SetReadOnly(bTrueAndFalse);
        }
        #endregion

        #region Description : 비품 마스터 스프레드 이벤트
        private void FPS91_TY_S_MR_2CB94087_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.TXT02_MAYYMM.SetValue(this.FPS91_TY_S_MR_2CB94087.GetValue("MAYYMM").ToString());
            this.TXT02_MASEQ.SetValue(this.FPS91_TY_S_MR_2CB94087.GetValue("MASEQ").ToString());

            this.CBH02_MABUSEO.DummyValue   = this.TXT02_MAYYMM.GetValue().ToString() + "01";
            this.CBH02_MAFRBUSEO.DummyValue = this.TXT02_MAYYMM.GetValue().ToString() + "01";

            this.CBH02_MABUSEO.SetValue(this.FPS91_TY_S_MR_2CB94087.GetValue("MABUSEO").ToString());
            this.CBH02_MABPCODE.SetValue(this.FPS91_TY_S_MR_2CB94087.GetValue("MABPCODE").ToString());
            this.DTP02_MABUYDATE.SetValue(this.FPS91_TY_S_MR_2CB94087.GetValue("MABUYDATE").ToString());
            this.CBH02_MAVEND.SetValue(this.FPS91_TY_S_MR_2CB94087.GetValue("MAVEND").ToString());
            this.CBH02_MAFRBUSEO.SetValue(this.FPS91_TY_S_MR_2CB94087.GetValue("MAFRBUSEO").ToString());
            this.CBH02_MAFRWICHI.SetValue(this.FPS91_TY_S_MR_2CB94087.GetValue("MAFRWICHI").ToString());

            UP_SetReadOnly(true);

            UP_SEL_EQUBPMOMF();
        }
        #endregion

        #region Description : 비품 이동 조회
        private void UP_SEL_EQUBPMOMF()
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_MR_2CB82082",
                this.TXT02_MAYYMM.GetValue().ToString(),
                this.TXT02_MASEQ.GetValue().ToString()
                );

            this.FPS91_TY_S_MR_2CB84083.SetValue(this.DbConnector.ExecuteDataTable());

            for (int i = 0; i < this.FPS91_TY_S_MR_2CB84083.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_MR_2CB84083.GetValue(i, "MOGWBUNHO").ToString() == "")
                {
                    this.FPS91_TY_S_MR_2CB84083_Sheet1.Cells[i, 15].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                }
            }
        }
        #endregion

        private void FPS91_TY_S_MR_2CB84083_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            if (e.Column != 1)
                return;

            // 부서명을 가져오기 위해서 스프레드의 예산년도에 파라미터 날짜를 넣음.
            string year = FPS91_TY_S_MR_2CB84083.GetValue(e.Row, "MODATE").ToString();            
            TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_MR_2CB84083, "MOCHBUSEO");
            if (tyCodeBox != null)
                tyCodeBox.DummyValue = year;
        }

        #region Description : 그룹웨어 문서 바로가기
        private void FPS91_TY_S_MR_2CB84083_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "15")
            {
                if (this.FPS91_TY_S_MR_2CB84083.GetValue("MOGWBUNHO").ToString() != "")
                {
                    if ((new TYMRPR005S(this.FPS91_TY_S_MR_2CB84083.GetValue("MOGWBUNHO").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                    }
                }
                //else
                //{
                //    this.ShowMessage("TY_M_MR_2BC51262");
                //    return;
                //}
            }
        }
        #endregion
    }
}
