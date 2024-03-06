using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using DataDynamics.ActiveReports;
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
    public partial class TYUSME059B : TYBase
    {
        #region Descriptino : 페이지 로드
        public TYUSME059B()
        {
            InitializeComponent();
        }

        private void TYUSME059B_Load(object sender, System.EventArgs e)
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

            if (this.CBO01_GMEGUBUN.GetText().ToString() == "시설사용료")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92CFC734", Get_Date(this.MTB01_GDATE.GetValue().ToString()),
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
                // 하역료 생성 SP 짜면 됨
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92D8U737", Get_Date(this.MTB01_GDATE.GetValue().ToString()),
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
            

            if (this.CBO01_GGUBUN.GetValue().ToString() == "A")
            {
                if (sOUTMSG.Substring(0, 2) == "OK")
                {
                    this.ShowMessage("TY_M_US_917BV435"); // 저장 메세지
                }
                else
                {
                    this.ShowMessage("TY_M_US_917BX436"); // 저장 메세지
                }                
            }
            else
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

            SetFocus(this.CBH01_GHWAJU.CodeText);
        }
        #endregion

        #region Description : 매출생성 ProcessCheck
        private void BTN61_CREATE_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt  = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

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

            if (this.CBO01_GGUBUN.GetText().ToString() == "생성")
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


                // 소급 생성시 자료 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92CI2736", Get_Date(this.MTB01_GDATE.GetValue().ToString()),
                                                            this.CBH01_STHANGCHA.GetValue().ToString(),
                                                            this.CBH01_EDHANGCHA.GetValue().ToString(), 
                                                            this.CBH01_GGOKJONG.GetValue().ToString(),
                                                            this.CBH01_GHWAJU.GetValue().ToString(),
                                                            this.CBO01_GMEGUBUN.GetValue().ToString()
                                                            );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_US_91BDB481");

                    SetFocus(this.MTB01_GDATE);

                    e.Successed = false;
                    return;
                }
            }
            else
            {
                // 소급 생성시 전표 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92CI8735", Get_Date(this.MTB01_GDATE.GetValue().ToString()),
                                                            this.CBH01_STHANGCHA.GetValue().ToString(),
                                                            this.CBH01_EDHANGCHA.GetValue().ToString(),
                                                            this.CBH01_GGOKJONG.GetValue().ToString(),
                                                            this.CBH01_GHWAJU.GetValue().ToString(),
                                                            this.CBO01_GMEGUBUN.GetValue().ToString()
                                                            );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_GB_25F8V482");

                    SetFocus(this.MTB01_GDATE);

                    e.Successed = false;
                    return;
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

        #region Description : 소급단가 조회
        private void BTN61_SILOCODEHELP30_Click(object sender, EventArgs e)
        {
            TYUSME062I popup = new TYUSME062I();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
            }
        }
        #endregion

        #region Description : 시작 항차 이벤트
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
