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

namespace TY.ER.US00
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
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_32J7C129 : 선급자재 생성 조회
    ///  TY_S_MR_32J7M130 : 선급자재 DETAIL 조회
    ///  TY_S_US_91OA3571 : 선급자재 DETAIL 하위 조회
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
    public partial class TYUSME011I : TYBase
    {
        private string fsGUBUN   = string.Empty;

        #region Description : 페이지 로드
        public TYUSME011I()
        {
            InitializeComponent();
        }

        private void TYUSME011I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_91OA5572, "ILDATE");

            this.FPS91_TY_S_US_91OA3571.Initialize();
            this.FPS91_TY_S_US_91OA5572.Initialize();

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            UP_BTN_DISPLAY("");

            this.DTP01_GDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));

            SetStartingFocus(this.DTP01_GDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.FPS91_TY_S_US_91OA3571.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_91O9T569",
                Get_Date(this.DTP01_GDATE.GetValue().ToString()),
                this.CBH01_GHANGCHA.GetValue().ToString(),
                this.CBH01_GHWAJU.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_91OA3571.SetValue(dt);

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

            this.FPS91_TY_S_US_91OA5572.Initialize();

            SetFocus(this.DTP01_ISMCYYMM);
        }
        #endregion

        #region Description : 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            for (i = 0; i < ds.Tables[0].Rows.Count; i++) // 등록
            {
                // 이송료 일자별 출고DATA 등록
                this.DbConnector.Attach("TY_P_US_91O9H564", Get_Date(this.DTP01_ISMCYYMM.GetValue().ToString().Trim()),
                                                            this.CBH01_ISHANGCHA.GetValue().ToString().Trim(),
                                                            this.CBH01_ISGOKJONG.GetValue().ToString().Trim(),
                                                            this.CBH01_ISHWAJU.GetValue().ToString().Trim(),
                                                            Get_Date(this.DTP01_ISYYMMDD.GetValue().ToString().Trim()),
                                                            Get_Date(ds.Tables[0].Rows[i]["ILDATE"].ToString().Trim()),
                                                            Get_Numeric(ds.Tables[0].Rows[i]["ILCHQTY"].ToString().Trim())
                                                            );
            }

            for (i = 0; i < ds.Tables[1].Rows.Count; i++) // 수정
            {
                // 이송료 일자별 출고DATA 수정
                this.DbConnector.Attach("TY_P_US_91O9R566", Get_Numeric(ds.Tables[1].Rows[i]["ILCHQTY"].ToString().Trim()),
                                                            Get_Date(this.DTP01_ISMCYYMM.GetValue().ToString().Trim()),
                                                            this.CBH01_ISHANGCHA.GetValue().ToString().Trim(),
                                                            this.CBH01_ISGOKJONG.GetValue().ToString().Trim(),
                                                            this.CBH01_ISHWAJU.GetValue().ToString().Trim(),
                                                            Get_Date(this.DTP01_ISYYMMDD.GetValue().ToString().Trim()),
                                                            Get_Date(ds.Tables[1].Rows[i]["ILDATE"].ToString().Trim())
                                                            );
            }

            for (i = 0; i < ds.Tables[2].Rows.Count; i++) // 삭제
            {
                // 이송료 일자별 출고DATA 삭제
                this.DbConnector.Attach("TY_P_US_91O9Q565", Get_Date(this.DTP01_ISMCYYMM.GetValue().ToString().Trim()),
                                                            this.CBH01_ISHANGCHA.GetValue().ToString().Trim(),
                                                            this.CBH01_ISGOKJONG.GetValue().ToString().Trim(),
                                                            this.CBH01_ISHWAJU.GetValue().ToString().Trim(),
                                                            Get_Date(this.DTP01_ISYYMMDD.GetValue().ToString().Trim()),
                                                            ds.Tables[2].Rows[i]["ILDATE"].ToString().Trim()
                                                            );
            }
            this.DbConnector.ExecuteTranQueryList();

            DataTable dt = new DataTable();

            // 이송료 DATA 생성 또는 수정 또는 삭제

            // 이송료 일자별 출고DATA 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_91O9G563",
                Get_Date(this.DTP01_ISMCYYMM.GetValue().ToString()),
                this.CBH01_ISHANGCHA.GetValue().ToString(),
                this.CBH01_ISGOKJONG.GetValue().ToString(),
                this.CBH01_ISHWAJU.GetValue().ToString(),
                Get_Date(this.DTP01_ISYYMMDD.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                // 이송료 삭제
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_91O9D560", Get_Date(this.DTP01_ISMCYYMM.GetValue().ToString().Trim()),
                                                            this.CBH01_ISHANGCHA.GetValue().ToString().Trim(),
                                                            this.CBH01_ISGOKJONG.GetValue().ToString().Trim(),
                                                            this.CBH01_ISHWAJU.GetValue().ToString().Trim(),
                                                            Get_Date(this.DTP01_ISYYMMDD.GetValue().ToString().Trim())
                                                            );
                this.DbConnector.ExecuteNonQuery();
            }
            else
            {
                string sCDGUBUN1   = string.Empty;

                string sCNBYISSVAT = string.Empty;

                double dCNBYISSAMT = 0;

                double dISISAMT    = 0;
                double dISISVAT    = 0;

                double dILCHQTYHap = 0;


                // 1. 곡종 => 주원료, 부원료인지 확인
                // 2. 계약관리의 이송료(주원료, 부원료) 단가 가져오기
                // 3. 이송료 일자별 출고DATA 출고량, 양도량 합계 DATA 가져오기
                // 5. 이송료, 이송료 계산

                // 1. 곡종 => 주원료, 부원료인지 확인
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_US_8BJFE166",
                    "GK",
                    this.CBH01_ISGOKJONG.GetValue().ToString(),
                    ""
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sCDGUBUN1 = dt.Rows[0]["CDGUBUN1"].ToString();
                }

                // 2. 계약관리의 이송료(주원료, 부원료) 단가 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_US_8BJHK186",
                    Set_Fill4(this.TXT01_ISCONTNO.GetValue().ToString().Substring(0, 4)),
                    Set_Fill2(this.TXT01_ISCONTNO.GetValue().ToString().Substring(4, 2))
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    dCNBYISSAMT = double.Parse(dt.Rows[0]["CNBYISSAMT"].ToString());
                    sCNBYISSVAT = dt.Rows[0]["CNBYISSVAT"].ToString();
                }

                // 3. 이송료 일자별 출고DATA 출고량, 양도량 합계 DATA 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_US_91O9S567",
                    Get_Date(this.DTP01_ISMCYYMM.GetValue().ToString()),
                    this.CBH01_ISHANGCHA.GetValue().ToString(),
                    this.CBH01_ISGOKJONG.GetValue().ToString(),
                    this.CBH01_ISHWAJU.GetValue().ToString(),
                    Get_Date(this.DTP01_ISYYMMDD.GetValue().ToString())
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    dILCHQTYHap   = double.Parse(Get_Numeric(dt.Rows[0]["ILCHQTY"].ToString()));

                    this.TXT01_ISCHQTY.SetValue(Get_Numeric(dt.Rows[0]["ILCHQTY"].ToString()));
                }

                // 5. 이송료
                if ((sCDGUBUN1 == "2" || sCDGUBUN1 == "3") &&
                    (this.CBH01_ISHWAJU.GetValue().ToString() == "D06" || this.CBH01_ISHWAJU.GetValue().ToString() == "I02" || this.CBH01_ISHWAJU.GetValue().ToString() == "J21"))
                {
                    if (sCNBYISSVAT == "N")
                    {
                        dISISAMT = Convert.ToDouble(UP_DotDelete(Convert.ToString((dILCHQTYHap * dCNBYISSAMT) / 10))) * 10;
                        dISISVAT = Math.Round(Convert.ToDouble(dISISAMT) / 10);
                    }
                    else
                    {
                        dISISAMT = Convert.ToDouble(UP_DotDelete(Convert.ToString((dILCHQTYHap * dCNBYISSAMT) / 10))) * 10;
                        dISISVAT = Math.Round(Convert.ToDouble(dISISAMT) / 11);
                        dISISAMT = dISISAMT - dISISVAT;
                    }
                }

                this.TXT01_ISISAMT.SetValue(Convert.ToString(dISISAMT));
                this.TXT01_ISISVAT.SetValue(Convert.ToString(dISISVAT));

                // 이송료 마스터 DATA 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_US_91O9V570",
                    Get_Date(this.DTP01_ISMCYYMM.GetValue().ToString()),
                    this.CBH01_ISHANGCHA.GetValue().ToString(),
                    this.CBH01_ISGOKJONG.GetValue().ToString(),
                    this.CBH01_ISHWAJU.GetValue().ToString(),
                    Get_Date(this.DTP01_ISYYMMDD.GetValue().ToString())
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    // 수정
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_91O9E562", Get_Numeric(this.TXT01_ISBEJNQTY.GetValue().ToString()),
                                                                Get_Numeric(this.TXT01_ISHWAKQTY.GetValue().ToString()),
                                                                Get_Numeric(this.TXT01_ISCHQTY.GetValue().ToString()),
                                                                Get_Numeric(this.TXT01_ISYDQTY.GetValue().ToString()),
                                                                Convert.ToString(dCNBYISSAMT),
                                                                this.TXT01_ISCONTNO.GetValue().ToString(),
                                                                Get_Numeric(this.TXT01_ISISAMT.GetValue().ToString()),
                                                                Get_Numeric(this.TXT01_ISISVAT.GetValue().ToString()),
                                                                "21",
                                                                TYUserInfo.EmpNo,
                                                                Get_Date(this.DTP01_ISMCYYMM.GetValue().ToString().Trim()),
                                                                this.CBH01_ISHANGCHA.GetValue().ToString().Trim(),
                                                                this.CBH01_ISGOKJONG.GetValue().ToString().Trim(),
                                                                this.CBH01_ISHWAJU.GetValue().ToString().Trim(),
                                                                Get_Date(this.DTP01_ISYYMMDD.GetValue().ToString().Trim())
                                                                );
                    this.DbConnector.ExecuteNonQuery();
                }
                else
                {
                    // 등록
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_91O95559", Get_Date(this.DTP01_ISMCYYMM.GetValue().ToString().Trim()),
                                                                this.CBH01_ISHANGCHA.GetValue().ToString().Trim(),
                                                                this.CBH01_ISGOKJONG.GetValue().ToString().Trim(),
                                                                this.CBH01_ISHWAJU.GetValue().ToString().Trim(),
                                                                Get_Date(this.DTP01_ISYYMMDD.GetValue().ToString().Trim()),
                                                                Get_Numeric(this.TXT01_ISBEJNQTY.GetValue().ToString()),
                                                                Get_Numeric(this.TXT01_ISHWAKQTY.GetValue().ToString()),
                                                                Get_Numeric(this.TXT01_ISCHQTY.GetValue().ToString()),
                                                                Get_Numeric(this.TXT01_ISYDQTY.GetValue().ToString()),
                                                                Convert.ToString(dCNBYISSAMT),
                                                                this.TXT01_ISCONTNO.GetValue().ToString(),
                                                                Get_Numeric(this.TXT01_ISISAMT.GetValue().ToString()),
                                                                Get_Numeric(this.TXT01_ISISVAT.GetValue().ToString()),
                                                                "21",
                                                                TYUserInfo.EmpNo
                                                                );
                    this.DbConnector.ExecuteNonQuery();
                }
            }
            

            this.ShowMessage("TY_M_MR_2BF50354");

            UP_RUN();
        }
        #endregion

        #region Description : 확인 메소드
        private void UP_RUN()
        {
            this.FPS91_TY_S_US_91OA5572.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_91O9V570",
                Get_Date(this.DTP01_ISMCYYMM.GetValue().ToString()),
                this.CBH01_ISHANGCHA.GetValue().ToString(),
                this.CBH01_ISGOKJONG.GetValue().ToString(),
                this.CBH01_ISHWAJU.GetValue().ToString(),
                Get_Date(this.DTP01_ISYYMMDD.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");

                fsGUBUN = "UPT";

                if (this.TXT01_ISJPNO.GetValue().ToString() != "")
                {
                    fsGUBUN = "";
                }

                UP_Field_ReadOnly(true);
            }
            else
            {
                fsGUBUN = "NEW";
            }

            // 이송료 일자별 출고DATA 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_91O9G563",
                Get_Date(this.DTP01_ISMCYYMM.GetValue().ToString()),
                this.CBH01_ISHANGCHA.GetValue().ToString(),
                this.CBH01_ISGOKJONG.GetValue().ToString(),
                this.CBH01_ISHWAJU.GetValue().ToString(),
                Get_Date(this.DTP01_ISYYMMDD.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();
            
            this.FPS91_TY_S_US_91OA5572.SetValue(dt);

            UP_BTN_DISPLAY(fsGUBUN);

            SetFocus(this.FPS91_TY_S_US_91OA5572);
        }
        #endregion

        #region Description : 처리 ProcessCheck
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_US_91OA5572.GetDataSourceInclude(TSpread.TActionType.New,    "ILDATE", "ILCHQTY"));
            ds.Tables.Add(this.FPS91_TY_S_US_91OA5572.GetDataSourceInclude(TSpread.TActionType.Update, "ILDATE", "ILCHQTY"));
            ds.Tables.Add(this.FPS91_TY_S_US_91OA5572.GetDataSourceInclude(TSpread.TActionType.Remove, "ILDATE"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_MR_2BF4Z352");
                e.Successed = false;
                return;
            }

            if (Get_Date(this.DTP01_ISMCYYMM.GetValue().ToString()) != Get_Date(this.DTP01_ISYYMMDD.GetValue().ToString()).ToString().Substring(0, 6))
            {
                this.ShowMessage("TY_M_US_918HU455");

                SetFocus(this.DTP01_ISMCYYMM);

                e.Successed = false;
                return;
            }

            DataTable dt = new DataTable();

            // 매출발생월 다음달 미수금액이 존재하는지 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_917AH422", Get_Date(this.DTP01_ISMCYYMM.GetValue().ToString()).Substring(0, 6).ToString(),
                                                        "");
            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_US_917AL423");

                SetFocus(this.DTP01_ISMCYYMM);

                e.Successed = false;
                return;
            }

            // SILO 보관료 계약번호 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_918HO453",
                this.CBH01_ISHANGCHA.GetValue().ToString(),
                this.CBH01_ISGOKJONG.GetValue().ToString(),
                this.CBH01_ISHWAJU.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_ISCONTNO.SetValue(dt.Rows[0]["JGCONTNO"].ToString());
            }


            // 계약번호 존재체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_8BJHK186",
                Set_Fill4(this.TXT01_ISCONTNO.GetValue().ToString().Substring(0, 4)),
                Set_Fill2(this.TXT01_ISCONTNO.GetValue().ToString().Substring(4, 2))
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_US_917AY427");

                SetFocus(this.DTP01_ISMCYYMM);

                e.Successed = false;
                return;
            }

            int i = 0;

            // 등록시 이송료 일자별 출고DATA 존재 체크
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[0].Rows.Count; i++) // 신규
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_US_91O9S568",
                        Get_Date(this.DTP01_ISMCYYMM.GetValue().ToString()),
                        this.CBH01_ISHANGCHA.GetValue().ToString(),
                        this.CBH01_ISGOKJONG.GetValue().ToString(),
                        this.CBH01_ISHWAJU.GetValue().ToString(),
                        Get_Date(this.DTP01_ISYYMMDD.GetValue().ToString()),
                        ds.Tables[0].Rows[i]["ILDATE"].ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_US_91NBG549");

                        SetFocus(this.DTP01_ISMCYYMM);

                        e.Successed = false;
                        return;
                    }
                }
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[1].Rows.Count; i++) // 수정
                {
                    // 이송료 일자별 출고DATA 존재 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_US_91O9S568",
                        Get_Date(this.DTP01_ISMCYYMM.GetValue().ToString()),
                        this.CBH01_ISHANGCHA.GetValue().ToString(),
                        this.CBH01_ISGOKJONG.GetValue().ToString(),
                        this.CBH01_ISHWAJU.GetValue().ToString(),
                        Get_Date(this.DTP01_ISYYMMDD.GetValue().ToString()),
                        ds.Tables[1].Rows[i]["ILDATE"].ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_US_91NBH550");

                        SetFocus(this.DTP01_ISMCYYMM);

                        e.Successed = false;
                        return;
                    }
                }
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[2].Rows.Count; i++) // 삭제
                {
                    // 이송료 일자별 출고DATA 존재 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_US_91O9S568",
                        Get_Date(this.DTP01_ISMCYYMM.GetValue().ToString()),
                        this.CBH01_ISHANGCHA.GetValue().ToString(),
                        this.CBH01_ISGOKJONG.GetValue().ToString(),
                        this.CBH01_ISHWAJU.GetValue().ToString(),
                        Get_Date(this.DTP01_ISYYMMDD.GetValue().ToString()),
                        ds.Tables[2].Rows[i]["ILDATE"].ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_US_91NBH550");

                        SetFocus(this.DTP01_ISMCYYMM);

                        e.Successed = false;
                        return;
                    }
                }
            }

            // 처리하시겠습니까?
            if (!this.ShowMessage("TY_M_MR_2BF50353"))
            {
                e.Successed = false;
                return;
            }

            // 스프레드 칼럼 데이터 넘겨주는 부분
            e.ArgData = ds;
        }
        #endregion

        #region Description : 스프레드 더블클릭
        private void FPS91_TY_S_US_91OA3571_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DTP01_ISMCYYMM.SetValue(this.FPS91_TY_S_US_91OA3571.GetValue("ISMCYYMM").ToString());
            this.CBH01_ISHANGCHA.SetValue(this.FPS91_TY_S_US_91OA3571.GetValue("ISHANGCHA").ToString());
            this.CBH01_ISGOKJONG.SetValue(this.FPS91_TY_S_US_91OA3571.GetValue("ISGOKJONG").ToString());
            this.CBH01_ISHWAJU.SetValue(this.FPS91_TY_S_US_91OA3571.GetValue("ISHWAJU").ToString());
            this.DTP01_ISYYMMDD.SetValue(this.FPS91_TY_S_US_91OA3571.GetValue("ISYYMMDD").ToString());
            this.TXT01_ISJPNO.SetValue(this.FPS91_TY_S_US_91OA3571.GetValue("ISJPNO").ToString());

            UP_RUN();
        }
        #endregion

        #region Description : 필드 ReadOnly
        private void UP_Field_ReadOnly(bool boolean)
        {
            this.DTP01_ISMCYYMM.SetReadOnly(boolean);
            this.CBH01_ISHANGCHA.SetReadOnly(boolean);
            this.CBH01_ISGOKJONG.SetReadOnly(boolean);
            this.CBH01_ISHWAJU.SetReadOnly(boolean);
            this.DTP01_ISYYMMDD.SetReadOnly(boolean);

            this.TXT01_ISBEJNQTY.SetReadOnly(boolean);
            this.TXT01_ISHWAKQTY.SetReadOnly(boolean);
            this.TXT01_ISCONTNO.SetReadOnly(boolean);
        }
        #endregion

        #region Description : 버튼 디스플레이
        private void UP_BTN_DISPLAY(string sGUBUN)
        {
            if (sGUBUN == "NEW")
            {
                this.BTN61_BATCH.Visible = true;
            }
            else if (sGUBUN == "UPT")
            {
                this.BTN61_BATCH.Visible = true;
            }
            else
            {
                this.BTN61_BATCH.Visible = false;
            }
        }
        #endregion

        #region Description : 필드 클리어
        private void UP_Field_Clear()
        {
            this.CBH01_ISHANGCHA.SetValue("");
            this.CBH01_ISGOKJONG.SetValue("");
            this.CBH01_ISHWAJU.SetValue("");
            
            this.TXT01_ISBEJNQTY.SetValue("");
            this.TXT01_ISHWAKQTY.SetValue("");
            this.TXT01_ISCHQTY.SetValue("");
            this.TXT01_ISYDQTY.SetValue("");
            this.TXT01_ISCONTNO.SetValue("");
            this.TXT01_ISJPNO.SetValue("");

            this.TXT01_ISISAMT.SetValue("");
            this.TXT01_ISISVAT.SetValue("");
        }
        #endregion

        #region Description : 화주 이벤트
        private void CBH01_GHWAJU_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetFocus(this.BTN61_INQ);
            }
        }
        #endregion
    }
}