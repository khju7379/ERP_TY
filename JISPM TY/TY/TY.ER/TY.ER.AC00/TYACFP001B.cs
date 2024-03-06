using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.AC00
{
    /// <summary>
    /// 미지급금생성 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.05.07 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2586C110 : 미지급금 생성
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  M1VNCD : 거래처
    ///  GOKCR : 생성구분
    ///  M1GUBN : 지급형태
    ///  M1SAGB : 지역구분
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    ///  M1DTED : 지급일자
    /// </summary>
    public partial class TYACFP001B : TYBase
    {
        #region Description : 페이지 로드
        public TYACFP001B()
        {
            InitializeComponent();
        }

        private void TYACFP001B_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.DTP01_M1DTED);
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);
        }
        #endregion

        #region Description : 배치 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sM1SAGB = string.Empty;
            string sOUTMSG = string.Empty;
            Int16 iCnt = 0;

            sM1SAGB = this.CBO01_M1SAGB.GetValue().ToString(); // 지역구분

            if (sM1SAGB != "")
            {
                sM1SAGB = sM1SAGB == "1" ? "1" : "6";
            }


            if (this.CBO01_GOKCR.GetValue().ToString().Trim() == "CREATE")
            {
                this.DbConnector.CommandClear(); // ANTPMEF  : 지역구분(1), 거래처(6), 생성일자(8)
                this.DbConnector.Attach("TY_P_AC_36CAU845", sM1SAGB, this.CBH01_M1VNCD.GetValue(), this.DTP01_M1DTED.GetString()); // 생성전 존재 체크
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                if (iCnt > 0)
                {
                    this.ShowMessage("TY_M_AC_28D5W379");
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                this.DbConnector.CommandClear(); // ANTPMEF
                this.DbConnector.Attach("TY_P_AC_36CAU845", sM1SAGB, this.CBH01_M1VNCD.GetValue(), this.DTP01_M1DTED.GetString()); // 삭제전 존재 체크
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                if (iCnt == 0)
                {
                    this.ShowMessage("TY_M_GB_23NAD870");
                    e.Successed = false;
                    return;
                }

                iCnt = 0;

                this.DbConnector.CommandClear(); // ANTPMEF
                this.DbConnector.Attach("TY_P_AC_36CB1846", sM1SAGB, this.CBH01_M1VNCD.GetValue(), this.DTP01_M1DTED.GetString()); // 삭제전 전표 존재 체크
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                if (iCnt > 0)
                {
                    this.ShowMessage("TY_M_MR_3174H522");
                    e.Successed = false;
                    return;
                }
            }

        }
        #endregion

        #region Description : 배치 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sSABUN = TYUserInfo.EmpNo;
            string sM1GUBN = string.Empty;
            string sM1SAGB = string.Empty;
            string sOUTMSG = string.Empty;

            sM1GUBN = this.CBO01_M1GUBN.GetValue().ToString(); // 지급구분
            sM1SAGB = this.CBO01_M1SAGB.GetValue().ToString(); // 지역구분

            //if (sM1SAGB != "")
            //{
            //    sM1SAGB = sM1SAGB == "1" ? "1" : "6";
            //}

            if (this.CBO01_M1GUBN.GetValue().ToString() == "")
            { sM1GUBN = " ";}

            if (this.CBO01_M1SAGB.GetValue().ToString() == "")
            { sM1SAGB = " ";}

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                //"TY_P_AC_2586C110",
                "TY_P_AC_7B39J930",
                this.DTP01_M1DTED.GetValue(),
                this.DTP01_GSTDATE.GetValue(),
                this.DTP01_GEDDATE.GetValue(),
                sM1GUBN.ToString(),
                sM1SAGB.ToString(),
                this.CBH01_M1VNCD.GetValue(),
                this.CBO01_GOKCR.GetValue(),
                sSABUN.ToString(),
                sOUTMSG.ToString()
                );

            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUTMSG.Substring(0, 2) == "ER")
            {
                this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            else
            {
                this.ShowCustomMessage(sOUTMSG, "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}