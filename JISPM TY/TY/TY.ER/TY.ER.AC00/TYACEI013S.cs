using System;
using System.Data;
using System.Drawing;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using GrapeCity.ActiveReports;
using TY.ER.GB00;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 기준일별 부도어음 명세서 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.05.03 13:44
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2532E988 : 기준일별 부도어음 명세서
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2532T989 : 기준일별 부도어음 명세서
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  E6CDSO : 발생부서
    ///  E6DTCO : 입금일자
    /// </summary>
    public partial class TYACEI013S : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYACEI013S()
        {
            InitializeComponent();
        }

        private void TYACEI013S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_E6DTCO.SetValue(DateTime.Now.ToString("yyyyMMdd"));

            this.CBH01_E6CDSO.DummyValue = this.DTP01_E6DTCO.GetValue();

            this.SetStartingFocus(this.DTP01_E6DTCO); 
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {

            this.FPS91_TY_S_AC_2532T989.Initialize(); 
            this.DbConnector.CommandClear();            
            this.DbConnector.Attach("TY_P_AC_2532E988", DTP01_E6DTCO.GetValue(), DTP01_E6DTCO.GetValue(), DTP01_E6DTCO.GetValue(), DTP01_E6DTCO.GetValue(), DTP01_E6DTCO.GetValue(),
                                                        DTP01_E6DTCO.GetValue(), DTP01_E6DTCO.GetValue(), CBH01_E6CDSO.GetValue());

            this.FPS91_TY_S_AC_2532T989.SetValue(UP_SuTotal_ds(this.DbConnector.ExecuteDataTable()));


            this.SetSpreadSumRow(this.FPS91_TY_S_AC_2532T989, "E6NONR", "[합    계]", Color.YellowGreen);  
        }
        #endregion

        #region Description : DTP01_E6DTCO_ValueChanged 이벤트
        private void DTP01_E6DTCO_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_E6CDSO.DummyValue = this.DTP01_E6DTCO.GetValue();
        }
        #endregion

        #region Description : 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_2532E988", DTP01_E6DTCO.GetValue(), DTP01_E6DTCO.GetValue(), DTP01_E6DTCO.GetValue(), DTP01_E6DTCO.GetValue(), DTP01_E6DTCO.GetValue(),
                                            DTP01_E6DTCO.GetValue(), DTP01_E6DTCO.GetValue(), CBH01_E6CDSO.GetValue());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            //총계 제외
            //dt.Rows.RemoveAt(dt.Rows.Count - 1);
                        
            SectionReport rpt = new TYACEI013R();

            rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

            (new TYERGB001P(rpt, dt)).ShowDialog();
        }
        #endregion

        #region Description : 합계 계산
        private DataTable UP_SuTotal_ds(DataTable dt)
        {

            // 합계를 보여주기 위한 빈 로우 하나 생성
            DataTable table = new DataTable();

            table = dt.Clone();

            DataRow row;
            
            int nNum = dt.Rows.Count;

            //foreach (DataRow dr in ds.Tables[0].Select("A1YNTB='Y'", "G2CDAC ASC"))
            //    table.Rows.Add(dr.ItemArray);

            if (nNum > 0)
            {

                foreach (DataRow dr in dt.Select("", "NUM ASC"))
                    table.Rows.Add(dr.ItemArray);

                nNum = table.Rows.Count;

                row = table.NewRow();
                table.Rows.InsertAt(row, nNum);


                table.Rows[nNum]["E6NONR"] = "[합    계]";

                table.Rows[nNum]["E6AMNR"] = dt.Compute("Sum(E6AMNR)", "").ToString();
                table.Rows[nNum]["E7INNR"] = dt.Compute("Sum(E7INNR)", "").ToString();
                table.Rows[nNum]["E7INJAN"] = dt.Compute("Sum(E7INJAN)", "").ToString();
                
            }

            return table;
        }
        #endregion


    }
}
