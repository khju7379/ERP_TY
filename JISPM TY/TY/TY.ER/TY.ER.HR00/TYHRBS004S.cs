using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.Drawing;

namespace TY.ER.HR00
{
    /// <summary>
    /// 사업계획 인건비&4대보험 예산 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2018.05.23 09:46
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_85NBM072 : 사업계획 인사 4대보험 예산 조회
    ///  TY_P_HR_85NGG078 : 사업계획 인건비 예산 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_85NGH079 : 사업계획 인건비 예산 조회
    ///  TY_S_HR_85NGN080 : 사업계획 인사 4대보험 예산 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CREATE : 생성
    ///  INQ : 조회
    ///  KBSABUN : 사번
    ///  KBBUSEO : 부서
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRBS004S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRBS004S()
        {
            InitializeComponent();
        }

        private void TYHRBS004S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.TXT01_SDATE.SetValue(DateTime.Now.ToString("yyyy") );

            this.CBH01_KBBUSEO.DummyValue = this.TXT01_SDATE.GetValue().ToString();
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sProCessidDetailP = string.Empty;
            string sProCessidFundP = string.Empty;

            if (CBO01_INQOPTION.GetValue().ToString() == "S")
            {
                sProCessidDetailP = "TY_P_HR_85NGG078";
                sProCessidFundP = "TY_P_HR_85NBM072";
            }
            else
            {
                sProCessidDetailP = "TY_P_HR_864BN159";
                sProCessidFundP = "TY_P_HR_864BO160";
            }

           
                //상세

                //인건비
                this.FPS91_TY_S_HR_85NGH079.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(sProCessidDetailP, TXT01_SDATE.GetValue().ToString(), this.CBH01_KBSABUN.GetValue(), this.CBH01_KBBUSEO.GetValue());
                this.FPS91_TY_S_HR_85NGH079.SetValue(this.DbConnector.ExecuteDataTable());

                if (this.FPS91_TY_S_HR_85NGH079.CurrentRowCount > 0)
                {
                    this.SetSpreadSumRow(this.FPS91_TY_S_HR_85NGH079, "BSCDPACNM", "[합 계]", SumRowType.Sum);
                    this.SetSpreadSumRow(this.FPS91_TY_S_HR_85NGH079, "BSCDPACNM", "[소 계]", SumRowType.SubTotal);

                    for (int i = 0; i < this.FPS91_TY_S_HR_85NGH079.CurrentRowCount; i++)
                    {
                        //합계라인 잠금
                        if ( Convert.ToInt16(this.FPS91_TY_S_HR_85NGH079.GetValue(i, "BSCSEQ").ToString()) > 900 )
                        {
                            for (int j = 14; j < 26; j++)
                            {
                                this.FPS91_TY_S_HR_85NGH079_Sheet1.Cells[i, j].ForeColor = Color.Black;
                                this.FPS91_TY_S_HR_85NGH079_Sheet1.Cells[i, j].Locked = true;
                            }
                        }
                    }
                    
                }


                //4대보험
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_85P8Y097", TXT01_SDATE.GetValue().ToString());
                this.CurrentDataTableRowMapping(this.DbConnector.ExecuteDataTable(), "02");

                this.FPS91_TY_S_HR_85NGN080.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(sProCessidFundP, TXT01_SDATE.GetValue().ToString(), this.CBH01_KBSABUN.GetValue(), this.CBH01_KBBUSEO.GetValue());
                this.FPS91_TY_S_HR_85NGN080.SetValue(this.DbConnector.ExecuteDataTable());

