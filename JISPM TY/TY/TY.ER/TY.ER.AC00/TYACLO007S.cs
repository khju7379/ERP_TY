using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.ER.GB00;
using GrapeCity.ActiveReports;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using FarPoint.Win.Spread.CellType;

namespace TY.ER.AC00
{
    /// <summary>
    /// 차입금 잔액조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2018.07.03 13:11
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_873I5320 : 차입금 잔액조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_87OG4467 : 차입금 계약별 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  LOCCURRTYPE : 통화유형
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYACLO007S : TYBase
    {
        #region Description : 폼 로드
        public TYACLO007S()
        {
            InitializeComponent();
        }

        private void TYACLO007S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(System.DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            this.SetStartingFocus(this.DTP01_STDATE);

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            if (this.TXT01_STYEAR.GetValue().ToString() == "" && this.TXT01_EDYEAR.GetValue().ToString() != "")
            {
                this.ShowMessage("TY_M_AC_87O9G463");
                this.TXT01_STYEAR.Focus();

                return;
            }

            if (this.TXT01_STYEAR.GetValue().ToString() != "" && this.TXT01_EDYEAR.GetValue().ToString() == "")
            {
                this.ShowMessage("TY_M_AC_87O9G463");
                this.TXT01_EDYEAR.Focus();

                return;
            }
            
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            // 20180827 수정전 소스
            //this.DbConnector.Attach("TY_P_AC_87OFP466", Get_Date(this.DTP01_STDATE.GetValue().ToString()),
            //                                            Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
            //                                            this.CBH01_LOCBANKCD.GetValue().ToString(),
            //                                            this.TXT01_STYEAR.GetValue().ToString(),
            //                                            this.TXT01_EDYEAR.GetValue().ToString(),
            //                                            Get_Date(this.DTP01_STDATE.GetValue().ToString()),
            //                                            Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
            //                                            this.CBH01_LOCBANKCD.GetValue().ToString(),
            //                                            this.TXT01_STYEAR.GetValue().ToString(),
            //                                            this.TXT01_EDYEAR.GetValue().ToString()
            //                                            );

            // 20180827 수정후 소스
            // 회전일 경우 잔액 구하는 로직 변경
            // 잔액 = 실행금액 - 상환금액
            // 잔액 > 0 이면 실행, 잔액 = 0 이면 = 상환
            //this.DbConnector.Attach("TY_P_AC_88RGN640", Get_Date(this.DTP01_STDATE.GetValue().ToString()),
            //                                            Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
            //                                            this.CBH01_LOCBANKCD.GetValue().ToString(),
            //                                            this.TXT01_STYEAR.GetValue().ToString(),
            //                                            this.TXT01_EDYEAR.GetValue().ToString(),
            //                                            Get_Date(this.DTP01_STDATE.GetValue().ToString()),
            //                                            Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
            //                                            this.CBH01_LOCBANKCD.GetValue().ToString(),
            //                                            this.TXT01_STYEAR.GetValue().ToString(),
            //                                            this.TXT01_EDYEAR.GetValue().ToString()
            //                                            );


            // 20180911 수정후 소스
            // 회전일 경우 건 BY 건으로 나타나게 함
            this.DbConnector.Attach("TY_P_AC_89BHN720", Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                                                        Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                                                        this.CBH01_LOCBANKCD.GetValue().ToString(),
                                                        this.TXT01_STYEAR.GetValue().ToString(),
                                                        this.TXT01_EDYEAR.GetValue().ToString(),
                                                        Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                                                        Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                                                        this.CBH01_LOCBANKCD.GetValue().ToString(),
                                                        this.TXT01_STYEAR.GetValue().ToString(),
                                                        this.TXT01_EDYEAR.GetValue().ToString()
                                                        );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_87OG4467.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_AC_87OG4467.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_87OG4467.GetValue(i, "TYDESC1").ToString() == "일반")
                {
                    // 특정 ROW 글자 크기 변경
                    this.FPS91_TY_S_AC_87OG4467_Sheet1.Cells[i, 4].ForeColor = Color.Blue;

                    this.FPS91_TY_S_AC_87OG4467_Sheet1.Cells[i, 4].Font = new Font("굴림", 9, FontStyle.Bold);

                    if (this.FPS91_TY_S_AC_87OG4467.GetValue(i, "STATUSNM").ToString() == "실행")
                    {
                        // 특정 ROW 글자 크기 변경
                        this.FPS91_TY_S_AC_87OG4467_Sheet1.Cells[i, 7].ForeColor  = Color.Blue;
                        this.FPS91_TY_S_AC_87OG4467_Sheet1.Cells[i, 11].ForeColor = Color.Blue;

                        this.FPS91_TY_S_AC_87OG4467_Sheet1.Cells[i, 7].Font  = new Font("굴림", 9, FontStyle.Bold);
                        this.FPS91_TY_S_AC_87OG4467_Sheet1.Cells[i, 11].Font = new Font("굴림", 9, FontStyle.Bold);
                    }
                    else if (this.FPS91_TY_S_AC_87OG4467.GetValue(i, "STATUSNM").ToString() == "상환")
                    {
                        // 특정 ROW 글자 크기 변경
                        this.FPS91_TY_S_AC_87OG4467_Sheet1.Cells[i, 7].ForeColor  = Color.Red;
                        this.FPS91_TY_S_AC_87OG4467_Sheet1.Cells[i, 11].ForeColor = Color.Red;

                        this.FPS91_TY_S_AC_87OG4467_Sheet1.Cells[i, 7].Font  = new Font("굴림", 9, FontStyle.Bold);
                        this.FPS91_TY_S_AC_87OG4467_Sheet1.Cells[i, 11].Font = new Font("굴림", 9, FontStyle.Bold);
                    }
                    else if (this.FPS91_TY_S_AC_87OG4467.GetValue(i, "STATUSNM").ToString() == "유동성 대체")
                    {
                        // 특정 ROW 글자 크기 변경
                        this.FPS91_TY_S_AC_87OG4467_Sheet1.Cells[i, 7].ForeColor  = Color.LimeGreen;
                        this.FPS91_TY_S_AC_87OG4467_Sheet1.Cells[i, 11].ForeColor = Color.LimeGreen;

                        this.FPS91_TY_S_AC_87OG4467_Sheet1.Cells[i, 7].Font  = new Font("굴림", 9, FontStyle.Bold);
                        this.FPS91_TY_S_AC_87OG4467_Sheet1.Cells[i, 11].Font = new Font("굴림", 9, FontStyle.Bold);
                    }
                    else if (this.FPS91_TY_S_AC_87OG4467.GetValue(i, "STATUSNM").ToString() == "유동성 재대체")
                    {
                        // 특정 ROW 글자 크기 변경
                        this.FPS91_TY_S_AC_87OG4467_Sheet1.Cells[i, 7].ForeColor  = Color.Peru;
                        this.FPS91_TY_S_AC_87OG4467_Sheet1.Cells[i, 11].ForeColor = Color.Peru;

                        this.FPS91_TY_S_AC_87OG4467_Sheet1.Cells[i, 7].Font  = new Font("굴림", 9, FontStyle.Bold);
                        this.FPS91_TY_S_AC_87OG4467_Sheet1.Cells[i, 11].Font = new Font("굴림", 9, FontStyle.Bold);
                    }
                }

                if (this.FPS91_TY_S_AC_87OG4467.GetValue(i, "TYDESC1").ToString() == "회전")
                {
                    // 특정 ROW 글자 크기 변경
                    this.FPS91_TY_S_AC_87OG4467_Sheet1.Cells[i, 4].ForeColor = Color.Red;

                    this.FPS91_TY_S_AC_87OG4467_Sheet1.Cells[i, 4].Font = new Font("굴림", 9, FontStyle.Bold);

                    if (this.FPS91_TY_S_AC_87OG4467.GetValue(i, "STATUSNM").ToString() == "실행")
                    {
                        // 특정 ROW 글자 크기 변경
                        this.FPS91_TY_S_AC_87OG4467_Sheet1.Cells[i, 7].ForeColor  = Color.Blue;
                        this.FPS91_TY_S_AC_87OG4467_Sheet1.Cells[i, 11].ForeColor = Color.Blue;

                        this.FPS91_TY_S_AC_87OG4467_Sheet1.Cells[i, 7].Font  = new Font("굴림", 9, FontStyle.Bold);
                        this.FPS91_TY_S_AC_87OG4467_Sheet1.Cells[i, 11].Font = new Font("굴림", 9, FontStyle.Bold);
                    }
                    else if (this.FPS91_TY_S_AC_87OG4467.GetValue(i, "STATUSNM").ToString() == "상환")
                    {
                        // 특정 ROW 글자 크기 변경
                        this.FPS91_TY_S_AC_87OG4467_Sheet1.Cells[i, 7].ForeColor  = Color.Red;
                        this.FPS91_TY_S_AC_87OG4467_Sheet1.Cells[i, 11].ForeColor = Color.Red;

                        this.FPS91_TY_S_AC_87OG4467_Sheet1.Cells[i, 7].Font  = new Font("굴림", 9, FontStyle.Bold);
                        this.FPS91_TY_S_AC_87OG4467_Sheet1.Cells[i, 11].Font = new Font("굴림", 9, FontStyle.Bold);
                    }
                }
            }
        }
        #endregion
    }
}
