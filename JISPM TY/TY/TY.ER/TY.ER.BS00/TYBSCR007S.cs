using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.BS00
{
    /// <summary>
    /// 사업계획 손익계산서 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.09.01 16:05
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_791GG520 : 사업계획 손익계산서 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_791HY521 : 사업계획 손익계산서 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  INQOPTION : 조회구분
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYBSCR007S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYBSCR007S()
        {
            InitializeComponent();
        }

        private void TYBSCR007S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy"));

            this.SetStartingFocus(DTP01_SDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_791HY521.Initialize();
            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_791GG520", this.DTP01_SDATE.GetString().ToString().Substring(0,4), CBO01_INQOPTION.GetValue() );
            this.FPS91_TY_S_AC_791HY521.SetValue(UP_InsertToRow(this.DbConnector.ExecuteDataTable()));
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private DataTable UP_InsertToRow(DataTable dt)
        {
            //물량 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_794D3525", this.DTP01_SDATE.GetString().ToString().Substring(0, 4), CBO01_INQOPTION.GetValue());
            DataTable dtsu = this.DbConnector.ExecuteDataTable();

            //내부거래
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_794EH526", this.DTP01_SDATE.GetString().ToString().Substring(0, 4), CBO01_INQOPTION.GetValue());
            DataTable dtlc = this.DbConnector.ExecuteDataTable();
            
            int i = 0;
            DataTable table = new DataTable();

            table = dt;
            DataRow row;
            int nNum = table.Rows.Count;

            for (i = 1; i < nNum; i++)
            {
                //매출액
                if (table.Rows[i - 1]["BPLCDAC"].ToString() == "01000000")
                {
                    //물량
                    if (CBO01_INQOPTION.GetValue().ToString() != "A")
                    {
                        if (dtsu.Rows.Count > 0)
                        {
                            row = table.NewRow();
                            table.Rows.InsertAt(row, i);

                            table.Rows[i]["BPLYYHD"] = table.Rows[i - 1]["BPLYYHD"].ToString();
                            table.Rows[i]["BPLCDAC"] = table.Rows[i - 1]["BPLCDAC"].ToString();
                            table.Rows[i]["BPLCDACNM"] = "    (물    량)";

                            table.Rows[i]["BPLCR01"] = dtsu.Rows[0]["BSMONAMT01"].ToString();
                            table.Rows[i]["BPLCR02"] = dtsu.Rows[0]["BSMONAMT02"].ToString();
                            table.Rows[i]["BPLCR03"] = dtsu.Rows[0]["BSMONAMT03"].ToString();
                            table.Rows[i]["BPLCR04"] = dtsu.Rows[0]["BSMONAMT04"].ToString();
                            table.Rows[i]["BPLCR05"] = dtsu.Rows[0]["BSMONAMT05"].ToString();
                            table.Rows[i]["BPLCR06"] = dtsu.Rows[0]["BSMONAMT06"].ToString();
                            table.Rows[i]["BPLCR07"] = dtsu.Rows[0]["BSMONAMT07"].ToString();
                            table.Rows[i]["BPLCR08"] = dtsu.Rows[0]["BSMONAMT08"].ToString();
                            table.Rows[i]["BPLCR09"] = dtsu.Rows[0]["BSMONAMT09"].ToString();
                            table.Rows[i]["BPLCR10"] = dtsu.Rows[0]["BSMONAMT10"].ToString();
                            table.Rows[i]["BPLCR11"] = dtsu.Rows[0]["BSMONAMT11"].ToString();
                            table.Rows[i]["BPLCR12"] = dtsu.Rows[0]["BSMONAMT12"].ToString();

                            table.Rows[i]["BPLCRTOTAL"] = dtsu.Rows[0]["BPLCRTOTAL"].ToString();
                            table.Rows[i]["BPLFHALF"] = dtsu.Rows[0]["BPLFHALF"].ToString();
                            table.Rows[i]["BPLSHALF"] = dtsu.Rows[0]["BPLSHALF"].ToString();

                            nNum = nNum + 1;

                            i = i + 1;
                        }
                    }
                   
                    //내부거래
                    if (dtlc.Rows.Count > 0)
                    {
                        row = table.NewRow();
                        table.Rows.InsertAt(row, i);

                        table.Rows[i]["BPLYYHD"] = table.Rows[i - 1]["BPLYYHD"].ToString();
                        table.Rows[i]["BPLCDAC"] = table.Rows[i - 1]["BPLCDAC"].ToString();
                        table.Rows[i]["BPLCDACNM"] = "    (내부거래)";

                        table.Rows[i]["BPLCR01"] = dtlc.Rows[0]["BSMONAMT01"].ToString();
                        table.Rows[i]["BPLCR02"] = dtlc.Rows[0]["BSMONAMT02"].ToString();
                        table.Rows[i]["BPLCR03"] = dtlc.Rows[0]["BSMONAMT03"].ToString();
                        table.Rows[i]["BPLCR04"] = dtlc.Rows[0]["BSMONAMT04"].ToString();
                        table.Rows[i]["BPLCR05"] = dtlc.Rows[0]["BSMONAMT05"].ToString();
                        table.Rows[i]["BPLCR06"] = dtlc.Rows[0]["BSMONAMT06"].ToString();
                        table.Rows[i]["BPLCR07"] = dtlc.Rows[0]["BSMONAMT07"].ToString();
                        table.Rows[i]["BPLCR08"] = dtlc.Rows[0]["BSMONAMT08"].ToString();
                        table.Rows[i]["BPLCR09"] = dtlc.Rows[0]["BSMONAMT09"].ToString();
                        table.Rows[i]["BPLCR10"] = dtlc.Rows[0]["BSMONAMT10"].ToString();
                        table.Rows[i]["BPLCR11"] = dtlc.Rows[0]["BSMONAMT11"].ToString();
                        table.Rows[i]["BPLCR12"] = dtlc.Rows[0]["BSMONAMT12"].ToString();

                        table.Rows[i]["BPLCRTOTAL"] = dtlc.Rows[0]["BPLCRTOTAL"].ToString();
                        table.Rows[i]["BPLFHALF"] = dtlc.Rows[0]["BPLFHALF"].ToString();
                        table.Rows[i]["BPLSHALF"] = dtlc.Rows[0]["BPLSHALF"].ToString();

                        nNum = nNum + 1;

                        i = i + 1;
                    }                  
                }

                //매출원가
                if (table.Rows[i - 1]["BPLCDAC"].ToString() == "02000000")
                {
                    //내부거래
                    if (dtlc.Rows.Count > 0)
                    {
                        row = table.NewRow();
                        table.Rows.InsertAt(row, i);

                        table.Rows[i]["BPLYYHD"] = table.Rows[i - 1]["BPLYYHD"].ToString();
                        table.Rows[i]["BPLCDAC"] = table.Rows[i - 1]["BPLCDAC"].ToString();
                        table.Rows[i]["BPLCDACNM"] = "    (내부거래)";

                        table.Rows[i]["BPLCR01"] = dtlc.Rows[1]["BSMONAMT01"].ToString();
                        table.Rows[i]["BPLCR02"] = dtlc.Rows[1]["BSMONAMT02"].ToString();
                        table.Rows[i]["BPLCR03"] = dtlc.Rows[1]["BSMONAMT03"].ToString();
                        table.Rows[i]["BPLCR04"] = dtlc.Rows[1]["BSMONAMT04"].ToString();
                        table.Rows[i]["BPLCR05"] = dtlc.Rows[1]["BSMONAMT05"].ToString();
                        table.Rows[i]["BPLCR06"] = dtlc.Rows[1]["BSMONAMT06"].ToString();
                        table.Rows[i]["BPLCR07"] = dtlc.Rows[1]["BSMONAMT07"].ToString();
                        table.Rows[i]["BPLCR08"] = dtlc.Rows[1]["BSMONAMT08"].ToString();
                        table.Rows[i]["BPLCR09"] = dtlc.Rows[1]["BSMONAMT09"].ToString();
                        table.Rows[i]["BPLCR10"] = dtlc.Rows[1]["BSMONAMT10"].ToString();
                        table.Rows[i]["BPLCR11"] = dtlc.Rows[1]["BSMONAMT11"].ToString();
                        table.Rows[i]["BPLCR12"] = dtlc.Rows[1]["BSMONAMT12"].ToString();

                        table.Rows[i]["BPLCRTOTAL"] = dtlc.Rows[1]["BPLCRTOTAL"].ToString();
                        table.Rows[i]["BPLFHALF"] = dtlc.Rows[1]["BPLFHALF"].ToString();
                        table.Rows[i]["BPLSHALF"] = dtlc.Rows[1]["BPLSHALF"].ToString();

                        nNum = nNum + 1;

                        i = i + 1;
                    }                  
                }

                //매출총이익
                if (table.Rows[i - 1]["BPLCDAC"].ToString() == "03000000")
                {
                    //내부거래
                    if (dtlc.Rows.Count > 0)
                    {
                        row = table.NewRow();
                        table.Rows.InsertAt(row, i);

                        table.Rows[i]["BPLYYHD"] = table.Rows[i - 1]["BPLYYHD"].ToString();
                        table.Rows[i]["BPLCDAC"] = table.Rows[i - 1]["BPLCDAC"].ToString();
                        table.Rows[i]["BPLCDACNM"] = "    (내부거래)";

                        table.Rows[i]["BPLCR01"] = dtlc.Rows[2]["BSMONAMT01"].ToString();
                        table.Rows[i]["BPLCR02"] = dtlc.Rows[2]["BSMONAMT02"].ToString();
                        table.Rows[i]["BPLCR03"] = dtlc.Rows[2]["BSMONAMT03"].ToString();
                        table.Rows[i]["BPLCR04"] = dtlc.Rows[2]["BSMONAMT04"].ToString();
                        table.Rows[i]["BPLCR05"] = dtlc.Rows[2]["BSMONAMT05"].ToString();
                        table.Rows[i]["BPLCR06"] = dtlc.Rows[2]["BSMONAMT06"].ToString();
                        table.Rows[i]["BPLCR07"] = dtlc.Rows[2]["BSMONAMT07"].ToString();
                        table.Rows[i]["BPLCR08"] = dtlc.Rows[2]["BSMONAMT08"].ToString();
                        table.Rows[i]["BPLCR09"] = dtlc.Rows[2]["BSMONAMT09"].ToString();
                        table.Rows[i]["BPLCR10"] = dtlc.Rows[2]["BSMONAMT10"].ToString();
                        table.Rows[i]["BPLCR11"] = dtlc.Rows[2]["BSMONAMT11"].ToString();
                        table.Rows[i]["BPLCR12"] = dtlc.Rows[2]["BSMONAMT12"].ToString();

                        table.Rows[i]["BPLCRTOTAL"] = dtlc.Rows[2]["BPLCRTOTAL"].ToString();
                        table.Rows[i]["BPLFHALF"] = dtlc.Rows[2]["BPLFHALF"].ToString();
                        table.Rows[i]["BPLSHALF"] = dtlc.Rows[2]["BPLSHALF"].ToString();

                        nNum = nNum + 1;

                        i = i + 1;
                    }
                }
            }

            return table;
        }
        #endregion

    }
}
