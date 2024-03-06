using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.Data.OleDb;

namespace TY.ER.US00
{
    /// <summary>
    /// 서울 근태엑셀 Upload 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.05.06 14:58
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_3562W600 : 서울근태엑셀 Upload
    ///  TY_P_HR_3562X601 : 서울근태엑셀 조회
    ///  TY_P_HR_35635603 : 서울 근태엑셀 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_3562Z602 : 서울 근태엑셀 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  EXCEL : 엑셀 업데이트
    ///  INQ : 조회
    ///  SAV : 저장
    ///  SEARCH : 찾아보기
    /// </summary>
    public partial class TYUSNJ001I : TYBase
    {
        // 엑셀 서식
        // TANK, VOLUME, LEVEL <= 1열에 명이 나옴
        // 2열부터 TANK, VOLUME, LEVEL 자료가 나옴

        #region Description : 폼로드
        public TYUSNJ001I()
        {
            InitializeComponent();
        }

        private void TYUSNJ001I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.FPS91_TY_S_US_96CBX787.Initialize();
            this.FPS91_TY_S_US_96CBY788.Initialize();

            this.DTP01_GDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));

            Timer tmr = new Timer();

            tmr.Tick += delegate
            {
                tmr.Stop();
                this.DTP01_GDATE.Focus();
            };

            tmr.Interval = 100;
            tmr.Start();
        }
        #endregion

        #region Description : 찾아보기 버튼
        private void BTN61_SEARCH_Click(object sender, EventArgs e)
        {
            OpenFile.Filter = "Excel 97-2003통합 문서(*.xls)|*.xls|Excel 통합 문서 (.xlsx)|*.xlsx|All Files (*.*)|*.*";

            if (this.OpenFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.txtFile.Text = this.OpenFile.FileName;
        }
        #endregion       

        #region Description : 엑셀 UPLOAD 버튼
        private void BTN61_EXCEL_Click(object sender, EventArgs e)
        {
            if (this.txtFile.Text.Trim() != "")
            {
                this.FPS91_TY_S_US_96CBX787.Initialize();
                this.FPS91_TY_S_US_96CBY788.Initialize();

                string strProvider = string.Empty;
                string strQuery = string.Empty;

                strProvider = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + this.txtFile.Text.Trim() + "; Extended Properties=Excel 12.0";

                strQuery = "SELECT * FROM [Sheet1$] "; //  , Sheet1$
                //strQuery = "SELECT * FROM [CM$] "; //  , Sheet1$

                OleDbConnection ExcelCon = new OleDbConnection(strProvider);
                ExcelCon.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(strQuery, strProvider);

                DataSet ds = new DataSet();
                adapter.Fill(ds, "EXCEL");

                this.FPS91_TY_S_US_96CBX787_Sheet1.RowCount = ds.Tables[0].Rows.Count;

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.FPS91_TY_S_US_96CBX787_Sheet1.Cells[i, 0].Value = ds.Tables[0].Rows[i][0].ToString();
                    this.FPS91_TY_S_US_96CBX787_Sheet1.Cells[i, 1].Value = ds.Tables[0].Rows[i][1].ToString();
                    this.FPS91_TY_S_US_96CBX787_Sheet1.Cells[i, 2].Value = ds.Tables[0].Rows[i][2].ToString();
                    this.FPS91_TY_S_US_96CBX787_Sheet1.Cells[i, 3].Value = ds.Tables[0].Rows[i][3].ToString();
                    this.FPS91_TY_S_US_96CBX787_Sheet1.Cells[i, 4].Value = ds.Tables[0].Rows[i][4].ToString();
                    this.FPS91_TY_S_US_96CBX787_Sheet1.Cells[i, 5].Value = ds.Tables[0].Rows[i][5].ToString();
                }

                SetFocus(this.DTP01_GDATE);

                this.ShowMessage("TY_M_UT_6APJV532");
            }
            else
            {
                this.ShowMessage("TY_M_AC_31B1C623");
            }
            
        }
        #endregion

        #region Description : 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            string sJUMIN = string.Empty;
            string sSabun = string.Empty;

            int i = 0;

            for (i = 0; i < this.FPS91_TY_S_US_96CBX787.ActiveSheet.RowCount; i++)
            {
                sJUMIN = this.FPS91_TY_S_US_96CBX787.GetValue(i, "JUMIN1").ToString() + this.FPS91_TY_S_US_96CBX787.GetValue(i, "JUMIN2").ToString();

                // 해당년도 주민번호에 해당하는 순번 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_96CGZ791", Get_Date(this.DTP01_GDATE.GetValue().ToString()).ToString().Substring(0, 4),
                                                            TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y", 
                                                            sJUMIN.ToString());
                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sSabun = dt.Rows[0]["HMSABUN"].ToString();
                }
                else
                {
                    // 항운노조 년별 순번 가져오기
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_96DBZ800", Get_Date(this.DTP01_GDATE.GetValue().ToString()).ToString().Substring(0, 4));
                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        sSabun = dt.Rows[0]["HMSABUN"].ToString();
                    }

                    // 항운노조 년별 생성
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_96DDQ802", Get_Date(this.DTP01_GDATE.GetValue().ToString()).Substring(0, 4),
                                                                sSabun.ToString(),
                                                                this.FPS91_TY_S_US_96CBX787.GetValue(i, "JUMIN1").ToString(),
                                                                TYUserInfo.SecureKey,
                                                                this.FPS91_TY_S_US_96CBX787.GetValue(i, "JUMIN2").ToString(),
                                                                TYUserInfo.SecureKey,
                                                                this.FPS91_TY_S_US_96CBX787.GetValue(i, "IRUM").ToString(),
                                                                this.FPS91_TY_S_US_96CBX787.GetValue(i, "JUSO").ToString(),
                                                                this.FPS91_TY_S_US_96CBX787.GetValue(i, "PHONE").ToString(),
                                                                Set_Fill2(this.FPS91_TY_S_US_96CBX787.GetValue(i, "CLASS").ToString())
                                                                );

                    this.DbConnector.ExecuteNonQuery();
                }

                // 항운노조 월별 생성
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_96DDT803", Get_Date(this.DTP01_GDATE.GetValue().ToString()),
                                                            sSabun.ToString(),
                                                            "1",
                                                            Get_Date(this.DTP01_GDATE.GetValue().ToString()).Substring(0, 4),
                                                            this.FPS91_TY_S_US_96CBX787.GetValue(i, "IRUM").ToString(),
                                                            this.FPS91_TY_S_US_96CBX787.GetValue(i, "JUSO").ToString(),
                                                            Set_Fill2(this.FPS91_TY_S_US_96CBX787.GetValue(i, "CLASS").ToString())
                                                            );

                this.DbConnector.ExecuteNonQuery();
            
            }

            this.ShowMessage("TY_M_US_96DEB804");
        }
        #endregion

        #region Description : 처리 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;
            int j = 0;
            int k = 0;

            int iCHK = 0;

            string sName   = string.Empty;
            string sJUMIN1 = string.Empty;
            string sJUMIN2 = string.Empty;
            string sJUMIN  = string.Empty;
            string sHMNAME = string.Empty;

            this.FPS91_TY_S_US_96CBY788.Initialize();

            this.FPS91_TY_S_US_96CBY788.Sheets[0].AddRows(0, this.FPS91_TY_S_US_96CBX787.ActiveSheet.RowCount);

            DataTable dt = new DataTable();

            // 월별 자료가 존재 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_96DAZ798", Get_Date(this.DTP01_GDATE.GetValue().ToString()).ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_US_96DB0799");

                SetFocus(this.DTP01_GDATE);

                e.Successed = false;
                return;
            }

            for (i = 0; i < this.FPS91_TY_S_US_96CBX787.ActiveSheet.RowCount; i++)
            {
                iCHK = 0;

                sJUMIN1 = this.FPS91_TY_S_US_96CBX787.GetValue(i, "JUMIN1").ToString();
                sJUMIN2 = this.FPS91_TY_S_US_96CBX787.GetValue(i, "JUMIN2").ToString();
                sName = this.FPS91_TY_S_US_96CBX787.GetValue(i, "IRUM").ToString();

                // 주민번호 앞자리
                if (sJUMIN1.ToString().Length != 6)
                {
                    // 이름
                    this.FPS91_TY_S_US_96CBY788.SetValue(j, "IRUM", sName);

                    // 주민번호 앞자리
                    this.FPS91_TY_S_US_96CBY788.SetValue(j, "JUMIN1", sJUMIN1);

                    // 주민번호 뒷자리
                    this.FPS91_TY_S_US_96CBY788.SetValue(j, "JUMIN2", sJUMIN2);

                    // 비고
                    this.FPS91_TY_S_US_96CBY788.SetValue(j, "DESC", "주민등록 번호 앞자리를 확인하세요.");

                    j++;
                }
                else if (sJUMIN2.ToString().Length != 7)
                {
                    // 이름
                    this.FPS91_TY_S_US_96CBY788.SetValue(j, "IRUM", sName);

                    // 주민번호 앞자리
                    this.FPS91_TY_S_US_96CBY788.SetValue(j, "JUMIN1", sJUMIN1);

                    // 주민번호 뒷자리
                    this.FPS91_TY_S_US_96CBY788.SetValue(j, "JUMIN2", sJUMIN2);

                    // 비고
                    this.FPS91_TY_S_US_96CBY788.SetValue(j, "DESC", "주민등록 번호 뒷자리를 확인하세요.");

                    j++;
                }
                else if (sJUMIN2.Substring(0, 1) != "1" && sJUMIN2.Substring(0, 1) != "2" && sJUMIN2.Substring(0, 1) != "3" && sJUMIN2.Substring(0, 1) != "4")
                {
                    // 이름
                    this.FPS91_TY_S_US_96CBY788.SetValue(j, "IRUM", sName);

                    // 주민번호 앞자리
                    this.FPS91_TY_S_US_96CBY788.SetValue(j, "JUMIN1", sJUMIN1);

                    // 주민번호 뒷자리
                    this.FPS91_TY_S_US_96CBY788.SetValue(j, "JUMIN2", sJUMIN2);

                    // 비고
                    this.FPS91_TY_S_US_96CBY788.SetValue(j, "DESC", "주민등록 번호 뒷자리를 확인하세요.");

                    j++;
                }
                else
                {
                    sJUMIN = sJUMIN1 + sJUMIN2;

                    for (k = 0; k < 12; k++)
                    {
                        if (k < 8)
                        {
                            iCHK = iCHK + (int.Parse(sJUMIN.Substring(k, 1)) * (k + 2));
                        }
                        else
                        {
                            iCHK = iCHK + (int.Parse(sJUMIN.Substring(k, 1)) * (k - 6));
                        }
                    }

                    iCHK = (11 - (iCHK % 11)) % 10;


                    if (iCHK != int.Parse(sJUMIN2.Substring(6, 1)))
                    {
                        // 이름
                        this.FPS91_TY_S_US_96CBY788.SetValue(j, "IRUM", sName);

                        // 주민번호 앞자리
                        this.FPS91_TY_S_US_96CBY788.SetValue(j, "JUMIN1", sJUMIN1);

                        // 주민번호 뒷자리
                        this.FPS91_TY_S_US_96CBY788.SetValue(j, "JUMIN2", sJUMIN2);

                        // 비고
                        this.FPS91_TY_S_US_96CBY788.SetValue(j, "DESC", "주민등록 번호 뒷자리를 확인하세요.");

                        j++;
                    }

                    // 항운노조 년별 명부 생성-체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_96CGZ791", Get_Date(this.DTP01_GDATE.GetValue().ToString()).ToString().Substring(0, 4),
                                                                TYUserInfo.SecureKey, "Y", TYUserInfo.SecureKey, "Y",
                                                                sJUMIN.ToString());
                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        sHMNAME = dt.Rows[0]["HMNAME"].ToString();

                        if (sHMNAME.ToString() != sName.ToString())
                        {
                            // 이름
                            this.FPS91_TY_S_US_96CBY788.SetValue(j, "IRUM", sName);

                            // 주민번호 앞자리
                            this.FPS91_TY_S_US_96CBY788.SetValue(j, "JUMIN1", sJUMIN1);

                            // 주민번호 뒷자리
                            this.FPS91_TY_S_US_96CBY788.SetValue(j, "JUMIN2", sJUMIN2);

                            // 비고
                            this.FPS91_TY_S_US_96CBY788.SetValue(j, "DESC", "년별 명부에 주민번호와 이름을 확인하세요.");

                            j++;
                        }
                    }
                }
            }



            string sOLD_JUMIN = string.Empty;
            string sNEW_JUMIN = string.Empty;

            // 중복 주민번호 체크
            for (i = 0; i < this.FPS91_TY_S_US_96CBX787.ActiveSheet.RowCount; i++)
            {
                sJUMIN1 = this.FPS91_TY_S_US_96CBX787.GetValue(i, "JUMIN1").ToString();
                sJUMIN2 = this.FPS91_TY_S_US_96CBX787.GetValue(i, "JUMIN2").ToString();
                sName   = this.FPS91_TY_S_US_96CBX787.GetValue(i, "IRUM").ToString();

                sOLD_JUMIN = sJUMIN1 + sJUMIN2;
                for (k = i + 1; k < this.FPS91_TY_S_US_96CBX787.ActiveSheet.RowCount; k++)
                {
                    sNEW_JUMIN = this.FPS91_TY_S_US_96CBX787.GetValue(k, "JUMIN1").ToString() + this.FPS91_TY_S_US_96CBX787.GetValue(k, "JUMIN2").ToString();

                    if (sOLD_JUMIN == sNEW_JUMIN)
                    {
                        // 이름
                        this.FPS91_TY_S_US_96CBY788.SetValue(j, "IRUM", sName);

                        // 주민번호 앞자리
                        this.FPS91_TY_S_US_96CBY788.SetValue(j, "JUMIN1", sJUMIN1);

                        // 주민번호 뒷자리
                        this.FPS91_TY_S_US_96CBY788.SetValue(j, "JUMIN2", sJUMIN2);

                        // 비고
                        this.FPS91_TY_S_US_96CBY788.SetValue(j, "DESC", "중복된 주민번호가 있습니다.");

                        j++;
                    }
                }
            }


            if (j >= 1)
            {
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_UT_6APKH541"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 생성년월 이벤트
        private void DTP01_GDATE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.SetFocus(this.BTN61_BATCH);                
            }
        }        

        private void DTP01_GDATE_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13)
            {
                this.SetFocus(this.BTN61_BATCH);
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
