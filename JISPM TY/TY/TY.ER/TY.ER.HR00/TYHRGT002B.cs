using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 근무표 생성 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.12.10 17:39
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
    ///  CLO : 닫기
    ///  CREATE : 생성
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYHRGT002B : TYBase
    {
        #region Description : 페이지 로드
        public TYHRGT002B()
        {
            InitializeComponent();
        }

        private void TYHRGT002B_Load(object sender, System.EventArgs e)
        {
            // 생성 체크
            this.BTN61_CREATE.ProcessCheck += new TButton.CheckHandler(BTN61_CREATE_ProcessCheck);
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 생성 버튼
        private void BTN61_CREATE_Click(object sender, EventArgs e)
        {
            int iMontCount = Convert.ToInt16(DTP01_GEDYYMM.GetString().Substring(4, 2)) - Convert.ToInt16(DTP01_GSTYYMM.GetString().Substring(4, 2));

            for (int i = 0; i <= iMontCount; i++)
            {
                System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

                DateTime EDDAY = new DateTime();

                int iEday = 0;
                int iDaycnt = (i == 0) ? Convert.ToInt16(DTP01_GSTYYMM.GetString().Substring(6, 2)) : 1;
                int iDayStart = iDaycnt;

                if (i == iMontCount)
                {
                    EDDAY = Convert.ToDateTime(DTP01_GEDYYMM.GetString().Substring(0, 4) + "-" + (Convert.ToInt16(DTP01_GEDYYMM.GetString().Substring(4, 2))).ToString() + "-" + DTP01_GEDYYMM.GetString().Substring(6, 2));
                    iEday = Convert.ToInt16(EDDAY.ToString("dd"));
                }
                else
                {
                    if (Convert.ToInt16(DTP01_GSTYYMM.GetString().Substring(4, 2)) + i == 12)
                    {
                        EDDAY = Convert.ToDateTime((Convert.ToInt16(DTP01_GSTYYMM.GetString().Substring(0, 4)) + 1).ToString() + "-01-01").AddDays(-1);
                        iEday = Convert.ToInt16(EDDAY.ToString("dd"));
                    }
                    else
                    {
                        EDDAY = Convert.ToDateTime(DTP01_GSTYYMM.GetString().Substring(0, 4) + "-" + (Convert.ToInt16(DTP01_GSTYYMM.GetString().Substring(4, 2)) + i + 1).ToString() + "-01").AddDays(-1);
                        iEday = Convert.ToInt16(EDDAY.ToString("dd"));
                    }

                }
                UP_Del_GTWORKJOE(EDDAY.ToString("yyyyMM") + Set_Fill2(iDaycnt.ToString()), EDDAY.ToString("yyyyMMdd"));
                UP_Del_GTGDREF(EDDAY.ToString("yyyyMM") + Set_Fill2(iDaycnt.ToString()), EDDAY.ToString("yyyyMMdd"));

                // 교대조 관리표 생성
                while (true)
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_HR_4CAJM684", EDDAY.ToString("yyyyMM") + Set_Fill2(iDaycnt.ToString()),
                                                                "M",
                                                                UP_WorkJoe_Return(EDDAY.ToString("yyyy-MM-") + Set_Fill2(iDaycnt.ToString()), "M"),
                                                                TYUserInfo.EmpNo
                                                                );  //M조

                    this.DbConnector.ExecuteNonQuery();
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_HR_4CAJM684", EDDAY.ToString("yyyyMM") + Set_Fill2(iDaycnt.ToString()),
                                                                "E",
                                                                UP_WorkJoe_Return(EDDAY.ToString("yyyy-MM-") + Set_Fill2(iDaycnt.ToString()), "E"),
                                                                TYUserInfo.EmpNo
                                                                );  //E조

                    this.DbConnector.ExecuteNonQuery();
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_HR_4CAJM684", EDDAY.ToString("yyyyMM") + Set_Fill2(iDaycnt.ToString()),
                                                                "N",
                                                                UP_WorkJoe_Return(EDDAY.ToString("yyyy-MM-") + Set_Fill2(iDaycnt.ToString()), "N"),
                                                                TYUserInfo.EmpNo
                                                                );  //N조

                    this.DbConnector.ExecuteNonQuery();
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_HR_4CAJM684", EDDAY.ToString("yyyyMM") + Set_Fill2(iDaycnt.ToString()),
                                                                "O",
                                                                UP_WorkJoe_Return(EDDAY.ToString("yyyy-MM-") + Set_Fill2(iDaycnt.ToString()), "O"),
                                                                TYUserInfo.EmpNo
                                                                );  //0조

                    this.DbConnector.ExecuteNonQuery();

                    iDaycnt++;

                    if (iEday < iDaycnt)
                    {
                        break;
                    }
                }

                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_HR_938E4032", EDDAY.ToString("yyyyMM")+Set_Fill2(iDayStart.ToString()) , EDDAY.ToString("yyyyMMdd"));  //생성된 교대조 관리표 조회

                DataTable dt = this.DbConnector.ExecuteDataTable();

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_HR_4CBBK702", dt.Rows[j]["GTWJOE"].ToString());  //근무조 조원 조회

                    DataTable dtMember = this.DbConnector.ExecuteDataTable();

                    for (int k = 0; k < dtMember.Rows.Count; k++)
                    {
                        string GDINTIME = string.Empty;
                        string GDOUTTIME = string.Empty;
                        string GTWGUBUN = string.Empty;

                        if (dt.Rows[j]["GTWGUBUN"].ToString() == "M")
                        {
                            GDINTIME = "0700";
                            GDOUTTIME = "1500";
                            GTWGUBUN = "1";
                        }
                        else if (dt.Rows[j]["GTWGUBUN"].ToString() == "E")
                        {
                            GDINTIME = "1500";
                            GDOUTTIME = "2300";
                            GTWGUBUN = "2";
                        }
                        else if (dt.Rows[j]["GTWGUBUN"].ToString() == "N")
                        {
                            GDINTIME = "2300";
                            GDOUTTIME = "0700";
                            GTWGUBUN = "3";
                        }
                        else if (dt.Rows[j]["GTWGUBUN"].ToString() == "O")
                        {
                            GDINTIME = "0000";
                            GDOUTTIME = "0000";
                            GTWGUBUN = "5";
                        }

                        datas.Add(new object[] {dt.Rows[j]["GTWDATE"].ToString(),
                                                dt.Rows[j]["GTWJOE"].ToString(),
                                                dtMember.Rows[k]["GJSABUN"].ToString(),
                                                GTWGUBUN,
                                                "",
                                                GDINTIME,
                                                GDOUTTIME,
                                                "",
                                                TYUserInfo.EmpNo
                                                });
                    }
                }
                if (datas.Count > 0)
                {
                    this.DbConnector.CommandClear();
                    foreach (object[] data in datas)
                    {
                        this.DbConnector.Attach("TY_P_HR_4C8F6662", data);
                    }

                    this.DbConnector.ExecuteTranQueryList();
                }
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ShowMessage("TY_M_GB_26E30875");
        }
        #endregion

        #region Description : 생성 ProcessCheck 이벤트
        private void BTN61_CREATE_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            string STDATE = this.DTP01_GSTYYMM.GetString();
            string EDDATE = this.DTP01_GEDYYMM.GetString();
            string sDATE = string.Empty;

            if (STDATE.Substring(0, 4) != EDDATE.Substring(0, 4))
            {
                this.ShowCustomMessage("생성년도가 동일하지 않습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (Convert.ToInt16(STDATE.Substring(4, 2)) > Convert.ToInt16(EDDATE.Substring(4, 2)))
            {
                this.ShowCustomMessage("생성시작월이 종료월보다 작습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            this.DbConnector.CommandClear();

            if (Convert.ToInt32(STDATE.Substring(4, 2)) == 1)
            {
                sDATE = (Convert.ToInt32(STDATE.Substring(0, 4)) - 1).ToString() + "12";
            }
            else
            {
                sDATE = Convert.ToInt32(STDATE.Substring(0, 4)).ToString() + string.Format("{0:00}", (Convert.ToInt32(STDATE.Substring(4, 2)) - 1));
            }

            this.DbConnector.Attach("TY_P_HR_4CBBJ701", sDATE);

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowCustomMessage("전월 근무표가 생성되지 않았습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_26E2Z874"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 교대조 생성 관리표 삭제
        private void UP_Del_GTWORKJOE(string STDATE, string EDDATE)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_HR_4CAJE679", STDATE,
                                                        EDDATE);

            this.DbConnector.ExecuteNonQueryList();
        }
        #endregion

        #region Description : 일별근무자관리 삭제
        private void UP_Del_GTGDREF(string STDATE, string EDDATE)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_HR_4CAJG680", STDATE,
                                                        EDDATE);

            this.DbConnector.ExecuteNonQueryList();
        }
        #endregion

        #region Description : 근무조 받아오기
        private string UP_WorkJoe_Return(string GTWDATE, string GTWGUBN)
        {
            string sReturn_Joe = string.Empty;
            string sFirstJoe = string.Empty;
            int iRowCount = 0;
            int iJoeA = 0;
            int iJoeB = 0;
            int iJoeC = 0;
            int iJoeD = 0;

            DataTable dt = new DataTable();

            if (GTWGUBN != "O") //휴무조가 아닌경우
            {

                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                    (
                    "TY_P_HR_4CAK5694",
                    Convert.ToDateTime(GTWDATE).AddDays(-4).ToString("yyyyMMdd"),
                    GTWDATE.Replace("-", ""),
                    GTWGUBN);

                dt = this.DbConnector.ExecuteDataTable();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (iRowCount == 0)
                    {
                        sFirstJoe = dt.Rows[i]["GTWJOE"].ToString();
                    }

                    if (dt.Rows[i]["GTWJOE"].ToString() == "A")
                    {
                        iJoeA++;
                    }
                    else if (dt.Rows[i]["GTWJOE"].ToString() == "B")
                    {
                        iJoeB++;
                    }
                    else if (dt.Rows[i]["GTWJOE"].ToString() == "C")
                    {
                        iJoeC++;
                    }
                    else if (dt.Rows[i]["GTWJOE"].ToString() == "D")
                    {
                        iJoeD++;
                    }

                    iRowCount++;
                }
                if (sFirstJoe == "A")
                {
                    if (iJoeA < 4)
                    {
                        sReturn_Joe = "A";
                    }
                    else
                    {
                        sReturn_Joe = "B";
                    }
                }
                else if (sFirstJoe == "B")
                {
                    if (iJoeB < 4)
                    {
                        sReturn_Joe = "B";
                    }
                    else
                    {
                        sReturn_Joe = "C";
                    }
                }
                else if (sFirstJoe == "C")
                {
                    if (iJoeC < 4)
                    {
                        sReturn_Joe = "C";
                    }
                    else
                    {
                        sReturn_Joe = "D";
                    }
                }
                else if (sFirstJoe == "D")
                {
                    if (iJoeD < 4)
                    {
                        sReturn_Joe = "D";
                    }
                    else
                    {
                        sReturn_Joe = "A";
                    }
                }
            }
            else                // 휴무조인 경우
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                    (
                    "TY_P_HR_4CAK8695",
                    GTWDATE.Replace("-", ""));

                dt = this.DbConnector.ExecuteDataTable();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["GTWJOE"].ToString() == "A")
                    {
                        iJoeA++;
                    }
                    else if (dt.Rows[i]["GTWJOE"].ToString() == "B")
                    {
                        iJoeB++;
                    }
                    else if (dt.Rows[i]["GTWJOE"].ToString() == "C")
                    {
                        iJoeC++;
                    }
                    else if (dt.Rows[i]["GTWJOE"].ToString() == "D")
                    {
                        iJoeD++;
                    }
                }
                if (iJoeA == 0)
                {
                    sReturn_Joe = "A";
                }
                if (iJoeB == 0)
                {
                    sReturn_Joe = "B";
                }
                if (iJoeC == 0)
                {
                    sReturn_Joe = "C";
                }
                if (iJoeD == 0)
                {
                    sReturn_Joe = "D";
                }
            }

            return sReturn_Joe;
        }
        #endregion
    }
}
