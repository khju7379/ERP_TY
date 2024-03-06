using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.UT00
{
    /// <summary>
    /// 고객정보 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.06.24 14:32
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_66OEJ389 : 고객정보 관리
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_66OEM390 : 고객정보 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_AC_25F59464 : 선택한 자료가 없습니다.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    ///  TY_M_GB_2BF7Y364 : 조회가 완료되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  VNSANGHO : 거래처명
    /// </summary>
    public partial class TYUTIN026I : TYBase
    {
        public TYUTIN026I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_66OEM390, "EMHWAJU", "VNSANGHO", "EMHWAJU");

            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_66OEM390, "EMSABUN", "KBHANGL", "EMSABUN");
        }

        #region Description : 페이지 로드
        private void TYUTIN026I_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_66OEM390, "EMUSERID");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.TXT01_VNSANGHO);
        }
        #endregion

        #region Description : 조회버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_66OEM390.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_UT_66OEJ389", this.TXT01_VNSANGHO.GetValue().ToString(),
                                                        "");

            this.FPS91_TY_S_UT_66OEM390.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 저장버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;



            if (ds.Tables[0].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();

                // 신규등록
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_UT_88EAA548", ds.Tables[0].Rows[i]["EMUSERID"].ToString(),
                                                                ds.Tables[0].Rows[i]["EMPASSWD"].ToString(),
                                                                ds.Tables[0].Rows[i]["EMUSNAME"].ToString(),
                                                                ds.Tables[0].Rows[i]["EMTELNUM"].ToString(),
                                                                ds.Tables[0].Rows[i]["EMEMAIL"].ToString(),
                                                                ds.Tables[0].Rows[i]["EMIRUM"].ToString(),
                                                                ds.Tables[0].Rows[i]["EMISANGHO"].ToString(),
                                                                ds.Tables[0].Rows[i]["EMSAUPNO"].ToString(),
                                                                ds.Tables[0].Rows[i]["EMADDRS"].ToString(),
                                                                ds.Tables[0].Rows[i]["EMHWAJU"].ToString(),
                                                                ds.Tables[0].Rows[i]["EMPASGB"].ToString(),
                                                                "T",
                                                                ds.Tables[0].Rows[i]["EMNAME"].ToString(),
                                                                ds.Tables[0].Rows[i]["EMPHONE"].ToString().Replace("-", ""),
                                                                "",
                                                                ds.Tables[0].Rows[i]["EMDELIVERY"].ToString(),
                                                                ds.Tables[0].Rows[i]["EMSABUN"].ToString(),
                                                                ds.Tables[0].Rows[i]["EMDYTEL"].ToString().Replace("-", ""),
                                                                TYUserInfo.EmpNo
                                                                );
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();

                // 수정
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_UT_88EAB549",
                                                                ds.Tables[1].Rows[i]["EMPASSWD"].ToString(),
                                                                ds.Tables[1].Rows[i]["EMUSNAME"].ToString(),
                                                                ds.Tables[1].Rows[i]["EMTELNUM"].ToString(),
                                                                ds.Tables[1].Rows[i]["EMEMAIL"].ToString(),
                                                                ds.Tables[1].Rows[i]["EMIRUM"].ToString(),
                                                                ds.Tables[1].Rows[i]["EMISANGHO"].ToString(),
                                                                ds.Tables[1].Rows[i]["EMSAUPNO"].ToString(),
                                                                ds.Tables[1].Rows[i]["EMADDRS"].ToString(),
                                                                ds.Tables[1].Rows[i]["EMHWAJU"].ToString(),
                                                                ds.Tables[1].Rows[i]["EMPASGB"].ToString(),
                                                                ds.Tables[1].Rows[i]["EMGUBUN"].ToString(),
                                                                ds.Tables[1].Rows[i]["EMNAME"].ToString(),
                                                                ds.Tables[1].Rows[i]["EMPHONE"].ToString().Replace("-", ""),
                                                                ds.Tables[1].Rows[i]["EMORDNAME"].ToString(),
                                                                ds.Tables[1].Rows[i]["EMDELIVERY"].ToString(),
                                                                ds.Tables[1].Rows[i]["EMSABUN"].ToString(),
                                                                ds.Tables[1].Rows[i]["EMDYTEL"].ToString().Replace("-", ""),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["EMUSERID"].ToString()
                                                                );
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 삭제버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_66OGY399", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            int iVNPERCNT = 0;
            int iCNT = 0;

            ds.Tables.Add(this.FPS91_TY_S_UT_66OEM390.GetDataSourceInclude(TSpread.TActionType.New, "EMUSERID", "EMPASSWD", "EMUSNAME", "EMTELNUM", "EMEMAIL", "EMIRUM",
                                                                                                    "EMISANGHO", "EMSAUPNO", "EMADDRS", "EMHWAJU", "EMPASGB", "EMGUBUN",
                                                                                                    "EMNAME", "EMPHONE", "EMORDNAME", "EMDELIVERY", "EMSABUN", "EMDYTEL",
                                                                                                    "EMHWAJUOLD2", "EMPASGBOLD"));

            ds.Tables.Add(this.FPS91_TY_S_UT_66OEM390.GetDataSourceInclude(TSpread.TActionType.Update, "EMUSERID", "EMPASSWD", "EMUSNAME", "EMTELNUM", "EMEMAIL", "EMIRUM",
                                                                                                       "EMISANGHO", "EMSAUPNO", "EMADDRS", "EMHWAJU", "EMPASGB", "EMGUBUN",
                                                                                                       "EMNAME", "EMPHONE", "EMORDNAME", "EMDELIVERY", "EMSABUN", "EMDYTEL",
                                                                                                       "EMHWAJUOLD2", "EMPASGBOLD"));


            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            

            // 신규등록
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_66TFK461", ds.Tables[0].Rows[i]["EMUSERID"].ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowCustomMessage("[" + ds.Tables[0].Rows[i]["EMUSERID"].ToString() + "] 거래처코드가 이미 등록되어 있습니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                    e.Successed = false;
                    return;
                }

                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_7AGI0802", ds.Tables[0].Rows[i]["EMSAUPNO"].ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowCustomMessage("[" + ds.Tables[0].Rows[i]["EMSAUPNO"].ToString() + "] 거래처 관리에 등록되지 않은 사업자 번호입니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                    e.Successed = false;
                    return;
                }

                // ID 발급제한 체크
                if (ds.Tables[0].Rows[i]["EMHWAJU"].ToString() != "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_BA7G9610", ds.Tables[0].Rows[i]["EMHWAJU"].ToString());
                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        // 발급가능수
                        iVNPERCNT = Convert.ToInt32(Get_Numeric(dt.Rows[0]["VNPERCNT"].ToString()));
                    }

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_BA7EY604", "T", ds.Tables[0].Rows[i]["EMHWAJU"].ToString());
                    dt = this.DbConnector.ExecuteDataTable();

                    // 현재발급수
                    iCNT = Convert.ToInt32(this.DbConnector.ExecuteScalar());

                    for (int x = 0 ; x < ds.Tables[0].Rows.Count ; x++)
                    {
                        // 신규등록개수
                        if (ds.Tables[0].Rows[i]["EMHWAJU"].ToString() == ds.Tables[0].Rows[x]["EMHWAJU"].ToString())
                        {
                            if (ds.Tables[0].Rows[x]["EMPASGB"].ToString() == "Y")
                            {
                                iCNT++;
                            }
                        }
                    }
                    for (int y = 0; y < ds.Tables[1].Rows.Count; y++)
                    {
                        if (ds.Tables[1].Rows[y]["EMHWAJU"].ToString() != ds.Tables[1].Rows[y]["EMHWAJUOLD2"].ToString())
                        {
                            if (ds.Tables[0].Rows[i]["EMHWAJU"].ToString() == ds.Tables[1].Rows[y]["EMHWAJU"].ToString())
                            {
                                // 수정자료 중 화주변경된 개수 증가
                                iCNT++;
                            }
                            else if (ds.Tables[0].Rows[i]["EMHWAJU"].ToString() == ds.Tables[1].Rows[y]["EMHWAJUOLD2"].ToString())
                            {
                                // 수정자료 중 화주변경된 개수 감소
                                iCNT--;
                            }
                        }
                        else
                        {
                            if (ds.Tables[1].Rows[y]["EMPASGB"].ToString() != ds.Tables[1].Rows[y]["EMPASGBOLD"].ToString())
                            {
                                if (ds.Tables[0].Rows[i]["EMHWAJU"].ToString() == ds.Tables[1].Rows[y]["EMHWAJU"].ToString())
                                {
                                    if (ds.Tables[1].Rows[y]["EMPASGB"].ToString() == "Y")
                                    {
                                        // 수정자료 중 사용구분변경된 개수 증가
                                        iCNT++;
                                    }
                                    else
                                    {
                                        // 수정자료 중 사용구분변경된 개수 감소
                                        iCNT--;
                                    }
                                }
                            }
                        }
                    }

                    if (iCNT > iVNPERCNT)
                    {
                        this.ShowCustomMessage("[" + ds.Tables[0].Rows[i]["EMHWAJU"].ToString() + "][발급:" + iCNT + "/제한:" + iVNPERCNT + "] 해당화주의 고객정보 ID 발급 개수를 초과하였습니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                }
                
            }

            // 수정
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                if (ds.Tables[1].Rows[i]["EMHWAJU"].ToString() != "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_BA7G9610", ds.Tables[1].Rows[i]["EMHWAJU"].ToString());
                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        // 발급가능수
                        iVNPERCNT = Convert.ToInt32(Get_Numeric(dt.Rows[0]["VNPERCNT"].ToString()));
                    }

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_BA7EY604", "T", ds.Tables[1].Rows[i]["EMHWAJU"].ToString());
                    dt = this.DbConnector.ExecuteDataTable();

                    // 현재발급수
                    iCNT = Convert.ToInt32(this.DbConnector.ExecuteScalar());

                    for (int y = 0; y < ds.Tables[1].Rows.Count; y++)
                    {
                        if (ds.Tables[1].Rows[y]["EMHWAJU"].ToString() != ds.Tables[1].Rows[y]["EMHWAJUOLD2"].ToString())
                        {
                            if (ds.Tables[1].Rows[i]["EMHWAJU"].ToString() == ds.Tables[1].Rows[y]["EMHWAJU"].ToString())
                            {
                                if (ds.Tables[1].Rows[y]["EMPASGB"].ToString() == "Y")
                                {
                                    // 수정자료 중 화주변경된 개수 증가
                                    iCNT++;
                                }
                            }
                            else if (ds.Tables[1].Rows[i]["EMHWAJU"].ToString() == ds.Tables[1].Rows[y]["EMHWAJUOLD2"].ToString())
                            {
                                if (ds.Tables[1].Rows[y]["EMPASGB"].ToString() == "Y")
                                {
                                    // 수정자료 중 화주변경된 개수 감소
                                    iCNT--;
                                }
                            }
                        }
                        else
                        {
                            if (ds.Tables[1].Rows[y]["EMPASGB"].ToString() != ds.Tables[1].Rows[y]["EMPASGBOLD"].ToString())
                            {
                                if (ds.Tables[1].Rows[i]["EMHWAJU"].ToString() == ds.Tables[1].Rows[y]["EMHWAJU"].ToString())
                                {
                                    if (ds.Tables[1].Rows[y]["EMPASGB"].ToString() == "Y")
                                    {
                                        // 수정자료 중 사용구분변경된 개수 증가
                                        iCNT++;
                                    }
                                    else
                                    {
                                        // 수정자료 중 사용구분변경된 개수 감소
                                        iCNT--;
                                    }
                                }
                            }
                        }
                    }


                    if (iCNT > iVNPERCNT)
                    {
                        this.ShowCustomMessage("[" + ds.Tables[1].Rows[i]["EMHWAJU"].ToString() + "][발급:" + iCNT + "/제한:" + iVNPERCNT + "] 해당화주의 고객정보 ID 발급 개수를 초과하였습니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                }
            }

            e.ArgData = ds;

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_UT_66OEM390.GetDataSourceInclude(TSpread.TActionType.Remove, "EMUSERID");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;

        }
        #endregion

        private void FPS91_TY_S_UT_66OEM390_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_UT_66OEM390.SetValue("EMPASGB", "N");
            this.FPS91_TY_S_UT_66OEM390.SetValue("EMDELIVERY", "N");
        }

        private void FPS91_TY_S_UT_66OEM390_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            string sEMHWAJU = this.FPS91_TY_S_UT_66OEM390.GetValue("EMHWAJU").ToString();
            string sEMHWAJUOLD = this.FPS91_TY_S_UT_66OEM390.GetValue("EMHWAJUOLD").ToString();

            if (sEMHWAJU.Length == 3)
            {
                if (sEMHWAJUOLD != sEMHWAJU)
                {
                    this.FPS91_TY_S_UT_66OEM390.SetValue("EMHWAJUOLD", sEMHWAJU);

                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_UT_66DFG160", sEMHWAJU);

                    DataTable dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.FPS91_TY_S_UT_66OEM390.SetValue("EMSAUPNO", dt.Rows[0]["VNSAUPNO"].ToString());    // 사업자번호
                        this.FPS91_TY_S_UT_66OEM390.SetValue("EMIRUM", dt.Rows[0]["VNIRUM"].ToString());        // 대표자
                        if (dt.Rows[0]["VNNEWADD"].ToString() != "")
                        {
                            this.FPS91_TY_S_UT_66OEM390.SetValue("EMADDRS", dt.Rows[0]["VNNEWADD"].ToString()); // 도로명 주소
                        }
                        else
                        {
                            this.FPS91_TY_S_UT_66OEM390.SetValue("EMADDRS", dt.Rows[0]["VNJUSO"].ToString());   // 주소
                        }
                        this.FPS91_TY_S_UT_66OEM390.SetValue("EMNAME", dt.Rows[0]["VNHYNAME"].ToString());      // 담당자 (하역료)
                        this.FPS91_TY_S_UT_66OEM390.SetValue("EMDYTEL", dt.Rows[0]["VNHYTEL"].ToString());      // 담당TEL (하역료)
                        this.FPS91_TY_S_UT_66OEM390.SetValue("EMEMAIL", dt.Rows[0]["VNHYMAIL"].ToString());     // E-Mail (하역료)
                    }
                }
            }
        }
    }
}
