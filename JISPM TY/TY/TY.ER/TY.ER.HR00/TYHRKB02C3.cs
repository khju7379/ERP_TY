using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 경력사항 프로그램입니다.
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
    ///  TY_P_HR_4BCGB386 : 경력사항 등록
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  HLSABUN : 사번
    ///  KLIDATE : 입사일자
    ///  KLJDATE : 퇴사일자
    ///  KLAMT : 급여
    ///  KLJIKJGNG : 회사명
    ///  KLJIKWI : 직위
    ///  KLSUNBUN : 순번
    ///  KLUPMU : 담당업무
    /// </summary>
    public partial class TYHRKB02C3 : TYBase
    {
        string fsKLSABUN = string.Empty;
        string fsKLSUNBUN = string.Empty;

        #region Description : 페이지 로드
        public TYHRKB02C3(string KLSABUN, string KLSUNBUN)
        {
            fsKLSABUN = KLSABUN;
            fsKLSUNBUN = KLSUNBUN;

            InitializeComponent();
        }

        private void TYHRKB02C3_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            if (string.IsNullOrEmpty(fsKLSUNBUN))
            {
                CBH01_HLSABUN.SetValue(fsKLSABUN);
                DTP01_KLIDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
                DTP01_KLJDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
                UP_GetSEQ();
            }
            else
            {   
                UP_Select();
            }
            SetStartingFocus(TXT01_KLJIKJGNG);
            CBH01_HLSABUN.SetReadOnly(true);
            TXT01_KLSUNBUN.SetReadOnly(true);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            if (string.IsNullOrEmpty(fsKLSUNBUN))   //등록
            {
                UP_GetSEQ();

                this.DbConnector.Attach("TY_P_HR_4BCGB386", CBH01_HLSABUN.GetValue().ToString(),
                                                            TXT01_KLSUNBUN.GetValue().ToString(),
                                                            TXT01_KLJIKJGNG.GetValue().ToString(),
                                                            DTP01_KLIDATE.GetValue().ToString(),
                                                            DTP01_KLJDATE.GetValue().ToString(),
                                                            TXT01_KLJIKWI.GetValue().ToString(),
                                                            TXT01_KLUPMU.GetValue().ToString(),
                                                            TXT01_KLAMT.GetValue().ToString(),
                                                            TYUserInfo.EmpNo
                                                            ); 
            }
            else                                    //수정
            {
                this.DbConnector.Attach("TY_P_HR_4BCGB386", TXT01_KLJIKJGNG.GetValue().ToString(),
                                                            DTP01_KLIDATE.GetValue().ToString(),
                                                            DTP01_KLJDATE.GetValue().ToString(),
                                                            TXT01_KLJIKWI.GetValue().ToString(),
                                                            TXT01_KLUPMU.GetValue().ToString(),
                                                            TXT01_KLAMT.GetValue().ToString(),
                                                            TYUserInfo.EmpNo,
                                                            CBH01_HLSABUN.GetValue().ToString(),
                                                            TXT01_KLSUNBUN.GetValue().ToString()
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
            this.DbConnector.Attach("TY_P_HR_4BDD5415", fsKLSABUN,
                                                        fsKLSUNBUN
                                                        );
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                CBH01_HLSABUN.SetValue(fsKLSABUN);

                TXT01_KLSUNBUN.SetValue(Set_Fill3(Convert.ToInt16(fsKLSUNBUN).ToString()));

                TXT01_KLJIKJGNG.SetValue(dt.Rows[0]["KLJIKJGNG"].ToString());
                DTP01_KLIDATE.SetValue(dt.Rows[0]["KLIDATE"].ToString());
                DTP01_KLJDATE.SetValue(dt.Rows[0]["KLJDATE"].ToString());
                TXT01_KLJIKWI.SetValue(dt.Rows[0]["KLJIKWI"].ToString());
                TXT01_KLUPMU.SetValue(dt.Rows[0]["KLUPMU"].ToString());
                TXT01_KLAMT.SetValue(dt.Rows[0]["KLAMT"].ToString());
            }
        }
        #endregion

        #region Description : 순번 가져오기
        private void UP_GetSEQ()
        {
            string SEQ = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BDDD416", fsKLSABUN
                                                        );

            Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

            this.TXT01_KLSUNBUN.SetValue(Set_Fill3(iCnt.ToString()));
        }
        #endregion
    }
}
