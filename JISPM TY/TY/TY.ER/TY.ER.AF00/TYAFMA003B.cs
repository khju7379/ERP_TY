using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AF00
{
    /// <summary>
    /// EIS 품목별매출현황 생성 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.07.10 15:27
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_26E2Z874 : 생성하시겠습니까?
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    ///  TY_M_GB_26E31876 : 생성 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYAFMA003B : TYBase
    {
        string fsSCSUBGN = string.Empty;

        #region  Description : 폼 로드 이벤트
        public TYAFMA003B(string sSCSUBGN)
        {
            InitializeComponent();
            this.SetPopupStyle();

            fsSCSUBGN = sSCSUBGN;
        }

        private void TYAFMA003B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_GSTYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.SetStartingFocus(this.DTP01_GSTYYMM);
        }
        #endregion

        #region  Description : 생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sGUBUN  = string.Empty;
            string sOUTMSG = string.Empty;

            string sYYMM_AGO = string.Empty;
            string sYEAR     = string.Empty;
            string sMONTH    = string.Empty;

            sYEAR  = this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4);
            sMONTH = this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2);

            if (sMONTH.ToString() == "01")
            {
                sYEAR = Convert.ToString(int.Parse(sYEAR) - 1);
                sMONTH = "12";
            }
            else
            {
                sMONTH = Set_Fill2(Convert.ToString(int.Parse(sMONTH) - 1));
            }

            sYYMM_AGO = sYEAR.ToString() + sMONTH.ToString();


            if (this.CBO01_GGUBUN.GetValue().ToString() == "CREATE")
            {
                if (fsSCSUBGN.ToString() == "TG") // 그레인
                {
                    // 태영그레인 - 생성
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_39B5E676",
                        this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6),
                        sOUTMSG.ToString()
                        );

                    sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                    if (sOUTMSG.ToString().Substring(0, 1) == "I")
                    {
                        UP_TY_ESCUSTMF_UPT();

                        this.ShowMessage("TY_M_GB_26E30875");
                    }
                    else
                    {
                        this.ShowMessage("TY_M_GB_26E31876");
                    }
                }
                else if (fsSCSUBGN.ToString() == "TH") // 호라이즌
                {
                    // 삭제
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_39C42698", "TH", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                    this.DbConnector.ExecuteNonQuery();

                    // 생성
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_39C44699", "TH", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                    this.DbConnector.ExecuteNonQuery();

                    this.ShowMessage("TY_M_GB_26E30875");
                }
                else if (fsSCSUBGN.ToString() == "TS") // GLS
                {
                    // 삭제
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_39G2M768", "TS", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                    this.DbConnector.ExecuteNonQuery();

                    // 생성
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_39G2P769", "TS", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                    this.DbConnector.ExecuteNonQuery();

                    string sESSPUMM = string.Empty;

                    DataTable dt = new DataTable();                    

                    // 품목별 데이터 조회
                    for (int i = 1; i <= 4; i++)
                    {
                        sESSPUMM = Convert.ToString(i) + "000";
                        
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_AC_3B13F158",
                            "TS",
                            this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6),
                            sESSPUMM.ToString()
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count <= 0)
                        {
                            // 주요매출처 등록
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach
                                (
                                "TY_P_AC_3B13H159",
                                "TS",
                                this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6),
                                sESSPUMM.ToString(),
                                "0"
                                );

                            this.DbConnector.ExecuteNonQuery();
                        }
                    }

                    double dESSMAEAMT = 0;
                    double dGITA      = 0;
                    double dB2AMCR    = 0;

                    string sCHA_AMT   = string.Empty;

                    // 총 금액 가져오기
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3B13M163",
                        "TS",
                        this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6)
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        dESSMAEAMT = double.Parse(Get_Numeric(dt.Rows[0]["ESSMAEAMT"].ToString()));
                        dGITA      = double.Parse(Get_Numeric(dt.Rows[0]["GITA"].ToString()));
                    }

                    // 미승인 차변 총금액 가져오기
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3B13Q164",
                        this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6)
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        dB2AMCR = double.Parse(Get_Numeric(dt.Rows[0]["B2AMCR"].ToString()));
                    }

                    if (dESSMAEAMT != dB2AMCR)
                    {
                        sCHA_AMT = string.Format("{0:#,###}", Convert.ToString(dB2AMCR - dESSMAEAMT + dGITA));

                        // 기타에 업데이트
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_AC_3B13V165",
                            sCHA_AMT.ToString(),
                            "TS",
                            this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6),
                            "4000"
                            );

                        this.DbConnector.ExecuteNonQuery();
                    }

                    this.ShowMessage("TY_M_GB_26E30875");
                }
            }
            else
            {
                if (fsSCSUBGN.ToString() == "TG") // 그레인
                {
                    // 태영그레인 - 취소
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_39B5I680", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                    this.DbConnector.ExecuteNonQuery();

                    // 태영그레인 - 기타매출처 삭제
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_3C23J507", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                    this.DbConnector.ExecuteNonQuery();

                    // 태영 - 취소
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_39B5K681", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                    this.DbConnector.ExecuteNonQuery();

                    // 태영 - 기타매출처 삭제
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_39H43808", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                    this.DbConnector.ExecuteNonQuery();
                }
                else if (fsSCSUBGN.ToString() == "TH") // 호라이즌
                {
                    // 삭제
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_39C42698", "TH",this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                    this.DbConnector.ExecuteNonQuery();
                }
                else if (fsSCSUBGN.ToString() == "TS") // 호라이즌
                {
                    // 삭제
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_39G2M768", "TS", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                    this.DbConnector.ExecuteNonQuery();
                }

                this.ShowMessage("TY_M_AC_2CDB1167");
            }

            this.DTP01_GSTYYMM.Focus();
        }
        #endregion

        #region Description : 태영 ESCUSTMF 업데이트
        private void UP_TY_ESCUSTMF_UPT()
        {
            int i = 0;

            // 태영-전체삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_39B5T684", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
            this.DbConnector.ExecuteNonQuery();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_39B5Q683", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_39B5V685",
                        dt.Rows[i]["ESCSUBGN"].ToString(),
                        dt.Rows[i]["ESCVEND"].ToString(),
                        dt.Rows[i]["ESCYYMM"].ToString(),
                        dt.Rows[i]["ESCHWAMUL1"].ToString(),
                        dt.Rows[i]["ESCHWAMUL2"].ToString(),
                        dt.Rows[i]["ESCHWAMUL3"].ToString(),
                        dt.Rows[i]["ESCVOLME"].ToString(),
                        dt.Rows[i]["ESCMAEAMT"].ToString(),
                        dt.Rows[i]["ESCMAEVN1"].ToString(),
                        dt.Rows[i]["ESCMAEVN2"].ToString(),
                        dt.Rows[i]["ESCMAEVN3"].ToString(),
                        dt.Rows[i]["ESCMAEVN4"].ToString(),
                        dt.Rows[i]["ESCMAEVN5"].ToString(),
                        dt.Rows[i]["ESCMAEVN6"].ToString(),
                        dt.Rows[i]["ESCMAEVN7"].ToString(),
                        dt.Rows[i]["ESCMAEVN8"].ToString(),
                        dt.Rows[i]["ESCMAEVN9"].ToString(),
                        dt.Rows[i]["ESCMAEVNT"].ToString()
                        );
                }
                this.DbConnector.ExecuteTranQueryList();
            }
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 처리 체크
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.CBO01_GGUBUN.GetValue().ToString() == "CREATE")
            {
                //DataTable dt = new DataTable();

                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach
                //    (
                //    "TY_P_AC_39B5H679",
                //    this.DTP01_GSTYYMM.GetValue().ToString()
                //    );

                //dt = this.DbConnector.ExecuteDataTable();

                //if (dt.Rows.Count > 0)
                //{
                //    this.ShowMessage("TY_M_AC_3871Y362");
                //    e.Successed = false;
                //    return;
                //}

                string sYYMM_AGO = string.Empty;
                string sYEAR     = string.Empty;
                string sMONTH    = string.Empty;

                sYEAR  = this.DTP01_GSTYYMM.GetValue().ToString().Substring(0,4);
                sMONTH = this.DTP01_GSTYYMM.GetValue().ToString().Substring(4,2);

                if (sMONTH.ToString() == "01")
                {
                    sYEAR = Convert.ToString(int.Parse(sYEAR) - 1);
                    sMONTH = "12";
                }
                else
                {
                    sMONTH = Set_Fill2(Convert.ToString(int.Parse(sMONTH) - 1));
                }

                sYYMM_AGO = sYEAR.ToString() + sMONTH.ToString();

                //// 전월자료 존재유무 체크
                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach
                //    (
                //    "TY_P_AC_3873S366",
                //    sYYMM_AGO.ToString()
                //    );

                //dt = this.DbConnector.ExecuteDataTable();

                //if (dt.Rows.Count <= 0)
                //{
                //    this.ShowMessage("TY_M_AC_3873V367");
                //    e.Successed = false;
                //    return;
                //}
            }

            if (!this.ShowMessage("TY_M_GB_26E2Z874"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion
    }
}