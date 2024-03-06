using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 관리전표 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2013.01.03 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_3134L483 : 관리전표 조회(그룹)
    ///  TY_P_AC_3135M486 : 관리전표 조회(상세)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_3134N485 : 관리전표 조회(그룹)
    ///  TY_S_AC_3134N487 : 관리전표 조회(상세)
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_2BF7Y364 : 조회가 완료되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  GSTYYMM : 시작년월
    /// </summary>
    /// 
    public partial class TYACNC008S : TYBase
    {
        #region Description : 페이지 로드
        public TYACNC008S()
        {
            InitializeComponent();
        }
        
        private void TYACNC008S_Load(object sender, System.EventArgs e)
        {
            //날짜
            DTP01_GSTYYMM.SetValue(DateTime.Now.ToString("yyyy"));

            this.BTN61_INQ_Click(null, null);  

            //최초 포커스 이동
            SetStartingFocus(DTP01_GSTYYMM);
        }
        #endregion

        #region Description : 조회 버튼(좌측) 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            //좌측 조회
            this.FPS91_TY_S_AC_3134N485.Initialize();
            this.FPS91_TY_S_AC_3135N487.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_3134L483",                
                this.DTP01_GSTYYMM.GetValue().ToString().Substring(0,4) 
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_3134N485.SetValue(dt);
            }

            //this.ShowMessage("TY_M_GB_2BF7Y364");
        }
        #endregion

        #region Description : FPS91_TY_S_AC_3134N485_CellDoubleClick(좌측 버튼 더블클릭)
        private void FPS91_TY_S_AC_3134N485_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //우측 상세 조회
            this.FPS91_TY_S_AC_3135N487.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_3135M486",
                this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4),
                this.FPS91_TY_S_AC_3134N485.GetValue("B2CDAC").ToString(),
                this.FPS91_TY_S_AC_3134N485.GetValue("B2DPAC").ToString()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_3135N487.SetValue(dt);
            }            
        }
        #endregion

    }
}
