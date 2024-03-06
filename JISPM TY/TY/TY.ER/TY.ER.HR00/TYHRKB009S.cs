using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 사원명부 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.11.27 17:53
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BRE1581 : 사원명부 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY4BRE5582 : 사원명부 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  KBBUSEO : 부서
    ///  KBJKCD : 직급
    ///  KBUNION : 노조유무
    /// </summary>
    public partial class TYHRKB009S : TYBase
    {
        #region Description : 페이지 로드
        public TYHRKB009S()
        {
            InitializeComponent();
        }

        private void TYHRKB009S_Load(object sender, System.EventArgs e)
        {
            this.CBH01_KBBUSEO.DummyValue = DateTime.Now.ToString("yyyyMMdd");
            this.CBO01_RESIGNGUBN.SetValue("1");

            this.UP_Set_JuminAuthCheck(CBO01_INQ_AUTH);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sKBBALCD1 = string.Empty;


            this.FPS91_TY4BRE5582.Initialize();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                      "TY_P_HR_4B5GY325", "BS", "", ""
                    );
                DataTable dcode = this.DbConnector.ExecuteDataTable();

                if (dcode.Rows.Count > 0)
                {
                    for (int i = 0; i < dcode.Rows.Count; i++)
                    {
                        if (CBO01_RESIGNGUBN.GetValue().ToString() == "1")
                        {
                            if (dcode.Rows[i]["Code"].ToString() != "900" && dcode.Rows[i]["Code"].ToString() != "560")
                            {
                                sKBBALCD1 += dcode.Rows[i]["Code"].ToString() + ",";
                            }
                        }
                        else if (CBO01_RESIGNGUBN.GetValue().ToString() == "2")
                        {
                            if (dcode.Rows[i]["Code"].ToString() == "900" || dcode.Rows[i]["Code"].ToString() == "560")
                            {
                                sKBBALCD1 += dcode.Rows[i]["Code"].ToString() + ",";
                            }
                        }
                        else
                        {
                            sKBBALCD1 += dcode.Rows[i]["Code"].ToString() + ",";
                        }
                    }
                    sKBBALCD1 = sKBBALCD1.Substring(0, sKBBALCD1.Length - 1);
                }
                        

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_HR_4BRE1581",
                this.CBH01_KBBUSEO.GetValue(),
                this.CBH01_KBJKCD.GetValue(),
                this.CBO01_KBUNION.GetValue(),
                sKBBALCD1,
                TYUserInfo.SecureKey,
                CBO01_INQ_AUTH.GetValue().ToString()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY4BRE5582.SetValue(dt);
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        }
        #endregion
       
    }
}
