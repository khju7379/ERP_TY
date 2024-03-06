using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 자금실적 수입/지출 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.10.08 18:26
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2A872472 : 자금실적 수입지출(수입) 조회
    ///  TY_P_AC_2A879473 : 자금실적 수입지출(지출) 조회
    ///  TY_P_AC_2A87E474 : 자금실적 수입지출(부서) 조회
    ///  TY_P_AC_2AA8T658 : 자금실적 수입지출(건별) 조회
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2A87Q477 : 자금실적 수입지출(집계) 조회
    ///  TY_S_AC_2A98E603 : 자금실적 수입지출(건별) 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  GSEMOK : 계정세목
    ///  GCDDP : 사업장코드
    ///  INQOPTION : 조회구분
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    /// </summary>
    public partial class TYACKF009S : TYBase
    {
        #region Description : 폼로드 이벤트
        public TYACKF009S()
        {
            InitializeComponent();
        }

        private void TYACKF009S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_GSTDATE.SetValue(DateTime.Now.ToString("yyyy-mm-DD"));
            this.DTP01_GEDDATE.SetValue(DateTime.Now.ToString("yyyy-mm-DD"));
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {

            if (CBO01_INQOPTION.GetValue().ToString() == "1") //집계
            {
                this.FPS91_TY_S_AC_2A87Q477.Visible = true;
                this.FPS91_TY_S_AC_2A98E603.Visible = false;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2A872472", this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString(),CBO01_GCDDP.GetValue());
                DataSet dsIndex_In = this.DbConnector.ExecuteDataSet();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2A879473", this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString(), CBO01_GCDDP.GetValue());
                DataSet dsIndex_Out = this.DbConnector.ExecuteDataSet();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2A87E474", this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString(), CBO01_GCDDP.GetValue());
                DataSet dsMrge = this.DbConnector.ExecuteDataSet();

                DataSet dsgrd = UP_ComputeDs(dsIndex_In, dsIndex_Out, dsMrge);

                this.FPS91_TY_S_AC_2A87Q477.SetValue(dsgrd);

                if (this.FPS91_TY_S_AC_2A87Q477.CurrentRowCount > 0)
                {
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_2A87Q477, "FUNDTABLECDACIN", "소 계", SumRowType.SubTotal);
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_2A87Q477, "FUNDTABLECDACIN", "총 계", SumRowType.Total);
                }
            }
            else
            {
                this.FPS91_TY_S_AC_2A87Q477.Visible = false;
                this.FPS91_TY_S_AC_2A98E603.Visible = true;

                DataSet dsgrd = new DataSet(); 

                if (CBO01_INQOPTION.GetValue().ToString() == "2")  //건별현황-수입(원화)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_2AA8T658", this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString(), "WON", CBH01_GSEMOK.GetValue(), CBO01_GCDDP.GetValue(), "1");
                    DataSet dsIndex_In = this.DbConnector.ExecuteDataSet();

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_2A87E474", this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString(), CBO01_GCDDP.GetValue());
                    DataSet dsMrge = this.DbConnector.ExecuteDataSet();

                    dsgrd = UP_ComputeDsDetailInWon(dsIndex_In, dsMrge);
                }
                else if (CBO01_INQOPTION.GetValue().ToString() == "3")  //건별현황-지출(외화)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_2AA8T658", this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString(), "USD", CBH01_GSEMOK.GetValue(), CBO01_GCDDP.GetValue(), "1");
                    DataSet dsIndex_In = this.DbConnector.ExecuteDataSet();

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_2A87E474", this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString(), CBO01_GCDDP.GetValue());
                    DataSet dsMrge = this.DbConnector.ExecuteDataSet();

                    dsgrd = UP_ComputeDsDetailInWon(dsIndex_In, dsMrge);
                }
                else if (CBO01_INQOPTION.GetValue().ToString() == "4")  //건별현황-지출(원화)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_2AA8T658", this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString(), "WON", CBH01_GSEMOK.GetValue(), CBO01_GCDDP.GetValue(), "2");
                    DataSet dsIndex_In = this.DbConnector.ExecuteDataSet();

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_2A87E474", this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString(), CBO01_GCDDP.GetValue());
                    DataSet dsMrge = this.DbConnector.ExecuteDataSet();

                    dsgrd = UP_ComputeDsDetailInWon(dsIndex_In, dsMrge);
                }
                else  //건별현황-지출(외화)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_2AA8T658", this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString(), "USD", CBH01_GSEMOK.GetValue(), CBO01_GCDDP.GetValue(), "2");
                    DataSet dsIndex_In = this.DbConnector.ExecuteDataSet();

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_2A87E474", this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString(), CBO01_GCDDP.GetValue());
                    DataSet dsMrge = this.DbConnector.ExecuteDataSet();

                    dsgrd = UP_ComputeDsDetailInWon(dsIndex_In, dsMrge);
                }


                this.FPS91_TY_S_AC_2A98E603.SetValue(dsgrd);


                if (this.FPS91_TY_S_AC_2A98E603.CurrentRowCount > 0)
                {
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_2A98E603, "FUNDTABLECDAC", "소 계", SumRowType.SubTotal);
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_2A98E603, "FUNDTABLECDAC", "총 계", SumRowType.Total);
                }

            }          

        }
        #endregion

        #region Description : 집계 수입/지출 내역
        public DataSet UP_ComputeDs(DataSet dsindex_In, DataSet dsindex_Out, DataSet dsMrge)
        {
            DataSet dsParam = new DataSet();

            string sFilter = "";
            string sSort = "";

            try
            {        

                //수입
                DataTable FundDRTable = new DataTable();

                //지출
                DataTable FundCRTable = new DataTable();

                FundDRTable = dsindex_In.Tables[0];
                FundCRTable = dsindex_Out.Tables[0];

                /*---------------- 계정과별 자금 집계 현황-------------------------- */
                //계정별/사업부별/일자별 집계-수입
                DataTable FundDRHAPTable = new DataTable();
                DataRow FundDRHAPRow;

                FundDRHAPTable.Columns.Add("FundDRHAPTableDPAC", typeof(System.String));
                FundDRHAPTable.Columns.Add("FundDRHAPTableCDACIN", typeof(System.String));
                FundDRHAPTable.Columns.Add("FundDRHAPTableCDACUP", typeof(System.String));
                FundDRHAPTable.Columns.Add("FundDRHAPTableFUND_Code", typeof(System.String));
                FundDRHAPTable.Columns.Add("FundDRHAPTableFUND_Name", typeof(System.String));
                FundDRHAPTable.Columns.Add("FundDRHAPTableRKACIN", typeof(System.String));
                FundDRHAPTable.Columns.Add("FundDRHAPTableINAMT", typeof(System.Double));

                if (CBO01_INQOPTION.GetValue().ToString() == "1")
                {
                    for (int i = 0; i < FundDRTable.Rows.Count; i++)
                    {
                        double dB2INAMT = 0;


                        bool bCheck = UP_DataTableSearch(FundDRHAPTable,
                            FundDRTable.Rows[i]["FundDRTableDPAC"].ToString(),
                            FundDRTable.Rows[i]["FundDRTableCDACIN"].ToString(),
                            FundDRTable.Rows[i]["FundDRTableCDACUP"].ToString(),
                            FundDRTable.Rows[i]["FundDRTableFUND_Code"].ToString());
                        if (bCheck == true)
                        {
                            if (FundDRTable.Rows[i]["FundDRTableDPAC"].ToString().Substring(0, 1) == "B")
                            {
                                sFilter = "";
                                sFilter = " SUBSTRING(FundDRTableDPAC,1,4)  = '" + FundDRTable.Rows[i]["FundDRTableDPAC"].ToString().Substring(0, 4) + "' AND ";
                                sFilter = sFilter + " FundDRTableCDACIN = '" + FundDRTable.Rows[i]["FundDRTableCDACIN"].ToString() + "' AND";
                                sFilter = sFilter + " FundDRTableCDACUP = '" + FundDRTable.Rows[i]["FundDRTableCDACUP"].ToString() + "' AND";
                                sFilter = sFilter + " FundDRTableFUND_Code = '" + FundDRTable.Rows[i]["FundDRTableFUND_Code"].ToString() + "'";
                                dB2INAMT = Convert.ToDouble(Get_Numeric(FundDRTable.Compute("SUM(FundDRTableINAMT)", sFilter).ToString()));

                                FundDRHAPRow = FundDRHAPTable.NewRow();
                                FundDRHAPRow["FundDRHAPTableDPAC"] = FundDRTable.Rows[i]["FundDRTableDPAC"].ToString().Substring(0, 4);
                                FundDRHAPRow["FundDRHAPTableCDACIN"] = FundDRTable.Rows[i]["FundDRTableCDACIN"].ToString();
                                FundDRHAPRow["FundDRHAPTableCDACUP"] = FundDRTable.Rows[i]["FundDRTableCDACUP"].ToString();
                                FundDRHAPRow["FundDRHAPTableFUND_Code"] = FundDRTable.Rows[i]["FundDRTableFUND_Code"].ToString();
                                FundDRHAPRow["FundDRHAPTableFUND_Name"] = FundDRTable.Rows[i]["FundDRTableFUND_Name"].ToString();
                                FundDRHAPRow["FundDRHAPTableRKACIN"] = FundDRTable.Rows[i]["FundDRTableRKACIN"].ToString();
                                FundDRHAPRow["FundDRHAPTableINAMT"] = dB2INAMT;
                                FundDRHAPTable.Rows.Add(FundDRHAPRow);

                            }
                            else
                            {
                                sFilter = "";
                                sFilter = " SUBSTRING(FundDRTableDPAC,1,1)  = '" + FundDRTable.Rows[i]["FundDRTableDPAC"].ToString().Substring(0, 1) + "' AND ";
                                sFilter = sFilter + " FundDRTableCDACIN = '" + FundDRTable.Rows[i]["FundDRTableCDACIN"].ToString() + "' AND";
                                sFilter = sFilter + " FundDRTableCDACUP = '" + FundDRTable.Rows[i]["FundDRTableCDACUP"].ToString() + "' AND";
                                sFilter = sFilter + " FundDRTableFUND_Code = '" + FundDRTable.Rows[i]["FundDRTableFUND_Code"].ToString() + "'";
                                dB2INAMT = Convert.ToDouble(Get_Numeric(FundDRTable.Compute("SUM(FundDRTableINAMT)", sFilter).ToString()));

                                FundDRHAPRow = FundDRHAPTable.NewRow();
                                FundDRHAPRow["FundDRHAPTableDPAC"] = FundDRTable.Rows[i]["FundDRTableDPAC"].ToString().Substring(0, 1);
                                FundDRHAPRow["FundDRHAPTableCDACIN"] = FundDRTable.Rows[i]["FundDRTableCDACIN"].ToString();
                                FundDRHAPRow["FundDRHAPTableCDACUP"] = FundDRTable.Rows[i]["FundDRTableCDACUP"].ToString();
                                FundDRHAPRow["FundDRHAPTableFUND_Code"] = FundDRTable.Rows[i]["FundDRTableFUND_Code"].ToString();
                                FundDRHAPRow["FundDRHAPTableFUND_Name"] = FundDRTable.Rows[i]["FundDRTableFUND_Name"].ToString();
                                FundDRHAPRow["FundDRHAPTableRKACIN"] = FundDRTable.Rows[i]["FundDRTableRKACIN"].ToString();
                                FundDRHAPRow["FundDRHAPTableINAMT"] = dB2INAMT;
                                FundDRHAPTable.Rows.Add(FundDRHAPRow);

                            }

                        }
                    }
                }

                //계정별/사업부별/일자별 집계-지출
                DataTable FundCRHAPTable = new DataTable();
                DataRow FundCRHAPRow;

                FundCRHAPTable.Columns.Add("FundCRHAPTableDPAC", typeof(System.String));
                FundCRHAPTable.Columns.Add("FundCRHAPTableCDACOUT", typeof(System.String));
                FundCRHAPTable.Columns.Add("FundCRHAPTableCDACUP", typeof(System.String));
                FundCRHAPTable.Columns.Add("FundCRHAPTableFUND_Code", typeof(System.String));
                FundCRHAPTable.Columns.Add("FundCRHAPTableFUND_Name", typeof(System.String));
                FundCRHAPTable.Columns.Add("FundCRHAPTableRKACOUT", typeof(System.String));
                FundCRHAPTable.Columns.Add("FundCRHAPTableOUTAMT", typeof(System.Double));

                if ( CBO01_INQOPTION.GetValue().ToString() == "1")
                {
                    for (int i = 0; i < FundCRTable.Rows.Count; i++)
                    {
                        double dB2OUTAMT = 0;

                        bool bCheck = UP_DataTableSearch(FundCRHAPTable,
                            FundCRTable.Rows[i]["FundCRTableDPAC"].ToString(),
                            FundCRTable.Rows[i]["FundCRTableCDACOUT"].ToString(),
                            FundCRTable.Rows[i]["FundCRTableCDACUP"].ToString(),
                            FundCRTable.Rows[i]["FundCRTableFUND_Code"].ToString());
                        if (bCheck == true)
                        {
                            if (FundCRTable.Rows[i]["FundCRTableDPAC"].ToString().Substring(0, 1) == "B")
                            {
                                sFilter = "";
                                sFilter = " SUBSTRING(FundCRTableDPAC,1,4) = '" + FundCRTable.Rows[i]["FundCRTableDPAC"].ToString().Substring(0, 4) + "' AND ";
                                sFilter = sFilter + " FundCRTableCDACOUT = '" + FundCRTable.Rows[i]["FundCRTableCDACOUT"].ToString() + "' AND";
                                sFilter = sFilter + " FundCRTableCDACUP = '" + FundCRTable.Rows[i]["FundCRTableCDACUP"].ToString() + "' AND";
                                sFilter = sFilter + " FundCRTableFUND_Code = '" + FundCRTable.Rows[i]["FundCRTableFUND_Code"].ToString() + "'";
                                dB2OUTAMT = Convert.ToDouble(Get_Numeric(FundCRTable.Compute("SUM(FundCRTableOUTAMT)", sFilter).ToString()));

                                FundCRHAPRow = FundCRHAPTable.NewRow();
                                FundCRHAPRow["FundCRHAPTableDPAC"] = FundCRTable.Rows[i]["FundCRTableDPAC"].ToString().Substring(0, 4);
                                FundCRHAPRow["FundCRHAPTableCDACOUT"] = FundCRTable.Rows[i]["FundCRTableCDACOUT"].ToString();
                                FundCRHAPRow["FundCRHAPTableCDACUP"] = FundCRTable.Rows[i]["FundCRTableCDACUP"].ToString();
                                FundCRHAPRow["FundCRHAPTableFUND_Code"] = FundCRTable.Rows[i]["FundCRTableFUND_Code"].ToString();
                                FundCRHAPRow["FundCRHAPTableFUND_Name"] = FundCRTable.Rows[i]["FundCRTableFUND_Name"].ToString();
                                FundCRHAPRow["FundCRHAPTableRKACOUT"] = FundCRTable.Rows[i]["FundCRTableRKACOUT"].ToString();
                                FundCRHAPRow["FundCRHAPTableOUTAMT"] = dB2OUTAMT;
                                FundCRHAPTable.Rows.Add(FundCRHAPRow);

                            }
                            else
                            {
                                sFilter = "";
                                sFilter = " SUBSTRING(FundCRTableDPAC,1,1) = '" + FundCRTable.Rows[i]["FundCRTableDPAC"].ToString().Substring(0, 1) + "' AND ";
                                sFilter = sFilter + " FundCRTableCDACOUT = '" + FundCRTable.Rows[i]["FundCRTableCDACOUT"].ToString() + "' AND";
                                sFilter = sFilter + " FundCRTableCDACUP = '" + FundCRTable.Rows[i]["FundCRTableCDACUP"].ToString() + "' AND";
                                sFilter = sFilter + " FundCRTableFUND_Code = '" + FundCRTable.Rows[i]["FundCRTableFUND_Code"].ToString() + "'";
                                dB2OUTAMT = Convert.ToDouble(Get_Numeric(FundCRTable.Compute("SUM(FundCRTableOUTAMT)", sFilter).ToString()));

                                FundCRHAPRow = FundCRHAPTable.NewRow();
                                FundCRHAPRow["FundCRHAPTableDPAC"] = FundCRTable.Rows[i]["FundCRTableDPAC"].ToString().Substring(0, 1);
                                FundCRHAPRow["FundCRHAPTableCDACOUT"] = FundCRTable.Rows[i]["FundCRTableCDACOUT"].ToString();
                                FundCRHAPRow["FundCRHAPTableCDACUP"] = FundCRTable.Rows[i]["FundCRTableCDACUP"].ToString();
                                FundCRHAPRow["FundCRHAPTableFUND_Code"] = FundCRTable.Rows[i]["FundCRTableFUND_Code"].ToString();
                                FundCRHAPRow["FundCRHAPTableFUND_Name"] = FundCRTable.Rows[i]["FundCRTableFUND_Name"].ToString();
                                FundCRHAPRow["FundCRHAPTableRKACOUT"] = FundCRTable.Rows[i]["FundCRTableRKACOUT"].ToString();
                                FundCRHAPRow["FundCRHAPTableOUTAMT"] = dB2OUTAMT;
                                FundCRHAPTable.Rows.Add(FundCRHAPRow);

                            }

                        }
                    }
                }
                /*---------------- 계정과별 자금 집계 현황...end-------------------------- */


                DataTable FundTable = new DataTable();
                DataRow FundRow;

                FundTable.Columns.Add("FundTableDPAC", typeof(System.String));
                FundTable.Columns.Add("FundTableCDACIN", typeof(System.String));
                FundTable.Columns.Add("FundTableCDACINUP", typeof(System.String));
                FundTable.Columns.Add("FundTableINFUND_Code", typeof(System.String));
                FundTable.Columns.Add("FundTableINFUND_Name", typeof(System.String));
                FundTable.Columns.Add("FundTableRKACIN", typeof(System.String));
                FundTable.Columns.Add("FundTableINAMT", typeof(System.Double));
                FundTable.Columns.Add("FundTableCDACOUT", typeof(System.String));
                FundTable.Columns.Add("FundTableCDACOUTUP", typeof(System.String));
                FundTable.Columns.Add("FundTableOUTFUND_Code", typeof(System.String));
                FundTable.Columns.Add("FundTableOUTFUND_Name", typeof(System.String));
                FundTable.Columns.Add("FundTableRKACOUT", typeof(System.String));
                FundTable.Columns.Add("FundTableOUTAMT", typeof(System.Double));


                if (CBO01_INQOPTION.GetValue().ToString() == "1") //집계현황
                {
                    for (int i = 0; i < dsMrge.Tables[0].Rows.Count; i++)
                    {
                        double ddsLinCnt = 0;

                        //수입 
                        sFilter = "";
                        sSort = "";
                        if (dsMrge.Tables[0].Rows[i][0].ToString().Substring(0, 1) == "B")
                        {
                            sFilter = " SUBSTRING(FundDRHAPTableDPAC,1,4) = '" + dsMrge.Tables[0].Rows[i][0].ToString() + "'";
                        }
                        else
                        {
                            sFilter = " SUBSTRING(FundDRHAPTableDPAC,1,1) = '" + dsMrge.Tables[0].Rows[i][0].ToString() + "'";
                        }

                        sSort = "FundDRHAPTableCDACIN ASC ";

                        DataRow[] DataRowDRMrge;
                        DataRowDRMrge = FundDRHAPTable.Select(sFilter, sSort);

                        //지출

                        sFilter = "";
                        sSort = "";
                        if (dsMrge.Tables[0].Rows[i][0].ToString().Substring(0, 1) == "B")
                        {
                            sFilter = " SUBSTRING(FundCRHAPTableDPAC,1,4) = '" + dsMrge.Tables[0].Rows[i][0].ToString() + "'";
                        }
                        else
                        {
                            sFilter = " SUBSTRING(FundCRHAPTableDPAC,1,1) = '" + dsMrge.Tables[0].Rows[i][0].ToString() + "'";
                        }

                        sSort = "FundCRHAPTableCDACOUT ASC ";

                        DataRow[] DataRowCRMrge;
                        DataRowCRMrge = FundCRHAPTable.Select(sFilter, sSort);

                        if (DataRowDRMrge.Length > DataRowCRMrge.Length)
                        {
                            ddsLinCnt = Convert.ToDouble(DataRowDRMrge.Length);
                        }
                        else
                        {
                            ddsLinCnt = Convert.ToDouble(DataRowCRMrge.Length);
                        }

                        for (int j = 0; j < ddsLinCnt; j++)
                        {
                            FundRow = FundTable.NewRow();
                            FundRow["FundTableDPAC"] = UP_Get_SAUP(dsMrge.Tables[0].Rows[i][0].ToString());
                            if (j < DataRowDRMrge.Length)
                            {
                                if (DataRowDRMrge[j]["FundDRHAPTableCDACIN"].ToString().Length > 8)
                                {
                                    FundRow["FundTableCDACIN"] = DataRowDRMrge[j]["FundDRHAPTableCDACIN"].ToString().Substring(0, 8);
                                }
                                else
                                {
                                    FundRow["FundTableCDACIN"] = DataRowDRMrge[j]["FundDRHAPTableCDACIN"].ToString();
                                }

                                FundRow["FundTableRKACIN"] = DataRowDRMrge[j]["FundDRHAPTableRKACIN"].ToString();

                                FundRow["FundTableCDACINUP"] = DataRowDRMrge[j]["FundDRHAPTableCDACUP"].ToString();
                                FundRow["FundTableINFUND_Code"] = DataRowDRMrge[j]["FundDRHAPTableFUND_Code"].ToString();
                                FundRow["FundTableINFUND_Name"] = DataRowDRMrge[j]["FundDRHAPTableFUND_Name"].ToString();

                                FundRow["FundTableINAMT"] = Convert.ToDouble(DataRowDRMrge[j]["FundDRHAPTableINAMT"].ToString());
                            }
                            else
                            {
                                FundRow["FundTableCDACIN"] = "";
                                FundRow["FundTableCDACINUP"] = "";
                                FundRow["FundTableINFUND_Code"] = "";
                                FundRow["FundTableINFUND_Name"] = "";
                                FundRow["FundTableRKACIN"] = "";
                                FundRow["FundTableINAMT"] = 0;
                            }
                            if (j < DataRowCRMrge.Length)
                            {
                                if (DataRowCRMrge[j]["FundCRHAPTableCDACOUT"].ToString().Length > 8)
                                {
                                    FundRow["FundTableCDACOUT"] = DataRowCRMrge[j]["FundCRHAPTableCDACOUT"].ToString().Substring(0, 8);
                                }
                                else
                                {
                                    FundRow["FundTableCDACOUT"] = DataRowCRMrge[j]["FundCRHAPTableCDACOUT"].ToString();
                                }
                                FundRow["FundTableRKACOUT"] = DataRowCRMrge[j]["FundCRHAPTableRKACOUT"].ToString();
                                FundRow["FundTableCDACOUTUP"] = DataRowCRMrge[j]["FundCRHAPTableCDACUP"].ToString();
                                FundRow["FundTableOUTFUND_Code"] = DataRowCRMrge[j]["FundCRHAPTableFUND_Code"].ToString();
                                FundRow["FundTableOUTFUND_Name"] = DataRowCRMrge[j]["FundCRHAPTableFUND_Name"].ToString();

                                FundRow["FundTableOUTAMT"] = Convert.ToDouble(DataRowCRMrge[j]["FundCRHAPTableOUTAMT"].ToString());
                            }
                            else
                            {
                                FundRow["FundTableCDACOUT"] = "";
                                FundRow["FundTableCDACOUTUP"] = "";
                                FundRow["FundTableOUTFUND_Code"] = "";
                                FundRow["FundTableOUTFUND_Name"] = "";
                                FundRow["FundTableRKACOUT"] = "";
                                FundRow["FundTableOUTAMT"] = 0;
                            }
                            FundTable.Rows.Add(FundRow);
                        }
                    }
                }

                //일자별 합계 ADD
                int iNum = 0;

                for (int i = 1; i < FundTable.Rows.Count; i++)
                {

                    if (FundTable.Rows[i - 1]["FundTableDPAC"].ToString() != FundTable.Rows[i]["FundTableDPAC"].ToString())
                    {
                        sFilter = "";

                        FundRow = FundTable.NewRow();
                        FundTable.Rows.InsertAt(FundRow, i);

                        FundTable.Rows[i]["FundTableCDACIN"] = "소 계";
                        sFilter = " FundTableDPAC = '" + FundTable.Rows[i - 1]["FundTableDPAC"].ToString() + "'";
                        FundTable.Rows[i]["FundTableINAMT"] = Convert.ToDouble(Get_Numeric(FundTable.Compute("SUM(FundTableINAMT)", sFilter).ToString()));
                        FundTable.Rows[i]["FundTableOUTAMT"] = Convert.ToDouble(Get_Numeric(FundTable.Compute("SUM(FundTableOUTAMT)", sFilter).ToString()));

                        i = i + 1;
                    }
                }

                iNum = FundTable.Rows.Count;

                sFilter = "";

                if (iNum > 0)
                {
                    FundRow = FundTable.NewRow();
                    FundTable.Rows.InsertAt(FundRow, iNum);

                    FundTable.Rows[iNum]["FundTableCDACIN"] = "소 계";
                    sFilter = " FundTableDPAC = '" + FundTable.Rows[iNum - 1]["FundTableDPAC"].ToString() + "'";
                    FundTable.Rows[iNum]["FundTableINAMT"] = Convert.ToDouble(Get_Numeric(FundTable.Compute("SUM(FundTableINAMT)", sFilter).ToString()));
                    FundTable.Rows[iNum]["FundTableOUTAMT"] = Convert.ToDouble(Get_Numeric(FundTable.Compute("SUM(FundTableOUTAMT)", sFilter).ToString()));

                    sFilter = "";

                    double dInamtTotal = 0;
                    double dOutamtTotal = 0;


                    FundRow = FundTable.NewRow();
                    FundTable.Rows.InsertAt(FundRow, iNum + 1);

                    FundTable.Rows[iNum + 1]["FundTableCDACIN"] = "총 계";
                    //sFilter= " FundTableDATE >= '"+Get_Date(txtSTDATE.Text.Trim())+"' AND";                    
                    //sFilter= sFilter + " FundTableDATE <= '"+Get_Date(txtETDATE.Text.Trim())+"' AND";                    
                    sFilter = " FundTableCDACIN <> '소 계' ";

                    dInamtTotal = Convert.ToDouble(Get_Numeric(FundTable.Compute("SUM(FundTableINAMT)", sFilter).ToString()));
                    dOutamtTotal = Convert.ToDouble(Get_Numeric(FundTable.Compute("SUM(FundTableOUTAMT)", sFilter).ToString()));

                    FundTable.Rows[iNum + 1]["FundTableINAMT"] = dInamtTotal;
                    FundTable.Rows[iNum + 1]["FundTableOUTAMT"] = dOutamtTotal;

                    FundRow = FundTable.NewRow();
                    FundTable.Rows.InsertAt(FundRow, iNum + 2);
                    FundTable.Rows[iNum + 2]["FundTableCDACIN"] = "과부족";
                    FundTable.Rows[iNum + 2]["FundTableINAMT"] = dInamtTotal - dOutamtTotal;
                    FundTable.Rows[iNum + 2]["FundTableOUTAMT"] = 0;
                }

                FundTable.TableName = "AC5030";

                dsParam.Tables.Add(FundTable);

                return dsParam;
            }
            catch (Exception e)
            {
                string dd = e.Message;
                string ss = dd;
            }

            return dsParam;
        }
        #endregion		

        #region Description : DataTable Search
        private bool UP_DataTableSearch(DataTable TableSearch, string sDPAC, string sCDAC, string sCDACUP, string sFUND)
        {
            int ik = 0;
            string kk = "";

            try
            {
                for (int k = 0; k < TableSearch.Rows.Count; k++)
                {
                    ik = k;

                    kk = TableSearch.Rows[k][0].ToString();

                    //무역부는 팀별로 분리
                    if (sDPAC.Substring(0, 1) == "B")
                    {
                        if (TableSearch.Rows[k][0].ToString().Length == 4)
                        {
                            if ((sDPAC.Substring(0, 4) == TableSearch.Rows[k][0].ToString().Substring(0, 4)) &&
                                (sCDAC == TableSearch.Rows[k][1].ToString()) &&
                                (sCDACUP == TableSearch.Rows[k][2].ToString()) &&
                                (sFUND == TableSearch.Rows[k][3].ToString()))
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if ((sDPAC.Substring(0, 1) == TableSearch.Rows[k][0].ToString().Substring(0, 1)) &&
                            (sCDAC == TableSearch.Rows[k][1].ToString()) &&
                            (sCDACUP == TableSearch.Rows[k][2].ToString()) &&
                            (sFUND == TableSearch.Rows[k][3].ToString()))
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
            catch
            {
                
            }

            return true;
        }

        private bool UP_DataTableSearchIN(DataTable TableSearch, string sDPAC, string sCDAC, string sVLMI1, string sVLMI2)
        {
            for (int k = 0; k < TableSearch.Rows.Count; k++)
            {
                if (sDPAC.Substring(0, 1) == TableSearch.Rows[k][0].ToString().Substring(0, 1) &&
                    sCDAC == TableSearch.Rows[k][1].ToString() &&
                    sVLMI1 == TableSearch.Rows[k][2].ToString() &&
                    sVLMI2 == TableSearch.Rows[k][3].ToString())
                {
                    return false;
                }
            }

            return true;
        }  		
        #endregion

        #region Description : 건별 수입/지출 내역-수입,지출(원화,외화)
        public DataSet UP_ComputeDsDetailInWon(DataSet dsindex, DataSet dsMrge)
        {
            DataSet dsParam = new DataSet();

            string sFilter = "";
            string sSort = "";

            string sB2VLMI1 = "";
            string sB2VLMI2 = "";


            DataTable FundDRHAPTable = new DataTable();
            DataRow FundDRHAPRow;

            FundDRHAPTable.Columns.Add("FundDRHAPTableDPAC", typeof(System.String));
            FundDRHAPTable.Columns.Add("FundDRHAPTableCDACIN", typeof(System.String));
            FundDRHAPTable.Columns.Add("FundDRHAPTableCDACUP", typeof(System.String));
            FundDRHAPTable.Columns.Add("FundDRHAPTableCDFDCODE", typeof(System.String));
            FundDRHAPTable.Columns.Add("FundDRHAPTableCDFDNAME", typeof(System.String));
            FundDRHAPTable.Columns.Add("FundDRHAPTableVLMI1IN", typeof(System.String));
            FundDRHAPTable.Columns.Add("FundDRHAPTableVLMI2IN", typeof(System.String));
            FundDRHAPTable.Columns.Add("FundDRHAPTableRKACIN", typeof(System.String));
            FundDRHAPTable.Columns.Add("FundDRHAPTableINAMT", typeof(System.Double));
            FundDRHAPTable.Columns.Add("FundDRHAPTableWINAMT", typeof(System.Double));
            FundDRHAPTable.Columns.Add("FundDRHAPTableYUL", typeof(System.String));
            FundDRHAPTable.Columns.Add("FundDRHAPTableJUNO", typeof(System.String));
            FundDRHAPTable.Columns.Add("FundDRHAPTableDTMK", typeof(System.String));


            for (int i = 0; i < dsindex.Tables[0].Rows.Count; i++)
            {
                sB2VLMI1 = dsindex.Tables[0].Rows[i]["PCVLMI1"].ToString();
                sB2VLMI2 = dsindex.Tables[0].Rows[i]["PCVLMI2"].ToString();

                FundDRHAPRow = FundDRHAPTable.NewRow();
                if (dsindex.Tables[0].Rows[i]["PCDPAC"].ToString().Substring(0, 1) == "C" ||
                    dsindex.Tables[0].Rows[i]["PCDPAC"].ToString().Substring(0, 1) == "G" ||
                    dsindex.Tables[0].Rows[i]["PCDPAC"].ToString().Substring(0, 1) == "A")
                {
                    FundDRHAPRow["FundDRHAPTableDPAC"] = "A";
                }
                else
                {
                    if (dsindex.Tables[0].Rows[i]["PCDPAC"].ToString().Substring(0, 1) == "B")
                    {
                        FundDRHAPRow["FundDRHAPTableDPAC"] = dsindex.Tables[0].Rows[i]["PCDPAC"].ToString().Substring(0, 4);
                    }
                    else
                    {
                        FundDRHAPRow["FundDRHAPTableDPAC"] = dsindex.Tables[0].Rows[i]["PCDPAC"].ToString().Substring(0, 1);
                    }
                }
                FundDRHAPRow["FundDRHAPTableCDACIN"] = dsindex.Tables[0].Rows[i]["A1NMAC"].ToString();
                FundDRHAPRow["FundDRHAPTableCDACUP"] = dsindex.Tables[0].Rows[i]["A1NMACUP"].ToString();
                FundDRHAPRow["FundDRHAPTableCDFDCODE"] = dsindex.Tables[0].Rows[i]["PCCDFD"].ToString();
                FundDRHAPRow["FundDRHAPTableCDFDNAME"] = dsindex.Tables[0].Rows[i]["A3ABFD"].ToString();
                FundDRHAPRow["FundDRHAPTableVLMI1IN"] = sB2VLMI1;
                FundDRHAPRow["FundDRHAPTableVLMI2IN"] = sB2VLMI2;
                FundDRHAPRow["FundDRHAPTableRKACIN"] = dsindex.Tables[0].Rows[i]["PCRKAC"].ToString();
                FundDRHAPRow["FundDRHAPTableINAMT"] = Convert.ToDouble(dsindex.Tables[0].Rows[i]["PCWAMT"].ToString());
                FundDRHAPRow["FundDRHAPTableWINAMT"] = Convert.ToDouble(Get_Numeric(dsindex.Tables[0].Rows[i]["PCDAMT"].ToString().Trim()));

                if (Convert.ToDouble(Get_Numeric(dsindex.Tables[0].Rows[i]["PCDYUL"].ToString().Trim())) > 0)
                {

                    FundDRHAPRow["FundDRHAPTableYUL"] = dsindex.Tables[0].Rows[i]["PCDGUB"].ToString().Trim().Substring(0, 1) + ' ' +
                                                         Get_Numeric(dsindex.Tables[0].Rows[i]["PCDYUL"].ToString().Trim());
                }
                else
                {
                    FundDRHAPRow["FundDRHAPTableYUL"] = Get_Numeric(dsindex.Tables[0].Rows[i]["PCDYUL"].ToString().Trim());
                }
                FundDRHAPRow["FundDRHAPTableJUNO"] = dsindex.Tables[0].Rows[i]["PCJUNNO1"].ToString();
                FundDRHAPRow["FundDRHAPTableDTMK"] = dsindex.Tables[0].Rows[i]["PCDATE"].ToString();
                FundDRHAPTable.Rows.Add(FundDRHAPRow);

                sB2VLMI1 = "";
                sB2VLMI2 = "";

            }

            /*---------------- 계정과별 자금 집계 현황-------------------------- */
            //계정별/사업부별/일자별 집계-수입


            DataTable FundTable = new DataTable();
            DataRow FundRow;

            FundTable.Columns.Add("FundTableDPAC", typeof(System.String));
            FundTable.Columns.Add("FundTableCDAC", typeof(System.String));
            FundTable.Columns.Add("FundTableCDACUP", typeof(System.String));
            FundTable.Columns.Add("FundTableCDFD", typeof(System.String));
            FundTable.Columns.Add("FundTableCDFDNM", typeof(System.String));
            FundTable.Columns.Add("FundTableVLMI1", typeof(System.String));
            FundTable.Columns.Add("FundTableVLMI2", typeof(System.String));
            FundTable.Columns.Add("FundTableRKAC", typeof(System.String));
            FundTable.Columns.Add("FundTableAMT", typeof(System.Double));
            FundTable.Columns.Add("FundTableWAMT", typeof(System.Double));
            FundTable.Columns.Add("FundTableYUL", typeof(System.String));
            FundTable.Columns.Add("FundTableJUNO", typeof(System.String));
            FundTable.Columns.Add("FundTableDTMK", typeof(System.String));

            for (int i = 0; i < dsMrge.Tables[0].Rows.Count; i++)
            {
                double ddsLinCnt = 0;

                //수입 
                sFilter = "";
                sSort = "";
                if (dsMrge.Tables[0].Rows[i][0].ToString().Substring(0, 1) == "B")
                {
                    sFilter = " SUBSTRING(FundDRHAPTableDPAC,1,4) = '" + dsMrge.Tables[0].Rows[i][0].ToString() + "'";
                }
                else
                {
                    sFilter = " SUBSTRING(FundDRHAPTableDPAC,1,1) = '" + dsMrge.Tables[0].Rows[i][0].ToString() + "'";
                }

                sSort = "FundDRHAPTableCDACIN ASC ";

                DataRow[] DataRowDRMrge;
                DataRowDRMrge = FundDRHAPTable.Select(sFilter, sSort);

                ddsLinCnt = Convert.ToDouble(DataRowDRMrge.Length);

                for (int j = 0; j < ddsLinCnt; j++)
                {
                    FundRow = FundTable.NewRow();
                    FundRow["FundTableDPAC"] = UP_Get_SAUP(dsMrge.Tables[0].Rows[i][0].ToString());
                    if (DataRowDRMrge[j]["FundDRHAPTableCDACIN"].ToString().Length > 8)
                    {
                        FundRow["FundTableCDAC"] = DataRowDRMrge[j]["FundDRHAPTableCDACIN"].ToString().Substring(0, 8);
                    }
                    else
                    {
                        FundRow["FundTableCDAC"] = DataRowDRMrge[j]["FundDRHAPTableCDACIN"].ToString();
                    }

                    FundRow["FundTableCDACUP"] = DataRowDRMrge[j]["FundDRHAPTableCDACUP"].ToString();
                    FundRow["FundTableCDFD"] = DataRowDRMrge[j]["FundDRHAPTableCDFDCODE"].ToString();
                    FundRow["FundTableCDFDNM"] = DataRowDRMrge[j]["FundDRHAPTableCDFDNAME"].ToString();

                    FundRow["FundTableVLMI1"] = DataRowDRMrge[j]["FundDRHAPTableVLMI1IN"].ToString();
                    FundRow["FundTableVLMI2"] = DataRowDRMrge[j]["FundDRHAPTableVLMI2IN"].ToString();
                    FundRow["FundTableRKAC"] = DataRowDRMrge[j]["FundDRHAPTableRKACIN"].ToString();

                    FundRow["FundTableAMT"] = Convert.ToDouble(DataRowDRMrge[j]["FundDRHAPTableINAMT"].ToString());
                    FundRow["FundTableWAMT"] = Convert.ToDouble(DataRowDRMrge[j]["FundDRHAPTableWINAMT"].ToString());
                    FundRow["FundTableYUL"] = DataRowDRMrge[j]["FundDRHAPTableYUL"].ToString();
                    FundRow["FundTableJUNO"] = DataRowDRMrge[j]["FundDRHAPTableJUNO"].ToString();
                    FundRow["FundTableDTMK"] = DataRowDRMrge[j]["FundDRHAPTableDTMK"].ToString();
                    FundTable.Rows.Add(FundRow);
                }
            }


            //일자별 합계 ADD
            int iNum = 0;

            for (int i = 1; i < FundTable.Rows.Count; i++)
            {

                if (FundTable.Rows[i - 1]["FundTableDPAC"].ToString() != FundTable.Rows[i]["FundTableDPAC"].ToString())
                {
                    sFilter = "";

                    FundRow = FundTable.NewRow();
                    FundTable.Rows.InsertAt(FundRow, i);

                    FundTable.Rows[i]["FundTableCDAC"] = "소 계";
                    sFilter = " FundTableDPAC = '" + FundTable.Rows[i - 1]["FundTableDPAC"].ToString() + "'";
                    FundTable.Rows[i]["FundTableAMT"] = Convert.ToDouble(Get_Numeric(FundTable.Compute("SUM(FundTableAMT)", sFilter).ToString()));
                    FundTable.Rows[i]["FundTableWAMT"] = Convert.ToDouble(Get_Numeric(FundTable.Compute("SUM(FundTableWAMT)", sFilter).ToString()));

                    i = i + 1;
                }
            }

            iNum = FundTable.Rows.Count;

            sFilter = "";

            if (iNum > 0)
            {
                FundRow = FundTable.NewRow();
                FundTable.Rows.InsertAt(FundRow, iNum);

                FundTable.Rows[iNum]["FundTableCDAC"] = "소 계";
                sFilter = " FundTableDPAC = '" + FundTable.Rows[iNum - 1]["FundTableDPAC"].ToString() + "'";
                FundTable.Rows[iNum]["FundTableAMT"] = Convert.ToDouble(Get_Numeric(FundTable.Compute("SUM(FundTableAMT)", sFilter).ToString()));
                FundTable.Rows[iNum]["FundTableWAMT"] = Convert.ToDouble(Get_Numeric(FundTable.Compute("SUM(FundTableWAMT)", sFilter).ToString()));

                sFilter = "";

                double dInamtTotal = 0;
                double dWInamtTotal = 0;

                FundRow = FundTable.NewRow();
                FundTable.Rows.InsertAt(FundRow, iNum + 1);

                FundTable.Rows[iNum + 1]["FundTableCDAC"] = "총 계";
                //sFilter= " FundTableDATE >= '"+Get_Date(txtSTDATE.Text.Trim())+"' AND";                    
                //sFilter= sFilter + " FundTableDATE <= '"+Get_Date(txtETDATE.Text.Trim())+"' AND";                    
                sFilter = " FundTableCDAC <> '소 계' ";

                dInamtTotal = Convert.ToDouble(Get_Numeric(FundTable.Compute("SUM(FundTableAMT)", sFilter).ToString()));
                dWInamtTotal = Convert.ToDouble(Get_Numeric(FundTable.Compute("SUM(FundTableWAMT)", sFilter).ToString()));

                FundTable.Rows[iNum + 1]["FundTableAMT"] = dInamtTotal;
                FundTable.Rows[iNum + 1]["FundTableWAMT"] = dWInamtTotal;
            }

            FundTable.TableName = "AC5030";

            dsParam.Tables.Add(FundTable);

            return dsParam;

        }
        #endregion	

        #region Description : 사업부명 반환 함수
        private string UP_Get_SAUP(string sSAUPCode)
        {
            string sReturnStr = "";

            if (sSAUPCode == "A")
            {
                sReturnStr = "관 리";
            }
            else if (sSAUPCode == "T")
            {
                sReturnStr = "UTT";
            }
            else if (sSAUPCode == "S")
            {
                sReturnStr = "SILO";
            }
            else if (sSAUPCode == "B801")
            {
                sReturnStr = "석유화학";
            }
            else if (sSAUPCode == "B802")
            {
                sReturnStr = "농업자원";
            }
            else
            {
                sReturnStr = "기타";
            }

            return sReturnStr;
        }
        #endregion


    }
}
