using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 자격면허 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.11.14 17:53
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BEHY424 : 자격사항 조회
    ///  TY_P_HR_4BHEE429 : 자격사항 등록
    ///  TY_P_HR_4BHFQ433 : 자격사항 수정
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23S40973 : 동일한 코드가 존재합니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  JKCODE : 자격코드
    ///  JKIDATE : 취득일자
    ///  JKJDATE : 만료일자
    ///  JKPYDATEE : 지급종료일자
    ///  JKPYDATES : 지급시작일자
    ///  HLCOMPANY : 회사
    ///  HLSABUN : 사번
    ///  JKBUNHO : 자격번호
    ///  JKPAYAMOUNT : 면허금액
    ///  JKSHHANG : 시행처
    /// </summary>
    public partial class TYHRKB02C6 : TYBase
    {
        string fsJKSABUN = string.Empty;
        string fsJKCODE = string.Empty;

        #region Description : 페이지 로드
        public TYHRKB02C6(string JKSABUN, string JKCODE)
        {
            fsJKSABUN = JKSABUN;
            fsJKCODE = JKCODE;

            InitializeComponent();
        }

        private void TYHRKB02C6_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            if (string.IsNullOrEmpty(fsJKCODE))
            {
                CBH01_HLSABUN.SetValue(fsJKSABUN);
                DTP01_JKIDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
                DTP01_JKJDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
                DTP01_JKPYDATES.SetValue("");
                DTP01_JKPYDATEE.SetValue("");
                SetStartingFocus(CBH01_JKCODE.CodeText);
            }
            else
            {
                UP_Select();
                CBH01_JKCODE.SetReadOnly(true);
                SetStartingFocus(DTP01_JKIDATE);
            }
            CBH01_HLSABUN.SetReadOnly(true);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            if (string.IsNullOrEmpty(fsJKCODE))   //등록
            {
                this.DbConnector.Attach("TY_P_HR_4BHEE429", CBH01_HLSABUN.GetValue().ToString(),
                                                            CBH01_JKCODE.GetValue().ToString(),
                                                            DTP01_JKIDATE.GetValue().ToString(),
                                                            DTP01_JKJDATE.GetValue().ToString(),
                                                            TXT01_JKBUNHO.GetValue().ToString(),
                                                            TXT01_JKSHHANG.GetValue().ToString(),
                                                            TXT01_JKPAYAMOUNT.GetValue().ToString(),
                                                            DTP01_JKPYDATES.GetValue().ToString(),
                                                            DTP01_JKPYDATEE.GetValue().ToString(),
                                                            TYUserInfo.EmpNo
                                                            );
            }
            else                                    // 수정
            {
                this.DbConnector.Attach("TY_P_HR_4BHFQ433", DTP01_JKIDATE.GetValue().ToString(),
                                                            DTP01_JKJDATE.GetValue().ToString(),
                                                            TXT01_JKBUNHO.GetValue().ToString(),
                                                            TXT01_JKSHHANG.GetValue().ToString(),
                                                            TXT01_JKPAYAMOUNT.GetValue().ToString(),
                                                            DTP01_JKPYDATES.GetValue().ToString(),
                                                            DTP01_JKPYDATEE.GetValue().ToString(),
                                                            TYUserInfo.EmpNo,
                                                            CBH01_HLSABUN.GetValue().ToString(),
                                                            CBH01_JKCODE.GetValue().ToString()
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
            if (string.IsNullOrEmpty(this.fsJKCODE))
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_HR_4BEHY424",
                    CBH01_HLSABUN.GetValue().ToString(),
                    CBH01_JKCODE.GetValue().ToString()
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
            this.DbConnector.Attach("TY_P_HR_4BEHY424", fsJKSABUN,
                                                        fsJKCODE
                                                        );
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                CBH01_HLSABUN.SetValue(fsJKSABUN);
                CBH01_JKCODE.SetValue(fsJKCODE);
                DTP01_JKIDATE.SetValue(dt.Rows[0]["JKIDATE"].ToString());
                DTP01_JKJDATE.SetValue(dt.Rows[0]["JKJDATE"].ToString());
                TXT01_JKBUNHO.SetValue(dt.Rows[0]["JKBUNHO"].ToString());
                TXT01_JKSHHANG.SetValue(dt.Rows[0]["JKSHHANG"].ToString());
                TXT01_JKPAYAMOUNT.SetValue(dt.Rows[0]["JKPAYAMOUNT"].ToString());
                DTP01_JKPYDATES.SetValue(dt.Rows[0]["JKPYDATES"].ToString());
                DTP01_JKPYDATEE.SetValue(dt.Rows[0]["JKPYDATEE"].ToString());
            }
        }
        #endregion
    }
}
