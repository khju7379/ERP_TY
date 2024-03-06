using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.UT00
{
    /// <summary>
    /// 코드박스 - 장기계약 조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.11.08 10:56
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_2B8C1196 : 코드박스 - 장기계약 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_2B84W204 : 코드박스-장기계약 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  OPM1020 : 계약업체
    ///  OPM1000 : 계약년도
    ///  OPM1040 : 계약내용
    ///  PRM1020 : 년월
    /// </summary>
    public partial class TYUTGB014S : TYBase
    {
        public string fsORDATE = string.Empty;
        public string fsORSEQ  = string.Empty;

        #region Description : 페이지 로드
        public TYUTGB014S()
        {
            InitializeComponent();
            this.SetPopupStyle();
        }

        private void TYUTGB014S_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_UT_6AJGE418.Initialize();

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_STIPHANG.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDIPHANG.SetValue(DateTime.Now.AddDays(+7).ToString("yyyy-MM-dd"));

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.DTP01_STIPHANG);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sVNRPCODE = string.Empty;

            if (this.CBH01_JIJGHWAJU.GetValue().ToString() != "")
            {
                // 대표거래처 코드 가져오기
                sVNRPCODE = Get_VNCODE(this.CBH01_JIJGHWAJU.GetValue().ToString());
            }

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            if (this.CBH01_JIJGHWAJU.GetValue().ToString() == "")
            {
                this.DbConnector.Attach
                   (
                   "TY_P_UT_6AJGB416",
                   Get_Date(this.DTP01_STIPHANG.GetValue().ToString()),
                   Get_Date(this.DTP01_EDIPHANG.GetValue().ToString())
                   );
            }
            else
            {
                this.DbConnector.Attach
                   (
                   "TY_P_UT_6AJGC417",
                   Get_Date(this.DTP01_STIPHANG.GetValue().ToString()),
                   Get_Date(this.DTP01_EDIPHANG.GetValue().ToString()),
                   sVNRPCODE.ToString()
                   );
            }

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_6AJGE418.SetValue(dt);

            //if (dt.Rows.Count <= 0)
            //{
            //    this.ShowMessage("TY_M_AC_2422N250");
            //}
        }
        #endregion

        #region Description : 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sSSID   = string.Empty;
            string sOUTMSG = string.Empty;

            sSSID = this.IPAdresss + Employer.EmpNo;

            // TEMP 삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_6AJH3419", sSSID.ToString());
            this.DbConnector.ExecuteNonQuery();

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // TEMP 등록
                this.DbConnector.Attach("TY_P_UT_6AJHC421", sSSID.ToString(),
                                                            ds.Tables[0].Rows[i]["JIYYMM"].ToString(),
                                                            ds.Tables[0].Rows[i]["JISEQ"].ToString());
            }

            this.DbConnector.ExecuteTranQueryList();

            // 지시 일괄 등록 SP 수행
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_6AJBE412", sSSID.ToString(),
                                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                                        sOUTMSG.ToString()
                                                        );

            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUTMSG.Substring(0, 1) != "I")
            {
                return;
            }


            // 메일 보내기
            // 거래처 담당자의 메일 계정이 있는 DB를 사용 안해서 메일 보내는거 코딩 추가 안함.
            // DB(AVMOBILEF)

            this.ShowMessage("TY_M_MR_2BF50354");

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 처리 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_UT_6AJGE418.GetDataSourceInclude(TSpread.TActionType.Select, "JIYYMM", "JISEQ"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2CV43442");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_MR_2BF50353"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion
    }
}