using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using DataDynamics.ActiveReports;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using FarPoint.Win.Spread.CellType;

namespace TY.ER.UT00
{
    /// <summary>
    /// 탱크로리 출고 할증 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.10.26 13:16
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_66FD4200 : 대표 거래처 코드 조회
    ///  TY_P_UT_6APKM542 : 탱크로리 출고 할증 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_6AQDM555 : 탱크로리 출고 할증 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  CHHWAJU : 화주
    ///  CHHWAMUL : 화물
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYUTIN027S : TYBase
    {
        #region Description : 페이지 로드
        public TYUTIN027S()
        {
            InitializeComponent();
        }

        private void TYUTIN027S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(System.DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            BTN61_INQ_Click(null, null);

            SetStartingFocus(this.CBH01_CHHWAJU.CodeText);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sHWAJU = string.Empty; ;

            // 대표거래처 코드 가져오기
            sHWAJU = Get_VNCODE(this.CBH01_CHHWAJU.GetValue().ToString());

            this.FPS91_TY_S_UT_6AQDM555.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_6APKM542", sHWAJU,
                                                        this.CBH01_CHHWAMUL.GetValue().ToString(),
                                                        this.DTP01_STDATE.GetString(),
                                                        this.DTP01_EDDATE.GetString(),
                                                        sHWAJU,
                                                        this.CBH01_CHHWAMUL.GetValue().ToString(),
                                                        this.DTP01_STDATE.GetString(),
                                                        this.DTP01_EDDATE.GetString()
                                                        );

            this.FPS91_TY_S_UT_6AQDM555.SetValue(this.DbConnector.ExecuteDataTable());

            // 특정 ROW 색깔 입히기
            for (int i = 0; i < this.FPS91_TY_S_UT_6AQDM555.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_UT_6AQDM555.GetValue(i, "VEND").ToString() == "소계")
                {
                    // 특정 ROW 색깔 입히기
                    this.FPS91_TY_S_UT_6AQDM555.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    this.FPS91_TY_S_UT_6AQDM555.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);
                }
            }
        }
        #endregion
    }
}
