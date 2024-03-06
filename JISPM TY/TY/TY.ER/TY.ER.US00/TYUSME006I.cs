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
    ///  TY_S_US_91MI4537 : 선급자재 DETAIL 하위 조회
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
    public partial class TYUSME006I : TYBase
    {
        private string fsGUBUN   = string.Empty;
        
        #region Description : 페이지 로드
        public TYUSME006I()
        {
            InitializeComponent();
        }

        private void TYUSME006I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_91NA9543, "HLDATE");

            this.FPS91_TY_S_US_91MI4537.Initialize();
            this.FPS91_TY_S_US_91NA9543.Initialize();

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);
            this.BTN61_EDIT.ProcessCheck += new TButton.CheckHandler(BTN61_EDIT_ProcessCheck);

            UP_BTN_DISPLAY("");

            this.DTP01_GDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));

            SetStartingFocus(this.DTP01_GDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.FPS91_TY_S_US_91MI4537.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_91MI2536",
                Get_Date(this.DTP01_GDATE.GetValue().ToString()),
                this.CBH01_GHANGCHA.GetValue().ToString(),
                this.CBH01_GHWAJU.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_91MI4537.SetValue(dt);

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

            this.FPS91_TY_S_US_91NA9543.Initialize();

            SetFocus(this.DTP01_HYMCYYMM);
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
                // 하역료 일자별 출고DATA 등록
                this.DbConnector.Attach("TY_P_US_91NBK551", Get_Date(this.DTP01_HYMCYYMM.GetValue().ToString().Trim()),
                                                            this.CBH01_HYHANGCHA.GetValue().ToString().Trim(),
                                                            this.CBH01_HYGOKJONG.GetValue().ToString().Trim(),
                                                            this.CBH01_HYHWAJU.GetValue().ToString().Trim(),
                                                            Get_Date(this.DTP01_HYYYMMDD.GetValue().ToString().Trim()),
                                                            Get_Date(ds.Tables[0].Rows[i]["HLDATE"].ToString().Trim()),
                                                            Get_Numeric(ds.Tables[0].Rows[i]["HLCHQTY"].ToString().Trim()),
                                                            Get_Numeric(ds.Tables[0].Rows[i]["HLYDQTY"].ToString().Trim())
                                                            );
            }

            for (i = 0; i < ds.Tables[1].Rows.Count; i++) // 수정
            {
                // 하역료 일자별 출고DATA 수정
                this.DbConnector.Attach("TY_P_US_91NBK552", Get_Numeric(ds.Tables[1].Rows[i]["HLCHQTY"].ToString().Trim()),
                                                            Get_Numeric(ds.Tables[1].Rows[i]["HLYDQTY"].ToString().Trim()),
                                                            Get_Date(this.DTP01_HYMCYYMM.GetValue().ToString().Trim()),
                                                            this.CBH01_HYHANGCHA.GetValue().ToString().Trim(),
                                                            this.CBH01_HYGOKJONG.GetValue().ToString().Trim(),
                                                            this.CBH01_HYHWAJU.GetValue().ToString().Trim(),
                                                            Get_Date(this.DTP01_HYYYMMDD.GetValue().ToString().Trim()),
                                                            Get_Date(ds.Tables[1].Rows[i]["HLDATE"].ToString().Trim())
                                                            );
            }

            for (i = 0; i < ds.Tables[2].Rows.Count; i++) // 삭제
            {
                // 하역료 일자별 출고DATA 삭제
                this.DbConnector.Attach("TY_P_US_91NBL553", Get_Date(this.DTP01_HYMCYYMM.GetValue().ToString().Trim()),
                                                            this.CBH01_HYHANGCHA.GetValue().ToString().Trim(),
                                                            this.CBH01_HYGOKJONG.GetValue().ToString().Trim(),
                                                            this.CBH01_HYHWAJU.GetValue().ToString().Trim(),
                                                            Get_Date(this.DTP01_HYYYMMDD.GetValue().ToString().Trim()),
                                                            ds.Tables[2].Rows[i]["HLDATE"].ToString().Trim()
                                                            );
            }
            this.DbConnector.ExecuteTranQueryList();

            DataTable dt = new DataTable();

            // 하역료 DATA 생성 또는 수정 또는 삭제

            // 하역료 일자별 출고DATA 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_91NA8542",
                Get_Date(this.DTP01_HYMCYYMM.GetValue().ToString()),
                this.CBH01_HYHANGCHA.GetValue().ToString(),
                this.CBH01_HYGOKJONG.GetValue().ToString(),
                this.CBH01_HYHWAJU.GetValue().ToString(),
                Get_Date(this.DTP01_HYYYMMDD.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                // 하역료 삭제
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_91NBZ556", Get_Date(this.DTP01_HYMCYYMM.GetValue().ToString().Trim()),
                                                            this.CBH01_HYHANGCHA.GetValue().ToString().Trim(),
                                                            this.CBH01_HYGOKJONG.GetValue().ToString().Trim(),
                                                            this.CBH01_HYHWAJU.GetValue().ToString().Trim(),
                                                            Get_Date(this.DTP01_HYYYMMDD.GetValue().ToString().Trim())
                                                            );
                this.DbConnector.ExecuteNonQuery();
            }
            else
            {
                string sCDGUBUN1   = string.Empty;

                string sCNHAYVAT   = string.Empty;
                string sCNBYHAYVAT = string.Empty;
                string sCNBYISSVAT = string.Empty;

                double dCNHAYAMT   = 0;
                double dCNBYHAYAMT = 0;
                double dCNBYISSAMT = 0;

                double dHYHAYKAMT  = 0;
                double dHYHAYKVAT  = 0;
                double dHYISAMT    = 0;
                double dHYISVAT    = 0;

                double dHLCHQTYHap   = 0;
                double dHLYDQTYHap   = 0;
                double dHLCHQTYTotal = 0;


                // 1. 곡종 => 주원료, 부원료인지 확인
                // 2. 계약관리의 하역료(주원료, 부원료) 단가 가져오기
                // 3. 하역료 일자별 출고DATA 출고량, 양도량 합계 DATA 가져오기
                // 5. 하역료, 이송료 계산

                // 1. 곡종 => 주원료, 부원료인지 확인
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_US_8BJFE166",
                    "GK",
                    this.CBH01_HYGOKJONG.GetValue().ToString(),
                    ""
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sCDGUBUN1 = dt.Rows[0]["CDGUBUN1"].ToString();
                }

                // 2. 계약관리의 하역료(주원료, 부원료) 단가 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_US_8BJHK186",
                    Set_Fill4(this.TXT01_HYCONTNO.GetValue().ToString().Substring(0, 4)),
                    Set_Fill2(this.TXT01_HYCONTNO.GetValue().ToString().Substring(4, 2))
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    dCNHAYAMT   = double.Parse(dt.Rows[0]["CNHAYAMT"].ToString());
                    dCNBYHAYAMT = double.Parse(dt.Rows[0]["CNBYHAYAMT"].ToString());
                    dCNBYISSAMT = double.Parse(dt.Rows[0]["CNBYISSAMT"].ToString());

                    sCNHAYVAT   = dt.Rows[0]["CNHAYVAT"].ToString();
                    sCNBYHAYVAT = dt.Rows[0]["CNBYHAYVAT"].ToString();
                }

                // 3. 하역료 일자별 출고DATA 출고량, 양도량 합계 DATA 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_US_91NGK557",
                    Get_Date(this.DTP01_HYMCYYMM.GetValue().ToString()),
                    this.CBH01_HYHANGCHA.GetValue().ToString(),
                    this.CBH01_HYGOKJONG.GetValue().ToString(),
                    this.CBH01_HYHWAJU.GetValue().ToString(),
                    Get_Date(this.DTP01_HYYYMMDD.GetValue().ToString())
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    dHLCHQTYHap   = double.Parse(Get_Numeric(dt.Rows[0]["HLCHQTY"].ToString()));
                    dHLYDQTYHap   = double.Parse(Get_Numeric(dt.Rows[0]["HLYDQTY"].ToString()));
                    dHLCHQTYTotal = double.Parse(Get_Numeric(dt.Rows[0]["HLTOTQTY"].ToString()));

                    this.TXT01_HYCHQTY.SetValue(Get_Numeric(dt.Rows[0]["HLCHQTY"].ToString()));
                    this.TXT01_HYYDQTY.SetValue(Get_Numeric(dt.Rows[0]["HLYDQTY"].ToString()));
                }

                // 5. 하역료 주원료
                //    주원료 : "1" 부원료(야적단가) : "2" 부원료(창고단가) : "3"
                if (sCDGUBUN1 == "1")
                {
                    if (sCNHAYVAT == "N")
                    {
                        dHYHAYKAMT = Convert.ToDouble(UP_DotDelete(Convert.ToString((dHLCHQTYTotal * dCNHAYAMT) / 10))) * 10;
                        dHYHAYKVAT = Math.Round(Convert.ToDouble(dHYHAYKAMT) / 10);
                    }
                    else
                    {
                        dHYHAYKAMT = Convert.ToDouble(UP_DotDelete(Convert.ToString((dHLCHQTYTotal * dCNHAYAMT) / 10))) * 10;
                        dHYHAYKVAT = Math.Round(Convert.ToDouble(dHYHAYKAMT) / 11);
                        dHYHAYKAMT = dHYHAYKAMT - dHYHAYKVAT;
                    }
                }

                // 5. 하역료 부원료
                //    주원료 : "1" 부원료(야적단가) : "2" 부원료(창고단가) : "3"
                if (sCDGUBUN1 == "2" || sCDGUBUN1 == "3")
                {
                    if (sCNBYHAYVAT == "N")
                    {
                        dHYHAYKAMT = Convert.ToDouble(UP_DotDelete(Convert.ToString((dHLCHQTYTotal * dCNBYHAYAMT) / 10))) * 10;
                        dHYHAYKVAT = Math.Round(Convert.ToDouble(dHYHAYKAMT) / 10);
                    }
                    else
                    {
                        dHYHAYKAMT = Convert.ToDouble(UP_DotDelete(Convert.ToString((dHLCHQTYTotal * dCNBYHAYAMT) / 10))) * 10;
                        dHYHAYKVAT = Math.Round(Convert.ToDouble(dHYHAYKAMT) / 11);
                        dHYHAYKAMT = dHYHAYKAMT - dHYHAYKVAT;
                    }
                }

                this.TXT01_HYHAYKAMT.SetValue(Convert.ToString(dHYHAYKAMT));
                this.TXT01_HYHAYKVAT.SetValue(Convert.ToString(dHYHAYKVAT));

                // 5. 이송료
                if ((sCDGUBUN1 == "2" || sCDGUBUN1 == "3") && (this.CBH01_HYHWAJU.GetValue().ToString() == "D06" || this.CBH01_HYHWAJU.GetValue().ToString() == "I02"))
                {
                    if (sCNBYISSVAT == "N")
                    {
                        dHYISAMT = Convert.ToDouble(UP_DotDelete(Convert.ToString((dHLCHQTYHap * dCNBYISSAMT) / 10))) * 10;
                        dHYISVAT = Math.Round(Convert.ToDouble(dHYISAMT) / 10);
                    }
                    else
                    {
                        dHYISAMT = Convert.ToDouble(UP_DotDelete(Convert.ToString((dHLCHQTYHap * dCNBYISSAMT) / 10))) * 10;
                        dHYISVAT = Math.Round(Convert.ToDouble(dHYISAMT) / 11);
                        dHYISAMT = dHYISAMT - dHYISVAT;
                    }
                }

                this.TXT01_HYISAMT.SetValue(Convert.ToString(dHYISAMT));
                this.TXT01_HYISVAT.SetValue(Convert.ToString(dHYISVAT));

                // 하역료 마스터 DATA 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_US_91NAF544",
                    Get_Date(this.DTP01_HYMCYYMM.GetValue().ToString()),
                    this.CBH01_HYHANGCHA.GetValue().ToString(),
                    this.CBH01_HYGOKJONG.GetValue().ToString(),
                    this.CBH01_HYHWAJU.GetValue().ToString(),
                    Get_Date(this.DTP01_HYYYMMDD.GetValue().ToString())
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    // 수정
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_91NBY555", Get_Numeric(this.TXT01_HYBEJNQTY.GetValue().ToString()),
                                                                Get_Numeric(this.TXT01_HYHWAKQTY.GetValue().ToString()),
                                                                Get_Numeric(this.TXT01_HYCHQTY.GetValue().ToString()),
                                                                Get_Numeric(this.TXT01_HYYDQTY.GetValue().ToString()),
                                                                this.TXT01_HYCONTNO.GetValue().ToString(),
                                                                Get_Numeric(this.TXT01_HYHAYKAMT.GetValue().ToString()),
                                                                Get_Numeric(this.TXT01_HYHAYKVAT.GetValue().ToString()),
                                                                Get_Numeric(this.TXT01_HYISAMT.GetValue().ToString()),
                                                                Get_Numeric(this.TXT01_HYISVAT.GetValue().ToString()),
                                                                this.CBO01_HYCRGUBUN.GetValue().ToString(),
                                                                "13",
                                                                TYUserInfo.EmpNo,
                                                                Get_Date(this.DTP01_HYMCYYMM.GetValue().ToString().Trim()),
                                                                this.CBH01_HYHANGCHA.GetValue().ToString().Trim(),
                                                                this.CBH01_HYGOKJONG.GetValue().ToString().Trim(),
                                                                this.CBH01_HYHWAJU.GetValue().ToString().Trim(),
                                                                Get_Date(this.DTP01_HYYYMMDD.GetValue().ToString().Trim())
                                                                );
                    this.DbConnector.ExecuteNonQuery();
                }
                else
                {
                    // 등록
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_91NBY554", Get_Date(this.DTP01_HYMCYYMM.GetValue().ToString().Trim()),
                                                                this.CBH01_HYHANGCHA.GetValue().ToString().Trim(),
                                                                this.CBH01_HYGOKJONG.GetValue().ToString().Trim(),
                                                                this.CBH01_HYHWAJU.GetValue().ToString().Trim(),
                                                                Get_Date(this.DTP01_HYYYMMDD.GetValue().ToString().Trim()),
                                                                Get_Numeric(this.TXT01_HYBEJNQTY.GetValue().ToString()),
                                                                Get_Numeric(this.TXT01_HYHWAKQTY.GetValue().ToString()),
                                                                Get_Numeric(this.TXT01_HYCHQTY.GetValue().ToString()),
                                                                Get_Numeric(this.TXT01_HYYDQTY.GetValue().ToString()),
                                                                this.TXT01_HYCONTNO.GetValue().ToString(),
                                                                Get_Numeric(this.TXT01_HYHAYKAMT.GetValue().ToString()),
                                                                Get_Numeric(this.TXT01_HYHAYKVAT.GetValue().ToString()),
                                                                Get_Numeric(this.TXT01_HYISAMT.GetValue().ToString()),
                                                                Get_Numeric(this.TXT01_HYISVAT.GetValue().ToString()),
                                                                this.CBO01_HYCRGUBUN.GetValue().ToString(),
                                                                "13",
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
            this.FPS91_TY_S_US_91NA9543.Initialize();
            this.CBH01_HLHWAJU.SetValue("");

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_91NAF544",
                Get_Date(this.DTP01_HYMCYYMM.GetValue().ToString()),
                this.CBH01_HYHANGCHA.GetValue().ToString(),
                this.CBH01_HYGOKJONG.GetValue().ToString(),
                this.CBH01_HYHWAJU.GetValue().ToString(),
                Get_Date(this.DTP01_HYYYMMDD.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");

                fsGUBUN = "UPT";

                if (this.TXT01_HYJPNO.GetValue().ToString() != "")
                {
                    fsGUBUN = "";
                }

                UP_Field_ReadOnly(true);
            }
            else
            {
                fsGUBUN = "NEW";
            }

            // 하역료 일자별 출고DATA 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_91NA8542",
                Get_Date(this.DTP01_HYMCYYMM.GetValue().ToString()),
                this.CBH01_HYHANGCHA.GetValue().ToString(),
                this.CBH01_HYGOKJONG.GetValue().ToString(),
                this.CBH01_HYHWAJU.GetValue().ToString(),
                Get_Date(this.DTP01_HYYYMMDD.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();
            
            this.FPS91_TY_S_US_91NA9543.SetValue(dt);

            UP_BTN_DISPLAY(fsGUBUN);

            SetFocus(this.FPS91_TY_S_US_91NA9543);
        }
        #endregion

        #region Description : 처리 ProcessCheck
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_US_91NA9543.GetDataSourceInclude(TSpread.TActionType.New,    "HLDATE", "HLCHQTY", "HLYDQTY"));
            ds.Tables.Add(this.FPS91_TY_S_US_91NA9543.GetDataSourceInclude(TSpread.TActionType.Update, "HLDATE", "HLCHQTY", "HLYDQTY"));
            ds.Tables.Add(this.FPS91_TY_S_US_91NA9543.GetDataSourceInclude(TSpread.TActionType.Remove, "HLDATE"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_MR_2BF4Z352");
                e.Successed = false;
                return;
            }

            if (Get_Date(this.DTP01_HYMCYYMM.GetValue().ToString()) != Get_Date(this.DTP01_HYYYMMDD.GetValue().ToString()).ToString().Substring(0, 6))
            {
                this.ShowMessage("TY_M_US_918HU455");

                SetFocus(this.DTP01_HYMCYYMM);

                e.Successed = false;
                return;
            }

            DataTable dt = new DataTable();

            // 매출발생월 다음달 미수금액이 존재하는지 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_917AH422", Get_Date(this.DTP01_HYMCYYMM.GetValue().ToString()).Substring(0, 6).ToString(),
                                                        "");
            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_US_917AL423");

                SetFocus(this.DTP01_HYMCYYMM);

                e.Successed = false;
                return;
            }

            // SILO 보관료 계약번호 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_918HO453",
                this.CBH01_HYHANGCHA.GetValue().ToString(),
                this.CBH01_HYGOKJONG.GetValue().ToString(),
                this.CBH01_HYHWAJU.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_HYCONTNO.SetValue(dt.Rows[0]["JGCONTNO"].ToString());
            }


            // 계약번호 존재체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_8BJHK186",
                Set_Fill4(this.TXT01_HYCONTNO.GetValue().ToString().Substring(0, 4)),
                Set_Fill2(this.TXT01_HYCONTNO.GetValue().ToString().Substring(4, 2))
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_US_917AY427");

                SetFocus(this.DTP01_HYMCYYMM);

                e.Successed = false;
                return;
            }

            int i = 0;

            // 등록시 하역료 일자별 출고DATA 존재 체크
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[0].Rows.Count; i++) // 신규
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_US_91NBE548",
                        Get_Date(this.DTP01_HYMCYYMM.GetValue().ToString()),
                        this.CBH01_HYHANGCHA.GetValue().ToString(),
                        this.CBH01_HYGOKJONG.GetValue().ToString(),
                        this.CBH01_HYHWAJU.GetValue().ToString(),
                        Get_Date(this.DTP01_HYYYMMDD.GetValue().ToString()),
                        ds.Tables[0].Rows[i]["HLDATE"].ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_US_91NBG549");

                        SetFocus(this.DTP01_HYMCYYMM);

                        e.Successed = false;
                        return;
                    }
                }
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[1].Rows.Count; i++) // 수정
                {
                    // 하역료 일자별 출고DATA 존재 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_US_91NBE548",
                        Get_Date(this.DTP01_HYMCYYMM.GetValue().ToString()),
                        this.CBH01_HYHANGCHA.GetValue().ToString(),
                        this.CBH01_HYGOKJONG.GetValue().ToString(),
                        this.CBH01_HYHWAJU.GetValue().ToString(),
                        Get_Date(this.DTP01_HYYYMMDD.GetValue().ToString()),
                        ds.Tables[1].Rows[i]["HLDATE"].ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_US_91NBH550");

                        SetFocus(this.DTP01_HYMCYYMM);

                        e.Successed = false;
                        return;
                    }
                }
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[2].Rows.Count; i++) // 삭제
                {
                    // 하역료 일자별 출고DATA 존재 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_US_91NBE548",
                        Get_Date(this.DTP01_HYMCYYMM.GetValue().ToString()),
                        this.CBH01_HYHANGCHA.GetValue().ToString(),
                        this.CBH01_HYGOKJONG.GetValue().ToString(),
                        this.CBH01_HYHWAJU.GetValue().ToString(),
                        Get_Date(this.DTP01_HYYYMMDD.GetValue().ToString()),
                        ds.Tables[2].Rows[i]["HLDATE"].ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_US_91NBH550");

                        SetFocus(this.DTP01_HYMCYYMM);

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
        private void FPS91_TY_S_US_91MI4537_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DTP01_HYMCYYMM.SetValue(this.FPS91_TY_S_US_91MI4537.GetValue("HYMCYYMM").ToString());
            this.CBH01_HYHANGCHA.SetValue(this.FPS91_TY_S_US_91MI4537.GetValue("HYHANGCHA").ToString());
            this.CBH01_HYGOKJONG.SetValue(this.FPS91_TY_S_US_91MI4537.GetValue("HYGOKJONG").ToString());
            this.CBH01_HYHWAJU.SetValue(this.FPS91_TY_S_US_91MI4537.GetValue("HYHWAJU").ToString());
            this.DTP01_HYYYMMDD.SetValue(this.FPS91_TY_S_US_91MI4537.GetValue("HYYYMMDD").ToString());
            this.TXT01_HYJPNO.SetValue(this.FPS91_TY_S_US_91MI4537.GetValue("HYJPNO").ToString());

            UP_RUN();
        }
        #endregion

        #region Description : 필드 ReadOnly
        private void UP_Field_ReadOnly(bool boolean)
        {
            this.DTP01_HYMCYYMM.SetReadOnly(boolean);
            this.CBH01_HYHANGCHA.SetReadOnly(boolean);
            this.CBH01_HYGOKJONG.SetReadOnly(boolean);
            this.CBH01_HYHWAJU.SetReadOnly(boolean);
            this.DTP01_HYYYMMDD.SetReadOnly(boolean);

            this.TXT01_HYBEJNQTY.SetReadOnly(boolean);
            this.TXT01_HYHWAKQTY.SetReadOnly(boolean);
            this.TXT01_HYCONTNO.SetReadOnly(boolean);

            this.CBO01_HYCRGUBUN.SetReadOnly(boolean);


            if (fsGUBUN == "UPT")
            {
                if (TXT01_HYJPNO.GetValue().ToString() == "")
                {
                    this.CBH01_HLHWAJU.SetReadOnly(false);
                }
                else
                {
                    this.CBH01_HLHWAJU.SetReadOnly(true);
                }
            }
            else
            {
                this.CBH01_HLHWAJU.SetReadOnly(true);
            }
        }
        #endregion

        #region Description : 버튼 디스플레이
        private void UP_BTN_DISPLAY(string sGUBUN)
        {
            

            if (sGUBUN == "NEW")
            {
                this.BTN61_BATCH.Visible = true;
                this.BTN61_EDIT.Visible = false;
            }
            else if (sGUBUN == "UPT")
            {
                this.BTN61_BATCH.Visible = true;
                if (TXT01_HYJPNO.GetValue().ToString() == "")
                {
                    this.BTN61_EDIT.Visible = true;
                }
                else
                {
                    this.BTN61_EDIT.Visible = false;
                }    
            }
            else
            {
                this.BTN61_BATCH.Visible = false;
                this.BTN61_EDIT.Visible = false;
            }
        }
        #endregion

        #region Description : 필드 클리어
        private void UP_Field_Clear()
        {
            this.CBH01_HYHANGCHA.SetValue("");
            this.CBH01_HYGOKJONG.SetValue("");
            this.CBH01_HYHWAJU.SetValue("");
            
            this.TXT01_HYBEJNQTY.SetValue("");
            this.TXT01_HYHWAKQTY.SetValue("");
            this.TXT01_HYCHQTY.SetValue("");
            this.TXT01_HYYDQTY.SetValue("");
            this.TXT01_HYCONTNO.SetValue("");
            this.TXT01_HYJPNO.SetValue("");

            this.TXT01_HYHAYKAMT.SetValue("");
            this.TXT01_HYHAYKVAT.SetValue("");
            this.TXT01_HYISAMT.SetValue("");
            this.TXT01_HYISVAT.SetValue("");
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

        #region Description : 계약번호 이벤트
        private void BTN61_CONTNO_Click(object sender, EventArgs e)
        {
            TYUSGB005S popup = new TYUSGB005S(Get_Date(this.DTP01_HYYYMMDD.GetValue().ToString()), this.CBH01_HYHWAJU.GetValue().ToString());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_HYCONTNO.SetValue(popup.fsCONTNO);

                this.SetFocus(this.TXT01_HYCONTNO);
            }
        }

        private void TXT01_HYCONTNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                TYUSGB005S popup = new TYUSGB005S(Get_Date(this.DTP01_HYYYMMDD.GetValue().ToString()), this.CBH01_HYHWAJU.GetValue().ToString());

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.TXT01_HYCONTNO.SetValue(popup.fsCONTNO);

                    this.SetFocus(this.TXT01_HYCONTNO);
                }
            }
        }
        #endregion

        #region Description : 청구화주 변경 ProcessCheck
        private void BTN61_EDIT_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            // 전표발행여부 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_91NAF544",
                                    Get_Date(this.DTP01_HYMCYYMM.GetValue().ToString()),
                                    this.CBH01_HYHANGCHA.GetValue().ToString(),
                                    this.CBH01_HYGOKJONG.GetValue().ToString(),
                                    this.CBH01_HYHWAJU.GetValue().ToString(),
                                    Get_Date(this.DTP01_HYYYMMDD.GetValue().ToString())
                                    );
            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["HYJPNO"].ToString() != "")
                {

                    this.ShowMessage("TY_M_MR_3174H522");

                    SetFocus(this.DTP01_HYMCYYMM);

                    e.Successed = false;
                    return;
                }
            }

            // 처리하시겠습니까?
            if (!this.ShowMessage("TY_M_MR_2BF50353"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 청구화주 변경 버튼
        private void BTN61_EDIT_Click(object sender, EventArgs e)
        {
            // 하역료 마스타
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_C17F0992", this.CBH01_HLHWAJU.GetValue().ToString().Trim(),
                                                        Get_Date(this.DTP01_HYMCYYMM.GetValue().ToString().Trim()),
                                                        this.CBH01_HYHANGCHA.GetValue().ToString().Trim(),
                                                        this.CBH01_HYGOKJONG.GetValue().ToString().Trim(),
                                                        this.CBH01_HYHWAJU.GetValue().ToString().Trim(),
                                                        Get_Date(this.DTP01_HYYYMMDD.GetValue().ToString().Trim())
                                                        );
            
            // 하역료 일자별
            this.DbConnector.Attach("TY_P_US_C1ABC993", this.CBH01_HLHWAJU.GetValue().ToString().Trim(),
                                                        Get_Date(this.DTP01_HYMCYYMM.GetValue().ToString().Trim()),
                                                        this.CBH01_HYHANGCHA.GetValue().ToString().Trim(),
                                                        this.CBH01_HYGOKJONG.GetValue().ToString().Trim(),
                                                        this.CBH01_HYHWAJU.GetValue().ToString().Trim(),
                                                        Get_Date(this.DTP01_HYYYMMDD.GetValue().ToString().Trim())
                                                        );
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_MR_2BF50354");

            this.CBH01_HYHWAJU.SetValue(this.CBH01_HLHWAJU.GetValue().ToString());

            UP_RUN();
        }
        #endregion
    }
}