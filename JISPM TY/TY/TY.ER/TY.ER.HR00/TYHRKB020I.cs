using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 학자금 대상자관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.03.14 17:21
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_73EI2942 : 학자금 지급대상자 상세내역 확인
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  HKSCRBUNGI : 지원분기
    ///  HKSCRDATE : 지원일자
    ///  HKSCRYEAR : 지원년도
    /// </summary>
    public partial class TYHRKB020I : TYBase
    {
        private DataTable fdt;
        private string fsHLGUBN;

        #region  Description : 폼 로드 이벤트
        public TYHRKB020I(DataTable dt, string sHLGUBN)
        {
            InitializeComponent();

            fdt = dt;
            fsHLGUBN = sHLGUBN;
        }

        private void TYHRKB020I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

          
            CBO01_HKSHLGUBN.SetValue(fsHLGUBN);

            TXT01_HKSCRYEAR.SetValue(DateTime.Now.ToString("yyyy"));
            DTP01_HKSCRDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            UP_Set_BungiCompute();
            DTP01_HKSPYDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));

            if (fsHLGUBN == "1")
            {
                LBL51_HKSCRBUNGI.Text = "지원분기";
            }
            else
            {
                LBL51_HKSCRBUNGI.Text = "지원학기";
            }

            this.FPS91_TY_S_HR_73EI2942.Initialize();
            this.FPS91_TY_S_HR_73EI2942.SetValue(fdt);
        }
        #endregion

        #region  Description : 분기 계산 함수
        private void UP_Set_BungiCompute()
        {
            string sBungi = string.Empty;

            string sDate = DTP01_HKSCRDATE.GetString().ToString();

            if (fsHLGUBN == "1")
            {
                if (Convert.ToInt16(sDate.Substring(4, 2)) > 1 && Convert.ToInt16(sDate.Substring(4, 2)) <= 3)
                {
                    sBungi = "1";
                }
                else if (Convert.ToInt16(sDate.Substring(4, 2)) > 3 && Convert.ToInt16(sDate.Substring(4, 2)) <= 6)
                {
                    sBungi = "2";
                }
                else if (Convert.ToInt16(sDate.Substring(4, 2)) > 6 && Convert.ToInt16(sDate.Substring(4, 2)) <= 9)
                {
                    sBungi = "3";
                }
                else
                {
                    sBungi = "4";
                }
            }
            else
            {
                if (Convert.ToInt16(sDate.Substring(4, 2)) > 1 && Convert.ToInt16(sDate.Substring(4, 2)) <= 6)
                {
                    sBungi = "1";
                }
                else
                {
                    sBungi = "2";
                }
            }

            TXT01_HKSCRBUNGI.SetValue(sBungi);
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sValue = string.Empty;

            double dTotal = 0;

            if (this.FPS91_TY_S_HR_73EI2942.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_HR_73EI2942.CurrentRowCount; i++)
                {
                    sValue = this.FPS91_TY_S_HR_73EI2942.ActiveSheet.RowHeader.Cells[i, 0].Text;

                    if (sValue == "U")
                    {
                        //지급총액 계산
                        dTotal = Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_HR_73EI2942.ActiveSheet.Cells[i, 20].Text.ToString())) +
                                  Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_HR_73EI2942.ActiveSheet.Cells[i, 21].Text.ToString())) +
                                  Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_HR_73EI2942.ActiveSheet.Cells[i, 22].Text.ToString()));

                        this.FPS91_TY_S_HR_73EI2942.SetValue(i, "HKSTOTALAMT", dTotal.ToString());

                        this.FPS91_TY_S_HR_73EI2942.ActiveSheet.RowHeader.Cells[i, 0].Text = "";
                    }
                }
            }

            this.ShowMessage("TY_M_GB_23NAD873");
        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : DTP01_HKSCRDATE_ValueChanged 이벤트
        private void DTP01_HKSCRDATE_ValueChanged(object sender, EventArgs e)
        {
            UP_Set_BungiCompute();
        }
        #endregion      

        #region  Description : FPS91_TY_S_HR_73EI2942_LeaveCell 이벤트
        private void FPS91_TY_S_HR_73EI2942_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            if (e.Column != 20 && e.Column != 21 && e.Column != 22 )
                return;

            double dTotal = Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_HR_73EI2942.GetValue(e.Row, "HKSIPHAKAMT").ToString().Trim())) +
                            Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_HR_73EI2942.GetValue(e.Row, "HKSDUROKAMT").ToString().Trim())) +
                            Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_HR_73EI2942.GetValue(e.Row, "HKSHAKSANGAMT").ToString().Trim()));

            this.FPS91_TY_S_HR_73EI2942.SetValue(e.Row, "HKSTOTALAMT", dTotal.ToString());
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            string sValue = string.Empty;

            if (this.FPS91_TY_S_HR_73EI2942.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_HR_73EI2942.CurrentRowCount; i++)
                {
                    sValue = this.FPS91_TY_S_HR_73EI2942.ActiveSheet.RowHeader.Cells[i, 0].Text;

                    if (sValue == "D")
                    {
                        this.FPS91_TY_S_HR_73EI2942.ActiveSheet.RemoveRows(i, 1);
                    }
                }
            }

            this.ShowMessage("TY_M_GB_23NAD874");
        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : 확정 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            Int16 iAPPCNT = 0;

            System.Collections.Generic.List<object[]> datam = new System.Collections.Generic.List<object[]>();
            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

            if (this.FPS91_TY_S_HR_73EI2942.CurrentRowCount > 0)
            {
                
                for (int i = 0; i < this.FPS91_TY_S_HR_73EI2942.CurrentRowCount; i++)
                {
                     this.DbConnector.CommandClear();
                     this.DbConnector.Attach("TY_P_HR_73DAQ899", TYUserInfo.SecureKey, TYUserInfo.PerAuth,
                                                                 this.FPS91_TY_S_HR_73EI2942.ActiveSheet.Cells[i, 0].Text.ToString(), 
                                                                 this.FPS91_TY_S_HR_73EI2942.ActiveSheet.Cells[i, 2].Text.ToString().Substring(0,4),
                                                                 this.FPS91_TY_S_HR_73EI2942.ActiveSheet.Cells[i, 2].Text.ToString().Substring(5,3));
                     DataTable dt = this.DbConnector.ExecuteDataTable();
                     if (dt.Rows.Count > 0)
                     {
                         
                         //지급총액 또는 장학금이 있는경우만 학년증가 한다.
                         //휴학 , 졸업은 제외
                         if (Convert.ToDouble(this.FPS91_TY_S_HR_73EI2942.ActiveSheet.Cells[i, 23].Text.ToString()) > 0 ||
                             Convert.ToDouble(this.FPS91_TY_S_HR_73EI2942.ActiveSheet.Cells[i, 24].Text.ToString()) > 0)
                         {
                             //지원횟수
                             iAPPCNT = Convert.ToInt16(dt.Rows[0]["APPCNT"].ToString());

                             iAPPCNT += 1;

                             datam.Add(new object[] { UP_Get_GradeCompute(dt.Rows[0]["HKHLGUBN"].ToString(), dt.Rows[0]["HKHAKYEAR"].ToString(), iAPPCNT.ToString()),
                                                     TXT01_HKSCRBUNGI.GetValue().ToString(),
                                                     TYUserInfo.EmpNo,
                                                     dt.Rows[0]["HKSABUN"].ToString(),
                                                     dt.Rows[0]["HKYEAR"].ToString(),
                                                     dt.Rows[0]["HKSSEQ"].ToString()
                                                   });
                         }

                         datas.Add(new object[] {
                                            CBO01_HKSHLGUBN.GetValue().ToString() == "1" ? "3" : "5",
                                            TXT01_HKSCRYEAR.GetValue().ToString(),
                                            TXT01_HKSCRBUNGI.GetValue().ToString(),
                                            DTP01_HKSCRDATE.GetString().ToString(),
                                            this.FPS91_TY_S_HR_73EI2942.ActiveSheet.Cells[i, 0].Text.ToString(),
                                            this.FPS91_TY_S_HR_73EI2942.ActiveSheet.Cells[i, 2].Text.ToString().Substring(0,4),
                                            this.FPS91_TY_S_HR_73EI2942.ActiveSheet.Cells[i, 2].Text.ToString().Substring(5,3),
                                            dt.Rows[0]["KBJKCD"].ToString(),
                                            dt.Rows[0]["KBBUSEO"].ToString(),
                                            UP_Get_GradeCompute(dt.Rows[0]["HKHLGUBN"].ToString(), dt.Rows[0]["HKHAKYEAR"].ToString(), iAPPCNT.ToString()),
                                            TXT01_HKSCRBUNGI.GetValue().ToString(),
                                            Get_Numeric(this.FPS91_TY_S_HR_73EI2942.ActiveSheet.Cells[i, 20].Text.ToString()),
                                            Get_Numeric(this.FPS91_TY_S_HR_73EI2942.ActiveSheet.Cells[i, 21].Text.ToString()),
                                            Get_Numeric(this.FPS91_TY_S_HR_73EI2942.ActiveSheet.Cells[i, 22].Text.ToString()),
                                            Get_Numeric(this.FPS91_TY_S_HR_73EI2942.ActiveSheet.Cells[i, 23].Text.ToString()),
                                            Get_Numeric(this.FPS91_TY_S_HR_73EI2942.ActiveSheet.Cells[i, 24].Text.ToString()),
                                            DTP01_HKSPYDATE.GetString().ToString().Substring(0,6),
                                            "N",
                                            this.FPS91_TY_S_HR_73EI2942.ActiveSheet.Cells[i, 25].Text.ToString(),
                                            TYUserInfo.EmpNo
                                            });
                     }
                }

                if (datas.Count > 0)
                {
                    this.DbConnector.CommandClear();
                    foreach (object[] data in datas)
                    {
                        this.DbConnector.Attach("TY_P_HR_73GEJ953", data);
                    }
                    foreach (object[] data in datam)
                    {
                        this.DbConnector.Attach("TY_P_HR_73H8Y955", data);
                    }
                    this.DbConnector.ExecuteTranQueryList();
                }
                
            }

            this.ShowMessage("TY_M_GB_3A81B997");

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            string sValue = string.Empty;
            
            if (this.FPS91_TY_S_HR_73EI2942.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_HR_73EI2942.CurrentRowCount; i++)
                {
                    sValue = this.FPS91_TY_S_HR_73EI2942.ActiveSheet.RowHeader.Cells[i, 0].Text;

                    if (sValue == "U")
                    {
                        this.ShowCustomMessage("저장되지 않은 자료가 존재합니다! 저장 버튼을 클릭후 확정하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return; 
                    }
                    if (sValue == "D")
                    {
                        this.ShowCustomMessage("삭제되지 않은 자료가 존재합니다! 삭제 버튼을 클릭후 확정하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }

                    //dTotal += Convert.ToDouble(Get_Numeric(this.FPS91_TY_S_HR_73EI2942.ActiveSheet.Cells[i, 23].Text.ToString()));
                }                
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_73HBX958", CBO01_HKSHLGUBN.GetValue().ToString() == "1" ? "3" : "5",
                                                        TXT01_HKSCRYEAR.GetValue().ToString(),
                                                        TXT01_HKSCRBUNGI.GetValue().ToString(),
                                                        DTP01_HKSCRDATE.GetString().ToString()
                                                        );
            Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
            if (iCnt > 0)
            {
                this.ShowCustomMessage("해당년도 분기에 지급된 내역이 존재합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }         
            
            if (!this.ShowMessage("TY_M_GB_3A81A996"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : 학년 계산 함수
        private string  UP_Get_GradeCompute(string sHKHLGUBN,  string sNowGrade, string sAppCnt)
        {
            string sGrade = string.Empty;

            if (sHKHLGUBN == "3")  //고등학교
            {
                sGrade = Convert.ToString(Math.Floor(   (Convert.ToDouble(sAppCnt) / 4) + 0.5  ));
            }
            else if (sHKHLGUBN == "4" || sHKHLGUBN == "5")  //전문대,  대학요
            {
                sGrade = Convert.ToString(Math.Floor((Convert.ToDouble(sAppCnt) / 2) + 0.5));
            }

            if (sGrade == "0")
            {
                sGrade = sNowGrade;
            }

            return sGrade;
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

       

    }
}
