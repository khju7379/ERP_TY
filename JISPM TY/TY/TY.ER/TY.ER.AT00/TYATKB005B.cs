using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.ER.GB00;

namespace TY.ER.AT00
{
    /// <summary>
    /// 세대별 요금생성 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2018.09.05 10:38
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
    ///  TY_M_MR_2BF50353 : 처리하시겠습니까?
    ///  TY_M_MR_2BF50354 : 처리하였습니다.
    ///  TY_M_UT_71BDP399 : 처리 중 오류가 발생하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  APBATGUBN : 구분
    ///  APUYYMM : 년월
    /// </summary>
    public partial class TYATKB005B : TYBase
    {
        #region Description : 폼 로드
        public TYATKB005B()
        {
            InitializeComponent();
        }

        private void TYATKB005B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_APUYYMM.SetValue(System.DateTime.Now.ToString("yyyy-MM"));

            SetStartingFocus(this.DTP01_APUYYMM);
        }
        #endregion

        #region Description : 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();

                double dAMRTOTALAMT = 0;
                double dAMRLATEAMT = 0;

                string sBIGO = string.Empty;

                // 생성
                if (this.CBO01_APBATGUBN.GetValue().ToString() == "A")
                {
                    // 미납금 조회

                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_HR_89SHB765", this.DTP01_APUYYMM.GetString().Substring(0, 6),
                                                                this.TXT01_AMRHOSU.GetValue().ToString());

                    dt = this.DbConnector.ExecuteDataTable();

