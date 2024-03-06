using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 승호생성 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2015.01.30 16:53
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_522E0249 : 승호생성 인사기본사항 조회
    ///  TY_P_HR_522ED250 : 승호파일 등록
    ///  TY_P_HR_522EE251 : 승호파일 삭제
    ///  TY_P_HR_522EF252 : 승호파일 기준년월 이상 존재 유무
    ///  TY_P_HR_522EG253 : 승호파일 발련번호 존재 유무
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2CDB0166 : 취소 하시겠습니까?
    ///  TY_M_AC_2CDB1167 : 취소 되었습니다!
    ///  TY_M_AC_2CDB1168 : 취소 작업에 실패했습니다!
    ///  TY_M_GB_26E2Z874 : 생성하시겠습니까?
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    ///  TY_M_GB_26E31876 : 생성 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  GGUBUN : 구분
    ///  YYYYMM : 기준 년월
    /// </summary>
    public partial class TYHRSJ002B : TYBase
    {
        #region Description : 폼 로드
        public TYHRSJ002B()
        {
            InitializeComponent();
        }

        private void TYHRSJ002B_Load(object sender, System.EventArgs e)
        {
            // 처리 체크
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_GDATE.SetValue(System.DateTime.Now.ToString("yyyyMM"));
        }
        #endregion

        #region Description : 처리 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.CBO01_GGUBUN.GetValue().ToString() == "A") // 생성
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_5C1F1243",
                                        TYUserInfo.EmpNo,
                                        this.DTP01_GDATE.GetValue().ToString()
                                        );

                    this.DbConnector.ExecuteTranQueryList();

                    this.ShowMessage("TY_M_HR_5C1HM251");
                }
                else // 취소
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_5C1F2244",
                                        this.DTP01_GDATE.GetValue().ToString()
                                        );

                    this.DbConnector.ExecuteTranQueryList();

                    this.ShowMessage("TY_M_HR_5C1HM250");
                }
            }
            catch
            {
                if (this.CBO01_GGUBUN.GetValue().ToString() == "A") // 생성
                {
                    this.ShowMessage("TY_M_HR_5C1HN252");
                }
                else
                {
                    this.ShowMessage("TY_M_HR_5C1HN253");
                }
            }
        }
        #endregion

        #region Description : 처리 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            // 상조회 테이블 존재 유무 체크
            this.DbConnector.Attach
            (
            "TY_P_HR_5C1FH246",
            this.DTP01_GDATE.GetValue().ToString()
            );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (this.CBO01_GGUBUN.GetValue().ToString() == "A") // 생성
                {
                    this.ShowMessage("TY_M_HR_5C1FN247");
                    e.Successed = false;
                    return;
                }
                else
                {
                    this.ShowMessage("TY_M_HR_5C1HN254");
                }
            }
            else
            {
                if (this.CBO01_GGUBUN.GetValue().ToString() == "A") // 생성
                {
                    this.ShowMessage("TY_M_HR_5C1HL249");
                }
                else
                {
                    this.ShowMessage("TY_M_HR_5C1FN248");
                    e.Successed = false;
                    return;
                }
            }
        }
        #endregion

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion
    }
}