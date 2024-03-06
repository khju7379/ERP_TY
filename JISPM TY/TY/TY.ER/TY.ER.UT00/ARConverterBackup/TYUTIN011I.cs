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
using DataDynamics.ActiveReports;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using FarPoint.Win.Spread.CellType;

namespace TY.ER.UT00
{
    /// <summary>
    /// 세무구분별 매입명세서 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.05.14 16:13
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_25EAH372 : 세무구분별 매입명세서 조회
    ///  TY_P_AC_25G19489 : 세무구분별 매입명세서 출력
    ///  TY_P_AC_25H3V532 : 세무구분별 매입명세서 집계표
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_25E4Y431 : 세무구분별 매입명세서
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_25GAZ484 : 세무 구분을 선택하세요.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  VNCODE : 거래처코드
    ///  B4VLMI1 : 관리항목값１
    ///  B4VLMI2 : 관리항목값２
    ///  B4VLMI4 : 관리항목값４
    ///  GDATEGUBUN : 일자구분
    ///  CBO01_GPRTGN : 출력구분
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYUTIN011I : TYBase
    {
        string fsPOPUP    = string.Empty;
        string fsGATANKNO = string.Empty;
        string fsGAMAJOR  = string.Empty;

        #region Description : 페이지 로드 
        public TYUTIN011I()
        {
            InitializeComponent();
        }

        private void TYUTIN011I_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_UT_6AOD2482.Initialize();
            this.FPS91_TY_S_UT_6AOER484.Initialize();
            this.FPS91_TY_S_UT_6AOFH485.Initialize();

            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_6AOD2482, "GAMAJOR");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            SetStartingFocus(this.TXT01_GATANKNO);
            
