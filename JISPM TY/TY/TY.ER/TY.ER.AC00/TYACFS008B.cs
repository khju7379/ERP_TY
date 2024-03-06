using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 대손처리 생성 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.08.14 14:40
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_28E1B386 : 대손처리 생성
    ///  TY_P_AC_28E1D387 : 대손처리 전체 삭제
    ///  TY_P_AC_28E1D388 : 대손처리 전표유무 체크
    ///  TY_P_AC_28E1O390 : 대손처리 체크
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_28D5W379 : 자료가 존재합니다! 삭제후 작업하세요
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_25F8V482 : 전표번호가 존재합니다!
    ///  TY_M_GB_26E2Z874 : 생성하시겠습니까?
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  GOKCR : 생성구분
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACFS008B : TYBase
    {
        #region Description : 처리 ProcessCheck 이벤트
        public TYACFS008B()
        {
            InitializeComponent();
        }

        private void TYACFS008B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_GSTYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            SetStartingFocus(DTP01_GSTYYMM);
        }
        #endregion

        #region Description : 생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            if (this.CBO01_GOKCR.GetValue().ToString() == "A")  //생성
            {
                this.DbConnector.Attach("TY_P_AC_28E1B386", Employer.EmpNo, this.DTP01_GSTYYMM.GetValue());
            }
            else //삭제
            {
                this.DbConnector.Attach("TY_P_AC_28E1D387", this.DTP01_GSTYYMM.GetValue());
            }

            this.DbConnector.ExecuteTranQueryList();

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

        #region Description : 닫기 버튼 이벤트
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
                this.DbConnector.Attach("TY_P_AC_28E1O390", this.DTP01_GSTYYMM.GetValue());
                iRowCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                if (iRowCnt > 0)
                {
                    this.ShowMessage("TY_M_AC_28D5W379");
                    e.Successed = false;
                    return;
                }

                //전표번호 존재하는지 체크
                iRowCnt = 0;
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_28E1D388", this.DTP01_GSTYYMM.GetValue());
                iRowCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                if (iRowCnt > 0)
                {
                    this.ShowMessage("TY_M_GB_25F8V482");
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
