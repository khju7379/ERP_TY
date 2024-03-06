using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.BS00
{
    /// <summary>
    /// 영업계획(매출액,취급량) 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.08.08 10:04
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_788A4373 : 사업계획-영업계획(매출액,취급량) 자료 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_788A2372 : 영업계획(매출액,취급량) 자료 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  INQOPTION : 조회구분
    ///  INQOPTION2 : 조회구분
    ///  BSYEAR : 년도
    /// </summary>
    public partial class TYBSUP004S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYBSUP004S()
        {
            InitializeComponent();
        }

        private void TYBSUP004S_Load(object sender, System.EventArgs e)
        {
            this.TXT01_BCYEAR.SetValue(DateTime.Now.AddYears(1).ToString("yyyy"));

            CBH01_BCDPMK.DummyValue = this.TXT01_BCYEAR.GetValue().ToString() + "0101";
            CBH01_BCDPAC.DummyValue = this.TXT01_BCYEAR.GetValue().ToString() + "0101";

            this.SetStartingFocus(this.TXT01_BCYEAR);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_788BE383.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach(CBO01_INQOPTION.GetValue().ToString() == "S" ? "TY_P_AC_788BN384" : "TY_P_AC_788EM391", CBO01_INQOPTION2.GetValue().ToString(), TXT01_BCYEAR.GetValue().ToString(), CBH01_BCDPMK.GetValue(), CBH01_BCDPAC.GetValue());
            this.FPS91_TY_S_AC_788BE383.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_788BE383.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_AC_788BE383.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_788BE383.GetValue(i, "BCCDAC").ToString() == "")
                    {
                        this.FPS91_TY_S_AC_788BE383.ActiveSheet.Rows[i].BackColor = Color.FromArgb(228, 242, 194);
                                                
                        for (int j = 17; j < 30; j++)
                        {
                            this.FPS91_TY_S_AC_788BE383_Sheet1.Cells[i, j].ForeColor = Color.Red;
                        }
                    }
                }
            }
        }
        #endregion       

    }
}