            UP_Spread_Title();
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            UP_Spread_Title();
            UP_Spread_Desc();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6AOD1481",
                Set_TankNo(this.TXT01_GATANKNO.GetValue().ToString())
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_UT_6AOD2482.SetValue(dt);
            }
        }
        #endregion

        #region Description : 마감시 필드 잠금
        private void UP_Field_ReadOnly()
        {
            int i = 0;

            
            for (i = 0; i < this.FPS91_TY_S_UT_6AOD2482_Sheet1.RowCount; i++)
            {
                this.FPS91_TY_S_UT_6AOD2482.ActiveSheet.Rows[i].Locked = true;
            }

            for (i = 0; i < this.FPS91_TY_S_UT_6AOFH485_Sheet1.RowCount; i++)
            {
                this.FPS91_TY_S_UT_6AOFH485.ActiveSheet.Rows[i].Locked = true;
            }
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            int i = 0;

            string sGATANKNO = string.Empty;
            string sGAMAJOR  = string.Empty;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_UT_6AOHG490", ds.Tables[0].Rows[i][0].ToString(),
                                                                ds.Tables[0].Rows[i][1].ToString(),
                                                                TYUserInfo.EmpNo
                                                                ); //저장

                    sGATANKNO = ds.Tables[0].Rows[i][0].ToString();
                    sGAMAJOR = ds.Tables[0].Rows[i][1].ToString();
                }
                this.DbConnector.ExecuteNonQueryList();
            }
            else
            {
                sGATANKNO = fsGATANKNO.ToString();
                sGAMAJOR  = fsGAMAJOR.ToString();
            }

            UP_UTIGAUGF_UPT(sGATANKNO, sGAMAJOR);

            this.ShowMessage("TY_M_GB_23NAD873");

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            if (dt.Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_UT_6AOHH491", dt.Rows[i][0].ToString(),
                                                                dt.Rows[i][1].ToString());
                }
                this.DbConnector.ExecuteNonQueryList();
            }

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지

            this.FPS91_TY_S_UT_6AOFH485.Initialize();

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 탱크용량관리 수정
        private void UP_UTIGAUGF_UPT(string sGATANKNO, string sGAMAJOR)
        {
            string[] sGAUGE = new string[100];

            string sGAUGE001 = string.Empty;
            string sGAUGE002 = string.Empty;
            string sGAUGE003 = string.Empty;
            string sGAUGE004 = string.Empty;
            string sGAUGE005 = string.Empty;
            string sGAUGE006 = string.Empty;
            string sGAUGE007 = string.Empty;
            string sGAUGE008 = string.Empty;
            string sGAUGE009 = string.Empty;
            string sGAUGE010 = string.Empty;
            string sGAUGE011 = string.Empty;
            string sGAUGE012 = string.Empty;
            string sGAUGE013 = string.Empty;
            string sGAUGE014 = string.Empty;
            string sGAUGE015 = string.Empty;
            string sGAUGE016 = string.Empty;
            string sGAUGE017 = string.Empty;
            string sGAUGE018 = string.Empty;
            string sGAUGE019 = string.Empty;
            string sGAUGE020 = string.Empty;
            string sGAUGE021 = string.Empty;
            string sGAUGE022 = string.Empty;
            string sGAUGE023 = string.Empty;
            string sGAUGE024 = string.Empty;
            string sGAUGE025 = string.Empty;
            string sGAUGE026 = string.Empty;
            string sGAUGE027 = string.Empty;
            string sGAUGE028 = string.Empty;
            string sGAUGE029 = string.Empty;
            string sGAUGE030 = string.Empty;
            string sGAUGE031 = string.Empty;
            string sGAUGE032 = string.Empty;
            string sGAUGE033 = string.Empty;
            string sGAUGE034 = string.Empty;
            string sGAUGE035 = string.Empty;
            string sGAUGE036 = string.Empty;
            string sGAUGE037 = string.Empty;
            string sGAUGE038 = string.Empty;
            string sGAUGE039 = string.Empty;
            string sGAUGE040 = string.Empty;
            string sGAUGE041 = string.Empty;
            string sGAUGE042 = string.Empty;
            string sGAUGE043 = string.Empty;
            string sGAUGE044 = string.Empty;
            string sGAUGE045 = string.Empty;
            string sGAUGE046 = string.Empty;
            string sGAUGE047 = string.Empty;
            string sGAUGE048 = string.Empty;
            string sGAUGE049 = string.Empty;
            string sGAUGE050 = string.Empty;
            string sGAUGE051 = string.Empty;
            string sGAUGE052 = string.Empty;
            string sGAUGE053 = string.Empty;
            string sGAUGE054 = string.Empty;
            string sGAUGE055 = string.Empty;
            string sGAUGE056 = string.Empty;
            string sGAUGE057 = string.Empty;
            string sGAUGE058 = string.Empty;
            string sGAUGE059 = string.Empty;
            string sGAUGE060 = string.Empty;
            string sGAUGE061 = string.Empty;
            string sGAUGE062 = string.Empty;
            string sGAUGE063 = string.Empty;
            string sGAUGE064 = string.Empty;
            string sGAUGE065 = string.Empty;
            string sGAUGE066 = string.Empty;
            string sGAUGE067 = string.Empty;
            string sGAUGE068 = string.Empty;
            string sGAUGE069 = string.Empty;
            string sGAUGE070 = string.Empty;
            string sGAUGE071 = string.Empty;
            string sGAUGE072 = string.Empty;
            string sGAUGE073 = string.Empty;
            string sGAUGE074 = string.Empty;
            string sGAUGE075 = string.Empty;
            string sGAUGE076 = string.Empty;
            string sGAUGE077 = string.Empty;
            string sGAUGE078 = string.Empty;
            string sGAUGE079 = string.Empty;
            string sGAUGE080 = string.Empty;
            string sGAUGE081 = string.Empty;
            string sGAUGE082 = string.Empty;
            string sGAUGE083 = string.Empty;
            string sGAUGE084 = string.Empty;
            string sGAUGE085 = string.Empty;
            string sGAUGE086 = string.Empty;
            string sGAUGE087 = string.Empty;
            string sGAUGE088 = string.Empty;
            string sGAUGE089 = string.Empty;
            string sGAUGE090 = string.Empty;
            string sGAUGE091 = string.Empty;
            string sGAUGE092 = string.Empty;
            string sGAUGE093 = string.Empty;
            string sGAUGE094 = string.Empty;
            string sGAUGE095 = string.Empty;
            string sGAUGE096 = string.Empty;
            string sGAUGE097 = string.Empty;
            string sGAUGE098 = string.Empty;
            string sGAUGE099 = string.Empty;
            string sGAUGE100 = string.Empty;

            int i = 0;
            int k = 0;

            for (i = 0; i < 25; i++)
            {
                if(k != 0) k++;

                if(k == 100)
                {
                    break;
                }

                sGAUGE[k] = this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 1].Text.ToString();

                k++;
                sGAUGE[k] = this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 3].Text.ToString();

                k++;
                sGAUGE[k] = this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 5].Text.ToString();

                k++;
                sGAUGE[k] = this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 7].Text.ToString();

                k++;
                sGAUGE[k] = this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 9].Text.ToString();
            }

            sGAUGE001 = sGAUGE[0].ToString();
            sGAUGE002 = sGAUGE[1].ToString();
            sGAUGE003 = sGAUGE[2].ToString();
            sGAUGE004 = sGAUGE[3].ToString();
            sGAUGE005 = sGAUGE[4].ToString();
            sGAUGE006 = sGAUGE[5].ToString();
            sGAUGE007 = sGAUGE[6].ToString();
            sGAUGE008 = sGAUGE[7].ToString();
            sGAUGE009 = sGAUGE[8].ToString();
            sGAUGE010 = sGAUGE[9].ToString();
            sGAUGE011 = sGAUGE[10].ToString();
            sGAUGE012 = sGAUGE[11].ToString();
            sGAUGE013 = sGAUGE[12].ToString();
            sGAUGE014 = sGAUGE[13].ToString();
            sGAUGE015 = sGAUGE[14].ToString();
            sGAUGE016 = sGAUGE[15].ToString();
            sGAUGE017 = sGAUGE[16].ToString();
            sGAUGE018 = sGAUGE[17].ToString();
            sGAUGE019 = sGAUGE[18].ToString();
            sGAUGE020 = sGAUGE[19].ToString();
            sGAUGE021 = sGAUGE[20].ToString();
            sGAUGE022 = sGAUGE[21].ToString();
            sGAUGE023 = sGAUGE[22].ToString();
            sGAUGE024 = sGAUGE[23].ToString();
            sGAUGE025 = sGAUGE[24].ToString();
            sGAUGE026 = sGAUGE[25].ToString();
            sGAUGE027 = sGAUGE[26].ToString();
            sGAUGE028 = sGAUGE[27].ToString();
            sGAUGE029 = sGAUGE[28].ToString();
            sGAUGE030 = sGAUGE[29].ToString();
            sGAUGE031 = sGAUGE[30].ToString();
            sGAUGE032 = sGAUGE[31].ToString();
            sGAUGE033 = sGAUGE[32].ToString();
            sGAUGE034 = sGAUGE[33].ToString();
            sGAUGE035 = sGAUGE[34].ToString();
            sGAUGE036 = sGAUGE[35].ToString();
            sGAUGE037 = sGAUGE[36].ToString();
            sGAUGE038 = sGAUGE[37].ToString();
            sGAUGE039 = sGAUGE[38].ToString();
            sGAUGE040 = sGAUGE[39].ToString();
            sGAUGE041 = sGAUGE[40].ToString();
            sGAUGE042 = sGAUGE[41].ToString();
            sGAUGE043 = sGAUGE[42].ToString();
            sGAUGE044 = sGAUGE[43].ToString();
            sGAUGE045 = sGAUGE[44].ToString();
            sGAUGE046 = sGAUGE[45].ToString();
            sGAUGE047 = sGAUGE[46].ToString();
            sGAUGE048 = sGAUGE[47].ToString();
            sGAUGE049 = sGAUGE[48].ToString();
            sGAUGE050 = sGAUGE[49].ToString();
            sGAUGE051 = sGAUGE[50].ToString();
            sGAUGE052 = sGAUGE[51].ToString();
            sGAUGE053 = sGAUGE[52].ToString();
            sGAUGE054 = sGAUGE[53].ToString();
            sGAUGE055 = sGAUGE[54].ToString();
            sGAUGE056 = sGAUGE[55].ToString();
            sGAUGE057 = sGAUGE[56].ToString();
            sGAUGE058 = sGAUGE[57].ToString();
            sGAUGE059 = sGAUGE[58].ToString();
            sGAUGE060 = sGAUGE[59].ToString();
            sGAUGE061 = sGAUGE[60].ToString();
            sGAUGE062 = sGAUGE[61].ToString();
            sGAUGE063 = sGAUGE[62].ToString();
            sGAUGE064 = sGAUGE[63].ToString();
            sGAUGE065 = sGAUGE[64].ToString();
            sGAUGE066 = sGAUGE[65].ToString();
            sGAUGE067 = sGAUGE[66].ToString();
            sGAUGE068 = sGAUGE[67].ToString();
            sGAUGE069 = sGAUGE[68].ToString();
            sGAUGE070 = sGAUGE[69].ToString();
            sGAUGE071 = sGAUGE[70].ToString();
            sGAUGE072 = sGAUGE[71].ToString();
            sGAUGE073 = sGAUGE[72].ToString();
            sGAUGE074 = sGAUGE[73].ToString();
            sGAUGE075 = sGAUGE[74].ToString();
            sGAUGE076 = sGAUGE[75].ToString();
            sGAUGE077 = sGAUGE[76].ToString();
            sGAUGE078 = sGAUGE[77].ToString();
            sGAUGE079 = sGAUGE[78].ToString();
            sGAUGE080 = sGAUGE[79].ToString();
            sGAUGE081 = sGAUGE[80].ToString();
            sGAUGE082 = sGAUGE[81].ToString();
            sGAUGE083 = sGAUGE[82].ToString();
            sGAUGE084 = sGAUGE[83].ToString();
            sGAUGE085 = sGAUGE[84].ToString();
            sGAUGE086 = sGAUGE[85].ToString();
            sGAUGE087 = sGAUGE[86].ToString();
            sGAUGE088 = sGAUGE[87].ToString();
            sGAUGE089 = sGAUGE[88].ToString();
            sGAUGE090 = sGAUGE[89].ToString();
            sGAUGE091 = sGAUGE[90].ToString();
            sGAUGE092 = sGAUGE[91].ToString();
            sGAUGE093 = sGAUGE[92].ToString();
            sGAUGE094 = sGAUGE[93].ToString();
            sGAUGE095 = sGAUGE[94].ToString();
            sGAUGE096 = sGAUGE[95].ToString();
            sGAUGE097 = sGAUGE[96].ToString();
            sGAUGE098 = sGAUGE[97].ToString();
            sGAUGE099 = sGAUGE[98].ToString();
            sGAUGE100 = sGAUGE[99].ToString();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_6AOHT492", sGAUGE001.ToString(),
                                                        sGAUGE002.ToString(),
                                                        sGAUGE003.ToString(),
                                                        sGAUGE004.ToString(),
                                                        sGAUGE005.ToString(),
                                                        sGAUGE006.ToString(),
                                                        sGAUGE007.ToString(),
                                                        sGAUGE008.ToString(),
                                                        sGAUGE009.ToString(),
                                                        sGAUGE010.ToString(),
                                                        sGAUGE011.ToString(),
                                                        sGAUGE012.ToString(),
                                                        sGAUGE013.ToString(),
                                                        sGAUGE014.ToString(),
                                                        sGAUGE015.ToString(),
                                                        sGAUGE016.ToString(),
                                                        sGAUGE017.ToString(),
                                                        sGAUGE018.ToString(),
                                                        sGAUGE019.ToString(),
                                                        sGAUGE020.ToString(),
                                                        sGAUGE021.ToString(),
                                                        sGAUGE022.ToString(),
                                                        sGAUGE023.ToString(),
                                                        sGAUGE024.ToString(),
                                                        sGAUGE025.ToString(),
                                                        sGAUGE026.ToString(),
                                                        sGAUGE027.ToString(),
                                                        sGAUGE028.ToString(),
                                                        sGAUGE029.ToString(),
                                                        sGAUGE030.ToString(),
                                                        sGAUGE031.ToString(),
                                                        sGAUGE032.ToString(),
                                                        sGAUGE033.ToString(),
                                                        sGAUGE034.ToString(),
                                                        sGAUGE035.ToString(),
                                                        sGAUGE036.ToString(),
                                                        sGAUGE037.ToString(),
                                                        sGAUGE038.ToString(),
                                                        sGAUGE039.ToString(),
                                                        sGAUGE040.ToString(),
                                                        sGAUGE041.ToString(),
                                                        sGAUGE042.ToString(),
                                                        sGAUGE043.ToString(),
                                                        sGAUGE044.ToString(),
                                                        sGAUGE045.ToString(),
                                                        sGAUGE046.ToString(),
                                                        sGAUGE047.ToString(),
                                                        sGAUGE048.ToString(),
                                                        sGAUGE049.ToString(),
                                                        sGAUGE050.ToString(),
                                                        sGAUGE051.ToString(),
                                                        sGAUGE052.ToString(),
                                                        sGAUGE053.ToString(),
                                                        sGAUGE054.ToString(),
                                                        sGAUGE055.ToString(),
                                                        sGAUGE056.ToString(),
                                                        sGAUGE057.ToString(),
                                                        sGAUGE058.ToString(),
                                                        sGAUGE059.ToString(),
                                                        sGAUGE060.ToString(),
                                                        sGAUGE061.ToString(),
                                                        sGAUGE062.ToString(),
                                                        sGAUGE063.ToString(),
                                                        sGAUGE064.ToString(),
                                                        sGAUGE065.ToString(),
                                                        sGAUGE066.ToString(),
                                                        sGAUGE067.ToString(),
                                                        sGAUGE068.ToString(),
                                                        sGAUGE069.ToString(),
                                                        sGAUGE070.ToString(),
                                                        sGAUGE071.ToString(),
                                                        sGAUGE072.ToString(),
                                                        sGAUGE073.ToString(),
                                                        sGAUGE074.ToString(),
                                                        sGAUGE075.ToString(),
                                                        sGAUGE076.ToString(),
                                                        sGAUGE077.ToString(),
                                                        sGAUGE078.ToString(),
                                                        sGAUGE079.ToString(),
                                                        sGAUGE080.ToString(),
                                                        sGAUGE081.ToString(),
                                                        sGAUGE082.ToString(),
                                                        sGAUGE083.ToString(),
                                                        sGAUGE084.ToString(),
                                                        sGAUGE085.ToString(),
                                                        sGAUGE086.ToString(),
                                                        sGAUGE087.ToString(),
                                                        sGAUGE088.ToString(),
                                                        sGAUGE089.ToString(),
                                                        sGAUGE090.ToString(),
                                                        sGAUGE091.ToString(),
                                                        sGAUGE092.ToString(),
                                                        sGAUGE093.ToString(),
                                                        sGAUGE094.ToString(),
                                                        sGAUGE095.ToString(),
                                                        sGAUGE096.ToString(),
                                                        sGAUGE097.ToString(),
                                                        sGAUGE098.ToString(),
                                                        sGAUGE099.ToString(),
                                                        sGAUGE100.ToString(),
                                                        sGATANKNO.ToString(),
                                                        sGAMAJOR.ToString()); //수정

            this.DbConnector.ExecuteNonQueryList();
        }
        #endregion

        

        #region Description : 스프레드 타이틀
        private void UP_Spread_Title()
        {
            this.FPS91_TY_S_UT_6AOFH485_Sheet1.ColumnHeaderRowCount = 1;
            this.FPS91_TY_S_UT_6AOFH485_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_UT_6AOFH485_Sheet1.AddColumnHeaderSpanCell(0, 0, 1, 10);

            this.FPS91_TY_S_UT_6AOFH485_Sheet1.ColumnHeader.Cells[0, 0].Value = "GAUGE";

            this.FPS91_TY_S_UT_6AOFH485_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion

        #region Description : 스프레드 틀 만들기
        private void UP_Spread_Desc()
        {
            this.FPS91_TY_S_UT_6AOFH485_Sheet1.ColumnCount = 10;
            this.FPS91_TY_S_UT_6AOFH485_Sheet1.RowCount    = 26;

            int i = 0;
            int k = 0;

            string sGAUGE = string.Empty;

            for(i = 0; i < 25; i++)
            {
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.SetRowHeight(i, 30);

                if(k != 0) k++;

                if(k >= 100)
                {
                    break;
                }

                sGAUGE = Set_Fill2(Convert.ToString(k));
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 0].Value = sGAUGE;

                k++;
                sGAUGE = Set_Fill2(Convert.ToString(k));
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 2].Value = sGAUGE;

                k++;
                sGAUGE = Set_Fill2(Convert.ToString(k));
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 4].Value = sGAUGE;

                k++;
                sGAUGE = Set_Fill2(Convert.ToString(k));
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 6].Value = sGAUGE;

                k++;
                sGAUGE = Set_Fill2(Convert.ToString(k));
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 8].Value = sGAUGE;

                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 0].BackColor = Color.Khaki;
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 2].BackColor = Color.Khaki;
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 4].BackColor = Color.Khaki;
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 6].BackColor = Color.Khaki;
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 8].BackColor = Color.Khaki;

                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 0].Font = new Font("굴림체", 11, FontStyle.Bold);
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 1].Font = new Font("굴림체", 11, FontStyle.Bold);
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 2].Font = new Font("굴림체", 11, FontStyle.Bold);
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 3].Font = new Font("굴림체", 11, FontStyle.Bold);
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 4].Font = new Font("굴림체", 11, FontStyle.Bold);
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 5].Font = new Font("굴림체", 11, FontStyle.Bold);
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 6].Font = new Font("굴림체", 11, FontStyle.Bold);
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 7].Font = new Font("굴림체", 11, FontStyle.Bold);
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 8].Font = new Font("굴림체", 11, FontStyle.Bold);
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 9].Font = new Font("굴림체", 11, FontStyle.Bold);
            }
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_UT_6AOD2482.GetDataSourceInclude(TSpread.TActionType.New, "GATANKNO", "GAMAJOR"));

            if (ds.Tables[0].Rows.Count > 1)
            {
                this.ShowMessage("TY_M_UT_6AOH0487");
                e.Successed = false;
                return;
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_6AOFK486",
                    ds.Tables[0].Rows[i]["GATANKNO"].ToString(),
                    ds.Tables[0].Rows[i]["GAMAJOR"].ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_6AOHD488");
                    e.Successed = false;
                    return;
                }
            }

            // 저장하시겠습니까?
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_UT_6AOD2482.GetDataSourceInclude(TSpread.TActionType.Remove, "GATANKNO", "GAMAJOR");

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;

        }
        #endregion

        #region Description : MAJOR-GAUGE 스프레드 이벤트
        private void FPS91_TY_S_UT_6AOD2482_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_UT_6AOD2482.SetValue(e.RowIndex, "GATANKNO", Set_TankNo(this.TXT01_GATANKNO.GetValue().ToString()));
        }

        private void FPS91_TY_S_UT_6AOD2482_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            fsGATANKNO = "";
            fsGAMAJOR = "";

            fsGATANKNO = this.FPS91_TY_S_UT_6AOD2482.GetValue("GATANKNO").ToString();
            fsGAMAJOR  = this.FPS91_TY_S_UT_6AOD2482.GetValue("GAMAJOR").ToString();

            // 스프레드 RANGE 채우기
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6AOEP483",
                this.FPS91_TY_S_UT_6AOD2482.GetValue("GATANKNO").ToString(),
                this.FPS91_TY_S_UT_6AOD2482.GetValue("GAMAJOR").ToString(),
                this.FPS91_TY_S_UT_6AOD2482.GetValue("GATANKNO").ToString(),
                this.FPS91_TY_S_UT_6AOD2482.GetValue("GAMAJOR").ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_UT_6AOER484.SetValue(dt);
            }

            // 스프레드 GAUGE 채우기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_UT_6AOFK486",
                this.FPS91_TY_S_UT_6AOD2482.GetValue("GATANKNO").ToString(),
                this.FPS91_TY_S_UT_6AOD2482.GetValue("GAMAJOR").ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                UP_Set_Spread_Fill(dt);
            }
            else
            {
                UP_Set_Spread_Zero();
            }
        }
        #endregion

        #region Description : 스프레드 Zero 내용 채우기
        private void UP_Set_Spread_Zero()
        {
            string sGAUGE = string.Empty;

            int k = 0;

            for (int i = 0; i < 25; i++)
            {
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.SetRowHeight(i, 30);

                if (k == 100)
                {
                    break;
                }
                k++;
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 1].Value = string.Format("{0:###0.000}", "0.000");
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 1].Font = new Font("굴림체", 11, FontStyle.Bold); 

                k++;
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 3].Value = string.Format("{0:###0.000}", "0.000");
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 3].Font = new Font("굴림체", 11, FontStyle.Bold); 

                k++;
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 5].Value = string.Format("{0:###0.000}", "0.000");
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 5].Font = new Font("굴림체", 11, FontStyle.Bold); 

                k++;
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 7].Value = string.Format("{0:###0.000}", "0.000");
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 7].Font = new Font("굴림체", 11, FontStyle.Bold); 

                k++;
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 9].Value = string.Format("{0:###0.000}", "0.000");
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 9].Font = new Font("굴림체", 11, FontStyle.Bold);
            }
        }
        #endregion

        #region Description : 스프레드 GAUGE 내용 채우기
        private void UP_Set_Spread_Fill(DataTable dt)
        {
            string sGAUGE = string.Empty;

            int k = 0;

            for (int i = 0; i < 25; i++)
            {
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.SetRowHeight(i, 30);

                if (k == 100)
                {
                    break;
                }
                k++;
                sGAUGE = "GAUGE" + Set_Fill3(Convert.ToString(k));
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 1].Value = string.Format("{0:###0.000}", dt.Rows[0][sGAUGE.ToString()].ToString());

                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 1].Font = new Font("굴림체", 11, FontStyle.Bold);

                k++;
                sGAUGE = "GAUGE" + Set_Fill3(Convert.ToString(k));
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 3].Value = string.Format("{0:###0.000}", dt.Rows[0][sGAUGE.ToString()].ToString());
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 3].Font = new Font("굴림체", 11, FontStyle.Bold);

                k++;
                sGAUGE = "GAUGE" + Set_Fill3(Convert.ToString(k));
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 5].Value = string.Format("{0:###0.000}", dt.Rows[0][sGAUGE.ToString()].ToString());
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 5].Font = new Font("굴림체", 11, FontStyle.Bold);

                k++;
                sGAUGE = "GAUGE" + Set_Fill3(Convert.ToString(k));
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 7].Value = string.Format("{0:###0.000}", dt.Rows[0][sGAUGE.ToString()].ToString());
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 7].Font = new Font("굴림체", 11, FontStyle.Bold);

                k++;
                sGAUGE = "GAUGE" + Set_Fill3(Convert.ToString(k));
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 9].Value = string.Format("{0:###0.000}", dt.Rows[0][sGAUGE.ToString()].ToString());
                this.FPS91_TY_S_UT_6AOFH485_Sheet1.Cells[i, 9].Font = new Font("굴림체", 11, FontStyle.Bold);
            }
        }
        #endregion
    }
}