using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using GrapeCity.ActiveReports;
using TY.ER.GB00;

namespace TY.ER.HR00
{
    /// <summary>
    /// 재직증명서 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.11.25 17:53
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BPGX507 : 재직증명서 조회
    ///  TY_P_HR_4BPIL522 : 재직증명서 등록
    ///  TY_P_HR_4BPIN523 : 재직증명서 수정
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_3219C986 : 동일 자료가 존재합니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  SAV : 저장
    ///  KBSABUN : 사번
    ///  CEBALYN : 발급여부
    ///  CEBIGO : 신청인
    ///  CEDATE : 발급일자
    ///  CEGUBUN : 발급구분
    ///  CEPOST : 직위
    ///  CEWORK1 : 담당업무내용1
    ///  CEWORK2 : 담당업무내용2
    ///  CEYAER : 발급년도
    ///  CEYONGDO : 용도
    ///  SEQ : 순서
    /// </summary>
    public partial class TYHRKB008I : TYBase
    {
        string fsCEYEAR = string.Empty;
        string fsCESEQ = string.Empty;

        #region Description : 페이지 로드
        public TYHRKB008I(string CEYEAR, string CESEQ)
        {
            InitializeComponent();

            fsCEYEAR = CEYEAR;
            fsCESEQ = CESEQ;
        }

        private void TYHRKB008I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);


