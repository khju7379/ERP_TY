using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 고정자산 자산이력 생성 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.12.14 13:41
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2CCA0097 : 고정자산 자산이력 등록
    ///  TY_P_AC_2CE1V191 : 고정자산 자산이력 생성자료 조회
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_26E2Z874 : 생성하시겠습니까?
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    ///  TY_M_GB_26E31876 : 생성 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACHF005B : TYBase
    {
        private string fsFXLNOWDPMK = "";
        private string fsFXLNOWSITE = "";
        
        public TYACHF005B()
        {
            InitializeComponent();
        }

        private void TYACHF005B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_GSTYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.SetStartingFocus(this.DTP01_GSTYYMM);
        }

        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sFXLSETGN = "";

            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();
            System.Collections.Generic.List<object[]> datau = new System.Collections.Generic.List<object[]>();
            System.Collections.Generic.List<object[]> datam = new System.Collections.Generic.List<object[]>();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2CE1V191", DTP01_GSTYYMM.GetString().ToString().Substring(0,6));
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    UP_Get_NowLocation(dt.Rows[i]["RRN6010"].ToString().Substring(0, 4), dt.Rows[i]["RRN6010"].ToString().Substring(4, 4), dt.Rows[i]["RRN6010"].ToString().Substring(8, 3));

                    if (dt.Rows[i]["RRN1080"].ToString().Substring(0, 1) == "4")
                    {
                        sFXLSETGN = "61";  //수선
                    }
                    else
                    {
                        sFXLSETGN = "31"; //자본적 지출
                    }

                    //자료 존재 확인
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_2CDBS126", dt.Rows[i]["RRN6010"].ToString().Substring(0,4),
                                                                dt.Rows[i]["RRN6010"].ToString().Substring(4,4),
                                                                dt.Rows[i]["RRN6010"].ToString().Substring(8,3),
                                                                dt.Rows[i]["RRM1100"].ToString(),
                                                                sFXLSETGN
                                                                );
                    DataTable dk = this.DbConnector.ExecuteDataTable();

                    if (dk.Rows.Count > 0)
                    {
                        datau.Add(new object[] { 
	                                         dt.Rows[i]["RRM1180"].ToString(),
                                             dt.Rows[i]["RRN1100"].ToString(),
                                             dt.Rows[i]["RRN1200"].ToString(),
	                                         dt.Rows[i]["RRN1230"].ToString(),                                             
	                                         "0",    
	                                         dt.Rows[i]["RRN1000"].ToString()+
                                             dt.Rows[i]["RRN1010"].ToString()+
                                             dt.Rows[i]["RRN1020"].ToString()+
                                             dt.Rows[i]["RRN1030"].ToString()+
                                             dt.Rows[i]["RRN1040"].ToString()+
                                             dt.Rows[i]["RRN1050"].ToString()+
                                             dt.Rows[i]["RRN1070"].ToString()+
                                             dt.Rows[i]["RRN1080"].ToString()+
                                             dt.Rows[i]["RRN1090"].ToString()+
                                             dt.Rows[i]["RRN1091"].ToString(),                                                                                         
	                                         dt.Rows[i]["RRM1460"].ToString(),
                                             dt.Rows[i]["RRN1080"].ToString(),
                                             "",
                                             "",        	
	                                         "",
	                                         "",
	                                         "",       
	                                         "0",
	                                         "",
	                                         "",
	                                         dt.Rows[i]["RRN1520"].ToString(),
	                                         "",     
	                                         "",     
	                                         fsFXLNOWDPMK,     
	                                         fsFXLNOWSITE,     
	                                         "",        
                                             "",        
                                             "",        
	                                         TYUserInfo.EmpNo,
                                             dt.Rows[i]["RRN6010"].ToString().Substring(0,4),
	                                         dt.Rows[i]["RRN6010"].ToString().Substring(4,4),
                                             dt.Rows[i]["RRN6010"].ToString().Substring(8,3),
                                             dt.Rows[i]["RRM1100"].ToString(),
	                                         sFXLSETGN
                                           });
                    }
                    else
                    {
                        //자산이력 등록
                        datas.Add(new object[] { dt.Rows[i]["RRN6010"].ToString().Substring(0,4),
	                                         dt.Rows[i]["RRN6010"].ToString().Substring(4,4),
                                             dt.Rows[i]["RRN6010"].ToString().Substring(8,3),
                                             dt.Rows[i]["RRM1100"].ToString(),
	                                         sFXLSETGN,
	                                         dt.Rows[i]["RRM1180"].ToString(),
                                             dt.Rows[i]["RRN1100"].ToString(),
                                             dt.Rows[i]["RRN1200"].ToString(),
	                                         dt.Rows[i]["RRN1230"].ToString(),                                             
	                                         "0",    
	                                         dt.Rows[i]["RRN1000"].ToString()+
                                             dt.Rows[i]["RRN1010"].ToString()+
                                             dt.Rows[i]["RRN1020"].ToString()+
                                             dt.Rows[i]["RRN1030"].ToString()+
                                             dt.Rows[i]["RRN1040"].ToString()+
                                             dt.Rows[i]["RRN1050"].ToString()+
                                             dt.Rows[i]["RRN1070"].ToString()+
                                             dt.Rows[i]["RRN1080"].ToString()+
                                             dt.Rows[i]["RRN1090"].ToString()+
                                             dt.Rows[i]["RRN1091"].ToString(),                                                                                         
	                                         dt.Rows[i]["RRM1460"].ToString(),
                                             dt.Rows[i]["RRN1080"].ToString(),
                                             "",
                                             "",        	
	                                         "",
	                                         "",
	                                         "",       
	                                         "0",
	                                         "",
	                                         "",
	                                         dt.Rows[i]["RRN1520"].ToString(),
	                                         "",     
	                                         "",     
	                                         fsFXLNOWDPMK,     
	                                         fsFXLNOWSITE,     
	                                         "",   
                                             "",
                                             "",
	                                         TYUserInfo.EmpNo  
                                           });
                    }

                    //자산디테일 update
                    datam.Add(new object[] { 
	                                         "61",
                                             dt.Rows[i]["RRM1180"].ToString(),
                                             dt.Rows[i]["RRM1100"].ToString(),
                                             fsFXLNOWDPMK,
                                             fsFXLNOWSITE,
                                             "",
	                                         dt.Rows[i]["RRN6010"].ToString().Substring(0,4),
	                                         dt.Rows[i]["RRN6010"].ToString().Substring(4,4),
                                             dt.Rows[i]["RRN6010"].ToString().Substring(8,3)
                                           });
                
                }

                if (datas.Count > 0 || datau.Count > 0 )
                {
                    this.DbConnector.CommandClear();
                    foreach (object[] data in datas)
                    {
                        this.DbConnector.Attach("TY_P_AC_2CCA0097", data);
                    }
                    foreach (object[] data2 in datau)
                    {
                        this.DbConnector.Attach("TY_P_AC_2CCAM100", data2);
                    }
                    foreach (object[] data1 in datam)
                    {
                        this.DbConnector.Attach("TY_P_AC_2CD3Y138", data1);
                    }
                    this.DbConnector.ExecuteNonQueryList();
                }

                this.ShowMessage("TY_M_GB_26E30875");
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }

        }

        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();  
        }

        #region Description : 처리 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            if (!this.ShowMessage("TY_M_GB_26E2Z874"))
            {
                e.Successed = false;
                return;
            }            
        }
        #endregion

        #region  Description : 자산의 현재위치 찾기
        private void UP_Get_NowLocation(string sYEAR, string sSEQ, string sNUM)
        {
            fsFXLNOWDPMK = "";
            fsFXLNOWSITE = "";

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2CBAN093", sYEAR.ToString(), sSEQ.ToString(), sNUM.ToString(), "63");
            DataTable dt = this.DbConnector.ExecuteDataTable();

            //자산 이력 등록이 없으면 현위치, 현부서는 자사 마스타에서 가져온다
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    fsFXLNOWDPMK = dt.Rows[i]["FXLMOVDPMK"].ToString();
                    fsFXLNOWSITE = dt.Rows[i]["FXLMOVSITE"].ToString();
                }
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2CC9F096", sYEAR.ToString(), sSEQ.ToString(), sNUM.ToString());
                DataTable dm = this.DbConnector.ExecuteDataTable();
                if (dm.Rows.Count > 0)
                {
                    fsFXLNOWDPMK = dm.Rows[0]["FXSAUP"].ToString();
                    fsFXLNOWSITE = dm.Rows[0]["FXSFITSITE"].ToString();
                }
            }
        }
        #endregion
    }
}
