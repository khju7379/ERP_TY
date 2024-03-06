using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.Data.OleDb;

namespace TY.ER.UT00
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
    public partial class TYUTIN009I : TYBase
    {
        private string[] fsGAUGE = new string[100];

        private string fsGATANKNO = string.Empty;
        private string fsGAMAJOR  = string.Empty;
        private string fsGAUGE001 = string.Empty;
        private string fsGAUGE002 = string.Empty;
        private string fsGAUGE003 = string.Empty;
        private string fsGAUGE004 = string.Empty;
        private string fsGAUGE005 = string.Empty;
        private string fsGAUGE006 = string.Empty;
        private string fsGAUGE007 = string.Empty;
        private string fsGAUGE008 = string.Empty;
        private string fsGAUGE009 = string.Empty;
        private string fsGAUGE010 = string.Empty;
        private string fsGAUGE011 = string.Empty;
        private string fsGAUGE012 = string.Empty;
        private string fsGAUGE013 = string.Empty;
        private string fsGAUGE014 = string.Empty;
        private string fsGAUGE015 = string.Empty;
        private string fsGAUGE016 = string.Empty;
        private string fsGAUGE017 = string.Empty;
        private string fsGAUGE018 = string.Empty;
        private string fsGAUGE019 = string.Empty;
        private string fsGAUGE020 = string.Empty;
        private string fsGAUGE021 = string.Empty;
        private string fsGAUGE022 = string.Empty;
        private string fsGAUGE023 = string.Empty;
        private string fsGAUGE024 = string.Empty;
        private string fsGAUGE025 = string.Empty;
        private string fsGAUGE026 = string.Empty;
        private string fsGAUGE027 = string.Empty;
        private string fsGAUGE028 = string.Empty;
        private string fsGAUGE029 = string.Empty;
        private string fsGAUGE030 = string.Empty;
        private string fsGAUGE031 = string.Empty;
        private string fsGAUGE032 = string.Empty;
        private string fsGAUGE033 = string.Empty;
        private string fsGAUGE034 = string.Empty;
        private string fsGAUGE035 = string.Empty;
        private string fsGAUGE036 = string.Empty;
        private string fsGAUGE037 = string.Empty;
        private string fsGAUGE038 = string.Empty;
        private string fsGAUGE039 = string.Empty;
        private string fsGAUGE040 = string.Empty;
        private string fsGAUGE041 = string.Empty;
        private string fsGAUGE042 = string.Empty;
        private string fsGAUGE043 = string.Empty;
        private string fsGAUGE044 = string.Empty;
        private string fsGAUGE045 = string.Empty;
        private string fsGAUGE046 = string.Empty;
        private string fsGAUGE047 = string.Empty;
        private string fsGAUGE048 = string.Empty;
        private string fsGAUGE049 = string.Empty;
        private string fsGAUGE050 = string.Empty;
        private string fsGAUGE051 = string.Empty;
        private string fsGAUGE052 = string.Empty;
        private string fsGAUGE053 = string.Empty;
        private string fsGAUGE054 = string.Empty;
        private string fsGAUGE055 = string.Empty;
        private string fsGAUGE056 = string.Empty;
        private string fsGAUGE057 = string.Empty;
        private string fsGAUGE058 = string.Empty;
        private string fsGAUGE059 = string.Empty;
        private string fsGAUGE060 = string.Empty;
        private string fsGAUGE061 = string.Empty;
        private string fsGAUGE062 = string.Empty;
        private string fsGAUGE063 = string.Empty;
        private string fsGAUGE064 = string.Empty;
        private string fsGAUGE065 = string.Empty;
        private string fsGAUGE066 = string.Empty;
        private string fsGAUGE067 = string.Empty;
        private string fsGAUGE068 = string.Empty;
        private string fsGAUGE069 = string.Empty;
        private string fsGAUGE070 = string.Empty;
        private string fsGAUGE071 = string.Empty;
        private string fsGAUGE072 = string.Empty;
        private string fsGAUGE073 = string.Empty;
        private string fsGAUGE074 = string.Empty;
        private string fsGAUGE075 = string.Empty;
        private string fsGAUGE076 = string.Empty;
        private string fsGAUGE077 = string.Empty;
        private string fsGAUGE078 = string.Empty;
        private string fsGAUGE079 = string.Empty;
        private string fsGAUGE080 = string.Empty;
        private string fsGAUGE081 = string.Empty;
        private string fsGAUGE082 = string.Empty;
        private string fsGAUGE083 = string.Empty;
        private string fsGAUGE084 = string.Empty;
        private string fsGAUGE085 = string.Empty;
        private string fsGAUGE086 = string.Empty;
        private string fsGAUGE087 = string.Empty;
        private string fsGAUGE088 = string.Empty;
        private string fsGAUGE089 = string.Empty;
        private string fsGAUGE090 = string.Empty;
        private string fsGAUGE091 = string.Empty;
        private string fsGAUGE092 = string.Empty;
        private string fsGAUGE093 = string.Empty;
        private string fsGAUGE094 = string.Empty;
        private string fsGAUGE095 = string.Empty;
        private string fsGAUGE096 = string.Empty;
        private string fsGAUGE097 = string.Empty;
        private string fsGAUGE098 = string.Empty;
        private string fsGAUGE099 = string.Empty;
        private string fsGAUGE100 = string.Empty;

        // 엑셀 서식
        // TANK, VOLUME, LEVEL <= 1열에 명이 나옴
        // 2열부터 TANK, VOLUME, LEVEL 자료가 나옴

        #region Description : 폼로드 
        public TYUTIN009I()
        {
            InitializeComponent();
        }

        private void TYUTIN009I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            //this.BTN61_SAV.IsAsynchronous = true;
            //this.BTN61_BATCH.IsAsynchronous = true;

            this.FPS91_TY_S_UT_6APJP527.Initialize();
            this.FPS91_TY_S_UT_6APJN523.Initialize();
        }
        #endregion

        #region Description : 엑셀 UPLOAD
        private void BTN61_EXCEL_Click(object sender, EventArgs e)
        {
            if (this.txtFile.Text.Trim() != "")
            {
                this.FPS91_TY_S_UT_6APJP527.Initialize();
                this.FPS91_TY_S_UT_6APJN523.Initialize();
                
                // 탱크번호, 볼륨(VOLUME), 레벨(LEVEL)

                string strProvider = string.Empty;
                string strQuery    = string.Empty;

                #region Description : CM 시트 열기

                strProvider = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + this.txtFile.Text.Trim() + "; Extended Properties=Excel 12.0";

                strQuery = "SELECT * FROM [" + this.TXT01_SHEET1NM.GetValue().ToString().Trim() + "$] "; //  , Sheet1$
                //strQuery = "SELECT * FROM [CM$] "; //  , Sheet1$

                OleDbConnection ExcelCon = new OleDbConnection(strProvider);
                ExcelCon.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(strQuery, strProvider);

                DataSet ds = new DataSet();
                adapter.Fill(ds, "EXCEL");

                this.FPS91_TY_S_UT_6APJP527_Sheet1.RowCount = ds.Tables[0].Rows.Count;

                fsGATANKNO = Set_TankNo(this.TXT01_GATANKNO.GetValue().ToString());

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (fsGATANKNO == "" && ds.Tables[0].Rows[i][0].ToString() != "")
                    {
                        fsGATANKNO = Set_TankNo(ds.Tables[0].Rows[i][0].ToString());
                    }

                    this.FPS91_TY_S_UT_6APJP527_Sheet1.Cells[i, 0].Value = fsGATANKNO.ToString();
                    this.FPS91_TY_S_UT_6APJP527_Sheet1.Cells[i, 1].Value = ds.Tables[0].Rows[i][1].ToString();
                    this.FPS91_TY_S_UT_6APJP527_Sheet1.Cells[i, 2].Value = Convert.ToString((double.Parse(ds.Tables[0].Rows[i][2].ToString())));
                }

                #endregion





                #region Description : MM 시트 열기

                strQuery = "";
                strQuery = "SELECT * FROM [" + this.TXT01_SHEET2NM.GetValue().ToString() + "$] "; //  , Sheet1$
                //strQuery = "SELECT * FROM [ReadMe mm$] "; //  , Sheet1$

                OleDbConnection ExcelCon1 = new OleDbConnection(strProvider);
                ExcelCon1.Open();

                OleDbDataAdapter adapter1 = new OleDbDataAdapter(strQuery, strProvider);

                DataSet ds1 = new DataSet();
                adapter1.Fill(ds1, "EXCEL");

                this.FPS91_TY_S_UT_6APJN523_Sheet1.RowCount = ds1.Tables[0].Rows.Count;

                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    if (fsGATANKNO == "" && ds1.Tables[0].Rows[i][0].ToString() != "")
                    {
                        fsGATANKNO = Set_TankNo(ds1.Tables[0].Rows[i][0].ToString());
                    }

                    this.FPS91_TY_S_UT_6APJN523_Sheet1.Cells[i, 0].Value = fsGATANKNO.ToString();
                    this.FPS91_TY_S_UT_6APJN523_Sheet1.Cells[i, 1].Value = ds1.Tables[0].Rows[i][1].ToString();
                    this.FPS91_TY_S_UT_6APJN523_Sheet1.Cells[i, 2].Value = Convert.ToString((double.Parse(ds1.Tables[0].Rows[i][2].ToString())));
                }

                #endregion

                this.ShowMessage("TY_M_UT_6APJV532");
            }
            else
            {
                this.ShowMessage("TY_M_AC_31B1C623");
            }

        }
        #endregion

        #region Description : CM 탱크테이블 업데이트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            int i = 0;
            int k = 0;
            int iCount = 0;

            // 탱크 용량 테이블 삭제
            UP_UTIGAUGF_Del();

            #region Description : 탱크 용량 테이블 업데이트(CM)

            for (i = 0; i < this.FPS91_TY_S_UT_6APJP527.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_UT_6APJP527.GetValue(i, "LEVEL").ToString().Length == 4 && this.FPS91_TY_S_UT_6APJP527.GetValue(i, "LEVEL").ToString().Substring(2, 2) == "00")
                {
                    fsGAMAJOR = Set_Fill2(Convert.ToString(iCount));

                    UP_UTIGAUGF_Ins();

                    k = 0;
                    iCount++;
                }
                else if (this.FPS91_TY_S_UT_6APJP527.GetValue(i, "LEVEL").ToString().Length == 3 && this.FPS91_TY_S_UT_6APJP527.GetValue(i, "LEVEL").ToString().Substring(1, 2) == "00")
                {
                    fsGAMAJOR = Set_Fill2(Convert.ToString(iCount));

                    UP_UTIGAUGF_Ins();

                    k = 0;
                    iCount++;
                }

                fsGAUGE[k] = this.FPS91_TY_S_UT_6APJP527.GetValue(i, "VOLUME").ToString();

                k++;
            }

            fsGAMAJOR = Set_Fill2(Convert.ToString(iCount));

            UP_UTIGAUGF_Ins();

            #endregion

            this.ShowMessage("TY_M_UT_7CC9E225");
        }

        private void BTN61_SAV_InvokerStart(object sender, TButton.ClickEventCheckArgs e)
        {

        }

        private void BTN61_SAV_InvokerEnd(object sender, TButton.ClickEventCheckArgs e)
        {

        }
        #endregion
        
        #region Description : 탱크 용량 테이블 업데이트
        private void UP_UTIGAUGF_Ins()
        {
            fsGAUGE001 = fsGAUGE[0].ToString();
            fsGAUGE002 = fsGAUGE[1].ToString();
            fsGAUGE003 = fsGAUGE[2].ToString();
            fsGAUGE004 = fsGAUGE[3].ToString();
            fsGAUGE005 = fsGAUGE[4].ToString();
            fsGAUGE006 = fsGAUGE[5].ToString();
            fsGAUGE007 = fsGAUGE[6].ToString();
            fsGAUGE008 = fsGAUGE[7].ToString();
            fsGAUGE009 = fsGAUGE[8].ToString();
            fsGAUGE010 = fsGAUGE[9].ToString();
            fsGAUGE011 = fsGAUGE[10].ToString();
            fsGAUGE012 = fsGAUGE[11].ToString();
            fsGAUGE013 = fsGAUGE[12].ToString();
            fsGAUGE014 = fsGAUGE[13].ToString();
            fsGAUGE015 = fsGAUGE[14].ToString();
            fsGAUGE016 = fsGAUGE[15].ToString();
            fsGAUGE017 = fsGAUGE[16].ToString();
            fsGAUGE018 = fsGAUGE[17].ToString();
            fsGAUGE019 = fsGAUGE[18].ToString();
            fsGAUGE020 = fsGAUGE[19].ToString();
            fsGAUGE021 = fsGAUGE[20].ToString();
            fsGAUGE022 = fsGAUGE[21].ToString();
            fsGAUGE023 = fsGAUGE[22].ToString();
            fsGAUGE024 = fsGAUGE[23].ToString();
            fsGAUGE025 = fsGAUGE[24].ToString();
            fsGAUGE026 = fsGAUGE[25].ToString();
            fsGAUGE027 = fsGAUGE[26].ToString();
            fsGAUGE028 = fsGAUGE[27].ToString();
            fsGAUGE029 = fsGAUGE[28].ToString();
            fsGAUGE030 = fsGAUGE[29].ToString();
            fsGAUGE031 = fsGAUGE[30].ToString();
            fsGAUGE032 = fsGAUGE[31].ToString();
            fsGAUGE033 = fsGAUGE[32].ToString();
            fsGAUGE034 = fsGAUGE[33].ToString();
            fsGAUGE035 = fsGAUGE[34].ToString();
            fsGAUGE036 = fsGAUGE[35].ToString();
            fsGAUGE037 = fsGAUGE[36].ToString();
            fsGAUGE038 = fsGAUGE[37].ToString();
            fsGAUGE039 = fsGAUGE[38].ToString();
            fsGAUGE040 = fsGAUGE[39].ToString();
            fsGAUGE041 = fsGAUGE[40].ToString();
            fsGAUGE042 = fsGAUGE[41].ToString();
            fsGAUGE043 = fsGAUGE[42].ToString();
            fsGAUGE044 = fsGAUGE[43].ToString();
            fsGAUGE045 = fsGAUGE[44].ToString();
            fsGAUGE046 = fsGAUGE[45].ToString();
            fsGAUGE047 = fsGAUGE[46].ToString();
            fsGAUGE048 = fsGAUGE[47].ToString();
            fsGAUGE049 = fsGAUGE[48].ToString();
            fsGAUGE050 = fsGAUGE[49].ToString();
            fsGAUGE051 = fsGAUGE[50].ToString();
            fsGAUGE052 = fsGAUGE[51].ToString();
            fsGAUGE053 = fsGAUGE[52].ToString();
            fsGAUGE054 = fsGAUGE[53].ToString();
            fsGAUGE055 = fsGAUGE[54].ToString();
            fsGAUGE056 = fsGAUGE[55].ToString();
            fsGAUGE057 = fsGAUGE[56].ToString();
            fsGAUGE058 = fsGAUGE[57].ToString();
            fsGAUGE059 = fsGAUGE[58].ToString();
            fsGAUGE060 = fsGAUGE[59].ToString();
            fsGAUGE061 = fsGAUGE[60].ToString();
            fsGAUGE062 = fsGAUGE[61].ToString();
            fsGAUGE063 = fsGAUGE[62].ToString();
            fsGAUGE064 = fsGAUGE[63].ToString();
            fsGAUGE065 = fsGAUGE[64].ToString();
            fsGAUGE066 = fsGAUGE[65].ToString();
            fsGAUGE067 = fsGAUGE[66].ToString();
            fsGAUGE068 = fsGAUGE[67].ToString();
            fsGAUGE069 = fsGAUGE[68].ToString();
            fsGAUGE070 = fsGAUGE[69].ToString();
            fsGAUGE071 = fsGAUGE[70].ToString();
            fsGAUGE072 = fsGAUGE[71].ToString();
            fsGAUGE073 = fsGAUGE[72].ToString();
            fsGAUGE074 = fsGAUGE[73].ToString();
            fsGAUGE075 = fsGAUGE[74].ToString();
            fsGAUGE076 = fsGAUGE[75].ToString();
            fsGAUGE077 = fsGAUGE[76].ToString();
            fsGAUGE078 = fsGAUGE[77].ToString();
            fsGAUGE079 = fsGAUGE[78].ToString();
            fsGAUGE080 = fsGAUGE[79].ToString();
            fsGAUGE081 = fsGAUGE[80].ToString();
            fsGAUGE082 = fsGAUGE[81].ToString();
            fsGAUGE083 = fsGAUGE[82].ToString();
            fsGAUGE084 = fsGAUGE[83].ToString();
            fsGAUGE085 = fsGAUGE[84].ToString();
            fsGAUGE086 = fsGAUGE[85].ToString();
            fsGAUGE087 = fsGAUGE[86].ToString();
            fsGAUGE088 = fsGAUGE[87].ToString();
            fsGAUGE089 = fsGAUGE[88].ToString();
            fsGAUGE090 = fsGAUGE[89].ToString();
            fsGAUGE091 = fsGAUGE[90].ToString();
            fsGAUGE092 = fsGAUGE[91].ToString();
            fsGAUGE093 = fsGAUGE[92].ToString();
            fsGAUGE094 = fsGAUGE[93].ToString();
            fsGAUGE095 = fsGAUGE[94].ToString();
            fsGAUGE096 = fsGAUGE[95].ToString();
            fsGAUGE097 = fsGAUGE[96].ToString();
            fsGAUGE098 = fsGAUGE[97].ToString();
            fsGAUGE099 = fsGAUGE[98].ToString();
            fsGAUGE100 = fsGAUGE[99].ToString();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_6APKW543", fsGATANKNO.ToString(),
                                                        fsGAMAJOR.ToString(),
                                                        fsGAUGE001.ToString(),
                                                        fsGAUGE002.ToString(),
                                                        fsGAUGE003.ToString(),
                                                        fsGAUGE004.ToString(),
                                                        fsGAUGE005.ToString(),
                                                        fsGAUGE006.ToString(),
                                                        fsGAUGE007.ToString(),
                                                        fsGAUGE008.ToString(),
                                                        fsGAUGE009.ToString(),
                                                        fsGAUGE010.ToString(),
                                                        fsGAUGE011.ToString(),
                                                        fsGAUGE012.ToString(),
                                                        fsGAUGE013.ToString(),
                                                        fsGAUGE014.ToString(),
                                                        fsGAUGE015.ToString(),
                                                        fsGAUGE016.ToString(),
                                                        fsGAUGE017.ToString(),
                                                        fsGAUGE018.ToString(),
                                                        fsGAUGE019.ToString(),
                                                        fsGAUGE020.ToString(),
                                                        fsGAUGE021.ToString(),
                                                        fsGAUGE022.ToString(),
                                                        fsGAUGE023.ToString(),
                                                        fsGAUGE024.ToString(),
                                                        fsGAUGE025.ToString(),
                                                        fsGAUGE026.ToString(),
                                                        fsGAUGE027.ToString(),
                                                        fsGAUGE028.ToString(),
                                                        fsGAUGE029.ToString(),
                                                        fsGAUGE030.ToString(),
                                                        fsGAUGE031.ToString(),
                                                        fsGAUGE032.ToString(),
                                                        fsGAUGE033.ToString(),
                                                        fsGAUGE034.ToString(),
                                                        fsGAUGE035.ToString(),
                                                        fsGAUGE036.ToString(),
                                                        fsGAUGE037.ToString(),
                                                        fsGAUGE038.ToString(),
                                                        fsGAUGE039.ToString(),
                                                        fsGAUGE040.ToString(),
                                                        fsGAUGE041.ToString(),
                                                        fsGAUGE042.ToString(),
                                                        fsGAUGE043.ToString(),
                                                        fsGAUGE044.ToString(),
                                                        fsGAUGE045.ToString(),
                                                        fsGAUGE046.ToString(),
                                                        fsGAUGE047.ToString(),
                                                        fsGAUGE048.ToString(),
                                                        fsGAUGE049.ToString(),
                                                        fsGAUGE050.ToString(),
                                                        fsGAUGE051.ToString(),
                                                        fsGAUGE052.ToString(),
                                                        fsGAUGE053.ToString(),
                                                        fsGAUGE054.ToString(),
                                                        fsGAUGE055.ToString(),
                                                        fsGAUGE056.ToString(),
                                                        fsGAUGE057.ToString(),
                                                        fsGAUGE058.ToString(),
                                                        fsGAUGE059.ToString(),
                                                        fsGAUGE060.ToString(),
                                                        fsGAUGE061.ToString(),
                                                        fsGAUGE062.ToString(),
                                                        fsGAUGE063.ToString(),
                                                        fsGAUGE064.ToString(),
                                                        fsGAUGE065.ToString(),
                                                        fsGAUGE066.ToString(),
                                                        fsGAUGE067.ToString(),
                                                        fsGAUGE068.ToString(),
                                                        fsGAUGE069.ToString(),
                                                        fsGAUGE070.ToString(),
                                                        fsGAUGE071.ToString(),
                                                        fsGAUGE072.ToString(),
                                                        fsGAUGE073.ToString(),
                                                        fsGAUGE074.ToString(),
                                                        fsGAUGE075.ToString(),
                                                        fsGAUGE076.ToString(),
                                                        fsGAUGE077.ToString(),
                                                        fsGAUGE078.ToString(),
                                                        fsGAUGE079.ToString(),
                                                        fsGAUGE080.ToString(),
                                                        fsGAUGE081.ToString(),
                                                        fsGAUGE082.ToString(),
                                                        fsGAUGE083.ToString(),
                                                        fsGAUGE084.ToString(),
                                                        fsGAUGE085.ToString(),
                                                        fsGAUGE086.ToString(),
                                                        fsGAUGE087.ToString(),
                                                        fsGAUGE088.ToString(),
                                                        fsGAUGE089.ToString(),
                                                        fsGAUGE090.ToString(),
                                                        fsGAUGE091.ToString(),
                                                        fsGAUGE092.ToString(),
                                                        fsGAUGE093.ToString(),
                                                        fsGAUGE094.ToString(),
                                                        fsGAUGE095.ToString(),
                                                        fsGAUGE096.ToString(),
                                                        fsGAUGE097.ToString(),
                                                        fsGAUGE098.ToString(),
                                                        fsGAUGE099.ToString(),
                                                        fsGAUGE100.ToString(),
                                                        TYUserInfo.EmpNo); //수정

            this.DbConnector.ExecuteNonQuery();

            fsGAUGE001 = "";
            fsGAUGE002 = "";
            fsGAUGE003 = "";
            fsGAUGE004 = "";
            fsGAUGE005 = "";
            fsGAUGE006 = "";
            fsGAUGE007 = "";
            fsGAUGE008 = "";
            fsGAUGE009 = "";
            fsGAUGE010 = "";
            fsGAUGE011 = "";
            fsGAUGE012 = "";
            fsGAUGE013 = "";
            fsGAUGE014 = "";
            fsGAUGE015 = "";
            fsGAUGE016 = "";
            fsGAUGE017 = "";
            fsGAUGE018 = "";
            fsGAUGE019 = "";
            fsGAUGE020 = "";
            fsGAUGE021 = "";
            fsGAUGE022 = "";
            fsGAUGE023 = "";
            fsGAUGE024 = "";
            fsGAUGE025 = "";
            fsGAUGE026 = "";
            fsGAUGE027 = "";
            fsGAUGE028 = "";
            fsGAUGE029 = "";
            fsGAUGE030 = "";
            fsGAUGE031 = "";
            fsGAUGE032 = "";
            fsGAUGE033 = "";
            fsGAUGE034 = "";
            fsGAUGE035 = "";
            fsGAUGE036 = "";
            fsGAUGE037 = "";
            fsGAUGE038 = "";
            fsGAUGE039 = "";
            fsGAUGE040 = "";
            fsGAUGE041 = "";
            fsGAUGE042 = "";
            fsGAUGE043 = "";
            fsGAUGE044 = "";
            fsGAUGE045 = "";
            fsGAUGE046 = "";
            fsGAUGE047 = "";
            fsGAUGE048 = "";
            fsGAUGE049 = "";
            fsGAUGE050 = "";
            fsGAUGE051 = "";
            fsGAUGE052 = "";
            fsGAUGE053 = "";
            fsGAUGE054 = "";
            fsGAUGE055 = "";
            fsGAUGE056 = "";
            fsGAUGE057 = "";
            fsGAUGE058 = "";
            fsGAUGE059 = "";
            fsGAUGE060 = "";
            fsGAUGE061 = "";
            fsGAUGE062 = "";
            fsGAUGE063 = "";
            fsGAUGE064 = "";
            fsGAUGE065 = "";
            fsGAUGE066 = "";
            fsGAUGE067 = "";
            fsGAUGE068 = "";
            fsGAUGE069 = "";
            fsGAUGE070 = "";
            fsGAUGE071 = "";
            fsGAUGE072 = "";
            fsGAUGE073 = "";
            fsGAUGE074 = "";
            fsGAUGE075 = "";
            fsGAUGE076 = "";
            fsGAUGE077 = "";
            fsGAUGE078 = "";
            fsGAUGE079 = "";
            fsGAUGE080 = "";
            fsGAUGE081 = "";
            fsGAUGE082 = "";
            fsGAUGE083 = "";
            fsGAUGE084 = "";
            fsGAUGE085 = "";
            fsGAUGE086 = "";
            fsGAUGE087 = "";
            fsGAUGE088 = "";
            fsGAUGE089 = "";
            fsGAUGE090 = "";
            fsGAUGE091 = "";
            fsGAUGE092 = "";
            fsGAUGE093 = "";
            fsGAUGE094 = "";
            fsGAUGE095 = "";
            fsGAUGE096 = "";
            fsGAUGE097 = "";
            fsGAUGE098 = "";
            fsGAUGE099 = "";
            fsGAUGE100 = "";


            int i = 0;

            for (i = 0; i < 100; i++)
            {
                fsGAUGE[i] = "";

            }
        }
        #endregion

        #region Description : 탱크 용량 테이블 삭제
        private void UP_UTIGAUGF_Del()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_6AQCP553", fsGATANKNO.ToString());

            this.DbConnector.ExecuteNonQuery();
        }
        #endregion

        #region Description : 탱크 자동화 테이블 테이블 업데이트(DB2)
        private void UP_UTAGAUGF_Ins()
        {
            int i = 0;
            int iCount = 0;

            this.DbConnector.CommandClear();
            for (i = 0; i < this.FPS91_TY_S_UT_6APJN523.ActiveSheet.RowCount; i++)
            {
                this.DbConnector.Attach("TY_P_UT_6AQB6547", fsGATANKNO.ToString(),
                                                            this.FPS91_TY_S_UT_6APJN523.GetValue(i, "VOLUME").ToString(),
                                                            this.FPS91_TY_S_UT_6APJN523.GetValue(i, "LEVEL").ToString());

                if (iCount == 1000)
                {
                    this.DbConnector.ExecuteNonQueryList();

                    if (i != this.FPS91_TY_S_UT_6APJN523.ActiveSheet.RowCount - 1)
                    {
                       this.DbConnector.CommandClear();
                    }

                    iCount = 0;
                }

                iCount++;
            }

           this.DbConnector.ExecuteNonQueryList();

            

            //this.DbConnector.ExecuteNonQueryList();

        }
        #endregion

        #region Description : 탱크 자동화 테이블 테이블 삭제(DB2)
        private void UP_UTAGAUGF_Del()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_6AQCN550", fsGATANKNO.ToString());

            this.DbConnector.ExecuteNonQuery();
        }
        #endregion

        #region Description : 탱크 자동화 테이블 테이블 업데이트(오라클)
        private void UP_GAUG_Ins()
        {
            int i = 0;
            int iCount = 0;

            this.DbConnector.CommandClear();

            for (i = 0; i < this.FPS91_TY_S_UT_6APJN523.ActiveSheet.RowCount; i++)
            {
                this.DbConnector.Attach("TY_P_UT_6AQB7548", fsGATANKNO.ToString(),
                                                            this.FPS91_TY_S_UT_6APJN523.GetValue(i, "VOLUME").ToString(),
                                                            this.FPS91_TY_S_UT_6APJN523.GetValue(i, "LEVEL").ToString());

                if (iCount == 1000)
                {
                    this.DbConnector.ExecuteNonQueryList();

                    if (i != this.FPS91_TY_S_UT_6APJN523.ActiveSheet.RowCount - 1)
                    {
                        this.DbConnector.CommandClear();
                    }

                    iCount = 0;
                }

                iCount++;
            }
            this.DbConnector.ExecuteNonQueryList();
        }
        #endregion

        #region Description : 탱크 자동화 테이블 테이블 삭제(오라클)
        private void UP_GAUG_Del()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_6AQCN549", fsGATANKNO.ToString());
            
            this.DbConnector.ExecuteNonQuery();
        }
        #endregion

        #region Description : 찾아보기
        private void BTN61_SEARCH_Click(object sender, EventArgs e)
        {
            OpenFile.Filter = "Excel 97-2003통합 문서(*.xls)|*.xls|Excel 통합 문서 (.xlsx)|*.xlsx|All Files (*.*)|*.*";

            if (this.OpenFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.txtFile.Text = this.OpenFile.FileName;
        }
        #endregion       

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            if (!this.ShowMessage("TY_M_UT_6APKH541"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : MM 업데이트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            
            //// 탱크 자동화 테이블 테이블 삭제(DB2)
            UP_UTAGAUGF_Del();
            //// 탱크 자동화 테이블 테이블 삭제(오라클)
            UP_GAUG_Del();

            //#region Description : 탱크 자동화 테이블 업데이트(DB2)-(MM)

            UP_UTAGAUGF_Ins();

            //#endregion

            //#region Description : 탱크 자동화 테이블 업데이트(오라클)-(MM)

            UP_GAUG_Ins();

            //#endregion

            this.ShowMessage("TY_M_UT_7CC9G226");
        }

        private void BTN61_BATCH_InvokerStart(object sender, TButton.ClickEventCheckArgs e)
        {
            // 탱크 자동화 테이블 테이블 삭제(DB2)
            //UP_UTAGAUGF_Del();
            // 탱크 자동화 테이블 테이블 삭제(오라클)
            //UP_GAUG_Del();

            #region Description : 탱크 자동화 테이블 업데이트(DB2)-(MM)

            //UP_UTAGAUGF_Ins();

            #endregion

            #region Description : 탱크 자동화 테이블 업데이트(오라클)-(MM)

            //UP_GAUG_Ins();

            #endregion
        }
        #endregion
    }
}
