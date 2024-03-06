using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 개별승인 전표관리 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2012.04.18 09:27
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_24IAI737 : 개별승인 자료 조회
    ///  TY_P_AC_25755082 : 승인 자료존재 유무확인
    ///  TY_P_AC_25752081 : 승인 자료존재 유무확인
    ///  
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_24I4S770 : 개별승인 조회(미승인,승인)
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_24IBB754 : 승인년월과 미승인년월이 일치하지 않습니다
    ///  TY_M_AC_25931119 : 승인년월과 미승인년월이 일치하지 않습니다
    ///  TY_M_AC_24IBD756 : 승인일자가 미승인 일자보다 작습니다
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다
    ///  TY_M_AC_2584X095 : 승인자를 확인하세요
    ///  TY_M_AC_2584Y096 : 승인일자를 확인하세요
    ///  TY_M_AC_2585M104 : 작성부서를 확인하세요
    ///  TY_M_AC_2585M105 : 작성일자를 확인하세요
    ///  TY_M_AC_2585M106 : 일련번호를 확인하세요
    ///  
    ///  # 필드사전 정보 ####
    ///  BTNJPNO : 미승인전표조회
    ///  CANCEL : 취소
    ///  CNDB2NOSQ : 전표번호_번호
    ///  CONFIRM : 확인
    ///  SAV : 저장
    ///  CNDB2DPMK : 전표번호_부서
    ///  CNDB2DTMK : 전표번호_일자
    /// </summary>
    public partial class TYACBJ002 : TYBase
    {

        private string  sSABUN = string.Empty;

        //private bool _Isloaded = false;

 
        public TYACBJ002()
        {
            InitializeComponent();
        }

        #region Description : Page Load()
        private void TYACBJ002_Load(object sender, System.EventArgs e)
        {
            // 로그인 사번 가져오기
            this.sSABUN = TYUserInfo.EmpNo.Trim().ToUpper();

            this.CBH01_B1NOEM.SetValue(TYUserInfo.EmpNo.Trim().ToUpper());
        }
        #endregion

        #region Description : 코드헬프 처리[전표번호 조회]
        //private void BTN61_BTNJPNO_Click(object sender, EventArgs e)
        //{
        //    TYACBJ002I popup = new TYACBJ002I(this.CBH01_CNDB2DPMK.GetValue().ToString(), this.DTP01_CNDB2DTMK.GetString().ToString(), this.TXT01_CNDB2NOSQ.GetValue().ToString());

        //    if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        this.CBH01_CNDB2DPMK.SetValue(popup.sDPMK);
        //        this.DTP01_CNDB2DTMK.SetValue(popup.sDTMK);
        //        this.TXT01_CNDB2NOSQ.SetValue(popup.sNOSQ);

        //        this.BTN61_CONFIRM_Click(null, null);
        //    }
        //} 
        #endregion

        #region Description : 확인
        private void BTN61_CONFIRM_Click(object sender, EventArgs e)
        {
            string sB2DPMK = string.Empty;
            string sB2DTMK = string.Empty;
            string sB2NOSQ = string.Empty;

            sB2DPMK = this.CBH01_CNDB2DPMK.GetValue().ToString().Trim();
            sB2DTMK = this.DTP01_CNDB2DTMK.GetString().ToString().Trim();
            sB2NOSQ = this.TXT01_CNDB2NOSQ.GetValue().ToString().Trim(); 

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_24IAI737", sB2DPMK, sB2DTMK, sB2NOSQ);
            this.FPS91_TY_S_AC_24I4S770.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_24I4S770.CurrentRowCount == 0)
                this.ShowMessage("TY_M_AC_2422N250");
            else
            {
                this.FPS91_TY_S_AC_24I4S770.Select();
                this.FPS91_TY_S_AC_24I4S770_Sheet1.ActiveRowIndex = 0;
                this.FPS91_TY_S_AC_24I4S770_CellClick(null, null);

                string sNOJP = this.FPS91_TY_S_AC_24I4S770.GetValue("B2NOJP").ToString();

                if (sNOJP == "")
                {
                    this.BTN61_CANCEL.Visible = false;
                    this.BTN61_SAV.Visible = true;
                    this.CBH01_B1NOEM.SetValue(sSABUN);
                    this.DTP01_B2DTLI.SetValue(this.DTP01_CNDB2DTMK.GetString().ToString().Trim());
                }
                else
                {
                    this.BTN61_CANCEL.Visible = true;
                    this.BTN61_SAV.Visible = false;
                }
            }
        }
        #endregion

        #region Description : 확인 체크
        private void BTN61_CONFIRM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // 작성부서
            if (this.CBH01_CNDB2DPMK.GetValue().ToString().Trim() == "")
            {
                this.ShowMessage("TY_M_AC_2585M104");
                this.CBH01_CNDB2DPMK.Focus();

                e.Successed = false;
                return;
            }

            // 작성일자
            if (this.DTP01_CNDB2DTMK.GetString().ToString().Trim() == "")
            {
                this.ShowMessage("TY_M_AC_2585M105");
                this.DTP01_CNDB2DTMK.Focus();

                e.Successed = false;
                return;
            }

            // 일련번호
            if (this.TXT01_CNDB2NOSQ.GetValue().ToString().Trim() == "")
            {
                this.ShowMessage("TY_M_AC_2585M106");
                this.TXT01_CNDB2NOSQ.Focus();

                e.Successed = false;
                return;
            }

        }
        #endregion

        #region Description : 승인 처리
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sB2DPMK = string.Empty;
            string sB2DTMK = string.Empty;
            string sB2NOSQ = string.Empty;
            string sPRDATE = string.Empty;
            string sPRSABUN = string.Empty;
            string sPRGUBUN = string.Empty;
            string sOUTMSG = string.Empty;

            sB2DPMK = this.CBH01_CNDB2DPMK.GetValue().ToString().Trim();
            sB2DTMK = this.DTP01_CNDB2DTMK.GetString().ToString().Trim();
            sB2NOSQ = this.TXT01_CNDB2NOSQ.GetValue().ToString().Trim();
            sPRDATE = this.DTP01_B2DTLI.GetValue().ToString().Trim();
            sPRSABUN = this.CBH01_B1NOEM.GetValue().ToString().Trim();
            sPRGUBUN = "INS";

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_24IAO743", sB2DPMK, sB2DTMK, sB2NOSQ, sPRDATE, sPRSABUN, sPRGUBUN, sOUTMSG); // SP CALL
            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar()); // SP의 OUTPUT 값 가져오는 부분

            if (sOUTMSG.Substring(0, 2) == "ER")
            {
                this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            else
            {
                this.ShowCustomMessage(sOUTMSG, "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.None);

                this.BTN61_CONFIRM_Click(null, null);
            }

        }
        #endregion

        #region Description : 승인 처리시 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // 승인자
            if (this.CBH01_B1NOEM.GetValue().ToString() == "")
            {
                this.ShowMessage("TY_M_AC_2584X095");
                this.CBH01_B1NOEM.Focus();

                e.Successed = false;
                return;
            }

            // 승인일자
            if (this.DTP01_B2DTLI.GetValue().ToString() == "")
            {
                this.ShowMessage("TY_M_AC_2584Y096");
                this.DTP01_B2DTLI.Focus();

                e.Successed = false;
                return;
            }

            // 승인년월과 미승인년월
            if (this.sSABUN == "0349-F" || this.sSABUN == "0269-M" ||
                this.sSABUN == "0348-F" || this.sSABUN == "0363-M" ||
                this.sSABUN == "0390-M" ) // 김계영,강경석,박옥경,차윤일,황승환
            {
                if (this.DTP01_B2DTLI.GetValue().ToString().Trim().Substring(0, 6) != this.DTP01_CNDB2DTMK.GetValue().ToString().Trim().Substring(0, 6))
                {
                    this.ShowMessage("TY_M_AC_25931119");
                    this.DTP01_B2DTLI.Focus();

                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (this.DTP01_B2DTLI.GetString().ToString().Trim() != this.DTP01_CNDB2DTMK.GetString().ToString().Trim())
                {
                    this.ShowMessage("TY_M_AC_24IBB754");
                    this.DTP01_B2DTLI.Focus();

                    e.Successed = false;
                    return;
                }
            }

            // 승인일자가 미승인 일자
            if (Int32.Parse(this.DTP01_CNDB2DTMK.GetString().ToString().Trim()) > Int32.Parse(this.DTP01_B2DTLI.GetString().ToString().Trim()))
            {
                this.ShowMessage("TY_M_AC_24IBD756");
                this.DTP01_B2DTLI.Focus();

                e.Successed = false;
                return;
            }

        }
        #endregion

        #region Description : 승인 취소 처리
        private void BTN61_CANCEL_Click(object sender, EventArgs e)
        {
            string sB2DPMK = string.Empty;
            string sB2DTMK = string.Empty;
            string sB2NOSQ = string.Empty;
            string sPRDATE = string.Empty;
            string sPRSABUN = string.Empty;
            string sPRGUBUN = string.Empty;
            string sOUTMSG = string.Empty;

            sB2DPMK = this.CBH01_CNDB2DPMK.GetValue().ToString().Trim();
            sB2DTMK = this.DTP01_CNDB2DTMK.GetString().ToString().Trim();
            sB2NOSQ = this.TXT01_CNDB2NOSQ.GetValue().ToString().Trim();
            sPRDATE = this.DTP01_B2DTLI.GetValue().ToString().Trim();
            sPRSABUN = this.CBH01_B1NOEM.GetValue().ToString().Trim();
            sPRGUBUN = "DEL";

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_24IAO743", sB2DPMK, sB2DTMK, sB2NOSQ, sPRDATE, sPRSABUN, sPRGUBUN, sOUTMSG);  // SP CALL
            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar()); // SP의 OUTPUT 값 가져오는 부분

            if (sOUTMSG.Substring(0, 2) == "ER")
            {
                 this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            else
            {
                this.ShowCustomMessage(sOUTMSG, "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.None);

                this.BTN61_CONFIRM_Click(null, null);
            }

        }
        #endregion

        #region Description : 스프레드 클릭 이벤트
        private void FPS91_TY_S_AC_24I4S770_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int row = (e == null ? 0 : e.Row);

            if (this.BTN61_SAV.Visible != true)
            {
                this.CBH01_B1NOEM.SetValue(this.FPS91_TY_S_AC_24I4S770.GetValue(row, "B1NOEM"));
                this.DTP01_B2DTLI.SetValue(this.FPS91_TY_S_AC_24I4S770.GetValue(row, "B2DTLI"));
            }
            this.TXT01_B2NOJP.SetValue(this.FPS91_TY_S_AC_24I4S770.GetValue(row, "B2NOJP"));

            this.TXT02_B1TTJPAMT.SetValue(this.FPS91_TY_S_AC_24I4S770.GetValue(row, "B1TTJP"));
            this.TXT02_KBHANGL1.SetValue(this.FPS91_TY_S_AC_24I4S770.GetValue(row, "KBHANGL1"));
            this.TXT02_B1IDJPNM.SetValue(this.FPS91_TY_S_AC_24I4S770.GetValue(row, "B1IDJPNM"));
            this.TXT02_B2NOLN.SetValue(this.FPS91_TY_S_AC_24I4S770.GetValue(row, "B2NOLN"));
            this.TXT02_A1MNAC.SetValue(this.FPS91_TY_S_AC_24I4S770.GetValue(row, "A1NMAC1"));
            this.TXT02_DPAC.SetValue(this.FPS91_TY_S_AC_24I4S770.GetValue(row, "DPAC"));
            this.TXT02_VLMINM1.SetValue(this.FPS91_TY_S_AC_24I4S770.GetValue(row, "VLMINM1"));
            this.TXT02_VLMINM2.SetValue(this.FPS91_TY_S_AC_24I4S770.GetValue(row, "VLMINM2"));
            this.TXT02_VLMINM3.SetValue(this.FPS91_TY_S_AC_24I4S770.GetValue(row, "VLMINM3"));
            this.TXT02_VLMINM4.SetValue(this.FPS91_TY_S_AC_24I4S770.GetValue(row, "VLMINM4"));
            this.TXT02_VLMINM5.SetValue(this.FPS91_TY_S_AC_24I4S770.GetValue(row, "VLMINM5"));
            this.TXT02_VLMINM6.SetValue(this.FPS91_TY_S_AC_24I4S770.GetValue(row, "VLMINM6"));

            this.TXT02_B2AMDR.SetValue(this.FPS91_TY_S_AC_24I4S770.GetValue(row, "B2AMDR"));
            this.TXT02_B2AMCR.SetValue(this.FPS91_TY_S_AC_24I4S770.GetValue(row, "B2AMCR"));
            this.TXT02_B2WCJP.SetValue(this.FPS91_TY_S_AC_24I4S770.GetValue(row, "B2WCJP"));
            this.TXT02_B2RKAC.SetValue(this.FPS91_TY_S_AC_24I4S770.GetValue(row, "B2RKAC"));
            this.TXT02_B2RKCU.SetValue(this.FPS91_TY_S_AC_24I4S770.GetValue(row, "B2RKCU"));
        }
        #endregion

    }
}
