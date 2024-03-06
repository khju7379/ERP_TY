using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 전자세금계산서 수기발급관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.04.17 11:08
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_34HBE509 : 전자세금계산서 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_34HBG511 : 전자세금계산서 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    ///  VNSANGHO : 거래처명
    /// </summary>
    public partial class TYACZZ007P : TYBase
    {
        #region Description : 폼 로드
        public TYACZZ007P()
        {
            InitializeComponent();
        }

        private void TYACZZ007P_Load(object sender, System.EventArgs e)
        {
            this.DTP01_GSTDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));
            this.DTP01_GEDDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));


            this.BTN61_INQ_Click(null, null);

            this.SetStartingFocus(this.DTP01_GSTDATE); 
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {

            this.FPS91_TY_S_AC_34HBG511.Initialize(); 

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                //"TY_P_AC_34HBE509",
                "TY_P_AC_88UBH667",
                this.DTP01_GSTDATE.GetString(),
                this.DTP01_GEDDATE.GetString(),
                this.TXT01_VNSANGHO.GetValue().ToString()
                );

            this.FPS91_TY_S_AC_34HBG511.SetValue(this.DbConnector.ExecuteDataTable());

            for (int i = 0; i < this.FPS91_TY_S_AC_34HBG511.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_34HBG511.GetValue(i, "BILL_NO").ToString() == "")
                {
                    this.FPS91_TY_S_AC_34HBG511_Sheet1.Cells[i, 12].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                }
                else
                {
                    //this.FPS91_TY_S_AC_34HBG511_Sheet1.Cells[i, 12].Image = global::TY.Service.Library.Properties.Resources.magnifier;
                }
            }
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYACZZ007I(string.Empty)) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : FPS91_TY_S_AC_34HBG511_CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_34HBG511_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYACZZ007I(this.FPS91_TY_S_AC_34HBG511.GetValue("BILL_NO").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : FPS91_TY_S_AC_34HBG511_ButtonClicked 이벤트
        private void FPS91_TY_S_AC_34HBG511_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "11") 
            {
                if (this.FPS91_TY_S_AC_34HBG511.GetValue("BILL_NO").ToString() != "")
                {
                    string sB2DPMK = this.FPS91_TY_S_AC_34HBG511.GetValue("BILL_NO").ToString().Substring(0, 6);
                    string sB2DTMK = this.FPS91_TY_S_AC_34HBG511.GetValue("BILL_NO").ToString().Substring(6, 8);
                    string sB2NOSQ = this.FPS91_TY_S_AC_34HBG511.GetValue("BILL_NO").ToString().Substring(14, 3);


                    if (this.OpenModalPopup(new TYACBJ001I(sB2DPMK, sB2DTMK, sB2NOSQ)) == System.Windows.Forms.DialogResult.OK)
                        this.BTN61_INQ_Click(null, null);

                }
                else
                {
                    this.ShowMessage("TY_M_MR_2BC51262");
                    return;
                }
            }
        }
        #endregion
    }
}
