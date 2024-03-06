using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 교육사항 프로그램입니다.
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
    ///  TY_P_HR_4BCGW387 : 교육사항 등록
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
    ///  GYIDATE : 교육시작일
    ///  GYJDATE : 교육종료일
    ///  GYADUAMT : 비용
    ///  GYBIGO : 비고
    ///  GYCHAMGA : 출결
    ///  GYDESC : 교육내용
    ///  GYESAN : 예산구분
    ///  GYSHHANG : 시행처
    ///  GYSUNGJUK : 성적
    /// </summary>
    public partial class TYHRKB02C4 : TYBase
    {
        string fsGYSABUN = string.Empty;
        string fsGYIDATE = string.Empty;

        #region Description : 페이지 로드
        public TYHRKB02C4(string GYSABUN, string GYIDATE)
        {
            fsGYSABUN = GYSABUN;
            fsGYIDATE = GYIDATE;

            InitializeComponent();
        }

        private void TYHRKB02C4_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.CBH01_HLSABUN.SetValue(fsGYSABUN);

            if (string.IsNullOrEmpty(fsGYIDATE))
            {
                DTP01_GYIDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
                DTP01_GYJDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
                SetStartingFocus(DTP01_GYIDATE);
            }
            else
            {
                UP_Select();
                SetStartingFocus(DTP01_GYJDATE);
                DTP01_GYIDATE.SetReadOnly(true);
            }
            CBH01_HLSABUN.SetReadOnly(true);
        }
        #endregion

        #region Description : 저장버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
           this.DbConnector.CommandClear();

           if (string.IsNullOrEmpty(fsGYIDATE))   //등록
           {
               this.DbConnector.Attach("TY_P_HR_4BCGW387", CBH01_HLSABUN.GetValue().ToString(),
                                                           DTP01_GYIDATE.GetValue().ToString(),
                                                           DTP01_GYJDATE.GetValue().ToString(),
                                                           TXT01_GYDESC.GetValue().ToString(),
                                                           TXT01_GYSHHANG.GetValue().ToString(),
                                                           TXT01_GYCHAMGA.GetValue().ToString(),
                                                           TXT01_GYSUNGJUK.GetValue().ToString(),
                                                           TXT01_GYADUAMT.GetValue().ToString(),
                                                           TXT01_GYBIGO.GetValue().ToString(),
                                                           TYUserInfo.EmpNo
                                                           );
           }
           else                                    // 수정
           {
               this.DbConnector.Attach("TY_P_HR_4BD9G397", DTP01_GYJDATE.GetValue().ToString(),
                                                           TXT01_GYDESC.GetValue().ToString(),
                                                           TXT01_GYSHHANG.GetValue().ToString(),
                                                           TXT01_GYCHAMGA.GetValue().ToString(),
                                                           TXT01_GYSUNGJUK.GetValue().ToString(),
                                                           TXT01_GYADUAMT.GetValue().ToString(),
                                                           TXT01_GYBIGO.GetValue().ToString(),
                                                           TYUserInfo.EmpNo,
                                                           CBH01_HLSABUN.GetValue().ToString(),
                                                           DTP01_GYIDATE.GetValue().ToString()
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
            if (string.IsNullOrEmpty(this.fsGYIDATE))
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_HR_4BDEG419",
                    CBH01_HLSABUN.GetValue().ToString(),
                    DTP01_GYIDATE.GetValue().ToString()
                    );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_3219C986");
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

        #region Description : 닫기버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 데이터 조회
        private void UP_Select()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BDEG419", fsGYSABUN,
                                                        fsGYIDATE
                                                        );
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                CBH01_HLSABUN.SetValue(fsGYSABUN);
                DTP01_GYIDATE.SetValue(fsGYIDATE);

                DTP01_GYJDATE.SetValue(dt.Rows[0]["GYJDATE"].ToString());
                TXT01_GYSHHANG.SetValue(dt.Rows[0]["GYSHHANG"].ToString());
                TXT01_GYDESC.SetValue(dt.Rows[0]["GYDESC"].ToString());
                TXT01_GYCHAMGA.SetValue(dt.Rows[0]["GYCHAMGA"].ToString());
                TXT01_GYSUNGJUK.SetValue(dt.Rows[0]["GYSUNGJUK"].ToString());
                TXT01_GYADUAMT.SetValue(dt.Rows[0]["GYADUAMT"].ToString());
                TXT01_GYBIGO.SetValue(dt.Rows[0]["GYBIGO"].ToString());
            }
        }
        #endregion
    }
}
