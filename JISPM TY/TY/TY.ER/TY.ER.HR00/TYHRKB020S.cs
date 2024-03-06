using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 학자금 지원대상자 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.03.14 15:02
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_73EGK929 : 학자금 지원대상자 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_73EGK930 : 학자금 지원대상자 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  SEL : 선택
    ///  KBSABUN : 사번
    /// </summary>
    public partial class TYHRKB020S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRKB020S()
        {
            InitializeComponent();
        }

        private void TYHRKB020S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SEL.ProcessCheck += new TButton.CheckHandler(BTN61_SEL_ProcessCheck);

            this.SetStartingFocus(CBH01_KBSABUN.CodeText);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_73EGK930.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_73EGK929", this.CBH01_KBSABUN.GetValue(), this.CBO01_GOKCR.GetValue(),TYUserInfo.SecureKey, TYUserInfo.PerAuth );
            this.FPS91_TY_S_HR_73EGK930.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 선택 버튼 이벤트
        private void BTN61_SEL_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            (new TYHRKB020I(ds.Tables[0], this.CBO01_GOKCR.GetValue().ToString())).ShowDialog();

            BTN61_INQ_Click(null, null);
        }

        private void BTN61_SEL_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_HR_73EGK930.GetDataSourceInclude(TSpread.TActionType.Select, "HKSABUN", "HKSABUNNM", "HKMANGENUM", "HKYEAR", "HKSSEQ", "HKCHDNAME", "HKHLGUBN", "HKHLGUBNNM", "HKHAKKYO", "HKHAKKYONM", "HKHAKGA", "HKHAKGANM", "HKHAKYEAR", "HKHAKGI", "HKINGHAKGI", "HKHAKGITOTAL", "HKJUGUBN", "HKJUGUBNNM"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_25F59464");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_HR_73EH9939"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion
    }
}
