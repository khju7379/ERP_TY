using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 무역파일번호 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.12.03 20:28
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_GB_2C38S817 : 무역파일번호 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_P_AC_87HGZ415 : 무역파일번호 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  FILENO : 파일번호
    /// </summary>
    public partial class TYACLO01C2 : TYBase
    {

        public string fsLOLICONTYEAR = string.Empty;
        public string fsLOLICONTNO   = string.Empty;
        public string fsLOLICONTSEQ  = string.Empty;
        public string fsLOLINUM      = string.Empty;
        public string fsJANAMT       = string.Empty;

        private string fsLOCCONTYEAR = string.Empty;
        private string fsLOCCONTSEQ  = string.Empty;

        #region  Description : 폼 로드 이벤트
        public TYACLO01C2(string sLOCCONTYEAR, string sLOCCONTSEQ)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsLOCCONTYEAR = sLOCCONTYEAR.ToString();
            fsLOCCONTSEQ  = sLOCCONTSEQ.ToString();
        }
        #endregion

        private void TYACLO01C2_Load(object sender, System.EventArgs e)
        {
            this.TXT01_STYEAR.SetValue(DateTime.Now.AddYears(-1).ToString("yyyy"));
            this.TXT01_EDYEAR.SetValue(DateTime.Now.ToString("yyyy"));

            this.BTN61_INQ_Click(null, null);

            this.SetStartingFocus(this.TXT01_STYEAR);
        }

        #region  Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_87HH5416.Initialize();
 
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_94BBP341", this.TXT01_STYEAR.GetValue().ToString(),
                                                        this.TXT01_EDYEAR.GetValue().ToString(),
                                                        fsLOCCONTYEAR.ToString(),
                                                        fsLOCCONTSEQ.ToString());

            // 20190411 수정전 소스
            //this.DbConnector.Attach("TY_P_AC_87HGZ415", this.TXT01_STYEAR.GetValue().ToString(),
            //                                            this.TXT01_EDYEAR.GetValue().ToString(),
            //                                            fsLOCCONTYEAR.ToString(),
            //                                            fsLOCCONTSEQ.ToString());

            this.FPS91_TY_S_AC_87HH5416.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_87HH5416.CurrentRowCount > 0)
            {
                this.SetFocus(this.FPS91_TY_S_AC_87HH5416); 
            }
        }
        #endregion

        #region Description : 종료 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_P_AC_87HGZ415_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int row = (e == null ? 0 : e.Row);

            fsLOLICONTYEAR = this.FPS91_TY_S_AC_87HH5416.GetValue(row, "LOLICONTNO").ToString().Substring(0, 4);
            fsLOLICONTNO   = this.FPS91_TY_S_AC_87HH5416.GetValue(row, "LOLICONTNO").ToString().Substring(5, 2);
            fsLOLINUM      = this.FPS91_TY_S_AC_87HH5416.GetValue(row, "LOLICONTNO").ToString().Substring(8, 3);
            fsLOLICONTSEQ  = this.FPS91_TY_S_AC_87HH5416.GetValue(row, "LOLICONTSEQ").ToString();

            fsJANAMT       = this.FPS91_TY_S_AC_87HH5416.GetValue(row, "JANAMT").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }

        private void FPS91_TY_P_AC_87HGZ415_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            int row = (e == null ? 0 : FPS91_TY_S_AC_87HH5416.ActiveSheet.ActiveRowIndex);

            fsLOLICONTYEAR = this.FPS91_TY_S_AC_87HH5416.GetValue(row, "LOLICONTNO").ToString().Substring(0, 4);
            fsLOLICONTNO   = this.FPS91_TY_S_AC_87HH5416.GetValue(row, "LOLICONTNO").ToString().Substring(5, 2);
            fsLOLINUM      = this.FPS91_TY_S_AC_87HH5416.GetValue(row, "LOLICONTNO").ToString().Substring(8, 3);
            fsLOLICONTSEQ  = this.FPS91_TY_S_AC_87HH5416.GetValue(row, "LOLICONTSEQ").ToString();

            fsJANAMT       = this.FPS91_TY_S_AC_87HH5416.GetValue(row, "JANAMT").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion
    }
}
