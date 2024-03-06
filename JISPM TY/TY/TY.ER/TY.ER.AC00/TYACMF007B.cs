using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.IO;
using System.Text;

namespace TY.ER.AC00
{
    /// <summary>
    /// 펌뱅킹 미지급금(현금,어음) 생성 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.11.07 18:29
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2B76O157 : 펌뱅킹 미지급금(현금) Master 등록
    ///  TY_P_AC_2B76Q158 : 펌뱅킹 미지급금(현금) 내역 등록
    ///  TY_P_AC_2B76Q159 : 펌뱅킹 미지급금(현금) 내역 삭제
    ///  TY_P_AC_2B76R160 : 펌뱅킹 미지급금(현금) Master 삭제
    ///  TY_P_AC_2B76V161 : 펌뱅킹 미지급금 조회
    ///  TY_P_AC_2B76Y162 : 펌뱅킹 미지급금(어음) 내역 등록
    ///  TY_P_AC_2B76Y163 : 펌뱅킹 미지급금(어음) 내역 삭제
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2B77B165 : 파일을 다운 작업을 하시겠습니까?
    ///  TY_M_GB_25UAA711 : 파일을 다운로드하였습니다.
    ///  TY_M_GB_26E2Z874 : 생성하시겠습니까?
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    ///  TY_M_GB_26E31876 : 생성 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  DWN : 다운
    ///  G2CDBK : 이체은행
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    ///  GOKCR : 생성구분
    /// </summary>
    public partial class TYACMF007B : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYACMF007B()
        {
            InitializeComponent();

            this.SetPopupStyle(); 
        }

