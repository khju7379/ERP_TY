using System;
using System.Data;
using System.Drawing;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.AC00
{
    /// <summary>
    /// 미지급금 조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.08.24 15:11
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_28O44547 : 미지급금 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_28O45548 : 미지급금 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  M1VNCD : 거래처
    ///  M1GUBN : 지급형태
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    /// </summary>
    public partial class TYACFP005S : TYBase
    {
        #region Description : 페이지 로드
        public TYACFP005S()
        {
            InitializeComponent();
        }

        private void TYACFP005S_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.DTP01_GSTDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_28O44547",
                this.DTP01_GSTDATE.GetValue(),
                this.DTP01_GEDDATE.GetValue(),
                this.CBO01_M1GUBN.GetValue(),
                this.CBH01_M1VNCD.GetValue(),
                this.DTP01_GSTDATE.GetValue(),
                this.DTP01_GEDDATE.GetValue(),
                this.CBH01_M1VNCD.GetValue(),
                this.DTP01_GSTDATE.GetValue(),
                this.DTP01_GEDDATE.GetValue(),
                this.CBH01_M1VNCD.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_28O45548.SetValue(dt);

                // 특정 ROW 색깔 입히기
                for (int i = 0; i < this.FPS91_TY_S_AC_28O45548.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_28O45548.GetValue(i, "VNSANGHO").ToString() == "현금합계")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_28O45548.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    }
                    else if (this.FPS91_TY_S_AC_28O45548.GetValue(i, "VNSANGHO").ToString() == "어음합계")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_28O45548.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
                    }
                }
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