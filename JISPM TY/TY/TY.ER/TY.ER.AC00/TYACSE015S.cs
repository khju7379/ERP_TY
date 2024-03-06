using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TY.Service.Library;
using TY.ER.GB00;
using GrapeCity.ActiveReports;
using Shoveling2010.SmartClient.SystemUtility.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 결산 손익계산서 조회-기타사업 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2014.06.17 15:11
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_46IAF819 : 전년대비 손익계산서-기타사업_매출 조회
    ///  TY_P_AC_46IAG820 : 전년대비 손익계산서-기타사업_판매비 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_46IAN821 : 결산 손익조회_기타사업
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PLQRGB2 : 조회구분
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    /// </summary>
    public partial class TYACSE015S : TYBase
    {
  
        #region Description : 페이지 로드
        public TYACSE015S()
        {
            InitializeComponent();
        }

        private void TYACSE015S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_INQ.ProcessCheck += new TButton.CheckHandler(BTN61_INQ_ProcessCheck);

            this.DTP01_GEDDATE.SetReadOnly(true);

            this.DTP01_GSTDATE.Focus();

        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sProced_id  = string.Empty;
            string s시작계정 = string.Empty;
            string s종료계정 = string.Empty;

            switch (this.CBO01_PLQRGB2.GetValue().ToString().Trim())
            {
                case "1":
                    sProced_id = "TY_P_AC_46IAF819";
                    s시작계정 = "41100100";
                    s종료계정 = "41100600";
                    break;

                case "2":
                    sProced_id = "TY_P_AC_46IAG820";
                    s시작계정 = "44120100";
                    s종료계정 = "44124500";
                    break;
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach(sProced_id, s시작계정, s종료계정, this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 6));

            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                this.DTP01_GEDDATE.SetValue(dt.Rows[0]["ANYYMMCM"].ToString().Trim());

                this.FPS91_TY_S_AC_46IAN821.SetValue(dt);
            }
        }
        #endregion

        #region Description : 조회 CHECK
        private void BTN61_INQ_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sProced_id = string.Empty;
            string s시작계정 = string.Empty;
            string s종료계정 = string.Empty;

            switch (this.CBO01_PLQRGB2.GetValue().ToString().Trim())
            {
                case "1":
                    sProced_id = "TY_P_AC_46IAF819";
                    s시작계정 = "41100100";
                    s종료계정 = "41100600";
                    break;

                case "2":
                    sProced_id = "TY_P_AC_46IAG820";
                    s시작계정 = "44120100";
                    s종료계정 = "44124500";
                    break;
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach(sProced_id, s시작계정, s종료계정, this.DTP01_GSTDATE.GetValue().ToString().Substring(0, 6));

            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count == 0)
            {
                this.ShowCustomMessage("자료가 존재하지 않습니다.", "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

        }
        #endregion

    }
}