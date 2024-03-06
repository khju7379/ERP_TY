using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.ER.GB00;
using DataDynamics.ActiveReports;


namespace TY.ER.US00
{
    /// <summary>
    /// 모선,화주별 재고 집계표 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.06.24 11:41
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_96OEH925 : 모선,화주별 재고집계표
    ///  TY_P_US_96OEI926 : 모선,화주별 재고집계표
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  IHHANGCHA : 항차
    ///  IHHWAJU : 화주
    ///  IHSOSOK : 협회
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYUSPR001P : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYUSPR001P()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }

        private void TYUSPR001P_Load(object sender, System.EventArgs e)
        {
            this.BTN61_PRT.ProcessCheck += new TButton.CheckHandler(BTN61_PRT_ProcessCheck);

            RDB01_CHK01.Checked = true;
            RDB01_CHK02.Checked = false;

            RDB02_GUBN01.Checked = true;
            RDB02_GUBN02.Checked = false;

            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.SetStartingFocus(DTP01_SDATE);
        }
        #endregion

        #region  Description : 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sFilter = string.Empty;

            double dJGHANGCHAQTYHap = 0;

            int iJGJESTDAT = 0;
            int iJGHANGCHANM = 0;
            int iJGHANGCHAQTY = 0;
            int iJGHANGCHA = 0;


            DataTable dtPrt = UP_Set_PrtDataTable();
            DataRow rw;
            DataRow[] selectedRows;

            this.DbConnector.CommandClear();
            //곡종,화주,항차 리스트 집계표
            this.DbConnector.Attach
            (
                //"TY_P_US_96OEH925",
                "TY_P_US_A1E9V731",
                this.DTP01_SDATE.GetString().ToString(),
                this.CBH01_IHHANGCHA.GetValue().ToString(),
                this.CBH02_IHHANGCHA.GetValue().ToString(),
                this.CBH01_IHHWAJU.GetValue().ToString(),
                this.CBH01_IHSOSOK.GetValue().ToString(),
                this.RDB01_CHK01.Checked == true ? "1" : "2"
            );
            DataTable dtList = this.DbConnector.ExecuteDataTable();

            this.DbConnector.CommandClear();
            //화주별 재고량 집계
            this.DbConnector.Attach
            (
                "TY_P_US_96OEI926",
                this.DTP01_SDATE.GetString().ToString(),
                this.CBH01_IHHANGCHA.GetValue().ToString(),
                this.CBH02_IHHANGCHA.GetValue().ToString(),
                this.CBH01_IHHWAJU.GetValue().ToString(),
                this.CBH01_IHSOSOK.GetValue().ToString(),
                this.RDB01_CHK01.Checked == true ? "1" : "2"
            );
            DataTable dtData = this.DbConnector.ExecuteDataTable();

            if (dtList.Rows.Count > 0)
            {
                for (int i = 0; i < dtList.Rows.Count; i++)
                {
                    rw = dtPrt.NewRow();
                    rw["DATE"] = Set_Date(this.DTP01_SDATE.GetString().ToString());
                    rw["JGGUBUN"] = this.RDB01_CHK01.Checked == true ? "전 체" : "통 관";
                    rw["JGGOKJONG"] = dtList.Rows[i]["JGGOKJONG"].ToString();
                    rw["JGGOKJONGNM"] = dtList.Rows[i]["JGGOKJONGNM"].ToString();
                    rw["JGHWAJU"] = dtList.Rows[i]["JGHWAJU"].ToString();
                    rw["JGHWAJUNM"] = dtList.Rows[i]["JGHWAJUNM"].ToString();

                    iJGJESTDAT = 4;
                    iJGHANGCHA = 13;
                    iJGHANGCHANM = 22;
                    iJGHANGCHAQTY = 31;

                    for (int j = 4; j < dtList.Columns.Count; j++)
                    {
                        if (dtList.Rows[i][j].ToString() != "")
                        {
                            rw[iJGHANGCHA] = dtList.Rows[i][j].ToString();

                            sFilter = " JGHANGCHA = '" + dtList.Rows[i][j].ToString() + "' ";
                            sFilter = sFilter + " AND JGGOKJONG = '" + dtList.Rows[i]["JGGOKJONG"].ToString() + "' ";
                            sFilter = sFilter + " AND JGHWAJU = '" + dtList.Rows[i]["JGHWAJU"].ToString() + "' ";

                            selectedRows = dtData.Select(sFilter, "");
                            foreach (DataRow dr in selectedRows)
                            {
                                rw[iJGHANGCHAQTY] = dr.ItemArray[17].ToString(); //재고량
                                dJGHANGCHAQTYHap += Convert.ToDouble(dr.ItemArray[17].ToString());
                            }

                            sFilter = " JGHANGCHA = '" + dtList.Rows[i][j].ToString() + "' ";
                            sFilter = sFilter + " AND JGGOKJONG = '" + dtList.Rows[i]["JGGOKJONG"].ToString() + "' ";
                            selectedRows = dtData.Select(sFilter, "");
                            foreach (DataRow dr in selectedRows)
                            {
                                rw[iJGHANGCHANM] = dr.ItemArray[1].ToString(); //모선명
                                rw[iJGJESTDAT] = Set_Date(dr.ItemArray[6].ToString()); //보관일자
                            }

                        }
                        else
                        {
                            rw[iJGHANGCHA] = "";
                            rw[iJGHANGCHANM] = "";
                            rw[iJGJESTDAT] = "";
                            rw[iJGHANGCHAQTY] = "";
                        }

                        iJGJESTDAT += 1;
                        iJGHANGCHA += 1;
                        iJGHANGCHANM += 1;
                        iJGHANGCHAQTY += 1;

                    }//for (int j = 4; j < dtList.Columns.Count - 4; j++)...end

                    rw["JGHANGCHAQTYHAP"] = dJGHANGCHAQTYHap.ToString();

                    dtPrt.Rows.Add(rw);

                    dJGHANGCHAQTYHap = 0;
                } //for (int i = 0; i < dtList.Rows.Count; i++)...end               


                ActiveReport rpt = new TYUSPR001R1(RDB02_GUBN01.Checked == true ? "1" : "2");
                rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;
                (new TYERGB001P(rpt, dtPrt)).ShowDialog();
            }
            else
            {
                this.ShowCustomMessage("출력할 자료 없습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }

        }

        private void BTN61_PRT_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
           

            if (!this.ShowMessage("TY_M_GB_2BN4U622"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion      

        #region  Description : DataTable 만들기
        private DataTable UP_Set_PrtDataTable()
        {
            DataTable dt = new DataTable();

            
            dt.Columns.Add("JGGOKJONG", typeof(System.String));
            dt.Columns.Add("JGGOKJONGNM", typeof(System.String));
            dt.Columns.Add("JGHWAJU", typeof(System.String));
            dt.Columns.Add("JGHWAJUNM", typeof(System.String));

            dt.Columns.Add("JGJESTDAT1", typeof(System.String));
            dt.Columns.Add("JGJESTDAT2", typeof(System.String));
            dt.Columns.Add("JGJESTDAT3", typeof(System.String));
            dt.Columns.Add("JGJESTDAT4", typeof(System.String));
            dt.Columns.Add("JGJESTDAT5", typeof(System.String));
            dt.Columns.Add("JGJESTDAT6", typeof(System.String));
            dt.Columns.Add("JGJESTDAT7", typeof(System.String));
            dt.Columns.Add("JGJESTDAT8", typeof(System.String));
            dt.Columns.Add("JGJESTDAT9", typeof(System.String));

            dt.Columns.Add("JGHANGCHA1", typeof(System.String));
            dt.Columns.Add("JGHANGCHA2", typeof(System.String));
            dt.Columns.Add("JGHANGCHA3", typeof(System.String));
            dt.Columns.Add("JGHANGCHA4", typeof(System.String));
            dt.Columns.Add("JGHANGCHA5", typeof(System.String));
            dt.Columns.Add("JGHANGCHA6", typeof(System.String));
            dt.Columns.Add("JGHANGCHA7", typeof(System.String));
            dt.Columns.Add("JGHANGCHA8", typeof(System.String));
            dt.Columns.Add("JGHANGCHA9", typeof(System.String));

            dt.Columns.Add("JGHANGCHANM1", typeof(System.String));
            dt.Columns.Add("JGHANGCHANM2", typeof(System.String));
            dt.Columns.Add("JGHANGCHANM3", typeof(System.String));
            dt.Columns.Add("JGHANGCHANM4", typeof(System.String));
            dt.Columns.Add("JGHANGCHANM5", typeof(System.String));
            dt.Columns.Add("JGHANGCHANM6", typeof(System.String));
            dt.Columns.Add("JGHANGCHANM7", typeof(System.String));
            dt.Columns.Add("JGHANGCHANM8", typeof(System.String));
            dt.Columns.Add("JGHANGCHANM9", typeof(System.String));

            dt.Columns.Add("JGHANGCHAQTY1", typeof(System.String));
            dt.Columns.Add("JGHANGCHAQTY2", typeof(System.String));
            dt.Columns.Add("JGHANGCHAQTY3", typeof(System.String));
            dt.Columns.Add("JGHANGCHAQTY4", typeof(System.String));
            dt.Columns.Add("JGHANGCHAQTY5", typeof(System.String));
            dt.Columns.Add("JGHANGCHAQTY6", typeof(System.String));
            dt.Columns.Add("JGHANGCHAQTY7", typeof(System.String));
            dt.Columns.Add("JGHANGCHAQTY8", typeof(System.String));
            dt.Columns.Add("JGHANGCHAQTY9", typeof(System.String));
            dt.Columns.Add("JGHANGCHAQTYHAP", typeof(System.String));

            dt.Columns.Add("DATE", typeof(System.String));
            dt.Columns.Add("JGGUBUN", typeof(System.String));

            dt.TableName = "TableNames";

            return dt;
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
