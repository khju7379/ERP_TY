using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.BS00
{
    /// <summary>
    /// 프로젝트이자 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.08.16 10:18
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_78GAN437 : 프로젝트이자 관리 등록
    ///  TY_P_AC_78GAO438 : 프로젝트이자 관리 수정
    ///  TY_P_AC_78GAQ439 : 프로젝트이자 관리 삭제
    ///  TY_P_AC_78GBJ440 : 프로젝트이자 관리 조회
    ///  TY_P_AC_78GBJ441 : 프로젝트이자 관리 확인
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_78GBJ442 : 프로젝트이자 관리
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  SAV : 저장
    ///  VSYEAR : 기준년도
    /// </summary>
    public partial class TYBSCR003I : TYBase
    {
        #region Description : 폼 로드
        public TYBSCR003I()
        {
            InitializeComponent();
        }
        
        private void TYBSCR003I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_78GBJ442, "BJYEAR", "BJDPAC");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_INQ.ProcessCheck += new TButton.CheckHandler(BTN61_INQ_ProcessCheck);
            this.TXT01_VSYEAR.Text = System.DateTime.Now.ToString("yyyy");

            if (Convert.ToInt16(TXT01_VSYEAR.GetValue().ToString()) > 2018 && Convert.ToInt16(TXT01_VSYEAR.GetValue().ToString()) <= 2020 )
            {
                this.BTN61_SAV.Visible = false;
            }
            else
            {
                this.BTN61_SAV.Visible = true;
            }           


            BTN61_INQ_Click(null, null);

            SetStartingFocus(this.TXT01_VSYEAR);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            try
            {
                this.FPS91_TY_S_AC_78GBJ442.Initialize();
                this.FPS91_TY_S_AC_7BEHA001.Initialize();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_78GBJ440", this.TXT01_VSYEAR.GetValue().ToString());
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    //this.FPS91_TY_S_AC_78GBJ442.SetValue(UP_ChangeDt(dt));
                    this.FPS91_TY_S_AC_78GBJ442.SetValue(dt);

                    for (int i = 0; i < this.FPS91_TY_S_AC_78GBJ442.ActiveSheet.RowCount; i++)
                    {
                        if (this.FPS91_TY_S_AC_78GBJ442.GetValue(i, "BJDPAC").ToString() == "")
                        {
                            // 합계 ROW 잠그기
                            this.FPS91_TY_S_AC_78GBJ442.ActiveSheet.Rows[i].Locked = true;
                        }
                    }
                }
                else
                {
                    //this.FPS91_TY_S_AC_78GBJ442.SetValue(Set_EmptyDt());
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_7BEH5999", this.TXT01_VSYEAR.GetValue().ToString());
                DataTable dts = this.DbConnector.ExecuteDataTable();
                if (dts.Rows.Count > 0)
                {
                    this.FPS91_TY_S_AC_7BEHA001.SetValue(dts);

                    this.SpreadSumRowAdd(this.FPS91_TY_S_AC_7BEHA001, "CDDESC1", "합  계", SumRowType.Sum, "BJSMPJINTAMT01","BJSMPJINTAMT02","BJSMPJINTAMT03","BJSMPJINTAMT04","BJSMPJINTAMT05","BJSMPJINTAMT06","BJSMPJINTAMT07","BJSMPJINTAMT08","BJSMPJINTAMT09","BJSMPJINTAMT10","BJSMPJINTAMT11","BJSMPJINTAMT12","BJSMPJINTAMTTOTAL");
                }

                UP_Spread_Load();
            }
            catch
            {

            }
        }

        private void BTN61_INQ_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (Convert.ToInt16(this.TXT01_VSYEAR.GetValue().ToString()) > 2018 && Convert.ToInt16(this.TXT01_VSYEAR.GetValue().ToString()) <= 2020 )
            {
                this.ShowCustomMessage("2019년 ~ 2020년까지 자료는 작업이 불가합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            DataTable dt = new DataTable();

            try
            {
                double[] dBNFUNDTOTAL = new double[ds.Tables[0].Rows.Count];

                //dBNFUNDTOTAL = UP_GetBNFUNDTOTAL(ds.Tables[0]);

                //신규등록
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_78GBJ441", ds.Tables[0].Rows[i]["BJYEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["BJDPAC"].ToString());
                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.DbConnector.CommandClear();

                        this.DbConnector.Attach("TY_P_AC_78GAO438", ds.Tables[0].Rows[i]["BJCAPAMT"].ToString(),
                                                                    ds.Tables[0].Rows[i]["BJRATE"].ToString(),
                                                                    ds.Tables[0].Rows[i]["BJINVESTAMT"].ToString(),
                                                                    ds.Tables[0].Rows[i]["BJYPJINTAMT"].ToString(),
                                                                    ds.Tables[0].Rows[i]["BJMPJINTAMT"].ToString(),
                                                                    TYUserInfo.EmpNo,
                                                                    ds.Tables[0].Rows[i]["BJYEAR"].ToString(),
                                                                    ds.Tables[0].Rows[i]["BJDPAC"].ToString()
                                                                    );
                        this.DbConnector.ExecuteTranQuery();
                    }
                    else
                    {
                        this.DbConnector.CommandClear();

                        this.DbConnector.Attach("TY_P_AC_78GAN437", ds.Tables[0].Rows[i]["BJYEAR"].ToString(),
                                                                    ds.Tables[0].Rows[i]["BJDPAC"].ToString(),
                                                                    ds.Tables[0].Rows[i]["BJCAPAMT"].ToString(),
                                                                    ds.Tables[0].Rows[i]["BJRATE"].ToString(),
                                                                    ds.Tables[0].Rows[i]["BJINVESTAMT"].ToString(),
                                                                    ds.Tables[0].Rows[i]["BJYPJINTAMT"].ToString(),
                                                                    ds.Tables[0].Rows[i]["BJMPJINTAMT"].ToString(),
                                                                    TYUserInfo.EmpNo
                                                                    );
                        this.DbConnector.ExecuteTranQuery();
                    }
                }


                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_7BEHH003", ds.Tables[1].Rows[i]["BJYEAR"].ToString(),
                                                                ds.Tables[1].Rows[i]["BJDPAC"].ToString());
                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.DbConnector.CommandClear();

                        this.DbConnector.Attach("TY_P_AC_7BEHJ007", ds.Tables[1].Rows[i]["BJSMPJINTAMT01"].ToString(),
                                                                    ds.Tables[1].Rows[i]["BJSMPJINTAMT02"].ToString(),
                                                                    ds.Tables[1].Rows[i]["BJSMPJINTAMT03"].ToString(),
                                                                    ds.Tables[1].Rows[i]["BJSMPJINTAMT04"].ToString(),
                                                                    ds.Tables[1].Rows[i]["BJSMPJINTAMT05"].ToString(),
                                                                    ds.Tables[1].Rows[i]["BJSMPJINTAMT06"].ToString(),
                                                                    ds.Tables[1].Rows[i]["BJSMPJINTAMT07"].ToString(),
                                                                    ds.Tables[1].Rows[i]["BJSMPJINTAMT08"].ToString(),
                                                                    ds.Tables[1].Rows[i]["BJSMPJINTAMT09"].ToString(),
                                                                    ds.Tables[1].Rows[i]["BJSMPJINTAMT10"].ToString(),
                                                                    ds.Tables[1].Rows[i]["BJSMPJINTAMT11"].ToString(),
                                                                    ds.Tables[1].Rows[i]["BJSMPJINTAMT12"].ToString(),
                                                                    TYUserInfo.EmpNo,
                                                                    ds.Tables[1].Rows[i]["BJYEAR"].ToString(),
                                                                    ds.Tables[1].Rows[i]["BJDPAC"].ToString()
                                                                    );
                        this.DbConnector.ExecuteTranQuery();
                    }
                    else
                    {
                        this.DbConnector.CommandClear();

                        this.DbConnector.Attach("TY_P_AC_7BEHI004", ds.Tables[1].Rows[i]["BJYEAR"].ToString(),
                                                                    ds.Tables[1].Rows[i]["BJDPAC"].ToString(),
                                                                    ds.Tables[1].Rows[i]["BJSMPJINTAMT01"].ToString(),
                                                                    ds.Tables[1].Rows[i]["BJSMPJINTAMT02"].ToString(),
                                                                    ds.Tables[1].Rows[i]["BJSMPJINTAMT03"].ToString(),
                                                                    ds.Tables[1].Rows[i]["BJSMPJINTAMT04"].ToString(),
                                                                    ds.Tables[1].Rows[i]["BJSMPJINTAMT05"].ToString(),
                                                                    ds.Tables[1].Rows[i]["BJSMPJINTAMT06"].ToString(),
                                                                    ds.Tables[1].Rows[i]["BJSMPJINTAMT07"].ToString(),
                                                                    ds.Tables[1].Rows[i]["BJSMPJINTAMT08"].ToString(),
                                                                    ds.Tables[1].Rows[i]["BJSMPJINTAMT09"].ToString(),
                                                                    ds.Tables[1].Rows[i]["BJSMPJINTAMT10"].ToString(),
                                                                    ds.Tables[1].Rows[i]["BJSMPJINTAMT11"].ToString(),
                                                                    ds.Tables[1].Rows[i]["BJSMPJINTAMT12"].ToString(),
                                                                    TYUserInfo.EmpNo
                                                                    );
                        this.DbConnector.ExecuteTranQuery();
                    }
                }

                BTN61_INQ_Click(null, null);

                this.ShowMessage("TY_M_GB_23NAD873");
            }
            catch
            {
                this.ShowMessage("TY_M_AC_246A2488");
            }
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_78GBJ442.GetDataSourceInclude(TSpread.TActionType.Update, "BJYEAR", "BJDPAC", "BJCAPAMT", "BJRATE", "BJINVESTAMT", "BJYPJINTAMT", "BJMPJINTAMT"));
            ds.Tables.Add(this.FPS91_TY_S_AC_7BEHA001.GetDataSourceInclude(TSpread.TActionType.Update, "BJYEAR", "BJDPAC", "BJSMPJINTAMT01","BJSMPJINTAMT02", "BJSMPJINTAMT03","BJSMPJINTAMT04","BJSMPJINTAMT05","BJSMPJINTAMT06","BJSMPJINTAMT07","BJSMPJINTAMT08","BJSMPJINTAMT09","BJSMPJINTAMT10","BJSMPJINTAMT11","BJSMPJINTAMT12"));

            // 저장 체크
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

            }

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
            e.ArgData = ds;
        }
        #endregion

        #region Description : 스프레드 타이틀 변경
        private void UP_Spread_Load()
        {
            this.FPS91_TY_S_AC_78GBJ442_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_78GBJ442_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_78GBJ442_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_AC_78GBJ442_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_AC_78GBJ442_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);
            this.FPS91_TY_S_AC_78GBJ442_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);
            this.FPS91_TY_S_AC_78GBJ442_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1);
            this.FPS91_TY_S_AC_78GBJ442_Sheet1.AddColumnHeaderSpanCell(0, 5, 2, 1);
            this.FPS91_TY_S_AC_78GBJ442_Sheet1.AddColumnHeaderSpanCell(0, 6, 1, 2);
            
            this.FPS91_TY_S_AC_78GBJ442_Sheet1.ColumnHeader.Cells[0, 0].Value = "년도";
            this.FPS91_TY_S_AC_78GBJ442_Sheet1.ColumnHeader.Cells[0, 1].Value = "귀속코드";
            this.FPS91_TY_S_AC_78GBJ442_Sheet1.ColumnHeader.Cells[0, 2].Value = "귀속";
            this.FPS91_TY_S_AC_78GBJ442_Sheet1.ColumnHeader.Cells[0, 3].Value = "자본금";
            this.FPS91_TY_S_AC_78GBJ442_Sheet1.ColumnHeader.Cells[0, 4].Value = "지분율";
            this.FPS91_TY_S_AC_78GBJ442_Sheet1.ColumnHeader.Cells[0, 5].Value = "실투자액";
            this.FPS91_TY_S_AC_78GBJ442_Sheet1.ColumnHeader.Cells[0, 6].Value = "PJ 이자";
            
            this.FPS91_TY_S_AC_78GBJ442_Sheet1.ColumnHeader.Cells[1, 6].Value = "년 반영액";
            this.FPS91_TY_S_AC_78GBJ442_Sheet1.ColumnHeader.Cells[1, 7].Value = "월 반영액";

            this.FPS91_TY_S_AC_78GBJ442_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion

        #region Description : TXT01_VSYEAR_TextChanged 이벤트
        private void TXT01_VSYEAR_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt16(TXT01_VSYEAR.GetValue().ToString()) > 2018 && Convert.ToInt16(TXT01_VSYEAR.GetValue().ToString()) <= 2020 )
            {
                this.BTN61_SAV.Visible = false;
            }
            else
            {
                this.BTN61_SAV.Visible = true;
            }
        }
        #endregion
    }
}
