using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 자금실적 원천계정 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.09.28 13:05
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_29S2Z357 : 자금실적 원천계정 조회
    ///  TY_P_AC_29S3B358 : 자금실적 원천부서 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_29S3D360 : 자금실적 원천(집계) 조회
    ///  TY_S_AC_29S3W362 : 자금실적 원천(건별) 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  GCDDP : 사업장코드
    ///  GSEMOK : 계정세목
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    /// </summary>
    public partial class TYACKF011S : TYBase
    {
        #region Description : 폼로드 이벤트
        public TYACKF011S()
        {
            InitializeComponent();
        }

        private void TYACKF011S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_GSTDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_GEDDATE.SetValue(DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd"));
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {           
  
            //집계
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29S2Z357", this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString(),
                                                        CBO01_GSEMOK.GetValue(), CBO01_GCDDP.GetValue() );
            DataSet dsIndex = this.DbConnector.ExecuteDataSet();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29S3B358", this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString(),
                                                        CBO01_GSEMOK.GetValue(), CBO01_GCDDP.GetValue());
            DataSet dsDpac = this.DbConnector.ExecuteDataSet();
            this.FPS91_TY_S_AC_29S3D360.SetValue(UP_ComputeDsHap(dsIndex, dsDpac));
            if (this.FPS91_TY_S_AC_29S3D360.CurrentRowCount > 0)
            {
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_29S3D360, "FUNDTABLECDACIN", "소 계", SumRowType.SubTotal);
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_29S3D360, "FUNDTABLECDACIN", "총 계", SumRowType.Total);
            }

            this.FPS91_TY_S_AC_29S3W362.SetValue(UP_ComputeDsDetail(dsIndex, dsDpac));

            if (this.FPS91_TY_S_AC_29S3W362.CurrentRowCount > 0)
            {
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_29S3W362, "FundTableCDACIN", "소 계", SumRowType.SubTotal);
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_29S3W362, "FundTableCDACIN", "총 계", SumRowType.Total);
            }
   
        }
        #endregion

        #region Description : 집계 수입/지출 내역 Hap
        public DataSet UP_ComputeDsHap(DataSet dsindex, DataSet dsDpac)
        {
            DataSet dsParam = new DataSet();

            try
            {
                double dInAmtHap = 0;

                int iNum = 0;

              
                DataTable FundTable = new DataTable();
                DataRow FundRow;

                FundTable.Columns.Add("FundTableDATE", typeof(System.String));
                FundTable.Columns.Add("FundTableDPAC", typeof(System.String));
                FundTable.Columns.Add("FundTableDPACNM", typeof(System.String));
                FundTable.Columns.Add("FundTableCDACIN", typeof(System.String));
                FundTable.Columns.Add("FundTableCDACUP", typeof(System.String));
                FundTable.Columns.Add("FundTableFUNDCODE", typeof(System.String));
                FundTable.Columns.Add("FundTableFUND", typeof(System.String));
                FundTable.Columns.Add("FundTableRKACIN", typeof(System.String));
                FundTable.Columns.Add("FundTableINAMT", typeof(System.Double));

                string sSort = "";
                string sFilter = "";

                for (int i = 0; i < dsDpac.Tables[0].Rows.Count; i++)
                {
                    DataRow[] DataRowSort;

                    sSort = "";
                    sFilter = "";
                    if (dsDpac.Tables[0].Rows[i][0].ToString().Substring(0, 1) == "A")
                    {
                        sFilter = " substring(PCDPAC,1,1) IN ('A','C','G') ";
                    }
                    else if (dsDpac.Tables[0].Rows[i][0].ToString().Substring(0, 1) == "B")
                    {
                        sFilter = " substring(PCDPAC,1,4) = '" + dsDpac.Tables[0].Rows[i][0].ToString() + "'";
                    }
                    else
                    {
                        sFilter = " substring(PCDPAC,1,1) = '" + dsDpac.Tables[0].Rows[i][0].ToString() + "'";
                    }
                    sFilter = sFilter + " AND PCCDAC = '" + dsDpac.Tables[0].Rows[i][1].ToString() + "'";

                    DataRowSort = dsindex.Tables[0].Select(sFilter, sSort);

                    for (int j = 0; j < DataRowSort.Length; j++)
                    {
                        dInAmtHap = dInAmtHap + Convert.ToDouble(DataRowSort[j]["PCWAMT"].ToString());

                        //마지막 자료만 입력
                        if (j == (DataRowSort.Length - 1))
                        {
                            FundRow = FundTable.NewRow();
                            FundRow["FundTableDATE"] = DataRowSort[j]["PCDATE"].ToString();
                            if (DataRowSort[j]["PCDPAC"].ToString().Substring(0, 1) == "B")
                            {
                                FundRow["FundTableDPAC"] = DataRowSort[j]["PCDPAC"].ToString().Substring(0, 4);
                            }
                            else if (DataRowSort[j]["PCDPAC"].ToString().Substring(0, 1) == "A" ||
                                DataRowSort[j]["PCDPAC"].ToString().Substring(0, 1) == "C" ||
                                DataRowSort[j]["PCDPAC"].ToString().Substring(0, 1) == "G")
                            {
                                FundRow["FundTableDPAC"] = "A";
                            }
                            else
                            {
                                FundRow["FundTableDPAC"] = DataRowSort[j]["PCDPAC"].ToString().Substring(0, 1);
                            }

                            if (DataRowSort[j]["PCDPAC"].ToString().Substring(0, 4) == "B801")
                            {
                                FundRow["FundTableDPACNM"] = "석유화학";
                            }
                            else if (DataRowSort[j]["PCDPAC"].ToString().Substring(0, 4) == "B802")
                            {
                                FundRow["FundTableDPACNM"] = "농업자원";
                            }
                            else if (DataRowSort[j]["PCDPAC"].ToString().Substring(0, 1) == "A" ||
                                     DataRowSort[j]["PCDPAC"].ToString().Substring(0, 1) == "C" ||
                                     DataRowSort[j]["PCDPAC"].ToString().Substring(0, 1) == "G")
                            {
                                FundRow["FundTableDPACNM"] = "관리";
                            }
                            else if (DataRowSort[j]["PCDPAC"].ToString().Substring(0, 1) == "S")
                            {
                                FundRow["FundTableDPACNM"] = "SILO";
                            }
                            else
                            {
                                FundRow["FundTableDPACNM"] = "UTT";
                            }

                            FundRow["FundTableCDACIN"] = DataRowSort[j]["A1NMAC"].ToString();
                            FundRow["FundTableCDACUP"] = DataRowSort[j]["A1NMACUP"].ToString();
                            FundRow["FundTableFUNDCODE"] = DataRowSort[j]["PCCDFD"].ToString();
                            FundRow["FundTableFUND"] = DataRowSort[j]["A3ABFD"].ToString();
                            FundRow["FundTableRKACIN"] = DataRowSort[j]["PCRKAC"].ToString() + "외";
                            FundRow["FundTableINAMT"] = dInAmtHap;
                            FundTable.Rows.Add(FundRow);

                            dInAmtHap = 0;
                        }

                    }//for..end - for( int j = 0; j < DataRowSort.Length; j++)

                }//for..end - for( int i=0; i < dsDpac.Tables[0].Rows.Count; i++)

                //사업별 합계처리
                for (int i = 1; i < FundTable.Rows.Count; i++)
                {
                    int iDplen = 1;
                    int iDplenNew = 1;

                    //무역부일경우 B1, B2, B5 
                    if (FundTable.Rows[i - 1]["FundTableDPAC"].ToString().Substring(0, 1) == "B")
                    {
                        iDplen = 4;
                    }

                    if (FundTable.Rows[i]["FundTableDPAC"].ToString().Substring(0, 1) != "B")
                    {
                        iDplenNew = 1;
                    }
                    else
                    {
                        iDplenNew = 4;
                    }

                    if (FundTable.Rows[i - 1]["FundTableDPAC"].ToString().Substring(0, iDplen) != FundTable.Rows[i]["FundTableDPAC"].ToString().Substring(0, iDplenNew))
                    {
                        sFilter = "";

                        FundRow = FundTable.NewRow();
                        FundTable.Rows.InsertAt(FundRow, i);

                        FundTable.Rows[i]["FundTableCDACIN"] = "소 계";
                        sFilter = " FundTableDPAC = '" + FundTable.Rows[i - 1]["FundTableDPAC"].ToString() + "'";
                        FundTable.Rows[i]["FundTableINAMT"] = Convert.ToDouble(Get_Numeric(FundTable.Compute("SUM(FundTableINAMT)", sFilter).ToString()));

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

                    sFilter = "";

                    double dInamtTotal = 0;

                    FundRow = FundTable.NewRow();
                    FundTable.Rows.InsertAt(FundRow, iNum + 1);

                    FundTable.Rows[iNum + 1]["FundTableCDACIN"] = "총 계";
                    sFilter = " FundTableCDACIN <> '소 계' ";

                    dInamtTotal = Convert.ToDouble(Get_Numeric(FundTable.Compute("SUM(FundTableINAMT)", sFilter).ToString()));

                    FundTable.Rows[iNum + 1]["FundTableINAMT"] = dInamtTotal;

                }

                FundTable.TableName = "AC5060";

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

        #region Description : 집계 수입/지출 내역 Detail
        public DataSet UP_ComputeDsDetail(DataSet dsindex, DataSet dsDpac)
        {
            DataSet dsParam = new DataSet();

            try
            {

                int iNum = 0;

               
                DataTable FundTable = new DataTable();
                DataRow FundRow;

                FundTable.Columns.Add("FundTableDATE", typeof(System.String));
                FundTable.Columns.Add("FundTableDPAC", typeof(System.String));
                FundTable.Columns.Add("FundTableDPACNM", typeof(System.String));
                FundTable.Columns.Add("FundTableCDACIN", typeof(System.String));
                FundTable.Columns.Add("FundTableCDACUP", typeof(System.String));
                FundTable.Columns.Add("FundTableFUNDCODE", typeof(System.String));
                FundTable.Columns.Add("FundTableFUND", typeof(System.String));
                FundTable.Columns.Add("FundTableVLMI1", typeof(System.String));
                FundTable.Columns.Add("FundTableRKACIN", typeof(System.String));
                FundTable.Columns.Add("FundTableINAMT", typeof(System.Double));
                FundTable.Columns.Add("FundTableJUNNO", typeof(System.String));

                string sSort = "";
                string sFilter = "";

                for (int i = 0; i < dsDpac.Tables[0].Rows.Count; i++)
                {
                    DataRow[] DataRowSort;

                    sSort = "";
                    sFilter = "";
                    if (dsDpac.Tables[0].Rows[i][0].ToString().Substring(0, 1) == "A")
                    {
                        sFilter = " substring(PCDPAC,1,1) IN ('A','C','G') ";
                    }
                    else if (dsDpac.Tables[0].Rows[i][0].ToString().Substring(0, 1) == "B")
                    {
                        sFilter = " substring(PCDPAC,1,4) = '" + dsDpac.Tables[0].Rows[i][0].ToString() + "'";
                    }
                    else
                    {
                        sFilter = " substring(PCDPAC,1,1) = '" + dsDpac.Tables[0].Rows[i][0].ToString() + "'";
                    }
                    sFilter = sFilter + " AND PCCDAC = '" + dsDpac.Tables[0].Rows[i][1].ToString() + "'";

                    DataRowSort = dsindex.Tables[0].Select(sFilter, sSort);

                    for (int j = 0; j < DataRowSort.Length; j++)
                    {

                        FundRow = FundTable.NewRow();
                        FundRow["FundTableDATE"] = DataRowSort[j]["PCDATE"].ToString();
                        if (DataRowSort[j]["PCDPAC"].ToString().Substring(0, 1) == "B")
                        {
                            FundRow["FundTableDPAC"] = DataRowSort[j]["PCDPAC"].ToString().Substring(0, 4);
                        }
                        else if (DataRowSort[j]["PCDPAC"].ToString().Substring(0, 1) == "A" ||
                            DataRowSort[j]["PCDPAC"].ToString().Substring(0, 1) == "C" ||
                            DataRowSort[j]["PCDPAC"].ToString().Substring(0, 1) == "G")
                        {
                            FundRow["FundTableDPAC"] = "A";
                        }
                        else
                        {
                            FundRow["FundTableDPAC"] = DataRowSort[j]["PCDPAC"].ToString().Substring(0, 1);
                        }


                        if (DataRowSort[j]["PCDPAC"].ToString().Substring(0, 4) == "B801")
                        {
                            FundRow["FundTableDPACNM"] = "석유화학";
                        }
                        else if (DataRowSort[j]["PCDPAC"].ToString().Substring(0, 4) == "B802")
                        {
                            FundRow["FundTableDPACNM"] = "농업자원";
                        }
                        else if (DataRowSort[j]["PCDPAC"].ToString().Substring(0, 1) == "A" ||
                                 DataRowSort[j]["PCDPAC"].ToString().Substring(0, 1) == "C" ||
                                 DataRowSort[j]["PCDPAC"].ToString().Substring(0, 1) == "G")
                        {
                            FundRow["FundTableDPACNM"] = "관리";
                        }
                        else if (DataRowSort[j]["PCDPAC"].ToString().Substring(0, 1) == "S")
                        {
                            FundRow["FundTableDPACNM"] = "SILO";
                        }
                        else
                        {
                            FundRow["FundTableDPACNM"] = "UTT";
                        }

                        FundRow["FundTableCDACIN"] = DataRowSort[j]["A1NMAC"].ToString();
                        FundRow["FundTableCDACUP"] = DataRowSort[j]["A1NMACUP"].ToString();
                        FundRow["FundTableFUNDCODE"] = DataRowSort[j]["PCCDFD"].ToString();
                        FundRow["FundTableFUND"] = DataRowSort[j]["A3ABFD"].ToString();
                        FundRow["FundTableVLMI1"] = DataRowSort[j]["PCVLMI1"].ToString();
                        FundRow["FundTableRKACIN"] = DataRowSort[j]["PCRKAC"].ToString();
                        FundRow["FundTableINAMT"] = Convert.ToDouble(DataRowSort[j]["PCWAMT"].ToString());
                        FundRow["FundTableJUNNO"] = DataRowSort[j]["PCJUNNO2"].ToString();
                        FundTable.Rows.Add(FundRow);


                    }//for..end - for( int j = 0; j < DataRowSort.Length; j++)

                }//for..end - for( int i=0; i < dsDpac.Tables[0].Rows.Count; i++)

                //사업별 합계처리
                for (int i = 1; i < FundTable.Rows.Count; i++)
                {
                    int iDplen = 1;
                    int iDplenNew = 1;

                    //무역부일경우 B1, B2, B5 
                    if (FundTable.Rows[i - 1]["FundTableDPAC"].ToString().Substring(0, 1) == "B")
                    {
                        iDplen = 4;
                    }

                    if (FundTable.Rows[i]["FundTableDPAC"].ToString().Substring(0, 1) != "B")
                    {
                        iDplenNew = 1;
                    }
                    else
                    {
                        iDplenNew = 4;
                    }

                    if (FundTable.Rows[i - 1]["FundTableDPAC"].ToString().Substring(0, iDplen) != FundTable.Rows[i]["FundTableDPAC"].ToString().Substring(0, iDplenNew))
                    {
                        sFilter = "";

                        FundRow = FundTable.NewRow();
                        FundTable.Rows.InsertAt(FundRow, i);

                        FundTable.Rows[i]["FundTableCDACIN"] = "소 계";
                        sFilter = " FundTableDPAC = '" + FundTable.Rows[i - 1]["FundTableDPAC"].ToString() + "'";
                        FundTable.Rows[i]["FundTableINAMT"] = Convert.ToDouble(Get_Numeric(FundTable.Compute("SUM(FundTableINAMT)", sFilter).ToString()));

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

                    sFilter = "";

                    double dInamtTotal = 0;

                    FundRow = FundTable.NewRow();
                    FundTable.Rows.InsertAt(FundRow, iNum + 1);

                    FundTable.Rows[iNum + 1]["FundTableCDACIN"] = "총 계";
                    sFilter = " FundTableCDACIN <> '소 계' ";

                    dInamtTotal = Convert.ToDouble(Get_Numeric(FundTable.Compute("SUM(FundTableINAMT)", sFilter).ToString()));

                    FundTable.Rows[iNum + 1]["FundTableINAMT"] = dInamtTotal;

                }

                FundTable.TableName = "AC5060";

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
    }
}
