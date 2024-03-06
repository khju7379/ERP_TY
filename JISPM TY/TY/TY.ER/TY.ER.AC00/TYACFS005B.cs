using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 개별분석 대상 거래처 가져오기 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.07.25 16:58
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_27Q92283 : 개별분석 대상 거래처 가져오기
    ///  TY_P_AC_27QBA285 : 개별분석 대상 거래처 일괄삭제
    ///  TY_P_AC_27QBD286 : 개별분석 대상 거래처 체크
    ///  TY_P_AC_27Q2E290 : 채권 연령분석MASTER 체크
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_27J8T137 : 복사 월에 자료가 존재합니다 삭제후 작업하세요!
    ///  TY_M_GB_26E2Z874 : 생성하시겠습니까?
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    ///  TY_M_AC_2422N250 : 자료가 존재하지않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  GSTYYMM : 시작년월
    ///  GOKCR : 생성구분
    /// </summary>
    public partial class TYACFS005B : TYBase
    {
        #region Description : 폼로드 이벤트
        public TYACFS005B()
        {
            InitializeComponent();
        }

        private void TYACFS005B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_GSTYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
        }
        #endregion

        #region Description : 처리 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            if (this.CBO01_GOKCR.GetValue().ToString() == "A")
            {
                this.DbConnector.Attach("TY_P_AC_27Q92283", Employer.EmpNo, this.DTP01_GSTYYMM.GetValue());
            }
            else
            {
                this.DbConnector.Attach("TY_P_AC_27QBA285", this.DTP01_GSTYYMM.GetValue());
            }

            this.DbConnector.ExecuteNonQuery();

            if (this.CBO01_GOKCR.GetValue().ToString() == "A")
            {
                this.ShowMessage("TY_M_GB_26E30875");
            }
            else
            {
                this.ShowMessage("TY_M_GB_23NAD874");
            }
        }
        #endregion

        #region Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();  
        }
        #endregion

        #region Description : 처리 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int iRowCnt = 0;

            if (this.CBO01_GOKCR.GetValue().ToString() == "A")
            {
                //생성년월에 자료가 존재하는 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_27QBD286", this.DTP01_GSTYYMM.GetValue());
                iRowCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                if (iRowCnt > 0)
                {
                    this.ShowMessage("TY_M_AC_27J8T137");
                    e.Successed = false;
                    return;
                }
            
                //연령분석Master에 해당년월 자료가 존재하는지 체크
                iRowCnt = 0;
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_27Q2E290", this.DTP01_GSTYYMM.GetValue());
                iRowCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                if (iRowCnt <= 0)
                {
                    this.ShowMessage("TY_M_AC_2422N250");
                    e.Successed = false;
                    return;
                }
            }

            if (this.CBO01_GOKCR.GetValue().ToString() == "A")
            {
                if (!this.ShowMessage("TY_M_GB_26E2Z874"))
                {
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (!this.ShowMessage("TY_M_GB_23NAD872"))
                {
                    e.Successed = false;
                    return;
                }
            }
        }
        #endregion
    }
}
