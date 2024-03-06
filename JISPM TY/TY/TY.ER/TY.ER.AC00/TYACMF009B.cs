using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 펌뱅킹 년말 이월작업 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.11.08 20:05
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2B881212 : 통장거래내역(외화) 월파일 일괄등록
    ///  TY_P_AC_2B886209 : 통장거래내역(원화)  월파일 삭제
    ///  TY_P_AC_2B888210 : 통장거래내역(외화)  월파일 삭제
    ///  TY_P_AC_2B889211 : 통장거래내역(원화) 월파일 일괄등록
    ///  TY_P_AC_2B88Y219	통장거래내역(외화) 존재 체크
    ///  TY_P_AC_2B88Y218	통장거래내역(원화) 존재 체크
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2B9AE223 : 이월작업을 하시겠습니까?
    ///  TY_M_AC_2B9AE222 : 이월작업이완료되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  H1DATE : 거래일자
    /// </summary>
    public partial class TYACMF009B : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYACMF009B()
        {
            InitializeComponent();
        }

        private void TYACMF009B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_H1DATE.SetValue(DateTime.Now.ToString("yyyy"));
            this.DTP02_H1DATE.SetValue(DateTime.Now.AddYears(1).ToString("yyyy"));

            this.SetStartingFocus(this.DTP01_H1DATE); 
        }
        #endregion

        #region Description : 처리 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sOUTMSG = string.Empty;
            this.DbConnector.CommandClear();

            //this.DbConnector.Attach("TY_P_AC_2B886209", this.DTP02_H1DATE.GetString().ToString().Substring(0, 4));
            //this.DbConnector.Attach("TY_P_AC_2B888210", this.DTP02_H1DATE.GetString().ToString().Substring(0, 4));
            //this.DbConnector.Attach("TY_P_AC_2B889211", this.DTP02_H1DATE.GetString().ToString().Substring(0, 4),this.DTP01_H1DATE.GetString().ToString().Substring(0, 4));
            //this.DbConnector.Attach("TY_P_AC_2B881212", this.DTP02_H1DATE.GetString().ToString().Substring(0, 4),this.DTP01_H1DATE.GetString().ToString().Substring(0, 4));            
            //this.DbConnector.ExecuteTranQueryList();

            this.DbConnector.Attach("TY_P_AC_413BJ930", this.DTP01_H1DATE.GetString().ToString().Substring(0, 4), this.DTP02_H1DATE.GetString().ToString().Substring(0, 4), sOUTMSG);
            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUTMSG.ToString().Substring(0, 2) == "OK")
            {
                this.ShowMessage("TY_M_AC_2B9AE222");
            }
            else
            {
                this.ShowMessage("TY_M_AC_2B9AE222");
            };
        }
        #endregion

        #region Description : 처리 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            //Int16 iCnt1 = 0;
            //Int16 iCnt2 = 0;

            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_AC_2B88Y218", this.DTP02_H1DATE.GetString().ToString().Substring(0,4)   + "0101");
            //iCnt1 = Convert.ToInt16(this.DbConnector.ExecuteScalar());

            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_AC_2B88Y219", this.DTP02_H1DATE.GetString().ToString().Substring(0, 4) + "0101");
            //iCnt2 = Convert.ToInt16(this.DbConnector.ExecuteScalar());

            //if (iCnt1 > 0 || iCnt2 > 0)
            //{
            //    this.ShowMessage("TY_M_AC_2B894220");
            //    e.Successed = false;
            //    return;
            //}

            if (!this.ShowMessage("TY_M_AC_2B9AE223"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
