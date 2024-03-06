using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using GrapeCity.ActiveReports;
using TY.ER.GB00;
using TY.ER.AC00;

namespace TY.ER.US00
{
    /// <summary>
    /// 하역료 단가 조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2016.06.08 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_7
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  CHYMDATE : 기준일자
    ///  CHYMSEQ : 순번
    /// </summary>
    public partial class TYUSME001B : TYBase
    {
        #region Descriptino : 페이지 로드
        public TYUSME001B()
        {
            InitializeComponent();
        }

        private void TYUSME001B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_CREATE_ProcessCheck);

            this.MTB01_GDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.MTB01_STDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.MTB01_EDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.MTB01_GDATE);
        }
        #endregion

        #region Description : 하역료 매출생성 버튼
        private void BTN61_CREATE_Click(object sender, EventArgs e)
        {
            string sOUTMSG = string.Empty;

            // 보관료 매출 SP 수행
            if (this.CBO01_GMEGUBUN.GetText().ToString() == "보관료")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_8BQDF241", Get_Date(this.MTB01_GDATE.GetValue().ToString()),
                                                            this.CBH01_STHANGCHA.GetValue().ToString(),
                                                            this.CBH01_EDHANGCHA.GetValue().ToString(),
                                                            this.CBH01_GGOKJONG.GetValue().ToString(),
                                                            this.CBH01_GHWAJU.GetValue().ToString(),
                                                            Get_Date(this.MTB01_STDATE.GetValue().ToString()),
                                                            Get_Date(this.MTB01_EDDATE.GetValue().ToString()),
                                                            this.CBO01_GGUBUN.GetValue().ToString(),
                                                            TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                                            sOUTMSG.ToString()
                                                            );

                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
            }
            else if (this.CBO01_GMEGUBUN.GetText().ToString() == "하역료")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_8C6B1277", Get_Date(this.MTB01_GDATE.GetValue().ToString()),
                                                            this.CBH01_STHANGCHA.GetValue().ToString(),
                                                            this.CBH01_EDHANGCHA.GetValue().ToString(),
                                                            this.CBH01_GGOKJONG.GetValue().ToString(),
                                                            this.CBH01_GHWAJU.GetValue().ToString(),
                                                            Get_Date(this.MTB01_STDATE.GetValue().ToString()),
                                                            Get_Date(this.MTB01_EDDATE.GetValue().ToString()),
                                                            this.CBO01_GGUBUN.GetValue().ToString(),
                                                            TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                                            this.CBO01_GHYGUBUN.GetValue().ToString(),
                                                            sOUTMSG.ToString()
                                                            );

                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
            }
            else if (this.CBO01_GMEGUBUN.GetText().ToString() == "이송료")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_91H90522", Get_Date(this.MTB01_GDATE.GetValue().ToString()),
                                                            this.CBH01_STHANGCHA.GetValue().ToString(),
                                                            this.CBH01_EDHANGCHA.GetValue().ToString(),
                                                            this.CBH01_GGOKJONG.GetValue().ToString(),
                                                            this.CBH01_GHWAJU.GetValue().ToString(),
                                                            Get_Date(this.MTB01_STDATE.GetValue().ToString()),
                                                            Get_Date(this.MTB01_EDDATE.GetValue().ToString()),
                                                            this.CBO01_GGUBUN.GetValue().ToString(),
                                                            TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                                            sOUTMSG.ToString()
                                                            );

                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
            }
            else if (this.CBO01_GMEGUBUN.GetText().ToString() == "시설사용료")
            {
                if (this.CBO01_GGUBUN.GetValue().ToString() == "A") // 생성
                {
                    UP_SISUL_Create();
                }
                else // 취소
                {
                    UP_SISUL_Cancel();
                }
            }
            else if (this.CBO01_GMEGUBUN.GetText().ToString() == "조출료")
            {
                if (this.CBO01_GGUBUN.GetValue().ToString() == "A") // 생성
                {
                    UP_USIMCLMF_Create();
                }
                else // 취소
                {
                    UP_USIMCLMF_Cancel();
                }
            }

            if (this.CBO01_GGUBUN.GetValue().ToString() == "A")
            {
                if (this.CBO01_GMEGUBUN.GetText().ToString() == "보관료" || this.CBO01_GMEGUBUN.GetText().ToString() == "하역료" || this.CBO01_GMEGUBUN.GetText().ToString() == "이송료")
                {
                    if (sOUTMSG.Substring(0, 2) == "OK")
                    {
                        if (MessageBox.Show("매출 생성 작업이 완료 되었습니다. 같은 항차를 생성하시겠습니까?", "확인", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            SetFocus(this.CBH01_GHWAJU.CodeText);
                        }
                        else
                        {
                            SetFocus(this.CBH01_STHANGCHA.CodeText);
                        }
                    }
                    else
                    {
                        this.ShowMessage("TY_M_US_917BX436"); // 저장 메세지

                        SetFocus(this.CBH01_GHWAJU.CodeText);
                    }
                }
                else
                {
                    this.ShowMessage("TY_M_US_917BV435"); // 저장 메세지

                    SetFocus(this.CBH01_GHWAJU.CodeText);
                }
            }
            else
            {
                if (this.CBO01_GMEGUBUN.GetText().ToString() == "보관료" || this.CBO01_GMEGUBUN.GetText().ToString() == "하역료" || this.CBO01_GMEGUBUN.GetText().ToString() == "이송료")
                {
                    if (sOUTMSG.Substring(0, 2) == "OK")
                    {
                        this.ShowMessage("TY_M_US_917BY437"); // 저장 메세지
                    }
                    else
                    {
                        this.ShowMessage("TY_M_US_917BY438"); // 저장 메세지
                    }
                }
                else
                {
                    this.ShowMessage("TY_M_US_917BY437"); // 저장 메세지
                }

                SetFocus(this.CBH01_GHWAJU.CodeText);
            }
        }
        #endregion

        #region Description : 시설사용료 생성
        private void UP_SISUL_Create()
        {
            string sUSSEQ = string.Empty;
            string sGUBUN = string.Empty;
            string sUSHWAJU = string.Empty;

            int i = 0;

            sUSHWAJU = "";

            DataTable dt = new DataTable();

            // 화주별 생성
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_91BEE483",
                this.CBH01_STHANGCHA.GetValue().ToString(),
                this.CBH01_EDHANGCHA.GetValue().ToString(),
                this.CBH01_GGOKJONG.GetValue().ToString(),
                this.CBH01_GHWAJU.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    if (sUSHWAJU == "")
                    {
                        sUSHWAJU = "" + SetDefaultValue(dt.Rows[i][0].ToString()).Trim() + "";
                    }
                    else
                    {
                        sUSHWAJU = sUSHWAJU + "," + SetDefaultValue(dt.Rows[i][0].ToString()).Trim() + "";
                    }
                }
            }

            if (sUSHWAJU != "")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_91BEM484", Get_Date(this.MTB01_GDATE.GetValue().ToString()).Substring(0, 6).ToString(),
                                                            Get_Date(this.MTB01_GDATE.GetValue().ToString()),
                                                            TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                                            this.CBH01_STHANGCHA.GetValue().ToString(),
                                                            this.CBH01_EDHANGCHA.GetValue().ToString(),
                                                            this.CBH01_GGOKJONG.GetValue().ToString(),
                                                            sUSHWAJU.ToString()
                                                            );

                this.DbConnector.ExecuteNonQuery();
            }

            sUSHWAJU = "";
            sUSSEQ = "";

            DataTable dtBL = new DataTable();
            DataTable dtUS = new DataTable();

            // B/L분할별로 생성
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_91FGA507",
                this.CBH01_STHANGCHA.GetValue().ToString(),
                this.CBH01_EDHANGCHA.GetValue().ToString(),
                this.CBH01_GGOKJONG.GetValue().ToString(),
                this.CBH01_GHWAJU.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_US_91FGD508",
                        SetDefaultValue(dt.Rows[i][0].ToString()).Trim(),
                        SetDefaultValue(dt.Rows[i][1].ToString()).Trim(),
                        SetDefaultValue(dt.Rows[i][2].ToString()).Trim()
                        );

                    dtBL = this.DbConnector.ExecuteDataTable();

                    if (dtBL.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtBL.Rows.Count; j++)
                        {
                            // 년월, 매출일자, 항차, 곡종, 화주에 따른 순번 가져오기
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach
                                (
                                "TY_P_US_91FGG509",
                                Get_Date(this.MTB01_GDATE.GetValue().ToString()).Substring(0, 6).ToString(),
                                Get_Date(this.MTB01_GDATE.GetValue().ToString()),
                                SetDefaultValue(dt.Rows[i][0].ToString()).Trim(),
                                SetDefaultValue(dt.Rows[i][1].ToString()).Trim(),
                                SetDefaultValue(dt.Rows[i][2].ToString()).Trim()
                                );

                            dtUS = this.DbConnector.ExecuteDataTable();

                            if (dtUS.Rows.Count > 0)
                            {
                                sUSSEQ = SetDefaultValue(dtUS.Rows[0][0].ToString()).Trim();
                            }

                            if (sGUBUN == "")
                            {
                                this.DbConnector.CommandClear();
                                this.DbConnector.Attach("TY_P_US_91FGK510", Get_Date(this.MTB01_GDATE.GetValue().ToString()).Substring(0, 6).ToString(),
                                                                            SetDefaultValue(dtBL.Rows[j][0].ToString()).Trim(),
                                                                            SetDefaultValue(dtBL.Rows[j][1].ToString()).Trim(),
                                                                            SetDefaultValue(dtBL.Rows[j][2].ToString()).Trim(),
                                                                            sUSSEQ.ToString(),
                                                                            Get_Date(this.MTB01_GDATE.GetValue().ToString()),
                                                                            SetDefaultValue(dtBL.Rows[j][3].ToString()).Trim(),
                                                                            SetDefaultValue(dtBL.Rows[j][4].ToString()).Trim(),
                                                                            SetDefaultValue(dtBL.Rows[j][5].ToString()).Trim(),
                                                                            SetDefaultValue(dtBL.Rows[j][6].ToString()).Trim(),
                                                                            SetDefaultValue(dtBL.Rows[j][7].ToString()).Trim(),
                                                                            "12",
                                                                            TYUserInfo.EmpNo.ToString().Trim().ToUpper()
                                                                            );

                                this.DbConnector.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Description : 시설사용료 취소
        private void UP_SISUL_Cancel()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_91BE5482", Get_Date(this.MTB01_GDATE.GetValue().ToString()).Substring(0, 6).ToString(),
                                                        Get_Date(this.MTB01_GDATE.GetValue().ToString()),
                                                        this.CBH01_STHANGCHA.GetValue().ToString(),
                                                        this.CBH01_EDHANGCHA.GetValue().ToString(),
                                                        this.CBH01_GGOKJONG.GetValue().ToString(),
                                                        this.CBH01_GHWAJU.GetValue().ToString()
                                                        );

            this.DbConnector.ExecuteTranQueryList();

        }
        #endregion

        #region Description : 조출료 생성
        private void UP_USIMCLMF_Create()
        {
            string sJGHWAJU = string.Empty;
            string sJGHANGCHA = string.Empty;

            int i = 0;
            int j = 0;

            sJGHWAJU = "";

            DataTable dt = new DataTable();
            DataTable dt_lm = new DataTable();

            // 화주별 생성
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_US_9BIE9519",
                this.CBH01_STHANGCHA.GetValue().ToString(),
                this.CBH01_EDHANGCHA.GetValue().ToString(),
                this.CBH01_GGOKJONG.GetValue().ToString(),
                this.CBH01_GHWAJU.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    sJGHANGCHA = SetDefaultValue(dt.Rows[i]["JGHANGCHA"].ToString()).Trim();
                    sJGHWAJU = SetDefaultValue(dt.Rows[i]["JGHWAJU"].ToString()).Trim();

                    // 조출료 생성 데이터 가져오기

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_US_91PBP596",
                        sJGHANGCHA.ToString(),
                        sJGHWAJU.ToString(),
                        Get_Date(this.MTB01_GDATE.GetValue().ToString()).Substring(0, 6).ToString(),
                        Get_Date(this.MTB01_GDATE.GetValue().ToString()),
                        TYUserInfo.EmpNo.ToString().Trim().ToUpper()
                        );

                    dt_lm = this.DbConnector.ExecuteDataTable();

                    if (dt_lm.Rows.Count > 0)
                    {
                        for (j = 0; j < dt_lm.Rows.Count; j++)
                        {
                            // 조출료 생성
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_US_91PBT598", dt_lm.Rows[j]["LMMCYYMM"].ToString(),
                                                                        dt_lm.Rows[j]["LMHANGCHA"].ToString(),
                                                                        dt_lm.Rows[j]["LMGOKJONG"].ToString(),
                                                                        dt_lm.Rows[j]["LMHWAJU"].ToString(),
                                                                        dt_lm.Rows[j]["LMYYMMDD"].ToString(),
                                                                        dt_lm.Rows[j]["LMBEJNQTY"].ToString(),
                                                                        dt_lm.Rows[j]["LMHJGONG"].ToString(),
                                                                        dt_lm.Rows[j]["LMHJVAT"].ToString(),
                                                                        dt_lm.Rows[j]["LMHJAMT"].ToString(),
                                                                        dt_lm.Rows[j]["LMMAECH"].ToString(),
                                                                        dt_lm.Rows[j]["LMGUBUN"].ToString(),
                                                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper()
                                                                        );

                            this.DbConnector.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
        #endregion

        #region Description : 조출료 취소
        private void UP_USIMCLMF_Cancel()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_91P93595", Get_Date(this.MTB01_GDATE.GetValue().ToString()).Substring(0, 6).ToString(),
                                                        Get_Date(this.MTB01_GDATE.GetValue().ToString()),
                                                        this.CBH01_STHANGCHA.GetValue().ToString(),
                                                        this.CBH01_EDHANGCHA.GetValue().ToString(),
                                                        this.CBH01_GGOKJONG.GetValue().ToString(),
                                                        this.CBH01_GHWAJU.GetValue().ToString()
                                                        );

            this.DbConnector.ExecuteTranQueryList();

        }
        #endregion


        #region Description : 매출생성 ProcessCheck
        private void BTN61_CREATE_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            if (this.CBO01_GMEGUBUN.GetText().ToString() == "보관료" || this.CBO01_GMEGUBUN.GetText().ToString() == "하역료")
            {
                if (Get_Date(this.MTB01_STDATE.GetValue().ToString().Trim()) == "")
                {
                    this.ShowMessage("TY_M_US_9179A415");

                    SetFocus(this.MTB01_STDATE);

                    e.Successed = false;
                    return;
                }

                if (Get_Date(this.MTB01_EDDATE.GetValue().ToString().Trim()) == "")
                {
                    this.ShowMessage("TY_M_US_9179A416");

                    SetFocus(this.MTB01_EDDATE);

                    e.Successed = false;
                    return;
                }

                // 출고일자
                if (int.Parse(Get_Date(this.MTB01_STDATE.GetValue().ToString().Trim())) > int.Parse(Get_Date(this.MTB01_EDDATE.GetValue().ToString().Trim())))
                {
                    this.ShowMessage("TY_M_US_9179D417");

                    SetFocus(this.MTB01_STDATE);

                    e.Successed = false;
                    return;
                }
            }

            // 매출발생월 이후의 미수금 존재 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_917AH422", Get_Date(this.MTB01_GDATE.GetValue().ToString()).Substring(0, 6).ToString(),
                                                        ""
                                                        );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_US_917AL423");

                SetFocus(this.MTB01_GDATE);

                e.Successed = false;
                return;
            }

            if (this.CBO01_GMEGUBUN.GetText().ToString() == "시설사용료")
            {
                if (this.CBO01_GGUBUN.GetText().ToString() == "생성")
                {
                    // 시설사용료 매출 데이터 존재 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_917B2430", this.CBH01_GGOKJONG.GetValue().ToString(),
                                                                this.CBH01_GHWAJU.GetValue().ToString(),
                                                                this.CBH01_STHANGCHA.GetValue().ToString(),
                                                                this.CBH01_EDHANGCHA.GetValue().ToString(),
                                                                Get_Date(this.MTB01_GDATE.GetValue().ToString())
                                                                );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_US_917AC419");

                        SetFocus(this.MTB01_GDATE);

                        e.Successed = false;
                        return;
                    }

                    // 시설사용료 매출생성시 입항파일 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_917AZ429", this.CBH01_GGOKJONG.GetValue().ToString(),
                                                                this.CBH01_GHWAJU.GetValue().ToString(),
                                                                this.CBH01_STHANGCHA.GetValue().ToString(),
                                                                this.CBH01_EDHANGCHA.GetValue().ToString()
                                                                );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_US_917AP425");

                        SetFocus(this.MTB01_GDATE);

                        e.Successed = false;
                        return;
                    }

                    // 시설사용료 및 하역료 매출생성시 계약파일 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_917AY426", this.CBH01_GGOKJONG.GetValue().ToString(),
                                                                this.CBH01_GHWAJU.GetValue().ToString(),
                                                                this.CBH01_STHANGCHA.GetValue().ToString(),
                                                                this.CBH01_EDHANGCHA.GetValue().ToString()
                                                                );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_US_917AY427");

                        SetFocus(this.MTB01_GDATE);

                        e.Successed = false;
                        return;
                    }
                }
                else // 취소
                {
                    // 시설사용료 매출 데이터 존재 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_917B6432", this.CBH01_GGOKJONG.GetValue().ToString(),
                                                                this.CBH01_GHWAJU.GetValue().ToString(),
                                                                this.CBH01_STHANGCHA.GetValue().ToString(),
                                                                this.CBH01_EDHANGCHA.GetValue().ToString(),
                                                                Get_Date(this.MTB01_GDATE.GetValue().ToString())
                                                                );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_US_917AF421");

                        SetFocus(this.MTB01_GDATE);

                        e.Successed = false;
                        return;
                    }
                }
            }
            else if (this.CBO01_GMEGUBUN.GetText().ToString() == "하역료")
            {
                if (this.CBO01_GGUBUN.GetText().ToString() == "생성")
                {
                    // 하역료 매출 데이터 존재 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_917A1418", this.CBH01_GGOKJONG.GetValue().ToString(),
                                                                this.CBH01_GHWAJU.GetValue().ToString(),
                                                                Get_Date(this.MTB01_GDATE.GetValue().ToString()).Substring(0, 6).ToString(),
                                                                this.CBH01_STHANGCHA.GetValue().ToString(),
                                                                this.CBH01_EDHANGCHA.GetValue().ToString(),
                                                                Get_Date(this.MTB01_GDATE.GetValue().ToString())
                                                                );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_US_917AC419");

                        SetFocus(this.MTB01_GDATE);

                        e.Successed = false;
                        return;
                    }


                    // 하역료 매출생성시 입항파일 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_917AO424", this.CBH01_GGOKJONG.GetValue().ToString(),
                                                                this.CBH01_GHWAJU.GetValue().ToString(),
                                                                this.CBH01_STHANGCHA.GetValue().ToString(),
                                                                this.CBH01_EDHANGCHA.GetValue().ToString()
                                                                );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_US_917AP425");

                        SetFocus(this.MTB01_GDATE);

                        e.Successed = false;
                        return;
                    }

                    // 시설사용료 및 하역료 매출생성시 계약파일 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_917AY426", this.CBH01_GGOKJONG.GetValue().ToString(),
                                                                this.CBH01_GHWAJU.GetValue().ToString(),
                                                                this.CBH01_STHANGCHA.GetValue().ToString(),
                                                                this.CBH01_EDHANGCHA.GetValue().ToString()
                                                                );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_US_917AY427");

                        SetFocus(this.MTB01_GDATE);

                        e.Successed = false;
                        return;
                    }
                }
                else // 취소
                {
                    // 하역료 매출 데이터 전표 존재 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_917AE420", this.CBH01_GGOKJONG.GetValue().ToString(),
                                                                this.CBH01_GHWAJU.GetValue().ToString(),
                                                                Get_Date(this.MTB01_GDATE.GetValue().ToString()).Substring(0, 6).ToString(),
                                                                this.CBH01_STHANGCHA.GetValue().ToString(),
                                                                this.CBH01_EDHANGCHA.GetValue().ToString(),
                                                                Get_Date(this.MTB01_GDATE.GetValue().ToString())
                                                                );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_US_917AF421");

                        SetFocus(this.MTB01_GDATE);

                        e.Successed = false;
                        return;
                    }
                }
            }

            if (this.CBO01_GGUBUN.GetValue().ToString() == "A")
            {
                // 처리 하시겠습니까?
                if (!this.ShowMessage("TY_M_GB_26E2Z874"))
                {
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                // 처리 하시겠습니까?
                if (!this.ShowMessage("TY_M_AC_2CDB0166"))
                {
                    e.Successed = false;
                    return;
                }
            }
        }
        #endregion

        #region Description : 무료보관일수 조회
        private void BTN61_SILOCODEHELP30_Click(object sender, EventArgs e)
        {
            TYUSME01C1 popup = new TYUSME01C1();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
            }
        }
        #endregion

        #region Description : 항차 이벤트
        private void CBH01_STHANGCHA_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13)
            {
                this.CBH01_EDHANGCHA.SetValue(this.CBH01_STHANGCHA.GetValue().ToString());
            }
        }
        #endregion
    }
}
