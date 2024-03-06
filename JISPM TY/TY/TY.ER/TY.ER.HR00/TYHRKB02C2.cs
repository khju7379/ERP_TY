using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 학력사항 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.11.12 15:23
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BCFE385 : 학력사항 등록
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  HLHAKGA : 학과
    ///  HLHAKKYO : 학교
    ///  HLHLGUBN : 학력구분
    ///  HLJUGUBN : 졸업구분
    ///  HLSABUN : 사번
    ///  HLIDATE : 입학일자
    ///  HLJDATE : 졸업일자
    ///  HLCOMPANY : 회사
    /// </summary>
    public partial class TYHRKB02C2 : TYBase
    {
        string fsLHSABUN = string.Empty;
        string fsLHLHGUBN = string.Empty;
        string fsLHJUGUBN = string.Empty;

        #region Description : 페이지 로드
        public TYHRKB02C2(string LHSABUN, string LHLHGUBN, string LHJUGUBN)
        {
            fsLHSABUN = LHSABUN;
            fsLHLHGUBN = LHLHGUBN;
            fsLHJUGUBN = LHJUGUBN;

            InitializeComponent();
        }

        private void TYHRKB02C2_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            if (string.IsNullOrEmpty(fsLHLHGUBN))
            {
                CBH01_HLSABUN.SetValue(fsLHSABUN);
                DTP01_HLIDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
                DTP01_HLJDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
                SetStartingFocus(CBH01_HLHLGUBN.CodeText);
            }
            else
            {
                UP_Select();
                CBH01_HLHLGUBN.SetReadOnly(true);
                CBH01_HLJUGUBN.SetReadOnly(true);
                SetStartingFocus(DTP01_HLIDATE);
            }
            CBH01_HLSABUN.SetReadOnly(true);
            
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            if (string.IsNullOrEmpty(fsLHLHGUBN))   //등록
            {
                this.DbConnector.Attach("TY_P_HR_4BCFE385", CBH01_HLSABUN.GetValue().ToString(),
                                                            CBH01_HLHLGUBN.GetValue().ToString(),
                                                            CBH01_HLJUGUBN.GetValue().ToString(),
                                                            DTP01_HLIDATE.GetValue().ToString(),
                                                            DTP01_HLJDATE.GetValue().ToString(),
                                                            CBH01_HLHAKKYO.GetValue().ToString(),
                                                            CBH01_HLHAKGA.GetValue().ToString(),
                                                            TYUserInfo.EmpNo
                                                            ); 
            }
            else                                    // 수정
            {
                this.DbConnector.Attach("TY_P_HR_4BD9A395", DTP01_HLIDATE.GetValue().ToString(),
                                                            DTP01_HLJDATE.GetValue().ToString(),
                                                            CBH01_HLHAKKYO.GetValue().ToString(),
                                                            CBH01_HLHAKGA.GetValue().ToString(),
                                                            TYUserInfo.EmpNo,
                                                            CBH01_HLSABUN.GetValue().ToString(),
                                                            CBH01_HLHLGUBN.GetValue().ToString(),
                                                            CBH01_HLJUGUBN.GetValue().ToString()
                                                            );
            }
            this.DbConnector.ExecuteNonQuery();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ShowMessage("TY_M_GB_23NAD873");
            this.Close();
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string STDATE = this.DTP01_HLIDATE.GetString();
            string EDDATE = this.DTP01_HLJDATE.GetString();

            if (Convert.ToInt32(STDATE) > Convert.ToInt32(EDDATE))
            {
                this.ShowCustomMessage("입학일자가 졸업일자보다 클수 없습니다 .", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (string.IsNullOrEmpty(this.fsLHLHGUBN))
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_HR_4BDB2413",
                    CBH01_HLSABUN.GetValue().ToString(),
                    CBH01_HLHLGUBN.GetValue().ToString(),
                    CBH01_HLJUGUBN.GetValue().ToString()
                    );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_GB_23S40973");
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
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

        #region Description : 데이터 조회
        private void UP_Select()
        {   
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BDB2413", fsLHSABUN,
                                                        fsLHLHGUBN,
                                                        fsLHJUGUBN
                                                        );
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                CBH01_HLSABUN.SetValue(fsLHSABUN);
                CBH01_HLHLGUBN.SetValue(fsLHLHGUBN);
                CBH01_HLJUGUBN.SetValue(fsLHJUGUBN);
                DTP01_HLIDATE.SetValue(dt.Rows[0]["HLIDATE"].ToString());
                DTP01_HLJDATE.SetValue(dt.Rows[0]["HLJDATE"].ToString());
                CBH01_HLHAKKYO.SetValue(dt.Rows[0]["HLHAKKYO"].ToString());
                CBH01_HLHAKGA.SetValue(dt.Rows[0]["HLHAKGA"].ToString());
            }
        }
        #endregion
    }
}