                    // 미납금 생성

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if(i+1 < dt.Rows.Count)
                        {
                            dAMRTOTALAMT += Convert.ToDouble(dt.Rows[i]["AMRTOTALAMT"].ToString());
                            dAMRLATEAMT += Convert.ToDouble(dt.Rows[i]["AMRLATEAMT"].ToString());
                            if (sBIGO != "")
                            {
                                sBIGO += ",";
                            }
                            sBIGO += dt.Rows[i]["AMRYYMM"].ToString();

                            if (dt.Rows[i]["AMRHOSU"].ToString() != dt.Rows[i + 1]["AMRHOSU"].ToString())
                            {
                                this.DbConnector.CommandClear();
                                this.DbConnector.Attach("TY_P_HR_89SHR767", this.DTP01_APUYYMM.GetString().Substring(0, 6),
                                                                            dt.Rows[i]["AMRHOSU"].ToString(),
                                                                            "1013",
                                                                            dAMRTOTALAMT + dAMRLATEAMT,
                                                                            sBIGO + " 월 분",
                                                                            TYUserInfo.EmpNo
                                                                            );
                                this.DbConnector.ExecuteTranQueryList();

                                dAMRTOTALAMT = 0;
                                dAMRLATEAMT = 0;
                                sBIGO = "";
                            }
                        }
                        else{

                            dAMRTOTALAMT += Convert.ToDouble(dt.Rows[i]["AMRTOTALAMT"].ToString());
                            dAMRLATEAMT += Convert.ToDouble(dt.Rows[i]["AMRLATEAMT"].ToString());
                            if (sBIGO != "")
                            {
                                sBIGO += ",";
                            }
                            sBIGO += dt.Rows[i]["AMRYYMM"].ToString();

                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_HR_89SHR767", this.DTP01_APUYYMM.GetString().Substring(0, 6),
                                                                        dt.Rows[i]["AMRHOSU"].ToString(),
                                                                        "1013",
                                                                        dAMRTOTALAMT + dAMRLATEAMT,
                                                                        sBIGO,
                                                                        TYUserInfo.EmpNo
                                                                        );
                            this.DbConnector.ExecuteTranQueryList();
                        }
                    }

                    // 세대별 월별 마스타, 내역 생성 SP
                    string sRET_MSG = string.Empty;

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_89SDS760", this.DTP01_APUYYMM.GetString().Substring(0, 6),
                                                                this.DTP01_AMRBILDATE.GetString(),
                                                                this.DTP01_AMRNILDATE.GetString(),
                                                                this.TXT01_AMRHOSU.GetValue().ToString(),
                                                                TYUserInfo.EmpNo,
                                                                sRET_MSG
                                                                );
                    sRET_MSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                    // 공지사항 복사
                    if (CKB01_NOTICEGB.Checked == true)
                    {
                        // 삭제 TY_P_HR_97VDB078
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_HR_97VDB078", this.DTP01_APUYYMM.GetString().Substring(0, 6));

                        DateTime dCOPYDATE = Convert.ToDateTime(DTP01_APUYYMM.GetString().ToString().Substring(0, 4) + "-" + DTP01_APUYYMM.GetString().ToString().Substring(4, 2) + "-01").AddMonths(-1);

                        // 복사 TY_P_HR_97VDN079
                        this.DbConnector.Attach("TY_P_HR_97VDN079", this.DTP01_APUYYMM.GetString().Substring(0, 6),
                                                                    TYUserInfo.EmpNo,
                                                                    dCOPYDATE.ToString("yyyyMM")
                                                                    );

                        this.DbConnector.ExecuteTranQueryList();
                    }

                    if (sRET_MSG == "OK")
                    {
                        this.ShowMessage("TY_M_MR_2BF50354");
                    }
                    else
                    {
                        this.ShowMessage("TY_M_UT_71BDP399");
                    }
                }
                // 취소
                else
                {
                    // 마스타 삭제
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_HR_8A2HY775",
                                            this.DTP01_APUYYMM.GetString().Substring(0,6),
                                            TXT01_AMRHOSU.GetValue().ToString());

                    this.DbConnector.ExecuteTranQuery();

                    // 내역 삭제
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_HR_8A2HZ776",
                                            this.DTP01_APUYYMM.GetString().Substring(0, 6),
                                            TXT01_AMRHOSU.GetValue().ToString());

                    this.DbConnector.ExecuteTranQuery();

                    this.ShowMessage("TY_M_MR_2BF50354");
                }
            }
            catch
            {
                this.ShowMessage("TY_M_UT_71BDP399");
            }
        }
        #endregion

        #region Description : BATCH ProcessCheck
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            if (this.TXT01_AMRHOSU.GetValue().ToString() != "")
            {
                // 호 수 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_8AFAM948", this.TXT01_AMRHOSU.GetValue().ToString());
                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowCustomMessage("존재하지않는 호수 입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            if(this.CBO01_APBATGUBN.GetValue().ToString() == "A")
            {
                

                // 자료 존재 유무 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_8A4DG779", this.DTP01_APUYYMM.GetString().Substring(0, 6),
                                                            this.TXT01_AMRHOSU.GetValue().ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowCustomMessage("동일 자료가 존재합니다. 취소 후 다시 작업하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                    e.Successed = false;
                    return;
                }

                // 사택 월별요금관리 마스타 등록 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_88VEQ677", this.DTP01_APUYYMM.GetString().Substring(0, 6));

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count == 0)
                {
                    this.ShowCustomMessage("사택월별 요금관리 자료가 존재하지 않습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                    e.Successed = false;
                    return;
                }

                if (this.TXT01_AMRHOSU.GetValue().ToString() != "777")
                {
                    // 아파트 세대별사용량관리 등록 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_8A4DP781", this.DTP01_APUYYMM.GetString().Substring(0, 6),
                                                                this.TXT01_AMRHOSU.GetValue().ToString());

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count == 0)
                    {
                        this.ShowCustomMessage("세대별 사용량 관리 자료가 존재하지 않습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                        e.Successed = false;
                        return;
                    }
                }
            }
            else{
                //// 납부 여부 체크
                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach("TY_P_HR_8A4DH780", this.DTP01_APUYYMM.GetString().Substring(0, 6),
                //                                            this.TXT01_AMRHOSU.GetValue().ToString());

                //dt = this.DbConnector.ExecuteDataTable();

                //if (dt.Rows.Count > 0)
                //{
                //    this.ShowCustomMessage("납부 내역이 존재합니다. 취소 작업이 불가합니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                //    e.Successed = false;
                //    return;
                //}
            }

            if (!this.ShowMessage("TY_M_MR_2BF50353"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void DTP01_APUYYMM_ValueChanged(object sender, EventArgs e)
        {
            if (this.DTP01_APUYYMM.GetString().Length >= 6)
            {
                DateTime dTime = Convert.ToDateTime(Set_Date(this.DTP01_APUYYMM.GetString().ToString()));

                this.DTP01_AMRBILDATE.SetValue(dTime.ToString("yyyy-MM" + "-26"));
                this.DTP01_AMRNILDATE.SetValue(dTime.AddMonths(+1).ToString("yyyy-MM" + "-15"));
            }
        }

        #region Description : 생성구분 선택 이벤트
        private void CBO01_APBATGUBN_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.CBO01_APBATGUBN.GetValue().ToString().Trim() == "A")
            {
                CKB01_NOTICEGB.SetReadOnly(false);
            }
            else
            {
                CKB01_NOTICEGB.SetReadOnly(true);
            }
        }
        #endregion
    }
}
