using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.UT00;

namespace TY.ER.ED00
{
    /// <summary>
    /// 반출보고서 출고상세내역 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.03.02 11:49
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_732DG821 : 반출보고서 출고내역상세 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_732DG822 : 반출보고서 반출상세내역 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  EDIDATE : 반입일자
    ///  EDISINNO : 반입근거번호
    /// </summary>
    public partial class TYEDKB03C1 : TYBase
    {
        private string fsEDIGJ;
        private string fsEDIDATE;
        private string fsEDISINNO;

        #region  Description : 폼 로드 이벤트
        public TYEDKB03C1( string sEDIGJ, string sEDIDATE, string sEDISINNO)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsEDIGJ = sEDIGJ;
            fsEDIDATE = sEDIDATE;
            fsEDISINNO = sEDISINNO;
        }

        private void TYEDKB03C1_Load(object sender, System.EventArgs e)
        {
            if (fsEDIGJ == "T")
            {
                this.FPS91_TY_S_UT_732DG822.Visible = true;
                this.FPS91_TY_S_UT_732F3825.Visible = false;
            }
            else
            {
                this.FPS91_TY_S_UT_732DG822.Visible = false;
                this.FPS91_TY_S_UT_732F3825.Visible = true;
            }

            this.DTP01_EDIDATE.SetValue(fsEDIDATE);
            this.TXT01_EDISINNO.SetValue(fsEDISINNO);

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_732DG822.Initialize();
            this.FPS91_TY_S_UT_732F3825.Initialize();

            if (fsEDIGJ == "T")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_732DG821", this.DTP01_EDIDATE.GetString().ToString(), TXT01_EDISINNO.GetValue().ToString());
                this.FPS91_TY_S_UT_732DG822.SetValue(this.DbConnector.ExecuteDataTable());
                if (this.FPS91_TY_S_UT_732DG822.CurrentRowCount > 0)
                {
                    this.SpreadSumRowAdd(this.FPS91_TY_S_UT_732DG822, "CHCHTANK", "합   계", SumRowType.Sum, "CHMTQTY", "CHYNCHQTY");
                }
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_732F1824", this.DTP01_EDIDATE.GetString().ToString(), TXT01_EDISINNO.GetValue().ToString());
                this.FPS91_TY_S_UT_732F3825.SetValue(this.DbConnector.ExecuteDataTable());
                if (this.FPS91_TY_S_UT_732F3825.CurrentRowCount > 0)
                {
                    this.SpreadSumRowAdd(this.FPS91_TY_S_UT_732F3825, "CHBINNO", "합   계", SumRowType.Sum, "CHMTQTY", "CHYNCHQTY");
                }
            }  
        
            //반출전송이력 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_73AE7882", fsEDIGJ, TXT01_EDISINNO.GetValue().ToString());
            this.FPS91_TY_S_UT_73AE7885.SetValue(this.DbConnector.ExecuteDataTable());
            if (this.FPS91_TY_S_UT_73AE7885.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_UT_73AE7885, "EDIJUKHA", "합   계", SumRowType.Sum, "EDICHQTY");

                if (this.FPS91_TY_S_UT_73AE7885.CurrentRowCount > 0)
                {
                    for (int i = 0; i < this.FPS91_TY_S_UT_73AE7885.CurrentRowCount; i++)
                    {
                        if (this.FPS91_TY_S_UT_73AE7885.GetValue(i, "EDIRCVGB").ToString() == "Y")
                        {
                            this.FPS91_TY_S_UT_73AE7885_Sheet1.Rows[i].ForeColor = Color.Blue;
                        }
                        else if (this.FPS91_TY_S_UT_73AE7885.GetValue(i, "EDIRCVGB").ToString() == "N")
                        {
                            this.FPS91_TY_S_UT_73AE7885_Sheet1.Rows[i].ForeColor = Color.Red;
                        }
                    }
                }
            }



        }
        #endregion

        #region  Description : FPS91_TY_S_UT_732DG822_CellDoubleClick 버튼 이벤트
        private void FPS91_TY_S_UT_732DG822_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.FPS91_TY_S_UT_732DG822.GetValue("CHHWAMUL").ToString() != "")   //합계라인이 아닌경우만
            {
                TYUTIN006I popup = new TYUTIN006I(this.FPS91_TY_S_UT_732DG822.GetValue("CHCHULIL").ToString(), this.FPS91_TY_S_UT_732DG822.GetValue("CHTKNO").ToString());

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.BTN61_INQ_Click(null, null);
                }
            }

        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        

    }
}