        private void TYACMF007B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);
            this.BTN61_DWN.ProcessCheck += new TButton.CheckHandler(BTN61_DWN_ProcessCheck);

            this.DTP01_GSTDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_GEDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.SetStartingFocus(this.DTP01_GSTDATE);
        }
        #endregion

        #region  Description : 생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

            if (this.CBO01_GOKCR.GetValue().ToString() == "1")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2B76R160", DTP01_GEDDATE.GetString());
                this.DbConnector.ExecuteTranQuery();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2B76Q159", DTP01_GEDDATE.GetString());
                this.DbConnector.ExecuteTranQuery();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2B76V161", DTP01_GEDDATE.GetString(), CBO01_GOKCR.GetValue());

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();
                    //내역
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        datas.Add(new object[] { dt.Rows[i]["M1DTED"].ToString(), 
                                                 dt.Rows[i]["M1VNCD"].ToString(), 
                                                 dt.Rows[i]["M1NOSQ"].ToString(), 
                                                 dt.Rows[i]["M1GUBN"].ToString(), 
                                                 dt.Rows[i]["VNBIGO"].ToString(), 
                                                 dt.Rows[i]["M1CDBK"].ToString(), 
                                                 dt.Rows[i]["M1NOAC"].ToString().Replace("-",""), 
                                                 dt.Rows[i]["M1AMT"].ToString(),
                                                 TYUserInfo.EmpNo  
                                                });
                    }
                    if (datas.Count > 0)
                    {
                        foreach (object[] data in datas)
                        {
                            this.DbConnector.Attach("TY_P_AC_2B76Q158", data);
                        }
                    }
                    datas.Clear();

                    //마스타
                    datas.Add(new object[] { dt.Rows[0]["M1DTED"].ToString(), 
                                             dt.Rows[0]["M1DTED"].ToString(), 
                                             dt.Rows[0]["M1DTED"].ToString(), 
                                             this.CBO01_G2CDBK.GetValue().ToString(),  
                                             dt.Rows.Count.ToString(),  
                                             dt.Compute("SUM(M1AMT)", "").ToString(),
                                             TYUserInfo.EmpNo
                                             });
                    if (datas.Count > 0)
                    {
                        foreach (object[] data in datas)
                        {
                            this.DbConnector.Attach("TY_P_AC_2B76O157", data);
                        }
                    }
                    this.DbConnector.ExecuteTranQueryList();
                }

            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2B76Y163", DTP01_GEDDATE.GetString());
                this.DbConnector.ExecuteTranQuery();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2B76V161", DTP01_GEDDATE.GetString(), CBO01_GOKCR.GetValue());

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();
                    //내역
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        datas.Add(new object[] { dt.Rows[i]["M1DTED"].ToString(), 
                                                 dt.Rows[i]["M1VNCD"].ToString(), 
                                                 dt.Rows[i]["M1NOSQ"].ToString(), 
                                                 dt.Rows[i]["M1GUBN"].ToString(), 
                                                 dt.Rows[i]["M1WNJP"].ToString(), 
                                                 Convert.ToString(Convert.ToDouble(dt.Rows[i]["M1AMT"].ToString()) -
                                                                  Convert.ToDouble(dt.Rows[i]["B2AMDR"].ToString())), 
                                                 dt.Rows[i]["B2AMDR"].ToString(),
                                                 dt.Rows[i]["M1AMT"].ToString(),
                                                 dt.Rows[i]["M1DATE"].ToString(), 
                                                 dt.Rows[i]["B2VLMI3"].ToString()                                                 
                                                });
                    }
                    if (datas.Count > 0)
                    {
                        foreach (object[] data in datas)
                        {
                            this.DbConnector.Attach("TY_P_AC_2B76Y162", data);
                        }
                    }
                    this.DbConnector.ExecuteTranQueryList();
                }
            }

            this.ShowMessage("TY_M_GB_26E30875");
        }
        #endregion

        #region  Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region  Description : 파일 다운 이벤트
        private void BTN61_DWN_Click(object sender, EventArgs e)
        {
            string sFilePath = "C:\\EDI\\OUT\\";
            string sReCordString = "";
            string sStrTemp = "";

            if (System.IO.Directory.Exists(sFilePath) == false)
            {
                System.IO.Directory.CreateDirectory(sFilePath);
            }

            if (CBO01_GOKCR.GetValue().ToString() == "1")
            {
                if (File.Exists("C:\\EDI\\OUT\\ACEDIMF.txt"))
                {
                     File.Delete("C:\\EDI\\OUT\\ACEDIMF.txt");
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2B799185", DTP01_GEDDATE.GetString());
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    
                    //StreamWriter StrWrReCode = File.AppendText("C:\\EDI\\OUT\\ACEDIMF.txt");                    
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{                        
                    //    sStrTemp = "";
                    //    sStrTemp = dt.Rows[i]["G2CDBK"].ToString().Trim();
                    //    sStrTemp += new String(Convert.ToChar(" "), (3 - Encoding.Default.GetByteCount(dt.Rows[i]["G2CDBK"].ToString().Trim())));

                    //    sReCordString = sStrTemp;

                    //    //구분자 ,
                    //    sReCordString += ",";

                    //    sStrTemp = "";
                    //    sStrTemp = dt.Rows[i]["G2NOAC"].ToString().Trim();
                    //    sStrTemp += new String(Convert.ToChar(" "), (18 - Encoding.Default.GetByteCount(dt.Rows[i]["G2NOAC"].ToString().Trim())));

                    //    sReCordString += sStrTemp;

                    //    //sReCordString += "KRW ";

                    //    sReCordString += ",";

                    //    sReCordString += String.Format("{0:D13}", Convert.ToInt64(Get_Numeric(dt.Rows[i]["G2AMT"].ToString())));

                    //    sReCordString += ",";

                    //    sStrTemp = "";                        
                    //    sStrTemp += new String(Convert.ToChar(" "), (8 - Encoding.Default.GetByteCount(dt.Rows[i]["G2VNCD"].ToString().Trim())));
                    //    sStrTemp += dt.Rows[i]["G2VNCD"].ToString().Trim();

                    //    sReCordString += sStrTemp;

                    //    sReCordString += ",   ";

                    //    sReCordString += "태영인더스트리";

                    //    StrWrReCode.WriteLine(sReCordString);                        
                    //}
                    //StrWrReCode.Close();

                    //string strResultFile = "C:\\EDI\\OUT\\ACEDIMF.txt";

                    //File.WriteAllText(strResultFile, StrWrReCode.ToString(), UTF8Encoding.UTF8);

                    StringBuilder StrWrReCode = new StringBuilder();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sStrTemp = "";
                        sStrTemp = dt.Rows[i]["G2CDBK"].ToString().Trim();
                        sStrTemp += new String(Convert.ToChar(" "), (3 - Encoding.Default.GetByteCount(dt.Rows[i]["G2CDBK"].ToString().Trim())));

                        sReCordString = sStrTemp;

                        //구분자 ,
                        sReCordString += ",";

                        sStrTemp = "";
                        sStrTemp = dt.Rows[i]["G2NOAC"].ToString().Trim();
                        sStrTemp += new String(Convert.ToChar(" "), (18 - Encoding.Default.GetByteCount(dt.Rows[i]["G2NOAC"].ToString().Trim())));

                        sReCordString += sStrTemp;

                        //sReCordString += "KRW ";

                        sReCordString += ",";

                        sReCordString += String.Format("{0:D13}", Convert.ToInt64(Get_Numeric(dt.Rows[i]["G2AMT"].ToString())));

                        sReCordString += ",";

                        sStrTemp = "";
                        sStrTemp += new String(Convert.ToChar(" "), (8 - Encoding.Default.GetByteCount(dt.Rows[i]["G2VNCD"].ToString().Trim())));
                        sStrTemp += dt.Rows[i]["G2VNCD"].ToString().Trim();

                        sReCordString += sStrTemp;

                        sReCordString += ",   ";

                        sReCordString += "태영인더스트리";

                        StrWrReCode.AppendLine(sReCordString);
                    }

                    string strResultFile = "C:\\EDI\\OUT\\ACEDIMF.txt";

                    File.WriteAllText(strResultFile, StrWrReCode.ToString(), UTF8Encoding.UTF8);                    

                }
            }
            else
            {
                if (File.Exists("C:\\EDI\\OUT\\외상매출금.txt"))
                {
                    File.Delete("C:\\EDI\\OUT\\외상매출금.txt");
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2B791186", DTP01_GEDDATE.GetString());
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    StreamWriter StrWrReCode = File.AppendText("C:\\EDI\\OUT\\외상매출금.txt");

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sReCordString = "261";

                        sReCordString += " , ";

                        sReCordString += "K0019";

                        sReCordString += " , ";

                        sReCordString += dt.Rows[i]["A1GLDATE"].ToString().Trim();

                        sReCordString += " , ";

                        sReCordString += String.Format("{0:D10}", Convert.ToInt64(Get_Numeric(dt.Rows[i]["A1AMTOT"].ToString())));

                        sReCordString += " , ";

                        sReCordString += dt.Rows[i]["A1WNJP"].ToString().Trim().Substring(6,8);

                        sReCordString += " , ";

                        if (dt.Rows[i]["VNSAUPNO"].ToString().Trim() == "")
                        {
                            sReCordString += "          ";
                        }
                        else
                        {
                            sReCordString += dt.Rows[i]["VNSAUPNO"].ToString().Trim();
                        }

                        sReCordString += " , ";

                        sReCordString += "6108110449";

                        sReCordString += " , ";

                        sReCordString += String.Format("{0:D10}", Convert.ToInt64(Get_Numeric(dt.Rows[i]["A1AMAMT"].ToString())));

                        sReCordString += " , ";

                        sReCordString += String.Format("{0:D10}", Convert.ToInt64(Get_Numeric(dt.Rows[i]["A1AMTOT"].ToString())));

                        sReCordString += " , ";

                        sReCordString += dt.Rows[i]["A1VNCD"].ToString().Trim();

                        StrWrReCode.WriteLine(sReCordString);
                    }
                    StrWrReCode.Close();
                }
            }
            this.ShowMessage("TY_M_GB_25UAA711");
        }
        #endregion

        #region Description : 생성 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2B76V161", DTP01_GEDDATE.GetString(), CBO01_GOKCR.GetValue());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_26E2Z874"))
            {
                e.Successed = false;
                return;
            }         
        }
        #endregion

        #region Description : 파일다운 ProcessCheck 이벤트
        private void BTN61_DWN_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            Int16 iCnt = 0;

            if (CBO01_GOKCR.GetValue().ToString() == "1")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2B792183", DTP01_GEDDATE.GetString());
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2B793184", DTP01_GEDDATE.GetString());
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
            }

            if (iCnt <= 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_AC_2B77B165"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion
    }
}