                if (this.FPS91_TY_S_HR_85NGN080.CurrentRowCount > 0)
                {
                    this.SetSpreadSumRow(this.FPS91_TY_S_HR_85NGN080, "BICDPACNM", "[합 계]", SumRowType.Sum);
                    this.SetSpreadSumRow(this.FPS91_TY_S_HR_85NGN080, "BICDPACNM", "[소 계]", SumRowType.SubTotal);

                    for (int i = 0; i < this.FPS91_TY_S_HR_85NGN080.CurrentRowCount; i++)
                    {
                        //합계라인 잠금
                        if (Convert.ToInt16(this.FPS91_TY_S_HR_85NGN080.GetValue(i, "BICSEQ").ToString()) > 900)
                        {
                            for (int j = 15; j < 27; j++)
                            {
                                this.FPS91_TY_S_HR_85NGN080_Sheet1.Cells[i, j].ForeColor = Color.Black;
                                this.FPS91_TY_S_HR_85NGN080_Sheet1.Cells[i, j].Locked = true;
                            }
                        }
                    }
                }
            

        }
        #endregion

        #region  Description : 사업계획 예산 자료 전송 팝업 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            if ((new TYHRBS004B()).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 사업계획 예산 생성 팝업 버튼 이벤트
        private void BTN61_CREATE_Click(object sender, EventArgs e)
        {
            if ((new TYHRBS003B()).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : TXT01_SDATE_TextChanged 이벤트
        private void TXT01_SDATE_TextChanged(object sender, EventArgs e)
        {
            this.CBH01_KBBUSEO.DummyValue = this.TXT01_SDATE.GetValue().ToString();
        }
        #endregion

        #region Description : 저장 ProcessCheck
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            double dBSCMONAMTTotal = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            //인건비
            if (ds.Tables[0].Rows.Count > 0)
            {
                
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dBSCMONAMTTotal = Convert.ToDouble(ds.Tables[0].Rows[i]["BSCMONAMT01"].ToString()) +
                                      Convert.ToDouble(ds.Tables[0].Rows[i]["BSCMONAMT02"].ToString()) +
                                      Convert.ToDouble(ds.Tables[0].Rows[i]["BSCMONAMT03"].ToString()) +
                                      Convert.ToDouble(ds.Tables[0].Rows[i]["BSCMONAMT04"].ToString()) +
                                      Convert.ToDouble(ds.Tables[0].Rows[i]["BSCMONAMT05"].ToString()) +
                                      Convert.ToDouble(ds.Tables[0].Rows[i]["BSCMONAMT06"].ToString()) +
                                      Convert.ToDouble(ds.Tables[0].Rows[i]["BSCMONAMT07"].ToString()) +
                                      Convert.ToDouble(ds.Tables[0].Rows[i]["BSCMONAMT08"].ToString()) +
                                      Convert.ToDouble(ds.Tables[0].Rows[i]["BSCMONAMT09"].ToString()) +
                                      Convert.ToDouble(ds.Tables[0].Rows[i]["BSCMONAMT10"].ToString()) +
                                      Convert.ToDouble(ds.Tables[0].Rows[i]["BSCMONAMT11"].ToString()) +
                                      Convert.ToDouble(ds.Tables[0].Rows[i]["BSCMONAMT12"].ToString()); 

                    this.DbConnector.Attach("TY_P_HR_8BEB4126", ds.Tables[0].Rows[i]["BSCMONAMT01"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSCMONAMT02"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSCMONAMT03"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSCMONAMT04"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSCMONAMT05"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSCMONAMT06"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSCMONAMT07"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSCMONAMT08"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSCMONAMT09"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSCMONAMT10"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSCMONAMT11"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSCMONAMT12"].ToString(),
                                                                dBSCMONAMTTotal.ToString(),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[0].Rows[i]["BSCYEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSCSEQ"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSCSABUNDP"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSCDPMK"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSCDPAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSCADAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["BSCCDAC"].ToString()
                                                                 );
                }
            }

            //4대보험
            if (ds.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    dBSCMONAMTTotal = Convert.ToDouble(ds.Tables[1].Rows[i]["BICMONAMT01"].ToString()) +
                                      Convert.ToDouble(ds.Tables[1].Rows[i]["BICMONAMT02"].ToString()) +
                                      Convert.ToDouble(ds.Tables[1].Rows[i]["BICMONAMT03"].ToString()) +
                                      Convert.ToDouble(ds.Tables[1].Rows[i]["BICMONAMT04"].ToString()) +
                                      Convert.ToDouble(ds.Tables[1].Rows[i]["BICMONAMT05"].ToString()) +
                                      Convert.ToDouble(ds.Tables[1].Rows[i]["BICMONAMT06"].ToString()) +
                                      Convert.ToDouble(ds.Tables[1].Rows[i]["BICMONAMT07"].ToString()) +
                                      Convert.ToDouble(ds.Tables[1].Rows[i]["BICMONAMT08"].ToString()) +
                                      Convert.ToDouble(ds.Tables[1].Rows[i]["BICMONAMT09"].ToString()) +
                                      Convert.ToDouble(ds.Tables[1].Rows[i]["BICMONAMT10"].ToString()) +
                                      Convert.ToDouble(ds.Tables[1].Rows[i]["BICMONAMT11"].ToString()) +
                                      Convert.ToDouble(ds.Tables[1].Rows[i]["BICMONAMT12"].ToString());

                    this.DbConnector.Attach("TY_P_HR_8BEBJ127", ds.Tables[1].Rows[i]["BICMONAMT01"].ToString(),
                                                                ds.Tables[1].Rows[i]["BICMONAMT02"].ToString(),
                                                                ds.Tables[1].Rows[i]["BICMONAMT03"].ToString(),
                                                                ds.Tables[1].Rows[i]["BICMONAMT04"].ToString(),
                                                                ds.Tables[1].Rows[i]["BICMONAMT05"].ToString(),
                                                                ds.Tables[1].Rows[i]["BICMONAMT06"].ToString(),
                                                                ds.Tables[1].Rows[i]["BICMONAMT07"].ToString(),
                                                                ds.Tables[1].Rows[i]["BICMONAMT08"].ToString(),
                                                                ds.Tables[1].Rows[i]["BICMONAMT09"].ToString(),
                                                                ds.Tables[1].Rows[i]["BICMONAMT10"].ToString(),
                                                                ds.Tables[1].Rows[i]["BICMONAMT11"].ToString(),
                                                                ds.Tables[1].Rows[i]["BICMONAMT12"].ToString(),
                                                                dBSCMONAMTTotal.ToString(),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["BICYEAR"].ToString(),
                                                                ds.Tables[1].Rows[i]["BICSEQ"].ToString(),
                                                                ds.Tables[1].Rows[i]["BICSABUNDP"].ToString(),
                                                                ds.Tables[1].Rows[i]["BICDPMK"].ToString(),
                                                                ds.Tables[1].Rows[i]["BICDPAC"].ToString(),
                                                                ds.Tables[1].Rows[i]["BICADAC"].ToString(),
                                                                ds.Tables[1].Rows[i]["BICCDAC"].ToString(),
                                                                ds.Tables[1].Rows[i]["BICGUBN"].ToString()
                                                                 );
                }
            }

            if (this.DbConnector.CommandCount > 0)
            {
                this.DbConnector.ExecuteTranQueryList();
            }

            this.ShowMessage("TY_M_GB_23NAD873");

            this.BTN61_INQ_Click(null, null);

        }
        
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();            

            // 인건비
            ds.Tables.Add(this.FPS91_TY_S_HR_85NGH079.GetDataSourceInclude(TSpread.TActionType.Update, "BSCYEAR", "BSCSEQ", "BSCSABUN", "BSCSABUNDP", "BSCDPMK", "BSCDPAC", "BSCADAC", "BSCCDAC", 
                                                                                                       "BSCMONAMT01", "BSCMONAMT02","BSCMONAMT03","BSCMONAMT04","BSCMONAMT05","BSCMONAMT06","BSCMONAMT07","BSCMONAMT08","BSCMONAMT09","BSCMONAMT10","BSCMONAMT11","BSCMONAMT12"));
            //4대보험
            ds.Tables.Add(this.FPS91_TY_S_HR_85NGN080.GetDataSourceInclude(TSpread.TActionType.Update, "BICYEAR", "BICSEQ", "BICSABUN", "BICSABUNDP", "BICDPMK",  "BICDPAC", "BICADAC", "BICCDAC", "BICGUBN", "BICMONTHAMT", 
                                                                                                       "BICMONAMT01", "BICMONAMT02", "BICMONAMT03", "BICMONAMT04", "BICMONAMT05", "BICMONAMT06", "BICMONAMT07", "BICMONAMT08", "BICMONAMT09", "BICMONAMT10", "BICMONAMT11", "BICMONAMT12"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            if( ds.Tables[0].Rows.Count > 0 )
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (Convert.ToInt16(ds.Tables[0].Rows[i]["BSCSEQ"].ToString()) > 900)
                    {
                        //합계라인 수정 불가
                        this.ShowMessage("TY_M_GB_85TFH132");
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    if (Convert.ToInt16(ds.Tables[1].Rows[i]["BICSEQ"].ToString()) > 900)
                    {
                        //합계라인 수정 불가
                        this.ShowMessage("TY_M_GB_85TFH132");
                        e.Successed = false;
                        return;
                    }
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
    }
}
