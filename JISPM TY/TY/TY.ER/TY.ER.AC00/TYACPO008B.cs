using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 품목별매출현황 생성 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.07.10 15:27
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
    ///  TY_M_GB_26E2Z874 : 생성하시겠습니까?
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    ///  TY_M_GB_26E31876 : 생성 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACPO008B : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYACPO008B()
        {
            InitializeComponent();
            this.SetPopupStyle(); 
        }

        private void TYACPO008B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_GSTYYMM.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"));

            this.SetStartingFocus(this.DTP01_GSTYYMM);
        }
        #endregion

        #region  Description : 생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            
            string sParamITEMCODE = "";
            string sITEMCODETOTAL = "";

            string sCDDP_Old = "";
            string sGRCODE_Old = "";

            //삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_37A30067", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
            this.DbConnector.ExecuteTranQuery(); 

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_37BBR080", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
            DataTable dr = this.DbConnector.ExecuteDataTable();

            if (dr.Rows.Count > 0)
            {
                for (int k = 0; k < dr.Rows.Count; k++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_37AAJ055", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), dr.Rows[k]["ERCDDP"].ToString());
                    DataTable dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //부서,그룹코드가 바뀌면 생성
                            if (i != 0 && (sCDDP_Old != dt.Rows[i]["ERCDDP"].ToString() || sGRCODE_Old != dt.Rows[i]["ERGRCODE"].ToString()))
                            {
                                sParamITEMCODE = sParamITEMCODE.Substring(0, sParamITEMCODE.Length - 1);

                                UP_Sales_Create(this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), sCDDP_Old, sParamITEMCODE, dt.Rows[i - 1]["ERGRCODE"].ToString());

                                sParamITEMCODE = "";
                                sCDDP_Old = "";
                                sGRCODE_Old = "";
                            }
                            sParamITEMCODE = sParamITEMCODE + dt.Rows[i]["ERITEMCODE"].ToString() + ",";

                            sITEMCODETOTAL = sITEMCODETOTAL + dt.Rows[i]["ERITEMCODE"].ToString() + ",";

                            sCDDP_Old = dt.Rows[i]["ERCDDP"].ToString();
                            sGRCODE_Old = dt.Rows[i]["ERGRCODE"].ToString();

                            if (i == dt.Rows.Count - 1)
                            {
                                sParamITEMCODE = sParamITEMCODE.Substring(0, sParamITEMCODE.Length - 1);

                                UP_Sales_Create(this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), sCDDP_Old, sParamITEMCODE, dt.Rows[i]["ERGRCODE"].ToString());                            
                            }
                        } //for (int i = 0; i < dt.Rows.Count; i++)...end

                        //주요품목처리 완료후 기타부분 처리
                        sITEMCODETOTAL = sITEMCODETOTAL.Substring(0, sITEMCODETOTAL.Length - 1);

                        UP_Sales_Create(this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), sCDDP_Old, sITEMCODETOTAL, "900");

                        sParamITEMCODE = "";
                        sCDDP_Old = "";
                        sGRCODE_Old = "";
                        sITEMCODETOTAL = "";
                    }

                } // for (int k = 0; k < dr.Rows.Count; k++)..end
            }

            this.ShowMessage("TY_M_GB_26E30875");
        }
        #endregion

        #region Description : 품목별매출이익현황 생성
        private void UP_Sales_Create(string sYYMM, string sParamCDDP, string sParamITEMCODE, string sGRCODE)
        {
            DataTable dt = new DataTable();
            
            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

            string sYYMM_S = "";

            sYYMM_S = sYYMM.Substring(0, 4) + "01";

            this.DbConnector.CommandClear();
            //월별 금액
            if (sGRCODE != "900")
            {
                this.DbConnector.Attach("TY_P_AC_37A51076", sYYMM_S, sYYMM, sParamCDDP, sParamITEMCODE);
                dt = this.DbConnector.ExecuteDataTable();
            }
            else
            {
                this.DbConnector.Attach("TY_P_AC_37BBK079", sYYMM_S, sYYMM, sParamCDDP, sParamITEMCODE);
                dt = this.DbConnector.ExecuteDataTable();
            }

            if (dt.Rows.Count > 0)
            {
                datas.Add(new object[] { sYYMM,
                                         sParamCDDP,
                                         sGRCODE,
                                         dt.Rows[0]["PDMAEAMT17"].ToString(),
                                         dt.Rows[0]["PDMAWAMT8"].ToString(),                                         
                                         dt.Rows[0]["PDMCIAMT11"].ToString(),
                                         dt.Rows[0]["PDMAEAMT17_TOTAL"].ToString(),
                                         dt.Rows[0]["PDMAWAMT8_TOTAL"].ToString(),                                         
                                         dt.Rows[0]["PDMCIAMT11_TOTAL"].ToString(),
                                         "A",
                                         TYUserInfo.EmpNo});
            }

            if (datas.Count > 0)
            {
                this.DbConnector.CommandClear();
                foreach (object[] data in datas)
                {
                    this.DbConnector.Attach("TY_P_AC_37A2Z066", data); //대변 저장                
                }

                this.DbConnector.ExecuteTranQueryList(); 
            }

        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
        #endregion

        #region Description : 처리 체크
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // 마감 완료 CHECK 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_27H64059", this.DTP01_GSTYYMM.GetValue().ToString().Substring(0, 4), this.DTP01_GSTYYMM.GetValue().ToString().Substring(4, 2));
            DataTable dt1 = this.DbConnector.ExecuteDataTable();

            if (dt1.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                e.Successed = false;
                return;
            }
            else
            {
                if (dt1.Rows[0]["ECGUBUN"].ToString() == "Y" || dt1.Rows[0]["ECGUBUN"].ToString() == "Z")
                {
                    this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_26E2Z874"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion
    }
}
