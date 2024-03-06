using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;

namespace TY.ER.HR00
{
    /// <summary>
    /// 연말정산 제출자료 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.06.15 15:49
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  SAV : 저장
    /// </summary>
    public partial class TYHRNT001S : TYBase
    {
        private string fsComPany = string.Empty;

        private string fsFixGubn = string.Empty;

        #region  Description : 폼 로드 이벤트
        public TYHRNT001S()
        {
            InitializeComponent();            
        }

        private void TYHRNT001S_Load(object sender, System.EventArgs e)
        {
            
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_7CDIL268, "BTNPOP");

            this.CBH01_KBBSTEAM.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            this.CBH01_KBSABUN.SetReadOnly(true);
            this.CBH01_KBBSTEAM.SetReadOnly(true);
            this.DTP01_KBIDATE.SetReadOnly(true);

            this.CBH01_KBSABUN.SetValue(TYUserInfo.EmpNo);

            UP_Spread_Title();

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

            

        #region  Description : 개인 기본정보 이벤트
        private void UP_InSaDataBing()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BBGV367", "", this.CBH01_KBSABUN.GetValue().ToString(), TYUserInfo.SecureKey, "Y");
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                CBH01_KBBSTEAM.SetValue(dt.Rows[0]["KBBSTEAM"].ToString());
                CBH01_KBJKCD.SetValue(dt.Rows[0]["KBJKCD"].ToString());
                DTP01_KBIDATE.SetValue(dt.Rows[0]["KBIDATE"].ToString());
                TXT01_KBJUSO.SetValue(dt.Rows[0]["KBJUSO"].ToString());
            }
        }
        #endregion        

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            //인사기본정보
            UP_InSaDataBing();

            FPS91_TY_S_HR_7CDIL268.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_7CDIK267", "TY", this.CBH01_KBSABUN.GetValue().ToString());
            FPS91_TY_S_HR_7CDIL268.SetValue(this.DbConnector.ExecuteDataTable());

            if (FPS91_TY_S_HR_7CDIL268.CurrentRowCount > 0)
            {
                

                for (int i = 0; i < this.FPS91_TY_S_HR_7CDIL268.CurrentRowCount; i++)
                {

                    if (this.FPS91_TY_S_HR_7CDIL268.GetValue(i, "ADDEDTAX").ToString() == "Y")
                    {
                        TButtonCellType tButtonCellType = new TButtonCellType();
                        tButtonCellType.TextAlign = FarPoint.Win.ButtonTextAlign.TextRightPictLeft;
                        tButtonCellType.TextOrientation = FarPoint.Win.TextOrientation.TextHorizontal;
                        tButtonCellType.Text = "보 기";
                        tButtonCellType.Picture = global::TY.Service.Library.Properties.Resources.expand4;
                        this.FPS91_TY_S_HR_7CDIL268.ActiveSheet.Cells[i, 0].CellType = tButtonCellType;                        
                    }
                    else
                    {
                        TButtonCellType tButtonCellType = new TButtonCellType();
                        tButtonCellType.TextAlign = FarPoint.Win.ButtonTextAlign.TextRightPictLeft;
                        tButtonCellType.TextOrientation = FarPoint.Win.TextOrientation.TextHorizontal;
                        tButtonCellType.Text = "제 출";                        
                        tButtonCellType.TextColor = Color.Blue;
                        tButtonCellType.Picture = global::TY.Service.Library.Properties.Resources.it_write;
                        this.FPS91_TY_S_HR_7CDIL268.ActiveSheet.Cells[i, 0].CellType = tButtonCellType;

                        //진행중인 문서 폰트 색상 변경
                        for (int j = 1; j < this.FPS91_TY_S_HR_7CDIL268.ActiveSheet.Columns.Count; j++)
                        {
                            this.FPS91_TY_S_HR_7CDIL268.ActiveSheet.Cells[i, j].ForeColor = Color.Blue;
                            this.FPS91_TY_S_HR_7CDIL268.ActiveSheet.Cells[i, j].Font = new Font("굴림", 10, FontStyle.Bold);
                        }
                    }
                }
            }
        }
        #endregion        

        #region Description : 스프레드 타이틀 변경
        private void UP_Spread_Title()
        {
            this.FPS91_TY_S_HR_7CDIL268_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_HR_7CDIL268_Sheet1.RowHeaderColumnCount = 1;

            for (int i = 0; i < 15; i++)
            {
                this.FPS91_TY_S_HR_7CDIL268_Sheet1.AddColumnHeaderSpanCell(0, i, 2, 1);
            }

            this.FPS91_TY_S_HR_7CDIL268_Sheet1.ColumnHeader.Cells[1, 0].Value = "";
            this.FPS91_TY_S_HR_7CDIL268_Sheet1.ColumnHeader.Cells[1, 1].Value = "회사구분";
            this.FPS91_TY_S_HR_7CDIL268_Sheet1.ColumnHeader.Cells[1, 2].Value = "귀속년도";
            this.FPS91_TY_S_HR_7CDIL268_Sheet1.ColumnHeader.Cells[1, 3].Value = "사 번";
            this.FPS91_TY_S_HR_7CDIL268_Sheet1.ColumnHeader.Cells[1, 4].Value = "성 명";
            this.FPS91_TY_S_HR_7CDIL268_Sheet1.ColumnHeader.Cells[1, 5].Value = "정산구분";
            this.FPS91_TY_S_HR_7CDIL268_Sheet1.ColumnHeader.Cells[1, 6].Value = "정산구분";
            this.FPS91_TY_S_HR_7CDIL268_Sheet1.ColumnHeader.Cells[1, 7].Value = "개인확정";
            this.FPS91_TY_S_HR_7CDIL268_Sheet1.ColumnHeader.Cells[1, 8].Value = "첨부파일";
            this.FPS91_TY_S_HR_7CDIL268_Sheet1.ColumnHeader.Cells[1, 9].Value = "신고서확정";
            this.FPS91_TY_S_HR_7CDIL268_Sheet1.ColumnHeader.Cells[1, 10].Value = "정산확정";
            this.FPS91_TY_S_HR_7CDIL268_Sheet1.ColumnHeader.Cells[1, 11].Value = "상 태";
            this.FPS91_TY_S_HR_7CDIL268_Sheet1.ColumnHeader.Cells[1, 12].Value = "총 소득";
            this.FPS91_TY_S_HR_7CDIL268_Sheet1.ColumnHeader.Cells[1, 13].Value = "근로소득공제";
            this.FPS91_TY_S_HR_7CDIL268_Sheet1.ColumnHeader.Cells[1, 14].Value = "근로소득금액";

            this.FPS91_TY_S_HR_7CDIL268_Sheet1.AddColumnHeaderSpanCell(0, 15, 1, 2);
            this.FPS91_TY_S_HR_7CDIL268_Sheet1.ColumnHeader.Cells[0, 15].Value = "결정세액";
            this.FPS91_TY_S_HR_7CDIL268_Sheet1.ColumnHeader.Cells[1, 15].Value = "소득세";
            this.FPS91_TY_S_HR_7CDIL268_Sheet1.ColumnHeader.Cells[1, 16].Value = "주민세";

            this.FPS91_TY_S_HR_7CDIL268_Sheet1.AddColumnHeaderSpanCell(0, 17, 1, 2);
            this.FPS91_TY_S_HR_7CDIL268_Sheet1.ColumnHeader.Cells[0, 17].Value = "전 근무지 세액";
            this.FPS91_TY_S_HR_7CDIL268_Sheet1.ColumnHeader.Cells[1, 17].Value = "소득세";
            this.FPS91_TY_S_HR_7CDIL268_Sheet1.ColumnHeader.Cells[1, 18].Value = "주민세";

            this.FPS91_TY_S_HR_7CDIL268_Sheet1.AddColumnHeaderSpanCell(0, 19, 1, 2);
            this.FPS91_TY_S_HR_7CDIL268_Sheet1.ColumnHeader.Cells[0, 19].Value = "현 근무지 세액";
            this.FPS91_TY_S_HR_7CDIL268_Sheet1.ColumnHeader.Cells[1, 19].Value = "소득세";
            this.FPS91_TY_S_HR_7CDIL268_Sheet1.ColumnHeader.Cells[1, 20].Value = "주민세";

            this.FPS91_TY_S_HR_7CDIL268_Sheet1.AddColumnHeaderSpanCell(0, 21, 1, 2);
            this.FPS91_TY_S_HR_7CDIL268_Sheet1.ColumnHeader.Cells[0, 21].Value = "차감징수세액";
            this.FPS91_TY_S_HR_7CDIL268_Sheet1.ColumnHeader.Cells[1, 21].Value = "소득세";
            this.FPS91_TY_S_HR_7CDIL268_Sheet1.ColumnHeader.Cells[1, 22].Value = "주민세";            

            this.FPS91_TY_S_HR_7CDIL268_Sheet1.ColumnHeader.Cells[0, 15].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_7CDIL268_Sheet1.ColumnHeader.Cells[0, 17].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_7CDIL268_Sheet1.ColumnHeader.Cells[0, 19].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_7CDIL268_Sheet1.ColumnHeader.Cells[0, 21].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion

        #region Description : FPS91_TY_S_HR_7CDIL268_ButtonClicked
        private void FPS91_TY_S_HR_7CDIL268_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "0")
            {
                if (this.OpenModalPopup(new TYHRNT001I(this.FPS91_TY_S_HR_7CDIL268.GetValue("ADCOMPANY").ToString(),
                                                    this.FPS91_TY_S_HR_7CDIL268.GetValue("ADYEAR").ToString(),
                                                    this.FPS91_TY_S_HR_7CDIL268.GetValue("ADSABUN").ToString()                                                    
                                                    )) == System.Windows.Forms.DialogResult.OK)
                   this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

    }
}