            if (string.IsNullOrEmpty(fsCEYEAR))
            {
                TXT01_CEYAER.SetValue(DateTime.Now.ToString("yyyy"));

                DTP01_CEDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

                UP_GetSEQ();

                CBH01_KBSABUN.SetValue(TYUserInfo.EmpNo);

                UP_KBSABUN_Info();

                CBO01_CEBALYN.SetValue("Y");

                BTN61_PRT.Visible = false;
            }
            else
            {
                UP_Select();

                BTN61_PRT.Visible = true;
            }
        }
        #endregion

        #region Description : 닫기버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 출력버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            /*
            if (CBO01_CEBALYN.GetValue().ToString() == "Y")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BPJJ528",
                                        TXT01_CEYAER.GetValue().ToString(),
                                        TXT01_SEQ.GetValue().ToString()
                                        );
                DataTable dt = this.DbConnector.ExecuteDataTable();

                SectionReport rpt = new TYHRKB008R();
                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                this.ShowCustomMessage("발급여부를 Y 로 수정하셔야만 출력가능합니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            } */

            //발급여부를 Y로 UPDATE                        
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_623IV511",
                                    "Y",
                                    TYUserInfo.EmpNo,
                                    TXT01_CEYAER.GetValue().ToString(),
                                    TXT01_SEQ.GetValue().ToString()
                                    );
            this.DbConnector.ExecuteNonQuery();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BPJJ528",
                                    TYUserInfo.SecureKey, "Y",
                                    TYUserInfo.SecureKey, "Y",
                                    TXT01_CEYAER.GetValue().ToString(),
                                    TXT01_SEQ.GetValue().ToString()
                                    );
            DataTable dt = this.DbConnector.ExecuteDataTable();

            SectionReport rpt = new TYHRKB008R();
            (new TYERGB001P(rpt, dt)).ShowDialog();

        }
        #endregion

        #region Description : 저장버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            if (string.IsNullOrEmpty(fsCEYEAR))
            {
                UP_GetSEQ();

                this.DbConnector.Attach("TY_P_HR_4BPIL522", TXT01_CEYAER.GetValue().ToString(),
                                                            TXT01_SEQ.GetValue().ToString(),
                                                            CBH01_KBSABUN.GetValue().ToString(),
                                                            DTP01_CEDATE.GetString(),
                                                            CBO01_CEGUBUN.GetValue().ToString(),
                                                            TXT01_CEYONGDO.GetValue().ToString(),
                                                            TXT01_CEPOST.GetValue().ToString(),
                                                            TXT01_CEWORK1.GetValue().ToString(),
                                                            TXT01_CEWORK2.GetValue().ToString(),
                                                            TXT01_CEBIGO.GetValue().ToString(),
                                                            CBO01_CEBALYN.GetValue().ToString(),
                                                            TYUserInfo.EmpNo
                                                            );
            }
            else
            {
                this.DbConnector.Attach("TY_P_HR_4BPIN523", CBH01_KBSABUN.GetValue().ToString(),
                                                            DTP01_CEDATE.GetString(),
                                                            CBO01_CEGUBUN.GetValue().ToString(),
                                                            TXT01_CEYONGDO.GetValue().ToString(),
                                                            TXT01_CEPOST.GetValue().ToString(),
                                                            TXT01_CEWORK1.GetValue().ToString(),
                                                            TXT01_CEWORK2.GetValue().ToString(),
                                                            TXT01_CEBIGO.GetValue().ToString(),
                                                            CBO01_CEBALYN.GetValue().ToString(),
                                                            TYUserInfo.EmpNo,
                                                            TXT01_CEYAER.GetValue().ToString(),
                                                            TXT01_SEQ.GetValue().ToString()
                                                            );
            }

            this.DbConnector.ExecuteNonQuery();

            this.ShowMessage("TY_M_GB_23NAD873");

            fsCEYEAR = TXT01_CEYAER.GetValue().ToString();
            fsCESEQ = TXT01_SEQ.GetValue().ToString();

            BTN61_PRT.Visible = true;
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            //사번 기본정보 디스플레이
            this.UP_KBSABUN_Info();

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 순번 가져오기
        private void UP_GetSEQ()
        {
            string SEQ = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BPJB527", TXT01_CEYAER.GetValue());

            Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

            this.TXT01_SEQ.SetValue(Set_Fill4(iCnt.ToString()));
        }
        #endregion

        #region Description : 데이터 조회
        private void UP_Select()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BPJJ528",
                                   TYUserInfo.SecureKey,
                                    "Y",
                                    TYUserInfo.SecureKey, "Y",
                                    fsCEYEAR,
                                    fsCESEQ
                                    );
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                TXT01_CEYAER.SetValue(fsCEYEAR);
                TXT01_SEQ.SetValue(Set_Fill4(Convert.ToInt16(fsCESEQ).ToString()));
                CBH01_KBSABUN.SetValue(dt.Rows[0]["CESABUN"].ToString());
                TXT01_CEBIGO.SetValue(dt.Rows[0]["CEBIGO"].ToString());

                TXT01_STDATE.SetValue(dt.Rows[0]["KBIDATE"].ToString());

                if (dt.Rows[0]["CEGUBUN"].ToString() == "2")
                {
                    TXT01_EDDATE.SetValue(dt.Rows[0]["EDDATE"].ToString());
                }

                DTP01_CEDATE.SetValue(dt.Rows[0]["DTPCEDATE"].ToString());
                CBO01_CEGUBUN.SetValue(dt.Rows[0]["CEGUBUN"].ToString());
                CBO01_CEBALYN.SetValue(dt.Rows[0]["CEBALYN"].ToString());
                TXT01_CEYONGDO.SetValue(dt.Rows[0]["CEYONGDOY"].ToString());
                TXT01_CEPOST.SetValue(dt.Rows[0]["CEPOST"].ToString());
                TXT01_CEWORK1.SetValue(dt.Rows[0]["CEWORK1"].ToString());
                TXT01_CEWORK2.SetValue(dt.Rows[0]["CEWORK2"].ToString());
                TXT01_KBBONJK.SetValue(dt.Rows[0]["KBBONJK"].ToString());
                TXT01_KBJUSO.SetValue(dt.Rows[0]["KBJUSO"].ToString());

                TXT01_GUNSOKYYMM.SetValue(dt.Rows[0]["WORKDATE"].ToString());
            }
        }
        #endregion

        #region Description : 사번선택 이벤트
        private void CBH01_KBSABUN_TextChanged(object sender, EventArgs e)
        {
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_HR_4BBGV367",
            //                        "",
            //                        CBH01_KBSABUN.GetValue().ToString()
            //                        );
            //DataTable dt = this.DbConnector.ExecuteDataTable();

            //if (dt.Rows.Count > 0)
            //{   
            //    //TXT01_CEPOST.SetValue(dt.Rows[0]["KBJCCDNM"].ToString());
            //    TXT01_CEPOST.SetValue(dt.Rows[0]["KBJJCDNM"].ToString());
            //    TXT01_KBBONJK.SetValue(dt.Rows[0]["KBBONJK"].ToString());
            //    TXT01_KBJUSO.SetValue(dt.Rows[0]["KBJUSO"].ToString());
            //    TXT01_STDATE.SetValue(dt.Rows[0]["KBIDATE"].ToString().Substring(0, 4) + "-" + dt.Rows[0]["KBIDATE"].ToString().Substring(4, 2) + "-" + dt.Rows[0]["KBIDATE"].ToString().Substring(6, 2));
            //    TXT01_EDDATE.SetValue(dt.Rows[0]["EDDATE"].ToString());

            //    this.DbConnector.CommandClear();
            //    this.DbConnector.Attach("TY_P_HR_4BRAB574",
            //                            DTP01_CEDATE.GetString(),
            //                            TXT01_STDATE.GetValue().ToString().Replace("-","")
            //                            );
            //    TXT01_GUNSOKYYMM.SetValue(this.DbConnector.ExecuteScalar());
            //}
        }
        #endregion

        #region Description : 발급일자 선택 이벤트
        private void DTP01_CEDATE_ValueChanged(object sender, EventArgs e)
        {
            if (TXT01_STDATE.GetValue().ToString().Replace("-", "") != "")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BRAB574",
                                        DTP01_CEDATE.GetString(),
                                        TXT01_STDATE.GetValue().ToString().Replace("-", "")
                                        );
                TXT01_GUNSOKYYMM.SetValue(this.DbConnector.ExecuteScalar());
            }
        }
        #endregion

        #region Description : 사번선택 이벤트
        private void CBH01_KBSABUN_Leave(object sender, EventArgs e)
        {
            this.UP_KBSABUN_Info();
        }
        #endregion

        #region Description : UP_KBSABUN_Info 이벤트
        private void UP_KBSABUN_Info()
        {
            if (CBH01_KBSABUN.GetValue().ToString() != "")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BBGV367",
                                        "",
                                        CBH01_KBSABUN.GetValue().ToString(), TYUserInfo.SecureKey, "Y"
                                        );
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    //TXT01_CEPOST.SetValue(dt.Rows[0]["KBJCCDNM"].ToString());
                    TXT01_CEPOST.SetValue(dt.Rows[0]["KBJJCDNM"].ToString());
                    TXT01_KBBONJK.SetValue(dt.Rows[0]["KBBONJK"].ToString());
                    TXT01_KBJUSO.SetValue(dt.Rows[0]["KBJUSO"].ToString());
                    TXT01_STDATE.SetValue(dt.Rows[0]["KBIDATE"].ToString().Substring(0, 4) + "-" + dt.Rows[0]["KBIDATE"].ToString().Substring(4, 2) + "-" + dt.Rows[0]["KBIDATE"].ToString().Substring(6, 2));

                    // 20180409 수정전 소스
                    //TXT01_EDDATE.SetValue(dt.Rows[0]["EDDATE"].ToString());

                    // 20180409 수정후 소스
                    if (dt.Rows[0]["EDDATE"].ToString() == "재직중")
                    {
                        TXT01_EDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        TXT01_EDDATE.SetValue(dt.Rows[0]["EDDATE"].ToString());
                    }

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_4BRAB574",
                                            //DTP01_CEDATE.GetString(),
                                            TXT01_STDATE.GetValue().ToString().Replace("-", ""),
                                            TXT01_EDDATE.GetValue().ToString().Replace("-", "")
                                            );
                    TXT01_GUNSOKYYMM.SetValue(this.DbConnector.ExecuteScalar());
                }
            }
        }
        #endregion
    }
}
