using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.US00
{
    /// <summary>
    /// BL재고관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.04.02 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_92FGG782 : B/L 재고관리 조회    
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_P_US_8BJHK186 : B/L 재고관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    /// </summary>
    public partial class TYUSKB008S : TYBase
    {
        #region Description : 페이지 로드
        public TYUSKB008S()
        {
            InitializeComponent();
        }

        private void TYUSKB008S_Load(object sender, System.EventArgs e)
        {            

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.CBH01_JBHANGCHA.CodeText);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_92FGH784.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                 "TY_P_US_92FGG782",
                 this.CBH01_JBHANGCHA.GetValue().ToString(),
                 this.CBH02_JBHANGCHA.GetValue().ToString(),
                 this.CBH01_JBGOKJONG.GetValue().ToString(),
                 this.CBH01_JBHWAJU.GetValue().ToString(),
                 this.TXT01_JBBLNO.GetValue().ToString()
                );

            this.FPS91_TY_S_US_92FGH784.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion        
    }
}