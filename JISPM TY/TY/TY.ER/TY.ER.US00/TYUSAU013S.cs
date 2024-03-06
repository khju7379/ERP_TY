using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using System.Drawing;

namespace TY.ER.US00
{
    /// <summary>
    /// BIN GATE메세지 Log 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2022.02.08 13:25
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_C28DM029 : BIN GATE 메세지 LOG 테이블 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_C28DN030 : BIN GATE 메세지 LOG 테이블 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  GMGKCODE : 곡종
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    ///  GMBINNO : BINNO
    ///  GMTAG : TAG
    /// </summary>
    public partial class TYUSAU013S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYUSAU013S()
        {
            InitializeComponent();
        }

        private void TYUSAU013S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_SDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.DTP02_SDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            this.DTP02_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            tabControl1.TabIndex = 0;            

            this.SetStartingFocus(this.TXT01_GABINNO);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN62_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_C28EJ034.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_C28EI033", this.TXT01_GABINNO.GetValue(), this.TXT01_GATAG.GetValue(), this.TXT01_GAGROUP.GetValue());
            this.FPS91_TY_S_US_C28EJ034.SetValue(this.DbConnector.ExecuteDataTable());

            if( this.FPS91_TY_S_US_C28EJ034.CurrentRowCount > 0 )
            {
                for (int i = 0; i < this.FPS91_TY_S_US_C28EJ034.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_US_C28EJ034.GetValue(i, "GAOPENST").ToString() == "OPEN")
                    {
                        this.FPS91_TY_S_US_C28EJ034_Sheet1.Cells[i, 8].ForeColor = Color.Blue;
                    }
                    else
                    {
                        this.FPS91_TY_S_US_C28EJ034_Sheet1.Cells[i, 8].ForeColor = Color.Red;
                    }
                }
            }
        }
        
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_C28DN030.Initialize();            

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_C28DM029", this.DTP01_SDATE.GetString(), this.DTP01_EDATE.GetString(), this.TXT01_GMTAG.GetValue(),
                                                        this.TXT01_GMBINNO.GetValue(), this.CBH01_GMGKCODE.GetValue(),
                                                        this.TXT01_GMMSGTEXT.GetValue());
            this.FPS91_TY_S_US_C28DN030.SetValue(this.DbConnector.ExecuteDataTable());
        }

        private void BTN63_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_C299J037.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_C299I036", this.DTP02_SDATE.GetString(), this.DTP02_EDATE.GetString(), 
                                                        this.TXT01_GLTAG.GetValue(),
                                                        this.TXT01_GLBINNO.GetValue(),
                                                        this.TXT01_GLSABUN.GetValue(), 
                                                        this.CBH01_GLGKCODE.GetValue() 
                                                        );
            this.FPS91_TY_S_US_C299J037.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_US_C299J037.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_US_C299J037.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_US_C299J037.GetValue(i, "GLGATEVAL").ToString() == "OPEN")
                    {
                        this.FPS91_TY_S_US_C299J037_Sheet1.Cells[i, 2].ForeColor = Color.Blue;
                    }
                    else
                    {
                        this.FPS91_TY_S_US_C299J037_Sheet1.Cells[i, 2].ForeColor = Color.Red;
                    }
                }
            }
        }
        #endregion          

       

       


    }
}
