using System;
using System.Data;
using System.Drawing; 
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using GrapeCity.ActiveReports;
using TY.ER.GB00;


namespace TY.ER.AC00
{
    /// <summary>
    /// 기준일별 받을어음 명세서 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.05.02 13:37
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2522E957 : 기준일별 받을어음 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2526C964 : 기준일별 받을어음 명세서
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  E6CDSO : 발생부서
    ///  E6DTCO : 입금일자
    ///  E6CDCL : 거래처코드
    ///  E6IDBG : 상태구분
    /// </summary>
    public partial class TYACEI012S : TYBase
    {

        #region Description : 폼 로드 이벤트
        public TYACEI012S()
        {
            InitializeComponent();
        }

        private void TYACEI012S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_E6DTCO.SetValue(DateTime.Now.ToString("yyyyMMdd"));

            this.CBH01_E6CDSO.DummyValue = this.DTP01_E6DTCO.GetValue();

            CBO01_E6IDBG.SetValue("10"); 

            UP_Spread_DisPlay(true, false, false, false);

            SetStartingFocus(DTP01_E6DTCO);  
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2522E957", DTP01_E6DTCO.GetValue(), CBH01_E6CDSO.GetValue(), CBO01_E6IDBG.GetValue(), CBH01_E6CDCL.GetValue(), CBO01_E6PRGN.GetValue());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (CBO01_E6PRGN.GetValue().ToString()  == "1") //입금일
            {
                UP_Spread_DisPlay(true, false, false, false);
                this.FPS91_TY_S_AC_2528U969.SetValue(this.DbConnector.ExecuteDataTable());
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_2528U969, "E6NONR", "소 계", Color.Yellow);
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_2528U969, "E6NONR", "총 계", Color.YellowGreen);
            }
            else if (CBO01_E6PRGN.GetValue().ToString() == "2")  //거래처
            {
                UP_Spread_DisPlay(false, true, false, false);
                this.FPS91_TY_S_AC_253BN980.SetValue(this.DbConnector.ExecuteDataTable());
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_253BN980, "E6NONR", "소 계", Color.Yellow);
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_253BN980, "E6NONR", "총 계", Color.YellowGreen);  
            }
            else if (CBO01_E6PRGN.GetValue().ToString() == "3") //만기일
            {
                UP_Spread_DisPlay(false, false, true, false);
                this.FPS91_TY_S_AC_2526C964.SetValue(this.DbConnector.ExecuteDataTable());
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_2526C964, "E6NONR", "소 계", Color.Yellow);
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_2526C964, "E6NONR", "총 계", Color.YellowGreen);  
            }
            else //은행
            {
                UP_Spread_DisPlay(false, false, false, true);
                this.FPS91_TY_S_AC_253BP981.SetValue(this.DbConnector.ExecuteDataTable());
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_253BP981, "E6NONR", "소 계", Color.Yellow);
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_253BP981, "E6NONR", "총 계", Color.YellowGreen);  
            }
        }
        #endregion

        #region Description : DTP01_E6DTCO_ValueChanged 이벤트
        private void DTP01_E6DTCO_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_E6CDSO.DummyValue = this.DTP01_E6DTCO.GetValue(); 
        }
        #endregion

        #region Description : 사용자정의 함수(스프레드 선택 함수)
        private void UP_Spread_DisPlay(bool bfpsvalue1,bool bfpsvalue2,bool bfpsvalue3,bool bfpsvalue4 )
        {
            this.FPS91_TY_S_AC_2528U969.Initialize();
            this.FPS91_TY_S_AC_253BN980.Initialize();
            this.FPS91_TY_S_AC_2526C964.Initialize();
            this.FPS91_TY_S_AC_253BP981.Initialize();
            
            this.FPS91_TY_S_AC_2528U969.Visible = bfpsvalue1;
            this.FPS91_TY_S_AC_253BN980.Visible = bfpsvalue2;
            this.FPS91_TY_S_AC_2526C964.Visible = bfpsvalue3;
            this.FPS91_TY_S_AC_253BP981.Visible = bfpsvalue4;
        }
        #endregion

        #region Description : 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            DataTable dtPrt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2522E957", DTP01_E6DTCO.GetValue(), CBH01_E6CDSO.GetValue(), CBO01_E6IDBG.GetValue(), CBH01_E6CDCL.GetValue(), CBO01_E6PRGN.GetValue());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            dtPrt = dt .Clone();

            if (CBO01_E6PRGN.GetValue().ToString() == "1") //입금일
            {
                foreach (DataRow rw in dt.Select("E6DTED <> ''", "E6DTCO, E6DTED ASC "))
                    dtPrt.Rows.Add(rw.ItemArray);
            }
            else if (CBO01_E6PRGN.GetValue().ToString() == "2")  //거래처
            {
                foreach (DataRow rw in dt.Select("E6DTED <> ''", "E6CDCL, E6DTCO, E6DTED ASC "))
                    dtPrt.Rows.Add(rw.ItemArray);
            }
            else if (CBO01_E6PRGN.GetValue().ToString() == "3") //만기일
            {
                foreach (DataRow rw in dt.Select("E6DTED <> ''", "E6DTED, E6DTCO ASC "))
                    dtPrt.Rows.Add(rw.ItemArray);
            }
            else //은행
            {
                foreach (DataRow rw in dt.Select("E6DTED <> ''", "E6NMBK, E6DTCO, E6DTED ASC "))
                    dtPrt.Rows.Add(rw.ItemArray);
            }  

            
            SectionReport rpt = null;

            if (CBO01_E6PRGN.GetValue().ToString() == "1") //입금일
            {
                rpt = new TYACEI012R1(); 
            }
            else if (CBO01_E6PRGN.GetValue().ToString() == "2")  //거래처
            {
                rpt = new TYACEI012R2(); 
            }
            else if (CBO01_E6PRGN.GetValue().ToString() == "3") //만기일
            {
                rpt = new TYACEI012R(); 
            }
            else //은행
            {
                rpt = new TYACEI012R3(); 
            }          

            rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

            (new TYERGB001P(rpt, dtPrt)).ShowDialog();
        }
        #endregion

    }
}
